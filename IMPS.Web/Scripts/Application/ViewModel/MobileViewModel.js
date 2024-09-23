
$(document).ready(function () {
    toastr.options.closeButton = true;
    toastr.options.positionClass = "toast-top-right";
    var MobileViewModel = function () {
        var self = this;
        self.schedulesList = ko.observableArray(['France', 'Germany', 'Spain']);
        self.NotificationsList = ko.observableArray();
        self.PlannedMovementsList = ko.observableArray();
        self.NewNotificationsList = ko.observableArray();
        self.FromBollards = ko.observableArray();
        self.ToBollards = ko.observableArray();
        self.selectedBerthKey = ko.observable("");
        self.selectedQuayKey = ko.observable("");
        self.Portdata = ko.observableArray();
        self.mobileBerthWithBollardsModel = ko.observable();
        self.fileSizeConfigValue = ko.observable();
        // Mobile chat
        self.getUserDetails = ko.observableArray();
        self.getConversationUserDetails = ko.observableArray();
        self.getConversations = ko.observableArray();
        self.getMessages = ko.observableArray();
        self.scheduledTasks = ko.observableArray();
        self.ScheduledTaskView = ko.observableArray([]);
        self.ScheduledTaskExecutionView = ko.observableArray();
        self.scheduledTaskExecutionModel = ko.observable();
        self.masterBerths = ko.observableArray([]);
        self.berthingSide = ko.observableArray([]);
        self.IncidentTypes = ko.observableArray([]);
        self.divingRequestDetails = ko.observableArray([]);
        self.TodayDateTime = ko.observable(new Date());
        self.isLoading = ko.observable(false);
        self.isSuccess = ko.observable(false);
        self.BerthsDetails = ko.observableArray([]);
        self.mobileIncidentReportModel = ko.observable(new MobileIncidentReportModel());
        self.searchVCNDtls = ko.observableArray();

        self.MenuItems = ko.observableArray([
                //{ "dataIconvalue": "inbox", "value": "#Notifications", "name": "Notifications", "clickevnt": "LoadNotifications" },
                { "dataIconvalue": "calendar", "value": "#Scheduled-Tasks", "name": "Scheduled Tasks", "clickevnt": "LoadTasks" },
                { "dataIconvalue": "ios7-edit", "value": "#Planned-Movements", "name": "Planned Movements", "clickevnt": "LoadPendingMovements" },
                { "dataIconvalue": "ios7-edit", "value": "#VCN-Status", "name": "VCN Status", "clickevnt": "LoadVCNSearchDtls" },
                { "dataIconvalue": "inbox", "value": "#Berth-Information", "name": "Berth Information", "clickevnt": "LoadBerthInformation" },
                { "dataIconvalue": "inbox", "value": "#MBerthPlanningGIS", "name": "M-Berth Planning GIS", "clickevnt": "VesselClick" },
                { "dataIconvalue": "tick", "value": "#Pending-Approvals", "name": "Pending Approvals", "clickevnt": "LoadPendingApprovals" },
                { "dataIconvalue": "paste", "value": "#Report-Incident", "name": "Report an Incident", "clickevnt": "LoadIncidentTypes" },
                { "dataIconvalue": "sent", "value": "#Mobile-Reports", "name": "Mobile Reports", "clickevnt": "LoadReports" },
                { "dataIconvalue": "megaphone", "value": "#Diving-Request", "name": "Diving Request", "clickevnt": "LoadDivingRequestData" },
                { "dataIconvalue": "sent", "value": "#Departure-Notice", "name": "Departure Notice", "clickevnt": "LoadDepartureNoticeData" },
                { "dataIconvalue": "sent", "value": "#Service-Request", "name": "Service Request", "clickevnt": "LoadServiceRequestData" },
                { "dataIconvalue": "ship", "value": "#All Ports", "name": "Ships in Ports", "clickevnt": "ViewPorts" }
                //{ "dataIconvalue": "sent", "value": "#Features", "name": "Features", "clickevnt": "LoadFeatures" },                
                //{ "dataIconvalue": "sent", "value": "#Pending-Approvals", "name": "Pending Approvals", "clickevnt": "LoadPendingApprovals" },

        ]);
        var selectedport = true;

        self.Initialize = function () {
            self.LoadPorts();

            if (window.location.hash) {
                if (window.location.hash == "#My-Schedule") {
                    self.LoadMySchedule();
                }
                else if (window.location.hash == "#Notifications") {
                    self.LoadNotifications();
                }
                else if (window.location.hash == "#All Ports") {

                }
                else if (window.location.hash == "#VCN-Status") {

                }
                else if (window.location.hash == "#Berth-Information") {
                    self.LoadBerthInformation();
                }

                else if (window.location.hash == "#MBerthPlanningGIS") {
                     self.VesselClick();
                    //self.NewClick();
                }

                else if (window.location.hash == "#Pending-Approvals") {
                    self.LoadPendingApprovals();
                }
                else if (window.location.hash == "#Scheduled-Tasks") {
                    self.LoadTasks();
                }
                else if (window.location.hash == "#Report-Incident") {
                    self.LoadIncidentTypes();
                }
                else if (window.location.hash == "#Planned-Movements") {
                    self.LoadPendingMovements();
                }
                else if (window.location.hash == "#Diving-Request") {
                    self.LoadDivingRequestData();
                }
                else if (window.location.hash == "#Departure-Notice") {

                }
                else if (window.location.hash == "#Service-Request") {

                }
                else
                    document.location.hash = "";
            }

            self.scheduledTaskExecutionModel(new ScheduledTaskExecutionModel());
            self.userAutoComplete();
            self.GetConversationUsers();
            //self.GetUsers();
            self.LoadMessages();
            self.LoadNewNotifications();
            self.GetFileSizeConfigValue();
            self.GetBerths();
        }

        self.GetBerths = function () {
            $.ajax({
                url: '/api/MobileScheduledTasks/GetBerths',
                dataType: 'json',
                success: function (result) {
                    self.BerthsDetails(result);
                }
            });
        }

        // To get the file size config value
        self.GetFileSizeConfigValue = function () {
            $.ajax({
                url: '/api/File/GetFileSizeConfigValue',
                dataType: 'json',
                success: function (result) {
                    self.fileSizeConfigValue(result);
                }
            });
        }

        self.MessagesClicked = function () {
            window.location.href = "#Messages";
            self.LoadMessages();
        }

        self.NotificationsClicked = function () {
            window.location.href = "#Notifications";
            self.LoadNotifications();
        }

        self.LoadNewNotifications = function (data) {
            self.isLoading(true);
            $.ajax({
                url: '/api/Mobile/GetNewNotifications',
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.NewNotificationsList(data);
                }
            });
        }

        self.LoadNotifications = function (data) {
            self.isLoading(true);
            $.ajax({
                url: '/api/Mobile/GetNotifications',
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.NotificationsList(data);
                }
            });
        }


        self.LoadPendingMovements = function () {
            self.isLoading(true);
            $.ajax({
                url: '/api/Mobile/GetPlannedMovements',
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.PlannedMovementsList(data);
                }
            });
        }

        self.LoadMySchedule = function (data) {
            self.schedulesList(['France', 'Germany', 'Spain', 'India', 'Srilanka']);
        }

        //////////////////////////////////////////////Menu Click Function Starts here
        //////////////////////////////////////////////Created By Omprakash K
        self.MenuClicked = function (data) {

            /*var fnstring = data.clickevnt;
            var fn = window["SampleViewModel."fnstring];
            
            // is object a function?
            if (typeof fn === "function") fn();*/
            switch (data.clickevnt) {
                case "LoadPendingApprovals":
                    self.LoadPendingApprovals(data);
                    window.location.href = "#Pending-Approvals";
                    break;
                case "LoadMySchedule":
                    self.LoadMySchedule(data);
                    window.location.href = "#My-Schedule";
                    break;
                case "LoadNotifications":
                    self.LoadNotifications(data);
                    window.location.href = "#Notifications";
                    break;
                case "ViewPorts":
                    var iframeelement = document.getElementById("allportsiframe");
                    iframeelement.setAttribute("src", "/AllPorts");
                    window.location.href = "#AllPorts";
                    break;
                case "LoadTasks":
                    self.LoadTasks(data);
                    window.location.href = "#Scheduled-Tasks";
                    break;
                case "LoadIncidentTypes":
                    self.LoadIncidentTypes();
                    window.location.href = "#Report-Incident";
                    break;
                case "LoadPendingMovements":
                    self.LoadPendingMovements();
                    window.location.href = "#Planned-Movements";
                    break;
                case "LoadBerthInformation":
                    self.LoadBerthInformation();
                    window.location.href = "#Berth-Information";
                    break;
                case "VesselClick":
                    self.VesselClick();
                    location.reload();
                    window.location.href = "#MBerthPlanningGIS";
                   break;
               
                case "LoadReports":
                    window.location.href = "#Mobile-Reports";
                    break;
                case "LoadDivingRequestData":
                    self.LoadDivingRequestData();
                    window.location.href = "#Diving-Request";
                    break;
                case "LoadDepartureNoticeData":
                    window.location.href = "/MDepartureNotice";
                    break;
                case "LoadServiceRequestData":
                    window.location.href = "/MServiceRequests";
                    break;
                case "LoadVCNSearchDtls":
                    window.location.href = "#VCN-Status";
                    break;
            }
            return true;
        }
       
        //////////////////////////////////////////////Menu Click Function Ends here

        //////////////////////////////////////////////Notifications Change the Status Once Click on provided Link Starts here
        //////////////////////////////////////////////Created By Omprakash K On 24th Sep 2014
        self.NotificationStatus = function (data) {
            self.isLoading(true);
            $.ajax({
                url: '/api/Mobile/PutNotificationsByID?' + data.NotificationId,
                data: data,
                dataType: 'json',
                type: 'Put',
                success: function (data) {
                    self.isLoading(false);
                    self.LoadNotifications(data);
                    self.LoadNewNotifications();
                }
            });
        }

        //////////////////////////////////////////////Notifications Change the Status Once Click on provided Link Ends here

        ///////////////Ports Created by Amala Gunda on 20th November 2014
       
        var decryptresult = "";
        self.LoadPorts = function () {
            selectedport = true;
            var uname = $("#loginusername").text();
            $.ajax({
                url: '/api/Account/GetUserPorts/' + uname,
                dataType: 'json',
                success: function (result) {
                    self.Portdata(result);
                }
            });

            var inSb = $.cookie('Port');
            if (inSb != undefined) {

                //decrypted code
                for (var i = 0; i < inSb.length; i++) {

                    decryptresult += String.fromCharCode(inSb.charCodeAt(i) ^ 6);
                }
                selectedport = false;

                $.cookie('Port', inSb, { path: '/' });
                $("#port").val(decryptresult);
                $("#portidtest").text(decryptresult);
            }
        }

        self.SelectedPort = function () {
            var singleValues = $("#port").val();
            if (selectedport == true) {

                var inSb = $.cookie('Port');
                if (inSb != undefined) {
                    $.confirm({
                        'title': ' Port change confirmation',
                        'message': 'Are you sure want to change Port?',
                        'buttons': {
                            'Yes': {
                                'class': 'confirm-yes-btn',
                                'action': function () {
                                    var encryptresult = "";
                                    var xorKey = 6;

                                    //encrypted code
                                    for (i = 0; i < singleValues.length; ++i) {
                                        encryptresult += String.fromCharCode(xorKey ^ singleValues.charCodeAt(i));
                                    }

                                    $.cookie('Port', encryptresult, { path: '/' });

                                    window.location.href = document.location.pathname;
                                }
                            },
                            'No': {
                                'class': 'gray',
                                'action': function () {
                                    $("#port").val(decryptresult);
                                }
                            }
                        }
                    });
                }
                else {
                    var encryptresult = "";
                    var xorKey = 6;
                    //encrypted code
                    for (i = 0; i < singleValues.length; ++i) {
                        encryptresult += String.fromCharCode(xorKey ^ singleValues.charCodeAt(i));
                    }

                    $.cookie('Port', encryptresult, { path: '/' });
                }

            }
            else {

                var flag = true;
                var first = "";
                $.each(self.Portdata(), function (key, val) {
                    if (decryptresult == val.PortCode) {
                        flag = false;
                        first = val.PortCode;
                    }
                });
                var value_port = "";
                if (flag == false) {
                    $("#port").val(first);
                    value_port = first;
                }
                else {
                    $("#port").val(singleValues);
                    value_port = singleValues;
                }

                //encrypted code
                var encryptresult = "";
                var xorKey = 6;
                for (i = 0; i < value_port.length; ++i) {
                    encryptresult += String.fromCharCode(xorKey ^ value_port.charCodeAt(i));
                }

                $.cookie('Port', encryptresult, { path: '/' });

            }
            selectedport = true;
            var uname = $("#loginusername").text();
            $.ajax({
                url: '/api/Account/GetUserPorts/' + uname,
                dataType: 'json',
                success: function (result) {
                    self.LoadPendingTasks();
                    self.LoadNotifications();
                }
            });
        }


        ///////////End Ports

        ///// Report an incident starts here /////

        self.LoadIncidentTypes = function () {
            self.mobileIncidentReportModel(new MobileIncidentReportModel());
            $("#MobileIncidentUl").addClass("km-button1 km-list km-listview");
            $.ajax({
                url: '/api/MobileIncidentReporting/GetIncidentTypes',
                dataType: 'json',
                success: function (result) {
                    self.IncidentTypes(ko.utils.arrayMap(result, function (item) {
                        return new IncidentType(item);
                    }));
                }
            });
        }


        // To save the incident details
        self.SaveIncident = function (model) {
            self.MIRValidation = ko.observable(model);
            self.MIRValidation().errors = ko.validation.group(self.MIRValidation());
            var errors = self.MIRValidation().errors().length;
            if (errors == 0) {
                checkboxes = document.getElementsByName('IncidentTypes');
                var chk = 0;
                for (var i = 0, n = checkboxes.length; i < n; i++) {
                    if (checkboxes[i].checked) {
                        chk = 1;
                    }
                }
                if (chk == 1) {
                    self.isLoading(true);
                    $.ajax({
                        type: "POST",
                        url: '/api/MobileIncidentReporting/PostIncidentData',
                        data: ko.toJSON(model),
                        contentType: 'application/json;charset=utf-8',
                        dataType: 'json',
                        success: function (data) {
                            self.isLoading(false);
                            window.location.href = "#Home";
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Incident Reporting details Saved successfully", "Incident Reporting");
                        }
                    });
                }
                else {
                    toastr.warning("Please Select at least One Incident nature.");

                }
            }
            else {
                self.MIRValidation().errors.showAllMessages();
                return;
            }
        }

        // To upload a document and save it to the database.
        var uploadedFiles = [];
        var documentData = [];
        self.uploadFile = function () {
            //window.location.href = "#Report-Incident";

            uploadedFiles = self.mobileIncidentReportModel().UploadedFiles();
            var opmlFile = $('#fileToUpload')[0];
            if (opmlFile.files.length > 0) {
                for (var i = 0; i < opmlFile.files.length; i++) {

                    var match = ko.utils.arrayFirst(self.mobileIncidentReportModel().IncidentDocuments(), function (item) {
                        return item.FileName() === opmlFile.files[i].name;
                    });
                    if (match == null) {
                        self.isLoading(true);
                        var fileSizeInBytes = opmlFile.files[i].size;
                        var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                        if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                            var elem = {};
                            elem.FileName = opmlFile.files[i].name;
                            elem.FileSize = opmlFile.files[i].size;
                            elem.FileDetails = opmlFile.files[i];
                            elem.IsAlreadyExists = false
                            uploadedFiles.push(elem);
                            self.mobileIncidentReportModel().UploadedFiles(uploadedFiles);
                        }
                        else {
                            toastr.warning("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                            return;
                        }
                    }
                    else {
                        toastr.warning("The selected file already exist, Please upload another file", "Error");
                        return;
                    }
                }

                var formData = new FormData();

                ko.utils.arrayMap(self.mobileIncidentReportModel().UploadedFiles(), function (item) {
                    formData.append(item.FileName, item.FileDetails);
                });

                $.ajax({
                    type: 'Post',
                    url: '/api/File/MultipleFileUpload?documentType=Doc1',
                    data: formData,
                    cache: false,
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    success: function (data) {
                        self.isLoading(false);
                        ko.utils.arrayMap(data, function (item) {
                            var Adddoc = new IncidentDocumnet();
                            Adddoc.DocumentID(item.DocumentID);
                            Adddoc.FileName(item.FileName);
                            self.mobileIncidentReportModel().IncidentDocuments.push(Adddoc);
                        });
                    }
                });
            } else {
                toastr.warning("Please select image", "Error");
            }

            self.mobileIncidentReportModel().UploadedFiles([]);
            $('#fileToUpload').val('');
            return;
        }

        self.DeleteDocument = function (documentRow) {
            self.mobileIncidentReportModel().IncidentDocuments.remove(documentRow);
        }

        //// Report an incident ends here /////



        //////////////////////////////////////////////For Berth Information Starts Here
        self.VMmobileBerthModel = ko.observable();
        self.LoadBerthInformation = function () {
            self.isLoading(true);
            self.mobileBerthModel = ko.observable();
            self.masterQuays = ko.observableArray([]);
            $.ajax({
                url: '/api/Mobile/GetQuaynames',
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.masterQuays(ko.utils.arrayMap(data, function (item) {
                        return new Quay(item);
                    }));
                    self.VMmobileBerthModel(new MobileBerthModel(self.masterQuays()));
                }
            });
        }


        self.VesselClick = function () {
            self.isLoading(true);
            

        }

        
        //////////////////////////////////////////////For Berth Information ends Here

        /////Pending Approvals starts here
        self.LoadPendingApprovals = function (data) {
            self.LoadPendingTasks();
        }
        ////////////////////////////////////////////////////////////////////////////////////////

        self.VMnotificationModel = ko.observable(new NotificationModel());

        self.GetViewList = ko.observableArray();

        self.EntityData = ko.observableArray([]);

        self.NumberOfNotification = ko.observableArray([]);

        self.LoadPendingTasks = function () {
            self.isLoading(true);
            var strHash = document.location.pathname;

            var value = strHash.substring(strHash.lastIndexOf('/') + 1);

            $.ajax({
                type: "Get",
                url: '/api/Account/GetPendingTasks',
                data: null,
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    self.isLoading(false);
                    self.EntityData(ko.utils.arrayMap(result, function (item) {
                        return new NotificationModel(item);
                    }));
                    var number = 0;
                    ko.utils.arrayForEach(result, function (val1) {
                        ko.utils.arrayForEach(val1.pendingTasks, function (val) {

                            self.NumberOfNotification.push(val);
                            var url = "/" + val1.PageUrl + "/" + val.ReferenceID

                            if (val.APIUrl != null) {
                                var tc = val.TaskCode.split(',');
                                var action = val.APIUrl.split(',');
                                var taskname = val.TaskName.split(',');
                                var hasremarks = val.HasRemarks.split(',');
                                for (var i = 0; i < tc.length; i++) {
                                    if (value == val.ReferenceID && strHash == url)
                                        self.GetViewList.push({ TaskCode: tc[i], TaskName: taskname[i], APIUrl: action[i], ReferenceID: val.ReferenceID, Remarks: val.Remarks, WorkflowInstanceId: val.WorkflowInstanceId, PageUrl: val1.PageUrl, HasRemarks: val.hasremarks[i] });
                                };
                            }
                        });
                    });
                }
            });
        }

        self.Actiontask = function (model) {

            if (typeof String.prototype.startsWith != 'function') {
                // see below for better implementation!
                String.prototype.startsWith = function (str) {
                    return this.indexOf(str) == 0;
                };
            }
            var strHash = document.location.pathname;
            var value = strHash.substring(strHash.lastIndexOf('/') + 1);
            var id = '#' + model.WorkflowInstanceId;
            var remarks = $(id).val();
            var mobileUrl = null;
            var tempRemarks = model.Remarks;
            var message = model.RequestName + " " + model.TaskName;
            var url = "/" + model.PageUrl + "/" + model.ReferenceID
            if (value == model.ReferenceID && strHash == url)
                model.Remarks = tempRemarks;
            else
                model.Remarks = remarks;

            var spanid = '#span' + model.WorkflowInstanceId;
            // if ((tempRemarks == "" || tempRemarks == null) && (model.Remarks == "" || model.Remarks == null)) {
            if ((model.Remarks == "" || model.Remarks.length == null) && model.HasRemarks == "Y") {
                $(spanid).text('Please enter ' + model.TaskName + ' Remarks');
                $(spanid).addClass("validationError1");
                return;
            }
                //else if (model.Remarks == "" || model.Remarks == null) {
                //    $(spanid).text('* Remarks is required');
                //    $(spanid).addClass("validationError1");

                //}
            else {
                $(spanid).text(' ');
                $(spanid).removeClass("validationError1");


                if (model.APIUrl.startsWith('/')) {
                    mobileUrl = model.APIUrl;
                }
                else {
                    mobileUrl = '/' + model.APIUrl;
                }

                // confirmation box - start
                $.confirm({
                    'title': model.RequestName + ' Confirmation',
                    'message': 'Do you want to proceed with ' + model.TaskName + '?',
                    'buttons': {
                        'Yes': {
                            'class': 'confirm-yes-btn',
                            'action': function () {
                                self.isLoading(true);
                                $.ajax({
                                    type: "POST",
                                    url: mobileUrl,
                                    data: ko.mapping.toJSON(model),
                                    contentType: 'application/json;charset=utf-8',
                                    dataType: 'json',
                                    statusCode: {
                                        201: function (data) {
                                            self.isLoading(false);
                                            toastr.options.closeButton = true;
                                            toastr.options.positionClass = "toast-top-right";
                                            toastr.success(message + " successfully", "Pending Task");

                                            var url = "/" + model.PageUrl + "/" + model.ReferenceID
                                            if (value == model.ReferenceID && strHash == url)
                                                window.location.href = "/" + model.PageUrl
                                            self.NumberOfNotification.removeAll();
                                            self.LoadPendingTasks();
                                        }
                                    }

                                });
                            }
                        },
                        'No': {
                            'class': 'gray',
                            'action':
                               function () { }
                        }
                    }
                });
                //confirmation box - end
            }
        }
        self.ResultVMnotificationModel = ko.observableArray();


        function CreateTableView(data) {
            var res = data.substring(2, data.length - 1);
            var array = res.split('","');
            var str = '<table>';
            var _key = '';
            var _value = '';

            str += '<tbody>';
            for (var i = 0; i < array.length; i++) {
                str += (i % 2 == 0) ? '<tr class="alt">' : '<tr>';

                for (var index in array[i]) {

                    _key = array[i].split('":"')[0];
                    _value = array[i].split('":"')[1];

                    if (index == 0)
                        str += '<td>' + _key + '</td>';
                    else if (index == 1) {
                        if (array[i][index] == null)
                            str += '<td> </td>';
                        else
                            str += '<td>' + _value + '</td>';
                    }
                }
                str += '</tr>';
            }
            str += '</tr>'
            str += '</tbody></table>';
            return str;
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        /////Pending Approvals ends here


        /// Mobile Chat Starts here

        // To get the list of users
        //self.GetUsers = function () {
        //    $.ajax({
        //        url: '/api/MobileConversation/GetUsers',
        //        dataType: 'json',
        //        success: function (data) {
        //            self.userAutoComplete(data);
        //        }
        //    });
        //}




        // To get the conversation users
        self.GetConversationUsers = function () {
            self.isLoading(true);
            $.ajax({
                url: '/api/MobileConversation/GetConversationUsers',
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.getConversationUserDetails(data);
                }
            });
        }

        // To get the list of new messages

        self.LoadMessages = function (data) {
            self.isLoading(true);
            $.ajax({
                url: '/api/MobileConversation/GetNewMessages',
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.getMessages(data);
                }
            });

        }

        self.userCancel = function (data) {
            window.location.href = "#Messages";
            self.LoadMessages();
        }

        // To add a conversation
        self.addUser = function (model) {

            var Conversation = {
                UserID2: $('#selectedUsrID').val()
            };

            $.ajax({
                type: "POST",
                url: '/api/MobileConversation/AddConversation',
                data: ko.toJSON(Conversation),
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    self.GetConversationUsers();
                    //self.GetUsers();
                    $('#autocomplete').val('').focus();
                }
            });
        }

        self.UserSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            $('#selectedUsrID').val(selecteddataItem.UserID);
        }

        // To send a data
        self.sendtext = function (model) {

            if ($('#message').val()) {
                var ConversationReply = {
                    ConversationID: $('#convID').text(),
                    Reply: $('#message').val()
                };

                $.ajax({
                    type: "POST",
                    url: '/api/MobileConversation/AddConversationReply',
                    data: ko.toJSON(ConversationReply),
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                    }
                });

                var chat = $.connection.chatHub;
                chat.client.broadcastMessage = function (UserName, Message) {
                    $('#discussion').append('<li style=color:#000;font-size:1.3em;font-weight:normal;line-height:3em;><strong>' + htmlEncode(UserName) + '</strong>: ' + htmlEncode(Message) + '&nbsp;&nbsp;&nbsp;&nbsp;<span>' + moment(new Date()).format('YYYY-MM-DD HH:mm') + '</span>' + '</li>');
                    $('#message').val('').focus();
                };

                $.connection.hub.start().done(function () {
                    chat.server.send($('#usrname').val(), $('#message').val());
                    $('#discussion').append('<li style=color:#000;font-size:1.3em;font-weight:normal;line-height:3em;><strong>' + htmlEncode($('#loginname').text()) + '</strong>: ' + htmlEncode($('#message').val()) + '&nbsp;&nbsp;&nbsp;&nbsp;<span>' + moment(new Date()).format('YYYY-MM-DD HH:mm') + '</span>' + '</li>');
                    $('#message').val('').focus();
                });

                function htmlEncode(value) {
                    var encodedValue = $('<div />').text(value).html();
                    return encodedValue;
                }
            }
        }

        // To get the list of conversation
        self.clickMe = function (data) {
            window.location.href = "#Conversations";
            $("#Conversations1").text(data.FirstName);
            $('#usrname').val(data.UserName);
            $("#discussion").text('');

            $.ajax({
                url: '/api/MobileConversation/GetConversations',
                data: { ConversationID: data.ConversationID, UserID: data.UserID },
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    self.getConversations(data);
                    $('#message').focus();
                }
            });
        }

        function htmlEncode(value) {
            var encodedValue = $('<div />').text(value).html();
            return encodedValue;
        }

        // To cancel a conversation
        self.cancel = function (data) {
            window.location.href = "#Messages";
            $("#discussion").text('');
            $.ajax({
                url: '/api/MobileConversation/GetConversationReply',
                data: { ConversationID: $('#convID').text() },
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    self.LoadMessages();
                }
            });

        }

        self.userAutoComplete = function () {
            $("#autocomplete").kendoAutoComplete({
                dataTextField: "FirstName",
                minLength: 1,
                dataSource: {
                    serverFiltering: true,
                    transport: {
                        read: { url: '/api/MobileConversation/GetUsers', dataType: 'json' }
                    }
                }, select: self.UserSelect
            });
        }

        //self.userAutoComplete = function (data) {
        //    $("#autocomplete").kendoAutoComplete({  
        //        dataSource: data,
        //        dataTextField: 'UserName',
        //        select: self.UserSelect
        //    });
        //}

        /// Mobile chat ends here

        /// Schedule task starts here

        /////Mobile VCN Search  ///////

        self.LoadVCNSearchDtls = function (model) {
            var VCN = $("#vcnid").val();
            //$.ajax({
            //    url: '/api/GetVCNStatusForMobApp/{VCN}/' + VCN,
            //    type:'get',
            //    dataType: 'json',
            //    success: function (data) {
            //        self.searchVCNDtls(data);
            //        self.isLoading(false);
            //    }
            //});
            $.ajax({
                type: "GET",
                url: '/api/GetVCNStatusForMobApp/{VCN}/',
                data: { VCN: VCN },
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.searchVCNDtls(data);
                }
            });
        }

        //////////////////////////////



        self.LoadTasks = function (model) {
            if (Modernizr.localstorage) {
                if (navigator.onLine) {
                    self.isLoading(true);
                    $.ajax({
                        url: '/api/MobileScheduledTasks/GetScheduledTasks',
                        dataType: 'json',
                        success: function (schdtasks) {
                            self.scheduledTasks(schdtasks);
                            self.isLoading(false);
                            localStorage.setItem("ScheduledTasksData", JSON.stringify(schdtasks));
                            if (schdtasks.length > 0) {

                                ko.utils.arrayForEach(schdtasks, function (schdtask) {

                                    if (schdtask.OperationType == 'BRTH') {
                                        $.ajax({
                                            type: "GET",
                                            url: '/api/MobileScheduledTasks/GetBerthingSide',
                                            dataType: 'json',
                                            success: function (data1) {
                                                self.berthingSide(data1);
                                                localStorage.setItem("BerthingSidedata", JSON.stringify(data1));
                                                $.ajax({
                                                    type: "GET",
                                                    url: '/api/MobileScheduledTasks/GetBerthswithBollards',
                                                    dataType: 'json',
                                                    success: function (data2) {
                                                        self.masterBerths(ko.utils.arrayMap(data2, function (item) {
                                                            return new Berth(item);
                                                        }));

                                                        localStorage.setItem("BerthswithBollardsData", JSON.stringify(data2));
                                                        $.ajax({
                                                            type: "GET",
                                                            url: '/api/MobileScheduledTasks/GetMobileResourceAllowTaskExecution',
                                                            data: { id: schdtask.ResourceAllocationID },
                                                            dataType: 'json',
                                                            success: function (data) {
                                                                if (data.length > 0) {
                                                                    localStorage.setItem("MobileResourceAllowTaskExecution" + data[0].ResourceAllocationID, JSON.stringify(data));
                                                                    if (data[0].FromBerthKey != '' && data[0].FromBerthKey != null && data[0].ToBerthKey != null && data[0].ToBerthKey != '') {

                                                                        if (data[0].FromBerthKey == data[0].ToBerthKey) {
                                                                            $.ajax({
                                                                                type: "GET",
                                                                                url: '/api/GetBollardsInBerthsDetails',
                                                                                data: { Id: data[0].FromBerthKey },
                                                                                dataType: 'json',
                                                                                success: function (data3) {
                                                                                    self.FromBollards(data3);
                                                                                    self.ToBollards(data3);
                                                                                    self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                                        return new ScheduledTaskExecutionModel(item);
                                                                                    }));
                                                                                    self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                                                    localStorage.setItem("EqualBollardsBerthsKeysDetails" + data[0].FromBerthKey, JSON.stringify(data3));
                                                                                }
                                                                            });
                                                                        }
                                                                        else {
                                                                            $.ajax({
                                                                                type: "GET",
                                                                                url: '/api/GetBollardsInBerthsDetails',
                                                                                data: { Id: data[0].FromBerthKey },
                                                                                dataType: 'json',
                                                                                success: function (data4) {
                                                                                    self.FromBollards(data4);
                                                                                    localStorage.setItem("NotEqualFromBollardsBerthsKeysDetails" + data[0].FromBerthKey, JSON.stringify(data4));
                                                                                    $.ajax({
                                                                                        type: "GET",
                                                                                        url: '/api/GetBollardsInBerthsDetails',
                                                                                        data: { Id: data[0].ToBerthKey },
                                                                                        dataType: 'json',
                                                                                        success: function (data5) {
                                                                                            self.ToBollards(data5);
                                                                                            self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                                                return new ScheduledTaskExecutionModel(item);
                                                                                            }));
                                                                                            self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                                                            localStorage.setItem("NotEqualToBollardsBerthsKeysDetails" + data[0].ToBerthKey, JSON.stringify(data5));
                                                                                        }
                                                                                    });
                                                                                }
                                                                            });
                                                                        }
                                                                    }

                                                                    else if (data[0].ToBerthKey != '' && data[0].ToBerthKey != null) {
                                                                        $.ajax({
                                                                            type: "GET",
                                                                            url: '/api/GetBollardsInBerthsDetails',
                                                                            data: { Id: data[0].ToBerthKey },
                                                                            dataType: 'json',
                                                                            success: function (data6) {
                                                                                self.ToBollards(data6);
                                                                                localStorage.setItem("SingleToBerthsKeysDetails" + data[0].ToBerthKey, JSON.stringify(data6));
                                                                                self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                                    return new ScheduledTaskExecutionModel(item);
                                                                                }));
                                                                                self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                                            }
                                                                        });

                                                                    }
                                                                    else if (data[0].FromBerthKey != '' && data[0].FromBerthKey != null) {
                                                                        $.ajax({
                                                                            type: "GET",
                                                                            url: '/api/GetBollardsInBerthsDetails',
                                                                            data: { Id: data[0].FromBerthKey },
                                                                            dataType: 'json',
                                                                            success: function (data7) {
                                                                                self.FromBollards(data7);
                                                                                localStorage.setItem("SingleFromBerthsKeysDetails" + data[0].FromBerthKey, JSON.stringify(data7));
                                                                                self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                                    return new ScheduledTaskExecutionModel(item);
                                                                                }));
                                                                                self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                                            }
                                                                        });

                                                                    }
                                                                    else {
                                                                        self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                            return new ScheduledTaskExecutionModel(item);
                                                                        }));
                                                                        self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                                    }
                                                                }
                                                                else {
                                                                    self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                        return new ScheduledTaskExecutionModel(item);
                                                                    }));
                                                                    self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                                }
                                                            }
                                                        });
                                                    }
                                                });
                                            }
                                        });
                                    }
                                    else {
                                        $.ajax({
                                            type: "GET",
                                            url: '/api/MobileScheduledTasks/GetMobileResourceAllowTaskExecution',
                                            data: { id: schdtask.ResourceAllocationID },
                                            dataType: 'json',
                                            success: function (data8) {
                                                if (data8.length > 0) {
                                                    self.ScheduledTaskExecutionView(ko.utils.arrayMap(data8, function (item) {
                                                        return new ScheduledTaskExecutionModel(item);
                                                    }));
                                                    self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                    localStorage.setItem("TaskExecution" + data8[0].ResourceAllocationID, JSON.stringify(data8));
                                                }
                                                else {
                                                    self.ScheduledTaskExecutionView(ko.utils.arrayMap(data8, function (item) {
                                                        return new ScheduledTaskExecutionModel(item);
                                                    }));
                                                    self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                }
                                            }
                                        });
                                    }

                                });
                            }
                        }
                    });
                }
                else {
                    if (localStorage.getItem("ScheduledTasksData") != null) {
                        var result = JSON.parse(localStorage["ScheduledTasksData"]);
                        self.scheduledTasks(result);
                    }
                    else {
                        toastr.warning('Network is Unavailable!..Please connect to the Internet');
                    }
                }

            } else {
                toastr.warning("Application requires local storage.", "Error");
            }
        }

        self.AckTasksClicked = function (data) {
            window.location.href = "#Schedule-Remarks";
            self.AckTasks(data);
        }
        var idvalue = null;
        var type = null;
        self.AckTasks = function (model) {
            var id = '#span' + model.ResourceAllocationID;
            idvalue = $(id).text();
            $('#scdremarks').val('');
            type = model.OperationType;

        }


        self.AcceptTasksClicked = function (data) {
            window.location.href = "#Scheduled-Tasks";
            self.AcceptTasks(data);
        }

        self.AcceptTasks = function (model) {
            self.isLoading(true);
            var ResourceAllocationVO = {
                ResourceAllocationID: idvalue,
                Remarks: $('#scdremarks').val(),
                TaskStatus: 'ACCP',
                OperationType: type
            };

            $.ajax({
                type: "PUT",
                url: '/api/MobileScheduledTasks/PutScheduledTasks',
                data: ResourceAllocationVO,
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.LoadTasks();
                }
            });
        }

        self.RejectTasksClicked = function (data) {
            if ($('#scdremarks').val() == "" || $('#scdremarks').val() == null) {
                toastr.warning('Please enter the Reject Remarks');
            }
            else {
                window.location.href = "#Scheduled-Tasks";
                self.RejectTasks(data);
            }
        }

        self.RejectTasks = function (model) {
            self.isLoading(true);
            var ResourceAllocationVO = {
                ResourceAllocationID: idvalue,
                Remarks: $('#scdremarks').val(),
                TaskStatus: 'REJT',
                OperationType: type
            };

            $.ajax({
                type: "PUT",
                url: '/api/MobileScheduledTasks/PutScheduledTasks',
                data: ResourceAllocationVO,
                dataType: 'json',
                success: function (data) {
                    self.isLoading(false);
                    self.LoadTasks();
                }
            });
        }

        self.ViewTasks = function (model) {
            window.location.href = "#taskinfo";

            if (Modernizr.localstorage) {
                if (navigator.onLine) {
                    self.isLoading(true);
                    $.ajax({
                        type: "GET",
                        url: '/api/MobileScheduledTasks/GetMobileScheduledTaskViewDetails',
                        data: { id: model.ResourceAllocationID },
                        dataType: 'json',
                        success: function (data) {
                            self.isLoading(false);
                            self.ScheduledTaskView(ko.utils.arrayMap(data, function (item) {
                                var column;
                                if (item != null) {
                                    column = item.split('@');
                                    return new LabelValueModel(column[0], column[1]);
                                }

                            }));

                            localStorage.setItem("ViewTaskExecution" + model.ResourceAllocationID, JSON.stringify(data));

                        }
                    });
                }
                else {
                    if (localStorage.getItem("ViewTaskExecution" + model.ResourceAllocationID) != null) {
                        var result = JSON.parse(localStorage["ViewTaskExecution" + model.ResourceAllocationID]);
                        self.ScheduledTaskView(ko.utils.arrayMap(result, function (item) {
                            var column;
                            if (item != null) {
                                column = item.split('@');
                                return new LabelValueModel(column[0], column[1]);
                            }
                        }));
                    }
                    else {
                        toastr.warning('Network is Unavailable!..Please connect to the Internet');
                    }

                }
            }
            else {
                toastr.warning("Application requires local storage.", "Error");
            }
        }

        self.GetBerthsWithBollardsClicked = function (data) {
            window.location.href = "#Berth-Schedule";
            self.GetBerthsWithBollards(data);
        }

        self.GetBerthsWithBollards = function (model) {


            if (Modernizr.localstorage) {

                if (navigator.onLine) {
                    self.isLoading(true);
                    $.ajax({
                        type: "GET",
                        url: '/api/MobileScheduledTasks/GetBerthingSide',
                        dataType: 'json',
                        success: function (data1) {
                            self.isLoading(false);
                            self.berthingSide(data1);
                            localStorage.setItem("BerthingSidedata", JSON.stringify(data1));
                            $.ajax({
                                type: "GET",
                                url: '/api/MobileScheduledTasks/GetBerthswithBollards',
                                dataType: 'json',
                                success: function (data2) {
                                    self.masterBerths(ko.utils.arrayMap(data2, function (item) {
                                        return new Berth(item);
                                    }));

                                    localStorage.setItem("BerthswithBollardsData", JSON.stringify(data2));
                                    $.ajax({
                                        type: "GET",
                                        url: '/api/MobileScheduledTasks/GetMobileResourceAllowTaskExecution',
                                        data: { id: model.ResourceAllocationID },
                                        dataType: 'json',
                                        success: function (data) {
                                            localStorage.setItem("MobileResourceAllowTaskExecution" + model.ResourceAllocationID, JSON.stringify(data));
                                            if (data.length > 0) {
                                                if (data[0].FromBerthKey != '' && data[0].FromBerthKey != null && data[0].ToBerthKey != null && data[0].ToBerthKey != '') {

                                                    if (data[0].FromBerthKey == data[0].ToBerthKey) {
                                                        $.ajax({
                                                            type: "GET",
                                                            url: '/api/GetBollardsInBerthsDetails',
                                                            data: { Id: data[0].FromBerthKey },
                                                            dataType: 'json',
                                                            success: function (data3) {
                                                                self.FromBollards(data3);
                                                                self.ToBollards(data3);
                                                                self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                    return new ScheduledTaskExecutionModel(item);
                                                                }));
                                                                self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                                localStorage.setItem("EqualBollardsBerthsKeysDetails" + data[0].FromBerthKey, JSON.stringify(data3));
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        $.ajax({
                                                            type: "GET",
                                                            url: '/api/GetBollardsInBerthsDetails',
                                                            data: { Id: data[0].FromBerthKey },
                                                            dataType: 'json',
                                                            success: function (data4) {
                                                                self.FromBollards(data4);
                                                                localStorage.setItem("NotEqualFromBollardsBerthsKeysDetails" + data[0].FromBerthKey, JSON.stringify(data4));
                                                                $.ajax({
                                                                    type: "GET",
                                                                    url: '/api/GetBollardsInBerthsDetails',
                                                                    data: { Id: data[0].ToBerthKey },
                                                                    dataType: 'json',
                                                                    success: function (data5) {
                                                                        self.ToBollards(data5);
                                                                        self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                            return new ScheduledTaskExecutionModel(item);
                                                                        }));
                                                                        self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                                        localStorage.setItem("NotEqualToBollardsBerthsKeysDetails" + data[0].ToBerthKey, JSON.stringify(data5));
                                                                    }
                                                                });
                                                            }
                                                        });
                                                    }
                                                }

                                                else if (data[0].ToBerthKey != '' && data[0].ToBerthKey != null) {
                                                    $.ajax({
                                                        type: "GET",
                                                        url: '/api/GetBollardsInBerthsDetails',
                                                        data: { Id: data[0].ToBerthKey },
                                                        dataType: 'json',
                                                        success: function (data6) {
                                                            self.ToBollards(data6);
                                                            localStorage.setItem("SingleToBerthsKeysDetails" + data[0].ToBerthKey, JSON.stringify(data6));
                                                            self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                return new ScheduledTaskExecutionModel(item);
                                                            }));
                                                            self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                        }
                                                    });

                                                }
                                                else if (data[0].FromBerthKey != '' && data[0].FromBerthKey != null) {
                                                    $.ajax({
                                                        type: "GET",
                                                        url: '/api/GetBollardsInBerthsDetails',
                                                        data: { Id: data[0].FromBerthKey },
                                                        dataType: 'json',
                                                        success: function (data7) {
                                                            self.FromBollards(data7);
                                                            localStorage.setItem("SingleFromBerthsKeysDetails" + data[0].FromBerthKey, JSON.stringify(data7));
                                                            self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                                return new ScheduledTaskExecutionModel(item);
                                                            }));
                                                            self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                        }
                                                    });

                                                }
                                                else {
                                                    self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                        return new ScheduledTaskExecutionModel(item);
                                                    }));
                                                    self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                                }
                                            }
                                            else {
                                                //self.ScheduledTaskExecutionView.removeAll();
                                                self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                                    return new ScheduledTaskExecutionModel(item);
                                                }));
                                                self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                            }
                                        }
                                    });
                                }
                            });
                        }
                    });
                }
                else {
                    if (localStorage["BerthingSidedata"] != null) {
                        var berthingSidedata = JSON.parse(localStorage["BerthingSidedata"]);
                        self.berthingSide(berthingSidedata);
                        if (localStorage["BerthswithBollardsData"] != null) {
                            var berthswithBollardsData = JSON.parse(localStorage["BerthswithBollardsData"]);
                            self.masterBerths(ko.utils.arrayMap(berthswithBollardsData, function (item) {
                                return new Berth(item);
                            }));
                            if (localStorage["MobileResourceAllowTaskExecution" + model.ResourceAllocationID] != null) {
                                var mobileResourceAllowTaskExecution = JSON.parse(localStorage["MobileResourceAllowTaskExecution" + model.ResourceAllocationID]);

                                if (mobileResourceAllowTaskExecution.length > 0) {
                                    if (mobileResourceAllowTaskExecution[0].FromBerthKey != '' && mobileResourceAllowTaskExecution[0].FromBerthKey != null && mobileResourceAllowTaskExecution[0].ToBerthKey != '' && mobileResourceAllowTaskExecution[0].ToBerthKey != null) {

                                        if (mobileResourceAllowTaskExecution[0].FromBerthKey == mobileResourceAllowTaskExecution[0].ToBerthKey) {
                                            if (localStorage["EqualBollardsBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].FromBerthKey] != null) {

                                                var equalBollardsBerthsKeysDetails = JSON.parse(localStorage["EqualBollardsBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].FromBerthKey]);
                                                self.FromBollards(equalBollardsBerthsKeysDetails);
                                                self.ToBollards(equalBollardsBerthsKeysDetails);
                                                self.ScheduledTaskExecutionView(ko.utils.arrayMap(mobileResourceAllowTaskExecution, function (item) {
                                                    return new ScheduledTaskExecutionModel(item);
                                                }));
                                                self.TaskExecutionButtons(self.ScheduledTaskExecutionView());

                                            }
                                        }
                                        else {

                                            if (localStorage["NotEqualFromBollardsBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].FromBerthKey] != null) {

                                                var notEqualFromBollardsBerthsKeysDetails = JSON.parse(localStorage["NotEqualFromBollardsBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].FromBerthKey]);
                                                self.FromBollards(notEqualFromBollardsBerthsKeysDetails);
                                                self.ScheduledTaskExecutionView(ko.utils.arrayMap(mobileResourceAllowTaskExecution, function (item) {
                                                    return new ScheduledTaskExecutionModel(item);
                                                }));
                                                self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                            }
                                            if (localStorage["NotEqualToBollardsBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].ToBerthKey] != null) {

                                                var notEqualToBollardsBerthsKeysDetails = JSON.parse(localStorage["NotEqualToBollardsBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].ToBerthKey]);
                                                self.ToBollards(notEqualToBollardsBerthsKeysDetails);
                                                self.ScheduledTaskExecutionView(ko.utils.arrayMap(mobileResourceAllowTaskExecution, function (item) {
                                                    return new ScheduledTaskExecutionModel(item);
                                                }));
                                                self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                            }
                                        }
                                    }
                                    else if (mobileResourceAllowTaskExecution[0].ToBerthKey != '' && mobileResourceAllowTaskExecution[0].ToBerthKey != null) {
                                        if (localStorage["SingleToBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].ToBerthKey] != null) {

                                            var singleToBerthsKeysDetails = JSON.parse(localStorage["SingleToBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].ToBerthKey]);
                                            self.ToBollards(singleToBerthsKeysDetails);
                                            self.ScheduledTaskExecutionView(ko.utils.arrayMap(mobileResourceAllowTaskExecution, function (item) {
                                                return new ScheduledTaskExecutionModel(item);
                                            }));
                                            self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                        }
                                    }
                                    else if (mobileResourceAllowTaskExecution[0].FromBerthKey != '' && mobileResourceAllowTaskExecution[0].FromBerthKey != null) {

                                        if (localStorage["SingleFromBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].FromBerthKey] != null) {

                                            var singleFromBerthsKeysDetails = JSON.parse(localStorage["SingleFromBerthsKeysDetails" + mobileResourceAllowTaskExecution[0].FromBerthKey]);
                                            self.FromBollards(singleFromBerthsKeysDetails);
                                            self.ScheduledTaskExecutionView(ko.utils.arrayMap(mobileResourceAllowTaskExecution, function (item) {
                                                return new ScheduledTaskExecutionModel(item);
                                            }));
                                            self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                        }

                                    }
                                    else {
                                        self.ScheduledTaskExecutionView(ko.utils.arrayMap(mobileResourceAllowTaskExecution, function (item) {
                                            return new ScheduledTaskExecutionModel(item);
                                        }));
                                        self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                    }
                                }
                                else {
                                    //self.ScheduledTaskExecutionView.removeAll();
                                    self.ScheduledTaskExecutionView(ko.utils.arrayMap(mobileResourceAllowTaskExecution, function (item) {
                                        return new ScheduledTaskExecutionModel(item);
                                    }));
                                    self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                }
                            }
                        }
                    }
                    else {
                        toastr.warning('Network is Unavailable!..Please connect to the Internet');
                    }
                }
            }
            else {
                toastr.warning("Application requires local storage.", "Error");
            }
        }

        self.getResourceAllowTaskExecutionClicked = function (data) {
            if (data.IsExecute == 'Y') {
                window.location.href = "#Scheduled-Task-Execution";
                self.getResourceAllowTaskExecution(data);
            } else {
                toastr.warning("The vessel is not arrived", "Scheduled Task Execution");
            }
        }

        self.getResourceAllowTaskExecution = function (model) {
            if (Modernizr.localstorage) {
                self.isLoading(true);
                if (navigator.onLine) {
                    if (localStorage.getItem("TaskExecution" + model.ResourceAllocationID) != null) {
                        self.isLoading(false);
                        var result = JSON.parse(localStorage["TaskExecution" + model.ResourceAllocationID]);
                        self.ScheduledTaskExecutionView(ko.utils.arrayMap(result, function (item) {
                            return new ScheduledTaskExecutionModel(item);
                        }));
                        self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                    }
                    else {
                        $.ajax({
                            type: "GET",
                            url: '/api/MobileScheduledTasks/GetMobileResourceAllowTaskExecution',
                            data: { id: model.ResourceAllocationID },
                            dataType: 'json',
                            success: function (data) {
                                self.ScheduledTaskExecutionView(ko.utils.arrayMap(data, function (item) {
                                    return new ScheduledTaskExecutionModel(item);
                                }));
                                self.isLoading(false);
                                self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                                localStorage.setItem("TaskExecution" + model.ResourceAllocationID, JSON.stringify(data));
                            }
                        });
                    }
                }
                else {
                    self.isLoading(false);
                    if (localStorage.getItem("TaskExecution" + model.ResourceAllocationID) != null) {
                        var result = JSON.parse(localStorage["TaskExecution" + model.ResourceAllocationID]);
                        self.ScheduledTaskExecutionView(ko.utils.arrayMap(result, function (item) {
                            return new ScheduledTaskExecutionModel(item);
                        }));
                        self.TaskExecutionButtons(self.ScheduledTaskExecutionView());
                    }
                    else {
                        toastr.warning('Network is Unavailable!..Please connect to the Internet');
                    }

                }
            }
            else {
                toastr.warning("Application requires local storage.", "Error");
            }

        }

        self.GetDate = function () { }

        self.TaskExecutionButtons = function (model) {
            if (model.length > 0) {
                if (model[0].OperationType() == 'BRTH' && model[0].MomentType() == 'ARMV') {
                    self.ArrivalBerthTaskExecution(model[0]);
                }
                else if (model[0].OperationType() == 'BRTH' && model[0].MomentType() == 'SHMV') {
                    self.ShiftingBerthTaskExecution(model[0]);
                }
                else if (model[0].OperationType() == 'BRTH' && model[0].MomentType() == 'WRMV') {
                    self.WarpingBerthTaskExecution(model[0]);
                }
                else if (model[0].OperationType() == 'BRTH' && model[0].MomentType() == 'SGMV') {
                    self.SailingBerthTaskExecution(model[0]);
                }
                else if (model[0].OperationType() == 'PILT') {
                    self.PilotageMobileTaskExecution(model[0]);
                }
                else if (model[0].OperationType() == 'PIHE') {
                    self.PilotBoatMobileTaskExecution(model[0]);
                }
                else if (model[0].OperationType() == 'TGWR') {
                    self.TugWorkBoatMobileTaskExecution(model[0]);
                }
                else if (model[0].OperationType() == 'WTST') {
                    self.WaterMobileTaskExecution(model[0]);
                }
                else if (model[0].OperationType() == 'FCST') {
                    self.FloatingCraneMobileTaskExecution(model[0]);
                }
            }
        }

        self.ArrivalBerthTaskExecution = function (model) {

            if (model.WaitingStartTime() != null) {
                if ($("#berthWtngStrtTime").prop('class') == 'mobile-btn') {
                    $("#berthWtngStrtTime").removeClass('mobile-btn');
                    $("#berthWtngStrtTime").addClass('btn-disable');
                }
            }

            if (model.WaitingEndTime() != null) {
                if ($("#berthWtngEndTime").prop('class') == 'mobile-btn') {
                    $("#berthWtngEndTime").removeClass('mobile-btn');
                    $("#berthWtngEndTime").addClass('btn-disable');
                }
            }

            if (model.StartTime() != null) {
                if ($("#btnStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnStrtTime").removeClass('mobile-btn');
                    $("#btnStrtTime").addClass('btn-disable');
                }
                if (model.FirstLineIn() != null) {
                    if ($("#btnFirstlinein").prop('class') == 'mobile-btn') {
                        $("#btnFirstlinein").removeClass('mobile-btn');
                        $("#btnFirstlinein").addClass('btn-disable');
                    }
                    if (model.LastLineIn() != null) {
                        if ($("#btnLastlinein").prop('class') == 'mobile-btn') {
                            $("#btnLastlinein").removeClass('mobile-btn');
                            $("#btnLastlinein").addClass('btn-disable');
                        }
                        if (model.EndTime() != null) {
                            if ($("#btnEndTime").prop('class') == 'mobile-btn') {
                                $("#btnEndTime").removeClass('mobile-btn');
                                $("#btnEndTime").addClass('btn-disable');
                            }
                            $("#SaveExittab").removeClass('save-exit-disable');
                            $("#SaveExittab").addClass('save-exit-btn');
                        }
                        else {
                            $("#btnEndTime").removeClass('btn-disable');
                            $("#btnEndTime").addClass('mobile-btn');
                        }
                    }
                    else {
                        $("#btnLastlinein").removeClass('btn-disable');
                        $("#btnLastlinein").addClass('mobile-btn');
                    }
                }
                else {
                    $("#btnFirstlinein").removeClass('btn-disable');
                    $("#btnFirstlinein").addClass('mobile-btn');
                }
            }
            //  $("#btnStrtTime").prop('class')
            // $("#btnFirstlinein").prop('class')
            // $("#btnLastlinein").prop('class')
            //$("#btnEndTime").prop('class')
        }
        self.ShiftingBerthTaskExecution = function (model) {

            //$("#btnStrtTime").prop('class')
            //$("#btnFirstLineoff").prop('class')
            //$("#btnLastLineoff").prop('class')
            //$("#btnFirstlinein").prop('class')
            //$("#btnLastlinein").prop('class')
            //$("#btnEndTime").prop('class')

            if (model.WaitingStartTime() != null) {
                if ($("#berthWtngStrtTime").prop('class') == 'mobile-btn') {
                    $("#berthWtngStrtTime").removeClass('mobile-btn');
                    $("#berthWtngStrtTime").addClass('btn-disable');
                }
            }

            if (model.WaitingEndTime() != null) {
                if ($("#berthWtngEndTime").prop('class') == 'mobile-btn') {
                    $("#berthWtngEndTime").removeClass('mobile-btn');
                    $("#berthWtngEndTime").addClass('btn-disable');
                }
            }

            if (model.StartTime() != null) {
                if ($("#btnStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnStrtTime").removeClass('mobile-btn');
                    $("#btnStrtTime").addClass('btn-disable');
                }
                if (model.FirstLineOut() != null) {
                    if ($("#btnFirstLineoff").prop('class') == 'mobile-btn') {
                        $("#btnFirstLineoff").removeClass('mobile-btn');
                        $("#btnFirstLineoff").addClass('btn-disable');
                    }
                    if (model.LastLineOut() != null) {
                        if ($("#btnLastLineoff").prop('class') == 'mobile-btn') {
                            $("#btnLastLineoff").removeClass('mobile-btn');
                            $("#btnLastLineoff").addClass('btn-disable');
                        }
                        if (model.FirstLineIn() != null) {
                            if ($("#btnFirstlinein").prop('class') == 'mobile-btn') {
                                $("#btnFirstlinein").removeClass('mobile-btn');
                                $("#btnFirstlinein").addClass('btn-disable');
                            }
                            if (model.LastLineIn() != null) {
                                if ($("#btnLastlinein").prop('class') == 'mobile-btn') {
                                    $("#btnLastlinein").removeClass('mobile-btn');
                                    $("#btnLastlinein").addClass('btn-disable');
                                }
                                if (model.EndTime() != null) {
                                    if ($("#btnEndTime").prop('class') == 'mobile-btn') {
                                        $("#btnEndTime").removeClass('mobile-btn');
                                        $("#btnEndTime").addClass('btn-disable');
                                    }
                                    $("#SaveExittab").removeClass('save-exit-disable');
                                    $("#SaveExittab").addClass('save-exit-btn');
                                }
                                else {
                                    $("#btnEndTime").removeClass('btn-disable');
                                    $("#btnEndTime").addClass('mobile-btn');
                                }
                            }
                            else {
                                $("#btnLastlinein").removeClass('btn-disable');
                                $("#btnLastlinein").addClass('mobile-btn');
                            }
                        }
                        else {
                            $("#btnFirstlinein").removeClass('btn-disable');
                            $("#btnFirstlinein").addClass('mobile-btn');
                        }
                    }
                    else {
                        $("#btnLastLineoff").removeClass('btn-disable');
                        $("#btnLastLineoff").addClass('mobile-btn');
                    }
                }
                else {
                    $("#btnFirstLineoff").removeClass('btn-disable');
                    $("#btnFirstLineoff").addClass('mobile-btn');
                }

            }
        }
        self.WarpingBerthTaskExecution = function (model) {
            //$("#btnStrtTime").prop('class')
            //$("#btnFirstLineoff").prop('class')
            //$("#btnLastLineoff").prop('class')
            //$("#btnFirstlinein").prop('class')
            //$("#btnLastlinein").prop('class')
            //$("#btnEndTime").prop('class')

            if (model.WaitingStartTime() != null) {
                if ($("#berthWtngStrtTime").prop('class') == 'mobile-btn') {
                    $("#berthWtngStrtTime").removeClass('mobile-btn');
                    $("#berthWtngStrtTime").addClass('btn-disable');
                }
            }

            if (model.WaitingEndTime() != null) {
                if ($("#berthWtngEndTime").prop('class') == 'mobile-btn') {
                    $("#berthWtngEndTime").removeClass('mobile-btn');
                    $("#berthWtngEndTime").addClass('btn-disable');
                }
            }

            if (model.StartTime() != null) {
                if ($("#btnStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnStrtTime").removeClass('mobile-btn');
                    $("#btnStrtTime").addClass('btn-disable');
                }
                if (model.FirstLineOut() != null) {
                    if ($("#btnFirstLineoff").prop('class') == 'mobile-btn') {
                        $("#btnFirstLineoff").removeClass('mobile-btn');
                        $("#btnFirstLineoff").addClass('btn-disable');
                    }
                    if (model.LastLineOut() != null) {
                        if ($("#btnLastLineoff").prop('class') == 'mobile-btn') {
                            $("#btnLastLineoff").removeClass('mobile-btn');
                            $("#btnLastLineoff").addClass('btn-disable');
                        }
                        if (model.FirstLineIn() != null) {
                            if ($("#btnFirstlinein").prop('class') == 'mobile-btn') {
                                $("#btnFirstlinein").removeClass('mobile-btn');
                                $("#btnFirstlinein").addClass('btn-disable');
                            }
                            if (model.LastLineIn() != null) {
                                if ($("#btnLastlinein").prop('class') == 'mobile-btn') {
                                    $("#btnLastlinein").removeClass('mobile-btn');
                                    $("#btnLastlinein").addClass('btn-disable');
                                }
                                if (model.EndTime() != null) {
                                    if ($("#btnEndTime").prop('class') == 'mobile-btn') {
                                        $("#btnEndTime").removeClass('mobile-btn');
                                        $("#btnEndTime").addClass('btn-disable');
                                    }
                                    $("#SaveExittab").removeClass('save-exit-disable');
                                    $("#SaveExittab").addClass('save-exit-btn');
                                }
                                else {
                                    $("#btnEndTime").removeClass('btn-disable');
                                    $("#btnEndTime").addClass('mobile-btn');
                                }
                            }
                            else {
                                $("#btnLastlinein").removeClass('btn-disable');
                                $("#btnLastlinein").addClass('mobile-btn');
                            }
                        }
                        else {
                            $("#btnFirstlinein").removeClass('btn-disable');
                            $("#btnFirstlinein").addClass('mobile-btn');
                        }
                    }
                    else {
                        $("#btnLastLineoff").removeClass('btn-disable');
                        $("#btnLastLineoff").addClass('mobile-btn');
                    }
                }
                else {
                    $("#btnFirstLineoff").removeClass('btn-disable');
                    $("#btnFirstLineoff").addClass('mobile-btn');
                }

            }

        }
        self.SailingBerthTaskExecution = function (model) {
            //$("#btnStrtTime").prop('class')
            //$("#btnFirstLineoff").prop('class')
            //$("#btnLastLineoff").prop('class')
            //$("#btnEndTime").prop('class')

            if (model.WaitingStartTime() != null) {
                if ($("#berthWtngStrtTime").prop('class') == 'mobile-btn') {
                    $("#berthWtngStrtTime").removeClass('mobile-btn');
                    $("#berthWtngStrtTime").addClass('btn-disable');
                }
            }

            if (model.WaitingEndTime() != null) {
                if ($("#berthWtngEndTime").prop('class') == 'mobile-btn') {
                    $("#berthWtngEndTime").removeClass('mobile-btn');
                    $("#berthWtngEndTime").addClass('btn-disable');
                }
            }

            if (model.StartTime() != null) {
                if ($("#btnStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnStrtTime").removeClass('mobile-btn');
                    $("#btnStrtTime").addClass('btn-disable');
                }
                if (model.FirstLineOut() != null) {
                    if ($("#btnFirstLineoff").prop('class') == 'mobile-btn') {
                        $("#btnFirstLineoff").removeClass('mobile-btn');
                        $("#btnFirstLineoff").addClass('btn-disable');
                    }
                    if (model.LastLineOut() != null) {
                        if ($("#btnLastLineoff").prop('class') == 'mobile-btn') {
                            $("#btnLastLineoff").removeClass('mobile-btn');
                            $("#btnLastLineoff").addClass('btn-disable');
                        }
                        if (model.EndTime() != null) {
                            if ($("#btnEndTime").prop('class') == 'mobile-btn') {
                                $("#btnEndTime").removeClass('mobile-btn');
                                $("#btnEndTime").addClass('btn-disable');
                            }
                            $("#SaveExittab").removeClass('save-exit-disable');
                            $("#SaveExittab").addClass('save-exit-btn');
                        }
                        else {
                            $("#btnEndTime").removeClass('btn-disable');
                            $("#btnEndTime").addClass('mobile-btn');
                        }

                    } else {
                        $("#btnLastLineoff").removeClass('btn-disable');
                        $("#btnLastLineoff").addClass('mobile-btn');
                    }
                }
                else {
                    $("#btnFirstLineoff").removeClass('btn-disable');
                    $("#btnFirstLineoff").addClass('mobile-btn');
                }
            }

        }
        self.PilotageMobileTaskExecution = function (model) {
            // $("#btnsupStrtTime").prop('class')
            // $("#btnsupPltOnBord").prop('class')
            // $("#btnsupPltOff").prop('class')
            //  $("#btnsupEndTime").prop('class')
            // $("#btnWtngStrtTime").prop('class')
            // $("#btnWtngEndTime").prop('class')

            if (model.WaitingStartTime() != null) {
                if ($("#btnWtngStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnWtngStrtTime").removeClass('mobile-btn');
                    $("#btnWtngStrtTime").addClass('btn-disable');
                }
            }

            if (model.WaitingEndTime() != null) {
                if ($("#btnWtngEndTime").prop('class') == 'mobile-btn') {
                    $("#btnWtngEndTime").removeClass('mobile-btn');
                    $("#btnWtngEndTime").addClass('btn-disable');
                }
            }

            if (model.StartTime() != null) {
                if ($("#btnsupStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnsupStrtTime").removeClass('mobile-btn');
                    $("#btnsupStrtTime").addClass('btn-disable');
                }
                if (model.PilotOnBoard() != null) {
                    if ($("#btnsupPltOnBord").prop('class') == 'mobile-btn') {
                        $("#btnsupPltOnBord").removeClass('mobile-btn');
                        $("#btnsupPltOnBord").addClass('btn-disable');
                    }
                    if (model.PilotOff() != null) {
                        if ($("#btnsupPltOff").prop('class') == 'mobile-btn') {
                            $("#btnsupPltOff").removeClass('mobile-btn');
                            $("#btnsupPltOff").addClass('btn-disable');
                        }
                        if (model.EndTime() != null) {
                            if ($("#btnsupEndTime").prop('class') == 'mobile-btn') {
                                $("#btnsupEndTime").removeClass('mobile-btn');
                                $("#btnsupEndTime").addClass('btn-disable');
                            }
                            $("#SaveSupExittab").removeClass('save-exit-disable');
                            $("#SaveSupExittab").addClass('save-exit-btn');
                        }
                        else {
                            $("#btnsupEndTime").removeClass('btn-disable');
                            $("#btnsupEndTime").addClass('mobile-btn');
                        }
                    }
                    else {
                        $("#btnsupPltOff").removeClass('btn-disable');
                        $("#btnsupPltOff").addClass('mobile-btn');
                    }
                }
                else {
                    $("#btnsupPltOnBord").removeClass('btn-disable');
                    $("#btnsupPltOnBord").addClass('mobile-btn');
                }
            }
        }
        self.PilotBoatMobileTaskExecution = function (model) {
            // $("#btnsupStrtTime").prop('class')
            // $("#btnsupPltOnBord").prop('class')
            //$("#btnsupPltOff").prop('class')
            // $("#btnsupEndTime").prop('class')

            if (model.StartTime() != null) {
                if ($("#btnsupStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnsupStrtTime").removeClass('mobile-btn');
                    $("#btnsupStrtTime").addClass('btn-disable');
                }
                if (model.PilotOnBoard() != null) {
                    if ($("#btnsupPltOnBord").prop('class') == 'mobile-btn') {
                        $("#btnsupPltOnBord").removeClass('mobile-btn');
                        $("#btnsupPltOnBord").addClass('btn-disable');
                    }
                    if (model.PilotOff() != null) {
                        if ($("#btnsupPltOff").prop('class') == 'mobile-btn') {
                            $("#btnsupPltOff").removeClass('mobile-btn');
                            $("#btnsupPltOff").addClass('btn-disable');
                        }
                        if (model.EndTime() != null) {
                            if ($("#btnsupEndTime").prop('class') == 'mobile-btn') {
                                $("#btnsupEndTime").removeClass('mobile-btn');
                                $("#btnsupEndTime").addClass('btn-disable');
                            }
                            $("#SaveSupExittab").removeClass('save-exit-disable');
                            $("#SaveSupExittab").addClass('save-exit-btn');
                        }
                        else {
                            $("#btnsupEndTime").removeClass('btn-disable');
                            $("#btnsupEndTime").addClass('mobile-btn');
                        }
                    }
                    else {
                        $("#btnsupPltOff").removeClass('btn-disable');
                        $("#btnsupPltOff").addClass('mobile-btn');
                    }
                }
                else {
                    $("#btnsupPltOnBord").removeClass('btn-disable');
                    $("#btnsupPltOnBord").addClass('mobile-btn');
                }
            }
        }
        self.TugWorkBoatMobileTaskExecution = function (model) {
            // $("#btnsupStrtTime").prop('class')
            //  $("#btnsupLineUpTime").prop('class')
            //$("#btnsupLineDownTime").prop('class')
            // $("#btnsupEndTime").prop('class')

            if (model.WaitingStartTime() != null) {
                if ($("#btnWtngStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnWtngStrtTime").removeClass('mobile-btn');
                    $("#btnWtngStrtTime").addClass('btn-disable');
                }
            }

            if (model.WaitingEndTime() != null) {
                if ($("#btnWtngEndTime").prop('class') == 'mobile-btn') {
                    $("#btnWtngEndTime").removeClass('mobile-btn');
                    $("#btnWtngEndTime").addClass('btn-disable');
                }
            }


            if (model.StartTime() != null) {
                if ($("#btnsupStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnsupStrtTime").removeClass('mobile-btn');
                    $("#btnsupStrtTime").addClass('btn-disable');
                }
                if (model.LineUp() != null) {
                    if ($("#btnsupLineUpTime").prop('class') == 'mobile-btn') {
                        $("#btnsupLineUpTime").removeClass('mobile-btn');
                        $("#btnsupLineUpTime").addClass('btn-disable');
                    }
                    if (model.LineDown() != null) {
                        if ($("#btnsupLineDownTime").prop('class') == 'mobile-btn') {
                            $("#btnsupLineDownTime").removeClass('mobile-btn');
                            $("#btnsupLineDownTime").addClass('btn-disable');
                        }
                        if (model.EndTime() != null) {
                            if ($("#btnsupEndTime").prop('class') == 'mobile-btn') {
                                $("#btnsupEndTime").removeClass('mobile-btn');
                                $("#btnsupEndTime").addClass('btn-disable');
                            }
                            $("#SaveSupExittab").removeClass('save-exit-disable');
                            $("#SaveSupExittab").addClass('save-exit-btn');
                        }
                        else {
                            $("#btnsupEndTime").removeClass('btn-disable');
                            $("#btnsupEndTime").addClass('mobile-btn');
                        }
                    }
                    else {
                        $("#btnsupLineDownTime").removeClass('btn-disable');
                        $("#btnsupLineDownTime").addClass('mobile-btn');
                    }
                }
                else {
                    $("#btnsupLineUpTime").removeClass('btn-disable');
                    $("#btnsupLineUpTime").addClass('mobile-btn');
                }
            }
        }
        self.WaterMobileTaskExecution = function (model) {
            // $("#btnsupStrtTime").prop('class')
            //$("#btnsupEndTime").prop('class')

            if (model.WaitingStartTime() != null) {
                if ($("#btnWtngStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnWtngStrtTime").removeClass('mobile-btn');
                    $("#btnWtngStrtTime").addClass('btn-disable');
                }
            }

            if (model.WaitingEndTime() != null) {
                if ($("#btnWtngEndTime").prop('class') == 'mobile-btn') {
                    $("#btnWtngEndTime").removeClass('mobile-btn');
                    $("#btnWtngEndTime").addClass('btn-disable');
                }
            }

            if (model.StartTime() != null) {
                if ($("#btnsupStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnsupStrtTime").removeClass('mobile-btn');
                    $("#btnsupStrtTime").addClass('btn-disable');
                }
                if (model.EndTime() != null) {
                    if ($("#btnsupEndTime").prop('class') == 'mobile-btn') {
                        $("#btnsupEndTime").removeClass('mobile-btn');
                        $("#btnsupEndTime").addClass('btn-disable');
                    }
                    $("#SaveSupExittab").removeClass('save-exit-disable');
                    $("#SaveSupExittab").addClass('save-exit-btn');
                }
                else {
                    $("#btnsupEndTime").removeClass('btn-disable');
                    $("#btnsupEndTime").addClass('mobile-btn');
                }
            }
        }
        self.FloatingCraneMobileTaskExecution = function (model) {
            // $("#btnsupStrtTime").prop('class')
            //  $("#btnsupTasTime").prop('class')
            //  $("#btnsupFswingTime").prop('class')
            // $("#btnsupLstSwingTime").prop('class')
            //$("#btnsupEndTime").prop('class')

            if (model.StartTime() != null) {
                if ($("#btnsupStrtTime").prop('class') == 'mobile-btn') {
                    $("#btnsupStrtTime").removeClass('mobile-btn');
                    $("#btnsupStrtTime").addClass('btn-disable');
                }
                if (model.TimeAlongSide() != null) {
                    if ($("#btnsupTasTime").prop('class') == 'mobile-btn') {
                        $("#btnsupTasTime").removeClass('mobile-btn');
                        $("#btnsupTasTime").addClass('btn-disable');
                    }
                    if (model.FirstSwing() != null) {
                        if ($("#btnsupFswingTime").prop('class') == 'mobile-btn') {
                            $("#btnsupFswingTime").removeClass('mobile-btn');
                            $("#btnsupFswingTime").addClass('btn-disable');
                        }
                        if (model.LastSwing() != null) {
                            if ($("#btnsupLstSwingTime").prop('class') == 'mobile-btn') {
                                $("#btnsupLstSwingTime").removeClass('mobile-btn');
                                $("#btnsupLstSwingTime").addClass('btn-disable');
                            }
                            if (model.EndTime() != null) {
                                if ($("#btnsupEndTime").prop('class') == 'mobile-btn') {
                                    $("#btnsupEndTime").removeClass('mobile-btn');
                                    $("#btnsupEndTime").addClass('btn-disable');
                                }
                                $("#SaveSupExittab").removeClass('save-exit-disable');
                                $("#SaveSupExittab").addClass('save-exit-btn');
                            }
                            else {
                                $("#btnsupEndTime").removeClass('btn-disable');
                                $("#btnsupEndTime").addClass('mobile-btn');
                            }
                        }
                        else {
                            $("#btnsupLstSwingTime").removeClass('btn-disable');
                            $("#btnsupLstSwingTime").addClass('mobile-btn');
                        }
                    }
                    else {
                        $("#btnsupFswingTime").removeClass('btn-disable');
                        $("#btnsupFswingTime").addClass('mobile-btn');
                    }
                }
                else {
                    $("#btnsupTasTime").removeClass('btn-disable');
                    $("#btnsupTasTime").addClass('mobile-btn');
                }
            }
        }

        self.GetStartTime = function (model) {
            self.isSuccess(false);
            if (model.OperationType() == 'BRTH') {
                if ($("#btnStrtTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'StartTime',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.StartTime(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);

                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnStrtTime").removeClass('mobile-btn');
                            $("#btnStrtTime").addClass('btn-disable');

                            if (model.MomentType() == 'ARMV') {
                                $("#btnFirstlinein").removeClass('btn-disable');
                                $("#btnFirstlinein").addClass('mobile-btn');
                            }
                            else {
                                $("#btnFirstLineoff").removeClass('btn-disable');
                                $("#btnFirstLineoff").addClass('mobile-btn');
                            }
                        }
                    });
                }
            }
            else {
                if ($("#btnsupStrtTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'StartTime',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.StartTime(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupStrtTime").removeClass('mobile-btn');
                            $("#btnsupStrtTime").addClass('btn-disable');

                            if (model.OperationType() == 'PILT' || model.OperationType() == 'PIHE') {
                                $("#btnsupPltOnBord").removeClass('btn-disable');
                                $("#btnsupPltOnBord").addClass('mobile-btn');
                            }
                            if (model.OperationType() == 'TGWR') {
                                $("#btnsupLineUpTime").removeClass('btn-disable');
                                $("#btnsupLineUpTime").addClass('mobile-btn');
                            }
                            if (model.OperationType() == 'WTST') {
                                $("#btnsupEndTime").removeClass('btn-disable');
                                $("#btnsupEndTime").addClass('mobile-btn');
                            }
                            if (model.OperationType() == 'FCST') {
                                $("#btnsupTasTime").removeClass('btn-disable');
                                $("#btnsupTasTime").addClass('mobile-btn');
                            }
                        }
                    });
                }
            }

        }

        self.GetPilotOnBoard = function (model) {
            self.isSuccess(false);

            if (model.OperationType() == 'PILT' || model.OperationType() == 'PIHE') {
                if ($("#btnsupPltOnBord").prop('class') == 'mobile-btn') {
                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'PilotOnBoard',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.PilotOnBoard(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupPltOnBord").removeClass('mobile-btn');
                            $("#btnsupPltOnBord").addClass('btn-disable');

                            $("#btnsupPltOff").removeClass('btn-disable');
                            $("#btnsupPltOff").addClass('mobile-btn');
                        }
                    });
                }
            }
        }

        self.GetPilotOff = function (model) {
            self.isSuccess(false);
            if (model.OperationType() == 'PILT' || model.OperationType() == 'PIHE') {
                if ($("#btnsupPltOff").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'PilotOff',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.PilotOff(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupPltOff").removeClass('mobile-btn');
                            $("#btnsupPltOff").addClass('btn-disable');

                            $("#btnsupEndTime").removeClass('btn-disable');
                            $("#btnsupEndTime").addClass('mobile-btn');
                        }
                    });
                }
            }
        }

        self.GetEndTime = function (model) {
            self.isSuccess(false);

            if (model.OperationType() == 'BRTH') {
                if ($("#btnEndTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'EndTime',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.EndTime(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnEndTime").removeClass('mobile-btn');
                            $("#btnEndTime").addClass('btn-disable');
                            $("#SaveExittab").removeClass('save-exit-disable');
                            $("#SaveExittab").addClass('save-exit-btn');
                        }
                    });
                }
            }
            else {

                if ($("#btnsupEndTime").prop('class') == 'mobile-btn') {
                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'EndTime',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.EndTime(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupEndTime").removeClass('mobile-btn');
                            $("#btnsupEndTime").addClass('btn-disable');
                            $("#SaveSupExittab").removeClass('save-exit-disable');
                            $("#SaveSupExittab").addClass('save-exit-btn');
                        }
                    });
                }
            }

        }



        self.GetWaitingStartTime = function (model) {
            self.isSuccess(false);

            if (model.OperationType() == 'PILT' || model.OperationType() == 'TGWR' || model.OperationType() == 'WTST') {
                if ($("#btnWtngStrtTime").prop('class') == 'mobile-btn') {
                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'WaitingStartTime',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.WaitingStartTime(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnWtngStrtTime").removeClass('mobile-btn');
                            $("#btnWtngStrtTime").addClass('btn-disable');
                        }
                    });

                }
            }
            else {
                if (model.OperationType() == 'BRTH') {
                    if ($("#berthWtngStrtTime").prop('class') == 'mobile-btn') {
                        var ScheduledTaskExecutionVO = {
                            ResourceAllocationID: model.ResourceAllocationID(),
                            FieldName: 'WaitingStartTime',
                            FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                            OperationType: model.OperationType(),
                            ValPKID: model.ValPKID()
                        };
                        model.WaitingStartTime(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                        self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                        $(document).ajaxStop(function () {
                            if (self.isSuccess()) {
                                $("#berthWtngStrtTime").removeClass('mobile-btn');
                                $("#berthWtngStrtTime").addClass('btn-disable');
                            }
                        });
                    }
                }
            }
        }

        self.GetWaitingEndTime = function (model) {
            self.isSuccess(false);
            if (model.OperationType() == 'PILT' || model.OperationType() == 'TGWR' || model.OperationType() == 'WTST') {
                if ($("#btnWtngEndTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'WaitingEndTime',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.WaitingEndTime(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnWtngEndTime").removeClass('mobile-btn');
                            $("#btnWtngEndTime").addClass('btn-disable');
                        }
                    });
                }
            }
            else {
                if (model.OperationType() == 'BRTH') {
                    if ($("#berthWtngEndTime").prop('class') == 'mobile-btn') {
                        var ScheduledTaskExecutionVO = {
                            ResourceAllocationID: model.ResourceAllocationID(),
                            FieldName: 'WaitingEndTime',
                            FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                            OperationType: model.OperationType(),
                            ValPKID: model.ValPKID()
                        };
                        model.WaitingEndTime(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                        self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                        $(document).ajaxStop(function () {
                            if (self.isSuccess()) {
                                $("#berthWtngEndTime").removeClass('mobile-btn');
                                $("#berthWtngEndTime").addClass('btn-disable');
                            }
                        });
                    }
                }
            }
        }

        self.GetLineUpTime = function (model) {
            self.isSuccess(false);
            if (model.OperationType() == 'TGWR') {
                if ($("#btnsupLineUpTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'LineUp',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.LineUp(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupLineUpTime").removeClass('mobile-btn');
                            $("#btnsupLineUpTime").addClass('btn-disable');
                            $("#btnsupLineDownTime").removeClass('btn-disable');
                            $("#btnsupLineDownTime").addClass('mobile-btn');
                        }
                    });
                }
            }
        }

        self.GetLineDownTime = function (model) {
            self.isSuccess(false);
            if (model.OperationType() == 'TGWR') {
                if ($("#btnsupLineDownTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'LineDown',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.LineDown(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupLineDownTime").removeClass('mobile-btn');
                            $("#btnsupLineDownTime").addClass('btn-disable');
                            $("#btnsupEndTime").removeClass('btn-disable');
                            $("#btnsupEndTime").addClass('mobile-btn');
                        }
                    });
                }
            }
        }

        self.SaveTotalDispensed = function (model) {
            var ScheduledTaskExecutionVO = {
                ResourceAllocationID: model.ResourceAllocationID(),
                FieldName: 'TotalDispensed',
                FieldValue: model.TotalDispensed(),
                OperationType: model.OperationType(),
                ValPKID: model.ValPKID()
            };
            self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
        }

        self.SaveMeterSerialNo = function (model) {
            var ScheduledTaskExecutionVO = {
                ResourceAllocationID: model.ResourceAllocationID(),
                FieldName: 'MeterSerialNo',
                FieldValue: model.MeterSerialNo(),
                OperationType: model.OperationType(),
                ValPKID: model.ValPKID()
            };
            self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
        }

        self.SaveOpeningMeterReading = function (model) {
            var ScheduledTaskExecutionVO = {
                ResourceAllocationID: model.ResourceAllocationID(),
                FieldName: 'OpeningMeterReading',
                FieldValue: model.OpeningMeterReading(),
                OperationType: model.OperationType(),
                ValPKID: model.ValPKID()
            };
            self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
        }

        self.SaveClosingMeterReading = function (model) {
            var ScheduledTaskExecutionVO = {
                ResourceAllocationID: model.ResourceAllocationID(),
                FieldName: 'ClosingMeterReading',
                FieldValue: model.ClosingMeterReading(),
                OperationType: model.OperationType(),
                ValPKID: model.ValPKID()
            };
            self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
        }

        self.GetTimeAlongSideTime = function (model) {
            self.isSuccess(false);
            if (model.OperationType() == 'FCST') {
                if ($("#btnsupTasTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'TimeAlongSide',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.TimeAlongSide(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupTasTime").removeClass('mobile-btn');
                            $("#btnsupTasTime").addClass('btn-disable');
                            $("#btnsupFswingTime").removeClass('btn-disable');
                            $("#btnsupFswingTime").addClass('mobile-btn');
                        }
                    });

                }
            }
        }

        self.GetFirstSwingTime = function (model) {
            self.isSuccess(false);
            if (model.OperationType() == 'FCST') {
                if ($("#btnsupFswingTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'FirstSwing',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.FirstSwing(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupFswingTime").removeClass('mobile-btn');
                            $("#btnsupFswingTime").addClass('btn-disable');
                            $("#btnsupLstSwingTime").removeClass('btn-disable');
                            $("#btnsupLstSwingTime").addClass('mobile-btn');
                        }
                    });
                }
            }
        }

        self.GetLastSwingTime = function (model) {
            self.isSuccess(false);
            if (model.OperationType() == 'FCST') {
                if ($("#btnsupLstSwingTime").prop('class') == 'mobile-btn') {

                    var ScheduledTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        FieldName: 'LastSwing',
                        FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                        OperationType: model.OperationType(),
                        ValPKID: model.ValPKID()
                    };
                    model.LastSwing(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                    self.SaveDataOnEachClick(ScheduledTaskExecutionVO);

                    $(document).ajaxStop(function () {
                        if (self.isSuccess()) {
                            $("#btnsupLstSwingTime").removeClass('mobile-btn');
                            $("#btnsupLstSwingTime").addClass('btn-disable');
                            $("#btnBacktoQuay").removeClass('btn-disable');
                            $("#btnBacktoQuay").addClass('mobile-btn');
                            $("#btnsupEndTime").removeClass('btn-disable');
                            $("#btnsupEndTime").addClass('mobile-btn');
                        }
                    });

                }
            }
        }

        self.GetBackToQuayTime = function (model) {
            self.isSuccess(false);
            if ($("#btnBacktoQuay").prop('class') == 'mobile-btn') {

                var ScheduledTaskExecutionVO = {
                    ResourceAllocationID: model.ResourceAllocationID(),
                    FieldName: 'BackToQuay',
                    FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                    OperationType: model.OperationType(),
                    ValPKID: model.ValPKID()
                };
                model.BackToQuay(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                self.SaveDataOnEachClick(ScheduledTaskExecutionVO);

                $(document).ajaxStop(function () {
                    if (self.isSuccess()) {
                        $("#btnBacktoQuay").removeClass('mobile-btn');
                        $("#btnBacktoQuay").addClass('btn-disable');
                        $("#SaveSupExittab").removeClass('save-exit-disable');
                        $("#SaveSupExittab").addClass('save-exit-btn');
                    }
                });
            }
        }

        self.GetFirstLineOffTime = function (model) {
            self.isSuccess(false);
            if ($("#btnFirstLineoff").prop('class') == 'mobile-btn') {
                var ScheduledTaskExecutionVO = {
                    ResourceAllocationID: model.ResourceAllocationID(),
                    FieldName: 'FirstLineOut',
                    FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                    OperationType: model.OperationType(),
                    ValPKID: model.ValPKID()
                };
                model.FirstLineOut(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                self.SaveDataOnEachClick(ScheduledTaskExecutionVO);

                $(document).ajaxStop(function () {
                    if (self.isSuccess()) {
                        $("#btnFirstLineoff").removeClass('mobile-btn');
                        $("#btnFirstLineoff").addClass('btn-disable');

                        if (model.MomentType() != 'ARMV') {
                            $("#btnLastLineoff").removeClass('btn-disable');
                            $("#btnLastLineoff").addClass('mobile-btn');
                        }
                    }
                });
            }
        }

        self.GetLastLineOffTime = function (model) {
            self.isSuccess(false);
            if ($("#btnLastLineoff").prop('class') == 'mobile-btn') {
                var ScheduledTaskExecutionVO = {
                    ResourceAllocationID: model.ResourceAllocationID(),
                    FieldName: 'LastLineOut',
                    FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                    OperationType: model.OperationType(),
                    ValPKID: model.ValPKID()
                };
                model.LastLineOut(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                $(document).ajaxStop(function () {
                    if (self.isSuccess()) {
                        $("#btnLastLineoff").removeClass('mobile-btn');
                        $("#btnLastLineoff").addClass('btn-disable');

                        if (model.MomentType() == 'SGMV') {
                            $("#btnEndTime").removeClass('btn-disable');
                            $("#btnEndTime").addClass('mobile-btn');
                        }
                        else {
                            $("#btnFirstlinein").removeClass('btn-disable');
                            $("#btnFirstlinein").addClass('mobile-btn');
                        }
                    }
                });
            }
        }

        self.GetFirstLineInime = function (model) {
            self.isSuccess(false);
            if ($("#btnFirstlinein").prop('class') == 'mobile-btn') {
                var ScheduledTaskExecutionVO = {
                    ResourceAllocationID: model.ResourceAllocationID(),
                    FieldName: 'FirstLineIn',
                    FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                    OperationType: model.OperationType(),
                    ValPKID: model.ValPKID()
                };
                model.FirstLineIn(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                $(document).ajaxStop(function () {
                    if (self.isSuccess()) {
                        $("#divFirstlinein").prop('disabled', true);
                        $("#divLastlinein").prop('disabled', false);

                        $("#btnFirstlinein").removeClass('mobile-btn');
                        $("#btnFirstlinein").addClass('btn-disable');
                        $("#btnLastlinein").removeClass('btn-disable');
                        $("#btnLastlinein").addClass('mobile-btn');
                    }
                });

            }
        }

        self.GetLastLineInime = function (model) {
            self.isSuccess(false);
            if ($("#btnLastlinein").prop('class') == 'mobile-btn') {
                var ScheduledTaskExecutionVO = {
                    ResourceAllocationID: model.ResourceAllocationID(),
                    FieldName: 'LastLineIn',
                    FieldValue: moment(new Date()).format('YYYY-MM-DD HH:mm'),
                    OperationType: model.OperationType(),
                    ValPKID: model.ValPKID()
                };
                model.LastLineIn(moment(new Date()).format('YYYY-MM-DD HH:mm'));
                self.SaveDataOnEachClick(ScheduledTaskExecutionVO);
                $(document).ajaxStop(function () {
                    if (self.isSuccess()) {
                        $("#divLastlinein").prop('disabled', true);
                        $("#divEndTime").prop('disabled', false);

                        $("#btnLastlinein").removeClass('mobile-btn');
                        $("#btnLastlinein").addClass('btn-disable');
                        $("#btnEndTime").removeClass('btn-disable');
                        $("#btnEndTime").addClass('mobile-btn');
                    }
                });

            }

        }

        var labelNames = [];
        self.SaveDataOnEachClick = function (model) {
            if (Modernizr.localstorage) {
                if (navigator.onLine) {
                    $.ajax({
                        type: "POST",
                        url: '/api/MobileScheduledTasks/PostMobileScheduledTaskExecution',
                        data: model,
                        dataType: 'json',
                        error: function (data) {
                            toastr.error(data.responseText);
                            self.isSuccess(false);
                            return false;
                        },
                        success: function (data) {
                            toastr.success("Saved to server", "Success");
                            self.isSuccess(true);
                            return true;

                        },
                        async: true
                    });
                }
                else {
                    localStorage.setItem(model.FieldName + model.ResourceAllocationID, JSON.stringify(model));

                    labelNames.push(model.FieldName + model.ResourceAllocationID);
                    localStorage.setItem("labelnames", JSON.stringify(labelNames));
                }
            }
            else {
                toastr.warning("Application requires local storage.", "Error");
            }
        }

        self.SaveToServer = function (data) {

            var model = data;

            if (localStorage["labelnames"] != null) {
                labelNames = [];
                var fieldNames = JSON.parse(localStorage["labelnames"]);

                ko.utils.arrayForEach(fieldNames, function (fieldName) {

                    if (localStorage[fieldName] != null) {
                        var result = JSON.parse(localStorage[fieldName]);

                        $.ajax({
                            type: "POST",
                            url: '/api/MobileScheduledTasks/PostMobileScheduledTaskExecution',
                            data: result,
                            dataType: 'json',
                            success: function (data) {
                                localStorage.removeItem("labelnames");
                                localStorage.removeItem(fieldName);
                                toastr.success("Saved to server", "Success");
                            }
                        });
                    }
                });
            }

            if (localStorage["ScheduledTasksData"] != null) {
                var scheduledTasksDetails = JSON.parse(localStorage["ScheduledTasksData"]);
                ko.utils.arrayForEach(scheduledTasksDetails, function (scheduledTask) {

                    if (localStorage["SaveScheduledTaskExecuton" + scheduledTask.ResourceAllocationID] != null) {

                        model = JSON.parse(localStorage["SaveScheduledTaskExecuton" + scheduledTask.ResourceAllocationID]);
                        if (model.length > 0) {
                            if (model[0].OperationType == "PILT" || model[0].OperationType == "PIHE") {

                                $.ajax({
                                    type: "POST",
                                    url: '/api/MobileScheduledTasks/PostPilotageTaskExecution',
                                    data: model[0],
                                    dataType: 'json',
                                    success: function (data) {
                                        localStorage.removeItem("SaveScheduledTaskExecuton" + scheduledTask.ResourceAllocationID);
                                        self.LoadTasks();
                                        toastr.success("Saved to server", "Success");
                                    }
                                });
                            }

                            if (model[0].OperationType == "TGWR" || model[0].OperationType == "WTST" || model[0].OperationType == "FCST") {
                                $.ajax({
                                    type: "POST",
                                    url: '/api/MobileScheduledTasks/PostTugOrWorkBoatTaskExecution',
                                    data: model[0],
                                    dataType: 'json',
                                    success: function (data) {
                                        localStorage.removeItem("SaveScheduledTaskExecuton" + scheduledTask.ResourceAllocationID);
                                        self.LoadTasks();
                                        toastr.success("Saved to server", "Success");
                                    }
                                });
                            }
                        }
                    }

                    if (localStorage["PostBerthingDetails" + scheduledTask.ResourceAllocationID] != null) {
                        var berthingData = JSON.parse(localStorage["PostBerthingDetails" + scheduledTask.ResourceAllocationID]);
                        if (berthingData != null) {
                            $.ajax({
                                type: "POST",
                                url: '/api/MobileScheduledTasks/PostBerthingDetails',
                                data: berthingData,
                                dataType: 'json',
                                success: function (data) {
                                    localStorage.removeItem("PostBerthingDetails" + scheduledTask.ResourceAllocationID);
                                    self.LoadTasks();
                                    toastr.success("Saved to server", "Success");
                                }
                            });
                        }
                    }
                });
            }
        }


        self.LoadFromBollards = function (event) {
            if (Modernizr.localstorage) {
                if (localStorage["BerthswithBollardsData"] != null) {
                    var berthswithBollardsData = JSON.parse(localStorage["BerthswithBollardsData"]);
                    if (berthswithBollardsData != null) {
                        var berthSelected = berthswithBollardsData.filter(function (item) {
                            return item.BerthKey === event.FromBerthKey();
                        })[0];
                        self.FromBollards(ko.toJS(berthSelected.Bollards));
                    }
                }
                else {
                    $.ajax({
                        type: "GET",
                        url: '/api/GetBollardsInBerthsDetails',
                        data: { Id: event.FromBerthKey() },
                        dataType: 'json',
                        success: function (data) {
                            self.FromBollards(data);
                        }
                    });
                }
            }
            else {
                toastr.warning("Application requires local storage.", "Error");
            }
        }

        self.LoadToBollards = function (event) {
            if (Modernizr.localstorage) {
                if (localStorage["BerthswithBollardsData"] != null) {
                    var berthswithBollardsData = JSON.parse(localStorage["BerthswithBollardsData"]);
                    if (berthswithBollardsData != null) {
                        var berthSelected = berthswithBollardsData.filter(function (item) {
                            return item.BerthKey === event.ToBerthKey();
                        })[0];
                        self.ToBollards(ko.toJS(berthSelected.Bollards));
                    }
                }
                else {
                    $.ajax({
                        type: "GET",
                        url: '/api/GetBollardsInBerthsDetails',
                        data: { Id: event.ToBerthKey() },
                        dataType: 'json',
                        success: function (data) {
                            self.ToBollards(data);
                        }
                    });
                }
            } else {
                toastr.warning("Application requires local storage.", "Error");
            }
        }


        self.SaveScheduledTaskExecutonClicked = function (data) {
            self.isSuccess(false);
            if (data.OperationType() == 'PILT' || data.OperationType() == 'TGWR' || data.OperationType() == 'WTST' || data.OperationType() == 'FCST' || data.OperationType() == 'PIHE') {
                if (data.OperationType() == 'FCST') {
                    if (data.Extend()) {
                        if (data.BackToQuay() != null) {
                            if ($("#SaveSupExittab").prop('class') == 'save-exit-btn') {
                                window.location.href = "#Scheduled-Tasks";
                                self.SaveScheduledTaskExecuton(data);
                                $(document).ajaxStop(function () {
                                    if (self.isSuccess()) {
                                        $("#SaveSupExittab").removeClass('save-exit-btn');
                                        $("#SaveSupExittab").addClass('save-exit-disable');
                                    }
                                });
                            }
                        }
                        else {
                            toastr.warning("Please Enter the BackToQuay.", "Scheduled Task Execution");
                        }
                    }
                    else {
                        if ($("#SaveSupExittab").prop('class') == 'save-exit-btn') {
                            window.location.href = "#Scheduled-Tasks";
                            self.SaveScheduledTaskExecuton(data);
                            $(document).ajaxStop(function () {
                                if (self.isSuccess()) {
                                    $("#SaveSupExittab").removeClass('save-exit-btn');
                                    $("#SaveSupExittab").addClass('save-exit-disable');
                                }
                            });
                        }
                    }
                } else {
                    if ($("#SaveSupExittab").prop('class') == 'save-exit-btn') {
                        window.location.href = "#Scheduled-Tasks";
                        self.SaveScheduledTaskExecuton(data);
                        $(document).ajaxStop(function () {
                            if (self.isSuccess()) {
                                $("#SaveSupExittab").removeClass('save-exit-btn');
                                $("#SaveSupExittab").addClass('save-exit-disable');
                            }
                        });
                    }
                }
            }
        }

        self.SaveScheduledTaskExecuton = function (model) {

            if (Modernizr.localstorage) {

                if (navigator.onLine) {
                    self.isLoading(true);
                    if (model.OperationType() == "PILT" || model.OperationType() == "PIHE") {

                        $.ajax({
                            type: "POST",
                            url: '/api/MobileScheduledTasks/PostPilotageTaskExecution',
                            data: model,
                            dataType: 'json',
                            error: function (data) {
                                toastr.error(data.responseText);
                                self.isSuccess(false);
                                return false;
                            },
                            success: function (data) {
                                self.isSuccess(true);
                                self.isLoading(false);
                                self.LoadTasks();
                                toastr.success("Saved to server", "Success");
                                return true;
                            }
                        });
                    }

                    if (model.OperationType() == "TGWR" || model.OperationType() == "WTST" || model.OperationType() == "FCST") {
                        $.ajax({
                            type: "POST",
                            url: '/api/MobileScheduledTasks/PostTugOrWorkBoatTaskExecution',
                            data: model,
                            dataType: 'json',
                            error: function (data) {
                                toastr.error(data.responseText);
                                self.isSuccess(false);
                                return false;
                            },
                            success: function (data) {
                                self.isSuccess(true);
                                self.isLoading(false);
                                self.LoadTasks();
                                toastr.success("Saved to server", "Success");
                                return true;
                            }
                        });
                    }

                }
                else {

                    var scheduleTaskDetails = JSON.parse(localStorage.getItem("TaskExecution" + model.ResourceAllocationID()));
                    if (scheduleTaskDetails.length > 0) {
                        if (model.StartTime() != null) {
                            scheduleTaskDetails[0].StartTime = model.StartTime();
                        }
                        if (model.EndTime() != null) {
                            scheduleTaskDetails[0].EndTime = model.EndTime();
                        }
                        if (model.Remarks() != null) {
                            scheduleTaskDetails[0].Remarks = model.Remarks();
                        }
                        if (model.Deficiencies() != null) {
                            scheduleTaskDetails[0].Deficiencies = model.Deficiencies();
                        }
                        if (model.OperationType() != null) {
                            scheduleTaskDetails[0].OperationType = model.OperationType();
                        }
                        if (model.OperationType() == "PILT" || model.OperationType() == "PIHE") {

                            if (model.PilotOnBoard() != null) {
                                scheduleTaskDetails[0].PilotOnBoard = model.PilotOnBoard();
                            }
                            if (model.PilotOff() != null) {
                                scheduleTaskDetails[0].PilotOff = model.PilotOff();
                            }
                            if (model.WaitingStartTime() != null) {
                                scheduleTaskDetails[0].WaitingStartTime = model.WaitingStartTime();
                            }
                            if (model.WaitingEndTime() != null) {
                                scheduleTaskDetails[0].WaitingEndTime = model.WaitingEndTime();
                            }
                            if (model.AdditionalTugs() != null) {
                                scheduleTaskDetails[0].AdditionalTugs = model.AdditionalTugs();
                            }
                            if (model.OffSteam() != null) {
                                scheduleTaskDetails[0].OffSteam = model.OffSteam();
                            }
                            if (model.MarineRevenueCleared() != null) {
                                scheduleTaskDetails[0].MarineRevenueCleared = model.MarineRevenueCleared();
                            }

                        }

                        if (model.OperationType() == "TGWR" || model.OperationType() == "WTST" || model.OperationType() == "FCST") {

                            if (model.LineUp() != null) {
                                scheduleTaskDetails[0].LineUp = model.LineUp();
                            }
                            if (model.LineDown() != null) {
                                scheduleTaskDetails[0].LineDown = model.LineDown();
                            }
                            if (model.PilotOn() != null) {
                                scheduleTaskDetails[0].PilotOn = model.PilotOn();
                            }
                            if (model.OpeningMeterReading() != null) {
                                scheduleTaskDetails[0].OpeningMeterReading = model.OpeningMeterReading();
                            }
                            if (model.ClosingMeterReading() != null) {
                                scheduleTaskDetails[0].ClosingMeterReading = model.ClosingMeterReading();
                            }
                            if (model.TotalDispensed() != null) {
                                scheduleTaskDetails[0].TotalDispensed = model.TotalDispensed();
                            }
                            if (model.MeterSerialNo() != null) {
                                scheduleTaskDetails[0].MeterSerialNo = model.MeterSerialNo();
                            }
                            if (model.FirstSwing() != null) {
                                scheduleTaskDetails[0].FirstSwing = model.FirstSwing();
                            }
                            if (model.LastSwing() != null) {
                                scheduleTaskDetails[0].LastSwing = model.LastSwing();
                            }
                            if (model.TimeAlongSide() != null) {
                                scheduleTaskDetails[0].TimeAlongSide = model.TimeAlongSide();
                            }
                        }

                        localStorage.setItem("SaveScheduledTaskExecuton" + model.ResourceAllocationID(), JSON.stringify(scheduleTaskDetails));
                    }
                }
            }
            else {
                toastr.warning("Application requires local storage.", "Error");
            }
        }


        self.SaveBerthingDetailsClicked = function (data) {
            self.isSuccess(false);
            if ($("#SaveExittab").prop('class') == 'save-exit-btn') {
                window.location.href = "#Scheduled-Tasks";
                self.SaveBerthingDetails(data);
                $(document).ajaxStop(function () {
                    if (self.isSuccess()) {
                        $("#SaveExittab").removeClass('save-exit-btn');
                        $("#SaveExittab").addClass('save-exit-disable');
                    }
                });
            }
        }

        self.SaveBerthingDetails = function (model) {

            if (Modernizr.localstorage) {
                if (navigator.onLine) {
                    self.isLoading(true);
                    $.ajax({
                        type: "POST",
                        url: '/api/MobileScheduledTasks/PostBerthingDetails',
                        data: model,
                        dataType: 'json',
                        error: function (data) {
                            toastr.error(data.responseText);
                            self.isSuccess(false);
                            return false;
                        },
                        success: function (data) {
                            self.isSuccess(true);
                            self.isLoading(false);
                            self.LoadTasks();
                            toastr.success("Saved to server", "Success");
                            return true;
                        }
                    });
                }
                else {

                    var ShiftingBerthingTaskExecutionVO = {
                        ResourceAllocationID: model.ResourceAllocationID(),
                        StartTime: model.StartTime(),
                        EndTime: model.EndTime(),
                        BerthingSide: model.BerthingSide(),
                        FirstLineIn: model.FirstLineIn(),
                        LastLineIn: model.LastLineIn(),
                        FirstLineOut: model.FirstLineOut(),
                        LastLineOut: model.LastLineOut(),
                        ForwardDraft: model.ForwardDraft(),
                        AftDraft: model.AftDraft(),
                        Remarks: model.Remarks(),
                        Deficiencies: model.Deficiencies(),
                        ValPKID: model.ValPKID(),
                        FromBerthKey: model.FromBerthKey(),
                        ToBerthKey: model.ToBerthKey(),
                        FromBolardKey: model.FromBolardKey(),
                        ToBolardKey: model.ToBolardKey(),
                        MooringBolardBowKey: model.MooringBolardBowKey(),
                        MooringBolardStemKey: model.MooringBolardStemKey(),
                        OperationType: model.OperationType()
                    };

                    localStorage.setItem("PostBerthingDetails" + model.ResourceAllocationID(), JSON.stringify(ShiftingBerthingTaskExecutionVO));
                }
            }
            else {
                toastr.warning("Application requires local storage.", "Error");
            }
        }

        self.CalcTotalDispensed = function (data, event) {

            if (data.OpeningMeterReading() != null && data.OpeningMeterReading() != "") {
                if (parseFloat(data.ClosingMeterReading()) > parseFloat(data.OpeningMeterReading())) {
                    var opngMtrRdg = data.OpeningMeterReading();
                    var clsgMtrRdg = data.ClosingMeterReading();
                    var ttlDispensed = (clsgMtrRdg - opngMtrRdg).toFixed(2);
                    //self.scheduledTaskExecutionModel().TotalDispensed(ttlDispensed);// = ttlDispensed;
                    data.TotalDispensed(ttlDispensed);// = ttlDispensed;
                    $('#TotalDispensed').val(ttlDispensed);
                }
                else {
                    toastr.warning("Closing meter reading should be greater than Opening meter reading", "Scheduled Task Execution");
                    self.scheduledTaskExecutionModel().ClosingMeterReading('');
                    self.scheduledTaskExecutionModel().OpeningMeterReading('');
                    $('#OpeningMeterReading').val('');
                    $('#ClosingMeterReading').val('');
                }

            } else {
                toastr.warning("Please enter the Opening meter reading", "Scheduled Task Execution");
                self.scheduledTaskExecutionModel().ClosingMeterReading('');
                self.scheduledTaskExecutionModel().TotalDispensed('');
                $('#TotalDispensed').val('');
                $('#ClosingMeterReading').val('');
            }
        }

        self.GetBollardlength = function (data, event) {
            if (data.ToBolardKey() != null && data.ToBolardKey() != "" && data.ToBolardKey() != undefined) {
                if (data.FromBolardKey() != null && data.FromBolardKey() != "" && data.FromBolardKey() != undefined) {
                    if (localStorage["BerthswithBollardsData"] != null) {
                        var berthswithBollardsData = JSON.parse(localStorage["BerthswithBollardsData"]);
                        if (berthswithBollardsData != null) {
                            var fromberthSelected = berthswithBollardsData.filter(function (item) {
                                return item.BerthKey === data.FromBerthKey();
                            })[0];

                            var frombollardSelected = fromberthSelected.Bollards.filter(function (item) {
                                return item.BolardKey === data.FromBolardKey();
                            })[0];

                            var toberthSelected = berthswithBollardsData.filter(function (item) {
                                return item.BerthKey === data.ToBerthKey();
                            })[0];

                            var tobollardSelected = toberthSelected.Bollards.filter(function (item) {
                                return item.BolardKey === data.ToBolardKey();
                            })[0];

                            var bollardLength = (tobollardSelected.ToMeter - frombollardSelected.FromMeter).toFixed(2);

                            if (data.VesselLength() > bollardLength) {
                                toastr.warning("Bollard length should be greater than vessel length.", "Scheduled Task Execution");
                                self.scheduledTaskExecutionModel().ToBolardKey('');
                                $('#selectedToBolardKey').val('');
                            }
                        }
                    }
                }
                else {
                    toastr.warning("Please select From Bollard.", "Scheduled Task Execution");
                    self.scheduledTaskExecutionModel().ToBolardKey('');
                    $('#selectedToBolardKey').val('');
                }
            }
        }

        ////applicationCache not using instead used localstorage, but still need to check.
        if (window.applicationCache != undefined) {

            window.applicationCache.addEventListener('updateready', onUpdateReady);
            if (window.applicationCache.status === window.applicationCache.UPDATEREADY) {
                onUpdateReady();
            }
            window.applicationCache.onupdateready = function (e) {
                applicationCache.swapCache();
                window.location.reload();
            }
        }
        function reportOnlineStatus() {
            if (navigator.onLine) {
                toastr.success("Online", "Online Status");
            }
            else {
                toastr.error("Offline", "Online Status");
            }
        }
        window.addEventListener("online", function (e) {
            reportOnlineStatus();
            self.SaveToServer();
        }, true);

        window.addEventListener("offline", function (e) {
            reportOnlineStatus();
        }, true);

        function onUpdateReady() {
            toastr.success('added');
        }

        /// Schedule task ends here


        /// Diving request Start here

        self.LoadDivingRequestData = function () {
            var job = $.connection.chatHub;

            // Declare a function on the job hub so the server can invoke it
            job.client.brodcastNews = function () {
                // getData();
                getDivingData();
            };

            // Start the connection
            $.connection.hub.start();
            getDivingData();

            function getDivingData() {

                var $tbl = $('#tblDivReq');
                $.ajax({

                    url: '../api/DivingRequestsForScroll',
                    type: 'GET',
                    datatype: 'json',
                    success: function (data) {
                        self.divingRequestDetails(data);
                    }
                });
            }
        };

        ///Diving Request ends here


        // Numeric 
        self.ValidateNumeric = function (data, event) {
            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            //charcheck = /[0-9]/;
            charcheck = /[0-9.\b]/;
            return charcheck.test(keychar);
        }

        // Alphanumeric
        self.ValidateAlphaNumeric = function (data, event) {

            CutPaste();
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /^[a-zA-Z0-9]/;
            return charcheck.test(keychar);
        }

        //Accept numeric and decimal 
        self.Validatenumericdecimal = function (data, event) {
            if (window.event) // IE
                keynum = event.keyCode;
            else if (event.which) // Netscape/Firefox/Opera
                keynum = event.which;
            keychar = String.fromCharCode(keynum);
            charcheck = /[0-9.\b]/;
            return charcheck.test(keychar);
        }


        ko.bindingHandlers.loadingWhen = {

            // any ViewModel using this extension needs a property called isLoading
            // the div tag to contain the loaded content uses a data-bind="loadingWhen: isLoading"
            init: function (element) {
                var
                    $element = $(element),
                    currentPosition = $element.css("position")
                $loader = $("<div>").addClass("loading-loader").hide();

                //add the loader
                $element.append($loader);
                //make sure that we can absolutely position the loader against the original element
                if (currentPosition == "auto" || currentPosition == "static")
                    $element.css("position", "static");

                //center the loader
                $loader.css({
                    position: "absolute",
                    top: "50%",
                    left: "50%",
                    "margin-left": -($loader.width() / 2) + "px",
                    "margin-top": -($loader.height() / 2) + "px"
                });
            },
            update: function (element, valueAccessor) {
                var isLoading = ko.utils.unwrapObservable(valueAccessor()),
                    $element = $(element),
                    $childrenToHide = $element.children(":not(div.loading-loader)"),
                    $loader = $element.find("div.loading-loader");

                if (isLoading) {
                    $childrenToHide.css("visibility", "hidden").attr("disabled", "disabled");
                    $loader.show();
                }
                else {
                    $loader.fadeOut("fast");
                    $childrenToHide.css("visibility", "visible").removeAttr("disabled");
                }
            }
        };

        self.Initialize();
    }
    var vm = new MobileViewModel();
    ko.applyBindings(vm);

    function GetDateTime() {
        var now = new Date();
        var datetime = now.getFullYear() + '-' + (now.getMonth() + 1) + '-' + now.getDate();
        datetime += ' ' + now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();
        return datetime;
    }

    function CutPaste() {
        var controls = $(".form-control");
        controls.bind("paste", function () {
            return false;
        });
        controls.bind("drop", function () {
            return false;
        });
        controls.bind("cut", function () {
            return false;
        });
        controls.bind("copy", function () {
            return false;
        });
    }

});



