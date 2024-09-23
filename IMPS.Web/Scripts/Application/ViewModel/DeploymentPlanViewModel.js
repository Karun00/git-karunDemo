(function (IPMSRoot) {

    var DeploymentPlanViewModel = function () {

        var self = this;
        $('#spnTitile').html("Deployment Plan");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.deploymentplanModel = ko.observable();
        self.DeploymentPlanList = ko.observableArray();
        self.PlannedDeploymentList = ko.observableArray();
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
        self.SelectedSubCatCode = ko.observable();
        self.SelectedMonths = ko.observable();
        self.deploymentplanReferenceData = ko.observable();
        self.AprColor = ko.observable();
        self.masterDeploymentPlanTypes = ko.observableArray([]);
        self.masterDeploymentPlanType = ko.observableArray([]);
        self.craftDredgerList = ko.observableArray();
        self.IsBudgetEnable = ko.observable(true);
        self.FinancialYearList = ko.observableArray();
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.deploymentplanModel(new IPMSROOT.DeploymentPlanModel());
            self.LoadDeploymentPlans();
            self.LoadInitialData();
            self.LoadDeploymentPlanTypes();
            self.LoadCraftNames();
            self.viewMode('List');
        }
        
        self.LoadDeploymentPlans = function () {
            self.viewModelHelper.apiGet('api/DeploymentPlan',
            null,
              function (result) {

                  self.DeploymentPlanList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.DeploymentPlanModel(item, self.masterDeploymentPlanTypes(), self.masterDeploymentPlanType());
                  }));


              });

        }     

        // Reference Data
        self.LoadInitialData = function () {
            self.viewModelHelper.apiGet('api/DeploymentPlanReferenceData', null,
                function (result1) {
                    self.deploymentplanReferenceData(new IPMSRoot.DeploymentPlanReferenceData(result1));
                }, null, null, false);

        }
        // For Grid Display of Planned Deployments in Add Screen
        self.LoadPlannedDeployments = function () {
            self.viewModelHelper.apiGet('api/PlannedDeployments',
            null,
              function (result) {

                  self.PlannedDeploymentList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.DeploymentPlanModel(item, self.masterDeploymentPlanTypes());
                  }));


              });

        }

        self.LoadCraftNames = function () {
            self.viewModelHelper.apiGet('api/DeploymentPlanCraft',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.craftDredgerList);
              });
        }

        // Deployment Plan Types
        self.LoadDeploymentPlanTypes = function () {
            self.viewModelHelper.apiGet('api/DeploymentPlanTypes', null,
           function (result) {
               self.masterDeploymentPlanTypes(ko.utils.arrayMap(result, function (item) {
                   return new IPMSRoot.DeploymentPlanType(item);
               }));
               self.masterDeploymentPlanType(ko.utils.arrayMap(result, function (item) {
                   return new IPMSRoot.DeploymentPlanType(item);
               }));

           });
        }


        //This is used to Validate numeric
        ValidateNumeric = function () {
            debugger;
            return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
        };
        self.adddeploymentplan = function () {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(false);
            self.IsBudgetEnable(true);
            self.deploymentplanModel(new IPMSRoot.DeploymentPlanModel());
            self.LoadDeploymentPlanTypes();
            $('#spnTitile').html("Add Deployment Plan");
        }
      


        self.ResetSupCat = function (model) {

            $('#divValidationError').addClass('display-none');
            ko.validation.reset();
            model.validationEnabled(false);
            self.deploymentplanModel().reset();
            $('#spanPortName').text('');
       
        }
        self.Cancel = function () {
            self.viewMode('List');
            self.deploymentplanModel().reset();
            $('#spnTitile').html("Deployment Plan");
            self.LoadDeploymentPlanTypes();
        }
      

        self.viewdeploymentplan = function (deploymentplan) {
            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsBudgetEnable(false);
            self.deploymentplanModel(deploymentplan);
            var keyList = deploymentplan.DeploymentBudget();
            var masterList = self.masterDeploymentPlanTypes();
            var craf = self.craftDredgerList();
       
                $.each(masterList, function (key, val) {
                    $.each(keyList, function (key, val1) {
                        if (val.SubCatCode() == val1.SubCatCode) {
                            val.Budget(val1.Budget);
                            val.DredgPlan(val1.DredgPlan);
                            val.SubCatCode(val1.SubCatCode);
                            val.Apr(val1.Apr);
                            val.May(val1.May);
                            val.Jun(val1.Jun);
                            val.Jul(val1.Jul);
                            val.Aug(val1.Aug);
                            val.Sep(val1.Sep);
                            val.Oct(val1.Oct);
                            val.Nov(val1.Nov);
                            val.Dec(val1.Dec);
                            val.Jan(val1.Jan);
                            val.Feb(val1.Feb);
                            val.Mar(val1.Mar);
                            val.AprCraftID(val1.AprCraftID);
                            val.MayCraftID(val1.MayCraftID);
                            val.JunCraftID(val1.JunCraftID);
                            val.JulCraftID(val1.JulCraftID);
                            val.AugCraftID(val1.AugCraftID);
                            val.SepCraftID(val1.SepCraftID);
                            val.OctCraftID(val1.OctCraftID);
                            val.NovCraftID(val1.NovCraftID);
                            val.DecCraftID(val1.DecCraftID);
                            val.JanCraftID(val1.JanCraftID);
                            val.FebCraftID(val1.FebCraftID);
                            val.MarCraftID(val1.MarCraftID);
                            for (var i = 0; i < craf.length; i++) {
                                var id_only = craf[i].CraftID();
                                var color_only = craf[i].DredgerColorCode();
                                if (val.AprCraftID() == id_only)
                                    val.AprColor(color_only);
                                if (val.MayCraftID() == id_only)
                                    val.MayColor(color_only);
                                if (val.JunCraftID() == id_only)
                                    val.JunColor(color_only);
                                if (val.JulCraftID() == id_only)
                                    val.JulColor(color_only);
                                if (val.AugCraftID() == id_only)
                                    val.AugColor(color_only);
                                if (val.SepCraftID() == id_only)
                                    val.SepColor(color_only);
                                if (val.OctCraftID() == id_only)
                                    val.OctColor(color_only);
                                if (val.NovCraftID() == id_only)
                                    val.NovColor(color_only);
                                if (val.DecCraftID() == id_only)
                                    val.DecColor(color_only);
                                if (val.JanCraftID() == id_only)
                                    val.JanColor(color_only);
                                if (val.FebCraftID() == id_only)
                                    val.FebColor(color_only);
                                if (val.MarCraftID() == id_only)
                                    val.MarColor(color_only);
                            }
                        }
                        return;
                    });
                });
                $('#spnTitile').html("View Deployment Plan");
            }
       // }
        self.editdeploymentplan = function (deploymentplan) {
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsBudgetEnable(true);
            self.deploymentplanModel(deploymentplan);
            
            var keyList = deploymentplan.DeploymentBudget();
            var masterList = self.masterDeploymentPlanTypes();

            var masterList1 = self.masterDeploymentPlanType();
     
            //}
            $.each(masterList, function (key, val) {
                $.each(keyList, function (key, val1) {
                    if (val.SubCatCode() == val1.SubCatCode) {
                        val.Budget(val1.Budget);
                        val.DredgPlan(val1.DredgPlan);
                        val.SubCatCode(val1.SubCatCode);                        
                        val.Apr(val1.Apr);                        
                        val.May(val1.May);
                        val.Jun(val1.Jun);
                        val.Jul(val1.Jul);
                        val.Aug(val1.Aug);
                        val.Sep(val1.Sep);
                        val.Oct(val1.Oct);
                        val.Nov(val1.Nov);
                        val.Dec(val1.Dec);
                        val.Jan(val1.Jan);
                        val.Feb(val1.Feb);
                        val.Mar(val1.Mar);
                        val.AprCraftID(val1.AprCraftID);
                        val.MayCraftID(val1.MayCraftID);
                        val.JunCraftID(val1.JunCraftID);
                        val.JulCraftID(val1.JulCraftID);
                        val.AugCraftID(val1.AugCraftID);
                        val.SepCraftID(val1.SepCraftID);
                        val.OctCraftID(val1.OctCraftID);
                        val.NovCraftID(val1.NovCraftID);
                        val.DecCraftID(val1.DecCraftID);
                        val.JanCraftID(val1.JanCraftID);
                        val.FebCraftID(val1.FebCraftID);
                        val.MarCraftID(val1.MarCraftID);
                        var craf = self.craftDredgerList();
                        for (var i = 0; i < craf.length; i++) {
                            var id_only = craf[i].CraftID();
                            var color_only = craf[i].DredgerColorCode();
                            if (val.AprCraftID() == id_only)
                                val.AprColor(color_only);
                            if (val.MayCraftID() == id_only)
                                val.MayColor(color_only);
                            if (val.JunCraftID() == id_only)
                                val.JunColor(color_only);
                            if (val.JulCraftID() == id_only)
                                val.JulColor(color_only);
                            if (val.AugCraftID() == id_only)
                                val.AugColor(color_only);
                            if (val.SepCraftID() == id_only)
                                val.SepColor(color_only);
                            if (val.OctCraftID() == id_only)
                                val.OctColor(color_only);
                            if (val.NovCraftID() == id_only)
                                val.NovColor(color_only);
                            if (val.DecCraftID() == id_only)
                                val.DecColor(color_only);
                            if (val.JanCraftID() == id_only)
                                val.JanColor(color_only);
                            if (val.FebCraftID() == id_only)
                                val.FebColor(color_only);
                            if (val.MarCraftID() == id_only)
                                val.MarColor(color_only);
                        }
                    }
                    return;
                });
            });


            $.each(masterList1, function (key, val) {
                $.each(keyList, function (key, val1) {
                    if (val.SubCatCode() == val1.SubCatCode) {
                        val.Budget(val1.Budget);
                        val.DredgPlan(val1.DredgPlan);
                        val.SubCatCode(val1.SubCatCode);
                        val.Apr(val1.Apr);
                        val.May(val1.May);
                        val.Jun(val1.Jun);
                        val.Jul(val1.Jul);
                        val.Aug(val1.Aug);
                        val.Sep(val1.Sep);
                        val.Oct(val1.Oct);
                        val.Nov(val1.Nov);
                        val.Dec(val1.Dec);
                        val.Jan(val1.Jan);
                        val.Feb(val1.Feb);
                        val.Mar(val1.Mar);
                        val.AprCraftID(val1.AprCraftID);
                        val.MayCraftID(val1.MayCraftID);
                        val.JunCraftID(val1.JunCraftID);
                        val.JulCraftID(val1.JulCraftID);
                        val.AugCraftID(val1.AugCraftID);
                        val.SepCraftID(val1.SepCraftID);
                        val.OctCraftID(val1.OctCraftID);
                        val.NovCraftID(val1.NovCraftID);
                        val.DecCraftID(val1.DecCraftID);
                        val.JanCraftID(val1.JanCraftID);
                        val.FebCraftID(val1.FebCraftID);
                        val.MarCraftID(val1.MarCraftID);
                        var craf = self.craftDredgerList();
                        for (var i = 0; i < craf.length; i++) {
                            var id_only = craf[i].CraftID();
                            var color_only = craf[i].DredgerColorCode();
                            if (val.AprCraftID() == id_only)
                                val.AprColor(color_only);
                            if (val.MayCraftID() == id_only)
                                val.MayColor(color_only);
                            if (val.JunCraftID() == id_only)
                                val.JunColor(color_only);
                            if (val.JulCraftID() == id_only)
                                val.JulColor(color_only);
                            if (val.AugCraftID() == id_only)
                                val.AugColor(color_only);
                            if (val.SepCraftID() == id_only)
                                val.SepColor(color_only);
                            if (val.OctCraftID() == id_only)
                                val.OctColor(color_only);
                            if (val.NovCraftID() == id_only)
                                val.NovColor(color_only);
                            if (val.DecCraftID() == id_only)
                                val.DecColor(color_only);
                            if (val.JanCraftID() == id_only)
                                val.JanColor(color_only);
                            if (val.FebCraftID() == id_only)
                                val.FebColor(color_only);
                            if (val.MarCraftID() == id_only)
                                val.MarColor(color_only);
                        }

                    }
                    return;
                });
            });



            self.IsCodeEnable(false);
            $('#spnTitile').html("Update Deployment Plan");
        }
        //   }
        Velidport = function (data, event) {

            debugger;
            $('#spanPortName').text('');
            if ($("#FinancialYear").val() == "") {
                $('#spanfinancialyear').text('Select Financial year First');
                $('#spanfinancialyear').css('display', '');
            }
            
            else {
                $('#spanPortName').text('');
                var flag = true;
                var items = JSON.parse(ko.toJSON(self.DeploymentPlanList));
                var entry = JSON.parse(ko.toJSON(data));
                $.each(items, function (index, value) {
                    if (value.FinancialYearID == entry.FinancialYearID) {
                            if (value.PortCode == entry.PortCode) {
                                $('#spanPortName').text('Deployment plan already exists for this port for the selected financial year');
                                $('#spanPortName').css('display', '');
                                flag = false;

                            }

                    }
                    return;
                });


                if (flag == true) {
                    $('#spanPortName').text('');
                }
            }

        }
        self.SaveDeploymentPlan = function (model) {
            model.validationEnabled(true);
            self.DeploymentPlanValidation = ko.observable(model);
            self.DeploymentPlanValidation().errors = ko.validation.group(self.DeploymentPlanValidation());
            var errors = self.DeploymentPlanValidation().errors().length;
            if ($('#spanPortName').text() != '') {
                 errors++;
            }
            var duplicate = false;
            var emptyfield = false;

            var List = self.masterDeploymentPlanTypes();

            var DeploymentBudgetList = [];
            var Error = "Y";
            $.each(List, function (key, val) {
                var _Apr = val.Apr(); var _AprCraftID = val.AprCraftID(); var _May = val.May(); var _MayCraftID = val.MayCraftID();
                var _Jun = val.Jun(); var _JunCraftID = val.JunCraftID(); var _Jul = val.Jul(); var _JulCraftID = val.JulCraftID();
                var _Aug = val.Aug(); var _AugCraftID = val.AugCraftID(); var _Sep = val.Sep(); var _SepCraftID = val.SepCraftID();
                var _Oct = val.Oct(); var _OctCraftID = val.OctCraftID(); var _Nov = val.Nov(); var _NovCraftID = val.NovCraftID();
                var _Dec = val.Dec(); var _DecCraftID = val.DecCraftID(); var _Jan = val.Jan(); var _JanCraftID = val.JanCraftID();
                var _Feb = val.Feb(); var _FebCraftID = val.FebCraftID(); var _Mar = val.Mar(); var _MarCraftID = val.MarCraftID();
                var _Budget = val.Budget(); var _DredgPlan = val.DredgPlan(); var _SubCatCode = val.SubCatCode();

                debugger;
                var rowid = key + 1;
                if (val.Budget() == undefined || val.Budget() == "") {
                    if (Error == "Y") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Enter Budget For Row: " + rowid, "Deployment Plan");
                        Error = "N";
                        emptyfield = true;
                        return;
                    }
                }
                else { }
                if (val.Budget() < val.DredgPlan()) {
                    if (Error == "Y") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Budget should be greater than plan value For Row: " + rowid, "Deployment Plan");
                        Error = "N";
                        emptyfield = true;
                        return;
                    }
                }
                else { }
                if (val.Budget() != null)
                DeploymentBudgetList.push(new BudgetList(_Apr, _AprCraftID, _May, _MayCraftID, _Jun, _JunCraftID, _Jul, _JulCraftID, _Aug,
                    _AugCraftID, _Sep, _SepCraftID, _Oct, _OctCraftID, _Nov, _NovCraftID,
                    _Dec, _DecCraftID, _Jan, _JanCraftID, _Feb, _FebCraftID, _Mar, _MarCraftID, _Budget, _DredgPlan, _SubCatCode));


            });
            model.DeploymentBudget = DeploymentBudgetList;
            if (errors == 0) {
            if (self.IsSave() == true && emptyfield == false) {

                    self.viewModelHelper.apiPost('api/DeploymentPlan', ko.mapping.toJSON(model), function Message(data) {
                        model.RecordStatus(data.RecordStatus);
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Deployment Plan Details Saved Successfully", "Deployment Plan");
                        self.LoadPlannedDeployments();
                        self.LoadDeploymentPlans();
                        $('#spnTitile').html("Deployment Plan  Master");
                        self.viewMode('List');

                    });
            }

            }
            else {
                self.DeploymentPlanValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ModifyDeploymentPlan = function (model) {   
            model.validationEnabled(true);
            self.DeploymentPlanValidation = ko.observable(model);
            self.DeploymentPlanValidation().errors = ko.validation.group(self.DeploymentPlanValidation());
            var errors = self.DeploymentPlanValidation().errors().length;
            var duplicate = false;
            var emptyfield = false;

            var List = self.masterDeploymentPlanTypes();


            var DeploymentBudgetList = [];
            var Error = "Y";
            $.each(List, function (key, val) {
                var _Apr = val.Apr(); var _AprCraftID = val.AprCraftID(); var _May= val.May();  var _MayCraftID= val.MayCraftID(); 
                var _Jun = val.Jun(); var _JunCraftID = val.JunCraftID(); var _Jul = val.Jul(); var _JulCraftID = val.JulCraftID();
                var _Aug = val.Aug(); var _AugCraftID = val.AugCraftID(); var _Sep = val.Sep(); var _SepCraftID = val.SepCraftID();
                var _Oct = val.Oct(); var _OctCraftID = val.OctCraftID(); var _Nov = val.Nov(); var _NovCraftID = val.NovCraftID();
                var _Dec = val.Dec(); var _DecCraftID = val.DecCraftID(); var _Jan = val.Jan(); var _JanCraftID = val.JanCraftID();
                var _Feb = val.Feb(); var _FebCraftID = val.FebCraftID(); var _Mar = val.Mar(); var _MarCraftID = val.MarCraftID();
                var _Budget = val.Budget(); var _DredgPlan = val.DredgPlan(); var _SubCatCode = val.SubCatCode();
                debugger;
                var rowid = key + 1;
                if (val.Budget() == undefined || val.Budget().length == "") {
                    if (Error == "Y") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Please Enter Budget For Row: " + rowid, "Deployment Plan");
                        Error = "N";
                        emptyfield = true;
                        return;
                    }
                }
                else { }
                if (val.Budget() < val.DredgPlan()) {
                    if (Error == "Y") {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Budget should be greater than plan value For Row: " + rowid, "Deployment Plan");
                        Error = "N";
                        emptyfield = true;
                        return;
                    }
                }
                else { }
                debugger;
                if (emptyfield == false)
                    DeploymentBudgetList.push(new BudgetList(_Apr, _AprCraftID, _May, _MayCraftID, _Jun, _JunCraftID, _Jul, _JulCraftID, _Aug,
                        _AugCraftID, _Sep, _SepCraftID, _Oct, _OctCraftID, _Nov, _NovCraftID,
                        _Dec, _DecCraftID, _Jan, _JanCraftID, _Feb, _FebCraftID, _Mar, _MarCraftID, _Budget, _DredgPlan, _SubCatCode));


            });
            if (errors == 0 && emptyfield == false) {
                model.DeploymentBudget = DeploymentBudgetList;
                self.viewModelHelper.apiPut('api/DeploymentPlan', ko.mapping.toJSON(model), function Message(data) {
                    model.RecordStatus(data.RecordStatus);
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("Deployment Plan Details Updated Successfully", "Deployment Plan");
                    self.LoadPlannedDeployments();
                    self.LoadDeploymentPlans();
                    self.LoadDeploymentPlanTypes();
                    $('#spnTitile').html("Deployment Plan  Master");
                    self.viewMode('List');

                });

            }
            else {
                
                self.DeploymentPlanValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        function BudgetList(Apr, AprCraftID, May, MayCraftID, Jun, JunCraftID, Jul, JulCraftID, Aug, AugCraftID, Sep, SepCraftID, Oct, OctCraftID, Nov, NovCraftID, Dec, DecCraftID, Jan, JanCraftID, Feb, FebCraftID, Mar, MarCraftID, Budget, DredgPlan, SubCatCode) {
            this.Apr = Apr;          
            this.May = May;
            this.Jun = Jun;
            this.Jul = Jul;
            this.Aug = Aug;
            this.Sep = Sep;
            this.Oct = Oct;
            this.Nov = Nov;
            this.Dec = Dec;
            this.Jan = Jan;
            this.Feb = Feb;
            this.Mar = Mar;

            this.AprCraftID = AprCraftID;
            this.MayCraftID = MayCraftID;
            this.JunCraftID = JunCraftID;
            this.JulCraftID = JulCraftID;
            this.AugCraftID = AugCraftID;
            this.SepCraftID = SepCraftID;
            this.OctCraftID = OctCraftID;
            this.NovCraftID = NovCraftID;
            this.DecCraftID = DecCraftID;
            this.JanCraftID = JanCraftID;
            this.FebCraftID = FebCraftID;
            this.MarCraftID = MarCraftID;

            this.Budget = Budget;
            this.DredgPlan = DredgPlan;
            this.SubCatCode = SubCatCode;
        }
      

        CalculatePlan = function (data, event) {

            debugger;            var may = data.May() == undefined || data.May() == "" ? 0 : parseInt(data.May());                      var total = (data.Apr() == undefined || data.Apr() == "" ? 0 : parseInt(data.Apr())) + (data.May() == undefined || data.May() == "" ? 0 : parseInt(data.May())) +
                (data.Jun() == undefined || data.Jun() == "" ? 0 : parseInt(data.Jun())) + (data.Jul() == undefined || data.Jul() == "" ? 0 : parseInt(data.Jul())) +
                (data.Aug() == undefined || data.Aug() == "" ? 0 : parseInt(data.Aug())) + (data.Sep() == undefined || data.Sep() == "" ? 0 : parseInt(data.Sep())) +
                (data.Oct() == undefined || data.Oct() == "" ? 0 : parseInt(data.Oct())) + (data.Nov() == undefined || data.Nov() == "" ? 0 : parseInt(data.Nov())) +
                (data.Dec() == undefined || data.Dec() == "" ? 0 : parseInt(data.Dec())) + (data.Jan() == undefined || data.Jan() == "" ? 0 : parseInt(data.Jan())) +
                (data.Feb() == undefined || data.Feb() == "" ? 0 : parseInt(data.Feb())) + (data.Mar() == undefined || data.Mar() == "" ? 0 : parseInt(data.Mar()));
         
            data.DredgPlan(total);
        }


        AssignDredger = function (data, event) {
            // For PopUp
            $('#stack1').modal('show');
            self.SelectedSubCatCode(data.SubCatCode());
            self.SelectedMonths(event.target.id);
            self.deploymentplanModel().SubCatCode(data.SubCatCode());
        } 
       
          

  
        self.SaveDredger = function (data, event) {
            var masterdata = self.masterDeploymentPlanTypes();
            //---------------------------------------------------------------------------------
            var craf = self.craftDredgerList();
            for (var i = 0; i < craf.length; i++) {
                var id_only = craf[i].CraftID();
                var color_only = craf[i].DredgerColorCode();
                //-----------------------------------------------------------------------------------------

                if (data.CraftID() != "" || data.CraftID() != undefined) {
                    $.each(masterdata, function (key, val) {
                        if (val.SubCatCode() == self.SelectedSubCatCode()) {
                            if (self.SelectedMonths() == "AprId") {
                                val.AprCraftID(data.CraftID());
                                if (data.CraftID() == id_only)//{
                                    val.AprColor(color_only);//}
                                else if (data.CraftID() == "")
                                    val.AprColor("");
                            }

                            else if (self.SelectedMonths() == "MayId") {
                                val.MayCraftID(data.CraftID());
                                if (data.CraftID() == id_only)
                                    val.MayColor(color_only);
                                else if (data.CraftID() == "")
                                    val.MayColor("");
                            }

                            else if (self.SelectedMonths() == "JunId") {
                                val.JunCraftID(data.CraftID());
                              
                                if (data.CraftID() == id_only)
                                    val.JunColor(color_only);
                                else if (data.CraftID() == "")
                                    val.JunColor("");
                            }
                            else if (self.SelectedMonths() == "JulId") {
                                val.JulCraftID(data.CraftID());
                              
                                if (data.CraftID() == id_only)
                                    val.JulColor(color_only);
                                else if (data.CraftID() == "")
                                    val.JulColor("");
                            }
                            else if (self.SelectedMonths() == "AugId") {
                                val.AugCraftID(data.CraftID());
                             
                                if (data.CraftID() == id_only)
                                    val.AugColor(color_only);
                                else if (data.CraftID() == "")
                                    val.AugColor("");
                            }
                            else if (self.SelectedMonths() == "SepId") {
                                val.SepCraftID(data.CraftID());
                             
                                if (data.CraftID() == id_only)
                                    val.SepColor(color_only);
                                else if (data.CraftID() == "")
                                    val.SepColor("");
                            }
                            else if (self.SelectedMonths() == "OctId") {
                                val.OctCraftID(data.CraftID());                              
                                if (data.CraftID() == id_only)
                                    val.OctColor(color_only);
                                else if (data.CraftID() == "")
                                    val.OctColor("");
                            }
                            else if (self.SelectedMonths() == "NovId") {
                                val.NovCraftID(data.CraftID());
                             
                                if (data.CraftID() == id_only)
                                    val.NovColor(color_only);
                                else if (data.CraftID() == "")
                                    val.NovColor("");
                            }
                            else if (self.SelectedMonths() == "DecId") {
                                val.DecCraftID(data.CraftID());                              
                                if (data.CraftID() == id_only)
                                    val.DecColor(color_only);
                                else if (data.CraftID() == "")
                                    val.DecColor("");
                            }
                            else if (self.SelectedMonths() == "JanId") {
                                val.JanCraftID(data.CraftID());
                                if (data.CraftID() == id_only)
                                    val.JanColor(color_only);
                                else if (data.CraftID() == "")
                                    val.JanColor("");
                            }
                            else if (self.SelectedMonths() == "FebId") {
                                val.FebCraftID(data.CraftID());
                               
                                if (data.CraftID() == id_only)
                                    val.FebColor(color_only);
                                else if (data.CraftID() == "")
                                    val.FebColor("");
                            }
                            else if (self.SelectedMonths() == "MarId") {
                                val.MarCraftID(data.CraftID());
                               
                                if (data.CraftID() == id_only)
                                    val.MarColor(color_only);
                                else if (data.CraftID() == "")
                                    val.MarColor("");
                            }
                        }
                    });
                }

                $('#stack1').modal('hide');
            }
        }


        self.Initialize();
    }
    IPMSRoot.DeploymentPlanViewModel = DeploymentPlanViewModel;


}(window.IPMSROOT));


