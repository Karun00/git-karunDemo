using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class FloatingCraneMapExtension
    {
        public static FloatingCraneVO MapToDTO(this FloatingCrane data)
        {
            FloatingCraneVO FloatingCraneVO = new FloatingCraneVO();
            if (data != null)
            {
                FloatingCraneVO.LicenseRequestID = data.LicenseRequestID;
                FloatingCraneVO.FloatingCraneID = data.FloatingCraneID;

                FloatingCraneVO.QualificationsCompetencies = data.QualificationsCompetencies;
                FloatingCraneVO.EmployQualifiedTrainedPers = data.EmployQualifiedTrainedPers;
                FloatingCraneVO.HardHat = data.HardHat;
                FloatingCraneVO.SafetyShoes = data.SafetyShoes;
                FloatingCraneVO.ReflectiveJacket = data.ReflectiveJacket;
                FloatingCraneVO.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
                FloatingCraneVO.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                FloatingCraneVO.RecordStatus = data.RecordStatus;
                FloatingCraneVO.CreatedDate = data.CreatedDate;
                FloatingCraneVO.ModifiedBy = data.ModifiedBy;
                FloatingCraneVO.ModifiedDate = data.ModifiedDate;
            }
            return FloatingCraneVO;
        }
        public static FloatingCrane MapToEntity(this FloatingCraneVO floatingcrane)
        {
            FloatingCrane FloatingCrane = new FloatingCrane();
            if (floatingcrane != null)
            {
                FloatingCrane.LicenseRequestID = floatingcrane.LicenseRequestID;
                FloatingCrane.FloatingCraneID = floatingcrane.FloatingCraneID;
                FloatingCrane.QualificationsCompetencies = floatingcrane.QualificationsCompetencies;
                FloatingCrane.EmployQualifiedTrainedPers = floatingcrane.EmployQualifiedTrainedPers;
                FloatingCrane.HardHat = floatingcrane.HardHat;
                FloatingCrane.SafetyShoes = floatingcrane.SafetyShoes;
                FloatingCrane.ReflectiveJacket = floatingcrane.ReflectiveJacket;
                FloatingCrane.SelfInflatingLifeJacket = floatingcrane.SelfInflatingLifeJacket;
                FloatingCrane.QualifyPublicLiabilityInsu = floatingcrane.QualifyPublicLiabilityInsu;
                FloatingCrane.RecordStatus = floatingcrane.RecordStatus;
                FloatingCrane.CreatedDate = floatingcrane.CreatedDate;
                FloatingCrane.ModifiedBy = floatingcrane.ModifiedBy;
                FloatingCrane.ModifiedDate = floatingcrane.ModifiedDate;
            }
            return FloatingCrane;
        }



        public static FloatingCraneVO MapToDTOObj(this IEnumerable<FloatingCrane> floatingCranes)
        {
            var floatingCranesVoList = new FloatingCraneVO();
            if (floatingCranes != null)
            {
                foreach (var data in floatingCranes)
                {
                    floatingCranesVoList.LicenseRequestID = data.LicenseRequestID;
                    floatingCranesVoList.FloatingCraneID = data.FloatingCraneID;
                    floatingCranesVoList.QualificationsCompetencies = data.QualificationsCompetencies;
                    floatingCranesVoList.EmployQualifiedTrainedPers = data.EmployQualifiedTrainedPers;
                    floatingCranesVoList.HardHat = data.HardHat;
                    floatingCranesVoList.SafetyShoes = data.SafetyShoes;
                    floatingCranesVoList.ReflectiveJacket = data.ReflectiveJacket;
                    floatingCranesVoList.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
                    floatingCranesVoList.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                    floatingCranesVoList.RecordStatus = data.RecordStatus;
                    floatingCranesVoList.CreatedDate = data.CreatedDate;
                    floatingCranesVoList.ModifiedBy = data.ModifiedBy;
                    floatingCranesVoList.ModifiedDate = data.ModifiedDate;
                    return floatingCranesVoList;
                }
            }
            return floatingCranesVoList;
        }


        public static List<FloatingCraneVO> MapToDTO(this IEnumerable<FloatingCrane> floatingCranes)
        {
            var floatingCranesVoList = new List<FloatingCraneVO>();
            if (floatingCranes != null)
            {
                foreach (var item in floatingCranes)
                {
                    floatingCranesVoList.Add(item.MapToDTO());
                }
            }
            return floatingCranesVoList;
        }

        public static List<FloatingCrane> MapToEntity(this IEnumerable<FloatingCraneVO> floatingCranesVoList)
        {
            var floatingCranes = new List<FloatingCrane>();
            if (floatingCranesVoList != null)
            {
                foreach (var item in floatingCranesVoList)
                {
                    floatingCranes.Add(item.MapToEntity());
                }
            }
            return floatingCranes;
        }
    }
}
