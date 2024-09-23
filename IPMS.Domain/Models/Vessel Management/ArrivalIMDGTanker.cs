using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class ArrivalIMDGTanker : EntityBase
	{
		[DataMember]
		public int ArrivalIMDGTankerID { get; set; }
		[DataMember]
		public string VCN { get; set; }
		[DataMember]
		public string Purpose { get; set; }
		[DataMember]
		public string Commodity { get; set; }
		[DataMember]
        public Nullable<decimal> Quantity { get; set; }
		[DataMember]
		public string FromTank { get; set; }
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
		public virtual SubCategory SubCategory { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }
		[DataMember]
		public virtual SubCategory SubCategory1 { get; set; }
		[DataMember]
		public virtual ArrivalNotification ArrivalNotification { get; set; }
	}
}
