(function (IPMSRoot) {

    var EmployeeViewModel = function () {
        var self = this;
        $('#EmployeeTitle').html("Employee");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.employeeList = ko.observableArray();
        self.employeeModel = ko.observable();
        self.departmentValues = ko.observableArray();
        self.designationValues = ko.observableArray();
        self.businessunitsValues = ko.observableArray();
        self.costcentersValues = ko.observableArray();
        self.payrollareasValues = ko.observableArray();
        self.psgroupsValues = ko.observableArray();
        self.personalsubareasValues = ko.observableArray();
        self.organizationalunitsValues = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.IsUnique = ko.observable(true);
        self.OfficialMobileNo = ko.observable();
        self.PersonalMobileNo = ko.observable();
        self.validationHelper = new IPMSROOT.validationHelper();
        self.designationCode = ko.observable();
        self.employeeID = ko.observable();

        self.portCode = ko.observable(); // Added by sandeep on 14-09-2015

        //Employee Initialization(pageload) mode
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.employeeModel(new IPMSROOT.EmployeeModel());
            self.LoadEmployeeList();
            self.LoadDepartments();
            self.LoadDesignations();
            self.LoadBusinessUnits();
            self.LoadCostCenters();
            self.LoadPayrollAreas();
            self.LoadPSGroups();
            self.LoadPersonalSubAreas();
            self.LoadOrganizationalUnits();
            self.viewMode('List');
        }

        //LoadEmployees fetches the data from API Service
        self.LoadEmployeeList = function (model) {

            var dcode = null;
            var search = null;
            if (model != undefined) {
                
                dcode = model.DesignationCode();
                if (dcode == undefined) {
                    dcode = null;
                }
                search = model.searchText();
                if (search == undefined || search == "") {
                    search = null;
                }
            }



            self.viewModelHelper.apiGet('api/Employee/GetEmployeesList', { designation: dcode, searchText: search },
            function (result) {
                self.employeeList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.EmployeeModel(item);
                }));
            });
        }

        self.ResetData = function () {
            $("#EMPSAPid").val("");
            $("select#searchDesignation").prop('selectedIndex', 0);
            self.employeeModel().searchText('');
            self.employeeModel().DesignationCode(undefined);
            self.LoadEmployeeList();
        }

        //******************Binding Dropdowns Start's here*****************************//

        //LoadDepartments fetches the Department details from API Service 
        self.LoadDepartments = function () {
            self.viewModelHelper.apiGet('api/Departments',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.departmentValues);

              });
        }

        //LoadDesignations fetches the Designation details from API Service
        self.LoadDesignations = function () {
            self.viewModelHelper.apiGet('api/Designations',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.designationValues);

              });
        }

        //LoadBusinessUnits fetches the Business Unit details from API Service
        self.LoadBusinessUnits = function () {
            //self.viewModelHelper.apiGet('api/BusinessUnits', // Commented by sandeep on 15-09-2015
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', // Added by sandeep on 15-09-2015
null,
  function (result) {
      ko.mapping.fromJS(result, {}, self.businessunitsValues);
  });
        }

        //LoadCostCenters fetches the Cost Center details from API Service
        self.LoadCostCenters = function () {
            self.viewModelHelper.apiGet('api/CostCenters',
    null,
      function (result) {
          ko.mapping.fromJS(result, {}, self.costcentersValues);
      });
        }

        //LoadPayrollAreas fetches the PayrollAreas details from API Service
        self.LoadPayrollAreas = function () {
            self.viewModelHelper.apiGet('api/PayrollAreas',
    null,
      function (result) {
          ko.mapping.fromJS(result, {}, self.payrollareasValues);
      });
        }

        //LoadPSGroups fetches the PG Group details from API Service
        self.LoadPSGroups = function () {
            self.viewModelHelper.apiGet('api/PSGroups',
    null,
      function (result) {
          ko.mapping.fromJS(result, {}, self.psgroupsValues);
      });
        }

        //LoadPersonalSubAreas fetches the Personal Sub Areas details from API Servicelist
        self.LoadPersonalSubAreas = function () {
            self.viewModelHelper.apiGet('api/PersonalSubAreas',
    null,
      function (result) {
          ko.mapping.fromJS(result, {}, self.personalsubareasValues);
      });
        }

        //LoadOrganizationalUnits fetches the Organizational Unit details from API Servicelist
        self.LoadOrganizationalUnits = function () {
            self.viewModelHelper.apiGet('api/OrganizationalUnits',
    null,
      function (result) {
          ko.mapping.fromJS(result, {}, self.organizationalunitsValues);
      });
        }

        //******************Binding Dropdowns End's here*****************************//

        //Employee in Add mode
        self.addEmployee = function () {
            self.employeeModel(new IPMSRoot.EmployeeModel());
            $('#EmployeeTitle').html("Add New Employee");
            $("#SAPNumber").kendoMaskedTextBox({ mask: "aaaaaaa"});
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            //Masking Of Official MobileNo.
            $("#OfficialMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OfficialMobileNomaskedtextbox = $("#OfficialMobileNo").data("kendoMaskedTextBox");
            self.OfficialMobileNo = OfficialMobileNomaskedtextbox.value();

            //Masking Of Personal MobileNo.
            $("#PersonalMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var PersonalMobileNomaskedtextbox = $("#PersonalMobileNo").data("kendoMaskedTextBox");
            self.PersonalMobileNo = PersonalMobileNomaskedtextbox.value();
            $("#SAPNumber").kendoMaskedTextBox({mask: "aaaaaaa" });
            var SAPNumberMaskedtextbox = $("#SAPNumber").data("kendoMaskedTextBox");
            self.SAPNumber = SAPNumberMaskedtextbox.value();
        }

        //Employee in Edit mode
        self.EditEmployee = function (employee) {

            $('#EmployeeTitle').html("Update Employee");
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(false);
            self.employeeModel(employee);
            self.designationCode(employee.DesignationCode());
            self.portCode(employee.PortCode()); // Added by sandeep on 14-09-2015
            self.employeeID(employee.EmployeeID());

            //Masking Of Official MobileNo.
            $("#OfficialMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OfficialMobileNomaskedtextbox = $("#OfficialMobileNo").data("kendoMaskedTextBox");
            self.OfficialMobileNo = OfficialMobileNomaskedtextbox.value();

            //Masking Of Personal MobileNo.
            $("#PersonalMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var PersonalMobileNomaskedtextbox = $("#PersonalMobileNo").data("kendoMaskedTextBox");
            self.PersonalMobileNo = PersonalMobileNomaskedtextbox.value();
        }

        //Employee in View mode
        self.ViewEmployee = function (employee) {
            $('#EmployeeTitle').html("View Employee");

            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.employeeModel(employee);
            $("#BirthDate").data('kendoDatePicker').enable(false);
            $("#JoiningDate").data('kendoDatePicker').enable(false);
            //Masking Of Official MobileNo.
            $("#OfficialMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OfficialMobileNomaskedtextbox = $("#OfficialMobileNo").data("kendoMaskedTextBox");
            self.OfficialMobileNo = OfficialMobileNomaskedtextbox.value();
            //Masking Of Personal MobileNo.
            $("#PersonalMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var PersonalMobileNomaskedtextbox = $("#PersonalMobileNo").data("kendoMaskedTextBox");
            self.PersonalMobileNo = PersonalMobileNomaskedtextbox.value();
        }

        //This method is fires when cancel button is pressed and all fields data is cleared and redirected to List form
        self.Cancel = function () {

            self.viewMode('List');
            self.employeeModel().reset();
            self.employeeModel().searchText('');
            self.employeeModel().DesignationCode(undefined);
            self.editableView(true);
            self.LoadEmployeeList();
            $('#EmployeeTitle').html("Employee");
            //Masking Of Official MobileNo.
            $("#OfficialMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OfficialMobileNomaskedtextbox = $("#OfficialMobileNo").data("kendoMaskedTextBox");
            self.OfficialMobileNo = OfficialMobileNomaskedtextbox.value();
            //Masking Of Personal MobileNo.
            $("#PersonalMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var PersonalMobileNomaskedtextbox = $("#PersonalMobileNo").data("kendoMaskedTextBox");
            self.PersonalMobileNo = PersonalMobileNomaskedtextbox.value();
        }

        //This method is fires when reset button pressed to reset the data that present in initial stage
        self.ResetEmployee = function (model) {
            // Modified by Srini Malepati, invisible to Info messages
            ko.validation.reset();
            self.employeeModel().reset();
            ValidationReset();
            //Masking Of Official MobileNo.
            $("#OfficialMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var OfficialMobileNomaskedtextbox = $("#OfficialMobileNo").data("kendoMaskedTextBox");
            self.OfficialMobileNo = OfficialMobileNomaskedtextbox.value();
            //Masking Of Personal MobileNo.
            $("#PersonalMobileNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var PersonalMobileNomaskedtextbox = $("#PersonalMobileNo").data("kendoMaskedTextBox");
            self.PersonalMobileNo = PersonalMobileNomaskedtextbox.value();
            self.EmployeeValidation().errors.showAllMessages(false);
        }

        //Delete button click event
        self.DeleteEmployee = function (model) {
            self.viewModelHelper.apiDelete('api/Employees', ko.mapping.toJSON(model), function Message(data) { });
        }

        /// Check Phone Number Validation
        CheckPhoneValidation = function (PhoneNumber, Type) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            PhoneNumber = PhoneNumber.replace(/(\)|\()|_|-+/g, '');

            if (Type == 'OfficialMobile') {
                var validOfficialMobileNumber = 0;
                if (PhoneNumber.length != 13) {
                    toastr.warning("Invalid Office Mobile Number.");
                    validOfficialMobileNumber++;
             
                }
                else if (PhoneNumber.length == 13) {
                    var validNo = parseInt(PhoneNumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Office Mobile Number");
                        validOfficialMobileNumber++;

                    }

                }
                return validOfficialMobileNumber;
            }

            if (Type == 'PersonalMobile') {
                var validPersonalMobileNumber = 0;
                if (PhoneNumber.length != 13) {
                    toastr.warning("Invalid Personal Mobile Number.");
                    validPersonalMobileNumber++;
              
                }
                else if (PhoneNumber.length == 13) {
                    var validNo = parseInt(PhoneNumber);
                    if (validNo == 0) {
                        toastr.warning("Invalid Personal Mobile Number");
                        validPersonalMobileNumber++;

                    }

                }
                return validPersonalMobileNumber;
            }
        }

        CheckSAPNumberValidation = function (SAPNumber) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            SAPNumber = SAPNumber.replace(/_/g, '');
            var validSAPNumber = 0;
            if (SAPNumber.length != 7) {
                toastr.warning("Invalid SAPNumber");
                validSAPNumber++;
            }
            else if (SAPNumber.length == 7) {
                var validNo = SAPNumber;
                if (validNo == 0) {
                    toastr.warning("Invalid SAPNumber ");
                    validSAPNumber++;
                }
            }
            return validSAPNumber;
        }
        //Add Employee data saving data in API Service 
        self.SaveEmployee = function (model) {

            self.EmployeeValidation = ko.observable(model);
            self.EmployeeValidation().errors = ko.validation.group(self.EmployeeValidation());
            var errors = self.EmployeeValidation().errors().length;
            errors = Validation();

            var filterContanctNumber1 = model.OfficialMobileNo();
            if (filterContanctNumber1 != null || filterContanctNumber1 != '' || filterContanctNumber1 != undefined) {
                var validPhoneNumber = 0;

                if (filterContanctNumber1.length != 0) {
                    validPhoneNumber = CheckPhoneValidation(filterContanctNumber1, "OfficialMobile");
                    if (validPhoneNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }

            var filterSAPNumber = model.SAPNumber();
            if (filterSAPNumber != null || filterSAPNumber != '' || filterSAPNumber != undefined) {
                var validSAPNumber = 0;
                if (filterSAPNumber.length != 0) {
                    validSAPNumber = CheckSAPNumberValidation(filterSAPNumber);
                    if (validSAPNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }

            var filterContanctNumber2 = model.PersonalMobileNo();
            if (filterContanctNumber2 != null || filterContanctNumber2 != '' || filterContanctNumber2 != undefined) {
                var validPhoneNumber = 0;

                if (filterContanctNumber2.length != 0) {
                    validPhoneNumber = CheckPhoneValidation(filterContanctNumber2, "PersonalMobile");
                    if (validPhoneNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }

            if (errors == 0 && self.EmployeeValidation().errors().length == 0) {

                $.each(self.employeeList(), function (index, employee) {

                    if (employee.SAPNumber().toLowerCase() == model.SAPNumber().toLowerCase()) {
                        self.UniqueCodeVisible(true);
                        self.IsUnique(false);
                    }
                    else {
                        self.IsUnique(true);

                    }
                });

                if (self.IsUnique() == true) {

                    model.Department(self.employeeModel().DepartmentCode());
                    model.Designation(self.employeeModel().DesignationCode());
                    model.BusinessUnit(self.employeeModel().BusinessUnitCode());
                    model.CostCenter(self.employeeModel().CostCenterCode());
                    model.PayrollArea(self.employeeModel().PayrollAreaCode());
                    model.PSGroup(self.employeeModel().PSGroupCode());
                    model.PersonalSubArea(self.employeeModel().PersonalSubAreaCode());
                    model.OrganizationalUnit(self.employeeModel().OrganizationalUnitCode());
                    model.Gender(self.employeeModel().GenderCode());
                    model.Age($('#Age').val());
                    model.YearsofService($('#YearsofService').val());
                    self.viewModelHelper.apiPost('api/Employees', ko.mapping.toJSON(model),
                       function Message(data) {
                           toastr.options.closeButton = true;
                           toastr.options.positionClass = "toast-top-right";
                           toastr.success("Employee details saved successfully.", "Employee");
                           self.LoadEmployeeList();
                           self.viewMode('List');
                       });
                }
            }
            else {
                self.EmployeeValidation().errors.showAllMessages(true);
                $('.validationElement:first').focus();

                return;
            }
        }

        //Modify Employee data saving data in API Service
        self.ModifyEmployee = function (model) {
            self.EmployeeValidation = ko.observable(model);
            self.EmployeeValidation().errors = ko.validation.group(self.EmployeeValidation());
            var errors = self.EmployeeValidation().errors().length;
            errors = Validation();

            var filterContanctNumber1 = model.OfficialMobileNo();
            if (filterContanctNumber1 != null || filterContanctNumber1 != '' || filterContanctNumber1 != undefined) {
                var validPhoneNumber = 0;

                if (filterContanctNumber1.length != 0) {
                    validPhoneNumber = CheckPhoneValidation(filterContanctNumber1, "OfficialMobile");
                    if (validPhoneNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }

            var filterContanctNumber2 = model.PersonalMobileNo();
            if (filterContanctNumber2 != null || filterContanctNumber2 != '' || filterContanctNumber2 != undefined) {
                var validPhoneNumber = 0;

                if (filterContanctNumber2.length != 0) {
                    validPhoneNumber = CheckPhoneValidation(filterContanctNumber2, "PersonalMobile");
                    if (validPhoneNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }

            if (errors == 0) {
                model.Department(self.employeeModel().DepartmentCode());
                model.Designation(self.employeeModel().DesignationCode());
                model.BusinessUnit(self.employeeModel().BusinessUnitCode());
                model.CostCenter(self.employeeModel().CostCenterCode());
                model.PayrollArea(self.employeeModel().PayrollAreaCode());
                model.PSGroup(self.employeeModel().PSGroupCode());
                model.PersonalSubArea(self.employeeModel().PersonalSubAreaCode());
                model.OrganizationalUnit(self.employeeModel().OrganizationalUnitCode());
                model.Gender(self.employeeModel().GenderCode());
                model.Age($('#Age').val());
                model.YearsofService($('#YearsofService').val())
                self.viewModelHelper.apiPut('api/Employees', ko.mapping.toJSON(model),
                    function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Employee details updated successfully.", "Employee");
                        self.LoadEmployeeList();
                        self.viewMode('List');
                        $('#EmployeeTitle').html("Employee");
                    });
            } else {
                self.EmployeeValidation().errors.showAllMessages();
                return;
            }

            self.viewMode('List');
        }

        ValidationReset = function () {
            var NoOfErrors = 0;
            $('#spanvdeptcode1').text('');
            $('#spanvdesgcode').text('');
            $('#spanvbucode').text('');
            $('#spanvcccode').text('');
            $('#spanpacode').text('');
            $('#spanpsgcode').text('');
            $('#spanpsacode').text('');
            $('#spanpoucode').text('');

        }

        //DepartmentCode Change
        ChangeDepartmentCode = function () {

            if ($("#Department").val() == "" || $("#Department").val() == null) {
                $('#spanvdeptcode1').text('Please select Department');

            }
            else {
                $("#spanvdeptcode1").text('');
            }
        }

        //Designation Change
        ChangeDesignation = function (data, event) {

            if ($("#Designation").val() == "" || $("#Designation").val() == null) {
                $('#spanvdesgcode').text('Please select Designation');

            }
            else {
                $("#spanvdesgcode").text('');

                var empID = ko.toJSON(self.employeeID());
                if (parseInt(empID) > 0) {
                    self.viewModelHelper.apiGet('api/FindEmployeeIDInResourceGroup/' + self.employeeModel().EmployeeID(), null,
                        function (result) {
                            if (result) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.options.timeout = 3000;
                                toastr.warning("Employee Designation can not be changed, employee used in resource group.", "Employee");
                                data.DesignationCode(self.designationCode());
                            }
                        });
                }

            }
        }

        //BusinessUnit Change
        ChangeBusinessUnit = function (data, event) {
            if ($("#BusinessUnit").val() == "" || $("#BusinessUnit").val() == null) {
                $('#spanvbucode').text('Please select Business Unit');

            }
            else {
                $("#spanvbucode").text('');

                var empID = ko.toJSON(self.employeeID());
                if (parseInt(empID) > 0) {
                    self.viewModelHelper.apiGet('api/FindEmployeeIDInResourceGroup/' + self.employeeModel().EmployeeID(), null,
                        function (result) {
                            if (result) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.options.timeout = 3000;
                                toastr.warning("Employee Business Unit can not be changed, employee used in resource group.", "Employee");
                                data.PortCode(self.portCode());
                            }
                        });
                }

            }
        }

        //CostCenter Change
        ChangeCostCenter = function () {

            if ($("#CostCenter").val() == "" || $("#CostCenter").val() == null) {
                $('#spanvcccode').text('Please select Cost Center');

            }
            else {
                $("#spanvcccode").text('');
            }
        }

        //PayrollArea Change
        ChangePayrollArea = function () {
            if ($("#PayrollArea").val() == "" || $("#PayrollArea").val() == null) {
                $('#spanpacode').text('Please select Payroll Area');

            }
            else {
                $("#spanpacode").text('');
            }
        }

        //PSGroup Change
        ChangePSGroup = function () {
            if ($("#PSGroup").val() == "" || $("#PSGroup").val() == null) {
                $('#spanpsgcode').text('Please select PS Group');

            }
            else {
                $("#spanpsgcode").text('');
            }
        }

        //PersonalSubArea Change
        ChangePersonalSubArea = function () {
            if ($("#PersonalSubArea").val() == "" || $("#PersonalSubArea").val() == null) {
                $('#spanpsacode').text('Please select Personal Sub Area');

            }
            else {
                $("#spanpsacode").text('');
            }
        }

        //OrganizationalUnit Change
        ChangeOrganizationalUnit = function () {
            if ($("#OrganizationalUnit").val() == "" || $("#OrganizationalUnit").val() == null) {
                $('#spanpoucode').text('Please select Organizational Unit');

            }
            else {
                $("#spanpoucode").text('');
            }
        }

        Validation = function () {
            var NoOfErrors = 0;
            //DefaultValidation();

            $('#spanvdeptcode1').text('');
            $('#spanvdesgcode').text('');
            $('#spanvbucode').text('');
            $('#spanvcccode').text('');
            $('#spanpacode').text('');
            $('#spanpsgcode').text('');
            $('#spanpsacode').text('');
            $('#spanpoucode').text('');

            if ($("#Department").val() == "" || $("#Department").val() == null) {
                $('#spanvdeptcode1').text('Please select Department');
                NoOfErrors++;
            }

            if ($("#Designation").val() == "" || $("#Designation").val() == null) {
                $('#spanvdesgcode').text('Please select Designation');
                NoOfErrors++;
            }
            if ($("#BusinessUnit").val() == "" || $("#BusinessUnit").val() == null) {
                $('#spanvbucode').text('Please select Business Unit');
                NoOfErrors++;
            }
            if ($("#CostCenter").val() == "" || $("#CostCenter").val() == null) {
                $('#spanvcccode').text('Please select Cost Center');
                NoOfErrors++;
            }
            if ($("#PayrollArea").val() == "" || $("#PayrollArea").val() == null) {
                $('#spanpacode').text('Please select Payroll Area');
                NoOfErrors++;
            }
            if ($("#PSGroup").val() == "" || $("#PSGroup").val() == null) {
                $('#spanpsgcode').text('Please select PS Group');
                NoOfErrors++;
            }
            if ($("#PersonalSubArea").val() == "" || $("#PersonalSubArea").val() == null) {
                $('#spanpsacode').text('Please select Personal Sub Area');
                NoOfErrors++;
            }
            if ($("#OrganizationalUnit").val() == "" || $("#OrganizationalUnit").val() == null) {
                $('#spanpoucode').text('Please select Organizational Unit');
                NoOfErrors++;
            }
            return NoOfErrors;
        }

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        //Calculating Age based on DOB
        CalcAge = function (data, event) {

            var today = new Date();
            var dd = Number(today.getDate());
            var mm = Number(today.getMonth() + 1);
            var yyyy = Number(today.getFullYear());

            var dobDate = this.value();
            var myBDY = Number(dobDate.getFullYear());
            var myBDM = Number(dobDate.getMonth() + 1);
            var myBDD = Number(dobDate.getDate());
            var age = yyyy - myBDY;

            if (mm < myBDM) {
                age = age - 1;
            }
            else if (mm == myBDM && dd < myBDD) {
                age = age - 1
            };
            self.employeeModel().Age(age);

            var year = myBDY + 18;
            // this.max(new Date(year, 11, 31));
            var joindatepicker = $("#JoiningDate").data("kendoDatePicker");


            joindatepicker.min(new Date(year, mm - 1, dd + 1));
            joindatepicker.value('');
            //var joindatepicker = $("#JoiningDate").data("kendoDatePicker");


            //joindatepicker.min(dobDate);
            //joindatepicker.value('');
            $('#YearsofService').val('');
        }

        //Calculating Years of Service based on Date Of Joining
        CalcYOS = function (data, event) {
            if (this.value()) {
                var today = new Date();
                var dd = Number(today.getDate());
                var mm = Number(today.getMonth() + 1);
                var yyyy = Number(today.getFullYear());
                var joinDatev = this.value();
                var year = Number(joinDatev.getFullYear());
                var month = Number(joinDatev.getMonth() + 1);
                var date = Number(joinDatev.getDate());
                var yos = yyyy - year;

                if (mm < month) {
                    yos = yos - 1;
                }
                else if (mm == month && dd < date) {
                    yos = yos - 1
                };
                self.employeeModel().YearsofService(yos);
            }
            else {
                this.value('');
                $('#YearsofService').val('');
            }
        };

        // Display Maximum date in DateTime picker as today date
        calmaxtoday18 = function () {
            //var tenYears = new Date();
            //this.max(new tenYears().setTime(tenYears.valueOf() - 18 * 365 * 24 * 60 * 60 * 1000));
            var today = new Date();
            var dd = Number(today.getDate());
            var mm = Number(today.getMonth() + 1);
            var yyyy = Number(today.getFullYear());

            var year = new Date().getYear() - 19;

            this.max(new Date(yyyy - 19, mm - 1, dd - 1));
        };

        calmaxtoday = function () {
            this.max(new Date());

        };
        // Verify SAP Number
        ValidEvent = function (data, event) {
            var databaseList = ko.toJSON(self.employeeList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;

            $.each(jsonObj, function (index, value) {
                if (value.SAPNumber.toLowerCase() == formList.SAPNumber.toLowerCase()) {
                    $('#spanvsap').text('SAP Number already exists!');
                    flag = false;
                }
                return;
            });

            if (flag == true) {
                $('#spanvsap').text('');
            }
        }

        // Verify ID  Number
        ValidIDNO = function (data, event) {

            var databaseList = ko.toJSON(self.employeeList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;

            $.each(jsonObj, function (index, value) {
                if (value.IDNo.toLowerCase() == formList.IDNo.toLowerCase()) {
                    $('#spanvidno').text('ID No. already exists!');
                    flag = false;
                }

                //if (formList.SAPNumber.length >= 11) {
                //    $('#spanvsap').text('SAP Number length should be less than or equal to 10');
                //    flag = false;
                //}
                return;
            });

            if (flag == true) {
                $('#spanvidno').text('');
            }
        }

        self.Initialize();
    }
    IPMSRoot.EmployeeViewModel = EmployeeViewModel;

}(window.IPMSROOT));





