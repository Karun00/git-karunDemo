(function (IPMSRoot) {

    var FuelReceiptViewModel = function (fuelreceiptid, viewDetail) {

        var self = this;
        $('#spnTitile').html("Fuel Receipt");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.fuelReceiptModel = ko.observable();
        self.FuelReceiptList = ko.observableArray();
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
        self.startDate = ko.observable();
        self.IsPrintEnable = ko.observable(false);
        //self.craftNameList = ko.observableArray();

        self.fuelReceiptReferenceData = ko.observable();


        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.fuelReceiptModel(new IPMSROOT.FuelReceiptModel());
            self.LoadInitialData();
            self.LoadFuelReceipts();       
            self.viewMode('List');
            //self.startDate(new Date());
            self.fuelReceiptModel().startDate(new Date());
            self.fuelReceiptModel().ReceiptstartDate(new Date());

        }

        self.LoadFuelReceipts = function () {
            if (viewDetail == true) {          

                self.viewModelHelper.apiGet('api/FuelReceiptFuelID/' + fuelreceiptid,
                 { fuelreceiptid: fuelreceiptid },
                  function (result) {
                      self.FuelReceiptList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.FuelReceiptModel(item);
                      }));
                      self.viewfuelreceipt(self.FuelReceiptList()[0]);
                  });
            }
            else {            

            self.viewModelHelper.apiGet('api/FuelReceipt',
                null,
                  function (result) { 
                      self.FuelReceiptList(ko.utils.arrayMap(result, function (item) {  
                          return new IPMSRoot.FuelReceiptModel(item);
                      }));


                  });

           }
        }

        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/FuelReceiptReferenceData', null,
                    function (result1) {
                        //TODO: Load this into self.ReferenceData().  and do it only once. not every time.
                        self.fuelReceiptReferenceData(new IPMSRoot.FuelReceiptReferenceData(result1));
                    }, null, null, false);
        }

        self.PortSelect = function (e) {           
            var dataItem = this.dataItem(e.item.index());
            self.fuelReceiptModel().PortCode(dataItem.PortCode);
            self.fuelReceiptModel().QuayCode(dataItem.QuayCode);
            self.fuelReceiptModel().BerthName(dataItem.BerthName);
        }

        self.SupplyingSelect = function (e) {          
            var dataItem = this.dataItem(e.item.index());
            if (dataItem.SubCatCode == "SOTH") {
                $("#spanother").show();
            }
            else {
                $("#spanother").hide();
            }
        
        }      
     



        self.editfuelreceipt = function (fuelreceipt) {          
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(false);
            self.IsPrintEnable(false);
            var fuelrequestionid = fuelreceipt.FuelRequisitionID();
           // self.fuelReceiptModel(fuelreceipt);
            self.viewModelHelper.apiGet('api/FuelReceipt/' + fuelrequestionid,
                { fuelrequestionid: fuelrequestionid },
                 function (result) {                  
                     ko.utils.arrayMap(result, function (item) {                    
                         self.fuelReceiptModel(new IPMSRoot.FuelReceiptModel(item));                      
                         if (self.fuelReceiptModel().SupplyingModeCode() == "SOTH") {
                             $("#spanother").show();
                         }                   

                        // self.fuelReceiptModel().PumpStartDateTime(new Date(Date.parse(self.fuelReceiptModel().PumpStartDateTime())));
                        // self.fuelReceiptModel().PumpFinishDateTime(new Date(Date.parse(self.fuelReceiptModel().PumpFinishDateTime())));                      
                         self.fuelReceiptModel().startDate(new Date(fuelreceipt.RequiredDate()));
                         self.fuelReceiptModel().ReceiptstartDate(new Date(fuelreceipt.RequiredDate()));

                         if (self.fuelReceiptModel().PumpFinishDateTime() != "" && self.fuelReceiptModel().PumpStartDateTime() != "") { 
                             //var datediff1 = Math.round((self.fuelReceiptModel().PumpFinishDateTime() - self.fuelReceiptModel().PumpStartDateTime()) / 1000 / 60);
                             //self.fuelReceiptModel().TotalTime(datediff1);                          
                             var strtdt = new Date(Date.parse(self.fuelReceiptModel().PumpStartDateTime()));
                             var enddt = new Date(Date.parse(self.fuelReceiptModel().PumpFinishDateTime()));

                             var delta = Math.abs(enddt - strtdt) / 1000;
                             var minutes = Math.floor(delta / 60);
                             self.fuelReceiptModel().TotalTime(minutes);

                              strtdt = self.fuelReceiptModel().PumpStartDateTime();
                              enddt = self.fuelReceiptModel().PumpFinishDateTime();

                              //self.fuelReceiptModel().PumpStartDateTime(moment(self.fuelReceiptModel().PumpStartDateTime()).format('YYYY-MM-DD HH:mm'));
                              //self.fuelReceiptModel().PumpFinishDateTime(moment(self.fuelReceiptModel().PumpFinishDateTime()).format('YYYY-MM-DD HH:mm'));

                              $("#PumpStartDateTime").val(moment(self.fuelReceiptModel().PumpStartDateTime()).format('YYYY-MM-DD HH:mm'));
                              $("#PumpFinishDateTime").val(moment(self.fuelReceiptModel().PumpFinishDateTime()).format('YYYY-MM-DD HH:mm'));
                          
                         }


                     });
                 });
       
            $('#spnTitile').html("Update Fuel Receipt");

        }

        self.viewfuelreceipt = function (fuelreceipt) {         
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            if (fuelreceipt.FuelReceiptStatus() == "Acknowledge") {
                self.IsPrintEnable(true);
            }
            // self.fuelReceiptModel(fuelreceipt);
            var fuelrequestionid = fuelreceipt.FuelRequisitionID();
            self.viewModelHelper.apiGet('api/FuelReceipt/' + fuelrequestionid,
              { fuelrequestionid: fuelrequestionid },
               function (result) {                
                   ko.utils.arrayMap(result, function (item) {                    
                       self.fuelReceiptModel(new IPMSRoot.FuelReceiptModel(item));
                       if (self.fuelReceiptModel().SupplyingModeCode() == "SOTH") {
                           $("#spanother").show();
                       }
               
                       $("#PumpStartDateTime").data('kendoDateTimePicker').enable(false);
                       $("#PumpFinishDateTime").data('kendoDateTimePicker').enable(false);
                       $("#ReceiptDate").data('kendoDatePicker').enable(false);
                       if (self.fuelReceiptModel().PumpFinishDateTime() != "" && self.fuelReceiptModel().PumpStartDateTime() != "") {
                           var strtdt = new Date(Date.parse(self.fuelReceiptModel().PumpStartDateTime()));
                           var enddt = new Date(Date.parse(self.fuelReceiptModel().PumpFinishDateTime()));
                           // var datediff1 = Math.abs((self.fuelReceiptModel().PumpFinishDateTime() - self.fuelReceiptModel().PumpStartDateTime()) / 1000);         
                           var delta = Math.abs(enddt - strtdt) / 1000;
                           var minutes = Math.floor(delta / 60);
                           self.fuelReceiptModel().TotalTime(minutes);

                           self.fuelReceiptModel().PumpStartDateTime(moment(self.fuelReceiptModel().PumpStartDateTime()).format('YYYY-MM-DD HH:mm'));
                           self.fuelReceiptModel().PumpFinishDateTime(moment(self.fuelReceiptModel().PumpFinishDateTime()).format('YYYY-MM-DD HH:mm'));
                       }
                   });
               });

            $('#spnTitile').html("View Fuel Receipt");   
            var ReferenceID = fuelreceipt.FuelReceiptID();
            var WorkflowInstanceID = fuelreceipt.WorkflowInstanceId();
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
                                 self.fuelReceiptModel().pendingTasks.push(pendingtaskaction);
                             });
                         });
        }

   
        self.ModifyFuelReceipt = function (model) {
          //  model.WaterContent($("#WaterContent").val());
            model.validationEnabled(true);
            self.FuelReceiptValidation = ko.observable(model);
            self.FuelReceiptValidation().errors = ko.validation.group(self.FuelReceiptValidation());
            var errors = self.FuelReceiptValidation().errors().length;                  

            if (model.ReceiptDate() == "Invalid date") {
                model.ReceiptDate("");
                errors = 1;              
            }
            if (model.PumpStartDateTime() == "Invalid date") {
                model.PumpStartDateTime("");
                errors = 1;
            }
            if (model.PumpFinishDateTime() == "Invalid date") {
                model.PumpFinishDateTime("");
                errors = 1;
            }
            if (model.SupplyingModeCode() == "SOTH") {
                if (model.Other() == "") {
                    $("#spanother1").show();
                    $("#spanother1").text('* Please Specify Other');
                    errors = 1;                   
                }               
            }
            if (model.WaterContent() > 100) {
                $("#spanwater").show();
                $("#spanwater").text('* Please enter Water Content between 0 to 100');                
                errors = 1;
            }
        

            if (errors == 0) {
                self.viewModelHelper.apiPost('api/FuelReceipt', ko.mapping.toJSON(model), function Message(data) {
                    model.RecordStatus(data.RecordStatus);
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Fuel Receipt details updated successfully", "Fuel Receipt");
                    self.LoadFuelReceipts();
                    $('#spnTitile').html("Fuel Receipt");
                    self.viewMode('List');

                });               
            }
            else {
                self.FuelReceiptValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
                //$('#divValidationError').removeClass('display-none');
                return;
            }

        }


        self.ResetFuelReceipt = function (model) {
            //$('#divValidationError').addClass('display-none');
            ko.validation.reset();
            model.validationEnabled(false);
            self.fuelReceiptModel().reset();
           // self.FuelReceiptValidation().errors.showAllMessages(false);
            if (self.fuelReceiptModel().SupplyingModeCode() == "SOTH") {
                $("#spanother").show();
            }
            else {
                $("#spanother").hide();
            }


        }

        self.CancelFuelReceipt = function () {
            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.viewMode('List');
                self.fuelReceiptModel().reset();
                $('#spnTitile').html("Fuel Receipt");
            }

        }

        //self.AssignGrade = function (e) {
        //    debugger;
        //    var dataItem = this.dataItem(e.item.index());            
        //    self.fuelReceiptModel().Grade(dataItem.SubCatName);
        //}

        HandleOtherKeyUp = function (data, event) {
            $('#spanother1').hide();
        }


        AssignGrade = function (event) {
            //self.fuelReceiptModel().Grade(dataItem.SubCatName);       
            self.fuelReceiptModel().Grade($("#GradeType").data("kendoDropDownList").text());
        }


        ChangeBerth = function (data, event) {

            var BerthList = self.fuelReceiptReferenceData().Berths();

            $.each(BerthList, function (index, berth) {
                if (berth.BerthCode() == data.BerthCode()) {
                    self.fuelReceiptModel().PortCode(berth.PortCode());
                    self.fuelReceiptModel().QuayCode(berth.QuayCode());
                    self.fuelReceiptModel().BerthName(berth.BerthName());
                }
            });
        }



        ValidDateReceipt = function (data, event) {          

            var some = JSON.parse(ko.toJSON(data));
            //  var ReceiptDate = ReceiptDate.value;              



          //  var ReceiptDate = new Date(Date.parse(data.ReceiptDate()));

            var ReceiptDate = data.ReceiptDate();
            var myDatePicker = new Date(ReceiptDate);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = '00';
            var Mnt = '00';

            self.fuelReceiptModel().ReceiptDate($("#ReceiptDate").val());

            $("#PumpStartDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
          //  $("#PumpStartDateTime").data('kendoDateTimePicker').min(ReceiptDate);
            $("#PumpFinishDateTime").data('kendoDateTimePicker').min(ReceiptDate);

            self.fuelReceiptModel().PumpStartDateTime("");
            self.fuelReceiptModel().PumpFinishDateTime("");
            self.fuelReceiptModel().TotalTime("");
        }

              



        ValidDateStart = function (data, event) {       
            var some = JSON.parse(ko.toJSON(data));
            //var startDate = some.PumpStartDateTime;
            var startDate = new Date(Date.parse(data.PumpStartDateTime()));
         //   var endDate = PumpFinishDateTime.value;     
            debugger;
            $("#PumpFinishDateTime").data('kendoDateTimePicker').min(startDate);

            self.fuelReceiptModel().PumpFinishDateTime("");

         //   self.fuelReceiptModel().PumpStartDateTime($("#PumpStartDateTime").val());

            //var pumpstart = new Date(Date.parse(data.PumpStartDateTime()));
            //var pumpfinish = new Date(Date.parse(data.PumpFinishDateTime()));



            var pumpstart = data.PumpStartDateTime();
            var pumpfinish = data.PumpFinishDateTime();
            if (pumpstart != "" && pumpfinish != "") {
                //var datediff1 = Math.round((pumpfinish - pumpstart) / 1000 / 60);            
                //self.fuelReceiptModel().TotalTime(datediff1);
              
                var delta = Math.abs(pumpfinish - pumpstart) / 1000;
                var minutes = Math.floor(delta / 60);
                self.fuelReceiptModel().TotalTime(minutes);
            }
            else if (pumpstart == "" || pumpfinish == "") {
                self.fuelReceiptModel().TotalTime("");
            }

        }



        ValidDateEnd = function (data, event) {

            debugger;

            //self.fuelReceiptModel().PumpFinishDateTime($("#PumpFinishDateTime").val());        

            //var pumpstart = new Date(self.fuelReceiptModel().PumpStartDateTime());
            //var pumpfinish = new Date(self.fuelReceiptModel().PumpFinishDateTime());

        
            var pumpstart = new Date(data.PumpStartDateTime());
            var pumpfinish = new Date(data.PumpFinishDateTime());
        
            if (pumpstart != "" && pumpfinish != "") {
                //var datediff1 = Math.round((pumpfinish - pumpstart) / 1000 / 60);              
                //self.fuelReceiptModel().TotalTime(datediff1);

                var delta = Math.abs(pumpfinish - pumpstart) / 1000;
                var minutes = Math.floor(delta / 60);
                self.fuelReceiptModel().TotalTime(minutes);
            }
            else if (pumpstart == "" || pumpfinish == "") {
                self.fuelReceiptModel().TotalTime("");
            }
        }



        ValidDate = function (data, event) {        
            var some = JSON.parse(ko.toJSON(data));         
            var startDate = PumpStartDateTime.value;
            var endDate = PumpFinishDateTime.value;  
           
            if (startDate) {

                $("#PumpFinishDateTime").kendoDateTimePicker({
                    min: startDate,
                    format: "yyyy-MM-dd HH:mm",
                    parseFormats: ["yyyy-MM-dd", "HH:mm"],
                    timeFormat: "HH:mm"
                });
            }
            if (endDate) {
                self.fuelReceiptModel().PumpFinishDateTime(endDate);
            }
      
            var pumpstart = new Date(Date.parse(data.PumpStartDateTime()));
            var pumpfinish = new Date(Date.parse(data.PumpFinishDateTime()));     
            if (pumpstart != "Invalid Date" && pumpfinish != "Invalid Date") {
                var datediff1 = Math.round((pumpfinish - pumpstart) / 1000 / 60);           
                self.fuelReceiptModel().TotalTime(datediff1);
                // alert("date");
            }
            else if (pumpstart == "Invalid Date" || pumpfinish != "Invalid Date") {
                self.fuelReceiptModel().TotalTime("");
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
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.fuelReceiptModel());
        }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        self.PDFGeneration = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
       

            var doc = new jsPDF();
            var yLocation = 20;
            var xLocation = 10;
            var FontSize = 9;

            yLocation = yLocation + 5;
            doc.setFontSize(15);
            doc.setFontType("bold");
            doc.text(75, yLocation, 'Fuel Receipt');
            doc.setFontType("normal");
            doc.setFontSize(FontSize);
            // doc.text(150, yLocation, 'PERMIT No: ' + $("#CraftType").text());
            debugger;


            var CraftName =  model.CraftName();
            

            ////////////////////
            yLocation = yLocation + 3;
            doc.setLineWidth(0.5);
            doc.line(10, yLocation, 200, yLocation);

            yLocation = yLocation + 10;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Requisition No.');
            doc.text(35, yLocation, ':');
            doc.text(40, yLocation, model.FuelRequistionNo());

          
            doc.setFontSize(FontSize);
            doc.text(75, yLocation, 'Craft Name');
            doc.text(95, yLocation, ':');
            // doc.text(100, yLocation, trimToLen(CraftName, 15));
            doc.text(100, yLocation, CraftName.substr(0, 15));
    

            doc.setFontSize(FontSize);
            doc.text(130, yLocation, 'Craft Code');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, model.CraftCode());

            ///////////////////////
            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(35, yLocation, '');
            doc.text(40, yLocation, '');


            doc.setFontSize(FontSize);
            doc.text(75, yLocation, '');
            doc.text(95, yLocation, '');
            doc.text(100, yLocation, CraftName.substr(15, 15));

            ////////////////////////

            var CraftType = model.CraftType();
            var IMONo = model.IMONo();


            ////////////////////
            yLocation = yLocation + 7;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Craft Type');
            doc.text(35, yLocation, ':');
            doc.text(40, yLocation,CraftName.substr(0, 15));

        
            doc.setFontSize(FontSize);
            doc.text(75, yLocation, 'IMO No.');
            doc.text(95, yLocation, ':');
            doc.text(100, yLocation, IMONo.substr(0, 15));

            doc.setFontSize(FontSize);
            doc.text(130, yLocation, 'Oil Type');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, model.OilType());

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(35, yLocation, '');
            doc.text(40, yLocation, CraftName.substr(15, 15));


            doc.setFontSize(FontSize);
            doc.text(75, yLocation, '');
            doc.text(95, yLocation, '');
            doc.text(100, yLocation, IMONo.substr(15, 15));

            doc.setFontSize(FontSize);
            doc.text(130, yLocation, '');
            doc.text(160, yLocation, '');
            doc.text(165, yLocation, '');
          
            

            ////////////////////
            yLocation = yLocation + 7;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'UOM');
            doc.text(35, yLocation, ':');
            doc.text(40, yLocation, model.UOMName());
        
            doc.setFontSize(FontSize);
            doc.text(75, yLocation, 'Quantity(Ltrs)');
            doc.text(95, yLocation, ':');
            doc.text(100, yLocation, $("#Quantity").text());

            doc.setFontSize(FontSize);
            doc.text(130, yLocation, 'Date Required');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, model.RequiredDate());

            //////////////////
            var ModeID =  model.ModeID();

            yLocation = yLocation + 7;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Supplying Mode');
            doc.text(35, yLocation, ':');
            doc.text(40, yLocation, $("#SupplyingMode").data("kendoDropDownList").text());


            doc.setFontSize(FontSize);
            doc.text(75, yLocation, 'Mode ID');
            doc.text(95, yLocation, ':');
            doc.text(100, yLocation, ModeID.substr(0, 13));

            doc.setFontSize(FontSize);
            doc.text(130, yLocation, 'Berth');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#Berth").data("kendoDropDownList").text());

            /////////////////////////////////

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(35, yLocation, '');
            doc.text(40, yLocation, '' );


            doc.setFontSize(FontSize);
            doc.text(75, yLocation, '');
            doc.text(95, yLocation, '');
            doc.text(100, yLocation, ModeID.substr(13, 15));

            doc.setFontSize(FontSize);
            doc.text(130, yLocation, '');
            doc.text(160, yLocation, '');
            doc.text(165, yLocation, '');

            //////////////////////////////////////////////////


            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(35, yLocation, '');
            doc.text(40, yLocation, '');


            doc.setFontSize(FontSize);
            doc.text(75, yLocation, '');
            doc.text(95, yLocation, '');
            doc.text(100, yLocation, ModeID.substr(28, 22));

            doc.setFontSize(FontSize);
            doc.text(130, yLocation, '');
            doc.text(160, yLocation, '');
            doc.text(165, yLocation, '');

            //////////////////////////////////////////////////



            if ($("#SupplyingMode").data("kendoDropDownList").text() == "Other") {     
                
                var Other = $("#Other").val();
                yLocation = yLocation + 7;
                doc.setFontSize(FontSize);
                doc.text(10, yLocation, 'Other');
                doc.text(35, yLocation, ':');
                doc.text(40, yLocation, Other.substr(0, 15));

                yLocation = yLocation + 4;
                doc.setFontSize(FontSize);
                doc.text(10, yLocation, '');
                doc.text(35, yLocation, '');
                doc.text(40, yLocation, Other.substr(15, 15));

                yLocation = yLocation + 4;
                doc.setFontSize(FontSize);
                doc.text(10, yLocation, '');
                doc.text(35, yLocation, '');
                doc.text(40, yLocation, Other.substr(30, 15));
            }



            ////////////////////

            var Flag = model.Flag();

            yLocation = yLocation + 7;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Receipt Date');
            doc.text(35, yLocation, ':');
            doc.text(40, yLocation, model.ReceiptDate());


            doc.setFontSize(FontSize);
            doc.text(75, yLocation, 'Flag');
            doc.text(95, yLocation, ':');
            doc.text(100, yLocation, Flag.substr(0, 15));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(35, yLocation, '');
            doc.text(40, yLocation, '');


            doc.setFontSize(FontSize);
            doc.text(75, yLocation, '');
            doc.text(95, yLocation, '');
            doc.text(100, yLocation, Flag.substr(15, 15));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(35, yLocation, '');
            doc.text(40, yLocation, '');


            doc.setFontSize(FontSize);
            doc.text(75, yLocation, '');
            doc.text(95, yLocation, '');
            doc.text(100, yLocation, Flag.substr(30, 15));


            //////////////////////
            yLocation = yLocation + 10;
            doc.setFontType("bold");
            doc.text(10, yLocation, 'DIP/FM Readings : ');
            doc.setFontType("normal");
            yLocation = yLocation + 1;
            doc.setLineWidth(0.5);
            doc.line(10, yLocation, 38, yLocation);

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Grade');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#GradeType").data("kendoDropDownList").text());

         
            doc.setFontSize(FontSize);
            doc.text(110, yLocation, 'Start Reading (Ltrs)');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#StartReading").val());

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Finish Reading (Ltrs)');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#FinishReading").val());

            doc.setFontSize(FontSize);
            doc.text(110, yLocation, 'Received Qty (Ltrs)');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#ReceivedQty").val());



            ////////////////////
            yLocation = yLocation + 10;
            doc.setFontType("bold");
            doc.text(10, yLocation, 'Temperature Readings : ');
            doc.setFontType("normal");
            yLocation = yLocation + 1;
            doc.setLineWidth(0.5);
            doc.line(10, yLocation, 38, yLocation);

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Received Temp (Degree Celsius)');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#ReceivedTempCelsius").val());


            doc.setFontSize(FontSize);
            doc.text(110, yLocation, 'VCF');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#VCF").val());

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Qtty @ 20 Degree Celsius (Ltrs)');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#Qttyat20Degree1").val());

            doc.setFontSize(FontSize);
            doc.text(110, yLocation, 'Qtty @ 20 Degree Celsius (Ltrs)');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#Qttyat20Degree2").val());


            ////////////////////
            yLocation = yLocation + 10;
            doc.setFontType("bold");
            doc.text(10, yLocation, 'Pumping Times : ');
            doc.setFontType("normal");
            yLocation = yLocation + 1;
            doc.setLineWidth(0.5);
            doc.line(10, yLocation, 38, yLocation);

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Start Date / Time');
            doc.text(35, yLocation, ':');
            doc.text(40, yLocation, $("#PumpStartDateTime").val());


            doc.setFontSize(FontSize);
            doc.text(70, yLocation, 'End Date / Time');
            doc.text(95, yLocation, ':');
            doc.text(100, yLocation, $("#PumpFinishDateTime").val());

            doc.setFontSize(FontSize);
            doc.text(130, yLocation, 'Total Time (mins)');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#TotalTime").text());


            //////////////////////
            yLocation = yLocation + 10;
            doc.setFontType("bold");
            doc.text(10, yLocation, 'Product Information : ');
            doc.setFontType("normal");
            yLocation = yLocation + 1;
            doc.setLineWidth(0.5);
            doc.line(10, yLocation, 38, yLocation);

            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Density @ 15 Degree Celsius');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#Densityat15DegCelsius").val());

            doc.setFontSize(FontSize);
            doc.text(110, yLocation, 'Density @ 20 Degree Celsius');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#Densityat20DegCelsius").val());


            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Flash Point');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#FlashPoint").val());

            doc.setFontSize(FontSize);
            doc.text(110, yLocation, 'Batch No.');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#BatchNo").val());


            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Kinematic Visc. @ 50 Deg Celsius');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#KinematicViscat50DegCelsius").val());

            doc.setFontSize(FontSize);
            doc.text(110, yLocation, 'Water Content (0-100%)');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#WaterContent").val());


            yLocation = yLocation + 5;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Sulphur Content');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation, $("#SulphurContent").val());

            doc.setFontSize(FontSize);
            doc.text(110, yLocation, 'Supplier');
            doc.text(160, yLocation, ':');
            doc.text(165, yLocation, $("#Supplier").val());

            var Remarks =  $("#Remarks").val();

            yLocation = yLocation + 8;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, 'Remarks');
            doc.text(65, yLocation, ':');
            doc.text(70, yLocation,  Remarks.substr(0, 70));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(65, yLocation, '');
            doc.text(70, yLocation, Remarks.substr(70, 70));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(65, yLocation, '');
            doc.text(70, yLocation, Remarks.substr(140, 70));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(65, yLocation, '');
            doc.text(70, yLocation, Remarks.substr(210, 70));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(65, yLocation, '');
            doc.text(70, yLocation, Remarks.substr(280, 70));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(65, yLocation, '');
            doc.text(70, yLocation, Remarks.substr(350, 70));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(65, yLocation, '');
            doc.text(70, yLocation, Remarks.substr(420, 70));

            yLocation = yLocation + 4;
            doc.setFontSize(FontSize);
            doc.text(10, yLocation, '');
            doc.text(65, yLocation, '');
            doc.text(70, yLocation, Remarks.substr(490, 70));

         

            
            //// Save the PDF
            doc.save('FuelReceipt.pdf');

           
          //  $("#classadd div").removeClass("modal-backdrop fade in");
         //   self.LoadGrid();
         //   self.viewMode('List');
         //   $('#spnTitile').html("Hot Work Permit Inspection List");
         //   $("#SuppHotWorkInspection div").trigger("click");
        }

        function trimToLen(str, maxLen) {
            var trimmed = str.substr(0, maxLen);
            return trimmed.substr(0, trimmed.lastIndexOf(' ')) + '';
        }


        self.viewWorkFlow = function (fuelreceipt) {
            var workflowinstanceId = fuelreceipt.WorkflowInstanceId();
            self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
             { workflowinstanceId: workflowinstanceId },
              function (result) {

                  self.fuelReceiptModel(new IPMSROOT.FuelReceiptModel());
                  self.fuelReceiptModel().WorkFlowRemarks(result);
                  $('#WorkFlowRemarks').text(result);
                  $('#stack1').modal('show');

              });

        }




        self.Initialize();
    }
    IPMSRoot.FuelReceiptViewModel = FuelReceiptViewModel;


}(window.IPMSROOT));



