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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RevenueStopListService : ServiceBase, IRevenueStopListService
    {
        private ISubCategoryRepository _subcategoryRepository;
        private IAgentRepository _agentRepository;
      //  private IPortRepository _portRepository;
        private IRevenueStopListRepository _revenueaccountstopRepository;
      //  private IAccountRepository _accountRepository;
        public RevenueStopListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
            _agentRepository = new AgentRepository(_unitOfWork);
            //_portRepository = new PortRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
         //   _accountRepository = new AccountRepository(_unitOfWork);
            _revenueaccountstopRepository = new RevenueStopListRepository(_unitOfWork);
        }

        public RevenueStopListService()
        {
            //Get logger

            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
            _agentRepository = new AgentRepository(_unitOfWork);
           // _portRepository = new PortRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
          //  _accountRepository = new AccountRepository(_unitOfWork);
            _revenueaccountstopRepository = new RevenueStopListRepository(_unitOfWork);
        }

        public RevenueStopReferenceDataVO GetRevennueStopReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {

                RevenueStopReferenceDataVO VO = new RevenueStopReferenceDataVO();
                VO.AgentDetails = _agentRepository.GetAllAgentswithAccountno(_PortCode);
                VO.RevenueAccountStatus = _subcategoryRepository.GetRevenueAccountStatus();
                return VO;
            });
        }



        public List<RevenueStopListVO> GetAllAgentsforgrid()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _revenueaccountstopRepository.GetAllAgentsforgrid(_PortCode);
            });
        }

        public List<RevenueStopListVO> GetAgentdetails(string ag)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _revenueaccountstopRepository.GetAllAgents(_PortCode, ag);
            });
        }

        public List<RevenueStopListVO> SearchRevennueStop(string AgentID, string agentname, string horbaraccountno, string accountstatus)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _revenueaccountstopRepository.GetSearchAgentData(AgentID, agentname, horbaraccountno, accountstatus, _PortCode);
            });
        }

        public RevenueStopListVO AddRevenueStop(RevenueStopListVO revenueStop)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                revenueStop.CreatedBy = _UserId;
                revenueStop.CreatedDate = DateTime.Now;
                revenueStop.ModifiedBy = _UserId;
                revenueStop.ModifiedDate = DateTime.Now;
                //List<string> statuslist = revenueStop.selectedrevenueaccountstatus;
                RevenueAccountStatus accountstatus = new RevenueAccountStatus();
                RevenueStopList revenuestoplist = new RevenueStopList();
                AgentAccount agentaccount = new AgentAccount();
                revenuestoplist = revenueStop.MapToEntity();
                revenuestoplist.RevenueAccountStatus = null;
                revenuestoplist.PortCode = _PortCode;
                revenuestoplist.AgentID = revenueStop.AgentID;
                revenuestoplist.AgentAccountID = revenueStop.AgentAccountID;
                revenuestoplist.RecordStatus = "A";
                revenuestoplist.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<RevenueStopList>().Insert(revenuestoplist);
                _unitOfWork.SaveChanges();
                accountstatus.RevenueStopListID = revenuestoplist.RevenueStopListID;
                accountstatus.AccountStatusCode = revenueStop.AccountStatus;
                accountstatus.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<RevenueAccountStatus>().Insert(accountstatus);
                _unitOfWork.SaveChanges();
                revenueStop = revenuestoplist.MapToDTO();
                return revenueStop;
            });
        }

        public RevenueStopListVO ModifyRevenueStop(RevenueStopListVO revenueStop)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                revenueStop.CreatedBy = _UserId;
                revenueStop.CreatedDate = DateTime.Now;
                revenueStop.ModifiedBy = _UserId;
                revenueStop.ModifiedDate = DateTime.Now;
                RevenueAccountStatus accountstatus = new RevenueAccountStatus();
                Agent agent = new Agent();
                agent = _unitOfWork.Repository<Agent>().Query().Select().Where(e => e.RegistrationNumber == revenueStop.RegistrationNumber).First<Agent>();
                RevenueStopList revenuestoplist = new RevenueStopList();
                revenuestoplist = revenueStop.MapToEntity();
                revenuestoplist.AgentID = agent.AgentID;
                revenuestoplist.AgentAccountID = revenueStop.AgentAccountID;
                revenuestoplist.RevenueAccountStatus = null;
                revenuestoplist.RecordStatus = revenueStop.RecordStatus;
                revenuestoplist.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<RevenueStopList>().Update(revenuestoplist);
                _unitOfWork.SaveChanges();
                accountstatus.RevenueStopListID = revenuestoplist.RevenueStopListID;
                accountstatus.AccountStatusCode = revenueStop.AccountStatus;
                accountstatus.RevenueAccountStatusID = revenueStop.RevenueAccountStatusID;
                accountstatus.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<RevenueAccountStatus>().Update(accountstatus);
                _unitOfWork.SaveChanges();

                revenueStop = revenuestoplist.MapToDTO();
                return revenueStop;
            });
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
