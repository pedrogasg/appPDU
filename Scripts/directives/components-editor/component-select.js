(function () {
    'use strict';
    angular.module('appPDU').directive('componentSelect', [function () {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-editor/component-select.html',
            scope: {},
            require: "^componentMain",
            controller: ['$scope', 'HtmlModels', function ($scope, HtmlModels) {
                this.selectType = function (type) {
                    $scope.typeSelected = true;
                };
                this.saveContent = function (content) {
                    HtmlModels.createHtmlChild($scope.parentId, content, function (html) {
                        $scope.mainCtrl.getWorkingItem().html(html);
                    });
                };
            }],
            controllerAs: 'ctrl',
            link: function (scope, element, attrs, mainCtrl) {
                scope.mainCtrl = mainCtrl;
                scope.parentId = attrs.componentSelect;
            }
        }
    }]);
})();