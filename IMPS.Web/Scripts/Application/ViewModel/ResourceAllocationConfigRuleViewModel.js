(function (IPMSRoot) {
    var isView = 0;
    var ResourceAllocationConfigRuleViewModel = function () {
        var self = this;
        $('#spnTitle').html("Resource Allocation Rule");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.resourceallocationconfigruleModel = ko.observable(new IPMSROOT.ResourceAllocationConfigRuleModel());
        self.viewMode = ko.observable(true);
        self.resourceallocationconfigruleList = ko.observableArray([]);
        self.referenceData = ko.observable();
        self.isview = ko.observable(true);
        self.isSave = ko.observable(true);
        self.isUpdate = ko.observable(true);

        self.Initialize = function () {
            self.LoadResourceAllocationConfigRuleData();
            self.LoadInitialData();
            self.viewMode('List');
        }

        Validation = function () {
            var NoOfErrors = 0;
            $('#spanPilotCapacity').text('');

            if ($("#PilotCapacity").val() == "" || $("#PilotCapacity").val() == null) {
                $('#spanPilotCapacity').text('* Please select Pilot Capacity.');
                NoOfErrors++;
            }
            return NoOfErrors;
        }

        //Designation Change
        ChangePilotCapacity = function () {

            var NoOfErrors = 0;
            if ($("#PilotCapacity").val() == "" || $("#PilotCapacity").val() == null) {
                $('#spanPilotCapacity').text('* Please select Pilot Capacity.');
                NoOfErrors++;
            }
            else {
                $("#spanPilotCapacity").text('');
            }
        }

        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/ResourceAllocationConfigRuleReferences', null, function (result1) {
                self.referenceData(new IPMSRoot.ReferenceData(result1));
            }, null, null, false);
        }

        self.LoadResourceAllocationConfigRuleData = function () {
            self.viewModelHelper.apiGet('api/ResourceallocationConfigruleList', null, function (result) {
                self.resourceallocationconfigruleList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.ResourceAllocationConfigRuleModel(item);
                    self.resourceallocationconfigruleModel(new IPMSROOT.ResourceAllocationConfigRuleModel(undefined));
                }));
            }, null, null, false);
        }

        // Add button
        self.addResourceAllocationConfigRule = function (data) {
            self.resourceallocationconfigruleModel(new IPMSROOT.ResourceAllocationConfigRuleModel(undefined));
            self.isview(true);
            self.isSave(true);
            self.isUpdate(false);
            $('#spnTitle').html("Add Resource Allocation Rule");
            self.viewMode('Form');
            self.isview(true);
        }

        //Edit Button
        self.EditResourceAllocationConfigRule = function (data) {
            self.resourceallocationconfigruleModel(data);
            $('#spnTitle').html("View Resource Allocation Rule");
            $("#EffectiveFrm").data('kendoDatePicker').enable(false);
            $("#ArrivalMultiSelect").data('kendoMultiSelect').enable(true);
            $("#shiftingservicetype").data('kendoMultiSelect').enable(true);
            $("#warpingservicetype").data('kendoMultiSelect').enable(true);
            $("#sailingservicetype").data('kendoMultiSelect').enable(true);
            $('#TotalTugs').prop("disabled", false);
            $('#PilotCapacity').prop("disabled", false);
            $('#FromMeter').prop("disabled", false);
            $('#ToMeter').prop("disabled", false);
            $('#NoOfGangs').prop("disabled", false);
            self.isview(true);
            self.isSave(false);
            self.isUpdate(true);
            $('#spnTitle').html("Update Resource Allocation Rule");
            self.viewMode('Form');
        }

        // View Button
        self.viewResourceAllocationConfigRule = function (data) {
            self.resourceallocationconfigruleModel(data);
            $('#spnTitle').html("View Resource Allocation Rule");
            $("#EffectiveFrm").data('kendoDatePicker').enable(false);
            $("#ArrivalMultiSelect").data('kendoMultiSelect').enable(false);
            $("#shiftingservicetype").data('kendoMultiSelect').enable(false);
            $("#warpingservicetype").data('kendoMultiSelect').enable(false);
            $("#sailingservicetype").data('kendoMultiSelect').enable(false);
            $('#TotalTugs').prop("disabled", true);
            $('#PilotCapacity').prop("disabled", true);
            $('#FromMeter').prop("disabled", true);
            $('#ToMeter').prop("disabled", true);
            $('#NoOfGangs').prop("disabled", true);
            $('#Savebutton').prop("visible", false);
            $('#Resetbutton').prop("visible", false);
            self.isview(false);
            self.isSave(false);
            self.isUpdate(false);
            self.viewMode('Form');
        }

        // Table Add button
        self.AddNewRowtotable = function (data) {
            self.resourceallocationconfigruleModel().ResourceGangConfigsVO.push(new IPMSROOT.ResourceGangConfigsDetails());
        }

        // Update
        self.UpdateResourceAllocationConfigRule = function (data) {

            self.resourceallocationconfigrulevalidation = ko.observable(data);
            self.resourceallocationconfigrulevalidation().errors = ko.validation.group(self.resourceallocationconfigrulevalidation());
            var errors = self.resourceallocationconfigrulevalidation().errors().length;

            self.ResourceGangConfigsValidation = ko.observable(data.ResourceGangConfigsVO());
            self.ResourceGangConfigsValidation().errors = ko.validation.group(self.ResourceGangConfigsValidation());
            var ResourceGangConfigserrors = self.ResourceGangConfigsValidation().errors().length;

            self.arrivalservicetypeValidation = ko.observable(data.arrivalservicetype());
            self.arrivalservicetypeValidation().errors = ko.validation.group(self.arrivalservicetypeValidation());
            var arrivalservicetypeerrors = self.arrivalservicetypeValidation().errors().length;

            self.sailingservicetypeValidation = ko.observable(data.sailingservicetype());
            self.sailingservicetypeValidation().errors = ko.validation.group(self.sailingservicetypeValidation());
            var sailingservicetypeerrors = self.sailingservicetypeValidation().errors().length;

            self.shiftingservicetypeValidation = ko.observable(data.shiftingservicetype());
            self.shiftingservicetypeValidation().errors = ko.validation.group(self.shiftingservicetypeValidation());
            var shiftingservicetypeerrors = self.shiftingservicetypeValidation().errors().length;

            self.warpingservicetypeValidation = ko.observable(data.warpingservicetype());
            self.warpingservicetypeValidation().errors = ko.validation.group(self.warpingservicetypeValidation());
            var warpingservicetypeerrors = self.warpingservicetypeValidation().errors().length;
            var errors1 = 0;
            errors1 = Validation();
            if (errors1 != 0) {
                self.resourceallocationconfigrulevalidation().errors.showAllMessages(true);
                return;
            }
            if (errors == 0 && ResourceGangConfigserrors == 0 && arrivalservicetypeerrors == 0 && sailingservicetypeerrors == 0 && shiftingservicetypeerrors == 0 && warpingservicetypeerrors == 0) {

                if (data.ResourceAllocationConfigRuleID() != 0) {
                    self.viewModelHelper.apiPut('api/ResourceAllocationConfigRule', ko.mapping.toJSON(data), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Resource allocation rule details updated successfully.", "Resource Allocation Configuration Rules");
                        self.LoadResourceAllocationConfigRuleData();
                        self.viewMode('List');
                        $('#spnTitle').html("Resource Allocation Rule");
                    });
                }
                else {
                    self.viewModelHelper.apiPost('api/ResourceAllocationConfigRule', ko.mapping.toJSON(data), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Resource allocation rule details saved successfully.", "Resource Allocation Configuration Rules");
                        self.LoadResourceAllocationConfigRuleData();
                        self.viewMode('List');
                        $('#spnTitle').html("Resource Allocation Rule");
                    });
                }
            }
            else {
                self.resourceallocationconfigrulevalidation().errors.showAllMessages(true);
                return;
            }
        }

        self.cancle = function (data) {
            self.viewMode('List');
            self.resourceallocationconfigruleModel().reset();
            $('#spnTitle').html("Resource Allocation Rule");
        }

        self.reset = function (data) {
            self.resourceallocationconfigruleModel().reset();
            self.viewMode('Form');
        }

        // Display Minimum date in DateTime picker as today date
        calmintoday = function () {
            var date = new Date();
            date.setDate(date.getDate() + 1);
            this.min(date);
        };

        //Action  : Button Delete
        //Purpose : Dynamically Remove the row from the ResourceGangConfig List
        RemoveResourceGangConfig = function (data) {
            self.resourceallocationconfigruleModel().ResourceGangConfigsVO.remove(data);
        };

        self.Initialize();
    }

    IPMSRoot.ResourceAllocationConfigRuleViewModel = ResourceAllocationConfigRuleViewModel;
}(window.IPMSROOT));