using Invoice_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Commands.Invoices
{
    public class DeleteInvoiceCommand:ICommand
    {
        public int InvoiceId { get; set; }
    }
}
