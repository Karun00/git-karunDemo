(function (ipmsRoot) {

    var MarineRevenueModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(false);
        self.RevenuePostingID = ko.observable();
        self.vcn = ko.observable().extend({ required: { onlyIf: self.validationEnabled, message: ' Please Select VCN' } });
        self.PostedDate = ko.observable();
        self.SAPAccNo = ko.observable();
        self.AgentID = ko.observable().extend({ required: { onlyIf: self.validationEnabled, message: ' Please Select Agent Name' } });
        self.AgentAccountID = ko.observable().extend({ required: { onlyIf: self.validationEnabled, message: ' Please Select SAP Accounts' } });
        self.AgentID1 = ko.observable();
        self.AgentAccountID1 = ko.observable();
      

        self.AccountNo = ko.observable();
        self.PostingStatus = ko.observable();
        self.AnyDangerousGoodsonBoard = ko.observable();

        self.VslSrchOn = ko.observable("VCN");

        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();
        self.ATA = ko.observable();
        self.ATD = ko.observable();
        self.ReasonForVisit = ko.observable();
        self.VesselType = ko.observable();
        self.RegisteredName = ko.observable();
        self.VesselName = ko.observable();
        self.VesselData = ko.observable();
        self.RevenueDetails = ko.observable();
        self.VCNSort;
        self.vcn.subscribe(function (value) {
            self.VCNSort = value;
        });

        self.VesselNameSort;
        self.VesselName.subscribe(function (value) {
            self.VesselNameSort = value;
        });

        self.ATASort;
        self.ATA.subscribe(function (value) {
            self.ATASort = value;
        });

        self.ATDSort;
        self.ATD.subscribe(function (value) {
            self.ATDSort = value;
        });

        self.RegisteredNameSort;
        self.RegisteredName.subscribe(function (value) {
            self.RegisteredNameSort = value;
        });

        self.VesselTypeSort;
        self.VesselType.subscribe(function (value) {
            self.VesselTypeSort = value;
        });

        self.ReasonForVisitSort;
        self.ReasonForVisit.subscribe(function (value) {
            self.ReasonForVisitSort = value;
        });

        self.PostingStatusSort;
        self.PostingStatus.subscribe(function (value) {
            self.PostingStatusSort = value;
        });

        self.cache = function () { };
        self.set(data);
    }

    var RevenuePostingDtl = function (data) {
        var self = this;

        self.RevenuePostingDtlID = ko.observable(data ? data.RevenuePostingDtlID : "");
        self.RevenuePostingID = ko.observable(data ? data.RevenuePostingID : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.Units = ko.observable(data ? data.Units : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.MomentType = ko.observable(data ? data.MomentType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
    }

    var Vessel = function (data) {
        var self = this;
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.VeselAutoName = ko.observable(data ? data.VeselAutoName : "");
        self.Arrno = ko.observable(data ? data.Arrno : "");
        self.IMONo = ko.observable(data ? data.IMONo : "");
        self.VesselType = ko.observable(data ? data.VesselType : "");
        self.VesselTypeName = ko.observable(data ? data.VesselTypeName : "");
        self.vcn = ko.observable(data ? data.vcn : "");
        self.ATA = ko.observable(data ? data.ATA : "");
        self.ATD = ko.observable(data ? data.ATD : "");
        self.VoyageIn = ko.observable(data ? data.VoyageIn : "");
        self.VoyageOut = ko.observable(data ? data.VoyageOut : "");
        self.GRT = ko.observable(data ? data.GRT : "");
        self.ReasonForVisit = ko.observable(data ? data.ReasonForVisit : "");
        self.LastPortOfCall = ko.observable(data ? data.LastPortOfCall : "");
        self.NextPortOfCall = ko.observable(data ? data.NextPortOfCall : "");
        self.CallSign = ko.observable(data ? data.CallSign : "");
        self.SAPVesselNo = ko.observable(data ? data.SAPVesselNo : "");


    }


    var RevenuePostingData = function (data) {
        var self = this;
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.AgentID = ko.observable(data ? data.AgentID : "");
        self.AgentAccountID = ko.observable(data ? data.AgentAccountID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.RegisteredName = ko.observable(data ? data.RegisteredName : "");

        self.PortDuesDetailsView = ko.observableArray(data ? ko.utils.arrayMap(data.PortDuesDetailsView, function (portDueDataview) {
            return new PortDueDataview(portDueDataview);
        }) : []);        

        self.PortDuesDetails = ko.observableArray(data ? ko.utils.arrayMap(data.PortDuesDetails, function (portDueData) {
            return new PortDueData(portDueData);
        }) : []);

        self.BerthDuesDetails = ko.observableArray(data ? ko.utils.arrayMap(data.BerthDuesDetails, function (berthDueData) {
            return new BerthDueData(berthDueData);
        }) : []);

        self.RefuseRemovalDetails = ko.observableArray(data ? ko.utils.arrayMap(data.RefuseRemovalDetails, function (refusalData) {
            
            return new RefusalData(refusalData);
        }) : []);


        self.ArrivalDetails = ko.observableArray(data ? ko.utils.arrayMap(data.ArrivalDetails, function (arrivalData) {
            return new ArrivalData(arrivalData);
        }) : []);

        self.ShiftingDetails = ko.observableArray(data ? ko.utils.arrayMap(data.ShiftingDetails, function (shiftingData) {
            return new ShiftingData(shiftingData);
        }) : []);

        self.WarpingDetails = ko.observableArray(data ? ko.utils.arrayMap(data.WarpingDetails, function (warpingData) {
            return new WarpingData(warpingData);
        }) : []);

        self.SailingDetails = ko.observableArray(data ? ko.utils.arrayMap(data.SailingDetails, function (sailingData) {
            return new SailingData(sailingData);
        }) : []);


        self.DryDockDetails = ko.observableArray(data ? ko.utils.arrayMap(data.DryDockDetails, function (drydocData) {
            return new DryDockData(drydocData);
        }) : []);

        self.DryDock12HrsDetails = ko.observableArray(data ? ko.utils.arrayMap(data.DryDock12HrsDetails, function (drydoc24hrData) {
            return new DryDock12HrsData(drydoc24hrData);
        }) : []);

        self.SupplimantoryDetails = ko.observableArray(data ? ko.utils.arrayMap(data.SupplimantoryDetails, function (supplimantDetail) {
            return new SupplimantData(supplimantDetail);
        }) : []);

        self.DrydockMislaniousDetails = ko.observableArray(data ? ko.utils.arrayMap(data.DrydockMislaniousDetails, function (drydockMislaniousDetail) {
            return new DrydockMislaniousData(drydockMislaniousDetail);
        }) : []);

        self.DisplayInfo = ko.observableArray(data ? ko.utils.arrayMap(data.DisplayInfo, function (displayInfo) {
            return new DisplayInfoData(displayInfo);
        }) : []);

    }

    var PortDueDataview = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
    }


    var PortDueData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.PostingDateTime = ko.observable(data ? moment(data.PostingDateTime).format('YYYY-MM-DD HH:mm') : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
    }

    var BerthDueData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.PostingDateTime = ko.observable(data ? moment(data.PostingDateTime).format('YYYY-MM-DD HH:mm') : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
    }
    var RefusalData = function (data) {
        var self = this;
       
        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.PostingDateTime = ko.observable(data ? moment(data.PostingDateTime).format('YYYY-MM-DD HH:mm') : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
    }

    var ArrivalData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
    }

    var ShiftingData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
    }

    var WarpingData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
    }

    var SailingData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
    }

    var DryDockData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
    }

    var DryDock12HrsData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
    }

    var SupplimantData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");

        self.CloseMterReding = ko.observable(data ? data.CloseMterReding : "");
        self.startmtrreding = ko.observable(data ? data.startmtrreding : "");
        self.MeterSerialNo = ko.observable(data ? data.MeterSerialNo : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");


    }

    var DrydockMislaniousData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
    }

    var DisplayInfoData = function (data) {
        var self = this;

        self.ISPOSTED = ko.observable(data ? data.ISPOSTED : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.MovementName = ko.observable(data ? data.MovementName : "");
        self.ServiceName = ko.observable(data ? data.ServiceName : "");
        self.GroupCode = ko.observable(data ? data.GroupCode : "");
        self.MaterialCode = ko.observable(data ? data.MaterialCode : "");
        self.MaterialDescription = ko.observable(data ? data.MaterialDescription : "");
        self.AccountNo = ko.observable(data ? data.AccountNo : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.Endtime = ko.observable(data ? data.Endtime : "");
        self.IsCalculated = ko.observable(data ? data.IsCalculated : "");
        self.Chargedas = ko.observable(data ? data.Chargedas : "");
        self.UOM = ko.observable(data ? data.UOM : "");
        self.MovementType = ko.observable(data ? data.MovementType : "");
        self.ServiceType = ko.observable(data ? data.ServiceType : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.OperationType = ko.observable(data ? data.OperationType : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.RecentlyPostedDate = ko.observable(data ? data.RecentlyPostedDate : "");
        self.DueHours = ko.observable(data ? data.DueHours : "");
        self.TotalHours = ko.observable(data ? data.TotalHours : "");
        self.ischecked = ko.observable(data ? data.ischecked : "");
        self.PostingDateTime = ko.observable(data ? data.PostingDateTime : "");
    }

    var RevenuePostingModelGrid = function (data) {
        var self = this;
        self.VCN = ko.observable();
        self.VesselName = ko.observable();
        self.RevenueFrom = ko.observable();
        self.RevenueTo = ko.observable();
        self.VCNSERCH = ko.observable();
        self.VESSELSERCH = ko.observable();
        self.set(data);
    }


    ipmsRoot.MarineRevenueModel = MarineRevenueModel;
    ipmsRoot.RevenuePostingDtl = RevenuePostingDtl;
    ipmsRoot.RevenuePostingData = RevenuePostingData;
    ipmsRoot.Vessel = Vessel;
    ipmsRoot.PortDueData = PortDueData;
    ipmsRoot.BerthDueData = BerthDueData;
    ipmsRoot.RefusalData = RefusalData;
    ipmsRoot.PortDueDataview = PortDueDataview;

    ipmsRoot.ArrivalData = ArrivalData;
    ipmsRoot.ShiftingData = ShiftingData;
    ipmsRoot.WarpingData = WarpingData;
    ipmsRoot.SailingData = SailingData;
    ipmsRoot.DryDockData = DryDockData;
    ipmsRoot.DryDock12HrsData = DryDock12HrsData;
    ipmsRoot.SupplimantData = SupplimantData;
    ipmsRoot.DrydockMislaniousData = DrydockMislaniousData;
    ipmsRoot.DisplayInfoData = DisplayInfoData;
    ipmsRoot.RevenuePostingModelGrid = RevenuePostingModelGrid;

}(window.IPMSROOT));

IPMSROOT.RevenuePostingModelGrid.prototype.set = function (data) {
    var self = this;
    self.VCN(data ? data.VCN : "");
    self.VesselName(data ? data.VesselName : "");

    var todaydate = new Date();
    var todate = new Date(todaydate);
    var fromdate = new Date(todaydate);
    todate.setDate(todaydate.getDate());
    fromdate.setDate(fromdate.getDate() - 30);
    self.RevenueFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
    self.RevenueTo(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");
    self.VCNSERCH(data ? data.VCNSERCH : "");

    self.VESSELSERCH(data ? data.VESSELSERCH : "");
}

IPMSROOT.MarineRevenueModel.prototype.set = function (data) {
    var self = this;

    self.RevenuePostingID(data ? (data.RevenuePostingID || "") : "");
    self.AgentID(data ? (data.AgentID || "") : "");
    self.AgentAccountID(data ? (data.AgentAccountID || "") : "");

    self.AgentID1(data ? (data.AgentID || "") : "");
    self.AgentAccountID1(data ? (data.AgentAccountID || "") : "");


    self.AccountNo(data ? (data.AccountNo || "") : "");
    self.vcn(data ? (data.vcn || "") : "");
    self.PostedDate(data ? (data.PostedDate || "") : "");
    self.SAPAccNo(data ? (data.SAPAccNo || "") : "");

    self.PostingStatus(data ? (data.PostingStatus || "") : "");
    self.AnyDangerousGoodsonBoard(data ? (data.AnyDangerousGoodsonBoard || "") : "");
    self.CreatedBy(data ? (data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.ModifiedBy(data ? (data.ModifiedBy || "") : "");
    self.ModifiedDate(data ? (data.ModifiedDate || "") : "");
    self.ATA(data ? (data.ATA || "") : "");
    self.ATD(data ? (data.ATD || "") : "");
    self.ReasonForVisit(data ? (data.ReasonForVisit || "") : "");
    self.VesselType(data ? (data.VesselType || "") : "");
    self.RegisteredName(data ? (data.RegisteredName || "") : "");
    self.VesselName(data ? (data.VesselName || "") : "");
    self.VesselData(data ? new IPMSROOT.Vessel(data.Vessel) : "");
    self.RevenueDetails(data ? new IPMSROOT.RevenuePostingData(data.RevenuePostingData) : new IPMSROOT.RevenuePostingData());
    self.cache.latestData = data;
}

IPMSROOT.MarineRevenueModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


//function portduescheckAll(ctrl, itmname) {

//    checkboxes = document.getElementsByName(itmname);

//    if (ctrl.checked) {
//        for (var i = 0, n = checkboxes.length; i < n; i++) {
//            if (checkboxes[i].style.display != 'none') {
//                checkboxes[i].checked = ctrl.checked;
//                checkboxes[i].value = ctrl.checked;
//                //checkboxes[i].prop('checked', true);
//            }
//        }
//    }
//    else {
//        for (var i = 0, n = checkboxes.length; i < n; i++) {
//            checkboxes[i].checked = ctrl.checked;
//            checkboxes[i].value = ctrl.checked;
//            // checkboxes[i].prop('checked', false);
//        }
//    }
//}









function portduescheckitm(ctrl, itmname, itmnameall) {
    
    var chk = true;
    if (ctrl.checked) {
        checkboxes = document.getElementsByName(itmname);
        for (var i = 0, n = checkboxes.length; i < n; i++) {
            if (!checkboxes[i].checked && checkboxes[i].style.display != 'none') {
                chk = false;
            }
        }
        checkboxesAll = document.getElementsByName(itmnameall);
        checkboxesAll[0].checked = chk;
    }
    else {
        chk = false;
        checkboxesAll = document.getElementsByName(itmnameall);
        checkboxesAll[0].checked = chk;
    }
}





