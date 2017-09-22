(function () {
    'use strict';

    angular
        .module('controleescolar.login')
        .controller('logincontroller', logincontroller);

    logincontroller.$inject = ['$scope', '$location', '$window', '$route', 'dataService', 'authService', 'webjson'];

    function logincontroller($scope, $location, $window, $route, dataService, authService, webjson) {
        /* jshint validthis:true */
        var vm = this;

        vm.message = "";
        vm.current = {};
        vm.current.userName = 'sysdba';
        vm.current.password = 'sysdba';

        vm.login = function () {
            authService.login(vm.current).then(function (response) {

                authService.setMenu(["/aluno", "/usuario", "/sensomec"]);
                vm.authentication = authService.authentication;

                $location.url('/home');
                vm.test = vm.isLogged();
            },
                function (err) {
                    vm.message = err.error_description;
                });
        };

        vm.isLogged = function () {
            return authService.authentication != undefined ? authService.authentication.isAuth : false;
        };

        vm.isActive = function (viewLocation) {
            return viewLocation === $location.path();
        };

        vm.logOut = function () {
            authService.logOut();

            $location.path('/login');
            vm.test = vm.isLogged();
        }

        vm.setMenuPrincipal = function (userMenu) {
            authService.setMenuPrincipal(userMenu);
        };

        vm.authentication = authService.authentication;
        vm.test = vm.isLogged();
    };
})(window.angular);
