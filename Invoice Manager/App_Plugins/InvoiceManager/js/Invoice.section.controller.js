
(function () {
    'use strict';

    function invoice($scope) {

        var vm = this;

        vm.page = {
            title: 'Invoices',
            description: 'Create invoice of the customers'
        }
        vm.items = [];



        vm.createInvoice = createInvoice;
        vm.refresh = refresh;
        function createInvoice() {
            console.log("clicked add new Invoice button.")
        }
        function refresh() {
            console.log("clicked invoice's Reload button.")
        }
        vm.buttonGroups = [
            {
                alias: "add",
                buttonStyle: "outline",
                defaultButton: {
                    labelKey: "info_createInvoice",
                    handler: vm.createInvoice
                },
                subButtons: []
            },
            {
                alias: "refresh",
                defaultButton: {
                    labelKey: "info_refresh",
                    handler: vm.refresh
                },
                subButtons: []
            }
        ];
    };

    angular.module('umbraco')
        .controller('invoicesController', invoice);
})();
