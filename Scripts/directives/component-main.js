(function () {
    'use strict';
    angular.module('appPDU').directive('componentMain', ['$compile', function ($compile) {
        return {
            restrict: 'A',
            scope: {},
            controller: function ($scope) {
                var modal, modalWindow;
                this.addModal = function (m) {
                    modal = m;
                    modalWindow = m.children();
                }
                this.openModal = function (message) {
                    modal.css('display', 'block');
                    var el = $compile('<div component-' + message + '></div>')($scope);
                    modalWindow.append(el);
                }
                this.closeModal = function (target) {
                    if (!modalWindow[0].contains(target)) {
                        modal.css('display', 'none')
                        modalWindow.empty();
                    }
                };
            }
        }
    }]);
})();