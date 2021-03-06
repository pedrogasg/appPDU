﻿(function () {
    'use strict';
    angular.module('appPDU').directive('editorGrid', ['$document', 'ContainerModels', function ($document, ContainerModels) {
        return {
            restrict: 'A',
            require: "^componentGrid",
            templateUrl: 'scripts/directives/components-grid/editor-grid.html',
            scope: {},
            controller: ['$scope', function ($scope) {
                var self = this;
                this.templateId = null;
                this.isLarge = false;
                this.validateGrid = function (id) {
                    ContainerModels.saveTemplate().
                        then(function (data) {
                            C
                            return ContainerModels.saveTemplateChildren()
                        }).
                        then(function (data) {

                        })
                    ContainerModels.saveTemplateChildren().then(function (result) {
                        var ids = result.data;
                        ContainerModels.setChildrenIds(ids);
                        ContainerModels.saveTemplate(function (result, headers) {
                            self.templateId = headers('id');
                        });
                    });
                    //$scope.parentCtrl.selectGrid($scope.parentCtrl.gridId);
                    $scope.parentCtrl.hideEditor();
                };
                this.close = function () {
                    self.templateId = null;
                    var cursor = $scope.cursor;
                    while (cursor.previousSibling && cursor.previousSibling.className.indexOf('new-container') != -1) {
                        cursor.parentNode.removeChild(cursor.previousSibling);
                    }
                    $scope.parentCtrl.hideEditor()
                }
                this.expand = function () {
                    moving(false);
                };
                this.move = function () {
                    moving(true);
                };
                this.addGrid = function () {

                    var cursor = $scope.cursor,
                        className = cursor.className,
                        size = Number(cursor.dataset.size),
                        offset = Number(cursor.dataset.offset),
                        start = Number(cursor.dataset.start),
                        div = document.createElement('div');
                    ContainerModels.createContainer(className);
                    div.className = 'new-container ' + className;
                    div.style.height = cursor.dataset.height + 'px';
                    cursor.parentNode.insertBefore(div, cursor);
                    cursor.dataset.start = (size + offset + start) % 12;
                    cursor.dataset.size = 1;
                    cursor.dataset.offset = 0;
                    cursor.className = "col-xs-1 col-xs-offset-0";
                };
                this.sprout = function () {
                    changeSize(30);
                }
                this.reduce = function () {
                    changeSize(-30);
                }
                function moving(pred) {
                    $scope.expand = !pred;
                    $scope.move = pred;
                }
                function changeSize(x) {
                    var cursor = $scope.cursor,
                        size = Number(cursor.dataset.height) + x;
                    self.isLarge = size > 90;
                    cursor.dataset.height = size;
                    cursor.style.height = size + 'px';
                    $scope.move = false;
                    $scope.expand = false;
                }
            }],
            controllerAs: 'editorCtrl',
            link: function (scope, element, attrs, parentCtrl) {
                var $cursor = element.find('#editor-cursor-master'),
                    cursor = $cursor[0],
                    divs = document.querySelectorAll('.inside-bootstrap>div'),
                    rightBounds = [];
                scope.cursor = cursor;
                scope.expand = false;
                scope.move = false;
                for (var i = 0, div; div = divs[i]; i++) {
                    rightBounds.push({
                        range: div.getBoundingClientRect().right,
                        index: div.dataset.index
                    });
                }
                element.find('.inside-bootstrap-master').on('dragover', function (event) {
                    var e = event.originalEvent;
                    for (var i = 0, bound; bound = rightBounds[i]; i++) {
                        if (e.pageX < bound.range) {
                            var size = Number(cursor.dataset.size),
                                offset = Number(cursor.dataset.offset),
                                start = Number(cursor.dataset.start),
                                index = Number(bound.index),
                                rest = 0;
                            if (scope.expand) {
                                size = index - offset - start;
                            } else if (scope.move) {
                                offset = index - 1 - start;
                            }
                            rest = size + offset + start;
                            if (rest > 12) {
                                size = size - (rest - 12);
                            }
                            if (size > 0 && offset >= 0) {
                                cursor.dataset.size = size;
                                cursor.dataset.offset = offset;
                                var classes = "col-xs-" + size + " col-xs-offset-" + offset;
                                cursor.className = classes;
                            }
                            break;
                        }
                    }
                });
                scope.parentCtrl = parentCtrl;
            }
        }
    }]);
})();