using System;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for LicenseRequest Port
    /// </summary>
    public partial class LicenseRequestPortVO
    {

        public int LicenseRequestPortID { get; set; }

        public int LicenseRequestID { get; set; }

        public string PortCode { get; set; }

        public string WFStatus { get; set; }

        public int VerifiedBy { get; set; }

        public System.DateTime VerifiedDate { get; set; }

        public int ApprovedBy { get; set; }

        public System.DateTime ApprovedDate { get; set; }
        public Nullable<int> WorkflowInstanceID { get; set; }

        public string RejectComments { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public Nullable<int> ModifiedBy { get; set; }


        public System.DateTime ModifiedDate { get; set; }
        //public  LicenseRequestVO LicenseRequest { get; set; }
        //public virtual User User { get; set; }
        //public virtual User User1 { get; set; }
        //public virtual User User2 { get; set; }
        //public virtual Port Port { get; set; }
        //public virtual User User3 { get; set; }
        //public virtual SubCategory SubCategory { get; set; }
    }
}
