(function () {
    'use strict';
    angular.module('appPDU').factory('HtmlModels', ['ObjectModels', function (ObjectModels) {
        function createHtmlChild(parentId, html, callback) {
            ObjectModels.get({ id: parentId }).$promise.then(function (parent) {
                var metadata = JSON.parse(parent.metadata)
                var model = {
                    name: Date.now().toString(),
                    type: 8,
                    typeName: 'Html',
                    data: html.toString()
                }
                ObjectModels.save(model, function (data, headers) {
                    metadata.childrenIds.push(headers('id'));
                    parent.metadata = JSON.stringify(metadata);
                    ObjectModels.save(parent).$promise.then(function (object) {
                        callback(html);
                    });
                });
            });
        }
        return {
            createHtmlChild: createHtmlChild
        }
    }]);
})();