using Invoice_Manager.Interfaces;
using Invoice_Manager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Invoice_Manager.Queries.QueryHandler
{
    public class GetCustomerHandler : IQueryHandler<GetCustomerQuery,SearchResult>
    {
        private readonly ICustomerService _customerService;
        private readonly IUmbracoMapper _mapper;
        public GetCustomerHandler(ICustomerService customerService, IUmbracoMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public SearchResult Handle(GetCustomerQuery query)
        {
            var option = _mapper.Map<CustomerSearchOption>(query);
            var data = _customerService.GetCustomer(option);
            var searchResult=_mapper.Map<SearchResult>(data);
            return searchResult;
        }
    }
}
