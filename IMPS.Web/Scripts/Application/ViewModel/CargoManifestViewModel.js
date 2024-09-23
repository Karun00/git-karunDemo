(function (IPMSRoot) {

    var CargoManifestViewModel = function () {

        var self = this;
        $('#spnTitle').html("Cargo Manifest");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.cargomanifestModel = ko.observable();
        self.CargoManifestList = ko.observableArray([]);
        self.CargoCommoditiesList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.isFirstMoveDateTime = ko.observable(false);
        self.isLastMoveDateTime = ko.observable(false);
        self.isOutTurnValid = ko.observable(false);

        self.ATA = ko.observable();
        self.ATD = ko.observable();

        var errormessage = '* This field is required.';

        self.Initialize = function () {
            self.viewMode('List');
            self.cargomanifestModel(new IPMSROOT.CargoManifestModel());
            self.LoadArrivalNotifications();
            self.IsUpdate(false);
        }

        self.LoadArrivalNotifications = function () {
            self.viewModelHelper.apiGet('api/CargoManifest', null,
              function (result) {
                  self.CargoManifestList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.CargoManifestModel(item);
                  }));
              }, null, null, false);
        }

        self.LoadCargoCommodity = function (VCN) {
            self.viewModelHelper.apiGet('api/ArrivalCommodityDetails', { VCN: VCN },
             function (result) {
                 self.cargomanifestModel().CargoManifests(ko.utils.arrayMap(result, function (item) {
                     return new IPMSRoot.CargoCommodity(item);
                 }));
             }, null, null, true);
        }

        self.editcargomanifest = function (cargomanif) {
            $('#spnTitle').html("Update Cargo Manifest");
            self.LoadCargoCommodity(cargomanif.VCN);
            self.editableView(true);
            self.IsSaveUpdateDisabled(false);
            self.viewMode('Form');
            self.SaveUpdate(cargomanif);

            if (cargomanif.FirstMoveDateTime() == "0001-01-01 00:00:00")
            { cargomanif.FirstMoveDateTime(""); }
            if (cargomanif.LastMoveDateTime() == "0001-01-01 00:00:00")
            { cargomanif.LastMoveDateTime(""); }

            var FirstMoveDateTime = ""
            if (cargomanif.FirstMoveDateTime() != null || cargomanif.FirstMoveDateTime() != "" || cargomanif.FirstMoveDateTime() != undefined) {
                FirstMoveDateTime = cargomanif.FirstMoveDateTime();
            }
            else {
                FirstMoveDateTime = new Date();
            }
            var myDatePicker = new Date(FirstMoveDateTime);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours() + 1;
            var Mnt = myDatePicker.getMinutes();
            $("#LastMoveDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));

            self.cargomanifestModel(cargomanif);
        }

        self.viewCargoManifest = function (cargomanif) {
            $('#spnTitle').html("View Cargo Manifest");
            self.editableView(false);
            self.IsSaveUpdateDisabled(false);
            self.IsSave(false);
            self.IsUpdate(false);
            self.LoadCargoCommodity(cargomanif.VCN);
            self.viewMode('Form');
            self.cargomanifestModel(cargomanif);
        }

        self.SaveUpdate = function (cargomanif) {

            if (cargomanif.CargoManifestID() > 0 && cargomanif.CargoManifestID() != null && cargomanif.CargoManifestID() != undefined) {
                self.IsSave(false);
                self.IsUpdate(true);
                $('#spnTitle').html("Update Cargo Manifest");

            } else {
                self.IsSave(true);
                self.IsUpdate(false);
                $('#spnTitle').html("Save Cargo Manifest");
            }
        }

        self.Cancel = function () {
            self.clearallerrormsgs();
            $('#spnTitle').html("Cargo Manifest");
            self.viewMode('List');
            self.cargomanifestModel().reset();
        }

        self.SaveCargoManifest = function (model) {
            model.validationEnabled(true);
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.CargoManifestValidation = ko.observable(model);
            self.CargoManifestValidation().errors = ko.validation.group(self.CargoManifestValidation());
            var errors = self.CargoManifestValidation().errors().length;

            var fromDate = model.FirstMoveDateTime();
            var toDate = model.LastMoveDateTime();

            if (fromDate == null || fromDate == '' || fromDate == undefined || fromDate == "Invalid date") {
                $("#isFirstMoveDateTime").text(errormessage);
                fromDate = "";
                self.isFirstMoveDateTime(true);
                errors = 1;
            }
            else {
                self.isFirstMoveDateTime(false);
                $("#isFirstMoveDateTime").text('');
            }

            if (toDate == null || toDate == '' || toDate == undefined || toDate == "Invalid date") {
                $("#isLastMoveDateTime").text(errormessage);
                toDate = "";
                self.isLastMoveDateTime(true);
                errors = 1;
            }
            else {
                self.isLastMoveDateTime(false);
                $("#isLastMoveDateTime").text('');
            }

            if ((fromDate != "") && (toDate != "")) {
                if (fromDate > toDate) {
                    errors = 1;
                    toastr.warning("Please check First move date should be lesser than Last move date.", "Move Dates Warning");
                }
                else {
                    errors = 0;
                    var Commlist = self.cargomanifestModel().CargoManifests();
                    var CommdtlsList = [];
                    $.each(Commlist, function (key, val) {
                        if (val.OutTurn() == 0 || val.OutTurn() == "" || val.OutTurn() == null) {
                            errors = 1;
                            self.isOutTurnValid(true);
                            $("#spanOutTurn").text("Please enter the Out Turn");
                            toastr.warning("Please enter the Out Turn", "Cargo Manifest");
                        }
                        else {
                            if (val.OutTurn() > val.Quantity()) {
                                errors = 1;
                                toastr.warning("Please check Out Turn should be lesser than Quantity", "Cargo Manifest");
                            }
                            else {
                                errors = 0;
                                self.isOutTurnValid(false);
                                $("#spanOutTurn").text("");
                            }
                        }
                    });
                }
            }
            else {
                errors = 1;
            }

            if (errors == 0) {
                self.viewModelHelper.apiPost('api/CargoManifest', ko.mapping.toJSON(model), function Message(data) {
                    model.RecordStatus(data.RecordStatus);
                    toastr.success("Cargo Manifest saved successfully.", "Cargo Manifest");
                    $('#spnTitle').html("Cargo Manifest");
                    self.LoadArrivalNotifications();
                    self.viewMode('List');
                });
            }
            else {
                self.CargoManifestValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ModifyCargoManifest = function (model) {
            model.validationEnabled(true);
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.CargoManifestValidation = ko.observable(model);
            self.CargoManifestValidation().errors = ko.validation.group(self.CargoManifestValidation());
            var errors = self.CargoManifestValidation().errors().length;

            var fromDate = model.FirstMoveDateTime();
            var toDate = model.LastMoveDateTime();

            if (fromDate == null || fromDate == '' || fromDate == undefined || fromDate == "Invalid date") {
                $("#isFirstMoveDateTime").text(errormessage);
                fromDate = "";
                self.isFirstMoveDateTime(true);
                errors = 1;
            }
            else {
                self.isFirstMoveDateTime(false);
                $("#isFirstMoveDateTime").text('');
            }

            if (toDate == null || toDate == '' || toDate == undefined || toDate == "Invalid date") {
                $("#isLastMoveDateTime").text(errormessage);
                toDate = "";
                self.isLastMoveDateTime(true);
                errors = 1;
            }
            else {
                self.isLastMoveDateTime(false);
                $("#isLastMoveDateTime").text('');
            }

            if (fromDate > toDate) {
                errors = 1;
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please check First Move date & time should be lesser than Last Move date & time.", "Move Dates Warning");
            }
            else {
                errors = 0;
                var Commlist = self.cargomanifestModel().CargoManifests();
                var CommdtlsList = [];
                $.each(Commlist, function (key, val) {
                    if (val.OutTurn() == 0 || val.OutTurn() == "" || val.OutTurn() == null) {
                        errors = 1;
                        self.isOutTurnValid(true);
                        $("#spanOutTurn").text("Please enter the Out Turn");
                        toastr.error("Please enter the Out Turn", "Cargo Manifest");
                    }
                    else {
                        if (val.OutTurn() > val.Quantity()) {
                            errors = 1;
                            toastr.warning("Please check Out Turn should be lesser than Quantity", "Cargo Manifest");
                        }
                        else {
                            errors = 0;
                            self.isOutTurnValid(false);
                            $("#spanOutTurn").text("");
                        }
                    }
                });
            }

            if (errors == 0) {
                self.viewModelHelper.apiPut('api/CargoManifest', ko.mapping.toJSON(model), function Message(data) {
                    model.RecordStatus(data.RecordStatus);
                    toastr.success("Cargo Manifest updated successfully.", "Cargo Manifest");
                    $('#spnTitle').html("Cargo Manifest");
                    self.LoadArrivalNotifications();
                    self.viewMode('List');
                });
            }
            else {
                self.CargoManifestValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.clearallerrormsgs = function () {
            self.isFirstMoveDateTime(false);
            self.isLastMoveDateTime(false);
        }

        ValidDate = function (data, event) {
            var some = JSON.parse(ko.toJSON(data));
            var fmdate = some.FirstMoveDateTime;
            var lmdate = some.LastMoveDateTime;
            var ATA = some.ATA;
            var ATD = some.ATD;
        }

        ChangeLastMoveDateTime = function () {
            if ($("#FirstMoveDateTime").val() == "" || $("#FirstMoveDateTime").val() == null) {
                $("#LastMoveDateTime").val("");
                $("#isFirstMoveDateTime").text(errormessage);
            }
            else {
                $("#isFirstMoveDateTime").text("");
                $("#LastMoveDateTime").val("");
                var FirstMoveDateTime = $("#FirstMoveDateTime").val();
                var myDatePicker = new Date(FirstMoveDateTime);
                var day = myDatePicker.getDate();
                var month = myDatePicker.getMonth();
                var year = myDatePicker.getFullYear();
                var Hour = myDatePicker.getHours() + 1;
                var Mnt = myDatePicker.getMinutes();
                $("#LastMoveDateTime").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
            }
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();
    }
    IPMSRoot.CargoManifestViewModel = CargoManifestViewModel;

}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.cargomanifestModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop])) {
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
        }
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.cargomanifestModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}