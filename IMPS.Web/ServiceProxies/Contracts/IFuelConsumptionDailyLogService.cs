using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IFuelConsumptionDailyLogService : IDisposable
    {
        [OperationContract]
        List<CraftVO> GetCraftDetails(string searchValue);
        [OperationContract]
        List<FuelConsumptionDailyLogVO> GetAllFuelConsumptionDailyLogDetails();
        [OperationContract]
        FuelConsumptionDailyLogVO AddFuelConsumptionDailyLog(FuelConsumptionDailyLogVO data);
        [OperationContract]
        FuelConsumptionDailyLogVO ModifyFuelConsumptionDailyLog(FuelConsumptionDailyLogVO data);
        [OperationContract]
        List<FuelConsumptionDailyLogVO> GetFuelConsumptionDailyLoggridDetails(int craftId);

    }
}
