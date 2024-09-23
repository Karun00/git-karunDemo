(function (IPMSRoot) {
    var isView = 0;

    var NewsViewModel = function () {
        var self = this;
        $('#spnTitle').html("News");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.newslist = ko.observableArray();
        self.newsModel = ko.observable();
        self.IsSave = ko.observable(false);
        self.IsUpdate = ko.observable(false);
        self.IsReset = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.IsActivityEnable = ko.observable(false);
        self.PortData = ko.observableArray([]);
        self.NewsPort = ko.observableArray([]);
        // Initialize method is fires in  page Initializetion mode
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.newsModel(new IPMSROOT.NewsModel());
            self.LoadNews();
            self.viewMode('List');  
        }
        // addnews method is fires when Addnew button press in Add mode
        self.addnews = function () {          
            self.newsModel(new IPMSRoot.NewsModel());
            $('#spnTitle').html("Add News");
            self.viewMode('Form');
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null, function (result) {
                self.newsModel().PortData(result);
            }, null, null, false);
            self.IsSave(true);
            self.IsReset(true);
            self.IsUpdate(false);
            self.IsCodeEnable(true);
            self.IsActivityEnable(false);
            
        }

        // viewnews method is fires when View button press in View mode
        self.viewnews = function (data) {          
            self.newsModel(data);
            $('#spnTitle').html("View News");
            self.viewMode('Form');
            self.IsSave(false);
            self.IsReset(false);
            self.IsUpdate(false);
            self.IsCodeEnable(false);
            $('#chkAllNewsport').prop('disabled', true);
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null, function (result) {             
                self.newsModel().PortData(result);               
                $.each(result, function (key, value) {                   
                    $('#chkPort_' + value.PortCode).prop('disabled', true);
                });                
            }, null, null, false);
            $.each(data.NewsPort(), function (key, value) {               
                $('#chkPort_' + value.PortCode).attr('checked', true);
                $('#chkPort_' + value.PortCode).css('outline', '')
            });
            var cnt = 0;
            $('#divNewsPortList input[name="port"]').each(function () {
                
                if (this.checked)
                    cnt += 1;
            });
            if ($('#divNewsPortList input[name="port"]').length == cnt)
                $('#chkAllNewsport').attr('checked', true);
            self.IsActivityEnable(false);            
        }

        // editnews method is fires when Edit button press in Edit mode
        self.editnews = function (data) {
            var StartDate = data.StartDate();
            var myDatePicker = new Date(StartDate);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours() + 1;
            var Mnt = myDatePicker.getMinutes();
            $("#datepickerE").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));

            self.newsModel(data);
            $('#spnTitle').html("Update News");
            self.viewMode('Form');
            self.IsSave(false);
            self.IsReset(true);
            self.IsUpdate(true);
            self.IsCodeEnable(true);
            $('#chkAllNewsport').prop('disabled', false);
            self.viewModelHelper.apiGet('api/Ports/GetAllPorts', null, function (result) {
                self.newsModel().PortData(result);
                $.each(result, function (key, value) {
                    $('#chkPort_' + value.PortCode).prop('disabled', false);
                });
            }, null, null, false);
            $.each(data.NewsPort(), function (key, value) {
                $('#chkPort_' + value.PortCode).attr('checked', true);
                $('#chkPort_' + value.PortCode).css('outline', '')
            });
            var cnt = 0;
            $('#divNewsPortList input[name="port"]').each(function () {
               
                if (this.checked)
                    cnt += 1;
            });
            if ($('#divNewsPortList input[name="port"]').length == cnt)
                $('#chkAllNewsport').attr('checked', true);
            self.IsActivityEnable(true);
        }

        // ResetNews method is fires when press the Reset button and set the all fields data in the stage that in page load time
        self.ResetNews = function (model) {           
            ko.validation.reset();
            self.newsModel().reset();
            $('#divNewsPortList input[name="port"]:checked').attr('checked', false);
            $('#divNewsPortList input[name="port"]:disabled').prop('disabled', false);
            $('#chkAllNewsport').attr('checked', false);
            $.each(model.NewsPort(), function (key, value) {
                $('#chkPort_' + value.PortCode).attr('checked', true);
                $('#chkPort_' + value.PortCode).css('outline', '')
            });
            var cnt = 0;
            $('#divNewsPortList input[name="port"]').each(function () {

                if (this.checked)
                    cnt += 1;
            });
            if ($('#divNewsPortList input[name="port"]').length == cnt)
                $('#chkAllNewsport').attr('checked', true);
            
        }

        //CancelNews method is Cancel the News  screen data and navigate to List screen
        self.CancelNews = function () {
            self.newsModel().reset();
            self.viewMode("List");
            $('#spnTitle').html("News");
        }

        ChangeEndDateDefault = function () {
            $("#datepickerE").val("");

            var StartDate = $("#datepickerS").val();
            var myDatePicker = new Date(StartDate);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var Hour = myDatePicker.getHours() + 1;
            var Mnt = myDatePicker.getMinutes();
            $("#datepickerE").data('kendoDateTimePicker').min(new Date(year, month, day, Hour, Mnt));
        }

        //Only Future Dates
        calOpen = function () {
            this.min(new Date());
        };

        //Preventing Backspace
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        // SaveNews Used For Save the data from all controls into backend
        self.SaveNews = function (News) {
         
            News.validationEnabled(true);
            self.NewsValidation = ko.observable(News);
            self.NewsValidation().errors = ko.validation.group(self.NewsValidation());
            var errors = self.NewsValidation().errors().length;
            var result = true;
            var count = $('#divNewsPortList input[name="port"]:checked').length;

            if (count == 0) {
                toastr.warning("Please select at least one port.", "Warning");
                result = false;
                errors = errors + 1;
            }
            else {
                errors = 0;
                var NewsPortObjArry = new Array();
                $('#divNewsPortList input[name="port"]:checked').each(function () {
                   var NewsPortObjArryData = new NewsPortData(self.newsModel().NewsID(), this.value, "A", self.newsModel().CreatedBy(), self.newsModel().CreatedDate(), '', '');                   
                    NewsPortObjArry.push(NewsPortObjArryData);
                });               
                News.NewsPort(NewsPortObjArry);               
            }           
            if (errors == 0) {               
                self.viewModelHelper.apiPost('api/News', ko.mapping.toJSON(News), function Message(data) {                   
                    if (data.NewsID > 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("News details saved successfully.", "News");                        
                        self.LoadNews();
                        self.viewMode('List');
                        //TODO:have to check how to reload the footer after saving and updating
                        var chat = $.connection.chatHub;

                        // Declare a function on the job hub so the server can invoke it
                        chat.client.BrodcastNews = function () {
                            getData();
                            getDivingData();
                        };

                        // Start the connection
                        //  $.connection.hub.start();
                        getData();
                        getDivingData();
                        
                    }
                });
            }
            else {
                self.NewsValidation().errors.showAllMessages();
                return;
            }
        }
        


        

       

        // UpdateNews Used For Save the data from all controls into backend
        self.UpdateNews = function (News) {
            //prompt("to be saved", ko.toJSON(self.terminaloperatorModel));
           
            News.validationEnabled(true);
            self.NewsValidation = ko.observable(News);
            self.NewsValidation().errors = ko.validation.group(self.NewsValidation());
            var errors = self.NewsValidation().errors().length;
            var result = true;
            var count = $('#divNewsPortList input[name="port"]:checked').length;

            if (count == 0) {
                toastr.warning("Please select at least one port.", "Warning");
                result = false;
                errors = errors + 1;
            }
            else {
                errors = 0;
                var NewsPortObjArry = new Array();
                $('#divNewsPortList input[name="port"]:checked').each(function () {
                    var NewsPortObjArryData = new NewsPortData(self.newsModel().NewsID(), this.value, "A", self.newsModel().CreatedBy(), self.newsModel().CreatedDate(), '', '');
                    NewsPortObjArry.push(NewsPortObjArryData);
                });
                News.NewsPort(NewsPortObjArry);
            }
            if (errors == 0) {
                self.viewModelHelper.apiPut('api/News', ko.mapping.toJSON(News), function Message(data) {
                    if (data.NewsID > 0) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("News details updated successfully.", "News");
                         self.LoadNews();
                         window.location.href = "/News";                        
                         self.viewMode('List');
                         var chat = $.connection.chatHub;

                        // Declare a function on the job hub so the server can invoke it
                         chat.client.BrodcastNews = function () {
                             getData();
                             getDivingData();
                         };

                        // Start the connection
                        //  $.connection.hub.start();
                         getData();
                         getDivingData();

                    }
                });
            }
            else {
                self.NewsValidation().errors.showAllMessages();
                return;
            }
        }

        // this is for grid binding
        // LoadNews Used For fetch the data for List from backend
        self.LoadNews = function () {            
            $('#spnTitle').html("News");
            self.viewModelHelper.apiGet('api/News', null, function (result) {               
                self.newslist(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.NewsModel(item);
                }));
            }, null, null, false);
        }
       

        self.Initialize();
    }
   

    IPMSRoot.NewsViewModel = NewsViewModel;
}(window.IPMSROOT));
//Checking for the ports
function toggle(source) {   
    checkboxes = document.getElementsByName('port');
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        checkboxes[i].checked = source.checked;
    }
}

