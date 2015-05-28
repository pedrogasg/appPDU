(function () {
    'use strict';
    angular.module('appPDU').directive('componentSelect', [function () {
        return {
            restrict: 'A',
            template: '<select><option value="image">Image</option><option value="html">HTML</option><option value="list">List</option></select>',
        }
    }]);
})();