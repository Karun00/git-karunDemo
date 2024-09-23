 /// <reference path="AgentRegView.js" />

function PortData(data) {
    this.PortID = ko.observable(data.PortID);
    this.PortCode = ko.observable(data.PortCode);
    this.PortName = ko.observable(data.PortName);
    this.InternationalCharacter = ko.observable(data.InternationalCharacter);
    this.ContactNo = ko.observable(data.ContactNo);
    this.Email = ko.observable(data.Email);
    this.Status = ko.observable(data.Status); 
        
}
var PortVM = {
    PortData: ko.observableArray([]),
    GetAll: function () {
        var self = this;
        $.ajax(
             {
                 url: "/api/Ports/GetAllPorts",
                 async: false,
                 dataType: 'json',
                 success: function (data) {                 
                     PortVM.PortData(data);
                     $.each(data, function (key, value) {
                         if (value.Status == 'A') {
                             $('#chkPort_' + value.PortID).attr('checked', true);
                         }
                     });
                 }
             });
    },
    ViewPort: function (port) {
        var portId = port.PortID;
        window.location = '/Ports/ManagePorts/' + portId + '/' + 1;
    }
    ,
    EditPort : function (port) {
        var portId = port.PortID;
        window.location = '/Ports/ManagePorts/'+portId;
    },
    DeletePort: function (port) {
        var self = this;
        var portId = port.PortID;
        $.ajax({
            type: "PUT",
            url: "/api/Ports/PutDeletePort/" + portId,
            contentType: "application/json",
            dataType: 'JSON',
            success: function (data) {
                vm.PortData.remove(port)
            },
            error: function (error) {
                alert(error.status + " " + error.statusText);
            }
        });
      
    }
};
var vm = PortVM;
ko.applyBindings(vm);
PortVM.GetAll();