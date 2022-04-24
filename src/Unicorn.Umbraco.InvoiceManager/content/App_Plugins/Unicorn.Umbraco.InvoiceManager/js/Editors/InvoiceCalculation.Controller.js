angular.module("umbraco").controller("InvoiceManager.Editors.InvoiceCalculation.Controller", function ($scope) {
	const vm = this;
	if (!$scope.model.value) {
		$scope.model.value.gst = 15;
		$scope.model.value.taxableamount = 0;
		$scope.model.value.totaltax = 0;
		$scope.model.value.totalamount = 0
		$scope.model.value.quantity = 0;
		$scope.model.value.unitprice = 0;
    }
	$scope.change = function () {
		$scope.model.value.taxableamount = ($scope.model.value.quantity * $scope.model.value.unitprice);
		$scope.model.value.totaltax = ($scope.model.value.taxableamount * $scope.model.value.gst) / 100;
		$scope.model.value.totalamount = $scope.model.value.taxableamount + $scope.model.value.totaltax;
    }
});