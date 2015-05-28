(function () {
    'use strict';
    angular.module('appPDU').directive('componentSelector', [function () {
        return {
            restrict: 'A',
            require: "^componentMain",
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
    }]);
})();