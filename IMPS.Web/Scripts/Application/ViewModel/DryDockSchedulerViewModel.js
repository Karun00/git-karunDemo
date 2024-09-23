(function (IPMSRoot) {
    var DryDockSchedulerViewModel = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSROOT.validationHelper();
        self.DryDockScheduleData = ko.observable(new IPMSROOT.DryDockSchedulerModel());
        self.pendingVesselList = ko.observableArray();
        self.DryDockScheduleList = ko.observableArray();
        self.MovementPopUp = ko.observable();
        self.GetDockList = ko.observableArray();
        self.DryDockScheduleModel = ko.observableArray();
        self.formattedDate = ko.observable();
        self.IsScheduleEnable = ko.observable(false);

        self.LoadPendingVesselsForDrydock = function () {

            self.viewModelHelper.apiGet('api/PendingVesselForDryDock',
            null,
              function (result) {
                  self.pendingVesselList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.DryDockSchedulerModel(item);
                  }));
              }, null, null, false);


        };

        self.LoadDryDockScheduler = function () {
            self.viewModelHelper.apiGet('api/ScheduledVesselForDryDock', null,
             function (result) {
                 self.DryDockScheduleList(ko.utils.arrayMap(result, function (item) {
                     return new IPMSRoot.ScheduleModel(item);
                 }));

             }, null, null, false);

        };

        self.LoadDockList = function () {
            self.viewModelHelper.apiGet('api/GetDockList', null,
                  function (result) {
                      var berthlist = $.map(result, function (item) {
                          return new IPMSRoot.BerthModel(item);
                      });
                      self.GetDockList(berthlist);

                  }, null, null, false);
        };

        self.ChangeBerth = function (e) {
            self.IsScheduleEnable(false);
            $(".btn").removeClass("btn btn-xs blue").addClass("btn btn-xs default");
            var dataItem = this.dataItem(e.item.index());
            self.DryDockScheduleData().PortCode(dataItem.PortCode);
            self.DryDockScheduleData().QuayCode(dataItem.QuayCode);
            self.DryDockScheduleData().BerthCode(dataItem.BerthCode);
        }

        self.GridClick = function (data) {
            $('#spanScheduleFromDate').css('display', 'none');
            $('#spanScheduleToDateError').css('display', 'none');
            
            $('#suppDockID').val(parseInt(data.SuppDryDockID()));
            $('#VCN').text(data.VCN());
            $('#VesselName').text(data.VesselName());
            $('#FromDate').text(data.FromDate());
            $('#ToDate').text(data.ToDate());

            var d1 = new Date();
            d2 = new Date(d1);
            d2.setMinutes(d1.getMinutes() + 30);

            $("#ScheduleFromDate").kendoDateTimePicker({
                change: onChange,
                min: d2,
                format: "yyyy-MM-dd HH:mm",
                timeFormat: "HH:mm"
                , month: { empty: '<span class=k-state-disabled>#= data.value #</span>' }
            });

            


            $("#ScheduleFromDate").val('');
            $("#ScheduleTODate").val('');
            var dockcode = self.DryDockScheduleData().BerthCode();
            var quaycode = self.DryDockScheduleData().QuayCode();
            var portcode = self.DryDockScheduleData().PortCode();
            
            if (dockcode == "" || dockcode == undefined) {
                $("#MovementModel").modal('hide');
                toastr.warning("Please Select Dock", "Dry Dock Scheduler");
                $("#ddldock").focus();
                return false;
            } else {
                var vesselLenth = parseFloat(data.LengthOverallInM());

                var berth = ko.utils.arrayFilter(self.GetDockList(), function (item) {                    
                    return item.PortCode() == portcode && item.QuayCode() == quaycode && item.BerthCode() == dockcode;
                });

                var berthLength = parseFloat(berth[0].Lengthm());

                if (vesselLenth <= berthLength) {
                    $("#MovementModel").modal('show');                
                    $('#Dock').text(berth[0].BerthName());
                } else {
                    $("#MovementModel").modal('hide');
                    toastr.warning("Vessel length should be less than Berth length", "Dry Dock Scheduler");
                    $("#ddldock").focus();
                    return false;
                }
            }
            
            self.MovementPopUp(data);

        }


        self.ScheduleDock = function (model) {           
            var berth = true;
            var fromDate = $("#ScheduleFromDate").val();
            var toDate = $("#ScheduleTODate").val();
            $('#spanScheduleFromDate').text('');
            $('#ScheduleTODate').text('');
            if (fromDate == '' && toDate == '') {
                $('#spanScheduleFromDate').css('display', '');
                $('#spanScheduleFromDate').text('Please enter schedule from date');

                $('#spanScheduleToDateError').css('display', '');
                $('#spanScheduleToDateError').text('Please enter schedule to date');
                return false;
            }
            if (fromDate == '') {
                $('#spanScheduleFromDate').css('display', '');
                $('#spanScheduleFromDate').text('Please enter schedule from date');
                return false;
            }

            if (toDate == '') {
                $('#spanScheduleToDateError').css('display', '');
                $('#spanScheduleToDateError').text('Please enter schedule to date');
                return false;
            }
            

            var count = 0;
            var berthlength = ko.utils.arrayFilter(self.DryDockScheduleList(), function (item) {
                if (item.ScheduleFromDate() != "Invalid date" && item.ScheduleToDate() != "Invalid date") {                       
                    var From = kendo.toString(new Date(fromDate));
                    var To = kendo.toString(new Date(toDate));
                    var SFrom = kendo.toString(new Date(item.ScheduleFromDate()));
                    var STo = kendo.toString(new Date(item.ScheduleToDate()));

                    if (item.ScheduleStatus() == 'DOCK') {
                        var SFrom = kendo.toString(new Date(item.EnteredDockDateTime()));
                    }
                    if (item.ScheduleStatus() == 'UNDK') {
                        var SFrom = kendo.toString(new Date(item.EnteredDockDateTime()));
                        var STo = kendo.toString(new Date(item.LeftDockDateTime()));
                    }
                               
                    return ((kendo.toString(new Date(fromDate)) >= kendo.toString(new Date(SFrom)) &&
                        kendo.toString(new Date(fromDate)) <= kendo.toString(new Date(STo)))
                         ||
                        (kendo.toString(new Date(toDate)) >= kendo.toString(new Date(SFrom)) &&
                        kendo.toString(new Date(toDate)) <= kendo.toString(new Date(STo)))
                        ||
                        (kendo.toString(new Date(fromDate)) <= kendo.toString(new Date(SFrom)) &&
                        kendo.toString(new Date(fromDate)) <= kendo.toString(new Date(STo)) &&
                        kendo.toString(new Date(toDate)) >= kendo.toString(new Date(SFrom)) &&
                        kendo.toString(new Date(toDate)) >= kendo.toString(new Date(STo)))

                    );
                }
            });
            if (berthlength.length != 0) {
                berth = false;                
                count++;
            }
                    
          
            var DryDockLengthList = self.DryDockScheduleList();
            var loa = true;
            var totallength = 0;

            $.each(DryDockLengthList, function (key, val1) {                                
                if (val1.ScheduleFromDate() != "Invalid date" && val1.ScheduleToDate() != "Invalid date") {
                    if ((kendo.toString(new Date(fromDate)) >= kendo.toString(new Date(val1.ScheduleFromDate())) &&
                                        kendo.toString(new Date(fromDate)) <= kendo.toString(new Date(val1.ScheduleToDate())))
                                         ||
                                        (kendo.toString(new Date(toDate)) >= kendo.toString(new Date(val1.ScheduleFromDate())) &&
                                        kendo.toString(new Date(toDate)) <= kendo.toString(new Date(val1.ScheduleToDate())))
                                        ||
                                        (kendo.toString(new Date(fromDate)) <= kendo.toString(new Date(val1.ScheduleFromDate())) &&
                                        kendo.toString(new Date(fromDate)) <= kendo.toString(new Date(val1.ScheduleToDate())) &&
                                        kendo.toString(new Date(toDate)) >= kendo.toString(new Date(val1.ScheduleFromDate())) &&
                                        kendo.toString(new Date(toDate)) >= kendo.toString(new Date(val1.ScheduleToDate())))) {                        
                        loa = false;
                        totallength = val1.LengthOverallInM() + totallength;
                    }
                }
            });

            
            var berthLength = 0;
            var dockcode = self.DryDockScheduleData().BerthCode();
            var quaycode = self.DryDockScheduleData().QuayCode();
            var portcode = self.DryDockScheduleData().PortCode();
            
            if (dockcode != "" || dockcode != undefined) {

                var berth = ko.utils.arrayFilter(self.GetDockList(), function (item) {
                    return item.PortCode() == portcode && item.QuayCode() == quaycode && item.BerthCode() == dockcode;
                });

                berthLength = parseFloat(berth[0].Lengthm());
            }

            if (count != 0) {                
                if (berthLength > totallength && parseFloat(model.LengthOverallInM()) <= (berthLength - totallength)) {

                    $("#MovementModel").modal('hide');
                    model.ScheduleFromDate(kendo.toString(new Date(fromDate), 'yyyy-MM-dd HH:mm'));
                    model.ScheduleToDate(kendo.toString(new Date(toDate), 'yyyy-MM-dd HH:mm'));
                    model.DockPortCode(self.DryDockScheduleData().PortCode());
                    model.DockBerthCode(self.DryDockScheduleData().BerthCode());
                    model.DockQuayCode(self.DryDockScheduleData().QuayCode());


                    self.viewModelHelper.apiPost('api/ScheduleDryDock', ko.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Dock is Scheduled Successfully", "Dry Dock Scheduler");
                        self.LoadPendingVesselsForDrydock();
                        $('#GridName').data('kendoGrid').dataSource.data(self.pendingVesselList());
                        $('#GridName').data('kendoGrid').refresh();
                        self.GetBerthScheduledData();
                    });

                }
                else {
                    toastr.warning("Vessel cannot be accommodated as the Berth does not have enough space left between dates.", "Dry Dock Scheduler");
                    return false;
                }
            }
            else {
                $("#MovementModel").modal('hide');
                model.ScheduleFromDate(kendo.toString(new Date(fromDate), 'yyyy-MM-dd HH:mm'));
                model.ScheduleToDate(kendo.toString(new Date(toDate), 'yyyy-MM-dd HH:mm'));
                model.DockPortCode(self.DryDockScheduleData().PortCode());
                model.DockBerthCode(self.DryDockScheduleData().BerthCode());
                model.DockQuayCode(self.DryDockScheduleData().QuayCode());


                self.viewModelHelper.apiPost('api/ScheduleDryDock', ko.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Dock is Scheduled Successfully", "Dry Dock Scheduler");
                    self.LoadPendingVesselsForDrydock();
                    $('#GridName').data('kendoGrid').dataSource.data(self.pendingVesselList());
                    $('#GridName').data('kendoGrid').refresh();
                    self.GetBerthScheduledData();
                });
            }          
            
        }

        self.SaveScheduleDock = function (model) {
             
            
            var scheduleDocks = ko.utils.arrayFilter(self.DryDockScheduleList(), function (item) {
                return item.ScheduleStatus() == "PLND";
            });

            self.viewModelHelper.apiPost('api/ConfirmedScheduleDryDocks', ko.toJSON(scheduleDocks), function Message(data) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("Scheduled Dock are Confirmed Successfully", "Dry Dock Scheduler");
                self.LoadPendingVesselsForDrydock();
                $('#GridName').data('kendoGrid').dataSource.data(self.pendingVesselList());
                $('#GridName').data('kendoGrid').refresh();
                
                self.GetBerthScheduledData();
               
            });
            self.LoadPendingVesselsForDrydock();
        }

        self.CancelScheduleDock = function () {
            window.location.href = "/Welcome";
        }

        debugger;
        self.Unplanned = function (data) {

            var portcode = self.DryDockScheduleData().PortCode();

            data.DockPortCode(self.DryDockScheduleData().PortCode());

            self.viewModelHelper.apiPost('api/UnPlannedScheduleDryDock', ko.toJSON(data), function Message(data) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("Dock is Unplanned Successfully", "Dry Dock Scheduler");
                self.LoadPendingVesselsForDrydock();
                $('#GridName').data('kendoGrid').dataSource.data(self.pendingVesselList());
                $('#GridName').data('kendoGrid').refresh();

                
                
                self.GetBerthScheduledData();
               
            });
            self.LoadPendingVesselsForDrydock();
        }

        self.GetBerthScheduledData = function () {
            $(".k-window-title").text('Hi');
            self.DryDockScheduleList.removeAll();
            var dockcode = self.DryDockScheduleData().BerthCode();
            var quaycode = self.DryDockScheduleData().QuayCode();

            
            if (dockcode == "" || dockcode == undefined) {
                toastr.warning("Please Select Dock", "Dry Dock Scheduler");
                $("#ddldock").focus();
                self.IsScheduleEnable(false);
                $(".btn").removeClass("btn btn-xs blue").addClass("btn btn-xs default");
            }
            else {
                self.IsScheduleEnable(true);
                $(".btn").removeClass("btn btn-xs default").addClass("btn btn-xs blue");
            }

            if (dockcode != '') {
                self.viewModelHelper.apiGet('api/ScheduledVesselForDryDock/' + quaycode + '/' + dockcode, null,
            
                 function (result) {                     
                     self.DryDockScheduleList(ko.utils.arrayMap(result, function (item) {
                         return new IPMSRoot.ScheduleModel(item);
                     }));

                 }, null, null, false);
            }
            self.NewData = ko.observableArray([]);
            if (self.DryDockScheduleList().length > 0) {
            var data;
            $.each(self.DryDockScheduleList(), function (index, item) {
                if (item.ToolTip() != null) {                    
                    if (item.EnteredDockDateTime() != "") {
                        item.start(item.EnteredDockDateTime());
                        item.end(item.ScheduleToDate());
                    }
                    if (item.EnteredDockDateTime() != "" && item.LeftDockDateTime() != "") {
                        item.start(item.EnteredDockDateTime());
                        item.end(item.LeftDockDateTime());
                    }
                    if (item.ExtensionDateTime() != "") {
                        item.end(item.ExtensionDateTime());
                    }
                    data = [{ id: item.id(), start: item.start(), end: item.end(), title: item.title() + '  ' + item.VesselName(), status: item.ScheduleStatus(), VesselName: item.VesselName(), ScheduleToDate: item.ScheduleToDate(), ScheduleFromDate: item.ScheduleFromDate(), DockBerthCode: item.BerthName(), IMONo: item.IMONo(), ReqFromDate: item.ReqFromDate(), ReqToDate: item.ReqToDate(), tooltipData: item.ToolTip() }];
                    
                    self.NewData.push(data[0]);
                    data = "";
                }
            });



            $("#scheduler1").kendoScheduler({
                date: new Date(),
                eventTemplate: $("#event-template").html(),
                edit: function (e) {
                    $(".k-window-title").text('Un Plan vessel');
                    if (!(e.event.status === "CNFR" || e.event.status === "PLND")) {
                        e.preventDefault();
                    }
                   
                },
              
                editable: {
                   
                    template: $("#editor").html(),
                    destroy: false,
                },
                views: [
                      { type: "month", selected: false, title: '1234' }
                ],
                dataSource: self.NewData(),
                save: function (e) {
                    if (e.container.find("[id=Planned]").is(':checked')) {
                        self.DryDockScheduleData().SuppDryDockID(e.event.id);
                        self.DryDockScheduleData().ScheduleStatus(e.event.status);
                        self.Unplanned(self.DryDockScheduleData());
                      
                    }
                }
                 

            });
                var scheduler = $("#scheduler1").data("kendoScheduler");
                var options = scheduler.options;
                options.views = [
                          { type: "month", selected: true }

                ];
                scheduler.destroy();
                $("#scheduler1").empty().kendoScheduler(options);

                $(".k-view-month").attr("data-name", "month").find("a").hide();


            }
            else {


                $("#scheduler1").kendoScheduler({
                    date: new Date(),
                    eventTemplate: $("#event-template").html(),
                    edit: function (e) {
                        $(".k-window-title").text('Un Plan vessel');
                        if (!(e.event.status === "CNFR" || e.event.status === "PLND")) {
                            e.preventDefault();
                            
                        }
                        
                    },
                   
                    editable: {
                        template: $("#editor").html(),
                        destroy: false,
                    },
                    views: [
                          { type: "month", selected: false }
                    ],
                    dataSource: []
                    
        
                });
                var scheduler = $("#scheduler1").data("kendoScheduler");
                var options = scheduler.options;
                scheduler.destroy();
                options.views = [
                       { type: "month", selected: true }
                ];

                $("#scheduler1").empty().kendoScheduler(options);

            $(".k-view-month").attr("data-name", "month").find("a").hide();

                $(".k-view-month").attr("data-name", "month").find("a").hide();
            }

        }
       
        self.Initialize = function () {

            self.DryDockScheduleData(new IPMSRoot.DryDockSchedulerModel());
            self.LoadDockList();
            self.LoadPendingVesselsForDrydock();

            var d1 = new Date();
            d2 = new Date(d1);
            d2.setMinutes(d1.getMinutes() + 30);

            $("#ScheduleTODate").kendoDateTimePicker({
                min: d2,
                format: "yyyy-MM-dd HH:mm",
                timeFormat: "HH:mm",
                 month: { empty: '<span class=k-state-disabled>#= data.value #</span>' }
            });

            self.NewData = ko.observableArray([]);
            var data;
            $.each(self.DryDockScheduleList(), function (index, item) {
                
                data = [{ id: item.id(), start: item.start(), end: item.end(), title: item.title() + '  ' + item.VesselName(), status: item.ScheduleStatus(), VesselName: item.VesselName(), ScheduleToDate: item.ScheduleToDate(), ScheduleFromDate: item.ScheduleFromDate(), DockBerthCode: item.DockBerthCode(), IMONo: item.IMONo(), ReqFromDate: item.ReqFromDate(), ReqToDate: item.ReqToDate(), tooltipData: item.ToolTip() }];
                    self.NewData.push(data[0]);
                    data = "";
                
            });
            


            $("#scheduler1").kendoScheduler({
                
                date: new Date(),
                eventTemplate: $("#event-template").html(),
                edit: function (e) {
                    $(".k-window-title").text('Un Plan vessel');
                    if (!(e.event.status === "CNFR" || e.event.status === "PLND")) {
                         e.preventDefault();
                    }
                },
                
                editable: {
                    title:'h123',
                    template: $("#editor").html(),
                    destroy: false,
                },
                views: [
                      { type: "month", selected: false }
                ]
                , dataSource: []
                
               

            });
            var scheduler = $("#scheduler1").data("kendoScheduler");
            var options = scheduler.options;
            scheduler.destroy();
            options.views = [
                   { type:  "month",selected: true }
            ];

            $("#scheduler1").empty().kendoScheduler(options);

            $(".k-view-month").attr("data-name", "month").find("a").hide();
            

        }
        $('[data-toggle="tooltip"]').tooltip({
            'placement': 'top'
        });
        $('[data-toggle="popover"]').popover({
            trigger: 'hover',
            'placement': 'top'
        });

        $('#userNameField').tooltip({
            'show': true,
            'placement': 'bottom',
            'title': "Please remember to..."
        });

        $('#userNameField').tooltip('show');
        var callbacks = $.Callbacks();
        
      
        $("#scheduler1").click(function () {            
            callbacks.add($("#scheduler1").dblclick());
        });

      
       
        self.Initialize();

        function scheduler_change(e) {
        
           
        }

        
    }
    IPMSRoot.DryDockSchedulerViewModel = DryDockSchedulerViewModel;
}(window.IPMSROOT));


function onChange() {
    
    $("#ScheduleTODate").val('');
    var d1 = new Date(this.value());
    d2 = new Date(d1);
    d2.setMinutes(d1.getMinutes() + 30);

    $("#ScheduleTODate").kendoDateTimePicker({
        format: "yyyy-MM-dd HH:mm",
        timeFormat: "HH:mm",
        min: d2,
        month: { empty: '<span class=k-state-disabled>#= data.value #</span>' }
    });
}