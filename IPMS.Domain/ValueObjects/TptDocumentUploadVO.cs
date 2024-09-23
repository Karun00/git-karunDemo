using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IPMS.Domain.ValueObjects
{
    [CollectionDataContract]
    [Serializable]
    public class RowCollection : List<DataRow>
    {
        [DataMember]
        public DataRow Row { get; set; }
    }

    [DataContract]
    public class TerminalDelaysVO 
    {  
        [DataMember]
        public int TerminalDelayID { get; set; }        
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string TerminalOperator { get; set; }
        [DataMember]
        public System.DateTime ArrivalDate { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public string  Voyage { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string Terminal { get; set; }
        [DataMember]
        public string CargoType { get; set; }
        [DataMember]
        public string ReasonForDelay { get; set; }
        [DataMember]
        public Nullable<decimal> DelayDuration { get; set; }
        [DataMember]
        public string   UnitOfMeasure { get; set; }
        [DataMember]
        public string Comments { get; set; }         
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string ErrorStatus { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    [DataContract]
    public class OutTurnVolumesVO
    {
        [DataMember]
        public int OutTurnVolumeID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string TerminalOperator { get; set; }
        [DataMember]
        public System.DateTime ArrivalDate { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public string Voyage { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string Terminal { get; set; }
        [DataMember]
        public string CargoType { get; set; }
        [DataMember]
        public Nullable<decimal> OutTurnVolume { get; set; }
        [DataMember]
        public string UnitOfMeasure { get; set; }
        [DataMember]
        public System.DateTime FirstCraneSwing { get; set; }
        [DataMember]
        public System.DateTime LastCraneSwing { get; set; }
        [DataMember]
        public int NoOfCranes { get; set; }       
        [DataMember]
        public string Comments { get; set; }  
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public string ErrorStatus { get; set; }
    }

    [DataContract]
    public class TerminalWeeklyDataVO
    {
        [DataMember]
        public int TerminalDataID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string Terminal { get; set; }
        [DataMember]
        public string TerminalOperator { get; set; }
        [DataMember]
        public int WeekNo { get; set; }
        [DataMember]
        public DateTime WeekEnding { get; set; }
        [DataMember]
        public string PerformanceArea { get; set; }
        [DataMember]
        public string Measure { get; set; }
        [DataMember]
        public string UnitOfMeasure { get; set; }        
        [DataMember]
        public string CargoType { get; set; }
        [DataMember]
        public decimal Planned { get; set; }
        [DataMember]
        public decimal Actual { get; set; }      
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string ErrorStatus { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    [DataContract]
    public class RailPlanVO
    {
        [DataMember]
        public int RailPlanID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string Terminal { get; set; }
        [DataMember]
        public string Corridor { get; set; }
        [DataMember]
        public DateTime PlannedDate { get; set; }
        [DataMember]
        public int SlNo { get; set; }
        [DataMember]
        public int Schedule { get; set; } 
        [DataMember]
        public int TrainNo { get; set; }
        [DataMember]
        public string Origin { get; set; }
        [DataMember]
        public string Destination { get; set; }
        [DataMember]
        public string BreakType { get; set; }
        [DataMember]
        public string PlannedETD { get; set; }
        [DataMember]
        public string PlannedETA { get; set; }
        [DataMember]
        public string Loco { get; set; }
        [DataMember]
        public string LocoQty { get; set; }
        [DataMember]
        public string NWBRef { get; set; }
        [DataMember]
        public string PlannedTons { get; set; }
        [DataMember]
        public string Load { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string YQ { get; set; }
        [DataMember]
        public string TrainStatus { get; set; }
        [DataMember]
        public string ReasonForChange { get; set; }
        [DataMember]
        public string NewETD { get; set; }
        [DataMember]
        public string NewETA { get; set; }
        [DataMember]
        public string  TrainMovement { get; set; }
        [DataMember]
        public string ATD { get; set; }
        [DataMember]
        public string ATA { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    [DataContract]
    public class TrainMonitoringVO
    {
        [DataMember]
        public int RailPlanID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string Terminal { get; set; }
        [DataMember]
        public string Corridor { get; set; }
        [DataMember]
        public DateTime PlannedDate { get; set; }
        [DataMember]
        public int SlNo { get; set; }
        [DataMember]
        public int Schedule { get; set; }
        [DataMember]
        public int TrainNo { get; set; }
        [DataMember]
        public string Origin { get; set; }
        [DataMember]
        public string Destination { get; set; }
        [DataMember]
        public string BreakType { get; set; }
        [DataMember]
        public string PlannedETD { get; set; }
        [DataMember]
        public string PlannedETA { get; set; }
        [DataMember]
        public string BUSINESS_UNIT { get; set; }
        [DataMember]
        public string CORRIDOR_NAME { get; set; }
        [DataMember]
        public string SECTION_DESC { get; set; }
        [DataMember]
        public string PLAN_TYPE { get; set; }
               [DataMember]
        public string TRAIN_ROUTE { get; set; }
               [DataMember]
               public string CATEGORY_DESC { get; set; }
               [DataMember]
               public string TRAIN_TYPE { get; set; }
               [DataMember]
               public string OUTBOUND_INBOUND_INDICATOR { get; set; }

               [DataMember]
               public string TOTAL_WAGONS { get; set; }
               [DataMember]
               public string ACTUAL_TRAIN_MASS { get; set; }
               [DataMember]
               public string DELAY_REASON { get; set; }
               [DataMember]
               public string ETA { get; set; }
               [DataMember]
               public string CANCELLATION_REF_NUMBER { get; set; }
               [DataMember]
               public string CANCELLATION_REASON { get; set; }
               [DataMember]
               public string STAGED_REF_NUMBER { get; set; }
               [DataMember]
               public string STAGED_REASON { get; set; }
               [DataMember]
               public string UPDATE_DATETIME { get; set; }
        //[DataMember]
        //public TimeSpan PlannedETD { get; set; }
        //[DataMember]
        //public TimeSpan PlannedETA { get; set; }
        [DataMember]
        public string Loco { get; set; }
        [DataMember]
        public int LocoQty { get; set; }
        [DataMember]
        public int NWBRef { get; set; }
        [DataMember]
        public int PlannedTons { get; set; }
        [DataMember]
        public string Load { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string YQ { get; set; }
        [DataMember]
        public string TrainStatus { get; set; }
        [DataMember]
        public int ReasonForChange { get; set; }
        [DataMember]
        public TimeSpan NewETD { get; set; }
        [DataMember]
        public TimeSpan NewETA { get; set; }
        [DataMember]
        public int TrainMovement { get; set; }
        [DataMember]
        public string ATD { get; set; }
        [DataMember]
        public string ATA { get; set; }
        //[DataMember]
        //public  Nullable<TimeSpan> ATD { get; set; }
        //[DataMember]
        //public  Nullable<TimeSpan> ATA { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    [DataContract]
    public class WagonDetailsVO
    {
        [DataMember]
        public int TrainNo { get; set; }
    
        [DataMember]
        public int WagonNumber { get; set; }
    [DataMember]
        public string WagonType{ get; set; }
    [DataMember]
    public string Commodity { get; set; }
   [DataMember]
        public int Tonnage{ get; set; }
    [DataMember]
        public DateTime OriginDate{ get; set; }
    [DataMember]
        public string TrainOrigin{ get; set; }
    [DataMember]
    public DateTime UpatedateTime { get; set; }
    }
}
