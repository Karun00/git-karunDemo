using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace IPMS.Domain.Models
{
     [DataContract]
    public partial class SlotOverRidingReasons : EntityBase
    {
        [DataMember]
         public int OverRideSlotID { get; set; }
        [DataMember]
        public int VesselCallMovementID { get; set; }      
        [DataMember]
        public string ReasonCode { get; set; }       
        [DataMember]
        public System.DateTime EnteredDateAndTime { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public User User1 { get; set; }
        [DataMember]
        public VesselCallMovement VesselCallMovement { get; set; }
        [DataMember]
        public SubCategory SubCategory { get; set; }
        [DataMember]
        public string PreviousSlot { get; set; }
        [DataMember]
        public string OverriddenSlot { get; set; }
        [DataMember]
        public Nullable<DateTime> PreviousSlotDate { get; set; }
        //[DataMember]
        //public Nullable<DateTime> OverriddenSlotDate { get; set; }

    }
}
