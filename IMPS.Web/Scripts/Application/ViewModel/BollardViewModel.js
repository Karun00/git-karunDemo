(function (IPMSRoot) {
    var BollardViewModel = function () {
        var self = this;
        $('#spnTitle').html("Bollard");
        self.viewModelHelper = new IPMSROOT.viewModelHelper();
        self.validationHelper = new IPMSRoot.validationHelper();
        self.viewMode = ko.observable();
        self.editableView = ko.observable(true);
        self.bollardModel = ko.observable(new IPMSROOT.BollardModel()); //new IPMSROOT.PortModel();
        self.bollardList = ko.observableArray();
        self.bollardsInBerth = ko.observableArray();
        self.portValues = ko.observableArray();
        self.quayValues = ko.observableArray();
        self.berthValues = ko.observableArray();   
        self.isPortChanged = ko.observable(true);
        self.isQuayChanged = ko.observable(true);
        self.IsUpdate = ko.observable(false);
        self.IsSave = ko.observable(true);
        self.IsReset = ko.observable(true);
        self.IsCodeEnable = ko.observable(true);
        self.recordstatusvalues = ko.observableArray([{ name: 'Active', val: 'A' }, { name: 'Inactive', val: 'I' }]);
        self.IsValid = ko.observable(true);
        self.IsUnique = ko.observable(true);
        self.UniqueBollardCodeVisible = ko.observable(false);
        self.UniqueBollardNameVisible = ko.observable(false);
        self.UniqueBollardShortNameVisible = ko.observable(false);
        self.IsFromMeterVisible = ko.observable(false);
        self.IsToMeterVisible = ko.observable(false);

        self.Initialize = function () {
            self.viewMode = ko.observable(true);
           // self.bollardModel(new IPMSROOT.BollardModel());
            self.LoadBollards();
            self.LoadPorts();
            self.viewMode('List');
        }
        
        self.LoadBollards = function () {
            self.viewModelHelper.apiGet('api/Bollards',
            null,
               function (result) {
                   self.bollardList(ko.utils.arrayMap(result, function (item) {
                       return new IPMSRoot.BollardModel(item);
                   }));
               });
        }


        self.LoadPorts = function () {
            self.quayValues(""); self.berthValues("");
            self.viewModelHelper.apiGet('api/Ports/GetLoginPort',
            null,
              //function (result) {
              //    ko.mapping.fromJS(result, {}, self.portValues);             
              //});
              function (result) {
                  var ports = $.map(result, function (item) {
                      return new self.Port(item);
                  });
                  self.portValues(ports);
              });
           
        }
        self.Port = function (data) {
            this.PortCode = ko.observable(data.PortCode);
            this.PortName = ko.observable(data.PortCode + '-' + data.PortName);
        };
      
       


        self.LoadQuays = function (event) {
            self.bollardModel().QuayCode("");
            self.quayValues("");
            self.berthValues("");
            self.bollardsInBerth("");
            if (event.PortCode() == undefined) {
                self.quayValues({ QuayCode: 0, QuayName: '',QuayLength:0 });
                self.berthValues({ BerthCode: 0, BerthName: '' });
                self.isPortChanged(false);
            }
            else {
                self.isPortChanged(true);
                self.isQuayChanged(false);
              
               self.berthValues("");
                
            }
                        
           
            self.viewModelHelper.apiGet('api/BollardPortQuays/' + event.PortCode(),
              null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.quayValues);
             });
        }

        self.LoadBerths = function (event) {
           self.berthValues("");
            self.bollardModel().BerthCode("");
            if (event.QuayCode() == undefined) {
                self.berthValues({ BerthCode: 0, BerthName: '' });
                self.isQuayChanged(false);
            }
            else {
                self.isQuayChanged(true);
              
            }
            
            self.viewModelHelper.apiGet('api/BollardQuayBerths/' + event.PortCode() + '/' + event.QuayCode(),
               null,
              function (result) {
                  ko.mapping.fromJS(result, {}, self.berthValues);

              });
                 
        }

        self.LoadBollardsInBerth = function (event) {
            if (event.BerthCode() == undefined) {
                self.bollardsInBerth("");
            }
            else {
                self.viewModelHelper.apiGet('api/BollardInBerths/' + event.PortCode() + '/' + event.QuayCode() + '/' + event.BerthCode(),
              null,
              function (result) {
                  self.bollardsInBerth(ko.utils.arrayMap(result, function (item) {
                      return new IPMSRoot.BollardModel(item);
                  }));
              });
            }
        }
        HandleBollardCodeKeyUp = function (data, event) {
            if (self.UniqueBollardCodeVisible() == true) {
                self.UniqueBollardCodeVisible(false);
            }

            if (self.UniqueBollardNameVisible() == true) {
                self.UniqueBollardNameVisible(false);
            }
            if (self.UniqueBollardShortNameVisible() == true) {
                self.UniqueBollardShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }

        HandleBollardNameKeyUp = function (data, event) {
            if (self.UniqueBollardCodeVisible() == true) {
                self.UniqueBollardCodeVisible(false);
            }

            if (self.UniqueBollardNameVisible() == true) {
                self.UniqueBollardNameVisible(false);
            }
            if (self.UniqueBollardShortNameVisible() == true) {
                self.UniqueBollardShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }

        HandleBollardShortNameKeyUp = function (data, event) {
            if (self.UniqueBollardCodeVisible() == true) {
                self.UniqueBollardCodeVisible(false);
            }

            if (self.UniqueBollardNameVisible() == true) {
                self.UniqueBollardNameVisible(false);
            }
            if (self.UniqueBollardShortNameVisible() == true) {
                self.UniqueBollardShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }
        HandleBollardFromMeterKeyUp=function (data, event) {
            if (self.UniqueBollardCodeVisible() == true) {
                self.UniqueBollardCodeVisible(false);
            }

            if (self.UniqueBollardNameVisible() == true) {
                self.UniqueBollardNameVisible(false);
            }
            if (self.UniqueBollardShortNameVisible() == true) {
                self.UniqueBollardShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }
        HandleBollardToMeterKeyUp = function (data, event) {
            if (self.UniqueBollardCodeVisible() == true) {
                self.UniqueBollardCodeVisible(false);
            }

            if (self.UniqueBollardNameVisible() == true) {
                self.UniqueBollardNameVisible(false);
            }
            if (self.UniqueBollardShortNameVisible() == true) {
                self.UniqueBollardShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }
        self.SaveBollard = function (model) {
            debugger;
            self.BollardValidation = ko.observable(model);
            self.BollardValidation().errors = ko.validation.group(self.BollardValidation());
            var errors = self.BollardValidation().errors().length;
            self.IsUnique(true);
            if (errors == 0) {
                $.each(JSON.parse(ko.toJSON(self.bollardList())), function (index, value) {
                    if (value.PortCode == model.PortCode() && value.QuayCode == model.QuayCode() && value.BerthCode == model.BerthCode()) {

                        if ((value.BollardCode).toLowerCase() == (model.BollardCode()).toLowerCase()) {
                            self.IsUnique(false);
                            self.UniqueBollardCodeVisible(true);

                        }
                        if ((value.BollardName).toLowerCase() == (model.BollardName()).toLowerCase()) {

                            self.IsUnique(false);
                            self.UniqueBollardNameVisible(true);

                        }

                        // return;
                        // }
                        if ((value.ShortName).toLowerCase() == (model.ShortName()).toLowerCase()) {
                            self.IsUnique(false);
                            self.UniqueBollardShortNameVisible(true);

                        }
                    }
                    return;
                });

                var bollardSum = 0;
                var lengthInvalid = false;
                $.each(JSON.parse(ko.toJSON(self.bollardList())), function (index, value) {
                    if (value.PortCode == model.PortCode() && value.QuayCode == model.QuayCode() && value.BerthCode == model.BerthCode()) {
                        bollardSum = bollardSum + ((parseInt(value.ToMeter) - parseInt(value.FromMeter)));

                        if (parseInt(model.FromMeter()) < parseInt(value.ToMeter)) {
                            $('#spanfrommeterid').text('From Meter should be greater than or equal to previous Bollard To Meter');
                            lengthInvalid = true; self.IsFromMeterVisible(true); self.IsValid(false);
                        }
                        if (parseInt(model.ToMeter()) < parseInt(value.ToMeter)) {
                            $('#spantometerid').text('To Meter should be greater than or equal to previous Bollard To Meter');
                            lengthInvalid = true; self.IsToMeterVisible(true); self.IsValid(false);
                        }
                    }


                });
                var berthLength = 0;
                $.each(JSON.parse(ko.toJSON(self.berthValues())), function (index, value) {
                    if (value.PortCode == model.PortCode() && value.QuayCode == model.QuayCode() && value.BerthCode == model.BerthCode()) {
                        berthLength = parseInt(value.Lengthm);

                    }
                });

                if (bollardSum > 0 && lengthInvalid == false) {
                    if (parseInt(model.FromMeter()) < berthLength && parseInt(model.ToMeter()) <= berthLength) {
                        if (parseInt(model.FromMeter()) >= bollardSum && parseInt(model.ToMeter()) >= bollardSum) {
                            if (parseInt(model.FromMeter()) < parseInt(model.ToMeter())) {

                                self.IsValid(true); self.IsFromMeterVisible(false);

                            }
                            else {
                                $('#spanfrommeterid').text('From Meter should be less than To Meter');
                                self.IsFromMeterVisible(true); self.IsValid(false);
                            }
                        }
                        else {
                            if (parseInt(model.FromMeter()) < bollardSum) {
                                $('#spanfrommeterid').text('From Meter should be greater than or equal to previous Bollard To Meter');
                                self.IsFromMeterVisible(true); self.IsValid(false);
                            }
                            if (parseInt(model.ToMeter()) < bollardSum) {

                                $('#spantometerid').text('To Meter should be greater than or equal to previous Bollard To Meter');
                                self.IsToMeterVisible(true); self.IsValid(false);
                            }
                        }
                    }

                    else {
                        if (parseInt(model.FromMeter()) >= berthLength) {
                            $('#spanfrommeterid').text('From Meter should be less than Berth Length');
                            self.IsFromMeterVisible(true); self.IsValid(false);
                        }
                        if (parseInt(model.ToMeter()) > berthLength) {
                            $('#spantometerid').text('To Meter should be less than or equal to Berth Length');
                            self.IsToMeterVisible(true); self.IsValid(false);
                        }
                    }
                }
                else {
                    if (bollardSum == 0) {
                        self.IsValid(true); self.IsFromMeterVisible(false); lengthInvalid = false;
                    }
                }


                if (self.IsUnique() == true && self.IsValid() == true && lengthInvalid == false) {
                    if (parseInt(model.FromMeter()) < berthLength && parseInt(model.ToMeter()) <= berthLength) {
                        if (parseInt(model.FromMeter()) < parseInt(model.ToMeter())) {
                            if (model.ContinousStatus()) {
                                model.Continous = "Y";
                            }
                            else
                                model.Continous = "N";

                            self.viewModelHelper.apiPost('api/Bollards', ko.mapping.toJSON(model), function Message(data) {
                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Bollard details saved successfully", "Bollard");
                                $('#spnTitle').html("Bollard");
                                self.LoadBollards();
                                self.viewMode('List');
                            });
                        }
                        else {
                            // toastr.error('from meter should be less than to meter');
                            $('#spanfrommeterid').text('From Meter should be less than To Meter');
                            self.IsFromMeterVisible(true); self.IsValid(false);
                        }

                    }
                    else {
                        if (parseInt(model.FromMeter()) > berthLength) {
                            $('#spanfrommeterid').text('From Meter should be less than Berth Length');
                            self.IsFromMeterVisible(true); self.IsValid(false);
                        }
                        if (parseInt(model.ToMeter()) > berthLength) {
                            $('#spantometerid').text('To Meter should be less than or equal to Berth Length');
                            self.IsToMeterVisible(true); self.IsValid(false);
                        }
                    }
                }

            }
            else {
                self.BollardValidation().errors.showAllMessages();
                $('#divValidationError').removeClass('display-none');
                return;
            }

           

        }

        self.ModifyBollard = function (model) {
            debugger;
            
            self.BollardValidation = ko.observable(model);
            self.BollardValidation().errors = ko.validation.group(self.BollardValidation());
            var errors = self.BollardValidation().errors().length;

            self.IsUnique(true);
            if (errors == 0) {

                $.each(JSON.parse(ko.toJSON(self.bollardList())), function (index, value) {
                    if (value.PortCode == model.PortCode() && value.QuayCode == model.QuayCode() && value.BerthCode == model.BerthCode()) {

                        if (!((value.BollardCode).toLowerCase() == (model.BollardCode()).toLowerCase())) {
                            if ((value.BollardName).toLowerCase() == (model.BollardName()).toLowerCase()) {

                                self.IsUnique(false);
                                self.UniqueBollardNameVisible(true);
                               
                            }
                            
                            return;
                        }
                        if (!((value.BollardCode).toLowerCase() == (model.BollardCode()).toLowerCase())) {
                            if ((value.ShortName).toLowerCase() == (model.ShortName()).toLowerCase()) {
                                self.IsUnique(false);
                                self.UniqueBollardShortNameVisible(true);

                            }
                            return;
                        }
                        

                    }
                    
                    
                });

                var bollardSum = 0; var lengthInvalid = false;
                $.each(JSON.parse(ko.toJSON(self.bollardList())), function (index, value) {
                    if (value.PortCode == model.PortCode() && value.QuayCode == model.QuayCode() && value.BerthCode == model.BerthCode()) {
                        if (!((value.BollardCode).toLowerCase() == (model.BollardCode()).toLowerCase())) {
                            bollardSum = bollardSum + ((parseInt(value.ToMeter) - parseInt(value.FromMeter)));
                            if (parseInt(model.FromMeter()) < parseInt(value.ToMeter)) {
                                $('#spanfrommeterid').text('From Meter should be greater than or equal to previous Bollard To Meter');
                                lengthInvalid = true; self.IsFromMeterVisible(true); self.IsValid(false);
                            }
                            if (parseInt(model.ToMeter()) < parseInt(value.ToMeter)) {
                                $('#spantometerid').text('To Meter should be greater than or equal to previous Bollard To Meter');
                                lengthInvalid = true; self.IsToMeterVisible(true); self.IsValid(false);
                            }
                        }
                    }
                });



                var berthLength = 0;
                $.each(JSON.parse(ko.toJSON(self.berthValues())), function (index, value) {
                          berthLength = parseInt(value.Lengthm);
                });


                if (bollardSum > 0 && lengthInvalid == false) {

                    if (parseInt(model.FromMeter()) < berthLength && parseInt(model.ToMeter()) <= berthLength) {
                 
                        if (parseInt(model.FromMeter()) >= bollardSum && parseInt(model.ToMeter()) >= bollardSum) {

                            if (parseInt(model.FromMeter()) < parseInt(model.ToMeter())) {
                                self.IsValid(true); self.IsFromMeterVisible(false);
                            }
                            else {
                                $('#spanfrommeterid').text('From Meter should be less than To Meter');
                                self.IsFromMeterVisible(true); self.IsValid(false);
                            }
                        }
                        else {
                            if (parseInt(model.FromMeter()) < bollardSum) {
                                $('#spanfrommeterid').text('From Meter should be greater than or equal to previous Bollard To Meter');
                                self.IsFromMeterVisible(true); self.IsValid(false);
                            }
                            if (parseInt(model.ToMeter()) < bollardSum) {

                                $('#spantometerid').text('To Meter should be greater than or equal to previous Bollard To Meter');
                                self.IsToMeterVisible(true); self.IsValid(false);
                            }
                        }
                    }
                    else {
                        if (parseInt(model.FromMeter()) >= berthLength) {
                            $('#spanfrommeterid').text('From Meter should be less than  Berth Length');
                            self.IsFromMeterVisible(true); self.IsValid(false);
                        }
                        if (parseInt(model.ToMeter()) > berthLength) {
                            $('#spantometerid').text('To Meter should be less than or equal to Berth Length');
                            self.IsToMeterVisible(true); self.IsValid(false);
                        }
                    }

                }
                else {
                    if (bollardSum == 0) {
                        self.IsValid(true); self.IsFromMeterVisible(false); lengthInvalid = false;
                    }
                }

                if (self.IsUnique() == true && self.IsValid() == true && lengthInvalid == false) {
                    if (parseInt(model.FromMeter()) < berthLength && parseInt(model.ToMeter()) <= berthLength) {
                        if (parseInt(model.FromMeter()) < parseInt(model.ToMeter())) {
                            if (model.ContinousStatus()) {
                                model.Continous = "Y";
                            }
                            else
                                model.Continous = "N";
                            self.viewModelHelper.apiPut('api/Bollards', ko.mapping.toJSON(model), function Message(data) {

                                toastr.options.closeButton = true;
                                toastr.options.positionClass = "toast-top-right";
                                toastr.success("Bollard details updated successfully", "Bollard");
                                self.LoadBollards();
                                self.viewMode('List');
                                $('#spnTitle').html("Bollard");

                            });
                        }
                        else {
                            // toastr.error('from meter  should be than to meter');
                            $('#spanfrommeterid').text('From Meter should be less than To meter');
                            self.IsFromMeterVisible(true); self.IsValid(false);
                        }
                    }
                    else {
                        if (parseInt(model.FromMeter()) > berthLength) {
                            $('#spanfrommeterid').text('From Meter should be less than  Berth Length');
                            self.IsFromMeterVisible(true); self.IsValid(false);
                        }
                        if (parseInt(model.ToMeter()) > berthLength) {
                            $('#spantometerid').text('To Meter should be less than or equal to Berth Length');
                            self.IsToMeterVisible(true); self.IsValid(false);
                        }
                    }
                }

            }
                else {

                    self.BollardValidation().errors.showAllMessages();
                    $('#divValidationError').removeClass('display-none');
                }
           

          
        }
        self.DeleteBollard = function (model) {          
            self.viewModelHelper.apiPost('api/Bollards/PutDeleteBollard', ko.mapping.toJS(model), alert("success"));
        }

        self.viewbollard = function (bollard) {
            self.viewMode('Form');
            self.IsUpdate(false);
            self.IsSave(false);
            self.IsReset(false);
            self.editableView(false);
            self.IsCodeEnable(false);
            self.isPortChanged(false);
            self.isQuayChanged(false);
            self.LoadPorts();
            self.quayValues([{ QuayCode: bollard.QuayCode(), QuayName: bollard.QuayName(), QuayLength:bollard.QuayLength()}]);
            self.berthValues([{ BerthCode: bollard.BerthCode(), BerthName: bollard.BerthName(), Lengthm: bollard.BerthLength() }]);
            self.bollardModel(bollard);
            $('#spnTitle').html("View Bollard");
            $("#grid").hide();
        }

        self.editbollard = function (bollard) {
            self.LoadPorts();
            self.IsValid(true);
            self.IsUnique(true);
            self.quayValues([{ QuayCode: bollard.QuayCode(), QuayName: bollard.QuayName(), QuayLength: bollard.QuayLength() }]);
            self.berthValues([{ BerthCode: bollard.BerthCode(), BerthName: bollard.BerthName(), Lengthm: bollard.BerthLength() }]);
            self.LoadBollardsInBerth(bollard);
            self.isPortChanged(false);
            self.isQuayChanged(false);
            self.viewMode('Form');
            self.IsUpdate(true);
            self.IsSave(false);
            self.IsReset(true);
            self.editableView(true);            
            self.IsCodeEnable(false);
            self.isPortChanged(false);
            self.isQuayChanged(false);
            $('#spnTitle').html("Update Bollard");
            $("#grid").show();
            self.bollardModel(bollard);
       }

        self.addBollard = function () {
           self.bollardModel(new IPMSRoot.BollardModel());
            self.viewMode('Form');
            self.isPortChanged(false);
            self.isQuayChanged(false);
            self.IsValid(true);
            self.IsUnique(true);
            self.IsUpdate(false);
            self.IsSave(true);
            self.IsReset(true);
            self.IsCodeEnable(true);
            self.LoadPorts();
            self.editableView(true);
            self.bollardsInBerth("");
            $('#spnTitle').html("Add Bollard");
            $("#grid").show();
        }

        self.cancel = function () {
            self.viewMode('List');          
            self.bollardModel().reset();
            $('#spnTitle').html("Bollard");
            if (self.UniqueBollardCodeVisible() == true) {
                self.UniqueBollardCodeVisible(false);
            }

            if (self.UniqueBollardNameVisible() == true) {
                self.UniqueBollardNameVisible(false);
            }
            if (self.UniqueBollardShortNameVisible() == true) {
                self.UniqueBollardShortNameVisible(false);
            }
            if (self.IsFromMeterVisible() == true) {
                self.IsFromMeterVisible(false);
            }
            if (self.IsToMeterVisible() == true) {
                self.IsToMeterVisible(false);
            }
        }

        self.ResetBollard = function (model) {
            ko.validation.reset();
            self.bollardModel().reset();
            self.BollardValidation = ko.observable(model);
            self.BollardValidation().errors = ko.validation.group(self.BollardValidation());
           self.BollardValidation().errors.showAllMessages(false);
           self.IsValid(true);
           self.IsUnique(true);

           if (self.IsSave()) {
               self.bollardsInBerth("");
           }
           else {
               self.LoadBollardsInBerth(model);
           }
           if (self.UniqueBollardCodeVisible() == true) {
               self.UniqueBollardCodeVisible(false);
           }

           if (self.UniqueBollardNameVisible() == true) {
               self.UniqueBollardNameVisible(false);
           }
           if (self.UniqueBollardShortNameVisible() == true) {
               self.UniqueBollardShortNameVisible(false);
           }
           if (self.IsFromMeterVisible() == true) {
               self.IsFromMeterVisible(false);
           }
           if (self.IsToMeterVisible() == true) {
               self.IsToMeterVisible(false);
           }
        }
     self.Initialize();
    }
    IPMSRoot.BollardViewModel = BollardViewModel;

}(window.IPMSROOT));
