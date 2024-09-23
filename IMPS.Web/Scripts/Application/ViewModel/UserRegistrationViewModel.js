
(function (IPMSROOT) {

    var UserRegistrationViewModel = function () {

        var self = this;

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSROOT.validationHelper();
        self.userRegistrationModel = ko.observable();
        self.viewMode = ko.observable();
        self.usersList = ko.observableArray();
        self.allUsersLists = ko.observableArray();
        self.portCodeList = ko.observableArray();
        self.TypeUser = ko.observable("");
        //self.TypeID = ko.observable();
        self.IsEnabled = ko.observable(false);
        self.IsPortEnabled = ko.observable(false);
        self.ContactNo = ko.observable();
        self.isUserNameEnable = ko.observable(true);
        self.CaptachText = ko.observable();
        self.currentUserType = ko.observable("");
        self.PortCode = ko.observableArray();
        self.UserTypeList = [
            { "name": "Employee", "selected": ko.observable(true), TypeUser: "EMP" },
            { "name": "Agent", "selected": ko.observable(false), TypeUser: "AGNT" },
            { "name": "Terminal Operator", "selected": ko.observable(false), TypeUser: "TO" }
        ];

        //UserRegistration Initialization(pageload) mode
        self.Initialize = function () {
            self.LoadPortCodes();
            self.LoadAllUsersList();
            self.userRegistrationModel(new IPMSROOT.UserRegistrationModel);
            self.viewMode('Form');
            var captachText = randString(6);
            self.CaptachText(captachText);
        }
        //Anusha
        self.LoadPortCodes = function () {

            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null,
                function (result) {
                    ko.mapping.fromJS(result, {}, self.portCodeList);
                });
        }
        //LoadAllUsersList fetches the GetUsers data from API Service
        self.LoadAllUsersList = function () {
            self.viewModelHelper.apiGet('api/Users',
                null,
                function (result) {
                    self.allUsersLists(ko.utils.arrayMap(result, function (item) {
                        return new IPMSROOT.UserRegistrationModel(item);
                    }));
                });
        }

        /// Check Phone Number Validation
        CheckPhoneValidation = function (PhoneNumber) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            PhoneNumber = PhoneNumber.replace(/(\)|\()|_|-+/g, '');

            var validPhoneNumber = 0;
            if (PhoneNumber.length != 13) {
                toastr.warning("Invalid contact number.");
                validPhoneNumber++;
                return validPhoneNumber;
            }
            else {
                return validPhoneNumber;
            }
        }

        //Add User data saving data in API Service 
        self.SaveUser = function (model) {

            $("#spanPortCode").text('');
            $('#spanPortCode').css('display', '');
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            model.validationEnabled(true);
            self.UserValidation = ko.observable(model);
            self.UserValidation().errors = ko.validation.group(self.UserValidation);
            var errors = self.UserValidation().errors().length;
            var UserTypeID = 0;
            var UserName = model.UserName();
            var val = 0;
            self.viewModelHelper.apiGet('api/CheckUserExists/' + UserTypeID + '/' + UserName,
            { UserTypeID: UserTypeID, UserName: UserName },
             function (result) {
                 if (result > 0) {
                     val = 1;
                 }
             }, null, null, false);

            if (val > 0) {
                toastr.warning("Already exists ! Select other User Name", "User");
                return false;
            }
            if ($("#txtCaptachCode").val() != "") {

                if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                    $("#spanCaptachCode").text('');
                }
                else {
                    $("#spanCaptachCode").text('* invalid captcha code');
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                    errors = errors + 1;
                }
            }
            else {
                $("#spanCaptachCode").text('* Enter Above Code is required');
                var captachText = randString(6);
                self.CaptachText(captachText);
                errors = errors + 1;
            }

            var filterContanctNumber = model.ContactNo();
            if (filterContanctNumber != null || filterContanctNumber != '' || filterContanctNumber != undefined) {
                var validPhoneNumber = 0;

                if (filterContanctNumber.length != 0) {
                    validPhoneNumber = CheckPhoneValidation(filterContanctNumber);
                    if (validPhoneNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }
            var currentUserType = self.currentUserType;
            model.UserType = self.currentUserType;
            if (count == 0) {
                $("#spanPortCode").text('Please select port name');
                $("#spanPortCode").css('display', '');
                toastr.warning('Please select atleast one port name');
            }

            if (model.ValidFromDate() == '') {
                errors = errors + 1;
                $('#spanValidFromDate').text('* This field is required');
                $('#spanValidFromDate').css('display', '');
            } else {
                model.ValidFromDate(moment(model.ValidFromDate()).format('YYYY-MM-DD'));
                $('#spanValidFromDate').text('');
                $('#spanValidFromDate').css('display', 'none');

            }

            if (model.ValidToDate() == '') {
                errors = errors + 1;
                $('#spanValidToDate').text('* This field is required');
                $('#spanValidToDate').css('display', '');
            } else {
                model.ValidToDate(moment(model.ValidToDate()).format('YYYY-MM-DD'));
                $('#spanValidToDate').text('');
                $('#spanValidToDate').css('display', 'none');
            }

            if (errors == 0) {
                var count = model.PortList().length;
                if (count == 0) {
                    $("#spanPortCode").text('Please select port name');
                    $("#spanPortCode").css('display', '');

                }
                else {
                    var flag = false;
                    if (model.UserType == "EMP") {
                        flag = true;
                    }
                    else if (model.UserType == "AGNT") {
                        flag = true;
                    }
                    else if (model.UserType == "TO") {
                        flag = true;
                    }
                    else {

                        if (count > 2) {
                            $("#spanPortCode").text('Please select one port name');
                            $('#spanPortCode').css('display', '');
                            flag = false;
                        }
                        else {
                            flag = true;
                        }
                    }
                    if (flag) {
                        {
                            var userPorts = [];
                            $.each(self.userRegistrationModel().PortList(), function (index, value) {
                                userPorts.push(new UserPort(0, value, 'A', 0));
                                model.UserPorts(userPorts);
                            });
                            filterContanctNumber = filterContanctNumber.replace(/(\)|\()|_|-+/g, '');
                            model.ContactNo(filterContanctNumber);
                            model.TypeUser = currentUserType;
                            self.viewModelHelper.apiPost('api/UserRegistration', ko.mapping.toJSON(model),
                                function Message(data) {
                                    toastr.success("User details saved successfully.", "User Registration");
                                    //selfUser.LoadUserList();
                                    self.viewMode('Form');
                                    $('#UserMasterTitile').html("User");
                                    self.userRegistrationModel(new IPMSROOT.UserRegistrationModel());
                                    self.LoadAllUsersList();
                                    window.location = '/Account/Login';
                                });
                            //}
                        }
                        //else {
                        //    self.UserValidation().errors.showAllMessages();
                        //    toastr.warning("User details fails to save");
                        //    return;
                        //}
                    }
                }
            }
            else {
                self.UserValidation().errors.showAllMessages();
                toastr.warning("You have some errors. Please check below");
                return;
            }
        }

        //This method is fires when cancel button is pressed redirected to home page
        self.cancel = function () {
            window.location = '/Account/Login';
        }
        ChangePortCode = function () {

            self.userRegistrationModel().ReferenceNo("");
            self.userRegistrationModel().Designation("");
            self.userRegistrationModel().FirstName("");
            self.userRegistrationModel().LastName("");
            self.userRegistrationModel().UserName("");
            self.userRegistrationModel().EmailID("");
            self.userRegistrationModel().ContactNo("");
            self.userRegistrationModel().UserTypeID("");
            self.userRegistrationModel().UserType("");

            $('#spanvsap').text('');
            $('#spanvsap').css('display', '');

        }
        //LoadUserType fetches the User details from API Service


        //Anusha
        LoadUserType = function (data, event) {
            self.IsEnabled(true);
            self.IsPortEnabled(true);
            self.usersList();
            self.currentUserType = data.TypeUser;
            if (data.TypeUser == "EMP") {
                TypeUser = JSON.parse(ko.toJSON(ko.observable("EMP")));
                $("#SAPTitle").text("SAP Number:");
                $("#spnDesignation").text("Designation:");
                $("#Title").text("Employee User");
            }
            else if (data.TypeUser == "AGNT") {
                TypeUser = JSON.parse(ko.toJSON(ko.observable("AGNT")));
                //self.viewModelHelper.apiGet('api/Agent/GetAllAgents',
                //null,
                //function (result) {
                //    ko.mapping.fromJS(result, {}, self.usersList);

                //    self.userRegistrationModel().ReferenceNo("");
                //    self.userRegistrationModel().FirstName("");
                //    self.userRegistrationModel().LastName("");
                //    self.userRegistrationModel().UserName("");
                //    self.userRegistrationModel().EmailID("");
                //    self.userRegistrationModel().ContactNo("");
                //    self.userRegistrationModel().Designation("");
                //    self.userRegistrationModel().UserTypeID("");
                //    self.userRegistrationModel().PortList("");

                $("#SAPTitle").html("Reference Number:");
                $("#spnDesignation").text("Agent Name:");
                $("#Title").text("Agent User ");
            }
            else if (data.TypeUser == "TO") {
                TypeUser = JSON.parse(ko.toJSON(ko.observable("TO")));
                //self.viewModelHelper.apiGet('api/TerminalOperatorsDetails',
                //    null,
                //    function (result) {
                //        ko.mapping.fromJS(result, {}, self.usersList);
                //        self.userRegistrationModel().ReferenceNo("");
                //        self.userRegistrationModel().FirstName("");
                //        self.userRegistrationModel().LastName("");
                //        self.userRegistrationModel().UserName("");
                //        self.userRegistrationModel().EmailID("");
                //        self.userRegistrationModel().ContactNo("");
                //        self.userRegistrationModel().Designation("");
                //        self.userRegistrationModel().UserTypeID("");
                //        self.userRegistrationModel().PortList("");
                //    });
                $("#SAPTitle").html("Registration Number:");
                $("#spnDesignation").text("Terminal Operator Name:");
                $("#Title").text("Terminal Operator User ");
            }

            //self.userRegistrationModel().ReferenceNo("");
            //self.userRegistrationModel().FirstName("");
            //self.userRegistrationModel().LastName("");
            //self.userRegistrationModel().UserName("");
            //self.userRegistrationModel().EmailID("");
            //self.userRegistrationModel().ContactNo("");
            //self.userRegistrationModel().Designation("");
            //self.userRegistrationModel().UserTypeID("");
            //self.userRegistrationModel().PortList("");
            //$('#spanPortCode').text('');
            //$('#spanPortCode').css('display', '');
            ////Masking Of Personal MobileNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNomaskedtextbox = $("#ContactNo").data("kendoMaskedTextBox");
            self.ContactNo = ContactNomaskedtextbox.value();
            var captachText = randString(6);
            self.CaptachText(captachText);
        }

        // Verify the User Name for unique values
        ValidEvent = function (data, event) {
            var databaseList = ko.toJSON(self.allUsersLists);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;

            $.each(jsonObj, function (index, value) {
                if (value.UserName.toLowerCase() == formList.UserName.toLowerCase()) {
                    $('#spanusername').text('User Name already exists!');
                    data.UserName('');
                    flag = false;
                }

                return;
            });

            if (flag == true) {
                $('#spanusername').text('');
            }
        }

        RefreshCaptach = function () {
            var captachText = randString(6);
            self.CaptachText(captachText);
        }

        function randString(x) {
            var s = "";
            while (s.length < x && x > 0) {
                var r = Math.random();
                s += (r < 0.1 ? Math.floor(r * 100) : String.fromCharCode(Math.floor(r * 26) + (r > 0.5 ? 97 : 65)));
            }
            return s;
        }

        ChangeCaptachCode = function () {
            if ($("#txtCaptachCode").val() != "") {

                if ($("#txtCaptachCode").val() == $("#lblCaptach").text()) {
                    $("#spanCaptachCode").text('');
                }
                else {
                    $("#spanCaptachCode").text('* invalid captcha code');
                    var captachText = randString(6);
                    self.CaptachText(captachText);
                }
            }
            else {
                $("#spanCaptachCode").text('* Enter Above Code is required');
                var captachText = randString(6);
                self.CaptachText(captachText);
            }
        }

        ValidDate = function () {
            self.userRegistrationModel().ValidToDate(self.userRegistrationModel().ValidFromDate());
        }

        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }









        //Anusha new
        ValidateNumber = function (data, event) {
            $("#spanPortCode").text('');
            $("#spanPortCode").css('display', '');
            $('#spanvsap').text('');
            $('#spanvsap').css('display', '');
            var port = JSON.parse(ko.toJSON(data.PortList));
            var flag = true;
            var chkFlag = true;
            var returnValue = "I";
            self.isUserNameEnable(true);
            var PortCode = data.PortList();
            //LoadUserType(data, event);
            //self.currentUserType = data.TypeUser;


            if (PortCode.length == 0) {
                $("#spanPortCode").text('Please select port name');
                $("#spanPortCode").css('display', '');
                $("#SAPNumber").val("");
            }
            else {
                if (TypeUser == "EMP") {
                    if (data.ReferenceNo != data.ReferenceNo) {
                        $('#spanvsap').text('Already SAP Number exists, please select other SAP Number');
                        $('#spanvsap').css('display', '');
                        chkFlag = false;
                    }
                }
                else if (TypeUser == "AGNT") {
                    if (data.ReferenceNo = data.ReferenceNo) {
                        chkFlag = true;
                    }
                    else {
                        $("#spanPortCode").text('Please select one port name');
                        $("#spanPortCode").css('display', '');
                        chkFlag = false;
                    }
                }
                else if (TypeUser == "TO") {
                    if (data.ReferenceNo = data.ReferenceNo) {
                        chkFlag = true;
                    }
                    else {
                        $("#spanPortCode").text('Please select port one code');
                        $("#spanPortCode").css('display', '');
                        chkFlag = false;
                    }
                }
                if (chkFlag) {
                    if (TypeUser == "EMP") {
                        self.viewModelHelper.apiGet('api/GetEmployeesListFetching/' + PortCode + '/' + data.ReferenceNo(),
                           null, function (result) {

                               var jsonObj = result;
                               if (jsonObj.length > 0) {
                                   $.each(jsonObj, function (index, value) {
                                       self.userRegistrationModel().FirstName(value.FirstName);
                                       self.userRegistrationModel().LastName(value.LastName);
                                       self.userRegistrationModel().EmailID(value.EmailID);
                                       self.userRegistrationModel().ContactNo(value.ContactNo);
                                       self.userRegistrationModel().Designation(value.DesignationName);
                                       self.userRegistrationModel().UserTypeID(value.UserTypeID);
                                       self.userRegistrationModel().UserType(TypeUser);
                                       self.userRegistrationModel().UserName(value.SAPNumber);
                                   })
                               }
                               else {
                                   $('#spanvsap').text('Invalid Reference Number');
                                   self.userRegistrationModel().Designation("");
                                   self.userRegistrationModel().FirstName("");
                                   self.userRegistrationModel().LastName("");
                                   self.userRegistrationModel().UserName("");
                                   self.userRegistrationModel().EmailID("");
                                   self.userRegistrationModel().ContactNo("");
                                   self.userRegistrationModel().UserTypeID("");
                               }
                               return;
                           })
                    }
                    else if (TypeUser == "AGNT") {
                        self.viewModelHelper.apiGet('api/GetAgentListDetailsFetch/' + PortCode + '/' + data.ReferenceNo(),
                           null, function (result) {
                               var jsonObj = result;
                               if (jsonObj.length > 0) {
                                   $.each(jsonObj, function (index, value) {
                                       self.userRegistrationModel().Designation(value.RegisteredName);
                                   })
                               }
                               else {

                                   $('#spanvsap').text('Invalid Registration Number');
                                   self.userRegistrationModel().Designation("");
                               }
                               return;
                           })
                    }
                    if (TypeUser == "TO") {
                        self.viewModelHelper.apiGet('api/GetTerminalOperatorListDetailsFetch/' + PortCode + '/' + data.ReferenceNo(),
                           null, function (result) {
                               var jsonObj = result;
                               if (jsonObj.length > 0) {
                                   $.each(jsonObj, function (index, value) {
                                       self.userRegistrationModel().FirstName(value.FirstName);
                                       self.userRegistrationModel().LastName(value.LastName);
                                       self.userRegistrationModel().EmailID(value.EmailID);
                                       self.userRegistrationModel().ContactNo(value.ContactNo);
                                       self.userRegistrationModel().Designation(value.DesignationName);
                                       self.userRegistrationModel().UserTypeID(value.UserTypeID);
                                       self.userRegistrationModel().UserType(TypeUser);
                                       self.userRegistrationModel().Designation(value.RegisteredName);
                                       self.userRegistrationModel().UserName(value.SAPNumber);
                                   });
                               }
                               else {

                                   $('#spanvsap').text('Invalid Registration Number');
                                   self.userRegistrationModel().Designation("");
                               }
                               return;
                           });
                    }
                }
            }
        }
        self.Initialize();
    }
    IPMSROOT.UserRegistrationViewModel = UserRegistrationViewModel;

}(window.IPMSROOT));

function UserPort(UserID, PortCode, RecordStatus, CreatedBy) {
    this.UserID = UserID;
    this.PortCode = PortCode;
    this.RecordStatus = "A";
    this.CreatedBy = CreatedBy;
}