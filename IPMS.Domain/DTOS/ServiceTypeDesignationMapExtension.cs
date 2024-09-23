using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ServiceTypeDesignationMapExtension
    {
        public static List<ServiceTypeDesignation> MapToEntity(this List<ServiceTypeDesignationVO> vos)
        {
            List<ServiceTypeDesignation> ServiceTypeDesignation = new List<ServiceTypeDesignation>();
            if (vos != null)
            {
                foreach (var stvo in vos)
                {
                    ServiceTypeDesignation.Add(stvo.MapToEntity());
                }
            }
            return ServiceTypeDesignation;
        }

        public static List<ServiceTypeDesignationVO> MapToDto(this IEnumerable<ServiceTypeDesignation> entities)
        {
            List<ServiceTypeDesignationVO> ServiceTypeDesignationvolist = new List<ServiceTypeDesignationVO>();
            if (entities != null)
            {
                foreach (var stvo in entities)
                {
                    ServiceTypeDesignationvolist.Add(stvo.MapToDto());
                }
            }
            return ServiceTypeDesignationvolist;
        }

        public static ServiceTypeDesignation MapToEntity(this ServiceTypeDesignationVO vo)
        {
            ServiceTypeDesignation std = new ServiceTypeDesignation();
            if (vo != null)
            {
                std.ServiceTypeDesignationID = vo.ServiceTypeDesignationID;
                std.ServiceTypeID = vo.ServiceTypeID;
                std.PortCode = vo.PortCode;
                std.DesignationCode = vo.DesignationCode;
                std.CraftType = vo.CraftType;
                std.RecordStatus = vo.RecordStatus;
                std.CreatedBy = vo.CreatedBy;
                std.CreatedDate = vo.CreatedDate;
                std.ModifiedBy = vo.ModifiedBy;
                std.ModifiedDate = vo.ModifiedDate;
            }
            return std;
        }

        public static ServiceTypeDesignationVO MapToDto(this ServiceTypeDesignation data)
        {
            ServiceTypeDesignationVO stdvo = new ServiceTypeDesignationVO();
            if (data != null)
            {
                stdvo.ServiceTypeDesignationID = data.ServiceTypeDesignationID;
                stdvo.ServiceTypeID = data.ServiceTypeID;
                stdvo.PortCode = data.PortCode;
                stdvo.DesignationCode = data.DesignationCode;
                stdvo.CraftType = string.IsNullOrEmpty(data.CraftType) ? "" : data.CraftType;
                stdvo.CreatedDate = data.CreatedDate;
                stdvo.CreatedBy = data.CreatedBy;
                stdvo.RecordStatus = data.RecordStatus;
                stdvo.ModifiedBy = data.ModifiedBy;
                stdvo.ModifiedDate = data.ModifiedDate;
            }
            return stdvo;
        }
    }
}
