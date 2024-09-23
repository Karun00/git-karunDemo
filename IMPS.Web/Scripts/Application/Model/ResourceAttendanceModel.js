(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })
    //  ResourceAttendanceModel Used For bind The All controls from view model
    var ResourceAttendanceModel = function (data) {

        var self = this;
        self.validationEnabled = ko.observable(true);
        self.ShiftName = ko.observable(data ? data.ShiftName : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.ResourceAttendanceID = ko.observable(data ? data.ResourceAttendanceID : "");
        self.ShiftID = ko.observable(data ? data.ShiftID : "");
        self.AttendanceDate = ko.observable(data ? data.AttendanceDate : new Date()).extend({ required: { message: '*  Please Select Attendance Date' } });
        self.Position = ko.observable(data ? data.Position : "");
        self.Designation = ko.observable(data ? data.Designation : "");
        self.Name = ko.observable(data ? data.Name : "");
        self.PersonalMobileNo = ko.observable(data ? data.PersonalMobileNo : "");
        self.DesignationCode = ko.observable(data ? data.DesignationCode : "");
        self.Gender = ko.observable(data ? data.Gender : "");
        self.BirthDate = ko.observable(data ? data.BirthDate : "");
        self.JoiningDate = ko.observable(data ? data.JoiningDate : "");
        self.Attendance = ko.observable(data ? data.Attendance : "");
        self.AttendanceStatus = ko.observable(data ? data.AttendanceStatus : "");
        self.ResourceAttendanceID = ko.observable(data ? data.ResourceAttendanceID : "");
        self.Employees = ko.observableArray(data ? ko.utils.arrayMap(data.Employees, function (data) {
            return new Employee(data);
        }) : []);
        self.shouldShowSave = ko.observable(false);
        self.shouldShowUpdate = ko.observable(false);

        self.cache = function () { };
        self.set(data);
    }

    //ResourceAttendance Used For bind and send data  from view model controls
    var ResourceAttendance = function (data) {
        var self = this;
        self.AttendanceDate = ko.observable(data ? data.AttendanceDate : "");
        self.ResourceAttendanceID = ko.observable(data ? data.ResourceAttendanceID : "");
        self.Position = ko.observable(data ? data.Position : "");
        self.ShiftID = ko.observable(data ? data.ShiftID : "");
        self.ResourceAttendanceDtls = ko.observableArray(data ? $.map(data.ResourceAttendanceDtls, function (item) { return new ResourceAttendanceDtl(item); }) : []);
    }

    // ResourceAttendanceDtl Used For bind and send data  from view model controls
    var ResourceAttendanceDtl = function (data) {
        var self = this;
        self.EmployeeID = ko.observable(data ? data.EmployeeID : "");
        self.AttendanceStatus = ko.observable(data ? data.AttendanceStatus : "");
        self.ShiftID = ko.observable(data ? data.ShiftID : "");
        self.JoiningDate = ko.observable(data ? data.JoiningDate : "");

    }

    // Employee Used For bind and send data  from view model controls
    var Employee = function (data) {
        var self = this;
        self.EmployeeID = ko.observable(data ? data.EmployeeID : "");
        self.EmpName = ko.observable(data ? data.EmpName : "");
        self.Gender = ko.observable(data ? data.Gender : "");
        self.PersonalMobileNo = ko.observable(data ? data.PersonalMobileNo : "");
        self.BirthDate = ko.observable(data ? data.BirthDate : "");
        self.JoiningDate = ko.observable(data ? data.JoiningDate : "");

        self.ShiftName = ko.observable(data ? data.ShiftName : "");
        self.ShiftID = ko.observable(data ? data.ShiftID : "");
        
        self.AttendanceStatus = ko.observable(data ? data.AttendanceStatus : "");
        self.FirstName = ko.observable(data ? data.FirstName : "");
        // Sorting fields

        //FirstName
        self.NameSort;
        self.EmpName.subscribe(function (value) {
            self.NameSort = value.trim();
        });

        self.PersonalMobileNoSort;
        self.PersonalMobileNo.subscribe(function (value) {
            self.PersonalMobileNoSort = value;
        });

        self.GenderSort;
        self.Gender.subscribe(function (value) {
            self.GenderSort = value;
        });

        self.JoiningDateSort;
        self.JoiningDate.subscribe(function (value) {
            self.JoiningDateSort = value;
        });

        self.ShiftNameSort;
        self.ShiftName.subscribe(function (value) {
            self.JoiningDateSort = value;
        });


        self.BirthDateSort;
        self.BirthDate.subscribe(function (value) {
            self.BirthDateSort = value;
        });
    }

    ipmsRoot.ResourceAttendanceModel = ResourceAttendanceModel;
    ipmsRoot.ResourceAttendance = ResourceAttendance;
    ipmsRoot.ResourceAttendanceDtl = ResourceAttendanceDtl;
    ipmsRoot.Employee = Employee;

}(window.IPMSROOT));

IPMSROOT.ResourceAttendanceModel.prototype.set = function (data) {
    var self = this;
    self.ShiftName(data ? (data.ShiftName || "") : "");
    self.StartTime(data ? (data.StartTime || "") : "");
    self.ResourceAttendanceID(data ? (data.ResourceAttendanceID || "") : "");
    self.ShiftID(data ? (data.ShiftID || "") : "");
    self.AttendanceDate(data ? (data.AttendanceDate || "") : new Date());
    self.Position(data ? (data.Position || "") : "");
    self.Designation(data ? (data.Designation || "") : "");
    self.Name(data ? (data.Name || "") : "");
    self.PersonalMobileNo(data ? (data.PersonalMobileNo || "") : "");
    self.DesignationCode(data ? (data.DesignationCode || "") : "");
    self.Gender(data ? (data.Gender || "") : "");
    self.BirthDate(data ? (data.BirthDate || "") : "");
    self.JoiningDate(data ? (data.JoiningDate || "") : "");

  
   

    self.Attendance(data ? (data.Attendance || "") : "");
    self.AttendanceStatus(data ? (data.AttendanceStatus || "") : "");

    self.cache.latestData = data;
}

IPMSROOT.ResourceAttendanceModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



