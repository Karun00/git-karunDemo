using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BudgetedValuesService : ServiceBase, IBudgetedValuesService
    {
        private IBudgetedValuesRepository _BudgetedValuesRepository;

        public BudgetedValuesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _BudgetedValuesRepository = new BudgetedValuesRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public BudgetedValuesService()
        {
            // TODO: Complete member initialization
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _BudgetedValuesRepository = new BudgetedValuesRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public List<FinancialYearVO> FinanceYearDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _BudgetedValuesRepository.FinanceYearDetails().ToList();
            });
        }

        public FinancialYearVO InsertOrUpdateBudgetedValues(FinancialYearVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _BudgetedValuesRepository.InsertOrUpdateBudgetedValues(data, _UserId);
            });
        }

        public List<FinancialYear> GetFinancialYears()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _BudgetedValuesRepository.GetFinancialYears().ToList();
            });
        }

        public List<Port> GetPortDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _BudgetedValuesRepository.GetPortDetails().ToList();
            });
        }
    }
}
