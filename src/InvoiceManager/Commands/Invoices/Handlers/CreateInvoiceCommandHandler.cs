using Invoice_Manager.Interfaces;
using Invoice_Manager.Models;
using Invoice_Manager.Models.Dtos;
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
    public class CreateInvoiceCommandHandler : ICommandHandler<CreateInvoiceCommand>
{
        private readonly IInvoiceService _invoiceService;
        private readonly IUmbracoMapper _mapper;
        public CreateInvoiceCommandHandler(IInvoiceService invoiceService, IUmbracoMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }
        public void Handle(CreateInvoiceCommand command)
{
            var option = _mapper.Map<InvoiceDto>(command);
            var data = _invoiceService.CreateInvoice(option);
        }
    }
}
