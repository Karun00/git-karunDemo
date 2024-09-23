(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    var RosterReferenceData = function (data) {

        var self = this;
        self.Designations = ko.observableArray(data ? $.map(data.Designations, function (item) { return new designations(item); }) : []);
        self.Shifts = ko.observableArray(data ? $.map(data.Shifts, function (item) { return new Shiftsdetails(item); }) : []);
        self.Months = ko.observableArray(data ? $.map(data.Months, function (item) { return new monthdetails(item); }) : []);

        self.Years = ko.observableArray(data ? $.map(data.Years, function (item) { return new Yeardetails(item); }) : []);

    }
    var designations = function (data) {

        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : undefined);
        self.SubCatName = ko.observable(data ? data.SubCatName : undefined);
    }
    var Shiftsdetails = function (data) {
        var self = this;
        self.ShiftID = ko.observable(data ? data.ShiftID : "");
        self.ShiftName = ko.observable(data ? data.ShiftName : "");
        self.ShiftFormat = ko.observable(data ? data.ShiftFormat : "");
    }
    var monthdetails = function (data) {
        var self = this;
        self.number = ko.observable(data ? data.number : undefined);
        self.monthname = ko.observable(data ? data.monthname : undefined);
    }

    var Yeardetails = function (data) {
        var self = this;
        self.YearName = ko.observable(data ? data.Years : undefined);
    }

    var RosterData = function (data) {
        var self = this;
        self.Designation = ko.observable(data ? data.Designation : undefined);
        self.month = ko.observable(data ? data.month : undefined);

        self.Year = ko.observable(data ? data.Year : undefined);
    }


    var RosterModel = function (data) {
        var self = this;
        self.RosterID = ko.observable(data ? data.RosterID : "");
        self.RosterCode = ko.observable(data ? data.RosterCode : "");
        self.Year = ko.observable(data ? data.Year : undefined).extend({ required: true });
        self.Week = ko.observable(data ? data.Week : "");
        self.shift = ko.observable(data ? data.shift : "");
        self.Designation = ko.observable(data ? data.Designation : undefined).extend({ required: true });
        self.month = ko.observable(data ? data.month : undefined).extend({ required: true });



        self.RosterGroupID = ko.observable(data ? data.RosterGroupID : "");
        self.ResourceGroupID = ko.observable(data ? data.ResourceGroupID : "");
        //self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        //self.WeekNo = ko.observable(data ? data.WeekNo : "");
        //self.CurDate = ko.observable(data ? data.CurDate : "");
        //self.CurDay = ko.observable(data ? data.CurDay : "");


        //self.GRP1 = ko.observableArray([]);//ko.observable(data ? data.GRP1 : "");
        //self.GRP2 = ko.observableArray([]);//ko.observable(data ? data.GRP2 : "");
        //self.GRP3 = ko.observableArray([]);//ko.observable(data ? data.GRP3 : "");
        //self.GRP4 = ko.observableArray([]);// ko.observable(data ? data.GRP4 : "");

        self.RosterAloocationLists = ko.observableArray(data ? ko.utils.arrayMap(data.RosterAloocationLists, function (data) {
            return new RosterAloocationList(data);
        }) : []);

        //self.RosterGroup = ko.observableArray(data ? ko.utils.arrayMap(data.RosterGroup, function (rosterdata) { return new AddRosterGroupTable(rosterdata); }) : []);
        //self.group = ko.observable();
        //
        //self.group.subscribe(function (value) {
        //    //value.dataSource.group({ field: 'category' });
        //    value.dataSource.group({ field: "WeekNo" });
        //});
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();

        self.shouldShowSave = ko.observable(false);
        self.shouldShowUpdate = ko.observable(false);
        self.shouldShowResetCan = ko.observable(false);
        self.shouldShowReset = ko.observable(false);


        self.cache = function () { };
        self.set(data);
     

    }

    // Employee Attendance
    var RosterAloocationList = function (data) {
        
        var self = this;
        self.WeekNo = ko.observable(data ? data.WeekNo : "");
        self.ResourceGroupID = ko.observable(data ? data.ResourceGroupID : "");
        self.ResourceGroupName = ko.observable(data ? data.ResourceGroupName : "");
        //self.Monday = ko.observableArray([]);
        //self.Wednesday = ko.observableArray([]);
        //self.Tuesday = ko.observableArray([]);
        //self.Thursday = ko.observableArray([]);
        //self.Friday = ko.observableArray([]);
        //self.Saturday = ko.observableArray([]);
        //self.Sunday = ko.observableArray([]);
        self.Year = ko.observable(data? data.Year : "");

        self.Monday = ko.observable(data ? data.Monday : "");
        self.Tuesday = ko.observable(data ? data.Tuesday : "");
        self.Wednesday = ko.observable(data ? data.Wednesday : "");
        self.Thursday = ko.observable(data ? data.Thursday : "");
        self.Friday = ko.observable(data ? data.Friday : "");
        self.Saturday = ko.observable(data ? data.Saturday : "");
        self.Sunday = ko.observable(data ? data.Sunday : "");
        self.DayFromTo = ko.observable(data ? data.RecordStatus : "");
        //self.DayFromTo = ko.computed(function () {
        //    var w = self.WeekNo();
        //    var y = self.Year();
        //    var simple = new Date(y, 0, 1 + (w - 1) * 7);
        //    var dow = simple.getDay();
        //    var ISOweekStart = simple;
            
        //    if (dow <= 5)
        //        ISOweekStart.setDate(simple.getDate() - simple.getDay());
        //    else
        //        ISOweekStart.setDate(simple.getDate() + 8 - simple.getDay());

          
        //    // return ISOweekStart;
        //    var d1 = new Date(Date.parse(ISOweekStart));
            

        //    //var rangeIsFrom =  d1.getFullYear() + "-"  +eval(d1.getMonth() + 1) + "-" + d1.getDate() ;
        //    //d1.setDate(d1.getDate() + 6);
        //    //var rangeIsTo = d1.getFullYear()  + "-" + eval(d1.getMonth() + 1) + "-" + d1.getDate();
        //    var rangeIsFrom =  d1.getDate()  + "/" + eval(d1.getMonth() + 1);
        //    d1.setDate(d1.getDate() + 6);
        //    var rangeIsTo =   d1.getDate() + "/" + eval(d1.getMonth() + 1);
        //    var DayFromTo1 = rangeIsFrom + " to " + rangeIsTo;
        //    return DayFromTo1;

        //    //var weekNo = self.WeekNo();
           
        //    //var d1 = new Date();
        //    //numOfdaysPastSinceLastMonday = eval(d1.getDay() - 1);
        //    //d1.setDate(d1.getDate() - numOfdaysPastSinceLastMonday);
        //    //var weekNoToday = d1.getWeek();
        //    //var weeksInTheFuture = eval(weekNo - weekNoToday);
        //    //d1.setDate(d1.getDate() + eval(7 * weeksInTheFuture));
        //    //var rangeIsFrom = eval(d1.getMonth() + 1) + "-" + d1.getDate() + "-" + d1.getFullYear();
        //    //d1.setDate(d1.getDate() + 6);
        //    //var rangeIsTo = eval(d1.getMonth() + 1) + "-" + d1.getDate() + "-" + d1.getFullYear();
        //    //return rangeIsFrom + " to " + rangeIsTo;


        //});


        
    }
    ipmsRoot.RosterAloocationList = RosterAloocationList;
    ipmsRoot.RosterModel = RosterModel;
    ipmsRoot.RosterReferenceData = RosterReferenceData;
    ipmsRoot.RosterData = RosterData;


}(window.IPMSROOT));

IPMSROOT.RosterModel.prototype.set = function (data) {
    var self = this;
    self.RosterID(data ? (data.RosterID || "") : "");
    self.RosterCode(data ? (data.RosterCode || "") : "");
    self.Year(data ? (data.Year || undefined) : undefined);
    self.Week(data ? (data.Week || "") : "");
    self.Designation(data ? (data.Designation || undefined) : undefined);
    self.month(data ? (data.month || undefined) : undefined);

    // self.Mon(data ? (data.Mon || "") : "");
    //self.Tue(data ? (data.Tue || "") : "");
    //self.Wed(data ? (data.Wed || "") : "");
    //self.Thu(data ? (data.Thu || "") : "");
    //self.Fri(data ? (data.Fri || "") : "");
    //self.Sat(data ? (data.Sat || "") : "");
    //self.Sun(data ? (data.Sun || "") : "");
    self.RosterGroupID(data ? (data.RosterGroupID || "") : "");
    self.ResourceGroupID(data ? (data.ResourceGroupID || "") : "");
    // self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.cache.latestData = data;
}
IPMSROOT.RosterModel.prototype.reset = function () {   
    this.set(this.cache.latestData);
}