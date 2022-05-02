using Unicorn.Umbraco.InvoiceManager.Enums;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Queries
{
    public class GetCustomerQuery:IQuery<CustomerSearchResultQuery>
    {
        public string Text { get; set; }
        public CustomerType CustomerType { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
