using Unicorn.Umbraco.InvoiceManager.Commands;
using Unicorn.Umbraco.InvoiceManager.Commands.CommandHandlers;
using Unicorn.Umbraco.InvoiceManager.Commands.Invoices;
using Unicorn.Umbraco.InvoiceManager.Commands.Invoices.Handlers;
using Unicorn.Umbraco.InvoiceManager.Helpers;
using Unicorn.Umbraco.InvoiceManager.Interfaces;
using Unicorn.Umbraco.InvoiceManager.Mappers;
using Unicorn.Umbraco.InvoiceManager.Migrations;
using Unicorn.Umbraco.InvoiceManager.Models;
using Unicorn.Umbraco.InvoiceManager.Notifications.Handlers;
using Unicorn.Umbraco.InvoiceManager.Queries;
using Unicorn.Umbraco.InvoiceManager.Queries.Invoice.QueryHandler;
using Unicorn.Umbraco.InvoiceManager.Queries.Invoices;
using Unicorn.Umbraco.InvoiceManager.Queries.QueryHandler;
using Unicorn.Umbraco.InvoiceManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.Extensions;
namespace Unicorn.Umbraco.InvoiceManager.Composer
{
    public class InvoiceManagerComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddUnique<CustomerServiceDependencies>();
            builder.Services.AddUnique<InvoiceServiceDependencies>();
            builder.AddNotificationHandler<UmbracoApplicationStartingNotification, ExecuteMigration>();
            builder.Services.AddUnique<InvoiceManagerBackOfficeHelperDependencies>();
            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesParsingHandler>();
            //customer
            builder.Services.AddUnique<ICustomerService, CustomerService>();
            builder.Services.AddUnique<ICustomer, Customer>();
            builder.Services.AddUnique<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddUnique<IQueryDispatcher, QueryDispatcher>();
            builder.Services.AddUnique<ICommand,AddCustomerCommand>();
            builder.Services.AddUnique<ICommandHandler<AddCustomerCommand>, AddCustomerCommandHandler>();
            builder.Services.AddUnique<ICommandHandler<EditCustomerCommand>, EditCustomerCommandHandler>();
            builder.Services.AddUnique<IQueryHandler<GetCustomerQuery, CustomerSearchResultQuery>, GetCustomerHandler>();
            builder.Services.AddUnique<IQueryHandler<GetAllCustomerQuery, ICustomer[]>, GetAllCustomerHandler>();
            builder.Services.AddUnique<ICommandHandler<DeleteCustomerCommand>, DeleteCustomerCommandHandler>();
            //invoices
            builder.Services.AddUnique<IInvoiceService, InvoiceServices>();
            builder.Services.AddUnique<IInvoice, Invoice>();
            builder.Services.AddUnique<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddUnique<IQueryDispatcher, QueryDispatcher>();
            builder.Services.AddUnique<ICommand, CreateInvoiceCommand>();
            builder.Services.AddUnique<ICommandHandler<CreateInvoiceCommand>, CreateInvoiceCommandHandler>();
            builder.Services.AddUnique<ICommandHandler<EditInvoiceCommand>, EditInvoiceCommandHandler>();
            builder.Services.AddUnique<IQueryHandler<GetInvoiceQuery, InvoiceSearchResultQuery>, GetInvoiceHandler>();
            builder.Services.AddUnique<ICommandHandler<DeleteInvoiceCommand>, DeleteInvoiceCommandHandler>();
            builder.Services.AddUnique<IQueryHandler<DownloadInvoiceQuery, Queries.Invoices.InvoiceData>, DownloadInvoiceHandler>();
            builder.Services.AddUnique<InvoiceManagerBackOfficeHelper>();
            builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>().Add<CustomerMappingDefinition>();

        }
    }
}
