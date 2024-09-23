(function (ipmsRoot) {

    var DateFormat = 'YYYY-MM-DD HH:mm';

    var vesselModel = function (data) {
        var self = this;

        self.Vessels = ko.observableArray(data ? $.map(data, function (item1) { return new vesselDetail(item1); }) : []);
    }

    //Getting all the details of ArrivalNotification Based on VCN Number
    var vesselDetail = function (data) {

        var self = this;

        self.VCN = ko.observable(data ? data.VCN : "");
        self.ReasonForVisit = ko.observable(data ? data.ReasonForVisit : "");
        self.ETA = ko.observable(data ? data.ETA : "");
        self.ETD = ko.observable(data ? data.ETD : "");
        self.ArrDraft = ko.observable(data ? data.ArrDraft : "");
        self.LastPortOfCall = ko.observable(data ? data.LastPortOfCall : "");
        self.NextPortOfCall = ko.observable(data ? data.NextPortOfCall : "");
        self.Tidal = ko.observable(data ? (data.Tidal != "I" ? "Yes" : "No") : "");
        self.DaylightRestriction = ko.observable(data ? (data.DaylightRestriction != "I" ? "Yes" : "No") : "No");
        self.VoyageIn = ko.observable(data ? data.VoyageIn : "");
        self.VoyageOut = ko.observable(data ? data.VoyageOut : "");
        self.VesselName = ko.observable(data ? (data.Vessel ? data.Vessel.VesselName : data.VesselName) : "");
        self.VesselType = ko.observable(data ? (data.Vessel ? data.Vessel.VesselType : data.VesselType) : "");
        self.CallSign = ko.observable(data ? (data.Vessel ? data.Vessel.CallSign : data.CallSign) : "");
        self.IMONo = ko.observable(data ? (data.Vessel ? data.Vessel.IMONo : data.IMONo) : "");
        self.LengthOverallInM = ko.observable(data ? (data.Vessel ? data.Vessel.LengthOverallInM : data.LengthOverallInM) : "");
        self.BeamInM = ko.observable(data ? (data.Vessel ? data.Vessel.BeamInM : data.BeamInM) : "");
        self.VesselNationality = ko.observable(data ? (data.Vessel ? data.Vessel.VesselNationality : data.VesselNationality) : "");
        self.GrossRegisteredTonnageInMT = ko.observable(data ? (data.Vessel ? data.Vessel.GrossRegisteredTonnageInMT : data.GrossRegisteredTonnageInMT) : "");
        self.DeadWeightTonnageInMT = ko.observable(data ? (data.Vessel ? data.Vessel.DeadWeightTonnageInMT : data.DeadWeightTonnageInMT) : "");
        self.AnyDangerousGoodsonBoard = ko.observable(data ? (data.AnyDangerousGoodsonBoard == "Yes" ? "Yes" : "No") : "No");
        self.DangerousGoodsClass = ko.observable(data ? ((data.DangerousGoodsClass != "" && data.DangerousGoodsClass != null) ? data.DangerousGoodsClass : "NA") : "NA");
        self.UNNo = ko.observable(data ? ((data.UNNo != "" && data.UNNo != null) ? data.UNNo : "NA") : "NA");
        self.CurrentBerth = ko.observable(data ? data.CurrentBerth : "NA");
        self.ETB = ko.observable(data ? data.ETB : "NA");
        self.ETUB = ko.observable(data ? data.ETUB : "NA");
    }

    //Service Type DDL
    var ServiceType = function (data) {
        var self = this;

        self.serviceTypeList = ko.observableArray(data ? $.map(data, function (item1) { return new serviceTypeDetail(item1); }) : []);
    }
    //Service details binding
    var serviceTypeDetail = function (data) {
        var self = this;

        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    //Document Type DDL
    var DocumentType = function (data) {
        var self = this;

        self.documentTypeList = ko.observableArray(data ? $.map(data, function (item1) { return new documentTypeDetail(item1); }) : []);
    }
    //Documetn details binding
    var documentTypeDetail = function (data) {

        var self = this;

        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //Locations DDL
    var Locations = function (data) {
        var self = this;

        self.locationList = ko.observableArray(data ? $.map(data, function (item1) { return new locationDetail(item1); }) : []);
    }
    //Location details binding
    var locationDetail = function (data) {
        var self = this;

        self.LocationID = ko.observable(data ? data.LocationID : "");
        self.LocationName = ko.observable(data ? data.LocationName : "");
    }

    //Berth DDL
    var Berths = function (data) {
        var self = this;

        self.berthList = ko.observableArray(data ? $.map(data, function (item1) { return new berthDetail(item1); }) : []);
    }

    //Berth details binding
    var berthDetail = function (data) {
        var self = this;

        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
    }



    //Hot, Cold and HotAndCold Work Permit Service Binding
    var SuppHotColdWorkPermit = function (data) {

        var self = this;

        self.SuppHotColdWorkPermitID = ko.observable(data ? data.SuppHotColdWorkPermitID : "");
        self.SuppServiceRequestID = ko.observable(data ? data.SuppServiceRequestID : "");

        self.GassFreeCertificateAvailable = ko.observable(data ? data.GassFreeCertificateAvailable : "N");
        self.GassFreeCertificateValidity = ko.observable(data ? moment(data.GassFreeCertificateValidity).format(DateFormat) : "");
        self.GassFreeIssuingAuthority = ko.observable(data ? data.GassFreeIssuingAuthority : "");
        self.LocationID = ko.observable(data ? data.LocationID : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : "");
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : "");
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
        self.OtherLocation = ko.observable(data ? data.OtherLocation : "");

        self.SuppHotColdWorkPermitDocumentsVO = ko.observableArray(data ? ko.utils.arrayMap(data.SuppHotColdWorkPermitDocumentsVO, function (document) {
            return new SuppHotColdWorkPermitDocument(document);
        }) : []);
    }
    var IMDGDetails = function (data) {
        var self = this;
        self.ClassCode = ko.observable(data ? data.ClassCode : "");
        self.UNNo = ko.observable(data ? data.UNNo : "");

    }
    //Floating Cranes Binding
    //Services to be Performed dynamic rows
    var SuppFloatingCranes = function (data) {
        var self = this;

        self.SuppFloatingCraneID = ko.observable(data ? data.SuppFloatingCraneID : "");
        self.SuppServiceRequestID = ko.observable(data ? data.SuppServiceRequestID : "");
        self.MassMT = ko.observable(data ? data.MassMT : "");
        self.Quantity = ko.observable(data ? data.Quantity : "");
        self.Description = ko.observable(data ? data.Description : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : "");
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : "");
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
    }

    //Floating Cranes Binding
    //Quantities of Commodity Getting from  Database table of ArrivalCommodity Table
    var ArrivalCommodity = function (data) {

        var self = this;

        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.CargoType = ko.observable(data ? data.CargoType : "");
        self.Package = ko.observable(data ? data.Package : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.CargoName = ko.observable(data ? data.CargoName : "");
        self.PackageName = ko.observable(data ? data.PackageName : "");
        self.UOMName = ko.observable(data ? data.UOMName : "");
        self.Quantity = ko.observable(data ? data.Quantity : "");
    }

    //Hot, Cold and HotAndCold Work Permit Serivce
    //Uploaded Documents
    var SuppHotColdWorkPermitDocument = function (data) {
        var self = this;

        self.SuppHotColdWorkPermitDocumentID = ko.observable(data ? data.SuppHotColdWorkPermitDocumentID : '');
        self.SuppHotColdWorkPermitID = ko.observable(data ? data.SuppHotColdWorkPermitID : '');
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.FileName = ko.observable(data ? data.FileName : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
        self.DocumentTypeName = ko.observable(data ? data.DocumentTypeName : "");
    }

    var SuppServiceRequestModelGrid = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.VesselID = ko.observable();
        //self.MovementType = ko.observable();

        self.RequestFrom = ko.observable();
        self.RequestTo = ko.observable();
        self.set(data);
    }

    var SupplementaryServiceRequestModel = function (data) {
        var self = this;
        self.SuppServiceRequestID = ko.observable();
        self.VesselData = ko.observable();
        self.IsValidationEnabled = ko.observable(false);
        self.VCN = ko.observable("").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please select the VCN.' } });
        self.VesselName = ko.observable();
        self.AnyDangerousGoods = ko.observable();
        self.ServiceTypeName = ko.observable();
        self.ServiceType = ko.observable(data ? data.ServiceType : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please select the service type.' } });
        self.DocumentType = ko.observable(data ? data.DocumentType : "");
        self.WorkflowInstanceID = ko.observable();
        self.PortCode = ko.observable("");
        self.BerthKey = ko.observable();
        self.FromDate = ko.observable();
        self.Quantity = ko.observable();
        self.Remarks = ko.observable();
        self.ToDate = ko.observable();
        self.DDCargo = ko.observable();
        self.DDBallast = ko.observable();
        self.DDBunkers = ko.observable();

        self.SuppHotColdWorkPermitsVO = ko.observable(data ? data.SuppHotColdWorkPermitsVO : "");
        self.SuppFloatingCranesVO = ko.observableArray(data ? data.SuppFloatingCranesVO : []);

        self.ArrivalCommodities = ko.observableArray(data ? data.ArrivalNotification.ArrivalCommodities : []);
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.TermsandConditions = ko.observable();
        self.WFStatus = ko.observable();
        self.WFStatusSupp = ko.computed(function () {
            return self.WFStatus() == 'Approve' ? "Approved" : self.WFStatus() == 'Reject' ? "Rejected" : self.WFStatus() == 'Confirm' ? "Confirmed" : self.WFStatus() == 'Cancel' ? "Cancelled" : self.WFStatus() == 'Update Request' ? "Update Request" : "New Request";
        });

        self.IsStartTime = ko.observable();

        self.WorkFlowRemarks = ko.observable();


        ////////////////KendoUI Grid Sorting/////////
        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });
        self.ServiceTypeSort;
        self.ServiceTypeName.subscribe(function (value) {
            self.ServiceTypeSort = value;
        });
        self.RequestDtAndTimeSort;
        self.FromDate.subscribe(function (value) {
            self.RequestDtAndTimeSort = value;
        });

        self.StatusSort;
        self.WFStatus.subscribe(function (value) {
            self.StatusSort = value;
        });

        self.ETB = ko.observable();
        self.ETUB = ko.observable();
        self.IsPrimaryAgent = ko.observable();

        ///////////////////////////////////////////////////


        self.UploadedFiles = ko.observableArray([]);

        self.cache = function () { };
        self.set(data);

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);
    }


    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Please enter remarks.' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");


    }


    var SupplementaryGridDetails = function (data) {

        var self = this;
        self.SuppServiceRequestID = ko.observable(data ? data.SuppServiceRequestID : "");
        self.VCN = ko.observable("");
        //self.VCN = ko.observable(data ? data.VCN : "");

        //self.VesselName = ko.observable(data ? data.VesselName : "");
        self.VesselName = ko.observable("");

        //self.ServiceTypeName = ko.observable(data ? data.ServiceType : "");
        self.ServiceTypeName = ko.observable("");
        //self.FromDate = ko.observable(data ? data.FromDate : "");
        self.FromDate = ko.observable("");
        //self.WFStatusSupp = ko.observable(data ? data.WFStatusSupp : "");
        self.AnyDangerousGoods = ko.observable(data ? data.AnyDangerousGoods : "");

        self.WFStatus = ko.observable("");
        ////self.WFStatusSupp = ko.observable("");
        ////self.WFStatus = ko.observable(data ? data.WFStatus : "");
        self.WFStatusSupp = ko.computed(function () {
            return self.WFStatus() == 'Approve' ? "Approved" : self.WFStatus() == 'Reject' ? "Rejected" : self.WFStatus() == 'Confirm' ? "Confirmed" : self.WFStatus() == 'Cancel' ? "Cancelled" : self.WFStatus() == 'Update Request' ? "Update Request" : "New Request";
        });
        
        self.IsPrimaryAgent = ko.observable(data ? data.IsPrimaryAgent : "");
        self.IsStartTime = ko.observable();

        self.WorkFlowRemarks = ko.observable();

        self.WorkflowInstanceID = ko.observable(data ? data.WorkflowInstanceID : "");


        ////////////////KendoUI Grid Sorting/////////
       
        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });
        self.ServiceTypeSort;
        self.ServiceTypeName.subscribe(function (value) {
            self.ServiceTypeSort = value;
        });
        self.RequestDtAndTimeSort;
        self.FromDate.subscribe(function (value) {
            self.RequestDtAndTimeSort = value;
        });

        self.StatusSort;
        self.WFStatus.subscribe(function (value) {
            self.StatusSort = value;
        });


        //self.ETB = ko.observable();
        //self.ETUB = ko.observable();
        //self.IsPrimaryAgent = ko.observable();

        /////////////////////////////////////////////////////


        //self.UploadedFiles = ko.observableArray([]);

        self.cache = function () { };
        self.set(data);


        //self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
        //    return new pendingTask(task);
        //}) : []);
        //self.SupplementaryGridDetail = ko.observableArray(data ? ko.utils.arrayMap(data.SupplementaryGridDetail, function (task1) {
        //    return new SupplementaryGridDetails(task1);
        //}) : []);
       

    }

    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.SupplementaryGridDetails = SupplementaryGridDetails;
    ipmsRoot.vesselDetail = vesselDetail;
    ipmsRoot.vesselModel = vesselModel;
    ipmsRoot.ServiceType = ServiceType;
    ipmsRoot.DocumentType = DocumentType;
    ipmsRoot.serviceTypeDetail = serviceTypeDetail;
    ipmsRoot.Locations = Locations;
    ipmsRoot.locationDetail = locationDetail;
    ipmsRoot.Berths = Berths;
    ipmsRoot.berthDetail = berthDetail;
    ipmsRoot.ArrivalCommodity = ArrivalCommodity;
    ipmsRoot.SupplementaryServiceRequestModel = SupplementaryServiceRequestModel;
    ipmsRoot.SuppHotColdWorkPermit = SuppHotColdWorkPermit;
    ipmsRoot.SuppFloatingCranes = SuppFloatingCranes;
    ipmsRoot.IMDGDetails = IMDGDetails;
    ipmsRoot.SuppHotColdWorkPermitDocument = SuppHotColdWorkPermitDocument;
    ipmsRoot.SuppServiceRequestModelGrid = SuppServiceRequestModelGrid;

}(window.IPMSROOT));
var DateFormat = 'YYYY-MM-DD HH:mm';

