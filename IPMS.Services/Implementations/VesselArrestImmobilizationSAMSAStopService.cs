using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System;
using IPMS.Domain;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VesselArrestImmobilizationSAMSAStopService : ServiceBase, IVesselArrestImmobilizationSAMSAStopService
    {
        private INotificationPublisher _notificationpublisher;
        private IUserRepository _userRepository;
        private IEntityRepository _entityRepository;
        private IVesselArrestImmobilizationSAMSAStopRepository _VAIsRepository;

        #region VesselArrestImmobilizationSAMSAStopService Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public VesselArrestImmobilizationSAMSAStopService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _userRepository = new UserRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
            _VAIsRepository = new VesselArrestImmobilizationSAMSAStopRepository(_unitOfWork);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public VesselArrestImmobilizationSAMSAStopService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);

            // TODO: Complete member initialization
            _userRepository = new UserRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
            _VAIsRepository = new VesselArrestImmobilizationSAMSAStopRepository(_unitOfWork);
        }
        #endregion

        #region GetVesselArrestImmobilizationSAMSAStopbyID
        /// <summary>
        /// gets the VesselArrestImmobilizationSAMSAStop data
        /// </summary>
        /// <returns></returns>
        public VesselArrestImmobilizationSAMSAStopVO GetVesselArrestImmobilizationSamsaStopById(int vasId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _VAIsRepository.GetVesselArrestImmobilizationSamsaStopById(vasId, _PortCode);
            });
        }
        #endregion

        #region GetVesselArrestImmobilizationSAMSAStopList
        /// <summary>
        /// Method to Get VesselArrest ImmobilizationSAMSAStop List
        /// </summary>
        /// <returns></returns>
        public List<VesselArrestImmobilizationSAMSAStopVO> GetVesselArrestImmobilizationSamsaStopList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _VAIsRepository.GetVesselArrestImmobilizationSamsaStopList(_PortCode);
            });
        }
        #endregion

        #region GetUserID
        /// <summary>
        /// Method to Get UserID by _LoginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int GetUserId(string loginName)
        {
            var user = (from u in _unitOfWork.Repository<User>().Query().Select()
                        where u.UserName == loginName
                        select u).FirstOrDefault<User>();
            return user.UserID;
        }
        #endregion

        #region GetEntities
        /// <summary>
        /// To Get Entity Details Based on EntitiyCode For Notifications
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        public Entity GetEntities(string entityCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _entityRepository.GetEntitiesNotification(entityCode);
            });
        }
        #endregion

        #region GetVcnDetails
        /// <summary>
        /// Method to Get VcnDetails
        /// </summary>
        /// <returns></returns>
        public List<ArrivalNotificationVO> GetVcnDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _VAIsRepository.GetVcnDetails(_PortCode);
            });
        }
        #endregion

        #region ModifyVesselArrestImmobilizationSAMSAStop
        /// <summary>
        /// Modify the VesselArrestImmobilizationSAMSAStop Data
        /// </summary>
        /// <param name="vesselArrestImmobilizationSamsaStopData"></param>
        /// <returns></returns>
        public VesselArrestImmobilizationSAMSAStopVO ModifyVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSamsaStopData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string name = _LoginName;
                if (String.IsNullOrEmpty(name))
                {
                    name = "admin";
                }
                int UserID = GetUserId(name);

                _VAIsRepository.ModifyVesselArrestImmobilizationSamsaStop(vesselArrestImmobilizationSamsaStopData, UserID);

                #region Sending Email for SAMSA Stop / Arrest

                var _user = _userRepository.GetUserById(UserID);
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = _user.UserType;
                nextStepCompany.UserTypeId = _user.UserTypeID;

                // Send Notifications when Vessel Arrested
                if ((vesselArrestImmobilizationSamsaStopData.VesselArrestedStatus == true) && (vesselArrestImmobilizationSamsaStopData.VesselReleasedStatus == false))
                {
                    //var _ArresteddCode = WFStatus.VesselArrested;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        vesselArrestImmobilizationSamsaStopData.VAISID.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, WFStatus.VesselArrested);
                }

                // Send Notifications when Vessel Reelased
                if ((vesselArrestImmobilizationSamsaStopData.VesselReleasedStatus == true) && (vesselArrestImmobilizationSamsaStopData.VesselArrestedStatus == true))
                {
                    //var _ReleasedCode = WFStatus.VesselReleased;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        vesselArrestImmobilizationSamsaStopData.VAISID.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, WFStatus.VesselReleased);
                }

                // Send Notifications when Vessel SAMSAStopped
                if ((vesselArrestImmobilizationSamsaStopData.SAMSAStopStatus == true) && (vesselArrestImmobilizationSamsaStopData.SAMSAClearedStatus ==false))
                {
                    //var _SAMSAStopCode = WFStatus.VesselSAMSAStopped;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        vesselArrestImmobilizationSamsaStopData.VAISID.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, WFStatus.VesselSAMSAStopped);
                }

                // Send Notifications when Vessel SAMSA Cleared
                if ((vesselArrestImmobilizationSamsaStopData.SAMSAClearedStatus == true) && (vesselArrestImmobilizationSamsaStopData.SAMSAStopStatus == true))
                {
                    //var _SAMSAClearCode = WFStatus.VesselSAMSACleared;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        vesselArrestImmobilizationSamsaStopData.VAISID.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, WFStatus.VesselSAMSACleared);
                }

                // Send Notifications when Immobilized
                if (vesselArrestImmobilizationSamsaStopData.ImmobilizationStatus == true)
                {
                    //var _Immobilization = WFStatus.Immobilization;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        vesselArrestImmobilizationSamsaStopData.VAISID.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, WFStatus.Immobilization);
                }

                #endregion

                return vesselArrestImmobilizationSamsaStopData;
            });
        }
        #endregion

        #region AddVesselArrestImmobilizationSAMSAStop
        /// <summary>
        /// Add the VesselArrestImmobilizationSAMSAStop data
        /// </summary>
        /// <param name="vesselArrestImmobilizationSamsaStopData"></param>
        /// <returns></returns>
        public VesselArrestImmobilizationSAMSAStopVO AddVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO vesselArrestImmobilizationSamsaStopData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                string name = _LoginName;
                if (String.IsNullOrEmpty(name))
                {
                    name = "admin";
                }
                int UserID = GetUserId(name);

              int idAddVesselArrestImmobilizationSAMSAStop = _VAIsRepository.AddVesselArrestImmobilizationSamsaStop(vesselArrestImmobilizationSamsaStopData, UserID);
                

                #region Sending Email for SAMSA Stop / Arrest

                var _user = _userRepository.GetUserById(UserID);
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = _user.UserType;
                nextStepCompany.UserTypeId = _user.UserTypeID;

                // Send Notifications when Vessel Arrested
                if ((vesselArrestImmobilizationSamsaStopData.VesselArrestedStatus == true) && (vesselArrestImmobilizationSamsaStopData.VesselReleasedStatus == false))
                {
                    var _ArresteddCode = WFStatus.VesselArrested;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        idAddVesselArrestImmobilizationSAMSAStop.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, _ArresteddCode);
                }

                // Send Notifications when Vessel Released
                if ((vesselArrestImmobilizationSamsaStopData.VesselArrestedStatus == true) && (vesselArrestImmobilizationSamsaStopData.VesselReleasedStatus == true))
                {
                    var _ReleasedCode = WFStatus.VesselReleased;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        idAddVesselArrestImmobilizationSAMSAStop.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, _ReleasedCode);
                }

                // Send Notifications when Vessel SAMSAStopped
                if ((vesselArrestImmobilizationSamsaStopData.SAMSAStopStatus == true) && (vesselArrestImmobilizationSamsaStopData.SAMSAClearedStatus == false))
                {
                    var _SAMSAStopCode = WFStatus.VesselSAMSAStopped;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        idAddVesselArrestImmobilizationSAMSAStop.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, _SAMSAStopCode);
                }

                // Send Notifications when Vessel SAMSA Cleared
                if ((vesselArrestImmobilizationSamsaStopData.SAMSAStopStatus == true) && (vesselArrestImmobilizationSamsaStopData.SAMSAClearedStatus == true))
                {
                    var _SAMSAClearCode = WFStatus.VesselSAMSACleared;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                        idAddVesselArrestImmobilizationSAMSAStop.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, _SAMSAClearCode);
                }

                // Send Notifications when Immobilized
                if (vesselArrestImmobilizationSamsaStopData.ImmobilizationStatus == true)
                {
                    //var _Immobilization = WFStatus.Immobilization;

                    _notificationpublisher.Publish(_entityRepository.GetEntitiesNotification(EntityCodes.VesselArrests).EntityID,
                       idAddVesselArrestImmobilizationSAMSAStop.ToString(CultureInfo.InvariantCulture), UserID, nextStepCompany, _PortCode, WFStatus.Immobilization);
                }


                #endregion

                return vesselArrestImmobilizationSamsaStopData;
            });
        }
        #endregion
    }
}
