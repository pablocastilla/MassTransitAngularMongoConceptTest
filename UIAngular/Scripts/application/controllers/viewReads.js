'use strict';

uiAngularControllers.controller('ViewReadsController', ['$scope','$rootScope','$http','ReadsService', function ($scope,$rootScope,$http,ReadsService) {
       
    $scope.reads = ReadsService.query();
   
    $scope.deleteRead = function (id) {     

        $http.delete("/api/reads/" + id);    
    };

    //if a readInsertionFinished is received the list is refreshed (rough solution, at the end this is an example) .
    var unbind = $rootScope.$on('readInsertionFinished', function () {
        $scope.reads = ReadsService.query();
    });

    $scope.$on('$destroy', unbind);
    
}]);