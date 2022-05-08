angular.module("umbraco.services").factory("invoiceService", function ($http, editorService, localizationService, notificationsService, overlayService, umbRequestHelper) {

    // Get the cache buster value
    const cacheBuster = Umbraco.Sys.ServerVariables.madnat.invoicemanager.cacheBuster;

    // Get the base URL for the API controller
    const baseUrl = Umbraco.Sys.ServerVariables.madnat.invoicemanager.invoicebaseUrl;

    const service = {

        createInvoice: function (options) {

            if (!options) options = {};
            if (typeof options === "function") options = { callback: options };

            const o = {
                size: "medium",
                view: `/App_Plugins/Unicorn.Umbraco.InvoiceManager/Dialogs/Invoice.html?v=${cacheBuster}`,
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

        editInvoice: function (invoice, options) {

            if (!options) options = {};
            if (typeof options === "function") options = { callback: options };

            editorService.open({
	            size: "medium",
                view: `/App_Plugins/Unicorn.Umbraco.InvoiceManager/Dialogs/Invoice.html?v=${cacheBuster}`,
                invoice: invoice,
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
        detailInvoice: function (invoice, options) {

            if (!options) options = {};
            if (typeof options === "function") options = { callback: options };

            editorService.open({
                size: "medium",
                view: `/App_Plugins/Unicorn.Umbraco.InvoiceManager/Dialogs/InvoiceDetails.html?v=${cacheBuster}`,
                invoice: invoice,
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

        deleteInvoice: function (invoice, success, failed) {
            $http({
                method: "GET",
                url: `${baseUrl}DeleteInvoice`,
                params: {
                    invoiceId: invoice.invoiceid
                }
            }).then(function () {
                notificationsService.success("Invoice deleted", "Your invoice was successfully deleted.");
                if (success) success(invoice);
            }, function (res) {
                notificationsService.error("Deleting invoice failed", res && res.data && res.data.meta ? res.data.meta.error : "The server was unable to delete your invoice.");
                if (failed) failed(customer);
            });
        },

        requestDeleteInvoice: function(options) {

            if (!options) options = {};
            if (!options.invoice) return;

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
                content: `Are you sure you want to delete the invoice at <strong>${options.invoice.invoicenumber}</strong>  created for <strong>${options.invoice.customer.name}</strong>?`,
                submit: function() {
    
                    // Update the button state in the UI
                    overlay.submitButtonState = "busy";
    
                    // Delete the invoice
                    service.deleteInvoice(options.invoice, function () {
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

            localizationService.localize("customer_overlayDeleteMessage", [options.invoice.invoicenumber, options.invoice.customer.name], overlay.content).then(function (value) {
                overlay.content = value;
            });
    
            // Open the overlay
            overlayService.confirmDelete(overlay);

        },
        downloadInvoice: function (id) {
            if (!id) {
                throw "id cannot be null";
            }

            var url = `${baseUrl}download?invoiceid=${id}`;

            return umbRequestHelper.downloadFile(url).then(function () {
                localizationService.localize("speechBubbles_documentTypeExportedSuccess").then(function (value) {
                    notificationsService.success("Invoice was downloaded to PDF file");
                });
            }, function (data) {
                localizationService.localize("speechBubbles_documentTypeExportedError").then(function (value) {
                    notificationsService.error("There are some server error to download invoice.");
                });
            });
        },

        propertiesToObject: function (array) {

            var result = {};

            array.forEach(function (p) {
                result[p.alias] = p.value === undefined ? null : p.value;
            });

            return result;

        }



    };

    

    return service;

});