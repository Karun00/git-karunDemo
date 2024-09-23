(function (IPMSRoot) {

    var MaterialCodeMasterViewModel = function () {

        var self = this;
        $('#spnTitile').html("Material Code");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.materialcodeModel = ko.observable();
        self.MaterialCodeList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveUpdateDisabled = ko.observable(false);
        self.IsUnique = ko.observable(true);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();


        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.materialcodeModel(new IPMSROOT.MaterialCodeMasterModel());
            self.LoadMaterialCodes();
            self.viewMode('List');

        }

        self.LoadMaterialCodes = function () {
            self.viewModelHelper.apiGet('api/MaterialCodeMaster',
            null,
              function (result) {

                  self.MaterialCodeList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.MaterialCodeMasterModel(item);
                  }));


              });

        }













        self.Initialize();
    }
    IPMSRoot.MaterialCodeMasterViewModel = MaterialCodeMasterViewModel;


}(window.IPMSROOT));



