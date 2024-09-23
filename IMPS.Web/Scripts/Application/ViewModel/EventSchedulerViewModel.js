(function (IPMSRoot) {
    var EventSchedulerViewModel = function () {
        var self = this;
        $('#spnTitle').html("Event Schedulers List");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.eventSchedulerModel = ko.observable(new IPMSROOT.EventSchedulerModel());
        self.eventSchedulerList = ko.observableArray();
        self.entityValues = ko.observableArray();     
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.IsClicks = ko.observable(false);      

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadSchedulers();
            self.LoadEntities();       
            self.viewMode('List');
        }

        self.LoadSchedulers = function () {
            self.viewModelHelper.apiGet('api/EventSchedulers',
            null,
              function (result) {
                  self.eventSchedulerList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.EventSchedulerModel(item);
                  }));
              });

        }



        self.viewScheduler = function (model) {

            if (model.EventScheduleType() == "W") {
                if (model.ExecutionPlan() != null) {
                    var weeklyitem = model.ExecutionPlan().split(';');
                    ko.utils.arrayForEach(weeklyitem, function (val) {                       
                        model.SelectedWeeklyItem.push(val);
                    });
                }
                
            }
            if (model.EventScheduleType() == "M") {

                var mday = model.ExecutionPlan().replace('DAY;', '');;
                model.Day(mday);
            }

            if (model.EventScheduleType() == "I") {
                if (model.EventScheduleTime() != null) {
                    var interval = model.EventScheduleTime().split(':');
                     model.Hour(interval[0]);
                     model.Minute(interval[1]);
                    // model.ExecutionPlan(interval);
                }
            }

            if (model.EventScheduleType() == "C") {

                model.Coustom(model.ExecutionPlan());
            }

            self.eventSchedulerModel(model);
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
          

            $('#spnTitle').html("View Event Schedulers");
        }

        self.editScheduler = function (model) {

            if (model.EventScheduleType() == "W") {
                if (model.ExecutionPlan() != null) {
                    var weeklyitem = model.ExecutionPlan().split(';');
                    ko.utils.arrayForEach(weeklyitem, function (val) {                       
                        model.SelectedWeeklyItem.push(val);
                    });
                }

            }
            if (model.EventScheduleType() == "M") {

                var mday = model.ExecutionPlan().replace('DAY;', '');;
                model.Day(mday);
            }

            if (model.EventScheduleType() == "I") {
                if (model.EventScheduleTime() != null) {
                    var interval = model.EventScheduleTime().split(':');
                    model.Hour(interval[0]);
                    model.Minute(interval[1]);
                    // model.ExecutionPlan(interval);
                }
            }
           

            if (model.EventScheduleType() == "C") {

                model.Coustom(model.ExecutionPlan());
            }
            self.eventSchedulerModel(model);
            $('#spnTitle').html("Update Event Scheduler Details");
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.IsClicks(true);
            self.editableView(true);
            self.IsCodeEnable(false);
           
        }

        self.addScheduler = function () {
            self.viewMode('Form');
            $('#spnTitle').html("Add Event Scheduler");
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.IsClicks(false);
            self.editableView(true);
            self.IsCodeEnable(true);          
            self.eventSchedulerModel(new IPMSRoot.EventSchedulerModel());
        }

        self.ResetScheduler = function (model) {
            self.eventSchedulerModel().reset();

           
        }

        self.LoadEntities = function () {
            self.viewModelHelper.apiGet('api/GetEntities',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.entityValues);
              });
        }

      
        self.cancel = function () {
            $('#spnTitle').html("Event Schedulers List");
            self.viewMode('List');
            self.eventSchedulerModel().reset();
        }



     
        self.SaveScheduler = function (model) {

            if (model.EventScheduleType() == "W") {
                var result = true;
                var weeklyitem = null;
                ko.utils.arrayForEach(model.SelectedWeeklyItem(), function (val) {
                    if (weeklyitem != null)
                        weeklyitem = weeklyitem + ";" + val;
                    else
                        weeklyitem = val;                  
                });
                if (weeklyitem == null) {
                    toastr.error("Please select at least one day.", "Error");
                    result = false;
                }
                else                    
                  model.ExecutionPlan(weeklyitem);                
            }

            if (model.EventScheduleType() == "M") {
                var mday = "DAY;" + model.Day();
                model.ExecutionPlan(mday);
            }

            if (model.EventScheduleType() == "I") {
                var interval= model.Hour() +":"+ model.Minute();
                model.EventScheduleTime(interval);
            }
            if (model.EventScheduleType() == "S") {
                model.EventScheduleStartDate($('#sdate').val());
            }
            if (model.EventScheduleType() == "C") {
                var coustom = model.Coustom();
                model.ExecutionPlan(coustom);
            }
          

            self.ENValidation = ko.observable(model);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (errors == 0) {
               
                self.viewModelHelper.apiPost('api/EventSchedulers', ko.mapping.toJSON(model),
                                function Message(data) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Event Schedular details saved successfully", "EventSchedular");
                                    self.LoadSchedulers();
                                    self.viewMode('List');
                                });            
            }
            else {
                self.ENValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.ModifyScheduler = function (model) {

            if (model.EventScheduleType() == "W") {
                var result = true;
                var weeklyitem = null;
                ko.utils.arrayForEach(model.SelectedWeeklyItem(), function (val) {
                    if (weeklyitem != null)
                        weeklyitem = weeklyitem + ";" + val;
                    else
                        weeklyitem = val;
                });
                if (weeklyitem == null) {
                    toastr.error("Please select at least one day.", "Error");
                    result = false;
                }
                else
                    model.ExecutionPlan(weeklyitem);
            }

            if (model.EventScheduleType() == "M") {
                var mday = "DAY;" + model.Day();
                model.ExecutionPlan(mday);
            }

            if (model.EventScheduleType() == "I") {
                var interval = model.Hour() + ":" + model.Minute();
                model.EventScheduleTime(interval);
            }
            if (model.EventScheduleType() == "S") {
                model.EventScheduleStartDate($('#sdate').val());
            }
            if (model.EventScheduleType() == "C") {
                var coustom = model.Coustom();
                model.ExecutionPlan(coustom);
            }


            self.ENValidation = ko.observable(model);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (errors == 0) {

                self.viewModelHelper.apiPut('api/EventSchedulers', ko.mapping.toJSON(model),
                                function Message(data) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Event Schedular details updated successfully", "EventSchedular");
                                    self.LoadSchedulers();
                                    self.viewMode('List');
                                });
            }
            else {
                self.ENValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }

        }

        self.DeleteScheduler = function (model) {
            //self.eventSchedulerModel(model);
            self.viewModelHelper.apiDelete('api/EventSchedulers', ko.mapping.toJSON(model), function Message(data) {
                self.LoadSchedulers();
            });
        }

       

        ValidEvent = function (data, event) {
            var databaseList = ko.toJSON(self.eventSchedulerList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;
            $.each(jsonObj, function (index, value) {
                if (formList.NotificationTemplateCode.length > 4) {
                    $('#spanvNotificationId').text('Notification code length should be less than 5 characters.');
                    flag = false;
                }
                else if (value.NotificationTemplateCode == formList.NotificationTemplateCode) {
                    $('#spanvNotificationId').text('Already exists.! Please enter another code.');
                    flag = false;

                }
                return;

            });

            if (flag == true) {
                $('#spanvNotificationId').text('');
            }

        }
        self.Initialize();
    }
    IPMSRoot.EventSchedulerViewModel = EventSchedulerViewModel;

}(window.IPMSROOT));













