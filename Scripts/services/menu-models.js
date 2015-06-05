(function () {
    'use strict';
    angular.module('appPDU').factory('MenuModel', ['ObjectModels', function (ObjectModels) {
        function createMenu(parentId, childs, callback) {
            ObjectModels.get({ id: parentId }).$promise.then(function (parent) {
                var metadata = JSON.parse(parent.metadata),
                    childMetadata = {
                        subType: '',
                        attributes: {
                            id: '',
                            classList: ''
                        },
                        childrenIds: childs
                    },
                    model = {
                    name: Date.now().toString(),
                    type: 16,
                    typeName: 'Menu',
                    metadata: JSON.stringify(childMetadata),
                    data: ''
                }
                ObjectModels.save(model, function (data, headers) {
                    metadata.childrenIds.push(headers('id'));
                    parent.metadata = JSON.stringify(metadata);
                    ObjectModels.update(parent).$promise.then(function (object) {
                        callback(object);
                    });
                });
            });
        }
        return {
            createMenu: createMenu
        }
    }]);
})();