using Invoice_Manager.Enums;
using Invoice_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Queries
{
    public class GetInvoiceQuery:IQuery<InvoiceSearchResultQuery>
    {
        public string Text { get; set; }
        public InvoiceStatus Status{ get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
