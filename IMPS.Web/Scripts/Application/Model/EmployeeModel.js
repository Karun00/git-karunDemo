(function (ipmsRoot) {

    //EmployeeModel Fills from viewmodel to model and send data to view and Validates cshtml Controls data
    var EmployeeModel = function (data) {

        var self = this;
        self.EmployeeID = ko.observable();
        self.SAPNumber = ko.observable("").extend({ required: true});// { message: '* SAP Number is required' } });
        self.FirstName = ko.observable("").extend({ required: true}); //{ message: '* First Name is required' } });
        self.LastName = ko.observable("").extend({ required: true}); //{ message: '* Last Name is required' } });
        self.Initials = ko.observable();
        self.Name = ko.observable();
        self.BirthDate = ko.observable("").extend({ required: true}); //{ message: '* Birth Date is required' } });
        self.Age = ko.observable("");
        self.JoiningDate = ko.observable("").extend({ required: true}); //{ message: '* Date of Joining is required' } });
        self.YearsofService = ko.observable();
        self.OfficialMobileNo = ko.observable();//ko.observable("").extend({ required: true}); //{ message: '* Official Mobile No. is required' } });
        self.PersonalMobileNo = ko.observable();// ko.observable("").extend({ required: true}); //{ message: '* Personal Mobile No. is required' } });
        self.EmailID = ko.observable("").extend({ required: { message: '* This field is required' }, email: { message: '* Please enter a proper email address' }, });
        self.Gender = ko.observable();
        self.GenderCode = ko.observable("GENM").extend({ required: true}); //{ message: '* Gender is required' } });
        self.IDNo = ko.observable("").extend({ required: true}); //{ message: '* ID No. is required' } });
        self.Department = ko.observable();
        self.DepartmentCode = ko.observable();
        self.DepartmentName = ko.observable();
        self.Designation = ko.observable();
        self.DesignationCode = ko.observable();
        self.DesignationName = ko.observable();
        self.BusinessUnit = ko.observable();
        self.BusinessUnitCode = ko.observable();
        self.BusinessUnitName = ko.observable();
        self.CostCenter = ko.observable();
        self.CostCenterCode = ko.observable();
        self.CostCenterName = ko.observable();
        self.PayrollArea = ko.observable();
        self.PayrollAreaCode = ko.observable();
        self.PayrollAreaName = ko.observable();
        self.PSGroup = ko.observable();
        self.PSGroupCode = ko.observable();
        self.PSGroupName = ko.observable();
        self.PersonalSubArea = ko.observable();
        self.PersonalSubAreaCode = ko.observable();
        self.PersonalSubAreaName = ko.observable();
        self.OrganizationalUnit = ko.observable();
        self.OrganizationalUnitCode = ko.observable();
        self.OrganizationalUnitName = ko.observable();
        self.RecordStatus = ko.observable('A');
        self.PortCode = ko.observable("");
        self.RecordStatust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        self.CreatedBy = ko.observable('1');
        self.CreatedDate = ko.observable(GetDateTime());
        self.SubCatCode = ko.observable();
        self.SubCatName = ko.observable();
        self.DeadWeightTonnage = ko.observable();

        self.searchText = ko.observable();
        

        ////////////////KendoUI Grid Sorting/////////
        self.NameSort;
        self.Name.subscribe(function (value) {
            self.NameSort = value;
        });
        self.DepartmentNameSort;
        self.DepartmentName.subscribe(function (value) {
            self.DepartmentNameSort = value;
        });
        self.BusinessUnitNameSort;
        self.BusinessUnitName.subscribe(function (value) {
            self.BusinessUnitNameSort = value;
        });
        self.IDNoSort;
        self.IDNo.subscribe(function (value) {
            self.IDNoSort = value;
        });


        self.StatusSort;
        self.RecordStatust.subscribe(function (value) {
            self.StatusSort = value;
        });


        ///////////////////////////////////////////////////

        self.cache = function () { };
        self.set(data);

    }

    ipmsRoot.EmployeeModel = EmployeeModel;

}(window.IPMSROOT));


