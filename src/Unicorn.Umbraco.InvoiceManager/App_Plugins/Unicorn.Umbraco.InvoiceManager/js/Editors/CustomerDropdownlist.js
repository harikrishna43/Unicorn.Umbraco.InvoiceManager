angular.module("umbraco").controller("InvoiceManager.Editors.CustomerDropdown.Controller", function ($scope, localizationService, customerService, editorState) {

    const vm = this;

    vm.current = editorState.getCurrent();
    vm.customers = [];
    customerService.getAllCustomers().then(function (r) {
        r.data.forEach(function (customer) {
            vm.customers.push(customer);
            if (vm.current) {
                $scope.model.value = customer;
            }
        });
        vm.loading = false;
    });

});