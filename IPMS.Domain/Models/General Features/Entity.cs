using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class Entity : EntityBase
	{
	  
		public Entity()
		{
			this.EntityPrivileges = new List<EntityPrivilege>();
            this.NotificationTemplates = new List<NotificationTemplate>();
            this.WorkflowInstances = new List<WorkflowInstance>();
            this.WorkflowTasks = new List<WorkflowTask>();
            this.WorkflowTaskRole = new List<WorkflowTaskRole>();
            this.EventSchedules = new List<EventSchedule>();
		}
		  [DataMember]
		public int EntityID { get; set; }
		  [DataMember]
		public int ModuleID { get; set; }
		  [DataMember]
		public string EntityName { get; set; }
		  [DataMember]
		public string PageUrl { get; set; }
		  [DataMember]
		public int OrderNo { get; set; }
		  [DataMember]
		public string RecordStatus { get; set; }
		  [DataMember]
		public int CreatedBy { get; set; }
		  [DataMember]
		public Nullable<System.DateTime> CreatedDate { get; set; }
		  [DataMember]
		public Nullable<int> ModifiedBy { get; set; }
		  [DataMember]
		public Nullable<System.DateTime> ModifiedDate { get; set; }
		  [DataMember]
        public string Tokens { get; set; }
        [DataMember]
        public string HasWorkFlow { get; set; }
        [DataMember]
        public string HasMenuItem { get; set; }
        [DataMember]
        public string ControllerName { get; set; }     
        [DataMember]
        public string EntityCode { get; set; }
        [DataMember]
        public string PendingTaskColumns { get; set; }
        [DataMember]
		public  User User { get; set; }
		[DataMember]
		public  User User1 { get; set; }
		[DataMember]
		public  Module Module { get; set; }
		[DataMember]
		public  ICollection<EntityPrivilege> EntityPrivileges { get; set; }
        [DataMember]
        public  ICollection<NotificationTemplate> NotificationTemplates { get; set; }
        [DataMember]
        public  ICollection<WorkflowInstance> WorkflowInstances { get; set; }
        [DataMember]
        public  ICollection<WorkflowTask> WorkflowTasks { get; set; }
        [DataMember]
        public  ICollection<WorkflowTaskRole> WorkflowTaskRole { get; set; }
        [DataMember]
        public  ICollection<EventSchedule> EventSchedules { get; set; }

          
	}
}
