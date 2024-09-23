(function (ipmsRoot) {

    var SAPPostingReferenceData = function (data) {
        var self = this;
        self.Reasons = ko.observableArray(data ? $.map(data.Reasons, function (item) { return new Reason(item); }) : []);
     

    }

    var Reason = function (data) {
        var self = this;
        self.SubCatCode = ko.observable(data ? data.SubCatCode : "");
        self.SubCatName = ko.observable(data ? data.SubCatName : "");
    }

    var SAPPostingModelItem = function (data) {
        var self = this;

        self.Item = ko.observable("");
        self.Status = ko.observable("");
    }


    var SAPPostingModel = function (data) {
        var self = this;

        self.validationEnabled = ko.observable(true);
        self.VCN = ko.observable("");
        self.VesselName = ko.observable("");
        self.PortCode = ko.observable("");

        self.SAPPostingID = ko.observable("");
        self.ReceavedARRNO = ko.observable("");
        self.Isadd = ko.observable(""); //Y
        self.IsView = ko.observable("");//Y
        self.IsUpdate = ko.observable("");//Y
        self.IsSapview = ko.observable("");//Y

        self.IsRepost = ko.observable("");//Y
        
        
        self.MarineAccNo = ko.observable("");
        self.MarinePostingId = ko.observable("");
        
        self.PostStatus = ko.observable("");
        self.RevinueAccountNo = ko.observable("");
        
        //BY Mahesh : For adding new field marine order no and Reason for visit and vessel type in arrival create/update
       
        self.ReasonForVisit = ko.observable("");
        self.VesselType = ko.observable("");
        self.ReceivedMarineOrderNo = ko.observable("");
             
        //

        self.Item = ko.observable("");
        self.Status = ko.observable("");
        self.MessageType = ko.observable("");
        self.MsgType = ko.observable("");
        self.PostingStatus = ko.observable("");
        self.AccountNo = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
        self.Reason = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required' } });
      

         self.ReferenceNo = ko.observable("");
         
         self.TransmitData = ko.observable("");
         self.Remarks = ko.observable("");
         self.RecordStatus = ko.observable("");
         self.CreatedBy = ko.observable("");
         self.ModifiedBy = ko.observable("");       
         self.SAPInvoice = ko.observable("");
         self.IsSAPInvoice = ko.observable();
         self.SAPReferenceNo = ko.observable("");


        // SAP Invoice Items
         self.MarineOrder = ko.observable("");
         self.BillingDocNo = ko.observable("");
         self.OrderNo = ko.observable("");
         self.ItemNo = ko.observable("");
         self.MaterialNo = ko.observable("");
         self.Service = ko.observable("");
         self.UOM = ko.observable("");
         self.Qunatity = ko.observable("");
         self.TarifF = ko.observable("");
         self.TarifF2 = ko.observable("");
         self.Amount = ko.observable("");
         self.VAT = ko.observable("");
         self.NetAmount = ko.observable("");
         self.SalesOrgNo = ko.observable("");
         self.AgentName = ko.observable("");
         self.Address = ko.observable("");
         self.TelephoneNo = ko.observable("");
         self.FaxNo = ko.observable("");
         self.Account = ko.observable("");
         self.VesselID = ko.observable("");
         self.VesselName = ko.observable("");
         self.VesselTonnage = ko.observable("");
         self.VesselCapacity = ko.observable("");
         self.VesselLength = ko.observable("");
         self.ArrivalID = ko.observable("");
         self.ArrivalDate = ko.observable("");
         self.Arrivaltime = ko.observable("");
         self.DepartureDate = ko.observable("");
         self.DepartureTime = ko.observable("");
         self.VoyageIn = ko.observable("");
         self.VoyageOut = ko.observable("");
         self.ESubscription = ko.observable("");
         self.NetValue = ko.observable("");
         self.RevenueAgentAccNo = ko.observable();


         self.RevenueAgentAccNoSort;
         self.RevenueAgentAccNo.subscribe(function (value) {
             self.RevenueAgentAccNoSort = value;
         });


        self.ItemSort;
        self.Item.subscribe(function (value) {
            self.ItemSort = value;
        });

        
        self.MessageSort;
        self.MsgType.subscribe(function (value) {
            self.MessageSort = value;
        });

        self.SAPPostingIDSort;
        self.SAPPostingID.subscribe(function (value) {
            self.SAPPostingIDSort = value;
        });

        self.ReceavedARRNOSort;
        self.ReceavedARRNO.subscribe(function (value) {
            self.ReceavedARRNOSort = value;
        });

        self.MarineAccNoSort;
        self.MarineAccNo.subscribe(function (value) {
            self.MarineAccNoSort = value;
        });
        
        self.ReceivedMarineOrderNoSort;
        self.ReceivedMarineOrderNo.subscribe(function (value) {
            self.ReceivedMarineOrderNoSort = value;
        });

        self.StatusSort;
        self.Status.subscribe(function (value) {
            self.StatusSort = value;
        });

        self.RemarksSort;
        self.Remarks.subscribe(function (value) {
            self.RemarksSort = value;
        });

        self.Resposting = ko.computed(function () {
            if (self.IsRepost() == "Y") {
                return true;
            }
            else {
                return false;
            }
        });

        self.AddPending = ko.computed(function () {
            if (self.Isadd() == "Y") {
                return true;
            }
            else {
                return false;
            }
        });


        self.ViewPending = ko.computed(function () {
            if (self.IsView() == "Y") {
                return true;
            }
            else {
                return false;
            }
        });

        self.MarineUpdatePending = ko.computed(function () {
            if (self.IsUpdate() == "Y") {
                return true;
            }
            else {
                return false;
            }
        });

        self.SAPInvoiceView = ko.computed(function () {
      
            if ((self.MsgType() == "CRMO" || self.MsgType() == "UPMO") && self.PostStatus() == "SCMP") {
                return true;
            }
            else {
                return false;
            }
        });


        self.InvoiceResponseView = ko.computed(function () {
            if (self.IsSapview() == "Y") {
                return true;
            }
            else {
                return false;
            }
        });

        self.cache = function () { };
        self.set(data);
    }   
  

  
    ipmsRoot.SAPPostingModel = SAPPostingModel;
    ipmsRoot.SAPPostingModelItem = SAPPostingModelItem;
    ipmsRoot.SAPPostingReferenceData = SAPPostingReferenceData;
    ipmsRoot.Reason = Reason;
  }(window.IPMSROOT));

