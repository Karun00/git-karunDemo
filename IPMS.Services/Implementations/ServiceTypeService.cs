using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServiceTypeService : ServiceBase, IServiceTypeService
    {
        private IServiceTypeRepository _serviceTypeRepository;

        public ServiceTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _serviceTypeRepository = new ServiceTypeRepository(_unitOfWork);
        }

        public ServiceTypeService()
        {
            // TODO: Complete member initialization
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _serviceTypeRepository = new ServiceTypeRepository(_unitOfWork);
        }

        /// <summary>
        /// To Get ServiceType Details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> ServiceTypeDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _serviceTypeRepository.ServiceTypeDetails();
            });
        }

        /// <summary>
        /// To Add ServiceType Data
        /// </summary>
        /// <param name="serviceTypeData"></param>
        /// <returns></returns>
        public ServiceTypeVO AddServiceType(ServiceTypeVO serviceTypeData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _serviceTypeRepository.AddServiceType(serviceTypeData, _UserId);
            });
        }

        /// <summary>
        /// To Modify ServiceType Data
        /// </summary>
        /// <param name="serviceTypeData"></param>
        /// <returns></returns>
        public ServiceTypeVO ModifyServiceType(ServiceTypeVO serviceTypeData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _serviceTypeRepository.ModifyServiceType(serviceTypeData, _UserId);
            });
        }
    }
}
