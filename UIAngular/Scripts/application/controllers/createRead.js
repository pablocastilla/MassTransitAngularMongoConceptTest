'use strict';

uiAngularControllers.controller('CreateReadController', ['$scope', '$rootScope', '$http', 'ReadsService', function ($scope, $rootScope,$http, ReadsService) {

    $rootScope.title = 'Create Read';

    $scope.newRead = { 
        SerialNumber: "sn66",
        ReadTimeStamp: "2014-04-10",
        Value: "33"
    };

    $scope.serviceRequestProgress={
        progress: 0,
        text: "not launched"
    };

    $scope.currentRequest = null;

    //this function is invoked to launch a command
    $scope.saveNewRead = function () {
        
        $scope.serviceRequestProgress = {
            progress: 33,
            text: "calling server"
        };

        var newRead = ReadsService.save($scope.newRead,function(putResponseHeaders){
           
            $scope.currentRequest = putResponseHeaders.TransactionID;
          
            $scope.serviceRequestProgress = {
                progress: 66,
                text: "server calling succeed"
            };
        });
       
    };


    var unbind = $rootScope.$on('readInsertionFinished', function (event, args) {
        
        if (args.message == $scope.currentRequest)
        {
            $scope.serviceRequestProgress = {
                progress: 100,
                text: "read successfully inserted"
            }
        }
        

        $scope.reads = ReadsService.query();
    });

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'shortDate'];
    $scope.format = $scope.formats[0];


    
}]);