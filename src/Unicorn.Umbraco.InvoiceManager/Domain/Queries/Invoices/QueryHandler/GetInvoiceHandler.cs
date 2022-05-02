using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Unicorn.Umbraco.InvoiceManager.Queries.QueryHandler
{
    public class GetInvoiceHandler : IQueryHandler<GetInvoiceQuery, InvoiceSearchResultQuery>
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IUmbracoMapper _mapper;
        public GetInvoiceHandler(IInvoiceService invoiceService, IUmbracoMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }
        public InvoiceSearchResultQuery Handle(GetInvoiceQuery query)
        {
            var option = _mapper.Map<InvoiceSearchOption>(query);
            var data = _invoiceService.GetInvoices(option);
            var searchResult=_mapper.Map<InvoiceSearchResultQuery>(data);
            return searchResult;
        }
    }
}
