using Invoice_Manager.Commands;
using Invoice_Manager.Interfaces;
using Invoice_Manager.Models;
using Invoice_Manager.Models.Dtos;
using Invoice_Manager.Models.Options;
using Invoice_Manager.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Invoice_Manager.Mappers
{
    public class CustomerMappingDefinition : IMapDefinition
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<AddCustomerOption, AddCustomerCommand>((source, context) => new AddCustomerCommand(), CustomerMap);
            mapper.Define<AddCustomerCommand, AddCustomerOption>((source, context) => new AddCustomerOption(), CustomerMap);
            mapper.Define<AddCustomerCommand, CustomerDto>((source, context) => new CustomerDto(), CustomerMap);
            mapper.Define<CustomerSearchResult, SearchResult>((source, context) => new SearchResult(), CustomerMap);
            mapper.Define<GetCustomerQuery, CustomerSearchOption>((source, context) => new CustomerSearchOption(), CustomerMap);
            mapper.Define<EditCustomerOption, EditCustomerCommand>((source, context) => new EditCustomerCommand(), CustomerMap);
            mapper.Define<EditCustomerCommand, ICustomer>((source, context) => new Customer(), CustomerMap);
        }

        private void CustomerMap(AddCustomerOption source, AddCustomerCommand target, MapperContext context)
        {
            target.Name = source.Name;
            target.GSTNumber = source.GSTNumber;
            target.State=source.State;
            target.Address = source.Address;
            target.City = source.City;
            target.CustomerType = source.CustomerType;
            target.ZipCode = source.ZipCode;
            target.Country = source.Country;
        }

        private void CustomerMap(EditCustomerOption source, EditCustomerCommand target, MapperContext context)
        {
            target.Name = source.Name;
            target.Id = source.Id;
            target.GSTNumber = source.GSTNumber;
            target.State = source.State;
            target.Address = source.Address;
            target.City = source.City;
            target.CustomerType = source.CustomerType;
            target.ZipCode = source.ZipCode;
            target.Country = source.Country;
        }

        private void CustomerMap(EditCustomerCommand source, ICustomer target, MapperContext context)
        {
            target.Name = source.Name;
            target.CustomerId = source.Id;
            target.GSTNumber = source.GSTNumber;
            target.State = source.State;
            target.Address = source.Address;
            target.City = source.City;
            target.CustomerType = source.CustomerType;
            target.ZipCode = source.ZipCode;
            target.Country = source.Country;
        }

        private void CustomerMap(AddCustomerCommand source, AddCustomerOption target, MapperContext context)
        {
            target.Name = source.Name;
            target.GSTNumber = source.GSTNumber;
            target.State = source.State;
            target.Address = source.Address;
            target.City = source.City;
            target.CustomerType = source.CustomerType;
            target.ZipCode = source.ZipCode;
            target.Country = source.Country;
        }
        private void CustomerMap(AddCustomerCommand source, CustomerDto target, MapperContext context)
        {
            target.Name = source.Name;
            target.GSTNumber = source.GSTNumber;
            target.State = source.State;
            target.Address = source.Address;
            target.City = source.City;
            target.CustomerType = source.CustomerType;
            target.ZipCode = source.ZipCode;
            target.Country = source.Country;
        }
        private void CustomerMap(CustomerSearchResult source, SearchResult target, MapperContext context)
        {
            target.Items= source.Items;
            target.Pagination = source.Pagination;
        }
        private void CustomerMap(GetCustomerQuery source, CustomerSearchOption target, MapperContext context)
        {
            target.Text = source.Text;
            target.Page = source.Page;
            target.Limit = source.Limit;
            target.Type = source.CustomerType;
        }
    }
}