IPMSROOT.SAPPostingModel.prototype.set = function (data) {
    var self = this;

    self.VesselName(data ? (data.VesselName || "") : "");
    self.VCN(data ? (data.VCN || "") : "");
    self.Item(data ? (data.Item || "") : "");
    self.Status(data ? (data.Status || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.MsgType(data ? (data.MsgType || "") : "");
    self.SAPPostingID(data ? (data.SAPPostingID || "") : "");
    self.ReceavedARRNO(data ? (data.ReceavedARRNO || "") : "");
    self.MarinePostingId(data ? (data.MarinePostingId || "") : "");

    self.ReceivedMarineOrderNo(data ? (data.ReceivedMarineOrderNo || "") : "");
    self.VesselType(data ? (data.VesselType || "") : "");
    self.ReasonForVisit(data ? (data.ReasonForVisit || "") : "");
    self.Remarks(data ? (data.Remarks || "") : "");
    
    self.IsRepost(data ? (data.IsRepost || "") : "");
    self.RevenueAgentAccNo(data ? (data.RevenueAgentAccNo || "") : "");

    self.Isadd(data ? (data.Isadd || "") : "");
    self.IsView(data ? (data.IsView || "") : "");
    self.IsUpdate(data ? (data.IsUpdate || "") : "");
    self.MarineAccNo(data ? (data.MarineAccNo || "") : "");
    self.PostStatus(data ? (data.PostStatus || "") : "");
    self.SAPReferenceNo(data ? (data.SAPReferenceNo || "") : "");
    self.IsSapview(data ? (data.IsSapview || "") : "");
    self.cache.latestData = ko.toJS(self);
}

IPMSROOT.SAPPostingModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}



