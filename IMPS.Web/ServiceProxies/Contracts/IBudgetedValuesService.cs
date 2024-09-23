using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IBudgetedValuesService : IDisposable
    {
        [OperationContract]
        List<FinancialYearVO> FinanceYearDetails();

        [OperationContract]
        FinancialYearVO InsertOrUpdateBudgetedValues(FinancialYearVO data);
        
        //[OperationContract]
        //Task<List<FinancialYearVO>> FinanaceYearDetailsAsync();

        [OperationContract]
        List<FinancialYear> GetFinancialYears();

        //[OperationContract]
        //Task<List<FinancialYear>> GetFinancialYearsAsync();

        [OperationContract]
        List<Port> GetPortDetails();

        //[OperationContract]
        //Task<List<Port>> GetPortDetailsAsync();
    }
}
