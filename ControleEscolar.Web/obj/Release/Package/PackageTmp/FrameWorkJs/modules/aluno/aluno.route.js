(function () {
    'use strict';

    angular
        .module('controleescolar.aluno')
        .config(['$routeProvider', function ($routeProvider) {

            $routeProvider
                 .when('/aluno', {
                     templateUrl: '../../layout/grid-crud.html',
                     controller: 'basecontroller',
                     controllerAs: 'vm',
                     resolve: {
                         factory: [function () {
                             return null;
                         }],
                         controller: [function () {
                             return "aluno";
                         }],
                         title: [function () {
                             return "Lista de alunos";
                         }],
                         entidades: [function () {
                             return [];
                         }]
                     }
                 })
                .when('/aluno/:id/:readonly?', {
                    templateUrl: '../aluno/view/aluno-edit.html',
                    controller: 'basecontroller',
                    controllerAs: 'vm',
                    resolve: {
                        factory: [function () {
                            return null;
                        }],
                        controller: [function () {
                            return "aluno";
                        }],
                        title: [function () {
                            return "Cadastro de aluno";
                        }],
                        entidades: [function () {
                            return [];
                        }]
                    }
                });
        }]);

})(window.angular);
