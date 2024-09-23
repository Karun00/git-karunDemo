(function (IPMSRoot) {

    var BudgetedValuesViewModel = function () {

        var self = this;
        $('#spnTitile').html("Budgeted Values");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.BudgetedValuesModel = ko.observable(new IPMSROOT.BudgetedValuesModel());
        self.BudgetedValuesList = ko.observableArray();
        self.PortsDetails = ko.observableArray();

        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.IsAdd = ko.observable(false);
        self.IsValid = ko.observable(false);

        self.validationHelper = new IPMSRoot.validationHelper();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadPorts();
            self.LoadFinanaceYearsList();
            self.viewMode('List');
        }

        //LoadFinanaceYearsList fetches the Budgeted Valuess data from API Service
        self.LoadFinanaceYearsList = function () {
            self.viewModelHelper.apiGet('api/FinanaceYearsList', null,
              function (result) {
                  self.BudgetedValuesList(ko.utils.arrayMap(result, function (item) {

                      return new IPMSRoot.BudgetedValuesModel(item);
                  }, null, null, false));
              });
        }

        //LoadDesignations fetches the Designation details from API Service
        self.LoadPorts = function () {
            self.viewModelHelper.apiGet('api/GetAllPorts', null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.PortsDetails);
              }, null, null, false);
        }

        //To Save Budgeted Values data
        self.SaveBudgetedValues = function (model) {
            model.validationEnabled(true);
            self.BudgetedValuesValidation = ko.observable(model);
            self.BudgetedValuesValidation().errors = ko.validation.group(self.BudgetedValuesValidation());
            var errors = self.BudgetedValuesValidation().errors().length;
            var duplicate = false;

            if (errors == 0) {
                if (self.IsSave() == true) {
                    self.viewModelHelper.apiPost('api/BudgetedValuesDetails', ko.mapping.toJSON(model),
                        function Message(data) {
                            model.RecordStatus(data.RecordStatus);
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Budgeted values saved successfully.", "Budgeted Values");
                            self.LoadFinanaceYearsList();
                            $('#spnTitile').html("Budgeted Values");
                            self.viewMode('List');
                        });
                }
            }
            else {
                self.BudgetedValuesValidation().errors.showAllMessages();
                return;
            }
        }

        //To Update Budgeted Values data
        self.ModifyBudgetedValues = function (model) {
            model.validationEnabled(true);

            self.BudgetedValuesValidation = ko.observable(model);
            self.BudgetedValuesValidation().errors = ko.validation.group(self.BudgetedValuesValidation());
            var errors = self.BudgetedValuesValidation().errors().length;

            if (errors == 0 && self.IsModified() == true) {
                self.viewModelHelper.apiPut('api/BudgetedValuesDetails', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Budgeted values updated successfully.", "Budgeted Values");
                        self.LoadFinanaceYearsList();
                        $('#spnTitile').html("Budgeted Values");
                        self.viewMode('List');
                    });
            }
            else {
                self.BudgetedValuesValidation().errors.showAllMessages();
                return;
            }
        }

        //To Reset values
        self.ResetBudgetedValues = function (model) {
            self.BudgetedValuesModel().reset();
            ko.validation.reset();
            model.validationEnabled(false);
        }

        self.Cancel = function () {
            self.BudgetedValuesModel().reset();
            self.viewMode('List');
            $('#spnTitile').html("Budgeted Values");
        }

        //To go for View
        self.viewBudgetedValues = function (BudgetedValues) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsSave(false);
            self.IsReset(false);
            self.IsUpdate(false);
            self.BudgetedValuesModel(BudgetedValues);
            self.IsAdd(false);
            self.editableView(false);

            $('#spnTitile').html("View Budgeted Values");
        }

        //To go for Edit
        self.editBudgetedValues = function (BudgetedValues) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.BudgetedValuesModel(BudgetedValues);
            self.IsCodeEnable(false);
            self.IsAdd(true);

            $('#spnTitile').html("Update Budgeted Values");
        }

        HandleDesignationCodeKeyUp = function (data, event) {
            if (self.UniqueDesignationCodeVisible() == true) {
                self.UniqueDesignationCodeVisible(false);
            }
        }

        self.Initialize();
    }
    IPMSRoot.BudgetedValuesViewModel = BudgetedValuesViewModel;
}(window.IPMSROOT));

function subscribeToModelChange(viewModel) {
    var localmodel = viewModel.BudgetedValuesModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            localmodel[prop].subscribe(function () { onModelChanged(viewModel); });
    }
}

function onModelChanged(viewModel) {
    var localmodel = viewModel.BudgetedValuesModel();
    for (var prop in localmodel) {
        if (ko.isObservable(localmodel[prop]))
            if (localmodel[prop]() && localmodel[prop]() != localmodel.cache.latestData[prop]) {
                viewModel.IsSaveUpdateDisabled(false);
                return;
            }
    }
    viewModel.IsSaveUpdateDisabled(true);
}








