using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Repository;
using IPMS.Domain;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VoyageMonitoringService : ServiceBase, IVoyageMonitoringService
    {
        private IVoyageMonitoringRepository _voyageMonitoringRepository;
        private IAccountRepository _accountRepository;

        public VoyageMonitoringService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _accountRepository = new AccountRepository(_unitOfWork);
            _voyageMonitoringRepository = new VoyageMonitoringRepository(_unitOfWork);
        }

        public VoyageMonitoringService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _accountRepository = new AccountRepository(_unitOfWork);
            _voyageMonitoringRepository = new VoyageMonitoringRepository(_unitOfWork);
        }

        #region GetVesselDetails
        public Vessel GetVesselDetails(int VesselID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _voyageMonitoringRepository.GetVesselDetails(VesselID);
            });
        }
        #endregion

        #region GetVCNDetailsVoyage
        public List<VoyageMonitoringVO> GetVcnDetailsVoyage(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //TODO : ApICall with  Paramters to be Implemented 
                int loginuserId = _accountRepository.GetUserId(_LoginName);
                var data = _voyageMonitoringRepository.GetUserDetails(loginuserId, _PortCode);
                int agentId = 0;
                if (!string.IsNullOrEmpty(data.UserType) && data.UserType == UserType.Agent)
                    agentId = data.UserTypeID;
                return _voyageMonitoringRepository.GetVcnDetailsVoyage(_PortCode, searchValue, agentId);
            });
        }
        #endregion

        #region GetVCNDetailsVoyage_VCN
        public List<VoyageMonitoringVO> GetVcnDetailsVoyage_vcn(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _voyageMonitoringRepository.GetVcnDetailsVoyageVcn(vcn, _PortCode);
            });

        }
        #endregion

        #region GetServiceRequestDetails
        /// <summary>
        /// To get Service Request Grid details in Voyage Monitoring
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        public List<VoyageMonitoringVO> GetServiceRequestDetails(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _voyageMonitoringRepository.GetServiceRequestDetails(VCN);
            });
        }
        #endregion

        #region GetChangeATAandATDDetails
        public List<VoyageMonitoringVO> GetChangeAtaAndAtdDetails(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _voyageMonitoringRepository.GetChangeAtaAndAtdDetails(VCN);
            });
        }
        #endregion

        //By mahesh : 
        #region GetPortandBreakinDetails
        public List<VoyageMonitoringVO> GetPortAndBreakLimitDetails(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _voyageMonitoringRepository.GetPortAndBreakLimitDetails(VCN);
            });
        }
        #endregion
        

        #region GetAnchorageDetails
        public List<VoyageMonitoringVO> GetAnchorageDetails(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _voyageMonitoringRepository.GetAnchorageDetails(VCN);
            });
        }
        #endregion

        #region Get Berth Deatails
        public List<VoyageMonitoringVO> GetBerthDetails(string VCN)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                return _voyageMonitoringRepository.GetBerthDetails(VCN, _PortCode);
            });
        }
        #endregion
    }
}
