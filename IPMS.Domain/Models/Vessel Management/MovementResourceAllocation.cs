using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class MovementResourceAllocation : EntityBase
    {
        public MovementResourceAllocation()
        {
            this.BerthingTaskExecutions = new List<BerthingTaskExecution>();
            this.FloatingCraneTaskExecutions = new List<FloatingCraneTaskExecution>();
            this.PilotageTaskExecutions = new List<PilotageTaskExecution>();
            this.PilotBoatTaskExecutions = new List<PilotBoatTaskExecution>();
            this.TugWorkboatTaskExecutions = new List<TugWorkboatTaskExecution>();
            this.WaterServiceTaskExecutions = new List<WaterServiceTaskExecution>();
        }

        [DataMember]
        public int MovementResourceAllocationID { get; set; }
        [DataMember]
        public int ServiceRequestID { get; set; }
        [DataMember]
        public string ResourceType { get; set; }
        [DataMember]
        public int ResourceID { get; set; }
        [DataMember]
        public System.DateTime MovementDateTime { get; set; }
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
        public  ICollection<BerthingTaskExecution> BerthingTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<FloatingCraneTaskExecution> FloatingCraneTaskExecutions { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  ServiceRequest ServiceRequest { get; set; }
        [DataMember]
        public  ICollection<PilotageTaskExecution> PilotageTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<PilotBoatTaskExecution> PilotBoatTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<TugWorkboatTaskExecution> TugWorkboatTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<WaterServiceTaskExecution> WaterServiceTaskExecutions { get; set; }
    }
}
