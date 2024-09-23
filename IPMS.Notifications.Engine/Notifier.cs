using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Repository;
using log4net;
using log4net.Config;
using IPMS.Domain.ValueObjects;

namespace IPMS.Notifications.Engine
{
    /// <summary>
    /// Common Class for sending Notifications for all type of Requests
    /// </summary>
    public abstract class Notifier : IDisposable
    {
        protected Notification pendingNotification;
        protected NotificationTemplateVO notificationTemplate;
        protected Entity entityDetails;
        protected List<NotificationRole> roles;
        protected IUserRepository _userRepository;
        protected IPortRepository _portRepository;
        protected IUnitOfWork _NotifierunitOfWork;
        public static ILog log;
        private Stopwatch wfstopwatch;
        private NotificationStatus _NotificationStatus;

        #region Private Variables
        private string msg = string.Empty;
        private static string _IsEmailRequired;
        private static string _IsSMSRequired;
        #endregion

        /// <summary>
        /// Notifier Constructor
        /// </summary>
        /// <param name="pendintNotification"></param>
        /// <param name="notificationTemplate"></param>
        /// <param name="entityDetails"></param>
        /// <param name="roles"></param>
        protected Notifier(Notification pendintNotification, NotificationTemplateVO notificationTemplate, Entity entityDetails, List<NotificationRole> roles)
        {
            this.pendingNotification = pendintNotification;
            _NotifierunitOfWork = new UnitOfWork(new TnpaContext());
            _userRepository = new UserRepository(_NotifierunitOfWork);
            _portRepository = new PortRepository(_NotifierunitOfWork);
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
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(Notifier));
                Stopwatch wfstopwatch;
                wfstopwatch = Stopwatch.StartNew();
                log.Debug("Notifier --> GetNotifier() : Fethching Notification Templates by Templatecode started ");
                var templateCode = new SqlParameter("@templatecode", pendingNotification.NotificationTemplateCode);
                var notificationTemplate = unitOfWork.SqlQuery<NotificationTemplateVO>("Select NotificationTemplateId,NotificationTemplateCode,NotificationTemplateName,EntityID,WorkflowTaskCode,PortCode,IsEmail,EmailSubject,EmailTemplate,IsSMS,SMSTemplate,IsSysMessage,SysMessageTemplate,NotificationTemplateBase,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate from NotificationTemplate where NotificationTemplateCode=@templatecode", templateCode).FirstOrDefault<NotificationTemplateVO>();
                wfstopwatch.Stop();
                log.Info(string.Format("Notifier --> GetNotifier() : Fethching Notification Templates by Templatecode completed {0} Milli seconds", wfstopwatch.ElapsedMilliseconds.ToString()));

                _IsEmailRequired = unitOfWork.SqlQuery<string>("Select EmailRequired from PortConfiguration").FirstOrDefault();
                _IsSMSRequired = unitOfWork.SqlQuery<string>("Select SMSRequired  from PortConfiguration").FirstOrDefault();

                if (notificationTemplate != null && string.IsNullOrEmpty(notificationTemplate.EmailTemplate))
                {
                    notificationTemplate.EmailTemplate = "";
                }
                if (notificationTemplate != null && string.IsNullOrEmpty(notificationTemplate.SMSTemplate))
                {
                    notificationTemplate.SMSTemplate = "";
                }

