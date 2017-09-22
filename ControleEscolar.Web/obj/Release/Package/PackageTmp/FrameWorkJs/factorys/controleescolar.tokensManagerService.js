(function () {
    'use strict';

    angular
        .module('controleescolar')
        .factory('tokensManagerService', tokensManagerService);

    tokensManagerService.$inject = ['$http', 'ngAuthSettings'];

    function tokensManagerService($http, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;

        var tokenManagerServiceFactory = {};

        var _getRefreshTokens = function () {

            return $http.get(serviceBase + 'api/refreshtokens').then(function (results) {
                return results;
            });
        };

        var _deleteRefreshTokens = function (tokenid) {

            return $http.delete(serviceBase + 'api/refreshtokens/?tokenid=' + tokenid).then(function (results) {
                return results;
            });
        };

        tokenManagerServiceFactory.deleteRefreshTokens = _deleteRefreshTokens;
        tokenManagerServiceFactory.getRefreshTokens = _getRefreshTokens;

        return tokenManagerServiceFactory;
    };
})(window.angular);