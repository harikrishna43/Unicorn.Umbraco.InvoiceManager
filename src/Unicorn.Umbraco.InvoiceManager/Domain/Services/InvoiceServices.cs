using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Unicorn.Umbraco.InvoiceManager.Models;
using Unicorn.Umbraco.InvoiceManager.Models.Dtos;
using Umbraco.Extensions;
using Unicorn.Umbraco.InvoiceManager.Models.Schema;

namespace Unicorn.Umbraco.InvoiceManager.Services
{
    public class InvoiceServices : IInvoiceService
    {
        private readonly ILogger _logger;
        private readonly IScopeProvider _scopeProvider;
        private readonly IDomainService _domains;
        private readonly IContentService _contentService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public InvoiceServices(InvoiceServiceDependencies dependencies)
        {
            _logger = dependencies.Logger;
            _scopeProvider = dependencies.ScopeProvider;
            _domains = dependencies.Domains;
            _contentService = dependencies.ContentService;
            _umbracoContextAccessor = dependencies.UmbracoContextAccessor;
        }

        public IInvoice CreateInvoice(InvoiceDto option)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));

            using (IScope scope = _scopeProvider.CreateScope())
            {
                try
                {
                    option.DateCreated = DateTime.UtcNow;
                    option.DateModified= DateTime.UtcNow;
                    scope.Database.Insert(option);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                scope.Complete();
            }

            return GetInvoiceById(option.InvoiceId);
        }

        public void DeleteInvoice(IInvoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            if (invoice is not Invoice r) throw new ArgumentException($"Invoice type is not supported: {invoice.GetType()}", nameof(invoice));

            using IScope scope = _scopeProvider.CreateScope();

            try
            {
                r.Dto.IsDeleted = true;
                r.Dto.DateModified = DateTime.UtcNow;
                scope.Database.Update(r.Dto);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete invoice from database.", ex);
            }

            scope.Complete();
        }

        public IEnumerable<IInvoice> GetAll()
        {
            using var scope = _scopeProvider.CreateScope();

            // Generate the SQL for the query
            var sql = scope.SqlContext.Sql().Select<Models.Dtos.InvoiceDto>().From<InvoiceDto>();

            // Make the call to the database
            var customers = scope.Database.Fetch<InvoiceDto>(sql).Where<InvoiceDto>(x => x.IsDeleted == false).Select(Invoice.CreateFromDto);//.Where<CustomerDto>(x=>x.IsDeleted==false);

            scope.Complete();

            return customers;
        }

        public IInvoice GetInvoiceById(int invoiceId)
        {
            // Validate the input
            if (invoiceId == 0) throw new ArgumentException("InvoiceId must have a value", nameof(invoiceId));

            InvoiceDto dto;

            using (IScope scope = _scopeProvider.CreateScope())
            {

                var sql = scope.SqlContext.Sql()
                    .Select<InvoiceDto>()
                    .AndSelect<CustomerDto>()
                    .From<InvoiceDto>()
                    .InnerJoin<CustomerDto>()
                    .On<InvoiceDto, CustomerDto>((left, right) => left.CustomerId == right.CustomerId)
                    .Where<InvoiceDto>(x => x.InvoiceId == invoiceId);

                dto = scope.Database.FirstOrDefault<InvoiceDto>(sql);
                scope.Complete();

            }

            return dto == null ? null : new Invoice(dto);
        }

        public InvoiceSearchResult GetInvoices(InvoiceSearchOption options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            InvoiceSearchResult result;

            using (var scope = _scopeProvider.CreateScope())
            {

                // Generate the SQL for the query
                var sql = scope.SqlContext.Sql()
                    .Select<InvoiceDto>()
                    .AndSelect<CustomerDto>()
                    .From<InvoiceDto>()
                    .InnerJoin<CustomerDto>()
                    .On<InvoiceDto, CustomerDto>((left, right) => left.CustomerId == right.CustomerId);

                // Search by the rootNodeId
                //if (options.Text != null) sql = sql.Where<CustomerDto>(x => x.Name.Contains(options.Text) || x.GSTNumber == options.Text || x.State == options.Text);

                // Search by the type
                if (options.Status == Enums.InvoiceStatus.Draft)
                {
                    int status = (int)options.Status;
                    sql = sql.Where<InvoiceDto>(x => x.Status == status);
                }
                else if (options.Status == Enums.InvoiceStatus.Created)
                {
                    int status = (int)options.Status;
                    sql = sql.Where<InvoiceDto>(x => x.Status == status);
                }

                // Search by the text
                if (string.IsNullOrWhiteSpace(options.Text) == false)
                {
                    if (int.TryParse(options.Text, out int id))
                    {
                        sql = sql.Where<InvoiceDto, CustomerDto>((x,y) =>  x.InvoiceId==id || y.CustomerId==id);
                    }
                    else
                    {
                        sql = sql.Where<InvoiceDto, CustomerDto>((x,y) => y.Name.Contains(options.Text)||x.InvoiceNote.Contains(options.Text) );
                    }
                }
                sql = sql.Where<InvoiceDto>(x => x.IsDeleted == false);
                sql = sql.OrderByDescending<InvoiceDto>(x => x.DateModified);

                var adata= scope.Database.FetchMultiple<InvoiceDto, CustomerDto>(sql);
                // Make the call to the database
                InvoiceDto[] all = scope.Database.Query<InvoiceDto>(sql).ToArray();

                // Calculate variables used for the pagination
                int limit = options.Limit;
                int pages = (int)Math.Ceiling(all.Length / (double)limit);
                int page = Math.Max(1, Math.Min(options.Page, pages));
                int offset = (page * limit) - limit;

                // Apply pagination and wrap the database rows
                IInvoice[] items = all
                    .Skip(offset)
                    .Take(limit)
                    .Select(x => (IInvoice)Invoice.CreateFromDto(x))
                    .ToArray();

                // Return the items (on the requested page)
                result = new InvoiceSearchResult(all.Length, limit, offset, page, pages, items);

                scope.Complete();

            }

            return result;
        }

        public IInvoice UpdateInvoice(IInvoice invoice)
        {
            // Some input validation
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            if (invoice is not Invoice c) throw new ArgumentException($"Customer type is not supported: {invoice.GetType()}", nameof(invoice));

            IInvoice existing = GetInvoiceById(invoice.InvoiceId);
            if (existing != null && existing.CustomerId != invoice.CustomerId)
            {

            }
            c.Dto.DateModified = DateTime.UtcNow;
            c.Dto.DateCreated = existing.DateCreated.ToUniversalTime();

            using (var scope = _scopeProvider.CreateScope())
            {
                try
                {
                    scope.Database.Update(c.Dto);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to update customer into the database.");
                    throw ex;
                }
                scope.Complete();
            }

            return invoice;
        }
    }
}
