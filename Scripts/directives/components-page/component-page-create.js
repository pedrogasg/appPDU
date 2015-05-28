(function () {
    'use strict';
    angular.module('appPDU').directive('componentPageCreate', [function () {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-page/component-page-create.html',
            scope: {},
            controller: ['$scope', function ($scope) {
                this.startCreation = function () {
                    $scope.creationStarted = true;
                }
                this.submit = function (form,model) {

                }
                this.changeTitle = function (page) {
                    page.name = page.title;
                }
            }],
            controllerAs:'ctrl',
            link: function (scope, element, attrs, ctrl) {

            }
        }
    }]);
})();