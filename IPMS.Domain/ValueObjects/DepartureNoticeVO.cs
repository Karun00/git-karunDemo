using System;
using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class DepartureNoticeVO
    {
        [DataMember]
        public string WFStatus { get; set; }
        [DataMember]
        public Nullable<DateTime> SubmissionDate { get; set; }
        [DataMember]
        public string DateTimeFormatConfigValue { get; set; }
        [DataMember]
        public int? DepartureID { get; set; }
        [DataMember]
        public string Tidal { get; set; }
        [DataMember]
        public string DaylightRestriction { get; set; }
        [DataMember]
        public string NoMainEngine { get; set; }
        [DataMember]
        public string WillSheBeUnderTow { get; set; }
        [DataMember]
        public string TowingDetails { get; set; }
        [DataMember]
        public string CurrentBerth { get; set; }
        [DataMember]
        public string SideAlongSideName { get; set; }
        [DataMember]
        public string SideAlongSideCode { get; set; }
        [DataMember]
        public string IsVesselDoubleBank { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EstimatedDatetimeOfSR { get; set; }
        [DataMember]
        public string EstimatedDatetimeOfSRConverted { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<DateTime> ModifiedDate { get; set; }
        [DataMember]
        public string WorkflowInstanceTaskName { get; set; }
        [DataMember]
        public string CurrentBerthCode { get; set; }
        [DataMember]
        public string WorkFlowRemarks { get; set; }
        [DataMember]
        public string AgentName { get; set; }
        [DataMember]
        public string AgentMobileNo { get; set; }
        [DataMember]
        public string AgentFaxNo { get; set; }
        [DataMember]
        public string AgentRepName { get; set; }
        [DataMember]
        public string AgentTelephoneNo { get; set; }
        [DataMember]
        public string AgentEmailID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public int? VesselID { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string VoyageIn { get; set; }
        [DataMember]
        public string VoyageOut { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public string CallSign { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public decimal? LengthOverallInM { get; set; }
        [DataMember]
        public decimal? BeamInM { get; set; }
        [DataMember]
        public string VesselNationality { get; set; }
        [DataMember]
        public string ArrDraft { get; set; }
        [DataMember]
        public string DepDraft { get; set; }
        [DataMember]
        public string NextPortOfCall { get; set; }
        [DataMember]
        public decimal? GrossRegisteredTonnageInMT { get; set; }
        [DataMember]
        public decimal? DeadWeightTonnageInMT { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }

        [DataMember]
        public int AgentID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string CurrentWorkStatus { get; set; }
        [DataMember]
        public string workflowRemarks { get; set; }
        [DataMember]
        public string IsFinal { get; set; }

        [DataMember]
        public int IsBanked { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ATB { get; set; }

    }
}
