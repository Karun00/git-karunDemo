(function (ipmsRoot) { 
    var TfrDocumentUploadModel = function (data) {
        var self = this;
        self.RailPlan = ko.observableArray();
        self.WagonDetails = ko.observableArray();
        self.RailChangeNotifications = ko.observableArray();
        self.ArrivalAndDepartureTimes = ko.observableArray();
        self.UploadDocument = ko.observable(); 
        self.ArrivalDate = ko.observable(data ? data.ArrivalDate : '');  
 
    }

    var RailPlan = function (data) {
        var self = this;        
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
        self.TerminalOperator = ko.observable(data ? data.TerminalOperator : "");
        self.SlNo = ko.observable(data ? data.SlNo : "");

        self.BUSINESS_UNIT = ko.observable(data ? data.BUSINESS_UNIT : "");
        self.CORRIDOR_NAME = ko.observable(); //ko.observable(data ? data.CORRIDOR_NAME : "");
        self.SECTION_DESC = ko.observable(data ? data.SECTION_DESC : "");
        self.PLAN_TYPE = ko.observable(data ? data.PLAN_TYPE : "");
        self.TRAIN_ROUTE = ko.observable(data ? data.TRAIN_ROUTE : "");
        self.CATEGORY_DESC = ko.observable(data ? data.CATEGORY_DESC : "");
        self.TRAIN_TYPE = ko.observable(data ? data.TRAIN_TYPE : "");
        self.OUTBOUND_INBOUND_INDICATOR = ko.observable(data ? data.OUTBOUND_INBOUND_INDICATOR : "");
        self.TOTAL_WAGONS = ko.observable(data ? data.TOTAL_WAGONS : "");
        self.ACTUAL_TRAIN_MASS = ko.observable(data ? data.ACTUAL_TRAIN_MASS : "");
        self.DELAY_REASON = ko.observable(data ? data.DELAY_REASON : "");
        self.CANCELLATION_REF_NUMBER = ko.observable(data ? data.CANCELLATION_REF_NUMBER : "");
        self.CANCELLATION_REASON = ko.observable(data ? data.CANCELLATION_REASON : "");
        self.STAGED_REF_NUMBER = ko.observable(data ? data.STAGED_REF_NUMBER : "");
        self.STAGED_REASON = ko.observable(data ? data.STAGED_REASON : "");
        self.UPDATE_DATETIME = ko.observable(data ? data.UPDATE_DATETIME : "");
        self.ETA = ko.observable(data ? data.ETA : "");
        self.Corridor = ko.observable(data ? data.Corridor : "");
        self.PlannedDate = ko.observable(data ? data.PlannedDate : "");
        self.Schedule = ko.observable(data ? data.Schedule : "");
        self.TrainNo = ko.observable(); //ko.observable(data ? data.TrainNo : "");
        self.Origin = ko.observable();// ko.observable(data ? data.Origin : "");
        self.Destination = ko.observable();//ko.observable(data ? data.Destination : "");
        self.BreakType = ko.observable(data ? data.BreakType : "");
        self.PlannedETD = ko.observable(data ?  data.PlannedETD : "");
        self.PlannedETA = ko.observable(data ?  data.PlannedETA : "");
        self.Loco = ko.observable(data ? data.Loco : "");
        self.LocoQty = ko.observable(data ? data.LocoQty : "");
        self.NWBRef = ko.observable(data ? data.NWBRef : "");
        self.PlannedTons = ko.observable();// ko.observable(data ? data.PlannedTons : "");
        self.Load = ko.observable();// ko.observable(data ? data.Load : "");
        self.Remark = ko.observable(data ? data.Remark : "");
        self.YQ = ko.observable(data ? data.YQ : "");
        self.TrainStatus = ko.observable();
        self.ReasonForChange = ko.observable(data ? data.ReasonForChange : "");
        self.NewETD = ko.observable(data ? data.NewETD : "");
        self.NewETA = ko.observable(data ? data.NewETA : "");
        self.TrainMovement = ko.observable(data ? data.TrainMovement : "");
        self.ATD = ko.observable(data ? data.ATD : "");
        self.ATA = ko.observable(data ? data.ATA : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        var txt = '';
        var trainStatusColor = '';
//        <table style='border:0px solid #f00; line-height:18px; font-size:11px;'><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>CORRIDOR NAME</b></td><td>:</td><td style='text-align:left'>" + self.CORRIDOR_NAME() + " </td><td  style='text-align:right'><b>PLANNED TONS</b></td><td>:</td><td style='text-align:left'>" + self.PlannedTons() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>TRAIN ROUTE</b></td><td>:</td><td style='text-align:left'>" + self.TRAIN_ROUTE() + "</td><td style='text-align:right'><b>BUSINESS UNIT</b></td><td>:</td><td style='text-align:left'>"+self.BUSINESS_UNIT() + "</td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>TRAIN_NUMBER</b></td><td>:</td><td style='text-align:left'>" + self.TrainNo() + " </td><td  style='text-align:right'><b>TRAIN_TYPE</b></td><td>:</td><td style='text-align:left'>" + self.TRAIN_TYPE() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ORIGIN DEPART_FROM_PLACE</b></td><td>:</td><td style='text-align:left'>" + self.Origin() + " </td><td  style='text-align:right'><b>PLAN_TYPE</b></td><td>:</td><td style='text-align:left'>" + self.PLAN_TYPE() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td style='text-align:right'><b>DEPART_TO_PLACE</b></td><td>:</td><td style='text-align:left'>" + self.Destination() + " </td><td style='text-align:right'><b>CATEGORY_DESC</b></td><td>:</td><td style='text-align:left'>" + self.CATEGORY_DESC() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>SCHEDULED_DEPART_DATE_TIME </b></td><td>:</td><td style='text-align:left'>" + self.PlannedETD() + " </td><td  style='text-align:right'><b>OUTBOUND_INBOUND_INDICATOR</b></td><td>:</td><td style='text-align:left'>" + self.OUTBOUND_INBOUND_INDICATOR() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>SCHEDULED_ARRIVAL_DATE_TIME</b></td><td>:</td><td style='text-align:left'>" + self.PlannedETA() + " </td><td  style='text-align:right'><b>ACTUAL_TRAIN_MASS</b></td><td>:</td><td style='text-align:left'>" + self.ACTUAL_TRAIN_MASS() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>TOTAL_WAGONS </b></td><td>:</td><td style='text-align:left'>" + self.TOTAL_WAGONS() + " </td><td  style='text-align:right'><b>DELAY_REASON</b></td><td>:</td><td style='text-align:left'>" + self.DELAY_REASON() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOCO QUANTITY</b></td><td>:</td><td style='text-align:left'>" + self.LocoQty() + " </td><td  style='text-align:right'><b>LOAD</b></td><td>:</td><td style='text-align:left'>" + self.Load() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ACTUAL_DEPART_DATE_TIME </b></td><td>:</td><td style='text-align:left'>" + self.ATD() + " </td><td  style='text-align:right'><b>MOVEMENT STATUS</b></td><td>:</td><td style='text-align:left'>" + self.(TrainMovement() == '1') ? 'Arrived' : ((TrainMovement() == '0') ? 'Departed' : '') + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ACTUAL_ARRIVE_DATE_TIME</b></td><td>:</td><td style='text-align:left'>" + self.ATA() + " </td><td  style='text-align:right'><b>Section Desc </b></td><td>:</td><td style='text-align:left'>" + self.Section Desc() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>STATUS</b></td><td>:</td><td style='text-align:left'>" + self.RecordStatus() + " </td><td  style='text-align:right'><b>CANCELLATION_REF_NUMBER</b></td><td>:</td><td style='text-align:left'>" + self.CANCELLATION_REF_NUMBER() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>CANCELLATION_REASON</b></td><td>:</td><td style='text-align:left'>" + self.CANCELLATION_REASON() + " </td><td  style='text-align:right'><b>STAGED_REF_NUMBER</b></td><td>:</td><td style='text-align:left'>" + self.STAGED_REF_NUMBER() + " </td></tr>
 //<tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>STAGED_REASON</b></td><td>:</td><td style='text-align:left'>" + self.STAGED_REASON() + " </td><td  style='text-align:right'><b>UPDATE_DATETIME</b></td><td>:</td><td style='text-align:left'>" + self.UPDATE_DATETIME() + " </td></tr></table>

       // if (!(self.title() == null || self.title() == '' || self.title() == undefined)) {
        //txt = "<table style='border:0px solid #f00; line-height:18px; font-size:11px;'><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Break- Type</b></td><td>:</td><td style='text-align:left'>" + self.BreakType() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Schedule</b></td><td>:</td><td style='text-align:left'>" + self.Schedule() + "</td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>NWB Ref</b></td><td>:</td><td style='text-align:left'>" + self.NWBRef() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Remark</b></td><td>:</td><td style='text-align:left'>" + self.Remark() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td style='text-align:right'><b>Loco</b></td><td>:</td><td style='text-align:left'>" + self.Loco() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>YQ</b></td><td>:</td><td style='text-align:left'>" + self.YQ() + " </td></tr></table>";
        txt = "<table cellpadding='1' cellspacing='1' style='border:0px solid #f00; line-height:18px; font-size:11px; background:#CCC;'><tr style='border-bottom:1px solid #ddd;'><td width='195' bgcolor='#FFFFFF'  style='text-align:right'><b>CORRIDOR NAME :</b></td><td width='176' bgcolor='#FFFFFF' style='text-align:left'>" + self.CORRIDOR_NAME() + " </td><td width='25' rowspan='14' bgcolor='#FFFFFF'  style='text-align:right'>&nbsp;</td><td width='204' bgcolor='#FFFFFF'  style='text-align:right'><b>PLANNED TONS :</b></td><td width='252' bgcolor='#FFFFFF' style='text-align:left'>" + self.PlannedTons() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>TRAIN ROUTE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.TRAIN_ROUTE() + "</td><td bgcolor='#FFFFFF' style='text-align:right'><b>BUSINESS UNIT :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.BUSINESS_UNIT() + "</td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>TRAIN_NUMBER :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.TrainNo() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>TRAIN_TYPE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.TRAIN_TYPE() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>ORIGIN DEPART_FROM_PLACE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.Origin() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>PLAN_TYPE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.PLAN_TYPE() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF' style='text-align:right'><b>DEPART_TO_PLACE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.Destination() + " </td><td bgcolor='#FFFFFF' style='text-align:right'><b>CATEGORY_DESC :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.CATEGORY_DESC() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>SCHEDULED_DEPART_DATE_TIME :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.PlannedETD() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>OUTBOUND_INBOUND_INDICATOR :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.OUTBOUND_INBOUND_INDICATOR() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>SCHEDULED_ARRIVAL_DATE_TIME:</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.PlannedETA() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>ACTUAL_TRAIN_MASS :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.ACTUAL_TRAIN_MASS() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>TOTAL_WAGONS :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.TOTAL_WAGONS() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>DELAY_REASON :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.DELAY_REASON() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>LOCO QUANTITY :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.LocoQty() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>LOAD :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.Load() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>ACTUAL_DEPART_DATE_TIME :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.ATD() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>MOVEMENT STATUS :</b></td><td bgcolor='#FFFFFF' style='text-align:left; color:" + trainStatusColor + "'>" + self.TrainStatus() + "</td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>ACTUAL_ARRIVE_DATE_TIME :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.ATA() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>Section Desc :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.SECTION_DESC() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>STATUS :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.RecordStatus() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>CANCELLATION_REF_NUMBER :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.CANCELLATION_REF_NUMBER() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>CANCELLATION_REASON :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.CANCELLATION_REASON() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>STAGED_REF_NUMBER :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.STAGED_REF_NUMBER() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>STAGED_REASON :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.STAGED_REASON() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>UPDATE_DATETIME :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.UPDATE_DATETIME() + " </td></tr></table>";

       // }
        //else {
        //    txt = '';
        //}
        self.ToolTip = ko.observable(txt);
    

       //self.set(data);
        
        self.TrainNoSort;
        self.TrainNo.subscribe(function (value) {

            self.TrainNoSort = parseInt(value);

        });

        self.DestinationSort;
        self.Destination.subscribe(function (value) {
          
            self.DestinationSort = value;
        });

        self.OriginSort;
        self.Origin.subscribe(function (value) {
         
            self.OriginSort = value;
        });
        

        self.CORRIDORNAMESort;
        self.CORRIDOR_NAME.subscribe(function (value) {
        
            self.CORRIDORNAMESort = value;
        });

        self.TrainStatusSort;
        self.TrainStatus.subscribe(function (value) {
        
            self.TrainStatusSort = value;
        });
         

        self.cache = function () { };
        self.set(data);
    }
    
    var WagonDetails = function (data)
    {
        var self = this;
        self.TrainNumber = ko.observable(data ? data.TrainNo : "");
        self.WagonNumber = ko.observable(data ? data.WagonNumber : "");
        self.WagonType = ko.observable(data ? data.WagonType : "");
        self.Commodity = ko.observable(data ? data.Commodity : "");
        self.Tonnage = ko.observable(data ? data.Tonnage : "");
        self.OriginDate = ko.observable(data ? data.OriginDate : "");
        self.TrainOrigin = ko.observable(data ? data.TrainOrigin : "");
        self.UpdatedateTime = ko.observable(data ? data.UpatedateTime : ""); 
    }
    var RailChangeNotifications = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
        // self.TerminalOperator = ko.observable(data ? data.TerminalOperator : ""); 
        self.SlNo = ko.observable(data ? data.SlNo : "");
        self.Corridor = ko.observable(data ? data.Corridor : "");
        self.PlannedDate = ko.observable(data ? data.PlannedDate : "");
        self.TrainNo = ko.observable(data ? data.TrainNo : "");  
        self.TrainStatus = ko.observable(data ? data.TrainStatus : "");
        self.ReasonForChange = ko.observable(data ? data.ReasonForChange : "");
        self.NewETD = ko.observable(data ? data.NewETD : "");
        self.NewETA = ko.observable(data ? data.NewETA : ""); 
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");

        //self.set(data);
    }

    var ArrivalAndDepartureTimes = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
        // self.TerminalOperator = ko.observable(data ? data.TerminalOperator : ""); 
        self.SlNo = ko.observable(data ? data.SlNo : "");
        self.Corridor = ko.observable(data ? data.Corridor : "");
        self.PlannedDate = ko.observable(data ? data.PlannedDate : "");
        self.TrainNo = ko.observable(data ? data.TrainNo : "");   
        self.TrainMovement = ko.observable(data ? data.TrainMovement : "");
        self.ATD = ko.observable(data ? data.ATD : "");
        self.ATA = ko.observable(data ? data.ATA : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : ""); 
        //self.set(data);
    }

    var UploadDocument = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentName = ko.observable(data ? data.DocumentName : '');
        self.DocumentCode = ko.observable(data ? data.DocumentCode : '');
        self.FileName = ko.observable(data ? data.FileName : '');
    }
 
    ipmsRoot.TfrDocumentUploadModel = TfrDocumentUploadModel;
    ipmsRoot.UploadDocument = UploadDocument(); 
    ipmsRoot.RailPlan = RailPlan;
    ipmsRoot.WagonDetails = WagonDetails;
    ipmsRoot.RailChangeNotifications = RailChangeNotifications;
    ipmsRoot.ArrivalAndDepartureTimes = ArrivalAndDepartureTimes;

}(window.IPMSROOT));

