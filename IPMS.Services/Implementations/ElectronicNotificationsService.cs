using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using log4net;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Domain.DTOS;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ElectronicNotificationsService : ServiceBase, IElectronicNotificationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        // private readonly ILog log;
        private IAccountRepository _accountRepository;

        public ElectronicNotificationsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        public ElectronicNotificationsService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
           // log =
            LogManager.GetLogger(typeof(ElectronicNotificationsService));
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        public string AddNotification(NotificationTemplate notificationdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                if (notificationdata.IsEmail == "True")
                    notificationdata.IsEmail = "Y";
                else
                    notificationdata.IsEmail = "N";
                if (notificationdata.IsSMS == "True")
                    notificationdata.IsSMS = "Y";
                else
                    notificationdata.IsSMS = "N";
                if (notificationdata.IsSysMessage == "True")
                    notificationdata.IsSysMessage = "Y";
                else
                    notificationdata.IsSysMessage = "N";

                notificationdata.CreatedDate = DateTime.Now;
                notificationdata.CreatedBy = userid;
                notificationdata.ModifiedBy = userid;
                notificationdata.ModifiedDate = DateTime.Now;

                List<NotificationRole> notificationRoles = notificationdata.NotificationRoles.ToList();
                notificationdata.NotificationRoles = null;
                List<NotificationPort> notificationPorts = notificationdata.NotificationPorts.ToList();
                notificationdata.NotificationPorts = null;
                notificationdata.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<NotificationTemplate>().Insert(notificationdata);
                _unitOfWork.SaveChanges();

                foreach (var item in notificationPorts)
                {
                    item.NotificationTemplateCode = notificationdata.NotificationTemplateCode;
                    item.RecordStatus = notificationdata.RecordStatus;
                    item.CreatedBy = notificationdata.CreatedBy;
                    item.CreatedDate = notificationdata.CreatedDate;
                    item.ModifiedBy = notificationdata.ModifiedBy;
                    item.ModifiedDate = notificationdata.ModifiedDate;
                    _unitOfWork.Repository<NotificationPort>().Insert(item);
                    _unitOfWork.SaveChanges();
                }

                foreach (var item in notificationRoles)
                {
                    item.NotificationTemplateCode = notificationdata.NotificationTemplateCode;
                    item.RecordStatus = notificationdata.RecordStatus;
                    item.CreatedBy = notificationdata.CreatedBy;
                    item.CreatedDate = notificationdata.CreatedDate;
                    item.ModifiedBy = notificationdata.ModifiedBy;
                    item.ModifiedDate = notificationdata.ModifiedDate;
                    _unitOfWork.Repository<NotificationRole>().Insert(item);
                    _unitOfWork.SaveChanges();
                }
                return notificationdata.NotificationTemplateCode;
            });
        }

        public List<Entity> GetEntityDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var entities = (from e in _unitOfWork.Repository<Entity>().Queryable().AsEnumerable<Entity>()
                                select e).ToList();

                return entities;
            });
        }

        public List<string> GetTokens(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string[] arrayOfTokensForEntity = null;
                List<string> tokensList = new List<string>();
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select().AsEnumerable<Entity>()
                              where e.EntityID == id
                              select e).FirstOrDefault<Entity>();
                if (entity.Tokens != null)
                {
                    arrayOfTokensForEntity = entity.Tokens.Split(',');
                    tokensList.AddRange(arrayOfTokensForEntity);
                }
                return tokensList;
            });
        }

        public List<Role> GetRolesDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var roles = (from r in _unitOfWork.Repository<Role>().Queryable().AsEnumerable<Role>()
                             select r).ToList();

                return roles;
            });
        }

        public List<Port> GetPortsDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var ports = (from p in _unitOfWork.Repository<Port>().Queryable().AsEnumerable<Port>()
                             select p).ToList();

                return ports;
            });
        }

        public List<NotificationDetails> GetNotifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return GetNotificationDetails();
            });
        }

        private List<NotificationDetails> GetNotificationDetails()
        {
            var notifications = (from n in _unitOfWork.Repository<NotificationTemplate>().Queryable().AsEnumerable<NotificationTemplate>()
                                 join e in _unitOfWork.Repository<Entity>().Queryable().AsEnumerable<Entity>()
                                 on n.EntityID equals e.EntityID
                                 where n.RecordStatus == "A"
                                 select new NotificationDetails
                                 {
                                     NotificationTemplateCode = n.NotificationTemplateCode,
                                     NotificationTemplateName = n.NotificationTemplateName,
                                     IsEmail = n.IsEmail,
                                     EmailTemplate = n.EmailTemplate,
                                     IsSMS = n.IsSMS,
                                     SMSTemplate = n.SMSTemplate,
                                     IsSysMessage = n.IsSysMessage,
                                     SysMessageTemplate = n.SysMessageTemplate,
                                     EntityID = n.EntityID,
                                     WorkflowTaskCode = n.WorkflowTaskCode,
                                     RecordStatus = n.RecordStatus,
                                     EntityName = e.EntityName,
                                     EmailSubject = n.EmailSubject,
                                     NotificationTemplateBase = n.NotificationTemplateBase
                                 }).ToList();

            foreach (var item in notifications)
            {
                List<Role> rolesList = (from r in _unitOfWork.Repository<Role>().Queryable()
                                        join nr in _unitOfWork.Repository<NotificationRole>().Queryable()
                                        on r.RoleID equals nr.RoleID
                                        where nr.NotificationTemplateCode == item.NotificationTemplateCode && nr.RecordStatus == "A"
                                        select r).ToList<Role>();

                List<RoleVO> rlist = rolesList.MapToDto();

                item.Roles = rlist.MapToEntity();
            }

            foreach (var item in notifications)
            {
                List<Port> portsList = (from p in _unitOfWork.Repository<Port>().Queryable()
                                        join np in _unitOfWork.Repository<NotificationPort>().Queryable()
                                        on p.PortCode equals np.PortCode
                                        where np.NotificationTemplateCode == item.NotificationTemplateCode && np.RecordStatus == "A"
                                        select p).ToList<Port>();

                List<PortVO> plist = portsList.MapToDTO();

                item.Ports = plist.MapToEntity();
            }

            return notifications;
        }

        public List<NotificationTemplateVO> GetAllNotifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var notifications = (from n in _unitOfWork.Repository<NotificationTemplate>().Query().Include(p => p.Entity).Include(p => p.NotificationPorts).Include(p => p.NotificationRoles).Include(p => p.SubCategory).Select()
                                     select n).ToList<NotificationTemplate>();

                return notifications.MapToDTO();
            });
        }


        public string DeleteNotification(NotificationTemplate notificationdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                notificationdata.ModifiedDate = DateTime.Now;
                notificationdata.ModifiedBy = userid;
                var notificationObj = _unitOfWork.Repository<NotificationTemplate>().Find(notificationdata.NotificationTemplateCode);
               
                notificationObj.RecordStatus = "I";
                notificationObj.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<NotificationTemplate>().Update(notificationObj);
                _unitOfWork.SaveChanges();

                var notificationRoleObj = (from nr in _unitOfWork.Repository<NotificationRole>().Query().Select()
                                           where nr.NotificationTemplateCode == notificationdata.NotificationTemplateCode
                                           select nr).ToList();

                foreach (var item in notificationRoleObj)
                {
                    item.RecordStatus = "I";
                    item.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<NotificationRole>().Update(item);
                    _unitOfWork.SaveChanges();
                }

                var notificationPortObj = (from np in _unitOfWork.Repository<NotificationPort>().Query().Select()
                                           where np.NotificationTemplateCode == notificationdata.NotificationTemplateCode
                                           select np).ToList();

                foreach (var item in notificationPortObj)
                {
                    item.RecordStatus = "I";
                    item.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<NotificationPort>().Update(item);
                    _unitOfWork.SaveChanges();
                }

                return notificationdata.NotificationTemplateCode;
            });
        }

        public string ModifyNotification(NotificationTemplate notificationdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);

                if (notificationdata.IsEmail == "True")
                    notificationdata.IsEmail = "Y";
                if (notificationdata.IsEmail == "False")
                    notificationdata.IsEmail = "N";
                if (notificationdata.IsSMS == "True")
                    notificationdata.IsSMS = "Y";
                if (notificationdata.IsSMS == "False")
                    notificationdata.IsSMS = "N";
                if (notificationdata.IsSysMessage == "True")
                    notificationdata.IsSysMessage = "Y";
                if (notificationdata.IsSysMessage == "False")
                    notificationdata.IsSysMessage = "N";

                notificationdata.CreatedDate = DateTime.Now;
                notificationdata.CreatedBy = userid;
                notificationdata.ModifiedDate = DateTime.Now;
                notificationdata.ModifiedBy = userid;

                List<NotificationRole> notificationRoles = notificationdata.NotificationRoles.ToList();
                List<NotificationPort> notificationPorts = notificationdata.NotificationPorts.ToList();

                var notificationRoleObj = (from nr in _unitOfWork.Repository<NotificationRole>().Query().Select()
                                           where nr.NotificationTemplateCode == notificationdata.NotificationTemplateCode
                                           select nr).ToList();

                if (notificationRoleObj != null)
                {
                    foreach (var notificationRole in notificationRoleObj)
                    {
                        if (!notificationdata.NotificationRoles.Any(l => l.RoleID == notificationRole.RoleID))
                        {
                            notificationRole.RecordStatus = "I";
                            _unitOfWork.Repository<NotificationRole>().Update(notificationRole);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }
               
                foreach (var notificationRole in notificationdata.NotificationRoles)
                {
                    var roleObj = (from nr in _unitOfWork.Repository<NotificationRole>().Query().Select()
                                   where nr.NotificationTemplateCode == notificationdata.NotificationTemplateCode && nr.RoleID == notificationRole.RoleID
                                   select nr).FirstOrDefault<NotificationRole>();
                    if (roleObj != null)
                    {
                        if (roleObj.RecordStatus == "I")
                        {
                            roleObj.RecordStatus = "A";
                            roleObj.ObjectState = ObjectState.Modified;
                            _unitOfWork.Repository<NotificationRole>().Update(roleObj);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    if (roleObj == null)
                    {
                        notificationRole.RecordStatus = notificationdata.RecordStatus;
                        notificationRole.CreatedBy = notificationdata.CreatedBy;
                        notificationRole.CreatedDate = notificationdata.CreatedDate;
                        notificationRole.ModifiedBy = notificationdata.ModifiedBy;
                        notificationRole.ModifiedDate = notificationdata.ModifiedDate;
                        _unitOfWork.Repository<NotificationRole>().Insert(notificationRole);
                        _unitOfWork.SaveChanges();
                    }
                }
                if (notificationdata.NotificationTemplateBase == "U")
                {
                    foreach (var notificationRole in notificationRoleObj)
                    {
                        if (notificationRole.RecordStatus == "A")
                        {
                            notificationRole.RecordStatus = "I";
                            _unitOfWork.Repository<NotificationRole>().Update(notificationRole);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }
                var notificationPortObj = (from np in _unitOfWork.Repository<NotificationPort>().Query().Select()
                                           where np.NotificationTemplateCode == notificationdata.NotificationTemplateCode
                                           select np).ToList();

                if (notificationPortObj != null)
                {
                    foreach (var notificationPort in notificationPortObj)
                    {
                        if (!notificationdata.NotificationPorts.Any(l => l.PortCode == notificationPort.PortCode))
                        {
                            notificationPort.RecordStatus = "I";
                            _unitOfWork.Repository<NotificationPort>().Update(notificationPort);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }

                foreach (var notificationPort in notificationdata.NotificationPorts)
                {
                    var portObj = (from np in _unitOfWork.Repository<NotificationPort>().Query().Select()
                                   where np.NotificationTemplateCode == notificationdata.NotificationTemplateCode && np.PortCode == notificationPort.PortCode
                                   select np).FirstOrDefault<NotificationPort>();
                    if (portObj != null)
                    {
                        if (portObj.RecordStatus == "I")
                        {
                            portObj.RecordStatus = "A";
                            portObj.ObjectState = ObjectState.Modified;
                            _unitOfWork.Repository<NotificationPort>().Update(portObj);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    if (portObj == null)
                    {
                        notificationPort.RecordStatus = notificationdata.RecordStatus;
                        notificationPort.CreatedBy = notificationdata.CreatedBy;
                        notificationPort.CreatedDate = notificationdata.CreatedDate;
                        notificationPort.ModifiedBy = notificationdata.ModifiedBy;
                        notificationPort.ModifiedDate = notificationdata.ModifiedDate;
                        _unitOfWork.Repository<NotificationPort>().Insert(notificationPort);
                        _unitOfWork.SaveChanges();
                    }
                }

                notificationdata.NotificationRoles = null;
                notificationdata.NotificationPorts = null;
                notificationdata.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<NotificationTemplate>().Update(notificationdata);
                _unitOfWork.SaveChanges();

                return notificationdata.NotificationTemplateCode;
            });
        }

        public List<SubCategoryVO> GetWorkflowStatus()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SubCategoryRepository repository = new SubCategoryRepository(_unitOfWork);
                List<SubCategory> subcategories = repository.GetWorkflowStatus();
                return subcategories.MapToDto();
            });
        }

        public void Dispose()
        {
        }
    }
}
