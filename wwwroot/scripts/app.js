(function () {
    'use strict';

    angular.module('appPDU', [
        'ui.router'
    ]).directive('mainControl', ['$compile',function ($compile) {
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
                    var el = $compile('<div '+message+'-component></div>')($scope);
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
    }]).
    directive('componentSelector', [function () {
        return {
            restrict: 'A',
            require: "^mainControl",
            link: function (scope, element, attrs, ctrl) {
                var control = document.createElement('div');
                control.className = "component-menu";
                control.innerHTML = '<ul><li class="update">Update</li><li class="remove">Remove</li><li class="select">Select</li></ul>';
                control = angular.element(control);
                control.on('click', function (e) {
                    var target = e.target;
                    ctrl.openModal(target.className)
                })
                element.css('position', 'relative')
                element.append(control);
            }
        }
    }]).
    directive('componentEditor', [function () {
        return {
            restrict: 'A',
            require: "^mainControl",
            link: function (scope, element, attrs, ctrl) {
                ctrl.addModal(element);
                element.on('click', function (e) {
                    ctrl.closeModal(e.target);
                })
            }
        }
    }]).directive('selectComponent', [function () {
        return {
            restrict: 'A',
            template: '<select><option value="image">Image</option><option value="html">HTML</option><option value="list">List</option></select>',
        }
    }]);
})();