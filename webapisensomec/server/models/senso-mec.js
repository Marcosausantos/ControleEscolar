'use strict';

module.exports = function(Sensomec) {

    Sensomec.EnviarAlunos = function (data, cb) {
        
        console.log('Entrou no WebService de Enviar Alunos.')
        console.log(data);
        Sensomec.create(data);

        var protocolo = new Date().getTime() / 1000
        console.log(protocolo);

        cb(null, protocolo);
    }

    Sensomec.remoteMethod('EnviarAlunos', {
        accepts: {arg: 'alunos', type: '[object]', required: true},
        returns: {arg: 'protocolo', type: 'string' }
    	
    });
};
