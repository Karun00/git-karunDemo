using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ResourceAllocationConfigRuleService : ServiceBase, IResourceAllocationConfigRuleService
    {
        private ISubCategoryRepository _subcategoryRepository;
        private IResourceAllocationConfigRuleRepository _resourceallocationconfigrulerepository;

        public ResourceAllocationConfigRuleService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _resourceallocationconfigrulerepository = new ResourceAllocationConfigRuleRepository(_unitOfWork);
        }

        public ResourceAllocationConfigRuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _resourceallocationconfigrulerepository = new ResourceAllocationConfigRuleRepository(_unitOfWork);
        }

        /// <summary>
        /// Method to Get ResourceAllocationConfigRuleList
        /// </summary>
        /// <returns></returns>
        public List<ResourceAllocationConfigRuleVO> GetResourceAllocationConfigRuleList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _resourceallocationconfigrulerepository.GetresourceAllocationconfigurationruledetails(_PortCode).MapToDTO();
            });
        }

        /// <summary>
        /// Method to Add ResourceAllocationConfigRule
        /// </summary>
        /// <param name="RACRdata"></param>
        /// <returns></returns>
        public ResourceAllocationConfigRuleVO AddResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _resourceallocationconfigrulerepository.AddResourceAllocationConfigRule(RACRdata, _UserId, _PortCode);
            });
        }

        /// <summary>
        /// Method to update ResourceAllocationConfigRule
        /// </summary>
        /// <param name="RACRdata"></param>
        /// <returns></returns>
        public ResourceAllocationConfigRuleVO ModifyResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _resourceallocationconfigrulerepository.ModifyResourceAllocationConfigRule(RACRdata, _UserId, _PortCode);
            });
        }

        /// <summary>
        /// Method to Get ResourceAllocationConfigRuleReferencesVO
        /// </summary>
        /// <returns></returns>
        public RAConfigruleReferenceVO GetResourceAllocationConfigRuleReferencesVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                RAConfigruleReferenceVO VO = new RAConfigruleReferenceVO();
               // string portCode = _PortCode;
                VO.ServiceTypes = _resourceallocationconfigrulerepository.GetResourceAllocationConfigRulemovementtypedetails().MapToDto();
                VO.PilotCapacity = _subcategoryRepository.GetPilotCapacity();
                return VO;
            });
        }
    }
}
