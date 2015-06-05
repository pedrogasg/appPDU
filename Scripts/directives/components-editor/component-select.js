(function () {
    'use strict';
    angular.module('appPDU').directive('componentSelect', [function () {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-editor/component-select.html',
            scope: {},
            require: "^componentMain",
            controller: ['$scope', '$http','HtmlModels','MenuModel', 'ObjectModels', function ($scope,$http, HtmlModels,MenuModel, ObjectModels) {
                var self = this;
                this.type = "";
                this.links = [];
                this.menus = [];
                this.ids = [];
                this.selectType = function (type) {
                    self.type = type;
                    if (type == 'MENU') {
                        $http.get('/api/webpages').then(function (result) {
                            self.links = result.data;
                        })
                    }
                };
                this.saveContentHtml = function (content) {
                    HtmlModels.createHtmlChild($scope.parentId, content, function (html) {
                        $scope.mainCtrl.getWorkingItem().html(html);
                        $scope.mainCtrl.closeModal();
                    });
                };
                this.addToMenu = function (id, name) {
                    console.log(id);
                    console.log(self.ids);
                    if (self.ids.indexOf(id) == -1) {
                        self.ids.push(id);
                        self.menus.push({
                            id: id,
                            name: name
                        })
                    }
                };
                this.close = function () {
                    $scope.mainCtrl.closeModal();
                }
                this.saveMenu = function () {
                    MenuModel.createMenu($scope.parentId, self.menus.map(function (m) { return m.id }), function (id) {
                        var list = document.getElementById('build-list');
                        $scope.mainCtrl.getWorkingItem().html(list.innerHTML);
                        $scope.mainCtrl.closeModal();
                    });
                }
            }],
            controllerAs: 'ctrl',
            link: function (scope, element, attrs, mainCtrl) {
                scope.mainCtrl = mainCtrl;
                scope.parentId = attrs.componentSelect;
            }
        }
    }]);
})();