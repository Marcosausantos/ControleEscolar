(function () {
    'use strict';

    var serviceBase = 'http://localhost:51319/';

    angular
        .module('controleescolar')
        .config(['$routeProvider', 'ngToastProvider', '$httpProvider', function ($routeProvider, ngToastProvider, $httpProvider) {
            $routeProvider
                .otherwise({
                    redirectTo: '/login'
                })
                .when('/home', {
                    templateUrl: 'home.html'
                });

            ngToastProvider.configure({
                animation: 'slide', 
                dismissButton: true
            });

            $httpProvider.interceptors.push('authInterceptorService');
        }])

        .constant('ngAuthSettings', {
            apiServiceBaseUri: serviceBase,
            clientId: 'ngAuthApp'
        })     


        .run(['authService', function (authService) {
            authService.fillAuthData();
        }]);
 
})(window.angular);
