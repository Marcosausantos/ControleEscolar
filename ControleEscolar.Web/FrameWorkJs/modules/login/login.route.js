
(function () {
    'use strict';

    angular
        .module('controleescolar.login')
        .config(['$routeProvider', function ($routeProvider) {

            $routeProvider                 
                .when('/login', {
                    templateUrl: '../login/view/login.html'
                });
        }]);

})(window.angular);
