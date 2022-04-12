
(function () {
    'use strict';

    function customers($scope, customerService) {

        var vm = this;
        vm.rootNodes = [
            { name: "All sites", key: "" },
            { name: "Global redirects", key: "00000000-0000-0000-0000-000000000000" }
        ];
        vm.page = {
            title: 'Customers',
            description: 'Create invoice of the customers',
        }
        vm.addCustomer = function () {
            customerService.addCustomer({
                rootNodes: vm.rootNodes,
                callback: function () {
                    vm.updateList(1);
                }
            });
        };
        vm.reload = reload;
        
        function reload() {
            console.log("clicked Reload button.")
        }
        vm.buttonGroups = [
            {
                alias: "add",
                buttonStyle: "outline",
                defaultButton: {
                    labelKey: "info_addNewCustomer",
                    handler: vm.addCustomer
                },
                subButtons: []
            },
            {
                alias: "refresh",
                defaultButton: {
                    labelKey: "info_refresh",
                    handler: vm.reload
                },
                subButtons: []
            }
        ];
    };

    

    angular.module('umbraco')
        .controller('customersController', customers);
})();
