using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class VesselHatchHoldVO
    {
        [DataMember]
        public int VesselHatchHoldID { get; set; }
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public Nullable<long> HatchHoldTypeM { get; set; }
        [DataMember]
        public Nullable<long> SafeWorkingLoad { get; set; }
        [DataMember]
        public Nullable<long> HoldCapacityCBM { get; set; }
        [DataMember]
        public string Description { get; set; }
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
        //[DataMember]
        //public virtual User User { get; set; }
        //[DataMember]
        //public virtual User User1 { get; set; }
        //[DataMember]
        //public virtual Vessel Vessel { get; set; }
    }
}
