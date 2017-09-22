(function () {
    'use strict';

    angular
        .module('controleescolar')
        .factory('utils', utils);

    utils.$inject = ['$http', '$location', 'ngToast'];

    function utils($http, $location, ngToast) {
        var service = {
            //Componentes
            setreadonlyinput: setreadonlyinput,
            //Location
            gopath: gopath,
            //validações
            jaincluido: jaincluido,
            //Funções
            removerlist: removerlist
        };
        
        return service;

        //Componentes
        function setreadonlyinput(readonly) {
            
            $('Form input').attr('disabled', readonly);
            
            return readonly;
        }

        //Location
        function gopath(psPath) {
            $location.path("/" + psPath);
        }

        //Validações
        function jaincluido(pLista, pCampo, pId, pMensagem) {
            var permitir = true;

            if (pLista) {
                var i = 0;

                angular.forEach(pLista, function (x) {
                    if (x[pCampo] == pId) {
                        ngToast.create({
                            className: 'warning',
                            content: pMensagem
                        });
                        permitir = false;
                    }
                    i++;
                });
            }

            return permitir;
        }

        //funções
        function removerlist(pLista, pCampo, pId) {
            var permitir = true;

            if (pLista) {
                var i = 0;
                
                angular.forEach(pLista, function (x) {
                    if (x[pCampo] == pId) {
                        pLista.splice(i, 1);
                    }
                    i++;
                });
            }

            return permitir;
        }
    }
})(window.angular);