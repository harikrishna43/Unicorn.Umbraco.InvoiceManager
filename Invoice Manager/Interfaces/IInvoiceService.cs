using Invoice_Manager.Models;
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
        IInvoice CreateInvoice(CreateInvoiceOption option);
        IInvoice UpdateInvoice(IInvoice invoice);
        List<IInvoice> GetAll();    
    }
}
