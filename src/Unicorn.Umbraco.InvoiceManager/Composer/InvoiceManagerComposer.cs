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
            builder.Services.AddSingleton<CustomerServiceDependencies>();
            builder.Services.AddSingleton<InvoiceServiceDependencies>();
            builder.AddNotificationHandler<UmbracoApplicationStartingNotification, ExecuteMigration>();
            builder.Services.AddSingleton<InvoiceManagerBackOfficeHelperDependencies>();
            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesParsingHandler>();
            //customer
            builder.Services.AddSingleton<ICustomerService, CustomerService>();
            builder.Services.AddSingleton<ICustomer, Customer>();
            builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            builder.Services.AddSingleton<ICommand,AddCustomerCommand>();
            builder.Services.AddSingleton<ICommandHandler<AddCustomerCommand>, AddCustomerCommandHandler>();
            builder.Services.AddSingleton<ICommandHandler<EditCustomerCommand>, EditCustomerCommandHandler>();
            builder.Services.AddSingleton<IQueryHandler<GetCustomerQuery, CustomerSearchResultQuery>, GetCustomerHandler>();
            builder.Services.AddSingleton<IQueryHandler<GetAllCustomerQuery, ICustomer[]>, GetAllCustomerHandler>();
            builder.Services.AddSingleton<ICommandHandler<DeleteCustomerCommand>, DeleteCustomerCommandHandler>();
            //invoices
            builder.Services.AddSingleton<IInvoiceService, InvoiceServices>();
            builder.Services.AddSingleton<IInvoice, Invoice>();
            builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            builder.Services.AddSingleton<ICommand, CreateInvoiceCommand>();
            builder.Services.AddSingleton<ICommandHandler<CreateInvoiceCommand>, CreateInvoiceCommandHandler>();
            builder.Services.AddSingleton<ICommandHandler<EditInvoiceCommand>, EditInvoiceCommandHandler>();
            builder.Services.AddSingleton<IQueryHandler<GetInvoiceQuery, InvoiceSearchResultQuery>, GetInvoiceHandler>();
            builder.Services.AddSingleton<ICommandHandler<DeleteInvoiceCommand>, DeleteInvoiceCommandHandler>();
            builder.Services.AddSingleton<IQueryHandler<DownloadInvoiceQuery, Queries.Invoices.InvoiceData>, DownloadInvoiceHandler>();
            builder.Services.AddSingleton<InvoiceManagerBackOfficeHelper>();
            builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>().Add<CustomerMappingDefinition>();

        }
    }
}
