(function() {
    'use strict';

    angular.module('appPDU').
        directive('cleanText', [function () {
            var charMap = {
                "ä": "a", "ö": "o", "ü": "u",
                "Ä": "A", "Ö": "O", "Ü": "U",
                "á": "a", "à": "a", "â": "a",
                "é": "e", "è": "e", "ê": "e",
                "ú": "u", "ù": "u", "û": "u",
                "ó": "o", "ò": "o", "ô": "o",
                "Á": "A", "À": "A", "Â": "A",
                "É": "E", "È": "E", "Ê": "E",
                "Ú": "U", "Ù": "U", "Û": "U",
                "Ó": "O", "Ò": "O", "Ô": "O",
                "ß": "s"
            };
            return {
                require: 'ngModel',
                restrict: 'A',
                link: function (scope, element, attrs, ngModelController) {
                    element.on('blur', function (e) {
                        var temp = element.val().toString().toLowerCase().replace(/\s/g, '-').replace(/[öäüÖÄÜáàâéèêúùûóòôÁÀÂÉÈÊÚÙÛÓÒÔß]/g, function (a) { return charMap[a] || a });
                        ngModelController.$setViewValue(temp, 'clean-text');
                        ngModelController.$render();
                    })
                    ngModelController.$formatters.unshift(function (data) {
                        if (data) {
                            var temp = data.toString().toLowerCase().replace(/\s/g, '-').replace(/[öäüÖÄÜáàâéèêúùûóòôÁÀÂÉÈÊÚÙÛÓÒÔß]/g, function (a) { return charMap[a] || a });
                            ngModelController.$setViewValue(temp, 'clean-text');
                            ngModelController.$render();
                            return temp;
                        } else {
                            return data;
                        }
                    });
                }
            };
        }]);

})();