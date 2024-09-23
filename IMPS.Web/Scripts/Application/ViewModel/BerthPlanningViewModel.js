var _gridContainerWidth;
(function (IPMSRoot) {
    var isView = 0;
    var StartDate;
    var BerthPlanningViewModel = function () {
        var self = this;
        var shapeCount = 0;
        _gridContainerWidth = $("#GridContainer").width();
        self.GridContainerWidth = ko.observable(_gridContainerWidth);
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.berthruleshelper = new BusinessRules.BerthingRulesHelper();
        self.viewMode = ko.observable();
        self.quayModel = ko.observable();
        self.berthplanningModel = ko.observable();
        self.berthsWithBollards = ko.observableArray();
        self.quayberthsWithBollards = ko.observableArray();
        self.quayBollards = ko.observableArray();
        self.Configurations = ko.observable();
        self.LoggedUserData = ko.observable();
        self.unplannedvessels = ko.observableArray();
        self.berthmaintainenceshapes = ko.observableArray();
        self.berthmaintainence = ko.observableArray();
        self.plannedvessesls = ko.observableArray();
        self.vesselShapes = ko.observableArray();
        self.QuayValues = ko.observableArray();
        self.PortName = ko.observable();
        self.suitableberths = ko.observableArray();
        // function suitableberthsforVCN(unplannedvessel, berths) { var self = this; self.Vessel = ko.observable(unplannedvessel), self.berths = ko.observableArray(berths) };
        self.EndDate = ko.observable();
        self.VCNDragged = ko.observable();
        self.selectedQuayCode = ko.observable();
        self.layerx = ko.observable();
        self.layery = ko.observable();
        self.FromBollardPosition = ko.observable();
        self.ToBollardPostion = ko.observable();
        self.SelectedDate = ko.observable();
        self.CurrentDate = ko.observable();
        self.EndDate = ko.observable();
        self.ScheduleEnabled = ko.observable(true);
        self.ConfirmEnabled = ko.observable(true);
        self.PastDate = ko.observable(true);
        self.ViewType = ko.observable();
        self.RolePrivileges = ko.observable();
        //dates
        var curdate = new Date();
        self.SelectedDate(curdate);
        self.CurrentDate(new Date());

        $("#FromDate").kendoDatePicker({
            value: self.SelectedDate(), format: 'yyyy-MM-dd', parseFormats: ["MM-dd-yyyy"], readonly: true, change: function () {
                self.SelectedDate(this.value());
                toastr.clear();

                if (self.SelectedDate().getTime() < curdate.getTime()) {
                    self.ScheduleEnabled(false);
                }
                else {
                    self.ScheduleEnabled(true);
                    self.PastDate(false);
                }
            }
        });

        var nodays;
        var todate = new Date(self.SelectedDate());
        //self.EndDate(moment(todate).add('days', nodays).format('MM-DD-YYYY'));
        var nohours;
        var noOftimerLines;
        var heightpic;

        var widthpic = 0;

        var TotalGridStage;
        var BerthStage;
        var Bollardstage;
        var TimerStage;

        var berthText;
        var bollardText;
        var TimerText;

        var GridLayer;
        var BerthLayer;
        var BollardLayer;
        var TimerLayer;
        var tooltip;

        // Initialize
        self.Initialize = function () {
            self.Configurations(new IPMSROOT.ConfigurationModel());
            InitializeStages();
            self.LoadBerthPlanningPrivileges();
            self.LoadPortConfiguration();
            self.LoadCommonData();
            self.LoadQuays();
            self.LoadUserDetails();
            self.LoadQuayBerthBollards();
            self.quayModel(new IPMSROOT.Quay());
            self.berthplanningModel(new IPMSROOT.BerthPlanningModel());
            self.VesselCallDetails();
            self.LoadSuitableBerths();
            self.EndDate(moment(todate).add('days', nodays).format('MM-DD-YYYY'));
            self.LoadBerthMaintenance();
            self.PortClick();

        };

        // Load Stages              
        function InitializeStages() {

            //Total Lay Out  // Change as per width
            TotalGridStage = new Kinetic.Stage({
                container: 'GridContainer',
                width: self.GridContainerWidth() + 2,
                height: 800
            });

            // Berth Lay Out
            BerthStage = new Kinetic.Stage({
                container: 'BerthContainer',
                width: self.GridContainerWidth() + 2,
                height: 10
            });

            //Bollard Lay Out
            Bollardstage = new Kinetic.Stage({
                container: 'BollardContainer',
                width: self.GridContainerWidth() + 2,
                height: 10
            });


            // Timer Lay Out
            TimerStage = new Kinetic.Stage({
                container: 'TimerContainer',
                width: 50,
                height: 800,
                x: 0,
                y: 19
            });

            berthText = new Kinetic.Text({
                fontSize: 12,
                fontFamily: 'Calibri',
                fill: 'black',
                paddingleft: 2,
                align: 'center'
            });

            bollardText = new Kinetic.Text({
                fontSize: 11,
                fontFamily: 'Calibri',
                fill: 'black',
                paddingleft: 2,
                align: 'center'
            });

            TimerText = new Kinetic.Text({
                fontSize: 13,
                fontFamily: 'Calibri',

                paddingleft: 5,
                align: 'top'
            });

            // Defining Layer and adding to Stages
            GridLayer = new Kinetic.Layer();
            BerthLayer = new Kinetic.Layer();
            TimerLayer = new Kinetic.Layer();
            BollardLayer = new Kinetic.Layer();

            TotalGridStage.add(GridLayer);
            BerthStage.add(BerthLayer);
            Bollardstage.add(BollardLayer);
            TimerStage.add(TimerLayer);

            var simpleText = new Kinetic.Text({
                x: 0,
                y: -30,
                text: 'Time',
                fontSize: 14,
                fontFamily: 'Calibri',
                fill: '#CCC',
                align: 'center'
            });

            TimerLayer.add(simpleText);

            tooltip = new Opentip(
                    "div#GridContainer", //target element 
                    "DummyContent", // will be replaced
                    "", // title
                    {

                        showOn: null,
                        // I'll manually manage the showOn effect
                    });


        }

        // Get Quays Data
        self.LoadQuays = function () {
            self.viewModelHelper.apiGet('api/QuaysForBerthPlanning', null,
            function (result) {
                self.PortName(result[0].PortName);
                ko.mapping.fromJS(result, {}, self.QuayValues);

            }, null, null, false);
        }

        // Get Quays Data
        self.LoadBerthPlanningPrivileges = function () {
            self.viewModelHelper.apiGet('api/GetBerthPlanningPrivileges', null,
            function (result) {

                self.RolePrivileges(result);

            }, null, null, false);
        }

        // Get User Data
        self.LoadUserDetails = function () {
            self.viewModelHelper.apiGet('api/UserDetails', null,
            function (result) {
                self.LoggedUserData(new IPMSRoot.UserModel(result));
            },
                null, null, false);
        }

        // Get Quay Berth and Bollards Data
        self.LoadQuayBerthBollards = function () {
            self.viewModelHelper.apiGet('api/QuaysBerthsBollards', null,
             function (result) {
                 self.quayberthsWithBollards(ko.utils.arrayMap(result, function (item) {
                     return new IPMSRoot.QuayModel(item);
                 }));
             },
                 null, null, false);

            // Row numbers for Quay Berths
            $.each(self.quayberthsWithBollards(), function (index, quay) {
                var berths = quay.Berths();
                $.each(berths, function (index, berth) {
                    berth.BerthID(index + 1);
                });
            });
        }

        // Get VesselCallMovement Data
        self.VesselCallDetails = function () {
            var todate = new Date(self.SelectedDate());
            self.EndDate(moment(todate).add('days', nodays - 1).format('MM-DD-YYYY'));
            self.viewModelHelper.apiGet('api/GetVesselCallMovements/' + kendo.toString(self.SelectedDate(), "MM-dd-yyyy") + '/' + kendo.toString(self.EndDate(), "MM-dd-yyyy"), null,
              function (result) {
                  //console.log('Vessels', result);
                  $.each(result, function (index, item) {
                      if (item.MovementStatus == "MPEN" || item.MovementStatus == null) {
                          if ((moment(item.ETB).format('YYYY-MM-DD') <= moment(self.SelectedDate()).format('YYYY-MM-DD')))
                              self.unplannedvessels.push(new IPMSRoot.BerthPlanningModel(item));
                      }
                      else {
                          self.plannedvessesls.push(new IPMSRoot.BerthPlanningModel(item));
                      }
                  });
              },
             null, null, false);
        }

        // Get Suitable Berths for Planned & Pending Vessels
        self.LoadSuitableBerths = function () {
            // Pending Vessels
            $.each(self.unplannedvessels(), function (index, vessel) {
                var ValidBerthsList = [];
                $.each(self.quayberthsWithBollards(), function (index, quay) {
                    var berths = quay.Berths();
                    $.each(berths, function (index, berth) {
                        var isBerthValid;
                        if (self.LoggedUserData().isTerminalOperator() == true) {
                            var IsTOBerth = $.inArray(berth.BerthCode(), self.LoggedUserData().Berths());
                            if (IsTOBerth != -1) {
                                isBerthValid = self.berthruleshelper.CheckBerthingRules(vessel, berth, self.LoggedUserData(), self.Configurations().UnderKeelClearance()).ValidBerth;
                            }
                            else {
                                isBerthValid = false;
                            }
                        }
                        else {
                            isBerthValid = self.berthruleshelper.CheckBerthingRules(vessel, berth, self.LoggedUserData(), self.Configurations().UnderKeelClearance()).ValidBerth;
                        }

                        if (isBerthValid) {
                            var berthObj = { QuayCode: berth.QuayCode(), BerthID: berth.BerthID(), BerthCode: berth.BerthCode(), BerthName: berth.BerthName(), BerthLength: berth.BerthLength() }
                            ValidBerthsList.push(berthObj);
                        }
                    });
                });
                vessel.SuitableBerthsList(ValidBerthsList);
            });

            // Planned Vessels
            $.each(self.plannedvessesls(), function (index, vessel) {
                var ValidBerthsList = [];
                $.each(self.quayberthsWithBollards(), function (index, quay) {
                    var berths = quay.Berths();
                    $.each(berths, function (index, berth) {

                        var isBerthValid;
                        if (self.LoggedUserData().isTerminalOperator() == true) {
                            var IsTOBerth = $.inArray(berth.BerthCode(), self.LoggedUserData().Berths());
                            if (IsTOBerth != -1) {
                                isBerthValid = self.berthruleshelper.CheckBerthingRules(vessel, berth, self.LoggedUserData(), self.Configurations().UnderKeelClearance()).ValidBerth;
                            }
                            else {
                                isBerthValid = false;
                            }
                        }
                        else {
                            isBerthValid = self.berthruleshelper.CheckBerthingRules(vessel, berth, self.LoggedUserData(), self.Configurations().UnderKeelClearance()).ValidBerth;
                        }

                        if (isBerthValid) {
                            var berthObj = { QuayCode: berth.QuayCode(), BerthID: berth.BerthID(), BerthCode: berth.BerthCode(), BerthName: berth.BerthName() }
                            ValidBerthsList.push(berthObj);
                        }
                    });
                });
                vessel.SuitableBerthsList(ValidBerthsList);
            });

        };


        self.GetSuitableBerths = function (vessel) {
            var ValidBerthsList = [];
            $.each(self.quayberthsWithBollards(), function (index, quay) {
                var berths = quay.Berths();
                $.each(berths, function (index, berth) {
                    var isBerthValid;
                    if (self.LoggedUserData().isTerminalOperator() == true) {
                        var IsTOBerth = $.inArray(berth.BerthCode(), self.LoggedUserData().Berths());
                        if (IsTOBerth != -1) {
                            isBerthValid = self.berthruleshelper.CheckBerthingRules(vessel, berth, self.LoggedUserData(), self.Configurations().UnderKeelClearance()).ValidBerth;
                        }
                        else {
                            isBerthValid = false;
                        }
                    }
                    else {
                        isBerthValid = self.berthruleshelper.CheckBerthingRules(vessel, berth, self.LoggedUserData(), self.Configurations().UnderKeelClearance()).ValidBerth;
                    }

                    if (isBerthValid) {
                        ValidBerthsList.push(berth);
                    }
                });
            });

            //vessel.SuitableBerthsList(ValidBerthsList);

            return ValidBerthsList;
        };



        // Get BerthMaintainence Data
        self.LoadBerthMaintenance = function () {
            self.viewModelHelper.apiGet('api/GetBerthMaintainenceRequests/' + kendo.toString(self.SelectedDate(), "MM-dd-yyyy") + '/' + kendo.toString(self.EndDate(), "MM-dd-yyyy"), null,
           function (result) {

               $.each(result, function (index, item) {
                   self.berthmaintainence.push(new IPMSRoot.BerthMaintainenceModel(item));
               });
           },
          null, null, false);
        }

        self.LoadPortConfiguration = function () {
            self.viewModelHelper.apiGet('api/GetBerthPlanningConfiguration', null,
           function (result) {
               $.each(result, function (index, item) {
                   if (item.ConfigName == "SLOT")
                       self.Configurations().Slot(item.ConfigValue);
                   if (item.ConfigName == "DAYS")
                       self.Configurations().Days(item.ConfigValue);
                   if (item.ConfigName == "UNDER KEEL CLEARANCE")
                       self.Configurations().UnderKeelClearance(item.ConfigValue);
                   if (item.ConfigName == "SAFEDISTANCE")
                       self.Configurations().SafeDistance(item.ConfigValue);
                   if (item.ConfigName == "SCHEDULED")
                       self.Configurations().ScheduleColor(item.ConfigValue);
                   if (item.ConfigName == "CONFIRMED")
                       self.Configurations().ConfirmColor(item.ConfigValue);
                   if (item.ConfigName == "BERTHED")
                       self.Configurations().BerthedColor(item.ConfigValue);
                   if (item.ConfigName == "MAINTAINENCE")
                       self.Configurations().MaintainenceColor(item.ConfigValue);
                   if (item.ConfigName == "ARRESTED")
                       self.Configurations().ArrestedColor(item.ConfigValue);
                   if (item.ConfigName == "SAILED")
                       self.Configurations().SailedColor(item.ConfigValue);
               });
           },
            null, null, false);
        }

        self.LoadCommonData = function () {
            nodays = self.Configurations().Days();
            nohours = self.Configurations().Slot();
            noOftimerLines = nodays * 24 / nohours;
            heightpic = 800 / noOftimerLines;
        }

        self.PortMouseOver = function (data, event) {
            var PortInfo = getInfo('P', data);
            tooltip.setContent(PortInfo);
            tooltip.currentPosition.left = event.pageX;
            tooltip.currentPosition.top = event.pageY;
            tooltip.show();
        }


        self.QuayMouseOver = function (data, event) {
            var QuayInfo = getInfo('Q', data);
            tooltip.setContent(QuayInfo);
            tooltip.currentPosition.left = event.pageX;
            tooltip.currentPosition.top = event.pageY;
            tooltip.show();

        }

        self.BerthMouseOver = function (data, event) {
            var BerthInfo = getInfo('B', data);
            tooltip.setContent(BerthInfo);
            tooltip.currentPosition.left = event.pageX;
            tooltip.currentPosition.top = event.pageY;
            tooltip.show();
        }

        self.MouseOut = function (data, event) {
            tooltip.hide();
        }

       
        self.VesselDetailsPop = function (data, event) {
            var vesselinfo = getVesselInfo(data);
            tooltip.setContent(vesselinfo);
            tooltip.currentPosition.left = event.layerX / widthpic;
            tooltip.currentPosition.top = event.layerY;
            tooltip.show();


        }

        // Port Click
        self.PortClick = function (data, event) {
            self.ViewType('Port');
            ClearGrid();
            InitializeStages();
            Portlength = 0;
            var PreviousLength = 0;
            var PQuayLength = 0;
            $.each(self.QuayValues(), function (index, item) {
                Portlength = Portlength + item.QuayLength();
            })
            widthpic = self.GridContainerWidth() / Portlength;
            self.LoadGridDatawithQuays(self.quayberthsWithBollards(), self.GridContainerWidth());
            self.vesselShapes([]);
            self.berthmaintainenceshapes([]);

            $.each(self.berthmaintainence(), function (index, item) {
                PreviousLength = 0;
                $.each(self.quayberthsWithBollards(), function (index, quay) {
                    if (item.MaintQuayCode() == quay.QuayCode()) {
                        self.DisplayMaintainence(item, 'Port', PreviousLength);
                        return;
                    }
                    else {
                        PreviousLength = PreviousLength + quay.QuayLength();
                    }
                });
            });

            //calling draw function to draw shape of planned vessel of that quay
            $.each(self.plannedvessesls(), function (index, item) {
                PQuayLength = 0
                $.each(self.quayberthsWithBollards(), function (index, quay) {
                    if (item.FromQuayCode() == quay.QuayCode()) {
                        self.DisplayVessels(item, PQuayLength);
                        return;
                    }
                    else {
                        PQuayLength = PQuayLength + quay.QuayLength();
                    }
                });
            });


        }

        // Quay Click
        self.QuayClick = function (data, event) {
            self.ViewType('Quay');
            self.selectedQuayCode(data.QuayCode());

            $.each(self.QuayValues(), function (index, item) {
                if (item.QuayCode() == data.QuayCode()) {
                    quaylength = item.QuayLength();
                    widthpic = self.GridContainerWidth() / quaylength;
                }
            });

            ClearGrid();
            InitializeStages();
            self.quayBollards([]);
            $.each(self.quayberthsWithBollards(), function (index, item) {
                if (item.QuayCode() == data.QuayCode()) {

                    // Remove 
                    self.Berths = ko.observableArray(item.Berths());

                    self.berthsWithBollards(item.Berths());
                    $.each(item.Berths(), function (index, berth) {
                        bollards = berth.Bollards();
                        $.each(bollards, function (index, bollard) {
                            self.quayBollards.push(bollard);
                        });
                    });
                }

            });

            self.LoadGridData(self.berthsWithBollards(), self.GridContainerWidth());
            self.vesselShapes([]);
            self.berthmaintainenceshapes([]);


            //calling draw function to draw shape of planned vessel of that quay
            $.each(self.plannedvessesls(), function (index, item) {
                if (item.FromQuayCode() == self.selectedQuayCode()) {
                    //console.log('self.plannedvessesls()', item.VCN());
                    var _shape = new Shape(item, 0);
                    self.vesselShapes.push(_shape);
                    self.DrawShape(_shape);

                }
            });

            // Maintainence Shapes
            $.each(self.berthmaintainence(), function (index, item) {
                if (item.MaintQuayCode() == self.selectedQuayCode()) {
                    self.DisplayMaintainence(item, 'Quay', 0);
                }
            });
        }

        // Berth Button Click 
        self.BerthClick = function (data, event) {
            self.ViewType('Berth');
            ClearGrid();
            InitializeStages();
            var prevberthslength = 0;
            self.berthmaintainenceshapes([]);
            $.each(self.berthsWithBollards(), function (index, item) {
                if (item.BerthCode() == data.BerthCode()) {
                    self.LoadGridWithBollards(item, self.GridContainerWidth(), prevberthslength);
                    $.each(self.plannedvessesls(), function (index, vessel) {

                        if (vessel.FromBerthCode() == item.BerthCode() || vessel.ToBerthCode() == item.BerthCode()) {
                            nohours = self.Configurations().Slot();
                            noOftimerLines = nodays * 24 / nohours;
                            heightpic = 800 / noOftimerLines;
                            widthpic = self.GridContainerWidth() / item.BerthLength();
                            var shape1 = vessel;

                            var shape = new Shape(shape1, prevberthslength);
                            self.DrawShape(shape);
                        }

                    });

                    // Maintainence
                    $.each(self.berthmaintainence(), function (index, maint) {
                        //if (maint.MaintBerthCode() == item.BerthCode())
                        if (maint.MaintBerthCode() == data.BerthCode()) {
                            nohours = self.Configurations().Slot();
                            noOftimerLines = nodays * 24 / nohours;
                            heightpic = 800 / noOftimerLines;
                            widthpic = self.GridContainerWidth() / item.BerthLength();
                            self.DisplayMaintainence(maint, 'Berth', prevberthslength);
                        }
                    });
                }
                else
                    prevberthslength = prevberthslength + item.BerthLength();
            })
        }

        self.DisplayVessels = function (item, PreviousLength) {
            var shape = new PShape(item, PreviousLength);
            self.vesselShapes.push(shape);
            self.DrawShape(shape);
        }

        //Clear Grid
        function ClearGrid() {
            TotalGridStage = null;
            BerthStage = null;
            TimerStage = null;
            Bollardstage = null;
            berthText = null;
            bollardText = null;
            TimerText;
            GridLayer = null;
            BerthLayer = null;
            TimerLayer = null;
            BollardLayer = null;
            shapeCount = 0;

        }

        self.DisplayMaintainence = function (item, type, PreviousLength) {
            if (type == 'Quay') {
                var maintshape = new MaintShape(item);
                self.berthmaintainenceshapes.push(maintshape);
                self.DrawMaintShape(maintshape);
            }
            else if (type == 'Berth') {
                var maintshape = new MaintBShape(item, PreviousLength);
                self.berthmaintainenceshapes.push(maintshape);
                self.DrawMaintShape(maintshape);
            }
            else {
                var maintshape = new MaintPShape(item, PreviousLength);
                self.berthmaintainenceshapes.push(maintshape);
                self.DrawMaintShape(maintshape);
            }
        }

        //View Click
        self.viewButtonClick = function () {
            var todate = new Date(self.SelectedDate());
            if (self.SelectedDate() != null) {
                self.EndDate(moment(todate).add('days', nodays).format('MM-DD-YYYY'));
                ClearGrid();
                InitializeStages();
                self.plannedvessesls([]);
                self.unplannedvessels([]);
                self.vesselShapes([]);
                self.berthmaintainence([]);
                self.LoadBerthMaintenance();
                self.VesselCallDetails();
                self.LoadSuitableBerths();
                self.PortClick();
            }
            else {

                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.error("Please Select From Date", "Berth Planning");
            }
        }

        // Schedule Click
        self.ScheduleClick = function () {
            var unplannedvessels;
            var userdata;
            var isBerthValid;
            var FromBerth;
            var ToBerth;
            var fromberth;
            var pendingvessel;
            var isBerthPlanned;
            var isBerthAvailable;
            var ValidBerthsList = [];
            var FromToBollard;
            userdata = self.LoggedUserData();
            if (self.unplannedvessels().length > 0) {
                if (self.ViewType() == 'Port') {
                    var StartDate = moment(self.SelectedDate()).startOf('day');
                    var EndDate = moment(self.SelectedDate()).add(2, 'day');
                    if (self.unplannedvessels().length > 0) {
                        unplannedvessels = self.unplannedvessels();
                        var AutoScheduleVessels = [];
                        for (i = 0; i < unplannedvessels.length; i++) {
                            if (unplannedvessels[i].ETB() > moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm') && moment(unplannedvessels[i].ETB()).format('YYYY-MM-DD') == moment(self.SelectedDate()).format('YYYY-MM-DD')) {
                                AutoScheduleVessels.push(unplannedvessels[i]);
                            }

                        }


                        // Looping Through Pending Vessels
                        for (i = 0; i < AutoScheduleVessels.length; i++) {
                            ValidBerthsList = [];
                            pendingvessel = AutoScheduleVessels[i];
                            if (pendingvessel.ETB() > moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm') && moment(pendingvessel.ETB()).format('YYYY-MM-DD') == moment(self.SelectedDate()).format('YYYY-MM-DD')) {

                                var SuitableBerthsList = "";
                                ValidBerthsList = self.GetSuitableBerths(pendingvessel);

                                if (ValidBerthsList.length > 0) {

                                    //  Preferred Berth - Alternate Berth
                                    var isPreferredBerth = false;
                                    var PreferredBerth;
                                    var AlternateBerth;
                                    var isAlternateBerth = false;
                                    var BerthAlloted = false;

                                    $.each(ValidBerthsList, function (index, validberth) {
                                        if (validberth.BerthCode() == pendingvessel.PreferredBerth()) {
                                            isPreferredBerth = true;
                                            PreferredBerth = validberth;

                                        }
                                        else if (validberth.BerthCode() == pendingvessel.AlternateBerth()) {
                                            isAlternateBerth = true;
                                            AlternateBerth = validberth;

                                        }

                                    });


                                    if (isPreferredBerth == true) {
                                        FromToBollard = {};
                                        FromToBollard = AssignBollards(pendingvessel, PreferredBerth);
                                        fromberth = GetBerth(FromToBollard.frombollard.BollardCode(), FromToBollard.frombollard.BerthCode(), self.selectedQuayCode());
                                        if (FromToBollard.FromBollardAssigned == true && FromToBollard.ToBollardAssigned == true) {

                                            var ConflictingVesselsfromClient = [];
                                            var _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(pendingvessel.VCN(), pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                            ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                            //console.log('VCN', pendingvessel.VCN());
                                            //console.log('GetConflicVesselsfromServer', GetConflicVesselsfromServer);
                                            //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);
                                            var isSafeDistance = CheckIsVesselSafe(pendingvessel, FromToBollard.frombollard.FromMeter(), FromToBollard.tobollard.FromMeter());
                                            if (isSafeDistance == true && _IsBerthSuitablefromServerSide == true && ConflictingVesselsfromClient.length == 0) {

                                                pendingvessel.FromPortCode(FromToBollard.frombollard.PortCode());
                                                pendingvessel.ToPortCode(FromToBollard.frombollard.PortCode());
                                                pendingvessel.FromQuayCode(self.selectedQuayCode());
                                                pendingvessel.ToQuayCode(self.selectedQuayCode());
                                                pendingvessel.FromBerthCode(FromToBollard.frombollard.BerthCode());
                                                //pendingvessel.BerthName(FromToBollard.frombollard.BerthName());
                                                pendingvessel.BerthName(fromberth.BerthName());

                                                pendingvessel.ToBerthCode(FromToBollard.tobollard.BerthCode());
                                                //pendingvessel.ToBerthName(FromToBollard.tobollard.BerthName());
                                                pendingvessel.FromBollardCode(FromToBollard.frombollard.BollardCode());
                                                pendingvessel.ToBollardCode(FromToBollard.tobollard.BollardCode());
                                                pendingvessel.FromBollardMeter(FromToBollard.frombollard.FromMeter());
                                                pendingvessel.ToBollardMeter(FromToBollard.tobollard.FromMeter());
                                                pendingvessel.FromBollardName(FromToBollard.frombollard.BollardName());
                                                pendingvessel.ToBollardName(FromToBollard.tobollard.BollardName());
                                                var startposition = FromToBollard.frombollard.FromMeter();
                                                var endposition = pendingvessel.VesselLength() + startposition;
                                                pendingvessel.PositionX(startposition);
                                                pendingvessel.MovementStatus("SCH");
                                                var shape = new Shape(pendingvessel, 0);
                                                self.vesselShapes.push(shape);
                                                self.plannedvessesls.push(pendingvessel);
                                                self.unplannedvessels.remove(pendingvessel);
                                                self.DrawShape(shape);
                                                BerthAlloted = true;

                                            }
                                        }
                                    }
                                    else if (isAlternateBerth == true) {
                                        FromToBollard = {};
                                        FromToBollard = AssignBollards(pendingvessel, AlternateBerth);
                                        fromberth = GetBerth(FromToBollard.frombollard.BollardCode(), FromToBollard.frombollard.BerthCode(), self.selectedQuayCode());

                                        if (FromToBollard.FromBollardAssigned == true && FromToBollard.ToBollardAssigned == true) {
                                            var ConflictingVesselsfromClient = [];
                                            var _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(pendingvessel.VCN(), pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                            ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                            //console.log('VCN', pendingvessel.VCN());
                                            //console.log('GetConflicVesselsfromServer', GetConflicVesselsfromServer);
                                            //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);
                                            var isSafeDistance = CheckIsVesselSafe(pendingvessel, FromToBollard.frombollard.FromMeter(), FromToBollard.tobollard.FromMeter());
                                            if (isSafeDistance == true && _IsBerthSuitablefromServerSide == true && ConflictingVesselsfromClient.length == 0) {

                                                pendingvessel.FromPortCode(FromToBollard.frombollard.PortCode());
                                                pendingvessel.ToPortCode(FromToBollard.frombollard.PortCode());
                                                pendingvessel.FromQuayCode(self.selectedQuayCode());
                                                pendingvessel.ToQuayCode(self.selectedQuayCode());
                                                pendingvessel.FromBerthCode(FromToBollard.frombollard.BerthCode());
                                                //pendingvessel.BerthName(FromToBollard.frombollard.BerthName());
                                                pendingvessel.BerthName(fromberth.BerthName());
                                                pendingvessel.ToBerthCode(FromToBollard.tobollard.BerthCode());
                                                //pendingvessel.ToBerthName(FromToBollard.tobollard.BerthName());
                                                pendingvessel.FromBollardCode(FromToBollard.frombollard.BollardCode());
                                                pendingvessel.ToBollardCode(FromToBollard.tobollard.BollardCode());
                                                pendingvessel.FromBollardMeter(FromToBollard.frombollard.FromMeter());
                                                pendingvessel.ToBollardMeter(FromToBollard.tobollard.FromMeter());
                                                pendingvessel.FromBollardName(FromToBollard.frombollard.BollardName());
                                                pendingvessel.ToBollardName(FromToBollard.tobollard.BollardName());
                                                var startposition = FromToBollard.frombollard.FromMeter();
                                                var endposition = pendingvessel.VesselLength() + startposition;
                                                pendingvessel.PositionX(startposition);
                                                pendingvessel.MovementStatus("SCH");
                                                var shape = new Shape(pendingvessel, 0);
                                                self.vesselShapes.push(shape);
                                                self.plannedvessesls.push(pendingvessel);
                                                self.unplannedvessels.remove(pendingvessel);
                                                self.DrawShape(shape);
                                                BerthAlloted == true;

                                            }
                                        }
                                    }
                                    else if (BerthAlloted == false) {
                                        $.each(ValidBerthsList, function (index, validberth) {

                                            FromToBollard = {};
                                            FromToBollard = AssignBollards(pendingvessel, validberth);
                                            fromberth = GetBerth(FromToBollard.frombollard.BollardCode(), FromToBollard.frombollard.BerthCode(), self.selectedQuayCode());
                                            if (FromToBollard.FromBollardAssigned == true && FromToBollard.ToBollardAssigned == true) {
                                                var ConflictingVesselsfromClient = [];
                                                var _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(pendingvessel.VCN(), pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                                ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                                //console.log('VCN', pendingvessel.VCN());
                                                //console.log('GetConflicVesselsfromServer', GetConflicVesselsfromServer);
                                                //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);
                                                var isSafeDistance = CheckIsVesselSafe(pendingvessel, FromToBollard.frombollard.FromMeter(), FromToBollard.tobollard.FromMeter());
                                                if (isSafeDistance == true && _IsBerthSuitablefromServerSide == true && ConflictingVesselsfromClient.length == 0) {
                                                    pendingvessel.FromPortCode(FromToBollard.frombollard.PortCode());
                                                    pendingvessel.ToPortCode(FromToBollard.frombollard.PortCode());
                                                    pendingvessel.FromQuayCode(FromToBollard.frombollard.QuayCode());
                                                    pendingvessel.ToQuayCode(FromToBollard.frombollard.QuayCode());
                                                    pendingvessel.FromBerthCode(FromToBollard.frombollard.BerthCode());
                                                    //pendingvessel.BerthName(FromToBollard.frombollard.BerthName());
                                                    pendingvessel.BerthName(fromberth.BerthName());
                                                    //pendingvessel.ToBerthName(FromToBollard.tobollard.BerthName());
                                                    pendingvessel.ToBerthCode(FromToBollard.tobollard.BerthCode());
                                                    pendingvessel.FromBollardCode(FromToBollard.frombollard.BollardCode());
                                                    pendingvessel.ToBollardCode(FromToBollard.tobollard.BollardCode());
                                                    pendingvessel.FromBollardMeter(FromToBollard.frombollard.FromMeter());
                                                    pendingvessel.ToBollardMeter(FromToBollard.tobollard.FromMeter());
                                                    pendingvessel.FromBollardName(FromToBollard.frombollard.BollardName());
                                                    pendingvessel.ToBollardName(FromToBollard.tobollard.BollardName());
                                                    var startposition = FromToBollard.frombollard.FromMeter();
                                                    var endposition = pendingvessel.VesselLength() + startposition;
                                                    pendingvessel.PositionX(startposition);
                                                    pendingvessel.MovementStatus("SCH");
                                                    var length = GetPreviousLength(FromToBollard.frombollard.QuayCode());
                                                    var shape = new PShape(pendingvessel, length);
                                                    self.vesselShapes.push(shape);
                                                    self.plannedvessesls.push(pendingvessel);
                                                    self.unplannedvessels.remove(pendingvessel);
                                                    self.DrawShape(shape);
                                                    return false;
                                                }
                                            }

                                        });
                                    }

                                }
                            }

                        } // End of FOR

                    }
                    else {

                        if (self.selectedQuayCode() != undefined) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.error("Please Select Quay", "Berth Planning");
                        }
                        else {

                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.error("No unplanned vessels to schedule", "Berth Planning");
                        }
                    }


                }

                    // Quay Schedule 
                else {
                    var StartDate = moment(self.SelectedDate()).startOf('day');
                    var EndDate = moment(self.SelectedDate()).add(2, 'day');
                    if (self.selectedQuayCode() != undefined && self.unplannedvessels().length > 0) {
                        unplannedvessels = self.unplannedvessels();
                        var AutoScheduleVessels = [];
                        for (i = 0; i < unplannedvessels.length; i++) {
                            if (unplannedvessels[i].ETB() > moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm') && moment(unplannedvessels[i].ETB()).format('YYYY-MM-DD') == moment(self.SelectedDate()).format('YYYY-MM-DD')) {
                                AutoScheduleVessels.push(unplannedvessels[i]);
                            }

                        }


                        // Looping Through Pending Vessels
                        for (i = 0; i < AutoScheduleVessels.length; i++) {
                            ValidBerthsList = [];
                            pendingvessel = AutoScheduleVessels[i];
                            if (pendingvessel.ETB() > moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm') && moment(pendingvessel.ETB()).format('YYYY-MM-DD') == moment(self.SelectedDate()).format('YYYY-MM-DD')) {

                                var SuitableBerthsList = "";
                                ValidBerthsList = self.GetSuitableBerths(pendingvessel);

                                if (ValidBerthsList.length > 0) {

                                    //  Preferred Berth - Alternate Berth
                                    var isPreferredBerth = false;
                                    var PreferredBerth;
                                    var AlternateBerth;
                                    var isAlternateBerth = false;
                                    var BerthAlloted = false;

                                    $.each(ValidBerthsList, function (index, validberth) {
                                        if (validberth.QuayCode() == self.selectedQuayCode() && validberth.BerthCode() == pendingvessel.PreferredBerth()) {
                                            isPreferredBerth = true;
                                            PreferredBerth = validberth;

                                        }
                                        else if (validberth.QuayCode() == self.selectedQuayCode() && validberth.BerthCode() == pendingvessel.AlternateBerth()) {
                                            isAlternateBerth = true;
                                            AlternateBerth = validberth;

                                        }

                                    });


                                    if (isPreferredBerth == true) {
                                        FromToBollard = {};
                                        FromToBollard = AssignBollards(pendingvessel, PreferredBerth);
                                        fromberth = GetBerth(FromToBollard.frombollard.BollardCode(), FromToBollard.frombollard.BerthCode(), self.selectedQuayCode());
                                        if (FromToBollard.FromBollardAssigned == true && FromToBollard.ToBollardAssigned == true) {

                                            var ConflictingVesselsfromClient = [];
                                            var _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(pendingvessel.VCN(), pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                            ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                            //console.log('VCN', pendingvessel.VCN());
                                            //console.log('GetConflicVesselsfromServer', GetConflicVesselsfromServer);
                                            //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);
                                            var isSafeDistance = CheckIsVesselSafe(pendingvessel, FromToBollard.frombollard.FromMeter(), FromToBollard.tobollard.FromMeter());
                                            if (isSafeDistance == true && _IsBerthSuitablefromServerSide == true && ConflictingVesselsfromClient.length == 0) {

                                                pendingvessel.FromPortCode(FromToBollard.frombollard.PortCode());
                                                pendingvessel.ToPortCode(FromToBollard.frombollard.PortCode());
                                                pendingvessel.FromQuayCode(self.selectedQuayCode());
                                                pendingvessel.ToQuayCode(self.selectedQuayCode());
                                                pendingvessel.FromBerthCode(FromToBollard.frombollard.BerthCode());
                                                //pendingvessel.BerthName(FromToBollard.frombollard.BerthName());
                                                pendingvessel.BerthName(fromberth.BerthName());
                                                pendingvessel.ToBerthCode(FromToBollard.tobollard.BerthCode());
                                                pendingvessel.FromBollardCode(FromToBollard.frombollard.BollardCode());
                                                //pendingvessel.ToBerthName(FromToBollard.tobollard.BerthName());
                                                pendingvessel.ToBollardCode(FromToBollard.tobollard.BollardCode());
                                                pendingvessel.FromBollardMeter(FromToBollard.frombollard.FromMeter());
                                                pendingvessel.ToBollardMeter(FromToBollard.tobollard.FromMeter());
                                                pendingvessel.FromBollardName(FromToBollard.frombollard.BollardName());
                                                pendingvessel.ToBollardName(FromToBollard.tobollard.BollardName());
                                                var startposition = FromToBollard.frombollard.FromMeter();
                                                var endposition = pendingvessel.VesselLength() + startposition;
                                                pendingvessel.PositionX(startposition);
                                                pendingvessel.MovementStatus("SCH");
                                                var shape = new Shape(pendingvessel, 0);
                                                self.vesselShapes.push(shape);
                                                self.plannedvessesls.push(pendingvessel);
                                                self.unplannedvessels.remove(pendingvessel);
                                                self.DrawShape(shape);
                                                BerthAlloted == true;

                                            }
                                        }
                                    }
                                    else if (isAlternateBerth == true) {
                                        FromToBollard = {};
                                        FromToBollard = AssignBollards(pendingvessel, AlternateBerth);
                                        fromberth = GetBerth(FromToBollard.frombollard.BollardCode(), FromToBollard.frombollard.BerthCode(), self.selectedQuayCode());
                                        if (FromToBollard.FromBollardAssigned == true && FromToBollard.ToBollardAssigned == true) {
                                            var ConflictingVesselsfromClient = [];
                                            var _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(pendingvessel.VCN(), pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                            ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                            //console.log('VCN', pendingvessel.VCN());
                                            //console.log('GetConflicVesselsfromServer', GetConflicVesselsfromServer);
                                            //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);
                                            var isSafeDistance = CheckIsVesselSafe(pendingvessel, FromToBollard.frombollard.FromMeter(), FromToBollard.tobollard.FromMeter());
                                            if (isSafeDistance == true && _IsBerthSuitablefromServerSide == true && ConflictingVesselsfromClient.length == 0) {

                                                pendingvessel.FromPortCode(FromToBollard.frombollard.PortCode());
                                                pendingvessel.ToPortCode(FromToBollard.frombollard.PortCode());
                                                pendingvessel.FromQuayCode(self.selectedQuayCode());
                                                pendingvessel.ToQuayCode(self.selectedQuayCode());
                                                pendingvessel.FromBerthCode(FromToBollard.frombollard.BerthCode());
                                                //pendingvessel.BerthName(FromToBollard.frombollard.BerthName());
                                                pendingvessel.BerthName(fromberth.BerthName());
                                                //pendingvessel.ToBerthName(FromToBollard.tobollard.BerthName());
                                                pendingvessel.ToBerthCode(FromToBollard.tobollard.BerthCode());
                                                pendingvessel.FromBollardCode(FromToBollard.frombollard.BollardCode());
                                                pendingvessel.ToBollardCode(FromToBollard.tobollard.BollardCode());
                                                pendingvessel.FromBollardMeter(FromToBollard.frombollard.FromMeter());
                                                pendingvessel.ToBollardMeter(FromToBollard.tobollard.FromMeter());
                                                pendingvessel.FromBollardName(FromToBollard.frombollard.BollardName());
                                                pendingvessel.ToBollardName(FromToBollard.tobollard.BollardName());
                                                var startposition = FromToBollard.frombollard.FromMeter();
                                                var endposition = pendingvessel.VesselLength() + startposition;
                                                pendingvessel.PositionX(startposition);
                                                pendingvessel.MovementStatus("SCH");
                                                var shape = new Shape(pendingvessel, 0);
                                                self.vesselShapes.push(shape);
                                                self.plannedvessesls.push(pendingvessel);
                                                self.unplannedvessels.remove(pendingvessel);
                                                self.DrawShape(shape);
                                                BerthAlloted == true;

                                            }
                                        }
                                    }
                                    else if (BerthAlloted == false) {

                                        $.each(ValidBerthsList, function (index, validberth) {
                                            if (validberth.QuayCode() == self.selectedQuayCode()) {
                                                FromToBollard = {};
                                                FromToBollard = AssignBollards(pendingvessel, validberth);
                                                fromberth = GetBerth(FromToBollard.frombollard.BollardCode(), FromToBollard.frombollard.BerthCode(), self.selectedQuayCode());
                                                if (FromToBollard.FromBollardAssigned == true && FromToBollard.ToBollardAssigned == true) {
                                                    var ConflictingVesselsfromClient = [];
                                                    var _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(pendingvessel.VCN(), pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                                    ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(pendingvessel.VesselCallMovementID(), FromToBollard.frombollard, FromToBollard.tobollard, pendingvessel.ETB(), pendingvessel.ETUB());
                                                    //console.log('VCN', pendingvessel.VCN());
                                                    //console.log('GetConflicVesselsfromServer', GetConflicVesselsfromServer);
                                                    //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);
                                                    var isSafeDistance = CheckIsVesselSafe(pendingvessel, FromToBollard.frombollard.FromMeter(), FromToBollard.tobollard.FromMeter());
                                                    if (isSafeDistance == true && _IsBerthSuitablefromServerSide == true && ConflictingVesselsfromClient.length == 0) {

                                                        pendingvessel.FromPortCode(FromToBollard.frombollard.PortCode());
                                                        pendingvessel.ToPortCode(FromToBollard.frombollard.PortCode());
                                                        pendingvessel.FromQuayCode(self.selectedQuayCode());
                                                        pendingvessel.ToQuayCode(self.selectedQuayCode());
                                                        pendingvessel.FromBerthCode(FromToBollard.frombollard.BerthCode());
                                                        //pendingvessel.BerthName(FromToBollard.frombollard.BerthName());
                                                        pendingvessel.BerthName(fromberth.BerthName());
                                                        pendingvessel.ToBerthCode(FromToBollard.tobollard.BerthCode());
                                                        //pendingvessel.ToBerthName(FromToBollard.tobollard.BerthName());
                                                        pendingvessel.FromBollardCode(FromToBollard.frombollard.BollardCode());
                                                        pendingvessel.ToBollardCode(FromToBollard.tobollard.BollardCode());
                                                        pendingvessel.FromBollardMeter(FromToBollard.frombollard.FromMeter());
                                                        pendingvessel.ToBollardMeter(FromToBollard.tobollard.FromMeter());
                                                        pendingvessel.FromBollardName(FromToBollard.frombollard.BollardName());
                                                        pendingvessel.ToBollardName(FromToBollard.tobollard.BollardName());
                                                        var startposition = FromToBollard.frombollard.FromMeter();
                                                        var endposition = pendingvessel.VesselLength() + startposition;
                                                        pendingvessel.PositionX(startposition);
                                                        pendingvessel.MovementStatus("SCH");
                                                        var shape = new Shape(pendingvessel, 0);
                                                        self.vesselShapes.push(shape);
                                                        self.plannedvessesls.push(pendingvessel);
                                                        self.unplannedvessels.remove(pendingvessel);
                                                        self.DrawShape(shape);
                                                        return false;
                                                    }
                                                }
                                            }
                                        });
                                    }

                                }
                                else {
                                    // console.log('No suitable Berth', pendingvessel.VCN());
                                }
                            }
                            else {
                                //   console.log('NotSelecetedVessels', pendingvessel.VCN() + '-' + pendingvessel.ETB());

                            }

                        } // End of FOR

                    }
                    else {

                        if (self.selectedQuayCode() != undefined) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.error("Please Select Quay", "Berth Planning");
                        }
                        else {

                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.error("No unplanned vessels to schedule", "Berth Planning");
                        }
                    }
                }
            }
            else {

                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.error("No unplanned vessels to schedule", "Berth Planning");
            }
        }



        function GetPreviousLength(QuayCode) {
            //  alert(QuayCode);
            var PQuayLength = 0; var exit = false;
            $.each(self.quayberthsWithBollards(), function (index, quay) {
                if (QuayCode == quay.QuayCode()) {                    //self.DisplayVessels(item, PQuayLength);
                    exit = true;
                }
                else {
                    if (exit == false) {
                        PQuayLength = PQuayLength + quay.QuayLength();
                    }
                }
            });

            return PQuayLength;


        }

        function IsBerthPlanned(berth) {
            var isPlanned = false;
            $.each(self.plannedvessesls(), function (index, vessel) {
                if (vessel.FromBerthCode() == berth.BerthCode() || vessel.ToBerthCode() == berth.BerthCode()) {
                    isPlanned = true;
                }

            });
            return isPlanned;
        }
        function AssignBollards(pendingvessel, validberth) {
            var FromBollardAssigned = false;
            var ToBollardAssigned = false;
            FromBollard = "";
            var ToBollard = "";
            var ISToBollard = false;
            var Length = 0;
            $.each(validberth.Bollards(), function (index, bollard) {

                if (FromBollard == "") {
                    FromBollard = bollard;
                    FromBollardAssigned = true;
                }
                var BollardLength = bollard.ToMeter() - bollard.FromMeter();
                Length = Length + BollardLength;
                if (Length > pendingvessel.LOA()) {
                    if (ToBollard == "") {
                        ToBollardAssigned = true;
                        ToBollard = bollard;
                        return false;
                    }

                }

            });

            var obj = { FromBollardAssigned: FromBollardAssigned, frombollard: FromBollard, ToBollardAssigned: ToBollardAssigned, tobollard: ToBollard }
            return obj;
        }

        //Confirm Button Click
        self.ConfirmClick = function () {
            $.each(self.plannedvessesls(), function (index, item) {
                if (item.MovementStatus() == "SCH") {
                    item.MovementStatus("CONF");
                    $.each(self.vesselShapes(), function (index, shape) {

                        if (shape.line.attrs.VCMID == item.VesselCallMovementID()) {
                            var line = shape.line;
                            line.attrs.fill = self.Configurations().ConfirmColor();
                            line.draggable(false);
                            //GridLayer.add(line); // Commented by sandeep on 13-04-2015
                            GridLayer.draw();
                        }
                    });

                }
            });

        }

        // Save Click
        self.SaveClick = function () {

            self.viewModelHelper.apiPut('api/SaveVesselCallMovement', ko.mapping.toJSON(self.plannedvessesls().concat(self.unplannedvessels())), function Message(data) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("Vessel Call Movements Saved successfully", "BerthPlanning");
            });
        }



        self.ResetClick = function () {


            // confirmation box - start
            $.confirm({
                'title': 'Berth Planning',
                'message': 'The data will be Lost. Are you sure you want to continue?',
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            $("#FromDate").kendoDatePicker({
                                value: curdate,
                                format: "yyyy-MM-dd",
                                readonly: true
                            });
                            ClearGrid();
                            InitializeStages();
                            self.plannedvessesls([]);
                            self.unplannedvessels([]);
                            self.vesselShapes([]);
                            self.berthmaintainence([]);
                            self.LoadBerthMaintenance();
                            self.VesselCallDetails();
                            self.PortClick();
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

        function IsBerthAvailable(uvessel, berth) {
            var IsAvailable = true;
            var BerthTime = new Date(Date.parse(uvessel.BerthTime()));
            var UnBerthTime = new Date(Date.parse(uvessel.UnBerthTime()));

            $.each(self.plannedvessesls(), function (index, planneditem) {
                if (planneditem.FromBerthCode() == berth.BerthCode()) {
                    var PVBerthTime = new Date(Date.parse(planneditem.BerthTime()));
                    var PVUnBerthTime = new Date(Date.parse(planneditem.UnBerthTime()));
                    if ((PVBerthTime.getTime() > BerthTime.getTime() && PVBerthTime.getTime() > UnBerthTime.getTime()) || (PVUnBerthTime.getTime() < BerthTime.getTime() && PVUnBerthTime.getTime() < UnBerthTime.getTime())) {
                        valid1 = true;
                    }
                    else {
                        IsAvailable = false;
                    }
                }

            });
            return IsAvailable;

        }

        //function IsBerthAvailabile(VCN,VCMID, frombollard, tobollard, fromtime, totime)
        //{

        //    //self.viewModelHelper.apiGet('api/GetVesselsAndMaintainenceDataForVessel', { VCN: VCN, QuayCode: frombollard.QuayCode(), FromBollardMeter: frombollard.FromMeter(), ToBollardMeter: tobollard.FromMeter(), BerthTime: kendo.toString(BerthTime, "yyyy-MM-dd HH:mm"), UnBerthTime: kendo.toString(UnBerthTime, "yyyy-MM-dd HH:mm") },

        //    var isAvailable;
        //    self.viewModelHelper.apiGet('api/CheckBerthAvailability', {VCN:VCN, VCMID: VCMID, QuayCode: frombollard.QuayCode(), FromBerthCode: frombollard.BerthCode(), FromBollardMeter: frombollard.FromMeter(), ToBerthCode: tobollard.BerthCode(), ToBollardMeter: tobollard.FromMeter(), FromTime: kendo.toString(fromtime, "yyyy-MM-dd HH:mm"), ToTime: kendo.toString(totime, "yyyy-MM-dd HH:mm") },
        //    function (result) {
        //        isAvailable = result;
        //    },
        //   null, null, false);
        //    return isAvailable;            
        //}


        function GetConflictingVesselsfromServer(VCN, VCMID, frombollard, tobollard, fromtime, totime) {

            var isAvailable;
            self.viewModelHelper.apiGet('api/CheckBerthAvailability', { VCN: VCN, VCMID: VCMID, QuayCode: frombollard.QuayCode(), FromBerthCode: frombollard.BerthCode(), FromBollardMeter: frombollard.FromMeter(), ToBerthCode: tobollard.BerthCode(), ToBollardMeter: tobollard.FromMeter(), FromTime: kendo.toString(fromtime, "yyyy-MM-dd HH:mm"), ToTime: kendo.toString(totime, "yyyy-MM-dd HH:mm") },
            function (result) {
                isAvailable = result;
            },
           null, null, false);
            return isAvailable;
        }





        function IsShiftingAvailable(VCMID, VCN, fromberth, toberth, fromtime, totime) {
            var isMovementPossible = true;

            $.each(self.plannedvessesls(), function (index, planneditem) {
                if (planneditem.VCN() == VCN && planneditem.VesselCallMovementID() != VCMID) {

                    if (planneditem.MovementType() == "ARMV") {
                        if (moment(planneditem.UnBerthTime()).format('YYYY-MM-DD HH:mm') > moment(fromtime).format('YYYY-MM-DD HH:mm')) {
                            isMovementPossible = false;
                        }

                    }
                    else if (planneditem.MovementType() == "SHMV") {

                        if (moment(planneditem.BerthTime()).format('YYYY-MM-DD HH:mm') < moment(totime).format('YYYY-MM-DD HH:mm')) {
                            isMovementPossible = false;
                        }

                    }
                }
            });

            return isMovementPossible;
        }


        //check availability of berth of the vessel on that ETA and ETD;
        function CheckBerthAvailability(uvessel, berth) {

            var eta = new Date(Date.parse(uvessel.BerthTime()));
            var etd = new Date(Date.parse(uvessel.UnBerthTime()));
            var vessellength = uvessel.VesselLength();
            var safedistance = 25;
            var valid1 = false;
            var countofplannedvesselsinberth = 0;
            var plannedvesselslength = 0;

            $.each(self.plannedvessesls(), function (index, planneditem) {
                if (planneditem.FromBerthCode() == berth.BerthCode()) {
                    countofplannedvesselsinberth++;
                    var peta = new Date(Date.parse(planneditem.BerthTime()));
                    var petd = new Date(Date.parse(planneditem.UnBerthTime()));
                    var startposition = GetFromBollardPosition(planneditem.FromBollardCode(), planneditem.FromBerthCode(), planneditem.FromQuayCode());
                    var endposition = GetToBollardPosition(planneditem.ToBollardCode());
                    var berthlength = berth.BerthLength();
                    var pletahours = peta.getTime();
                    var pledthours = petd.getTime();
                    var etahours = eta.getTime();
                    var etdhours = etd.getTime();

                    var berthlength = berth.BerthLength();
                    if ((peta.getTime() > eta.getTime() && peta.getTime() > etd.getTime()) || (petd.getTime() < eta.getTime() && petd.getTime() < etd.getTime())) {
                        valid1 = true;
                    }

                    else if (endposition + vessellength + 25 <= berthlength && (peta.getTime() <= eta.getTime() && petd.getTime > eta.getTime())) {
                        plannedvesselslength = endposition;
                        valid1 = true;
                    }
                }
            });
            // }
            if (valid1 == false && countofplannedvesselsinberth == 0) {
                valid1 = true;
                plannedvesselslength = berth.Bollards()[0].FromMeter();
            }
            var obj = { valid: valid1, StartBollardPosition: plannedvesselslength }
            return obj;
        }

        function DateTimeCheck(fromdate, todate, checkdate) {




        }

        //allot  berth from suitable berths of that vessel--not yet completely implemented
        function AllocateBerth() {
            var vesselvsberth = [];
            var sberths = self.suitableberths();                        //total suitableberths with their vcns
            for (i = 0; i < sberths.length; i++) {

                var vesselwithberths = sberths[i];              //vessel with list of suitable berths

                //list of suitable berths of that vessel is > 1

                for (j = 0; j < vesselwithberths.berths().length; j++) {//looping the berths of that vessel

                    var berth = vesselwithberths.berths()[j];           //taking one berth from the vessel berths list

                    for (k = i + 1; k < sberths.length; k++) {          //looping for remaining vessels to check conflict

                        var nextvesselwithberths = sberths[k];          //next vessel with list of suitable berths

                        if (nextvesselwithberths.berths().length == 1) {          //list of berths==1

                            var nextitemberth = nextvesselwithberths.berths();  //only one berth form the next vessel berths list

                            if (nextitemberth.BerthCode() == berth.BerthCode()) {

                                var peta = new Date(Date.parse(vesselwithberths.Vessel().BerthTime()));
                                var petd = new Date(Date.parse(vesselwithberths.Vessel().UnBerthTime()));
                                var neta = new Date(Date.parse(nextvesselwithberths.Vessel().BerthTime()));
                                var netd = new Date(Date.parse(nextvesselwithberths.Vessel().UnBerthTime()));

                                if (peta == neta) {
                                    self.suitableberths()[i].berths().remove(nextitemberth);
                                }
                            }

                        } else {
                            alert("next berthcount>1");

                        }
                    }
                }
            }
        }
        //get bollards availability for the vessel in the berth--not yet completely implemented

        //get frombollard position based on bollardcode
        function GetFromBollardPosition(bollardcode, berthcode, quaycode) {
            var position;
            $.each(self.berthsWithBollards(), function (index, item) {
                $.each(item.Bollards(), function (index, bollard) {
                    if (bollard.BollardCode() == bollardcode) {
                        position = bollard.FromMeter();
                    }
                });

            });
            return position;
        }

        //get tobollard position based on bollardcode
        function GetToBollardPosition(bollardcode) {
            var position;
            $.each(self.berthsWithBollards(), function (index, item) {
                $.each(item.Bollards(), function (index, bollard) {
                    if (bollard.BollardCode() == bollardcode) {
                        position = bollard.ToMeter();
                    }
                });

            });
            return position;
        }

        function GetBollardAvailability(berth, plannedvessel) {

            var frombollard; valid1 = true;
            var plannedvesselslength = 0;

            $.each(self.plannedvessesls(), function (index, planneditem) {
                if (planneditem.FromBerthCode() == berth.BerthCode()) {
                    var peta = new Date(Date.parse(planneditem.BerthTime()));
                    var petd = new Date(Date.parse(planneditem.UnBerthTime()));
                    var startposition = GetFromBollardPosition(planneditem.FromBollardCode());
                    var endposition = GetToBollardPosition(planneditem.ToBollardCode());

                    var berthlength = berth.BerthLength();
                    if (peta != eta && petd != etd) {
                        if (((peta < eta && eta < petd) && (peta < etd && etd < petd))) {

                            valid1 = false;

                        }
                    }
                    else if (endposition + vessellength >= berthlength) {
                        plannedvesselslength = plannedvesselslength + planneditem.VesselLength();
                        valid1 = false;

                    }
                }
            });
        }

        //drag and drop implementation
        self.drags = function (event) {

            if (event.dataTransfer != null) {
                event.dataTransfer.setData('Text', event.currentTarget.id);
            }

        }

        //self.DragEnd = function (event) {
        //    event.preventDefault();
        //    event.stopPropagation();
        //    if (event.dataTransfer != null) {
        //        if (self.ViewType() == 'Port' || self.ViewType() == 'Berth') {
        //            bootbox.alert('Vessels can be dragged in when a quay is selected');
        //        }
        //    }

        //    if (self.ViewType() == 'Quay') {
        //        if (event.dataTransfer != null) {
        //            var _VesselCallMovementID = parseInt(event.dataTransfer.getData('Text'));
        //            var selectedshape;

        //            if (_VesselCallMovementID > 0) {
        //                self.VCNDragged(_VesselCallMovementID);

        //                self.layerx(event.layerX);
        //                self.layery(event.layerY);

        //                selectedshape = ($.grep(self.unplannedvessels(), function (vessel) {
        //                    console.log('vessel', vessel.VesselCallMovementID());
        //                    return (vessel.VesselCallMovementID() === _VesselCallMovementID);
        //                }))[0];

        //                console.log('selectedshape', selectedshape);


        //                //   selectedshape = item;
        //                var canDoubleBank;
        //                var IsColliding;
        //                var isVesselSafe;
        //                var isTimeRange;
        //                var DraggedBerthTime;
        //                var DraggedUnBerthTime;
        //                var VesselsConflict = [];
        //                var DraggedTime = parseInt(self.layery() / heightpic);
        //                var SetTime = moment(self.SelectedDate()).startOf('day');                      
        //                var hoursdiff = moment.duration(moment(selectedshape.UnBerthTime()).diff(selectedshape.BerthTime())).asHours();
        //                var DraggedBerthTime = new Date((new Date(SetTime).setHours(DraggedTime)));
        //                var DraggedUnBerthTime = new Date((new Date(SetTime).setHours(DraggedTime + hoursdiff)));

        //                var fromberthvalid; var toberthvalid; var fromberth; var toberth; var frombollard; var tobollard;
        //                var isFromToBerthSame = true;
        //                var IsValid = false;
        //                var length = selectedshape.VesselLength() * widthpic;
        //                frombollard = GetFromBollard(self.layerx() / widthpic);
        //                tobollard = GetToBollard((self.layerx() + length) / widthpic);
        //                fromberth = GetBerth(frombollard.BollardCode());
        //                toberth = GetBerth(tobollard.BollardCode());
        //                for (i = 0; i < self.plannedvessesls().length; i++) {
        //                    canDoubleBank = false;
        //                    IsColliding = false;
        //                    isVesselSafe = false;
        //                    var plannedvessel = self.plannedvessesls()[i];
        //                    if (selectedshape.VesselCallMovementID() != plannedvessel.VesselCallMovementID() && plannedvessel.FromQuayCode() == frombollard.QuayCode()) {
        //                        IsColliding = AreCollidingBollards(plannedvessel, selectedshape, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
        //                        isTimeRange = isInTimeRange(plannedvessel, selectedshape, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
        //                        isVesselSafe = IsVesselSafe(plannedvessel, selectedshape, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
        //                        var VesselConflict = { VCN: plannedvessel.VCN(), IsColliding: IsColliding, isVesselSafe: isVesselSafe, isInTimeRange: isTimeRange }
        //                        VesselsConflict.push(VesselConflict);

        //                    }
        //                }
        //                var CollidingVCN;
        //                var NotSafeVCN;
        //                var IsColliding = false;
        //                var IsNotSafe = false;
        //                console.log('VesselsConflict', VesselsConflict);

        //                $.each(VesselsConflict, function (index, item) {
        //                    if (item.IsColliding && item.isTimeRange) {
        //                        CollidingVCN = item.VCN;
        //                        IsColliding = true;
        //                    }
        //                    if (item.isVesselSafe == false) {
        //                        NotSafeVCN = item.VCN;
        //                        IsNotSafe = true;
        //                    }

        //                });


        //                if (IsColliding) {
        //                    var WithinTime;
        //                    var PlannedVesselBerthTime;
        //                    var PlannedVesselUnBerthTime;
        //                    var SelectedVesselBerthTime = DraggedBerthTime;
        //                    var SelectedVesselUnBerthTime = DraggedUnBerthTime;

        //                    for (i = 0; i < self.plannedvessesls().length; i++) {
        //                        if (self.plannedvessesls()[i].VCN() == CollidingVCN) {
        //                            PlannedVesselBerthTime = new Date(Date.parse(self.plannedvessesls()[i].BerthTime()));
        //                            PlannedVesselUnBerthTime = new Date(Date.parse(self.plannedvessesls()[i].UnBerthTime()));
        //                        }
        //                    }

        //                    var PlannedVesselRange = moment().range(PlannedVesselBerthTime, PlannedVesselUnBerthTime);
        //                    var SelectedVesselRange = moment().range(SelectedVesselBerthTime, SelectedVesselUnBerthTime);
        //                    var TimeCondition1 = PlannedVesselRange.contains(SelectedVesselBerthTime);
        //                    var TimeCondition2 = PlannedVesselRange.contains(SelectedVesselUnBerthTime);


        //                    if (TimeCondition1 == true && TimeCondition2 == true) {
        //                        WithinTime = true;
        //                    }
        //                    else {
        //                        WithinTime = false;
        //                    }


        //                    //  if (selectedshape.LOA() < plannedvessel.LOA() && WithinTime == true && plannedvessel.MovementStatus() == "BERT") {
        //                    if (selectedshape.LOA() < plannedvessel.LOA() && WithinTime == true) {
        //                        canDoubleBank = true;
        //                    }

        //                    if (canDoubleBank == true) {

        //                        bootbox.confirm("Do you want to Bank " + selectedshape.attrs.id + " on " + CollidingVCN, function (result) {
        //                            if (result) {
        //                                var x1 = parseInt(selectedshape.getX() / widthpic);
        //                                var y1 = parseInt(selectedshape.getY() / heightpic);
        //                                for (i = 0; i < self.plannedvessesls().length; i++) {
        //                                    if (self.plannedvessesls()[i].VCN() == CollidingVCN) {
        //                                        self.plannedvessesls()[i].DoubleBankedVessel(selectedshape.attrs.id);
        //                                        var Bank = self.plannedvessesls()[i].IsBanked() + 1;
        //                                        self.plannedvessesls()[i].IsBanked(Bank);
        //                                        //alert(self.plannedvessesls()[i].DoubleBankedVessel() +'-'+  self.plannedvessesls()[i].IsBanked());
        //                                        $.each(self.vesselShapes(), function (index, item) {
        //                                            if (selectedshape.Id() == self.plannedvessesls()[i].VCN()) {

        //                                                var line = item.line;
        //                                                line.attrs.stroke = 'red';
        //                                                line.attrs.opacity = .1,
        //                                                line.draggable(false);
        //                                                var layer = line.getLayer();
        //                                                layer.draw();
        //                                            }

        //                                        });

        //                                        var vmstatus = "SCH";
        //                                        selectedshape.attrs.fill = "#FFBF00";
        //                                        selectedshape.draggable(true);
        //                                        var fromBollard = self.plannedvessesls()[i].FromBollardMeter();
        //                                        var startposition = GetFromBollardPosition(self.plannedvessesls()[i].FromBollardCode());
        //                                        var yposition = GetTimePosition(DraggedBerthTime, DraggedUnBerthTime);
        //                                        var fromberth = GetBerth(self.plannedvessesls()[i].FromBollardCode());
        //                                        var toberth = GetBerth(self.plannedvessesls()[i].ToBollardCode());
        //                                        var y1 = parseInt(selectedshape.getY() / heightpic);
        //                                        var vessel = updatePlannedItem(selectedshape.attrs.VCMID, selectedshape.attrs.id, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName());

        //                                        selectedshape.setPosition({ x: parseInt(startposition * widthpic), y: parseInt(yposition * heightpic) });
        //                                        selectedshape.name(vessel);
        //                                        var layer = selectedshape.getLayer();
        //                                        layer.draw();


        //                                    }
        //                                }

        //                                //var vmstatus = "SCH";
        //                                //if (selectedshape.name().MovementStatus() == "BERT") {
        //                                //    vmstatus = "SCH";
        //                                //    selectedshape.attrs.fill = "#FFBF00";
        //                                //    selectedshape.draggable(true);
        //                                //}

        //                                //alert('dfasdfas');
        //                                //var fromberth = GetBerth(frombollard.BollardCode());
        //                                //var toberth = GetBerth(tobollard.BollardCode());
        //                                //var vessel = updatePlannedItem(vcn, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1);


        //                            }
        //                            else {
        //                                selectedshape.setPosition({ x: Math.round(parseInt(selectedshape.name().PositionX() + prevsberthslength)) * widthpic, y: this.attrs.PositionY });
        //                                var layer = selectedshape.getLayer();
        //                                layer.draw()
        //                            }

        //                        });

        //                    }
        //                    else {
        //                        bootbox.alert('Vessel cannot be double banked on' + CollidingVCN);
        //                        selectedshape.setPosition({ x: Math.round(parseInt(selectedshape.name().PositionX() + prevsberthslength)) * widthpic, y: this.attrs.PositionY });
        //                        var layer = selectedshape.getLayer();
        //                        layer.draw()

        //                    }

        //                }
        //                else if (IsNotSafe) {
        //                    bootbox.alert('Safe Distance rule is in violation with' + NotSafeVCN);
        //                    selectedshape.setPosition({ x: Math.round(parseInt(selectedshape.name().PositionX() + prevsberthslength)) * widthpic, y: this.attrs.PositionY });
        //                    var layer = selectedshape.getLayer();
        //                    layer.draw()
        //                }



        //                if (IsColliding == false && IsNotSafe == false) {
        //                    fromberthvalid = self.berthruleshelper.CheckBerthingRules(selectedshape, fromberth, self.LoggedUserData(), self.Configurations().UnderKeelClearance());
        //                    if (fromberth.BerthCode() != toberth.BerthCode()) {
        //                        toberthvalid = self.berthruleshelper.CheckBerthingRules(selectedshape, toberth, self.LoggedUserData(), self.Configurations().UnderKeelClearance());
        //                    }
        //                    var isContinous = CheckDiscontinousBollard(self.layerx() / widthpic, (self.layerx() / widthpic + length));
        //                    if (isFromToBerthSame == true) {
        //                        if (fromberthvalid.ValidBerth == true && isContinous == true)
        //                            IsValid = true;
        //                    } else {
        //                        if (fromberthvalid.ValidBerth && toberthvalid.ValidBerth && isContinous == true)
        //                            IsValid = true;
        //                    }

        //                    if (IsValid) {
        //                        selectedshape.FromPortCode(fromberth.PortCode());
        //                        selectedshape.ToPortCode(fromberth.PortCode());
        //                        selectedshape.FromQuayCode(fromberth.QuayCode());
        //                        selectedshape.ToQuayCode(fromberth.QuayCode());
        //                        selectedshape.FromBerthCode(fromberth.BerthCode());
        //                        selectedshape.ToBerthCode(toberth.BerthCode());
        //                        selectedshape.FromBollardCode(frombollard.BollardCode());
        //                        selectedshape.ToBollardCode(tobollard.BollardCode());
        //                        selectedshape.FromBollardMeter(frombollard.FromMeter());
        //                        selectedshape.ToBollardMeter(tobollard.FromMeter());
        //                        selectedshape.PositionX(frombollard.FromMeter());
        //                        selectedshape.PositionY(self.layery() / heightpic);
        //                        var DraggedTime = parseInt(self.layery() / heightpic);
        //                        var SetTime = moment(self.SelectedDate()).startOf('day');
        //                        var DraggedBerthTime = new Date((new Date(SetTime).setHours(DraggedTime)));
        //                        var DraggedUnBerthTime = new Date((new Date(SetTime).setHours(DraggedTime + selectedshape.Diff())));
        //                        //  alert(DraggedBerthTime+'////'+DraggedUnBerthTime);
        //                        selectedshape.ETB(moment(DraggedBerthTime).format('YYYY-MM-DD HH:mm:ss'));
        //                        selectedshape.ETUB(moment(DraggedUnBerthTime).format('YYYY-MM-DD HH:mm:ss'));
        //                        selectedshape.MovementStatus("SCH");
        //                        self.plannedvessesls.push(selectedshape);
        //                        self.unplannedvessels.remove(selectedshape);
        //                        var shape = new Shape(selectedshape, 0);
        //                        self.vesselShapes.push(shape);
        //                        self.DrawShape(shape);
        //                        event.target.appendChild(document.getElementById(_VesselCallMovementID));
        //                    }
        //                    else {
        //                        var Msg = "";
        //                        Msg = RulesAlert(fromberth, toberth, frombollard, tobollard, isFromToBerthSame, fromberthvalid, toberthvalid, isContinous, true, true);
        //                        bootbox.alert(Msg);
        //                    }

        //                }
        //            }
        //        }

        //    }


        //}



        //   Old DragEnd
        self.DragEnd = function (event) {

            if (self.RolePrivileges() == "true") {
                event.preventDefault();
                event.stopPropagation();
                if (event.dataTransfer != null) {
                    if (self.ViewType() == 'Port' || self.ViewType() == 'Berth') {
                        bootbox.alert('Vessels can be dragged in when a quay is selected');
                    }
                }

                if (self.ViewType() == 'Quay') {
                    if (event.dataTransfer != null) {
                        var VCN = event.dataTransfer.getData('Text');
                        var _VesselCallMovementID = parseInt(event.dataTransfer.getData('Text'));
                        var selectedshape;
                        if (_VesselCallMovementID > 0) {
                            self.VCNDragged(_VesselCallMovementID);
                            self.layerx(event.layerX);
                            self.layery(event.layerY);
                            selectedshape = ($.grep(self.unplannedvessels(), function (vessel) {
                                return (vessel.VesselCallMovementID() === _VesselCallMovementID);
                            }))[0];

                            var _VCMID = selectedshape.VesselCallMovementID();
                            var _VCN = selectedshape.VCN();

                            var canDoubleBank;
                            var IsColliding;
                            var isVesselSafe;
                            var isTimeRange;
                            var DraggedBerthTime;
                            var DraggedUnBerthTime;
                            var VesselsConflict = [];

                            var DraggedTime = parseInt(self.layery() / heightpic);
                            var SetTime = moment(self.SelectedDate()).startOf('day');
                            var hoursdiff = moment.duration(moment(selectedshape.UnBerthTime()).diff(selectedshape.BerthTime())).asHours();
                            var DraggedBerthTime = new Date((new Date(SetTime).setHours(DraggedTime)));
                            var DraggedUnBerthTime = new Date((new Date(SetTime).setHours(DraggedTime + hoursdiff)));
                            // console.log('DraggedBerthTime-DraggedUnBerthTime',DraggedBerthTime+'-'+DraggedUnBerthTime);


                            var BerthTime = selectedshape.BerthTime()
                            if (moment(BerthTime).format('YYYY-MM-DD HH:mm') > moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm')) {

                                var frombollard; var tobollard; var fromberth; var toberth;
                                var _SuitableBerthsList = [];
                                var IsVesselSuitedAtBerths = true;
                                var BerthsStatusList = [];
                                var ConflictingVesselsfromServer;
                                var getBerths;
                                var _IsBerthSuitablefromServerSide;
                                var length = selectedshape.VesselLength() * widthpic;
                                frombollard = GetFromBollard(self.layerx() / widthpic);
                                tobollard = GetToBollard((self.layerx() + length) / widthpic);
                                //fromberth = GetBerth(frombollard.BollardCode());
                                //toberth = GetBerth(tobollard.BollardCode());
                                var fromberth = GetBerth(frombollard.BollardCode(), frombollard.BerthCode(), frombollard.QuayCode());
                                var toberth = GetBerth(tobollard.BollardCode(), tobollard.BerthCode(), tobollard.QuayCode());

                                // Getting Beths between Bollards
                                getBerths = getBerthsbetweenBollards(frombollard, tobollard);

                                // Getting the Suitable Berths of the Quay Selected
                                $.each(selectedshape.SuitableBerthsList(), function (index, suitableberth) {
                                    if (suitableberth.QuayCode == self.selectedQuayCode())
                                        _SuitableBerthsList.push(suitableberth.BerthCode);
                                });

                                // Check if the Berths are in Suitable Berths List 
                                $.each(getBerths.berthsList, function (index, item) {
                                    var isSuited = $.inArray(item, _SuitableBerthsList);
                                    if (isSuited == -1) {
                                        IsVesselSuitedAtBerths = false;
                                        BerthsStatusList.push(item);
                                    }
                                });


                                if (IsVesselSuitedAtBerths == true && getBerths.AreContinousBollards == true) {

                                    // Conflicts from Server Side 
                                    _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(_VCN, _VCMID, frombollard, tobollard, selectedshape.BerthTime(), selectedshape.UnBerthTime());
                                    //console.log('_IsBerthSuitablefromServerSide', _IsBerthSuitablefromServerSide);
                                    ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(_VCMID, frombollard, tobollard, selectedshape.BerthTime(), selectedshape.UnBerthTime());
                                    //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);

                                    if (ConflictingVesselsfromClient.length == 0 && _IsBerthSuitablefromServerSide == true) {

                                        selectedshape.FromPortCode(fromberth.PortCode());
                                        selectedshape.ToPortCode(fromberth.PortCode());
                                        selectedshape.FromQuayCode(fromberth.QuayCode());
                                        selectedshape.ToQuayCode(fromberth.QuayCode());
                                        selectedshape.FromBerthCode(fromberth.BerthCode());
                                        selectedshape.ToBerthCode(toberth.BerthCode());
                                        selectedshape.FromBollardCode(frombollard.BollardCode());
                                        selectedshape.ToBollardCode(tobollard.BollardCode());
                                        selectedshape.FromBollardMeter(frombollard.FromMeter());
                                        selectedshape.ToBollardMeter(tobollard.FromMeter());
                                        selectedshape.BerthName(fromberth.BerthName());
                                        selectedshape.PositionX(frombollard.FromMeter());
                                        selectedshape.FromBollardName(frombollard.BollardName());
                                        selectedshape.ToBollardName(tobollard.BollardName());
                                        selectedshape.PositionY(self.layery() / heightpic);
                                        selectedshape.MovementStatus("SCH");
                                        self.plannedvessesls.push(selectedshape);
                                        self.unplannedvessels.remove(selectedshape);
                                        var shape = new Shape(selectedshape, 0);
                                        self.vesselShapes.push(shape);
                                        self.DrawShape(shape);



                                    }
                                    else {
                                        var NotSafe = false;
                                        var IsColliding = false;
                                        var DoubleBankingVessels = [];
                                        var DoubleBankSuitableVessels = [];
                                        var NotSafeVessels = [];
                                        var CollidingVessels = [];
                                        //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);

                                        $.each(ConflictingVesselsfromClient, function (index, item) {

                                            if (item.DoubleBank == false) {
                                                if (item.VesselNotSafe == true || item.VesselCollide == true) {
                                                    //NotSafeVessels.push(item.VCN()); // commented by sandeep on 27-05-2015
                                                    NotSafeVessels.push({ VCMID: item.VCMID(), VCN: item.VCN() }); // Added by sandeep on 27-05-2015
                                                    NotSafe = true;
                                                }

                                            }
                                            else {
                                                //DoubleBankingVessels.push(item.VCN()); // commented by sandeep on 27-05-2015
                                                DoubleBankingVessels.push(item.VCMID()); // Added by sandeep on 27-05-2015
                                            }

                                        });


                                        // console.log('DoubleBankingVessels', DoubleBankingVessels);

                                        if (NotSafe == true) {

                                            var count = NotSafeVessels.length - 1;
                                            var vessel = NotSafeVessels[count].VCN;
                                            var Msg = "";
                                            if (NotSafe == true || IsColliding == true)
                                                Msg = Msg + "<li>Safe Distance of  is not maintained with " + vessel + ".</li>";
                                            bootbox.alert(Msg);

                                        }
                                        else if (DoubleBankingVessels.length > 0) {


                                            $.each(self.plannedvessesls(), function (index, item) {
                                                //var contains = $.inArray(item.VCN(), DoubleBankingVessels); // Commented by sandeep on 28-05-2015
                                                var contains = $.inArray(item.VesselCallMovementID(), DoubleBankingVessels); // Added by sandeep on 28-05-2015
                                                if (contains != -1) {

                                                    // if (selectedshape.attrs.LOA < item.LOA() && item.MovementStatus() == "BERT") {
                                                    //if (selectedshape.LOA() < item.LOA() && item.MovementStatus() == "BERT") {
                                                    // if (selectedshape.LOA() < item.LOA()) {
                                                    DoubleBankSuitableVessels.push(item);
                                                    //}
                                                }
                                            });


                                            var BankingVessel;
                                            if (DoubleBankSuitableVessels.length == 1) {
                                                BankingVessel = DoubleBankSuitableVessels[0];
                                                if (BankingVessel.IsBanked() == 0) {
                                                    canDoubleBank = true;
                                                }
                                                else {
                                                    canDoubleBank = false;
                                                }

                                            }

                                            else if (DoubleBankSuitableVessels.length == 2) {
                                                canDoubleBank = true;
                                                if (DoubleBankSuitableVessels[0].LOA() < DoubleBankSuitableVessels[1]) {
                                                    BankingVessel = DoubleBankSuitableVessels[0];
                                                }
                                                else {
                                                    BankingVessel = DoubleBankSuitableVessels[1];

                                                }

                                            }

                                            if (canDoubleBank == true) {

                                                bootbox.confirm("Do you want to Bank " + _VCN + " on " + BankingVessel.VCN(), function (result) {
                                                    if (result) {

                                                        ForDoubleBanking = true;

                                                        for (i = 0; i < self.plannedvessesls().length; i++) {
                                                            if (self.plannedvessesls()[i].VCN() == BankingVessel.VCN() && self.plannedvessesls()[i].VesselCallMovementID() == BankingVessel.VesselCallMovementID()) {

                                                                //self.plannedvessesls()[i].DoubleBankedVessel(selectedshape.VCN()); // Commented by sandeep on 28-05-2015
                                                                self.plannedvessesls()[i].DoubleBankedVessel(selectedshape.VCN()); // Added by sandeep on 28-05-2015
                                                                self.plannedvessesls()[i].DoubleBankedVessel(selectedshape.VesselCallMovementID());
                                                                var Bank = self.plannedvessesls()[i].IsBanked() + 1;
                                                                self.plannedvessesls()[i].IsBanked(Bank);

                                                                $.each(self.vesselShapes(), function (index, item) {
                                                                    //console.log(item);
                                                                    if (item.Id() == self.plannedvessesls()[i].VCN() && item.VCMID() == self.plannedvessesls()[i].VesselCallMovementID()) {
                                                                        var line = item.line;
                                                                        line.attrs.stroke = 'red';
                                                                        line.attrs.opacity = .1,
                                                                        line.draggable(false);
                                                                        var layer = line.getLayer();
                                                                        GridLayer.draw();
                                                                    }

                                                                });

                                                                selectedshape.FromPortCode(fromberth.PortCode());
                                                                selectedshape.ToPortCode(fromberth.PortCode());
                                                                selectedshape.FromQuayCode(fromberth.QuayCode());
                                                                selectedshape.ToQuayCode(fromberth.QuayCode());
                                                                selectedshape.FromBerthCode(fromberth.BerthCode());
                                                                selectedshape.ToBerthCode(toberth.BerthCode());
                                                                selectedshape.FromBollardCode(frombollard.BollardCode());
                                                                selectedshape.ToBollardCode(tobollard.BollardCode());
                                                                selectedshape.FromBollardMeter(frombollard.FromMeter());
                                                                selectedshape.ToBollardMeter(tobollard.FromMeter());
                                                                selectedshape.BerthName(fromberth.BerthName());
                                                                selectedshape.FromBollardName(frombollard.BollardName());
                                                                selectedshape.ToBollardName(tobollard.BollardName());
                                                                selectedshape.PositionX(frombollard.FromMeter());
                                                                selectedshape.PositionY(self.layery() / heightpic);
                                                                selectedshape.MovementStatus("SCH");
                                                                self.plannedvessesls.push(selectedshape);
                                                                self.unplannedvessels.remove(selectedshape);
                                                                var shape = new Shape(selectedshape, 0);
                                                                self.vesselShapes.push(shape);
                                                                self.DrawShape(shape);

                                                                //selectedshape.FromPortCode(self.plannedvessesls()[i].FromPortCode());
                                                                //selectedshape.ToPortCode(self.plannedvessesls()[i].FromPortCode());
                                                                //selectedshape.FromQuayCode(self.plannedvessesls()[i].FromQuayCode());
                                                                //selectedshape.ToQuayCode(self.plannedvessesls()[i].FromQuayCode);
                                                                //selectedshape.FromBerthCode(self.plannedvessesls()[i].FromBerthCode());
                                                                //selectedshape.ToBerthCode(self.plannedvessesls()[i].ToBerthCode());
                                                                //selectedshape.FromBollardCode(self.plannedvessesls()[i].FromBollardCode());
                                                                //selectedshape.ToBollardCode(self.plannedvessesls()[i].ToBollardCode());
                                                                //selectedshape.FromBollardMeter(self.plannedvessesls()[i].FromBollardMeter());
                                                                //selectedshape.BerthName(self.plannedvessesls()[i].BerthName());
                                                                //selectedshape.ToBollardMeter(self.plannedvessesls()[i].ToBollardMeter());
                                                                //selectedshape.PositionX(self.plannedvessesls()[i].FromBollardMeter());
                                                                //selectedshape.PositionY(self.layery() / heightpic);
                                                                //selectedshape.MovementStatus("SCH");
                                                                //self.plannedvessesls.push(selectedshape);
                                                                //self.unplannedvessels.remove(selectedshape);
                                                                //var shape = new Shape(selectedshape, 0);
                                                                //self.vesselShapes.push(shape);
                                                                //self.DrawShape(shape);
                                                            }
                                                        }
                                                    }

                                                });

                                            }
                                            else {
                                                bootbox.alert('Vessel is not yet berthed. Hence double bank is not possible on this vessel');

                                            }

                                        }

                                    }


                                }

                                else {
                                    Msg = "";
                                    if (getBerths.AreContinousBollards == false)
                                        Msg = Msg + "<li>Bollards are discontinous.</li>";

                                    $.each(self.berthsWithBollards(), function (index, item) {
                                        var notSuited = $.inArray(item.BerthCode(), BerthsStatusList);
                                        if (notSuited != -1) {
                                            var failed = getfailedBerthingRules(selectedshape, item);
                                            Msg = getRulesMsg(item.BerthName(), failed);
                                        }

                                    });
                                    bootbox.alert(Msg);
                                }
                            }
                            else {
                                bootbox.alert('Vessel ETB must be greater than current time.');
                            }
                        }
                    }

                }
            }

        }

        // End Old DragEnd

        self.allowDrop = function (event) {

            event.preventDefault();
            event.stopPropagation();
        }


        function getRulesMsg(berth, valid) {
            var Msg = "";
            if (valid.DraftValid == false)
                Msg = Msg + "<li>" + valid.DraftRuleMsg + "</li>";
            if (valid.CargoTypeValid == false)
                Msg = Msg + "<li>" + valid.CargoRuleMsg + "</li>";
            if (valid.TOBerthValid == false)
                Msg = Msg + "<li>" + valid.TORuleMsg + "</li>";
            return Msg;
        }



        function RulesAlert(fromberth, toberth, frombollard, tobollard, isFromToBerthSame, fromberthvalid, toberthvalid, isContinous, isAvailable, IsShiftingAvailable) {

            var Msg = "";
            if (frombollard.QuayCode() != tobollard.QuayCode())
                Msg = Msg + "<li>Berth is not suitable to place a vessel between Quays</li>";
            if (isContinous == false)
                Msg = Msg + "<li>Berth is not suitable to as Bollard are discontinous.</li>";
            if (isAvailable == false)
                Msg = Msg + "<li>Berth is not available.</li>";
            if (IsShiftingAvailable == false)
                Msg = Msg + "<li>Shifting is not possible.</li>";


            if (fromberthvalid.DraftValid == false)
                Msg = Msg + "<li>" + fromberth.BerthName() + " Draft is not valid</li>";
            if (fromberthvalid.TOBerthValid == false)
                Msg = Msg + "<li>" + fromberth.BerthName() + " Terminal Operator is not Valid</li>";
            if (fromberthvalid.CargoTypeValid == false)
                Msg = Msg + "<li>" + fromberth.BerthName() + " Cargo Type is not valid</li>";

            if (isFromToBerthSame == false) {
                if (toberthvalid.DraftValid == false)
                    Msg = Msg + "<li>" + toberth.BerthName() + " Draft is not valid</li>";
                if (toberthvalid.TOBerthValid == false)
                    Msg = Msg + "<li>" + toberth.BerthName() + " Terminal Operator is not valid</li>";
                if (toberthvalid.CargoTypeValid == false)
                    Msg = Msg + "<li>" + toberth.BerthName() + " Cargo Type is not valid</li>";
            }


            return Msg;
        }


        //Get berth of a particular bollard
        function GetBerth(bollardcode, berthcode, quaycode) {
            var berth;
            for (j = 0; j < self.berthsWithBollards().length; j++) {
                for (var i = 0; i < (self.berthsWithBollards()[j]).Bollards().length; i++) {
                    if (bollardcode == (self.berthsWithBollards()[j]).Bollards()[i].BollardCode() && berthcode == (self.berthsWithBollards()[j]).Bollards()[i].BerthCode() && quaycode == (self.berthsWithBollards()[j]).Bollards()[i].QuayCode()) {
                        berth = (self.berthsWithBollards())[j];
                    }
                }
            }
            return berth;
        }


        function GetBerthPort(bollardcode) {
            var berth;

            for (j = 0; j < self.quayberthsWithBollards().length; j++) {
                for (var i = 0; i < (self.quayberthsWithBollards()[j]).Berths().length; i++) {
                    for (var k = 0; k < (self.quayberthsWithBollards()[j]).Berths()[i].Bollards().length; k++) {
                        if (bollardcode == self.quayberthsWithBollards()[j].Berths()[i].Bollards()[k].BollardCode()) {
                            berth = self.quayberthsWithBollards()[j].Berths()[i];

                        }
                    }

                }
            }
            return berth;
        }
        //function to get frombollard based on position
        function GetFromBollard(position) {

            var frombollard = {};
            for (i = 0; i < self.berthsWithBollards().length; i++) {
                var length = (self.berthsWithBollards()[i]).Bollards().length;
                for (j = 0; j < length; j++) {

                    if ((self.berthsWithBollards()[i]).Bollards()[j].FromMeter() <= position && position < (self.berthsWithBollards()[i]).Bollards()[j].ToMeter()) {
                        frombollard = ((self.berthsWithBollards()[i]).Bollards()[j]);
                    }

                }
            }
            return frombollard;
        }

        function GetFromBollardPort(position) {

            var PreviousQuayLength = 0;
            var QuayLength = 0;
            var PreviousBerthLength = 0;
            var SelectedIndex = 0;
            var berth = 0;
            var frombollardindex;
            var frombollard = {};
            var tobollard = {}
            var bollards = [];
            for (i = 0; i < self.quayberthsWithBollards().length; i++) {
                PreviousBerthLength = 0;
                PreviousQuayLength = PreviousQuayLength + self.quayberthsWithBollards()[i].QuayLength();
                if (PreviousQuayLength >= position) {
                    SelectedIndex = i;
                    break;
                }
            }

            var QuayBerths = self.quayberthsWithBollards()[SelectedIndex].Berths();
            var QuayBerthLength;
            if (i != 0)
                QuayBerthLength = PreviousQuayLength - self.quayberthsWithBollards()[SelectedIndex].QuayLength();
            else
                QuayBerthLength = 0;
            for (j = 0; j < QuayBerths.length; j++) {
                PreviousBerthLength = PreviousBerthLength + QuayBerths[j].BerthLength();
                if (PreviousBerthLength + QuayBerthLength >= position) {
                    berth = QuayBerths[j];
                    var TotalLength = QuayBerthLength + (PreviousBerthLength - QuayBerths[j].BerthLength());
                    var Position = position;
                    var BollardLength = 0;
                    for (k = 0; k < QuayBerths[j].Bollards().length; k++) {
                        var rest = Position - TotalLength;
                        var length = QuayBerths[j].Bollards()[k].ToMeter() - QuayBerths[j].Bollards()[k].FromMeter();
                        BollardLength = BollardLength + length;
                        if (rest <= BollardLength) {
                            frombollard = QuayBerths[j].Bollards()[k];
                            frombollardindex = k;
                            //console.log('from', frombollard.BollardCode());
                            break;
                        }
                    }
                    break;
                }
            }
            //   var totalbollardlength = 0;
            //   for (l = frombollardindex; l < berth.Bollards().length; l++) {                  
            //           var bollardlength = berth.Bollards()[l].ToMeter() - berth.Bollards()[l].FromMeter();
            //           totalbollardlength = bollardlength + totalbollardlength;
            //           if (VesselLength <= totalbollardlength) {
            //               //console.log('Tobollard', berth.Bollards()[l].BollardCode());
            //               tobollard = berth.Bollards()[l]
            //               break;
            //           }                
            //   }

            ////   GetToBollardPort(position, VesselLength);

            //   bollards.push(frombollard);
            ////   bollards.push(tobollard);
            return frombollard;


        }


        function GetToBollardPort(position) {
            var PreviousQuayLength = 0;
            var QuayLength = 0;
            var PreviousBerthLength = 0;
            var SelectedIndex = 0;
            var berth = 0;
            var tobollard = {}
            for (i = 0; i < self.quayberthsWithBollards().length; i++) {
                PreviousBerthLength = 0;
                PreviousQuayLength = PreviousQuayLength + self.quayberthsWithBollards()[i].QuayLength();
                if (PreviousQuayLength >= position) {
                    SelectedIndex = i;
                    break;
                }
            }

            var QuayBerths = self.quayberthsWithBollards()[SelectedIndex].Berths();
            var QuayBerthLength;
            if (i != 0)
                QuayBerthLength = PreviousQuayLength - self.quayberthsWithBollards()[SelectedIndex].QuayLength();
            else
                QuayBerthLength = 0;
            for (j = 0; j < QuayBerths.length; j++) {
                PreviousBerthLength = PreviousBerthLength + QuayBerths[j].BerthLength();
                if (PreviousBerthLength + QuayBerthLength >= position) {
                    berth = QuayBerths[j];
                    var TotalLength = QuayBerthLength + (PreviousBerthLength - QuayBerths[j].BerthLength());
                    var Position = position;
                    var BollardLength = 0;
                    for (k = 0; k < QuayBerths[j].Bollards().length; k++) {
                        var rest = Position - TotalLength;
                        var length = QuayBerths[j].Bollards()[k].ToMeter() - QuayBerths[j].Bollards()[k].FromMeter();
                        BollardLength = BollardLength + length;
                        if (rest <= BollardLength) {
                            tobollard = QuayBerths[j].Bollards()[k];
                            break;
                        }
                    }
                    break;
                }
            }
            return tobollard;
        }

        //function to get tobollard based on position
        function GetToBollard(position) {
            var tobollard;
            for (i = 0; i < self.berthsWithBollards().length; i++) {
                var length = (self.berthsWithBollards()[i]).Bollards().length;
                for (j = 0; j < length; j++) {
                    if ((self.berthsWithBollards()[i]).Bollards()[j].FromMeter() < position && position <= (self.berthsWithBollards()[i]).Bollards()[j].ToMeter()) {
                        tobollard = ((self.berthsWithBollards()[i]).Bollards()[j]);
                    }


                }
            }
            return tobollard;
        }


        function CheckDiscontinous(frombollard, tobollard) {

            var berthsWithBollards;
            var QuayBollards = [];
            QuayBollards = GetQuayBollards(frombollard.QuayCode());
            var isDiscontinous = false;
            var isfrombollard = false;
            var istobollard = false;
            for (var i = 0; i < QuayBollards.length; i++) {
                if (isfrombollard) {
                    if (!istobollard) {
                        if (QuayBollards[i].Continous()() == 'N') {
                            isDiscontinous = true;
                            break;
                        }
                    }
                    else {
                        break;
                    }
                }

                if (QuayBollards[i].BollardCode()() == frombollard.BollardCode())
                    isfrombollard = true;
                if (QuayBollards[i].BollardCode()() == tobollard.BollardCode())
                    istobollard = true;

            }
            return isDiscontinous
        }

        function GetQuayBollards(QuayName) {
            var Berths;
            var QuayBollards = [];
            $.each(self.quayberthsWithBollards(), function (index, item) {
                if (QuayName == item.QuayCode()) {
                    var Berths = item.Berths();
                    $.each(Berths, function (index, berth) {
                        bollards = berth.Bollards();
                        $.each(bollards, function (index, bollard) {
                            //  self.quayBollards.push(new IPMSRoot.Bollard(bollard));
                            QuayBollards.push(new IPMSRoot.Bollard(bollard));
                        });
                    });

                }
            });


            return QuayBollards
        }

        //function CheckDiscontinousBollardBetweenBollards(frombollard , tobollard) {
        //    var status = true;
        //    var exitloop = true;
        //    var berthsum = 0;
        //    var bollardList;
        //    var frombollard = false;
        //    $.each(self.berthsWithBollards(), function (index, item) {               
        //        var bollards = item.Bollards();
        //        $.each(bollards, function (index, bollard) {
        //            if (bollard.BollardCode() == frombollard.BollardCode())
        //            {
        //                bollardList.push(bollard);
        //                frombollard = true;
        //            }
        //            return exitloop;
        //        });

        //        berthsum = berthsum + item.BerthLength();
        //        return exitloop;
        //    });
        //    return status;
        //}





        //function to check the discontinous bollard in between from and to bollard
        function CheckDiscontinousBollard(startposition, endposition) {
            var status = true;
            var exitloop = true;
            var berthsum = 0;
            $.each(self.berthsWithBollards(), function (index, item) {

                //if (berthsum >= startposition)
                //{
                var bollards = item.Bollards();
                $.each(bollards, function (index, bollard) {
                    if (index > 0) {
                        if (bollard.FromMeter() >= startposition && bollard.FromMeter() <= endposition) {
                            if (bollard.Continous() == "N") {
                                status = false;
                                exitloop = false;
                            }
                        }
                        //else if(endposition<=item.BerthLength()) {
                        //    exitloop = false;
                        //}
                    }
                    return exitloop;
                });
                //}
                berthsum = berthsum + item.BerthLength();
                return exitloop;
            });
            return status;
        }


        ////Update vessel information in vesselcallmovement table.
        //self.UpdateVesselInformation = function (plannedvessel) {

        //    self.viewModelHelper.apiPut('api/UpdateVesselCallMovement', ko.mapping.toJSON(plannedvessel), function Message(data) {

        //    });
        //}

        // Binding Quays , Berth and Bollards Data to the Grid
        self.LoadGridDatawithQuays = function (data, GridWidth) {

            var numberOfQuays = data.length;

            var noOfShortLines = numberOfQuays;
            var previousQuaysLength = 0;
            for (var i = 0; i < numberOfQuays; i++) {
                var quaylength = 0;
                var textdisplaydistance = 0;
                quaylength = previousQuaysLength + data[i].QuayLength();
                textdisplaydistance = previousQuaysLength + data[i].QuayLength() / 3;
                previousQuaysLength = quaylength;
                var I = (widthpic * quaylength);
                I = Math.round(I);
                TotPicSize = I;

                // Quay Short Line 
                var QuayShortLine = new Kinetic.Line({
                    points: [I, 0, I, 10],
                    stroke: 'black',
                    strokeWidth: 2,
                    fill: 'black',
                    closed: false,
                    id: data[i].QuayCode(),
                    name: "Quay Name:" + data[i].QuayName() + "\n" + "Quay Length:" + data[i].QuayLength() + "\n"
                });
                //Adding Quay Line
                var QuayLongLine = new Kinetic.Line({
                    points: [I, 0, I, 800],
                    stroke: 'black',
                    strokeWidth: 2,
                    fill: 'black',
                    closed: false,
                    id: data[i].QuayCode(),
                    name: "Quay Name:" + data[i].QuayName() + "\n" + "Quay Length:" + data[i].QuayLength() + "\n"

                });



                QuayShortLine.on("mouseover", function (event) {
                    tooltip.setContent(this.name());
                    tooltip.currentPosition.left = event.pageX;
                    tooltip.currentPosition.top = event.pageY;
                    tooltip.show();

                });
                QuayShortLine.on("mouseout", function (event) {
                    tooltip.hide();
                });

                QuayLongLine.on("mouseover", function (event) {
                    tooltip.setContent(this.name());
                    tooltip.currentPosition.left = event.pageX;
                    tooltip.currentPosition.top = event.pageY;
                    tooltip.show();

                });
                QuayLongLine.on("mouseout", function (event) {
                    tooltip.hide();
                });

                BerthLayer.add(QuayShortLine);
                GridLayer.add(QuayLongLine);
                var K = (widthpic * textdisplaydistance);
                K = Math.round(K);
                BerthLayer.add(addquayText(berthText, K, data[i].QuayCode()));
                BerthLayer.add(berthText);
                BerthLayer.draw();
            }


            function addquayText(control, index, text) {
                control = control.clone({ text: text, x: index });
                control.offsetX(control.height() / 2);
                return control;
            }



            //**************************************** Berths *****************************

            var numberOfBollards = 0;
            var previousBerthsLength = 0;
            var previousBollardsLength = 0;
            for (j = 0; j < numberOfQuays; j++) {
                numberOfBerths = data[j].Berths().length;
                for (var k = 0; k < numberOfBerths; k++) {
                    var berthlength = 0;
                    var textdisplaydistance = 0;
                    berthlength = previousBerthsLength + data[j].Berths()[k].BerthLength();
                    textdisplaydistance = previousBerthsLength + data[j].Berths()[k].BerthLength() / 3;

                    var I = (widthpic * berthlength);
                    I = Math.round(I);
                    TotPicSize = I;
                    // Quay Short Line 
                    var BerthShortLine = new Kinetic.Line({
                        points: [I, 0, I, 10],
                        stroke: 'blue',
                        strokeWidth: 2,
                        fill: 'blue',
                        closed: false,
                        id: data[j].Berths()[k].BerthCode(),
                        name: "Quay Name:" + data[j].Berths()[k].BerthName() + "\n" + "Quay Length:" + data[j].Berths()[k].BerthLength() + "\n"
                    });
                    BollardLayer.add(BerthShortLine);
                    BollardLayer.draw();
                    previousBerthsLength = berthlength;
                }

                function addbollardText(control, index, text) {
                    control = control.clone({ text: text, x: index });
                    control.offsetX(control.height() / 2);
                    return control;
                }

            }
            //***************************** Display Time & Lines *********************************
            self.DisplayTimeLines();
            GridLayer.draw();
        }


        //3. Binding Berths and Bollards Data to Grid     
        self.LoadGridData = function (data, GridWidth) {
            var numberOfBerths = data.length;
            var noOfBerthShortLines = numberOfBerths;
            var previousBerthsLength = 0;
            for (var i = 0; i < noOfBerthShortLines; i++) {
                var berthlength = 0;
                var textdisplaydistance = 0;

                berthlength = previousBerthsLength + data[i].BerthLength();
                textdisplaydistance = previousBerthsLength + data[i].BerthLength() / 3;

                previousBerthsLength = berthlength;
                var I = (widthpic * berthlength);
                I = Math.round(I);
                TotPicSize = I;

                // Berth Short Line 
                var BerthShortLine = new Kinetic.Line({
                    points: [I, 0, I, 10],
                    stroke: 'black',
                    strokeWidth: 2,
                    fill: 'black',
                    closed: false,
                    id: data[i].BerthCode(),
                    name: "BerthName:" + data[i].BerthName() + "\n" + "BerthLength:" + data[i].BerthLength() + "\n"
                });

                //Adding Berth Line
                var BerthLongLine = new Kinetic.Line({
                    points: [I, 0, I, 800],
                    stroke: 'black',
                    strokeWidth: 2,
                    fill: 'black',
                    closed: false,
                    id: data[i].BerthCode(),
                    name: "BerthName:" + data[i].BerthName() + "\n" + "BerthLength:" + data[i].BerthLength() + "\n"

                });
                BerthShortLine.on("mouseover", function (event) {
                    tooltip.setContent(this.name());
                    tooltip.currentPosition.left = event.pageX;
                    tooltip.currentPosition.top = event.pageY;
                    tooltip.show();

                });
                BerthShortLine.on("mouseout", function (event) {
                    tooltip.hide();
                });

                BerthLongLine.on("mouseover", function (event) {
                    tooltip.setContent(this.name());
                    tooltip.currentPosition.left = event.pageX;
                    tooltip.currentPosition.top = event.pageY;
                    tooltip.show();

                });
                BerthLongLine.on("mouseout", function (event) {
                    tooltip.hide();
                });
                BerthLayer.add(BerthShortLine);
                GridLayer.add(BerthLongLine);
                var K = (widthpic * textdisplaydistance);
                K = Math.round(K);



                BerthLayer.add(addberthText(berthText, K, data[i].BerthName()));
                BerthLayer.add(berthText);

                BerthLayer.draw();
            }


            function addberthText(control, index, text) {
                control = control.clone({ text: text, x: index });
                control.offsetX(control.height() / 2);
                return control;
            }



            //**************************************** Bollards *****************************



            var numberOfBollards = 0;
            for (j = 0; j < numberOfBerths; j++) {
                numberOfBollards = data[j].Bollards().length;
                for (var i = 0; i < numberOfBollards; i++) {
                    var currentBollardlength = parseInt((data[j].Bollards())[i].FromMeter());
                    var textdisplaydistance = currentBollardlength + ((data[j].Bollards())[i].ToMeter() - (data[j].Bollards())[i].FromMeter()) / 6;

                    var K = (widthpic * currentBollardlength);
                    K = Math.round(K);
                    TotPicSize = K;
                    var BollardgridLine = new Kinetic.Line({
                        points: [K, 0, K, 10],
                        stroke: (data[j].Bollards())[i].Continous() == 'N' ? 'red' : 'blue',
                        strokeWidth: 2,
                        fill: 'black',
                        closed: false,
                        id: (data[j].Bollards())[i].BollardCode(),
                        name: "BollardCode:" + (data[j].Bollards())[i].BollardCode() + "\n" + "BollardName:" + (data[j].Bollards())[i].BollardName()
                    });

                    //bollard line tooltip
                    BollardgridLine.on("mouseover", function (event) {
                        tooltip.setContent(this.name());
                        tooltip.currentPosition.left = event.layerX;
                        tooltip.currentPosition.top = event.layerY;
                        tooltip.show();
                    });
                    BollardgridLine.on("mouseout", function (event) {
                        tooltip.hide();
                    });
                    BollardLayer.add(BollardgridLine);

                    var I = (widthpic * textdisplaydistance);
                    I = Math.round(I);
                    //  BollardLayer.add(addbollardText(bollardText, I, (data[j].Bollards())[i].BollardCode()));
                    //   BollardLayer.add(bollardText);
                    BollardLayer.draw();

                }


                function addbollardText(control, index, text) {
                    control = control.clone({ text: text, x: index });
                    control.offsetX(control.height() / 2);
                    return control;
                }

            }

            //***************************** Display Time & Lines *********************************
            self.DisplayTimeLines();
            GridLayer.draw();
        }

        //Loading Grid with Bollards of the berth on berth selection

        self.LoadGridWithBollards = function (data, GridWidth, prevberthslength) {
            var numberOfBerths = 1;
            var widthpic = GridWidth / data.BerthLength();
            var noOfBerthShortLines = numberOfBerths;
            var previousBerthsLength = 0;
            for (var i = 0; i < noOfBerthShortLines; i++) {
                var berthlength = 0;
                var textdisplaydistance = 0;

                berthlength = previousBerthsLength + data.BerthLength();
                textdisplaydistance = previousBerthsLength + data.BerthLength() / 3;
                previousBerthsLength = berthlength;
                var I = (widthpic * berthlength);
                I = Math.round(I);
                TotPicSize = I;


                // Berth Short Line 
                var BerthShortLine = new Kinetic.Line({
                    points: [I, 0, I, 10],
                    stroke: 'black',
                    strokeWidth: 1,
                    fill: 'black',
                    closed: false,
                });

                //Adding Berth Line
                var BerthLongLine = new Kinetic.Line({
                    points: [I, 0, I, 800],
                    stroke: 'black',
                    strokeWidth: 1,
                    fill: 'black',
                    closed: false,

                });
                //berth line tooltip
                BerthShortLine.on("mouseover", function (event) {

                    tooltip.setContent(this.name());
                    tooltip.currentPosition.left = event.pageX;
                    tooltip.currentPosition.top = event.pageY;
                    tooltip.show();

                });
                BerthShortLine.on("mouseout", function (event) {
                    tooltip.hide();
                });

                BerthLongLine.on("mouseover", function (event) {

                    tooltip.setContent(this.name());
                    tooltip.currentPosition.left = event.pageX;
                    tooltip.currentPosition.top = event.pageY;
                    tooltip.show();

                });
                BerthLongLine.on("mouseout", function (event) {
                    tooltip.hide();
                });
                BerthLayer.add(BerthShortLine);
                GridLayer.add(BerthLongLine);

                var K = (widthpic * textdisplaydistance);
                K = Math.round(K);

                BerthLayer.add(addberthText(berthText, K, data.BerthName()));
                BerthLayer.add(berthText);
                BerthLayer.draw();
            }

            function addberthText(control, index, text) {
                control = control.clone({ text: text, x: index });
                control.offsetX(control.height() / 2);
                return control;
            }


            //**************************************** Bollards *****************************

            var numberOfBollards = 0;
            for (j = 0; j < numberOfBerths; j++) {
                numberOfBollards = data.Bollards().length;
                for (var i = 0; i < numberOfBollards; i++) {

                    var currentBollardlength = parseInt((data.Bollards())[i].FromMeter() - prevberthslength);
                    var textdisplaydistance = currentBollardlength + ((data.Bollards())[i].ToMeter() - (data.Bollards())[i].FromMeter()) / 6;
                    var K = (widthpic * currentBollardlength);
                    K = Math.round(K);
                    TotPicSize = K;
                    var BollardgridLine = new Kinetic.Line({
                        points: [K, 0, K, 10],
                        stroke: (data.Bollards())[i].Continous() == 'N' ? 'red' : 'blue',
                        strokeWidth: 2,
                        fill: 'black',
                        closed: false,
                        id: (data.Bollards())[i].BollardCode(),
                        name: "BollardCode:" + (data.Bollards())[i].BollardCode() + "\n" + "BollardName:" + (data.Bollards())[i].BollardName()
                    });

                    //bollard line tooltip
                    BollardgridLine.on("mouseover", function (event) {
                        tooltip.setContent(this.name());
                        tooltip.currentPosition.left = event.layerX;
                        tooltip.currentPosition.top = event.layerY;
                        tooltip.show();
                    });
                    BollardgridLine.on("mouseout", function (event) {
                        tooltip.hide();
                    });
                    BollardLayer.add(BollardgridLine);

                    var I = (widthpic * textdisplaydistance);
                    I = Math.round(I);
                    //   BollardLayer.add(addbollardText(bollardText, I, (data.Bollards())[i].BollardCode()));
                    //   BollardLayer.add(bollardText);
                    BollardLayer.draw();

                }
                function addbollardText(control, index, text) {
                    control = control.clone({ text: text, x: index });
                    control.offsetX(control.height() / 2);
                    return control;

                }

            }

            //***************************** Display Time & Lines *********************************
            self.DisplayTimeLines();
            GridLayer.draw();
        }

        self.DrawShape = function (shape) {


            //function theyAreColliding(c1, c2) {
            //    return Math.pow(c1.getX() - c2.getX(), 2) + Math.pow(c1.getY() - c2.getY(), 2) < Math.pow(c2.getRadius(), 2) / 2;
            //}
            function theyAreColliding(rect1, rect2) {
                if (rect2.aX() > rect1.aX() && rect2.aX() < (rect1.aX() + rect1.width()))
                    return true;
                else
                    return false;
            }

            function colliding(rec1, rec2) {
                var status = false;
                var rec1Top = rec1.aY();
                var rec1Bottom = rec1.aY() + rec1.length();
                var rec1Left = rec1.aX();
                var rec1Right = rec1.aX() + rec1.width();

                var rec2Top = rec2.aY();
                var rec2Bottom = rec2.aY() + rec2.length();
                var rec2Left = rec2.aX();
                var rec2Right = rec2.aX() + rec2.width();

                if (!(rec1Bottom < rec2Top ||
                 rec1Top > rec2Bottom ||
                 rec1Left > rec2Right ||
                 rec1Right < rec2Left))

                    status = true;

                return status;
            }



            //self.DrawOnLayer = function () {
            //for (var i = 0  ; i <shapeObjs.length; i++) {
            //var shape = shapeObjs[i];
            // var isdraggable = shape.linecolor() == "blue" ? false : true;

            var line = shape.line;
            // line.setPosition({ x: parseInt(shape.aX()), y: parseInt(shape.aY()) });
            // line.setPosition({ x: parseInt(self.layerx()), y: parseInt(self.layery()) });
            // line.draggable(isdraggable);


            GridLayer.add(line);
            GridLayer.draw();
            //}

            //};
            self.removeShape = function (gift) {
                self.shapes.remove(gift);
            };
            self.clearshapes = function () {
                GridLayer.remove();
            };

            //self.DrawOnLayer();

        }

        self.DrawMaintShape = function (shape) {
            var line = shape.line;
            GridLayer.add(line);
            GridLayer.draw();
        }

        // Time Lines
        self.DisplayTimeLines = function () {
            var selectedDate = new Date(self.SelectedDate());
            var currentHour;
            selectedDate.setHours(0);
            var disdate = moment(self.SelectedDate()).format('MM-DD');
            var count = 0;

            for (var i = 0; i < noOftimerLines; i++) {
                var H = heightpic * i;
                var gridShortTimerLine = new Kinetic.Line({
                    points: [0, H, 50, H],
                    stroke: 'black',
                    strokeWidth: .5,
                    fill: 'black',
                    closed: false,
                    text: 'dsdfsf',

                });
                TimerLayer.add(gridShortTimerLine);

                //Adding Vertical GridLines
                var gridLongTimerLine = new Kinetic.Line({
                    points: [0, H, 2000, H],
                    stroke: '#555555',
                    strokeWidth: .5,
                    fill: 'black',
                    closed: false,

                });
                GridLayer.add(gridLongTimerLine);
                var color = '#CCC';
                var sttime = selectedDate.getHours() + (i * nohours);
                if (sttime >= 24 && sttime < 48) {
                    sttime = sttime - 24;
                }
                else if (sttime >= 48) {
                    sttime = sttime - 48;

                }
                if (sttime == 0) {
                    sttime = disdate;
                    count = count + 1;
                    disdate = moment(selectedDate).add('days', count).format('MM-DD');
                    color = 'red';
                }
                else {
                    sttime = sttime + ":00";
                }

                var ht = i * heightpic;
                TimerLayer.add(addTimeStatusText(TimerText, ht, sttime, color));
                TimerLayer.add(TimerText);
            }

            TimerLayer.draw();
            function addTimeStatusText(control, index, text, color) {
                control = control.clone({ text: text, y: index, fill: color });
                control.offsetY((control.height() / 2) + 5);

                return control;
            }
        }


        function UnScheduleVessel(id) {
            $.each(self.plannedvessesls(), function (index, item) {

                if (item != undefined) {
                    if (item.VesselCallMovementID() == id) {

                        item.FromPortCode(null);
                        item.ToPortCode(null);
                        item.FromQuayCode(null);
                        item.ToQuayCode(null);
                        item.FromBerthCode(null);
                        item.ToBerthCode(null);
                        item.FromBollardCode(null);
                        item.FromBollardMeter(null);
                        item.ToBollardMeter(null);
                        item.ToBollardCode(null);
                        item.FromBollardName(null);
                        item.ToBollardName(null);
                        item.BerthName(null);
                        item.MovementStatus("MPEN");
                        //self.unplannedvessels.push(item);
                        self.unplannedvessels.splice(0, 0, item);
                        self.plannedvessesls.remove(item);



                        var vesselid;
                        $.each(self.plannedvessesls(), function (index, item) {
                            if (item.DoubleBankedVessel() == id) {
                                vesselid = item.VesselCallMovementID();
                                var banked = item.IsBanked() - 1;
                                item.IsBanked(banked);
                                item.DoubleBankedVessel(null);
                            }
                        });

                        $.each(self.vesselShapes(), function (index, value) {
                            //console.log(item);
                            if (value.VCMID() == vesselid) {
                                var line = value.line;
                                if (line.name().MovementStatus() == 'SCH') {
                                    line.attrs.stroke = 'black';
                                    line.attrs.opacity = .1,
                                    line.draggable(true);
                                    var layer = line.getLayer();
                                    GridLayer.draw();
                                }
                                else {
                                    line.attrs.stroke = 'black';
                                    line.attrs.opacity = .1;
                                    var layer = line.getLayer();
                                    GridLayer.draw();
                                }
                            }
                        });
                    }
                }
            })
        }

        //updating status on clicking the individual shape
        function ConfirmVessel(id) {
            $.each(self.plannedvessesls(), function (index, item) {
                if (item.VesselCallMovementID() == id) {
                    item.MovementStatus("CONF");
                }
            });
        }

        //function to pass data to berthing rules to verify on draggin a shape
        function passDataToBerthingRules(vcn, berth) {
            var valid;
            $.each(self.plannedvessesls(), function (index, item) {
                if (item.VCN() == vcn) {
                    var userdata = self.LoggedUserData();
                    valid = self.berthruleshelper.CheckBerthingRules(item, berth, self.LoggedUserData(), self.Configurations().UnderKeelClearance());
                }

            });
            return valid;
        }


        function getfailedBerthingRules(vessel, berth) {
            var userdata = self.LoggedUserData();
            valid = self.berthruleshelper.CheckBerthingRules(vessel, berth, self.LoggedUserData(), self.Configurations().UnderKeelClearance());
            return valid;
        }





        //function to update the modified data on dragging shape for planned vessel after berth rules validated
        function updatePlannedItem(VCMID, vcn, movementstatus, fromportcode, fromquaycode, fromberthcode, toberthcode, frombollardcode, tobollardcode, x, tobollardFromMeter, y1, BerthName, toBerthName, fromBollardName, toBollardName) {
            var modifiedvessel;

            $.each(self.plannedvessesls(), function (index, item) {
                if (item.VesselCallMovementID() == VCMID) {
                    item.FromPortCode(fromportcode);
                    item.ToPortCode(fromportcode);
                    item.FromQuayCode(fromquaycode);
                    item.ToQuayCode(fromquaycode);
                    item.FromBerthCode(fromberthcode);
                    item.BerthName(BerthName);

                    item.ToBerthCode(toberthcode);
                    item.FromBollardCode(frombollardcode);
                    item.ToBollardCode(tobollardcode);
                    item.FromBollardMeter(x);
                    item.ToBollardMeter(tobollardFromMeter);
                    item.FromBollardName(fromBollardName);
                    item.ToBollardName(toBollardName);
                    item.PositionX(x);
                    var DraggedTime = y1;
                    var SetTime = moment(self.SelectedDate()).startOf('day');
                    item.PositionY(y1);
                    var eta = y1 >= 24 ? y1 - 24 : y1;

                    var ETA1 = new Date((new Date(item.BerthTime()).setHours(eta)));
                    var ETD1 = new Date((new Date(item.BerthTime()).setHours(eta + item.VesselWidth())));
                    var DraggedBerthTime = new Date((new Date(SetTime).setHours(DraggedTime)));
                    var DraggedUnBerthTime = new Date((new Date(SetTime).setHours(DraggedTime + item.Diff())));
                    item.ETB(moment(DraggedBerthTime).format('YYYY-MM-DD HH:mm'));
                    item.ETUB(moment(DraggedUnBerthTime).format('YYYY-MM-DD HH:mm'));
                    item.BerthTime(moment(DraggedBerthTime).format('YYYY-MM-DD HH:mm'));
                    item.UnBerthTime(moment(DraggedUnBerthTime).format('YYYY-MM-DD HH:mm'));
                    item.MovementStatus(movementstatus);
                    modifiedvessel = item;
                }
            });

            return modifiedvessel;
        }

        // MaintPShape

        function MaintPShape(data, Previouslength) {
            var line = this;
            shapeCount++;
            this.Id = ko.observable(data.BerthMaintenanceID);
            this.aX = ko.observable(0);
            this.aY = ko.observable(0);
            var VesselLength = data.Length() - data.PositionX();
            var duration = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom())));
            var hours = duration.asHours();
            //var VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            var VesselWidth;
            var PositionY;

            //if (moment(data.PeriodFrom()).date() == moment(self.SelectedDate()).date()) {
            //    PositionY = moment(data.PeriodFrom()).hour();
            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            //}
            //else if (moment(data.PeriodFrom()).date() < moment(self.SelectedDate()).date() && moment(data.PeriodTo()).date() > moment(self.SelectedDate()).date()) {
            //    var date = self.SelectedDate();
            //    PositionY = 0;

            //    do {
            //        date = moment(date).add('days', 1).date();
            //        PositionY = PositionY + 24;

            //    } while (date < moment(data.PeriodTo()).date());

            //    PositionY = PositionY + moment(data.PeriodTo()).hour();
            //    VesselWidth = moment.duration(moment(date).diff(moment(self.SelectedDate()))).asHours();
            //}
            //else if (moment(data.PeriodTo()).date() == moment(self.SelectedDate()).date()) {

            //    PositionY = moment(data.PeriodTo()).hour();
            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(self.SelectedDate()))).asHours();
            //}
            //else {
            //    PositionY = moment(data.PeriodFrom()).hour() + 24;
            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            //}

            var SetTime = moment(self.SelectedDate()).startOf('day');
            var SelectedDate = moment(moment(self.SelectedDate()).format('YYYY-MM-DD'));
            var BerthDate = moment(moment(data.PeriodFrom()).format('YYYY-MM-DD'));
            //console.log('VCMID=BerthTime-SelectedDate', data1.VesselCallMovementID() + '--' + moment(data1.BerthTime()).format('YYYY-MM-DD') + '-----' + moment(self.SelectedDate()).format('YYYY-MM-DD'));

            if (SelectedDate.isSame(BerthDate)) {
                PositionY = moment(data.PeriodFrom()).hour();
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(data.PeriodFrom())).asHours();
            }
            else if (BerthDate.isBefore(SelectedDate)) {
                PositionY = 0;
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(SetTime))).asHours();
            }
            else {
                PositionY = moment(data.PeriodFrom()).hour() + 24;
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(data.PeriodFrom())).asHours();
            }

            this.width = ko.observable(Math.round(parseInt(VesselWidth * heightpic) / nohours));
            this.length = ko.observable(Math.round(parseInt(VesselLength * widthpic)));
            this.StartPositionX = ko.observable(Math.round(parseInt(data.PositionX() + Previouslength)) * widthpic);
            //this.StartPositionY = ko.observable(Math.round((parseInt(data.PositionY())) * heightpic) / nohours); // Commented by sandeep
            this.StartPositionY = ko.observable(Math.round(parseInt(PositionY) * heightpic) / nohours); // Added by sandeep
            this.line = new Kinetic.Line({
                points: [line.aX(), line.aY(),
                        parseInt(line.aX()) + parseInt(line.length()), line.aY(),
                        parseInt(line.aX()) + parseInt(line.length()), parseInt(line.aY()) + parseInt(line.width()),
                        line.aX(), parseInt(line.aY()) + parseInt(line.width())],
                stroke: "white",
                strokeWidth: 2,
                fill: "#CFCFCF",
                closed: true,
                draggable: false,
                id: line.Id(),
                x: line.StartPositionX(),
                y: line.StartPositionY(),
                length: line.length(),
                width: line.width()
            });

        }

        // MaintShape
        function MaintShape(data) {

            var line = this;
            shapeCount++;
            //item.ETA(moment(ETA1).format('YYYY-MM-DD HH:mm:ss'));

            this.Id = ko.observable(data.BerthMaintenanceID);
            this.aX = ko.observable(0);
            this.aY = ko.observable(0);
            var VesselLength = data.Length() - data.PositionX();
            var duration = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom())));
            var hours = duration.asHours();
            //var VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            var VesselWidth;
            var PositionY;

            //if (moment(data.PeriodFrom()).date() == moment(self.SelectedDate()).date()) {
            //    PositionY = moment(data.PeriodFrom()).hour();

            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            //}
            //else if (moment(data.PeriodFrom()).date() < moment(self.SelectedDate()).date() && moment(data.PeriodTo()).date() > moment(self.SelectedDate()).date()) {
            //    var date = self.SelectedDate();
            //    PositionY = 0;

            //    do {
            //        date = moment(date).add('days', 1).date();
            //        PositionY = PositionY + 24;

            //    } while (date < moment(data.PeriodTo()).date());

            //    PositionY = PositionY + moment(data.PeriodTo()).hour();
            //    VesselWidth = moment.duration(moment(date).diff(moment(self.SelectedDate()))).asHours();
            //}
            //else if (moment(data.PeriodTo()).date() == moment(self.SelectedDate()).date()) {

            //    PositionY = moment(data.PeriodTo()).hour();
            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(self.SelectedDate()))).asHours();
            //}

            //else {
            //    PositionY = moment(data.PeriodFrom()).hour() + 24;
            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            //}

            var SetTime = moment(self.SelectedDate()).startOf('day');
            var SelectedDate = moment(moment(self.SelectedDate()).format('YYYY-MM-DD'));
            var BerthDate = moment(moment(data.PeriodFrom()).format('YYYY-MM-DD'));
            //console.log('VCMID=BerthTime-SelectedDate', data1.VesselCallMovementID() + '--' + moment(data1.BerthTime()).format('YYYY-MM-DD') + '-----' + moment(self.SelectedDate()).format('YYYY-MM-DD'));

            if (SelectedDate.isSame(BerthDate)) {
                PositionY = moment(data.PeriodFrom()).hour();
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(data.PeriodFrom())).asHours();
            }
            else if (BerthDate.isBefore(SelectedDate)) {
                PositionY = 0;
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(SetTime))).asHours();
            }
            else {
                PositionY = moment(data.PeriodFrom()).hour() + 24;
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(data.PeriodFrom())).asHours();
            }

            this.width = ko.observable(Math.round(parseInt(VesselWidth * heightpic) / nohours));
            this.length = ko.observable(Math.round(parseInt(VesselLength * widthpic)));

            this.StartPositionX = ko.observable(Math.round(parseInt(data.PositionX())) * widthpic);
            //this.StartPositionY = ko.observable(Math.round((parseInt(data.PositionY())) * heightpic) / nohours);
            this.StartPositionY = ko.observable(Math.round((parseInt(PositionY)) * heightpic) / nohours);

            this.line = new Kinetic.Line({

                points: [line.aX(), line.aY(),
                        parseInt(line.aX()) + parseInt(line.length()), line.aY(),
                        parseInt(line.aX()) + parseInt(line.length()), parseInt(line.aY()) + parseInt(line.width()),
                        line.aX(), parseInt(line.aY()) + parseInt(line.width())],
                stroke: "white",
                strokeWidth: 2,
                fill: "#CFCFCF",
                closed: true,
                draggable: false,
                id: line.Id(),
                x: line.StartPositionX(),
                y: line.StartPositionY(),
                length: line.length(),
                width: line.width()
            });

        }

        // MainBShape
        function MaintBShape(data, prevsberthslength) {
            var line = this;
            shapeCount++;
            //item.ETA(moment(ETA1).format('YYYY-MM-DD HH:mm:ss'));

            this.Id = ko.observable(data.BerthMaintenanceID);
            this.aX = ko.observable(0);
            this.aY = ko.observable(0);
            var VesselLength = data.Length() - data.PositionX();
            var duration = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom())));
            var hours = duration.asHours();
            //var VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            var VesselWidth;
            var PositionY;

            //if (moment(data.PeriodFrom()).date() == moment(self.SelectedDate()).date()) {
            //    PositionY = moment(data.PeriodFrom()).hour();

            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            //}
            //else if (moment(data.PeriodFrom()).date() < moment(self.SelectedDate()).date() && moment(data.PeriodTo()).date() > moment(self.SelectedDate()).date()) {
            //    var date = self.SelectedDate();
            //    PositionY = 0;

            //    do {
            //        date = moment(date).add('days', 1).date();
            //        PositionY = PositionY + 24;

            //    } while (date < moment(data.PeriodTo()).date());

            //    PositionY = PositionY + moment(data.PeriodTo()).hour();
            //    VesselWidth = moment.duration(moment(date).diff(moment(self.SelectedDate()))).asHours();
            //}
            //else if (moment(data.PeriodTo()).date() == moment(self.SelectedDate()).date()) {

            //    PositionY = moment(data.PeriodTo()).hour();
            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(self.SelectedDate()))).asHours();
            //}

            //else {
            //    PositionY = moment(data.PeriodFrom()).hour() + 24;
            //    VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(data.PeriodFrom()))).asHours();
            //}

            var SetTime = moment(self.SelectedDate()).startOf('day');
            var SelectedDate = moment(moment(self.SelectedDate()).format('YYYY-MM-DD'));
            var BerthDate = moment(moment(data.PeriodFrom()).format('YYYY-MM-DD'));
            //console.log('VCMID=BerthTime-SelectedDate', data1.VesselCallMovementID() + '--' + moment(data1.BerthTime()).format('YYYY-MM-DD') + '-----' + moment(self.SelectedDate()).format('YYYY-MM-DD'));

            if (SelectedDate.isSame(BerthDate)) {
                PositionY = moment(data.PeriodFrom()).hour();
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(data.PeriodFrom())).asHours();
            }
            else if (BerthDate.isBefore(SelectedDate)) {
                PositionY = 0;
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(moment(SetTime))).asHours();
            }
            else {
                PositionY = moment(data.PeriodFrom()).hour() + 24;
                VesselWidth = moment.duration(moment(data.PeriodTo()).diff(data.PeriodFrom())).asHours();
            }

            this.width = ko.observable(Math.round(parseInt(VesselWidth * heightpic) / nohours));
            this.length = ko.observable(Math.round(parseInt(VesselLength * widthpic)));

            //this.StartPositionX = ko.observable(Math.round(parseInt(data.PositionX())) * widthpic);
            this.StartPositionX = ko.observable(Math.round(parseInt(data.PositionX() - prevsberthslength)) * widthpic);
            this.StartPositionY = ko.observable(Math.round((parseInt(PositionY)) * heightpic) / nohours);

            this.line = new Kinetic.Line({
                points: [line.aX(), line.aY(),
                       parseInt(line.aX()) + parseInt(line.length()), line.aY(),
                       parseInt(line.aX()) + parseInt(line.length()), parseInt(line.aY()) + parseInt(line.width()),
                       line.aX(), parseInt(line.aY()) + parseInt(line.width())],

                stroke: "white",
                strokeWidth: 2,
                fill: "#CFCFCF",
                closed: true,
                draggable: false,
                id: line.Id(),
                x: line.StartPositionX(),
                y: line.StartPositionY(),
                length: line.length(),
                width: line.width()
            });

        }


        // DRAW SHAPE
       
        function Shape(data1, prevsberthslength) {

            var line = this;
            shapeCount++;
            this.VCMID = ko.observable(data1.VesselCallMovementID());
            this.Id = ko.observable(data1.VCN());
            this.name = ko.observable(data1.VesselName());
            this.aX = ko.observable(0);
            this.aY = ko.observable(0);
            this.IsDraggable = ko.observable(true);
            var PositionY;
            var VesselWidth;
            var SetTime = moment(self.SelectedDate()).startOf('day');
            var SelectedDate = moment(moment(self.SelectedDate()).format('YYYY-MM-DD'));
            var BerthDate = moment(moment(data1.BerthTime()).format('YYYY-MM-DD'));
            //console.log('VCMID=BerthTime-SelectedDate', data1.VesselCallMovementID() + '--' + moment(data1.BerthTime()).format('YYYY-MM-DD') + '-----' + moment(self.SelectedDate()).format('YYYY-MM-DD'));

            if (SelectedDate.isSame(BerthDate)) {
                PositionY = moment(data1.BerthTime()).hour();
                VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(data1.BerthTime())).asHours();
            }
            else if (BerthDate.isBefore(SelectedDate)) {
                PositionY = 0;
                VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(moment(SetTime))).asHours();
            }
            else {
                PositionY = moment(data1.BerthTime()).hour() + 24;
                VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(data1.BerthTime())).asHours();
            }


            //if (moment(data1.BerthTime()) == moment(self.SelectedDate())) {
            //    PositionY = moment(data1.BerthTime()).hour();
            //    VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(data1.BerthTime())).asHours();
            //}
            //else if (moment(data1.BerthTime()) < moment(self.SelectedDate())) {
            //    PositionY = 0;
            //    VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(moment(SetTime))).asHours();
            //}
            //else {
            //    PositionY = moment(data1.BerthTime()).hour() + 24;
            //    VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(data1.BerthTime())).asHours();
            //}


            // this.width = ko.observable(Math.round(parseInt(data1.VesselWidth() * heightpic) / nohours));
            this.width = ko.observable(Math.round(parseInt(VesselWidth * heightpic) / nohours));
            this.length = ko.observable(Math.round(parseInt(data1.LOA() * widthpic)));
            this.ReasonForVisit = ko.observable(data1.ReasonforVisit());
            this.Draft = ko.observable(data1.Draft());
            this.StartPositionX = ko.observable(Math.round(parseInt(data1.PositionX() - prevsberthslength)) * widthpic);
            this.StartPositionY = ko.observable(Math.round((parseInt(PositionY)) * heightpic) / nohours);

            //if (data1.VCN() == 'VCNDB1500032') {
            //    debugger;
            //    console.log('vcn', data1);
            //    console.log('StartPositionX ', this.StartPositionX());
            //    console.log('StartPositionY ', this.StartPositionY());
            //}


            if (self.ViewType() == "Berth") {
                this.IsDraggable(false);
                if (data1.MovementStatus() == "MPEN" || data1.MovementStatus() == null)
                    this.linecolor = ko.observable("orange");
                else if (data1.MovementType() == "SGMV")
                    this.linecolor = self.Configurations().SailedColor;
                else if (data1.MovementStatus() == "BERT")
                    this.linecolor = self.Configurations().BerthedColor;
                else if (data1.MovementStatus() == "SCH")
                    this.linecolor = self.Configurations().ScheduleColor;
                else if (data1.MovementStatus() == "CONF")
                    this.linecolor = self.Configurations().ConfirmColor;
                else if (data1.MovementStatus() == "SALD")
                    this.linecolor = self.Configurations().SailedColor;
            }
            else {
                if (data1.MovementStatus() == "BERT") {
                    this.IsDraggable(false);
                }
                else if (moment(data1.BerthTime()).format('YYYY-MM-DD') < moment(self.SelectedDate()).format('YYYY-MM-DD')) {
                    this.IsDraggable(false);
                }
                else {
                    this.IsDraggable(true);
                }


                if (data1.MovementStatus() == "MPEN" || data1.MovementStatus() == null) {
                    this.linecolor = self.Configurations().ScheduleColor;
                }
                else if (data1.MovementType() == "SGMV") {
                    this.linecolor = self.Configurations().SailedColor;
                }
                else if (data1.MovementStatus() == "BERT") {
                    this.linecolor = self.Configurations().BerthedColor;
                    this.IsDraggable = ko.observable(false);
                }
                else if (data1.MovementStatus() == "SCH") {
                    this.linecolor = self.Configurations().ScheduleColor;
                }
                else if (data1.MovementStatus() == "CONF") {
                    this.linecolor = self.Configurations().ConfirmColor;
                    this.IsDraggable = ko.observable(false);
                }
                else if (data1.MovementStatus() == "SALD") {
                    this.linecolor = self.Configurations().SailedColor;
                    this.IsDraggable = ko.observable(false);
                }
            }



            this.line = new Kinetic.Line({
                points: [line.aX(), line.aY(),
                        parseInt(line.aX()) + parseInt(line.length()), line.aY(),
                        parseInt(line.aX()) + parseInt(line.length()), parseInt(line.aY()) + parseInt(line.width()) - 20,
                        parseInt(line.aX()) + parseInt(line.length()) / 2, parseInt(line.aY()) + parseInt(line.width()),
                        line.aX(), parseInt(line.aY()) + parseInt(line.width()) - 20],
                stroke: "black",
                strokeWidth: 2,
                fill: line.linecolor(),
                closed: true,
                draggable: line.IsDraggable(),
                dragBoundFunc: function (pos) {


                    var minX = TotalGridStage.getX();
                    var maxX = TotalGridStage.getX() + TotalGridStage.getWidth() - this.attrs.length;
                    var minY = TotalGridStage.getY();
                    var maxY = TotalGridStage.getY() + TotalGridStage.getHeight() - this.attrs.width;

                    var X = pos.x;
                    var Y = pos.y;
                    if (X < minX) {
                        X = minX;
                    }
                    if (X > maxX) {
                        X = maxX;
                    }

                    return ({
                        x: X,
                        y: Y
                    });
                },
                id: line.Id(),
                name: data1,
                LOA: data1.LOA(),
                PositionY: line.StartPositionY(),
                BerthTime: data1.BerthTime(),
                UnBerthTime: data1.UnBerthTime(),
                VesselWidth: data1.VesselWidth(),
                VCMID: data1.VesselCallMovementID(),
                HoursDiff: data1.Diff(),
                x: line.StartPositionX(),
                y: line.StartPositionY(),
                length: line.length(),
                width: line.width()

            });

            this.line.on("dragover"), function () {
                var selectedshape = this;
                selectedshape.moveToTop();
                var layer = selectedshape.getLayer();
                layer.draw();
            };


            this.line.on("dragend", function () {

                var selectedshape = this;
                selectedshape.moveToTop();
                var layer = selectedshape.getLayer();
                layer.draw();
                var canDoubleBank;
                var IsColliding;
                var isTimeRange;
                var isVesselSafe;
                var DraggedBerthTime;
                var DraggedUnBerthTime;
                var _SuitableBerthsList = [];
                var frombollard = GetFromBollard(this.getX() / widthpic);
                var tobollard = GetToBollard((this.getX() + this.attrs.length) / widthpic);

                var VesselsConflict = [];
                var DraggedTime = parseInt(this.getY() / heightpic);
                var SetTime = moment(self.SelectedDate()).startOf('day');
                var DraggedBerthTime = new Date((new Date(SetTime).setHours(DraggedTime)));
                var DraggedUnBerthTime = new Date((new Date(SetTime).setHours(DraggedTime + selectedshape.attrs.HoursDiff)));
                var VesselData;
                var _VCMID = selectedshape.attrs.VCMID;
                var _VCN = selectedshape.attrs.id;
                var IsVesselSuitedAtBerths = true;
                var BerthsStatusList = [];
                var ConflictingVesselsfromServer;
                var _IsBerthSuitablefromServerSide;

                var ConflictingVesselsfromClient = [];
                // Getting the vessel data from plannedvessesls
                VesselData = $.grep(self.plannedvessesls(), function (vessel) {
                    return (vessel.VesselCallMovementID() === _VCMID);
                });

                var revertpositon = GetTimePosition(VesselData[0].BerthTime(), VesselData[0].UnBerthTime());
                if (moment(DraggedBerthTime).format('YYYY-MM-DD HH:mm') > moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm')) {

                    // Getting Beths between Bollards
                    getBerths = getBerthsbetweenBollards(frombollard, tobollard);

                    // Getting the Suitable Berths of the Quay Selected
                    $.each(VesselData[0].SuitableBerthsList(), function (index, suitableberth) {
                        if (suitableberth.QuayCode == self.selectedQuayCode())
                            _SuitableBerthsList.push(suitableberth.BerthCode);
                    });

                    // Check if the Berths are in Suitable Berths List 
                    $.each(getBerths.berthsList, function (index, item) {
                        var isSuited = $.inArray(item, _SuitableBerthsList);
                        if (isSuited == -1) {
                            IsVesselSuitedAtBerths = false;
                            BerthsStatusList.push(item);
                        }
                    });

                    if (IsVesselSuitedAtBerths == true && getBerths.AreContinousBollards == true) {

                        //for (i = 0; i < self.plannedvessesls().length; i++)
                        //{
                        //    canDoubleBank = false;
                        //    IsColliding = false;
                        //    isVesselSafe = false;
                        //    var plannedvessel = self.plannedvessesls()[i];
                        //    if (selectedshape.attrs.id != plannedvessel.VCN() && plannedvessel.FromQuayCode() == frombollard.QuayCode()) {

                        //        IsColliding = AreCollidingBollards(plannedvessel, selectedshape, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
                        //        isTimeRange = isInTimeRange(plannedvessel, selectedshape, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);


                        //        isVesselSafe = IsVesselSafe(plannedvessel, selectedshape, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
                        //        var VesselConflict = { VCN: plannedvessel.VCN(), IsColliding: IsColliding, isVesselSafe: isVesselSafe, isTimeRange: isTimeRange }
                        //        VesselsConflict.push(VesselConflict);                                                    
                        //    }
                        //}
                        // Conflicts from Server Side 
                        _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(_VCN, _VCMID, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
                        //console.log('_IsBerthSuitablefromServerSide', _IsBerthSuitablefromServerSide);
                        ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(_VCMID, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
                        //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);

                        if (ConflictingVesselsfromClient.length == 0 && _IsBerthSuitablefromServerSide == true) {

                            var x1 = parseInt(this.getX() / widthpic);
                            var y1 = parseInt(this.getY() / heightpic);
                            var isFromToBerthSame = true;
                            var isContinous;
                            var isShifting;
                            var IsValid = false;
                            var fromberth = GetBerth(frombollard.BollardCode(), frombollard.BerthCode(), frombollard.QuayCode());
                            var toberth = GetBerth(tobollard.BollardCode(), tobollard.BerthCode(), tobollard.QuayCode());
                            var userdata = self.LoggedUserData();
                            var vcn = this.id();
                            //var vmstatus = "SCH";
                            //if (this.name().MovementStatus() == "BERT") {
                            //    vmstatus = "SCH";
                            //    //this.attrs.fill = "#FFBF00";
                            //    this.attrs.fill = self.Configurations().ScheduleColor;

                            //    this.draggable(true);
                            //}

                            var vmstatus = this.name().MovementStatus();
                            var vesselid;


                            $.each(self.plannedvessesls(), function (index, item) {
                                if (item.DoubleBankedVessel() == selectedshape.attrs.VCMID) {
                                    vesselid = item.VesselCallMovementID();
                                    var banked = item.IsBanked() - 1;
                                    item.IsBanked(banked);
                                    item.DoubleBankedVessel(null);
                                }
                            });

                            $.each(self.vesselShapes(), function (index, value) {
                                //console.log(item);
                                if (value.VCMID() == vesselid) {
                                    var line = value.line;
                                    if (line.name().MovementStatus() == 'SCH') {
                                        line.attrs.stroke = 'black';
                                        line.attrs.opacity = .1,
                                        line.draggable(true);
                                        var layer = line.getLayer();
                                        GridLayer.draw();
                                    }
                                    else {
                                        line.attrs.stroke = 'black';
                                        line.attrs.opacity = .1;
                                        var layer = line.getLayer();
                                        GridLayer.draw();
                                    }
                                }
                            });

                            //var vessel = updatePlannedItem(selectedshape.attrs.VCMID, vcn, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName(), toberth.BerthName());
                            var vessel = updatePlannedItem(selectedshape.attrs.VCMID, vcn, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName(), toberth.BerthName(), frombollard.BollardName(), tobollard.BollardName());
                            this.setPosition({ x: parseInt(frombollard.FromMeter() * widthpic), y: parseInt(y1 * heightpic) });
                            this.name(vessel);
                            var layer = this.getLayer();
                            layer.draw();


                        }
                        else {
                            var NotSafe = false;
                            var IsColliding = false;
                            var DoubleBankingVessels = [];
                            var DoubleBankSuitableVessels = [];
                            var NotSafeVessels = [];
                            var CollidingVessels = [];
                            $.each(ConflictingVesselsfromClient, function (index, item) {
                                //  console.log(item);
                                if (item.DoubleBank == false) {
                                    if (item.VesselNotSafe == true || item.VesselCollide == true) {
                                        //NotSafeVessels.push(item.VCN()); // commented by sandeep on 27-05-2015

                                        NotSafeVessels.push({ VCMID: item.VCMID(), VCN: item.VCN() }); // Added by sandeep on 27-05-2015
                                        NotSafe = true;
                                    }

                                }
                                else {
                                    DoubleBankingVessels.push(item.VCMID());
                                }

                            });


                            if (NotSafe == true) {

                                var Msg = "";
                                var count = NotSafeVessels.length - 1;
                                var vessel = NotSafeVessels[count].VCN;
                                if (NotSafe == true || IsColliding == true)
                                    Msg = Msg + "<li>Safe Distance of  is not maintained with " + vessel + ".</li>";

                                bootbox.alert(Msg);
                                selectedshape.setPosition({ x: Math.round(parseInt(selectedshape.name().PositionX() + prevsberthslength)) * widthpic, y: this.attrs.PositionY });
                                var layer = selectedshape.getLayer();
                                layer.draw();

                            }
                            else if (DoubleBankingVessels.length > 0) {


                                $.each(self.plannedvessesls(), function (index, item) {
                                    var contains = $.inArray(item.VesselCallMovementID(), DoubleBankingVessels);
                                    if (contains != -1) {

                                        //if (selectedshape.attrs.LOA < item.LOA() && item.MovementStatus() == "BERT") {
                                        DoubleBankSuitableVessels.push(item);
                                        //}
                                    }
                                });


                                var BankingVessel;
                                if (DoubleBankSuitableVessels.length == 1) {
                                    BankingVessel = DoubleBankSuitableVessels[0];
                                    if (BankingVessel.IsBanked() == 0) {
                                        canDoubleBank = true;
                                    }
                                    else {
                                        canDoubleBank = false;
                                    }
                                }

                                else if (DoubleBankSuitableVessels.length == 2) {
                                    canDoubleBank = true;
                                    if (DoubleBankSuitableVessels[0].LOA() < DoubleBankSuitableVessels[1]) {
                                        BankingVessel = DoubleBankSuitableVessels[0];
                                    }
                                    else {
                                        BankingVessel = DoubleBankSuitableVessels[1];
                                    }
                                }

                                if (canDoubleBank == true) {

                                    bootbox.confirm("Do you want to Bank " + selectedshape.attrs.id + " on " + BankingVessel.VCN(), function (result) {
                                        if (result) {

                                            ForDoubleBanking = true;
                                            var x1 = parseInt(selectedshape.getX() / widthpic);
                                            var y1 = parseInt(selectedshape.getY() / heightpic);

                                            var vesselid;

                                            $.each(self.plannedvessesls(), function (index, item) {
                                                if (item.DoubleBankedVessel() == selectedshape.attrs.VCMID) {
                                                    vesselid = item.VesselCallMovementID();
                                                    var banked = item.IsBanked() - 1;
                                                    item.IsBanked(banked);
                                                    item.DoubleBankedVessel(null);
                                                }
                                            });

                                            $.each(self.vesselShapes(), function (index, value) {
                                                //console.log(item);
                                                if (value.VCMID() == vesselid) {
                                                    var line = value.line;
                                                    if (line.name().MovementStatus() == 'SCH') {
                                                        line.attrs.stroke = 'black';
                                                        line.attrs.opacity = .1,
                                                        line.draggable(true);
                                                        var layer = line.getLayer();
                                                        GridLayer.draw();
                                                    }
                                                    else {
                                                        line.attrs.stroke = 'black';
                                                        line.attrs.opacity = .1;
                                                        var layer = line.getLayer();
                                                        GridLayer.draw();
                                                    }
                                                }
                                            });

                                            for (i = 0; i < self.plannedvessesls().length; i++) {
                                                if (self.plannedvessesls()[i].VCN() == BankingVessel.VCN() && self.plannedvessesls()[i].VesselCallMovementID() == BankingVessel.VesselCallMovementID()) {
                                                    //self.plannedvessesls()[i].DoubleBankedVessel(selectedshape.attrs.id); // Commented by sandeep on 27-05-2015
                                                    self.plannedvessesls()[i].DoubleBankedVessel(selectedshape.attrs.VCMID); // Added by sandeep on 27-05-2015
                                                    var Bank = self.plannedvessesls()[i].IsBanked() + 1;
                                                    self.plannedvessesls()[i].IsBanked(Bank);
                                                    $.each(self.vesselShapes(), function (index, item) {
                                                        //console.log(item);
                                                        if (item.Id() == self.plannedvessesls()[i].VCN() && item.VCMID() == self.plannedvessesls()[i].VesselCallMovementID()) {
                                                            var line = item.line;
                                                            line.attrs.stroke = 'red';
                                                            line.attrs.opacity = .1,
                                                            line.draggable(false);
                                                            var layer = line.getLayer();
                                                            GridLayer.draw();
                                                        }
                                                    });


                                                    //var vmstatus = "SCH";
                                                    //selectedshape.attrs.fill = self.Configurations().ScheduleColor();

                                                    //selectedshape.draggable(true);
                                                    var vmstatus = selectedshape.attrs.name.MovementStatus()

                                                    // var fromBollard = self.plannedvessesls()[i].FromBollardMeter();
                                                    //var startposition = GetFromBollardPosition(self.plannedvessesls()[i].FromBollardCode());

                                                    var startposition = self.plannedvessesls()[i].FromBollardMeter();
                                                    var yposition = GetTimePosition(DraggedBerthTime, DraggedUnBerthTime);

                                                    //var fromberth = GetBerth(self.plannedvessesls()[i].FromBollardCode());
                                                    //var toberth = GetBerth(self.plannedvessesls()[i].ToBollardCode());

                                                    var fromberth = GetBerth(self.plannedvessesls()[i].FromBollardCode(), self.plannedvessesls()[i].FromBerthCode(), self.plannedvessesls()[i].FromQuayCode());
                                                    var toberth = GetBerth(self.plannedvessesls()[i].ToBollardCode(), self.plannedvessesls()[i].ToBerthCode(), self.plannedvessesls()[i].ToQuayCode());

                                                    var y1 = parseInt(selectedshape.getY() / heightpic);
                                                    //var vessel = updatePlannedItem(selectedshape.attrs.id, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName());
                                                    var vessel = updatePlannedItem(selectedshape.attrs.VCMID, selectedshape.attrs.id, vmstatus, self.plannedvessesls()[i].FromPortCode(), self.plannedvessesls()[i].FromQuayCode(), self.plannedvessesls()[i].FromBerthCode(), self.plannedvessesls()[i].ToBerthCode(), self.plannedvessesls()[i].FromBollardCode(), self.plannedvessesls()[i].ToBollardCode(), self.plannedvessesls()[i].FromBollardMeter(), self.plannedvessesls()[i].ToBollardMeter(), y1, fromberth.BerthName(), toberth.BerthName(), self.plannedvessesls()[i].FromBollardName(), self.plannedvessesls()[i].ToBollardName());
                                                    selectedshape.setPosition({ x: parseInt(startposition * widthpic), y: parseInt(yposition * heightpic) });
                                                    selectedshape.name(vessel);
                                                    var layer = selectedshape.getLayer();
                                                    GridLayer.draw();

                                                }
                                            }
                                        }
                                        else {
                                            selectedshape.setPosition({ x: Math.round(parseInt(selectedshape.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round(parseInt(revertpositon * heightpic)) });
                                            var layer = selectedshape.getLayer();
                                            layer.draw();
                                        }

                                    });

                                }
                                else {
                                    bootbox.alert('Vessel is not yet berthed. Hence double bank is not possible on this vessel');
                                    selectedshape.setPosition({ x: Math.round(parseInt(selectedshape.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round(parseInt(revertpositon * heightpic)) });
                                    var layer = selectedshape.getLayer();
                                    layer.draw();

                                }

                            }
                            else {

                                var x1 = parseInt(this.getX() / widthpic);
                                var y1 = parseInt(this.getY() / heightpic);
                                var isFromToBerthSame = true;
                                var isContinous;
                                var isShifting;
                                var IsValid = false;
                                var fromberth = GetBerth(frombollard.BollardCode(), frombollard.BerthCode(), frombollard.QuayCode());
                                var toberth = GetBerth(tobollard.BollardCode(), tobollard.BerthCode(), tobollard.QuayCode());
                                var userdata = self.LoggedUserData();
                                var vcn = this.id();
                                //var vmstatus = "SCH";
                                //if (this.name().MovementStatus() == "BERT") {
                                //    vmstatus = "SCH";
                                //    //this.attrs.fill = "#FFBF00";
                                //    this.attrs.fill = self.Configurations().ScheduleColor;

                                //    this.draggable(true);
                                //}
                                var vmstatus = this.name().MovementStatus();
                                var vesselid;
                                $.each(self.plannedvessesls(), function (index, item) {
                                    if (item.DoubleBankedVessel() == selectedshape.attrs.VCMID) {
                                        vesselid = item.VesselCallMovementID();
                                        var banked = item.IsBanked() - 1;
                                        item.IsBanked(banked);
                                        item.DoubleBankedVessel(null);
                                    }
                                });
                                $.each(self.vesselShapes(), function (index, value) {
                                    //console.log(item);
                                    if (value.VCMID() == vesselid) {
                                        var line = value.line;
                                        if (line.name().MovementStatus() == 'SCH') {
                                            line.attrs.stroke = 'black';
                                            line.attrs.opacity = .1,
                                            line.draggable(true);
                                            var layer = line.getLayer();
                                            GridLayer.draw();
                                        }
                                        else {
                                            var line = value.line;
                                            line.attrs.stroke = 'black';
                                            line.attrs.opacity = .1;
                                            var layer = line.getLayer();
                                            GridLayer.draw();
                                        }
                                    }
                                });
                                var vessel = updatePlannedItem(selectedshape.attrs.VCMID, vcn, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName(), toberth.BerthName(), frombollard.BollardName(), tobollard.BollardName());
                                this.setPosition({ x: parseInt(frombollard.FromMeter() * widthpic), y: parseInt(y1 * heightpic) });
                                this.name(vessel);
                                var layer = this.getLayer();
                                layer.draw();
                            }

                        }


                    }

                    else {
                        Msg = "";
                        if (getBerths.AreContinousBollards == false)
                            Msg = Msg + "<li>Bollards are discontinous.</li>";

                        $.each(self.berthsWithBollards(), function (index, item) {
                            var notSuited = $.inArray(item.BerthCode(), BerthsStatusList);
                            if (notSuited != -1) {
                                var failed = getfailedBerthingRules(VesselData[0], item);
                                Msg = getRulesMsg(item.BerthName(), failed);
                            }

                        });
                        bootbox.alert(Msg);
                        this.setPosition({ x: Math.round(parseInt(this.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round(parseInt(revertpositon * heightpic)) });
                        var layer = this.getLayer();
                        layer.draw()
                    }


                }
                else {
                    bootbox.alert('Vessel ETB must be greater than current time.');

                    this.setPosition({ x: Math.round(parseInt(this.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round(parseInt(revertpositon * heightpic)) });

                    var layer = this.getLayer();
                    layer.draw();

                }
            });

            // End 
            //srinivas
            //context menu on vessel
            this.line.on("click", function (event) {

                if (self.RolePrivileges() == "true") {
                    if (self.ViewType() != "Berth") {
                        var line = this;
                        var vcn = this.id();
                        if (line.attrs.name.IsBanked() == 0) {
                            //var movementStatus = GetMovementStatus(vcn); // Commenetd by sandeep on 27-04-2015
                            var movementStatus = data1.MovementStatus(); // Added by sandeep on 27-04-2015
                            var recordStatus = data1.RecordStatus();
                            if (movementStatus == "SCH") {
                                var _bollardcode;
                                $("[id$='contextMenu']").css({
                                    display: 'inline',
                                    position: 'absolute',
                                    top: event.layerY,
                                    left: event.layerX,
                                    opacity: .8
                                });
                                $("[id$='contextMenuH']").html('');
                                $('<span />').html('Actions').appendTo($("[id$='contextMenuH']"));
                                $("[id$='contextMenuB']").html(''); //clear                                
                                var div = $('<div />'),
                                     btn = $('<input />', {
                                         type: 'button',
                                         value: 'Unschedule',
                                         id: line,
                                         on: {
                                             click: function () {

                                                 var layer = line.getLayer();
                                                 line.remove();
                                                 layer.draw();
                                                 UnScheduleVessel(line.attrs.VCMID);
                                                 $("[id$='contextMenu']").css({
                                                     display: 'none'
                                                 });
                                             }
                                         }
                                     });

                                var div2 = $('<br/><div />'),
                                 btn2 = $('<input />', {
                                     type: 'button',
                                     value: 'Confirm',
                                     id: line,
                                     on: {
                                         click: function () {
                                             line.attrs.fill = self.Configurations().ConfirmColor();
                                             line.draggable(false);
                                             var layer = line.getLayer();
                                             layer.draw();
                                             ConfirmVessel(line.attrs.VCMID);
                                             $("[id$='contextMenu']").css({
                                                 display: 'none'
                                             });
                                         }
                                     }
                                 });

                                var div3 = $('<br/><div style="padding:2px 0 2px 0;"/>'),
                               btn3 = $('<select />', {
                                   width: '120px',
                                   //height: '20px',
                                   on: {
                                       change: function (e) {
                                           //alert($(this).val());
                                           _bollardcode = $(this).val();
                                       }
                                   }
                               });

                                var option = $('<option/>');
                                option.attr({ 'value': '' }).text('Select Bollard');
                                btn3.append(option);
                                btn3.attr({ 'size': 5 });

                                $.each(self.berthsWithBollards(), function (index, value) {
                                    if (value.QuayCode() == data1.FromQuayCode() && value.BerthCode() == data1.FromBerthCode()) {
                                        $.each(value.Bollards(), function (index, item) {
                                            //var option = $('<option/>');
                                            //option.attr({ 'value': item.BollardCode() }).text(item.BollardName());
                                            //btn2.append(option);
                                            btn3.append('<option value="' + item.BollardCode() + '">' + item.BollardName() + '</option>');
                                        });
                                    }
                                });

                                var div4 = $('<div />'),
                                    btn4 = $('<input />', {
                                        type: 'button',
                                        value: 'ChangeBollard',
                                        id: line,
                                        on: {
                                            click: function () {

                                                if (_bollardcode != undefined) {
                                                    var frombollard;
                                                    $.each(self.berthsWithBollards(), function (index, value) {
                                                        if (value.QuayCode() == data1.FromQuayCode() && value.BerthCode() == data1.FromBerthCode()) {
                                                            $.each(value.Bollards(), function (index, item) {
                                                                if (item.BollardCode() == _bollardcode) {
                                                                    frombollard = item;
                                                                }
                                                            });
                                                        }
                                                    });

                                                    var PositionY;
                                                    var SetTime = moment(self.SelectedDate()).startOf('day');
                                                    var SelectedDate = moment(moment(self.SelectedDate()).format('YYYY-MM-DD'));
                                                    var BerthDate = moment(moment(data1.BerthTime()).format('YYYY-MM-DD'));

                                                    if (SelectedDate.isSame(BerthDate)) {
                                                        PositionY = moment(data1.BerthTime()).hour();
                                                    }
                                                    else {
                                                        PositionY = moment(data1.BerthTime()).hour() + 24;
                                                    }

                                                    var selectedshape = this;
                                                    //selectedshape.moveToTop();
                                                    //var layer = selectedshape.getLayer();
                                                    //layer.draw();
                                                    var canDoubleBank;
                                                    var IsColliding;
                                                    var isTimeRange;
                                                    var isVesselSafe;
                                                    var DraggedBerthTime;
                                                    var DraggedUnBerthTime;
                                                    var _SuitableBerthsList = [];
                                                    //var frombollard = GetFromBollard(data1.FromBollardMeter());
                                                    var tobollard = GetToBollard((frombollard.FromMeter() + data1.VesselLength()));

                                                    var VesselsConflict = [];
                                                    var DraggedBerthTime = new Date(data1.BerthTime());
                                                    var DraggedUnBerthTime = new Date(data1.UnBerthTime());
                                                    var VesselData;
                                                    var _VCMID = data1.VesselCallMovementID();
                                                    var _VCN = data1.VCN();
                                                    var IsVesselSuitedAtBerths = true;
                                                    var BerthsStatusList = [];
                                                    var ConflictingVesselsfromServer;
                                                    var _IsBerthSuitablefromServerSide;

                                                    var ConflictingVesselsfromClient = [];
                                                    // Getting the vessel data from plannedvessesls
                                                    VesselData = $.grep(self.plannedvessesls(), function (vessel) {
                                                        return (vessel.VesselCallMovementID() === _VCMID);
                                                    });

                                                    var revertpositon = GetTimePosition(VesselData[0].BerthTime(), VesselData[0].UnBerthTime());
                                                    if (moment(DraggedBerthTime).format('YYYY-MM-DD HH:mm') > moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm')) {

                                                        // Getting Beths between Bollards
                                                        getBerths = getBerthsbetweenBollards(frombollard, tobollard);

                                                        // Getting the Suitable Berths of the Quay Selected
                                                        $.each(VesselData[0].SuitableBerthsList(), function (index, suitableberth) {
                                                            if (suitableberth.QuayCode == self.selectedQuayCode())
                                                                _SuitableBerthsList.push(suitableberth.BerthCode);
                                                        });

                                                        // Check if the Berths are in Suitable Berths List 
                                                        $.each(getBerths.berthsList, function (index, item) {
                                                            var isSuited = $.inArray(item, _SuitableBerthsList);
                                                            if (isSuited == -1) {
                                                                IsVesselSuitedAtBerths = false;
                                                                BerthsStatusList.push(item);
                                                            }
                                                        });

                                                        if (IsVesselSuitedAtBerths == true && getBerths.AreContinousBollards == true) {

                                                            // Conflicts from Server Side 
                                                            _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(_VCN, _VCMID, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
                                                            //console.log('_IsBerthSuitablefromServerSide', _IsBerthSuitablefromServerSide);
                                                            ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(_VCMID, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
                                                            //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);

                                                            if (ConflictingVesselsfromClient.length == 0 && _IsBerthSuitablefromServerSide == true) {

                                                                var x1 = parseInt(frombollard.FromMeter());
                                                                var y1 = parseInt(PositionY);
                                                                var isFromToBerthSame = true;
                                                                var isContinous;
                                                                var isShifting;
                                                                var IsValid = false;
                                                                var fromberth = GetBerth(frombollard.BollardCode(), frombollard.BerthCode(), frombollard.QuayCode());
                                                                var toberth = GetBerth(tobollard.BollardCode(), tobollard.BerthCode(), tobollard.QuayCode());
                                                                var userdata = self.LoggedUserData();
                                                                var vcn = line.id();
                                                                var vmstatus = data1.MovementStatus();
                                                                
                                                                var vessel = updatePlannedItem(data1.VesselCallMovementID(), vcn, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName(), toberth.BerthName(), frombollard.BollardName(), tobollard.BollardName());
                                                                line.setPosition({ x: parseInt(frombollard.FromMeter() * widthpic), y: parseInt(y1 * heightpic) });
                                                                line.name(vessel);
                                                                var layer = line.getLayer();
                                                                layer.draw();
                                                            }
                                                            else {
                                                                var NotSafe = false;
                                                                var IsColliding = false;
                                                                var DoubleBankingVessels = [];
                                                                var DoubleBankSuitableVessels = [];
                                                                var NotSafeVessels = [];
                                                                var CollidingVessels = [];
                                                                $.each(ConflictingVesselsfromClient, function (index, item) {
                                                                    //  console.log(item);
                                                                    if (item.DoubleBank == false) {
                                                                        if (item.VesselNotSafe == true || item.VesselCollide == true) {
                                                                            NotSafeVessels.push(item.VCN());
                                                                            NotSafe = true;
                                                                        }
                                                                    }
                                                                    else {
                                                                        DoubleBankingVessels.push(item.VCN());
                                                                    }

                                                                });

                                                                if (NotSafe == true) {

                                                                    var Msg = "";
                                                                    if (NotSafe == true || IsColliding == true)
                                                                        Msg = Msg + "<li>Safe Distance of  is not maintained with " + NotSafeVessels.join(',') + ".</li>";

                                                                    bootbox.alert(Msg);
                                                                    //line.setPosition({ x: Math.round(parseInt(line.name().PositionX() + prevsberthslength)) * widthpic, y: line.PositionY });
                                                                    //var layer = line.getLayer();
                                                                    //layer.draw();

                                                                }
                                                                else if (DoubleBankingVessels.length > 0) {
                                                                    bootbox.alert("<li>Berth is occupied.</li>");
                                                                }
                                                                else {
                                                                    var x1 = parseInt(frombollard.FromMeter());
                                                                    var y1 = parseInt(PositionY);
                                                                    var isFromToBerthSame = true;
                                                                    var isContinous;
                                                                    var isShifting;
                                                                    var IsValid = false;
                                                                    var fromberth = GetBerth(frombollard.BollardCode(), frombollard.BerthCode(), frombollard.QuayCode());
                                                                    var toberth = GetBerth(tobollard.BollardCode(), tobollard.BerthCode(), tobollard.QuayCode());
                                                                    var userdata = self.LoggedUserData();
                                                                    var vcn = line.id();
                                                                    var vmstatus = data1.MovementStatus();

                                                                    var vessel = updatePlannedItem(data1.VesselCallMovementID(), vcn, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName(), toberth.BerthName(), frombollard.BollardName(), tobollard.BollardName());
                                                                    line.setPosition({ x: parseInt(frombollard.FromMeter() * widthpic), y: parseInt(y1 * heightpic) });
                                                                    line.name(vessel);
                                                                    var layer = line.getLayer();
                                                                    layer.draw();
                                                                }
                                                            }
                                                        }

                                                        else {
                                                            Msg = "";
                                                            if (getBerths.AreContinousBollards == false)
                                                                Msg = Msg + "<li>Bollards are discontinous.</li>";

                                                            $.each(self.berthsWithBollards(), function (index, item) {
                                                                var notSuited = $.inArray(item.BerthCode(), BerthsStatusList);
                                                                if (notSuited != -1) {
                                                                    var failed = getfailedBerthingRules(VesselData[0], item);
                                                                    Msg = getRulesMsg(item.BerthName(), failed);
                                                                }

                                                            });
                                                            bootbox.alert(Msg);
                                                            //line.setPosition({ x: Math.round(parseInt(line.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round(parseInt(revertpositon * heightpic)) });
                                                            //var layer = line.getLayer();
                                                            //layer.draw()
                                                        }
                                                    }
                                                    else {
                                                        bootbox.alert('Vessel ETB must be greater than current time.');

                                                        //line.setPosition({ x: Math.round(parseInt(line.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round(parseInt(revertpositon * heightpic)) });

                                                        //var layer = line.getLayer();
                                                        //layer.draw()
                                                    }
                                                    $("[id$='contextMenu']").css({
                                                        display: 'none'
                                                    });
                                                }
                                            }
                                        }
                                    });


                                div.append(btn).appendTo($("[id$='contextMenuB']"));
                                div2.append(btn2).appendTo($("[id$='contextMenuB']"));
                                div3.append(btn3).appendTo($("[id$='contextMenuB']"));
                                div4.append(btn4).appendTo($("[id$='contextMenuB']"));

                            }
                                //srinivas
                           
                            else if (movementStatus == "CONF" && recordStatus!="A") {
                                var _bollardcode;

                                $("[id$='contextMenu']").css({
                                    display: 'inline',
                                    position: 'absolute',
                                    top: event.layerY,
                                    left: event.layerX,
                                    opacity: .8
                                });
                                $("[id$='contextMenuH']").html('');
                                $('<span />').html('Actions').appendTo($("[id$='contextMenuH']"));
                                $("[id$='contextMenuB']").html(''); //clear                                
                                var div = $('<div />'),
                                    btn = $('<input />', {
                                        type: 'button',
                                        value: 'Unschedule',
                                        id: line,
                                        on: {
                                            click: function () {
                                                var layer = line.getLayer();
                                                line.remove();
                                                layer.draw();
                                                //console.log('line', line);
                                                UnScheduleVessel(line.attrs.VCMID);
                                                $("[id$='contextMenu']").css({
                                                    display: 'none'
                                                });

                                            }
                                        }
                                    });

                                var div2 = $('<br/><div style="padding:2px 0 2px 0;" />'),
                               btn2 = $('<select />', {
                                   width: '120px',
                                   on: {
                                       change: function (e) {
                                           //alert($(this).val());
                                           _bollardcode = $(this).val();
                                       }
                                   }
                               });

                                var option = $('<option/>');
                                option.attr({ 'value': '' }).text('Select Bollard');
                                btn2.append(option);
                                btn2.attr({ 'size': 5 });

                                $.each(self.berthsWithBollards(), function (index, value) {
                                    if (value.QuayCode() == data1.FromQuayCode() && value.BerthCode() == data1.FromBerthCode()) {
                                        $.each(value.Bollards(), function (index, item) {
                                            //var option = $('<option/>');
                                            //option.attr({ 'value': item.BollardCode() }).text(item.BollardName());
                                            //btn2.append(option);
                                            btn2.append('<option value="' + item.BollardCode() + '">' + item.BollardName() + '</option>');
                                        });
                                    }
                                });

                                var div3 = $('<div />'),
                                    btn3 = $('<input />', {
                                        type: 'button',
                                        value: 'ChangeBollard',
                                        id: line,
                                        on: {
                                            click: function () {
                                                if (_bollardcode != undefined) {
                                                    var frombollard;
                                                    $.each(self.berthsWithBollards(), function (index, value) {
                                                        if (value.QuayCode() == data1.FromQuayCode() && value.BerthCode() == data1.FromBerthCode()) {
                                                            $.each(value.Bollards(), function (index, item) {
                                                                if (item.BollardCode() == _bollardcode) {
                                                                    frombollard = item;
                                                                }
                                                            });
                                                        }
                                                    });

                                                    var PositionY;
                                                    var SetTime = moment(self.SelectedDate()).startOf('day');
                                                    var SelectedDate = moment(moment(self.SelectedDate()).format('YYYY-MM-DD'));
                                                    var BerthDate = moment(moment(data1.BerthTime()).format('YYYY-MM-DD'));

                                                    if (SelectedDate.isSame(BerthDate)) {
                                                        PositionY = moment(data1.BerthTime()).hour();
                                                    }
                                                    else {
                                                        PositionY = moment(data1.BerthTime()).hour() + 24;
                                                    }

                                                    var selectedshape = this;
                                                    //selectedshape.moveToTop();
                                                    //var layer = selectedshape.getLayer();
                                                    //layer.draw();
                                                    var canDoubleBank;
                                                    var IsColliding;
                                                    var isTimeRange;
                                                    var isVesselSafe;
                                                    var DraggedBerthTime;
                                                    var DraggedUnBerthTime;
                                                    var _SuitableBerthsList = [];
                                                    //var frombollard = GetFromBollard(data1.FromBollardMeter());
                                                    var tobollard = GetToBollard((frombollard.FromMeter() + data1.VesselLength()));

                                                    var VesselsConflict = [];
                                                    var DraggedBerthTime = new Date(data1.BerthTime());
                                                    var DraggedUnBerthTime = new Date(data1.UnBerthTime());
                                                    var VesselData;
                                                    var _VCMID = data1.VesselCallMovementID();
                                                    var _VCN = data1.VCN();
                                                    var IsVesselSuitedAtBerths = true;
                                                    var BerthsStatusList = [];
                                                    var ConflictingVesselsfromServer;
                                                    var _IsBerthSuitablefromServerSide;

                                                    var ConflictingVesselsfromClient = [];
                                                    // Getting the vessel data from plannedvessesls
                                                    VesselData = $.grep(self.plannedvessesls(), function (vessel) {
                                                        return (vessel.VesselCallMovementID() === _VCMID);
                                                    });

                                                    var revertpositon = GetTimePosition(VesselData[0].BerthTime(), VesselData[0].UnBerthTime());
                                                    if (moment(DraggedBerthTime).format('YYYY-MM-DD HH:mm') > moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm')) {

                                                        // Getting Beths between Bollards
                                                        getBerths = getBerthsbetweenBollards(frombollard, tobollard);

                                                        // Getting the Suitable Berths of the Quay Selected
                                                        $.each(VesselData[0].SuitableBerthsList(), function (index, suitableberth) {
                                                            if (suitableberth.QuayCode == self.selectedQuayCode())
                                                                _SuitableBerthsList.push(suitableberth.BerthCode);
                                                        });

                                                        // Check if the Berths are in Suitable Berths List 
                                                        $.each(getBerths.berthsList, function (index, item) {
                                                            var isSuited = $.inArray(item, _SuitableBerthsList);
                                                            if (isSuited == -1) {
                                                                IsVesselSuitedAtBerths = false;
                                                                BerthsStatusList.push(item);
                                                            }
                                                        });

                                                        if (IsVesselSuitedAtBerths == true && getBerths.AreContinousBollards == true) {

                                                            // Conflicts from Server Side 
                                                            _IsBerthSuitablefromServerSide = GetConflictingVesselsfromServer(_VCN, _VCMID, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
                                                            //console.log('_IsBerthSuitablefromServerSide', _IsBerthSuitablefromServerSide);
                                                            ConflictingVesselsfromClient = GetConflictingVesselsinPlannedVessels(_VCMID, frombollard, tobollard, DraggedBerthTime, DraggedUnBerthTime);
                                                            //console.log('ConflictingVesselsfromClient', ConflictingVesselsfromClient);

                                                            if (ConflictingVesselsfromClient.length == 0 && _IsBerthSuitablefromServerSide == true) {

                                                                var x1 = parseInt(frombollard.FromMeter());
                                                                var y1 = parseInt(PositionY);
                                                                var isFromToBerthSame = true;
                                                                var isContinous;
                                                                var isShifting;
                                                                var IsValid = false;
                                                                var fromberth = GetBerth(frombollard.BollardCode(), frombollard.BerthCode(), frombollard.QuayCode());
                                                                var toberth = GetBerth(tobollard.BollardCode(), tobollard.BerthCode(), tobollard.QuayCode());
                                                                var userdata = self.LoggedUserData();
                                                                var vcn = line.id();
                                                                var vmstatus = data1.MovementStatus();

                                                                var vessel = updatePlannedItem(data1.VesselCallMovementID(), vcn, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName(), toberth.BerthName(), frombollard.BollardName(), tobollard.BollardName());
                                                                line.setPosition({ x: parseInt(frombollard.FromMeter() * widthpic), y: parseInt(y1 * heightpic) });
                                                                line.name(vessel);
                                                                var layer = line.getLayer();
                                                                layer.draw();
                                                            }
                                                            else {
                                                                var NotSafe = false;
                                                                var IsColliding = false;
                                                                var DoubleBankingVessels = [];
                                                                var DoubleBankSuitableVessels = [];
                                                                var NotSafeVessels = [];
                                                                var CollidingVessels = [];
                                                                $.each(ConflictingVesselsfromClient, function (index, item) {
                                                                    //  console.log(item);
                                                                    if (item.DoubleBank == false) {
                                                                        if (item.VesselNotSafe == true || item.VesselCollide == true) {
                                                                            NotSafeVessels.push(item.VCN());
                                                                            NotSafe = true;
                                                                        }
                                                                    }
                                                                    else {
                                                                        DoubleBankingVessels.push(item.VCN());
                                                                    }
                                                                });

                                                                if (NotSafe == true) {

                                                                    var Msg = "";
                                                                    if (NotSafe == true || IsColliding == true)
                                                                        Msg = Msg + "<li>Safe Distance of  is not maintained with " + NotSafeVessels.join(',') + ".</li>";

                                                                    bootbox.alert(Msg);
                                                                    //line.setPosition({ x: Math.round(parseInt(line.name().PositionX() + prevsberthslength)) * widthpic, y: line.PositionY });
                                                                    //var layer = line.getLayer();
                                                                    //layer.draw();
                                                                }
                                                                else if (DoubleBankingVessels.length > 0) {
                                                                    bootbox.alert("<li>Berth is occupied.</li>");
                                                                }
                                                                else {
                                                                    var x1 = parseInt(frombollard.FromMeter());
                                                                    var y1 = parseInt(PositionY);
                                                                    var isFromToBerthSame = true;
                                                                    var isContinous;
                                                                    var isShifting;
                                                                    var IsValid = false;
                                                                    var fromberth = GetBerth(frombollard.BollardCode(), frombollard.BerthCode(), frombollard.QuayCode());
                                                                    var toberth = GetBerth(tobollard.BollardCode(), tobollard.BerthCode(), tobollard.QuayCode());
                                                                    var userdata = self.LoggedUserData();
                                                                    var vcn = line.id();
                                                                    var vmstatus = data1.MovementStatus();

                                                                    var vessel = updatePlannedItem(data1.VesselCallMovementID(), vcn, vmstatus, fromberth.PortCode(), fromberth.QuayCode(), fromberth.BerthCode(), toberth.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName(), toberth.BerthName(), frombollard.BollardName(), tobollard.BollardName());
                                                                    line.setPosition({ x: parseInt(frombollard.FromMeter() * widthpic), y: parseInt(y1 * heightpic) });
                                                                    line.name(vessel);
                                                                    var layer = line.getLayer();
                                                                    layer.draw();
                                                                }
                                                            }
                                                        }

                                                        else {
                                                            Msg = "";
                                                            if (getBerths.AreContinousBollards == false)
                                                                Msg = Msg + "<li>Bollards are discontinous.</li>";

                                                            $.each(self.berthsWithBollards(), function (index, item) {
                                                                var notSuited = $.inArray(item.BerthCode(), BerthsStatusList);
                                                                if (notSuited != -1) {
                                                                    var failed = getfailedBerthingRules(VesselData[0], item);
                                                                    Msg = getRulesMsg(item.BerthName(), failed);
                                                                }
                                                            });
                                                            bootbox.alert(Msg);
                                                            //line.setPosition({ x: Math.round(parseInt(line.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round(parseInt(revertpositon * heightpic)) });
                                                            //var layer = line.getLayer();
                                                            //layer.draw()
                                                        }
                                                    }
                                                    else {
                                                        bootbox.alert('Vessel ETB must be greater than current time.');

                                                        //line.setPosition({ x: Math.round(parseInt(line.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round(parseInt(revertpositon * heightpic)) });

                                                        //var layer = line.getLayer();
                                                        //layer.draw()
                                                    }
                                                    $("[id$='contextMenu']").css({
                                                        display: 'none'
                                                    });
                                                }
                                            }
                                        }
                                    });


                                //var myDiv = "HI1";
                                //var option = $('<option/>');
                                //option.attr({ 'value': 'myValue1' }).text(myDiv);
                                //btn2.append(option);
                                //var myDiv = "HI2";
                                //var option = $('<option/>');
                                //option.attr({ 'value': 'myValue2' }).text(myDiv);
                                //btn2.append(option);                    

                                div.append(btn).appendTo($("[id$='contextMenuB']"));
                                div2.append(btn2).appendTo($("[id$='contextMenuB']"));
                                div3.append(btn3).appendTo($("[id$='contextMenuB']"));

                                //alert($("#sldtext option:selected").val());
                            }
                        }
                    }
                }

                event.cancelBubble = true;
            });


            this.line.on("mousemove", function () {
                $("[id$='contextMenu']").css({
                    display: 'none'
                });
            });

            //vessel tooltip
            this.line.on("mouseover", function (event) {
                var data1 = this.name();
                var vesselinfo = getVesselInfo(data1);
                tooltip.setContent(vesselinfo);
                tooltip.currentPosition.left = event.layerX / widthpic;
                tooltip.currentPosition.top = event.layerY;
                tooltip.show();
            });

            this.line.on("mouseout", function (event) {
                tooltip.hide();
            });
        }

        function CheckIsVesselSafe(selectedvessel, fromBollard, toBollard) {

            var IsVesselSafe = true;

            var TimeConflict = false;
            var VesselSafeDistance = self.Configurations().SafeDistance();
            var VesselsConflict = [];
            var isSafeDistance = true;
            var FromPoint = 0;
            var ToPoint = 0;
            var PlannedVesselBerthTime;
            var PlannedVesselUnBerthTime;
            var SelectedVesselBerthTime;
            var SelectedVesselUnBerthTime;
            $.each(self.plannedvessesls(), function (index, item) {
                isSafeDistance = true;
                FromPoint = 0;
                ToPoint = 0;
                if (fromBollard > VesselSafeDistance) {
                    FromPoint = parseFloat(fromBollard) - parseFloat(VesselSafeDistance);
                    ToPoint = parseFloat(toBollard) + parseFloat(VesselSafeDistance);
                }
                else {

                    FromPoint = parseFloat(fromBollard);
                    ToPoint = parseFloat(toBollard) + parseFloat(VesselSafeDistance);

                }
                PlannedVesselBerthTime = new Date(Date.parse(item.BerthTime()));
                PlannedVesselUnBerthTime = new Date(Date.parse(item.UnBerthTime()));
                SelectedVesselBerthTime = selectedvessel.BerthTime();
                SelectedVesselUnBerthTime = selectedvessel.UnBerthTime();

                var PlannedVesselRange = moment().range(PlannedVesselBerthTime, PlannedVesselUnBerthTime);
                var SelectedVesselRange = moment().range(SelectedVesselBerthTime, SelectedVesselUnBerthTime);

                var TimeCondition1 = PlannedVesselRange.contains(SelectedVesselBerthTime);
                var TimeCondition2 = PlannedVesselRange.contains(SelectedVesselUnBerthTime);
                var TimeCondition3 = SelectedVesselRange.contains(PlannedVesselBerthTime);
                var TimeCondition4 = SelectedVesselRange.contains(PlannedVesselUnBerthTime);

                if (TimeCondition1 == true || TimeCondition2 == true || TimeCondition3 == true || TimeCondition4 == true) {
                    TimeConflict = true;
                }
                else {
                    TimeConflict = false;
                }

                if (TimeConflict == true) {
                    var NotSafeCondition1 = FromPoint >= parseFloat(item.FromBollardMeter()) && FromPoint <= parseFloat(item.ToBollardMeter());
                    var NotSafeCondition2 = ToPoint >= parseFloat(item.FromBollardMeter()) && ToPoint <= parseFloat(item.ToBollardMeter());
                    if (NotSafeCondition1 == true) {
                        isSafeDistance = false;
                    }
                    if (NotSafeCondition2 == true) {
                        isSafeDistance = false;
                    }
                }
                var VesselConflict = { VCN: item.VCN(), isVesselSafe: isSafeDistance }
                VesselsConflict.push(VesselConflict);
            });


            $.each(VesselsConflict, function (index, item) {
                if (item.isVesselSafe == false) {
                    isSafeDistance = false;
                    return;
                }

            });
            return isSafeDistance;
        }


        function CheckIsBerthAvailable(selectedvessel, fromBollard, toBollard) {

            var IsVesselSafe = true;

            var TimeConflict = false;
            var VesselSafeDistance = self.Configurations().SafeDistance();
            var VesselsConflict = [];
            var isSafeDistance = true;
            var FromPoint = 0;
            var ToPoint = 0;
            var PlannedVesselBerthTime;
            var PlannedVesselUnBerthTime;
            var SelectedVesselBerthTime;
            var SelectedVesselUnBerthTime;
            $.each(self.plannedvessesls(), function (index, item) {
                isSafeDistance = true;
                FromPoint = 0;
                ToPoint = 0;
                if (fromBollard > VesselSafeDistance) {
                    FromPoint = parseFloat(fromBollard) - parseFloat(VesselSafeDistance);
                    ToPoint = parseFloat(toBollard) + parseFloat(VesselSafeDistance);
                }
                else {

                    FromPoint = parseFloat(fromBollard);
                    ToPoint = parseFloat(toBollard) + parseFloat(VesselSafeDistance);

                }
                PlannedVesselBerthTime = new Date(Date.parse(item.BerthTime()));
                PlannedVesselUnBerthTime = new Date(Date.parse(item.UnBerthTime()));
                SelectedVesselBerthTime = selectedvessel.BerthTime();
                SelectedVesselUnBerthTime = selectedvessel.UnBerthTime();

                var PlannedVesselRange = moment().range(PlannedVesselBerthTime, PlannedVesselUnBerthTime);
                var SelectedVesselRange = moment().range(SelectedVesselBerthTime, SelectedVesselUnBerthTime);

                var TimeCondition1 = PlannedVesselRange.contains(SelectedVesselBerthTime);
                var TimeCondition2 = PlannedVesselRange.contains(SelectedVesselUnBerthTime);
                var TimeCondition3 = SelectedVesselRange.contains(PlannedVesselBerthTime);
                var TimeCondition4 = SelectedVesselRange.contains(PlannedVesselUnBerthTime);

                if (TimeCondition1 == true || TimeCondition2 == true || TimeCondition3 == true || TimeCondition4 == true) {
                    TimeConflict = true;
                }
                else {
                    TimeConflict = false;
                }

                if (TimeConflict == true) {
                    var NotSafeCondition1 = FromPoint >= parseFloat(item.FromBollardMeter()) && FromPoint <= parseFloat(item.ToBollardMeter());
                    var NotSafeCondition2 = ToPoint >= parseFloat(item.FromBollardMeter()) && ToPoint <= parseFloat(item.ToBollardMeter());
                    if (NotSafeCondition1 == true) {
                        isSafeDistance = false;
                    }
                    if (NotSafeCondition2 == true) {
                        isSafeDistance = false;
                    }
                }
                var VesselConflict = { VCN: item.VCN(), isVesselSafe: isSafeDistance }
                VesselsConflict.push(VesselConflict);
            });


            $.each(VesselsConflict, function (index, item) {
                if (item.isVesselSafe == false) {
                    isSafeDistance = false;
                    return;
                }

            });
            return isSafeDistance;
        }


        function IsVesselSafe(plannedvessel, selectedshape, fromBollard, toBollard, selectedshapeBerthTime, selectedshapeUnBerthTime) {
            var isSafeDistance = true;
            var TimeConflict = false;
            var PlannedVesselBerthTime;
            var PlannedVesselUnBerthTime;
            var SelectedVesselBerthTime;
            var SelectedVesselUnBerthTime;
            var FromPoint = 0;
            var ToPoint = 0;
            var VesselSafeDistance = self.Configurations().SafeDistance();
            if (fromBollard.FromMeter() > VesselSafeDistance) {
                FromPoint = parseFloat(fromBollard.FromMeter()) - parseFloat(VesselSafeDistance);
                ToPoint = parseFloat(toBollard.FromMeter()) + parseFloat(VesselSafeDistance);
            }
            else {

                FromPoint = parseFloat(fromBollard.FromMeter());
                ToPoint = parseFloat(toBollard.FromMeter()) + parseFloat(VesselSafeDistance);

            }

            PlannedVesselBerthTime = new Date(Date.parse(plannedvessel.BerthTime()));
            PlannedVesselUnBerthTime = new Date(Date.parse(plannedvessel.UnBerthTime()));
            SelectedVesselBerthTime = selectedshapeBerthTime;
            SelectedVesselUnBerthTime = selectedshapeUnBerthTime;


            var PlannedVesselRange = moment().range(PlannedVesselBerthTime, PlannedVesselUnBerthTime);
            var SelectedVesselRange = moment().range(SelectedVesselBerthTime, SelectedVesselUnBerthTime);

            var TimeCondition1 = PlannedVesselRange.contains(SelectedVesselBerthTime);
            var TimeCondition2 = PlannedVesselRange.contains(SelectedVesselUnBerthTime);
            var TimeCondition3 = SelectedVesselRange.contains(PlannedVesselBerthTime);
            var TimeCondition4 = SelectedVesselRange.contains(PlannedVesselUnBerthTime);

            if (TimeCondition1 == true || TimeCondition2 == true || TimeCondition3 == true || TimeCondition4 == true) {
                TimeConflict = true;
            }
            else {
                TimeConflict = false;
            }

            //var SelectedVesselDuration = moment.duration(moment(selectedshape.attrs.UnBerthTime).diff(selectedshape.attrs.BerthTime)).asHours();           
            if (TimeConflict == true) {

                var NotSafeCondition1 = FromPoint >= parseFloat(plannedvessel.FromBollardMeter()) && FromPoint <= parseFloat(plannedvessel.ToBollardMeter());
                var NotSafeCondition2 = ToPoint >= parseFloat(plannedvessel.FromBollardMeter()) && ToPoint <= parseFloat(plannedvessel.ToBollardMeter());
                if (NotSafeCondition1 == true) {
                    isSafeDistance = false;
                }
                if (NotSafeCondition2 == true) {
                    isSafeDistance = false;
                }
            }



            return isSafeDistance;

        }

        function GetTimePosition(BerthTime, UnBerthTime) {
            var SetTime = moment(self.SelectedDate()).startOf('day');
            var PositionY;
            var VesselWidth;
            if (moment(BerthTime).date() == moment(self.SelectedDate()).date()) {
                PositionY = moment(BerthTime).hour();
                VesselWidth = moment.duration(moment(UnBerthTime).diff(BerthTime)).asHours();
            }
            else if (moment(BerthTime).date() < moment(self.SelectedDate()).date()) {
                PositionY = 0;
                VesselWidth = moment.duration(moment(UnBerthTime).diff(moment(SetTime))).asHours();
            }
            else {
                PositionY = moment(BerthTime).hour() + 24;
                VesselWidth = moment.duration(moment(UnBerthTime).diff(BerthTime)).asHours();
            }

            return PositionY;

        }
        function AreCollidingBollards(plannedvessel, selectedshape, fromBollard, toBollard, selectedshapeBerthTime, selectedshapeUnBerthTime) {
            var isColliding = false;
            if (plannedvessel.FromQuayCode() == fromBollard.QuayCode()) {

                var Coliding1 = parseFloat(fromBollard.FromMeter()) >= parseFloat(plannedvessel.FromBollardMeter()) && parseFloat(fromBollard.FromMeter()) <= parseFloat(plannedvessel.ToBollardMeter());
                var Coliding2 = parseFloat(toBollard.FromMeter()) >= parseFloat(plannedvessel.FromBollardMeter()) && parseFloat(toBollard.FromMeter()) <= parseFloat(plannedvessel.ToBollardMeter());

                if (Coliding1 == true || Coliding2 == true)
                    isColliding = true;

                //   console.log('Dragged Vessel', fromBollard.FromMeter(), toBollard.FromMeter());
                //   console.log('Planned Vessel', plannedvessel.VCN() + '-' + plannedvessel.FromBollardMeter() + '-' + plannedvessel.ToBollardMeter());
                //   console.log('Colliding', Coliding1 + '-' + Coliding2);

            }


            return isColliding;
        }

        function isInTimeRange(plannedvessel, selectedshape, fromBollard, toBollard, selectedshapeBerthTime, selectedshapeUnBerthTime) {
            var WithinTime;
            var PlannedVesselBerthTime;
            var PlannedVesselUnBerthTime;
            var SelectedVesselBerthTime = selectedshapeBerthTime;
            var SelectedVesselUnBerthTime = selectedshapeUnBerthTime;
            PlannedVesselBerthTime = new Date(Date.parse(self.plannedvessesls()[i].BerthTime()));
            PlannedVesselUnBerthTime = new Date(Date.parse(self.plannedvessesls()[i].UnBerthTime()));

            var PlannedVesselRange = moment().range(PlannedVesselBerthTime, PlannedVesselUnBerthTime);
            var SelectedVesselRange = moment().range(SelectedVesselBerthTime, SelectedVesselUnBerthTime);
            var TimeCondition1 = PlannedVesselRange.contains(SelectedVesselBerthTime);
            var TimeCondition2 = PlannedVesselRange.contains(SelectedVesselUnBerthTime);
            if (TimeCondition1 == true && TimeCondition2 == true)
                WithinTime = true;
            else
                WithinTime = false;

            return WithinTime

        }
     
        function PShape(data1, prevsberthslength) {

            var line = this;
            shapeCount++;
            this.Id = ko.observable(data1.VCN());
            this.name = ko.observable(data1.VesselName());
            this.aX = ko.observable(0);
            this.aY = ko.observable(0);
            this.IsDraggable = ko.observable(true);
            var PositionY;
            var VesselWidth;
            var SetTime = moment(self.SelectedDate()).startOf('day');


            var SelectedDate = moment(moment(self.SelectedDate()).format('YYYY-MM-DD'));
            var BerthDate = moment(moment(data1.BerthTime()).format('YYYY-MM-DD'));
            //console.log('VCMID=BerthTime-SelectedDate', data1.VesselCallMovementID() + '--' + moment(data1.BerthTime()).format('YYYY-MM-DD') + '-----' + moment(self.SelectedDate()).format('YYYY-MM-DD'));

            if (SelectedDate.isSame(BerthDate)) {
                PositionY = moment(data1.BerthTime()).hour();
                VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(data1.BerthTime())).asHours();
            }
            else if (BerthDate.isBefore(SelectedDate)) {
                PositionY = 0;
                VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(moment(SetTime))).asHours();
            }
            else {
                PositionY = moment(data1.BerthTime()).hour() + 24;
                VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(data1.BerthTime())).asHours();
            }


            //if (moment(data1.BerthTime()).date() == moment(self.SelectedDate()).date()) {
            //    PositionY = moment(data1.BerthTime()).hour();
            //    VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(data1.BerthTime())).asHours();
            //}
            //else if (moment(data1.BerthTime()).date() < moment(self.SelectedDate()).date()) {
            //    PositionY = 0;
            //    VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(moment(SetTime))).asHours();
            //}
            //else {
            //    PositionY = moment(data1.BerthTime()).hour() + 24;
            //    VesselWidth = moment.duration(moment(data1.UnBerthTime()).diff(data1.BerthTime())).asHours();
            //}




            this.width = ko.observable(Math.round(parseInt(VesselWidth * heightpic) / nohours));
            this.length = ko.observable(Math.round(parseInt(data1.LOA() * widthpic)));

            this.ReasonForVisit = ko.observable(data1.ReasonforVisit());
            this.Draft = ko.observable(data1.Draft());

            this.StartPositionX = ko.observable(Math.round(parseInt(data1.PositionX() + prevsberthslength)) * widthpic);
            this.StartPositionY = ko.observable(Math.round((parseInt(PositionY)) * heightpic) / nohours);

            this.IsDraggable(false);
            if (data1.MovementStatus() == "MPEN" || data1.MovementStatus() == null)
                this.linecolor = ko.observable("orange");
            else if (data1.MovementType() == "SGMV") {
                //this.linecolor = ko.observable("yellow");
                this.linecolor = self.Configurations().SailedColor;
            }
            else if (data1.MovementStatus() == "BERT")
                this.linecolor = self.Configurations().BerthedColor;
            else if (data1.MovementStatus() == "SCH")
                this.linecolor = self.Configurations().ScheduleColor;
            else if (data1.MovementStatus() == "CONF")
                this.linecolor = self.Configurations().ConfirmColor;
            else if (data1.MovementStatus() == "SALD")
                this.linecolor = self.Configurations().SailedColor;


            this.line = new Kinetic.Line({
                points: [line.aX(), line.aY(),
                        parseInt(line.aX()) + parseInt(line.length()), line.aY(),
                        parseInt(line.aX()) + parseInt(line.length()), parseInt(line.aY()) + parseInt(line.width()) - 20,
                        parseInt(line.aX()) + parseInt(line.length()) / 2, parseInt(line.aY()) + parseInt(line.width()),
                        line.aX(), parseInt(line.aY()) + parseInt(line.width()) - 20],
                stroke: "black",
                strokeWidth: 2,
                fill: line.linecolor(),
                closed: true,
                draggable: this.IsDraggable(),
                dragBoundFunc: function (pos) {
                    var minX = TotalGridStage.getX();
                    var maxX = TotalGridStage.getX() + TotalGridStage.getWidth() - this.attrs.length;
                    var minY = TotalGridStage.getY();
                    var maxY = TotalGridStage.getY() + TotalGridStage.getHeight() - this.attrs.width;

                    var X = pos.x;
                    var Y = pos.y;
                    if (X < minX) {
                        X = minX;
                    }
                    if (X > maxX) {
                        X = maxX;
                    }
                    return ({
                        x: X,
                        y: Y
                    });
                },
                id: line.Id(),
                name: data1,
                LOA: data1.LOA(),
                BerthTime: data1.BerthTime(),
                PositionY: line.StartPositionY(),
                UnBerthTime: data1.UnBerthTime(),
                x: line.StartPositionX(),
                y: line.StartPositionY(),
                length: line.length(),
                width: line.width()
            });

            this.line.on("dragend", function () {

                var selectedshape = line.Id;
                var isFromToBerthSame = true;
                var IsValid = false;
                var fromberthvalid;
                var toberthvalid;
                var isContinous;
                var x1 = parseInt(this.getX() / widthpic);
                var y1 = parseInt(this.getY() / heightpic);

                var frombollard = GetFromBollardPort(this.getX() / widthpic);
                var tobollard = GetToBollardPort((x1 + this.attrs.length / widthpic));

                isContinous = CheckDiscontinous(frombollard, tobollard);


                var fromberth = GetBerthPort(frombollard.BollardCode());
                var toberth = GetBerthPort(tobollard.BollardCode());
                var userdata = self.LoggedUserData();
                var vcn = this.id();
                var fromberthvalid = passDataToBerthingRules(vcn, fromberth);


                if (fromberth.BerthCode() != toberth.BerthCode()) {
                    toberthvalid = passDataToBerthingRules(vcn, toberth)
                    isFromToBerthSame = false;
                }

                if (isFromToBerthSame == true) {
                    if (fromberthvalid.ValidBerth == true && isContinous == true)
                        IsValid = true;
                } else {
                    if (fromberthvalid.ValidBerth && toberthvalid.ValidBerth && isContinous == true)
                        IsValid = true;
                }

                if (IsValid) {

                    var vmstatus = "SCH";
                    if (this.name().MovementStatus() == "BERT") {
                        vmstatus = "SCH";
                        //this.attrs.fill = "#FFBF00";
                        this.attrs.fill = self.Configurations().ScheduleColor;

                        this.draggable(true);
                    }
                    //var vessel = updatePlannedItem(vcn, vmstatus, frombollard.PortCode(), frombollard.QuayCode(), frombollard.BerthCode(), tobollard.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName());
                    var vessel = updatePlannedItem(vcn, vmstatus, frombollard.PortCode(), frombollard.QuayCode(), frombollard.BerthCode(), tobollard.BerthCode(), frombollard.BollardCode(), tobollard.BollardCode(), frombollard.FromMeter(), tobollard.FromMeter(), y1, fromberth.BerthName(), toberth.BerthName(), frombollard.BollardName(), tobollard.BollardName());
                    this.name(vessel);
                    var layer = this.getLayer();
                    layer.draw();
                }
                else {

                    var Msg = ""
                    Msg = RulesAlert(fromberth, toberth, frombollard, tobollard, isFromToBerthSame, fromberthvalid, toberthvalid, isContinous, true, true);
                    bootbox.alert(Msg);
                    this.setPosition({ x: Math.round(parseInt(this.name().PositionX() + prevsberthslength)) * widthpic, y: Math.round((parseInt(PositionY)) * heightpic) / nohours });
                    var layer = this.getLayer();
                    layer.draw();
                }
            });


            //context menu on vessel
            this.line.on("click", function (event) {

                var line = this;
                var vcn = this.id();
                var draggable = this.draggable();
                //var movementStatus = GetMovementStatus(vcn);  // Commented by sandeep on 27-04-2015
                var movementStatus = data1.MovementStatus(); // Added by sandeep on 27-04-2015
                if (movementStatus == "SCH" && draggable == true) {


                    $("[id$='contextMenu']").css({
                        display: 'inline',
                        position: 'absolute',
                        top: event.layerY,
                        left: event.layerX,
                        opacity: .8
                    });


                    $("[id$='contextMenuH']").html('');
                    $('<span />').html('Actions').appendTo($("[id$='contextMenuH']"));
                    $("[id$='contextMenuB']").html(''); //clear                                
                    var div = $('<div />'),
                         btn = $('<input />', {
                             type: 'button',
                             value: 'UnSchedule',
                             id: line,
                             on: {
                                 click: function () {

                                     var layer = line.getLayer();
                                     line.remove();
                                     layer.draw();
                                     UnScheduleVessel(line.VCMID());
                                     $("[id$='contextMenu']").css({
                                         display: 'none'
                                     });

                                 }
                             }
                         });

                    var div2 = $('<br/><div />'),
                     btn2 = $('<input />', {
                         type: 'button',
                         value: 'Confirm',
                         id: line,
                         on: {
                             click: function () {
                                 //line.attrs.fill = "#006400";
                                 line.attrs.fill = self.Configurations().ConfirmColor();
                                 line.draggable(false);
                                 var layer = line.getLayer();
                                 layer.draw();
                                 ConfirmVessel(line.id());
                                 $("[id$='contextMenu']").css({
                                     display: 'none'
                                 });

                             }
                         }
                     });
                    div.append(btn).appendTo($("[id$='contextMenuB']"));
                    div2.append(btn2).appendTo($("[id$='contextMenuB']"));
                }
                else if (movementStatus == "CONF" && draggable == true ) {

                    $("[id$='contextMenu']").css({
                        display: 'inline',
                        position: 'absolute',
                        top: event.layerY,
                        left: event.layerX,
                        opacity: .8
                    });
                    $("[id$='contextMenuH']").html('');
                    $('<span />').html('Actions').appendTo($("[id$='contextMenuH']"));
                    $("[id$='contextMenuB']").html(''); //clear                                
                    var div = $('<div />'),
                        btn = $('<input />', {
                            type: 'button',
                            value: 'UnSchedule',
                            id: line,
                            on: {
                                click: function () {
                                    var layer = line.getLayer();
                                    line.remove();
                                    layer.draw();
                                    UnScheduleVessel(line.VCMID());
                                    $("[id$='contextMenu']").css({
                                        display: 'none'
                                    });

                                }
                            }
                        });

                    div.append(btn).appendTo($("[id$='contextMenuB']"));
                }

                event.cancelBubble = true;
            });


            this.line.on("mousemove", function () {
                $("[id$='contextMenu']").css({
                    display: 'none'
                });
            });


            //vessel tooltip
            this.line.on("mouseover", function (event) {
                var data1 = this.name();
                var vesselinfo = getVesselInfo(data1);
                tooltip.setContent(vesselinfo);
                tooltip.currentPosition.left = event.layerX / widthpic;
                tooltip.currentPosition.top = event.layerY;
                tooltip.show();
            });

            this.line.on("mouseout", function (event) {
                tooltip.hide();
            });
        }


        function theyAreColliding(shape, line) {
            if (rect2.aX() > shape.aX() && rect2.aX() < (shape.aX() + shape.width()))
                return true;
            else
                return false;
        }

        function colliding(shape, line, X, Y) {

            var status = false;
            var rec1Top = shape.aY();
            var rec1Bottom = shape.aY() + shape.length();
            var rec1Left = shape.aX();
            var rec1Right = shape.aX() + shape.width();
            var rec2Top = Y;
            var rec2Bottom = Y + line.length();
            var rec2Left = X;
            var rec2Right = X + line.width();
            if (!(rec1Bottom < rec2Top ||
             rec1Top > rec2Bottom ||
             rec1Left > rec2Right ||
             rec1Right < rec2Left))
                status = true;
            return status;
        }


        function getInfo(type, data) {
            var data;
            if (type == 'Q') {
                var data = "<table class='tooltipcss' id='" + data.QuayCode() + "'><tr><td class='tooltipheader'>Quay Code:</td><td>" + data.QuayCode() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Quay Name:</td><td>" + data.QuayName() + "</td></tr>" +
                      "<tr><td class='tooltipheader'>Quay Length:</td><td>" + data.QuayLength() + "</td></tr>";
            }
            else if (type == 'B') {
                var data = "<table class='tooltipcss' id='" + data.BerthCode() + "'><tr><td class='tooltipheader'>Berth Code:</td><td>" + data.BerthCode() + "</td></tr>" +
                "<tr><td class='tooltipheader'>Berth Name:</td><td>" + data.BerthName() + "</td></tr>" +
                "<tr><td class='tooltipheader'>Berth Length:</td><td>" + data.BerthLength() + "</td></tr>" +
                 "<tr><td class='tooltipheader'>Berth Draft:</td><td>" + data.Draft() + "</td></tr>" +
                "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.CargoTypeNames() + "</td></tr>";
            }
            return data;
        }
       
        function getVesselInfo(data) {
            var suitableBerths = "";
            var color = self.Configurations().ArrestedColor();
            var SuitableBerthsList = [];
            SuitableBerthsList = data.SuitableBerthsList();
            $.each(SuitableBerthsList, function (index, suitableberth) {
                if (self.ViewType() == "Quay") {
                    if (suitableberth.QuayCode == self.selectedQuayCode()) {
                        if (suitableBerths == "")
                            suitableBerths = suitableberth.BerthName;
                        else
                            suitableBerths = suitableBerths + ', ' + suitableberth.BerthName;
                    }

                }
                else if (self.ViewType() == "Port") {
                    if (suitableBerths == "")
                        suitableBerths = suitableberth.BerthName;
                    else
                        suitableBerths = suitableBerths + ', ' + suitableberth.BerthName;
                }

            });

            if (data.MovementStatus() == "BERT" || data.MovementStatus() == "SALD") {
                if (data.MovementType() == "ARMV" || data.MovementType() == "WRMV" || data.MovementType() == "SHMV") {
                    if (data.isVesselArrested()) {
                        var data = "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td style='background-color:" + color + ";color:white'>" + data.VCN() + "</td>  </tr>" +
                            "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardName() + " to " + data.ToBollardName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.VesselLength() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>MovementType:</td><td>" + data.MovementTypeName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Draft:</td><td>" + data.MaxDraft() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Tidal:</td><td>" + data.IsTidal() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Mooring Bollards:</td><td>" + data.MooringBowBollard() + " to " + data.MooringStemBollard() + "</td></tr>";
                    } else {
                        var data = "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td>" + data.VCN() + "</td>  </tr>" +
                           "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardName() + " to " + data.ToBollardName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.VesselLength() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>MovementType:</td><td>" + data.MovementTypeName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Draft:</td><td>" + data.MaxDraft() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Tidal:</td><td>" + data.IsTidal() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Mooring Bollards:</td><td>" + data.MooringBowBollard() + " to " + data.MooringStemBollard() + "</td></tr>";
                    }
                }
                else {
                    if (data.isVesselArrested()) {
                        var data = "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td style='background-color: " + color + ";color:white'>" + data.VCN() + "</td>  </tr>" +
                            "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardName() + " to " + data.ToBollardName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.VesselLength() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>MovementType:</td><td>" + data.MovementTypeName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Draft:</td><td>" + data.MaxDraft() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Tidal:</td><td>" + data.IsTidal() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
                            "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>";
                    } else {
                        var data = "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td>" + data.VCN() + "</td>  </tr>" +
                           "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardName() + " to " + data.ToBollardName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.VesselLength() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>MovementType:</td><td>" + data.MovementTypeName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Draft:</td><td>" + data.MaxDraft() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Tidal:</td><td>" + data.IsTidal() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
                           "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>";
                    }
                }

            }
            else {
                if (data.isVesselArrested()) {
                    var data = "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td style='background-color:" + color + ";color:white'>" + data.VCN() + "</td>  </tr>" +
                        "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardName() + " to " + data.ToBollardName() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.VesselLength() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>MovementType:</td><td>" + data.MovementTypeName() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Draft:</td><td>" + data.MaxDraft() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Tidal:</td><td>" + data.IsTidal() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>" +
                        "<tr><td class='tooltipheader'>Suitable-Berths:</td><td style='color:red' >" + suitableBerths + "</td></tr>";
                } else {
                    var data = "<table class='tooltipcss' id='" + data.VCN() + "'><tr><td class='tooltipheader'>VCN:</td><td>" + data.VCN() + "</td>  </tr>" +
                       "<tr><td class='tooltipheader'>Vessel Name:</td><td>" + data.VesselName() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Vessel Type:</td><td>" + data.VesselType() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Berth:</td><td>" + data.BerthName() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Bollards:</td><td>" + data.FromBollardName() + " to " + data.ToBollardName() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Vessel Length:</td><td>" + data.VesselLength() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>MovementType:</td><td>" + data.MovementTypeName() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Reason For Visit:</td><td>" + data.ReasonforVisitName() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Draft:</td><td>" + data.MaxDraft() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Tidal:</td><td>" + data.IsTidal() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Cargo Types:</td><td>" + data.ArrivalCommoditiesNames() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Agent:</td><td>" + data.Agent() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>SideAlongSide:</td><td>" + data.SideAlongSide() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Berth Time:</td><td>" + data.BerthTime() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>UnBerth Time:</td><td>" + data.UnBerthTime() + "</td></tr>" +
                       "<tr><td class='tooltipheader'>Suitable-Berths:</td><td style='color:red' >" + suitableBerths + "</td></tr>";
                }
            }

            return data;
        }

        function GetMovementStatus(vcn) {
            var movementstatus;
            var exitloop;
            $.each(self.plannedvessesls(), function (index, item) {
                if (item.VCN() == vcn) {
                    movementstatus = item.MovementStatus();
                    exitloop = false;
                }
                return exitloop;
            });
            return movementstatus;
        }

        //get Berths Between Bollards
        function getBerthsbetweenBollards(fromBollard, toBollard) {
            var berthsList = [];
            var _AreContinousBollards = true;

            $.each(self.quayBollards(), function (index, bollard) {
                if (bollard.FromMeter() >= fromBollard.FromMeter() && bollard.FromMeter() <= toBollard.FromMeter()) {
                    var BerthContains = $.inArray(bollard.BerthCode(), berthsList);
                    if (BerthContains == -1)
                        berthsList.push(bollard.BerthCode());

                    if (bollard.Continous() == "N") {
                        _AreContinousBollards = false
                    }
                }



            });

            var obj = { berthsList: berthsList, AreContinousBollards: _AreContinousBollards }
            return obj;


        }


        //GetConflictingVesselsinPlannedVessels        
        function GetConflictingVesselsinPlannedVessels(VCMID, fromBollard, toBollard, selectedshapeBerthTime, selectedshapeUnBerthTime) {
            var CollidingVessels = [];

            $.each(self.plannedvessesls(), function (index, _plannedvessel) {
                if (VCMID != _plannedvessel.VesselCallMovementID() && _plannedvessel.FromQuayCode() == fromBollard.QuayCode()) {
                    var WithinTime;
                    var PlannedVesselBerthTime;
                    var PlannedVesselUnBerthTime;
                    var SelectedVesselBerthTime = moment(selectedshapeBerthTime);
                    var SelectedVesselUnBerthTime = moment(selectedshapeUnBerthTime);
                    var TimeConflict = false;
                    var BollardConflict = false;
                    var DoubleBank = false;
                    var SafeFromPoint;
                    var SafeToPoint;
                    var VesselCollide = false;
                    var VesselNotSafe = false;
                    var VesselSafeDistance = self.Configurations().SafeDistance();
                    if (fromBollard.FromMeter() > VesselSafeDistance) {
                        SafeFromPoint = parseFloat(fromBollard.FromMeter()) - parseFloat(VesselSafeDistance);
                        SafeToPoint = parseFloat(toBollard.FromMeter()) + parseFloat(VesselSafeDistance);
                    }
                    else {
                        SafeFromPoint = parseFloat(fromBollard.FromMeter());
                        SafeToPoint = parseFloat(toBollard.FromMeter()) + parseFloat(VesselSafeDistance);
                    }



                    //    PlannedVesselBerthTime = new Date(Date.parse(_plannedvessel.BerthTime()));
                    //  PlannedVesselUnBerthTime = new Date(Date.parse(_plannedvessel.UnBerthTime()));

                    PlannedVesselBerthTime = moment(_plannedvessel.BerthTime());
                    PlannedVesselUnBerthTime = moment(_plannedvessel.UnBerthTime());


                    var PlannedVesselRange = moment().range(PlannedVesselBerthTime, PlannedVesselUnBerthTime);
                    var SelectedVesselRange = moment().range(SelectedVesselBerthTime, SelectedVesselUnBerthTime);
                    var TimeCondition1 = PlannedVesselRange.contains(SelectedVesselBerthTime);
                    var TimeCondition2 = PlannedVesselRange.contains(SelectedVesselUnBerthTime);
                    var TimeCondition3 = SelectedVesselRange.contains(PlannedVesselBerthTime);
                    //var TimeCondition4 = SelectedVesselRange.contains(SelectedVesselUnBerthTime); // Commented by sandeep on 12-10-215
                    var TimeCondition4 = SelectedVesselRange.contains(PlannedVesselUnBerthTime); // Added by sandeep on 12-10-2015


                    if (TimeCondition1 == true || TimeCondition2 == true || TimeCondition3 == true || TimeCondition4 == true) {
                        TimeConflict = true;

                        //    console.log('DoubleBank', DoubleBank);
                    }

                    if (TimeConflict == true) {

                        var Coliding1 = parseFloat(_plannedvessel.FromBollardMeter()) >= parseFloat(fromBollard.FromMeter()) && parseFloat(_plannedvessel.FromBollardMeter()) <= parseFloat(toBollard.FromMeter());
                        var Coliding2 = parseFloat(fromBollard.FromMeter()) >= parseFloat(_plannedvessel.FromBollardMeter()) && (parseFloat(fromBollard.FromMeter()) <= _plannedvessel.ToBollardMeter());
                        var Coliding3 = parseFloat(toBollard.FromMeter()) >= parseFloat(_plannedvessel.FromBollardMeter()) && parseFloat(toBollard.FromMeter()) <= parseFloat(_plannedvessel.ToBollardMeter());


                        if (TimeCondition1 == true && TimeCondition2 == true && Coliding2 == true && Coliding3 == true) {
                            DoubleBank = true
                        }



                        var VesselSafe1 = parseFloat(_plannedvessel.FromBollardMeter()) >= parseFloat(SafeFromPoint) && parseFloat(_plannedvessel.FromBollardMeter()) <= parseFloat(SafeToPoint);
                        var VesselSafe2 = parseFloat(SafeFromPoint) >= parseFloat(_plannedvessel.FromBollardMeter()) && (parseFloat(SafeFromPoint) <= _plannedvessel.ToBollardMeter());
                        var VesselSafe3 = parseFloat(SafeToPoint) >= parseFloat(_plannedvessel.FromBollardMeter()) && parseFloat(SafeToPoint) <= parseFloat(_plannedvessel.ToBollardMeter());
                        //if (_plannedvessel.VCN() == "VCNDB1500069") {
                        // //   console.log('TC1-TC2-TC3-TC4', TimeCondition1 + '-' + TimeCondition2 + '-' + TimeCondition3 + '-' + TimeCondition4);
                        // //   console.log('Coliding1-Coliding2-Coliding3', Coliding1 + '-' + Coliding2 + '-' + Coliding3);
                        //    // console.log('VesselSafe1-VesselSafe2-VesselSafe3', VesselSafe1 + '-' + VesselSafe2 + '-' + VesselSafe3);
                        //}
                        if (Coliding1 == true || Coliding2 == true || Coliding3 == true) {
                            VesselCollide = true;
                        }
                        if (VesselSafe1 == true || VesselSafe2 == true || VesselSafe3 == true) {
                            VesselNotSafe = true;
                        }
                        if (VesselNotSafe == true || VesselCollide == true)
                            CollidingVessels.push({ VCMID: _plannedvessel.VesselCallMovementID, VCN: _plannedvessel.VCN, DoubleBank: DoubleBank, VesselCollide: VesselCollide, VesselNotSafe: VesselNotSafe });
                    }

                }


            });


            return CollidingVessels;

        }



        self.Initialize();

    }

    IPMSRoot.BerthPlanningViewModel = BerthPlanningViewModel;

}(window.IPMSROOT));
