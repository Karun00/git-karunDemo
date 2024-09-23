using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models 
{
    public partial class Section625E:EntityBase
    {
        public Section625E()
        {
            this.Section625EDetail = new List<Section625EDetail>();
        }

        public int Section625EID { get; set; }
        public int Section625ABCDID { get; set; }
        public System.DateTime IncidentDateTime { get; set; }
        public System.DateTime TimeReported { get; set; }
        public string OwnerNameofStolenItem { get; set; }
        public string OwnerAddress { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public Nullable<System.DateTime> IDWhenandWhereStolenDateTime { get; set; }
        public string IDWhenandWhereStolenLocation { get; set; }
        public Nullable<System.DateTime> IDWhenWasDiscoveredDateTime { get; set; }
        public string IDWhenWasDiscoveredLocation { get; set; }
        public string TheftOccur { get; set; }
        public string StolenFromBuilding { get; set; }
        public string ISPSBreach { get; set; }
        public string ProtectTheft { get; set; }
        public string Circumstances { get; set; }
        public string TheftAvoided { get; set; }
        public string PoliceAdviced { get; set; }
        public string SAPSOBNumber { get; set; }
        public string PoliceStationReportedTo { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int Hour24Report625ID { get; set; }
        public  Hour24Report625 Hour24Report625 { get; set; }
        public  Section625ABCD Section625ABCD { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  ICollection<Section625EDetail> Section625EDetail { get; set; }
    }
}
