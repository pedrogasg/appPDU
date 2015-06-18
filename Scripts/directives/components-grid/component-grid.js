(function () {
    'use strict';
    angular.module('appPDU').directive('componentGrid', ['ContainerModels', function (ContainerModels) {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-grid/component-grid.html',
            scope: {
                validateData: '&validateData'
            },
            controller: ['$scope', function ($scope) {
                
                $scope.editorIsVisible = false;
                this.gridId = null;
                this.hideEditor = function () {
                    toogleEditor(false);
                }
                this.showEditor = function () {
                    toogleEditor(true);
                }
                this.getPageId = function () {
                    return $scope.pageId;
                }
                this.selectGrid = function (id) {
                    ContainerModels.saveTemplate($scope.pageId, id)
                }
                function toogleEditor(predicate) {
                    $scope.editorIsVisible = predicate;
                }
                this.close = function () {
                    $scope.$parent.close();
                }
                
            }],
            controllerAs: 'gridCtrl',
            link: function (scope, element, attrs) {
                scope.pageId = attrs.componentGrid;
            }
        }
    }]);
})();