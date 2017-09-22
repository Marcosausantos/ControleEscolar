(function () {
    'use strict';

    angular
        .module('controleescolar.usuario')
        .config(['$routeProvider', function ($routeProvider) {

            $routeProvider
                 .when('/usuario', {
                     templateUrl: '../../layout/grid-crud.html',
                     controller: 'usuariocontroller',
                     controllerAs: 'vm',
                     resolve: {
                         factory: [function () {
                             return null;
                         }],
                         controller: [function () {
                             return "usuario";
                         }],
                         title: [function () {
                             return "Lista de usuário";
                         }],
                         entidades: [function () {
                             return [];
                         }]
                     }
                 })
                .when('/usuario/:id/:readonly?', {
                    templateUrl: '../usuario/view/usuario-edit.html',
                    controller: 'usuariocontroller',
                    controllerAs: 'vm',
                    resolve: {
                        factory: [function () {
                            return null;
                        }],
                        controller: [function () {
                            return "usuario";
                        }],
                        title: [function () {
                            return "Cadastro de usuário";
                        }],
                        entidades: [function () {
                            return [];
                        }]
                    }
                });
        }]);

})(window.angular);
