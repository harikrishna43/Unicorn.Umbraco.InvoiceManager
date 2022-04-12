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

    if ($scope.model.redirect) {

        $scope.model.title = "Edit redirect";
        localizationService.localize("customer_editCustomer").then(function (value) { $scope.model.title = value; });

        $scope.model.submitButtonLabelKey = "customer_save";

        //destionation = $scope.model.redirect.destination;

        $scope.model.hiddenProperties.push({
            alias: "id",
            value: $scope.model.customer.customerid
        });

        //$scope.model.hiddenProperties.push({
        //    alias: "key",
        //    value: $scope.model.redirect.key
        //});

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
    });

    $scope.model.properties.push({
        alias: "address",
        label: "Address",
        labelKey: "customer_propertyAddress",
        description: "Add customer address",
        descriptionKey: "customer_propertyAddressDescription",
        view: `textarea`,
        value: $scope.model.customer && $scope.model.customer.address ? $scope.model.customer.address : "",
    });

    $scope.model.properties.push({
        alias: "state",
        label: "State",
        labelKey: "customer_propertyState",
        description: "Add customer State",
        descriptionKey: "customer_propertyStateDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.state ? $scope.model.customer.state : "",
    });
    $scope.model.properties.push({
        alias: "country",
        label: "Country",
        labelKey: "customer_propertyCountry",
        description: "Add customer Country",
        descriptionKey: "customer_propertyCountryDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.country ? $scope.model.customer.country : "",
    });
    $scope.model.properties.push({
        alias: "country",
        label: "Country",
        labelKey: "customer_propertyCountry",
        description: "Add customer Country",
        descriptionKey: "customer_propertyCountryDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.country ? $scope.model.customer.country : "",
    });

    $scope.model.properties.push({
        alias: "zipCode",
        label: "ZipCode",
        labelKey: "customer_propertyZipCode",
        description: "Add customer ZipCode",
        descriptionKey: "customer_propertyZipCodeDescription",
        view: `textbox`,
        value: $scope.model.customer && $scope.model.customer.zipCode ? $scope.model.customer.zipCode : "",
    });//


    $scope.model.infoProperties = [];

    // We only wish to initialize/show the info properties when we have a redirect (eg. when editing, not adding)
    //if ($scope.model.redirect && $scope.model.redirect.id) {
    //    $scope.model.infoProperties = [
    //        {
    //            alias: "customerid",
    //            label: "ID",
    //            labelKey: "redirects_customerId",
    //            view: `label`,
    //            value: $scope.model.redirect ? $scope.model.redirect.id : null,
    //            readonly: true
    //        },
    //        {
    //            alias: "key",
    //            label: "Key",
    //            labelKey: "redirects_propertyKey",
    //            view: `/App_Plugins/Skybrud.Umbraco.Redirects/Views/Editors/Code.html?v=${cacheBuster}`,
    //            value: $scope.model.redirect ? $scope.model.redirect.key : null,
    //            readonly: true
    //        },
    //        {
    //            alias: "createDate",
    //            label: "Created Date",
    //            labelKey: "redirects_propertyCreateDate",
    //            view: `/App_Plugins/Skybrud.Umbraco.Redirects/Views/Editors/Timestamp.html?v=${cacheBuster}`,
    //            value: $scope.model.redirect ? $scope.model.redirect.createDate : null,
    //            hello: moment(new Date($scope.model.redirect.updateDate)).fromNow(),
    //            readonly: true
    //        },
    //        {
    //            alias: "updateDate",
    //            label: "Updated Date",
    //            labelKey: "redirects_propertyUpdateDate",
    //            view: `/App_Plugins/Skybrud.Umbraco.Redirects/Views/Editors/Timestamp.html?v=${cacheBuster}`,
    //            value: $scope.model.redirect ? $scope.model.redirect.updateDate : null,
    //            hello: moment(new Date($scope.model.redirect.updateDate)).fromNow(),
    //            readonly: true
    //        }
    //    ];
    //};

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

    $scope.model.navigation = $scope.model.redirect && $scope.model.redirect.id ? [vm.settingsApp, vm.infoApp] : [];

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
        const redirect = customerService.propertiesToObject(allProperties);

        // Attempt to submit the form (Angular validation will kick in)
        if (!formHelper.submitForm({ scope: $scope })) return;

        // Reset the Angular form
        formHelper.resetForm({ scope: $scope });

        // Make sure we set a loading state
        vm.loading = true;

        // Make sure we set the "rootNodeKey" property as well
        if (redirect.rootNodeId > 0) {
            const rootNode = vm.options.rootNodes.find(x => x.id === redirect.rootNodeId);
            redirect.rootNodeKey = rootNode ? rootNode.key : "00000000-0000-0000-0000-000000000000";
        } else {
            redirect.rootNodeKey = "00000000-0000-0000-0000-000000000000";
        }

        if (redirect.key) {
            $http({
                method: "POST",
                url: `${baseUrl}EditCustomer`,
                params: {
                    redirectId: redirect.key
                },
                data: redirect
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
                data: redirect
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