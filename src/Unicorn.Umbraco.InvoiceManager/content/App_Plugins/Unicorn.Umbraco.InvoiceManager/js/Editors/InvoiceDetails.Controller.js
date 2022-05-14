angular.module("umbraco").controller("InvoiceManager.Editors.InvoiceDetails.Controller", function ($scope) {

    if (!$scope.model.value) {
        $scope.model.value = [];
    }
    console.log($scope.model.value)
    $scope.editMode = false;
    $scope.setFocus = false;
    $scope.TaxableAmount = 0.0;
    $scope.TotalAmount = 0.0;
    $scope.TotalTaxAmount = 0.0;
    if ($scope.model.value) {
        for (var i = 0; i < $scope.model.value.length; i++) {
            var item = $scope.model.value[i];
            $scope.TaxableAmount += item.TaxableAmount;
            $scope.TotalAmount += item.TotalAmount;
            $scope.TotalTaxAmount += item.TotalTax;
        }
    }
});