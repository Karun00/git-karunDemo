using System;
using System.Collections.Generic;
using System.Configuration;
using System.Messaging;
using System.ServiceModel;
using System.Web.Script.Serialization;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using log4net;
using log4net.Config;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class NotificationPublisherService : ServiceBase, INotificationPublisherService
    {
        private readonly INotificationRepository _notificationRepository;
        private ILog _notificationlog = LogManager.GetLogger(typeof(NotificationPublisherService));

        public NotificationPublisherService(IUnitOfWork unitOfWork)
        {
            XmlConfigurator.Configure();
            _unitOfWork = unitOfWork;
            _notificationRepository = new NotificationRepository(_unitOfWork);

        }

        public NotificationPublisherService()
        {
            XmlConfigurator.Configure();
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _notificationRepository = new NotificationRepository(_unitOfWork);
        }

        //NOTE: This method is implemented for API call To convert DB Scheduler to Queue, Still need to refine
        public bool PushMessageToQueue(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode)
        {
            bool _status = false;
            return ExecuteFaultHandledOperation(() =>
            {
                _status = _notificationRepository.PushMessageToQueue(entityId, reference, userid, company, portcode, workFlowTaskCode, null);
                return _status;
            });


        }

    }
}
