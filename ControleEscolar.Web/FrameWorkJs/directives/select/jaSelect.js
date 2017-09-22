(function () {
    'use strict';

    angular
        .module('controleescolar')
        .directive('jaSelect', ['dataService', function (dataService) {
            return {
                scope: {
                    jaController: '=jacontroller',
                    jaEntidades: '=jaentidades',
                    jaModel: '=jamodel',
                    jaResultado: '=jaresultado',                    
                    jaReadonly: '=jareadonly',
                    jaCustomAdd: '=jacustomadd',
                    jaCustomClear: '=jacustomclear'
                },
                templateUrl: '../../directives/select/jaSelect.html',
                link: function (scope, element, attrs) {                    
                    scope.onClear = function () {                        
                        scope.jaModel = null;
                    };

                    //&& (scope.jaEntidades == undefined)
                    if ((scope.jaController !== "") ) {
                        dataService.list(scope.jaController).then(function (resp) {
                            scope.jaEntidades = resp;
                        });
                    }
                }
            };
        }]);
})(window.angular);