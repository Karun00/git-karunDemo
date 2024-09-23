(function (ipmsRoot) {
    var ResourceGroupModel = function (data) {

        var self = this;
        self.validationEnabled = ko.observable(true);
        self.ResourceGroupID = ko.observable();
        self.ResourceGroupCode = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });
        self.PortCode = ko.observable();
        self.ResourceGroupName = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });
        self.Position = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });

        self.ResourceEmpList = ko.observableArray([]);
        self.ResourceEmployeeGroups = ko.observableArray([]);
        self.Designation = ko.observable();
        self.EmployeeID = ko.observable();
        self.DesignationCode = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.Status = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.ResourceGroupCodeSort;
        self.ResourceGroupCode.subscribe(function (value) {
            self.ResourceGroupCodeSort = value;
        });

        self.ResourceGroupNameSort;
        self.ResourceGroupName.subscribe(function (value) {
            self.ResourceGroupNameSort = value;
        });

        self.DesignationSort;
        self.Designation.subscribe(function (value) {
            self.DesignationSort = value;
        });

        self.cache = function () { };
        self.set(data);
    };

    ipmsRoot.ResourceGroupModel = ResourceGroupModel;

}(window.IPMSROOT));

IPMSROOT.ResourceGroupModel.prototype.set = function (data) {
    var self = this;

    self.ResourceGroupID(data ? (data.ResourceGroupID || "") : "");
    self.ResourceGroupCode(data ? (data.ResourceGroupCode || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.ResourceGroupName(data ? (data.ResourceGroupName || "") : "");
    self.Position(data ? (data.Position || "") : "");
    self.RecordStatus(data ? data.RecordStatus : "A");
    self.ResourceEmployeeGroups(data ? (data.ResourceEmployeeGroups || "") : "");

    self.DesignationCode(data ? (data.DesignationCode || "") : "");

    var emp = (data ? (data.ResourceEmployeeGroups.length || "") : "");
    var empgrps = [];

    for (i = 0; i < emp; i++) {
        empgrps.push(data.ResourceEmployeeGroups[i].EmployeeID);
    }
    self.ResourceEmpList(empgrps ? (empgrps || "") : "");
    self.Designation(data ? (data.Designation || "") : "");
    self.CreatedBy(data ? data.CreatedBy : '');
    self.CreatedDate(data ? data.CreatedDate : "");

    self.cache.latestData = data;
}

IPMSROOT.ResourceGroupModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


