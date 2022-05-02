using Unicorn.Umbraco.InvoiceManager.Helpers;
using System.Collections.Generic;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

#pragma warning disable 1591

namespace Unicorn.Umbraco.InvoiceManager.Notifications.Handlers {
    
    public class ServerVariablesParsingHandler : INotificationHandler<ServerVariablesParsingNotification> {
        
        private readonly InvoiceManagerBackOfficeHelper _backoffice;

        public ServerVariablesParsingHandler(InvoiceManagerBackOfficeHelper backoffice) {
            _backoffice = backoffice;
        }
        
        public void Handle(ServerVariablesParsingNotification notification) {

            if (!(notification.ServerVariables.TryGetValue("madnat", out object value) && value is Dictionary<string, object> invoicemanager))  {
                notification.ServerVariables["madnat"] = invoicemanager = new Dictionary<string, object>();
            }

            invoicemanager.Add("invoicemanager", _backoffice.GetServerVariables());

        }

    }

}