using Invoice_Manager.Helpers;
using Invoice_Manager.Interfaces;
using Invoice_Manager.Migrations;
using Invoice_Manager.Notifications.Handlers;
using Invoice_Manager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
//using Skybrud.Umbraco.Redirects.ContentApps;
//using Skybrud.Umbraco.Redirects.Factories.References;
//using Skybrud.Umbraco.Redirects.Helpers;
//using Skybrud.Umbraco.Redirects.Middleware;
//using Skybrud.Umbraco.Redirects.Notifications.Handlers;
//using Skybrud.Umbraco.Redirects.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.Extensions;
namespace Invoice_Manager.Composer
{
    public class InvoiceManagerComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddUnique<CustomerServiceDependencies>();
            builder.AddNotificationHandler<UmbracoApplicationStartingNotification, ExecuteMigration>();
            builder.Services.AddUnique<InvoiceManagerBackOfficeHelperDependencies>();
            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesParsingHandler>();
            builder.Services.AddUnique<ICustomerService, CustomerService>();
            builder.Services.AddUnique<InvoiceManagerBackOfficeHelper>();
        }
    }
}
