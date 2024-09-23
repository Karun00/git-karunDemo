using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class ResourceAllocationVO
    {
        public int ResourceAllocationID { get; set; }
        public string ServiceReferenceType { get; set; }
        public int ServiceReferenceID { get; set; }
        public string ServiceTypeCode { get; set; }
        public string ServiceTypeName { get; set; }
        public Nullable<int> ResourceID { get; set; }
        public string ResourceType { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public Nullable<DateTime> ActualScheduledTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }
        public string TaskStatus { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ServiceReferenceTypeName { get; set; }
        public string VCN { get; set; }
        public string OperationTypeName { get; set; }
        public string ResourceTypeName { get; set; }
        public string TaskStatusName { get; set; }
        public Nullable<System.DateTime> AcknowledgeDate { get; set; }
        public string Remarks { get; set; }
        public ShiftingBerthingTaskExecutionVO ShiftingBerthingTaskExecution { get; set; }
        public PilotageServiceRecordingVO PilotageServiceRecording { get; set; }
        public OtherServiceRecordingVO OtherServiceRecording { get; set; }

        // FOR SHIFTING  
        public int BerthingTaskExecutionID { get; set; }
        public int ShiftResourceAllocationID { get; set; }
        public System.DateTime ShiftStartTime { get; set; }
        public System.DateTime ShiftEndTime { get; set; }
        public string FromBerthPortCode { get; set; }
        public string FromBerthQuayCode { get; set; }
        public string FromBerthCode { get; set; }
        public string ToBerthPortCode { get; set; }
        public string ToBerthQuayCode { get; set; }
        public string ToBerthCode { get; set; }
        public string BerthingSide { get; set; }
        public string FromBollardPortCode { get; set; }
        public string FromBollardQuayCode { get; set; }
        public string FromBollardBerthCode { get; set; }
        public string FromBollardCode { get; set; }
        public string ToBollardPortCode { get; set; }
        public string ToBollardQuayCode { get; set; }
        public string ToBollardBerthCode { get; set; }
        public string ToBollardCode { get; set; }
        public string MooringBollardBowPortcode { get; set; }
        public string MooringBollardBowQuayCode { get; set; }
        public string MooringBollardBowBerthCode { get; set; }
        public string MooringBollardBowBollardCode { get; set; }
        public string MooringBollardStemPortcode { get; set; }
        public string MooringBollardStemQuayCode { get; set; }
        public string MooringBollardStemBerthCode { get; set; }
        public string MooringBollardStemBollardCode { get; set; }
        public System.DateTime FirstLineIn { get; set; }
        public System.DateTime LastLineIn { get; set; }
        public System.DateTime FirstLineOut { get; set; }
        public System.DateTime LastLineOut { get; set; }
        public string ForwardDraft { get; set; }
        public string AftDraft { get; set; }
        public string Deficiencies { get; set; }
        public string ShiftRecordStatus { get; set; }
        public int ShiftCreatedBy { get; set; }
        public System.DateTime ShiftCreatedDate { get; set; }
        public int ShiftModifiedBy { get; set; }
        public System.DateTime ShiftModifiedDate { get; set; }

        // -- Added by sandeep on 22-09-2014
        public string AllocSlot { get; set; }
        public string VesselName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public bool IsCraft { get; set; }
        public Nullable<int> CraftId { get; set; }
        public string CraftName { get; set; }
        public string TugResourceName { get; set; }
        public string MovementType { get; set; }
        public string BerthCode { get; set; }
        public string BerthName { get; set; }
        public long Quantity { get; set; }
        public DateTime AllocationDate { get; set; }
        // -- end

        public string OperationType { get; set; }
        public string SlotTime { get; set; }
        public string ResourceName { get; set; }

        public string AnyDangerousGoodsonBoard { get; set; }
        public Nullable<decimal> VesselLength { get; set; }
        public string BerthKey { get; set; }

        //-- Added by sandeep on 20-04-2015
        public string MovementTypeName { get; set; }
        //-- end

        //-- Added by sandeep on 15-05-2015

        public string VesselType { get; set; }
        public DateTime ETA { get; set; }
        public string ReasonForVisit { get; set; }
        public string CargoType { get; set; }
        public decimal LOA { get; set; }
        public decimal GRT { get; set; }
        public decimal DWT { get; set; }
        public string CurrentBerth { get; set; }
        public string ToBerth { get; set; }
        public Nullable<DateTime> MovementDateTime { get; set; }
        public decimal Beam { get; set; }
        public string ArrivalDraft { get; set; }
        public string TidalCondition { get; set; }
        public string DayLightCondition { get; set; }
        public string FromBollard { get; set; }
        public string ToBollard { get; set; }
        public string MovementTypeCode { get; set; }
        public string MovementStatus { get; set; }
        public string SideAlongSide { get; set; }
        //-- end
        //public string IsCompleted { get; set; }
        //Added by divy ato display grid fro water
        public int OtherServiceRecordingID { get; set; }
        public Nullable<System.DateTime> WaterStartTime { get; set; }

        public Nullable<System.DateTime> WaterEndTime { get; set; }
       
        public Nullable<decimal> OpeningMeterReading { get; set; }
        
        public Nullable<decimal> ClosingMeterReading { get; set; }
        
        public Nullable<decimal> TotalDispensed { get; set; }       
        
       
        public string DelayReason { get; set; }
        
        public Nullable<System.DateTime> WaitingStartTime { get; set; }
        
        public Nullable<System.DateTime> WaitingEndTime { get; set; }
        
        public string IsCompleted { get; set; }

        public string MeterNo { get; set; }

        public int IsTop { get; set; }

        public string action { get; set; }

       
    }
}


