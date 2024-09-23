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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                   ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class QuayService : ServiceBase, IQuayService
    {
        private IQuayRepository _quayRepository;

        public QuayService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _quayRepository = new QuayRepository(_unitOfWork);
        }

        public QuayService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _quayRepository = new QuayRepository(_unitOfWork);
           
            // TODO: Complete member initialization
        }

        
       /// <summary>
       /// Post Quay Data
       /// </summary>
       /// <param name="quayData"></param>
       /// <returns></returns>
        public QuayVO AddQuay(QuayVO quayData)
        {
            return EncloseTransactionAndHandleException(() =>
            {                         
                Quay quay = new Quay();
                quay = QuayMapExtension.MapToEntity(quayData);
                quay.CreatedBy = _UserId;
                quay.CreatedDate = DateTime.Now;
                quay.ModifiedBy = _UserId;
                quay.ModifiedDate = DateTime.Now;               
                quay.ObjectState = ObjectState.Added;
                quay.PortCode = _PortCode;
                _unitOfWork.Repository<Quay>().Insert(quay);
                _unitOfWork.SaveChanges();
                return quayData;
            });
        }

        /// <summary>
        /// Modify Quay Data
        /// </summary>
        /// <param name="quayData"></param>
        /// <returns></returns>
        public QuayVO ModifyQuay(QuayVO quayData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                Quay quay = new Quay();
                quay = QuayMapExtension.MapToEntity(quayData);
                quay.ModifiedBy = _UserId;
                quay.ModifiedDate = DateTime.Now;
                quay.ObjectState = ObjectState.Modified;
                quay.PortCode = _PortCode;
                _unitOfWork.Repository<Quay>().Update(quay);
                _unitOfWork.SaveChanges();
                return quayData;
            });
        }

        /// <summary>
        /// To Get Quay Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuayVO GetQuayId(string id)
        {
            // throw new NotImplementedException();
            return ExecuteFaultHandledOperation(() =>
            {
                return _quayRepository.GetQuayId(id);
               
            });
        }

        /// <summary>
        /// To Delete Quay by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuayVO DeleteQuay(long id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _quayRepository.DeleteQuay(id);
            });

        }

        /// <summary>
        /// To Get Quay Details
        /// </summary>
        /// <returns></returns>
        public List<QuayVO> QuayDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _quayRepository.QuayDetails(_PortCode);
            });
        }

        /// <summary>
        /// To Get Berths based on Quay
        /// </summary>
        /// <returns></returns>
        public List<QuayVO> GetQuaysWithBerths(string portCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _quayRepository.GetQuaysWithBerths(portCode);
            });
           
        }

        /// <summary>
        /// To Get Quays with Berths
        /// </summary>
        /// <returns></returns>
        public List<QuayVO> GetQuaysWithBerthsMobile()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _quayRepository.GetQuaysWithBerthsMobile(_PortCode);
            });
        }
    }
}

