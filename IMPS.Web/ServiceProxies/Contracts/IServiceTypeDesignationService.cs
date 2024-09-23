using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IServiceTypeDesignationService : IDisposable
    {
        /// <summary>
        /// To Get ServiceTypeDesignation Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ServiceTypeVO> ServiceTypeDesignationDetails();

        /// <summary>
        /// To Add ServiceTypeDesignation Data
        /// </summary>
        /// <param name="serviceTypeDesignationData"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceTypeVO AddServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData);

        /// <summary>
        /// To Modify ServiceTypeDesignation Data
        /// </summary>
        /// <param name="serviceTypeDesignationData"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceTypeVO ModifyServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData);

        [OperationContract]
        List<SubCategory> GetDesignations();

        //[OperationContract]
        //Task<List<SubCategory>> GetDesignationsAsync();

        [OperationContract]
        List<SubCategory> GetCraftTypes();

        //[OperationContract]
        //Task<List<SubCategory>> GetCraftTypesAsync();

        [OperationContract]
        List<ServiceType> GetServiceTypes();

        //[OperationContract]
        //List<ServiceType> GetServiceTypesAsync();
    }
}
