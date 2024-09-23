(function (IPMSRoot) {

    var StatementFactViewModel = function () {

        var self = this;
        $('#spnTitile').html("Vessel Performance Report");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.statementfactModel = ko.observable();
        self.StatementFactList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsPrint = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.statementfactReferenceData = ko.observable();
        self.vesselArrivalList = ko.observableArray();
        self.TugsList = ko.observableArray();
        self.IsKeyEventEnable = ko.observable(true);
        self.IsVCNEnable = ko.observable(true);
        self.IsEnable = ko.observable(true);

        self.IsWeather = ko.observable(true);
        self.IsVessel = ko.observable(true);
        self.IsTerminal = ko.observable(true);
        self.statementFactModelGrid = ko.observable(new IPMSROOT.StatementFactModelGrid());
        self.masterKeyEvents = ko.observableArray([]);

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.statementfactModel(new IPMSROOT.StatementFactModel());
            self.statementFactModelGrid(new IPMSRoot.StatementFactModelGrid(undefined));
            self.LoadReferenceData();
            self.LoadStatementFacts();
            //  self.LoadKeyEvents();
            self.viewMode('List');
        }

        self.LoadStatementFacts = function () {

            var vcnSearch = self.statementFactModelGrid().VCN();
            var vesselName = self.statementFactModelGrid().VesselName();

            if (vcnSearch == "") {
                vcnSearch = "All";
            }
            if (vesselName == "") {
                vesselName = "All";
            }

            self.viewModelHelper.apiGet('api/StatementFacts/' + vcnSearch + '/' + vesselName, null, function (result) {
                self.StatementFactList(ko.utils.arrayMap(result, function (item) {
                    //item.StatementFactEvents.Duration;
                    $.each(item.StatementFactEvents, function (key, val) {
                    //    var v = val.Duration;
                    //    if (v == 1)
                    //    {
                    //        value = v.toFixed(2);
                        //    }
                        if (item.StatementFactEvents[key] != null) {
                            if (item.StatementFactEvents[key].Duration != null) {

                                // if (item.StatementFactEvents[0].Duration == 1) {

                                item.StatementFactEvents[key].Duration = item.StatementFactEvents[key].Duration.toFixed(2);
                                //item.StatementFactEvents[1].Duration = item.StatementFactEvents[1].Duration.toFixed(2);
                                //  }
                            }
                        }
                    });
                  
                    return new IPMSRoot.StatementFactModel(item);
                }));
            });
        }

        self.GetDataClick = function (model) {

            var vcnSearch = self.statementFactModelGrid().VCN();
            var vesselName = self.statementFactModelGrid().VesselName();

            if (vcnSearch == "") {
                vcnSearch = "All";
            }
            if (vesselName == "") {
                vesselName = "All";
            }

            self.viewModelHelper.apiGet('api/StatementFacts/' + vcnSearch + '/' + vesselName, null, function (result) {
                self.StatementFactList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.StatementFactModel(item);
                }));
            });
        }


        self.ClearFilter = function () {
            self.statementFactModelGrid().VCN('');
            self.statementFactModelGrid().VesselName('');
            self.LoadStatementFacts();
        }



        self.LoadReferenceData = function () {
            self.viewModelHelper.apiGet('api/StatementFactReferenceData', null, function (refdata) {
                self.statementfactReferenceData(new IPMSRoot.ReferenceData(refdata));
            }, null, null, false);
        }

        self.LoadKeyEvents = function () {
            self.viewModelHelper.apiGet('api/StatementFactKeyEvents', null, function (result) {
                self.masterKeyEvents(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.KeyEvent(item);
                }));
            });
        }

        self.LoadVesselArrivals = function () {
            self.viewModelHelper.apiGet('api/StatementVCN', null, function (result) {
                ko.mapping.fromJS(result, {}, self.vesselArrivalList);
            });
        }

        self.VCNSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());

            self.statementfactModel().VCN(selecteddataItem.VCN);

            self.viewModelHelper.apiGet('api/StatementVCNInfo/' + selecteddataItem.VCN, null, function (result) {
                self.statementfactModel().VesselName(result.VesselName);
                self.statementfactModel().CurrentBerth(result.CurrentBerth);
                // self.statementfactModel().DateFrom(kendo.toString(new Date(Date.parse(result.DateFrom)), 'yyyy-MM-dd HH:mm'));
                // self.statementfactModel().DateTo(kendo.toString(new Date(Date.parse(result.DateTo)), 'yyyy-MM-dd HH:mm'));
                //  self.statementfactModel().DateFrom(result.DateFrom);
                //  self.statementfactModel().DateTo(result.DateTo);
                self.statementfactModel().VoyageIn(result.VoyageIn);
                self.statementfactModel().VoyageOut(result.VoyageOut);

                if (result.SDateFrom != null) {
                    self.statementfactModel().SDateFrom(moment(result.SDateFrom).format('YYYY-MM-DD HH:mm'));
                }
                if (result.SDateTo != null) {
                    self.statementfactModel().SDateTo(moment(result.SDateTo).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardPilotOnBoard != null) {
                    self.statementfactModel().InwardPilotOnBoard(moment(result.InwardPilotOnBoard).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardFirstLine != null) {
                    self.statementfactModel().InwardFirstLine(moment(result.InwardFirstLine).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardAllFast != null) {
                    self.statementfactModel().InwardAllFast(moment(result.InwardAllFast).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardPilotAway != null) {
                    self.statementfactModel().InwardPilotAway(moment(result.InwardPilotAway).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardPilotOnBoard != null) {
                    self.statementfactModel().OutwardPilotOnBoard(moment(result.OutwardPilotOnBoard).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardAllCast != null) {
                    self.statementfactModel().OutwardAllCast(moment(result.OutwardAllCast).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardPilotAway != null) {
                    self.statementfactModel().OutwardPilotAway(moment(result.OutwardPilotAway).format('YYYY-MM-DD HH:mm'));
                }


                self.statementfactModel().DraftArrivalFwd(result.DraftArrivalFwd);
                self.statementfactModel().DraftArrivalAft(result.DraftArrivalAft);
                self.statementfactModel().DraftSailingAft(result.DraftSailingAft);
                self.statementfactModel().DraftSailingFwd(result.DraftSailingFwd);


            });

            self.viewModelHelper.apiGet('api/StatementTUGInfo/' + selecteddataItem.VCN, null,
                function (result) {
                    self.statementfactModel().StatementTugDetails(new IPMSRoot.StatementTugData(result));
                }, null, null, false);
        }

        self.addstatementfact = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsKeyEventEnable(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);
            self.IsVCNEnable(true);
            self.IsEnable(true);
            // self.LoadKeyEvents();
            self.IsPrint(false);
            self.statementfactModel(new IPMSRoot.StatementFactModel());

            $('#spnTitile').html("Add Vessel Performance Report");
        }

        self.ResetStatementFact = function (model) {
         

            ko.validation.reset();
            model.validationEnabled(false);
            self.IsPrint(false);
            if (self.IsUpdate()) {
                self.statementfactModel(model);
                self.statementfactModel().reset();

            }

            if (self.IsSave()) {
                //self.LoadKeyEvents();
             
                self.statementfactModel(new IPMSROOT.StatementFactModel(undefined));
                self.statementfactModel().reset();
            }
        }

        self.Cancel = function () {
            $('#spnTitile').html("Vessel Performance Report");
            self.viewMode('List');
            self.statementfactModel().reset();
        }

        self.viewstatementfact = function (stafact) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsKeyEventEnable(false);
            self.IsVCNEnable(false);
            self.IsEnable(false);
            self.IsPrint(true);
            self.statementfactModel(stafact);
            $('#spnTitile').html("View Vessel Performance Report");

            self.viewModelHelper.apiGet('api/StatementVCNInfo/' + stafact.VCN(), null, function (result) {
                self.statementfactModel().VCN(result.VCN);
                self.statementfactModel().VesselName(result.VesselName);
                self.statementfactModel().CurrentBerth(result.CurrentBerth);
                self.statementfactModel().VoyageIn(result.VoyageIn);
                self.statementfactModel().VoyageOut(result.VoyageOut);

                if (result.SDateFrom != null) {
                    self.statementfactModel().SDateFrom(moment(result.SDateFrom).format('YYYY-MM-DD HH:mm'));
                }
                if (result.SDateTo != null) {
                    self.statementfactModel().SDateTo(moment(result.SDateTo).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardPilotOnBoard != null) {
                    self.statementfactModel().InwardPilotOnBoard(moment(result.InwardPilotOnBoard).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardFirstLine != null) {
                    self.statementfactModel().InwardFirstLine(moment(result.InwardFirstLine).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardAllFast != null) {
                    self.statementfactModel().InwardAllFast(moment(result.InwardAllFast).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardPilotAway != null) {
                    self.statementfactModel().InwardPilotAway(moment(result.InwardPilotAway).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardPilotOnBoard != null) {
                    self.statementfactModel().OutwardPilotOnBoard(moment(result.OutwardPilotOnBoard).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardAllCast != null) {
                    self.statementfactModel().OutwardAllCast(moment(result.OutwardAllCast).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardPilotAway != null) {
                    self.statementfactModel().OutwardPilotAway(moment(result.OutwardPilotAway).format('YYYY-MM-DD HH:mm'));
                }

                self.statementfactModel().DraftArrivalFwd(result.DraftArrivalFwd);
                self.statementfactModel().DraftArrivalAft(result.DraftArrivalAft);
                self.statementfactModel().DraftSailingAft(result.DraftSailingAft);
                self.statementfactModel().DraftSailingFwd(result.DraftSailingFwd);
            });

            self.viewModelHelper.apiGet('api/StatementTUGInfo/' + stafact.VCN(), null,
                function (result) {
                    self.statementfactModel().StatementTugDetails(new IPMSRoot.StatementTugData(result));
                }, null, null, false);
        }

        self.editstatementfact = function (stafact) {
        
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsKeyEventEnable(true);
            self.IsVCNEnable(false);
            self.IsEnable(true);
            self.IsPrint(false);
            self.statementfactModel(stafact);

            self.statementfactModel().VCN(stafact.VCN());
            self.statementfactModel().VCN_VesselName(stafact.VCN_VesselName());
            self.viewModelHelper.apiGet('api/StatementVCNInfo/' + stafact.VCN(), null, function (result) {
          

                self.statementfactModel().VesselName(result.VesselName);
                self.statementfactModel().CurrentBerth(result.CurrentBerth);
                self.statementfactModel().VoyageIn(result.VoyageIn);
                self.statementfactModel().VoyageOut(result.VoyageOut);

                if (result.SDateFrom != null) {
                    self.statementfactModel().SDateFrom(moment(result.SDateFrom).format('YYYY-MM-DD HH:mm'));
                }
                if (result.SDateTo != null) {
                    self.statementfactModel().SDateTo(moment(result.SDateTo).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardPilotOnBoard != null) {
                    self.statementfactModel().InwardPilotOnBoard(moment(result.InwardPilotOnBoard).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardFirstLine != null) {
                    self.statementfactModel().InwardFirstLine(moment(result.InwardFirstLine).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardAllFast != null) {
                    self.statementfactModel().InwardAllFast(moment(result.InwardAllFast).format('YYYY-MM-DD HH:mm'));
                }
                if (result.InwardPilotAway != null) {
                    self.statementfactModel().InwardPilotAway(moment(result.InwardPilotAway).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardPilotOnBoard != null) {
                    self.statementfactModel().OutwardPilotOnBoard(moment(result.OutwardPilotOnBoard).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardAllCast != null) {
                    self.statementfactModel().OutwardAllCast(moment(result.OutwardAllCast).format('YYYY-MM-DD HH:mm'));
                }
                if (result.OutwardPilotAway != null) {
                    self.statementfactModel().OutwardPilotAway(moment(result.OutwardPilotAway).format('YYYY-MM-DD HH:mm'));
                }

                self.statementfactModel().DraftArrivalFwd(result.DraftArrivalFwd);
                self.statementfactModel().DraftArrivalAft(result.DraftArrivalAft);
                self.statementfactModel().DraftSailingAft(result.DraftSailingAft);
                self.statementfactModel().DraftSailingFwd(result.DraftSailingFwd);
            });

            self.viewModelHelper.apiGet('api/StatementTUGInfo/' + stafact.VCN(), null,
                function (result) {
                    self.statementfactModel().StatementTugDetails(new IPMSRoot.StatementTugData(result));
                }, null, null, false);

            self.IsCodeEnable(false);
            $('#spnTitile').html("Update Vessel Performance Report");
        }

        self.SaveStatementFact = function (model) {

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.StatementFactValidation = ko.observable(model);
            self.StatementFactValidation().errors = ko.validation.group(self.StatementFactValidation());
            var errors = self.StatementFactValidation().errors().length;

            if (errors == 0) {

                if (model.StatementCommodities().length > 0) {
                    var ManError = "Y";
                    $.map(model.StatementCommodities(), function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            if (CommoditiesListC !== undefined) {
                                var QuantityVal = CommoditiesListC.Quantity();
                                if (QuantityVal == '' || QuantityVal == null) {
                                    QuantityVal = 0;
                                }
                                if (CommoditiesListC.CommodityBerthKey() == undefined || CommoditiesListC.CargoType() == undefined || CommoditiesListC.Package() == undefined || CommoditiesListC.UOM() == undefined || CommoditiesListC.CommodityBerthKey() == "" || CommoditiesListC.CargoType() == "" || CommoditiesListC.Package() == "" || CommoditiesListC.UOM() == "" || CommoditiesListC.Commodity() == "" || CommoditiesListC.Commodity() == undefined || CommoditiesListC.TerminalOperatorID() == "" || CommoditiesListC.TerminalOperatorID() == undefined || QuantityVal == 0) {                                
                                    //toastr.options.closeButton = true;
                                    //toastr.options.positionClass = "toast-top-right";
                                    toastr.warning("Please enter valid details of Quantities of Commodity", "Vessel Performance Report");
                                    ManError = "N";
                                    errors = 1;
                                }
                            }
                    });                   
                }
                //Validation is added by srinivas
                if (model.StatementFactEvents().length > 0) {
                    var ManError = "Y";
                    $.map(model.StatementFactEvents(), function (item) {
                        var CommoditiesList = item;
                        if (CommoditiesList != null)
                            if (CommoditiesList !== undefined) {
                                if (CommoditiesList.Duration() == undefined || CommoditiesList.Remarks() == undefined || CommoditiesList.Duration() == "" || CommoditiesList.Remarks() == "") {
                                    toastr.warning("Please enter valid details of Duration and Remarks", "Vessel Performance Report");
                                    ManError = "N";
                                    errors = 1;
                                }
                            }
                    });
                }
            }
           


            if (errors == 0) {
                if (self.IsSave() == true) {
                    self.viewModelHelper.apiPost('api/StatementFacts', ko.mapping.toJSON(model),
                        function Message(data) {
                            model.RecordStatus(data.RecordStatus);
                            toastr.success("Vessel Performance Report Details Saved Successfully.", "Vessel Performance Report");
                            self.LoadStatementFacts();
                            $('#spnTitile').html("Vessel Performance Report");
                            self.viewMode('List');
                        });
                }
            }
            else {
                self.StatementFactValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
                return;
            }
        }
       
        self.ModifyStatementFact = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.StatementFactValidation = ko.observable(model);
            self.StatementFactValidation().errors = ko.validation.group(self.StatementFactValidation());
            var errors = self.StatementFactValidation().errors().length;

            if (errors == 0) {

                if (model.StatementCommodities().length > 0) {
                    var ManError = "Y";
                    $.map(model.StatementCommodities(), function (item) {
                        var CommoditiesListC = item;
                        if (CommoditiesListC != null)
                            if (CommoditiesListC !== undefined) {
                                var QuantityVal = CommoditiesListC.Quantity();
                                if (QuantityVal == '' || QuantityVal == null) {
                                    QuantityVal = 0;
                                }
                                if (CommoditiesListC.CommodityBerthKey() == undefined || CommoditiesListC.CargoType() == undefined || CommoditiesListC.Package() == undefined || CommoditiesListC.UOM() == undefined || CommoditiesListC.CommodityBerthKey() == "" || CommoditiesListC.CargoType() == "" || CommoditiesListC.Package() == "" || CommoditiesListC.UOM() == "" || CommoditiesListC.Commodity() == "" || CommoditiesListC.Commodity() == undefined || CommoditiesListC.TerminalOperatorID() == "" || CommoditiesListC.TerminalOperatorID() == undefined || QuantityVal == 0) {
                                    //toastr.options.closeButton = true;
                                    //toastr.options.positionClass = "toast-top-right";
                                    toastr.warning("Please enter valid details of Quantities of Commodity", "Vessel Performance Report");
                                    ManError = "N";
                                    errors = 1;
                                }
                            }
                    });
                }
                //validation added by srinivas
              
                if (model.StatementFactEvents().length > 0) {
                    var ManError = "Y";
                    $.map(model.StatementFactEvents(), function (item) {
                        var CommoditiesList = item;
                        if (CommoditiesList != null)
                            if (CommoditiesList !== undefined) {
                                if (CommoditiesList.Duration() == undefined || CommoditiesList.Remarks() == undefined || CommoditiesList.Duration() == "" || CommoditiesList.Remarks() == "") {
                                    toastr.warning("Please enter valid details of Duration and Remarks", "Vessel Performance Report");
                                    ManError = "N";
                                    errors = 1;
                                }
                            }
                    });
                }

            }



            if (errors == 0) {

                if (self.IsUpdate() == true) {
                    self.viewModelHelper.apiPut('api/StatementFacts', ko.mapping.toJSON(model),
                        function Message(data) {
                            toastr.success("Vessel Performance Report Details Updated Successfully.", "Vessel Performance Report");
                            $('#spnTitile').html("Vessel Performance Report");
                            self.LoadStatementFacts();
                            self.viewMode('List');
                        });
                }
            }
            else {
                self.StatementFactValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
                return;
            }
        }



        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        VCNBlur = function () {

            if (self.statementfactModel().VCN() == "") {

                self.statementfactModel().VesselName('');
                self.statementfactModel().CurrentBerth('');
                self.statementfactModel().VoyageIn('');
                self.statementfactModel().VoyageOut('');

                self.statementfactModel().SDateFrom('');
                self.statementfactModel().SDateTo('');
                self.statementfactModel().InwardPilotOnBoard('');
                self.statementfactModel().InwardFirstLine('');
                self.statementfactModel().InwardAllFast('');
                self.statementfactModel().InwardPilotAway('');
                self.statementfactModel().OutwardPilotOnBoard('');
                self.statementfactModel().OutwardAllCast('');
                self.statementfactModel().OutwardPilotAway('');
                self.statementfactModel().DraftArrivalFwd('');
                self.statementfactModel().DraftArrivalAft('');
                self.statementfactModel().DraftSailingAft('');
                self.statementfactModel().DraftSailingFwd('');

                self.statementfactModel().StatementTugDetails(ko.utils.arrayMap(undefined, function (item) {
                    return new IPMSRoot.StatementTugData(item);
                }));
            }
        }

        VCNKeypress = function () {

            self.statementfactModel().VCN('');
            self.statementfactModel().VesselName('');
            self.statementfactModel().CurrentBerth('');
            self.statementfactModel().VoyageIn('');
            self.statementfactModel().VoyageOut('');

            self.statementfactModel().SDateFrom('');
            self.statementfactModel().SDateTo('');
            self.statementfactModel().InwardPilotOnBoard('');
            self.statementfactModel().InwardFirstLine('');
            self.statementfactModel().InwardAllFast('');
            self.statementfactModel().InwardPilotAway('');
            self.statementfactModel().OutwardPilotOnBoard('');
            self.statementfactModel().OutwardAllCast('');
            self.statementfactModel().OutwardPilotAway('');
            self.statementfactModel().DraftArrivalFwd('');
            self.statementfactModel().DraftArrivalAft('');
            self.statementfactModel().DraftSailingAft('');
            self.statementfactModel().DraftSailingFwd('');

            self.statementfactModel().StatementTugDetails(ko.utils.arrayMap(undefined, function (item) {
                return new IPMSRoot.StatementTugData(item);
            }));
        }

        $('#VCN').live('keydown', function (e) {
            var charCode = e.which || e.keyCode;
            if (charCode == 8 || charCode == 46) {
                self.statementfactModel().VesselName('');
                self.statementfactModel().CurrentBerth('');
                self.statementfactModel().VoyageIn('');
                self.statementfactModel().VoyageOut('');

                self.statementfactModel().SDateFrom('');
                self.statementfactModel().SDateTo('');
                self.statementfactModel().InwardPilotOnBoard('');
                self.statementfactModel().InwardFirstLine('');
                self.statementfactModel().InwardAllFast('');
                self.statementfactModel().InwardPilotAway('');
                self.statementfactModel().OutwardPilotOnBoard('');
                self.statementfactModel().OutwardAllCast('');
                self.statementfactModel().OutwardPilotAway('');
                self.statementfactModel().DraftArrivalFwd('');
                self.statementfactModel().DraftArrivalAft('');
                self.statementfactModel().DraftSailingAft('');
                self.statementfactModel().DraftSailingFwd('');

                self.statementfactModel().StatementTugDetails(ko.utils.arrayMap(undefined, function (item) {
                    return new IPMSRoot.StatementTugData(item);
                }));
            }
        });


        self.AddNewRowtotable = function () {
      

            self.statementfactModel().StatementFactEvents.push(new IPMSROOT.OperationalDelay());
        }

        self.RemoveAddNewRowtotable = function (row) {
            document.getElementById("RemoveAddNewRowtotable").focus();
            self.statementfactModel().StatementFactEvents.remove(row);
        }


        self.AddNewCommodity = function (data) {
           
            if (data.StatementCommodities().length > 0) {
                var ManError = "Y";
                $.map(data.StatementCommodities, function (item) {
                    var CommoditiesListC = item;
                    if (CommoditiesListC != null)
                        ko.utils.arrayForEach(CommoditiesListC, function (CommodChk) {
                            if (CommodChk !== undefined) {
                                if (CommodChk.CommodityBerthKey() == "" || CommodChk.CargoType() == "" || CommodChk.Package() == "" || CommodChk.UOM() == "" || CommodChk.Quantity() == "" || CommodChk.Quantity() == 0 || CommodChk.TerminalOperatorID() == "" || CommodChk.TerminalOperatorID() == 0 || CommodChk.Commodity() == "") {
                                    //toastr.options.closeButton = true;
                                    //toastr.options.positionClass = "toast-top-right";
                                    toastr.warning("Please Enter Commodity Details", "Vessel Performance Report");
                                    ManError = "N";
                                }
                            }
                        });

                });
                if (ManError == "Y") {

                    var acomodity = new IPMSROOT.StatementCommodity();
                    //acomodity.CommodityBerthKey(data.PreferedBerthKey());
                    self.statementfactModel().StatementCommodities.push(acomodity);
                }
            }
            else {
                var acomodity = new IPMSROOT.StatementCommodity();
                //acomodity.CommodityBerthKey(data.PreferedBerthKey());
                self.statementfactModel().StatementCommodities.push(acomodity);
            }
            
        }

        self.removeCommodities = function (data) {
              self.statementfactModel().StatementCommodities.remove(data);
        }


        self.Initialize();
    }
    IPMSRoot.StatementFactViewModel = StatementFactViewModel;

}(window.IPMSROOT));




