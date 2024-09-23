using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Repository;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using log4net;
using log4net.Config;
using System.Linq;
using System.Diagnostics;

namespace IPMS.Notifications
{
    /// <summary>
    /// Common Class for sending Notifications for all type of Requests
    /// </summary>
    public abstract class Notifier
    {
        protected Notification pendingNotification;
        protected NotificationTemplate notificationTemplate;
        protected Entity entityDetails;
        protected List<NotificationRole> roles;
        protected IUserRepository _userRepository;
        protected IUnitOfWork _NotifierunitOfWork;
        public ILog log;
        private string msg = string.Empty;
        private Stopwatch wfstopwatch;
        private EmailSender emailSender;
        private SMSSender smsSender;
        /// <summary>
        /// Notifier Constructor
        /// </summary>
        /// <param name="pendintNotification"></param>
        /// <param name="notificationTemplate"></param>
        /// <param name="entityDetails"></param>
        /// <param name="roles"></param>
        protected Notifier(Notification pendintNotification, NotificationTemplate notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
        {
            this.pendingNotification = pendintNotification;
            _NotifierunitOfWork = new UnitOfWork(new TnpaContext());
            _userRepository = new UserRepository(_NotifierunitOfWork);
            this.notificationTemplate = notificationTemplate;
            this.entityDetails = entityDetails;
            this.roles = roles;
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(Notifier));

        }

        /// <summary>
        /// To get the Request Notifer from the Pending Notification
        /// </summary>
        /// <param name="pendingNotification"></param>
        /// <returns></returns>
        public static Notifier GetNotifier(Notification pendingNotification)
        {
            Notifier notifier = null;
            using (IUnitOfWork unitOfWork = new UnitOfWork(new TnpaContext()))
            {

                var notificationTemplate = (from nt in unitOfWork.Repository<NotificationTemplate>().Query().Select()
                                            where nt.NotificationTemplateCode == pendingNotification.NotificationTemplateCode
                                            select nt).FirstOrDefault<NotificationTemplate>();

                if (notificationTemplate != null)
                {
                    var entityDetails = (from e in unitOfWork.Repository<Entity>().Query().Select()
                                         where e.EntityID == notificationTemplate.EntityID
                                         select e).FirstOrDefault<Entity>();

                    var roles = (from nr in unitOfWork.Repository<NotificationRole>().Query().Include(r=>r.Role).Select()
                                 where nr.NotificationTemplateCode == notificationTemplate.NotificationTemplateCode
                                 select nr).ToList<NotificationRole>();

                    switch (entityDetails.EntityCode)
                    {
                        case EntityCodes.Arrival_Notification:
                            notifier = new ArrivalNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.IMDGAN:
                            notifier = new IMDGArrivalNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.ISPSAN:
                            notifier = new ISPSArrivalNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.PortHealthAN:
                            notifier = new PHArrivalNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Service_Request:
                            notifier = new ServiceRequestNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.VACHREQ:
                            notifier = new VesselAgentChangeNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.AGENTREG:
                            notifier = new AgentRegistrationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.LICENSEREQ:
                            notifier = new LicensingRequestNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.PilotExemption:
                            notifier = new PilotExemptionRequestNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.User_Registration:
                            notifier = new UserRegistrationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.DivingRequestOccupation:
                            notifier = new DivingRequestOccupationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.BerthMaintenance_Approval:
                            notifier = new BerthMaintenanceApprovalNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.BerthMaintenanceCompletion_Approval:
                            notifier = new BerthMaintenanceCompletionNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.VESLREG:
                            notifier = new VesselRegistrationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Vessel_ETAChange:
                            notifier = new VesselETAChangeNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.SupplymentaryServiceRequest:
                            notifier = new SupplymentaryServiceRequestNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Statement_Fact:
                            notifier = new StatementFactNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Fuel_Requisition:
                            notifier = new FuelRequisitionNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Berth_PreScheduling:
                            notifier = new BerthPreSchedulingNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Fuel_Receipt:
                            notifier = new FuelReceiptNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.User:
                            notifier = new UserNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.CraftReminderConfig:
                            notifier = new CraftReminderNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.VesselArrests:
                            notifier = new VesselArrestImmobilizationSAMSAStopNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.AutomatedSlotting:
                            notifier = new AutomatedSlottingNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Supp_DryDockUndock:
                            notifier = new AutomatedSlottingNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;

                        //Added By Santosh On 20th Dec 2014 For Departure Notice Request Notifications
                        case EntityCodes.DepartureNotice:
                            notifier = new DepartureNoticeNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                    }

                }
            }


            return notifier;
        }