IPMSROOT.SuppServiceRequestModelGrid.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");
    // self.VesselID(data ? data.VesselID : "");
    // self.MovementType(data ? data.MovementType : "");

    var todaydate = new Date();
    var todate = new Date(todaydate);
    var fromdate = new Date(todaydate);
    todate.setDate(todaydate.getDate()+14);
    fromdate.setDate(fromdate.getDate() -14);
    self.RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
    self.RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
}


IPMSROOT.SupplementaryServiceRequestModel.prototype.set = function (data) {
    var self = this;

    self.SuppServiceRequestID(data ? (data.SuppServiceRequestID || '') : '');
    
    self.VesselData(data ? new IPMSROOT.vesselDetail(data.ArrivalNotification) : "");
    //self.VCN(data ? data.VCN || null : null);
    self.VCN(data ? (data.VCN || null) : null);
    self.VesselName(data ? data.VesselName : "");
    self.AnyDangerousGoods(data ? data.AnyDangerousGoods : "I");
    self.ServiceTypeName(data ? data.ServiceTypeName : "");
    //self.BerthCode(data ? (data.BerthCode || null) : null);
    self.PortCode(data ? (data.PortCode || "") : "");
    //self.QuayCode(data ? (data.QuayCode || "Q1") : "Q1");
    self.BerthKey(data ? (data.BerthKey || "") : "");
    self.FromDate(data ? (moment(data.FromDate).format(DateFormat) || null) : null);

    self.Quantity(data ? (data.Quantity || null) : null);
    self.Remarks(data ? (data.Remarks || "") : "");
    self.ToDate(data ? (moment(data.ToDate).format(DateFormat) || null) : null);

    self.ServiceType(data ? (data.ServiceType || "") : "");
    self.DocumentType(data ? (data.DocumentType || "") : "");
    self.WorkflowInstanceID(data ? (data.WorkflowInstanceID || null) : null);
    self.DDCargo(data ? (data.DDCargo || "") : "");
    self.DDBallast(data ? (data.DDBallast || "") : "");
    self.DDBunkers(data ? (data.DDBunkers || "") : "");

    self.SuppHotColdWorkPermitsVO(data ? new IPMSROOT.SuppHotColdWorkPermit(data.SuppHotColdWorkPermitsVO) : new IPMSROOT.SuppHotColdWorkPermit());
    self.SuppFloatingCranesVO(data ? ko.utils.arrayMap(data.SuppFloatingCranesVO, function (floatingcrane) {
        return new IPMSROOT.SuppFloatingCranes(floatingcrane);
    }) : []);



    self.CreatedBy(data ? data.CreatedBy : '');
    self.CreatedDate(data ? data.CreatedDate : "");
    self.ModifiedBy(data ? data.ModifiedBy : '');
    self.ModifiedDate(data ? data.ModifiedDate : "");
    self.TermsandConditions(data ? (data.TermsandConditions || true) : true);
    self.WFStatus(data ? data.WFStatus : "");

    self.ArrivalCommodities = ko.observableArray(data ? ko.utils.arrayMap(data.ArrivalNotification.ArrivalCommodities, function (commodity) {
        return new IPMSROOT.ArrivalCommodity(commodity);
    }) : null);

    self.ETB(data ? data.ETB : "");
    self.ETUB(data ? data.ETUB : "");

    self.IsPrimaryAgent(data ? data.IsPrimaryAgent : "");
    self.IsStartTime(data ? data.IsStartTime : true);

    self.cache.latestData = data;
}

