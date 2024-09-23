(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });


    var PortEntryPassApplicationReferenceData = function (data) {
        var self = this;
        
        self.Ports = ko.observableArray(data ? $.map(data.Ports, function (item) { return new PortRequesttype(item); }) : []);
        self.ApplicationCategory = ko.observableArray(data ? $.map(data.ApplicationCategory, function (item) { return new appcategory(item); }) : []);
        self.PermitTypes = ko.observableArray(data ? $.map(data.PermitTypes, function (item) { return new PermitType(item); }) : []);
        self.AreaOfConstruction = ko.observableArray(data ? $.map(data.AreaOfConstruction, function (item) { return new areaofconstruction(item); }) : []);
        self.TemporaryPermits = ko.observableArray(data ? $.map(data.TemporaryPermits, function (item) { return new TemporaryPermit(item); }) : []);
        self.AdhocPermits = ko.observableArray(data ? $.map(data.AdhocPermits, function (item) { return new AdhocPermit(item); }) : []);
        self.OperatedTerminals = ko.observableArray(data ? $.map(data.OperatedTerminals, function (item) { return new OperatedTerminal(item); }) : []);
        self.PermitDocumentTypes = ko.observableArray(data ? $.map(data.PermitDocumentTypes, function (item) { return new PermitDocumentType(item); }) : []);
        self.SecurityDocumentTypes = ko.observableArray(data ? $.map(data.SecurityDocumentTypes, function (item) { return new SecurityDocumentType(item); }) : []);
        self.PermitRequeirements = ko.observableArray(data ? $.map(data.PermitRequeirements, function (item) { return new PermitRequeirement(item); }) : []);
        self.AccessGates = ko.observableArray(data ? $.map(data.AccessGates, function (item) { return new AccessGate(item); }) : []);
        self.PermitRequeirementstypes = ko.observableArray(data ? $.map(data.PermitRequeirementstypes, function (item) { return new PermitRequeirementstype(item); }) : []);
        self.PermitRequeirementsDuration = ko.observableArray(data ? $.map(data.PermitRequeirementsDuration, function (item) { return new PermitRequeirementsDurationS(item); }) : []);
        self.PermitCodes = ko.observableArray(data ? $.map(data.PermitCodes, function (item) { return new Permitcode(item); }) : []);
        self.PermitStatus = ko.observableArray(data ? $.map(data.PermitStatus, function (item) { return new PermitStatusS(item); }) : []);
    }

    var AdvnceSearchmodel = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.VesselID = ko.observable();
        //self.MovementType = ko.observable();

        self.RequestFrom = ko.observable();
        self.RequestTo = ko.observable();
        self.set(data);
    }

    var PortRequesttype = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }
    var appcategory = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitType = function (data) {
        var self = this;        
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var areaofconstruction = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var TemporaryPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AdhocPermit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var OperatedTerminal = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitDocumentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var SecurityDocumentType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitRequeirement = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var AccessGate = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitRequeirementstype = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitRequeirementsDurationS = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var Permitcode = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PermitStatusS = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //****************************************ChildModels***********************************************************************************
    // PermitRequestDocument Used For bind The upload controls from view modelPermitcodes
    var PermitRequestDocument = function (data) {
        var self = this;

        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
        self.deletevisable = ko.observable(false);

    }
    var PermitRequestArea = function (data) {
        var self = this;
        self.PermitRequestAreaCode = ko.observableArray([]);
        self.PermitRequestAreaCode = ko.observable(data ? data.PermitRequestAreaCode : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.PermitRequestAreaID = ko.observable(data ? data.PermitRequestAreaID : "");
    }
    var PermitRequestContractor = function (data) {
        var self = this;
        self.PermitRequestContractorID = ko.observable(data ? data.PermitRequestContractorID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.CompanyName = ko.observable(data ? data.CompanyName : "");
        self.ContractNo = ko.observable(data ? data.ContractNo : "");
        self.ContractManagerName = ko.observable(data ? data.ContractManagerName : "");
        self.ContractDuration = ko.observable(data ? data.ContractDuration : "");
        self.ServiceCompanyName = ko.observable(data ? data.ServiceCompanyName : "");
        self.ResponsibleManager = ko.observable(data ? data.ResponsibleManager : "");
        self.ContactNo = ko.observable(data ? data.ContactNo : "");
        self.MobileNo = ko.observable(data ? data.MobileNo : "");
    }
    var VehiclePermit = function (data) {
        var self = this;
        self.VehiclePermitID = ko.observable(data ? data.VehiclePermitID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.VehicleMake = ko.observable(data ? data.VehicleMake : "");
        self.VehicleRegnNo = ko.observable(data ? data.VehicleRegnNo : "");
        self.PermitRequirementCode = ko.observable(data ? data.PermitRequirementCode : "");
        self.Reason = ko.observable(data ? data.Reason : "");
    }
    var VisitorPermit = function (data) {
        var self = this;
        self.VisitorPermitID = ko.observable(data ? data.VisitorPermitID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.CompanyName = ko.observable(data ? data.CompanyName : "");
        self.Reason = ko.observable(data ? data.Reason : "");
        self.AuthorizedPersonName = ko.observable(data ? data.AuthorizedPersonName : "");
        self.Division = ko.observable(data ? data.Division : "");
        self.PositionHeld = ko.observable(data ? data.PositionHeld : "");
        self.EscortName = ko.observable(data ? data.EscortName : "");
        self.TelephoneNo = ko.observable(data ? data.TelephoneNo : "");
        self.PermitNo = ko.observable(data ? data.PermitNo : "");
        self.PermitCode = ko.observable(data ? data.PermitCode : "");
    }
    var WharfVehiclePermit = function (data) {
        var self = this;
        self.WharfVehiclePermitID = ko.observable(data ? data.WharfVehiclePermitID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.VehicleMake = ko.observable(data ? data.VehicleMake : "");
        self.Hometelephone = ko.observable(data ? data.Hometelephone : "");
        self.VehicleModel = ko.observable(data ? data.VehicleModel : "");
        self.VehicleRegnNo = ko.observable(data ? data.VehicleRegnNo : "");
        self.VehicleDescription = ko.observable(data ? data.VehicleDescription : "");
        self.VehicleRegisterd = ko.observable(data ? data.VehicleRegisterd : "");
        self.VehiclePointed = ko.observable(data ? data.VehiclePointed : "");
        self.Reason = ko.observable(data ? data.Reason : "");
        self.MobileNo = ko.observable(data ? data.MobileNo : "");
        self.TelephoneNo = ko.observable(data ? data.TelephoneNo : "");
        self.ContractDuration = ko.observable(data ? data.ContractDuration : "");
        self.PermitRequirement = ko.observable(data ? data.PermitRequirement : "");
        self.ContractorNo = ko.observable(data ? data.ContractorNo : "");
        self.TemporaryPermits = ko.observable(data ? data.TemporaryPermits : "");
        self.AccessGates = ko.observable(data ? data.AccessGates : "");
        self.OtherSpecify = ko.observable(data ? data.OtherSpecify : "");
    }
    var PersonalPermit = function (data) {
        var self = this;
        
        self.PersonalPermitID = ko.observable(data ? data.PersonalPermitID : "");
        self.PermitRequestID = ko.observable(data ? data.PermitRequestID : "");
        self.PermitCategoryCode = ko.observable(data ? data.PermitCategoryCode : "");
        self.AllNPASites = ko.observable(data ? data.AllNPASites : "");
        self.SpecificNPASites = ko.observable(data ? data.SpecificNPASites : "");
        self.SpecifyArea = ko.observable(data ? data.SpecifyArea : "");
        self.LeaseholdSite = ko.observable(data ? data.LeaseholdSite : "").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Specify Name And Specific Location of Lease Holding Site' } });
        self.PhysicalAddress = ko.observable(data ? data.PhysicalAddress : "").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Physical Address' } });
        self.AdhocPermits = ko.observable(data ? data.AdhocPermits : "");
        self.TemporaryPermits = ko.observable(data ? data.TemporaryPermits : "");
        self.AllPorts = ko.observable(data ? data.AllPorts : "");
        self.ConstructionArea = ko.observable(data ? data.ConstructionArea : "");
        self.PermanentPermits = ko.observable(data ? data.PermanentPermits : "");
        self.Reason = ko.observable(data ? data.Reason : "").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Reason' } });
        self.permittype = ko.observable(data ? data.permittype : "");
        
    }
    var PermitRequestDetailsverifyedbySSA = function (data) {
        var self = this;
        self.PermitRequestverifyedID = ko.observable(data ? data.PermitRequestverifyedID : "");
        self.permitrRequestID = ko.observable(data ? data.permitrRequestID : "");
        self.CreminalCheck = ko.observable(data ? data.CreminalCheck : "");
        self.Comments = ko.observable(data ? data.Comments : "");
        self.Flag = ko.observable(data ? data.Flag : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.verifyedUserID = ko.observable(data ? data.verifyedUserID : "");
        self.verifyedDate = ko.observable(data ? data.verifyedDate : "");
    }
    var PermitRequestDetailsverifyedbySAPS = function (data) {
        var self = this;
        self.PermitRequestverifyedID = ko.observable(data ? data.PermitRequestverifyedID : "");
        self.permitrRequestID = ko.observable(data ? data.permitrRequestID : "");
        self.CreminalCheck = ko.observable(data ? data.CreminalCheck : "");
        self.Comments = ko.observable(data ? data.Comments : "");
        self.Flag = ko.observable(data ? data.Flag : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.verifyedUserID = ko.observable(data ? data.verifyedUserID : "");
        self.verifyedDate = ko.observable(data ? data.verifyedDate : "");
    }

    var PermitRequestverifyedbySSADocument = function (data) {
        var self = this;

        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
    }

    var PermitRequestverifyedbySAPSDocument = function (data) {
        var self = this;

        self.DocumentID = ko.observable(data ? data.DocumentID : "");
        self.DocumentName = ko.observable(data ? data.DocumentName : "");
        self.FileName = ko.observable(data ? data.FileName : "");
    }
    //****************************************MainModel***********************************************************************************
    var InternalEmployeePermitModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(false);
        self.PermitRequestID = ko.observable();
        self.PortCode = ko.observable("");
        self.PermitRequestTypeCode = ko.observable("");
        self.PermitRequestTypeCodeName = ko.observable();
        self.CompanyName = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Company Name' } });
        self.DepartmentName = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Department Name' } });
        self.ApplicantFullName = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Applicant Full Name' } });
        self.ApplicantSurName = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Applicant Surname' } });
        self.PensionEmployeeNo = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Pension/ Employee No.' } });
        self.IDPassportNo = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a ID/ Pass port No.' } });
        self.Occupation = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Occupation' } });
        self.Email = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Email.' }, email: { message: '* Please enter a proper email address.' }, });
        self.MobileNo = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Mobile No.' } });
        self.HomeAddress = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Home Address' } });
        self.CompanyAddress = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Company Address' } });
        self.CompanyTelephoneNo = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Company Telephone No.' } });
        self.CompanyFaxNo = ko.observable("").extend({ required: { onlyif: self.validationEnabled, message: '* Please Enter a Company Fax No.' } });
        self.RecordStatus = ko.observable();
        self.PortName = ko.observable();
        self.CreatedDate = ko.observable();
        self.permitStatusName = ko.observable();
        self.permitStatus = ko.observable();
        self.ReferenceNo = ko.observable();
        self.AppealBoardRemarks = ko.observable();
        self.PSOremarkes = ko.observable();
        self.AppealRemarks = ko.observable();
        self.Status = ko.observable();

        self.FlagSAPS = ko.observable();
        self.FlagPSSA = ko.observable();

        self.documentupload = ko.observable(false);
        self.Flag = ko.observable();



        self.isinetrnalEmpApproveVisible = ko.computed(function () {

            if (self.PermitRequestTypeCode() != null && self.PermitRequestTypeCode() != '') {
                if (self.PermitRequestTypeCode() == "APCG" || self.permitStatus() == 'IEPA' ) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);
        self.isApproveVisible = ko.computed(function () {

            if (self.PermitRequestTypeCode() != null && self.PermitRequestTypeCode() != '') {
                if (self.permitStatus() == 'PSOA' ) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);
        self.isAppealApproveVisible = ko.computed(function () {

            if (self.PermitRequestTypeCode() != null && self.PermitRequestTypeCode() != '') {
                if ( self.permitStatus() == 'PAAD' ) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        self.isissueissableAppealApproveVisible = ko.computed(function () {

            if (self.PermitRequestTypeCode() != null && self.PermitRequestTypeCode() != '') {
                if (self.permitStatus() == 'PAAD') {
                    return true;
                }
                else {
                    return false;
                }
            }
        }, this);

        //--------Arrays--------------------------------------
        self.selectedDocumentsType = ko.observableArray([]);
        self.UploadedFiles = ko.observableArray([]);
        self.PermitRequestAreas = ko.observableArray([]);

        //------------------------------------------------------

        self.toggleAssociation = function (item) {
            self.PermitRequestTypeCode(item.SubCatCode());
            self.documentupload(true);
            return true;
            //if(self.PermitRequestTypeCode=='APCA')

        };
        self.toggleAssociationrediobutton11 = function (item) {

            self.PersonalPermits().PermitCategoryCode(item.SubCatCode());
            self.documentupload(true);
            return true;
        };
        self.toggleAssociationrediobutton12 = function (item) {
            self.PersonalPermits().AdhocPermits(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton13 = function (item) {
            self.PersonalPermits().TemporaryPermits(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton14 = function (item) {
            self.PersonalPermits().ConstructionArea(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton15 = function (item) {
            self.VehiclePermits().PermitRequirementCode(item.SubCatCode());
            return true;
        };

        self.toggleAssociationrediobutton16 = function (item) {
            self.WharfVehiclePermits().PermitRequirement(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton17 = function (item) {
            self.WharfVehiclePermits().TemporaryPermits(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton18 = function (item) {
            self.WharfVehiclePermits().AccessGates(item.SubCatCode());
            return true;
        };
        self.toggleAssociationrediobutton19 = function (item) {
            self.VisitorPermits().PermitCode(item.SubCatCode());
            return true;
        };

        //----------------------------------------------------------------------------------------------------------------------------------------
        self.ApplicantFullNameSort;
        self.ApplicantFullName.subscribe(function (value) {
            self.ApplicantFullNameSort = value;
        });
        self.ReferenceNoSort;
        self.ReferenceNo.subscribe(function (value) {
            self.ReferenceNoSort = value;
        });
        self.PermitRequestTypeCodeNameSort;
        self.PermitRequestTypeCodeName.subscribe(function (value) {
            self.PermitRequestTypeCodeNameSort = value;
        });
        self.CompanyNameSort;
        self.CompanyName.subscribe(function (value) {
            self.CompanyNameSort = value;
        });
        self.IDPassportNoSort;
        self.IDPassportNo.subscribe(function (value) {
            self.IDPassportNoSort = value;
        });
        self.permitStatusNameSort;
        self.permitStatusName.subscribe(function (value) {
            self.permitStatusNameSort = value;
        });
        self.CreatedDateSort;
        self.CreatedDate.subscribe(function (value) {
            self.CreatedDateSort = value;
        });
        self.PensionEmployeeNoSort;
        self.PensionEmployeeNo.subscribe(function (value) {
            self.PensionEmployeeNoSort = value;
        });



        //-----------------------------------------------------------------------------------------------------------------------------------------
        //self.PermitRequestDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestDocuments, function (PortEntryPass) {
        //    return new PermitRequestDocument(PortEntryPass);
        //}) : []);
        self.PermitRequestDocuments = ko.observableArray([]);

        self.PermitRequestAreas = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestAreas, function (PortEntryPass) {
            return new PermitRequestArea(PortEntryPass);
        }) : []);
        self.PermitRequestContractors = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestContractors, function (PortEntryPass) {
            return new PermitRequestContractor(PortEntryPass);
        }) : []);
        self.VehiclePermits = ko.observableArray(data ? ko.utils.arrayMap(data.VehiclePermits, function (PortEntryPass) {
            return new VehiclePermit(PortEntryPass);
        }) : []);
        self.VisitorPermits = ko.observableArray(data ? ko.utils.arrayMap(data.VisitorPermits, function (PortEntryPass) {
            return new VisitorPermit(PortEntryPass);
        }) : []);
        self.WharfVehiclePermits = ko.observableArray(data ? ko.utils.arrayMap(data.WharfVehiclePermits, function (PortEntryPass) {
            return new WharfVehiclePermit(PortEntryPass);
        }) : []);
        self.PersonalPermits = ko.observableArray(data ? ko.utils.arrayMap(data.PersonalPermits, function (PortEntryPass) {
            return new PersonalPermit(PortEntryPass);
        }) : []);
        self.PermitRequestVerifyedDetailsverifyedbySSA = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySSA, function (PortEntryPass) {
            return new PermitRequestDetailsverifyedbySSA(PortEntryPass);
        }) : []);
        self.PermitRequestVerifyedDetailsverifyedbySAPS = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySAPS, function (PortEntryPass) {
            return new PermitRequestDetailsverifyedbySAPS(PortEntryPass);
        }) : []);

        //if (data.PermitRequestVerifyedDetailsverifyedbySSA().PermitRequestverifyedbySSADocuments != null) {

        self.PermitRequestverifyedbySSADocuments = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySSA.PermitRequestverifyedbySSADocuments, function (PortEntryPass) {
            return new PermitRequestverifyedbySSADocument(PortEntryPass);
        }) : []);
        //}
        //if (data.PermitRequestVerifyedDetailsverifyedbySAPS.PermitRequestverifyedbySAPSDocuments != null) {
        self.PermitRequestverifyedbySAPSDocuments = ko.observableArray(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySAPS.PermitRequestverifyedbySAPSDocuments, function (PortEntryPass) {
            return new PermitRequestverifyedbySAPSDocument(PortEntryPass);
        }) : []);
        //}

        //-----------------------------------------------------------------------------------------------------------------------------------------

        self.cache = function () { };
        self.set(data);
    }
    //****************************************MainModel END***********************************************************************************
    ipmsRoot.PermitRequestverifyedbySSADocument = PermitRequestverifyedbySSADocument;
    ipmsRoot.PermitRequestverifyedbySAPSDocument = PermitRequestverifyedbySAPSDocument;

    ipmsRoot.PermitRequestDetailsverifyedbySSA = PermitRequestDetailsverifyedbySSA;
    ipmsRoot.PermitRequestDetailsverifyedbySAPS = PermitRequestDetailsverifyedbySAPS;

    ipmsRoot.Permitcode = Permitcode;
    ipmsRoot.PermitRequeirement = PermitRequeirement;
    ipmsRoot.AccessGate = AccessGate;
    ipmsRoot.PermitRequeirementstype = PermitRequeirementstype;
    ipmsRoot.PermitRequeirementsDurationS = PermitRequeirementsDurationS;
    ipmsRoot.appcategory = appcategory;
    ipmsRoot.PermitType = PermitType;
    ipmsRoot.areaofconstruction = areaofconstruction;
    ipmsRoot.TemporaryPermit = TemporaryPermit;
    ipmsRoot.AdhocPermit = AdhocPermit;
    ipmsRoot.OperatedTerminal = OperatedTerminal;
    ipmsRoot.PermitRequestDocument = PermitRequestDocument;
    // ipmsRoot.PermitRequestArea = PermitRequestArea;
    ipmsRoot.PermitRequestContractor = PermitRequestContractor;
    ipmsRoot.VehiclePermit = VehiclePermit;
    ipmsRoot.VisitorPermit = VisitorPermit;
    ipmsRoot.WharfVehiclePermit = WharfVehiclePermit;
    ipmsRoot.PersonalPermit = PersonalPermit;
    ipmsRoot.PortEntryPassApplicationReferenceData = PortEntryPassApplicationReferenceData;
    ipmsRoot.InternalEmployeePermitModel = InternalEmployeePermitModel;
    ipmsRoot.AdvnceSearchmodel = AdvnceSearchmodel;
}(window.IPMSROOT));



var DateFormat = 'YYYY-MM-DD HH:mm';

IPMSROOT.AdvnceSearchmodel.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");

    var todaydate = new Date();
    var todate = new Date(todaydate);
    var fromdate = new Date(todaydate);
    todate.setDate(todaydate.getDate());
    fromdate.setDate(fromdate.getDate() - 1);
    self.RequestFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
    self.RequestTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
}

IPMSROOT.InternalEmployeePermitModel.prototype.set = function (data) {

    
    var self = this;
    self.Status(data ? (data.Status || "") : "")
    self.FlagSAPS(data ? (data.FlagSAPS || "") : "")
    self.FlagPSSA(data ? (data.FlagPSSA || "") : "")
    self.Flag(data ? (data.Flag || "") : "")
    self.AppealBoardRemarks(data ? (data.AppealBoardRemarks || "") : "");
    self.PSOremarkes(data ? (data.PSOremarkes || "") : "");
    self.AppealRemarks(data ? (data.AppealRemarks || "") : "");
    self.permitStatusName(data ? (data.permitStatusName || "") : "");
    self.permitStatus(data ? (data.permitStatus || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.documentupload(data ? (data.documentupload || "") : "");
    self.PermitRequestID(data ? (data.PermitRequestID || "") : "");
    self.ReferenceNo(data ? (data.ReferenceNo || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.PermitRequestTypeCodeName(data ? (data.PermitRequestTypeCodeName || "") : "");
    self.PortName(data ? (data.PortName || "") : "");
    self.Email(data ? (data.Email || "") : "");
    self.MobileNo(data ? (data.MobileNo || "") : "");
    self.PermitRequestTypeCode(data ? (data.PermitRequestTypeCode || "") : "");
    self.CompanyName(data ? (data.CompanyName || "") : "");
    self.DepartmentName(data ? (data.DepartmentName || "") : "");
    self.ApplicantFullName(data ? (data.ApplicantFullName || "") : "");
    self.ApplicantSurName(data ? (data.ApplicantSurName || "") : "");
    self.PensionEmployeeNo(data ? (data.PensionEmployeeNo || "") : "");
    self.IDPassportNo(data ? (data.IDPassportNo || "") : "");
    self.Occupation(data ? (data.Occupation || "") : "");
    self.HomeAddress(data ? (data.HomeAddress || "") : "");
    self.CompanyAddress(data ? (data.CompanyAddress || "") : "");
    self.CompanyTelephoneNo(data ? (data.CompanyTelephoneNo || "") : "");
    self.CompanyFaxNo(data ? (data.CompanyFaxNo || "") : "");
    self.CreatedDate(data ? (moment(data.CreatedDate).format('YYYY-MM-DD HH:mm') || "") : "");
    self.PermitRequestContractors(data ? new IPMSROOT.PermitRequestContractor(data.PermitRequestContractors) : new IPMSROOT.PermitRequestContractor(undefined));
    // self.PermitRequestAreas(data ? new IPMSROOT.PermitRequestArea(data.PermitRequestAreas) : new IPMSROOT.PermitRequestArea(undefined));
    self.VehiclePermits(data ? new IPMSROOT.VehiclePermit(data.VehiclePermits) : new IPMSROOT.VehiclePermit(undefined));
    self.VisitorPermits(data ? new IPMSROOT.VisitorPermit(data.VisitorPermits) : new IPMSROOT.VisitorPermit(undefined));
    self.WharfVehiclePermits(data ? new IPMSROOT.WharfVehiclePermit(data.WharfVehiclePermits) : new IPMSROOT.WharfVehiclePermit(undefined));
    self.PersonalPermits(data ? new IPMSROOT.PersonalPermit(data.PersonalPermits) : new IPMSROOT.PersonalPermit(undefined));
    self.PermitRequestVerifyedDetailsverifyedbySSA(data ? new IPMSROOT.PermitRequestDetailsverifyedbySSA(data.PermitRequestVerifyedDetailsverifyedbySSA) : new IPMSROOT.PermitRequestDetailsverifyedbySSA(undefined));
    self.PermitRequestVerifyedDetailsverifyedbySAPS(data ? new IPMSROOT.PermitRequestDetailsverifyedbySAPS(data.PermitRequestVerifyedDetailsverifyedbySAPS) : new IPMSROOT.PermitRequestDetailsverifyedbySAPS(undefined));
    //self.PermitRequestDocuments(data ? new IPMSROOT.PermitRequestDocument(data.PermitRequestDocuments) : new IPMSROOT.PermitRequestDocument(undefined));
    self.PermitRequestAreas(data ? (data.PermitRequestAreas ? $.map(data.PermitRequestAreas, function (item) { return item }) : []) : []);
    self.PermitRequestDocuments(data ? ko.utils.arrayMap(data.PermitRequestDocuments, function (PortEntryPass) {
        return new IPMSROOT.PermitRequestDocument(PortEntryPass);
    }) : []);

    self.PermitRequestverifyedbySSADocuments(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySSA.PermitRequestverifyedbySSADocuments, function (PortEntryPass) {
        return new IPMSROOT.PermitRequestverifyedbySSADocument(PortEntryPass);
    }) : []);

    self.PermitRequestverifyedbySAPSDocuments(data ? ko.utils.arrayMap(data.PermitRequestVerifyedDetailsverifyedbySAPS.PermitRequestverifyedbySAPSDocuments, function (PortEntryPass) {
        return new IPMSROOT.PermitRequestverifyedbySAPSDocument(PortEntryPass);
    }) : []);
    self.cache.latestData = data;
}

IPMSROOT.InternalEmployeePermitModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$/;
    return charcheck.test(keychar);
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