(function (IPMSRoot) {

    var ReportBuilderViewModel = function () {
        var self = this;
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();

        self.reportBuilderReferenceData = ko.observableArray([]);
        self.reportBuilderModel = ko.observable(new IPMSROOT.ReportBuilderModel());
        self.reportQueryOperator = ko.observable();
        self.reportCategoryList = ko.observableArray();
        self.reportCategorycolumntypeList = ko.observableArray();
        self.reportCategorygridcolumntypeList = ko.observableArray([]);
        self.reportCategoryorderbycolumntypeList = ko.observableArray();
        self.reportCategorycolumntype = ko.observable();
        self.reportbuilderGridData = ko.observableArray();
        self.reportFilterList = ko.observableArray();       
        self.dttype1 = ko.observable(false);
        self.dttype2 = ko.observable(false);
        self.dttype3 = ko.observable(false);
        self.dttype4 = ko.observable(false);
        self.dateFormat = ko.observable();
        self.IsView = ko.observable(false);
       
       

        self.reportcoulmn = ko.observable();
        
        self.Initialize = function () {
            self.ReportCategories();
            self.ReportQueryOperator();
            self.ReportFilterData();         
            $("#btnsId").hide();
        }

        self.ReportCategories = function () {            
           
            self.viewModelHelper.apiGet('api/ReportBuilder/GetReportCategory',
               null,
                 function (result) {
                     ko.mapping.fromJS(result, {}, self.reportCategoryList);
                 });
        }

        self.LoadColumns = function (event) {
            
              //self.reportBuilderModel().Selectedmultilistcolumn("");
            if (event.ReportbuilderId() == undefined) {
                alert('You have not selected category');
            }
            else {               
                self.viewModelHelper.apiGet('api/ReportBuilder/GetReportCategoryColumn',
                      { reportbilderid: event.ReportbuilderId() },
                    function (result2) {                        
                        ko.mapping.fromJS(result2, {}, self.reportCategorycolumntypeList)                        
                    }, null, null, false);
                if (self.IsView()) {
                    $('#divColumnList').show();
                }
                else {
                    $("#Column").val("").data("kendoDropDownList").text("Select...");
                    $("#Operator").val("").data("kendoDropDownList").text("Select...");
                  
                }
                
                //  self.ReportQueryOperator();

                //Column
            }
        }

        self.datatypeFor = function (event) {
            
           
            var statust = true;

            if (event.OPERATOR_VALUE1() == " is NULL " || event.OPERATOR_VALUE1() == " is NOT NULL " || event.OPERATOR_VALUE1() == undefined) {
                self.dttype1(false);
                self.dttype3(false);
                self.dttype2(false);
                self.dttype4(false);
                statust = false;
            }
            if (statust) {

                if (event.DATA_TYPE == "datetime" && event.OPERATOR_VALUE1() == " between ") {
                    //  self.OperatorNullEvent(event);
                    self.dttype1(true);
                    self.dttype3(false);
                    self.dttype2(false);
                    self.dttype4(false);
                    return;

                }
                if (event.DATA_TYPE == "datetime" && event.OPERATOR_VALUE1() != " between ") {
                    //  self.OperatorNullEvent(event);
                    self.dttype3(false);
                    self.dttype1(false);
                    self.dttype4(false);
                    self.dttype2(true);
                    return;
                }
                if (event.DATA_TYPE == "int" || event.DATA_TYPE == "numeric" || event.DATA_TYPE == "tinyint" || event.DATA_TYPE == "bigint" || event.DATA_TYPE == "float" || event.DATA_TYPE == "decimal" || event.DATA_TYPE == "smallint") {
                    //  self.OperatorNullEvent(event);
                    self.dttype3(false);
                    self.dttype2(false);
                    self.dttype1(false);
                    self.dttype4(true);
                    return;
                }
                if (event.DATA_TYPE != "int" || event.DATA_TYPE != "numeric" || event.DATA_TYPE != "tinyint" || event.DATA_TYPE != "bigint" || event.DATA_TYPE != "float" || event.DATA_TYPE != "decimal" || event.DATA_TYPE != "smallint") {
                    //   self.OperatorNullEvent(event);
                    self.dttype3(true);
                    self.dttype2(false);
                    self.dttype1(false);
                    self.dttype4(false);
                    return;
                }

                else {
                    //self.OperatorNullEvent(event);
                    self.dttype4(false);
                    self.dttype1(false);
                    self.dttype2(false);
                    self.dttype3(true);
                    return;
                }
            }
        }

        self.OperatorNullEvent = function (model) {
            
           // //$('#divColumnList').show();
            if (model.OPERATOR_VALUE1() == " is NULL " || model.OPERATOR_VALUE1() == " is NOT NULL ") {
                self.dttype1(false);
                self.dttype3(false);
                self.dttype2(false);
                self.dttype4(false);
                return;
            }
        }

        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }

        self.ReportQueryOperator = function () {
            
            ////$('#divColumnList').show();
            var sk = ko.toJS(self.reportCategorycolumntypeList());
            //$('#divColumnList').show();
            if (sk != null) {
                $.each(sk, function (key, val) {
                    
                    if (val.COLUMN_NAME == self.reportBuilderModel().SelectedddlColumn()) {
                        
                        self.reportBuilderModel().DATA_TYPE = val.DATA_TYPE;
                        self.reportBuilderModel().query(val.query);
                    }
                });
            }

            if (self.reportBuilderModel().DATA_TYPE == "") {
                
                self.reportBuilderReferenceData({ OperatorId: 0, OperatorValue: '', OperatorName: '' });
            }
            else {
                self.viewModelHelper.apiGet('api/ReportBuilder/GetReportQueryOperator',
                    { datatype: self.reportBuilderModel().DATA_TYPE },
                      function (result) {
                          ko.mapping.fromJS(result, {}, self.reportBuilderReferenceData);
                          self.itemsEmpty();
                      });
            }
        }

        self.itemsEmpty = function () {
           
            self.reportBuilderModel().OPERATOR_VALUE1('');
            self.dttype1(false);
            self.dttype3(false);
            self.dttype2(false);
            self.dttype4(false);
            self.reportBuilderModel().Text("");
            self.reportBuilderModel().FromDate("");
            self.reportBuilderModel().ToDate("");
            self.reportBuilderModel().DateValue("");
            self.reportBuilderModel().ValueNumber("");
            self.reportBuilderModel().ResultValue("");
           // $('#divColumnList input[name="port"]:checked').attr('checked', false);
            $('#divColumnList').hide();
           // $('#chkAllColumns input[name="port"]:checked').attr('checked', false);
        }

        self.saveLookup = function (model) {
            
            if (model.Text() != "") {
                self.reportBuilderModel().ValueText(model.Text());
                self.reportBuilderModel().ResultValue(model.Text());
            }
        }

        self.eventForSelectValue = function (model) {
            
            //$('#divColumnList').show();
            var item;
            if (model.FromDate() != "" && model.ToDate() == "") {
                var FromDate = model.FromDate();
                var myDatePicker = new Date(FromDate);
                var day = myDatePicker.getDate();
                var month = myDatePicker.getMonth();
                var year = myDatePicker.getFullYear();
                $("#datepicker2").data('kendoDatePicker').min(new Date(year, month, day));
            }
            else if (model.FromDate() != "" && model.ToDate() != "") {
                item = moment(model.FromDate()).format('YYYY-MM-DD') + "'" + " " + "and" + " " + "'" + moment(model.ToDate()).format('YYYY-MM-DD');
            }

            if (model.Text() != "") {
                if (model.OPERATOR_VALUE1() == " like ") {
                    item = "%" + model.Text() + "%";
                }
                else {
                    item = model.Text();
                }
            }

            if (model.ValueNumber() != "") {
                item = model.ValueNumber();
            }

            if (model.DateValue() != "") {
                item = moment(model.DateValue()).format('YYYY-MM-DD');
            }

            self.reportBuilderModel().ResultValue(item);
        }

        self.GetQuery = function (model) {            
            var status = self.validationEvent(model);
            if (status) {
                var column = $("#Column option:selected").text();
                var ov = model.OPERATOR_VALUE1();
                var value = model.ResultValue();
               //var value = "'"+ model.ResultValue()+"'";
                var selectedOptions;
                if (model.OPERATOR_VALUE1() == " is NULL " || model.OPERATOR_VALUE1() == " is NOT NULL ") {
                    selectedOptions = column + ov;
                }
                else {
                    selectedOptions = column + ov + "'" + value + "'";
                    //$('#divColumnList').show();
                    //if (model.ReportbuilderId() != 3) {
                    //    selectedOptions = column + ov + "'" + value + "'";
                    //}
                    //else {
                    //    selectedOptions = column + ov + value;
                    //}
                }
                var status = true;

                if (model.allItems() != "") {
                    ko.utils.arrayForEach(model.allItems(), function (val) {
                        if (val == selectedOptions) {
                            alert('Your Condition Is Already Selected ');
                            status = false;

                        }
                    });
                }
                if (model.ColumnItems() != "") {
                    ko.utils.arrayForEach(model.ColumnItems(), function (val) {
                        if (val == column) {
                            alert('You can perform one condition against one Column');
                            status = false;
                        }
                    });
                }

                if (status) {
                    $('#divColumnList').show();
                    model.allItems.push(selectedOptions);
                    model.ColumnItems.push(column);
                    //self.IsRemoveEnable(true);

                }

                //  model.allItems.push(selectedOptions);
                self.reportBuilderModel().Text("");
                self.reportBuilderModel().FromDate("");
                self.reportBuilderModel().ToDate("");
                self.reportBuilderModel().DateValue("");
                self.reportBuilderModel().ValueNumber("");
                self.reportBuilderModel().ResultValue("");
                
            }
        }       

        self.RemoveQuery = function (model) {
            //$('#divColumnList').show();
            model.allItems.removeAll(model.selectedItems());
            model.selectedItems([]); // Clear selection

            if (model.ColumnItems() != "") {
                ko.utils.arrayForEach(model.ColumnItems(), function (val) {
                    if (val == model.SelectedddlColumn()) {
                        model.ColumnItems.remove(model.SelectedddlColumn());

                    }
                });
            }

        }

        self.validationEvent = function (model) {
            
            //$('#divColumnList').show();
            var sts = true;
            if (model.ReportbuilderId() == "") {
                alert("You must select Report Category");
                sts = false;
            }
            else {
                if (model.SelectedddlColumn() == "") {
                    alert("You must Select Coulmn");
                    sts = false;
                }
                else {

                    if (model.OPERATOR_VALUE1() == "") {
                        alert("Please enter Operator Value");
                        sts = false;
                    }
                    else {

                        if (model.ResultValue() == "") {
                            if (model.OPERATOR_VALUE1() == " is NULL " || model.OPERATOR_VALUE1() == " is NOT NULL ")
                            { sts = true; }
                            else
                            {
                                alert("Please Enter Value Fields");
                                sts = false;
                            }
                        }

                    }
                }
            }

            return sts;

        }

        self.Submit = function (model) {
            
            var stats = true;
            //$('#divColumnList').show();
            if (model.allItems() == "") {
                alert("You should select at least one condition");
                stats = false;
                return false;
            }
            if (model.Selectedmultilistcolumn() == "")
            {
                alert("Please select at least one column.");
                stats = false;
            }            
            //var count = $('#divColumnsList input[name="column"]:checked').length;

            //if (count == 0) {
            //    toastr.warning("Please select at least one column.", "Warning");
            //    stats = false;
            //    return false;
            //}
            if (stats) {
                //$("#id1").removeClass("expand").addClass("collapse");
                //$("#anchor1").removeClass("collapse").addClass("expand");
                //$("#id1").css("display", "none");
                $("#btnsId").show();

                if (self.reportBuilderModel().items().length > 0) {
                    
                    var arvalue = self.reportBuilderModel().items().length;
                    for (var i = 0; i < arvalue; i++) {
                        self.reportBuilderModel().items.pop();
                    }
                }

                var column_value = "";
                var columnType = "";

                ko.utils.arrayForEach(model.Selectedmultilistcolumn(), function (sms) {
                    
                    ko.utils.arrayForEach(self.reportCategorycolumntypeList(), function (rcc) {

                        if (sms == rcc.COLUMN_NAME()) {
                            if (columnType == "")
                                columnType = rcc.DATA_TYPE();

                            else
                                columnType = columnType + "," + rcc.DATA_TYPE();

                        }

                    });
                });

                
                //$('#divColumnsList input[name="column"]:checked').each(function () {                   
                //    var col = this.value;
                //    ko.utils.arrayForEach(self.reportCategorycolumntypeList(), function (rcc) {                        
                //        if (col == rcc.COLUMN_NAME()) {                            
                //            if (columnType == "")                                
                //                    columnType = rcc.DATA_TYPE();
                //            else
                //                    columnType = columnType + "," + rcc.DATA_TYPE();
                //           }     
                //        });
                //   });

                model.ColumnTypes = columnType;

                //$('#divColumnsList input[name="column"]:checked').each(function () {
                 
                //    var col = this.value;
                //    if (column_value == "")
                //                column_value = col;

                //            else
                //                column_value = column_value + "," + col;

                //        });

                ko.utils.arrayForEach(model.Selectedmultilistcolumn(), function (val) {
                    if (column_value == "")
                        column_value = val;

                    else
                        column_value = column_value + "," + val;

                });

                var serach_filter = ""
                ko.utils.arrayForEach(model.allItems(), function (val) {
                    if (serach_filter == "")
                        serach_filter = val;
                    else
                        serach_filter = serach_filter + " and " + val;
                });

                model.SearchFilter = serach_filter;
                model.DisplayColumns = column_value;

                if (model.OrderByAD == "") {
                    model.OrderByAD = "asc";
                }
               
                self.viewModelHelper.apiPost('api/ReportBuilder/ReportBuilderGridData', ko.mapping.toJSON(model), function Message(data) {
                  
                    if (data == undefined) {
                        alert('Data is not availbale for Your query');
                    }
                    else {
                        
                        model.reslt(data[0].Result);
                    }

                    self.items = ko.observableArray("");
                    
                    ko.utils.arrayForEach(data, function (val) {
                        
                        var result = jQuery.parseJSON(val.Result);
                        var DateFormat = null;

                        //Get Current Portcode
                        var PortCode = $.cookie('Port');
                        var decryptedPortCode = "";
                        if (PortCode != undefined) {

                            //decrypted code
                            for (var i = 0; i < PortCode.length; i++) {
                                decryptedPortCode += String.fromCharCode(PortCode.charCodeAt(i) ^ 6);
                            }
                        }///

                        //Get DateFormat Config value
                        self.viewModelHelper.apiGet('api/ReportBuilder/GetDateFormatConfig', { PortCode: decryptedPortCode },
                            function (result) {
                                //DateFormat = result;
                                DateFormat='yyyy-MM-dd'
                                model.DateFormat = DateFormat;
                                DateFormat = DateFormat.toUpperCase();
                            }, null, null, false);
                        ko.utils.arrayForEach(result, function (value) {
                            
                            var len = Object.keys(value).length;
                            for (var i = 0; i < len; i++) {
                                var setvalue = value[Object.getOwnPropertyNames(value)[i]];

                                if (setvalue.match(/^\d\d\d\d-\d\d-\d\d/)) {
                                    //setvalue = moment(setvalue).format(DateFormat);
                                    setvalue = moment(setvalue).format('YYYY-MM-DD');
                                }
                                value[Object.getOwnPropertyNames(value)[i]] = setvalue;
                            }

                            self.items.push(value);
                            self.reportBuilderModel().items.push(value);

                        });
                    });
                    self.reportBuilderModel().dataLength(self.items().length);
                    self.reportBuilderModel().pageindex(3);

                    /////////////////////////////////
                    var datatypeTemp = "";
                    var columnTemp = "";

                    if (model.DisplayColumns == "") {
                        ko.utils.arrayForEach(self.reportCategorycolumntypeList(), function (value) {
                            
                            if (columnTemp == "") {
                                columnTemp = value.COLUMN_NAME();
                                datatypeTemp = value.DATA_TYPE();
                            }
                            else {
                                columnTemp = columnTemp + "," + value.COLUMN_NAME();
                                datatypeTemp = datatypeTemp + "," + value.DATA_TYPE();
                            }
                        });
                        
                        model.DisplayColumns = columnTemp;
                        model.ColumnTypes = datatypeTemp;
                    }
                    //////////////////////////
                    
                    var col_list = { DisplayColumns: model.DisplayColumns, Result: model.reslt, ColumnTypes: model.ColumnTypes, QueryTemplateName: model.QueryTemplateName, ReportHeader: model.ReportHeader, DateFormat: model.DateFormat }
                    $.ajax({
                        url: 'SetData',
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: ko.mapping.toJSON(col_list),
                        success: function (data, textStatus, jqXHR) {
                        },
                        error: function (error) {
                            var err = error.Message;

                        }
                    });
                });
            }
        }

        self.saveCriteriaPopup = function (model) {
            
            //$('#divColumnList').show();
            if (model.allItems() == "") {
                alert("You should select at least one condition for save.");
                return false;
            }
        }

        self.SaveCriteria = function (model) {
            
            //$('#divColumnList').show();
            //var rep = new IPMSRoot.ReportQueryTemplate();
            //rep.QueryTemplateName = model.QueryTemplateName();
            //rep.ReportHeader = model.ReportHeader();

            self.ServiceValidation = ko.observable(model);
            self.ServiceValidation().errors = ko.validation.group(self.ServiceValidation());
            var errors = self.ServiceValidation().errors().length;
            if (errors == 0) {
                var flagValue;
                if (model.QueryTemplateId() == undefined) {
                    flagValue = "I";
                }
                else
                    if (model.QuerytemplateNameCopy() == model.QueryTemplateName()) {

                        flagValue = "U";

                    }
                    else {

                        flagValue = "I";
                    }


                var stts = true;

                if (model.allItems() == "") {
                    alert("You should select at least one condition for save");
                    stts = false;
                }
                if (stts) {
                    if (self.reportBuilderModel().items().length > 0) {
                        var arvalue = self.reportBuilderModel().items().length;
                        for (var i = 0; i < arvalue; i++)
                            self.reportBuilderModel().items.pop();
                    }

                    var serach_filter = ""
                    ko.utils.arrayForEach(model.allItems(), function (val) {
                        if (serach_filter == "")
                            serach_filter = val;
                        else
                            serach_filter = serach_filter + " and " + val;

                    });

                    model.SearchFilter = serach_filter;
                    //$('#divColumnsList input[name="column"]:checked').each(function () {
                    //    
                    //    var col = this.value;                     
                        
                    //    if (model.COLUMN_NAME == "")
                    //        model.COLUMN_NAME=col;

                    //    else
                    //        model.COLUMN_NAME = model.COLUMN_NAME + "," + col;

                    //});
                    //model.ColumnData = model.COLUMN_NAME;
                    
                    //model.Selectedmultilistcolumn = model.DisplayColumns.split(',')
                    model.COLUMN_NAME = $("#Column option:selected").text();
                    model.DATA_TYPE = self.reportBuilderModel().DATA_TYPE;
                    var obj = new IPMSRoot.ReportQueryTemplate();
                    obj.QueryTemplateId = 0;
                    obj.ReportbuilderId = self.reportBuilderModel().ReportbuilderId;
                    obj.QueryTemplateName = self.reportBuilderModel().QueryTemplateName;
                    obj.ReportHeader = self.reportBuilderModel().ReportHeader;
                    obj.QueryTemplateId = self.reportBuilderModel().QueryTemplateId;

                    obj.Flag = flagValue;
                    obj.UserQuery = ko.toJSON(model);


                    self.viewModelHelper.apiPost('api/ReportBuilder/AddReportQueryTemplate',
                       ko.mapping.toJSON(obj),
                         function Message(data) {
                             toastr.options.closeButton = true;
                             toastr.options.positionClass = "toast-top-right";
                             toastr.success("Report Criteria Saved Successfully", "Report Criteria");
                             $(".close").trigger("click");
                             self.ReportFilterData();

                         });
                }
            }
            else {
                self.ServiceValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ReportFilterData = function () {
            //$('#divColumnList').show();
            self.viewModelHelper.apiGet('api/ReportBuilder/GetReportFilterData',
                 null,
               function (result) {
                   ko.mapping.fromJS(result, {}, self.reportFilterList)
               }, null, null, false);
        }

        self.viewFilter = function (data) {
           
            $('#divColumnList').show();
            self.IsView(true);
            var ref_this = $("ul#mainmenu li.active");
            var tabs = ref_this.attr("id")
            switch (tabs) {
                case "li_tab0":
                    $('#li_tab1').addClass('active');
                    $('#tab_1').addClass('tab-pane active');

                    $('#li_tab0').removeClass('active');
                    $('#tab_0').removeClass('tab-pane active').addClass('tab-pane');
                    break;
                case "li_tab1":
                    $('#li_tab0').addClass('active');
                    $('#tab_0').addClass('tab-pane active');

                    $('#li_tab1').removeClass('active');
                    $('#tab_1').removeClass('tab-pane active').addClass('tab-pane');
                    break;
            }            
            self.LoadColumns(data)
            
            var refdata = JSON.parse(data.UserQuery());

            var obj = new IPMSRoot.ReportBuilderModel();
            obj.ReportbuilderId(data.ReportbuilderId());
            obj.SelectedddlColumn(refdata.COLUMN_NAME);
            obj.OPERATOR_VALUE1(refdata.OPERATOR_VALUE1);            
            obj.DATA_TYPE(refdata.DATA_TYPE);
            obj.Value(refdata.Value);
            obj.allItems(refdata.allItems);
            obj.Selectedmultilistcolumn(refdata.Selectedmultilistcolumn);
            obj.OrderBy(refdata.OrderBy);
            obj.OrderByAD(refdata.OrderByAD);
            obj.QueryTemplateName(data.QueryTemplateName());
            obj.QuerytemplateNameCopy(data.QueryTemplateName());
            obj.ReportHeader(data.ReportHeader());
            obj.QueryTemplateId(data.QueryTemplateId());
            self.reportBuilderModel(obj);
            //$.each(refdata.Selectedmultilistcolumn, function (key, value) {               
            //    $('#chkPort_' + value).attr('checked', true);
            //    $('#chkPort_' + value).css('outline', '')
            //});            
            //var cnt = 0;
            //$('#divColumnList input[name="column"]:checked').each(function () {
            //    if (this.checked)
            //        cnt += 1;
            //});
            //if ($('#divColumnsList input[name="column"]').length == cnt)
            //    $('#chkAllColumns').attr('checked', true);

           

            self.ReportQueryOperator();

        }

        self.DeleteFilter = function (data) {             
            var TemplateId = data.QueryTemplateId();
            self.viewModelHelper.apiGet('api/ReportBuilder/DeleteReportQueryTemplate',
                      { ID: TemplateId },
                    function (result2) {                     
                        toastr.success("Record deleted successfully.", "Success");
                        self.ReportFilterData();
                        stats = true;
                    }, null, null, false);
            
        }


        self.ResetReport = function (event) {
            
            self.itemsEmpty();
            self.reportBuilderModel().reset();
            
           

        }

        self.Initialize();
       

        //var ReportColumn = function (columnName) {
        //    var self = this;
        //};

       
        //ReportColumnClick = function (ctrl) {
        //    

        //    var Adddoc = new IPMSROOT.ReportCategoryColumnType();

        //    Adddoc.COLUMN_NAME(ctrl.value);

        //    self.ColumnData.push(Adddoc);
        //    self.Selectedmultilistcolumn.push(Adddoc)
        //}


    }

    IPMSRoot.ReportBuilderViewModel = ReportBuilderViewModel;
}(window.IPMSROOT));

//Checking for the columns
function toggle(source) {
    //checkboxes = document.getElementsByName('column');
    //for (var i = 0, n = checkboxes.length; i < n; i++) {
    //    checkboxes[i].checked = source.checked;
    //}
}

//  column Selection Check box for single
function toggleSingle(Single) {       
    //checkboxes = document.getElementsByName('column');
    //var chk = true;
    //for (var i = 0, n = checkboxes.length; i < n; i++) {
    //    if (!checkboxes[i].checked) {
    //        chk = false;
    //    }
    //}

    //checkboxesAll = document.getElementsByName('AllColumns');
    //checkboxesAll[0].checked = chk;
}
