using Invoice_Manager.Interfaces;
using Invoice_Manager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Services
{
    public class InvoiceServices : IInvoiceService
    {
        public IInvoice CreateInvoice(CreateInvoiceOption option)
        {
            throw new NotImplementedException();
        }

        public List<IInvoice> GetAll()
        {
            throw new NotImplementedException();
        }

        public IInvoice UpdateInvoice(IInvoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
