using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class DivingRequestVO
    {
        [DataMember]
        public string DRN { get; set; }
        [DataMember]
        public string OccupationReason { get; set; }
        [DataMember]
        public virtual string Port { get; set; }
        [DataMember]
        public virtual string LocationorQuay { get; set; }

        [DataMember]
        public virtual string Raisedby { get; set; }
        [DataMember]
        public virtual string Berth { get; set; }
        [DataMember]
        public int DivingRequestID { get; set; }
        [DataMember]
        public string FromPortCode { get; set; }
        [DataMember]
        public string FromQuayCode { get; set; }
        [DataMember]
        public string FromQuayName { get; set; }
        [DataMember]
        public string FromBerthCode { get; set; }
        [DataMember]
        public string FromBerthName { get; set; }
        [DataMember]
        public string FromBollardCode { get; set; }
        [DataMember]
        public string FromBollardName { get; set; }
        [DataMember]
        public string ToPortCode { get; set; }
        [DataMember]
        public string ToQuayCode { get; set; }
        [DataMember]
        public string ToQuayName { get; set; }
        [DataMember]
        public string ToBerthCode { get; set; }
        [DataMember]
        public string ToBerthName { get; set; }
        [DataMember]
        public string ToBollardCode { get; set; }
        [DataMember]
        public string ToBollardName { get; set; }
        [DataMember]
        public string RequiredByDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

        //Modified by prasad     
        [DataMember]
        public string LocationTypeName { get; set; }
        [DataMember]
        public int OtherLocationName { get; set; }
        [DataMember]
        public string QuayLocationName { get; set; }
        [DataMember]
        public int OtherLocation { get; set; }
        [DataMember]
        public string QuayLocation { get; set; }
        [DataMember]
        public string OcupationFromDate { get; set; }
        [DataMember]
        public string OcupationToDate { get; set; }
        [DataMember]
        public Nullable<decimal> HoursOfOccupation1 { get; set; }
        [DataMember]
        public string StartTime { get; set; }
        [DataMember]
        public string StopTime { get; set; }
        [DataMember]
        public string HoursOfOccupation2 { get; set; }
        [DataMember]
        public string DivingReferenceNo { get; set; }
        [DataMember]
        public string SupervisorName { get; set; }
        [DataMember]
        public string DiveTenders { get; set; }
        [DataMember]
        public string LoggedDiveTimeFrom { get; set; }
        [DataMember]
        public string LoggedDiveTimeTo { get; set; }
        [DataMember]
        public string TimeDiveOperationCancelled { get; set; }
        [DataMember]
        public string DiveNature { get; set; }
        [DataMember]
        public Nullable<decimal> DiverDepth { get; set; }
        [DataMember]
        public bool BreathingMixture { get; set; }
        [DataMember]
        public bool CompressedAir { get; set; }
        [DataMember]
        public string DivingEquipmentUsed1 { get; set; }
        [DataMember]
        public string DivingEquipmentUsed2 { get; set; }
        [DataMember]
        public string TimeLeftWorkshop { get; set; }
        [DataMember]
        public string TimeLeftSite { get; set; }
        [DataMember]
        public string TimeArrivedWorkshop { get; set; }
        [DataMember]
        public string TimeArrivedSite { get; set; }
        [DataMember]
        public string DecompressionTables { get; set; }
        [DataMember]
        public string Visibility { get; set; }
        [DataMember]
        public string SeaCondition { get; set; }
        [DataMember]
        public string UnderWaterCurrents { get; set; }
        [DataMember]
        public string ContaminatedWater { get; set; }
        [DataMember]
        public Nullable<decimal> WaterTemperature { get; set; }
        [DataMember]
        public string LostDiveTime { get; set; }
        [DataMember]
        public string RepetiveDiveDesignation { get; set; }
        [DataMember]
        public bool SkiBoat { get; set; }
        [DataMember]
        public bool LDV { get; set; }
        [DataMember]
        public bool Trailer { get; set; }
        [DataMember]
        public DivingCheckListVO DivingCheckList { get; set; }
        [DataMember]
        public int ChangeLocation { get; set; }
        [DataMember]
        public string LocationType { get; set; }

        [DataMember]
        public List<DivingRequestDiverVO> DivingRequestDivers1 { get; set; }

        [DataMember]
        public List<DivingRequestDiverVO> DivingRequestDivers2 { get; set; }

        [DataMember]
        public List<DivingRequestDiverVO> DivingRequestDivers3 { get; set; }

        [DataMember]
        public bool CommsCheck { get; set; }
        [DataMember]
        public bool BoilOut { get; set; }
        [DataMember]
        public bool MainGas { get; set; }
        [DataMember]
        public string Schedule { get; set; }
        [DataMember]
        public string LocationName { get; set; }

        // -- Added by sandeep on 15-12-2014

        [DataMember]
        public string ClearanceNo { get; set; }

        [DataMember]
        public string FromBollard { get; set; }

        [DataMember]
        public string ToBollard { get; set; }

        [DataMember]
        public Nullable<int> WorkflowInstanceID { get; set; }
        // -- end

        [DataMember]
        public Nullable<decimal> HoursOfOccupation { get; set; }

        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string DivingSupervisorName { get; set; }

        //Added By Omprakash For Notification on 16th Dec 2014
        public string PortName { get; set; }

        //-- Added by sandeep on 27-03-2015
        [DataMember]
        public string Reason { get; set; }
        //-- end

        [DataMember]
        public string OccupationReasonName { get; set; }
    }
}
