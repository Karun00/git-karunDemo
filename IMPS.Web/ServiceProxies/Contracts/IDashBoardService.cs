using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IDashBoardService : IDisposable
    {
        [OperationContract]
        List<DashBoardVO> DashBoardDetails(string fromDate, string toDate);
        [OperationContract]
        List<WegoDashBoardVO> GetWegoDashBoradDetails(DateTime fromDate, DateTime toDate);
        [OperationContract]
        List<TotalMovementsDashBoardVO> TotalMovementsDashboardDetails(DateTime fromDate, DateTime toDate);
        [OperationContract]
        List<GetAllPorts> GetAllPorts();
        [OperationContract]
        List<WegoBerthUtilizationVO> GetWegoBerthUtilizationData(string fromDate, string toDate);
        [OperationContract]
        List<CargoTypeDashboardVO> CargoTypeDashboard(string fromDate, string toDate, string portcode);
        [OperationContract]
        string GetUserPrivilegesWithControllerName(string controllerName, string username);
        [OperationContract]
        string GetReportPeriod();
        [OperationContract]
        List<PlannedMovementsDtlsVO> GetPlannedMovementsCount(string portcode);
        [OperationContract]
        AnchorageDtlsVO GetAnchorageCount(string portcode);
        [OperationContract]
        List<PortWiseCountVO> GetPortWiseCount(string portcode);
       
    }
}

