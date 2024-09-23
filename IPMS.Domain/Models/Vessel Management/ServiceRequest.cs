using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ServiceRequest : EntityBase
    {
        public ServiceRequest()
        {
            this.MovementResourceAllocations = new List<MovementResourceAllocation>();
            this.ServiceRequestApprovals = new List<ServiceRequestApproval>();
            this.ServiceRequestDocuments = new List<ServiceRequestDocument>();
            this.ServiceRequestSailings = new List<ServiceRequestSailing>();
            this.ServiceRequestShiftings = new List<ServiceRequestShifting>();
            this.ServiceRequestWarpings = new List<ServiceRequestWarping>();
            this.VesselCallMovements = new List<VesselCallMovement>();
        }

        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public int ServiceRequestID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string MovementType { get; set; }
        [DataMember]
        public System.DateTime MovementDateTime { get; set; }
        [DataMember]
        public string SideAlongSideCode { get; set; }
        [DataMember]
        public string OwnSteam { get; set; }
        [DataMember]
        public string NoMainEngine { get; set; }
        [DataMember]
        public string Comments { get; set; }
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
        public string IsTidal { get; set; }
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
        public string IsFinal { get; set; }

        [NotMapped]
        public string MovementName { get; set; }
        [NotMapped]
        public string VesselName { get; set; }
        [NotMapped]
        public System.DateTime SubmittedDateTime { get; set; }
        [DataMember]
        public Nullable<decimal> DraftFWD { get; set; }
        [DataMember]
        public Nullable<decimal> DraftAFT { get; set; }

        [DataMember]
        public Nullable<int> PassengersEmbarking { get; set; }
        [DataMember]
        public Nullable<int> PassengersDisembarking { get; set; }

        [DataMember]
        public Nullable<int> BPWorkflowInstanceId { get; set; }
        [DataMember]
        public WorkflowInstance WorkflowInstance1 { get; set; }

        [DataMember]
        public Nullable<System.DateTime> PreferredDateTime { get; set; }
        [DataMember]
        public string SlotPeriod { get; set; }
        [DataMember]
        public string MovementSlot { get; set; }
        [NotMapped]
        public bool IsUpdateMovement { get; set; }


        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }
        [DataMember]
        public  ArrivalNotification ArrivalNotification { get; set; }
        [DataMember]
        public  ICollection<MovementResourceAllocation> MovementResourceAllocations { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  SubCategory SubCategory1 { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestApproval> ServiceRequestApprovals { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestDocument> ServiceRequestDocuments { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestSailing> ServiceRequestSailings { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestShifting> ServiceRequestShiftings { get; set; }
        [DataMember]
        public  ICollection<ServiceRequestWarping> ServiceRequestWarpings { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string IsRecordingCompleted { get; set; }

    }
}
