$(function () {
   
    var applicantId = parseInt($('#spanApplicantId').text());
    var agentViewModel = function () {
        var self = this;
        self.applicant = ko.observableArray();
        var applicantId = parseInt($('#spanApplicantId').text());
        $.ajax({
            type: "GET",
            url: "/api/AgentRegistration/GetApplicant/" + applicantId,
            datatype: "JSON",
            data: {},
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                
                self.applicant(data);
                $.each(data.Appl_Port_Workflow, function (key, value) {
                    $('#chkPort_' + value.PortID).attr('checked', true);
                });
            },
            failure: function (data) {
            }
        });

    };

    var PortVM = function () {
        var self = this;
        self.PortData = ko.observableArray([]);
        $.ajax(
             {
                 url: "/api/Ports/GetAllPorts",
                 // async: false,
                 dataType: 'JSON',
                 success: function (data) {
                     self.PortData(data);
                  //   GetDetails();
                 }
             });
        //function GetDetails() {
        //    //$.each(agentViewModel.   data, function (key, value) {
        //    //    $('#chkPort_' + value.PortID).attr('checked', true);
        //    //});
        //    //$.ajax({
        //    //    url: "/api/AgentRegistration/GetPortsAgent/" + applicantId,
        //    //    async: false,
        //    //    dataType: 'json',
        //    //    data: {},
        //    //    contentType: "application/json;charset=utf-8",
        //    //    success: function (data) {                   
        //    //        $.each(data, function (key, value) {
        //    //            $('#chkPort_' + value.PortID).attr('checked', true);
        //    //        });
        //    //    }
        //    //});        
        //}

    };
 
    ko.applyBindings(new PortVM, document.getElementById('First'));
    ko.applyBindings(new agentViewModel, document.getElementById('Second'));

});



