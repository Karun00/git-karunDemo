(function (ipmsRoot) {

    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });
   
    //NewsModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var NewsModel = function (data) {
        var self = this;
        self.validationEnabled = ko.observable(false);
        self.NewsID = ko.observable("");
        self.Title = ko.observable("").extend({ required: true });
        self.NewsContent = ko.observable("").extend({ required: true });
        self.PortData = ko.observableArray([]);

        self.NewsPortDtls = ko.observableArray([]);
        //self.NewsPorts = ko.observableArray([]);
        //self.NewsPort = ko.observableArray([]);
        //Old
        //var regExpressionOfURL = /^\s*www\.[a-z\d\-]{1,255}\.[a-z]{2,6}\s*$/;

        //Added by Santosh for accepting / and all other URL related keys
        var regExpressionOfURL = /^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/[^\s]*)?$/i;

        self.NewsUrl = ko.observable("");
        self.StartDate = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.EndDate = ko.observable("").extend({ required: { onlyIf: self.validationEnabled, message: '* This field is required.' } });
        self.RecordStatus = ko.observable("");
        self.CreatedBy = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.ModifiedBy = ko.observable("");
        self.ModifiedDate = ko.observable("");
        self.NewsPort = ko.observableArray([]);
        //self.PortCode = ko.observable("");
        self.PortSelected = ko.observableArray([]);
        self.Statust = ko.computed(function () {
            return self.RecordStatus() == 'A' ? "Active" : "Inactive";
        });
       

        self.TitleSort;
        self.Title.subscribe(function (value) {
            self.TitleSort = value;
        });

        self.NewsContentSort;
        self.NewsContent.subscribe(function (value) {
            self.NewsContentSort = value;
        });

        self.StartDateSort;
        self.StartDate.subscribe(function (value) {
            self.StartDateSort = value;
        });

        self.EndDateSort;
        self.EndDate.subscribe(function (value) {
            self.EndDateSort = value;
        });
   

        self.cache = function () { };
        self.set(data);
    };
    ipmsRoot.NewsModel = NewsModel;
    //ipmsRoot.PortReferenceData = PortReferenceData;
}(window.IPMSROOT));

//NewsModel Fills from viewmodel to model and set data in the controls and send data to view and Validates cshtml Controls data
var DateFormat = 'YYYY-MM-DD HH:mm';

//var Port = function (data) {
//    var self = this;
//    self.PortCode = ko.observable(data ? data.PortCode : "");
//    self.Selected = ko.observable(false);
//}


IPMSROOT.NewsModel.prototype.set = function (data) {
    var self = this;
    self.NewsID(data ? (data.NewsID || "") : "");
    self.Title(data ? (data.Title || "") : "");
    self.NewsUrl(data ? (data.NewsUrl || "") : "");
    self.NewsContent(data ? (data.NewsContent || "") : "");
    self.StartDate(data ? (moment(data.StartDate).format(DateFormat) || null) : null);
    self.EndDate(data ? (moment(data.EndDate).format(DateFormat) || null) : null);
    self.RecordStatus(data ? (data.RecordStatus || "A") : "A");
    self.CreatedBy(data ? (data.CreatedBy == 'NULL' ? "" : data.CreatedBy || "") : "");
    self.CreatedDate(data ? (data.CreatedDate == 'NULL' ? "" : data.CreatedDate || "") : "");
    self.cache.latestData = data;
    //self.PortCode(data ? (data.PortCode == 'NULL' ? "" : data.PortCode || "") : "");
    self.NewsPort(data ? (data.NewsPort || "") : "");
    //self.AgentPorts(data ? (data.AgentPorts || "") : "");
}

// Reset News Model Data
IPMSROOT.NewsModel.prototype.reset = function () {
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

//Accept Alpha numeric
function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9]/;
    return charcheck.test(keychar);
}

//Accept valid Urls
function ValidateURL(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /[0-9a-zA-Z.]/;
    return charcheck.test(keychar);
}

//Accept valid dates
function ValidateDate(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[]$/;
    return charcheck.test(keychar);
}