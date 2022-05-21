using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using Unicorn.Umbraco.InvoiceManager.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Unicorn.Umbraco.InvoiceManager.Queries.QueryHandler
{
    public class IsCustomerExistsHandler : IQueryHandler<IsCustomerExistsQuery, bool>
    {
        private readonly ICustomerService _customerService;
        private readonly IUmbracoMapper _mapper;
        public IsCustomerExistsHandler(ICustomerService customerService, IUmbracoMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public bool Handle(IsCustomerExistsQuery query)
        {
            return  _customerService.IsCustomerExists(query.PhoneNumber);
        }
    }
}
