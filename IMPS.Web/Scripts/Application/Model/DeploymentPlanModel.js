(function (ipmsRoot) {

    var DeploymentPlanReferenceData = function (data) {
        var self = this;
        self.FinancialYears = ko.observableArray(data ? $.map(data.FinancialYears, function (item) { return new FinancialYear(item); }) : []);
        self.DredgingColors = ko.observableArray(data ? $.map(data.DredgingColors, function (item) { return new DredgingColor(item); }) : []);
        self.DredgingTypes = ko.observableArray(data ? $.map(data.DredgingTypes, function (item) { return new DredgingType(item); }) : []);
        self.PortTypes = ko.observableArray(data ? $.map(data.PortTypes, function (item) { return new PortType(item); }) : []);
        self.CraftColors = ko.observableArray(data ? $.map(data.CraftColors, function (item) { return new CraftColor(item); }) : []);
    }

    var FinancialYear = function (data) {
     var self = this;
    self.FinancialYearID = ko.observable(data ? data.FinancialYearID : "");
    self.FinancialYear = ko.observable(data ? data.FinancialYear : "");
    }

    var DredgingColor = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    
    var DredgingType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    var PortType = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }
    var CraftColor = function (data) {
        var self = this;
        self.ConfigValue = ko.observable(data ? data.ConfigValue : "");
        //self.PortName = ko.observable(data ? data.PortName : "");
    }
    var DeploymentPlanType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

        self.Budget = ko.observable(data ? data.Budget : "");
        self.DredgPlan = ko.observable(data ? data.DredgPlan : "");
        self.Apr = ko.observable(data ? data.Apr : "");
        self.May = ko.observable(data ? data.May : "");
        self.Jun = ko.observable(data ? data.Jun : "");
        self.Jul = ko.observable(data ? data.Jul : "");
        self.Aug = ko.observable(data ? data.Aug : "");
        self.Sep = ko.observable(data ? data.Sep : "");
        self.Oct = ko.observable(data ? data.Oct : "");
        self.Nov = ko.observable(data ? data.Nov : "");
        self.Dec = ko.observable(data ? data.Dec : "");
        self.Jan = ko.observable(data ? data.Jan : "");
        self.Feb = ko.observable(data ? data.Feb : "");
        self.Mar = ko.observable(data ? data.Mar : "");

        self.AprCraftID = ko.observable(data ? data.AprCraftID : "");
        self.MayCraftID = ko.observable(data ? data.MayCraftID : "");
        self.JunCraftID = ko.observable(data ? data.JunCraftID : "");
        self.JulCraftID = ko.observable(data ? data.JulCraftID : "");
        self.AugCraftID = ko.observable(data ? data.AugCraftID : "");
        self.SepCraftID = ko.observable(data ? data.SepCraftID : "");
        self.OctCraftID = ko.observable(data ? data.OctCraftID : "");
        self.NovCraftID = ko.observable(data ? data.NovCraftID : "");
        self.DecCraftID = ko.observable(data ? data.DecCraftID : "");
        self.JanCraftID = ko.observable(data ? data.JanCraftID : "");
        self.FebCraftID = ko.observable(data ? data.FebCraftID : "");
        self.MarCraftID = ko.observable(data ? data.MarCraftID : "");

        self.AprColor = ko.observable(data ? data.AprColor : "");
        self.MayColor = ko.observable(data ? data.MayColor : "");
        self.JunColor = ko.observable(data ? data.JunColor : "");
        self.JulColor = ko.observable(data ? data.JulColor : "");
        self.AugColor = ko.observable(data ? data.AugColor : "");
        self.SepColor = ko.observable(data ? data.SepColor : "");
        self.OctColor = ko.observable(data ? data.OctColor : "");
        self.NovColor = ko.observable(data ? data.NovColor : "");
        self.DecColor = ko.observable(data ? data.DecColor : "");
        self.JanColor = ko.observable(data ? data.JanColor : "");
        self.FebColor = ko.observable(data ? data.FebColor : "");
        self.MarColor = ko.observable(data ? data.MarColor : "");

     
    }



    var DeploymentPlanModel = function (data, masterDeploymentPlanTypes) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.DeploymentPlanID = ko.observable("");
       // self.FinancialYearCode = ko.observable("");        
       // self.FinancialYearName = ko.observable("");
        self.PortCode = ko.observable("").extend({ CodeUnique: self.PortCode, required: { onlyIf: self.validationEnabled, message: '* Please Select the Port Name' } });
        self.PortName = ko.observable("");

        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");

        self.FinancialYearID = ko.observable("").extend({ CodeUnique: self.FinancialYearID, required: { onlyIf: self.validationEnabled, message: '* Please Select the Financial Year' } });
        self.DateName = ko.observable("");
        self.StartDate = ko.observable("");
        self.EndDate = ko.observable("");
        self.FinancialYearDate = ko.observable("");
       // self.Description = ko.observable("").extend({ CodeUnique: self.Description, required: true}); //{ onlyIf: self.validationEnabled, message: '* Please Enter the Description' } });
        self.Description = ko.observable("").extend({ CodeUnique: self.Description, required: { onlyIf: self.validationEnabled, message: '* Please Enter the Description' } });
        self.DredgPlan = ko.observable("");
        self.GridDate = ko.observable("");
        // For Planned Deployment Grid
        self.Budget = ko.observable();
        
        self.Apr = ko.observable();
        self.May = ko.observable();
        self.Jun = ko.observable();
        self.Jul = ko.observable();
        self.Aug = ko.observable();
        self.Sep = ko.observable();
        self.Oct = ko.observable();
        self.Nov = ko.observable();
        self.Dec = ko.observable();
        self.Jan = ko.observable();
        self.Feb = ko.observable();
        self.Mar = ko.observable();

        self.AprCraftID = ko.observable();
        self.MayCraftID = ko.observable();
        self.JunCraftID = ko.observable();
        self.JulCraftID = ko.observable();
        self.AugCraftID = ko.observable();
        self.SepCraftID = ko.observable();
        self.OctCraftID = ko.observable();
        self.NovCraftID = ko.observable();
        self.DecCraftID = ko.observable();
        self.JanCraftID = ko.observable();
        self.FebCraftID = ko.observable();
        self.MarCraftID = ko.observable();
      

        self.SubCatCode = ko.observable("");
       // self.CellID = ko.observable("");

        self.CraftID = ko.observable("");
        self.CraftName = ko.observable("");
        self.DredgerColorCode = ko.observable("");
        //self.masterDeploymentPlanTypes = ko.observableArray(data ? ko.utils.arrayMap(data.masterDeploymentPlanTypes, function (deploymentplan) {
        //    return new DeploymentPlanType(deploymentplan);
        //}) : []);

        self.DeploymentBudget = ko.observableArray([]);

        self.EditPending = ko.computed(function () {

            var startdate = self.StartDate();
            var enddate = self.EndDate();
            var CDate = (moment(new Date()).format('YYYY-MM-DD'));
            var SYear = (moment(startdate).format('YYYY-MM-DD'));
            var EYear = (moment(enddate).format('YYYY-MM-DD'));
            if (CDate >= SYear && CDate <= EYear) {
                //self.RecordStatus1 = ko.observable('Active');
                return true;
               
            }
            else {
               // self.RecordStatus1 = ko.observable('In Active');
                return false;
               
            }

        });

        self.Statust = ko.computed(function () {

            var startdate = self.StartDate();
            var enddate = self.EndDate();
            var CDate = (moment(new Date()).format('YYYY-MM-DD'));
            var SYear = (moment(startdate).format('YYYY-MM-DD'));
            var EYear = (moment(enddate).format('YYYY-MM-DD'));
            if (CDate >= SYear && CDate <= EYear) {
                var x="Active";
                return x;

            }
            else {
                var x ="In Active";
                return x;

            }
        });

        self.DateNameSort;
        self.DateName.subscribe(function (value) {
            self.DateNameSort = value;
        });
        self.DescriptionSort;
        self.Description.subscribe(function (value) {
            self.DescriptionSort = value;
        });

        self.PortNameSort;
        self.PortName.subscribe(function (value) {
            self.PortNameSort = value;
        });


        self.cache = function () { };
        self.set(data);
    }
    ipmsRoot.DeploymentPlanModel = DeploymentPlanModel;
    ipmsRoot.DeploymentPlanReferenceData = DeploymentPlanReferenceData;
    ipmsRoot.FinancialYear = FinancialYear;
    ipmsRoot.DeploymentPlanType = DeploymentPlanType;
    ipmsRoot.DredgingColor = DredgingColor;
    ipmsRoot.DredgingType = DredgingType;
    ipmsRoot.PortType = PortType;
    ipmsRoot.CraftColor = CraftColor;

}(window.IPMSROOT));

