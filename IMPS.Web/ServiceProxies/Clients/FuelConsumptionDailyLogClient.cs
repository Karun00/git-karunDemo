using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class FuelConsumptionDailyLogClient : UserClientBase<IFuelConsumptionDailyLogService>,IFuelConsumptionDailyLogService
    {

        public List<CraftVO> GetCraftDetails(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetCraftDetails(searchValue));
        }

        public List<FuelConsumptionDailyLogVO> GetAllFuelConsumptionDailyLogDetails()
        {
            return WrapOperationWithException(() => Channel.GetAllFuelConsumptionDailyLogDetails());
        }

        public FuelConsumptionDailyLogVO AddFuelConsumptionDailyLog(FuelConsumptionDailyLogVO data)
        {
            return WrapOperationWithException(() => Channel.AddFuelConsumptionDailyLog(data));
        }

        public FuelConsumptionDailyLogVO ModifyFuelConsumptionDailyLog(FuelConsumptionDailyLogVO data)
        {
            return WrapOperationWithException(() => Channel.ModifyFuelConsumptionDailyLog(data));
        }

        public List<FuelConsumptionDailyLogVO> GetFuelConsumptionDailyLoggridDetails(int craftId)
        {
            return WrapOperationWithException(() => Channel.GetFuelConsumptionDailyLoggridDetails(craftId));
        }
    }
}