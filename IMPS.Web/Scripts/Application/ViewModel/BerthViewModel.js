(function (IPMSRoot) {
    var BerthViewModel = function () {
        var self = this;
        $('#spnTitle').html("Berth");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.berthModel = ko.observable();
        self.berthList = ko.observableArray();
        self.portValues = ko.observableArray();
        self.quayValues = ko.observableArray();
        self.berthTypeValues = ko.observableArray();
        self.berthValues = ko.observableArray();
        self.isPortChanged = ko.observable(true);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.IsClicks = ko.observable(false);
        self.IsNewBerth = ko.observable(false);
        self.cargoTypeValues = ko.observableArray();
        self.vesselTypeValues = ko.observableArray();
        self.reasonTypeValues = ko.observableArray();
        self.IsValid = ko.observable(true);
        self.IsUnique = ko.observable(true);
        self.IsModified = ko.observable(false);
        self.IsDataChanged = ko.observable(false);

        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.UniqueShortNameVisible = ko.observable(false);
        self.IsFromMeterVisible = ko.observable(false);
        self.IsToMeterVisible = ko.observable(false);

        self.IsFromToEnable = ko.observable(false);     

        self.IsDrydock = ko.observable(true);

        self.LoadBerths = function () {
            self.viewModelHelper.apiGet('api/Berths', null, function (result) {
                self.berthList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.BerthModel(item);
                }));
            }, null, null, false);
        }

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.berthModel(new IPMSROOT.BerthModel());
            self.LoadBerthTypes();
            self.LoadCargoTypes();
            self.LoadVesselTypes();
            self.LoadReasonTypes();
            self.LoadPorts();
            self.LoadBerths();
            self.LoadQuays();
            self.viewMode('List');
        }

        self.subscribeBerthtypes = function (event) {
            var cargoSelect = $("#cargoType").data("kendoMultiSelect");
            var vesselSelect = $("#vesselType").data("kendoMultiSelect");
            var reasonSelect = $("#reasonType").data("kendoMultiSelect");
          
            if (event.BerthType() == "DRDK") {
                self.berthModel().CargoType("");
                self.berthModel().VesselType("");
                self.berthModel().ReasonForVisitType("");
           
                cargoSelect.enable(false);
                vesselSelect.enable(false);
                reasonSelect.enable(false);
                $("#cargospan").hide();
                $("#vesselspan").hide();
                $("#reasonspan").hide();
                $('#spancargoTypeid').hide();
                $('#spanvesselTypeid').hide();
                $('#spanreasonTypeid').hide();

            }
            else {
                cargoSelect.enable(true);
                vesselSelect.enable(true);
                reasonSelect.enable(true);
                $("#cargospan").show();
                $("#vesselspan").show();
                $("#reasonspan").show();
            }
         
            if (event.BerthType() != undefined) {
                $('#spanberthtype').text('');
            }
        }

        self.subscribeQuaytypes = function (event) {
            if (event.QuayCode() != undefined) {
                $('#spanquayid').text('');
                self.LoadQuayBerths(event.PortCode(), event.QuayCode());
            }
        }

        //for grid display in add screen
        self.LoadQuayBerths = function (PortCode, QuayCode) {
            if (QuayCode == undefined) {
                self.berthValues({});
            }
            else {
                self.viewModelHelper.apiGet('api/Berths/' + PortCode + '/' + QuayCode, null, function (result) {
                    self.berthValues(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.BerthModel(item);
                    }));
                });
            }
        }

        self.subscribeCargotypes = function (event) {
            if (event.CargoType() != undefined) {
                $('#spancargoTypeid').text('');
                self.IsModified(true);
            }
        }

        self.subscribeVesseltypes = function (event) {
            if (event.VesselType() != undefined) {
                $('#spanvesselTypeid').text('');
                self.IsModified(true);
            }
        }

        self.subscribeReasontypes = function (event) {
            if (event.ReasonForVisitType() != undefined) {
                $('#spanreasonTypeid').text('');
                self.IsModified(true);
            }
        }

        self.subscribedatachanges = function (event) {
            if (event == undefined)
                self.IsModified(false);
            else
                self.IsModified(true);
        }

        self.LoadQuays = function () {
            self.viewModelHelper.apiGet('api/BerthPQDtl', null, function (result) {
                ko.mapping.fromJS(result, {}, self.quayValues);
            });
        }

        /////////////////////////////////////////////////////
        self.LoadBerthTypes = function () {
            self.viewModelHelper.apiGet('api/BerthTypes', null, function (result) {
                ko.mapping.fromJS(result, {}, self.berthTypeValues);
            });
        }

        self.LoadCargoTypes = function () {
            self.viewModelHelper.apiGet('api/BerthCargos', null, function (result) {
                ko.mapping.fromJS(result, {}, self.cargoTypeValues);
            });
        }

        self.LoadVesselTypes = function () {
            self.viewModelHelper.apiGet('api/BerthVesselTypes', null, function (result) {
                ko.mapping.fromJS(result, {}, self.vesselTypeValues);
            });
        }

        self.LoadReasonTypes = function () {
            self.viewModelHelper.apiGet('api/BerthReasonTypes', null, function (result) {
                ko.mapping.fromJS(result, {}, self.reasonTypeValues);
            });
        }
        ///////////////////////////////////////////////////////

        self.LoadPorts = function () {
            self.viewModelHelper.apiGet('api/Ports/GetLoginPort', null, function (result) {
                var ports = $.map(result, function (item) {
                    return new self.Port(item);
                });
                self.portValues(ports);
            });
        }

        self.Port = function (data) {
            this.PortCode = ko.observable(data.PortCode);
            this.PortName = ko.observable(data.PortCode + '-' + data.PortName);
        };

        self.SaveBerth = function (model) {
            model.validationEnabled(true);
            self.BerthValidation = ko.observable(model);
            self.BerthValidation().errors = ko.validation.group(self.BerthValidation());
            var errors = self.BerthValidation().errors().length;

            if (model.BerthType() != "DRDK") {
                if (model.BerthType() == "") {
                    $('#spanberthtype').text('* Please select Berth Type');
                    $('#spanberthtype').show();
                    errors = 1;
                }

                if (model.CargoType() == "") {
                    $('#spancargoTypeid').text('* Please select Cargo Types');
                    $('#spancargoTypeid').show();
                    errors = 1;
                }

                if (model.VesselType() == "") {
                    $('#spanvesselTypeid').text('* Please select Vessel Types');
                    $('#spanvesselTypeid').show();
                    errors = 1;
                }

                if (model.ReasonForVisitType() == "") {
                    $('#spanreasonTypeid').text('* Please select Reason For Visit');
                    $('#spanreasonTypeid').show();
                    errors = 1;
                }
            }
            if (errors == 0) {

                $.each(JSON.parse(ko.toJSON(self.berthList())), function (index, value) {

                    if (value.PortCode == model.PortCode() && value.QuayCode == model.QuayCode()) {

                        if ((value.BerthCode).toLowerCase() == (model.BerthCode()).toLowerCase()) {
                            self.IsUnique(false);
                            self.UniqueCodeVisible(true);
                        }

                        if ((value.BerthName).toLowerCase() == (model.BerthName()).toLowerCase()) {

                            self.IsUnique(false);
                            self.UniqueNameVisible(true);
                        }
                    }
                    if ((value.ShortName).toLowerCase() == (model.ShortName()).toLowerCase()) {
                        self.IsUnique(false);
                        self.UniqueShortNameVisible(true);
                    }
                    return;
                });
                if (self.IsUnique() == true && model.CargoType() != null) {

                    model.CargoType(self.berthModel().CargoType());
                    model.VesselType(self.berthModel().VesselType());
                    model.ReasonForVisitType(self.berthModel().ReasonForVisitType());
                    model.Lengthm(self.berthModel().IsLength());
                    self.viewModelHelper.apiPost('api/Berths', ko.mapping.toJSON(model),
                        function Message(data) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Berth details saved successfully.", "Berth");
                            self.cache = "";
                            self.LoadBerths();
                            $('#spnTitle').html("Berth");
                            self.viewMode('List');
                        });
                }
            }

            else {
                self.BerthValidation().errors.showAllMessages();
                return;
            }
        }

        self.ModifyBerth = function (model) {
            model.validationEnabled(true);
            self.BerthValidation = ko.observable(model);
            self.BerthValidation().errors = ko.validation.group(self.BerthValidation());
            var errors = self.BerthValidation().errors().length;

            if (model.BerthType() != "DRDK") {
                if (model.BerthType() == "") {
                    $('#spanberthtype').text('* Please select Berth Type');
                    $('#spanberthtype').show();
                    errors = 1;
                }

                if (model.CargoType() == "") {
                    $('#spancargoTypeid').text('* Please select Cargo Types');
                    $('#spancargoTypeid').show();
                    errors = 1;
                }

                if (model.VesselType() == "") {
                    $('#spanvesselTypeid').text('* Please select Vessel Types');
                    $('#spanvesselTypeid').show();
                    errors = 1;
                }

                if (model.ReasonForVisitType() == "") {
                    $('#spanreasonTypeid').text('* Please select Reason For Visit');
                    $('#spanreasonTypeid').show();
                    errors = 1;
                }
            }

            if (errors == 0) {
                $.each(JSON.parse(ko.toJSON(self.berthList())), function (index, value) {
                    if (value.PortCode == model.PortCode() && value.QuayCode == model.QuayCode()) {
                        if (!((value.BerthCode).toLowerCase() == (model.BerthCode()).toLowerCase())) {
                            if ((value.BerthName).toLowerCase() == (model.BerthName()).toLowerCase()) {
                                self.IsModified(false);
                                self.UniqueNameVisible(true);
                            }
                        }
                    }
                    else {
                        if (!((value.BerthCode).toLowerCase() == (model.BerthCode()).toLowerCase())) {
                            if ((value.ShortName).toLowerCase() == (model.ShortName()).toLowerCase()) {
                                self.IsModified(false);
                                self.UniqueShortNameVisible(true);
                            }
                        }
                        return;
                    }
                });

                if (self.IsModified() == true) {
                    model.Lengthm(self.berthModel().IsLength());
                    model.CargoType(self.berthModel().CargoType());
                    model.VesselType(self.berthModel().VesselType());
                    model.ReasonForVisitType(self.berthModel().ReasonForVisitType());
                    self.viewModelHelper.apiPut('api/Berths', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Berth details updated successfully.", "Berth");
                        self.cache = "";
                        self.LoadBerths();
                        $('#spnTitle').html("Berth");
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.BerthValidation().errors.showAllMessages();
            }
        }

        self.DeleteBerth = function (model) {
            model.Lengthm(self.berthModel().IsLength());
            model.CargoType(self.berthModel().CargoType());
            self.viewModelHelper.apiPost('api/Berth/PutDeleteBerth', ko.mapping.toJSON(model),
                function Message(data) { });
        }

        self.viewberth = function (berth) {
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.isPortChanged(false);
            self.IsFromToEnable(false);
            self.viewMode('Form');
            self.berthTypeValues();
            self.portValues();

            self.quayValues([{ QuayCode: berth.QuayCode(), QuayName: berth.QuayName(), QuayLength: berth.QuayLength() }]);

            self.berthModel(berth);

            $('#spnTitle').html("View Berth");
            $("#grid").hide();
            var cargoSelect = $("#cargoType").data("kendoMultiSelect");
            var vesselSelect = $("#vesselType").data("kendoMultiSelect");
            var reasonSelect = $("#reasonType").data("kendoMultiSelect");

            cargoSelect.enable(false);
            vesselSelect.enable(false);
            reasonSelect.enable(false);

            if (berth.BerthType() == "DRDK") {
                $("#cargospan").hide();
                $("#vesselspan").hide();
                $("#reasonspan").hide();
            }
            else {  
                $("#cargospan").show();
                $("#vesselspan").show();
                $("#reasonspan").show();
            }
        }

        self.editberth = function (berth) {
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.IsClicks(true);
            self.editableView(true);
            self.IsCodeEnable(false);
            self.isPortChanged(false);
            self.IsModified(true);
            self.IsFromToEnable(false);
            self.viewMode('Form');
            self.berthTypeValues();
            self.portValues();
            self.quayValues([{ QuayCode: berth.QuayCode(), QuayName: berth.QuayName(), QuayLength: berth.QuayLength() }]);
            self.LoadQuayBerths(berth.PortCode(), berth.QuayCode());
            self.LoadCargoTypes();
            self.berthModel(berth);

            $('#spnTitle').html("Update Berth");
            $("#grid").show();
            var cargoSelect = $("#cargoType").data("kendoMultiSelect");
            var vesselSelect = $("#vesselType").data("kendoMultiSelect");
            var reasonSelect = $("#reasonType").data("kendoMultiSelect");

            if (berth.BerthType() == "DRDK") {

                cargoSelect.enable(false);
                vesselSelect.enable(false);
                reasonSelect.enable(false);
                $("#cargospan").hide();
                $("#vesselspan").hide();
                $("#reasonspan").hide();
            }
            else {
                cargoSelect.enable(true);
                vesselSelect.enable(true);
                reasonSelect.enable(true);
                $("#cargospan").show();
                $("#vesselspan").show();
                $("#reasonspan").show();
            }

        }

        self.addBerth = function () {
            self.berthModel(new IPMSRoot.BerthModel());
            self.IsSave(true);
            self.IsClicks(true);
            self.editableView(true);
            self.IsDrydock(true);
            self.IsCodeEnable(true);
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsReset(true);
            self.IsValid(false);
            self.IsUnique(true);
            self.IsFromToEnable(true);
            self.LoadPorts();
            self.berthValues("");
            self.LoadBerthTypes();
            self.LoadCargoTypes();
            $('#spnTitle').html("Add Berth");
            $("#grid").show();
            var cargoSelect = $("#cargoType").data("kendoMultiSelect");
            var vesselSelect = $("#vesselType").data("kendoMultiSelect");
            var reasonSelect = $("#reasonType").data("kendoMultiSelect");

            cargoSelect.enable(true);
            vesselSelect.enable(true);
            reasonSelect.enable(true);
        }

        self.ResetBerth = function (model) {
            model.validationEnabled(false);
            ko.validation.reset();
            self.berthModel().reset();
            self.BerthValidation = ko.observable(model);
            self.BerthValidation().errors = ko.validation.group(self.BerthValidation());
            self.BerthValidation().errors.showAllMessages(false);

            if (self.IsSave()) {
                self.berthValues("");
            }
            else {
                self.LoadQuayBerths(model.PortCode(), model.QuayCode());
            }
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }

            if ($('#spanberthtype').is(':visible')) {
                $('#spanberthtype').css('display', 'none');
            }

            if ($('#spancargoTypeid').is(':visible')) {
                $('#spancargoTypeid').css('display', 'none');
            }

            if ($('#spanvesselTypeid').is(':visible')) {
                $('#spanvesselTypeid').css('display', 'none');
            }

            if ($('#spanreasonTypeid').is(':visible')) {
                $('#spanreasonTypeid').css('display', 'none');
            }
        }

        self.cancel = function () {
            self.viewMode('List');
            self.berthModel().reset();
            $('#spnTitle').html("Berth");
        }

        HandleBerthCodeKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }

        HandleBerthNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }

        HandleBerthShortNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }

        HandleFromMeterKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }

        HandleToMeterKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (self.UniqueShortNameVisible() == true) {
                self.UniqueShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }

        self.Initialize();
    }
    IPMSRoot.BerthViewModel = BerthViewModel;

}(window.IPMSROOT));











