(function (ipmsRoot) {    

    var SuppDockUnDockTimeModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(false);
        self.SuppServiceRequestID = ko.observable();
        self.VesselData = ko.observable();
        self.IsValidationEnabled = ko.observable(true);
        self.Chamber = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.LocationID = ko.observable("");
        self.RequestDate = ko.observable();
        self.DGStatus = ko.observable();
        self.PermitStatus = ko.observable();
        self.WorkflowInstanceID = ko.observable();
        self.ServiceTypeName = ko.observable("");
        self.ServiceType = ko.observable("");
        self.DockPortCode = ko.observable("");
        self.BerthName = ko.observable();
        self.FromDate = ko.observable();
        self.ToDate = ko.observable();
        self.PermitNo = ko.observable("");     
    
        self.LocationName = ko.observable();
        self.RecordStatus = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();

        self.BarkeelCode = ko.observable();
        self.CargoTons = ko.observable();
        self.Ballast = ko.observable();
        self.Bunkers = ko.observable();
        self.ExtensionDateTime = ko.observable();
        self.Remarks = ko.observable();
        self.RecordStatus = ko.observable();
        self.TermsandConditions = ko.observable();


        self.EnteredDockDateTime = ko.observable();
        self.OnBlocksDateTime = ko.observable();
        self.DryDockDateTime = ko.observable();
        self.FinishedDockDateTime = ko.observable();
        self.OffBlocksDateTime = ko.observable();
        self.LeftDockDateTime = ko.observable();

        self.startDate = ko.observable();
        self.endDate = ko.observable();

        self.SuppDryDockID = ko.observable();

        self.DockQuayCode = ko.observable();
        self.DockBerthCode = ko.observable();
        self.ScheduleFromDate = ko.observable();
        self.ScheduleToDate = ko.observable();
        self.ScheduleStatus = ko.observable();
        

        self.AnyDangerousGoodsonBoard = ko.observable();
        self.DangerousGoodsClass = ko.observable();
        self.UNNo = ko.observable();

        self.PermitStatust = ko.computed(function () {
            var isDryDockDateTime = true;
            var isLeftDockDateTimee = true;
            
            if (self.DryDockDateTime() == "Invalid date" || self.DryDockDateTime() == undefined) {
                isDryDockDateTime = false;
            }
            if (self.LeftDockDateTime() == "Invalid date" || self.LeftDockDateTime() == undefined) {
                isLeftDockDateTimee = false;
            }
            if ((isDryDockDateTime == false) && (isLeftDockDateTimee == false)) {
                return "New";
            }
            if ((isDryDockDateTime == true) && (isLeftDockDateTimee == false)) {
                return "Docked";
            }
            if ((isDryDockDateTime == true) && (isLeftDockDateTimee == true)) {

                return "Undocked";
            }

            //if ((isDryDockDateTime == true) && (isLeftDockDateTimee == false)) {
            //    return "Pending";
            //}
            //if ((isDryDockDateTime == true) && (isLeftDockDateTimee == true)) {
                
            //    return "Submitted";
            //}
        });

       

        self.Status = ko.observable();

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });
        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });
        self.ServiceTypeSort;
        self.ServiceTypeName.subscribe(function (value) {
            self.ServiceTypeSort = value;
        });

        self.RequestDtAndTimeSort;
        self.FromDate.subscribe(function (value) {
            self.RequestDtAndTimeSort = value;
        });

        self.ToDateSort;
        self.ToDate.subscribe(function (value) {
            self.ToDateSort = value;
        });


        self.PermitStatusSort;
        self.PermitStatust.subscribe(function (value) {
            self.PermitStatusSort = value;
        });


        
      

        self.cache = function () { };
        self.set(data);
    }


    ipmsRoot.SuppDockUnDockTimeModel = SuppDockUnDockTimeModel;



}(window.IPMSROOT));

IPMSROOT.SuppDockUnDockTimeModel.prototype.set = function (data) {
    var self = this;
    self.SuppServiceRequestID(data ? (data.SuppServiceRequestID || '') : '');
    self.SuppDryDockID(data ? (data.SuppDryDockID || '') : '');
    self.VCN(data ? data.VCN || "" : "");
    self.VesselName(data ? data.VesselName : "");

    self.BerthName(data ? data.BerthName : "");
    self.BarkeelCode(data ? data.BarkeelCode : "N");
    self.CargoTons(data ? data.CargoTons : null);
    self.Ballast(data ? data.Ballast : null);
    self.Bunkers(data ? data.Bunkers : null);
    self.ExtensionDateTime(data ? data.ExtensionDateTime : null);
    self.Remarks(data ? data.Remarks : "");
    self.RecordStatus(data ? data.RecordStatus : "A");

    self.Chamber(data ? data.Chamber : "");
    //self.EnteredDockDateTime(data ? data.EnteredDockDateTime : null);
    //self.OnBlocksDateTime(data ? data.OnBlocksDateTime : null);
    //self.DryDockDateTime(data ? data.DryDockDateTime : null);
    //self.FinishedDockDateTime(data ? data.FinishedDockDateTime : null);
    //self.OffBlocksDateTime(data ? data.OffBlocksDateTime : null);
    //self.LeftDockDateTime(data ? data.LeftDockDateTime : null);

    self.EnteredDockDateTime(data ? (moment(data.EnteredDockDateTime).format('YYYY-MM-DD HH:mm') || "") : null);
    self.OnBlocksDateTime(data ? (moment(data.OnBlocksDateTime).format('YYYY-MM-DD HH:mm') || "") : null);
    self.DryDockDateTime(data ? (moment(data.DryDockDateTime).format('YYYY-MM-DD HH:mm') || "") : null);
    self.FinishedDockDateTime(data ? (moment(data.FinishedDockDateTime).format('YYYY-MM-DD HH:mm') || "") : null);
    self.OffBlocksDateTime(data ? (moment(data.OffBlocksDateTime).format('YYYY-MM-DD HH:mm') || "") : null);
    self.LeftDockDateTime(data ? (moment(data.LeftDockDateTime).format('YYYY-MM-DD HH:mm') || "") : null); 
   
    self.DockPortCode(data ? data.DockPortCode : "");
    self.DockQuayCode(data ? data.DockQuayCode : null);
    self.DockBerthCode(data ? data.DockBerthCode : null);
    self.ScheduleFromDate(data ? (moment(data.ScheduleFromDate).format('YYYY-MM-DD HH:mm') || "") : null);
    self.ScheduleToDate(data ? (moment(data.ScheduleToDate).format('YYYY-MM-DD HH:mm') || "") : null);
    //self.ScheduleFromDate(data ? data.ScheduleFromDate : null);
    //self.ScheduleToDate(data ? data.ScheduleToDate : null);
    self.ScheduleStatus(data ? data.ScheduleStatus : null);

    self.ServiceTypeName(data ? data.ServiceTypeName : "");

    
    self.FromDate(data ? data.FromDate : "");
    self.ToDate(data ? data.ToDate : "");
  
    self.WorkflowInstanceID(data ? (data.WorkflowInstanceID || null) : null);

   
  

    self.CreatedBy(data ? data.CreatedBy : '');
    self.CreatedDate(data ? data.CreatedDate : "");
    self.ModifiedBy(data ? data.ModifiedBy : '');
    self.ModifiedDate(data ? data.ModifiedDate : "");

     


    self.cache.latestData = data;

}

IPMSROOT.SuppDockUnDockTimeModel.prototype.reset = function () {
    this.set(this.cache.latestData);
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