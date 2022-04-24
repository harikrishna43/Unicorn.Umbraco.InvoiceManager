using Unicorn.Umbraco.InvoiceManager.Enums;
using J2N.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Models.Options
{
    public class InvoiceSearchOption
    {
        
        public string Text { get; set; }
        public InvoiceStatus Status { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }

       public InvoiceSearchOption()
        {
            Page = 1;
            Limit = 20;
        }
    }
}
