(function () {
    'use strict';
    angular.module('appPDU').directive('componentHtml', [function () {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-html/component-html.html',
            scope: {},
            require: "^componentMain",
            controller: ['$scope', '$http', 'HtmlModels', function ($scope, $http, HtmlModels, ObjectModels) {
                var self = this;
                                
                this.saveContentHtml = function (content) {
                    HtmlModels.createHtmlChild($scope.parentId, content, function (html) {
                        $scope.mainCtrl.getWorkingItem().html(html);
                        $scope.mainCtrl.closeModal();
                    });
                };
                
                this.close = function () {
                    $scope.mainCtrl.closeModal();
                }
                
            }],
            controllerAs: 'ctrl',
            link: {
                pre:function (scope, element, attrs) {
                    scope.currentId = attrs.currentId;
                },
                post: function (scope, element, attrs, mainCtrl) {
                    if (scope.currentId) {
                        HtmlModels.getHtml(scope.currentId).then(function (result) {
                            scope.content = result.data;
                        });
                    }
                    scope.mainCtrl = mainCtrl;
                    scope.parentId = attrs.componentHtml;
                }
            }
        }
    }]);
})();