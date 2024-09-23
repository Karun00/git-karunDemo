using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
         ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SuppDockUnDockTimeService : ServiceBase, ISuppDockUnDockTimeService
    {
        ISuppDockUnDockTimeRepository _SuppDockUnDockTimeRepository = null;
        private INotificationPublisher _notificationpublisher;
        private IPortConfigurationRepository _portConfigurationRepository;
        private const string _entityCode = EntityCodes.Supp_DryDockUndock;
        private IEntityRepository _entityRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SuppDockUnDockTimeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _SuppDockUnDockTimeRepository = new SuppDockUnDockTimeRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
        }
        /// <summary>
        /// 
        /// </summary>
        public SuppDockUnDockTimeService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _SuppDockUnDockTimeRepository = new SuppDockUnDockTimeRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
            // TODO: Complete member initialization
        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <returns></returns>
        public List<SuppDryDockVO> AllSuppDockUnDockTimeDetails()
        {

            return ExecuteFaultHandledOperation(() =>
            {
                return _SuppDockUnDockTimeRepository.AllSuppDockUnDockTimeDetails(_PortCode);
            });


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SuppDryDockVO ModifySuppDockUnDockTime(SuppDryDockVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var entityid = _entityRepository.GetEntitiesNotification(EntityCodes.Vessel_ETAChange).EntityID;

                CompanyVO nextStepCompany = GetUserDetails(_UserId);
                SuppDryDock SuppDockUnDockTime = data.MapToEntity();

                SuppDockUnDockTime.ModifiedBy = _UserId;
                SuppDockUnDockTime.ModifiedDate = System.DateTime.Now;
                SuppDockUnDockTime.RecordStatus = "A";

                if (data.EnteredDockDateTime != null)
                {
                    SuppDockUnDockTime.ScheduleStatus = "DOCK";
                }
                if (data.LeftDockDateTime != null)
                {
                    SuppDockUnDockTime.ScheduleStatus = "UNDK";
                }

                // SuppDockUnDockTime.SuppServiceRequestID = SuppDockUnDockTimedata.SuppDockUnDockTimenVO.SuppServiceRequestID;
                //  SuppDockUnDockTime.CreatedBy = _UserId;
                // SuppDockUnDockTime.CreatedDate = System.DateTime.Now;

                SuppDockUnDockTime.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<SuppDryDock>().Update(SuppDockUnDockTime);

                _unitOfWork.SaveChanges();

                _notificationpublisher.Publish(entityid, SuppDockUnDockTime.SuppDryDockID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                return data;
            });
        }

        public CompanyVO GetUserDetails(int UserID)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == UserID
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }

    }
}
