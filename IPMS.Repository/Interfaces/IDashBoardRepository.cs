using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IDashBoardRepository
    {
        List<DashBoardVO> DashBoardDetails(int UserID, string fromDate, string toDate, string PortCode);
        List<PlannedMovementsDtlsVO> GetPlannedMovementsCount(string portcode);
        AnchorageDtlsVO GetAnchorageCount(string portcode);
        List<PortWiseCountVO> GetPortWiseCount(string portcode);
        List<WegoDashBoardVO> GetWegoDashBoradDetails(DateTime fromDate, DateTime toDate, string PortCode);
        List<TotalMovementsDashBoardVO> TotalMovementsDashboardDetails(DateTime fromDate, DateTime toDate);
        List<GetAllPorts> GetAllPorts();
        List<WegoBerthUtilizationVO> GetWegoBerthUtilizationData(string fromDate, string toDate, string PortCode);
        List<CargoTypeDashboardVO> CargoTypeDashboard(string fromDate, string toDate, string portcode);
    }
}

