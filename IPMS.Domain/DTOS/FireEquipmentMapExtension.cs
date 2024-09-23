using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
namespace IPMS.Domain.DTOS
{
     public static class FireEquipmentMapExtension
    {
         public static FireEquipmentVO MapToDTO(this FireEquipment data)
         {
             FireEquipmentVO FireEquipmentVO = new FireEquipmentVO();
             if (data != null)
             {
                 FireEquipmentVO.FireEquipmentID = data.FireEquipmentID;
                 FireEquipmentVO.LicenseRequestID = data.LicenseRequestID;
                 FireEquipmentVO.MemberAssociationsBureaus = data.MemberAssociationsBureaus;
                 FireEquipmentVO.EquipmentTradersAssociation = data.EquipmentTradersAssociation;
                 FireEquipmentVO.AutomaticSprinklerInspection = data.AutomaticSprinklerInspection;
                 FireEquipmentVO.FireDetectionInstallers = data.FireDetectionInstallers;
                 FireEquipmentVO.EquipInstallationMaintenance = data.EquipInstallationMaintenance;
                 FireEquipmentVO.YearsProvidingEquipment = data.YearsProvidingEquipment;
                 FireEquipmentVO.EmployeesApplQualifications = data.EmployeesApplQualifications;
                 FireEquipmentVO.FireMaintenanceCertificate = data.FireMaintenanceCertificate;
                 FireEquipmentVO.SANS1475permit = data.SANS1475permit;
                 FireEquipmentVO.DOFTASCertificate = data.DOFTASCertificate;
                 FireEquipmentVO.GenlHealthSafetyCertificate = data.GenlHealthSafetyCertificate;
                 FireEquipmentVO.FireDivisionRegistration = data.FireDivisionRegistration;
                 FireEquipmentVO.EquipmentRegisterTestCerti = data.EquipmentRegisterTestCerti;
                 FireEquipmentVO.HardHat = data.HardHat;
                 FireEquipmentVO.SafetyShoes = data.SafetyShoes;
                 FireEquipmentVO.ReflectiveJacket = data.ReflectiveJacket;
                 FireEquipmentVO.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
                 FireEquipmentVO.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                 FireEquipmentVO.RiskAssessmentReportDealing = data.RiskAssessmentReportDealing;
                 FireEquipmentVO.CompiledPlanReducingRisk = data.CompiledPlanReducingRisk;
                 FireEquipmentVO.SafetyShoes = data.SafetyShoes;
                 FireEquipmentVO.CreatedBy = data.CreatedBy;
                 FireEquipmentVO.RecordStatus = data.RecordStatus;
                 FireEquipmentVO.CreatedDate = data.CreatedDate;
                 FireEquipmentVO.ModifiedBy = data.ModifiedBy;
                 FireEquipmentVO.ModifiedDate = data.ModifiedDate;
             }
             return FireEquipmentVO;
         }
         public static FireEquipment MapToEntity(this FireEquipmentVO FireEquipmentvo)
        {
            FireEquipment FireEquipment = new FireEquipment();
            if (FireEquipmentvo != null)
             {
                 FireEquipment.FireEquipmentID = FireEquipmentvo.FireEquipmentID;
                 FireEquipment.LicenseRequestID = FireEquipmentvo.LicenseRequestID;
                 FireEquipment.MemberAssociationsBureaus = FireEquipmentvo.MemberAssociationsBureaus;
                 FireEquipment.EquipmentTradersAssociation = FireEquipmentvo.EquipmentTradersAssociation;
                 FireEquipment.AutomaticSprinklerInspection = FireEquipmentvo.AutomaticSprinklerInspection;
                 FireEquipment.FireDetectionInstallers = FireEquipmentvo.FireDetectionInstallers;
                 FireEquipment.EquipInstallationMaintenance = FireEquipmentvo.EquipInstallationMaintenance;
                 FireEquipment.YearsProvidingEquipment = FireEquipmentvo.YearsProvidingEquipment;
                 FireEquipment.EmployeesApplQualifications = FireEquipmentvo.EmployeesApplQualifications;
                 FireEquipment.FireMaintenanceCertificate = FireEquipmentvo.FireMaintenanceCertificate;
                 FireEquipment.SANS1475permit = FireEquipmentvo.SANS1475permit;
                 FireEquipment.DOFTASCertificate = FireEquipmentvo.DOFTASCertificate;
                 FireEquipment.GenlHealthSafetyCertificate = FireEquipmentvo.GenlHealthSafetyCertificate;
                 FireEquipment.FireDivisionRegistration = FireEquipmentvo.FireDivisionRegistration;
                 FireEquipment.EquipmentRegisterTestCerti = FireEquipmentvo.EquipmentRegisterTestCerti;
                 FireEquipment.HardHat = FireEquipmentvo.HardHat;
                 FireEquipment.SafetyShoes = FireEquipmentvo.SafetyShoes;
                 FireEquipment.ReflectiveJacket = FireEquipmentvo.ReflectiveJacket;
                 FireEquipment.SelfInflatingLifeJacket = FireEquipmentvo.SelfInflatingLifeJacket;
                 FireEquipment.QualifyPublicLiabilityInsu = FireEquipmentvo.QualifyPublicLiabilityInsu;
                 FireEquipment.RiskAssessmentReportDealing = FireEquipmentvo.RiskAssessmentReportDealing;
                 FireEquipment.CompiledPlanReducingRisk = FireEquipmentvo.CompiledPlanReducingRisk;
                 FireEquipment.SafetyShoes = FireEquipmentvo.SafetyShoes;
                 FireEquipment.CreatedBy = FireEquipmentvo.CreatedBy;
                 FireEquipment.RecordStatus = FireEquipmentvo.RecordStatus;
                 FireEquipment.CreatedDate = FireEquipmentvo.CreatedDate;
                 FireEquipment.ModifiedBy = FireEquipmentvo.ModifiedBy;
                 FireEquipment.ModifiedDate = FireEquipmentvo.ModifiedDate;
             }
             return FireEquipment;
        }


