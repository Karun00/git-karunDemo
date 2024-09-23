
using System;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
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
        [FaultContract(typeof(Exception))]
        RoleVO AddRolePrivilege(RoleVO rolePrivilegeData);

        /// <summary>
        /// To Modify Role Privilege Data
        /// </summary>
        /// <param name="rolePrivilegeData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        RoleVO ModifyRolePrivilege(RoleVO rolePrivilegeData);

        /// <summary>
        ///  To Get Role Privilege Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<RoleVO> RolePrivilegeDetails();

        /// <summary>
        /// To Get Module Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ReferenceDataVO GetRoleReferenceData();

        /// <summary>
        ///  To Get Entities with Privileges
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="SubModuleId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EntityListPrivlegesVO> GetEntitiesWithPrivileges(int moduleId, int subModuleId,int roleId);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RolePrivlegeListVO> GetRolesPrivilegeEdit(int moduleId, int subModuleId, int roleId);

        /// <summary>
        ///  To Get SubModules based on Moduleid
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<ModuleVO> GetSubModulesWithModuleId(int moduleId);




    }
}
