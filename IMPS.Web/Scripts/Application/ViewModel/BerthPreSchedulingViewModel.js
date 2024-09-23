(function (IPMSRoot) {
    var isView = 0;
    var BerthPreSchedulingViewModel = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.berthpreschedulingModel = ko.observable(new IPMSROOT.BerthPreSchedulingModel());
        self.viewMode = ko.observable();
        self.berthprescheduleList = ko.observableArray();
        self.craftmasterModel = ko.observable();
        self.berthPreSchedulingBinding = ko.observable();
        self.berthprescheduleReferenceData = ko.observable();
        self.IsScheduleStatus = ko.observable(true);
        self.viewMode = ko.observable();
        self.IsSchedule = ko.observable();
        self.CurrentDate = ko.observable();
        self.EnableUpdate = ko.observable(true);
        self.EnableGetSuitable = ko.observable(true);
        self.EnableGetSuitableBerths = ko.observable(true);
        self.Dateloadgrid = ko.observable(true);
        self.SelectBollards = ko.observable(false);

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.berthpreschedulingModel(new IPMSROOT.BerthPreSchedulingModel());
            self.LoadReferenceData();

            self.berthprescheduleReferenceData().ETA(moment(new Date()).format('YYYY-MM-DD'));
            var CurrentDate = new Date();
            var ETA = self.berthprescheduleReferenceData().ETA();
            CurrentDate.setDate(CurrentDate.getDate() + 2);
            self.berthprescheduleReferenceData().ETD(moment(CurrentDate).format('YYYY-MM-DD'));

            var ETD = self.berthprescheduleReferenceData().ETD();
            self.Dateloadgrid(true);
            self.LoadBerthPreSchedulingList();
            self.viewMode('List');
        }


        self.LoadBerthPreSchedulingList = function (data) {

            if (self.Dateloadgrid() == true) {

                var ETA = self.berthprescheduleReferenceData().ETA();
                var ETD = self.berthprescheduleReferenceData().ETD();
            }
            else {
                var ETA = $("#SearchETAFrom").val();
                var ETD = $("#SearchETATo").val();

            }



            var AgentID = self.berthprescheduleReferenceData().selectedAgentID();

            var VesselType = self.berthprescheduleReferenceData().selectedVesselType();
            var ReasonforVisit = self.berthprescheduleReferenceData().selectedReasonforVisit();
            var CargoType = self.berthprescheduleReferenceData().selectedCargoType();
            var MovementStatus = self.berthprescheduleReferenceData().selectedMovementStatus();

            if (AgentID == undefined)
                AgentID = "All";
            if (VesselType == undefined)
                VesselType = "All";
            if (ReasonforVisit == undefined)
                ReasonforVisit = "All";
            if (CargoType == undefined)
                CargoType = "All";
            if (MovementStatus == undefined)
                MovementStatus = "All";


            self.viewModelHelper.apiGet('api/GetVCMList/' + AgentID + '/' + ETA + '/' + ETD + '/' + VesselType + '/' + ReasonforVisit + '/' + CargoType + '/' + MovementStatus, {},
                        function (result) {
                            self.berthprescheduleList(result);
                        });
        }

        self.GetDataClick = function (model) {


            var etafrom = $("#SearchETAFrom").val();
            var etato = $("#SearchETATo").val();
            if (etato < etafrom) {
                bootbox.alert('ETA To should be greater than ETA From');
            }
            else {

                self.LoadBerthPreSchedulingListOnGetData();
            }
        }



        self.LoadBerthPreSchedulingListOnGetData = function (data) {

            var ETA = $("#SearchETAFrom").val();
            var ETD = $("#SearchETATo").val();
            var AgentID = self.berthprescheduleReferenceData().selectedAgentID();


            var VesselType = self.berthprescheduleReferenceData().selectedVesselType();
            var ReasonforVisit = self.berthprescheduleReferenceData().selectedReasonforVisit();
            var CargoType = self.berthprescheduleReferenceData().selectedCargoType();
            var MovementStatus = self.berthprescheduleReferenceData().selectedMovementStatus();
            if (AgentID == "" || AgentID == undefined)
                AgentID = "All";
            if (VesselType == "" || VesselType == undefined)
                VesselType = "All";
            if (ReasonforVisit == "" || ReasonforVisit == undefined)
                ReasonforVisit = "All";
            if (CargoType == "" || CargoType == undefined)
                CargoType = "All";
            if (MovementStatus == "" || MovementStatus == undefined)
                MovementStatus = "All";


            self.viewModelHelper.apiGet('api/GetVCMList/' + AgentID + '/' + ETA + '/' + ETD + '/' + VesselType + '/' + ReasonforVisit + '/' + CargoType + '/' + MovementStatus, {},
                        function (result) {
                            self.berthprescheduleList(result);

                        });
        }


        self.LoadReferenceData = function () {
            self.viewModelHelper.apiGet('api/BerthPreSchedulingReferenceData', null,
                    function (result1) {

                        self.berthprescheduleReferenceData(new IPMSROOT.ReferenceData(result1));
                    }, null, null, false);
        }

        self.AgentSelect = function (e) {

            var selecteddataItem = this.dataItem(e.item.index());
            self.berthpreschedulingModel().AgentID(selecteddataItem.AgentID);


        }


        self.ScheduleClick = function (e) {

            self.SelectBollards(false);

            $('#spanbollard').text('');
            self.berthPreSchedulingBinding(e);
            var vcn = e.VCN;
            var vesselCallMovementID = e.VesselCallMovementID;
            self.EnableGetSuitable(false);
            //self.berthpreschedulingModel().ETB((moment(e.ETA).format('YYYY-MM-DD HH:mm'))); // Commented by sandeep
            self.berthpreschedulingModel().ETB((moment(e.ETB).format('YYYY-MM-DD HH:mm'))); // Added by sandeep
            self.berthpreschedulingModel().ETUB((moment(e.ETD).format('YYYY-MM-DD HH:mm')));

            var ETB = moment(self.berthpreschedulingModel().ETB()).format('YYYY-MM-DD HH:mm').toString();
            var ETUB = moment(self.berthpreschedulingModel().ETUB()).format('YYYY-MM-DD HH:mm').toString();

            self.viewModelHelper.apiGet('api/GetSuitableBerths', { VCN: vcn, ETB: ETB, ETUB: ETUB, VesselCallMovementID: vesselCallMovementID },
                function (result) {
                    self.berthpreschedulingModel().Berths(result);
                }, null, null, false);


            self.viewMode('Form');
            self.berthpreschedulingModel().VesselCallID(e.VesselCallID);
            self.berthpreschedulingModel().VesselCallMovementID(e.VesselCallMovementID);
            self.berthpreschedulingModel().VCN(e.VCN);
            self.berthpreschedulingModel().LengthOverallInM(e.LengthOverallInM);
            self.berthpreschedulingModel().SheduledBerth("");
            var SuitableBerthsList = self.berthpreschedulingModel().Berths();

            if (SuitableBerthsList == "") {
                self.EnableUpdate(false);
                self.berthpreschedulingModel().SheduledBerth("");
                bootbox.alert('Suitable berths are not available.');

            } else {
                self.EnableUpdate(true);
                var Pberthassigned = false;
                $.each(SuitableBerthsList, function (key, value) {
                    if (e.PreferredBerth == value.BerthCode && Pberthassigned == false) {
                        self.berthpreschedulingModel().SheduledBerth(e.PreferredBerth);
                        Pberthassigned = true;
                    }
                    else if (e.AlternateBerth == value.BerthCode && Pberthassigned == false) {
                        self.berthpreschedulingModel().SheduledBerth(e.AlternateBerth);
                    }
                    return;
                });

                var scheduledberth = self.berthpreschedulingModel().SheduledBerth();

                if (self.berthpreschedulingModel().SheduledBerth() != "") {
                    $.each(SuitableBerthsList, function (key, value) {
                        if (scheduledberth == value.BerthCode) {

                            self.berthpreschedulingModel().PortCode(value.PortCode);
                            self.berthpreschedulingModel().QuayCode(value.QuayCode);

                            //var dataItembollards = value.Bollards;
                            //self.berthpreschedulingModel().FromBollardCode(dataItembollards[0].BollardCode);
                            //var LengthOverallInM = value.LengthOverallInM;

                            //var total = 0;
                            //var bollardstop = false;
                            //$.each(dataItembollards, function (index, bollard) {

                            //    var bolength = parseFloat(bollard.ToMeter) - parseFloat(bollard.FromMeter);
                            //    if ((parseFloat(bolength) < parseFloat(LengthOverallInM)) && bollardstop == false) {

                            //        total = total + bolength;
                            //        if (total >= LengthOverallInM) {
                            //            self.berthpreschedulingModel().ToBollardCode(bollard.BollardCode);
                            //            bollardstop = true;
                            //        }
                            //    }
                            //    else if ((parseFloat(bolength) > parseFloat(LengthOverallInM)) && bollardstop == false) {
                            //        self.berthpreschedulingModel().ToBollardCode(bollard.BollardCode);
                            //        bollardstop = true;

                            //    }

                            //});

                            self.SelectBollards(true);
                            self.berthpreschedulingModel().FromBollardCode("");

                            self.berthpreschedulingModel().Bollards([]);

                            self.viewModelHelper.apiGet('api/BollardsInBerth/' + value.PortCode + '/' + value.QuayCode + '/' + value.BerthCode, null,
                                function (result) {
                                    self.berthpreschedulingModel().Bollards(result);
                                }, null, null, false);
                        }
                    });
                }
            }

            //var ETBstartDate = moment(e.ETA).format('YYYY-MM-DD HH:mm'); // Commented by sandeep
            var ETBstartDate = moment(e.ETB).format('YYYY-MM-DD HH:mm'); // Added by sandeep
            $("#ETB").kendoDateTimePicker({
                min: ETBstartDate,
                format: "yyyy-MM-dd HH:mm",
                parseFormats: ["yyyy-MM-dd", "HH:mm"],
                timeFormat: "HH:mm",
                month: { empty: '<span class=k-state-disabled>#= data.value #</span>' }
            });

            $("#ETUB").kendoDateTimePicker({
                min: ETBstartDate,
                format: "yyyy-MM-dd HH:mm",
                parseFormats: ["yyyy-MM-dd", "HH:mm"],
                timeFormat: "HH:mm",
                month: { empty: '<span class=k-state-disabled>#= data.value #</span>' }
            });

        }

        self.GetSuitableBerthClick = function (model) {
            self.SelectBollards(false);
            self.berthpreschedulingModel().FromBollardCode("");
            self.berthpreschedulingModel().SheduledBerth("");
            self.EnableGetSuitableBerths(true);
            self.EnableUpdate(true);
            var vcn = self.berthpreschedulingModel().VCN();
            var ETB = moment(self.berthpreschedulingModel().ETB()).format('YYYY-MM-DD HH:mm').toString();
            var ETUB = moment(self.berthpreschedulingModel().ETUB()).format('YYYY-MM-DD HH:mm').toString();

            if (ETB >= ETUB) {
                self.berthpreschedulingModel().Berths.removeAll();
                bootbox.alert('ETUB should be greater than ETB');
                self.EnableUpdate(false);
            }
            else {
                self.viewModelHelper.apiGet('api/GetSuitableBerths', { VCN: vcn, ETB: ETB, ETUB: ETUB },
                  function (result) {

                      self.berthpreschedulingModel().Berths(result);
                  }, null, null, false);

                if (self.berthpreschedulingModel().Berths() == "") {
                    self.EnableUpdate(false);
                    self.berthpreschedulingModel().SheduledBerth("");
                    bootbox.alert('Suitable berths are not available.');

                }

            }
        }


        self.UpdateClick = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if (model.SheduledBerth() == "") {
                $('#spanberth').text('* This field is required.');
                $('#spanberth').show();
            }
            else if (model.FromBollardCode() == "") {
                $('#spanbollard').text('* This field is required.');
                $('#spanbollard').show();
            }
            else {

                self.berthpreschedulingModel().IsScheduleStatus(true);
                self.viewModelHelper.apiPut('api/BerthPreScheduling', ko.mapping.toJSON(model), function Message(data) {
                    if (data != "true") {
                        toastr.warning(data, "Berth Pre Scheduling");
                    }
                    else {
                        toastr.success("Berth is scheduled.", "Berth Pre Scheduling");
                        self.Dateloadgrid(false);
                        self.LoadBerthPreSchedulingList();
                        self.viewMode('List');
                    }

                });
            }
        }


        self.ConfirmClick = function (model) {

            // if (confirm('Are you sure want to Confirm?')) {
            $.confirm({
                'title': 'Berth Pre Scheduling Confirmation',
                'message': 'Are you sure want to confirm?',
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            self.IsScheduleStatus(false);
                            self.berthpreschedulingModel().IsScheduleStatus(true);
                            self.viewModelHelper.apiPut('api/BerthPreScheduling', ko.mapping.toJSON(model), function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Berth is confirmed.", "Berth Pre Scheduling");
                                self.Dateloadgrid(false);
                                self.LoadBerthPreSchedulingList();
                                self.viewMode('List');

                            });

                        }
                    },
                    'No': {
                        'class': 'gray',
                        'action': function () {
                            //   $(this).dialog("close");
                        }
                    }
                }
            });
        }

        self.ScheduledBerthChange = function (e) {
            
            $('#spanberth').hide();
            var dataItem = this.dataItem(e.item.index());

            if (dataItem.BerthCode != "") {
                self.SelectBollards(true);

                self.berthpreschedulingModel().PortCode(dataItem.PortCode);
                self.berthpreschedulingModel().QuayCode(dataItem.QuayCode);
                self.berthpreschedulingModel().BerthCode(dataItem.BerthCode);
                self.berthpreschedulingModel().FromBollardCode("");

                //var dataItembollards = dataItem.Bollards;
                //self.berthpreschedulingModel().FromBollardCode(dataItembollards[0].BollardCode);
                //var LengthOverallInM = dataItem.LengthOverallInM;

                //var total = 0;
                //var bollardstop = false;
                //$.each(dataItembollards, function (index, bollard) {

                //    var bolength = parseFloat(bollard.ToMeter) - parseFloat(bollard.FromMeter);

                //    if ((parseFloat(bolength) < parseFloat(LengthOverallInM)) && bollardstop == false) {
                //        total = total + bolength;
                //        if (total >= LengthOverallInM) {
                //            self.berthpreschedulingModel().ToBollardCode(bollard.BollardCode);
                //            bollardstop = true;
                //        }
                //    }
                //    else if ((parseFloat(bolength) > parseFloat(LengthOverallInM)) && bollardstop == false) {
                //        self.berthpreschedulingModel().ToBollardCode(bollard.BollardCode);
                //        bollardstop = true;
                //    }
                //});

                self.berthpreschedulingModel().Bollards([]);

                self.viewModelHelper.apiGet('api/BollardsInBerth/' + dataItem.PortCode + '/' + dataItem.QuayCode + '/' + dataItem.BerthCode, null,
                    function (result) {
                        self.berthpreschedulingModel().Bollards(result);
                    }, null, null, false);
            }
            else {
                self.SelectBollards(false);
            }
        }

        self.BollardChange = function (e) {
           
            $('#spanbollard').hide();
            var dataItem = this.dataItem(e.item.index());

            if (dataItem.BollardCode != "") {
                self.berthpreschedulingModel().FromBollardCode(dataItem.BollardCode);
                self.berthpreschedulingModel().FromBollardMeter(dataItem.FromMeter);
            }
            else {
            }
        }

        ValidDate = function (data, event) {
            self.SelectBollards(false);
            self.berthpreschedulingModel().FromBollardCode("");
            var some = JSON.parse(ko.toJSON(data));
            var startDate = ETB.value;
            var endDate = ETUB.value;
            self.berthpreschedulingModel().SheduledBerth("");
            self.EnableGetSuitable(true);
            self.EnableGetSuitableBerths(false);
            if (startDate) {
                self.berthpreschedulingModel().ETB(startDate);
                $("#ETUB").kendoDateTimePicker({
                    min: startDate,
                    format: "yyyy-MM-dd HH:mm",
                    parseFormats: ["yyyy-MM-dd", "HH:mm"],
                    timeFormat: "HH:mm",
                    month: { empty: '<span class=k-state-disabled>#= data.value #</span>' }
                });
            }
            if (endDate) {
                self.berthpreschedulingModel().ETUB(endDate);
            }
        }



        SearchValidDate = function (data, event) {

            var some = JSON.parse(ko.toJSON(data));
            var startDate = SearchETAFrom.value;
            var endDate = SearchETATo.value;

            if (startDate) {
                self.berthpreschedulingModel().ETA(startDate);
                $("#SearchETATo").kendoDatePicker({
                    min: startDate,
                    format: "yyyy-MM-dd",
                    parseFormats: ["yyyy-MM-dd"]
                });
            }
            if (endDate) {
                self.berthpreschedulingModel().ETD(endDate);
            }
        }


        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }



        self.CancelClick = function () {
            self.viewMode('List');
            self.berthpreschedulingModel().reset();


        }
        self.Initialize();
    }
    IPMSRoot.BerthPreSchedulingViewModel = BerthPreSchedulingViewModel;

}(window.IPMSROOT));