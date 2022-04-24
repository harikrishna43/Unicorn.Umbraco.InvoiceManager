using Unicorn.Umbraco.InvoiceManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Commands
{
    public class DeleteCustomerCommand:ICommand
    {
        public int CustomerId { get; set; }
    }
}
