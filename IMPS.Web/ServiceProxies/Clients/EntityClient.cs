using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class EntityClient : UserClientBase<IEntityService> , IEntityService
    {
        public List<EntityModulesVO> EntityDetails()
        {
            return WrapOperationWithException(() => Channel.EntityDetails());
        }

        //public Task<List<EntityModulesVO>> EntityDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.EntityDetailsAsync());
        //}

        public List<EntityModulesVO> GetAllSubModules()
        {
           return WrapOperationWithException(() => Channel.GetAllSubModules());                   
        }

        //public Task<List<EntityModulesVO>> GetAllSubModulesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetAllSubModulesAsync());
        //}

        public List<SubCategoryVO> GetPrivilegeTypes()
        {
            return WrapOperationWithException(() => Channel.GetPrivilegeTypes());
        }

        //public Task<List<SubCategoryVO>> GetPrivilegeTypesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPrivilegeTypesAsync());
        //}

        public EntityModulesVO ModifyEntity(EntityModulesVO entityData)
        {
            return WrapOperationWithException(() => Channel.ModifyEntity(entityData));
        }

        //public Task<EntityModulesVO> ModifyEntityAsync(EntityModulesVO entitydata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyEntityAsync(entitydata));
        //}

        public EntityModulesVO AddEntity(EntityModulesVO entityData)
        {
            return WrapOperationWithException(() => Channel.AddEntity(entityData));
        }

        //public Task<EntityModulesVO> AddEntityAsync(EntityModulesVO entitydata)
        //{
        //    return WrapOperationWithException(() => Channel.AddEntityAsync(entitydata));
        //}     

    }
}