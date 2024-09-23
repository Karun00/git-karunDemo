using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class DivingMapExtesion
    {
        public static DivingVO MapToDto(this Diving data)
        {
            DivingVO DivingVO = new DivingVO();
            if (data != null)
            {
                DivingVO.LicenseRequestID = data.LicenseRequestID;
                DivingVO.DivingID = data.DivingID;
                DivingVO.QualificationsCompetencies = data.QualificationsCompetencies;
                DivingVO.ProvideDivingPorts = data.ProvideDivingPorts;
                DivingVO.YearsProvidingDiving = data.YearsProvidingDiving;
                DivingVO.RegisteredDepartmentLabour = data.RegisteredDepartmentLabour;
                DivingVO.EquipmentPersProtClothing = data.EquipmentPersProtClothing;
                DivingVO.EquipmentRegisterTestCert = data.EquipmentRegisterTestCert;
                DivingVO.EquipmentIncludeTwoRadioSets = data.EquipmentIncludeTwoRadioSets;
                DivingVO.QualifyPublLiabInsurance = data.QualifyPublLiabInsurance;
                DivingVO.BBBEE = data.BBBEE;
                DivingVO.CreatedBy = data.CreatedBy;
                DivingVO.RecordStatus = data.RecordStatus;
                DivingVO.CreatedDate = data.CreatedDate;
                DivingVO.ModifiedBy = data.ModifiedBy;
                DivingVO.ModifiedDate = data.ModifiedDate;
            }
            return DivingVO;
        }
        public static Diving MapToEntity(this DivingVO divingVo)
        {
            Diving Diving = new Diving();
            if (divingVo != null)
            {
                Diving.LicenseRequestID = divingVo.LicenseRequestID;
                Diving.DivingID = divingVo.DivingID;
                Diving.QualificationsCompetencies = divingVo.QualificationsCompetencies;
                Diving.ProvideDivingPorts = divingVo.ProvideDivingPorts;
                Diving.YearsProvidingDiving = divingVo.YearsProvidingDiving;
                Diving.RegisteredDepartmentLabour = divingVo.RegisteredDepartmentLabour;
                Diving.EquipmentPersProtClothing = divingVo.EquipmentPersProtClothing;
                Diving.EquipmentRegisterTestCert = divingVo.EquipmentRegisterTestCert;
                Diving.EquipmentIncludeTwoRadioSets = divingVo.EquipmentIncludeTwoRadioSets;
                Diving.QualifyPublLiabInsurance = divingVo.QualifyPublLiabInsurance;
                Diving.BBBEE = divingVo.BBBEE;
                Diving.CreatedBy = divingVo.CreatedBy;
                Diving.RecordStatus = divingVo.RecordStatus;
                Diving.CreatedDate = divingVo.CreatedDate;
                Diving.ModifiedBy = divingVo.ModifiedBy;
                Diving.ModifiedDate = divingVo.ModifiedDate;
            }
            return Diving;
        }

        public static List<DivingVO> MapToDto(this IEnumerable<Diving> divings)
        {
            var divingsVoList = new List<DivingVO>();
            if (divings != null)
            {
                foreach (var item in divings)
                {
                    divingsVoList.Add(item.MapToDto());
                }
            }
            return divingsVoList;
        }

        public static DivingVO MapToDtoObj(this IEnumerable<Diving> divings)
        {
            var divingsVoList = new DivingVO();
            if (divings != null)
            {
                foreach (var item in divings)
                {

                    divingsVoList.LicenseRequestID = item.LicenseRequestID;
                    divingsVoList.DivingID = item.DivingID;
                    divingsVoList.QualificationsCompetencies = item.QualificationsCompetencies;
                    divingsVoList.ProvideDivingPorts = item.ProvideDivingPorts;
                    divingsVoList.YearsProvidingDiving = item.YearsProvidingDiving;
                    divingsVoList.RegisteredDepartmentLabour = item.RegisteredDepartmentLabour;
                    divingsVoList.EquipmentPersProtClothing = item.EquipmentPersProtClothing;
                    divingsVoList.EquipmentRegisterTestCert = item.EquipmentRegisterTestCert;
                    divingsVoList.EquipmentIncludeTwoRadioSets = item.EquipmentIncludeTwoRadioSets;
                    divingsVoList.QualifyPublLiabInsurance = item.QualifyPublLiabInsurance;
                    divingsVoList.BBBEE = item.BBBEE;
                    divingsVoList.CreatedBy = item.CreatedBy;
                    divingsVoList.RecordStatus = item.RecordStatus;
                    divingsVoList.CreatedDate = item.CreatedDate;
                    divingsVoList.ModifiedBy = item.ModifiedBy;
                    divingsVoList.ModifiedDate = item.ModifiedDate;
                }
            }
            return divingsVoList;
        }


        public static List<Diving> MapToEntity(this IEnumerable<DivingVO> divingsVoList)
        {
            var bunkerings = new List<Diving>();
            if (divingsVoList != null)
            {
                foreach (var item in divingsVoList)
                {
                    bunkerings.Add(item.MapToEntity());
                }
            }
            return bunkerings;

        }

    }
}
