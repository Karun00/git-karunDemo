(function (IPMSRoot) {
    var ServiceRequestViewModel = function (servicerequestid, viewDetail) {
        var self = this;
        $('#spnTitle').html("Service Requests");

        self.arrivalNotificationReferenceData = ko.observable();
        self.warpingBollard = ko.observable();
        self.getVCNDtls = ko.observable();
        self.currentBerthnBollardData = ko.observable();


        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.pasenVisible = ko.observable(false);
        self.documentVisible = ko.observable(false);
        self.servicerequestModel = ko.observable();
        self.LoadServiceDetails = ko.observable();
        self.printView = ko.observable(false);
        self.getMovementTypes = ko.observableArray();
        self.berthsData = ko.observableArray([]);
        self.berthsList = ko.observableArray();

        self.getAllberths = ko.observableArray();
        self.bollardValues = ko.observableArray();
        self.isBerthChanged = ko.observable(true);

        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.AddDoc = ko.observable();

        self.MovementtypesArray = ko.observableArray();
        self.sidealongSides = ko.observable();
        self.getSideAlongSideArray = ko.observableArray();
        self.serviceRequests = ko.observableArray();
        self.selectedServiceRequest = ko.observable();
        self.LoadvcnValues = ko.observableArray();
        self.WarpingBollardsData = ko.observable();
        self.berthMasterListdata = ko.observableArray();
        self.ServiceRequestsData = ko.observableArray();
        self.getBerthArray = ko.observable();
        self.selectedBerth = ko.observableArray();
        self.WarpingbollardValues = ko.observableArray([]);
        self.getCurrentBerthsnBollards = ko.observableArray();
        self.vcnlocal = ko.observable("");
        self.vcntype = ko.observable("");

        self.ETAmin = ko.observable();
        self.ETDmax = ko.observable();
        self.validationHelper = new IPMSROOT.validationHelper();
        self.serviceRequestModelGrid = ko.observable(new IPMSROOT.ServiceRequestModelGrid());

        self.BerthName = ko.observable();
        self.Bollards = ko.observable();
        $('#divBerthName').hide();
        $('#divBollards').hide();
        self.isEnableBtn = ko.observable(true);

        self.MovementStartTime = ko.observable("");
        self.PreferredStartTime = ko.observable("");

        self.ismultiplepfileToUpload = ko.observable(false);

        ///////Mahesh : For Loading Berths //////////////////
        self.LoadBerths = function () {
            self.viewModelHelper.apiGet('api/ServiceRequestBerths',
           null,
              function (result) {

                  self.berthsData(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.Berth(item);
                  }));

                  //commented by garima - with this pending task workflow action is giving some problms
                  // self.LoadServiceRequestDetails();
              }, null, null, false);

        }

        ////  Mahesh : For Initialization /////////////////////////
        self.Initialize = function () {

            self.viewMode = ko.observable(true);
            self.servicerequestModel(new IPMSROOT.ServiceRequestModel());
            self.LoadMovementTypes();
            self.LoadBerths();

            self.serviceRequestModelGrid(new IPMSRoot.ServiceRequestModelGrid(undefined));

            self.LoadBollardsForWarping();
            self.LoadServiceRequestDetails();
            if (viewDetail == true) {

            }
            else {
                self.viewMode('List');
            }


            self.IsCodeEnable(true);
        }

        ///////Mahesh : For Loading VCN's List to bind to autocomplete ////////////////////

        self.LoadVCNDetails = function () {
            self.viewModelHelper.apiGet('api/ServiceRequest/GetVCNDetailsForServiceRequest', null,
      function (result1) {

          self.getVCNDtls(new IPMSRoot.vesselModel(result1));
      }, null, null, false);
        }

        /////////mahesh : To select item from autom complete ......................
        self.eventVCN = function (model) {

            if (self.servicerequestModel().MovementType() != "SHMV") {
                if (model.IsPHANFinal != "Y") {
                    toastr.error("Port Health Officer Approval Is In Process For VCN : " + model.VCN, "Service Request");
                    model.VCN = '';
                    self.servicerequestModel().VesselData('');
                    self.servicerequestModel().UserName('');
                    self.servicerequestModel().ContactNo('');
                    self.servicerequestModel().EmailID('');
                    self.servicerequestModel().VCN(model.VCN);
                    return;
                }
                if (model.IsISPSANFinal == "N") {
                    toastr.error("ISPSApproval Is In Process For VCN : " + model.VCN, "Service Request");
                    model.VCN = '';
                    self.servicerequestModel().VesselData('');
                    self.servicerequestModel().UserName('');
                    self.servicerequestModel().ContactNo('');
                    self.servicerequestModel().VCN(model.VCN);
                    self.servicerequestModel().EmailID('');
                    return;
                }
                if (model.IsIMDGANFinal == "N") {
                    toastr.error("IMDG Approval Is In Process For VCN : " + model.VCN, "Service Request");
                    model.VCN = '';
                    self.servicerequestModel().VesselData('');
                    self.servicerequestModel().UserName('');
                    self.servicerequestModel().ContactNo('');
                    self.servicerequestModel().EmailID('');
                    self.servicerequestModel().VCN(model.VCN);
                    return;
                }
                if (model.Tidal == "Yes") {
                    if (model.TidalStatus == "No") {
                        toastr.error("DHM Approval Is In Process For VCN : " + model.VCN, "Service Request");
                        model.VCN = '';
                        self.servicerequestModel().VesselData('');
                        self.servicerequestModel().UserName('');
                        self.servicerequestModel().ContactNo('');
                        self.servicerequestModel().EmailID('');
                        self.servicerequestModel().VCN(model.VCN);
                        return;
                    }
                }
            }

        }



        self.VesselSelect = function (e) {

            var dataItem = this.dataItem(e.item.index());


            if (self.servicerequestModel().MovementType() == 'ARMV') {
                if (dataItem.Tidal == 'Yes' || dataItem.IsSpecialNature == 'Yes') {
                    self.documentVisible(true);
                }
                else {
                    self.documentVisible(false);
                }
            } else {
                if (dataItem.IsSpecialNature == 'Yes') {
                    self.documentVisible(true);
                }
                else {
                    self.documentVisible(false);
                }
            }


            if (self.servicerequestModel().MovementType() == "" || self.servicerequestModel().MovementType() == undefined) {
                toastr.error("Please select Movement Type");
                self.servicerequestModel().VesselData('');
                self.servicerequestModel().UserName('');
                self.servicerequestModel().ContactNo('');
                self.servicerequestModel().EmailID('');
                self.servicerequestModel().VCN('');
                dataItem.VCN = '';
                console.log(self.servicerequestModel().VCN(), 'VCN');
                console.log(dataItem.VCN, 'dataVCN');
            }
            else {
                dataItem.ETA = moment(dataItem.ETA).format('YYYY-MM-DD HH:mm');
                dataItem.ETD = moment(dataItem.ETD).format('YYYY-MM-DD HH:mm');
                self.servicerequestModel().VCN(dataItem.VCN);
                self.servicerequestModel().VesselType(dataItem.VesselTypeCode);
                self.servicerequestModel().VesselData(dataItem);
                self.eventVCN(dataItem);
                if (self.arrivalNotificationReferenceData().UserDetails().length > 0) {
                    self.servicerequestModel().UserName(self.arrivalNotificationReferenceData().UserDetails()[0].UserName());
                    self.servicerequestModel().ContactNo(self.arrivalNotificationReferenceData().UserDetails()[0].ContactNo());
                    self.servicerequestModel().EmailID(self.arrivalNotificationReferenceData().UserDetails()[0].EmailID());
                }
            }

            if (self.servicerequestModel().MovementType() == "ARMV") {
                if (dataItem.Tidal == 'Yes') {
                    self.servicerequestModel().IsTidal(true);
                }
                else {
                    self.servicerequestModel().IsTidal(false);
                }
            }

            if (self.servicerequestModel().VesselType() == 'V010') {
                self.pasenVisible(true);
            }
            else {
                self.pasenVisible(false);
            }


            var VCN = dataItem.VCN;
            if (dataItem.VCN != "" && self.servicerequestModel().MovementType() != undefined) {
                self.viewModelHelper.apiGet('api/ServiceRequestsGC', { vcn: VCN },
                function (result) {
                    ko.mapping.fromJS(result[0], {}, self.currentBerthnBollardData);
                    if (result.length != 0) {
                        self.BerthName('NA');
                        self.Bollards('NA');
                        var bollards = '';
                        $('#divBerthName').hide();
                        $('#divBollards').hide();
                        if (self.servicerequestModel().MovementType() != "ARMV" && result[0].ATB != null) {
                            self.servicerequestModel().CurrentBerth(result[0].CurrentBerth);
                            self.servicerequestModel().CurrentFromBollardName(result[0].CurrentFromBollardName);
                            self.servicerequestModel().CurrentToBollardName(result[0].CurrentToBollardName);
                            self.servicerequestModel().CurrentBerthCode(result[0].CurrentBerthCode);
                        } else if (self.servicerequestModel().MovementType() == "ARMV") {
                            $('#divBerthName').show();
                            $('#divBollards').show();
                            self.BerthName(result[0].AllocatedBerth);
                            bollards = ((result[0].AllocatedFromBollardName != '' || result[0].AllocatedFromBollardName != undefined) ? result[0].AllocatedFromBollardName : 'NA') + ' To ' + ((result[0].AllocatedToBollardName != '' || result[0].AllocatedToBollardName != undefined) ? result[0].AllocatedToBollardName : 'NA');
                            self.Bollards(bollards);
                        }
                        if (self.arrivalNotificationReferenceData().UserDetails().length > 0) {
                            self.servicerequestModel().UserName(self.arrivalNotificationReferenceData().UserDetails()[0].UserName());
                            self.servicerequestModel().ContactNo(self.arrivalNotificationReferenceData().UserDetails()[0].ContactNo());
                            self.servicerequestModel().EmailID(self.arrivalNotificationReferenceData().UserDetails()[0].EmailID());
                        }
                    }
                });
            }

            if (self.ETAmin == "") {
                self.ETAmin = self.servicerequestModel().VesselData().ETA();
                self.ETDmax = self.servicerequestModel().VesselData().ETD();
            }
            else {
                self.ETAmin = self.servicerequestModel().VesselData().ETA;
                self.ETDmax = self.servicerequestModel().VesselData().ETD;
            }

            var myDatePicker = new Date(self.ETAmin);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();

            var myDatePicker = new Date(self.ETDmax);
            var day1 = myDatePicker.getDate();
            var month1 = myDatePicker.getMonth();
            var year1 = myDatePicker.getFullYear();

            $("#Datepicker").data('kendoDatePicker').min(new Date(year, month, day));
            $("#Datepicker").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#Datepicker1").data('kendoDatePicker').min(new Date(year, month, day));
            $("#Datepicker1").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#Datepicker2").data('kendoDatePicker').min(new Date(year, month, day));
            $("#Datepicker2").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#Datepicker3").data('kendoDatePicker').min(new Date(year, month, day));
            $("#Datepicker3").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#PreferredDate").data('kendoDatePicker').min(new Date(year, month, day));
            $("#PreferredDate").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#PreferredDate1").data('kendoDatePicker').min(new Date(year, month, day));
            $("#PreferredDate1").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#PreferredDate2").data('kendoDatePicker').min(new Date(year, month, day));
            $("#PreferredDate2").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#PreferredDate3").data('kendoDatePicker').min(new Date(year, month, day));
            $("#PreferredDate3").data('kendoDatePicker').max(new Date(year1, month1, day1));

        }





        VCNKeyDown = function () {
            self.servicerequestModel().VesselData('');
            self.BerthName('');
            self.Bollards('');
            self.servicerequestModel().MovementDateTime('');
            self.servicerequestModel().PreferredDateTime('');
            self.servicerequestModel().SlotPeriod('');
            self.servicerequestModel().MovementSlot('');
            self.servicerequestModel().SideAlongSideCode('');
            self.servicerequestModel().DraftFWD('');
            self.servicerequestModel().DraftAFT('');
            self.servicerequestModel().Comments('');
            self.servicerequestModel().CurrentBerth('');
            self.servicerequestModel().CurrentFromBollardName('');
            self.servicerequestModel().CurrentToBollardName('');
            self.servicerequestModel().BerthKey('');
            self.servicerequestModel().WarpDistance('');
            self.servicerequestModel().Warp('');
            self.servicerequestModel().UserName('');
            self.servicerequestModel().ContactNo('');
            self.servicerequestModel().EmailID('');
        }


        self.eventCurrentdataNew = function (model) {
            self.servicerequestModel().VesselData('');
            self.servicerequestModel().VCN('');
            self.servicerequestModel().OwnSteam('');
            self.servicerequestModel().IsTidal('');
            self.servicerequestModel().NoMainEngine('');
            self.servicerequestModel().UserName('');
            self.servicerequestModel().ContactNo('');
            self.servicerequestModel().EmailID('');
            self.documentVisible(false);
            self.eventCurrentdata(model);
        }

        self.eventCurrentdata = function (model) {

            if (model.VesselType() == 'V010') {
                self.pasenVisible(true);
            }
            else {
                self.pasenVisible(false);
            }



            var VCN = model.VCN;
            var Mtype = model.MovementType;

            if (VCN() != "" && Mtype() != undefined) {
                self.viewModelHelper.apiGet('api/ServiceRequestsGC', { vcn: VCN },
                function (result) {
                    ko.mapping.fromJS(result[0], {}, self.currentBerthnBollardData);
                    if (result.length != 0) {
                        self.BerthName('NA');
                        self.Bollards('NA');
                        var bollards = '';
                        $('#divBerthName').hide();
                        $('#divBollards').hide();
                        if (Mtype() != "ARMV" && result[0].ATB != null) {
                            self.servicerequestModel().CurrentBerth(result[0].CurrentBerth);
                            self.servicerequestModel().CurrentFromBollardName(result[0].CurrentFromBollardName);
                            self.servicerequestModel().CurrentToBollardName(result[0].CurrentToBollardName);
                            self.servicerequestModel().CurrentBerthCode(result[0].CurrentBerthCode);
                        } else if (Mtype() == "ARMV") {
                            $('#divBerthName').show();
                            $('#divBollards').show();
                            self.BerthName(result[0].AllocatedBerth);
                            bollards = ((result[0].AllocatedFromBollardName != '' || result[0].AllocatedFromBollardName != undefined) ? result[0].AllocatedFromBollardName : 'NA') + ' To ' + ((result[0].AllocatedToBollardName != '' || result[0].AllocatedToBollardName != undefined) ? result[0].AllocatedToBollardName : 'NA');
                            self.Bollards(bollards);
                        }
                    }
                });
            }

            if (Mtype() != "ARMV") {
                $('#divBerthName').hide();
                $('#divBollards').hide();
            }

            if (self.ETAmin == "") {
                self.ETAmin = model.VesselData().ETA();
                self.ETDmax = model.VesselData().ETD();
            }
            else {
                self.ETAmin = model.VesselData().ETA;
                self.ETDmax = model.VesselData().ETD;
            }

            var myDatePicker = new Date(self.ETAmin);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();

            var myDatePicker = new Date(self.ETDmax);
            var day1 = myDatePicker.getDate();
            var month1 = myDatePicker.getMonth();
            var year1 = myDatePicker.getFullYear();


            $("#Datepicker").data('kendoDatePicker').min(new Date(year, month, day));
            $("#Datepicker").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#Datepicker1").data('kendoDatePicker').min(new Date(year, month, day));
            $("#Datepicker1").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#Datepicker2").data('kendoDatePicker').min(new Date(year, month, day));
            $("#Datepicker2").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#Datepicker3").data('kendoDatePicker').min(new Date(year, month, day));
            $("#Datepicker3").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#PreferredDate").data('kendoDatePicker').min(new Date(year, month, day));
            $("#PreferredDate").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#PreferredDate1").data('kendoDatePicker').min(new Date(year, month, day));
            $("#PreferredDate1").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#PreferredDate2").data('kendoDatePicker').min(new Date(year, month, day));
            $("#PreferredDate2").data('kendoDatePicker').max(new Date(year1, month1, day1));

            $("#PreferredDate3").data('kendoDatePicker').min(new Date(year, month, day));
            $("#PreferredDate3").data('kendoDatePicker').max(new Date(year1, month1, day1));


        }
        self.viewModelHelper.apiGet('api/ReportBuilder/GetReportCategory',
              null,
                function (result) {
                    ko.mapping.fromJS(result, {}, self.reportCategoryList);
                });
        ////////////Mahesh : To load grid details in service request ....................

        self.LoadServiceRequestDetails = function () {

            if (viewDetail == true) {

                var s1 = servicerequestid;

                servicerequestid = s1.split("x")[0];

                self.vcntype = s1.split("x")[1];
               
                self.viewModelHelper.apiGet('api/ServiceRequests/' + servicerequestid,
                 { serviceid: servicerequestid },
                  function (result) {
                      self.LoadServiceDetails(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.ServiceRequestModel(item, self.berthsData());
                      }));
                      self.viewservReqst(self.LoadServiceDetails()[0]);


                  });

            }
            else {

                var frmdate = self.serviceRequestModelGrid().MovementFrom();
                var todate = self.serviceRequestModelGrid().MovementTo();

                var vcnSearch = self.serviceRequestModelGrid().VCN();
                var vesselName = self.serviceRequestModelGrid().VesselName();
                var MovementType = self.serviceRequestModelGrid().MovementType();

                if (vcnSearch == "") {
                    vcnSearch = "All";
                }
                if (vesselName == "") {
                    vesselName = "All";
                }
                if (MovementType == "" || MovementType == undefined) {
                    MovementType = "All";
                }
               
                self.viewModelHelper.apiGet('api/ServiceRequests/' + frmdate + '/' + todate + '/' + vcnSearch + '/' + vesselName + '/' + MovementType,
          null,
           function (result) {

               //var minutes = result.MovementDateTime.getMinutes;
               //minutes = minutes - 1;
               //result.MovementDateTime.setMinutes(minutes);
               self.LoadServiceDetails(ko.utils.arrayMap(result, function (item) {
                   return new IPMSRoot.ServiceRequestModel(item, self.berthsData());

               }));

           });
            }

        }


        SearchMovementtoCal = function () {
            this.min($("#SearchMovementFrom").val());
            var myDatePicker = new Date($("#SearchMovementFrom").val());
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth() + 2;
            var year = myDatePicker.getFullYear();
            this.max(new Date(year, month, day));

        }

        SearchValidDate = function (data, event) {
            self.serviceRequestModelGrid().MovementTo(self.serviceRequestModelGrid().MovementFrom());
        }


        self.GetDataClick = function (model) {

            var frmdt = self.serviceRequestModelGrid().MovementFrom();
            var todt = self.serviceRequestModelGrid().MovementTo();

            frmdate = moment(frmdt).format('YYYY-MM-DD');
            todate = moment(todt).format('YYYY-MM-DD');

            var vcnSearch = self.serviceRequestModelGrid().VCN();
            var vesselName = self.serviceRequestModelGrid().VesselName();
            var MovementType = self.serviceRequestModelGrid().MovementType();

            if (vcnSearch == "") {
                vcnSearch = "All";
            }
            if (vesselName == "") {
                vesselName = "All";
            }
            if (MovementType == "" || MovementType == undefined) {
                MovementType = "All";
            }


            self.viewModelHelper.apiGet('api/ServiceRequests/' + frmdate + '/' + todate + '/' + vcnSearch + '/' + vesselName + '/' + MovementType,
          null,
        function (result) {

            self.LoadServiceDetails(ko.utils.arrayMap(result, function (item) {
                return new IPMSRoot.ServiceRequestModel(item, self.berthsData());
            }));
        });

        }


        self.ClearFilter = function () {

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate() + 30);
            fromdate.setDate(fromdate.getDate() - 30);


            self.serviceRequestModelGrid().MovementFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            self.serviceRequestModelGrid().MovementTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");


            self.serviceRequestModelGrid().VCN('');
            self.serviceRequestModelGrid().VesselName('');
            self.serviceRequestModelGrid().MovementType('');
            self.LoadServiceRequestDetails();
        }

        PreferredSlot = function (model) {

            var preferredDate = moment(model.PreferredDateTime()).format('YYYY-MM-DD hh:mm:ss A')
            self.viewModelHelper.apiGet('api/ServiceRequestPreferredSlot', { PreferredDate: preferredDate },
                function (result1) {

                    self.servicerequestModel().SlotPeriod(result1.SlotPeriod);
                }, null, null, false);
        }

        callMovement = function (model) {


            var ETAFrom = new Date(self.ETAmin);
            var day = ETAFrom.getDate();
            var month = ETAFrom.getMonth();
            var year = ETAFrom.getFullYear();

            var ETDTo = new Date(self.ETDmax);
            var day1 = ETDTo.getDate();
            var month1 = ETDTo.getMonth();
            var year1 = ETDTo.getFullYear();

            var CurrentDate = new Date();
            var cday = CurrentDate.getDate();
            var cmonth = CurrentDate.getMonth();
            var cyear = CurrentDate.getFullYear();


            if (ETAFrom > CurrentDate && ETDTo < CurrentDate) {
                $("#Datepicker").data('kendoDatePicker').min(new Date(year, month, day));
                $("#Datepicker").data('kendoDatePicker').max(new Date(cyear, cmonth, cday));
            }
            else {
                $("#Datepicker").data('kendoDatePicker').min(new Date(cyear, cmonth, cday));
                $("#Datepicker").data('kendoDatePicker').max(new Date(year1, month1, day1));
            }
        }

        callPrefferedSlot = function (model) {

            var ETAFrom = new Date(self.ETAmin);
            var day = ETAFrom.getDate();
            var month = ETAFrom.getMonth();
            var year = ETAFrom.getFullYear();

            var ETDTo = new Date(self.ETDmax);
            var day1 = ETDTo.getDate();
            var month1 = ETDTo.getMonth();
            var year1 = ETDTo.getFullYear();

            var CurrentDate = new Date();
            var cday = CurrentDate.getDate();
            var cmonth = CurrentDate.getMonth();
            var cyear = CurrentDate.getFullYear();


            if (ETAFrom > CurrentDate && ETDTo < CurrentDate) {
                $("#PreferredDate").data('kendoDatePicker').min(new Date(year, month, day));
                $("#PreferredDate").data('kendoDatePicker').max(new Date(cyear, cmonth, cday));
            }
            else {
                $("#PreferredDate").data('kendoDatePicker').min(new Date(cyear, cmonth, cday));
                $("#PreferredDate").data('kendoDatePicker').max(new Date(year1, month1, day1));
            }
        }
       
        //srinivas
        self.MovementSlotChange = function (e) {

            var dataItem = this.dataItem(e.item.index());
            if (dataItem.EndTime != undefined) {
                if (dataItem.EndTime == "0") {
                    //  self.MovementStartTime("1441");
                    self.MovementStartTime("1441");
                    //self.MovementStartTime("1439");
                    
                    
                }
                else if (dataItem.EndTime == "60") {
                    self.MovementStartTime("1500");
                }
                else {
                    self.MovementStartTime(dataItem.EndTime + 1);
                }
                if (self.servicerequestModel().SlotPeriod() == "" || self.servicerequestModel().SlotPeriod() == "Choose....") {
                    self.PreferredStartTime(dataItem.StartTime + 1);
                }
            }

            if (self.servicerequestModel().SlotPeriod() == "" || self.servicerequestModel().SlotPeriod() == "Choose....") {
                self.servicerequestModel().SlotPeriod(dataItem.SlotPeriod);
            }
        }


        self.PrefferedSlotChange = function (e) {
            var dataItem = this.dataItem(e.item.index());
            if (dataItem.StartTime != undefined) {
                self.PreferredStartTime(dataItem.StartTime + 1);
            }
            //if (dataItem.EndTime != undefined) {
            //    self.PreferredStartTime(dataItem.EndTime - 1);
            //}
            //if(dataItem.EndTime == 0)
            //{
            //    self.PreferredStartTime(1440 - 1);
            //}
            //if (dataItem.EndTime > dataItem.StartTime) {
            //    self.PreferredStartTime(1440 - 1);           
            //}

        }

        self.EditSlotBlockingDate = function (model) {
            self.CurrentDate = ko.observable();
            self.CurrentDate(new Date());

            var allSlots = self.arrivalNotificationReferenceData().AllSlots();

            var MovementDate = new Date(model.MovementDateTime());
            MovementDate.setHours(0);
            MovementDate.setMinutes(0);

            var reasons;
            if (self.IsSave()) { reasons = model.VesselData().Reasons; }
            else { reasons = model.VesselData().Reasons(); }

            var bunkering = false;
            var allReasons = reasons.split(',');

            $.each(allReasons, function (key, item) {
                if (item == "BUNK") {
                    bunkering = true;
                    return;
                }
            });

            if (allReasons.length > 1) {
                bunkering = false;
            }

            if (MovementDate <= self.CurrentDate()) {
                
                self.arrivalNotificationReferenceData().MovementSlots.removeAll();

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();

                    //if (bunkering == true) {
                    //    debugger;
                    //    if (item.StartTime() <= totalminutes && totalminutes <= item.EndTime()) {
                    //        AddSlot.SlotPeriod(item.SlotPeriod());
                    //        AddSlot.StartTime(item.StartTime());
                    //        AddSlot.EndTime(item.EndTime());
                    //        self.arrivalNotificationReferenceData().MovementSlots.push(AddSlot);                         

                    //    }
                    //}
                    if (totalminutes < item.StartTime() || (totalminutes >= item.StartTime() && totalminutes <= item.EndTime())) {                 //by anjani
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        self.arrivalNotificationReferenceData().MovementSlots.push(AddSlot);
                    }
                });

            }
            else {
                self.arrivalNotificationReferenceData().MovementSlots.removeAll();

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();
                    MovementDate.setHours(0);
                    MovementDate.setMinutes(0);
                    var minutes = item.StartTime();
                    MovementDate.setMinutes(minutes);
                    if (MovementDate < new Date(self.ETDmax)) {
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        self.arrivalNotificationReferenceData().MovementSlots.push(AddSlot);
                    }

                });
            }
            if (model.PreferredDateTime() != null) {
                var PrefferedDate = new Date(model.PreferredDateTime());
                PrefferedDate.setHours(0);
                PrefferedDate.setMinutes(0);

                if (PrefferedDate <= self.CurrentDate()) {
                    self.arrivalNotificationReferenceData().Slots.removeAll();

                    $.each(allSlots, function (key, item) {

                        var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                        var AddSlot = new IPMSROOT.SlotType();

                        if (bunkering == true) {
                            
                            if (item.StartTime() <= totalminutes && totalminutes <= item.EndTime()) {
                                AddSlot.SlotPeriod(item.SlotPeriod());
                                AddSlot.StartTime(item.StartTime());
                                AddSlot.EndTime(item.EndTime());
                                self.arrivalNotificationReferenceData().Slots.push(AddSlot);
                            }
                        }
                        if (totalminutes < item.StartTime()) {
                            AddSlot.SlotPeriod(item.SlotPeriod());
                            AddSlot.StartTime(item.StartTime());
                            AddSlot.EndTime(item.EndTime());
                            self.arrivalNotificationReferenceData().Slots.push(AddSlot);
                        }
                    });
                }
                else {
                    self.arrivalNotificationReferenceData().Slots.removeAll();
                    $.each(allSlots, function (key, item) {
                        var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                        var AddSlot = new IPMSROOT.SlotType();
                        PrefferedDate.setHours(0);
                        PrefferedDate.setMinutes(0);
                        var minutes = item.StartTime();
                        PrefferedDate.setMinutes(minutes);
                        if (PrefferedDate < new Date(self.ETDmax)) {
                            AddSlot.SlotPeriod(item.SlotPeriod());
                            AddSlot.StartTime(item.StartTime());
                            AddSlot.EndTime(item.EndTime());
                            self.arrivalNotificationReferenceData().Slots.push(AddSlot);
                        }
                    });
                }
            }
        }



        SlotBlockingDate = function (model) {
            self.CurrentDate = ko.observable();
            self.CurrentDate(new Date());

            var allSlots = self.arrivalNotificationReferenceData().AllSlots();

            var MovementDate = self.servicerequestModel().MovementDateTime();
            MovementDate.setHours(0);
            MovementDate.setMinutes(0);

            if (MovementDate <= self.CurrentDate()) {

                var reasons;
                if (self.IsSave()) { reasons = model.VesselData().Reasons; }
                else { reasons = model.VesselData().Reasons(); }

                var bunkering = false;
                var allReasons = reasons.split(',');

                $.each(allReasons, function (key, item) {
                    if (item == "BUNK") {
                        bunkering = true;
                        return;
                    }
                });

                if (allReasons.length > 1) {
                    bunkering = false;
                }

                self.arrivalNotificationReferenceData().MovementSlots.removeAll();
                self.arrivalNotificationReferenceData().Slots.removeAll();
                self.servicerequestModel().MovementSlot("");
                self.servicerequestModel().SlotPeriod("");

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();

                    //if (bunkering == true) {
                       
                    //    if (item.StartTime() <= totalminutes && totalminutes <= item.EndTime()) {
                    //        AddSlot.SlotPeriod(item.SlotPeriod());
                    //        AddSlot.StartTime(item.StartTime());
                    //        AddSlot.EndTime(item.EndTime());
                    //        self.arrivalNotificationReferenceData().MovementSlots.push(AddSlot);
                    //        self.arrivalNotificationReferenceData().Slots.push(AddSlot);

                    //    }
                    //}

                    if (totalminutes < item.StartTime() || (totalminutes >= item.StartTime() && totalminutes <= item.EndTime()))  //by anjani
                    {
                        
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        self.arrivalNotificationReferenceData().MovementSlots.push(AddSlot);
                        self.arrivalNotificationReferenceData().Slots.push(AddSlot);

                    }

                });

                var prefferedDT = moment(MovementDate).format('YYYY-MM-DD HH:mm');
                if (self.servicerequestModel().PreferredDateTime() == "") {
                    self.servicerequestModel().PreferredDateTime(prefferedDT);
                }
            }
            else {
                self.arrivalNotificationReferenceData().MovementSlots.removeAll();
                self.arrivalNotificationReferenceData().Slots.removeAll();
                self.servicerequestModel().MovementSlot("");
                self.servicerequestModel().SlotPeriod("");

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();
                    MovementDate.setHours(0);
                    MovementDate.setMinutes(0);
                    var minutes = item.StartTime();
                    MovementDate.setMinutes(minutes);
                    if (MovementDate < new Date(self.ETDmax)) {
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        self.arrivalNotificationReferenceData().MovementSlots.push(AddSlot);
                        self.arrivalNotificationReferenceData().Slots.push(AddSlot);
                    }

                });
                var prefferedDT = moment(MovementDate).format('YYYY-MM-DD HH:mm');
                if (self.servicerequestModel().PreferredDateTime() == "") {
                    self.servicerequestModel().PreferredDateTime(prefferedDT);
                }
            }
        }


        PreSlotBlockingDate = function (model) {
            self.CurrentDate = ko.observable();
            self.CurrentDate(new Date());

            var allSlots = self.arrivalNotificationReferenceData().AllSlots();

            var PrefferedDate = self.servicerequestModel().PreferredDateTime();
            PrefferedDate.setHours(0);
            PrefferedDate.setMinutes(0);

            if (PrefferedDate <= self.CurrentDate()) {

                var reasons;
                if (self.IsSave()) { reasons = model.VesselData().Reasons; }
                else { reasons = model.VesselData().Reasons(); }

                var bunkering = false;
                var allReasons = reasons.split(',');

                $.each(allReasons, function (key, item) {
                    if (item == "BUNK") {
                        bunkering = true;
                        return;
                    }
                });

                if (allReasons.length > 1) {
                    bunkering = false;
                }

                self.arrivalNotificationReferenceData().Slots.removeAll();
                self.servicerequestModel().SlotPeriod("");

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();

                    if (bunkering == true) {
                    
                        if (item.StartTime() <= totalminutes && totalminutes <= item.EndTime()) {
                            AddSlot.SlotPeriod(item.SlotPeriod());
                            AddSlot.StartTime(item.StartTime());
                            AddSlot.EndTime(item.EndTime());
                            self.arrivalNotificationReferenceData().Slots.push(AddSlot);

                        }
                    }

                    if (totalminutes < item.StartTime()) {
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        self.arrivalNotificationReferenceData().Slots.push(AddSlot);
                    }
                });
            }
            else {
                self.arrivalNotificationReferenceData().Slots.removeAll();
                self.servicerequestModel().SlotPeriod("");
                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();
                    PrefferedDate.setHours(0);
                    PrefferedDate.setMinutes(0);
                    var minutes = item.StartTime();
                    PrefferedDate.setMinutes(minutes);
                    if (PrefferedDate < new Date(self.ETDmax)) {
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        self.arrivalNotificationReferenceData().Slots.push(AddSlot);
                    }
                });
            }
        }

        ////////// Mahesh : To Load Bollards for Warping ..............................  

        self.LoadBollardsForWarping = function () {
            self.temp = ko.observable();
            self.temp = self.servicerequestModel().CurrentBerthCode;
            if (self.temp == undefined) {
                self.temp = null;
            }
            self.viewModelHelper.apiGet('api/ServiceRequestsGB', { BerthCode: self.temp },
                     function (result1) {
                         self.warpingBollard(new IPMSRoot.WarpingBollard(result1));
                     }, null, null, false);

        }

        ///////// Mahesh : TO load reference data like Movement Types data and Side along side data......

        self.LoadMovementTypes = function () {
            self.viewModelHelper.apiGet('api/GetReferenceData', null,
                  function (result1) {
                      self.arrivalNotificationReferenceData(new IPMSRoot.ArrivalNotificationReferenceData(result1));

                  }, null, null, false);
        }

        ////////Mahesh : To upload Documents for sailing .............................

        var uploadedFiles = [];
        var documentData = [];
        self.uploadFile = function () {
            var documentType = $('#selUploadDocs option:selected').text();
            uploadedFiles = self.servicerequestModel().UploadedFiles();
            var opmlFile = $('#fileToUpload')[0];
            if (opmlFile.files.length > 0) {
                if (opmlFile.files.length > 1) {
                    toastr.error("Please upload only one document", "Error");
                    return;
                }
                else {
                    for (var i = 0; i < opmlFile.files.length; i++) {
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
                            self.servicerequestModel().UploadedFiles(uploadedFiles);
                        }
                        else {
                            toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
                            return;
                        }
                    }
                }

                var formData = new FormData();
                $.each(self.servicerequestModel().UploadedFiles(), function (key, val) {
                    formData.append(val.name, val.FileDetails);
                });

                self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=Doc1', formData, function Message(data) {
                    self.servicerequestModel().DocumentID(data[0].DocumentID);
                    self.servicerequestModel().FileName(data[0].FileName);

                });
            }
            else {
                toastr.error("Please select file", "Error");
                return;
            }
            self.servicerequestModel().UploadedFiles([]);
            $('#fileToUpload').val('');
            return;
        }

        self.uploadFileGrid = function () {
            if ($('#selUploadDocs').get(0).selectedIndex == 0) {
                toastr.error("Please select document Type.");
                return;
            } {
                $("#spanHWPSfileToUpload").text("");
                self.ismultiplepfileToUpload(false);
                var documentType = $('#selUploadDocs option:selected').text();

                if (documentType != 'Choose....') {
                    self.servicerequestModel().UploadedFiles([]);
                    uploadedFiles = self.servicerequestModel().UploadedFiles();
                    var opmlFile = $('#fileToUploadGrid')[0];
                    if (opmlFile.files.length > 0) {
                        for (var i = 0; i < opmlFile.files.length; i++) {
                            var match = ko.utils.arrayFirst(self.servicerequestModel().ServiceRequestDocuments(), function (item) {
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
                                    self.servicerequestModel().UploadedFiles(uploadedFiles);
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
                                var Adddoc = new IPMSROOT.ServiceRequestDocument();

                                Adddoc.DocumentID(item.DocumentID);
                                Adddoc.FileName(item.FileName);
                                Adddoc.DocumentName(CategoryName);
                                Adddoc.DocumentCode(CategoryCode);
                                self.servicerequestModel().ServiceRequestDocuments.push(Adddoc);
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
                self.servicerequestModel().UploadedFiles([]);
                $('#fileToUpload').val('');
                return;
            }
        }

        self.DeleteDocumentGrid = function (documentRow) {

            self.servicerequestModel().ServiceRequestDocuments.remove(documentRow);
        }

        self.DeleteDocument = function (model) {

            self.servicerequestModel().DocumentID("");
            self.servicerequestModel().FileName("");
            $("#fileToUpload").text('');
        }


        ////////Mahesh : To save Service Request ...................
       
        self.SaveRequest = function (model) {

            if (model.VCN() != model.VesselData().VCN) {
                toastr.warning("Please reselect the appropriate VCN from the Auto-complete");
                return;
            }
            else {

                if (model != undefined) {
                    if (model.VCN == null)
                    { model.VCN(model.VesselData().VCN); }
                }

                if (model.MovementSlot() == "Choose....") {
                    model.MovementSlot("");
                }

                if (model.SlotPeriod() == "Choose....") {
                    model.SlotPeriod("");
                }

                model.IsValidationEnabled(true);
                self.ServiceValidation = ko.observable(model);
                self.ServiceValidation().errors = ko.validation.group(self.ServiceValidation());
                var errors = self.ServiceValidation().errors().length;

                self.BerthValidation = ko.observable(model);
                self.BerthValidation().errors = ko.validation.group(self.BerthValidation());
                var errors1 = self.BerthValidation().errors().length;
                var errorShift = 0;
                if (model.MovementType() == 'SHMV') {
                    errorShift = errors1;
                    model.DraftFWD = model.DraftFWD_SHMV;
                    model.DraftAFT = model.DraftAFT_SHMV;
                }

                var msg = '';

                var msgIndem = '';
                var msgTida = '';
                var msgSpl = '';

                if (model.MovementType() == "ARMV") {
                    if (model.VesselData().Tidal == 'Yes' || model.VesselData().IsSpecialNature == 'Yes') {
                        if (model.ServiceRequestDocuments().length > 0) {
                            msg = "<p>Please provide below list of documents :</p>";
                            msgIndem = "<p>Indemnity</p>";
                            $.each(model.ServiceRequestDocuments(), function (key, val) {
                                if (val.DocumentCode() == 'SRIN') {
                                    msgIndem = '';
                                }
                            });

                            if (model.VesselData().Tidal == 'Yes') {
                                msgTida = "<p>Tidal Form</p>";
                                $.each(model.ServiceRequestDocuments(), function (key, val) {
                                    if (val.DocumentCode() == 'SRTF') {
                                        msgTida = '';
                                    }
                                });
                            }

                            if (model.VesselData().IsSpecialNature == 'Yes') {
                                msgSpl = "<p>Special Nature Form</p>";
                                $.each(model.ServiceRequestDocuments(), function (key, val) {
                                    if (val.DocumentCode() == 'SRSF') {
                                        msgSpl = ''
                                    }
                                });
                            }
                        }
                        else {
                            errors = errors + 1;
                            toastr.warning("Please upload documents");
                        }
                    }
                }
                else {
                    if (model.IsTidal() || model.VesselData().IsSpecialNature == 'Yes') {
                        if (model.ServiceRequestDocuments().length > 0) {
                            msg = "<p>Please provide below list of documents :</p>";
                            msgIndem = "<p>Indemnity</p>";
                            $.each(model.ServiceRequestDocuments(), function (key, val) {
                                if (val.DocumentCode() == 'SRIN') {
                                    msgIndem = '';
                                }
                            });

                            if (model.IsTidal()) {
                                msgTida = "<p>Tidal Form</p>";
                                $.each(model.ServiceRequestDocuments(), function (key, val) {
                                    if (val.DocumentCode() == 'SRTF') {
                                        msgTida = '';
                                    }
                                });
                            }

                            if (model.VesselData().IsSpecialNature == 'Yes') {
                                msgSpl = "<p>Special Nature Form</p>";
                                $.each(model.ServiceRequestDocuments(), function (key, val) {
                                    if (val.DocumentCode() == 'SRSF') {
                                        msgSpl = ''
                                    }
                                });
                            }
                        }
                        else {
                            errors = errors + 1;
                            toastr.warning("Please upload documents");
                        }
                    }
                }


                if (errors == 0 && errorShift == 0 && msgIndem == '' && msgTida == '' && msgSpl == '') {

                    self.servicerequestModel().IsUpdateMovement(true);
                    var momentDate = new Date(self.servicerequestModel().MovementDateTime());
                    
                    if (momentDate != "" && self.MovementStartTime() != "" ) {
                        momentDate.setHours(0);
                        momentDate.setMinutes(0);
                        momentDate.setMinutes(self.MovementStartTime());
                        self.servicerequestModel().MovementDateTime(momentDate);
                    }
                    //if (momentDate != "" && self.MovementStartTime() != "" && self.MovementStartTime() == '1441') {
                    //    //momentDate.setHours(0);
                    //    //momentDate.setMinutes(0);
                    //    momentDate.setMinutes(self.MovementStartTime()-2);
                    //    self.servicerequestModel().MovementDateTime(momentDate);
                    //}

                    var prefferedDate = new Date(self.servicerequestModel().PreferredDateTime());

                    if (prefferedDate != "" && self.PreferredStartTime() != "") {
                        prefferedDate.setHours(0);
                        prefferedDate.setMinutes(0);
                        prefferedDate.setMinutes(self.PreferredStartTime());
                        self.servicerequestModel().PreferredDateTime(prefferedDate);
                    }

                    var serviceRequestsSailing = [];
                    var serviceRequestsShifting = [];
                    var serviceRequestsWarping = [];
                    var serviceRequestDocumnet = [];


                    if (self.servicerequestModel().MovementType() == 'SGMV') {
                        model.ServiceRequestSailing(new ServiceRequestSailing(0, 0, model.MarineRevenueCleared, model.DocumentID, new ServiceRequestDocument(0, 0, model.FileName, model.FileName, model.FileName)));

                    }
                    if (self.servicerequestModel().MovementType() == 'SHMV') {
                        model.ServiceRequestShifting(new ServiceRequestShifting(0, 0, model.ToPortCode, model.ToQuayCode, model.ToBerthCode, model.BerthKey(), model.DraftFWD_SHMV(), model.DraftAFT_SHMV()));
                    }
                    if (self.servicerequestModel().MovementType() == 'WRMV') {
                        model.ServiceRequestWarping(new ServiceRequestWarping(0, 0, model.Warp, model.WarpDistance));
                    }

                    model.MovementName = $("#movtID option:selected").text();
                    model.VesselName = model.VesselData().VesselName;
                    self.viewModelHelper.apiPost('api/ServiceRequests', ko.mapping.toJSON(model),
                                function Message(data) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Service Request Added Successfully", "ServiceRequest");
                                    self.LoadServiceRequestDetails();
                                    self.viewMode('List');
                                    $('#spnTitle').html("Service Requests");
                                });
                }
                else {
                    self.ServiceValidation().errors.showAllMessages();
                    self.BerthValidation().errors.showAllMessages();
                    $('#divValidationError').removeClass('display-none');
                    if (msgIndem != '' || msgTida != '' || msgSpl != '') {
                        msg = msg + msgIndem + msgTida + msgSpl;
                        toastr.warning(msg);
                    }
                    return;
                }
            }
        }

        ////////mahesh : To Modify Service Request ..................
       
        self.ModifyRequest = function (model) {
            model.IsValidationEnabled(true);

            if (model.MovementSlot() == "Choose....") {
                model.MovementSlot("");
            }

            if (model.SlotPeriod() == "Choose....") {
                model.SlotPeriod("");
            }


            self.ServiceValidation = ko.observable(model);
            self.ServiceValidation().errors = ko.validation.group(self.ServiceValidation());
            var errors = self.ServiceValidation().errors().length;

            var msg = '';

            var msgIndem = '';
            var msgTida = '';
            var msgSpl = '';




            if (model.MovementType() == "ARMV") {
                if (model.VesselData().Tidal() == 'Yes' || model.VesselData().IsSpecialNature() == 'Yes') {
                    if (model.ServiceRequestDocuments().length > 0) {
                        msg = "<p>Please provide below list of documents :</p>";
                        msgIndem = "<p>Indemnity</p>";
                        $.each(model.ServiceRequestDocuments(), function (key, val) {
                            if (val.DocumentCode() == 'SRIN') {
                                msgIndem = '';
                            }
                        });

                        if (model.VesselData().Tidal() == 'Yes') {
                            msgTida = "<p>Tidal Form</p>";
                            $.each(model.ServiceRequestDocuments(), function (key, val) {
                                if (val.DocumentCode() == 'SRTF') {
                                    msgTida = '';
                                }
                            });
                        }

                        if (model.VesselData().IsSpecialNature() == 'Yes') {
                            msgSpl = "<p>Special Nature Form</p>";
                            $.each(model.ServiceRequestDocuments(), function (key, val) {
                                if (val.DocumentCode() == 'SRSF') {
                                    msgSpl = ''
                                }
                            });
                        }
                    }
                    else {
                        errors = errors + 1;
                        toastr.warning("Please upload documents");
                    }
                }
            }
            else {
                if (model.IsTidal() || model.VesselData().IsSpecialNature() == 'Yes') {
                    if (model.ServiceRequestDocuments().length > 0) {
                        msg = "<p>Please provide below list of documents :</p>";
                        msgIndem = "<p>Indemnity</p>";
                        $.each(model.ServiceRequestDocuments(), function (key, val) {
                            if (val.DocumentCode() == 'SRIN') {
                                msgIndem = '';
                            }
                        });

                        if (model.IsTidal()) {
                            msgTida = "<p>Tidal Form</p>";
                            $.each(model.ServiceRequestDocuments(), function (key, val) {
                                if (val.DocumentCode() == 'SRTF') {
                                    msgTida = '';
                                }
                            });
                        }

                        if (model.VesselData().IsSpecialNature() == 'Yes') {
                            msgSpl = "<p>Special Nature Form</p>";
                            $.each(model.ServiceRequestDocuments(), function (key, val) {
                                if (val.DocumentCode() == 'SRSF') {
                                    msgSpl = ''
                                }
                            });
                        }
                    }
                    else {
                        errors = errors + 1;
                        toastr.warning("Please upload documents");
                    }
                }
            }
            //alert(self.MovementStartTime());
            // --new Start

            ko.utils.arrayForEach(self.arrivalNotificationReferenceData().MovementSlots(), function (val) {

                if (val.SlotPeriod() == model.MovementSlot()) {
                    if (val.EndTime() != undefined) {
                        if (val.EndTime() == "0") {
                            self.MovementStartTime("1441");
                        }
                        else if (val.EndTime() == "60") {
                            self.MovementStartTime("1500");
                        }
                        else {
                            self.MovementStartTime(val.EndTime() + 1);
                        }
                    }
                }

            });
            //alert(self.MovementStartTime());
            // --new End
            var momentDate = new Date(self.servicerequestModel().MovementDateTime());
            if (momentDate != "" && self.MovementStartTime() != "") {
                momentDate.setHours(0);
                momentDate.setMinutes(0);
                momentDate.setMinutes(self.MovementStartTime());
                self.servicerequestModel().MovementDateTime(momentDate);
            }

            //if (momentDate != "" && self.MovementStartTime() != "" && self.MovementStartTime() == '1441') {
            //    //momentDate.setHours(0);
            //    //momentDate.setMinutes(0);
            //    momentDate.setMinutes(self.MovementStartTime()-2);
            //    self.servicerequestModel().MovementDateTime(momentDate);
            //}
            var prefferedDate = new Date(self.servicerequestModel().PreferredDateTime());

            if (prefferedDate != "" && self.PreferredStartTime() != "") {
                prefferedDate.setHours(0);
                prefferedDate.setMinutes(0);
                prefferedDate.setMinutes(self.PreferredStartTime());
                self.servicerequestModel().PreferredDateTime(prefferedDate);
            }


            var editMDate = new Date(model.EditMovementDate());
            var MDate = new Date(model.MovementDateTime());

            if (editMDate.valueOf() == MDate.valueOf() && model.EditMovementSlot() == model.MovementSlot()) {
                self.servicerequestModel().IsUpdateMovement(false);
            }
            else {
                self.servicerequestModel().IsUpdateMovement(true);
            }



            if (errors == 0 && msgIndem == '' && msgTida == '' && msgSpl == '') {

                //  model.MovementSlot()


                var serviceRequestsSailing = [];
                var serviceRequestsShifting = [];
                var serviceRequestsWarping = [];
                var serviceRequestDocumnet = [];

                if (self.servicerequestModel().MovementType() == 'SGMV') {
                    model.ServiceRequestSailing(new ServiceRequestSailing(model.serviceSailing().ServiceRequestSailingID, 0, model.MarineRevenueCleared, model.DocumentID, new ServiceRequestDocument(0, 0, model.FileName, model.FileName, model.FileName)));

                }
                if (self.servicerequestModel().MovementType() == 'SHMV') {
                    model.ServiceRequestShifting(new ServiceRequestShifting(model.Draftaft().ServiceRequestShiftingID, 0, model.ToPortCode, model.ToQuayCode, model.ToBerthCode, model.BerthKey(), model.DraftFWD_SHMV(), model.DraftAFT_SHMV()));
                }
                if (self.servicerequestModel().MovementType() == 'WRMV') {
                    model.ServiceRequestWarping(new ServiceRequestWarping(model.serviceWarping().ServiceRequestWarpingID, 0, model.Warp, model.WarpDistance));
                }

                self.viewModelHelper.apiPut('api/ServiceRequests', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Service Request Updated Successfully", "ServiceRequest");
                                self.LoadServiceRequestDetails();
                                self.viewMode('List');
                                $('#spnTitle').html("Service Requests");
                            });
            }
            else {
                self.ServiceValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                if (msgIndem != '' || msgTida != '' || msgSpl != '') {
                    msg = msg + msgIndem + msgTida + msgSpl;
                    toastr.warning(msg);
                }
                return;
            }
        }

        //////// Mahesh : To add New Service request ................

        self.addServRequest = function (data) {
            self.servicerequestModel(new IPMSROOT.ServiceRequestModel(undefined, self.berthsData()));
            self.servicerequestModel().IsValidationEnabled(false);
            $('#spnTitle').html("Add Service Request");
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.isEnableBtn(true);
            self.documentVisible(false);
            $("#movtID").attr('disabled', false).trigger("liszt:updated");
            $("#Vessel").data('kendoAutoComplete').enable(true);
            $("#Datepicker").data('kendoDatePicker').enable(true);
            $("#Datepicker1").data('kendoDatePicker').enable(true);
            $("#Datepicker2").data('kendoDatePicker').enable(true);
            $("#Datepicker3").data('kendoDatePicker').enable(true);
            $("#PreferredDate").data('kendoDatePicker').enable(true);
            $("#PreferredDate1").data('kendoDatePicker').enable(true);
            $("#PreferredDate2").data('kendoDatePicker').enable(true);
            $("#PreferredDate3").data('kendoDatePicker').enable(true);
            self.IsCodeEnable(true);
            self.printView(false);
            $('#divBerthName').hide();
            $('#divBollards').hide();
        }

        //////// Mahesh : To View service request details in View mode .........

        self.viewservReqst = function (servicerequest) {

            var Mtype = servicerequest.MovementType;

            if (servicerequest.VesselType() == 'V010') {
                self.pasenVisible(true);
            }
            else {
                self.pasenVisible(false);
            }

            if (Mtype() == "ARMV") {
                if (servicerequest.VesselData().Tidal() == 'Yes' || servicerequest.VesselData().IsSpecialNature() == 'Yes') {
                    self.documentVisible(true);
                }
                else {
                    self.documentVisible(false);
                }
            } else {
                if (servicerequest.VesselData().IsSpecialNature() == 'Yes' || servicerequest.IsTidal()) {
                    self.documentVisible(true);
                } else {
                    self.documentVisible(false);
                }
            }
            self.ETAmin = "";
            self.ETDmax = "";
            self.eventCurrentdata(servicerequest);
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.isEnableBtn(false);
            self.printView(true);
            $("#wfview").show();

            self.servicerequestModel(servicerequest);
            $('#spnTitle').html("View Service Request");
            self.servicerequestModel().pendingTasks.removeAll();
            var ReferenceID = servicerequest.ServiceRequestID();
            self.vcnlocal = servicerequest.VCNSort;
            var WorkflowInstanceID;
            if (servicerequest.MovementType() == "SHMV") {
                WorkflowInstanceID = servicerequest.BPWorkflowInstanceId();
            }
            else {
                WorkflowInstanceID = servicerequest.WorkflowInstanceId();
            }


            self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                  null,
                        function (result) {

                            ko.utils.arrayForEach(result, function (val) {
                                var pendingtaskaction = new IPMSROOT.pendingTask();
                                pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                pendingtaskaction.ReferenceID(val.ReferenceID);
                                pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                pendingtaskaction.APIUrl(val.APIUrl);
                                pendingtaskaction.TaskName(val.TaskName);
                                pendingtaskaction.TaskDescription(val.TaskDescription);
                                pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                pendingtaskaction.HasRemarks(val.HasRemarks);
                                self.servicerequestModel().pendingTasks.push(pendingtaskaction);

                            });
                        });

            if (Mtype() != "ARMV") {
                $('#divBerthName').hide();
                $('#divBollards').hide();
            }
        }

        self.cancelReqst = function (servicerequest) {
            self.viewMode('List');
            self.viewMode('PopUp');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.printView(false);

            self.servicerequestModel(servicerequest);

        }


        self.cancelConfirmReqst = function (servicerequest) {
            self.viewMode('List');
            self.viewMode('PopUp1');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.printView(false);

            self.servicerequestModel(servicerequest);

        }

        ///////// Mahesh : To Edit service request details in Edit mode ........

        self.editservReqst = function (servicerequest) {


            if (servicerequest.VesselType() == 'V010') {
                self.pasenVisible(true);
            }
            else {
                self.pasenVisible(false);
            }

            if (servicerequest.MovementType() == "ARMV") {
                if (servicerequest.VesselData().Tidal() == 'Yes' || servicerequest.VesselData().IsSpecialNature() == 'Yes') {
                    self.documentVisible(true);
                }
                else {
                    self.documentVisible(false);
                }
            } else {
                if (servicerequest.VesselData().IsSpecialNature() == 'Yes' || servicerequest.IsTidal()) {
                    self.documentVisible(true);
                } else {
                    self.documentVisible(false);
                }
            }


            self.ETAmin = "";
            self.ETDmax = "";
            self.printView(false);
            self.LoadBerths();
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            $("#movtID").attr("disabled", true);
            $("#Vessel").data('kendoAutoComplete').enable(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.isEnableBtn(true);
            self.servicerequestModel(servicerequest);
            self.servicerequestModel().IsValidationEnabled(false);
            $("#Datepicker").data('kendoDatePicker').enable(true);
            $("#Datepicker1").data('kendoDatePicker').enable(true);
            $("#Datepicker2").data('kendoDatePicker').enable(true);
            $("#Datepicker3").data('kendoDatePicker').enable(true);
            $("#PreferredDate").data('kendoDatePicker').enable(true);
            $("#PreferredDate1").data('kendoDatePicker').enable(true);
            $("#PreferredDate2").data('kendoDatePicker').enable(true);
            $("#PreferredDate3").data('kendoDatePicker').enable(true);
            $('#spnTitle').html("Edit Service Request");
            self.eventCurrentdata(servicerequest);
            self.EditSlotBlockingDate(servicerequest);
        }

        ///////// mahesh : For Reset functionality ...................

        self.ResetRequest = function () {
            self.printView(false);
            // confirmation box - start
            $.confirm({
                'title': 'Service Request confirmation',
                'message': 'Do you want to reset the changes ?',
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            self.currentBerthnBollardData('');
                            self.servicerequestModel().reset();
                            if (self.IsUpdate() == true) {
                                self.EditSlotBlockingDate(self.servicerequestModel());
                            }

                        }
                    },
                    'No': {
                        'class': 'gray',
                        'action': function () {
                        }
                    }
                }
            });

        }

        /////// mahesh : For Cancel Functionality....................

        self.closePopup = function () {
            self.servicerequestModel().workflowRemarks("");
            self.viewMode('List');

        }

        self.CancelButton = function () {
            $(".close").trigger("click");
            self.LoadServiceRequestDetails();
            self.viewMode('List');

        }


        self.Cancel = function (servicerequest) {

            if (viewDetail == true) {
                if (self.vcntype == 'VM') {
                    window.location.href = '/VoyageMonitoring/ManageVoyageMonitoring/' + self.vcnlocal;
                }
                else {
                    window.location.href = '/Welcome';
                }
            }
            else {
                self.viewMode('List');
                self.servicerequestModel().reset();
                self.servicerequestModel().pendingTasks.removeAll();
                location.reload();
            }
            $('#spnTitle').html("Service Requests");


        }
        ///////////mahesh : For Cancel Workflow     ..........................



        self.cancelWFRequest = function (model) {



            if (model.workflowRemarks() == undefined || model.workflowRemarks() == "") {
                $('#spanremarks').text('Please Enter Reason');
                return;
            }

            self.viewModelHelper.apiPost('api/ServiceRequests/GridCancel', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Service Request Cancelled Successfully", "ServiceRequest");
                                $(".close").trigger("click");
                                self.LoadServiceRequestDetails();
                                self.viewMode('List');
                            });

        }

        self.cancelConfirmWFRequest = function (model) {



            if (model.workflowRemarks() == undefined || model.workflowRemarks() == "") {
                $('#spanremarks1').text('Please Enter Reason');
                return;
            }
            model.IsConfirmCancel("Y");
            self.viewModelHelper.apiPost('api/ServiceRequests/GridCancel', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Service Request Cancellation Request Sent Successfully", "ServiceRequest");
                                $(".close").trigger("click");
                                self.LoadServiceRequestDetails();
                                self.viewMode('List');
                            });

        }

        self.selectBerth = function () {
            if (self.servicerequestModel().CurrentBerthCode() == self.servicerequestModel().BerthKey()) {
                toastr.warning("You Should Select Otherthan CurrentBerth");
                self.servicerequestModel().BerthKey(undefined);
                return;
            }
        }

        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.servicerequestModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.viewWorkFlow = function (servicerequest) {

            var WorkflowInstanceID;
            if (servicerequest.MovementType() == "SHMV") {
                if (servicerequest.BPWorkflowInstanceId() != null && servicerequest.WorkflowInstanceId() != null) {
                    workflowinstanceId = servicerequest.WorkflowInstanceId();
                }
                else if (servicerequest.BPWorkflowInstanceId() != "" && servicerequest.WorkflowInstanceId() == "") {
                    workflowinstanceId = servicerequest.BPWorkflowInstanceId();
                }
                else if (servicerequest.BPWorkflowInstanceId() == "" && servicerequest.WorkflowInstanceId() != "") {
                    workflowinstanceId = servicerequest.WorkflowInstanceId();
                }
                else {
                    workflowinstanceId = servicerequest.BPWorkflowInstanceId();
                }
            }
            else {
                workflowinstanceId = servicerequest.WorkflowInstanceId();
            }

            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {

                  self.servicerequestModel(new IPMSROOT.ServiceRequestModel());
                  self.servicerequestModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }

        ChangeShiftTidal = function (data) {

            var shiftData = ko.toJS(data);

            if (($('#IST1').is(':checked')) || shiftData.VesselData.IsSpecialNature == 'Yes') {
                self.documentVisible(true);
            }
            else {
                self.documentVisible(false);
            }
        }

        ChangeWarpingTidal = function (data) {

            var warpingData = ko.toJS(data);

            if (($('#isTidalWarp').is(':checked')) || warpingData.VesselData.IsSpecialNature == 'Yes') {
                self.documentVisible(true);
            }
            else {
                self.documentVisible(false);
            }
        }

        ChangeSailingTidal = function (data) {

            var sailingData = ko.toJS(data);

            if (($('#IsTidalSailing').is(':checked')) || sailingData.VesselData.IsSpecialNature == 'Yes') {
                self.documentVisible(true);
            }
            else {
                self.documentVisible(false);
            }
        }


        self.Initialize();
    }
    IPMSRoot.ServiceRequestViewModel = ServiceRequestViewModel;

}(window.IPMSROOT));

