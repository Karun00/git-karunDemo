using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ServiceRequest_VO
    {
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public int ServiceRequestID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string MovementType { get; set; }
        [DataMember]
        public string MovementDateTime { get; set; }
        [DataMember]
        public System.DateTime SubmittedDateTime { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string MovementName { get; set; }
        [DataMember]
        public string SideAlongSideCode { get; set; }
        [DataMember]
        public string SieAlongSideName { get; set; }
        [DataMember]
        public bool OwnSteam { get; set; }
        [DataMember]
        public bool NoMainEngine { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string IsFinal { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string workflowRemarks { get; set; }
        [DataMember]
        public string WorkflowTaskCode { get; set; }
        [DataMember]
        public List<string> ToBerths { get; set; }
        [DataMember]
        public List<string> FromBollards { get; set; }
        [DataMember]
        public List<string> ToBollards { get; set; }
        [DataMember]
        public ServiceRequestShiftingVO ServiceRequestShifting { get; set; }
        [DataMember]
        public ServiceRequestSailingVO ServiceRequestSailing { get; set; }
        [DataMember]
        public ServiceRequestWarpingVO ServiceRequestWarping { get; set; }
        [DataMember]
        public List<ServiceRequestDocumentVO> ServiceRequestDocuments { get; set; }
        [DataMember]
        public string WorkflowInstanceTaskName { get; set; }
        [DataMember]
        public ArrivalNotificationVO ArrivalNotification { get; set; }
        [DataMember]
        public VesselVO Vessel { get; set; }
        [DataMember]
        public VesselCallMovementVO VesselCallMovement { get; set; }
        [DataMember]
        public SubCategoryVO Subcategory { get; set; }
        [DataMember]
        public SubCategoryVO Subcategory1 { get; set; }
        [DataMember]
        public AgentVO Agent { get; set; }
        [DataMember]
        public AuthorizedContactPersonVO AuthorizedContactPerson { get; set; }

        [DataMember]
        public string CurrentBerth { get; set; }
        [DataMember]
        public string CurrentFromBollardName { get; set; }
        [DataMember]
        public string CurrentToBollardName { get; set; }
        [DataMember]
        public string CurrentBerthCode { get; set; }

        [DataMember]
        public string LastPortOfCall { get; set; }
        [DataMember]
        public string NextPortOfCall { get; set; }

        [DataMember]
        public List<string> ReasonForVisit { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public string VesselNationality { get; set; }

        [DataMember]
        public System.DateTime ETA { get; set; }
        [DataMember]
        public System.DateTime ETD { get; set; }
        [DataMember]
        public Nullable<decimal> DraftFWD { get; set; }
        [DataMember]
        public Nullable<decimal> DraftAFT { get; set; }

        [DataMember]
        public Nullable<int> PassengersEmbarking { get; set; }
        [DataMember]
        public Nullable<int> PassengersDisembarking { get; set; }
        [DataMember]
        public string IsSpecialNature { get; set; }
        [DataMember]
        public string Tidal { get; set; }
        [DataMember]
        public bool IsTidal { get; set; }
        [DataMember]
        public Nullable<int> BPWorkflowInstanceId { get; set; }
        [DataMember]
        public System.DateTime MovementDate { get; set; }
        [DataMember]
        public System.DateTime SubMovementDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PreferredDate { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string ContactNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string IsConfirmCancel { get; set; }
        [DataMember]
        public string SlotStatus { get; set; }
        [DataMember]
        public string SubmitDateTime { get; set; }
        [DataMember]
        public string PreferredDateTime { get; set; }
        [DataMember]
        public string SlotPeriod { get; set; }
        [DataMember]
        public string MovementSlot { get; set; }
        [DataMember]
        public bool IsUpdateMovement { get; set; }
        [DataMember]
        public string Reasons { get; set; }
        [DataMember]
        public string IsRecordingCompleted { get; set; }
        
    }

    public class ServiceRequestDocumentVO
    {
        [DataMember]
        public int ServiceRequestDocumentID { get; set; }
        [DataMember]
        public int ServiceRequestID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string DocumentCode { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public DocumentVO ServiceRequestDocument { get; set; }

    }

    public class ServiceRequestChangeETAVO
    {
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
        public string IsFinal { get; set; }    
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

    }

    public class SlotVO
    {
        [DataMember]
        public string Slot { get; set; }
        [DataMember]
        public string SlotPeriod { get; set; }
        [DataMember]
        public int SlotNumber { get; set; }
        [DataMember]
        public double StartTime { get; set; }
        [DataMember]
        public double EndTime { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public DateTime SlotDate { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
    }
}