        /// <summary>
        /// To Process the Pending Notification
        /// </summary>
        /// <returns></returns>
        public bool ProcessNotification()
        {
            bool sts = true;

            List<User> usersToBeNotified = null;
            try
            {
                var openNotification = pendingNotification;//_notificationRepository.GetPendingNotificationById(pendingNotification.NotificationId);

                #region Updating of Email/SMS/SystemNotification Status field as (P)rocess
                if (openNotification != null)
                {
                    if (openNotification.EmailStatus == "O")
                    {
                        msg = "ProcessNotification Email record Started.... " + openNotification.NotificationId;
                        log.Info(msg);
                        pendingNotification.EmailStatus = "P";
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET EmailStatus ='P', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                    }

                    if (openNotification.SMSStatus == "O")
                    {
                        msg = "ProcessNotification SMS record Started.... " + openNotification.NotificationId;
                        log.Info(msg);
                        pendingNotification.SMSStatus = "P";
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET SMSStatus ='P', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                    }

                    if (openNotification.SystemNotificationStatus == "O")
                    {
                        msg = "ProcessNotification SystemNotification record Started.... " + openNotification.NotificationId;
                        log.Info(msg);
                        pendingNotification.SystemNotificationStatus = "P";
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET SystemNotificationStatus ='P', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                    }
                }
                #endregion

                #region Fetching List of Users notifications to be send
                usersToBeNotified = GetUsersToBeNotified();
                msg = "Total No.of Users Identified is : " + usersToBeNotified.Count;
                log.Info(msg);
                #endregion

                Dictionary<string, string> messageTemplatePlaceHolders = GetTokensDictionary();

                NotificationStatus status = new NotificationStatus();

                foreach (User user in usersToBeNotified)
                {
                    #region Sending Notification


                    #region Updating EmailStatus field back in Notification table based on result
                    
                    wfstopwatch = Stopwatch.StartNew();
                    
                    msg = "ProcessEmail Started....";
                    log.Info(msg);

                    status.EmailStatus = ProcessEmail(user, messageTemplatePlaceHolders);
                    msg = "ProcessEmail Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    if (status.EmailStatus == true)
                    {
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET EmailStatus ='D', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                    }
                    else
                    {
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET EmailStatus ='E', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                        msg = "Email Status updation failed.... ";
                        log.Info(msg);

                    }
                    wfstopwatch.Stop();
                    msg = "ProcessEmail Notification updation Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);
                    
                    #endregion

                    #region Updating SMSStatus field back in Notification table based on result
                    wfstopwatch = Stopwatch.StartNew();
                    msg = "SMSStatus Started....";
                    log.Info(msg);
                    
                    status.SMSStatus = ProcessSMS(user, messageTemplatePlaceHolders);
                    msg = "ProcessSMS Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    if (status.SMSStatus == true)
                    {
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET SMSStatus ='D', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                        msg = "SMS Status updated as D.... ";
                        log.Info(msg);
                    }
                    else
                    {
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET SMSStatus ='E', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                        msg = "SMS Status updation failed...";
                        log.Info(msg);

                    }
                    wfstopwatch.Stop();
                    msg = "ProcessSMS Notification updation Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    #endregion

                    #region Updating SystemNotificationStatus field back in Notification table based on result
                    wfstopwatch = Stopwatch.StartNew();
                    msg = "SystemNotificationStatus Started....";
                    log.Info(msg);

                    status.SystemNotificationStatus = ProcessSystemNotification(user, messageTemplatePlaceHolders);

                    if (status.SystemNotificationStatus == true)
                    {
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET SystemNotificationStatus ='D', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                        msg = "SystemNotificationStatus Status updated as D.... ";
                        log.Info(msg);

                    }
                    else
                    {
                        _NotifierunitOfWork.ExecuteSqlCommand("update Notification SET SystemNotificationStatus ='E', ModifiedBy = @p0, ModifiedDate = @p1 where NotificationId = @p2", openNotification.ModifiedBy, DateTime.Now, openNotification.NotificationId);
                        msg = "SystemNotificationStatus Status updation failed...";
                        log.Info(msg);
                    }
                    wfstopwatch.Stop();
                    msg = "SystemNotificationStatus Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    #endregion

                    #endregion
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);

            }
            //finally
            //{
            //    if (_NotifierunitOfWork != null)
            //    {
            //        log.Debug("_NotifierunitOfWork object disposed");
            //        _NotifierunitOfWork.Dispose();
            //    }
            //}
            return sts;

        }

