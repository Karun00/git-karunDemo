using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class CraftReminderConfigService : ServiceBase, ICraftReminderConfigService
    {
        private ICraftReminderConfigRepository _craftreminderconfigRepository;
        private IAccountRepository _accountRepository;

        public CraftReminderConfigService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _craftreminderconfigRepository = new CraftReminderConfigRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public CraftReminderConfigService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _craftreminderconfigRepository = new CraftReminderConfigRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public List<CraftReminderConfigVO> GetCraftReminderConfigDetails(int craftId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftreminderconfigRepository.GetCraftReminderConfigDetails(craftId).MapToDTO();
            });
        }

        public List<CraftVO> GetCraftReminderConfigById(int craftReminderConfigId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftreminderconfigRepository.GetCraftReminderConfigById(craftReminderConfigId, _PortCode);
            });
        }

        

        public CraftReferenceVO GetCraftReminderReferences()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftreminderconfigRepository.GetCraftReminderReferences();
            });
        }

        public CraftVO AddCraftReminderConfig(CraftReminderConfigVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _craftreminderconfigRepository.AddCraftReminderConfig(data, _PortCode, _UserId);
            });
        }

        public CraftReminderConfigVO ModifyCraftReminderConfig(CraftReminderConfigVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _craftreminderconfigRepository.ModifyCraftReminderConfig(data, _UserId);
            });
        }

        public void AcknowledgeCraftReminderConfig(string craftReminderConfigID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int craftconfigid = Convert.ToInt32(craftReminderConfigID, CultureInfo.InvariantCulture);
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _craftreminderconfigRepository.GetCraftReminderConfigByConfigId(craftconfigid);

                CraftReminderConfigWorkFlow craftWorkFlow = new CraftReminderConfigWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<CraftReminderConfigWorkFlow> wf = new WorkFlowEngine<CraftReminderConfigWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(craftWorkFlow, taskcode);
            });
        }
    }
}
