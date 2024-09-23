using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ContractorPermitApplicationDetailsMapExtension
    {
        public static List<ContractorPermitApplicationDetails> MapToEntity(this IEnumerable<ContractorPermitApplicationDetailsVO> vos)
        {
            List<ContractorPermitApplicationDetails> entities = new List<ContractorPermitApplicationDetails>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<ContractorPermitApplicationDetailsVO> MapToDTO(this IEnumerable<ContractorPermitApplicationDetails> entities)
        {
            List<ContractorPermitApplicationDetailsVO> vos = new List<ContractorPermitApplicationDetailsVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static ContractorPermitApplicationDetailsVO MapToDTO(this ContractorPermitApplicationDetails data)
        {
            ContractorPermitApplicationDetailsVO Vo = new ContractorPermitApplicationDetailsVO();
            if (data != null)
            {
                Vo.ContractorPermitApplicationID = data.ContractorPermitApplicationID;
                Vo.PermitRequestID = data.PermitRequestID;
                Vo.ContractorCompanyName = data.ContractorCompanyName;
                Vo.ContractorCompanyManager = data.ContractorCompanyManager;
                Vo.Department = data.Department;
                Vo.TelephoneNumber = data.TelephoneNumber;
                Vo.SubContractorCompanyName = data.SubContractorCompanyName;
                Vo.SubContractorTelephoneNumber = data.SubContractorTelephoneNumber;              
                Vo.RecordStatus = data.RecordStatus;
                Vo.CreatedBy = data.CreatedBy;
                Vo.CreatedDate = data.CreatedDate;
                Vo.ModifiedBy = data.ModifiedBy;
                Vo.CreatedBy = data.CreatedBy;
                Vo.ModifiedDate = data.ModifiedDate;
            }
            return Vo;
        }
        public static ContractorPermitApplicationDetails MapToEntity(this ContractorPermitApplicationDetailsVO VO)
        {
            ContractorPermitApplicationDetails data = new ContractorPermitApplicationDetails();
            if (VO != null)
            {
                data.ContractorPermitApplicationID = VO.ContractorPermitApplicationID;
                data.PermitRequestID = VO.PermitRequestID;
                data.ContractorCompanyName = VO.ContractorCompanyName;
                data.ContractorCompanyManager = VO.ContractorCompanyManager;
                data.Department = VO.Department;
                data.TelephoneNumber = VO.TelephoneNumber;
                data.SubContractorCompanyName = VO.SubContractorCompanyName;
                data.SubContractorTelephoneNumber = VO.SubContractorTelephoneNumber;              
                data.RecordStatus = VO.RecordStatus;
                data.CreatedBy = VO.CreatedBy;
                data.CreatedDate = VO.CreatedDate;
                data.ModifiedBy = VO.ModifiedBy;
                data.CreatedBy = VO.CreatedBy;
                data.ModifiedDate = VO.ModifiedDate;
            }
            return data;
        }

        public static ContractorPermitApplicationDetailsVO MapToDTOObj(this IEnumerable<ContractorPermitApplicationDetails> ContractorPermitApplicationDetails)
        {
            var ContractorPermitApplicationDetailsVOList = new ContractorPermitApplicationDetailsVO();
            if (ContractorPermitApplicationDetails != null)
            {
                foreach (var data in ContractorPermitApplicationDetails)
                {
                    ContractorPermitApplicationDetailsVOList.ContractorPermitApplicationID = data.ContractorPermitApplicationID;
                    ContractorPermitApplicationDetailsVOList.PermitRequestID = data.PermitRequestID;
                    ContractorPermitApplicationDetailsVOList.ContractorCompanyName = data.ContractorCompanyName;
                    ContractorPermitApplicationDetailsVOList.Department = data.Department;
                    ContractorPermitApplicationDetailsVOList.ContractorCompanyManager = data.ContractorCompanyManager;
                    ContractorPermitApplicationDetailsVOList.TelephoneNumber = data.TelephoneNumber;
                    ContractorPermitApplicationDetailsVOList.SubContractorCompanyName = data.SubContractorCompanyName;
                    ContractorPermitApplicationDetailsVOList.SubContractorTelephoneNumber = data.SubContractorTelephoneNumber;                    
                    ContractorPermitApplicationDetailsVOList.RecordStatus = data.RecordStatus;
                    ContractorPermitApplicationDetailsVOList.CreatedBy = data.CreatedBy;
                    ContractorPermitApplicationDetailsVOList.CreatedDate = data.CreatedDate;
                    ContractorPermitApplicationDetailsVOList.ModifiedBy = data.ModifiedBy;
                    ContractorPermitApplicationDetailsVOList.CreatedBy = data.CreatedBy;
                    ContractorPermitApplicationDetailsVOList.ModifiedDate = data.ModifiedDate;
                }
            }
            return ContractorPermitApplicationDetailsVOList;
        }
    }
}

