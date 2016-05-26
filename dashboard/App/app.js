'use strict';
angular.module('portalApp', ['ngRoute', 'dx'])
.config(['$routeProvider', function ($routeProvider) {

    $routeProvider.when('/Summary', {
        controller: 'summaryController',
        templateUrl: '/App/Views/summaryView.html',
    }).when('/Production', {
        controller: 'productionController',
        templateUrl: '/App/Views/productionView.html',
    }).otherwise({ redirectTo: '/Summary' });

}]);