                if (notificationTemplate != null && string.IsNullOrEmpty(notificationTemplate.SysMessageTemplate))
                {
                    notificationTemplate.SysMessageTemplate = "";
                }
                if (notificationTemplate != null)
                {
                    var entityDetails = (from e in unitOfWork.Repository<Entity>().Queryable().Where(e => e.EntityID.Equals(notificationTemplate.EntityID))
                                         select e).FirstOrDefault<Entity>();

                    var roles = (from nr in unitOfWork.Repository<NotificationRole>().Query().Include(r => r.Role).Select()
                                 where nr.NotificationTemplateCode == notificationTemplate.NotificationTemplateCode && nr.RecordStatus == RecordStatus.Active
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
                        case EntityCodes.WasteDeclarationAN:
                            notifier = new WDArrivalNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
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
                        case EntityCodes.ColdWorkSupplServiceRequest:
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
                        case EntityCodes.Supp_DryDockExtension:
                            notifier = new SuppDryDockExtensionNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.DepartureNotice:
                            notifier = new DepartureNoticeNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Dredging_Volume:
                            notifier = new DredgingVolumeNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Berth_Occupation:
                            notifier = new BerthOccupationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Dredging_Priority:
                            notifier = new DredgingPriorityNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Supp_DryDock:
                            notifier = new SuppDryDockNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.PortEntryPassApplication:
                            notifier = new PortEntryPassApplicationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Hour24Report625:
                            notifier = new Hour24Report625Notifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        //case EntityCodes.SSAverification:
                        //    notifier = new PortEntryPassApplicationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                        //    break;
                        case EntityCodes.Docking_Plan:
                            notifier = new DockingPlanNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.ExternalDivingRegister:
                            notifier = new ExternalDivingOnCompletionNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.DivingRequestOnCompletion:
                            notifier = new DivingRequestOnCompletionNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.AutomatedResourceAllocation:
                            notifier = new AutomatedResourceAllocationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.MobileTask_Code:
                            notifier = new MobileScheduleNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.MobileScheduleTaskExecution:
                            notifier = new MobileScheduleTaskNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.CraftOut_Commision:
                            notifier = new CraftOutOfCommissionNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.CraftIn_Commision:
                            notifier = new CraftOutOfCommissionNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.DHMAN:
                            notifier = new DHMArrivalNotificationNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.ServiceRequest_Shifting:
                            notifier = new ServiceRequestShiftingNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.Capture_ArrDeparture:
                            notifier = new VesselCallAnchorageNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.ServiceRecording:
                            notifier = new ServiceRecordingNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.FloatingSuppServiceRequest:
                            notifier = new SupplymentaryServiceRequestNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.HotWorkSuppServiceRequest:
                            notifier = new SupplymentaryServiceRequestNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.HotColdSuppServiceRequest:
                            notifier = new SupplymentaryServiceRequestNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
                            break;
                        case EntityCodes.WaterSuppServiceRequest:
                            notifier = new SupplymentaryServiceRequestNotifier(pendingNotification, notificationTemplate, entityDetails, roles);
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
            bool sts = false;
            var openNotification = pendingNotification;
            try
            {

                List<User> usersToBeNotified = GetUsersToBeNotified();
                Dictionary<string, string> messageTemplatePlaceHolders = GetTokensDictionary();
                try
                {
                    if (string.IsNullOrEmpty(messageTemplatePlaceHolders["%PortCode%"]))
                    {
                        messageTemplatePlaceHolders["%PortCode%"] = pendingNotification.PortCode;
                    }
                }
                catch (KeyNotFoundException)
                {
                    //Nothing to do 
                }

                try
                {
                    if (string.IsNullOrEmpty(messageTemplatePlaceHolders["%PortName%"]))
                    {
                        var PortName = _portRepository.GetPortDetailsByPortCode(pendingNotification.PortCode).PortName;
                        messageTemplatePlaceHolders["%PortName%"] = PortName;
                    }
                }
                catch (KeyNotFoundException)
                {
                    //Nothing to do 
                }

                _NotificationStatus = new NotificationStatus();


                if (usersToBeNotified.Count > 1)
                {
                    if (_IsEmailRequired == "Y")
                    {
                        if (notificationTemplate.EmailTemplate != "")
                        {
                            _NotificationStatus.EmailStatus = ProcessEmailForListOfUsers(openNotification, messageTemplatePlaceHolders, usersToBeNotified);
                        }
                        else
                        {
                            _NotificationStatus.Message = "EmailTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                            _NotificationStatus.EmailStatus = false;
                            log.Error(_NotificationStatus.Message);
                        }
                    }
                    else
                    {
                        _NotificationStatus.Message = "EmailRequired set as No";
                        _NotificationStatus.EmailStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }
                    if (_IsSMSRequired == "Y")
                    {
                        if (notificationTemplate.SMSTemplate != "")
                        {
                            _NotificationStatus.SMSStatus = ProcessSMSForListOfUsers(openNotification, messageTemplatePlaceHolders, usersToBeNotified);
                        }
                        else
                        {
                            _NotificationStatus.Message = "SMSTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                            _NotificationStatus.SMSStatus = false;
                            log.Error(_NotificationStatus.Message);
                        }
                    }
                    else
                    {
                        _NotificationStatus.Message = "SMSRequired set as No";
                        _NotificationStatus.SMSStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }

                    if (notificationTemplate.SysMessageTemplate != "")
                    {
                        _NotificationStatus.SystemNotificationStatus = ProcessSystemNotificationForListOfUsers(openNotification, messageTemplatePlaceHolders, usersToBeNotified);
                    }
                    else
                    {
                        _NotificationStatus.Message = "SysMessageTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                        _NotificationStatus.SystemNotificationStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }

                }
                else if (usersToBeNotified.Count == 1)
                {
                    if (_IsEmailRequired == "Y")
                    {
                        if (notificationTemplate.EmailTemplate != "")
                        {
                            _NotificationStatus.EmailStatus = ProcessEmail(openNotification, messageTemplatePlaceHolders, usersToBeNotified[0]);
                        }
                       
                        else
                        {
                            _NotificationStatus.Message = "EmailTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                            _NotificationStatus.EmailStatus = false;
                            log.Error(_NotificationStatus.Message);
                        }
                    }
                    else
                    {
                        _NotificationStatus.Message = "EmailRequired set as No";
                        _NotificationStatus.EmailStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }
                   
                    if (_IsSMSRequired == "Y")
                    {
                        if (notificationTemplate.SMSTemplate != "")
                        {
                            _NotificationStatus.SMSStatus = ProcessSMS(openNotification, messageTemplatePlaceHolders, usersToBeNotified[0]);
                        }
                        else
                        {
                            _NotificationStatus.Message = "SMSTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                            _NotificationStatus.SMSStatus = false;
                            log.Error(_NotificationStatus.Message);
                        }
                    }
                   else if (_IsSMSRequired == "N")
                    {
                        if (notificationTemplate.SMSTemplate != "")
                        {
                            _NotificationStatus.SMSStatus = ProcessSMS(openNotification, messageTemplatePlaceHolders, usersToBeNotified[0]);
                        }
                        else
                        {
                            _NotificationStatus.Message = "SMSTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                            _NotificationStatus.SMSStatus = false;
                            log.Error(_NotificationStatus.Message);
                        }
                    }
                    else
                    {
                        _NotificationStatus.Message = "SMSRequired set as No";
                        _NotificationStatus.SMSStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }

                    if (notificationTemplate.SysMessageTemplate != "")
                    {
                        _NotificationStatus.SystemNotificationStatus = ProcessSystemNotification(openNotification, messageTemplatePlaceHolders, usersToBeNotified[0]);
                    }
                    else
                    {
                        _NotificationStatus.Message = "SysMessageTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                        _NotificationStatus.SystemNotificationStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }

                }
                else if (usersToBeNotified.Count == 0)
                {
                    User user = new User();
                    if (_IsEmailRequired == "Y")
                    {
                        if (notificationTemplate.EmailTemplate != "")
                        {
                            _NotificationStatus.EmailStatus = ProcessEmail(openNotification, messageTemplatePlaceHolders, user);
                        }

                        else
                        {
                            _NotificationStatus.Message = "EmailTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                            _NotificationStatus.EmailStatus = false;
                            log.Error(_NotificationStatus.Message);
                        }
                    }
                    else
                    {
                        _NotificationStatus.Message = "EmailRequired set as No";
                        _NotificationStatus.EmailStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }

                    if (_IsSMSRequired == "Y")
                    {
                        if (notificationTemplate.SMSTemplate != "")
                        {
                            _NotificationStatus.SMSStatus = ProcessSMS(openNotification, messageTemplatePlaceHolders, usersToBeNotified[0]);
                        }
                        else
                        {
                            _NotificationStatus.Message = "SMSTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                            _NotificationStatus.SMSStatus = false;
                            log.Error(_NotificationStatus.Message);
                        }
                    }
                    else
                    {
                        _NotificationStatus.Message = "SMSRequired set as No";
                        _NotificationStatus.SMSStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }

                    if (notificationTemplate.SysMessageTemplate != "")
                    {
                        _NotificationStatus.SystemNotificationStatus = ProcessSystemNotification(openNotification, messageTemplatePlaceHolders, usersToBeNotified[0]);
                    }
                    else
                    {
                        _NotificationStatus.Message = "SysMessageTemplate should not be empty for NotificationID : " + openNotification.NotificationId;
                        _NotificationStatus.SystemNotificationStatus = false;
                        log.Error(_NotificationStatus.Message);
                    }

                }
                else
                {
                    _NotificationStatus.EmailStatus = false;
                    _NotificationStatus.SMSStatus = false;
                    _NotificationStatus.SystemNotificationStatus = false;
                    _NotificationStatus.Message = "No users found to Send Email / SMS / SystemNotification for Notification " + openNotification.NotificationId + "(" + openNotification.NotificationTemplateCode + ")";
                    log.Error(_NotificationStatus.Message);
                }

                if (_NotificationStatus.EmailStatus == true)
                {
                    pendingNotification.EmailStatus = "D";
                }
                else
                {
                    pendingNotification.EmailStatus = "E";
                }
                if (_NotificationStatus.SMSStatus == true)
                {
                    pendingNotification.SMSStatus = "D";
                }
                else
                {
                    pendingNotification.SMSStatus = "E";
                }

                if (_NotificationStatus.SystemNotificationStatus == true)
                {
                    pendingNotification.SystemNotificationStatus = "D";
                }
                else
                {
                    pendingNotification.SystemNotificationStatus = "E";
                }
                sts = true;
            }
            catch (Exception ex)
            {
                msg = "ProcessNotification method ERROR: " + openNotification.NotificationId + "(" + openNotification.NotificationTemplateCode + ") " + ex.Message;

                if (ex.InnerException != null)
                {
                    if (!string.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        msg = msg + " Inner Exception:" + ex.InnerException.Message;
                    }
                }
                _NotificationStatus.Message = msg;
                log.Error(msg);
            }
            return sts;
        }


        /// <summary>
        /// To Process Pending Notification wise SystemNotification for Single User
        /// </summary>
        /// <param name="openNotification"></param>
        /// <param name="messageTemplatePlaceHolders"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool ProcessSystemNotification(Notification openNotification, Dictionary<string, string> messageTemplatePlaceHolders, User user)
        {
            bool status = true;
            string systemNotificationContent = ConstructMessage(user, messageTemplatePlaceHolders, this.notificationTemplate);
            systemNotificationContent = this.notificationTemplate.SysMessageTemplate;


            using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    log.Info("Sysetm Notification User ID: " + user.UserID + " NotificationId Id : " + pendingNotification.NotificationId);
                    uow.ExecuteSqlCommand("insert into dbo.SystemNotification(UserID, NotificationId, PortCode, NotificationText, CreatedBy, CreatedDate, IsRead) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", user.UserID, pendingNotification.NotificationId, pendingNotification.PortCode, systemNotificationContent, pendingNotification.CreatedBy, DateTime.Now.ToString(), "N");

                }
                catch (Exception ex)
                {
                    status = false;
                    msg = "ProcessSystemNotification ERROR: " + ex.Message;
                    if (ex.InnerException != null)
                    {
                        if (!string.IsNullOrEmpty(ex.InnerException.Message))
                        {
                            msg = msg + " Inner Exception:" + ex.InnerException.Message;
                        }
                    }
                    log.Error(msg);

                }
            }
            return status;
        }

        /// <summary>
        /// To Process Pending Notification wise SystemNotification for Multipl Users
        /// </summary>
        /// <param name="openNotification"></param>
        /// <param name="messageTemplatePlaceHolders"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        private bool ProcessSystemNotificationForListOfUsers(Notification openNotification, Dictionary<string, string> messageTemplatePlaceHolders, List<User> users)
        {
            bool status = true;
            string systemNotificationContent = ConstructMessageForListOfUsers(users, messageTemplatePlaceHolders, this.notificationTemplate);
            systemNotificationContent = this.notificationTemplate.SysMessageTemplate;

            using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    foreach (User user in users)
                    {
                        log.Info("Sysetm Notification User ID: " + user.UserID + " NotificationId Id : " + pendingNotification.NotificationId);
                        uow.ExecuteSqlCommand("insert into dbo.SystemNotification(UserID, NotificationId, PortCode, NotificationText, CreatedBy, CreatedDate, IsRead) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", user.UserID, pendingNotification.NotificationId, pendingNotification.PortCode, systemNotificationContent, pendingNotification.CreatedBy, DateTime.Now.ToString(), "N");
                    }
                }
                catch (Exception ex)
                {
                    status = false;
                    msg = "ProcessSystemNotificationForListOfUsers ERROR: " + ex.Message;
                    if (ex.InnerException != null)
                    {
                        if (!string.IsNullOrEmpty(ex.InnerException.Message))
                        {
                            msg = msg + " Inner Exception:" + ex.InnerException.Message;
                        }
                    }
                    log.Error(msg);
                }
            }
            return status;
        }

