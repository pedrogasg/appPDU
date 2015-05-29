(function () {
    'use strict';
    angular.module('appPDU').factory('ObjectModels', ['$resource',function ($resource) {
        return $resource('/api/ObjectModel/:id', null, {
            'update': { method: 'PUT' }
        })
    }]);
})();