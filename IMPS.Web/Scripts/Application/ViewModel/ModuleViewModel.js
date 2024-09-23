(function (IPMSRoot) {
    var ModuleViewModel = function () {
        var self = this;
        self.Moduledata = ko.observableArray();
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.automatedSlottingPrivileges = ko.observable();
        self.automatedResourceSchedulingPrivileges = ko.observable();
        self.moduleModel = ko.observable();
        self.showshipsinport= ko.observable(false);
        self.Initialize = function () {
            self.showshipsinport(false);
            self.moduleModel(new IPMSROOT.ModuleModel());
            self.LoadModules();
            self.LoadAutomatedSlottingPrivileges();
            self.LoadAutomatedResourceAllocationPrivileges();
            self.UserRoleID();
        }

        //ships in ports only for admin
        self.UserRoleID = function () {
            
            self.viewModelHelper.apiGet('api/Module/GetUserRoles', null,
            function (result) {
                $.each(result, function (key, val) {
                    if (val.RoleID == 1) {
                        self.showshipsinport(true);
                    }
                  
                });

            }, null, null, false);
        }


       
        self.LoadAutomatedSlottingPrivileges = function () {
            self.viewModelHelper.apiGet('api/GetRolePrivileges/AutomatedSlotting', null,
            function (result) {
                ko.mapping.fromJS(result, {}, self.automatedSlottingPrivileges);                

            }, null, null, false);
        }

        self.LoadAutomatedResourceAllocationPrivileges = function () {
            self.viewModelHelper.apiGet('api/GetRolePrivileges/AutomatedResourceScheduling', null,
            function (result) {
                ko.mapping.fromJS(result, {}, self.automatedResourceSchedulingPrivileges);
                
            }, null, null, false);
        }

        // Loads the module data
        self.LoadModules = function () {
            // var uname = $("#username").text();
            var uname = $("#loginusername").text();
            self.viewModelHelper.apiGet('api/Account/GetUserModules/' + uname,
            null,
              function (result) {

                  $.each(result, function (key, val) {
                      $.each(val.Module1, function (key1, val1) {
                          $.each(val1.Entities, function (key2, val2) {
                              val2.PageUrl = '/' + val2.PageUrl;
                          });
                      });
                  });
                  ko.mapping.fromJS(result, {}, self.Moduledata);

                  var module0 = jQuery('#ModuleName0');
                  module0.children('a').children('i').addClass('fa fa-book');

                  var module1 = jQuery('#ModuleName1');
                  module1.children('a').children('i').addClass('fa fa-cogs');

                  var module2 = jQuery('#ModuleName2');
                  module2.children('a').children('i').addClass('fa fa-asterisk');

                  var module3 = jQuery('#ModuleName3');
                  module3.children('a').children('i').addClass('fa fa-building');

                  var dashboard = jQuery('#dashboard');

                  var strHash = document.location.pathname;

                  if (strHash == "/Welcome") {
                      dashboard.addClass('active');
                  }
                  else {

                      dashboard.removeClass('active');
                      jQuery("a[href='" + strHash + "']").parent().addClass("active");
                      jQuery("a[href='" + strHash + "']").parent().parent().parent().addClass("active");
                      jQuery("a[href='" + strHash + "']").parent().parent().parent().children('a').children('span.arrow').addClass('open');
                      jQuery("a[href='" + strHash + "']").parent().parent().parent().parent().parent().addClass("active");
                      jQuery("a[href='" + strHash + "']").parent().parent().parent().parent().parent().children('a').children('span.arrow').addClass("open");
                  }
              });

        }


        self.Initialize();
    }
    IPMSRoot.ModuleViewModel = ModuleViewModel;

}(window.IPMSROOT));

