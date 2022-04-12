using Invoice_Manager.Models;
using Invoice_Manager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_Manager.Interfaces
{
    public interface ICustomerService
    {
        ICustomer AddCustomer(AddCustomerOption option);
        ICustomer UpdateCustomer(ICustomer customer);
        CustomerSearchResult GetCustomer(CustomerSearchOption option);
        IEnumerable<ICustomer> GetAll();
        ICustomer GetCustomerById(int id);
    }
}
