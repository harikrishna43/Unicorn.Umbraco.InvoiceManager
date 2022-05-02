using Unicorn.Umbraco.InvoiceManager.Models;
using Unicorn.Umbraco.InvoiceManager.Models.Dtos;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Interfaces
{
    public interface IInvoiceService
    {
        IInvoice CreateInvoice(InvoiceDto option);

        InvoiceSearchResult GetInvoices(InvoiceSearchOption option);

        IInvoice UpdateInvoice(IInvoice customer);
        
        IEnumerable<IInvoice> GetAll();
        
        IInvoice GetInvoiceById(int id);
        
        void DeleteInvoice(IInvoice customer);
    }
}
