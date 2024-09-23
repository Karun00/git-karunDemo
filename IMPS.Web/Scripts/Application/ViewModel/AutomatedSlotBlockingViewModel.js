(function (IPMSRoot) {

    var AutomatedSlotBlockingViewModel = function () {

        var self = this;
        $('#spnTitile').html("Automated Slot Blocking");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.autoslotBlockingModel = ko.observable();
        self.slottingReferenceData = ko.observable();
        self.AutoBlockingList = ko.observableArray();

        self.IsPrivelegeEnable = ko.observable(true);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.IsStatusEnable = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadAutoBlockSlots();
            self.LoadReferenceData();
            self.IsCodeEnable(true);
            self.viewMode('List');
            $("#OtherID").hide();
        }


        self.LoadAutoBlockSlots = function () {
            self.viewModelHelper.apiGet('api/AutomatedSlotBlocking',
            null,
              function (result) {

                  self.AutoBlockingList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.AutomatedSlotBlockingModel(item);
                  }));
              });
        }

        self.LoadReferenceData = function () {
            self.viewModelHelper.apiGet('api/GetAutomatedReferenceData', null,
                  function (result1) {
                      self.slottingReferenceData(new IPMSRoot.AutomatedReferenceData(result1));
                  }, null, null, false);
        }      

        calmintoday = function () {
            this.min(new Date());
        };


        SlotBlockingDate = function (model) {
            self.CurrentDate = ko.observable();
            self.CurrentDate(new Date());

            var allSlots = self.slottingReferenceData().SlotTypes();

            var fromDate = self.autoslotBlockingModel().FromDate();
            fromDate.setHours(0);
            fromDate.setMinutes(0);

            if (fromDate <= self.CurrentDate()) {

                self.slottingReferenceData().AllSlots.removeAll();                
                self.autoslotBlockingModel().SlotFrom("");
                self.autoslotBlockingModel().SlotTo("");

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();

                    if (item.StartTime() <= totalminutes && totalminutes <= item.EndTime()) {
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        AddSlot.SlotTime(item.SlotPeriod().substring(0, 5));
                        self.slottingReferenceData().AllSlots.push(AddSlot);                        
                    }
                   
                    if (totalminutes < item.StartTime()) {
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        AddSlot.SlotTime(item.SlotPeriod().substring(0, 5));
                        self.slottingReferenceData().AllSlots.push(AddSlot);
                      
                    }

                });            
            }
            else {
                self.slottingReferenceData().AllSlots.removeAll();
                self.autoslotBlockingModel().SlotFrom("");
                self.autoslotBlockingModel().SlotTo("");

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        AddSlot.SlotTime(item.SlotPeriod().substring(0, 5));
                        self.slottingReferenceData().AllSlots.push(AddSlot);
                });               
            }
        }


        self.SlotFromChange = function (e) {
            var dataItem = this.dataItem(e.item.index());            
            if (dataItem.StartTime != undefined) {
                self.autoslotBlockingModel().StartTime(dataItem.StartTime);              
            }
            self.autoslotBlockingModel().SlotTo("");
            self.autoslotBlockingModel().ToDate("");
        }


        self.SlotToChange = function (e) {
            var dataItem = this.dataItem(e.item.index());            
            if (dataItem.StartTime != undefined) {
                self.autoslotBlockingModel().ToStartTime(dataItem.StartTime);                               
            }          
        }

        self.ReasonChange = function (model) { 
            if (model.Reason() == "ROTR") {
                $("#OtherID").show();
                model.Other("");

            }
            else {
                $("#OtherID").hide();
                model.Other("");
            }
        }        


        self.AddSlot = function () {
            self.autoslotBlockingModel(new IPMSROOT.AutomatedSlotBlockingModel());
            ko.validation.group(self.autoslotBlockingModel()).showAllMessages(false);
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsPrivelegeEnable(true);
            self.IsCodeEnable(true);
            self.IsStatusEnable(false);
            $("#OtherID").hide();

            $('#spnTitile').html("Add Automated Slot Blocking");
        }

        self.EditSlotBlocking = function(model) {
            self.CurrentDate = ko.observable();
            self.CurrentDate(new Date());

            var allSlots = self.slottingReferenceData().SlotTypes();

            var fromDate = new Date(model.FromDate());
            fromDate.setHours(0);
            fromDate.setMinutes(0);

            if (fromDate <= self.CurrentDate()) {
                self.slottingReferenceData().AllSlots.removeAll();

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();

                    if (item.StartTime() <= totalminutes && totalminutes <= item.EndTime()) {
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        AddSlot.SlotTime(item.SlotPeriod().substring(0, 5));
                        self.slottingReferenceData().AllSlots.push(AddSlot);
                    }

                    if (totalminutes < item.StartTime()) {
                        AddSlot.SlotPeriod(item.SlotPeriod());
                        AddSlot.StartTime(item.StartTime());
                        AddSlot.EndTime(item.EndTime());
                        AddSlot.SlotTime(item.SlotPeriod().substring(0, 5));
                        self.slottingReferenceData().AllSlots.push(AddSlot);
                    }
                });
            }
            else {
                self.slottingReferenceData().AllSlots.removeAll();

                $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();
                    AddSlot.SlotPeriod(item.SlotPeriod());
                    AddSlot.StartTime(item.StartTime());
                    AddSlot.EndTime(item.EndTime());
                    AddSlot.SlotTime(item.SlotPeriod().substring(0, 5));
                    self.slottingReferenceData().AllSlots.push(AddSlot);
                });
            }
        }

        self.editSlot = function (model) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsPrivelegeEnable(true);
            self.IsCodeEnable(true);
            self.IsStatusEnable(true);
            self.EditSlotBlocking(model);         

            self.autoslotBlockingModel(model);
            if (model.Reason() == "ROTR") {
                $("#OtherID").show();
            }
            else {
                $("#OtherID").hide();
            }
            
            $('#spnTitile').html("Update Automated Slot Blocking");

          

        }

        self.viewSlot = function (model) {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsPrivelegeEnable(false);
            self.IsCodeEnable(false);
            self.IsStatusEnable(false);            
            self.CurrentDate = ko.observable();
            self.CurrentDate(new Date());

            var allSlots = self.slottingReferenceData().SlotTypes();

            self.slottingReferenceData().AllSlots.removeAll();

            $.each(allSlots, function (key, item) {

                    var totalminutes = self.CurrentDate().getHours() * 60 + self.CurrentDate().getMinutes();
                    var AddSlot = new IPMSROOT.SlotType();
                    AddSlot.SlotPeriod(item.SlotPeriod());
                    AddSlot.StartTime(item.StartTime());
                    AddSlot.EndTime(item.EndTime());
                    AddSlot.SlotTime(item.SlotPeriod().substring(0, 5));
                    self.slottingReferenceData().AllSlots.push(AddSlot);
            });

            self.autoslotBlockingModel(model);
            if (model.Reason() == "ROTR") {
                $("#OtherID").show();            
            }
            else {
                $("#OtherID").hide();                
            }
        
            $('#spnTitile').html("View Automated Slot Blocking");
        }

        self.ResetAutoSlot = function (model) {                            
            ko.validation.reset();
            self.autoslotBlockingModel().reset();
            if (self.IsSave()) {
                self.autoslotBlockingModel().FromDate("");
                self.autoslotBlockingModel().ToDate("");
            }
            if (self.IsUpdate()) {
                self.autoslotBlockingModel(model);
            }

            if (model.Reason() == "ROTR") {
                $("#OtherID").show();
            }
            else {
                $("#OtherID").hide();
            }
        }

        self.Cancel = function () {
            self.viewMode('List');
            $('#spnTitile').html("Automated Slot Blocking");
            self.autoslotBlockingModel().reset();

        }

        self.SaveAutoSlot = function (model) {
            model.validationEnabled(true);
            self.AutoSlotValidation = ko.observable(model);
            self.AutoSlotValidation().errors = ko.validation.group(self.AutoSlotValidation());
            var errors = self.AutoSlotValidation().errors().length;
            var SlotValidate = true;

            
                if (model.Reason() == "ROTR") {
                    if (model.Other() == "") {
                        $("#spanOther").text('This field is required.');
                        errors = errors + 1;
                    }
                }
            

            if (errors == 0) {                
                if (self.autoslotBlockingModel().StartTime() > self.autoslotBlockingModel().ToStartTime()) {
                    self.autoslotBlockingModel().SlotTo('');
                    self.autoslotBlockingModel().ToStartTime('');
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select Slot To greater than Slot From", "Automated Slot Blocking");
                    SlotValidate = false;
                }               
            }           

            if (SlotValidate == true) {
                if (errors == 0) {

                    var fromDate = new Date(self.autoslotBlockingModel().FromDate());
                    if (fromDate != "" && self.autoslotBlockingModel().StartTime() != "") {
                        fromDate.setHours(0);
                        fromDate.setMinutes(0);
                        fromDate.setMinutes(self.autoslotBlockingModel().StartTime() + 1);
                        self.autoslotBlockingModel().FromDate(fromDate);
                    }

                    var toDate = new Date(self.autoslotBlockingModel().FromDate());
                    if (toDate != "" && self.autoslotBlockingModel().ToStartTime() != "") {
                        toDate.setHours(0);
                        toDate.setMinutes(0);
                        toDate.setMinutes(self.autoslotBlockingModel().ToStartTime() + 1);
                        self.autoslotBlockingModel().ToDate(toDate);
                    }

                    self.viewModelHelper.apiPost('api/AutomatedSlotBlocking', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Automated Slot Blocking details saved successfully", "Automated Slot Blocking");
                        self.LoadAutoBlockSlots();
                        $('#spnTitile').html("Automated Slot Blocking");
                        self.viewMode('List');

                    });
                }
                else {
                    self.AutoSlotValidation().errors.showAllMessages();
                    toastr.warning("You have some errors. Please check below");
                    return;
                }
            }



        }

        self.ModifyAutoSlot = function (model) {
            self.AutoSlotValidation = ko.observable(model);
            self.AutoSlotValidation().errors = ko.validation.group(self.AutoSlotValidation());
            var errors = self.AutoSlotValidation().errors().length;
            var SlotValidate = true;

            if (errors == 0) {
                if (model.Reason() == "ROTR") {
                    if (model.Other() == "") {
                        $("#spanOther").text('This field is required.');
                        errors = errors + 1;
                    }
                }
            }

            if (errors == 0) {                
                if (self.autoslotBlockingModel().StartTime() > self.autoslotBlockingModel().ToStartTime()) {
                    self.autoslotBlockingModel().SlotTo('');
                    self.autoslotBlockingModel().ToStartTime('');
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Please Select Slot To greater than Slot From", "Automated Slot Blocking");
                    SlotValidate = false;
                }               
            }

            if (SlotValidate == true) {
                if (errors == 0) {


                    var fromDate = new Date(self.autoslotBlockingModel().FromDate());
                    if (fromDate != "" && self.autoslotBlockingModel().StartTime() != "") {
                        fromDate.setHours(0);
                        fromDate.setMinutes(0);
                        fromDate.setMinutes(self.autoslotBlockingModel().StartTime() + 1);
                        self.autoslotBlockingModel().FromDate(fromDate);
                    }

                    var toDate = new Date(self.autoslotBlockingModel().FromDate());
                    if (toDate != "" && self.autoslotBlockingModel().ToStartTime() != "") {
                        toDate.setHours(0);
                        toDate.setMinutes(0);
                        toDate.setMinutes(self.autoslotBlockingModel().ToStartTime() + 1);
                        self.autoslotBlockingModel().ToDate(toDate);
                    }

                    self.viewModelHelper.apiPut('api/AutomatedSlotBlocking', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Automated Slot Blocking details updated successfully", "Automated Slot Blocking");
                        self.LoadAutoBlockSlots();
                        $('#spnTitile').html("Automated Slot Blocking");
                        self.viewMode('List');

                    });
                }
                else {
                    self.AutoSlotValidation().errors.showAllMessages();
                    toastr.warning("You have some errors. Please check below");
                    return;
                }
            }

        }

        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }



        self.Initialize();
    }
    IPMSRoot.AutomatedSlotBlockingViewModel = AutomatedSlotBlockingViewModel;

}(window.IPMSROOT));


function SlotType(SlotPeriod, StartTime, EndTime, SlotTime) {
    this.SlotPeriod = SlotPeriod;
    this.StartTime = StartTime;
    this.EndTime = EndTime;
    this.SlotTime = SlotTime;

}