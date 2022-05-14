angular.module("umbraco").controller("InvoiceManager.InvoiceDetailDialog.Controller", function ($scope, $http, editorService, notificationsService, customerService, invoiceService, localizationService, formHelper) {

    // Get the cache buster value
    const cacheBuster = Umbraco.Sys.ServerVariables.madnat.invoicemanager.cacheBuster;

    // Get the base URL for the API controller
    const baseUrl = Umbraco.Sys.ServerVariables.madnat.invoicemanager.invoicebaseUrl;

    const vm = this;

    vm.options = $scope.model.options || {};
    vm.invocedata =  {};
    vm.invocedata.gst = 15
    vm.invocedata.quantity= 0
    vm.invocedata.taxableamount= 0
    vm.invocedata.totalamount=0
    vm.invocedata.totaltax= 0
    vm.invocedata.unitprice=0
    $scope.model.submitButtonLabelKey = "customer_add";

    $scope.model.title = "Create new Invoice";
    localizationService.localize("invoice_createNewInvoice").then(function (value) { $scope.model.title = value; });


    $scope.model.hiddenProperties = [];

    if ($scope.model.invoice) {

        $scope.model.title = "Edit Invoice";
        localizationService.localize("invoice_editInvoice").then(function (value) { $scope.model.title = value; });

        $scope.model.submitButtonLabelKey = "invoice_save";

        $scope.model.hiddenProperties.push({
            alias: "invoiceid",
            value: $scope.model.invoice.invoiceid
        });
    }

    $scope.model.properties = [];
    $scope.model.advancedProperties = [];

    $scope.model.advancedProperties.push({
        alias: "invoiceid",
        label: "Invoice Id",
        labelKey: "invoice_propertyInvoiceId",
        view: `readonlyvalue`,
        value: $scope.model.invoice && $scope.model.invoice? $scope.model.invoice.invoiceid:0,
        validation: {
            mandatory: true
        }
    });

    $scope.model.advancedProperties.push({
        alias: "status",
        label: "Invoice status",
        labelKey: "invoice_propertyInvoiceStatus",
        description: "Invoice status",
        descriptionKey: "invoice_propertyInvoiceStatusDescription",
        view: `readonlyvalue`,
        value: $scope.model.invoice && $scope.model.invoice ? $scope.model.invoice.status==0?"Draft":"Created":""
    });

    $scope.model.advancedProperties.push({
        alias: "invoicedate",
        label: "Invoice Date",
        labelKey: "invoice_propertyInvoiceDate",
        description: "Specify invoice date",
        descriptionKey: "invoice_propertyInvoiceDateDescription",
        view: `readonlyvalue`,
        value: $scope.model.invoice && $scope.model.invoice.invoicedate ? $scope.model.invoice.invoicedate : "",
        hello: moment(new Date($scope.model.invoice.invoicedate)).fromNow(),
        readonly: true
    });

    $scope.model.advancedProperties.push({
        alias: "duedate",
        label: "Due Date",
        labelKey: "invoice_propertyDueDate",
        description: "Invoice due date",
        descriptionKey: "invoice_propertyDueDateDescription",
        value: $scope.model.invoice && $scope.model.invoice.duedate ? $scope.model.invoice.duedate : "",
        view: `readonlyvalue`,
        hello: moment(new Date($scope.model.invoice.duedate)).fromNow(),
        readonly: true
    });
    
    $scope.model.advancedProperties.push({
        alias: "invoicedata",
        label: "Invoice Details (Included GST)",
        labelKey: "invoice_propertyTotalAmount",
        view: `/App_Plugins/Unicorn.Umbraco.InvoiceManager/Editors/InvoiceDetails.html?v=${cacheBuster}`,
        value: $scope.model.invoice && $scope.model.invoice.invoicedata ? $scope.model.invoice.invoicedata : null,
        validation: {
            mandatory: true,
        }
    });

    $scope.model.infoProperties = [];
    if ($scope.model.invoice && $scope.model.invoice.invoiceid) {
        $scope.model.infoProperties = [
            {
                alias: "invoicenumber",
                label: "Invoice Number",
                labelKey: "invoice_propertyInvoiceNumber",
                view: `readonlyvalue`,
                value: $scope.model.invoice ? $scope.model.invoice.invoicenumber : null,
                readonly: true
            },
            {
                alias: "name",
                label: "Customer name",
                labelKey: "invoice_propertyName",
                view: `readonlyvalue`,
                value: $scope.model.invoice ? $scope.model.invoice.customer.name : null,
                readonly: true
            },
            {
                alias: "phone",
                label: "Phone",
                labelKey: "invoice_propertyPhone",
                view: `readonlyvalue`,
                value: $scope.model.invoice ? $scope.model.invoice.customer.phone : null,
                readonly: true
            },
            {
                alias: "updateDate",
                label: "Updated Date",
                labelKey: "invoice_propertyUpdateDate",
                view: `readonlyvalue`,
                value: $scope.model.invoice ? $scope.model.invoice.customer.datemodified : null,
                hello: moment(new Date($scope.model.invoice.customer.datemodified)).fromNow(),
                readonly: true
            }
        ];
    };
    const allProperties = $scope.model.properties.concat($scope.model.advancedProperties, $scope.model.hiddenProperties);

    allProperties.concat($scope.model.infoProperties).forEach(function (p) {

        // Localize the label
        if (p.labelKey) {
            localizationService.localize(p.labelKey).then(function (value) {
                if (!value.length || value[0] === "[") return;
                p.label = value;
            });
        }
        
        // Localize the description
        if (p.descriptionKey) {
            localizationService.localize(p.descriptionKey).then(function (value) {
                if (!value.length || value[0] === "[") return;
                p.description = value;
            });
        }
        
        // Localize any config options
        if (p.config && p.config.options) {
            p.config.options.forEach(function (o) {
                if (!o.labelKey) return;
                localizationService.localize(o.labelKey).then(function (value) {
                    if (!value.length || value[0] === "[") return;
                    o.label = value;
                });
            });
        }

    });

    vm.settingsApp = {
        alias: "invoice",
        name: "Invoice",
        icon: "icon-bill",
        view: "nope",
        active: true
    };

    vm.infoApp = {
        alias: "info",
        name: "Customer",
        view: "nope",
        icon: "icon-info"
    };

    $scope.model.navigation = $scope.model.invoice && $scope.model.invoice.invoiceid ? [vm.settingsApp, vm.infoApp] : [];

    function initLabels() {

        vm.labels = {
            addSuccessfulTitle: "Invoice created",
            addSuccessfulMessage: "Your invoice has successfully been created.",
            addFailedTitle: "Saving failed",
            addFailedMessage: "The invoice could not be created due to an error on the server.",
            saveSuccessfulTitle: "Invoice added",
            saveSuccessfulMessage: "Your invoice has successfully been created.",
            saveFailedTitle: "Saving failed",
            saveFailedMessage: "The invoice could not be saved due to an error on the server."
        };

        angular.forEach(vm.labels, function (_, key) {
            localizationService.localize(`invoice_${key}`).then(function (value) {
                if (!value.length || value[0] === "[") return;
                vm.labels[key] = value;
            });
        });

        localizationService.localize("invoice_settingsApp").then(function (value) {
            if (!value.length || value[0] === "[") return;
            vm.settingsApp.name = value;
        });
        
        localizationService.localize("invoice_infoApp").then(function (value) {
            if (!value.length || value[0] === "[") return;
            vm.infoApp.name = value;
        });

    }

    initLabels();

    vm.save = function () {

        // Map the properties back to an object we can send to the API
        const invoice = invoiceService.propertiesToObject(allProperties);

        // Attempt to submit the form (Angular validation will kick in)
        if (!formHelper.submitForm({ scope: $scope })) return;

        // Reset the Angular form
        formHelper.resetForm({ scope: $scope });

        // Make sure we set a loading state
        vm.loading = true;

        if (invoice.invoiceid) {
            $http({
                method: "POST",
                url: `${baseUrl}EditInvoice`,
                params: {
                    invoiceid: invoice.invoiceid
                },
                data: invoice
            }).then(function (r) {
                vm.loading = false;
                notificationsService.success(vm.labels.saveSuccessfulTitle, vm.labels.saveSuccessfulMessage);
                $scope.model.submit(r);
            }, function (res) {
                vm.loading = false;
                notificationsService.error(vm.labels.saveFailedTitle, res && res.data && res.data.meta ? res.data.meta.error : vm.labels.saveFailedMessage);
            });
        } else {
            $http({
                method: "POST",
                url: `${baseUrl}CreateInvoice`,
                data: invoice
            }).then(function (r) {
                vm.loading = false;
                notificationsService.success(vm.labels.addSuccessfulTitle, vm.labels.addSuccessfulMessage);
                $scope.model.submit(r);
            }, function (res) {
                vm.loading = false;
                notificationsService.error(vm.labels.addFailedTitle, res && res.data && res.data.meta ? res.data.meta.error : vm.labels.addFailedMessage);
            });
        }

    };

    vm.close = function () {
        if ($scope.model.close) {
            $scope.model.close();
        } else {
            editorService.close();
        }
    };

});