        /// <summary>
        /// Sending Email to the speicified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="messageTemplatePlaceHolders"></param>
        /// <returns></returns>
        private bool ProcessEmail(User user, Dictionary<string, string> messageTemplatePlaceHolders)
        {
            try
            {
                if (pendingNotification.EmailStatus == "P")
                {
                    string emailBody = notificationTemplate.EmailTemplate;
                    foreach (var placeHoder in messageTemplatePlaceHolders)
                    {
                        emailBody = emailBody.Replace(placeHoder.Key, placeHoder.Value);
                    }
                    msg = "ProcessEmail emailBody preparation Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    emailBody = emailBody.Replace("[UserName]", user.FirstName + " " + user.LastName);
                    emailBody = emailBody.Replace("[NAME]", "IPMS ADMIN");

                    EMailLog(user.UserID, pendingNotification.NotificationId, emailBody, notificationTemplate.EmailSubject, user.EmailID);
                    msg = "ProcessEmail EMailLog data insertion Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    msg = "Email Body :" + emailBody + " EmailSubject : " + notificationTemplate.EmailSubject + " EmailId :" + user.EmailID;
                    log.Info(msg);

                    if (emailSender == null)
                        InitializeEmailSMSObject();

                    emailSender.SendEMail(emailBody, notificationTemplate.EmailSubject, user.EmailID);
                    msg = "ProcessEmail Sending Email Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);
                    
                    msg = "Email sent successfully...";
                    log.Info(msg);

                }
                return true;
            }
            catch (Exception ex)
            {
                msg = "ProcessEmail() method exception " + ex.Message;
                log.Error(msg);
                return false;
            }
        }

        public void EMailLog(int UserId, int NotificationId, string msgbody, string subject, string toEmailId)
        {
            int CreatedBy = UserId;
            int ModifiedBy = UserId;
            _NotifierunitOfWork.ExecuteSqlCommand("insert into dbo.EmailLog values(" + UserId + "," + NotificationId + ",'" + subject + "','" + msgbody + "','" + toEmailId + "'," + CreatedBy + ",getdate()," + ModifiedBy + ",getdate())");

        }

        public void SMSLog(int UserId, int NotificationId, string SMSText, string ContactNo)
        {
            int CreatedBy = UserId;
            int ModifiedBy = UserId;
            _NotifierunitOfWork.ExecuteSqlCommand("insert into dbo.SMSLog values(" + UserId + "," + NotificationId + ",'" + SMSText + "','" + ContactNo + "'," + CreatedBy + ",getdate()," + ModifiedBy + ",getdate())");

        }

