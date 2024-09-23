(function (ipmsRoot) {

    var vesselModel = function (data) {
        var self = this;
        self.Vessels = ko.observableArray(data ? $.map(data, function (item1) { return new vesselDetail(item1); }) : []);
    }


    //Getting all the details of ArrivalNotification Based on VCN Number
    var vesselDetail = function (data) {

        var self = this;
        self.VCN = ko.observable(data ? data.VCN : "");

        self.ReasonForVisit = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.ReasonForVisit : data.ReasonForVisit) : "");
        self.ETA = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.ETA : data.ETA) : "");
        self.ETD = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.ETD : data.ETD) : "");
        self.ArrDraft = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.ArrDraft : data.ArrDraft) : "");
        self.LastPortOfCall = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.LastPortOfCall : data.LastPortOfCall) : "");
        self.NextPortOfCall = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.NextPortOfCall : data.NextPortOfCall) : "");
        self.Tidal = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Tidal : data.Tidal) : "");
        self.DaylightRestriction = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.DaylightRestriction : data.DaylightRestriction) : "");
        self.VoyageIn = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.VoyageIn : data.VoyageIn) : "");
        self.VoyageOut = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.VoyageOut : data.VoyageOut) : "");

        self.VesselName = ko.observable(data ? (data.Vessel ? data.Vessel.VesselName : data.VesselName) : "");
        self.VesselType = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.VesselType : data.VesselType) : "");
        self.CallSign = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.CallSign : data.CallSign) : "");
        self.IMONo = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.IMONo : data.IMONo) : "");
        self.LengthOverallInM = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.LengthOverallInM : data.LengthOverallInM) : "");
        self.BeamInM = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.BeamInM : data.BeamInM) : "");
        self.VesselNationality = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.VesselNationality : data.VesselNationality) : "");
        self.GrossRegisteredTonnageInMT = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.GrossRegisteredTonnageInMT : data.GrossRegisteredTonnageInMT) : "");
        self.DeadWeightTonnageInMT = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.DeadWeightTonnageInMT : data.DeadWeightTonnageInMT) : "");
        self.AnyDangerousGoodsonBoard = ko.observable(data ? data.AnyDangerousGoodsonBoard : "");
        self.DangerousGoodsClass = ko.observable(data ? data.DangerousGoodsClass : "");
        self.UNNo = ko.observable(data ? data.UNNo : "");
    }

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
    }

    var SuppHotColdWorkPermit = function (data) {
        var self = this;

        self.SuppHotColdWorkPermitID = ko.observable(data ? data.SuppHotColdWorkPermitID : '');
        self.SuppServiceRequestID = ko.observable(data ? data.SuppServiceRequestID : '');
        self.GassFreeCertificateAvailable = ko.observable(data ? data.GassFreeCertificateAvailable : "No");
        self.GassFreeCertificateValidity = ko.observable(data ? (data.GassFreeCertificateValidity != null ? moment(data.GassFreeCertificateValidity).format('YYYY-MM-DD HH:mm') : "") : "");
        self.GassFreeIssuingAuthority = ko.observable(data ? data.GassFreeIssuingAuthority : "");
        self.LocationID = ko.observable(data ? data.LocationID : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");

        self.LocationName = ko.observable(data ? data.LocationName : "");

        self.GassCertificateAvailable = ko.computed(function () {
            if (self.GassFreeCertificateAvailable() != "N") {
                return "No";
            }
            else {
                return "Yes";
            }
        });
        self.SuppHotColdWorkPermitDocumentsVO = ko.observableArray(data ? ko.utils.arrayMap(data.SuppHotColdWorkPermitDocumentsVO, function (document) {
            return new SuppHotColdWorkPermitDocument(document);
        }) : []);
    }

    


    var SuppHotWorkInspection = function (data) {
        var self = this;

        self.SuppHotWorkInspectionID = ko.observable(data ? data.SuppHotWorkInspectionID : 0);
        self.SuppServiceRequestID = ko.observable(data ? data.SuppServiceRequestID : 0);
        self.EmergencyProcedure = ko.observable(data ? data.EmergencyProcedure : "N");
        self.FireRiskAssessment = ko.observable(data ? data.FireRiskAssessment : "N");
        self.FlammableGases = ko.observable(data ? data.FlammableGases : "N");
        self.GasMonitoring = ko.observable(data ? data.GasMonitoring : "N");
        self.FireDetectors = ko.observable(data ? data.FireDetectors : "N");
        self.EquipmentCondition = ko.observable(data ? data.EquipmentCondition : "N");
        self.ConductiveMetals = ko.observable(data ? data.ConductiveMetals : "N");
        self.EquipmentStandby = ko.observable(data ? data.EquipmentStandby : "N");
        self.HighProtection = ko.observable(data ? data.HighProtection : "N");
        self.AdequateVentilation = ko.observable(data ? data.AdequateVentilation : "N");
        self.BarricadesRequired = ko.observable(data ? data.BarricadesRequired : "N");
        self.SymbolicSafetyScience = ko.observable(data ? data.SymbolicSafetyScience : "N");
        self.PersonalProtective = ko.observable(data ? data.PersonalProtective : "N");
        self.TrainedFireWatch = ko.observable(data ? data.TrainedFireWatch : "N");
        self.PostWelding = ko.observable(data ? data.PostWelding : "N");
        self.HouseKeepingPractices = ko.observable(data ? data.HouseKeepingPractices : "N");
        self.AdditionalConditions = ko.observable(data ? data.AdditionalConditions : "");
        self.PermitStatus = ko.observable(data ? data.PermitStatus : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "A");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDate = ko.observable(data ? data.ModifiedDate : "");
        self.HWPN = ko.observable(data ? data.HWPN : "");
        self.HWPNText = ko.observable(data ? data.HWPNText : "");
        //PermitStatus
        //self.PermitStatust = ko.computed(function () {
        //    return self.PermitStatus() == 'Y' ? "Yes" : "No";
        //});

    }

    var SuppHotWorkInspectionModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
        self.SuppServiceRequestID = ko.observable();
        self.VesselData = ko.observable();
        self.IsValidationEnabled = ko.observable(true);
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.LocationID = ko.observable("");
        self.RequestDate = ko.observable();
        self.DGStatus = ko.observable();
        self.PermitStatus = ko.observable();
        self.WorkflowInstanceID = ko.observable();
        self.ServiceTypeName = ko.observable("");
        self.ServiceType = ko.observable("");
        self.PortCode = ko.observable("");
        self.BerthName = ko.observable();
        self.FromDate = ko.observable();
        self.ToDate = ko.observable();
        self.PermitNo = ko.observable("");

        self.SuppHotWorkInspectionVO = ko.observable(data ? data.SuppHotWorkInspectionVO : "");
        self.SuppHotColdWorkPermitsVO = ko.observable();
        self.LocationName = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();

        self.AnyDangerousGoodsonBoard = ko.observable();
        self.DangerousGoodsClass = ko.observable();
        self.UNNo = ko.observable();
        self.HWPN = ko.observable();
        self.HWPNText = ko.observable();
       

        //PermitStatus
        self.permit = ko.observable(data ? (data.SuppHotWorkInspectionVO != null ? data.SuppHotWorkInspectionVO.PermitStatus : "") : "");

        self.PermitStatust = ko.computed(function () {
            //return self.DockingPlanStatus() == 'Approve' ? "Approved" : self.DockingPlanStatus() == 'Reject' ? "Rejected" : self.DockingPlanStatus() == 'Update Request' ? "Update Request" : "New Request";
            return self.permit() == 'Verified' ? "Verified" : self.permit() == 'Issued' ? "Issued" : "New";
        });

        self.Status = ko.computed(function () {
            return self.permit() == 'Y' ? false : true;
        });

        self.value = ko.observable(data ? (data.SuppHotWorkInspectionVO != null ? data.SuppHotWorkInspectionVO.value : "") : "");
        //PermitNo
        //self.permitno = ko.observable(data ? data.SuppHotWorkInspectionVO.SuppHotWorkInspectionID : "");
        self.permitno = ko.observable(data ? (data.SuppHotWorkInspectionVO != null ? data.SuppHotWorkInspectionVO.SuppHotWorkInspectionID : "") : "");

      



        //self.dangergoods(data ? data.ArrivalNotification.AnyDangerousGoodsonBoard : "Not Binded");

        //self.AnyDangerousGoodsonBoardt = ko.computed(function () {
        //    return self.dangergoods() == 'A' ? "Yes" : "No";
        //});
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

        self.ToDateSort;
        self.ToDate.subscribe(function (value) {
            self.ToDateSort = value;
        });


        self.PermitStatusSort;
        self.PermitStatus.subscribe(function (value) {
            self.PermitStatusSort = value;
        });


        self.RequestDtAndTimeSort;
        self.FromDate.subscribe(function (value) {
            self.RequestDtAndTimeSort = value;
        });

        self.permitnoSort;
        self.HWPN.subscribe(function (value) {
            self.permitnoSort = value;
        });

        self.HWPNText.subscribe(function (value) {
            self.permitnoSort = value;
        });

        ////DGStatusSort
        //self.DGStatusSort;
        //self.AnyDangerousGoodsonBoardt.subscribe(function (value) {
        //    self.DGStatusSort = value;
        //});


        self.StatusSort = ko.computed(function () {
            if (self.WorkflowInstanceID() != null) {
                return "Completed";
            }
            else {
                return "New";
            }
        });
        ///////////////////////////////////////////////////

       

        self.UploadedFiles = ko.observableArray([]);

        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.vesselModel = vesselModel;
    ipmsRoot.vesselDetail = vesselDetail;
    ipmsRoot.SuppHotColdWorkPermit = SuppHotColdWorkPermit;


    ipmsRoot.SuppHotWorkInspection = SuppHotWorkInspection;
    ipmsRoot.SuppHotWorkInspectionModel = SuppHotWorkInspectionModel;
    ipmsRoot.SuppHotColdWorkPermitDocument = SuppHotColdWorkPermitDocument;
    //PermitStatus
    //self.PermitStatust = ko.computed(function () {
    //    return self.PermitStatus() == 'Y' ? "Yes" : "No";
    //});


}(window.IPMSROOT));

