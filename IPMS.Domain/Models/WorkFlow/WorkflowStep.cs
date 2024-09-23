using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class WorkflowStep : EntityBase
    {
        public WorkflowStep()
        {
            this.WorkflowStepRoles = new List<WorkflowStepRole>();
        }

        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public int Step { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual ICollection<WorkflowStepRole> WorkflowStepRoles { get; set; }
    }
	
}
