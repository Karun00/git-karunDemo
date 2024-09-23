using System;
using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for LicenseRequest
    /// </summary>
    public partial class LicenseRequestVO
    {
        public int LicenseRequestID { get; set; }
        public string LicenseRequestType { get; set; }
        public List<string> LicenseRequestPortsArr { get; set; }
        public List<string> LicensePortWFArray { get; set; }

        public List<LicenseRequestDocumentVO> LicenseRequestDocuments { get; set; }

        public string ReferenceNo { get; set; }

        public string RegisteredName { get; set; }

        public string TradingName { get; set; }

        public string RegistrationNumber { get; set; }

        public string VATNumber { get; set; }

        public string IncomeTaxNumber { get; set; }

        public string SkillsDevLevyNumber { get; set; }

        public int BusinessAddressID { get; set; }

        public Nullable<int> PostalAddressID { get; set; }

        public string TelephoneNo1 { get; set; }

        public string TelephoneNo2 { get; set; }

        public string FaxNo { get; set; }

        public int AuthorizedContactPersonID { get; set; }

        public Nullable<int> ReferenceWorkflowInstenceID { get; set; }

        public string ValidTaxClearanceCertificate { get; set; }

        public string BBBEEStatus { get; set; }

        public string VerifiedBBBEEStatus { get; set; }

        public string BBBEEExemptedMicroEnterprise { get; set; }

        public string PublicLiabilityInsurance { get; set; }
        public string Workflowstatus { get; set; }
        public Nullable<int> WorkflowInstanceID { get; set; }
        public string LicenseRequestTypeName { get; set; }

        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Boolean AddressCheckbox { get; set; }
        public AddressVO BusinessAddress { get; set; }

        public AddressVO PostalAddress { get; set; }

        public AuthorizedContactPersonVO AuthorizedContactPerson { get; set; }

        public BunkeringVO Bunkerings { get; set; }

        public DivingVO Divings { get; set; }

        public FireEquipmentVO FireEquipments { get; set; }

        public FireProtectionVO FireProtections { get; set; }

        public FloatingCraneVO FloatingCranes { get; set; }

        public LicenseRequestPortVO LicenseRequestPorts { get; set; }

        public PestControlVO PestControls { get; set; }

        public PollutionControlVO PollutionControls { get; set; }

        public StevedoreVO Stevedores { get; set; }
    }
}

