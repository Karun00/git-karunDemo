(function (ipmsRoot) {

    var VesselCallReason = function (data) {
        var self = this;

        self.Reasons = ko.observableArray(data ? $.map(data, function (item) { return new ReasonCombo(item); }) : []);
       
    }
    var ReasonCombo = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    
    //Knockout variable has been assgined to maintain in the Viewmodel
    var VesselCallAnchorageModel = function (data) {
        var self = this;
        self.VesselCallID = ko.observable();
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.IMONo = ko.observable();
        self.CallSign = ko.observable();
        self.ATA = ko.observable();
        
        self.ATD = ko.observable();
        self.PortLimitIn = ko.observable();
        self.PortLimitOut = ko.observable();
        self.ATB = ko.observable();
        self.ATUB = ko.observable();

        self.BreakWaterIn = ko.observable();
        self.BreakWaterOut = ko.observable();
        self.AnchorDropTime = ko.observable();
        self.ETA = ko.observable();
        self.ETD = ko.observable();
        self.RecentAgentID = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.StartDate = ko.observable();
        self.AnyDangerousGoodsonBoard = ko.observable();
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        self.VesselStatus = ko.observable(data ? data.VesselStatus : "");
        self.VesselCallAnchorages = ko.observableArray(data ? (data.VesselCallAnchorages || null) : null);


        //AdvanceSearch

        self.ETAFrom = ko.observable();
        self.ETATo = ko.observable();
        self.VCNSearch = ko.observable('');
        self.VesselNameSearch = ko.observable('');
        self.VCNSelected = ko.observable('');
        self.VesselNameSelected = ko.observable('');
        var todaydate = new Date();
        var todate = new Date(todaydate);
        var fromdate = new Date(todaydate);
        todate.setDate(todaydate.getDate() + 30);
        fromdate.setDate(fromdate.getDate() - 30);
        self.ETAFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
        self.ETATo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");

        // -- end


        //////// KendoUI Grid Sorting ////////
        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.ATASort;
        self.ATA.subscribe(function (value) {
            self.ATASort = value;
        });

        self.AnchorDropTimeSort;
        self.AnchorDropTime.subscribe(function (value) {
            self.AnchorDropTimeSort = value;
        });
        self.ATDSort;
        self.ATD.subscribe(function (value) {
            self.ATDSort = value;
        });
        self.StatusSort;
        self.VesselStatus.subscribe(function (value) {
            self.StatusSort = value;
        });
        //////////////////////////////////////////
        self.cache = function () { };
        self.set(data);
    }

    var VesselCallAnchorage = function (data) {
        var self = this;
        self.VesselCallAnchorageID = ko.observable(data ? data.VesselCallAnchorageID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.AnchorDropTime = ko.observable(data ? (data.AnchorDropTime != null ? moment(data.AnchorDropTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");
        self.AnchorAweighTime = ko.observable(data ? (data.AnchorAweighTime != "" ? moment(data.AnchorAweighTime).format('YYYY-MM-DD HH:mm') : "" || "") : "");
        self.AnchorPosition = ko.observable(data ? data.AnchorPosition : "");
        self.BearingDistanceFromBreakWater = ko.observable(data ? data.BearingDistanceFromBreakWater : "");
        self.Reason = ko.observable(data ? data.Reason : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "A");
        self.ATB = ko.observable(data ? data.ATB : "");
        self.ATUB = ko.observable(data ? data.ATUB : "");
    }

    ipmsRoot.VesselCallReason = VesselCallReason;
    ipmsRoot.ReasonCombo = ReasonCombo;
    ipmsRoot.VesselCallAnchorage = VesselCallAnchorage;
    ipmsRoot.VesselCallAnchorageModel = VesselCallAnchorageModel;

}(window.IPMSROOT));

//Initial Value for resetting the unchanged or before saved values
IPMSROOT.VesselCallAnchorageModel.prototype.set = function (data) {

    var self = this;
    self.VCN(data ? (data.VCN || "") : "");
    self.VesselName(data && data.Vessel != undefined ? (data.Vessel.VesselName || "") : "");
    self.IMONo(data && data.Vessel != undefined ? (data.Vessel.IMONo || "") : "");
    self.CallSign(data && data.Vessel != undefined ? (data.Vessel.CallSign || "") : "");
    self.ATA(data ? (data.ATA || "") : "");
    self.ATD(data ? (data.ATD || "") : "");
    self.PortLimitIn(data ? (data.PortLimitIn != null ? moment(data.PortLimitIn).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.PortLimitOut(data ? (data.PortLimitOut != null ? moment(data.PortLimitOut).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.BreakWaterIn(data ? (data.BreakWaterIn != null ? moment(data.BreakWaterIn).format('YYYY-MM-DD HH:mm') : "" || "") : "");
    self.BreakWaterOut(data ? (data.BreakWaterOut != null ? moment(data.BreakWaterOut).format('YYYY-MM-DD HH:mm') : "" || "") : "");
   
    self.VCNSearch(data ? (data.VCNSearch || "") : "");
    self.VesselNameSearch(data && data.VesselNameSearch != undefined ? (data.VesselNameSearch || "") : "");
    self.VCNSelected(data ? (data.VCNSelected || "") : "");
    self.VesselNameSelected(data && data.VesselNameSelected != undefined ? (data.VesselNameSelected || "") : "");
    self.ATB(data ? (data.ATB || "") : "");
    self.ATUB(data ? (data.ATUB || "") : "");
    self.ETA(data ? (data.ETA || "") : "");
    self.ETD(data ? (data.ETD || "") : "");
    self.RecentAgentID(data ? (data.RecentAgentID || "") : "");
    self.VesselCallID(data && data.VesselCallID != undefined ? (data.VesselCallID || "") : "");
    self.CreatedBy(data ? (data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate || "") : "");
    self.VesselCallAnchorages(data ? ko.utils.arrayMap(data.VesselCallAnchorages, function (anchorages) { return new IPMSROOT.VesselCallAnchorage(anchorages); }) : []);
    self.AnyDangerousGoodsonBoard(data && data.ArrivalNotification != null && data.ArrivalNotification.AnyDangerousGoodsonBoard != null ? data.ArrivalNotification.AnyDangerousGoodsonBoard : "N")
    self.cache.latestData = data;
}

IPMSROOT.VesselCallAnchorageModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


//Accept only numeric 
function ValidateNumeric(data, event) {
    if (event.which == 9 || event.which == 0)
        return true;
    else {
        if (window.event) // IE
            keynum = event.keyCode;
        else if (event.which) // Netscape/Firefox/Opera
            keynum = event.which;
        keychar = String.fromCharCode(keynum);
        charcheck = /[0-9\b]/;
        return charcheck.test(keychar);
    }
}

