using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Repository;
using System.Web.Mvc;
using IPMS.Services.WorkFlow;
using IPMS.Domain.DTOS;
using IPMS.Domain;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ExternalDivingRegisterService : ServiceBase, IExternalDivingRegisterService
    {
        IExternalDivingRegisterRepository _externalDivingRegisterRepository = null;
        private INotificationPublisher _notificationpublisher;
        private IUserRepository _userRepository;
        private IBerthRepository _berthRepository;
        private IEntityRepository _entity;

        public ExternalDivingRegisterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _externalDivingRegisterRepository = new ExternalDivingRegisterRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
        }

        public ExternalDivingRegisterService()
        {
            // TODO: Complete member initialization
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _externalDivingRegisterRepository = new ExternalDivingRegisterRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
        }

        /// <summary>
        /// adds / inserts the external diving register details
        /// </summary>
        /// <param name="externalDivingRegisterData"></param>
        /// <returns></returns>
        public ExternalDivingRegisterVO AddExternalDivingRegister(ExternalDivingRegisterVO externalDivingRegisterData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ExternalDivingRegister ExternalDivingRegister = new ExternalDivingRegister();
                ExternalDivingRegister = ExternalDivingRegisterMapExtension.MapToEntity(externalDivingRegisterData);
                ExternalDivingRegister.CreatedBy = _UserId;
                ExternalDivingRegister.CreatedDate = System.DateTime.Now;

                ExternalDivingRegister.ModifiedBy = _UserId;
                ExternalDivingRegister.ModifiedDate = System.DateTime.Now;
                ExternalDivingRegister.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<ExternalDivingRegister>().Insert(ExternalDivingRegister);
                _unitOfWork.SaveChanges();

                if (externalDivingRegisterData.StopTime != null)
                {
                    var _user = _userRepository.GetUserById(_UserId);
                    CompanyVO nextStepCompany = new CompanyVO();
                    nextStepCompany.UserType = _user.UserType;
                    nextStepCompany.UserTypeId = _user.UserTypeID;
                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.ExternalDivingRegister).EntityID, ExternalDivingRegister.ExternalDivingRegisterID.ToString(System.Globalization.CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Confirmed);
                }
                return externalDivingRegisterData;                
            });
        }

        /// <summary>
        /// Modifies / update the External diving register details
        /// </summary>
        /// <param name="externalDivingRegisterData"></param>
        /// <returns></returns>
        public ExternalDivingRegisterVO ModifyExternalDivingRegister(ExternalDivingRegisterVO externalDivingRegisterData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                if (externalDivingRegisterData.StopTime != null)
                {
                    var _user = _userRepository.GetUserById(_UserId);
                    CompanyVO nextStepCompany = new CompanyVO();
                    nextStepCompany.UserType = _user.UserType;
                    nextStepCompany.UserTypeId = _user.UserTypeID;
                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.ExternalDivingRegister).EntityID, externalDivingRegisterData.ExternalDivingRegisterID.ToString(System.Globalization.CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Confirmed);
                }
                return _externalDivingRegisterRepository.ModifyExternalDivingRegister(externalDivingRegisterData, _UserId);
            });
        }

        /// <summary>
        /// Deletes external diving register data using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExternalDivingRegisterVO DeleteExternalDivingRegister(long id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _externalDivingRegisterRepository.DeleteExternalDivingRegister(id);
            });
        }

        /// <summary>
        /// Gets External diving register list
        /// </summary>
        /// <returns></returns>
        public List<ExternalDivingRegisterVO> AllExternalDivingRegisterDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _externalDivingRegisterRepository.AllExternalDivingRegisterDetails(_PortCode);
            });
        }

        /// <summary>
        /// Gets all Companies
        /// </summary>
        /// <returns></returns>
        public List<LicenseRequestVO> GetAllCompanies()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _externalDivingRegisterRepository.GetAllCompanies(_PortCode);
            });
        }

        /// <summary>
        /// Gets all Vessels
        /// </summary>
        /// <returns></returns>
        public List<VesselVO> GetAllVessels()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _externalDivingRegisterRepository.GetAllVessels(_PortCode);
            });
        }

        /// <summary>
        /// Gets all Reference Data
        /// </summary>
        /// <returns></returns>
        public ExternalDivingRegisterVO GetDivingReferenceData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ExternalDivingRegisterVO _externalReferenceData = new ExternalDivingRegisterVO();
                _externalReferenceData.Berths = _berthRepository.GetBerths(_PortCode);

                return _externalReferenceData;
            });
        }
    }
}