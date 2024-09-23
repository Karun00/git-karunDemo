using System;

namespace IPMS.Domain.ValueObjects
{
    public class VoyageMonitoringVO
    {
        public string VCN { get; set; }

        public int VesselID { get; set; }

        public string VesselName { get; set; }

        public string ReasonForVisit { get; set; }

        public string VesselType { get; set; }

        public string CallSign { get; set; }

        public Nullable<System.DateTime> ATA { get; set; }

        public Nullable<System.DateTime> ATD { get; set; }

        public Nullable<System.DateTime> ETA { get; set; }

        public Nullable<System.DateTime> ETD { get; set; }

        public string IMONo { get; set; }

        public Nullable<decimal> LengthOverallInM { get; set; }

        public Nullable<decimal> BeamInM { get; set; }

        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }

        public Nullable<decimal> DeadWeightTonnageInMT { get; set; }

        public string LastPortOfCall { get; set; }

        public string NextPortOfCall { get; set; }

        public string Tidal { get; set; }

        public string DaylightRestriction { get; set; }

        public string VesselNationality { get; set; }

        public string ArrDraft { get; set; }

        public string DepDraft { get; set; }

        public string VoyageIn { get; set; }

        public string VoyageOut { get; set; }

        public string PortRestriction { get; set; }

        public string CargoType { get; set; }

        public string Terminal { get; set; }

        public Nullable<System.DateTime> NominationDate { get; set; }

        //Change ATA/ATD details
        public Nullable<System.DateTime> NewATA { get; set; }

        public Nullable<System.DateTime> NewATD { get; set; }

        public string ChangeReason { get; set; }

        //Service Request details        
        public string MovementName { get; set; }

        public string SRStatus { get; set; }

        public System.DateTime RequestDatetime { get; set; }

        //Anchorage Details
        public Nullable<System.DateTime> PortLimitEnterTime { get; set; }
        public Nullable<System.DateTime> AnchorageDownTime { get; set; }
        public Nullable<System.DateTime> AnchorageUpTime { get; set; }
        public string BearingDistanceFromBreakWater { get; set; }
       
        public string BreakWaterInTime { get; set; }
        public Nullable<System.DateTime> BreakWaterIn { get; set; }
        public Nullable<System.DateTime> BreakWaterOut { get; set; }
        public Nullable<System.DateTime> PortLimitIn { get; set; }
        public Nullable<System.DateTime> PortLimitOut { get; set; }

        public virtual ArrivalNotificationVO ArrivalNotification { get; set; }
        public virtual VesselVO Vessel { get; set; }
        public virtual VesselCallVO VesselCalls { get; set; }

        //Port
        public string portcode { get; set; }

        public int? VesselETAChangeID { get; set; }

        public Nullable<System.DateTime> ATB { get; set; }
        public Nullable<System.DateTime> ATUB { get; set; }
        public string BerthName { get; set; }
        public string VCNVesselName { get; set; }
    }
}
