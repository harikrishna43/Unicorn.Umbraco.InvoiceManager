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
    public class GetCustomerHandler : IQueryHandler<GetCustomerQuery, CustomerSearchResultQuery>
    {
        private readonly ICustomerService _customerService;
        private readonly IUmbracoMapper _mapper;
        public GetCustomerHandler(ICustomerService customerService, IUmbracoMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public CustomerSearchResultQuery Handle(GetCustomerQuery query)
        {
            var option = _mapper.Map<CustomerSearchOption>(query);
            var data = _customerService.GetCustomer(option);
            var searchResult=_mapper.Map<CustomerSearchResultQuery>(data);
            return searchResult;
        }
    }
}
