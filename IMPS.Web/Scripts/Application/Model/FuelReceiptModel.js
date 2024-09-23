(function (ipmsRoot) {


    var FuelReceiptReferenceData = function (data) {
        var self = this;
        self.SupplyingModes = ko.observableArray(data ? $.map(data.SupplyingModes, function (item) { return new SupplyingMode(item); }) : []);
        self.GradeTypes = ko.observableArray(data ? $.map(data.GradeTypes, function (item) { return new GradeType(item); }) : []);
        self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item); }) : []);

    }

    var SupplyingMode = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    var GradeType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    var Berth = function (data) {
        var self = this;
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
      
    }

    var FuelReceiptModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.FuelRequisitionID = ko.observable();
     
       // self.CraftID = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Craft Name' } });
        self.OilTypeCode = ko.observable("");
        //self.FuelRequisitionID = ko.observable("");
        self.FuelReceiptID = ko.observable("");
        self.FuelGradeCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Grade' } });
        
      //  self.RequisitionDate = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Requisition Date' } });
        self.FuelRemarks = ko.observable("");


        self.SupplyingModeCode = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Supplying Mode' } });
        self.ModeID = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Mode ID' } });
        self.ReceiptDate = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please select Receipt Date' } });
        self.StartReading = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Start Reading' } });
        self.FinishReading = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Finish Reading' } });
        self.ReceivedQty = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Received Qty' } });
        self.ReceivedTempCelsius = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Received Temp' } });
        self.VCF = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter VCF' } });
        self.Qttyat20Degree1 = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Qtty @ 20 Degree Celsius' } });
        self.Qttyat20Degree2 = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Qtty @ 20 Degree Celsius' } });
        self.PumpStartDateTime = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Start Date/Time' } });
        self.PumpFinishDateTime = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter End Date / Time' } });
        self.Densityat15DegCelsius = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Density @ 15 Degree Celsius' } });
        self.Densityat20DegCelsius = ko.observable("").extend({ required: true }); //{ onlyIf: self.validationEnabled, message: '* Please enter Density @ 20 Degree Celsius' } });
        self.FlashPoint = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Flash Point' } });
        self.BatchNo = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Batch No.' } });
        self.KinematicViscat50DegCelsius = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Kinematic Visc. @ 50 Deg Celsius' } });
        self.WaterContent = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Water Content' } });
        self.SulphurContent = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Sulphur Content' } });
        self.Supplier = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Supplier' } });
        self.Flag = ko.observable("").extend({ required: true }); // { onlyIf: self.validationEnabled, message: '* Please enter Flag' } });
        self.Other = ko.observable("");


        self.PortCode = ko.observable("");
        self.QuayCode = ko.observable("");
        self.BerthCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Berth' } });
        self.BerthName = ko.observable();


        self.FuelReceiptNo = ko.observable("");
        self.FuelReceiptStatus = ko.observable("");
        self.startDate = ko.observable();
        self.ReceiptstartDate = ko.observable();


        self.FuelRequistionNo = ko.observable("");
        self.CraftCode = ko.observable("");
        self.CraftName = ko.observable("");
        self.CraftType = ko.observable("");
        self.IMONo = ko.observable("");
      //  self.OwnersName = ko.observable("");
        self.OilType = ko.observable();
        self.RequisitionStatus = ko.observable();
        self.UOMCode = ko.observable("");
        self.Quantity = ko.observable("");
        self.RequiredDate = ko.observable("");

        self.startDate = ko.observable();
        self.TotalTime = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.WorkflowInstanceId = ko.observable();
        self.Grade = ko.observable("");
        self.UOMName = ko.observable("");

        self.WorkFlowRemarks = ko.observable();

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);

        self.ReceiptNoSort;
        self.FuelReceiptNo.subscribe(function (value) {
            self.ReceiptNoSort = value;
        });
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

        self.ReceivedQtySort;
        self.ReceivedQty.subscribe(function (value) {
            self.ReceivedQtySort = value;
        });

        self.ReceiptDateSort;
        self.ReceiptDate.subscribe(function (value) {
            self.ReceiptDateSort = value;
        });
        self.ReceiptStatusSort;
        self.FuelReceiptStatus.subscribe(function (value) {
            self.ReceiptStatusSort = value;
        });

        self.EditPending = ko.computed(function () {
            if (self.FuelReceiptStatus() == "Acknowledge") {
                return false;
            }           
            else {
                return true;
            }
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

    ipmsRoot.FuelReceiptModel = FuelReceiptModel;
    ipmsRoot.FuelReceiptReferenceData = FuelReceiptReferenceData;
    ipmsRoot.SupplyingMode = SupplyingMode;
    ipmsRoot.GradeType = GradeType;
    ipmsRoot.Berth = Berth;

}(window.IPMSROOT));

