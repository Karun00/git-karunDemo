(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    // CraftReferenceData Used For Fills The All Dropdowns
    var CraftReferenceData = function (data) {
        var self = this;
        self.PortOfRegistrys = ko.observableArray(data ? $.map(data.PortOfRegistry, function (item) { return new PortOfRegistry(item); }) : []);
        self.CraftTypes = ko.observableArray(data ? $.map(data.CraftType, function (item) { return new CraftType(item); }) : []);
        self.CraftCommissionStatuss = ko.observableArray(data ? $.map(data.CraftCommissionStatus, function (item) { return new CraftCommissionStatus(item); }) : []);
        self.PropulsionTypes = ko.observableArray(data ? $.map(data.PropulsionType, function (item) { return new PropulsionType(item); }) : []);
        self.ClassificationSocietys = ko.observableArray(data ? $.map(data.ClassificationSociety, function (item) { return new ClassificationSociety(item); }) : []);
        self.CraftNationalitys = ko.observableArray(data ? $.map(data.CraftNationality, function (item) { return new CraftNationality(item); }) : []);
        self.FuelTypes = ko.observableArray(data ? $.map(data.FuelType, function (item) { return new FuelType(item); }) : []);
        self.EngineTypes = ko.observableArray(data ? $.map(data.EngineType, function (item) { return new EngineType(item); }) : []);
    }

    //PortOfRegistryModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var PortOfRegistry = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //CraftTypeModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var CraftType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //CraftCommissionStatusModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var CraftCommissionStatus = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //PropulsionTypeModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var PropulsionType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //ClassificationSocietyModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var ClassificationSociety = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //CraftNationalityModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var CraftNationality = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //FuelTypeModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var FuelType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //EngineTypeModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var EngineType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    //CraftModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var CraftModel = function (data) {

        var self = this;

        self.validationEnabled = ko.observable(true);

        //For Color Code
        //self.colorPalette = ko.observable("basic");
        //self.selectedChoice = ko.observable("#ffffff");
        ///

        self.CraftID = ko.observable();
        self.CraftCode = ko.observable("").extend({ required: true });
        self.IMONo = ko.observable("").extend({ onlyIf: self.validationEnabled, required: { message: '* This field is required.' } });
        self.CraftName = ko.observable("").extend({ required: true });
        self.CallSign = ko.observable("").extend({ required: true });
        self.ExCallSign = ko.observable();
        self.CraftType = ko.observable("").extend({ required: true });
        self.CraftBuildDate = ko.observable("");
        self.DateOfDelivery = ko.observable();
        self.CraftNationality = ko.observable("").extend({ required: true });
        self.ClassificationSociety = ko.observable("");
        self.CommissionDate = ko.observable();
        self.AFCInMetricTon = ko.observable("").extend({ required: true });
        self.FuelType = ko.observable("").extend({ required: true });
        self.PortOfRegistry = ko.observable();
        self.EnginePower = ko.observable();
        self.EngineType = ko.observable();
        self.PropulsionType = ko.observable();
        self.NoOfPropellers = ko.observable();
        self.MaxManeuveringSpeed = ko.observable("");
        self.BeamM = ko.observable("").extend({ required: true });
        self.RegisteredLengthM = ko.observable("").extend({ required: true });
        self.ForwardDraftM = ko.observable("").extend({ required: true });
        self.AftDraftM = ko.observable("").extend({ required: true });
        self.GrossRegisteredTonnageMT = ko.observable("").extend({ required: true });
        self.NetRegisteredTonnageMT = ko.observable("").extend({ required: true });
        self.DeadWeightTonnageMT = ko.observable("");
        self.BollardPullMT = ko.observable("");
        self.OwnersName = ko.observable("").extend({ required: true });
        self.Address = ko.observable();
        self.PhoneNumber = ko.observable();
        self.EmailID = ko.observable(data ? data.EmailID : "").extend({ maxLength: 50, email: { onlyIf: self.validationEnabled, message: '* Please enter valid email address' }, });

        self.InitialFuelQuantityMT = ko.observable();
        self.LOROBMT = ko.observable();
        self.HYDROBMT = ko.observable();
        self.FreshwaterROBMT = ko.observable();
        self.CraftCommissionStatus = ko.observable("").extend({ required: true });
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        // Modified by Srini, on 10th Feb 2015, UAT Issues - Craft grid does not sort on Status. There is no filter option on this field ('Status' field).
        self.Statust = ko.computed(function () {
           // return self.RecordStatus() == 'I' ? "Inactive" : "Active";
            var status = "";
            if (self.RecordStatus() == 'I') {
                status = "Inactive";
            }
            if (self.RecordStatus() == 'A') {
                status = "Active";
            }
            return status;
        });

        self.CraftNameSort;
        self.CraftName.subscribe(function (value) {
            self.CraftNameSort = value;
        });

        self.CraftCodeSort;
        self.CraftCode.subscribe(function (value) {
            self.CraftCodeSort = value;
        });

        self.IMONoSort;
        self.IMONo.subscribe(function (value) {
            self.IMONoSort = value;
        });

        self.CallSignSort;
        self.CallSign.subscribe(function (value) {
            self.CallSignSort = value;
        });

        self.StatusSort;
        self.Statust.subscribe(function (value) {
            self.StatusSort = value;
        });

        self.DredgerColorCode = ko.observable();

        $("#PhoneNumber").kendoMaskedTextBox({ mask: "000-000-0000" });

        self.cache = function () { };
        self.set(data);
    }

    ipmsRoot.CraftReferenceData = CraftReferenceData;
    ipmsRoot.CraftModel = CraftModel;
    ipmsRoot.PortOfRegistry = PortOfRegistry;
    ipmsRoot.CraftType = CraftType;
    ipmsRoot.CraftCommissionStatus = CraftCommissionStatus;
    ipmsRoot.PropulsionType = PropulsionType;
    ipmsRoot.ClassificationSociety = ClassificationSociety;
    ipmsRoot.CraftNationality = CraftNationality;
    ipmsRoot.FuelType = FuelType;
    ipmsRoot.EngineType = EngineType;
}(window.IPMSROOT));

//CraftModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.CraftModel.prototype.set = function (data) {
    var self = this;
    self.CraftID(data ? (data.CraftID || "") : "");
    self.CraftCode(data ? (data.CraftCode || "") : "");
    self.IMONo(data ? (data.IMONo || "") : "");
    self.CraftName(data ? (data.CraftName || "") : "");
    self.CallSign(data ? (data.CallSign || "") : "");
    self.ExCallSign(data ? (data.ExCallSign || "") : "");
    self.CraftType(data ? (data.CraftType || "") : "");
    self.CraftBuildDate(data ? (data.CraftBuildDate || "") : "");
    self.DateOfDelivery(data ? (data.DateOfDelivery || "") : "");
    self.CraftNationality(data ? (data.CraftNationality || "") : "");
    self.ClassificationSociety(data ? (data.ClassificationSociety || "") : "");
    self.CommissionDate(data ? (data.CommissionDate || "") : "");
    self.AFCInMetricTon(data ? (data.AFCInMetricTon || "") : "");
    self.FuelType(data ? (data.FuelType || "") : "");
    self.PortOfRegistry(data ? (data.PortOfRegistry || "") : "");
    self.EnginePower(data ? (data.EnginePower || "") : "");
    self.EngineType(data ? (data.EngineType || "") : "");
    self.PropulsionType(data ? (data.PropulsionType || "") : "");
    self.NoOfPropellers(data ? (data.NoOfPropellers || "") : "");
    self.MaxManeuveringSpeed(data ? (data.MaxManeuveringSpeed || "") : "");
    self.BeamM(data ? (data.BeamM || "") : "");
    self.RegisteredLengthM(data ? (data.RegisteredLengthM || "") : "");
    self.ForwardDraftM(data ? (data.ForwardDraftM || "") : "");
    self.AftDraftM(data ? (data.AftDraftM || "") : "");
    self.GrossRegisteredTonnageMT(data ? (data.GrossRegisteredTonnageMT || "") : "");
    self.NetRegisteredTonnageMT(data ? (data.NetRegisteredTonnageMT || "") : "");
    self.DeadWeightTonnageMT(data ? (data.DeadWeightTonnageMT || "") : "");
    self.BollardPullMT(data ? (data.BollardPullMT || "") : "");
    self.OwnersName(data ? (data.OwnersName || "") : "");
    self.Address(data ? (data.Address || "") : "");
    self.PhoneNumber(data ? (data.PhoneNumber || "") : "");
    self.EmailID(data ? (data.EmailID || "") : "");
    self.InitialFuelQuantityMT(data ? (data.InitialFuelQuantityMT || "") : "");
    self.LOROBMT(data ? (data.LOROBMT || "") : "");
    self.HYDROBMT(data ? (data.HYDROBMT || "") : "");
    self.FreshwaterROBMT(data ? (data.FreshwaterROBMT || "") : "");
    self.CraftCommissionStatus(data ? (data.CraftCommissionStatus || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");

    self.DredgerColorCode(data ? (data.DredgerColorCode || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.CraftModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$/;
    return charcheck.test(keychar);
}

//Accept only numeric 
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
}

//Accept only Alphabets and spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z ]*$/;
    return charcheck.test(keychar);
}

//Accept Alpha numeric
function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    return charcheck.test(keychar);
}

//Allow onlu Two Positive Digits
function ValidateDecimalValue(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^\s*-?[1-9]\d*(\.\d{1,2})?\s*$/;
    return charcheck.test(keychar);
}