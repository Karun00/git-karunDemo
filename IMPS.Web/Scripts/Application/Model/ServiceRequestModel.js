(function (ipmsRoot) {
    var ArrivalNotificationReferenceData = function (data) {
        var self = this;

        self.MovementTypes = ko.observableArray(data ? $.map(data.getMomentTypes, function (item) { return new MovementType(item); }) : []);
        self.sideAlongSides = ko.observableArray(data ? $.map(data.getSideAlongSides, function (item) { return new sideAlongSide(item); }) : []);
        self.getWarpSides = ko.observableArray(data ? $.map(data.getWarpSides, function (item) { return new warpSides(item); }) : []);
        self.getDocumenttypes = ko.observableArray(data ? $.map(data.getDocumenttypes, function (item) { return new DocumentType(item); }) : []);
        self.UserDetails = ko.observableArray(data ? $.map(data.UserDetails, function (item) { return new UserDetail(item); }) : []);
        self.Slots = ko.observableArray(data ? $.map(data.Slots, function (item) { return new SlotType(item); }) : []);
        self.MovementSlots = ko.observableArray(data ? $.map(data.Slots, function (item) { return new SlotType(item); }) : []);
        self.AllSlots = ko.observableArray(data ? $.map(data.Slots, function (item) { return new SlotType(item); }) : []);
    }

    var UserDetail = function (data) {
        var self = this;
        self.UserName = ko.observable(data ? data.FirstName + ' ' + data.LastName : "");
        self.ContactNo = ko.observable(data ? data.ContactNo : "");
        self.EmailID = ko.observable(data ? data.EmailID : "");
    }

    var SlotType = function (data) {
        var self = this;
        self.SlotPeriod = ko.observable(data ? data.SlotPeriod : "");
        self.SlotNumber = ko.observable(data ? data.SlotNumber : "");
        self.Slot = ko.observable(data ? data.Slot : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.EndTime = ko.observable(data ? data.EndTime : "");

    }

    var MovementType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var sideAlongSide = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var warpSides = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var DocumentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var WarpingBollard = function (data) {
        var self = this;
        self.warpingFromBollards = ko.observableArray(data ? $.map(data, function (item) { return new warpingFromBollard(item); }) : []);

    }


    var warpingFromBollard = function (data) {
        var self = this;
        self.BollardName = ko.observable(data ? data.BollardName : "");
        self.FromBollardKey = ko.observable(data ? data.FromBollardKey : "");
        self.ToBollardKey = ko.observable(data ? data.ToBollardKey : "");
    }
    var ServerRequestShift = function (data) {

        var self = this;
        self.IsValidationEnabled = ko.observable(true);
        self.DraftFWD = ko.observable(data ? data.DraftFWD : "");
        self.DraftAFT = ko.observable(data ? data.DraftAFT : "");
        self.ServiceRequestShiftingID = ko.observable(data ? data.ServiceRequestShiftingID : "");

    }
    var ServiceRequestWarp = function (data) {
        var self = this;

        self.ServiceRequestWarpingID = ko.observable(data ? data.ServiceRequestWarpingID : "");        

    }
    var ServiceRequestSail = function (data) {
        var self = this;
        self.ServiceRequestSailingID = ko.observable(data ? data.ServiceRequestSailingID : "");
        self.DocumentID = ko.observable(data ? data.DocumentID : "");        
        self.MarineRevenueCleared = ko.observable("");
        self.FileName = ko.observable(data ? data.FileName : "");
        self.DocPath = ko.observable("");
    }

    var Berth = function (data, berthsData) {
        var self = this;
       
        self.ToPortCode = ko.observable(data ? data.ToPortCode : "");
        self.ToQuayCode = ko.observable(data ? data.ToQuayCode : "");
        self.ToBerthCode = ko.observable(data ? data.ToBerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
        self.Bollards = ko.observableArray([new Bollard()]);
        if (data !== undefined && data != null) {
            if (data.Bollards !== undefined) {
                self.Bollards($.map(data.Bollards, function (item) {
                    return new Bollard(item);
                }));
            }
            else {
                if (berthsData != undefined) {
                    var selectedBerth = berthsData.filter(function (obj) {
                        return obj.BerthKey() === self.BerthKey();
                    })[0];

                    if (selectedBerth != null && selectedBerth != undefined) {
                        self.Bollards(selectedBerth.Bollards());
                    }
                }
            }
        }
    }

    var Bollard = function (data) {
        var self = this;

        self.FromPositionPortCode = ko.observable(data ? data.FromPositionPortCode : "");
        self.FromPositionQuayCode = ko.observable(data ? data.FromPositionQuayCode : "");
        self.FromPositionBerthCode = ko.observable(data ? data.FromPositionBerthCode : "");
        self.FromPositionBollardCode = ko.observable(data ? data.FromPositionBollardCode : "");

        self.ToPositionPortCode = ko.observable(data ? data.ToPositionPortCode : "");
        self.ToPositionQuayCode = ko.observable(data ? data.ToPositionQuayCode : "");
        self.ToPositionBerthCode = ko.observable(data ? data.ToPositionBerthCode : "");
        self.ToPositionBollardCode = ko.observable(data ? data.ToPositionBollardCode : "");

        self.BollardName = ko.observable(data ? data.BollardName : "");

        self.FromBollardKey = ko.observable(data ? data.FromBollardKey : "").extend({ required: { message: '* Please Select From Bollard' } });
        self.ToBollardKey = ko.observable(data ? data.ToBollardKey : "").extend({ required: { message: '* Please Select To Bollard' } });
    }

    var vesselModel = function (data) {
        var self = this;
        self.Vessels = ko.observableArray(data ? $.map(data, function (item1) { return new vesselDetail(item1); }) : []);

    }

    var vesselDetail = function (data) {
        var self = this;
        self.VCN = ko.observable(data ? data.VCN : "");

        self.ReasonForVisit = ko.observable(data ? data.ReasonForVisit : "");
        self.ETA = ko.observable(data ? moment(data.ETA).format('YYYY-MM-DD HH:mm') : "");
        self.ETD = ko.observable(data ? moment(data.ETD).format('YYYY-MM-DD HH:mm') : "");
        self.ArrDraft = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.ArrDraft : data.ArrDraft) : "");
        self.LastPortOfCall = ko.observable(data ? data.LastPortOfCall : "");
        self.NextPortOfCall = ko.observable(data ? data.NextPortOfCall : "");
        self.Tidal = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Tidal == "I" ? "No" : "Yes" : data.Tidal == "I" ? "No" : "Yes") : "");
        self.DaylightRestriction = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.DaylightRestriction == "I" ? "No" : "Yes" : data.DaylightRestriction == "I" ? "No" : "Yes") : "");
        self.VoyageIn = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.VoyageIn : data.VoyageIn) : "");
        self.PilotExemption = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.PilotExemption == "I" ? "No" : "Yes" : data.PilotExemption == "I" ? "No" : "Yes") : "");

        self.RegisteredName = ko.observable(data ? (data.Agent ? data.Agent.RegisteredName : data.RegisteredName) : "");
        self.TelephoneNo1 = ko.observable(data ? (data.Agent ? data.Agent.TelephoneNo1 : data.TelephoneNo1) : "");
        self.FaxNo = ko.observable(data ? (data.Agent ? data.Agent.FaxNo : data.FaxNo) : "");
        self.FirstName = ko.observable(data ? (data.AuthorizedContactPerson ? data.AuthorizedContactPerson.FirstName : data.FirstName) : "");
        self.SurName = ko.observable(data ? (data.AuthorizedContactPerson ? data.AuthorizedContactPerson.SurName : data.SurName) : "");
        self.CellularNo = ko.observable(data ? (data.AuthorizedContactPerson ? data.AuthorizedContactPerson.CellularNo : data.CellularNo) : "");
        self.EmailID = ko.observable(data ? (data.AuthorizedContactPerson ? data.AuthorizedContactPerson.EmailID : data.EmailID) : "");


        self.VesselName = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.VesselName : data.VesselName) : "");
        self.VesselType = ko.observable(data ? data.VesselType : "");
        self.VesselTypeCode = ko.observable(data ? data.VesselTypeCode : "");
        self.CallSign = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.CallSign : data.CallSign) : "");
        self.IMONo = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.IMONo : data.IMONo) : "");
        self.LengthOverallInM = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.LengthOverallInM : data.LengthOverallInM) : "");
        self.BeamInM = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.BeamInM : data.BeamInM) : "");
        self.VesselNationality = ko.observable(data ? data.VesselNationality : "");
        self.GrossRegisteredTonnageInMT = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.GrossRegisteredTonnageInMT : data.GrossRegisteredTonnageInMT) : "");
        self.DeadWeightTonnageInMT = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.Vessel.DeadWeightTonnageInMT : data.DeadWeightTonnageInMT) : "");
        self.IsSpecialNature = ko.observable(data ? (data.ArrivalNotification ? data.ArrivalNotification.IsSpecialNature == "I" ? "No" : "Yes" : data.IsSpecialNature == "I" ? "No" : "Yes") : "");
        self.Reasons = ko.observable(data ? data.Reasons : "");
    }    


    var ServiceRequestModelGrid = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.VesselID = ko.observable();
        self.MovementType = ko.observable();

        self.MovementFrom = ko.observable();
        self.MovementTo = ko.observable();
        self.set(data);
    }

    var ServiceRequestModel = function (data, berthsData) {
        var self = this;
        
        self.RadeilValue = ko.observable('50');

        self.VesselType = ko.observable(data ? (data.ArrivalNotification ? (data.ArrivalNotification.Vessel ? data.ArrivalNotification.Vessel.VesselType : "") : "") : "");

        self.IsValidationEnabled = ko.observable(true);
        self.ServiceRequestID = ko.observable(data ? data.ServiceRequestID : 0);

        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : 0);
        self.WorkflowTaskCode = ko.observable();
        self.WorkflowInstanceTaskName = ko.observable();
        self.UserName = ko.observable(data ? data.UserName : "");
        self.ContactNo = ko.observable(data ? data.ContactNo : "");
        self.EmailID = ko.observable(data ? data.EmailID : "");       

        
        self.BPWorkflowInstanceId = ko.observable(data ? data.BPWorkflowInstanceId : 0);

        self.currentData = ko.observable();

        self.VesselData = ko.observable();

        self.CurrentBerth = ko.observable(data ? data.CurrentBerth : "");
        self.CurrentFromBollardName = ko.observable(data ? data.CurrentFromBollardName : "");
        self.CurrentToBollardName = ko.observable(data ? data.CurrentToBollardName : "");
        self.CurrentBerthCode = ko.observable(data ? data.CurrentBerthCode : "");
        self.ATB = ko.observable(data ? data.ATB : "");
        self.AllocatedBerth = ko.observable(data ? data.AllocatedBerth : "");
        self.AllocatedFromBollardName = ko.observable(data ? data.AllocatedFromBollardName : "");
        self.AllocatedToBollardName = ko.observable(data ? data.AllocatedToBollardName : "");


        self.chkDateTime = ko.observable("");

        self.MovementType = ko.observable(data ? data.MovementType : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please Select the Movement Type' } });

        self.FromBollardKey = ko.observable(data ? (data.ServiceRequestWarping ? data.ServiceRequestWarping.FromBollardKey : undefined) : undefined);
        self.ToBollardKey = ko.observable(data ? (data.ServiceRequestWarping ? data.ServiceRequestWarping.ToBollardKey : undefined) : undefined);


        self.VCN = ko.observable("").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please Select the VCN' } });

        self.MovementDateTime = ko.observable("").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please Select the Movement Date' } });
        self.SubMovementDate = ko.observable("");
        self.SubmittedDateTime = ko.observable("");
        self.VesselName = ko.observable("");
        self.MovementName = ko.observable("");
        self.PreferredDateTime = ko.observable("");
        self.SlotPeriod = ko.observable("");
        self.MovementSlot = ko.observable("").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please Select the Slot' } });

        self.EditMovementDate = ko.observable("");
        self.EditMovementSlot = ko.observable("");
        self.IsUpdateMovement = ko.observable(false);     

        self.SideAlongSideCode = ko.observable(data ? data.SideAlongSideCode : "").extend({ required: { onlyIf: self.IsValidationEnabled, message: '* Please Select the Side Along Side' } });
               
        self.Warp = ko.observable(data ? (data.ServiceRequestWarping ? data.ServiceRequestWarping.Warp : undefined) : undefined).extend({ required: { onlyIf: function () { return self.MovementType() === 'WRMV'; }, message: '* Please Select Warp' } });
        self.WarpDistance = ko.observable(data ? (data.ServiceRequestWarping ? data.ServiceRequestWarping.WarpDistance : "") : "").extend({ required: { onlyIf: function () { return self.MovementType() === 'WRMV'; }, message: '* Please Select WarpDistance' } });

        self.MarineRevenueCleared = ko.observable(false);
        self.OwnSteam = ko.observable(data ? data.OwnSteam : "");
        self.IsTidal = ko.observable(data ? data.IsTidal : "");
        self.NoMainEngine = ko.observable(data ? data.NoMainEngine : "");
        self.Comments = ko.observable(data ? data.Comments : "");
        self.workflowRemarks = ko.observable();
        self.IsFinal = ko.observable(data ? data.IsFinal : "");
        self.RecordStatus = ko.observable('A');
        self.SlotStatus = ko.observable();
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "In Active";
        });

        self.WorkflowTaskCode = ko.observable(data ? data.WorkflowTaskCode : "");

        self.TidalStatus = ko.observable(data ? data.TidalStatus : "");
        self.IsConfirmCancel = ko.observable(data ? data.IsConfirmCancel : "N");
        

        

        self.isEditVisible = ko.observable();
        self.isEditVisible = ko.computed(function () {
            if (self.IsFinal() == "Y" || self.WorkflowTaskCode() == "WFSA" || self.WorkflowTaskCode() == "WFCA" || self.WorkflowTaskCode() == "WFRE" || self.WorkflowTaskCode() == "WFCC" || self.WorkflowTaskCode() == "WSSA" || self.WorkflowTaskCode() == "WSRE") {
                return false;
            }
            else {
                return true;
            }
        }, this);

        self.isCancelVisible = ko.observable();
        self.isCancelVisible = ko.computed(function () {

            if (self.RecordStatus() === "I")
                return false;
            else {
                if (self.RecordStatus === "I" ||
                    self.IsFinal() == "Y" ||
                    self.WorkflowTaskCode() == "WFCA" ||
                    self.WorkflowTaskCode() == "WFRE" ||
                    self.WorkflowTaskCode() == "WFCC" ||
                    self.WorkflowTaskCode() == "WSSA" ||
                    self.WorkflowTaskCode() == "WSRE") {
                    return false;
                } else {
                    return true;
                }
            }
        }, this);

        self.isConfirmCancelVisible = ko.observable();
        self.isConfirmCancelVisible = ko.computed(function () {

            if (self.RecordStatus() === "I")
                return false;
            else {
                if (self.WorkflowTaskCode() == "WFCO" && self.SlotStatus() != "SCHD") {
                    if (self.WorkflowTaskCode() == "WFCO" && self.SlotStatus() != "CNFR") {
                        return true;
                    } else false;
                } else {
                    return false;
                }
            }
        }, this);
        


        self.IsPHANFinal = ko.observable(data ? data.IsPHANFinal : "");
        self.IsISPSANFinal = ko.observable(data ? data.IsISPSANFinal : "");
        self.IsIMDGANFinal = ko.observable(data ? data.IsIMDGANFinal : "");


        self.CreatedBy = ko.observable('1');
        self.CreatedDate = GetDateTime();
        self.ModifiedBy = ko.observable('1');
        self.ModifiedDate = GetDateTime();

        self.WorkFlowRemarks = ko.observable();

        self.ServiceRequestSailing = ko.observable(data ? data.ServiceRequestSailing : "");
        self.ServiceRequestShifting = ko.observable(data ? data.ServiceRequestShifting : "");
        self.ServiceRequestWarping = ko.observable(data ? data.ServiceRequestWarping : "");

        self.ServiceRequestDocument = ko.observableArray([]);

        self.UploadedFiles = ko.observableArray([]);       
        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.MarineRevenueCleared = ko.observable(data ? (data.ServiceRequestSailing ? data.ServiceRequestSailing.MarineRevenueCleared : data.MarineRevenueCleared) : "");
        self.DocPath = ko.observable("");
        self.FileName = ko.observable(data ? data.FileName : "");        
        self.ServiceRequestSailingID = ko.observable(data ? (data.ServiceRequestSailing ? data.ServiceRequestSailing.ServiceRequestSailingID : data.ServiceRequestSailingID) : "");

        self.currentData = ko.observable();

        self.BerthCode = ko.observable();

        self.BollardCode = ko.observable(data ? data.BollardCode : "");
        self.BollardName = ko.observable(data ? data.BollardName : "");
        self.PortCode = ko.observable();
        self.PortName = ko.observable();

        self.QuayCode = ko.observable();
        self.QuayName = ko.observable();


        self.SieAlongSideName = ko.observable(data ? data.SieAlongSideName : "");
        self.serviceid = ko.observable();

        self.DraftFWD_SHMV = ko.observable(data ? (data.ServiceRequestShifting ? data.ServiceRequestShifting.DraftFWD : "") : "").extend({ required: { onlyIf: function () { return self.MovementType() === 'SHMV'; }, message: '* Please Select Draft FWD' } });
        self.DraftAFT_SHMV = ko.observable(data ? (data.ServiceRequestShifting ? data.ServiceRequestShifting.DraftAFT : "") : "").extend({ required: { onlyIf: function () { return self.MovementType() === 'SHMV'; }, message: '* Please Select Draft AFT' } });
        self.BerthKey = ko.observable(data ? (data.ServiceRequestShifting ? data.ServiceRequestShifting.BerthKey : undefined) : undefined).extend({ required: { onlyIf: function () { return self.MovementType() === 'SHMV'; }, message: '* Please Select To Berth' } });


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        self.serviceWarping = ko.observable(data ? new ServiceRequestWarp(data.ServiceRequestWarping) : new ServiceRequestWarp());
        self.serviceSailing = ko.observable(data ? new ServiceRequestSail(data.ServiceRequestSailing) : new ServiceRequestSail());

        self.Draftaft = ko.observable(data ? new ServerRequestShift(data.ServiceRequestShifting) : new ServerRequestShift());
        self.ShiftToBerth = ko.observable(data ? new Berth(data.ServiceRequestShifting, berthsData) : new Berth(undefined, berthsData));
        self.ShiftFromPositionBollard = ko.observable(data ? new Bollard(data.ServiceRequestShifting) : new Bollard());
        self.ShiftToPositionBollard = ko.observable(data ? new Bollard(data.ServiceRequestShifting) : new Bollard());
        self.AnyDangerousGoodsonBoard = ko.observable();

        self.PassengersDisembarking = ko.observable().extend({ required: { onlyIf: function () { return (self.VesselType() === 'V010' && MovementType() === 'SGMV'); }, message: '* Please Select the Passengers Disembarking' } });;
        self.PassengersEmbarking = ko.observable().extend({ required: { onlyIf: function () { return (self.VesselType() === 'V010' && MovementType() === 'SGMV'); }, message: '* Please Select the Passengers Embarking' } });;
        self.Tidal = ko.observable();
        self.IsSpecialNature = ko.observable();

        self.BerthKey.subscribe(function (berthName) {
            if (berthName !== undefined) {
                var selectedBerth = berthsData.filter(function (obj) {
                    return obj.BerthKey() === berthName;
                })[0];

                if (selectedBerth !== undefined) {
                    self.ShiftToBerth().ToPortCode(selectedBerth.ToPortCode());
                    self.ShiftToBerth().ToQuayCode(selectedBerth.ToQuayCode());
                    self.ShiftToBerth().ToBerthCode(selectedBerth.ToBerthCode());
                    self.ShiftToBerth().BerthName(selectedBerth.BerthName());
                    self.ShiftToBerth().Bollards($.map(selectedBerth.Bollards(), function (item) {
                        return new Bollard(ko.toJS(item));
                    }));
                }

            }
            else {

                self.ShiftFromPositionBollard().FromBollardKey(undefined);
                self.ShiftToPositionBollard().ToBollardKey(undefined)

            }


        });


        self.ShiftFromPositionBollard().FromBollardKey.subscribe(function (bollardName1) {
            if (bollardName1 !== undefined) {
                var selectedBollard = self.ShiftToBerth().Bollards().filter(function (obj) {
                    return obj.FromBollardKey() === bollardName1;
                })[0];

                if (selectedBollard !== undefined) {
                    self.ShiftFromPositionBollard().FromPositionPortCode(selectedBollard.FromPositionPortCode());
                    self.ShiftFromPositionBollard().FromPositionQuayCode(selectedBollard.FromPositionQuayCode());
                    self.ShiftFromPositionBollard().FromPositionBerthCode(selectedBollard.FromPositionBerthCode());
                    self.ShiftFromPositionBollard().FromPositionBollardCode(selectedBollard.FromPositionBollardCode());
                }
            }
        });

        self.ShiftToPositionBollard().ToBollardKey.subscribe(function (bollardName2) {
            if (bollardName2 !== undefined) {
                var selectedBollard = self.ShiftToBerth().Bollards().filter(function (obj) {
                    return obj.ToBollardKey() === bollardName2;
                })[0];

                if (selectedBollard !== undefined) {
                    self.ShiftToPositionBollard().ToPositionPortCode(selectedBollard.ToPositionPortCode());
                    self.ShiftToPositionBollard().ToPositionQuayCode(selectedBollard.ToPositionQuayCode());
                    self.ShiftToPositionBollard().ToPositionBerthCode(selectedBollard.ToPositionBerthCode());
                    self.ShiftToPositionBollard().ToPositionBollardCode(selectedBollard.ToPositionBollardCode());
                }
            }
        });
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });
        self.MovementNameSort;
        self.MovementName.subscribe(function (value) {
            self.MovementNameSort = value;
        });
        self.MovementDateTimeSort;
        self.MovementDateTime.subscribe(function (value) {
            self.MovementDateTimeSort = value;
        });
        //self.SubmovementDateTimeSort;
        //self.SubmovementDateTime.subscribe(function (value) {
        //    self.SubmovementDateTimeSort = value;
        //});
        self.SubmittedDateTimeSort;
        self.SubmittedDateTime.subscribe(function (value) {
            self.SubmittedDateTimeSort = value;
        });
        self.WorkflowInstanceTaskNameSort;
        self.WorkflowInstanceTaskName.subscribe(function (value) {
            self.WorkflowInstanceTaskNameSort = value;
        });


        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.DraftFWD = ko.observable();
        self.DraftAFT = ko.observable();

        self.ServiceRequestDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.ServiceRequestDocuments, function (servicerequestDocument) {

            return new ServiceRequestDocument(servicerequestDocument);
        }) : []);

        

        self.cache = function () { };
        self.set(data);

    }

    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Enter Remarks' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : null);
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    var ServiceRequestDocument = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.DocumentName : '');
        self.DocumentCode = ko.observable(data ? data.DocumentCode : '');
        self.FileName = ko.observable(data ? data.FileName : '');
        self.ServiceRequestID = ko.observable(data ? data.ServiceRequestID : '');
        self.ServiceRequestDocumentID = ko.observable(data ? data.ServiceRequestDocumentID : '');


    }

    ipmsRoot.ServiceRequestModel = ServiceRequestModel;
    ipmsRoot.Berth = Berth;
    ipmsRoot.Bollard = Bollard;
    ipmsRoot.ServerRequestShift = ServerRequestShift;
    ipmsRoot.WarpingBollard = WarpingBollard;
    ipmsRoot.ArrivalNotificationReferenceData = ArrivalNotificationReferenceData;
    ipmsRoot.MovementType = MovementType;
    ipmsRoot.sideAlongSide = sideAlongSide;
    ipmsRoot.warpingFromBollard = warpingFromBollard;
    ipmsRoot.vesselDetail = vesselDetail;
    ipmsRoot.vesselModel = vesselModel;
    ipmsRoot.UserDetail = UserDetail;    
    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.ServiceRequestSail = ServiceRequestSail;
    ipmsRoot.ServiceRequestModelGrid = ServiceRequestModelGrid;
    ipmsRoot.ServiceRequestDocument = ServiceRequestDocument;
    ipmsRoot.SlotType = SlotType;

}(window.IPMSROOT));


