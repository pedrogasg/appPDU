(function () {
    'use strict';
    angular.module('appPDU').directive('componentMain', ['$compile', function ($compile) {
        return {
            restrict: 'A',
            scope: {},
            controller: function ($scope) {
                var modal, modalWindow, workingItem, self = this;

                $scope.validateData = function (id) {

                    self.closeModal(null, true);
                }
                $scope.close = function (data) {
                    self.closeModal(null, true);
                }
                this.addModal = function (m) {
                    modal = m;
                    modalWindow = m.children();
                };
                this.setWorkingItem = function (item) {
                    workingItem = item;
                };
                this.getWorkingItem = function () {
                    return workingItem;
                };
                this.openModal = function (message, param) {
                    modal.css('display', 'block');
                    var el = $compile('<div component-' + message + '="' + param + '"></div>')($scope);
                    modalWindow.append(el);
                };
                this.closeModal = function (target, force) {
                    if (!modalWindow[0].contains(target) || force) {
                        modal.css('display', 'none');
                        workingItem = null;
                        modalWindow.empty();
                    }
                };
            }
        }
    }]);
})();