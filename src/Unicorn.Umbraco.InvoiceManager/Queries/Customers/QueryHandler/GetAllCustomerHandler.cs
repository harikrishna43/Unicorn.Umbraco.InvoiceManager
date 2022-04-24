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
    public class GetAllCustomerHandler : IQueryHandler<GetAllCustomerQuery, ICustomer[]>
    {
        private readonly ICustomerService _customerService;
        private readonly IUmbracoMapper _mapper;
        public GetAllCustomerHandler(ICustomerService customerService, IUmbracoMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public InvoiceSearchResultQuery Handle(GetCustomerQuery query)
        {
            var option = _mapper.Map<CustomerSearchOption>(query);
            var data = _customerService.GetCustomer(option);
            var searchResult=_mapper.Map<InvoiceSearchResultQuery>(data);
            return searchResult;
        }

        public ICustomer[] Handle(GetAllCustomerQuery query)
        {
            var data = _customerService.GetAll();
            return data.ToArray();
        }
    }
}
