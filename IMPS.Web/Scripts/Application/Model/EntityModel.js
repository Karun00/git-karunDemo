(function (ipmsRoot) {

    var EntityModel = function (data, masterModules, masterPriviligeTypes) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.EntityCode = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Page Code' } });
        self.EntityName = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Page Name' } });
        self.PageUrl = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Page URL' } });
        self.OrderNo = ko.observable("").extend({ required: true });// { onlyIf: self.validationEnabled, message: '* Please enter the Sequence No' } });
        self.ModuleID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Sub Module Name' } });
        self.HasWorkflow = ko.observable('');
        self.HasMenuItem = ko.observable('');
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.ModuleName = ko.observable("");
        self.EntityID = ko.observable("");
        self.HasWorkflowStatus = ko.observable(false);
        self.HasMenuItemStatus = ko.observable(false);
        self.ModuleNameList = ko.observable("");
     

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });



        self.EntityPrivileges = ko.observableArray([]);          

        self.EntityCodeSort;
        self.EntityCode.subscribe(function (value) {
            self.EntityCodeSort = value;
        });
        self.EntityNameSort;
        self.EntityName.subscribe(function (value) {
            self.EntityNameSort = value;
        });
        self.PageUrlSort;
        self.PageUrl.subscribe(function (value) {
            self.PageUrlSort = value;
        });
        self.HasWorkflowSort;
        self.HasWorkflow.subscribe(function (value) {
            self.HasWorkflowSort = value;
        });
        self.HasMenuItemSort;
        self.HasMenuItem.subscribe(function (value) {
            self.HasMenuItemSort = value;
        });
        
        self.ModuleNameSort;
        self.ModuleNameList.subscribe(function (value) {
            self.ModuleNameSort = value;
        });
        self.OrderNoSort;
        self.OrderNo.subscribe(function (value) {
            self.OrderNoSort = value.toString();
        });
        self.StatusSort;
        self.RecordStatus.subscribe(function (value) {
            self.StatusSort = value.toString();
        });
        self.cache = function () { };
        self.set(data);
    }

    var Privilege = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var SubModule = function (data) {
        var self = this;
     
         self.ModuleID = ko.observable(data ? data.ModuleID : "");
        self.ParentModuleName = ko.observable(data ? data.ParentModuleName : "");
        self.ModuleName = ko.observable(data.ModuleName + ' - ' + data.ParentModuleName);    
      
    }

    ipmsRoot.EntityModel = EntityModel;
    ipmsRoot.Privilege = Privilege;
    ipmsRoot.SubModule = SubModule;   
}(window.IPMSROOT));

IPMSROOT.EntityModel.prototype.set = function (data) {
    var self = this;
    self.EntityID(data ? (data.EntityID || "") : "");
    self.EntityCode(data ? (data.EntityCode || "") : "");
    self.EntityName(data ? (data.EntityName || "") : "");
    self.ModuleID(data ? (data.ModuleID || "") : "");
    self.ModuleName(data ? (data.ModuleName || "") : "");
    self.PageUrl(data ? (data.PageUrl || "") : "");
    self.OrderNo(data ? (data.OrderNo == 'NULL' ? "" : data.OrderNo || "") : "");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.HasWorkflow(data ? (data.HasWorkflow || "") : "");
    self.HasMenuItem(data ? (data.HasMenuItem || "") : "");
    self.ModuleNameList(data ? (data.ModuleNameList || "") : "");
    self.HasWorkflowStatus(data ? (data.HasWorkflowStatus || "") : "");
    self.HasMenuItemStatus(data ? (data.HasMenuItemStatus || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.EntityPrivileges(data ? (data.EntityPrivileges ? $.map(data.EntityPrivileges, function (item) { return item }) : []) : []);

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.EntityModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

