using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Clients
{
    public class ResourceGroupClient : UserClientBase<IResourceGroupService>, IResourceGroupService
    {
        /// <summary>
        /// To get designation details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            return WrapOperationWithException(() => Channel.GetDesignations());
        }

        /// <summary>
        ///To get employee details by designation code
        /// </summary>
        /// <param name="designationCode"></param>
        /// <returns></returns>
        public List<Employee> GetEmployees(string resourceGroupCode, string designationCode, string mode)
        {
            return WrapOperationWithException(() => Channel.GetEmployees(resourceGroupCode, designationCode, mode));
        }

        /// <summary>
        /// To add resource group details
        /// </summary>
        /// <param name="resourcegrp"></param>
        /// <returns></returns>
        public ResourceGroupVO AddResourceGroupDetails(ResourceGroupVO resourcegrp)
        {
            return WrapOperationWithException(() => Channel.AddResourceGroupDetails(resourcegrp));
        }

        /// <summary>
        /// To get resource group details
        /// </summary>
        /// <returns></returns>
        public List<ResourceGroupVO> GetResourceGroupDetails()
        {
            return WrapOperationWithException(() => Channel.GetResourceGroupDetails());
        }

        /// <summary>
        /// To modify resource group details
        /// </summary>
        /// <param name="rsgroup"></param>
        /// <returns></returns>
        public ResourceGroupVO ModifyResourceGroups(ResourceGroupVO rsgroup)
        {
            return WrapOperationWithException(() => Channel.ModifyResourceGroups(rsgroup));
        }
    }
}