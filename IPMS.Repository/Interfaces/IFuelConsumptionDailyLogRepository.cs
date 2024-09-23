using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IFuelConsumptionDailyLogRepository
    {
        List<FuelConsumptionDailyLog> GetAllFuelConsumptionDailyLogDetails(string portCode);
        List<CraftVO> GetCraftDetails(string searchValue);
        List<FuelConsumptionDailyLog> GetFuelConsumptionDailyLoggridDetails(int craftId);
    }
}
