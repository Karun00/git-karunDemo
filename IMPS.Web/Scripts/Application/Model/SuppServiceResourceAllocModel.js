(function (ipmsRoot) {

    var ResourceSlot = function (data) {

        var self = this;

        self.SlotNumber = ko.observable(data ? data.SlotNumber : "");
        self.ResourceName = ko.observable(data ? data.ResourceName : "");
        self.ResourceID = ko.observable(data ? data.ResourceID : "");
        self.Status = ko.observable(data ? data.Status : "");
        self.ServiceTypeCode = ko.observable(data ? data.ServiceTypeCode : "");
        self.IsCraft = ko.observable(data ? data.IsCraft : false);
        self.ServiceReferenceID = ko.observable(data ? data.ServiceReferenceID : "");
        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.CraftID = ko.observable(data ? data.CraftID : "");
        self.CraftName = ko.observable(data ? data.CraftName : "");
        self.ShiftID = ko.observable(data ? data.ShiftID : "");
        self.TugResourceName = ko.observable(data ? data.TugResourceName : "");
        self.SlotPeriod = ko.observable(data ? data.SlotPeriod : "");
        self.IsChanged = ko.observable(data ? data.IsChanged : false);
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.SlotHeader = ko.observable(data ? data.SlotHeader : "");
        self.AllocationDate = ko.observable(data ? data.AllocationDate : "");
    }

    var Slot = function (data) {

        var self = this;

        self.SlotNumber = ko.observable(data ? data.SlotNumber : "");
        self.SlotPeriod = ko.observable(data ? data.SlotPeriod : "");
        self.AllocationDate = ko.observable(data ? data.AllocationDate : "");
        self.SlotHeader = ko.observable(data ? data.SlotHeader : "");
        self.ShiftID = ko.observable(data ? data.ShiftID : "");
    }

    var SuppServiceResourceAllocModel = function (data) {

        var self = this;

        self.ResourceAllocationID = ko.observable(data ? data.ResourceAllocationID : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "");
        self.ServiceReferenceID = ko.observable(data ? data.ServiceReferenceID : "");
        self.ServiceTypeCode = ko.observable(data ? data.ServiceTypeCode : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.CreatedBy = ko.observable(data ? data.CreatedBy : "");
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");
        self.AllocSlot = ko.observable(data ? data.AllocSlot : "");
        self.StartTime = ko.observable(data ? data.StartTime : "");
        self.EndTime = ko.observable(data ? data.EndTime : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.VesselName = ko.observable(data ? data.VesselName : "");
        self.AllocationDate = ko.observable(data ? moment(data.AllocationDate).format('YYYY-MM-DD HH:mm') : "");
        self.Quantity = ko.observable(data ? data.Quantity : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.ServiceTypeName = ko.observable(data ? data.ServiceTypeName : "");
        self.AcknowledgeDate = ko.observable(data ? data.AcknowledgeDate : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.ResourceID = ko.observable(data ? data.ResourceID : "");
        self.AnyDangerousGoodsonBoard = ko.observable(data ? data.AnyDangerousGoodsonBoard : "I");
        self.IsConfirm = ko.observable(data ? data.IsConfirm : false);

        self.Title = self.VCN() + ' - ' + self.VesselName();

        //var VesselStructure = "<table><tr><td style='text-align:right;width: 160px;'>VCN</td><td style='width:10%;'>:</td><td style='text-align:left'>" + self.VCN() + " </td></tr><tr><td  style='text-align:right'>Vessel Name</td><td>:</td><td  style='text-align:left'>" + self.VesselName() + " </td></tr> <tr><td  style='text-align:right'>Service Type</td><td>:</td><td  style='text-align:left'>" + self.ServiceTypeName() + " </td></tr> <tr><td  style='text-align:right'>Service Required at Berth</td><td>:</td><td  style='text-align:left'>" + self.BerthName() + " </td></tr> <tr><td  style='text-align:right'>Date of Service</td><td>:</td><td  style='text-align:left'>" + self.AllocationDate() + " </td></tr> <tr><td  style='text-align:right'>Quantity</td><td>:</td><td  style='text-align:left'>" + self.Quantity() + " </td></tr>  </table> ";
        var VesselStructure = "<table><tr><td style='text-align:right;width: 160px;'>VCN</td><td style='width:10%;'>:</td><td style='text-align:left'>" + self.VCN() + " </td></tr><tr><td  style='text-align:right'>Vessel Name</td><td>:</td><td  style='text-align:left'>" + self.VesselName() + " </td></tr> <tr><td  style='text-align:right'>Service Type</td><td>:</td><td  style='text-align:left'>" + self.ServiceTypeName() + " </td></tr> <tr><td  style='text-align:right'>Service Required at Berth</td><td>:</td><td  style='text-align:left'>" + self.BerthName() + " </td></tr> <tr><td  style='text-align:right'>Date of Service</td><td>:</td><td  style='text-align:left'>" + self.AllocationDate() + " </td></tr> </table> ";

        self.VesselDetails = ko.observable(VesselStructure);

        self.ResourceSlots = ko.observableArray(data ? ko.utils.arrayMap(data.ResourceSlots, function (item) {
            return new ResourceSlot(item);
        }) : []);
    }

    var ResourceCalendarSearchModel = function (data) {
        var self = this;
        self.ShiftID = ko.observable(data ? data.ShiftID : 0);
        self.OperationType = ko.observable(data ? data.OperationType : "WTST");
        self.AllocationDate = ko.observable(data ? data.AllocationDate : "");
        self.ServiceReferenceType = ko.observable(data ? data.ServiceReferenceType : "SUPP");
    }

    var ResourceCalendarModel = function (data) {
        var self = this;
        self.UserID = ko.observable(data ? data.UserID : 0);
        self.UserFullName = ko.observable(data ? data.UserFullName : "");
        self.ShiftID = ko.observable(data ? data.ShiftID : 0);
        self.AllocSlot = ko.observableArray(data ? data.AllocSlot : "");
        self.AllocationDate = ko.observable(data ? data.AllocationDate : "");
        self.AttendanceStatus = ko.observable(data ? data.AttendanceStatus : "");
        self.ResourceWorkStatus = ko.observable(data ? data.ResourceWorkStatus : "");
        self.AttendanceDate = ko.observable(data ? data.AttendanceDate : "");
        self.VCN = ko.observable(data ? data.VCN : "");
        self.Designation = ko.observable(data ? data.Designation : "");

        self.ResourceCalendarSlotDetails = ko.observableArray(data ? ko.utils.arrayMap(data.ResourceCalendarSlotDetails, function (item) {
            return new ResourceCalendarSlotDetailed(item);
        }) : []);
    }

    var ResourceCalendarSlotDetailed = function (data) {
        var self = this;
        self.VCN = ko.observable(data ? data.VCN : "");
        self.AttendanceStatus = ko.observable(data ? data.AttendanceStatus : "");
        self.SlotPeriod = ko.observable(data ? data.SlotPeriod : "");
        self.SlotText = ko.observable(data ? data.SlotText : "");
    }

    var ResourceCalendarHeaderModel = function (data) {
        var self = this;
        self.UserID = ko.observable(data ? data.UserID : "");
        self.UserFullName = ko.observable(data ? data.UserFullName : "");
        self.ResourceCalendarSlots = ko.observableArray(data ? ko.utils.arrayMap(data.ResourceCalendarSlots, function (item) {
            return new ResourceCalendarSlot(item);
        }) : []);

    }

    var ResourceCalendarSlot = function (data) {
        var self = this;
        self.SlotNumber = ko.observable(data ? data.SlotNumber : "");
        self.AllocSlot = ko.observable(data ? data.AllocSlot : "");
        self.ResourceDetails = ko.observable(data ? data.ResourceDetails : "");
    }

    ipmsRoot.ResourceCalendarSearchModel = ResourceCalendarSearchModel;
    ipmsRoot.ResourceCalendarModel = ResourceCalendarModel;
    ipmsRoot.ResourceCalendarSlotDetailed = ResourceCalendarSlotDetailed;
    ipmsRoot.ResourceCalendarHeaderModel = ResourceCalendarHeaderModel;
    ipmsRoot.ResourceCalendarSlot = ResourceCalendarSlot;
    ipmsRoot.Slot = Slot;
    ipmsRoot.ResourceSlot = ResourceSlot;
    ipmsRoot.SuppServiceResourceAllocModel = SuppServiceResourceAllocModel;

}(window.IPMSROOT));
