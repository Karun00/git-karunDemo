using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class VesselGrabVO
    {
        [DataMember]
        public int VesselGrabID { get; set; }
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public Nullable<long> GrabTypeM { get; set; }
        [DataMember]
        public Nullable<long> SafeWorkingLoad { get; set; }
        [DataMember]
        public Nullable<long> GrabCapacityCBM { get; set; }
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
