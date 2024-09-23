toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";
(function (IPMSRoot) {
    var isView = 0;
    var preferedBerthDraft = 0.0;
    var ArrivalNotificationViewModel = function (vcn, WorkflowInstanceId, viewDetail) {
        var self = this;
        var initaldt = "";
        self.viewArrivalNotification1 = ko.observableArray();
        self.arrivalNotificationList = ko.observableArray();

        self.arrivalNotificationListGrid = ko.observableArray();
        self.arrivalNotificationListSearchGrid = ko.observableArray();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSROOT.validationHelper();
        self.viewMode = ko.observable();
        self.printView = ko.observable(false);
        self.arrivalNotificationModelGrid = ko.observable(new IPMSROOT.ArrivalNotificationModelGrid());
        self.arrivalNotificationModelSearchGrid = ko.observable(new IPMSROOT.ArrivalNotificationModelSearchGrid());
        self.arrivalNotificationModel = ko.observable(new IPMSROOT.ArrivalNotificationModel());

        self.arrivalCommodityModel = ko.observable(new IPMSROOT.ArrivalCommodity());
        self.arrivalNotificationReferenceData = ko.observable();
        self.arrivalNotificationReferenceBirthData = ko.observable();
        self.arrivalNotificationAllBirthData = ko.observable();
        self.RefUserTypeFlag = ko.observable();
        self.isClearenceEnable = ko.observable(false);
        self.isOtherRsonVisible = ko.observable(false);


        self.isArrvalEnable = ko.observable(false);
        self.isPHOEnable = ko.observable(false);
        self.isISPSEnable = ko.observable(false);
        self.isISPSDTLEnable = ko.observable(false);
        self.isIMDGEnable = ko.observable(false);
        self.isIMDGEnablechk = ko.observable(false);

        self.isWasteDeclationEnable = ko.observable(false);


        self.isOtherRsonVisible = ko.observable(false);

        var firstSave = true;


        self.arrivalNotificationReferenceDraftData = ko.observable();

        self.IsAddMode = ko.observable(false);
        self.IsTerminalOperatorChanged = ko.observable(false);
        self.DaylightSpecifyReasonChanged = ko.observable(false);
        self.SpecialNetureChanged = ko.observable(false);
        self.Exeedportlimitenable = ko.observable(false);
        self.ExceedPortLimitationsReasonChanged = ko.observable(false);
        self.ClearanceChanged = ko.observable(false);
        self.ExemptionPilotIDChanged = ko.observable(false);
        self.isGoBackVisible = ko.observable(false);
        self.isGoNextVisible = ko.observable(false);

        self.isSaveVisible = ko.observable(true);
        self.isUpdateVisible = ko.observable(false);
        self.isSubmitVisible = ko.observable(false);
        self.isspanVCNSearchValid = ko.observable(false);
        self.isspanVesselSearchValid = ko.observable(false);



        self.isSaveDraftVisible = ko.observable(false);
        self.isViewMode = ko.observable(false);
        self.isReset = ko.observable(true);
        self.isCancelVisible = ko.observable(true);
        self.LayByVisble = ko.observable(false);
        self.BunkersVisible = ko.observable(false);
        self.viewModeForTabs = ko.observable();
        self.ETAselectedDate = ko.observable();
        self.isEnabled = ko.observable(true);
        self.isVslEnabled = ko.observable(true);
        self.isVisitReasonEnabled = ko.observable(true);
        self.isTOEnabled = ko.observable(true);
        self.isArvValEnable = ko.observable(true);

        self.isExemptionEnable = ko.observable(false);
        self.isDrftCmbEnabled = ko.observable(true);
        self.isVisible = ko.observable(true);
        self.ReferenceData = ko.observable();
        self.BerthList = ko.observableArray([]);
        self.reasonForVisit = ko.observableArray([]);
        self.selectedBerth = ko.observable();
        self.shouldShowDivAV = ko.observable(false);
        self.isAddIMDGInformationsEnabled = ko.observable(true);
        self.isAddTankerCommoditiesEnabled = ko.observable(true);
        self.IsDangerousGoods = ko.observable(false);
        self.IsValidVCN = ko.observable(false);
        self.DryDockDetailsVisible = ko.observable(false);

        self.IsValidDockingPlanID = ko.observable(false);
        self.isDraftVisible = ko.observable(false);
        self.VUPD = ko.observable(false);
        self.isspanEtaValid = ko.observable(false);
        self.isspanVslValid = ko.observable(false);
        self.isspanOptValid1 = ko.observable(false);
        self.isspanOptValid2 = ko.observable(false);
        self.isspanOptValid3 = ko.observable(false);
        self.isspanOptValid4 = ko.observable(false);


        self.WasteDeclarationVisible = ko.observable(false);
        self.isWasteDeclEnablechk = ko.observable(false);

        self.isspanLastPortWaste = ko.observable(false);
        self.isspanNxtPortWaste = ko.observable(false);
        self.isspanDateLastWaste = ko.observable(false);
        self.getMarpolClassList = ko.observableArray();


        self.isspanSpecifyReason = ko.observable(false);

        self.arrivalNotificationReferenceDataforvessel = ko.observable();

        self.isspanloadValid = ko.observable(false);
        self.ismultiplepfileToUpload = ko.observable(false);


        self.isspanEtdValid = ko.observable(false);





        $("#ExemptionSpn").hide();
        self.initialized = false;
        //LoadInitialData fetches the data from Api service
        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/ArrivanNotificationReferenceData', null,
                    function (result1) {
                        self.arrivalNotificationReferenceData(new IPMSRoot.ArrivalNotificationReferenceData(result1));
                        self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
                        self.arrivalNotificationReferenceDraftData(new IPMSRoot.ArrivalNotificationReferenceDraftData(result1));
                        self.RefUserTypeFlag(result1.RefUserType);
                        self.arrivalNotificationModelSearchGrid(new IPMSRoot.ArrivalNotificationModelSearchGrid(undefined));
                    }, null, null, false);
        }




        //LoadArrivalNotifications fetches the data from API Service FROM BBBB
        self.LoadArrivalNotifications = function () {

            if (viewDetail == true) {


                self.viewModelHelper.apiGet('api/ArrivalNotificationss/' + vcn + '/' + WorkflowInstanceId,
                 {},
                  function (result) {
                      self.arrivalNotificationList(ko.utils.arrayMap(result, function (item) {
                          preferedBerthDraft = 0;

                          ko.utils.arrayForEach(self.arrivalNotificationReferenceBirthData().Berths(), function (DocChk) {
                              if (DocChk.BerthKey() === item.PreferedBerthKey) {
                                  preferedBerthDraft = DocChk.Draftm();


                                  //To avoid the alert message when clicking the viewbutton in the Notifications 28-05-2024 venkatesh start
                                  //alert(preferedBerthDraft);
                                  //To avoid the alert message when clicking the viewbutton in the Notifications 28-05-2024 venkatesh end

                                  return;
                              }
                          });
                          return new IPMSRoot.ArrivalNotificationModel(item, self.arrivalNotificationReferenceData());
                      }));


                      if (self.VUPD() == false) {
                          if (result[0].UserType == "AGNT" && result[0].WFCode == "VRES") {

                              self.editArrivalNotification(self.arrivalNotificationList()[0]);
                          }
                          else {
                              self.viewArrivalNotification(self.arrivalNotificationList()[0]);
                          }
                      }
                      else {
                          self.CancelArrivalNotification(self.arrivalNotificationList()[0]);
                      }
                  });

            }
            else {


                var frmdt = self.arrivalNotificationModelSearchGrid().ETAFrom();
                var todt = self.arrivalNotificationModelSearchGrid().ETATo();

                frmdt = moment(frmdt).format('YYYY-MM-DD');
                todt = moment(todt).format('YYYY-MM-DD');

                var vcnSearch = self.arrivalNotificationModelSearchGrid().VCN();
                var veselid = self.arrivalNotificationModelSearchGrid().VesselID();
                var imdg = self.arrivalNotificationModelSearchGrid().IsserchIMDG();
                var isps = self.arrivalNotificationModelSearchGrid().IsserchISPS();

                var imdgClear = self.arrivalNotificationModelSearchGrid().IsserchIMDGClear();
                var ispsClear = self.arrivalNotificationModelSearchGrid().IsserchISPSClear();
                var phoClear = self.arrivalNotificationModelSearchGrid().IsserchPHOClear();


                if (vcnSearch == '') {
                    vcnSearch = 'NA';

                }
                if (veselid == '') {
                    veselid = 0;
                }


                self.viewModelHelper.apiGet('api/ArrivalNotificationssearchgrd/' + frmdt + '/' + todt + '/' + vcnSearch + '/' + veselid + '/' + imdg + '/' + isps + '/' + imdgClear + '/' + ispsClear + '/' + phoClear,
              {},
             function (result) {
                 debugger
                 self.arrivalNotificationListGrid(ko.utils.arrayMap(result, function (item) {
                     return new IPMSRoot.ArrivalNotificationModelGrid(item);
                 }));


             });
            }

        }

        // Srini
        self.ClearFilter = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate() + 30);
            fromdate.setDate(fromdate.getDate() - 30);


            self.arrivalNotificationModelSearchGrid().ETAFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.arrivalNotificationModelSearchGrid().ETATo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");


            self.arrivalNotificationModelSearchGrid().VCN('');
            self.arrivalNotificationModelSearchGrid().VCNSERCH('');
            self.arrivalNotificationModelSearchGrid().VesselID('');
            self.arrivalNotificationModelSearchGrid().VesselName('');
            self.arrivalNotificationModelSearchGrid().IsserchIMDG('All');
            self.arrivalNotificationModelSearchGrid().IsserchISPS('All');

            self.arrivalNotificationModelSearchGrid().IsserchIMDGClear(false);
            self.arrivalNotificationModelSearchGrid().IsserchISPSClear(false);
            self.arrivalNotificationModelSearchGrid().IsserchPHOClear(false);

            $("#spanVCNSearchValid").text('');
            $("#spanVesselSearchValid").text('');
            self.isspanVCNSearchValid(false);
            self.isspanVesselSearchValid(false);
            self.LoadArrivalNotifications();
        }

        self.GetDataClick = function (model) {
            var serchalrt = true;
            if (model.VCNSERCH() != model.VCN()) {
                serchalrt = false;
                $("#spanVCNSearchValid").text('Please select valid VCN');
                self.isspanVCNSearchValid(true);
            }
            else {
                $("#spanVCNSearchValid").text('');
                self.isspanVCNSearchValid(false);
            }

            if (model.VesselID() == "" && model.VesselName() != "") {
                serchalrt = false;
                $("#spanVesselSearchValid").text('Please select valid Vessel Name/IMO No.');
                self.isspanVesselSearchValid(true);
            }
            else {
                $("#spanVesselSearchValid").text('');
                self.isspanVesselSearchValid(false);
            }

            if (serchalrt) {
                $("#spanVCNSearchValid").text('');
                $("#spanVesselSearchValid").text('');
                self.isspanVCNSearchValid(false);
                self.isspanVesselSearchValid(false);
                self.LoadArrivalNotifications();
            }
        }

        self.LoadArrivalNotificationsSearch = function (data) {

            var ETAFrom = $("#SearchETAFrom").val();
            var ETATo = $("#SearchETATo").val();


            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate() + 30);
            fromdate.setDate(fromdate.getDate() - 30);

            self.arrivalNotificationModel().ETAFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            var CurrentDate = new Date();

            if (ETAFrom == "" || ETAFrom == undefined) {
                ETAFrom = self.arrivalNotificationModel().ETAFrom();
            }

            self.arrivalNotificationModel().ETATo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
            if (ETATo == "" || ETATo == undefined) {
                ETATo = self.arrivalNotificationModel().ETATo();
            }

            self.viewModelHelper.apiGet('api/ArrivalNotifications/' + ETAFrom + '/' + ETATo, {},
                        function (result) {
                            self.arrivalNotificationList(ko.utils.arrayMap(result, function (item) {
                                return new IPMSRoot.ArrivalNotificationModel(item, self.arrivalNotificationReferenceData());
                            }));
                        });
        }

        SearchETAtoCal = function () {
            this.min($("#SearchETAFrom").val());

        }


        SearchValidDate = function (data, event) {
            self.arrivalNotificationModelSearchGrid().ETATo(self.arrivalNotificationModelSearchGrid().ETAFrom());
        }

        SerchVesselKeyPress = function (e) {
            self.arrivalNotificationModelSearchGrid().VesselID('');
        }

        SerchVesselBackSpace = function (e) {
            self.arrivalNotificationModelSearchGrid().VesselID('');
        }


        self.VesselSelectSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.arrivalNotificationModelSearchGrid().VesselID(selecteddataItem.VesselID);
            self.arrivalNotificationModelSearchGrid().VesselName(selecteddataItem.VesselName);
        }

        SerchVCNBackSpace = function (e) {
            self.arrivalNotificationModelSearchGrid().VCNSERCH('');
        }



        SerchVCNBackSpaceNumValid = function (evt) {

            self.arrivalNotificationModelSearchGrid().VCNSERCH('');

            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }


        self.VCNSelectSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.arrivalNotificationModelSearchGrid().VCNSERCH(selecteddataItem.vcn);
        }



        // Srini End
        //Binding Vessel Information Bhoji
        self.VesselSelect = function (e) {

            self.DryDockDetailsVisible(false);
            self.isspanVslValid(false);
            $("select#reasonforvisit").prop('selectedIndex', 0);
            var selecteddataItem = this.dataItem(e.item.index());
            self.arrivalNotificationModel().VesselData(selecteddataItem);
            self.arrivalNotificationModel().VesselID(selecteddataItem.VesselID);
            self.arrivalNotificationModel().IMONo(selecteddataItem.IMONo);
            self.arrivalNotificationModel().DockingPlanID(selecteddataItem.DockingPlanID);
            self.arrivalNotificationModel().SubmissionDate(selecteddataItem.SubmissionDate);
            self.arrivalNotificationModel().VesselName(selecteddataItem.VesselName);

            self.arrivalNotificationModel().DockIsFinal(selecteddataItem.DockIsFinal);
            self.arrivalNotificationModel().DockStatus(selecteddataItem.DockStatus);

            if (selecteddataItem.GrossRegisteredTonnageInMT > 500) {
                self.arrivalNotificationModel().AppliedForISPS('A');
                $("#IspsClearenceSpn").show();
                $("#AppliedDate").attr("disable", false);
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(true);
                self.isISPSEnable(false);
            }
            else {
                self.arrivalNotificationModel().AppliedForISPS('I');
                self.arrivalNotificationModel().ISPSReferenceNo('');
                self.arrivalNotificationModel().AppliedDate('');
                $("#IspsClearenceSpn").hide();
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(false);
                self.isISPSEnable(true);
            }

            if (selecteddataItem.DockingPlanID != null && selecteddataItem.DockingPlanID != '' && selecteddataItem.DockIsFinal == "Y" && selecteddataItem.DockStatus == "A") {
                self.IsValidDockingPlanID(true);
            }
            else {
                self.IsValidDockingPlanID(false);
            }

            if ($("#ETA").val() == "" || $("#ETA").val() == null) {
            }
            else {

                var dateobj = kendo.parseDate(self.arrivalNotificationModel().ETA(), "yyyy-MM-dd HH:mm");
                var datestring = kendo.toString(dateobj, "yyyy-MM-dd HH:mm");
                self.arrivalNotificationModel().ETA(datestring);
                self.viewModelHelper.apiPost('api/ArrivalDuplicateData', ko.mapping.toJSON(self.arrivalNotificationModel()), function Message(data) {
                    var ValidRule = data.split('@');
                    if (ValidRule[0] == 1) {
                        self.arrivalNotificationModel().VesselName('');
                        self.arrivalNotificationModel().VesselData('');
                        self.arrivalNotificationModel().VesselID('');
                        self.arrivalNotificationModel().IMONo('');
                        self.arrivalNotificationModel().DockingPlanID('');
                        self.arrivalNotificationModel().SubmissionDate('');
                        self.DryDockDetailsVisible(false);
                        toastr.warning("Vessel is on the Berth with" + ValidRule[1]);
                    }
                    else if (ValidRule[0] == 2) {
                        self.arrivalNotificationModel().VesselName('');
                        self.arrivalNotificationModel().VesselData('');
                        self.arrivalNotificationModel().VesselID('');
                        self.arrivalNotificationModel().IMONo('');
                        self.arrivalNotificationModel().DockingPlanID('');
                        self.arrivalNotificationModel().SubmissionDate('');
                        self.DryDockDetailsVisible(false);
                        toastr.warning("Arrival Notification is already raised on " + ValidRule[1]);
                    }
                    else if (ValidRule[0] == 3) {
                        self.arrivalNotificationModel().VesselName('');
                        self.arrivalNotificationModel().VesselData('');
                        self.arrivalNotificationModel().VesselID('');
                        self.arrivalNotificationModel().IMONo('');
                        self.arrivalNotificationModel().DockingPlanID('');
                        self.arrivalNotificationModel().SubmissionDate('');
                        self.DryDockDetailsVisible(false);
                        toastr.warning("" + ValidRule[1] + " is already Rejected with same VoyageIn/VoyageOut values for this Vessel");
                    }
                });
            }
        }





        self.SecondaryAgentID1Select = function (e) {

            var selecteddataItem = this.dataItem(e.item.index());
            self.arrivalNotificationModel().SecondaryAgentID1(selecteddataItem.AgentID);
        }

        self.SecondaryAgentID2Select = function (e) {

            var selecteddataItem = this.dataItem(e.item.index());
            self.arrivalNotificationModel().SecondaryAgentID2(selecteddataItem.AgentID);
        }


        AgentName2Blur = function () {
            if (self.arrivalNotificationModel().SecondaryAgent2Name() == "") {
                self.arrivalNotificationModel().SecondaryAgentID2('');
                self.arrivalNotificationModel().SecondaryAgent2Name('');
            }
        }
        AgentName1Blur = function () {
            if (self.arrivalNotificationModel().SecondaryAgent1Name() == "") {
                self.arrivalNotificationModel().SecondaryAgentID1('');
                self.arrivalNotificationModel().SecondaryAgent1Name('');

            }
        }


        AgentName1Keypress = function () {
            if (self.arrivalNotificationModel().AgentID() != "") {
                self.arrivalNotificationModel().SecondaryAgentID1('');
                self.arrivalNotificationModel().SecondaryAgent1Name('');
            }
        }

        AgentName2Keypress = function () {
            if (self.arrivalNotificationModel().AgentID() != "") {
                self.arrivalNotificationModel().SecondaryAgentID2('');
                self.arrivalNotificationModel().SecondaryAgent2Name('');

            }
        }

        VesselNameBlur = function () {
            if (self.arrivalNotificationModel().VesselID() == "") {
                self.arrivalNotificationModel().VesselName('');
                self.arrivalNotificationModel().VesselData('');
                self.arrivalNotificationModel().VesselID('');
                self.arrivalNotificationModel().IMONo('');
                self.arrivalNotificationModel().DockingPlanID('');
                self.arrivalNotificationModel().SubmissionDate('');
                $("select#reasonforvisit").prop('selectedIndex', 0);
                self.DryDockDetailsVisible(false);

                self.arrivalNotificationModel().AppliedForISPS('I');
                self.arrivalNotificationModel().ISPSReferenceNo('');
                self.arrivalNotificationModel().AppliedDate('');
                $("#IspsClearenceSpn").hide();
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(false);
            }
        }

        VesselNameKeypress = function () {
            if (self.arrivalNotificationModel().VesselID() != "") {
                self.arrivalNotificationModel().VesselData('');
                self.arrivalNotificationModel().VesselID('');
                self.arrivalNotificationModel().IMONo('');
                self.arrivalNotificationModel().DockingPlanID('');
                self.arrivalNotificationModel().SubmissionDate('');
                $("select#reasonforvisit").prop('selectedIndex', 0);
                self.DryDockDetailsVisible(false);


                self.arrivalNotificationModel().AppliedForISPS('I');
                self.arrivalNotificationModel().ISPSReferenceNo('');
                self.arrivalNotificationModel().AppliedDate('');
                $("#IspsClearenceSpn").hide();
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(false);
            }
        }


        self.LoadTOBirths = function (e) {
            var DataItem = this.dataItem(e.item.index());

            if (DataItem.TerminalOperatorid > 0) {
                self.arrivalNotificationModel().TerminalOperatorID(DataItem.TerminalOperatorid);

                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: DataItem.TerminalOperatorid },
                     function (result1) {

                         self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
                     }, null, null, false);
            }
            else {
                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: "0" },
                     function (result1) {
                         self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
                     }, null, null, false);

            }

        }




        //VesselDeetails fetches the data from API Service to AutoComplete text box
        VesselEvent = function (data, event) {
            self.viewModelHelper.apiGet('api/Vessels/' + VesselName(),
                                    { vslname: VesselName() },
                                      function (result) {
                                          console.log(ko.toJSON(result));
                                          return result;

                                      });
        }

        //TerminalOperator fetches the data from API Service to AutoComplete text box
        self.arrivalNotificationModel().IsTerminalOperator.subscribe(function (item) {
            if (item == "Y") {
                self.IsTerminalOperatorChanged(true);
            }
            else if (item == "N") {
                self.IsTerminalOperatorChanged(false);
            }
        });



        self.AlterBerth = function (event) {
            if ($("#preferredberthList").val() == '') {
                $("select#alterBirthname").prop('selectedIndex', 0);
                toastr.warning("Please Select Preferred Berth");
                return;
            }
            if (event.AlternateBerthKey() == $("#preferredberthList").val()) {
                $("select#alterBirthname").prop('selectedIndex', 0);
                toastr.warning("Preferred Berth and Alternate Berth Cannot be Same");
                return;
            }

        }

        self.ExemptionChange = function (event) {
            if (event.ExemptionPilotID() > 0) {
                if (!event.PilotExemptionChecked()) {
                    $("select#exemptionname").prop('selectedIndex', 0);
                    toastr.warning("Please select Pilot Exemption Checkbox");
                    return;
                }
            }
        }



        self.TOHit = function (event) {
            self.isTOEnabled(true);
            $("#IsTOStrSpn").show();
            self.arrivalNotificationModel().TerminalOperatorID('');
            $("#SpecTO").focus();
        }
        self.TOHitNo = function (event) {
            $("#IsTOStrSpn").hide();
            self.isTOEnabled(false);
            self.arrivalNotificationModel().TerminalOperatorID('');
            $("#SpecTO").val('');
            self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: "0" },
                   function (result1) {
                       self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
                   }, null, null, false);
        }

        //LoadLayup fetches the ReasonForVisit details from API Service 
        self.LoadLayup = function (event) {
            self.BunkersVisible(false);
            self.arrivalNotificationModel().bunkersVisible(false);
            self.LayByVisble(false);
            self.arrivalNotificationModel().layByVisble(false);
            self.isOtherRsonVisible(false);
            self.DryDockDetailsVisible(false);
            var isDRYD = 0;
            var isLABYREPA = 0;
            var isBUNK = 0;
            var isOTHR = 0;
            for (var i = 0; i < event.ArrivaReasonArray().length; i++) {
                event.ReasonForVisit(event.ArrivaReasonArray()[i]);
                if ($("#txtVessels").val() == '') {
                    self.DryDockDetailsVisible(false);
                    $("select#reasonforvisit").prop('selectedIndex', 0);
                    toastr.warning("Please Select VCN", "Arrival Notification");
                    return;
                }
                if (event.ReasonForVisit() == 'DRYD' && !(self.IsValidDockingPlanID())) {
                    self.DryDockDetailsVisible(false);

                    var Repair = ["DRYD"];
                    var ReasonforVisitKendo = $("#reasonforvisit").data('kendoMultiSelect');
                    var values = ReasonforVisitKendo.value().slice();
                    values = $.grep(values, function (a) {
                        return $.inArray(a, Repair) == -1;
                    });
                    ReasonforVisitKendo.dataSource.filter({});
                    ReasonforVisitKendo.value(values);
                    toastr.warning("Docking Plan Must Be Approved when Reason For Visit is Dry Dock", "Arrival Notification");
                    return;
                }
                else if (event.ReasonForVisit() == 'DRYD') {
                    self.DryDockDetailsVisible(true);
                    isDRYD = 1;
                }
                if (event.ReasonForVisit() == 'LABY' || event.ReasonForVisit() == 'REPA') {
                    self.LayByVisble(true);
                    self.arrivalNotificationModel().layByVisble(true);
                    isLABYREPA = 1;
                }
                else if (event.ReasonForVisit() == 'BUNK') {

                    self.BunkersVisible(true);
                    self.arrivalNotificationModel().bunkersVisible(true);
                    isBUNK = 1;
                }
                else if (event.ReasonForVisit() == 'OTHR') {
                    self.isOtherRsonVisible(true);
                    isOTHR = 1;
                }
            }
            if (isDRYD == 0) {
                self.arrivalNotificationModel().DryDockBerthKey('');
            }
            if (isOTHR == 0) {
                self.arrivalNotificationModel().SpecifyReason('');
            }
            if (isBUNK == 0) {
                self.arrivalNotificationModel().BunkersRequired('');
                self.arrivalNotificationModel().BunkersMethod('');
                self.arrivalNotificationModel().BunkerService('');
                self.arrivalNotificationModel().DistanceFromStern('');
                self.arrivalNotificationModel().TonsMT('');
                self.arrivalNotificationModel().AnyImpInfo('');
            }
            if (isLABYREPA == 0) {
                self.arrivalNotificationModel().PlannedDurationDate('');
                self.arrivalNotificationModel().PlannedDurationToDate('');
                self.arrivalNotificationModel().Daycnt('');
                self.arrivalNotificationModel().ReasonForLayup('');
            }
        }

        //DaylightRestriction fetches the ClearanceChanged details from API Service 
        self.arrivalNotificationModel().Clearance.subscribe(function (item) {

            if (item == "Y") {

                self.ClearanceChanged(true);
            }
            else if (item == "N") {

                self.ClearanceChanged(false);
            }
        });

        //Arrival Notification Repair Date Change
        ChangeRepairDate = function () {
            $("#PlannedDurationToDate").val('');
            self.arrivalNotificationModel().PlannedDurationToDate('');
            self.arrivalNotificationModel().Daycnt('');
        }


        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }


        //DaylightRestriction fetches the DaylightSpecifyReason details from API Service 
        self.arrivalNotificationModel().ExemptionPilotID.subscribe(function (item) {
            if (item !== undefined) {
                self.arrivalNotificationModel().MasterName(item.FirstName);
            }
            else {
                self.arrivalNotificationModel().MasterName("");
            }
        });

        //Calculating no of Days
        Calcdate = function (data, event) {
            var today = this.value();
            var dobDate = kendo.parseDate(self.arrivalNotificationModel().PlannedDurationDate());
            var days = (today - dobDate) / (60 * 60 * 24 * 1000);
            self.arrivalNotificationModel().Daycnt(days);
        }
        // validate the data 
        self.ValidateForm = function (ArrivalNotificationData) {


            $("#CellNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            toastr.clear();
            ArrivalNotificationData.ReasonForVisit('');
            var isOptnlInfo = 0;
            for (var i = 0; i < ArrivalNotificationData.ArrivaReasonArray().length; i++) {
                ArrivalNotificationData.ReasonForVisit(ArrivalNotificationData.ArrivaReasonArray()[i]);
                if (ArrivalNotificationData.ReasonForVisit() == 'DRYD' || ArrivalNotificationData.ReasonForVisit() == 'BUNK' || ArrivalNotificationData.ReasonForVisit() == 'LABY' || ArrivalNotificationData.ReasonForVisit() == 'REPA') {
                }
                else {
                    isOptnlInfo = 1;
                }
            }

            ArrivalNotificationData.PlanDateTimeOfBerth.extend({ required: true });
            ArrivalNotificationData.PlanDateTimeToVacateBerth.extend({ required: true });
            ArrivalNotificationData.PlanDateTimeToStartCargo.extend({ required: true });
            ArrivalNotificationData.PlanDateTimeToCompleteCargo.extend({ required: true });
            if (isOptnlInfo == 0) {
                ArrivalNotificationData.PlanDateTimeOfBerth.rules.remove(function (item) { return item.rule = "required"; });
                ArrivalNotificationData.PlanDateTimeToVacateBerth.rules.remove(function (item) { return item.rule = "required"; });
                ArrivalNotificationData.PlanDateTimeToStartCargo.rules.remove(function (item) { return item.rule = "required"; });
                ArrivalNotificationData.PlanDateTimeToCompleteCargo.rules.remove(function (item) { return item.rule = "required"; });
                self.isspanOptValid1(false);
                self.isspanOptValid2(false);
                self.isspanOptValid3(false);
                self.isspanOptValid4(false);
            }
            if (ArrivalNotificationData.ArrDraft() == '.' ||
                ArrivalNotificationData.ArrDraft() == null ||
                ArrivalNotificationData.ArrDraft() == '' ||
                ArrivalNotificationData.DepDraft() == null ||
                ArrivalNotificationData.DepDraft() == '' ||
                ArrivalNotificationData.DepDraft() == '.') {
                toastr.warning("Please Enter Valid draft details ", "Arrival Notification");
                ArrivalNotificationData.PreferedBerthKey(undefined);
                return;
            } else {
                var IsvalidDraft = true;
                var aa = '';

                var aftd = parseFloat(ArrivalNotificationData.ArrDraft()).toFixed(2);
                var depd = parseFloat(ArrivalNotificationData.DepDraft()).toFixed(2);
                var actdft = preferedBerthDraft.toFixed(2);
                if (compare(actdft, aftd) && compare(actdft, depd)) {
                    aa = "Arr. Draft(m) - " +
                        aftd +
                        " and Dep. Draft(m) - " +
                        depd +
                        " are more than Berth Draft - " +
                        actdft;
                    IsvalidDraft = false;
                } else if (compare(actdft, aftd)) {
                    aa = "Arr. Draft(m) - " + aftd + " is more than Berth Draft - " + actdft;
                    IsvalidDraft = false;
                } else if (compare(actdft, depd)) {
                    aa = "Dep. Draft(m) - " + depd + " is more than Berth Draft - " + actdft;
                    IsvalidDraft = false;
                }
                if (!IsvalidDraft) {
                    toastr.error(aa, "Arrival Notification");
                    return false;
                }

            }

            self.ArrivalNotificationValidation = ko.observable(ArrivalNotificationData);
            self.ArrivalNotificationValidation().errors = ko.validation.group(self.ArrivalNotificationValidation());
            var errors = self.ArrivalNotificationValidation().errors().length;
            if (ValidateFormValues(self, ArrivalNotificationData) == true) {

                if (ArrivalNotificationData.VesselID() == '' || ArrivalNotificationData.VesselName() == '') {
                    self.isspanVslValid(true);
                    $("#spanVslValid").text('This field is required');

                    toastr.warning("You Have Some form Errors. Please Check Below.");
                    result = false;
                }
                else {
                    self.isspanVslValid(false);
                    if (self.viewModeForTabs() == "notification1") {
                        GoToTab2(self, ArrivalNotificationData);
                    }
                    else if (self.viewModeForTabs() == "notification2") {
                        GoToTab3(self, ArrivalNotificationData);
                    }
                    else {
                        return;
                    }
                }
            }

            if (viewDetail === true) {

                self.isSubmitVisible(false);
            }

        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
        //ArrivalNotification Draft Data saveing data in API Service 
        self.DraftArrivalNotification = function (ArrivalNotificationData) {
            toastr.clear();
            self.isspanVslValid = ko.observable(false);
            self.ArrivalNotificationValidation = ko.observable(ArrivalNotificationData);
            self.ArrivalNotificationValidation().errors = ko.validation.group(self.ArrivalNotificationValidation());
            var arrivalNotificationValidationerrors = self.ArrivalNotificationValidation().errors().length;
            var isdrafterror = 'N';
            if (arrivalNotificationValidationerrors == 0) {

                if (ArrivalNotificationData.ArrivalCommodities().length > 0) {
                    $.each(ArrivalNotificationData.ArrivalCommodities(), function (key, item) {
                        var CommodChk = item;
                        if (CommodChk != null)
                            if (CommodChk !== undefined) {
                                var QuantityVal = CommodChk.Quantity();
                                if (QuantityVal == '' || QuantityVal == null) {
                                    QuantityVal = 0;
                                }
                                if (CommodChk.CommodityBerthKey() == undefined || CommodChk.CargoType() == undefined || CommodChk.Commodity() == undefined || CommodChk.Package() == undefined || CommodChk.UOM() == undefined || CommodChk.CommodityBerthKey() == "" || CommodChk.CargoType() == "" || CommodChk.Commodity() == "" || CommodChk.Package() == "" || CommodChk.UOM() == "" || QuantityVal == 0) {
                                    isdrafterror = 'Y';
                                    toastr.warning("Please enter valid details of Quantities of Commodity", "Arrival Notification");
                                    result = false;
                                }
                            }
                    });
                }
                if (ArrivalNotificationData.ViewModeForTabs() == 'notification2') {
                    if (ArrivalNotificationData.IMDGInformations().length > 0) {
                        var IMDGInformationsDetails = ko.utils.arrayFilter(ArrivalNotificationData.IMDGInformations(), function (items) {
                            if (items.Others() == null)
                                items.Others('');
                            if (items.Purpose() == null || items.ClassCode() == null || items.CargoCode() == null || items.UNNo() == '') {
                                isdrafterror = 'Y';
                                toastr.warning("Please Enter Valid Details of IMDG Cargo Information", "Arrival Notification");
                                result = false;
                            }
                        });
                    }

                    if (ArrivalNotificationData.WasteDeclarations().length > 0) {

                        var WasteDeclarationsDetails = ko.utils.arrayFilter(ArrivalNotificationData.WasteDeclarations(), function (items) {
                            if (items.Others() == null)
                                items.Others('');
                            if (items.MarpolCode() == null || items.ClassCode() == null || items.LicenseRequestID() == null || items.Quantity() == "") {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.warning("Please Enter Valid Details of Waste Declaration Information", "Arrival Notification");
                                result = false;
                            }
                        });
                    }

                }
            }

            if (arrivalNotificationValidationerrors == 0 && isdrafterror == 'N') {
                if ($('input:radio[name=IsDangerousGoodsonBoard]:checked').val() == "A") {
                    ArrivalNotificationData.AnyDangerousGoodsonBoard('A')
                }
                else {
                    ArrivalNotificationData.AnyDangerousGoodsonBoard('I');
                }

                if ($('input:radio[name=IsDangerousGoodsonBoard]:checked').val() == "A") {
                    ArrivalNotificationData.AnyDangerousGoodsonBoard('A')
                }
                else {
                    ArrivalNotificationData.AnyDangerousGoodsonBoard('I');
                }

                if (ArrivalNotificationData.IsTerminalOperator() == "A") {
                }
                else {
                    ArrivalNotificationData.TerminalOperatorID('');
                }
                var dateobj = kendo.parseDate(ArrivalNotificationData.ETA(), "yyyy-MM-dd HH:mm");
                var datestring = kendo.toString(dateobj, "yyyy-MM-dd HH:mm");
                ArrivalNotificationData.ETA(datestring);

                var dateobjETD = kendo.parseDate(ArrivalNotificationData.ETD(), "yyyy-MM-dd HH:mm");
                var datestringETD = kendo.toString(dateobjETD, "yyyy-MM-dd HH:mm");
                ArrivalNotificationData.ETD(datestringETD);

                var objPlanDateTimeOfBerth = kendo.parseDate(ArrivalNotificationData.PlanDateTimeOfBerth(), "yyyy-MM-dd HH:mm");
                var objstrPlanDateTimeOfBerth = kendo.toString(objPlanDateTimeOfBerth, "yyyy-MM-dd HH:mm");
                ArrivalNotificationData.PlanDateTimeOfBerth(objstrPlanDateTimeOfBerth);

                var objPlanDateTimeToVacateBerth = kendo.parseDate(ArrivalNotificationData.PlanDateTimeToVacateBerth(), "yyyy-MM-dd HH:mm");
                var objstrPlanDateTimeToVacateBerth = kendo.toString(objPlanDateTimeToVacateBerth, "yyyy-MM-dd HH:mm");
                ArrivalNotificationData.PlanDateTimeToVacateBerth(objstrPlanDateTimeToVacateBerth);

                var objPlanDateTimeToStartCargo = kendo.parseDate(ArrivalNotificationData.PlanDateTimeToStartCargo(), "yyyy-MM-dd HH:mm");
                var objstrPlanDateTimeToStartCargo = kendo.toString(objPlanDateTimeToStartCargo, "yyyy-MM-dd HH:mm");
                ArrivalNotificationData.PlanDateTimeToStartCargo(objstrPlanDateTimeToStartCargo);

                var objPlanDateTimeToCompleteCargo = kendo.parseDate(ArrivalNotificationData.PlanDateTimeToCompleteCargo(), "yyyy-MM-dd HH:mm");
                var objstrPlanDateTimeToCompleteCargo = kendo.toString(objPlanDateTimeToCompleteCargo, "yyyy-MM-dd HH:mm");
                ArrivalNotificationData.PlanDateTimeToCompleteCargo(objstrPlanDateTimeToCompleteCargo);

                var objAppliedDate = kendo.parseDate(ArrivalNotificationData.AppliedDate(), "yyyy-MM-dd");
                var objstrAppliedDate = kendo.toString(objAppliedDate, "yyyy-MM-dd");
                ArrivalNotificationData.AppliedDate(objstrAppliedDate);

                var objLoadDischargeDate = kendo.parseDate(ArrivalNotificationData.LoadDischargeDate(), "yyyy-MM-dd");
                var objstrLoadDischargeDate = kendo.toString(objLoadDischargeDate, "yyyy-MM-dd");
                ArrivalNotificationData.LoadDischargeDate(objstrLoadDischargeDate);


                var objDischargeDate = kendo.parseDate(ArrivalNotificationData.DischargeDate(), "yyyy-MM-dd");
                var objstrDischargeDate = kendo.toString(objDischargeDate, "yyyy-MM-dd");
                ArrivalNotificationData.DischargeDate(objstrDischargeDate);



                var objPlannedDurationDate = kendo.parseDate(ArrivalNotificationData.PlannedDurationDate(), "yyyy-MM-dd");
                var objstrPlannedDurationDate = kendo.toString(objPlannedDurationDate, "yyyy-MM-dd");
                ArrivalNotificationData.PlannedDurationDate(objstrPlannedDurationDate);

                var objPlannedDurationToDate = kendo.parseDate(ArrivalNotificationData.PlannedDurationToDate(), "yyyy-MM-dd");
                var objstrPlannedDurationToDate = kendo.toString(objPlannedDurationToDate, "yyyy-MM-dd");
                ArrivalNotificationData.PlannedDurationToDate(objstrPlannedDurationToDate);



                var objDateLastWasteDelivered = kendo.parseDate(ArrivalNotificationData.DateLastWasteDelivered(), "yyyy-MM-dd HH:mm");
                var objstrDateLastWasteDelivered = kendo.toString(objDateLastWasteDelivered, "yyyy-MM-dd HH:mm");
                ArrivalNotificationData.DateLastWasteDelivered(objstrDateLastWasteDelivered);


                self.viewModelHelper.isLoading(true);
                if (ArrivalNotificationData.VCN() == "") {
                    self.viewModelHelper.apiPost('api/DraftArrivalNotifications', ko.mapping.toJSON(ArrivalNotificationData), function Message(data) {
                        self.viewModelHelper.isLoading(false);
                        toastr.success("Arrival Notification Draft details saved successfully for " + data.VCN + ".", "Arrival Notification");
                        self.LoadArrivalNotifications();
                        self.viewMode('List');
                    });
                }
                else {
                    self.viewModelHelper.isLoading(true);
                    self.viewModelHelper.apiPost('api/DraftArrivalNotifications', ko.mapping.toJSON(ArrivalNotificationData), function Message(data) {
                        self.viewModelHelper.isLoading(false);
                        toastr.success("Arrival Notification Draft details updated successfully for " + data.VCN + ".", "Arrival Notification");
                        self.LoadArrivalNotifications();
                        self.viewMode('List');
                    });
                }
            }
            else {



                if ($("#ETA").val() == "" || $("#ETA").val() == null) {
                    $("#spanEtaValid").text('This field is required');
                    self.isspanEtaValid(true);
                }
                else {
                    $("#ValidityDateMsg").text('');
                    self.isspanEtaValid(false);
                }

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
                if ($("#ETD").val() == "" || $("#ETD").val() == null) {
                    $("#spanEtdValid").text('This field is required');
                    self.isspanEtdValid(true);

                }
                else {
                    $("#spanEtdValid").text('');
                    self.isspanEtdValid(false);
                }

                self.ArrivalNotificationValidation().errors.showAllMessages();
                toastr.warning("You Have Some form Errors. Please Check Below.");
                return;

            }


        }

        VacateBerth = function (ArrivalNotificationData) {
            if ($("#PlanDateTimeToVacateBerth").val() == "" || $("#PlanDateTimeToVacateBerth").val() == null) {
                $("#spanOptValid4").text('This field is required');
                self.isspanOptValid4(true);
            }
            else {
                $("#spanOptValid4").text('');
                self.isspanOptValid4(false);
            }
        }

        StartCargoOPS = function (ArrivalNotificationData) {
            if ($("#PlanDateTimeToStartCargo").val() == "" || $("#PlanDateTimeToStartCargo").val() == null) {
                $("#spanOptValid3").text('This field is required');
                self.isspanOptValid3(true);
            }
            else {
                $("#PlanDateTimeToCompleteCargo").val('');
                self.arrivalNotificationModel().PlanDateTimeToCompleteCargo('');
                $("#PlanDateTimeToVacateBerth").val('');
                self.arrivalNotificationModel().PlanDateTimeToVacateBerth('');

                $("#spanOptValid3").text('');
                self.isspanOptValid3(false);
            }
        }

        CompleteCargoOPS = function (ArrivalNotificationData) {
            if ($("#PlanDateTimeToCompleteCargo").val() == "" || $("#PlanDateTimeToCompleteCargo").val() == null) {
                $("#spanOptValid2").text('This field is required');
                self.isspanOptValid2(true);
            }
            else {
                $("#PlanDateTimeToVacateBerth").val('');
                self.arrivalNotificationModel().PlanDateTimeToVacateBerth('');
                $("#spanOptValid2").text('');
                self.isspanOptValid2(false);
            }
        }
        PlanDate = function (ArrivalNotificationData) {
            if ($("#PlanDateTimeOfBerth").val() == "" || $("#PlanDateTimeOfBerth").val() == null) {
                $("#spanOptValid1").text('This field is required');
                self.isspanOptValid1(true);
            }
            else {
                $("#PlanDateTimeToCompleteCargo").val('');
                self.arrivalNotificationModel().PlanDateTimeToCompleteCargo('');
                $("#PlanDateTimeToStartCargo").val('');
                self.arrivalNotificationModel().PlanDateTimeToStartCargo('');
                $("#PlanDateTimeToVacateBerth").val('');
                self.arrivalNotificationModel().PlanDateTimeToVacateBerth('');
                $("#spanOptValid1").text('');
                self.isspanOptValid1(false);
            }
        }

        self.ChangeVoyageIn = function (ArrivalNotificationData) {
            var vesselid = ArrivalNotificationData.VesselID() != "" && ArrivalNotificationData.VesselID() != undefined ? ArrivalNotificationData.VesselID() : null;
            var voyagein = ArrivalNotificationData.VoyageIn() != "" && ArrivalNotificationData.VoyageIn() != undefined ? ArrivalNotificationData.VoyageIn() : null;
            var voyageout = ArrivalNotificationData.VoyageOut() != "" && ArrivalNotificationData.VoyageOut() != undefined ? ArrivalNotificationData.VoyageOut() : null;

            if (vesselid != null && vesselid != undefined && vesselid != "" && vesselid != "undefined") {
                self.viewModelHelper.apiGet('api/ArrivalNotificationVoyageValidation/' + vesselid + '/' + voyagein + '/' + voyageout,
                null,
             function Message(data) {
                 var ValidRule = data.split('@');
                 if (ValidRule[0] == 1) {
                     toastr.warning("" + ValidRule[1] + " is already rejected with same Voyage In/Voyage Out values for this Vessel ");
                     self.viewModelHelper.isLoading(false);
                     $("#VoyageIn").val('');
                     self.arrivalNotificationModel().VoyageIn('');
                     $('#VoyageIn').focus();
                 }
                 else {
                     self.viewModelHelper.isLoading(false);
                     $('#VoyageOut').focus();
                 }
             });
            }

        }


        self.ChangeVoyageOut = function (ArrivalNotificationData) {
            var vesselid = ArrivalNotificationData.VesselID();
            var voyagein = ArrivalNotificationData.VoyageIn() != "" && ArrivalNotificationData.VoyageIn() != undefined ? ArrivalNotificationData.VoyageIn() : null;
            var voyageout = ArrivalNotificationData.VoyageOut() != "" && ArrivalNotificationData.VoyageOut() != undefined ? ArrivalNotificationData.VoyageOut() : null;
            self.viewModelHelper.apiGet('api/ArrivalNotificationVoyageValidation/' + vesselid + '/' + voyagein + '/' + voyageout,
                null,
             function Message(data) {
                 var ValidRule = data.split('@');
                 if (ValidRule[0] == 1) {
                     toastr.warning("" + ValidRule[1] + " is already rejected with same Voyage In/Voyage Out values for this Vessel ");
                     self.viewModelHelper.isLoading(false);
                     $("#VoyageOut").val('');
                     self.arrivalNotificationModel().VoyageOut('');
                     $('#VoyageOut').focus();
                 }
                 else {
                     $('#TerminalOperator').focus();
                 }
             });
        }
        //Arrival Notification ETA Date Change
        ChangeETADate = function (ArrivalNotificationData) {

            if ($("#ETA").val() == "" || $("#ETA").val() == null) {
                $("#ETD").val('');
                self.arrivalNotificationModel().ETD('');
                $("#spanEtaValid").text('* Please specify ETA Date');
            }
            else {
                var dateobj = kendo.parseDate(self.arrivalNotificationModel().ETA(), "yyyy-MM-dd HH:mm");
                var datestring = kendo.toString(dateobj, "yyyy-MM-dd HH:mm");
                self.arrivalNotificationModel().ETA(datestring);
                if (self.arrivalNotificationModel().VesselID() > 0) {
                    self.viewModelHelper.apiPost('api/ArrivalDuplicateData', ko.mapping.toJSON(self.arrivalNotificationModel()), function Message(data) {
                        var ValidRule = data.split('@');
                        if (ValidRule[0] == 1) {
                            $("#ETA").val('');
                            self.arrivalNotificationModel().ETA('');
                            toastr.warning("Vessel is on the Berth with" + ValidRule[1]);
                        }
                        else if (ValidRule[0] == 2) {
                            $("#ETA").val('');
                            self.arrivalNotificationModel().ETA('');
                            toastr.warning("Arrival Notification is already raised on " + ValidRule[1]);
                        }
                        else if (ValidRule[0] == 3) {
                            $("#ETA").val('');
                            self.arrivalNotificationModel().ETA('');
                            toastr.warning("" + ValidRule[1] + " is already Rejected with same VoyageIn/VoyageOut values for this Vessel");
                        }
                        else {
                            $("#spanEtaValid").text('');
                            $("#ETD").val('');
                            self.arrivalNotificationModel().ETD('');
                            var StartDateValue = $("#ETA").val();
                            var myDatePicker = kendo.parseDate(self.arrivalNotificationModel().ETA(), "yyyy-MM-dd HH:mm");
                            var day = myDatePicker.getDate();
                            var month = myDatePicker.getMonth();
                            var year = myDatePicker.getFullYear();
                            var Hour = myDatePicker.getHours() + 1;
                            var Mnt = myDatePicker.getMinutes();
                            $("#ETD").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                            var dateobj = kendo.parseDate(self.arrivalNotificationModel().ETA(), "yyyy-MM-dd HH:mm");
                            var datestring = kendo.toString(dateobj, "yyyy-MM-dd HH:mm");
                            self.arrivalNotificationModel().ETA(datestring);
                            self.viewModelHelper.apiPost('api/TimeRuleConfigData', ko.mapping.toJSON(self.arrivalNotificationModel()),
                                                              function Message(data) {
                                                                  var ValidRule = data.split('@');
                                                                  if (ValidRule[0] == 2) {
                                                                      toastr.warning("Arrival Notification is below " + ValidRule[1] + " Hours.");
                                                                      self.viewModelHelper.isLoading(false);

                                                                  }
                                                              },
                                                          function failure(result) {
                                                              self.viewModelHelper.isLoading(false);
                                                              toastr.error(result.responseText);
                                                          },
                                                          function callbackloder(result) {
                                                              self.viewModelHelper.isLoading(false);
                                                          }
                                                         );
                        }
                    });

                }
                else {

                    $("#spanEtaValid").text('');
                    $("#ETD").val('');
                    self.arrivalNotificationModel().ETD('');
                    var StartDateValue = $("#ETA").val();
                    var myDatePicker = kendo.parseDate(self.arrivalNotificationModel().ETA(), "yyyy-MM-dd HH:mm");
                    var day = myDatePicker.getDate();
                    var month = myDatePicker.getMonth();
                    var year = myDatePicker.getFullYear();
                    var Hour = myDatePicker.getHours() + 1;
                    var Mnt = myDatePicker.getMinutes();
                    $("#ETD").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                    var dateobj = kendo.parseDate(self.arrivalNotificationModel().ETA(), "yyyy-MM-dd HH:mm");
                    var datestring = kendo.toString(dateobj, "yyyy-MM-dd HH:mm");
                    self.arrivalNotificationModel().ETA(datestring);
                    self.viewModelHelper.apiPost('api/TimeRuleConfigData', ko.mapping.toJSON(self.arrivalNotificationModel()),
                                                      function Message(data) {
                                                          var ValidRule = data.split('@');
                                                          if (ValidRule[0] == 2) {
                                                              toastr.warning("Arrival Notification is below " + ValidRule[1] + " Hours.");
                                                              self.viewModelHelper.isLoading(false);
                                                          }
                                                      },
                                                  function failure(result) {
                                                      self.viewModelHelper.isLoading(false);
                                                      toastr.error(result.responseText);
                                                  },
                                                  function callbackloder(result) {
                                                      self.viewModelHelper.isLoading(false);
                                                  }
                                                 );

                }
            }
            $("#PlanDateTimeOfBerth").val('');
            self.arrivalNotificationModel().PlanDateTimeOfBerth('');
            $("#PlanDateTimeToVacateBerth").val('');
            self.arrivalNotificationModel().PlanDateTimeToVacateBerth('');
            $("#PlanDateTimeToStartCargo").val('');
            self.arrivalNotificationModel().PlanDateTimeToStartCargo('');
            $("#PlanDateTimeToCompleteCargo").val('');
            self.arrivalNotificationModel().PlanDateTimeToCompleteCargo('');
            $("#LoadDischargeDate").val('');
            self.arrivalNotificationModel().LoadDischargeDate('');
            $("#DischargeDate").val('');
            self.arrivalNotificationModel().DischargeDate('');
            $("#PlannedDurationDate").val('');
            self.arrivalNotificationModel().PlannedDurationDate('');
            $("#PlannedDurationToDate").val('');
            self.arrivalNotificationModel().PlannedDurationToDate('');
            $("#Daycnt").val('');
            self.arrivalNotificationModel().Daycnt('');

            $("#DateLastWasteDelivered").val('');
            self.arrivalNotificationModel().DateLastWasteDelivered('');
        }

        //Arrival Notification ETD Date Change
        ChangeETDDate = function () {

            if (($("#ETA").val() == "" || $("#ETA").val() == null) && ($("#ETD").val() != "" || $("#ETD").val() != null)) {
                $("#spanEtaValid").text('* Please specify ETA Date');
                $("#ETD").val('');
                self.arrivalNotificationModel().ETD('');
            }
            else {
                $("#spanEtdValid").text('');
            }
            $("#PlanDateTimeOfBerth").val('');
            self.arrivalNotificationModel().PlanDateTimeOfBerth('');
            $("#PlanDateTimeToVacateBerth").val('');
            self.arrivalNotificationModel().PlanDateTimeToVacateBerth('');
            $("#PlanDateTimeToStartCargo").val('');
            self.arrivalNotificationModel().PlanDateTimeToStartCargo('');
            $("#PlanDateTimeToCompleteCargo").val('');
            self.arrivalNotificationModel().PlanDateTimeToCompleteCargo('');
            $("#LoadDischargeDate").val('');
            self.arrivalNotificationModel().LoadDischargeDate('');
            $("#DischargeDate").val('');
            self.arrivalNotificationModel().DischargeDate('');
            $("#PlannedDurationDate").val('');
            self.arrivalNotificationModel().PlannedDurationDate('');
            $("#PlannedDurationToDate").val('');
            self.arrivalNotificationModel().PlannedDurationToDate('');
            $("#Daycnt").val('');
            self.arrivalNotificationModel().Daycnt('');

        }

        //ArrivalNotificationData saveing data in API Service 
        self.SaveArrivalNotification = function (ArrivalNotificationData) {
            toastr.clear();

            ArrivalNotificationData.AnyDangerousGoodsonBoard($('input:radio[name=IsDangerousGoodsonBoard]:checked').val());

            var dateobj = kendo.parseDate(ArrivalNotificationData.ETA(), "yyyy-MM-dd HH:mm");
            var datestring = kendo.toString(dateobj, "yyyy-MM-dd HH:mm");
            ArrivalNotificationData.ETA(datestring);

            var dateobjETD = kendo.parseDate(ArrivalNotificationData.ETD(), "yyyy-MM-dd HH:mm");
            var datestringETD = kendo.toString(dateobjETD, "yyyy-MM-dd HH:mm");
            ArrivalNotificationData.ETD(datestringETD);

            var objPlanDateTimeOfBerth = kendo.parseDate(ArrivalNotificationData.PlanDateTimeOfBerth(), "yyyy-MM-dd HH:mm");
            var objstrPlanDateTimeOfBerth = kendo.toString(objPlanDateTimeOfBerth, "yyyy-MM-dd HH:mm");
            ArrivalNotificationData.PlanDateTimeOfBerth(objstrPlanDateTimeOfBerth);

            var objPlanDateTimeToVacateBerth = kendo.parseDate(ArrivalNotificationData.PlanDateTimeToVacateBerth(), "yyyy-MM-dd HH:mm");
            var objstrPlanDateTimeToVacateBerth = kendo.toString(objPlanDateTimeToVacateBerth, "yyyy-MM-dd HH:mm");
            ArrivalNotificationData.PlanDateTimeToVacateBerth(objstrPlanDateTimeToVacateBerth);

            var objPlanDateTimeToStartCargo = kendo.parseDate(ArrivalNotificationData.PlanDateTimeToStartCargo(), "yyyy-MM-dd HH:mm");
            var objstrPlanDateTimeToStartCargo = kendo.toString(objPlanDateTimeToStartCargo, "yyyy-MM-dd HH:mm");
            ArrivalNotificationData.PlanDateTimeToStartCargo(objstrPlanDateTimeToStartCargo);

            var objPlanDateTimeToCompleteCargo = kendo.parseDate(ArrivalNotificationData.PlanDateTimeToCompleteCargo(), "yyyy-MM-dd HH:mm");
            var objstrPlanDateTimeToCompleteCargo = kendo.toString(objPlanDateTimeToCompleteCargo, "yyyy-MM-dd HH:mm");
            ArrivalNotificationData.PlanDateTimeToCompleteCargo(objstrPlanDateTimeToCompleteCargo);

            var objAppliedDate = kendo.parseDate(ArrivalNotificationData.AppliedDate(), "yyyy-MM-dd");
            var objstrAppliedDate = kendo.toString(objAppliedDate, "yyyy-MM-dd");
            ArrivalNotificationData.AppliedDate(objstrAppliedDate);

            var objLoadDischargeDate = kendo.parseDate(ArrivalNotificationData.LoadDischargeDate(), "yyyy-MM-dd");
            var objstrLoadDischargeDate = kendo.toString(objLoadDischargeDate, "yyyy-MM-dd");
            ArrivalNotificationData.LoadDischargeDate(objstrLoadDischargeDate);

            var objDischargeDate = kendo.parseDate(ArrivalNotificationData.DischargeDate(), "yyyy-MM-dd");
            var objstrDischargeDate = kendo.toString(objDischargeDate, "yyyy-MM-dd");
            ArrivalNotificationData.DischargeDate(objstrDischargeDate);

            var objPlannedDurationDate = kendo.parseDate(ArrivalNotificationData.PlannedDurationDate(), "yyyy-MM-dd");
            var objstrPlannedDurationDate = kendo.toString(objPlannedDurationDate, "yyyy-MM-dd");
            ArrivalNotificationData.PlannedDurationDate(objstrPlannedDurationDate);

            var objPlannedDurationToDate = kendo.parseDate(ArrivalNotificationData.PlannedDurationToDate(), "yyyy-MM-dd");
            var objstrPlannedDurationToDate = kendo.toString(objPlannedDurationToDate, "yyyy-MM-dd");
            ArrivalNotificationData.PlannedDurationToDate(objstrPlannedDurationToDate);




            var objDateLastWasteDelivered = kendo.parseDate(ArrivalNotificationData.DateLastWasteDelivered(), "yyyy-MM-dd HH:mm");
            var objstrDateLastWasteDelivered = kendo.toString(objDateLastWasteDelivered, "yyyy-MM-dd HH:mm");
            ArrivalNotificationData.DateLastWasteDelivered(objstrDateLastWasteDelivered);

            var docchkif = ValiedDocumentCheck(self, ArrivalNotificationData);

            if (docchkif != "") {

                $.confirm({
                    'title': 'Arrival Notification',
                    'message': 'Do you want to Add new Arrival Notification ? without uploading of ' + docchkif,
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {
                                if (firstSave) {
                                    firstSave = false;
                                    if (ArrivalNotificationData.VCN() == "" || (ArrivalNotificationData.VCN() != "" && ArrivalNotificationData.VCN() == ArrivalNotificationData.DraftKey())) {

                                        self.viewModelHelper.apiPost('api/TimeRuleConfigData', ko.mapping.toJSON(ArrivalNotificationData),
                                            function Message(data) {
                                                var ValidRule = data.split('@');
                                                if (ValidRule[0] == 1) {
                                                    toastr.error("Arrival Notification does not accept below " + ValidRule[1] + " Hours.");
                                                    self.viewModelHelper.isLoading(false);
                                                    firstSave = true;
                                                }
                                                else {
                                                    var vesselid = ArrivalNotificationData.VesselID();
                                                    var voyagein = ArrivalNotificationData.VoyageIn();
                                                    var voyageout = ArrivalNotificationData.VoyageOut();
                                                    self.viewModelHelper.apiGet('api/ArrivalNotificationVoyageValidation/' + vesselid + '/' + voyagein + '/' + voyageout,
                                                        {},
                                                     function Message(data) {
                                                         var ValidRule = data.split('@');
                                                         if (ValidRule[0] == 1) {
                                                             toastr.warning("" + ValidRule[1] + " is already rejected with same Voyage In/Voyage Out values for this Vessel ");
                                                             self.viewModelHelper.isLoading(false);
                                                             firstSave = true;
                                                         }
                                                         else {
                                                             ArrivalNotificationData.GRT(ArrivalNotificationData.VesselData().GrossRegisteredTonnageInMT);
                                                             self.viewModelHelper.apiPost('api/ArrivalNotifications', ko.mapping.toJSON(ArrivalNotificationData),
                                                                 function Message(data) {
                                                                     firstSave = true;
                                                                     self.viewModelHelper.isLoading(false);
                                                                     toastr.success("Arrival notification details saved successfully with " + data.VCN + ".", "Arrival Notification");
                                                                     self.CancelArrivalNotification(ArrivalNotificationData);
                                                                     self.LoadArrivalNotifications();
                                                                     self.viewMode('List');
                                                                 },
                                                   function failure(result) {
                                                       firstSave = true;
                                                       self.viewModelHelper.isLoading(false);
                                                       toastr.error(result.responseText);
                                                   },
                                                   function callbackloder(result) {
                                                       firstSave = true;
                                                       self.viewModelHelper.isLoading(false);
                                                   });
                                                         }
                                                     });


                                                }
                                            },
                                        function failure(result) {
                                            firstSave = true;
                                            self.viewModelHelper.isLoading(false);
                                            toastr.error(result.responseText);
                                        },
                                        function callbackloder(result) {
                                            firstSave = true;
                                        }
                                        );
                                    }



                                    else {

                                        self.viewModelHelper.apiPost('api/TimeRuleConfigData', ko.mapping.toJSON(ArrivalNotificationData),
                                            function Message(data) {

                                                var ValidRule = data.split('@');
                                                if (ValidRule[0] == 1) {
                                                    toastr.error("Arrival Notification does not accept below " + ValidRule[1] + " Hours.");
                                                    self.viewModelHelper.isLoading(false);
                                                    firstSave = true;
                                                }
                                                else {
                                                    var vesselid = ArrivalNotificationData.VesselID();
                                                    var voyagein = ArrivalNotificationData.VoyageIn();
                                                    var voyageout = ArrivalNotificationData.VoyageOut();
                                                    self.viewModelHelper.apiGet('api/ArrivalNotificationVoyageValidation/' + vesselid + '/' + voyagein + '/' + voyageout,
                                                        {},
                                                     function Message(data) {
                                                         var ValidRule = data.split('@');
                                                         if (ValidRule[0] == 1) {
                                                             toastr.warning("" + ValidRule[1] + " is already rejected with same Voyage In/Voyage Out values for this Vessel ");
                                                             self.viewModelHelper.isLoading(false);
                                                             firstSave = true;
                                                         }
                                                         else {
                                                             self.viewModelHelper.isLoading(true);
                                                             ArrivalNotificationData.GRT(ArrivalNotificationData.VesselData().GrossRegisteredTonnageInMT());
                                                             self.viewModelHelper.apiPut('api/ArrivalNotifications', ko.mapping.toJSON(ArrivalNotificationData),
                                                                 function Message(data) {
                                                                     firstSave = true;
                                                                     toastr.success("Arrival notification details updated successfully for " + data.VCN + ".", "Arrival Notification");
                                                                     self.CancelArrivalNotification(ArrivalNotificationData);
                                                                     self.LoadArrivalNotifications();
                                                                     self.viewMode('List');
                                                                     self.viewModelHelper.isLoading(false);
                                                                 },
                                                             function failure(result) {
                                                                 firstSave = true;
                                                                 self.viewModelHelper.isLoading(false);
                                                                 toastr.error(result.responseText);
                                                             },
                                                             function callbackloder(result) {
                                                                 firstSave = true;
                                                             });
                                                         }
                                                     });
                                                }

                                            },
                                        function failure(result) {
                                            firstSave = true;
                                            self.viewModelHelper.isLoading(false);
                                            toastr.error(result.responseText);
                                        },
                                        function callbackloder(result) {
                                            firstSave = true;

                                        }
                                       );
                                    }
                                }
                            }
                        },
                        'No': {
                            'class': 'gray',
                            'action': function () {
                                firstSave = true;
                                self.viewModelHelper.isLoading(false);
                            }
                        }
                    }
                });
            }
            else {
                if (firstSave) {
                    firstSave = false;
                    if (ArrivalNotificationData.VCN() == "" || (ArrivalNotificationData.VCN() != "" && ArrivalNotificationData.VCN() == ArrivalNotificationData.DraftKey())) {
                        self.viewModelHelper.apiPost('api/TimeRuleConfigData', ko.mapping.toJSON(ArrivalNotificationData),
                            function Message(data) {
                                var ValidRule = data.split('@');
                                if (ValidRule[0] == 1) {
                                    toastr.error("Arrival Notification does not accept below " + ValidRule[1] + " Hours.");
                                    self.viewModelHelper.isLoading(false);
                                    firstSave = true;
                                }
                                else {
                                    var vesselid = ArrivalNotificationData.VesselID();
                                    var voyagein = ArrivalNotificationData.VoyageIn();
                                    var voyageout = ArrivalNotificationData.VoyageOut();
                                    self.viewModelHelper.apiGet('api/ArrivalNotificationVoyageValidation/' + vesselid + '/' + voyagein + '/' + voyageout,
                                        {},
                                     function Message(data) {
                                         var ValidRule = data.split('@');
                                         if (ValidRule[0] == 1) {
                                             toastr.warning("" + ValidRule[1] + " is already rejected with same Voyage In/Voyage Out values for this Vessel ");
                                             self.viewModelHelper.isLoading(false);
                                             firstSave = true;
                                         }
                                         else {
                                             ArrivalNotificationData.GRT(ArrivalNotificationData.VesselData().GrossRegisteredTonnageInMT);
                                             self.viewModelHelper.apiPost('api/ArrivalNotifications', ko.mapping.toJSON(ArrivalNotificationData),
                                                 function Message(data) {
                                                     firstSave = true;
                                                     self.viewModelHelper.isLoading(false);
                                                     toastr.success("Arrival notification details saved successfully with " + data.VCN + ".", "Arrival Notification");
                                                     self.CancelArrivalNotification(ArrivalNotificationData);
                                                     self.LoadArrivalNotifications();
                                                     self.viewMode('List');
                                                 },
                                             function failure(result) {
                                                 firstSave = true;
                                                 self.viewModelHelper.isLoading(false);
                                                 toastr.error(result.responseText);
                                             },
                                             function callbackloder(result) {
                                                 firstSave = true;
                                             });
                                         }
                                     });

                                }

                            },
                        function failure(result) {
                            firstSave = true;
                            self.viewModelHelper.isLoading(false);
                            toastr.error(result.responseText);
                        },
                        function callbackloder(result) {
                            firstSave = true;
                        }
                        );
                    }
                    else {

                        self.viewModelHelper.apiPost('api/TimeRuleConfigData', ko.mapping.toJSON(ArrivalNotificationData),
                            function Message(data) {
                                var ValidRule = data.split('@');
                                if (ValidRule[0] == 1) {
                                    toastr.error("Arrival Notification does not accept below " + ValidRule[1] + " Hours.");
                                    self.viewModelHelper.isLoading(false);
                                    firstSave = true;
                                }
                                else {
                                    var vesselid = ArrivalNotificationData.VesselID();
                                    var voyagein = ArrivalNotificationData.VoyageIn();
                                    var voyageout = ArrivalNotificationData.VoyageOut();
                                    self.viewModelHelper.apiGet('api/ArrivalNotificationVoyageValidation/' + vesselid + '/' + voyagein + '/' + voyageout,
                                        {},
                                     function Message(data) {
                                         var ValidRule = data.split('@');
                                         if (ValidRule[0] == 1) {
                                             toastr.warning("" + ValidRule[1] + " is already rejected with same Voyage In/Voyage Out values for this Vessel ");
                                             self.viewModelHelper.isLoading(false);
                                             firstSave = true;
                                         }
                                         else {
                                             ArrivalNotificationData.GRT(ArrivalNotificationData.VesselData().GrossRegisteredTonnageInMT());
                                             self.viewModelHelper.apiPut('api/ArrivalNotifications', ko.mapping.toJSON(ArrivalNotificationData),
                                                 function Message(data) {
                                                     firstSave = true;
                                                     self.viewModelHelper.isLoading(false);
                                                     toastr.success("Arrival notification details updated successfully for " + data.VCN + ".", "Arrival Notification");
                                                     self.CancelArrivalNotification(ArrivalNotificationData);
                                                     self.LoadArrivalNotifications();
                                                     self.viewMode('List');
                                                 },
                                             function failure(result) {
                                                 firstSave = true;
                                                 self.viewModelHelper.isLoading(false);
                                                 toastr.error(result.responseText);
                                             },
                                             function callbackloder(result) {
                                                 firstSave = true;

                                             });
                                         }
                                     });
                                }

                            },
                        function failure(result) {
                            firstSave = true;
                            self.viewModelHelper.isLoading(false);
                            toastr.error(result.responseText);
                        },
                        function callbackloder(result) {
                            firstSave = true;

                        }
                       );
                    }
                }
            }
        }

        //ArrivalNotification  in Add mode
        self.addArrivalNotification = function () {
            firstSave = true;
            self.IsAddMode(true);
            self.isReset(true);
            self.isVslEnabled(true);

            self.isTOEnabled(false);
            self.isArvValEnable(true);
            self.isspanVslValid(false);


            self.isArrvalEnable(true);
            self.isVisitReasonEnabled(true);
            self.isPHOEnable(true);
            self.isISPSEnable(true);
            self.isISPSDTLEnable(false);
            self.isIMDGEnable(true);
            self.isIMDGEnablechk(true);
            self.isWasteDeclEnablechk(true);
            self.isWasteDeclationEnable(true);


            self.isDrftCmbEnabled(true);
            self.isOtherRsonVisible(false);
            self.arrivalNotificationModel(new IPMSROOT.ArrivalNotificationModel(undefined, self.arrivalNotificationReferenceData()));

            self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: "0" },
                function (result1) {
                    self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
                }, null, null, false);

            self.viewModelHelper.apiGet('api/LoadDrafts', null,
                function (result1) {
                    self.arrivalNotificationReferenceDraftData(new IPMSRoot.ArrivalNotificationReferenceDraftData(result1));
                }, null, null, false);

            self.viewMode('Form');
            self.viewModeForTabs('notification1');
            self.isSubmitVisible(false);

            if (self.arrivalNotificationReferenceData().UserDetails().length > 0) {
                self.arrivalNotificationModel().UserName(self.arrivalNotificationReferenceData().UserDetails()[0].UserName());
                self.arrivalNotificationModel().ContactNo(self.arrivalNotificationReferenceData().UserDetails()[0].ContactNo());
                self.arrivalNotificationModel().ArrivalCreatedAgent(self.arrivalNotificationReferenceData().UserDetails()[0].ArrivalCreatedAgent());
            }
            self.isSaveDraftVisible(true);
            self.isViewMode(false);
            self.isSaveVisible(true);
            self.isUpdateVisible(false);
            self.isGoBackVisible(false);
            self.isGoNextVisible(false);
            self.BunkersVisible(false);
            self.arrivalNotificationModel().bunkersVisible(false);
            self.LayByVisble(false);
            self.arrivalNotificationModel().layByVisble(false);
            self.isDraftVisible(true);
            self.isspanEtaValid(false);

            self.isspanOptValid1(false);
            self.isspanOptValid2(false);
            self.isspanOptValid3(false);
            self.isspanOptValid4(false);

            self.SpecialNetureChanged(false);
            self.isspanSpecifyReason(false);
            self.isspanloadValid(false);

            self.isspanEtdValid(false);
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            $("#IspsClearenceSpn").hide();
            $("#IsTOStrSpn").hide();
            ko.validation.group(self.arrivalNotificationModel()).showAllMessages(false);
            self.isClearenceEnable(false);

            $("#ExemptionSpn").hide();
            $("#AppliedDate").attr("disable", true);
            $("#ISPSReferenceNo").prop('disabled', true);
            $("#AppliedDate").data('kendoDatePicker').enable(false);
            $("#DaylightSpecifyReason").attr("disable", true);
            $("#DaylightSpecifyReason").prop('disabled', true);
            self.arrivalNotificationModel().EnableDisableAddNew(true);
            self.arrivalNotificationModel().EnableDisableAddNewIMDG(true);
            self.isExemptionEnable(false);

        }


        self.editArrivalNotificationNew = function (arrivalNotification) {
            self.viewModelHelper.apiGet('api/ArrivalNotification/GetArrivalNotificationNew',
             { vcn: arrivalNotification.VCN(), WorkflowInstanceId: 0 },
              function (result) {
                  self.arrivalNotificationList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.ArrivalNotificationModel(item, self.arrivalNotificationReferenceData());

                  }));
                  self.editArrivalNotification(self.arrivalNotificationList()[0]);
                  preferedBerthDraft = 0;

                  ko.utils.arrayForEach(self.arrivalNotificationReferenceBirthData().Berths(), function (DocChk) {
                      if (DocChk.BerthKey() === self.arrivalNotificationList()[0].PreferedBerthKey()) {
                          preferedBerthDraft = DocChk.Draftm();
                          return;
                      }
                  });


              });
        }


        //ArrivalNotification in  Edit mode  BBBB
        self.editArrivalNotification = function (arrivalNotification) {

            $("#CellNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            self.IsAddMode(false);
            $("#ISPSReferenceNo").prop('disabled', true);
            $("#AppliedDate").data('kendoDatePicker').enable(false);
            self.isReset(true);
            self.isVslEnabled(false);

            self.isArvValEnable(true);
            self.isDrftCmbEnabled(false);
            self.isspanVslValid(false);
            self.viewModeForTabs('notification1');
            firstSave = true;

            self.isDraftVisible(false);

            self.LayByVisble(false);
            self.BunkersVisible(false);
            self.arrivalNotificationModel().bunkersVisible(false);
            self.arrivalNotificationModel().layByVisble(false);
            self.isOtherRsonVisible(false);

            for (var i = 0; i < arrivalNotification.ArrivaReasonArray().length; i++) {
                arrivalNotification.ReasonForVisit(arrivalNotification.ArrivaReasonArray()[i]);
                if (arrivalNotification.ReasonForVisit() == 'DRYD') {
                    self.DryDockDetailsVisible(true);
                }
                if (arrivalNotification.ReasonForVisit() == 'LABY' || arrivalNotification.ReasonForVisit() == 'REPA') {
                    self.LayByVisble(true);
                    self.arrivalNotificationModel().layByVisble(true);
                }
                else if (arrivalNotification.ReasonForVisit() == 'BUNK') {

                    self.BunkersVisible(true);
                    self.arrivalNotificationModel().bunkersVisible(true);
                }
                else if (arrivalNotification.ReasonForVisit() == 'OTHR') {
                    self.isOtherRsonVisible(true);
                }
            }


            if (arrivalNotification.TerminalOperatorID() > 0) {

                var autocomplete = $("#SpecTO").data("kendoAutoComplete");
                autocomplete.suggest(arrivalNotification.RegisteredName());

                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: arrivalNotification.TerminalOperatorID() },
            function (result1) {
                self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
            }, null, null, false);
            }
            else {

                $("#SpecTO").val('');

                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: "0" },
               function (result1) {
                   self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
               }, null, null, false);

            }

            self.viewMode("Form");

            self.isSaveDraftVisible(false);
            self.isViewMode(false);
            self.isSaveVisible(false);
            self.isUpdateVisible(true);
            self.isGoBackVisible(false);
            self.isGoNextVisible(false);

            self.isSubmitVisible(false);

            self.isspanEtaValid(false);
            self.isspanOptValid1(false);
            self.isspanOptValid2(false);
            self.isspanOptValid3(false);
            self.isspanOptValid4(false);

            self.isspanloadValid(false);
            self.isspanEtdValid(false);
            self.arrivalNotificationModel().ViewModeForTabs('notification1');
            if (arrivalNotification.TerminalOperatorID() > 0) {
                var autocomplete = $("#SpecTO").data("kendoAutoComplete");
                $("#SpecTO").text(arrivalNotification.RegisteredName());
            }
            if (arrivalNotification.PilotExemptionChecked()) {
                $("#ExemptionSpn").show();
                self.isExemptionEnable(true);
            }
            else {
                $("#ExemptionSpn").hide();
                self.isExemptionEnable(false);
            }

            if (arrivalNotification.IsTerminalOperator() == 'A') {
                $("#IsTOStrSpn").show();
                self.isTOEnabled(true);
            }
            else {
                $("#IsTOStrSpn").hide();
                self.isTOEnabled(false);
            }

            var ReferenceID = arrivalNotification.VCN();
            self.arrivalNotificationModel(arrivalNotification);
            self.arrivalNotificationModel().pendingTasks.removeAll();

            var WorkflowInstanceID = 0;
            self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                   null,
                         function (result) {
                             ko.utils.arrayForEach(result, function (val) {
                                 var pendingtaskaction = new IPMSROOT.pendingTask();
                                 if (WorkflowInstanceId == val.WorkflowInstanceId) {
                                     pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                     pendingtaskaction.EntityCode(val.EntityCode);
                                     pendingtaskaction.ReferenceID(val.ReferenceID);
                                     pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                     pendingtaskaction.APIUrl(val.APIUrl);
                                     pendingtaskaction.TaskName(val.RequestName + ' ' + val.TaskName);
                                     pendingtaskaction.TaskDescription(val.TaskDescription);
                                     pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                     pendingtaskaction.HasRemarks(val.HasRemarks);
                                     self.arrivalNotificationModel().pendingTasks.push(pendingtaskaction);
                                 }
                             });
                         });

            var index = 0;
            HandleProgressBarAndActiveTab(index);

            if (self.arrivalNotificationModel().AnyDangerousGoodsonBoard() == "A") {
                self.IsDangerousGoods(true);
                self.isIMDGEnablechk(true);
                $("#rdYesDangerousGoods").attr('checked', 'checked');
            }
            else {
                self.IsDangerousGoods(false);
                self.isIMDGEnablechk(true);
                $("#rdNoDangerousGoods").attr('checked', 'checked');
            }

            if (self.arrivalNotificationModel().WasteDeclaration() == "A") {
                self.WasteDeclarationVisible(true);
                self.isWasteDeclEnablechk(true);
                $("#rdYesWasteDeclaration").attr('checked', 'checked');
            }
            else {
                self.WasteDeclarationVisible(false);
                self.isWasteDeclEnablechk(true);
                $("#rdNoWasteDeclaration").attr('checked', 'checked');
            }


            self.isClearenceEnable(false);
            $("#DraftDetailsList").attr("disable", true);

            var StartDateValue = $("#ETA").val();
            var myDatePicker = new Date(StartDateValue);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours() + 1;
            var Mnt = myDatePicker.getMinutes();
            $("#ETD").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
            self.isArrvalEnable(false);
            self.isPHOEnable(false);
            self.isISPSEnable(false);
            self.isISPSDTLEnable(false);
            self.isIMDGEnable(false);
            self.isVisitReasonEnabled(false);
            self.isWasteDeclationEnable(false);
            self.arrivalNotificationModel().EnableDisableAddNew(false);
            self.arrivalNotificationModel().EnableDisableAddNewIMDG(false);

            $("#LoadDischargeDate").attr("disable", true);
            $("#DischargeDate").attr("disable", true);


            $("#DateLastWasteDelivered").attr("disable", true);


            self.DaylightSpecifyReasonChanged(false);
            self.SpecialNetureChanged(false);


            $("#DaylightSpecifyReasontxt").attr("disabled", true);
            $("#DaylightSpecifyReasontxt").prop('disabled', true);
            self.Exeedportlimitenable(false);

            var reasonSelect = $("#reasonforvisit").data("kendoMultiSelect");
            reasonSelect.enable(false);
            $("#ETA").data('kendoDateTimePicker').enable(false);
            $("#ETD").data('kendoDateTimePicker').enable(false);
            $("#PlanDateTimeOfBerth").data('kendoDateTimePicker').enable(false);
            $("#PlanDateTimeToStartCargo").data('kendoDateTimePicker').enable(false);
            $("#PlanDateTimeToCompleteCargo").data('kendoDateTimePicker').enable(false);
            $("#PlanDateTimeToVacateBerth").data('kendoDateTimePicker').enable(false);



            $("#PlannedDurationDate").data('kendoDatePicker').enable(false);
            $("#PlannedDurationToDate").data('kendoDatePicker').enable(false);


            $("#DischargeDate").data('kendoDatePicker').enable(false);
            $("#LoadDischargeDate").data('kendoDatePicker').enable(false);


            $("#DateLastWasteDelivered").data('kendoDateTimePicker').enable(false);
            self.viewModelHelper.apiGet('api/ArrivalNotification/GetNotificationStatus', { vcn: arrivalNotification.VCN() },
               function (result) {

                   ko.utils.arrayMap(result, function (item) {
                       if (item.WEntityCode == 'ARVLNOT' && (item.WEntityStatus == 'NEW' || item.WEntityStatus == 'VRES')) {

                           self.isArrvalEnable(true);
                           self.isVisitReasonEnabled(true);
                           self.isArvValEnable(true);
                           reasonSelect.enable(true);

                           self.arrivalNotificationModel().EnableDisableAddNew(true);



                           if (arrivalNotification.IsSpecialNature() == 'A') {
                               self.SpecialNetureChanged(true);
                               $("#SpecialNeturetxt").attr("disabled", false);
                               $("#SpecialNeturetxt").prop('disabled', false);

                           }


                           if (arrivalNotification.DaylightRestriction() == 'A') {
                               self.DaylightSpecifyReasonChanged(true);
                               $("#DaylightSpecifyReasontxt").attr("disabled", false);
                               $("#DaylightSpecifyReasontxt").prop('disabled', false);

                           }
                           if (arrivalNotification.ExceedPortLimitations() == 'A') {
                               self.Exeedportlimitenable(true);
                           }
                           $("#ETA").data('kendoDateTimePicker').enable(true);
                           $("#ETD").data('kendoDateTimePicker').enable(true);
                           $("#PlanDateTimeOfBerth").data('kendoDateTimePicker').enable(true);
                           $("#PlanDateTimeToStartCargo").data('kendoDateTimePicker').enable(true);
                           $("#PlanDateTimeToCompleteCargo").data('kendoDateTimePicker').enable(true);
                           $("#PlanDateTimeToVacateBerth").data('kendoDateTimePicker').enable(true);
                           $("#PlannedDurationDate").data('kendoDatePicker').enable(true);
                           $("#PlannedDurationToDate").data('kendoDatePicker').enable(true);
                       }
                       if (item.WEntityCode == 'PHAN' && (item.WEntityStatus == 'NEW' || item.WEntityStatus == 'VRES')) {
                           self.isPHOEnable(true);
                       }


                       if (item.WEntityCode == 'ISPSAN' && (item.WEntityStatus == 'NEW' || item.WEntityStatus == 'VRES')) {


                           if (self.arrivalNotificationModel().AppliedForISPS() == 'I') {
                               self.isISPSEnable(true);
                               self.isISPSDTLEnable(false);
                               self.arrivalNotificationModel().ISPSReferenceNo('');
                               self.arrivalNotificationModel().AppliedDate('');
                               $("#IspsClearenceSpn").hide();
                               $("#ISPSReferenceNo").prop('disabled', true);
                               $("#AppliedDate").data('kendoDatePicker').enable(false);


                           }
                           else {
                               if (self.arrivalNotificationModel().VesselData().GrossRegisteredTonnageInMT() > 500) {
                                   self.isISPSEnable(false);
                                   self.isISPSDTLEnable(false);
                                   $("#IspsClearenceSpn").show();
                                   $("#AppliedDate").attr("disable", false);
                                   $("#ISPSReferenceNo").prop('disabled', true);
                                   $("#AppliedDate").data('kendoDatePicker').enable(true);

                               } else {
                                   self.isISPSEnable(true);
                                   self.isISPSDTLEnable(false);
                                   $("#IspsClearenceSpn").hide();
                                   $("#ISPSReferenceNo").prop('disabled', true);
                                   $("#AppliedDate").data('kendoDatePicker').enable(false);
                               }

                           }
                       }
                       if (item.WEntityCode == 'IMDGAN' && (item.WEntityStatus == 'NEW' || item.WEntityStatus == 'VRES')) {
                           self.isIMDGEnable(true);

                           self.arrivalNotificationModel().EnableDisableAddNewIMDG(true);

                           $("#DischargeDate").data('kendoDatePicker').enable(true);
                           $("#LoadDischargeDate").data('kendoDatePicker').enable(true);

                       }
                   })
               });

        }





        //ArrivalNotification View mode

        self.viewArrivalNotificationNew = function (arrivalNotification) {

            self.viewModelHelper.apiGet('api/ArrivalNotification/GetArrivalNotificationNew',
             { vcn: arrivalNotification.VCN(), WorkflowInstanceId: 0 },
              function (result) {
                  self.arrivalNotificationList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.ArrivalNotificationModel(item, self.arrivalNotificationReferenceData());

                  }));
                  self.viewArrivalNotification(self.arrivalNotificationList()[0]);

                  preferedBerthDraft = 0;
                  ko.utils.arrayForEach(self.arrivalNotificationReferenceBirthData().Berths(), function (DocChk) {
                      if (DocChk.BerthKey() === self.arrivalNotificationList()[0].PreferedBerthKey()) {
                          preferedBerthDraft = DocChk.Draftm();
                          return;
                      }
                  });


              });
            self.printView(true);
        }

        self.viewArrivalNotification = function (arrivalNotification) {
            self.IsAddMode(false);
            self.isDraftVisible(false);
            self.isVslEnabled(false);
            self.isVisitReasonEnabled(false);
            self.isArvValEnable(false);
            self.isISPSEnable(false);
            self.isIMDGEnable(false);
            self.isWasteDeclationEnable(false);
            self.isspanVslValid(false);
            self.Exeedportlimitenable(false);
            self.DaylightSpecifyReasonChanged(false);
            self.SpecialNetureChanged(false);


            self.isDrftCmbEnabled(false);
            self.isspanEtaValid(false);
            self.isspanOptValid1(false);
            self.isspanOptValid2(false);
            self.isspanOptValid3(false);
            self.isspanOptValid4(false);
            self.isspanloadValid(false);
            self.isTOEnabled(false);
            self.isspanEtdValid(false);

            var ReferenceID = arrivalNotification.VCN();
            if (arrivalNotification.TerminalOperatorID() > 0) {
                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: arrivalNotification.TerminalOperatorID() },
            function (result1) {
                self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
            }, null, null, false);
            }
            else {
                $("#SpecTO").val('');
                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: "0" },
               function (result1) {
                   self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
               }, null, null, false);

            }

            self.arrivalNotificationModel(arrivalNotification);

            if (arrivalNotification.TerminalOperatorID() > 0) {
                var autocomplete = $("#SpecTO").data("kendoAutoComplete");
                autocomplete.suggest(arrivalNotification.RegisteredName());
            }

            self.arrivalNotificationModel().EnableDisableAddNew(false);
            self.arrivalNotificationModel().EnableDisableAddNewIMDG(false);


            //BugID #4679,4681
            self.arrivalNotificationModel(arrivalNotification);
            if (arrivalNotification.WFCode != 'VRES') {
                self.arrivalNotificationModel().pendingTasks.removeAll();

                var WorkflowInstanceID = 0;

                self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                       null,
                             function (result) {
                                 ko.utils.arrayForEach(result, function (val) {

                                     var pendingtaskaction = new IPMSROOT.pendingTask();
                                     if (WorkflowInstanceId == val.WorkflowInstanceId) {
                                         pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                         pendingtaskaction.EntityCode(val.EntityCode);
                                         pendingtaskaction.ReferenceID(val.ReferenceID);
                                         pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                         pendingtaskaction.APIUrl(val.APIUrl);
                                         pendingtaskaction.TaskName(val.RequestName + ' ' + val.TaskName);
                                         pendingtaskaction.TaskDescription(val.TaskDescription);
                                         pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                         pendingtaskaction.HasRemarks(val.HasRemarks);
                                         self.arrivalNotificationModel().pendingTasks.push(pendingtaskaction);
                                     }
                                 });
                             });
            }

            var index = 0;
            HandleProgressBarAndActiveTab(index);
            self.viewMode("Form");
            self.viewModeForTabs('notification1');
            self.LayByVisble(false);
            self.BunkersVisible(false);
            self.arrivalNotificationModel().bunkersVisible(false);
            self.arrivalNotificationModel().layByVisble(false);
            self.isOtherRsonVisible(false);
            for (var i = 0; i < arrivalNotification.ArrivaReasonArray().length; i++) {
                arrivalNotification.ReasonForVisit(arrivalNotification.ArrivaReasonArray()[i]);
                if (arrivalNotification.ReasonForVisit() == 'DRYD') {
                    self.DryDockDetailsVisible(true);
                }
                if (arrivalNotification.ReasonForVisit() == 'LABY' || arrivalNotification.ReasonForVisit() == 'REPA') {
                    self.LayByVisble(true);
                    self.arrivalNotificationModel().layByVisble(true);
                }
                else if (arrivalNotification.ReasonForVisit() == 'BUNK') {

                    self.BunkersVisible(true);
                    self.arrivalNotificationModel().bunkersVisible(true);
                }
                else if (arrivalNotification.ReasonForVisit() == 'OTHR') {
                    self.isOtherRsonVisible(true);
                }
            }

            if (arrivalNotification.PilotExemptionChecked()) {
                $("#ExemptionSpn").show();
                self.isExemptionEnable(true);
            }
            else {
                $("#ExemptionSpn").hide();
                self.isExemptionEnable(false);
            }

            if (arrivalNotification.IsTerminalOperator() == 'A') {
                $("#IsTOStrSpn").show();
            }
            else {
                $("#IsTOStrSpn").hide();
            }

            $("#exemptionname").attr("disabled", true);

            if (arrivalNotification.AppliedForISPS() == "A") {
                $("#IspsClearenceSpn").show();
                $("#AppliedDate").attr("disable", true);
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(false);
            }
            else {


                $("#IspsClearenceSpn").hide();
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(false);
            }


            self.isSubmitVisible(false);
            self.isSaveDraftVisible(false);
            self.isViewMode(true);
            self.isSaveVisible(false);
            self.isUpdateVisible(false);
            self.isGoBackVisible(false);
            self.isGoNextVisible(true);


            self.isReset(false);
            self.shouldShowDivAV(true);


            if (viewDetail) {
                self.isClearenceEnable(self.RefUserTypeFlag());
            }
            else {
                self.isClearenceEnable(false);
            }

            self.arrivalNotificationModel().ViewModeForTabs('notification1');
            if (self.arrivalNotificationModel().AnyDangerousGoodsonBoard() == "A") {
                self.IsDangerousGoods(true);
                self.isIMDGEnablechk(false); // Added by sandeep
                $("#rdYesDangerousGoods").attr('checked', 'checked');
            }
            else {
                self.IsDangerousGoods(false);
                self.isIMDGEnablechk(false); // Added by sandeep
                $("#rdNoDangerousGoods").attr('checked', 'checked');
            }

            if (self.arrivalNotificationModel().WasteDeclaration() == "A") {
                self.WasteDeclarationVisible(true);
                self.isWasteDeclEnablechk(true);
                $("#rdYesWasteDeclaration").attr('checked', 'checked');
            }
            else {
                self.WasteDeclarationVisible(false);
                self.isWasteDeclEnablechk(true);
                $("#rdNoWasteDeclaration").attr('checked', 'checked');
            }

        };

        //Arrival Draft details 
        DraftDetailsListChange = function () {
            firstSave = true;
            var vcnVal = $("#DraftDetailsList").val();
            if (vcnVal != null && vcnVal != '') {

                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: "0" },
                          function (result1) {
                              self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
                          }, null, null, false);



                self.viewModelHelper.apiGet('api/ArrivalNotificationByVCN/',
                     { vcn: vcnVal },
                      function (result) {
                          self.arrivalNotificationModel(new IPMSRoot.ArrivalNotificationModel(result, self.arrivalNotificationReferenceData()));
                          self.isReset(true);
                          self.isVslEnabled(false);
                          self.isVisitReasonEnabled(true);
                          self.isDrftCmbEnabled(true);
                          self.viewModeForTabs('notification1');
                          self.isspanVslValid(false);


                          if (self.arrivalNotificationModel().TerminalOperatorID() > 0) {
                              var autocomplete = $("#SpecTO").data("kendoAutoComplete");
                              autocomplete.suggest(self.arrivalNotificationModel().RegisteredName());

                              self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: self.arrivalNotificationModel().TerminalOperatorID() },
                                      function (result1) {
                                          self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
                                      }, null, null, false);
                          }




                          self.LayByVisble(false);
                          self.BunkersVisible(false);
                          self.arrivalNotificationModel().bunkersVisible(false);
                          self.arrivalNotificationModel().layByVisble(false);
                          self.isOtherRsonVisible(false);

                          for (var i = 0; i < self.arrivalNotificationModel().ArrivaReasonArray().length; i++) {
                              self.arrivalNotificationModel().ReasonForVisit(self.arrivalNotificationModel().ArrivaReasonArray()[i]);
                              if (self.arrivalNotificationModel().ReasonForVisit() == 'DRYD') {
                                  self.DryDockDetailsVisible(true);
                              }
                              if (self.arrivalNotificationModel().ReasonForVisit() == 'LABY' || self.arrivalNotificationModel().ReasonForVisit() == 'REPA') {
                                  self.LayByVisble(true);
                                  self.arrivalNotificationModel().layByVisble(true);
                              }
                              else if (self.arrivalNotificationModel().ReasonForVisit() == 'BUNK') {

                                  self.BunkersVisible(true);
                                  self.arrivalNotificationModel().bunkersVisible(true);
                              }
                              else if (self.arrivalNotificationModel().ReasonForVisit() == 'OTHR') {
                                  self.isOtherRsonVisible(true);
                              }
                          }


                          self.isSubmitVisible(false);
                          self.isSaveDraftVisible(true);
                          self.isViewMode(false);
                          self.isSaveVisible(false);
                          self.isUpdateVisible(true);
                          self.isGoBackVisible(false);
                          self.isGoNextVisible(false);


                          self.arrivalNotificationModel().ViewModeForTabs('notification1');
                          if (self.arrivalNotificationModel().AppliedForISPS() == 'A') {
                              $("#IspsClearenceSpn").show();
                          }
                          else {
                              $("#IspsClearenceSpn").hide();
                          }

                          if (self.arrivalNotificationModel().PilotExemptionChecked()) {
                              $("#ExemptionSpn").show();
                              self.isExemptionEnable(true);
                          }
                          else {
                              $("#ExemptionSpn").hide();
                              self.isExemptionEnable(false);
                          }

                          var index = 0;
                          HandleProgressBarAndActiveTab(index);

                          if (self.arrivalNotificationModel().AnyDangerousGoodsonBoard() == "A") {
                              self.IsDangerousGoods(true);
                              $("#rdYesDangerousGoods").attr('checked', 'checked');
                          }
                          else {
                              self.IsDangerousGoods(false);
                              $("#rdNoDangerousGoods").attr('checked', 'checked');
                          }

                          if (self.arrivalNotificationModel().WasteDeclaration() == "A") {
                              self.WasteDeclarationVisible(true);
                              $("#rdYesWasteDeclaration").attr('checked', 'checked');
                          }
                          else {
                              self.WasteDeclarationVisible(false);
                              $("#rdNoWasteDeclaration").attr('checked', 'checked');
                          }

                          self.isClearenceEnable(false);


                          if (self.arrivalNotificationModel().AppliedForISPS() == "A") {
                              $("#IspsClearenceSpn").show();
                              $("#AppliedDate").attr("disable", false);
                              $("#ISPSReferenceNo").prop('disabled', true);
                              $("#AppliedDate").data('kendoDatePicker').enable(true);
                          }
                          else {

                              self.arrivalNotificationModel().ISPSReferenceNo('');
                              self.arrivalNotificationModel().AppliedDate('');
                              $("#IspsClearenceSpn").hide();
                              $("#ISPSReferenceNo").prop('disabled', true);
                              $("#AppliedDate").data('kendoDatePicker').enable(false);
                          }


                          if (self.arrivalNotificationModel().IsTerminalOperator() == 'A') {
                              $("#IsTOStrSpn").show();
                              self.isTOEnabled(true);
                          }
                          else {
                              $("#IsTOStrSpn").hide();
                              self.isTOEnabled(false);
                          }


                          self.arrivalNotificationModel().DraftKey(vcnVal);

                          $("#DraftDetailsList").val(vcnVal);
                          var StartDateValue = $("#ETA").val();
                          var myDatePicker = new Date(StartDateValue);
                          var day = myDatePicker.getDate();
                          var month = myDatePicker.getMonth();
                          var year = myDatePicker.getFullYear();
                          var Hour = myDatePicker.getHours() + 1;
                          var Mnt = myDatePicker.getMinutes();
                          $("#ETD").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
                          if (self.arrivalNotificationReferenceData().UserDetails().length > 0) {
                              self.arrivalNotificationModel().UserName(self.arrivalNotificationReferenceData().UserDetails()[0].UserName());
                              self.arrivalNotificationModel().ContactNo(self.arrivalNotificationReferenceData().UserDetails()[0].ContactNo());
                              self.arrivalNotificationModel().ArrivalCreatedAgent(self.arrivalNotificationReferenceData().UserDetails()[0].ArrivalCreatedAgent());
                          }
                      });



            }
            else {
                self.addArrivalNotification();
            }


        }

        //this method is  fires when reset button pressed to reset the data that present in intail stage
        self.ResetArrivalNotification = function (arrivalNotification) {
            firstSave = true;
            self.ismultiplepfileToUpload = ko.observable(false);
            self.isspanVslValid(false);
            self.isspanEtaValid(false);
            self.isspanOptValid1(false);
            self.isspanOptValid2(false);
            self.isspanOptValid3(false);
            self.isspanOptValid4(false);
            self.isspanloadValid(false);
            self.isspanEtdValid(false);
            var notify = self.viewModeForTabs();

            if (arrivalNotification.TerminalOperatorID() > 0) {
                var autocomplete = $("#SpecTO").data("kendoAutoComplete");
                autocomplete.suggest(arrivalNotification.RegisteredName());

                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: arrivalNotification.TerminalOperatorID() },
            function (result1) {
                self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
            }, null, null, false);
            }
            else {
                $("#SpecTO").val('');

                self.viewModelHelper.apiGet('api/LoadTOBirths', { TOID: "0" },
               function (result1) {
                   self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(result1));
               }, null, null, false);

            }

            self.isClearenceEnable(false);


            $('#selUploadDocs').val('');
            self.viewModeForTabs(notify);
            self.arrivalNotificationModel().ViewModeForTabs(notify);
            self.isViewMode(false);

            if (self.IsAddMode()) {
                self.LayByVisble(false);
                self.BunkersVisible(false);
                self.arrivalNotificationModel().bunkersVisible(false);
                self.arrivalNotificationModel().layByVisble(false);
                self.isOtherRsonVisible(false);
                self.isSaveVisible(true);
                self.isUpdateVisible(false);
                $("#txtVessels").val('aa');
                self.arrivalNotificationModel().reset();
                $("#ExemptionSpn").hide();
                $("#IsTOStrSpn").hide();
                $("#txtVessels").val('');
                self.isSaveDraftVisible(true);
                self.arrivalNotificationModel().ArrivalIMDGTankers.removeAll();
                self.arrivalNotificationModel().IMDGInformations.removeAll();
                self.arrivalNotificationModel().ArrivalCommodities.removeAll();
                self.arrivalNotificationModel().ArrivalDocuments.removeAll();
                self.arrivalNotificationModel().WasteDeclarations.removeAll();

                self.arrivalNotificationModel().ISPSReferenceNo('');
                self.arrivalNotificationModel().AppliedDate('');
                $("#IspsClearenceSpn").hide();
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(false);

            }
            else {
                self.arrivalNotificationModel().reset();
                self.editArrivalNotification(arrivalNotification);
            }
            self.viewModeForTabs('notification1');
            var index = 0;
            HandleProgressBarAndActiveTab(index);
            self.isGoNextVisible(false);

        }


        //this method is  fires when Cancel button is pressed and allfields data is cleared  and redirected to List form
        self.CancelArrivalNotification = function (arrivalNotification) {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                $("#txtVessels").val('aa');
                self.arrivalNotificationModel().reset();
                self.arrivalNotificationModel().pendingTasks.removeAll();
                self.isArvValEnable(true);
                self.isISPSEnable(true);
                self.isIMDGEnable(true);
                self.isspanVslValid(false);
                self.isspanEtaValid(false);
                self.isspanOptValid1(false);
                self.isspanOptValid2(false);
                self.isspanOptValid3(false);
                self.isspanOptValid4(false);
                self.isspanSpecifyReason(false);
                self.isspanloadValid(false);
                self.isTOEnabled(false);
                $("#txtVessels").val('');

                self.isArrvalEnable(false);
                self.isVisitReasonEnabled(false);
                self.isPHOEnable(false);
                self.isISPSEnable(false);
                self.isISPSDTLEnable(false);
                self.isIMDGEnable(false);
                self.isWasteDeclationEnable(false);


                self.isspanEtdValid(false);
                self.viewModeForTabs('notification1');
                self.shouldShowDivAV(false);


                self.arrivalNotificationModel().ViewModeForTabs('notification1');
                self.isViewMode(false);
                self.viewMode("List");
                self.isGoNextVisible(false);
            }
        }



        //this method is  fires when tab 1 button is pressed
        self.GotoTab1 = function (arrivalnotificationData) {
            GoToTab1(self, arrivalnotificationData);
        }
        //this method is  fires when tab 2 button is pressed
        self.GotoTab2 = function (arrivalnotificationData) {
            $("#CellNo").kendoMaskedTextBox({ mask: "(000)000-000-0000" });
            self.arrivalNotificationModel().ReasonForVisit('');
            if (ValidateFormValues(self, arrivalnotificationData) == true) {

                if (self.viewModeForTabs() == 'notification3') {
                    self.viewModeForTabs('notification2');
                    self.arrivalNotificationModel().ViewModeForTabs('notification2');
                    GoToTab2(self, arrivalnotificationData);
                }
                else {
                    self.viewModeForTabs('notification2');
                    self.arrivalNotificationModel().ViewModeForTabs('notification2');
                    GoToTab2(self, arrivalnotificationData);
                }
            }
        }
        //this method is  fires when tab 3 button is pressed
        self.GotoTab3 = function (arrivalnotificationData) {
            self.arrivalNotificationModel().ReasonForVisit('');
            if (self.arrivalNotificationModel().ViewModeForTabs() == "notification1") {
                if (ValidateFormValues(self, arrivalnotificationData) == true) {
                    self.isGoBackVisible(true);

                    self.viewModeForTabs('notification2');
                    self.arrivalNotificationModel().ViewModeForTabs('notification2');
                    if (ValidateFormValues(self, arrivalnotificationData) == true) {
                        self.viewModeForTabs('notification3');
                        self.arrivalNotificationModel().ViewModeForTabs('notification3');
                        GoToTab3(self, arrivalnotificationData);

                    }
                    else {
                        GoToTab2(self, arrivalnotificationData);
                    }
                    if (viewDetail == true) {
                        self.isSubmitVisible(false);
                    }
                    else {
                        if (!self.isViewMode())
                            self.isSubmitVisible(true);
                    }
                }
            }
            else {
                if (ValidateFormValues(self, arrivalnotificationData) == true) {

                    if (self.viewModeForTabs() == 'notification3') {
                        self.viewModeForTabs('notification3');
                        self.arrivalNotificationModel().ViewModeForTabs('notification3');
                        GoToTab3(self, arrivalnotificationData);
                    }
                    else {

                        if (GoToTab3(self, arrivalnotificationData)) {
                            self.viewModeForTabs('notification3');
                            self.arrivalNotificationModel().ViewModeForTabs('notification3');
                        }
                    }

                    if (viewDetail == true) {
                        self.isSubmitVisible(false);
                    }
                    else {
                        if (!self.isViewMode())
                            self.isSubmitVisible(true);
                    }
                }
            }

        }
        //this method is  fires when back  button is pressed
        self.GoToPrevTab = function (arrivalnotificationData) {
            if (self.viewModeForTabs() == 'notification3') {
                GoToTab2(self, arrivalnotificationData);
                return;
            }
            else if (self.viewModeForTabs() == 'notification2') {
                GoToTab1(self, arrivalnotificationData);
            }
        }
        self.GoToNextTab = function (arrivalnotificationData) {

            if (self.viewModeForTabs() == 'notification1') {
                GoToTab2(self, arrivalnotificationData);
                return;
            }
            else if (self.viewModeForTabs() == 'notification2') {
                GoToTab3(self, arrivalnotificationData);
            }
            if (viewDetail == true) {
                self.isSubmitVisible(false);
            }
            else {
                self.isSubmitVisible(false);
            }
        }

        var uploadedFiles = [];
        var documentData = [];
        //this method is  fires when upload  button is pressed
        self.uploadFile = function () {
            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } {
                $("#spanHWPSfileToUpload").text("");
                self.ismultiplepfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();

                if (documentType != 'Choose....') {
                    self.arrivalNotificationModel().UploadedFiles([]);

                    uploadedFiles = self.arrivalNotificationModel().UploadedFiles();
                    var opmlFile = $('#fileToUpload')[0];
                    if (opmlFile.files.length > 0) {
                        for (var i = 0; i < opmlFile.files.length; i++) {
                            var match = ko.utils.arrayFirst(self.arrivalNotificationModel().ArrivalDocuments(), function (item) {
                                return item.FileName() === opmlFile.files[i].name;
                            });
                            if (match == null) {
                                var elem = {};
                                elem.FileName = opmlFile.files[i].name;
                                var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                                var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx', 'xls'];
                                if ($.inArray(extension, fileExtension) != -1) {
                                    elem.CategoryName = $('#selUploadDocs option:selected').text();
                                    elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                    elem.FileDetails = opmlFile.files[i];
                                    elem.IsAlreadyExists = false
                                    uploadedFiles.push(elem);
                                    self.arrivalNotificationModel().UploadedFiles(uploadedFiles);
                                } else {
                                    toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
                                    return;
                                }
                            }
                            else {
                                toastr.warning("The selected file already exist, Please upload another file", "Error");
                                return;
                            }
                        }
                        var formData = new FormData();
                        $.each(uploadedFiles, function (key, val) {
                            formData.append(val.name, val.FileDetails);
                        });
                        var CategoryName = $('#selUploadDocs option:selected').text();
                        var CategoryCode = $('#selUploadDocs option:selected').val();


                        self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=' + CategoryCode, formData, function Message(data) {
                            self.Listdocuments = ko.observableArray();

                            self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                                var Adddoc = new IPMSROOT.ArrivalDocument();

                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(CategoryName);
                                Adddoc.DocumentCode(CategoryCode);
                                self.arrivalNotificationModel().ArrivalDocuments.push(Adddoc);
                                $("select#selUploadDocs").prop('selectedIndex', 0);

                            }));
                        });

                    }

                    else {
                        $("#spanmultiplepfileToUpload").text('Please select file');
                        self.ismultiplepfileToUpload(true);
                    }
                }
                else {
                    $("#spanmultiplepfileToUpload1").text('Please select Document Category');
                    self.ismultiplepfileToUpload(true);
                }
                self.arrivalNotificationModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }



        }
        //this method is  fires when delete  button is pressed in uploaded document list
        self.DeleteDocument = function (documentRow) {
            self.arrivalNotificationModel().ArrivalDocuments.remove(documentRow);
        }

        isAdd = 0;
        index = 1;
        //this method is  fires when add  button is pressed in  new row is added in Commodity list
        self.AddNewQualitiesOfCommodity = function (arrivalNotificationData) {

            if (arrivalNotificationData.PreferedBerthKey() == '' || arrivalNotificationData.PreferedBerthKey() == null) {
                toastr.warning("Please Select Prefered Berth Details", "Arrival Notification");

            } else {
                if (arrivalNotificationData.ArrivalCommodities().length > 0) {
                    var ManError = "Y";
                    $.map(arrivalNotificationData.ArrivalCommodities, function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                                if (CommodChk !== undefined) {
                                    if (CommodChk.CommodityBerthKey() == "" || CommodChk.CargoType() == "" || CommodChk.Package() == "" || CommodChk.UOM() == "" || CommodChk.Quantity() == "" || CommodChk.Quantity() == 0) {
                                        toastr.warning("Please Enter Commodity Details", "Arrival Notification");
                                        ManError = "N";
                                    }
                                }
                            });

                    });
                    if (ManError == "Y") {

                        var acomodity = new IPMSROOT.ArrivalCommodity();
                        acomodity.CommodityBerthKey(arrivalNotificationData.PreferedBerthKey());
                        self.arrivalNotificationModel().ArrivalCommodities.push(acomodity);
                    }
                }
                else {
                    var acomodity = new IPMSROOT.ArrivalCommodity();
                    acomodity.CommodityBerthKey(arrivalNotificationData.PreferedBerthKey());
                    self.arrivalNotificationModel().ArrivalCommodities.push(acomodity);
                }
            }
        }



        self.removeArrivalIMDGTankers = function (arrivalNotificationData) {
            self.arrivalNotificationModel().ArrivalIMDGTankers.remove(arrivalNotificationData);
        }



        self.AddIMDGInformations = function (arrivalNotificationData) {
            if (arrivalNotificationData.IMDGInformations().length > 0) {
                var ManError = "Y";
                $.map(arrivalNotificationData.IMDGInformations, function (item) {
                    var IMDGInformationsC = item;
                    if (IMDGInformationsC != null)
                        ko.utils.arrayForEach(IMDGInformationsC, function (CommodChk) {

                            if (CommodChk !== undefined) {
                                if (CommodChk.Others() == null)
                                    CommodChk.Others('');
                                if (CommodChk.Purpose() == "" || CommodChk.ClassCode() == "" || CommodChk.CargoCode() == "" || CommodChk.UNNo().trim() == "") {
                                    toastr.warning("Please Enter IMDG Cargo Information Details", "Arrival Notification");
                                    ManError = "N";
                                }
                            }
                        });

                });
                if (ManError == "Y")
                    self.arrivalNotificationModel().IMDGInformations.push(new IPMSROOT.IMDGContainerInformationdetails());
            }
            else {
                self.arrivalNotificationModel().IMDGInformations.push(new IPMSROOT.IMDGContainerInformationdetails());
            }

        }
        self.removeIMDGInformations = function (arrivalNotificationData) {
            self.arrivalNotificationModel().IMDGInformations.remove(arrivalNotificationData);
        }


        self.AddWasteDeclarations = function (arrivalNotificationData) {
            if (arrivalNotificationData.WasteDeclarations().length > 0) {
                var ManError = "Y";
                $.map(arrivalNotificationData.WasteDeclarations, function (item) {
                    var WasteDeclarationItem = item;
                    if (WasteDeclarationItem != null)
                        ko.utils.arrayForEach(WasteDeclarationItem, function (CommodChk) {

                            if (CommodChk !== undefined) {
                                if (CommodChk.Others() == null)
                                    CommodChk.Others('');
                                if (CommodChk.MarpolCode() == "" || CommodChk.ClassCode() == "" || CommodChk.LicenseRequestID() == "" || CommodChk.Quantity() == "") {
                                    toastr.warning("Please Enter Waste Declaration Information Details", "Arrival Notification");
                                    ManError = "N";
                                }
                            }
                        });

                });
                if (ManError == "Y")
                    self.arrivalNotificationModel().WasteDeclarations.push(new IPMSROOT.WasteDeclarationDetails(undefined, self.arrivalNotificationReferenceData()));
            }
            else {
                self.arrivalNotificationModel().WasteDeclarations.push(new IPMSROOT.WasteDeclarationDetails(undefined, self.arrivalNotificationReferenceData()));
            }
        }

        self.removeWasteDeclarations = function (arrivalNotificationData) {
            self.arrivalNotificationModel().WasteDeclarations.remove(arrivalNotificationData);
        }

        self.removeArrivalCommodities = function (arrivalNotificationData) {
            self.arrivalNotificationModel().ArrivalCommodities.remove(arrivalNotificationData);
        }

        AnyDangerousGoodsClick = function () {
            if ($('input:radio[name=IsDangerousGoodsonBoard]:checked').val() == "A") {
                self.IsDangerousGoods(true);
                $("#rdYesDangerousGoods").attr('checked', 'checked');
                $("#DischargeDate").data('kendoDatePicker').enable(true);
                $("#LoadDischargeDate").data('kendoDatePicker').enable(true);


            }
            else {
                self.IsDangerousGoods(false);
                $("#rdNoDangerousGoods").attr('checked', 'checked');
                self.arrivalNotificationModel().LoadDischargeDate('');
                self.arrivalNotificationModel().DischargeDate('');
                self.arrivalNotificationModel().CellNo('');
                self.arrivalNotificationModel().CargoDescription('');
                self.arrivalNotificationModel().ArrivalIMDGTankers.removeAll();
                self.arrivalNotificationModel().IMDGInformations.removeAll();
                $("#DischargeDate").data('kendoDatePicker').enable(false);
                $("#LoadDischargeDate").data('kendoDatePicker').enable(false);


            }
        }

        AnyWasteDeclarationClick = function () {
            if ($('input:radio[name=WasteDeclaration]:checked').val() == "A") {
                self.WasteDeclarationVisible(true);
                $("#rdYesWasteDeclaration").attr('checked', 'checked');

                $("#DateLastWasteDelivered").data('kendoDateTimePicker').enable(true);


            }
            else {
                self.WasteDeclarationVisible(false);
                $("#rdNoWasteDeclaration").attr('checked', 'checked');
                self.arrivalNotificationModel().LastPortWasteDelivered('');
                self.arrivalNotificationModel().NextPortWasteDelivery('');
                self.arrivalNotificationModel().DateLastWasteDelivered('');
                self.arrivalNotificationModel().WasteDeclarations.removeAll();


                $("#DateLastWasteDelivered").data('kendoDateTimePicker').enable(false);


            }
        }

        //ArrivalNotification Initializetion(pageload) mode
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadInitialData();
            self.viewModeForTabs('notification1');
            self.arrivalNotificationModel().ViewModeForTabs('notification1');

            self.LoadArrivalNotifications();

            if (viewDetail == true) {

            }
            else {
                self.viewMode('List');
            }
        }



        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {

                if (dat.TaskCode() == "VUPD") {
                    self.VUPD(true);
                    $(".close").trigger("click");
                    self.arrivalNotificationModel().WokflowFlag(dat.EntityCode());
                    self.arrivalNotificationModel().workflowRemarks(dat.Remarks());
                    self.SaveArrivalNotification(self.arrivalNotificationModel());
                }
                else
                    action.SubmitAction(dat, self.arrivalNotificationModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }


        //Change of VCN need to reset all the values - Omprakash kotha on 11th November 2014
        $('#txtVessels').live('keydown', function (e) {



            var charCode = e.which || e.keyCode;
            if (charCode == 8 || charCode == 46) {
                self.arrivalNotificationModel().VesselData('');
                self.arrivalNotificationModel().VesselID('');
                self.arrivalNotificationModel().IMONo('');
                self.arrivalNotificationModel().DockingPlanID('');
                self.arrivalNotificationModel().SubmissionDate('');
                $("select#reasonforvisit").prop('selectedIndex', 0);
                self.DryDockDetailsVisible(false);
            }
        });


        //Change of VCN need to reset all the values - Omprakash kotha on 11th November 2014
        $('#SpecTO').live('keydown', function (e) {

            var charCode = e.which || e.keyCode;
            if (charCode == 8 || charCode == 46) {
                $('#SpecTO').focus();
                self.arrivalNotificationModel().TerminalOperatorID('');
                self.arrivalNotificationReferenceBirthData(new IPMSRoot.ArrivalNotificationReferenceBirthData(undefined));

            }
        });


        PlanDateTimeOfBerthCal = function () {
            this.min($("#ETA").val());
            this.max($("#ETD").val());
        }

        PlanDateTimeToStartCargoCal = function () {

            this.min($("#PlanDateTimeOfBerth").val());
            this.max($("#ETD").val());
        }
        PlanDateTimeToCompleteCargoCal = function () {

            this.min($("#PlanDateTimeToStartCargo").val());
            this.max($("#ETD").val());
        }
        PlanDateTimeToVacateBerthCal = function () {

            this.min($("#PlanDateTimeToCompleteCargo").val());
            this.max($("#ETD").val());
        }


        CheckingPilotExemption = function (ctrl) {

            if (ctrl.checked) {
                self.isExemptionEnable(true);
                $("#ExemptionSpn").show();
            }
            else {
                $("select#exemptionname").prop('selectedIndex', 0);
                self.isExemptionEnable(false);
                $("#ExemptionSpn").hide();
            }
        }



        CheckingISPS = function (ctrl) {
            if (ctrl.value == "A") {
                $("#IspsClearenceSpn").show();
                $("#AppliedDate").attr("disable", false);
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(true);
            }
            else {
                self.arrivalNotificationModel().ISPSReferenceNo('');
                self.arrivalNotificationModel().AppliedDate('');
                $("#IspsClearenceSpn").hide();
                $("#ISPSReferenceNo").prop('disabled', true);
                $("#AppliedDate").data('kendoDatePicker').enable(false);
            }
        }


        CheckingDaylightRestriction = function (ctrl) {
            if (ctrl.value == "A") {
                self.DaylightSpecifyReasonChanged(true);
            }
            else {
                self.DaylightSpecifyReasonChanged(false);
                self.arrivalNotificationModel().DaylightSpecifyReason('');
            }

        }

        CheckingSpecialNeture = function (ctrl) {
            if (ctrl.value == "A") {
                self.SpecialNetureChanged(true);
            }
            else {
                self.SpecialNetureChanged(false);
                self.arrivalNotificationModel().SpecialNatureReason('');
            }

        }

        CheckingExceedPortLimitations = function (ctrl) {

            if (ctrl.value == "A") {
                self.Exeedportlimitenable(true);
            }
            else {
                self.Exeedportlimitenable(false);
                self.arrivalNotificationModel().ExceedSpecifyReason('');
            }

        }




        ///cancel status

        self.cancelReqst = function (arrivalNotification) {
            self.viewMode('PopUp');

            arrivalNotification.workflowRemarks("Cancel");
            self.arrivalNotificationModelGrid(arrivalNotification);

            self.printView(false);




        }

        self.ViewcancelReqst = function (arrivalNotification) {

            self.viewMode('PopUpCslRem');
            self.arrivalNotificationModel().CancelRemarks(arrivalNotification.CancelRemarks());
            self.printView(false);
        }


        self.cancelWFView = function (ArrivalNotificationData) {
            $(".close").trigger("click");
            self.viewMode('List');
        }

        self.cancelWF = function (ArrivalNotificationData) {
            $(".close").trigger("click");
            self.LoadArrivalNotifications();
            self.viewMode('List');
        }

        self.cancelWFRequestNew = function (arrivalNotification) {

            var rem = arrivalNotification.workflowRemarks();
            self.viewModelHelper.apiGet('api/ArrivalNotification/GetArrivalNotificationNew',
                       { vcn: arrivalNotification.VCN(), WorkflowInstanceId: 0 },
                        function (result) {
                            self.arrivalNotificationList(ko.utils.arrayMap(result, function (item) {
                                return new IPMSRoot.ArrivalNotificationModel(item, self.arrivalNotificationReferenceData());

                            }));
                            self.arrivalNotificationList()[0].CancelRemarks(rem);
                            self.cancelWFRequest(self.arrivalNotificationList()[0], rem);

                        });

        }


        self.cancelWFRequest = function (ArrivalNotificationData) {

            self.viewModelHelper.apiPost('api/ArrivalNotification/Cancel', ko.mapping.toJSON(ArrivalNotificationData),
                            function Message(data) {
                                toastr.success("Arrival Notification Cancelled Successfully", "ArrivalNotification");
                                $(".close").trigger("click");
                                self.LoadArrivalNotifications();
                                self.viewMode('List');
                            });
        }



        self.BerthChange1 = function (arrivalNotification) {
            if (arrivalNotification.ArrDraft() == '.' || arrivalNotification.ArrDraft() == null || arrivalNotification.ArrDraft() == '' || arrivalNotification.DepDraft() == null || arrivalNotification.DepDraft() == '' || arrivalNotification.DepDraft() == '.') {
                toastr.warning("Please Enter Valid draft details ", "Arrival Notification");
                arrivalNotification.PreferedBerthKey(undefined);
                return;
            }
            else {
                var IsvalidDraft = true;
                var aa = '';
                preferedBerthDraft = 0;
                ko.utils.arrayForEach(self.arrivalNotificationReferenceBirthData().Berths(), function (DocChk) {

                    if (DocChk.BerthKey() === arrivalNotification.PreferedBerthKey()) {
                        preferedBerthDraft = DocChk.Draftm();
                        var aftd = parseFloat(arrivalNotification.ArrDraft()).toFixed(2);
                        var depd = parseFloat(arrivalNotification.DepDraft()).toFixed(2);
                        var actdft = parseFloat(DocChk.Draftm()).toFixed(2);
                        if (compare(actdft, aftd) && compare(actdft, depd)) {
                            aa = "Arr. Draft(m) - " + aftd + " and Dep. Draft(m) - " + depd + " are more than Berth Draft - " + actdft;
                            IsvalidDraft = false;
                        }
                        else if (compare(actdft, aftd)) {
                            aa = "Arr. Draft(m) - " + aftd + " is more than Berth Draft - " + actdft;
                            IsvalidDraft = false;
                        }
                        else if (compare(actdft, depd)) {
                            aa = "Dep. Draft(m) - " + depd + " is more than Berth Draft - " + actdft;
                            IsvalidDraft = false;
                        }
                        return;
                    }

                });

                if (!IsvalidDraft) {
                    toastr.error(aa, "Arrival Notification");
                }
            }
        }

        self.BerthChange11 = function (event) {
            if (self.arrivalNotificationModel().ArrDraft() == '.' || self.arrivalNotificationModel().ArrDraft() == null || self.arrivalNotificationModel().ArrDraft() == '' || self.arrivalNotificationModel().DepDraft() == null || self.arrivalNotificationModel().DepDraft() == '' || self.arrivalNotificationModel().DepDraft() == '.') {
                toastr.warning("Please Enter Valid draft details ", "Arrival Notification");
                event.PreferedBerthKey(undefined);
                return;
            }
            else {
                var DrftValid = false;
                var aa = '';

                ko.utils.arrayForEach(self.arrivalNotificationReferenceBirthData().Berths(), function (DocChk) {

                    if (DocChk.BerthKey() == self.arrivalNotificationModel().PreferedBerthKey()) {

                        var aftd = parseFloat(self.arrivalNotificationModel().ArrDraft()).toFixed(2);
                        var depd = parseFloat(self.arrivalNotificationModel().DepDraft()).toFixed(2);
                        var actdft = parseFloat(DocChk.Draftm()).toFixed(2);
                        var actdft = parseFloat(DocChk.Draftm()).toFixed(2);
                        if (compare(actdft, aftd) && compare(actdft, depd)) {
                            aa = "Arr. Draft(m) - " + aftd + " and Dep. Draft(m) - " + depd + " are more than Berth Draft - " + actdft;
                            DrftValid = true;
                        }
                        else if (compare(actdft, aftd)) {

                            aa = "Arr. Draft(m) - " + aftd + " is more than Berth Draft - " + actdft;
                            DrftValid = true;
                        }
                        else if (compare(actdft, depd)) {
                            aa = "Dep. Draft(m) - " + depd + " is more than Berth Draft - " + actdft;
                            DrftValid = true;
                        }
                        return;
                    }

                });

                if (DrftValid) {
                    toastr.warning(aa, "Arrival Notification");
                }

            }
        }

        self.BerthChange2 = function (event) {
            ko.utils.arrayForEach(self.arrivalNotificationReferenceBirthData().Berths(), function (DocChk) {      
                if (DocChk.BerthKey() == event.CommodityBerthKey()) {
                    var Cargos = DocChk.CargoDetails();

                    var Cargoarr = Cargos.split(',');
                    var alrte = true;
                    for (var i = 0; i < Cargoarr.length; i++) {
                        if (Cargoarr[i] == event.CargoType()) {
                            alrte = false;
                        }
                    }
                    if (alrte) {
                        toastr.warning("Cargo Type is Not specified for selected Berth.", "Arrival Notification");
                    }
                }
            });




        }

        self.ValidateWasteQuantity = function (data, event) {

            if ((event.which != 46 || $("#WasteQuantity").val().indexOf('.') != -1) && (event.which < 48 || event.which > 57))
                return false;

            return true;

        }

        self.Initialize();
    }


    ko.bindingHandlers.kendoAutoComplete.options.filter = "contains";
    IPMSRoot.ArrivalNotificationViewModel = ArrivalNotificationViewModel;
}(window.IPMSROOT));

