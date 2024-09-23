(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    // MobileBerthModel Fills  from viewmodel to model  and send data to view and Validates cshtml Controls data
    var MobileBerthModel = function (data) {
        var self = this;
        self.Berths = ko.observableArray();
        self.selectedQuayKey = ko.observable("");
        self.selectedQuayKey.subscribe(function (key) {
            if (key !== undefined) {
                var quaySelected = data.filter(function (item) {
                    return item.QuayKey() === key;
                })[0];
                self.Berths(ko.toJS(quaySelected.Berths));
            }
        });
        self.selectedBerth = ko.observable();
        self.selectedBerthKey = ko.observable("");
        self.selectedBerthKey.subscribe(function (key) {
            if (key !== undefined) {
                var berthSelected = self.Berths().filter(function (item) {
                    return item.BerthKey === key;
                })[0];
                self.selectedBerth(ko.toJS(berthSelected));
            }
            else {
                self.selectedBerth("");
            }
        });

        self.cache = function () { };
        self.set(data);
    }

    //Mobile Berth Model
    var Berth = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QueyCode = ko.observable(data ? data.QueyCode : "");
        self.BerthCode = ko.observable(data ? data.BerthCode : "");
        self.BerthName = ko.observable(data ? data.BerthName : "");
        self.BerthKey = ko.observable(data ? data.BerthKey : "");
        self.Draftm = ko.observable(data ? data.Draftm : "");
        self.Lengthm = ko.observable(data ? data.Lengthm : "");
        self.CargoDetails = ko.observable(data ? data.CargoDetails : "");
    }

    //Mobile Quay Model
    var Quay = function (data) {
        var self = this;
        self.PortCode = ko.observable(data ? data.PortCode : "");
        self.QuayCode = ko.observable(data ? data.QuayCode : "");
        self.QuayName = ko.observable(data ? data.QuayName : "");
        self.QuayKey = ko.observable(data ? data.QuayKey : "");
        self.Berths = ko.observableArray(data ? (data.berthlist ? $.map(data.berthlist, function (item) { return new Berth(item) }) : []) : []);
    }

    //Mobile Document Model
    var Document = function (data) {
        var self = this;
        self.DocumentID = ko.observable(data ? data.DocumentID : '');
        self.DocumentType = ko.observable(data ? data.DocumentType : '');
        self.FileName = ko.observable(data ? data.FileName : '');
        self.DATA = ko.observable(data ? data.DATA : '');
    }

    ipmsRoot.MobileBerthModel = MobileBerthModel;
    ipmsRoot.Berth = Berth;
    ipmsRoot.Quay = Quay;
    ipmsRoot.Document = Document;
}(window.IPMSROOT));

IPMSROOT.MobileBerthModel.prototype.set = function (data) {
    var self = this;
    self.cache.latestData = data;
}
IPMSROOT.MobileBerthModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}

