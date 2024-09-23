(function (IPMSRoot) {
    var ElectronicNotificationsViewModel = function () {
        var self = this;
        $('#spnTitle').html("Electronic Notifications");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.electronicNotificationsModel = ko.observable(new IPMSROOT.ElectronicNotificationsModel());
        self.electronicNotificationsList = ko.observableArray();
        self.entityValues = ko.observableArray([]);
        self.tokenValues = ko.observableArray([]);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(false);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.IsClicks = ko.observable(false);
        self.Roles = ko.observableArray();
        self.Ports = ko.observableArray();
        self.WFStatus = ko.observableArray();
        self.isEntityChanged = ko.observable(true);

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
            self.LoadNotifications();
            self.LoadEntities();
            self.LoadRoles();
            self.LoadPorts();
            self.LoadWFStatus();
            self.viewMode('List');
        }


        self.LoadNotifications = function () {
            self.viewModelHelper.apiGet('api/ElectronicNotifications',
            null,
              function (result) {
                  self.electronicNotificationsList(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.ElectronicNotificationsModel(item);
                  }));
              });

        }
        //self.LoadNotifications = function () {
        //    self.viewModelHelper.apiGet('api/ElectronicNotifications',
        //    null,
        //      function (result) {
        //          self.electronicNotificationsList(ko.utils.arrayMap(result, function (item) {
        //              return new IPMSRoot.ElectronicNotificationsModel(item);
        //          }));
        //      });

        //}

        self.ModifyNotification = function (model) {

            var notificationRoles = [];
            $.each(self.electronicNotificationsModel().SelectedRolenames(), function (index, value) {
                notificationRoles.push(new NotificationRole(0, model.NotificationTemplateCode(), value, 0, 0));
                model.NotificationRoles(notificationRoles);
            });

            var notificationPorts = [];
            $.each(self.electronicNotificationsModel().SelectedPortNames(), function (index, value) {
                notificationPorts.push(new NotificationPort(value, 0, 0, 0, 0));
                model.NotificationPorts(notificationPorts);
            });

            self.viewModelHelper.apiPut('api/ElectronicNotifications', ko.mapping.toJSON(model), function Message(data) {
               
                toastr.options.closeButton = true;
                toastr.options.positionClass = "toast-top-right";
                toastr.success("Electronic Notifications details updated successfully", "Electronic Notifications");
                self.LoadNotifications();
                self.viewMode('List');
            });
        }

        self.viewElectronicNotification = function (value) {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);

            //self.electronicNotificationsModel().SelectedRolenames.removeAll();
            //$.each(value.NotificationRole(), function (key, value1) {
            //    self.electronicNotificationsModel().SelectedRolenames.push(value1.RoleID);
            //});

            //self.electronicNotificationsModel().SelectedPortNames.removeAll();
            //$.each(value.NotificationPort(), function (key, value2) {
            //    self.electronicNotificationsModel().SelectedPortNames.push(value2.PortCode);
            //});

            self.electronicNotificationsModel(value);
            if (JSON.parse(ko.toJSON(value.IsEmail)) == "N") {
                $('#emailcheck').removeAttr('checked');
                $('#emaildisplay').css('display', 'none');
            }
            if (JSON.parse(ko.toJSON(value.IsEmail)) == "Y") {
                $('#emailcheck').attr('checked', 'checked');
                $('#emaildisplay').css('display', 'block');
            }
            if (JSON.parse(ko.toJSON(value.IsSMS)) == "N") {
                $('#smscheck').removeAttr('checked');
                $('#smsdisplay').css('display', 'none');
            }
            if (JSON.parse(ko.toJSON(value.IsSMS)) == "Y") {
                $('#smscheck').attr('checked', 'checked');
                $('#smsdisplay').css('display', 'block');
            }
            if (JSON.parse(ko.toJSON(value.IsSysMessage)) == "N") {
                $('#sncheck').removeAttr('checked');
                $('#sndisplay').css('display', 'none');
            }
            if (JSON.parse(ko.toJSON(value.IsSysMessage)) == "Y") {
                $('#sncheck').attr('checked', 'checked');
                $('#sndisplay').css('display', 'block');
            }
            if (value.NotificationTemplateBase() == "U") {
                $('#roledisplay').css('display', 'none');
            }
            if (value.NotificationTemplateBase() == "R") {
                $('#roledisplay').css('display', 'block');
            }

            $('#spnTitle').html("View Electronic Notifications");
        }

        self.editElectronicNotification = function (value) {
            self.LoadTokens(value);
            $('#spnTitle').html("Update Electronic Notifications Details");
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.IsClicks(true);
            self.editableView(true);
            self.IsCodeEnable(false);
            //self.electronicNotificationsModel().SelectedRolenames.removeAll();
            //$.each(value.NotificationRole(), function (key, value) {
            //    self.electronicNotificationsModel().SelectedRolenames.push(value.RoleID);
            //});
            //self.electronicNotificationsModel().SelectedPortNames.removeAll();
            //$.each(value.NotificationPort(), function (key, value) {
            //    self.electronicNotificationsModel().SelectedPortNames.push(value.PortCode);
            //});
            self.electronicNotificationsModel(value);
            if (JSON.parse(ko.toJSON(value.IsEmail)) == "N") {
                $('#emailcheck').removeAttr('checked');
                $('#emaildisplay').css('display', 'none');
            }
            else {
                $('#emailcheck').attr('checked', 'checked');
                $('#emaildisplay').css('display', 'block');

            }
            if (JSON.parse(ko.toJSON(value.IsSMS)) == "N") {
                $('#smscheck').removeAttr('checked');
                $('#smsdisplay').css('display', 'none');
            }
            else {
                $('#smscheck').attr('checked', 'checked');
                $('#smsdisplay').css('display', 'block');
            }
            if (JSON.parse(ko.toJSON(value.IsSysMessage)) == "N") {
                $('#sncheck').removeAttr('checked');
                $('#sndisplay').css('display', 'none');
            }
            else {
                $('#sncheck').attr('checked', 'checked');
                $('#sndisplay').css('display', 'block');
            }
            if (value.NotificationTemplateBase() == "U") {
                $('#roledisplay').css('display', 'none');
            }
            if (value.NotificationTemplateBase() == "R") {
                $('#roledisplay').css('display', 'block');
            }
        }

        self.addElectronicNotification = function () {
            self.viewMode('Form');
            $('#spnTitle').html("Add Electronic Notifications");
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.IsClicks(false);
            self.editableView(true);
            self.IsCodeEnable(true);
            self.electronicNotificationsModel().SelectedRolenames.removeAll();
            self.electronicNotificationsModel().SelectedPortNames.removeAll();
            self.electronicNotificationsModel(new IPMSRoot.ElectronicNotificationsModel());
        }

        self.ResetNotification = function (model) {
            self.electronicNotificationsModel().reset();

            if (JSON.parse(ko.toJSON(model.IsEmail)) == "N") {
                $('#emailcheck').removeAttr('checked');
                $('#emaildisplay').css('display', 'none');
            }
            if (JSON.parse(ko.toJSON(model.IsEmail)) == "Y") {
                $('#emailcheck').attr('checked', 'checked');
                $('#emaildisplay').css('display', 'block');
            }
            if (JSON.parse(ko.toJSON(model.IsSMS)) == "N") {
                $('#smscheck').removeAttr('checked');
                $('#smsdisplay').css('display', 'none');
            }
            if (JSON.parse(ko.toJSON(model.IsSMS)) == "Y") {
                $('#smscheck').attr('checked', 'checked');
                $('#smsdisplay').css('display', 'block');
            }
            if (JSON.parse(ko.toJSON(model.IsSysMessage)) == "N") {
                $('#sncheck').removeAttr('checked');
                $('#sndisplay').css('display', 'none');
            }
            if (JSON.parse(ko.toJSON(model.IsSysMessage)) == "Y") {
                $('#sncheck').attr('checked', 'checked');
                $('#sndisplay').css('display', 'block');

            }
        }

        self.LoadEntities = function () {
            self.entityValues("");
            self.viewModelHelper.apiGet('api/GetEntities',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.entityValues);
              });
          
        }

        self.LoadTokens = function (value) {
            //if (value.entityid() != undefined)
                $('#spanventity').text('');           
            if (value.EntityID() == undefined) {
                self.isEntityChanged(false);
                self.tokenValues.removeAll();
            }
            else 
           {
                self.isEntityChanged(true);
            }

            self.viewModelHelper.apiGet('api/GetTokens',
          { entityID: value.EntityID },
            function (result) {
                ko.mapping.fromJS(result, {}, self.tokenValues);
            });
           
        }




        self.AddTokenToBodyEmail = function (model) {
            model.EmailTemplate(model.EmailTemplate() + " %" + model.SelectedTokensEmail() + "%");
        }

        self.AddTokenToBodySMS = function (model) {
            model.SMSTemplate(model.SMSTemplate() + " %" + model.SelectedTokensSMS() + "%");
        }

        self.AddTokenToBodySystemNotification = function (model) {
            model.SysMessageTemplate(model.SysMessageTemplate() + " %" + model.SelectedTokensSysMessage() + "%");
        }

        self.LoadRoles = function () {
            self.viewModelHelper.apiGet('api/GetRoles',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.Roles);
              });
        }

        self.LoadPorts = function () {
            self.viewModelHelper.apiGet('api/GetPorts',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.Ports);
              });
        }

        self.LoadWFStatus = function () {
            self.viewModelHelper.apiGet('api/GetWorkflowStatus',
            null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.WFStatus);
              });
        }

        self.cancel = function () {
            $('#spnTitle').html("Electronic Notifications");
            self.viewMode('List');
            self.electronicNotificationsModel().reset();
        }



        self.dataLoad = function (event) {
            if (event.RoleID() != undefined)
                $('#spanvroleid').text('');
        }

        self.loadPorts = function (event) {
            if (event.PortCode() != undefined)
                $('#spanvportid').text('');
        }

        self.SaveNotification = function (model) {

            self.ENValidation = ko.observable(model);
            self.ENValidation().errors = ko.validation.group(self.ENValidation());
            var errors = self.ENValidation().errors().length;
            if (errors == 0) {
                if (ko.toJSON(model.EntityID) == undefined || ko.toJSON(model.RoleID) == undefined) {
                    if (ko.toJSON(model.EntityID) == undefined)
                        $('#spanventity').text('Please select Entity Name');
                    else if (ko.toJSON(model.RoleID) == undefined)
                        $('#spanvroleid').text('Please select Role');
                }

                else {
                    var notificationRoles = [];
                    $.each(self.electronicNotificationsModel().SelectedRolenames(), function (index, value) {
                        notificationRoles.push(new NotificationRole(0, 0, value, 0, 0));
                        model.NotificationRoles(notificationRoles);
                    });

                    var notificationPorts = [];
                    $.each(self.electronicNotificationsModel().SelectedPortNames(), function (index, value) {
                        notificationPorts.push(new NotificationPort(value, 0, 0, 0, 0));
                        model.NotificationPorts(notificationPorts);
                    });

                    self.viewModelHelper.apiPost('api/ElectronicNotifications', ko.mapping.toJSON(model),
                                function Message(data) {
                                    toastr.options.closeButton = true;
                                    toastr.options.positionClass = "toast-top-right";
                                    toastr.success("Electronic Notifications details saved successfully", "Electronic Notifications");
                                    self.LoadNotifications();
                                    self.viewMode('List');
                                });
                }
            }
            else {
                self.ENValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }
        }

        self.DeleteNotification = function (model) {
            self.viewModelHelper.apiDelete('api/ElectronicNotifications', ko.mapping.toJSON(model), function Message(data) {
                self.LoadNotifications();
            });
        }

        //ChangeEvent = function (data, event) {
        //    if (data.NotificationTemplateCode().length == 0 || data.NotificationTemplateName().length == 0) {
        //        self.IsClicks(false);
        //    }
        //    else {
        //        self.IsClicks(true);
        //    }
        //}

        ValidEvent = function (data, event) {
            var databaseList = ko.toJSON(self.electronicNotificationsList);
            var jsonObj = JSON.parse(databaseList);
            var formList = JSON.parse(ko.toJSON(data));
            var flag = true;
            $.each(jsonObj, function (index, value) {
                if (formList.NotificationTemplateCode.length > 4) {
                    $('#spanvNotificationId').text('Notification Code Length should be less than 5');
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
    IPMSRoot.ElectronicNotificationsViewModel = ElectronicNotificationsViewModel;

}(window.IPMSROOT));

//Reusable Notification Objects
function NotificationTemplate(NotificationTemplateCode, RecordStatus, CreatedBy) {
    this.NotificationTemplateCode = NotificationTemplateCode;
    this.RecordStatus = RecordStatus;
    this.CreatedBy = CreatedBy;
}
function NotificationRole(NotificationRoleID, NotificationTemplateCode, RoleID, RecordStatus, CreatedBy) {
    this.NotificationRoleID = NotificationRoleID;
    this.NotificationTemplateCode = NotificationTemplateCode;
    this.RoleID = RoleID;
    this.RecordStatus = RecordStatus;
    this.CreatedBy = CreatedBy;
}
function NotificationPort(PortCode, NotificationTemplateCode, RecordStatus, CreatedBy) {
    this.PortCode = PortCode;
    this.NotificationTemplateCode = NotificationTemplateCode;
    this.RecordStatus = RecordStatus;
    this.CreatedBy = CreatedBy;
}












