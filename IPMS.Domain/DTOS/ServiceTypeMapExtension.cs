using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ServiceTypeMapExtension
    {
        public static List<ServiceType> MapToEntity(this List<ServiceTypeVO> vos)
        {
            List<ServiceType> servicetype = new List<ServiceType>();
            if (vos != null)
            {
                foreach (var stvo in vos)
                {
                    servicetype.Add(stvo.MapToEntity());
                }
            }
            return servicetype;
        }

        public static List<ServiceTypeVO> MapToDto(this List<ServiceType> entities)
        {
            List<ServiceTypeVO> servicetypevolist = new List<ServiceTypeVO>();
            if (entities != null)
            {
                foreach (var stvo in entities)
                {
                    servicetypevolist.Add(stvo.MapToDto());
                }
            }
            return servicetypevolist;
        }

        public static ServiceType MapToEntity(this ServiceTypeVO vo)
        {
            ServiceType servicetype = new ServiceType();
            if (vo != null)
            {
                servicetype.ServiceTypeID = vo.ServiceTypeID;
                servicetype.ServiceTypeName = vo.ServiceTypeName;
                servicetype.ServiceTypeCode = vo.ServiceTypeCode.ToUpper();
                servicetype.IsServiceType = "N";
                servicetype.ServiceUOM = vo.ServiceUOM;
                servicetype.IsCraft = vo.IsCraft;
                servicetype.RecordStatus = vo.RecordStatus;
                servicetype.CreatedBy = vo.CreatedBy;
                servicetype.CreatedDate = vo.CreatedDate;
                servicetype.ModifiedBy = vo.ModifiedBy;
                servicetype.ModifiedDate = vo.ModifiedDate;
            }
            return servicetype;
        }

        public static ServiceTypeVO MapToDto(this ServiceType data)
        {
            ServiceTypeVO vo = new ServiceTypeVO();
            if (data != null)
            {
                vo.ServiceTypeID = data.ServiceTypeID;
                vo.ServiceTypeName = data.ServiceTypeName;
                vo.IsServiceType = data.IsServiceType;
                vo.IsCraft = data.IsCraft;
                vo.RecordStatus = data.RecordStatus;
                vo.ServiceTypeCode = data.ServiceTypeCode.ToUpper();
                vo.IsServiceType = data.IsServiceType;
                vo.ServiceUOM = data.ServiceUOM;
                vo.RecordStatus = data.RecordStatus;
                vo.CreatedBy = data.CreatedBy;
                vo.CreatedDate = data.CreatedDate;
                vo.ModifiedBy = data.ModifiedBy;
                vo.ModifiedDate = data.ModifiedDate;
                vo.ServiceTypeDesignations = data.ServiceTypeDesignations.MapToDto();
            }
            return vo;
        }
    }
}
