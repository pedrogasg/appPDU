(function () {
    'use strict';
    angular.module('appPDU').directive('existingGrids', [function () {
        return {
            restrict: 'A', 
            require: "^componentGrid",
            templateUrl: 'scripts/directives/components-grid/existing-grids.html',
            scope: {},
            controller: ['$scope', function ($scope) {
                this.addGrid = function () {
                    $scope.parentCtrl.showEditor()
                };
                this.selectGrid = function (id) {
                    $scope.parentCtrl.selectGrid(id);
                }
            }],
            controllerAs: 'exitCtrl',
            link: function (scope, element, attrs, parentCtrl) {
                scope.parentCtrl = parentCtrl;
            }
        }
    }]);
})();