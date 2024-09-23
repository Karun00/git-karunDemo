(function (IPMSRoot) {

    var AuditLogViewModel = function () {

        var self = this;
        $('#spnTitile').html("Audit Trail");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.auditLogModel = ko.observable();
        self.auditLogList = ko.observableArray();
        self.auditLogSearch = ko.observable(new IPMSROOT.AuditLogModel);
        self.validationHelper = new IPMSRoot.validationHelper();
        

        self.LoadAuditLogs = function () {
            self.viewModelHelper.apiGet('api/AuditLog/GetAllAuditLogs', { ColumnName: null, SearchValue: null },
                function (result) {
                    self.auditLogList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.AuditLogModel(item);
                    }));
                });
        }

        self.GetAuditSearchDetails = function (data) {
            if ($("#divAuditGrd").data("kendoGrid") != undefined) {
                //var datasource = $("#divAuditGrd").data("kendoGrid").dataSource;
                //var filters = datasource.filter();
                //filters.filters.splice(i, 1);
                //datasource.filter(filters);
                $("form.k-filter-menu button[type='reset']").trigger("click");
                
            }
            
            var username; var auditFrmDt; var auditToDt; var isSecurityAuditLog
            if (data == undefined) {
                username = self.auditLogModel().UserName() != "" ? self.auditLogModel().UserName() : "";
                auditFrmDt = moment(self.auditLogModel().AuditFromDateTime()).format('YYYY-MM-DD HH:mm');
                auditToDt = moment(self.auditLogModel().AuditToDateTime()).format('YYYY-MM-DD HH:mm');
                if ($('#chkIsSecurityAuditTrail').val()!=undefined)
                    isSecurityAuditLog = $('#chkIsSecurityAuditTrail').is(':checked') == true ? 'Y' : 'N';
                else
                    isSecurityAuditLog='Y'
            }
            else {
                username = data.UserName() != "" ? data.UserName() : "";
                auditFrmDt = moment(data.AuditFromDateTime()).format('YYYY-MM-DD HH:mm');
                auditToDt = moment(data.AuditToDateTime()).format('YYYY-MM-DD HH:mm');

                if ($('#chkIsSecurityAuditTrail').val() != undefined)
                    isSecurityAuditLog = $('#chkIsSecurityAuditTrail').is(':checked') == true ? 'Y' : 'N';
                else
                    isSecurityAuditLog = 'Y'
            }

            self.viewModelHelper.apiGet('api/AuditLog/GetAuditSearchDetails', { userName: username, auditFromDt: auditFrmDt, auditToDt: auditToDt, isSecurityAuditLog: isSecurityAuditLog },
                function (result) {
                    self.auditLogList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.AuditLogModel(item);
                    }));

                    
                });
        }

        self.ResetAuditSearchDetails = function (data) {
           
            data.AuditFromDateTime(new Date());
            var AuditFromDateTime = new Date(data.AuditFromDateTime())
            AuditFromDateTime.setHours(AuditFromDateTime.getHours() + 1)
            data.AuditToDateTime(AuditFromDateTime);
            data.UserName('');
            $('#chkIsSecurityAuditTrail').attr('checked', true);
            self.GetAuditSearchDetails(data);
        }

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.auditLogModel(new IPMSROOT.AuditLogModel());
            self.GetAuditSearchDetails();
            //self.LoadAuditLogs();

            self.viewMode('List');
        }

        AddDayToAuditToDate = function (data, event) {
           
            var AuditFromDateTime = new Date(data.AuditFromDateTime())
            AuditFromDateTime.setHours(AuditFromDateTime.getHours() + 1)
            data.AuditToDateTime(AuditFromDateTime);
        }

        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.Initialize();

        var originalFilter = kendo.data.DataSource.fn.filter;

        //Custom Filter operation - Start
        kendo.data.DataSource.fn.filter = function (e) {

            var ColumnName = null;
            var SearchValue = null;
            var UserName = null;
            var AuditFromDateTime = null;
            var AuditToDateTime = null;

            if ($('#userName').val() != '')
                UserName = $('#userName').val();

            if ($('#AuditFromDt').val() != '')
                AuditFromDateTime = $('#AuditFromDt').val();

            if ($('#AuditToDt').val() != '')
                AuditToDateTime = $('#AuditToDt').val();



            try {
                if (arguments.length > 0 && e.filters.length > 0) {

                    if (e.filters[0].field.length > 0) {
                        ColumnName = e.filters[0].field;
                    }
                    ColumnName = ColumnName.replace("Sort", "");

                    if (e.filters[0].value.length > 0) {
                        SearchValue = e.filters[0].value;
                    }

                    self.viewModelHelper.apiGet('api/AuditLog/GetAllAuditLogs', { ColumnName: ColumnName, SearchValue: SearchValue, UserName: UserName, AuditFromDt: AuditFromDateTime, AuditToDt: AuditToDateTime },
                        function (result) {
                            self.auditLogList(ko.utils.arrayMap(result, function (item) {
                                return new IPMSRoot.AuditLogModel(item);
                            }));
                        });

                    $.event.trigger('auditLogList', [e, $(this)]);
                }
                return originalFilter.apply(this, arguments);
            } catch (e) {
                self.viewModelHelper.apiGet('api/AuditLog/GetAllAuditLogs', { ColumnName: null, SearchValue: null, UserName: null, AuditFromDt: null, AuditToDt: null },
                function (result) {
                    self.auditLogList(ko.utils.arrayMap(result, function (item) {
                        return new IPMSRoot.AuditLogModel(item);
                    }));
                }, null, null, false);
                return;
            }
        };
        ////End

    }
    IPMSRoot.AuditLogViewModel = AuditLogViewModel;

}(window.IPMSROOT));


