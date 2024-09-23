using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IEntityService
    {
        /// <summary>
        /// To Get Entity Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EntityModulesVO> EntityDetails();

        /// <summary>
        /// To Get Sub Modules
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EntityModulesVO> GetAllSubModules();    

        /// <summary>
        /// To Get Privilege Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetPrivilegeTypes();

        /// <summary>
        /// To Add Entity Data
        /// </summary>
        /// <param name="entityData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        EntityModulesVO AddEntity(EntityModulesVO entityData);

        /// <summary>
        /// To Modify Entity Data
        /// </summary>
        /// <param name="entityData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        EntityModulesVO ModifyEntity(EntityModulesVO entityData);


         }
}