//ArrivalNotification Initializetion(pageload) mode
function MapArrivalNotificationDetails(arrivalnotificationdata) {

    self.arrivalNotificationModel().VCN(arrivalnotificationdata.VCN);
    self.arrivalNotificationModel().WorkflowInstanceId(arrivalnotificationdata.WorkflowInstanceId);
    self.arrivalNotificationModel().PortCode(arrivalnotificationdata.PortCode);
    self.arrivalNotificationModel().VesselID(arrivalnotificationdata.VesselID);
    self.arrivalNotificationModel().VesselName(arrivalnotificationdata.VesselName);
    self.arrivalNotificationModel().VoyageIn(arrivalnotificationdata.VoyageIn);
    self.arrivalNotificationModel().VoyageOut(arrivalnotificationdata.VoyageOut);
    self.arrivalNotificationModel().IsTerminalOperator(arrivalnotificationdata.IsTerminalOperator);
    self.arrivalNotificationModel().TerminalOperatorID(arrivalnotificationdata.TerminalOperatorID);
    self.arrivalNotificationModel().ReasonForVisit(arrivalnotificationdata.ReasonForVisit);
    self.arrivalNotificationModel().ArrDraft(arrivalnotificationdata.ArrDraft);
    self.arrivalNotificationModel().DepDraft(arrivalnotificationdata.DepDraft);
    self.arrivalNotificationModel().LastPortOfCall(arrivalnotificationdata.LastPortOfCall);
    self.arrivalNotificationModel().NextPortOfCall(arrivalnotificationdata.NextPortOfCall);
    self.arrivalNotificationModel().AppliedForISPS(arrivalnotificationdata.AppliedForISPS);
    self.ArrivalNotificationData().AppliedDate(arrivalnotificationdata.AppliedDate);
    self.arrivalNotificationModel().Clearance(arrivalnotificationdata.Clearance);
    self.arrivalNotificationModel().ISPSReferenceNo(arrivalnotificationdata.ISPSReferenceNo);
    self.arrivalNotificationModel().PilotExemption(arrivalnotificationdata.PilotExemption);
    self.arrivalNotificationModel().PreferredPortCode(arrivalnotificationdata.PreferredPortCode);
    self.arrivalNotificationModel().PreferredQuayCode(arrivalnotificationdata.PreferredQuayCode);
    self.arrivalNotificationModel().PreferredBerthCode(arrivalnotificationdata.PreferredBerthCode);
    self.arrivalNotificationModel().AlternatePortCode(arrivalnotificationdata.AlternatePortCode);
    self.arrivalNotificationModel().AlternateQuayCode(arrivalnotificationdata.AlternateQuayCode);
    self.arrivalNotificationModel().AlternateBerthCode(arrivalnotificationdata.AlternateBerthCode);
    self.arrivalNotificationModel().PreferredSideDock(arrivalnotificationdata.PreferredSideDock);
    self.arrivalNotificationModel().PreferredSideAlternateBirth(arrivalnotificationdata.PreferredSideAlternateBirth);
    self.arrivalNotificationModel().ReasonAlternateBirth(arrivalnotificationdata.ReasonAlternateBirth);
    self.arrivalNotificationModel().Tidal(arrivalnotificationdata.Tidal);
    self.arrivalNotificationModel().BallastWater(arrivalnotificationdata.BallastWater);
    self.arrivalNotificationModel().WasteDeclaration(arrivalnotificationdata.WasteDeclaration);
    self.arrivalNotificationModel().DaylightRestriction(arrivalnotificationdata.DaylightRestriction);
    self.arrivalNotificationModel().IsSpecialNature(arrivalnotificationdata.IsSpecialNature);

    self.arrivalNotificationModel().LastPortWasteDelivered(arrivalnotificationdata.LastPortWasteDelivered);
    self.arrivalNotificationModel().NextPortWasteDelivery(arrivalnotificationdata.NextPortWasteDelivery);
    self.arrivalNotificationModel().DateLastWasteDelivered(arrivalnotificationdata.DateLastWasteDelivered);

    self.arrivalNotificationModel().SpecialNatureReason(arrivalnotificationdata.SpecialNatureReason);
    self.arrivalNotificationModel().ExceedPortLimitations(arrivalnotificationdata.ExceedPortLimitations);
    self.arrivalNotificationModel().ExceedPortLimitationsReason(arrivalnotificationdata.ExceedPortLimitationsReason);
    self.arrivalNotificationModel().DaylightSpecifyReason(arrivalnotificationdata.DaylightSpecifyReason);
    self.arrivalNotificationModel().ExceedSpecifyReason(arrivalnotificationdata.ExceedSpecifyReason);
    self.arrivalNotificationModel().AnyAdditionalInfo(arrivalnotificationdata.AnyAdditionalInfo);
    self.arrivalNotificationModel().CreatedBy(arrivalnotificationdata.CreatedBy);
    self.arrivalNotificationModel().CreatedDate(arrivalnotificationdata.CreatedDate);
}
//this method is  fires when HandleProgressBarAndActiveTab   is pressed
function HandleProgressBarAndActiveTab(index) {

    var total = $('#ulTabs').find('li').length;
    var current = index + 1;
    $('li', $('#divFormWizardTabNavigation')).removeClass("done");
    var li_list = $('#ulTabs').find('li');
    for (var i = 0; i < index; i++) {
        $(li_list[i]).addClass("done");
    }
    for (var i = current; i < total; i++) {
        $(li_list[i]).removeClass("done");
        $(li_list[i]).removeClass("active");
    }
    $(li_list[index]).addClass("active");
    var $percent = (current / total) * 100;
    $('#divFormWizardTabNavigation').find('.progress-bar').css({
        width: $percent + '%'
    });
}

