(function (IPMSRoot) {

    var ModuleViewModel = function () {
        var self = this;
        $('#spnTitile').html("Module Master");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.ModuleModel = ko.observable();
        self.ModuleList = ko.observableArray();

        self.initialize = function () {
            self.viewMode = ko.observable(true);
            self.ModuleModel(new IPMSROOT.ModuleModel());
            self.LoadModule();
            self.viewMode('List');
        }
    }
    IPMSRoot.ModuleViewModel = ModuleViewModel;


}(window.IPMSROOT));