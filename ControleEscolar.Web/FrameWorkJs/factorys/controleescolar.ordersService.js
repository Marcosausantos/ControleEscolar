(function () {
    'use strict';

    angular
        .module('controleescolar')
        .factory('ordersService', ordersService);

    ordersService.$inject = ['$http', 'ngAuthSettings'];

    function ordersService($http, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;

        var ordersServiceFactory = {};

        var _getOrders = function () {

            return $http.get(serviceBase + 'api/orders').then(function (results) {
                return results;
            });
        };

        ordersServiceFactory.getOrders = _getOrders;

        return ordersServiceFactory;
    };
})(window.angular);