(function () {
    'use strict';
    angular.module('appPDU').directive('componentPageCreate', ['ObjectModels', function (ObjectModels) {
        return {
            restrict: 'A',
            templateUrl: 'scripts/directives/components-page/component-page-create.html',
            scope: {},
            controller: ['$scope', function ($scope) {
                function updatePageId (data, headers) {
                    $scope.page.id = headers('id');
                }
                this.startCreation = function () {
                    $scope.showEditor = true;
                }
                this.submit = function (form,model,metadata,classes) {
                    if (form.$valid) {
                        metadata.attributes.classList = classes.split(',');
                        model.metadata = JSON.stringify(metadata);
                        if ($scope.created) {
                            ObjectModels.update(model, updatePageId);
                        } else {
                            ObjectModels.save(model, updatePageId);
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
                scope.created = Boolean(id);
                if (scope.created) {
                    ObjectModels.get({ id: id }).$promise.then(function (page) {
                        scope.page = page;
                        scope.metadata = JSON.parse(page.metadata)
                        if (scope.metadata.attributes && scope.metadata.attributes.classList) {
                            scope.classes = scope.metadata.attributes.classList.join(',');
                        }
                    });
                } else {
                    scope.page = {
                        type: 1,
                        typeName: 'WebPage'
                    };
                }
            }
        }
    }]);
})();