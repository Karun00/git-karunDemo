(function (IPMSRoot) {

    var DashBoardViewModel = function () {
        var self = this;
        $('#spnTitle').html("DashBoard");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.dashBoardValues = ko.observable();
        self.dashboardModel = ko.observable();
        self.durationPeriodValue = ko.observable();
        self.Initialize = function () {
            self.dashboardModel(new IPMSROOT.DashBoardModel());
            //  self.LoadDashBoard();
            self.GetDurationPeriod();
            //    self.getData();
            self.viewMode('Form');
            self.ReloadPlannedMovementsCount();
            

        }
      
        var decryptresult = "";
        var portcode = "";
        try
        {
            portcode = $.cookie('Port');
            if (portcode == undefined || portcode == '') {

                var uname = $("#loginusername").text();
                $.ajax({
                    url: 'api/Account/GetUserPorts/' + uname,
                    type: 'GET',
                    success: function (data) {
                        portcode = data[0].PortCode;
                        self.ReloadPlannedMovementsCount();
                       
                    }
                });
            }
            else {
                for (var i = 0; i < portcode.length; i++) {
                    decryptresult += String.fromCharCode(portcode.charCodeAt(i) ^ 6);
                }
                portcode = decryptresult;
                self.ReloadPlannedMovementsCount
                
            }
        }
        catch(ex)
        {
            console.log(ex.message);
        }
        debugger;
        self.ReloadPlannedMovementsCount = function () {
            self.viewModelHelper.apiGet('api/GetPlannedMovementsCount/' + portcode,
            null,
              function (result1) {
                  ko.utils.arrayForEach(result1, function (val) {
                      //htmldataBody += '<tr>';

                      //htmldataBody += '<span>' + val.BerthName + '</span>';
                      //htmldataBody += '</tr>';
                  
                  self.dashboardModel().PlannedMovementsCount(val.PlannedMovementsCount);
                  self.dashboardModel().PlannedMovtsArrivalCount(val.PlannedMovtsArrivalCount);
                  self.dashboardModel().PlannedMovtsShiftingCount(val.PlannedMovtsShiftingCount);
                  self.dashboardModel().PlannedMovtsWarpingCount(val.PlannedMovtsWarpingCount);
                  self.dashboardModel().PlannedMovtsSailingCount(val.PlannedMovtsSailingCount);
                  });
              }, null, null, false);
             
        }
        

        self.GetDurationPeriod = function () {
            self.viewModelHelper.apiGet('api/Dashboard/GetReportPeriod',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.durationPeriodValue);
                  //self.getData(undefined, self.durationPeriodValue());
              });

        }


        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
        //self.LoadDashBoard = function (model) {
        //    self.viewModelHelper.apiGet('api/Dashboard', null,
        //         function (result1) {
        //             self.dashBoardValues(new IPMSRoot.DashBoardModel(result1[0]));
        //         }, null, null, false);
        //}

        self.Initialize();
    }

   

    IPMSRoot.DashBoardViewModel = DashBoardViewModel;

}(window.IPMSROOT));












