(function () {
    'use strict';   

    angular.module('controleescolar', [
         // Angular modules
         'ngAnimate',
         'ngRoute',
         'ngResource',
         'ngSanitize',

         //Modules Segurança
         'controleescolar.basecontroller',
         'controleescolar.login',
         'controleescolar.usuario',

         //Escola
         'controleescolar.aluno',
         'controleescolar.sensomec',
         
         //Terceiros
         'LocalStorageModule',
         'angular-loading-bar',
         'validation',
         'validation.rule',
         'ngToast',
         'angularDateInterceptor'
    ]);

})(window.angular);
