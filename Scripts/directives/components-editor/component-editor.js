(function () {
    'use strict';
    angular.module('appPDU').directive('componentEditor', [function () {
        return {
            restrict: 'A',
            require: "^componentMain",
            link: function (scope, element, attrs, ctrl) {
                ctrl.addModal(element);
                element.on('click', function (e) {
                    ctrl.closeModal(e.target);
                })
            }
        }
    }]);
})();