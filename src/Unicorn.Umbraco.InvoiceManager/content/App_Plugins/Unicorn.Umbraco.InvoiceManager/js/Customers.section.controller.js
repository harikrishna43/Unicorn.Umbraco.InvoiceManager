
(function () {
    'use strict';

    function customers($scope, customerService, $http, $q, $timeout, overlayService, notificationsService, localizationService, eventsService) {
        const baseUrl = Umbraco.Sys.ServerVariables.madnat.invoicemanager.customerbaseUrl;
        var vm = this;
        vm.types = [
            { name: "All", value: 2 },
            { name: "Individual", value:0 },
            { name: "Company", value: 1 }
        ];
        vm.page = {
            title: 'Customers',
            description: 'Create invoice of the customers',
        }
        vm.addCustomer = function () {
            customerService.addCustomer({
                callback: function () {
                    vm.updateList(1);
                }
            });
        };
        vm.reload = reload;
        vm.filters = {
            type: vm.types[0],
            text: ""
        };
        
        vm.activeFilters = 0;

        vm.loading = false;
        vm.editCustomer = function (customer) {
            customerService.editCustomer(customer, {
                callback: function () {
                    vm.updateList();
                }
            });
        };

        vm.deleteCustomer = function (customer) {
            customerService.requestDeleteCustomer({
                customer: customer,
                submit: function () {
                    vm.updateList();
                    overlayService.close();
                }
            });
        };

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
            }
        ];
        // Initial pagination options
        vm.pagination = {
            text: "",
            page: 1,
            pages: 0,
            limit: 20,
            offset: 0,
            pagination: []
        };

        // Loads the previous page
        vm.prev = function () {
            if (vm.pagination.page > 1) vm.updateList(vm.pagination.page - 1);
        };

        // Loads the next pages
        vm.next = function () {
            if (vm.pagination.page < vm.pagination.pages) vm.updateList(vm.pagination.page + 1);
        };
        

        vm.filterUpdated = function () {
            vm.updateList();
        };
        // Updates the list based on current arguments
        vm.updateList = function (page) {

            vm.loading = true;

            // If a page is specified, we load that page
            page = (page ? page : vm.pagination.page);

            // Declare the arguments (making up the query string) for the call to the API
            const args = {
                limit: vm.pagination.limit,
                page: page
            };

            vm.activeFilters = 0;

            // Any filters?
            if (vm.filters.rootNode && vm.filters.rootNode.key) {
                args.rootNodeKey = vm.filters.rootNode.key;
                vm.activeFilters++;
            }

            // Any filters?
            if (vm.filters.type.value !== 2) {
                args.type = vm.filters.type.value;
                vm.activeFilters++;
            }

            if (vm.filters.text) {
                args.text = vm.filters.text;
                vm.activeFilters++;
            }

            // Declare the HTTP options
            const http = $http({
                method: "GET",
                url: `${baseUrl}GetCustomers`,
                params: args
            });

            // Show the loader for at least 200 ms
            const timer = $timeout(function () { }, 200);

            // Wait for both the AJAX call and the timeout
            $q.all([http, timer]).then(function (array) {
                console.log("loader:" + vm.loading);
                vm.loading = false;
                vm.customers = array[0].data.items;
                console.log("loader:" + vm.loading);
                // Update our pagination model
                vm.pagination = array[0].data.pagination;
                vm.pagination.pagination = [];

                const from = Math.max(1, vm.pagination.page - 7);
                const to = Math.min(vm.pagination.pages, vm.pagination.page + 7);

                for (let i = from; i <= to; i++) {
                    vm.pagination.pagination.push({
                        page: i,
                        active: vm.pagination.page === i
                    });
                }

                const tokens = [
                    vm.pagination.from,
                    vm.pagination.to,
                    vm.pagination.total,
                    vm.pagination.page,
                    vm.pagination.pages
                ];

                localizationService.localize("customer_pagination", tokens).then(function (value) {
                    vm.pagination.text = value;
                });

            }, function () {
                notificationsService.error("Unable to load Customers", "The list of customer could not be loaded.");
                vm.loading.list = false;
            });
            
        };
        vm.updateList();


    };

    angular.module('umbraco')
        .controller('customersController', customers);
})();
