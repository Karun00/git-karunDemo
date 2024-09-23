using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Messaging;
using System.Web.Script.Serialization;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System.Globalization;
using IPMS.Domain;

namespace IPMS.Notifications.Engine
{
    public class NotificationEngine : IDisposable
    {
        private ILog log;
        private bool isInProcess;
        public NotificationEngine()
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(NotificationEngine));
                isInProcess = false;
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            }
        }
        public bool IsInProcess()
        {
            return isInProcess;
        }
        public void ProcessMessageById(string messageId)
        {
            try
            {
                isInProcess = true;
                MessageQueueTransaction msmqTransaction = new MessageQueueTransaction();
                MessageQueue msMq = new MessageQueue(ConfigurationManager.AppSettings["MSMQPath"].ToString());
                msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                try
                {
                    string msgId = messageId.Replace("{", "").Replace("}", "");

                    log.Info("Before PeekById : " + msgId);
                    var message = msMq.PeekById(msgId);
                    log.Info("Ater PeekById : " + msgId);
                    if (message != null)
                    {
                        log.Info("Message Body : " + message.Body.ToString());
                        msmqTransaction.Begin();
                        NotificationVO objNotification = new JavaScriptSerializer().Deserialize<NotificationVO>(message.Body.ToString());
                        if (objNotification != null)
                        {
                            Notification notifications = new Notification
                            {
                                NotificationId = Convert.ToInt32(message.Id.Split('\\')[1]),
                                NotificationTemplateCode = objNotification.NotificationTemplateCode,
                                Reference = objNotification.Reference,
                                RecordStatus = objNotification.RecordStatus,
                                EmailStatus = objNotification.EmailStatus,
                                SMSStatus = objNotification.SMSStatus,
                                SystemNotificationStatus = objNotification.SystemNotificationStatus,
                                PortCode = objNotification.PortCode,
                                CreatedBy = objNotification.CreatedBy,
                                ModifiedBy = objNotification.ModifiedBy,
                                DateTime = objNotification.DateTime,
                                CreatedDate = objNotification.CreatedDate,
                                ModifiedDate = objNotification.ModifiedDate,
                                UserType = objNotification.UserType,
                                UserTypeId = objNotification.UserTypeId

                            };
                            Stopwatch wfstopwatch = Stopwatch.StartNew();
                            List<Notification> pendingNotifications = new List<Notification>();
                            pendingNotifications.Add(notifications);
                            bool notificationStatus = ProcessNotifications(pendingNotifications);

                            wfstopwatch.Stop();
                            if (!notificationStatus)
                            {
                                msmqTransaction.Abort();
                                log.Error("ProcessNotifications method return error..check the log");
                            }
                            else
                            {
                                msmqTransaction.Commit();
                                msMq.ReceiveById(messageId);
                                log.Info("Time taken to complete Notification " +
                                         objNotification.NotificationId +
                                         "Notification Template Code : " +
                                         objNotification.NotificationTemplateCode +
                                         " : " + wfstopwatch.ElapsedMilliseconds.ToString());
                            }
                        }
                        else
                        {
                            log.Info("Message Queue doesn't contain any message!!");

                        }
                    }

                }
                catch (MessageQueueException e)
                {                   

                    if (e.MessageQueueErrorCode ==
                        MessageQueueErrorCode.IOTimeout)
                    {
                        log.Error("No message arrived in queue.");

                    }
                    else if (e.MessageQueueErrorCode == MessageQueueErrorCode.TransactionUsage)
                    {
                        log.Error("Queue is not transactional.");
                    }
                    else
                    {
                        log.Error("Message Queue error : " + e.Message);
                    }
                }
                finally
                {
                    msmqTransaction.Dispose();
                    msMq.Close();
                }
                log.Info("MSMQ Implementation Completed..");
            }
            catch (Exception ex)
            {
                log_error("NotificationEngine Start() method Error : ", ex);
            }
            finally
            {
                isInProcess = false;
            }
        }

        public void ProcessAllMessages()
        {
            try
            {
                string excludedTemplatecodes = ConfigurationManager.AppSettings["ExcludedTemplates"].ToString();
                isInProcess = true;
                #region MSMQ Implementation

                MessageQueueTransaction msmqTransaction = new MessageQueueTransaction();
                MessageQueue msMq =
                    new MessageQueue();
                msMq.Path = ConfigurationManager.AppSettings["MSMQPath"].ToString();
                msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                try
                {
                    var timeout = new TimeSpan(0, 0, 0, 1);
                    var queueIter = msMq.GetMessageEnumerator2();
                    while (queueIter.MoveNext(timeout))
                    {

                        if (queueIter.Current != null)
                        {
                            msmqTransaction.Begin();

                            log.Info("Message Id is : " + queueIter.Current.Id.Split('\\')[1].ToString());

                            if (queueIter.Current != null)
                            {
                                if (excludedTemplatecodes.Contains(queueIter.Current.Label))
                                {
                                    log.Warn(string.Format("Message {0} is skipped..Templatecode {1}!!!",
                                        queueIter.Current.Id.Split('\\')[1].ToString(), queueIter.Current.Label.ToString()));
                                }
                                else
                                {
                                    using (
                                        var message = msMq.ReceiveById(queueIter.Current.Id, timeout, msmqTransaction))
                                    {
                                        if (message != null)
                                        {
                                            NotificationVO objNotification =
                                                new JavaScriptSerializer().Deserialize<NotificationVO>(
                                                    message.Body.ToString());

                                            if (objNotification != null)
                                            {
                                                log.Info("objNotification is " + message.Body);

                                                Notification notifications = new Notification
                                                {
                                                    //TODO:ID to be refactor
                                                    NotificationId = Convert.ToInt32(message.Id.Split('\\')[1]),
                                                    NotificationTemplateCode = objNotification.NotificationTemplateCode,
                                                    Reference = objNotification.Reference,
                                                    RecordStatus = objNotification.RecordStatus,
                                                    EmailStatus = objNotification.EmailStatus,
                                                    SMSStatus = objNotification.SMSStatus,
                                                    SystemNotificationStatus = objNotification.SystemNotificationStatus,
                                                    PortCode = objNotification.PortCode,
                                                    CreatedBy = objNotification.CreatedBy,
                                                    ModifiedBy = objNotification.ModifiedBy,
                                                    DateTime = objNotification.DateTime,
                                                    CreatedDate = objNotification.CreatedDate,
                                                    ModifiedDate = objNotification.ModifiedDate,
                                                    UserType = objNotification.UserType,
                                                    UserTypeId = objNotification.UserTypeId,
                                                    EmailAddress=objNotification.EmailAddress,
                                                    NotificationTemplateBase = objNotification.NotificationTemplateBase

                                                    

                                                };
                                                Stopwatch wfstopwatch = Stopwatch.StartNew();
                                                List<Notification> pendingNotifications = new List<Notification>();
                                                pendingNotifications.Add(notifications);
                                                bool notificationStatus = ProcessNotifications(pendingNotifications);

                                                wfstopwatch.Stop();
                                                msmqTransaction.Commit();
                                                if (!notificationStatus)
                                                {
                                                    log.Error("ProcessNotifications method return error..check the log");
                                                }
                                                else
                                                {
                                                    log.Info("Time taken to complete Notification " +
                                                             objNotification.NotificationId +
                                                             "Notification Template Code : " +
                                                             objNotification.NotificationTemplateCode +
                                                             " : " + wfstopwatch.ElapsedMilliseconds.ToString());
                                                }
                                            }
                                            else
                                            {
                                                log.Info("Message Queue doesn't contain any message!!");

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                catch (MessageQueueException e)
                {
                    msmqTransaction.Abort();

                    if (e.MessageQueueErrorCode ==
                        MessageQueueErrorCode.IOTimeout)
                    {
                        log.Error("No message arrived in queue.");

                    }
                    else if (e.MessageQueueErrorCode == MessageQueueErrorCode.TransactionUsage)
                    {
                        log.Error("Queue is not transactional.");
                    }
                    else
                    {
                        log.Error("Message Queue error : " + e.Message);
                    }
                }
                finally
                {
                    msmqTransaction.Dispose();
                    msMq.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {

                log_error("NotificationEngine Start() method Error : ", ex);
            }
            finally
            {
                isInProcess = false;
            }
        }
        public bool ProcessNotifications(List<Notification> pendingNotifications)
        {
            bool _status = false;
            foreach (Notification pendingNotification in pendingNotifications)
            {
                Stopwatch wfstopwatch;
                wfstopwatch = Stopwatch.StartNew();
                log.Info(" Process Started For : " + pendingNotification.NotificationTemplateCode + " " + pendingNotification.NotificationId);
                try
                {

                    Notifier notifier = Notifier.GetNotifier(pendingNotification);

                    try
                    {
                        if (notifier != null)
                        {
                            log.Info(string.Format("Process started...Time Taken to Initialize Notifier {0}  is {1} Milliseconds", pendingNotification.NotificationId, wfstopwatch.ElapsedMilliseconds.ToString()));
                            _status = notifier.ProcessNotification();
                            log.Info(string.Format("Time Taken to complete notifier.ProcessNotification() for {0}  is {1} Milliseconds", pendingNotification.NotificationTemplateCode, wfstopwatch.ElapsedMilliseconds.ToString()));
                        }
                        else
                        {
                            pendingNotification.EmailStatus = "E";
                            pendingNotification.SMSStatus = "E";
                            pendingNotification.SystemNotificationStatus = "E";
                            log.Error("Undefined Notifier :" + pendingNotification.NotificationTemplateCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        _status = false;
                        pendingNotification.EmailStatus = "E";
                        pendingNotification.SMSStatus = "E";
                        pendingNotification.SystemNotificationStatus = "E";
                        log.Info(string.Format("ProcessNotifications FAILED : Notification Id {0}  TemplateCode = {1}", pendingNotification.NotificationId, pendingNotification.NotificationTemplateCode));

                    }
                }
                catch (Exception ex)
                {
                    _status = false;
                    log_error("ProcessNotifications FAILED : Notification Id " + pendingNotification.NotificationId + ", TemplateCode = " + pendingNotification.NotificationTemplateCode, ex);
                }
                wfstopwatch.Stop();
                log.Info(string.Format("Time taken for processing of  : {0}  is {1} Milliseconds", pendingNotification.NotificationId, wfstopwatch.ElapsedMilliseconds.ToString()));


            }
            return _status;
        }
        private void log_error(string pretext, Exception ex)
        {
            string msg = pretext + " " + ex.Message;
            if (ex.InnerException != null)
            {
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    msg = msg + " Inner Exception:" + ex.InnerException.Message;
                }
            }
            log.Error(msg);

        }

        #region Schedular Jobs MSMQ Queuing

        public void PushSchedulerJobsMessageToQueue()
        {
            log.Info("PushSchedulerJobsMessageToQueue() method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            try
            {
                isInProcess = true;
                using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
                {
                    IEntityRepository _entity = new EntityRepository(_unitOfWork);
                    IUserRepository _userRepository = new UserRepository(_unitOfWork);
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);

                    #region Resource allocation not acknowledged email after conformation
                    try
                    {
                        Stopwatch rastopwatch = Stopwatch.StartNew();
                        bool status = false;
                        log.Info("Resource allocation not acknowledged email after conformations started ");
                        List<NotificationVO> auraresourceinfo = GetAuraNotConfirmedAutoEmailInfo();
                        log.Info("Resource allocation not acknowledged email after conformations email count : " + auraresourceinfo.Count);
                        
                        if (auraresourceinfo.Count > 0)
                        {
                            foreach (var item in auraresourceinfo)
                            {
                                int userid = item.CreatedBy;
                                var nextstepcompany = _userRepository.GetUserDetails(userid);
                                status = _notificationRepository.PushMessageToQueue(_entity.GetEntitiesNotification(EntityCodes.AutomatedResourceAllocation).EntityID, Convert.ToString(item.Reference), userid, nextstepcompany, item.PortCode, item.WorkflowTaskCode, item.NotificationTemplateCode);
                                if (status == false)
                                    log.Info(" Resource allocation not acknowledged email -Push message to Queue failed for reference no {0} "+ Convert.ToString(item.Reference));

                            }
                        }
                        rastopwatch.Stop();
                        log.Info(string.Format("Resource allocation not acknowledged email after conformations  is completed in {0} Milliseconds ", rastopwatch.ElapsedMilliseconds.ToString()));

                    }
                    catch (Exception ex)
                    {
                        log.Info(string.Format("resource allocation not acknowledged email after conformation error {0} {1} ", ex.Message, ex));
                    }
                    #endregion

                    #region Service request auto email Rule-1
                    try
                    {
                        Stopwatch sr1stopwatch = Stopwatch.StartNew();
                        bool status = false;
                        log.Info("Service request auto email Rule-1 email started ");
                        List<NotificationVO> srRule1 = GetServiceRequestAutoEmailRule1();
                        log.Info("Service request auto email Rule-1 email count : " + srRule1.Count);
                        if (srRule1.Count > 0)
                        {
                            foreach (var item in srRule1)
                            {
                                int userId = item.CreatedBy;
                                var nextStepCompany = new CompanyVO();
                                nextStepCompany.UserType = item.UserType;
                                nextStepCompany.UserTypeId = item.UserTypeId;
                                status = _notificationRepository.PushMessageToQueue(_entity.GetEntitiesNotification(EntityCodes.Service_Request).EntityID, Convert.ToString(item.Reference), userId, nextStepCompany, item.PortCode, item.WorkflowTaskCode, item.NotificationTemplateCode);
                                if (status == false)
                                    log.Info(" Service request auto email Rule-1 email -Push message to Queue failed for reference no {0} " + Convert.ToString(item.Reference));
                            }
                        }
                        sr1stopwatch.Stop();
                        log.Info(string.Format("Service request auto email Rule-1 email  is completed in {0} Milliseconds ", sr1stopwatch.ElapsedMilliseconds.ToString()));
                        
                    }
                    catch (Exception ex)
                    {
                        log.Info(string.Format("Service request auto email Rule-1 error {0} {1} ", ex.Message, ex));

                    }


                    #endregion

                    #region Service request auto email Rule-2
                    try
                    {
                        Stopwatch sr2stopwatch = Stopwatch.StartNew();
                        bool status = false;
                        log.Info("Service request auto email Rule-2 email started ");
                        List<NotificationVO> srRule2 = GetServiceRequestAutoEmailRule2();
                        log.Info("Service request auto email Rule-2 email count : " + srRule2.Count);

                        if (srRule2.Count > 0)
                        {
                            foreach (var item in srRule2)
                            {
                                int userId = item.CreatedBy;
                                var nextStepCompany = new CompanyVO();
                                nextStepCompany.UserType = item.UserType;
                                nextStepCompany.UserTypeId = item.UserTypeId;
                                status = _notificationRepository.PushMessageToQueue(_entity.GetEntitiesNotification(EntityCodes.Service_Request).EntityID, Convert.ToString(item.Reference), userId, nextStepCompany, item.PortCode, item.WorkflowTaskCode, item.NotificationTemplateCode);
                                if (status == false)
                                    log.Info(" Service request auto email Rule-2 email -Push message to Queue failed for reference no {0} " + Convert.ToString(item.Reference));
                           
                            }

                        }
                        sr2stopwatch.Stop();
                        log.Info(string.Format("Service request auto email Rule-2(150 mins) email  is completed in {0} Milliseconds ", sr2stopwatch.ElapsedMilliseconds.ToString()));
                        
                    }
                    catch (Exception ex)
                    {
                        log.Info(string.Format("Service request auto email Rule-2 error {0} {1} ", ex.Message, ex));

                    }

                    #endregion

                    #region Service request auto email Rule-3
                    try
                    {
                        Stopwatch sr3stopwatch = Stopwatch.StartNew();
                        bool status = false;
                        log.Info("Service request auto email Rule-3 email started ");
                        List<NotificationVO> srRule3 = GetServiceRequestAutoEmailRule3();
                        log.Info("Service request auto email Rule-3 email count : " + srRule3.Count);

                        if (srRule3.Count > 0)
                        {
                            foreach (var item in srRule3)
                            {
                                int userId = item.CreatedBy;
                                var nextStepCompany = new CompanyVO();
                                nextStepCompany.UserType = item.UserType;
                                nextStepCompany.UserTypeId = item.UserTypeId;
                                status = _notificationRepository.PushMessageToQueue(_entity.GetEntitiesNotification(EntityCodes.Service_Request).EntityID, Convert.ToString(item.Reference), userId, nextStepCompany, item.PortCode, item.WorkflowTaskCode, item.NotificationTemplateCode);
                                if (status == false)
                                    log.Info(" Service request auto email Rule-3 email -Push message to Queue failed for reference no {0} " + Convert.ToString(item.Reference));
                            }
                        }
                        sr3stopwatch.Stop();
                        log.Info(string.Format("Service request auto email Rule-3 email  is completed in {0} Milliseconds ", sr3stopwatch.ElapsedMilliseconds.ToString()));
                        
                    }
                    catch (Exception ex)
                    {
                        log.Info(string.Format("Service request auto email Rule-3 error {0} {1} ", ex.Message, ex));

                    }

                    #endregion


                    #region Craft reminder auto Email Daily
                    try
                    {
                        Stopwatch crdstopwatch = Stopwatch.StartNew();
                        bool status = false;
                        log.Info("Craft reminder auto Email Daily email Started ");
                        List<NotificationVO> crDaily = GetCraftReminderAutoEmailDaily();
                        log.Info("Craft reminder auto Email Daily email count " + crDaily.Count);
                        if (crDaily.Count > 0)
                        {
                            foreach (var item in crDaily)
                            {
                                int userId = item.CreatedBy;
                                var nextstepcompany = _userRepository.GetUserDetails(userId);
                                status = _notificationRepository.PushMessageToQueue(_entity.GetEntitiesNotification(EntityCodes.CraftReminderConfig).EntityID, Convert.ToString(item.Reference), userId, nextstepcompany, item.PortCode, item.WorkflowTaskCode, item.NotificationTemplateCode);
                                if (status == false)
                                    log.Info(" Craft reminder auto Email Daily email -Push message to Queue failed for reference no {0} " + Convert.ToString(item.Reference));
                            }
                        }
                        crdstopwatch.Stop();
                        log.Info(string.Format("Craft reminder auto Email Daily email  is completed in {0} Milliseconds ", crdstopwatch.ElapsedMilliseconds.ToString()));
                        
                    }
                    catch (Exception ex)
                    {
                        log.Info(string.Format("Craft reminder auto Email Daily error {0} {1} ", ex.Message, ex));
                    }

                    #endregion

                    #region Craft reminder auto Email Weekly
                    try
                    {
                        Stopwatch crwstopwatch = Stopwatch.StartNew();
                        bool status = false;
                        log.Info("Craft reminder auto Email Weekly email Started ");
                        List<NotificationVO> crWeekly = GetCraftReminderAutoEmailWeekly();
                        log.Info("Craft reminder auto Email Weekly email count " + crWeekly.Count);
                        if (crWeekly.Count > 0)
                        {
                            foreach (var item in crWeekly)
                            {
                                int userId = item.CreatedBy;
                                var nextstepcompany = _userRepository.GetUserDetails(userId);
                                status = _notificationRepository.PushMessageToQueue(_entity.GetEntitiesNotification(EntityCodes.CraftReminderConfig).EntityID, Convert.ToString(item.Reference), userId, nextstepcompany, item.PortCode, item.WorkflowTaskCode, item.NotificationTemplateCode);
                                if (status == false)
                                    log.Info(" Craft reminder auto Email Weekly email -Push message to Queue failed for reference no {0}  " + Convert.ToString(item.Reference));
                            }
                        }
                        crwstopwatch.Stop();
                        log.Info(string.Format("Craft reminder auto Email Weekly email  is completed in {0} Milliseconds ", crwstopwatch.ElapsedMilliseconds.ToString()));
                        
                    }
                    catch (Exception ex)
                    {
                        log.Info(string.Format("Craft reminder auto Email Weekly error {0} {1} ", ex.Message, ex));

                    }
                    
                    #endregion

                    #region Craft reminder auto Email Monthly
                    try
                    {
                        Stopwatch crmstopwatch = Stopwatch.StartNew();
                        bool status = false;
                        log.Info("Craft reminder auto Email Monthly email Started ");
                        List<NotificationVO> crMonthly = GetCraftReminderAutoEmailMonthly();
                        log.Info("Craft reminder auto Email Monthly email count : "+ crMonthly.Count);

                        if (crMonthly.Count > 0)
                        {
                            foreach (var item in crMonthly)
                            {
                                int userId = item.CreatedBy;
                                var nextstepcompany = _userRepository.GetUserDetails(userId);
                                status = _notificationRepository.PushMessageToQueue(_entity.GetEntitiesNotification(EntityCodes.CraftReminderConfig).EntityID, Convert.ToString(item.Reference), userId, nextstepcompany, item.PortCode, item.WorkflowTaskCode, item.NotificationTemplateCode);
                                if (status == false)
                                    log.Info(" Craft reminder auto Email Monthly email -Push message to Queue failed for reference no {0}  " + Convert.ToString(item.Reference));
                            }
                        }
                        crmstopwatch.Stop();
                        log.Info(string.Format("Craft reminder auto Email Monthly email  is completed in {0} Milliseconds ", crmstopwatch.ElapsedMilliseconds.ToString()));
                        
                    }
                    catch (Exception ex)
                    {
                        log.Info(string.Format("Craft reminder auto Email Monthly error {0} {1} ", ex.Message, ex));

                    }

                    #endregion


                }

            }
            catch (Exception ex)
            {
                log.Info(string.Format("PushSchedulerJobsMessageToQueue() method Error {0} {1} ", ex.Message, ex));
            }
            finally
            {
                isInProcess = false;
                wfstopwatch.Stop();
            }
            log.Info(string.Format("PushSchedulerJobsMessageToQueue() method completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));

        }

        private List<NotificationVO> GetAuraNotConfirmedAutoEmailInfo()
        {
            log.Info("Resource allocation not acknowledged Email after conformation method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            List<NotificationVO> resourceInfo = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);
                    resourceInfo = _notificationRepository.GetAuraNotConfirmedAutoEmailInfo();
                }
                catch (Exception ex)
                {
                    log_error("Resource allocation-GetAuraNotConfirmedAutoEmailInfo() method error", ex);
                }
            }
            wfstopwatch.Stop();
            log.Info(string.Format("Resource allocation not acknowledged Email after conformation method is completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));
            return resourceInfo;
        }

        private List<NotificationVO> GetServiceRequestAutoEmailRule1()
        {

            log.Info("Service request auto email Rule-1 method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            List<NotificationVO> srRule1Info = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);
                    srRule1Info = _notificationRepository.GetServiceRequestAutoEmailRule1();
                }
                catch (Exception ex)
                {
                    log_error("GetServiceRequestAutoEmailRule1() method error", ex);
                }
            }
            wfstopwatch.Stop();
            log.Info(string.Format("Service request auto email Rule-1  method  is completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));
            return srRule1Info;
        }

        private List<NotificationVO> GetServiceRequestAutoEmailRule2()
        {
            log.Info("Service request auto email Rule-2 method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            List<NotificationVO> srRule2Info = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);
                    srRule2Info = _notificationRepository.GetServiceRequestAutoEmailRule2();
                }
                catch (Exception ex)
                {
                    log_error("GetServiceRequestAutoEmailRule2() method error", ex);
                }
            }
            wfstopwatch.Stop();
            log.Info(string.Format("Service request auto email Rule-2  method  is completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));
            return srRule2Info;
        }

        private List<NotificationVO> GetServiceRequestAutoEmailRule3()
        {
            log.Info("Service request auto email Rule-3 method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            List<NotificationVO> srRule3Info = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);
                    srRule3Info = _notificationRepository.GetServiceRequestAutoEmailRule3();
                }
                catch (Exception ex)
                {
                    log_error("GetServiceRequestAutoEmailRule3() method error", ex);
                }
            }
            wfstopwatch.Stop();
            log.Info(string.Format("Service request auto email Rule-3  method  is completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));
            return srRule3Info;
        }

        private List<NotificationVO> GetCraftReminderAutoEmailDaily()
        {
            log.Info("GetCraftReminderAutoEmailDaily() method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            List<NotificationVO> crDailyInfo = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);
                    crDailyInfo = _notificationRepository.GetCraftReminderAutoEmailDaily();
                }
                catch (Exception ex)
                {
                    log_error("GetCraftReminderAutoEmailDaily() method error", ex);
                }
            }
            wfstopwatch.Stop();
            log.Info(string.Format("GetCraftReminderAutoEmailDaily() method  is completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));
            return crDailyInfo;
        }

        private List<NotificationVO> GetCraftReminderAutoEmailWeekly()
        {
            log.Info("GetCraftReminderAutoEmailWeekly() method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            List<NotificationVO> crWeelyInfo = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);
                    crWeelyInfo = _notificationRepository.GetCraftReminderAutoEmailWeekly();
                }
                catch (Exception ex)
                {
                    log_error("GetCraftReminderAutoEmailWeekly() method error", ex);
                }
            }
            wfstopwatch.Stop();
            log.Info(string.Format("GetCraftReminderAutoEmailWeekly() method  is completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));
            return crWeelyInfo;
        }

        private List<NotificationVO> GetCraftReminderAutoEmailMonthly()
        {
            log.Info("GetCraftReminderAutoEmailMonthly() method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            List<NotificationVO> crMonthlyInfo = null;
            using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);
                    crMonthlyInfo = _notificationRepository.GetCraftReminderAutoEmailMonthly();
                }
                catch (Exception ex)
                {
                    log_error("GetCraftReminderAutoEmailMonthly() method error", ex);
                }
            }
            wfstopwatch.Stop();
            log.Info(string.Format("GetCraftReminderAutoEmailMonthly() method  is completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));
            return crMonthlyInfo;
        }

        #endregion

        #region SAP Vessel Data
   
        public void ProcessSAPVesselMasterData()
        {
            log.Info("ProcessSAPVesselMasterData() method is started ");
            Stopwatch wfstopwatch = Stopwatch.StartNew();
            try
            {
                isInProcess = true;
                using (IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext()))
                {
                    IEntityRepository _entity = new EntityRepository(_unitOfWork);
                    IUserRepository _userRepository = new UserRepository(_unitOfWork);
                    INotificationRepository _notificationRepository = new NotificationRepository(_unitOfWork);
                    ISAPPostingRepository _sapPostingRepository = new SAPPostingRepository(_unitOfWork);

                    #region SAP Vessel Posting Creation
                    try
                    {
                        Stopwatch rastopwatch = Stopwatch.StartNew();
                        bool status = false;
                        log.Info("Arrival Vessel Details of ETA – 7 <= Current Date");
                        List<SAPArrivalVO> vessels = _sapPostingRepository.GetAutoSAPVesselDetails();
                        log.Info("Arrival Vessel Details of ETA – 7 <= Current Date count : " + vessels.Count);

                        if (vessels.Count > 0)
                        {
                            foreach (var item in vessels)
                            {                               
                                var totalVessels = _sapPostingRepository.GetSAPVesselPostingDetails(item);                              
                            }
                        }
                        rastopwatch.Stop();
                        log.Info(string.Format("GetSAPVesselPostingDetails Method is completed in {0} Milliseconds ", rastopwatch.ElapsedMilliseconds.ToString()));

                    }
                    catch (Exception ex)
                    {
                        log.Info(string.Format("GetSAPVesselPostingDetails Method error {0} {1} ", ex.Message, ex));
                    }
                    #endregion
                


                }

            }
            catch (Exception ex)
            {
                log.Info(string.Format("ProcessSAPVesselMasterData() method Error {0} {1} ", ex.Message, ex));
            }
            finally
            {
                isInProcess = false;
                wfstopwatch.Stop();
            }
            log.Info(string.Format("ProcessSAPVesselMasterData() method completed in {0} Milliseconds ", wfstopwatch.ElapsedMilliseconds.ToString()));

        }
        #endregion


        #region IDisposable Methods

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}