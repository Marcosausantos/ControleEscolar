(function () {
    'use strict';

    angular
        .module('controleescolar')
        .filter('startPage', function () {
            return function (input, start) {                
                start = +start;
                return input.slice(start);
            };
        });
})(window.angular);