        /// <summary>
        /// To Process Pending Notification wise SMS for Single User
        /// </summary>
        /// <param name="openNotification"></param>
        /// <param name="messageTemplatePlaceHolders"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool ProcessSMS(Notification openNotification, Dictionary<string, string> messageTemplatePlaceHolders, User user)
        {
            bool status = false;
            string smsBody = ConstructMessage(user, messageTemplatePlaceHolders, this.notificationTemplate);
            smsBody = this.notificationTemplate.SMSTemplate;

            try
            {
                //using (SMSSender smsSender = new SMSSender())
                //{
                string result = string.Empty;
                SMSSender smsSender = new SMSSender();
                result = smsSender.SendSMS(user.ContactNo, smsBody);

                if (result.ToString().ToUpper().IndexOf("OK:") > -1)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                //}
            }
            catch (Exception ex)
            {
                status = false;
                msg = "ProcessSMS ERROR: " + ex.Message;
                if (ex.InnerException != null)
                {
                    if (!string.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        msg = msg + " Inner Exception:" + ex.InnerException.Message;
                    }
                }
                log.Error(msg);
            }


            if (status == true)
            {
                using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
                {

                    var SMSLogInsert = uow.ExecuteSqlCommand("insert into dbo.SMSLog(UserId,NotificationId,SMSText,ContactNo,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,UserIds) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", user.UserID, openNotification.NotificationId, smsBody, user.ContactNo, openNotification.CreatedBy, DateTime.Now.ToString(), openNotification.ModifiedBy, DateTime.Now.ToString(), user.UserID);
                }
            }

            return status;

        }

