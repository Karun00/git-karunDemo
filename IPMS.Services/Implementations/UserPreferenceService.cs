using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;

using IPMS.Repository;
using System.Data.SqlClient;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserPreferenceService : ServiceBase, IUserPreferenceService
    {
        //    private readonly IUnitOfWork _unitOfWork;
        private IUserPreferenceRepository _userPreferenceRepository;
        private IAccountRepository _accountRepository;

        public UserPreferenceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userPreferenceRepository = new UserPreferenceRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);

        }
        public UserPreferenceService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _userPreferenceRepository = new UserPreferenceRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);

        }

        public List<UserPreferenceVO> GetUserPreferenceDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int _UserId = _accountRepository.GetUserId(_LoginName);
                return _userPreferenceRepository.GetUserPreferenceDetails(_UserId);
            });
        }

        public List<UserPreferenceVO> GetUserPreferenceDetailsByUser()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int _UserId = _accountRepository.GetUserId(_LoginName);
                return _userPreferenceRepository.GetUserPreferenceDetailsByUser(_UserId);
            });
        }

        public UserPreferenceVO AddUserPreference(UserPreferenceVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {

               // string name = _LoginName;

                int _UserId = _accountRepository.GetUserId(_LoginName);

                var cnt = _userPreferenceRepository.GetUserPreferences(_UserId);

                UserPreference value = null;
                value = data.MapToEntity();



                value.CreatedBy = _UserId;
                value.CreatedDate = DateTime.Now;
                value.ModifiedBy = _UserId;
                value.ModifiedDate = DateTime.Now;
                value.RecordStatus = "A";
                if (cnt != null)
                {
                    value.ObjectState = ObjectState.Modified;
                    cnt.DashBoardConfig = value.DashBoardConfig;
                    _unitOfWork.Repository<UserPreference>().Update(cnt);
                }
                else
                {
                    value.UserID = _UserId;
                    value.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<UserPreference>().Insert(value);
                }
                _unitOfWork.SaveChanges();

                data = value.MapToDTO();

                return data;
            });
        }


    }
}
