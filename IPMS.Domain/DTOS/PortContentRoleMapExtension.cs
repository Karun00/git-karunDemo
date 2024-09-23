using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PortContentRoleMapExtension
    {
        public static List<PortContentRoleVO> MapToDTO(this IEnumerable<PortContentRole> porcontentroleList)
        {
            List<PortContentRoleVO> portcontentrolevoList = new List<PortContentRoleVO>();

            foreach (var data in porcontentroleList)
            {
                portcontentrolevoList.Add(data.MapToDTO());
            }
            return portcontentrolevoList;
        }

        public static List<PortContentRole> MapToEntity(this IEnumerable<PortContentRoleVO> PortcontentroleVoList)
        {
            List<PortContentRole> portcontentroleList = new List<PortContentRole>();

            foreach (var data in PortcontentroleVoList)
            {
                portcontentroleList.Add(data.MapToEntity());
            }
            return portcontentroleList;
        }

        public static PortContentRoleVO MapToDTO(this PortContentRole data)
        {
            PortContentRoleVO portcontentrolevo = new PortContentRoleVO();

            portcontentrolevo.PortContentID = data.PortContentID;
            portcontentrolevo.RoleID = data.RoleID;
            portcontentrolevo.UserType = data.UserType;
            portcontentrolevo.RecordStatus = data.RecordStatus;
            portcontentrolevo.CreatedBy = data.CreatedBy;
            portcontentrolevo.CreatedDate = data.CreatedDate;
            portcontentrolevo.ModifiedBy = data.ModifiedBy;
            portcontentrolevo.ModifiedDate = data.ModifiedDate;

            return portcontentrolevo;
        }

        public static PortContentRole MapToEntity(this PortContentRoleVO portcontentrolevo)
        {
            PortContentRole portcontentrole = new PortContentRole();
            portcontentrole.PortContentID = portcontentrolevo.PortContentID;
            portcontentrole.RoleID = portcontentrolevo.RoleID;
            portcontentrole.UserType = portcontentrolevo.UserType;
            portcontentrole.RecordStatus = portcontentrolevo.RecordStatus;
            portcontentrole.CreatedBy = portcontentrolevo.CreatedBy;
            portcontentrole.CreatedDate = portcontentrolevo.CreatedDate;
            portcontentrole.ModifiedBy = portcontentrolevo.ModifiedBy;
            portcontentrole.ModifiedDate = portcontentrolevo.ModifiedDate;

            return portcontentrole;
        }
    }
}
