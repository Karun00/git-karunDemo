using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using IPMS.Domain.Models;


namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class SlotOverRidingReasonsVO
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
        public string PreviousSlot { get; set; }
        [DataMember]
        public string OverriddenSlot { get; set; }
        [DataMember]
        public Nullable<DateTime> PreviousSlotDate { get; set; }
        //[DataMember]
        //public Nullable<DateTime> OverriddenSlotDate { get; set; }

        [DataMember]
        public string PreviousSlotDis { get; set; }
       
        
    }
}

