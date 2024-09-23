(function (IPMSRoot) {
    var ResourceGroupViewModel = function () {

        var self = this;
        $('#spnTitile').html("Resource Grouping");

        self.viewModelHelper = new IPMSROOT.viewModelHelper();

        self.ResourceModel = ko.observable();
        self.Designations = ko.observableArray();
        self.ResourceEmployeeGroups = ko.observableArray();
        self.viewMode = ko.observable();
        self.ResourceGroupList = ko.observable();

        self.IsResourceCodeEnable = ko.observable(true);
        self.IsResourceNameEnable = ko.observable(true);
        self.IsViewMode = ko.observable(true);
        self.DisplayMode = ko.observable('ADD');
        self.IsAddMode = ko.observable(false);
        self.IsEditMode = ko.observable(false);

        self.IsSaveVisible = ko.observable(true);
        self.IsResetVisible = ko.observable(true);

        // To intialize objcets
        self.Initialize = function () {

            self.viewMode('List');
            self.LoadResourceGrpList();
            self.ResourceModel(new IPMSROOT.ResourceGroupModel());
            self.LoadDesignations();
        }

        //To get desigantions
        self.LoadDesignations = function () {
            self.viewModelHelper.apiGet('api/DesignationDetails', null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.Designations);
              }, null, null, false);
        }

        // To get employees by desigantion
        self.LoadEmployees = function (event) {
            var multi = $("#MultiSelect1").data("kendoMultiSelect");
            multi.value("");
            self.ResourceEmployeeGroups.removeAll();
            self.ResourceModel().ResourceEmpList.removeAll();

            self.viewModelHelper.apiGet('api/Employees/' + event,
                { ResourceGroupCode: '', designationcode: event, mode: self.DisplayMode() },
                function (result) {
                    if (result.length > 0) {
                        ko.mapping.fromJS(result, {}, self.ResourceEmployeeGroups);
                        $('#spanvemp').hide();
                    }
                    else {
                        ko.mapping.fromJS(result, {}, self.ResourceEmployeeGroups);
                    }
                }, null, null, false);
        }

        // To load Resource Grouping list
        self.LoadResourceGrpList = function () {
            self.viewModelHelper.apiGet('api/ResourceGrouping', null,
              function (result) {
                  self.ResourceGroupList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.ResourceGroupModel(item);
                  }));
              }, null, null, false);
        }

        //To save and modify Resource Grouping details
        self.saveResourceGroup = function (model) {
            var validationErrorMessage = "* This field is required.";

            self.ResourceGroupValidation = ko.observable(model);
            self.ResourceGroupValidation().errors = ko.validation.group(self.ResourceGroupValidation());
            var errors = self.ResourceGroupValidation().errors().length;
            var cnt = 0;
            var resourceEmpGroup = [];
            $.each(model.ResourceEmpList(), function (index, value) {
                resourceEmpGroup.push(new ResourceGroupEmployees(value));
                model.ResourceEmployeeGroups(resourceEmpGroup);
            });
            if (model.ResourceGroupName() == "") {
                $('#spanRsrcName').text(validationErrorMessage);
                $('#spanRsrcName').show();
                errors = 1;
            }

            if (model.ResourceGroupCode() == "") {
                $('#spanimono').text(validationErrorMessage);
                $('#spanimono').show();
                errors = 1;
            }

            if (model.Position() == "") {
                $('#spanDesignations').text(validationErrorMessage);
                $('#spanDesignations').show();
                errors = 1;
            }

            if (model.ResourceEmpList().length == 0) {
                $('#spanvemp').text(validationErrorMessage);
                $('#spanvemp').show();
                errors = 1;
            }

            if (errors == 0) {
                if (model.ResourceGroupID() == 0) {
                    self.viewModelHelper.apiPost('api/ResourceGrouping', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Resource grouping details saved successfully.", "Resource Grouping");
                        self.LoadResourceGrpList();
                        self.viewMode('List');
                        $('#spnTitile').html("Resource Grouping");
                    });
                }
                else {
                    self.viewModelHelper.apiPut('api/ResourceGrouping', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Resource grouping details updated successfully.", "Resource Grouping");
                        self.LoadResourceGrpList();
                        self.viewMode('List');
                        $('#spnTitile').html("Resource Grouping");
                    });
                }
            }
            else {
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        //add Resource Grouping view mode
        self.addResourceGroup = function () {
            self.IsAddMode(true);
            self.IsEditMode(false);
            self.viewMode('Form');
            self.IsViewMode(true);
            self.DisplayMode('ADD');
            self.IsResourceCodeEnable(true);
            self.IsResourceNameEnable(true);
            self.IsSaveVisible(true);
            self.IsResetVisible(true);
            self.ResourceModel(new IPMSROOT.ResourceGroupModel());
            $('#spnTitile').html("Add Resource Grouping");

            var dataSource = new kendo.data.DataSource({
                data: []
            });
            var ddlMulti = $("#MultiSelect1").data("kendoMultiSelect");
            ddlMulti.setDataSource(dataSource);
        }

        //view Resource Grouping view mode
        self.viewData = function (model) {
            self.IsAddMode(false);
            self.IsEditMode(false);
            self.viewMode('Form');
            self.DisplayMode('VIEW');
            self.IsViewMode(false);
            self.IsResourceCodeEnable(false);
            self.IsResourceNameEnable(false);
            self.IsSaveVisible(false);
            self.IsResetVisible(false);
            self.LoadEmployeesEditmode(model);

            model.ResourceEmpList.removeAll();

            $.each(model.ResourceEmployeeGroups(), function (key, value) {
                model.ResourceEmpList.push(value.EmployeeID);
            });
            self.ResourceModel(model);

            $('#spnTitile').html("View Resource Grouping");
        }

        self.LoadEmployeesEditmode = function (model) {
            var multi = $("#MultiSelect1").data("kendoMultiSelect");
            multi.value("");
            self.ResourceEmployeeGroups.removeAll();
            self.ResourceModel().ResourceEmpList.removeAll();
            self.viewModelHelper.apiGet('api/Employees/' + event,
                { ResourceGroupCode: model.ResourceGroupCode(), designationcode: model.DesignationCode(), mode: self.DisplayMode() },
                function (result) {
                    if (result.length > 0) {
                        ko.mapping.fromJS(result, {}, self.ResourceEmployeeGroups);
                        $('#spanvemp').hide();
                    }
                    else {
                        ko.mapping.fromJS(result, {}, self.ResourceEmployeeGroups);
                    }
                }, null, null, false);
        }

        //edit Resource Grouping view mode
        self.editData = function (model) {
            self.IsAddMode(false);
            self.IsEditMode(true);

            self.viewMode('Form');
            self.IsViewMode(true);
            self.DisplayMode('EDIT');
            self.IsResourceCodeEnable(false);
            self.IsResourceNameEnable(false);
            self.IsSaveVisible(true);
            self.IsResetVisible(true);
            self.LoadEmployeesEditmode(model);
            model.ResourceEmpList.removeAll();

            $.each(model.ResourceEmployeeGroups(), function (key, value) {
                model.ResourceEmpList.push(value.EmployeeID);
            });
            self.ResourceModel(model);

            $('#spnTitile').html("Update Resource Grouping");
        }

        //to reset Resource Grouping deails
        self.resetResourceGroup = function (model) {

            ko.validation.reset();

            self.ResourceModel().reset();

            model.validationEnabled(false);
            $('#divValidationError').addClass('display-none');
            model.ResourceEmpList.removeAll();
            if (self.IsEditMode()) {
                self.LoadEmployeesEditmode(model);
            }
            self.ResourceModel(model);
            $.each(model.ResourceEmployeeGroups(), function (key, value) {
                model.ResourceEmpList.push(value.EmployeeID);
            });
        }

        //to cancel Resource Grouping deails
        self.cancelResourceGroup = function () {
            self.viewMode('List');
            $('#spnTitile').html("Resource Grouping");
        }

        //Validate Resource Code Exist
        ValidEventResourceCode = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.ResourceGroupList));
            var entry = JSON.parse(ko.toJSON(data));

            $.each(items, function (index, value) {
                if (value.ResourceGroupCode == entry.ResourceGroupCode) {
                    $('#spanimono').text('Already exists.! Please enter another code.');
                    $('#spanimono').css('display', '');
                    self.ResourceModel().ResourceGroupCode('');
                    $('#ResourceGroupCode').val("");
                }
                return;
            });
        }

        HandleKeyUpResourceCode = function (data, event) {

            var items = JSON.parse(ko.toJSON(self.ResourceGroupList));
            var entry = JSON.parse(ko.toJSON(data));

            $.each(items, function (index, value) {
                if (value.ResourceGroupCode == entry.ResourceGroupCode) {
                    $('#spanimono').css('display', '');
                }
                else {
                    $('#spanimono').css('display', 'none');
                }
            });
        }

        //Validate Resource Name Exist
        ValidEventResourceName = function (data, event) {
            var items = JSON.parse(ko.toJSON(self.ResourceGroupList));
            var entry = JSON.parse(ko.toJSON(data));

            $.each(items, function (index, value) {
                if (value.ResourceGroupName == entry.ResourceGroupName) {
                    $('#spanRsrcName').text('Already exists.! Please enter another name.');
                    $('#spanRsrcName').css('display', '');
                    $('#ResourceGroupName').val("");
                }
                return;
            });
        }

        HandleKeyUpResourceName = function (data, event) {

            var items = JSON.parse(ko.toJSON(self.ResourceGroupList));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.ResourceGroupName == entry.ResourceGroupName) {
                    $('#spanRsrcName').css('display', '');
                }
                else {
                    $('#spanRsrcName').css('display', 'none');
                }
            });
        }
        self.Initialize();
    }

    IPMSRoot.ResourceGroupViewModel = ResourceGroupViewModel;

}(window.IPMSROOT));

function ResourceGroupEmployees(EmployeeID) {
    this.EmployeeID = EmployeeID;
}

//To get validate alphabet with spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[(a-z)(A-Z)(0-9)]+$/;
    return charcheck.test(keychar);
}

