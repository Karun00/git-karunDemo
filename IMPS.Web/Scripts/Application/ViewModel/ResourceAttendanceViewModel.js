(function (IPMSRoot) {

    var ResourceAttendanceViewModel = function () {

        var self = this;
        $('#spnTitile').html("Resource Attendance");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        self.resourceAttendanceModel = ko.observable(new IPMSROOT.ResourceAttendanceModel());
        self.Designations = ko.observableArray();
        self.Shifts = ko.observableArray();
        self.ResourceAttendanceData = ko.observableArray();
        self.IsViewMode = ko.observable(true);
        self.viewMode = ko.observable();
        self.hasEnable = ko.observable(true);
        self.gridvisiable = ko.observable(false);
        self.ispresent = ko.observable(true);

        self.validationHelper = new IPMSROOT.validationHelper();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        // To intialize objcets
        self.Initialize = function () {
            self.LoadDesignations();
            self.LoadShifts();
        }

        //To get desigantions
        self.LoadDesignations = function () {
            self.viewModelHelper.apiGet('api/ResourceDesignationDetails', null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.Designations);
              }, null, null, false);
        }

        //To get shifts
        self.LoadShifts = function (data) {
            var ResourceAttendance = new IPMSROOT.ResourceAttendance();
            ResourceAttendance.Position(self.resourceAttendanceModel().Position());
            ResourceAttendance.AttendanceDate(moment(self.resourceAttendanceModel().AttendanceDate()).format('YYYY-MM-DD'))

            self.viewModelHelper.apiPost('api/ResourceShiftDetails',
            ko.toJSON(ResourceAttendance),
            function (result) {
                ko.mapping.fromJS(result, {}, self.Shifts);
            });
        }

        // To get the Resource Attendance Data
        self.LoadResourceAttendanceData = function (data) {
            self.RAValidation = ko.observable(data);
            self.RAValidation().errors = ko.validation.group(self.RAValidation());
            var errors = self.RAValidation().errors().length;
            self.ispresent(true);
            var errors1 = 0;
            errors1 = Validation();

            if (errors1 != 0) {
                self.RAValidation().errors.showAllMessages();
                return;
            }
            if (errors == 0) {
                self.hasEnable(false);
                var ResourceAttendance = new IPMSROOT.ResourceAttendance();
                ResourceAttendance.Position(data.Position());
                ResourceAttendance.AttendanceDate(moment(data.AttendanceDate()).format('YYYY-MM-DD'));
                ResourceAttendance.ShiftID(data.ShiftName());

                self.viewModelHelper.apiPost('api/ResourceAttendanceDetails', ko.toJSON(ResourceAttendance),
                        function (result) {
                            self.gridvisiable(true);
                            self.resourceAttendanceModel().Employees([]);
                            ko.utils.arrayMap(result, function (data) {
                                var employee = new IPMSROOT.Employee();
                                employee.EmployeeID(data.EmployeeID);
                                employee.EmpName(data.EmpName);
                                employee.Gender(data.Gender);
                                employee.PersonalMobileNo(data.PersonalMobileNo);
                                employee.BirthDate(data.BirthDate);
                                employee.JoiningDate(data.JoiningDate);
                                employee.ShiftName(data.ShiftName);
                                employee.ShiftID(data.ShiftID);

                                employee.AttendanceStatus(data.AttendanceStatus);
                                self.resourceAttendanceModel().Employees.push(employee);
                                self.resourceAttendanceModel().ResourceAttendanceID(data.ResourceAttendanceID);

                                if (data.ResourceAttendanceID > 0) {
                                    self.resourceAttendanceModel().shouldShowSave(false);
                                    self.resourceAttendanceModel().shouldShowUpdate(true);
                                }
                                else {
                                    self.resourceAttendanceModel().shouldShowUpdate(false);
                                    self.resourceAttendanceModel().shouldShowSave(true);
                                }
                            });

                            if (moment(data.AttendanceDate()).format('YYYY-MM-DD') != moment(new Date()).format('YYYY-MM-DD')) {
                                self.resourceAttendanceModel().shouldShowSave(false);
                                self.resourceAttendanceModel().shouldShowUpdate(false);
                                self.ispresent(false);
                            }
                        });
            }
            else {
                self.RAValidation().errors.showAllMessages();

                return;
            }
        }

        self.ResetResourceAttendance = function (model) {
            self.resourceAttendanceModel().reset();
            self.resourceAttendanceModel().AttendanceDate(moment(new Date()).format('YYYY-MM-DD'));
            $('#spanvdesgcode').text('');
            $('#spanvshifts').text('');
        }

        self.ResettotalResourceAttendance = function (model) {
            self.gridvisiable(false);
            self.LoadDesignations();
            self.LoadShifts();
            self.resourceAttendanceModel(new IPMSROOT.ResourceAttendanceModel());
            self.resourceAttendanceModel().reset();
            $('#spnTitile').html("Resource Attendance");
            self.resourceAttendanceModel().AttendanceDate(moment(new Date()).format('YYYY-MM-DD'));
            self.hasEnable(true);
            $('#spanvdesgcode').text('');
            $('#spanvshifts').text('');
        }

        // To save Resource Attendance Data
        self.SaveResourceAttendance = function (data) {

            self.RAValidation = ko.observable(data);
            self.RAValidation().errors = ko.validation.group(self.RAValidation());
            var errors = self.RAValidation().errors().length;

            var errors1 = 0;
            errors1 = Validation();

            if (errors1 != 0) {
                self.RAValidation().errors.showAllMessages();
                return;
            }

            if (errors == 0) {
                var ResourceAttendance = new IPMSROOT.ResourceAttendance();
                ResourceAttendance.AttendanceDate(data.AttendanceDate);
                ResourceAttendance.Position(data.Position);
                ResourceAttendance.ShiftID(data.ShiftName);
                ResourceAttendance.ResourceAttendanceID(self.resourceAttendanceModel().ResourceAttendanceID());
                ko.utils.arrayMap(data.Employees(), function (data) {
                    debugger;
                    var resourceAttendanceDtl = new IPMSROOT.ResourceAttendanceDtl();
                    resourceAttendanceDtl.EmployeeID(data.EmployeeID);
                    resourceAttendanceDtl.ShiftID(data.ShiftID);
                    resourceAttendanceDtl.JoiningDate(data.JoiningDate);
                    resourceAttendanceDtl.AttendanceStatus(data.AttendanceStatus);
                    ResourceAttendance.ResourceAttendanceDtls.push(resourceAttendanceDtl);

                });
                var isupdate = self.resourceAttendanceModel().shouldShowUpdate();
                var isSave = self.resourceAttendanceModel().shouldShowSave()

                self.viewModelHelper.apiPost('api/SaveResourceAttendance', ko.toJSON(ResourceAttendance), function (result) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Resource attendance saved successfully.", "Resource Attendance");
                });

                self.hasEnable(true);
                self.resourceAttendanceModel().Employees([]);
                self.resourceAttendanceModel().shouldShowSave(false);
                self.resourceAttendanceModel().shouldShowUpdate(false);
                self.resourceAttendanceModel().reset();
                self.resourceAttendanceModel().AttendanceDate(moment(new Date()).format('YYYY-MM-DD'));
                $('#spanvdesgcode').text('');
                $('#spanvshifts').text('');
                self.gridvisiable(false);
            }
            else {
                self.RAValidation().errors.showAllMessages();
                return;
            }
        }

        self.cancel = function (model) {
            window.location = '/ResourceAttendance';
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        //Designation Change
        ChangeDesignation = function () {
            if ($("#Designation").val() == "" || $("#Designation").val() == null) {
                $('#spanvdesgcode').text('Please select employee designation.');
            }
            else {
                $("#spanvdesgcode").text('');
            }
        }

        //Designation Change
        ChangeShifts = function () {
            if ($("#Shifts").val() == "" || $("#Shifts").val() == null) {
                $('#spanvshifts').text('Please select shift.');
            }
            else {
                $("#spanvshifts").text('');
            }
        }

        Validation = function () {
            var NoOfErrors = 0;

            $('#spanvdesgcode').text('');
            $('#spanvshifts').text('');

            if ($("#Designation").val() == "" || $("#Designation").val() == null) {
                $('#spanvdesgcode').text('Please select employee designation.');
                NoOfErrors++;
            }

            if ($("#Shifts").val() == "" || $("#Shifts").val() == null) {
                $('#spanvshifts').text('Please select shift.');
                NoOfErrors++;
            }
            return NoOfErrors;
        }

        self.Initialize();
    }

    IPMSRoot.ResourceAttendanceViewModel = ResourceAttendanceViewModel;

}(window.IPMSROOT));

