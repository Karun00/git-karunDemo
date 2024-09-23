(function (ipmsRoot) {


    var FuelRequisitionReferenceData = function (data) {
        var self = this;
        self.OilTypes = ko.observableArray(data ? $.map(data.OilTypes, function (item) { return new OilType(item); }) : []);
        self.GradeTypes = ko.observableArray(data ? $.map(data.GradeTypes, function (item) { return new GradeType(item); }) : []);
        self.UOMTypes = ko.observableArray(data ? $.map(data.UOMTypes, function (item) { return new UOMType(item); }) : []);

    }

    var OilType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    var GradeType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    var UOMType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    
    var FuelRequisitionModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);
        self.FuelRequisitionID = ko.observable();
        self.FuelRequistionNo = ko.observable("");
        self.CraftID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Craft Name' } });
        self.OilTypeCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select OilType' } });
        self.FuelRequisitionID = ko.observable("");
        self.PortCode = ko.observable("");
        self.GradeCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Grade' } });
        self.UOMCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select UOM' } });
        self.Quantity = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.RequiredDate = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Required Date' } });
        self.RequisitionDate = ko.observable("");
        self.Remarks = ko.observable("");
        self.CraftCode = ko.observable("");
        self.CraftName = ko.observable("");
        self.CraftType = ko.observable("");
        self.IMONo = ko.observable("");
        self.OwnersName = ko.observable("");
        self.OilType = ko.observable();
        self.RequisitionStatus = ko.observable();
        self.Grade = ko.observable("");
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.ReceiptStatus = ko.observable("");
        self.WorkflowInstanceId = ko.observable();
        self.startDate = ko.observable();
        self._LoginName = ko.observable();
        self.FuelReceiptStatus = ko.observable();
        self.WorkFlowRemarks = ko.observable();
        self.FuelRequistionType = ko.observable();
        //self.Statust = ko.computed(function () {
        //    return self.RecordStatus() == 'A' ? "Active" : "In Active";
        //});



        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.RequisitionNoSort;
        self.FuelRequistionNo.subscribe(function (value) {
            self.RequisitionNoSort = value;
        });
        self.CraftNameSort;
        self.CraftName.subscribe(function (value) {
            self.CraftNameSort = value;
        });
        self.CraftTypeSort;
        self.CraftType.subscribe(function (value) {
            self.CraftTypeSort = value;
        });
        self.IMOSort;
        self.IMONo.subscribe(function (value) {
            self.IMOSort = value;
        });
        self.OilTypeSort;
        self.OilType.subscribe(function (value) {
            self.OilTypeSort = value;
        });

        self.QuantitySort;
        self.Quantity.subscribe(function (value) {
            self.QuantitySort = value;
        });
        self.DateRequiredSort;
        self.RequiredDate.subscribe(function (value) {
            self.DateRequiredSort = value;
        });
        self.RequisitionDateSort;
        self.RequisitionDate.subscribe(function (value) {
            self.RequisitionDateSort = value;
        });
        self.RequisitionStatusSort;
        self.RequisitionStatus.subscribe(function (value) {
            self.RequisitionStatusSort = value;
        });
        self.CraftCodeSort;
        self.CraftCode.subscribe(function (value) {
            self.CraftCodeSort = value;
        });
        self.ReceiptStatusSort;
        self.FuelReceiptStatus.subscribe(function (value) {
            self.ReceiptStatusSort = value;
        });

        self.EditPending = ko.computed(function () {
            if (self.RequisitionStatus() == "Approve") {
                return false;
            }
            else if (self.RequisitionStatus() == "Reject") {
                return false;
            }
            else {
                return true;
            }
        });

        self.RequisitionStatust = ko.computed(function () {
            return self.RequisitionStatus() == 'Approve' ? "Approved" : self.RequisitionStatus() == 'Reject' ? "Rejected" : self.RequisitionStatus() == 'Update Request' ? "Update Request" : "New Request";
        });


        self.cache = function () { };
        self.set(data);
    }

    var pendingTask = function (data) {

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Enter Remarks' } });
        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    ipmsRoot.pendingTask = pendingTask;

    ipmsRoot.FuelRequisitionModel = FuelRequisitionModel;
    ipmsRoot.FuelRequisitionReferenceData = FuelRequisitionReferenceData;
    ipmsRoot.OilType = OilType;
    ipmsRoot.GradeType = GradeType;
    ipmsRoot.UOMType = UOMType;

}(window.IPMSROOT));

IPMSROOT.FuelRequisitionModel.prototype.set = function (data) {
    var self = this;
    self.FuelRequisitionID(data ? (data.FuelRequisitionID || "") : "");
    self.FuelRequistionNo(data ? (data.FuelRequistionNo || "") : "");
    self.CraftID(data ? (data.CraftID == 'NULL' ? "" : data.CraftID || "") : "");
    self.CraftCode(data ? (data.CraftCode || "") : "");
    self.CraftName(data ? (data.CraftName || "") : "");
    self.CraftType(data ? (data.CraftType || "") : "");
    self.IMONo(data ? (data.IMONo || "") : "");
    self.OilTypeCode(data ? (data.OilTypeCode || "") : "");
    self.OilType(data ? (data.OilType || "") : "");
    self.GradeCode(data ? (data.GradeCode || "") : "");
    self.Grade(data ? (data.Grade || "") : "");
    self.UOMCode(data ? (data.UOMCode || "") : "");
    //   self.UOMName(data ? (data.UOMName || "") : "");    
    self.RequisitionStatus(data ? (data.RequisitionStatus || "") : "");
    //  self.Quantity(data ? (data.Quantity || "") : "");
    self.Quantity(data ? (data.Quantity == 0 ? 0 : data.Quantity || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.RequiredDate(data ? (moment(data.RequiredDate).format('YYYY-MM-DD') || "") : "");
    self.RequisitionDate(data ? (moment(data.RequisitionDate).format('YYYY-MM-DD') || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.ReceiptStatus(data ? (data.ReceiptStatus || "") : "");
    self.WorkflowInstanceId(data ? (data.WorkflowInstanceId || null) : null);
    self.OwnersName(data ? (data.OwnersName || "") : "");
    self.FuelReceiptStatus(data ? (data.FuelReceiptStatus || "") : "");
    self.FuelRequistionType(data ? (data.FuelRequistionType || "") : "");
    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.FuelRequisitionModel.prototype.reset = function () {
    this.set(this.cache.latestData);
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



