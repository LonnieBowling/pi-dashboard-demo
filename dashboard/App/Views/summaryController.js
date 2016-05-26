'use strict';
angular.module('portalApp')
.controller('summaryController', ['$scope','dataService', function ($scope, dataService) {
    $scope.summaryCount = '2018';
    $scope.todayProduction = 8;
    $scope.yesterdayProduction = 48;
    $scope.monthlyProduction = 475;
    $scope.dockedVessels = 16;

    $scope.init = function () {
        dataService.getKPIs().success(function (data) {
            $scope.productionDataSource = data;
        }).error(function (err) {
            $scope.productionDataSource = [];
        });
    };

    $scope.productionDataSource = [];

    $scope.production = {
        bindingOptions: {
            dataSource: 'productionDataSource'
        },
        commonSeriesSettings: {
            argumentField: 'Timestamp',
            type: 'bar'
        },
        series: [
            { valueField: 'Value', name: 'ore (tons)' }
        ],

    };
}]);