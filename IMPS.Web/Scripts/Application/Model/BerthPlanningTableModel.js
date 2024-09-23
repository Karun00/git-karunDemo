(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });

    var ReferenceData = function (data) {
        var self = this;
     //   self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item); }) : []);
        self.Quays = ko.observableArray(data ? $.map(data.Quays, function (item) { return new Quay(item); }) : []);
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.VesselStatus = ko.observable(data ? data.VesselStatus : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");       
       // self.FromDate = ko.observable(data ? data.FromDate : "");
        self.SelectedETA = ko.observable(data ? data.SelectedETA : "");
        self.ToDate = ko.observable(data ? data.ToDate : "");
        self.VesselStatuses = ko.observableArray(data ? $.map(data.VesselStatuses, function (item) { return new VesselStatus(item); }) : []);
    }
    
   //var Berth = function (data) {
   //     var self = this;
   //     self.BerthCode = ko.observable(data ? data.BerthCode : "");
   //     self.BerthName = ko.observable(data ? data.BerthName : "");
   // }

    var Quay = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.QuayName = ko.observable(data ? data.QuayName : "");    
    }

    var VesselStatus = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var ConfigurationModel = function (data) {
        var self = this;
        self.ConfigLabelName = ko.observable(data ? data.ConfigLabelName : "");
        self.ConfigName = ko.observable(data ? data.ConfigName : "");
        self.ConfigValue = ko.observable(data ? data.ConfigValue : "");
    }

    var BerthPlanningTableModel = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.FromBerth = ko.observable();
        self.ToBerth = ko.observable();       
        self.FromBollard = ko.observable();
        self.ToBollard = ko.observable();    
        self.ETB = ko.observable();
        self.ETUB = ko.observable();
        self.ATB = ko.observable(data ? moment(data.ATB).format('YYYY-MM-DD HH:mm') : "");     
        self.ATUB = ko.observable(data ? moment(data.ATUB).format('YYYY-MM-DD HH:mm') : "");
        self.ATUB = ko.observable();
        self.LengthOverallInM = ko.observable();
        self.Cargo = ko.observable();
        self.ReasonforVisit = ko.observable();
        self.FromBollard = ko.observable();
        self.ToBollard = ko.observable();
        self.BerthCode = ko.observable("");
        self.PortCode = ko.observable();
        self.Result = ko.observableArray(data ? data.Result : []);
        self.MooringBowBollard = ko.observable();
        self.MooringStemBollard = ko.observable();

     
       
        self.SelectedETA = ko.observable();
        self.ToDate = ko.observable();
        self.CargoTypeName = ko.observable();
        self.cache = function () { };
        self.set(data);

    }

    ipmsRoot.BerthPlanningTableModel = BerthPlanningTableModel;
    ipmsRoot.ReferenceData = ReferenceData;
    ipmsRoot.ConfigurationModel = ConfigurationModel;
 


}(window.IPMSROOT));


IPMSROOT.BerthPlanningTableModel.prototype.set = function (data) {
    var self = this;
    self.BerthCode(data ? (data.BerthCode || "") : "");
    self.FromBerth(data ? (data.FromBerth || "") : "");
    self.VCN(data ? (data.VCN || "") : "");
    self.MooringBowBollard(data ? (data.FromBerth || "") : "");
    self.MooringStemBollard(data ? (data.FromBerth || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.BerthPlanningTableModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}
