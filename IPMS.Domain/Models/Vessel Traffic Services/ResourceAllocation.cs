using Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class ResourceAllocation : EntityBase
    {
        public ResourceAllocation()
        {
            this.OtherServiceRecordings = new List<OtherServiceRecording>();
            this.PilotageServiceRecordings = new List<PilotageServiceRecording>();
            this.ShiftingBerthingTaskExecutions = new List<ShiftingBerthingTaskExecution>();
        }

        public int ResourceAllocationID { get; set; }
        public string ServiceReferenceType { get; set; }
        public int ServiceReferenceID { get; set; }
        public string OperationType { get; set; }
        public Nullable<int> ResourceID { get; set; }        
        public string ResourceType { get; set; }
        public Nullable<System.DateTime> AcknowledgeDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> ActualScheduledTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string TaskStatus { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  ICollection<OtherServiceRecording> OtherServiceRecordings { get; set; }
        public  ICollection<PilotageServiceRecording> PilotageServiceRecordings { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  User User2 { get; set; }
        public  SubCategory SubCategory1 { get; set; }
        public  SubCategory SubCategory2 { get; set; }
        public  SubCategory SubCategory3 { get; set; }
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions { get; set; }

        // -- Added by sandeep on 22-09-2014
        public string AllocSlot { get; set; }
        public Nullable<int> CraftID { get; set; }
        public  Craft Craft { get; set; }
        public Nullable<System.DateTime> AllocationDate { get; set; }

        // -- end
        public bool IsConfirm { get; set; }
        [NotMapped]
        public Nullable<System.DateTime> MovementDateTime { get; set; }
        
    }
}
