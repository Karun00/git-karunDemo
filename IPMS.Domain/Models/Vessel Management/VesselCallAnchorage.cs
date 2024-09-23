using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class VesselCallAnchorage : EntityBase
	{
		[DataMember]
		public int VesselCallAnchorageID { get; set; }
		[DataMember]
		public string VCN { get; set; }
		[DataMember]
		public System.DateTime AnchorDropTime { get; set; }
		[DataMember]
		public Nullable<System.DateTime> AnchorAweighTime { get; set; }
		[DataMember]
		public string AnchorPosition { get; set; }
		[DataMember]
		public string BearingDistanceFromBreakWater { get; set; }
		[DataMember]
		public string Reason { get; set; }
		[DataMember]
		public string Remarks { get; set; }
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
		public virtual ArrivalNotification ArrivalNotification { get; set; }
		[DataMember]
		public virtual SubCategory SubCategory { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }
      
	}
}