ko.validation.init({
    registerExtenders: true,
    messagesOnModified: true,
    decorateElement: true
});

//EmployeeModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.EmployeeModel.prototype.set = function (data) {
    var self = this;
    self.EmployeeID(data ? (data.EmployeeID) || 0 : 0);
    self.SAPNumber(data ? (data.SAPNumber || "") : "");
    self.FirstName(data ? (data.FirstName || "") : "");
    self.LastName(data ? (data.LastName || "") : "");
    self.Initials(data ? (data.Initials || "") : "");
    self.Name(data ? (data.Name || "") : "");
    self.BirthDate(data ? (data.BirthDate || "") : "");
    self.Age(data ? (data.Age || "") : "");
    self.JoiningDate(data ? (data.JoiningDate || "") : "");
    self.YearsofService(data ? (data.YearsofService || "") : "");
    self.OfficialMobileNo(data ? (data.OfficialMobileNo || "") : "");
    self.PersonalMobileNo(data ? (data.PersonalMobileNo || "") : "");
    self.EmailID(data ? (data.EmailID || "") : "");
    self.Gender(data ? (data.Gender || "") : "");
    self.GenderCode(data ? (data.GenderCode || "") : "GENM");
    self.IDNo(data ? (data.IDNo || "") : "");
    self.Department(data ? (data.Department || "") : "");
    self.DepartmentCode(data ? (data.DepartmentCode || null) : null);
    self.DepartmentName(data ? (data.DepartmentName || "") : "");
    self.Designation(data ? (data.Designation || "") : "");
    self.DesignationCode(data ? (data.DesignationCode || null) : null);
    self.DesignationName(data ? (data.DesignationName || "") : "");
    self.BusinessUnit(data ? (data.BusinessUnit || "") : "");
    self.BusinessUnitCode(data ? (data.BusinessUnitCode || null) : null);
    self.BusinessUnitName(data ? (data.BusinessUnitName || "") : "");
    self.CostCenter(data ? (data.CostCenter || "") : "");
    self.CostCenterCode(data ? (data.CostCenterCode || null) : null);
    self.CostCenterName(data ? (data.CostCenterName || "") : "");
    self.PayrollArea(data ? (data.PayrollArea || "") : "");
    self.PayrollAreaCode(data ? (data.PayrollAreaCode || null) : null);
    self.PayrollAreaName(data ? (data.PayrollAreaName || "") : "");
    self.PSGroup(data ? (data.PSGroup || "") : "");
    self.PSGroupCode(data ? (data.PSGroupCode || null) : null);
    self.PSGroupName(data ? (data.PSGroupName || "") : "");
    self.PersonalSubArea(data ? (data.PersonalSubArea || "") : "");
    self.PersonalSubAreaCode(data ? (data.PersonalSubAreaCode || null) : null);
    self.PersonalSubAreaName(data ? (data.PersonalSubAreaName || "") : "");
    self.OrganizationalUnit(data ? (data.OrganizationalUnit || "") : "");
    self.OrganizationalUnitCode(data ? (data.OrganizationalUnitCode || null) : null);
    self.OrganizationalUnitName(data ? (data.OrganizationalUnitName || "") : "");
    self.SubCatCode(data ? (data.SubCatCode || null) : null);
    self.SubCatName(data ? (data.SubCatName || "") : "");
    self.PortCode(data ? (data.PortCode || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.DeadWeightTonnage(data ? (data.DeadWeightTonnage || "") : "");
    
    
    self.cache.latestData = data;
}


IPMSROOT.EmployeeModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


//Accept only numeric 
function Validatenumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9]/;
    return charcheck.test(keychar);
}

//Getting Date time
function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}

//Accept only Alphabets and spaces
function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z ]*$/;
    return charcheck.test(keychar);
}

//Accept Alpha numeric
function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9@!#\$\^%&*()+=\-_~`\[\]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    return charcheck.test(keychar);
}