        /// <summary>
        /// To Process Pending Notification wise SystemNotification for Multiple Users
        /// </summary>
        /// <param name="openNotification"></param>
        /// <param name="messageTemplatePlaceHolders"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        private bool ProcessSMSForListOfUsers(Notification openNotification, Dictionary<string, string> messageTemplatePlaceHolders, List<User> users)
        {
            bool status = false;

            var listContactNo = String.Join(";", users.GroupBy(x => x.ContactNo).Select(x => x.First()).ToList().ToArray().Select(x => x.ContactNo));
            var listuserids = String.Join(",", users.GroupBy(x => x.UserID).Select(x => x.First()).ToList().ToArray().Select(x => x.UserID));
            string smsBody = ConstructMessageForListOfUsers(users, messageTemplatePlaceHolders, this.notificationTemplate);
            smsBody = this.notificationTemplate.SMSTemplate;

            try
            {
                //using (SMSSender smsSender = new SMSSender())
                //{
                string result = string.Empty;
                SMSSender smsSender = new SMSSender();
                result = smsSender.SendSMS(listContactNo, smsBody);
                log.Debug(result);
                if (result.ToString().ToUpper().IndexOf("OK:") > -1)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                //}
            }
            catch (Exception ex)
            {
                status = false;
                msg = "ProcessSMSForListOfUsers ERROR: " + ex.Message;
                if (ex.InnerException != null)
                {
                    if (!string.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        msg = msg + " Inner Exception:" + ex.InnerException.Message;
                    }
                }
                log.Error(msg);
            }


            if (status == true)
            {
                using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
                {
                    var SMSLogInsert = uow.ExecuteSqlCommand("insert into dbo.SMSLog(UserId,NotificationId,SMSText,ContactNo,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,UserIds) values(null,@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)", openNotification.NotificationId, smsBody, listContactNo, openNotification.CreatedBy, DateTime.Now.ToString(), openNotification.ModifiedBy, DateTime.Now.ToString(), listuserids);
                }
            }

            return status;
        }

