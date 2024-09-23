using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                 ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BerthPlanningService : ServiceBase, IBerthPlanningService
    {

        private IBerthPlanningRepository _berthplanningRepository;
        public BerthPlanningService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
        }
        public BerthPlanningService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _berthplanningRepository = new BerthPlanningRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
        }

        /// <summary>
        /// To Get Quays List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<QuayVO> GetQuaysInPort()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<QuayVO> quays = _berthplanningRepository.GetQuaysInPort(_PortCode);
                return quays;
            });
        }

        /// <summary>
        /// To Get BerthsBollards List
        /// </summary>
        /// <param name="request"></param>
        ///  /// <param name="quayCode"></param>
        /// <returns></returns>
        public List<BerthsData> GetBerthsWithBollard(string quayCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<BerthsData> berthswithbollards = _berthplanningRepository.GetBerthsWithBollard(_PortCode, quayCode);
                return berthswithbollards;
            });
        }

        /// <summary>
        /// To Get QuayBerthsBollards List
        /// </summary>
        /// <param name="request"></param>
        ///  /// <param name="quaycode"></param>
        /// <returns></returns>
        public List<QuayBerthBollardData> GetQuayBerthsBollard()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<QuayBerthBollardData> quayberthswithbollards = _berthplanningRepository.GetQuayBerthsBollard(_PortCode);
                return quayberthswithbollards;
            });
        }





        /// <summary>
        /// To Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        public List<BerthPlanningVO> GetVesselInformation(string fromDate, string toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<BerthPlanningVO> VesselCallDetails = _berthplanningRepository.GetVesselInformation(_PortCode, _UserId, _UserType, fromDate, toDate);
                return VesselCallDetails;
            });
        }


        public Boolean CheckBerthAvailability(string vcn, string vcmId, string quayCode, string fromBerthCode, string fromBollardMeter, string toBerthCode, string toBollardMeter, string fromTime, string toTime)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Boolean VesselCallDetails = _berthplanningRepository.CheckBerthAvailability(_PortCode, vcn, vcmId, quayCode, fromBerthCode, fromBollardMeter, toBerthCode, toBollardMeter, fromTime, toTime);
                return VesselCallDetails;
            });

        }
        public List<BerthMaintenanceData> GetBerthMaintenance(string fromDate, string toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<BerthMaintenanceData> BerthMaintenanceList = _berthplanningRepository.GetBerthMaintenance(_PortCode, _UserId, fromDate, toDate);
                return BerthMaintenanceList;
            });
        }


        public UserData GetUserDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                UserData UserDetails = _berthplanningRepository.GetUserDetails(_PortCode, _UserId);
                return UserDetails;
            });
        }

        public List<BerthPlanningConfiguration> GetBerthPlanningConfigurations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<BerthPlanningConfiguration> BPDetails = _berthplanningRepository.GetBerthPlanningConfigurations(_PortCode);
                return BPDetails;
            });
        }



        /// <summary>
        /// To Update VesselCallMovementInformation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="plannedvessel"></param>
        /// <returns></returns>
        public List<BerthPlanningVO> SaveVesselCallMovements(List<BerthPlanningVO> plannedVessels)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                List<BerthPlanningVO> vesselsWithSuitableBerths = _berthplanningRepository.SaveVesselCallMovements(plannedVessels, _UserId);
                return vesselsWithSuitableBerths;
            });
        }

        public GISMapPathVo GetGisMapPath(string portcodegis)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthplanningRepository.GetGisMapPath(portcodegis);
            });
        }

        /// <summary>
        /// To Get VesselCallMovements List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fromDate"></param>
        ///<param name="toDate"></param>
        /// <returns></returns>
        public List<BerthPlanningVO> GetVesselInformationGIS( string portcode,string fromDate, string toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<BerthPlanningVO> VesselCallDetails = _berthplanningRepository.GetVesselInformationGIS(portcode, _UserId, _UserType, fromDate, toDate);
                return VesselCallDetails;
            });
        }
        //Anchored code
        public List<AnchorVesselInfoGISVO> GetAnchorVesselInformationGIS(string portcode1)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<AnchorVesselInfoGISVO> VesselCallDetails = _berthplanningRepository.GetAnchorVesselInformationGIS(portcode1);
                return VesselCallDetails;
            });
        }

        public List<BerthedVessels> GetBerthedVesselDetails(string portcode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<BerthedVessels> BerthedVessel = _berthplanningRepository.GetBerthedVesselDetails(portcode);
                return BerthedVessel;
            });
        }
        public List<SailedVessels> GetSailedVesselDetails(string portcode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<SailedVessels> SailedVessel = _berthplanningRepository.GetSailedVesselDetails(portcode);
                return SailedVessel;
            });
        }
        public List<AnchoredVessels> GetAnchoredVesselDetails(string portcode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<AnchoredVessels> AnchoredVessel = _berthplanningRepository.GetAnchoredVesselDetails(portcode);
                return AnchoredVessel;
            });
        }
    }
}
