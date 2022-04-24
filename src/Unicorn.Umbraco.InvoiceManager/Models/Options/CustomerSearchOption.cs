using Unicorn.Umbraco.InvoiceManager.Enums;
using J2N.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Models.Options
{
    public class CustomerSearchOption
    {
        public CustomerSearchOption()
        {
            Page = 1;
            Limit = 20;
        }
        public string Text { get; set; }
        public CustomerType Type { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
