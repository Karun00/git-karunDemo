(function (ipmsRoot) {

    var SuppMiscReferenceData = function (data) {
        var self = this;
        self.MiscServiceTypes = ko.observableArray(data ? $.map(data.MiscServiceTypes, function (item) { return new MiscServiceType(item); }) : []);
        self.PhaseTypes = ko.observableArray(data ? $.map(data.PhaseTypes, function (item) { return new PhaseType(item); }) : []);   
    }

    var MiscServiceType = function (data) {
        var self = this;
        self.ServiceTypeID = ko.observable(data ? data.ServiceTypeID : "");
        self.ServiceTypeName = ko.observable(data ? data.ServiceTypeName : "");
        self.ServiceTypeCode = ko.observable(data ? data.ServiceTypeCode : "");        
    }

    var PhaseType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var SuppMiscRecordingModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
        self.ServiceTypeID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Service Type' } });
        self.Phase = ko.observable("");//commented and added validation in viewmodel by divya on 31Oct2017
        self.FromDateTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select From Date' } });
        self.ToDateTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select To Date' } });        
        self.Quantity = ko.observable("")//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please enter Quantity' } });//commented and added validation in viewmodel by divya on 17Nov2017
        //Added by divya on 30 Oct2017
        self.StartMeterReading = ko.observable("");
        self.EndMeterReading = ko.observable("");
        //end
        self.UOMCode = ko.observable("");
        self.Remarks = ko.observable("");
        self.SuppDryDockID = ko.observable("");
        self.ServiceTypeName = ko.observable("");
        self.ServiceTypeCode = ko.observable("");

       
        
    }

    var SuppMiscServiceModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.SuppMiscServiceID = ko.observable("");
        self.SuppDryDockID = ko.observable("");
        self.VCN = ko.observable("");
        self.VesselName = ko.observable();
        self.VesselAgent = ko.observable();
       
        self.DockPortCode = ko.observable();
        self.FromDate = ko.observable("");
        self.ToDate = ko.observable("");
        self.ServiceTypeID = ko.observable("");
        self.ServiceTypeName = ko.observable("");
        self.Phase = ko.observable("");
        self.FromDateTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select From Date' } });
        self.ToDateTime = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select To Date' } });
        self.Quantity = ko.observable("")//.extend({ required: { onlyIf: self.validationEnabled, message: '* Please enter Quantity' } });removed by divya on 17Nov2017
       //Added by divya on 30thOCt2017
        self.StartMeterReading = ko.observable("");
        self.EndMeterReading = ko.observable("");
        //end
        self.UOMCode = ko.observable("");
        self.Remarks = ko.observable("");
        self.BerthName = ko.observable("");
        self.LeftDockDateTime = ko.observable("NA");
              

        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.ServiceTypeCode = ko.observable("");

        self.ScheduleFromDate = ko.observable();
        self.ScheduleToDate = ko.observable();
        self.ExtensionDateTime = ko.observable();

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });
        self.RequestFromSort;
        self.FromDate.subscribe(function (value) {
            self.RequestFromSort = value;
        });
        self.RequestToSort;
        self.ToDate.subscribe(function (value) {
            self.RequestToSort = value;
        });

        self.MiscServiceSort;
        self.ServiceTypeName.subscribe(function (value) {
            self.MiscServiceSort = value;
        });

        self.FromDateSort;
        self.FromDateTime.subscribe(function (value) {
            self.FromDateSort = value;
        });

        self.ToDateSort;
        self.ToDateTime.subscribe(function (value) {
            self.ToDateSort = value;
        });


        self.QuantitySort;
        self.Quantity.subscribe(function (value) {
            self.QuantitySort = value.toString();
        });
        //Added by divya on 30Oct2017
        self.StartMeterReadingSort;
        self.StartMeterReading.subscribe(function (value) {
            self.StartMeterReadingSort = value.toString();
        });
        self.EndMeterReadingSort;
        self.EndMeterReading.subscribe(function (value) {
            self.EndMeterReadingSort = value.toString();
        });


        self.UOMSort;
        self.UOMCode.subscribe(function (value) {
            self.UOMSort = value;
        });


        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.SuppMiscServiceModel = SuppMiscServiceModel;
    ipmsRoot.SuppMiscReferenceData = SuppMiscReferenceData;
    ipmsRoot.SuppMiscRecordingModel = SuppMiscRecordingModel;
}(window.IPMSROOT));

IPMSROOT.SuppMiscServiceModel.prototype.set = function (data) {
    debugger;
    var self = this;  


    self.SuppDryDockID(data ? data.SuppDryDockID : 0);
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");
    self.DockPortCode(data ? data.DockPortCode : "");
    self.FromDate(data ? data.FromDate : "");
    self.ToDate(data ? data.ToDate : "");
    self.VesselAgent(data ? data.VesselAgent : "");

    self.SuppMiscServiceID(data ? data.SuppMiscServiceID : 0);
    self.ServiceTypeID(data ? (data.ServiceTypeID || "") : "");
    self.ServiceTypeName(data ? (data.ServiceTypeName || "") : "");
    self.Phase(data ? data.Phase : null);
    self.FromDateTime(data ? data.FromDateTime : "");
    self.ToDateTime(data ? data.ToDateTime : "");
    self.Quantity(data ? data.Quantity : "");   
    self.UOMCode(data ? data.UOMCode : "");
    self.Remarks(data ? data.Remarks : "");
    self.BerthName(data ? data.BerthName : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.LeftDockDateTime(data ? data.LeftDockDateTime : "NA");    
    self.ServiceTypeCode(data ? data.ServiceTypeCode : "");
    self.StartMeterReading(data ? data.StartMeterReading : "");
    self.EndMeterReading(data ? data.EndMeterReading : "");

    self.ScheduleFromDate(data ? (moment(data.ScheduleFromDate).format('YYYY-MM-DD HH:mm') || "") : null);
    self.ScheduleToDate(data ? (moment(data.ScheduleToDate).format('YYYY-MM-DD HH:mm') || "") : null);
    
    self.ExtensionDateTime(data ? (data.ExtensionDateTime != null || data.ExtensionDateTime != undefined ? moment(data.ExtensionDateTime).format('YYYY-MM-DD HH:mm') : null || null) : null);
   


    
    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.SuppMiscServiceModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}


