using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class Section625EVO
    {      
        [DataMember]
        public int Section625EID { get; set; }
        [DataMember]
        public int Section625ABCDID { get; set; }
        [DataMember]
        public int Hour24Report625ID { get; set; }
        [DataMember]
        public System.DateTime IncidentDateTime { get; set; }
        [DataMember]
        //public System.DateTime TimeReported { get; set; }
        public string TimeReported { get; set; }
        [DataMember]
        public string OwnerNameofStolenItem { get; set; }
        [DataMember]
        public string OwnerAddress { get; set; }
        [DataMember]
        public string TelephoneNo { get; set; }
        [DataMember]
        public string MobileNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> IDWhenandWhereStolenDateTime { get; set; }
        [DataMember]
        public string IDWhenandWhereStolenLocation { get; set; }
        [DataMember]
        public Nullable<System.DateTime> IDWhenWasDiscoveredDateTime { get; set; }
        [DataMember]
        public string IDWhenWasDiscoveredLocation { get; set; }
        [DataMember]
        public string TheftOccur { get; set; }
        [DataMember]
        public string StolenFromBuilding { get; set; }
        [DataMember]
        public string ISPSBreach { get; set; }
        [DataMember]
        public string ProtectTheft { get; set; }
        [DataMember]
        public string Circumstances { get; set; }
        [DataMember]
        public string TheftAvoided { get; set; }
        [DataMember]
        public string PoliceAdviced { get; set; }
        [DataMember]
        public string SAPSOBNumber { get; set; }
        [DataMember]
        public string PoliceStationReportedTo { get; set; }
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
    }
}
