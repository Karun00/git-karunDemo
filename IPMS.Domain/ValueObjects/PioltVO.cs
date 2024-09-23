using System;
using System.Collections.Generic;


namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Pilot
    /// </summary>
    public partial class PioltVO
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
        public string DateofBirth { get; set; }
        public string IssuedDate { get; set; }
        public Nullable<DateTime> ExpiryDate { get; set; }
        public string RenewalDate { get; set; }

        public Boolean AddressCheckbox { get; set; }

        public string ContactNo { get; set; }
        public string CellNo { get; set; }
        public string EmailID { get; set; }
        public string IDNo { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public string RecordStatus { get; set; }       
        public int CreatedBy { get; set; }       
        public System.DateTime CreatedDate { get; set; }       
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string Certificate_of_Competency { get; set; }
        //Added by Santosh on 31-Dec-2014 for BugID: 1935
        public Nullable<DateTime> IssuedApprovedDate { get; set; }
        public string FullName { get; set; }
        public string IssueDate { get; set; }
        public string RenewDate { get; set; }
        public AddressVO ResidentialAddress { get; set; }      
        public AddressVO PostalAddress { get; set; }
        public WorkFlowInstanceVO Workflowinstance { get; set; }
        public List<PilotExemptionRequestVO> PilotExemptionRequest { get; set; }
        public List<PilotExemptionRequestDocumentVO> PilotExemptionRequestdocument{ get; set; }
        public PilotCertificateVO pilotcertificate { get; set; }

        //public string IssuedDate(string p)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
