using Unicorn.Umbraco.InvoiceManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Commands
{
    public class EditCustomerCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CustomerType { get; set; }

        public string GSTNumber {get; set;}

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Phone { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
    }
}
