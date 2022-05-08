
(function () {
    'use strict';

    function invoice($scope, customerService, invoiceService, $http, $q, $timeout, overlayService, notificationsService, localizationService, eventsService) {
        const baseUrl = Umbraco.Sys.ServerVariables.madnat.invoicemanager.invoicebaseUrl;
        var vm = this;
        vm.types = [
            { name: "All", value: 2 },
            { name: "Draft", value: 0 },
            { name: "Created", value: 1 }
        ];
        vm.page = {
            title: 'Invoices',
            description: 'Create invoice of the customers'
        }
        vm.items = [];
        vm.usersOptions = {};
        vm.userSortData = [
            { label: "Name (A-Z)", key: "Name", direction: "Ascending" },
            { label: "Name (Z-A)", key: "Name", direction: "Descending" },
            { label: "Newest", key: "CreateDate", direction: "Descending" },
            { label: "Oldest", key: "CreateDate", direction: "Ascending" },
            { label: "Last login", key: "LastLoginDate", direction: "Descending" }
        ];
        vm.createInvoice = function () {
            invoiceService.createInvoice({

                callback: function () {
                    vm.updateList(1);
                }
            });
        };
        vm.refresh = refresh;
        function refresh() {
            console.log("clicked invoice's Reload button.")
        }
        vm.filters = {
            type: vm.types[0],
            text: ""
        };
        vm.activeFilters = 0;

        vm.loading = false;
        vm.editInvoice = function (invoice) {
            invoiceService.editInvoice(invoice, {
                rootNodes: vm.rootNodes,
                callback: function () {
                    vm.updateList();
                }
            });
        };

        vm.detailInvoice = function (invoice) {
            invoiceService.detailInvoice(invoice, {
                rootNodes: vm.rootNodes,
                callback: function () {
                    vm.updateList();
                }
            });
        };
        vm.download = function (invoice) {
            invoiceService.downloadInvoice(invoice.invoiceid);
        };

        vm.deleteInvoice = function (invoice) {
            invoiceService.requestDeleteInvoice({
                invoice: invoice,
                submit: function () {
                    vm.updateList();
                    overlayService.close();
                }
            });
        };
        vm.buttonGroups = [
            {
                alias: "add",
                buttonStyle: "outline",
                defaultButton: {
                    labelKey: "info_createInvoice",
                    handler: vm.createInvoice
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
                url: `${baseUrl}GetInvoices`,
                params: args
            });

            // Show the loader for at least 200 ms
            const timer = $timeout(function () { }, 200);

            // Wait for both the AJAX call and the timeout
            $q.all([http, timer]).then(function (array) {
                console.log("loader:" + vm.loading);
                vm.loading = false;
                vm.invoices = array[0].data.items;
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

                localizationService.localize("invoice_pagination", tokens).then(function (value) {
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
        .controller('invoicesController', invoice);
})();
