(function (ipmsRoot) {

    var CraftOutOfCommReferenceData = function (data) {
        var self = this;
        self.Craftss = ko.observableArray(data ? $.map(data.Craftss, function (item) { return new Crafts(item); }) : []);
        self.AllCraftss = ko.observableArray(data ? $.map(data.AllCraftss, function (item) { return new AllCrafts(item); }) : []);
        self.Reasonss = ko.observableArray(data ? $.map(data.Reasonss, function (item) { return new Reasons(item); }) : []);
        self.CommStatuss = ko.observableArray(data ? $.map(data.CommStatuss, function (item) { return new CommStatus(item); }) : []);
    }

    var Crafts = function (data) {
        var self = this;
        self.CraftID = ko.observable(data ? data.CraftID : "");
        self.CraftName = ko.observable(data ? data.CraftName : "");
        self.CraftCode = ko.observable(data ? data.CraftCode : "");
        self.CraftType = ko.observable(data ? data.CraftType : "");
    }

    var AllCrafts = function (data) {
        var self = this;
        self.CraftID = ko.observable(data ? data.CraftID : "");
        self.CraftName = ko.observable(data ? data.CraftName : "");
        self.CraftCode = ko.observable(data ? data.CraftCode : "");
        self.CraftType = ko.observable(data ? data.CraftType : "");
    }

    var Reasons = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var CommStatus = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var CraftOutOfCommissionModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
        self.CraftOutOfCommissionID = ko.observable("");
        self.CraftID = ko.observable("").extend({ required: true });
        self.CraftCode = ko.observable("");
        self.CraftName = ko.observable("");
        self.CraftType = ko.observable("");
        self.CraftTypeName = ko.observable("");        
        self.IMONo = ko.observable("");
        self.CraftCommissionStatus = ko.observable("").extend({ required: true });
        self.ReasonName = ko.observable("");        
        self.Reason = ko.observable("").extend({ required: true });
        self.Remarks = ko.observable("").extend({ required: true });
        self.ExpectedDuration = ko.observable("").extend({ required: true });
        self.CraftsList = ko.observableArray([]);
        self.CraftsList1 = ko.observableArray([]);
        self.ReasonsList = ko.observableArray([]);
        self.CommStatusList = ko.observableArray([]);
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.OutOfCommissionDate = ko.observable("");
        self.BackToCommissionDate = ko.observable("");

        self.CraftCodeSort;
        self.CraftCode.subscribe(function (value) {
            self.CraftCodeSort = value;
        });
        self.CraftIDSort;
        self.CraftID.subscribe(function (value) {
            self.CraftIDSort = value;
        });
        self.CraftNameSort;
        self.CraftName.subscribe(function (value) {
            self.CraftNameSort = value;
        });
        self.CraftTypeSort;
        self.CraftTypeName.subscribe(function (value) {
            self.CraftTypeSort = value;
        });
        self.IMONoSort;
        self.IMONo.subscribe(function (value) {
            self.IMONoSort = value;
        });
        self.ReasonSort;
        self.ReasonName.subscribe(function (value) {
            self.ReasonSort = value;
        });
        self.ExpectedDurationSort;
        self.ExpectedDuration.subscribe(function (value) {
            self.ExpectedDurationSort = value;
        });
        self.cache = function () { };
        self.set(data);
    }
    
    ipmsRoot.AllCrafts = AllCrafts;
    ipmsRoot.Crafts = Crafts;
    ipmsRoot.Reasons = Reasons;
    ipmsRoot.CommStatus = CommStatus;
    ipmsRoot.CraftOutOfCommReferenceData = CraftOutOfCommReferenceData;
    ipmsRoot.CraftOutOfCommissionModel = CraftOutOfCommissionModel;
}(window.IPMSROOT));

IPMSROOT.CraftOutOfCommissionModel.prototype.set = function (data) {
    var self = this;
    self.CraftOutOfCommissionID(data ? (data.CraftOutOfCommissionID || "") : "");
    self.CraftID(data ? (data.CraftID || "") : "");
    self.CraftCode(data ? (data.CraftCode || "") : "");
    self.CraftName(data ? (data.CraftName || "") : "");
    self.CraftType(data ? (data.CraftType || "") : "");
    self.CraftTypeName(data ? (data.CraftTypeName || "") : "");
    self.IMONo(data ? (data.IMONo || "") : "");
    self.Reason(data ? (data.Reason || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.ExpectedDuration(data ? (data.ExpectedDuration || "") : "");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy == 'NULL' ? "" : data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate == 'NULL' ? "" : data.ModifiedDate || "") : "");
    self.OutOfCommissionDate(data ? (data.OutOfCommissionDate == 'NULL' ? "" : data.OutOfCommissionDate || "") : "");
    self.BackToCommissionDate(data ? (data.BackToCommissionDate == 'NULL' ? "" : data.BackToCommissionDate || "") : "");
    self.RecordStatus("A");
    
    self.CraftCommissionStatus(data ? (data.CraftCommissionStatus || "OC") : "OC");
    self.ReasonName(data ? (data.ReasonName || "") : "");
    
    self.CraftsList(data ? (data.CraftsList ? $.map(data.CraftsList, function (item) { return item }) : []) : []);
    self.ReasonsList(data ? (data.ReasonsList ? $.map(data.Reasons, function (item) { return item }) : []) : []);
    self.CommStatusList(data ? (data.CommStatusList ? $.map(data.CommStatus, function (item) { return item }) : []) : []);
    self.CraftsList1(data ? (data.CraftsList1 ? $.map(data.Crafts, function (item) { return item }) : []) : []);

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.CraftOutOfCommissionModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9.]/;

    return charcheck.test(keychar);
}

function allowOnlyTwoPositiveDigts(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

//thanks: http://javascript.nwbox.com/cursor_position/
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}

