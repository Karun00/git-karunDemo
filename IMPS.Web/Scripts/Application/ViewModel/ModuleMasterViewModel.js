(function (IPMSRoot) {

    var ModuleMasterViewModel = function () {
        var self = this;
        $('#spnTitile').html("Module Master");
        self.validationHelper = new IPMSRoot.validationHelper();

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.moduleMasterModel = ko.observable(new IPMSRoot.ModuleMasterModel());
        self.viewMode = ko.observable();

        //Tree view 
        self.ModuledataTreeView = ko.observableArray();
        self.editableView = ko.observable(true);
        self.ModuleList = ko.observableArray();
        self.ParentList = ko.observableArray();
        self.editButton = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.isModuleEditable = ko.observable(false);
        self.isSubModuleEditable = ko.observable(false);

        // Initilize
        self.initialize = function () {
            self.viewMode = ko.observable(true);
            self.moduleMasterModel(new IPMSROOT.ModuleMasterModel());
            self.LoadModules();
            self.viewMode('List');
            self.LoadModulesTreeview();
        };

        // Loads module list
        self.LoadModules = function () {

            self.viewModelHelper.apiGet('api/Modules', null, function (result) {
                self.ModuleList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.ModuleMasterModel(item);
                }));
            }, null, null, false);

            self.viewModelHelper.apiGet('api/Module/GetParentModules', null, function (result) {
                self.ParentList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.ModuleMasterModel(item);
                }));
            }, null, null, false);
        };

        // Loads the module data for the tree view
        self.LoadModulesTreeview = function () {
            self.viewModelHelper.apiGet('api/Modules', null,
                function (result) {
                    $.each(result, function (key, val) {
                        $.each(val.Module1, function (key1, val1) { });
                    });
                    ko.mapping.fromJS(result, {}, self.ModuledataTreeView);
                });
        }

        // Saves / Adds the new module
        self.SaveModule = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.ModuleValidation = ko.observable(model);
            self.ModuleValidation().errors = ko.validation.group(self.ModuleValidation());
            var errors = self.ModuleValidation().errors().length;

            if (ko.toJSON(model.ParentModuleID) == "null") {
                $('#spanparentid').text('* This field is required.');
            }

            if (errors == 0) {
                self.viewModelHelper.apiPost('api/Modules', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.success("Module details saved successfully.", "Module Master");
                        self.moduleMasterModel(new IPMSROOT.ModuleMasterModel());
                        self.viewMode('List');
                        self.LoadModulesTreeview();
                        $('#stack1').modal('hide');
                        $('#spnTitile').html("Module Master");
                    });
            }
            else {
                self.ModuleValidation().errors.showAllMessages();
                return;
            }
        };

        // Modifies the Module data
        self.ModifyModule = function (model) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            model.validationEnabled(true);
            self.viewModelHelper.apiPut('api/Modules', ko.mapping.toJSON(model),
                function Message(data) {
                    toastr.success("Module details updated successfully.", "Module Master");
                    self.moduleMasterModel(new IPMSROOT.ModuleMasterModel());
                    self.viewMode('List');
                    self.LoadModulesTreeview();
                    $('#stack1').modal('hide');
                    $('#spnTitile').html("Module Master");
                });
        };

        // Module slected / changed
        self.moduleSelected = function (model) {
            self.editButton(false);
            self.moduleMasterModel(new IPMSRoot.ModuleMasterModel(ko.mapping.toJS(model)));
        };

        // Addnew module button click
        self.addModule = function () {

            $('#stack1').modal('show');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.isModuleEditable(false);
            self.isSubModuleEditable(false);
            self.moduleMasterModel(new IPMSRoot.ModuleMasterModel());
            $('#spnTitile1').html("Add Module");
        }

        // EDit Module button click
        self.editModule = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if (self.moduleMasterModel().ModuleID() == 0) {
                toastr.warning("Please select at least one module.", "Module Master");
            }
            else {
                $('#stack1').modal('show');
                if (self.moduleMasterModel().ParentModuleID() > 0) {
                    $("#spnTitile1").text("Update Sub Module");
                    $("#moduleLable").text("Sub Module Name");
                    self.isModuleEditable(true);
                    self.isSubModuleEditable(false);
                }
                else if (self.moduleMasterModel().ModuleID() > 0) {
                    $("#spnTitile1").text("Update Module");
                    $("#moduleLable").text("Module Name");
                    self.isModuleEditable(false);
                    self.isSubModuleEditable(true);
                }
            }

            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true)
        }

        // ViewModule Button Click
        self.viewModule = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            if (self.moduleMasterModel().ModuleID() == 0) {
                toastr.warning("Please select at least one module.", "Module Master");
            }
            else {
                $('#stack1').modal('show');
                if (self.moduleMasterModel().ParentModuleID() > 0) {
                    $("#spnTitile1").text("View Sub Module");
                    $("#moduleLable").text("Sub Module Name");
                    self.isModuleEditable(true);
                    self.isSubModuleEditable(true);
                }
                else if (self.moduleMasterModel().ModuleID() > 0) {
                    $("#spnTitile1").text("View Module");
                    $("#moduleLable").text("Module Name");
                    self.isModuleEditable(true);
                    self.isSubModuleEditable(true);
                }
            }

            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
        }

        // Cancel Button Click
        self.Cancel = function (model) {
            self.viewMode('List');
            model.validationEnabled(false);
            self.moduleMasterModel().reset();
            $('#spnTitle').html("Module Master");
        }

        // Reset button Click
        self.ResetModule = function (model) {
            model.validationEnabled(false);
            self.moduleMasterModel().reset();
        }

        ChangeModuleLableNames = function (data, event) {
            if (self.moduleMasterModel().ModuleID() > 0) {
                $("#spnTitile1").text("Update Module");
                $("#moduleLable").text("Module Name");
            } else {
                $("#ModuleName").val("");
                $("#OrderNo").val("");
                $("#spnTitile1").text("Add Module");
                $("#moduleLable").text("Module Name");
            }
        }

        ChangeSubModuleLableNames = function (data, event) {
            if (self.moduleMasterModel().ParentModuleID() > 0) {
                $("#spnTitile1").text("Update Sub Module");
                $("#moduleLable").text("Sub Module Name");
            }
            else {
                $("#ModuleName").val("");
                $("#OrderNo").val("");
                $("#spnTitile1").text("Add Sub Module");
                $("#moduleLable").text("Sub Module Name");
            }
        }

        // VALIDATING Module name existing or not
        self.ValidEvent = function (data, event) {
            var databaseList = ko.toJSON(self.LoadModulesTreeview);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;

            $.each(jsonObj, function (index, value) {
                if (value.ModuleName == formList.ModuleName) {
                    $('#spanModuleName').text(formList.ModuleName + ' is already exists! Please enter another Name.');
                    flag = false;
                }
            });
        }

        //To validate alphabet with spaces
        self.ValidateAlphabetsWithSpaces = function (data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z\b ]*$/;
            return charcheck.test(keychar);
        }

        //Accept only numeric 
        self.ValidateNumeric = function (data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /[0-9]/;
            return charcheck.test(keychar);
        }

        self.initialize();
    }

    IPMSRoot.ModuleMasterViewModel = ModuleMasterViewModel;
}(window.IPMSROOT));
