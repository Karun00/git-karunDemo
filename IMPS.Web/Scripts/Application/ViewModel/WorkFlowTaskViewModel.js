(function (IPMSRoot) {

    var WorkFlowTaskViewModel = function () {
        var self = this;
        $('#spnTitle').html("Workflow Configuration");

        self.workflowRefernceData = ko.observable();

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.WorkFlowTaskDataList = ko.observableArray();
        self.workflowtaskModel = ko.observable(new IPMSROOT.WorkFlowTaskModel());
        self.LoadWorkFlowTaskDetails = ko.observable();

        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsModified = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.IsAdd = ko.observable(true);
        self.IsActivityEnable = ko.observable(true);
        self.IsSaveUpdateDisabled = ko.observable(false);

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.viewMode('List');
            self.LoadReferenceData();
            self.IsCodeEnable(true);
            self.LoadWorkFlowTaskData();
        }

        self.LoadReferenceData = function () {
            self.viewModelHelper.apiGet('api/WorkFlowTask/GetWorkFlowTaskReferenceDetails', null, function (result) {
                self.workflowRefernceData(new IPMSRoot.WorkFlowRefernceData(result));
            }, null, null, false);
        }

        self.LoadWorkFlowTaskData = function () {
            $('#spnTitle').html("Workflow Configuration");

            self.viewModelHelper.apiGet('api/WorkFlowTaskData', null, function (result) {
                self.WorkFlowTaskDataList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.WorkFlowTaskModel(item);
                }));
            });
        }

        CheckFormValidation = function () {

            var databaseList = ko.toJSON(self.workflowtaskModel().WorkFlowTaskVO);
            var jsonObj = JSON.parse(databaseList);
            var errors = 0;

            $.each(jsonObj, function (index, value) {
                var WorkflowTaskCode = value.WorkflowTaskCode;
                var strWorkflowTaskCode = "";
                if ((WorkflowTaskCode != "") && (WorkflowTaskCode != undefined)) {
                    strWorkflowTaskCode = "SSS";
                }

                var Step = value.Step;
                var strStep = "";
                if ((Step >= 0) && (Step != undefined)) {
                    strStep = "SSS";
                }

                var NextStep = value.NextStep;
                var strNextStep = "";
                if ((NextStep >= 0) && (NextStep != undefined)) {
                    strNextStep = "SSS";
                }

                var HasNotification = value.HasNotification;
                var strHasNotification = "";
                if ((HasNotification != "") && (HasNotification != undefined)) {
                    strHasNotification = "SSS";
                }

                var ValidityPeriod = value.ValidityPeriod;
                var strValidityPeriod = "";
                if ((ValidityPeriod >= 0) && (ValidityPeriod != undefined)) {
                    strValidityPeriod = "SSS";
                }

                var RoleID = value.RoleID;
                var strRoleID = "";
                if ((RoleID > 0) && (RoleID != undefined)) {
                    strRoleID = "SSS";
                }

                if (strWorkflowTaskCode == "" || strStep == "" || strValidityPeriod == "") {
                    errors = errors + 1;
                }
            });
            return errors;
        }

        self.AddNewTask = function (Data) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            Data.validationEnabled(true);

            var errors = CheckFormValidation();

            if (errors > 0) {
                toastr.warning("Please select Task / Step / Validity Period.", "Workflow Configuration");
            }
            if (Data.EntityID() > 0 && errors == 0) {
                var WorkFlowTaskVO = new IPMSROOT.WorkFlowTaskVO();
                WorkFlowTaskVO.EntityID(Data.EntityID());
                self.workflowtaskModel().WorkFlowTaskVO.push(WorkFlowTaskVO);
            }
        }

        self.addWorkFlowTask = function (data) {
            self.workflowtaskModel(new IPMSROOT.WorkFlowTaskModel());
            $('#spnTitle').html("Add Workflow Configuration");
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.IsAdd(true);
            self.editableView(true);
            self.IsCodeEnable(true);
        }

        // This is used to Edit WorkFlow Task
        self.editWorkFlowTask = function (data) {
            self.viewMode('Form');
            self.IsSave(false);
            self.IsUpdate(true);
            self.IsCodeEnable(true);
            self.IsReset(true);
            self.IsAdd(true);
            self.IsActivityEnable(true);
            self.workflowtaskModel(data);

            $("#entity").prop('disabled', true);
            $('#spnTitle').html("Update Workflow Configuration");
        }

        // This is used to View WorkFlow Task
        self.viewWorkFlowTask = function (data) {
            $('#spnTitle').html("View Workflow Configuration");
            self.workflowtaskModel(data);
            self.viewMode('Form');
            self.IsSave(false);
            self.IsUpdate(false);
            self.IsReset(false);
            self.IsCodeEnable(false);
            self.IsAdd(false);
            $("#entity").prop('disabled', true);
        }

        //change Event for Entity selection
        self.ChangeEntity = function (data, event) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var selectedEntityID = data.EntityID();
            if (selectedEntityID != null || selectedEntityID != undefined) {
                var obj = self.WorkFlowTaskDataList();
                var iCount = 0;
                self.Valid = ko.observable();
                self.Valid = obj.filter(function (a) {
                    if (a.EntityID() == selectedEntityID) {
                        iCount = iCount + 1;
                    }
                });

                if (iCount > 0) {
                    toastr.warning("The selected entity is already exists.", "Workflow Configuration");
                    $("select#entity").prop('selectedIndex', 0);
                }
            }
        }

        //change Event for Work flow Task selection against entityid
        ChangeWorkflowTask = function (data, event) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var selectedWorkflowTaskCode = data.WorkflowTaskCode();
            var obj = self.workflowtaskModel().WorkFlowTaskVO();
            var iCount = 0;
            self.thisWorkflowTaskRoles = ko.observableArray();
            self.thisWorkflowTaskRoles = obj;
            self.Valid = ko.observable();
            self.Valid = self.thisWorkflowTaskRoles.filter(function (a) {
                if (a.WorkflowTaskCode() == selectedWorkflowTaskCode) {
                    iCount = iCount + 1
                }
            });

            if (iCount > 1) {
                toastr.warning("The selected Task name is already exists.! Please select another task.", "Workflow Configuration");
                self.IsSaveUpdateDisabled(true);
            }
            else {
                self.IsSaveUpdateDisabled(false);
            }
        }

        //change Event for role selection against step
        ChangeRoleForStep = function (data, event) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            
            self.filteredlist = ko.observableArray('');
            self.thisWorkflowTaskRoles = ko.observableArray('');
            self.gridlist = ko.observableArray('');
            self.selectedStep = ko.observable('');
            self.selectedStep = data.Step();
            self.gridlist = self.workflowtaskModel().WorkFlowTaskVO();
            var iCount = 0;
            
            for (var g = 0; g < self.gridlist.length; g++) {
                self.gridlist[g].Step(parseInt(self.gridlist[g].Step()));
            }
            self.thisWorkflowTaskRoles = self.gridlist;

            //filter rows of selected Step
            self.filteredlist = self.thisWorkflowTaskRoles.filter(function (a) {
                return parseInt(a.Step()) === parseInt(self.selectedStep);
            });

            var filteredlist = self.filteredlist;

            //Loop for check items from grid filtered list
            for (var i = 0; i < filteredlist.length; i++) {
                var istep = filteredlist[i].Step();
                var iroles = new Array();
                iroles = filteredlist[i].arrayRoles();
                iroles.sort(function (a, b) { return a - b });
                var striroles = iroles.join(",");

                for (var j = i + 1; j < filteredlist.length; j++) {
                    var jstep = filteredlist[j].Step();
                    var jroles = new Array();
                    jroles = filteredlist[j].arrayRoles();
                    jroles.sort(function (a, b) { return a - b });
                    var strjroles = jroles.join(",");

                    //When Roles Mismatched
                    if (striroles != strjroles) {
                        //alert('Role(s) Mismatch ' + striroles + ' with ' + strjroles);
                        iCount = iCount + 1;
                    }
                }
            }

            if (iCount > 0) {
                toastr.warning("For same step should assign same roles.", "Workflow Configuration");
                self.IsSaveUpdateDisabled(true);
            }
            else {
                self.IsSaveUpdateDisabled(false);
            }
        }

        self.saveTask = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.WorkFlowTaskValidation = ko.observable(model);
            self.WorkFlowTaskValidation().errors = ko.validation.group(self.WorkFlowTaskValidation());
            var valerrors = self.WorkFlowTaskValidation().errors().length;
            var errors = CheckFormValidation();

            if (errors > 0) {
                toastr.warning("Please select Task / Step / Validity Period.", "Workflow Configuration");
            }

            if (valerrors == 0 && errors == 0) {
                self.viewModelHelper.apiPost('api/AddWorkFlowTaskData', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.success("Workflow Configuration saved successfully.", "Workflow Configuration");
                                self.LoadWorkFlowTaskData();
                                self.viewMode('List');
                            });
            }
            else {
                self.WorkFlowTaskValidation().errors.showAllMessages();
                return;
            }
        }

        self.updateTask = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.WorkFlowTaskValidation = ko.observable(model);
            self.WorkFlowTaskValidation().errors = ko.validation.group(self.WorkFlowTaskValidation());
            var valerrors = self.WorkFlowTaskValidation().errors().length;
            var errors = CheckFormValidation();

            if (errors > 0) {
                toastr.warning("Please select Task / Step / Validity Period.", "Workflow Configuration");
            }

            if (valerrors == 0 && errors == 0) {
                self.viewModelHelper.apiPut('api/ModifyWorkFlowTask', ko.mapping.toJSON(model),
                                function Message(data) {
                                    toastr.success("Workflow configuration updated successfully.", "Workflow Configuration");
                                    self.LoadWorkFlowTaskData();
                                    self.viewMode('List');
                                });
            }
            else {
                self.WorkFlowTaskValidation().errors.showAllMessages();
                return;
            }
        }

        /////// mahesh : For Reset functionality ...................
        self.ResetRequest = function (model) {
            model.validationEnabled(false);
            self.IsSaveUpdateDisabled(false);
            ko.validation.reset();
            self.workflowtaskModel().reset();
        }

        /////// mahesh : For Cancel Functionality....................
        self.Cancel = function () {
            self.workflowtaskModel().validationEnabled(false);
            self.IsSaveUpdateDisabled(false);
            ko.validation.reset();
            self.workflowtaskModel().reset();
            self.LoadWorkFlowTaskData();
            $('#spnTitle').html("Workflow Configuration");
            self.viewMode('List');
        }

        self.Initialize();
    }

    IPMSRoot.WorkFlowTaskViewModel = WorkFlowTaskViewModel;

}(window.IPMSROOT));
