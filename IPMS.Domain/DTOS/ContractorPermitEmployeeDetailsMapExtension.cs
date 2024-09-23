using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ContractorPermitEmployeeDetailsMapExtension
    {
        public static List<ContractorPermitEmployeeDetails> MapToEntity(this IEnumerable<ContractorPermitEmployeeDetailsVO> vos)
        {
            List<ContractorPermitEmployeeDetails> entities = new List<ContractorPermitEmployeeDetails>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<ContractorPermitEmployeeDetailsVO> MapToDTO(this IEnumerable<ContractorPermitEmployeeDetails> entities)
        {
            List<ContractorPermitEmployeeDetailsVO> vos = new List<ContractorPermitEmployeeDetailsVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static ContractorPermitEmployeeDetailsVO MapToDTO(this ContractorPermitEmployeeDetails data)
        {
            ContractorPermitEmployeeDetailsVO Vo = new ContractorPermitEmployeeDetailsVO();
            if (data != null)
            {
                Vo.PermitRequestID = data.PermitRequestID;
                Vo.ContractorPermitEmployeeID = data.ContractorPermitEmployeeID;
                Vo.EmployeeName = data.EmployeeName;
                Vo.IDNumber = data.IDNumber;
                Vo.JobTitle = data.JobTitle;
                Vo.CriminalRecord = data.CriminalRecord;
                Vo.EmpSignature = data.EmpSignature;
                Vo.RecordStatus = data.RecordStatus;
                
            }
            return Vo;
        }
        public static ContractorPermitEmployeeDetails MapToEntity(this ContractorPermitEmployeeDetailsVO VO)
        {
            ContractorPermitEmployeeDetails data = new ContractorPermitEmployeeDetails();
            if (VO != null)
            {
                data.PermitRequestID = VO.PermitRequestID;
                data.ContractorPermitEmployeeID = VO.ContractorPermitEmployeeID;
                data.EmployeeName = VO.EmployeeName;
                data.IDNumber = VO.IDNumber;
                data.JobTitle = VO.JobTitle;
                data.CriminalRecord = VO.CriminalRecord;
                data.EmpSignature = VO.EmpSignature;
                data.RecordStatus = VO.RecordStatus;
               
            }
            return data;
        }

        public static ContractorPermitEmployeeDetailsVO MapToDTOObj(this IEnumerable<ContractorPermitEmployeeDetails> ContractorPermitApplicationDetails)
        {
            var ContractorPermitApplicationDetailsVOList = new ContractorPermitEmployeeDetailsVO();
            if (ContractorPermitApplicationDetails != null)
            {
                foreach (var data in ContractorPermitApplicationDetails)
                {
                    ContractorPermitApplicationDetailsVOList.PermitRequestID = data.PermitRequestID;
                    ContractorPermitApplicationDetailsVOList.ContractorPermitEmployeeID = data.ContractorPermitEmployeeID;
                    ContractorPermitApplicationDetailsVOList.EmployeeName = data.EmployeeName;
                    ContractorPermitApplicationDetailsVOList.IDNumber = data.IDNumber;
                    ContractorPermitApplicationDetailsVOList.JobTitle = data.JobTitle;
                    ContractorPermitApplicationDetailsVOList.CriminalRecord = data.CriminalRecord;
                    ContractorPermitApplicationDetailsVOList.EmpSignature = data.EmpSignature;
                    ContractorPermitApplicationDetailsVOList.RecordStatus = data.RecordStatus;
                   
                }
            }
            return ContractorPermitApplicationDetailsVOList;
        }
    }
}


