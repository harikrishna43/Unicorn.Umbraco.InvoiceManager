using Unicorn.Umbraco.InvoiceManager.Models;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Umbraco.InvoiceManager.Interfaces
{
    public interface ICustomerService
    {
        ICustomer AddCustomer(AddCustomerOption option);
        ICustomer UpdateCustomer(ICustomer customer);
        CustomerSearchResult GetCustomer(CustomerSearchOption option);
        IEnumerable<ICustomer> GetAll();
        ICustomer GetCustomerById(int id);
        void DeleteCustomer(ICustomer customer);
        bool IsCustomerExists(string phonenumber);
    }
}
