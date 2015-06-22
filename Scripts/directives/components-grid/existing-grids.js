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
                    ContainerModels.createTemplateContainer()
                    $scope.parentCtrl.showEditor()
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
                ContainerModels.getTemplates().then(function (result) {
                    var containers = []
                    var temps = result.data;
                    for (var i = 0, temp; temp = temps[i]; i++) {
                        var children = [], tempChildren;
                        var tempChildren = temp.children;
                        for (var j = 0, child; child = tempChildren[j]; j++) {
                            var meta = JSON.parse(child.metadata);
                            console.log(meta);
                            children.push({ id: child.id, classes: meta.attributes.classList.join(' ') });
                        }
                        containers.push({ id: temp.id, children: children });
                    }
                    scope.templates = containers;
                })
            }
        }
    }]);
})();