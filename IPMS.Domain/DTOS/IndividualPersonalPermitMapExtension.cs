using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class IndividualPersonalPermitMapExtension
    {
        public static List<IndividualPersonalPermit> MapToEntity(this IEnumerable<IndividualPersonalPermitVO> vos)
        {
            List<IndividualPersonalPermit> entities = new List<IndividualPersonalPermit>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<IndividualPersonalPermitVO> MapToDTO(this IEnumerable<IndividualPersonalPermit> entities)
        {
            List<IndividualPersonalPermitVO> vos = new List<IndividualPersonalPermitVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static IndividualPersonalPermitVO MapToDTO(this  IndividualPersonalPermit data)
        {
            IndividualPersonalPermitVO Vo = new IndividualPersonalPermitVO();
            if (data != null)
            {
                Vo.IndividualPersonalPermitID = data.IndividualPersonalPermitID;
                Vo.PermitRequestID = data.PermitRequestID;                              
                Vo.permittype = data.permittype;
                Vo.IndividualTemporaryPermits = data.IndividualTemporaryPermits;
                Vo.IndividualPermanentPermits = data.IndividualPermanentPermits;
                Vo.TempFromDate= data.TempFromDate;
                Vo.TempToDate = data.TempToDate;
                Vo.PerFromDate = data.PerFromDate;
                Vo.PerToDate = data.PerToDate;
                Vo.IsCamera = data.IsCamera;
                Vo.CameraDetails = data.CameraDetails;
                Vo.IsTools = data.IsTools;
                Vo.ToolsDetails = data.ToolsDetails;
                Vo.IsSpclEquipment = data.IsSpclEquipment;
                Vo.SpclEquipmentDetails = data.SpclEquipmentDetails;
                Vo.AuthorisedSurname = data.AuthorisedSurname;
                Vo.TelephoneWork = data.TelephoneWork;
                Vo.AuthorisedMobile = data.AuthorisedMobile;
                Vo.AuthorisedIdentityNumber = data.AuthorisedIdentityNumber;
                Vo.AuthorisedEmail = data.AuthorisedEmail;
                Vo.AuthorisedSignature = data.AuthorisedSignature;
                Vo.SignatoryDate = data.SignatoryDate;
                Vo.ContractorTemporaryPermits = data.ContractorTemporaryPermits;
                Vo.ContractorPermanentPermits = data.ContractorPermanentPermits;
                Vo.ContractorTempFromDate = data.ContractorTempFromDate;
                Vo.ContractorTempToDate = data.ContractorTempToDate;
                Vo.ContractorPerFromDate = data.ContractorPerFromDate;
                Vo.ContractorPerToDate = data.ContractorPerToDate;



            }

            return Vo;
        }
        public static IndividualPersonalPermit MapToEntity(this IndividualPersonalPermitVO VO)
        {
            IndividualPersonalPermit data = new IndividualPersonalPermit();
            if (VO != null)
            {
                data.IndividualPersonalPermitID = VO.IndividualPersonalPermitID;
                data.PermitRequestID = VO.PermitRequestID;
                data.permittype = VO.permittype;
                data.IndividualTemporaryPermits = VO.IndividualTemporaryPermits;
                data.IndividualPermanentPermits = VO.IndividualPermanentPermits;
                data.TempFromDate = VO.TempFromDate;
                data.TempToDate = VO.TempToDate;
                data.PerFromDate = VO.PerFromDate;
                data.PerToDate = VO.PerToDate;
                data.IsCamera = VO.IsCamera;
                data.CameraDetails = VO.CameraDetails;
                data.IsTools = VO.IsTools;
                data.ToolsDetails = VO.ToolsDetails;
                data.IsSpclEquipment = VO.IsSpclEquipment;
                data.SpclEquipmentDetails = VO.SpclEquipmentDetails;
                data.AuthorisedSurname = VO.AuthorisedSurname;
                data.TelephoneWork = VO.TelephoneWork;
                data.AuthorisedMobile = VO.AuthorisedMobile;
                data.AuthorisedIdentityNumber = VO.AuthorisedIdentityNumber;
                data.AuthorisedEmail = VO.AuthorisedEmail;
                data.AuthorisedSignature = VO.AuthorisedSignature;
                data.SignatoryDate = VO.SignatoryDate;
                data.ContractorTemporaryPermits = VO.ContractorTemporaryPermits;
                data.ContractorPermanentPermits = VO.ContractorPermanentPermits;
                data.ContractorTempFromDate = VO.ContractorTempFromDate;
                data.ContractorTempToDate = VO.ContractorTempToDate;
                data.ContractorPerFromDate = VO.ContractorPerFromDate;
                data.ContractorPerToDate = VO.ContractorPerToDate;

            }
            return data;
        }

        public static IndividualPersonalPermitVO MapToDTOObj(this IEnumerable<IndividualPersonalPermit> PermitRequestContractor)
        {
            var IndividualPersonalPermitVOList = new IndividualPersonalPermitVO();
            if (PermitRequestContractor != null)
            {
                foreach (var data in PermitRequestContractor)
                {
                    IndividualPersonalPermitVOList.IndividualPersonalPermitID = data.IndividualPersonalPermitID;
                    IndividualPersonalPermitVOList.PermitRequestID = data.PermitRequestID;
                    IndividualPersonalPermitVOList.permittype = data.permittype;
                    IndividualPersonalPermitVOList.IndividualTemporaryPermits = data.IndividualTemporaryPermits;
                    IndividualPersonalPermitVOList.IndividualPermanentPermits = data.IndividualPermanentPermits;
                    IndividualPersonalPermitVOList.TempFromDate = data.TempFromDate;
                    IndividualPersonalPermitVOList.TempToDate = data.TempToDate;
                    IndividualPersonalPermitVOList.PerFromDate = data.PerFromDate;
                    IndividualPersonalPermitVOList.PerToDate = data.PerToDate;
                    IndividualPersonalPermitVOList.IsCamera = data.IsCamera;
                    IndividualPersonalPermitVOList.CameraDetails = data.CameraDetails;
                    IndividualPersonalPermitVOList.IsTools = data.IsTools;
                    IndividualPersonalPermitVOList.ToolsDetails = data.ToolsDetails;
                    IndividualPersonalPermitVOList.IsSpclEquipment = data.IsSpclEquipment;
                    IndividualPersonalPermitVOList.SpclEquipmentDetails = data.SpclEquipmentDetails;
                    IndividualPersonalPermitVOList.AuthorisedSurname = data.AuthorisedSurname;
                    IndividualPersonalPermitVOList.TelephoneWork = data.TelephoneWork;
                    IndividualPersonalPermitVOList.AuthorisedMobile = data.AuthorisedMobile;
                    IndividualPersonalPermitVOList.AuthorisedIdentityNumber = data.AuthorisedIdentityNumber;
                    IndividualPersonalPermitVOList.AuthorisedEmail = data.AuthorisedEmail;
                    IndividualPersonalPermitVOList.AuthorisedSignature = data.AuthorisedSignature;
                    IndividualPersonalPermitVOList.SignatoryDate = data.SignatoryDate;
                    IndividualPersonalPermitVOList.ContractorTemporaryPermits = data.ContractorTemporaryPermits;
                    IndividualPersonalPermitVOList.ContractorPermanentPermits = data.ContractorPermanentPermits;
                    IndividualPersonalPermitVOList.ContractorTempFromDate = data.ContractorTempFromDate;
                    IndividualPersonalPermitVOList.ContractorTempToDate = data.ContractorTempToDate;
                    IndividualPersonalPermitVOList.ContractorPerFromDate = data.ContractorPerFromDate;
                    IndividualPersonalPermitVOList.ContractorPerToDate = data.ContractorPerToDate;
                 
                }
            }
            return IndividualPersonalPermitVOList;
        }


    }
}
