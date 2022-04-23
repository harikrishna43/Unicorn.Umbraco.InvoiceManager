using Invoice_Manager.Models;
using Invoice_Manager.Models.Dtos;
using Invoice_Manager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Interfaces
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
