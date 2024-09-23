(function (IPMSRoot) {
    var DepartureNoticeViewModel = function (viewDetail, DepartureID) {
        var self = this;
        $('#spnTitle').html("Departure Notice");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.IsEditableView = ko.observable(false);
        self.departureNoticeModel = ko.observable(new IPMSROOT.DepartureNoticeModel());
        self.LoadDepartureNoticeDetails = ko.observable();
        self.LoadPendingArrivalNotifications = ko.observable();
        self.viewDepartureNoticeData = ko.observable();

        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.DepartureNotice = ko.observableArray();
        self.DepartureNoticeData = ko.observableArray();
        self.getCurrentBerthsnBollards = ko.observableArray();
        self.DepartureNoticeIDLocal = ko.observable("");
        self.vcntype = ko.observable("");
        self.IsVesselEnable = ko.observable(false);
        self.isspanVCNSearchValid = ko.observable(false);
        self.isspanVesselSearchValid = ko.observable(false);
        //// For Initialization /////////////////////////
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadPendingArrivalNotificationsList();
            if (viewDetail == true) {
            }
            else {
                self.viewMode('List');
            }
            self.IsCodeEnable(true);
        }

        ////////////To load grid details in Departure Notice ....................
        self.LoadPendingArrivalNotificationsList = function () {
            
            if (viewDetail == true) {
               
               
                var vcn = self.departureNoticeModel().VCNSearch() != undefined && self.departureNoticeModel().VCNSearch() != '' ? self.departureNoticeModel().VCNSearch() : 'NA';
                var vesselName = self.departureNoticeModel().VesselNameSearch() != undefined && self.departureNoticeModel().VesselNameSearch() != '' ? self.departureNoticeModel().VesselNameSearch() : 'NA';
                var submissionDateFrom = '';
                var submissionDateTO = '';

                               
                self.viewModelHelper.apiGet('api/GetPendingArrivalNotifications', { DepartureID: DepartureID, VCN: vcn, VesselName: vesselName, SubmissionDateFrom: submissionDateFrom, SubmissionDateTO: submissionDateTO },
                      function (result) {
                          self.LoadDepartureNoticeDetails(ko.utils.arrayMap(result, function (item) {
                              return new IPMSRoot.DepartureNoticeModel(item);
                          }));
                          self.viewDepartureNotice(self.LoadDepartureNoticeDetails()[0]);
                      }, null, null, false);
            }
            else {
                var vcn = self.departureNoticeModel().VCNSearch() != undefined && self.departureNoticeModel().VCNSearch() != '' ? self.departureNoticeModel().VCNSearch() : 'NA';
                var vesselName = self.departureNoticeModel().VesselNameSearch() != undefined && self.departureNoticeModel().VesselNameSearch() != '' ? self.departureNoticeModel().VesselNameSearch() : 'NA';
                var submissionDateFrom = moment(self.departureNoticeModel().SubmissionDateFrom()).format('YYYY-MM-DD');
                var submissionDateTO = moment(self.departureNoticeModel().SubmissionDateTO()).format('YYYY-MM-DD');

                self.viewModelHelper.apiGet('api/GetPendingArrivalNotifications', { DepartureID: null, VCN: vcn, VesselName: vesselName, SubmissionDateFrom: submissionDateFrom, SubmissionDateTO: submissionDateTO },
                      function (result) {
                          self.LoadDepartureNoticeDetails(ko.utils.arrayMap(result, function (item) {
                              return new IPMSRoot.DepartureNoticeModel(item);
                          }));
                      }, null, null, false);
            }
        }

        ////////To save Departure Notice ...................
        self.SaveDepartureNotice = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            model.isValidationEnabled(true);

            self.DepartureNoticeValidation = ko.observable(model);
            self.DepartureNoticeValidation().errors = ko.validation.group(self.DepartureNoticeValidation());
            var errors = self.DepartureNoticeValidation().errors().length;

            if (errors == 0) {
                if (model.ATB() == "" || model.ATB() == null) {
                    toastr.warning("Arrival Service Request is not Completed.", "Departure Notice");
                    errors = 1;
                }
            }
            


            if (errors == 0) {
                self.viewModelHelper.apiPost('api/DepartureNotice', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.success("Departure notice details saved successfully and sent for acknowledgement.", "Departure Notice");
                                self.LoadPendingArrivalNotificationsList();
                                self.viewMode('List');
                                $('#spnTitle').html("Departure Notice");
                            });
            }
            else {
                self.DepartureNoticeValidation().errors.showAllMessages();
                return;
            }

        }

        ////////To Modify Departure Notice ..................
        self.ModifyDepartureNotice = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            model.isValidationEnabled(true);
            self.ServiceValidation = ko.observable(model);
            self.ServiceValidation().errors = ko.validation.group(self.ServiceValidation());
            var errors = self.ServiceValidation().errors().length;

            if (errors == 0) {
                if (model.ATB() == "" || model.ATB() == null) {
                    toastr.warning("Arrival Service Request is not Completed.", "Departure Notice");
                    errors = 1;
                }
            }

            if (errors == 0) {
                self.viewModelHelper.apiPut('api/DepartureNotice', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.success("Departure notice details updated successfully.", "Departure Notice");
                                self.LoadPendingArrivalNotificationsList();
                                self.viewMode('List');
                                $('#spnTitle').html("Departure Notice");
                            });
            }
            else {
                self.ServiceValidation().errors.showAllMessages();
                return;
            }
        }

        ///////// To Submit for Departure Notice details in Acknowledge mode ........
        self.acknowledgeDepartureNotice = function (departureNotice) {            
            $('#spnTitle').html("Acknowledge Departure Notice");
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsEditableView(false);
            self.IsVesselEnable(false);

            $("#wfview").show();
            self.departureNoticeModel(departureNotice);
            self.departureNoticeModel().isValidationEnabled(true);
            $("#EstSerReq").data('kendoDateTimePicker').enable(true);

            var myDatePicker = new Date();
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours() + 1;
            var Mnt = myDatePicker.getMinutes();
            $("#EstSerReq").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));

            
            
                self.departureNoticeModel().pendingTasks.removeAll();
                var ReferenceID = departureNotice.DepartureID();
                var WorkflowInstanceID = departureNotice.WorkflowInstanceId();

                if (ReferenceID > 0 && WorkflowInstanceID > 0) {
                    self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID, null,
                        function(result) {
                            ko.utils.arrayForEach(result, function(val) {
                                var pendingtaskaction = new IPMSROOT.pendingTask();
                                pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                pendingtaskaction.ReferenceID(val.ReferenceID);
                                pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                pendingtaskaction.APIUrl(val.APIUrl);
                                pendingtaskaction.TaskName(val.TaskName);
                                pendingtaskaction.TaskDescription(val.TaskDescription);
                                pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                pendingtaskaction.HasRemarks(val.HasRemarks);
                                self.departureNoticeModel().pendingTasks.push(pendingtaskaction);
                            });
                        });
                }
            
        }

        ///////// To Edit Departure Notice details in Edit mode ........
        self.editDepartureNotice = function (departureNotice) {
            $('#spnTitle').html("Edit Departure Notice");
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsEditableView(false);
            self.IsVesselEnable(false);

            $("#wfview").show();
            self.departureNoticeModel(departureNotice);
            self.departureNoticeModel().isValidationEnabled(true);

            $("#EstSerReq").data('kendoDateTimePicker').enable(true);
                        
            var myDatePicker = new Date();
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours() + 1;
            var Mnt = myDatePicker.getMinutes();
            $("#EstSerReq").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));

            self.departureNoticeModel().pendingTasks.removeAll();
            
        }

        //////// To View Departure Notice details in View mode .........
        self.viewDepartureNotice = function(departureNotice) {
            $('#spnTitle').html("View Departure Notice");
            self.viewMode('Form');
            self.departureNoticeModel(departureNotice);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            if (viewDetail == true) {
                $("#wfview").show();
                self.departureNoticeModel().pendingTasks.removeAll();
                var ReferenceID = departureNotice.DepartureID();
                var WorkflowInstanceID = departureNotice.WorkflowInstanceId();

                self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID, null,
                    function(result) {
                        ko.utils.arrayForEach(result, function(val) {
                            var pendingtaskaction = new IPMSROOT.pendingTask();
                            pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                            pendingtaskaction.ReferenceID(val.ReferenceID);
                            pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                            pendingtaskaction.APIUrl(val.APIUrl);
                            pendingtaskaction.TaskName(val.TaskName);
                            pendingtaskaction.TaskDescription(val.TaskDescription);
                            pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                            pendingtaskaction.HasRemarks(val.HasRemarks);
                            self.departureNoticeModel().pendingTasks.push(pendingtaskaction);
                        });
                    });
            }
        }

        ///////// For Reset functionality ...................
        self.ResetDepartureNotice = function (model) {

            self.departureNoticeModel().isValidationEnabled(false);
            ko.validation.reset();
            self.departureNoticeModel().reset();
        }

        /////// For Cancel Functionality....................
        self.Cancel = function (departureNotice) {

            if (viewDetail == true) {
                window.location.href = "/Welcome";
            }
            else {
                $('#spnTitle').html("Departure Notice");                
                self.LoadPendingArrivalNotificationsList();
                self.viewMode('List');
            }
        }

        self.dataLoad = function (event) {
            if (event.MovementDateTime() != undefined)
                $('#spanremarks').text('');
        }

        self.closePopup = function () {
            self.departureNoticeModel().workflowRemarks("");
            self.viewMode('List');
        }

        self.WorkflowAction = function (data) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(data);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (data.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(data, self.departureNoticeModel());
            }
            else {
                self.ENValidation().errors.showAllMessages();
            }
        }

        self.cancelWFRequest = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            if (model.workflowRemarks() == undefined) {
                $('#spanremarks').text('Please enter reason.');
                return;
            }
            self.viewModelHelper.apiPost('api/DepartureNotice/GridCancel', ko.mapping.toJSON(model),
                            function Message(data) {
                                toastr.success("Departure Notice request cancelled successfully.", "Departure Notice");
                                $(".close").trigger("click");
                                self.LoadPendingArrivalNotificationsList();
                                self.viewMode('List');
                            });
        }

        //Preventing Backspace
        PreventBackSpace = function (event) {
            
            CutPaste();
            var evt = event || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8 || keyCode === 46) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }

        self.SrearchDepatureDet = function () {
            var isnoError = true;
          
            var vcnSearch = self.departureNoticeModel().VCNSearch();
            var vesselName = self.departureNoticeModel().VesselNameSearch();
            var vcnSearchSelected = self.departureNoticeModel().VCNSelected();

            if (vcnSearch == "") {
                vcnSearch = "All";
                $("#spanVCNSearchValid").text('');
                self.isspanVCNSearchValid(false);
            }
            else {

                if (vcnSearchSelected != vcnSearch) {
                    isnoError = false;
                    $("#spanVCNSearchValid").text('Please select valid VCN');
                    self.isspanVCNSearchValid(true);
                }

             
            }
          
            if (vesselName == "") {
                vesselName = "All";
                $("#spanVesselSearchValid").text('');
                self.isspanVesselSearchValid(false);
            }
            else {

                if (self.departureNoticeModel().VesselNameSelected() != vesselName) {
                    isnoError = false; 
                    $("#spanVesselSearchValid").text('Please select valid Vessel Name/IMO No.');
                    self.isspanVesselSearchValid(true);
                }


            }
            if (isnoError) {
                viewDetail = false;
                
                self.LoadPendingArrivalNotificationsList();

                var grid = $("#divDepartureNoticeList").data("kendoGrid");

                if (self.LoadDepartureNoticeDetails().length <= 5)
                    grid.dataSource.pageSize(5);
                else
                    grid.dataSource.pageSize(20);

                grid.refresh();
              
               
            }
        }

        self.ResetSearchDet = function (data) {
            viewDetail = false;
            data.VCNSearch('');
            data.VesselNameSearch('')

            var todaydate = new Date();
            var todate = new Date(todaydate);
            var fromdate = new Date(todaydate);
            todate.setDate(todaydate.getDate() + 30);
            fromdate.setDate(fromdate.getDate() - 30);
            data.SubmissionDateFrom(fromdate ? (moment(fromdate).format('YYYY-MM-DD') || "") : "");
            data.SubmissionDateTO(todate ? (moment(todate).format('YYYY-MM-DD') || "") : "");


            self.isspanVCNSearchValid(false);
            self.isspanVesselSearchValid(false);
            self.LoadPendingArrivalNotificationsList();

            var grid = $("#divDepartureNoticeList").data("kendoGrid");

            if (self.LoadDepartureNoticeDetails().length <= 5)
                grid.dataSource.pageSize(5);
            else
                grid.dataSource.pageSize(20);

            grid.refresh();
        }

        ValidDate = function () {
            self.departureNoticeModel().SubmissionDateTO(self.departureNoticeModel().SubmissionDateFrom());
        }


        self.viewWorkFlow = function (departurenotice) {
            var workflowinstanceId = departurenotice.WorkflowInstanceId();

            if (workflowinstanceId == "") {
                $('#WorkFlowRemarks').text('No Remarks');
                $('#stack1').modal('show');
            }
            else {
                self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
                 { workflowinstanceId: workflowinstanceId },
                  function (result) {
                      self.departureNoticeModel(new IPMSROOT.DepartureNoticeModel());
                      self.departureNoticeModel().WorkFlowRemarks(result);
                      $('#WorkFlowRemarks').text(result);
                      $('#stack1').modal('show');
                  });
            }
        }
        //-------------------------------------------------------
        SerchVesselBackSpace = function (e) {

            self.departureNoticeModel().VesselNameSearch('');
        }
        SerchVCNBackSpace = function (e) {

            self.departureNoticeModel().VCNSearch('');
        }

        SerchVCNBackSpaceNumValid = function (evt) {


            self.departureNoticeModel().VCNSearch('');

            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
       
        VCNonblur = function (e) {
            

            var vcnblur = $("#VCNName").val();
            self.departureNoticeModel().VCNSearch(vcnblur);



        }
        Vesselonblur = function (e) {
            var vesselblur = $("#VesselName1").val();
            self.departureNoticeModel().VesselNameSearch(vesselblur);

        }
        self.VCNSelectSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.departureNoticeModel().VCNSelected(selecteddataItem.vcn);            
            self.isspanVCNSearchValid(false);
            $("#spanVCNSearchValid").text('');
        }

        self.VesselSelectSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.departureNoticeModel().VesselNameSelected(selecteddataItem.VesselName);
            self.isspanVesselSearchValid(false);
            $("#spanVesselSearchValid").text('');

        }
        





        //-------------------------------------------------------

        self.Initialize();
    }
    IPMSRoot.DepartureNoticeViewModel = DepartureNoticeViewModel;

}(window.IPMSROOT));
