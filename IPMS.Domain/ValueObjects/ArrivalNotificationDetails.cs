using Core.Repository;
using System;

namespace IPMS.Domain.ValueObjects
{
    public class ArrivalNotificationDetails : EntityBase
    {

        public string VCN { get; set; }
        public string PortCode { get; set; }
        public string VesselName { get; set; }
        public string IMONo { get; set; }
        public System.DateTime ETA { get; set; }
        public System.DateTime ETD { get; set; }
        public string PreferredPortCode { get; set; }
        public string PreferredQuayCode { get; set; }
        public string PreferredBerthCode { get; set; }
        public Nullable<System.DateTime> NominationDate { get; set; }
        public string ReasonForVisit { get; set; }
        public string RecordStatus { get; set; }
        public string VesselType { get; set; }
        public int Quantity { get; set; }
        public string PreferredBerthName { get; set; }
        public string AlternateBerthName { get; set; }
    }

}
