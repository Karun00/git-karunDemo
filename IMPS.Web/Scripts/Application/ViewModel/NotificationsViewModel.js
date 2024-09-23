(function (IPMSRoot) {
    var NotificationViewModel = function () {
        var self = this;
        self.Portdata = ko.observableArray();
        self.RolesList = ko.observableArray();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.notificationModel = ko.observable(new IPMSROOT.NotificationModel());
        self.shouldShowDivAV = ko.observable(false);
        self.shouldShowDivVAC = ko.observable(false);
        self.shouldShowDivSR = ko.observable(false);
        self.shouldShowDivAN = ko.observable(false);
        self.GetNotificationList = ko.observableArray();
        self.arrayList = ko.observableArray();
        self.TaskCodeData = ko.observableArray();
        self.sysNotifications = ko.observableArray([]);
        self.sysNotificationsCount = ko.observable([]);
        self.getUserDetails = ko.observableArray();
        self.getConversationUserDetails = ko.observableArray();
        self.getConversations = ko.observableArray();
        self.getMessages = ko.observableArray([]);

        self.pendingcount = ko.observable();

        var selectedport = true;

        self.Initialize = function () {
            self.LoadPorts();
            self.GetNewMessages();
            //self.GetUsers();
            // self.LoadPendingTaskCount();
            //self.LoadPendingTasks(); //TODO: To be Unit Test while fixing the bug#4102 - Santosh

        }

        self.LoadPendingTaskCount = function () {
            self.viewModelHelper.apiGet('api/Account/GetPendingTaskCount',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.pendingcount);
              });
        }

        self.LoadSysNotifications = function () {
            self.viewModelHelper.apiGet('api/Mobile/GetNotifications',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.sysNotifications);
              }, null, null, false);
        }

        //self.LoadSysNotificationsCount = function () {

        //    self.viewModelHelper.apiGet('/api/Mobile/GetNewNotifications',
        //    null,
        //      function (result) {
        //          ko.mapping.fromJS(result, {}, self.sysNotificationsCount);
        //      });
        //}



        //self.LoadSysNotifications = function () {
        //    self.viewModelHelper.apiGet('api/Account/GetSystemNotifications',
        //    null,
        //      function (result) {
        //          ko.mapping.fromJS(result, {}, self.sysNotifications);
        //      });
        //}

        self.NotificationStatus = function (data) {

            self.viewModelHelper.apiPut('api/Mobile/PutNotificationsByID',
            ko.mapping.toJSON(data),
              function (result) {
                  self.LoadSysNotifications();
              });
        }

        var decryptresult = "";
        self.LoadPorts = function () {
            selectedport = true;
            //var uname = $("#username").text();
            var uname = $("#loginusername").text();
            self.viewModelHelper.apiGet('api/Account/GetUserPorts/' + uname,
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.Portdata);
              })

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
           
           //var a= $.cookie('singleValues',singleValues);
            
            if (selectedport == true) {

                var inSb = $.cookie('Port');
                if (inSb != undefined) {

                    // confirmation box - start
                    $.confirm({
                        'title': ' Port change confirmation',
                        'message': 'Are you sure want to change Port ?',
                        'buttons': {
                            'Yes': {
                                'class': 'blue',
                                'action': function () {
                                    var encryptresult = "";
                                    var xorKey = 6;

                                    //encrypted code
                                    for (i = 0; i < singleValues.length; ++i) {
                                        encryptresult += String.fromCharCode(xorKey ^ singleValues.charCodeAt(i));
                                    }
                                   
                                    $.cookie('Port', encryptresult, { path: '/' });

                                    window.location.href = "/DashBoard";
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
                    //confirmation box - end
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
                    if (decryptresult == val.PortCode()) {
                        flag = false;
                        first = val.PortCode();

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
                ///
            }
            selectedport = true;
            // var uname = $("#username").text();
            var uname = $("#loginusername").text();

            self.viewModelHelper.apiGet('api/Account/GetUserPorts/' + uname,
            null,
            function (result) {
                //   self.LoadPendingTasks();
                self.LoadSysNotifications();
                self.LoadPendingTaskCount()
                //  self.LoadSysNotificationsCount();
            })
        }

        self.GetViewList = ko.observableArray();

        self.EntityData = ko.observableArray([]);

        self.NumberOfNotification = ko.observableArray([]);

        self.LoadPendingTasks = function () {

            var strHash = document.location.pathname;

            var value = strHash.substring(strHash.lastIndexOf('/') + 1);

            self.viewModelHelper.apiGet('api/Account/GetPendingTasks',
                  null,
                    function (result) {
                        self.EntityData(ko.utils.arrayMap(result, function (item) {
                            return new IPMSRoot.NotificationModel(item);
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
                                            self.GetViewList.push({ TaskCode: tc[i], TaskName: taskname[i], APIUrl: action[i], ReferenceID: val.ReferenceID, PreviousRemarks: val.PreviousRemarks, Remarks: val.Remarks, WorkflowInstanceId: val.WorkflowInstanceId, PageUrl: val1.PageUrl, HasRemarks: val.hasremarks[i], WorkflowTaskCode: val.WorkflowTaskCode });
                                    };
                                }
                            });
                        });
                    });
            self.LoadPendingTaskCount();
        }

        self.Actiontask = function (model) {
            var strHash = document.location.pathname;
            var value = strHash.substring(strHash.lastIndexOf('/') + 1);
            var id = '#' + model.WorkflowInstanceId;
            var remarks = $(id).val();
            var tempRemarks = model.Remarks;
            var message = model.RequestName + " " + model.TaskDescription;
            var url = "/" + model.PageUrl + "/" + model.ReferenceID

            if (value == model.ReferenceID && strHash == url)
                model.Remarks = tempRemarks;
            else
                model.Remarks = remarks;

            if (typeof (model.Remarks) !== "undefined" && model.Remarks) {
                tempRemarks = model.Remarks;
            }

            var spanid = '#span' + model.WorkflowInstanceId;
            // if ((tempRemarks == "" || tempRemarks.length === 0) || (model.Remarks == "" || model.Remarks.length == null) && model.HasRemarks == "Y") {
            if ((model.Remarks == "" || model.Remarks.length == null) && model.HasRemarks == "Y") {
                $(spanid).text('Please enter Remarks');
                $(spanid).addClass("validationError1");
                return;
            }
            else {
                $(spanid).text(' ');
                $(spanid).removeClass("validationError1");

                // confirmation box - start

                self.viewModelHelper.apiGet('api/WorkFlowTaskStatus/' + model.ReferenceID + '/' + model.WorkflowInstanceId + '/' + model.TaskCode,
    null,
                function Message(data) {
                    if (data.WFTaskStatus == true) {

                        $.confirm({
                            'title': model.RequestName,
                            'message': 'Do you want to ' + model.TaskName + '?',
                            'buttons': {
                                'Yes': {
                                    'class': 'blue',
                                    'action': function () {
                                        self.viewModelHelper.apiPost(model.APIUrl, ko.mapping.toJSON(model), function Message(data) {
                                            toastr.options.closeButton = true;
                                            toastr.options.positionClass = "toast-top-right";
                                            toastr.success(message + " Successfully", "Pending Task");
                                            var url = "/" + model.PageUrl + "/" + model.ReferenceID
                                            if (value == model.ReferenceID && strHash == url)
                                                window.location.href = "/" + model.PageUrl
                                            self.NumberOfNotification.removeAll();
                                            self.LoadPendingTaskCount();
                                            self.LoadPendingTasks();
                                            self.cache = "";

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
                    }
                    else {
                        toastr.warning("Request is already processed by another User.", "Pending Task");
                       // return;
                        self.LoadPendingTasks();
                    }
                });
                //confirmation box - end
            }
        }
        self.Edit = function (model) {
            window.location.href = "/" + model.PageUrl + "/" + model.ReferenceID + "/" + model.WorkflowInstanceId;
        }
        self.View = function (model) {
            if (model.PageUrl == "ArrivalNotifications") {
                window.location.href = "/" + model.PageUrl + "/" + model.ReferenceID + "/" + model.WorkflowInstanceId;
            }
            else {
                window.location.href = "/" + model.PageUrl + "/" + model.ReferenceID;

            }
        }

        self.clickMe = function (data) {
            self.viewModelHelper.apiGet('api/MobileConversation/GetConversations', { ConversationID: data.ConversationID(), UserID: data.UserID() },
         function (result) {
             ko.mapping.fromJS(result, {}, self.getConversations);
         });
            $("#chat_div1").text('');

            var box = null;
            if (box != null) {
                box.chatbox("option", "boxManager").toggleBox();
            }

            $("#chat_div1").chatbox(
                {
                    id: "FirstUser",
                    user:
                    {
                        key: "value"
                    },
                    title: data.UserName(),
                    offset: 0,
                    messageSent: function (id, user, msg) {
                        self.sendtext(msg, data.ConversationID(), data.UserID(), data.UserName());
                    },
                    boxClosed: function () {
                        self.viewModelHelper.apiGet('api/MobileConversation/GetConversationReply', { ConversationID: data.ConversationID() },
                        function (result) {
                        });
                        self.GetNewMessages();
                        $("#chat_div1").text('');
                    }
                });

            $("#newtitle").text(data.UserName());
            self.GetNewMessages();
        }

        //self.GetUsers = function () {
        //    self.viewModelHelper.apiGet('api/MobileConversation/GetUsers',
        //    null,
        //      function (result) {
        //          ko.mapping.fromJS(result, {}, self.getUserDetails);
        //      });
        //}

        self.UserSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.notificationModel().UserName(selecteddataItem);
            self.notificationModel().UserID(selecteddataItem.UserID);
        }

        // To add a conversation
        self.addUser = function (model) {
            //self.AUValidation = ko.observable(model);
            //self.AUValidation().errors = ko.validation.group(self.AUValidation());
            //var errors = self.AUValidation().errors().length;

            if (model.UserID() == undefined || model.UserID() == "") {
                $('#spid1').text('Please Select User');
                return;
            }

            //  if (errors == 0) {

            var Conversation = new IPMSROOT.Conversation();
            Conversation.UserID2(model.UserID);

            self.viewModelHelper.apiPost('api/MobileConversation/AddConversation',
            ko.toJSON(Conversation),
            function (result) {
                $('#users').val('');
                $('#spid1').text('');
                $(".close").trigger("click");
            });
            //}
            //else {
            //    self.AUValidation().errors.showAllMessages();
            //    return;
            //}
        }

        self.cancelUser = function () {

            $('#users').val('');
            $('#spid1').text('');
            $(".close").trigger("click");
        }

        self.GetNewMessages = function () {
            self.viewModelHelper.apiGet('api/MobileConversation/GetNewMessages',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.getMessages);
              });
        }

        // To send a data
        self.sendtext = function (msg, conversationID, userID, userName) {
            var ConversationReply = new IPMSROOT.ConversationReply();
            ConversationReply.ConversationID(conversationID);
            ConversationReply.Reply(msg);

            self.viewModelHelper.apiPost('api/MobileConversation/AddConversationReply',
               ko.toJSON(ConversationReply),
                  function (result) {
                  });

            var chat = $.connection.chatHub;
            chat.client.broadcastMessage = function (UserName, Message) {
                $("#newtitle").text(UserName);
                $("#chat_div1").chatbox("option", "boxManager").addMsg(UserName, Message);                
            };

            $.connection.hub.start().done(function () {
                chat.server.send(userName, msg);
                $("#chat_div1").chatbox("option", "boxManager").addMsg($('#loginname').text(), msg);
            });
        }

        self.GetConversationUsers = function () {
            self.viewModelHelper.apiGet('api/MobileConversation/GetConversationUsers',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.getConversationUserDetails);
              });
        }


        self.Initialize();
    }
    IPMSRoot.NotificationViewModel = NotificationViewModel;

}(window.IPMSROOT));

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
