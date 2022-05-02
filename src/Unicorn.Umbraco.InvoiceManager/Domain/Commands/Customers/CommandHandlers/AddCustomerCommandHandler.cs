using Unicorn.Umbraco.InvoiceManager.Commands;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Unicorn.Umbraco.InvoiceManager.Commands.CommandHandlers
{
    public class AddCustomerCommandHandler : ICommandHandler<AddCustomerCommand>
    {
        private readonly ICustomerService _customerService;
        private readonly IUmbracoMapper _mapper;
        public AddCustomerCommandHandler(ICustomerService customerService, IUmbracoMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public void Handle(AddCustomerCommand command)
        {
            var option = _mapper.Map<AddCustomerOption>(command);
            var data=_customerService.AddCustomer(option);
        }
    }
}
