using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IRolePrivilegeService
    {
        /// <summary>
        /// To Add Role Privilege Data
        /// </summary>
        /// <param name="rolePrivilegeData"></param>
        /// <returns></returns>
        [OperationContract]
        RoleVO AddRolePrivilege(RoleVO rolePrivilegeData);

        /// <summary>
        /// To Modify Role Privilege Data
        /// </summary>
        /// <param name="rolePrivilegeData"></param>
        /// <returns></returns>
        [OperationContract]
        RoleVO ModifyRolePrivilege(RoleVO rolePrivilegeData);

        [OperationContract]
        RolePrivilegeVO DeleteRolePrivilege(long id);

        /// <summary>
        ///  To Get Role Privilege Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<RoleVO> RolePrivilegeDetails();

        [OperationContract]
        RolePrivilegeVO GetRolePrivilegeId(long id);
              
        [OperationContract]
        IEnumerable<ModuleVO> GetModuleDetails();
    
        [OperationContract]
        IEnumerable<ModuleVO> GetSubModuleDetails(int modId);

        /// <summary>
        /// To Get Module Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReferenceDataVO GetRoleReferenceData();

        /// <summary>
        ///  To Get SubModules based on Moduleid
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<ModuleVO> GetSubModulesWithModuleId(int moduleId);

        /// <summary>
        ///  To Get Entities with Privileges
        /// </summary>
        /// <param name="Moduleid"></param>
        /// <param name="SubModuleid"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        [OperationContract]
        List<EntityListPrivlegesVO> GetEntitiesWithPrivileges(int moduleId, int subModuleId, int roleId);

        [OperationContract]
        List<RolePrivlegeListVO> GetRolesPrivilegeEdit(int moduleId, int subModuleId, int roleId);
    }
}
