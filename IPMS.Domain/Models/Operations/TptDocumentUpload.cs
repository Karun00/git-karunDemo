using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class TerminalDelay : EntityBase
    {
        public int TerminalDelayID { get; set; }
        public Nullable<int> VesselId { get; set; }
        public string VCN_No { get; set; }
        public string IMONo { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public System.DateTime ArrivalDate { get; set; }
        public string cargoType { get; set; }
        public string ReasonForDelay { get; set; }
        public string UnitOfMeasure { get; set; }
        public Nullable<decimal> DelayDuration { get; set; }
        public string Comments { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    public partial class TerminalData : EntityBase
    {
        public int TerminalDataID { get; set; }
        public int WeekNo { get; set; }
        public System.DateTime WeekEnding { get; set; }
        public string PerformanceArea { get; set; }
        public string measure { get; set; }
        public string UnitOfMeasure { get; set; }
        public string CargoType { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public decimal Planned_Qty { get; set; }
        public decimal Actual_Qty { get; set; }
        public string Comments { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
    public partial class OutTurnVolume : EntityBase
    {
        public int OutTurnVolumeID { get; set; }
        public Nullable<int> VesselId { get; set; }
        public string VCN_No { get; set; }
        public string IMONo { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public System.DateTime ArrivalDate { get; set; }
        public string cargoType { get; set; }
        public Nullable<decimal> OutturnVolume { get; set; }
        public System.DateTime FirstCraneSwing { get; set; }
        public System.DateTime LastCraneSwing { get; set; }
        public int NoOfCranes { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Comments { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
    public partial class RailPlan : EntityBase
    {
        public int RailPlanNo {get;set;}
        public int Port {get;set;}
        public string Corridor {get;set;}
        public DateTime PlannedDate {get;set;}
        public int Schedule {get;set;}
        public int TrainNo {get;set;}
        public string Origin {get;set; }
        public string Destination {get;set;}
        public string BreakType{get;set;} 
        public string PlannedETD {get;set;}
        public string PlannedETA {get;set;}
        public string Loco {get;set;}
        public string LocoQty {get;set;}
        public string NWBRef {get;set;}
        public string PlannedTons {get;set;}
        public string Load {get;set;}
        public string Remark {get;set;}
        public string YQ {get;set;}
        public string TrainStatus {get;set;}

         
        public string ReasonForChange {get;set;}
        public string NewETD {get;set;}
        public string NewETA {get;set;}
         
        public string TrainMovement {get;set;}
        public string ATD {get;set;}
        public string ATA {get;set;}
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}
