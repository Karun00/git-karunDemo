(function (IPMSRoot) {

    var BerthMaintenanceCompViewModel = function (berthmaintenancecompid, viewDetail) {

        var self = this;
        $('#spnTitile').html("Berth Maintenance Completion");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.RadiobtnView = ko.observable(false);
        self.berthmaintenancecompModel = ko.observable();
        self.IsAdd = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.BerthMainCompList = ko.observableArray();
        self.BerthMainIDsList = ko.observableArray();
        self.IsBerthmainidlistenable = ko.observable(true);
        self.BethMaintenanceDetails = ko.observableArray();
        self.RefData = ko.observable();
        self.Initialize = function () {
            self.viewMode = ko.observable(true);

            self.viewMode('List');
            self.LoadBerthMainComp();
            self.berthmaintenancecompModel(new IPMSROOT.BerthMaintenanceCompModel());
            self.viewModelHelper.apiGet('api/BerthMaintenanceids', null,
             function (result) {
                 var berthlist = $.map(result, function (item) {
                     return new IPMSRoot.BerthMaintenance(item);
                 });
                 self.BerthMainIDsList(berthlist);
             });
            if (viewDetail == true) {
            }
            else {
                self.viewMode('List');
            }
        }

        self.BerthMainID = function (data) {
            this.BerthMaintenanceID = ko.observable(data.BerthMaintenanceID);
        };

        self.LoadBerthMainComp = function () {           
            if (viewDetail == true) {
                self.viewModelHelper.apiGet('api/BerthMaintenanceCompletions/' + berthmaintenancecompid,
                 { berthmaintenancecompid: berthmaintenancecompid },
                  function (result) {
                      self.BerthMainCompList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.BerthMaintenanceCompModel(item);
                      }));
                      self.viewBerthMaintComp(self.BerthMainCompList()[0]);
                  });
            }
            else {
                self.viewModelHelper.apiGet('api/BerthMaintenanceCompletions',
                null,
                  function (result) {
                      self.BerthMainCompList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.BerthMaintenanceCompModel(item);
                      }));
                  });
            }
        }

        self.BerthMaintSelect = function (e) {     
            var dataItem = this.dataItem(e.item.index());
            self.berthmaintenancecompModel().BerthMaintData(dataItem);
            self.berthmaintenancecompModel().CompletionDateTime("");
            $("#CompletionDate1").data('kendoDateTimePicker').min(dataItem.PeriodFrom);
            $("#CompletionDate1").data('kendoDateTimePicker').max(new Date());

        }

        self.addberthmaincomp = function () {       
            self.viewMode('Form');          
            self.IsUpdate(false);

            self.IsReset(true);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsBerthmainidlistenable(true);
            self.berthmaintenancecompModel(new IPMSRoot.BerthMaintenanceCompModel());

            self.viewModelHelper.apiGet('api/BerthMaintenanceids', null,
           function (result) {
               var berthlist = $.map(result, function (item) {
                   return new IPMSRoot.BerthMaintenance(item);
               });
               self.BerthMainIDsList(berthlist);
           });
 

            self.IsUnique(false);
            $("#CompletionDate1").data('kendoDateTimePicker').enable(true);
            $("#CompletionDate2").data('kendoDateTimePicker').enable(true);
            $('#spnTitile').html("Add Berth Maintenance Completion");
            self.IsAdd(true);
            $("#Edit").hide();
            $("#Add").show();
            //$("#CompletionDate1").data('kendoDateTimePicker').min(new Date());
            //$("#CompletionDate1").data('kendoDateTimePicker').max(new Date());
        }

        self.editBerthMainComp = function (BerthMainComp) {
            debugger;
            $('#spnTitile').html("Update Berth Maintenance Completion");
            $("#Add").hide();
            $("#Edit").show();
            self.viewMode('Form');
            self.IsAdd(false);
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(false);
            subscribeToModelChange(self);
            self.IsBerthmainidlistenable(false);
            self.berthmaintenancecompModel(BerthMainComp);

            $("#CompletionDate1").data('kendoDateTimePicker').enable(true);
            $("#CompletionDate2").data('kendoDateTimePicker').enable(true);
            $("#Add").hide();
            $("#Edit").show();
            $("#CompletionDate1").data('kendoDateTimePicker').min(BerthMainComp.PeriodFrom());
            $("#CompletionDate1").data('kendoDateTimePicker').max(new Date());
            $("#CompletionDate2").data('kendoDateTimePicker').min(BerthMainComp.PeriodFrom());
            $("#CompletionDate2").data('kendoDateTimePicker').max(new Date());
        }

        self.viewBerthMaintComp = function (BerthMainComp) {
            debugger;
            $('#spnTitile').html("View Berth Maintenance Completion");
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsAdd(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.IsBerthmainidlistenable(false);
            self.berthmaintenancecompModel(BerthMainComp);
            $("#CompletionDate1").data('kendoDateTimePicker').enable(false);
            $("#CompletionDate2").data('kendoDateTimePicker').enable(false);
            $("#Add").hide();
            $("#Edit").show();
            var ReferenceID = BerthMainComp.BerthMaintenanceCompletionID();
            var WorkflowInstanceID = BerthMainComp.WorkflowInstanceId();
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
                                 pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                 pendingtaskaction.TaskDescription(val.TaskDescription);
                                 pendingtaskaction.HasRemarks(val.HasRemarks);
                                 self.berthmaintenancecompModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }

        self.berthmainIdChanged = function (event) {
            if (event.BerthMaintenanceID() == undefined) {
            }
            else {
                self.LoadBerthDetails(event.BerthMaintenanceID());
            }
        }

        self.LoadBerthDetails = function (data) {
            self.viewModelHelper.apiGet('api/BerthMaintenanceids/' + data,
                null,
               function (result) {
                   self.RefData(ko.utils.arrayMap(result, function (item) {
                       return new IPMSRoot.BerthMaintenance(item);
                   }));
               });
        }

        self.Cancel = function () {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.viewMode('List');
                self.berthmaintenancecompModel().reset();
                $('#spnTitile').html("Berth Maintenance Completion");
                $("#Add").hide();
                $("#Edit").hide();
            }
        }

        self.ResetbethmaintComp = function (BerthMainComp) {
           // $('#divValidationError').addClass('display-none');
            ko.validation.reset();
            BerthMainComp.validationEnabled(false);
            //self.BerthmainValidation().errors.showAllMessages(false);      
            if (self.IsAdd()) {
                self.berthmaintenancecompModel().reset();
                self.addberthmaincomp();
                $("#Edit").hide();
                $("#Add").show();
               
                self.IsUpdate(false);
                self.IsAdd(true);
                self.IsReset(true);
                self.IsSave(true);

            }
            if (self.IsUpdate()) {
                self.berthmaintenancecompModel().reset();
                self.berthmaintenancecompModel(BerthMainComp);
                $("#Edit").show();
                $("#Add").hide();
                self.IsUpdate(true);
                self.IsSave(false);
                self.IsReset(true);
                self.editableView(true);
                self.IsCodeEnable(false);
            }

        }

        self.SavebethmaintComp = function (model) {         
            model.validationEnabled(true);
            self.BerthmainValidation = ko.observable(model);
            self.BerthmainValidation().errors = ko.validation.group(self.BerthmainValidation());
            var errors = self.BerthmainValidation().errors().length;
            var duplicate = false;
            model.BerthMaintenanceNo(model.BerthMaintData().BerthMaintenanceNo);
            model.MaintenanceTypeCode(model.BerthMaintData().MaintenanceTypeCode);
            model.MaintBerthCode(model.BerthMaintData().MaintBerthCode);
            model.FromBollard(model.BerthMaintData().FromBollard);
            model.ToBollard(model.BerthMaintData().ToBollard);

            if (errors == 0) {
                self.IsUnique(true);
                self.viewModelHelper.apiPost('api/BerthMaintenanceCompletions', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Berth Maintenance Completion Details Saved Successfully", "Berth Maintenance Completion");
                    $('#spnTitile').html("Berth Maintenance Completion");
                    self.LoadBerthMainComp();
                    self.viewMode('List');
                });
            }
            else {
                self.BerthmainValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
               // $('#divValidationError').removeClass('display-none');
                return;
            }
            $("#Add").hide();
            $("#Edit").hide();
        }

        self.ModifybethmaintComp = function (model) {
            model.validationEnabled(true);
            self.BerthmainValidation = ko.observable(model);
            self.BerthmainValidation().errors = ko.validation.group(self.BerthmainValidation());
            var errors = self.BerthmainValidation().errors().length;
            var duplicate = false;
            if (errors == 0) {
                self.IsModified(true);
                self.viewModelHelper.apiPut('api/BerthMaintenanceCompletions', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Berth Maintenance Completion Details Updated Successfully", "Berth Maintenance Completion");
                    $('#spnTitile').html("Berth Maintenance Completion");
                    self.LoadBerthMainComp();
                    self.viewMode('List');
                });
            }
            else {
                self.BerthmainValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
            //    $('#divValidationError').removeClass('display-none');
                return;
            }
            $("#Add").hide();
            $("#Edit").hide();
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }



        self.WorkflowAction = function (dat) {          
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.berthmaintenancecompModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        self.viewWorkFlow = function (berthmaintenanceComp) {
            debugger;
            var workflowinstanceId = berthmaintenanceComp.WorkflowInstanceId();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {
                  debugger;

                  self.berthmaintenancecompModel(new IPMSROOT.BerthMaintenanceCompModel());
                  self.berthmaintenancecompModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').val(result);
                  $('#stack1').modal('show');

              });

        }

        self.Initialize();
    }
    IPMSRoot.BerthMaintenanceCompViewModel = BerthMaintenanceCompViewModel;
}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.berthmaintenancecompModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.berthmaintenancecompModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}