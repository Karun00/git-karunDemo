toastr.options.closeButton = true;
toastr.options.positionClass = "toast-top-right";

(function (IPMSRoot) {
    var DredgingPriorityViewModel = function (dredgingpriorityid, viewDetail) {
        var self = this;
        $('#spnTitile').html("Dredging Priority");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.dredgingpriorityModel = ko.observable();
        self.dredgingPriorityAreaModel = ko.observable();
        self.dredgingPriorityDocumentModel = ko.observable();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsUnique = ko.observable(true);
        self.UniqueNameVisible = ko.observable(false);
        self.IsCodeEnable = ko.observable(true);
        self.validationHelper = new IPMSRoot.validationHelper();
        self.dredgingpriorityreferenceData = ko.observable();
        self.DredgingPriorityList = ko.observableArray();
        self.DredgingPriorityAreaList = ko.observableArray();
        self.isfileToUpload = ko.observable(false);
        self.fileSizeConfigValue = ko.observable();
        self.isUploadFileVisible = ko.observable(true);
        self.IsModified = ko.observable(false);
        self.isDelUploadFileVisible = ko.observable(true);
        self.isfileToUpload = ko.observable(false);
        self.isGridsEnable = ko.observable(true);
        self.isGridbtnEnable = ko.observable(false)
        self.DredgingPriorityVolumes = ko.observableArray([]);
        self.isAreaEnable = ko.observable(true);
        self.AreaValues = ko.observableArray();
        self.FinancialYearList = ko.observableArray();
        self.StartDate = ko.observable();
        self.IsBerthEnable = ko.observable(true);
        self.IsLocationEnable = ko.observable(true);

        self.YearID = ko.observable();
        self.DredgingpriorityId = ko.observable();
        self.Documents = ko.observableArray();
        self.IsCancel = ko.observable(false);
        self.IsAreaEdit = ko.observable(true);
        self.MonthList = ko.observableArray();
        self.MonthsList = ko.observableArray();
        self.MonthName = ko.observableArray();
        self.dredgingDocument = ko.observableArray();
        self.IsIconEdit = ko.observable(true);
        self.PortList = ko.observableArray();
        // Initialize method is fires in  pageload Initializetion mode
        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.GetFileSizeConfigValue();
            self.dredgingpriorityModel(new IPMSROOT.DredgingPriorityModel());
            self.dredgingPriorityAreaModel(new IPMSROOT.DredgingPriorityAreaModel());
            self.dredgingPriorityDocumentModel(new IPMSROOT.dredgingdocument());
            self.dredgingpriorityModel().StartDate(new Date());
            self.LoadInitialData();
            self.LoadFinancialYear();
            self.LoadDredgingPriorityArea();
            self.viewMode('List');
            $("#btnCancel").hide();
            //  self.dredgingpriorityModel().MonthValue();

        }



        self.LoadFinancialYear = function () {
            self.viewModelHelper.apiGet('api/DredgingPriority/GetFinancialYear',
           null,
             function (result) {
                 var FinancialYears = $.map(result, function (item) {
                     return new self.FinancialYear(item);
                 });
                 $('#spanMonth').css('display', '');
                 self.FinancialYearList(FinancialYears);
                 self.LoadDredgingPrioritys();
                 self.LoadVolumes();
                 self.LoadMonthes(self.YearID());
                 //  self.dredgingpriorityModel().MonthValue(self.DredgingPriorityList().MonthValue());
             });

        }

        self.FinancialYear = function (data) {
            debugger;
            self.StartDate = ko.observable(data.StartDate)//.extend({

            self.EndDate = ko.observable(data.EndDate)//.extend({
            var StartDateValue = data.StartDate;
            var myDatePicker = new Date(StartDateValue);
            var EndDateValue = data.EndDate;
            var myDatePicker1 = new Date(EndDateValue);
            var CDate = (moment(new Date()).format('YYYY-MM-DD'));
            var SDate = ko.observable(data.StartDate)
            var SDate1 = data.SDate
            var SYear = (moment(myDatePicker).format('YYYY-MM-DD'));
            var EDate = ko.observable(data.EndDate)
            var EDate1 = data.EDate
            var EYear = (moment(myDatePicker1).format('YYYY-MM-DD'));
            if (CDate >= SYear && CDate <= EYear) {
                self.YearID = ko.observable(data.FinancialYearID);
                self.dredgingpriorityModel().FinancialYearID(self.YearID());
            }

            this.FinancialYear = ko.observable(data.FinancialYear);
            this.FinancialYearID = ko.observable(data.FinancialYearID);
        };
        self.addDredgingPriority = function () {
            $("#btnCancel").show();
            self.viewMode('Form');
            self.isGridsEnable(true);
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.IsUnique(true);
            self.IsAreaEdit(true);
            self.isDelUploadFileVisible(true);
            self.DredgingPriorityVolumes(false);
            self.dredgingpriorityModel(new IPMSROOT.DredgingPriorityModel());
            self.dredgingpriorityModel().FinancialYearID(self.YearID());
            self.dredgingpriorityModel().StartDate(new Date());
            self.LoadVolumes();
            self.LoadMonthes(self.YearID());
            self.LoadDredgingPriorityArea();
        }
        self.LoadVolumes = function () {
            var FinancialYearID = self.YearID();
            if (FinancialYearID != "") {
                self.viewModelHelper.apiGet('api/DredgingPriorityVolumes/' + FinancialYearID,
                  { FinancialYearID: FinancialYearID },
                   function (result) {
                       self.DredgingPriorityVolumes(ko.utils.arrayMap(result, function (item) {
                           self.dredgingpriorityModel().DeploymentPlanID(item.DeploymentPlanID);
                           self.dredgingPriorityAreaModel().DeploymentPlanID(item.DeploymentPlanID);
                           return new IPMSRoot.KeyEvent(item);
                       }));

                   });
                self.viewModelHelper.apiGet('api/DredgingPrioritys/' + FinancialYearID,
               { FinancialYearID: FinancialYearID },
          function (result) {

              self.DredgingPriorityList(ko.utils.arrayMap(result, function (item) {
                  return new IPMSRoot.DredgingPriorityModel(item);
              }));


          });
            }
            else {
                self.DredgingPriorityVolumes(false)
            }
        }

        self.LoadVolumeschange = function (event) {
            debugger;


            if (event.FinancialYearID() != "") {
                self.viewModelHelper.apiGet('api/DredgingPriorityVolumes/' + event.FinancialYearID(),
                  { FinancialYearID: event.FinancialYearID() },
                   function (result) {
                       self.DredgingPriorityVolumes(ko.utils.arrayMap(result, function (item) {
                           return new IPMSRoot.KeyEvent(item);
                       }));

                   });
                self.viewModelHelper.apiGet('api/DredgingPrioritys/' + event.FinancialYearID(),
                 { FinancialYearID: event.FinancialYearID() },
            function (result) {

                self.DredgingPriorityList(ko.utils.arrayMap(result, function (item) {
                    return new IPMSRoot.DredgingPriorityModel(item);
                }));


            });
                self.LoadMonthes(event.FinancialYearID());
                //    $('#spanMonth').css('display', '');
                //    $('#spanFinancialYear').text('');
                //    self.viewModelHelper.apiGet('api/GetDredgingMonthes/' + event.FinancialYearID(),
                // { FinancialYearID: event.FinancialYearID() },
                //function (result) {
                //    var TotMonthlists = $.map(result, function (item) {
                //        return new self.TotMonthlist(item);
                //    });
                //    self.MonthList(TotMonthlists);
                //});
            }
            else {
                self.DredgingPriorityVolumes(false);
                self.MonthsList.removeAll();
                $('#spanFinancialYear').text('* Please Select Financial Year');
                $('#spanFinancialYear').css('display', '');
            }

        }
        self.LoadVolumeschangeList = function (event) {
            if (event.FinancialYearID() != "") {
                self.viewModelHelper.apiGet('api/DredgingPriorityVolumes/' + event.FinancialYearID(),
                   { FinancialYearID: event.FinancialYearID() },
                    function (result) {
                        self.DredgingPriorityVolumes(ko.utils.arrayMap(result, function (item) {
                            return new IPMSRoot.KeyEvent(item);
                        }));

                    });
                self.viewModelHelper.apiGet('api/DredgingPrioritys/' + event.FinancialYearID(),
                  { FinancialYearID: event.FinancialYearID() },
             function (result) {

                 self.DredgingPriorityList(ko.utils.arrayMap(result, function (item) {
                     return new IPMSRoot.DredgingPriorityModel(item);
                 }));


             });


                //      self.viewModelHelper.apiGet('api/GetDredgingMonthes/' + event.FinancialYearID(),
                // { FinancialYearID: event.FinancialYearID() },
                //function (result) {
                //    var TotMonthlists = $.map(result, function (item) {
                //        return new self.TotMonthlist(item);
                //    });
                //    self.MonthList(TotMonthlists);
                //});

            }
            else {
                self.DredgingPriorityVolumes(false);
                self.viewModelHelper.apiGet('api/DredgingPrioritys/' + 0,
                   { FinancialYearID: 0 },
              function (result) {

                  self.DredgingPriorityList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.DredgingPriorityModel(item);
                  }));


              });

            }


        }

        // Get MonthsName
        self.LoadMonthes = function (FinancialYearID) {
            //var FinancialYearID = self.YearID();
            self.viewModelHelper.apiGet('api/GetDredgingMonthes/' + FinancialYearID,
             { FinancialYearID: FinancialYearID },
            function (result) {
                var TotMonthlists = $.map(result, function (item) {
                    return new self.TotMonthlist(item);
                });
                self.MonthList(TotMonthlists);
            });
        }
        self.TotMonthlist = function (data) {
            self.MonthsList.removeAll();
            $('#spanMonth').text('');
            self.StartDate = ko.observable(data.StartDate)
            var SValue = data.StartDate;
            myDatePicker = new Date(SValue);
            var smonth = myDatePicker.getMonth();
            var syear = myDatePicker.getFullYear();
            var sday = myDatePicker.getDate();
            var sdate = syear + '-' + (smonth) + '-' + sday;
            var StartDateValue = moment(sdate).format('YYYY-MM-DD');
            var startMonth = self.StartDate.getMonth
            self.EndDate = ko.observable(data.EndDate)
            var EndDateValue = moment(data.EndDate).format('YYYY-MM-DD');
            var temp = StartDateValue;
            while (moment(temp).format('YYYY-MM') != moment(EndDateValue).format('YYYY-MM')) {

                temp = moment(temp).add(1, 'M');
                var month = moment(temp).format('MMMM');
                console.log(moment(temp).format('YYYY-MM'));
                self.MonthName.push(month);
                var MonthObj = { MonthValue: moment(temp).format('YYYY-MM'), MonthName: month }
                self.MonthsList.push(MonthObj);

            }
        };
        PreventBackSpace = function () {
            return self.validationHelper.PreventBackspaces_keydownEvent(event);
        }
        self.WorkflowAction = function (dat) {
            var action = new IPMSROOT.WorkFlowCommon();
            self.ENValidation = ko.observable(dat);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (dat.HasRemarks() == "N") { errors = 0; }
            if (errors == 0) {
                action.SubmitAction(dat, self.dredgingpriorityModel());
            }
            else {

                self.ENValidation().errors.showAllMessages();

            }
        }

        Requireddate = function () {
            var month = $("#Month").val();
            var FromDate = moment(month + '-' + 01 + ' 00:00:00');
            var ToDate = FromDate.clone().endOf('month');
            var myDatePicker = new Date(FromDate);
            var day = myDatePicker.getDate();
            var month = myDatePicker.getMonth();
            var year = myDatePicker.getFullYear();
            var myDatePicker = new Date(ToDate);
            var day1 = myDatePicker.getDate();
            var month1 = myDatePicker.getMonth();
            var year1 = myDatePicker.getFullYear();
            $("#RequiredDate").data('kendoDatePicker').min(new Date(year, month, day));
            $("#RequiredDate").data('kendoDatePicker').max(new Date(year1, month1, day1));
        }

        Monthconvertion = function (data, event) {
            debugger;
            $('#spanMonth').css('display', '');
            var Monthvelidate = true;
            var flag = true;
            var items = JSON.parse(ko.toJSON(self.DredgingPriorityList()));
            var entry = JSON.parse(ko.toJSON(data));
            $.each(items, function (index, value) {
                if (value.MonthValue == entry.MonthValue) {
                    $('#spanMonth').text('Already Exists! Select Other Month');
                    $('#spanMonth').css('display', '');
                    flag = false;
                }
                // }
                return;
            });


            if (flag == true) {
                $('#spanMonth').text('');
            }
            if (Monthvelidate == true) {
                var month = $("#Month").val();
                var FromDate = moment(month + '-' + 01 + ' 00:00:00');
                var ToDate = FromDate.clone().endOf('month');
                self.dredgingpriorityModel().FromDate(moment(FromDate).format('YYYY-MM-DD'));
                self.dredgingpriorityModel().ToDate(moment(ToDate).format('YYYY-MM-DD'));
                self.dredgingpriorityModel().Month(moment(ToDate).format('MMMM'));
            }
            else { }
        }

        self.LoadDredgingPriorityArea = function (DredgingPriorityID) {
            // var DredgingPriorityID = self.dredgingpriorityModel().DredgingPriorityID();
            self.viewModelHelper.apiGet('api/DredgingPriorityArea/' + DredgingPriorityID, {},
              function (result) {

                  self.DredgingPriorityAreaList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.DredgingPriorityModel(item);
                  }));


              });

        }

        self.viewdredgingpriorityPending = function (dredgingpriority) {
            $("#btnCancel").show();
            var DredgingPriorityID = dredgingpriority.DredgingOperationID();
            self.viewModelHelper.apiGet('api/DredgingPriorityAreas/' + DredgingPriorityID, {},
              function (result) {

                  self.DredgingPriorityAreaList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.DredgingPriorityModel(item);
                  }));
              });


            self.viewMode('Form');
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.IsAreaEdit(false);
            self.editableView(false);
            self.isGridsEnable(false);
            self.isDelUploadFileVisible(false);

            self.dredgingpriorityModel(dredgingpriority);

            self.IsIconEdit(false);
            // self.LoadMonthes(dredgingpriority.FinancialYearID());
            //self.dredgingpriorityModel().MonthValue(dredgingpriority.MonthValue());
            $("#RequiredDate").data('kendoDatePicker').enable(false);
          //  var dropdownlist = $("#Month").data("kendoDropDownList");
           // $("#Month").val(dredgingpriority.MonthValue());
            //dropdownlist.enable(true);
            var dropdownlist1 = $("#FinancialYear").data("kendoDropDownList");
            dropdownlist1.enable(false);
            $('#spnTitile').html("View Dredging Priority");

           
            var DredgingPriorityID = dredgingpriority.DredgingPriorityID();
            self.viewModelHelper.apiGet('api/DredgingPriorityDocument/' + DredgingPriorityID, {},
          // null,
              function (result) {
                  self.Documents(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.dredgingdocument(item);

                  }));

                  self.dredgingpriorityModel().DredgingPriorityDocumentsVO(self.Documents());
              });
            self.dredgingpriorityModel().MonthValue(dredgingpriority.MonthValue());
            var FinancialYearID = dredgingpriority.FinancialYearID();
            self.viewModelHelper.apiGet('api/GetDredgingMonthes/' + FinancialYearID,
             { FinancialYearID: FinancialYearID },
            function (result) {
                var TotMonthlists = $.map(result, function (item) {
                    return new self.TotMonthlist(item);
                });
                self.MonthList(TotMonthlists);
                $("#Month").val(dredgingpriority.MonthValue());
                console.log('123', self.MonthsList());
                console.log('234',dredgingpriority.MonthValue());
            });
           
            var ReferenceID = dredgingpriority.DredgingOperationID();
            var WorkflowInstanceID = dredgingpriority.DPAWorkflowInstanceID();
            self.viewModelHelper.apiGet('api/WorkFlowTasks/' + ReferenceID + '/' + WorkflowInstanceID,
                   null,
                         function (result) {

                             ko.utils.arrayForEach(result, function (val) {
                                 var pendingtaskaction = new IPMSROOT.pendingTask();
                                 pendingtaskaction.WorkflowInstanceId(val.WorkflowInstanceId);
                                 pendingtaskaction.ReferenceID(val.ReferenceID);
                                 pendingtaskaction.TaskCode(val.WorkflowTaskCode);
                                 pendingtaskaction.APIUrl(val.APIUrl);
                                 pendingtaskaction.TaskName(val.TaskName);
                                 pendingtaskaction.PreviousRemarks(val.PreviousRemarks);
                                 pendingtaskaction.TaskDescription(val.TaskDescription);
                                 pendingtaskaction.HasRemarks(val.HasRemarks);
                                 self.dredgingpriorityModel().pendingTasks.push(pendingtaskaction);
                             });
                         });

        }
        self.LoadDredgingPrioritys = function () {
            var FinancialYearID = self.YearID();
            if (viewDetail == true) {
                self.viewModelHelper.apiGet('api/DredgingPriority/' + dredgingpriorityid,
                 { dredgingpriorityid: dredgingpriorityid },
                  function (result) {
                      self.DredgingPriorityList(ko.utils.arrayMap(result, function (item) {
                          return new IPMSRoot.DredgingPriorityModel(item);
                      }));
                      self.viewdredgingpriorityPending(self.DredgingPriorityList()[0]);
                  });
            }
            else {


                self.viewModelHelper.apiGet('api/DredgingPrioritys/' + FinancialYearID,
             { FinancialYearID: FinancialYearID },
              function (result) {

                  self.DredgingPriorityList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.DredgingPriorityModel(item, self.DredgingPriorityVolumes());
                  }));


              });

            }
        }
        //--------------------------------



        self.EnableArea = function (event) {
            debugger;
            if (event.AreaType() == "B") {
                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(false);
                dropdownlist.value('');
                $("#Location").val('');
                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(true);
                $('#spanvelBerth').css('display', 'inline');
                $('#spanvelLocation').css('display', 'none');
                $('#spnLocation').text('');
                $('#spnBerthCode').text('');
                $('#spn1AreaType').text('');
            }
            if (event.AreaType() == "L") {
                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(true);
                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#Berth').val('');
                dropdownlist.value('');
                //dropdownlist.val('');
                $('#spanvelBerth').css('display', 'none');
                $('#spanvelLocation').css('display', 'inline');
                $('#spnLocation').text('');
                $('#spnBerthCode').text('');
                $('#spn1AreaType').text('');
            }
            if (event.AreaType() == "0") {
                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(false);
                dropdownlist.value('');
                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(false);
                dropdownlist.value('');
                $('#spanvelBerth').css('display', 'none');
                $('#spanvelLocation').css('display', 'none');
                $('#spnLocation').text('');
                $('#spnBerthCode').text('');
                $('#spn1AreaType').text('');
            }
        }


        self.BindKey = function (keyList) {
            console.log(keyList);
            self.DredgingPriorityVolumes(ko.utils.arrayMap(keyList, function (item) {
                return new IPMSRoot.KeyEvent(item);
            }));
            console.log(self.DredgingPriorityVolumes());
        }

        self.LoadInitialData = function () {

            self.viewModelHelper.apiGet('api/DredgingPriorityReferenceData', null,
                    function (result1) {
                        //TODO: Load this into self.ReferenceData().  and do it only once. not every time.
                        self.dredgingpriorityreferenceData(new IPMSRoot.DredgingPriorityReferenceData(result1));
                    }, null, null, false);
        }

        // To get the file size config value
        self.GetFileSizeConfigValue = function () {
            self.viewModelHelper.apiGet('api/File/GetFileSizeConfigValue',
          null,
            function (result) {
                self.fileSizeConfigValue(result);
                ko.mapping.fromJS(result, {}, self.fileSizeConfigValue);

            });
        }

        Cancel = function () {

            if (viewDetail == true) {
                window.location.href = '/Welcome';
            }
            else {
                self.viewMode('List');
                $("#btnCancel").hide();
                self.dredgingpriorityModel(new IPMSROOT.DredgingPriorityModel());
                self.dredgingpriorityModel().reset();
                self.DredgingPriorityVolumes(false);
                $('#spnTitile').html("Dredging Priority");
                viewDetail = false;
                self.IsIconEdit(true);
                self.LoadFinancialYear();
                if (self.UniqueNameVisible() == true) {
                    self.UniqueNameVisible(false);
                }
            }
        }
        self.CancelDredgingPriorityArea = function () {
            $('#stack2').modal('hide')
            $('#divValidationError').addClass('display-none');
            $('#spnTitile').html("Dredging Priority");
            $("#btnCancel").show();
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }

        }

        //--------------------
        self.viewdredgingpriority = function (dredgingpriority) {
            // var DredgingPriorityID = dredgingpriority.DredgingPriorityID();
            self.LoadDredgingPriorityArea(dredgingpriority.DredgingPriorityID());
            self.viewMode('Form');
            $("#btnCancel").show();
            self.IsCodeEnable(false);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.IsAreaEdit(false);
            self.editableView(false);
            self.isGridsEnable(false);
            self.IsIconEdit(false);
            self.dredgingpriorityModel(dredgingpriority);
            self.LoadMonthes(dredgingpriority.FinancialYearID());
            self.isDelUploadFileVisible(false);
            $("#FromDate").data('kendoDatePicker').enable(false);
            $("#ToDate").data('kendoDatePicker').enable(false);
            $('#spnTitile').html("View Dredging Priority");
            $("#RequiredDate").data('kendoDatePicker').enable(false);
        }


        self.editdredgingpriority = function (dredgingpriority) {
            debugger;
            console.log('Edit', dredgingpriority);
            self.LoadDredgingPriorityArea(dredgingpriority.DredgingPriorityID());
            //var DredgingPriorityID = dredgingpriority.DredgingPriorityID();
            //self.viewModelHelper.apiGet('api/DredgingPriorityArea/' + DredgingPriorityID, {},
            //  function (result) {              

            //          self.DredgingPriorityAreaList(ko.utils.arrayMap(result, function (item) {                        
            //              return new IPMSRoot.DredgingPriorityModel(item);
            //      }));  


            //  });
            $("#btnCancel").show();
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.isGridsEnable(false);
            self.IsAreaEdit(true);
            self.isDelUploadFileVisible(true);
            self.dredgingpriorityModel(dredgingpriority);
            self.dredgingpriorityModel().StartDate(new Date());
            self.dredgingpriorityModel().MonthValue(dredgingpriority.MonthValue());
            self.LoadMonthes(dredgingpriority.FinancialYearID());
            self.IsCodeEnable(false);
            var dropdownlist = $("#Month").data("kendoDropDownList");
            dropdownlist.enable(false);
            $('#spnTitile').html("Update Dredging Priority");

        }

        self.ResetDredgingPriority = function (model) {
            $('#divValidationError').addClass('display-none');
            ko.validation.reset();
            $('#spnLocation').text('');
            $('#spnBerthCode').text('');
            $('#spn1AreaType').text('');
            model.validationEnabled(false);
            self.dredgingPriorityAreaModel().reset();
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if (model.AreaType() == 'B') {
                debugger;

                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(true);
                dropdownlist.value(model.BerthKey());
                $("#Berth").val(model.BerthKey());

                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#spanvelBerth').css('display', 'inline');
                $('#spanvelLocation').css('display', 'none');
            }
            else if (model.AreaType() == 'L') {
                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(true);
                dropdownlist.value(model.AreaLocationID());
                $("#Location").val(model.AreaLocationID());
                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#spanvelBerth').css('display', 'none');
                $('#spanvelLocation').css('display', 'inline');
            }
            else {
                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(false);
                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#spanvelBerth').css('display', 'none');
                $('#spanvelLocation').css('display', 'none');
            }
        }


        self.AddPriorityDtl = function () {
            debugger;
            var errors = 0
            if ($("#FinancialYear").val() == "") {
                errors = 1;
                $('#spanFinancialYear').text('* Please Select Financial Year');
                $('#spanFinancialYear').css('display', '');
            }
            if ($("#Month").val() == "") {
                errors = 1;
                $('#spanMonth').text('* Please Select Month Name');
                $('#spanMonth').css('display', '');
            }

            if ($('#spanMonth').text() != '') {
                errors = 1;
            }

            if (errors == 0) {
                self.IsUpdate(false);
                self.IsSave(true);
                self.IsReset(true);
                self.IsCancel(true);
                self.editableView(true);
                self.isGridsEnable(true);
                if (self.UniqueNameVisible() == true) {
                    self.UniqueNameVisible(false);
                }
                self.dredgingPriorityAreaModel(new IPMSROOT.DredgingPriorityAreaModel());
                $('#stack2').modal('show')
                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(false);
                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#spanvelBerth').css('display', 'none');
                $('#spanvelLocation').css('display', 'none');
                Requireddate();
            }
            else {

                toastr.options.positionClass = "toast-top-right";
                toastr.warning('Dredging Priority Field Error : Please Select Valid Financial Year and Month Name ');
                return;
            }
            $('#RequiredDate').data('kendoDatePicker').enable(true);
            $('#FromDate').data('kendoDatePicker').enable(true);
            $('#ToDate').data('kendoDatePicker').enable(true);
            $('#spnTitile1').html("Add Dredging Priority");

        }
        ValidDate = function (data, event) {

            var some = JSON.parse(ko.toJSON(data));
            var startDate = FromDate.value;
            var endDate = ToDate.value;
            var RDate = RequiredDate.value;
            if (startDate) {
                $("#ToDate").kendoDatePicker({
                    min: startDate,
                    format: "yyyy-MM-dd ",
                    parseFormats: ["yyyy-MM-dd"],

                });
            }
            if (endDate) {
                self.dredgingpriorityModel().ToDate(endDate);
            }

        }
        HandleLocationNameKeyUp = function (data, event) {

            debugger;
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            if ($("#spnBerthCode").text() != '') {
                $("#spnBerthCode").text('');
            }
            if ($("#spnLocation").text() != '') {
                $("#spnLocation").text('');
            }
        }


        //To Save Dredging Priority Operation details
        self.SaveDredgingPriorityOperation = function (model) {
            console.log('model', model);
            debugger;
            $("#btnCancel").hide();
            $('#spnLocation').text('');
            $('#spnBerthCode').text('');
            $('#spn1AreaType').text('');
            model.validationEnabled(true);
            self.DredgingPriorityValidation = ko.observable(model);
            self.DredgingPriorityValidation().errors = ko.validation.group(self.DredgingPriorityValidation());

            var errors = self.DredgingPriorityValidation().errors().length;
            if (model.AreaType() == "L") {
                if ($("#Location").val() == "" || $("#Location").val() == null) {
                    $('#spnLocation').text('* Please select the Location');
                    errors++;
                }
                else {
                    var AreaName = self.dredgingpriorityreferenceData().LocationTypes();
                    for (var i = 0; i < AreaName.length; i++) {
                        var Location_ID = AreaName[i].LocationID();
                        var Location_Name = AreaName[i].LocationName();
                        if (model.AreaLocationID() == Location_ID) {
                            var L_Name = Location_Name;
                            var L_code = Location_ID;
                            self.dredgingpriorityModel().AreaName(L_Name);
                            self.dredgingpriorityModel().AreaLocationID(L_code);
                        }
                    }
                    self.dredgingpriorityModel().BerthCode('');
                    self.dredgingpriorityModel().QuayCode('');
                }

            }
            else if (model.AreaType() == "B") {
                if ($("#Berth").val() == "" || $("#Berth").val() == null) {
                    $('#spnBerthCode').text('* Please select the Berth');
                    errors++;
                }
                else {
                    var AreaName = self.dredgingpriorityreferenceData().BerthTypes();
                    for (var i = 0; i < AreaName.length; i++) {
                        var Berth_code = AreaName[i].BerthCode();
                        var Berth_name = AreaName[i].BerthName();
                        var Quay_Code = AreaName[i].QuayCode();
                        var Port_Code = AreaName[i].PortCode();
                        if (model.BerthKey() == Port_Code + "." + Quay_Code + "." + Berth_code) {
                            var B_Name = Berth_name;
                            var Q_code = Quay_Code;
                            var B_code = Berth_code;
                            self.dredgingpriorityModel().AreaName(B_Name);
                            self.dredgingpriorityModel().QuayCode(Q_code);
                            self.dredgingpriorityModel().BerthCode(B_code);
                        }
                    }
                    self.dredgingpriorityModel().AreaLocationID('');
                }
            }
            else if (model.AreaType() == "0") {
                $('#spn1AreaType').text('* Please select the AreaType');
                errors++;
                return;
            }
            var DredgingType = self.dredgingpriorityreferenceData().DredgingTypes();
            for (var i = 0; i < DredgingType.length; i++) {
                var DredgingType_ID = DredgingType[i].SubCatCode();
                var DredgingType_Name = DredgingType[i].SubCatName();
                if (model.TypeCode() == DredgingType_ID) {
                    var T_Name = DredgingType_Name;
                    self.dredgingpriorityModel().DredgingMaterial(T_Name);
                }
            }
            if (errors == 0) {
                var PriorityArea = new IPMSROOT.DredgingPriorityAreaModel();
                PriorityArea.Priority(model.Priority());
                // PriorityArea.AreaLocationID(model.AreaLocationID());
                PriorityArea.TypeCode(model.TypeCode());
                PriorityArea.RequiredDate(model.RequiredDate());
                PriorityArea.PromulgateDepth(model.PromulgateDepth());
                PriorityArea.Requirement(model.Requirement());
                PriorityArea.DPARemarks(model.DPARemarks());
                PriorityArea.AreaType(model.AreaType());
                PriorityArea.PortCode(model.PortCode());
                //  PriorityArea.QuayCode(model.QuayCode());
                // PriorityArea.BerthCode(model.BerthCode());
                PriorityArea.DesignDepth(model.DesignDepth());
                PriorityArea.PromulgateDepth(model.PromulgateDepth());
                PriorityArea.FinancialYearID(self.dredgingpriorityModel().FinancialYearID());
                PriorityArea.DredgingPriorityID(self.dredgingpriorityModel().DredgingPriorityID());
                PriorityArea.DredgingMaterial(self.dredgingpriorityModel().DredgingMaterial());
                PriorityArea.AreaName(self.dredgingpriorityModel().AreaName());
                PriorityArea.RequireDate(moment(model.RequiredDate()).format('YYYY-MM-DD'));
                PriorityArea.AreaLocationID(self.dredgingpriorityModel().AreaLocationID());
                PriorityArea.QuayCode(self.dredgingpriorityModel().QuayCode());
                PriorityArea.BerthCode(self.dredgingpriorityModel().BerthCode());

                model.FinancialYearID(self.dredgingpriorityModel().FinancialYearID());
                model.FromDate(self.dredgingpriorityModel().FromDate());
                model.ToDate(self.dredgingpriorityModel().ToDate());
                model.Month(self.dredgingpriorityModel().Month());
                model.DredgingPriorityDocumentsVO(self.dredgingpriorityModel().DredgingPriorityDocumentsVO());
                model.DeploymentPlanID(self.dredgingpriorityModel().DeploymentPlanID());

                model.DredgingPriorityID(self.dredgingpriorityModel().DredgingPriorityID());
                var duplicate = false;
                if (self.IsSave()) {
                    $.each(self.DredgingPriorityAreaList(), function (index, location) {
                        debugger;
                        if ((location.DredgingPriorityID()) == (model.DredgingPriorityID())) {
                            if ((location.RequiredDate()) == (moment(model.RequiredDate()).format('YYYY-MM-DD'))) {
                                if ((location.BerthCode()) == (model.BerthCode()) && (location.BerthCode()) != "" && (model.BerthCode()) != "" && (location.TypeCode()) == (model.TypeCode())) {
                                    self.UniqueNameVisible(true);
                                    duplicate = true;
                                    self.IsUnique(false);
                                }
                                if ((location.AreaLocationID()) == (model.AreaLocationID()) && (model.AreaLocationID() != "") && (location.AreaLocationID()) != "" && (location.TypeCode()) == (model.TypeCode())) {
                                    self.UniqueNameVisible(true);
                                    duplicate = true;
                                    self.IsUnique(false);
                                }
                            }
                        }
                        return;
                    });
                }
                if (!duplicate) {
                    self.IsUnique(true);
                }
                if (self.IsUnique() == true) {
                    model.DredgingOperationsVO.push(PriorityArea);

                    self.viewModelHelper.apiPost('api/DredgingPriority', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Dredging Priority Saved Successfully", "Dredging Priority");
                        self.dredgingpriorityModel().DredgingPriorityID(data.DredgingPriorityID);
                        self.LoadDredgingPriorityArea(data.DredgingPriorityID);
                        $('#divValidationError').addClass('display-none');
                        $('#stack2').modal('hide')
                        $('#spnTitile').html("Dredging Priority");
                        self.viewMode('Form');
                        $("#btnCancel").show();
                        if (self.UniqueNameVisible() == true) {
                            self.UniqueNameVisible(false);
                        }


                    });
                }
            }


            else {

                self.DredgingPriorityValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                $("#btnCancel").show();
                if (self.UniqueNameVisible() == true) {
                    self.UniqueNameVisible(false);
                }
                return;
            }

        }

        // To modify Dredging Priority data
        self.ModifyDredgingPriority = function (model) {
            debugger;
            console.log('model', model);
            $('#spnLocation').text('');
            $('#spnBerthCode').text('');
            $('#spn1AreaType').text('');
            model.Month(self.dredgingpriorityModel().Month());
            model.FinancialYearID(self.dredgingpriorityModel().FinancialYearID());
            model.FromDate(self.dredgingpriorityModel().FromDate());
            model.ToDate(self.dredgingpriorityModel().ToDate());
            model.DredgingPriorityDocumentsVO(self.dredgingpriorityModel().DredgingPriorityDocumentsVO());
            model.DeploymentPlanID(self.dredgingpriorityModel().DeploymentPlanID());

            model.DredgingPriorityID(self.dredgingpriorityModel().DredgingPriorityID());
            var PriorityArea = new IPMSROOT.DredgingPriorityAreaModel();
            PriorityArea.Priority(model.Priority());
            PriorityArea.TypeCode(model.TypeCode());
            PriorityArea.RequiredDate(model.RequiredDate());
            PriorityArea.PromulgateDepth(model.PromulgateDepth());
            PriorityArea.Requirement(model.Requirement());
            PriorityArea.DPARemarks(model.DPARemarks());
            PriorityArea.AreaType(model.AreaType());
            PriorityArea.PortCode(model.PortCode());
            PriorityArea.DesignDepth(model.DesignDepth());
            PriorityArea.PromulgateDepth(model.PromulgateDepth());
            PriorityArea.FinancialYearID(self.dredgingpriorityModel().FinancialYearID());
            PriorityArea.DredgingPriorityID(self.dredgingpriorityModel().DredgingPriorityID());
            PriorityArea.DredgingOperationID(model.DredgingOperationID());

            //-------------------------------------
            // PriorityArea.validationEnabled(true);
           // self.DredgingPriorityValidation = ko.observable(PriorityArea);
            model.validationEnabled(true);
            self.DredgingPriorityValidation = ko.observable(model);
            self.DredgingPriorityValidation().errors = ko.validation.group(self.DredgingPriorityValidation());
            var errors = self.DredgingPriorityValidation().errors().length;

            var TypeName = self.dredgingpriorityreferenceData().DredgingTypes();
            for (var i = 0; i < TypeName.length; i++) {
                var Type_ID = TypeName[i].SubCatCode();
                var Type_Name = TypeName[i].SubCatName();
                if (model.TypeCode() == Type_ID) {
                    var T_Name = Type_Name;
                    self.dredgingpriorityModel().DredgingMaterial(T_Name);
                }
            }
            if (model.AreaType() == "L") {
                if ($("#Location").val() == "" || $("#Location").val() == null) {
                    $('#spnLocation').text('* Please select the Location');
                    errors++;
                }
                else {
                    var AreaName = self.dredgingpriorityreferenceData().LocationTypes();
                    for (var i = 0; i < AreaName.length; i++) {
                        var Location_ID = AreaName[i].LocationID();
                        var Location_Name = AreaName[i].LocationName();
                        if (model.AreaLocationID() == Location_ID) {
                            var L_Name = Location_Name;
                            var L_code = Location_ID;
                            self.dredgingpriorityModel().AreaName(L_Name);
                            self.dredgingpriorityModel().AreaLocationID(L_code);
                        }
                    }
                    self.dredgingpriorityModel().BerthCode('');
                    self.dredgingpriorityModel().QuayCode('');
                }

            }
            else if (model.AreaType() == "B") {
                if ($("#Berth").val() == "" || $("#Berth").val() == null) {
                    $('#spnBerthCode').text('* Please select the Berth');
                    errors++;
                }
                else {
                    var AreaName = self.dredgingpriorityreferenceData().BerthTypes();
                    for (var i = 0; i < AreaName.length; i++) {
                        var Berth_code = AreaName[i].BerthCode();
                        var Berth_name = AreaName[i].BerthName();
                        var Quay_Code = AreaName[i].QuayCode();
                        var Port_Code = AreaName[i].PortCode();
                        if (model.BerthKey() == Port_Code+ "." + Quay_Code + "." + Berth_code) {
                            var B_Name = Berth_name;
                            var Q_code = Quay_Code;
                            var B_code = Berth_code;
                            self.dredgingpriorityModel().AreaName(B_Name);
                            self.dredgingpriorityModel().QuayCode(Q_code);
                            self.dredgingpriorityModel().BerthCode(B_code);
                        }
                    }
                    self.dredgingpriorityModel().AreaLocationID('');
                }
            }
            else if (model.AreaType() == "0") {
                $('#spn1AreaType').text('* Please select the AreaType');
                errors++;

            }

            //-----------------------------------------
            PriorityArea.DredgingMaterial(self.dredgingpriorityModel().DredgingMaterial());
            PriorityArea.AreaName(self.dredgingpriorityModel().AreaName());
            PriorityArea.RequireDate(moment(model.RequiredDate()).format('YYYY-MM-DD'));
            PriorityArea.AreaLocationID(self.dredgingpriorityModel().AreaLocationID());
            PriorityArea.QuayCode(self.dredgingpriorityModel().QuayCode());
            PriorityArea.BerthCode(self.dredgingpriorityModel().BerthCode());
            if (errors == 0) {
                var duplicate = false;
                self.IsModified(true);
                if (self.IsUpdate()) {
                    $.each(self.DredgingPriorityAreaList(), function (index, location) {
                        if (!((location.DredgingOperationID()) == (model.DredgingOperationID()))) {

                            debugger;
                            if ((location.DredgingPriorityID()) == (model.DredgingPriorityID())) {
                                debugger;
                                if ((location.RequiredDate()) == (moment(model.RequiredDate()).format('YYYY-MM-DD'))) {
                                    if ((location.BerthCode()) == (model.BerthCode()) && (location.BerthCode()) != "" && (model.BerthCode()) != "" && (location.TypeCode()) == (model.TypeCode())) {
                                        self.UniqueNameVisible(true);
                                        duplicate = true;
                                        self.IsModified(false);
                                    }
                                    if ((location.AreaLocationID()) == (model.AreaLocationID()) && (model.AreaLocationID() != "") && (location.AreaLocationID()) != "" && (location.TypeCode()) == (model.TypeCode())) {
                                        self.UniqueNameVisible(true);
                                        duplicate = true;
                                        self.IsModified(false);
                                    }
                                }
                            }
                        }
                        return;
                    });
                }
                if (!duplicate) {
                    self.IsModified(true);
                }
                if (self.IsModified() == true) {
                    model.DredgingOperationsVO.push(PriorityArea);
                    $("#btnCancel").hide();
                    self.viewModelHelper.apiPost('api/DredgingPriority', ko.mapping.toJSON(model), function Message(data) {
                        toastr.options.closeButton = true;
                        toastr.options.positionClass = "toast-top-right";
                        toastr.success("Dredging Priority Updated Successfully", "Dredging Priority");
                        self.dredgingpriorityModel().DredgingPriorityID(data.DredgingPriorityID);
                        self.LoadDredgingPriorityArea(data.DredgingPriorityID);
                        $('#divValidationError').addClass('display-none');
                        $('#stack2').modal('hide')
                        $('#spnTitile').html("Dredging Priority");
                        self.viewMode('Form');
                        $("#btnCancel").show();

                    });
                }

            } else {
               // PriorityArea.validationEnabled(true);
              //  self.DredgingPriorityValidation = ko.observable(model);
              //  self.DredgingPriorityValidation().errors = ko.validation.group(self.DredgingPriorityValidation());
                self.DredgingPriorityValidation().errors.showAllMessages();
                toastr.options.positionClass = "toast-top-right";
                toastr.warning("Please enter mandatory fields", "Dredging Priority");
                $('#divValidationError').removeClass('display-none');
                $("#btnCancel").show();

                return;

            }
        }


        self.editDredgingArea = function (dredgingpriority) {
            debugger;
            self.LoadDredgingPriorityArea(dredgingpriority.DredgingPriorityID());
            // var DredgingPriorityID = dredgingpriority.DredgingPriorityID();

            //  self.viewModelHelper.apiGet('api/DredgingPriorityArea/' + DredgingPriorityID, {},
            //// null,
            //    function (result) {                 
            //        self.DredgingPriorityAreaList(ko.utils.arrayMap(result, function (item) {                   
            //            return new IPMSRoot.DredgingPriorityModel(item);
            //        }));


            //    });
            self.Documents(self.dredgingpriorityModel().DredgingPriorityDocumentsVO());
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);
            self.isGridsEnable(true);
            self.IsCancel(true);
            self.isGridsEnable(true);
            self.isDelUploadFileVisible(true);
            self.IsAreaEdit(false);
            if (self.UniqueNameVisible() == true) {
                self.UniqueNameVisible(false);
            }
            self.dredgingPriorityAreaModel(dredgingpriority);
            //  self.dredgingpriorityModel().FinancialYearID(self.YearID());
            self.dredgingpriorityModel().DredgingPriorityDocumentsVO(self.Documents());
            self.IsCodeEnable(false);
            if ($("#AreaType").val() == 'B') {
                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#spanvelBerth').css('display', 'inline');
                $('#spanvelLocation').css('display', 'none');
            }
            else if ($("#AreaType").val() == 'L') {
                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#spanvelBerth').css('display', 'none');
                $('#spanvelLocation').css('display', 'inline');
            }
            var dropdownlist = $("#Month").data("kendoDropDownList");
            dropdownlist.enable(false);
            $('#spnTitile1').html("Update Dredging Priority");
            debugger;
            Requireddate();
            $('#stack2').modal('show')
        }
        self.viewDredgingArea = function (dredgingpriority) {
            self.Documents(self.dredgingpriorityModel().DredgingPriorityDocumentsVO());
            self.IsCancel(true);
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.IsCodeEnable(false);
            self.editableView(false);
            self.isGridsEnable(false);
            Requireddate();
            self.IsAreaEdit(false);
            self.dredgingPriorityAreaModel(dredgingpriority);
            //   self.dredgingpriorityModel().FinancialYearID(self.YearID());
            self.dredgingpriorityModel().DredgingPriorityDocumentsVO(self.Documents());
            self.IsCodeEnable(false);
            if ($("#AreaType").val() == 'B') {
                var dropdownlist = $("#Location").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#spanvelBerth').css('display', 'inline');
                $('#spanvelLocation').css('display', 'none');
            }
            else if ($("#AreaType").val() == 'L') {
                var dropdownlist = $("#Berth").data("kendoDropDownList");
                dropdownlist.enable(false);
                $('#spanvelBerth').css('display', 'none');
                $('#spanvelLocation').css('display', 'inline');
            }
            var dropdownlist = $("#Month").data("kendoDropDownList");
            dropdownlist.enable(false);
            $('#RequiredDate').data('kendoDatePicker').enable(false);
            $('#stack2').modal('show')
            $('#spnTitile1').html("View Dredging Priority");

        }

        //This is used to Validate numeric
        ValidateNumeric = function () {
            return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
        };

        self.Initialize();

        var uploadedFiles = [];
        var documentData = [];

        self.uploadFile = function () {
            $("#spanHWPSfileToUpload").text("");

            self.isUploadFileVisible(false);
            var documentType = $('#selUploadDocs option:selected').text();
            self.dredgingpriorityModel().UploadedFiles([]);
            uploadedFiles = self.dredgingpriorityModel().UploadedFiles();
            var opmlFile = $('#fileToUpload')[0];
            if (opmlFile.files.length > 0) {
                for (var i = 0; i < opmlFile.files.length; i++) {
                    var match = ko.utils.arrayFirst(self.dredgingpriorityModel().DredgingPriorityDocumentsVO(), function (item) {
                        return item.FileName() === opmlFile.files[i].name;
                    });
                    if (match == null) {
                        var fileSizeInBytes = opmlFile.files[i].size;
                        var filesizeinMB = (fileSizeInBytes / (1024 * 1024)).toFixed(2);
                        if (parseFloat(filesizeinMB) <= parseFloat(self.fileSizeConfigValue())) {
                            var elem = {};
                            elem.FileName = opmlFile.files[i].name;
                            elem.FileSize = opmlFile.files[i].size;
                            var extension = opmlFile.files[i].name.split('.').pop().toLowerCase();
                            var fileExtension = ['pdf', 'png', 'docx', 'doc', 'xlsx'];
                            if ($.inArray(extension, fileExtension) != -1) {
                                elem.CategoryName = $('#selUploadDocs option:selected').text();
                                elem.CategoryCode = $('#selUploadDocs option:selected').val();
                                elem.FileDetails = opmlFile.files[i];
                                elem.IsAlreadyExists = false
                                uploadedFiles.push(elem);
                                self.dredgingpriorityModel().UploadedFiles(uploadedFiles);
                            }
                            else {
                                toastr.error("Please upload the files with formats (PDF, word, excel, .PNG) only", "Error");
                                return;
                            }
                        }
                        else {
                            toastr.error("The " + opmlFile.files[i].name + " file size is exceeded the maximum set limit " + self.fileSizeConfigValue() + "MB", "Error");
                            return;
                        }
                    }
                    else {
                        toastr.error("The selected file already exist, Please upload another file", "Error");
                        return;
                    }

                }

                var formData = new FormData();
                $.each(uploadedFiles, function (key, val) {
                    formData.append(val.name, val.FileDetails);
                });
                var CategoryName = $('#selUploadDocs option:selected').text();
                var CategoryCode = $('#selUploadDocs option:selected').val();
                self.viewModelHelper.apiUpload('api/File/MultipleFileUpload?documentType=Doc1', formData, function Message(data) {
                    self.Listdocuments = ko.observableArray();
                    self.Listdocuments(ko.utils.arrayMap(data, function (item) {
                        var Adddoc = new IPMSROOT.dredgingdocument();
                        Adddoc.DocumentID(item.DocumentID);
                        Adddoc.FileName(item.FileName);
                        Adddoc.DocumentName(documentType);
                        self.dredgingpriorityModel().DredgingPriorityDocumentsVO.push(Adddoc);

                    }));


                });
            } else {
                $("#spanHWPSfileToUpload").text('Please select file');
                self.isUploadFileVisible(true);

            }
            self.dredgingpriorityModel().UploadedFiles([]);
            $('#fileToUpload').val('');
            return;
            // }
        }

        self.BerthSelect = function (e) {
            var dataItem = this.dataItem(e.item.index());
            self.dredgingPriorityAreaModel().PortCode(dataItem.PortCode);
            self.dredgingPriorityAreaModel().QuayCode(dataItem.QuayCode);
        }

        self.DeleteDocument = function (documentRow) {
            self.dredgingpriorityModel().DredgingPriorityDocumentsVO.remove(documentRow);
        }
        isAdd = 0;
        index = 1;

        self.viewWorkFlow = function (dredging) {
            debugger;
            var workflowinstanceId = dredging.DPAWorkflowInstanceID();
            if (workflowinstanceId == "" || workflowinstanceId == null) {
                $('#WorkFlowRemarks').text('No Remarks');
                $('#stack4').modal('show');
            }
            else {
                self.viewModelHelper.apiGet('api/WorkFlowRemarks/' + workflowinstanceId,
                 { workflowinstanceId: workflowinstanceId },
                  function (result) {
                    //  self.dredgingpriorityModel(new IPMSROOT.DredgingPriorityModel());
                      self.dredgingpriorityModel().WorkFlowRemarks(result);
                      $('#WorkFlowRemarks').text(result);
                      $('#stack4').modal('show');
                  });
            }
        }



    }
    IPMSRoot.DredgingPriorityViewModel = DredgingPriorityViewModel;


}(window.IPMSROOT));
//Validate Only Numberic
//Validatenumeric = function () {
//    return self.validationHelper.ValidateNumeric_keypressEvent(this, event);
//}

function Validatenumeric1(el, evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}
//thanks: http://javascript.nwbox.com/cursor_position/
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}