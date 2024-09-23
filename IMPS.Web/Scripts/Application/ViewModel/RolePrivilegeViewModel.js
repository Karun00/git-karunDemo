(function (IPMSRoot) {

    var RolePrivilegeViewModel = function () {

        var self = this;
        $('#spnTitile').html("Role Privileges");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.roleprivilegeModel = ko.observable();
        self.RolePrivList = ko.observableArray();
        self.masterModules = ko.observableArray();
        self.SubModulesList = ko.observableArray();
        self.EntitiesList = ko.observableArray();
        self.RolesList = ko.observableArray();
        self.ModuleID = ko.observableArray();
        self.SubModuleID = ko.observableArray();
        self.IsAdd = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        //self.Displaybtn = ko.observable();
        self.dataSource = ko.observable();
        self.IsSaveUpdateDisabled = ko.observable(true);
        self.IsUnique = ko.observable(true);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.SetSubModuleID = ko.observable();
        self.BindRoles = ko.observable();
        self.IsAssign = ko.observable(false);
        self.IsviewMode = ko.observable(false);
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.IsSaveUpdateDisabled(true);
            self.roleprivilegeModel(new IPMSROOT.RolePrivilegeModel());
            self.LoadRolePriv();
            self.LoadModules();
            self.viewMode('List');
        }

        self.LoadRolePriv = function () {
            self.viewModelHelper.apiGet('api/RolePrivileges', null,
              function (result) {
                  self.RolePrivList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.RolePrivilegeModel(item);
                  }));
              });
        }

        self.LoadModules = function () {
            self.viewModelHelper.apiGet('api/RoleReferenceData', null,
              function (result) {
                  self.masterModules(ko.utils.arrayMap(result.getModules, function (item) {
                      return new IPMSRoot.Module(item);
                  }));
              });
        }

        self.LoadSubModules = function (Modid) {
            if (Modid == undefined) {
                self.SubModulesList({ ModuleID: 0, ModuleName: '' });
            }
            else {
                self.viewModelHelper.apiGet('api/SubModules/' + Modid, null,
                  function (result) {
                      self.SubModulesList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.Module(item);
                      }));
                  });
            }
        }

        self.LoadEntities = function (Moduleid, SubModuleid, RoleID) {
            if (Moduleid == undefined) {
                self.EntitiesList({ EntityID: 0, ModuleName: '' });
            }
            else {
                self.viewModelHelper.apiGet('api/EntitiesWithPrivileges/' + Moduleid + '/' + SubModuleid + '/' + RoleID,
                    { Moduleid: Moduleid, SubModuleid: SubModuleid, RoleID: RoleID },
                    function (result) {
                        self.EntitiesList(ko.utils.arrayMap(result, function (item) {
                            return new IPMSRoot.Entities(item);
                        }));
                    }
                  );
            }
        }

        self.addrolepriv = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsAdd(true);
            self.IsSave(true);
            self.IsviewMode(false);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);
            self.roleprivilegeModel(new IPMSRoot.RolePrivilegeModel());
            $('#spnTitile').html("Add Role Privileges");
        }

        ValidationReset = function () {
            var NoOfErrors = 0;
            $('#spanModid').text('');

            $('#spanSubModid').text('');
        }

        //Module Change
        ChangeModule = function () {
            if ($("#Mod").val() == "" || $("#Mod").val() == null) {
                $('#spanModid').text('* This field is required.');
            }
            else {
                $("#spanModid").text('');
            }
        }

        //Sub Module Change
        ChangeSubModule = function () {

            if ($("#SubMod").val() == "" || $("#Mod").val() == null) {
                $('#spanSubModid').text('* This field is required.');
            }
            else {
                $("#spanSubModid").text('');
            }
        }

        Validation = function () {
            var NoOfErrors = 0;
            $('#spanModid').text('');
            $('#spanSubModid').text('');

            if ($("#SubMod").val() == "" || $("#SubMod").val() == null) {
                $('#spanSubModid').text('* This field is required.');
                NoOfErrors++;
            }

            if ($("#Mod").val() == "" || $("#Mod").val() == null) {
                $('#spanModid').text('* This field is required.');

                NoOfErrors++;
            }
            return NoOfErrors;
        }

        self.SaveRolePriv = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.RolePrivValidation = ko.observable(model);
            self.RolePrivValidation().errors = ko.validation.group(self.RolePrivValidation());
            var errors = self.RolePrivValidation().errors().length;
            var duplicate = false;
            var errors1 = 0;

            errors1 = Validation();

            var PrivilegesList = self.EntitiesList();
            var RolePrivilegesList = [];
            $.each(PrivilegesList, function (key, val) {
                var _EntityID = val.EntityID();
                var _SubCatCode;
                $.each(val.EntityPrivileges(), function (key, val) {
                    _SubCatCode = val.SubCatCode;
                    if (val.IsRole == true)
                        RolePrivilegesList.push(new RolePrivelege(0, _EntityID, _SubCatCode));
                });
            });

            model.RolePrivileges = RolePrivilegesList;

            if ((errors == 0) && (errors1 == 0)) {
                if (self.IsSave()) {
                    $.each(self.RolePrivList(), function (index, rolepriv) {
                        if ((rolepriv.RoleCode()).toLowerCase() == (model.RoleCode()).toLowerCase()) {
                            self.UniqueCodeVisible(true);
                            duplicate = true;
                            self.IsUnique(false);
                        }

                        if ((rolepriv.RoleName()).toLowerCase() == (model.RoleName()).toLowerCase()) {
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
                    self.viewModelHelper.apiPost('api/RolePrivileges', ko.mapping.toJSON(model),
                        function Message(data) {
                            model.RecordStatus(data.RecordStatus);
                            toastr.success("Role privileges saved successfully.", "Role Privileges");
                            self.LoadRolePriv();
                            $('#spnTitile').html("Role Privileges");
                            self.viewMode('List');
                            self.IsAssign(false);
                        });
                }
            }
            else {
                self.RolePrivValidation().errors.showAllMessages();
                return;
            }
        }

        self.ModifyRolePriv = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.RolePrivValidation = ko.observable(model);
            self.RolePrivValidation().errors = ko.validation.group(self.RolePrivValidation());
            var errors = self.RolePrivValidation().errors().length;
            var duplicate = false;

            var PrivilegesList = self.EntitiesList();

            var RolePrivilegesList = [];
            $.each(PrivilegesList, function (key, val) {
                var _EntityID = val.EntityID();
                var _SubCatCode;
                $.each(val.EntityPrivileges(), function (key, val) {
                    _SubCatCode = val.SubCatCode;
                    if (val.IsRole == true)
                        RolePrivilegesList.push(new RolePrivelege(model.RoleID, _EntityID, _SubCatCode));
                });
            });

            model.RolePrivileges = RolePrivilegesList;

            var errors1 = 0;
            errors1 = Validation();

            if ((errors == 0) && (errors1 == 0)) {
                if (self.IsUpdate()) {
                    $.each(self.RolePrivList(), function (index, rolepriv) {
                        if (!((rolepriv.RoleCode()).toLowerCase() == (model.RoleCode()).toLowerCase())) {
                            if ((rolepriv.RoleName()).toLowerCase() == (model.RoleName()).toLowerCase()) {
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
                    self.viewModelHelper.apiPut('api/RolePrivileges', ko.mapping.toJSON(model),
                        function Message(data) {
                            toastr.success("Role privileges updated successfully.", "Role Privileges");
                            $('#spnTitile').html("Role Privileges");
                            self.LoadRolePriv();
                            self.viewMode('List');
                            self.IsAssign(false);
                        });
                }
            }
            else {
                self.RolePrivValidation().errors.showAllMessages();
                return;
            }
        }

        self.Displaybtn = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            self.IsSaveUpdateDisabled(false);

            var ModuleID = $("#Mod").val();
            var SubModuleID = $("#SubMod").val();

            if (ModuleID != "" && ModuleID != undefined) {
                if (SubModuleID != "" && SubModuleID != undefined) {
                    if (self.IsAdd() == true) {
                        self.LoadEntities(ModuleID, SubModuleID, 0);
                        self.IsAssign(true);
                    }
                    else {
                        self.LoadEntities(ModuleID, SubModuleID, model.RoleID());
                        self.IsAssign(true);

                        var entList = self.EntitiesList();
                        var roList = self.RolesList();
                        self.BindRoles(entList);
                    }
                }
                else {
                    self.IsAssign(false);
                    self.IsSaveUpdateDisabled(true);
                    toastr.warning("Please select sub module.", "Role Privileges");
                }
            }
            else {
                self.IsAssign(false);
                self.IsSaveUpdateDisabled(true);
                toastr.warning("Please select module", "Role Privileges");
            }
        }

        self.BindRoles = function (entList) {
            self.EntitiesList.push(new IPMSRoot.Entities(entList));
        }

        self.ResetRolePriv = function (model) {
            $('#spanModid').text('');
            $('#spanSubModid').text('');
            ko.validation.reset();
            model.validationEnabled(false);
            self.roleprivilegeModel().reset();
            self.IsAssign(false);
            model.ModuleID("");
            model.SubModuleID("");
            self.IsSaveUpdateDisabled(true);
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.Cancel = function () {
            self.IsAssign(false);
            self.viewMode('List');
            self.roleprivilegeModel().reset();
            $('#spnTitile').html("Role Privileges");
            self.IsSaveUpdateDisabled(true);
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.viewrolepriv = function (rolepriv) {
            $('#spnTitile').html("View Role Privileges");
            self.IsAssign(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsviewMode(true);
            self.IsReset(false);
            self.editableView(false);
            self.viewMode('Form');

            self.roleprivilegeModel(rolepriv);
        }

        self.editrolepriv = function (rolepriv) {
            $('#spnTitile').html("Update Role Privileges");

            self.IsAssign(false);
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsviewMode(false);
            self.IsReset(true);
            rolepriv.ModuleID("");
            rolepriv.SubModuleID("");
            self.editableView(true);
            self.IsCodeEnable(false);
            self.viewMode('Form');
            subscribeToModelChange(self);

            self.roleprivilegeModel(rolepriv);
        }

        self.FillSubMod = function (event) {
            self.IsAssign(false);
            ChangeModule();
            if (!event) {
                self.IsSaveUpdateDisabled(true);
                self.SubModulesList({ ModuleID: 0, ModuleName: '' });
            }
            else {
                if (event.ModuleID() == undefined) {
                    self.IsSaveUpdateDisabled(true);
                    self.SubModulesList({ ModuleID: 0, ModuleName: '' });
                }
                else {
                    self.IsSaveUpdateDisabled(true);
                    self.LoadSubModules(event.ModuleID());
                }
            }
        }

        self.SetSubModuleID = function (event) {
            if (event.ModuleID() == undefined) {
            }
            else {
                self.SubModuleID = event.ModuleID;
            }

            ChangeSubModule();
        }

        HandleRolePrivCodeKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        HandleRolePrivNameKeyUp = function (data, event) {
            if (self.UniqueCodeVisible() == true) {
                self.UniqueCodeVisible(false);
            }

            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
        }

        self.Initialize();
    }

    IPMSRoot.RolePrivilegeViewModel = RolePrivilegeViewModel;

}(window.IPMSROOT));

function RolePrivelege(RoleID, EntityID, SubCatCode) {
    this.RoleID = RoleID;
    this.EntityID = EntityID;
    this.SubCatCode = SubCatCode;
}

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.roleprivilegeModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.roleprivilegeModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}