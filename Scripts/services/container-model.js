(function () {
    'use strict';
    angular.module('appPDU').factory('ContainerModels', ['$http', 'ObjectModels', function ($http, ObjectModels) {
        var template, templateChildren;
        /**
        * A unique time based name !
        */
        function timelessName() {
            return 'pdu-' + Date.now().toString();
        }
        /**
        * Create a new empty template
        */
        function createTemplateContainer() {
            var now = timelessName(),
                    metadata = {
                        subType: 'Template',
                        attributes: {
                            id: now,
                            classList: ['container']
                        },
                    };

            template = {
                name: now,
                type: 2,
                typeName: 'Container',
                metadata: JSON.stringify(metadata),
                data: '',
                order: 0,
                childrenIds: []
            };
            templateChildren = [];
        }
        /**
        * Add the proper template uid to a page
        * @param {Guid} pageId - The Id of the page
        * @param {Guid} id - The template Id
        * @returns {Promise} The ngResourcePromise of the update page
        */
        function addTemplateToPage(pageId, id) {
            return ObjectModels.get({ id: pageId }).$promise.then(function (page) {
                var metadata = JSON.parse(parent.metadata);
                metadata.template = id;
                page.metadata = JSON.stringify(metadata);
                return ObjectModels.update(page);
            });
        }
        /**
        * Create a container with a space delimited CSS classes
        * @parma {string} classes - The space delimited CSS classes
        */
        function createContainer(classes) {
            var now = timelessName(),
                childMetadata = {
                    subType: classes.replace(/\s/g, ''),
                    attributes: {
                        id: now,
                        classList: classes.split(' ')
                    }
                },
                model = {
                    name: now,
                    type: 4,
                    typeName: 'Container',
                    metadata: JSON.stringify(childMetadata),
                    data: '',
                    childrenIds: []
                };
            templateChildren.push(model);
        }
        /**
         * Delete a container in the templateChildren array.
         * @param {string} name - The name of the container.
         */
        function deleteContainer(name) {
            for (var i = 0, model; model = templateChildren[i]; i++) {
                if (name == model.name) {
                    templateChildren.splice(i, 1);
                    break;
                }
            }
        }
        /**
        * Get All Pages in a promise
        * @returns {Promise} the $http promise with all the pages
        */
        function getPages() {
            return $http.get('api/type/1');
        }
        /**
        * Get all templates with containers
        * @returns {Promise} the $http promise with all the containers 
        */
        function getTemplates() {
            return $http.get('api/templates');
        }


        /**
        * Save the template children
        * @returns {Promise} the $http promise with the ids of all the containers created
        */
        function saveTemplateChildren() {
            for (var i = 0, child; child = templateChildren[i]; i++) {
                child.order = i;
            }
            return saveModels(templateChildren);
        }
        /**
        * Save many objects at the same time
        * @params {ObjectModel[]} - An array with all the objects to create
        * @returns {Promise} the $http promise with the ids of all the objects created
        */
        function saveModels(models) {
            return $http.post('api/creationbatch', models);
        }

        /**
         * This callback is displayed for save the template.
         * @callback creationCallback
         * @param {Result} result
         * @param {headers} headers
         */

        /**
        * Save template
        * @param {creationCallback} callback - The callback to get the headers
        * @returns {Promise} The ngResourcePromise of the save template
        */
        function saveTemplate(callback) {
            return ObjectModels.save(template, callback)
        }


        function setChildrenIds(ids) {
            template.childrenIds = ids;
        }
        return {
            createContainer: createContainer,
            deleteContainer: deleteContainer,
            getPages: getPages,
            getTemplates: getTemplates,
            createTemplateContainer: createTemplateContainer,
            saveTemplate: saveTemplate,
            saveModels: saveModels,
            saveTemplateChildren: saveTemplateChildren,
            addTemplateToPage: addTemplateToPage,
            setChildrenIds: setChildrenIds
        }
    }]);
})();