using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class VesselVO
    {
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public string ExCallSign { get; set; }
        [DataMember]
        public string ClassificationSociety { get; set; }
        [DataMember]
        public string SearchName { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public string CallSign { get; set; }
        [DataMember]
        public string OfficialNumber { get; set; }
        [DataMember]
        public long VesselBuildYear { get; set; }
        [DataMember]
        public Nullable<long> NoOfBays { get; set; }
        [DataMember]
        public string PortOfRegistry { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string ExVesselName { get; set; }
        [DataMember]
        public Nullable<long> NoOfRowsOnDesk { get; set; }
        [DataMember]
        public string VesselNationality { get; set; }
        [DataMember]
        public string IsGovtVessel { get; set; }
        [DataMember]
        public long MMSINumber { get; set; }
        [DataMember]
        public Nullable<decimal> BeamInM { get; set; }
        [DataMember]
        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }
        [DataMember]
        public Nullable<decimal> LengthOverallInM { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        [DataMember]
        public Nullable<decimal> NetRegisteredTonnageInMT { get; set; }
        [DataMember]
        public Nullable<long> ParallelBodyLengthInM { get; set; }
        [DataMember]
        public Nullable<decimal> DeadWeightTonnageInMT { get; set; }
        [DataMember]
        public Nullable<long> BowToManifoldDistanceInM { get; set; }
        [DataMember]
        public Nullable<decimal> SummerDeadWeightInMT { get; set; }
        [DataMember]
        public Nullable<long> SummerDraftFWDInM { get; set; }
        [DataMember]
        public Nullable<long> SummerDraftAFTInM { get; set; }
        [DataMember]
        public Nullable<long> SummerDisplacementInMT { get; set; }
        [DataMember]
        public Nullable<long> TEUCapacity { get; set; }
        [DataMember]
        public Nullable<long> ReducedGRT { get; set; }
        [DataMember]
        public string BowThruster { get; set; }
        [DataMember]
        public Nullable<long> BowToForwardHatchDistanceM { get; set; }
        [DataMember]
        public Nullable<long> BowThrusterPowerKW { get; set; }
        [DataMember]
        public Nullable<long> BowToBridgeFrontDistanceM { get; set; }
        [DataMember]
        public Nullable<long> SternThrusterPowerKW { get; set; }
        //[DataMember]
        //public string WFStatus { get; set; }
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
        public string VesselTypeName { get; set; }

        [DataMember]
        public virtual ICollection<ArrivalNotificationVO> ArrivalNotifications { get; set; }
        [DataMember]
        public virtual ICollection<VesselApprovalVO> VesselApprovals { get; set; }
        [DataMember]
        public virtual ICollection<VesselEngineVO> VesselEngines { get; set; }
        [DataMember]
        public virtual ICollection<VesselGearVO> VesselGears { get; set; }
        [DataMember]
        public virtual ICollection<VesselGrabVO> VesselGrabs { get; set; }
        [DataMember]
        public virtual ICollection<VesselHatchHoldVO> VesselHatchHolds { get; set; }
        [DataMember]
        public virtual ICollection<VesselCertificateDetailVO> VesselCertificateDetails { get; set; }

        [DataMember]
        public string DockingPlanNo { get; set; }
        [DataMember]
        public string DockingPlanID { get; set; }
        [DataMember]
        public string SubmissionDate { get; set; }
        [DataMember]
        public string PortOfRegistryName { get; set; }



        [DataMember]
        public string WokflowStatus { get; set; }

        [DataMember]
        public string WFStatus { get; set; }

        // -- Added by sandeep on 13-01-2014
        [DataMember]
        public int PilotExemptionID { get; set; }
        // -- end
        [DataMember]
        public string IsFinal { get; set; }

        [DataMember]
        public string DockIsFinal { get; set; }
        [DataMember]
        public string DockStatus { get; set; }

        [DataMember]
        public string SternThruster { get; set; }
        [DataMember]
        public string IsVisible { get; set; }
        [DataMember]
        public string PilotExemption { get; set; }


    }
}
