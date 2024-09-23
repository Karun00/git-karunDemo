using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IResourceGroupRepository
    {
        /// <summary>
        /// To get desigantion details
        /// </summary>
        /// <returns></returns>
        List<SubCategory> GetDesignations();

        /// <summary>
        /// To get resource group details
        /// </summary>
        /// <returns></returns>
        List<ResourceGroupVO> GetResourceGroups(string portCode);

        /// <summary>
        /// To get employee by desigantion code
        /// </summary>
        /// <param name="designationCode"></param>
        /// <returns></returns>
        List<Employee> GetEmployees(string resourceGroupCode, string designationCode, string mode, string portCode);

        ResourceGroupVO AddResourceGroupDetails(ResourceGroupVO resourcegrp, int userId, string portCode);

        ResourceGroupVO ModifyResourceGroups(ResourceGroupVO rsgroup, int userId, string portCode);
    }
}
