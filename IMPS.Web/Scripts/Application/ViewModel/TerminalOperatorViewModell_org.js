(function (IPMSRoot) {
    var isView = 0;
    var TerminalOperatorViewModel = function () {
        var self = this;
        $('#spnTitle').html("TerminalOperatorMaster");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        //self.terminaloperatorModel = ko.observable(new IPMSROOT.TerminalOperatorModel());
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.Quaynamevalues = ko.observableArray();
        self.terminaloperatorModel = ko.observable([]);
        self.terminaloperatorList = ko.observableArray();
        self.isQuayChanged = ko.observable(true);
        self.isportChanged = ko.observable(true);
        self.berthValues = ko.observableArray();
        self.IsCodeEnable = ko.observable(true);
        self.Cargovalues = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.licensedstatus = ko.observable(true);
        self.selectedQuayCode = ko.observable();




        // this is for grid binding
        self.Loadterminaloparetor = function () {
            self.viewModelHelper.apiGet('api/TerminalOperator/GetTerminalOperatorlist',
             //{ status:'A' },
              function (result) {
                  ko.mapping.fromJS(result, {}, self.terminaloperatorList);

                  /*self.terminaloperatorList(result);*/
                  //self.terminaloperatorList(ko.utils.arrayMap(result, function (item) {
                  //    return new IPMSRoot.TerminalOperatorModel(item);
                  //}));
              });
        }

        self.Initialize = function () {
            self.terminaloperatorModel(new IPMSROOT.TerminalOperatorModel());
            self.Loadterminaloparetor();
            self.viewMode('List');

        }
        //for Add iteam 
        self.addTerminalOperator = function () {
            self.terminaloperatorModel(new IPMSRoot.TerminalOperatorModel());
            $('#spnTitle').html("Add Terminal Operator");
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.selectedQuayCode();
            self.LoadQuayname();
            self.loadCargohandling();


        }

        self.cancel = function () {
            self.viewMode('List');
            self.terminaloperatorModel().reset();
            $('#spnTitle').html("TerminalOperatorMaster");

        }


        self.ResetTerminalOperator = function (model) {
            self.terminaloperatorModel(model).reset();
            self.viewMode('Form');
            self.editableView(true);

        }

        self.SaveTerminalOperator = function (TerminalOperator) {
            //self.TerminalOperatorValidation = ko.observable(model);
            //self.TerminalOperatorValidation().errors = ko.validation.group(self.TerminalOperatorValidation());
            //if (ValidateFormValues(model) == false) {
            //    return;
            ////}
            //alert(TerminalOperator.selectedQuayCode());
            var applicantAddressObj = new ApplicantAddress(
                  0,
                  self.terminaloperatorModel().NumStreet(),
                  self.terminaloperatorModel().Suburb(),
                  self.terminaloperatorModel().TownCity(),
                  self.terminaloperatorModel().PostalCode(),
                  'ADPE',
                  'A',
                  1,
                  GetDateTime()
              );

            self.terminaloperatorModel().PostalAddress(applicantAddressObj);
            if (self.terminaloperatorModel().NumStreet2() != '' || self.terminaloperatorModel().Suburb2() != '') {

                var applicantAddressObjForAddrType2 = new ApplicantAddress(
                 0,
                self.terminaloperatorModel().NumStreet2(),
                self.terminaloperatorModel().Suburb2(),
                self.terminaloperatorModel().TownCity2(),
                self.terminaloperatorModel().PostalCode2(),
                'ADTE',
                'A',
                 1,
                GetDateTime()
                );
            }
            self.terminaloperatorModel().BusinessAddress(applicantAddressObjForAddrType2);

            if ($('#radioBerths').is(':checked') == true) {
                var applberthWorkFlowObjArry = new Array();
                $('#tblberth input[name="Berth"]:checked').each(function () {
                    var terminaloperatorberthObj = new terminaloperatorberth(0, null, TerminalOperator.selectedQuayCode(), this.value, GetDateTime(), 1, "A");
                    applberthWorkFlowObjArry.push(terminaloperatorberthObj);
                });
                self.terminaloperatorModel().TerminalOperatorBerths(applberthWorkFlowObjArry);//pushing applicant workflow array object

            }
            if ($('#radioCargoHandling').is(':checked') == true) {
                var applcargoWorkFlowObjArry = new Array();
                $('#tblcargo input[name="cargo"]:checked').each(function () {
                    var terminaloperatorcargohandlingobj = new terminaloperatorcargohandling(0, this.value, GetDateTime(), 1, "A");
                    applcargoWorkFlowObjArry.push(terminaloperatorcargohandlingobj);
                });
                self.terminaloperatorModel().TerminalOperatorCargoHandlings(applcargoWorkFlowObjArry);//pushing applicant workflow array object

            }
            self.viewModelHelper.apiPost('api/TerminalOperator/PostTerminalOperatorData', ko.mapping.toJSON(TerminalOperator),

               function (applicantId) {
                   if (applicantId > 0) {
                       ResetAllFields(TerminalOperator);
                       toastr.success("TerminalOperator details saved successfully", "TerminalOperator");
                       if (isAuthenticated == 'True') {
                           setTimeout(function () {
                               window.location = '/TerminalOperatorMaster/TerminalOperatorMaster';
                           }, 5000);
                       }
                       else {
                           setTimeout(function () {
                               window.location = '/Account/Login';
                           }, 5000);
                       }

                   }
                   else {
                       toastr.error("An error occured. Please try again later!", "Error");
                   }
               });



        }

        self.UpdateTerminalOperator = function (TerminalOperator) {
            // self.TerminalOperatorValidation = ko.observable(model);
            //self.TerminalOperatorValidation().errors = ko.validation.group(self.TerminalOperatorValidation());
            //if (ValidateFormValues(model) == false) {
            //    return;
            //}
            var applicantAddressObj = new ApplicantAddress(
               self.terminaloperatorModel().PostalAddressID(),
               self.terminaloperatorModel().NumStreet(),
               self.terminaloperatorModel().Suburb(),
               self.terminaloperatorModel().TownCity(),
               self.terminaloperatorModel().PostalCode(),
               'ADPE',
               'A',
               1,
               GetDateTime()
           );

            self.terminaloperatorModel().PostalAddress(applicantAddressObj);
            //if (self.terminaloperatorModel().NumStreet2() != '' || self.terminaloperatorModel().Suburb2() != '') {

            var applicantAddressObjForAddrType2 = new ApplicantAddress(
            self.terminaloperatorModel().BusinessAddressID(),
            self.terminaloperatorModel().NumStreet2(),
            self.terminaloperatorModel().Suburb2(),
            self.terminaloperatorModel().TownCity2(),
            self.terminaloperatorModel().PostalCode2(),
            'ADTE',
            'A',
             1,
            GetDateTime()
            );
            //}
            self.terminaloperatorModel().BusinessAddress(applicantAddressObjForAddrType2);

            if ($('#radioBerths').is(':checked') == true) {
                var applberthWorkFlowObjArry = new Array();
                $('#tblberth input[name="Berth"]:checked').each(function () {
                    var terminaloperatorberthObj = new terminaloperatorberth(0, "CT", TerminalOperator.selectedQuayCode(), this.value, GetDateTime(), 1, "A");
                    applberthWorkFlowObjArry.push(terminaloperatorberthObj);
                });
                self.terminaloperatorModel().TerminalOperatorBerths(applberthWorkFlowObjArry);//pushing applicant workflow array object

            }
            if ($('#radioCargoHandling').is(':checked') == true) {
                var applcargoWorkFlowObjArry = new Array();
                $('#tblcargo input[name="cargo"]:checked').each(function () {
                    var terminaloperatorcargohandlingobj = new terminaloperatorcargohandling(0, this.value, GetDateTime(), 1, "A");
                    applcargoWorkFlowObjArry.push(terminaloperatorcargohandlingobj);
                });
                self.terminaloperatorModel().TerminalOperatorCargoHandlings(applcargoWorkFlowObjArry);//pushing applicant workflow array object

            }
            self.viewModelHelper.apiPost('api/TerminalOperator/PostTerminalOperatorData', ko.mapping.toJSON(TerminalOperator),

                function (applicantId) {
                    if (applicantId > 0) {
                        ResetAllFields(TerminalOperator);
                        toastr.success("TerminalOperator details saved successfully", "TerminalOperator");
                        if (isAuthenticated == 'True') {
                            setTimeout(function () {
                                window.location = '/TerminalOperatorMaster/TerminalOperatorMaster';
                            }, 5000);
                        }
                        else {
                            setTimeout(function () {
                                window.location = '/Account/Login';
                            }, 5000);
                        }

                    }
                    else {
                        toastr.error("An error occured. Please try again later!", "Error");
                    }
                });



        }

        self.EditTerminalOperator = function (TerminalOperator) {
            isView = 0;
            $('#spnTitle').html("Update Terminal Operator");
            GetTerminalOperatorDetails(TerminalOperator.TerminalOperatorID, self, isView);
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(false);
            self.LoadQuayname();
            self.terminaloperatorModel().isView(true);
            self.loadCargohandling();
            //self.FillCargo();
            self.FillBirth();
            //self.berthValues();
            //self.terminaloperatorModel(TerminalOperator);
            //self.Quaynamevalues();  //{ QuayCode: berth.QuayCode(), QuayName: berth.QuayName() }



        }

        self.viewTerminalOperator = function (TerminalOperator) {
            isView = 1;
            GetTerminalOperatorDetails(TerminalOperator.TerminalOperatorID, self, isView);
            self.viewMode('Form');
            self.terminaloperatorModel().isView(false);
            $('#spnTitle').html("View TerminalOperator");
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.loadCargohandling();
            self.FillCargo(true);
            self.FillBirth(true);
            self.LoadQuayname(true);
            self.loadQuayandBerthname(true);

            //self.terminaloperatorModel(TerminalOperator);
            //self.loadQuayandBerthname({ QuayCode: berth.QuayCode(), QuayName: berth.QuayName() });


        }

        self.DeleteTerminalOperator = function (Terminalaoperator)
        { InactiveAgent(Terminalaoperator, self); }

        function GetTerminalOperatorDetails(TerminalOperatorID, model, isView) {

            model.viewModelHelper.apiGet('api/TerminalOperator/GetTerminalOperatorDetails', { TerminalOperatorID: TerminalOperatorID },
                               function (result) {

                                   FillTerminalOperatorDetailsForUpdation(result, model, isView);
                               });
        }

        function FillTerminalOperatorDetailsForUpdation(model, vm, isView) {

            if (model != null) {


                vm.terminaloperatorModel().TerminalOperatorID(model.TerminalOperatorID);
                vm.terminaloperatorModel().RegisteredName(model.RegisteredName);
                vm.terminaloperatorModel().RegistrationNumber(model.RegistrationNumber);
                vm.terminaloperatorModel().TradingName(model.TradingName);
                vm.terminaloperatorModel().RegistrationDate(model.RegistrationDate);
                vm.terminaloperatorModel().ValidityDate(model.ValidityDate);
                vm.terminaloperatorModel().PremiseLocation(model.PremiseLocation);
                vm.terminaloperatorModel().LicensedFor(model.LicensedFor);
                vm.terminaloperatorModel().TelephoneNo1(model.TelephoneNo1);
                vm.terminaloperatorModel().FaxNo(model.FaxNo);
                vm.terminaloperatorModel().RecordStatus(model.RecordStatus);
                vm.terminaloperatorModel().CreatedBy(model.CreatedBy);
                vm.terminaloperatorModel().CreatedDate(model.CreatedDate);
                vm.terminaloperatorModel().ModifiedBy(model.ModifiedBy);
                vm.terminaloperatorModel().ModifiedDate(model.ModifiedDate);

                vm.terminaloperatorModel().PostalAddressID(model.PostalAddressID);
                vm.terminaloperatorModel().BusinessAddressID(model.BusinessAddressID);
                vm.terminaloperatorModel().AddressType(model.PostalAddress.AddressType);
                vm.terminaloperatorModel().NumStreet(model.PostalAddress.NumberStreet);
                vm.terminaloperatorModel().Suburb(model.PostalAddress.Suburb);
                vm.terminaloperatorModel().TownCity(model.PostalAddress.TownCity);
                vm.terminaloperatorModel().PostalCode(model.PostalAddress.PostalCode);
                vm.terminaloperatorModel().TelephoneNo1(model.TelephoneNo1);
                vm.terminaloperatorModel().FaxNo(model.FaxNo);
                if (model.BusinessAddress != null) {
                    vm.terminaloperatorModel().AddressType1(model.BusinessAddress.AddressType1);
                    vm.terminaloperatorModel().NumStreet2(model.BusinessAddress.NumberStreet);
                    vm.terminaloperatorModel().Suburb2(model.BusinessAddress.Suburb);
                    vm.terminaloperatorModel().TownCity2(model.BusinessAddress.TownCity);
                    vm.terminaloperatorModel().PostalCode2(model.BusinessAddress.PostalCode);

                    //? '' : model.Address[1].PostalCode);
                }
                //if (model.LicensedFor == "TOB") {
                //    $('#divquay').show();
                //    $('#divberth').show();
                //    $('#divcargo').hide();
                //    self.Quaynamevalues({ QuayCode: model.QuayCode(), QuayName: model.QuayName() });
                //}
                //else {
                //    $('#divquay').hide();
                //    $('#divberth').hide();
                //    $('#divcargo').show();

                //}






                if (isView == 1) {
                    $('#divtoregfirstdetails input[type=text],#divtoregseconddetails input[type=text],#divtoaddressdetails input[type=text],#divtoaddressdetails input[type=checkbox],#divlicensedfor input[type=checkbox],#divlicensedfor input[type=radio],#divcargo input[type=checkbox],#tblberth input[type=checkbox],#Quaynames, #chkBuisinessAddress').prop('disabled', true);
                    //$('#datepicker').attr('readonly', 'true');
                }
                else {
                    $('#divtoregfirstdetails input[type=text],#divtoregseconddetails input[type=text],#divtoaddressdetails input[type=text],#divtoaddressdetails input[type=checkbox],#divlicensedfor input[type=checkbox],#divlicensedfor input[type=radio],#Quaynames,S#chkBuisinessAddress').prop('disabled', false);

                }
            }


        }

        function InactiveAgent(Terminalaoperator, self) {
            self.viewModelHelper.apiGet('api/TerminalOperator/InactiveTerminalOperator', { id: Terminalaoperator.TerminalOperatorID },
                     function (result) {
                         if (result == true) {

                             toastr.success("TerminalOperator details Delete successfully", "TerminalOperator");
                             //alert("TerminalOperator details Delete successfully");
                             self.Loadterminaloparetor(self);
                         }
                     });
        }

        function ResetAllFields(model) {

            model.TerminalOperatorID('');
            model.TradingName('');
            model.RegistrationNumber('');
            model.RegistrationDate('');
            model.RegisteredName('');
            model.ValidityDate('');
            model.PremiseLocation('');
            model.FaxNo('');
            model.TelephoneNo1('');
            model.LicensedFor('');
            model.PostalAddress();
            model.BusinessAddress();
            model.NumStreet('');
            model.Suburb('');
            model.TownCity('');
            model.PostalCode('');
            model.NumStreet2('');
            model.Suburb2('');
            model.TownCity2('');
            model.PostalCode2('');
            model.TelephoneNo1('');
            model.FaxNo('');
            model.TerminalOperatorBerths([]);
            model.TerminalOperatorCargoHandlings([]);
            // model.PortCode();
            //model.QuayCode('');
            //model.QuayName('');
            //model.BerthCode('');
            //model.BerthName('');

            //alert("view from controller");


            //$('#divPortList input[name="port"]:checked').attr('checked', false);
            //$('#chkSelectAll').attr('checked', false);

        }

        self.FillPostal = function () {
            if ($('#chkBuisinessAddress').is(':checked') == true) {
                self.terminaloperatorModel().NumStreet2((self.terminaloperatorModel().NumStreet()));
                self.terminaloperatorModel().Suburb2((self.terminaloperatorModel().Suburb()));
                self.terminaloperatorModel().TownCity2((self.terminaloperatorModel().TownCity()));
                self.terminaloperatorModel().PostalCode2((self.terminaloperatorModel().PostalCode()));
            }
            else {
                self.terminaloperatorModel().postalNumStreet('');
                self.terminaloperatorModel().postalSuburb('');
                self.terminaloperatorModel().postalTownCity('');
                self.terminaloperatorModel().postalPostalCode('');
            }
            return true;
        }

        self.FillBirth = function (event) {

            if ($('#radioBerths').is(':checked') == true) {
                $('#radioCargoHandling').attr("checked", false);
                $('#divquay').show();
                $('#divberth').show();
                $('#divcargo').hide();

            }
            else if ($('#radioCargoHandling').is(':checked') == true) {
                $('#radioBerths').attr("checked", false);
                $('#divquay').hide();
                $('#divberth').hide();
                $('#divcargo').show();

            }
            return true;
        }



        self.LoadQuayname = function (event) {

            self.viewModelHelper.apiGet('api/TerminalOperator/GetQuaynames',
          null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.Quaynamevalues);
                  // self.Quaynamevalues(result);
                  /*self.Quaynamevalues(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.TerminalOperatorModel(item);
                  }));*/
              });
        }

        self.loadQuayandBerthname = function (event) {
            alert(event.selectedQuayCode());
            //self.viewModelHelper.apiGet('api/TerminalOperator/GetBerthandQuays',
            //    { QuayCode: event.selectedQuayCode() },
            //  function (result) {
            //      ko.mapping.fromJS(result, {}, self.berthValues);
            //  });
        }

        self.loadCargohandling = function (event) {
            self.viewModelHelper.apiGet('api/TerminalOperator/GetCargoHandling',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.Cargovalues);
              });

        }

        ValidEvent = function (data, event) {
            var items = ko.toJSON(self.berthList);
            var jsonObj = JSON.parse(items);
            var some = JSON.parse(ko.toJSON(data));
            $.each(jsonObj, function (index, value) {
                if (value.BerthCode == some.BerthCode) {
                    $('#spanterminaloperatorcode').text('Already exists ! enter other Code');
                    $('#spanterminaloperatorcode').css('display', '');
                }
                return;
            });
        }

        var keynum;
        var keychar;
        var charcheck;

        function ValidateAlphanumeric(data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
            return charcheck.test(keychar);
        }

        function Validatenumeric(data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /[0-9]/;
            return charcheck.test(keychar);
        }

        function ValidateFormValues(model) {
            var result = true;
            var countCargo = $('#tblcargo input[name="port"]:checked').length;
            var countberth = $('#tblberth input[name="port"]:checked').length;
            if ($('#radioBerths').is(':checked') == true) {
                if (countberth == 0) {
                    toastr.error("Please select the Checkbox of berth to alloat terminaloperator.", "Error");
                    result = false;
                }
            }
            else {
                if (countCargo == 0) {
                    toastr.error("Please select the Checkbox of Cargotypeyou want to for.", "Error");
                    result = false;
                }
            }
            if (model.terminaloperatorModel().errors().length > 0) {
                model.terminaloperatorModel().errors.showAllMessages();
                toastr.error("Please fill the fields", "Error");
                result = false;
            }
            if (model.viewMode() == 'register2') {
                if (agentData.AgentData().UploadedFiles().length == 0) {
                    toastr.error("Please upload documents", "Error");
                    return false;
                }
            }
            if (result == false) {
                $('#ulTabs li:first a').click();
            }
            return result;
        }

        function ValidateAlphabetsWithSpaces(data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z ]*$/;
            return charcheck.test(keychar);
        }
        self.Initialize();
    }
    IPMSRoot.TerminalOperatorViewModel = TerminalOperatorViewModel;

}(window.IPMSROOT));

