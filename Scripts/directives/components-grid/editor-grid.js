(function () {
    'use strict';
    angular.module('appPDU').directive('editorGrid', ['$document', function ($document) {
        return {
            restrict: 'A',
            require: "^componentGrid",
            templateUrl: 'scripts/directives/components-grid/editor-grid.html',
            scope: {},
            controller: ['$scope', function ($scope) {
                this.validateGrid = function (id) {
                    $scope.parentCtrl.selectGrid(id);
                };
            }],
            controllerAs: 'editorCtrl',
            link: function (scope, element, attrs, parentCtrl) {
                var cursor = element.find('#editor-cursor-master')[0];
               
                element.find('.inside-bootstrap-master').on('dragover', function (event) {
                    var e = event.originalEvent,
                         divs = document.querySelectorAll('.inside-bootstrap>div');
                    for (var i = 0,div;div = divs[i];i++) {
                        console.log(div.getBoundingClientRect());
                    }
                    /*var target = e.target;
                    cursor.className = target.dataset.index;*/
                });
                scope.parentCtrl = parentCtrl;
            }
        }
    }]);
})();