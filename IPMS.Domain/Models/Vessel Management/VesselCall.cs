using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class VesselCall : EntityBase
	{
		[DataMember]
		public int VesselCallID { get; set; }
		[DataMember]
		public string VCN { get; set; }
		[DataMember]
		public int RecentAgentID { get; set; }
		[DataMember]
		public System.DateTime ETA { get; set; }
		[DataMember]
		public System.DateTime ETD { get; set; }
		[DataMember]
        public Nullable<System.DateTime> ETB { get; set; }
		[DataMember]
        public Nullable<System.DateTime> ETUB { get; set; }
		[DataMember]
        public Nullable<System.DateTime> ATA { get; set; }
		[DataMember]
        public Nullable<System.DateTime> ATD { get; set; }
		[DataMember]
        public Nullable<System.DateTime> ATB { get; set; }
		[DataMember]
        public Nullable<System.DateTime> ATUB { get; set; }
		[DataMember]
        public Nullable<System.DateTime> BreakWaterIn { get; set; }
		[DataMember]
        public Nullable<System.DateTime> BreakWaterOut { get; set; }
		[DataMember]
        public Nullable<System.DateTime> PortLimitIn { get; set; }
		[DataMember]
        public Nullable<System.DateTime> PortLimitOut { get; set; }
		[DataMember]
        public Nullable<System.DateTime> AnchorUp { get; set; }
		[DataMember]
        public Nullable<System.DateTime> AnchorDown { get; set; }
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
		public string RecordStatus { get; set; }
		[DataMember]
		public int CreatedBy { get; set; }
		[DataMember]
		public System.DateTime CreatedDate { get; set; }
		[DataMember]
		public int? ModifiedBy { get; set; }
		[DataMember]
		public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public int NoofTimesETAChanged { get; set; }
		[DataMember]
		public virtual Agent Agent { get; set; }
		[DataMember]
		public virtual ArrivalNotification ArrivalNotification { get; set; }
		[DataMember]
		public virtual Bollard Bollard { get; set; }
		[DataMember]
		public virtual Bollard Bollard1 { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }

	}
}
