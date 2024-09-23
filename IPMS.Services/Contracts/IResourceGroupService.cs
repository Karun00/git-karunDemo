using System;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IResourceGroupService
    {
        /// <summary>
        ///To get resource group details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ResourceGroupVO> GetResourceGroupDetails();
        /// <summary>
        /// To get desigantion details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetDesignations();
        /// <summary>
        /// To get employee by desigantion code
        /// </summary>
        /// <param name="designationCode"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Employee> GetEmployees(string resourceGroupCode, string designationCode, string mode);
        /// <summary>
        /// To add resource group details
        /// </summary>
        /// <param name="resourcegrp"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceGroupVO AddResourceGroupDetails(ResourceGroupVO resourcegrp);
        /// <summary>
        /// To modify resource group details
        /// </summary>
        /// <param name="rsgroup"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ResourceGroupVO ModifyResourceGroups(ResourceGroupVO rsgroup);
    }
}
