using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class VesselMapExtenstion
    {
        public static Vessel MapToEntity(this VesselVO vo)
        {
            Vessel vessel = new Vessel();
            if (vo != null)
            {
                vessel.IMONo = vo.IMONo;
                vessel.ExCallSign = vo.ExCallSign;
                vessel.ClassificationSociety = vo.ClassificationSociety;
                vessel.VesselID = vo.VesselID;
                vessel.VesselName = vo.VesselName;
                vessel.VesselType = vo.VesselType;
                vessel.CallSign = vo.CallSign;
                vessel.OfficialNumber = vo.OfficialNumber;
                vessel.VesselBuildYear = vo.VesselBuildYear;
                vessel.NoOfBays = vo.NoOfBays;
                vessel.PortOfRegistry = vo.PortOfRegistry;
                vessel.ExVesselName = vo.ExVesselName;
                vessel.NoOfRowsOnDesk = vo.NoOfRowsOnDesk;
                vessel.VesselNationality = vo.VesselNationality;
                vessel.IsGovtVessel = vo.IsGovtVessel;
                vessel.MMSINumber = vo.MMSINumber;
                vessel.BeamInM = vo.BeamInM;
                vessel.GrossRegisteredTonnageInMT = vo.GrossRegisteredTonnageInMT;
                vessel.LengthOverallInM = vo.LengthOverallInM;
                vessel.NetRegisteredTonnageInMT = vo.NetRegisteredTonnageInMT;
                vessel.ParallelBodyLengthInM = vo.ParallelBodyLengthInM;
                vessel.DeadWeightTonnageInMT = vo.DeadWeightTonnageInMT;
                vessel.BowToManifoldDistanceInM = vo.BowToManifoldDistanceInM;
                vessel.SummerDeadWeightInMT = vo.SummerDeadWeightInMT;
                vessel.SummerDraftAFTInM = vo.SummerDraftAFTInM;
                vessel.SummerDraftFWDInM = vo.SummerDraftFWDInM;
                vessel.SummerDisplacementInMT = vo.SummerDisplacementInMT;
                vessel.TEUCapacity = vo.TEUCapacity;
                vessel.ReducedGRT = vo.ReducedGRT;
                vessel.BowThruster = vo.BowThruster;
                vessel.BowToForwardHatchDistanceM = vo.BowToForwardHatchDistanceM;
                vessel.BowThrusterPowerKW = vo.BowThrusterPowerKW;
                vessel.BowToBridgeFrontDistanceM = vo.BowToBridgeFrontDistanceM;
                vessel.SternThrusterPowerKW = vo.SternThrusterPowerKW;
                vessel.WorkflowInstanceId = vo.WorkflowInstanceId;
                vessel.RecordStatus = vo.RecordStatus;
                vessel.CreatedBy = vo.CreatedBy;
                vessel.CreatedDate = vo.CreatedDate;
                vessel.ModifiedBy = vo.ModifiedBy;
                vessel.ModifiedDate = vo.ModifiedDate;
                vessel.VesselEngines = vo.VesselEngines.ToList().MapToEntity();
                vessel.VesselGears = vo.VesselGears.ToList().MapToEntity();
                vessel.VesselGrabs = vo.VesselGrabs.ToList().MapToEntity();
                vessel.VesselHatchHolds = vo.VesselHatchHolds.ToList().MapToEntity();
                vessel.VesselCertificateDetails = vo.VesselCertificateDetails.ToList().MapToEntity();
                vessel.SubmissionDate = vo.CreatedDate;
                vessel.SternThruster = vo.SternThruster != null ? vo.SternThruster : "N";
                vessel.IsFinal = vo.IsFinal;
            }
            return vessel;
        }



        public static VesselVO MapToDto(this Vessel data)
        {
            VesselVO VO = new VesselVO();
            if (data != null)
            {
                VO.IMONo = data.IMONo;
                VO.ExCallSign = data.ExCallSign;
                VO.ClassificationSociety = data.ClassificationSociety;
                VO.VesselID = data.VesselID;
                VO.VesselName = data.VesselName;
                VO.VesselType = data.VesselType;
                VO.CallSign = data.CallSign;
                VO.OfficialNumber = data.OfficialNumber;
                VO.VesselBuildYear = data.VesselBuildYear;
                VO.NoOfBays = data.NoOfBays;
                VO.PortOfRegistry = data.PortOfRegistry;
                VO.ExVesselName = data.ExVesselName;
                VO.NoOfRowsOnDesk = data.NoOfRowsOnDesk;                
                VO.VesselNationality = data.VesselNationality != null ? data.VesselNationality : "NA";
                VO.IsGovtVessel = data.IsGovtVessel;
                VO.MMSINumber = data.MMSINumber;
                VO.BeamInM = data.BeamInM != null ? data.BeamInM : 0;
                VO.GrossRegisteredTonnageInMT = data.GrossRegisteredTonnageInMT;
                VO.LengthOverallInM = data.LengthOverallInM != null ? data.LengthOverallInM : 0;
                VO.NetRegisteredTonnageInMT = data.NetRegisteredTonnageInMT;
                VO.ParallelBodyLengthInM = data.ParallelBodyLengthInM;
                VO.DeadWeightTonnageInMT = data.DeadWeightTonnageInMT;
                VO.BowToManifoldDistanceInM = data.BowToManifoldDistanceInM;
                VO.SummerDeadWeightInMT = data.SummerDeadWeightInMT;
                VO.SummerDraftAFTInM = data.SummerDraftAFTInM;
                VO.SummerDraftFWDInM = data.SummerDraftFWDInM;
                VO.SummerDisplacementInMT = data.SummerDisplacementInMT;
                VO.TEUCapacity = data.TEUCapacity;
                VO.ReducedGRT = data.ReducedGRT;
                VO.BowThruster = data.BowThruster;
                VO.BowToForwardHatchDistanceM = data.BowToForwardHatchDistanceM;
                VO.BowThrusterPowerKW = data.BowThrusterPowerKW;
                VO.BowToBridgeFrontDistanceM = data.BowToBridgeFrontDistanceM;
                VO.SternThrusterPowerKW = data.SternThrusterPowerKW;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.WorkflowInstanceId = data.WorkflowInstanceId;

                if (data.PortRegistry != null)
                {
                    VO.PortOfRegistryName = data.PortRegistry.PortName;
                }

                //Added by Santosh on 2/2/2015
                //To Get DockingPlanNo and SubmissionDate when Repair was selected as Reason for visit

                if (data.DockingPlans.Count > 0)
                {
                    foreach (var item in data.DockingPlans)
                    {

                        VO.DockingPlanNo = item.DockingPlanNo.ToString(CultureInfo.InvariantCulture);
                        VO.DockingPlanID = item.DockingPlanID.ToString(CultureInfo.InvariantCulture);
                        VO.SubmissionDate = item.CreatedDate.ToString("yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture);
                    }
                }
                
                if (data.VesselEngines.Count > 0)
                {
                    VO.VesselEngines = data.VesselEngines.ToList().MapToDto();
                }

                if (data.VesselGears.Count > 0)
                {
                    VO.VesselGears = data.VesselGears.ToList().MapToDto();
                }

                if (data.VesselGrabs.Count > 0)
                {
                    VO.VesselGrabs = data.VesselGrabs.ToList().MapToDto();
                }
                if (data.VesselHatchHolds.Count > 0)
                {
                    VO.VesselHatchHolds = data.VesselHatchHolds.ToList().MapToDto();
                }
                if (data.VesselCertificateDetails.Count > 0)
                {
                    VO.VesselCertificateDetails = data.VesselCertificateDetails.ToList().MapToDTO();
                }
                VO.VesselTypeName = data.SubCategory3 == null ? string.Empty : data.SubCategory3.SubCatName;

                VO.WFStatus = data.WokflowStatus;
                VO.CreatedDate = data.CreatedDate;
                VO.SternThruster = data.SternThruster;
            }
            return VO;
        }

        public static List<Vessel> MapToEntity(this List<VesselVO> vos)
        {
            List<Vessel> VesselEntities = new List<Vessel>();
            if (vos != null)
            {
                foreach (var vesselvo in vos)
                {
                    VesselEntities.Add(vesselvo.MapToEntity());
                }
            }
            return VesselEntities;
        }

        public static List<VesselVO> MapToDTO(this List<Vessel> data)
        {
            List<VesselVO> vos = new List<VesselVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDto());
                }
            }
            return vos;
        }


        public static List<VesselVO> MapToDtoWithDryDoc(this List<Vessel> data)
        {
            List<VesselVO> vos = new List<VesselVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDtoWithDryDoc());
                }
            }
            return vos;
        }


        public static VesselVO MapToDtoWithDryDoc(this Vessel data)
        {
            VesselVO VO = new VesselVO();
            if (data != null)
            {
                VO.IMONo = data.IMONo;
                VO.ExCallSign = data.ExCallSign;
                VO.ClassificationSociety = data.ClassificationSociety;
                VO.VesselID = data.VesselID;
                VO.VesselName = data.VesselName + '-' + data.IMONo;
                VO.VesselType = data.VesselType;
                VO.CallSign = data.CallSign;
                VO.OfficialNumber = data.OfficialNumber;
                VO.VesselBuildYear = data.VesselBuildYear;
                VO.NoOfBays = data.NoOfBays;
                VO.PortOfRegistry = data.PortOfRegistry;
                VO.ExVesselName = data.ExVesselName;
                VO.NoOfRowsOnDesk = data.NoOfRowsOnDesk;                
                VO.VesselNationality = data.SubCategory2 == null ? "NA" : data.SubCategory2.SubCatName;
                VO.IsGovtVessel = data.IsGovtVessel;
                VO.MMSINumber = data.MMSINumber;
                VO.BeamInM = data.BeamInM;
                VO.GrossRegisteredTonnageInMT = data.GrossRegisteredTonnageInMT;
                VO.LengthOverallInM = data.LengthOverallInM;
                VO.NetRegisteredTonnageInMT = data.NetRegisteredTonnageInMT;
                VO.ParallelBodyLengthInM = data.ParallelBodyLengthInM;
                VO.DeadWeightTonnageInMT = data.DeadWeightTonnageInMT;
                VO.BowToManifoldDistanceInM = data.BowToManifoldDistanceInM;
                VO.SummerDeadWeightInMT = data.SummerDeadWeightInMT;
                VO.SummerDraftAFTInM = data.SummerDraftAFTInM;
                VO.SummerDraftFWDInM = data.SummerDraftFWDInM;
                VO.SummerDisplacementInMT = data.SummerDisplacementInMT;
                VO.TEUCapacity = data.TEUCapacity;
                VO.ReducedGRT = data.ReducedGRT;
                VO.BowThruster = data.BowThruster;
                VO.BowToForwardHatchDistanceM = data.BowToForwardHatchDistanceM;
                VO.BowThrusterPowerKW = data.BowThrusterPowerKW;
                VO.BowToBridgeFrontDistanceM = data.BowToBridgeFrontDistanceM;
                VO.SternThrusterPowerKW = data.SternThrusterPowerKW;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.WorkflowInstanceId = data.WorkflowInstanceId;
                VO.SternThruster = data.SternThruster;
                if (data.DockingPlans.Count > 0)
                {
                    foreach (var dockingPlan in data.DockingPlans)
                    {

                        //WFSA
                        VO.DockingPlanNo = dockingPlan.DockingPlanNo.ToString(CultureInfo.InvariantCulture);
                        VO.DockingPlanID = dockingPlan.DockingPlanID.ToString(CultureInfo.InvariantCulture);
                        VO.SubmissionDate = dockingPlan.CreatedDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        VO.DockIsFinal = dockingPlan.IsFinal;
                        VO.DockStatus = dockingPlan.RecordStatus;

                    }
                }


                if (data.VesselEngines.Count > 0)
                {
                    VO.VesselEngines = data.VesselEngines.ToList().MapToDto();
                }

                if (data.VesselGears.Count > 0)
                {
                    VO.VesselGears = data.VesselGears.ToList().MapToDto();
                }

                if (data.VesselGrabs.Count > 0)
                {
                    VO.VesselGrabs = data.VesselGrabs.ToList().MapToDto();
                }
                if (data.VesselHatchHolds.Count > 0)
                {
                    VO.VesselHatchHolds = data.VesselHatchHolds.ToList().MapToDto();
                }
                if (data.VesselCertificateDetails.Count > 0)
                {
                    VO.VesselCertificateDetails = data.VesselCertificateDetails.ToList().MapToDTO();
                }
                VO.VesselTypeName = data.SubCategory3 == null ? string.Empty : data.SubCategory3.SubCatName;
            }
            return VO;
        }




        public static List<VesselVO> MapToDtoForCombo(this List<Vessel> data)
        {
            List<VesselVO> vos = new List<VesselVO>();
            if (data != null)
            {
                foreach (var vessel in data)
                {
                    vos.Add(vessel.MapToDtoCombo());
                }
            }
            return vos;
        }

        public static VesselVO MapToDtoCombo(this Vessel data)
        {
            VesselVO VO = new VesselVO();
            if (data != null)
            {
                VO.VesselID = data.VesselID;
                VO.VesselName = data.VesselName;
            }
            return VO;
        }


        public static VesselVO MapToDtoWithDrydockDtls(this Vessel data, string Port)
        {
            VesselVO VO = new VesselVO();
            if (data != null)
            {
                VO.IMONo = data.IMONo;
                VO.ExCallSign = data.ExCallSign;
                VO.ClassificationSociety = data.ClassificationSociety;
                VO.VesselID = data.VesselID;
                VO.VesselName = data.VesselName;
                VO.VesselType = data.VesselType;
                VO.CallSign = data.CallSign;
                VO.OfficialNumber = data.OfficialNumber;
                VO.VesselBuildYear = data.VesselBuildYear;
                VO.NoOfBays = data.NoOfBays;
                VO.PortOfRegistry = data.PortOfRegistry;
                VO.ExVesselName = data.ExVesselName;
                VO.NoOfRowsOnDesk = data.NoOfRowsOnDesk;
                VO.VesselNationality = data.SubCategory2 != null ? data.SubCategory2.SubCatName : "NA";
                VO.IsGovtVessel = data.IsGovtVessel;
                VO.MMSINumber = data.MMSINumber;
                VO.BeamInM = data.BeamInM != null ? data.BeamInM : 0;
                VO.GrossRegisteredTonnageInMT = data.GrossRegisteredTonnageInMT;
                VO.LengthOverallInM = data.LengthOverallInM != null ? data.LengthOverallInM : 0;
                VO.NetRegisteredTonnageInMT = data.NetRegisteredTonnageInMT;
                VO.ParallelBodyLengthInM = data.ParallelBodyLengthInM;
                VO.DeadWeightTonnageInMT = data.DeadWeightTonnageInMT;
                VO.BowToManifoldDistanceInM = data.BowToManifoldDistanceInM;
                VO.SummerDeadWeightInMT = data.SummerDeadWeightInMT;
                VO.SummerDraftAFTInM = data.SummerDraftAFTInM;
                VO.SummerDraftFWDInM = data.SummerDraftFWDInM;
                VO.SummerDisplacementInMT = data.SummerDisplacementInMT;
                VO.TEUCapacity = data.TEUCapacity;
                VO.ReducedGRT = data.ReducedGRT;
                VO.BowThruster = data.BowThruster;
                VO.BowToForwardHatchDistanceM = data.BowToForwardHatchDistanceM;
                VO.BowThrusterPowerKW = data.BowThrusterPowerKW;
                VO.BowToBridgeFrontDistanceM = data.BowToBridgeFrontDistanceM;
                VO.SternThrusterPowerKW = data.SternThrusterPowerKW;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.WorkflowInstanceId = data.WorkflowInstanceId;

                if (data.PortRegistry != null)
                {
                    VO.PortOfRegistryName = data.PortRegistry.PortName;
                }

                //Added by Santosh on 2/2/2015
                //To Get DockingPlanNo and SubmissionDate when Repair was selected as Reason for visit

                if (data.DockingPlans.Count > 0)
                {
                    foreach (var item in data.DockingPlans)
                    {
                        if (item.PortCode == Port)
                        {
                            VO.DockingPlanNo = item.DockingPlanNo.ToString(CultureInfo.InvariantCulture);
                            VO.DockingPlanID = item.DockingPlanID.ToString(CultureInfo.InvariantCulture);
                            VO.SubmissionDate = item.CreatedDate.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);
                        }
                    }
                }               

                if (data.VesselEngines.Count > 0)
                {
                    VO.VesselEngines = data.VesselEngines.ToList().MapToDto();
                }

                if (data.VesselGears.Count > 0)
                {
                    VO.VesselGears = data.VesselGears.ToList().MapToDto();
                }

                if (data.VesselGrabs.Count > 0)
                {
                    VO.VesselGrabs = data.VesselGrabs.ToList().MapToDto();
                }
                if (data.VesselHatchHolds.Count > 0)
                {
                    VO.VesselHatchHolds = data.VesselHatchHolds.ToList().MapToDto();
                }
                if (data.VesselCertificateDetails.Count > 0)
                {
                    VO.VesselCertificateDetails = data.VesselCertificateDetails.ToList().MapToDTO();
                }
                VO.VesselTypeName = data.SubCategory3 == null ? string.Empty : data.SubCategory3.SubCatName;

                VO.WFStatus = data.WokflowStatus;
                VO.CreatedDate = data.CreatedDate;
                VO.SternThruster = data.SternThruster;
            }
            return VO;
        }



    }
}
