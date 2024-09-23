(function (ipmsRoot) {
    
    var UserPreferenceModel = function (data) {
        var self = this;

        self.Entityname = ko.observable();
        self.EntityCode = ko.observable();
        self.allSelected = ko.observableArray([]);

        self.UserEntityname = ko.observable();
        self.UserEntityCode = ko.observable();
        self.itemSelected = ko.observableArray([]);


        self.DashBoardConfig = ko.observable();
        self.Userid = ko.observable();

        self.cache = function () { };
        self.set(data);

    };


    ipmsRoot.UserPreferenceModel = UserPreferenceModel;

}(window.IPMSROOT));

IPMSROOT.UserPreferenceModel.prototype.set = function (data) {
    var self = this;


    self.Entityname(data ? data.Entityname : "");
    self.EntityCode(data ? data.EntityCode : "");
    self.allSelected(data ? data.allSelected : "");

    self.itemSelected(data ? data.itemSelected : "");

    self.Userid(data ? data.Userid : "");

    self.cache.latestData = data;

}
IPMSROOT.UserPreferenceModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}