//  Port Selection Check box for single
function toggleSingle(source) {   
    checkboxes = document.getElementsByName('port');
    var chk = true;
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        if (!checkboxes[i].checked) {
            chk = false;
        }
    }

    checkboxesAll = document.getElementsByName('Allport');
    checkboxesAll[0].checked = chk;
}
function getData() {
   
    $('#divNews').hide();
    var $tbl = $('#tblJobInfo');
    $.ajax({
        url: '../api/NewsScroll',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            
            if (data.length > 0) {
                debugger;
                $("#divNews").show();
                $tbl.empty();
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    rows.push(' <tr><td><a href="http://' + data[i].NewsUrl + '" target="_blank" style="color:#00ff00;">' + $('<div/>').text(data[i].NewsContent).html() + '</a></td></tr>');
                }
                $tbl.append('<marquee scrolldelay="150" onmouseover="this.stop();" onmouseout="this.start();">' + rows.join('&nbsp;&nbsp;&nbsp;&nbsp;') + '</marquee>');

            }
        }
    });
}
function getDivingData() {

    $('#divDivingRequest').hide();

    var $tbl = $('#tblDivReq');
    $.ajax({

        url: '../api/DivingRequestsForScroll',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                $("#divDivingRequest").show();
                $tbl.empty();
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    rows.push(' <tr><td> Diving Activity At ' + data[i].LocationorQuay + ' Will Be Stop By  ' + data[i].OcupationToDate + '</td></tr> ');
                }
                $tbl.append('<marquee scrolldelay="150" onmouseover="this.stop();" onmouseout="this.start();">' + rows.join('&nbsp;&nbsp;&nbsp;&nbsp;') + '</marquee>');

            }
        }
    });
}
function NewsPortData(NewsID, PortCode, RecordStatus, CreatedDate, CreatedBy,ModifiedBy,ModifiedDate ) {
    this.NewsID = NewsID;
    this.PortCode = PortCode;    
    this.CreatedDate = CreatedDate;
    this.CreatedBy = CreatedBy;
    this.RecordStatus = RecordStatus;
    this.ModifiedBy = ModifiedBy;
    this.ModifiedDate = ModifiedDate;
}

