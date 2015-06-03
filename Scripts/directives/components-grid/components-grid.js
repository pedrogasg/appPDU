(function () {
    'use strict';
    angular.module('appPDU').directive('componentGrid', [function () {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-grid/component-grid.html',
            scope: {},
            controller: ['$scope', function ($scope) {
                this.showEditor = function () {

                }
            }],
            controllerAs: 'ctrl',
            link: function (scope, element, attrs, mainCtrl) {
            }
        }
    }]);
})();