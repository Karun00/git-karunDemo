(function (IPMSRoot) {
    var isView = 0;
    var RevenueStopViewModel = function () {
        var self = this;
        $('#spnTitle').html("Revenue Stop List");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.revenueStopModel = ko.observable(new IPMSROOT.RevenueStopModel());
        self.revenueStopSearchDetails = ko.observable(new IPMSROOT.RevenueStopModel());
        self.revenuestopList = ko.observableArray([]);
        self.revenuestoplistReferenceData = ko.observable();
        self.viewMode = ko.observable();
        self.getagentdtls = ko.observableArray();
        self.hasEnable = ko.observable(true);
        self.issave = ko.observable(false);
        self.isupdate = ko.observable(false);
        self.isview = ko.observable(true);
        self.IsMultiselectEnable = ko.observable(true);

        self.Initialize = function () {
            self.viewMode('List');
            self.GetrevenueStopgridlistData();
            self.LoadrevenuestoplistReferencedata();


        }
        self.cancel = function (data) {
            self.viewMode('List');
            $('#spnTitle').html("Revenue Stop List");
        }

        self.LoadrevenuestoplistReferencedata = function () {
            self.viewModelHelper.apiGet('api/RevenueStoplistReference', null,
                  function (result) {

                      self.revenuestoplistReferenceData(new IPMSRoot.RevenueStopListReferenceData(result));
                      ko.mapping.fromJS(result.AgentDetails, {}, self.getagentdtls);
                  }, null, null, false);
        }



        self.GetrevenueStopgridlistData = function (data) {
            //self.VesselRegistrationList.removeAll();
            //if (isValidate == true) {
            self.viewModelHelper.apiGet('api/GetAllagentdetails', null,
                      function (result) {
                          self.revenuestopList(ko.utils.arrayMap(result, function (item) {
                              return new IPMSRoot.RevenueStopModel(item);
                          }));
                      });

            //}
            //else {
            //    toastr.options.closeButton = true;
            //    toastr.options.positionClass = "toast-top-right";
            //    toastr.warning("Please enter fields", "Revenue stop");
            //}

        }

        self.Viewrevenuestop = function (data) {

            self.revenueStopModel(data);
            self.revenueStopModel().validationEnabled(false);
            $('#spnTitle').html("View Revenue Stop ");
            self.viewMode('Form');
            $("#StopDate").data('kendoDatePicker').enable(false);
            self.isview(false);
            self.isupdate(false);
            self.issave(false);
            $("input[type=radio]").attr('disabled', true);
            self.IsMultiselectEnable(false);
        }

        self.Editrevenuestop = function (data) {

            self.revenueStopModel(data);
            self.revenueStopModel().validationEnabled(false);
            $('#spnTitle').html("Edit Revenue Stop ");
            self.viewMode('Form');
            self.isupdate(true);
            self.isview(true);
            self.issave(false);
            self.IsMultiselectEnable(false);
            $("#StopDate").data('kendoDatePicker').max(new Date());
        }

        // This is used to Add New Shift
        self.addRevenueStop = function () {
            self.revenueStopModel(new IPMSRoot.RevenueStopModel(undefined));
            self.revenueStopModel().validationEnabled(false);
            $('#spnTitle').html("Add Revenue Stop ");
            self.viewMode('Form');
            $("#StopDate").data('kendoDatePicker').max(new Date());
            self.issave(true);
            self.isview(true);
            self.isupdate(false);
            self.IsMultiselectEnable(true);
        }

        self.AgentnameSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.revenueStopModel().RegisteredName(selecteddataItem.RegisteredName);
            self.revenueStopModel().RegistrationNumber(selecteddataItem.RegistrationNumber);
            self.revenueStopModel().AccountNo(selecteddataItem.AccountNo);
            self.revenueStopModel().AgentAccountID(selecteddataItem.AgentAccountID);
            //  self.revenueStopModel().AgentID(selecteddataItem.AgentID);
            self.revenueStopModel().AgentID = selecteddataItem.AgentID


        }
        self.Saverevenuestoplist = function (data) {
            data.validationEnabled(true);
            self.RevenueStopValidation = ko.observable(data);
            self.RevenueStopValidation().errors = ko.validation.group(self.RevenueStopValidation());
            var errors = self.RevenueStopValidation().errors().length;
            if (errors == 0) {
                if (data.AccountStatus() != undefined && data.AccountStatus() != "") {
                    self.viewModelHelper.apiPost('api/SaveRevenueStop', ko.mapping.toJSON(data), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("RevenueStop Submitted successfully", "RevenueStop");
                        self.viewMode('List');
                        self.GetrevenueStopgridlistData();
                    });
                }
                else {
                    toastr.warning("Please select Revenue Account Status", "Revenue Stop");
                    return;
                }
            }
            else {
                self.RevenueStopValidation().errors.showAllMessages();
                toastr.warning("You have some form errors. Please check below", "Revenue Stop");
                return;
            }

        }
        self.resetrevenuestoplist = function (data) {

            self.revenueStopModel().reset();
        }
        self.editrevenueStoplist = function (data) {
            self.viewModelHelper.apiPost('api/UpdateRevenueStop', ko.mapping.toJSON(data), function Message(data) {
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("RevenueStop Submitted successfully", "RevenueStop");
                self.viewMode('List');
                self.GetrevenueStopgridlistData();
            });
        }
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }


        self.Initialize();
    }

    IPMSRoot.RevenueStopViewModel = RevenueStopViewModel;
}(window.IPMSROOT));


