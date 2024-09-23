(function (ipmsRoot) {


    var ReferenceData = function (data) {
        var self = this;

        self.Ports = ko.observableArray(data ? $.map(data.Ports, function (item) { return new Port(item); }) : []);
        self.Operations = ko.observableArray(data ? $.map(data.Operations, function (item) { return new Operation(item); }) : []);
        self.DelayTypes = ko.observableArray(data ? $.map(data.DelayTypes, function (item) { return new DelayType(item); }) : []);
        self.Berths = ko.observableArray(data ? $.map(data.Berths, function (item) { return new Berth(item); }) : []);
        self.CargoTypes = ko.observableArray(data ? $.map(data.CargoTypes, function (item) { return new Cargo(item); }) : []);
        self.Purposes = ko.observableArray(data ? $.map(data.Purpose, function (item) { return new Purpose(item); }) : []);
        self.Uoms = ko.observableArray(data ? $.map(data.Uoms, function (item) { return new Uoml(item); }) : []);
        self.Commodities = ko.observableArray(data ? $.map(data.Commodities, function (item) { return new Commodity(item); }) : []);
        self.TerminalOperators = ko.observableArray(data ? $.map(data.TerminalOperators, function (item) { return new TerminalOperator(item); }) : []);        
    }

    var Berth = function (data) {
        var self = this;
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
        self.Draftm = ko.observable(data ? data.Draftm : "");
        self.CargoDetails = ko.observable(data ? data.CargoDetails : "");
    }

    var TerminalOperator = function (data) {
        var self = this;
        self.TerminalOperatorID = ko.observable(data ? data.TerminalOperatorID : "");
        self.RegisteredName = ko.observable(data ? data.RegisteredName : "");
    }

    
    var Commodity = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var Cargo = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }
    
    var Purpose = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var Uoml = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var Port = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.PortName = ko.observable(data ? data.PortName : "");
    }

    var DelayType = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }


    var Operation = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var KeyEvent = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
        

        self.OperationsDelay = ko.observable(data ? data.OperationsDelay : "");
        self.KeyEventDateTime = ko.observable(data ? data.KeyEventDateTime : "");
        // self.KeyEventDateTime = ko.observable(data ? (moment(data.KeyEventDateTime).format('YYYY-MM-DD HH:mm:ss') || "") : "");



    }

    var OperationalDelay = function (data) {
        

        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");

        self.DelayType = ko.observable(data ? data.DelayType : "");
        //self.StartOperational = ko.observable(data ? (data.StartOperational != "" ? moment(data.StartOperational).format('YYYY-MM-DD HH:mm:ss') : null) : null);
        //self.EndOperational = ko.observable(data ? (data.EndOperational != "" ? moment(data.EndOperational).format('YYYY-MM-DD HH:mm:ss') : null) : null);

        self.StartOperational = ko.observable(data ? (data.StartOperational == "" || data.StartOperational == null ? null : (moment(data.StartOperational).format('YYYY-MM-DD HH:mm'))) : null);
        self.EndOperational = ko.observable(data ? (data.EndOperational == "" || data.EndOperational == null ? null : (moment(data.EndOperational).format('YYYY-MM-DD HH:mm'))) : null);
        self.Duration = ko.observable(data ? data.Duration : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
        self.StatementFactEventID = ko.observable(data ? data.StatementFactEventID : "");
        

    }

    
    //var VCN = function (data) {
    //    var self = this;
    //    self.VCN = ko.observable(data ? data.VCN : "");
    //    self.VesselName = ko.observable(data ? data.VesselName : "");
    //}

    var StatementFactModel = function (data) {
        var self = this;
        

        self.validationEnabled = ko.observable(true);
        self.StatementFactID = ko.observable();
        self.VCN = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select VCN' } });
        self.OperationCode = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* Please select Operation' } });
        self.OperationName = ko.observable("");
        self.MasterName = ko.observable("");
        self.RecordStatus = ko.observable('A');
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.PortCode = ko.observable();
        self.PortName = ko.observable();
        self.DateTime = ko.observable();
        self.SubCatCode = ko.observable();
        self.SubCatName = ko.observable();
        self.VesselName = ko.observable();
        self.Voyage = ko.observable();
        self.Berth = ko.observable();
        self.SDateFrom = ko.observable();
        self.SDateTo = ko.observable();

        //   self.masterKeyEvents = ko.observableArray(data ? $.map(data.masterKeyEvents, function (item) { return new KeyEvent(item); }) : []);
        //self.StatementFactEvents = ko.observableArray([]);
        //self.KeyEvents = ko.observableArray(data ? $.map(data.KeyEvents, function (item) { return new KeyEvent(item); }) : []);


        self.VesselName = ko.observable();
        self.VCN_VesselName = ko.observable();
        self.CurrentBerth = ko.observable();
        self.DateFrom = ko.observable();
        self.DateTo = ko.observable();
        self.VoyageIn = ko.observable();
        self.VoyageOut = ko.observable();

        self.InwardPilotOnBoard = ko.observable();
        self.InwardPilotAway = ko.observable();
        self.InwardFirstLine = ko.observable();
        self.InwardAllFast = ko.observable();
        self.OutwardPilotOnBoard = ko.observable();
        self.OutwardAllCast = ko.observable();
        self.OutwardPilotAway = ko.observable();

        self.DraftArrivalFwd = ko.observable();
        self.DraftArrivalAft = ko.observable();
        self.DraftSailingAft = ko.observable();
        self.DraftSailingFwd = ko.observable();


        //  self.KeyEventDateTime = ko.observable();
        //  self.OperationsDelay = ko.observable();

        self.ArrivalFuel = ko.observable();
        self.ArrivalDiesel = ko.observable();
        self.SailingFuel = ko.observable();
        self.SailingDiesel = ko.observable();
        self.MasterName = ko.observable();
        self.StatementTugDetails = ko.observable();



        self.EOSPDateTime = ko.observable();
        self.GangwayDown = ko.observable();
        self.NORTendered = ko.observable();
        self.NORAccepted = ko.observable();
        self.StevedoreOnBoard = ko.observable();
        self.StevedoreStart = ko.observable();
        self.StevedoreEnd = ko.observable();
        self.StevedoreOff = ko.observable();
        self.CranesDeployed = ko.observable();
        self.StartCargo = ko.observable();
        self.EndCargo = ko.observable();




        self.StatementFactEvents = ko.observableArray(data ? ko.utils.arrayMap(data.StatementFactEvents, function (commodity) { return new OperationalDelay(commodity); }) : []);

        self.StatementCommodities = ko.observableArray(data ? ko.utils.arrayMap(data.StatementCommodities, function (item) { return new StatementCommodity(item); }) : []);

        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });

        self.VCNSort;
        self.VCN.subscribe(function (value) {
            self.VCNSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });
        self.VoyageInSort;
        self.VoyageIn.subscribe(function (value) {
            self.VoyageInSort = value;
        });

        self.VoyageOutSort;
        self.VoyageOut.subscribe(function (value) {
            self.VoyageOutSort = value;
        });

        self.OperationCodeSort;
        self.OperationName.subscribe(function (value) {
            self.OperationCodeSort = value;
        });
        self.BerthSort;
        self.CurrentBerth.subscribe(function (value) {
            self.BerthSort = value;
        });
        self.DateFromSort;
        self.SDateFrom.subscribe(function (value) {
            self.DateFromSort = value;
        });
        self.DateToSort;
        self.SDateTo.subscribe(function (value) {
            self.DateToSort = value;
        });

        self.MasterNameSort;
        self.MasterName.subscribe(function (value) {
            self.MasterNameSort = value;
        });



        self.cache = function () { };
        self.set(data);
    }

    var StatementTugData = function (data) {
        var self = this;

        self.ArrivalDetails = ko.observableArray(data ? ko.utils.arrayMap(data.ArrivalDetails, function (arrivalData) {
            return new ArrivalData(arrivalData);
        }) : []);

        self.SailingDetails = ko.observableArray(data ? ko.utils.arrayMap(data.SailingDetails, function (sailingData) {
            return new SailingData(sailingData);
        }) : []);
    }

    var StatementCommodity = function (data) {
        var self = this;
        //self.ArrivalCommodityID = ko.observable(data ? data.ArrivalCommodityID : "");
        self.CommodityBerthKey = ko.observable(data ? data.CommodityBerthKey : "");
        
        self.Commodity = ko.observable(data ? data.Commodity : "");
        self.TerminalOperatorID = ko.observable(data ? data.TerminalOperatorID : "");
        self.CargoType = ko.observable(data ? data.CargoType : "");
        self.Package = ko.observable(data ? data.Package : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.Quantity = ko.observable(data ? data.Quantity : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : '');
        self.CreatedDatedatetime = ko.observable(data ? data.CreatedDatedatetime : '');
        self.ModifiedBy = ko.observable(data ? data.ModifiedBy : '');
        self.ModifiedDatedatetime = ko.observable(data ? data.ModifiedDatedatetime : '');
    }

    var ArrivalData = function (data) {
        var self = this;
        self.CraftName = ko.observable(data ? data.CraftName : "");
        self.CRAFTTYPE = ko.observable(data ? data.CRAFTTYPE : "");
        self.ForwardDraftM = ko.observable(data ? data.ForwardDraftM : "");
        self.AftDraftM = ko.observable(data ? data.AftDraftM : "");
    }

    var SailingData = function (data) {
        var self = this;
        self.CraftName = ko.observable(data ? data.CraftName : "");
        self.CRAFTTYPE = ko.observable(data ? data.CRAFTTYPE : "");
        self.ForwardDraftM = ko.observable(data ? data.ForwardDraftM : "");
        self.AftDraftM = ko.observable(data ? data.AftDraftM : "");
    }

    var StatementFactModelGrid = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VesselName = ko.observable();

        self.set(data);
    }

    ipmsRoot.StatementTugData = StatementTugData;
    ipmsRoot.ArrivalData = ArrivalData;
    ipmsRoot.SailingData = SailingData;
    ipmsRoot.StatementFactModel = StatementFactModel;
    ipmsRoot.ReferenceData = ReferenceData;
    ipmsRoot.KeyEvent = KeyEvent;
    ipmsRoot.OperationalDelay = OperationalDelay;
    ipmsRoot.DelayType = DelayType;
    ipmsRoot.StatementFactModelGrid = StatementFactModelGrid;
    ipmsRoot.StatementCommodity = StatementCommodity;
    ipmsRoot.Berth = Berth;
    ipmsRoot.TerminalOperator = TerminalOperator;
    ipmsRoot.Commodity = Commodity;
    ipmsRoot.Cargo = Cargo;
    ipmsRoot.Purpose = Purpose;
    ipmsRoot.Uoml = Uoml;
}(window.IPMSROOT));

