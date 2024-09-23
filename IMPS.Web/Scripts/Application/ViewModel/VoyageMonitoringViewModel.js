(function (IPMSRoot) {
    var VoyageMonitoringViewModel = function (vcn, viewDetail) {
        var self = this;

        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.voyagemodel = ko.observable(new IPMSRoot.VoyageMonitoringModel());
        self.getVCNDtls = ko.observable();
        self.getVCNDtls1 = ko.observableArray();
        self.selectedVCN = ko.observable(vcn);
        self.getServiceRequestDetailss = ko.observable();
        self.getChangeATAandATDDetails = ko.observable();
        self.getAnchorageDetails = ko.observable();
        self.getSuppServiceRequestDetails = ko.observable();
        self.getServiceRecordingDetails = ko.observable();
        self.IsVCNEnable = ko.observable(true);
        self.GetBerthDetails = ko.observable();
        self.VCNValue = ko.observable();
        self.getPorBreakDetails = ko.observable();
        //// For Initialization //////////////////////////
        self.Initialize = function () {
            self.viewMode('Form');
            self.LoadVCNDetails();
            //self.voyagemodel(new IPMSRoot.VoyageMonitoringModel());
        }

        //This method is fires when cancel button is pressed and all fields data is cleared and redirected to List form
        self.cancel = function () {
            window.location = "/Welcome";
        }

        self.RedirectToChangeETA = function (data) {
            var vcn = data.VCN();
            var VesselETAChangeID = data.VesselETAChangeID();
            window.location.href = "/ChangeETA?vcn=" + vcn + "&id=" + VesselETAChangeID;
        }

        self.RedirectToServiceRequestDtls = function (data) {
            var servicerequestid = data.VCN();
            window.location.href = '/ServiceRequests/' + servicerequestid + 'x' + 'VM';
        }

        self.RedirectToAnchorageDtls = function (data) {
            window.location.href = '/vesselcallanchorage/vesselcallanchorage/' + data.VCN();
        }

        self.RedirectToSuppServiceRequestDtls = function (data) {
            var id = data.SuppServiceRequestID();
            window.location.href = '/SupplementaryServiceRequests/' + id + 'x' + 'VM';
        }

        self.LoadVCNDetails = function () {
            if (viewDetail == true) {
                self.IsVCNEnable(false);

                if (vcn != "") {
                    self.selectedVCN(vcn);
                    self.voyagemodel(new IPMSRoot.VoyageMonitoringModel());

                    self.viewModelHelper.apiGet('api/GetVCNDetailsVoyage_VCN', { vcn: vcn },
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.getVCNDtls1);
                            self.voyagemodel().VCN(self.getVCNDtls1()[0].VCN());
                            self.voyagemodel().VCNVesselName(self.getVCNDtls1()[0].VCNVesselName());
                            self.voyagemodel().VesselData(self.getVCNDtls1()[0]);
                        }, null, null, false);

                    self.viewModelHelper.apiGet('api/GetServiceRequestDetailss', { VCN: vcn },
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.getServiceRequestDetailss);
                            return new IPMSRoot.VoyageMonitoringModel(result);
                        }, null, null, false);

                    self.viewModelHelper.apiGet('api/GetChangeATAandATDDetails', { VCN: vcn },
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.getChangeATAandATDDetails);
                        }, null, null, false);

                    self.viewModelHelper.apiGet('api/GetAnchorageDetails', { VCN: vcn },
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.getAnchorageDetails);
                        }, null, null, false); 

                    self.viewModelHelper.apiGet('api/GetPortAndBreakLimitDetails', { VCN: vcn },
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.getPorBreakDetails);
                        }, null, null, false);

                    self.viewModelHelper.apiGet('api/GetSupplymentaryServiceRequestList_VCN', { VCN: vcn },
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.getSuppServiceRequestDetails);
                        }, null, null, false);

                    self.viewModelHelper.apiGet('api/GetresourceAllocationdetails_VCN', { vcn: vcn },
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.getServiceRecordingDetails);
                        }, null, null, false);

                    self.viewModelHelper.apiGet('api/GetBerthDetails', { VCN: vcn },
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.GetBerthDetails);
                        }, null, null, false);
                }
                else {
                    //self.viewModelHelper.apiGet('api/GetVCNDetailsVoyage', null,
                    //    function (result) {
                    //        ko.mapping.fromJS(result, {}, self.getVCNDtls);
                    //    }, null, null, false);
                }
            }
            else {
                self.IsVCNEnable(true);
                //self.viewModelHelper.apiGet('api/GetVCNDetailsVoyage', null, function (result) {
                //    ko.mapping.fromJS(result, {}, self.getVCNDtls);
                //}, null, null, false);
            }
        }

        self.check = function () {
            toastr.options.closeButton = true;
            toastr.options.positionClass = "toast-top-right";

            var ff = $("#VCN").val();
            var dd = self.VCNValue();

            if (ff != dd) {
                self.voyagemodel().VesselData("");
                toastr.error("Invalid VCN No.", "Error");

                self.viewModelHelper.apiGet('api/GetServiceRequestDetailss', { VCN: "INVALID" },
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getServiceRequestDetailss);
                        return new IPMSRoot.VoyageMonitoringModel(result);
                    }, null, null, false);

                self.viewModelHelper.apiGet('api/GetChangeATAandATDDetails', { VCN: "INVALID" },
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getChangeATAandATDDetails);
                    }, null, null, false);

                self.viewModelHelper.apiGet('api/GetAnchorageDetails', { VCN: "INVALID" },
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getAnchorageDetails);
                    }, null, null, false); 

                self.viewModelHelper.apiGet('api/GetPortAndBreakLimitDetails', { VCN: "INVALID" },
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getPorBreakDetails);
                    }, null, null, false);

                self.viewModelHelper.apiGet('api/GetSupplymentaryServiceRequestList_VCN', { VCN: "INVALID" },
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getSuppServiceRequestDetails);
                    }, null, null, false);

                self.viewModelHelper.apiGet('api/GetresourceAllocationdetails_VCN', { vcn: "INVALID" },
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.getServiceRecordingDetails);
                    }, null, null, false);

                self.viewModelHelper.apiGet('api/GetBerthDetails', { VCN: "INVALID" },
                 function (result) {
                      ko.mapping.fromJS(result, {}, self.GetBerthDetails);
                 }, null, null, false);

                return;
            }
        }

        self.VesselSelect = function (e) {
            self.IsVCNEnable(true);
            var dataItem = this.dataItem(e.item.index());
            self.voyagemodel().VesselData(dataItem);
            self.voyagemodel().VCN(dataItem.VCN);
            self.voyagemodel().VCNVesselName(dataItem.VCNVesselName);
            self.VCNValue(dataItem.VCNVesselName);
            self.viewModelHelper.apiGet('api/GetServiceRequestDetailss', { VCN: dataItem.VCN },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.getServiceRequestDetailss);
                    return new IPMSRoot.VoyageMonitoringModel(result);
                }, null, null, false);

            self.viewModelHelper.apiGet('api/GetChangeATAandATDDetails', { VCN: dataItem.VCN },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.getChangeATAandATDDetails);
                }, null, null, false);

            self.viewModelHelper.apiGet('api/GetAnchorageDetails', { VCN: dataItem.VCN },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.getAnchorageDetails);
                }, null, null, false);
            self.viewModelHelper.apiGet('api/GetPortAndBreakLimitDetails', { VCN: dataItem.VCN },
               function (result) {
                   ko.mapping.fromJS(result, {}, self.getPorBreakDetails);
                }, null, null, false);

            self.viewModelHelper.apiGet('api/GetSupplymentaryServiceRequestList_VCN', { VCN: dataItem.VCN },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.getSuppServiceRequestDetails);
                }, null, null, false);

            self.viewModelHelper.apiGet('api/GetresourceAllocationdetails_VCN', { vcn: dataItem.VCN },
                function (result) {
                    ko.mapping.fromJS(result, {}, self.getServiceRecordingDetails);
                }, null, null, false);

            self.viewModelHelper.apiGet('api/GetBerthDetails', { VCN: dataItem.VCN },
                       function (result) {
                           ko.mapping.fromJS(result, {}, self.GetBerthDetails);
                       }, null, null, false);
        }

        ///////// For Reset functionality ...................
        self.ResetRequest = function () {
            self.VoyageMonitoringModel().reset();
            $('#spnTitle').html("Voyage Monitoring");
        }

        self.Initialize();
    }

    IPMSRoot.VoyageMonitoringViewModel = VoyageMonitoringViewModel;

}(window.IPMSROOT));

function LogOut() {
    $.ajax({
        cache: true,
        type: "POST",
        url: "@(Url.Action('changeETA', 'VesselETAChange'))",
        success: function (data) {
            window.location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {

        },
        complete: function () { }

    }); // end ajax code
}




