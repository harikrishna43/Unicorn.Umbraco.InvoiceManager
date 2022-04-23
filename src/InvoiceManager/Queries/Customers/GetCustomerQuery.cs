using Invoice_Manager.Enums;
using Invoice_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Queries
{
    public class GetCustomerQuery:IQuery<CustomerSearchResultQuery>
    {
        public string Text { get; set; }
        public CustomerType CustomerType { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
