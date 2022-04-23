using Invoice_Manager.Commands;
using Invoice_Manager.Commands.Invoices;
using Invoice_Manager.Interfaces;
using Invoice_Manager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Invoice_Manager.Commands.CommandHandlers
{
    public class DeleteInvoiceCommandHandler : ICommandHandler<DeleteInvoiceCommand>
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IUmbracoMapper _mapper;
        public DeleteInvoiceCommandHandler(IInvoiceService invoiceService, IUmbracoMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }
        public void Handle(DeleteInvoiceCommand command)
        {
            var customer = _invoiceService.GetInvoiceById(command.InvoiceId);
            _invoiceService.DeleteInvoice(customer);
        }
    }
}
