(function (IPMSRoot) {
    var WorkFlowCommon = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();

        self.SubmitAction = function (pdata, par) {
            self.message = ko.observable();
            self.message = pdata.TaskDescription();


            self.viewModelHelper.apiGet('api/WorkFlowTaskStatus/' + pdata.ReferenceID() + '/' + pdata.WorkflowInstanceId() + '/' + pdata.TaskCode(),
                null,
                function Message(data) {                    
                    if (data.WFTaskStatus == true) {
                        self.viewModelHelper.apiPost(pdata.APIUrl(), ko.mapping.toJSON(pdata), function Message(data) {
                            toastr.options.closeButton = true;
                            toastr.options.positionClass = "toast-top-right";
                            toastr.success("Record " + self.message + " Successfully", "Pending Task");
                            setTimeout(
                              function () {
                                  window.location.href = "/Welcome";
                                  //var strHash = document.location.pathname;
                                  //window.location.href = strHash;
                              }, 10000);

                            self.cache = "";

                        },
                        function failure(result) {                            
                            toastr.error(result.responseText);
                          },
                        false);

                    }
                    else {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.warning("Request is already processed by another User.", "Pending Task");
                        setTimeout(
                        function () {
                            window.location.href = "/Welcome";
                        }, 10000);
                    }

                });

            $(".close").trigger("click");
        }


    }
    IPMSRoot.WorkFlowCommon = WorkFlowCommon;
}(window.IPMSROOT));

/*
IPMSROOT.WorkFlowCommon.prototype.Approve = function ()
{
    alert("sadfsadfasfd");
}*/