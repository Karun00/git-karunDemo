(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    // MobileConversationModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var MobileConversationModel = function (data) {
        var self = this;
        self.UserID = ko.observable(data ? data.UserID : "");
        self.UserName = ko.observable(data ? data.UserName : "");
        self.ConversationID = ko.observable(data ? data.ConversationID : "");
        self.selectedChoice = ko.observable();

        self.cache = function () { };
        self.set(data);
    }

    // Mobile conversation model
    var Conversation = function (data) {
        var self = this;
        self.UserID2 = ko.observable(data ? data.UserID : "");
    }

    //Mobile conversation reply model
    var ConversationReply = function (data) {
        var self = this;
        self.ConversationID = ko.observable(data ? data.ConversationID : "");
        self.Reply = ko.observable(data ? data.Reply : "");
        self.ConversationReplyID = ko.observable(data ? data.Reply : "");
        self.IsRead = ko.observable(data ? data.Reply : "");
    }
    ipmsRoot.MobileConversationModel = MobileConversationModel;
    ipmsRoot.Conversation = Conversation;
    ipmsRoot.ConversationReply = ConversationReply;
}(window.IPMSROOT));

IPMSROOT.MobileConversationModel.prototype.set = function (data) {
    var self = this;
    self.UserID(data ? (data.UserID || null) : null);
    self.UserName(data ? (data.UserName || null) : null);
    self.cache.latestData = data;
}
IPMSROOT.MobileConversationModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}
