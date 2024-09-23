using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPMS.Domain.Models
{
    public partial class BerthMaintenanceCompletion : EntityBase
    {
        public BerthMaintenanceCompletion()
        {
            this.BerthMaintenanceCompApprovals = new List<BerthMaintenanceCompApproval>();
        }

        public int BerthMaintenanceCompletionID { get; set; }
        public int BerthMaintenanceID { get; set; }
        public System.DateTime CompletionDateTime { get; set; }
        public string observation { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public  BerthMaintenance BerthMaintenance { get; set; }
        public  ICollection<BerthMaintenanceCompApproval> BerthMaintenanceCompApprovals { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  WorkflowInstance WorkflowInstance { get; set; }


        [NotMapped]
        public string MaintenanceType { get; set; }
        [NotMapped]
        public string BerthName { get; set; }
        [NotMapped]
        public string BollardsFrom { get; set; }
        [NotMapped]
        public string BollardsTo { get; set; }
        [NotMapped]
        public string ReferenceNo { get; set; }

    }
}
