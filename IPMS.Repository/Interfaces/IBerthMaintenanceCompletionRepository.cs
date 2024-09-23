using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IBerthMaintenanceCompletionRepository
    {
        BerthMaintenanceDataVO GetBerthMaintenanceCompletionDetailsByID(string BerthMaintenanceCompletionID);
        List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletionList(string portCode);
        List<DataVO> GetBethMaintenanceIDs(string portCode);
        IEnumerable<DataVO> BethMaintenanceDetails(int id);
        List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletion(int berthMaintenanceCompletionId);
        BerthMaintenanceCompletion GetApproveid(string berthMaintenanceCompletionId);
    }
}
