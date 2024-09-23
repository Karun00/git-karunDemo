(function (ipmsRoot) {

    var WorkFlowRefernceData = function (data) {
        var self = this;
        self.WorkFlowEvents_List = ko.observableArray(data ? $.map(data.WorkFlowEvents, function (item) { return new WorkFlowEvent(item); }) : []);
        self.Entities_List = ko.observableArray(data ? $.map(data.Entities, function (item) { return new Entity(item); }) : []);
        self.Role_List = ko.observableArray(data ? $.map(data.Roles, function (item) { return new Role(item); }) : []);
    }

    var WorkFlowEvent = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var Entity = function (data) {
        var self = this;
        self.EntityID = ko.observable(data ? data.EntityID : "");
        self.EntityName = ko.observable(data ? data.EntityName : "");
    }

    var Role = function (data) {
        var self = this;
        self.RoleID = ko.observable(data ? data.RoleID : "");
        self.RoleName = ko.observable(data ? data.RoleName : "");
    }

    var WorkFlowTaskModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(false);

        self.EntityID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.EntityName = ko.observable();
        self.CreatedBy = ko.observable('1');
        self.CreatedDate = GetDateTime();
        self.ModifiedBy = ko.observable('1');
        self.ModifiedDate = GetDateTime();
        self.WorkFlowTaskVO = ko.observableArray();
        self.WorkFlowTaskRoleVO = ko.observableArray();

        self.EntityNameSort;
        self.EntityName.subscribe(function (value) {
            self.EntityNameSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }

    var WorkFlowTaskVO = function (data) {
        var self = this;

        self.WorkflowTaskCode = ko.observable(data ? data.WorkflowTaskCode : "");
        self.Step = ko.observable(data ? data.Step : "");
        self.NextStep = ko.observable(data ? data.NextStep : "");
        self.HasNotification = ko.observable(data ? data.HasNotification == "Y" : true || false);
        self.HasRemarks = ko.observable(data ? data.HasRemarks == "Y" : true || false);
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.ValidityPeriod = ko.observable(data ? data.ValidityPeriod : "");
        self.RoleID = ko.observable(data ? data.RoleID : "");
        self.EntityID = ko.observable(data ? data.EntityID : "");
        self.EntityName = ko.observable(data ? data.EntityName : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");

        //Getting RoleID's from array
        var temp = [];
        try {
            if (typeof data.arrayRoles != "undefined" && data.arrayRoles[0] != null && data.arrayRoles[0].length > 0) {
            for (i = 0; i < data.arrayRoles.length; i++) {
                temp.push(data.arrayRoles[i]);
            }
        }
        else {
            temp.push(0);
        }
        } catch (e) { }

        self.arrayRoles = ko.observableArray(temp ? (temp || "") : "");
    }

    var WorkflowTaskRole = function (data) {
        var self = this;
        self.EntityID = ko.observable(data ? data.EntityID : "");
        self.Step = ko.observable(data ? data.Step : "");
        self.RoleID = ko.observable(data ? data.RoleID : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
    }

    ipmsRoot.WorkFlowTaskModel = WorkFlowTaskModel;
    ipmsRoot.WorkFlowRefernceData = WorkFlowRefernceData;
    ipmsRoot.WorkFlowEvent = WorkFlowEvent;
    ipmsRoot.Entity = Entity;
    ipmsRoot.WorkFlowTaskVO = WorkFlowTaskVO;
    ipmsRoot.WorkflowTaskRole = WorkflowTaskRole;
    ipmsRoot.Role = Role;

}(window.IPMSROOT));

IPMSROOT.WorkFlowTaskModel.prototype.set = function (data) {
    var self = this;

    self.EntityID(data ? data.EntityID : "");
    self.EntityName(data ? (data.EntityName) || "" : "");

    self.WorkFlowTaskVO(data ? ko.utils.arrayMap(data.WorkFlowTaskVO, function (workflowtask) {
        return new IPMSROOT.WorkFlowTaskVO(workflowtask);
    }) : []);

    self.WorkFlowTaskRoleVO(data ? ko.utils.arrayMap(data.WorkFlowTaskRoleVO, function (workflowtask) {
        return new IPMSROOT.WorkflowTaskRole(workflowtask);
    }) : []);

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.WorkFlowTaskModel.prototype.reset = function () {
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

