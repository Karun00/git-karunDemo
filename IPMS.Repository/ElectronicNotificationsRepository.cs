using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System.Data.SqlClient;


namespace IPMS.Repository
{
    public class ElectronicNotificationsRepository : IElectronicNotificationsRepository
    {
        private IUnitOfWork _unitOfWork;
       // private readonly ILog log;

        public ElectronicNotificationsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
           // log = LogManager.GetLogger(typeof(ElectronicNotificationsRepository));
        }
        public List<NotificationDetails> GetNotificationTemplates(string portCode)
        {
            var notificationtemplatedata = new List<NotificationDetails>();
            //try
            //{
                var _portcode = new SqlParameter("@p_portcode", portCode);
                notificationtemplatedata = _unitOfWork.SqlQuery<NotificationDetails>("dbo.[usp_GetNotificationTemplates] @p_portcode", _portcode).ToList();
            //}
            //catch (Exception ex)
            ////{
            //    log.Error("Exception " + ex.Message);
            //}
            return notificationtemplatedata;

        }
        public List<Role> GetNotificationRoles(string notificationTemplateCode, string portCode)
        {
            var notificationroledata = new List<Role>();
            //try
            //{
                var _portcode = new SqlParameter("@p_portcode", portCode);
                var _notificationtemplatecode = new SqlParameter("@p_NotificationTemplateCode", notificationTemplateCode);
                notificationroledata = _unitOfWork.SqlQuery<Role>("dbo.[usp_GetNotificationRoles] @p_NotificationTemplateCode, @p_portcode", _notificationtemplatecode, _portcode).ToList();
            //}
            //catch (Exception ex)
            //{
                //log.Error("Exception " + ex.Message);
            //}
            return notificationroledata;

        }

        public List<Port> GetNotificationPorts(string notificationTemplateCode)
        {
            var notificationportdata = new List<Port>();
            //try
            //{
                var _notificationtemplatecode = new SqlParameter("@p_NotificationTemplateCode", notificationTemplateCode);
                notificationportdata = _unitOfWork.SqlQuery<Port>("dbo.[usp_GetNotificationPorts] @p_NotificationTemplateCode", _notificationtemplatecode).ToList();
            //}
            //catch (Exception ex)
            //{
                //log.Error("Exception " + ex.Message);
            //}
            return notificationportdata;

        }

    }
}
