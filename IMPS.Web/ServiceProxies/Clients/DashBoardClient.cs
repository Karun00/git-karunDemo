using System.Collections.Generic;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.Models;
using System;

namespace IPMS.ServiceProxies.Clients
{
    public class DashBoardClient : UserClientBase<IDashBoardService>, IDashBoardService
    {
        public List<DashBoardVO> DashBoardDetails(string fromDate, string toDate)
        {
            return WrapOperationWithException(() => Channel.DashBoardDetails(fromDate, toDate));
        }

        public string GetReportPeriod()
        {
            return WrapOperationWithException(() => Channel.GetReportPeriod());
        }

        public List<WegoDashBoardVO> GetWegoDashBoradDetails(DateTime fromDate, DateTime toDate)
        {
            return WrapOperationWithException(() => Channel.GetWegoDashBoradDetails(fromDate, toDate));
        }
        public List<TotalMovementsDashBoardVO> TotalMovementsDashboardDetails(DateTime fromDate, DateTime toDate)
        {
            return WrapOperationWithException(() => Channel.TotalMovementsDashboardDetails(fromDate, toDate));
        }

        public List<GetAllPorts> GetAllPorts()
        {
            return WrapOperationWithException(() => Channel.GetAllPorts());
        }
        public List<WegoBerthUtilizationVO> GetWegoBerthUtilizationData(string fromDate, string toDate)
        {
            return WrapOperationWithException(() => Channel.GetWegoBerthUtilizationData(fromDate, toDate));
        }

        public List<CargoTypeDashboardVO> CargoTypeDashboard(string fromDate, string toDate, string portcode)
        {
            return WrapOperationWithException(() => Channel.CargoTypeDashboard(fromDate, toDate, portcode));
        }

        public string GetUserPrivilegesWithControllerName(string controllerName, string username)
        {
            throw new NotImplementedException();
        }

        public List<PlannedMovementsDtlsVO> GetPlannedMovementsCount(string portcode)
        {
            return WrapOperationWithException(() => Channel.GetPlannedMovementsCount(portcode));
        }
        public AnchorageDtlsVO GetAnchorageCount(string portcode)
        {
            return WrapOperationWithException(() => Channel.GetAnchorageCount(portcode));
        }
        public List<PortWiseCountVO> GetPortWiseCount(string portcode)
        {
            return WrapOperationWithException(() => Channel.GetPortWiseCount(portcode));
        }
    }
}
