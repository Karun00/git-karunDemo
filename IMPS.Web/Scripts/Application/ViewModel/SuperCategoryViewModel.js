(function (IPMSRoot) {

    var SuperCategoryViewModel = function () {

        var self = this;
        $('#spnTitile').html("Super Category");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.supercategoryModel = ko.observable();
        self.SupCatList = ko.observableArray();
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


        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.supercategoryModel(new IPMSROOT.SuperCategoryModel());
            self.LoadSupCats();
            self.viewMode('List');
        }

        self.LoadSupCats = function () {
            self.viewModelHelper.apiGet('api/SuperCategories', null, function (result) {
                self.SupCatList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.SuperCategoryModel(item);
                }));
            });
        }

        self.addsupcat = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);

            self.supercategoryModel(new IPMSRoot.SuperCategoryModel());

            $('#spnTitile').html("Add Super Category");
        }

        self.SaveSupCat = function (model) {
            model.validationEnabled(true);
            self.SupCatValidation = ko.observable(model);
            self.SupCatValidation().errors = ko.validation.group(self.SupCatValidation());
            var errors = self.SupCatValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {

                if (self.IsSave()) {
                    $.each(self.SupCatList(), function (index, supcat) {
                        if ((supcat.SupCatCode()).toLowerCase() == (model.SupCatCode()).toLowerCase()) {
                            self.UniqueCodeVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                        }

                        if ((supcat.SupCatName()).toLowerCase() == (model.SupCatName()).toLowerCase()) {
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
                    self.viewModelHelper.apiPost('api/SuperCategories', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Super Category details saved successfully.", "Super Category");
                        self.LoadSupCats();
                        $('#spnTitile').html("Super Category");
                        self.viewMode('List');
                    });
                }
            }
            else {
                self.SupCatValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ModifySupCat = function (model) {
            model.validationEnabled(true);
            self.SupCatValidation = ko.observable(model);
            self.SupCatValidation().errors = ko.validation.group(self.SupCatValidation());
            var errors = self.SupCatValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {
                if (self.IsUpdate()) {
                    $.each(self.SupCatList(), function (index, supcat) {
                        if (!((supcat.SupCatCode()).toLowerCase() == (model.SupCatCode()).toLowerCase())) {
                            if ((supcat.SupCatName()).toLowerCase() == (model.SupCatName()).toLowerCase()) {
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
                    self.viewModelHelper.apiPut('api/SuperCategories', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Super Category details updated successfully.", "Super Category");
                        $('#spnTitile').html("Super Category");
                        self.LoadSupCats();
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.SupCatValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }


        self.ResetSupCat = function (model) {

            $('#divValidationError').addClass('display-none');
            ko.validation.reset();
            model.validationEnabled(false);
            self.supercategoryModel().reset();
            self.SupCatValidation().errors.showAllMessages(false);


            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.Cancel = function () {
            self.viewMode('List');
            self.supercategoryModel().reset();
            $('#spnTitile').html("Super Category");

            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.viewsupcat = function (supcat) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.supercategoryModel(supcat);
            $('#spnTitile').html("View Super Category");
        }

        self.editsupcat = function (supcat) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.supercategoryModel(supcat);
            self.IsCodeEnable(false);
            $('#spnTitile').html("Update Super Category");
        }

        HandleSupCatCodeKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        HandleSupCatNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.Initialize();
    }
    IPMSRoot.SuperCategoryViewModel = SuperCategoryViewModel;

}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.supercategoryModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.supercategoryModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}

