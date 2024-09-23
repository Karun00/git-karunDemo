using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserRegistration : ServiceBase, IUserRegistration
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserRegistration(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserRegistration()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }

        public User AddUser(User userData)
        {
            return ExecuteFaultHandledOperation(() =>
                {
                    userData.ModifiedDate = DateTime.Now;
                    userData.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<User>().Insert(userData);
                    _unitOfWork.SaveChanges();

                    return userData;
                });
        }

    }
}