IPMSROOT.ServiceRequestModelGrid.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");
    self.VesselID(data ? data.VesselID : "");
    self.MovementType(data ? data.MovementType : "");

    var todaydate = new Date();
    var todate = new Date(todaydate);
    var fromdate = new Date(todaydate);
    todate.setDate(todaydate.getDate() + 30);
    fromdate.setDate(fromdate.getDate() - 30);
    self.MovementFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
    self.MovementTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
}

IPMSROOT.ServiceRequestModel.prototype.set = function (data) {
    var self = this;
    self.IsValidationEnabled(false);
    self.ServiceRequestID(data ? (data.ServiceRequestID || 0) : 0);
    self.MovementType(data ? data.MovementType || null : null);
    self.VCN(data ? data.VCN || "" : "");
    if (data != undefined) {
        self.VesselName(data ? data.VesselName || "" : "");
        self.MovementName(data ? data.MovementName || "" : "");
    }
    self.VesselData(data ? new IPMSROOT.vesselDetail(data) : "");
    self.MovementDateTime(data ? moment(data.MovementDateTime).format('YYYY-MM-DD HH:mm') || "" : "");
    self.SubMovementDate(data ? moment(data.SubMovementDate).format('YYYY-MM-DD HH:mm') || "" : "");
    self.PreferredDateTime(data ? moment(data.PreferredDateTime).format('YYYY-MM-DD HH:mm') || "" : "");
    self.SubmittedDateTime(data ? data.SubmittedDateTime || "" : "");
    self.OwnSteam(data ? data.OwnSteam || "" : "");
    self.IsTidal(data ? data.IsTidal || "" : "");
    self.NoMainEngine(data ? data.NoMainEngine || "" : "");
    self.Comments(data ? data.Comments || "" : "");
    self.SideAlongSideCode(data ? data.SideAlongSideCode || null : null);
    self.TidalStatus(data ? data.TidalStatus || "" : "");

    self.UserName(data ? data.UserName || "" : "");
    self.ContactNo(data ? data.ContactNo || "" : "");
    self.EmailID(data ? data.EmailID || "" : "");
    self.SlotPeriod(data ? data.SlotPeriod || "" : "");
    self.MovementSlot(data ? data.MovementSlot || "" : "");
    

    self.Warp(data ? (data.ServiceRequestWarping ? data.ServiceRequestWarping.Warp : undefined) || undefined : undefined);

    self.WorkflowInstanceTaskName(data ? data.WorkflowInstanceTaskName == "Confirmation Cancel" ? "Cancel Confirmation Pending" : data.WorkflowInstanceTaskName == "Cancel Request Approve" ? "Cancellation Approved" : data.WorkflowInstanceTaskName || null : null);
    self.FromBollardKey(data ? (data.ServiceRequestWarping ? data.ServiceRequestWarping.FromBollardKey : undefined) || undefined : undefined)
    self.ToBollardKey(data ? (data.ServiceRequestWarping ? data.ServiceRequestWarping.ToBollardKey : undefined) || undefined : undefined)
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.DraftFWD_SHMV(data ? (data.ServiceRequestShifting ? data.ServiceRequestShifting.DraftFWD : "") || "" : "");
    self.DraftAFT_SHMV(data ? (data.ServiceRequestShifting ? data.ServiceRequestShifting.DraftAFT : "") || "" : "");
    self.BerthKey(data ? (data.ServiceRequestShifting ? data.ServiceRequestShifting.BerthKey : undefined) || undefined : undefined);
    self.MarineRevenueCleared(data ? (data.ServiceRequestSailing ? data.ServiceRequestSailing.MarineRevenueCleared : "") || "" : "");
    self.DocumentID(data ? (data.ServiceRequestSailing ? data.ServiceRequestSailing.DocumentID : "") || "" : "");    
    self.FileName(data ? (data.ServiceRequestSailing != null ? (data.ServiceRequestSailing.ServiceRequestDocument != null ? data.ServiceRequestSailing.ServiceRequestDocument.FileName : "") || "" : "") : "");
    
    $('#fileToUpload').val('');   

    self.CurrentBerth(data ? (data.CurrentBerth || "") : "");
    self.CurrentFromBollardName(data ? (data.CurrentFromBollardName || "") : "");
    self.CurrentToBollardName(data ? (data.CurrentToBollardName || "") : "");
    self.CurrentBerthCode(data ? (data.CurrentBerthCode || "") : "");
    self.AnyDangerousGoodsonBoard(data ? data.ArrivalNotification.AnyDangerousGoodsonBoard : "N");
    self.ATB(data ? (data.ATB || null) : null);
    self.AllocatedBerth(data ? (data.AllocatedBerth || "") : "");
    self.AllocatedFromBollardName(data ? (data.AllocatedFromBollardName || "") : "");
    self.AllocatedToBollardName(data ? (data.AllocatedToBollardName  || "") : "");

    self.DraftFWD(data ? data.DraftFWD : "");
    self.DraftAFT(data ? data.DraftAFT : "");
    self.VesselType(data ? (data.ArrivalNotification ? (data.ArrivalNotification.Vessel ? data.ArrivalNotification.Vessel.VesselType : "") : "") : "");

    self.PassengersEmbarking(data ? data.PassengersEmbarking : "");
    self.PassengersDisembarking(data ? data.PassengersDisembarking : "");
    self.Tidal(data ? (data.ArrivalNotification ? data.ArrivalNotification.Tidal == "I" ? "No" : "Yes" : data.Tidal == "I" ? "No" : "Yes") : "");
    self.IsSpecialNature(data ? (data.ArrivalNotification ? data.ArrivalNotification.IsSpecialNature == "I" ? "No" : "Yes" : data.IsSpecialNature == "I" ? "No" : "Yes") : "");
    self.BPWorkflowInstanceId(data ? data.BPWorkflowInstanceId : 0);
    self.IsConfirmCancel(data ? data.IsConfirmCancel : "N");
    self.SlotStatus(data ? (data.SlotStatus || "") : "");
    
    self.EditMovementDate(data ? moment(data.MovementDateTime).format('YYYY-MM-DD HH:mm') || "" : "");
    self.EditMovementSlot(data ? (data.MovementSlot || "") : "");    

    self.cache.latestData = data;

}
IPMSROOT.ServiceRequestModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}


function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9.\b]/;
    return charcheck.test(keychar);
}

