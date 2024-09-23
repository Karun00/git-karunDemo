(function (ipmsRoot) {

    var BudgetedValuesModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(true);

        self.FinancialYearID = ko.observable();
        self.StartDate = ko.observable();
        self.EndDate = ko.observable();
        self.IsCurrentFinancialYear = ko.observable();
        self.BudgetedValuesFYDescription = ko.observable();
        self.FinancialYear = ko.observable();
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.BudgetedValuesVO = ko.observableArray();

        self.isEditVisible = ko.observable();
        self.isEditVisible = ko.computed(function () {
            if (self.IsCurrentFinancialYear() == 'N') {
                return false;
            }
            else {
                return true;
            }
        }, this);

        self.FinancialYearSort;
        self.FinancialYear.subscribe(function (value) {
            self.FinancialYearSort = value;
        });

        self.BudgetedValuesFYDescriptionSort;
        self.BudgetedValuesFYDescription.subscribe(function (value) {
            self.BudgetedValuesFYDescriptionSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }

    var BudgetedValues = function (data) {

        var self = this;
        self.validationEnabled = ko.observable(true);

        self.BudgetedValuesID = ko.observable(data ? data.BudgetedValuesID : 0);
        self.FinancialYearID = ko.observable(data ? data.FinancialYearID : 0);
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
        self.VolumesContainers = ko.observable(data ? data.VolumesContainers : "");
        self.VolumesRBCT = ko.observable(data ? data.VolumesRBCT : "");
        self.VolumesDryBulk = ko.observable(data ? data.VolumesDryBulk : "");
        self.VolumesBreakBulk = ko.observable(data ? data.VolumesBreakBulk : "");
        self.MovementsContainers = ko.observable(data ? data.MovementsContainers : "");
        self.MovementsRBCT = ko.observable(data ? data.MovementsRBCT : "");
        self.MovementsDryBulk = ko.observable(data ? data.MovementsDryBulk : "");
        self.MovementsBreakBulk = ko.observable(data ? data.MovementsBreakBulk : "");
        self.STATContainers = ko.observable(data ? data.STATContainers : "");
        self.STATRBCT = ko.observable(data ? data.STATRBCT : "");
        self.STATDryBulk = ko.observable(data ? data.STATDryBulk : "");
        self.STATBreakBulk = ko.observable(data ? data.STATBreakBulk : "");
        self.TotalArrivals = ko.observable(data ? data.TotalArrivals : "");
        self.TotalGT = ko.observable(data ? data.TotalGT : "");

        self.TotalArrivals = ko.observable(data ? data.TotalArrivals : "");
        self.TotalGT = ko.observable(data ? data.TotalGT : "");

        self.TotalPilotDelays = ko.observable(data ? data.TotalPilotDelays : "");
        self.TotalTugDelays = ko.observable(data ? data.TotalTugDelays : "");
        self.TotalBerthingDelays = ko.observable(data ? data.TotalBerthingDelays : "");
        self.TotalTugAvailability = ko.observable(data ? data.TotalTugAvailability : "");
        self.TotalTugUtilization = ko.observable(data ? data.TotalTugUtilization : "");

        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? (data.CreatedBy == null ? "" : data.CreatedBy || "") : "");
        self.CreatedDate = ko.observable(data ? (data.CreatedDate == null ? "" : data.CreatedDate || "") : "");
        self.ModifiedBy = ko.observable(data ? (data.ModifiedBy == null ? "" : data.ModifiedBy || "") : "");
        self.ModifiedDate = ko.observable(data ? (data.ModifiedDate == null ? "" : data.ModifiedDate || "") : "");
    }

    ipmsRoot.BudgetedValuesModel = BudgetedValuesModel;
    ipmsRoot.BudgetedValues = BudgetedValues;
}(window.IPMSROOT));

IPMSROOT.BudgetedValuesModel.prototype.set = function (data) {
    var self = this;

    self.FinancialYearID(data ? (data.FinancialYearID || null) : null);
    self.FinancialYear(data ? (data.FinancialYear || "") : "");
    self.StartDate(data ? (data.StartDate || "") : "");
    self.EndDate(data ? (data.EndDate || "") : "");
    self.IsCurrentFinancialYear(data ? (data.IsCurrentFinancialYear || "") : "");
    self.BudgetedValuesFYDescription(data ? (data.BudgetedValuesFYDescription || "") : "");

    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == null ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == null ? "" : data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy == null ? "" : data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate == null ? "" : data.ModifiedDate || "") : "");

    self.BudgetedValuesVO(data ? ko.utils.arrayMap(data.BudgetedValuesVO, function (result) {
        return new IPMSROOT.BudgetedValues(result);
    }) : []);


    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.BudgetedValuesModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

//function for Allow Only Two Positive Digits For Decimal Value
function AllowOnlyTwoPositiveDigitsForDecimal(el, evt) {

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
    var caratPos = GetSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

//thanks: http://javascript.nwbox.com/cursor_position/
function GetSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}