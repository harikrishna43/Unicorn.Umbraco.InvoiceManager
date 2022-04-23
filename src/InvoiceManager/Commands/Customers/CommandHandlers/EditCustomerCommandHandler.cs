using Invoice_Manager.Commands;
using Invoice_Manager.Interfaces;
using Invoice_Manager.Models;
using Invoice_Manager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Invoice_Manager.Commands.CommandHandlers
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
