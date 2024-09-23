(function (IPMSRoot) {

    var SAPPostingViewModel = function () {

        var self = this;
        $('#spnTitile').html("SAP Posting");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.sapPostingModel = ko.observable();
        self.SAPPostingList = ko.observableArray();
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
        self.vcnList = ko.observableArray();
        self.IsEditable = ko.observable(true);
        self.sapPostingReferenceData = ko.observable();
        self.sapPostingModelItem = ko.observable();
        self.IsReasonEnable = ko.observable(true);
        self.IsGenerateEnable = ko.observable(true);

        self.AccountList = ko.observableArray();


        self.Initialize = function () {

            self.viewMode = ko.observable(true);
            self.sapPostingModel(new IPMSROOT.SAPPostingModel());
            self.LoadInitialData();
            self.viewMode('Form');

        }


        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/SAPPostingReferenceData', null,
                    function (result1) {
                        //TODO: Load this into self.ReferenceData().  and do it only once. not every time.
                        self.sapPostingReferenceData(new IPMSRoot.SAPPostingReferenceData(result1));
                    }, null, null, false);
        }
       
        self.VCNSelect = function (e) {

            var dataItem = this.dataItem(e.item.index());
            self.sapPostingModel().VCN(dataItem.VCN);           
            //self.sapPostingModel().ReasonForVisit(dataItem.ReasonForVisit);
            //self.sapPostingModel().VesselType(dataItem.VesselType);

        }

        self.SaveSAPPosting = function (model) {
            model.validationEnabled(true);
            self.SAPPostingValidation = ko.observable(model);
            self.SAPPostingValidation().errors = ko.validation.group(self.SAPPostingValidation());
            var errors = self.SAPPostingValidation().errors().length;
            errors = 0;
            model.Reason("02");
            if (model.TransmitData() == '' || model.TransmitData() == null)
            {
                toastr.warning("You dont have sufficient XML data to post");
                return;
            }
            else
            {
             if (model.MessageType() == 'CRAR') {

                 var chkMsg = model.TransmitData();
                 var kunnr = $(chkMsg).find("KUNNR").text();

                 if (kunnr == '' || kunnr == null || kunnr == 0) {
                     toastr.warning("SAP Vessel No. is mandatory to post data");
                     errors = 1;
                     return;
                 }
             }

            if (errors == 0) {
                self.viewModelHelper.apiPost('api/SAPPosting', ko.mapping.toJSON(model), function Message(data) {

                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    var retMsg = "";
                    if (data.MessageType == 'CRAR')
                    {
                        retMsg = "Vessel Arrival Create Request Submitted Successfully.";
                    } else
                        if (data.MessageType == 'UPAR')
                        {
                            retMsg = "Vessel Arrival Update Request Submitted Successfully.";
                        }
                        else
                            if (data.MessageType == 'CRMO')
                            {
                                retMsg = "Marine Order Create Request Submitted Successfully.";
                            }
                            else
                                if(data.MessageType == 'UPMO')
                                {
                                    retMsg = "Marine Order Update Request Submitted Successfully.";
                                }
                                else
                                    if (data.MessageType == 'CRIN') {
                                        retMsg = "Invoice Request Submitted Successfully.";
                                    }
                    toastr.success(retMsg, "SAP Posting");
                    self.SAPPostingList.removeAll();
                    self.sapPostingModel().reset();
                    $('#spnTitile').html("SAP Posting");
                    self.viewMode('Form');
                });
            }
            else {
                self.SAPPostingValidation().errors.showAllMessages();
                toastr.warning("You have some form errors. Please check below.");
                return;
            }
        }
        }

        self.UpdateMarinePosting = function (model) {
            self.viewMode('Form1');
            self.IsCodeEnable(false);
            self.IsReasonEnable(true);
            self.editableView(true);
            self.IsSaveUpdateDisabled(false);
            self.IsGenerateEnable(true);
            var VCN = self.sapPostingModel().VCN();
            var MsgType = model.MsgType();
            var PostingStatus = model.PostStatus();
            self.sapPostingModel(new IPMSRoot.SAPPostingModel());
            var PortCode = self.sapPostingModel().PortCode();
            self.sapPostingModel().MessageType(model.MsgType());
            self.sapPostingModel().VCN(VCN);
            var ReceavedARRNO = ((model.ReceavedARRNO() != undefined && model.ReceavedARRNO() != '') ? model.ReceavedARRNO() : 'NA')
            var MarineAccNo = model.MarineAccNo() != undefined && model.MarineAccNo() != '' ? model.MarineAccNo() : 'NA';
            var MarinePostingId = model.MarinePostingId() != undefined && model.MarinePostingId() != '' ? model.MarinePostingId() : '0';
            var RevenueAgentAccNo = model.RevenueAgentAccNo() != undefined && model.RevenueAgentAccNo() != '' ? model.RevenueAgentAccNo() : 'NA';

            self.sapPostingModel().ReceavedARRNO(model.ReceavedARRNO());
            self.sapPostingModel().MarineAccNo(model.MarineAccNo());
            self.sapPostingModel().MarinePostingId(model.MarinePostingId());
            self.sapPostingModel().RevenueAgentAccNo(model.RevenueAgentAccNo());
            if (model.MsgType() == 'CRAR' || model.MsgType() == 'UPAR') {
                $("#ReasonID").show();
            }
            else if (model.MsgType() == 'CRMO' || model.MsgType() == "UPMO") {
                $("#ReasonID").hide();
            }

            if (VCN != "") {
                self.sapPostingModel().RevinueAccountNo(MarineAccNo);
                self.viewModelHelper.apiGet('api/MarineSAPPostingUpdatedtls/' + VCN + '/' + MsgType + '/' + ReceavedARRNO + '/' + MarineAccNo + '/' + MarinePostingId + '/' + PostingStatus + '/' + RevenueAgentAccNo,
              null,
                 function (result) {
                     var ValidRule = result.split('$');
                     if (ValidRule.length > 1) {
                         self.sapPostingModel().TransmitData(ValidRule[0]);
                         self.sapPostingModel().MarinePostingId(ValidRule[1]);
                     }
                     else {
                         self.sapPostingModel().TransmitData(result);
                     }

                 });

                self.viewModelHelper.apiGet('api/SAPPostingAccountInfo/' + VCN, // + '/' + PortCode,
              null,
                 function (result) {
                     ko.mapping.fromJS(result, {}, self.AccountList);
                 });
            }
        }


        self.AddPosting = function (model) {
            self.viewMode('Form1');
            self.IsCodeEnable(false);
            self.IsReasonEnable(true);
            self.editableView(true);
            //self.IsSave(false);
            self.IsSaveUpdateDisabled(false);
            self.IsGenerateEnable(true);
           
            var VCN = self.sapPostingModel().VCN();
            var MsgType = model.MsgType();
            var PostingStatus = "SNEW";

            self.sapPostingModel(new IPMSRoot.SAPPostingModel());
            var PortCode = self.sapPostingModel().PortCode();
            self.sapPostingModel().MessageType(model.MsgType());
            self.sapPostingModel().VCN(VCN);
            var ReceavedARRNO = ((model.ReceavedARRNO() != undefined && model.ReceavedARRNO() != '') ? model.ReceavedARRNO() : 'NA')
            var MarineAccNo = model.MarineAccNo() != undefined && model.MarineAccNo() != '' ? model.MarineAccNo() : 'NA';
            var RevenueAgentAccNo = model.RevenueAgentAccNo() != undefined && model.RevenueAgentAccNo() != '' ? model.RevenueAgentAccNo() : 'NA';
            self.sapPostingModel().ReceavedARRNO(model.ReceavedARRNO());
            self.sapPostingModel().MarineAccNo(model.MarineAccNo());
            var MarinePostingId = model.MarinePostingId() != undefined && model.MarinePostingId() != '' ? model.MarinePostingId() : '0';

            self.sapPostingModel().ReasonForVisit(model.ReasonForVisit());
            self.sapPostingModel().VesselType(model.VesselType());
            self.sapPostingModel().IsRepost(model.IsRepost());
            self.sapPostingModel().SAPPostingID(model.SAPPostingID());
            self.sapPostingModel().RevenueAgentAccNo(model.RevenueAgentAccNo());

            // By Mahesh : for showing span text depend on Message type 
            
            if (model.MsgType() == 'CRAR') {
                $('#spnTitile').html("SAP Posting Details – Vessel Arrival Create");
                $('#spnTitile10').html("Vessel Arrival Create");
            }
            else if (model.MsgType() == 'UPAR')
            {
                $('#spnTitile').html("SAP Posting Details – Vessel Arrival Update");
                $('#spnTitile10').html("Vessel Arrival Update");
                
            }
            else if (model.MsgType() == 'CRMO')
            {
                $('#spnTitile').html("SAP Posting Details – Marine Order Create");
                $('#spnTitile10').html("Marine Order Create");
            }
            else if (model.MsgType() == 'UPMO')
            {
                $('#spnTitile').html("SAP Posting Details – Marine Order Update");
                $('#spnTitile10').html("Marine Order Update");
            }
            

            ///////////

            if (model.MsgType() == 'CRAR' || model.MsgType() == 'UPAR') {             
                $("#ReasonID").show();
            }
            else if (model.MsgType() == 'CRMO' || model.MsgType() == "UPMO") {
                $("#ReasonID").hide();
            }
            
            if (VCN != "") {

                self.sapPostingModel().RevinueAccountNo(MarineAccNo);
                self.viewModelHelper.apiGet('api/SAPPostingAddDetails/' + VCN + '/' + MsgType + '/' + ReceavedARRNO + '/' + MarineAccNo + '/' + MarinePostingId + '/' + PostingStatus + '/' + RevenueAgentAccNo,
              null,
                 function (result) {
                     var ValidRule = result.split('$');
                     if (ValidRule.length > 1) {
                         self.sapPostingModel().TransmitData(ValidRule[0]);
                         self.sapPostingModel().MarinePostingId(ValidRule[1]);

                     }
                     else {
                         self.sapPostingModel().TransmitData(result);
                     }

                 });

                self.viewModelHelper.apiGet('api/SAPPostingAccountInfo/' + VCN, // + '/' + PortCode,
              null,
                 function (result) {
                     ko.mapping.fromJS(result, {}, self.AccountList);
                 });
            }
        }

            self.RePosting = function (model) {

                self.IsReasonEnable(true);
                self.editableView(true);
                self.IsSaveUpdateDisabled(true);
                self.IsGenerateEnable(false);
                var SAPPostingID = model.SAPPostingID();
                var MsgType = model.MsgType();
                self.sapPostingModel().Reason('');
                // By Mahesh : for showing span text depend on Message type 
                if (model.MsgType() == 'CRAR') {
                    $('#spnTitile').html("SAP Posting Details View – Vessel Arrival Create");
                    $('#spnTitile10').html("Vessel Arrival Create");
                }
                else if (model.MsgType() == 'UPAR') {
                    $('#spnTitile').html("SAP Posting Details View – Vessel Arrival Update");
                    $('#spnTitile10').html("Vessel Arrival Update");

                }
                else if (model.MsgType() == 'CRMO') {
                    $('#spnTitile').html("SAP Posting Details View – Marine Order Create");
                    $('#spnTitile10').html("Marine Order Create");
                }
                else if (model.MsgType() == 'UPMO') {
                    $('#spnTitile').html("SAP Posting Details View – Marine Order Update");
                    $('#spnTitile10').html("Marine Order Update");
                }

            

                ///////////
                if (model.MsgType() == 'CRAR' || model.MsgType() == 'UPAR') {
                    $("#ReasonID").show();
                }
                else if (model.MsgType() == 'CRMO' || model.MsgType() == "UPMO") {
                    $("#ReasonID").hide();
                }

                if (VCN != "") {
                    self.sapPostingModel().ReasonForVisit(model.ReasonForVisit());
                    self.sapPostingModel().VesselType(model.VesselType());
                    self.sapPostingModel().MarineAccNo(model.MarineAccNo());
                    self.sapPostingModel().MarinePostingId(model.MarinePostingId());

                    self.viewModelHelper.apiGet('api/SAPPostingDetails/' + SAPPostingID, // + '/' + PortCode,
                      null,

                     function (result) {

                         self.sapPostingModel().VCN(result.VCN);
                         self.sapPostingModel().PostingStatus(result.PostingStatus);
                         self.sapPostingModel().MessageType(result.MessageType);
                         var codeTag = "<CODE>#CODE#</CODE>";
                         var codeTag2 = codeTag.replace("#CODE#", result.Reason);
                         var poc = "<PORTCALL />"; var poc2 = " <PORTCALL></PORTCALL> ";
                         var por = "<PORTORIGIN />"; var por2 = "<PORTORIGIN></PORTORIGIN> ";
                          
                         result.TransmitData = result.TransmitData.replace(codeTag2, codeTag)
                                                                  .replace(poc, poc2)
                                                                  .replace(por, por2)
                             .replace("<KUNNR>0</KUNNR>", "<KUNNR>" + self.sapPostingModel().MarineAccNo() + "</KUNNR>");
                         self.sapPostingModel().TransmitData(result.TransmitData);
                       //  self.sapPostingModel().Reason(result.Reason);
                         self.sapPostingModel().RevenueAgentAccNo(result.RevenueAgentAccNo);
                         self.sapPostingModel().RevinueAccountNo(result.RevinueAccountNo);
                         self.sapPostingModel().MarinePostingId(result.MarinePostingId);
                         self.sapPostingModel().SAPPostingID(result.SAPPostingID);
                     });
                }
                self.viewMode('Form1');

            }

            self.ReasonChange = function (e) {
                var res = $("#Reason").val();
                if (res != undefined && res != null && res != "") {
                    self.IsGenerateEnable(true);
                }
                else {
                    self.IsGenerateEnable(false);
                }
            }

        self.ViewPosting = function (model) {

            self.IsReasonEnable(false);
            self.editableView(false);
            self.IsSaveUpdateDisabled(false);
            self.IsGenerateEnable(false);
            var SAPPostingID = model.SAPPostingID();
            var MsgType = model.MsgType();

            // By Mahesh : for showing span text depend on Message type 
            if (model.MsgType() == 'CRAR') {
                $('#spnTitile').html("SAP Posting Details View – Vessel Arrival Create");
                $('#spnTitile10').html("Vessel Arrival Create");
            }
            else if (model.MsgType() == 'UPAR') {
                $('#spnTitile').html("SAP Posting Details View – Vessel Arrival Update");
                $('#spnTitile10').html("Vessel Arrival Update");

            }
            else if (model.MsgType() == 'CRMO') {
                $('#spnTitile').html("SAP Posting Details View – Marine Order Create");
                $('#spnTitile10').html("Marine Order Create");
            }
            else if (model.MsgType() == 'UPMO') {
                $('#spnTitile').html("SAP Posting Details View – Marine Order Update");
                $('#spnTitile10').html("Marine Order Update");
            }

            

            ///////////
            if (model.MsgType() == 'CRAR' || model.MsgType() == 'UPAR') {
                $("#ReasonID").show();
            }
            else if (model.MsgType() == 'CRMO' || model.MsgType() == "UPMO") {
                $("#ReasonID").hide();
            }

            if (VCN != "") {
                self.sapPostingModel().ReasonForVisit(model.ReasonForVisit());
                self.sapPostingModel().VesselType(model.VesselType());
                self.sapPostingModel().MarineAccNo(model.MarineAccNo());
                self.viewModelHelper.apiGet('api/SAPPostingDetails/' + SAPPostingID, // + '/' + PortCode,
                  null,

                 function (result) {

                     self.sapPostingModel().VCN(result.VCN);
                     self.sapPostingModel().PostingStatus(result.PostingStatus);
                     self.sapPostingModel().MessageType(result.MessageType);
                     self.sapPostingModel().TransmitData(result.TransmitData);
                     self.sapPostingModel().Reason(result.Reason);
                 });
            }
            self.viewMode('Form1');

        }

        self.GenerateXML = function (model) {
         
            model.validationEnabled(true);
            self.SAPPostingValidation = ko.observable(model);
            self.SAPPostingValidation().errors = ko.validation.group(self.SAPPostingValidation());
            var errors = self.SAPPostingValidation().errors().length;
            //   $("#Reason").attr("disable", true);
            if (model.MessageType() == 'CRAR' || model.MessageType() == 'UPAR') {
                // if (model.Reason() != "" && model.AccountNo() != "") {
                if (model.Reason() != "") {
                    $("#Reason").data('kendoDropDownList').enable(false);
                   // $("#Account").data('kendoDropDownList').enable(false);
                    self.IsSaveUpdateDisabled(true);
                    self.IsGenerateEnable(false);
                }
                else {
                    self.SAPPostingValidation().errors.showAllMessages();
                    toastr.warning("You have some form errors. Please check below.");
                    return;
                }
                if(model.TransmitData() != null || model.TransmitData() == '')
                {
                    var chkMsg = model.TransmitData();
                    var portcall=$(chkMsg).find("PORTCALL").text();
                    var portorigin = $(chkMsg).find("PORTORIGIN").text();
                    var kunnr = $(chkMsg).find("KUNNR").text();                   
                    if (portcall == '' || portorigin == '') {
                        self.IsSaveUpdateDisabled(false);
                        self.IsGenerateEnable(true);
                        toastr.warning("Port Call / Port Origin should not be empty");
                        return;
                    }
                    if (kunnr == '' || kunnr == null) {
                        self.IsSaveUpdateDisabled(false);
                        self.IsGenerateEnable(true);
                        toastr.warning("SAP Vessel No. is mandatory to post data");
                        return;
                    }

                }
            }

            //else if (model.MessageType() == "CRMO" || model.MessageType() == 'UPMO') {
            //    if (model.AccountNo() == "Choose") {
            //        model.AccountNo('');
            //    }
            //    if (model.AccountNo() != "") {
            //        $("#Account").data('kendoDropDownList').enable(false);
                    self.IsSaveUpdateDisabled(true);
                    self.IsGenerateEnable(false);
            //        errors = 1;
            //    }
            //    else {
            //        self.SAPPostingValidation().errors.showAllMessages();
            //        toastr.warning("You have some form errors. Please check below.");
            //        return;


            //    }
            //}
            
            
            // self.IsReasonEnable(false);
            var ReasonCode = self.sapPostingModel().Reason();
            var Kunnr = self.sapPostingModel().AccountNo();
            var TransData = self.sapPostingModel().TransmitData().replace("#CODE#", ReasonCode).replace("@@@@", Kunnr);
            self.sapPostingModel().TransmitData(TransData);

            self.IsReasonEnable(false);


        }

        self.GetData = function () {
            var VCN = self.sapPostingModel().VCN();
            if (VCN != "") {
                self.viewModelHelper.apiGet('api/SAPPostingItemGrid/' + VCN,
              null,

                 function (result) {

                     self.SAPPostingList(ko.utils.arrayMap(result, function (item) {
                         return new IPMSRoot.SAPPostingModel(item);
                     }));
                     if (self.SAPPostingList()[0].ReasonForVisit() == "") {
                      //   self.viewMode('Form');
                         self.SAPPostingList('');
                         toastr.warning("Selected VCN is Not Available", "SAP Posting");
                     }
                     //  alert(self.SAPPostingList()[0].ReasonForVisit());
                 });

            }
            else {
                self.SAPPostingList('');
                toastr.warning("Please Select VCN For Posting", "SAP Posting");
            }
           
        }


        self.Reset = function () {
            self.sapPostingModel().VCN('');
            self.SAPPostingList.removeAll();
            self.sapPostingModel().reset();

        }

        self.Cancel = function () {

            self.viewMode('Form');
           // self.SAPPostingList.removeAll();
            //  self.sapPostingModel().reset();
            $('#spnTitile').html("SAP Posting");
        }

        self.CancelSAPInvoice = function () {
            $('#stack1').modal('hide');
            self.viewMode('Form');
            self.SAPPostingList.removeAll();
            self.sapPostingModel().reset();
        }

        
        self.SaveSapInvoice = function (model) {
            
            model.Reason("02"); // Hard coded the value
            self.viewModelHelper.apiPost('api/SAPPostingInvoice', ko.mapping.toJSON(model), function Message(data) {

                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("SAP Posting Invoice Details Saved successfully.", "SAP Posting");
                self.SAPPostingList.removeAll();
                self.sapPostingModel().reset();
                $('#spnTitile').html("SAP Posting");
                self.viewMode('Form');
                $('#stack1').modal('hide');
            });


        }

        self.SAPInvoiceSelect = function (model) {
           
            
            
            var VCN = self.sapPostingModel().VCN();
            var MsgType = model.MsgType();
            //var PostingStatus = model.PostStatus();

            var PostingStatus = 'SAPINV';
            
            self.sapPostingModel(new IPMSRoot.SAPPostingModel());
            var PortCode = self.sapPostingModel().PortCode();
            self.sapPostingModel().MessageType(model.MsgType());
            self.sapPostingModel().VCN(VCN);         
            var ReceavedARRNO = ((model.ReceavedARRNO() != undefined && model.ReceavedARRNO() != '') ? model.ReceavedARRNO() : 'NA')
            var MarineAccNo = model.MarineAccNo() != undefined && model.MarineAccNo() != '' ? model.MarineAccNo() : 'NA';
            var RevenueAgentAccNo = model.RevenueAgentAccNo() != undefined && model.RevenueAgentAccNo() != '' ? model.RevenueAgentAccNo() : 'NA';
            self.sapPostingModel().ReceavedARRNO(model.ReceavedARRNO());
            self.sapPostingModel().MarineAccNo(model.MarineAccNo());
            self.sapPostingModel().SAPReferenceNo(model.SAPReferenceNo());
            self.sapPostingModel().RevenueAgentAccNo(model.RevenueAgentAccNo());

            var MarinePostingId = model.MarinePostingId() != undefined && model.MarinePostingId() != '' ? model.MarinePostingId() : '0';
       
            if (VCN != "") {

                self.sapPostingModel().RevinueAccountNo(MarineAccNo);
                self.viewModelHelper.apiGet('api/SAPPostingAddDetails/' + VCN + '/' + MsgType + '/' + ReceavedARRNO + '/' + MarineAccNo + '/' + MarinePostingId + '/' + PostingStatus + '/' + RevenueAgentAccNo,
              null,
                 function (result) {                   
                     if (result != "") {
                         self.sapPostingModel().SAPInvoice(result);
                     }
                 });
            }
            $('#stack1').modal('show');
        }


        self.InvoiceResponseSelect = function (model) {
           
            self.sapPostingModel().SAPReferenceNo(model.SAPReferenceNo());
            var MarineOrderNo = self.sapPostingModel().SAPReferenceNo();

            if (MarineOrderNo != "") {
                self.viewModelHelper.apiGet('api/SAPInvoiceResponse/' + MarineOrderNo,
              null,
                 function (result) {
                     if (result != null) {
                         self.sapPostingModel().BillingDocNo(result.BillingDocNo);
                         self.sapPostingModel().OrderNo(result.OrderNo);
                         self.sapPostingModel().ItemNo(result.ItemNo);
                         self.sapPostingModel().MaterialNo(result.MaterialNo);
                         self.sapPostingModel().Service(result.Service);
                         self.sapPostingModel().UOM(result.UOM);
                         self.sapPostingModel().Qunatity(result.Qunatity);
                         self.sapPostingModel().TarifF(result.TarifF);
                         self.sapPostingModel().TarifF2(result.TarifF2);
                         self.sapPostingModel().Amount(result.Amount);
                         self.sapPostingModel().VAT(result.VAT);
                         self.sapPostingModel().NetAmount(result.NetAmount);
                         self.sapPostingModel().SalesOrgNo(result.SalesOrgNo);
                         self.sapPostingModel().AgentName(result.AgentName);
                         self.sapPostingModel().Address(result.Address);
                         self.sapPostingModel().TelephoneNo(result.TelephoneNo);
                         self.sapPostingModel().FaxNo(result.FaxNo);
                         self.sapPostingModel().Account(result.Account);

                         self.sapPostingModel().VesselID(result.VesselID);
                         self.sapPostingModel().VesselName(result.VesselName);
                         self.sapPostingModel().VesselTonnage(result.VesselTonnage);
                         self.sapPostingModel().VesselCapacity(result.VesselCapacity);
                         self.sapPostingModel().VesselLength(result.VesselLength);
                         self.sapPostingModel().ArrivalID(result.ArrivalID);


                         self.sapPostingModel().ArrivalDate(result.ArrivalDate);
                         self.sapPostingModel().Arrivaltime(result.Arrivaltime);
                         self.sapPostingModel().DepartureDate(result.DepartureDate);
                         self.sapPostingModel().DepartureTime(result.DepartureTime);
                         self.sapPostingModel().VoyageIn(result.VoyageIn);
                         self.sapPostingModel().VoyageOut(result.VoyageOut);
                         self.sapPostingModel().ESubscription(result.ESubscription);
                         self.sapPostingModel().NetValue(result.NetValue);
                         $('#stack2').modal('show');
                     }
                     else {
                         toastr.options.closeButton = true;
                         toastr.options.positionClass = "toast-top-right";
                         toastr.warning("No Records Found.", "SAP Posting");
                     }
                 });
            }
            


        }

        self.Initialize();
    }
    IPMSRoot.SAPPostingViewModel = SAPPostingViewModel;


}(window.IPMSROOT));


