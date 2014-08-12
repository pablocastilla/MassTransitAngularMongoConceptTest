
uiAngularServices.service('SignalRHubService', function ($, $rootScope) {
    var proxy = null;

    var initialize = function () {
        //Getting the connection object
        connection = $.hubConnection();

        //Creating proxy
        this.proxy = connection.createHubProxy('nonPersistentHub');

        //Starting connection
        connection.start();

        //Publishing an event when server pushes a readInsertionFinished message
        this.proxy.on('readInsertionFinished', function (msg) {
            $rootScope.$emit("readInsertionFinished", { message: msg });
        });
    };

    
    return {
        initialize: initialize
       
    };
});
