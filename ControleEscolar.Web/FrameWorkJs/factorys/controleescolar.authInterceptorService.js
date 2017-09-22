(function () {
    'use strict';

    angular
        .module('controleescolar')
        .factory('authInterceptorService', authInterceptorService);

    authInterceptorService.$inject = ['$q', '$injector', '$location', '$window', 'localStorageService'];

    function authInterceptorService($q, $injector, $location, $window, localStorageService) {

        var authInterceptorServiceFactory = {};

        var _request = function (config) {

            config.headers = config.headers || {};
       
            var authData = localStorageService.get('authorizationData');
                       
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            var authService = $injector.get('authService');

            if ((!authService.authentication.isAuth ||
                 !authService.isAuthenticated($location.path())) &&
                !authService.pageacesso($location.path()))
            {
                authService.logOut();
                $location.path('/login');
                window.location.reload(true);            
            }

            return config;
        }

        var _responseError = function (rejection) {
            if (rejection.status === 401) {
                var authService = $injector.get('authService');
                var authData = localStorageService.get('authorizationData');

                if (authData) {
                    if (authData.useRefreshTokens) {
                        $location.path('/home');
                        return $q.reject(rejection);
                    }
                }

                authService.logOut();
                $location.path('/login');                
            }
            return $q.reject(rejection);
        }        

        authInterceptorServiceFactory.request = _request;
        authInterceptorServiceFactory.responseError = _responseError;

        return authInterceptorServiceFactory;
     };
})();