using Unicorn.Umbraco.InvoiceManager.Enums;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Queries
{
    public class GetInvoiceQuery:IQuery<InvoiceSearchResultQuery>
    {
        public string Text { get; set; }
        public InvoiceStatus Status{ get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
