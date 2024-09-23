(function (IPMSRoot) {

    var FuelRequisitionViewModel = function (fuelrequisitionid, viewDetail) {

        var self = this;
        $('#spnTitile').html("Fuel Requisition");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.fuelRequisitionModel = ko.observable();
        self.FuelRequisitionList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.craftNameList = ko.observableArray();

        self.fuelRequisitionReferenceData = ko.observable();
        self.startDate = ko.observable();
        self.NameofPerson = ko.observable();
        self.isFuelRequistionchk = ko.observable(false);
        self.FuelRequisition = ko.observable();
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.fuelRequisitionModel(new IPMSROOT.FuelRequisitionModel());
            self.LoadInitialData();
            self.LoadFuelRequisitions();
            self.LoadCraftNames();
            self.viewMode('List');
            self.fuelRequisitionModel().startDate(new Date());

        }

        self.LoadFuelRequisitions = function () {
            if (viewDetail == true) {

                self.viewModelHelper.apiGet('api/FuelRequisition/' + fuelrequisitionid,
                 { fuelrequisitionid: fuelrequisitionid },
                  function (result) {
                      self.FuelRequisitionList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.FuelRequisitionModel(item);
                      }));
                      self.viewfuelrequisition(self.FuelRequisitionList()[0]);
                  });
            }
            else {

                self.viewModelHelper.apiGet('api/FuelRequisition',
                null,
                  function (result) {

                      self.FuelRequisitionList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.FuelRequisitionModel(item);
                      }));


                  });

            }
        }

        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/FuelRequisitionReferenceData', null,
                    function (result1) {

                        //TODO: Load this into self.ReferenceData().  and do it only once. not every time.
                        self.fuelRequisitionReferenceData(new IPMSRoot.FuelRequisitionReferenceData(result1));
                        self.NameofPerson(result1.OwnersName);
                        $(".FuelRequistion").attr('checked', false);
                    }, null, null, false);
        }

        self.LoadCraftNames = function () {
            self.viewModelHelper.apiGet('api/FuelRequisitionCraft',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.craftNameList);
              });
        }

        self.CraftSelect = function (e) {

            var selecteddataItem = this.dataItem(e.item.index());
            if (selecteddataItem.CraftID != "") {
                self.viewModelHelper.apiGet('api/FuelRequisitionCraftInfo/' + selecteddataItem.CraftID,
              //{ vcn: selecteddataItem.VCN },
              null,

                 function (result) {

                     self.fuelRequisitionModel().CraftCode(result.CraftCode);
                     self.fuelRequisitionModel().CraftType(result.CraftType);
                     self.fuelRequisitionModel().IMONo(result.IMONo);
                     self.fuelRequisitionModel().CraftName(result.CraftName);

                 });
            }
            else {
                self.fuelRequisitionModel().CraftCode("");
                self.fuelRequisitionModel().CraftType("");
                self.fuelRequisitionModel().IMONo("");


            }

        }

        self.AssignGrade = function (e) {

            var dataItem = this.dataItem(e.item.index());
            self.fuelRequisitionModel().Grade(dataItem.SubCatName);
        }
        self.AssignOil = function (e) {

            var dataItem = this.dataItem(e.item.index());
            self.fuelRequisitionModel().OilType(dataItem.SubCatName);
        }

        self.addfuelrequisition = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(true);

            self.isFuelRequistionchk(false);

            self.fuelRequisitionModel(new IPMSRoot.FuelRequisitionModel());
            $('#spnTitile').html("Add Fuel Requisition");
            $("#RequiredDate").data('kendoDatePicker').enable(true);
            //  $("#RequisitionDate").data('kendoDatePicker').enable(true);
            self.fuelRequisitionModel().RequisitionDate(new Date());
            self.fuelRequisitionModel().startDate(new Date());
            self.fuelRequisitionModel().OwnersName(self.NameofPerson());
            $("#rdYesFuelRequistion").attr('checked', 'checked');
            $("#divoiltype").show();
            AnyFuelRequistionClick = function () {
                $("input[name='FuelRequistion']").on("click", function () {

                    if ($("input[name='FuelRequistion']:checked").val() == 'Both') {
                        $("#divfuelrequistion").show();

                        $("#divoiltype").hide();
                        $("#divgrade").hide();
                        $("#spanmaintenance").show();
                        $("#spanoiltype").show();
                    }

                    else if ($("input[name='FuelRequistion']:checked").val() == 'Oil') {
                        $("#divoiltype").show();

                        $("#divfuelrequistion").hide();
                        $("#divgrade").hide();
                        $("#spanmaintenance").hide();

                    }

                    else {
                        $("#divgrade").show();
                        $("#divfuelrequistion").hide();
                        $("#divoiltype").hide();
                        $("#spanoiltype").hide();

                    }

                });
            }

        }


        self.editfuelrequisition = function (fuelrequisition) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(false);
            self.isFuelRequistionchk(false);
            self.fuelRequisitionModel(fuelrequisition);
            $('#spnTitile').html("Update Fuel Requisition");
            $("#RequiredDate").data('kendoDatePicker').enable(true);
            self.fuelRequisitionModel().startDate(new Date());
            //  $("#RequisitionDate").data('kendoDatePicker').enable(true);
            // self.fuelRequisitionModel().OwnersName(self.NameofPerson());
            fuelrequisition.FuelRequistionType();

            $("input[name='FuelRequistion']").on("click", function () {
                if ($("input[name='FuelRequistion']:checked").val() == 'Both') {
                    $("#divfuelrequistion").show();
                    $("#divoiltype").hide();
                    $("#divgrade").hide();
                }

                else if ($("input[name='FuelRequistion']:checked").val() == 'Oil') {
                    $("#divoiltype").show();
                    $("#divfuelrequistion").hide();
                    $("#divgrade").hide();
                }
                else {
                    $("#divgrade").show();
                    $("#divfuelrequistion").hide();
                    $("#divoiltype").hide();

                }


            });

            if (fuelrequisition.FuelRequistionType() == 'Both') {
                $("#divfuelrequistion").show();
                $("#divoiltype").hide();
                $("#divgrade").hide();
                $("#rdBothFuelRequistion").attr('checked', 'checked');

            }

            else if (fuelrequisition.FuelRequistionType() == 'Oil') {
                $("#divoiltype").show();
                $("#divfuelrequistion").hide();
                $("#divgrade").hide();
                $("#rdYesFuelRequistion").attr('checked', 'checked');

            }
            else {
                $("#divgrade").show();
                $("#divfuelrequistion").hide();
                $("#divoiltype").hide();
                $("#rdNoFuelRequistion").attr('checked', 'checked');
            }


        }

        
        self.viewfuelrequisition = function (fuelrequisition) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.isFuelRequistionchk(false);
            self.fuelRequisitionModel(fuelrequisition);
            $('#spnTitile').html("View Fuel Requisition");
            $("#RequiredDate").data('kendoDatePicker').enable(false);
            //    $("#RequisitionDate").data('kendoDatePicker').enable(false);
            //  self.fuelRequisitionModel().OwnersName(self.NameofPerson());
            fuelrequisition.FuelRequistionType();
            if (fuelrequisition.FuelRequistionType() == 'Both') {
                $("#divfuelrequistion").show();
                $("#divoiltype").hide();
                $("#divgrade").hide();
                $("#rdBothFuelRequistion").attr('checked', 'checked');
            }

            else if (fuelrequisition.FuelRequistionType() == 'Oil') {
                $("#divoiltype").show();
                $("#divfuelrequistion").hide();
                $("#divgrade").hide();
                $("#rdYesFuelRequistion").attr('checked', 'checked');

            }
            else {
                $("#divgrade").show();
                $("#divfuelrequistion").hide();
                $("#divoiltype").hide();
                $("#rdNoFuelRequistion").attr('checked', 'checked');
            }

            var ReferenceID = fuelrequisition.FuelRequisitionID();
            var WorkflowInstanceID = fuelrequisition.WorkflowInstanceId();
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
                                 self.fuelRequisitionModel().pendingTasks.push(pendingtaskaction);
                             });
                         });

        }
        
        self.SaveFuelRequisition = function (model) {

            model.validationEnabled(true);
            self.FuelRequisitionValidation = ko.observable(model);
            model.FuelRequistionType($("input[name='FuelRequistion']:checked").val());
            self.FuelRequisitionValidation().errors = ko.validation.group(self.FuelRequisitionValidation());

            var errors = self.FuelRequisitionValidation().errors().length;


            if (errors == 0 || errors == 1) {
                if (self.IsSave() == true) {
                    self.viewModelHelper.apiPost('api/FuelRequisition', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Fuel Requisition details saved successfully", "Fuel Requisition");
                        self.LoadFuelRequisitions();
                        $('#spnTitile').html("Fuel Requisition");
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.FuelRequisitionValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
                //$('#divValidationError').removeClass('display-none');
                return;
            }

        }

        self.ModifyFuelRequisition = function (model) {
            model.validationEnabled(true);
            self.FuelRequisitionValidation = ko.observable(model);
            self.FuelRequisitionValidation().errors = ko.validation.group(self.FuelRequisitionValidation());
            var errors = self.FuelRequisitionValidation().errors().length;
            model.FuelRequistionType($("input[name='FuelRequistion']:checked").val());
            if (errors == 0) {
                //  if (self.IsSave() == true) {
                self.viewModelHelper.apiPut('api/FuelRequisition', ko.mapping.toJSON(model), function Message(data) {
                    model.RecordStatus(data.RecordStatus);
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Fuel Requisition details updated successfully", "Fuel Requisition");
                    self.LoadFuelRequisitions();
                    $('#spnTitile').html("Fuel Requisition");
                    self.viewMode('List');

                });
                //    }
            }
            else {
                self.FuelRequisitionValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
                //$('#divValidationError').removeClass('display-none');
                return;
            }

        }

       


        self.ResetFuelRequisition = function (model) {


            if (self.IsSave()) {
                ko.validation.reset();
                model.validationEnabled(false);
                self.fuelRequisitionModel().reset();
                $("#rdYesFuelRequistion").prop("checked", false);
                //$("#rdYesFuelRequistion").attr('checked', 'checked');
                $("#rdBothFuelRequistion").prop("checked", false);
                $("#rdNoFuelRequistion").prop("checked", false);
                self.isFuelRequistionchk = ko.observable(false);
                self.fuelRequisitionModel().RequisitionDate(new Date());
            }
            else {

                ko.validation.reset();
                self.fuelRequisitionModel().reset();
                self.fuelRequisitionModel().OwnersName(self.NameofPerson());
                self.editfuelrequisition(model);
            }
        }


        self.CancelFuelRequisition = function (model) {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {


               


                window.location.href = '/FuelRequisition';

                self.fuelRequisitionModel().reset();
                $('#spnTitile').html("Fuel Requisition");
            }

        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            //dat.FuelRequistionType($("input[name='FuelRequistion']:checked").val());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.fuelRequisitionModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        self.viewWorkFlow = function (fuelrequisition) {
            var workflowinstanceId = fuelrequisition.WorkflowInstanceId();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {

                  self.fuelRequisitionModel(new IPMSROOT.FuelRequisitionModel());
                  self.fuelRequisitionModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }
        self.Initialize();
    }
    IPMSRoot.FuelRequisitionViewModel = FuelRequisitionViewModel;


}(window.IPMSROOT));