IPMSROOT.RailPlan.prototype.set = function (data) {
    var self = this;
    self.PortCode(data ? (data.PortCode || "") : "");
    self.PortName(data ? (data.PortName || "") : "");
    self.TerminalOperator(data ? (data.TerminalOperator || "") : "");
    self.SlNo(data ? (data.SlNo || "") : "");
    self.BUSINESS_UNIT(data ? (data.BUSINESS_UNIT ||""):"");
    self.Corridor(data ? (data.Corridor || "") : "");
    self.CORRIDOR_NAME(data ? (data.CORRIDOR_NAME || "") : "");
    self.PlannedDate(data ? (data.PlannedDate || "" ): "");
    self.Schedule(data ? (data.Schedule || "") : "");
    self.TrainNo(data ? (data.TrainNo || "" ): "");
    self.Origin(data ? (data.Origin || "" ): "");
    self.Destination(data ?(data.Destination || "" ): "");
    self.BreakType(data ? (data.BreakType || "") : "");
    self.PlannedETD(data ? (data.PlannedETD || "" ): "");
    self.PlannedETA(data ? (data.PlannedETA || "" ): "");
    self.Loco(data ? (data.Loco || "" ): "");
    self.LocoQty(data ? (data.LocoQty || "") : "");
    self.NWBRef(data ? (data.NWBRef || "") : "");
    self.PlannedTons(data ? (data.PlannedTons || "" ): "");
    self.Load(data ? data.Load || "" : "");
    self.Remark(data ? (data.Remark || "") : "");
    self.YQ(data ? (data.YQ || "") : "");
    self.TrainStatus(data ? (data.TrainStatus || "" ): "");
    self.ReasonForChange(data ? (data.ReasonForChange || "") : "");
    self.NewETD(data ? (data.NewETD || "") : "");
    self.NewETA(data ? (data.NewETA || "" ): "");
    self.TrainMovement(data ? (data.TrainMovement || "") : "");
    self.ATD(data ? (data.ATD || "" ): "");
    self.ATA(data ? (data.ATA || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    var trainStatusColor = '';
    if (self.TrainStatus() == 'CANCELLED') {
        trainStatusColor = 'red';
    } 
    else if (self.TrainStatus() == 'STAGED') {
        trainStatusColor = 'yellow';
    }
    else if (self.TrainStatus() == 'ARRIVED')
    {
        trainStatusColor = 'blue';
    } else if (self.TrainStatus() == 'NOT DEPARTED') {
        trainStatusColor = 'pink';
    } else if (self.TrainStatus() == 'EARLY') {
        trainStatusColor = 'orange';
    } else if (self.TrainStatus() == 'ON TIME') {
        trainStatusColor = 'green';
    }
    else if (self.TrainStatus() == 'LATE') {
        trainStatusColor = 'orangered';
    }

    var txt = '';
    txt = "<table cellpadding='1' cellspacing='1' style='border:0px solid #f00; line-height:18px; font-size:11px; background:#CCC;'><tr style='border-bottom:1px solid #ddd;'><td width='195' bgcolor='#FFFFFF'  style='text-align:right'><b>CORRIDOR NAME :</b></td><td width='176' bgcolor='#FFFFFF' style='text-align:left'>" + self.CORRIDOR_NAME() + " </td><td width='25' rowspan='14' bgcolor='#FFFFFF'  style='text-align:right'>&nbsp;</td><td width='204' bgcolor='#FFFFFF'  style='text-align:right'><b>PLANNED TONS :</b></td><td width='252' bgcolor='#FFFFFF' style='text-align:left'>" + self.PlannedTons() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>TRAIN ROUTE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.TRAIN_ROUTE() + "</td><td bgcolor='#FFFFFF' style='text-align:right'><b>BUSINESS UNIT :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.BUSINESS_UNIT() + "</td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>TRAIN_NUMBER :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.TrainNo() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>TRAIN_TYPE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.TRAIN_TYPE() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>ORIGIN DEPART_FROM_PLACE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.Origin() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>PLAN_TYPE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.PLAN_TYPE() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF' style='text-align:right'><b>DEPART_TO_PLACE :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.Destination() + " </td><td bgcolor='#FFFFFF' style='text-align:right'><b>CATEGORY_DESC :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.CATEGORY_DESC() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>SCHEDULED_DEPART_DATE_TIME :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.PlannedETD() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>OUTBOUND_INBOUND_INDICATOR :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.OUTBOUND_INBOUND_INDICATOR() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>SCHEDULED_ARRIVAL_DATE_TIME:</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.PlannedETA() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>ACTUAL_TRAIN_MASS :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.ACTUAL_TRAIN_MASS() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>TOTAL_WAGONS :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.TOTAL_WAGONS() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>DELAY_REASON :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.DELAY_REASON() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>LOCO QUANTITY :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.LocoQty() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>LOAD :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.Load() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>ACTUAL_DEPART_DATE_TIME :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.ATD() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>MOVEMENT STATUS :</b></td><td bgcolor='#FFFFFF' style='text-align:left; color:" + trainStatusColor + "'>" + self.TrainStatus() + "</td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>ACTUAL_ARRIVE_DATE_TIME :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.ATA() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>Section Desc :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.SECTION_DESC() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>STATUS :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.RecordStatus() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>CANCELLATION_REF_NUMBER :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.CANCELLATION_REF_NUMBER() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>CANCELLATION_REASON :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.CANCELLATION_REASON() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>STAGED_REF_NUMBER :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.STAGED_REF_NUMBER() + " </td></tr><tr style='border-bottom:1px solid #ddd;'><td bgcolor='#FFFFFF'  style='text-align:right'><b>STAGED_REASON :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.STAGED_REASON() + " </td><td bgcolor='#FFFFFF'  style='text-align:right'><b>UPDATE_DATETIME :</b></td><td bgcolor='#FFFFFF' style='text-align:left'>" + self.UPDATE_DATETIME() + " </td></tr></table>";
    
    self.ToolTip(txt);  
    self.cache.latestData = data;
} 

IPMSROOT.TfrDocumentUploadModel.prototype.set = function (data) {
    var self = this;
    self.PortCode(data ? (data.PortCode || "") : "");
    self.cache.latestData = data;
}

IPMSROOT.TfrDocumentUploadModel.prototype.reset = function () { 

    for (var index in this) {
        if (ko.isObservable(this[index])) {
            this[index](null);
        }
        this.UploadDocument = null;
    }
}

////function for Allow Only Two Positive Digits For Decimal Value
//function AllowOnlyTwoPositiveDigitsForDecimal(el, evt) {

//    var charCode = (evt.which) ? evt.which : event.keyCode;
//    var number = el.value.split('.');
//    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
//        return false;
//    }
//    //just one dot
//    if (number.length > 1 && charCode == 46) {
//        return false;
//    }
//    //get the carat position
//    var caratPos = GetSelectionStart(el);
//    var dotPos = el.value.indexOf(".");
//    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
//        return false;
//    }
//    return true;
//}

//thanks: http://javascript.nwbox.com/cursor_position/
//function GetSelectionStart(o) {
//    if (o.createTextRange) {
//        var r = document.selection.createRange().duplicate()
//        r.moveEnd('character', o.value.length)
//        if (r.text == '') return o.value.length
//        return o.value.lastIndexOf(r.text)
//    } else return o.selectionStart
//}