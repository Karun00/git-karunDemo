(function (IPMSRoot) {

    var VesselSAPPostingViewModel = function (data) {

        var self = this;
        $('#spnTitile').html("Vessel SAP Posting");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.SAPPostingList = ko.observableArray();
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable();
        self.IsModified = ko.observable(false);
        self.IsSaveDisabled = ko.observable(true);
        self.IsUnique = ko.observable(true);
        self.UniqueCodeVisible = ko.observable(false);
        self.UniqueNameVisible = ko.observable(false);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.vcnList = ko.observableArray();
        self.IsEditable = ko.observable(true);
        self.sapPostingReferenceData = ko.observable();
        self.sapPostingModelItem = ko.observable();
        self.vesselSAPPostingModel = ko.observable();
        self.LoadSAPVessels = ko.observable();
        self.IsCancel = ko.observable(true);

        self.AccountList = ko.observableArray();

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.vesselSAPPostingModel(new IPMSROOT.VesselSAPPostingModel());
            self.LoadDetails();
            self.viewMode('List');

            self.IsSaveDisabled(false);
        }

        self.LoadDetails = function () {
            self.viewModelHelper.apiGet('api/GetSAPPostVessels',
                 null,
                  function (result) {
                      self.LoadSAPVessels(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.VesselSAPPostingModel(item);
                      }));

                  });
        }

        self.addNew = function () {
            self.vesselSAPPostingModel(new IPMSROOT.VesselSAPPostingModel(undefined));        
            self.viewMode('Form');
            self.IsSaveDisabled(true);
            self.editableView(true);     
            self.IsCodeEnable(true);
            self.viewMode('Form');
        }

        self.RepostSAP = function (model) {
            self.IsSaveDisabled(true);
            self.IsReset(false);
            view(model);
        }

        self.ViewSAP = function (model) {

                self.IsSaveDisabled(false);
                self.IsReset(false);

                view(model);
    //        var columnName = "IM";
    //        var SearchValue = model.IMONo();

    //        self.viewModelHelper.apiGet('api/VesselSAPPosting/SAPVesselData/' + columnName + '/' + SearchValue, null,
    //function (result1) {
    //    self.vesselSAPPostingModel(new IPMSRoot.VesselSAPPostingModel(result1[0]));
    //    self.vesselSAPPostingModel().SearchText(result1[0].VesselName);
    //}, null, null, false);
    //        self.viewMode('Form');

        }

        view = function (model) {
            var columnName = "IM";
            var SearchValue = model.IMONo();

            self.viewModelHelper.apiGet('api/VesselSAPPosting/SAPVesselData/' + columnName + '/' + SearchValue, null,
    function (result1) {
        self.vesselSAPPostingModel(new IPMSRoot.VesselSAPPostingModel(result1[0]));
        self.vesselSAPPostingModel().SearchText(result1[0].VesselName);
    }, null, null, false);
            self.viewMode('Form');
        }

        self.Reset = function () {
            self.vesselSAPPostingModel().reset();
        }

        self.Cancel = function () {
            self.viewMode('List');
            self.vesselSAPPostingModel().reset();
        }

        VesselNameBlur = function () {
            if (self.vesselSAPPostingModel().VesselID() == "") {
                self.vesselSAPPostingModel().VesselID('');
                self.vesselSAPPostingModel().VesselName('');
                self.vesselSAPPostingModel().IMONo('');
                self.vesselSAPPostingModel().VesselType('');
                self.vesselSAPPostingModel().VesselTypeName('');
                self.vesselSAPPostingModel().LengthOverallInM('');
                self.vesselSAPPostingModel().BeamInM('');
                self.vesselSAPPostingModel().CallSign('');
                self.vesselSAPPostingModel().VesselNationality('');
                self.vesselSAPPostingModel().GrossRegisteredTonnageInMT('');
                self.vesselSAPPostingModel().DeadWeightTonnageInMT('');
                self.vesselSAPPostingModel().SAPAccountNo('');
                self.vesselSAPPostingModel().TransmitData("");
                self.IsSaveDisabled(false);
            }
        }

        self.SaveSAPPosting = function () {

            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var dataModel = self.vesselSAPPostingModel();

            var xmlstring = dataModel.TransmitData();

            //if (dataModel.IsVesselSeened() == '02') {
            //    xmlstring = xmlstring.replace("<IVESIND>02</IVESIND>", "<IVESIND>01</IVESIND>");
            //}
            //else {
            //    xmlstring = xmlstring.replace("<IVESIND>01</IVESIND>", "<IVESIND>02</IVESIND>");
            //}


            dataModel.TransmitData(xmlstring);

            var transMsg = dataModel.TransmitData();

            var vtype = $(transMsg).find("IVESTYPE").text();
            if (vtype == '' || vtype == null) {
                toastr.warning("Vessel Type should not be empty");
                return;
            }

            if (dataModel != null && dataModel != undefined) {
                self.viewModelHelper.apiPost('api/VesselSAPPosting', ko.mapping.toJSON(dataModel), function Message(data) {
                    toastr.success("Vessel SAP details posted successfully.", "SAP Posting");
                    self.LoadDetails();
                    self.viewMode('List');
                    self.IsSaveDisabled(false);
                    self.IsSaveDisabled(false);
                });
            }
            else {
                self.SAPPostingValidation().errors.showAllMessages();
                toastr.warning("You have some form errors. Please check below.");
                return;
            }
        }

        VesselNameKeypress = function () {
            self.vesselSAPPostingModel().VesselID('');
            self.vesselSAPPostingModel().VesselName('');
            self.vesselSAPPostingModel().IMONo('');
            self.vesselSAPPostingModel().VesselType('');
            self.vesselSAPPostingModel().VesselTypeName('');
            self.vesselSAPPostingModel().LengthOverallInM('');
            self.vesselSAPPostingModel().BeamInM('');
            self.vesselSAPPostingModel().CallSign('');
            self.vesselSAPPostingModel().VesselNationality('');
            self.vesselSAPPostingModel().GrossRegisteredTonnageInMT('');
            self.vesselSAPPostingModel().DeadWeightTonnageInMT('');
            self.vesselSAPPostingModel().SAPAccountNo('');
            self.vesselSAPPostingModel().TransmitData("");
            self.vesselSAPPostingModel().VSNAME("");
            self.IsSaveDisabled(false);
        }

        self.VesselSelect = function (e) {

            var selecteddataItem = this.dataItem(e.item.index());
            self.vesselSAPPostingModel().VesselID(selecteddataItem.VesselID);
            self.vesselSAPPostingModel().VesselName(selecteddataItem.VesselName);
            self.vesselSAPPostingModel().IMONo(selecteddataItem.IMONo);
            self.vesselSAPPostingModel().VesselType(selecteddataItem.VesselType);


            self.vesselSAPPostingModel().VesselTypeName(selecteddataItem.VesselTypeName);
            self.vesselSAPPostingModel().LengthOverallInM(selecteddataItem.LengthOverallInM);
            self.vesselSAPPostingModel().BeamInM(selecteddataItem.BeamInM);
            self.vesselSAPPostingModel().CallSign(selecteddataItem.CallSign);
            self.vesselSAPPostingModel().VesselNationality(selecteddataItem.VesselNationality);
            self.vesselSAPPostingModel().GrossRegisteredTonnageInMT(selecteddataItem.GrossRegisteredTonnageInMT);


            self.vesselSAPPostingModel().DeadWeightTonnageInMT(selecteddataItem.DeadWeightTonnageInMT);
            self.vesselSAPPostingModel().SAPAccountNo(selecteddataItem.SAPAccountNo);
            
            self.vesselSAPPostingModel().VSNAME(selecteddataItem.VSNAME);

            if (selecteddataItem.sappoststatus == "SNEW" || selecteddataItem.sappoststatus == "SERR" || selecteddataItem.sappoststatus == "SPOS") {
                self.vesselSAPPostingModel().TransmitData("");
                toastr.warning("Vessel is already posted process in progress");
                self.IsSaveDisabled(false);
            }
            else {
                if (selecteddataItem.SAPAccountNo == "" || selecteddataItem.SAPAccountNo == null) {
                    self.vesselSAPPostingModel().TransmitData(selecteddataItem.TransmitData);
                    self.IsSaveDisabled(true);
                }
                else {
                    self.vesselSAPPostingModel().TransmitData("");
                    toastr.warning("Vessel is Posted  having SAP Account No:" + selecteddataItem.SAPAccountNo);
                    self.IsSaveDisabled(false);
                }
            }

        }

        self.Initialize();
    }
    IPMSRoot.VesselSAPPostingViewModel = VesselSAPPostingViewModel;


}(window.IPMSROOT))