        /// <summary>
        /// Sending SMS to the specified user OfficialMobileNo
        /// </summary>
        /// <param name="user"></param>
        /// <param name="messageTemplatePlaceHolders"></param>
        /// <returns></returns>
        private bool ProcessSMS(User user, Dictionary<string, string> messageTemplatePlaceHolders)
        {
            try
            {
                if (pendingNotification.SMSStatus == "P")
                {
                    msg = pendingNotification.EmailStatus;
                    log.Info(msg);
                    string msgBody = notificationTemplate.SMSTemplate;
                    foreach (var placeHoder in messageTemplatePlaceHolders)
                    {
                        msgBody = msgBody.Replace(placeHoder.Key, placeHoder.Value);
                    }
                    msg = "ProcessSMS msgBody preparation Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    msgBody = msgBody.Replace("[UserName]", user.FirstName + " " + user.LastName);

                    SMSLog(user.UserID, pendingNotification.NotificationId, msgBody, user.ContactNo);
                    msg = "ProcessSMS SMSLog data insertion Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    if (smsSender == null)
                        InitializeEmailSMSObject();

                    string result = smsSender.SendSMS(user.ContactNo, msgBody); //This can be made as singleton or static..
                    msg = "ProcessSMS SendSMS Email Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    if (result.ToString().ToUpper().IndexOf("ERROR") > 0)
                    {
                        msg = "SMS Sending Error : " + result;
                        log.Info(result);
                        return false;
                    }
                    msg = "SMS sent successfully...";
                    log.Info(msg);

                }
                return true;
            }
            catch (Exception ex)
            {
                msg = "SMS Sending failed..." + ex.Message;
                log.Error(msg);
                return false;
            }
        }

        /// <summary>
        /// Sending System Notification to the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="messageTemplatePlaceHolders"></param>
        /// <returns></returns>
        private bool ProcessSystemNotification(User user, Dictionary<string, string> messageTemplatePlaceHolders)
        {
            try
            {
                if (pendingNotification.SystemNotificationStatus == "P")
                {
                    string notificationText = notificationTemplate.SysMessageTemplate;
                    foreach (var placeHoder in messageTemplatePlaceHolders)
                    {
                        notificationText = notificationText.Replace(placeHoder.Key, placeHoder.Value);
                        notificationText = notificationText.Replace(placeHoder.Key, entityDetails.EntityName);

                    }
                    msg = "ProcessSystemNotification notificationText preparation Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    _NotifierunitOfWork.ExecuteSqlCommand("insert into dbo.SystemNotification(UserID, NotificationId, PortCode, NotificationText, CreatedBy, CreatedDate, IsRead) values(" + user.UserID + "," + pendingNotification.NotificationId + ",'" + pendingNotification.PortCode + "','" + notificationText + "'," + pendingNotification.CreatedBy + ",getdate(),'N')");
                    msg = "ProcessSystemNotification SystemNotification data insertion Completed for " + pendingNotification.NotificationId + " in " + wfstopwatch.Elapsed.ToString();
                    log.Info(msg);

                    msg = "System Notification completed successfully...";
                    log.Info(msg);

                }
                return true;
            }
            catch (Exception ex)
            {
                msg = "ProcessSystemNotification() exception " + ex.Message;
                log.Error(msg);
                return false;
            }
        }

        private void InitializeEmailSMSObject()
        {
            if (emailSender == null)
                emailSender = new EmailSender();

            if (smsSender == null)
                smsSender = new SMSSender();
        }

        /// <summary>
        /// Used to assign EmailSender and SMSSender from Notification engine instead of every time Reintializing in Notifier
        /// </summary>
        /// <param name="objemail"></param>
        /// <param name="objsms"></param>
        public void SetEmailSMSObject (EmailSender objemail, SMSSender objsms)
        {
            if (objemail != null)
                this.emailSender = objemail;

            if (objsms != null)
                this.smsSender = objsms;

        }

        /// <summary>
        /// To get the Port code
        /// </summary>
        /// <returns></returns>
        public abstract string GetPortCode();

        /// <summary>
        /// To get List of Users for sending notifications
        /// </summary>
        /// <returns></returns>
        public abstract List<User> GetUsersToBeNotified();

        /// <summary>
        /// To Get the values from the object speicifed in Notification Template in between placeholder (%) name
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, string> GetTokensDictionary();


    }

    /// <summary>
    /// To maintain the Notification status after sending as True or False
    /// </summary>
    public class NotificationStatus
    {
        public bool EmailStatus { get; set; }
        public bool SMSStatus { get; set; }
        public bool SystemNotificationStatus { get; set; }
    }
}
