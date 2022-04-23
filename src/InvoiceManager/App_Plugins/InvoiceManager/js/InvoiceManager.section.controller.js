
(function () {
    'use strict';

    function invoiceManagerSection($scope) {

        var vm = this;

        vm.page = {
            title: 'Invoice Manager',
            description: 'v1.0.0-beta',
            navigation: [
                {
                    'name': 'Info',
                    'alias': 'default',
                    'icon': 'icon-sprout',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/InvoiceManager/Dashboards/info.html',
                    'active': true
                },
                {
                    'name': 'Customers',
                    'alias': 'customers',
                    'icon': 'icon-users',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/InvoiceManager/Customers/customers.html'
                },
                {
                    'name': 'Invoices',
                    'alias': 'invoices',
                    'icon': 'icon-bill',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/InvoiceManager/Invoice/Invoice.html'
                }
            ]
        }

    };

    angular.module('umbraco')
        .controller('invoiceManagerSectionController', invoiceManagerSection);
})();
