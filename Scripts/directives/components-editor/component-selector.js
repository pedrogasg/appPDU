(function () {
    'use strict';
    angular.module('appPDU').directive('componentSelector', [function () {
        return {
            restrict: 'A',
            require: "^componentMain",
            templateUrl: 'scripts/directives/components-editor/component-selector.html',
            scope: {},
            link: {
                pre: function (scope, element, attrs){ 
                    scope.action = attrs.action;
                },
                post: function (scope, element, attrs, ctrl) {
                    var children = element.parent().find('.item-menu');
                    if (children.length > 1) {
                        console.log(scope.action);
                        scope.$destroy();
                        element.remove();
                        return;
                    }
                    var id = attrs.componentSelector;
                    scope.openModalWithAction = function () {
                        console.log(scope.action);
                    }
                    scope.openSelector = function () {
                        ctrl.setWorkingItem(element.parent());
                        ctrl.openModal('select', id)
                    }

                    element.parent().css('position', 'relative')

                }
            }
        }
    }]);
})();