(function (IPMSRoot) {

    var EntityViewModel = function () {

        var self = this;
        $('#spnTitile').html("Entity Management");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.entityModel = ko.observable();
        self.EntityList = ko.observableArray();
        self.SubModuleList = ko.observableArray();
        self.masterPriviligeTypes = ko.observableArray([]);
        self.masterModules = ko.observableArray([]);
        self.IsPrivelegeEnable = ko.observable(true);
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
            self.LoadEntities();
            self.LoadSubModules();
            self.LoadPrivileges();
            self.viewMode('List');
        }

        self.LoadSubModules = function () {
            self.viewModelHelper.apiGet('api/EntitySubModules', null,

              function (result) {
                  self.masterModules(ko.utils.arrayMap(result, function (item) {

                      return new IPMSRoot.SubModule(item);
                  }));
              });
        }

        self.LoadEntities = function () {
            self.viewModelHelper.apiGet('api/Entities',
            null,
              function (result) {

                  self.EntityList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.EntityModel(item, self.masterModules(), self.masterPriviligeTypes());
                  }));
              });
        }

        self.LoadPrivileges = function () {
            self.viewModelHelper.apiGet('api/EntityPrivilege', null,
           function (result) {
               self.masterPriviligeTypes(ko.utils.arrayMap(result, function (item) {
                   return new IPMSRoot.Privilege(item);
               }));

           });
        }

        self.addentity = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsPrivelegeEnable(true);
            self.IsCodeEnable(true);
            self.IsUnique(true);
            self.entityModel(new IPMSROOT.EntityModel(undefined, self.masterModules()));
            $('#spnTitile').html("Add Entity");
        }

        self.editentity = function (entity) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsPrivelegeEnable(true);
            self.IsCodeEnable(false);
            self.entityModel(entity);
            $('#spnTitile').html("Update Entity");

        }

        self.viewentity = function (entity) {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsPrivelegeEnable(false);
            self.IsCodeEnable(false);
            self.entityModel(entity);
            $('#spnTitile').html("View Entity");
        }

        self.ResetEntity = function (model) {

            $('#divValidationError').addClass('display-none');
            $('#spanEntityPriv').hide();
            model.validationEnabled(false);
            ko.validation.reset();
            self.entityModel().reset();
            self.EntityValidation().errors.showAllMessages(false);
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.Cancel = function () {
            self.viewMode('List');
            $('#spnTitile').html("Entity Management");
            self.entityModel(new IPMSRoot.EntityModel());

            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.SaveEntity = function (model) {
            model.validationEnabled(true);
            self.EntityValidation = ko.observable(model);
            self.EntityValidation().errors = ko.validation.group(self.EntityValidation());
            var errors = self.EntityValidation().errors().length;
            var duplicate = false;

            if (model.EntityPrivileges() == "") {
                $('#spanEntityPriv').text('* Please select at least one privilege.');
                $('#spanEntityPriv').show();
                errors = 1;
            }

            if (errors == 0) {
                if (self.IsSave()) {
                    $.each(JSON.parse(ko.toJSON(self.EntityList())), function (index, value) {
                        if (parseInt(value.ModuleID) == parseInt(model.ModuleID())) {
                            if ((value.EntityCode).toLowerCase() == (model.EntityCode()).toLowerCase()) {
                                self.IsUnique(false);
                                duplicate = true;
                                self.UniqueCodeVisible(true);
                            }
                            if ((value.EntityName).toLowerCase() == (model.EntityName()).toLowerCase()) {
                                self.UniqueNameVisible(true);
                                duplicate = true;
                                self.IsUnique(false);
                            }

                        }
                        else {
                            if ((value.EntityCode).toLowerCase() == (model.EntityCode()).toLowerCase()) {
                                self.IsUnique(false);
                                duplicate = true;
                                self.UniqueCodeVisible(true);
                            }
                        }
                        return;

                    });
                }
                if (!duplicate) {
                    self.IsUnique(true);
                }

                if (model.HasWorkflowStatus()) {
                    model.HasWorkflow = "Y";
                }
                else
                    model.HasWorkflow = "N";

                if (model.HasMenuItemStatus()) {
                    model.HasMenuItem = "Y";
                }
                else
                    model.HasMenuItem = "N";

                if (self.IsSave() == true && self.IsUnique() == true) {
                    self.viewModelHelper.apiPost('api/Entities', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Entity details saved successfully.", "Entity Management");
                        self.LoadEntities();
                        $('#spnTitile').html("Entity Management");
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.EntityValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ModifyEntity = function (model) {
            self.EntityValidation = ko.observable(model);
            self.EntityValidation().errors = ko.validation.group(self.EntityValidation());
            var errors = self.EntityValidation().errors().length;
            var duplicate = false;

            if (model.EntityPrivileges() == "") {
                $('#spanEntityPriv').text('* Please select at least one privilege.');
                $('#spanEntityPriv').show();
                errors = 1;
            }

            if (errors == 0) {
                if (self.IsUpdate()) {
                    $.each(JSON.parse(ko.toJSON(self.EntityList())), function (index, value) {
                        if (parseInt(value.ModuleID) == parseInt(model.ModuleID())) {
                            if (!((value.EntityCode).toLowerCase() == (model.EntityCode()).toLowerCase())) {
                                if ((value.EntityName).toLowerCase() == (model.EntityName()).toLowerCase()) {
                                    self.UniqueNameVisible(true);
                                    duplicate = true;
                                    self.IsModified(false);
                                }
                            }
                        }
                        return;
                    });
                }
                if (!duplicate) {
                    self.IsModified(true);
                }

                if (model.HasWorkflowStatus()) {
                    model.HasWorkflow = "Y";
                }
                else
                    model.HasWorkflow = "N";

                if (model.HasMenuItemStatus()) {
                    model.HasMenuItem = "Y";
                }
                else
                    model.HasMenuItem = "N";

                if (self.IsUpdate() == true && self.IsModified() == true) {
                    model.EntityPrivileges(self.entityModel().EntityPrivileges());
                    self.viewModelHelper.apiPut('api/Entities', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Entity details updated successfully.", "Entity Management");
                        self.LoadEntities();
                        $('#spnTitile').html("Entity Management");
                        self.viewMode('List');

                    });
                }
            }
            else {
                self.EntityValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        HandleEntityCodeKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        HandleEntityNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        HandleEntityPrivilege = function (data, event) {
            $('#spanEntityPriv').hide();
        }

        self.Initialize();
    }
    IPMSRoot.EntityViewModel = EntityViewModel;

}(window.IPMSROOT));
