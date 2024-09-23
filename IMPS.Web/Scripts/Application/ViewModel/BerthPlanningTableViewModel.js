(function (IPMSRoot) {
    var isView = 0;
    var BerthPlanningTableViewModel = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.berthplanningtableModel = ko.observable(new IPMSRoot.BerthPlanningTableModel());
        self.berthplanningtableReferenceData = ko.observable();
        self.BerthPlanningTableList = ko.observableArray();
        self.QuayCode = ko.observable();
        self.QuayName = ko.observable();
        self.BerthCode = ko.observable();
        self.BerthName = ko.observable();
        self.SubCatCode = ko.observable();
        self.SubCatName = ko.observable();
        self.berthValues = ko.observableArray();
        self.NoofDaysChecked = ko.observable();
        self.Configurations = ko.observableArray();
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {

            self.LoadPortConfiguration();
            self.berthplanningtableModel(new IPMSROOT.BerthPlanningTableModel());
            self.LoadReferenceData();
            self.LoadVCMTableData();
        }

        self.LoadReferenceData = function () {
            self.viewModelHelper.apiGet('api/BerthPlanningTableReferenceData', null,
                    function (refdata) {
                        //console.log(refdata);
                        self.berthplanningtableReferenceData(new IPMSRoot.ReferenceData(refdata));
                    }, null, null, false);
        }

        self.LoadBerths = function (event) {

            self.berthValues("");
            self.berthplanningtableReferenceData().BerthCode("");
            var PortCode = self.berthplanningtableModel().PortCode();
            var QuayCode = self.berthplanningtableReferenceData().QuayCode();
            //console.log('QuayCode', QuayCode);
            if (QuayCode == undefined || QuayCode == "") {
                //  self.berthValues({ BerthCode: 0, BerthName: '' });
                self.berthplanningtableModel().BerthCode("");
                self.berthValues("");
                //  self.isQuayChanged(false);
            }
            else {
                //self.isQuayChanged(true);
                self.viewModelHelper.apiGet('api/BollardQuayBerths/' + PortCode + '/' + QuayCode,
              null,
             function (result) {
                 ko.mapping.fromJS(result, {}, self.berthValues);

             });

            }

        }

        self.PortSelect = function (e) {
            var dataItem = this.dataItem(e.item.index());
            self.berthplanningtableModel().PortCode(dataItem.PortCode);
        }

        self.LoadPortConfiguration = function () {
            self.viewModelHelper.apiGet('api/GetBerthPlanningConfiguration', null,
           function (result) {
               //console.log('congi', result);
               self.Configurations(ko.utils.arrayMap(result, function (item) {
                   return new IPMSRoot.ConfigurationModel(item);
               }));
           },
            null, null, false);


            $.each(self.Configurations(), function (index, item) {
                if (item.ConfigName() == "DAYS")
                    self.NoofDaysChecked(item.ConfigValue());


            });


        }

        self.LoadVCMTableData = function () {
            self.berthplanningtableModel().SelectedETA(moment(new Date()).format('YYYY-MM-DD'));
            var ETA = self.berthplanningtableModel().SelectedETA();
            var CurrentDate = new Date();
            var nodays = self.NoofDaysChecked();
            self.berthplanningtableModel().ToDate(moment(CurrentDate).add('days', nodays - 1).format('MM-DD-YYYY'));

            var ToDate = self.berthplanningtableModel().ToDate();

            var QuayCode = self.berthplanningtableReferenceData().QuayCode();
            var BerthCode = self.berthplanningtableModel().BerthCode();
            var VesselStatus = self.berthplanningtableReferenceData().VesselStatus();

            if (QuayCode == "")
                QuayCode = undefined;
            if (BerthCode == "")
                BerthCode = undefined;
            if (VesselStatus == "")
                VesselStatus = undefined;
            self.viewModelHelper.apiGet('api/GetVCMTableList/' + QuayCode + '/' + BerthCode + '/' + VesselStatus + '/' + kendo.toString(ETA, "MM-DD-YYYY") + '/' + ToDate,
                null,
                        function (result) {

                            //console.log('table', result);
                            self.BerthPlanningTableList(result);
                            if (result[0] != undefined) {
                                var resultset = JSON.stringify(result);
                                var col_list = { Result: resultset }
                            }
                            else {
                                var col_list = { Result: null }
                            }

                            // self.berthplanningtableModel().Result()
                            $.ajax({
                                url: 'SetDataBerth',
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                data: ko.mapping.toJSON(col_list),
                                success: function (data, textStatus, jqXHR) {
                                },
                                error: function (error) {
                                    //alert(error.status + " " + error.statusText);
                                }
                            });


                        }, null, null, false);


        }

        self.GetDataClick = function (model) {

            self.LoadVCMTableDataOnGetData();


        }

        self.LoadVCMTableDataOnGetData = function () {
            var ETA = self.berthplanningtableModel().SelectedETA();
            var ToDate = self.berthplanningtableModel().ToDate();
            var QuayCode = self.berthplanningtableReferenceData().QuayCode();
            var BerthCode = self.berthplanningtableModel().BerthCode();
            var VesselStatus = self.berthplanningtableReferenceData().VesselStatus();

            if (QuayCode == "")
                QuayCode = undefined;
            if (BerthCode == "")
                BerthCode = undefined;
            if (VesselStatus == "")
                VesselStatus = undefined;
            self.viewModelHelper.apiGet('api/GetVCMTableList/' + QuayCode + '/' + BerthCode + '/' + VesselStatus + '/' + ETA + '/' + ToDate,
                null,
                        function (result) {


                            //console.log('table', result);

                            self.BerthPlanningTableList(result);
                            if (result[0] != undefined) {
                                var resultset = JSON.stringify(result);
                                var col_list = { Result: resultset }
                            }
                            else {
                                var col_list = { Result: null }
                            }

                            // self.berthplanningtableModel().Result()
                            $.ajax({
                                url: 'SetDataBerth',
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                data: ko.mapping.toJSON(col_list),
                                success: function (data, textStatus, jqXHR) {
                                },
                                error: function (error) {
                                    //alert(error.status + " " + error.statusText);
                                }
                            });
                        }, null, null, false);
        }

        OnDateChange = function (e) {

            var SelectETA = e.SelectedETA();
            var ToDateP = new Date(Date.parse(e.SelectedETA()));
            var nodays = self.NoofDaysChecked();
            self.berthplanningtableModel().ToDate(moment(ToDateP).add('days', nodays - 1).format('MM-DD-YYYY'));
            self.berthplanningtableModel().SelectedETA(moment(e.SelectedETA()).format('YYYY-MM-DD'));
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();
    }
    IPMSRoot.BerthPlanningTableViewModel = BerthPlanningTableViewModel;

}(window.IPMSROOT));
