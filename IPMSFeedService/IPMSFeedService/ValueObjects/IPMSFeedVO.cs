using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPMSFeedService.ValueObjects
{
    public class IPMSFeedVO
    {
        //NOTE: Naming conventions (field names and case) and format should maintain ASIS should be same as IPMS FEED service

        public string MovementDateTime { get; set; }
        public string VCN { get; set; }
        public string ServiceRequestID { get; set; }
        public string MovementTyp { get; set; }
        public string VesselName { get; set; }
        public string VsslTyp { get; set; }
        public string Breakwaterin { get; set; }
        public string PortLimitOut { get; set; }

        public string BerthName { get; set; }
        public string PortCode { get; set; }

        public string ReasonForVisit { get; set; }
        public string PortName { get; set; }

        public string LastPortOfCall { get; set; }
        public string NextPortOfCall { get; set; }

        public string CallSign { get; set; }
        public string GrossRegisteredTonnageInMT { get; set; }

        public string DeadWeightTonnageInMT { get; set; }
        public string LengthOverallInM { get; set; }


        public string forwarddraft { get; set; }
        public string afterdraft { get; set; }
        public string BreakWaterOut { get; set; }
        public string PortLimitIn { get; set; }
        public string RegisteredName { get; set; }
        public string BERTH1 { get; set; }
        public string Pilot { get; set; }
        public string CraftNationality { get; set; }
        public string Tug { get; set; }

        public string IMONo { get; set; }
        public string Berthmaster1 { get; set; }
        public string FirstLineInTime1 { get; set; }
        public string LastLineInTime1 { get; set; }
        public string Berthmaster2 { get; set; }
        public string FirstLineInTime2 { get; set; }
        public string LastLineInTime2 { get; set; }
        public string pilt1 { get; set; }
        public string PilotOnBoardTime1 { get; set; }
        public string PilotOfftime1 { get; set; }
        public string pilt2 { get; set; }
        public string PilotOnBoardTime2 { get; set; }
        public string PilotOfftime2 { get; set; }
        public string Tug1 { get; set; }
        public string TugStartTime1 { get; set; }
        public string TugEndTime1 { get; set; }
        public string Tug2 { get; set; }
        public string TugStartTime2 { get; set; }
        public string TugEndTime2 { get; set; }
        public string Tug3 { get; set; }
        public string TugStartTime3 { get; set; }
        public string TugEndTime3 { get; set; }
        public string Sno { get; set; }
    }

    public class IPMSANFeedVO
    {
        public string MovementDateTime { get; set; }
        public string VCN { get; set; }
        public string IMONo { get; set; }
        public string CallSign { get; set; }

        public string ServiceRequestID { get; set; }
        public string MovementTyp { get; set; }
        public string VesselName { get; set; }
        public string RegisteredName { get; set; }
        public string ETA { get; set; }
        public string ETD { get; set; }
        public string AlternateBerthName { get; set; }
        public string PreferredBerthName { get; set; }
        public string ETB { get; set; }
        public string ETUB { get; set; }
        public string VsslType { get; set; }

        public string Reasonforvisit { get; set; }
        public string Terminalname { get; set; }
        public string UOM { get; set; }
        public int? Quantity { get; set; }
        public string Commodity { get; set; }
        public int? Moves { get; set; }
        public string VoyageIn { get; set; }
        public string VoyageOut { get; set; }
        public string Agentname { get; set; }
        public string TerminalOperatorName { get; set; }

    }

    public class IPMSLocationVO
    {
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public string IMONo { get; set; }
        public string ATA { get; set; }
        public string ATD { get; set; }
        public string Breakwaterin { get; set; }
        public string Breakwaterout { get; set; }
        public string Portlimitin { get; set; }
        public string Portlimitout { get; set; }
        public string Anchordroptime { get; set; }
        public string Anchoraweightime { get; set; }
        public string Anchorposition { get; set; }
        public string Reason { get; set; }

    }
}