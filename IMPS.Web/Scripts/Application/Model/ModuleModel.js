/// <reference path="../ViewModel/PortViewModel.js" />
(function (ipmsRoot) {
    var ModuleModel = function () {
        var self = this;
        self.ModuleList = ko.observableArray([]);
        self.ModuleName  = ko.observable();
        self.Module1 = ko.observable();
        self.Entities = ko.observableArray([]);
      
    }
    ipmsRoot.ModuleModel = ModuleModel;
}(window.IPMSROOT));

