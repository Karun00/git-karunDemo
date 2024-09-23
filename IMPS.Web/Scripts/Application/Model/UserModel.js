(function (ipmsRoot) {

    //UserModel Fills from viewmodel to model and send data to view and Validates cshtml Controls data
    var UserModel = function (data) {
        var selfUser = this;
        selfUser.UserID = ko.observable();
        selfUser.applicant = ko.observableArray();
        selfUser.SubCatCode = ko.observable();
        selfUser.SubCatName = ko.observable();
        selfUser.UserTypeID = ko.observable();
        selfUser.Name = ko.observable(); 
        selfUser.UserType = ko.observable();
        selfUser.FirstName = ko.observable("").extend({ required: { message: '* This field is required.' } });
        selfUser.LastName = ko.observable();
        selfUser.UserName = ko.observable("").extend({ required: { message: '* This field is required.' } });
        selfUser.Designation = ko.observable();
        selfUser.Roles = ko.observableArray();
        selfUser.viewRole = ko.observableArray();
        selfUser.viewPorts = ko.observableArray();
        selfUser.UserPorts = ko.observableArray();
        selfUser.RoleName = ko.observable();
        selfUser.RecordStatus = ko.observable('A');
        selfUser.DormantStatus = ko.observable('N');
        selfUser.RecordStatust = ko.computed(function () {
            return selfUser.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        selfUser.CreatedBy = ko.observable();
        selfUser.CreatedDate = ko.observable();
        // selfUser.ReferenceNo = ko.observable();
        selfUser.ReferenceNo = ko.observable("");
        selfUser.EmailID = ko.observable("").extend({ required: { message: '* This field is required.' }, email: { message: '* Please enter valid Email' } });
        selfUser.ContactNo = ko.observable("").extend({ required: { message: '* This field is required.' } });
        selfUser.UserRoles = ko.observableArray([]);
        selfUser.Roles1 = ko.observableArray();
        selfUser.AnonymousUserYn = ko.observable();
        selfUser.PWD = ko.observable();
        selfUser.IsFirstTimeLogin = ko.observable();
        selfUser.PwdExpirtyDate = ko.observable();
        selfUser.IncorrectLogins = ko.observable();
        selfUser.LoginTime = ko.observable();
        selfUser.usertypeSearch = ko.observable();
        selfUser.userNameSearch = ko.observable();
        selfUser.WorkflowInstanceId = ko.observable();
        selfUser.ReferenceNoSearch = ko.observable();
        self.WorkFlowRemarks = ko.observable();
        selfUser.ReasonForAccess = ko.observable();
        selfUser.ValidFromDate = ko.observable('').extend({ required: true });
        selfUser.ValidToDate = ko.observable('').extend({ required: true });
        selfUser.PortNames = ko.observableArray();
        ////////////////KendoUI Grid Sorting/////////
        selfUser.NameSort;
        selfUser.Name.subscribe(function (value) {
            selfUser.NameSort = value;
        });
        selfUser.SubCatNameSort;
        selfUser.SubCatName.subscribe(function (value) {
            selfUser.SubCatNameSort = value;
        });
        selfUser.UserNameSort;
        selfUser.UserName.subscribe(function (value) {
            selfUser.UserNameSort = value;
        });
        selfUser.ReferenceNoSort;
        selfUser.ReferenceNo.subscribe(function (value) {
            selfUser.ReferenceNoSort = value;
        });
        selfUser.StatusSort;
        selfUser.RecordStatust.subscribe(function (value) {
            selfUser.StatusSort = value;
        });
        selfUser.FirstNameSortable;
        selfUser.FirstName.subscribe(function (value) {
            selfUser.FirstNameSortable = value;
        });
        selfUser.LastNameSortable;
        selfUser.LastName.subscribe(function (value) {
            selfUser.LastNameSortable = value;
        });
        ///////////////////////////////////////////////////
        //add pending task - Srini
        selfUser.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task);
        }) : []);
        selfUser.cache = function () { };
        selfUser.set(data);
    }

    // Added by Srini
    var pendingTask = function (data) {
        var selfUser = this;
        selfUser.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        selfUser.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        selfUser.Remarks = ko.observable(data ? data.Remarks : "").extend({ required: { message: '* Enter Remarks' } });
        selfUser.TaskCode = ko.observable(data ? data.TaskCode : "");
        selfUser.APIUrl = ko.observable(data ? data.APIUrl : "");
        selfUser.TaskName = ko.observable(data ? data.TaskName : "");
        selfUser.TaskDescription = ko.observable(data ? data.TaskDescription : "");
        selfUser.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        selfUser.HasRemarks = ko.observable(data ? data.HasRemarks : "");
    }

    var Role = function (data) {
        var selfUser = this;
        selfUser.RoleID = ko.observable(data ? data.RoleID : "");
        selfUser.RoleName = ko.observable(data ? data.RoleName : "");
    }

    IPMSROOT.pendingTask = pendingTask;
    ipmsRoot.UserModel = UserModel;
    ipmsRoot.Role = Role;

}(window.IPMSROOT));

//UserModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
IPMSROOT.UserModel.prototype.set = function (data) {
    var selfUser = this;
    selfUser.UserID(data ? (data.UserID) || 0 : 0);
    selfUser.ReferenceNo(data ? (data.ReferenceNo || "") : "");
    selfUser.FirstName(data ? (data.FirstName || "") : "");
    selfUser.LastName(data ? (data.LastName || "") : "");
    selfUser.Designation(data ? (data.Designation || "") : "");
    selfUser.Name(data ? (data.Name || "") : null);
    selfUser.UserTypeID(data ? (data.UserTypeID || null) : null);
    selfUser.UserName(data ? ((data.UserName != "" ? data.UserName : "") || "") : "");
    selfUser.ContactNo(data ? (data.ContactNo || "") : "");
    selfUser.EmailID(data ? (data.EmailID || "") : "");
    selfUser.RoleName(data ? (data.RoleName || "") : "");
    selfUser.Roles1(data ? (data.Roles || null) : null);
    selfUser.RecordStatus(data ? (data.RecordStatus || "") : "A");
    selfUser.AnonymousUserYn(data ? (data.AnonymousUserYn || "") : "");
    selfUser.PWD(data ? data.PWD : "");
    selfUser.IsFirstTimeLogin(data ? data.IsFirstTimeLogin : "");
    selfUser.PwdExpirtyDate(data ? data.PwdExpirtyDate : "");
    selfUser.IncorrectLogins(data ? data.IncorrectLogins : 0);
    selfUser.LoginTime(data ? data.LoginTime : "");
    selfUser.WorkflowInstanceId(data ? data.WorkflowInstanceId : "");
    selfUser.ReasonForAccess(data ? data.ReasonForAccess : "");
    selfUser.ValidFromDate(data ? moment(data.ValidFromDate).format('YYYY-MM-DD') : "");
    selfUser.ValidToDate(data ? moment(data.ValidToDate).format('YYYY-MM-DD') : "");
    selfUser.CreatedBy(data ? (data.CreatedBy) || '' : '');
    selfUser.CreatedDate(data ? (data.CreatedDate) || "" : "");
    selfUser.PortNames(data ? (data.PortNames) || "" : "");
    selfUser.UserPorts(data ? (data.UserPorts) || "" : "");
    var length = (data ? (data.Roles.length || null) : null);
    var temp = [];
    for (i = 0; i < length; i++) {
        temp.push(data.Roles[i].RoleID);
    }
    selfUser.viewRole(temp ? (temp || "") : "");
    var porttemp = [];
    var portlength = (data ? (data.UserPorts.length || null) : null);
    for (i = 0; i < portlength; i++) {
        porttemp.push(data.UserPorts[i].PortCode);
    }
    selfUser.viewPorts(porttemp ? (porttemp || "") : "");
    selfUser.SubCatCode(data ? (data.SubCatCode || null) : null);
    selfUser.SubCatName(data ? (data.SubCatName || "") : "");
    selfUser.DormantStatus(data ? data.DormantStatus : "N");
    selfUser.cache.latestData = data;
}

IPMSROOT.UserModel.prototype.reset = function () {
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