using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IBudgetedValuesRepository
    {
        List<FinancialYearVO> FinanceYearDetails();

        FinancialYearVO InsertOrUpdateBudgetedValues(FinancialYearVO data, int UserId);

        List<FinancialYear> GetFinancialYears();

        List<Port> GetPortDetails();
    }
}
