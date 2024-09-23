using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IResourceGroupService : IDisposable
    {
        /// <summary>
        /// To get deisgnation details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategory> GetDesignations();

        /// <summary>
        /// To get employee details by designation code
        /// </summary>
        /// <param name="designationCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<Employee> GetEmployees(string resourceGroupCode, string designationCode, string mode);

        /// <summary>
        /// To add resource group details
        /// </summary>
        /// <param name="resourcegrp"></param>
        /// <returns></returns>
        [OperationContract]
        ResourceGroupVO AddResourceGroupDetails(ResourceGroupVO resourcegrp);

        /// <summary>
        /// To modify resource group details
        /// </summary>
        /// <param name="rsgroup"></param>
        /// <returns></returns>
        [OperationContract]
        ResourceGroupVO ModifyResourceGroups(ResourceGroupVO rsgroup);

        /// <summary>
        /// To get resource group details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ResourceGroupVO> GetResourceGroupDetails();
    }
}
