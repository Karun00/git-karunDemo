using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class CodeDtl : EntityBase
	{
		[DataMember]
		public int CodeDtlID { get; set; }
		[DataMember]
		public int CodeID { get; set; }
		[DataMember]
		public int StartValue { get; set; }
		[DataMember]
		public int CurValue { get; set; }
		[DataMember]
		public int CodeDtlMonth { get; set; }
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
		public virtual Code Code { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }
	}
}
