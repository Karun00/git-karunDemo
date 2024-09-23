(function (IPMSRoot) {

    var CargoTypeDashBoardViewModel = function () {
        var self = this;
        $('#spnTitle').html("DashBoard");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.CargoDashboardModel = ko.observable();
        self.durationPeriodValue = ko.observable();
        self.CargoDashboardDetail = ko.observable();

        self.Initialize = function () {
            self.CargoDashboardModel(new IPMSROOT.CargoDashboardModel());
            self.getData(undefined);
            self.viewMode('Form');

            $("#ddlport").text('');
            self.viewModelHelper.apiGet('api/getAllPort', null,
                   function (result) {

                       $.each(result, function (data, value) {
                           $("#ddlport").append($("<option></option>").val(value.PortCode).html(value.PortName));
                       });
                   }, null, null, false);
            $('select').on('change', function () {

                var abc = $('#ddlport').val();
                $.cookie('portname', abc);
               
            });
        }

        self.stdate = ko.observable();
        self.endate = ko.observable();

        self.getData = function (model) {

            var FromDate = $("#fromdateid").val();
            var ToDate = $("#todateid").val();

            if (model == undefined) {
                var fromdate = new Date();
                var todate = new Date();
                fromdate.setDate(fromdate.getDate() - 7);
                //fromdate.setDate(1);
                //fromdate.setMonth(3);
                //fromdate.setHours(0);
                //fromdate.setMinutes(0);
                //fromdate.setSeconds(0)
               
                if (todate.getMonth() >= 3) {
                    fromdate.setFullYear(todate.getFullYear());
                }
                else {
                    fromdate.setFullYear(todate.getFullYear() - 1);
                }

                self.stdate(fromdate);
               self.endate(new Date());
            }
            else {

                self.stdate(model.FromDate());
                self.endate(model.ToDate());

            }



            if (FromDate == "" || FromDate == undefined) {
                FromDate = moment(self.stdate()).format('YYYY-MM-DD hh:mm:ss A');
            }

            if (ToDate == "" || ToDate == undefined) {
                ToDate = moment(self.endate()).format('YYYY-MM-DD hh:mm:ss A');
            }
          
            //$("#ddlport").text('');
            //self.viewModelHelper.apiGet('api/getAllPort', null,
            //       function (result) {

            //           $.each(result, function (data, value) {
            //               $("#ddlport").append($("<option></option>").val(value.PortCode).html(value.PortName));
            //           });
            //       }, null, null, false);

           
            //$('select').on('change', function () {
                //var portcode = $(this).val();
            //var portcode = $('#ddlport').val();
            var portcode = $.cookie('portname');
            if (portcode == "" || portcode == undefined)
                { portcode = null; }
                //alert($(this).find(":selected").val());
            //alert(this.value);          
                self.viewModelHelper.apiGet('api/CargoTypeDashboard/{FromDate}/{ToDate}/' + portcode,
                                    { FromDate: FromDate, ToDate: ToDate },
                               function (result) {
                                   self.CargoDashboardModel().CargoDashboardDetail(ko.utils.arrayMap(result, function (item) {
                                      
                                       return new IPMSRoot.CargoDashboardDetail(item);
                                   }));
                               }, null, null, false);
            // });
            
        }
        SearchToDate = function () {
            this.min($("#fromdateid").val());

        }
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }


        self.Initialize();    
    }
    IPMSRoot.CargoTypeDashBoardViewModel = CargoTypeDashBoardViewModel;
   
}(window.IPMSROOT));











