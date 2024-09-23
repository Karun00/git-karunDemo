using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    public class BerthPreSchedulingVO
    {
        public int VesselCallID { get; set; }
        public int VesselCallMovementID { get; set; }
        public int ServiceRequestID { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string MovementStatus { get; set; }
        public Boolean IsScheduleStatus { get; set; }
        public string ETB { get; set; }
        public string ETUB { get; set; }
        public string SheduledBerth { get; set; }
        public string FromBollardCode { get; set; }
        public string ToBollardCode { get; set; }
        public decimal FromBollardMeter { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
        public string VCN { get; set; }
    }

    public class BerthPreSchedulingReferenceVO
    {
        public List<AgentData> Agents { get; set; }
        public List<SubCategoryCodeNameVO> VesselType { get; set; }
        public List<SubCategoryCodeNameVO> ReasonForVisit { get; set; }
        public List<SubCategoryCodeNameVO> CargoType { get; set; }
        public List<SubCategoryCodeNameVO> MovementStatus { get; set; }


    }

    public class AgentData
    {
        public int AgentID { get; set; }
        public string RegisteredName { get; set; }
    }

    public class VCMData
    {


        public int AgentID { get; set; }
        public int VesselCallID { get; set; }
        public int VesselCallMovementID { get; set; }
        public Nullable<int> ServiceRequestID { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public Boolean isDryDock { get; set; }
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public string VesselTypeCode { get; set; }
        public string Agent { get; set; }
        //public Nullable<long> LengthOverallInM { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
        public string MaxDraft { get; set; }
        public string IMDG { get; set; }
        public Nullable<System.DateTime> NominationDate { get; set; }
        public System.DateTime ETA { get; set; }
        public System.DateTime ETD { get; set; }
        public Nullable<System.DateTime> ETB { get; set; }
        public Nullable<System.DateTime> ETUB { get; set; }
        public string PreferredBerth { get; set; }
        public string AlternateBerth { get; set; }
        public string ReasonforVisit { get; set; }
        public string ReasonforAlternateBerth { get; set; }
        public List<string> CargoType { get; set; }
        public string CargoTypeName { get; set; }
        public List<ArrivalCommodity> ArrivalCommodities { get; set; }
        public string Berth { get; set; }
        public string MovementStatus { get; set; }
        public string MovementType { get; set; }
        public string MovementTypeName { get; set; }
        public string VesselColor { get; set; }
        public string ReasonForVisitName { get; set; }
        public System.DateTime CurrentDate { get; set; }
        public int NoofTimesETAChanged { get; set; }
        public List<String> VesselArrested { get; set; }
        public Boolean isVesselArrested { get; set; }
        public System.DateTime BerthTime { get; set; }
        public System.DateTime UnBerthTime { get; set; }
        public List<string> ArrivalReasons { get; set; }

        //-- Added by sandeep on 10-06-2015
        public string IsTidal { get; set; }
        //-- end

    }

    public class VesselData
    {
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public string VesselType { get; set; }
        // public Nullable<long> LengthOverallInM { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
        public decimal ArrDraft { get; set; }
        public decimal MaxDraft { get; set; }
        public decimal DeptDraft { get; set; }
        public string PreferredBerth { get; set; }
        public string AlternateBerth { get; set; }
        public List<string> ArrivalReasons { get; set; }
        public string CargoType { get; set; }
        public string Tidal { get; set; }
        public string VesselTypeCode { get; set; }
        public string BerthCode { get; set; }
        public Nullable<System.DateTime> ETB { get; set; }
        public Nullable<System.DateTime> ETUB { get; set; }
        public List<String> ArrivalCommodities { get; set; }

    }

    public class BerthPlanningTableReferenceVO
    {
        public List<BerthVO> Berths { get; set; }
        public List<QuayVO> Quays { get; set; }
        public List<SubCategoryCodeNameVO> VesselStatuses { get; set; }

    }

    public class VCMTableData
    {

        public int VesselCallID { get; set; }
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public string Agent { get; set; }
        // public Nullable<long> LengthOverallInM { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
        public string MaxDraft { get; set; }
        public string IMDG { get; set; }
        public Nullable<System.DateTime> NominationDate { get; set; }
        public System.DateTime ETA { get; set; }
        public System.DateTime ETD { get; set; }
        public Nullable<System.DateTime> ETB { get; set; }
        public Nullable<System.DateTime> ETUB { get; set; }
        public System.DateTime BerthTime { get; set; }
        public System.DateTime UnBerthTime { get; set; }
        public string PreferredBerth { get; set; }
        public string AlternateBerth { get; set; }
        public string ReasonforVisit { get; set; }
        public List<string> CargoType { get; set; }
        public string Berth { get; set; }
        public string MovementStatus { get; set; }
        public string MovementType { get; set; }
        public string FromBerth { get; set; }
        public string ToBerth { get; set; }
        public string FromBollard { get; set; }
        public string ToBollard { get; set; }
        public Nullable<System.DateTime> ATB { get; set; }
        public Nullable<System.DateTime> ATUB { get; set; }
        public string CargoTypeName { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public string ReasonForVisitName { get; set; }
        public string MovementTypeName { get; set; }
        public string BerthNameFrom { get; set; }
        public string BerthNameTo { get; set; }

        public string BollardNameFrom { get; set; }
        public string BollardNameTo { get; set; }

        public string DisplayColumns { get; set; }
        public string Result { get; set; }
        public List<String> VesselArrested { get; set; }
        public Boolean isVesselArrested { get; set; }
        public List<string> ArrivalReasons { get; set; }
        public string VesselColor { get; set; }

        //-- Added by sandeep on 29-04-2015
        public string MooringBowBollard { get; set; }

        public string MooringStemBollard { get; set; }       
        //-- end

    }


    public class BerthDataVO
    {
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public string BerthName { get; set; }
        public string ShortName { get; set; }
        public string BerthType { get; set; }
        public decimal Lengthm { get; set; }
        public decimal Draftm { get; set; }
        public decimal TidalDraft { get; set; }
        public List<BerthCargo> BerthCargoes { get; set; }
        public List<BerthVesselType> BerthVesselTypes { get; set; }
        public List<BerthReasonForVisit> BerthReasonForVisits { get; set; }
        public List<int> TerminalOperators { get; set; }
        public List<BollardVO> Bollards { get; set; }
        //public List<BerthMaintenance> BerthMaintenances { get; set; }        
    }

    public class SuitableBerthVO
    {
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public string BerthName { get; set; }
        public decimal Length { get; set; }
        public decimal Draft { get; set; }
        public decimal Lengthm { get; set; }
        public decimal Draftm { get; set; }
        public List<BollardVO> Bollards { get; set; }
        //public Nullable<long> LengthOverallInM { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
        public Nullable<decimal> VesselLength { get; set; }
    }

    public class VesselAvailableData
    {
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public Nullable<long> LengthOverallInM { get; set; }
        public string MaxDraft { get; set; }
        public string PreferredBerth { get; set; }
        public string AlternateBerth { get; set; }
        public string ReasonforVisit { get; set; }
        public string CargoType { get; set; }
        public string Tidal { get; set; }
        public string VesselTypeCode { get; set; }
        public string BerthCode { get; set; }
        public Nullable<System.DateTime> ETB { get; set; }
        public Nullable<System.DateTime> ETUB { get; set; }

    }


}
