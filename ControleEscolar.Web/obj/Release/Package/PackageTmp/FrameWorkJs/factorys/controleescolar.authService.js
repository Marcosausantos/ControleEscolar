(function () {
    'use strict';

    angular
        .module('controleescolar')
        .factory('authService', authService);

    authService.$inject = ['$http', '$q', 'localStorageService', 'ngAuthSettings', 'dataService'];

    function authService($http, $q, localStorageService, ngAuthSettings, dataService) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            userName: "",
            useRefreshTokens: false,
            userMenu: [],
            userMenuPrincipal: {}
        };

        var _externalAuthData = {
            provider: "",
            userName: "",
            externalAccessToken: ""
        };        

        var _setMenu = function (userMenu) {
             var authData = localStorageService.get('authorizationData');

             _authentication.userMenu = userMenu;

             localStorageService.set('authorizationData', {
                 token: authData.token,
                 userName: authData.userName,
                 refreshToken: authData.refreshToken,
                 useRefreshTokens: authData.useRefreshTokens,
                 userMenu: userMenu
             });
        };

        var _setMenuPrincipal = function (userMenu) {            
            var authData = localStorageService.get('authorizationData');
            
            _authentication.userMenuPrincipal = userMenu;

            localStorageService.set('authorizationData', {
                token: authData.token,
                userName: authData.userName,
                refreshToken: authData.refreshToken,
                useRefreshTokens: authData.useRefreshTokens,
                userMenu: authData.userMenu,
                userMenuPrincipal: userMenu
            });
        };

        var _login = function (loginData) {
            localStorageService.set('authorizationData', null);

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            if (loginData.useRefreshTokens) {
                data = data + "&client_id=" + ngAuthSettings.clientId;
            }

            var deferred = $q.defer();

            $http.post(serviceBase + 'token',
                       data,
                       {                           
                           headers: {
                               'Accept': "application/json, text/plain, */*",
                               'Content-Type': 'application/x-www-form-urlencoded'
                           }
                       }).success(function (response) {

                if (loginData.useRefreshTokens) {
                    localStorageService.set('authorizationData', {
                        token: response.access_token,
                        userName: loginData.userName,
                        refreshToken: response.refresh_token,
                        useRefreshTokens: true
                    });
                }
                else {
                    localStorageService.set('authorizationData', {
                        token: response.access_token,
                        userName: loginData.userName,
                        refreshToken: "",
                        useRefreshTokens: false
                    });
                }

                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;
                _authentication.useRefreshTokens = loginData.useRefreshTokens;
                _authentication.userMenu = [];
                _authentication.userMenuPrincipal = [];

                deferred.resolve(response);                             

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _logOut = function () {

            localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;
            _authentication.userMenu = [];
            _authentication.userMenuPrincipal = [];

        };

        var _fillAuthData = function () {

            var authData = localStorageService.get('authorizationData');

            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
                _authentication.useRefreshTokens = authData.useRefreshTokens;
                _authentication.userMenu = authData.userMenu;
                _authentication.userMenuPrincipal = authData.userMenuPrincipal;
            }

        };

        var _refreshToken = function () {
            var deferred = $q.defer();

            var authData = localStorageService.get('authorizationData');

            if (authData) {

                if (authData.useRefreshTokens) {

                    var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;

                    localStorageService.remove('authorizationData');

                    $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                        localStorageService.set('authorizationData', {
                            token: response.access_token,
                            userName: response.userName,
                            refreshToken: response.refresh_token,
                            useRefreshTokens: true
                        });

                        deferred.resolve(response);

                    }).error(function (err, status) {
                        _logOut();
                        deferred.reject(err);
                    });
                }
            }

            return deferred.promise;
        };

        var _obtainAccessToken = function (externalData) {

            var deferred = $q.defer();

            $http.get(serviceBase + 'api/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {

                localStorageService.set('authorizationData', {
                    token: response.access_token,
                    userName: response.userName,
                    refreshToken: "",
                    useRefreshTokens: false
                });

                _authentication.isAuth = true;
                _authentication.userName = response.userName;
                _authentication.useRefreshTokens = false;
                _authentication.userMenu = [];
                _authentication.userMenuPrincipal = [];

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        };

        var _registerExternal = function (registerExternalData) {

            var deferred = $q.defer();

            $http.post(serviceBase + 'api/account/registerexternal', registerExternalData).success(function (response) {

                localStorageService.set('authorizationData', {
                    token: response.access_token,
                    userName: response.userName,
                    refreshToken: "",
                    useRefreshTokens: false
                });

                _authentication.isAuth = true;
                _authentication.userName = response.userName;
                _authentication.useRefreshTokens = false;
                _authentication.userMenu = [];
                _authentication.userMenuPrincipal = [];

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _isAuthenticated = function (path) {
            if (path.substr(1, path.length).lastIndexOf('/') > 0)
                path = path.substr(0, path.substr(1, path.length).indexOf('/') + 1);

            var permite = false;
            angular.forEach(_authentication.userMenu, function (x) {
                if (x) {
                    if (x == path) {
                        permite = true;
                        return;
                    }
                }
            });

            //angular.forEach(_authentication.userMenu, function (x) {
            //    if (x.Pagina) {
            //        if (x.Pagina.Url.replace('#', '') == path) {
            //            permite = true;
            //            return;
            //        }
            //    }

            //    angular.forEach(x.SubMenus, function (z) {
            //        if (z.Pagina) {
            //            if (z.Pagina.Url.replace('#', '') == path) {
            //                permite = true;
            //                return;
            //            }
            //        }
            //    });
            //});
            return permite;
        }

        var _pageacesso = function (path) {
            var listpage = ['/login', '/home'];

            if (path.substr(1, path.length).lastIndexOf('/') > 0)
                path = path.substr(0, path.substr(1, path.length).indexOf('/') + 1);

            return listpage.indexOf(path) >= 0;
        }
        
        authServiceFactory.login            = _login;
        authServiceFactory.logOut           = _logOut;
        authServiceFactory.fillAuthData     = _fillAuthData;
        authServiceFactory.authentication   = _authentication;
        authServiceFactory.refreshToken     = _refreshToken;
        authServiceFactory.setMenu          = _setMenu;
        authServiceFactory.setMenuPrincipal = _setMenuPrincipal;
        authServiceFactory.isAuthenticated  = _isAuthenticated;
        authServiceFactory.pageacesso       = _pageacesso;

        authServiceFactory.obtainAccessToken = _obtainAccessToken;
        authServiceFactory.externalAuthData  = _externalAuthData;
        authServiceFactory.registerExternal  = _registerExternal;

        return authServiceFactory;
    };
})(window.angular);