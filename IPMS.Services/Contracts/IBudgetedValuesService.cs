using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IBudgetedValuesService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<FinancialYearVO> FinanceYearDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        FinancialYearVO InsertOrUpdateBudgetedValues(FinancialYearVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<FinancialYear> GetFinancialYears();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Port> GetPortDetails();
    }
}
