(function (IPMSRoot) {
    var MobileBerthViewModel = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.mobileBerthModel = ko.observable();
        self.masterQuays = ko.observableArray([]);
        self.Initialize = function () {
            self.LoadQuays();
        }

        // To get the quay details along with berth details
        self.LoadQuays = function () {
            self.viewModelHelper.apiGet('api/Mobile/GetQuaynames', null,
              function (result) {
                  self.masterQuays(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.Quay(item);
                  }));
                  self.mobileBerthModel(new IPMSROOT.MobileBerthModel(self.masterQuays()));
              });
        }

        self.Initialize();
    }

    IPMSRoot.MobileBerthViewModel = MobileBerthViewModel;
}(window.IPMSROOT));

