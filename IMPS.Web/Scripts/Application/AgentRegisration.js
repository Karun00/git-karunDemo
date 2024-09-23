$(function () {
    var self = this;
    debugger

    function AgentRegViewModel(Applicant) {
        debugger;
        var self = this;
        self.applicant = ko.observable();
        /*self.ApplicantName = Applicant.ApplicantName;
        self.ApplicantTradName = Applicant.ApplicantTradName;*/

    }

    var agentreg = new AgentRegViewModel();
    ko.applyBindings(agentreg);

    $.ajax({
        type: "GET",
        url: "/api/AgentRegistration/8",
        datatype: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            agentreg.applicant(data);
        },
        error: function (xhr, desc, error) {
            window.alert('description' + desc);
            window.alert('error' + error);
        }
    });

    //function PortData(data) {
    //    var self = this;
    //    //this.port_id = ko.observable(data.port_id);
    //    //this.port_name = ko.observable(data.port_name);
    //    self.ports = ko.observable();

    //}
    //var portsdata = new PortData();
    //ko.applyBindings(portsdata);
    //$.ajax({
    //    url: "/api/Ports",
    //    async: false,
    //    dataType: 'json',
    //    success: function (data) {
    //        PortVM.PortData(data);
    //    }
    //});


});


