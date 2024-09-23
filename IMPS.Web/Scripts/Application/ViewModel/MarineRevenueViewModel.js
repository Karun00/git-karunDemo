(function (IPMSRoot) {
    var MarineRevenueViewModel = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSROOT.validationHelper();
        $('#spnTitle').html("Revenue Posting List");
        self.marineRevenueModel = ko.observable();
        self.MarineRevenueList = ko.observableArray();
        self.AgentAccountList = ko.observableArray([]);
        self.IsCodeEnable = ko.observable(true);
        self.agents = ko.observableArray([]);
        self.viewMode = ko.observable();
        self.isspanVCNSearchValid = ko.observable(false);
        self.isspanVesselSearchValid = ko.observable(false);
        self.revenuePostingModelGrid = ko.observable(new IPMSROOT.RevenuePostingModelGrid());
        self.BerthDueVisible = ko.observable(false);
        self.PortDueVisible = ko.observable(false);
        self.RefuseVisible = ko.observable(false);

        self.Initialize = function () {
            
            self.viewMode('List');
            self.LoadMarineRevenueList();
            self.marineRevenueModel(new IPMSROOT.MarineRevenueModel());
            self.revenuePostingModelGrid(new IPMSRoot.RevenuePostingModelGrid(undefined));
        }

        self.LoadMarineRevenueList = function () {
           
            var frmdate = moment(self.revenuePostingModelGrid().RevenueFrom()).format('YYYY-MM-DD');
            var todate = moment(self.revenuePostingModelGrid().RevenueTo()).format('YYYY-MM-DD');

            var vcnSearch = self.revenuePostingModelGrid().VCN();
            var vesselName = self.revenuePostingModelGrid().VesselName();

            if (vcnSearch == "") {
                vcnSearch = "All";
            }
            if (vesselName == "") {
                vesselName = "All";
            }

            self.viewModelHelper.apiGet('api/MarineRevenueDetails/' + vcnSearch + '/' + vesselName + '/' + frmdate + '/' + todate,
          null,
            function (result) {
                self.MarineRevenueList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.MarineRevenueModel(item);
                }));
            }, null, null, false);

           
            // self.viewModelHelper.apiGet('api/MarineRevenue/GetMarineRevenueList',
            //null,
            //  function (result) {
            //      self.MarineRevenueList(ko.utils.arrayMap(result, function (item) {

            //          return new IPMSRoot.MarineRevenueModel(item);
            //      }));
            //  }, null, null, false);
        }

        self.addMarineRevenue = function () {
            self.marineRevenueModel(new IPMSROOT.MarineRevenueModel());
            self.viewMode('Form');
            self.IsCodeEnable(true);
            $('#spnTitle').html("Add Revenue Posting ");
        }

        ChangeETADate = function () {
            

            var a1 = $("#ETA").val();

            if (self.marineRevenueModel().RevenueDetails().PortDuesDetails().length > 0) {
                $.each(self.marineRevenueModel().RevenueDetails().PortDuesDetails(), function (key, item) {
                    var dtlchk = item;
                    if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                        dtlchk.PostingDateTime(a1);
                        var dtOcupationToDate = new Date(Date.parse(moment(dtlchk.RecentlyPostedDate())));
                        var dtOcupationEndDate = new Date(Date.parse(moment(dtlchk.PostingDateTime())));
                        var diff = dtOcupationEndDate - dtOcupationToDate;
                        var msec = diff;
                        var hh = msec / 1000 / 60 / 60 / 24;
                        //  var hh1 = msec / 86400000;
                        dtlchk.DueHours(parseFloat(hh).toFixed(3));
                    }
                });

            }
        }

        ChangeETADatebertdue = function () {          
            var a1 = $("#ETA1").val();
           
            if (self.marineRevenueModel().RevenueDetails().BerthDuesDetails().length > 0) {
             
                $.each(self.marineRevenueModel().RevenueDetails().BerthDuesDetails(), function (key, item) {
                    var dtlchk = item;
                    if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                        dtlchk.PostingDateTime(a1);
                        var dtOcupationToDate = new Date(Date.parse(moment(dtlchk.RecentlyPostedDate())));
                        var dtOcupationEndDate = new Date(Date.parse(moment(dtlchk.PostingDateTime())));
                        var diff = dtOcupationEndDate - dtOcupationToDate;
                        var msec = diff;
                        var hh = msec / 1000 / 60 / 60 / 24;
                        dtlchk.DueHours(parseFloat(hh).toFixed(3));
                    }
                });

            }
        }

        ChangeETARefuseDate = function () {            
           var a1 = $("#ETARefuse").val();
           if (self.marineRevenueModel().RevenueDetails().RefuseRemovalDetails().length > 0) {              
               $.each(self.marineRevenueModel().RevenueDetails().RefuseRemovalDetails(), function (key, item) {
                    var dtlchk = item;
                    if (dtlchk != null && dtlchk.ISPOSTED() == 0) {                        
                        dtlchk.PostingDateTime(a1);
                        var dtOcupationToDate = new Date(Date.parse(moment(dtlchk.RecentlyPostedDate())));
                        var dtOcupationEndDate = new Date(Date.parse(moment(dtlchk.PostingDateTime())));
                        var diff = dtOcupationEndDate - dtOcupationToDate;
                        var msec = diff;
                        var hh = msec / 1000 / 60 / 60 / 24;
                        var val = parseFloat(hh).toFixed(2);
                        var substr = val.split('.');
                        var FinalValue;
                        if (parseFloat(substr[1]) == parseFloat(00)) {                          
                            FinalValue = Math.round(hh);                            
                        }
                        else if (parseFloat(substr[1]) > parseFloat(00))
                            FinalValue = Math.ceil(hh);                            
                        dtlchk.DueHours(FinalValue);
                    }
                });

            }
        }


        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
        calminmaxtodayberthdue = function (model) {
            var midt;
            var mxdt;
            var strmyDatePicker;
            var strday;
            var strmonth;
            var stryear;
            var strHour;
            var strMnt;

            var endmyDatePicker;
            var endday;
            var endmonth;
            var endyear;
            var endHour;
            var endMnt;

            if (self.marineRevenueModel().RevenueDetails().BerthDuesDetails().length > 0) {
                $.each(self.marineRevenueModel().RevenueDetails().BerthDuesDetails(), function (key, item) {
                    var dtlchk = item;
                    if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                        strmyDatePicker = kendo.parseDate(dtlchk.RecentlyPostedDate() ? moment(dtlchk.RecentlyPostedDate()).format('YYYY-MM-DD HH:mm') : moment(dtlchk.StartTime()).format('YYYY-MM-DD HH:mm'), "yyyy-MM-dd HH:mm");
                        strday = strmyDatePicker.getDate();
                        strmonth = strmyDatePicker.getMonth();
                        stryear = strmyDatePicker.getFullYear();
                        strHour = strmyDatePicker.getHours();
                        strMnt = strmyDatePicker.getMinutes()+1;
                        if (dtlchk.Endtime() == null) {
                            mxdt = new Date();
                        }
                        else {
                            endmyDatePicker = kendo.parseDate(moment(dtlchk.Endtime()).format('YYYY-MM-DD HH:mm'), "yyyy-MM-dd HH:mm");
                            endday = endmyDatePicker.getDate();
                            endmonth = endmyDatePicker.getMonth();
                            endyear = endmyDatePicker.getFullYear();
                            endHour = endmyDatePicker.getHours();
                            endMnt =endmyDatePicker.getMinutes();
                            mxdt = new Date(endyear, endmonth, endday, endHour, endMnt)
                        }
                    }
                });
                this.min(new Date(stryear, strmonth, strday, strHour, strMnt));
                //  this.min(new Date());
                this.max(mxdt);
            }
        }



        calminmaxtoday = function (model) {
            
            var midt;
            var mxdt;
            var strmyDatePicker;
            var strday;
            var strmonth;
            var stryear;
            var strHour;
            var strMnt;

            var endmyDatePicker;
            var endday;
            var endmonth;
            var endyear;
            var endHour;
            var endMnt;

            if (self.marineRevenueModel().RevenueDetails().PortDuesDetails().length > 0) {
                $.each(self.marineRevenueModel().RevenueDetails().PortDuesDetails(), function (key, item) {
                    var dtlchk = item;
                    if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                        
                        strmyDatePicker = kendo.parseDate(dtlchk.RecentlyPostedDate() ? moment(dtlchk.RecentlyPostedDate()).format('YYYY-MM-DD HH:mm') : moment(dtlchk.StartTime()).format('YYYY-MM-DD HH:mm'), "yyyy-MM-dd HH:mm");
                        strday = strmyDatePicker.getDate();
                        strmonth = strmyDatePicker.getMonth();
                        stryear = strmyDatePicker.getFullYear();
                        strHour = strmyDatePicker.getHours();
                        strMnt = strmyDatePicker.getMinutes()+1;//+1 added by divya on 8thDec2017
                        if (dtlchk.Endtime() == null) {
                            mxdt = new Date();
                        }
                        else {
                            endmyDatePicker = kendo.parseDate(moment(dtlchk.Endtime()).format('YYYY-MM-DD HH:mm'), "yyyy-MM-dd HH:mm");
                            endday = endmyDatePicker.getDate();
                            endmonth = endmyDatePicker.getMonth();
                            endyear = endmyDatePicker.getFullYear();
                            endHour = endmyDatePicker.getHours();
                            endMnt = endmyDatePicker.getMinutes();
                            mxdt = new Date(endyear, endmonth, endday, endHour, endMnt)
                        }
                    }
                });
                this.min(new Date(stryear, strmonth, strday, strHour, strMnt));
                //  this.min(new Date());
                this.max(mxdt);
            }
        }


        calminmaxtodayForRefuse = function (model) {
            
            var midt;
            var mxdt;
            var strmyDatePicker;
            var strday;
            var strmonth;
            var stryear;
            var strHour;
            var strMnt;

            var endmyDatePicker;
            var endday;
            var endmonth;
            var endyear;
            var endHour;
            var endMnt;

            if (self.marineRevenueModel().RevenueDetails().RefuseRemovalDetails().length > 0) {
               
                $.each(self.marineRevenueModel().RevenueDetails().RefuseRemovalDetails(), function (key, item) {
                    var dtlchk = item;
                    if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                    
                        strmyDatePicker = kendo.parseDate(dtlchk.RecentlyPostedDate() ? moment(dtlchk.RecentlyPostedDate()).format('YYYY-MM-DD HH:mm') : moment(dtlchk.StartTime()).format('YYYY-MM-DD HH:mm'), "yyyy-MM-dd HH:mm");
                        strday = strmyDatePicker.getDate();
                        strmonth = strmyDatePicker.getMonth();
                        stryear = strmyDatePicker.getFullYear();
                        strHour = strmyDatePicker.getHours();
                        strMnt = strmyDatePicker.getMinutes()+1;
                        if (dtlchk.Endtime() == null) {
                            mxdt = new Date();
                        }
                        else {
                            endmyDatePicker = kendo.parseDate(moment(dtlchk.Endtime()).format('YYYY-MM-DD HH:mm'), "yyyy-MM-dd HH:mm");
                            endday = endmyDatePicker.getDate();
                            endmonth = endmyDatePicker.getMonth();
                            endyear = endmyDatePicker.getFullYear();
                            endHour = endmyDatePicker.getHours();
                            endMnt = endmyDatePicker.getMinutes();
                            mxdt = new Date(endyear, endmonth, endday, endHour, endMnt)
                        }
                    }
                });
                this.min(new Date(stryear, strmonth, strday, strHour, strMnt));
                //  this.min(new Date());
                this.max(mxdt);
            }
        }


        portduescheckAll = function (ctrl, itmname, dtllist) {
            
        checkboxes = document.getElementsByName(itmname);
        if (ctrl.checked) {
            for (var i = 0, n = checkboxes.length; i < n; i++) {
                if (checkboxes[i].style.display != 'none') {
                    checkboxes[i].checked = ctrl.checked;
                    checkboxes[i].value = ctrl.checked;
                    //checkboxes[i].prop('checked', true);
                }
            }
        }
        else {
            for (var i = 0, n = checkboxes.length; i < n; i++) {
                checkboxes[i].checked = ctrl.checked;
                checkboxes[i].value = ctrl.checked;
                // checkboxes[i].prop('checked', false);
            }
        }

        switch (dtllist) {
            case 'A': {
                if (self.marineRevenueModel().RevenueDetails().PortDuesDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().PortDuesDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'B': {
                if (self.marineRevenueModel().RevenueDetails().ArrivalDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().ArrivalDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'C': {
                if (self.marineRevenueModel().RevenueDetails().ShiftingDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().ShiftingDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'D': {
                if (self.marineRevenueModel().RevenueDetails().WarpingDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().WarpingDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'E': {
                if (self.marineRevenueModel().RevenueDetails().SailingDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().SailingDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'F': {
                if (self.marineRevenueModel().RevenueDetails().SupplimantoryDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().SupplimantoryDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'G': {
                if (self.marineRevenueModel().RevenueDetails().DryDockDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().DryDockDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'H': {
                if (self.marineRevenueModel().RevenueDetails().DryDock12HrsDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().DryDock12HrsDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'I': {
                if (self.marineRevenueModel().RevenueDetails().DrydockMislaniousDetails().length > 0) {
                    $.each(self.marineRevenueModel().RevenueDetails().DrydockMislaniousDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }

            case 'J': {
                if (self.marineRevenueModel().RevenueDetails().BerthDuesDetails().length > 0) {
                   
                    $.each(self.marineRevenueModel().RevenueDetails().BerthDuesDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }
            case 'K': {
                if (self.marineRevenueModel().RevenueDetails().RefuseRemovalDetails().length > 0) {

                    $.each(self.marineRevenueModel().RevenueDetails().RefuseRemovalDetails(), function (key, item) {

                        var dtlchk = item;
                        if (dtlchk != null && dtlchk.ISPOSTED() == 0) {
                            dtlchk.ischecked(ctrl.checked);
                        }
                    });
                }
                break;
            }

        }
    }





        self.VesselSelect = function (e) {
            
        var selecteddataItem = this.dataItem(e.item.index());
        self.marineRevenueModel().VesselData(selecteddataItem);
        
        self.marineRevenueModel().RevenueDetails().VCN(selecteddataItem.vcn);


        self.viewModelHelper.apiPost('api/MarineRevenue/GetVCNAgents?vcn=' + selecteddataItem.vcn, null,
        function (result) {
            ko.mapping.fromJS(result, {}, self.agents);
        }, null, null, false);

        self.viewModelHelper.apiPost('api/MarineRevenue/GetRevenueSectionDetails?vcn=' + selecteddataItem.vcn,
        null,
        function (result) {
            
            if (result.BerthDuesDetails.length==0)
                self.BerthDueVisible(false);
            else
                self.BerthDueVisible(true);
            if (result.PortDuesDetails.length == 0)
                self.PortDueVisible(false);
            else
                self.PortDueVisible(true);
            if (result.RefuseRemovalDetails.length == 0)
                self.RefuseVisible(false);
            else
                self.RefuseVisible(true);
            self.marineRevenueModel().RevenueDetails(new IPMSRoot.RevenuePostingData(result));
           
            
            self.marineRevenueModel().RevenueDetails().VCN(selecteddataItem.vcn);

        }, null, null, false);
    }

    self.LoadAgentAccount = function (data) {

        self.viewModelHelper.apiGet('api/MarineRevenue/GetAgentAccountDetails', { agentID: data.AgentID() },
              function (result) {
                  ko.mapping.fromJS(result, {}, self.AgentAccountList);
              }, null, null, false);
    }

    self.PostMarineRevenue = function (data) {
       
        data.validationEnabled(true);
        self.MarineRevenueValidation = ko.observable(data);
        self.MarineRevenueValidation().errors = ko.validation.group(self.MarineRevenueValidation());
        var errors = self.MarineRevenueValidation().errors().length;

        if (errors == 0) {
            var Ispost = false;
            //     var chkAll = new Array("portduesitem", "ArrivalduesAll", "shiftingAllchk", "warpingAllchk", "sailingAllchk");
            var chkitem = new Array("portduesitem", "berthduesitem","refuseremovalitem", "Arrivalduesitem", "shiftingitemchk", "warpingitemchk", "sailingitemchk", "drydockmainitemchk", "drydockDailyitemchk", "supplitemchk", "DrydocMisitemchk");

            for (i = 0; i < chkitem.length; i++) {
                itemcheckboxes = document.getElementsByName(chkitem[i]);
                for (var j = 0; j < itemcheckboxes.length; j++) {
                    if (itemcheckboxes[j].checked && itemcheckboxes[j].style.display != 'none') {
                        Ispost = true;
                    }
                }
            }

            if (Ispost) {
            //    
                //data.RevenueDetails().VCN(data.vcn());
                data.RevenueDetails().AgentID(data.AgentID());
                data.RevenueDetails().RegisteredName($('#marineAgentID option:selected').text());
                data.RevenueDetails().AgentAccountID(data.AgentAccountID());
                data.RevenueDetails().AccountNo($('#marineAccountID option:selected').text());

                self.viewModelHelper.apiPost('api/MarineRevenue/PostMarineRevenueDetails',
                         ko.mapping.toJSON(data.RevenueDetails()),
             function (result) {
                 toastr.options.closeButton = true;
                 toastr.options.positionClass = "toast-top-right";
                 toastr.success("Marine Revenue details submitted successfully.", "Marine Revenue");
                 self.LoadMarineRevenueList();
                 self.viewMode('List');
             }, null, null, false);

            }
            else {
                toastr.warning("Please Select atleast One Item.", "Marine Revenue");
            }


        }
        else {
            self.MarineRevenueValidation().errors.showAllMessages();
            toastr.warning("You have some form errors. Please check below", "Marine Revenue");
            return;
        }
    }

    self.Cancel = function () {
        self.viewMode('List');
        self.LoadMarineRevenueList();
        $('#spnTitle').html("Revenue PostIng List");
    }

    self.viewMarineRevenueDetails = function (data) {
        
        self.viewMode('Form');
        self.IsCodeEnable(false);
        $('#spnTitle').html("View Revenue Posting ");
        self.marineRevenueModel().vcn(data.vcn());
        var agnt = 1;
        var agntacc = 1;
        var agvcn = 0;
        var cmpdtls = 0;
        //self.viewModelHelper.apiPost('api/MarineRevenue/GetVCNAgents?vcn=' + data.vcn(),
        //    null,
        //      function (result) {
        //          ko.mapping.fromJS(result, {}, self.agents);
        //          self.marineRevenueModel().AgentID(data.AgentID());
        //      }, null, function callbackloder(result) {
        //          //firstSave = true;
        //          agnt = 1;
        //          if (agnt == 1 && agntacc == 1 && agvcn == 1 && cmpdtls == 1) {
        //              self.viewModelHelper.isLoading(false);
        //          }
        //      }
        //      , false);


        //self.viewModelHelper.apiGet('api/MarineRevenue/GetAgentAccountDetails', { agentID: data.AgentID() },
        //        function (result) {
        //            ko.mapping.fromJS(result, {}, self.AgentAccountList);
        //            self.marineRevenueModel().AgentAccountID(data.AgentAccountID());
        //        }, null, function callbackloder(result) {
        //            agntacc = 1;
        //            if (agnt == 1 && agntacc == 1 && agvcn == 1 && cmpdtls == 1) {
        //                self.viewModelHelper.isLoading(false);
        //            }
        //        }, false);


        self.viewModelHelper.apiGet('api/MarineRevenue/GetVcnViewDetails', { VCN: data.vcn() },
               function (result) {
                   self.marineRevenueModel().VesselData(result[0]);
               }, null, function callbackloder(result) {
                   agvcn = 1;
                   if (agnt == 1 && agntacc == 1 && agvcn == 1 && cmpdtls == 1) {
                       self.viewModelHelper.isLoading(false);
                   }
               }, false);



        self.viewModelHelper.apiGet('api/MarineRevenue/GetRevenueSectionDetailsView', { RevenuePostingId: data.RevenuePostingID(), Agentid: data.AgentID(), Accountid: data.AgentAccountID() },
            
              function (result) {
                  
                  self.marineRevenueModel().RevenueDetails(new IPMSRoot.RevenuePostingData(result));
                  
              }, null, function callbackloder(result) {
                  cmpdtls = 1;
                  if (agnt == 1 && agntacc == 1 && agvcn == 1 && cmpdtls == 1) {
                      self.viewModelHelper.isLoading(false);
                  }
                  
              }, false);

    }

    self.GetDataClick = function (model) {
        
        var frmdate = moment(self.revenuePostingModelGrid().RevenueFrom()).format('YYYY-MM-DD');
        var todate = moment(self.revenuePostingModelGrid().RevenueTo()).format('YYYY-MM-DD');

        //var frmdate = self.revenuePostingModelGrid().RevenueFrom();
        //var todate = self.revenuePostingModelGrid().RevenueTo();
        var isnoError = true;
        var vcnSearch = self.revenuePostingModelGrid().VCN();
        var vesselName = self.revenuePostingModelGrid().VesselName();
        var vcnSearchSelected = model.VCNSERCH();

        if (vcnSearch == "") {
            vcnSearch = "All";
            $("#spanVCNSearchValid").text('');
            self.isspanVCNSearchValid(false);
        }
        else {

            if (vcnSearchSelected != vcnSearch) {
                isnoError = false;
                $("#spanVCNSearchValid").text('Please select valid VCN');
                self.isspanVCNSearchValid(true);
            }

             
        }
          
        if (vesselName == "") {
            vesselName = "All";
            $("#spanVesselSearchValid").text('');
            self.isspanVesselSearchValid(false);
        }
        else {

            if (model.VESSELSERCH() != vesselName) {
                isnoError = false; 
                $("#spanVesselSearchValid").text('Please select valid Vessel Name/IMO No.');
                self.isspanVesselSearchValid(true);
            }


        }
        if (isnoError) {

            self.viewModelHelper.apiGet('api/MarineRevenueDetails/' + vcnSearch + '/' + vesselName + '/' + frmdate + '/' + todate,
          null,
            function (result) {
                
                self.MarineRevenueList(ko.utils.arrayMap(result, function (item) {
                    
                    return new IPMSRoot.MarineRevenueModel(item);
                }));
            }, null, null, false);
        }
    }

    SearchRevenueCal = function () {
        this.min($("#RevenueFromDate").val());
        var myDatePicker = new Date($("#RevenueFromDate").val());
        var day = myDatePicker.getDate() + 30;
        var month = myDatePicker.getMonth();
        var year = myDatePicker.getFullYear();
        this.max(new Date(year, month, day));

    }

    SearchValidDate = function (data, event) {
        self.revenuePostingModelGrid().RevenueTo(self.revenuePostingModelGrid().RevenueFrom());
    }


    self.ClearFilter = function () {
        self.revenuePostingModelGrid().VCN('');
        self.revenuePostingModelGrid().VesselName('');
        $("#spanVCNSearchValid").text('');
        $("#spanVesselSearchValid").text('');
        self.isspanVCNSearchValid(false);
        self.isspanVesselSearchValid(false);
        var todaydate = new Date();
        var todate = new Date(todaydate);
        var fromdate = new Date(todaydate);
        todate.setDate(todaydate.getDate());
        fromdate.setDate(fromdate.getDate() - 30);


        self.revenuePostingModelGrid().RevenueFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
        self.revenuePostingModelGrid().RevenueTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");

        self.LoadMarineRevenueList();
    }

        
    //-------------------------------------------------------
    SerchVesselBackSpace = function (e) {
        self.revenuePostingModelGrid().VesselName('');
    }
    SerchVCNBackSpace = function (e) {
        self.revenuePostingModelGrid().VCN('');
    }

    VCNonblur = function (e) {


        var vcnblur = $("#VCNName").val();
        self.revenuePostingModelGrid().VCN(vcnblur);



    }
    Vesselonblur = function (e) {
        var vesselblur = $("#VesselName1").val();
        self.revenuePostingModelGrid().VesselName(vesselblur);

    }
    //SerchVCNBackSpaceNumValid = function (e) {
    //    self.arrivalNotificationModelSearchGrid().VCNSERCH('');
    //}

    SerchVCNBackSpaceNumValid = function (evt) {

        self.revenuePostingModelGrid().VCN('');

        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;

        return true;

        //if (window.event) // IE
        //    keynum = event.keyCode;
        //else if (event.which) // Netscape/Firefox/Opera
        //    keynum = event.which;
        //keychar = String.fromCharCode(keynum);
        //charcheck = /[0-9\b]/g;
        //// var result = keychar.match(charcheck)
        //return ((keychar.match(charcheck) == null) ? false : true);
    }


    self.VCNSelectSearch = function (e) {
        var selecteddataItem = this.dataItem(e.item.index());
        self.revenuePostingModelGrid().VCNSERCH(selecteddataItem.vcn);
        self.isspanVCNSearchValid(false);
        $("#spanVCNSearchValid").text('');
    }

    self.VesselSelectSearch = function (e) {
        var selecteddataItem = this.dataItem(e.item.index());
        self.revenuePostingModelGrid().VESSELSERCH(selecteddataItem.VesselName);
        self.isspanVesselSearchValid(false);
        $("#spanVesselSearchValid").text('');
           
    }
        

    //-------------------------------------------------------

    self.Initialize();
}
    IPMSRoot.MarineRevenueViewModel = MarineRevenueViewModel;
}(window.IPMSROOT));
