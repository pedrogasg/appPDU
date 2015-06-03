(function () {
    'use strict';
    angular.module('appPDU').directive('componentSelector', [function () {
        return {
            restrict: 'A',
            require: "^componentMain",
            templateUrl: 'scripts/directives/components-editor/component-selector.html',
            scope: {},
            link: function (scope, element, attrs, ctrl) {
                var children = element.parent().find('.item-menu');
                for (var i = 0, child; child = children[i]; i++) {
                    if (child == element.get(0)) {
                        continue;
                    } else {
                        scope.$destroy();
                        element.remove();
                        return;
                    }
                }
                var id = attrs.componentSelector;
                scope.action = function (action) {
                    ctrl.setWorkingItem(element.parent());
                    ctrl.openModal(action, id)
                }

                element.parent().css('position', 'relative')

            }
        }
    }]);
})();