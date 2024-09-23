toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";

(function (IPMSRoot) {

    var VesselRegistrationViewModel = function (vcn, viewDetail) {

        var self = this;

        self.viewMode = ko.observable();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.vesselRegModel = ko.observable();
        self.ClassificationSocietyList = ko.observableArray();
        self.VesselTypeList = ko.observableArray();
        self.PORList = ko.observableArray();
        self.VesselNationalityList = ko.observableArray();
        self.VesselReferenceDtl = ko.observable();
        self.VesselReferenceDtl1 = ko.observable();
        self.VesselRegistrationList = ko.observableArray();
        self.VesselRegisrationDetModel = ko.observable(new IPMSROOT.VesselRegistrationModel());
        self.VesselSearchDetails = ko.observable(new IPMSROOT.VesselRegistrationModel());
        self.validationHelper = new IPMSRoot.validationHelper();
        self.IsViewfrmApproval = ko.observable('Y');

        self.isAddMode = ko.observable();
        self.isViewMode = ko.observable();
        self.isEngineGridEnable = ko.observable(true);

        //Vessel tab//
        self.isEditable = ko.observable(true);
        self.isIMONoEnable = ko.observable(true);
        self.isClassificationSocitiesEnable = ko.observable(true);
        self.isExCallSignEnable = ko.observable(true);
        self.isVesselNameEnable = ko.observable(true);
        self.isVesselTypeEnable = ko.observable(true);
        self.isNoOfBaysEnable = ko.observable(true);
        self.isCallSignEnable = ko.observable(true);
        self.isOfficialNumberEnable = ko.observable(true);
        self.isVesselBuildYearEnable = ko.observable(true);
        self.isNoOfRowsOnDeskEnable = ko.observable(true);
        self.isVesselNationalityEnable = ko.observable(true);
        self.isMMSINumberEnable = ko.observable(true);
        self.isExVesselNameEnable = ko.observable(true);

        self.isEditableEnable = ko.observable(true);
        $('#divEngineGrid').attr('disabled', true);
        self.isRecordStatusEnable = ko.observable(true);

        self.isGridsEnable = ko.observable(true);
        self.isButtonenable = ko.observable(false);
        //Vessel end tab//

        // Vessel Parameters Tab//
        self.isBeamInMEnable = ko.observable(true);
        self.isGrossRegisteredTonnageInMTEnable = ko.observable(true);
        self.isNetRegisteredTonnageInMTEnable = ko.observable(true);
        self.isParallelBodyLengthInMEnable = ko.observable(true);
        self.isDeadWeightTonnageInMTEnable = ko.observable(true);
        self.isBowToManifoldDistanceInMEnable = ko.observable(true);
        self.isSummerDeadWeightInMTEnable = ko.observable(true);
        self.isSummerDraftFWDInMEnable = ko.observable(true);
        self.isSummerDisplacementInMTEnable = ko.observable(true);
        self.isSummerDraftAFTInMEnable = ko.observable(true);
        self.isTEUCapacityEnable = ko.observable(true);
        self.isReducedGRTEnable = ko.observable(true);
        self.isBowThrusterEnable = ko.observable(true);
        // Vessel Parameters End Tab//

        // Vessel Certificates Tab//
        $('#divCertificatesGrid').attr('disabled', true);
        // Vessel Certificates End Tab//

        //Other Details tab//
        self.isBowToForwardHatchDistanceMEnable = ko.observable(true);
        self.isBowThrusterPowerKWEnable = ko.observable(true);
        self.isBowToBridgeFrontDistanceMEnable = ko.observable(true);
        self.isSternThrusterPowerKWEnable = ko.observable(true);
        self.isGovtVesselEnable = ko.observable(true);
        $('#divHatchHoldGrid').attr('disabled', true);
        $('#divGearGrid').attr('disabled', true);
        $('#divGrabGrid').attr('disabled', true);
        //Other Details end tab//

        self.futureDate = ko.observable();
        self.isFutureDateReadonly = ko.observable(false);

        $('#SaveExit').hide();
        $('#UpdateExit').hide();
        $('#reqTEUCapacity').hide();
        self.OnSelectFutureDate = function (data) {

            var fDate = new Date(data.DateOfIssue());
            fDate.setDate(fDate.getDate() + 1);
            self.futureDate(fDate);
            self.isFutureDateReadonly(true);
        }

        self.LoadRegReferences = function () {

            self.viewModelHelper.apiGet('api/ReferenceData', null,
               function (result1) {
                   self.VesselReferenceDtl(new IPMSRoot.VesselReferenceData(result1));
               }, null, null, false);
        }

        // To get vessel registration data
        self.LoadVesselRegistrationData = function () {

            if (viewDetail == true) {
                self.viewModelHelper.apiGet('api/VesselRegistration/' + vcn, { vcn: vcn }, function (result) {
                    self.VesselRegistrationList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.VesselRegistrationModel(item);
                    }));
                    self.viewData(self.VesselRegistrationList()[0]);
                });

            }
            else {
                self.viewModelHelper.apiGet('api/VesselRegistration', null, function (result) {
                    self.VesselRegistrationList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.VesselRegistrationModel(item);
                    }));

                    var grid = $("#vesselList").data("kendoGrid");

                    if (self.VesselRegistrationList().length <= 5)
                        grid.dataSource.pageSize(5);
                    else
                        grid.dataSource.pageSize(20);

                    grid.refresh();

                });
            }

        }

        self.changevalue = function () {
            $(".validationError").hide();
        }

        //To add vessel engine details
        self.AddEngineDet = function (engineDet) {

            $(".validationError").hide();
            if (engineDet.VesselEngines().length > 0) {

                var ManError = "Y";
                $.each(engineDet.VesselEngines(), function (index, item) {

                    if (item.EnginePower() == "" && item.EngineType() == null && item.PropulsionType() == null && item.NoOfPropellers() == "" && item.MaxSpeed() == "") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please enter engine details.", "Vessel Registration");
                        ManError = "N";
                    }
                });

                if (ManError == "Y")
                    self.VesselRegisrationDetModel().VesselEngines.push(new IPMSROOT.VesselEngineData());
            }
            else {
                self.VesselRegisrationDetModel().VesselEngines.push(
                new IPMSROOT.VesselEngineData());
            }
        }

        //validate decimal value
        self.ValidateDecimal = function (data, event) {

            if ((event.which != 46 || $("#BeamInM").val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                return false;
            }
            else {
                return true;
            }

        }

        self.ValidateDecimalLOA = function (data, event) {
            if ((event.which != 46 || $("#LengthOverallInM").val().indexOf('.') != -1) && (event.which < 48 || event.which > 57))
                return false;

            return true;

        }

        self.ValidateDecimalGRT = function (data, event) {

            if ((event.which != 46 || $("#GrossRegisteredTonnageInMT").val().indexOf('.') != -1) && (event.which < 48 || event.which > 57))
                return false;

            return true;

        }

        self.ValidateDecimalNRT = function (data, event) {

            if ((event.which != 46 || $("#NetRegisteredTonnageInMT").val().indexOf('.') != -1) && (event.which < 48 || event.which > 57))
                return false;

            return true;

        }

        self.ValidateDecimalDWT = function (data, event) {

            if ((event.which != 46 || $("#DeadWeightTonnageInMT").val().indexOf('.') != -1) && (event.which < 48 || event.which > 57))
                return false;

            return true;

        }

        self.ValidateDecimalSummer = function (data, event) {

            if ((event.which != 46 || $("#SummerDeadWeightInMT").val().indexOf('.') != -1) && (event.which < 48 || event.which > 57))
                return false;

            return true;

        }

        self.ValidateChangevalue = function () {


            if ($("#BeamInM").val() != "" && $("#LengthOverallInM").val() != "") {

                if (parseFloat($("#BeamInM").val()) > parseFloat($("#LengthOverallInM").val())) {

                    $("#BeamInM").val('');
                    $("#BeamInM").focus();
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Beam length should not be more than LOA.", "Vessel Registration");
                    return false;
                }

            }

        }

        self.ValidateChangevalueLOA = function () {

            if ($("#BeamInM").val() != "" && $("#LengthOverallInM").val() != "") {

                if (parseFloat($("#LengthOverallInM").val()) <= parseFloat($("#BeamInM").val())) {

                    $("#LengthOverallInM").val('')
                    $("#LengthOverallInM").focus()
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("LOA  should not be less than to Beam", "Vessel Registration");
                    return false
                }
            }


        }

        self.ValidateChangevalueGRT = function (event) {


            if ($("#GrossRegisteredTonnageInMT").val() != "" && $("#NetRegisteredTonnageInMT").val() != "") {

                if (parseFloat($("#GrossRegisteredTonnageInMT").val()) < parseFloat($("#NetRegisteredTonnageInMT").val())) {
                    event.GrossRegisteredTonnageInMT('');
                    $("#GrossRegisteredTonnageInMT").val('')
                    $("#GrossRegisteredTonnageInMT").focus()
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("GRT should not be less than NRT", "Vessel Registration");
                    return false
                }
            }
            if ($("#GrossRegisteredTonnageInMT").val() != "" && $("#SummerDeadWeightInMT").val() != "") {

                if (parseFloat($("#GrossRegisteredTonnageInMT").val()) <= parseFloat($("#SummerDeadWeightInMT").val())) {
                    event.GrossRegisteredTonnageInMT('');
                    $("#GrossRegisteredTonnageInMT").val('')
                    $("#GrossRegisteredTonnageInMT").focus()

                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("GRT should not be more than or equal to Summer DWT", "Vessel Registration");
                    return false

                }
            }

        }

        self.ValidateChangevalueNRT = function (model) {

            model.validationEnabled(true);
            self.VesselValidation = ko.observable(model);
            self.VesselValidation().errors = ko.validation.group(self.VesselValidation());

            if ($("#GrossRegisteredTonnageInMT").val() != "" && $("#NetRegisteredTonnageInMT").val() != "") {


                if (parseFloat($("#NetRegisteredTonnageInMT").val()) > parseFloat($("#GrossRegisteredTonnageInMT").val())) {
                    model.NetRegisteredTonnageInMT('');
                    $("#NetRegisteredTonnageInMT").val('')
                    $("#NetRegisteredTonnageInMT").focus()
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("NRT  should not be more than GRT", "Vessel Registration");
                    self.VesselValidation().errors.showAllMessages();
                    return false;

                }
            }

        }

        self.ValidateChangevalueDWT = function () {


        }

        self.ValidateChangevalueSummer = function (event) {

            if ($("#GrossRegisteredTonnageInMT").val() != "" && $("#SummerDeadWeightInMT").val() != "") {

                if (parseFloat($("#SummerDeadWeightInMT").val()) >= parseFloat($("#GrossRegisteredTonnageInMT").val())) {
                    event.SummerDeadWeightInMT('');
                    $("#SummerDeadWeightInMT").val('')
                    $("#SummerDeadWeightInMT").focus()
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Summer DWT should not be greater than or equal to GRT", "Vessel Registration");
                    return false
                }
            }

        }

        self.CheckWfStatus = function (data) {
            if (self.isAddMode() != 'YES') {
                if (data.WFStatus().toLowerCase() == 'new' || data.WFStatus().toLowerCase() == 'new request') {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("The vessel is at under approval process.", "Vessel Registration");
                    $('#status option[value="A"]').prop('selected', true);
                    self.VesselRegisrationDetModel().RecordStatus('A');
                }
            }
        }

        //----------


        //To add certificate details
        self.AddCertificates = function (certificatesDet) {

            if (certificatesDet.VesselCertificateDetails().length > 0) {
                var ManError = "Y";

                $.each(certificatesDet.VesselCertificateDetails(), function (index, item) {

                    if (item.CertificateName() == null && item.CertificateNo() == "" && item.DateOfIssue() == "" && item.DateOfValidity() == "") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please enter certificates details.", "Vessel Registration");
                        ManError = "N";
                    }
                });

                if (ManError == "Y")
                    self.VesselRegisrationDetModel().VesselCertificateDetails.push(new IPMSROOT.VesselCertificatesData());

            }
            else {
                self.VesselRegisrationDetModel().VesselCertificateDetails.push(
                    new IPMSROOT.VesselCertificatesData());
            }
        }

        //To add Hach hold details
        self.AddHachHold = function (hachHoldDet) {

            if (hachHoldDet.VesselHatchHolds().length > 0) {
                var ManError = "Y";
                $.each(hachHoldDet.VesselHatchHolds(), function (index, item) {
                    if (item.HatchHoldTypeM() == "" && item.SafeWorkingLoad() == "" && item.HoldCapacityCBM() == "" && item.Description() == "") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please enter Hatch / Hold  details.", "Vessel Registration");
                        ManError = "N";
                    }
                });

                if (ManError == "Y")
                    self.VesselRegisrationDetModel().VesselHatchHolds.push(new IPMSROOT.VesselHachHoldData());
            }
            else {
                self.VesselRegisrationDetModel().VesselHatchHolds.push(
                    new IPMSROOT.VesselHachHoldData());
            }
        }

        //To add gear details
        self.AddGearDet = function (gearDet) {

            if (gearDet.VesselGears().length > 0) {
                var ManError = "Y";

                $.each(gearDet.VesselGears(), function (index, item) {

                    if (item.GearTypeM() == "" && item.SafeWorkingLoad() == "" && item.GearCapacityCBM() == "" && item.Description() == "") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please enter gear details.", "Vessel Registration");
                        ManError = "N";
                    }
                });

                if (ManError == "Y")
                    self.VesselRegisrationDetModel().VesselGears.push(new IPMSROOT.VesselGearData());
            }
            else {
                self.VesselRegisrationDetModel().VesselGears.push(
                    new IPMSROOT.VesselGearData());
            }
        }

        // To add grab details
        self.AddGrabDet = function (grabDet) {

            if (grabDet.VesselGrabs().length > 0) {
                var ManError = "Y";

                $.each(grabDet.VesselGrabs(), function (index, item) {

                    if (item.GrabTypeM() == "" && item.SafeWorkingLoad() == "" && item.GrabCapacityCBM() == "" && item.Description() == "") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please enter grab details.", "Vessel Registration");
                        ManError = "N";
                    }
                });


                if (ManError == "Y")
                    self.VesselRegisrationDetModel().VesselGrabs.push(new IPMSROOT.VesselGrabData());
            }
            else {
                self.VesselRegisrationDetModel().VesselGrabs.push(
                    new IPMSROOT.VesselGrabData());
            }
        }

        ChangeVesselType = function (event) {

            $("#spanmaintenance").text('');

            if (event.VesselType() == "CTSH") {
                $('#reqTEUCapacity').show();
            }
            else
                $('#reqTEUCapacity').hide();

        }

        ChangePortOfRegistry = function () {

            var por = $("#PortOfRegistry").val();
            var result = self.VesselReferenceDtl().ddlPOR().filter(function (item) {
                return (item.PortName() == por);
            });

            if ($("#PortOfRegistry").val() == "") {
                $("#spanPortOfRegistry").text('* This field is required.');
            }
            else {
                if (result.length == 0) {
                    $("#spanPortOfRegistry").text('* This field is required.');
                    $("#PortOfRegistry").val('');
                    $("#PortOfRegistry").text('');
                } else {
                    $("#spanmaspanPortOfRegistryintenance").text('');
                }

            }
        }

        self.ResetSearchDtl = function () {

            $("#SerchIMONo").val("");
            $("select#SerchNationality").prop('selectedIndex', 0);
            $("select#SerchNationality").prop('selectedIndex', 0);
            $("select#SerchVesselType").prop('selectedIndex', 0);
            $("#SearchPOR").val("");
            $("#SearchVesselName").val("");
            $("#SearchCallSign").val("");


            self.LoadVesselRegistrationData();


        }


        ChangeDateofIssues = function () {

            if ($("#DateOfIssue").val() != "" && $("#DateOfValidity").val() != "") {

                if ($("#DateOfIssue").val() > $("#DateOfValidity").val()) {

                    $("#DateOfIssue").val('');
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Date of Issue should not greater than Date of Validity.", "Vessel Registration");
                    result = false;
                }
            }

        }

        ChangeVesselNationality = function () {

            if ($("#ddlVesselNationality").val() == "") {
                $("#spanVesselNationality").text('This field is required.');

            }
            else {

                $("#spanVesselNationality").text('');

            }
        }

        //To Save vessel registration details
        self.SaveVesselDet = function (model) {

            model.validationEnabled(true);
            self.VesselValidation = ko.observable(model);
            self.VesselValidation().errors = ko.validation.group(self.VesselValidation());

            var result = true;
            if (model.VesselBuildYear._latestValue != "") {
                try {
                    model.VesselBuildYear(model.VesselBuildYear().getFullYear());
                } catch (err) { }
            }

            if ($("#IMONo").val() == "") {
                $('#spanimono').text('* This field is required.');
                result = false;
            }

            if ($("#VesselName").val() == "") {
                result = false;
            }
            if ($("#CallSign").val() == "") {

                result = false;
            }
            if ($("#VesselType").val() == "" || $("#VesselType").val() == null) {

                result = false;
            }

            if ($("#VesselBuildYear").val() == "" || $("#VesselBuildYear").val() == null) {

                result = false;
            }
            if ($("#MMSINumber").val() == "" || $("#MMSINumber").val() == null) {

                result = false;
            }

            if ($("#BeamInM").val() == "") {

                result = false;
            }
            if ($("#LengthOverallInM").val() == "") {

                result = false;
            }
            if ($("#GrossRegisteredTonnageInMT").val() == "") {

                result = false;
            }

            if ($("#DeadWeightTonnageInMT").val() == "") {

                result = false;
            }

            if ($("#VesselType").val() == "") {
                $("#spanmaintenance").text('* This field is required.');
                result = false;
            }

            else {
                $("#spanmaintenance").text('');

            }
            if ($("#PortOfRegistry").val() == "") {
                $("#spanPortOfRegistry").text('* This field is required.');
                result = false;
            }
            else {

                var resultPOR = self.VesselReferenceDtl().ddlPOR().filter(function (item) {
                    return (item.PortName() == $("#PortOfRegistry").val());
                });

                if (resultPOR.length == 0)
                    result = false;
                else
                    $("#spanmaintenance").text('');
            }

            if ($("#ddlVesselNationality").val() == "") {
                $("#spanVesselNationality").text('* This field is required.');
                result = false;
            }

            else {
                $("#spanVesselNationality").text('');

            }

            if ($("#ddlVesselNationality").val() == "" || $("#ddlVesselNationality").val() == null) {

                result = false;
            }

            if (model.VesselType() == "CTSH" && $('#TEUCapacity').val() == "") {
                result = false;
            }

            var items = JSON.parse(ko.toJSON(self.VesselRegistrationList()));
            var entry = JSON.parse(ko.toJSON(model.IMONo));

            $.each(items, function (index, value) {

                if (value.IMONo == model.IMONo()) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.error("Request for the particular IMO is already done.", "Vessel Registration");
                    result = false;
                }

            });

            var dublicateCertificates = false;
            if (model.VesselCertificateDetails().length > 0) {
                var filteredArray = [];
                $.each(model.VesselCertificateDetails(), function (index, item) {
                    var alreadyAdded = false;
                    for (i in filteredArray) {

                        if (filteredArray[i].CertificateName != undefined)
                            if (filteredArray[i].CertificateName() == item.CertificateName()) {
                                alreadyAdded = true;
                            }
                    }
                    if (!alreadyAdded) {
                        filteredArray.push(item);
                    }
                    else {
                        dublicateCertificates = true;
                        result = false;
                    }
                });
            }


            var errors = self.VesselValidation().errors().length;

            if (result == true) {

                self.viewModelHelper.apiPost('api/VesselRegistration', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Vessel registration details saved successfully.", "Vessel Registration");
                    self.LoadVesselRegistrationData();
                    $('#spnTitile').html("Vessel Registration");
                    setTimeout("window.location.reload(true);", 500);
                }, null, function callbackloder(result) {
                    self.viewModelHelper.isLoading(false);
                }, false);


            } else {
                if (dublicateCertificates == true) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Selected certificate is already added to the statutory certificate details.", "Vessel Registration");
                } else {
                    self.VesselValidation().errors.showAllMessages();
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please fill all the required fields", "Vessel Registration");
                    $('#divValidationError').removeClass('display-none');
                }
                return false;
            }

        }

        // To modify vessel registration data
        self.UpdateVesselDet = function (model) {


            model.validationEnabled(true);
            self.VesselValidation = ko.observable(model);
            self.VesselValidation().errors = ko.validation.group(self.VesselValidation());


            if (model.VesselBuildYear._latestValue != "") {
                try {
                    model.VesselBuildYear(model.VesselBuildYear().getFullYear());
                } catch (err) { }
            }


            var dublicateCertificates = false;

            if (model.VesselCertificateDetails().length > 0) {
                var filteredArray = [];
                $.each(model.VesselCertificateDetails(), function (index, item) {
                    var alreadyAdded = false;
                    for (i in filteredArray) {

                        if (filteredArray[i].CertificateName != undefined)
                            if (filteredArray[i].CertificateName() == item.CertificateName()) {
                                alreadyAdded = true;
                            }
                    }
                    if (!alreadyAdded) {
                        filteredArray.push(item);
                    }
                    else {
                        dublicateCertificates = true;
                    }
                });
            }



            var result = true;

            if (dublicateCertificates == true) {

                result = false;
            }

            if ($("#IMONo").val() == "") {

                result = false;

            }
            if ($("#VesselName").val() == "") {

                result = false;
            }
            if ($("#CallSign").val() == "") {

                result = false;
            }
            if ($("#VesselType").val() == "" || $("#VesselType").val() == null) {

                result = false;
            }
            if ($("#PortOfRegistry").val() == "" || $("#PortOfRegistry").val() == null) {
                result = false;
            } else {
                var resultPOR = self.VesselReferenceDtl().ddlPOR().filter(function (item) {
                    return (item.PortName() == $("#PortOfRegistry").val());
                });

                if (resultPOR.length == 0) {
                    result = false;
                }
            }
            if ($("#VesselBuildYear").val() == "" || $("#VesselBuildYear").val() == null) {

                result = false;
            }
            if ($("#MMSINumber").val() == "" || $("#MMSINumber").val() == null) {

                result = false;
            }

            if ($("#BeamInM").val() == "") {

                result = false;
            }
            if ($("#LengthOverallInM").val() == "") {

                result = false;
            }
            if ($("#GrossRegisteredTonnageInMT").val() == "") {

                result = false;
            }

            if ($("#DeadWeightTonnageInMT").val() == "") {

                result = false;
            }

            if ($("#ddlVesselNationality").val() == "" || $("#ddlVesselNationality").val() == null) {

                result = false;
            }

            if (model.VesselType() == "CTSH" && $('#TEUCapacity').val() == "") {
                result = false;
            }


            var errors = self.VesselValidation().errors().length;

            if (result == true) {
                self.viewModelHelper.apiPut('api/VesselRegistration', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Vessel details updated successfully", "Vessel Registration");
                    self.LoadVesselRegistrationData();
                    $('#spnTitile').html("Vessel Registration");
                    setTimeout("window.location.reload(true);", 500);

                });

            } else {

                if (dublicateCertificates == true) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Selected certificate is already added to the statutory certificate details", "Vessel Registration");
                }
                else {
                    self.VesselValidation().errors.showAllMessages();
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please fill all the required fields", "Vessel Registration");
                }
                return;
            }
        }

        self.Initialize = function () {


            self.viewMode("List");
            self.LoadVesselRegistrationData();
            self.LoadRegReferences();

            self.vesselRegModel(new IPMSRoot.VesselRegistrationModel());

            if (viewDetail == true) {

            }
            else {
                self.viewMode('List');
            }

        }

        self.GetVesselData = function (data) {


            var IMONO = $('#SerchIMONo').val();
            var VesselName = $('#SearchVesselName').val();
            var PortofRegistry = $('#SearchPOR').val();
            var VesselNationality = $('#SerchNationality').val();
            var VesselType = $('#SerchVesselType').val();
            var clallsign = $('#SearchCallSign').val();
            var isValidate = true;

            if (IMONO == undefined || IMONO == "") {
                IMONO = "ALL";

            }
            if (VesselName == undefined || VesselName == "") {
                VesselName = "ALL";

            }
            if (PortofRegistry == undefined || PortofRegistry == "") {

                PortofRegistry = "ALL";

            }
            if (VesselNationality == undefined || VesselNationality == "") {
                VesselNationality = "ALL";

            }
            if (VesselType == undefined || VesselType == "") {
                VesselType = "ALL";

            }
            if (clallsign == undefined || clallsign == "") {
                clallsign = "ALL";

            }

            if (isValidate == true) {
                self.viewModelHelper.apiGet('api/GetVesselList/' + IMONO + '/' + VesselName + '/' + PortofRegistry + '/' + VesselNationality + '/' + VesselType + '/' + clallsign, {},
                          function (result) {

                              self.VesselRegistrationList(ko.utils.arrayMap(result, function (item) {
                                  return new IPMSRoot.VesselRegistrationModel(item);
                              }
                              ));

                              var grid = $("#vesselList").data("kendoGrid");
                              if (self.VesselRegistrationList().length <= 5)
                                  grid.dataSource.pageSize(5);
                              else
                                  grid.dataSource.pageSize(20);

                              grid.refresh();

                          });
            }
            else {

                self.LoadVesselRegistrationData();
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please fill all the required fields.", "Vessel Registration");
            }

        }

        //Binding Port Information 
        self.VesselSelect = function (e) {

            $("select#PortOfRegistry").prop('selectedIndex', 0);
            var selecteddataItem = this.dataItem(e.item.index());
            self.VesselRegisrationDetModel().PortCode(selecteddataItem.PortCode);
            self.VesselRegisrationDetModel().PortName(selecteddataItem.PortName);
            self.VesselRegisrationDetModel().PortOfRegistry(selecteddataItem.PortCode);
            $("#spanPortOfRegistry").hide();
        }

        self.VesselNationalitySelect = function (e) {

            $("select#veslnatid").prop('selectedIndex', 0);
            var selecteddataItem = this.dataItem(e.item.index());
            self.VesselRegisrationDetModel().SubCatName(selecteddataItem.VNName);
            self.VesselRegisrationDetModel().SubCatCode(selecteddataItem.VNCode);

        }

        self.RemoveCertificate = function (Certificatedata) {
            self.VesselRegisrationDetModel().VesselCertificateDetails.remove(Certificatedata);
        };

        self.RemoveHatchHolds = function (HatchHoldsdata) {
            self.VesselRegisrationDetModel().VesselHatchHolds.remove(HatchHoldsdata);
        };

        self.RemoveGear = function (Geardata) {
            self.VesselRegisrationDetModel().VesselGears.remove(Geardata);
        };

        self.RemoveGrab = function (Grabdata) {
            self.VesselRegisrationDetModel().VesselGrabs.remove(Grabdata);
        };

        self.RemoveEngineData = function (EngineData) {
            self.VesselRegisrationDetModel().VesselEngines.remove(EngineData);
        };

        self.SaveContinue = function (model) {
            
            var ref_this = $("ul#mainmenu li.active");
            var tabs = ref_this.attr("id")
            model.validationEnabled(true);

            self.VesselValidation = ko.observable(model);
            self.VesselValidation().errors = ko.validation.group(self.VesselValidation());
            self.VesselValidation().errors.showAllMessages(false);

            switch (tabs) {
                case "li_tab0":
                    $('#SaveContinue').addClass("btn default button-next");
                    var errors = true;

                    if ($("#IMONo").val() == "") {
                        $('#spanimono').text('Please enter IMO No.');
                        errors = false;
                    }
                    if ($("#VesselName").val() == "") {
                        errors = false;
                    }
                    if ($("#CallSign").val() == "") {

                        errors = false;
                    }
                    if ($("#VesselType").val() == "" || $("#VesselType").val() == null) {

                        errors = false;

                        $("#spanmaintenance").text("* This field is required.");

                    }
                    if ($("#PortOfRegistry").val() == "" || $("#PortOfRegistry").val() == null) {

                        errors = false;
                    } else {
                        var result = self.VesselReferenceDtl().ddlPOR().filter(function (item) {
                            return (item.PortName() == $("#PortOfRegistry").val());
                        });

                        if (result.length == 0) {
                            errors = false;
                        }
                    }
                    if ($("#VesselBuildYear").val() == "" || $("#VesselBuildYear").val() == null) {

                        errors = false;
                    }
                    if ($("#MMSINumber").val() == "" || $("#MMSINumber").val() == null) {

                        errors = false;
                    }

                    if ($("#ddlVesselNationality").val() == "" || $("#ddlVesselNationality").val() == null) {

                        errors = false;
                    }

                    if (model.VesselType() == "CTSH" && $('#TEUCapacity').val() == "") {
                        errors = false;
                    }


                    if (errors == true) {

                        $('#li_tab1').addClass('active');
                        $('#tab_1').addClass('tab-pane active');

                        $('#li_tab0').removeClass('active');
                        $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab2').removeClass('active');
                        $('#tab_2').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab3').removeClass('active');
                        $('#tab_3').removeClass('tab-pane active').addClass('tab-pane');
                        if (self.isViewMode() == 'YES') {
                            $('#SaveExit').hide();
                            self.isEditableEnable(false);
                        }
                        else {
                            $('#SaveExit').show();
                            self.isEditableEnable(true);                            
                        }
                        break;
                    }
                    else {
                        self.VesselValidation().errors.showAllMessages();


                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please fill all the required fields", "Vessel Registration");
                        break;
                    }



                case "li_tab1":
                    $('#SaveContinue').addClass("btn default button-next");

                    var errors = true;
                    if ($("#BeamInM").val() == "") {

                        errors = false;
                    }

                    if ($("#LengthOverallInM").val() == "") {

                        errors = false;
                    }
                    if ($("#GrossRegisteredTonnageInMT").val() == "") {

                        errors = false;
                    }
                    if ($("#DeadWeightTonnageInMT").val() == "") {

                        errors = false;
                    }
                    if (errors == true) {

                        $('#li_tab2').addClass('active');
                        $('#tab_2').addClass('tab-pane active');

                        $('#li_tab0').removeClass('active');
                        $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab1').removeClass('active');
                        $('#tab_1').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab3').removeClass('active');
                        $('#tab_3').removeClass('tab-pane active').addClass('tab-pane');

                        if (self.isViewMode() == 'YES') {
                            $('#SaveExit').hide();
                            self.isEditableEnable(false);
                        }
                        else {
                            $('#SaveExit').show();
                            self.isEditableEnable(true);                            
                        }
                        break;
                    }
                    else {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please fill all the required fields", "Vessel Registration");
                        self.VesselValidation().errors.showAllMessages();
                        break;

                    }

                case "li_tab2":

                    var dublicateCertificates = false;

                    if (model.VesselCertificateDetails().length > 0) {
                        var filteredArray = [];
                        $.each(model.VesselCertificateDetails(), function (index, item) {
                            var alreadyAdded = false;
                            for (i in filteredArray) {

                                if (filteredArray[i].CertificateName != undefined)
                                    if (filteredArray[i].CertificateName() == item.CertificateName()) {
                                        alreadyAdded = true;
                                    }
                            }
                            if (!alreadyAdded) {
                                filteredArray.push(item);
                            }
                            else {
                                dublicateCertificates = true;
                            }
                        });
                    }


                    if (dublicateCertificates == false) {
                        $('#li_tab3').addClass('active');
                        $('#tab_3').addClass('tab-pane active');

                        $('#li_tab0').removeClass('active');
                        $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab1').removeClass('active');
                        $('#tab_1').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab2').removeClass('active');
                        $('#tab_2').removeClass('tab-pane active').addClass('tab-pane');

                        if (self.isViewMode() == 'YES') {
                            $('#SaveContinue').hide();
                            self.isEditableEnable(false);
                        } else {
                            self.isEditableEnable(true);
                            $('#SaveExit').hide();
                            $('#SaveContinue').show();
                            $('#SaveContinue').html('Save')
                            $('#SaveContinue').addClass("btn green");
                        }


                    } else {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Selected certificate is already added to the statutory certificate details", "Vessel Registration");
                    }

                    break;
                case "li_tab3":
                    self.SaveVesselDet(model);

                    break;
            }

        }

        self.UpdateContinue = function (model) {


            var ref_this = $("ul#mainmenu li.active");
            var tabs = ref_this.attr("id")
            model.validationEnabled(true);
            self.VesselValidation = ko.observable(model);
            self.VesselValidation().errors = ko.validation.group(self.VesselValidation());
            self.VesselValidation().errors.showAllMessages(false);
            switch (tabs) {
                case "li_tab0":

                    $('#UpdateContinue').addClass("btn default button-next");
                    var errors = true;
                    if ($("#IMONo").val() == "") {

                        errors = false;
                    }
                    if ($("#VesselName").val() == "") {

                        errors = false;
                    }
                    if ($("#CallSign").val() == "") {

                        errors = false;
                    }
                    if ($("#VesselType").val() == "" || $("#VesselType").val() == null) {

                        errors = false;
                    }
                    if ($("#PortOfRegistry").val() == "" || $("#PortOfRegistry").val() == null) {

                        errors = false;
                        $("#spanPortOfRegistry").text("Please select Port Of Registry");
                    } else {
                        var result = self.VesselReferenceDtl().ddlPOR().filter(function (item) {
                            return (item.PortName() == $("#PortOfRegistry").val());
                        });

                        if (result.length == 0) {
                            $("#spanPortOfRegistry").text('*Please Select Valid Port');
                            $("#PortOfRegistry").text('');
                            $("#PortOfRegistry").val('');
                            errors = false;
                        } else {
                            $("#spanPortOfRegistry").text('');
                        }

                    }
                    if ($("#VesselBuildYear").val() == "" || $("#VesselBuildYear").val() == null) {

                        errors = false;
                    }
                    if ($("#MMSINumber").val() == "" || $("#MMSINumber").val() == null) {

                        errors = false;
                    }
                    if ($("#ddlVesselNationality").val() == "" || $("#ddlVesselNationality").val() == null) {

                        errors = false;
                    }

                    if (model.VesselType() == "CTSH" && $('#TEUCapacity').val() == "") {
                        errors = false;
                    }

                    if (errors == true) {
                        $('#li_tab1').addClass('active');
                        $('#tab_1').addClass('tab-pane active');

                        $('#li_tab0').removeClass('active');
                        $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab2').removeClass('active');
                        $('#tab_2').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab3').removeClass('active');
                        $('#tab_3').removeClass('tab-pane active').addClass('tab-pane');

                        $('#UpdateExit').hide();
                        $('#UpdateExit').show();
                        break;
                    }
                    else {

                        self.VesselValidation().errors.showAllMessages();
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please fill all the required fields", "Vessel Registration");


                        break;

                    }
                case "li_tab1":

                    $('#UpdateContinue').addClass("btn default button-next");
                    var errors = true;
                    if ($("#BeamInM").val() == "") {

                        errors = false;
                    }

                    if ($("#LengthOverallInM").val() == "") {

                        errors = false;
                    }
                    if ($("#GrossRegisteredTonnageInMT").val() == "") {

                        errors = false;
                    }
                    if ($("#DeadWeightTonnageInMT").val() == "") {

                        errors = false;
                    }
                    if (errors == true) {
                        $('#li_tab2').addClass('active');
                        $('#tab_2').addClass('tab-pane active');

                        $('#li_tab0').removeClass('active');
                        $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab1').removeClass('active');
                        $('#tab_1').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab3').removeClass('active');
                        $('#tab_3').removeClass('tab-pane active').addClass('tab-pane');

                        $('#UpdateExit').show();
                        break;
                    }
                    else {
                        self.VesselValidation().errors.showAllMessages();
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please fill all the required fields", "Vessel Registration");

                        break;
                    }

                case "li_tab2":
                    var dublicateCertificates = false;

                    if (model.VesselCertificateDetails().length > 0) {
                        var filteredArray = [];
                        $.each(model.VesselCertificateDetails(), function (index, item) {
                            var alreadyAdded = false;
                            for (i in filteredArray) {

                                if (filteredArray[i].CertificateName != undefined)
                                    if (filteredArray[i].CertificateName() == item.CertificateName()) {
                                        alreadyAdded = true;
                                    }
                            }
                            if (!alreadyAdded) {
                                filteredArray.push(item);
                            }
                            else {
                                dublicateCertificates = true;
                            }
                        });
                    }
                    if (dublicateCertificates == false) {
                        $('#li_tab3').addClass('active');
                        $('#tab_3').addClass('tab-pane active');

                        $('#li_tab0').removeClass('active');
                        $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab1').removeClass('active');
                        $('#tab_1').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab2').removeClass('active');
                        $('#tab_2').removeClass('tab-pane active').addClass('tab-pane');

                        $('#UpdateExit').hide();
                        $('#UpdateContinue').show();
                        $('#UpdateContinue').html('Update')
                        $('#UpdateContinue').addClass("btn green");

                    }
                    else {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Selected certificate is already added to the statutory certificate details", "Vessel Registration");

                    }

                    break;

                case "li_tab3":
                    self.UpdateVesselDet(model);

                    break;


            }

        }

        self.vesseldettab = function () {

            $('#SaveContinue').removeClass("btn default button-next green");
            $('#UpdateContinue').removeClass("btn default button-next green");


            $('#SaveContinue').addClass("btn default button-next");
            $('#UpdateContinue').addClass("btn default button-next");

            $('#divValidationError').addClass('display-none');


            if (self.isViewMode() == "NO") {
                self.isEditableEnable(true);
                if (self.isAddMode() == "YES") {
                    $('#SaveContinue').html('Next');
                    $('#SaveExit').hide();
                    $('#SaveContinue').show();
                    $('#UpdateContinue').hide();
                    $('#UpdateExit').hide();

                } else {
                    $('#UpdateContinue').html('Next');
                    $('#SaveExit').hide();
                    $('#SaveContinue').hide();
                    $('#UpdateContinue').show();
                    $('#UpdateExit').hide();
                }
            } else {
                self.isEditableEnable(false);
                $('#SaveExit').hide();
                $('#SaveContinue').html('Next');
                $('#SaveContinue').show();
            }
        }

        self.vesselparamtab = function (model) {



            $('#divValidationError').addClass('display-none');
            model.validationEnabled(true);
            self.VesselValidation = ko.observable(model);
            self.VesselValidation().errors = ko.validation.group(self.VesselValidation());
            self.VesselValidation().errors.showAllMessages(false);
            var ref_this = $("ul#mainmenu li.active");
            var tabs = "li_tab1" //ref_this.attr("id")

            $('#SaveContinue').removeClass("btn default button-next green");
            $('#UpdateContinue').removeClass("btn default button-next green");

            $('#SaveContinue').addClass("btn default button-next");
            $('#UpdateContinue').addClass("btn default button-next");

            switch (tabs) {

                case "li_tab1":


                    var errors = true;
                    if ($("#IMONo").val() == "") {

                        errors = false;

                    }
                    if ($("#VesselName").val() == "") {

                        errors = false;
                    }
                    if ($("#CallSign").val() == "") {

                        errors = false;
                    }
                    if ($("#VesselType").val() == "" || $("#VesselType").val() == null) {

                        errors = false;
                    }
                    if ($("#PortOfRegistry").val() == "" || $("#PortOfRegistry").val() == null) {

                        errors = false;
                    } else {

                        var result = self.VesselReferenceDtl().ddlPOR().filter(function (item) {
                            return (item.PortName() == $("#PortOfRegistry").val());
                        });

                        if (result.length == 0) {
                            errors = false;
                        }
                    }
                    if ($("#VesselBuildYear").val() == "" || $("#VesselBuildYear").val() == null) {

                        errors = false;
                    }
                    if ($("#MMSINumber").val() == "" || $("#MMSINumber").val() == null) {

                        errors = false;
                    }

                    if (errors == true) {
                        $('#li_tab0').removeClass('active');
                        $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab1').addClass('active');
                        $('#tab_1').addClass('tab-pane active').attr('href', "#tab_1")

                        $('#li_tab2').removeClass('active');
                        $('#tab_2').removeClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab3').removeClass('active');
                        $('#tab_3').removeClass('tab-pane active').addClass('tab-pane');


                        if (self.isViewMode() == "NO") {
                            self.isEditableEnable(true);
                            if (self.isAddMode() == "YES") {
                                $('#SaveContinue').html('Next');
                                $('#SaveExit').show();
                                $('#SaveContinue').show();
                                $('#UpdateContinue').hide();
                                $('#UpdateExit').hide();

                            } else {
                                $('#UpdateContinue').html('Next');
                                $('#SaveExit').hide();
                                $('#SaveContinue').hide();
                                $('#UpdateContinue').show();
                                $('#UpdateExit').show();
                            }
                        } else {
                            self.isEditableEnable(false);
                            $('#SaveExit').hide();
                            $('#SaveContinue').html('Next');
                            $('#SaveContinue').show();
                        }
                    }
                    else {

                        $('#li_tab0').addClass('active');
                        $('#tab_0').addClass('tab-pane active').addClass('tab-pane');

                        $('#li_tab1').removeClass('active');
                        $('#tab_1').removeClass('tab-pane active').addClass('tab-pane');

                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please fill all the required fields", "Vessel Registration");
                        self.VesselValidation().errors.showAllMessages();
                        break;

                    }

            }
        }

        self.vesselcerttab = function (model) {

            $('#SaveContinue').removeClass("btn default button-next green");
            $('#UpdateContinue').removeClass("btn default button-next green");

            $('#SaveContinue').addClass("btn default button-next");
            $('#UpdateContinue').addClass("btn default button-next");



            $('#divValidationError').addClass('display-none');

            model.validationEnabled(true);
            self.VesselValidation = ko.observable(model);
            self.VesselValidation().errors = ko.validation.group(self.VesselValidation());
            self.VesselValidation().errors.showAllMessages(false);

            var errors = true;
            if ($("#IMONo").val() == "") {

                errors = false;

            }
            if ($("#VesselName").val() == "") {

                errors = false;
            }
            if ($("#CallSign").val() == "") {

                errors = false;
            }
            if ($("#VesselType").val() == "" || $("#VesselType").val() == null) {

                errors = false;
            }
            if ($("#PortOfRegistry").val() == "" || $("#PortOfRegistry").val() == null) {

                errors = false;
            } else {

                var result = self.VesselReferenceDtl().ddlPOR().filter(function (item) {
                    return (item.PortName() == $("#PortOfRegistry").val());
                });

                if (result.length == 0) {
                    errors = false;
                }
            }

            if ($("#VesselBuildYear").val() == "" || $("#VesselBuildYear").val() == null) {

                errors = false;
            }
            if ($("#MMSINumber").val() == "" || $("#MMSINumber").val() == null) {

                errors = false;
            }
            if ($("#ddlVesselNationality").val() == "" || $("#ddlVesselNationality").val() == null) {

                errors = false;
            }
            var isvalid = true
            if ($("#BeamInM").val() == "") {

                isvalid = false;
            }
            if ($("#LengthOverallInM").val() == "") {

                isvalid = false;
            }
            if ($("#GrossRegisteredTonnageInMT").val() == "") {

                isvalid = false;
            }
            if ($("#DeadWeightTonnageInMT").val() == "") {

                isvalid = false;
            }
            if (errors == true && isvalid == false) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please fill all the required fields of vessel parameters.", "Vessel Registration");
                self.VesselValidation().errors.showAllMessages();
                return false;
            }
            if (errors == true && isvalid == true) {

                $('#li_tab2').addClass('active');
                $('#tab_2').addClass('tab-pane active');

                $('#li_tab0').removeClass('active');
                $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                $('#li_tab1').removeClass('active');
                $('#tab_1').removeClass('tab-pane active').addClass('tab-pane');

                $('#li_tab3').removeClass('active');
                $('#tab_3').removeClass('tab-pane active').addClass('tab-pane');

                if (self.isViewMode() == "NO") {
                    self.isEditableEnable(true);
                    if (self.isAddMode() == "YES") {
                        $('#SaveContinue').html('Next');
                        $('#SaveExit').show();
                        $('#SaveContinue').show();
                        $('#UpdateContinue').hide();
                        $('#UpdateExit').hide();
                        self.isGridsEnable(true);
                        $("#CertificateNo").prop('disabled', false)
                    } else {
                        self.isGridsEnable(false);
                        $("#CertificateNo").prop('disabled', true)
                        $('#UpdateContinue').html('Next');
                        $('#SaveExit').hide();
                        $('#SaveContinue').hide();
                        $('#UpdateContinue').show();
                        $('#UpdateExit').show();
                    }
                } else {
                    self.isEditableEnable(false);
                    $('#SaveExit').hide();
                    $('#SaveContinue').html('Next');

                    $('#SaveContinue').show();
                }
            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please fill all the required fields", "Vessel Registration");
                self.VesselValidation().errors.showAllMessages();
                return false;
            }

            $('#SaveContinue').addClass("btn default button-next");
        }

        self.vesselothertab = function (model) {


            $('#divValidationError').addClass('display-none');

            model.validationEnabled(true);
            self.VesselValidation = ko.observable(model);
            self.VesselValidation().errors = ko.validation.group(self.VesselValidation());
            self.VesselValidation().errors.showAllMessages(false);

            $('#SaveContinue').addClass("btn default button-next green");
            $('#UpdateContinue').addClass("btn default button-next green");


            var errors = true;
            if ($("#IMONo").val() == "") {

                errors = false;

            }
            if ($("#VesselName").val() == "") {

                errors = false;
            }
            if ($("#CallSign").val() == "") {

                errors = false;
            }

            if ($("#VesselType").val() == "" || $("#VesselType").val() == null) {

                errors = false;
            }
            if ($("#PortOfRegistry").val() == "" || $("#PortOfRegistry").val() == null) {

                errors = false;
            } else {
                var result = self.VesselReferenceDtl().ddlPOR().filter(function (item) {
                    return (item.PortName() == $("#PortOfRegistry").val());
                });

                if (result.length == 0) {
                    errors = false;
                }
            }
            if ($("#VesselBuildYear").val() == "" || $("#VesselBuildYear").val() == null) {

                errors = false;
            }
            if ($("#MMSINumber").val() == "" || $("#MMSINumber").val() == null) {

                errors = false;
            }
            if ($("#ddlVesselNationality").val() == "" || $("#ddlVesselNationality").val() == null) {

                result = false;
            }
            var isvalid = true;
            if ($("#BeamInM").val() == "") {

                isvalid = false;
            }
            if ($("#LengthOverallInM").val() == "") {

                isvalid = false;
            }
            if ($("#GrossRegisteredTonnageInMT").val() == "") {

                isvalid = false;
            }
            if ($("#DeadWeightTonnageInMT").val() == "") {

                isvalid = false;
            }
            if (errors == true && isvalid == false) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please fill all the required fields of vessel parameters.", "Vessel Registration");
                self.VesselValidation().errors.showAllMessages();
                return false;
            }
            if (errors == true && isvalid == true) {

                $('#li_tab3').addClass('active');
                $('#tab_3').addClass('tab-pane active');

                $('#li_tab0').removeClass('active');
                $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');

                $('#li_tab1').removeClass('active');
                $('#tab_1').removeClass('tab-pane active').addClass('tab-pane');

                $('#li_tab2').removeClass('active');
                $('#tab_2').removeClass('tab-pane active').addClass('tab-pane');



                if (self.isAddMode() == "YES") {
                    $('#SaveContinue').html('Save');
                    $('#SaveExit').hide();
                    self.isEditableEnable(true);
                } else if (self.isViewMode() == 'YES') {
                    $('#SaveContinue').hide();
                    self.isEditableEnable(false);
                } else {
                    self.isEditableEnable(true);
                    $('#UpdateContinue').html('Update');
                    $('#UpdateExit').hide();
                }




            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please enter all the required fields.", "Vessel Registration");
                self.VesselValidation().errors.showAllMessages();
                return false;
            }
        }

        self.addvesselreg = function () {
            $('#msgNote').css('display', 'none');
            self.IsViewfrmApproval('N');
            $('#spnTitile').html("Add Vessel Registration");
            $(".validationError").hide();
            self.VesselRegisrationDetModel(new IPMSRoot.VesselRegistrationModel());
            $(".validationError").hide();
            self.viewMode('Form');
            self.isAddMode("YES");
            self.isViewMode("NO")
            $('#SaveExit').hide();

            $('#SaveContinue').show();
            $('#UpdateContinue').hide();
            $('#UpdateExit').hide();
            self.IsControlesEnable();
            self.isIMONoEnable(true);
            $('#reqTEUCapacity').hide();

            self.isEditableEnable(false);

            self.isClassificationSocitiesEnable(true);
            self.isExCallSignEnable(true);
            self.isVesselTypeEnable(true);
            self.isNoOfBaysEnable(true);
            self.isVesselBuildYearEnable(true);
            self.isCallSignEnable(true);
            self.isOfficialNumberEnable(true);
            self.isNoOfRowsOnDeskEnable(true);
            self.isVesselNationalityEnable(true);
            self.isMMSINumberEnable(true);
            self.isExVesselNameEnable(true);


        }

        self.viewData = function (data) {

            $('#msgNote').css('display', 'none');
            self.IsViewfrmApproval('N');
            $('#spnTitile').html("View Vessel Registration");
            self.viewMode("Form");
            self.isAddMode("NO");
            self.isViewMode("YES")
            self.VesselRegisrationDetModel(data);
            $('#SaveExit').hide();
            $('#SaveContinue').show();
            $('#UpdateContinue').hide();
            $('#UpdateExit').hide();
            $('#btnReset').hide();
            self.IsControlesEnable();
            var autocomplete = $("#PortOfRegistry").data("kendoAutoComplete");
            autocomplete.suggest(data.PortOfRegistryName());
            $('#PortOfRegistry').attr('disabled', 'disabled');

            if (data.VesselType() == "CTSH")
                $('#reqTEUCapacity').show();
            else
                $('#reqTEUCapacity').hide();

            // Added by srini
            var ReferenceID = data.IMONo();
            var WorkflowInstanceID = data.WorkflowInstanceId();
            self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                   null,
                         function (result) {
                             self.IsViewfrmApproval('Y');
                             ko.utils.arrayForEach(result, function (val) {
                                 var pendingtaskaction = new IPMSROOT.pendingTask();
                                 pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                 pendingtaskaction.ReferenceID(val.ReferenceID);
                                 pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                 pendingtaskaction.APIUrl(val.APIUrl);
                                 pendingtaskaction.TaskName(val.TaskName);
                                 pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                 pendingtaskaction.TaskDescription(val.TaskDescription);
                                 pendingtaskaction.HasRemarks(val.HasRemarks);
                                 self.VesselRegisrationDetModel().pendingTasks.push(pendingtaskaction);
                             });
                         });


        }

        self.editData = function (data) {
            $('#msgNote').css('display', 'none');
            self.VesselRegisrationDetModel(data);
            var now = new Date();
            var endDate = new Date(now);
            if (data != undefined) {
                var currentYear = '';
                if (data.VesselBuildYear().toString().length > 5) {
                    currentYear = new Date(data.VesselBuildYear()).getFullYear();
                    self.VesselRegisrationDetModel().VesselBuildYear(currentYear);
                } else {
                    endDate.setYear(data ? (data.VesselBuildYear() || "") : "");
                    currentYear = new Date(endDate).getFullYear();
                    self.VesselRegisrationDetModel().VesselBuildYear(currentYear);
                }
            }

            self.IsViewfrmApproval('N');
            $('#spnTitile').html("Update Vessel Registration");
            self.viewMode("Form");

            self.VesselRegisrationDetModel().PortCode(data.PortOfRegistry());
            self.isAddMode("NO");
            self.isViewMode("NO")
            $('#SaveExit').hide();
            $('#SaveContinue').hide();
            $('#UpdateContinue').show();
            $('#UpdateExit').hide();
            $('#btnReset').hide();
            self.IsControlesEnable();
            self.isIMONoEnable(false);

            var autocomplete = $("#PortOfRegistry").data("kendoAutoComplete");
            autocomplete.suggest(data.PortOfRegistryName());
            $('#PortOfRegistry').attr('disabled', 'disabled');

            if (data.VesselType() == "CTSH")
                $('#reqTEUCapacity').show();
            else
                $('#reqTEUCapacity').hide();

        }

        self.Cancel = function (model) {

            $('#msgNote').css('display', '');

            if (viewDetail == false) {

                $('#spnTitile').html("Vessel Registration");
                ko.validation.reset();
                model.validationEnabled(false);
                self.VesselRegisrationDetModel().reset();

                self.viewMode('List');

            } else {
                window.location.href = "/Welcome";
            }
        }

        self.ResetData = function (model) {

            $('#divValidationError').addClass('display-none');

            if (self.isAddMode() == 'YES') {

                var ref_this = $("ul#mainmenu li.active");
                var tabs = ref_this.attr("id")

                switch (tabs) {

                    case "li_tab0":

                        $("#IMONo").val("");
                        $("#VesselName").val("");
                        $("#CallSign").val("");
                        $("#ExCallSign").val("");
                        $("#VesselType").val("");
                        $("#PortOfRegistry").val("");
                        $("#NoOfRowsOnDesk").val("");
                        $("#ExVesselName").val("");
                        $("#status").val("A");
                        $("#OfficialNumber").val("");
                        $("#PortOfRegistry").val("");
                        $("#selproposedagent").val("");
                        $("#ClassificationSociety").val("");
                        $("#NoOfBays").val("");
                        $("#VesselBuildYear").val("");
                        $("#MMSINumber").val("");
                        $("#MMSINumber").val("");
                        $('#IsGovtVessel2').attr('checked', 'checked');
                        $("#ddlVesselNationality").val("");
                        break;
                    case "li_tab1":

                        $("#BeamInM").val("");
                        $("#LengthOverallInM").val("");
                        $("#GrossRegisteredTonnageInMT").val("");
                        $("#ParallelBodyLengthInM").val("");
                        $("#BowToManifoldDistanceInM").val("");
                        $("#SummerDraftAFTInM").val("");
                        $("#SummerDraftFWDInM").val("");
                        $("#ReducedGRT").val("");
                        $("#SummerDeadWeightInMT").val("");
                        $("#SummerDisplacementInMT").val("");
                        $("#TEUCapacity").val("");
                        $("#BowThrusterStatus").val("N");
                        $("#SternThrusterStatus").val("N");
                        $("#NetRegisteredTonnageInMT").val("");
                        $("#DeadWeightTonnageInMT").val("");
                        break;
                    case "li_tab2":

                        $("#CertificateName").val("");
                        $("#CertificateNo").val("");
                        $("#DateOfIssue").val("");
                        $("#DateOfValidity").val("");

                        break;
                    case "li_tab3":
                        self.VesselRegisrationDetModel().VesselHatchHolds.removeAll();
                        self.VesselRegisrationDetModel().VesselGears.removeAll();
                        self.VesselRegisrationDetModel().VesselGrabs.removeAll();
                        self.VesselRegisrationDetModel().VesselEngines.removeAll();

                        break;
                    default:
                        ko.validation.reset();
                        model.validationEnabled(false);
                        self.VesselRegisrationDetModel().reset();
                        self.VesselRegisrationDetModel().VesselCertificateDetails.removeAll();
                        self.VesselRegisrationDetModel().VesselHatchHolds.removeAll();
                        self.VesselRegisrationDetModel().VesselGears.removeAll();
                        self.VesselRegisrationDetModel().VesselGrabs.removeAll();
                        self.VesselRegisrationDetModel().VesselEngines.removeAll();
                        self.VesselValidation = ko.observable(model);
                        $("#spanmaintenance").text('');
                        $("#spanPortOfRegistry").text('');
                        break;
                }
            }
            else {

                ko.validation.reset();
                model.validationEnabled(false);
                self.VesselRegisrationDetModel().reset();
                self.VesselRegisrationDetModel().VesselCertificateDetails.removeAll();
                self.VesselRegisrationDetModel().VesselHatchHolds.removeAll();
                self.VesselRegisrationDetModel().VesselGears.removeAll();
                self.VesselRegisrationDetModel().VesselGrabs.removeAll();
                self.VesselRegisrationDetModel().VesselEngines.removeAll();
                self.VesselValidation = ko.observable(model);
                $("#spanmaintenance").text('');
                $("#spanPortOfRegistry").text('');

            }

        }

        self.IsControlesEnable = function () {

            if (self.isViewMode() == "YES") {
                self.isClassificationSocitiesEnable(false);

                //Vessel tab//
                self.isIMONoEnable(false);
                self.isClassificationSocitiesEnable(false);
                self.isExCallSignEnable(false);
                self.isVesselNameEnable(false);
                self.isVesselTypeEnable(false);
                self.isNoOfBaysEnable(false);
                self.isVesselBuildYearEnable(false);
                self.isCallSignEnable(false);
                self.isOfficialNumberEnable(false);
                self.isNoOfRowsOnDeskEnable(false);
                self.isVesselNationalityEnable(false);
                self.isMMSINumberEnable(false);
                self.isExVesselNameEnable(false);
                self.isGovtVesselEnable(false);

                self.isEditableEnable(false);
                self.isVesselNationalityEnable(false);
                $('#divIsGovtVessel').attr('disabled', 'disabled');
                $('#divEngineGrid').attr('disabled', true);

                self.isRecordStatusEnable(false);
                self.isGridsEnable(false);
                //Vessel end tab//

                //grid columns//
                self.isEngineGridEnable(false);
                //grid columns

                // Vessel Parameters Tab//
                self.isBeamInMEnable(false);
                self.isGrossRegisteredTonnageInMTEnable(false);
                self.isNetRegisteredTonnageInMTEnable(false);
                self.isParallelBodyLengthInMEnable(false);
                self.isDeadWeightTonnageInMTEnable(false);
                self.isBowToManifoldDistanceInMEnable(false);
                self.isSummerDeadWeightInMTEnable(false);
                self.isSummerDraftFWDInMEnable(false);
                self.isSummerDisplacementInMTEnable(false);
                self.isSummerDraftAFTInMEnable(false);
                self.isTEUCapacityEnable(false);
                self.isReducedGRTEnable(false);
                self.isBowThrusterEnable(false);
                // Vessel Parameters End Tab//

                // Vessel Certificates Tab//
                $('#divCertificatesGrid').attr('disabled', false);
                // Vessel Certificates End Tab//

                //Other Details tab//
                self.isBowToForwardHatchDistanceMEnable(false);
                self.isBowThrusterPowerKWEnable(false);
                self.isBowToBridgeFrontDistanceMEnable(false);
                self.isSternThrusterPowerKWEnable(false);
                $('#divHatchHoldGrid').attr('disabled', false);
                $('#divGearGrid').attr('disabled', 'disabled');
                $('#divGrabGrid').attr('disabled', 'disabled');
                //Other Details end tab//


            } else {

                //Vessel tab//              
                self.isClassificationSocitiesEnable(false);
                self.isExCallSignEnable(false);
                self.isVesselNameEnable(true);
                self.isVesselTypeEnable(false);
                self.isNoOfBaysEnable(false);
                self.isVesselBuildYearEnable(false);
                self.isCallSignEnable(false);
                self.isOfficialNumberEnable(false);
                self.isNoOfRowsOnDeskEnable(false);
                self.isVesselNationalityEnable(false);
                self.isMMSINumberEnable(false);
                self.isExVesselNameEnable(false);
                self.isRecordStatusEnable(true);
                //Vessel end tab//
                self.isEditableEnable(true);

                // Vessel Parameters Tab//
                self.isBeamInMEnable(true);
                self.isGrossRegisteredTonnageInMTEnable(true);
                self.isNetRegisteredTonnageInMTEnable(true);
                self.isParallelBodyLengthInMEnable(true);
                self.isDeadWeightTonnageInMTEnable(true);
                self.isBowToManifoldDistanceInMEnable(true);
                self.isSummerDeadWeightInMTEnable(true);
                self.isSummerDraftFWDInMEnable(true);
                self.isSummerDisplacementInMTEnable(true);
                self.isSummerDraftAFTInMEnable(true);
                self.isTEUCapacityEnable(true);
                self.isReducedGRTEnable(true);
                self.isBowThrusterEnable(true);
                self.isGovtVesselEnable(false);
                // Vessel Parameters End Tab//

                // Vessel Certificates Tab//
                $('#divCertificatesGrid').attr('disabled', true);
                // Vessel Certificates End Tab//

                //Other Details tab//
                self.isBowToForwardHatchDistanceMEnable(true);
                self.isBowThrusterPowerKWEnable(true);
                self.isBowToBridgeFrontDistanceMEnable(true);
                self.isSternThrusterPowerKWEnable(true);
                //Other Details end tab//

                self.isGridsEnable(false);
            }
        }

        self.ValidateIMONO = function (data, event) {

            var entry = JSON.parse(ko.toJSON(data));

            self.VesselRegisrationDetModel(new IPMSRoot.VesselRegistrationModel());
            self.VesselRegisrationDetModel().IMONo(entry.IMONo);
            $('#SaveContinue').show();
            $('#reqTEUCapacity').hide();
            $(".validationError").text('');
            self.viewModelHelper.apiGet('api/CheckIMOExists/' + entry.IMONo,
                { imo: entry.IMONo },
                 function (result) {
                     if (result > 0) {
                         self.VesselRegisrationDetModel().IMONo("");

                         $('#spanimono').text('Already exists.! enter another IMO No.');
                         $('#spanimono').css('display', '');
                         $('#IMONo').val("");
                         return false;
                     } else {

                         self.viewModelHelper.apiGet('api/GetVesselDetailsFromService/' + $("#IMONo").val(), { imo: $("#IMONo").val() }, function (result) {

                             var val = chekVesselExistinService(result);
                             if (val == true) {
                                 self.VesselRegisrationDetModel().VesselType(result.VesselType);
                                 self.VesselRegisrationDetModel().CallSign(result.CallSign);
                                 self.VesselRegisrationDetModel().VesselName(result.VesselName);
                                 self.VesselRegisrationDetModel().MMSINumber(result.MMSINumber);
                                 self.VesselRegisrationDetModel().VesselNationality(result.VesselNationality);
                                 self.VesselRegisrationDetModel().GrossRegisteredTonnageInMT(result.GrossRegisteredTonnageInMT);
                                 self.VesselRegisrationDetModel().LengthOverallInM(result.LengthOverallInM);
                                 self.VesselRegisrationDetModel().BeamInM(result.BeamInM);
                                 self.VesselRegisrationDetModel().NetRegisteredTonnageInMT(result.NetRegisteredTonnageInMT);
                                 self.VesselRegisrationDetModel().DeadWeightTonnageInMT(result.DeadWeightTonnageInMT);
                                 $("#VesselBuildYear").val(result.VesselBuildYear);
                                 $("#ClassificationSociety").prop('selectedIndex', 1);
                                 $("#PortOfRegistry").val(result.PortName);
                                 self.VesselRegisrationDetModel().PortCode(result.PortCode);
                                 self.VesselRegisrationDetModel().PortName(result.PortName);
                                 self.VesselRegisrationDetModel().PortOfRegistry(result.PortOfRegistry);
                             } else {

                                 $('#spanimono').css('display', 'none');
                                 return true;
                             }
                         });

                     }
                 }, null, function callbackloder(result) {
                     self.viewModelHelper.isLoading(false);
                 }, false);

        }

        chekVesselExistinService = function (data) {
            var result = false;
            if (data.VesselType != null)
                result = true;
            if (data.CallSign != null)
                result = true;
            if (data.VesselName != null)
                result = true;
            if (data.MMSINumber > 0 && data.MMSINumber != null)
                result = true;
            if (data.VesselNationality != null)
                result = true;
            if (data.GrossRegisteredTonnageInMT != null)
                result = true;
            if (data.LengthOverallInM != null)
                result = true;
            if (data.BeamInM != null)
                result = true;
            if (data.NetRegisteredTonnageInMT != null)
                result = true;
            if (data.DeadWeightTonnageInMT != null)
                result = true;
            if (data.MMSINumber > 0 && result.VesselBuildYear != null)
                result = true;
            if (data.ClassificationSociety != null)
                result = true;
            if (data.PortOfRegistry != null)
                result = true;
            if (data.PortCode != null)
                result = true;
            if (data.PortName != null)
                result = true;
            if (data.PortOfRegistry != null)
                result = true;

            return result;
        }

        HandleKeyUp = function (data, event) {

            var keyCode = event.charCode || event.keyCode;
            if (keyCode === 8 || keyCode === 46) {

                event.returnValue = true;
            }
            else {
                var keyChar = String.fromCharCode(keyCode);
                return /[a-zA-Z0-9]/.test(keyChar);
            }
            var items = JSON.parse(ko.toJSON(self.VesselRegistrationList));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.IMONo == entry.IMONo) {
                    $('#spanimono').css('display', '');

                }
                else {
                    $('#spanimono').css('display', 'none');
                }
            });
        }

        // Added by Srini
        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.VesselRegisrationDetModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        /////////////service from loyd

        self.GetIMOFromService = function () {
            //alert('Hi...');

            self.viewModelHelper.apiGet('api/GetVesselDetailsFromService/' + $("#IMONo").val(), { imo: $("#IMONo").val() }, function (result) {

                self.VesselRegisrationDetModel().VesselType(result.VesselType);
                self.VesselRegisrationDetModel().CallSign(result.CallSign);
                self.VesselRegisrationDetModel().VesselName(result.VesselName);
                self.VesselRegisrationDetModel().MMSINumber(result.MMSINumber);
                self.VesselRegisrationDetModel().VesselNationality(result.VesselNationality);

                self.VesselRegisrationDetModel().GrossRegisteredTonnageInMT(result.GrossRegisteredTonnageInMT);
                self.VesselRegisrationDetModel().LengthOverallInM(result.LengthOverallInM);
                self.VesselRegisrationDetModel().BeamInM(result.BeamInM);
                self.VesselRegisrationDetModel().NetRegisteredTonnageInMT(result.NetRegisteredTonnageInMT);
                self.VesselRegisrationDetModel().DeadWeightTonnageInMT(result.DeadWeightTonnageInMT);

                $("#VesselBuildYear").val(result.VesselBuildYear);


                $("#ClassificationSociety").prop('selectedIndex', 1);
                $("#PortOfRegistry").val(result.PortName);
                self.VesselRegisrationDetModel().PortCode(result.PortCode);
                self.VesselRegisrationDetModel().PortName(result.PortName);
                self.VesselRegisrationDetModel().PortOfRegistry(result.PortOfRegistry);

            });

        }


        // Numeric With Decimal
        self.ValidateNumeric = function (data, event) {

            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            //charcheck = /[0-9]/;
            charcheck = /[0-9.\b]/;
            return charcheck.test(keychar);
        }

        //Accept only numeric 
        self.ValidatenumericWithOutDecimal = function (data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /[0-9]/;
            return charcheck.test(keychar);
        }

        self.ValidateDate = function (data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[]$/;
            return charcheck.test(keychar);
        }

        self.allowOnlyTwoPositiveDigts = function (el, evt) {
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

        //Preventing Backspace
        PreventBackSpace = function (event) {
            //return self.validationHelper.PreventBackspaces_keydownEvent(event);
            CutPaste();
            var evt = event || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8 || keyCode === 46) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }

        self.viewWorkFlow = function (vessel) {
            var workflowinstanceId = vessel.WorkflowInstanceId();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {

                  self.VesselRegisrationDetModel(new IPMSROOT.VesselRegistrationModel());
                  self.VesselRegisrationDetModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }

        self.Initialize();
    }
    IPMSRoot.VesselRegistrationViewModel = VesselRegistrationViewModel;

}(window.IPMSROOT));













