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
using System.Web.Mvc;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class TerminalOperatorService : ServiceBase, ITerminalOperatorService
    {
      //  private IAccountRepository _accountRepository;
        private ITerminalOperatorRepository _terminalOperatorRepository;

        public TerminalOperatorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          //  _accountRepository = new AccountRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public TerminalOperatorService()
        {
            // TODO: Complete member initialization
            _unitOfWork = new UnitOfWork(new TnpaContext());
           // _accountRepository = new AccountRepository(_unitOfWork);
            _terminalOperatorRepository = new TerminalOperatorRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        #region GetTerminalOperatorList
        /// <summary>
        /// To get Terminal Operator List
        /// </summary>
        /// <returns></returns>
        public List<TerminalOperatorVO> GetTerminalOperatorList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _terminalOperatorRepository.GetTerminalOperatorList(_PortCode);
            });
        }
        #endregion

        #region AddterminalOperator
        /// <summary>
        /// To Add Terminal Operator Data
        /// </summary>
        /// <param name="terminaloperatordata"></param>
        /// <returns></returns>
        public TerminalOperatorVO AddTerminalOperator(TerminalOperatorVO entity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _terminalOperatorRepository.AddTerminalOperator(entity, _UserId);
            });
        }
        #endregion

        #region ModifyterminalOperator
        /// <summary>
        /// To Modify Terminal Operator Data
        /// </summary>
        /// <param name="terminaloperatordata"></param>
        /// <returns></returns>
        public TerminalOperatorVO ModifyTerminalOperator(TerminalOperatorVO entity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _terminalOperatorRepository.ModifyTerminalOperator(entity, _UserId);
            });
        }
        #endregion

        #region GetCargoTypes
        /// <summary>
        /// To get CargoTypes
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetCargoTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _terminalOperatorRepository.GetCargoTypes();
            });
        }
        #endregion
    }
}
