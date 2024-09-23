(function (ipmsRoot) {

    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    var CraftConfigReferenceData = function (data) {
        var self = this;
        self.particularTypesData = ko.observableArray(data ? $.map(data.ParticularTypes, function (item) { return new ParticularType(item); }) : []);
        self.calenderTypesData = ko.observableArray(data ? $.map(data.CalenderTypes, function (item) { return new CalenderType(item); }) : []);
        self.fuelTypesData = ko.observableArray(data ? $.map(data.FuelType, function (item) { return new FuelType(item); }) : []);
        self.craftTypesData = ko.observableArray(data ? $.map(data.CraftType, function (item) { return new CraftType(item); }) : []);
    }

    var ParticularType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var CalenderType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var FuelType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var CraftType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var CraftModel = function (data) {

        var self = this;

        self.CraftCode = ko.observable("");
        self.IMONo = ko.observable("");
        self.CraftName = ko.observable("");
        self.CallSign = ko.observable("");
        self.CraftBuildDate = ko.observable(data ? data.CraftBuildDate : "");
        self.BuildDate = ko.observable(data ? data.BuildDate : "");
        
        self.FuelTypeName = ko.observable(data ? data.FuelTypeName : "");
        self.CraftTypeName = ko.observable(data ? data.CraftTypeName : "");
        self.CommissionStatus = ko.observable(data ? data.CommissionStatus : "");
        self.CraftID = ko.observable(data ? data.CraftID : "");

        self.ParticularsNo = ko.observable("");
        self.CraftReminderConfigID = ko.observable("");
        self.ReminderName = ko.observable("");
        self.ParticularsName = ko.observable("");
        self.IssuingAuthority = ko.observable("");
        self.DateOfIssue = ko.observable("");
        self.DateOfValidity = ko.observable("");
        self.AlertOccurance1 = ko.observable("");
        self.AlertPeriod1 = ko.observable("");
        self.AlertOccurance2 = ko.observable("");
        self.AlertPeriod2 = ko.observable("");
        self.AlertOccurance3 = ko.observable("");
        self.AlertPeriod3 = ko.observable("");
        self.ReminderStatus = ko.observable("");
        self.ExitReminderConfig = ko.observable("");
        self.RecordStatus = ko.observable('A');

        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");

        self.CraftReminderConfig = ko.observableArray(data ? ko.utils.arrayMap(data.CraftReminderConfigs, function (model) {
            return new CraftReminderConfigModel1(model);
        }) : []);


        self.CraftCodeSort;
        self.CraftCode.subscribe(function (value) {
            self.CraftCodeSort = value;
        });
        self.CraftNameSort;
        self.CraftName.subscribe(function (value) {
            self.CraftNameSort = value;
        });
        self.IMONoSort;
        self.IMONo.subscribe(function (value) {
            self.IMONoSort = value;
        });
        self.CallSignSort;
        self.CallSign.subscribe(function (value) {
            self.CallSignSort = value;
        });

        self.ParticularsNameSort;
        self.ParticularsName.subscribe(function (value) {
            self.ParticularsNameSort = value;
        });
        self.ParticularsNoSort;
        self.ParticularsNo.subscribe(function (value) {
            self.ParticularsNoSort = value;
        });
        self.IssuingAuthoritySort;
        self.IssuingAuthority.subscribe(function (value) {
            self.IssuingAuthoritySort = value;
        });
        self.DateOfIssueSort;
        self.DateOfIssue.subscribe(function (value) {
            self.DateOfIssueSort = value;
        });
        self.DateOfValiditySort;
        self.DateOfValidity.subscribe(function (value) {
            self.DateOfValiditySort = value;
        });
        self.AlertOccurance1Sort;
        self.AlertOccurance1.subscribe(function (value) {
            self.AlertOccurance1Sort = value;
        });
        self.AlertOccurance2Sort;
        self.AlertOccurance2.subscribe(function (value) {
            self.AlertOccurance2Sort = value;
        });
        self.AlertOccurance3Sort;
        self.AlertOccurance3.subscribe(function (value) {
            self.AlertOccurance3Sort = value;
        });

       

        self.cache = function () { };
        self.set(data);
    }

    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Enter Remarks' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : null);
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    var CraftReminderConfigModel1 = function (data) {
        var self = this;
        self.CraftReminderConfigID = ko.observable(data ? data.CraftReminderConfigID : "");
        self.CraftID = ko.observable(data ? data.CraftID : "");
        self.ReminderName = ko.observable(data ? data.ReminderName : "").extend({ required: { message: '* Please Select Particular Name' } });
        self.ParticularsNo = ko.observable(data ? data.ParticularsNo : "").extend({ required: { message: '* Please Enter Particular No' } });
        self.IssuingAuthority = ko.observable(data ? data.IssuingAuthority : "").extend({ required: { message: '* Please Enter Issuing Authority' } });
        self.DateOfIssue = ko.observable(data ? data.DateOfIssue : "").extend({ required: { message: '* Please Enter Date Of Issue' } });
        self.DateOfValidity = ko.observable(data ? data.DateOfValidity : "").extend({ required: { message: '* Please Enter Date Of Validity' } });
        self.AlertOccurance1 = ko.observable(data ? data.AlertOccurance1 : "");
        self.AlertPeriod1 = ko.observable(data ? data.AlertPeriod1 : null);
        self.AlertOccurance2 = ko.observable(data ? data.AlertOccurance2 : "");
        self.AlertPeriod2 = ko.observable(data ? data.AlertPeriod2 : null);
        self.AlertOccurance3 = ko.observable(data ? data.AlertOccurance3 : "");
        self.AlertPeriod3 = ko.observable(data ? data.AlertPeriod3 : null);
        self.ReminderStatus = ko.observable(data ? data.ReminderStatus : "");
        self.ExitReminderConfig = ko.observable(data ? data.ExitReminderConfig : "");
        self.ParticularsName = ko.observable(data ? data.ParticularsName : "");
        self.RecordStatus = ko.observable('A');

        self.RecordStatust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);
        self.cache = function () { };
        self.set(data);

    }

    ipmsRoot.CraftModel = CraftModel;
    ipmsRoot.CraftReminderConfigModel1 = CraftReminderConfigModel1;
    ipmsRoot.CraftConfigReferenceData = CraftConfigReferenceData;
    ipmsRoot.ParticularType = ParticularType;
    ipmsRoot.CalenderType = CalenderType;
    ipmsRoot.FuelType = FuelType;
    ipmsRoot.CraftType = CraftType;

}(window.IPMSROOT));
IPMSROOT.CraftModel.prototype.set = function (data) {
    var self = this;

    self.CraftCode(data ? (data.CraftCode || "") : "");
    self.IMONo(data ? (data.IMONo || "") : "");
    self.CraftName(data ? (data.CraftName || "") : "");
    self.CallSign(data ? (data.CallSign || "") : "");
    self.CraftBuildDate(data ? (moment(data.CraftBuildDate).format('YYYY-MM-DD') || "") : "");
    self.FuelTypeName(data ? (data.FuelTypeName || "") : "");
    self.CraftTypeName(data ? (data.CraftTypeName || "") : "");
    self.CommissionStatus(data ? (data.CommissionStatus || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.CraftModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

IPMSROOT.CraftReminderConfigModel1.prototype.set = function (data) {
    var self = this;
    self.ReminderName(data ? (data.ReminderName || "") : "");
    self.ParticularsNo(data ? (data.ParticularsNo || "") : "");
    self.IssuingAuthority(data ? (data.IssuingAuthority || "") : "");
    self.DateOfIssue(data ? (data.DateOfIssue || "") : "");
    self.DateOfValidity(data ? (data.DateOfValidity || "") : "");
    self.AlertOccurance1(data ? (data.AlertOccurance1 || "") : "");
    self.AlertPeriod1(data ? (data.AlertPeriod1 || null) : null);
    self.AlertOccurance2(data ? (data.AlertOccurance2 || "") : "");
    self.AlertPeriod2(data ? (data.AlertPeriod2 || null) : null);
    self.AlertOccurance3(data ? (data.AlertOccurance3 || "") : "");
    self.AlertPeriod3(data ? (data.AlertPeriod3 || null) : null);
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    
    self.cache.latestData = data;
}

IPMSROOT.CraftReminderConfigModel1.prototype.reset = function () {
    this.set(this.cache.latestData);
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





