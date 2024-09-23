(function (ipmsRoot) {



    // Fuel Consumption Log ReferenceData Used For Fills The All Dropdowns in Carft Dtls form
    var FuelConsumptionLogReferenceData = function (data) {
        var self = this;
        self.CarftDtls = ko.observableArray(data ? $.map(data, function (item) { return new CarftDtl(item); }) : []);  //It will have Carft Dtls list under this port.  Only active Carft Dtls.
    }
    // Carft Details Model
    var CarftDtl = function (data) {
        var self = this;
        self.CraftID = ko.observable(data ? data.CraftID : "");
        self.CraftName = ko.observable(data ? data.CraftName : "");
        self.CraftType = ko.observable(data ? data.CraftType : "");
        self.CraftCode = ko.observable(data ? data.CraftCode : "");
        self.IMONo = ko.observable(data ? data.IMONo : "");
        self.FuelType = ko.observable(data ? data.FuelType : "");
        self.SubCategory = ko.observableArray(data ? data.SubCatName : "");
        self.CraftTypeName = ko.observable(data ? data.CraftType : "");
        self.FuelTypeName = ko.observable(data ? data.FuelType : "");
    }

    // Fuel consumption log dtl model
    var FuelConsumptionLogDtl = function (data) {
        var self = this;
        self.StartRunningHrs = ko.observable(data ? data.StartRunningHrs : "");
        self.RunningHours = ko.observable(data ? data.RunningHours : "");
        self.EndRunningHrs = ko.observable(data ? data.EndRunningHrs : "");
        //  self.AvgFuelConsumed = ko.observable(data ? data.AvgFuelConsumed : "");
        self.FuelType = ko.observable(data ? data.Crafts.FuelType : "");
        self.PreviousROB = ko.observable(data ? data.PreviousROB : 0);
        self.PresentROB = ko.observable(data ? data.PresentROB : 0);
        self.FuelReceived = ko.observable(data ? data.FuelReceived : 0);
        self.StartDateTime = ko.observable(data ? data.StartDateTime : "");
        self.EndDateTime = ko.observable(data ? data.EndDateTime : "");

        self.FuelConsumed = ko.computed(function () {
            var fuelconsumed = 0.0;

            if (parseFloat(self.FuelReceived()) >= 0 && parseFloat(self.PresentROB()) >= 0)
                fuelconsumed = parseFloat(parseFloat(self.PreviousROB()) + parseFloat(self.FuelReceived()) - parseFloat(self.PresentROB())).toFixed(2);

            return fuelconsumed;
        });

        self.AvgFuelConsumed = ko.computed(function () {
            var avgfuelconsumed = 0.0;

            if (parseFloat(self.FuelConsumed()) >= 0 && parseFloat(self.RunningHours()) > 0) {
                avgfuelconsumed = parseFloat(parseFloat(self.FuelConsumed()) / parseFloat(self.RunningHours())).toFixed(2);
            }
            return avgfuelconsumed;
        });

    }

    //Fuel Consumption Daily Log Fills from viewmodel to model  and send data to view and Validates cshtml Controls data
    var FuelConsumptionDailyLogModel = function (data) {
        var self = this;
        self.FuelConsumptionDailyLogID = ko.observable();
        self.Crafts = ko.observable(data ? new ipmsRoot.CarftDtl(data.Crafts) : new ipmsRoot.CarftDtl());
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.CraftID = ko.observable("");
        self.CraftCode = ko.observable("");
        self.CraftName = ko.observable(data ? data.CraftName : "").extend({ required: { message: '* Please Select the Craft Name' } });
        self.CraftType = ko.observable(data ? data.CraftType : "");
        self.IMONo = ko.observable(data ? data.IMONo : "");
        self.FuelType = ko.observable(data ? data.FuelType : "");
        self.PreviousROB = ko.observable(data ? data.PreviousROB : "").extend({ required: true });
        self.PresentROB = ko.observable(data ? data.PresentROB : "").extend({ required: true });
        self.StartDateTime = ko.observable(data ? data.StartDateTime : "").extend({ required: true }); // { message: '* Please Select the Start Running Hours' } });
        self.EndDateTime = ko.observable(data ? data.EndDateTime : "").extend({ required: true }); // { message: '* Please Select the End Running Hours' } });
        self.RunningHours = ko.observable(data ? data.RunningHours : "");
        self.AvgFuelConsumed = ko.observable(data ? data.AvgFuelConsumed : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
        self.Craftdtls = ko.observable(data ? data.Crafts : "");
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.FuelConsumed = ko.observable("");
        self.FuelTypeName = ko.observable("");
        self.CraftTypeName = ko.observable("");
        self.FuelReceived = ko.observable(data ? data.FuelReceived : "");
        self.StartRunningHrs = ko.observable(data ? data.StartRunningHrs : "").extend({ required: true });
        self.EndRunningHrs = ko.observable(data ? data.EndRunningHrs : "").extend({ required: true });

        self.FuelConsumed = ko.computed(function () {
            if (self.PresentROB() != "" && self.PreviousROB() != "") {
                if (self.FuelReceived() == "") {
                    self.FuelReceived("0");
                }
                //  if (self.FuelReceived() != null && self.PresentROB() != null) {
                if (parseFloat(self.FuelReceived()) >= 0 && parseFloat(self.PresentROB()) >= 0)
                    return (parseFloat(self.PreviousROB()) + parseFloat(self.FuelReceived()) - parseFloat(self.PresentROB())).toFixed(2);
                //   }
            }

        });

        self.AvgFuelConsumed = ko.computed(function () {
            var avgfuelconsumed = 0.0;

            if (parseFloat(self.FuelConsumed()) >= 0 && parseFloat(self.RunningHours()) > 0) {
                avgfuelconsumed = parseFloat(parseFloat(self.FuelConsumed()) / parseFloat(self.RunningHours())).toFixed(2);
            }
            return avgfuelconsumed;
        });
        self.PreviousFuelLogDetails = ko.observableArray(data ? data.PreviousFuelLogDetails : []);

        self.CraftCodeSort;
        self.CraftCode.subscribe(function (value) {
            self.CraftCodeSort = value;
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

        self.FuelTypeSort;
        self.FuelTypeName.subscribe(function (value) {
            self.FuelTypeSort = value;
        });

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "In Active";
        });

        self.cache = function () { };
        self.set(data);
    };
    ipmsRoot.FuelConsumptionLogReferenceData = FuelConsumptionLogReferenceData;
    ipmsRoot.FuelConsumptionDailyLogModel = FuelConsumptionDailyLogModel;
    ipmsRoot.CarftDtl = CarftDtl;
    ipmsRoot.FuelConsumptionLogDtl = FuelConsumptionLogDtl;

}(window.IPMSROOT));

//Fuel Consumption Daily Log Model Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.FuelConsumptionDailyLogModel.prototype.set = function (data) {
    var self = this;
    self.FuelConsumptionDailyLogID(data ? (data.FuelConsumptionDailyLogID || 0) : 0);
    self.PreviousROB(data ? (data.PreviousROB || "") : "");
    self.PresentROB(data ? (data.PresentROB || "") : "");
    self.StartDateTime(data ? (data.StartDateTime || "") : "");
    self.EndDateTime(data ? (data.EndDateTime || "") : "");
    self.StartRunningHrs(data ? (data.StartRunningHrs || "") : "");
    self.EndRunningHrs(data ? (data.EndRunningHrs || "") : "");
    self.RunningHours(data ? (data.RunningHours || "") : "");
    //  self.AvgFuelConsumed(data ? (data.AvgFuelConsumed || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.Crafts(data ? data.Crafts : "");
    self.CraftID(data ? data.CraftID : "");
    self.PortCode(data ? data.PortCode : "");
    self.CraftCode(data ? (data.Crafts != null ? data.Crafts.CraftCode : "") : "");
    self.CraftName(data ? (data.Crafts != null ? data.Crafts.CraftName : "") : "");

    self.CraftType(data ? (data.Crafts != null ? data.Crafts.CraftType : "") : "");
    self.IMONo(data ? (data.Crafts != null ? data.Crafts.IMONo : "") : "");
    self.FuelType(data ? data.Crafts.FuelType : "");
    self.CraftTypeName(data ? (data.Crafts != null ? data.Crafts.CraftTypeName : "") : "");
    self.FuelTypeName(data ? (data.Crafts != null ? data.Crafts.FuelTypeName : "") : "");
    self.FuelReceived(data ? (data.FuelReceived || "") : "");

    self.PreviousFuelLogDetails(data ? ko.utils.arrayMap(data.PreviousFuelLogDetails, function (commodity) {
        return new IPMSROOT.FuelConsumptionLogDtl(commodity);
    }) : null);


    self.cache.latestData = data;
}



IPMSROOT.FuelConsumptionDailyLogModel.prototype.reset = function () {
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


function allowOnlyThreePositiveDigts(el, evt) {
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
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 2)) {
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

