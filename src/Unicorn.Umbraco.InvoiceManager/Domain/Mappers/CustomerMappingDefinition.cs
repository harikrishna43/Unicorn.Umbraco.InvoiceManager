using Unicorn.Umbraco.InvoiceManager.Commands;
using Unicorn.Umbraco.InvoiceManager.Commands.Invoices;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Models;
using Unicorn.Umbraco.InvoiceManager.Models.Dtos;
using Unicorn.Umbraco.InvoiceManager.Models.Options;
using Unicorn.Umbraco.InvoiceManager.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Mapping;

namespace Unicorn.Umbraco.InvoiceManager.Mappers
{
    public class CustomerMappingDefinition : IMapDefinition
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            //customer
            mapper.Define<AddCustomerOption, AddCustomerCommand>((source, context) => new AddCustomerCommand(), CustomerMap);
            mapper.Define<AddCustomerCommand, AddCustomerOption>((source, context) => new AddCustomerOption(), CustomerMap);
            mapper.Define<AddCustomerCommand, CustomerDto>((source, context) => new CustomerDto(), CustomerMap);
            mapper.Define<CustomerSearchResult, CustomerSearchResultQuery>((source, context) => new CustomerSearchResultQuery(), CustomerMap);
            mapper.Define<GetCustomerQuery, CustomerSearchOption>((source, context) => new CustomerSearchOption(), CustomerMap);
            mapper.Define<EditCustomerOption, EditCustomerCommand>((source, context) => new EditCustomerCommand(), CustomerMap);
            mapper.Define<EditCustomerCommand, ICustomer>((source, context) => new Customer(), CustomerMap);
            //Invoices
            mapper.Define<CreateInvoiceOption, CreateInvoiceCommand>((source, context) => new CreateInvoiceCommand(), InvoiceMap);
            mapper.Define<CreateInvoiceCommand, CreateInvoiceOption>((source, context) => new CreateInvoiceOption(), InvoiceMap);
            mapper.Define<CreateInvoiceCommand, InvoiceDto>((source, context) => new InvoiceDto(), InvoiceMap);
            mapper.Define<InvoiceSearchResult, InvoiceSearchResultQuery>((source, context) => new InvoiceSearchResultQuery(), InvoiceMap);
            mapper.Define<GetInvoiceQuery, InvoiceSearchOption>((source, context) => new InvoiceSearchOption(), InvoiceMap);
            mapper.Define<EditInvoiceOption, EditInvoiceCommand>((source, context) => new EditInvoiceCommand(), InvoiceMap);
            mapper.Define<EditInvoiceCommand, IInvoice>((source, context) => new Invoice(), InvoiceMap);
            mapper.Define<EditInvoiceCommand, Invoice>((source, context) => new Invoice(), InvoiceMap);
            mapper.Define<InvoiceData, InvoiceItemsDto>((source, context) => new InvoiceItemsDto(), InvoiceMap);
            mapper.Define<InvoiceData, IInvoiceData>((source, context) => new InvoiceData(), InvoiceMap);
        }

        #region Customer mapper
        private void CustomerMap(AddCustomerOption source, AddCustomerCommand target, MapperContext context)
        {
            target.Name = source.Name;
            target.GSTNumber = source.GSTNumber;
            target.State = source.State;
            target.Address = source.Address;
            target.City = source.City;
            target.CustomerType = source.CustomerType;
            target.ZipCode = source.ZipCode;
            target.Country = source.Country;
            target.Phone = source.Phone;
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
            target.Phone = source.Phone;
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
            target.Phone = source.Phone;
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
            target.Phone = source.Phone;
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
            target.Phone = source.Phone;
        }
        private void CustomerMap(CustomerSearchResult source, CustomerSearchResultQuery target, MapperContext context)
        {
            target.Items = source.Items;
            target.Pagination = source.Pagination;
        }
        private void CustomerMap(GetCustomerQuery source, CustomerSearchOption target, MapperContext context)
        {
            target.Text = source.Text;
            target.Page = source.Page;
            target.Limit = source.Limit;
            target.Type = source.CustomerType;
        }
        #endregion

        #region Invoice Mapper
        private void InvoiceMap(CreateInvoiceOption source, CreateInvoiceCommand target, MapperContext context)
        {

            target.InvoiceDate = source.InvoiceDate;
            target.PaymentStatus = source.PaymentStatus;
            target.InvoiceNote = source.InvoiceNote;
            target.DueDate = source.DueDate;
            target.Status = source.Status;
            target.CustomerId = source.CustomerId;
            target.InvoiceId = source.InvoiceId;
            target.InvoiceData = source.InvoiceData;
        }

        private void InvoiceMap(CreateInvoiceCommand source, CreateInvoiceOption target, MapperContext context)
        {
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.Status = source.Status;
            target.PaymentStatus = source.PaymentStatus;
            target.InvoiceNote = source.InvoiceNote;
            target.CustomerId = source.CustomerId;
            target.InvoiceId = source.InvoiceId;
            target.InvoiceData = source.InvoiceData;
            target.InvoiceNote = source.InvoiceNote;
        }

        private void InvoiceMap(CreateInvoiceCommand source, InvoiceDto target, MapperContext context)
        {
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.PaymentStatus = source.PaymentStatus;
            target.InvoiceNote = source.InvoiceNote;
            target.Status = source.Status;
            target.CustomerId = source.CustomerId;
            target.Id = source.InvoiceId;
            target.InvoiceItems = context.Map<List<InvoiceData>, List<InvoiceItemsDto>>(source.InvoiceData);
        }
        private void InvoiceMap(InvoiceData source, InvoiceItemsDto target, MapperContext context)
        {
            
            target.Description = source.Description;
            target.UnitPrice = source.UnitPrice;
            target.Quantity = source.Quantity;
            target.Id = source.Id;
            target.GST = source.GST;
            target.InvoiceId = source.InvoiceId;

        }
        private void InvoiceMap(InvoiceSearchResult source, InvoiceSearchResultQuery target, MapperContext context)
        {
            target.Items = source.Items;
            target.Pagination = source.Pagination;
        }
        private void InvoiceMap(GetInvoiceQuery source, InvoiceSearchOption target, MapperContext context)
        {
            target.Limit = source.Limit;
            target.Page = source.Page;
            target.Status = source.Status;
            target.Text = source.Text;

        }
        private void InvoiceMap(EditInvoiceOption source, EditInvoiceCommand target, MapperContext context)
        {
            target.InvoiceNote = source.InvoiceNote;
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.Description = source.Description;
            target.Status = source.Status;
            target.CustomerId = source.Customer.CustomerId;
            target.GST = source.GST;
            target.InvoiceId = source.InvoiceId;
            target.Customer = source.Customer;
            target.PaymentStatus = source.PaymentStatus;
            target.InvoiceData = source.InvoiceData;
        }
        private void InvoiceMap(EditInvoiceCommand source, IInvoice target, MapperContext context)
        {
            target.InvoiceDate = source.InvoiceDate;
            target.PaymentStatus = source.PaymentStatus;
            target.InvoiceNote = source.InvoiceNote;
            target.DueDate = source.DueDate;
            target.Status = source.Status;
            target.CustomerId = source.Customer.CustomerId;
            target.InvoiceId = source.InvoiceId;
            target.PaymentStatus = source.PaymentStatus;
            target.InvoiceData = context.Map<List<InvoiceData>, List<IInvoiceData>>(source.InvoiceData);
        }

        private void InvoiceMap(EditInvoiceCommand source, Invoice target, MapperContext context)
        {
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.Status = source.Status;
            target.CustomerId = source.Customer.CustomerId;
            target.InvoiceId = source.InvoiceId;
            target.InvoiceNote= source.InvoiceNote;
            target.PaymentStatus = source.PaymentStatus;
            target.InvoiceData = context.Map<List<InvoiceData>, List<IInvoiceData>>(source.InvoiceData);
        }

        private void InvoiceMap(Unicorn.Umbraco.InvoiceManager.Models.InvoiceData source, Unicorn.Umbraco.InvoiceManager.Interfaces.IInvoiceData target, MapperContext context)
        {
            target.Description = source.Description;
            target.UnitPrice = source.UnitPrice;
            target.Quantity = source.Quantity;
            target.Id = source.Id;
            target.GST = source.GST;
            target.InvoiceId=source.InvoiceId;
            target.DateCreated = source.DateCreated;
            target.DateModified = source.DateModified;
        }
        #endregion
    }
}
