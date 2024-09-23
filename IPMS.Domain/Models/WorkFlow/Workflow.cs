using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class Workflow : EntityBase
	{
		public Workflow()
		{
			this.WorkflowInstances = new List<WorkflowInstance>();
			this.WorkflowStepConfigs = new List<WorkflowStepConfig>();
		}

		[DataMember]
		public string WorkflowCode { get; set; }
		[DataMember]
		public string WorkflowName { get; set; }
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
		public virtual ICollection<WorkflowInstance> WorkflowInstances { get; set; }
		[DataMember]
		public virtual ICollection<WorkflowStepConfig> WorkflowStepConfigs { get; set; }
	}
}
