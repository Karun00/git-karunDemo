using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;


namespace IPMS.Domain.DTOS
{
    public static class RolePrivilegeMapExtension
    {
        public static List<RoleVO> MapToDto(this IEnumerable<Role> portList)
        {
            List<RoleVO> portvoList = new List<RoleVO>();
            if (portList != null)
            {
                foreach (var data in portList)
                {
                    portvoList.Add(data.MapToDto());

                }
            }
            return portvoList;
        }

        public static List<Role> MapToEntity(this IEnumerable<RoleVO> portVoList)
        {
            List<Role> portList = new List<Role>();
            if (portVoList != null)
            {
                foreach (var data in portVoList)
                {
                    portList.Add(data.MapToEntity());

                }
            }
            return portList;
        }

        public static RoleVO MapToDto(this Role data)
        {
            RoleVO RPVO = new RoleVO();
            if (data != null)
            {
                RPVO.RoleID = data.RoleID;
                RPVO.RoleCode = data.RoleCode;
                RPVO.RoleName = data.RoleName;
                RPVO.RoleDescription = data.RoleDescription;
                RPVO.RecordStatus = data.RecordStatus;
                RPVO.CreatedBy = data.CreatedBy;
                RPVO.CreatedDate = data.CreatedDate;
                RPVO.ModifiedBy = data.ModifiedBy;
                RPVO.ModifiedDate = data.ModifiedDate;
                //  RPVO.RolePrivileges = data.  
            }
            return RPVO;
        }
        public static Role MapToEntity(this RoleVO roleVo)
        {
            Role role = new Role();
            if (roleVo != null)
            {
                role.RoleID = roleVo.RoleID;
                role.RoleCode = roleVo.RoleCode;
                role.RoleName = roleVo.RoleName;
                role.RoleDescription = roleVo.RoleDescription;
                role.RecordStatus = roleVo.RecordStatus;
                role.CreatedBy = roleVo.CreatedBy;
                role.CreatedDate = roleVo.CreatedDate;
                role.ModifiedBy = roleVo.ModifiedBy;
                role.ModifiedDate = roleVo.ModifiedDate;
            }
            return role;
        }



        public static List<RolePrivilege> MapToEntity(this IEnumerable<RolePrivilegeVO> rolePrivilegeVoList)
        {
            List<RolePrivilege> roleprivleges = new List<RolePrivilege>();
            if (rolePrivilegeVoList != null)
            {
                foreach (var item in rolePrivilegeVoList)
                {
                    roleprivleges.Add(item.MapToEntity());
                }
            }
            return roleprivleges;
        }

        public static RolePrivilege MapToEntity(this RolePrivilegeVO data)
        {

            return new RolePrivilege
            {
                RoleID = data.RoleID ,
                EntityID = data.EntityID,
                SubCatCode = data.SubCatCode,
                RecordStatus = data.RecordStatus,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }

        public static Role MaoToEntity(this RoleVO rpVo)
        {
            Role data = new Role();
            if (rpVo != null)
            {
                data.RoleID = rpVo.RoleID;
                data.RoleCode = rpVo.RoleCode;
                data.RoleName = rpVo.RoleName;
                data.RoleDescription = rpVo.RoleDescription;
                data.RecordStatus = rpVo.RecordStatus;
                data.CreatedBy = rpVo.CreatedBy;
                data.CreatedDate = rpVo.CreatedDate;
                data.ModifiedBy = rpVo.ModifiedBy;
                data.ModifiedDate = rpVo.ModifiedDate;
            }
            return data;
        }
    }
}
