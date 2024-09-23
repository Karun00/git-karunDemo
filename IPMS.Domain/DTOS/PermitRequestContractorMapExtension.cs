using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PermitRequestContractorMapExtension
    {
        public static List<PermitRequestContractor> MapToEntity(this IEnumerable<PermitRequestContractorVO> vos)
        {
            List<PermitRequestContractor> entities = new List<PermitRequestContractor>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<PermitRequestContractorVO> MapToDTO(this IEnumerable<PermitRequestContractor> entities)
        {
            List<PermitRequestContractorVO> vos = new List<PermitRequestContractorVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;     

        }
        public static PermitRequestContractorVO MapToDTO(this PermitRequestContractor data)
        {
            PermitRequestContractorVO Vo = new PermitRequestContractorVO();
            if (data != null)
            {
                Vo.PermitRequestContractorID = data.PermitRequestContractorID;
                Vo.PermitRequestID = data.PermitRequestID;
                Vo.CompanyName = data.CompanyName;
                Vo.ContractNo = data.ContractNo;
                Vo.ContractManagerName = data.ContractManagerName;
                Vo.ContractDuration = data.ContractDuration;
                Vo.ServiceCompanyName = data.ServiceCompanyName;
                Vo.ResponsibleManager = data.ResponsibleManager;
                Vo.ContactNo = data.ContactNo;
                Vo.MobileNo = data.MobileNo;
                Vo.RecordStatus = data.RecordStatus;
                Vo.CreatedBy = data.CreatedBy;
                Vo.CreatedDate = data.CreatedDate;
                Vo.ModifiedBy = data.ModifiedBy;
                Vo.CreatedBy = data.CreatedBy;
                Vo.ModifiedDate = data.ModifiedDate;
            }
            return Vo;
        }
        public static PermitRequestContractor MapToEntity(this PermitRequestContractorVO VO)
        {
            PermitRequestContractor data = new PermitRequestContractor();
            if (VO != null)
            {
                data.PermitRequestContractorID = VO.PermitRequestContractorID;
                data.PermitRequestID = VO.PermitRequestID;
                data.CompanyName = VO.CompanyName;
                data.ContractNo = VO.ContractNo;
                data.ContractManagerName = VO.ContractManagerName;
                data.ContractDuration = VO.ContractDuration;
                data.ServiceCompanyName = VO.ServiceCompanyName;
                data.ResponsibleManager = VO.ResponsibleManager;
                data.ContactNo = VO.ContactNo;
                data.MobileNo = VO.MobileNo;
                data.RecordStatus = VO.RecordStatus;
                data.CreatedBy = VO.CreatedBy;
                data.CreatedDate = VO.CreatedDate;
                data.ModifiedBy = VO.ModifiedBy;
                data.CreatedBy = VO.CreatedBy;
                data.ModifiedDate = VO.ModifiedDate;
            }
            return data;
        }
    
     public static PermitRequestContractorVO MapToDTOObj(this IEnumerable<PermitRequestContractor> PermitRequestContractor)
        {
            var PermitRequestContractorVOList = new PermitRequestContractorVO();
            if (PermitRequestContractor != null)
         {
             foreach (var data in PermitRequestContractor)
             {
                 PermitRequestContractorVOList.PermitRequestContractorID = data.PermitRequestContractorID;
                 PermitRequestContractorVOList.PermitRequestID = data.PermitRequestID;
                 PermitRequestContractorVOList.CompanyName = data.CompanyName;
                 PermitRequestContractorVOList.ContractNo = data.ContractNo;
                 PermitRequestContractorVOList.ContractManagerName = data.ContractManagerName;
                 PermitRequestContractorVOList.ContractDuration = data.ContractDuration;
                 PermitRequestContractorVOList.ServiceCompanyName = data.ServiceCompanyName;
                 PermitRequestContractorVOList.ResponsibleManager = data.ResponsibleManager;
                 PermitRequestContractorVOList.ContactNo = data.ContactNo;
                 PermitRequestContractorVOList.MobileNo = data.MobileNo;
                 PermitRequestContractorVOList.RecordStatus = data.RecordStatus;
                 PermitRequestContractorVOList.CreatedBy = data.CreatedBy;
                 PermitRequestContractorVOList.CreatedDate = data.CreatedDate;
                 PermitRequestContractorVOList.ModifiedBy = data.ModifiedBy;
                 PermitRequestContractorVOList.CreatedBy = data.CreatedBy;
                 PermitRequestContractorVOList.ModifiedDate = data.ModifiedDate;
             }
         }
         return PermitRequestContractorVOList;
        }
    }
}
