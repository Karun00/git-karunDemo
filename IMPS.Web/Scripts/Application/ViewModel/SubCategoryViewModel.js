(function (IPMSRoot) {

    var SubCategoryViewModel = function () {

        var self = this;
        $('#spnTitile').html("Sub Category");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.subcategoryModel = ko.observable();
        self.SubCategoryList = ko.observableArray();
        self.AllSubCategoryList = ko.observableArray();
        self.SuperCategoryList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.editableView = ko.observable();
        self.IsSuperCatChanged = ko.observable(true);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.IsModified = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.subcategoryModel(new IPMSROOT.SubCategoryModel());
            self.viewMode('Form');
            self.IsCodeEnable(true);
            self.editableView(true);
            $("#grid").hide();
            self.LoadDefaultGrid();            
        }

        self.LoadDefaultGrid = function () {
            self.viewModelHelper.apiGet('api/SubCategories', null, function (result) {
                var SuperCategories = $.map(result, function (item) {
                    return new self.SuperCategory(item);
                });
                self.SuperCategoryList(SuperCategories);
            });
        };

        self.SuperCategory = function (data) {
            this.SupCatCode = ko.observable(data.SupCatCode);
            this.SupCatName = ko.observable(data.SupCatCode + '-' + data.SupCatName);
        };

        self.addsubcategory = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.editableView(true);
            self.IsUnique(false);
            self.subcategoryModel(new IPMSRoot.SubCategoryModel());
            $('#spnTitile').html("Add Sub Category");
        }

        self.SupCatChanged = function (event) {
            if (event.SupCatCode() == undefined) {
                self.cancel();
                self.SubCategoryList({ SubcatCode: 0, SubcatName: '' });
                self.IsSuperCatChanged(false);
            }
            else {
                self.IsSuperCatChanged(true);
                $('#spanSupcatid').text('');
                $("#grid").show();
                self.LoadGrid(event.SupCatCode());
            }
        }

        self.LoadGrid = function (SupCatCode) {
            if (SupCatCode == undefined) {
                self.SubCategoryList({ SubcatCode: 0, SubcatName: '' });
                self.IsSuperCatChanged(false);
            }
            else {
                self.viewModelHelper.apiGet('api/SubCategoryDetails/' + SupCatCode, null, function (result) {
                    self.SubCategoryList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.SubCategoryModel(item);
                    }));
                });

                self.viewModelHelper.apiGet('api/AllSubCategoryDetails', null, function (result) {
                    self.AllSubCategoryList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.SubCategoryModel(item);
                    }));
                });
            }
        }

        self.SaveSubCategory = function (model) {
            model.validationEnabled(true);
            self.SubCategoryValidation = ko.observable(model);
            self.SubCategoryValidation().errors = ko.validation.group(self.SubCategoryValidation());
            var errors = self.SubCategoryValidation().errors().length;
            var duplicate = false;
            if (errors == 0) {
                if (self.IsSave()) {
                    $.each(self.AllSubCategoryList(), function (index, subcategory) {
                        if ((subcategory.SubCatCode()).toLowerCase() == (model.SubCatCode()).toLowerCase()) {
                            self.UniqueCodeVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                        }
                        return;
                    });
                    $.each(self.SubCategoryList(), function (index, subcategory) {
                        if ((subcategory.SubCatName()).toLowerCase() == (model.SubCatName()).toLowerCase()) {
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
                    self.viewModelHelper.apiPost('api/SubCategories', ko.mapping.toJSON(model), function Message(data) {
                        model.SupCatCode(data.SupCatCode);
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Sub Category details saved successfully.", "Sub Category");
                        $('#spnTitile').html("Sub Category");
                        self.viewMode('Form');
                    });
                    self.cancel();
                }
            }
            else {
                self.SubCategoryValidation().errors.showAllMessages();
                return;
            }
        }

        self.ModifySubCategory = function (model) {
            model.validationEnabled(true);
            self.SubCategoryValidation = ko.observable(model);
            self.SubCategoryValidation().errors = ko.validation.group(self.SubCategoryValidation());
            var errors = self.SubCategoryValidation().errors().length;
            self.IsModified(true);
            var duplicate = false;

            if (errors == 0) {
                if (self.IsUpdate()) {
                    $.each(self.SubCategoryList(), function (index, subcategory) {

                        if (!((subcategory.SubCatCode()).toLowerCase() == (model.SubCatCode()).toLowerCase())) {

                            if ((subcategory.SubCatName()).toLowerCase() == (model.SubCatName()).toLowerCase()) {
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
                    self.viewModelHelper.apiPut('api/SubCategories', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Sub Category details updated successfully.", "Sub Category");
                        self.viewMode('Form');
                    });

                    self.cancel();
                }
                else {
                }

            }
            else {
                self.SubCategoryValidation().errors.showAllMessages();
                return;
            }
        }

        self.DeleteSubCategory = function (model) {
            if (confirm("Are You Sure you Want To Delete Sub Category(" + model.SubCatCode() + ")")) {
                self.viewModelHelper.apiDelete('api/SubCategories/PostDeletesubcategoryData/' + ko.mapping.toJSON(model.SubCatCode), null, function (result) {
                    self.SubCategoryList.remove(model);
                });
            }
        }

        self.viewsubcategory = function (subcategory) {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.editableView(false);
            self.subcategoryModel(subcategory);
            $('#spnTitile').html("View Sub Category");
        }

        self.editsubcategory = function (subcategory) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.subcategoryModel(subcategory);
            self.IsCodeEnable(false);
            self.editableView(true);
            subscribeToModelChange(self);
            $('#spnTitile').html("Update Sub Category");
        }

        HandleSubCatCodeKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        HandleSubCatNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.ResetSubCategory = function (model) {
            model.validationEnabled(false);
            if (self.IsSave()) {
                self.subcategoryModel().reset();
                self.SubCategoryList.removeAll();
                $("#grid").show();
            }
            else {
                ko.validation.reset();
                self.subcategoryModel().reset();
            }

            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.cancel = function () {
            self.viewMode('Form');
            $('#spnTitile').html("Sub Category");
            self.subcategoryModel(new IPMSRoot.SubCategoryModel());
            self.IsCodeEnable(true);
            self.editableView(true);
            self.SubCategoryList.removeAll();
            $("#grid").show();
            $("#Save").show();
            $("#Reset").show();
            $("#Update").hide();

            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.Initialize();
    }

    IPMSRoot.SubCategoryViewModel = SubCategoryViewModel;

}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.subcategoryModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.subcategoryModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}

