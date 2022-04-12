using Invoice_Manager.Helpers;
using System.Collections.Generic;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

#pragma warning disable 1591

namespace Invoice_Manager.Notifications.Handlers {
    
    public class ServerVariablesParsingHandler : INotificationHandler<ServerVariablesParsingNotification> {
        
        private readonly InvoiceManagerBackOfficeHelper _backoffice;

        public ServerVariablesParsingHandler(InvoiceManagerBackOfficeHelper backoffice) {
            _backoffice = backoffice;
        }
        
        public void Handle(ServerVariablesParsingNotification notification) {
            
            // Get or create the "skybrud" dictionary
            if (!(notification.ServerVariables.TryGetValue("madnat", out object value) && value is Dictionary<string, object> skybrud))  {
                notification.ServerVariables["madnat"] = skybrud = new Dictionary<string, object>();
            }

            // Append the "redirects" dictionary to "skybrud"
            skybrud.Add("invoicemanager", _backoffice.GetServerVariables());

        }

    }

}