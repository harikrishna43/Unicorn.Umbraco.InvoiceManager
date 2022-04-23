angular.module("umbraco").controller("InvoiceManager.CustomerDialog.Controller", function ($scope, $http, editorService, notificationsService, customerService, localizationService, formHelper) {

    // Get the cache buster value
    const cacheBuster = Umbraco.Sys.ServerVariables.madnat.invoicemanager.cacheBuster;

    // Get the base URL for the API controller
    const baseUrl = Umbraco.Sys.ServerVariables.madnat.invoicemanager.customerbaseUrl;

    const vm = this;

    vm.options = $scope.model.options || {};

    $scope.model.submitButtonLabelKey = "customer_add";

    $scope.model.title = "Add new Customer";
    localizationService.localize("customer_addNewCustomer").then(function (value) { $scope.model.title = value; });


    $scope.model.hiddenProperties = [];

    if ($scope.model.customer) {

        $scope.model.title = "Edit Customer";
        localizationService.localize("customer_editCustomer").then(function (value) { $scope.model.title = value; });

        $scope.model.submitButtonLabelKey = "customer_save";


        $scope.model.hiddenProperties.push({
            alias: "id",
            value: $scope.model.customer.id
        });

    }

    $scope.model.properties = [];

    $scope.model.properties.push({
        alias: "name",
        label: "Name",
        labelKey: "customer_propertyName",
        description: "Add Customer name",
        descriptionKey: "customer_propertyNameDescription",
        view: "textbox",
        value: $scope.model.customer && $scope.model.customer.name ? $scope.model.customer.name : "",
        validation: {
            mandatory: true
        }
    });

    $scope.model.properties.push({
        alias: "phone",
        label: "Phone",
        labelKey: "customer_propertyPhone",
        description: "Add Customer phone number",
        descriptionKey: "customer_propertyPhoneDescription",
        view: "textbox",
        value: $scope.model.customer && $scope.model.customer.phone ? $scope.model.customer.phone: "",
        validation: {
            mandatory: true
        }
    });

    $scope.model.properties.push({
        alias: "customertype",
        label: "Customer Type",
        labelKey: "customer_propertyCustomerType",
        description: "Specify the Customer type",
        descriptionKey: "customer_propertyCustomerTypeDescription",
        view: `/App_Plugins/InvoiceManager/Editors/RadioGroup.html?v=${cacheBuster}`,
        value: $scope.model.customer && $scope.model.customer.customertype ? $scope.model.customer.customertype: 0,
        config: {
            options: [
                {
                    label: "Individial",
                    labelKey: "customer_labelIndividial",
                    value: 0
                },
                {
                    label: "Company",
                    labelKey: "customer_labelCompany",
                    value: 1
                }
            ]
        },
        validation: {
            mandatory: true
        }
    });

    $scope.model.properties.push({
        alias: "gstnumber",
        label: "GST Number",
        labelKey: "customer_propertyGSTNumber",
        description: "GST Number",
        descriptionKey: "customer_propertyGSTNumberDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.gstnumber ? $scope.model.customer.gstnumber : "",
        validation: {
            mandatory: true
        }
    });

    $scope.model.properties.push({
        alias: "address",
        label: "Address",
        labelKey: "customer_propertyAddress",
        description: "Add customer address",
        descriptionKey: "customer_propertyAddressDescription",
        view: `textarea`,
        value: $scope.model.customer && $scope.model.customer.address ? $scope.model.customer.address : "",
        validation: {
            mandatory: true
        }
    });
    $scope.model.properties.push({
        alias: "city",
        label: "City",
        labelKey: "customer_propertyCity",
        description: "Add customer City",
        descriptionKey: "customer_propertyCityDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.city ? $scope.model.customer.city : "",
        validation: {
            mandatory: true
        }
    });
    $scope.model.properties.push({
        alias: "state",
        label: "State",
        labelKey: "customer_propertyState",
        description: "Add customer State",
        descriptionKey: "customer_propertyStateDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.state ? $scope.model.customer.state : "",
        validation: {
            mandatory: true
        }
    });
    
    $scope.model.properties.push({
        alias: "country",
        label: "Country",
        labelKey: "customer_propertyCountry",
        description: "Add customer Country",
        descriptionKey: "customer_propertyCountryDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.country ? $scope.model.customer.country : "",
        validation: {
            mandatory: true
        }
    });

    $scope.model.properties.push({
        alias: "zipcode",
        label: "Postal Code",
        labelKey: "customer_propertyZipCode",
        description: "Add customer ZipCode",
        descriptionKey: "customer_propertyZipCodeDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.zipcode ? $scope.model.customer.zipcode : "",
        validation: {
            mandatory: true
        }
    });//


    $scope.model.infoProperties = [];

    const allProperties = $scope.model.properties.concat($scope.model.hiddenProperties);

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
        alias: "settings",
        name: "Settings",
        icon: "icon-equalizer",
        view: "nope",
        active: true
    };

    vm.infoApp = {
        alias: "info",
        name: "Info",
        view: "nope",
        icon: "icon-info"
    };

    $scope.model.navigation = $scope.model.cusotmer && $scope.model.customer.id ? [vm.settingsApp, vm.infoApp] : [];

    function initLabels() {

        vm.labels = {
            addSuccessfulTitle: "Customer added",
            addSuccessfulMessage: "Your customer has successfully been added.",
            addFailedTitle: "Saving failed",
            addFailedMessage: "The customer could not be added due to an error on the server.",
            saveSuccessfulTitle: "Customer added",
            saveSuccessfulMessage: "Your customer has successfully been added.",
            saveFailedTitle: "Saving failed",
            saveFailedMessage: "The customer could not be saved due to an error on the server."
        };

        angular.forEach(vm.labels, function (_, key) {
            localizationService.localize(`customer_${key}`).then(function (value) {
                if (!value.length || value[0] === "[") return;
                vm.labels[key] = value;
            });
        });

        localizationService.localize("customer_settingsApp").then(function (value) {
            if (!value.length || value[0] === "[") return;
            vm.settingsApp.name = value;
        });
        
        localizationService.localize("customer_infoApp").then(function (value) {
            if (!value.length || value[0] === "[") return;
            vm.infoApp.name = value;
        });

    }

    initLabels();

    vm.save = function () {

        // Map the properties back to an object we can send to the API
        const customer = customerService.propertiesToObject(allProperties);

        // Attempt to submit the form (Angular validation will kick in)
        if (!formHelper.submitForm({ scope: $scope })) return;

        // Reset the Angular form
        formHelper.resetForm({ scope: $scope });

        // Make sure we set a loading state
        vm.loading = true;

        if (customer.id) {
            $http({
                method: "POST",
                url: `${baseUrl}EditCustomer`,
                params: {
                    customerid: customer.customerid
                },
                data: customer
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
                url: `${baseUrl}Addcustomer`,
                data: customer
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