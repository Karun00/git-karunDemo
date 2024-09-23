using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
namespace IPMS.Domain.DTOS
{
    public static class PestControlMapExtension
    {
        public static PestControlVO MapToDTO(this PestControl data)
        {
            PestControlVO PestControlVO = new PestControlVO();
            if (data != null)
            {
                PestControlVO.PestControlID = data.PestControlID;
                PestControlVO.LicenseRequestID = data.LicenseRequestID;
                PestControlVO.AgricultureDeptrelevant = data.AgricultureDeptrelevant;
                PestControlVO.SAQACertificate = data.SAQACertificate;
                PestControlVO.EmployQualifiedTrainedPers = data.EmployQualifiedTrainedPers;
                PestControlVO.HardHat = data.HardHat;
                PestControlVO.SafetyShoes = data.SafetyShoes;
                PestControlVO.ReflectiveJacket = data.ReflectiveJacket;
                PestControlVO.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
                PestControlVO.FaceMasks = data.FaceMasks;
                PestControlVO.ProtectiveGloves = data.ProtectiveGloves;
                PestControlVO.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                PestControlVO.CreatedBy = data.CreatedBy;
                PestControlVO.RecordStatus = data.RecordStatus;
                PestControlVO.CreatedDate = data.CreatedDate;
                PestControlVO.ModifiedBy = data.ModifiedBy;
                PestControlVO.ModifiedDate = data.ModifiedDate;
            }
            return PestControlVO;
        }
        public static PestControl MapToEntity(this PestControlVO data)
        {
            PestControl PestControl = new PestControl();
            if (data != null)
            {
                PestControl.PestControlID = data.PestControlID;
                PestControl.LicenseRequestID = data.LicenseRequestID;
                PestControl.AgricultureDeptrelevant = data.AgricultureDeptrelevant;
                PestControl.SAQACertificate = data.SAQACertificate;
                PestControl.EmployQualifiedTrainedPers = data.EmployQualifiedTrainedPers;
                PestControl.HardHat = data.HardHat;
                PestControl.SafetyShoes = data.SafetyShoes;
                PestControl.ReflectiveJacket = data.ReflectiveJacket;
                PestControl.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
                PestControl.FaceMasks = data.FaceMasks;
                PestControl.ProtectiveGloves = data.ProtectiveGloves;
                PestControl.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                PestControl.CreatedBy = data.CreatedBy;
                PestControl.RecordStatus = data.RecordStatus;
                PestControl.CreatedDate = data.CreatedDate;
                PestControl.ModifiedBy = data.ModifiedBy;
                PestControl.ModifiedDate = data.ModifiedDate;
            }
            return PestControl;
        }

        public static PestControlVO MapToDTOObj(this IEnumerable<PestControl> pestControls)
        {
            var pestControlsVoList = new PestControlVO();
            if (pestControls != null)
            {
                foreach (var data in pestControls)
                {
                    pestControlsVoList.PestControlID = data.PestControlID;
                    pestControlsVoList.LicenseRequestID = data.LicenseRequestID;
                    pestControlsVoList.AgricultureDeptrelevant = data.AgricultureDeptrelevant;
                    pestControlsVoList.SAQACertificate = data.SAQACertificate;
                    pestControlsVoList.EmployQualifiedTrainedPers = data.EmployQualifiedTrainedPers;
                    pestControlsVoList.HardHat = data.HardHat;
                    pestControlsVoList.SafetyShoes = data.SafetyShoes;
                    pestControlsVoList.ReflectiveJacket = data.ReflectiveJacket;
                    pestControlsVoList.SelfInflatingLifeJacket = data.SelfInflatingLifeJacket;
                    pestControlsVoList.FaceMasks = data.FaceMasks;
                    pestControlsVoList.ProtectiveGloves = data.ProtectiveGloves;
                    pestControlsVoList.QualifyPublicLiabilityInsu = data.QualifyPublicLiabilityInsu;
                    pestControlsVoList.CreatedBy = data.CreatedBy;
                    pestControlsVoList.RecordStatus = data.RecordStatus;
                    pestControlsVoList.CreatedDate = data.CreatedDate;
                    pestControlsVoList.ModifiedBy = data.ModifiedBy;
                    pestControlsVoList.ModifiedDate = data.ModifiedDate;

                }
            }
            return pestControlsVoList;
        }



        public static List<PestControlVO> MapToDTO(this IEnumerable<PestControl> pestControls)
        {
            var pestControlsVoList = new List<PestControlVO>();
            if (pestControls != null)
            {
                foreach (var item in pestControls)
                {
                    pestControlsVoList.Add(item.MapToDTO());
                }
            }
            return pestControlsVoList;
        }

        public static List<PestControl> MapToEntity(this IEnumerable<PestControlVO> pestControlsVoList)
        {
            var pestControls = new List<PestControl>();
            if (pestControlsVoList != null)
            {
                foreach (var item in pestControlsVoList)
                {
                    pestControls.Add(item.MapToEntity());
                }
            }
            return pestControls;
        }
    }
}
