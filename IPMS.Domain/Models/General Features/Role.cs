using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class Role : EntityBase
	{
		public Role()
		{
			this.ArrivalApprovals = new List<ArrivalApproval>();
			this.NotificationRoles = new List<NotificationRole>();
			this.RolePrivileges = new List<RolePrivilege>();
			this.UserRoles = new List<UserRole>();
            this.WorkflowProcess = new List<WorkflowProcess>();
            this.WorkflowTaskRoles = new List<WorkflowTaskRole>();
            //added by shankar on 07-Nov-14
            this.PortContentRoles = new List<PortContentRole>();
            //end
        }
		[DataMember]
		public int RoleID { get; set; }
        [DataMember]
        public string RoleCode { get; set; }
		[DataMember]
		public string RoleName { get; set; }
        [DataMember]
        public string RoleDescription { get; set; }
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
		public  ICollection<ArrivalApproval> ArrivalApprovals { get; set; }
		[DataMember]
		public  ICollection<NotificationRole> NotificationRoles { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
		[DataMember]
		public  ICollection<RolePrivilege> RolePrivileges { get; set; }
		[DataMember]
		public  ICollection<UserRole> UserRoles { get; set; }
		[DataMember]
        public  ICollection<WorkflowProcess> WorkflowProcess { get; set; }
        [DataMember]
        public  ICollection<WorkflowTaskRole> WorkflowTaskRoles { get; set; }
        //added by shankar on 07-Nov-14
        public  ICollection<PortContentRole> PortContentRoles { get; set; }
        //end
    }
}
