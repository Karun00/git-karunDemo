    (function (IPMSRoot) {

    var BerthMaintenanceViewModel = function (berthmaintenanceid, viewDetail) {

        var self = this;
        $('#spnTitile').html("Berth Maintenance");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.berthmaintenanceModel = ko.observable();
        self.BerthMaintenanceList = ko.observableArray();
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
        self.berthMaintenanceReferenceData = ko.observable();     
        self.masterDepartments = ko.observableArray([]);
        self.BollardList = ko.observableArray();
        self.ToBollardList = ko.observableArray();
        self.isBerthChanged = ko.observable();
        self.validationEnabled = ko.observable();       
        self.startDate = ko.observable();
        self.BerthKy = ko.observable();

        self.Initialize = function () {         
            self.viewMode = ko.observable(true);
            self.berthmaintenanceModel(new IPMSROOT.BerthMaintenanceModel());           
            self.LoadInitialData();
            self.LoadBerthMaintenence(); 
            self.viewMode('List');
           // self.startDate(new Date());
            self.berthmaintenanceModel().startDate(new Date());

        }

        self.LoadBerthMaintenence = function () {

            if (viewDetail == true) {

                self.viewModelHelper.apiGet('api/BerthMaintenances/' + berthmaintenanceid,
                 { berthmaintenanceid: berthmaintenanceid },
                  function (result) {
                      self.BerthMaintenanceList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.BerthMaintenanceModel(item);
                      }));
                      self.viewberthmaintenance(self.BerthMaintenanceList()[0]);
                  });
            }
            else {

                self.viewModelHelper.apiGet('api/BerthMaintenances',
                null,
                  function (result) {

                      self.BerthMaintenanceList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.BerthMaintenanceModel(item);
                      }));
                  });
            }
        }        

        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/BerthMaintenanceReferenceData', null,
                    function (result1) {
                        //TODO: Load this into self.ReferenceData().  and do it only once. not every time.                      
                        self.berthMaintenanceReferenceData(new IPMSRoot.BerthMaintenanceReferenceData(result1));
                    }, null, null, false);
        }

        self.LoadBollardsBerth = function (event) {           
           // self.BollardList("");
          //  self.ToBollardList("");

            if (event == undefined) {
                self.BollardList({ FromBollardKey: '', BollardName: '' });
                self.ToBollardList({ ToBollardKey: '', BollardName: '' });
                self.isBerthChanged(false);
            }
            else {
                self.isBerthChanged(true);
                self.berthmaintenanceModel().BerthName($("#BerthNameId").find("option:selected").text());
                if (event.BerthKey() != null) {
                    var BerthKeySplit = event.BerthKey().split('.');
                    self.LoadBerths(BerthKeySplit[0], BerthKeySplit[1], BerthKeySplit[2]);
                }
            }

           
        }     


        self.LoadBerths = function (PortCode,QuayCode,BerthCode) {

            self.viewModelHelper.apiGet('api/BerthMaintenances/' + PortCode + '/' + QuayCode + '/' + BerthCode,
                null,
           function (result) {             
               ko.mapping.fromJS(result, {}, self.BollardList);
               ko.mapping.fromJS(result, {}, self.ToBollardList);
           });

        }

        self.AssignFromBollard = function (event) {
            self.berthmaintenanceModel().BollardsFrom($("#BollardFromId").find("option:selected").text());
        }

        self.AssignToBollard = function (event) {
            self.berthmaintenanceModel().BollardsTo($("#BollardToId").find("option:selected").text());
        }

        
        self.AssignMaint = function (event) {
            self.berthmaintenanceModel().MaintenanceType($("#MaintenanceId").find("option:selected").text());           
        }

        self.addberthmaintenance = function () {
            self.startDate(new Date());
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(true);
            self.isBerthChanged(false);
            self.validationEnabled(true);        
            self.berthmaintenanceModel(new IPMSRoot.BerthMaintenanceModel());
            self.berthmaintenanceModel().startDate(new Date());
            $('#spnTitile').html("Add Berth Maintenance");
            $("#PeriodFrom").data('kendoDateTimePicker').enable(true);
            $("#PeriodTo").data('kendoDateTimePicker').enable(true);
            $("#BerthNo").hide();
        }

        self.editberthmaintenance = function (berthmaintenance) {
            debugger;
            self.LoadBerths(berthmaintenance.MaintPortCode(), berthmaintenance.MaintQuayCode(), berthmaintenance.MaintBerthCode());
            self.BerthKy(berthmaintenance.BerthKey());
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsModified(true);
            self.IsCodeEnable(false);
            self.isBerthChanged(true);
            self.BollardList([{ FromBollardKey: berthmaintenance.FromPortCode() + '.' + berthmaintenance.FromQuayCode() + '.' + berthmaintenance.FromBerthCode() + '.' + berthmaintenance.FromBollard(), BollardName: berthmaintenance.BollardName() }]);
            self.ToBollardList([{ ToBollardKey: berthmaintenance.ToPortCode() + '.' + berthmaintenance.ToQuayCode() + '.' + berthmaintenance.ToBerthCode() + '.' + berthmaintenance.ToBollard(), BollardName: berthmaintenance.BollardName() }]);
            self.berthmaintenanceModel(berthmaintenance);
            $("#PeriodFrom").data('kendoDateTimePicker').enable(true);
            $("#PeriodTo").data('kendoDateTimePicker').enable(true);
            $('#spnTitile').html("Update Berth Maintenance");
            $("#BerthNo").show();

            $("#PeriodFrom").data('kendoDateTimePicker').min(self.berthmaintenanceModel().PeriodFrom());
            //  $("#PeriodTo").data('kendoDateTimePicker').min(self.berthmaintenanceModel().PeriodFrom());
            var myDatePicker = new Date(self.berthmaintenanceModel().PeriodFrom());
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours();
            var Mnt = myDatePicker.getMinutes() + 1;
            $("#PeriodTo").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
        }

    


        self.viewberthmaintenance = function (berthmaintenance) {           
            self.LoadBerths(berthmaintenance.MaintPortCode(), berthmaintenance.MaintQuayCode(), berthmaintenance.MaintBerthCode());
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.isBerthChanged(false);
            self.BollardList([{ FromBollardKey: berthmaintenance.FromPortCode() + '.' + berthmaintenance.FromQuayCode() + '.' + berthmaintenance.FromBerthCode() + '.' + berthmaintenance.FromBollard(), BollardName: berthmaintenance.BollardName() }]);
            self.ToBollardList([{ ToBollardKey: berthmaintenance.ToPortCode() + '.' + berthmaintenance.ToQuayCode() + '.' + berthmaintenance.ToBerthCode() + '.' + berthmaintenance.ToBollard(), BollardName: berthmaintenance.BollardName() }]);
            self.berthmaintenanceModel(berthmaintenance);
            $("#PeriodFrom").data('kendoDateTimePicker').enable(false);
            $("#PeriodTo").data('kendoDateTimePicker').enable(false);
            $('#spnTitile').html("View Berth Maintenance");
            $("#BerthNo").show();           
            var ReferenceID = berthmaintenance.BerthMaintenanceID();
            var WorkflowInstanceID = berthmaintenance.WorkflowInstanceId();
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
                                 self.berthmaintenanceModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }

        self.SaveBerthMaintenance = function (model) {            
            model.validationEnabled(true);
            self.BerthMaintenanceValidation = ko.observable(model);
            if (model.PeriodFrom() == "Invalid date")
                model.PeriodFrom("");

            if (model.PeriodTo() == "Invalid date")
                model.PeriodTo("");
            self.BerthMaintenanceValidation().errors = ko.validation.group(self.BerthMaintenanceValidation());
            var errors = self.BerthMaintenanceValidation().errors().length;
            var duplicate = false;
            var bollardvalid = true;
       
            var validate = true;
            var PeriodFrom = $("#PeriodFrom").val();
            var PeriodTo = $("#PeriodTo").val();       
                  
            
            if (errors == 0) {
                //if (PeriodFrom > PeriodTo) {
                  
                //    toastr.options.closeButton = true;
                //    toastr.options.positionClass = "toast-top-right";
                //    toastr.warning("Period From should be greater than Period To");
                //    validate = false;
                //}
              if (model.BollardsFrom() == model.BollardsTo()) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Bollard From and Bollard To cannot be same");
                    self.berthmaintenanceModel().ToBollardKey("");
                    self.berthmaintenanceModel().ToBollardKey($("#BollardToId").val(""));
                    bollardvalid = false;
                }
                if (bollardvalid == true) {
                    
                    var BerthKeyData = model.BerthKey().split('.');
                    model.MaintPortCode(BerthKeyData[0]);
                    model.MaintQuayCode(BerthKeyData[1]);
                    model.MaintBerthCode(BerthKeyData[2]);

                    var FromollardKeyData = model.FromBollardKey().split('.');

                    model.FromPortCode(FromollardKeyData[0]);
                    model.FromQuayCode(FromollardKeyData[1]);
                    model.FromBerthCode(FromollardKeyData[2]);
                    model.FromBollard(FromollardKeyData[3]);

                    var ToBollardKeyData = model.ToBollardKey().split('.');

                    model.ToPortCode(ToBollardKeyData[0]);
                    model.ToQuayCode(ToBollardKeyData[1]);
                    model.ToBerthCode(ToBollardKeyData[2]);
                    model.ToBollard(ToBollardKeyData[3]);
                   
                    if (self.IsUnique() == true) {
                        self.viewModelHelper.apiPost('api/BerthMaintenances', ko.mapping.toJSON(model), function Message(data) {                        
                            model.RecordStatus(data.RecordStatus);
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Berth Maintenance Details Saved Successfully", "Berth Maintenance");
                            self.LoadBerthMaintenence();
                            $('#spnTitile').html("Berth Maintenance");
                            self.viewMode('List');

                        });
                    }
                }
            }
            else {
                self.BerthMaintenanceValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
              //  $('#divValidationError').removeClass('display-none');
                return;
            }
        }


        self.ModifyBerthMaintenance = function (model) {
            debugger;
            model.validationEnabled(true);
            self.BerthMaintenanceValidation = ko.observable(model);
            self.BerthMaintenanceValidation().errors = ko.validation.group(self.BerthMaintenanceValidation());
            var errors = self.BerthMaintenanceValidation().errors().length;
            var duplicate = false;
            var bollardvalid = true;


            if (errors == 0) {
                //if (model.BollardsFrom() == model.BollardsTo()) {
                if (model.FromBollardKey() == model.ToBollardKey()) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.warning("Bollard From and Bollard To cannot be same");
                    self.berthmaintenanceModel().ToBollardKey("");
                    self.berthmaintenanceModel().ToBollardKey($("#BollardToId").val(""));
                    bollardvalid = false;
                }

                if (bollardvalid == true) {
                    self.viewModelHelper.apiPut('api/BerthMaintenances', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Berth Maintenance Details Updated Successfully", "Berth Maintenance");
                        $('#spnTitile').html("Berth Maintenance");
                        self.LoadBerthMaintenence();
                        self.viewMode('List');

                    });
                }            
            }
            else {
                self.BerthMaintenanceValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
               // $('#divValidationError').removeClass('display-none');
                return;
            }          
        }

  


        self.ResetBerthMaintenance = function (model) {
            debugger;
         if(self.IsSave() == true){
          //  $('#divValidationError').addClass('display-none');
            ko.validation.reset();
            model.validationEnabled(false);
            self.berthmaintenanceModel().reset();
            self.isBerthChanged(false);
           

         }
         if (self.IsUpdate() == true) {
             self.berthmaintenanceModel().reset();
             self.berthmaintenanceModel().BerthKey(self.BerthKy())
             self.LoadBerths(model.MaintPortCode(), model.MaintQuayCode(), model.MaintBerthCode());
             self.BollardList([{ FromBollardKey: model.FromPortCode() + '.' + model.FromQuayCode() + '.' + model.FromBerthCode() + '.' + model.FromBollard(), BollardName: model.BollardName() }]);
             self.ToBollardList([{ ToBollardKey: model.ToPortCode() + '.' + model.ToQuayCode() + '.' + model.ToBerthCode() + '.' + model.ToBollard(), BollardName: model.BollardName() }]);
             self.berthmaintenanceModel(model);
             self.berthmaintenanceModel().FromBollardKey(self.BollardList()[0].FromBollardKey);
             self.berthmaintenanceModel().ToBollardKey(self.ToBollardList()[0].ToBollardKey);

             $("#PeriodFrom").data('kendoDateTimePicker').enable(true);
             $("#PeriodTo").data('kendoDateTimePicker').enable(true);
         }

        }
        self.Cancel = function () {         
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                $('#spnTitile').html("Berth Maintenance");
                self.LoadBerthMaintenence();
                self.viewMode('List');
                self.berthmaintenanceModel().reset();
            }
        }

        ValidDate = function (data, event) {
            debugger;
            var some = JSON.parse(ko.toJSON(data));
            var periodf = some.PeriodFrom;
            var periodt = some.PeriodTo;
            var startDate = PeriodFrom.value;
            var endDate = PeriodTo.value;
            var PF = data.PeriodFrom();

            var myDatePicker = new Date(startDate);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours();
            var Mnt = myDatePicker.getMinutes() + 1;
            if (startDate) {

             //   $("#PeriodTo").data('kendoDateTimePicker').min(startDate);
                $("#PeriodTo").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));



                self.berthmaintenanceModel().PeriodTo($("#PeriodTo").val(""));
            }
            self.berthmaintenanceModel().PeriodFrom($("#PeriodFrom").val());
           self.berthmaintenanceModel().PeriodTo($("#PeriodTo").val());
        }

        
        NEWValidDate = function (data, event) {
            //    self.changeETAModel().NewETD(event.target.value);
            //    self.IsNewETDValid(false);
            self.berthmaintenanceModel().PeriodTo($("#PeriodTo").val());
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }


        self.viewWorkFlow = function (berthmaintenance) {
          //  alert('abc');
          
            debugger;
            var workflowinstanceId = berthmaintenance.WorkflowInstanceId();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {
                  debugger;
                  
                  self.berthmaintenanceModel(new IPMSROOT.BerthMaintenanceModel());
                  self.berthmaintenanceModel().WorkFlowRemarks(result);               
                  $('#WorkFlowRemarks').val(result);
                  $('#stack1').modal('show');            
                            
              });

        }




        self.WorkflowAction = function (dat) {           
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.berthmaintenanceModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }


        self.Initialize();
    }
    IPMSRoot.BerthMaintenanceViewModel = BerthMaintenanceViewModel;


}(window.IPMSROOT));
