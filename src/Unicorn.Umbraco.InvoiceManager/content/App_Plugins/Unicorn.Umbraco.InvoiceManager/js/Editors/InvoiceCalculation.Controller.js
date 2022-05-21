angular.module("umbraco").controller("InvoiceManager.Editors.InvoiceCalculation.Controller", function ($scope) {

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
    $scope.remove = function (item, evt) {
        evt.preventDefault();
        $scope.TotalTaxAmount -= item.TotalTax;
        $scope.TaxableAmount -= item.TaxableAmount;
        $scope.TotalAmount -= item.TotalAmount;
        $scope.model.value = _.reject($scope.model.value, function (x) {
            return x.note === item.note;
        });
        DataCalculation();
    };

    $scope.edit = function (item, evt) {
        evt.preventDefault();
        $scope.editMode = true;
        $scope.setFocus = false;

        $scope.newInvoiceItem = item;
        DataCalculation();
    };

    $scope.cancel = function (evt) {
        evt.preventDefault();
        $scope.editMode = false;
        $scope.setFocus = true;

        $scope.newInvoiceItem = null;
    };

    $scope.change = function () {
        // Listen to the change event and set focus 2 false
        if ($scope.setFocus) {
            $scope.setFocus = false;
            return;
        }
    };


    $scope.add = function (evt) {
        evt.preventDefault();

        $scope.editMode = false;
        $scope.setFocus = true;

        if ($scope.newInvoiceItem && $scope.newInvoiceItem.description &&
            Utilities.isNumber($scope.newInvoiceItem.unitprice) && Utilities.isNumber($scope.newInvoiceItem.quantity) &&
            $scope.newInvoiceItem.unitprice > 0 && $scope.newInvoiceItem.quantity > 0) {

            var exists = _.find($scope.model.value, function (item) { return $scope.newInvoiceItem.note === item.note; });

            $scope.newInvoiceItem.gst = 15;
            $scope.newInvoiceItem.TaxableAmount = $scope.newInvoiceItem.unitprice * $scope.newInvoiceItem.quantity;
            $scope.newInvoiceItem.TotalTax = ($scope.newInvoiceItem.TaxableAmount * $scope.newInvoiceItem.gst) / 100;
            $scope.newInvoiceItem.TotalAmount = $scope.newInvoiceItem.TotalTax + $scope.newInvoiceItem.TaxableAmount;

            if (!exists) {
                $scope.model.value.push($scope.newInvoiceItem);
                $scope.newInvoiceItem = {};
                $scope.hasError = false;
                $scope.cropAdded = false;
                DataCalculation();
                return;
            }
            else {
                $scope.newInvoiceItem = null;
                $scope.hasError = false;
                DataCalculation();
                return;
            }
            
        }

        //there was an error, do the highlight (will be set back by the directive)
        $scope.hasError = true;
    };
    $scope.createNew = function (event) {
        if (event.keyCode == 13) {
            $scope.add(event);
        }
    };

    $scope.sortableOptions = {
        axis: 'y',
        containment: 'parent',
        cursor: 'move',
        tolerance: 'pointer'
    };
    function DataCalculation() {
        $scope.TaxableAmount = 0;
        $scope.TotalAmount = 0;
        $scope.TotalTaxAmount = 0;
        if ($scope.model.value) {
            for (var i = 0; i < $scope.model.value.length; i++) {
                var item = $scope.model.value[i];
                $scope.TaxableAmount += item.TaxableAmount;
                $scope.TotalAmount += item.TotalAmount;
                $scope.TotalTaxAmount += item.TotalTax;
            }
        }
    }
});