IPMSROOT.StatementFactModelGrid.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");
}

IPMSROOT.StatementFactModel.prototype.set = function (data) {
    var self = this;
    self.StatementFactID(data ? (data.StatementFactID || "") : "");
    self.VCN(data ? (data.VCN || "") : "");
    self.VesselName(data ? (data.VesselName || "") : "");
    self.VCN_VesselName(data ? (data.VCN_VesselName || "") : "");
    self.OperationCode(data ? (data.OperationCode == 'NULL' ? "" : data.OperationCode || "") : "");
    self.OperationName(data ? (data.OperationName || "") : "");

    self.Voyage(data ? (data.Voyage || "") : "");
    self.Berth(data ? (data.Berth || "") : "");
    self.DateFrom(data ? (data.DateFrom || null) : null);
    self.DateTo(data ? (data.DateTo || null) : null);

    //self.SDateFrom(data ? (moment(data.SDateFrom).format('YYYY-MM-DD HH:mm') || null) : null);
    //self.SDateTo(data ? (moment(data.SDateTo).format('YYYY-MM-DD HH:mm') || null) : null);

    self.SDateFrom(data ? (data.SDateFrom == "" || data.SDateFrom == null ? null : (moment(data.SDateFrom).format('YYYY-MM-DD HH:mm'))) : null);
    self.SDateTo(data ? (data.SDateTo == "" || data.SDateTo == null ? null : (moment(data.SDateTo).format('YYYY-MM-DD HH:mm'))) : null);

    self.MasterName(data ? (data.MasterName || "") : "");
    self.ArrivalFuel(data ? (data.ArrivalFuel || "") : "");
    self.ArrivalDiesel(data ? (data.ArrivalDiesel || "") : "");
    self.SailingFuel(data ? (data.SailingFuel || "") : "");
    self.SailingDiesel(data ? (data.SailingDiesel || "") : "");

    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");   
    self.StatementFactEvents(data ? (data.StatementFactEvents ? $.map(data.StatementFactEvents, function (item) { return new IPMSROOT.OperationalDelay(item); }) : []) : []);
    //self.StatementFactEvents(data ? ko.utils.arrayMap(data.StatementFactEvents, function (commodity) { return new OperationalDelay(commodity); }) : []);
    //self.StatementFactEvents(data ? (data.StatementFactEvents ? $.map(data.StatementFactEvents, function (item) { return item }) : []) : []);

    self.VesselName(data ? (data.VesselName || "") : "");
    self.CurrentBerth(data ? (data.CurrentBerth || "") : "");
    self.VoyageIn(data ? (data.VoyageIn || "") : "");
    self.VoyageOut(data ? (data.VoyageOut || "") : "");

    self.InwardPilotOnBoard(data ? (data.InwardPilotOnBoard || null) : null);
    self.InwardPilotAway(data ? (data.InwardPilotAway || null) : null);
    self.InwardFirstLine(data ? (data.InwardFirstLine || null) : null);
    self.InwardAllFast(data ? (data.InwardAllFast || null) : null);

    self.OutwardPilotOnBoard(data ? (data.OutwardPilotOnBoard || null) : null);
    self.OutwardAllCast(data ? (data.OutwardAllCast || null) : null);
    self.OutwardPilotAway(data ? (data.OutwardPilotAway || null) : null);

    self.DraftArrivalFwd(data ? (data.DraftArrivalFwd || "") : "");
    self.DraftArrivalAft(data ? (data.DraftArrivalAft || "") : "");
    self.DraftSailingAft(data ? (data.DraftSailingAft || "") : "");
    self.DraftSailingFwd(data ? (data.DraftSailingFwd || "") : "");
    self.StatementTugDetails(data ? new IPMSROOT.StatementTugData(data.StatementTugData) : new IPMSROOT.StatementTugData());

    self.EOSPDateTime(data ? (data.EOSPDateTime == "" || data.EOSPDateTime == null ? null : (moment(data.EOSPDateTime).format('YYYY-MM-DD HH:mm'))) : null);
    self.GangwayDown(data ? (data.GangwayDown == "" || data.GangwayDown == null ? null : (moment(data.GangwayDown).format('YYYY-MM-DD HH:mm'))) : null);
    self.NORTendered(data ? (data.NORTendered == "" || data.NORTendered == null ? null : (moment(data.NORTendered).format('YYYY-MM-DD HH:mm'))) : null);
    self.NORAccepted(data ? (data.NORAccepted == "" || data.NORAccepted == null ? null : (moment(data.NORAccepted).format('YYYY-MM-DD HH:mm'))) : null);
    self.StevedoreOnBoard(data ? (data.StevedoreOnBoard == "" || data.StevedoreOnBoard == null ? null : (moment(data.StevedoreOnBoard).format('YYYY-MM-DD HH:mm'))) : null);
    self.StevedoreStart(data ? (data.StevedoreStart == "" || data.StevedoreStart == null ? null : (moment(data.StevedoreStart).format('YYYY-MM-DD HH:mm'))) : null);
    self.StevedoreEnd(data ? (data.StevedoreEnd == "" || data.StevedoreEnd == null ? null : (moment(data.StevedoreEnd).format('YYYY-MM-DD HH:mm'))) : null);
    self.StevedoreOff(data ? (data.StevedoreOff == "" || data.StevedoreOff == null ? null : (moment(data.StevedoreOff).format('YYYY-MM-DD HH:mm'))) : null);
    self.CranesDeployed(data ? (data.CranesDeployed || "") : "");
    //self.CranesDeployed(data ? (data.CranesDeployed == "" || data.CranesDeployed == null ? null : (moment(data.CranesDeployed).format('YYYY-MM-DD HH:mm'))) : null);
    self.StartCargo(data ? (data.StartCargo == "" || data.StartCargo == null ? null : (moment(data.StartCargo).format('YYYY-MM-DD HH:mm'))) : null);
    self.EndCargo(data ? (data.EndCargo == "" || data.EndCargo == null ? null : (moment(data.EndCargo).format('YYYY-MM-DD HH:mm'))) : null);
    
    

    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.StatementFactModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
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



