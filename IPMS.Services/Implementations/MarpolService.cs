using Core.Repository;
using IPMS.Core.Repository.Exceptions;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MarpolService : ServiceBase, IMarpolService
    {
        private ISubCategoryRepository _subcategoryRepository;        
        private IMarpolRepository _marpolRepository;

        public MarpolService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);            
            _marpolRepository = new MarpolRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public MarpolService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _marpolRepository = new MarpolRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);           
        }
        public List<MarpolVO> GetMarpolDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marpolRepository.GetMarpolDetails();
            });
        }

        /// <summary>
        /// To get reference data for Marpol Types
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MarpolVO GetMarpolReferenceData()
        {
            MarpolVO VO = new MarpolVO();
            VO.MarpolTypes = _subcategoryRepository.MarpolTypes();           

            return VO;
        }

        /// <summary>
        /// To Add  Marpol Details
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        public MarpolVO SaveMarpolDetails(MarpolVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                Marpol marpol = new Marpol();
                marpol.ClassCode = data.ClassCode;
                marpol.ClassName = data.ClassName;
                marpol.MarpolCode = data.MarpolCode;
                marpol.RecordStatus = data.RecordStatus;
                marpol.CreatedBy = _UserId;
                marpol.CreatedDate = DateTime.Now;
                marpol.ModifiedBy = _UserId;
                marpol.ModifiedDate = DateTime.Now;


                marpol.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<Marpol>().Insert(marpol);
                _unitOfWork.SaveChanges();   

                return data;
            });
        }

        /// <summary>
        /// To Modify Marpol Details
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        public MarpolVO ModifyMarpolDetails(MarpolVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {              

                Marpol marpol = new Marpol();
                marpol.ClassCode = data.ClassCode;
                marpol.ClassName = data.ClassName;
                marpol.MarpolCode = data.MarpolCode;
                marpol.RecordStatus = data.RecordStatus;
                marpol.CreatedBy = _UserId;
                marpol.CreatedDate = DateTime.Now;
                marpol.ModifiedBy = _UserId;
                marpol.ModifiedDate = DateTime.Now;


                marpol.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Marpol>().Update(marpol);
                _unitOfWork.SaveChanges();       

                return data;
            });
        }


    }
}