ko.validation.configure({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
});
toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";

$('input').bind("paste", function (e) {
    e.preventDefault();
});

function ApplicantAddress(AddressID, NumStreet, Suburb, TownCity, PostalCode, AddressType, RecordStatus, CreatedBy, CreatedDate) {
    this.AddressID = AddressID;
    this.NumberStreet = NumStreet;
    this.Suburb = Suburb;
    this.TownCity = TownCity;
    this.PostalCode = PostalCode;
    this.AddressType = AddressType;
    this.RecordStatus = RecordStatus;
    this.CreatedBy = CreatedBy;
    this.CreatedDate = CreatedDate;
}

function terminaloperatordetails(TerminalOperatorID, RegisteredName, TradingName, RegistrationNumber, RegistrationDate, ValidityDate, PremiseLocation, LicensedFor, TelephoneNo1, FaxNo, CreatedBy, CreatedDate, RecordStatus) {
    this.TerminalOperatorID = TerminalOperatorID;
    this.RegisteredName = RegisteredName;
    this.TradingName = TradingName;
    this.RegistrationNumber = RegistrationNumber;
    this.RegistrationDate = RegistrationDate;
    this.ValidityDate = ValidityDate;
    this.PremiseLocation = PremiseLocation;
    this.TelephoneNo1 = TelephoneNo1;
    this.FaxNo = FaxNo;
    this.LicensedFor = LicensedFor;
    this.CreatedBy = CreatedBy;
    this.CreatedDate = CreatedDate;
    this.RecordStatus = RecordStatus;
}

