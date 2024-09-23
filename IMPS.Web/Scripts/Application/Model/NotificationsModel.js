(function (ipmsRoot) {
    var NotificationModel = function (data) {
        var self = this;
        self.PortName = ko.observable();
        self.PortCode = ko.observable();
        self.selectedportcode = ko.observable();

        self.VCN = ko.observable();

        self.WorkflowTaskCode = ko.observable();
        self.EntityCode = ko.observable();
        self.PageUrl = ko.observable();
        self.EntityName = ko.observable(data ? data.EntityName : "");
        self.group = ko.observable();
        self.group.subscribe(function (value) {
            value.dataSource.group([{ field: "EntityName" }]);
        });

        self.UserID = ko.observable(data ? data.UserID : "");
        self.UserName = ko.observable(data ? data.UserName : "");
        self.ConversationID = ko.observable(data ? data.ConversationID : "");
        self.selectedChoice = ko.observable();
        self.NextStep = ko.observable();

        self.SubCatName = ko.observable(data ? data.SubCatName : "");


        self.TaskID = ko.observable(data ? data.TaskID : "");

        self.columnNames = ko.observableArray([]);
        self.EntityColumns = ko.observable(data ? data.EntityColumns : "");

        //Added for Diving Request Approval Userdefind Date & Time Format
        self.EntityCodeUDF = ko.observable(data ? data.EntityCode : "");

        if (data != undefined) {
            if (data.EntityColumns != null) {
                var column = data.EntityColumns.split(',');
                for (var i = 0; i < column.length; i++) {
                    self.columnNames.push({ Column: column[i] });
                }
            }
            self.columnNames.push({ Column: "Workflow Status" });

            self.columnNames.push({ Column: "Remarks" });
            self.columnNames.push({ Column: "Action" });
        }

        self.pendingTasks = ko.observableArray(data ? ko.utils.arrayMap(data.pendingTasks, function (task) {
            return new pendingTask(task, data.PageUrl, self.EntityColumns(), self.SubCatName(), data.RequestName, self.EntityCodeUDF());
        }) : []);


        //By mahesh for system notificationsw....
        self.NotificationText = ko.observable(data ? data.NotificationText : "");
        self.CreatedDate = ko.observable(data ? data.CreatedDate : "");



        /////////

        self.cache = function () { };
        self.set(data);

    }

    var pendingTask = function (data, objpage, entitycolumn, workflowstatus, requestname, valOfEntityCodeForDRA) {
        var strHash = document.location.pathname;
        var value = strHash.substring(strHash.lastIndexOf('/') + 1);

        var self = this;
        self.WorkflowInstanceId = ko.observable(data ? data.WorkflowInstanceId : "");
        self.ReferenceID = ko.observable(data ? data.ReferenceID : "");
        self.Remarks = ko.observable(data ? data.Remarks : "");
        self.PreviousRemarks = ko.observable(data ? data.PreviousRemarks : "");
        self.ReferenceData = ko.observable(data ? data.ReferenceData : "");
        self.Rows = ko.observableArray([]);

        self.HasRemarks = ko.observable(data ? data.HasRemarks : "");

        self.TaskCode = ko.observable(data ? data.TaskCode : "");
        self.APIUrl = ko.observable(data ? data.APIUrl : "");
        self.TaskName = ko.observable(data ? data.TaskName : "");
        self.TaskDescription = ko.observable(data ? data.TaskDescription : "");

        self.GetTask = ko.observableArray([]);

        self.DateTimeConfigFormat = ko.observable(data ? data.DateTimeConfigFormat : "");
        var dateformat = self.DateTimeConfigFormat().toUpperCase();

        //Added for Diving Request Approval Userdefind Date & Time Format
        var dateTimeformat = 'YYYY/MM/DD h:mm a';

        if (data != undefined) {
            actualObj = jQuery.parseJSON(data.ReferenceData);

            if (entitycolumn != null) {
                var column = entitycolumn.replace(/\s/g, '');
                column = column.split(',');

                $.each(column, function (k, v) {
                    $.each(actualObj, function (key, value) {

                        if (v == key) {
                            //Check Value is matches to Date Format - //TODO:Logic is incorrect to be fixed in differant way
                            //var regexDate = /^\w{2}[a-zA-Z]/g;
                            //if (!regexDate.test(value)) {

                            //    if (isValidDate(value) == true) {
                            //        var value1 = value;

                            //        //Added for Diving Request Approval Userdefind Date & Time Format
                            //        if (valOfEntityCodeForDRA == 'DIVREQ') {
                            //            value = moment(value).format(dateTimeformat);
                            //        }
                            //        //Datetime Format for all other datetime column values
                            //        else {
                            //            value = moment(value).format(dateformat);
                            //        }

                            //        if (value.match(/^\d\d\d\d\d\d\d\d/)) {
                            //            value = value;
                            //        }
                            //        else {
                            //            value = value1;
                            //        }
                            //    }
                            //}

                            self.Rows.push({ Row: (value.length > 20 ? value.substring(0, 20) + '...' : value) });
                        }
                    });
                });

                var rl = column.length - self.Rows().length;
                for (var i = 0; i < rl; i++) {
                    self.Rows.push({ Row: "" });
                }
            }

            self.ArrivalFlag = ko.observable(false);
            self.ArrivalFlag = ko.computed(function () {
                if (objpage == "ArrivalNotifications" && data.WorkflowTaskCode == 'VRES') {
                    return true;
                }
                else {
                    return false;
                }
            }, this);


            self.Rows.push({ Row: workflowstatus });
            self.Rows.push({ Row: "RowRemarks" });
            self.Rows.push({ Row: "RowAction", ReferenceID: data.ReferenceID, PageUrl: objpage, PreviousRemarks: data.PreviousRemarks, WorkflowInstanceId: data.WorkflowInstanceId, ArrivalFlag: self.ArrivalFlag });

            if (data.APIUrl != null) {
                var tc = data.TaskCode.split(',');
                var action = data.APIUrl.split(',');
                var taskname = data.TaskName.split(',');
                var hasremarks = data.HasRemarks.split(',');
                var taskdescription = data.TaskDescription.split(',');
                for (var i = 0; i < tc.length; i++) {
                    self.GetTask.push({ TaskCode: tc[i], TaskName: taskname[i], TaskDescription: taskdescription[i], APIUrl: action[i], PreviousRemarks: data.PreviousRemarks, Remarks: data.Remarks, WorkflowInstanceId: data.WorkflowInstanceId, ReferenceID: data.ReferenceID, RequestName: requestname, HasRemarks: hasremarks[i] });
                    //var url = "/" + objpage + "/" + data.ReferenceID
                    //if (value == data.ReferenceID && strHash == url)
                    //    self.GetViewList.push({ TaskCode: tc[i], TaskName: taskname[i], APIUrl: action[i], Remarks: data.Remarks, WorkflowInstanceId: data.WorkflowInstanceId, ReferenceID: data.ReferenceID });
                };
            }


        }


    }
    var Conversation = function (data) {
        var self = this;
        self.UserID2 = ko.observable(data ? data.UserID : "");
    }

    //Mobile conversation reply model
    var ConversationReply = function (data) {
        var self = this;
        self.ConversationID = ko.observable(data ? data.ConversationID : "");
        self.Reply = ko.observable(data ? data.Reply : "");
        self.ConversationReplyID = ko.observable(data ? data.ConversationReplyID : "");
        self.IsRead = ko.observable(data ? data.IsRead : "");
    }
    //var GetTask = function (data) {

    //    self.APIUrl = ko.observable(data ? data.APIUrl : "");
    //    self.TaskName = ko.observable(data ? data.TaskName : "");
    //    self.TaskID = ko.observable(data ? data.TaskID : "");
    //}


    ipmsRoot.NotificationModel = NotificationModel;
    ipmsRoot.pendingTask = pendingTask;
    ipmsRoot.Conversation = Conversation;
    ipmsRoot.ConversationReply = ConversationReply;

}(window.IPMSROOT));


