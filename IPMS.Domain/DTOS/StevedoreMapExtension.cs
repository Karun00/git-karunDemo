using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class StevedoreMapExtension
    {
        public static StevedoreVO MapToDTO(this Stevedore data)
        {
            StevedoreVO StevedoreVO = new StevedoreVO();
            StevedoreVO.LicenseRequestID = data.LicenseRequestID;
            StevedoreVO.StevedoreID = data.StevedoreID;
            StevedoreVO.NASASAMember = data.NASASAMember;
            StevedoreVO.QualifiedFirstAiderPerShift = data.QualifiedFirstAiderPerShift;
            StevedoreVO.QualifiedFirstAiderPerShift50 = data.QualifiedFirstAiderPerShift50;
            StevedoreVO.FireFightingEmployeesTrained = data.FireFightingEmployeesTrained;
            StevedoreVO.SafetyOfficers = data.SafetyOfficers;
            StevedoreVO.QualifiedExperiencedRiggers = data.QualifiedExperiencedRiggers;
            StevedoreVO.VehicleOperators = data.VehicleOperators;
            StevedoreVO.LiftingEquipmentInspectors = data.LiftingEquipmentInspectors;
            StevedoreVO.Electricians = data.Electricians;
            StevedoreVO.FlagSignalOperators = data.FlagSignalOperators;
            StevedoreVO.Lifting = data.Lifting;
            StevedoreVO.EquipmentInspectors = data.EquipmentInspectors;
            StevedoreVO.HazardousCargoHandlers = data.HazardousCargoHandlers;
            StevedoreVO.SafetyHealthEnvironmentalRep = data.SafetyHealthEnvironmentalRep;
            StevedoreVO.OperationalManager = data.OperationalManager;
            StevedoreVO.LiftingEquipment = data.LiftingEquipment;
            StevedoreVO.MotorizedEquipment = data.MotorizedEquipment;
            StevedoreVO.ElectricalEquipment = data.ElectricalEquipment;
            StevedoreVO.PalletsLoadSupportingDevices = data.PalletsLoadSupportingDevices;
            StevedoreVO.HandProtectors = data.HandProtectors;
            StevedoreVO.HeadProtectors = data.HeadProtectors;
            StevedoreVO.EyeFaceProtection = data.EyeFaceProtection;
            StevedoreVO.Footwear = data.Footwear;
            StevedoreVO.ProtectiveClothing = data.ProtectiveClothing;
            StevedoreVO.HearingConservationEquipment = data.HearingConservationEquipment;
            StevedoreVO.RespiratoryEquipment = data.RespiratoryEquipment;
            StevedoreVO.SafetyHarnesses = data.SafetyHarnesses;
            StevedoreVO.OHSAAppointed = data.OHSAAppointed;
            StevedoreVO.SafetyHealthEnvIncidents = data.SafetyHealthEnvIncidents;
            StevedoreVO.OccuHealthSafetyTraining = data.OccuHealthSafetyTraining;
            StevedoreVO.AnnualRefresher = data.AnnualRefresher;
            StevedoreVO.FirstAidBoxesAvailable = data.FirstAidBoxesAvailable;
            StevedoreVO.FacilitiesTreatInjuries = data.FacilitiesTreatInjuries;
            StevedoreVO.EmergencyContactDetails = data.EmergencyContactDetails;
            StevedoreVO.EvacuationProceduresPracticed = data.EvacuationProceduresPracticed;
            StevedoreVO.HousekeepingFacilities = data.HousekeepingFacilities;
            StevedoreVO.WorkerFacilities = data.WorkerFacilities;
            StevedoreVO.StackingStorageFacilities = data.StackingStorageFacilities;
            StevedoreVO.InspectionFacilities = data.InspectionFacilities;
            StevedoreVO.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
            StevedoreVO.CompiledRiskAssessment = data.CompiledRiskAssessment;
            StevedoreVO.CompiledPlanReducingRisk = data.CompiledPlanReducingRisk;
            return StevedoreVO;
        }

        public static Stevedore MapToEntity(this StevedoreVO data)
        {
            Stevedore Stevedore = new Stevedore();
            Stevedore.LicenseRequestID = data.LicenseRequestID;
            Stevedore.StevedoreID = data.StevedoreID;
            Stevedore.NASASAMember = data.NASASAMember;
            Stevedore.QualifiedFirstAiderPerShift = data.QualifiedFirstAiderPerShift;
            Stevedore.QualifiedFirstAiderPerShift50 = data.QualifiedFirstAiderPerShift50;
            Stevedore.FireFightingEmployeesTrained = data.FireFightingEmployeesTrained;
            Stevedore.SafetyOfficers = data.SafetyOfficers;
            Stevedore.QualifiedExperiencedRiggers = data.QualifiedExperiencedRiggers;
            Stevedore.VehicleOperators = data.VehicleOperators;
            Stevedore.LiftingEquipmentInspectors = data.LiftingEquipmentInspectors;
            Stevedore.Electricians = data.Electricians;
            Stevedore.FlagSignalOperators = data.FlagSignalOperators;
            Stevedore.Lifting = data.Lifting;
            Stevedore.EquipmentInspectors = data.EquipmentInspectors;
            Stevedore.HazardousCargoHandlers = data.HazardousCargoHandlers;
            Stevedore.SafetyHealthEnvironmentalRep = data.SafetyHealthEnvironmentalRep;
            Stevedore.OperationalManager = data.OperationalManager;
            Stevedore.LiftingEquipment = data.LiftingEquipment;
            Stevedore.MotorizedEquipment = data.MotorizedEquipment;
            Stevedore.ElectricalEquipment = data.ElectricalEquipment;
            Stevedore.PalletsLoadSupportingDevices = data.PalletsLoadSupportingDevices;
            Stevedore.HandProtectors = data.HandProtectors;
            Stevedore.HeadProtectors = data.HeadProtectors;
            Stevedore.EyeFaceProtection = data.EyeFaceProtection;
            Stevedore.Footwear = data.Footwear;
            Stevedore.ProtectiveClothing = data.ProtectiveClothing;
            Stevedore.HearingConservationEquipment = data.HearingConservationEquipment;
            Stevedore.RespiratoryEquipment = data.RespiratoryEquipment;
            Stevedore.SafetyHarnesses = data.SafetyHarnesses;
            Stevedore.OHSAAppointed = data.OHSAAppointed;
            Stevedore.SafetyHealthEnvIncidents = data.SafetyHealthEnvIncidents;
            Stevedore.OccuHealthSafetyTraining = data.OccuHealthSafetyTraining;
            Stevedore.AnnualRefresher = data.AnnualRefresher;
            Stevedore.FirstAidBoxesAvailable = data.FirstAidBoxesAvailable;
            Stevedore.EmergencyContactDetails = data.EmergencyContactDetails;
            Stevedore.EvacuationProceduresPracticed = data.EvacuationProceduresPracticed;
            Stevedore.HousekeepingFacilities = data.HousekeepingFacilities;
            Stevedore.WorkerFacilities = data.WorkerFacilities;
            Stevedore.StackingStorageFacilities = data.StackingStorageFacilities;
            Stevedore.InspectionFacilities = data.InspectionFacilities;
            Stevedore.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
            Stevedore.CompiledRiskAssessment = data.CompiledRiskAssessment;
            Stevedore.CompiledPlanReducingRisk = data.CompiledPlanReducingRisk;
            return Stevedore;
        }

        public static StevedoreVO MapToDTOObj(this IEnumerable<Stevedore> stevedores)
        {
            var stevedoresVoList = new StevedoreVO();
            foreach (var data in stevedores)
            {
                stevedoresVoList.LicenseRequestID = data.LicenseRequestID;
                stevedoresVoList.StevedoreID = data.StevedoreID;
                stevedoresVoList.NASASAMember = data.NASASAMember;
                stevedoresVoList.QualifiedFirstAiderPerShift = data.QualifiedFirstAiderPerShift;
                stevedoresVoList.QualifiedFirstAiderPerShift50 = data.QualifiedFirstAiderPerShift50;
                stevedoresVoList.FireFightingEmployeesTrained = data.FireFightingEmployeesTrained;
                stevedoresVoList.SafetyOfficers = data.SafetyOfficers;
                stevedoresVoList.QualifiedExperiencedRiggers = data.QualifiedExperiencedRiggers;
                stevedoresVoList.VehicleOperators = data.VehicleOperators;
                stevedoresVoList.LiftingEquipmentInspectors = data.LiftingEquipmentInspectors;
                stevedoresVoList.Electricians = data.Electricians;
                stevedoresVoList.FlagSignalOperators = data.FlagSignalOperators;
                stevedoresVoList.Lifting = data.Lifting;
                stevedoresVoList.EquipmentInspectors = data.EquipmentInspectors;
                stevedoresVoList.HazardousCargoHandlers = data.HazardousCargoHandlers;
                stevedoresVoList.SafetyHealthEnvironmentalRep = data.SafetyHealthEnvironmentalRep;
                stevedoresVoList.OperationalManager = data.OperationalManager;
                stevedoresVoList.LiftingEquipment = data.LiftingEquipment;
                stevedoresVoList.MotorizedEquipment = data.MotorizedEquipment;
                stevedoresVoList.ElectricalEquipment = data.ElectricalEquipment;
                stevedoresVoList.PalletsLoadSupportingDevices = data.PalletsLoadSupportingDevices;
                stevedoresVoList.HandProtectors = data.HandProtectors;
                stevedoresVoList.HeadProtectors = data.HeadProtectors;
                stevedoresVoList.EyeFaceProtection = data.EyeFaceProtection;
                stevedoresVoList.Footwear = data.Footwear;
                stevedoresVoList.ProtectiveClothing = data.ProtectiveClothing;
                stevedoresVoList.HearingConservationEquipment = data.HearingConservationEquipment;
                stevedoresVoList.RespiratoryEquipment = data.RespiratoryEquipment;
                stevedoresVoList.SafetyHarnesses = data.SafetyHarnesses;
                stevedoresVoList.OHSAAppointed = data.OHSAAppointed;
                stevedoresVoList.SafetyHealthEnvIncidents = data.SafetyHealthEnvIncidents;
                stevedoresVoList.OccuHealthSafetyTraining = data.OccuHealthSafetyTraining;
                stevedoresVoList.AnnualRefresher = data.AnnualRefresher;
                stevedoresVoList.FirstAidBoxesAvailable = data.FirstAidBoxesAvailable;
                stevedoresVoList.FacilitiesTreatInjuries = data.FacilitiesTreatInjuries;
                stevedoresVoList.EmergencyContactDetails = data.EmergencyContactDetails;
                stevedoresVoList.EvacuationProceduresPracticed = data.EvacuationProceduresPracticed;
                stevedoresVoList.HousekeepingFacilities = data.HousekeepingFacilities;
                stevedoresVoList.WorkerFacilities = data.WorkerFacilities;
                stevedoresVoList.StackingStorageFacilities = data.StackingStorageFacilities;
                stevedoresVoList.InspectionFacilities = data.InspectionFacilities;
                stevedoresVoList.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                stevedoresVoList.CompiledRiskAssessment = data.CompiledRiskAssessment;
                stevedoresVoList.CompiledPlanReducingRisk = data.CompiledPlanReducingRisk;
            }
            return stevedoresVoList;

        }

        public static List<StevedoreVO> MapToDTO(this IEnumerable<Stevedore> stevedores)
        {
            var stevedoresVoList = new List<StevedoreVO>();
            foreach (var item in stevedores)
            {
                stevedoresVoList.Add(item.MapToDTO());
            }
            return stevedoresVoList;
        }

        public static List<Stevedore> MapToEntity(this IEnumerable<StevedoreVO> stevedoresVoList)
        {
            var stevedores = new List<Stevedore>();
            foreach (var item in stevedoresVoList)
            {
                stevedores.Add(item.MapToEntity());
            }
            return stevedores;
        }
    }
}
