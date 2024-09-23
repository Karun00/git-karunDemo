using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using Core.Repository;
using IPMS.Data.Context;
using log4net;
using System;
using log4net.Config;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    public class DredgingVolumeNotifier:Notifier
       
    {
        //private DredgingVolume _dredgingvolume;
        //protected IDredgingVolumeRepository _dredgingvolumeRepository;

        public DredgingVolumeNotifier(Notification pendingNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
            : base(pendingNotification, notificationTemplate, entityDetails, roles)
        {         
            //try
            //{
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(DredgingVolumeNotifier));
                //_dredgingvolumeRepository = new DredgingVolumeRepository(_NotifierunitOfWork);
                //_dredgingvolume = _dredgingvolumeRepository.GetDredgingVolumeRequestDetailsByID(pendingNotification.Reference);
        //}
        //    catch (Exception ex)
        //    {
        //        log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
        //    }
        }
        public override string GetPortCode()
        {
            return pendingNotification.PortCode;
        }
        public override List<User> GetUsersToBeNotified()
        {
            return _userRepository.GetUsersForRoleAndPortCode(GetPortCode(), this.roles);
        }
        public override Dictionary<string, string> GetTokensDictionary()
        {
            //return Common.GetTokensDictionary(entityDetails, _dredgingvolume);
            return null;
        }
    }
}
