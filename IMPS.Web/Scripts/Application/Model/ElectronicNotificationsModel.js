(function (ipmsRoot) {
    var ElectronicNotificationsModel = function (data) {
        var self = this;
        self.NotificationTemplateCode = ko.observable("").extend({ required: { message: '* This field is required' } });
        self.NotificationTemplateName = ko.observable("").extend({ required: { message: '* This field is required' } });
        self.EntityID = ko.observable()
        self.EntityName = ko.observable("");
        self.Tokens = ko.observable("");
        self.RecordStatus = ko.observable("");
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
        self.SelectedRolenames = ko.observableArray();
        self.SelectedPortNames = ko.observableArray();
        self.SelectedTokens = ko.observableArray();

        self.SelectedTokensEmail = ko.observable("");
        self.SelectedTokensSMS = ko.observable();
        self.SelectedTokensSysMessage = ko.observable();

        self.WorkflowTaskCode = ko.observable('0');
        self.RoleID = ko.observable();
        self.RoleName = ko.observable();
        self.PortCode = ko.observable();
        self.PortName = ko.observable();
        self.Roles = ko.observableArray();
        self.Ports = ko.observableArray();
        self.NotificationPort = ko.observableArray();
        self.NotificationRole = ko.observableArray();
        self.EmailSubject = ko.observable();
        self.IsEmail = ko.observable();
        self.IsEmail.subscribe(function (checked) {
            if (checked)
                $('#emaildisplay').css('display', 'block');
            else
                $('#emaildisplay').css('display', 'none');
        });
        self.IsHighPriority = ko.observable(0);
        self.EmailTemplate = ko.observableArray();
        self.IsSMS = ko.observable(0);
        self.IsSMS.subscribe(function (checked) {
            if (checked)
                $('#smsdisplay').css('display', 'block');
            else
                $('#smsdisplay').css('display', 'none');
        });
        self.SMSTemplate = ko.observable();
        self.IsSysMessage = ko.observable(0);
        self.IsSysMessage.subscribe(function (checked) {
            if (checked)
                $('#sndisplay').css('display', 'block');
            else
                $('#sndisplay').css('display', 'none');
        });
        self.SysMessageTemplate = ko.observable();
        self.NotificationTemplateBase = ko.observable('R').extend({ required: { message: '* NotificationTemplateBase is required' } });
        self.NotificationTemplateBase.subscribe(function (checked) {
            if (checked == 'R')
                $('#roledisplay').css('display', 'block');
            else
                $('#roledisplay').css('display', 'none');
        });
        self.NotificationTemplateCodeSort;
        self.NotificationTemplateCode.subscribe(function (value) {
            self.NotificationTemplateCodeSort = value;
        });
        self.NotificationTemplateNameSort;
        self.NotificationTemplateName.subscribe(function (value) {
            self.NotificationTemplateNameSort = value;
        });
        self.EntityNameSort;
        self.EntityName.subscribe(function (value) {
            self.EntityNameSort = value;
        });
        self.cache = function () { };
        self.set(data);

        self.NotificationRoles = ko.observableArray([]);
        self.NotificationPorts = ko.observableArray([]);
    }
    ipmsRoot.ElectronicNotificationsModel = ElectronicNotificationsModel;
}(window.IPMSROOT));

IPMSROOT.ElectronicNotificationsModel.prototype.set = function (data) {
    var self = this;
    self.NotificationTemplateCode(data ? (data.NotificationTemplateCode || "") : "");
    self.NotificationTemplateName(data ? (data.NotificationTemplateName || "") : "");
    self.EntityID(data ? (data.EntityID || null) : null);
    self.EntityName(data ? (data.EntityName || "") : "");
    self.RoleID(data ? (data.RoleID || null) : null);
    self.RoleName(data ? (data.RoleName || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.WorkflowTaskCode(data ? (data.WorkflowTaskCode || "") : "");
    self.IsEmail(data ? (data.IsEmail || "") : "");
    self.EmailTemplate(data ? (data.EmailTemplate || "") : "");
    self.IsHighPriority(data ? (data.IsHighPriority || "") : "");
    self.IsSMS(data ? (data.IsSMS || "") : "");
    self.SMSTemplate(data ? (data.SMSTemplate || "") : "");
    self.IsSysMessage(data ? (data.IsSysMessage || "") : "");
    self.NotificationTemplateBase(data ? (data.NotificationTemplateBase || "") : "R");
    self.SysMessageTemplate(data ? (data.SysMessageTemplate || "") : "");
    self.Roles(data ? (data.Roles || "") : "");
    self.Ports(data ? (data.Ports || "") : "");
    self.Tokens(data ? (data.Tokens || "") : "");
    self.EmailSubject(data ? (data.EmailSubject || "") : "");

    self.NotificationRole(data ? (data.Roles || "") : "");


    var length = (data ? (data.Roles.length || null) : null);
    var temp = [];

    for (i = 0; i < length; i++) {
        temp.push(data.Roles[i].RoleID);
    }
    self.SelectedRolenames(temp ? (temp || "") : "");

    self.NotificationPort(data ? (data.Ports || "") : "");

    var portLength = (data ? (data.Ports.length || null) : null);
    var ports = [];
    for (i = 0; i < portLength; i++) {
        ports.push(data.Ports[i].PortCode);
    }
    self.SelectedPortNames(ports ? (ports || "") : "");

    self.cache.latestData = data;
}

IPMSROOT.ElectronicNotificationsModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
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




