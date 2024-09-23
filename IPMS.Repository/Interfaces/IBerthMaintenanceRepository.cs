using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IBerthMaintenanceRepository
    {
        BerthMaintenanceVO GetBerthMaintenanceRequestDetailsByID(string BerthMaintenanceID);
        List<BerthMaintenanceVO> GetBerthMaintenanceDetails(string portCode);
        List<BollardVO> GetBerthBollards(string portCode, string quayCode, string berthCode);
        List<BerthMaintenanceVO> GetBerthMaintenance(int berthMaintenanceId);
        BerthMaintenance GetBerthMaintenanceApproveId(string berthMaintenanceId);
        string GetWorkFlowRemarks(int workFlowInstanceId);
    }
}
