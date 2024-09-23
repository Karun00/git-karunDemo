ko.validation.configure({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
});
$('input').bind("paste", function (e) {
    e.preventDefault();
});
var portId = parseInt($('#spanPortId').text());
var isView = parseInt($('#spanIsView').text());
var portViewModel = function () {
    var self = this;
    self.PortCode = ko.observable();
    self.PortName = ko.observable();
    self.InternationalCharacter = ko.observable();
    self.GeographicLocation = ko.observable();
    self.ContactNo = ko.observable();
    self.Email = ko.observable();
    self.Fax = ko.observable();
    self.Website = ko.observable();
    self.Description = ko.observable();
    self.Status = ko.observableArray([{ name: "Active",val:"A" },{name:"Inactive",val:"I"}]);
    self.CreatedBy = ko.observable('1');
    self.CreatedDate = GetDateTime();
    self.selectStatus = ko.observable();
   
    var PortData = {
        PortCode: self.PortCode,
        PortName: self.PortName,
        InternationalCharacter: self.InternationalCharacter,
        GeographicLocation: self.GeographicLocation,
        ContactNo: self.ContactNo,
        Email: self.Email,
        Fax: self.Fax,
        Website: self.Website,
        Description: self.Description,
        Status: self.selectStatus,
        CreatedBy:self.CreatedBy,
        CreatedDate:self.CreatedDate,
    };
    self.portdata = ko.observableArray([]);
    self.portdata().errors = ko.validation.group(self.portdata());
    self.ResetPort = function () {
        PortData.PortCode(''),
        PortData.PortName(''),
        PortData.InternationalCharacter(''),
        PortData.GeographicLocation(''),
        PortData.ContactNo(''),
        PortData.Email(''),
        PortData.Fax(''),
        PortData.Website(''),
        PortData.Description('')
        PortData.Status('')
    },
    self.SavePort = function () {     
        if (self.portdata().length > 0) {
            self.portdata().errors.showAllMessages();
            return;
        }       
        PortData.PortID = portId;
        $.ajax({
            type: "POST",
            url: "/api/Ports/PostPortData",
            data: ko.toJSON(PortData),
            contentType: "application/json",
            success: function (data) {
            },
            error: function () {
                alert("Failed");
            }
        });
    }
}
var vm = new portViewModel();
ko.applyBindings(vm);

if (portId > 0) {
    PortDataAtId();
    if (isView>0) {
        $("#eventDiv").hide();
    } else {
        $("#btnSave").val("Update");
    }
  
}

function PortDataAtId()
{
    $.ajax({
        type: "get",
        url: "/api/Ports/GetPortDtl/" + portId,
        contentType: "application/json",
        success: function (data) {
            vm.PortCode(data.PortCode);
            vm.PortName(data.PortName);
            vm.InternationalCharacter(data.InternationalCharacter);
            vm.GeographicLocation(data.GeographicLocation);
            vm.ContactNo(data.ContactNo);
            vm.Email(data.Email);
            vm.Fax(data.Fax);
            vm.Description(data.Description);
            vm.Website(data.Website);
            vm.selectStatus(data.Status);
        },
        error: function () {
            alert("Failed");
        }
    });
}

function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
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

function ValidateAlphabetsWithSpaces(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z ]*$/;
    return charcheck.test(keychar);
}