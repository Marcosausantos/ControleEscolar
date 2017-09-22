(function () {
    'use strict';

    angular
        .module('controleescolar')
        .directive('jaList', ['webjson', '$location', 'utils', 
            function (webjson, $location, utils) {
            return {
                scope: {
                    jaGridvm: '=jagridvm',
                    jaCurrents: '=jacurrents',
                    jaGridconf: '=jagridconf',
                    jaReadonly: '=readonly',
                    jaCustomRemove: '=jacustomremove',
                    jaCustomEditar: '=jacustomeditar',
                    jaCustomSalvar: '=jacustomsalvar',
                    jaCustomLer: '=jacustomler',                    
                    jaCustomCancelar: '=jacustomcancelar'
                },
                templateUrl:
                     function (elem, attr) {                         
                         return attr.template;                         
                     },
                link: function (scope, element, attrs) {

                    scope.utils = utils;

                    scope.consultagridpemissao = true;
                    scope.incluirgridpermissao = true;
                    scope.editgridpermissao = true;
                    scope.removepermissao = true;                    
                    
                    /************************/
                    /*         Grid         */
                    /************************/
                    scope.vsCarregarGrid = '';
                    scope.field = '';

                    //Carrega definição da grid
                    scope.carregadefinicaogrid = function () {                                                
                        webjson.getJson(scope.jaGridconf).then(function (items) {
                            scope.ConfController = items;

                            angular.forEach(scope.ConfController.grid, function (x) {
                                if (!x.readonly) {
                                    scope.vbPermitiEdit = true;
                                };

                                if (x.ordem == true)
                                    scope.field = x.nome;
                            });

                            if (scope.field == "")
                                scope.field = "Id";

                            scope.orderBy = { field: scope.field, asc: true };

                        }, function (status) {
                            scope.vsCarregarGrid = 'Não foi possível configurar a grid, entre contado com suporte.';
                            console.log(status);
                        });
                    };

                    scope.carregadefinicaogrid();

                    ////OrderBy   
                    scope.setOrderBy = function (field) {
                        var asc = scope.orderBy.field === field ? !scope.orderBy.asc : true;
                        scope.orderBy = { field: field, asc: asc };
                    };

                    ////Retorna se o type esteja readonly ou não
                    scope.getType = function (pColumn, pEditMode) {
                        if (pColumn.readonly || !pEditMode || pEditMode == null) {
                            return pColumn.type + "readonly";
                        }
                        else {
                            return pColumn.type;
                        };
                    };

                    //Retorna para grid sub item
                    scope.subitem = function (lscurrent, column) {

                        var vsColumn = column.substr(0, column.indexOf(".")).replace(".", "");
                        var vsSubitem = column.substr(column.indexOf("."), column.lenght).replace(".", "");
                        
                        var entidade = lscurrent[vsColumn];

                        if (entidade !== undefined && entidade !== null)
                            return entidade[vsSubitem];
                        else
                            return null;
                    };

                    /************************/
                    /*       Operação       */
                    /************************/
  
                    //Editar grid
                    scope.jaGridvm.editgrid = function (pCurrent, pReadonly) {                        
                        if (scope.vbPermitiEdit) {
                            scope.OldEntidade = angular.copy(pCurrent);
                            pCurrent.editMode = !pCurrent.editMode;
                        }
                        else {
                            if (pReadonly)
                               scope.utils.gopath(scope.jaGridvm.controller + '/' + pCurrent.Id + '/readonly')
                            else
                               scope.utils.gopath(scope.jaGridvm.controller + '/' + pCurrent.Id)
                        };                        
                    };                    

                    //Salva registro via grid
                    scope.jaGridvm.savegrid = function (pCurrent) {                        
                        pCurrent.editMode = !pCurrent.editMode;                        
                    };

                    scope.jaGridvm.removegrid = function (pCurrent) {
                        scope.jaGridvm.remove(pCurrent.Id);                        
                    };

                    //Cancelar a edição da grid
                    scope.jaGridvm.cancelgrid = function (pCurrent) {
                        pCurrent.editMode = !pCurrent.editMode;                        
                    };

                    //paginacao
                    scope.itemsPerPage = 10;
                    scope.currentPage = 0;

                    scope.range = function () {
                        var rangeSize = 5;
                        var ret = [];
                        var start;
                        var count = 0;

                        start = scope.currentPage;
                        count = scope.pageCount();
                        if (start > rangeSize - count) {
                            start = rangeSize - count + 1;
                        }
                        
                        for (var i = start; i < start + rangeSize; i++) {
                            ret.push(i);
                        }

                        return ret;
                    };

                    scope.prevPage = function () {
                        if (scope.currentPage > 0) {
                            scope.currentPage--;
                        }
                    };

                    scope.prevPageDisabled = function () {
                        return scope.currentPage === 0 ? "disabled" : "";
                    };

                    scope.pageCount = function () {
                        //return Math.ceil(scope.jaGridvm.currents.length / scope.itemsPerPage) - 1;
                        if ((scope.jaGridvm.currents.length / scope.perPage) > Math.floor(Math.round(scope.jaGridvm.currents.length / scope.perPage))) {
                            return Math.floor(Math.round(scope.jaGridvm.currents.length / scope.perPage)) + 1;
                        }
                        else {
                            return Math.floor(Math.round(scope.jaGridvm.currents.length / scope.perPage));
                        }
                    };

                    scope.nextPage = function () {
                        if (scope.currentPage < scope.pageCount()) {
                            scope.currentPage++;
                        }
                    };

                    scope.nextPageDisabled = function () {
                        return scope.currentPage === scope.pageCount() ? "disabled" : "";
                    };

                    scope.setPage = function (n) {
                        scope.currentPage = n;
                    };
                }
            };
        }]);
})(window.angular);
