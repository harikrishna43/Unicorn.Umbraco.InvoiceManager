using Invoice_Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Commands
{
    public class DeleteCustomerCommand:ICommand
    {
        public int CustomerId { get; set; }
    }
}
