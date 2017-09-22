(function () {
    'use strict';   

    angular
        .module('controleescolar.basecontroller')
        .controller('basecontroller', basecontroller);
    
    basecontroller.$inject = ['$routeParams', 'utils', 'ngToast', 'dataService', 'factory', 'controller', 'title', 'entidades'];
        
    function basecontroller($routeParams, utils, ngToast, dataService, factory, controller, title, entidades) {
        /* jshint validthis:true */
        var vm = this;

        vm.All = [];
        vm.currents = [];
        vm.current = {};
        vm.title = title;
        vm.controller = controller;

        vm.utils = utils;

        vm.factory = factory;        

        vm.readonly = false;        

        vm.activate = function () {

            vm.setentidades(entidades);

            // get entity to edit
            if ($routeParams.id !== undefined && $routeParams.id != "0") {                
                dataService.get(vm.controller, $routeParams.id).then(function (resp) {
                    vm.current = resp;
                });
            }
            
            if ($routeParams.readonly !== undefined && $routeParams.readonly == "readonly") {                
                vm.readonly = vm.utils.setreadonlyinput(true);
            }
        }

        //lista de entidades
        vm.setentidades = function(entidades) {
            for (var i = 0; i < entidades.length; i++) {
                vm.listentidades(entidades[i]);
            }
        }

        vm.listentidades = function (entidade) {
            dataService.list(entidade).then(function (resp) {
                vm.current[entidade] = resp;
            }, function (error) {
                console.log('widget: Erro ao carregar lista entidades.');
            });
        }

        vm.activate();
        
        //Comandos
        vm.list = function (controller) {
            dataService.list(controller).then(function (resp) {
                vm.currents = resp;
            }, function (error) {
                console.log('widget: Erro ao carregar lista.');
            });
        }

        vm.getData = function getData(url, params) {
            var dados = dataService.getData(url, params).then(function (resp) {
                vm.currents = resp;
            }, function (error) {
                console.log('widget: Erro ao carregar lista.');
            });

            return dados;            
        }

        vm.new = function () {
            vm.utils.gopath(vm.controller + '/0');
        }

        vm.cancel = function () {
            vm.utils.gopath(vm.controller);
        }

        vm.save = function (go) {
            dataService.save(vm.controller, vm.current).then(function (resp) {
                ngToast.create('Operação realizada com sucesso');

                if (!go)
                    vm.utils.gopath(vm.controller);
                else
                    vm.current = resp;

            }, function (error) {
                console.log('widget: Erro ao salvar.');
            });
        }

        vm.remove = function (pId) {
            if (confirm("Tem certeza que deseja excluir?")) {
                dataService.remove(vm.controller, pId).then(function (resp) {
                    ngToast.create('Operação realizada com sucesso');

                    vm.list(vm.controller);

                }, function (error) {
                    console.log('widget: Erro ao remover.');
                });
            }
        }

        vm.clear = function () {
            vm.current = {};            
        }
    };
})(window.angular);

