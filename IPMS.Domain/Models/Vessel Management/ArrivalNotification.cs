using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ArrivalNotification : EntityBase
    {
        public ArrivalNotification()
        {
            this.ArrivalApprovals = new List<ArrivalApproval>();
            this.ArrivalCommodities = new List<ArrivalCommodity>();
            this.ArrivalDocuments = new List<ArrivalDocument>();
            this.ArrivalIMDGTankers = new List<ArrivalIMDGTanker>();
            this.ArrivalAgents = new List<ArrivalAgent>();
            this.CargoManifests = new List<CargoManifest>();
            this.IMDGInformations = new List<IMDGInformation>();
            this.ServiceRequests = new List<ServiceRequest>();
            this.StatementFacts = new List<StatementFact>();
            this.SuppServiceRequests = new List<SuppServiceRequest>();
            this.VesselAgentChanges = new List<VesselAgentChange>();
            this.VesselArrestImmobilizationSAMSAs = new List<VesselArrestImmobilizationSAMSA>();
            this.VesselCalls = new List<VesselCall>();
            this.VesselCallAnchorages = new List<VesselCallAnchorage>();
            this.VesselCallMovements = new List<VesselCallMovement>();
            this.VesselETAChanges = new List<VesselETAChange>();
            this.RevenuePostings = new List<RevenuePosting>();
            // -- Sandeep Added on 06-11-2014
            this.SuppDryDocks = new List<SuppDryDock>();
            // -- end

            // -- Santosh Added on 10-12-2014
            this.DepartureNotices = new List<DepartureNotice>();
            // -- end

            this.ArrivalReasons = new List<ArrivalReason>();
            this.WasteDeclarations = new List<WasteDeclaration>();
        }

        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public int AgentID { get; set; }
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public string VoyageIn { get; set; }
        [DataMember]
        public string VoyageOut { get; set; }
        [DataMember]
        public System.DateTime ETA { get; set; }
        [DataMember]
        public System.DateTime ETD { get; set; }
        [DataMember]
        public string ArrDraft { get; set; }
        [DataMember]
        public string DepDraft { get; set; }      
        [DataMember]
        public string ReasonForVisit { get; set; }
        [DataMember]
        public string IsTerminalOperator { get; set; }
        [DataMember]
        public Nullable<int> TerminalOperatorID { get; set; }
        [DataMember]
        public string LastPortOfCall { get; set; }
        [DataMember]
        public string NextPortOfCall { get; set; }
        [DataMember]
        public Nullable<System.DateTime> NominationDate { get; set; }
        [DataMember]
        public string AppliedForISPS { get; set; }
        [DataMember]
        public Nullable<System.DateTime> AppliedDate { get; set; }
        [DataMember]
        public string Clearance { get; set; }
        [DataMember]
        public string ISPSReferenceNo { get; set; }
        [DataMember]
        public string PilotExemption { get; set; }
        [DataMember]
        public Nullable<int> ExemptionPilotID { get; set; }
        [DataMember]
        public string PreferredPortCode { get; set; }
        [DataMember]
        public string PreferredQuayCode { get; set; }
        [DataMember]
        public string PreferredBerthCode { get; set; }
        [DataMember]

        public string AlternatePortCode { get; set; }
        [DataMember]
        public string AlternateQuayCode { get; set; }
        [DataMember]
        public string AlternateBerthCode { get; set; }

        // new 

        public string DryDockBerthPortCode { get; set; }
        [DataMember]
        public string DryDockBerthQuayCode { get; set; }
        [DataMember]
        public string DryDockBerthCode { get; set; }


        [DataMember]
        public string PreferredSideDock { get; set; }
        [DataMember]
        public string PreferredSideAlternateBirth { get; set; }
        [DataMember]
        public string ReasonAlternateBirth { get; set; }
        [DataMember]
        public string SpecifyReason { get; set; }
        [DataMember]
        public string CancelRemarks { get; set; }
        [DataMember]
        public string Tidal { get; set; }
        [DataMember]
        public string BallastWater { get; set; }
        [DataMember]
        public string WasteDeclaration { get; set; }
        [DataMember]
        public string DaylightRestriction { get; set; }
        [DataMember]
        public string DaylightSpecifyReason { get; set; }
        [DataMember]
        public string ExceedPortLimitations { get; set; }
        [DataMember]
        public string ExceedSpecifyReason { get; set; }
        [DataMember]
        public string AnyAdditionalInfo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PlanDateTimeOfBerth { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PlanDateTimeToVacateBerth { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PlanDateTimeToStartCargo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PlanDateTimeToCompleteCargo { get; set; }
        [DataMember]
        public string AnyDangerousGoodsonBoard { get; set; }
        [DataMember]
        public string DangerousGoodsClass { get; set; }
        [DataMember]
        public string UNNo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LoadDischargeDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DischargeDate { get; set; }

        [DataMember]
        public Nullable<decimal> IMDGNetQty { get; set; }
        [DataMember]
        public string CellNo { get; set; }
        [DataMember]
        public string CargoDescription { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PlannedDurationDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PlannedDurationToDate { get; set; }
        [DataMember]
        public string ReasonForLayup { get; set; }
        [DataMember]
        public string BunkersRequired { get; set; }
        [DataMember]
        public string BunkersMethod { get; set; }
        [DataMember]
        public Nullable<int> BunkerService { get; set; }        
        [DataMember]
        public Nullable<decimal> DistanceFromStern { get; set; }
        [DataMember]
        public Nullable<decimal> TonsMT { get; set; }
        [DataMember]
        public string AnyImpInfo { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
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
        public string IsANFinal { get; set; }
        [DataMember]
        public string IsISPSANFinal { get; set; }
        [DataMember]
        public string IsPHANFinal { get; set; }
        [DataMember]
        public string IsSpecialNature { get; set; }
        [DataMember]
        public string SpecialNatureReason { get; set; }
        [DataMember]
        public string IsIMDGANFinal { get; set; }

        [DataMember]
        public string Isdraft { get; set; }
        [DataMember]
        public string GeneratedVCN { get; set; }
        [DataMember]
        public string LastPortWasteDelivered { get; set; }
        [DataMember]
        public string NextPortWasteDelivery { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DateLastWasteDelivered { get; set; }

        [DataMember]
        public  Agent Agent { get; set; }
        [DataMember]
        public  ICollection<ArrivalApproval> ArrivalApprovals { get; set; }
        [DataMember]
        public  ICollection<ArrivalAgent> ArrivalAgents { get; set; }
        [DataMember]
        public  ICollection<ArrivalCommodity> ArrivalCommodities { get; set; }
        [DataMember]
        public  ICollection<ArrivalDocument> ArrivalDocuments { get; set; }
        [DataMember]
        public  ICollection<ArrivalIMDGTanker> ArrivalIMDGTankers { get; set; }
        [DataMember]
        public  Berth Berth { get; set; }
        [DataMember]
        public  Berth Berth2 { get; set; }
        [DataMember]
        public  Berth Berth1 { get; set; }

        [DataMember]
        public  LicenseRequest LicenseRequest { get; set; }      
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  Pilot Pilot { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  Port Port { get; set; }

        [DataMember]
        public  PortRegistry LastPort { get; set; }
        [DataMember]
        public  PortRegistry NextPort { get; set; }

        [DataMember]
        public PortRegistry LastPortWasteDeliveredPort { get; set; }
        [DataMember]
        public PortRegistry NextPortWasteDeliveryPort { get; set; }

        [DataMember]
        public  SubCategory SubCategory1 { get; set; }
        [DataMember]
        public  SubCategory SubCategory2 { get; set; }
        [DataMember]
        public  SubCategory SubCategory12 { get; set; }
        [DataMember]
        public  SubCategory SubCategory13 { get; set; }

        [DataMember]
        public  SubCategory SubCategory3 { get; set; }
        [DataMember]
        public  TerminalOperator TerminalOperator { get; set; }
        [DataMember]
        public  Vessel Vessel { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }
        [DataMember]
        public  ICollection<CargoManifest> CargoManifests { get; set; }
        [DataMember]
        public  ICollection<IMDGInformation> IMDGInformations { get; set; }
        [DataMember]
        public  ICollection<ServiceRequest> ServiceRequests { get; set; }
        [DataMember]
        public  ICollection<StatementFact> StatementFacts { get; set; }
        [DataMember]
        public  ICollection<SuppServiceRequest> SuppServiceRequests { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChange> VesselAgentChanges { get; set; }
        [DataMember]
        public  ICollection<VesselArrestImmobilizationSAMSA> VesselArrestImmobilizationSAMSAs { get; set; }
        [DataMember]
        public  ICollection<VesselCall> VesselCalls { get; set; }
        [DataMember]
        public  ICollection<VesselCallAnchorage> VesselCallAnchorages { get; set; }
        [DataMember]
        public  ICollection<VesselCallMovement> VesselCallMovements { get; set; }
        [DataMember]
        public  ICollection<VesselETAChange> VesselETAChanges { get; set; }

        // -- Sandeep Added on 06-11-2014
        [DataMember]
        public  ICollection<SuppDryDock> SuppDryDocks { get; set; }
        // -- end

        // -- Santosh Added on 10-12-2014
        public  IList<DepartureNotice> DepartureNotices { get; set; }
        // -- end
        [DataMember]
        public  ICollection<RevenuePosting> RevenuePostings { get; set; }
        [DataMember]
        public  ICollection<ArrivalReason> ArrivalReasons { get; set; }
        [DataMember]
        public ICollection<WasteDeclaration> WasteDeclarations { get; set; }
    }
}










