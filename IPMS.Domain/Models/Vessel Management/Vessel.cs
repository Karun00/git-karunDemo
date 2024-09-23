using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    public partial class Vessel : EntityBase
    {
        public Vessel()
        {
            this.ArrivalNotifications = new List<ArrivalNotification>();
            this.DockingPlans = new List<DockingPlan>();
            this.VesselApprovals = new List<VesselApproval>();
            this.VesselEngines = new List<VesselEngine>();
            this.VesselGears = new List<VesselGear>();
            this.VesselGrabs = new List<VesselGrab>();
            this.VesselHatchHolds = new List<VesselHatchHold>();
            this.PilotExemptionRequests = new List<PilotExemptionRequest>();
            this.VesselCertificateDetails = new List<VesselCertificateDetail>();

            // Added by sandeep on 04-08-2014
            this.ExternalDivingRegisters = new List<ExternalDivingRegister>();
            // -- end
        }
        [NotMapped]
        public DateTime SubmissionDate { get; set; }
        public int VesselID { get; set; }
        public string IMONo { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        public string ExCallSign { get; set; }
        public string ClassificationSociety { get; set; }
        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public Nullable<long> NoOfBays { get; set; }
        public string CallSign { get; set; }
        public string OfficialNumber { get; set; }
        public long VesselBuildYear { get; set; }
        public string PortOfRegistry { get; set; }
        public string ExVesselName { get; set; }
        public Nullable<long> NoOfRowsOnDesk { get; set; }
        public string VesselNationality { get; set; }
        public string IsGovtVessel { get; set; }
        public long MMSINumber { get; set; }
        public Nullable<decimal> BeamInM { get; set; }
        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
        public Nullable<decimal> NetRegisteredTonnageInMT { get; set; }
        public Nullable<long> ParallelBodyLengthInM { get; set; }
        public Nullable<decimal> DeadWeightTonnageInMT { get; set; }
        public Nullable<long> BowToManifoldDistanceInM { get; set; }
        //public Nullable<long> SummerDeadWeightInMT { get; set; }
        public Nullable<decimal> SummerDeadWeightInMT { get; set; }
        public Nullable<long> SummerDraftFWDInM { get; set; }
        public Nullable<long> SummerDraftAFTInM { get; set; }
        public Nullable<long> SummerDisplacementInMT { get; set; }
        public Nullable<long> TEUCapacity { get; set; }
        public Nullable<long> ReducedGRT { get; set; }
        public string BowThruster { get; set; }
        //[DataMember]
        //public string CertificateName { get; set; }
        //[DataMember]
        //public string CertificateNo { get; set; }
        //[DataMember]
        //public Nullable<System.DateTime> DateOfIssue { get; set; }
        //[DataMember]
        //public Nullable<System.DateTime> DateOfValidity { get; set; }
        public Nullable<long> BowToForwardHatchDistanceM { get; set; }
        public Nullable<long> BowThrusterPowerKW { get; set; }
        public Nullable<long> BowToBridgeFrontDistanceM { get; set; }
        public Nullable<long> SternThrusterPowerKW { get; set; }
        //public string WFStatus { get; set; }
        //public int VerifiedBy { get; set; }
        //public System.DateTime VerifiedDate { get; set; }
        //public int ApprovedBy { get; set; }
        //public System.DateTime ApprovedDate { get; set; }
        //public string RejectComments { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }
        [DataMember]
        public  ICollection<DockingPlan> DockingPlans { get; set; }
        [DataMember]
        public  ICollection<PilotExemptionRequest> PilotExemptionRequests { get; set; }
        //[DataMember]
        //public  Port Port { get; set; }
        [DataMember]
        public  PortRegistry PortRegistry { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        //public  SubCategory SubCategory1 { get; set; }
        public  SubCategory SubCategory2 { get; set; }
        public  SubCategory SubCategory3 { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  ICollection<VesselApproval> VesselApprovals { get; set; }
        public  ICollection<VesselEngine> VesselEngines { get; set; }
        public  ICollection<VesselGear> VesselGears { get; set; }
        public  ICollection<VesselGrab> VesselGrabs { get; set; }
        public  ICollection<VesselHatchHold> VesselHatchHolds { get; set; }
        public  ICollection<VesselCertificateDetail> VesselCertificateDetails { get; set; }
        public  WorkflowInstance WorkflowInstances { get; set; }

        // Added by sandeep on 04-08-2014
        public  ICollection<ExternalDivingRegister> ExternalDivingRegisters { get; set; }
        // -- end
        [DataMember]
        public string IsFinal { get; set; }

        [NotMapped]
        public string VesselTypeName { get; set; }
        [NotMapped]
        public string WokflowStatus { get; set; }
        [NotMapped]
        public string DockingPlanNo { get; set; }
        [NotMapped]
        public int? DockingPlanID { get; set; }
        [NotMapped]
        public string Remarks { get; set; }

        public string SternThruster { get; set; }
    }
}
