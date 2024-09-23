(function (IPMSRoot) {
    var MobileConversationViewModel = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.mobileConversationModel = ko.observable();
        self.getUserDetails = ko.observableArray();
        self.getConversationUserDetails = ko.observableArray();
        self.getConversations = ko.observableArray();
        self.getMessages = ko.observableArray();
        self.Initialize = function () {
            self.mobileConversationModel(new IPMSROOT.MobileConversationModel());
            self.viewMode = ko.observable(true);
            self.viewMode('List');
            self.GetUsers();
            self.GetConversationUsers();
            self.GetNewMessages();
        }

        // To get the list of users
        self.GetUsers = function () {
            self.viewModelHelper.apiGet('api/MobileConversation/GetUsers',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.getUserDetails);
              });
        }

        // To get the conversation users
        self.GetConversationUsers = function () {
            self.viewModelHelper.apiGet('api/MobileConversation/GetConversationUsers',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.getConversationUserDetails);
              });

        }

        // To get the list of new messages
        self.GetNewMessages = function () {
            self.viewModelHelper.apiGet('api/MobileConversation/GetNewMessages',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.getMessages);
              });

        }

        // To add a conversation
        self.addUser = function (model) {
            var Conversation = new IPMSROOT.Conversation();
            Conversation.UserID2(model.UserID);

            self.viewModelHelper.apiPost('api/MobileConversation/AddConversation',
            ko.toJSON(Conversation),
            function (result) {
                self.GetConversationUsers();
                self.GetUsers();
                $('#users').val('').focus()
            });
        }

        self.UserSelect = function (e) {
            var selecteddataItem = this.dataItem(e.item.index());
            self.mobileConversationModel().UserName(selecteddataItem);
            self.mobileConversationModel().UserID(selecteddataItem.UserID);
        }

        // To send a data
        self.sendtext = function (model) {
            var ConversationReply = new IPMSROOT.ConversationReply();
            ConversationReply.ConversationID($('#convID').val());
            ConversationReply.Reply($('#message').val());

            self.viewModelHelper.apiPost('api/MobileConversation/AddConversationReply',
           ko.toJSON(ConversationReply),
           function (result) {
           });

            var chat = $.connection.chatHub;
            chat.client.broadcastMessage = function (UserName, Message) {
                $('#discussion').append('<li><strong>' + htmlEncode(UserName) + '</strong>: ' + htmlEncode(Message) + '</li>');
                $('#message').val('').focus();
            };

            $.connection.hub.start().done(function () {
                chat.server.send($('#usID').val(), $('#message').val());
                $('#discussion').append('<li><strong>' + htmlEncode($('#loginname').text()) + '</strong>: ' + htmlEncode($('#message').val()) + '</li>');
                $('#message').val('').focus();
            });

            function htmlEncode(value) {
                var encodedValue = $('<div />').text(value).html();
                return encodedValue;
            }
        }

        // To get the list of conversation
        self.clickMe = function (data) {

            var id = '#' + data.UserName();
            $(id).hide();
            $('#convID').val(data.ConversationID());
            $('#usID').val(data.UserID());

            self.viewModelHelper.apiGet('api/MobileConversation/GetConversations',
                { ConversationID: data.ConversationID, UserID: data.UserID },
          function (result) {
              ko.mapping.fromJS(result, {}, self.getConversations);
              $('#discussion').text('');
          });

            self.viewMode('Chat');

        }

        function htmlEncode(value) {
            var encodedValue = $('<div />').text(value).html();
            return encodedValue;
        }

        self.chatBox = function () {
            self.viewMode('Form');
        }

        // To cancel a conversation
        self.cancel = function (data) {
            self.viewModelHelper.apiGet('api/MobileConversation/GetConversationReply', { ConversationID: $('#convID').val() },
           function (result) {
               self.GetNewMessages();
           })
            self.viewMode('List');
        }
        // To cancel a user list
        self.userCancel = function (data) {
            self.viewMode('List');
        }
        self.Initialize();
    }


    IPMSRoot.MobileConversationViewModel = MobileConversationViewModel;
}(window.IPMSROOT));

