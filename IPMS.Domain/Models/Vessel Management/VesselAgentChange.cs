using System;
using System.Collections.Generic;
using Core.Repository;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    public partial class VesselAgentChange : EntityBase
    {
        public VesselAgentChange()
        {
            this.VesselAgentChangeApprovals = new List<VesselAgentChangeApproval>();
            this.VesselAgentChangeDocuments = new List<VesselAgentChangeDocument>();
        }

        public int VesselAgentChangeID { get; set; }
        public string VCN { get; set; }
        public int ProposedAgent { get; set; }
        [NotMapped]
        public  string ProposedAgentName { get; set; }
        [NotMapped]
        public  string RequestedAgentName { get; set; }
        [NotMapped]
        public  string ReasonForVisit { get; set; }
        [NotMapped]
        public  string VesselType { get; set; }
        [NotMapped]
        public string VesselName { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  Agent Agent { get; set; }

        public  SubCategory SubCategory { get; set; }
        public  WorkflowInstance WorkflowInstatnce { get; set; }

        public  ArrivalNotification ArrivalNotification { get; set; }
        public  User CreatedUser { get; set; }
        public  User ModifiedUser { get; set; }
        public  ICollection<VesselAgentChangeApproval> VesselAgentChangeApprovals { get; set; }
        public  ICollection<VesselAgentChangeDocument> VesselAgentChangeDocuments { get; set; }
        public string ReasonForTransferCode { get; set; }
        public System.DateTime EffectiveDateTime { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public Nullable<int> CurrentAgentID { get; set; }
        [NotMapped]
        public string WorkFlowStatus { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string IsFinal { get; set; }
    }
}
