using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPMS.Domain.Models
{
	[DataContract]
	public partial class VesselCallMovement : EntityBase
	{
		[DataMember]
		public int VesselCallMovementID { get; set; }
		[DataMember]
		public string VCN { get; set; }
        [DataMember]
        public string MovementType { get; set; }
        [DataMember]
        public Nullable<System.DateTime> MovementDateTime { get; set; }
		[DataMember]
		public Nullable<int> ServiceRequestID { get; set; }
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
		public string SlotStatus { get; set; }
		[DataMember]
		public Nullable<System.DateTime> SlotDate { get; set; }
		[DataMember]
		public string Slot { get; set; }
		[DataMember]
		public string MovementStatus { get; set; }
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
		public virtual Bollard Bollard { get; set; }
		[DataMember]
		public virtual Bollard Bollard1 { get; set; }
		[DataMember]
		public virtual ServiceRequest ServiceRequest { get; set; }
		[DataMember]
        public virtual SubCategory MovementStatusName { get; set; }
		[DataMember]
		public virtual SubCategory SubCategory1 { get; set; }
        [DataMember]
        public virtual SubCategory MovementType_SubCategory { get; set; }
		[DataMember]
		public virtual User User { get; set; }
		[DataMember]
		public virtual User User1 { get; set; }
        //[DataMember]
        //public virtual VesselCall VesselCall { get; set; }


        [DataMember]
        public Nullable<System.DateTime> ETB { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ETUB { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ATB { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ATUB { get; set; }

        //-- Added by sandeep on 29-04-2015
        [DataMember]
        public string MooringBollardBowPortCode { get; set; }
        [DataMember]
        public string MooringBollardBowQuayCode { get; set; }
        [DataMember]
        public string MooringBollardBowBerthCode { get; set; }
        [DataMember]
        public string MooringBollardBowBollardCode { get; set; }
        [DataMember]
        public string MooringBollardStemPortCode { get; set; }
        [DataMember]
        public string MooringBollardStemQuayCode { get; set; }
        [DataMember]
        public string MooringBollardStemBerthCode { get; set; }
        [DataMember]
        public string MooringBollardStemBollardCode { get; set; }
        [DataMember]
        public virtual Bollard Bollard2 { get; set; }
        [DataMember]
        public virtual Bollard Bollard3 { get; set; }
        //-- end
        [DataMember]
        public ICollection<SlotOverRidingReasons> SlotOverRidingReasons { get; set; }


        [NotMapped]
        public string MovementName { get; set; }
        [NotMapped]
        public string BerthName { get; set; }
        [NotMapped]
        public string BollardFrom { get; set; }
        [NotMapped]
        public string BollardTo { get; set; }
        [NotMapped]
        public string VesselName { get; set; }
        [NotMapped]
        public string MovementTypeName { get; set; }

	}
}
