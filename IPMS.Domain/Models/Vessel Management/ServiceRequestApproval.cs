using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class ServiceRequestApproval : EntityBase
	{
		[DataMember]
		public int ServiceRequestApprovalID { get; set; }
		[DataMember]
		public int ServiceRequestID { get; set; }
		[DataMember]
		public string WFStatus { get; set; }
		[DataMember]
		public int ApprovedBy { get; set; }
		[DataMember]
		public System.DateTime ApprovedDate { get; set; }
		[DataMember]
		public string RejectComments { get; set; }
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
		public virtual ServiceRequest ServiceRequest { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }
		[DataMember]
		public virtual User User2 { get; set; }
		[DataMember]
		public virtual SubCategory SubCategory { get; set; }
	}
}