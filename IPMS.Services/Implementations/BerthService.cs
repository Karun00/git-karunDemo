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
    public class BerthService : ServiceBase, IBerthService
    {
        private ISubCategoryRepository _subcategoryRepository;
        private IBerthRepository _berthRepository;

        public BerthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public BerthService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        #region GetBerthsDetails
        /// <summary>
        /// To Get Berth Details
        /// </summary>
        /// <returns></returns>
        public List<BerthVO> GetBerthsDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthRepository.GetBerthsDetails(_PortCode);
            });
        }
        #endregion

        #region GetPortQuayDetails
        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="Portid"></param>
        /// <returns></returns>
        public List<QuayVO> GetPortQuayDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthRepository.GetPortQuayDetails(_PortCode);
            });
        }
        #endregion

        #region GetBerthsInQuay
        /// <summary>
        /// To Get Berths In Quay
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        public List<BerthVO> GetBerthsInQuay(string portCode, string quayCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthRepository.GetBerthsInQuay(portCode, quayCode);
            });
        }
        #endregion

        #region AddBerth
        /// <summary>
        /// Add Berth
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public BerthVO AddBerth(BerthVO berthData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _berthRepository.AddBerth(berthData, _UserId, _PortCode);
            });
        }
        #endregion

        #region ModifyBerth
        /// <summary>
        /// Update Berth 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public BerthVO ModifyBerth(BerthVO berthData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _berthRepository.ModifyBerth(berthData, _UserId, _PortCode);
            });
        }
        #endregion

        #region DelBerthByID
        /// <summary>
        /// To Delete Berth By Id
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        public BerthVO DelBerthById(BerthVO berthData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthRepository.DelBerthById(berthData, _UserId, _PortCode);
            });
        }
        #endregion

        #region GetBerthType
        /// <summary>
        /// To Get Berth Types
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetBerthType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthRepository.GetBerthType();
            });
        }
        #endregion

        #region GetCargoType
        /// <summary>
        /// To Get Cargo Types
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetCargoType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<SubCategory> cargotypes = new List<SubCategory>();
                cargotypes = _subcategoryRepository.CargoTypes();
                return cargotypes;
            });
        }
        #endregion

        #region GetVesselType
        /// <summary>
        /// To Get Vessel Types
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryCodeNameVO> GetVesselType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<SubCategoryCodeNameVO> vesseltypes = new List<SubCategoryCodeNameVO>();
                vesseltypes = _subcategoryRepository.GetVesselTypes();
                return vesseltypes;
            });
        }
        #endregion

        #region GetReasonType
        /// <summary>
        /// Get Reason For Visit Types
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryCodeNameVO> GetReasonType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<SubCategoryCodeNameVO> reasontypes = new List<SubCategoryCodeNameVO>();
                reasontypes = _subcategoryRepository.GetReasonsForVisit();
                return reasontypes;
            });
        }
        #endregion

        #region GetBerthsWithBollards
        /// <summary>
        /// //////////// By Mahesh ///////////////////////////////
        /// </summary>
        /// <returns></returns> // returns berthlist with correspond bollards.
        public List<BerthVO> GetBerthsWithBollards()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthRepository.GetBerthsWithBollards(_PortCode);
            });
        }
        #endregion

        #region GetBerthsWithPortCode
        public List<BerthVO> GetBerthsWithPortCode()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthRepository.GetBerths(_PortCode);
            });
        }
        #endregion
    }
}
