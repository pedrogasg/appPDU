(function () {
    'use strict';
    angular.module('appPDU').directive('componentGrid', [function () {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-grid/component-grid.html',
            scope: {
                validateData: '&validateData'
            },
            controller: ['$scope', function ($scope) {
                
                $scope.editorIsVisible = false;
                this.hideEditor = function () {
                    toogleEditor(false);
                }
                this.showEditor = function () {
                    toogleEditor(true);
                }
                this.selectGrid = function (id) {
                    $scope.$parent.validateData('test');
                }
                function toogleEditor(predicate) {
                    $scope.editorIsVisible = predicate;
                }
            }],
            controllerAs: 'gridCtrl',
            link: function (scope, element, attrs) {
            }
        }
    }]);
})();