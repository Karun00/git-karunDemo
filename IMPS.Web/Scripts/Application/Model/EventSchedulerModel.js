(function (ipmsRoot) {



    var EventSchedulerModel = function (data) {
        var self = this;
     
        self.EntityID = ko.observable(data ? data.EntityID : "").extend({ required: { message: '* Entity Name is required' } });
        self.EntityName = ko.observable(data ? data.Entity.EntityName : "");
        self.EventScheduleID = ko.observable(data ? data.EventScheduleID : 0);
        self.EventScheduleCode = ko.observable("");
        self.EventScheduleName = ko.observable(data ? data.EventScheduleName : "").extend({ required: { message: '* Event Schedule Name is required' } });
     
        self.EventScheduleStartDate = ko.observable(data ? data.EventScheduleStartDate : new Date());
        self.EventScheduleTime = ko.observable(data ? data.EventScheduleTime : new Date());
        self.ExecutionPlan = ko.observable(data ? data.ExecutionPlan : "");
        self.NextExecutionDateTime = ko.observable(data ? data.NextExecutionDateTime : "");
        self.EventScheduleEndDateTime = ko.observable(data ? data.EventScheduleEndDateTime : "");
        self.ExecutionCount = ko.observable(data ? data.ExecutionCount : "");
        self.LastExecutionDateTime = ko.observable(data ? data.LastExecutionDateTime : "");

   
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
     
        self.CreatedDate = ko.observable(data ? data.CreatedDate : new Date());
        self.CreatedBy = ko.observable(data ? data.CreatedBy : 0);
            

        self.EventScheduleTypeItems = ko.observableArray([{ value: 'D', Name: 'Daily' }, { value: 'W', Name: 'Weekly' }, { value: 'M', Name: 'Monthly' }, { value: 'S', Name: 'Scheduled' }, { value: 'I', Name: 'Interval' }, { value: 'C', Name: 'Custom' }]);
        self.EventScheduleType = ko.observable(data ? data.EventScheduleType : 'D');

        self.toggleAssociation = function (item) {        
                    
            self.EventScheduleType(item.value);
           return true;
        };       
      
        self.WeeklyItems = ko.observableArray([{ value: 'MON', Name: 'Monday' }, { value: 'TUE', Name: 'Tuesday' }, { value: 'WED', Name: 'Wednesday' }, { value: 'THU', Name: 'Thursday' }, { value: 'FRI', Name: 'Friday' }, { value: 'SAT', Name: 'Saturday' }, { value: 'SUN', Name: 'Sunday' }]);
       

        self.SelectedWeeklyItem = ko.observableArray([]);
        self.Day = ko.observable(data ? data.Day : "");
        self.Hour = ko.observable(data ? data.Hour : "");
        self.Minute = ko.observable(data ? data.Minute : "");
        self.Coustom = ko.observable(data ? data.Coustom : "");

        self.cache = function () { };
        self.set(data);
     
    }
    ipmsRoot.EventSchedulerModel = EventSchedulerModel;
}(window.IPMSROOT));

IPMSROOT.EventSchedulerModel.prototype.set = function (data) {
    var self = this;
    self.EventScheduleID(data ? (data.EventScheduleID  || null): null);
    self.EntityID(data ? (data.EntityID || null) : null);
    self.EntityName(data ? (data.Entity.EntityName || "") : "");
    self.EventScheduleCode(data ? (data.EventScheduleCode || "") : "");
    self.EventScheduleName(data ? (data.EventScheduleName || "") : "");
    self.EventScheduleStartDate(data ? (data.EventScheduleStartDate || new Date()) : new Date());
    self.EventScheduleTime(data ? (data.EventScheduleTime || new Date()) : new Date());

    self.CreatedDate(data ? (data.CreatedDate || new Date()) : new Date());
    self.CreatedBy(data ? (data.CreatedBy || 0) : 0);

    self.EventScheduleType(data ? (data.EventScheduleType || "D") : "D");
    self.SelectedWeeklyItem(data ? (data.SelectedWeeklyItem || []) : []);
    self.Day(data ? (data.Day ||"") : "");
    self.Hour(data ? (data.Hour || "") : "");
    self.Minute(data ? (data.Minute || "") : "");
    self.Coustom(data ? (data.Coustom || "") : "");

    self.cache.latestData = data;
}

IPMSROOT.EventSchedulerModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}


function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
}




