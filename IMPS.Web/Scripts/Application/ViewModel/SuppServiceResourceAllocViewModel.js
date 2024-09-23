(function (IPMSRoot) {

    var SuppServiceResourceAllocViewModel = function () {

        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.suppServiceResourceAllocList = ko.observableArray();
        self.maximumSlots = ko.observable();
        self.isTableFull = function (parent) {
            return parent().length < self.maximumSlots;
        };

        self.Users = ko.observableArray();
        self.VCN = ko.observable();
        self.VCNList = ko.observableArray();
        self.SlotsCount = ko.observableArray();
        self.getResourceList = ko.observableArray();
        self.colwidth = ko.observable('1332px');
        self.maintableWidth = ko.observable('1332px');
        self.maintableWidthRC = ko.observable('2235px');
        self.Date = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });

        self.CurrentDate = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });

        self.status = ko.observable(false);
        //self.IsValidDate = ko.observable(true);
        self.IsCurrentDate = ko.observable(true);
        $('.displaytxtResourceAllocation').text(moment(self.CurrentDate()).format('MMM DD, YYYY'));
        self.Date = ko.observable(new Date()).extend({
            isoDate: 'dd/mm/yyyy'
        });
        self.isChange = ko.observable(false);
        self.getShiftsTypeList = ko.observableArray();
        self.ResourceTypeHeaderForCalendar = ko.observable();
        self.getResourceCalendarList = ko.observableArray();
        self.ResourceCalendarSlotsCount = ko.observableArray();
        self.ResourceAllocationCount = ko.observableArray();
        self.movementResourceCalendarModel = ko.observableArray();
        self.colwidthRC = ko.observable('900px');
        self.PortName = ko.observable();
        self.activeSlots = ko.observableArray();
        self.activeResourceSlots = ko.observableArray();
        self.nextDayStatus = ko.observable(false);

        //self.IsChanged = ko.observable(false);

        self.Initialize = function () {

            self.LoadVCN();
            self.LoadSlotsCount();
            self.LoadShiftsTypes();
            self.LoadSuppServiceResourceAlloc();
            self.LoadPortName();
        }

        self.LoadPortName = function () {
            self.viewModelHelper.apiGet('api/GetPortNameByPortCode', null,
                function (result) {
                    self.PortName(result);
                });
        }

        self.LoadShiftsTypes = function () {
            self.viewModelHelper.apiGet('api/GetAllShiftTypes', null,
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getShiftsTypeList);
                    });
        }

        self.SelectResourceCalendar = function () {
            self.LoadSlotsCount();
            var obj = JSON.parse(ko.toJSON(self.SlotsCount));
            var selectedShiftVal = $("#ResourceShiftTypeForRC").val();
            self.ResourceCalendarSlotsCount.removeAll();
            self.movementResourceCalendarModel.removeAll();
            ko.utils.arrayFilter(self.SlotsCount(), function (Slt) {
                if (Slt.ShiftID() == parseInt(selectedShiftVal)) {
                    self.ResourceCalendarSlotsCount.push(Slt);
                }
            });

            if (self.ResourceCalendarSlotsCount().length > 0) {

                var dynamciWidth1 = ((self.ResourceCalendarSlotsCount().length * 150) + 150 + 30 + 250 + 5) + 'px'
                self.maintableWidthRC(dynamciWidth1);
                dynamciWidth1 = (self.ResourceCalendarSlotsCount().length * 150) + 'px';
                self.colwidthRC(dynamciWidth1);
                var ResourceCalendarSearchModel = new IPMSROOT.ResourceCalendarSearchModel();
                ResourceCalendarSearchModel.ShiftID = $("#ResourceShiftTypeForRC").val();
                ResourceCalendarSearchModel.OperationType = "WTST";
                ResourceCalendarSearchModel.ServiceReferenceType = "SUPP";
                ResourceCalendarSearchModel.AllocationDate = moment(self.CurrentDate()).format('YYYY-MM-DD');
                //self.ResourceTypeHeaderForCalendar($("#ResourceServiceTypeForRC option:selected").text());
                if (ResourceCalendarSearchModel != undefined) {
                    self.LoadResourceCalendar(ResourceCalendarSearchModel);
                }
            }
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

        self.getVCNSelect = function (data) {
            //var date = moment(data.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A')
            //self.CurrentDate(date);
            $('.nav-tabs a[href="#tab_0"]').tab('show');
            $("#ResourceShiftTypeForRC").prop('selectedIndex', 0);
            self.ResourceCalendarSlotsCount.removeAll();
            self.movementResourceCalendarModel([]);
            self.LoadSlotsCount();
            self.LoadSuppServiceResourceAlloc();
        }

        self.selectedSlotColumn = ko.observable();

        self.LoadSuppServiceResourceAlloc = function () {
            var selectedValue = $("#VCNDetails").val();
            //var selectedValue = null;

            //self.viewModelHelper.apiGet('api/SuppServiceResourceAllocation/{slotDate}/{vcn}', { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A'), vcn: selectedValue },
            self.viewModelHelper.apiGet('api/ResourceAllocation/{slotDate}/{vcn}', { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A'), vcn: selectedValue },
                function (result) {
                    self.suppServiceResourceAllocList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSROOT.SuppServiceResourceAllocModel(item);
                    }));
                }, null, null, false);

            if (moment(self.Date()).format('YYYY-MM-DD') == moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                self.status(true);
            }
            else {
                self.status(false);
            }
        }

        self.LoadSlotsCount = function () {

            self.viewModelHelper.apiGet('api/GetSlotConfiguration/{slotDate}', { slotDate: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
                function (result) {

                    self.SlotsCount(ko.utils.arrayMap(result, function (item) {
                        return new IPMSROOT.Slot(item);
                    }));
                    //ko.utils.arrayMap(result, {}, self.SlotsCount);
                    //self.maximumSlots(self.SlotsCount.length);
                    self.maximumSlots(self.SlotsCount().length);
                    var dynamciWidth = ((self.SlotsCount().length * 150) + 150 + 30 + 250 + 5) + 'px'
                    self.maintableWidth(dynamciWidth);
                    dynamciWidth = (self.SlotsCount().length * 150) + 'px';
                    self.colwidth(dynamciWidth);
                }, null, null, false);

        }

        self.Confirm = function () {
            debugger;
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var count = 0;
            var slotcount = 0;

            self.GetActiveResourceSlots();

            $.each(self.suppServiceResourceAllocList(), function (index, item) {
                ko.utils.arrayMap(item.ResourceSlots(), function (res) {
                    if (res.TaskStatus() != null) {
                        $.each(self.activeResourceSlots(), function (index, value) {
                            if (value.SlotNumber() == res.SlotNumber()) {
                                slotcount = slotcount + 1;
                                if (res.TaskStatus() == "SCHD" || res.TaskStatus() == "OVRD" || (res.TaskStatus() == "PNDG" && self.isChange() == true)) {
                                    count = count + 1;
                                    item.IsConfirm("true");
                                    return false;
                                }
                            }
                        });
                    }
                });
            });

            //$.each(self.SlotsCount(), function (index, value) {
            //    if (value.SlotNumber() == number) {
            //        slotperiod = value.SlotPeriod();
            //        return false;
            //    }
            //});

            //self.GetActiveResourceSlots();
            //var status = false;

            //$.each(self.GetActiveResourceSlots(), function (index, value) {
            //    if (value == slotperiod) {
            //        status = true;
            //        return false;
            //    }
            //});

            //ko.utils.arrayMap(self.suppServiceResourceAllocList(), function (item) {

            //    ko.utils.arrayMap(item.ResourceSlots(), function (res) {
            //        if (res.TaskStatus() == "SCHD" || res.TaskStatus() == "OVRD" || (res.TaskStatus() == "PNDG" && self.isChange() == true)) {
            //            count = count + 1;
            //            return false;
            //        }
            //    });
            //});


            if (count > 0) {
                //self.viewModelHelper.apiPut('api/SuppServiceResourceAllocation', ko.toJSON(self.suppServiceResourceAllocList),
                self.viewModelHelper.apiPut('api/ResourceAllocation', ko.toJSON(self.suppServiceResourceAllocList),
                    function Message(data) {
                        toastr.success("Resource allocation saved successfully.", "Resource Allocation");
                        //self.IsChanged(false);
                        self.LoadSuppServiceResourceAlloc();
                        self.isChange(false);
                    });
            }
            else {
                if (slotcount == 0) {
                    toastr.warning("All are previous slots.", "Resource Allocation");
                }
                else {
                    toastr.warning("Resource allocation has no changes.", "Resource Allocation");
                }
            }
            //}
            //else {
            //    toastr.success("Resource Allocation No Changes", "Resource Allocation");
            //}
        }

        self.Cancel = function () {
            window.location = "/Welcome";
        }

        self.updateSlot = function (arg) {
            for (var j = 0; j < arg.targetParent().length; j++) {
                arg.targetParent()[j].SlotNumber(j + 1);
                //arg.targetParent()[arg.targetIndex].IsChanged(true);
                arg.targetParent()[arg.targetIndex].ResourceID(0);
                arg.targetParent()[arg.targetIndex].ResourceName("Unscheduled");
                arg.targetParent()[arg.targetIndex].TaskStatus("PNDG");
            }
            self.isChange(true);
        }

        //Verification logic goes here.
        self.verifyAssignments = function (arg) {
            debugger;
            if (arg.item.ResourceID() != null) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";

                var dt = moment(new Date()).format('YYYY-MM-DD');
                var currentDate = moment(self.CurrentDate()).format('YYYY-MM-DD');

                if (currentDate < dt) {
                    toastr.warning("Cannot move previous days slots.", "Resource Allocation");
                    arg.cancelDrop = true;
                }
                else if (currentDate == dt) {

                    var date = moment(self.Date()).format('HH:mm');
                    var hours = date.split(':');

                    var obj = JSON.parse(ko.toJSON(self.SlotsCount));
                    var number = null;
                    $.each(obj, function (index, value) {
                        var period = value.SlotPeriod.split('-');

                        if (parseInt(hours[0]) >= parseInt(period[0]) && parseInt(hours[0]) < parseInt(period[1])) {
                            number = value.SlotNumber;
                            return false;
                        }
                    });

                    if (parseInt(arg.targetIndex + 1) >= parseInt(number)) {
                        if (arg.item.TaskStatus() == 'STRD' || arg.item.TaskStatus() == 'VERF' || arg.item.TaskStatus() == 'COMP' || arg.item.TaskStatus() == 'ACCP' || arg.item.TaskStatus() == 'CFRI') {
                            arg.cancelDrop = true;
                            var toasterMessage = GetTaskMessage(arg.item.TaskStatus());
                            toastr.warning(toasterMessage, "Resource Allocation");
                            return;
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
                        toastr.warning("Can not move previous slots.", "Resource Allocation");
                        arg.cancelDrop = true;
                    }
                }
                else if (currentDate > dt) {
                    toastr.warning("Cannot move future days slots.", "Resource Allocation");
                    arg.cancelDrop = true;
                }
            }
            else {
                arg.cancelDrop = true;
            }
            //arg.item.TaskStatus()
        };

        GetTaskMessage = function (status) {
            if (status == "STRD")
                return "Can not move, Vessel is started.";
            else if (status == "VERF")
                return "Can not move, Vessel is verified.";
            else if (status == "COMP")
                return "Can not move, Vessel is completed.";
            else if (status == "CFRI")
                return "Can not move, Vessel is confirmed.";
            else if (status == "ACCP")
                return "Can not move, Vessel is accepted.";
        }

        GetSaveTaskMessage = function (status) {
            if (status == "STRD")
                return "Can not save, Vessel is started.";
            else if (status == "VERF")
                return "Can not save, Vessel is verified.";
            else if (status == "COMP")
                return "Can not save, Vessel is completed.";
            else if (status == "CFRI")
                return "Can not save, Vessel is confirmed.";
            else if (status == "ACCP")
                return "Can not save, Vessel is accepted.";
            else if (status == "REJT")
                return "Can not save, Vessel is rejected.";
            else if (status == "PNDG")
                return "Vessel has no changes.";
        }

        GetConfirmTaskMessage = function (status) {
            if (status == "STRD")
                return "Can not confirm, Vessel is started.";
            else if (status == "VERF")
                return "Can not confirm, Vessel is verified.";
            else if (status == "COMP")
                return "Can not confirm, Vessel is completed.";
            else if (status == "CFRI")
                return "Can not confirm, Vessel is confirmed.";
            else if (status == "ACCP")
                return "Can not confirm, Vessel is accepted.";
            else if (status == "REJT")
                return "Can not confirm, Vessel is rejected.";
            else if (status == "PNDG")
                return "Vessel has no changes.";
        }

        self.ResourceOnClick = function (data) {
            debugger;
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var dt = moment(new Date()).format('YYYY-MM-DD');
            var currentDate = moment(self.CurrentDate()).format('YYYY-MM-DD');
            var selectedVessel = null;

            if (data.ResourceID() != null) {
                if (currentDate < dt) {
                    if (data.ServiceTypeCode() == 'FCST') {
                        if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD' || data.TaskStatus() == 'ACCP')) {
                            $.each(self.suppServiceResourceAllocList(), function (key, value) {
                                if (parseInt(value.ServiceReferenceID()) == parseInt(data.ServiceReferenceID())) {
                                    selectedVessel = value;
                                    return;
                                }
                            });
                            var lblVCN = selectedVessel.VCN();
                            var lblVesselName = selectedVessel.VesselName();
                            var lblServiceType = selectedVessel.ServiceTypeName();
                            var lblBerth = selectedVessel.BerthName();
                            var lblDate = selectedVessel.AllocationDate();
                            //var lblQuantity = selectedVessel.Quantity();

                            if (data.ResourceID() > 0) {
                                self.LoadUsers(data);
                                self.selectedSlotColumn(data);
                                $('#ResourceID option')
                                    .filter(function () { return $.trim($(this).val()) == data.ResourceID(); })
                                    .attr('selected', true);
                            }
                            else {
                                self.LoadUsers(data);
                                self.selectedSlotColumn(data);
                            }
                            $("#ResourceDetails").hide();
                            $("#idVCN").text(lblVCN);
                            $("#idVesselName").text(lblVesselName);
                            $("#idServiceType").text(lblServiceType);
                            $("#idBerth").text(lblBerth);
                            $("#idDate").text(lblDate);
                            // $("#idQuantity").text(lblQuantity);
                            $('#ResourceModel').modal('toggle');
                            $("#ResourceModel").modal('show');
                            self.nextDayStatus(true);
                        }
                        else {
                            toastr.warning("Resource cannot change for previous days slot.", "Resource Allocation");
                            //$("#ResourceModel").modal('hide');
                        }
                    }
                    else {
                        toastr.warning("Resource cannot change for previous days slot.", "Resource Allocation");
                    }
                    //$("#ResourceModel").modal('hide');
                }
                else if (currentDate == dt) {
                    self.nextDayStatus(false);
                    var date = moment(self.Date()).format('HH:mm');
                    var hours = date.split(':');

                    var obj = JSON.parse(ko.toJSON(self.SlotsCount));
                    var number = null;
                    $.each(obj, function (index, value) {
                        var period = value.SlotPeriod.split('-');

                        if (parseInt(hours[0]) >= parseInt(period[0]) && parseInt(hours[0]) < parseInt(period[1])) {
                            number = value.SlotNumber;
                            return false;
                        }
                    });

                    if (parseInt(data.SlotNumber()) >= parseInt(number)) {

                        //if (data.ResourceName() == "" || data.ResourceName() == null) { return false; }

                        //alert('My Function On Click');            

                        //self.viewModelHelper.apiGet('api/SuppServiceResourceAlloc/GetSearchResource', { value: ko.toJSON(data) },
                        //    function (result) {
                        //        ko.utils.fromJS(result, {}, self.Users);
                        //    }, null, null, false);

                        if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD')) {
                            //if (data.ResourceID() != null) {

                            //var serviceRequestId = data.ServiceReferenceID();
                            //alert(serviceRequestId);
                            //var movements = self.suppServiceResourceAllocList();


                            $.each(self.suppServiceResourceAllocList(), function (key, value) {
                                if (parseInt(value.ServiceReferenceID()) == parseInt(data.ServiceReferenceID())) {
                                    selectedVessel = value;
                                    return;
                                }
                            });

                            //for (var i = 0; i < movements.length; i++) {
                            //    if (movements[i].ServiceRequestId() == serviceRequestId) {
                            //        thisSelectedMovement = movements[i];
                            //        break;
                            //    }
                            //}
                            var lblVCN = selectedVessel.VCN();
                            var lblVesselName = selectedVessel.VesselName();
                            var lblServiceType = selectedVessel.ServiceTypeName();
                            var lblBerth = selectedVessel.BerthName();
                            var lblDate = selectedVessel.AllocationDate();
                            //var lblQuantity = selectedVessel.Quantity();

                            if (data.ResourceID() > 0) {
                                self.LoadUsers(data);
                                self.selectedSlotColumn(data);
                                $('#ResourceID option')
                                    .filter(function () { return $.trim($(this).val()) == data.ResourceID(); })
                                    .attr('selected', true);
                            }
                            else {
                                self.LoadUsers(data);
                                self.selectedSlotColumn(data);
                            }

                            if (data.ServiceTypeCode() == 'WTST') {
                                $("#NextShift").hide();
                            }

                            $("#idVCN").text(lblVCN);
                            $("#idVesselName").text(lblVesselName);
                            $("#idServiceType").text(lblServiceType);
                            $("#idBerth").text(lblBerth);
                            $("#idDate").text(lblDate);
                            // $("#idQuantity").text(lblQuantity);
                            $('#ResourceModel').modal('toggle');
                            $("#ResourceModel").modal('show');
                        }
                        else {
                            toastr.warning("Resource cannot change.", "Resource Allocation");
                            //$("#ResourceModel").modal('hide');
                        }

                    }
                    else {
                        if (data.ServiceTypeCode() == 'FCST') {
                            if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD' || data.TaskStatus() == 'ACCP')) {
                                $.each(self.suppServiceResourceAllocList(), function (key, value) {
                                    if (parseInt(value.ServiceReferenceID()) == parseInt(data.ServiceReferenceID())) {
                                        selectedVessel = value;
                                        return;
                                    }
                                });
                                var lblVCN = selectedVessel.VCN();
                                var lblVesselName = selectedVessel.VesselName();
                                var lblServiceType = selectedVessel.ServiceTypeName();
                                var lblBerth = selectedVessel.BerthName();
                                var lblDate = selectedVessel.AllocationDate();
                                //var lblQuantity = selectedVessel.Quantity();

                                if (data.ResourceID() > 0) {
                                    self.LoadUsers(data);
                                    self.selectedSlotColumn(data);
                                    $('#ResourceID option')
                                        .filter(function () { return $.trim($(this).val()) == data.ResourceID(); })
                                        .attr('selected', true);
                                }
                                else {
                                    self.LoadUsers(data);
                                    self.selectedSlotColumn(data);
                                }
                                $("#ResourceID").attr('disabled', true);
                                $("#idVCN").text(lblVCN);
                                $("#idVesselName").text(lblVesselName);
                                $("#idServiceType").text(lblServiceType);
                                $("#idBerth").text(lblBerth);
                                $("#idDate").text(lblDate);
                                // $("#idQuantity").text(lblQuantity);
                                $("#btnSave").hide();
                                $("#btnDelete").hide();
                                $('#ResourceModel').modal('toggle');
                                $("#ResourceModel").modal('show');
                            }
                            else {
                                toastr.warning("Resource cannot change for previous slots.", "Resource Allocation");
                                //$("#ResourceModel").modal('hide');
                            }
                        }
                        else {
                            toastr.warning("Resource cannot change for previous slots.", "Resource Allocation");
                        }
                        //toastr.warning("Resource cannot change for previous slots.", "Resource Allocation");
                        //$("#ResourceModel").modal('hide');
                    }
                }
                else if (currentDate > dt) {
                    toastr.warning("Resource cannot change for future days slots.", "Resource Allocation");
                    // $("#ResourceModel").modal('hide');
                }
            }
            else {
                //return false;
                //$("#ResourceModel").modal('hide');
            }
        }

        self.SelectResource = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var selectedValue = $("#ResourceID").val();
            var selectedText = $("#ResourceID option:selected").text();
            if (selectedValue == "") {
                toastr.warning("Please select user.", "Resource Allocation", 3000);
                return false;
            }

            if (data.ResourceID() == selectedValue) {
                toastr.warning("Please select another user.", "Resource Allocation", 3000);
                return false;
            }
            else {

                if (selectedText.length > 18) {
                    selectedText = selectedText.substring(0, 16) + "...";
                }
                self.selectedSlotColumn().ResourceID(selectedValue);
                self.selectedSlotColumn().ResourceName(selectedText);
                data.TaskStatus("OVRD");
                data.ResourceID(selectedValue);
                // arg.item.IsChanged(true);
                //data.IsChanged(true);
                self.isChange(true);
            }
            $(".close").trigger("click");
            //self.IsChanged(true);
            //self.selectedSlotColumn().ResourceName("New Resource");
        }

        self.UpdateSlot = function (data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var selectedVessel;

            $.each(self.suppServiceResourceAllocList(), function (key, value) {
                if (parseInt(value.ServiceReferenceID()) == parseInt(data.ServiceReferenceID())) {
                    selectedVessel = value;
                    return;
                }
            });

            self.viewModelHelper.apiPut('api/UpdateSlotById/' + selectedVessel.ResourceAllocationID(), null,
                function (result) {
                    toastr.warning("Slot changed successfully.", "Resource Allocation");
                    self.LoadVCN();
                    self.LoadSuppServiceResourceAlloc();
                });
            $(".close").trigger("click");

        }

        self.DeleteResource = function (data) {

            //data.TaskStatus('PNDG');
            //data.ResourceID(0);
            //data.IsChanged(true);
            //data.ResourceName('UnPlanned');
            ////self.selectedSlotColumn().ResourceName("New Resource");
            //$(".close").trigger("click");
            ////self.IsChanged(true);
            if (!(data.TaskStatus() == 'VERF' || data.TaskStatus() == 'COMP' || data.TaskStatus() == 'STRD')) {
                data.TaskStatus('PNDG');
                data.ResourceID(0);
                data.ResourceName('Unscheduled');
                //data.IsChanged(true);
                //data.TugResourceName('UnPlanned');
                self.isChange(true);
                //self.selectedSlotColumn().ResourceName("New Resource");

            }
            else {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Resource cannot be deleted.", "Resource Allocation");
            }

        }

        self.LoadUsers = function (data) {

            var ResourceSlotVO = new IPMSROOT.ResourceSlot(data);
            self.viewModelHelper.apiGet('api/GetSearchResource/' + data.ServiceTypeCode() + "/" + data.ServiceReferenceID() + "/" + data.SlotNumber() + "/" + moment(data.AllocationDate()).format('YYYY-MM-DD'), null,
            //self.viewModelHelper.apiGet('api/GetSearchResource/' + data.ServiceTypeCode() + data.ServiceReferenceID() + data.SlotNumber(), null, //ko.toJSON(data)

                     function (result) {
                         ko.mapping.fromJS(result, {}, self.getResourceList);
                     }, null, null, false);

            //var obj = JSON.parse(ko.toJSON(self.suppServiceResourceAllocList));

            //$.each(obj, function (key, value) {

            //    //$.each(value.ResourceAllocationSlots, function (key, value1) {
            //    var avblusers = ko.utils.arrayFilter(value.ResourceSlots, function (item) {
            //        return (item.ResourceID != null && item.ResourceID != data.ResourceID());
            //    })[0];

            //    if (avblusers != undefined) {

            //        if (avblusers.ResourceID != 0) {

            //            var users = ko.utils.arrayFilter(self.getResourceList(), function (item) {
            //                return item.ID() == avblusers.ResourceID;
            //            })[0];

            //            self.getResourceList.remove(users);
            //        }
            //    }
            //    //});

            //});


            if (data.TaskStatus() != "COMP" || data.TaskStatus() != "VERF") {

                self.checkUserList = ko.observableArray();
                var obj = JSON.parse(ko.toJSON(self.suppServiceResourceAllocList));

                $.each(obj, function (key, value) {
                    var avblusers = ko.utils.arrayFilter(value.ResourceSlots, function (item) {
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

        self.LoadVCN = function () {

            //self.viewModelHelper.apiGet('api/ServiceRequest/GetVCNDetails', null,
            self.viewModelHelper.apiGet('api/GetResourceAllocationVCNDetails/{date}', { date: moment(self.CurrentDate()).format('YYYY-MM-DD hh:mm:ss A') },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.VCNList);
                    //console.log(ko.toJSON(self.VCNList));
                }, null, null, false);
        }

        //self.calMaxToday = function () {

        //    var date = new Date();
        //    date.setDate(date.getDate() + 1);
        //    this.max(date);
        //}

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.LoadPreviousDay = function () {
            $('.nav-tabs a[href="#tab_0"]').tab('show');
            $("#ResourceShiftTypeForRC").prop('selectedIndex', 0);
            $("#VCNDetails").prop('selectedIndex', 0);
            self.ResourceCalendarSlotsCount.removeAll();
            self.movementResourceCalendarModel([]);
            var dt = new Date();
            dt.setDate(dt.getDate() + 1);
            var currentDate = self.CurrentDate();

            //if (currentDate > dt) {
            //    self.IsValidDate(false);
            //}
            //else {
            //    self.IsValidDate(true);
            //}

            currentDate.setDate(currentDate.getDate() - 1);
            if ((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) {
                self.IsCurrentDate(true);
            }
            else {
                self.IsCurrentDate(false);
            }

            self.CurrentDate(currentDate);
            $('.displaytxtResourceAllocation').text(moment(currentDate).format('MMM DD, YYYY'));

            self.LoadVCN();
            self.LoadSlotsCount();
            self.LoadSuppServiceResourceAlloc();
        }

        self.LoadNextDay = function () {
            $('.nav-tabs a[href="#tab_0"]').tab('show');
            $("#ResourceShiftTypeForRC").prop('selectedIndex', 0);
            $("#VCNDetails").prop('selectedIndex', 0);
            self.ResourceCalendarSlotsCount.removeAll();
            self.movementResourceCalendarModel([]);
            var dt = new Date();
            var dt1 = new Date();
            var varcurrentDate = self.CurrentDate();
            var futureDate = self.CurrentDate();
            dt1.setDate(dt1.getDate() - 1);

            //if (futureDate > dt1) {
            //    self.IsValidDate(false);
            //}
            //else {

            //    self.IsValidDate(true);
            //}
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
            self.LoadVCN();
            self.LoadSlotsCount();
            self.LoadSuppServiceResourceAlloc();
            //$("#ResourceShiftTypeForRC").prop('selectedIndex', 0);
            //$("#VCNDetails").prop('selectedIndex', 0);

        }

        self.getResourceByDate = function (data) {
            var date = data.CurrentDate();
            self.CurrentDate(date);
            $('.nav-tabs a[href="#tab_0"]').tab('show');
            $("#ResourceShiftTypeForRC").prop('selectedIndex', 0);
            $("#VCNDetails").prop('selectedIndex', 0);
            self.ResourceCalendarSlotsCount.removeAll();
            self.movementResourceCalendarModel([]);

            var dt = new Date();
            dt.setDate(dt.getDate() + 1);
            var currentDate = self.CurrentDate();

            //if (currentDate > dt) {
            //    self.IsValidDate(false);
            //}
            //else {
            //    self.IsValidDate(true);
            //}

            currentDate.setDate(currentDate.getDate());
            if ((dt.getDate() == currentDate.getDate()) && (dt.getMonth() == currentDate.getMonth()) && (dt.getFullYear() == currentDate.getFullYear())) {
                self.IsCurrentDate(true);
            }
            else {
                self.IsCurrentDate(false);
            }

            self.CurrentDate(currentDate);
            $('.displaytxtResourceAllocation').text(moment(currentDate).format('MMM DD, YYYY'));

            self.LoadVCN();
            self.LoadSlotsCount();
            self.LoadSuppServiceResourceAlloc();
        }

        self.clickRefresh = function () {
            location.reload();
            //window.location.reload();
        }

        self.saveResourceAllocation = function (data) {
            debugger;
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                toastr.options.timeOut = 3000;
                toastr.warning("Can not save future date vessel.", "Automated Slotting");
                return false;
            }

            else if (moment(self.Date()).format('YYYY-MM-DD') == moment(self.CurrentDate()).format('YYYY-MM-DD')) {

                var count = 0;
                var taskStatus = null;
                var number = 0;
                var slotperiod = null;

                ko.utils.arrayMap(data.ResourceSlots(), function (res) {
                    if (res.TaskStatus() != null) {
                        taskStatus = res.TaskStatus();
                        number = res.SlotNumber();
                        if (res.TaskStatus() == "SCHD" || res.TaskStatus() == "OVRD" || (res.TaskStatus() == "PNDG" && self.isChange() == true)) {
                            count = count + 1;
                            return false;
                        }
                    }
                });

                $.each(self.SlotsCount(), function (index, value) {
                    if (value.SlotNumber() == number) {
                        slotperiod = value.SlotPeriod();
                        return false;
                    }
                });

                self.GetActiveSlots();
                var status = false;

                $.each(self.activeSlots(), function (index, value) {
                    if (value == slotperiod) {
                        status = true;
                        return false;
                    }
                });

                if (status) {
                    if (count > 0) {
                        self.viewModelHelper.apiPut('api/UpdateResourceAllocation', ko.toJSON(data),
                            function Message(result) {
                                toastr.success("Resource allocation saved successfully.", "Resource Allocation");
                                //self.IsChanged(false);
                                self.LoadSuppServiceResourceAlloc();
                                self.isChange(false);
                            });
                    }
                    else {
                        var toasterMessage = GetSaveTaskMessage(taskStatus);
                        toastr.warning(toasterMessage, "Resource Allocation");
                    }
                }
                else {
                    toastr.options.timeOut = 3000;
                    toastr.warning("Can not save previous slot vessel.", "Automated Slotting");
                    return false;
                }
            }

            else {
                toastr.options.timeOut = 3000;
                toastr.warning("Can not save previous date vessel.", "Automated Slotting");
                return false;
            }
        }

        self.confirmResourceAllocation = function (data) {
            debugger;
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if (moment(self.Date()).format('YYYY-MM-DD') < moment(self.CurrentDate()).format('YYYY-MM-DD')) {
                toastr.options.timeOut = 3000;
                toastr.warning("Can not confirm future date vessel.", "Automated Slotting");
                return false;
            }

            else if (moment(self.Date()).format('YYYY-MM-DD') == moment(self.CurrentDate()).format('YYYY-MM-DD')) {

                var count = 0;
                var taskStatus = null;
                var number = null;
                var slotperiod = null;
                var resourceID = 0;

                ko.utils.arrayMap(data.ResourceSlots(), function (res) {
                    if (res.TaskStatus() != null) {
                        taskStatus = res.TaskStatus();
                        number = res.SlotNumber();
                        resourceID = res.ResourceID();
                        if (res.TaskStatus() == "SCHD" || res.TaskStatus() == "OVRD" || (res.TaskStatus() == "PNDG" && self.isChange() == true)) {
                            count = count + 1;
                            return false;
                        }
                    }
                });

                $.each(self.SlotsCount(), function (index, value) {
                    if (value.SlotNumber() == number) {
                        slotperiod = value.SlotPeriod();
                        return false;
                    }
                });

                self.GetActiveSlots();
                var status = false;

                $.each(self.activeSlots(), function (index, value) {
                    if (value == slotperiod) {
                        status = true;
                        return false;
                    }
                });

                if (status) {
                    if (count > 0) {
                        if (resourceID > 0) {
                            data.IsConfirm("true");
                            self.viewModelHelper.apiPut('api/UpdateResourceAllocation', ko.toJSON(data),
                                function Message(result) {
                                    toastr.success("Resource allocation confirmed successfully.", "Resource Allocation");
                                    //self.IsChanged(false);
                                    self.LoadSuppServiceResourceAlloc();
                                    self.isChange(false);
                                });
                        }
                        else {
                            toastr.warning("Resource not available.", "Resource Allocation");
                        }
                    }
                    else {
                        var toasterMessage = GetConfirmTaskMessage(taskStatus);
                        toastr.warning(toasterMessage, "Resource Allocation");
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

        self.GetActiveSlots = function () {
            self.activeSlots.removeAll();
            self.viewModelHelper.apiGet('api/GetActiveSlots', null,
                function (result) {
                    ko.mapping.fromJS(result, {}, self.activeSlots);
                }, null, null, false);
        }

        self.GetActiveResourceSlots = function () {
            self.activeResourceSlots.removeAll();
            self.viewModelHelper.apiGet('api/GetActiveResourceSlot', null,
                function (result) {
                    ko.mapping.fromJS(result, {}, self.activeResourceSlots);
                }, null, null, false);
        }

        self.Initialize();
    }

    IPMSRoot.SuppServiceResourceAllocViewModel = SuppServiceResourceAllocViewModel;
}(window.IPMSROOT));



