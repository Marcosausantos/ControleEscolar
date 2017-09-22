(function () {
    'use strict';

    angular
        .module('controleescolar.sensomec')
        .config(['$routeProvider', function ($routeProvider) {

            $routeProvider
                .when('/sensomec', {
                     templateUrl: '../../layout/grid-crud.html',
                     controller: 'sensomeccontroller',
                     controllerAs: 'vm',
                     resolve: {
                         factory: [function () {
                             return null;
                         }],
                         controller: [function () {
                             return "sensomec";
                         }],
                         title: [function () {
                             return "Lista de rotina do Mec";
                         }],
                         entidades: [function () {
                             return [];
                         }]
                     }
                 })
                .when('/sensomec/:id/:readonly?', {
                    templateUrl: '../sensomec/view/sensomec-edit.html',
                    controller: 'sensomeccontroller',
                    controllerAs: 'vm',
                    resolve: {
                        factory: [function () {
                            return null;
                        }],
                        controller: [function () {
                            return "sensomec";
                        }],
                        title: [function () {
                            return "Cadastro de rotina do Mec";
                        }],
                        entidades: [function () {
                            return ['aluno'];
                        }]
                    }
                });
        }]);

})(window.angular);
