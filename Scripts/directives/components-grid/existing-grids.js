(function () {
    'use strict';
    angular.module('appPDU').directive('existingGrids', ['ContainerModels', function (ContainerModels) {
        return {
            restrict: 'A', 
            require: "^componentGrid",
            templateUrl: 'scripts/directives/components-grid/existing-grids.html',
            scope: {},
            controller: ['$scope', function ($scope) {
                this.addGrid = function () {
                    ContainerModels.createContainer($scope.parentCtrl.getPageId(),'container', 0, function (id) {
                        $scope.parentCtrl.gridId = id;
                        $scope.parentCtrl.showEditor()
                    }, function (childId, parent, metadata) {
                        metadata.template = childId;
                    });
                };
                this.selectGrid = function (id) {
                    $scope.parentCtrl.selectGrid(id);
                }
                this.close = function () {
                    $scope.parentCtrl.close();
                }
            }],
            controllerAs: 'exitCtrl',
            link: function (scope, element, attrs, parentCtrl) {
                scope.parentCtrl = parentCtrl;
            }
        }
    }]);
})();