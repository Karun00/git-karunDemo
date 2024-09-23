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
    public class ServiceTypeDesignationService : ServiceBase, IServiceTypeDesignationService
    {
        private IServiceTypeDesignationRepository _serviceTypeDesignationRepository;

        public ServiceTypeDesignationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _serviceTypeDesignationRepository = new ServiceTypeDesignationRepository(_unitOfWork);
        }

        public ServiceTypeDesignationService()
        {
            // TODO: Complete member initialization

            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _serviceTypeDesignationRepository = new ServiceTypeDesignationRepository(_unitOfWork);
        }

        /// <summary>
        /// To Get ServiceTypeDesignation Details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> ServiceTypeDesignationDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _serviceTypeDesignationRepository.ServiceTypeDesignationDetails(_PortCode).ToList();
            });
        }

        /// <summary>
        /// To Add ServiceTypeDesignation Data
        /// </summary>
        /// <param name="serviceTypeDesignationData"></param>
        /// <returns></returns>
        public ServiceTypeVO AddServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _serviceTypeDesignationRepository.AddServiceTypeDesignation(serviceTypeDesignationData, _UserId, _PortCode);
            });
        }

        /// <summary>
        /// To Modify ServiceTypeDesignation Data
        /// </summary>
        /// <param name="serviceTypeDesignationData"></param>
        /// <returns></returns>
        public ServiceTypeVO ModifyServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _serviceTypeDesignationRepository.ModifyServiceTypeDesignation(serviceTypeDesignationData, _UserId, _PortCode);
            });
        }

        /// <summary>
        /// This method is used for fetches the designation details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _serviceTypeDesignationRepository.GetDesignations().ToList();
            });
        }

        /// <summary>
        /// To Get Service Types
        /// </summary>
        /// <returns></returns>
        public List<ServiceType> GetServiceTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _serviceTypeDesignationRepository.GetServiceTypes().ToList();
            });
        }

        /// <summary>
        /// To Get Craft Types Details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetCraftTypes()
        {
            return _serviceTypeDesignationRepository.GetCraftTypes(_PortCode).ToList(); ;
        }
    }
}