var keynum;
var keychar;
var charcheck;
// validate the data 

function ValiedDocumentCheck(self, ArrivalNotificationData) {


    var docISPS = "N";
    var docPHCD = "N";
    var docIMDO = "N";
    var docBWD = "N";
    var docWDD = "N";
    var docLDD = "N";

    var docITCD = "N";
    var docPCD = "N";
    var docSRD = "N";
    var docSHRD = "N";

    var Documentalert = "";
    var isalert = false;
    //BWD  //IMDO      //ISPS   --  //LDD  //PHCD         //WDD   
    //ITCD  PCD SRD SHRD
    if (ArrivalNotificationData.ArrivalDocuments().length > 0) {
        $.map(ArrivalNotificationData.ArrivalDocuments, function (item) {
            var ArrivalDocumentsC = item;
            if (ArrivalDocumentsC != null)
                ko.utils.arrayForEach(ArrivalDocumentsC, function (DocChk) {

                    if (DocChk !== undefined) {
                        if (DocChk.DocumentCode() == "ISPS") {
                            docISPS = "Y"
                        }
                        else if (DocChk.DocumentCode() == "PHCD") {
                            docPHCD = "Y"
                        }
                        else if (DocChk.DocumentCode() == "IMDO") {
                            docIMDO = "Y"
                        }
                        else if (DocChk.DocumentCode() == "BWD") {
                            docBWD = "Y"
                        }
                        else if (DocChk.DocumentCode() == "WDD") {
                            docWDD = "Y"
                        }
                        else if (DocChk.DocumentCode() == "LDD") {
                            docLDD = "Y"
                        }
                        else if (DocChk.DocumentCode() == "ITCD") {
                            docITCD = "Y"

                        }
                        else if (DocChk.DocumentCode() == "PCD") {
                            docPCD = "Y"
                        }
                        else if (DocChk.DocumentCode() == "SRD") {
                            docSRD = "Y"
                        }
                        else if (DocChk.DocumentCode() == "SHRD") {
                            docSHRD = "Y"
                        }
                    }
                });

        });


        if (docBWD == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "BWD") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }
        if (docIMDO == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "IMDO") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }

        if (docISPS == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "ISPS") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }
        if (docITCD == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "ITCD") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }
        if (docLDD == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "LDD") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }

        if (docPHCD == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "PHCD") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }
        if (docPCD == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "PCD") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }
        if (docSRD == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "SRD") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }

        if (docSHRD == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "SHRD") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }

        if (docWDD == "N") {
            isalert = true;
            $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
                if (item != null)
                    if (item.SubCatCode() == "WDD") {
                        Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
                    }
            });
        }

        if (isalert) {
            Documentalert = Documentalert.substring(0, Documentalert.length - 1);
            Documentalert = Documentalert + '.';
        }
        return Documentalert;



    }
    else {
        $.each(self.arrivalNotificationReferenceData().Doctypes(), function (key, item) {
            if (item != null)
                Documentalert = Documentalert + ' ' + item.SubCatName() + ",";
        });
        Documentalert = Documentalert.substring(0, Documentalert.length - 1);
        Documentalert = Documentalert + '.';
        return Documentalert;
    }

}
function ValidateFormValues(self, ArrivalNotificationData) {

    ArrivalNotificationData.ReasonForVisit('');
    var isOptnlInfo = 0;
    for (var i = 0; i < ArrivalNotificationData.ArrivaReasonArray().length; i++) {
        ArrivalNotificationData.ReasonForVisit(ArrivalNotificationData.ArrivaReasonArray()[i]);
        if (ArrivalNotificationData.ReasonForVisit() == 'DRYD' || ArrivalNotificationData.ReasonForVisit() == 'BUNK' || ArrivalNotificationData.ReasonForVisit() == 'LABY' || ArrivalNotificationData.ReasonForVisit() == 'REPA') {
        }
        else {
            isOptnlInfo = 1;
        }
    }
    ArrivalNotificationData.PlanDateTimeOfBerth.extend({ required: true });
    ArrivalNotificationData.PlanDateTimeToVacateBerth.extend({ required: true });
    ArrivalNotificationData.PlanDateTimeToStartCargo.extend({ required: true });
    ArrivalNotificationData.PlanDateTimeToCompleteCargo.extend({ required: true });
    if (isOptnlInfo == 0) {
        ArrivalNotificationData.PlanDateTimeOfBerth.rules.remove(function (item) { return item.rule = "required"; });
        ArrivalNotificationData.PlanDateTimeToVacateBerth.rules.remove(function (item) { return item.rule = "required"; });
        ArrivalNotificationData.PlanDateTimeToStartCargo.rules.remove(function (item) { return item.rule = "required"; });
        ArrivalNotificationData.PlanDateTimeToCompleteCargo.rules.remove(function (item) { return item.rule = "required"; });
        self.isspanOptValid1(false);
        self.isspanOptValid2(false);
        self.isspanOptValid3(false);
        self.isspanOptValid4(false);

    }


    self.ArrivalNotificationValidation = ko.observable(ArrivalNotificationData);
    self.ArrivalNotificationValidation().errors = ko.validation.group(self.ArrivalNotificationValidation());
    var errors = self.ArrivalNotificationValidation().errors().length;
    var result = true;
    if (errors == 0) {
        result = true;
    }
    else {
        result = false;
    }

    var isqtyCommodityOptional = 0;
    for (var i = 0; i < ArrivalNotificationData.ArrivaReasonArray().length; i++) {

        ArrivalNotificationData.ReasonForVisit(ArrivalNotificationData.ArrivaReasonArray()[i]);
        if (ArrivalNotificationData.ReasonForVisit() == "OTHR" && (ArrivalNotificationData.SpecifyReason() == null || ArrivalNotificationData.SpecifyReason() == '')) {
            toastr.warning("Please Enter Other (specify)", "Arrival Notification");
            $("#spanSpecifyReason").text('This field is required');
            self.isspanSpecifyReason(true);
            result = false;
        }
        else if (ArrivalNotificationData.ReasonForVisit() == "OTHR" && (ArrivalNotificationData.SpecifyReason() != null || ArrivalNotificationData.SpecifyReason() != '')) {
            $("#spanSpecifyReason").text('');
            self.isspanSpecifyReason(false);
        }

        if (ArrivalNotificationData.ReasonForVisit() == "BUNK" || ArrivalNotificationData.ReasonForVisit() == "STOR"
            || ArrivalNotificationData.ReasonForVisit() == "LABY" || ArrivalNotificationData.ReasonForVisit() == "REPA"
            || ArrivalNotificationData.ReasonForVisit() == "DRYD") {
        }
        else { isqtyCommodityOptional = 1; }

    }


    //Draft Validation
    var IsvalidDraft = true;
    var msg = '';
    ko.utils.arrayForEach(self.arrivalNotificationReferenceBirthData().Berths(), function (DocChk) {

        if (DocChk.BerthKey() === ArrivalNotificationData.PreferedBerthKey()) {

            var aftd = parseFloat(ArrivalNotificationData.ArrDraft()).toFixed(2);
            var depd = parseFloat(ArrivalNotificationData.DepDraft()).toFixed(2);
            var actdft = parseFloat(DocChk.Draftm()).toFixed(2);
            if (compare(actdft, aftd) && compare(actdft, depd)) {
                msg = "Arr. Draft(m) - " + aftd + " and Dep. Draft(m) - " + depd + " are more than Berth Draft - " + actdft;
                IsvalidDraft = false;
            }
            else if (compare(actdft, aftd)) {
                msg = "Arr. Draft(m) - " + aftd + " is more than Berth Draft - " + actdft;
                IsvalidDraft = false;
            }
            else if (compare(actdft, depd)) {
                msg = "Dep. Draft(m) - " + depd + " is more than Berth Draft - " + actdft;
                IsvalidDraft = false;
            }
        }
        if (!IsvalidDraft) {
            toastr.error(aa, "Arrival Notification");
        }

    });

    if (!IsvalidDraft) {
        toastr.error(msg, "Arrival Notification");
        result = false;
    }

    // Newcheck Reddy
    if (ArrivalNotificationData.ArrivalCommodities().length == 0 && isqtyCommodityOptional == 1) {
        toastr.warning("Please Enter Quantities of Commodity", "Arrival Notification");
        result = false;
    }
    else {

        $.each(ArrivalNotificationData.ArrivalCommodities(), function (key, item) {
            var CommodChk = item;

            if (CommodChk != null)
                //Quantity UOM CommodityBerthKey CargoType Package
                if (CommodChk !== undefined) {
                    var QuantityVal = CommodChk.Quantity();
                    if (QuantityVal == '' || QuantityVal == null) {
                        QuantityVal = 0;
                    }
                    if (CommodChk.CommodityBerthKey() == undefined || CommodChk.CargoType() == undefined || CommodChk.Commodity() == undefined || CommodChk.Package() == undefined || CommodChk.UOM() == undefined || CommodChk.CommodityBerthKey() == "" || CommodChk.CargoType() == ""|| CommodChk.Commodity() == "" || CommodChk.Package() == "" || CommodChk.UOM() == "" || QuantityVal == 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please enter valid details of Quantities of Commodity", "Arrival Notification");

                        result = false;
                    }
                }

        });

    }

    if ($('input:radio[name=WasteDeclaration]:checked').val() == "A") {
        self.WasteDeclarationVisible(true);
        $("#rdYesWasteDeclaration").attr('checked', 'checked');
        if (self.isViewMode() != true) {
            self.isWasteDeclationEnable(true);

            $("#DateLastWasteDelivered").data('kendoDateTimePicker').enable(true);
        }


    }
    else {
        self.WasteDeclarationVisible(false);
        $("#rdNoWasteDeclaration").attr('checked', 'checked');
        self.arrivalNotificationModel().LastPortWasteDelivered('');
        self.arrivalNotificationModel().NextPortWasteDelivery('');
        self.arrivalNotificationModel().DateLastWasteDelivered('');
        self.arrivalNotificationModel().WasteDeclarations.removeAll();
        if (self.isViewMode() != true) {
            self.isWasteDeclationEnable(false);

            $("#DateLastWasteDelivered").data('kendoDateTimePicker').enable(false);
        }
    }


    if (ArrivalNotificationData.ViewModeForTabs() == 'notification2') {
        if ($('input:radio[name=IsDangerousGoodsonBoard]:checked').val() == "A") {
            if (ArrivalNotificationData.IMDGInformations().length == 0) {
                if (ArrivalNotificationData.IMDGInformations().length == 0) {
                    toastr.warning("Please Add Atleast One IMDG Cargo Information", "Arrival Notification");
                    result = false;
                }
            }
            if (ArrivalNotificationData.IMDGInformations().length > 0) {

                var IMDGInformationsDetails = ko.utils.arrayFilter(ArrivalNotificationData.IMDGInformations(), function (items) {
                    if (items.Others() == null)
                        items.Others('');
                    if (items.Purpose() == null || items.ClassCode() == null || items.CargoCode() == null || items.UNNo() == '') {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Enter Valid Details of IMDG Cargo Information", "Arrival Notification");
                        result = false;
                    }
                });


                var alrtmsg = "";

                if (ArrivalNotificationData.CellNo() == null || ArrivalNotificationData.CellNo() == '') {
                    alrtmsg = alrtmsg + " Cell No. ";
                    result = false;
                }


                if (ArrivalNotificationData.CargoDescription() == null || ArrivalNotificationData.CargoDescription() == '') {
                    alrtmsg = alrtmsg + " Cargo Description ";
                    result = false;
                }

                if (alrtmsg != "") {
                    toastr.warning("Please Enter " + alrtmsg + " of Dangerous Goods", "Arrival Notification");
                    result = false;
                }

                var cellno = ArrivalNotificationData.CellNo();
                cellno = cellno.replace(/(\)|\()|_|-+/g, '');

                if (cellno.length != 0) {
                    if (cellno.length != 13) {
                        toastr.warning("Invalid Cell No.", "Arrival Notification");
                        result = false;
                    }
                    else if (cellno.length == 13) {
                        var validNo = parseInt(cellno);
                        if (validNo == 0) {
                            toastr.warning("Invalid Cell No.", "Arrival Notification");
                            result = false;
                        }
                    }
                }



            }

        }

        if ($('input:radio[name=WasteDeclaration]:checked').val() == "A") {
            if (ArrivalNotificationData.WasteDeclarations().length == 0) {
                if (ArrivalNotificationData.WasteDeclarations().length == 0) {
                    toastr.warning("Please Add Atleast One Waste Declaration Information", "Arrival Notification");
                    result = false;
                }
            }
            if (ArrivalNotificationData.WasteDeclarations().length > 0) {

                var WasteDeclarationsDetails = ko.utils.arrayFilter(ArrivalNotificationData.WasteDeclarations(), function (items) {
                    if (items.Others() == null)
                        items.Others('');
                    if (items.MarpolCode() == null || items.ClassCode() == null || items.LicenseRequestID() == null || items.Quantity() == "") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Enter Valid Details of Waste Declaration Information", "Arrival Notification");
                        result = false;
                    }
                });


                var alrtmsg = "";

                if (ArrivalNotificationData.LastPortWasteDelivered() == null || ArrivalNotificationData.LastPortWasteDelivered() == '') {
                    alrtmsg = alrtmsg + " Last Port Waste Delivered ";
                    result = false;
                }


                if (ArrivalNotificationData.NextPortWasteDelivery() == null || ArrivalNotificationData.NextPortWasteDelivery() == '') {
                    alrtmsg = alrtmsg + " Next Port Waste Delivery ";
                    result = false;
                }

                if (ArrivalNotificationData.DateLastWasteDelivered() == null || ArrivalNotificationData.DateLastWasteDelivered() == '') {
                    alrtmsg = alrtmsg + " Date Last Waste Delivered ";
                    result = false;
                }


                if (alrtmsg != "") {
                    toastr.warning("Please Enter " + alrtmsg + " of  Waste Declaration", "Arrival Notification");
                    result = false;
                }
            }

        }






        var Reasonalrtmsg = "";
        var ReasonalrtmsgLR = "";


        var fromReason = "";
        var isreasonLayup = false;
        var isBunker = false;
        var isRep = false;

        for (var i = 0; i < ArrivalNotificationData.ArrivaReasonArray().length; i++) {

            ArrivalNotificationData.ReasonForVisit(ArrivalNotificationData.ArrivaReasonArray()[i]);
            if (ArrivalNotificationData.ReasonForVisit() == 'LABY' || ArrivalNotificationData.ReasonForVisit() == 'REPA' && isreasonLayup == false) {
                isreasonLayup = true;
                fromReason = "Lay By and Repair Information Details";
                if (ArrivalNotificationData.PlannedDurationDate() == null || ArrivalNotificationData.PlannedDurationDate() == '') {
                    isRep = true;
                    ReasonalrtmsgLR = ReasonalrtmsgLR + " Planned Duration of Lay Up or Repairs ";
                    result = false;
                }
                if (ArrivalNotificationData.PlannedDurationToDate() == null || ArrivalNotificationData.PlannedDurationToDate() == '') {
                    isRep = true;
                    ReasonalrtmsgLR = ReasonalrtmsgLR + " To ";
                    result = false;
                }
                if (ArrivalNotificationData.ReasonForLayup() == null || ArrivalNotificationData.ReasonForLayup() == '') {
                    isRep = true;
                    ReasonalrtmsgLR = ReasonalrtmsgLR + " Reason for Lay Up/Nature of Repairs ";
                    result = false;
                }
                if (isRep) {
                    ReasonalrtmsgLR = ReasonalrtmsgLR + " of Lay By and Repair Information Details. ";
                }
            }



            if (ArrivalNotificationData.ReasonForVisit() == 'BUNK') {
                fromReason = "Bunker Information Details";

                if (ArrivalNotificationData.BunkersRequired() == null || ArrivalNotificationData.BunkersRequired() == '') {
                    isBunker = true;
                    Reasonalrtmsg = Reasonalrtmsg + " Bunkers Required (Specify Type) ";
                    result = false;
                }
                if (ArrivalNotificationData.BunkersMethod() == null || ArrivalNotificationData.BunkersMethod() == '') {
                    isBunker = true;
                    Reasonalrtmsg = Reasonalrtmsg + " Bunkers Method (Specify) ";
                    result = false;
                }
                if (ArrivalNotificationData.BunkerService() == null || ArrivalNotificationData.BunkerService() == '') {
                    isBunker = true;
                    Reasonalrtmsg = Reasonalrtmsg + " Bunker Service Provider ";
                    result = false;
                }
                if (ArrivalNotificationData.DistanceFromStern() == null || ArrivalNotificationData.DistanceFromStern() == '') {
                    isBunker = true;
                    Reasonalrtmsg = Reasonalrtmsg + " Distance from Stern to Bunker Point (in m) ";
                    result = false;
                }
                if (ArrivalNotificationData.TonsMT() == null || ArrivalNotificationData.TonsMT() == '') {
                    isBunker = true;
                    Reasonalrtmsg = Reasonalrtmsg + " Tons (in mt) ";
                    result = false;
                }
                if (isBunker) {
                    Reasonalrtmsg = Reasonalrtmsg + " of Bunker Information Details. ";

                }
            }


        }
        if (isRep && isBunker) {
            toastr.warning("Please Enter " + ReasonalrtmsgLR + ' ' + Reasonalrtmsg, "Arrival Notification");
            result = false;
        }
        else if (isRep) {
            toastr.warning("Please Enter " + ReasonalrtmsgLR, "Arrival Notification");
            result = false;
        }
        else if (isBunker) {
            toastr.warning("Please Enter " + Reasonalrtmsg, "Arrival Notification");
            result = false;
        }
    }


    if (ArrivalNotificationData.errors().length > 0) {
        ArrivalNotificationData.errors.showAllMessages();

        if (ArrivalNotificationData.VesselID() == '' || ArrivalNotificationData.VesselName() == '') {
            self.isspanVslValid(true);
        }
        else {
            self.isspanVslValid(false);
        }



        // Main
        toastr.warning("You Have Some form Errors. Please Check Below.");
        result = false;

        var isOptnlInfo = 0;
        for (var i = 0; i < ArrivalNotificationData.ArrivaReasonArray().length; i++) {
            ArrivalNotificationData.ReasonForVisit(ArrivalNotificationData.ArrivaReasonArray()[i]);
            if (ArrivalNotificationData.ReasonForVisit() == 'DRYD' || ArrivalNotificationData.ReasonForVisit() == 'BUNK' || ArrivalNotificationData.ReasonForVisit() == 'LABY' || ArrivalNotificationData.ReasonForVisit() == 'REPA') {
            }
            else {
                isOptnlInfo = 1;
            }
        }

        if (isOptnlInfo == 1) {

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





        if ($("#ETA").val() == "" || $("#ETA").val() == null) {
            $("#spanEtaValid").text('This field is required');
            self.isspanEtaValid(true);
        }
        else {
            $("#ValidityDateMsg").text('');
            self.isspanEtaValid(false);
        }


        if ($("#ETD").val() == "" || $("#ETD").val() == null) {
            $("#spanEtdValid").text('This field is required');
            self.isspanEtdValid(true);

        }
        else {
            $("#spanEtdValid").text('');
            self.isspanEtdValid(false);
        }


        if (self.arrivalNotificationModel().ViewModeForTabs() == "notification1") {
            GoToTab1(self, ArrivalNotificationData);
        }
        else if (self.arrivalNotificationModel().ViewModeForTabs() == "notification2") {
            GoToTab2(self, ArrivalNotificationData);
        }
        else {
            GoToTab3(self, ArrivalNotificationData);
        }
    }

    return result;


}
function GoToTab1(self, arrivalnotificationData) {
    self.viewModeForTabs('notification1');
    self.arrivalNotificationModel().ViewModeForTabs('notification1');
    self.isSubmitVisible(false);
    self.isGoNextVisible(false);
    if (self.IsAddMode()) {
        self.isSaveDraftVisible(true);
    }
    else {
        self.isSaveDraftVisible(false);
    }
    if (self.isViewMode()) {
        self.isSaveVisible(false);
        self.isUpdateVisible(false);
        self.isGoNextVisible(true);
    }
    else {

        if (self.arrivalNotificationModel().VCN() != "") {
            self.isUpdateVisible(true);
            self.isSaveVisible(false);

        }
        else {
            self.isUpdateVisible(false);
            self.isSaveVisible(true);
        }
    }
    self.isGoBackVisible(false);
    var index = 0;
    HandleProgressBarAndActiveTab(index);
}

