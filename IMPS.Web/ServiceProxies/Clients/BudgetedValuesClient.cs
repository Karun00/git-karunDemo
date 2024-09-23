using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class BudgetedValuesClient : UserClientBase<IBudgetedValuesService>, IBudgetedValuesService
    {
        public List<FinancialYearVO> FinanceYearDetails()
        {
            return WrapOperationWithException(() => Channel.FinanceYearDetails());
        }

        public FinancialYearVO InsertOrUpdateBudgetedValues(FinancialYearVO data)
        {
            return WrapOperationWithException(() => Channel.InsertOrUpdateBudgetedValues(data));
        }

        //public Task<List<FinancialYearVO>> FinanaceYearDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.FinanaceYearDetailsAsync());
        //}

        public List<FinancialYear> GetFinancialYears()
        {
            return WrapOperationWithException(() => Channel.GetFinancialYears());
        }

        //public Task<List<FinancialYear>> GetFinancialYearsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetFinancialYearsAsync());
        //}

        public List<Port> GetPortDetails()
        {
            return WrapOperationWithException(() => Channel.GetPortDetails());
        }

        //public Task<List<Port>> GetPortDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPortDetailsAsync());
        //}
    }
}