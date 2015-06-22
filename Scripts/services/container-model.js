(function () {
    'use strict';
    angular.module('appPDU').factory('ContainerModels', ['$http', 'ObjectModels', function ($http, ObjectModels) {
        var template, templateChilds;
        function createTemplateContainer() {
            var now = Date.now().toString(),
                    metadata = {
                        subType: 'Template',
                        attributes: {
                            id: now,
                            classList: ['container']
                        },
                    };

            template = {
                name: now,
                type: 2,
                typeName: 'Container',
                metadata: JSON.stringify(metadata),
                data: '',
                order: 0,
                childrenIds: []
            };
        }
        function saveTemplate(parentId, id) {
            ObjectModels.get({ id: parentId }).$promise.then(function (parent) {
                var metadata = JSON.parse(parent.metadata);
                metadata.template = id;
                parent.metadata = JSON.stringify(metadata);
                ObjectModels.update(parent);
            });
        }
        function createContainer(parentId, classes, order, callback, beforeUpdateParent) {
            var childMetadata = {
                    subType: classes.replace(/\s/g, ''),
                    attributes: {
                        id: Date.now().toString(),
                        classList: classes.split(' ')
                    },
                    childrenIds: []
                },
                model = {
                    name: Date.now().toString(),
                    type: 4,
                    typeName: 'Container',
                    metadata: JSON.stringify(childMetadata),
                    data: '',
                    order: order
                };
        }
        function getPages() {
            return $http.get('api/type/1');
        }
        function getTemplates() {
            return $http.get('api/templates');
        }
        return {
            createContainer: createContainer,
            getPages: getPages,
            getTemplates: getTemplates,
            createTemplateContainer: createTemplateContainer,
            saveTemplate: saveTemplate
        }
    }]);
})();