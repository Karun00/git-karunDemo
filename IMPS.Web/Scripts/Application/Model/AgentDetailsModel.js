(function (ipmsRoot) {
    var ApplicantModel = function (data) {

        var self = this;
        self.ApplicantID = ko.observable(); 
        self.ApplicantName = ko.observable();
        self.ApplicantTradName = ko.observable();
        self.RegnNo = ko.observable();
        self.VatNo = ko.observable();
        self.IncTaxNo = ko.observable();
        self.SkilDevLevyNo = ko.observable();
        //self.SarsTaxCleaCert = ko.observable();
        //self.SAASOA = ko.observable();
        //self.BBBEEQualify = ko.observable();
        //self.BBBEEStatus = ko.observable();
        //self.BBBEEStatVeri = ko.observable();
        //self.Status = ko.observable();
        self.CreatedBy = ko.observable();
        self.CreatedDate = ko.observable();
        self.ModifiedBy = ko.observable();
        self.ModifiedDate = ko.observable();

    }

    ipmsRoot.ApplicantModel = ApplicantModel;


   
        var ApplicantAddress = function (data) {

            var self = this;
            self.NumStreet = ko.observable(); 
            self.Suburb = ko.observable();
            self.TownCity = ko.observable();
            self.PostalCode = ko.observable();
            self.Telephone1 = ko.observable();
            self.FaxNo = ko.observable(); 
       
        }

        ipmsRoot.ApplicantAddress = ApplicantAddress;


            var ApplicantAuthCont= function (data) {

                var self = this;
                self.FirstName = ko.observable(); 
                self.LastName = ko.observable();
                self.IdentityNo = ko.observable();
                self.Designation = ko.observable();
                self.CellularNo = ko.observable();
                self.Designation = ko.observable(); 
       
            }

            ipmsRoot.ApplicantAuthCont = ApplicantAuthCont;


}
(window.IPMSROOT));

    
