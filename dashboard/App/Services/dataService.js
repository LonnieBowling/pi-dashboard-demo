'use strict';
angular.module('portalApp')
.factory('dataService', ['$http', function ($http) {
    return {
        getKPIs: function () {
            return $http.get('/kpi/getKPIs');
        }
    };
}]);