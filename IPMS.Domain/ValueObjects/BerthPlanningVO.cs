using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class BerthPlanningVO
    {
        public int VesselCallMovementID { get; set; }
        public string VCN { get; set; }
        public string BerthName { get; set; }
        public DateTime ETA { get; set; }
        public DateTime ETD { get; set; }
        public Nullable<System.DateTime> ETB { get; set; }
        public Nullable<System.DateTime> ETUB { get; set; }
        public Nullable<System.DateTime> ATB { get; set; }
        public Nullable<System.DateTime> ATUB { get; set; }
        public Nullable<System.DateTime> NominationDate { get; set; }
        public System.DateTime BerthTime { get; set; }
        public System.DateTime UnBerthTime { get; set; }
        public string FromPortCode { get; set; }
        public string FromQuayCode { get; set; }
        public string FromBerthCode { get; set; }
        public string FromBollardCode { get; set; }
        public decimal FromBollardMeter { get; set; }
        public string ToPortCode { get; set; }
        public string ToQuayCode { get; set; }
        public string ToBerthCode { get; set; }
        public string ToBollardCode { get; set; }
        public decimal ToBollardMeter { get; set; }
        public decimal? PositionX { get; set; }
        public int PositionY { get; set; }
        public string MovementStatus { get; set; }
        //Added by srinivas
        public string RecordStatus { get; set; }
        public string MovementType { get; set; }
        public string MovementTypeName { get; set; }
        public string QuayCode { get; set; }
        public string PreferredBerth { get; set; }
        public string AlternateBerth { get; set; }
        public string Tidal { get; set; }
        public decimal DeptDraft { get; set; }
        public decimal Draft { get; set; }
        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public Nullable<decimal> LOA { get; set; }
        public Nullable<decimal> VesselLength { get; set; }
        public string IMONo { get; set; }
        public string Agent { get; set; }
        public string BerthNo { get; set; }
        public string BerthingSide { get; set; }
        public string ReasonforVisit { get; set; }
        public string ReasonforVisitName { get; set; }
        public int VesselWidth { get; set; }
        public string DoubleBankedVessel { get; set; }
        public string SideAlongSide { get; set; }
        public string TripleBankedVessel { get; set; }
        public int IsBanked { get; set; }
        public List<String> ArrivalCommodities { get; set; }
        public List<String> VesselArrested { get; set; }
        public Boolean isVesselArrested { get; set; }
        public string ArrivalCommoditiesNames { get; set; }
        public string FromCoordinates { get; set; }
        public string ToCoordinates { get; set; }
        public string FromOffsetCoordinates { get; set; }
        public string ToOffsetCoordinates { get; set; }
        public string FromMidCoordinates { get; set; }
        public string ToMidCoordinates { get; set; }
        //    public string PreviousCoordinates { get; set; }
        public List<String> ArrivalReasons { get; set; }

        //-- Added by sandeep on 29-04-2015
        public string MooringBowBollard { get; set; }

        public string MooringStemBollard { get; set; }
        //-- end
        public string ArrivalCommoditiesString { get; set; }
        public string ArrivalReasonsString { get; set; }

        //-- Added by sandeep on 25-06-2015
        public string FromBollardName { get; set; }
        public string IsTidal { get; set; }
        public string ToBollardName { get; set; }

        //-- end

        public string precoord { get; set; }
        public string preoffsetcord { get; set; }


    }

    public class QuayBerthBollardData
    {
        public string QuayCode { get; set; }
        public string QuayName { get; set; }
        public decimal QuayLength { get; set; }
        public List<BerthsData> Berths { get; set; }
    }

    public class BerthPlanningConfiguration
    {
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public string ConfigLabelName { get; set; }
    }


    public class BerthsData
    {
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public string BerthName { get; set; }
        public string PortName { get; set; }
        public string QuayName { get; set; }
        public string ShortName { get; set; }
        public string BerthType { get; set; }
        public string BerthTypeName { get; set; }
        public decimal QuayLength { get; set; }
        public decimal FromMeter { get; set; }
        public decimal ToMeter { get; set; }
        public decimal Lengthm { get; set; }
        public decimal Draftm { get; set; }
        public string RecordStatus { get; set; }
        public string BerthKey { get; set; }
        public Nullable<decimal> TidalDraft { get; set; }
        public List<String> CargoType { get; set; }
        public string CargoTypeNames { get; set; }
        public List<String> VesselTypes { get; set; }
        public List<String> ReasonsForVisitType { get; set; }
        public List<BollardData> Bollards { get; set; }
        public List<int> TerminalOperators { get; set; }
        public List<BerthMaintenanceVO> BerthMaintainance { get; set; }
    }

    public class BollardData
    {
        public string BollardCode { get; set; }
        public string BollardName { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public decimal QuayLength { get; set; }
        public string BerthCode { get; set; }
        public decimal BerthLength { get; set; }
        public string FromBollardKey { get; set; }
        public string ToBollardKey { get; set; }
        public decimal FromMeter { get; set; }
        public decimal ToMeter { get; set; }
        public string Continous { get; set; }
        public string Coordinates { get; set; }
        public string OffsetCoordinates { get; set; }
        public string MidCoordinates { get; set; }
        //   public string PreviousCoordinates { get; set; }
        public bool ContinousStatus { get; set; }
    }

    public class BerthMaintenanceData
    {
        public int BerthMaintenanceID { get; set; }
        public string PortCode { get; set; }
        public string MaintPortCode { get; set; }
        public string MaintQuayCode { get; set; }
        public string MaintBerthCode { get; set; }
        public string FromBerthCode { get; set; }
        public string FromBollard { get; set; }
        public string ToBerthCode { get; set; }
        public string ToBollard { get; set; }
        public System.DateTime PeriodFrom { get; set; }
        public System.DateTime PeriodTo { get; set; }
        public decimal? PositionX { get; set; }
        public int PositionY { get; set; }
        public decimal? Length { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public decimal FromBollardMeter { get; set; }
        public decimal ToBollardMeter { get; set; }

    }

    public class UserData
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public Boolean isTerminalOperator { get; set; }
        public Boolean isBerthPlanner { get; set; }
        public List<string> Berths { get; set; }

    }

    public class AvailabilityVO
    {

        public string VCN { get; set; }
        public string FromPortCode { get; set; }
        public string FromQuayCode { get; set; }
        public string FromBerthCode { get; set; }
        public string FromBollardCode { get; set; }
        public decimal FromBollardMeter { get; set; }
        public decimal ToBollardMeter { get; set; }
        public string ToPortCode { get; set; }
        public string ToQuayCode { get; set; }
        public string ToBerthCode { get; set; }
        public string ToBollardCode { get; set; }
        public System.DateTime BerthTime { get; set; }
        public System.DateTime UnBerthTime { get; set; }
    }


    public class ConflictingData
    {
        public List<AvailabilityVO> VesselsColliding { get; set; }
        public List<BerthMaintenanceData> MaintainenceData { get; set; }
    }


    public class PreviousBollardData
    {
        public string BollardCode { get; set; }
        public decimal FromMeter { get; set; }
        public decimal ToMeter { get; set; }
        public string Coordinates { get; set; }
        public string OffsetCoordinates { get; set; }

        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }

    }

    public class GISMapPathVo
    {
        public string mapPath { get; set; }
        public string geographicLocation { get; set; }

    }
    public class BerthedVessels
    {

        public string VesselName { get; set; }
        public string VCN { get; set; }
        public string ReasonforVisitName { get; set; }
        public string  ATA { get; set; }
        public string ATB { get; set; }
        public string BerthName { get; set; }
        public string AgentName { get; set; }
    
    }
    public class AnchoredVessels
    {

        public string VesselName { get; set; }
        public string VCN { get; set; }
        public string AnchorageReason { get; set; }
        public string AnchorPosition { get; set; }
        public string BearingDistanceFromBreakWater { get; set; }
        public string AgentName { get; set; }

    }
    public class SailedVessels
    {

        public string VesselName { get; set; }
        public string VCN { get; set; }
        public string ATUB { get; set; }
        public string ATD { get; set; }
        public string PortName { get; set; }
        public string AgentName { get; set; }

    }
    public class AnchorVesselInfoGISVO
    {

        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public Nullable<decimal> DeadWeightTonnageInMT { get; set; }
        public string IMONo { get; set; }
        public string VCN { get; set; }
        public string NextPortOfCall { get; set; }
        public DateTime ETA { get; set; }
        public DateTime ETD { get; set; }
        public string ReasonforvisitName { get; set; }
        public string CargoTypes { get; set; }
        public string ArrDraft { get; set; }
        public string DepDraft { get; set; }
        public string LastPortOfCall { get; set; }
        public string Reason { get; set; }
        public string AnchorPosition { get; set; }
        public string BearingDistanceFromBreakWater { get; set; }
        public string PortOfRegistry { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
        public Nullable<decimal> BeamInM { get; set; }
        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }
        public string VesselNationality { get; set; }
        public string AgentName { get; set; }
        public long VesselBuildYear { get; set; }
        public string PortCode { get; set; }
        public string Cooordinates { get; set; }

    }

}
