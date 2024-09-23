using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IRolePrivilegeRepository
    {
        IEnumerable<RoleVO> RolePrivilegeDetails();

        ReferenceDataVO GetRoleReferenceData();

        List<EntityListPrivlegesVO> GetEntitiesWithPrivileges(int moduleId, int subModuleId, int roleId);

        IEnumerable<ModuleVO> GetSubModulesWithModuleId(int moduleId);

        List<RolePrivlegeListVO> GetRolesPrivilegeEdit(int moduleId, int subModuleId, int roleId);
    }
}
