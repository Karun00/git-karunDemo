(function (ko, $, undefined) {

    ko.bindingHandlers.flash = {
        init: function (element) {
            $(element).hide();
        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            if (value) {
                $(element).stop().hide().text(value).fadeIn(function () {
                    clearTimeout($(element).data("timeout"));
                    $(element).data("timeout", setTimeout(function () {
                        $(element).fadeOut();
                        valueAccessor()(null);
                    }, 3000));
                });
            }
        },
        timeout: null
    };


    var isExtendYn;
    var chkSlotsExetended = 'N';
    var PreviousSlotStatus = '';
    var VesselMovement = function (data) {
        var self = this;
        self.VesselCallMovementID = data.VesselCallMovementID;
        self.VCN = ko.observable(data.VCN);
        self.MovementStatus = ko.observable(data.MovementStatus);
        self.MovementStatus = ko.observable(data.MovementStatus);
        self.VesselName = ko.observable(data.VesselName);
        self.VesselType = ko.observable(data.VesselType);
        self.ETA = ko.observable(data.ETA);
        self.ReasonForVisit = ko.observable(data.ReasonForVisit);
        self.LOA = ko.observable(data.LOA);
        self.MovementType = ko.observable(data.MovementType);
        self.MovementTypeCode = ko.observable(data.MovementTypeCode);
        self.GRT = ko.observable(data.GRT);
        self.PortCode = ko.observable(data.PortCode);
        self.ETB = ko.observable(data.ETB);
        self.ETUB = ko.observable(data.ETUB);
        self.ATB = ko.observable(data.ATB);
        self.ATUB = ko.observable(data.ATUB);
        self.MovementDateTime = ko.observable(data.MovementDateTime);
        self.Beam = ko.observable(data.Beam);
        self.ArrivalDraft = ko.observable(data.ArrivalDraft);
        self.GRT = ko.observable(data.GRT);
        self.DWT = ko.observable(data.DWT);
        self.PreviousPort = ko.observable(data.PreviousPort);
        self.TidalCondition = ko.observable(data.TidalCondition == "I" ? "No" : "Yes");
        self.DayLightCondition = ko.observable(data.DayLightCondition == "I" ? "No" : "Yes");
        self.ServiceRequestID = ko.observable(data.ServiceRequestID);
        self.SlotStatus = ko.observable(data.SlotStatus);
        self.SlotDate = ko.observable(data.SlotDate);
        self.Slot = ko.observable(data.Slot);
        self.CargoType = ko.observable(data.CargoType);
        self.CurrentBerth = ko.observable(data.CurrentBerth + " to " + data.ToBerth);
        self.WarpingBerth = ko.observable(data.CurrentBerth + " to " + data.CurrentBerth);
        self.SailingCurrentBerth = ko.observable(data.CurrentBerth);
        self.ToBerth = ko.observable(data.ToBerth);
        self.PreviousPort = ko.observable(data.PreviousPort != null ? data.PreviousPort : "NA");
        self.ToBollard = ko.observable(data.ToBollard);
        self.FromBollard = ko.observable(data.FromBollard);
        self.FromPositionPortCode = ko.observable(data ? data.FromPositionPortCode : "");
        self.AnyDangerousGoodsonBoard = ko.observable(data ? data.AnyDangerousGoodsonBoard : "I");
        self.ReasonForDisplay = ko.observable(data ? data.ReasonForDisplay : "Nil");
        self.EnteredDateTime = ko.observable(data ? data.EnteredDateTime : '');
        self.ReasonCode = ko.observable(data ? data.ReasonCode : '');
        self.PreviousSlot = ko.observable(data ? data.PreviousSlot : '');
        self.OverriddenSlot = ko.observable(data ? data.OverriddenSlot : '');
        self.SourceSlotName = ko.observable(data ? data.SourceSlotName : '');
        self.SourceSlotindex = ko.observable(data ? data.SourceSlotindex : '');
        self.TargetSlotName = ko.observable(data ? data.TargetSlotName : '');
        self.TargetSlotindex = ko.observable(data ? data.TargetSlotindex : '');
        self.SourceSlotStatus = ko.observable(data ? data.SourceSlotStatus : '');
        self.TargetSlotStatus = ko.observable(data ? data.TargetSlotStatus : '');
        self.SourceSlotForNo = ko.observable(data ? data.SourceSlotForNo : '');
        self.TargetSlotForNo = ko.observable(data ? data.TargetSlotForNo : '');
        self.SlotDateForNo = ko.observable(data.SlotDateForNo ? data.SlotDateForNo : '');
        self.Sidealongside = ko.observable(data.Sidealongside ? data.Sidealongside : '');
        self.Tidal = ko.observable(data.Tidal ? data.Tidal : '');
        self.OwnSteam = ko.observable(data.OwnSteam ? data.OwnSteam : '');
        self.Nomainengine = ko.observable(data.Nomainengine ? data.Nomainengine : '');
       

        self.movementsLoaded = ko.observableArray([]);


        self.Detail = ko.computed(function () {

            var vcnlength = self.VCN().length;
            var vcn = null;

            if (parseInt(vcnlength) > 17) {
                vcn = self.VCN().substring(0, 15) + '...';
            }
            else {
                vcn = self.VCN() + '                  ';
                vcn = vcn.substring(0, 17);
            }

            var vessellength = self.VesselName().length;
            var vesselName = null;

            if (parseInt(vessellength) > 15) {
                vesselName = self.VesselName().substring(0, 13) + '...';
            }
            else {
                vesselName = self.VesselName() + '                  ';
                vesselName = vesselName.substring(0, 15);
            }

            var movementtypelength = self.MovementType().length;
            var movementType = null;

            if (parseInt(movementtypelength) > 17) {
                movementType = self.MovementType().substring(0, 15) + '...';
            }
            else {
                movementType = self.MovementType() + '                  ';
                movementType = movementType.substring(0, 17);
            }

            vesselName = vesselName.toLowerCase().capitalize();
            return vcn + " " + vesselName + " " + movementType;
        });



        self.IsChanged = ko.observable(data ? data.IsChanged : "");
        self.TaskStatus = ko.observable(data ? data.TaskStatus : "");
        self.ExtendYn = ko.observable(data ? (data.ExtendYn != null) ? data.ExtendYn : isExtendYn : "N");
        self.ExtendedSlotDate = ko.observable(data ? data.ExtendedSlotDate : "");

        if (self.MovementTypeCode() == "ARMV") {
            //Arrival            
            if (self.AnyDangerousGoodsonBoard() == "A") {
                self.ToolTipDetails = ko.observable("<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td<td style='text-align:left;background-color:#FA370E'>" + self.VCN() + " </td><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'><b>Vessel Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ETA</b></td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Reason For Visit</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'><b>Cargo Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOA</b></td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'><b>Beam</b></td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'><b>Arrival Draft</b></td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>GRT</b></td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'><b>DWT</b></td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'><b>Side Along Side </b></td><td>:</td><td style='text-align:left'>" + self.Sidealongside() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Tidal Condition</b></td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'><b>Daylight Condition</b></td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td><td  style='text-align:right'><b>Is Tidal </b></td><td>:</td><td style='text-align:left'>" + self.Tidal() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>To Berth</b></td><td>:</td><td style='text-align:left'>" + self.ToBerth() + " </td><td  style='text-align:right'><b>To Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.ToBollard() + " </td><td  style='text-align:right'><b>Own Steam</b></td><td>:</td><td style='text-align:left'>" + self.OwnSteam() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Movement Type</b></td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'><b>Movement Date and Time</b></td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>No Main Engine</b></td><td>:</td><td style='text-align:left'>" + self.Nomainengine() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> </table>" + "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Reasons For Override</b></td><td>:</td><td style='text-align:left;line-height: 17px;w'>" + self.ReasonForDisplay());
            }
            else {
                self.ToolTipDetails = ko.observable("<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td><td style='text-align:left'>" + self.VCN() + " </td><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'><b>Vessel Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ETA</b></td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Reason For Visit</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'><b>Cargo Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOA</b></td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'><b>Beam</b></td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'><b>Arrival Draft</b></td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>GRT</b></td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'><b>DWT</b></td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'><b>Side Along Side </b></td><td>:</td><td style='text-align:left'>" + self.Sidealongside() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Tidal Condition</b></td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'><b>Daylight Condition</b></td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td><td  style='text-align:right'><b>Is Tidal </b></td><td>:</td><td style='text-align:left'>" + self.Tidal() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>To Berth</b></td><td>:</td><td style='text-align:left'>" + self.ToBerth() + " </td><td  style='text-align:right'><b>To Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.ToBollard() + " </td><td  style='text-align:right'><b>Own Steam</b></td><td>:</td><td style='text-align:left'>" + self.OwnSteam() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Movement Type</b></td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'><b>Movement Date and Time</b></td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>No Main Engine</b></td><td>:</td><td style='text-align:left'>" + self.Nomainengine() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> </table> " + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> </table>" + "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Reasons For Override</b></td><td>:</td><td style='text-align:left;line-height: 17px;'>" + self.ReasonForDisplay());
            }
        }
        if (self.MovementTypeCode() == "SGMV") {
            //Sailing
            if (self.AnyDangerousGoodsonBoard() == "A") {
                self.ToolTipDetails = ko.observable("<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td><td style='text-align:left;background-color:#FA370E'>" + self.VCN() + " </td><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'><b>Vessel Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ETA</b></td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Reason For Visit</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'><b>Cargo Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOA</b></td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'><b>Beam</b></td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'><b>Arrival Draft</b></td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>GRT</b></td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'><b>DWT</b></td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'><b>Side Along Side </b></td><td>:</td><td style='text-align:left'>" + self.Sidealongside() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:0px solid #ddd;'><td  style='text-align:right'><b>Current Berth</b></td><td>:</td><td style='text-align:left'>" + self.SailingCurrentBerth() + " </td><td  style='text-align:right'><b>Tidal Condition</b></td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'><b>Daylight Condition</b></td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td></tr> <tr style='border-bottom:2px solid #ddd;'><td  style='text-align:right'><b>Movement Type</b></td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'><b>Movement Date and Time</b></td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Is Tidal </b></td><td>:</td><td style='text-align:left'>" + self.Tidal() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:2px solid #ddd;'><td  style='text-align:right'><b>Own Steam</b></td><td>:</td><td style='text-align:left'>" + self.OwnSteam() + " </td><td  style='text-align:right'><b>No Main Engine</b></td><td>:</td><td style='text-align:left'>" + self.Nomainengine() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></table> " + "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Reasons For Override</b></td><td>:</td><td style='text-align:left;line-height: 17px;'>" + self.ReasonForDisplay());
            }
            else {
                self.ToolTipDetails = ko.observable("<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td><td style='text-align:left'>" + self.VCN() + " </td><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'><b>Vessel Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ETA</b></td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Reason For Visit</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'><b>Cargo Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOA</b></td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'><b>Beam</b></td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'><b>Arrival Draft</b></td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>GRT</b></td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'><b>DWT</b></td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'><b>Side Along Side </b></td><td>:</td><td style='text-align:left'>" + self.Sidealongside() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:0px solid #ddd;'><td  style='text-align:right'><b>Current Berth</b></td><td>:</td><td style='text-align:left'>" + self.SailingCurrentBerth() + " </td><td  style='text-align:right'><b> Tidal Condition</b></td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'><b>Daylight Condition</b></td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td></tr> <tr style='border-bottom:2px solid #ddd;'><td  style='text-align:right'><b>Movement Type</b></td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'><b>Movement Date and Time</b></td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Is Tidal </b></td><td>:</td><td style='text-align:left'>" + self.Tidal() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr style='border-bottom:2px solid #ddd;'><td  style='text-align:right'><b>Own Steam</b></td><td>:</td><td style='text-align:left'>" + self.OwnSteam() + " </td><td  style='text-align:right'><b>No Main Engine</b></td><td>:</td><td style='text-align:left'>" + self.Nomainengine() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> </table> " + "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Reasons For Override</b></td><td>:</td><td style='text-align:left;line-height: 17px;'>" + self.ReasonForDisplay());
            }
        }
        if (self.MovementTypeCode() == "SHMV") {
            //Shifting
            if (self.AnyDangerousGoodsonBoard() == "A") {
                self.ToolTipDetails = ko.observable("<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td><td style='text-align:left;background-color:#FA370E'>" + self.VCN() + " </td><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'><b>Vessel Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ETA</b></td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Reason For Visit</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'><b>Cargo Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOA</b></td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'><b>Beam</b></td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'><b>Arrival Draft</b></td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>GRT</b></td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'><b>DWT</b></td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'><b>Side Along Side </b></td><td>:</td><td style='text-align:left'>" + self.Sidealongside() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:0px solid #ddd;'><td  style='text-align:right'><b>Current Berth</b></td><td>:</td><td style='text-align:left'>" + self.CurrentBerth() + " </td><td  style='text-align:right'><b> Tidal Condition</b></td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'><b>Daylight Condition</b></td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td></tr> <tr style='border:2px solid #f00;'><td  style='text-align:right'><b>To Berth</b></td><td>:</td><td style='text-align:left'>" + self.ToBerth() + " </td><td  style='text-align:right'><b>Current Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.FromBollard() + " </td><td  style='text-align:right'><b>To Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.ToBollard() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Movement Type</b></td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'><b>Movement Date and Time</b></td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Is Tidal </b></td><td>:</td><td style='text-align:left'>" + self.Tidal() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Own Steam</b></td><td>:</td><td style='text-align:left'>" + self.OwnSteam() + " </td><td  style='text-align:right'><b>No Main Engine</b></td><td>:</td><td style='text-align:left'>" + self.Nomainengine() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> </table> " + "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Reasons For Override</b></td><td>:</td><td style='text-align:left;line-height: 17px;'>" + self.ReasonForDisplay());
            }
            else {
                self.ToolTipDetails = ko.observable("<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td><td style='text-align:left'>" + self.VCN() + " </td><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'><b>Vessel Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ETA</b></td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Reason For Visit</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'><b>Cargo Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOA</b></td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'><b>Beam</b></td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'><b>Arrival Draft</b></td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>GRT</b></td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'><b>DWT</b></td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'><b>Side Along Side </b></td><td>:</td><td style='text-align:left'>" + self.Sidealongside() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:0px solid #ddd;'><td  style='text-align:right'><b>Current Berth</b></td><td>:</td><td style='text-align:left'>" + self.CurrentBerth() + " </td><td  style='text-align:right'><b> Tidal Condition</b></td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'><b>Daylight Condition</b></td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td></tr> <tr style='border:2px solid #f00;'><td  style='text-align:right'><b>To Berth</b></td><td>:</td><td style='text-align:left'>" + self.ToBerth() + " </td><td  style='text-align:right'><b>Current Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.FromBollard() + " </td><td  style='text-align:right'><b>To Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.ToBollard() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Movement Type</b></td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'><b>Movement Date and Time</b></td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Is Tidal </b></td><td>:</td><td style='text-align:left'>" + self.Tidal() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Own Steam</b></td><td>:</td><td style='text-align:left'>" + self.OwnSteam() + " </td><td  style='text-align:right'><b>No Main Engine</b></td><td>:</td><td style='text-align:left'>" + self.Nomainengine() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></table> " + "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Reasons For Override</b></td><td>:</td><td style='text-align:left;line-height: 17px;'>" + self.ReasonForDisplay());
            }
        }
        if (self.MovementTypeCode() == "WRMV") {
            //Warping
            if (self.AnyDangerousGoodsonBoard() == "A") {
                self.ToolTipDetails = ko.observable("<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td><td style='text-align:left;background-color:#FA370E'>" + self.VCN() + " </td><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'><b>Vessel Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ETA</b></td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Reason For Visit</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'><b>Cargo Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOA</b></td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'><b>Beam</b></td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'><b>Arrival Draft</b></td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>GRT</b></td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'><b>DWT</b></td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'><b>Side Along Side </b></td><td>:</td><td style='text-align:left'>" + self.Sidealongside() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:0px solid #ddd;'><td  style='text-align:right'><b>Tidal Condition</b></td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'><b>Daylight Condition</b></td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td><td  style='text-align:right'><b>Is Tidal </b></td><td>:</td><td style='text-align:left'>" + self.Tidal() + " </td></tr> <tr style='border:2px solid #f00;'><td  style='text-align:right'><b>Current Berth</b></td><td>:</td><td style='text-align:left'>" + self.WarpingBerth() + " </td><td  style='text-align:right'><b>Current Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.FromBollard() + " </td><td  style='text-align:right'><b>To Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.ToBollard() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Movement Type</b></td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'><b>Movement Date and Time</b></td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD HH:mm') + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Own Steam</b></td><td>:</td><td style='text-align:left'>" + self.OwnSteam() + " </td><td  style='text-align:right'><b>No Main Engine</b></td><td>:</td><td style='text-align:left'>" + self.Nomainengine() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> </table> " + "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Reasons For Override</b></td><td>:</td><td style='text-align:left;line-height: 17px;'>" + self.ReasonForDisplay());
            }
            else {
                self.ToolTipDetails = ko.observable("<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>VCN</b></td><td>:</td><td style='text-align:left'>" + self.VCN() + " </td><td  style='text-align:right'><b>Vessel Name</b></td><td>:</td><td style='text-align:left'>" + self.VesselName() + " </td><td  style='text-align:right'><b>Vessel Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.VesselType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>ETA</b></td><td>:</td><td style='text-align:left'>" + moment(self.ETA()).format('YYYY-MM-DD HH:mm') + " </td><td  style='text-align:right'><b>Reason For Visit</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.ReasonForVisit() + " </td><td  style='text-align:right'><b>Cargo Type</b></td><td>:</td><td style='text-align:left;width:200px; line-height:17px;'>" + self.CargoType() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>LOA</b></td><td>:</td><td style='text-align:left'>" + self.LOA() + " </td><td  style='text-align:right'><b>Beam</b></td><td>:</td><td style='text-align:left'>" + self.Beam() + " </td><td  style='text-align:right'><b>Arrival Draft</b></td><td>:</td><td style='text-align:left'>" + self.ArrivalDraft() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>GRT</b></td><td>:</td><td style='text-align:left'>" + self.GRT() + " </td><td  style='text-align:right'><b>DWT</b></td><td>:</td><td style='text-align:left'>" + self.DWT() + " </td><td  style='text-align:right'><b>Side Along Side </b></td><td>:</td><td style='text-align:left'>" + self.Sidealongside() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> <tr style='border-bottom:0px solid #ddd;'><td  style='text-align:right'><b>Tidal Condition</b></td><td>:</td><td style='text-align:left'>" + self.TidalCondition() + " </td><td  style='text-align:right'><b>Daylight Condition</b></td><td>:</td><td style='text-align:left'>" + self.DayLightCondition() + " </td><td  style='text-align:right'><b>Is Tidal </b></td><td>:</td><td style='text-align:left'>" + self.Tidal() + " </td></tr> <tr style='border:2px solid #f00;'><td  style='text-align:right'><b>Current Berth</b></td><td>:</td><td style='text-align:left'>" + self.WarpingBerth() + " </td><td  style='text-align:right'><b>Current Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.FromBollard() + " </td><td  style='text-align:right'><b>To Bollard No's</b></td><td>:</td><td style='text-align:left'>" + self.ToBollard() + " </td></tr> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Movement Type</b></td><td>:</td><td style='text-align:left'>" + self.MovementType() + " </td><td  style='text-align:right'><b>Movement Date and Time</b></td><td>:</td><td style='text-align:left'>" + moment(self.MovementDateTime()).format('YYYY-MM-DD HH:mm') + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Own Steam</b></td><td>:</td><td style='text-align:left'>" + self.OwnSteam() + " </td><td  style='text-align:right'><b>No Main Engine</b></td><td>:</td><td style='text-align:left'>" + self.Nomainengine() + " </td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr> </table> " + "<table style='border:0px solid #f00; line-height:30px;'> <tr style='border-bottom:1px solid #ddd;'><td  style='text-align:right'><b>Reasons For Override</b></td><td>:</td><td style='text-align:left;line-height: 17px;'>" + self.ReasonForDisplay());
            }
        }
    };

    String.prototype.capitalize = function () {
        return this.replace(/(^|\s)([a-z])/g, function (m, p1, p2) { return p1 + p2.toUpperCase(); });
    };

    var Slot = function (section, slotNumber, slotDate, vesselMovements, blockedSlots) {
        var self = this;
        if (vesselMovements != undefined)
            self.VesselMovements = ko.observableArray($.map(vesselMovements, function (item) { return new VesselMovement(item) }));

        self.slotNumber = slotNumber;
        self.slotDate = slotDate;

        self.slotHeading = ko.computed(function () {
            if (section == "Planned") {
                var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                var date = (new Date(self.slotDate)).getDate();
                var month = m_names[(new Date(self.slotDate)).getMonth()];
                return date + " - " + month + " " + self.slotNumber;
            }
        });

        self.BlockedStatus = "#FFFFFF";



        self.Slotnum = (slotNumber.replace(/:/g, '')).substring(0, 4) + (slotDate.replace(/-/g, '')).substring(4, 8);


        $.each(blockedSlots, function (index, item) {

            if (item.SlotPeriod == slotNumber) {
                self.BlockedStatus = "#e02222";
                self.ToolTipDetails1 = "Slots are Blocked for Reason - " + item.ReasonName;
                setTimeout(function () {
                    $("#Blocked" + self.Slotnum).kendoTooltip({
                        content: self.ToolTipDetails1
                    }, 10);
                }, 10);

                return;
            }
        });



        if (vesselMovements != undefined)
            this.VesselMovements.id = section + "," + slotNumber + "," + slotDate;

    };

    var AutoSlotsModel = function () {

        var self = this;
        self.plannedSlots = ko.observableArray([]);
        self.pendingSlots = ko.observableArray([]);
        self.slotDuration = ko.observable();
        self.slotDisplayPeriod = ko.observable();
        self.noOfVesselsPerSlot = ko.observable();
        self.extendedSlots = ko.observable();
        self.IsCurrentDate = ko.observable(true);
        self.viewModelHelper1 = new IPMSROOT.viewModelHelper();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.SlotsCount = ko.observableArray();
        self.isVisible = ko.observable(false);
        self.isEnabled = ko.observable(false);
        self.selectedVesselColumn = ko.observable();
        self.isChanged = ko.observable(false);
        self.activeSlots = ko.observableArray();
        self.blockedSlots = ko.observableArray();
        self.IsDateChange = ko.observable(false);




        self.startTime = ko.observableArray();
        self.endTime = ko.observableArray();


        var curdate = new Date();
        self.CurrentDate = ko.observable(new Date(curdate.getFullYear(), curdate.getMonth(), curdate.getDate()));

        $('.displaytxt').text(moment(self.CurrentDate()).format('MMM DD, YYYY'));
        self.Date = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });

        self.ResourceDate = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });

        self.ResourceOnClick = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if (self.isVisible()) {

                if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                    var dt = new Date();
                    var dt1 = new Date();
                    var currentDate = self.CurrentDate();
                    dt1.setDate(dt1.getDate() + 1);

                    if (((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) || ((dt1.getDate() == currentDate.getDate()) && (dt1.getMonth() == currentDate.getMonth()) && (dt1.getFullYear() == currentDate.getFullYear()))) {

                        $.confirm({
                            'title': ' Automated Slotting',
                            'message': 'Do you want to Confirm?' + "<table style='width:100%;'><tr><td style='text-align:right'> VCN</td><td width='10px;'>:</td><td style='text-align:left'>" + data.VCN() + "</td></tr><tr><td style='text-align:right'> Vessel Name</td><td>:</td><td style='text-align:left'>" + data.VesselName() + "</td></tr><tr><td style='text-align:right'> Movement Type</td><td>:</td><td style='text-align:left'>" + data.MovementType() + "</td></tr></table>",
                            'buttons': {
                                'Yes': {
                                    'class': 'blue',
                                    'action': function () {
                                        toastr.success("Vessel is confirmed successfully, Click on “Save” to effect the confirmed changes.", "Automated Slotting");
                                        data.SlotStatus("CNFR");
                                        data.IsChanged(true);
                                        self.isChanged(true);
                                    }
                                },
                                'No': {
                                    'class': 'gray',
                                    'action': function () {
                                    }
                                }
                            }
                        });
                    }
                    else {
                        toastr.options.timeOut = 3000;
                        toastr.warning("Can not confirm future date vessel beyond 48 hours.", "Automated Slotting");
                        return false;
                    }
                }

                else if (moment(self.Date()).format('YYYY-MM-DD') == moment(self.CurrentDate()).format('YYYY-MM-DD')) {

                    self.GetActiveSlots();
                    var status = false;

                    $.each(self.activeSlots(), function (index, value) {
                        if (value == data.Slot()) {
                            status = true;
                            return false;
                        }
                    });

                    if (status) {
                        if (data.SlotStatus() == 'CNFR') {
                            toastr.warning("Vessel is already confirmed.", "Automated Slotting");
                            return false;
                        }

                        if (data.SlotStatus() == 'SCHD') {
                            toastr.warning("Vessel is scheduled.", "Automated Slotting");
                            return false;
                        }

                        if (data.SlotStatus() == 'PLND' || data.SlotStatus() == 'OVRD') {

                            $.confirm({
                                'title': ' Automated Slotting',
                                'message': 'Do you want to Confirm?' + "<table style='width:100%;'><tr><td style='text-align:right'> VCN</td><td width='10px;'>:</td><td style='text-align:left'>" + data.VCN() + "</td></tr><tr><td style='text-align:right'> Vessel Name</td><td>:</td><td style='text-align:left'>" + data.VesselName() + "</td></tr><tr><td style='text-align:right'> Movement Type</td><td>:</td><td style='text-align:left'>" + data.MovementType() + "</td></tr></table>",
                                'buttons': {
                                    'Yes': {
                                        'class': 'blue',
                                        'action': function () {
                                            toastr.success("Vessel is confirmed successfully, Click on “Save” to effect the confirmed changes.", "Automated Slotting");
                                            data.SlotStatus("CNFR");
                                            data.IsChanged(true);
                                            self.isChanged(true);

                                        }
                                    },
                                    'No': {
                                        'class': 'gray',
                                        'action': function () {
                                        }
                                    }
                                }
                            });
                        }
                    }
                    else {
                        toastr.options.timeOut = 3000;
                        toastr.warning("Can not confirm previous slot vessel.", "Automated Slotting");
                        return false;
                    }
                }

                else {
                    toastr.options.timeOut = 3000;
                    toastr.warning("Can not confirm previous date vessel.", "Automated Slotting");
                    return false;
                }
            }
            else {
                toastr.options.timeOut = 3000;
                toastr.warning("Can not confirm vessel, you do not have permission.", "Automated Slotting");
                return false;
            }
        }

        self.PortName = ko.observable();

        self.viewMode = ko.observable();
        self.ReasonTypes = ko.observable();
        self.ReasonTypeCodes = ko.observable();
        self.Reason = ko.observable();
        self.PreviousSlot = ko.observable();
        self.OverriddenSlot = ko.observable();
        self.ReasonCode = ko.observable();
        self.VesselCallMovementID = ko.observable();
        self.isspanOverrideReason = ko.observable(false);
        // self.isResonVisible = ko.observable(false);


        self.autoSlot = ko.observable(new IPMSROOT.AutomatedSlotting());


        self.GetReasonTypes = function () {
            self.viewModelHelper.apiGet('api/ReasonTypes', null, function (result) {
                self.ReasonTypes(result);
            }, null, null, false);

        }

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.AutomatedConfigurationDetails();
            self.GetReasonTypes();
            self.LoadPlannedMovements();
            self.LoadUnPlannedMovements();
            self.LoadSlotsCount();
            self.LoadPortName();
            self.LoadPrivileges();

            if (self.isVisible() == true) {
                self.isEnabled(true);
            }
            else {
                self.isEnabled(false);
            }
            self.isspanOverrideReason(false);
            $("#spanOverrideReason").text('');
        }


        self.LoadPrivileges = function () {

            self.viewModelHelper1.apiGet('api/GetPrivilegesByUserIDAndEntityCode/' + 'AUTOSLOT', null,
                function (result) {
                    self.isVisible(result);
                }, null, null, false);
        }

        self.LoadSlotsCount = function () {

            self.viewModelHelper1.apiGet('api/GetSlotConfiguration/{slotDate}', { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.SlotsCount);
                });
        }

        self.LoadPreviousDay = function () {
            // confirmation box - start
            if (self.isChanged()) {
                $.confirm({
                    'title': 'Automated Slotting',
                    'message': 'All changes done to slot planning will be lost. Do you want proceed anyway?',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {
                                self.isChanged(false);
                                var dt = new Date();
                                var currentDate = self.CurrentDate();
                                var dt1 = new Date();
                                dt1.setDate(dt1.getDate() + 1);

                                currentDate.setDate(currentDate.getDate() - 1);
                                if (((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) || ((dt1.getDate() == currentDate.getDate()) && (dt1.getMonth() == currentDate.getMonth()) && (dt1.getFullYear() == currentDate.getFullYear()))) {
                                    if (self.isVisible() == true) {
                                        self.IsCurrentDate(true);
                                        self.isEnabled(true);
                                    }
                                    else {
                                        self.IsCurrentDate(false);
                                        self.isEnabled(false);
                                    }
                                }
                                else {
                                    self.IsCurrentDate(false);
                                    self.isEnabled(false);
                                }

                                self.CurrentDate(currentDate);
                                $('.displaytxt').text(moment(currentDate).format('MMM DD, YYYY'));
                                self.plannedSlots([]);

                                self.AutomatedConfigurationDetails();
                                self.LoadPlannedMovements();
                                self.LoadUnPlannedMovements();
                            }
                        },
                        'No': {
                            'class': 'gray',
                            'action': function () {
                            }
                        }
                    }
                });
                //confirmation box - end
            }
            else {
                self.isChanged(false);
                var dt = new Date();
                var currentDate = self.CurrentDate();
                var dt1 = new Date();
                dt1.setDate(dt1.getDate() + 1);

                currentDate.setDate(currentDate.getDate() - 1);
                if (((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) || ((dt1.getDate() == currentDate.getDate()) && (dt1.getMonth() == currentDate.getMonth()) && (dt1.getFullYear() == currentDate.getFullYear()))) {
                    if (self.isVisible() == true) {
                        self.IsCurrentDate(true);
                        self.isEnabled(true);
                    }
                    else {
                        self.IsCurrentDate(false);
                        self.isEnabled(false);
                    }
                }
                else {
                    self.IsCurrentDate(false);
                    self.isEnabled(false);
                }

                self.CurrentDate(currentDate);
                $('.displaytxt').text(moment(currentDate).format('MMM DD, YYYY'));
                self.plannedSlots([]);

                self.AutomatedConfigurationDetails();
                self.LoadPlannedMovements();
                self.LoadUnPlannedMovements();
            }
        }

        self.LoadNextDay = function () {
            // confirmation box - start
            if (self.isChanged()) {
                $.confirm({
                    'title': 'Automated Slotting',
                    'message': 'All changes done to slot planning will be lost. Do you want proceed anyway?',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {

                                self.isChanged(false);
                                var dt = new Date();
                                var dt1 = new Date();
                                var currentDate = self.CurrentDate();
                                var futureDate = self.CurrentDate();
                                dt1.setDate(dt1.getDate() + 1);
                                currentDate.setDate(currentDate.getDate() + 1);
                                if (((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) || ((dt1.getDate() == currentDate.getDate()) && (dt1.getMonth() == currentDate.getMonth()) && (dt1.getFullYear() == currentDate.getFullYear()))) {
                                    if (self.isVisible() == true) {
                                        self.IsCurrentDate(true);
                                        self.isEnabled(true);
                                    }
                                    else {
                                        self.IsCurrentDate(false);
                                        self.isEnabled(false);
                                    }
                                }
                                else {
                                    self.IsCurrentDate(false);
                                    self.isEnabled(false);
                                }

                                self.CurrentDate(currentDate);
                                $('.displaytxt').text(moment(currentDate).format('MMM DD, YYYY'));
                                self.plannedSlots([]);
                                self.AutomatedConfigurationDetails();
                                self.LoadPlannedMovements();
                                self.LoadUnPlannedMovements();
                            }
                        },
                        'No': {
                            'class': 'gray',
                            'action': function () {
                            }
                        }
                    }
                });
                //confirmation box - end
            }
            else {

                self.isChanged(false);
                var dt = new Date();
                var dt1 = new Date();
                var currentDate = self.CurrentDate();
                var futureDate = self.CurrentDate();
                dt1.setDate(dt1.getDate() + 1);
                currentDate.setDate(currentDate.getDate() + 1);
                if (((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) || ((dt1.getDate() == currentDate.getDate()) && (dt1.getMonth() == currentDate.getMonth()) && (dt1.getFullYear() == currentDate.getFullYear()))) {
                    if (self.isVisible() == true) {
                        self.IsCurrentDate(true);
                        self.isEnabled(true);
                    }
                    else {
                        self.IsCurrentDate(false);
                        self.isEnabled(false);
                    }
                }
                else {
                    self.IsCurrentDate(false);
                    self.isEnabled(false);
                }

                self.CurrentDate(currentDate);
                $('.displaytxt').text(moment(currentDate).format('MMM DD, YYYY'));
                self.plannedSlots([]);
                self.AutomatedConfigurationDetails();
                self.LoadPlannedMovements();
                self.LoadUnPlannedMovements();
            }
        }

        self.LoadSlotConfiguration = function (data) {


            //TODO: Get configuration from the server SYNCRHONOUSLY.  Don't use async here          


            self.slotDuration(data.Duration);
            self.slotDisplayPeriod(data.OperationalPeriod);

            if (isExtendYn != 'Y')
                self.noOfVesselsPerSlot(data.NoofSlots);
            else
                self.noOfVesselsPerSlot(parseInt(data.NoofSlots) + parseInt(data.ExtendableSlots));

            self.extendedSlots(data.ExtendableSlots);

        }

        self.ExtendSlots = function (data) {

            if (self.isVisible()) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";

                if (self.PlannedSlotsCount() > 0) {
                    if (chkSlotsExetended != 'Y' && isExtendYn != 'Y') {
                        isExtendYn = 'Y';
                        self.noOfVesselsPerSlot(parseInt(self.noOfVesselsPerSlot()) + self.extendedSlots());
                        toastr.success("Slots are extended to  " + self.extendedSlots() + "  successfully.", "Automated Slotting");
                    }
                    else {
                        toastr.warning("Already slots are extended.", "Automated Slotting");
                    }
                }
                else {
                    toastr.warning("You can not extend slots. Planned slots are not available.", "Automated Slotting");
                }
            }
            else {
                toastr.options.timeOut = 3000;
                toastr.warning("Can not extended slots, you do not have permission.", "Automated Slotting");
                return false;
            }
        }

        self.AutomatedConfigurationDetails = function () {
            self.AutomatedExtendableYesNo();
            self.viewModelHelper1.apiGet('api/ConfigurationDetails/{slotDate}',
           { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
             function (result) {

                 self.LoadSlotConfiguration(result);


             }, null, null, false);
        }

        self.LoadUnPlannedMovements = function () {

            var pendingVesselMovements;
            self.pendingSlots.removeAll();

            self.viewModelHelper1.apiGet('api/UnPlannedVesselsDet/{slotDate}',
           { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
             function (result) {
                 pendingVesselMovements = result;
             }, null, null, false);

            var pendingArrivals = $.map(pendingVesselMovements, function (item) { if (item.MovementTypeCode == "ARMV") return item; });
            var pendingSailings = $.map(pendingVesselMovements, function (item) { if (item.MovementTypeCode == "SGMV") return item; });
            var pendingShiftings = $.map(pendingVesselMovements, function (item) { if (item.MovementTypeCode == "SHMV") return item; });
            var pendingWarpings = $.map(pendingVesselMovements, function (item) { if (item.MovementTypeCode == "WRMV") return item; });

            var pendingArrivalSlot = new Slot("Unplanned", "Arrivals", "", pendingArrivals, "");
            var pendingSailingsSlot = new Slot("Unplanned", "Sailings", "", pendingSailings, "");
            var pendingShiftingsSlot = new Slot("Unplanned", "Shiftings", "", pendingShiftings, "");
            var pendingWarpingsSlot = new Slot("Unplanned", "Warpings", "", pendingWarpings, "");


            self.pendingSlots.push(pendingArrivalSlot);
            self.pendingSlots.push(pendingSailingsSlot);
            self.pendingSlots.push(pendingShiftingsSlot);
            self.pendingSlots.push(pendingWarpingsSlot);
        }

        self.PlannedSlotsCount = ko.observable();

        self.LoadPlannedMovements = function () {
            var alreadyPlannedMvmtnt;
            debugger;
            self.viewModelHelper1.apiGet('api/AutomatedSlots/{slotDate}',
        { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
          function (result) {

              self.PlannedSlotsCount(result.length);


              if (result.length > 0) {
                  $.each(result, function (index, data) {
                      alreadyPlannedMvmtnt = result;
                  });
              } else {

              }
          }, null, null, false);

            self.GetBlockedSlots();

            var slotDuration = self.slotDuration(); // in hrs
            var displayPeriod = self.slotDisplayPeriod();

            var noOfSlots = 1440 / slotDuration; //Always 24 hrs

            var startTime = parseInt(displayPeriod);

            var endTime = 1440;
            var displayEndTime = endTime;

            if (displayEndTime != 1440) displayEndTime = displayEndTime + 1440;
            var time = startTime;
            for (var i = 0; i < noOfSlots; i++) {
                var dateChanged = false;

                var displayTime = time;
                var slotDate = self.CurrentDate();
                var slotDate1 = slotDate;

                var startperiod = self.ConvertMinsToHrs(displayTime);
                var endperiod = self.ConvertMinsToHrs((displayTime + slotDuration));

                if (displayTime + slotDuration >= 1440) {
                    time = (time + slotDuration) - 1440;
                    endperiod = self.ConvertMinsToHrs(time);

                    dateChanged = true;
                }
                else {
                    time = time + slotDuration;
                }


                slotDate1 = moment(new Date(slotDate1)).format('YYYY-MM-DD');

                var slotName = startperiod.toString() + "-" + endperiod.toString();
                var chkDisplaymin = displayTime;
                var chkDisplayendmin = displayTime + slotDuration;

                var slotMovements;
                if (alreadyPlannedMvmtnt != undefined) {
                    slotMovements = $.map(alreadyPlannedMvmtnt, function (item) {
                        var plnDate = moment(item.SlotDate).format('YYYY-MM-DD');

                        if (plnDate == slotDate1) {
                            var plnDate1 = new Date(moment(item.SlotDate).format('YYYY-MM-DD HH:mm'));
                            var totalminutes = (plnDate1.getHours()) * 60 + plnDate1.getMinutes();

                            if (chkDisplaymin > chkDisplayendmin) {
                                if (totalminutes <= chkDisplayendmin && chkDisplaymin >= totalminutes) {
                                    return item;
                                }
                                if (totalminutes >= chkDisplayendmin && chkDisplaymin <= totalminutes) {
                                    return item;
                                }
                            }

                            if (totalminutes >= chkDisplaymin && totalminutes < chkDisplayendmin) {
                                return item;
                            }


                        }
                    });
                }
                else {
                    slotMovements = [];
                }



                self.plannedSlots.push(new Slot("Planned", slotName, slotDate1, slotMovements, self.blockedSlots()));

                //It is to overcome an issue with strange behavious.  Changing slotDate is changing the observable value self.CurrentDate()
                if (dateChanged == true) {
                    slotDate.setDate(slotDate.getDate() + 1);
                    self.IsDateChange(true);
                }

            }
            if (self.IsDateChange()) {
                self.CurrentDate().setDate(self.CurrentDate().getDate() - 1);
            }
        }

        self.ConvertMinsToHrs = function (minutes) {
            var hours = Math.trunc(minutes / 60);
            var minutes = minutes % 60;
            hours = hours < 10 ? '0' + hours : hours;
            minutes = minutes < 10 ? '0' + minutes : minutes;
            return hours + ':' + minutes;
        }

        self.isSlotFull = function (parent) {
            return (parent().length - 1) < self.noOfVesselsPerSlot();

        };

        self.SavePlan = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if (self.isVisible()) {
                if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {

                    var dt = new Date();
                    var dt1 = new Date();
                    var currentDate = self.CurrentDate();
                    dt1.setDate(dt1.getDate() + 1);

                    if (((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) || ((dt1.getDate() == currentDate.getDate()) && (dt1.getMonth() == currentDate.getMonth()) && (dt1.getFullYear() == currentDate.getFullYear()))) {
                        if (self.isChanged()) {

                            // confirmation box - start
                            $.confirm({
                                'title': ' Automated Slotting',
                                'message': 'Do you want to save changes?',
                                'buttons': {
                                    'Yes': {
                                        'class': 'blue',
                                        'action': function () {
                                            var movements = new Array();
                                            $.each(self.plannedSlots(), function (index, data) {
                                                if (data.VesselMovements().length > 0) {
                                                    $.each(data.VesselMovements(), function (index, data1) {
                                                        data1.ExtendYn(isExtendYn);
                                                        data1.ExtendedSlotDate(moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A'));
                                                        movements.push(data1);
                                                    });
                                                }
                                            });

                                            $.each(self.pendingSlots(), function (index, data) {
                                                if (data.VesselMovements().length > 0) {
                                                    $.each(data.VesselMovements(), function (index, data1) {
                                                        movements.push(data1);
                                                    });
                                                }
                                            });

                                            if (movements.length > 0) {
                                                self.viewModelHelper1.apiPut('api/UpdateVesselSoltDtl', ko.toJSON(movements), function Message(msg) {
                                                    toastr.success("Vessel details are saved successfully.", "Automated Slotting");

                                                    self.isChanged(false);
                                                    self.plannedSlots.removeAll();
                                                    self.pendingSlots.removeAll();
                                                    self.LoadPlannedMovements();
                                                    self.LoadUnPlannedMovements();
                                                }, null, null, false);
                                            }
                                        }
                                    },
                                    'No': {
                                        'class': 'gray',
                                        'action': function () {
                                        }
                                    }
                                }
                            });
                            //confirmation box - end
                        }
                        else {
                            toastr.warning("Vessels are not changed.", "Automated Slotting");
                            return false;
                        }
                    }
                    else {
                        toastr.options.timeOut = 3000;
                        toastr.warning("Can not save future date vessels beyond 48 hours.", "Automated Slotting");
                        return false;
                    }
                }
                else {
                    if (self.isChanged()) {
                        // confirmation box - start
                        $.confirm({
                            'title': ' Automated Slotting',
                            'message': 'Do you want to save changes?',
                            'buttons': {
                                'Yes': {
                                    'class': 'blue',
                                    'action': function () {

                                        var movements = new Array();
                                        $.each(self.plannedSlots(), function (index, data) {
                                            if (data.VesselMovements().length > 0) {
                                                $.each(data.VesselMovements(), function (index, data1) {
                                                    data1.ExtendYn(isExtendYn);
                                                    data1.ExtendedSlotDate(moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A'));
                                                    // data1.ReasonCode = b;
                                                    //data1.PreviousSlot = self.PreviousSlot;
                                                    //data1.OverriddenSlot = self.OverriddenSlot;
                                                    movements.push(data1);
                                                });
                                            }
                                        });

                                        $.each(self.pendingSlots(), function (index, data) {
                                            if (data.VesselMovements().length > 0) {
                                                $.each(data.VesselMovements(), function (index, data1) {
                                                    // data1.ReasonCode = b;
                                                    //data1.PreviousSlot = self.PreviousSlot;
                                                    //data1.OverriddenSlot = self.OverriddenSlot;
                                                    movements.push(data1);
                                                });
                                            }
                                        });

                                        if (movements.length > 0) {
                                            self.viewModelHelper1.apiPut('api/UpdateVesselSoltDtl', ko.toJSON(movements), function Message(msg) {
                                                toastr.success("Vessel details are saved successfully.", "Automated Slotting");
                                                self.isChanged(false);
                                                self.plannedSlots.removeAll();
                                                self.pendingSlots.removeAll();
                                                self.LoadPlannedMovements();
                                                self.LoadUnPlannedMovements();
                                            }, null, null, false);
                                        }
                                    }
                                },
                                'No': {
                                    'class': 'gray',
                                    'action': function () {
                                    }
                                }
                            }
                        });
                        //confirmation box - end
                    }
                    else {
                        toastr.warning("Vessels are not changed.", "Automated Slotting");
                        return false;
                    }
                }
            }
            else {
                toastr.options.timeOut = 3000;
                toastr.warning("Can not confirm vessel, you do not have permission.", "Automated Slotting");
                return false;
            }

            //prompt("Pendings", ko.toJSON(self.pendingSlots));
        }

        this.updateLastAction = function (arg) {
            self.SourceSlotName = '';
            self.TargetSlotName = '';
            self.SourceSlotindex = '';
            self.TargetSlotindex = '';

            self.VesselCallMovementID = arg.item.VesselCallMovementID;
            var source = arg.sourceParent.id.split(",");
            var target = arg.targetParent.id.split(",");


            self.SourceSlotName = source[0];
            self.TargetSlotName = target[0];
            var targetSlotForverify = target[1];
            var SourceSlotForVerfity = source[1];
            self.SourceSlotForNo = source[1];
            self.TargetSlotForNo = target[1];
            //self.SlotDateForNo = arg.item.SlotDate(moment(new Date(targetSlotDate)).format('YYYY-MM-DD'));
            
           
            if (self.SourceSlotName == "Unplanned" && self.TargetSlotName=="Planned") {
                if (source[1] == "Arrivals") self.SourceSlotindex = 0;
                else if (source[1] == "Sailings") self.SourceSlotindex = 1;
                else if (source[1] == "Shiftings") self.SourceSlotindex = 2;
                else if (source[1] == "Warpings") self.SourceSlotindex = 3;

                $.each(self.plannedSlots(), function (index, data) {
                    if (data.slotNumber == targetSlotForverify) {
                        self.TargetSlotindex = index;
                    }                   
                });
                
            }
            else if (self.SourceSlotName == "Planned" && self.TargetSlotName == "Unplanned") {
                
                $.each(self.plannedSlots(), function (index, data) {
                    if (data.slotNumber == SourceSlotForVerfity) {
                        self.SourceSlotindex = index;
                    }
                });
                    if (target[1] == "Arrivals") self.TargetSlotindex = 0;
                    else if (target[1] == "Sailings") self.TargetSlotindex = 1;
                    else if (target[1] == "Shiftings") self.TargetSlotindex = 2;
                    else if (target[1] == "Warpings") self.TargetSlotindex = 3;
                    self.SlotDateForNo = moment(new Date(arg.item.SlotDate())).format('YYYY-MM-DD');
                    //arg.item.SlotDate(moment(new Date(arg.item.SlotDate())).format('YYYY-MM-DD'));
                    
            }   
           else if (self.SourceSlotName == "Planned" && self.TargetSlotName == "Planned") {
               
                $.each(self.plannedSlots(), function (index, data) {
                    if (data.slotNumber == SourceSlotForVerfity) {
                        self.SourceSlotindex = index;                        
                    }                   
                });

                $.each(self.plannedSlots(), function (index, data) {
                    if (data.slotNumber == targetSlotForverify) {
                        self.TargetSlotindex = index;
                    }
                });
           }          

            var sourceSection = source[0];
            var targetSection = target[0];

            var sourceSlot = source[1];
            var targetSlot = target[1];

            var targetSlotDate = target[2];

            arg.item.Slot(targetSlot);
            if (targetSection == "Unplanned") {               
                arg.item.SlotStatus("PEND");
                arg.item.SlotDate("");
                arg.item.Slot("");
                arg.item.IsChanged(true);
                self.isChanged(true);
            }
            else {               
                  arg.item.SlotStatus("OVRD");               
                arg.item.SlotDate(moment(new Date(targetSlotDate)).format('YYYY-MM-DD'));
                arg.item.Slot(targetSlot);
                arg.item.IsChanged(true);
                self.isChanged(true);
            }
            






        };
        //verify that if a fourth member is added, there is at least one member of each gender
        this.verifyAssignments = function (arg) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                if (self.isVisible()) {


                    self.GetBlockedSlots();

                    var dt = new Date();
                    var dt1 = new Date();
                    var currentDate = self.CurrentDate();
                    dt1.setDate(dt1.getDate() + 1);

                    if (((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) || ((dt1.getDate() == currentDate.getDate()) && (dt1.getMonth() == currentDate.getMonth()) && (dt1.getFullYear() == currentDate.getFullYear()))) {
                        var source = arg.sourceParent.id.split(",");
                        var target = arg.targetParent.id.split(",");
                        var sourceSection = source[0];
                        var targetSection = target[0];

                        var blockedstatus = false;

                        $.each(self.blockedSlots(), function (index, item) {

                            if (item.SlotPeriod == target[1]) {
                                blockedstatus = true;
                                return false;
                            }
                        });


                        if (blockedstatus) {
                            toastr.options.timeOut = 3000;
                            toastr.warning("Can not move as Slots are Blocked.", "Automated Slotting");
                            arg.cancelDrop = true;
                            return;

                        }

                        if (targetSection != "Unplanned") {
                            if (arg.targetParent().length >= self.noOfVesselsPerSlot()) {
                                toastr.warning("Can not move as it reached maximum limit.", "Automated Slotting");
                                arg.cancelDrop = true;
                                return;
                            }
                        }

                        if (sourceSection != "Unplanned" && targetSection == "Unplanned") {
                            arg.item.IsChanged(true);

                            var targetSlot = target[1];

                            if (targetSection == "Unplanned") {
                                switch (arg.item.MovementTypeCode()) {
                                    case "ARMV": if (targetSlot != "Arrivals") arg.cancelDrop = true; break;
                                    case "SHMV": if (targetSlot != "Shiftings") arg.cancelDrop = true; break;
                                    case "WRMV": if (targetSlot != "Warpings") arg.cancelDrop = true; break;
                                    case "SGMV": if (targetSlot != "Sailings") arg.cancelDrop = true; break;
                                }
                            }
                        }
                        else if (sourceSection == "Unplanned" && targetSection == "Unplanned") {
                            arg.cancelDrop = true;
                        }
                        else {
                            arg.item.IsChanged(true);
                        }
                    }
                    else {
                        toastr.options.timeOut = 3000;
                        toastr.warning("Can not modify future date vessels beyond 48 hours.", "Automated Slotting");
                        arg.cancelDrop = true;
                    }
                }
                else {
                    toastr.options.timeOut = 3000;
                    toastr.warning("Can not confirm vessel, you do not have permission.", "Automated Slotting");
                    arg.cancelDrop = true;
                }
            }

            else if (moment(self.Date()).format('YYYY-MM-DD') == moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                if (self.isVisible()) {
                    self.GetActiveSlots();
                    self.GetBlockedSlots();


                    var source = arg.sourceParent.id.split(",");
                    var target = arg.targetParent.id.split(",");
                    var sourceSection = source[0];
                    var targetSection = target[0];
                    var sourceSlot1 = source[1];
                    var targetSlot1 = target[1];

                    if (targetSection != "Unplanned") {
                        if (arg.targetParent().length >= self.noOfVesselsPerSlot()) {
                            toastr.warning("Can not move as it reached maximum limit.", "Automated Slotting");
                            arg.cancelDrop = true;
                            return;

                        }
                    }

                    var status = false;

                    $.each(self.activeSlots(), function (index, item) {
                        if (item == target[1]) {
                            status = true;
                            return false;
                        }
                    });

                    var blockedstatus = false;

                    $.each(self.blockedSlots(), function (index, item) {

                        if (item.SlotPeriod == target[1]) {
                            blockedstatus = true;
                            return false;
                        }
                    });

                    if (blockedstatus) {
                        toastr.options.timeOut = 3000;
                        toastr.warning("Can not move as Slots are Blocked.", "Automated Slotting");
                        arg.cancelDrop = true;
                        return;
                    }

                    if (sourceSection != "Unplanned" && targetSection != "Unplanned") {
                        if (status) {
                            arg.item.IsChanged(true);//1229
                           
                            var slotStatus = arg.item.SlotStatus();
                            self.SourceSlotStatus = arg.item.SlotStatus();
                          
                            if (slotStatus == 'ONG' || slotStatus == 'CMPL' || slotStatus == 'SCHD') {
                                var toasterMessage = GetTaskMessage(slotStatus);
                                toastr.warning(toasterMessage, "Automated Slotting");
                                arg.cancelDrop = true;
                                return;
                            }
                            if (slotStatus == 'OVRD') {
                                self.PreviousSlot = sourceSlot1;
                                self.OverriddenSlot = targetSlot1;
                                self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                //self.SourceSlot1 = sourceSection;
                                //self.TargerSlot1 = targetSection;
                                $('#stack12').modal('show');
                            }
                            else if (slotStatus == 'PLND') {
                                self.PreviousSlot = sourceSlot1;
                                self.OverriddenSlot = targetSlot1;
                                self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                $('#stack12').modal('show');
                                self.SlotStatus1 = sourceSection;
                            }
                            else if (slotStatus == 'CNFR') {
                                self.PreviousSlot = sourceSlot1;
                                self.OverriddenSlot = targetSlot1;
                                self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                self.SlotStatus1 = sourceSection;
                                $('#stack12').modal('show');
                            }
                        }
                        else {
                            toastr.options.timeOut = 3000;
                            toastr.warning("Can not move previous slots.", "Automated Slotting");
                            arg.cancelDrop = true;
                        }

                    }
                    else if (sourceSection == "Unplanned" && targetSection != "Unplanned") {
                        if (status) {
                            arg.item.IsChanged(true);
                            self.PreviousSlot = "Awaiting Slot";
                            self.OverriddenSlot = targetSlot1;
                            self.VesselCallMovementID = arg.item.VesselCallMovementID;
                            self.SlotStatus1 = sourceSection;
                            $('#stack12').modal('show');
                        }
                        else {
                            toastr.options.timeOut = 2000;
                            toastr.warning("Can not move previous slots.", "Automated Slotting");
                            arg.cancelDrop = true;
                        }


                    }
                    else if (sourceSection != "Unplanned" && targetSection == "Unplanned") {

                        arg.item.IsChanged(true);

                        var sourceSlot = source[1];
                        var targetSlot = target[1];

                        var sourceSlot1 = source[1];
                        var targetSlot1 = target[1];

                      
                        var slotStatus = arg.item.SlotStatus();
                        self.SourceSlotStatus = arg.item.SlotStatus();
                        if (slotStatus == 'ONG' || slotStatus == 'CMPL' || slotStatus == 'SCHD') {
                            var toasterMessage = GetTaskMessage(slotStatus);
                            toastr.warning(toasterMessage, "Automated Slotting");
                            arg.cancelDrop = true;
                            return;
                        }
                        if (targetSection == "Unplanned") {
                            switch (arg.item.MovementTypeCode()) {
                                case "ARMV": if (targetSlot != "Arrivals")
                                    arg.cancelDrop = true;
                                else {
                                    if (slotStatus == 'OVRD' || slotStatus == 'PLND' || slotStatus == 'CNFR') {
                                        self.PreviousSlot = sourceSlot1;
                                        self.OverriddenSlot = "Awaiting Slot";
                                        self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                        self.SlotStatus1 = sourceSection;
                                        $('#stack12').modal('show');
                                    }

                                }
                                    break;
                                case "SHMV": if (targetSlot != "Shiftings") arg.cancelDrop = true;
                                else {
                                    if (slotStatus == 'OVRD' || slotStatus == 'PLND' || slotStatus == 'CNFR') {
                                        self.PreviousSlot = sourceSlot1;
                                        self.OverriddenSlot = "Awaiting Slot";
                                        self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                        self.SlotStatus1 = sourceSection;
                                        $('#stack12').modal('show');
                                    }

                                } break;

                                case "WRMV": if (targetSlot != "Warpings") arg.cancelDrop = true;
                                else {
                                    if (slotStatus == 'OVRD' || slotStatus == 'PLND' || slotStatus == 'CNFR') {
                                        self.PreviousSlot = sourceSlot1;
                                        self.OverriddenSlot = "Awaiting Slot";
                                        self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                        self.SlotStatus1 = sourceSection;
                                        $('#stack12').modal('show');
                                    }

                                } break;
                                case "SGMV": if (targetSlot != "Sailings") arg.cancelDrop = true;
                                else {
                                    if (slotStatus == 'OVRD' || slotStatus == 'PLND' || slotStatus == 'CNFR') {
                                        self.PreviousSlot = sourceSlot1;
                                        self.OverriddenSlot = "Awaiting Slot";
                                        self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                        self.SlotStatus1 = sourceSection;
                                        $('#stack12').modal('show');
                                    }

                                } break;
                            }
                        }
                        //if (slotStatus == 'OVRD') {
                        //    self.PreviousSlot = sourceSlot1;
                        //    self.OverriddenSlot = "Awaiting Slot";
                        //    $('#stack12').modal('show');
                        //}
                        //if (slotStatus == 'PLND') {
                        //    self.PreviousSlot = sourceSlot1;
                        //    self.OverriddenSlot = "Awaiting Slot";
                        //    $('#stack12').modal('show');
                        //}



                    }
                    else if (sourceSection == "Unplanned" && targetSection == "Unplanned") {
                        arg.cancelDrop = true;
                    }

                }
                else {
                    toastr.options.timeOut = 3000;
                    toastr.warning("Can not modify slots, you do not have permission.", "Automated Slotting");
                    arg.cancelDrop = true;
                }
            }
            else {
                if (self.isVisible()) {
                    var source = arg.sourceParent.id.split(",");
                    var target = arg.targetParent.id.split(",");

                    var sourceSection = source[0];
                    var targetSection = target[0];

                    var sourceSlot1 = source[1];
                    var targetSlot1 = target[1];

                    if (sourceSection != "Unplanned" && targetSection == "Unplanned") {
                        arg.item.IsChanged(true);

                        var sourceSlot = source[1];
                        var targetSlot = target[1];

                        
                        var slotStatus = arg.item.SlotStatus();
                        self.SourceSlotStatus = arg.item.SlotStatus();
                        if (slotStatus == 'ONG' || slotStatus == 'CMPL' || slotStatus == 'SCHD') {
                            var toasterMessage = GetTaskMessage(slotStatus);
                            toastr.warning(toasterMessage, "Automated Slotting");
                            arg.cancelDrop = true;
                            return;
                        }


                        if (targetSection == "Unplanned") {
                            switch (arg.item.MovementTypeCode()) {
                                case "ARMV": if (targetSlot != "Arrivals")
                                    arg.cancelDrop = true;
                                else {
                                    if (slotStatus == 'OVRD' || slotStatus == 'PLND' || slotStatus == 'CNFR') {
                                        self.PreviousSlot = sourceSlot1;
                                        self.OverriddenSlot = "Awaiting Slot";
                                        self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                        self.SlotStatus1 = sourceSection;
                                        $('#stack12').modal('show');
                                    }

                                }
                                    break;
                                case "SHMV": if (targetSlot != "Shiftings") arg.cancelDrop = true;
                                else {
                                    if (slotStatus == 'OVRD' || slotStatus == 'PLND' || slotStatus == 'CNFR') {
                                        self.PreviousSlot = sourceSlot1;
                                        self.OverriddenSlot = "Awaiting Slot";
                                        self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                        self.SlotStatus1 = sourceSection;
                                        $('#stack12').modal('show');
                                    }

                                } break;

                                case "WRMV": if (targetSlot != "Warpings") arg.cancelDrop = true;
                                else {
                                    if (slotStatus == 'OVRD' || slotStatus == 'PLND' || slotStatus == 'CNFR') {
                                        self.PreviousSlot = sourceSlot1;
                                        self.OverriddenSlot = "Awaiting Slot";
                                        self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                        self.SlotStatus1 = sourceSection;
                                        $('#stack12').modal('show');
                                    }

                                } break;
                                case "SGMV": if (targetSlot != "Sailings") arg.cancelDrop = true;
                                else {
                                    if (slotStatus == 'OVRD' || slotStatus == 'PLND' || slotStatus == 'CNFR') {
                                        self.PreviousSlot = sourceSlot1;
                                        self.OverriddenSlot = "Awaiting Slot";
                                        self.VesselCallMovementID = arg.item.VesselCallMovementID;
                                        self.SlotStatus1 = sourceSection;
                                        $('#stack12').modal('show');
                                    }

                                } break;
                            }
                        }
                    }
                    else {
                        toastr.options.timeOut = 3000;
                        toastr.warning("Can not modify previous date vessels.", "Automated Slotting");
                        arg.cancelDrop = true;
                    }
                }
                else {
                    toastr.options.timeOut = 3000;
                    toastr.warning("Can not modify vessel, you do not have permission.", "Automated Slotting");
                    arg.cancelDrop = true;
                }
            }


        };

        GetTaskMessage = function (status) {
            if (status == "ONG")
                return "Can not move, Vessel is started.";
            else if (status == "SCHD")
                return "Can not move, Vessel is scheduled.";
            else if (status == "CMPL")
                return "Can not move, Vessel is completed.";
            else {
                return "Can not move vessel.", "Automated Slotting";
            }
        }

        //This method is fires when cancel button is pressed redirected to Dashboard page
        self.cancel = function () {
            window.location = '/Welcome';
        }
        self.ClickYes = function (data) {
            var reason = $('#ResnOverRide option:selected').text();
           // PreviousSlotStatus = '';
            if (reason == 'Choose....') {
                self.isspanOverrideReason(true);
                $("#spanOverrideReason").text('This field is required');

            }
            else {
                self.isspanOverrideReason(false);
                $("#spanOverrideReason").text('');
                self.Reason = reason;
                self.ReasonCode = $('#ResnOverRide option:selected').val();
                //var ID = data.VesselMovements().VesselCallMovementID();

                var movements1 = new Array();
                $.each(self.plannedSlots(), function (index, data) {
                    if (data.VesselMovements().length > 0) {
                        $.each(data.VesselMovements(), function (index, data1) {
                            var reson = $('#ResnOverRide option:selected').val();
                            if (data1.VesselCallMovementID == self.VesselCallMovementID) {
                                data1.ReasonCode = $('#ResnOverRide option:selected').val();
                                data1.PreviousSlot = self.PreviousSlot;
                                data1.OverriddenSlot = self.OverriddenSlot;
                            }
                            movements1.push(data1);
                        });
                    }
                });

                $.each(self.pendingSlots(), function (index, data) {
                    if (data.VesselMovements().length > 0) {
                        $.each(data.VesselMovements(), function (index, data1) {
                            if (data1.VesselCallMovementID == self.VesselCallMovementID) {
                                data1.ReasonCode = $('#ResnOverRide option:selected').val();
                                data1.PreviousSlot = self.PreviousSlot;
                                data1.OverriddenSlot = self.OverriddenSlot;
                            }
                            movements1.push(data1);
                        });
                    }
                });
                $('#ResnOverRide').find('option:first').attr('selected', 'selected');
                $('#stack12').modal('hide');
            }
        }


        self.ClickNo = function (data) {
            var id = self.VesselCallMovementID;
            var slot1 = self.SlotStatus1;
            $('#stack12').modal('hide');
            self.isspanOverrideReason(false);
            $('#ResnOverRide').find('option:first').attr('selected', 'selected');
            var Plnnedmovements1 = new Array();
            if (self.SourceSlotName == "Unplanned" && self.TargetSlotName == "Planned") {
                var targetIndex = self.TargetSlotindex;
                var sourceIndex = self.SourceSlotindex;
                var length = self.plannedSlots()[targetIndex].VesselMovements().length;
                for (var i = 0; i < length; i++) {
                    if (self.plannedSlots()[targetIndex].VesselMovements()[i].VesselCallMovementID == self.VesselCallMovementID) {
                        var data = self.plannedSlots()[targetIndex].VesselMovements()[i];
                        self.plannedSlots()[targetIndex].VesselMovements()[i].SlotStatus('PEND');
                        self.plannedSlots()[targetIndex].VesselMovements()[i].Slot(null);
                        if (i == 0) {
                            self.plannedSlots()[targetIndex].VesselMovements.shift(data);
                            break;
                        }
                        else if (i == length) {
                            self.plannedSlots()[targetIndex].VesselMovements.pop(data);
                            break;
                        }
                        else {
                            self.plannedSlots()[targetIndex].VesselMovements.splice(i, 1);
                            break;
                        }
                    }
                }

                //var data = self.plannedSlots()[targetIndex].VesselMovements()[0];
                //self.plannedSlots()[targetIndex].VesselMovements()[0].SlotStatus('PEND');
                //self.plannedSlots()[targetIndex].VesselMovements.shift(data);
                self.pendingSlots()[sourceIndex].VesselMovements.push(data);                  
               
            }
            else if (self.SourceSlotName == "Planned" && self.TargetSlotName == "Unplanned") {
                var targetIndex = self.TargetSlotindex;
                var sourceIndex = self.SourceSlotindex;
                var length = self.pendingSlots()[targetIndex].VesselMovements().length;
                for (var i = 0; i < length; i++) {
                    if (self.pendingSlots()[targetIndex].VesselMovements()[i].VesselCallMovementID == self.VesselCallMovementID) {
                        var data = self.pendingSlots()[targetIndex].VesselMovements()[i];
                        self.pendingSlots()[targetIndex].VesselMovements()[i].SlotStatus(self.SourceSlotStatus);
                        self.pendingSlots()[targetIndex].VesselMovements()[i].Slot(self.SourceSlotForNo);
                        self.pendingSlots()[targetIndex].VesselMovements()[i].SlotDate(self.SlotDateForNo); 
                        if (i == 0) {
                            self.pendingSlots()[targetIndex].VesselMovements.shift(data);
                            break;
                        }
                        else if (i == length) {
                            self.pendingSlots()[targetIndex].VesselMovements.pop(data);
                            break;
                        }
                        else {
                            self.pendingSlots()[targetIndex].VesselMovements.splice(i, 1);
                            break;
                        }
                    }
                }



                //var data = self.pendingSlots()[targetIndex].VesselMovements()[0];
                //self.pendingSlots()[targetIndex].VesselMovements()[0].SlotStatus(self.SourceSlotStatus);
                //self.pendingSlots()[targetIndex].VesselMovements.shift(data);               
                self.plannedSlots()[sourceIndex].VesselMovements.push(data);               
                self.SourceSlotStatus = '';
            }
            else if (self.SourceSlotName == "Planned" && self.TargetSlotName == "Planned") {
                var targetIndex = self.TargetSlotindex;
                var sourceIndex = self.SourceSlotindex;
                var length = self.plannedSlots()[targetIndex].VesselMovements().length;
                for (var i = 0; i < length; i++) {
                    if (self.plannedSlots()[targetIndex].VesselMovements()[i].VesselCallMovementID == self.VesselCallMovementID) {
                        self.plannedSlots()[targetIndex].VesselMovements()[i].SlotStatus(self.SourceSlotStatus);
                        self.plannedSlots()[targetIndex].VesselMovements()[i].Slot(self.SourceSlotForNo);
                        var data = self.plannedSlots()[targetIndex].VesselMovements()[i];
                     
                        if (i == 0) {
                            self.plannedSlots()[targetIndex].VesselMovements.shift(data);
                            break;
                        }
                        else if (i == length) {
                            self.plannedSlots()[targetIndex].VesselMovements.pop(data);
                            break;
                        }
                        else {
                            self.plannedSlots()[targetIndex].VesselMovements.splice(i, 1);
                            break;
                        }
                    }
                }

                //var data = self.plannedSlots()[targetIndex].VesselMovements()[0];
                //self.plannedSlots()[targetIndex].VesselMovements()[0].SlotStatus(self.SourceSlotStatus);
                self.plannedSlots()[sourceIndex].VesselMovements.push(data);
                //self.plannedSlots()[targetIndex].VesselMovements.shift(data);               
            }

        }



        self.Refresh = function () {
            self.plannedSlots.removeAll();
            self.pendingSlots.removeAll();
            self.LoadUnPlannedMovements();
            self.LoadPlannedMovements();
            self.isChanged(false);
        }

        self.AutomatedExtendableYesNo = function () {

            self.viewModelHelper1.apiGet('api/GettingExtendableYesNo/{slotDate}', { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
             function (result) {
                 if (result != null) {
                     if (result.ExtendYn == 'Y') {
                         isExtendYn = 'Y';
                         chkSlotsExetended = 'Y';
                     }
                     else {
                         isExtendYn = 'N';
                         chkSlotsExetended = 'N';
                     }
                 }
                 else {
                     isExtendYn = 'N';
                     chkSlotsExetended = 'N';
                 }
             }, null, null, false);

        }

        self.LoadPortName = function () {
            self.viewModelHelper.apiGet('api/GetPortNameByPortCode', null,
                function (result) {
                    self.PortName(result);
                });
        }

        self.ConfirmAll = function () {

            var dt = new Date();
            var dt1 = new Date();
            var currentDate = self.CurrentDate();
            dt1.setDate(dt1.getDate() + 1);
            var count = 0;

            if (((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) || ((dt1.getDate() == currentDate.getDate()) && (dt1.getMonth() == currentDate.getMonth()) && (dt1.getFullYear() == currentDate.getFullYear()))) {

                $.each(self.plannedSlots(), function (index, data) {
                    if (data.VesselMovements().length > 0) {
                        $.each(data.VesselMovements(), function (index, data1) {
                            if (data1.SlotStatus() != "CNFR" && data1.SlotStatus() != "SCHD") {
                                data1.SlotStatus("CNFR");
                                data1.IsChanged(true);
                                self.isChanged(true);
                                count++;
                            }
                        });
                    }
                });
            }
            else {
                self.GetActiveSlots();
                var count = 0;
                $.each(self.plannedSlots(), function (index, data) {
                    if (data.VesselMovements().length > 0) {
                        $.each(data.VesselMovements(), function (index, data1) {
                            $.each(self.activeSlots(), function (index, value) {
                                if (value == data1.Slot() && data1.SlotStatus() != "CNFR" && data1.SlotStatus() != "SCHD") {
                                    data1.SlotStatus("CNFR");
                                    data1.IsChanged(true);
                                    self.isChanged(true);
                                    count++;
                                }
                            });
                        });
                    }
                });
            }

            if (count == 0) {
                toastr.warning("No vessels are available for confirmation.", "Automated Slotting");
            }
            else {
                toastr.success("Vessels are confirmed successfully, Click on “Save” to effect the confirmed changes.", "Automated Slotting");
            }
        }

        self.GetActiveSlots = function () {
            self.activeSlots.removeAll();
            self.viewModelHelper1.apiGet('api/GetActiveSlots', null,
                function (result) {
                    ko.mapping.fromJS(result, {}, self.activeSlots);
                }, null, null, false);
        }

        self.GetBlockedSlots = function () {
            self.blockedSlots.removeAll();
            self.viewModelHelper1.apiGet('api/GetBlockedSlots/{slotDate}',
                { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
                function (result) {

                    self.blockedSlots(result);
                }, null, null, false);
        }

        self.getResourceByDate = function (data) {
            var date = data.ResourceDate();
            self.CurrentDate(date);

            self.isChanged(false);
            var dt = new Date();

            var currentDate = self.CurrentDate();
            var dt1 = new Date();
            dt1.setDate(dt1.getDate() + 1);

            currentDate.setDate(currentDate.getDate());
            if ((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) {
                if (self.isVisible() == true) {
                    self.IsCurrentDate(true);
                    self.isEnabled(true);
                }
                else {
                    self.IsCurrentDate(false);
                    self.isEnabled(false);
                }
            }
            else {
                self.IsCurrentDate(false);
                self.isEnabled(false);
            }

            self.CurrentDate(currentDate);
            $('.displaytxt').text(moment(currentDate).format('MMM DD, YYYY'));
            self.plannedSlots([]);

            self.AutomatedConfigurationDetails();
            self.LoadPlannedMovements();
            self.LoadUnPlannedMovements();
        }

        self.clickRefresh = function () {
            location.reload();
        }
        self.Initialize();

    };

    var vm = new AutoSlotsModel();

    ko.bindingHandlers.sortable.beforeMove = vm.verifyAssignments;
    ko.bindingHandlers.sortable.afterMove = vm.updateLastAction;


    //ko.applyBindings(vm);
    ko.applyBindings(vm, $("#AutomatedSlot")[0], { insertMessages: false, messagesOnModified: false, grouping: { deep: true } });
})(ko, jQuery);
