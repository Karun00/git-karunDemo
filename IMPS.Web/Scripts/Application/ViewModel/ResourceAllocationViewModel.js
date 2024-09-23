(function (IPMSRoot) {
    var isView = 0;

    var ResourceAllocationViewmodel = function () {
       
        var self = this;
        $('#spnTitle').html("Service Recording");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        //self.dateFormat = new IPMSROOT.dateFormat();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.resourceallocationModel = ko.observable(new IPMSROOT.ResourceAllocationModel());
        self.viewMode = ko.observable(true);
        self.resourceallocationList = ko.observableArray([]);        
        self.referenceData = ko.observable();
        self.fromblordreferenceData = ko.observable();
        self.toblordreferenceData = ko.observable();
        self.IsCodeEnable = ko.observable(true);
        self.IsSave = ko.observable(true);
        self.IsResetEnable = ko.observable(true);
        self.VisibleServiceRecording = ko.observable(false);
        self.BerthsDetails = ko.observableArray([]);
        self.isstarttimeMsg = ko.observable(false);
        self.isPilotagestarttime = ko.observable(false);
        self.isfloatingcranestarttimeMsg = ko.observable(false);
        self.istugwaterstarttimeMsg = ko.observable(false);
        self.ispilothelicopterstarttimeMsg = ko.observable(false);
        self.iswaterservicestarttimeMsg = ko.observable(false);
        self.isBackToQA = ko.observable(false);
        self.isEndTime = ko.observable(false);
        self.IsWaterSave=ko.observable(false);
        self.isspanVCNSearchValid = ko.observable(false);
        self.isspanVesselSearchValid = ko.observable(false);
        self.isspanResourceSearchValid = ko.observable(false);
        self.IsBerthEnable = ko.observable(false);
        self.IsWaterResourceEnable = ko.observable(true);       
        self.WaterResourceList = ko.observableArray([]);
        $('#divWaterServiceList1').hide();
        self.IsComp = ko.observable(false);
        self.IsWaterEdit = ko.observable(false);
        self.IsWaterView = ko.observable(false);
        self.IsResEdit_Water = ko.observable(false);
        self.IsResView_Water = ko.observable(false);
        self.isSpanDelay = ko.observable(false);
        self.isSpanMopsDelay = ko.observable(false); 

        self.Initialize = function () {
            self.resourceallocationModel(new IPMSROOT.ResourceAllocationModel());
            self.LoadResourceAllacationMaster();
            self.LoadInitialData();         
            self.viewMode('List');
            self.GetBerths();
        }
        //dateTimePicker1.Value.ToString("MM/dd/yyyy")
        //self.dateFormat = self.IPMSDateFormat = ko.observable('dd-MM-yyyy');
        self.getval = function (data,event)
        
       // function getval(data)
        {
            
            $('#delay').val("");
            $('#actualtime').val();
            self.resourceallocationModel();
            self.resourceallocationModel().PilotageServiceRecording().IsValidationEnabled(false);
            //self.resourceallocationModel().PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
            //            return item.rule = "required";
            //});
            //data.PilotageServiceRecording().IsValidationEnabled(false);
            //self.resour.
           // self.resourceallocationModel.MOPSDelay.message.remove();
          //  var dateformatConfig = "yyyy-MM-dd HH:mm";
          // var abc= self.resourceallocationModel();
          // var myDate = $("#PilotonBoard").val().toString("yyyy/MM/dd");
          //var dt= kendo.toString(myDate, dateformatConfig);
          ////var myDate1 = kendo.parseDate(myDate, self.dateFormat());
          //alert($.datepicker.formatDate('dd/MM/yy', new Date(myDate)));
            //alert(myDate1);
            //var actualtime = new Date(ptstDatestrings2);
            ////var date=date1.setMinutes(date1.getMinutes() + 30);
            //var pilottime = new Date(ptobDatestring1);
            //// var pilotdate = pilottime.setMinutes(pilottime.getMinutes() + 30);
            //var timedifference = (new Date(pilottime) - new Date(actualtime));
            //var resultInMinutes = Math.round(timedifference / 60000);
            ////var timedifference =date2.getMinutes()-date1.getMinutes();
            ////alert(resultInMinutes);
            //if (resultInMinutes > 30) {

            //    if ($('#MOPSDelay').val() == "") {

            //        data.PilotageServiceRecording().IsValidationEnabled(true);

            //        //self.isSpanDelay(true);

            //        //alert('Please complete the field');
            //    }
            //}
            //else {

            //    //data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
            //    //    return item.rule = "required";
            //    //});

            //    errors = 0;
            //    data.PilotageServiceRecording().IsValidationEnabled(false);
            //    self.isSpanDelay(false);

            //    // $('#delay').val("");


            //}


        }
        //$('#PilotonBoard').on('change', function () {
        //    alert("fdjfkdsjf");
        //});
        //debugger;
        //$('#PilotonBoard').kendoDatePicker({
        //    change: function () {
        //        alert("hkdfdh");
        //        //if (data.OperationType() == 'PILT') {

        //        var actualtime = new Date(ptstDatestrings2);
        //        //var date=date1.setMinutes(date1.getMinutes() + 30);
        //        var pilottime = new Date(ptobDatestring1);
        //        // var pilotdate = pilottime.setMinutes(pilottime.getMinutes() + 30);
        //        var timedifference = (new Date(pilottime) - new Date(actualtime));
        //        var resultInMinutes = Math.round(timedifference / 60000);
        //        //var timedifference =date2.getMinutes()-date1.getMinutes();
        //        //alert(resultInMinutes);
        //        if (resultInMinutes > 30) {

        //            if ($('#MOPSDelay').val() == "") {

        //                data.PilotageServiceRecording().IsValidationEnabled(true);

        //                //self.isSpanDelay(true);

        //                //alert('Please complete the field');
        //            }
        //        }
        //        else {

        //            //data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
        //            //    return item.rule = "required";
        //            //});

        //            errors = 0;
        //            data.PilotageServiceRecording().IsValidationEnabled(false);
        //            self.isSpanDelay(false);

        //            // $('#delay').val("");


        //        }


        //        //  }

        //    }

        //});

        //Delay Reasons code
        self.SelectedDelayReasons = function (value) {

            if (value.DelayReason != 'OTER')
            {
                value.DelayOtherReason(null);
            }

        }



        self.GetBerths = function () {
            self.viewModelHelper.apiGet('api/MobileScheduledTasks/GetBerths', null,
         function (result1) {
             self.BerthsDetails(result1);
         }, null, null, false);
        }

        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/ResourceallocationReferenceData', null,
                    function (result1) {
                        self.referenceData(new IPMSRoot.ReferenceData(result1));
                    }, null, null, false);
            self.fromblordreferenceData(new IPMSRoot.FromblordreferenceData(undefined));
            self.toblordreferenceData(new IPMSRoot.ToblordreferenceData(undefined));
        }
        //GetResourceallocationformdetails
      
        self.GetPostedDataForWaterService = function (data) {
            
            self.IsWaterSave(true);          
            var resouceAllocationID = data.ResourceAllocationID();
            var act = data.action;
            self.viewModelHelper.apiGet('api/GetWaterDetailsList/' + resouceAllocationID +'/'+ act , null,
                function (result1) {
                  
                    if (result1.length == 0) {
                        $('#divWaterServiceList1').hide();
                       
                    }
                    else {                       
                        $('#divWaterServiceList1').show();                       
                        self.WaterResourceList(ko.utils.arrayMap(result1, function (item) {
                            return new IPMSRoot.OtherServiceRecordingDeatils(item);
                        }));
                    }
                }, null, null, false);
          
        }

        self.LoadResourceAllacationMaster = function () {           
            var vcn = self.resourceallocationModel().VCNSearch() != undefined && self.resourceallocationModel().VCNSearch() != '' ? self.resourceallocationModel().VCNSearch() : 'NA';
            var vesselName = self.resourceallocationModel().VesselNameSearch() != undefined && self.resourceallocationModel().VesselNameSearch() != '' ? self.resourceallocationModel().VesselNameSearch() : 'NA';
           
            var resourceName = self.resourceallocationModel().ResourceName() != undefined && self.resourceallocationModel().ResourceName() != '' ? self.resourceallocationModel().ResourceName() : 'NA';

            self.viewModelHelper.apiGet('api/GetResourceallocationList/' + vcn + '/' + vesselName + '/' + resourceName, null,
                function (result) {                  
                    self.resourceallocationList(ko.utils.arrayMap(result, function (item) {                   
                        return new IPMSRoot.ResourceAllocationModel(item);
                        self.resourceallocationModel(new IPMSRoot.OtherServiceRecordingDetails(item));
                    }));
                }, null, null, false);
        }
        
        self.GetResourceallocationformdetails = function (data) {
        
            self.viewModelHelper.apiPost('api/GetFormDetails', ko.mapping.toJSON(data), function (result1) {
                 
                if (data.OperationType() == "BRTH" || data.OperationType() == "SHFT") {
                        
                    // From Bollard
                    if (result1.ShiftingBerthingTaskExecution.FromBerthKey != "" && result1.ShiftingBerthingTaskExecution.FromBerthKey != null) {
                        self.viewModelHelper.apiGet('api/GetBollardsInBerthsDetails', { Id: result1.ShiftingBerthingTaskExecution.FromBerthKey },
                             function (result2) {
                                 self.fromblordreferenceData(new IPMSRoot.FromblordreferenceData(result2));
                             }, null, null, false);
                    }

                    // To Bollard
                    if (result1.ShiftingBerthingTaskExecution.ToBerthKey != "" && result1.ShiftingBerthingTaskExecution.ToBerthKey != null) {
                        $('#divWaterServiceList1').hide();
                        self.viewModelHelper.apiGet('api/GetBollardsInBerthsDetails', { Id: result1.ShiftingBerthingTaskExecution.ToBerthKey },
                               function (result3) {
                                   self.toblordreferenceData(new IPMSRoot.ToblordreferenceData(result3));
                               }, null, null, false);
                    }
                    result1.VCN = data.VCN();
                    result1.VesselName = data.VesselName();
                    result1.FirstName = data.FirstName();
                    result1.VesselLength = data.VesselLength();
                    self.resourceallocationModel(new IPMSRoot.ResourceAllocationModel(result1));
                }
                else {                    
                    result1.VCN = data.VCN();
                    result1.FirstName = data.FirstName();
                    result1.VesselName = data.VesselName();
                    result1.VesselLength = data.VesselLength();
                    result1.BerthName = data.BerthName();

                    if (data.OperationType() == "FCST") {
                        if (result1.OtherServiceRecording.Extend) {
                            self.isEndTime(false);
                            self.isBackToQA(true);
                        }
                        else {
                            self.isEndTime(true);
                            self.isBackToQA(false);
                        }
                    }
                   
                    self.resourceallocationModel(new IPMSRoot.ResourceAllocationModel(result1));
                  
                    if (data.OperationType() == 'WTST') {
                        
                        var ac = sessionStorage.getItem("action");
                        sessionStorage.removeItem("action");
                        var count = sessionStorage.getItem("count");
                        sessionStorage.removeItem("count");
                        if (count == null) {
                            count = 1;
                        }

                        if (self.resourceallocationModel().OtherServiceRecording().IsCompleted() == "Yes") {
                            $('#idIsCompleted').prop('disabled', true);
                            self.resourceallocationModel().OtherServiceRecording().IsCompleted(true);
                        }
                        else if (self.resourceallocationModel().OtherServiceRecording().IsCompleted() == "No" && ac != "View") {
                            $('#idIsCompleted').prop('disabled', false);
                            self.resourceallocationModel().OtherServiceRecording().IsCompleted(false);
                            self.resourceallocationModel().OtherServiceRecording().StartTime('');
                            self.resourceallocationModel().OtherServiceRecording().EndTime('');
                            self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
                            self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
                            self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
                            self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
                            self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
                            self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                            self.resourceallocationModel().OtherServiceRecording().MeterNo('');
                            self.resourceallocationModel().Remarks('');
                            self.resourceallocationModel().DelayReason('');
                            $('#totaldispensed').val('');
                            $('#DelayReason').val('');
                            self.resourceallocationModel().Deficiencies('');

                        }
                        else if (self.resourceallocationModel().OtherServiceRecording().IsCompleted() == "No" && ac == "View") {
                            $('#idIsCompleted').prop('disabled', true);
                            self.resourceallocationModel().OtherServiceRecording().IsCompleted(false);
                            //self.resourceallocationModel().OtherServiceRecording().StartTime('');
                            //self.resourceallocationModel().OtherServiceRecording().EndTime('');
                            //self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
                            //self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
                            //self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
                            //self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
                            //self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
                            //self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                            //self.resourceallocationModel().Remarks('');
                            //self.resourceallocationModel().DelayReason('');
                            //$('#totaldispensed').val('');
                            //$('#DelayReason').val('');
                            //self.resourceallocationModel().Deficiencies('');

                        }
                        //$('#section7').show();
                        //self.resourceallocationModel().OtherServiceRecording().StartTime('');
                        //self.resourceallocationModel().OtherServiceRecording().EndTime('');
                        //self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
                        //self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
                        //self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
                        //self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
                        //self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
                        //self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                        //self.resourceallocationModel().Remarks('');
                        //self.resourceallocationModel().DelayReason('');
                        //$('#totaldispensed').val('');
                        //$('#DelayReason').val('');
                        //self.resourceallocationModel().Deficiencies('');
                    }
                    //else {
                    //    self.resourceallocationModel().OtherServiceRecording().StartTime('');
                    //    self.resourceallocationModel().OtherServiceRecording().EndTime('');
                    //    self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
                    //    self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
                    //    self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
                    //    self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
                    //    self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
                    //    self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                    //    self.resourceallocationModel().Remarks('');
                    //    self.resourceallocationModel().DelayReason('');
                    //    $('#totaldispensed').val('');
                    //    $('#DelayReason').val('');
                    //    self.resourceallocationModel().Deficiencies('');
                    //}
                   
                

                }
            }, null, null, false);

            //}
        }

        self.GetFromBollarddetails = function (data) {
            self.viewModelHelper.apiGet('api/GetBollardsInBerthsDetails', { Id: data.FromBerthKey() },
                  function (result) {
                      self.fromblordreferenceData(new IPMSRoot.FromblordreferenceData(result));
                  }, null, null, false);
        }

        self.GetToBollarddetails = function (data) {
            self.viewModelHelper.apiGet('api/GetBollardsInBerthsDetails', { Id: data.ToBerthKey() },
                  function (result) {
                      self.toblordreferenceData(new IPMSRoot.ToblordreferenceData(result));
                  }, null, null, false);
        }

        // This is used to View automated slot config
        self.viewResourceAllacation = function (modeldata) {
            
            if (modeldata.TaskStatus() == 'COMP') {
                self.IsComp(true);
            }
            self.IsResetEnable(false);
            self.IsWaterSave(false);
            $('#divWaterServiceList1').hide();
            modeldata.action = "View";
            if (modeldata.TaskStatus() == 'CFRI') {
                // confirmation box - start
                $.confirm({
                    'title': ' Task acknowledged confirmation',
                    'message': 'The Task is not yet acknowledged by the Resource.Would you like to continue with the Execution details?',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {
                                self.IsCodeEnable(true);
                                var varResourceAllocationVO = {
                                    ResourceAllocationID: modeldata.ResourceAllocationID(),
                                    Remarks: 'Acknowledged By VTC',
                                    TaskStatus: 'ACCP',
                                    OperationType: modeldata.OperationType
                                };

                                $.ajax({
                                    type: "PUT",
                                    url: '/api/MobileScheduledTasks/PutScheduledTasks',
                                    data: varResourceAllocationVO,
                                    dataType: 'json',
                                    success: function (data) {
                                        self.viewModelHelper.apiGet('api/GetResourcealocationByResourceAllocID', { Id: modeldata.ResourceAllocationID() },
                                        function (result) {
                                            self.updatedData = ko.observable();
                                            self.updatedData = new IPMSROOT.ResourceAllocationModel(result);                                            
                                            if (result.OperationType == "WTST") {
                                                
                                                $('#divWaterServiceList1').hide();
                                                sessionStorage.setItem("action", "View");
                                                self.IsCodeEnable(false);
                                                self.VisibleServiceRecording(false);                                                
                                                self.GetPostedDataForWaterService(modeldata);
                                                self.GetResourceallocationformdetails(modeldata);                                                
                                                self.IsWaterResourceEnable(false);                                               
                                                $('#idIsCompleted').prop('disabled', true);
                                                self.IsWaterSave(false);
                                                self.IsSave(false);
                                                self.viewMode('Form');
                                                self.resourceallocationModel(new IPMSROOT.ResourceAllocationModel());
                                                self.LoadResourceAllacationMaster();
                                                self.LoadInitialData();
                                            }
                                            else {
                                                $('#spnTitle').html("View Service Recording");
                                                self.GetResourceallocationformdetails(self.updatedData);
                                                self.IsSave(false);
                                                self.IsWaterSave(false);
                                                self.VisibleServiceRecording(false);
                                                self.viewMode('Form');
                                                self.IsCodeEnable(false);
                                                self.resourceallocationModel(new IPMSROOT.ResourceAllocationModel());
                                                self.LoadResourceAllacationMaster();
                                                self.LoadInitialData();
                                            }
                                        }, null, null, false);
                                    }
                                });
                            }
                        },
                        'No': {
                            'class': 'gray',
                            'action': function () {
                                // $("#port").val(decryptresult);
                                return false;
                            }
                        }
                    }
                });
                //confirmation box - end
            }
            else {
                $('#spnTitle').html("View Service Recording");
                if (modeldata.OperationType() == "WTST") {
                    self.IsWaterResourceEnable(false);
                    self.IsBerthEnable(false);
                    self.VisibleServiceRecording(false);
                    var action = "View";
                    self.GetPostedDataForWaterService(modeldata,action);
                    self.GetResourceallocationformdetails(modeldata);                
                    self.IsWaterSave(false);
                    self.IsSave(false);
                    self.viewMode('Form');
                    sessionStorage.setItem("action", "View");
                    self.IsCodeEnable(false);
                    self.LoadInitialData();
                }
                else {
                    $('#divWaterServiceList1').hide();
                    self.GetResourceallocationformdetails(modeldata);
                    self.IsSave(false);
                    self.IsWaterSave(false);
                    self.VisibleServiceRecording(false);
                    self.viewMode('Form');
                    self.IsCodeEnable(false);
                    self.LoadInitialData();
                }
               
            }
        }

        self.viewWaterAllacation = function (data) {
            
            sessionStorage.setItem("action", "View");
            self.IsCodeEnable(false);
            self.IsWaterResourceEnable(false);           
            self.IsResetEnable(true);
            self.IsBerthEnable(false);
            self.IsTop = false;
            self.IsWaterSave(false);
            self.IsSave(false);
            self.VisibleServiceRecording(false);
           
           
            $("#divWaterServiceList1").show();
            $('#spnTitle').html("View Service Recording");
            if (data.IsCompleted() == "Yes") {
                self.resourceallocationModel().OtherServiceRecording().IsCompleted(true);
            }
            else {
                self.resourceallocationModel().OtherServiceRecording().IsCompleted(false);
            }
                self.resourceallocationModel().OtherServiceRecording().StartTime(data.StartTime());
                self.resourceallocationModel().OtherServiceRecording().EndTime(data.EndTime());
                self.resourceallocationModel().OtherServiceRecording().WaitingStartTime(data.WaitingStartTime());
                self.resourceallocationModel().OtherServiceRecording().WaitingEndTime(data.WaitingEndTime());                
                self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading(data.OpeningMeterReading());
                self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading(data.ClosingMeterReading());
                self.resourceallocationModel().OtherServiceRecording().TotalDispensed(data.TotalDispensed());
                self.resourceallocationModel().OtherServiceRecording().MeterNo(data.MeterNo());
                self.resourceallocationModel().OtherServiceRecording().ActualScheduledTime(data.ActualScheduledTime());
                $('#DelayReason').val(data.DelayReason());
                $('#Remarks').val(data.Remarks());
                $('#Deficiencies').val(data.Deficiencies());                    
           
        }
       


        self.cancel = function () {
         
            self.LoadInitialData();
            self.viewMode('List');

            self.resourceallocationModel().reset();            
           
            $('#spnTitle').html("Service Recording");
            
        }

        self.Reset = function (model) {
            
            var ac = sessionStorage.getItem("action");
            sessionStorage.removeItem("action");
            self.IsCodeEnable(true);
           
            //if (self.resourceallocationModel().TaskStatus() == "ACCP") {
            //    if (ac == "View") {
            //        self.resourceallocationModel().ResourceName('');
            //        self.resourceallocationModel().OtherServiceRecording().StartTime('');
            //        self.resourceallocationModel().OtherServiceRecording().EndTime('');
            //        self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
            //        self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
            //        self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
            //        self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
            //        self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
            //        self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
            //        self.resourceallocationModel().Remarks('');
            //        self.resourceallocationModel().DelayReason('');
            //        $('#totaldispensed').val('');
            //        $('#DelayReason').val('');
            //        self.resourceallocationModel().Deficiencies('');
            //        self.IsSave(false);
            //        self.IsWaterSave(false);
            //    }
            //    else if (ac == "Verify") {
            //        self.resourceallocationModel().ResourceName('');
            //        self.resourceallocationModel().OtherServiceRecording().StartTime('');
            //        self.resourceallocationModel().OtherServiceRecording().EndTime('');
            //        self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
            //        self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
            //        self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
            //        self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
            //        self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
            //        self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
            //        self.resourceallocationModel().Remarks('');
            //        self.resourceallocationModel().DelayReason('');
            //        $('#totaldispensed').val('');
            //        $('#DelayReason').val('');
            //        self.resourceallocationModel().Deficiencies('');
            //        self.IsSave(false);
            //        self.IsWaterSave(false);
            //        self.VerifyServiceRecording(true);
            //    }
            //    else if (ac == "Edit") {
            //        self.resourceallocationModel().ResourceName('');
            //        self.resourceallocationModel().OtherServiceRecording().StartTime('');
            //        self.resourceallocationModel().OtherServiceRecording().EndTime('');
            //        self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
            //        self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
            //        self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
            //        self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
            //        self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
            //        self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
            //        self.resourceallocationModel().Remarks('');
            //        self.resourceallocationModel().DelayReason('');
            //        $('#totaldispensed').val('');
            //        $('#DelayReason').val('');
            //        self.resourceallocationModel().Deficiencies('');
            //        if (model.OtherServiceRecording().IsCompleted() == false) {
            //            self.IsSave(false);
            //            self.IsWaterSave(true);
            //            self.VerifyServiceRecording(false);
            //        }

            //    }

            //}
            if (self.resourceallocationModel().TaskStatus() == "COMP" || self.resourceallocationModel().TaskStatus() == "VERF") {
                
                    self.IsWaterSave(false);
                    self.IsSave(false);
                    self.resourceallocationModel().reset();
            }
            else {
                self.IsWaterSave(true);
                self.IsSave(false);
                self.resourceallocationModel().reset();
            }
            if (ac == "View") {
                if (self.resourceallocationModel().TaskStatus()=="COMP" ||self.resourceallocationModel().TaskStatus()=="VERF") {
                    self.IsWaterSave(false);
                    self.IsSave(false);
                    self.resourceallocationModel().reset();
                    //self.resourceallocationModel(model);
                }
                else {
                    self.IsWaterSave(true);
                    self.IsSave(false);
                    self.resourceallocationModel().reset();
                }
                //self.IsWaterSave(false);
                //self.IsSave(false);
            }
                self.resourceallocationModel().ResourceName('');
                self.resourceallocationModel().OtherServiceRecording().StartTime('');
                self.resourceallocationModel().OtherServiceRecording().EndTime('');
                self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
                self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
                self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
                self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
                self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
                self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                self.resourceallocationModel().Remarks('');
                self.resourceallocationModel().DelayReason('');
                $('#totaldispensed').val('');
                $('#DelayReason').val('');
                self.resourceallocationModel().Deficiencies('');




        }
       
        self.SaveResourceAllacation = function (data) {
            var dateformatConfig = "yyyy-MM-dd HH:mm";
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            var errors = 0;
            if (data.MovementType() == 'ARMV' && data.OperationType() == 'BRTH') {
                data.ShiftingBerthingTaskExecution().FirstLineOut.rules.remove(function (item) {
                    return item.rule = "required";
                });
                data.ShiftingBerthingTaskExecution().LastLineOut.rules.remove(function (item) {
                    return item.rule = "required";
                });

                data.ShiftingBerthingTaskExecution().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.ShiftingBerthingTaskExecution());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;

                //toastr.errors(errors, "error");
            }
            else if ((data.MovementType() == 'WRMV' || data.MovementType() == 'SHMV') && data.OperationType() == 'BRTH') {
                data.ShiftingBerthingTaskExecution().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.ShiftingBerthingTaskExecution());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
            }
            else if ((data.MovementType() == 'SGMV') && data.OperationType() == 'BRTH') {
                data.ShiftingBerthingTaskExecution().FirstLineIn.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().LastLineIn.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().FromBerthKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().FromBolardKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().ToBerthKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().ToBolardKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().MooringBolardBowKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().MooringBolardStemKey.extend({ validatable: false });

                data.ShiftingBerthingTaskExecution().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.ShiftingBerthingTaskExecution());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
            }
           
            else if (data.OperationType() == 'PILT') {
                
                //data.PilotageServiceRecording().IsValidationEnabled(true);
                ////data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
                ////    return item.rule = "required";
                ////});
                //self.ResourceValidation = ko.observable(data.PilotageServiceRecording());
                //self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                //errors = self.ResourceValidation().errors().length;

             

                //anushastart 28/005/2024

                data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
                    return item.rule = "required";
                });
                data.PilotageServiceRecording().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.PilotageServiceRecording());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
                //end
            }
            else if (data.OperationType() == 'FCST') {
                data.OtherServiceRecording().LineUp.extend({ validatable: false });
                data.OtherServiceRecording().LineDown.extend({ validatable: false });
                data.OtherServiceRecording().PilotOn.extend({ validatable: false });
                data.OtherServiceRecording().OpeningMeterReading.extend({ validatable: false });
                data.OtherServiceRecording().ClosingMeterReading.extend({ validatable: false });
                data.OtherServiceRecording().TotalDispensed.extend({ validatable: false });
                data.OtherServiceRecording().MeterNo.extend({ validatable: false });

                if (data.OtherServiceRecording().Extend()) {
                    data.OtherServiceRecording().EndTime.extend({ validatable: false });
                }
                else {
                    data.OtherServiceRecording().BackToQuay.extend({ validatable: false });
                }

                data.OtherServiceRecording().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.OtherServiceRecording());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
            }
            else if (data.OperationType() == 'PIHE') {
                data.PilotageServiceRecording().WaitingStartTime.extend({ validatable: false });
                data.PilotageServiceRecording().WaitingEndTime.extend({ validatable: false });
                data.PilotageServiceRecording().AdditionalTugs.extend({ validatable: false });
                data.PilotageServiceRecording().OffSteam.extend({ validatable: false });
                data.PilotageServiceRecording().MarineRevenueCleared.extend({ validatable: false });
                data.PilotageServiceRecording().IsValidationEnabled(true);

                data.PilotageServiceRecording().ActualScheduledTime.rules.remove(function (item) {
                    return item.rule = "required";
                });
                data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
                    return item.rule = "required";
                });
                self.ResourceValidation = ko.observable(data.PilotageServiceRecording());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
                //data.OtherServiceRecording().LineUp.extend({ validatable: false });
                //data.OtherServiceRecording().LineDown.extend({ validatable: false });
                //data.OtherServiceRecording().FirstSwing.extend({ validatable: false });
                //data.OtherServiceRecording().OpeningMeterReading.extend({ validatable: false });
                //data.OtherServiceRecording().ClosingMeterReading.extend({ validatable: false });
                //data.OtherServiceRecording().TotalDispensed.extend({ validatable: false });
                //data.OtherServiceRecording().LastSwing.extend({ validatable: false });
                //data.OtherServiceRecording().TimeAlongSide.extend({ validatable: false });
                //data.OtherServiceRecording().BackToQuay.extend({ validatable: false });

                //data.OtherServiceRecording().IsValidationEnabled(true);
                //self.ResourceValidation = ko.observable(data.OtherServiceRecording());
                //self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                //errors = self.ResourceValidation().errors().length;
            }
            else if (data.OperationType() == 'TGWR') {               
                data.OtherServiceRecording().PilotOn.extend({ validatable: false });
                data.OtherServiceRecording().OpeningMeterReading.extend({ validatable: false });
                data.OtherServiceRecording().ClosingMeterReading.extend({ validatable: false });
                data.OtherServiceRecording().TotalDispensed.extend({ validatable: false });
                data.OtherServiceRecording().FirstSwing.extend({ validatable: false });
                data.OtherServiceRecording().LastSwing.extend({ validatable: false });
                data.OtherServiceRecording().TimeAlongSide.extend({ validatable: false });
                data.OtherServiceRecording().BackToQuay.extend({ validatable: false });                
                data.OtherServiceRecording().MeterNo.extend({ validatable: false });
                data.OtherServiceRecording().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.OtherServiceRecording());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
            }
            else if (data.OperationType() == 'WTST') {
                data.OtherServiceRecording().LineUp.extend({ validatable: false });
                data.OtherServiceRecording().LineDown.extend({ validatable: false });
                data.OtherServiceRecording().PilotOn.extend({ validatable: false });
                data.OtherServiceRecording().FirstSwing.extend({ validatable: false });
                data.OtherServiceRecording().LastSwing.extend({ validatable: false });
                data.OtherServiceRecording().TimeAlongSide.extend({ validatable: false });
                data.OtherServiceRecording().BackToQuay.extend({ validatable: false });

                data.OtherServiceRecording().IsValidationEnabled(true);
                //if (data.OtherServiceRecording().OpeningMeterReading() == "") {

                //}

                var OpeningMeterReading = data.OtherServiceRecording().OpeningMeterReading();
                var ClosingMeterReading = data.OtherServiceRecording().ClosingMeterReading();
                if ((data.OtherServiceRecording().OpeningMeterReading()) != "" && (data.OtherServiceRecording().ClosingMeterReading()) != "") {
                    if (parseFloat(ClosingMeterReading) > parseFloat(OpeningMeterReading)) {
                        var ttlDispensed = (ClosingMeterReading - OpeningMeterReading).toFixed(2);
                        data.OtherServiceRecording().TotalDispensed(ttlDispensed);// = ttlDispensed;
                        $('#totaldispensed').val(ttlDispensed);
                    }
                    else {
                        toastr.warning("Closing meter reading should be greater than Opening meter reading", "Service Recording");
                        data.OtherServiceRecording().ClosingMeterReading('');
                        data.OtherServiceRecording().TotalDispensed('');
                        //self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
                        //$('#openingmeterreading').val('');
                        $('#closingmeterreading').val('');
                        $('#totaldispensed').val('');
                        errors = errors + 1;
                        return false;
                    }

                }
                    self.ResourceValidation = ko.observable(data.OtherServiceRecording());
                    self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                    errors = self.ResourceValidation().errors().length;                
            }
           
            var resourceStDateobj = kendo.parseDate(data.StartTime(), dateformatConfig);
            var resourceStDatestring = kendo.toString(resourceStDateobj, dateformatConfig);
            data.StartTime(resourceStDatestring);

            var resourceEtDateobj = kendo.parseDate(data.EndTime(), dateformatConfig);
            var resourceEtDatestring = kendo.toString(resourceEtDateobj, dateformatConfig);
            data.EndTime(resourceEtDatestring);

            var sbstDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().StartTime(), dateformatConfig);
            var sbstDatestring = kendo.toString(sbstDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().StartTime(sbstDatestring);

            var sbetDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().EndTime(), dateformatConfig);
            var sbetDatestring = kendo.toString(sbetDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().EndTime(sbetDatestring);

            var sbfliDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().FirstLineIn(), dateformatConfig);
            var sbfliDatestring = kendo.toString(sbfliDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().FirstLineIn(sbfliDatestring);

            var sblliDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().LastLineIn(), dateformatConfig);
            var sblliDatestring = kendo.toString(sblliDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().LastLineIn(sblliDatestring);

            var sbfloDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().FirstLineOut(), dateformatConfig);
            var sbfloDatestring = kendo.toString(sbfloDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().FirstLineOut(sbfloDatestring);

            var sblloDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().LastLineOut(), dateformatConfig);
            var sblloDatestring = kendo.toString(sblloDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().LastLineOut(sblloDatestring);
          
            var ptstDateobj = kendo.parseDate(data.PilotageServiceRecording().StartTime(), dateformatConfig);
            var ptstDatestring = kendo.toString(ptstDateobj, dateformatConfig);
            data.PilotageServiceRecording().StartTime(ptstDatestring);
           
            var ptstDateobjs2 = kendo.parseDate(data.PilotageServiceRecording().ActualScheduledTime(), dateformatConfig);
            var ptstDatestrings2 = kendo.toString(ptstDateobjs2, dateformatConfig);
            data.PilotageServiceRecording().ActualScheduledTime(ptstDatestrings2);

            var ptetDateobj = kendo.parseDate(data.PilotageServiceRecording().EndTime(), dateformatConfig);
            var ptetDatestring = kendo.toString(ptetDateobj, dateformatConfig);
            data.PilotageServiceRecording().EndTime(ptetDatestring);

            var ptobDateobj1 = kendo.parseDate(data.PilotageServiceRecording().PilotOnBoard(), dateformatConfig);
            var ptobDatestring1 = kendo.toString(ptobDateobj1, dateformatConfig);
            data.PilotageServiceRecording().PilotOnBoard(ptobDatestring1);

            var ptofDateobj = kendo.parseDate(data.PilotageServiceRecording().PilotOff(), dateformatConfig);
            var ptofDatestring = kendo.toString(ptofDateobj, dateformatConfig);
            data.PilotageServiceRecording().PilotOff(ptofDatestring);

            var ptwtstDateobj = kendo.parseDate(data.PilotageServiceRecording().WaitingStartTime(), dateformatConfig);
            var ptwtstDatestring = kendo.toString(ptwtstDateobj, dateformatConfig);
            data.PilotageServiceRecording().WaitingStartTime(ptwtstDatestring);

            var ptwtetDateobj = kendo.parseDate(data.PilotageServiceRecording().WaitingEndTime(), dateformatConfig);
            var ptwtetDatestring = kendo.toString(ptwtetDateobj, dateformatConfig);
            data.PilotageServiceRecording().WaitingEndTime(ptwtetDatestring);

            var btwtstDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().WaitingStartTime(), dateformatConfig);
            var btwtstDatestring = kendo.toString(btwtstDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().WaitingStartTime(btwtstDatestring);

            var btwtetDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().WaitingEndTime(), dateformatConfig);
            var btwtetDatestring = kendo.toString(btwtetDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().WaitingEndTime(btwtetDatestring);

            var osStDateobj = kendo.parseDate(data.OtherServiceRecording().StartTime(), dateformatConfig);
            var osStDatestring = kendo.toString(osStDateobj, dateformatConfig);
            data.OtherServiceRecording().StartTime(osStDatestring);

            var osEtDateobj = kendo.parseDate(data.OtherServiceRecording().EndTime(), dateformatConfig);
            var osEtDatestring = kendo.toString(osEtDateobj, dateformatConfig);
            data.OtherServiceRecording().EndTime(osEtDatestring);

            var osLuDateobj = kendo.parseDate(data.OtherServiceRecording().LineUp(), dateformatConfig);
            var osLuDatestring = kendo.toString(osLuDateobj, dateformatConfig);
            data.OtherServiceRecording().LineUp(osLuDatestring);

            var osLdDateobj = kendo.parseDate(data.OtherServiceRecording().LineDown(), dateformatConfig);
            var osLdDatestring = kendo.toString(osLdDateobj, dateformatConfig);
            data.OtherServiceRecording().LineDown(osLdDatestring);

            var osPoDateobj = kendo.parseDate(data.OtherServiceRecording().PilotOn(), dateformatConfig);
            var osPoDatestring = kendo.toString(osPoDateobj, dateformatConfig);
            data.OtherServiceRecording().PilotOn(osPoDatestring);

            var osFsDateobj = kendo.parseDate(data.OtherServiceRecording().FirstSwing(), dateformatConfig);
            var osFsDatestring = kendo.toString(osFsDateobj, dateformatConfig);
            data.OtherServiceRecording().FirstSwing(osFsDatestring);

            var osLsDateobj = kendo.parseDate(data.OtherServiceRecording().LastSwing(), dateformatConfig);
            var osLsDatestring = kendo.toString(osLsDateobj, dateformatConfig);
            data.OtherServiceRecording().LastSwing(osLsDatestring);

            var osTasDateobj = kendo.parseDate(data.OtherServiceRecording().TimeAlongSide(), dateformatConfig);
            var osTasDatestring = kendo.toString(osTasDateobj, dateformatConfig);
            data.OtherServiceRecording().TimeAlongSide(osTasDatestring);

            var oswtstDateobj = kendo.parseDate(data.OtherServiceRecording().WaitingStartTime(), dateformatConfig);
            var oswtstDatestring = kendo.toString(oswtstDateobj, dateformatConfig);
            data.OtherServiceRecording().WaitingStartTime(oswtstDatestring);

            var oswtetDateobj = kendo.parseDate(data.OtherServiceRecording().WaitingEndTime(), dateformatConfig);
            var oswtetDatestring = kendo.toString(oswtetDateobj, dateformatConfig);
            data.OtherServiceRecording().WaitingEndTime(oswtetDatestring);


            if (data.OperationType() == 'TGWR') {               
                if (data.OtherServiceRecording().StartTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().EndTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().LineUp() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().LineDown() == null) {
                    errors = errors + 1;
                }
                if (data.OtherServiceRecording().WaitingStartTime() != null && data.OtherServiceRecording().WaitingStartTime() != "") {
                    if (data.OtherServiceRecording().WaitingEndTime() == null || data.OtherServiceRecording().WaitingEndTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting End Time value.");
                    }
                }

                if (data.OtherServiceRecording().WaitingEndTime() != null && data.OtherServiceRecording().WaitingEndTime() != "") {
                    if (data.OtherServiceRecording().WaitingStartTime() == null || data.OtherServiceRecording().WaitingStartTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting Start Time value.");
                    }
                }

                if ((data.OtherServiceRecording().WaitingStartTime() != "") && (data.OtherServiceRecording().WaitingStartTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != "")) {
                    if (data.OtherServiceRecording().WaitingStartTime() >= data.OtherServiceRecording().WaitingEndTime()) {
                        errors = errors + 1;
                        toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    }
                }
            }

            if (data.OperationType() == 'WTST') {               
                if (data.OtherServiceRecording().StartTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().EndTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().WaitingStartTime() != null && data.OtherServiceRecording().WaitingStartTime() != "") {
                    if (data.OtherServiceRecording().WaitingEndTime() == null || data.OtherServiceRecording().WaitingEndTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting End Time value.");
                    }
                }

                if (data.OtherServiceRecording().WaitingEndTime() != null && data.OtherServiceRecording().WaitingEndTime() != "") {
                    if (data.OtherServiceRecording().WaitingStartTime() == null || data.OtherServiceRecording().WaitingStartTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting Start Time value.");
                    }
                }

                if ((data.OtherServiceRecording().WaitingStartTime() != "") && (data.OtherServiceRecording().WaitingStartTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != "")) {
                    if (data.OtherServiceRecording().WaitingStartTime() >= data.OtherServiceRecording().WaitingEndTime()) {
                        errors = errors + 1;
                        toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    }
                }
            }

            if (data.OperationType() == 'FCST') {
                
                if (data.OtherServiceRecording().StartTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().TimeAlongSide() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().FirstSwing() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().LastSwing() == null) {
                    errors = errors + 1;
                }

                var resourceBQDateobj = kendo.parseDate(data.OtherServiceRecording().BackToQuay(), dateformatConfig);
                var resourceBQDatestring = kendo.toString(resourceBQDateobj, dateformatConfig);
                data.OtherServiceRecording().BackToQuay(resourceBQDatestring);

                if (data.OtherServiceRecording().Extend()) {
                    if (data.OtherServiceRecording().BackToQuay() == null) {
                        errors = errors + 1;
                    }
                }
                else {
                    if (data.OtherServiceRecording().EndTime() == null) {
                        errors = errors + 1;
                    }
                }
            }

            if (data.OperationType() == 'PIHE') {
                if (data.PilotageServiceRecording().StartTime() == null) {
                    errors = errors + 1;
                }

                if (data.PilotageServiceRecording().EndTime() == null) {
                    errors = errors + 1;
                }

                if (data.PilotageServiceRecording().PilotOnBoard() == null) {
                    errors = errors + 1;
                }

                if (data.PilotageServiceRecording().PilotOff() == null) {
                    errors = errors + 1;
                }
            }

            if (data.OperationType() == 'PILT') { 
                if (data.PilotageServiceRecording().WaitingStartTime() != null && data.PilotageServiceRecording().WaitingStartTime() != "") {
                    if (data.PilotageServiceRecording().WaitingEndTime() == null || data.PilotageServiceRecording().WaitingEndTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting End Time value.");
                    }
                }

                if (data.PilotageServiceRecording().WaitingEndTime() != null && data.PilotageServiceRecording().WaitingEndTime() != "") {
                    if (data.PilotageServiceRecording().WaitingStartTime() == null || data.PilotageServiceRecording().WaitingStartTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting Start Time value.");
                    }
                }

                if ((data.PilotageServiceRecording().WaitingEndTime() != "") && (data.PilotageServiceRecording().WaitingEndTime() != null) && (data.PilotageServiceRecording().WaitingStartTime() != null) && (data.PilotageServiceRecording().WaitingStartTime() != "")) {
                    if (data.PilotageServiceRecording().WaitingEndTime() <= data.PilotageServiceRecording().WaitingStartTime()) {
                        errors = errors + 1;
                        toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    }
                }
            }
            if (data.OperationType() == 'BRTH') {
                if (data.ShiftingBerthingTaskExecution().WaitingStartTime() != null && data.ShiftingBerthingTaskExecution().WaitingStartTime() != "") {
                    if (data.ShiftingBerthingTaskExecution().WaitingEndTime() == null || data.ShiftingBerthingTaskExecution().WaitingEndTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting End Time value.");
                    }
                }

                if (data.ShiftingBerthingTaskExecution().WaitingEndTime() != null && data.ShiftingBerthingTaskExecution().WaitingEndTime() != "") {
                    if (data.ShiftingBerthingTaskExecution().WaitingStartTime() == null || data.ShiftingBerthingTaskExecution().WaitingStartTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting Start Time value.");
                    }
                }

                if ((data.ShiftingBerthingTaskExecution().WaitingEndTime() != "") && (data.ShiftingBerthingTaskExecution().WaitingEndTime() != null) && (data.ShiftingBerthingTaskExecution().WaitingStartTime() != null) && (data.ShiftingBerthingTaskExecution().WaitingStartTime() != "")) {
                    if (data.ShiftingBerthingTaskExecution().WaitingEndTime() <= data.ShiftingBerthingTaskExecution().WaitingStartTime()) {
                        errors = errors + 1;
                        toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    }
                }
            }
         

            // if (data.OperationType() =='PILT')
               
            // {
                


            //    var actualtime = new Date(ptstDatestrings2);
            //    //var date=date1.setMinutes(date1.getMinutes() + 30);
            //    var pilottime = new Date(ptobDatestring1);
            //   // var pilotdate = pilottime.setMinutes(pilottime.getMinutes() + 30);
            //    var timedifference = (new Date(pilottime) - new Date(actualtime));
            //    var resultInMinutes = Math.round(timedifference / 60000);
            //    //var timedifference =date2.getMinutes()-date1.getMinutes();
            //    //alert(resultInMinutes);
            //    if (resultInMinutes > 30)
            //    {
                  
                   
            //            if ($('#MOPSDelay').val() == "") {
                           
            //                data.PilotageServiceRecording().IsValidationEnabled(true);

            //                //self.isSpanDelay(true);
                            
            //                //alert('Please complete the field');
            //            }

                       
                   
            //    }
            //    else {

            //        //data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
            //        //    return item.rule = "required";
            //        //});

            //        errors = 0;
            //        data.PilotageServiceRecording().IsValidationEnabled(false);
            //       // self.isSpanDelay(false);
                    
            //        //$('#delay').val("");
                    

            //    }


            //}


            //if (data.OperationType() == 'PILT' || data.OperationType() == 'TGWR' || data.OperationType() == 'BRTH') {
            //    if (data.OperationType() == 'PIHE') {
            //    }


            //}

            //anusha28/05/2024 start
            if (data.OperationType() == 'PILT') {

                var actualtime = new Date(ptstDatestrings2);
                var pilottime = new Date(ptobDatestring1);
                var timedifference = (new Date(pilottime) - new Date(actualtime));
                var resultInMinutes = Math.round(timedifference / 60000);
                if (resultInMinutes > 30) {


                    if ($('#MOPSDelay').val() == "" || $('#MOPSDelay').val() == 'null' || $('#MOPSDelay').val() == undefined) {
                        errors = 1;
                        data.PilotageServiceRecording().IsValidationEnabled(true);
                        toastr.warning("Please Enter MOPS Delay Reason", "Service Recording");
                        return;
                    }

                }
                else {

                    data.PilotageServiceRecording().IsValidationEnabled(false);
                }

                if (errors > 0) {
                    data.PilotageServiceRecording().IsValidationEnabled(true);
                }


                //anushaupdateendtim
                if ((data.PilotageServiceRecording().EndTime() != "") && (data.PilotageServiceRecording().EndTime() != null) && (data.PilotageServiceRecording().StartTime() != null) && (data.PilotageServiceRecording().StartTime() != "")) {
                    if (data.PilotageServiceRecording().EndTime() < data.PilotageServiceRecording().StartTime()) {
                        errors = errors + 1;
                        toastr.warning('End Time Should be Greater Than Start Time');
                        self.resourceallocationModel().PilotageServiceRecording().EndTime(data.EndTime);
                        $("#piltEndTime").focus();
                    }
                }




            }

            if (errors == 0) {
                self.viewModelHelper.apiPost('api/PostResourceAloocationDetails', ko.mapping.toJSON(data), function Message(data) {
                    toastr.success("Service recording details saved successfully.", "Service Recording");
                    self.LoadResourceAllacationMaster();
                    self.viewMode('List');
                    
                    $('#spnTitle').html("Service Recording");
                });
            }
            

            else {
                self.ResourceValidation().errors.showAllMessages();
                toastr.warning("You have some form errors. Please check below.");
                return;
            }
        }


        self.onkeydown = function () {
            data.PilotageServiceRecording().IsValidationEnabled(true);
        }
        self.SaveWaterResourceAllacation = function (data) {

            data.OperationType('WTST');
            var ResourceId = data.ResourceAllocationID; //parseInt(sessionStorage.getItem("ID"));
          sessionStorage.removeItem("ID");
          var dateformatConfig = "yyyy-MM-dd HH:mm";

          toastr.options.closeButton = true;
          toastr.options.positionClass = "toast-top-right";

          var errors = 0;   
        
              data.OtherServiceRecording().LineUp.extend({ validatable: false });
              data.OtherServiceRecording().LineDown.extend({ validatable: false });
              data.OtherServiceRecording().PilotOn.extend({ validatable: false });
              data.OtherServiceRecording().FirstSwing.extend({ validatable: false });
              data.OtherServiceRecording().LastSwing.extend({ validatable: false });
              data.OtherServiceRecording().TimeAlongSide.extend({ validatable: false });
              data.OtherServiceRecording().BackToQuay.extend({ validatable: false });
              data.OtherServiceRecording().ResourceAllocationID(ResourceId);
              data.OtherServiceRecording().IsValidationEnabled(true);
         
              var OpeningMeterReading = data.OtherServiceRecording().OpeningMeterReading();
              var ClosingMeterReading = data.OtherServiceRecording().ClosingMeterReading();
              if ((data.OtherServiceRecording().OpeningMeterReading()) != "" && (data.OtherServiceRecording().ClosingMeterReading()) != "") {


                  if (parseFloat(ClosingMeterReading) > parseFloat(OpeningMeterReading)) {
                      var ttlDispensed = (ClosingMeterReading - OpeningMeterReading).toFixed(2);
                      data.OtherServiceRecording().TotalDispensed(ttlDispensed);// = ttlDispensed;
                      $('#totaldispensed').val(ttlDispensed);
                  }
                  else {
                      toastr.warning("Closing meter reading should be greater than Opening meter reading", "Service Recording");
                      data.OtherServiceRecording().ClosingMeterReading('');
                      data.OtherServiceRecording().TotalDispensed('');
                      $('#closingmeterreading').val('');
                      $('#totaldispensed').val('');
                      errors = errors + 1;
                      return false;
                  }

              }

                  self.ResourceValidation = ko.observable(data.OtherServiceRecording());
                  self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                  errors = self.ResourceValidation().errors().length;
              
          

          var resourceStDateobj = kendo.parseDate(data.StartTime(), dateformatConfig);
          var resourceStDatestring = kendo.toString(resourceStDateobj, dateformatConfig);
          data.StartTime(resourceStDatestring);

          var resourceEtDateobj = kendo.parseDate(data.EndTime(), dateformatConfig);
          var resourceEtDatestring = kendo.toString(resourceEtDateobj, dateformatConfig);
          data.EndTime(resourceEtDatestring);

          var sbstDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().StartTime(), dateformatConfig);
          var sbstDatestring = kendo.toString(sbstDateobj, dateformatConfig);
          data.ShiftingBerthingTaskExecution().StartTime(sbstDatestring);

          var sbetDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().EndTime(), dateformatConfig);
          var sbetDatestring = kendo.toString(sbetDateobj, dateformatConfig);
          data.ShiftingBerthingTaskExecution().EndTime(sbetDatestring);

          var sbfliDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().FirstLineIn(), dateformatConfig);
          var sbfliDatestring = kendo.toString(sbfliDateobj, dateformatConfig);
          data.ShiftingBerthingTaskExecution().FirstLineIn(sbfliDatestring);

          var sblliDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().LastLineIn(), dateformatConfig);
          var sblliDatestring = kendo.toString(sblliDateobj, dateformatConfig);
          data.ShiftingBerthingTaskExecution().LastLineIn(sblliDatestring);

          var sbfloDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().FirstLineOut(), dateformatConfig);
          var sbfloDatestring = kendo.toString(sbfloDateobj, dateformatConfig);
          data.ShiftingBerthingTaskExecution().FirstLineOut(sbfloDatestring);

          var sblloDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().LastLineOut(), dateformatConfig);
          var sblloDatestring = kendo.toString(sblloDateobj, dateformatConfig);
          data.ShiftingBerthingTaskExecution().LastLineOut(sblloDatestring);

          var ptstDateobj = kendo.parseDate(data.PilotageServiceRecording().StartTime(), dateformatConfig);
          var ptstDatestring = kendo.toString(ptstDateobj, dateformatConfig);
          data.PilotageServiceRecording().StartTime(ptstDatestring);

          var ptstDateobjs = kendo.parseDate(data.PilotageServiceRecording().StartTime(), dateformatConfig);
          var ptstDatestrings = kendo.toString(ptstDateobjs, dateformatConfig);
          data.PilotageServiceRecording().StartTime(ptstDatestrings);

          var ptetDateobj = kendo.parseDate(data.PilotageServiceRecording().EndTime(), dateformatConfig);
          var ptetDatestring = kendo.toString(ptetDateobj, dateformatConfig);
          data.PilotageServiceRecording().EndTime(ptetDatestring);

          var ptobDateobj = kendo.parseDate(data.PilotageServiceRecording().PilotOnBoard(), dateformatConfig);
          var ptobDatestring = kendo.toString(ptobDateobj, dateformatConfig);
          data.PilotageServiceRecording().PilotOnBoard(ptobDatestring);

          var ptofDateobj = kendo.parseDate(data.PilotageServiceRecording().PilotOff(), dateformatConfig);
          var ptofDatestring = kendo.toString(ptofDateobj, dateformatConfig);
          data.PilotageServiceRecording().PilotOff(ptofDatestring);

          var ptwtstDateobj = kendo.parseDate(data.PilotageServiceRecording().WaitingStartTime(), dateformatConfig);
          var ptwtstDatestring = kendo.toString(ptwtstDateobj, dateformatConfig);
          data.PilotageServiceRecording().WaitingStartTime(ptwtstDatestring);

          var ptwtetDateobj = kendo.parseDate(data.PilotageServiceRecording().WaitingEndTime(), dateformatConfig);
          var ptwtetDatestring = kendo.toString(ptwtetDateobj, dateformatConfig);
          data.PilotageServiceRecording().WaitingEndTime(ptwtetDatestring);

          var btwtstDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().WaitingStartTime(), dateformatConfig);
          var btwtstDatestring = kendo.toString(btwtstDateobj, dateformatConfig);
          data.ShiftingBerthingTaskExecution().WaitingStartTime(btwtstDatestring);

          var btwtetDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().WaitingEndTime(), dateformatConfig);
          var btwtetDatestring = kendo.toString(btwtetDateobj, dateformatConfig);
          data.ShiftingBerthingTaskExecution().WaitingEndTime(btwtetDatestring);

          var osStDateobj = kendo.parseDate(data.OtherServiceRecording().StartTime(), dateformatConfig);
          var osStDatestring = kendo.toString(osStDateobj, dateformatConfig);
          data.OtherServiceRecording().StartTime(osStDatestring);

          var osEtDateobj = kendo.parseDate(data.OtherServiceRecording().EndTime(), dateformatConfig);
          var osEtDatestring = kendo.toString(osEtDateobj, dateformatConfig);
          data.OtherServiceRecording().EndTime(osEtDatestring);

          var osLuDateobj = kendo.parseDate(data.OtherServiceRecording().LineUp(), dateformatConfig);
          var osLuDatestring = kendo.toString(osLuDateobj, dateformatConfig);
          data.OtherServiceRecording().LineUp(osLuDatestring);

          var osLdDateobj = kendo.parseDate(data.OtherServiceRecording().LineDown(), dateformatConfig);
          var osLdDatestring = kendo.toString(osLdDateobj, dateformatConfig);
          data.OtherServiceRecording().LineDown(osLdDatestring);

          var osPoDateobj = kendo.parseDate(data.OtherServiceRecording().PilotOn(), dateformatConfig);
          var osPoDatestring = kendo.toString(osPoDateobj, dateformatConfig);
          data.OtherServiceRecording().PilotOn(osPoDatestring);

          var osFsDateobj = kendo.parseDate(data.OtherServiceRecording().FirstSwing(), dateformatConfig);
          var osFsDatestring = kendo.toString(osFsDateobj, dateformatConfig);
          data.OtherServiceRecording().FirstSwing(osFsDatestring);

          var osLsDateobj = kendo.parseDate(data.OtherServiceRecording().LastSwing(), dateformatConfig);
          var osLsDatestring = kendo.toString(osLsDateobj, dateformatConfig);
          data.OtherServiceRecording().LastSwing(osLsDatestring);

          var osTasDateobj = kendo.parseDate(data.OtherServiceRecording().TimeAlongSide(), dateformatConfig);
          var osTasDatestring = kendo.toString(osTasDateobj, dateformatConfig);
          data.OtherServiceRecording().TimeAlongSide(osTasDatestring);

          var oswtstDateobj = kendo.parseDate(data.OtherServiceRecording().WaitingStartTime(), dateformatConfig);
          var oswtstDatestring = kendo.toString(oswtstDateobj, dateformatConfig);
          data.OtherServiceRecording().WaitingStartTime(oswtstDatestring);

          var oswtetDateobj = kendo.parseDate(data.OtherServiceRecording().WaitingEndTime(), dateformatConfig);
          var oswtetDatestring = kendo.toString(oswtetDateobj, dateformatConfig);
          data.OtherServiceRecording().WaitingEndTime(oswtetDatestring);

        
              if (data.OtherServiceRecording().StartTime() == null) {
                  errors = errors + 1;
              }

              if (data.OtherServiceRecording().EndTime() == null) {
                  errors = errors + 1;
              }

              if (data.OtherServiceRecording().WaitingStartTime() != null && data.OtherServiceRecording().WaitingStartTime() != "") {
                  if (data.OtherServiceRecording().WaitingEndTime() == null || data.OtherServiceRecording().WaitingEndTime() == "") {
                      errors = errors + 1;
                      toastr.warning("Please enter the Waiting End Time value.");
                  }
              }

              if (data.OtherServiceRecording().WaitingEndTime() != null && data.OtherServiceRecording().WaitingEndTime() != "") {
                  if (data.OtherServiceRecording().WaitingStartTime() == null || data.OtherServiceRecording().WaitingStartTime() == "") {
                      errors = errors + 1;
                      toastr.warning("Please enter the Waiting Start Time value.");
                  }
              }

              if ((data.OtherServiceRecording().WaitingStartTime() != "") && (data.OtherServiceRecording().WaitingStartTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != "")) {
                  if (data.OtherServiceRecording().WaitingStartTime() >= data.OtherServiceRecording().WaitingEndTime()) {
                      errors = errors + 1;
                      toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                  }
              }
              if ((data.OtherServiceRecording().StartTime() != "") && (data.OtherServiceRecording().StartTime() != null) && (data.OtherServiceRecording().EndTime() != null) && (data.OtherServiceRecording().EndTime() != "")) {
                  if (data.OtherServiceRecording().StartTime() >= data.OtherServiceRecording().EndTime()) {
                      errors = errors + 1;
                      toastr.warning('End Time should be greater than Start Time.');
                  }
              }
              if (errors == 0) {
                  
              self.viewModelHelper.apiPost('api/SaveWaterAllocationDetails', ko.mapping.toJSON(data), function Message(data) {
                  toastr.success("Water Service recording details saved successfully.", "Service Recording");
                  self.LoadResourceAllacationMaster();
                  self.viewMode('List');
                  $('#spnTitle').html("Service Recording");
              });
          }
          else {
              self.ResourceValidation().errors.showAllMessages();
              toastr.warning("You have some form errors. Please check below.");
              return;
          }
        }

  
        self.EditResourceAllacation = function (modeldata) {
            if (modeldata.TaskStatus() == 'COMP') {
                self.IsComp(true);
            }
            
            modeldata.action = "Edit";          
            if (modeldata.TaskStatus() == 'CFRI') {
                // confirmation box - start
                $.confirm({
                    'title': ' Task acknowledged confirmation',
                    'message': 'The Task is not yet acknowledged by the Resource.Would you like to continue with the Execution details?',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {
                                self.IsCodeEnable(true);

                                var varResourceAllocationVO = {
                                    ResourceAllocationID: modeldata.ResourceAllocationID(),
                                    Remarks: 'Acknowledged By VTC',
                                    TaskStatus: 'ACCP',
                                    OperationType: modeldata.OperationType
                                };

                                $.ajax({
                                    type: "PUT",
                                    url: '/api/MobileScheduledTasks/PutScheduledTasks',
                                    data: varResourceAllocationVO,
                                    dataType: 'json',
                                    success: function (data) {
                                        
                                        self.viewModelHelper.apiGet('api/GetResourcealocationByResourceAllocID', { Id: modeldata.ResourceAllocationID() },
                                        function (result) {
                                           
                                            self.updatedData = ko.observable();
                                            self.updatedData = new IPMSROOT.ResourceAllocationModel(result);
                                            self.GetResourceallocationformdetails(self.updatedData);
                                            if (modeldata.OperationType() == "WTST") {                                                
                                                $('#divWaterServiceList1').hide();
                                                self.IsResetEnable(true);
                                                self.viewMode('Form');
                                                self.IsSave(false);
                                                self.IsWaterSave(true);
                                                self.VisibleServiceRecording(false);
                                                $('#spnTitle').html("Update Service Recording");                                              
                                                self.resourceallocationModel().OtherServiceRecording().IsCompleted(false);
                                                self.resourceallocationModel(new IPMSROOT.ResourceAllocationModel());
                                                self.LoadResourceAllacationMaster();
                                                self.LoadInitialData();
                                            }                                              
                                            else {                                                
                                                $('#divWaterServiceList1').hide();
                                                self.viewMode('Form');
                                                self.IsSave(true);
                                                self.IsWaterSave(false);
                                                self.VisibleServiceRecording(false);
                                                $('#spnTitle').html("Update Service Recording");
                                                self.resourceallocationModel(new IPMSROOT.ResourceAllocationModel());
                                                self.LoadResourceAllacationMaster();
                                                self.LoadInitialData();
                                            }
                                        }, null, null, false);
                                    }
                                });
                            }
                        },
                        'No': {
                            'class': 'gray',
                            'action': function () {
                                // $("#port").val(decryptresult);
                                return false;
                            }
                        }
                    }
                });
                //confirmation box - end
            }
            else {
                
                //self.IsCodeEnable(true);
                if (modeldata.OperationType() == "WTST") {
                    
                    if (modeldata.TaskStatus() == 'ACCP') {
                        self.IsCodeEnable(true);
                        self.VisibleServiceRecording(false);
                        self.GetResourceallocationformdetails(modeldata);
                        self.GetPostedDataForWaterService(modeldata);
                        self.viewMode('Form');
                        sessionStorage.setItem("action", "Edit");
                        self.IsCodeEnable(true);
                        self.VisibleServiceRecording(false);
                        self.IsWaterSave(true);
                        self.IsSave(false);
                        if (modeldata.OtherServiceRecording().IsCompleted() == "Yes") {
                            self.IsSave(true)
                            self.IsWaterSave(false);
                        }
                        else {
                            self.IsWaterSave(true);
                            self.IsSave(false);
                            self.resourceallocationModel().OtherServiceRecording().StartTime('');
                            self.resourceallocationModel().OtherServiceRecording().EndTime('');
                            self.resourceallocationModel().OtherServiceRecording().WaitingStartTime('');
                            self.resourceallocationModel().OtherServiceRecording().WaitingEndTime('');
                            self.resourceallocationModel().OtherServiceRecording().IsCompleted('');
                            self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
                            self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
                            self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                        }
                    }
                    if (modeldata.FirstName() != " ") {
                        self.IsWaterResourceEnable(false);
                    }
                    else {
                        self.IsWaterResourceEnable(true);
                    }
                    if (modeldata.TaskStatus() == 'COMP') {
                        sessionStorage.setItem("action", "Edit");
                        self.IsCodeEnable(true);
                        self.VisibleServiceRecording(false);
                        self.GetResourceallocationformdetails(modeldata);
                        self.GetPostedDataForWaterService(modeldata);
                        self.viewMode('Form');
                        self.IsCodeEnable(true);
                        self.VisibleServiceRecording(false);
                        self.IsWaterSave(false);
                        self.IsSave(true);                        
                    }              
                 
                }                
                else {                    
                    $('#divWaterServiceList1').hide();
                    self.VisibleServiceRecording(false);
                    self.GetResourceallocationformdetails(modeldata);
                    self.viewMode('Form');
                    self.IsWaterSave(false);
                    self.IsSave(true);
                    self.IsCodeEnable(true);
                    //$('#Extend').prop('disabled', true);
                    //$('#BackToQuay').prop('disabled', true);
                }  
                             
                //self.IsSave(true);
                //self.VisibleServiceRecording(false);
                $('#spnTitle').html("Update Service Recording");
            }
            
            

        
          
        }

        self.EditWaterAllocation = function (data) {
            
            var ac = sessionStorage.getItem("action");
            sessionStorage.removeItem("action");
            sessionStorage.setItem("action", "Edit");
            self.IsResetEnable(true);
            self.IsCodeEnable(true);
            $("#divWaterServiceList1").show();
            if (data.IsCompleted() == "Yes") {
                self.resourceallocationModel().OtherServiceRecording().IsCompleted(true);
            }
            else {
                self.resourceallocationModel().OtherServiceRecording().IsCompleted(false);
            }

            if (self.resourceallocationModel().OtherServiceRecording().IsCompleted()) {
                $('#idIsCompleted').prop('disabled', true);
            }
            else {
                $('#idIsCompleted').prop('disabled', false);
            }

            self.resourceallocationModel().OtherServiceRecording().StartTime(data.StartTime());
            self.resourceallocationModel().OtherServiceRecording().EndTime(data.EndTime());
            self.resourceallocationModel().OtherServiceRecording().WaitingStartTime(data.WaitingStartTime());
            self.resourceallocationModel().OtherServiceRecording().WaitingEndTime(data.WaitingEndTime());           
            self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading(data.OpeningMeterReading());
            self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading(data.ClosingMeterReading());
            self.resourceallocationModel().OtherServiceRecording().TotalDispensed(data.TotalDispensed());
            self.resourceallocationModel().OtherServiceRecording().MeterNo(data.MeterNo());
            self.resourceallocationModel().OtherServiceRecording().ActualScheduledTime(data.ActualScheduledTime());
            $('#DelayReason').val(data.DelayReason());
            $('#Remarks').val(data.Remarks());
            $('#Deficiencies').val(data.Deficiencies());           
            $('#spnTitle').html("Update Service Recording");
            self.IsWaterSave(false);
            self.IsSave(true);
            if (ac == "Verify") {
                self.IsWaterSave(false);
                self.IsSave(false);
            }
            else {
                self.IsWaterSave(false);
                self.IsSave(true);
            }
        }

        self.ValidateMeterNO = function (data, event) {          
            var entry = JSON.parse(ko.toJSON(data));              
            $(".validationError").text('');
            var meterno=entry.MeterNo;
            var resourceAllocationID=entry.ResourceAllocationID;
            self.viewModelHelper.apiGet('api/CheckMeterNoExists/' + entry.MeterNo + '/'+ entry.ResourceAllocationID,
                { meterno: entry.MeterNo,resourceAllocationID:entry.ResourceAllocationID },
                 function (result) {                   
                     if (result > 0) {                        
                         self.resourceallocationModel().OtherServiceRecording().MeterNo("");
                         $('#spanmeterno').text('Already exists for this service Reqest.! enter another Meter No.');
                         $('#spanmeterno').css('display', '');
                         $('#idMeterNo').val("");
                         return false;
                     }
                     else {
                         return true;
                     }
                 });
        }
        HandleKeyUp = function (data, event) {

            var keyCode = event.charCode || event.keyCode;
            if (keyCode === 8 || keyCode === 46) {

                event.returnValue = true;
            }
            else {
                var keyChar = String.fromCharCode(keyCode);
                return /[a-zA-Z0-9]/.test(keyChar);
            }            
            //var items = JSON.parse(ko.toJSON(self.resourceallocationModel().));
            //var entry = JSON.parse(ko.toJSON(data));
            //$.each(items, function (index, value) {
                //if (value.IMONo == entry.IMONo) {
                //    $('#spanimono').css('display', '');
                //}
                //else {
                    $('#spanimono').css('display', 'none');
            //    }
            //});
        }
        
      

        self.verifyResourceAllocation = function (modeldata) {
            if (modeldata.TaskStatus() == 'COMP') {
                self.IsComp(true);
            }
            modeldata.action = "Verify";
            sessionStorage.setItem("action","Verify");
            self.IsCodeEnable(true);
            self.GetResourceallocationformdetails(modeldata);
            self.VisibleServiceRecording(true);
            self.IsSave(false);
            self.IsWaterSave(false);
            self.viewMode('Form');
            $("#divWaterServiceList1").hide();
            self.isSpanDelay(false);
            self.isSpanMopsDelay(false);
            if (modeldata.OperationType() == 'WTST') {
                self.GetPostedDataForWaterService(modeldata);

                self.IsResetEnable(false);
                self.IsSave(false);
                self.IsWaterSave(false);               
            }           
            $('#spnTitle').html("Verify Service Recording");
        }
        self.VerifyServiceRecording = function (data) {
            

            var dateformatConfig = "yyyy-MM-dd HH:mm";

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var errors = 0;
            if (data.MovementType() == 'ARMV' && data.OperationType() == 'BRTH') {
                data.ShiftingBerthingTaskExecution().FirstLineOut.rules.remove(function (item) {
                    return item.rule = "required";
                });
                data.ShiftingBerthingTaskExecution().LastLineOut.rules.remove(function (item) {
                    return item.rule = "required";
                });

                data.ShiftingBerthingTaskExecution().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.ShiftingBerthingTaskExecution());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;

                //toastr.errors(errors, "error");
            }
            else if ((data.MovementType() == 'WRMV' || data.MovementType() == 'SHMV') && data.OperationType() == 'BRTH') {
                data.ShiftingBerthingTaskExecution().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.ShiftingBerthingTaskExecution());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
            }
            else if ((data.MovementType() == 'SGMV') && data.OperationType() == 'BRTH') {
                data.ShiftingBerthingTaskExecution().FirstLineIn.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().LastLineIn.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().FromBerthKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().FromBolardKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().ToBerthKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().ToBolardKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().MooringBolardBowKey.extend({ validatable: false });
                data.ShiftingBerthingTaskExecution().MooringBolardStemKey.extend({ validatable: false });

                data.ShiftingBerthingTaskExecution().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.ShiftingBerthingTaskExecution());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
            }
           // else if (data.OperationType() == 'PILT') {

                //data.PilotageServiceRecording().IsValidationEnabled(true);
                //data.PilotageServiceRecording().ActualScheduledTime.rules.remove(function (item) {
                //    return item.rule = "required";
                //});
                //data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
                //    return item.rule = "required";
                //});
              
                //self.ResourceValidation = ko.observable(data.PilotageServiceRecording());
                //self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                //errors = self.ResourceValidation().errors().length;
                //if (errors == 0) {
                //    if (data.PilotageServiceRecording().PilotOnBoard() != null && data.MovementDateTime() != null) {
                //        var pob = new Date(data.PilotageServiceRecording().PilotOnBoard());
                //        var movementdate = new Date(data.MovementDateTime());                       
                //        var difference = pob.getTime() - movementdate.getTime(); 
                //        var resultInMinutes = Math.round(difference / 60000);
                //        if (resultInMinutes > 31) {
                //            self.isSpanDelay(true);
                //            //self.isSpanMopsDelay(true);
                //            //if (data.PilotageServiceRecording().DelayReason().trim() == "" || data.PilotageServiceRecording().MOPSDelay() == "" || data.PilotageServiceRecording().MOPSDelay() == undefined) {
                //            //    if (data.PilotageServiceRecording().DelayReason().trim() == "") {
                //            //        toastr.warning("Please enter Delay Reason.", "Resource Allocation");
                //            //    }
                //            //    if (data.PilotageServiceRecording().MOPSDelay() == "" || data.PilotageServiceRecording().MOPSDelay() == undefined) {
                //            //        toastr.warning("Please enter MOPS Delay Reason.", "Resource Allocation");
                //            //    }
                //            //    return false;
                //            //}
                //            if (data.PilotageServiceRecording().DelayReason().trim() == "" ) {
                //                if (data.PilotageServiceRecording().DelayReason().trim() == "") {
                //                    toastr.warning("Please enter Delay Reason.", "Resource Allocation");
                //                }
                //                //if (data.PilotageServiceRecording().MOPSDelay() == "" || data.PilotageServiceRecording().MOPSDelay() == undefined) {
                //                //    toastr.warning("Please enter MOPS Delay Reason.", "Resource Allocation");
                //                //}
                //                return false;
                //            }
                //        } else {
                //            self.isSpanDelay(false);
                //           // self.isSpanMopsDelay(false);
                //        }
                //    }
                //}
            
              
        
          //  }
            else if (data.OperationType() == 'FCST') {
                data.OtherServiceRecording().LineUp.extend({ validatable: false });
                data.OtherServiceRecording().LineDown.extend({ validatable: false });
                data.OtherServiceRecording().PilotOn.extend({ validatable: false });
                data.OtherServiceRecording().OpeningMeterReading.extend({ validatable: false });
                data.OtherServiceRecording().ClosingMeterReading.extend({ validatable: false });
                data.OtherServiceRecording().TotalDispensed.extend({ validatable: false });
                data.OtherServiceRecording().MeterNo.extend({ validatable: false });
                if (data.OtherServiceRecording().Extend()) {
                    data.OtherServiceRecording().EndTime.extend({ validatable: false });
                }
                else {
                    data.OtherServiceRecording().BackToQuay.extend({ validatable: false });
                }

                data.OtherServiceRecording().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.OtherServiceRecording());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
            }
            else if (data.OperationType() == 'PIHE') {
                data.PilotageServiceRecording().WaitingStartTime.extend({ validatable: false });
                data.PilotageServiceRecording().WaitingEndTime.extend({ validatable: false });
                data.PilotageServiceRecording().AdditionalTugs.extend({ validatable: false });
                data.PilotageServiceRecording().OffSteam.extend({ validatable: false }); 
                data.PilotageServiceRecording().MarineRevenueCleared.extend({ validatable: false });
                data.PilotageServiceRecording().IsValidationEnabled(true);
                data.PilotageServiceRecording().ActualScheduledTime.rules.remove(function (item) {
                    return item.rule = "required";
               });
                data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
                    return item.rule = "required";
                });
                self.ResourceValidation = ko.observable(data.PilotageServiceRecording());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;             
            }
            else if (data.OperationType() == 'TGWR') {

                data.OtherServiceRecording().PilotOn.extend({ validatable: false });
                data.OtherServiceRecording().OpeningMeterReading.extend({ validatable: false });
                data.OtherServiceRecording().ClosingMeterReading.extend({ validatable: false });
                data.OtherServiceRecording().TotalDispensed.extend({ validatable: false });
                data.OtherServiceRecording().FirstSwing.extend({ validatable: false });
                data.OtherServiceRecording().LastSwing.extend({ validatable: false });
                data.OtherServiceRecording().TimeAlongSide.extend({ validatable: false });
                data.OtherServiceRecording().BackToQuay.extend({ validatable: false });
                data.OtherServiceRecording().MeterNo.extend({ validatable: false });
                data.OtherServiceRecording().IsValidationEnabled(true);
                self.ResourceValidation = ko.observable(data.OtherServiceRecording());
                self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                errors = self.ResourceValidation().errors().length;
            }
            else if (data.OperationType() == 'WTST') {
                data.OtherServiceRecording().LineUp.extend({ validatable: false });
                data.OtherServiceRecording().LineDown.extend({ validatable: false });
                data.OtherServiceRecording().PilotOn.extend({ validatable: false });
                data.OtherServiceRecording().FirstSwing.extend({ validatable: false });
                data.OtherServiceRecording().LastSwing.extend({ validatable: false });
                data.OtherServiceRecording().TimeAlongSide.extend({ validatable: false });
                data.OtherServiceRecording().BackToQuay.extend({ validatable: false });

                data.OtherServiceRecording().IsValidationEnabled(true);

                var OpeningMeterReading = data.OtherServiceRecording().OpeningMeterReading();
                var ClosingMeterReading = data.OtherServiceRecording().ClosingMeterReading();
                if ((data.OtherServiceRecording().OpeningMeterReading()) != "" && (data.OtherServiceRecording().ClosingMeterReading()) != "") {
                    if (parseFloat(ClosingMeterReading) > parseFloat(OpeningMeterReading)) {
                        var ttlDispensed = (ClosingMeterReading - OpeningMeterReading).toFixed(2);
                        data.OtherServiceRecording().TotalDispensed(ttlDispensed);// = ttlDispensed;
                        $('#totaldispensed').val(ttlDispensed);
                    }
                    else {
                        toastr.warning("Closing meter reading should be greater than Opening meter reading", "Service Recording");
                        data.OtherServiceRecording().ClosingMeterReading('');
                        data.OtherServiceRecording().TotalDispensed('');
                        $('#closingmeterreading').val('');
                        $('#totaldispensed').val('');
                        errors = errors + 1;
                        return false;
                    }
                }

                    self.ResourceValidation = ko.observable(data.OtherServiceRecording());
                    self.ResourceValidation().errors = ko.validation.group(self.ResourceValidation());
                    errors = self.ResourceValidation().errors().length;
                
            }

            var resourceStDateobj = kendo.parseDate(data.StartTime(), dateformatConfig);
            var resourceStDatestring = kendo.toString(resourceStDateobj, dateformatConfig);
            data.StartTime(resourceStDatestring);

            var resourceEtDateobj = kendo.parseDate(data.EndTime(), dateformatConfig);
            var resourceEtDatestring = kendo.toString(resourceEtDateobj, dateformatConfig);
            data.EndTime(resourceEtDatestring);

            var sbstDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().StartTime(), dateformatConfig);
            var sbstDatestring = kendo.toString(sbstDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().StartTime(sbstDatestring);

            var sbetDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().EndTime(), dateformatConfig);
            var sbetDatestring = kendo.toString(sbetDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().EndTime(sbetDatestring);

            var sbfliDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().FirstLineIn(), dateformatConfig);
            var sbfliDatestring = kendo.toString(sbfliDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().FirstLineIn(sbfliDatestring);

            var sblliDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().LastLineIn(), dateformatConfig);
            var sblliDatestring = kendo.toString(sblliDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().LastLineIn(sblliDatestring);

            var sbfloDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().FirstLineOut(), dateformatConfig);
            var sbfloDatestring = kendo.toString(sbfloDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().FirstLineOut(sbfloDatestring);

            var sblloDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().LastLineOut(), dateformatConfig);
            var sblloDatestring = kendo.toString(sblloDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().LastLineOut(sblloDatestring);

            var ptstDateobj = kendo.parseDate(data.PilotageServiceRecording().StartTime(), dateformatConfig);
            var ptstDatestring = kendo.toString(ptstDateobj, dateformatConfig);
            data.PilotageServiceRecording().StartTime(ptstDatestring);

            var ptstDateobjs2 = kendo.parseDate(data.PilotageServiceRecording().ActualScheduledTime(), dateformatConfig);
            var ptstDatestrings2 = kendo.toString(ptstDateobjs2, dateformatConfig);
            data.PilotageServiceRecording().ActualScheduledTime(ptstDatestrings2);

            var ptetDateobj = kendo.parseDate(data.PilotageServiceRecording().EndTime(), dateformatConfig);
            var ptetDatestring = kendo.toString(ptetDateobj, dateformatConfig);
            data.PilotageServiceRecording().EndTime(ptetDatestring);

            var ptobDateobj1 = kendo.parseDate(data.PilotageServiceRecording().PilotOnBoard(), dateformatConfig);
            var ptobDatestring1 = kendo.toString(ptobDateobj1, dateformatConfig);
            data.PilotageServiceRecording().PilotOnBoard(ptobDatestring1);

            var ptofDateobj = kendo.parseDate(data.PilotageServiceRecording().PilotOff(), dateformatConfig);
            var ptofDatestring = kendo.toString(ptofDateobj, dateformatConfig);
            data.PilotageServiceRecording().PilotOff(ptofDatestring);

            var ptwtstDateobj = kendo.parseDate(data.PilotageServiceRecording().WaitingStartTime(), dateformatConfig);
            var ptwtstDatestring = kendo.toString(ptwtstDateobj, dateformatConfig);
            data.PilotageServiceRecording().WaitingStartTime(ptwtstDatestring);

            var ptwtetDateobj = kendo.parseDate(data.PilotageServiceRecording().WaitingEndTime(), dateformatConfig);
            var ptwtetDatestring = kendo.toString(ptwtetDateobj, dateformatConfig);
            data.PilotageServiceRecording().WaitingEndTime(ptwtetDatestring);

            var osStDateobj = kendo.parseDate(data.OtherServiceRecording().StartTime(), dateformatConfig);
            var osStDatestring = kendo.toString(osStDateobj, dateformatConfig);
            data.OtherServiceRecording().StartTime(osStDatestring);

            var osEtDateobj = kendo.parseDate(data.OtherServiceRecording().EndTime(), dateformatConfig);
            var osEtDatestring = kendo.toString(osEtDateobj, dateformatConfig);
            data.OtherServiceRecording().EndTime(osEtDatestring);

            var osLuDateobj = kendo.parseDate(data.OtherServiceRecording().LineUp(), dateformatConfig);
            var osLuDatestring = kendo.toString(osLuDateobj, dateformatConfig);
            data.OtherServiceRecording().LineUp(osLuDatestring);

            var osLdDateobj = kendo.parseDate(data.OtherServiceRecording().LineDown(), dateformatConfig);
            var osLdDatestring = kendo.toString(osLdDateobj, dateformatConfig);
            data.OtherServiceRecording().LineDown(osLdDatestring);

            var osPoDateobj = kendo.parseDate(data.OtherServiceRecording().PilotOn(), dateformatConfig);
            var osPoDatestring = kendo.toString(osPoDateobj, dateformatConfig);
            data.OtherServiceRecording().PilotOn(osPoDatestring);

            var osFsDateobj = kendo.parseDate(data.OtherServiceRecording().FirstSwing(), dateformatConfig);
            var osFsDatestring = kendo.toString(osFsDateobj, dateformatConfig);
            data.OtherServiceRecording().FirstSwing(osFsDatestring);

            var osLsDateobj = kendo.parseDate(data.OtherServiceRecording().LastSwing(), dateformatConfig);
            var osLsDatestring = kendo.toString(osLsDateobj, dateformatConfig);
            data.OtherServiceRecording().LastSwing(osLsDatestring);

            var osTasDateobj = kendo.parseDate(data.OtherServiceRecording().TimeAlongSide(), dateformatConfig);
            var osTasDatestring = kendo.toString(osTasDateobj, dateformatConfig);
            data.OtherServiceRecording().TimeAlongSide(osTasDatestring);

            var btwtstDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().WaitingStartTime(), dateformatConfig);
            var btwtstDatestring = kendo.toString(btwtstDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().WaitingStartTime(btwtstDatestring);

            var btwtetDateobj = kendo.parseDate(data.ShiftingBerthingTaskExecution().WaitingEndTime(), dateformatConfig);
            var btwtetDatestring = kendo.toString(btwtetDateobj, dateformatConfig);
            data.ShiftingBerthingTaskExecution().WaitingEndTime(btwtetDatestring);


            var oswtstDateobj = kendo.parseDate(data.OtherServiceRecording().WaitingStartTime(), dateformatConfig);
            var oswtstDatestring = kendo.toString(oswtstDateobj, dateformatConfig);
            data.OtherServiceRecording().WaitingStartTime(oswtstDatestring);

            var oswtetDateobj = kendo.parseDate(data.OtherServiceRecording().WaitingEndTime(), dateformatConfig);
            var oswtetDatestring = kendo.toString(oswtetDateobj, dateformatConfig);
            data.OtherServiceRecording().WaitingEndTime(oswtetDatestring);


            if (data.OperationType() == 'TGWR') {
                if (data.OtherServiceRecording().StartTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().EndTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().LineUp() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().LineDown() == null) {
                    errors = errors + 1;
                }
                if (data.OtherServiceRecording().WaitingStartTime() != null && data.OtherServiceRecording().WaitingStartTime() != "") {
                    if (data.OtherServiceRecording().WaitingEndTime() == null || data.OtherServiceRecording().WaitingEndTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting End Time value.");
                    }
                }

                if (data.OtherServiceRecording().WaitingEndTime() != null && data.OtherServiceRecording().WaitingEndTime() != "") {
                    if (data.OtherServiceRecording().WaitingStartTime() == null || data.OtherServiceRecording().WaitingStartTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting Start Time value.");
                    }
                }

                if ((data.OtherServiceRecording().WaitingStartTime() != "") && (data.OtherServiceRecording().WaitingStartTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != "")) {
                    if (data.OtherServiceRecording().WaitingStartTime() >= data.OtherServiceRecording().WaitingEndTime()) {
                        errors = errors + 1;
                        toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    }
                }
            }

            if (data.OperationType() == 'WTST') {
                if (data.OtherServiceRecording().StartTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().EndTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().WaitingStartTime() != null && data.OtherServiceRecording().WaitingStartTime() != "") {
                    if (data.OtherServiceRecording().WaitingEndTime() == null || data.OtherServiceRecording().WaitingEndTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting End Time value.");
                    }
                }

                if (data.OtherServiceRecording().WaitingEndTime() != null && data.OtherServiceRecording().WaitingEndTime() != "") {
                    if (data.OtherServiceRecording().WaitingStartTime() == null || data.OtherServiceRecording().WaitingStartTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting Start Time value.");
                    }
                }

                if ((data.OtherServiceRecording().WaitingStartTime() != "") && (data.OtherServiceRecording().WaitingStartTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != null) && (data.OtherServiceRecording().WaitingEndTime() != "")) {
                    if (data.OtherServiceRecording().WaitingStartTime() >= data.OtherServiceRecording().WaitingEndTime()) {
                        errors = errors + 1;
                        toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    }
                }
            }

            if (data.OperationType() == 'FCST') {
                if (data.OtherServiceRecording().StartTime() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().TimeAlongSide() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().FirstSwing() == null) {
                    errors = errors + 1;
                }

                if (data.OtherServiceRecording().LastSwing() == null) {
                    errors = errors + 1;
                }


                var resourceBQDateobj = kendo.parseDate(data.OtherServiceRecording().BackToQuay(), dateformatConfig);
                var resourceBQDatestring = kendo.toString(resourceBQDateobj, dateformatConfig);
                data.OtherServiceRecording().BackToQuay(resourceBQDatestring);

                if (data.OtherServiceRecording().Extend()) {
                    if (data.OtherServiceRecording().BackToQuay() == null) {
                        errors = errors + 1;
                    }
                }
                else {
                    if (data.OtherServiceRecording().EndTime() == null) {
                        errors = errors + 1;
                    }
                }
            }

            if (data.OperationType() == 'PIHE') {
                if (data.PilotageServiceRecording().StartTime() == null) {
                    errors = errors + 1;
                }

                if (data.PilotageServiceRecording().EndTime() == null) {
                    errors = errors + 1;
                }

                if (data.PilotageServiceRecording().PilotOnBoard() == null) {
                    errors = errors + 1;
                }

                if (data.PilotageServiceRecording().PilotOff() == null) {
                    errors = errors + 1;
                }
            }

            if (data.OperationType() == 'PILT') {
                if (data.PilotageServiceRecording().WaitingStartTime() != null && data.PilotageServiceRecording().WaitingStartTime() != "") {
                    if (data.PilotageServiceRecording().WaitingEndTime() == null || data.PilotageServiceRecording().WaitingEndTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting End Time value.");
                    }
                }

                if (data.PilotageServiceRecording().WaitingEndTime() != null && data.PilotageServiceRecording().WaitingEndTime() != "") {
                    if (data.PilotageServiceRecording().WaitingStartTime() == null || data.PilotageServiceRecording().WaitingStartTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting Start Time value.");
                    }
                }

                if ((data.PilotageServiceRecording().WaitingEndTime() != "") && (data.PilotageServiceRecording().WaitingEndTime() != null) && (data.PilotageServiceRecording().WaitingStartTime() != null) && (data.PilotageServiceRecording().WaitingStartTime() != "")) {
                    if (data.PilotageServiceRecording().WaitingEndTime() <= data.PilotageServiceRecording().WaitingStartTime()) {
                        errors = errors + 1;
                        toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    }
                }
            }
            if (data.OperationType() == 'BRTH') {
                if (data.ShiftingBerthingTaskExecution().WaitingStartTime() != null && data.ShiftingBerthingTaskExecution().WaitingStartTime() != "") {
                    if (data.ShiftingBerthingTaskExecution().WaitingEndTime() == null || data.ShiftingBerthingTaskExecution().WaitingEndTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting End Time value.");
                    }
                }

                if (data.ShiftingBerthingTaskExecution().WaitingEndTime() != null && data.ShiftingBerthingTaskExecution().WaitingEndTime() != "") {
                    if (data.ShiftingBerthingTaskExecution().WaitingStartTime() == null || data.ShiftingBerthingTaskExecution().WaitingStartTime() == "") {
                        errors = errors + 1;
                        toastr.warning("Please enter the Waiting Start Time value.");
                    }
                }

                if ((data.ShiftingBerthingTaskExecution().WaitingEndTime() != "") && (data.ShiftingBerthingTaskExecution().WaitingEndTime() != null) && (data.ShiftingBerthingTaskExecution().WaitingStartTime() != null) && (data.ShiftingBerthingTaskExecution().WaitingStartTime() != "")) {
                    if (data.ShiftingBerthingTaskExecution().WaitingEndTime() <= data.ShiftingBerthingTaskExecution().WaitingStartTime()) {
                        errors = errors + 1;
                        toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    }
                }

                //anushaverifyingupdateendtime 28/05/2024
                if ((data.PilotageServiceRecording().EndTime() != "") && (data.PilotageServiceRecording().EndTime() != null) && (data.PilotageServiceRecording().StartTime() != null) && (data.PilotageServiceRecording().StartTime() != "")) {
                    if (data.PilotageServiceRecording().EndTime() <= data.PilotageServiceRecording().StartTime()) {
                        errors = errors + 1;
                        toastr.warning('End Time Should be Greater than Start Time');
                    }
                }
                //anusha end 
            }
            
            //if (data.OperationType() == 'PILT') {

            //    var actualtime = new Date(ptstDatestrings2);
            //    //var date=date1.setMinutes(date1.getMinutes() + 30);
            //    var pilottime = new Date(ptobDatestring1);
            //    // var pilotdate = pilottime.setMinutes(pilottime.getMinutes() + 30);
            //    var timedifference = (new Date(pilottime) - new Date(actualtime));
            //    var resultInMinutes1 = Math.round(timedifference / 60000);
            //    //var timedifference =date2.getMinutes()-date1.getMinutes();
            //    //alert(resultInMinutes);
            //    if (resultInMinutes1 > 30) {
            //        //if (errors == 1) {
            //            if ($('#MOPSDelay').val() == "") {
            //                errors = 1;
            //                data.PilotageServiceRecording().IsValidationEnabled(true);
            //                //self.isSpanDelay(true);

            //                //alert('Please complete the field');
            //            }

            //        //}

            //    }
            //    else {
            //        errors = 0;
            //        data.PilotageServiceRecording().IsValidationEnabled(false);
            //        //data.PilotageServiceRecording().MOPSDelay.rules.remove(function (item) {
            //        //    return item.rule = "required";
            //        //});
            //        self.isSpanDelay(false);
            //    }


            //}


            //anusha 28/05/2024 starttime
            if (data.OperationType() == 'PILT') {

                var actualtime = new Date(ptstDatestrings2);
                var pilottime = new Date(ptobDatestring1);
                var timedifference = (new Date(pilottime) - new Date(actualtime));
                var resultInMinutes1 = Math.round(timedifference / 60000);
                if (resultInMinutes1 > 30) {
                    //if (errors == 1) {
                    if ($('#MOPSDelay').val() == "") {
                        errors = 1;
                        data.PilotageServiceRecording().IsValidationEnabled(true);

                    }
                }
                else {
                    errors = 0;
                    data.PilotageServiceRecording().IsValidationEnabled(false);
                    self.isSpanDelay(false);
                }

            }


            //anushaverifyingendtime
            if ((data.PilotageServiceRecording().EndTime() != "") && (data.PilotageServiceRecording().EndTime() != null) && (data.PilotageServiceRecording().StartTime() != null) && (data.PilotageServiceRecording().StartTime() != "")) {
                if (data.PilotageServiceRecording().EndTime() < data.PilotageServiceRecording().StartTime()) {
                    errors = errors + 1;
                    toastr.warning('End Time Should be Greater Than Start Time');
                    self.resourceallocationModel().PilotageServiceRecording().EndTime(data.EndTime);
                    $("#piltEndTime").focus();
                }
            }
            //   anusha end

            if (errors == 0) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";

                $.confirm({
                    'title': 'Service Recording Verification',
                    'message': 'Do you want to verify the service record ?',
                    'buttons': {
                        'Yes': {
                            'class': 'blue',
                            'action': function () {
                                self.viewModelHelper.apiPut('api/PutResourceAloocationDetails', ko.mapping.toJSON(data), function Message(data) {
                                    toastr.success("Service recording details verified successfully.", "Service Recording");
                                    self.LoadResourceAllacationMaster();
                                    self.viewMode('List');
                                    $('#spnTitle').html("Service Recording");
                                });
                            }
                        },
                        'No': {
                            'class': 'gray',
                            'action': function () {
                            }
                        }
                    }
                });
            }
            else {
                self.ResourceValidation().errors.showAllMessages();
                toastr.warning("You have some form errors. Please check below.");
                return;
            }
        }

        self.GetBollardlength = function (data, event) {
            if (data.ToBolardKey() != null && data.ToBolardKey() != "" && data.ToBolardKey() != undefined) {
                if (data.FromBolardKey() != null && data.FromBolardKey() != "" && data.FromBolardKey() != undefined) {

                    var fromBollardsData = self.fromblordreferenceData().FromBollards();

                    var frombollardSelected = fromBollardsData.filter(function (item) {
                        return item.ToBollardKey() === data.FromBolardKey();
                    })[0];

                    var toBollardsData = self.toblordreferenceData().ToBollards();

                    var tobollardSelected = toBollardsData.filter(function (item) {
                        return item.ToBollardKey() === data.ToBolardKey();
                    })[0];

                    var bollardLength = (tobollardSelected.ToMeter() - frombollardSelected.FromMeter()).toFixed(2);
                    var vesselTotalLength = self.resourceallocationModel().VesselLength();

                    if (vesselTotalLength > bollardLength) {
                        toastr.warning("Bollard length should be greater than vessel length.", "Service Recording");
                        self.resourceallocationModel().ShiftingBerthingTaskExecution().ToBolardKey('');
                        $('#ToBollardbow').val('');
                    }

                }
                else {
                    toastr.warning("Please select From Bollard.", "Service Recording");
                    self.resourceallocationModel().ShiftingBerthingTaskExecution().ToBolardKey('');
                    $('#ToBollardbow').val('');
                }
            }
        }

        self.SrearchDet = function () {
            
            var isnoError = true;
            var vcnSearch = self.resourceallocationModel().VCNSearch();
            var vesselName = self.resourceallocationModel().VesselNameSearch();
            var resourceName = self.resourceallocationModel().ResourceName();
            var vcnSelected = self.resourceallocationModel().VCNSelected();
            var vesselSelected = self.resourceallocationModel().VesselSelected();
            var resourceSelected = self.resourceallocationModel().ResourceSelected();

            if (vcnSearch == "") {
                vcnSearch = "All";
                $("#spanVCNSearchValid").text('');
                self.isspanVCNSearchValid(false);
            }
            else {
                if (vcnSearch != "undefined") {
                    if (vcnSelected != vcnSearch) {
                        isnoError = false;
                        $("#spanVCNSearchValid").text('Please select valid VCN');
                        self.isspanVCNSearchValid(true);
                    }

                }
            }
          
            if (vesselName == "") {
                vesselName = "All";
                $("#spanVesselSearchValid").text('');
                self.isspanVesselSearchValid(false);
            }
            else {
                if (vesselName != "undefined") {
                    if (vesselSelected != vesselName) {
                        isnoError = false;
                        $("#spanVesselSearchValid").text('Please select valid Vessel Name/IMO No.');
                        self.isspanVesselSearchValid(true);
                    }
                }
                else {
                    $("#spanVesselSearchValid").text('');
                    self.isspanVesselSearchValid(false);
                }

            }

            if (resourceName == "") {
                resourceName = "All";
                $("#spanResourceSearchValid").text('');
                self.isspanResourceSearchValid(false);
            }
            else {
                if (resourceName != "undefined") {
                    if (resourceSelected != resourceName) {
                        isnoError = false;
                        $("#spanResourceSearchValid").text('Please select valid Resource Name.');
                        self.isspanResourceSearchValid(true);
                    }
                }


            }

        

            if (isnoError) {
                self.LoadResourceAllacationMaster();

                var grid = $("#divServiceRecList").data("kendoGrid");

                if (self.resourceallocationList().length <= 5)
                    grid.dataSource.pageSize(5);
                else
                    grid.dataSource.pageSize(20);

                grid.refresh();
            }
        }

        self.ResetSearchDet = function (data) {
            //data.VCNSearch('');
            //data.VesselNameSearch('')

            //data.ResourceName('')
            //data.TaskStatusSearch();
            
            self.resourceallocationModel(new IPMSROOT.ResourceAllocationModel());

            self.LoadResourceAllacationMaster();

            var grid = $("#divServiceRecList").data("kendoGrid");

            if (self.resourceallocationList().length <= 5)
                grid.dataSource.pageSize(5);
            else
                grid.dataSource.pageSize(20);

            grid.refresh();
        }

        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        ValidDate = function () {
            self.resourceallocationModel().ETATo(self.resourceallocationModel().ETAFrom());
        }

        CalTotalDispensed = function () {
            
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            var OpeningMeterReading = self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading();
            var ClosingMeterReading = self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading();

            if (OpeningMeterReading != null && OpeningMeterReading != "") {
                if (ClosingMeterReading != null && ClosingMeterReading != "") {
                    if (parseFloat(ClosingMeterReading) > parseFloat(OpeningMeterReading)) {
                        var opngMtrRdg = OpeningMeterReading;
                        var clsgMtrRdg = ClosingMeterReading;
                        var ttlDispensed = (clsgMtrRdg - opngMtrRdg).toFixed(2);
                        self.resourceallocationModel().OtherServiceRecording().TotalDispensed(ttlDispensed);// = ttlDispensed;
                        $('#totaldispensed').val(ttlDispensed);
                    }
                    else {
                        toastr.warning("Closing meter reading should be greater than Opening meter reading", "Service Recording");
                        self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
                        self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                        //self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading('');
                        //$('#openingmeterreading').val('');
                        $('#closingmeterreading').val('');
                        $('#totaldispensed').val('');
                    }
                }
                else {
                    toastr.warning("Please enter the Closing meter reading", "Service Recording");
                    self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                    $('#totaldispensed').val('');
                }

            } else {
                toastr.warning("Please enter the Opening meter reading", "Service Recording");
                self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
                self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
                $('#totaldispensed').val('');
                $('#closingmeterreading').val('');
            }
        }

        changeOpeningMeterReading = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var OpeningMeterReading = self.resourceallocationModel().OtherServiceRecording().OpeningMeterReading();
            var ClosingMeterReading = self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading();
            self.resourceallocationModel().OtherServiceRecording().ClosingMeterReading('');
            self.resourceallocationModel().OtherServiceRecording().TotalDispensed('');
            $('#totaldispensed').val('');
            $('#closingmeterreading').val('');
        }

        chkExtend = function () {
            if ($("#Extend").is(":checked")) {
                self.isEndTime(false);
                self.isBackToQA(true);
            }
            else {
                self.isEndTime(true);
                self.isBackToQA(false);
            }
        }

        CompareTugWtSt = function (data, event) {
            var dtTugWaitingStartTime = moment(self.resourceallocationModel().OtherServiceRecording().WaitingStartTime()).format('YYYY-MM-DD HH:mm');
            var dtTugWaitingEndTime = data.sender._oldText;

            if ((self.resourceallocationModel().OtherServiceRecording().WaitingStartTime() != "") && (self.resourceallocationModel().OtherServiceRecording().WaitingStartTime() != null)) {
                if (dtTugWaitingStartTime >= dtTugWaitingEndTime) {
                    toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    $("#TugWaitingEndTime").val('');
                    $("#TugWaitingEndTime").focus();
                }
            }
        }

        CompareTugWtEt = function (data, event) {
            var dtTugWaitingStartTime = data.sender._oldText;
            var dtTugWaitingEndTime = moment(self.resourceallocationModel().OtherServiceRecording().WaitingEndTime()).format('YYYY-MM-DD HH:mm');

            if ((self.resourceallocationModel().OtherServiceRecording().WaitingEndTime() != "") && (self.resourceallocationModel().OtherServiceRecording().WaitingEndTime() != null)) {
                if (dtTugWaitingStartTime >= dtTugWaitingEndTime) {
                    toastr.warning('Waiting Start Time should be less than Waiting End Time.');
                    $("#TugWaitingStartTime").val('');
                    $("#TugWaitingStartTime").focus();
                }
            }
        }

        CompareWaterWtSt = function (data, event) {
            var dtWaterWaitingStartTime = moment(self.resourceallocationModel().OtherServiceRecording().WaitingStartTime()).format('YYYY-MM-DD HH:mm');
            var dtWaterWaitingEndTime = data.sender._oldText;

            if ((self.resourceallocationModel().OtherServiceRecording().WaitingStartTime() != "") && (self.resourceallocationModel().OtherServiceRecording().WaitingStartTime() != null)) {
                if (dtWaterWaitingStartTime >= dtWaterWaitingEndTime) {
                    toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    $("#WaterWaitingEndTime").val('');
                    $("#WaterWaitingEndTime").focus();
                }
            }
        }

        CompareWaterWtEt = function (data, event) {

            var dtWaterWaitingStartTime = data.sender._oldText;
            var dtWaterWaitingEndTime = moment(self.resourceallocationModel().OtherServiceRecording().WaitingEndTime()).format('YYYY-MM-DD HH:mm');

            if ((self.resourceallocationModel().OtherServiceRecording().WaitingEndTime() != "") && (self.resourceallocationModel().OtherServiceRecording().WaitingEndTime() != null)) {
                if (dtWaterWaitingStartTime >= dtWaterWaitingEndTime) {
                    toastr.warning('Waiting Start Time should be less than Waiting End Time.');
                    $("#WaterWaitingStartTime").val('');
                    $("#WaterWaitingStartTime").focus();
                }
            }
        }


        ComparePiltWtSt = function (data, event) {
            var dtPiltWaitingStartTime = moment(self.resourceallocationModel().PilotageServiceRecording().WaitingStartTime()).format('YYYY-MM-DD HH:mm');
            var dtPiltWaitingEndTime = data.sender._oldText;

            if ((self.resourceallocationModel().PilotageServiceRecording().WaitingStartTime() != "") && (self.resourceallocationModel().PilotageServiceRecording().WaitingStartTime() != null)) {
                if (dtPiltWaitingStartTime >= dtPiltWaitingEndTime) {
                    toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    $("#WaitingEndTime").val('');
                    $("#WaitingEndTime").focus();
                }
            }
        }

        ComparePiltWtEt = function (data, event) {
            var dtPiltWaitingStartTime = data.sender._oldText;
            var dtPiltWaitingEndTime = moment(self.resourceallocationModel().PilotageServiceRecording().WaitingEndTime()).format('YYYY-MM-DD HH:mm');

            if ((self.resourceallocationModel().PilotageServiceRecording().WaitingEndTime() != "") && (self.resourceallocationModel().PilotageServiceRecording().WaitingEndTime() != null)) {
                if (dtPiltWaitingStartTime >= dtPiltWaitingEndTime) {
                    toastr.warning('Waiting Start Time should be less than Waiting End Time.');
                    $("#WaitingStartTime").val('');
                    $("#WaitingStartTime").focus();
                }
            }
        }

        CompareBerthWtSt = function (data, event) {
            var dtBerthWaitingStartTime = moment(self.resourceallocationModel().ShiftingBerthingTaskExecution().WaitingStartTime()).format('YYYY-MM-DD HH:mm');
            var dtBerthWaitingEndTime = data.sender._oldText;

            if ((self.resourceallocationModel().ShiftingBerthingTaskExecution().WaitingStartTime() != "") && (self.resourceallocationModel().ShiftingBerthingTaskExecution().WaitingStartTime() != null)) {
                if (dtBerthWaitingStartTime >= dtBerthWaitingEndTime) {
                    toastr.warning('Waiting End Time should be greater than Waiting Start Time.');
                    $("#BerthWaitingEndTime").val('');
                    $("#BerthWaitingEndTime").focus();
                }
            }
        }

        CompareBerthWtEt = function (data, event) {
            var dtBerthWaitingStartTime = data.sender._oldText;
            var dtBerthWaitingEndTime = moment(self.resourceallocationModel().ShiftingBerthingTaskExecution().WaitingEndTime()).format('YYYY-MM-DD HH:mm');

            if ((self.resourceallocationModel().ShiftingBerthingTaskExecution().WaitingEndTime() != "") && (self.resourceallocationModel().ShiftingBerthingTaskExecution().WaitingEndTime() != null)) {
                if (dtBerthWaitingStartTime >= dtBerthWaitingEndTime) {
                    toastr.warning('Waiting Start Time should be less than Waiting End Time.');
                    $("#BerthWaitingStartTime").val('');
                    $("#BerthWaitingStartTime").focus();
                }
            }
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }


        //-------------------------------------------------------
        SerchVesselBackSpace = function (e) {
            self.resourceallocationModel().VesselNameSearch('');
        }
        SerchVCNBackSpace = function (e) {
            self.resourceallocationModel().VCNSearch('');
        }

        SerchResourceBackSpace = function (e) {
           // self.resourceallocationModel().ResourceName('');
            self.resourceallocationModel().ResourceName('');
        }
        //SerchVCNBackSpaceNumValid = function (e) {
        //    self.arrivalNotificationModelSearchGrid().VCNSERCH('');
        //}

        VCNonblur = function (e) {


            var vcnblur = $("#VCNName").val();
            self.resourceallocationModel().VCNSearch(vcnblur);



        }
        Resourceonblur = function (e) {


            var vcnblur = $("#ResourceName1").val();
            self.resourceallocationModel().ResourceName(vcnblur);



        }
        Vesselonblur = function (e) {
            var vesselblur = $("#VesselName1").val();
            self.resourceallocationModel().VesselNameSearch(vesselblur);

        }

        SerchVCNBackSpaceNumValid = function (evt) {

            self.resourceallocationModel().VCNSearch('');

            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;

            //if (window.event) // IE
            //    keynum = event.keyCode;
            //else if (event.which) // Netscape/Firefox/Opera
            //    keynum = event.which;
            //keychar = String.fromCharCode(keynum);
            //charcheck = /[0-9\b]/g;
            //// var result = keychar.match(charcheck)
            //return ((keychar.match(charcheck) == null) ? false : true);
        }


        self.VCNSelectSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.resourceallocationModel().VCNSelected(selecteddataItem.vcn);
            self.isspanVCNSearchValid(false);
            $("#spanVCNSearchValid").text('');
        }

        self.VesselSelectSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.resourceallocationModel().VesselSelected(selecteddataItem.VesselName);
            self.isspanVesselSearchValid(false);
            $("#spanVesselSearchValid").text('');

        }
        chkIsCompleted = function () {
           
            if ($("#idIsCompleted").is(":checked")) {
                self.IsCompleted="Y";
            }
            else {
                self.IsCompleted="N";
            }
        }
        
        self.ResourceNameSearch = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
                self.resourceallocationModel().ResourceSelected(selecteddataItem.FirstName);
            self.isspanResourceSearchValid(false);
            $("#spanResourceSearchValid").text('');
        }

        //Set MaxDate as Today's date or Current date to avoid past date of post request
        calmaxtoday = function () {         
                this.max(new Date());
        }
       
        calminStartDate = function () {
            
            var midt;
            var mxdt;
            var strmyDatePicker;
            var strday;
            var strmonth;
            var stryear;
            var strHour;
            var strMnt;

            var endmyDatePicker;
            var endday;
            var endmonth;
            var endyear;
            var endHour;
            var endMnt;
            var ListLength = self.WaterResourceList().length;
            if (ListLength == 0) {
                this.max(new Date());
            }
            else {
                var Enddate = self.WaterResourceList()[0].EndTime();
                if (Enddate != null) {

                    strmyDatePicker = kendo.parseDate(moment(Enddate).format('YYYY-MM-DD HH:mm'), "yyyy-MM-dd HH:mm");
                    strday = strmyDatePicker.getDate();
                    strmonth = strmyDatePicker.getMonth();
                    stryear = strmyDatePicker.getFullYear();
                    strHour = strmyDatePicker.getHours();
                    strMnt = strmyDatePicker.getMinutes() + 1;
                    mxdt = new Date();

                    this.min(new Date(stryear, strmonth, strday, strHour, strMnt));
                    this.max(new Date());
                }
            }
           

        }
           

        //anushaupdate 28/05/2024 start

        ComparePiltWtStanu = function (data, event) {
            var dtPiltWaitingStartTime2 = moment(self.resourceallocationModel().PilotageServiceRecording().StartTime()).format('YYYY-MM-DD HH:mm');
            var dtPiltWaitingEndTime2 = data.sender._oldText;

            if ((self.resourceallocationModel().PilotageServiceRecording().StartTime() != "") && (self.resourceallocationModel().PilotageServiceRecording().EndTime() != null)) {
                if (dtPiltWaitingStartTime2 > dtPiltWaitingEndTime2) {


                    toastr.warning('End Time should be greater than  Start Time.');
                    self.resourceallocationModel().PilotageServiceRecording().EndTime(data.EndTime);

                    //$("#piltEndTime").val('');
                    $("#piltEndTime").focus();
                }
            }
        }


        //end
        calminEndDate = function () {
            
            var midt;
            var mxdt;
            var strmyDatePicker;
            var strday;
            var strmonth;
            var stryear;
            var strHour;
            var strMnt;

            var endmyDatePicker;
            var endday;
            var endmonth;
            var endyear;
            var endHour;
            var endMnt;
            var ListLength = self.WaterResourceList().length;
            if (ListLength == 0) {
                this.max(new Date());
            }
            else {
                var Enddate = self.WaterResourceList()[0].EndTime();
                if (Enddate != null) {

                    strmyDatePicker = kendo.parseDate(moment(Enddate).format('YYYY-MM-DD HH:mm'), "yyyy-MM-dd HH:mm");
                    strday = strmyDatePicker.getDate();
                    strmonth = strmyDatePicker.getMonth();
                    stryear = strmyDatePicker.getFullYear();
                    strHour = strmyDatePicker.getHours();
                    strMnt = strmyDatePicker.getMinutes() + 2;
                    mxdt = new Date();


                    this.min(new Date(stryear, strmonth, strday, strHour, strMnt));
                    this.max(new Date());
                }
                else {
                    //
                    this.max(new Date());
                }
            }

        }
        

        //-------------------------------------------------------

        self.Initialize();
    }
    IPMSRoot.ResourceAllocationViewmodel = ResourceAllocationViewmodel;
}(window.IPMSROOT));