﻿angular.module("umbraco").controller("InvoiceManager.Editors.Timestamp.Controller", function ($scope) {

    const vm = this;

    if (!$scope.model.value) return;

    vm.date = new Date($scope.model.value);
    vm.from = moment(vm.date).fromNow();

});