function terminaloperatorberth(TerminalOperatorID, PortCode, QuayCode, BerthCode, CreatedDate, CreatedBy, RecordStatus) {
    this.TerminalOperatorID = TerminalOperatorID;
    this.PortCode = PortCode;
    this.QuayCode = QuayCode;
    this.BerthCode = BerthCode;
    this.CreatedDate = CreatedDate;
    this.CreatedBy = CreatedBy;
    this.RecordStatus = RecordStatus;
}

function terminaloperatorcargohandling(TerminalOperatorID, SubCatCode, CreatedDate, CreatedBy, RecordStatus) {
    this.TerminalOperatorID = TerminalOperatorID;
    this.CargoTypeCode = SubCatCode;
    this.CreatedDate = CreatedDate;
    this.CreatedBy = CreatedBy;
    this.RecordStatus = RecordStatus;
}

ChangeEvent = function (data, event) {
    if (data.RegisteredName().length == 0 || data.TradingName().length == 0 || data.RegistrationDate().length == 0 || data.PremiseLocation().length == 0 || data.TelephoneNo1().length == 0 || data.FaxNo().length == 0 || data.NumStreet().length == 0 || data.Suburb().length == 0 || data.TownCity().length == 0 || data.PostalCode().length == 0 || data.NumStreet2().length == 0 || data.Suburb2().length == 0 || data.TownCity2().length == 0 || data.PostalCode2().length == 0)
    { self.IsClicks(false); }
    else
    { self.IsClicks(true); }
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}