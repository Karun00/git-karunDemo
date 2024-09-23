using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class ChangePasswordLog : EntityBase
	{      

		[DataMember]
        public int LogTransId { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public System.DateTime ChangeDateTime { get; set; }
        [DataMember]
        public string OldPwd { get; set; }
        [DataMember]
        public string NewPwd { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual User User2 { get; set; }
	}
}
