(function () {
    'use strict';

    angular
        .module('controleescolar')
        .config(configValidator);

    configValidator.$inject = ['$validationProvider'];

    function configValidator($validationProvider) {

        var defaultMsg,
            expression;

        /**
         * Setup a default message for Url
         */
        defaultMsg = {
            url: {
                error: 'Url obrigartória',
                success: 'It\'s Url'
            }
        };

        var defaultMsg = {
            required: {
                error: " <font color=\"red\">Campo obrigatório</font>",
                success: ''
            },
            url: {
                error: 'Url inválida',
                success: ''
            },
            email: {
                error: 'E-mail inválido',
                success: ''
            },
            number: {
                error: 'Valor numérico inválido',
                success: ''
            }
        };

        $validationProvider.setDefaultMsg(defaultMsg);


        /**
         * Setup a new Expression and default message
         * In this example, we setup a IP address Expression and default Message
         */
        expression = {
            ip: /^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/
        };

        defaultMsg = {
            ip: {
                error: 'This isn\'t ip address',
                success: 'It\'s ip'
            }
        };

        $validationProvider.setExpression(expression)
                           .setDefaultMsg(defaultMsg);

        // or we can just setup directly
        $validationProvider.setDefaultMsg({ ip: { error: 'This no ip', success: 'this ip' } })
                           .setExpression({ ip: /^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/ });

        /**
         * Additions validation
         */
        $validationProvider
            .setExpression({
                huei: function (value, scope, element, attrs) {
                    return value === 'Huei Tan';
                }
            })
            .setDefaultMsg({
                huei: {
                    error: 'This should be Huei Tan',
                    success: 'Thanks!'
                }
            });

        /**
         * Range Validation
         */
        $validationProvider
            .setExpression({
                range: function (value, scope, element, attrs) {
                    if (value >= parseInt(attrs.min) && value <= parseInt(attrs.max)) {
                        return value;
                    }
                }
            })
            .setDefaultMsg({
                range: {
                    error: 'Number should between 5 ~ 10',
                    success: 'good'
                }
            });


        /**
        * Range Validation
        */
        $validationProvider
            .setExpression({
                field: function (value, scope, element, attrs) {
                    if (value >= parseInt($(attrs.selector).val())) {
                        return value;
                    }
                }
            })
            .setDefaultMsg({
                field: {
                    error: 'Valor atual deve ser maior que',
                    success: 'good'
                }
            });
    }
})(window.angular);
