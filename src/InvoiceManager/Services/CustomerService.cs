using Invoice_Manager.Interfaces;
using Invoice_Manager.Models;
using Invoice_Manager.Models.Dtos;
using Invoice_Manager.Models.Options;
using Invoice_Manager.Models.Schema;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Services.Implement;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Invoice_Manager.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger _logger;
        private readonly IScopeProvider _scopeProvider;
        private readonly IDomainService _domains;
        private readonly IContentService _contentService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public CustomerService(CustomerServiceDependencies dependencies)
        {
            _logger = dependencies.Logger;
            _scopeProvider = dependencies.ScopeProvider;
            _domains = dependencies.Domains;
            _contentService = dependencies.ContentService;
            _umbracoContextAccessor = dependencies.UmbracoContextAccessor;
        }

        public ICustomer AddCustomer(AddCustomerOption option)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));

            Customer item = new Customer()
            {
                Name = option.Name,
                Address = option.Address,
                City = option.City,
                Country = option.Country,
                CustomerType = option.CustomerType,
                GSTNumber = option.GSTNumber,
                State = option.State,
                ZipCode = option.ZipCode,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                Phone=option.Phone
            };

            using (IScope scope = _scopeProvider.CreateScope())
            {
                try
                {
                    scope.Database.Insert(item.Dto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                scope.Complete();
            }

            return GetCustomerById(item.CustomerId);
        }

        public void DeleteCustomer(ICustomer customer)
        {
            // Some input validation
            if (customer == null) throw new ArgumentNullException(nameof(customer));

            // This implementation only supports the "Redirect class"
            if (customer is not Customer r) throw new ArgumentException($"Customer type is not supported: {customer.GetType()}", nameof(customer));

            // Create a new scope
            using IScope scope = _scopeProvider.CreateScope();

            // Remove the redirect from the database
            try
            {
                r.Dto.IsDeleted = true;
                r.Dto.DateModified = DateTime.UtcNow;
                scope.Database.Update(r.Dto);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete redirect from database.", ex);
            }

            // Complete the scope
            scope.Complete();
        }

        public IEnumerable<ICustomer> GetAll()
        {
            // Create a new scope
            using var scope = _scopeProvider.CreateScope();

            // Generate the SQL for the query
            var sql = scope.SqlContext.Sql().Select<CustomerDto>().From<CustomerDto>();

            // Make the call to the database
            var customers = scope.Database.Fetch<CustomerDto>(sql).Where<CustomerDto>(x => x.IsDeleted == false).Select(Customer.CreateFromDto);//.Where<CustomerDto>(x=>x.IsDeleted==false);

            scope.Complete();

            return customers;
        }

        public CustomerSearchResult GetCustomer(CustomerSearchOption options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            CustomerSearchResult result;

            using (var scope = _scopeProvider.CreateScope())
            {

                // Generate the SQL for the query
                var sql = scope.SqlContext.Sql().Select<CustomerDto>().From<CustomerDto>();

                // Search by the rootNodeId
                //if (options.Text != null) sql = sql.Where<CustomerDto>(x => x.Name.Contains(options.Text) || x.GSTNumber == options.Text || x.State == options.Text);

                // Search by the type
                if (options.Type == Enums.CustomerType.Individual)
                {
                    int type = (int)options.Type;
                    sql = sql.Where<CustomerDto>(x => x.CustomerType == type);
                }
                else if (options.Type == Enums.CustomerType.Company)
                {
                    int type = (int)options.Type;
                    sql = sql.Where<CustomerDto>(x => x.CustomerType == type);
                }

                // Search by the text
                if (string.IsNullOrWhiteSpace(options.Text) == false)
                {
                    if (int.TryParse(options.Text, out int customerId))
                    {
                        sql = sql.Where<CustomerDto>(x => x.CustomerId == customerId);
                    }
                    else
                    {
                        sql = sql.Where<CustomerDto>(x => x.Name.Contains(options.Text) || x.GSTNumber.Contains(options.Text) || x.Address.Contains(options.Text) || x.State.Contains(options.Text) || x.City.Contains(options.Text) || x.Country.Contains(options.Text) || x.ZipCode.Contains(options.Text));
                    }
                }
                sql = sql.Where<CustomerDto>(x => x.IsDeleted == false);
                // Order the redirects
                sql = sql.OrderByDescending<CustomerDto>(x => x.DateModified);

                // Make the call to the database
                CustomerDto[] all = scope.Database.Fetch<CustomerDto>(sql).ToArray();

                // Calculate variables used for the pagination
                int limit = options.Limit;
                int pages = (int)Math.Ceiling(all.Length / (double)limit);
                int page = Math.Max(1, Math.Min(options.Page, pages));
                int offset = (page * limit) - limit;

                // Apply pagination and wrap the database rows
                ICustomer[] items = all
                    .Skip(offset)
                    .Take(limit)
                    .Select(x => (ICustomer)Customer.CreateFromDto(x))
                    .ToArray();

                // Return the items (on the requested page)
                result = new CustomerSearchResult(all.Length, limit, offset, page, pages, items);

                scope.Complete();

            }

            return result;
        }

        public ICustomer GetCustomerById(int customerId)
        {
            // Validate the input
            if (customerId == 0) throw new ArgumentException("CustomerId must have a value", nameof(customerId));

            CustomerDto dto;

            using (IScope scope = _scopeProvider.CreateScope())
            {

                var sql = scope.SqlContext.Sql()
                    .Select<CustomerDto>()
                    .From<CustomerDto>()
                    .Where<CustomerDto>(x => x.CustomerId == customerId);

                dto = scope.Database.FirstOrDefault<CustomerDto>(sql);
                scope.Complete();

            }

            return dto == null ? null : new Customer(dto);
        }

        public ICustomer UpdateCustomer(ICustomer customer)
        {
            // Some input validation
            if (customer == null) throw new ArgumentNullException(nameof(customer));

            // This implementation only supports the "Redirect class"
            if (customer is not Customer c) throw new ArgumentException($"Customer type is not supported: {customer.GetType()}", nameof(customer));

            // Check whether another redirect matches the new URL and query string
            ICustomer existing = GetCustomerById(customer.CustomerId);
            if (existing != null && existing.CustomerId != customer.CustomerId)
            {

            }
            c.Dto.DateModified = DateTime.UtcNow;
            c.Dto.DateCreated = existing.DateCreated.ToUniversalTime();

            // Update the redirect in the database
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

            return customer;
        }
    }
}
