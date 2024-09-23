(function (IPMSRoot) {

    var AgentDetailsViewModel = function () {

        var self = this;
       
        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.agentModel = ko.observable();
        self.AgentsList = ko.observableArray();
        self.ApplicantAddressList = ko.observableArray();
        self.ApplicantAuthContList=ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsSaveUpdateDisabled = ko.observable(true);


        self.ViewUrl = function (model) {
            //if (confirm("Are You Sure you Want To Delete Quay(" + model.QuayID() + ")")) {
                /*self.viewModelHelper.apiPost('api/Quay/DeletequayData', ko.mapping.toJS(model), function () { alert("Quay Deleted") });*/
            self.viewModelHelper.apiGet('api/Agent/GetAgent' + ko.mapping.toJSON(model.ApplicantID),
               null,
                  function (result) {
                      self.AgentsList.remove(model);
                  });
            //}
        }

        self.Initialize();
    }
    IPMSRoot.AgentDetailsViewModel = AgentDetailsViewModel;
}(window.IPMSROOT));