IPMSROOT.FuelReceiptModel.prototype.set = function (data) {
    var self = this;    
    self.FuelReceiptID(data ? (data.FuelReceiptID || "") : "");
    self.FuelReceiptNo(data ? (data.FuelReceiptNo || "") : "");
    self.FuelRequisitionID(data ? (data.FuelRequisitionID || "") : "");
    self.FuelRequistionNo(data ? (data.FuelRequistionNo || "") : "");
 //   self.CraftID(data ? (data.CraftID == 'NULL' ? "" : data.CraftID || "") : "");
    self.CraftCode(data ? (data.CraftCode || "") : "");
    self.CraftName(data ? (data.CraftName || "") : "");
    self.CraftType(data ? (data.CraftType || "") : "");
    self.IMONo(data ? (data.IMONo || "") : "");
    self.OilTypeCode(data ? (data.OilTypeCode || "") : "");
    self.OilType(data ? (data.OilType || "") : "");

    self.FuelGradeCode(data ? (data.FuelGradeCode || "") : "");
     self.Grade(data ? (data.Grade || "") : "");
    self.UOMCode(data ? (data.UOMCode || "") : "");
    self.UOMName(data ? (data.UOMName || "") : "");  
    self.Quantity(data ? (data.Quantity || "") : "");

    self.PortCode(data ? (data.PortCode || "") : "");
    self.QuayCode(data ? (data.QuayCode || "") : "");
   
    self.SupplyingModeCode(data ? (data.SupplyingModeCode || "") : "");
    self.Other(data ? (data.Other || "") : "");
    self.ModeID(data ? (data.ModeID || "") : "");
    self.BerthCode(data ? (data.BerthCode || "") : "");
    //self.ReceiptDate(data ? (moment(data.ReceiptDate).format('YYYY-MM-DD') || "") : "");
    self.ReceiptDate(data ? (data.ReceiptDate || "") : "");
    self.Flag(data ? (data.Flag || "") : "");
    

    self.StartReading(data ? (data.StartReading || "") : "");
    self.FinishReading(data ? (data.FinishReading || "") : "");
    self.ReceivedQty(data ? (data.ReceivedQty || "") : "");

    self.ReceivedTempCelsius(data ? (data.ReceivedTempCelsius || "") : "");
    self.VCF(data ? (data.VCF || "") : "");
    self.Qttyat20Degree1(data ? (data.Qttyat20Degree1 || "") : "");
    self.Qttyat20Degree2(data ? (data.Qttyat20Degree2 || "") : "");

    //self.PumpStartDateTime(data ? (moment(data.PumpStartDateTime).format('YYYY-MM-DD HH:mm') || "") : "");
    //self.PumpFinishDateTime(data ? (moment(data.PumpFinishDateTime).format('YYYY-MM-DD HH:mm') || "") : "");

    self.PumpStartDateTime(data ? (data.PumpStartDateTime || "") : "");
    self.PumpFinishDateTime(data ? (data.PumpFinishDateTime || "") : "");


    self.Densityat15DegCelsius(data ? (data.Densityat15DegCelsius || "") : "");
    self.Densityat20DegCelsius(data ? (data.Densityat20DegCelsius || "") : "");
    self.FlashPoint(data ? (data.FlashPoint || "") : "");
    self.BatchNo(data ? (data.BatchNo || "") : "");
    self.KinematicViscat50DegCelsius(data ? (data.KinematicViscat50DegCelsius || "") : "");
    self.WaterContent(data ? (data.WaterContent || "") : "");
    self.SulphurContent(data ? (data.SulphurContent || "") : "");
    self.Supplier(data ? (data.Supplier || "") : "");


    self.FuelReceiptStatus(data ? (data.FuelReceiptStatus || "") : "");
    self.RequisitionStatus(data ? (data.RequisitionStatus || "") : "");
  
    self.FuelRemarks(data ? (data.FuelRemarks || "") : "");
    self.RequiredDate(data ? (moment(data.RequiredDate).format('YYYY-MM-DD') || "") : "");
    //self.RequisitionDate(data ? (moment(data.RequisitionDate).format('YYYY-MM-DD') || "") : "");   
  

    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.WorkflowInstanceId(data ? (data.WorkflowInstanceId || null) : null);
    self.BerthName(data ? (data.BerthName || null) : null);
    
    


    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.FuelReceiptModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


function allowOnlyThreePositiveDigts(el, evt) {
    debugger;
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



function limit(element) {

    debugger;
    var thisJ = $("#WaterContent");
    var max = 100;
    var min = 0;
    var intOnly = String(thisJ.attr("intOnly")).toLowerCase() == "true";

    var test = function (str) {
        return str == "" || /* (!intOnly && str == ".") || */
            ($.isNumeric(str) && str * 1 <= max && str * 1 >= min &&
            (!intOnly || str.indexOf(".") == -1) && str.match(/^0\d/) == null);
        // commented out code would allow entries like ".7"
    };

    thisJ.keydown(function () {
        var str = thisJ.val();
        if (test(str)) thisJ.data("dwnval", str);
    });

    thisJ.keyup(function () {
        var str = thisJ.val();
        if (!test(str)) thisJ.val(thisJ.data("dwnval"));
    });
}


