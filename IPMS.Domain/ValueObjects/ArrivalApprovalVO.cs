
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Arrival Notification Approvals
    /// </summary>
    public class ArrivalApprovalVO
    {
        public string VCN { get; set; }
        public int RoleID { get; set; }
        public string WFStatus { get; set; }
        public int ApprovedBy { get; set; }
        public System.DateTime ApprovedDate { get; set; }
        public string RejectComments { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
