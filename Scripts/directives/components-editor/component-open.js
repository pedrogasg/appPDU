(function () {
    'use strict';
    angular.module('appPDU').directive('componentOpen', [function () {
        return {
            restrict: 'A',
            require: "^componentMain",
            link: function (scope, element, attrs, ctrl) {
                element.on('click', function (e) {
                    ctrl.openModal(attrs.componentOpen,attrs.id);
                })
            }
        }
    }]);
})();