IPMSROOT.SupplementaryGridDetails.prototype.set = function (data) {
    var self = this;

    //self.VCN = ko.observable("");
    //self.VesselName = ko.observable("");
    self.VCN(data ? data.VCN || "" : "");
    self.VesselName(data ? data.VesselName || "" : "");
    self.ServiceTypeName(data ? data.ServiceType || "" : "");
    self.FromDate(data ? data.FromDate || "" : "");
    self.WFStatus(data ? data.WFStatus : "");
    ////self.WFStatus(data ? data.WFStatus || "" : "");
    ////self.WFStatusSupp = ko.observable(data ? data.WFStatusSupp : "");
    //self.WFStatus = ko.observable(data ? data.WFStatus : "");
    //self.WFStatusSupp = ko.computed(function () {
    //    return self.WFStatus() == 'Approve' ? "Approved" : self.WFStatus() == 'Reject' ? "Rejected" : self.WFStatus() == 'Confirm' ? "Confirmed" : self.WFStatus() == 'Cancel' ? "Cancelled" : self.WFStatus() == 'Update Request' ? "Update Request" : "New Request";
    //});
    self.cache.latestData = data;
    
}



IPMSROOT.SupplementaryServiceRequestModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


IPMSROOT.SupplementaryGridDetails.prototype.reset = function () {
    this.set(this.cache.latestData);
}

