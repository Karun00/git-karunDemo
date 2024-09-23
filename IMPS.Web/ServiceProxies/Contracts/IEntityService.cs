using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IEntityService
    {
        /// <summary>
        /// To Get Entity Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<EntityModulesVO> EntityDetails();

        /// <summary>
        /// To Get Entity Details Asynchronously
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<EntityModulesVO>> EntityDetailsAsync();

        /// <summary>
        /// To Get Sub Modules
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<EntityModulesVO> GetAllSubModules();

        /// <summary>
        /// To Get Sub Modules Asynchronously
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<EntityModulesVO>> GetAllSubModulesAsync();

        /// <summary>
        /// To Get Privilege Types   
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> GetPrivilegeTypes();

        /// <summary>
        /// To Get Privilege Types Asynchronously       
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<SubCategoryVO>> GetPrivilegeTypesAsync();

        /// <summary>
        ///  To Add Entity Data
        /// </summary>
        /// <param name="entityData"></param>
        /// <returns></returns>
        [OperationContract]
        EntityModulesVO AddEntity(EntityModulesVO entityData);

        /// <summary>
        ///  To Add Entity Data Asynchronously
        /// </summary>
        /// <param name="entityData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<EntityModulesVO> AddEntityAsync(EntityModulesVO entitydata);

        /// <summary>
        /// To Modify Entity Data
        /// 
        /// </summary>
        /// <param name="entityData"></param>
        /// <returns></returns>
        [OperationContract]
        EntityModulesVO ModifyEntity(EntityModulesVO entityData);

        /// <summary>        
        /// To Modify Entity Data Asynchronously      
        /// </summary>
        /// <param name="entityData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<EntityModulesVO> ModifyEntityAsync(EntityModulesVO entitydata);
       
       
    }
}