function GoToTab2(self, arrivalnotificationData) {

    var isqtyCommodityOptional = 0;
    for (var i = 0; i < arrivalnotificationData.ArrivaReasonArray().length; i++) {
        if (arrivalnotificationData.ReasonForVisit() == "BUNK" || arrivalnotificationData.ReasonForVisit() == "STOR"
            || arrivalnotificationData.ReasonForVisit() == "LABY" || arrivalnotificationData.ReasonForVisit() == "REPA"
            || arrivalnotificationData.ReasonForVisit() == "DRYD") {
        }
        else { isqtyCommodityOptional = 1; }
    }

    var errorCount = 0;
    if (arrivalnotificationData.ArrivalCommodities().length == 0 && isqtyCommodityOptional == 1) {
        toastr.warning("Please Enter Quantities of Commodity", "Arrival Notification");
        errorCount = errorCount + 1;
        return;
    }
    else {

        $.each(arrivalnotificationData.ArrivalCommodities(), function (key, item) {

            var CommodChk = item;
            if (CommodChk != null)

                //Quantity UOM CommodityBerthKey CargoType Package
                if (CommodChk !== undefined) {
                    var QuantityVal = CommodChk.Quantity();
                    if (QuantityVal == '' || QuantityVal == null) {
                        QuantityVal = 0;
                    }
                    if (CommodChk.CommodityBerthKey() == "" || CommodChk.CargoType() == "" || CommodChk.Commodity() == "" || CommodChk.Package() == "" || CommodChk.UOM() == "" || QuantityVal == 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Enter Valid Details of Quantities of Commodity", "Arrival Notification");
                        errorCount = errorCount + 1;
                        return;
                    }
                }

        });

    }
    if (errorCount == 0) {
        if ($('input:radio[name=IsDangerousGoodsonBoard]:checked').val() == "A") {
            self.IsDangerousGoods(true);
            $("#rdYesDangerousGoods").attr('checked', 'checked');
        }
        else if ($('input:radio[name=IsDangerousGoodsonBoard]:checked').val() == "I") {
            self.IsDangerousGoods(false);
            $("#rdNoDangerousGoods").attr('checked', 'checked');
        }
        else {
            self.IsDangerousGoods(false);
            $("#rdNoDangerousGoods").attr('checked', 'checked');
        }

        self.viewModeForTabs('notification2');
        self.arrivalNotificationModel().ViewModeForTabs('notification2');
        self.isSubmitVisible(false);
        self.isGoNextVisible(false);

        if (self.IsAddMode()) {
            self.isSaveDraftVisible(true);
        }
        else {
            self.isSaveDraftVisible(false);
        }


        if (self.isViewMode()) {
            self.isSaveVisible(false);
            self.isUpdateVisible(false);
            self.isGoNextVisible(true);
        }
        else {

            if (self.arrivalNotificationModel().VCN() != "") {
                self.isUpdateVisible(true);
                self.isSaveVisible(false);

            }
            else {
                self.isUpdateVisible(false);
                self.isSaveVisible(true);
            }

        }

        self.isGoBackVisible(true);
        var index = 1;
        HandleProgressBarAndActiveTab(index);
    }
}
function GoToTab3(self, arrivalnotificationData) {

    var errorCountForArrivalIMDGTankers = 0;
    var alrtmsg = "";
    self.isGoNextVisible(false);
    if ($('input:radio[name=IsDangerousGoodsonBoard]:checked').val() == "A") {
        if (arrivalnotificationData.IMDGInformations().length == 0) {
            if (arrivalnotificationData.IMDGInformations().length == 0) {
                errorCountForArrivalIMDGTankers = 1;
                toastr.warning("Please Add Atleast One IMDG Cargo Information", "Arrival Notification");
                return errorCountForArrivalIMDGTankers;
            }

        }

        if (arrivalnotificationData.IMDGInformations().length > 0) {


            var IMDGInformationsDetails = ko.utils.arrayFilter(arrivalnotificationData.IMDGInformations(), function (items) {
                if (items.Others() == null)
                    items.Others('');
                if (items.Purpose() == null || items.ClassCode() == null || items.CargoCode() == null || items.UNNo() == '') {
                    errorCountForArrivalIMDGTankers = 1;
                    return errorCountForArrivalIMDGTankers;
                }
            });

            if (errorCountForArrivalIMDGTankers == 1) {
                toastr.warning("Please Enter Details of IMDG Cargo Information", "Arrival Notification");
                errorCountForArrivalIMDGTankers++;
                return errorCountForArrivalIMDGTankers;
            }

            if (arrivalnotificationData.CellNo() == null || arrivalnotificationData.CellNo() == '') {
                alrtmsg = alrtmsg + " Cell No. ";
                result = false;
            }


            if (arrivalnotificationData.CargoDescription() == null || arrivalnotificationData.CargoDescription() == '') {
                alrtmsg = alrtmsg + " Cargo Description ";
                result = false;
            }

            if (alrtmsg != "") {
                toastr.warning("Please Enter " + alrtmsg + " of Dangerous Goods", "Arrival Notification");
                result = false;
            }
        }

    }


    if ($('input:radio[name=WasteDeclaration]:checked').val() == "A") {
        if (arrivalnotificationData.WasteDeclarations().length == 0) {
            if (arrivalnotificationData.WasteDeclarations().length == 0) {
                toastr.warning("Please Add Atleast One Waste Declaration Information", "Arrival Notification");
                result = false;
            }
        }
        if (arrivalnotificationData.WasteDeclarations().length > 0) {

            var WasteDeclarationsDetails = ko.utils.arrayFilter(arrivalnotificationData.WasteDeclarations(), function (items) {
                if (items.Others() == null)
                    items.Others('');
                if (items.MarpolCode() == null || items.ClassCode() == null || items.LicenseRequestID() == null || items.Quantity() == "") {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Enter Valid Details of Waste Declaration Information", "Arrival Notification");
                    result = false;
                }
            });


            var alrtmsg = "";

            if (arrivalnotificationData.LastPortWasteDelivered() == null || arrivalnotificationData.LastPortWasteDelivered() == '') {
                alrtmsg = alrtmsg + " Last Port Waste Delivered ";
                result = false;
            }


            if (arrivalnotificationData.NextPortWasteDelivery() == null || arrivalnotificationData.NextPortWasteDelivery() == '') {
                alrtmsg = alrtmsg + " Next Port Waste Delivery ";
                result = false;
            }

            if (arrivalnotificationData.DateLastWasteDelivered() == null || arrivalnotificationData.DateLastWasteDelivered() == '') {
                alrtmsg = alrtmsg + " Date Last Waste Delivered ";
                result = false;
            }


            if (alrtmsg != "") {
                toastr.warning("Please Enter " + alrtmsg + " of  Waste Declaration", "Arrival Notification");
                result = false;
            }
        }

    }


    if (errorCountForArrivalIMDGTankers == 0 && alrtmsg == "") {
        self.viewModeForTabs('notification3');
        self.arrivalNotificationModel().ViewModeForTabs('notification3');


        if (self.isViewMode()) {
            self.isSaveVisible(false);
            self.isUpdateVisible(false);
        }
        else {
            if (self.arrivalNotificationModel().VCN() != "") {
                self.isUpdateVisible(true);
                self.isSaveVisible(false);

            }
            else {
                self.isUpdateVisible(false);
                self.isSaveVisible(true);
            }
            self.isSubmitVisible(true);

            if (self.IsAddMode()) {
                self.isSaveDraftVisible(true);
            }
            else {
                self.isSaveDraftVisible(false);
            }


            self.isSaveVisible(false);
            self.isUpdateVisible(false);
        }



        self.isGoBackVisible(true);
        var index = 2;
        HandleProgressBarAndActiveTab(index);
    }
    else {

        if (self.arrivalNotificationModel().ViewModeForTabs() == "notification1") {
            GoToTab1(self, arrivalnotificationData);
        }
        else if (self.arrivalNotificationModel().ViewModeForTabs() == "notification2") {
            GoToTab2(self, arrivalnotificationData);
        }
        else {
            GoToTab3(self, arrivalnotificationData);
        }
    }


}

