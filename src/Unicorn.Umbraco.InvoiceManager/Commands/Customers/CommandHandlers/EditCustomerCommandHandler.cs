using Unicorn.Umbraco.InvoiceManager.Commands;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Unicorn.Umbraco.InvoiceManager.Commands.CommandHandlers
{
    public class EditCustomerCommandHandler : ICommandHandler<EditCustomerCommand>
    {
        private readonly ICustomerService _customerService;
        private readonly IUmbracoMapper _mapper;
        public EditCustomerCommandHandler(ICustomerService customerService, IUmbracoMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public void Handle(EditCustomerCommand command)
{
            var option = _mapper.Map<ICustomer>(command);
            var data=_customerService.UpdateCustomer(option);
        }
    }
}
