(function (ipmsRoot) {

    var VesselReferenceData = function (data) {
        var self = this;

        self.ddlVesselTypes = ko.observableArray(data ? $.map(data.GetVesselTypes, function (item) { return new VesselTypes(item); }) : []);
        self.ddlClassificationSocities = ko.observableArray(data ? $.map(data.GetClassificationSocity, function (item) { return new ClassificationSocities(item); }) : []);
        self.ddlPOR = ko.observableArray(data ? $.map(data.GetPOR, function (item) { return new POR(item); }) : []);
        self.ddlVesselNationality = ko.observableArray(data ? $.map(data.GetVesselNationality, function (item) { return new VesselNationality(item); }) : []);
        self.ddlEngineTypes = ko.observableArray(data ? $.map(data.GetEngineTypes, function (item) { return new EngineTypes(item); }) : []);
        self.ddlPropulsionTypes = ko.observableArray(data ? $.map(data.GetPropulsionTypes, function (item) { return new PropulsionTypes(item); }) : []);
        self.ddlCertificateNames = ko.observableArray(data ? $.map(data.GetCertificateNames, function (item) { return new CertificateNames(item); }) : []);
        self.ddlSafeWorkingLoad = ko.observableArray(data ? $.map(data.GetSafeWorkingLoad, function (item) { return new SafeworkingLoads(item); }) : []);
    }

    var VesselTypes = function (data) {
        var self = this;
        self.vesselTypeName = ko.observable(data ? data.SubCatName : "");
        self.vesselTypeCode = ko.observable(data ? data.SubCatCode : "");
    }

    var ClassificationSocities = function (data) {
        var self = this;
        self.ClsSocityName = ko.observable(data ? data.SubCatName : "");
        self.ClsSocityCode = ko.observable(data ? data.SubCatCode : "");
    }

    var POR = function (data) {

        var self = this;
        self.PortName = ko.observable(data ? data.PortName : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
    }

    var VesselNationality = function (data) {
        var self = this;
        self.VNName = ko.observable(data ? data.SubCatName : "");
        self.VNCode = ko.observable(data ? data.SubCatCode : "");

    }

    var EngineTypes = function (data) {
        var self = this;
        self.EngineTypeName = ko.observable(data ? data.SubCatName : "");
        self.EngineTypeCode = ko.observable(data ? data.SubCatCode : "");

    }

    var PropulsionTypes = function (data) {
        var self = this;
        self.PropulsionTypesName = ko.observable(data ? data.SubCatName : "");
        self.PropulsionTypesCode = ko.observable(data ? data.SubCatCode : "");
    }

    var CertificateNames = function (data) {
        var self = this;
        self.CertificateNames = ko.observable(data ? data.SubCatName : "");
        self.CertificateCode = ko.observable(data ? data.SubCatCode : "");

    }

    var SafeworkingLoads = function (data) {
        var self = this;
        self.SafeworkingLoadNames = ko.observable(data ? data.SubCatName : "");
        self.SafeworkingLoadCode = ko.observable(data ? data.SubCatCode : "");

    }

    var VesselEngineData = function (data) {
        var self = this;
        self.VesselEngineID = ko.observable(data ? data.VesselEngineID : "")
        self.EnginePower = ko.observable(data ? data.EnginePower : "").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });
        self.EngineType = ko.observable(data ? data.EngineType : "");
        self.PropulsionType = ko.observable(data ? data.PropulsionType : "");
        self.NoOfPropellers = ko.observable(data ? data.NoOfPropellers : "");
        self.MaxSpeed = ko.observable(data ? data.MaxSpeed : "");
    }

    var VesselCertificatesData = function (data) {
        var self = this;
        self.VACERTID = ko.observable(data ? data.VACERTID : "");
        self.CertificateName = ko.observable(data ? data.CertificateName : "");
        self.CertificateNo = ko.observable(data ? data.CertificateNo : "");
        self.DateOfIssue = ko.observable(data ? moment(data.DateOfIssue).format('YYYY-MM-DD') : "");
        self.DateOfValidity = ko.observable(data ? moment(data.DateOfValidity).format('YYYY-MM-DD') : "");
    }

    var VesselHachHoldData = function (data) {
        var self = this;
        self.VesselHatchHoldID = ko.observable(data ? data.VesselHatchHoldID : "");
        self.HatchHoldTypeM = ko.observable(data ? data.HatchHoldTypeM : "");
        self.SafeWorkingLoad = ko.observable(data ? data.SafeWorkingLoad : "");
        self.HoldCapacityCBM = ko.observable(data ? data.HoldCapacityCBM : "");
        self.Description = ko.observable(data ? data.Description : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
    }

    var VesselGearData = function (data) {
        var self = this;
        self.VesselGearID = ko.observable(data ? data.VesselGearID : "");
        self.GearTypeM = ko.observable(data ? data.GearTypeM : "");
        self.SafeWorkingLoad = ko.observable(data ? data.SafeWorkingLoad : "");
        self.GearCapacityCBM = ko.observable(data ? data.GearCapacityCBM : "");
        self.Description = ko.observable(data ? data.Description : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
    }

    var VesselGrabData = function (data) {
        var self = this;
        self.VesselGrabID = ko.observable(data ? data.VesselGrabID : "");
        self.GrabTypeM = ko.observable(data ? data.GrabTypeM : "");
        self.SafeWorkingLoad = ko.observable(data ? data.SafeWorkingLoad : "");
        self.GrabCapacityCBM = ko.observable(data ? data.GrabCapacityCBM : "");
        self.Description = ko.observable(data ? data.Description : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
    }

    var VesselRegistrationModel = function (data) {

        var self = this;

        /* Vessel Details Start */
        self.validationEnabled = ko.observable(true);
        self.VesselID = ko.observable("");
        self.WorkflowInstanceId = ko.observable();
        self.IMONo = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please enter IMO No.' } });
        //self.ExCallSign = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please enter EX-Call Sign' } });
        self.ExCallSign = ko.observable("");
        //self.ClassificationSociety = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* Please select classification society' } });
        self.ClassificationSociety = ko.observable("");
        self.VesselName = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please enter Vessel Name' } });
        // self.VesselType = ko.observable(undefined).extend({ onlyIf: self.validationEnabled, required: { message: '* Please select Vessel Type' } });
        //self.VesselType = ko.observable("").extend({ onlyIf: self.validationEnabled, message: '* Please select Vessel Type' });
        self.VesselType = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });
        self.NoOfBays = ko.observable("");
        self.CallSign = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please enter Call Sign' } });
        self.OfficialNumber = ko.observable();
        self.VesselBuildYear = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please select vessel build year' } });
        self.NoOfRowsOnDesk = ko.observable("");
        //  self.PortOfRegistry = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* Please select port of registry' } });
        // self.PortOfRegistry = ko.observable("").extend({ onlyIf: self.validationEnabled, message: '*  Please select port of registry' });
        self.PortOfRegistry = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });
        self.PortCode = ko.observable(""); //.extend({ onlyIf: self.validationEnabled, required: { message: '* Please enter Port of registry' } });
        self.PortName = ko.observable("");
        self.PortOfRegistryName = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });




        self.SubCatName = ko.observable("");
        self.SubCatCode = ko.observable("");
        self.MMSINumber = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please enter MMSI  Number' } });
        self.ExVesselName = ko.observable("");
        self.VesselNationality = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '*This field is required.' } });;
        self.IsGovtVessel = ko.observable('N');
        self.RecordStatus = ko.observable('A');
        self.WFStatus = ko.observable("");
        self.CreatedDate = ko.observable();
        self.VesselTypeName = ko.observable();
        self.IsVisible = ko.observable();
        self.Status = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        //self.IsApproved = ko.observable('N');
        //  self.PORCode = ko.observable("");
        /* Vessel Details end */
        /* VesselParameters Start*/
        self.validation1 = ko.observable(true);
        self.BeamInM = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please enter Beam in (M)' } });
        self.GrossRegisteredTonnageInMT = ko.observable("").extend({
            required: true,
            validation: {
                validator: function (val, params) { 
                    var result = true;
                    if (val > 0) {
                        result = true
                    } else {
                        self.GrossRegisteredTonnageInMT('');
                        result = false;
                    }
                    return result;
                },
                message: '* GRT must be greater than zero',
                onlyIf: self.validationEnabled
            }
        });// { message: '* Please enter Gross Registered Tonnage (GRT) ' } });
        self.LengthOverallInM = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please enter Length Over All (LOA) in (M)' } });
        //self.NetRegisteredTonnageInMT = ko.observable("").extend({ onlyIf: self.validationEnabled, required: true });// { message: '* Please enter Net Registered Tonnage (NRT) in MT' } });
        self.NetRegisteredTonnageInMT = ko.observable("");
        self.ParallelBodyLengthInM = ko.observable("")
        self.DeadWeightTonnageInMT = ko.observable("").extend({ onlyIf: self.validation1, required: true });//{ message: '* Please enter Dead Weight Tonnage (DWT) in MT' } });;
        self.BowToManifoldDistanceInM = ko.observable("");
        self.SummerDeadWeightInMT = ko.observable("");
        self.SummerDraftFWDInM = ko.observable("");
        self.SummerDisplacementInMT = ko.observable("");
        self.SummerDraftAFTInM = ko.observable("");
        //  self.TEUCapacity = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is requireddddddd' } });;
        self.TEUCapacity = ko.observable("").extend({ required: { onlyIf: function () { return self.VesselType() === 'CTSH'; }, message: '* This field is required.' } });
        self.ReducedGRT = ko.observable("");

        self.BowThruster = ko.observable('N');
        self.BowThrusterStatus = ko.computed(function () {
            return self.BowThruster() == 'Y' ? "YES" : "NO";
        });

        self.SternThruster = ko.observable('N');
        self.SternThrusterStatus = ko.computed(function () {
            return self.SternThruster() == 'Y' ? "YES" : "NO";
        });

        self.IsFinal = ko.observable();

        self.WorkFlowRemarks = ko.observable();

        /* VesselParameters END*/

        // Other Details Start ///

        self.BowToForwardHatchDistanceM = ko.observable("");
        self.BowThrusterPowerKW = ko.observable("");
        self.BowToBridgeFrontDistanceM = ko.observable("");
        self.SternThrusterPowerKW = ko.observable("");

        // Other Details End ///

        self.VesselEngines = ko.observableArray(data ? ko.utils.arrayMap(data.VesselEngines, function (VesselEnginesData) {
            return new VesselEngineData(VesselEnginesData);
        }) : []);

        self.VesselCertificateDetails = ko.observableArray(data ? ko.utils.arrayMap(data.VesselCertificateDetails, function (VesselCertificates) {
            return new VesselCertificatesData(VesselCertificates);
        }) : []);

        self.VesselHatchHolds = ko.observableArray(data ? ko.utils.arrayMap(data.VesselHatchHolds, function (VesselHatchHolds) {
            return new VesselHachHoldData(VesselHatchHolds);
        }) : []);

        self.VesselGears = ko.observableArray(data ? ko.utils.arrayMap(data.VesselGears, function (VesselGears) {
            return new VesselGearData(VesselGears);
        }) : []);

        self.VesselGrabs = ko.observableArray(data ? ko.utils.arrayMap(data.VesselGrabs, function (VesselGrabs) {
            return new VesselGrabData(VesselGrabs);
        }) : []);

        //add pending task - Srini

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        // end


        //Sorting//
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.IMONoSort;
        self.IMONo.subscribe(function (value) {
            self.IMONoSort = value;
        });

        self.VesseltypeSort;
        self.VesselTypeName.subscribe(function (value) {
            self.VesseltypeSort = value;
        });

        self.CallSignSort;
        self.CallSign.subscribe(function (value) {
            self.CallSignSort = value;
        });

        self.ExCallSignSort;
        self.ExCallSign.subscribe(function (value) {
            self.ExCallSignSort = value;
        });

        self.SubmissionDateSort;
        self.CreatedDate.subscribe(function (value) {
            self.SubmissionDateSort = kendo.parseDate(value);
        });
        //Sorting End//

        self.cache = function () { };
        self.set(data);
    }

    // Added by Srini
    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* This field is required.' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    IPMSROOT.pendingTask = pendingTask;
    //End Srini

    IPMSROOT.VesselRegistrationModel = VesselRegistrationModel;
    IPMSROOT.VesselReferenceData = VesselReferenceData;
    ipmsRoot.VesselEngineData = VesselEngineData;
    ipmsRoot.VesselCertificatesData = VesselCertificatesData;
    ipmsRoot.VesselHachHoldData = VesselHachHoldData;
    ipmsRoot.VesselGearData = VesselGearData;
    ipmsRoot.VesselGrabData = VesselGrabData;

}(window.IPMSROOT));

