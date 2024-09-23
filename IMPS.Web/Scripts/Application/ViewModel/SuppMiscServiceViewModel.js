(function (IPMSRoot) {

    var SuppMiscServiceViewModel = function () {

        var self = this;
        $('#spnTitile').html("Miscellaneous Service Recording");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.ddleditableView = ko.observable(true);
        self.suppMiscServiceModel = ko.observable();
        self.suppMiscRecordingModel = ko.observable();
        self.SuppMiscServiceList = ko.observableArray();
        self.SuppMiscDockingList = ko.observableArray();
        self.suppMiscReferenceData = ko.observable();
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
        self.MinimumDate = ko.observable();
        self.MaximumDate = ko.observable();
        self.IseditMode = ko.observable(false);
        self.IsSuppdryDockID = ko.observable();
        self.IsPhaseEnable = ko.observable(true);
       


        self.IsUpdateRec = ko.observable(false);
        self.IsSaveRec = ko.observable(true);
        self.IsCancel = ko.observable(false);
        self.Initialize = function () {           
            self.viewMode = ko.observable(true);
            self.suppMiscServiceModel(new IPMSROOT.SuppMiscServiceModel());
            self.suppMiscRecordingModel(new IPMSROOT.SuppMiscRecordingModel());
            self.LoadSuppMiscDocking();
            self.LoadInitialData();
            self.LoadSuppMiscServices();
            self.viewMode('List');
            $("#btnCancel").hide();
           
           
            

        }

        // For List
        self.LoadSuppMiscDocking = function () {
            self.viewModelHelper.apiGet('api/SuppMiscService',
            null,
              function (result) {                  
                  self.SuppMiscDockingList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.SuppMiscServiceModel(item);
                  }));

                  self.ddleditableView(false);
              });

        }

        // For Misc Recording 
        self.LoadSuppMiscServices = function () {
            
            var SuppDryDockID = self.suppMiscServiceModel().SuppDryDockID();
            self.viewModelHelper.apiGet('api/SuppMiscServiceRecording/' + SuppDryDockID, {},
         
              function (result) {

                  self.SuppMiscServiceList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.SuppMiscServiceModel(item);
                  }));


              });

        }

        self.LoadInitialData = function () {
           
            self.viewModelHelper.apiGet('api/SuppMiscReferenceData', null,
                function (result1) {
                    self.suppMiscReferenceData(new IPMSRoot.SuppMiscReferenceData(result1));
                }, null, null, false);

        }

        self.addsuppmisc = function () {
          
            self.viewMode('Form');          
            self.IsSave(true);
            self.IsUpdate(false);
            self.editableView(true);
            self.ddleditableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);           
            self.suppMiscServiceModel(new IPMSROOT.SuppMiscServiceModel());

            $('#spnTitile').html("Add Miscellaneous Service Recording");
        }



        self.viewSuppMisc = function (SuppMisc) {
          
            $("#btnCancel").show();
            $("#abc").attr('readonly', false);
            self.IsCancel(true);
            self.IseditMode(false);
            self.MinimumDate(SuppMisc.ScheduleFromDate() + ' 12:00');
            self.MaximumDate(SuppMisc.ScheduleToDate() + ' 12:00');
            
            var SuppDryDockID = SuppMisc.SuppDryDockID();
            self.viewModelHelper.apiGet('api/SuppMiscServiceRecording/' + SuppDryDockID, {},
              function (result) {

                  self.SuppMiscServiceList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.SuppMiscServiceModel(item);
                  }));
              });

            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.ddleditableView(false);

            self.suppMiscServiceModel(SuppMisc);
            self.IsSuppdryDockID(SuppMisc.SuppDryDockID());
            $('#spnTitile').html("View Miscellaneous Service Recording");
        }

        // Update / Edit Mode
        self.editSuppMisc = function (SuppMisc) {
          
            $("#btnCancel").show();
            $("#abc").attr('readonly', true);
            self.IseditMode(true);
            self.IsCancel(true);
            self.MinimumDate(SuppMisc.ScheduleFromDate() + ' 12:00');
            self.MaximumDate(SuppMisc.ScheduleToDate() + ' 12:00');
            if (SuppMisc.ExtensionDateTime() == "Invalid date") {
                self.suppMiscServiceModel().ExtensionDateTime(null);
            }
            if (SuppMisc.ExtensionDateTime() != null) {
                self.MaximumDate(SuppMisc.ExtensionDateTime() + ' 12:00');
            }
            var SuppDryDockID = SuppMisc.SuppDryDockID();
            self.viewModelHelper.apiGet('api/SuppMiscServiceRecording/' + SuppDryDockID, {},
              function (result) {                
                  self.SuppMiscServiceList(ko.utils.arrayMap(result, function (item) {                  
                      return new IPMSRoot.SuppMiscServiceModel(item);
                  }));


              });
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.ddleditableView(false);
            self.IsCodeEnable(true);
            self.suppMiscServiceModel(SuppMisc);
            self.IsSuppdryDockID(SuppMisc.SuppDryDockID());
            $('#spnTitile').html("Update Miscellaneous Service Recording");

           

        }

        Cancel = function () {            
            self.viewMode('List');
            self.suppMiscServiceModel().reset();
            $('#spnTitile').html("Miscellaneous Service Recording");
            $("#btnCancel").hide();

        }
        
        //Save Pop up
        self.SaveSuppmiscRec = function (model) {
         
            
            model.validationEnabled(true);
            if (model.ToDateTime() == "Invalid date" || model.ToDateTime() == undefined)
                model.ToDateTime("");         
            self.SuppMiscValidation = ko.observable(model);          
                self.SuppMiscValidation().errors = ko.validation.group(self.SuppMiscValidation());
                var errors = self.SuppMiscValidation().errors().length;
                var duplicate = false;                        
                model.SuppDryDockID(self.IsSuppdryDockID());
               
            //if (model.ServiceTypeCode() == "") added by divya on 31Oct2017
                if (model.ServiceTypeCode() == "") {
                    if (model.Phase() == "") {
                        $("#spnphase").show();
                        $("#spnphase").text('* Please select Phase');
                        errors = 1;
                    }
                    if (model.StartMeterReading() == "") {
                        $("#spnStartReadingEmpty").show();
                        $("#spnStartReadingEmpty").text('* Please enter Start Meter Reading');
                        errors = 1;
                    }
                    if (model.EndMeterReading() == "") {
                        $("#spnEndReadingEmpty").show();
                        $("#spnEndReadingEmpty").text('* Please enter End Meter Reading');
                        errors = 1;
                    }
                    if ($('#txtQty').val()=="") {
                        $('#spnQuantityEmpty').show();
                        $('#spnQuantityEmpty').text('* Please enter Qunatity');
                        errors = 1;
                    }

                }//end 

                else if (model.ServiceTypeCode() != "") {
                    if (model.ServiceTypeCode() == 'MSE2' || model.ServiceTypeCode() == 'MSE4') {
                        model.StartMeterReading("");
                        model.EndMeterReading("");
                      
                        if (model.Phase() == "") {
                            $("#spnphase").show();
                            $("#spnphase").text('* Please select Phase');
                            errors = 1;
                        }
                        if ($('#txtQty').val() == "") {
                            $('#spnQuantityEmpty').show();
                            $('#spnQuantityEmpty').text('* Please enter Qunatity');
                            errors = 1;
                        }
                        else {//added by divya on 31Oct2017
                            $("#spnphase").hide();

                            if (model.ServiceTypeCode() != "" && model.FromDateTime() != "" && model.ToDateTime() != "" && model.Quantity() != "")
                                errors = 0;
                        }
                    }// end  
                        //added by divya on 30Oct2017
                    else if (model.ServiceTypeCode() == "MSWT") {//if service type selected is "Water"                    
                        var Svalue = model.StartMeterReading();
                        var Evalue = model.EndMeterReading();                       
                        $("#spnphase").hide();
                        model.Phase(null);
                        if (Svalue == "") {
                            $("#spnStartReadingEmpty").show();
                            $("#spnStartReadingEmpty").text('* Please enter Start Meter Reading');
                            errors = 1;
                        }
                        if (Evalue == "") {
                            $("#spnEndReadingEmpty").show();
                            $("#spnEndReadingEmpty").text('* Please enter End Meter Reading');
                            errors = 1;
                        }                       
                        
                         if (Svalue != "" && Evalue != "") {                       
                            $("#spnStartReadingEmpty").hide();
                            $("#spnEndReadingEmpty").hide();
                            if (parseInt(Svalue) == 0) {
                                $("#spnStartReadingValid").show();
                                $("#spnStartReadingValid").text('* Start Meter Reading should be greater than zero');
                                errors = 1;
                            }
                            if (parseInt(Evalue) == 0) {
                                $("#spnEndReadingValid").show();
                                $("#spnEndReadingValid").text('* End Meter Reading should be greater than zero');
                                errors = 1;
                            }
                            if (parseInt(Svalue) == parseInt(Evalue)) {
                                $("#spnStartReadingEmpty").hide();
                                $("#spnEndReadingValid").show();
                                $("#spnEndReadingValid").text('* Start Meter Reading and End Meter Reading cannot be same');
                                errors = 1;
                            }
                            else if (parseInt(Svalue) > parseInt(Evalue)) {
                                $("#spnEndReading").hide();
                                $("#spnStartReadingValid").show();
                                $("#spnStartReadingValid").text('* Start Meter Reading should not be greater than End Meter Reading ');
                                errors = 1;
                            }
                            else {
                                model.Quantity = parseInt(Evalue) - parseInt(Svalue);
                                $('#txtQty').val(parseInt(Evalue) - parseInt(Svalue));
                            }
                        }
                    }//end is water               
                    else {
                        $("#spnEndReadingEmpty").hide();
                        $("#spnStartReadingEmpty").hide();
                        $("#spnEndReadingValid").hide();
                        $("#spnStartReadingValid").hide();
                        $("#spnphase").hide();
                       
                        if ($('#txtQty').val() == "") {
                            $('#spnQuantityEmpty').show();
                            $('#spnQuantityEmpty').text('* Please enter Qunatity');
                            errors = 1;
                        }
                        model.Phase(null);
                        model.StartMeterReading("");
                        model.EndMeterReading("");
                        if (model.ServiceTypeCode() != "" && model.FromDateTime() != "" && model.ToDateTime() != "" && model.Quantity() != "")
                            errors = 0;
                    }
                }
                
           
           

            if (errors == 0) {
               
                if (model.Phase() == "") {
                    model.Phase(null);
                }
              
                self.viewModelHelper.apiPut('api/SuppMiscServiceRecording', ko.mapping.toJSON(model), function Message(data) {
                   
                    if (data.SuppMiscServiceID == 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Miscellaneous Service Recording Saved Successfully.", "Miscellaneous Service Recording");
                    }
                    else {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Miscellaneous Service Recording Updated Successfully.", "Miscellaneous Service Recording");
                    }
                    self.LoadSuppMiscServices();
                    $('#stack1').modal('hide')
                    $('#spnTitile').html("Miscellaneous Service Recording");
                    self.viewMode('Form');
                    self.ddleditableView(false);
                });
            }
            else {
                self.SuppMiscValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
           
        }


        // Add Pop Up
        self.addSuppmiscRec = function () {
            
            self.ddleditableView(true);
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsPhaseEnable(true);
            self.suppMiscRecordingModel(new IPMSROOT.SuppMiscRecordingModel());
            $('#stack1').modal('show')

            $("#FromDate").data('kendoDateTimePicker').enable(true);
            $("#ToDate").data('kendoDateTimePicker').enable(true);
            $('#spnTitile1').html("Add Miscellaneous Service Details");
            $("#StartMeterReading").hide();//Added by divya on 31OCt2017
            $("#EndMeterReading").hide();//Added by divya on 31OCt2017
        }


        // Edit Pop Up
        self.editSuppmiscRec = function (SuppMisc) {
            
            $("#btnCancel").show();
            self.ddleditableView(false);
            self.IsPhaseEnable(true);
            var SuppDryDockID = SuppMisc.SuppDryDockID();
            self.viewModelHelper.apiGet('api/SuppMiscServiceRecording/' + SuppDryDockID, {},
              function (result) {
                
                  self.SuppMiscServiceList(ko.utils.arrayMap(result, function (item) {
                    
                      return new IPMSRoot.SuppMiscServiceModel(item);
                  }));

              });

            self.suppMiscRecordingModel(SuppMisc);
            $('#stack1').modal('show');
            //Added by divy aon 31Oct2017
            if (SuppMisc.ServiceTypeName() != 'Water') {
                $('#txtQty').attr('readonly', false);
                $("#StartMeterReading").hide();
                $("#EndMeterReading").hide();
            }
            if (SuppMisc.ServiceTypeName() == 'Water') {
                $("#StartMeterReading").show();
                $("#EndMeterReading").show();
                $('#txtQty').attr('readonly', true);
            }//end
            if (SuppMisc.ServiceTypeName() == 'Electricity 230 V' || SuppMisc.ServiceTypeName() == 'Electricity 440 V') {
                $("#PhaseId").show();
            }
            else {
                $("#PhaseId").hide();
            }
            $("#FromDate").data('kendoDateTimePicker').enable(true);
            $("#ToDate").data('kendoDateTimePicker').enable(true);
            $('#spnTitile1').html("Update Miscellaneous Service Details");
            self.IsUpdate(true);
            self.IsSave(false);
            self.editableView(true);
        
        }
        
        // View Pop UP
        self.viewSuppmiscRec = function (SuppMisc) {
           
            $("#btnCancel").show();
            self.ddleditableView(false);
            self.IsPhaseEnable(false);
            var SuppDryDockID = SuppMisc.SuppDryDockID();
            self.viewModelHelper.apiGet('api/SuppMiscServiceRecording/' + SuppDryDockID, {},
              function (result) {
                  self.SuppMiscServiceList(ko.utils.arrayMap(result, function (item) {
                     
                      return new IPMSRoot.SuppMiscServiceModel(item);
                  }));

              });

            self.suppMiscRecordingModel(SuppMisc);
            $('#stack1').modal('show');
            //Added by divy aon 31Oct2017
            if (SuppMisc.ServiceTypeName() != 'Water') {
                
                $("#StartMeterReading").hide();
                $("#EndMeterReading").hide();
            }
            if (SuppMisc.ServiceTypeName() == 'Water') {
                $("#StartMeterReading").show();
                $("#EndMeterReading").show();
            }//end
            if (SuppMisc.ServiceTypeName() == 'Electricity 230 V' || SuppMisc.ServiceTypeName() == 'Electricity 440 V') {
                $("#PhaseId").show();               
            }
            else {
                $("#PhaseId").hide();
            }
            $("#FromDate").data('kendoDateTimePicker').enable(false);
            $("#ToDate").data('kendoDateTimePicker').enable(false);
            $('#spnTitile1').html("View Miscellaneous Service Details");
            self.IsUpdate(false);
            self.IsSave(false);
            self.editableView(false);
            

          
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.OnSelectService = function (e) {
           
            var dataItem = this.dataItem(e.item.index());
            $('#txtQty').val('');
            if (dataItem.ServiceTypeName != "Water") {//added by divya on 31Oct2017
                $('#txtQty').attr('readonly', false);
                $("#txtStartMeterReading").val("");//clear the values 
                $("#txtEndtMeterReading").val("");
                $("#StartMeterReading").hide();//hide the div
                $("#EndMeterReading").hide();              
                $("#spnEndReadingEmpty").show();
                $("#spnStartReadingEmpty").show();
                $("#spnEndReadingValid").hide();
                $("#spnStartReadingValid").hide();
                if ($('#txtQty').val() == "") {
                    $('#spnQuantityEmpty').show();
                }
                else {
                    $('#spnQuantityEmpty').hide();
                }
               
                

                if (dataItem.ServiceTypeName == 'Electricity 230 V' || dataItem.ServiceTypeName == 'Electricity 440 V') {
                    $("#PhaseId").show();
                   
                }
                else {
                    $("#PhaseId").hide();
                   
                }
            }
            else if (dataItem.ServiceTypeName == 'Water') {//added by divya on 30Oct2017
                $('#txtQty').attr('readonly', true);
                $("#StartMeterReading").show();
                $("#EndMeterReading").show();               
                $("#PhaseId").hide();
               // $("spnQuantityEmpty").hide();
                $('#spnQuantityEmpty').hide();
            }          
            
            self.suppMiscRecordingModel().ServiceTypeCode(dataItem.ServiceTypeCode);
        }

        //added by divya on 31Oct 2017--to remove the validation message after the textbox is filled.and after selecting the phase
        self.OnNoFocus = function (e) {//for start meter reading    
            
            $("#spnStartReadingEmpty").hide();           
            $("#spnStartReadingValid").hide();
            var StartMerterVal = $("#txtStartMeterReading").val();//clear the values 
            var EndMeterVal = $("#txtEndtMeterReading").val();
            if (EndMeterVal == '') {
            }
            else {
                if (StartMerterVal != "" && EndMeterVal != "") {
                    $("#spnStartReadingEmpty").hide();
                    $("#spnEndReadingEmpty").hide();
                    if (parseInt(StartMerterVal) == 0) {
                        $("#spnStartReadingValid").show();
                        $("#spnStartReadingValid").text('* Start Meter Reading should be greater than zero');
                        errors = 1;
                    }
                    if (parseInt(EndMeterVal) == 0) {
                        $("#spnEndReadingValid").show();
                        $("#spnEndReadingValid").text('* End Meter Reading should be greater than zero');
                        errors = 1;
                    }
                    if (parseInt(StartMerterVal) == parseInt(EndMeterVal)) {
                        $("#spnStartReadingEmpty").hide();
                        $("#spnEndReadingValid").show();
                        $("#spnEndReadingValid").text('* Start Meter Reading and End Meter Reading cannot be same');
                        errors = 1;
                    }
                    else if (parseInt(StartMerterVal) > parseInt(EndMeterVal)) {
                        $("#spnEndReading").hide();
                        $("#spnStartReadingValid").show();
                        $("#spnStartReadingValid").text('* Start Meter Reading should not be greater than End Meter Reading ');
                        errors = 1;
                    }
                    else {
                        var txtQtyval = (parseInt(EndMeterVal) - parseInt(StartMerterVal))
                        $('#txtQty').val(txtQtyval);
                    }
                }
            }
        }
        self.OnNoFocusEnd = function (e) {//for end meter reading 
            $("#spnEndReadingEmpty").hide();
            $("#spnEndReadingValid").hide();
            $("#spnStartReadingValid").hide();
            var StartMerterVal = $("#txtStartMeterReading").val();//clear the values 
            var EndMeterVal = $("#txtEndtMeterReading").val();
            if (StartMerterVal != "" && EndMeterVal != "") {
                $("#spnStartReadingEmpty").hide();
                $("#spnEndReadingEmpty").hide();
                if (parseInt(StartMerterVal) == 0) {
                    $("#spnStartReadingValid").show();
                    $("#spnStartReadingValid").text('* Start Meter Reading should be greater than zero');
                    errors = 1;
                }
                if (parseInt(EndMeterVal) == 0) {
                    $("#spnEndReadingValid").show();
                    $("#spnEndReadingValid").text('* End Meter Reading should be greater than zero');
                    errors = 1;
                }
                if (parseInt(StartMerterVal) == parseInt(EndMeterVal)) {
                    $("#spnStartReadingEmpty").hide();
                    $("#spnEndReadingValid").show();
                    $("#spnEndReadingValid").text('* Start Meter Reading and End Meter Reading cannot be same');
                    errors = 1;
                }
                else if (parseInt(StartMerterVal) > parseInt(EndMeterVal)) {
                    $("#spnEndReading").hide();
                    $("#spnStartReadingValid").show();
                    $("#spnStartReadingValid").text('* Start Meter Reading should not be greater than End Meter Reading ');
                    errors = 1;
                }
                else {
                    var txtQtyval = (parseInt(EndMeterVal) - parseInt(StartMerterVal))
                    $('#txtQty').val(txtQtyval);
                }
            }
           
            
        }
        self.OnQtyFocusEnd = function (e) {
            $('#spnQuantityEmpty').hide();
        }
        self.OnSelectPhase = function (e) {//phase drop down
            $("#spnphase").hide();
            
        }
       

       

        FromDatecalOpen = function () {
            //Date Validation starts here
            $("#FromDate").data('kendoDateTimePicker').min(self.MinimumDate());
            $("#FromDate").data('kendoDateTimePicker').max(self.MaximumDate());
            self.suppMiscRecordingModel().ToDateTime('');
            $("#ToDate").data('kendoDateTimePicker').min(self.MinimumDate());
            $("#ToDate").data('kendoDateTimePicker').max(self.MaximumDate());
            //Date Validation ends here
        }

        ToDatecalOpen = function () {          
            //Date Validation starts here
            var StartDateValue = $("#FromDate").val();  
            $("#FromDate").data('kendoDateTimePicker').min(self.MinimumDate());
            $("#FromDate").data('kendoDateTimePicker').max(self.MaximumDate());
            if (StartDateValue == "" || StartDateValue == null) {
                $("#ToDate").data('kendoDateTimePicker').min(self.MinimumDate());
            }
            else {
                $("#ToDate").data('kendoDateTimePicker').min(StartDateValue);

            }
            $("#ToDate").data('kendoDateTimePicker').max(self.MaximumDate());
            //Date Validation ends here
        }

        self.Initialize();
    }
    IPMSRoot.SuppMiscServiceViewModel = SuppMiscServiceViewModel;


}(window.IPMSROOT));

