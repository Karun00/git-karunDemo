(function (ipmsRoot) {

    var AccountLoginModel = function (data) {
        $('#txtusername').focus();
        var ReturnUrl = $('#spanReturnUrl').text();
        var user = $('#cusername').text();
        var pwd = $('#cpwd').text();
        var self = this;
        self.RememberMe = ko.observable();
        self.IsMobile = ko.observable();

        if ((user != "" && user != null) && (pwd != "" && pwd != null)) {
            self.username = ko.observable(user).extend({ required: { message: '* User name is required' } });
            self.password = ko.observable(pwd).extend({ required: { message: '* Password is required' } });
            self.RememberMe = ko.observable(true);
            $('#chkRemember').closest('span').addClass('checked');
        }
        else {
            self.username = ko.observable(data ? data.username : "").extend({ required: { message: '* User name is required' } });
            self.password = ko.observable(data ? data.password : "").extend({ required: { message: '* Password is required' } });
            self.RememberMe = ko.observable(data ? data.RememberMe : false);

            self.cache = function () { };
            self.set(data);
        }

        self.ReturnUrl = ko.observable(ReturnUrl);
    }

    var ForgotPasswordModel = function (data) {
        var self = this;
        self.username = ko.observable(data ? data.username : "").extend({ required: { message: '* User name is required' } });
    }

    ipmsRoot.ForgotPasswordModel = ForgotPasswordModel;
    ipmsRoot.AccountLoginModel = AccountLoginModel;

}(window.IPMSROOT));

IPMSROOT.AccountLoginModel.prototype.set = function (data) {
    var self = this;
    self.username(data ? (data.username || "") : "");
    self.password(data ? (data.password || "") : "");
    self.RememberMe(data ? (data.RememberMe || "") : "");
    self.IsMobile(data ? (data.IsMobile || "") : "");

    self.cache.latestData = data;
}

IPMSROOT.AccountLoginModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}




