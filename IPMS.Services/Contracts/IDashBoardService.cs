using System;
using Core.Repository.Providers.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPMS.Domain.Models;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IDashBoardService
    {

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DashBoardVO> DashBoardDetails(string fromDate, string toDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PlannedMovementsDtlsVO> GetPlannedMovementsCount(string portcode);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        AnchorageDtlsVO GetAnchorageCount(string portcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<PortWiseCountVO> GetPortWiseCount(string portcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetReportPeriod();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<WegoDashBoardVO> GetWegoDashBoradDetails(DateTime fromDate, DateTime toDate);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<TotalMovementsDashBoardVO> TotalMovementsDashboardDetails(DateTime fromDate, DateTime toDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<GetAllPorts> GetAllPorts();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<WegoBerthUtilizationVO> GetWegoBerthUtilizationData(string fromDate, string toDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<CargoTypeDashboardVO> CargoTypeDashboard(string fromDate, string toDate, string portcode);

    }
}

