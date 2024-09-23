using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class BerthCargo : EntityBase
	{
		[DataMember]
		public int BerthCargoID { get; set; }
		[DataMember]
		public string PortCode { get; set; }
		[DataMember]
		public string QuayCode { get; set; }
		[DataMember]
		public string BerthCode { get; set; }
		[DataMember]
		public string CargoTypeCode { get; set; }
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
		public virtual Berth Berth { get; set; }
		[DataMember]
		public virtual SubCategory SubCategory { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }
	}
}