IPMSROOT.DeploymentPlanModel.prototype.set = function (data) {
    var self = this;
    self.DeploymentPlanID(data ? (data.DeploymentPlanID || "") : "");
   // self.FinancialYearCode(data ? (data.FinancialYearCode == 'NULL' ? "" : data.FinancialYearCode || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.PortName(data ? (data.PortName || "") : "");
   // self.FinancialYearName(data ? (data.FinancialYearName || "") : "");
    self.Description(data ? (data.Description || "") : "");
    self.DateName(data ? (data.DateName || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.DeploymentBudget(data ? (data.DeploymentBudget ? $.map(data.DeploymentBudget, function (item) { return item }) : []) : []);


    self.FinancialYearID(data ? (data.FinancialYearID || "") : "");
    self.StartDate(data ? (data.StartDate == 'NULL' ? "" : data.StartDate || "") : "");
    self.EndDate(data ? (data.EndDate == 'NULL' ? "" : data.EndDate || "") : "");
    self.FinancialYearDate(data ? (data.FinancialYearDate || "") : "");
    self.GridDate(data ? (data.GridDate || "") : "");
    self.SubCatCode(data ? (data.SubCatCode || "") : "");
    self.Budget(data ? (data.Budget || "") : "");
    self.DredgPlan(data ? (data.DredgPlan || "") : "");
    self.Apr(data ? (data.Apr || "") : "");
    self.May(data ? (data.May || "") : "");
    self.Jun(data ? (data.Jun || "") : "");
    self.Jul(data ? (data.Jul || "") : "");
    self.Aug(data ? (data.Aug || "") : "");
    self.Sep(data ? (data.Sep || "") : "");
    self.Oct(data ? (data.Oct || "") : "");
    self.Nov(data ? (data.Nov || "") : "");
    self.Dec(data ? (data.Dec || "") : "");
    self.Jan(data ? (data.Jan || "") : "");
    self.Feb(data ? (data.Feb || "") : "");
    self.Mar(data ? (data.Mar || "") : "");

    self.AprCraftID(data ? (data.AprCraftID || "") : "");
    self.MayCraftID(data ? (data.MayCraftID || "") : "");
    self.JunCraftID(data ? (data.JunCraftID || "") : "");
    self.JulCraftID(data ? (data.JulCraftID || "") : "");
    self.AugCraftID(data ? (data.AugCraftID || "") : "");
    self.SepCraftID(data ? (data.SepCraftID || "") : "");
    self.OctCraftID(data ? (data.OctCraftID || "") : "");
    self.NovCraftID(data ? (data.NovCraftID || "") : "");
    self.DecCraftID(data ? (data.DecCraftID || "") : "");
    self.JanCraftID(data ? (data.JanCraftID || "") : "");
    self.FebCraftID(data ? (data.FebCraftID || "") : "");
    self.MarCraftID(data ? (data.MarCraftID || "") : "");


  //  self.DredgerColorCode(data ? (data.DredgerColorCode || "") : "");
  //  self.CraftID(data ? (data.CraftID || "") : "");
  //  self.CraftName(data ? (data.CraftName || "") : "");

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.DeploymentPlanModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}




