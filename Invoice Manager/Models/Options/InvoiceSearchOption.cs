using Invoice_Manager.Enums;
using J2N.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Models.Options
{
    public class InvoiceSearchOption
    {
        
        public string Text { get; set; }
        public InvoiceStatus Status { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }

        InvoiceSearchOption()
        {
            Page = 1;
            Limit = 20;
        }
    }
}
