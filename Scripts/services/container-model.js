﻿(function () {
    'use strict';
    angular.module('appPDU').factory('ContainerModels', ['$http', 'ObjectModels', function ($http,ObjectModels) {
        function createContainer(parentId, classes, order, callback, beforeUpdateParent) {
            ObjectModels.get({ id: parentId }).$promise.then(function (parent) {
                var metadata = JSON.parse(parent.metadata),
                    childMetadata = {
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
                        order:order
                    };
                ObjectModels.save(model, function (data, headers) {
                    var id = headers('id');
                    metadata.childrenIds = metadata.childrenIds || [];
                    metadata.childrenIds.push(id);
                    if (beforeUpdateParent) {
                        beforeUpdateParent(id,parent,metadata);
                    }
                    parent.metadata = JSON.stringify(metadata);
                    ObjectModels.update(parent).$promise.then(function (object) {
                        callback(id);
                    });
                });
            });
        }
        function getPages() {
            $http.get('api/webpages');
        }
        return {
            createContainer: createContainer,
            getPages:getPages
        }
    }]);
})();