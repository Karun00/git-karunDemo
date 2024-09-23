using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Bollard : EntityBase
    {
        public Bollard()
        {
            this.BerthMaintenances = new List<BerthMaintenance>();
            this.BerthMaintenances1 = new List<BerthMaintenance>();
            this.ServiceRequestShiftings = new List<ServiceRequestShifting>();
            this.ServiceRequestShiftings1 = new List<ServiceRequestShifting>();
            this.ServiceRequestWarpings = new List<ServiceRequestWarping>();
            this.ServiceRequestWarpings1 = new List<ServiceRequestWarping>();
            //this.Divings = new List<Diving>();
            //this.Divings1 = new List<Diving>();
            this.VesselCalls = new List<VesselCall>();
            this.VesselCalls1 = new List<VesselCall>();
            this.VesselCallMovements = new List<VesselCallMovement>();
            this.VesselCallMovements1 = new List<VesselCallMovement>();
            this.DivingRequests = new List<DivingRequest>();
            this.DivingRequests1 = new List<DivingRequest>();
            this.ShiftingBerthingTaskExecutions = new List<ShiftingBerthingTaskExecution>();
            this.ShiftingBerthingTaskExecutions1 = new List<ShiftingBerthingTaskExecution>();
            this.ShiftingBerthingTaskExecutions2 = new List<ShiftingBerthingTaskExecution>();
            this.ShiftingBerthingTaskExecutions3 = new List<ShiftingBerthingTaskExecution>();

            //-- Added by sandeep on 29-04-2015
            this.VesselCallMovements2 = new List<VesselCallMovement>();
            this.VesselCallMovements3 = new List<VesselCallMovement>();
            //-- end

        }


        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string BollardCode { get; set; }
        [DataMember]
        public string BollardName { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public decimal FromMeter { get; set; }
        [DataMember]
        public decimal ToMeter { get; set; }
        [DataMember]
        public string Continuous { get; set; }
        [DataMember]
        public string Coordinates { get; set; }
        [DataMember]
        public string OffsetCoordinates { get; set; }
        [DataMember]
        public string MidCoordinates { get; set; }
        //[DataMember]
        //public string PreviousCoordinates { get; set; }
        [DataMember]
        public string Description { get; set; }
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
        public  Berth Berth { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestShifting> ServiceRequestShiftings { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestShifting> ServiceRequestShiftings1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestWarping> ServiceRequestWarpings { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestWarping> ServiceRequestWarpings1 { get; set; }
        [DataMember]
        public  ICollection<VesselCall> VesselCalls { get; set; }
        [DataMember]
        public  ICollection<VesselCall> VesselCalls1 { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements1 { get; set; }
        [DataMember]
        public  ICollection<DivingRequest> DivingRequests { get; set; }
        [DataMember]
        public  ICollection<DivingRequest> DivingRequests1 { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions1 { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions2 { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions3 { get; set; }

        //-- Added by sandeep on 29-04-2015
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements2 { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements3 { get; set; }
        //-- end

    }
}
