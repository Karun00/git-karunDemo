(function (IPMSRoot) {

    var AutomatedResourceScheduleViewModel = function () {
        var self = this;
        self.movementResourceAllocations = ko.observableArray();
        self.movementResourceCalendarModel = ko.observableArray();
        var currentDate = new Date();
        //For Common Validation
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.configuredSlots = ko.observableArray();
        self.maximumSlots = ko.observable();
        self.selectedAllocationRow = ko.observable();
        self.selectedOtherThanTugSlotColumn = ko.observable();
        self.selectedTugSlotColumn = ko.observable();
        self.MovementPopUp = ko.observable();
        self.getServiceTypeList = ko.observableArray();
        self.getShiftsTypeList = ko.observableArray();
        self.getCraftsList = ko.observableArray();
        self.getResourceList = ko.observableArray();
        self.VCNObservable = ko.observable();
        self.OtherthanTugSelectedResourceId = ko.observable();
        self.UserID = ko.observable();
        self.blockedSlots = ko.observableArray();
        self.maintableWidth = ko.observable('1332px');
        self.maintableWidthRC = ko.observable('2235px');
        self.colwidth = ko.observable('900px');
        self.colwidthRC = ko.observable('900px');
        self.CurrentDate = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });
        self.Date = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });

        self.ResourceDate = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });

        self.status = ko.observable(false);

        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        self.ConfirmedServiceRequest = ko.observableArray();
        self.AutomatedResourceSchedule = ko.observable();
        self.IsCurrentDate = ko.observable(true);
        self.IsValidDate = ko.observable(true);
        self.IsAnyChangedInMovement = ko.observable(true);

        self.ResourceTypeHeaderForCalendar = ko.observable();
        self.getResourceCalendarList = ko.observableArray();
        self.confirmResourceAllocationColor = ko.observable('black');
        $('.displaytxtResourceAllocation').text(moment(self.CurrentDate()).format('MMM DD, YYYY'));
        self.Date = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });
        self.PortName = ko.observable();
        self.movementResourceCalendarModel2 = ko.observableArray();
        self.getCraftAvailabilityServiceTypes = ko.observableArray();
        self.isVisible = ko.observable(false);
        self.activeSlots = ko.observableArray();   

        /////////////////////////////////////////////Dynamic Resource Allocation Starts here///////////////////////////////////////

        self.isTableFull = function (parent) {
            return parent().length < self.maximumSlots;
        }

        self.LoadResourceAllocations = function () {

            self.viewModelHelper.apiGet('api/ResourceAllocationDetails/{slotDate}',
           { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
             function (result) {
                 if (result.length > 0) {
                     self.movementResourceAllocations(ko.utils.arrayMap(result, function (item) {
                         return new IPMSROOT.ResourceAllocations(item);
                     }))
                 } else {
                     self.movementResourceAllocations([]);
                     self.ResourceAllocationCount(self.movementResourceAllocations.length);
                 };
             }, null, null, false);

            if (self.isVisible()) {
                if (moment(self.Date()).format('YYYY-MM-DD') <= moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                    self.status(true);
                    $("#grid").show();
                }
                else {
                    self.status(false);
                    $("#grid").hide();
                }
            }
            else {
                self.status(false);
                $("#grid").hide();
            }          

            self.IsAnyChangedInMovement(true);
        }

        /////////////////////////////////////////////Dynamic Resource Allocation Ends here///////////////////////////////////////

        self.LoadConfirmedServiceReqDetails = function () {
            self.ConfirmedServiceRequest.removeAll();
            self.viewModelHelper.apiGet('api/ConfirmedServiceReq', null,
             function (result) {
                 if (result.length > 0) {
                     $.each(result, function (index, data) {
                         self.ConfirmedServiceRequest.push(new IPMSRoot.ConfirmedServiceRequestsModel(data));
                     });

                 } else {
                     self.ConfirmedServiceRequest([]);
                 }
             }, null, null, false);
        }

        self.Initialize = function () {            
            self.AutomatedResourceSchedule(new IPMSROOT.ConfirmedServiceRequestsModel());
            self.LoadPrivileges();
            self.LoadConfirmedServiceReqDetails();
            self.LoadResourceAllocations();
            self.LoadServiceTypes();
            self.LoadShiftsTypes();
            self.LoadSlotsCount();
            self.LoadPortName();
            self.LoadCraftAvailabilityServiceTypes();

        }

        self.LoadPrivileges = function () {

            self.viewModelHelper.apiGet('api/GetAutomatedResourceSchedulingPrivileges/' + 'AutomatedResourceScheduling', null,
                function (result) {
                    self.isVisible(result);
                }, null, null, false);
        }

        self.updateSlot = function (arg) {

            for (var j = 0; j < arg.targetParent().length; j++) {
                arg.targetParent()[j].SlotNumber(j + 1);
                arg.targetParent()[arg.targetIndex].IsChanged(true);
            }
            //UpdatedResourceAllocation(self.movementResourceAllocations(), arg.targetIndex);

            var targetSlotNumber = arg.targetIndex + 1;
            var slotDetails = arg.item;

            var serviceRequestId = arg.item.ServiceReferenceID();
            var movements = self.movementResourceAllocations();
            var thisMovement;
            for (var i = 0; i < movements.length; i++) {
                if (movements[i].ServiceRequestId() == serviceRequestId) {
                    thisMovement = movements[i];
                    break;
                }
            }

            if (thisMovement != undefined) {
                for (var i = 0; i < thisMovement.ResourceAllocationSlots().length; i++) {

                    thisMovement.ResourceAllocationSlots()[i].AllocSlot(targetSlotNumber.toString());
                    // thisMovement.ResourceAllocationSlots()[i].AllocSlot(arg.targetParent()[i].SlotPeriod());
                    //thisMovement.ResourceAllocationSlots()[i].AllocationDate(new Date());  //TODO: Get the date based on targetSlotNumber. 
                    var resourceAllocationId = thisMovement.ResourceAllocationSlots()[i].ResourceAllocationID();
                    var resourceAllocationDate = thisMovement.ResourceAllocationSlots()[i].AllocationDate();
                    var varIsCraft = thisMovement.ResourceAllocationSlots()[i].IsCraft();
                    var svcTypeCode = thisMovement.ResourceAllocationSlots()[i].ServiceTypeCode();

                    var varServiceTypeName = thisMovement.ResourceAllocationSlots()[i].ServiceTypeName();

                    //var varIsCraft = thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[9].IsCraft()
                    //Clear old slots
                    for (var j = 0 ; j < thisMovement.ResourceAllocationSlots()[i].ResourceSlots().length ; j++) {

                        //thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].SlotNumber(j + 1);
                        //thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].AllocationDate(thisMovement.ResourceAllocationSlots()[i].AllocationDate());
                        if (j == arg.targetIndex) {
                            //Set the slots 
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ResourceAllocationID(resourceAllocationId);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ResourceID(0);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ResourceName("Unscheduled");
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].TugResourceName("Unscheduled");
                            //thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[arg.targetIndex].AllocationDate(resourceAllocationDate);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].Status("PNDG");
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].TaskStatus("PNDG");
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].CraftID(0);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].CraftName(null);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ServiceReferenceID(serviceRequestId);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ServiceTypeCode(svcTypeCode);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].IsCraft(varIsCraft);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ServiceTypeName(varServiceTypeName);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].AllocationDate(thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j - 1].AllocationDate());

                        }
                        else {
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ResourceAllocationID(null);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ResourceID(null);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ResourceName(null);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].TugResourceName(null);
                            //thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[arg.targetIndex].AllocationDate(resourceAllocationDate);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].Status(null);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].TaskStatus("CLEARSlot");

                            $.each(self.blockedSlots(), function (index, item) {
                                if (item.SlotNumber == j + 1) {
                                    thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].TaskStatus("BLCK");
                                }
                            });
                            
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].CraftID(null);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].CraftName(null);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ServiceReferenceID(serviceRequestId);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ServiceTypeCode(svcTypeCode);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].IsCraft(varIsCraft);
                            thisMovement.ResourceAllocationSlots()[i].ResourceSlots()[j].ServiceTypeName(varServiceTypeName);
                        }
                    }
                }
            }
            
        }       

        self.GetBlockedSlots = function () {            
            self.blockedSlots.removeAll();
            self.viewModelHelper.apiGet('api/GetBlockedSlots/{slotDate}',
                { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
                function (result) {
                    self.blockedSlots(result);
                }, null, null, false);
        }

        //Verification logic goes here.
        self.verifyAssignments = function (arg) {            
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.GetBlockedSlots();

            if (self.isVisible()) {

                var active = $('.nav-tabs .active').text();
                if (active.trim() == "Resource Calendar") {
                    arg.cancelDrop = true;
                    return;
                }

                var date = moment(self.Date()).format('HH:mm');
                var hours = date.split(':');

                var Dateminutes = new Date(self.Date());
                var totalminutes = (Dateminutes.getHours()) * 60 + Dateminutes.getMinutes();

                var obj = JSON.parse(ko.toJSON(self.SlotsCount));                
                var number = null;

                var serviceRequestId = arg.item.ServiceReferenceID();

                var movements = self.movementResourceAllocations();
                var thisMovement;
                for (var i = 0; i < movements.length; i++) {
                    if (movements[i].ServiceRequestId() == serviceRequestId) {
                        thisMovement = movements[i];
                        break;
                    }
                }
                var iCount = 0;
                for (var i = 0; i < thisMovement.ResourceAllocationSlots().length ; i++) {
                    thisResourceAllocationSlots = thisMovement.ResourceAllocationSlots()[i].ResourceSlots();
                    var Valid = thisResourceAllocationSlots.filter(function (a) {
                        return (a.TaskStatus() == "STRD" || a.TaskStatus() == "VERF" || a.TaskStatus() == 'COMP' || a.TaskStatus() == 'CFRI' || a.TaskStatus() == 'ACCP');
                    });
                    if (Valid.length > 0) {
                        iCount++;
                    }

                }
                if (iCount != 0) {
                    toastr.warning("Can not move slots.", "Automated Resource Allocation");
                    arg.cancelDrop = true;
                    return;
                }

                if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                    if (arg.item.TaskStatus() == 'STRD' || arg.item.TaskStatus() == 'VERF' || arg.item.TaskStatus() == 'COMP' || arg.item.TaskStatus() == 'CFRI' || arg.item.TaskStatus() == 'ACCP') {
                        arg.cancelDrop = true;
                        return;
                    }
                    if (arg.item.TaskStatus() == "PNDG" || arg.item.TaskStatus() == "SCHD" || arg.item.TaskStatus() == "OVRD") {
                        $.each(self.blockedSlots(), function (index, item) {
                            if (item.SlotNumber == arg.targetIndex + 1) {
                                //blockedstatus = true;
                                toastr.options.timeOut = 3000;
                                toastr.warning("Can not move as Slots are Blocked for Reason - " + item.ReasonName, "Automated Slotting");
                                arg.cancelDrop = true;
                                return false;
                            }
                        });
                    }

                    if (arg.item.SlotNumber() != arg.targetIndex + 1 && arg.item.ResourceName() != "Unscheduled") {
                        arg.item.TaskStatus('SCHD');
                        arg.item.IsChanged(true);
                    }
                    if (arg.item.ResourceName() == null || arg.item.ResourceName() == "") {
                        arg.cancelDrop = true;
                    }
                   
                }
                else if (moment(self.Date()).format('YYYY-MM-DD') == moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                    $.each(obj, function (index, value) {

                        var startminutes = value.StartMinutes;
                        var endminutes = value.EndMinutes;

                        if (endminutes < startminutes) {
                            endminutes = startminutes + (value.Duration - 1);
                        }

                        if (totalminutes >= startminutes && totalminutes < endminutes) {
                            number = value.SlotNumber;
                            return false;
                        }

                    });

                    if (parseInt(arg.targetIndex + 1) >= parseInt(number)) {

                        if (arg.item.TaskStatus() == 'STRD' || arg.item.TaskStatus() == 'VERF' || arg.item.TaskStatus() == 'COMP' || arg.item.TaskStatus() == 'CFRI' || arg.item.TaskStatus() == 'ACCP') {
                            arg.cancelDrop = true;
                            return;
                        }
                        if (arg.item.TaskStatus() == "PNDG" || arg.item.TaskStatus() == "SCHD" || arg.item.TaskStatus() == "OVRD") {
                            $.each(self.blockedSlots(), function (index, item) {
                                if (item.SlotNumber == arg.targetIndex + 1) {                                    
                                    toastr.options.timeOut = 3000;
                                    toastr.warning("Can not move as Slots are Blocked for Reason - " + item.ReasonName, "Automated Slotting");
                                    arg.cancelDrop = true;
                                    return false;
                                }
                            });
                        }

                        if (arg.item.SlotNumber() != arg.targetIndex + 1 && arg.item.ResourceName() != "Unscheduled") {
                            arg.item.TaskStatus('SCHD');
                            arg.item.IsChanged(true);
                        }
                        if (arg.item.ResourceName() == null || arg.item.ResourceName() == "") {
                            arg.cancelDrop = true;
                        }
                        


                    }
                    else {
                        toastr.warning("Can not move previous slots.", "Automated Resource Allocation");
                        arg.cancelDrop = true;
                    }
                }
                else {
                    toastr.warning("Can not move previous date slots.", "Automated Resource Allocation");
                    arg.cancelDrop = true;
                }
            }
            else {
                arg.cancelDrop = true;
            }

            
        }

        self.LoadCrafts = function (data) {           
            self.viewModelHelper.apiGet('api/GetSearchCraft/' + data.ServiceTypeCode() + '/' + data.SlotNumber() + "/" + moment(data.AllocationDate()).format('YYYY-MM-DD'), null, //ko.toJSON(data)
                   function (result) {
                       ko.mapping.fromJS(result, {}, self.getCraftsList);
                   }, null, null, false);


            if (data.TaskStatus() != "COMP" || data.TaskStatus() != "VERF") {

                self.checkCraftList = ko.observableArray();
                var obj = JSON.parse(ko.toJSON(self.movementResourceAllocations));

                $.each(obj, function (key, value) {
                    if (value.ServiceRequestId == data.ServiceReferenceID()) {
                        $.each(value.ResourceAllocationSlots, function (key, value1) {
                            var avblcrafts = ko.utils.arrayFilter(value1.ResourceSlots, function (item) {
                                return (item.CraftID != null && (item.TaskStatus != "COMP" && item.TaskStatus != "VERF"));
                            })[0];

                            if (avblcrafts != undefined) {

                                if (avblcrafts.CraftID != 0) {

                                    var crafts = ko.utils.arrayFilter(self.getCraftsList(), function (item) {
                                        return item.ID() == avblcrafts.CraftID;
                                    })[0];

                                    if (crafts != undefined) {
                                        if (crafts != undefined) {
                                            self.checkCraftList.push(crafts);
                                        }
                                    }
                                }
                            }
                        });
                    }
                });

                var craftobj = JSON.parse(ko.toJSON(self.checkCraftList));

                $.each(craftobj, function (key, value) {
                    var crafts1 = ko.utils.arrayFilter(self.getCraftsList(), function (item) {
                        return item.ID() == value.ID;
                    })[0];

                    if (crafts1 != undefined) {
                        if (data.CraftID() != crafts1.ID()) {
                            self.getCraftsList.remove(crafts1);
                        }
                    }
                });

                self.checkCraftList.removeAll();
            }
            else {

                var craft = ko.utils.arrayFilter(self.getCraftsList(), function (item) {
                    return item.ID() == data.CraftID();
                })[0];

                self.getCraftsList.removeAll();
                self.getCraftsList.push(craft);

            }
        }

        // Verify Users based on VCN
        self.LoadUsers = function (data) {       
            var ResourceSlotVO = new IPMSROOT.ResourceSlotVO(data);           
            self.viewModelHelper.apiGet('api/GetSearchResource/' + data.ServiceTypeCode() + "/" + data.ServiceReferenceID() + "/" + data.SlotNumber() + "/" + moment(data.AllocationDate()).format('YYYY-MM-DD'), null,

                     function (result) {
                         ko.mapping.fromJS(result, {}, self.getResourceList);
                     }, null, null, false);

            if (data.TaskStatus() != "COMP" || data.TaskStatus() != "VERF") {

                self.checkUserList = ko.observableArray();

                var obj = JSON.parse(ko.toJSON(self.movementResourceAllocations));

                $.each(obj, function (key, value) {
                    if (value.ServiceRequestId == data.ServiceReferenceID()) {

                        $.each(value.ResourceAllocationSlots, function (key, value1) {
                            var avblusers = ko.utils.arrayFilter(value1.ResourceSlots, function (item) {
                                return (item.ResourceID != null && (item.TaskStatus != "COMP" && item.TaskStatus != "VERF"));
                            })[0];

                            if (avblusers != undefined) {

                                if (avblusers.ResourceID != 0) {

                                    var users = ko.utils.arrayFilter(self.getResourceList(), function (item) {
                                        return item.ID() == avblusers.ResourceID;
                                    })[0];

                                    if (users != undefined) {
                                        if (users != undefined) {
                                            self.checkUserList.push(users);
                                        }
                                    }
                                }
                            }
                        });
                    }
                });

                var userobj = JSON.parse(ko.toJSON(self.checkUserList));

                $.each(userobj, function (key, value) {
                    var users1 = ko.utils.arrayFilter(self.getResourceList(), function (item) {
                        return item.ID() == value.ID;
                    })[0];

                    if (users1 != undefined) {
                        if (data.ResourceID() != users1.ID()) {
                            self.getResourceList.remove(users1);
                        }
                    }
                });

                self.checkUserList.removeAll();
            }
            else {
                var user = ko.utils.arrayFilter(self.getResourceList(), function (item) {
                    return item.ID() == data.ResourceID();
                })[0];

                self.getResourceList.removeAll();
                self.getResourceList.push(user);
            }

        }      


        self.ResourceOnClick = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if (self.isVisible()) {               
                if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD')) {
                    if (data.ResourceID() != null) {
                        /////
                        var serviceRequestId = data.ServiceReferenceID();
                        var movements = self.movementResourceAllocations();
                        var thisSelectedMovement;
                        for (var i = 0; i < movements.length; i++) {
                            if (movements[i].ServiceRequestId() == serviceRequestId) {
                                thisSelectedMovement = movements[i];
                                break;
                            }
                        }
                        var lclVCN = thisSelectedMovement.VCN();
                        var lclVesselName = thisSelectedMovement.VesselName();
                        var lclMovementType = thisSelectedMovement.MovementType();
                        var lclServiceTypeName = data.ServiceTypeName();


                        if (data.ResourceID() > 0) {
                            if (data.IsCraft() == true) {
                                self.selectedTugSlotColumn(data);
                                self.LoadCrafts(data);
                                self.LoadUsers(data);

                                $('#TugCraftID option')
                                    .filter(function () { return $.trim($(this).val()) == data.CraftID(); })
                                    .attr('selected', true);
                                $('#TugResourceID option')
                                    .filter(function () { return $.trim($(this).val()) == data.ResourceID(); })
                                    .attr('selected', true);
                            }
                            else {

                                self.LoadUsers(data);
                                self.selectedOtherThanTugSlotColumn(data);
                                $('#OtherThanTugResourceID option')
                                    .filter(function () { return $.trim($(this).val()) == data.ResourceID(); })
                                    .attr('selected', true);
                            }
                        }
                        else {
                            if (data.IsCraft() == true) {
                                self.selectedTugSlotColumn(data);
                                self.LoadCrafts(data);
                                self.LoadUsers(data);
                            }
                            else {
                                self.LoadUsers(data);
                                self.selectedOtherThanTugSlotColumn(data);
                            }
                        }
                        if (data.IsCraft()) {
                            $("#selectedTugSlotColumnVCN").text(lclVCN);
                            $("#selectedTugSlotColumnVesselName").text(lclVesselName);
                            $("#selectedTugSlotColumnMovementType").text(lclMovementType);
                            $("#selectedTugSlotColumnResourceType").text(lclServiceTypeName);
                            $("#selectedTugSlotColumnVCN").val(lclVCN);
                            $("#selectedTugSlotColumnVesselName").val(lclVesselName);
                            $("#selectedTugSlotColumnMovementType").val(lclMovementType);
                            $("#selectedTugSlotColumnResourceType").val(lclServiceTypeName);

                            $('#TugModel').modal('toggle');
                            $("#TugModel").modal('show');
                            $("#OtherThanTugModel").modal('hide');
                        }
                        else {
                            $("#selectedOtherThanTugSlotColumnVCN").text(lclVCN);
                            $("#selectedOtherThanTugSlotColumnVesselName").text(lclVesselName);
                            $("#selectedOtherThanTugSlotColumnMovementType").text(lclMovementType);
                            $("#selectedOtherThanTugSlotColumnResourceType").text(lclServiceTypeName);
                            $("#selectedOtherThanTugSlotColumnVCN").val(lclVCN);
                            $("#selectedOtherThanTugSlotColumnVesselName").val(lclVesselName);
                            $("#selectedOtherThanTugSlotColumnMovementType").val(lclMovementType);
                            $("#selectedOtherThanTugSlotColumnResourceType").val(lclServiceTypeName);
                            $('#OtherThanTugModel').modal('toggle');
                            $("#OtherThanTugModel").modal('show');
                            $("#TugModel").modal('hide');
                        }
                    }
                    else {
                        return false;
                    }
                }
                else {
                    toastr.warning("Resource cannot change.", "Automated Resource Allocation");
                }
            }
            else {
                toastr.warning("You don't have permission.", "Automated Resource Allocation");
            }
        }

        self.SelectOtherThanTugResource = function (data) {
            if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD')) {
                var selectedValue = $("#OtherThanTugResourceID").val();
                var selectedText = $("#OtherThanTugResourceID option:selected").text();
                if (selectedValue == "") { return false; }

                if (selectedText.length > 17) {
                    selectedText = selectedText.substring(0, 15) + "...";
                }

                self.selectedOtherThanTugSlotColumn().ResourceID(selectedValue);
                self.selectedOtherThanTugSlotColumn().ResourceName(selectedText);
                data.ResourceID(selectedValue);
                if (selectedValue != null) {
                    self.selectedOtherThanTugSlotColumn().TaskStatus('OVRD');  //
                }                
                data.IsChanged(true);
                $(".close").trigger("click");                
            }
        }

        self.SelectTugResource = function (data) {           
            if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD')) {
                var ResourceSelectedValue = $("#TugResourceID").val();
                var ResourceSelectedText = $("#TugResourceID option:selected").text();
                var CraftSelectedValue = $("#TugCraftID").val();
                var CraftSelectedText = $("#TugCraftID option:selected").text();
                if (ResourceSelectedValue == "" || CraftSelectedValue == "") { return false; }
                var selectedText = CraftSelectedText + "/" + ResourceSelectedText;

                if (selectedText.length > 17) {
                    selectedText = selectedText.substring(0, 15) + "...";
                }

                self.selectedTugSlotColumn().TugResourceName(selectedText);
                data.ResourceID(ResourceSelectedValue);
                data.CraftID(CraftSelectedValue);
                data.IsChanged(true);
                if (ResourceSelectedValue != null) {
                    self.selectedTugSlotColumn().TaskStatus('OVRD'); 
                }
                
                $(".close").trigger("click");
            }
        }

        self.DeleteTugResource = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD')) {
                data.TaskStatus('PNDG');
                data.CraftID(0);
                data.ResourceID(0);
                data.IsChanged(true);
                data.TugResourceName('Unscheduled');                

            }
            else {
                toastr.warning("Resource cannot be deleted.", "Automated Resource Allocation");
            }

            $(".close").trigger("click");
        }

        self.DeleteOtherThanTugResource = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD')) {
                data.TaskStatus('PNDG');
                data.ResourceID(0);
                data.IsChanged(true);
                data.ResourceName('Unscheduled');                

            }
            else {
                toastr.warning("Resource cannot be deleted.", "Automated Resource Allocation");
            }

            $(".close").trigger("click");
        }

        self.SlotsCount = ko.observableArray();
        self.ResourceCalendarSlotsCount = ko.observableArray();
        self.ResourceAllocationCount = ko.observableArray();
        self.ResourceCalendarSlotsCount2 = ko.observableArray();
        self.ResourceAllocationCount2 = ko.observableArray();

        self.LoadSlotsCount = function () {

            self.viewModelHelper.apiGet('api/GetSlotConfiguration/{slotDate}', { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
                function (result) {
                    if (result.length > 0) {
                        self.SlotsCount(ko.utils.arrayMap(result, function (item) {
                            return new IPMSROOT.Slot(item);
                        }))
                    };                    
                    self.maximumSlots(self.SlotsCount().length);
                    var dynamciWidth = ((self.SlotsCount().length * 150) + 150 + 30 + 250 + 5) + 'px'
                    self.maintableWidth(dynamciWidth);
                    dynamciWidth = (self.SlotsCount().length * 150) + 'px';
                    self.colwidth(dynamciWidth);
                });
        }

        self.addResourceForGivenVCN = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            var selectedValue = $("#ResourceServiceType").val();
            var selectedText = $("#ResourceServiceType option:selected").text();

            if (selectedValue != "") {

                $.each(self.movementResourceAllocations(), function (key, value) {
                    var objSlotNumber = 1;
                    if (value.ServiceRequestId() == data.ServiceRequestId()) {
                        var valueOfSlotNumber = ko.utils.arrayFilter(value.ResourceAllocationSlots()[0].ResourceSlots(), function (UserIDSelect) {

                            if (UserIDSelect.ResourceName() != null) {
                                objSlotNumber = UserIDSelect.SlotNumber();
                            }
                          
                        });
                        var isCraftValue;



                      
                        var selectedDate = self.CurrentDate();
                    



                        $.each(self.getServiceTypeList(), function (index, value) {
                            if (value.ServiceTypeCode() == selectedValue) {
                                isCraftValue = value.IsCraft();
                            }
                        });
                     
                        self.objResourceAllocationSlot = new IPMSROOT.ResourceAllocationSlots({ AllocSlot: 1, AddSlotNumber: objSlotNumber, count: self.SlotsCount().length, ServiceTypeCode: selectedValue, ServiceTypeName: selectedText, IsCraft: isCraftValue, ServiceTypeCode: selectedValue, ServiceTypeName: selectedText, TaskStatus: 'PNDG', IsServiceTypeDeleted: false, ServiceReferenceID: value.ServiceRequestId(), IsChanged: true, AllocationDate: selectedDate, StartTime: "", EndTime: "" });
                        self.movementResourceAllocations()[key].ResourceAllocationSlots.push(self.objResourceAllocationSlot);

                        $(".close").trigger("click");
                    }
                });
            }
            else {
                toastr.warning("Please select resource type.", "Automated Resource Allocation");
            }
        }

        self.MovmentPopUpClick = function (data) {

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.viewModelHelper.apiGet('api/VerifyMovementIsActiveByVCNAndServiceRequestID/' + data.VCN() + '/' + data.ServiceRequestId(), null,
                function (result) {
                    if (result) {
                        $('#MovementModel').modal('toggle');
                        $("#MovementModel").modal('show');
                        self.LoadServiceTypes();
                        self.LoadShiftsTypes();
                        self.MovementPopUp(data);
                    }
                    else {
                        toastr.warning("Cannot add resource, vessel is berthed.", "Automated Resource Allocation");
                    }
                }, null, null, false);
        }

        self.removeResourceAllocation = function (data) {
            self.movementResourceAllocations().ResourceSlots().remove(data.ResourceSlots())
        }

        //Purpose : To get Service Type From API (DDL for Service Type)
        self.serviceTypeData = ko.observable();
        self.LoadServiceTypes = function () {
            self.viewModelHelper.apiGet('api/GetServiceTypes', null,
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getServiceTypeList);
                    });
        }

        self.LoadShiftsTypes = function () {
            self.viewModelHelper.apiGet('api/GetAllShiftTypes', null,
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getShiftsTypeList);
                    });
        }

        self.schedulerescource = function (ConfirmedServiceRequest) {
                       

            // confirmation box - start
            $.confirm({
                'title': ' Automated Resource Allocation',
                'message': 'Do you want to Schedule?',
                'buttons': {
                    'Yes': {
                        'class': 'blue',
                        'action': function () {
                            self.viewModelHelper.apiPost('api/ScheduleResource', ko.mapping.toJSON(ConfirmedServiceRequest), function Message(data) {

                                var result = data.split(',');

                                if (parseInt(result[0]) > 0) {

                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Automated resource allocation scheduled successfully.", "Automated Resource Allocation");
                                    self.AutomatedResourceSchedule(new IPMSROOT.ConfirmedServiceRequestsModel());
                                    self.LoadConfirmedServiceReqDetails();
                                    self.LoadResourceAllocations();                                   
                                    $("#grid").data("kendoGrid").dataSource.data(self.ConfirmedServiceRequest());                                    
                                }
                                else if (result[1] == "true") {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.warning("We can not schedule the service types.", "Automated Resource Allocation");
                                }
                                else if (result[1] == "false") {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.error("We do not have service types, please create service types.", "Automated Resource Allocation");
                                }
                            });
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

        self.saveResourceAllocation = function (allocationdata) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            

            var serviceRequestId = allocationdata.ServiceRequestId();
            var movements = self.movementResourceAllocations();
            var thisSelectedMovement;
            for (var i = 0; i < movements.length; i++) {
                if (movements[i].ServiceRequestId() == serviceRequestId) {
                    thisSelectedMovement = movements[i];
                    break;
                }
            }
            
            var count = 0;
            $.each(thisSelectedMovement.ResourceAllocationSlots(), function (index, value) {
                ko.utils.arrayFilter(value.ResourceSlots(), function (prod) {
                    if (prod.ResourceID() == null || prod.ResourceID() == 0) {
                    } else {
                        return count += 1;
                    }
                });
            });

            if (count == 0) {
                toastr.warning("Atleast one resource is required.", "Automated Resource Allocation");
                return false;
            }

            if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                // confirmation box - start
                $.confirm({
                    'title': ' Automated Resource Allocation',
                    'message': 'Do you want to Save?',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {
                                self.viewModelHelper.apiPost('api/AutomatedResourceScheduling/PostResourceAllocations', ko.toJSON(allocationdata), function (data) {
                                    toastr.success("Automated resource allocation saved successfully.", "Automated Resource Allocation");
                                    self.LoadResourceAllocations();
                                });
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
                var date = moment(self.Date()).format('HH:mm');
                var hours = date.split(':');

                var Dateminutes = new Date(self.Date());
                var totalminutes = (Dateminutes.getHours()) * 60 + Dateminutes.getMinutes();

                var slot = ko.observableArray();
                slot = allocationdata.ResourceAllocationSlots()[0].ResourceSlots();
                var currentIndex = null;
                var obj = JSON.parse(ko.toJSON(self.SlotsCount));
                var number = null;
                $.each(obj, function (index, value) {                  

                    var startminutes = value.StartMinutes;
                    var endminutes = value.EndMinutes;

                    if (endminutes < startminutes) {
                        endminutes = startminutes + (value.Duration - 1);
                    }

                    if (totalminutes >= startminutes && totalminutes < endminutes) {
                        number = value.SlotNumber;
                        return false;
                    }
                });

                $.each(slot, function (index, value) {
                    if (value.ResourceID() != null) {
                        currentIndex = value.SlotNumber();
                        return false;
                    }
                });

                if (parseInt(currentIndex) >= parseInt(number)) {

                    // confirmation box - start
                    $.confirm({
                        'title': ' Automated Resource Allocation',
                        'message': 'Do you want to Save?',
                        'buttons': {
                            'Yes': {
                                'class': 'blue',
                                'action': function () {
                                    self.viewModelHelper.apiPost('api/AutomatedResourceScheduling/PostResourceAllocations', ko.toJSON(allocationdata), function (data) {
                                        toastr.success("Automated resource allocation saved successfully.", "Automated Resource Allocation");
                                        self.LoadResourceAllocations();
                                    });
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
                    toastr.warning("You can not save previous slots.", "Automated Resource Allocation");
                    return false;
                }
            }
        }

        self.removeResourceAllocation = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var serviceRequestId = data.ServiceReferenceID();
            var movements = self.movementResourceAllocations();
            var thisSelectedMovement;
            for (var i = 0; i < movements.length; i++) {
                if (movements[i].ServiceRequestId() == serviceRequestId) {
                    thisSelectedMovement = movements[i];
                    break;
                }
            }

            var count = 0;
            $.each(thisSelectedMovement.ResourceAllocationSlots(), function (index, value) {
                if (!value.IsServiceTypeDeleted()) {
                    count = count + 1;
                }
            });

            if (count > 1) {
                if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                    var rsc = ko.utils.arrayFilter(data.ResourceSlots(), function (item) {
                        return (item.ResourceID() != null || item.ResourceID() != undefined);
                    });
                    if (rsc[0] != undefined) {
                        if (rsc[0].ResourceID() == 0) {

                            // confirmation box - start
                            $.confirm({
                                'title': ' Automated Resource Allocation',
                                'message': 'Do you want to remove anyway?',
                                'buttons': {
                                    'Yes': {
                                        'class': 'blue',
                                        'action': function () {
                                            data.IsServiceTypeDeleted(true);
                                            rsc[0].IsChanged(true);
                                        }
                                    },
                                    'No': {
                                        'class': 'gray',
                                        'action': function () {
                                        }
                                    }
                                }
                            });
                        } else {
                            if (rsc[0].ResourceID() > 0) {
                                toastr.warning("You can not remove service type. if you wish to remove first Unscheduled the user.", "Automated Resource Allocation");
                            }
                        }
                    }
                    else {
                        // confirmation box - start
                        $.confirm({
                            'title': ' Automated Resource Allocation',
                            'message': 'Do you want to Remove?',
                            'buttons': {
                                'Yes': {
                                    'class': 'blue',
                                    'action': function () {
                                        data.IsServiceTypeDeleted(true);
                                        data.ResourceSlots().IsChanged(true);
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
                }
                else {

                    var date = moment(self.Date()).format('HH:mm');
                    var hours = date.split(':');

                    var slot = ko.observableArray();
                    slot = data.ResourceSlots();
                    var currentIndex = null;
                    var obj = JSON.parse(ko.toJSON(self.SlotsCount));
                    var number = null;

                    var Dateminutes = new Date(self.Date());
                    var totalminutes = (Dateminutes.getHours()) * 60 + Dateminutes.getMinutes();

                    $.each(obj, function (index, value) {                        
                        var startminutes = value.StartMinutes;
                        var endminutes = value.EndMinutes;

                        if (endminutes < startminutes) {
                            endminutes = startminutes + (value.Duration - 1);
                        }

                        if (totalminutes >= startminutes && totalminutes < endminutes) {
                            number = value.SlotNumber;
                            return false;
                        }
                    });

                    $.each(slot, function (index, value) {
                        if (value.ResourceID() != null) {
                            currentIndex = value.SlotNumber();
                            return false;
                        }
                    });

                    if (parseInt(currentIndex) >= parseInt(number)) {
                        var rsc = ko.utils.arrayFilter(data.ResourceSlots(), function (item) {
                            return (item.ResourceID() != null || item.ResourceID() != undefined);
                        });
                        if (rsc[0] != undefined) {
                            if (rsc[0].ResourceID() == 0) {

                                // confirmation box - start
                                $.confirm({
                                    'title': ' Automated Resource Allocation',
                                    'message': 'Do you want to remove anyway?',
                                    'buttons': {
                                        'Yes': {
                                            'class': 'blue',
                                            'action': function () {
                                                data.IsServiceTypeDeleted(true);
                                                rsc[0].IsChanged(true);
                                            }
                                        },
                                        'No': {
                                            'class': 'gray',
                                            'action': function () {
                                            }
                                        }
                                    }
                                });                                
                            } else {
                                if (rsc[0].ResourceID() > 0) {
                                    toastr.warning("You can not remove service type. if you wish to remove first Unscheduled the user.", "Automated Resource Allocation");
                                }
                            }
                        }
                        else {

                            // confirmation box - start
                            $.confirm({
                                'title': ' Automated Resource Allocation',
                                'message': 'Do you want to Remove?',
                                'buttons': {
                                    'Yes': {
                                        'class': 'blue',
                                        'action': function () {
                                            data.IsServiceTypeDeleted(true);
                                            data.ResourceSlots().IsChanged(true);
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
                    }
                    else {
                        toastr.warning("You can't remove previous slots.", "Automated Resource Allocation");
                        return false;
                    }
                }
            }
            else {
                toastr.warning("Atleast one resource is required.", "Automated Resource Allocation");
                return false;
            }
        }

        self.confirmResourceAllocation = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                var result;
                var cnt = 0;

                $.each(data.ResourceAllocationSlots(), function (index, res) {
                    result = ko.utils.arrayFilter(res.ResourceSlots(), function (prod) {
                        if (prod.ResourceID() != null && prod.ResourceID() == 0) {
                            return cnt += 1;
                        }
                    });
                });

                data.OperationType('CNFR');// For Confirm Resource Allocation

                if (cnt == 0) {

                    // confirmation box - start
                    $.confirm({
                        'title': ' Automated Resource Allocation',
                        'message': 'Do you want to Confirm?',
                        'buttons': {
                            'Yes': {
                                'class': 'blue',
                                'action': function () {
                                    data.IsConfirm(true);

                                    self.viewModelHelper.apiPost('api/AutomatedResourceScheduling/PostResourceAllocations', ko.toJSON(data),
                                        function (data) {
                                            toastr.options.closeButton = true;
                                            toastr.options.positionClass = "toast-top-right";
                                            toastr.success("Automated resource allocation confirmed successfully.", "Automated Resource Allocation");
                                            self.LoadResourceAllocations();
                                        });
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
                    toastr.warning("All resources should be allocated for seleted slot.", "Automated Resource Allocation");
                }
            }
            else {

                var date = moment(self.Date()).format('HH:mm');
                var hours = date.split(':');

                var slot = ko.observableArray();
                slot = data.ResourceAllocationSlots()[0].ResourceSlots();
                var currentIndex = null;
                var obj = JSON.parse(ko.toJSON(self.SlotsCount));
                var number = null;
                var Dateminutes = new Date(self.Date());
                var totalminutes = (Dateminutes.getHours()) * 60 + Dateminutes.getMinutes();

                $.each(obj, function (index, value) {

                    var startminutes = value.StartMinutes;
                    var endminutes = value.EndMinutes;

                    if (endminutes < startminutes) {
                        endminutes = startminutes + (value.Duration - 1);
                    }

                    if (totalminutes >= startminutes && totalminutes < endminutes) {
                        number = value.SlotNumber;
                        return false;
                    }
                });

                $.each(slot, function (index, value) {
                    if (value.ResourceID() != null) {
                        currentIndex = value.SlotNumber();
                        return false;
                    }
                });

                if (parseInt(currentIndex) >= parseInt(number)) {
                    var result;
                    var cnt = 0;

                    $.each(data.ResourceAllocationSlots(), function (index, res) {
                        result = ko.utils.arrayFilter(res.ResourceSlots(), function (prod) {
                            if (prod.ResourceID() != null && prod.ResourceID() == 0) {
                                return cnt += 1;
                            }
                        });
                    });

                    data.OperationType('CNFR');// For Confirm Resource Allocation

                    if (cnt == 0) {

                        // confirmation box - start
                        $.confirm({
                            'title': ' Automated Resource Allocation',
                            'message': 'Do you want to Confirm?',
                            'buttons': {
                                'Yes': {
                                    'class': 'blue',
                                    'action': function () {
                                        data.IsConfirm(true);

                                        self.viewModelHelper.apiPost('api/AutomatedResourceScheduling/PostResourceAllocations', ko.toJSON(data),
                                            function (data) {
                                                toastr.options.closeButton = true;
                                                toastr.options.positionClass = "toast-top-right";
                                                toastr.success("Automated resource allocation confirmed successfully.", "Automated Resource Allocation");
                                                self.LoadResourceAllocations();
                                            });
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
                        toastr.warning("All resources should be allocated for seleted slot.", "Automated Resource Allocation");
                    }

                }
                else {
                    toastr.warning("You can not confirm previous slots.", "Automated Resource Allocation");
                    return false;
                }
            }
        }

        self.deallocateResourceAllocation = function (allocationdata) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var isStartedCount = 0;
            var obj = JSON.parse(ko.toJSON(allocationdata));
            $.each(obj.ResourceAllocationSlots, function (key, value1) {
                var avblcrafts = ko.utils.arrayFilter(value1.ResourceSlots, function (item) {
                    if (item.TaskStatus == "STRD" || item.TaskStatus == "ACCP" || item.TaskStatus == "CFRI" || item.TaskStatus == "COMP" || item.TaskStatus == "VERF") {
                        isStartedCount++;
                    }
                    return (item.ResourceID != null);
                })[0];
            });

            if (isStartedCount == 0) {

                // confirmation box - start
                $.confirm({
                    'title': 'Automated Resource Allocation',
                    'message': 'Do you want to Deallocate?',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {
                                allocationdata.OperationType('DEAL');// For Deallocate Resource to Vessel Call Movement
                                self.viewModelHelper.apiPost('api/AutomatedResourceScheduling/PostResourceAllocations', ko.toJSON(allocationdata), function (data) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Automated resource allocation deallocated successfully.", "Automated Resource Allocation");
                                    self.AutomatedResourceSchedule(new IPMSROOT.ConfirmedServiceRequestsModel());
                                    self.LoadConfirmedServiceReqDetails();
                                    self.LoadResourceAllocations();
                                    self.LoadServiceTypes();
                                    self.LoadSlotsCount();
                                    self.LoadShiftsTypes();
                                    //window.location.reload(true);
                                    $("#grid").data("kendoGrid").dataSource.data(self.ConfirmedServiceRequest());
                                });
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
                toastr.warning("You can not deallocate as some resources are in progress state.", "Automated Resource Allocation");
            }
        }

        self.getResourceByDate = function (data) {
            var date = data.ResourceDate();
            self.CurrentDate(date);
            //self.LoadSlotsCount();
            //self.LoadResourceAllocations();
            //self.ConfirmedServiceRequest.removeAll();
            //self.LoadConfirmedServiceReqDetails();
            $('.nav-tabs a[href="#tab_0"]').tab('show');

            //self.SelectResourceCalendar();

            var dt = new Date();
            dt.setDate(dt.getDate() + 1);
            var currentDate = self.CurrentDate();

            if (currentDate > dt) {
                self.IsValidDate(false);
            }
            else {
                self.IsValidDate(true);
            }

            currentDate.setDate(currentDate.getDate());
            if ((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) {
                self.IsCurrentDate(true);
            }
            else {
                self.IsCurrentDate(false);
            }

            self.CurrentDate(currentDate);
            $('.displaytxtResourceAllocation').text(moment(currentDate).format('MMM DD, YYYY'));

            //var date = moment($('.displaytxtResourceAllocation').text(moment(currentDate).format('MMM DD, YYYY'))).format('YYYY-MM-DD hh:mm:ss A')
            // self.CurrentDate(date);
            self.LoadSlotsCount();
            self.LoadResourceAllocations();
            self.ConfirmedServiceRequest.removeAll();
            self.LoadConfirmedServiceReqDetails();
            $("select#ResourceServiceTypeForRC").prop('selectedIndex', 0);
            $("select#ResourceShiftTypeForRC").prop('selectedIndex', 0);
            $("select#ResourceServiceTypeForCA").prop('selectedIndex', 0);
            $("select#ResourceShiftTypeForCA").prop('selectedIndex', 0);
            self.ResourceCalendarSlotsCount.removeAll();
            self.ResourceCalendarSlotsCount2.removeAll();
            self.movementResourceCalendarModel([]);
            self.movementResourceCalendarModel2([]);
        }

        self.clickRefresh = function () {
            window.location.reload();
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.calMaxToday = function () {

            var date = new Date();
            date.setDate(date.getDate() + 1);
            this.max(date);
        }

        function UpdatedResourceAllocation(movement, targetedIndex) {

        }

        self.LoadPreviousDay = function () {
            $('.nav-tabs a[href="#tab_0"]').tab('show');

            //self.SelectResourceCalendar();

            var dt = new Date();
            dt.setDate(dt.getDate() + 1);
            var currentDate = self.CurrentDate();

            if (currentDate > dt) {
                self.IsValidDate(false);
            }
            else {
                self.IsValidDate(true);
            }

            currentDate.setDate(currentDate.getDate() - 1);
            if ((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) {
                self.IsCurrentDate(true);
            }
            else {
                self.IsCurrentDate(false);
            }

            self.CurrentDate(currentDate);
            $('.displaytxtResourceAllocation').text(moment(currentDate).format('MMM DD, YYYY'));

            //var date = moment($('.displaytxtResourceAllocation').text(moment(currentDate).format('MMM DD, YYYY'))).format('YYYY-MM-DD hh:mm:ss A')
            // self.CurrentDate(date);
            self.LoadSlotsCount();
            self.LoadResourceAllocations();
            self.ConfirmedServiceRequest.removeAll();
            self.LoadConfirmedServiceReqDetails();
            $("select#ResourceServiceTypeForRC").prop('selectedIndex', 0);
            $("select#ResourceShiftTypeForRC").prop('selectedIndex', 0);
            $("select#ResourceServiceTypeForCA").prop('selectedIndex', 0);
            $("select#ResourceShiftTypeForCA").prop('selectedIndex', 0);
            self.ResourceCalendarSlotsCount.removeAll();
            self.ResourceCalendarSlotsCount2.removeAll();
            self.movementResourceCalendarModel([]);
            self.movementResourceCalendarModel2([]);
            self.ResourceDate(moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm'));
        }

        self.LoadNextDay = function () {
            $('.nav-tabs a[href="#tab_0"]').tab('show');

            var dt = new Date();
            var dt1 = new Date();
            var varcurrentDate = self.CurrentDate();
            var futureDate = self.CurrentDate();
            dt1.setDate(dt1.getDate() - 1);

            if (futureDate > dt1) {
                self.IsValidDate(false);
            }
            else {
                self.IsValidDate(true);
            }

            varcurrentDate.setDate(varcurrentDate.getDate() + 1);
            if ((dt.getDate() == varcurrentDate.getDate()) && (dt.getMonth() == varcurrentDate.getMonth()) && (dt.getFullYear() == varcurrentDate.getFullYear())) {
                self.IsCurrentDate(true);
            }
            else {
                self.IsCurrentDate(false);
            }

            self.CurrentDate(varcurrentDate);
            $('.displaytxtResourceAllocation').text(moment(self.CurrentDate()).format('MMM DD, YYYY'));

            //var date = moment($('.displaytxtResourceAllocation').text(moment(self.CurrentDate()).format('MMM DD, YYYY'))).format('YYYY-MM-DD hh:mm:ss A')
            // self.CurrentDate(date);
            self.LoadSlotsCount();
            self.LoadResourceAllocations();
            self.ConfirmedServiceRequest.removeAll();
            self.LoadConfirmedServiceReqDetails();
            $("select#ResourceServiceTypeForRC").prop('selectedIndex', 0);
            $("select#ResourceShiftTypeForRC").prop('selectedIndex', 0);
            $("select#ResourceServiceTypeForCA").prop('selectedIndex', 0);
            $("select#ResourceShiftTypeForCA").prop('selectedIndex', 0);
            self.ResourceCalendarSlotsCount.removeAll();
            self.ResourceCalendarSlotsCount2.removeAll();
            self.movementResourceCalendarModel([]);
            self.movementResourceCalendarModel2([]);
            self.ResourceDate(moment(self.CurrentDate()).format('YYYY-MM-DD HH:mm'));
        }

        self.SelectCraftCalendar = function () {
            self.LoadSlotsCount();
            var obj = JSON.parse(ko.toJSON(self.SlotsCount));
            var selectedShiftVal = $("#ResourceShiftTypeForCA").val();
            self.ResourceCalendarSlotsCount2.removeAll();
            ko.utils.arrayFilter(self.SlotsCount(), function (Slt) {
                if (Slt.ShiftID() == parseInt(selectedShiftVal)) {
                    self.ResourceCalendarSlotsCount2.push(Slt);
                }
            });
            var dynamciWidth1 = ((self.ResourceCalendarSlotsCount2().length * 150) + 150 + 30 + 250 + 5) + 'px'
            self.maintableWidthRC(dynamciWidth1);
            dynamciWidth1 = (self.ResourceCalendarSlotsCount2().length * 150) + 'px';
            self.colwidthRC(dynamciWidth1);
            var ResourceCalendarSearchModel = new IPMSROOT.ResourceCalendarSearchModel();
            ResourceCalendarSearchModel.ShiftID = $("#ResourceShiftTypeForCA").val();
            ResourceCalendarSearchModel.OperationType = $("#ResourceServiceTypeForCA").val();
            ResourceCalendarSearchModel.AllocationDate = moment(self.CurrentDate()).format('YYYY-MM-DD');
            self.ResourceTypeHeaderForCalendar($("#ResourceServiceTypeForCA option:selected").text());
            if (ResourceCalendarSearchModel != undefined) {
                self.LoadCraftCalendar(ResourceCalendarSearchModel);
            }
        }

        self.SelectResourceCalendar = function () {
            self.LoadSlotsCount();
            var obj = JSON.parse(ko.toJSON(self.SlotsCount));
            var selectedShiftVal = $("#ResourceShiftTypeForRC").val();
            self.ResourceCalendarSlotsCount.removeAll();
            ko.utils.arrayFilter(self.SlotsCount(), function (Slt) {
                if (Slt.ShiftID() == parseInt(selectedShiftVal)) {
                    self.ResourceCalendarSlotsCount.push(Slt);
                }
            });
            var dynamciWidth1 = ((self.ResourceCalendarSlotsCount().length * 150) + 150 + 30 + 250 + 5) + 'px'
            self.maintableWidthRC(dynamciWidth1);
            dynamciWidth1 = (self.ResourceCalendarSlotsCount().length * 150) + 'px';
            self.colwidthRC(dynamciWidth1);
            var ResourceCalendarSearchModel = new IPMSROOT.ResourceCalendarSearchModel();
            ResourceCalendarSearchModel.ShiftID = $("#ResourceShiftTypeForRC").val();
            ResourceCalendarSearchModel.OperationType = $("#ResourceServiceTypeForRC").val();
            ResourceCalendarSearchModel.AllocationDate = moment(self.CurrentDate()).format('YYYY-MM-DD');
            self.ResourceTypeHeaderForCalendar($("#ResourceServiceTypeForRC option:selected").text());
            if (ResourceCalendarSearchModel != undefined) {
                self.LoadResourceCalendar(ResourceCalendarSearchModel);
            }
        }

        self.LoadCraftCalendar = function (data) {

            self.viewModelHelper.apiPost('api/GetCraftCalendarDetails', ko.toJSON(data),
                function (result) {

                    if (result.length > 0) {
                        self.movementResourceCalendarModel2(ko.utils.arrayMap(result, function (item) {
                            return new IPMSROOT.ResourceCalendarModel(item);
                        }))
                    } else {
                        self.movementResourceCalendarModel2([]);
                    };
                }, null, null, false);
        }

        self.LoadResourceCalendar = function (data) {

            self.viewModelHelper.apiPost('api/GetResourceCalendarDetails', ko.toJSON(data),
                function (result) {

                    if (result.length > 0) {
                        self.movementResourceCalendarModel(ko.utils.arrayMap(result, function (item) {
                            return new IPMSROOT.ResourceCalendarModel(item);
                        }))
                    } else {
                        self.movementResourceCalendarModel([]);
                    };
                }, null, null, false);
        }

        self.LoadPortName = function () {
            self.viewModelHelper.apiGet('api/GetPortNameByPortCode', null,
                function (result) {
                    self.PortName(result);
                });
        }

        self.LoadCraftAvailabilityServiceTypes = function () {
            self.viewModelHelper.apiGet('api/GetCraftAvailabilityServiceTypes', null,
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getCraftAvailabilityServiceTypes);
                    });
        }

        self.GetActiveSlots = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.activeSlots.removeAll();
            self.viewModelHelper1.apiGet('api/GetActiveSlots', null,
                function (result) {
                    ko.mapping.fromJS(result, {}, self.activeSlots);
                }, null, null, false);
        }

        self.Initialize();
    }

    IPMSRoot.AutomatedResourceScheduleViewModel = AutomatedResourceScheduleViewModel;
}(window.IPMSROOT));
