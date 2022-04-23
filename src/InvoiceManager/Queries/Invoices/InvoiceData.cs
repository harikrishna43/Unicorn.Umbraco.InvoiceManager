using Invoice_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Queries.Invoices
{
    public class InvoiceData
    {
        public IInvoice Invoice { get; set; }
        public string InvoiceHtmlString { get; set; }
    }
}
