
(function (IPMSRoot) {

    var UserViewModel = function (UserID, viewDetail) {

        var selfUser = this;
        $('#UserMasterTitile').html("User");
        selfUser.viewModelHelper = new IPMSROOT.viewModelHelper();
        selfUser.validationHelper = new IPMSROOT.validationHelper();
        selfUser.viewMode = ko.observable();
        selfUser.UserList = ko.observableArray();
        selfUser.userModel = ko.observable(new IPMSROOT.UserModel());
        selfUser.editableView = ko.observable(true);
        selfUser.editActive = ko.observable(true);
        selfUser.userTypeValues = ko.observableArray();
        selfUser.employeeValues = ko.observableArray();
        selfUser.isUserTypeChanged = ko.observable(true);
        selfUser.isUserChanged = ko.observable(true);
        selfUser.isReferenceNoEnable = ko.observable(true);
        selfUser.isUserTypeEnable = ko.observable(true);
        selfUser.isNameEnable = ko.observable(true);
        selfUser.Roles = ko.observableArray();
        selfUser.IsUpdate = ko.observable(false);
        selfUser.IsSave = ko.observable(false);
        selfUser.IsReset = ko.observable(true);
        selfUser.isRoleEnabled = ko.observable(true);
        selfUser.isPortEnabled = ko.observable(true);
        selfUser.SelectedRoles = ko.observableArray([]);
        selfUser.IsUserNameEnabled = ko.observable(true);
        selfUser.ContactNo = ko.observable();
        selfUser.IsFirstNameEnabled = ko.observable(true);
        selfUser.IsSurNameEnabled = ko.observable(true);
        selfUser.IsEmailAddEnabled = ko.observable(true);
        selfUser.IsContactNumberEnabled = ko.observable(true);
        selfUser.IsReasonForAccessEnabled = ko.observable(true);
        selfUser.IsValidDtFromEnabled = ko.observable(true);
        selfUser.IsValidDtToEnabled = ko.observable(true);
        selfUser.IsUserPort = ko.observable(false);
        selfUser.isDormantUserEnabled = ko.observable(false);
        selfUser.isDormantUser = ko.observable(false);
        selfUser.isDormantUserVisible = ko.observable(false);
        selfUser.UserPorts = ko.observableArray();
        selfUser.isAgentOrTerminal = false;
        //User Initialization(pageload) mode
        selfUser.Initialize = function () {
            selfUser.viewMode = ko.observable(true);
            selfUser.LoadUserTypes();
            selfUser.LoadRoles();
            selfUser.LoadPortData();
            selfUser.LoadUserList();

            if (viewDetail == true) {
                $('#UserMasterTitile').html("View User");

                selfUser.viewMode('Form');
            }
            else {
                selfUser.viewMode('List');
            }
        }

        //LoadUsers fetches the data from API Service
        selfUser.LoadUserList = function (model) {
            selfUser.SetAdvSearchValues();
            $("#divAutoEmployees").hide();
            $("#divAutoAgent").hide();
            $("#divAutoTo").hide();
            $("#divExternalUser").hide();
            $("#divDefault").show();
            if (viewDetail == true) {
                selfUser.IsUpdate(false);
                selfUser.IsSave(false);
                selfUser.IsReset(false);
                selfUser.IsReset(false);
                selfUser.viewModelHelper.apiGet('api/User/GetUserDetailsByIDView', { id: UserID },
                  function (result) {
                      selfUser.UserList(new IPMSRoot.UserModel(result));
                      selfUser.viewuser(selfUser.UserList());
                  }, null, null, true);
            }
            else {
                var type = null;
                var search = null;
                var referenceNo = null;
                var isDarmentUser = 'N';
                if (model != undefined) {
                    type = model.usertypeSearch() != undefined ? model.usertypeSearch() : '';
                    search = model.userNameSearch() != undefined ? model.userNameSearch() : '';
                    referenceNo = model.ReferenceNoSearch() != undefined ? model.ReferenceNoSearch() : '';
                } else {
                    type = 'AGNT';
                }
                isDarmentUser = $('#chkDormantUsers').is(':checked') == true ? 'Y' : 'N';

                var grid = $("#grdUserList").data("kendoGrid");
                if (grid != undefined)
                    grid.dataSource.filter({});

                selfUser.viewModelHelper.apiGet('api/User/GetUsersListForGrid', { userType: type, SearchText: search, DarmentUser: isDarmentUser, ReferenceNo: referenceNo }, function (result) {
                    selfUser.UserList(ko.utils.arrayMap(result, function (item) {

                        selfUser.SetAdvSearchValues()
                        return new IPMSRoot.UserModel(item);

                    }));
                });
            }
        }

        selfUser.SetAdvSearchValues = function () {

            var usertype = $('select#UsertypeID option:selected').val();
            if (usertype == 'EMP') {
                $("#grdUserList thead [data-field=ReferenceNoSort] .k-link").html("SAP No.");
                $("#grdUserList thead [data-field=ReferenceNoSort]").show();
                //$("#tdReferenceNo").show();
                $("#grdUserList div table .tdReferenceNo").show();
            }
            else if (usertype == 'AGNT') {
                $("#grdUserList thead [data-field=ReferenceNoSort] .k-link").html("Reference No.");
                $("#grdUserList thead [data-field=ReferenceNoSort]").show();
                //$("#tdReferenceNo").show();
                $("#grdUserList div table .tdReferenceNo").show();
            }
            else if (usertype == 'TO') {
                $("#grdUserList thead [data-field=ReferenceNoSort] .k-link").html("Registration No.");
                $("#grdUserList thead [data-field=ReferenceNoSort]").show();
                //$("#tdReferenceNo").show();
                $("#grdUserList div table .tdReferenceNo").show();
            }
            else {
                $("#grdUserList thead [data-field=ReferenceNoSort] .k-link").html("");
                //$("#grdUserList thead [data-field=ReferenceNoSort]").hide();
                ////$("#tdReferenceNo").hide();
                //$("#grdUserList div table .tdReferenceNo").css("display", "none");

            }
        }

        selfUser.ResetData = function () {
            $("#UserNameID").val("");
            $("select#UsertypeID").prop('selectedIndex', 0);
            $('#chkDormantUsers').attr('checked', false);
            $("#UsertypeID option[value='AGNT']").attr("selected", "selected");
            $("#refno").val("");
            $('#lblReferenceNo').html('Reference No.');
            $('#divSAPNO').show();
            selfUser.LoadUserList();
        }

        //OrganizationalUnit Change
        Changename = function () {
            if ($("#name").val() == "" || $("#name").val() == null) {
                $('#spanUserTypeID').text('* Name is required');
            }
            else {
                $("#spanUserTypeID").text('');
            }
        }


        //PersonalSubArea Change
        ChangePersonalSubArea = function () {
            if ($("#utype").val() == "" || $("#utype").val() == null) {
                $('#spanUsertype').text('* This field is required');
            }
            else {
                $("#spanUsertype").text('');
            }
        }


        Validation = function () {
            var NoOfErrors = 0;
            $('#spanUsertype').text('');
            if ($("#utype").val() == "" || $("#utype").val() == null) {
                $('#spanUsertype').text('* This field is required');
                NoOfErrors++;
            }
            if ($('#utype').val() == 'EMP') {
                $('#spanEmployees').text('* Name is required');
                $('#spanEmployees').css('display', '');
                NoOfErrors++;
            } else if ($('#utype').val() == 'AGNT') {
                $('#spanAgent').text('* Name is required');
                $('#spanAgent').css('display', '');
                NoOfErrors++;
            } else {
                $('#spanTO').text('* Name is required');
                $('#spanTO').css('display', '');
                NoOfErrors++;
            }

            $('#spanUserTypeID').text('');
            return NoOfErrors;
        }

        PreventBackSpace = function () {
            return selfUser.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        //LoadUserTypes fetches the User Type details from API Service
        selfUser.LoadUserTypes = function () {
            selfUser.viewModelHelper.apiGet('api/UserTypes',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, selfUser.userTypeValues);
              });
        }

        //Roles fetches the data from API Service to MultiSelector
        selfUser.LoadRoles = function () {

            selfUser.viewModelHelper.apiGet('api/Roles',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, selfUser.Roles);

              }, null, null, false);
        }

        selfUser.LoadPortData = function () {
            selfUser.viewModelHelper.apiGet('api/GetAllPortsDetails',
             null,
              function (result) {
                  ko.mapping.fromJS(result, {}, selfUser.UserPorts);
              })
        }

        //LoadUsers fetches the User details from API Service
        selfUser.LoadUsers = function (event) {

            selfUser.ClearEmployeeDetails();
            selfUser.isNameEnable(true);
            selfUser.isRoleEnabled(true);
            selfUser.isPortEnabled(true);
            selfUser.userModel().UserTypeID("");

            $("#divDefault").hide();
            $('#spanUserTypeID').text('');

            if ($("#utype").val() == "" || $("#utype").val() == null) {
                $('#spanUsertype').text('* This field is required');
                selfUser.userModel().FirstName("");
                selfUser.userModel().ReferenceNo("");
                selfUser.userModel().Designation("");
                selfUser.userModel().LastName("");
                selfUser.userModel().ContactNo("");
                selfUser.userModel().EmailID("");
                selfUser.userModel().UserName("");
                selfUser.userModel().UserTypeID("");
                $('#NoTitile').html("No.:");
                selfUser.userModel().viewRole("");
                selfUser.userModel().viewPorts("");
            }
            else {
                $("#spanUsertype").text('');

            }
            selfUser.employeeValues.removeAll();

            if (event.SubCatCode() == "AGNT") {
                selfUser.userModel().Name = '';
                $('#divReferenceNo').show();
                $('#divDesignation').show();
                $("#divAutoEmployees").hide();
                $("#divAutoAgent").show();
                $("#divAutoTo").hide();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
                selfUser.LoadRoles();
                selfUser.LoadPortData();
                $("#AutoAgent").kendoAutoComplete({
                    dataTextField: 'Name',
                    minLength: 1, pageSize: 1,
                    dataSource:
                        new kendo.data.DataSource({
                            serverFiltering: true,
                            transport: {
                                read: {
                                    url: "/api/AgentsDetails", dataType: 'json'
                                }
                            }, schema: {
                                data: function (data) { ko.mapping.fromJS(data, {}, selfUser.employeeValues); return data; }, total: function (data) {
                                    return data.length;
                                }
                            }
                        }),

                    value: selfUser.userModel().Name,
                    select: BindData,
                    placeholder: "Select Agent...",
                });
                $('#NoTitile').html("Reference Number:");

                var obj = JSON.parse(ko.toJSON(selfUser.Roles));
                $.each(obj, function (key, value) {
                    if (value.RoleCode == "AGNT") {
                        selfUser.userModel().viewRole([value.RoleID]);
                    }
                });

                if (selfUser.userModel().viewRole().length == 0) {
                    $('#spanRole').text('* This field is required');
                    $('#spanRole').css('display', '');
                } else {
                    $('#spanRole').text('* This field is required');
                    $('#spanRole').css('display', 'none');
                }

                var ports = JSON.parse(ko.toJSON(selfUser.UserPorts));
                $.each(ports, function (key, value) {
                    selfUser.userModel().viewPorts([value.PortCode]);
                });
                if (selfUser.userModel().viewPorts().length == 0) {
                    $('#spanMultiports').text('* This field is required');
                    $('#spanMultiports').css('display', '');
                } else {
                    $('#spanMultiports').text('* This field is required');
                    $('#spanMultiports').css('display', 'none');
                }
                selfUser.isRoleEnabled(false);
                selfUser.isUserChanged(false);
                $("#UserName").kendoMaskedTextBox({ mask: "aaaaaaaaa" });
                var UserNameMaskedtextbox = $("#UserName").data("kendoMaskedTextBox");
                selfUser.UserName = UserNameMaskedtextbox.value();
            }
            if (event.SubCatCode() == "EMP") {

                selfUser.userModel().Name = '';
                $('#divReferenceNo').show();
                $('#divDesignation').show();

                $("#divAutoEmployees").show();
                $("#divAutoAgent").hide();
                $("#divAutoTo").hide();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
                selfUser.LoadRoles();
                selfUser.LoadPortData();

                $("#AutoEmployees").kendoAutoComplete({
                    dataTextField: 'Name',
                    minLength: 1, pageSize: 1,
                    dataSource:
                        new kendo.data.DataSource({
                            serverFiltering: true,
                            transport: {
                                read: {
                                    url: "/api/EmployeesDetails", dataType: 'json'
                                }
                            }, schema: {
                                data: function (data) { ko.mapping.fromJS(data, {}, selfUser.employeeValues); return data; }, total: function (data) {
                                    return data.length;
                                }
                            }
                        }),

                    value: selfUser.userModel().Name,
                    select: BindData,
                    placeholder: "Select Employee...",
                });
                var obj = JSON.parse(ko.toJSON(selfUser.Roles));

                if (selfUser.userModel().viewRole().length > 0)
                    selfUser.userModel().viewRole.removeAll();

                $.each(obj, function (key, value) {
                    if (value.RoleCode == "EMP") {
                        selfUser.userModel().viewRole([value.RoleID]);
                    }
                });
                var ports = JSON.parse(ko.toJSON(selfUser.UserPorts));
                if (selfUser.userModel().viewPorts().length > 0)
                    selfUser.userModel().viewPorts.removeAll();
                $.each(ports, function (key, value) {
                    selfUser.userModel().viewPorts([value.PortCode]);
                });
                
                $('#NoTitile').html("SAP No.:");
                selfUser.isUserChanged(true);
                $("#UserName").kendoMaskedTextBox({ mask: "aaaaaaa" });
                var UserNameMaskedtextbox = $("#UserName").data("kendoMaskedTextBox");
                selfUser.UserName = UserNameMaskedtextbox.value();
                UserNameMaskedtextbox.destroy();
                $("#UserName").kendoMaskedTextBox({ mask: "aaaaaaa" });
                var UserNameMaskedtextbox = $("#UserName").data("kendoMaskedTextBox");
                selfUser.UserName = UserNameMaskedtextbox.value();
            }
            if (event.SubCatCode() == "TO") {

                selfUser.userModel().Name = '';
                $('#divReferenceNo').show();
                $('#divDesignation').show();
                $("#divAutoEmployees").hide();
                $("#divAutoAgent").hide();
                $("#divAutoTo").show();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
                selfUser.LoadRoles();
                selfUser.LoadPortData();
                $("#AutoTo").kendoAutoComplete({
                    dataTextField: 'Name',
                    minLength: 1, pageSize: 1,
                    dataSource:
                        new kendo.data.DataSource({
                            serverFiltering: true,
                            transport: {
                                read: {
                                    url: "/api/TerminalOperatorsDetails", dataType: 'json'
                                }
                            }, schema: {
                                data: function (data) { ko.mapping.fromJS(data, {}, selfUser.employeeValues); return data; }, total: function (data) {
                                    return data.length;
                                }
                            },

                        }),

                    value: selfUser.userModel().Name,
                    select: BindData,
                    placeholder: "Select Terminal Operator ...",
                });
                $('#NoTitile').html("Registration Number:");
                var obj = JSON.parse(ko.toJSON(selfUser.Roles));
                $.each(obj, function (key, value) {
                    if (value.RoleCode == "TO") {
                        selfUser.userModel().viewRole([value.RoleID]);
                    }
                });
                if (selfUser.userModel().viewRole().length == 0) {
                    $('#spanRole').text('* This field is required');
                    $('#spanRole').css('display', '');
                } else {
                    $('#spanRole').text('* This field is required');
                    $('#spanRole').css('display', 'none');
                }
                var ports = JSON.parse(ko.toJSON(selfUser.UserPorts));
                $.each(ports, function (key, value) {
                    selfUser.userModel().viewPorts([value.PortCode]);
                });
                if (selfUser.userModel().viewPorts().length == 0) {
                    $('#spanMultiports').text('* This field is required');
                    $('#spanMultiports').css('display', '');
                } else {
                    $('#spanMultiports').text('* This field is required');
                    $('#spanMultiports').css('display', 'none');
                }
                selfUser.isRoleEnabled(false);
                selfUser.isUserChanged(false);
                $("#UserName").kendoMaskedTextBox({ mask: "aaaaaaaaa" });
                var UserNameMaskedtextbox = $("#UserName").data("kendoMaskedTextBox");
                selfUser.UserName = UserNameMaskedtextbox.value();
            }

            if (event.SubCatCode() == "EXTU") {
                selfUser.userModel().Name = '';
                $('#divReferenceNo').hide();
                $('#divDesignation').hide();

                $("#divAutoEmployees").hide();
                $("#divAutoAgent").hide();
                $("#divAutoTo").hide();
                $("#divExternalUser").show();
                $("#divDefault").hide();
                selfUser.IsUserNameEnabled(true);
                selfUser.editActive(true);

                selfUser.editableView(true);

                if (selfUser.userModel().viewRole().length > 0) {
                    selfUser.userModel().viewRole.removeAll();
                    selfUser.userModel().viewRole([]);
                }
                if (selfUser.userModel().viewPorts().length > 0) {
                    selfUser.userModel().viewPorts.removeAll();
                    selfUser.userModel().viewPorts([]);
                }

                selfUser.Roles.removeAll();
                selfUser.LoadRoles();
                selfUser.LoadPortData();
                selfUser.Roles.remove(function (item) {
                    return item.RoleCode() != "SAPS" && item.RoleCode() != "SSA" && item.RoleCode() != "EREP"
                })


                var obj = JSON.parse(ko.toJSON(selfUser.Roles));
                $.each(obj, function (key, value) {
                    selfUser.userModel().viewRole([]);
                });
                var ports = JSON.parse(ko.toJSON(selfUser.UserPorts));
                $.each(ports, function (key, value) {
                    selfUser.userModel().viewPorts([]);
                });
                $("#UserName").kendoMaskedTextBox({ mask: "aaaaaaaaa" });
                var UserNameMaskedtextbox = $("#UserName").data("kendoMaskedTextBox");
                selfUser.UserName = UserNameMaskedtextbox.value();
                UserNameMaskedtextbox.destroy();
                $("#UserName").kendoMaskedTextBox({ mask: "aaaaaaaaa" });
                var UserNameMaskedtextbox = $("#UserName").data("kendoMaskedTextBox");
                selfUser.UserName = UserNameMaskedtextbox.value();

            }
            selfUser.isNameEnable(true);
            //Masking Of Personal MobileNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNomaskedtextbox = $("#ContactNo").data("kendoMaskedTextBox");
            selfUser.ContactNo = ContactNomaskedtextbox.value();
        }



        ////Bind user data in textbox for particular Agent || Employee || Terminal Operator        
        BindData = function (e) {


            $('#spanAgent').text('');
            $('#spanAgent').css('display', '');
            $('#spanEmployees').text('');
            $('#spanEmployees').css('display', '');
            $('#AutoTo').text('');
            $('#AutoTo').css('display', '');
            $('#spanExternalUser').text('');
            $('#spanExternalUser').css('display', '');

            var dataItem = this.dataItem(e.item.index());
            var items = ko.toJSON(selfUser.UserList);
            var jsonObj = JSON.parse(items);
            var some = dataItem;
            var items = ko.toJSON(selfUser.employeeValues);
            var jsonObj = JSON.parse(items);

            selfUser.userModel().FirstName("");
            selfUser.userModel().ReferenceNo("");
            selfUser.userModel().Designation("");
            selfUser.userModel().LastName("");
            selfUser.userModel().ContactNo("");
            selfUser.userModel().EmailID("");
            selfUser.userModel().UserName("");
            selfUser.userModel().ReasonForAccess("");
            selfUser.userModel().ValidFromDate("");
            selfUser.userModel().ValidToDate("");


            $.each(jsonObj, function (index, value) {

                if (parseInt(value.UserTypeID) == some.UserTypeID) {
                    selfUser.userModel().FirstName(value.FirstName);
                    selfUser.userModel().ReferenceNo(value.ReferenceNo);
                    if (selfUser.userModel().SubCatCode() == "AGNT") {
                        selfUser.userModel().Designation("");
                    }
                    else {
                        selfUser.userModel().Designation(value.Designation);
                    }

                    if (selfUser.userModel().SubCatCode() == "EMP") {
                        selfUser.userModel().UserName(value.ReferenceNo);
                    } else {
                        selfUser.userModel().UserName(value.UserName);
                    }

                    selfUser.userModel().LastName(value.LastName);
                    selfUser.userModel().ContactNo(value.ContactNo);
                    selfUser.userModel().EmailID(value.EmailID);
                    selfUser.userModel().UserTypeID(value.UserTypeID);
                    selfUser.userModel().ReasonForAccess(value.ReasonForAccess);
                    selfUser.userModel().ValidFromDate(value.ValidFromDate);
                    selfUser.userModel().ValidToDate(value.ValidToDate);


                    if (selfUser.userModel().UserName() == '') {
                        selfUser.IsUserNameEnabled(true);
                        selfUser.editableView(true);
                        selfUser.editActive(true);
                    }
                    else {
                        selfUser.IsUserNameEnabled(false);
                        selfUser.editableView(false);
                        selfUser.editActive(false);
                    }

                    if (selfUser.userModel().SubCatCode() == "AGNT" || selfUser.userModel().SubCatCode() == "TO") {
                        selfUser.userModel().FirstName("");
                        selfUser.userModel().Designation("");
                        selfUser.userModel().LastName("");
                        selfUser.userModel().ContactNo("");
                        selfUser.userModel().EmailID("");
                        selfUser.userModel().UserName("");
                        selfUser.userModel().ReasonForAccess("");
                        selfUser.userModel().ValidFromDate("");
                        selfUser.userModel().ValidToDate("");
                        selfUser.IsUserNameEnabled(true);

                    } else {
                        selfUser.IsUserNameEnabled(false);
                    }

                    return false;
                }
                else {

                    selfUser.userModel().FirstName("");
                    selfUser.userModel().ReferenceNo("");
                    selfUser.userModel().Designation("");
                    selfUser.userModel().LastName("");
                    selfUser.userModel().ContactNo("");
                    selfUser.userModel().EmailID("");
                    selfUser.userModel().UserName("");
                    selfUser.userModel().UserTypeID("");
                    selfUser.userModel().ReasonForAccess("");
                    selfUser.userModel().ValidFromDate("");
                    selfUser.userModel().ValidToDate("");
                }

            });


            if (selfUser.userModel().ReferenceNo() != '') {
                $('#spanReferenceNo').text('');
                $('#spanReferenceNo').css('display', 'none');
            }

            if ($('#FN').val() != '') {
                $('#userid').text('');
                $('#userid').css('display', 'none');
            }

            if ($("#utype").val() == "" || $("#utype").val() == null) {
                $('#spanUsertype').text('* This field is required');
                $('#spanUsertype').css('display', '');
            }
            else {
                $("#spanUsertype").text('');
                $('#spanUsertype').css('display', 'none');
            }

            if ($("#UserName").val() == '') {
                $('#spanusername').text('* This field is required');
                $('#spanusername').css('display', '');
            } else {
                $('#spanusername').text('');
                $('#spanusername').css('display', 'none');
            }

            //Masking Of Personal MobileNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNomaskedtextbox = $("#ContactNo").data("kendoMaskedTextBox");
            selfUser.ContactNo = ContactNomaskedtextbox.value();
        }


        //User in Add mode
        selfUser.addUser = function () {
            selfUser.userModel(new IPMSRoot.UserModel());
            $('#UserMasterTitile').html("Add User");
            $('#NoTitile').html("No.:");
            selfUser.viewMode('Form');
            selfUser.userModel().viewRole.removeAll();
            selfUser.userModel().viewPorts.removeAll();
            selfUser.isUserTypeEnable(true);
            selfUser.isNameEnable(false);
            selfUser.isReferenceNoEnable(false);
            selfUser.IsUserNameEnabled(true);
            selfUser.IsUpdate(false);
            selfUser.IsSave(true);
            selfUser.IsUserPort(false);
            selfUser.IsReset(true);
            selfUser.editableView(true);
            selfUser.editActive(true);
            selfUser.isDormantUserEnabled(false);
            selfUser.isDormantUser(false);
            selfUser.isDormantUserVisible(false);
            selfUser.isPortEnabled(true);

            //Masking Of Personal MobileNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            $("#divAutoEmployees").hide();
            $("#divAutoAgent").hide();
            $("#divAutoTo").hide();
            $("#divExternalUser").hide();
            $("#divDefault").show();
             var ContactNomaskedtextbox = $("#ContactNo").data("kendoMaskedTextBox");
             selfUser.ContactNo = ContactNomaskedtextbox.value();
             $("#UserName").kendoMaskedTextBox({ mask: "aaaaaaaaa" });
             var UserNameMaskedtextbox = $("#UserName").data("kendoMaskedTextBox");
             selfUser.UserName = UserNameMaskedtextbox.value();
            
            selfUser.LoadPortData();
        }
        //roleValidation
        selfUser.roleValidation = function () {
            $("#spanRole").text('');
            var count = model.viewRole().length;

            if (count == 0) {
                $("#spanRole").text('* Role is required');

            }
        }

        /// Check Phone Number Validation
        CheckPhoneValidation = function (PhoneNumber) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            PhoneNumber = PhoneNumber.replace(/(\)|\()|_|-+/g, '');

            var validPhoneNumber = 0;
            if (PhoneNumber.length != 13) {
                toastr.warning("Invalid Contact Number");
                validPhoneNumber++;

            }
            else if (PhoneNumber.length == 13) {
                var validNo = parseInt(PhoneNumber);
                if (validNo == 0) {
                    toastr.warning("Invalid Contact Number");
                    validPhoneNumber++;

                }
            }
            return validPhoneNumber;
        }

        CheckUserNameValidation = function (UserName) {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";
            UserName = UserName.replace(/_/g, '');
            var validUserName = 0;
            if (selfUser.isAgentOrTerminal != undefined && selfUser.isAgentOrTerminal != null && selfUser.isAgentOrTerminal != "undefined" && selfUser.isAgentOrTerminal == true) {
                if (UserName.length != 9) {
                    toastr.warning("Invalid User Name");
                    validUserName++;
                }
                else if (UserName.length == 9) {
                    var validName = UserName;
                    if (validName == 0) {
                        toastr.warning("Invalid User Name ");
                        validUserName++;
                    }
                }
            }
            else {
                if (UserName.length != 7) {
                    toastr.warning("Invalid User Name");
                    validUserName++;
                }
                else if (UserName.length == 7) {
                    var validName = UserName;
                    if (validName == 0) {
                        toastr.warning("Invalid User Name ");
                        validUserName++;
                    }
                }
            }
           
            return validUserName;
        }

        //Add User data saving data in API Service 
        selfUser.SaveUser = function (model) {

            if (model.ValidFromDate() != '')
                model.ValidFromDate(moment(model.ValidFromDate()).format('YYYY-MM-DD'));
            if (model.ValidToDate() != '')
                model.ValidToDate(moment(model.ValidToDate()).format('YYYY-MM-DD'));
            selfUser.viewModelHelper.isLoading(true);
            selfUser.UserValidation = ko.observable(model);
            selfUser.UserValidation().errors = ko.validation.group(selfUser.UserValidation());

            if ($('#utype').val() == 'AGNT') {
                model.Name = $("#AutoAgent").val();
                selfUser.isAgentOrTerminal=true;

            } else if ($('#utype').val() == 'EMP') {
                model.Name = $("#AutoEmployees").val();
                selfUser.isAgentOrTerminal = false;
            } else if ($('#utype').val() == 'TO') {
                model.Name = $("#AutoTo").val();
                selfUser.isAgentOrTerminal = true;
            } else {
                model.Name = $("#ExternalUser").val();
                selfUser.isAgentOrTerminal = true;
            }

            var errors = selfUser.UserValidation().errors().length;
            var errorValidateUser = selfUser.ValidateUser(model);
            var filterContanctNumber = model.ContactNo();
            var filterUserName = model.UserName();
            if (filterUserName != null || filterUserName != '' || filterUserName != undefined) {
                var validUserName = 0;
                if (filterUserName.length != 0) {
                    validUserName = CheckUserNameValidation(filterUserName);
                    if (validUserName > 0) {
                        errors = errors + 1;
                    }
                }
            }
            if (filterContanctNumber != null || filterContanctNumber != '' || filterContanctNumber != undefined) {
                var validPhoneNumber = 0;
                if (filterContanctNumber.length != 0) {
                    validPhoneNumber = CheckPhoneValidation(filterContanctNumber);
                    if (validPhoneNumber > 0) {
                        errors = errors + 1;
                    }
                }
            }

            if (errors == 0 && errorValidateUser == 0) {
                selfUser.viewModelHelper.isLoading(true);
                var userRoles = [];

                $.each(selfUser.userModel().viewRole(), function (index, value) {
                    userRoles.push(new UserRole(0, value, 'A', 0));
                    model.UserRoles(userRoles);
                });

                var portnames = [];
                $.each(selfUser.userModel().viewPorts(), function (index, value) {
                    portnames.push(new UserPorts(0, value, 'WFSA', 'A', 0, 0));
                    model.UserPorts(portnames);
                });

                model.UserType(selfUser.userModel().SubCatCode());
                var filterContanctNumber = model.ContactNo();
                filterContanctNumber = filterContanctNumber.replace(/(\)|\()|_|-+/g, '');
                model.ContactNo(filterContanctNumber);

                selfUser.viewModelHelper.apiPost('api/Users', ko.mapping.toJSON(model), function Message(data) {
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("User details saved successfully", "User");
                    advSearchUserTypeChange(model);
                    selfUser.LoadUserList();
                    $('#spnTitile').html("User");

                    selfUser.viewMode('List');
                    //setTimeout("window.location.reload(true);", 300);
                }, null, null, false);

            }
            else {
                selfUser.UserValidation().errors.showAllMessages();
                return false;
            }



        }

        //Modify User data saving data in API Service 
        selfUser.ModifyUser = function (model) {
            if (model.ValidFromDate() != '')
                model.ValidFromDate(moment(model.ValidFromDate()).format('YYYY-MM-DD'));

            if (model.ValidToDate() != '')
                model.ValidToDate(moment(model.ValidToDate()).format('YYYY-MM-DD'));

            selfUser.UserValidation = ko.observable(model);
            selfUser.UserValidation().errors = ko.validation.group(selfUser.UserValidation());
            var errors = selfUser.UserValidation().errors().length;

            errors = errors + selfUser.ValidateUser(model);

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

            if (errors == 0) {
                var userRoles = [];
                if (selfUser.isDormantUser()) {
                    model.DormantStatus("Y");
                }
                else {
                    model.DormantStatus("N");
                }
                $.each(selfUser.userModel().viewRole(), function (index, value) {

                    userRoles.push(new UserRole(0, value, 'A', 0));
                    model.UserRoles(userRoles);

                });
                var ports = [];
                $.each(selfUser.userModel().viewPorts(), function (index, value) {
                    ports.push(new UserPorts(0, value, 'WFSA', 'A', 0, 0));
                    model.UserPorts(ports);
                });
                model.UserTypeID(selfUser.userModel().UserTypeID());
                model.UserType(selfUser.userModel().SubCatCode());
                //Phone number replace with invalid chars
                filterContanctNumber = filterContanctNumber.replace(/(\)|\()|_|-+/g, '');
                model.ContactNo(filterContanctNumber);

                selfUser.viewModelHelper.apiPut('api/Users', ko.mapping.toJSON(model), function Message(data) {
                    selfUser.LoadUserList();
                    selfUser.viewMode('List');
                    $('#UserMasterTitile').html("User");
                    toastr.options.closeButton = true;
                    toastr.options.positionClass = "toast-top-right";
                    toastr.success("User details updated successfully", "User");
                });
            } else {
                selfUser.UserValidation().errors.showAllMessages();
                return;
            }

            selfUser.viewMode('List');
        }


        //User in View mode
        selfUser.viewuser = function (user) {

            //if (user.ValidFromDate() != '')
            //    user.ValidFromDate(moment(user.ValidFromDate()).format('YYYY-MM-DD'));

            //if (user.ValidToDate() != '')
            //    user.ValidToDate(moment(user.ValidToDate()).format('YYYY-MM-DD'));

            selfUser.LoadRoles();
            selfUser.LoadPortData();
            selfUser.viewMode('Form');
            selfUser.IsUpdate(false);
            selfUser.IsSave(false);
            selfUser.IsReset(false);
            selfUser.IsReset(false);
            selfUser.IsUserPort(true);

            selfUser.isDormantUserEnabled(false);
            selfUser.isDormantUserVisible(true);

            //selfUser.employeeValues({ Name: user.Name(), UserTypeID: user.UserTypeID() });
            selfUser.isReferenceNoEnable(false);
            user.viewRole.removeAll();
            $.each(user.Roles1(), function (key, value) {
                user.viewRole.push(value.RoleID);
            }
              );

            user.viewPorts.removeAll();
            $.each(user.UserPorts(), function (key, value) {
                user.viewPorts.push(value.PortCode);
            });
            selfUser.isNameEnable(false);
            selfUser.userModel(user);
            selfUser.editableView(false);
            selfUser.editActive(false);
            selfUser.isRoleEnabled(false);
            selfUser.isPortEnabled(false);
            selfUser.isReferenceNoEnable(false);
            selfUser.IsFirstNameEnabled(false);
            selfUser.IsSurNameEnabled(false);
            selfUser.IsUserNameEnabled(false);
            selfUser.IsReasonForAccessEnabled(false);
            selfUser.IsValidDtFromEnabled(false);
            selfUser.IsValidDtToEnabled(false);
            selfUser.IsContactNumberEnabled(false);
            selfUser.IsEmailAddEnabled(false);

            $('#UserMasterTitile').html("View User");
            selfUser.IsUserNameEnabled(false);
            selfUser.isUserTypeEnable(false);

            if (user.DormantStatus() == "Y") {
                selfUser.isDormantUser(true);
            }
            else {
                selfUser.isDormantUser(false);
            }

            if (user.SubCatCode() == "EMP") {
                $('#NoTitile').html("SAP No.:");
            }
            else if (user.SubCatCode() == "AGNT") {
                selfUser.isUserChanged(false);
                $('#NoTitile').html("Reference Number:");

            }
            else {
                selfUser.isUserChanged(false);
                $('#NoTitile').html("Registration Number:");
            }

            $("#divAutoEmployees").hide();
            $("#divAutoAgent").hide();
            $("#divAutoTo").hide();
            $("#divExternalUser").hide();
            $("#divDefault").hide();
            $("#AutoEmployees").prop("disabled", false);
            $("#AutoAgent").prop("disabled", false);
            $("#AutoTo").prop("disabled", false);
            if (user.SubCatCode() == "EMP") {
                $("#divAutoEmployees").show();
                $("#divAutoAgent").hide();
                $("#divAutoTo").hide();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
            } else if (user.SubCatCode() == "AGNT") {
                $("#divAutoEmployees").hide();
                $("#divAutoAgent").show();
                $("#divAutoTo").hide();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
            } else if (user.SubCatCode() == "TO") {
                $("#divAutoEmployees").hide();
                $("#divAutoAgent").hide();
                $("#divAutoTo").show();
                $("#divDefault").hide();
                $("#divExternalUser").hide();
            } else {
                $('#divReferenceNo').hide();
                $('#divDesignation').hide();
                $("#divAutoEmployees").hide();
                $("#divAutoAgent").hide();
                $("#divAutoTo").hide();
                $("#divExternalUser").show();
                $("#divDefault").hide();
            }

            //Masking Of Personal MobileNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNomaskedtextbox = $("#ContactNo").data("kendoMaskedTextBox");
            selfUser.ContactNo = ContactNomaskedtextbox.value();

            $("#AutoEmployees").prop("disabled", true);
            $("#AutoAgent").prop("disabled", true);
            $("#AutoTo").prop("disabled", true);

            selfUser.LoadEmployees(user);

            // Added by srini
            if (viewDetail == true) {
                var ReferenceID = user.UserID();
                var WorkflowInstanceID = user.WorkflowInstanceId();
                selfUser.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                       null,
                             function (result) {

                                 ko.utils.arrayForEach(result, function (val) {
                                     var pendingtaskaction = new IPMSROOT.pendingTask();
                                     pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                     pendingtaskaction.ReferenceID(val.ReferenceID);
                                     pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                     pendingtaskaction.APIUrl(val.APIUrl);
                                     pendingtaskaction.TaskName(val.TaskName);
                                     pendingtaskaction.TaskDescription(val.TaskDescription);
                                     pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                     pendingtaskaction.HasRemarks(val.HasRemarks);
                                     selfUser.userModel().pendingTasks.push(pendingtaskaction);
                                 });
                             }, null, null, false);

            }
        }

        //reset password
        selfUser.resetpassword = function (user) {
            selfUser.employeeValues({ Name: user.Name(), UserTypeID: user.UserTypeID() });
        }

        //User in Edit mode
        selfUser.edituser = function (user) {

            user.viewRole.removeAll();
            $.each(user.Roles1(), function (key, value) {
                user.viewRole.push(value.RoleID);
            });

            user.viewPorts.removeAll();
            $.each(user.UserPorts(), function (key, value) {
                user.viewPorts.push(value.PortCode);
            });
            //if (user.ValidFromDate() != '')
            //    user.ValidFromDate(moment(user.ValidFromDate()).format('YYYY-MM-DD'));

            //if (user.ValidToDate() != '')
            //    user.ValidToDate(moment(user.ValidToDate()).format('YYYY-MM-DD'));

            selfUser.LoadRoles();
            selfUser.LoadPortData();
            selfUser.userModel(user);
            selfUser.viewMode('Form');
            selfUser.editableView(false);
            selfUser.editActive(true);
            selfUser.isReferenceNoEnable(false);
            selfUser.IsFirstNameEnabled(false);
            selfUser.IsSurNameEnabled(false);
            selfUser.IsUserNameEnabled(false);
            selfUser.IsReasonForAccessEnabled(true);
            selfUser.IsValidDtFromEnabled(true);
            selfUser.IsValidDtToEnabled(true);
            selfUser.IsContactNumberEnabled(true);
            selfUser.IsEmailAddEnabled(true);
            selfUser.IsUserPort(true);
            selfUser.isDormantUserVisible(true);
            selfUser.isPortEnabled(true);

            if (user.DormantStatus() == "Y") {
                selfUser.isDormantUser(true);
                selfUser.isDormantUserEnabled(true);
            }
            else {
                selfUser.isDormantUser(false);
                selfUser.isDormantUserEnabled(false);
            }

            if (user.SubCatCode() == "EMP") {
                selfUser.isUserChanged(true);
                $('#NoTitile').html("SAP No.:");
                selfUser.isRoleEnabled(true);
            }
            else {
                selfUser.isUserChanged(false);
                $('#NoTitile').html("Registration Number:");
                selfUser.isRoleEnabled(false);
            }



            $("#divAutoEmployees").hide();
            $("#divAutoAgent").hide();
            $("#divAutoTo").hide();
            $("#divExternalUser").hide();
            $("#divDefault").hide();

            $("#AutoEmployees").prop("disabled", true);
            $("#AutoAgent").prop("disabled", true);
            $("#AutoTo").prop("disabled", true);
            $("#ExternalUser").prop("disabled", true);

            if (user.SubCatCode() == "EMP") {
                $("#divAutoEmployees").show();
                $("#divAutoAgent").hide();
                $("#divAutoTo").hide();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
            } else if (user.SubCatCode() == "AGNT") {
                $("#divAutoEmployees").hide();
                $("#divAutoAgent").show();
                $("#divAutoTo").hide();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
            } else if (user.SubCatCode() == "TO") {
                $("#divAutoEmployees").hide();
                $("#divAutoAgent").hide();
                $("#divAutoTo").show();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
            } else {
                $('#divReferenceNo').hide();
                $('#divDesignation').hide();

                $("#divAutoEmployees").hide();
                $("#divAutoAgent").hide();
                $("#divAutoTo").hide();
                $("#divExternalUser").show();
                $("#divDefault").hide();
            }

            selfUser.IsUpdate(true);
            selfUser.IsSave(false);
            selfUser.IsReset(true);

            selfUser.isUserTypeEnable(false);
            selfUser.isNameEnable(false);
            $('#UserMasterTitile').html("Update User");
            //selfUser.IsUserNameEnabled(false);

            //Masking Of Personal MobileNo.
            $("#ContactNo").kendoMaskedTextBox({
                mask: "(000)000-000-0000"
            });
            var ContactNomaskedtextbox = $("#ContactNo").data("kendoMaskedTextBox");
            selfUser.ContactNo = ContactNomaskedtextbox.value();
            selfUser.LoadEmployees(user);

        }

        selfUser.ResetUserPwd = function (model) {

            selfUser.viewModelHelper.apiPut('api/User/PutResetUserPassword', ko.mapping.toJSON(model),
                      function Message(data) {
                          toastr.options.closeButton = true;
                          toastr.options.positionClass = "toast-top-right";
                          toastr.success("Password reset successful", "User");
                          selfUser.LoadUserList();
                          selfUser.viewMode('List');
                          $('#UserMasterTitile').html("User");
                      });

        }

        //This method is fires when cancel button is pressed and all fields data is cleared and redirected to List form
        selfUser.cancel = function () {
            if (viewDetail == false) {
                selfUser.viewMode('List');
                selfUser.userModel().reset();
                $('#UserMasterTitile').html("User");

                //Masking Of Personal MobileNo.
                $("#ContactNo").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var ContactNomaskedtextbox = $("#ContactNo").data("kendoMaskedTextBox");
                selfUser.ContactNo = ContactNomaskedtextbox.value();
                selfUser.userModel().pendingTasks.removeAll();
            } else { window.location.href = "/Welcome"; }

        }

        //This method is fires when reset button pressed to reset the data that present in initial stage
        selfUser.ResetUser = function (model) {

            $("#divDefault").hide();
            $('#txtdefault').val('');
            $('#spanAgent').text('');
            $('#spanAgent').css('display', '');
            if (model.UserID() > 0) {
                ko.validation.reset();
                selfUser.userModel().reset();
                model.reset();

                $("#ContactNo").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });
                var ContactNotextbox = $("#ContactNo").data("kendoMaskedTextBox");
                selfUser.ContactNo = ContactNotextbox.value();
                //Masking Of Personal MobileNo.
                $("#ContactNo").kendoMaskedTextBox({
                    mask: "(000)000-000-0000"
                });

            }
            else {

                if (model.SubCatCode() == "AGNT") {

                    $('#AutoAgent').val('');
                    $('#spanAgent').text('');
                    $('#spanAgent').css('display', 'none');
                    $('#divAutoAgent').hide();
                    $("#divDefault").show();
                    var temp = [];
                    temp.push('');
                    selfUser.userModel().viewRole.push(temp);
                    selfUser.userModel().viewRole([0]);
                    selfUser.userModel().viewPorts.push(temp);
                    selfUser.userModel().viewPorts([0]);
                } else if (model.SubCatCode() == "EMP") {

                    $('#AutoEmployees').val('');
                    $('#spanEmployees').text('');
                    $('#spanEmployees').css('display', 'none');
                    $('#divAutoEmployees').hide();
                    $("#divDefault").show();
                    var temp = [];
                    temp.push('');

                    if (selfUser.userModel().viewRole.length > 0) {
                        selfUser.userModel().viewRole.push(temp);
                        selfUser.userModel().viewRole([0]);
                    } else {
                        //selfUser.userModel().viewRole.push(temp);
                        selfUser.userModel().viewRole([0]);
                    }
                    if (selfUser.userModel().viewPorts.length > 0) {
                        selfUser.userModel().viewPorts.push(temp);
                        selfUser.userModel().viewPorts([0]);
                    } else {
                        selfUser.userModel().viewPorts([0]);
                    }

                } else if (model.SubCatCode() == "TO") {

                    $('#AutoTo').val('');
                    $('#spanTO').text('');
                    $('#spanTO').css('display', 'none');
                    $('#divAutoTo').hide();
                    $("#divDefault").show();
                    var temp = [];
                    temp.push('');

                    selfUser.userModel().viewRole.push(temp);
                    selfUser.userModel().viewRole([0]);
                    selfUser.userModel().viewPorts.push(temp);
                    selfUser.userModel().viewPorts([0]);
                } else if (model.SubCatCode() == "EXTU") {
                    $('#ExternalUser').val('');
                    $('#spanExternalUser').text('');
                    $('#spanExternalUser').css('display', 'none');
                    $('#divExternalUser').hide();
                    $("#divDefault").show();
                    var temp = [];
                    temp.push('');

                    selfUser.userModel().viewRole.push(temp);
                    selfUser.userModel().viewRole([0]);

                    selfUser.userModel().viewPorts.push(temp);
                    selfUser.userModel().viewPorts([0]);
                    if (model.viewRole().length > 0) {
                        model.Roles.removeAll();
                        model.viewRole.removeAll();
                    }

                    if (selfUser.userModel().viewRole.length > 0)
                        selfUser.userModel().viewRole.removeAll();

                    if (model.viewPorts().length > 0) {
                        //model.UserPorts.removeAll();
                        model.viewPorts.removeAll();
                    }
                    if (selfUser.userModel().viewPorts.length > 0)
                        selfUser.userModel().viewPorts.removeAll();

                } else {
                    $('#divExternalUser').hide();
                    $('#divAutoEmployees').hide();
                    $('#divAutoAgent').hide();
                    $('#divAutoTo').hide();
                }

                $("#divDefault").show();

                $('#utype').val('');
                $('#spanUsertype').text('');

                $('#Desig').val('');

                $('#FN').val('');
                $('#FN').text('');

                $('#LN').val('');
                $('#LN').text('');

                $('#UserName').val('');
                $('#spanusername').text('');

                $('#EmailID').val('');
                $('#ContactNo').val('');
                $('#ReferenceNo').val('');
                $('#ReasonForAccess').val('');
                $('#ValidFromDate').val('');
                $('#ValidToDate').val('');
            }



        }

        //Clear text boxes function
        selfUser.ClearEmployeeDetails = function () {

            selfUser.userModel().FirstName("");
            selfUser.userModel().ReferenceNo("");
            selfUser.userModel().Designation("");
            selfUser.userModel().LastName("");
            selfUser.userModel().ContactNo("");
            selfUser.userModel().EmailID("");
            selfUser.userModel().UserName("");
            selfUser.userModel().ReasonForAccess("");
            selfUser.userModel().ValidFromDate("");
            selfUser.userModel().ValidToDate("");
            $('#MultiSelect').val('');
            $('#MultiSelect').text('');
            $('#MultiPorts').val('');
            $('#MultiPorts').text('');
            selfUser.userModel().viewRole([0]);

            selfUser.userModel().viewPorts([0]);
            selfUser.IsFirstNameEnabled(true);
            selfUser.IsSurNameEnabled(true);
            selfUser.IsUserNameEnabled(true);
            selfUser.IsEmailAddEnabled(true);
            selfUser.IsContactNumberEnabled(true);
            selfUser.IsReasonForAccessEnabled(true);
            selfUser.IsValidDtFromEnabled(true);
            selfUser.IsValidDtToEnabled(true);
        }

        // Verify UserName for unique value
        ValidEvent = function (data, event) {
            var databaseList = ko.toJSON(selfUser.UserList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;
            var UserTypeID = formList.UserTypeID
            var UserName = formList.UserName.toLowerCase() == null ? '' : formList.UserName.toLowerCase();

            selfUser.viewModelHelper.apiGet('api/CheckUserExists/' + UserTypeID + '/' + UserName,
            null,
              function (result) {
                  if (result > 0) {
                      $('#spanusername').text('User Name already exists!');
                      flag = false;
                      return false;
                  } else {
                      $('#spanusername').text("");
                      flag = true;
                      return true;
                  }
              }, null, null, false);
        }

        // Added by  Srini
        selfUser.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            selfUser.ENValidation = ko.observable(dat);
            selfUser.ENValidation().errors = ko.validation.group(selfUser.ENValidation());
            var errors = selfUser.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, selfUser.userModel());
            }
            else {
                selfUser.ENValidation().errors.showAllMessages();
            }
        }

        selfUser.LoadEmployees = function (user) {

            if (user.SubCatCode() == "AGNT") {
                $("#divAutoEmployees").hide();
                $("#divAutoAgent").show();
                $("#divAutoTo").hide();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
                $("#AutoAgent").kendoAutoComplete({
                    dataTextField: 'Name',
                    minLength: 1, pageSize: 1,
                    dataSource: new kendo.data.DataSource({
                        transport: {
                            read: {
                                url: "/api/AgentsDetails",
                            }
                        }, schema: {
                            data: function (data) { ko.mapping.fromJS(data, {}, selfUser.employeeValues); return data; }, total: function (data) {
                                return data.length;
                            }
                        }
                    }),

                    value: user.Name(),
                    select: BindData,
                    placeholder: "Select Agent...",
                });


            }


            if (user.SubCatCode() == "EMP") {

                $("#divAutoEmployees").show();
                $("#divAutoAgent").hide();
                $("#divAutoTo").hide();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
                $("#AutoEmployees").kendoAutoComplete({
                    dataTextField: 'Name',
                    minLength: 1, pageSize: 1,
                    dataSource: new kendo.data.DataSource({
                        transport: {
                            read: {
                                url: "/api/EmployeesDetails",
                            }
                        }, schema: {
                            data: function (data) { ko.mapping.fromJS(data, {}, selfUser.employeeValues); return data; }, total: function (data) {
                                return data.length;
                            }
                        }
                    }),

                    value: user.Name(),
                    select: BindData,
                    placeholder: "Select Employee...",
                });
            }

            if (user.SubCatCode() == "TO") {

                $("#divAutoEmployees").hide();
                $("#divAutoAgent").hide();
                $("#divAutoTo").show();
                $("#divExternalUser").hide();
                $("#divDefault").hide();
                $("#AutoTo").kendoAutoComplete({
                    dataTextField: 'Name',
                    minLength: 1, pageSize: 1,
                    dataSource: {
                        serverFiltering: true,
                        transport: {
                            read: { url: '/api/TerminalOperatorsDetails', dataType: 'json' }
                        }, schema: {
                            data: function (data) { ko.mapping.fromJS(data, {}, selfUser.employeeValues); return data; }, total: function (data) {

                                return data.length;
                            }
                        },
                    },
                    filter: 'contains',
                    value: user.Name(),
                    select: BindData,
                    placeholder: "Select Terminal Operator...",
                });

            }
        }

        selfUser.ValidateUser = function (some) {

            var val = 0;
            var UserTypeID = some.UserTypeID() == '' ? null : some.UserTypeID();
            var UserName = some.UserName() == '' ? null : some.UserName();
            $('#spanRole').text('');
            $('#spanRole').css('display', 'none');
            $('#spanusername').text('');
            $('#spanusername').css('display', 'none');

            $('#spanValidFromDate').text('');
            $('#spanValidFromDate').css('display', 'none');

            $('#spanValidToDate').text('');
            $('#spanValidToDate').css('display', 'none');
            if (some.UserID() == '' || some.UserID() == 0) {
                selfUser.viewModelHelper.apiGet('api/CheckUserExists/' + UserTypeID + '/' + UserName,
                  { UserTypeID: UserTypeID, UserName: UserName },
                   function (result) {
                       if (result > 0) {

                           val += 1;
                           toastr.options.closeButton = true;
                           toastr.options.positionClass = "toast-top-right";
                           toastr.warning("Already exists ! Select another User Name", "User");
                       }
                   }, null, null, false);
            }
            var multi = $("#MultiSelect").data("kendoMultiSelect");
            var multiports = $("#MultiPorts").data("kendoMultiSelect");

            if (some.Name == '') {
                val += 1;
                if ($('#utype').val() == 'AGNT') {
                    $('#spanAgent').text('* This field is required');
                    $('#spanAgent').css('display', '');
                } else if ($('#utype').val() == 'EMP') {
                    $('#spanEmployees').text('* This field is required');
                    $('#spanEmployees').css('display', '');
                } else if ($('#utype').val() == 'TO') {
                    $('#spanTO').text('* This field is required');
                    $('#spanTO').css('display', '');
                } else if ($('#utype').val() == 'EXTU') {
                    $('#spanExternalUser').text('* This field is required');
                    $('#spanExternalUser').css('display', '');
                } else {
                    $('#spandefault').text('* This field is required');
                    $('#spandefault').css('display', '');
                }


            } else {
                if ($('#utype').val() == 'AGNT') {
                    $('#spanAgent').text('');
                    $('#spanAgent').css('display', 'none');
                } else if ($('#utype').val() == 'EMP') {
                    $('#spanEmployees').text('');
                    $('#spanEmployees').css('display', 'none');
                } else if ($('#utype').val() == 'TO') {
                    $('#spanTO').text('');
                    $('#spanTO').css('display', 'none');
                } else if ($('#utype').val() == 'EXTU') {
                    $('#spanExternalUser').text('');
                    $('#spanExternalUser').css('display', 'none');
                } else {
                    $('#spandefault').text('');
                    $('#spandefault').css('display', 'none');
                }
            }


            if ($("#utype").val() == "" || $("#utype").val() == null) {
                val += 1;
                $('#spanUsertype').text('* This field is required');
                $('#spanUsertype').css('display', '');
            }
            else {
                $("#spanUsertype").text('');
                $('#spanUsertype').css('display', 'none');
            }

            if (some.UserName() == '') {
                val += 1;
                $('#spanusername').text('* This field is required');
                $('#spanusername').css('display', '');
            } else {
                $('#spanusername').text('');
                $('#spanusername').css('display', 'none');
            }
            if (selfUser.userModel().viewRole().length == 0 || multi.value().length == 0) {
                val += 1;
                $('#spanRole').text('* This field is required');
                $('#spanRole').css('display', '');
            } else {
                $('#spanRole').text('* This field is required');
                $('#spanRole').css('display', 'none');
            }

            if (selfUser.userModel().viewPorts().length == 0 || multiports.value().length == 0) {
                val += 1;
                $('#spanMultiports').text('* This field is required');
                $('#spanMultiports').css('display', '');
            } else {
                $('#spanMultiports').text('* This field is required');
                $('#spanMultiports').css('display', 'none');
            }

            if (some.ValidFromDate() == '') {
                val += 1;
                $('#spanValidFromDate').text('* This field is required');
                $('#spanValidFromDate').css('display', '');
            } else {
                $('#spanValidFromDate').text('');
                $('#spanValidFromDate').css('display', 'none');

            }

            if (some.ValidToDate() == '') {
                val += 1;
                $('#spanValidToDate').text('* This field is required');
                $('#spanValidToDate').css('display', '');
            } else {
                $('#spanValidToDate').text('');
                $('#spanValidToDate').css('display', 'none');
            }



            if (some.SubCatCode() != 'EXTU') {
                if (some.ReferenceNo() == '') {
                    val += 1;
                    $('#spanReferenceNo').text('* This field is required');
                    $('#spanReferenceNo').css('display', '');
                } else {
                    $('#spanReferenceNo').text('');
                    $('#spanReferenceNo').css('display', 'none');
                }

            }

            return val;
        }

        ValidDate = function () {
            selfUser.userModel().ValidToDate(selfUser.userModel().ValidFromDate());
        }

        advSearchUserTypeChange = function (data) {

            var usertype = $('select#UsertypeID option:selected').val();
            if (data.userNameSearch() != undefined)
                data.userNameSearch('');
            if (data.ReferenceNoSearch() != undefined)
                data.ReferenceNoSearch('');
            $('#chkDormantUsers').attr('checked', false);;

            $("#UserNameID").val('');
            $("#refno").val('');
            if (usertype == 'EMP') {
                $('#lblReferenceNo').html('SAP No.');
                $('#divSAPNO').show();
            }
            else if (usertype == 'AGNT') {
                $('#lblReferenceNo').html('Reference No.');
                $('#divSAPNO').show();
            }
            else if (usertype == 'TO') {
                $('#lblReferenceNo').html('Registration No.');
                $('#divSAPNO').show();
            }
            else {
                $('#divSAPNO').hide();

            }
        }

        selfUser.viewWorkFlow = function (user) {
            var workflowinstanceId = user.WorkflowInstanceId();
            if (workflowinstanceId == "" || workflowinstanceId == null) {
                $('#WorkFlowRemarks').text('No Remarks');
                $('#stack1').modal('show');
            }
            else {
                selfUser.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
                 { workflowinstanceId: workflowinstanceId },
                  function (result) {

                      selfUser.userModel(new IPMSROOT.UserModel());
                      selfUser.userModel().WorkFlowRemarks(result);
                      $('#WorkFlowRemarks').text(result);
                      $('#stack1').modal('show');

                  });
            }
        }



        selfUser.Initialize();
    }

    IPMSRoot.UserViewModel = UserViewModel;

}(window.IPMSROOT));

function UserRole(UserID, RoleID, RecordStatus, CreatedBy) {
    this.UserID = UserID;
    this.RoleID = RoleID;
    this.RecordStatus = RecordStatus;
    this.CreatedBy = CreatedBy;
}

function UserPorts(UserID, PortCode, WFStatus, RecordStatus, CreatedBy, ModifiedDate) {
    this.UserID = UserID;
    this.PortCode = PortCode;
    this.WFStatus = WFStatus;
    this.RecordStatus = RecordStatus;
    this.CreatedBy = CreatedBy;
    this.ModifiedDate = ModifiedDate;
};