//Validation for IMDG Cargo Information and IMDG Tanker Commodities

calmaxtoday = function () {
    this.max(new Date());


};
calmintoday = function () {
    this.min(new Date());


};

// remove rows from temparary table the data 
function RemoveQualityCommodity(obj) {
    $(obj).closest("tr").remove();
    var objIndex = $(obj).closest("tr").index();
    self.arrivalNotificationModel().CommoditiesQuantitiesList().pop(self.arrivalNotificationModel().CommoditiesQuantitiesList()[objIndex]);
    self.arrivalNotificationModel().EnableDisableAddNew(true);
    isEdit = isEdit - 1;
}





//To validate alphabet with spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z \b]*$/;
    return charcheck.test(keychar);
}



function compare(number1, number2) {
    var precision1, precision2, decimal1, decimal2, flag = false;
    precision1 = parseInt(number1.substr(0, String(number1).indexOf('.')));
    decimal1 = parseInt(number1.substr(String(number1).indexOf('.') + 1));
    precision2 = parseInt(number2.substr(0, String(number2).indexOf('.')));
    decimal2 = parseInt(number2.substr(String(number2).indexOf('.') + 1));
    if (precision1 < precision2) flag = true;
    else if (precision1 == precision2 && decimal1 < decimal2) flag = true;
    return flag;
}


