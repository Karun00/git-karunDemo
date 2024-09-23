(function (IPMSRoot) {
    var isView = 0;
    var AutomatedSlotConfigurationViewmodel = function () {
        var self = this;
        $('#spnTitle').html("Automated Slot Configuration");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.automatedslotconfigModel = ko.observable(new IPMSROOT.AutomatedSlotConfigurationModel());
        self.viewMode = ko.observable();
        self.automatedslotconfigReferenceData = ko.observable();
        self.automatedslotconfigList = ko.observableArray([]);
        self.viewMode = ko.observable();
        self.automatedslotconfigmasterModel = ko.observable();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);           
        self.IsAdd = ko.observable(false);


        // Initialize method is fires in  pageload Initializetion mode
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadInitialData();
            self.automatedslotconfigmasterModel(new IPMSROOT.AutomatedSlotConfigurationModel());
            self.LoadAutomatedSlotConfigMaster();
            self.viewMode('List');
        }


        self.LoadAutomatedSlotConfigMaster = function () {

            // return new IPMSRoot.CraftModel(item);
            self.viewModelHelper.apiGet('api/AutomatedSlotConfiguration/GetAutomatedSlotconfigList', null,
                        function (result) {
                            self.automatedslotconfigList(ko.utils.arrayMap(result, function (item) {
                                return new IPMSRoot.AutomatedSlotConfigurationModel(item);
                                self.automatedslotconfigmasterModel(new IPMSROOT.AutomatedSlotConfigurationModel(undefined));
                            }));
                        });
        }

        self.AddSlotPriorityConfig = function (Data) {
            
            self.automatedslotconfigmasterModel().SlotPriorityConfigurations.push(new IPMSROOT.SlotPriorityConfiguration());
        }

        //LoadInitialData fetches the data from Api service
        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/AutomatedSlotConfigReferenceData', null,
                    function (result1) {
                        self.automatedslotconfigReferenceData(new IPMSRoot.AutomatedSlotConfigReferenceData(result1));
                    }, null, null, false);
        }

        self.OperationalChange = function (model) {            
            var dateobj = kendo.parseDate(model.OperationalPeriod1(), "yyyy-MM-dd HH:mm");
            var datestring = kendo.toString(dateobj, "yyyy-MM-dd HH:mm");
            model.OperationalTime(datestring);
            //self.OperationalDate(datestring);
        }


        self.SaveAutomatedSlotConfiguration = function (AutomatedSlotConfigData) {
            self.AutomatedSlotConfigurationValidation = ko.observable(AutomatedSlotConfigData);
            self.AutomatedSlotConfigurationValidation().errors = ko.validation.group(self.AutomatedSlotConfigurationValidation());
            var errors = self.AutomatedSlotConfigurationValidation().errors().length;
            var record = true;

            if (errors == 0) {                
                if (self.automatedslotconfigmasterModel().SlotPriorityConfigurations().length != 0) {
                        var ManError = "Y";
                        $.each(self.automatedslotconfigmasterModel().SlotPriorityConfigurations(), function (index, item) {                            
                            if (item != null)
                                if (item !== undefined) {
                                    if (item.vesseltype() == "" || item.vesseltype() == undefined || item.Priority() == undefined || item.Priority() == "" || item.NoofVessels() == undefined || item.NoofVessels() == "") {
                                        toastr.options.closeButton = true;
                                        toastr.options.positionClass = "toast-top-right";
                                        toastr.warning("Please enter All Fields of Slot Priority Configuration Details", "Automated Slot Configuration");
                                        record = false;
                                        return false;
                                    }
                                }
                        });
                    }                
            }



            if (errors == 0 && record == true) {

                //var dateobj = kendo.parseDate(AutomatedSlotConfigData.OperationalPeriod1(), "yyyy-MM-dd HH:mm");
                //var datestring = kendo.toString(dateobj, "yyyy-MM-dd HH:mm");
                AutomatedSlotConfigData.OperationalPeriod1(AutomatedSlotConfigData.OperationalTime());

                if (AutomatedSlotConfigData.SlotCofiguratinid() == 0) {
                self.viewModelHelper.apiPost('api/AutomatedSlotConfigaretions', ko.mapping.toJSON(AutomatedSlotConfigData), function Message(data) {

                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Automated slot configuration details saved successfully.", "Automated Slot Configuration");
                    self.LoadAutomatedSlotConfigMaster();
                    self.viewMode('List');
                    $('#spnTitle').html("Automated Slot Configuration");
                });
            }
            else {

                self.viewModelHelper.apiPut('api/AutomatedSlotConfigaretions', ko.mapping.toJSON(AutomatedSlotConfigData), function Message(data) {

                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Automated slot configuration details updated successfully.", "Automated Slot Configuration");
                    self.LoadAutomatedSlotConfigMaster();
                    self.viewMode('List');
                    $('#spnTitle').html("Automated Slot Configuration");
                });
            }
        }
            else
            {
                self.AutomatedSlotConfigurationValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }
        
        // This is used to Add new Automated Slot Configuration
        self.addautomatedslotconfig = function () {
            self.automatedslotconfigmasterModel(new IPMSROOT.AutomatedSlotConfigurationModel(undefined));
            self.viewMode('Form');
            self.IsSave(true);
            self.IsUpdate(false);
            self.IsCodeEnable(true);
            self.IsReset(true);
     
            self.IsAdd(true);
            $('#spnTitle').html("Add Automated Slot Configuration");
        }

        // This is used to Edit Automated Slot Configuration
        self.Editautomatedslotconfig = function (data) {
            self.automatedslotconfigmasterModel(data);            
            self.viewMode('Form');    
            self.IsSave(false);
            self.IsUpdate(true);
            self.IsCodeEnable(true);
            self.IsReset(true);
          
            self.IsAdd(true);
            $("#EffectiveFrm").data('kendoDatePicker').enable(false);
            $('#spnTitle').html("Update Automated Slot Configuration");
        }

        // This is used to View Automated Slot Configuration
        self.viewautomatedslotconfig = function (data) {
            $('#spnTitle').html("View Automated Slot Configuration");
            self.automatedslotconfigmasterModel(data);
            self.viewMode('Form');
            self.IsSave(false);
            self.IsUpdate(false);
            self.IsReset(false);
            self.IsCodeEnable(false);
            self.IsAdd(false);
            $("#EffectiveFrm").data('kendoDatePicker').enable(false);
            $("#OperationalPeriod").data('kendoTimePicker').enable(false);
        }

        // This is used to Cancel Automated Slotting Configuration
        self.cancelautomatedslotconfig = function () {
            self.automatedslotconfigmasterModel().reset();
            self.viewMode('List');
            $('#spnTitle').html("Automated Slot Configuration");

        }

        // This is used to Reset Automated Slotting Configuration
        self.Resetautomatedslotconfig = function () {
            ko.validation.reset();
            self.automatedslotconfigmasterModel().reset();
        }

        //This is used to Validate numeric
        ValidateNumeric = function () {
            return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
        };

        //Validate Alphabets With Spaces
        ValidateAlphabetsWithSpaces = function () {
            return self.validationHelper.ValidateAlphabetsWithSpaces_keypressEvent(this, event);
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
        // Display Minimum date in DateTime picker as today date
        calmintoday = function () {
              var date = new Date();
    
            date.setDate(date.getDate() +1);
            this.min(date);

            //var d = new Date();
            //d.setDate(d.getDate() + 1);
            //this.min(d);
        };

        RemoveVesselTypeConfig = function (data) {
            self.automatedslotconfigmasterModel().SlotPriorityConfigurations.remove(data);
        };

        self.Initialize();
    }
    IPMSRoot.AutomatedSlotConfigurationViewmodel = AutomatedSlotConfigurationViewmodel;
}(window.IPMSROOT));