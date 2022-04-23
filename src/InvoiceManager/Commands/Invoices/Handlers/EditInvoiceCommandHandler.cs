using Invoice_Manager.Interfaces;
using Invoice_Manager.Models;
using Invoice_Manager.Models.Options;
using Invoice_Manager.Services;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Invoice_Manager.Commands.Invoices.Handlers
{
    public class EditInvoiceCommandHandler : ICommandHandler<EditInvoiceCommand>
{
        private readonly IInvoiceService _invoiceService;
        private readonly IUmbracoMapper _mapper;
        public EditInvoiceCommandHandler(IInvoiceService invoiceService, IUmbracoMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }
        public void Handle(EditInvoiceCommand command)
{
            var option = _mapper.Map<Invoice>(command);
            var data = _invoiceService.UpdateInvoice(option);
        }
    }
}