IPMSROOT.SuppHotWorkInspectionModel.prototype.set = function (data) {
    var self = this;
    self.SuppServiceRequestID(data ? (data.SuppServiceRequestID || '') : '');
    self.VesselData(data ? new IPMSROOT.vesselDetail(data.ArrivalNotification) : "");
    self.VCN(data ? data.VCN || "" : "");
    self.VesselName(data ? data.VesselName : "");
    self.ServiceTypeName(data ? data.ServiceTypeName : "");

    self.PortCode(data ? data.PortCode : "");
    self.FromDate(data ? data.FromDate : "");
    self.ToDate(data ? data.ToDate : "");
    self.ServiceType(data ? data.ServiceType : "");
    self.WorkflowInstanceID(data ? (data.WorkflowInstanceID || null) : null);
    
    self.SuppHotWorkInspectionVO(data ? new IPMSROOT.SuppHotWorkInspection(data.SuppHotWorkInspectionVO) : new IPMSROOT.SuppHotWorkInspection());
    self.SuppHotColdWorkPermitsVO(data ? new IPMSROOT.SuppHotColdWorkPermit(data.SuppHotColdWorkPermitsVO) : "");
    self.HWPN(data ? self.SuppHotWorkInspectionVO().HWPN() : "");
    self.HWPNText(data ? self.SuppHotWorkInspectionVO().HWPNText() : "");
    self.CreatedBy(data ? data.CreatedBy : '');
    self.CreatedDate(data ? data.CreatedDate : "");
    self.ModifiedBy(data ? data.ModifiedBy : '');
    self.ModifiedDate(data ? data.ModifiedDate : "");

    self.AnyDangerousGoodsonBoard(data ? data.ArrivalNotification.AnyDangerousGoodsonBoard : "Not Binded");
    self.AnyDangerousGoodsonBoardt = ko.computed(function () {
        return self.AnyDangerousGoodsonBoard() == 'A' ? "Yes" : "No";
    });


    self.DangerousGoodsClass(data ? data.ArrivalNotification.DangerousGoodsClass : "Not Binded");
    self.UNNo(data ? data.ArrivalNotification.UNNo : "Not Binded");
    self.BerthName(data ? data.BerthName : "");

    self.LocationName(data ? data.SuppHotColdWorkPermitsVO.LocationName : "");

    self.cache.latestData = data;

}

IPMSROOT.SuppHotWorkInspectionModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



//Accept only numeric 
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
}

//Accept only Alphabets and spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z ]*$/;
    return charcheck.test(keychar);
}

//Accept Alpha numeric

function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    return charcheck.test(keychar);
}