         public static FireEquipmentVO MapToDTOObj(this IEnumerable<FireEquipment> fireEquipments)
         {
             var bunkeringVoList = new FireEquipmentVO();
             if (fireEquipments != null)
             {
                 foreach (var item in fireEquipments)
                 {


                     bunkeringVoList.FireEquipmentID = item.FireEquipmentID;
                     bunkeringVoList.LicenseRequestID = item.LicenseRequestID;
                     bunkeringVoList.MemberAssociationsBureaus = item.MemberAssociationsBureaus;
                     bunkeringVoList.EquipmentTradersAssociation = item.EquipmentTradersAssociation;
                     bunkeringVoList.AutomaticSprinklerInspection = item.AutomaticSprinklerInspection;
                     bunkeringVoList.FireDetectionInstallers = item.FireDetectionInstallers;
                     bunkeringVoList.EquipInstallationMaintenance = item.EquipInstallationMaintenance;
                     bunkeringVoList.YearsProvidingEquipment = item.YearsProvidingEquipment;
                     bunkeringVoList.EmployeesApplQualifications = item.EmployeesApplQualifications;
                     bunkeringVoList.FireMaintenanceCertificate = item.FireMaintenanceCertificate;
                     bunkeringVoList.SANS1475permit = item.SANS1475permit;
                     bunkeringVoList.DOFTASCertificate = item.DOFTASCertificate;
                     bunkeringVoList.GenlHealthSafetyCertificate = item.GenlHealthSafetyCertificate;
                     bunkeringVoList.FireDivisionRegistration = item.FireDivisionRegistration;
                     bunkeringVoList.EquipmentRegisterTestCerti = item.EquipmentRegisterTestCerti;
                     bunkeringVoList.HardHat = item.HardHat;
                     bunkeringVoList.SafetyShoes = item.SafetyShoes;
                     bunkeringVoList.ReflectiveJacket = item.ReflectiveJacket;
                     bunkeringVoList.SelfInflatingLifeJacket = item.SelfInflatingLifeJacket;
                     bunkeringVoList.QualifyPublicLiabilityInsu = item.QualifyPublicLiabilityInsu;
                     bunkeringVoList.RiskAssessmentReportDealing = item.RiskAssessmentReportDealing;
                     bunkeringVoList.CompiledPlanReducingRisk = item.CompiledPlanReducingRisk;
                     bunkeringVoList.SafetyShoes = item.SafetyShoes;
                     bunkeringVoList.CreatedBy = item.CreatedBy;
                     bunkeringVoList.RecordStatus = item.RecordStatus;
                     bunkeringVoList.CreatedDate = item.CreatedDate;
                     bunkeringVoList.ModifiedBy = item.ModifiedBy;
                     bunkeringVoList.ModifiedDate = item.ModifiedDate;


                 }
             }
             return bunkeringVoList;
         }




         public static List<FireEquipmentVO> MapToDTO(this IEnumerable<FireEquipment> fireEquipments)
         {
             var bunkeringVoList = new List<FireEquipmentVO>();
             if (fireEquipments != null)
             {
                 foreach (var item in fireEquipments)
                 {
                     bunkeringVoList.Add(item.MapToDTO());
                 }
             }
             return bunkeringVoList;
         }

         public static List<FireEquipment> MapToEntity(this IEnumerable<FireEquipmentVO> bunkeringVoList)
         {
             var bunkerings = new List<FireEquipment>();
             if (bunkeringVoList != null)
             {
                 foreach (var item in bunkeringVoList)
                 {
                     bunkerings.Add(item.MapToEntity());
                 }
             }
             return bunkerings;

         }

    }
}
