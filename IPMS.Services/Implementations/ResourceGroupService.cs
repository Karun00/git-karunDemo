using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Security.Cryptography;
using System.IO;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System.Data.Entity.SqlServer;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using System.Data.SqlClient;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ResourceGroupService : ServiceBase, IResourceGroupService
    {
        private IResourceGroupRepository _ResourceGroupRepository;
     //   private IAccountRepository _accountRepository;

        public ResourceGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
           // _accountRepository = new AccountRepository(_unitOfWork);
            _ResourceGroupRepository = new ResourceGroupRepository(_unitOfWork);
        }

        public ResourceGroupService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
          //  _accountRepository = new AccountRepository(_unitOfWork);
            _ResourceGroupRepository = new ResourceGroupRepository(_unitOfWork);
        }

        /// <summary>
        /// To get desigantion details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ResourceGroupRepository.GetDesignations();
            });
        }

        /// <summary>
        /// To get resource group details
        /// </summary>
        /// <returns></returns>
        public List<ResourceGroupVO> GetResourceGroupDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ResourceGroupRepository.GetResourceGroups(_PortCode);
            });
        }

        /// <summary>
        ///To get employee details by desigantion code 
        /// </summary>
        /// <param name="designationCode"></param>
        /// <returns></returns>
        public List<Employee> GetEmployees(string resourceGroupCode, string designationCode, string mode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ResourceGroupRepository.GetEmployees(resourceGroupCode, designationCode, mode, _PortCode);
            });
        }

        /// <summary>
        ///To add resource group details 
        /// </summary>
        /// <param name="resourcegrp"></param>
        /// <returns></returns>
        public ResourceGroupVO AddResourceGroupDetails(ResourceGroupVO resourcegrp)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ResourceGroupRepository.AddResourceGroupDetails(resourcegrp, _UserId, _PortCode);
            });
        }

        /// <summary>
        ///  To modify resource group details
        /// </summary>
        /// <param name="rsgroup"></param>
        /// <returns></returns>
        public ResourceGroupVO ModifyResourceGroups(ResourceGroupVO rsgroup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ResourceGroupRepository.ModifyResourceGroups(rsgroup, _UserId, _PortCode);
            });
        }
    }
}
