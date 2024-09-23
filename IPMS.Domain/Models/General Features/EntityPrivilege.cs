using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class EntityPrivilege : EntityBase
	{
		public EntityPrivilege()
		{
			this.RolePrivileges = new List<RolePrivilege>();
		}
		[DataMember]
		public int EntityID { get; set; }
        [DataMember]
        public string SubCatCode { get; set; }
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
		public  Entity Entity { get; set; }
        [DataMember]
		public  SubCategory SubCategory { get; set; }
        [DataMember]
		public  ICollection<RolePrivilege> RolePrivileges { get; set; }
	}
}
