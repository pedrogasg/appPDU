(function () {
    'use strict';
    angular.module('appPDU').directive('componentOpenPage', [function () {
        return {
            restrict: 'A',
            require: "^componentMain",
            link: function (scope, element, attrs, ctrl) {
                element.on('click', function (e) {
                    ctrl.openModal('page-create',attrs.componentOpenPage);
                })
            }
        }
    }]);
})();