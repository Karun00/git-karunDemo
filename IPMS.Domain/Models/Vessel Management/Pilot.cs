using Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    public partial class Pilot : EntityBase
    {
        public Pilot()
        {
            this.ArrivalNotifications = new List<ArrivalNotification>();
            this.PilotCertificates = new List<PilotCertificate>();
            this.PilotExemptionRequests = new List<PilotExemptionRequest>();
            this.PilotExemptionRequestDocuments = new List<PilotExemptionRequestDocument>();
        }

        public int PilotID { get; set; }
        public string PortCode { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateofBirth { get; set; }
        public string IDNo { get; set; }
        public string NationalityCode { get; set; }
        public System.DateTime IssuedDate { get; set; }
        public System.DateTime RenewalDate { get; set; }
        public string IssuingAuthority { get; set; }
        public string InvoiceRecipient { get; set; }
        public string LicenseRecipient { get; set; }
        public int PostalAddressID { get; set; }
        public Nullable<int> ResidentialAddressID { get; set; }
        public string ContactNo { get; set; }
        public string CellNo { get; set; }
        public string EmailID { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        //Added by Santosh on 31-Dec-2014 for BugID: 1935
        public Nullable<DateTime> IssuedApprovedDate { get; set; }
        public Nullable<DateTime> ExpiryDate { get; set; }
        public string Certificate_of_Competency { get; set; }
        [NotMapped]
        public string FullName { get; set; }

        [NotMapped]
        public string PortName { get; set; }

        [NotMapped]
        public string IssueDate { get; set; }

        [NotMapped]
        public string RenewDate { get; set; }
        public  Address PostalAddress { get; set; }
        public  Address ResidentialAddress { get; set; }
        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  Port Port { get; set; }
        public  ICollection<PilotCertificate> PilotCertificates { get; set; }
        public  WorkflowInstance WorkflowInstance { get; set; }
        public  ICollection<PilotExemptionRequest> PilotExemptionRequests { get; set; }
        public  ICollection<PilotExemptionRequestDocument> PilotExemptionRequestDocuments { get; set; }

    }
}
