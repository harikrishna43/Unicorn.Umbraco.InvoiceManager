angular.module("umbraco.services").factory("customerService", function ($http, editorService, localizationService, notificationsService, overlayService) {

    // Get the cache buster value
    const cacheBuster = Umbraco.Sys.ServerVariables.madnat.invoicemanager.cacheBuster;

    // Get the base URL for the API controller
    const baseUrl = Umbraco.Sys.ServerVariables.madnat.invoicemanager.customerbaseUrl;

    const service = {

        addCustomer: function (options) {

            if (!options) options = {};
            if (typeof options === "function") options = { callback: options };

            const o = {
                size: "medium",
                view: `/App_Plugins/Unicorn.Umbraco.InvoiceManager/Dialogs/Customer.html?v=${cacheBuster}`,
                options: options,
                submit: function(value) {
                    if (options.callback) options.callback(value);
                    editorService.close();
                },
                close: function() {
                    editorService.close();
                }
            };

            if (options.destination) o.destination = options.destination;

            editorService.open(o);

        },

        editCustomer: function (customer, options) {

            if (!options) options = {};
            if (typeof options === "function") options = { callback: options };

            editorService.open({
	            size: "medium",
                view: `/App_Plugins/Unicorn.Umbraco.InvoiceManager/Dialogs/Customer.html?v=${cacheBuster}`,
                customer: customer,
                options: options,
                submit: function (value) {
                    if (options.callback) options.callback(value);
                    editorService.close();
                },
                close: function () {
                    editorService.close();
                }
            });

        },

        deleteCustomer: function (customer, success, failed) {
            $http({
                method: "GET",
                url: `${baseUrl}DeleteCustomer`,
                params: {
                    customerId: customer.id
                }
            }).then(function () {
                notificationsService.success("Customer deleted", "Your customer was successfully deleted.");
                if (success) success(customer);
            }, function (res) {
                notificationsService.error("Deleting customer failed", res && res.data && res.data.meta ? res.data.meta.error : "The server was unable to delete your customer.");
                if (failed) failed(customer);
            });
        },

        requestDeleteCustomer: function(options) {

            if (!options) options = {};
            if (!options.customer) return;

            if (typeof options.submit !== "function") {
                options.submit = function() {
                    overlayService.close();
                };
            }

            if (typeof options.close !== "function") {
                options.close = function() {
                    overlayService.close();
                };
            }

            const overlay = {
                title: "Confirm delete",
                content: `Are you sure you want to delete the customer at <strong>${options.customer.name}</strong> ?`,
                submit: function() {
    
                    // Update the button state in the UI
                    overlay.submitButtonState = "busy";
    
                    // Delete the customer
                    service.deleteCustomer(options.customer, function () {
                        if (typeof options.submit === "function") {
                            options.submit(overlay);
                        } else {
                            overlayService.close();
                        }
                    }, function () {
                        overlay.submitButtonState = "error";
                    });
    
                },
                close: function() {
                    options.close(overlay);
                }
            };

            localizationService.localize("customer_overlayDeleteTitle", null, overlay.title).then(function (value) {
                overlay.title = value;
            });

            localizationService.localize("customer_overlayDeleteMessage", [options.customer.name], overlay.content).then(function (value) {
                overlay.content = value;
            });
    
            // Open the overlay
            overlayService.confirmDelete(overlay);

        },

        propertiesToObject: function (array) {

            var result = {};

            array.forEach(function (p) {
                result[p.alias] = p.value === undefined ? null : p.value;
            });

            return result;

        }

    };

    service.getAllCustomers = function () {
        return $http.get(`${baseUrl}GetAllCustomers`);
    };

    return service;

});