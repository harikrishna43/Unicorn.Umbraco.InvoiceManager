
(function () {
    'use strict';

    function invoiceManagerSection($scope) {

        var vm = this;

        vm.page = {
            title: 'Invoice Manager',
            navigation: [
                {
                    'name': 'Info',
                    'alias': 'default',
                    'icon': 'icon-sprout',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/Unicorn.Umbraco.InvoiceManager/Dashboards/info.html',
                    'active': true
                },
                {
                    'name': 'Customers',
                    'alias': 'customers',
                    'icon': 'icon-users',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/Unicorn.Umbraco.InvoiceManager/Customers/customers.html'
                },
                {
                    'name': 'Invoices',
                    'alias': 'invoices',
                    'icon': 'icon-bill',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/Unicorn.Umbraco.InvoiceManager/Invoice/Invoice.html'
                }
            ]
        }

    };

    angular.module('umbraco')
        .controller('invoiceManagerSectionController', invoiceManagerSection);
})();
