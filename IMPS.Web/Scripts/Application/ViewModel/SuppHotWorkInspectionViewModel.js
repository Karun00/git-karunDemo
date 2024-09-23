(function (IPMSRoot) {

    var SuppHotWorkInspectionViewModel = function () {

        var self = this;
        $('#spnTitile').html("Hot Work Permit Inspection List");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.SuppHotWorkInspectionModel = ko.observable();
        self.SuppHotWorkInspectionList = ko.observableArray();
        self.AllSuppHotWorkInspectionList = ko.observableArray();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.editableView = ko.observable();
        self.IsPrintPemit = ko.observable(false);
        self.UserName = ko.observable();
        self.Designation = ko.observable();
        self.currSuppHotWorkInspectionID = ko.observable();
        self.IsVerify = ko.observable(false);
        
        //self.viewMode = ko.observable(true);

        // self.DivingLogDateTime = ko.observable();

        // Intilaize the model view
        self.Initialize = function () {
            self.viewMode(true);
            self.SuppHotWorkInspectionModel(new IPMSROOT.SuppHotWorkInspectionModel());
            self.LoadGrid();
            self.viewMode('List');
            self.UserDetailsByID();
            //self.viewMode('List');
        }

        // Grid / list
        self.LoadGrid = function () {
            self.viewModelHelper.apiGet('api/SuppHotWorkInspections', null, function (result) {
                self.SuppHotWorkInspectionList(ko.utils.arrayMap(result, function (item) {
                    if (item.SuppHotWorkInspectionVO != null) {
                        if (!(item.SuppHotWorkInspectionVO.EmergencyProcedure == "Y" && item.SuppHotWorkInspectionVO.FireRiskAssessment == "Y" && item.SuppHotWorkInspectionVO.FlammableGases == "Y" && item.SuppHotWorkInspectionVO.GasMonitoring == "Y" && item.SuppHotWorkInspectionVO.FireDetectors == "Y" && item.SuppHotWorkInspectionVO.EquipmentCondition == "Y" && item.SuppHotWorkInspectionVO.ConductiveMetals == "Y" && item.SuppHotWorkInspectionVO.EquipmentStandby == "Y" && item.SuppHotWorkInspectionVO.HighProtection == "Y" && item.SuppHotWorkInspectionVO.AdequateVentilation == "Y" && item.SuppHotWorkInspectionVO.BarricadesRequired == "Y" && item.SuppHotWorkInspectionVO.SymbolicSafetyScience == "Y" && item.SuppHotWorkInspectionVO.PersonalProtective == "Y" && item.SuppHotWorkInspectionVO.TrainedFireWatch == "Y" && item.SuppHotWorkInspectionVO.PostWelding == "Y" && item.SuppHotWorkInspectionVO.HouseKeepingPractices == "Y")) {
                            item.SuppHotWorkInspectionVO.HWPNText = "";
                            //item.SuppHotWorkInspectionVO.PermitStatus = "Issued";
                            item.SuppHotWorkInspectionVO.value = "N";

                        }
                        else {
                            item.SuppHotWorkInspectionVO.HWPNText = item.SuppHotWorkInspectionVO.HWPN;
                            //item.SuppHotWorkInspectionVO.PermitStatus = "Y";
                            item.SuppHotWorkInspectionVO.value = "Y";

                        }
                       
                    }
                    return new IPMSRoot.SuppHotWorkInspectionModel(item);
                }));
            });
        }
        // User Details
        self.UserDetailsByID = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.viewModelHelper.apiGet('api/UserDetailsByID', null, function (result) {

                self.UserName(result.FirstName + " " + result.LastName);
                self.Designation(result.Designation);
                return;
            });
        }


        //Commented By Omprakash On 4th September 2014 
        //////// Saves the Hot Work Permit Inspection Details
        //////self.SaveSuppHotWorkInspection = function (model) {
        //////    model.validationEnabled(true);
        //////    self.SuppHotWorkInspectionValidation = ko.observable(model);
        //////    self.SuppHotWorkInspectionValidation().errors = ko.validation.group(self.SuppHotWorkInspectionValidation());
        //////    var errors = self.SuppHotWorkInspectionValidation().errors().length;

        //////    if (errors == 0) {
        //////        self.viewModelHelper.apiPost('api/SuppHotWorkInspections', ko.mapping.toJSON(model), function Message(data) {
        //////            toastr.success("Hot Work Permit Inspection details saved successfully", "SuppHotWorkInspection");
        //////            self.LoadGrid();
        //////            self.viewMode('List');
        //////        });
        //////        //self.cancel();
        //////    }
        //////    else {
        //////        self.SuppHotWorkInspectionValidation().errors.showAllMessages();
        //////        $('#divValidationError').removeClass('display-none');
        //////        return;
        //////    }
        //////}

        //  Updates the Hot Work Permit Inspection details
        self.ModifySuppHotWorkInspection = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if ($("#txtAdditionalConditions").val().length == 0 || $("#txtRemarks").val().length == 0) {
                toastr.warning("* Please fill mandatory fields");
                return;
            }
            model.validationEnabled(true);
            self.SuppHotWorkInspectionValidation = ko.observable(model);
            self.SuppHotWorkInspectionValidation().errors = ko.validation.group(self.SuppHotWorkInspectionValidation());
            var errors = self.SuppHotWorkInspectionValidation().errors().length;

            var v1 = model.SuppHotWorkInspectionVO._latestValue.EmergencyProcedure();
            var v2 = model.SuppHotWorkInspectionVO._latestValue.FireRiskAssessment();
            var v3 = model.SuppHotWorkInspectionVO._latestValue.FlammableGases();
            var v4 = model.SuppHotWorkInspectionVO._latestValue.GasMonitoring();

            var v5 = model.SuppHotWorkInspectionVO._latestValue.FireDetectors();
            var v6 = model.SuppHotWorkInspectionVO._latestValue.EquipmentCondition();
            var v7 = model.SuppHotWorkInspectionVO._latestValue.ConductiveMetals();
            var v8 = model.SuppHotWorkInspectionVO._latestValue.EquipmentStandby();
            var v9 = model.SuppHotWorkInspectionVO._latestValue.HighProtection();
            var v10 = model.SuppHotWorkInspectionVO._latestValue.AdequateVentilation();
            var v11 = model.SuppHotWorkInspectionVO._latestValue.BarricadesRequired();
            var v12 = model.SuppHotWorkInspectionVO._latestValue.SymbolicSafetyScience();
            var v13 = model.SuppHotWorkInspectionVO._latestValue.PersonalProtective();
            var v14 = model.SuppHotWorkInspectionVO._latestValue.TrainedFireWatch();
            var v15 = model.SuppHotWorkInspectionVO._latestValue.PostWelding();
            var v16 = model.SuppHotWorkInspectionVO._latestValue.HouseKeepingPractices();

            if (v1 == 'Y' && v2 == 'Y' && v3 == 'Y' && v4 == 'Y' && v5 == 'Y' && v6 == 'Y' && v7 == 'Y' && v8 == 'Y'
                && v9 == 'Y' && v10 == 'Y' && v11 == 'Y' && v12 == 'Y' && v13 == 'Y' && v14 == 'Y' && v15 == 'Y' && v16) {
                model.ActionName = "Verify";
            }
            else {
                model.ActionName = "Update";
            }

            if (errors == 0) {
                
                self.viewModelHelper.apiPut('api/SuppHotWorkInspections', ko.mapping.toJSON(model), function Message(data) {
                    toastr.success("Hot work permit inspection details updated successfully.", "SuppHotWorkInspection");
                });
                //self.cancel();
                self.Initialize();
            }
            else {
                self.SuppHotWorkInspectionValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }
        self.VerifySuppHotWorkInspection = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if ($("#txtAdditionalConditions").val().length == 0 || $("#txtRemarks").val().length == 0) {
                toastr.warning("* Please fill mandatory fields");
                return;
            }
            model.validationEnabled(true);
            self.SuppHotWorkInspectionValidation = ko.observable(model);
            self.SuppHotWorkInspectionValidation().errors = ko.validation.group(self.SuppHotWorkInspectionValidation());
            var errors = self.SuppHotWorkInspectionValidation().errors().length;

            if (errors == 0) {
                model.ActionName = "Verify";
                self.viewModelHelper.apiPut('api/SuppHotWorkInspections', ko.mapping.toJSON(model), function Message(data) {
                    toastr.success("Hot work permit inspection details Verified successfully.", "SuppHotWorkInspection");
                });
                //self.cancel();
                self.Initialize();
            }
            else {
                self.SuppHotWorkInspectionValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        // Deletes the Hot Work Permit Inspection
        self.DeleteSuppHotWorkInspection = function (model) {
            // confirmation box - start
            $.confirm({
                'title': ' Delete Hot Work Permit Inspection',
                'message': "Are you sure you want to delete hot work permit inspection(" + model.SubCatCode() + ")",
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            self.viewModelHelper.apiPut('api/SuppHotWorkInspection/PostDeleteSuppHotWorkInspectionData/' + ko.mapping.toJSON(model.SubCatCode), null, function (result) {
                                self.SuppHotWorkInspectionList.remove(model);
                            });
                        }
                    },
                    'No': {
                        'class': 'gray',
                        'action': function () {

                        }
                    }
                }
            });
            //confirmation box - end
        }

        // Add new mode
        self.addSuppHotWorkInspection = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsVerify(true);
            self.SuppHotWorkInspectionModel(new IPMSRoot.SuppHotWorkInspectionModel());
            $('#spnTitile').html("Add Hot Work Permit Inspection");

        }

        // View mode
        self.viewSuppHotWorkInspection = function (SuppHotWorkInspection) {
            SuppHotWorkInspection.FromDate(moment(SuppHotWorkInspection.FromDate()).format('YYYY-MM-DD HH:mm'));
            SuppHotWorkInspection.ToDate(moment(SuppHotWorkInspection.ToDate()).format('YYYY-MM-DD HH:mm'));


            var v1 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EmergencyProcedure();
            var v2 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FireRiskAssessment();
            var v3 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FlammableGases();
            var v4 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.GasMonitoring();

            var v5 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FireDetectors();
            var v6 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EquipmentCondition();
            var v7 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.ConductiveMetals();
            var v8 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EquipmentStandby();
            var v9 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.HighProtection();
            var v10 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.AdequateVentilation();
            var v11 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.BarricadesRequired();
            var v12 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.SymbolicSafetyScience();
            var v13 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.PersonalProtective();
            var v14 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.TrainedFireWatch();
            var v15 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.PostWelding();
            var v16 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.HouseKeepingPractices();

            if (v1 == 'Y' && v2 == 'Y' && v3 == 'Y' && v4 == 'Y' && v5 == 'Y' && v6 == 'Y' && v7 == 'Y' && v8 == 'Y'
                && v9 == 'Y' && v10 == 'Y' && v11 == 'Y' && v12 == 'Y' && v13 == 'Y' && v14 == 'Y' && v15 == 'Y' && v16) {
                self.IsPrintPemit(true);
                self.IsUpdate(false);
                self.IsReset(false);
                self.editableView(false);
                self.IsVerify(false);
            }
            else {
                self.IsPrintPemit(false);
                self.IsUpdate(true);
                self.IsReset(true);
                self.editableView(true);
                self.IsVerify(true);
            }

            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.IsVerify(false);

            self.SuppHotWorkInspectionModel(SuppHotWorkInspection);
            $('#spnTitile').html("View Hot Work Permit Inspection");

            $("#UserName").text(self.UserName());
            $("#Designation").text(self.Designation());


        }

       
        // Update / Edit Mode
        self.editSuppHotWorkInspection = function (SuppHotWorkInspection) {

            
            SuppHotWorkInspection.FromDate(moment(SuppHotWorkInspection.FromDate()).format('YYYY-MM-DD HH:mm'));
            SuppHotWorkInspection.ToDate(moment(SuppHotWorkInspection.ToDate()).format('YYYY-MM-DD HH:mm'));
            

            //SuppHotWorkInspection.SuppHotWorkInspectionVO().PermitStatus()
            if (SuppHotWorkInspection.SuppHotWorkInspectionVO().SuppHotWorkInspectionID() != null) {
                self.currSuppHotWorkInspectionID(SuppHotWorkInspection.SuppHotWorkInspectionVO().SuppHotWorkInspectionID());
            }
            else {
                self.currSuppHotWorkInspectionID(null);
            }

            var v1 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EmergencyProcedure();
            var v2 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FireRiskAssessment();
            var v3 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FlammableGases();
            var v4 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.GasMonitoring();

            var v5 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FireDetectors();
            var v6 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EquipmentCondition();
            var v7 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.ConductiveMetals();
            var v8 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EquipmentStandby();
            var v9 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.HighProtection();
            var v10 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.AdequateVentilation();
            var v11 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.BarricadesRequired();
            var v12 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.SymbolicSafetyScience();
            var v13 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.PersonalProtective();
            var v14 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.TrainedFireWatch();
            var v15 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.PostWelding();
            var v16 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.HouseKeepingPractices();

            if (v1 == 'Y' && v2 == 'Y' && v3 == 'Y' && v4 == 'Y' && v5 == 'Y' && v6 == 'Y' && v7 == 'Y' && v8 == 'Y'
                && v9 == 'Y' && v10 == 'Y' && v11 == 'Y' && v12 == 'Y' && v13 == 'Y' && v14 == 'Y' && v15 == 'Y' && v16) {
                self.IsPrintPemit(true);
                self.IsUpdate(false);
                self.IsReset(false);
                self.editableView(false);
                self.IsVerify(false);
            }
            else {
                self.IsPrintPemit(false);
                self.IsUpdate(true);
                self.IsReset(true);
                self.editableView(true);
                self.IsVerify(false);
            }

            self.viewMode('Form');
            //self.IsUpdate(true);
            //self.IsSave(true);
            //self.IsReset(true);
            //self.editableView(true);
            //self.IsCodeEnable(true);
            self.SuppHotWorkInspectionModel(SuppHotWorkInspection);
            $('#spnTitile').html("Update Hot Work Permit Inspection");
            //var valuserName = ;
            $("#UserName").text(self.UserName());
            $("#Designation").text(self.Designation());

        }


        self.verifyEditSuppHotWorkInspection = function (SuppHotWorkInspection) {


            SuppHotWorkInspection.FromDate(moment(SuppHotWorkInspection.FromDate()).format('YYYY-MM-DD HH:mm'));
            SuppHotWorkInspection.ToDate(moment(SuppHotWorkInspection.ToDate()).format('YYYY-MM-DD HH:mm'));


            //SuppHotWorkInspection.SuppHotWorkInspectionVO().PermitStatus()
            if (SuppHotWorkInspection.SuppHotWorkInspectionVO().SuppHotWorkInspectionID() != null) {
                self.currSuppHotWorkInspectionID(SuppHotWorkInspection.SuppHotWorkInspectionVO().SuppHotWorkInspectionID());
            }
            else {
                self.currSuppHotWorkInspectionID(null);
            }

            var v1 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EmergencyProcedure();
            var v2 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FireRiskAssessment();
            var v3 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FlammableGases();
            var v4 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.GasMonitoring();

            var v5 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.FireDetectors();
            var v6 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EquipmentCondition();
            var v7 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.ConductiveMetals();
            var v8 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.EquipmentStandby();
            var v9 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.HighProtection();
            var v10 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.AdequateVentilation();
            var v11 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.BarricadesRequired();
            var v12 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.SymbolicSafetyScience();
            var v13 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.PersonalProtective();
            var v14 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.TrainedFireWatch();
            var v15 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.PostWelding();
            var v16 = SuppHotWorkInspection.SuppHotWorkInspectionVO._latestValue.HouseKeepingPractices();

            if (v1 == 'Y' && v2 == 'Y' && v3 == 'Y' && v4 == 'Y' && v5 == 'Y' && v6 == 'Y' && v7 == 'Y' && v8 == 'Y'
                && v9 == 'Y' && v10 == 'Y' && v11 == 'Y' && v12 == 'Y' && v13 == 'Y' && v14 == 'Y' && v15 == 'Y' && v16) {
                self.IsPrintPemit(true);
                self.IsUpdate(false);
                self.IsReset(false);
                self.editableView(false);
                self.IsVerify(false);
            }
            else {
                self.IsPrintPemit(false);
                self.IsUpdate(false);
                self.IsReset(true);
                self.editableView(true);
                self.IsVerify(true);
            }

            self.viewMode('Form');
            //self.IsUpdate(true);
            //self.IsSave(true);
            //self.IsReset(true);
            //self.editableView(true);
            //self.IsCodeEnable(true);
            self.SuppHotWorkInspectionModel(SuppHotWorkInspection);
            $('#spnTitile').html("Verify Hot Work Permit Inspection");
            //var valuserName = ;
            $("#UserName").text(self.UserName());
            $("#Designation").text(self.Designation());

        }

        self.PDFGeneration = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if (model.SuppHotWorkInspectionVO().PermitStatus() == "New") {
                self.viewModelHelper.apiPost('api/SuppHotWorkInspection/PostModifyHotWorkInspectionPermitStatus', ko.mapping.toJSON(model),
                          function (result) {
                              document.getElementById("Update").style.visibility = "hidden";
                              toastr.success("Hot work permit inspection status updated successfully.", "SuppHotWorkInspection");

                          });
            }

            var doc = new jsPDF();
            var yLocation = 20;
            var xLocation = 10;
            var FontSize = 9;

            yLocation = yLocation + 5;
            doc.setFontSize(15);
            doc.setFontType("bold");
            doc.text(75, yLocation, 'Hot Work Permit');
            doc.setFontType("normal");
            doc.setFontSize(FontSize);
            doc.text(150, yLocation, 'PERMIT No: ' + $("#PrintSuppHotWorkInspectionID").text());

            yLocation = yLocation + 3;
            doc.setLineWidth(0.5);
            doc.line(10, yLocation, 200, yLocation);

            yLocation = yLocation + 10;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'VCN');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#PrintVCN").text());


            //yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(120, yLocation, 'Vessel Name');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#PrintVesselName").text());

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Service Request');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#PrintServiceRequest").text());

            // yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(120, yLocation, 'Current Berth');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, '');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Service Requested at Berth');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#PrintBerthName").text());

            // yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(120, yLocation, 'Request Date');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#PrintRequestDate").text());

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Location');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#PrintLocationName").text());

            // yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(120, yLocation, 'Request From');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#PrintFromDate").text());

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Gas Free Certificate Issuing Authority');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#PrintGassFreeIssuingAuthority").text());

            //yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(120, yLocation, 'Request To');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#PrintToDate").text());

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Gas Free Certificate Available');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#PrintGassCertificateAvailable").text());

            //yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(120, yLocation, 'Gas Free Certificate Validity');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#PrintGassFreeCertificateValidity").text());

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Dangerous Goods Status');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#PrintAnyDangerousGoodsonBoardt").text());

            //yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(120, yLocation, 'Class');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#PrintDangerousGoodsClass").text());

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'UN No.');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#PrintUNNo").text());

            yLocation = yLocation + 10;
            doc.setFontType("bold");
            doc.text(10, yLocation, 'Uploaded Documents : ');
            doc.setFontType("normal");
            yLocation = yLocation + 1;
            doc.setLineWidth(0.5);
            doc.line(10, yLocation, 43, yLocation);
            yLocation = yLocation + 5;

            self.FileName = ko.observableArray();
            self.FileName = self.SuppHotWorkInspectionModel().SuppHotColdWorkPermitsVO().SuppHotColdWorkPermitDocumentsVO();
            if (self.FileName.length > 0) {
                $.each(self.FileName, function (key, value) {
                    doc.setFontSize(FontSize);
                    doc.text(10, yLocation, value.FileName());
                    yLocation = yLocation + 5;
                });
            }
            else {
                doc.setFontSize(FontSize);
                doc.text(10, yLocation, 'No Uploaded Documents.');
                yLocation = yLocation + 5;
            }


            yLocation = yLocation + 5;
            doc.setFontType("bold");
            doc.text(10, yLocation, 'Inspection Details : ');
            doc.setFontType("normal");
            yLocation = yLocation + 1;
            doc.setLineWidth(0.5);
            doc.line(10, yLocation, 38, yLocation);



            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'The person conducting the hot work is familiar with the emergency evacuation procedures.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Is the hot work area and surrounding area free of any combustibles - 10 meters minimum (Fire Risk Assessment).');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Has the hot work area been tested for flammable gases.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Is continuous gas monitoring required and agree upon.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Have smoke/fire detectors been disabled locally.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Hot work equipment is in good condition (Oxygen/Acetylene Cylinders, Hoses, Arc welding equipment/cables).');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Arc welding area dry (no water) and insulated mat used when working with conductive metals.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Fire equipment on standby (Fire Extinguisher, Hose Reel, Fire Blanket and First aid kit).');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Welding curtains available and shade for eye protection sufficient for the amperage.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Provide Adequate Ventilation:');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Barricades mandatory.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Symbolic Safety Signs mandatory.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Personal Protective Equipment (PPE) requirement.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Trained fire watch is mandatory and agreed upon.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Post welding/cutting (Hot work) fire watch minimum of half hour.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Good housekeeping practices and designated area for sharp scraps, electrode stubs and debris.');
            doc.text(180, yLocation, 'Yes');

            yLocation = yLocation + 10;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Name');
            doc.text(30, yLocation, ': ');
            doc.text(35, yLocation, self.UserName() + '');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Designation');
            doc.text(30, yLocation, ':');
            doc.text(35, yLocation, self.Designation());

            yLocation = yLocation + 15;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'Authorized Signature:');

            yLocation = yLocation + 15;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'I verify that the job area has been examined and authorize hot work to be carried out providing the above precautions and conditions');

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(xLocation, yLocation, 'are mentioned throughtout the term of their permit.');



            //// Save the PDF
            doc.save('HotWorkPermit.pdf');
            // self.SuppHotWorkInspectionModel().reset();
            $("#classadd div").removeClass("modal-backdrop fade in");

            self.LoadGrid();

            self.viewMode('List');
            $('#spnTitile').html("Hot Work Permit Inspection List");
            $("#SuppHotWorkInspection div").trigger("click");
        }


        // Reset button
        self.ResetSuppHotWorkInspection = function (model) {
            ko.validation.reset();
            self.SuppHotWorkInspectionModel().reset();
            self.SuppHotWorkInspectionValidation().errors.showAllMessages(false);
            if ($('#divValidationError').is(':visible')) {
                $('#divValidationError').css('display', 'none');
            }

        }

        // Cancel Button
        self.cancel = function () {
            self.SuppHotWorkInspectionModel().reset();
            self.LoadGrid();

            self.viewMode('List');
            $('#spnTitile').html("Hot Work Permit Inspection List");

        }



        //Validate Alphabets With Spaces
        ValidateAlphabetsWithSpaces = function (event) {
            return self.validationHelper.ValidateAlphabetsWithSpaces_keypressEvent(this, event);
        }
        function myFunction() {
            //function createLBRBF(hiddenField) {
            var f = this.getField("myApparelChoice");
            if (f != null) this.removeField("myApparelChoice");
            var fieldtype = event.target.value;
            var btn = this.getField(hiddenField);
            var myRect = btn.rect;
            var width = 72;
            var height = (fieldtype == "listbox") ? 5 * 13 : 13;
            myRect[2] = myRect[0] + width;
            myRect[3] = myRect[1] - height;
            var f = this.addField("myApparelChoice", fieldtype, this.pageNum, myRect);
            f.setItems([["Socks", "1"], ["Shoes", "2"], ["Pants", "3"],
            ["Shirt", "4"], ["Tie", "5"]]);
            f.setAction("Keystroke", "lbrbfKeystroke()");
            f.delay = true;
            f.defaultValue = "1"; f.value = "1";
            f.strokeColor = ["RGB", 0, 0.6, 0];
            f.fillColor = ["RGB", 0.98, 0.92, 0.73];
            f.delay = false;
            //}



        }
        self.Initialize();
    }
    IPMSRoot.SuppHotWorkInspectionViewModel = SuppHotWorkInspectionViewModel;


}(window.IPMSROOT));



