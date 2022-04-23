﻿using Invoice_Manager.Commands;
using Invoice_Manager.Commands.Invoices;
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
        }

        #region Customer mapper
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
        #endregion

        #region Invoice Mapper
        private void InvoiceMap(CreateInvoiceOption source, CreateInvoiceCommand target, MapperContext context)
        {
            target.InvoiceNote = source.InvoiceNote;
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.Description = source.Description;
            target.UnitPrice=source.InvoiceData.UnitPrice;
            target.Quantity = source.InvoiceData.Quantity;
            target.Status = source.Status;
            target.CustomerId = source.CustomerId;
            target.GST = source.InvoiceData.GST;
            target.InvoiceId = source.InvoiceId;
        }

        private void InvoiceMap(CreateInvoiceCommand source, CreateInvoiceOption target, MapperContext context)
        {
            target.InvoiceNote = source.InvoiceNote;
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.Description = source.Description;
            target.UnitPrice = source.UnitPrice;
            target.Quantity = source.Quantity;
            target.Status = source.Status;
            target.CustomerId = source.CustomerId;
            target.GST = source.GST;
            target.InvoiceId = source.InvoiceId;
        }

        private void InvoiceMap(CreateInvoiceCommand source, InvoiceDto target, MapperContext context)
        {
            target.InvoiceNote = source.InvoiceNote;
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.Description = source.Description;
            target.UnitPrice = source.UnitPrice;
            target.Quantity = source.Quantity;
            target.Status = source.Status;
            target.CustomerId = source.CustomerId;
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
            target.UnitPrice = source.InvoiceData.UnitPrice;
            target.Quantity = source.InvoiceData.Quantity;
            target.Status = source.Status;
            target.CustomerId = source.Customer.CustomerId;
            target.GST = source.GST;
            target.InvoiceId = source.InvoiceId;
            target.Customer = source.Customer;
        }
        private void InvoiceMap(EditInvoiceCommand source, IInvoice target, MapperContext context)
        {
            target.InvoiceNote = source.InvoiceNote;
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.Description = source.Description;
            target.UnitPrice = source.UnitPrice;
            target.Quantity = source.Quantity;
            target.Status = source.Status;
            target.CustomerId = source.Customer.CustomerId;
            target.GST = source.GST;
            target.InvoiceId = source.InvoiceId;
            //target.Customer = source.Customer;
        }
        private void InvoiceMap(EditInvoiceCommand source, Invoice target, MapperContext context)
        {
            target.InvoiceNote = source.InvoiceNote;
            target.InvoiceDate = source.InvoiceDate;
            target.DueDate = source.DueDate;
            target.Description = source.Description;
            target.UnitPrice = source.UnitPrice;
            target.Quantity = source.Quantity;
            target.Status = source.Status;
            target.CustomerId = source.Customer.CustomerId;
            target.GST = source.GST;
            target.InvoiceId = source.InvoiceId;
            //target.Customer = source.Customer;
        }
        #endregion
    }
}