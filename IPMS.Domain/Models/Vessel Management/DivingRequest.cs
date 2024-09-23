using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DivingRequest : EntityBase
    {
        public DivingRequest()
        {
            this.DivingOccupationApprovals = new List<DivingOccupationApproval>();
            this.DivingRequestDivers = new List<DivingRequestDiver>();
            this.DivingCheckLists = new List<DivingCheckList>();
        }

        [DataMember]
        public string DRN { get; set; }
        [DataMember]
        public string OccupationReason { get; set; }
        [NotMapped]
        public  string Port { get; set; }
        [NotMapped]
        public  string LocationorQuay { get; set; }

        [NotMapped]
        public  string Raisedby { get; set; }
        [NotMapped]
        public  string Berth { get; set; }
        [DataMember]
        public int DivingRequestID { get; set; }
        [DataMember]
        public string FromPortCode { get; set; }
        [DataMember]
        public string FromQuayCode { get; set; }
        [DataMember]
        public string FromBerthCode { get; set; }
        [DataMember]
        public string FromBollardCode { get; set; }
        [DataMember]
        public string ToPortCode { get; set; }
        [DataMember]
        public string ToQuayCode { get; set; }
        [DataMember]
        public string ToBerthCode { get; set; }
        [DataMember]
        public string ToBollardCode { get; set; }

        [DataMember]
        public System.DateTime RequiredByDate { get; set; }
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
        [DataMember]
        public  Bollard Bollard { get; set; }
        [DataMember]
        public  Bollard Bollard1 { get; set; }
        [DataMember]
        public  User User { get; set; }

        [DataMember]
        public  User User1 { get; set; }

        // --  Modified by Prasad on 01-08-2014

        [DataMember]
        public Nullable<int> OtherLocation { get; set; }
        [DataMember]
        public string QuayLocation { get; set; }

        [DataMember]
        public Nullable<System.DateTime> OcupationFromDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OcupationToDate { get; set; }
        [DataMember]
        public Nullable<decimal> HoursOfOccupation1 { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StopTime { get; set; }
        [DataMember]
        public Nullable<decimal> HoursOfOccupation2 { get; set; }
        [DataMember]
        public string DivingReferenceNo { get; set; }
        [DataMember]
        public string SupervisorName { get; set; }
        [DataMember]
        public string DiveTenders { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LoggedDiveTimeFrom { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LoggedDiveTimeTo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeDiveOperationCancelled { get; set; }
        [DataMember]
        public string DiveNature { get; set; }
        [DataMember]
        public Nullable<decimal> DiverDepth { get; set; }
        [DataMember]
        public string BreathingMixture { get; set; }
        [DataMember]
        public string CompressedAir { get; set; }
        [DataMember]
        public string DivingEquipmentUsed1 { get; set; }
        [DataMember]
        public string DivingEquipmentUsed2 { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeLeftWorkshop { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeLeftSite { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeArrivedWorkshop { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeArrivedSite { get; set; }
        [DataMember]
        public string DecompressionTables { get; set; }
        [DataMember]
        public string CommsCheck { get; set; }
        [DataMember]
        public string BoilOut { get; set; }
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
        public Nullable<System.DateTime> LostDiveTime { get; set; }
        [DataMember]
        public string RepetiveDiveDesignation { get; set; }
        [DataMember]
        public string SkiBoat { get; set; }
        [DataMember]
        public string LDV { get; set; }
        [DataMember]
        public string Trailer { get; set; }
        [DataMember]
        public  ICollection<DivingOccupationApproval> DivingOccupationApprovals { get; set; }

        [DataMember]
        public  ICollection<DivingRequestDiver> DivingRequestDivers { get; set; }
        [DataMember]
        public  ICollection<DivingCheckList> DivingCheckLists { get; set; }

        // -- end

        [DataMember]
        public Nullable<int> ChangeLocation { get; set; }
        [DataMember]
        public string LocationType { get; set; }
        [DataMember]
        public  Quay Quay { get; set; }
        [DataMember]
        public  Location Location { get; set; }
        [DataMember]
        public  Location Location1 { get; set; }

        [DataMember]
        public string MainGas { get; set; }
        [DataMember]
        public string Schedule { get; set; }

        // -- Added by sandeep on 15-12-2014
        [DataMember]
        public string ClearanceNo { get; set; }
        //- end

        [NotMapped]
        public string FromBollard { get; set; }

        [NotMapped]
        public string ToBollard { get; set; }

        //-- Added by sandeep on 10-03-2015
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        //-- end
    }
}