        private bool ProcessEmail(Notification openNotification, Dictionary<string, string> messageTemplatePlaceHolders, User user)
        {
            bool status = false;
            if (user == null)
            {
                msg = "No user found for Notification ID :" + openNotification.NotificationId + " Templatecode " + openNotification.NotificationTemplateCode;
                _NotificationStatus.Message = msg;
            }

            if (user.EmailID == "")
            {
                msg = "Email IDs not found for Notification ID :" + openNotification.NotificationId + " Templatecode " + openNotification.NotificationTemplateCode;
                _NotificationStatus.Message = msg;
                log.Error(msg);
            }
            else
            {
                string emailBody = ConstructMessage(user, messageTemplatePlaceHolders, this.notificationTemplate);
                emailBody = this.notificationTemplate.EmailTemplate;

                if (string.IsNullOrEmpty(emailBody))
                {
                    msg = "Tokens data not found for Notification ID :" + openNotification.NotificationId + " Templatecode " + openNotification.NotificationTemplateCode;
                    _NotificationStatus.Message = msg;
                    log.Error(msg);
                }
                else if (openNotification.NotificationTemplateBase == "M")
                {
                    var mail = openNotification.EmailAddress;
                    EmailSender emailSender = new EmailSender();


                    wfstopwatch = Stopwatch.StartNew();
                    status = emailSender.SendEMail(emailBody, notificationTemplate.EmailSubject, mail);
                    wfstopwatch.Stop();
                    log.Info(string.Format("Time take for SendEMail method for Email {0}  Templatecode {1} in {2} Milliseconds ", user.EmailID, openNotification.NotificationTemplateCode, wfstopwatch.ElapsedMilliseconds.ToString()));
                }
                else
                {
                    EmailSender emailSender = new EmailSender();


                    wfstopwatch = Stopwatch.StartNew();
                    status = emailSender.SendEMail(emailBody, notificationTemplate.EmailSubject, user.EmailID);
                    wfstopwatch.Stop();
                    log.Info(string.Format("Time take for SendEMail method for Email {0}  Templatecode {1} in {2} Milliseconds ", user.EmailID, openNotification.NotificationTemplateCode, wfstopwatch.ElapsedMilliseconds.ToString()));

                    if (status == true)
                    {
                        using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
                        {
                            var EmailLogInsert = uow.ExecuteSqlCommand("insert into dbo.EmailLog(UserId,NotificationId,EmailSubject,EmailBody,EmailId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", user.UserID.ToString(CultureInfo.InvariantCulture), openNotification.NotificationId, notificationTemplate.EmailSubject, emailBody, user.EmailID, openNotification.CreatedBy, DateTime.Now.ToString(CultureInfo.InvariantCulture), openNotification.ModifiedBy, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        }
                    }
                    else
                    {
                        msg = "Email sending failed for Email :" + user.EmailID + " Templatecode " + openNotification.NotificationTemplateCode;
                        _NotificationStatus.Message = msg;
                        log.Error(msg);
                    }
                }
            }
            return status;
        }

        private bool ProcessEmailForListOfUsers(Notification openNotification, Dictionary<string, string> messageTemplatePlaceHolders, List<User> users)
        {
            bool status = false;

            if (users.Count == 0)
            {
                msg = "No users found to execute ProcessEmailForListOfUsers method for Notification ID :" + openNotification.NotificationId + " Templatecode " + openNotification.NotificationTemplateCode;
                _NotificationStatus.Message = msg;
                log.Error(msg);
            }
            else
            {
                var listemailids = String.Join(";", users.GroupBy(x => x.EmailID).Select(x => x.First()).ToArray().Select(x => x.EmailID));
                var listuserids = String.Join(",", users.GroupBy(x => x.UserID).Select(x => x.First()).ToArray().Select(x => x.UserID));
                string emailBody = ConstructMessageForListOfUsers(users, messageTemplatePlaceHolders, this.notificationTemplate);
                emailBody = this.notificationTemplate.EmailTemplate;

                if (string.IsNullOrEmpty(emailBody))
                {
                    msg = "Tokens data not found for Notification ID :" + openNotification.NotificationId + " Templatecode " + openNotification.NotificationTemplateCode;
                    _NotificationStatus.Message = msg;
                    log.Error(msg);
                }
                else
                {
                    if (listemailids.Length == 0 || listemailids == null)
                    {
                        msg = "Email IDs not found for Notification ID :" + openNotification.NotificationId + " Templatecode " + openNotification.NotificationTemplateCode;
                        _NotificationStatus.Message = msg;
                        log.Error(msg);
                    }
                    else
                    {
                        EmailSender emailSender = new EmailSender();

                        wfstopwatch = Stopwatch.StartNew();
                        status = emailSender.SendEMail(emailBody, notificationTemplate.EmailSubject, listemailids);
                        wfstopwatch.Stop();
                        log.Debug("Time take for SendEMail method for Email :" + listemailids + " Templatecode " + openNotification.NotificationTemplateCode + " in " + wfstopwatch.Elapsed.ToString());

                        if (status == true)
                        {
                            using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
                            {
                                var EmailLogInsert = uow.ExecuteSqlCommand("insert into dbo.EmailLog(UserIds,NotificationId,EmailSubject,EmailBody,EmailId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", listuserids, openNotification.NotificationId, notificationTemplate.EmailSubject, emailBody, listemailids, openNotification.CreatedBy, DateTime.Now.ToString(), openNotification.ModifiedBy, DateTime.Now.ToString());
                            }
                        }
                        else
                        {
                            msg = "Email sending failed for Email :" + listemailids + " Templatecode " + openNotification.NotificationTemplateCode;
                            _NotificationStatus.Message = msg;
                            log.Error(msg);
                        }
                    }
                }
            }
            return status;
        }

        public string ConstructMessage(User user, Dictionary<string, string> messageTemplatePlaceHolders, NotificationTemplateVO template)
        {
            int TokenCnt = 0;
            int TokenNullCnt = 0;
            foreach (var placeHoder in messageTemplatePlaceHolders)
            {
                template.EmailTemplate = template.EmailTemplate.Replace(placeHoder.Key, placeHoder.Value);
                template.EmailSubject = template.EmailSubject.Replace(placeHoder.Key, placeHoder.Value);
                template.SMSTemplate = template.SMSTemplate.Replace(placeHoder.Key, placeHoder.Value);
                template.SysMessageTemplate = template.SysMessageTemplate.Replace(placeHoder.Key, placeHoder.Value);

                if (string.IsNullOrEmpty(placeHoder.Value))
                {
                    TokenNullCnt += 1;
                }
                TokenCnt += 1;
            }

            if (TokenCnt == TokenNullCnt && TokenCnt > 0) ///Tokens Data not found in object
            {
                template.EmailTemplate = "";
            }
            else
            {
                template.EmailTemplate = template.EmailTemplate.Replace("[UserName]", user.FirstName + " " + user.LastName);
                template.EmailTemplate = template.EmailTemplate.Replace("[NAME]", "IPMS ADMIN");
                template.SMSTemplate = template.SMSTemplate.Replace("[UserName]", user.FirstName + " " + user.LastName);
                template.SMSTemplate = template.SMSTemplate.Replace("[NAME]", "IPMS ADMIN");
                template.SysMessageTemplate = template.SysMessageTemplate.Replace("[UserName]", user.FirstName + " " + user.LastName);
                template.SysMessageTemplate = template.SysMessageTemplate.Replace("[NAME]", "IPMS ADMIN");
            }
            return template.EmailTemplate;
        }

        private string ConstructMessageForListOfUsers(List<User> users, Dictionary<string, string> messageTemplatePlaceHolders, NotificationTemplateVO template)
        {
            int TokenCnt = 0;
            int TokenNullCnt = 0;
            foreach (var placeHoder in messageTemplatePlaceHolders)
            {
                template.EmailTemplate = template.EmailTemplate.Replace(placeHoder.Key, placeHoder.Value);
                template.EmailSubject = template.EmailSubject.Replace(placeHoder.Key, placeHoder.Value);
                template.SMSTemplate = template.SMSTemplate.Replace(placeHoder.Key, placeHoder.Value);
                template.SysMessageTemplate = template.SysMessageTemplate.Replace(placeHoder.Key, placeHoder.Value);

                if (string.IsNullOrEmpty(placeHoder.Value))
                {
                    TokenNullCnt += 1;
                }
                TokenCnt += 1;
            }
            if (TokenCnt == TokenNullCnt && TokenCnt > 0) ///Tokens Data not found in object
            {
                template.EmailTemplate = "";
            }
            else
            {
                template.EmailTemplate = template.EmailTemplate.Replace("[UserName]", "All");
                template.EmailTemplate = template.EmailTemplate.Replace("[NAME]", "IPMS ADMIN");
                template.SMSTemplate = template.SMSTemplate.Replace("[UserName]", "All");
                template.SMSTemplate = template.SMSTemplate.Replace("[NAME]", "IPMS ADMIN");
                template.SysMessageTemplate = template.SysMessageTemplate.Replace("[UserName]", "All");
                template.SysMessageTemplate = template.SysMessageTemplate.Replace("[NAME]", "IPMS ADMIN");
            }
            return template.EmailTemplate;
        }


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
        public string Message { get; set; }
    }
}
