using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class ServiceRequestSailing : EntityBase
	{
		[DataMember]
		public int ServiceRequestSailingID { get; set; }
		[DataMember]
		public int ServiceRequestID { get; set; }
		[DataMember]
		public string MarineRevenueCleared { get; set; }
		[DataMember]
		public Nullable<int> DocumentID { get; set; }
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
		public virtual Document Document { get; set; }
		[DataMember]
		public virtual ServiceRequest ServiceRequest { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }
	}
}
