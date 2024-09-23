using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class LicenseRequest : EntityBase
    {
        public LicenseRequest()
        {
            this.Bunkerings = new List<Bunkering>();
            this.Divings = new List<Diving>();
            this.FireEquipments = new List<FireEquipment>();
            this.FireProtections = new List<FireProtection>();
            this.FloatingCranes = new List<FloatingCrane>();
            this.LicenseRequestPorts = new List<LicenseRequestPort>();
            this.PestControls = new List<PestControl>();
            this.PollutionControls = new List<PollutionControl>();
            this.Stevedores = new List<Stevedore>();
            this.LicenseRequestDocuments = new List<LicenseRequestDocument>();
            this.ArrivalNotifications = new List<ArrivalNotification>();
            this.WasteDeclarations = new List<WasteDeclaration>();
        }
        [DataMember]
        public int LicenseRequestID { get; set; }
        [DataMember]
        public string LicenseRequestType { get; set; }
        [DataMember]
        public string ReferenceNo { get; set; }
        [DataMember]
        public string RegisteredName { get; set; }
        [DataMember]
        public string TradingName { get; set; }
        [DataMember]
        public string RegistrationNumber { get; set; }
        [DataMember]
        public string VATNumber { get; set; }
        [DataMember]
        public string IncomeTaxNumber { get; set; }
        [DataMember]
        public string SkillsDevLevyNumber { get; set; }
        [DataMember]
        public int BusinessAddressID { get; set; }
        [DataMember]
        public Nullable<int> PostalAddressID { get; set; }
        [DataMember]
        public string TelephoneNo1 { get; set; }
        [DataMember]
        public string TelephoneNo2 { get; set; }
        [DataMember]
        public string FaxNo { get; set; }
        [DataMember]
        public int AuthorizedContactPersonID { get; set; }
        [DataMember]
        public string ValidTaxClearanceCertificate { get; set; }
        [DataMember]
        public string BBBEEStatus { get; set; }
        [DataMember]
        public string VerifiedBBBEEStatus { get; set; }
        [DataMember]
        public string BBBEEExemptedMicroEnterprise { get; set; }
        [DataMember]
        public string PublicLiabilityInsurance { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

        [NotMapped]
        public string LicenseRequestTypeName { get; set; }
        [DataMember]
        public  ICollection<Bunkering> Bunkerings { get; set; }
        [DataMember]
        public  ICollection<Diving> Divings { get; set; }
        [DataMember]
        public  ICollection<FireEquipment> FireEquipments { get; set; }
        [DataMember]
        public  ICollection<FireProtection> FireProtections { get; set; }
        [DataMember]
        public  ICollection<FloatingCrane> FloatingCranes { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestPort> LicenseRequestPorts { get; set; }
        [DataMember]
        public  ICollection<PestControl> PestControls { get; set; }
        [DataMember]
        public  ICollection<PollutionControl> PollutionControls { get; set; }
        [DataMember]
        public  ICollection<Stevedore> Stevedores { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestDocument> LicenseRequestDocuments { get; set; }
        [DataMember]
        public  AuthorizedContactPerson AuthorizedContactPerson { get; set; }
        [DataMember]
        public  Address BusinessAddress { get; set; }
        [DataMember]
        public  Address PostalAddress { get; set; }
        [DataMember]
        public ICollection<WasteDeclaration> WasteDeclarations { get; set; }
    }
}