//////////Mahesh : Functions for returning ServiceRequestShifting, ServiceRequestSailing,ServiceRequestWarping,Document..........

function ServiceRequestSailing(ServiceRequestSailingID, ServiceRequestID, MarineRevenueCleared, DocumentID, Document) {

    this.ServiceRequestSailingID = ServiceRequestSailingID;
    this.ServiceRequestID = ServiceRequestID;
    this.MarineRevenueCleared = MarineRevenueCleared;
    this.DocumentID = DocumentID;
    this.ServiceRequestDocument = Document;
}

function ServiceRequestShifting(ServiceRequestShiftingID, ServiceRequestID, ToPortCode, ToQuayCode, ToBerthCode, BerthKey, DraftFWD_SHMV, DraftAFT_SHMV) {

    this.ServiceRequestShiftingID = ServiceRequestShiftingID;
    this.ServiceRequestID = ServiceRequestID;

    this.ToPortCode = ToPortCode;
    this.ToQuayCode = ToQuayCode;
    this.ToBerthCode = ToBerthCode;

    this.Berthkey = BerthKey;

    this.DraftFWD = DraftFWD_SHMV;
    this.DraftAFT = DraftAFT_SHMV;

}

function ServiceRequestWarping(ServiceRequestWarpingID, ServiceRequestID, Warp, WarpDistance) {

    this.ServiceRequestWarpingID = ServiceRequestWarpingID;
    this.ServiceRequestID = ServiceRequestID;

    this.Warp = Warp;
    this.WarpDistance = WarpDistance;

}

function ServiceRequestDocument(DocumentID, DocumentType, DocumentName, DocumentPath, FileName) {
    this.DocumentID = DocumentID;
    this.DocumentType = DocumentType;
    this.DocumentName = DocumentName;
    this.DocumentPath = DocumentPath;
    this.FileName = FileName;

}

function SlotType(SlotPeriod, StartTime, EndTime) {
    this.SlotPeriod = SlotPeriod;
    this.StartTime = StartTime;
    this.EndTime = EndTime;

}



