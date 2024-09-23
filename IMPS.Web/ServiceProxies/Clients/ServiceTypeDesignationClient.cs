using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class ServiceTypeDesignationClient : UserClientBase<IServiceTypeDesignationService>, IServiceTypeDesignationService
    {
        public List<ServiceTypeVO> ServiceTypeDesignationDetails()
        {
            return WrapOperationWithException(() => Channel.ServiceTypeDesignationDetails());
        }

        public ServiceTypeVO AddServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData)
        {
            return WrapOperationWithException(() => Channel.AddServiceTypeDesignation(serviceTypeDesignationData));
        }

        public ServiceTypeVO ModifyServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData)
        {
            return WrapOperationWithException(() => Channel.ModifyServiceTypeDesignation(serviceTypeDesignationData));
        }

        public List<SubCategory> GetDesignations()
        {
            return WrapOperationWithException(() => Channel.GetDesignations());
        }

        //public Task<List<SubCategory>> GetDesignationsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetDesignationsAsync());
        //}

        public List<ServiceType> GetServiceTypes()
        {
            return WrapOperationWithException(() => Channel.GetServiceTypes());
        }

        //public List<ServiceType> GetServiceTypesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetServiceTypesAsync());
        //}

        public List<SubCategory> GetCraftTypes()
        {
            return WrapOperationWithException(() => Channel.GetCraftTypes());
        }

        //public Task<List<SubCategory>> GetCraftTypesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetCraftTypesAsync());
        //}
    }
}