using Invoice_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Queries.Invoices
{
    public class DownloadInvoiceQuery: IQuery<InvoiceData>
    {
        public int InvoiceId { get; set; }
    }
}
