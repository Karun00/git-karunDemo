using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class ServiceRequestWarping : EntityBase
	{
		[DataMember]
		public int ServiceRequestWarpingID { get; set; }
		[DataMember]
		public int ServiceRequestID { get; set; }
		[DataMember]
		public string FromPositionPortCode { get; set; }
		[DataMember]
		public string FromPositionQuayCode { get; set; }
		[DataMember]
		public string FromPositionBerthCode { get; set; }
		[DataMember]
		public string FromPositionBollardCode { get; set; }
		[DataMember]
		public string ToPositionPortCode { get; set; }
		[DataMember]
		public string ToPositionQuayCode { get; set; }
		[DataMember]
		public string ToPositionBerthCode { get; set; }
		[DataMember]
		public string ToPositionBollardCode { get; set; }

        [DataMember]
        public string Warp { get; set; }
        [DataMember]
        public decimal WarpDistance { get; set; }

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
		public virtual Bollard Bollard { get; set; }
		[DataMember]
		public virtual Bollard Bollard1 { get; set; }
		[DataMember]
		public virtual ServiceRequest ServiceRequest { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }

        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
	}
}
