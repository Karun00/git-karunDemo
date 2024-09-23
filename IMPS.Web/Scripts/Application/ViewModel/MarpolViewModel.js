(function (IPMSRoot) {

    var MarpolViewModel = function () {

        var self = this;
        $('#spnTitile').html("Marpol");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.marpolModel = ko.observable();
        self.marpolReferenceData = ko.observable();
        self.MarpolList = ko.observableArray();

        self.IsPrivelegeEnable = ko.observable(true);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.IsStatusEnable = ko.observable(false);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadMarpols();
            self.LoadReferenceData();
            self.IsCodeEnable(true);
            self.viewMode('List');
         
        }


        self.LoadMarpols = function () {
            self.viewModelHelper.apiGet('api/MarpolDetails',
            null,
              function (result) {

                  self.MarpolList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.MarpolModel(item);
                  }));
              });
        }

        self.LoadReferenceData = function () {
            self.viewModelHelper.apiGet('api/GetMarpolReferenceData', null,
                  function (result1) {
                      self.marpolReferenceData(new IPMSRoot.MarpolReferenceData(result1));
                  }, null, null, false);
        }


        self.AddMarpol = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);

            self.marpolModel(new IPMSRoot.MarpolModel());
            ko.validation.group(self.marpolModel()).showAllMessages(false);
            
            $('#spnTitile').html("Add Marpol");
        }

        self.SaveMarpol = function (model) {
            model.validationEnabled(true);
            self.MarpolValidation = ko.observable(model);
            self.MarpolValidation().errors = ko.validation.group(self.MarpolValidation());
            var errors = self.MarpolValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {

                if (self.IsSave()) {
                    $.each(self.MarpolList(), function (index, marpol) {
                        if ((marpol.ClassCode()).toLowerCase() == (model.ClassCode()).toLowerCase()) {
                            self.UniqueCodeVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                        }

                        if ((marpol.ClassName()).toLowerCase() == (model.ClassName()).toLowerCase()) {
                            self.UniqueNameVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                        }

                        return;
                    });
                }
                if (!duplicate) {
                    self.IsUnique(true);
                }
                if (self.IsUnique() == true) {
                    self.viewModelHelper.apiPost('api/MarpolDetails', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Marpol details saved successfully.", "Marpol");
                        self.LoadMarpols();
                        $('#spnTitile').html("Marpol");
                        self.viewMode('List');
                    });
                }
            }
            else {
                self.MarpolValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ModifyMarpol = function (model) {
            model.validationEnabled(true);
            self.MarpolValidation = ko.observable(model);
            self.MarpolValidation().errors = ko.validation.group(self.MarpolValidation());
            var errors = self.MarpolValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {
                if (self.IsUpdate()) {
                    $.each(self.MarpolList(), function (index, marpol) {
                        if (!((marpol.ClassCode()).toLowerCase() == (model.ClassCode()).toLowerCase())) {
                            if ((marpol.ClassName()).toLowerCase() == (model.ClassName()).toLowerCase()) {
                                self.UniqueNameVisible(true);
                                duplicate = true;
                                self.IsModified(false);
                            }
                        }
                        return;
                    });
                }
                if (!duplicate) {
                    self.IsModified(true);
                }

                if (self.IsModified()) {
                    self.viewModelHelper.apiPut('api/MarpolDetails', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Marpol details updated successfully.", "Marpol");
                        $('#spnTitile').html("Marpol");
                        self.LoadMarpols();
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.MarpolValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }


        self.ResetMarpol = function (model) {

            $('#divValidationError').addClass('display-none');
            ko.validation.reset();
            model.validationEnabled(false);
            self.marpolModel().reset();
            self.MarpolValidation().errors.showAllMessages(false);


            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.Cancel = function () {
            self.viewMode('List');
            self.marpolModel().reset();
            $('#spnTitile').html("Marpol");

            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }           
        }

        self.viewMarpol = function (marpol) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.marpolModel(marpol);
            $('#spnTitile').html("View Marpol");
        }

        self.editMarpol = function (marpol) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.marpolModel(marpol);
            self.IsCodeEnable(false);
            $('#spnTitile').html("Update Marpol");
        }

        HandleClassCodeKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        HandleClassNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }


        self.Initialize();
    }
    IPMSRoot.MarpolViewModel = MarpolViewModel;

}(window.IPMSROOT));


