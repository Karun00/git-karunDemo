(function (IPMSRoot) {

    var PlannedMovementsViewModel = function () {

        var self = this;

        self.viewModelHelper = new IPMSRoot.viewModelHelper();
        self.viewMode = ko.observable();
        self.plannedMovementsModel = ko.observable();
        self.PlannedMovementsList = ko.observableArray();

        //Planned Movements Initialization(pageload) mode
        self.Initialize = function () {
            self.plannedMovementsModel(new IPMSROOT.PlannedMovementsModel());
            self.LoadPlannedMovements();
            self.viewMode('List');
        }

        self.LoadPlannedMovements = function () {
            // Proxy created on the fly
            var chat = $.connection.chatHub;

            // Declare a function on the job hub so the server can invoke it
            chat.client.BrodcastNews = function () {
                self.LoadPlannedMovementsList();
            };

            // Start the connection
            $.connection.hub.start().done(function () {
                self.LoadPlannedMovementsList();
                chat.server.show();
            });
        }
        
        //LoadPlannedMovements fetches the data from API Service
        self.LoadPlannedMovementsList = function () {
            self.viewModelHelper.apiGet('api/Mobile/GetPlannedMovements', null, function (result) {
                self.PlannedMovementsList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.PlannedMovementsModel(item);
                }));
            }, null, null, false);
        }

        self.Initialize();
    }

    IPMSRoot.PlannedMovementsViewModel = PlannedMovementsViewModel;

}(window.IPMSROOT));
