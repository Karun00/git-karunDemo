using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class RolePrivilegeClient : UserClientBase<IRolePrivilegeService>,IRolePrivilegeService
    {

        public RoleVO AddRolePrivilege(RoleVO rolePrivilegeData)
        {
            return Channel.AddRolePrivilege(rolePrivilegeData);
        }

        public RoleVO ModifyRolePrivilege(RoleVO rolePrivilegeData)
        {
            return Channel.ModifyRolePrivilege(rolePrivilegeData);
        }

        public RolePrivilegeVO DeleteRolePrivilege(long id)
        {
            return Channel.DeleteRolePrivilege(id);
        }

        public RolePrivilegeVO GetRolePrivilegeId(long id)
        {
            return Channel.GetRolePrivilegeId(id);
        }

        public IEnumerable<RoleVO> RolePrivilegeDetails()
        {
            return Channel.RolePrivilegeDetails();
        }

        public IEnumerable<ModuleVO> GetModuleDetails()
        {
            return Channel.GetModuleDetails();
        }

        public IEnumerable<ModuleVO> GetSubModuleDetails(int modId)
        {
            return Channel.GetSubModuleDetails(modId);
        }

        public ReferenceDataVO GetRoleReferenceData()
        {
            return Channel.GetRoleReferenceData();
        }

        public IEnumerable<ModuleVO> GetSubModulesWithModuleId(int moduleId)
        {
            return Channel.GetSubModulesWithModuleId(moduleId);
        }

        public List<EntityListPrivlegesVO> GetEntitiesWithPrivileges(int moduleId, int subModuleId, int roleId)
        {
            return Channel.GetEntitiesWithPrivileges(moduleId, subModuleId, roleId);
        }

        public List<RolePrivlegeListVO> GetRolesPrivilegeEdit(int moduleId, int subModuleId, int roleId)
        {
            return Channel.GetRolesPrivilegeEdit(moduleId, subModuleId, roleId);
        }
    }
}