IPMSROOT.VesselRegistrationModel.prototype.set = function (data) {

    var self = this;
    /* Vessel Details Start */
    self.VesselID(data ? (data.VesselID || "") : "");
    self.WorkflowInstanceId(data ? (data.WorkflowInstanceId || "") : "");
    self.IMONo(data ? (data.IMONo || "") : "");
    self.ExCallSign(data ? (data.ExCallSign || "") : "");
    self.ClassificationSociety(data ? (data.ClassificationSociety || "") : "");
    self.VesselName(data ? (data.VesselName || "") : "");
    self.VesselType(data ? (data.VesselType || "") : "");
    self.VesselTypeName(data ? (data.VesselTypeName || "") : "");
    self.NoOfBays(data ? (data.NoOfBays || "") : "");
    self.CallSign(data ? (data.CallSign || "") : "");
    self.OfficialNumber(data ? (data.OfficialNumber || "") : "");

    var now = new Date();
    var endDate = new Date(now);
    self.VesselBuildYear('');
    try {
        if (data != undefined) {
            endDate.setYear(data ? (data.VesselBuildYear || "") : "");
            self.VesselBuildYear(endDate);
        }
        else
            self.VesselBuildYear(data ? (data.VesselBuildYear || "") : "");
    } catch (error) { };
    self.NoOfRowsOnDesk(data ? (data.NoOfRowsOnDesk || "") : "");
    self.PortOfRegistry(data ? (data.PortOfRegistry || "") : "");
    self.MMSINumber(data ? (data.MMSINumber || "") : "");
    self.ExVesselName(data ? (data.ExVesselName || "") : "");
    self.VesselNationality(data ? (data.VesselNationality || "") : "");
    self.IsGovtVessel(data ? (data.IsGovtVessel || "") : "N");

    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    /* Vessel Details END */
    
    /* VesselParameters Start*/
    self.BeamInM=ko.observable(data ? data.BeamInM:"");
    self.GrossRegisteredTonnageInMT=ko.observable(data ? data.GrossRegisteredTonnageInMT:"");
    self.LengthOverallInM=ko.observable(data ? data.LengthOverallInM :"");
    self.NetRegisteredTonnageInMT(data ? (data.NetRegisteredTonnageInMT || "") : "");
    self.ParallelBodyLengthInM(data ? (data.ParallelBodyLengthInM || "") : "");
    self.DeadWeightTonnageInMT=ko.observable(data ? data.DeadWeightTonnageInMT:"");
    self.BowToManifoldDistanceInM(data ? (data.BowToManifoldDistanceInM || "") : "");
    self.SummerDeadWeightInMT(data ? (data.SummerDeadWeightInMT || "") : "");
    self.SummerDraftFWDInM(data ? (data.SummerDraftFWDInM || "") : "");
    self.SummerDisplacementInMT(data ? (data.SummerDisplacementInMT || "") : "");
    self.SummerDraftAFTInM(data ? (data.SummerDraftAFTInM || "") : "");
    self.TEUCapacity(data ? (data.TEUCapacity || "") : "");
    self.ReducedGRT(data ? (data.ReducedGRT || "") : "");
    self.BowThruster(data ? (data.BowThruster || "N") : "N");

    self.PortOfRegistryName(data ? (data.PortOfRegistryName || "") : "");

    self.SternThruster(data ? (data.SternThruster || "N") : "N");

    /* VesselParameters END*/

    // Other Details Start ///

    self.BowToForwardHatchDistanceM(data ? (data.BowToForwardHatchDistanceM || "") : "");
    self.BowThrusterPowerKW(data ? (data.BowThrusterPowerKW || "") : "");
    self.BowToBridgeFrontDistanceM(data ? (data.BowToBridgeFrontDistanceM || "") : "");
    self.SternThrusterPowerKW(data ? (data.SternThrusterPowerKW || "") : "");
    self.WFStatus(data ? (data.WFStatus || "") : "")

    self.IsFinal(data ? (data.IsFinal || "") : "")
    self.IsVisible(data ? (data.IsVisible || "") : "")
    // Other Details End ///
    // self.PORCode(data ? (data.PORCode || "") : "");

    //self.IsApproved(data ? (data.IsApproved || "N") : "N");
    self.cache.latestData = data;
}

IPMSROOT.VesselRegistrationModel.prototype.reset = function () {

    this.set(this.cache.latestData);
}

function toDateEnable(fromdate) {

    return fromdate == "" ? true : false;
}

function allowOnlyTwoPositiveDigts(el, evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}


function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}


