(function () {
    'use strict';

    angular
        .module('controleescolar')
        .factory('dataService', dataService);

    dataService.$inject = ['$http', 'ngAuthSettings'];

    function dataService($http, ngAuthSettings) {
        var service = {            
            getData: getData,
            list: list,
            get: get,            
            save: save,
            update: update,
            remove: remove
        };

        var apiServiceBaseUri = ngAuthSettings.apiServiceBaseUri + 'api/';

        return service;

        function getData(url, param) {
            var data = $http.get(apiServiceBaseUri + url, { params: param }).then(function (resp) {
                return resp.data;
            });

            return data;
        }

        function list(url, skip, take) {
            var entities = $http.get(apiServiceBaseUri + url,
                { params: { skip: skip, take: take } }).then(function (resp) {
                return resp.data;
                });            

            return entities;
        }

        function get(url, id) {
            var entity = $http.get(apiServiceBaseUri + url + '/get', { params: { id: id } }).then(function (resp) {
                return resp.data;
            });

            return entity;
        }

        function save(url, entity) {
            var result = $http.post(apiServiceBaseUri + url, entity).then(function (resp) {
                return resp.data;
            });

            return result;
        }

        function update(url, id, entity) {
            var result = $http.put(apiServiceBaseUri + url, entity, { params: { id: id } }).then(function (resp) {
                return resp.data;
            });

            return result;
        }

        function remove(url, id) {
            var result = $http.delete(apiServiceBaseUri + url, { params: { id: id } }).then(function (resp) {
                return resp.data;
            });

            return result;
        }
    }
})(window.angular);