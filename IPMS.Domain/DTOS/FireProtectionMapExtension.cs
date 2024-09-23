using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
   public static class FireProtectionMapExtension
    {    public static FireProtectionVO MapToDTO(this FireProtection data)
        {
        FireProtectionVO FireProtectionVO = new FireProtectionVO();
        if (data != null)
        {
            FireProtectionVO.LicenseRequestID = data.LicenseRequestID;
            FireProtectionVO.FireProtectionID = data.FireProtectionID;
            FireProtectionVO.HighRiskLicense = data.HighRiskLicense;
            FireProtectionVO.EmployeesApplQualifications = data.EmployeesApplQualifications;
            FireProtectionVO.YearsProvidingProtection = data.YearsProvidingProtection;
            FireProtectionVO.SAQAAccreditedBody = data.SAQAAccreditedBody;
            FireProtectionVO.BasicMarineFireFightingCerti = data.BasicMarineFireFightingCerti;
            FireProtectionVO.Level1FirstAidCertificate = data.Level1FirstAidCertificate;
            FireProtectionVO.BreathingApparatusCertificate = data.BreathingApparatusCertificate;
            FireProtectionVO.GenlHealthSafetyCertificate = data.GenlHealthSafetyCertificate;
            FireProtectionVO.ApprenticeshipProgramme = data.ApprenticeshipProgramme;
            FireProtectionVO.EquipmentRegisterTestCerti = data.EquipmentRegisterTestCerti;
            FireProtectionVO.HardHat = data.HardHat;
            FireProtectionVO.SafetyShoes = data.SafetyShoes;
            FireProtectionVO.ReflectiveJacket = data.ReflectiveJacket;
            FireProtectionVO.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
            FireProtectionVO.FireHelmet = data.FireHelmet;
            FireProtectionVO.FireCoat = data.FireCoat;
            FireProtectionVO.HardHat = data.HardHat;
            FireProtectionVO.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
            FireProtectionVO.CompiledRiskAssessment = data.CompiledRiskAssessment;
            FireProtectionVO.CompiledPlanReducingRisk = data.CompiledPlanReducingRisk;
        }
        return FireProtectionVO;
        }
    public static FireProtection MapToEntity(this FireProtectionVO data)
    {
        FireProtection FireProtection = new FireProtection();
        if (data != null)
        {
            FireProtection.LicenseRequestID = data.LicenseRequestID;
            FireProtection.FireProtectionID = data.FireProtectionID;
            FireProtection.HighRiskLicense = data.HighRiskLicense;
            FireProtection.EmployeesApplQualifications = data.EmployeesApplQualifications;
            FireProtection.YearsProvidingProtection = data.YearsProvidingProtection;
            FireProtection.SAQAAccreditedBody = data.SAQAAccreditedBody;
            FireProtection.BasicMarineFireFightingCerti = data.BasicMarineFireFightingCerti;
            FireProtection.Level1FirstAidCertificate = data.Level1FirstAidCertificate;
            FireProtection.BreathingApparatusCertificate = data.BreathingApparatusCertificate;
            FireProtection.GenlHealthSafetyCertificate = data.GenlHealthSafetyCertificate;
            FireProtection.ApprenticeshipProgramme = data.ApprenticeshipProgramme;
            FireProtection.EquipmentRegisterTestCerti = data.EquipmentRegisterTestCerti;
            FireProtection.HardHat = data.HardHat;
            FireProtection.SafetyShoes = data.SafetyShoes;
            FireProtection.ReflectiveJacket = data.ReflectiveJacket;
            FireProtection.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
            FireProtection.FireHelmet = data.FireHelmet;
            FireProtection.FireCoat = data.FireCoat;
            FireProtection.HardHat = data.HardHat;
            FireProtection.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
            FireProtection.CompiledRiskAssessment = data.CompiledRiskAssessment;
            FireProtection.CompiledPlanReducingRisk = data.CompiledPlanReducingRisk;
        }
        return FireProtection;
    }


    public static FireProtectionVO MapToDTOObj(this IEnumerable<FireProtection> fireProtections)
    {
        var fireProtectionsVoList = new FireProtectionVO();
        if (fireProtections != null)
            {
                foreach (var data in fireProtections)
                {
                    fireProtectionsVoList.LicenseRequestID = data.LicenseRequestID;
                    fireProtectionsVoList.FireProtectionID = data.FireProtectionID;
                    fireProtectionsVoList.HighRiskLicense = data.HighRiskLicense;
                    fireProtectionsVoList.EmployeesApplQualifications = data.EmployeesApplQualifications;
                    fireProtectionsVoList.YearsProvidingProtection = data.YearsProvidingProtection;
                    fireProtectionsVoList.SAQAAccreditedBody = data.SAQAAccreditedBody;
                    fireProtectionsVoList.BasicMarineFireFightingCerti = data.BasicMarineFireFightingCerti;
                    fireProtectionsVoList.Level1FirstAidCertificate = data.Level1FirstAidCertificate;
                    fireProtectionsVoList.BreathingApparatusCertificate = data.BreathingApparatusCertificate;
                    fireProtectionsVoList.GenlHealthSafetyCertificate = data.GenlHealthSafetyCertificate;
                    fireProtectionsVoList.ApprenticeshipProgramme = data.ApprenticeshipProgramme;
                    fireProtectionsVoList.EquipmentRegisterTestCerti = data.EquipmentRegisterTestCerti;
                    fireProtectionsVoList.HardHat = data.HardHat;
                    fireProtectionsVoList.SafetyShoes = data.SafetyShoes;
                    fireProtectionsVoList.ReflectiveJacket = data.ReflectiveJacket;
                    fireProtectionsVoList.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
                    fireProtectionsVoList.FireHelmet = data.FireHelmet;
                    fireProtectionsVoList.FireCoat = data.FireCoat;
                    fireProtectionsVoList.HardHat = data.HardHat;
                    fireProtectionsVoList.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                    fireProtectionsVoList.CompiledRiskAssessment = data.CompiledRiskAssessment;
                    fireProtectionsVoList.CompiledPlanReducingRisk = data.CompiledPlanReducingRisk;
                }
            }
        return fireProtectionsVoList;
    }


    public static List<FireProtectionVO> MapToDTO(this IEnumerable<FireProtection> fireProtections)
    {
        var fireProtectionsVoList = new List<FireProtectionVO>();
        if (fireProtections != null)
        {
            foreach (var item in fireProtections)
            {
                fireProtectionsVoList.Add(item.MapToDTO());
            }
        }
        return fireProtectionsVoList;
    }

    public static List<FireProtection> MapToEntity(this IEnumerable<FireProtectionVO> fireProtectionsVoList)
    {
        var fireProtections = new List<FireProtection>();
        if (fireProtectionsVoList != null)
        {
            foreach (var item in fireProtectionsVoList)
            {
                fireProtections.Add(item.MapToEntity());
            }
        }
        return fireProtections;
    }


    }
}
