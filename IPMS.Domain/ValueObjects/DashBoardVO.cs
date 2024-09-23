using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class DashBoardVO
    {

        [DataMember]
        public System.Nullable<int> BerthOccupancy { get; set; }
        [DataMember]
        public System.Nullable<int> BerthUtilization { get; set; }
        [DataMember]
        public System.Nullable<decimal> ResourceUtilization { get; set; }
        [DataMember]
        public System.Nullable<int> VesselMovements { get; set; }
        [DataMember]
        public string CommodityHandle { get; set; }
        [DataMember]
        public System.Nullable<int> DelayStatistics { get; set; }
        [DataMember]
        public System.Nullable<int> ISPSClearance { get; set; }
        [DataMember]
        public System.Nullable<int> ISPSClearance_NC { get; set; }
        [DataMember]
        public System.Nullable<int> ISPSClearance_Ex { get; set; }
        [DataMember]
        public System.Nullable<int> SafetyStatistics { get; set; }
         [DataMember]
        public System.Nullable<int> SafetyStatistics_DF { get; set; }
         [DataMember]
         public System.Nullable<int> SafetyStatistics_ADC { get; set; }
         [DataMember]
         public System.DateTime fromDate { get; set; }
         [DataMember]
         public System.DateTime toDate { get; set; }



    }


    public class WegoDashBoardVO
    {
        public string WegoKPI { get; set; }
        public string Automotive { get; set; }        
        public string BreakBulk { get; set; }
        public string Bulk { get; set; }
        public string Container { get; set; }
        public string LiquidBulk { get; set; }
        public string NonOperational	{ get; set; }
        public string Bunkers { get; set; }
        public string Passengers { get; set; }
        public string ALL { get; set; }
        public double Test { get; set; }

        public ICollection<BerthVO> Berths { get; set; } 


    }
    public class TotalMovementsDashBoardVO
    {
        public string MovementType { get; set; }
        public Nullable<decimal> CapeTown { get; set; }
        public Nullable<decimal> Durban { get; set; }
        public Nullable<decimal> EastLondon { get; set; }
        public Nullable<decimal> MosselBay { get; set; }
        public Nullable<decimal> Ngqura { get; set; }
        public Nullable<decimal> PortElizabeth { get; set; }
        public Nullable<decimal> RichardsBay { get; set; }
        public Nullable<decimal> SaldanhaBay { get; set; }
        public Nullable<decimal> Total { get; set; }
    }

    public class GetAllPorts
    {
        public string PortCode { get; set; }
        public string PortName { get; set; }
    }
     public class WegoVesselDetailsVO
     {        
         public string CargoCode { get; set; }
         public string CargoType { get; set; }
         public decimal GRT { get; set; }
         public decimal LOA { get; set; }         

         public decimal STAT { get; set; }

         public decimal VesselDelayAnchorage { get; set; }

         public decimal NPAManueringTime { get; set; }

         public decimal PilotageIn { get; set; }

         public decimal MarineServiceTimeIn { get; set; }

         public decimal AdherenceRequested { get; set; }

         public decimal MarineServiceTimeOut { get; set; }

         public decimal Volumes { get; set; }
         public decimal StartEndCargo { get; set; }
         public decimal LastLineOffFirstLineIn { get; set; }
         public decimal PreCargoWorking { get; set; }
         public decimal WorkingTime { get; set; }
         public decimal DepartureWaiting { get; set; }

         public decimal ShipWorkingHour { get; set; }
         public decimal BerthProductivity { get; set; }
         public decimal ShipProductivityIndicator { get; set; }  
         
     }

     public class WegoVesselVCNVO
     {
         public string CargoType { get; set; }
         public string NoofVessels { get; set; }
         public string GRT { get; set; }
         public string LOA { get; set; }
         public string STAT { get; set; }
         public string VesselDelayAnchorage { get; set; }
         public string NPAManueringTime { get; set; }
         public string PilotageIn { get; set; }
         public string MarineServiceTimeIn { get; set; }
         public string AdherenceRequested { get; set; }
         public string MarineServiceTimeOut { get; set; }        

         public string ShipWorkingHour { get; set; }
         public string BerthProductivity { get; set; }
         public string ShipProductivityIndicator { get; set; }
         public string TotalVolumes { get; set; }
         public string ParcelSizes { get; set; }
         public string PreCargoWorking { get; set; }
         public string WorkingTime { get; set; }
         public string DepartureWaiting { get; set; }

     }
     public class WegoVesselCountVO
     {
         public int VesselAutomative { get; set; }
         public int VesselBreakBulk { get; set; }
         public int VesselContainer { get; set; }
         public int VesselBulk { get; set; }
         public int VesselLiquidBulk { get; set; }
         public int VesselNonOperational { get; set; }
         public int VesselBunkers { get; set; }
         public int VesselPassengers { get; set; }

     }

     public class WegoBerthUtilizationVO
     {
         public string BerthName { get; set; }
         public int NoofVessels { get; set; }
         public string AnchorageWaitingTime { get; set; }
         public string STAT { get; set; }
         public string PilotageTimeIn { get; set; }
         public string PreCargoWorking { get; set; }
         public string VesselWorkingTime { get; set; }
         public string PostCargoWorkingTime { get; set; }
         public string PilotageTimeOut { get; set; }         

     }
    //CargoTypeDashboard
     public class CargoTypeDashboardVO
     {
         public string SubCatName { get; set; }
         public int ONBERTH { get; set; }
         public int PLANNEDMOVEMENTS { get; set; }
         public int ANCHORAGE { get; set; }


     }
     
     [DataContract]
     public class PlannedMovementsDtlsVO
     {
         [DataMember]
         public int? PlannedMovementsCount { get; set; }
         [DataMember]
         public int? PlannedMovtsShiftingCount { get; set; }
         [DataMember]
         public int? PlannedMovtsWarpingCount { get; set; }
         [DataMember]
         public int? PlannedMovtsSailingCount { get; set; }
         [DataMember]
         public int? PlannedMovtsArrivalCount { get; set; }
     }

     [DataContract]
     public class AnchorageDtlsVO
     {
         [DataMember]
         public System.DateTime AnchorDropTime { get; set; }
         [DataMember]
         public Nullable<System.DateTime> AnchorAweighTime { get; set; }
         [DataMember]
         public string BearingDistanceFromBreakWater { get; set; }
         
         [DataMember]
         public string Reason { get; set; }
         [DataMember]
         public string Vesselname { get; set; }
         [DataMember]
         public Nullable<System.DateTime> ATA { get; set; }
         [DataMember]
         public string CargoType { get; set; }
         [DataMember]
         public string VesselType { get; set; }
         [DataMember]
         public string VCN { get; set; }
         [DataMember]
         public string Reasonforvisit { get; set; }
         [DataMember]
         public string PortCode { get; set; }
         [DataMember]
         public Dictionary<string, int> LstofAnchorage { get; set; }
        
     }
     [DataContract]
     public class PortWiseCountVO
     {
          [DataMember]
         public int? AnchorCount { get; set; }
         [DataMember]
           public int? BerthCount { get; set; }
         [DataMember]
          public int? Sailed { get; set; }
         [DataMember]
         public string BerthName { get; set; }
         [DataMember]
         public string Cargoname { get; set; }
         
     }

}
