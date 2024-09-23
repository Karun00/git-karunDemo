(function (IPMSRoot) {

    var WegoBerthDashBoardViewModel = function () {
        var self = this;
        $('#spnTitle').html("DashBoard");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        //self.wegodashBoardValues = ko.observable();
        self.wegoberthdashboardModel = ko.observable();
        self.durationPeriodValue = ko.observable();
        //self.wegoDashBoardDetails = ko.observable();



        self.Initialize = function () {            
            self.wegoberthdashboardModel(new IPMSROOT.WegoBerthDashBoardModel());
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
                FromDate = moment(self.stdate()).format('YYYY-MM-DD');
            }

            if (ToDate == "" || ToDate == undefined) {
                ToDate = moment(self.endate()).format('YYYY-MM-DD');
            }


            self.viewModelHelper.apiGet('api/WegoBerthUtilizationData/' + FromDate + '/' + ToDate, {},
                   function (result) {
                       self.wegoberthdashboardModel().WegoBerthDashBoardDetails(ko.utils.arrayMap(result, function (item) {
                           return new IPMSRoot.WegoBerthDashBoardDetail(item);
                       }));
                   });
        }



        SearchToDate = function () {
            this.min($("#fromdateid").val());

        }


        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }


        self.Initialize();
    }
    IPMSRoot.WegoBerthDashBoardViewModel = WegoBerthDashBoardViewModel;

}(window.IPMSROOT));











