using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    public partial class BerthMaintenanceCompApproval : EntityBase
    {
        public int BerthMaintenanceCompApprovalID { get; set; }
        public int BerthMaintenanceCompletionID { get; set; }
        public string WFStatus { get; set; }
        public int ApprovedBy { get; set; }
        public System.DateTime ApprovedDate { get; set; }
        public string RejectComments { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual User User { get; set; }
        public virtual BerthMaintenanceCompletion BerthMaintenanceCompletion { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
