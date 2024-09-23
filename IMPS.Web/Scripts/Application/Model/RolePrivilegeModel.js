(function (ipmsRoot) {

    var RolePrivilegeModel = function (data, masterModules, SubModulesList) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.RoleID = ko.observable();
        self.RoleCode = ko.observable("").extend({ CodeUnique: self.RoleCode, required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.RoleName = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.RoleDescription = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.ModuleID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.RolePrivileges = ko.observableArray([]);
        //self.ModuleName = ko.observable("");
        self.SubModuleID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        //self.SubModuleName = ko.observable("");
        self.EntitieList = ko.observableArray([]);
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.RoleCodeSort;
        self.RoleCode.subscribe(function (value) {
            self.RoleCodeSort = value;
        });
        self.RoleNameSort;
        self.RoleName.subscribe(function (value) {
            self.RoleNameSort = value;
        });

        self.getModules = ko.observableArray([]);
        self.getSubModules = ko.observableArray([]);
        self.getEntityPrivList = ko.observableArray([]);

        self.cache = function () { };
        self.set(data);
    }

    var Roles = function (data) {
        var self = this;
        self.RoleID = ko.observable(data ? data.RoleID : "");
        self.EntityID = ko.observable(data ? data.EntityID : "");
        self.RolePrivEditList = ko.observable(data ? data.RolePrivEditList : [new RolePrivEdit()]);
    }

    var RolePrivEdit = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
        self.RPrivchecked = ko.observable(data ? data.RPrivchecked : true);
    }

    var Module = function (data) {
        var self = this;
        self.ModuleID = ko.observable(data ? data.ModuleID : "");
        self.ModuleName = ko.observable(data ? data.ModuleName : "");
        self.SubModules = ko.observableArray(data ? data.SubModules : [new SubModule()]);
    }

    var Entities = function (data) {
        var self = this;
        self.EntityID = ko.observable(data ? data.EntityID : "");
        self.EntityName = ko.observable(data ? data.EntityName : "");
        self.EntityPrivileges = ko.observable(data ? data.EntityPrivileges : [new EntityPriv()]);
    }

    var EntityPriv = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
        self.IsRole = ko.observable(data ? data.IsRole : false);
    }

    var SubModule = function (data) {
        var self = this;
        self.SubModuleID = ko.observable(data ? data.ModuleID : "");
        self.SubModuleName = ko.observable(data ? data.ModuleName : "");
    }

    var RolePrivilege = function (data) {
        var self = this;
        self.RoleID = ko.observable(data ? data.RoleID : "");
        self.EntityID = ko.observable(data ? data.EntityID : "");
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
    }

    ipmsRoot.Module = Module;
    ipmsRoot.SubModule = SubModule;
    ipmsRoot.Entities = Entities;
    ipmsRoot.EntityPriv = EntityPriv;
    ipmsRoot.Roles = Roles;
    ipmsRoot.RolePrivEdit = RolePrivEdit;
    ipmsRoot.RolePrivilegeModel = RolePrivilegeModel;

}(window.IPMSROOT));

IPMSROOT.RolePrivilegeModel.prototype.set = function (data) {
    var self = this;
    self.RoleID(data ? (data.RoleID || "") : "");
    self.RoleCode(data ? (data.RoleCode || "") : "");
    self.RoleName(data ? (data.RoleName == 'NULL' ? "" : data.RoleName || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.RoleDescription(data ? (data.RoleDescription == 'NULL' ? "" : data.RoleDescription || "") : "");
    self.RolePrivileges(data ? (data.RolePrivileges ? $.map(data.RolePrivileges, function (item) { return item }) : []) : []);

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.RolePrivilegeModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}


