using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models;
using Unicorn.Umbraco.InvoiceManager.Models.Dtos;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using Unicorn.Umbraco.InvoiceManager.Services;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Unicorn.Umbraco.InvoiceManager.Commands.Invoices.Handlers
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
