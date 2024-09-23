using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
namespace IPMS.Domain.DTOS
{
    public static class PollutionControlMapExtension
    {
        public static PollutionControlVO MapToDTO(this PollutionControl data)
        {
            PollutionControlVO PollutionControlVO = new PollutionControlVO();
            PollutionControlVO.LicenseRequestID = data.LicenseRequestID;
            PollutionControlVO.PollutionControlID = data.PollutionControlID;
            PollutionControlVO.MemberInstituteWasteMgnt = data.MemberInstituteWasteMgnt;
            PollutionControlVO.SAQACertificate = data.SAQACertificate;
            PollutionControlVO.EmployQualifiedTrainedPers = data.EmployQualifiedTrainedPers;
            PollutionControlVO.HardHat = data.HardHat;
            PollutionControlVO.SafetyShoes = data.SafetyShoes;
            PollutionControlVO.ReflectiveJacket = data.ReflectiveJacket;
            PollutionControlVO.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
            PollutionControlVO.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
            PollutionControlVO.CreatedBy = data.CreatedBy;
            PollutionControlVO.RecordStatus = data.RecordStatus;
            PollutionControlVO.CreatedDate = data.CreatedDate;
            PollutionControlVO.ModifiedBy = data.ModifiedBy;
            PollutionControlVO.ModifiedDate = data.ModifiedDate;
            return PollutionControlVO;
        }
        public static PollutionControl MapToEntity(this PollutionControlVO data)
        {
            PollutionControl PollutionControl = new PollutionControl();
            PollutionControl.LicenseRequestID = data.LicenseRequestID;
            PollutionControl.PollutionControlID = data.PollutionControlID;
            PollutionControl.MemberInstituteWasteMgnt = data.MemberInstituteWasteMgnt;
            PollutionControl.SAQACertificate = data.SAQACertificate;
            PollutionControl.EmployQualifiedTrainedPers = data.EmployQualifiedTrainedPers;
            PollutionControl.HardHat = data.HardHat;
            PollutionControl.SafetyShoes = data.SafetyShoes;
            PollutionControl.ReflectiveJacket = data.ReflectiveJacket;
            PollutionControl.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
            PollutionControl.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
            PollutionControl.CreatedBy = data.CreatedBy;
            PollutionControl.RecordStatus = data.RecordStatus;
            PollutionControl.CreatedDate = data.CreatedDate;
            PollutionControl.ModifiedBy = data.ModifiedBy;
            PollutionControl.ModifiedDate = data.ModifiedDate;
            return PollutionControl;
        }


        public static PollutionControlVO MapToDTOObj(this IEnumerable<PollutionControl> pollutionControls)
        {
            var pollutionControlsVoList = new PollutionControlVO();
            foreach (var data in pollutionControls)
            {
                pollutionControlsVoList.LicenseRequestID = data.LicenseRequestID;
                pollutionControlsVoList.PollutionControlID = data.PollutionControlID;
                pollutionControlsVoList.MemberInstituteWasteMgnt = data.MemberInstituteWasteMgnt;
                pollutionControlsVoList.SAQACertificate = data.SAQACertificate;
                pollutionControlsVoList.EmployQualifiedTrainedPers = data.EmployQualifiedTrainedPers;
                pollutionControlsVoList.HardHat = data.HardHat;
                pollutionControlsVoList.SafetyShoes = data.SafetyShoes;
                pollutionControlsVoList.ReflectiveJacket = data.ReflectiveJacket;
                pollutionControlsVoList.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
                pollutionControlsVoList.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                pollutionControlsVoList.CreatedBy = data.CreatedBy;
                pollutionControlsVoList.RecordStatus = data.RecordStatus;
                pollutionControlsVoList.CreatedDate = data.CreatedDate;
                pollutionControlsVoList.ModifiedBy = data.ModifiedBy;
                pollutionControlsVoList.ModifiedDate = data.ModifiedDate;
            }
            return pollutionControlsVoList;
        }


        public static List<PollutionControlVO> MapToDTO(this IEnumerable<PollutionControl> pollutionControls)
        {
            var pollutionControlsVoList = new List<PollutionControlVO>();
            foreach (var item in pollutionControls)
            {
                pollutionControlsVoList.Add(item.MapToDTO());
            }
            return pollutionControlsVoList;
        }

        public static List<PollutionControl> MapToEntity(this IEnumerable<PollutionControlVO> pollutionControlsVoList)
        {
            var pollutionControls = new List<PollutionControl>();
            foreach (var item in pollutionControlsVoList)
            {
                pollutionControls.Add(item.MapToEntity());
            }
            return pollutionControls;
        }
    }
}
