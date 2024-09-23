(function (IPMSRoot) {

    var ChangeETAViewModel = function (VCNChangeETA, VesselETAChangeID, viewDetail) {
        var self = this;
        
        $('#spnTitle').html("Change ETA");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.changeETAModel = ko.observable(new IPMSROOT.ChangeETAModel());
        self.vesselArrivalList = ko.observableArray();
        self.ChangeETAList = ko.observableArray();
        self.validationHelper = new IPMSRoot.validationHelper();

        self.editableView = ko.observable(true);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.IsVCNnum = ko.observable(false);
        self.IsVCN = ko.observable(false);
        self.IsNewETAValid = ko.observable(false);
        self.IsNewETDValid = ko.observable(false);
        self.IsVoyageIN = ko.observable(false);
        self.IsVoyageOUT = ko.observable(false);
        self.IsRemarks = ko.observable(false);
        self.StartDate = ko.observable();
        self.IsValid = ko.observable(false);
        self.selectedVCN = ko.observable(VCNChangeETA);
        self.IsSaveButtonEnabled = ko.observable(true);
        self.isspanOptValid1 = ko.observable(false);
        self.isspanOptValid2 = ko.observable(false);
        self.isspanOptValid3 = ko.observable(false);
        self.isspanOptValid4 = ko.observable(false);
        self.isDateEnable = ko.observable(false);
        self.SaveValidDate = ko.observable(false);

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.changeETAModel(new IPMSROOT.ChangeETAModel());           
            self.LoadChangeETA();

            if (viewDetail == true) {
            }
            else {
                self.viewMode('List');
            }
        }

        self.LoadChangeETA = function () {
            if (viewDetail == true) {
                var VCNChangeETA = self.selectedVCN() != undefined ? self.selectedVCN() : '';
                self.viewModelHelper.apiGet('api/GetzChangeETADetails/' + VCNChangeETA + '/' + VesselETAChangeID, { vcn: VCNChangeETA, VesselETAChangeID: VesselETAChangeID },
                    function (result) {
                        self.ChangeETAList(ko.utils.arrayMap(result, function (item) {
                            return new IPMSRoot.ChangeETAModel(item);
                        }));
                        self.viewchangeeta(self.ChangeETAList()[0]);
                    });
            }
            else {
                var vcn = self.changeETAModel().VCNSearch() != undefined && self.changeETAModel().VCNSearch() != '' ? self.changeETAModel().VCNSearch() : 'ALL';
                var vesselName = self.changeETAModel().VesselNameSearch() != undefined && self.changeETAModel().VesselNameSearch() != '' ? self.changeETAModel().VesselNameSearch() : 'ALL';
                var etafrom = moment(self.changeETAModel().ETAFrom()).format('YYYY-MM-DD');
                var etato = moment(self.changeETAModel().ETATo()).format('YYYY-MM-DD');
                var agentNameSearch = self.changeETAModel().AgentNameSearch() != undefined && self.changeETAModel().AgentNameSearch() != '' ? self.changeETAModel().AgentNameSearch() : 'ALL';

                self.viewModelHelper.apiGet('api/ChangeETA/' + vcn + '/' + vesselName + '/' + etafrom + '/' + etato + '/' + agentNameSearch, null,
                    function (result) {
                        self.ChangeETAList(ko.utils.arrayMap(result, function (item) {
                            return new IPMSRoot.ChangeETAModel(item);
                        }));
                    });
            }
        }

        self.LoadVesselArrivals = function () {
            self.vesselArrivalList([]);
            self.viewModelHelper.apiGet('api/GetVCNs', null,
                function (result) {
                    ko.mapping.fromJS(result, {}, self.vesselArrivalList);
                });
        }

        self.subscribeNewETD = function (e) {
            var neweta = new Date(Date.parse(e.NewETA()));
            var eta = new Date(e.ETA());
            var etd = new Date(e.ETD());

            var datediff = Math.floor((Date.UTC(etd.getFullYear(), etd.getMonth(), etd.getDate()) - Date.UTC(eta.getFullYear(), eta.getMonth(), eta.getDate())) / (1000 * 60 * 60 * 24));
            
            var hourdiff = etd.getHours() - eta.getHours();
            var mindiff = etd.getMinutes() - eta.getMinutes();
            var secdiff = etd.getSeconds() - eta.getSeconds();
            neweta.setDate(neweta.getDate() + datediff);
            neweta.setHours(neweta.getHours() + hourdiff);
            neweta.setMinutes(neweta.getMinutes() + mindiff);
            neweta.setSeconds(neweta.getSeconds() + secdiff);
            self.changeETAModel().NewETD(neweta);

            self.IsVCN(false);
            self.IsNewETAValid(false);
            self.IsNewETDValid(false);
            self.IsVoyageIN(false);
            self.IsVoyageOUT(false);
            self.IsRemarks(false);
            self.IsValid(true);
            self.isDateEnable(true);
            var selectNeweta = e.NewETA();
            var selectNewetd = e.NewETD();

            $("#newetd").data('kendoDateTimePicker').min(selectNeweta);

            $("#PlanDateTimeOfBerth").data('kendoDateTimePicker').enable(true);
            $("#PlanDateTimeToStartCargo").data('kendoDateTimePicker').enable(true);
            $("#PlanDateTimeToCompleteCargo").data('kendoDateTimePicker').enable(true);
            $("#PlanDateTimeToVacateBerth").data('kendoDateTimePicker').enable(true);

            
            $("#PlanDateTimeOfBerth").val('');
            self.changeETAModel().PlanDateTimeOfBerth('');
            $("#PlanDateTimeToStartCargo").val('');
            self.changeETAModel().PlanDateTimeToStartCargo('');
            $("#PlanDateTimeToCompleteCargo").val('');
            self.changeETAModel().PlanDateTimeToCompleteCargo('');
            $("#PlanDateTimeToVacateBerth").val('');
            self.changeETAModel().PlanDateTimeToVacateBerth('');

        }

        NewETDChange = function(data, event) {
            var myNewETA = kendo.toString(new Date(self.changeETAModel().NewETA()), 'yyyy-MM-dd HH:mm');
            var myNewETD = event.target.value;

            if (myNewETA == myNewETD) {
                myNewETA = new Date(myNewETA);
                var day = myNewETA.getDate();
                var month = myNewETA.getMonth();
                var year = myNewETA.getFullYear();
                var Hour = myNewETA.getHours() + 1;
                var Mnt = myNewETA.getMinutes();

                var myNewETD = new Date(year, month, day, Hour, Mnt);
                self.changeETAModel().NewETD(myNewETD);
                self.IsNewETDValid(false);
            } else {
                self.changeETAModel().NewETD(event.target.value);
            }

            if ((self.changeETAModel().ATA() != '' || self.changeETAModel().ATB() != '') && (self.changeETAModel().ATA() != undefined || self.changeETAModel().ATB() != undefined)) {
                    $("#PlanDateTimeToCompleteCargo").data('kendoDateTimePicker').enable(true);
                    $("#PlanDateTimeToVacateBerth").data('kendoDateTimePicker').enable(true);
                    $("#PlanDateTimeToCompleteCargo").val('');
                    self.changeETAModel().PlanDateTimeToCompleteCargo('');
                    $("#PlanDateTimeToVacateBerth").val('');
                    self.changeETAModel().PlanDateTimeToVacateBerth('');
            } else {
                $("#PlanDateTimeOfBerth").data('kendoDateTimePicker').enable(true);
                $("#PlanDateTimeToStartCargo").data('kendoDateTimePicker').enable(true);
                $("#PlanDateTimeToCompleteCargo").data('kendoDateTimePicker').enable(true);
                $("#PlanDateTimeToVacateBerth").data('kendoDateTimePicker').enable(true);

                $("#PlanDateTimeOfBerth").val('');
                self.changeETAModel().PlanDateTimeOfBerth('');
                $("#PlanDateTimeToStartCargo").val('');
                self.changeETAModel().PlanDateTimeToStartCargo('');
                $("#PlanDateTimeToCompleteCargo").val('');
                self.changeETAModel().PlanDateTimeToCompleteCargo('');
                $("#PlanDateTimeToVacateBerth").val('');
                self.changeETAModel().PlanDateTimeToVacateBerth('');
            }
        }

        self.VCNSelect = function (e) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var selecteddataItem = this.dataItem(e.item.index());
            self.viewModelHelper.apiGet('api/GetVesselInfoByVCN/' + selecteddataItem.VCN, null, function (result) {

                if (result == null) {
                    self.IsVCNnum(false);
                    toastr.error("No records were found for this VCN. Please provide valid VCN.", "Change ETA");
                }
                else {
                    self.IsVCNnum(true);
                    self.isDateEnable(false);
                    self.changeETAModel().VCN(result.VCN);
                    self.changeETAModel().VesselName(result.VesselName);
                    self.changeETAModel().Date(kendo.toString(new Date(), 'yyyy-MM-dd HH:mm'));
                    self.changeETAModel().StartDate(new Date());
                    self.changeETAModel().VesselAgent(result.VesselAgent);
                    self.changeETAModel().ReportingTo(result.ReportingTo);
                    self.changeETAModel().AgentName(result.AgentName);
                    self.changeETAModel().LOA(result.LOA);
                    self.changeETAModel().GRT(result.GRT);
                    self.changeETAModel().Draft(result.Draft);

                    self.changeETAModel().ETA(result.ETA);
                    self.changeETAModel().ETD(result.ETD);

                    self.changeETAModel().NewETA("");
                    self.changeETAModel().NewETD("");
                    self.changeETAModel().NoofTimesETAChanged(result.NoofTimesETAChanged);
                    self.changeETAModel().VesselCallMovementID(result.VesselCallMovementID);

                    self.changeETAModel().VoyageIn(result.VoyageIn);
                    self.changeETAModel().VoyageOut(result.VoyageOut);
                    self.changeETAModel().Remarks(result.Remarks); 
                    self.changeETAModel().PlanDateTimeOfBerth(kendo.toString(new Date(Date.parse(result.PlanDateTimeOfBerth)), 'yyyy-MM-dd HH:mm'));
                    self.changeETAModel().PlanDateTimeToStartCargo(kendo.toString(new Date(Date.parse(result.PlanDateTimeToStartCargo)), 'yyyy-MM-dd HH:mm'));
                    self.changeETAModel().PlanDateTimeToCompleteCargo(kendo.toString(new Date(Date.parse(result.PlanDateTimeToCompleteCargo)), 'yyyy-MM-dd HH:mm'));
                    self.changeETAModel().PlanDateTimeToVacateBerth(kendo.toString(new Date(Date.parse(result.PlanDateTimeToVacateBerth)), 'yyyy-MM-dd HH:mm'));
                    self.changeETAModel().ArrivalReasonforVisit(result.ArrivalReasonforVisit);
                    self.SaveValidDate(false);
                    ValidationETA(self.changeETAModel());
                    self.IsSaveButtonEnabled(true);

                    if ((result.ATA != '' || result.ATB != '') && (result.ATD != '' || result.ATUB != '')) {
                        $("#neweta").data('kendoDateTimePicker').enable(false);
                        $("#newetd").data('kendoDateTimePicker').enable(false);
                        $("#neweta").data('kendoDateTimePicker').min(result.ETA);
                        self.changeETAModel().NewETA(result.ETA);
                        self.changeETAModel().NewETD(result.ETD);

                        self.changeETAModel().ATA(result.ATA);
                        self.changeETAModel().ATD(result.ATD);

                        self.changeETAModel().ATB(result.ATB);
                        self.changeETAModel().ATUB(result.ATUB);

                        self.IsSaveButtonEnabled(false);
                        toastr.warning("Sailing task completed for the selected VCN, you cannot proposed New ETD", "Change ETA");

                    } else if (result.ATA != '' || result.ATB != '') {
                        $("#neweta").data('kendoDateTimePicker').enable(false);
                        $("#newetd").data('kendoDateTimePicker').enable(true);

                        $("#neweta").data('kendoDateTimePicker').min(result.ETA);
                        $("#newetd").data('kendoDateTimePicker').min(result.ETA);

                        self.changeETAModel().NewETA(result.ETA);
                        self.changeETAModel().NewETD(result.ETD);

                        self.changeETAModel().ATA(result.ATA);
                        self.changeETAModel().ATD('');

                        self.changeETAModel().ATB(result.ATB);
                        self.changeETAModel().ATUB('');

                        toastr.warning("Arrival task completed for the selected VCN, you cannot proposed New ETA", "Change ETA");
                    }
                    else {

                        $("#neweta").data('kendoDateTimePicker').min(new Date);
                        $("#neweta").data('kendoDateTimePicker').enable(true);
                        $("#newetd").data('kendoDateTimePicker').enable(true);
                    }
                    $("#PlanDateTimeOfBerth").data('kendoDateTimePicker').enable(false);
                    $("#PlanDateTimeToStartCargo").data('kendoDateTimePicker').enable(false);
                    $("#PlanDateTimeToCompleteCargo").data('kendoDateTimePicker').enable(false);
                    $("#PlanDateTimeToVacateBerth").data('kendoDateTimePicker').enable(false);


                }
            });
        }


        ValidationETA = function (model) {
            var ArrivalReasons = self.changeETAModel().ArrivalReasonforVisit().split(',');

            var isOptnlInfo = 0;
            for (var i = 0; i < ArrivalReasons.length; i++) {
                model.ReasonForVisit(ArrivalReasons[i]);
                if (model.ReasonForVisit() == 'DRYD' || model.ReasonForVisit() == 'BUNK' || model.ReasonForVisit() == 'LABY' || model.ReasonForVisit() == 'REPA') {
                } else {
                    isOptnlInfo = 1;
                }
            }

            model.PlanDateTimeOfBerth.extend({ required: true });
            model.PlanDateTimeToVacateBerth.extend({ required: true });
            model.PlanDateTimeToStartCargo.extend({ required: true });
            model.PlanDateTimeToCompleteCargo.extend({ required: true });
            self.isspanOptValid1(true);
            self.isspanOptValid2(true);
            self.isspanOptValid3(true);
            self.isspanOptValid4(true);

            if (isOptnlInfo == 0) {
                model.PlanDateTimeOfBerth.rules.remove(function (item) { return item.rule = "required"; });
                model.PlanDateTimeToVacateBerth.rules.remove(function (item) { return item.rule = "required"; });
                model.PlanDateTimeToStartCargo.rules.remove(function (item) { return item.rule = "required"; });
                model.PlanDateTimeToCompleteCargo.rules.remove(function (item) { return item.rule = "required"; });
                self.isspanOptValid1(false);
                self.isspanOptValid2(false);
                self.isspanOptValid3(false);
                self.isspanOptValid4(false);
                $("#spanPlan1").hide();
                $("#spanPlan3").hide();
                $("#spanPlan2").hide();
                $("#spanPlan4").hide();
            }

            if (isOptnlInfo == 1 && self.SaveValidDate()) {

                if ($("#PlanDateTimeOfBerth").val() == "" || $("#PlanDateTimeOfBerth").val() == null) {
                    $("#spanOptValid1").text('This field is required');
                    self.isspanOptValid1(true);
                }
                else {
                    $("#spanOptValid1").text('');
                    self.isspanOptValid1(false);
                }
                if ($("#PlanDateTimeToCompleteCargo").val() == "" || $("#PlanDateTimeToCompleteCargo").val() == null) {
                    $("#spanOptValid2").text('This field is required');
                    self.isspanOptValid2(true);
                }
                else {
                    $("#spanOptValid2").text('');
                    self.isspanOptValid2(false);
                }

                if ($("#PlanDateTimeToStartCargo").val() == "" || $("#PlanDateTimeToStartCargo").val() == null) {
                    $("#spanOptValid3").text('This field is required');
                    self.isspanOptValid3(true);
                }
                else {
                    $("#spanOptValid3").text('');
                    self.isspanOptValid3(false);
                }

                if ($("#PlanDateTimeToVacateBerth").val() == "" || $("#PlanDateTimeToVacateBerth").val() == null) {
                    $("#spanOptValid4").text('This field is required');
                    self.isspanOptValid4(true);
                }
                else {
                    $("#spanOptValid4").text('');
                    self.isspanOptValid4(false);
                }
            }
            else {
                $("#spanOptValid1").text('');
                self.isspanOptValid1(false);
                $("#spanOptValid2").text('');
                self.isspanOptValid2(false);
                $("#spanOptValid3").text('');
                self.isspanOptValid3(false);
                $("#spanOptValid4").text('');
                self.isspanOptValid4(false);

            }

        }

        calmaxtoday = function () {
            this.max(new Date());
        };

        calmintoday = function () {
            this.min(new Date());
        };

        self.hideerrormesages = function (e) {
            if (e != undefined) {
                self.IsVCN(false);
                self.IsNewETAValid(false);
                self.IsNewETDValid(false);
                self.IsVoyageIN(false);
                self.IsVoyageOUT(false);
                self.IsRemarks(false);
                self.IsValid(true);
            }
        }

        self.SaveChangeETA = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.ChangeETAValidation = ko.observable(model);
            self.ChangeETAValidation().errors = ko.validation.group(self.ChangeETAValidation());
            var errors = self.ChangeETAValidation().errors().length;
           
            var currentDate = new Date(Date.parse(model.StartDate()));

            var match = ko.utils.arrayFirst(self.vesselArrivalList(), function (item) {
                return item.VCN() === model.VCN();
            });

            if (match == null) {
                toastr.error("Please select valid VCN.", "Change ETA");
                self.IsVCNnum(false);
                return;
            }

            if (model.VCN() == "" || model.VCN() == null) {
                self.IsVCN(true);
                self.IsVCNnum(false);
                self.IsValid(false);
            }

            if (self.IsVCNnum() == true) {

                if (model.NewETA() == "" || model.NewETA() == null) {
                    self.IsNewETAValid(true);
                    self.IsValid(false);
                    errors = 1;

                    $("#spanneweta").text('* This field is required.');
                }

                if (model.NewETD() == "" || model.NewETD() == null) {
                    self.IsNewETDValid(true);
                    self.IsValid(false);
                    errors = 1;

                    $("#spannewetd").text('* This field is required.');
                }

                self.SaveValidDate(true);
                ValidationETA(model);
                
                
                if (errors == 0) {

                    var objPlanDateTimeOfBerth = kendo.parseDate(model.PlanDateTimeOfBerth(), "yyyy-MM-dd HH:mm");
                    var objstrPlanDateTimeOfBerth = kendo.toString(objPlanDateTimeOfBerth, "yyyy-MM-dd HH:mm");
                    model.PlanDateTimeOfBerth(objstrPlanDateTimeOfBerth);

                    var objPlanDateTimeToVacateBerth = kendo.parseDate(model.PlanDateTimeToVacateBerth(), "yyyy-MM-dd HH:mm");
                    var objstrPlanDateTimeToVacateBerth = kendo.toString(objPlanDateTimeToVacateBerth, "yyyy-MM-dd HH:mm");
                    model.PlanDateTimeToVacateBerth(objstrPlanDateTimeToVacateBerth);

                    var objPlanDateTimeToStartCargo = kendo.parseDate(model.PlanDateTimeToStartCargo(), "yyyy-MM-dd HH:mm");
                    var objstrPlanDateTimeToStartCargo = kendo.toString(objPlanDateTimeToStartCargo, "yyyy-MM-dd HH:mm");
                    model.PlanDateTimeToStartCargo(objstrPlanDateTimeToStartCargo);

                    var objPlanDateTimeToCompleteCargo = kendo.parseDate(model.PlanDateTimeToCompleteCargo(), "yyyy-MM-dd HH:mm");
                    var objstrPlanDateTimeToCompleteCargo = kendo.toString(objPlanDateTimeToCompleteCargo, "yyyy-MM-dd HH:mm");
                    model.PlanDateTimeToCompleteCargo(objstrPlanDateTimeToCompleteCargo);

                    if (moment(model.NewETD()).format('YYYY-MM-DD HH:mm') > moment(model.NewETA()).format('YYYY-MM-DD HH:mm')) {
                        self.IsValid(true);
                        if (self.IsValid() == true) {
                            self.viewModelHelper.apiPost('api/SaveChangeETA', ko.mapping.toJSON(model), function Message(data) {
                                toastr.success("Change ETA details saved successfully.", "Change ETA");
                                self.viewMode('List');
                                self.LoadChangeETA();
                            });
                        }
                    }
                    else {
                        $("#spannewetd").text('New ETD should be greater than New ETA.');
                        self.IsNewETDValid(true);
                        self.IsValid(false);
                    }
                }
                else {
                    self.ChangeETAValidation().errors.showAllMessages();
                    self.IsValid(false);
                    return;
                }
            }
            else {
                toastr.error("No records were found for this VCN. Please provide valid VCN.", "Change ETA");
            }
        }

        self.addchangeeta = function () {
            self.LoadVesselArrivals();
            self.viewMode('Form');
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.changeETAModel(new IPMSROOT.ChangeETAModel());
            self.changeETAModel().StartDate(new Date());
            $('#spnTitle').html("Change ETA");
            $("#neweta").data('kendoDateTimePicker').enable(true);
            $("#newetd").data('kendoDateTimePicker').enable(true);
        }

        self.viewchangeeta = function (changeeta) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.changeETAModel(changeeta);
            self.isDateEnable(false);
            self.changeETAModel().Date(kendo.toString(new Date(Date.parse(changeeta.CreatedDateAndTime())), 'yyyy-MM-dd HH:mm'));
            self.changeETAModel().ETA(kendo.toString(new Date(Date.parse(changeeta.OldETA())), 'yyyy-MM-dd HH:mm'));
            self.changeETAModel().ETD(kendo.toString(new Date(Date.parse(changeeta.OldETD())), 'yyyy-MM-dd HH:mm'));
            self.changeETAModel().NewETA(new Date(Date.parse(changeeta.NewETA())));
            self.changeETAModel().NewETD(new Date(Date.parse(changeeta.NewETD())));
            $('#spnTitle').html("Change ETA");
            $("#neweta").data('kendoDateTimePicker').enable(false);
            $("#newetd").data('kendoDateTimePicker').enable(false);
            ValidationETA(changeeta);
        }

        self.Cancel = function () {
            if (viewDetail == true) {
                window.location.href = '/VoyageMonitoring/ManageVoyageMonitoring/' + self.selectedVCN();
            }
            else {
                self.viewMode('List');
                self.changeETAModel().reset();
                $('#spnTitle').html('Change ETA');
                self.LoadChangeETA();
            }
        }

        self.ResetChangeETA = function (model) {
            ko.validation.reset();
            model.validationEnabled(false);
            self.ChangeETAValidation = ko.observable(model);
            self.ChangeETAValidation().errors = ko.validation.group(self.ChangeETAValidation());
            self.changeETAModel().reset();
            self.ChangeETAValidation().errors.showAllMessages(false);
            self.IsVCN(false);
            self.IsNewETAValid(false);
            self.IsNewETDValid(false);
            self.IsVoyageIN(false);
            self.IsVoyageOUT(false);
            self.IsRemarks(false);
            self.IsValid(true);
            $("#spanOptValid1").text('');
            $("#spanOptValid2").text('');
            $("#spanOptValid3").text('');
            $("#spanOptValid4").text('');
        }

        self.SrearchChangeETADet = function (data) {
            viewDetail = false;
            self.LoadChangeETA();

            var grid = $("#divChangeETAList").data("kendoGrid");

            if (self.ChangeETAList().length <= 5)
                grid.dataSource.pageSize(5);
            else
                grid.dataSource.pageSize(20);

            grid.refresh();

        }

        self.ResetSearchDet = function (data) {

            viewDetail = false;
            data.VCNSearch('');
            data.VesselNameSearch('')
            data.ETAFrom(new Date());
            data.ETATo(new Date())
            data.AgentNameSearch('')

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate() + 30);
            fromdate.setDate(fromdate.getDate() - 30);
            data.ETAFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            data.ETATo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");


            self.LoadChangeETA();

            var grid = $("#divChangeETAList").data("kendoGrid");

            if (self.ChangeETAList().length <= 5)
                grid.dataSource.pageSize(5);
            else
                grid.dataSource.pageSize(20);

            grid.refresh();
        }


        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        ValidDate = function () {
            self.changeETAModel().ETATo(self.changeETAModel().ETAFrom());
        }



        PlanDateTimeOfBerthCal = function () {
            this.min($("#neweta").val());
            this.max($("#newetd").val());
        }

        PlanDateTimeToStartCargoCal = function () {

            this.min($("#PlanDateTimeOfBerth").val());
            this.max($("#newetd").val());
        }
        PlanDateTimeToCompleteCargoCal = function () {

            this.min($("#PlanDateTimeToStartCargo").val());
            this.max($("#newetd").val());
        }
        PlanDateTimeToVacateBerthCal = function () {

            this.min($("#PlanDateTimeToCompleteCargo").val());
            this.max($("#newetd").val());
        }


        VacateBerth = function (model) {
            if ($("#PlanDateTimeToVacateBerth").val() == "" || $("#PlanDateTimeToVacateBerth").val() == null) {
                $("#spanOptValid4").text('This field is required');
                self.isspanOptValid4(true);
            }
            else {
                $("#spanOptValid4").text('');
                self.isspanOptValid4(false);
            }
        }

        StartCargoOPS = function (model) {
            if ($("#PlanDateTimeToStartCargo").val() == "" || $("#PlanDateTimeToStartCargo").val() == null) {
                $("#spanOptValid3").text('This field is required');
                self.isspanOptValid3(true);
            }
            else {
                $("#PlanDateTimeToCompleteCargo").val('');
                self.changeETAModel().PlanDateTimeToCompleteCargo('');
                $("#PlanDateTimeToVacateBerth").val('');
                self.changeETAModel().PlanDateTimeToVacateBerth('');

                $("#spanOptValid3").text('');
                self.isspanOptValid3(false);
            }
        }

        CompleteCargoOPS = function (model) {
            if ($("#PlanDateTimeToCompleteCargo").val() == "" || $("#PlanDateTimeToCompleteCargo").val() == null) {
                $("#spanOptValid2").text('This field is required');
                self.isspanOptValid2(true);
            }
            else {
                $("#PlanDateTimeToVacateBerth").val('');
                self.changeETAModel().PlanDateTimeToVacateBerth('');
                $("#spanOptValid2").text('');
                self.isspanOptValid2(false);
            }
        }
        PlanDate = function (model) {
            if ($("#PlanDateTimeOfBerth").val() == "" || $("#PlanDateTimeOfBerth").val() == null) {
                $("#spanOptValid1").text('This field is required');
                self.isspanOptValid1(true);
            }
            else {
                $("#PlanDateTimeToCompleteCargo").val('');
                self.changeETAModel().PlanDateTimeToCompleteCargo('');
                $("#PlanDateTimeToStartCargo").val('');
                self.changeETAModel().PlanDateTimeToStartCargo('');
                $("#PlanDateTimeToVacateBerth").val('');
                self.changeETAModel().PlanDateTimeToVacateBerth('');
                $("#spanOptValid1").text('');
                self.isspanOptValid1(false);
            }
        }




        self.Initialize();
    }

    IPMSRoot.ChangeETAViewModel = ChangeETAViewModel;

}(window.IPMSROOT));