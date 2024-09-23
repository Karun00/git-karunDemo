using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IServiceTypeDesignationService
    {
        /// <summary>
        /// To Get ServiceTypeDesignation Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceTypeVO> ServiceTypeDesignationDetails();

        /// <summary>
        /// To Add ServiceTypeDesignation Data
        /// </summary>
        /// <param name="serviceTypeDesignationData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ServiceTypeVO AddServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData);

        /// <summary>
        /// To Modify ServiceTypeDesignation Data
        /// </summary>
        /// <param name="serviceTypeDesignationData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ServiceTypeVO ModifyServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetDesignations();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetCraftTypes();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceType> GetServiceTypes();
    }
}
