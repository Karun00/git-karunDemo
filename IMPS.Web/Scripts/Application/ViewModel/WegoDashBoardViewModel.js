(function (IPMSRoot) {

    var WegoDashBoardViewModel = function () {
        var self = this;
        $('#spnTitle').html("DashBoard");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.wegodashBoardValues = ko.observable();
        self.wegodashboardModel = ko.observable();
        self.durationPeriodValue = ko.observable();
        self.wegoDashBoardDetails = ko.observable();
        
        self.Initialize = function () {
            self.wegodashboardModel(new IPMSROOT.WegoDashBoardModel());
           
            
            self.getData(undefined);
            self.viewMode('Form');
        }

        self.stdate = ko.observable();
        self.endate = ko.observable();      

        self.getData = function (model) {

            var FromDate = $("#fromdateid").val();
            var ToDate = $("#todateid").val();            

            if (model == undefined) {
                var fromdate = new Date();
                var todate = new Date();
                fromdate.setDate(1);
                fromdate.setMonth(3);
                fromdate.setHours(0);
                fromdate.setMinutes(0);
                fromdate.setSeconds(0)
                
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
           
            self.viewModelHelper.apiGet('api/WegoDashBoradData/{FromDate}/{ToDate}',
                         { FromDate: FromDate, ToDate: ToDate },
                    function (result) {
                        debugger;
                        self.wegodashboardModel().WegoDashBoardDetails(ko.utils.arrayMap(result, function (item) {
                            return new IPMSRoot.WegoDashBoardDetail(item);
                        }));
                    }, null, null, false);

        }
        

        SearchToDate = function () {
            this.min($("#fromdateid").val());

        }

        
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
      

        self.Initialize();
    }
    IPMSRoot.WegoDashBoardViewModel = WegoDashBoardViewModel;

}(window.IPMSROOT));











