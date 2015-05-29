(function () {
    'use strict';
    angular.module('appPDU').directive('componentPageCreate', ['ObjectModels', function (ObjectModels) {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-page/component-page-create.html',
            scope: {},
            controller: ['$scope', function ($scope) {
                this.startCreation = function () {
                    $scope.showEditor = true;
                }
                this.submit = function (form,model,metadata) {
                    if (form.$valid) {
                        model.metadata = JSON.stringify(metadata);
                        if ($scope.created) {
                            ObjectModels.update(model, function (data) {
                                console.log(data)
                            });
                        } else {
                            ObjectModels.save(model, function (data) {
                                console.log(data);
                            });
                        }
                    }
                }
                this.changeTitle = function (page) {
                    page.name = page.title;
                }
            }],
            controllerAs:'ctrl',
            link: function (scope, element, attrs, ctrl) {
                var id = attrs.componentPageCreate;
                scope.created = new Boolean(id);
                if (scope.created) {
                    ObjectModels.get({ id: id }).$promise.then(function (page) {
                        scope.page = page;
                        scope.metadata = JSON.parse(page.metadata)
                    });
                } else {
                    scope.page = {
                        type: 1,
                        typeName: 'page'
                    };
                }
            }
        }
    }]);
})();