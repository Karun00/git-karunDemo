using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class ServiceRequestDocument : EntityBase
	{
        [DataMember]
        public int ServiceRequestDocumentID { get; set; }
		[DataMember]
		public int ServiceRequestID { get; set; }
		[DataMember]
		public int DocumentID { get; set; }
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
        public string DocumentCode { get; set; }
		[DataMember]
		public virtual Document Document { get; set; }
		[DataMember]
		public virtual ServiceRequest ServiceRequest { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
		[DataMember]
		public virtual User User { get; set; }
        [DataMember]
		public virtual User User1 { get; set; }
	}
}
