(function (ipmsRoot) {

    var VesselSAPPostingModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);

        self.VslSrchOn = ko.observable("");
        self.SearchText = ko.observable("");
        self.IsVesselSeened = ko.observable("");

        self.VesselID = ko.observable("");
        self.VesselName = ko.observable("");
        self.IMONo = ko.observable("");
        self.VesselType = ko.observable("");
        self.VesselTypeName = ko.observable("");
        self.LengthOverallInM = ko.observable("");
        self.BeamInM = ko.observable("");
        self.CallSign = ko.observable("");
        self.VesselNationality = ko.observable("");
        self.GrossRegisteredTonnageInMT = ko.observable("");
        self.DeadWeightTonnageInMT = ko.observable("");
        self.SAPAccountNo = ko.observable("");
        self.sappoststatus = ko.observable("");
        self.VSNAME = ko.observable("");

        self.TransmitData = ko.observable("");

       // self.VCN = ko.observable();
        self.SAPReferenceNo = ko.observable();
        self.Remarks = ko.observable();
        self.IsRepost = ko.observable();

        //self.VCNSort;
        //self.VCN.subscribe(function (value) {
        //    self.VCNSort = value;
        //});

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.SAPReferenceNoSort;
        self.SAPReferenceNo.subscribe(function (value) {
            self.SAPReferenceNo = value;
        });

        self.IMONoSort;
        self.IMONo.subscribe(function (value) {
            self.IMONoSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.VesselSAPPostingModel = VesselSAPPostingModel;
}(window.IPMSROOT));

IPMSROOT.VesselSAPPostingModel.prototype.set = function (data) {
    var self = this;

    self.VslSrchOn(data ? (data.VslSrchOn || "") : "");
    self.SearchText(data ? (data.SearchText || "") : "");
    self.IsVesselSeened("02");

    self.VesselName(data ? (data.VesselName || "") : "");
    self.VesselID(data ? (data.VesselID || "") : "");
    self.IMONo(data ? (data.IMONo || "") : "");
    self.VesselType(data ? (data.VesselType || "") : "");
    self.VesselTypeName(data ? (data.VesselTypeName || "") : "");
    self.LengthOverallInM(data ? (data.LengthOverallInM || "") : "");
    self.BeamInM(data ? (data.BeamInM || "") : "");
    self.CallSign(data ? (data.CallSign || "") : "");
    self.VesselNationality(data ? (data.VesselNationality || "") : "");
    self.GrossRegisteredTonnageInMT(data ? (data.GrossRegisteredTonnageInMT || "") : "");
    self.DeadWeightTonnageInMT(data ? (data.DeadWeightTonnageInMT || "") : "");
    self.SAPAccountNo(data ? (data.SAPAccountNo || "") : "");
    self.TransmitData(data ? (data.TransmitData || "") : "");
    self.sappoststatus(data ? (data.sappoststatus || "") : "");
    self.VSNAME(data ? (data.VSNAME || "") : "");

 //   self.VCN(data ? (data.VCN || "") : "");
    self.SAPReferenceNo(data ? (data.SAPReferenceNo || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.IsRepost(data ? (data.IsRepost || "") : "");


    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.VesselSAPPostingModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



