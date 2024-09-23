using System;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for pilotExemption Details
    /// </summary>
    public class pilotExemptionDetailsVO
    {
        public int PilotID { get; set; }
        public string PortCode { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string NationalityCode { get; set; }
        public string IssuingAuthority { get; set; }
        public string InvoiceRecipient { get; set; }
        public string LicenseRecipient { get; set; }
        public int PostalAddressID { get; set; }
        public Nullable<int> ResidentialAddressID { get; set; }
        public System.DateTime DateofBirth { get; set; }
        public String IssuedDate { get; set; }
        public String RenewalDate { get; set; }
        public string ContactNo { get; set; }
        public string CellNo { get; set; }
        public string EmailID { get; set; }
        public string IDNo { get; set; }
        public string RecordStatus { get; set; }
        public string WorkflowTaskCode { get; set; }
        public int PilotExemptionRequestID { get; set; }
        public int PilotExemptionRequestDocumentID { get; set; }   
        public int DocumentID { get; set; }   
        public string FileName { get; set; }
        public string DocumentName { get; set; }
        public System.DateTime MovementDate { get; set; }
        public int VesselID { get; set; }
        public string PilotRoleCode { get; set; }
        public string MovementTypeCode { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
