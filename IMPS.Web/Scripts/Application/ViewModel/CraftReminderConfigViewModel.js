(function (IPMSRoot) {

    var isView = 0;
    var CraftReminderConfigViewModel = function (craftreminderconfigID, viewDetail) {
        var self = this;
        $('#spnTitle').html("Craft Reminder Configuration");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);

        self.craftModel = ko.observable(new IPMSROOT.CraftModel());
        self.craftReminderConfigModel1 = ko.observable(new IPMSRoot.CraftReminderConfigModel1());

        self.craftmasterList = ko.observableArray([]);
        self.craftconfigreferenceData = ko.observable();

        self.IsSave = ko.observable(true);
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.IsBack = ko.observable(false);
        $("#bckButton").hide();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadCraftMaster();
            self.LoaddropdownsData();
            self.viewMode('List');
            if (viewDetail == true) { self.viewMode('Pop'); }
            self.IsCodeEnable(true);
        }

        self.LoadCraftMaster = function () {

            if (viewDetail == true) {

                self.viewModelHelper.apiGet('api/Crafts/' + craftreminderconfigID, null, function (result) {
                    self.craftmasterList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.CraftModel(item);
                    }));
                    self.WorkFlowView(self.craftmasterList()[0]);
                }, null, null, false);
               
            }
            else {
                self.viewModelHelper.apiGet('api/Crafts', null, function (result) {
                    self.craftmasterList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.CraftModel(item);
                    }));
                }, null, null, false);
            }

        }

        self.LoaddropdownsData = function () {
            self.viewModelHelper.apiGet('api/GetCraftreferencedata', null, function (result1) {
                self.craftconfigreferenceData(new IPMSRoot.CraftConfigReferenceData(result1));
            }, null, null, false);
        }

        self.Save = function (model) {
            var obj = self.craftReminderConfigModel1();

            obj.DateOfIssue($('#dateofissue').val());
            obj.DateOfValidity($('#dateofvalidity').val());

            obj.CraftID = model.CraftID();
            self.CraftValidation = ko.observable(obj);
            self.CraftValidation().errors = ko.validation.group(self.CraftValidation());
            var errors = self.CraftValidation().errors().length;
            if (errors == 0) {
                self.viewModelHelper.apiPost('api/CraftsReminderConfig', ko.mapping.toJSON(obj), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Craft reminder configuration saved successfully.", "Craft Reminder Configuration");

                    $(".close").trigger("click");

                    setTimeout(function () {
                        self.AddConfig(new IPMSRoot.CraftModel(data));
                    }, 1000);
                    self.cache = "";
                });
                $('#spnTitle').html("Craft Reminder Configuration");
            }
            else {
                self.CraftValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }

        }

        self.Update = function (model) {
            var obj = self.craftReminderConfigModel1();

            obj.DateOfIssue($('#dateofissue').val());
            obj.DateOfValidity($('#dateofvalidity').val());

            self.CraftValidation = ko.observable(obj);
            self.CraftValidation().errors = ko.validation.group(self.CraftValidation());
            var errors = self.CraftValidation().errors().length;
            if (errors == 0) {
                self.viewModelHelper.apiPut('api/CraftsReminderConfig', ko.mapping.toJSON(obj), function Message(data) {

                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Craft reminder configuration updated successfully.", "Craft Reminder Configuration");
                    $(".close").trigger("click");
                    self.viewMode('Form');
                    $('#spnTitle').html("Craft Reminder Configuration");
                });
            }
            else {
                self.CraftValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.AddConfig = function (model) {
            self.viewMode('Form');
            $("#bckButton").show();
            self.craftModel(model);
        }

        self.Back = function (model) {
            $('#spnTitle').html("Craft Reminder Configuration");
            self.viewMode('List');
        }

        self.AddNew = function (model) {
            self.viewMode('Form');
            $('#spnTitle').html("Add Craft Reminder Configuration");
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.craftReminderConfigModel1(new IPMSRoot.CraftReminderConfigModel1());
        }

        self.Edit = function (model) {
            self.viewMode('Form');
            $('#spnTitle').html("Update Craft Reminder Configuration");
            self.IsUpdate(true);
            self.IsSave(false);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsReset(true);
            self.craftReminderConfigModel1(model);
        }

        self.View = function (model) {
                self.craftReminderConfigModel1(model);
                self.viewMode('Form');
                $('#spnTitle').html("View Craft Reminder Configuration");
                self.IsUpdate(false);
                self.IsSave(false);
                self.IsReset(false);
                self.editableView(false);
                self.IsCodeEnable(false);
                $("#dateofissue").data('kendoDatePicker').enable(false);
                $("#dateofvalidity").data('kendoDatePicker').enable(false);
                $("#particularsname1").data('kendoDropDownList').enable(false);
                $("#particularsname2").data('kendoDropDownList').enable(false);
                $("#particularsname3").data('kendoDropDownList').enable(false);
                $("#Status").data('kendoDropDownList').enable(false);
                $("#particularsname").data('kendoDropDownList').enable(false);          
        }
        self.WorkFlowView = function (model) {
            if (viewDetail == true) {

                self.viewMode('Pop');
                self.craftReminderConfigModel1(model.CraftReminderConfig()[0]);
                self.craftModel(model);
                $('#spnTitle').html("View Craft Reminder Configuration");
                self.IsUpdate(false);
                self.IsSave(false);
                self.IsReset(false);
                self.editableView(false);
                self.IsCodeEnable(false);

                var ReferenceID = model.CraftReminderConfig()[0].CraftReminderConfigID();
                var WorkflowInstanceID = 0;

                self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                      null,
                            function (result) {

                                ko.utils.arrayForEach(result, function (val) {
                                    var pendingtaskaction = new IPMSROOT.pendingTask();
                                    pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                    pendingtaskaction.ReferenceID(val.ReferenceID);
                                    pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                    pendingtaskaction.APIUrl(val.APIUrl);
                                    pendingtaskaction.TaskName(val.TaskName);
                                    pendingtaskaction.TaskDescription(val.TaskDescription);
                                    pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                    pendingtaskaction.HasRemarks(val.HasRemarks);
                                    self.craftReminderConfigModel1().pendingTasks.push(pendingtaskaction);

                                });
                            });
            }              
        }
        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.craftReminderConfigModel1());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }
        self.CancelWF = function () {
            window.location.href = "/Welcome";
        }

        self.Reset = function () {
            ko.validation.reset();
            $('#spnTitle').html("Update Craft Reminder Configuration");
            self.craftReminderConfigModel1().reset();
        }

        self.Cancel = function () {
            ko.validation.reset();
            $('#spnTitle').html("Craft Reminder Configuration");
            self.craftReminderConfigModel1().reset();
            self.cache = "";
        }

        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();
    }
    IPMSRoot.CraftReminderConfigViewModel = CraftReminderConfigViewModel;

}(window.IPMSROOT));