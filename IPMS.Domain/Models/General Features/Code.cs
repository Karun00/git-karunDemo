using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class Code : EntityBase
	{
		public Code()
		{
			this.CodeDtls = new List<CodeDtl>();
		}

		[DataMember]
		public int CodeID { get; set; }
		[DataMember]
		public string PortCode { get; set; }
		[DataMember]
		public string CodeName { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public int StartValue { get; set; }
		[DataMember]
		public int CurValue { get; set; }
		[DataMember]
		public string IsMonth { get; set; }
		[DataMember]
		public int CodeYear { get; set; }
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
		public  User User { get; set; }
		[DataMember]
		public  User User1 { get; set; }
		[DataMember]
		public  Port Port { get; set; }
		[DataMember]
		public  ICollection<CodeDtl> CodeDtls { get; set; }
	}
}