IPMSROOT.NotificationModel.prototype.set = function (data) {
    var self = this;
    self.PortName(data ? (data.PortName || "") : null);
    self.PortCode(data ? (data.PortCode || "") : "");
    self.selectedportcode(data ? (data.selectedportcode || "") : "");
    //  self.ReferenceID(data ? (data.ReferenceID || "") : "");
    self.VCN(data ? (data.VCN || "") : "");
    // self.ReferenceData(data ? (data.ReferenceData || "") : ""); 
    self.WorkflowTaskCode(data ? (data.WorkflowTaskCode || "") : "");
    self.EntityCode(data ? (data.EntityCode || "") : "");
    self.NextStep(data ? (data.NextStep || "") : "");

    self.SubCatName(data ? (data.SubCatName || "") : null);
    self.NotificationText(data ? (data.NotificationText || "") : "");
    self.CreatedDate(data ? (data.CreatedDate || "") : "");
    self.TaskID(data ? (data.TaskID || "") : "");

    self.cache.latestData = data;
}




IPMSROOT.NotificationModel.prototype.reset = function () {
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


function ValidateAlphanumeric(data, event) {
    if (window.event) // IE
        keynum = event.keyCode;
    else if (event.which) // Netscape/Firefox/Opera
        keynum = event.which;
    keychar = String.fromCharCode(keynum);
    charcheck = /^[a-zA-Z0-9]/;
    return charcheck.test(keychar);
}

function isValidDate(dateString) {
    var value = false;

    // First check for the pattern
    if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dateString)) {
        value = true;
    }
    return value;
}






