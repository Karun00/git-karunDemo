using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.Runtime.Serialization;


namespace IPMS.Domain.ValueObjects
{


    public class VesselCallAnchorageVO
    {
        [DataMember]
        public int VesselCallAnchorageID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string AnchorDropTime { get; set; }
        [DataMember]
        public string AnchorAweighTime { get; set; }
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
        public string VesselName { get; set; }
        [DataMember]
        public string PortCode { get; set; }


        [DataMember]
        public Nullable<System.DateTime> BreakWaterIn { get; set; }
        [DataMember]
        public Nullable<System.DateTime> BreakWaterOut { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PortLimitIn { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PortLimitOut { get; set; }
        [DataMember]
        public System.DateTime AnchorDropDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> AnchorAweighDateTime { get; set; }
    }
}
