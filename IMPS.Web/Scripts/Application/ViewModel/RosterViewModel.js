(function (IPMSRoot) {
    var isView = 0;
    var RosterMasterViewModel = function () {
        var self = this;
        $('#spnTitle').html("Roster");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.rosterModel = ko.observable(new IPMSROOT.RosterModel());
        self.rostermasterList = ko.observableArray([]);
        self.DesignationsList = ko.observableArray([]);
        self.Shiftslist = ko.observableArray([]);
        self.rostermasterModel = ko.observable();
        self.rosterReferenceData = ko.observable();
        self.viewMode = ko.observable();
        self.hasEnable = ko.observable(true);

        self.Initialize = function () {
            self.rostermasterModel(new IPMSROOT.RosterModel());
            self.LoadRosterReferencedata();
            ko.validation.group(self.rostermasterModel()).showAllMessages(false);
        }

        //self.AddNewRowtotable = function (RosterGroup) {
        //    self.rostermasterModel().RosterGroup.push(new IPMSROOT.AddRosterGroupTable());
        //}

        self.LoadRosterReferencedata = function () {
            self.viewModelHelper.apiGet('api/Roster/GetReferenceData', null,
                  function (result) {
                      self.rosterReferenceData(new IPMSRoot.RosterReferenceData(result));
                  }, null, null, false);
        }

        self.LoadRosterDetails = function (data) {

            var date = new Date();
            var month = date.getMonth();
            var year = date.getFullYear();

            var success = true;

            if (data.Year() < year) {
                success = false;
            }
            else if (year == data.Year() && data.month() < month) {
                success = false;
            }


            self.rostermasterModelValidation = ko.observable(data);
            self.rostermasterModelValidation().errors = ko.validation.group(self.rostermasterModelValidation());
            var rostermasterModelValidationerrors = self.rostermasterModelValidation().errors().length;

            if (rostermasterModelValidationerrors == 0) {
                //
                var RosterData = new IPMSROOT.RosterData();
                RosterData.Designation(data.Designation());
                RosterData.month(data.month());
                RosterData.Year(data.Year());
                var Savebtn = 0;
                self.viewModelHelper.apiPost('api/RosterDetails', ko.toJSON(RosterData),
                            function (result) {
                                if (result.length > 0) {
                                    self.hasEnable(false);
                                    self.rostermasterModel().shouldShowSave(true);
                                    self.rostermasterModel().shouldShowUpdate(false);
                                    self.rostermasterModel().shouldShowReset(true);
                                    self.rostermasterModel().shouldShowResetCan(true);
                                    //$("#cancel").html('Reset');
                                    self.rostermasterModel().RosterAloocationLists([]);
                                    ko.utils.arrayMap(result, function (data) {
                                        var employee = new IPMSROOT.RosterAloocationList();
                                        employee.WeekNo(data.WeekNo);
                                        employee.ResourceGroupID(data.ResourceGroupID);
                                        employee.ResourceGroupName(data.ResourceGroupName);
                                        employee.Monday(data.Monday);
                                        employee.Tuesday(data.Tuesday);
                                        employee.Wednesday(data.Wednesday);
                                        employee.Thursday(data.Thursday);
                                        employee.Friday(data.Friday);
                                        employee.Saturday(data.Saturday);
                                        employee.Sunday(data.Sunday);
                                        employee.Year(data.Year);
                                        employee.DayFromTo(data.RecordStatus);
                                        
                                        //var d = new Date();
                                        //var n = d.getFullYear();
                                        //if (RosterData.Year() >= n) {
                                        if (success) {

                                            if (Savebtn == 0) {
                                                if (data.Monday > 0 || data.Tuesday > 0 || data.Wednesday > 0 || data.Thursday > 0 || data.Friday > 0 || data.Saturday > 0 || data.Sunday > 0) {
                                                    Savebtn = 1;
                                                }
                                            }
                                            else {
                                                self.rostermasterModel().shouldShowSave(false);
                                                self.rostermasterModel().shouldShowUpdate(true);
                                            }
                                        }
                                        else {
                                            self.rostermasterModel().shouldShowSave(false);
                                            self.rostermasterModel().shouldShowUpdate(false);
                                            self.rostermasterModel().shouldShowReset(false);
                                        }
                                        self.rostermasterModel().RosterAloocationLists.push(employee);
                                    });


                                }
                                else {
                                    //$("#cancel").html('Reset');
                                    //self.rostermasterModel().shouldShowResetCan(true);
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.error("No records found", "Roster");
                                }
                            });
            }
            else {
                self.rostermasterModelValidation().errors.showAllMessages();
                //toastr.options.closeButton = true;
                //toastr.options.positionClass = "toast-top-right";
                //toastr.error("Please Select All Details", "Roster");
                return;
            }
        }

        self.ResetRosterMaster = function (model) {

            //self.rostermasterModel().reset();
            self.hasEnable(true);
            //self.rostermasterModel().RosterAloocationLists.removeAll();
            self.rostermasterModel(new IPMSROOT.RosterModel());
            self.LoadRosterReferencedata();
            $('#spnTitle').html("Roster");
        }

        ///
        self.CancelRosterMaster = function () {
            //window.location = '/Dashboard';

            self.hasEnable(true);
            self.rostermasterModel(new IPMSROOT.RosterModel());
            self.LoadRosterReferencedata();
            $('#spnTitle').html("Roster");
        }

        self.SaveRoster = function (data) {
            var RosterData = new IPMSROOT.RosterData();
            RosterData.Designation(data.Designation);
            RosterData.month(data.month);
            //prompt("to be saved", ko.toJSON(data));
            self.viewModelHelper.apiPost('api/SaveRosterData', ko.toJSON(data),
                            function (result) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                if (self.rostermasterModel().shouldShowSave() == true) {
                                    toastr.success("Roster details saved successfully.", "Roster");
                                }
                                if (self.rostermasterModel().shouldShowUpdate() == true) {
                                    toastr.success("Roster details updated successfully.", "Roster");
                                }
                                self.rostermasterModel(new IPMSROOT.RosterModel());
                                self.hasEnable(true);
                            });
        }

        self.Initialize();
    }
    IPMSRoot.RosterMasterViewModel = RosterMasterViewModel;

}(window.IPMSROOT));

