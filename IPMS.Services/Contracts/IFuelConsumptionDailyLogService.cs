using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IFuelConsumptionDailyLogService
    {

        [OperationContract]
        List<CraftVO> GetCraftDetails(string searchValue);

        [OperationContract]
        List<FuelConsumptionDailyLogVO> GetFuelConsumptionDailyLoggridDetails(int craftId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<FuelConsumptionDailyLogVO> GetAllFuelConsumptionDailyLogDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        FuelConsumptionDailyLogVO AddFuelConsumptionDailyLog(FuelConsumptionDailyLogVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        FuelConsumptionDailyLogVO ModifyFuelConsumptionDailyLog(FuelConsumptionDailyLogVO data);
    }
}
