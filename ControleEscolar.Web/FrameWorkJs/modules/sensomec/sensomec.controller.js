(function () {
    'use strict';   

    angular
        .module('controleescolar.sensomec')
        .controller('sensomeccontroller', basecontroller);
    
    basecontroller.$inject = ['$routeParams', 'utils', 'ngToast', 'dataService', 'factory', 'controller', 'title', 'entidades'];
        
    function basecontroller($routeParams, utils, ngToast, dataService, factory, controller, title, entidades) {
        /* jshint validthis:true */
        var vm = this;

        vm.currents = [];
        vm.current = {};
        vm.title = title;
        vm.controller = controller;

        vm.envioPermitido = false;

        vm.utils = utils;

        vm.factory = factory;        

        vm.readonly = false;        

        vm.activate = function () {

            vm.setentidades(entidades);

            // get entity to edit
            if ($routeParams.id !== undefined && $routeParams.id != "0") {                
                dataService.get(vm.controller, $routeParams.id).then(function (resp) {
                    vm.current = resp;
                    vm.liberaEnvio();
                    vm.readonly = vm.utils.setreadonlyinput(!vm.rotinaConcluida());
                });
            }
            else {
                setNumeroRotina();
                vm.current.Competencia = new Date();
                vm.current.DateGeracao = new Date();
                vm.current.Status = "Incluído";

                getAluno();
            }

            if (($routeParams.readonly !== undefined && $routeParams.readonly == "readonly") ) {                
                vm.readonly = vm.utils.setreadonlyinput(true);
            }
        }

        vm.liberaEnvio = function () {
            var result = vm.current.SensoMecItens.some(function (e) {
                return (e.Enviar == true && vm.readonly);
            })
            vm.envioPermitido = result && vm.current.Status != "Concluído";
        };

        //lista de entidades
        vm.setentidades = function(entidades) {
            for (var i = 0; i < entidades.length; i++) {
                vm.listentidades(entidades[i]);
            }
        }

        vm.rotinaConcluida = function () {
            return vm.current.Status != "Concluído";
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

        function setNumeroRotina() {
            dataService.list('sensomec/').then(function (resp) {
                vm.current.NumeroRotina = resp.length + 1;
            });
        }

        function getAluno() {
            dataService.list('SensoMec/getAlunos/').then(function (resp) {
                vm.current.SensoMecItens = resp;
            });
        }

        vm.new = function () {
            vm.utils.gopath(vm.controller + '/0');
        }

        vm.cancel = function () {
            vm.utils.gopath(vm.controller);
        }

        vm.enviar = function () {
            vm.current.Status = "Enviado";

            vm.sensoenviar = [];
            vm.current.SensoMecItens.forEach(function (element) {
                if (element.Enviar)
                    vm.sensoenviar.push({ nomealuno: element.Aluno.Nome, datanascimento: element.Aluno.DataNascimento });
            });

            dataService.save('SensoMec/PostEnviarAlunos', vm.sensoenviar).then(function (resp) {
                vm.current.NumeroProtocoloRetorno = resp;
                vm.current.DataEnvio = new Date();
                vm.current.Status = "Concluído";
                vm.update();
                ngToast.create('Operação realizada com sucesso');
            }, function (error) {
                console.log('widget: Erro ao salvar.');
                vm.current.Status = "Erro";
                vm.update();
            });
        }

        vm.save = function (go) {
            dataService.save(vm.controller, vm.current).then(function (resp) {
                ngToast.create('Operação realizada com sucesso');
                if (!go)
                    vm.utils.gopath(vm.controller);
                else {
                    vm.current = resp;
                    vm.readonly = vm.utils.setreadonlyinput(true);
                    vm.liberaEnvio();
                }

            }, function (error) {
                console.log('widget: Erro ao salvar.');
            });
        }

        vm.update = function () {
            dataService.update(vm.controller, vm.current.Id, vm.current).then(function (resp) {
                vm.readonly = vm.utils.setreadonlyinput(true);
                vm.liberaEnvio();
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

