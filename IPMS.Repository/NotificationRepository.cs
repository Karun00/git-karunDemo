using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using log4net;
using log4net.Config;
using System.Diagnostics;
using IPMS.Domain.ValueObjects;
using System.Messaging;
using System.Web.Script.Serialization;
using AutoMapper;
using System.Data.Entity.Infrastructure;


namespace IPMS.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILog log;
        public NotificationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(NotificationRepository));

        }
        public List<Notification> GetPendingNotifications(int laterThanId)
        {
            Stopwatch wfstopwatch;
            wfstopwatch = Stopwatch.StartNew();
            log.Info("Query Fetching Started");
            var pendingNotifications = (from n in _unitOfWork.Repository<Notification>().Query().Select()
                                        where ((n.EmailStatus == "O" || n.SMSStatus == "O" || n.SystemNotificationStatus == "O") && n.NotificationId > laterThanId)
                                        select n).OrderBy(info => info.NotificationId).ToList();
            wfstopwatch.Stop();
            log.Info("Query Fetching Completed in  " + wfstopwatch.Elapsed.ToString());
            return pendingNotifications;
        }

        public List<NotificationTemplate> GetNotificationTemplateByEntityTaskOrTemplateCode(int entityId, string workFlowTaskCode, string portcode, string NotificationTemplateCode = null)
        {
            DbRawSqlQuery<NotificationTemplate> notificationTemplate = null;
            if (!string.IsNullOrEmpty(workFlowTaskCode) && !string.IsNullOrEmpty(NotificationTemplateCode))
            {
                notificationTemplate = _unitOfWork.SqlQuery<NotificationTemplate>("SELECT NT.NotificationTemplateId, NT.NotificationTemplateCode, NT.NotificationTemplateName, NP.PortCode, NT.EntityID, NT.WorkflowTaskCode, NT.PortCode, NT.IsEmail, NT.EmailSubject, NT.EmailTemplate, NT.IsSMS, NT.SMSTemplate, NT.IsSysMessage, NT.SysMessageTemplate, NT.NotificationTemplateBase, NT.RecordStatus, NT.CreatedBy, NT.CreatedDate, NT.ModifiedBy, NT.ModifiedDate FROM NotificationTemplate NT inner join NotificationPort NP on NT.NotificationTemplateCode = NP.NotificationTemplateCode where NT.EntityID=@p0 and NT.WorkflowTaskCode=@p1 and NP.PortCode=@p2 and NT.NotificationTemplateCode=@p3", entityId, workFlowTaskCode, portcode, NotificationTemplateCode);
            }
            else if (!string.IsNullOrEmpty(NotificationTemplateCode))
            {
                notificationTemplate = _unitOfWork.SqlQuery<NotificationTemplate>("SELECT NT.NotificationTemplateId, NT.NotificationTemplateCode, NT.NotificationTemplateName, NP.PortCode, NT.EntityID, NT.WorkflowTaskCode, NT.PortCode, NT.IsEmail, NT.EmailSubject, NT.EmailTemplate, NT.IsSMS, NT.SMSTemplate, NT.IsSysMessage, NT.SysMessageTemplate, NT.NotificationTemplateBase, NT.RecordStatus, NT.CreatedBy, NT.CreatedDate, NT.ModifiedBy, NT.ModifiedDate FROM NotificationTemplate NT inner join NotificationPort NP on NT.NotificationTemplateCode = NP.NotificationTemplateCode where NT.EntityID=@p0 and NT.NotificationTemplateCode=@p1 and NP.PortCode=@p2", entityId, NotificationTemplateCode, portcode);
            }
            else if (!string.IsNullOrEmpty(workFlowTaskCode))
            {
                notificationTemplate = _unitOfWork.SqlQuery<NotificationTemplate>("SELECT NT.NotificationTemplateId, NT.NotificationTemplateCode, NT.NotificationTemplateName, NP.PortCode, NT.EntityID, NT.WorkflowTaskCode, NT.PortCode, NT.IsEmail, NT.EmailSubject, NT.EmailTemplate, NT.IsSMS, NT.SMSTemplate, NT.IsSysMessage, NT.SysMessageTemplate, NT.NotificationTemplateBase, NT.RecordStatus, NT.CreatedBy, NT.CreatedDate, NT.ModifiedBy, NT.ModifiedDate FROM NotificationTemplate NT inner join NotificationPort NP on NT.NotificationTemplateCode = NP.NotificationTemplateCode where NT.EntityID=@p0 and NT.WorkflowTaskCode=@p1 and NP.PortCode=@p2", entityId, workFlowTaskCode, portcode);
            }

            return Mapper.Map<List<NotificationTemplate>>(notificationTemplate);
        }
        public List<NotificationTemplate> GetNotificationTemplateByEntityTask(int entityId, string workFlowTaskCode, string portcode)
        {
            //TODO: Using LINQ query taking long time, hence converted to Queries & Stored Procedure
            var notificationTemplate =
                _unitOfWork.SqlQuery<NotificationTemplate>(
                    "SELECT NT.NotificationTemplateId, NT.NotificationTemplateCode, NT.NotificationTemplateName, NP.PortCode, NT.EntityID, NT.WorkflowTaskCode, NT.PortCode, NT.IsEmail, NT.EmailSubject, NT.EmailTemplate, NT.IsSMS, NT.SMSTemplate, NT.IsSysMessage, NT.SysMessageTemplate, NT.NotificationTemplateBase, NT.RecordStatus, NT.CreatedBy, NT.CreatedDate, NT.ModifiedBy, NT.ModifiedDate FROM NotificationTemplate NT inner join NotificationPort NP on NT.NotificationTemplateCode = NP.NotificationTemplateCode where NT.EntityID=@p0 and NT.WorkflowTaskCode=@p1 and NP.PortCode=@p2",
                    entityId, workFlowTaskCode, portcode);

            return Mapper.Map<List<NotificationTemplate>>(notificationTemplate);
            //return notificationTemplate;
        }

        public List<NotificationTemplate> GetNotificationTemplateByEntityTask(int entityId, string workFlowTaskCode, string portcode, string NotificationTemplateBase, int PermitRequestID, string EmailAddress)
        {

             var notificationTemplate= _unitOfWork.SqlQuery<NotificationTemplate>(
                  "SELECT distinct NT.NotificationTemplateId, NT.NotificationTemplateCode, NT.NotificationTemplateName,w.PortCode,ip.EmailAddress,NT.EntityID, NT.WorkflowTaskCode, NT.IsEmail, NT.EmailSubject, NT.EmailTemplate, NT.IsSMS, NT.SMSTemplate,NT.IsSysMessage, NT.SysMessageTemplate, NT.NotificationTemplateBase, NT.RecordStatus,NT.CreatedBy, NT.CreatedDate,NT.ModifiedBy, NT.ModifiedDate, w.WorkflowInstanceId from WorkflowInstance w inner join WorkflowProcess wp on wp.WorkflowInstanceId=w.WorkflowInstanceId inner join PermitRequest p on w.WorkflowInstanceId=p.WorkflowInstanceId inner join IndividualPermitApplicationDetails ip on ip.PermitRequestID=p.PermitRequestID inner join NotificationTemplate NT on NT.EntityID=w.EntityID where NT.EntityID=@p0 and NT.WorkflowTaskCode=@p1 and w.PortCode=@p2 and NT.NotificationTemplateBase=@p3 and p.PermitRequestID=@p4 and ip.EmailAddress=@p5", entityId, workFlowTaskCode, portcode, NotificationTemplateBase, PermitRequestID, EmailAddress);

            return Mapper.Map<List<NotificationTemplate>>(notificationTemplate);
        }

        public bool PushMessageToQueue(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode, string NotificationTemplateCode = null)
        {

            //TODO: Common logic to be implemented, Same logic implemented in NotificationPublisher, but this is used for API calls too
            bool _status = false;

            Stopwatch wfstopwatch;
            wfstopwatch = Stopwatch.StartNew();
            log.Info("Fetching Notification Templates GetNotificationTemplateByEntityTask...Started");

            List<NotificationTemplate> notificationTemplate = new List<NotificationTemplate>();
            if (!string.IsNullOrEmpty(workFlowTaskCode) && !string.IsNullOrEmpty(NotificationTemplateCode))
            {
                notificationTemplate = GetNotificationTemplateByEntityTaskOrTemplateCode(entityId, workFlowTaskCode, portcode, NotificationTemplateCode);
            }
            else if (!string.IsNullOrEmpty(NotificationTemplateCode))
            {
                notificationTemplate = GetNotificationTemplateByEntityTaskOrTemplateCode(entityId, workFlowTaskCode, portcode, NotificationTemplateCode);
            }
            else if (!string.IsNullOrEmpty(workFlowTaskCode))
            {
                notificationTemplate = GetNotificationTemplateByEntityTaskOrTemplateCode(entityId, workFlowTaskCode, portcode);
            }

            wfstopwatch.Stop();
            log.Info("Fetching Notification Templates GetNotificationTemplateByEntityTask...Completed in  " + wfstopwatch.Elapsed.ToString());

            if (notificationTemplate.Count != 0)
            {
                if (company != null)
                {
                    foreach (var item in notificationTemplate)
                    {
                        NotificationVO notifications = new NotificationVO()
                        {
                            NotificationTemplateCode = item.NotificationTemplateCode,
                            Reference = reference,
                            RecordStatus = item.RecordStatus,
                            EmailStatus = "O",
                            SMSStatus = "O",
                            SystemNotificationStatus = "O",
                            PortCode = portcode,
                            CreatedBy = userid,
                            UserID = userid,
                            ModifiedBy = userid,
                            DateTime = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            UserType = company.UserType,
                            UserTypeId = company.UserTypeId

                        };

                        #region MSMQ Implementation code
                        MessageQueue msMq = null;
                        MessageQueueTransaction msmqTransaction = new MessageQueueTransaction();

                        msmqTransaction.Begin();

                        string queueName;
                        //queueName = (ConfigurationManager.AppSettings["MSMQPath"] == null ? "." : ConfigurationManager.AppSettings["MSMQPath"].ToString() + "-" + notifications.NotificationTemplateCode);
                        queueName = ConfigurationManager.AppSettings["MSMQPath"];

                        if (!MessageQueue.Exists(queueName))
                            msMq = MessageQueue.Create(queueName, true);
                        else
                            msMq = new MessageQueue(queueName, true);

                        Trustee trEveryOne = new Trustee("EVERYONE");
                        MessageQueueAccessControlEntry everyoneace =
                            new MessageQueueAccessControlEntry(trEveryOne, MessageQueueAccessRights.FullControl,
                                AccessControlEntryType.Allow);
                        AccessControlList dacleveryone = new AccessControlList();
                        dacleveryone.Add(everyoneace);
                        msMq.SetPermissions(dacleveryone);

                        Trustee t = new Trustee("NETWORK SERVICE");
                        MessageQueueAccessControlEntry ace = new MessageQueueAccessControlEntry(t, MessageQueueAccessRights.FullControl, AccessControlEntryType.Allow);
                        AccessControlList dacl = new AccessControlList();
                        dacl.Add(ace);
                        msMq.SetPermissions(dacl);


                        try
                        {
                            using (Message msgNotification = new Message())
                            {
                                string json = new JavaScriptSerializer().Serialize(notifications);
                                //myMessage.BodyStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
                                msgNotification.Body = json;
                                msgNotification.Priority = (notifications.NotificationTemplateCode == "URPG"
                                    ? MessagePriority.VeryHigh
                                    : MessagePriority.Normal);
                                msgNotification.Label = notifications.NotificationTemplateCode;

                                msMq.UseJournalQueue = true;
                                log.Info("Message is : " + json);
                                msMq.Send(msgNotification, msmqTransaction);
                                log.Info("Message Pushed to " + queueName + msgNotification.Body);
                                msmqTransaction.Commit();
                                _status = true;
                            }

                        }
                        catch (MessageQueueException e)
                        {
                            _status = false;
                            log.Error(e.Message);
                            msmqTransaction.Abort();
                        }
                        catch (Exception ex)
                        {
                            _status = false;
                            log.Error(ex.Message);
                            msmqTransaction.Abort();
                        }
                        finally
                        {
                            msMq.Close();
                        }

                        #endregion

                    }
                }
            }
            return _status;

        }

        public List<NotificationVO> GetAuraNotConfirmedAutoEmailInfo()
        {
            var sn = _unitOfWork.SqlQuery<NotificationVO>("dbo.USP_AURA_NOTCONFIRMED_AUTO_EMAIL").ToList();
            return sn;
        }

        public List<NotificationVO> GetServiceRequestAutoEmailRule1()
        {
            var sr1 = _unitOfWork.SqlQuery<NotificationVO>("dbo.usp_Service_Request_Auto_Email_Rule1").ToList();            
            return sr1;
        }
        public List<NotificationVO> GetServiceRequestAutoEmailRule2()
        {
            var sr2 = _unitOfWork.SqlQuery<NotificationVO>("dbo.usp_Service_Request_Auto_Email_Rule2").ToList();
            return sr2;
        }
        public List<NotificationVO> GetServiceRequestAutoEmailRule3()
        {
            var sr3 = _unitOfWork.SqlQuery<NotificationVO>("dbo.usp_Service_Request_Auto_Email_Rule3").ToList();
            return sr3;
        }
        public List<NotificationVO> GetCraftReminderAutoEmailDaily()
        {
            var crD = _unitOfWork.SqlQuery<NotificationVO>("dbo.USP_CRAFT_REMINDER_AUTO_EMAIL_DAILY").ToList();
            return crD;
        }
        public List<NotificationVO> GetCraftReminderAutoEmailWeekly()
        {
            var crW = _unitOfWork.SqlQuery<NotificationVO>("dbo.USP_CRAFT_REMINDER_AUTO_EMAIL_Weekly").ToList();
            return crW;
        }
        public List<NotificationVO> GetCraftReminderAutoEmailMonthly()
        {
            var crM = _unitOfWork.SqlQuery<NotificationVO>("dbo.USP_CRAFT_REMINDER_AUTO_EMAIL_MONTHLY").ToList();
            return crM;
        }
    }
}
