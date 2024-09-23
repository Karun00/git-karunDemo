//$(function () {
//    var BerthVM = function () {
//        var self = this;
//        self.Berths = ko.observableArray([]);
//    }
//    var VM = new BerthVM();
//    ko.applyBindings(VM);
//    $.ajax({
//        url: '/api/Berth/GetBerths',
//        type: "GET",
//        contentType: "application/json",
//        dataType: 'json',
//        success: function (data) {
//            VM.Berths(data);
//           // alert(ko.toJSON(VM.Berths(data)));
//           // alert("caled");
//        },
//        error: function (data) { alert(data.status) }
//    });
//});

///////////////////////////////////////////////////////////////////////
$(function () {
    function VM() {
        var self = this;
        self.BerthCode = ko.observable();
        self.BerthName = ko.observable();
        self.ShortName = ko.observable();
        self.PortID = ko.observable();
        self.QuayID = ko.observable();
        self.BerthType = ko.observable();
        self.FromMeter = ko.observable();
        self.ToMeter = ko.observable();
        self.Status = ko.observable('A');
        self.LengthM = ko.observable();
        self.DraftM = ko.observable();
        self.CreatedBy = ko.observable('1');
        self.CreatedDate = GetDateTime();

        self.Berths = ko.observableArray([]);
        self.Ports = ko.observableArray();
        self.selectedPort = ko.observable();
        self.Quays = ko.observableArray();
        self.selectedQuay = ko.observable();
        self.selectedGroup = ko.observable();
        //self.selectedGroups = ko.observable();

        //self.Postdata = ko.observableArray([]);
       


        var BerthData = {
            BerthCode: self.BerthCode,
            BerthName: self.BerthName,
            ShortName: self.ShortName,
            PortID: self.PortID,
            QuayID: self.QuayID,
            BerthType: self.BerthType,
            FromMeter: self.FromMeter,
            ToMeter: self.ToMeter,
            Status: self.Status,
            LengthM: self.LengthM,
            DraftM: self.DraftM,
            CreatedBy: self.CreatedBy,
            CreatedDate: self.CreatedDate
        };
        

        self.SaveBerth = function () {
          //  alert(vm.selectedGroup().Ports[0].Quays[0].QuayID);
           // return;
            BerthData.PortID = vm.selectedGroup().PortID;
            BerthData.QuayID = vm.selectedGroup().Quays[0].QuayID;
            $.ajax({
                type: "POST",
                url: "/api/Berth/PostBerthData",
                data: ko.toJSON(BerthData),
                contentType: "application/json",
                success: function (data) {
                    
                   // vm.Postdata(data);
                    // alert(vm.Postdata());
                },
                error: function () {
                    alert("Failed");
                }
            })

        };
            
        $.ajax({
            url: '/api/Berth/GetBerths',
            type: "GET",
            contentType: "application/json",
            dataType: 'json',
            success: function (data) {
                vm.Berths(data);
            },
            error: function (data) { alert(data.status) }
        }),
        $.ajax(
             {
                 type: 'GET',
                 url: "/api/Berth/GetPQDtl",
                 dataType: 'JSON',
                 success: function (data) {
                     self.Ports(data);
                   //  self.Quays(data);
                  
                     self.selectedGroup.subscribe(function (item) {
                        BerthData.selectedQuay(item.QuayID);
                     });                  
                     }
             });

    }
    var vm = new VM();
    ko.applyBindings(vm);

});
function GetDateTime() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var currentDate = month + '/' + date.getDate() + '/' + date.getFullYear();
    return currentDate;
}





//var portViewModel = function () {
//    var self = this;
//    self.PortCode = ko.observable();
//    self.PortName = ko.observable();
//    self.InternationalCharacter = ko.observable();
//    self.GeographicLocation = ko.observable();
//    self.ContactNo = ko.observable();
//    self.Email = ko.observable();
//    self.Fax = ko.observable();
//    self.Website = ko.observable();
//    self.Description = ko.observable();
//    self.Status = ko.observableArray([{ name: "Active",val:"A" },{name:"Inactive",val:"I"}]);
//    self.CreatedBy = ko.observable('1');
//    self.CreatedDate = GetDateTime();
//    self.selectStatus = ko.observable();
   
//    var PortData = {
//        PortCode: self.PortCode,
//        PortName: self.PortName,
//        InternationalCharacter: self.InternationalCharacter,
//        GeographicLocation: self.GeographicLocation,
//        ContactNo: self.ContactNo,
//        Email: self.Email,
//        Fax: self.Fax,
//        Website: self.Website,
//        Description: self.Description,
//        Status: self.selectStatus,
//        CreatedBy:self.CreatedBy,
//        CreatedDate:self.CreatedDate,
//    };
      
//    PortData.PortID = portId;
//    $.ajax({
//        type: "POST",
//        url: "/api/Ports/PostPortData",
//        data: ko.toJSON(PortData),
//        contentType: "application/json",
//        success: function (data) {
//        },
//        error: function () {
//            alert("Failed");
//        }
//    });
//}
//}
//var vm = new portViewModel();
//ko.applyBindings(vm);


//function PortDataAtId()
//{
//    $.ajax({
//        type: "get",
//        url: "/api/Ports/GetPortDtl/" + portId,
//        contentType: "application/json",
//        success: function (data) {
//            vm.PortCode(data.PortCode);
//            vm.PortName(data.PortName);
//            vm.InternationalCharacter(data.InternationalCharacter);
//            vm.GeographicLocation(data.GeographicLocation);
//            vm.ContactNo(data.ContactNo);
//            vm.Email(data.Email);
//            vm.Fax(data.Fax);
//            vm.Description(data.Description);
//            vm.Website(data.Website);
//            vm.selectStatus(data.Status);
//        },
//        error: function () {
//            alert("Failed");
//        }
//    });

