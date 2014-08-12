'use strict';

//returns an instance for calling the reads rest service
uiAngularServices.factory('ReadsService', ['$resource',
    function ($resource) {

        return $resource("/api/reads");
      
}]);

