using System;
using System.Collections.Generic;
using System.Configuration;
using System.Messaging;
using System.Web.Script.Serialization;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using log4net;

namespace IPMS.Services.WorkFlow
{
    public class NotificationPublisher : INotificationPublisher

    {
        private readonly IUnitOfWork _unitOfWork;

        private static readonly ILog notificationlog = LogManager.GetLogger(typeof(NotificationPublisher));
        private INotificationRepository _notificationRepository;
        public NotificationPublisher(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = new NotificationRepository(_unitOfWork);
        }
        //public NotificationPublishers(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //    _notificationRepository = new NotificationRepository(_unitOfWork);
        //}

        public NotificationPublisher()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _notificationRepository = new NotificationRepository(_unitOfWork);
        }

        public void Publish(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode)
        {
           //bool retvalue=_notificationPublisherService.PushMessageToQueue(entityId,reference,userid,company,portcode,workFlowTaskCode);
            List<NotificationTemplate> notificationTemplate = _notificationRepository.GetNotificationTemplateByEntityTask(entityId, workFlowTaskCode, portcode);

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

                        //TODO: Separate message queue to be created based on Template, present creating only one
                        //queueName = (ConfigurationManager.AppSettings["MSMQPath"] ==null ? "." : ConfigurationManager.AppSettings["MSMQPath"].ToString() + "-" + notifications.NotificationTemplateCode);
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
                                notificationlog.Info("Message is : " + json);
                                msMq.Send(msgNotification, msmqTransaction);
                                notificationlog.Info("Message Pushed to " + queueName + msgNotification.Body);
                                msmqTransaction.Commit();
                            }

                        }
                        catch (MessageQueueException e)
                        {
                            notificationlog.Error(e.Message);
                            msmqTransaction.Abort();
                        }
                        catch (Exception ex)
                        {
                            notificationlog.Error(ex.Message);
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
        }

        public void ManualPublish(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode, string NotificationTemplateBase, int PermitRequestID, string EmailAddress)
        {
            //bool retvalue=_notificationPublisherService.PushMessageToQueue(entityId,reference,userid,company,portcode,workFlowTaskCode);
            List<NotificationTemplate> notificationTemplate = _notificationRepository.GetNotificationTemplateByEntityTask(entityId, workFlowTaskCode, portcode, NotificationTemplateBase, PermitRequestID, EmailAddress);

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
                            UserTypeId = company.UserTypeId,
                            EmailAddress=EmailAddress,
                            NotificationTemplateBase=item.NotificationTemplateBase

                           

                        };

                        #region MSMQ Implementation code
                        MessageQueue msMq = null;
                        MessageQueueTransaction msmqTransaction = new MessageQueueTransaction();

                        msmqTransaction.Begin();

                        string queueName;

                        //TODO: Separate message queue to be created based on Template, present creating only one
                        //queueName = (ConfigurationManager.AppSettings["MSMQPath"] ==null ? "." : ConfigurationManager.AppSettings["MSMQPath"].ToString() + "-" + notifications.NotificationTemplateCode);
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
                                notificationlog.Info("Message is : " + json);
                                msMq.Send(msgNotification, msmqTransaction);
                                notificationlog.Info("Message Pushed to " + queueName + msgNotification.Body);
                                msmqTransaction.Commit();
                            }

                        }
                        catch (MessageQueueException e)
                        {
                            notificationlog.Error(e.Message);
                            msmqTransaction.Abort();
                        }
                        catch (Exception ex)
                        {
                            notificationlog.Error(ex.Message);
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
        }


    }

   
}