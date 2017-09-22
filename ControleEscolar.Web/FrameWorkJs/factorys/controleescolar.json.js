(function () {
    'use strict';

    angular
        .module('controleescolar')
        .factory('webjson', webjson);

    webjson.$inject = ['$http', '$q'];

    function webjson($http, $q) {
        var service = {
            getJson: getJson
        };        

        return service;

        function getJson(psJson) {
            var deferred = $q.defer();

            $http({ method: "GET", url: psJson + ".json" })
                .success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).error(function (data, status, headers, config) {
                    deferred.reject(data);
                });

            return deferred.promise;
        }        
    }
})(window.angular);