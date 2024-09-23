 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;

using IPMS.Repository;
using System.Data.SqlClient;

namespace IPMS.Services
{ 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, 
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DashBoardService : ServiceBase, IDashBoardService
    {    
        private IDashBoardRepository _dashboardRepository;
        private IAccountRepository _accountRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigRepository;
        private IBerthRepository _berthRepository;
        public DashBoardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dashboardRepository=new DashBoardRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _portGeneralConfigRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
        }
        public DashBoardService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
             _dashboardRepository=new DashBoardRepository(_unitOfWork);
             _accountRepository = new AccountRepository(_unitOfWork);
             _portGeneralConfigRepository = new PortGeneralConfigsRepository(_unitOfWork);
             _berthRepository = new BerthRepository(_unitOfWork);
        }

        public List<DashBoardVO> DashBoardDetails(string fromDate, string toDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int _UserId = _accountRepository.GetUserId(_LoginName);
                return _dashboardRepository.DashBoardDetails(_UserId, fromDate, toDate, _PortCode);
            });
        }

       public string GetReportPeriod()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int _UserId = _accountRepository.GetUserId(_LoginName);
                var ports = _accountRepository.GetPortsByUser(_UserId).FirstOrDefault();
                var res = "";                
                 if (string.IsNullOrEmpty(_PortCode))
                {
                    res = ports.PortCode;
                }
                else {
                    res = _PortCode;
                }


                return _portGeneralConfigRepository.GetReportPeriod(res);

            });
        }

        /// <summary>
       /// To get Wego Commodity Vessel Details
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
       public List<WegoDashBoardVO> GetWegoDashBoradDetails(DateTime fromDate, DateTime toDate)
       {
           return ExecuteFaultHandledOperation(() =>
           {               
               return _dashboardRepository.GetWegoDashBoradDetails(fromDate, toDate, _PortCode);
           });
       }

       public List<TotalMovementsDashBoardVO> TotalMovementsDashboardDetails(DateTime fromDate, DateTime toDate)
       {
           return ExecuteFaultHandledOperation(() =>
           {
               return _dashboardRepository.TotalMovementsDashboardDetails(fromDate, toDate);
           });
       }

       public List<GetAllPorts> GetAllPorts()
       {
           return ExecuteFaultHandledOperation(() =>
           {
               return _dashboardRepository.GetAllPorts();
           });
       }
        /// <summary>
        /// To get Wego Berth Utilization Details
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
       public List<WegoBerthUtilizationVO> GetWegoBerthUtilizationData(string fromDate, string toDate)
       {
           return ExecuteFaultHandledOperation(() =>
           {               
               return _dashboardRepository.GetWegoBerthUtilizationData(fromDate, toDate, _PortCode);
           });
       }

       public List<CargoTypeDashboardVO> CargoTypeDashboard(string fromDate, string toDate, string portcode)
       {
           return ExecuteFaultHandledOperation(() =>
           {
               return _dashboardRepository.CargoTypeDashboard(fromDate, toDate, portcode);
           });
       }
       public IEnumerable<PlannedMovementsDtlsVO> GetPlannedMovementsCount(string portcode)
       {
           return ExecuteFaultHandledOperation(() =>
           {
               int _UserId = _accountRepository.GetUserId(_LoginName);
               return _dashboardRepository.GetPlannedMovementsCount(portcode);
           });
       }
       public AnchorageDtlsVO GetAnchorageCount(string portcode)
       {
           return ExecuteFaultHandledOperation(() =>
           {
               int _UserId = _accountRepository.GetUserId(_LoginName);
               return _dashboardRepository.GetAnchorageCount(portcode);
           });
       }
       public List<PortWiseCountVO> GetPortWiseCount(string portcode)
       {
           return ExecuteFaultHandledOperation(() =>
           {
               int _UserId = _accountRepository.GetUserId(_LoginName);
               return _dashboardRepository.GetPortWiseCount(portcode);
           });
       }
    }
}
