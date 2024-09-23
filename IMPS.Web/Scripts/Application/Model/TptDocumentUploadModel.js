(function (ipmsRoot) {

    var TptDocumentUploadModel = function (data) {
        var self = this;
        self.TerminalWeeklyData = ko.observableArray();
        self.OutTurnVolumess = ko.observableArray();
        self.UploadDocument = ko.observable();
        self.TerminalDelay = ko.observableArray();
        self.ArrivalDate = ko.observable(data ? data.ArrivalDate : '');
        self.WeekNo = ko.observable(data ? data.WeekNo : "");
        self.WeekEnding = ko.observable(data ? data.WeekEnding : ""); 
    }

    var TerminalData = function (data) {
        var self = this;        
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
        self.TerminalOperator = ko.observable(data ? data.TerminalOperator : "");
        self.WeekNo = ko.observable(data ? data.WeekNo : "");
        self.WeekEnding = ko.observable(data ? data.WeekEnding : "");
        self.PerformanceArea = ko.observable(data ? data.PerformanceArea : "");
        self.Measure = ko.observable(data ? data.Measure : "");
        self.UnitOfMeasure = ko.observable(data ? data.UnitOfMeasure : "");
        self.CargoType = ko.observable(data ? data.CargoType : "");
        self.Planned = ko.observable(data ? data.Planned : "");
        self.Actual = ko.observable(data ? data.Actual : "");
        self.Comments = ko.observable(data ? data.Comments : "");
        self.Terminal = ko.observable(data ? data.Terminal : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.ErrorStatus = ko.observable(data ? data.ErrorStatus : "");
    }

    //var TptDocumentUpload = function (data) {
    var OutTurnVolumes = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.IMONo = ko.observable(data ? data.IMONo : "");
        self.Voyage = ko.observable(data ? data.Voyage : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.Terminal = ko.observable(data ? data.Terminal : "");
        self.CargoType = ko.observable(data ? data.CargoType : "");
        self.OutTurnVolume = ko.observable(data ? data.OutTurnVolume : "");
        self.UnitOfMeasure = ko.observable(data ? data.UnitOfMeasure : "");
        
        self.FirstCraneSwing = ko.observable(data ? moment(data.FirstCraneSwing).format('YYYY-MM-DD HH:mm:ss') : "");
        self.LastCraneSwing = ko.observable(data ? moment(data.LastCraneSwing).format('YYYY-MM-DD HH:mm:ss') : "");
        self.NoOfCranes = ko.observable(data ? data.NoOfCranes : "");
        self.Comments = ko.observable(data ? data.Comments : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.ErrorStatus = ko.observable(data ? data.ErrorStatus : ""); 
        self.ArrivalDate = ko.observable(data ? data.ArrivalDate : '');
        self.ErrorStatus = ko.observable(data ? data.ErrorStatus : ""); 
      
    }

    var UploadDocument = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.DocumentName : '');
        self.DocumentCode = ko.observable(data ? data.DocumentCode : '');
        self.FileName = ko.observable(data ? data.FileName : '');
    }
    var TerminalDelays = function (data) {

        var self = this; 
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
        self.TerminalOperator = ko.observable(data ? data.TerminalOperator : "");

        self.ArrivalDate = ko.observable(data ? data.ArrivalDate : "");
        self.IMONo = ko.observable(data ? data.IMONo : 0);
        self.Voyage = ko.observable(data ? data.Voyage : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.Terminal = ko.observable(data ? data.Terminal : "");
        self.CargoType = ko.observable(data ? data.CargoType : "");        
        self.ReasonForDelay = ko.observable(data ? data.ReasonForDelay : "");
        self.DelayDuration = ko.observable(data ? data.DelayDuration : "");
        self.UnitOfMeasure = ko.observable(data ? data.UnitOfMeasure : "");
        self.Comments = ko.observable(data ? data.Comments : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.ErrorStatus = ko.observable(data ? data.ErrorStatus : ""); 
    }

    ipmsRoot.TptDocumentUploadModel = TptDocumentUploadModel;
    ipmsRoot.UploadDocument = UploadDocument();
    ipmsRoot.TerminalDelays = TerminalDelays;
    ipmsRoot.OutTurnVolumes = OutTurnVolumes;
    ipmsRoot.TerminalData = TerminalData;  
}(window.IPMSROOT));
IPMSROOT.TptDocumentUploadModel.prototype.set = function (data) {
    var self = this;
    self.PortCode(data ? (data.PortCode || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.TptDocumentUploadModel.prototype.reset = function () {
   
    // this.set(this.cache.latestData);
    debugger;
    for (var index in this) {
        if (ko.isObservable(this[index])) {
            this[index](null);
        }
        this.UploadDocument = null;
    }
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