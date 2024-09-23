using System;
using System.Collections.Generic;
using System.Linq;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using System.Globalization;
using System.Configuration;
using log4net;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                  ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserService : ServiceBase, IUserService
    {
        private INotificationPublisher _notificationpublisher;
        private IEntityRepository _entity;
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;

        private ISubCategoryRepository _subcategoryRepository;
        private IUserRepository _userRepository;
        private IAuditLogRepository _auditLogRepository;
        private static readonly ILog userLog = LogManager.GetLogger(typeof(UserService));

        public UserService(IUnitOfWork unitOfWork)
        {
            log4net.Config.XmlConfigurator.Configure();

            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _auditLogRepository = new AuditLogRepository(_unitOfWork);

            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
        }

        public UserService()
        {
            log4net.Config.XmlConfigurator.Configure();

            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);

            _userRepository = new UserRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _auditLogRepository = new AuditLogRepository(_unitOfWork);


        }
        /// <summary>
        /// This method is used for fetches the user type details
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetUserType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var UserType = (from ad in _unitOfWork.Repository<SubCategory>().Queryable()
                                where ad.SupCatCode == SuperCategoryConstants.USER_TYPE
                                select new SubCategoryVO
                                {
                                 SubCatCode   = ad.SubCatCode,
                                 SubCatName = ad.SubCatName,
                                 SupCatCode = ad.SupCatCode,
                                 RecordStatus = ad.RecordStatus,
                                 CreatedBy = ad.CreatedBy,
                                 CreatedDate = ad.CreatedDate,
                                 ModifiedBy = ad.ModifiedBy,
                                 ModifiedDate = ad.ModifiedDate
                                }).ToList();

                return UserType;
            });
        }

        public List<UserMasterVO> GetUsersListForGrid(string userType, string searchText, string darmentUser, string referenceNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var Agentlist = (from u in _unitOfWork.Repository<User>().Queryable()
                                 join a in _unitOfWork.Repository<Agent>().Queryable()
                                on u.UserTypeID equals a.AgentID
                                 join sc in _unitOfWork.Repository<SubCategory>().Queryable()
                                on u.UserType equals sc.SubCatCode
                                 join up in _unitOfWork.Repository<UserPort>().Queryable()
                                 on u.UserID equals up.UserID
                                 join p in _unitOfWork.Repository<Port>().Queryable()
                                 on up.PortCode equals p.PortCode
                                 where u.UserType == UserType.Agent && up.PortCode == _PortCode
                                 orderby u.ModifiedDate descending
                                 select new UserMasterVO
                                 {
                                     UserID = u.UserID,
                                     SubCatCode = u.UserType,
                                     SubCatName = sc.SubCatName,
                                     UserType = u.UserType,
                                     Name = a.RegisteredName,
                                     UserName = u.UserName,
                                     RecordStatus = u.RecordStatus,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     EmailID = u.EmailID,
                                     UserTypeID = u.UserTypeID,
                                     ContactNo = u.ContactNo,
                                     ReferenceNo = a.ReferenceNo,
                                     CreatedBy = u.CreatedBy,
                                     CreatedDate = u.CreatedDate,
                                     ModifiedBy = u.ModifiedBy,
                                     ModifiedDate = u.ModifiedDate,
                                     PortCode = up.PortCode,
                                     PWD = u.PWD,
                                     IsFirstTimeLogin = u.IsFirstTimeLogin,
                                     PwdExpirtyDate = u.PwdExpirtyDate,
                                     IncorrectLogins = u.IncorrectLogins,
                                     LoginTime = u.LoginTime,
                                     DormantStatus = u.DormantStatus,
                                     ReasonForAccess = u.ReasonForAccess,
                                     AnonymousUserYn = u.AnonymousUserYn,
                                     ValidFromDate = u.ValidFromDate.HasValue ? u.ValidFromDate.ToString() : string.Empty,
                                     ValidToDate = u.ValidToDate.HasValue ? u.ValidToDate.ToString() : string.Empty,
                                     PortNames = (from upm in u.UserPorts
                                                  select upm.Port.PortName
                                                ).ToList(),

                                 }).ToList();

                var TOlist = GetToUsersListForGrid();

                var Emplist = GetEmpUsersListForGrid();

                var ExternalUser = (from u in _unitOfWork.Repository<User>().Queryable()
                                    join sc in _unitOfWork.Repository<SubCategory>().Queryable()
                                   on u.UserType equals sc.SubCatCode
                                    join up in _unitOfWork.Repository<UserPort>().Queryable()
                                    on u.UserID equals up.UserID
                                    join p in _unitOfWork.Repository<Port>().Queryable()
                                    on up.PortCode equals p.PortCode
                                    where u.UserType == UserType.ExternalUser && up.PortCode == _PortCode
                                    orderby u.ModifiedDate descending
                                    select new UserMasterVO
                                    {
                                        UserID = u.UserID,
                                        SubCatCode = u.UserType,
                                        SubCatName = sc.SubCatName,
                                        UserType = u.UserType,
                                        Name = u.FirstName,
                                        UserName = u.UserName,
                                        RecordStatus = u.RecordStatus,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        EmailID = u.EmailID,
                                        UserTypeID = u.UserTypeID,
                                        ContactNo = u.ContactNo,
                                        ReferenceNo = "",
                                        CreatedBy = u.CreatedBy,
                                        CreatedDate = u.CreatedDate,
                                        ModifiedBy = u.ModifiedBy,
                                        ModifiedDate = u.ModifiedDate,
                                        PortCode = up.PortCode,
                                        PWD = u.PWD,
                                        IsFirstTimeLogin = u.IsFirstTimeLogin,
                                        PwdExpirtyDate = u.PwdExpirtyDate,
                                        IncorrectLogins = u.IncorrectLogins,
                                        AnonymousUserYn = u.AnonymousUserYn,
                                        LoginTime = u.LoginTime,
                                        DormantStatus = u.DormantStatus,
                                        ReasonForAccess = u.ReasonForAccess,
                                        ValidFromDate = u.ValidFromDate.HasValue ? u.ValidFromDate.ToString() : string.Empty,
                                        ValidToDate = u.ValidToDate.HasValue ? u.ValidToDate.ToString() : string.Empty,
                                        PortNames = (from upm in u.UserPorts
                                                     select upm.Port.PortName
                                               ).ToList(),
                                    }).ToList();

                var Userlist = Agentlist.Union(Emplist).Union(TOlist).Union(ExternalUser).ToList().OrderByDescending(r => r.UserID);

                foreach (var user in Userlist)
                {
                    List<Role> rolelist = (from r in _unitOfWork.Repository<Role>().Queryable()
                                           join ur in _unitOfWork.Repository<UserRole>().Queryable()
                                           on r.RoleID equals ur.RoleID
                                           where ur.UserID == user.UserID
                                           select r).ToList<Role>();

                    user.Roles = rolelist.MapToDto();
                    user.ValidFromDate = Convert.ToDateTime(user.ValidFromDate, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    user.ValidToDate = Convert.ToDateTime(user.ValidToDate, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                foreach (var users in Userlist)
                {
                    List<UserPort> userPorts = (from up in _unitOfWork.Repository<UserPort>().Queryable()
                                                where up.UserID == users.UserID && up.RecordStatus == RecordStatus.Active
                                                select up).ToList<UserPort>();
                    users.UserPorts = userPorts.MapToDto();
                }

                List<UserMasterVO> Result = new List<UserMasterVO>();

                Result = Userlist.ToList<UserMasterVO>().Where(v => v.DormantStatus == darmentUser && v.UserType == userType).ToList();


                if (!string.IsNullOrWhiteSpace(searchText))
                    Result = Result.FindAll(t => t.Name.ToLower(CultureInfo.InvariantCulture).Contains(searchText.ToLower(CultureInfo.InvariantCulture)));

                if (!string.IsNullOrWhiteSpace(referenceNo))
                    Result = Result.FindAll(t => t.ReferenceNo.ToUpperInvariant().Contains(referenceNo.ToUpperInvariant()));

                return Result;
            });
        }

        private List<UserMasterVO> GetEmpUsersListForGrid()
        {
            var Emplist = (from u in _unitOfWork.Repository<User>().Queryable()
                           join a in _unitOfWork.Repository<Employee>().Queryable()
                          on u.UserTypeID equals a.EmployeeID
                           join sc in _unitOfWork.Repository<SubCategory>().Queryable()
                          on u.UserType equals sc.SubCatCode
                           join up in _unitOfWork.Repository<UserPort>().Queryable()
                          on u.UserID equals up.UserID
                           join p in _unitOfWork.Repository<Port>().Queryable()
                             on up.PortCode equals p.PortCode
                           where u.UserType == UserType.Employee && up.PortCode == _PortCode
                           orderby u.ModifiedDate descending
                           select new UserMasterVO
                           {
                               UserID = u.UserID,
                               SubCatCode = u.UserType,
                               SubCatName = sc.SubCatName,
                               UserType = u.UserType,
                               Name = a.FirstName + " " + a.LastName,
                               UserName = u.UserName,
                               RecordStatus = u.RecordStatus,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               EmailID = u.EmailID,
                               UserTypeID = u.UserTypeID,
                               ContactNo = u.ContactNo,
                               Designation = a.SubCategory3.SubCatName,
                               ReferenceNo = a.SAPNumber,
                               CreatedBy = u.CreatedBy,
                               CreatedDate = u.CreatedDate,
                               ModifiedBy = u.ModifiedBy,
                               ModifiedDate = u.ModifiedDate,
                               PWD = u.PWD,
                               IsFirstTimeLogin = u.IsFirstTimeLogin,
                               PwdExpirtyDate = u.PwdExpirtyDate,
                               AnonymousUserYn = u.AnonymousUserYn,
                               IncorrectLogins = u.IncorrectLogins,
                               LoginTime = u.LoginTime,
                               DormantStatus = u.DormantStatus,
                               ReasonForAccess = u.ReasonForAccess,
                               ValidFromDate = u.ValidFromDate.HasValue ? u.ValidFromDate.ToString() : string.Empty,
                               ValidToDate = u.ValidToDate.HasValue ? u.ValidToDate.ToString() : string.Empty,
                               PortNames = (from upm in u.UserPorts
                                            select upm.Port.PortName
                                              ).ToList(),

                           }).ToList();
            return Emplist;
        }

        private List<UserMasterVO> GetToUsersListForGrid()
        {
            var TOlist = (from u in _unitOfWork.Repository<User>().Queryable()
                          join a in _unitOfWork.Repository<TerminalOperator>().Queryable()
                          on u.UserTypeID equals a.TerminalOperatorID
                          join sc in _unitOfWork.Repository<SubCategory>().Queryable()
                             on u.UserType equals sc.SubCatCode
                          join up in _unitOfWork.Repository<UserPort>().Queryable()
                           on u.UserID equals up.UserID
                          join p in _unitOfWork.Repository<Port>().Queryable()
                          on up.PortCode equals p.PortCode
                          where u.UserType == UserType.TerminalOperator && up.PortCode == _PortCode
                          orderby u.ModifiedDate descending
                          select new UserMasterVO
                          {
                              UserID = u.UserID,
                              SubCatCode = u.UserType,
                              SubCatName = sc.SubCatName,
                              UserType = u.UserType,
                              Name = a.RegisteredName,
                              UserName = u.UserName,
                              RecordStatus = u.RecordStatus,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              EmailID = u.EmailID,
                              ContactNo = u.ContactNo,
                              UserTypeID = u.UserTypeID,
                              ReferenceNo = a.RegistrationNumber,
                              CreatedBy = u.CreatedBy,
                              CreatedDate = u.CreatedDate,
                              ModifiedBy = u.ModifiedBy,
                              ModifiedDate = u.ModifiedDate,
                              AnonymousUserYn = u.AnonymousUserYn,
                              PortCode = up.PortCode,
                              PWD = u.PWD,
                              IsFirstTimeLogin = u.IsFirstTimeLogin,
                              PwdExpirtyDate = u.PwdExpirtyDate,
                              IncorrectLogins = u.IncorrectLogins,
                              LoginTime = u.LoginTime,
                              DormantStatus = u.DormantStatus,
                              ReasonForAccess = u.ReasonForAccess,
                              ValidFromDate = u.ValidFromDate.HasValue ? u.ValidFromDate.ToString() : string.Empty,
                              ValidToDate = u.ValidToDate.HasValue ? u.ValidToDate.ToString() : string.Empty,
                              PortNames = (from upm in u.UserPorts
                                           select upm.Port.PortName
                                              ).ToList(),
                          }).ToList();
            return TOlist;
        }

        /// <summary>
        /// This method is used for fetches the role details
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var Roles = _unitOfWork.Repository<Role>().Queryable().OrderBy(x => x.RoleName).ToList();
                return Roles;
            });
        }

        /// <summary>
        /// This method is used for fetches the active user details
        /// </summary>
        /// <returns></returns>
        public List<UserMasterVO> GetUsersList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var Agentlist = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                                 join a in _unitOfWork.Repository<Agent>().Query().Tracking(true).Select()
                                on u.UserTypeID equals a.AgentID
                                 join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                                on u.UserType equals sc.SubCatCode
                                 join up in _unitOfWork.Repository<UserPort>().Query().Tracking(true).Select()
                                 on u.UserID equals up.UserID
                                 where u.UserType == UserType.Agent
                                 orderby u.ModifiedDate descending
                                 select new UserMasterVO
                                 {
                                     UserID = u.UserID,
                                     SubCatCode = u.UserType,
                                     SubCatName = sc.SubCatName,
                                     UserType = u.UserType,
                                     Name = u.FirstName + " " + u.LastName,
                                     UserName = u.UserName,
                                     RecordStatus = u.RecordStatus,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     EmailID = u.EmailID,
                                     UserTypeID = u.UserTypeID,
                                     ContactNo = u.ContactNo,
                                     ReferenceNo = a.ReferenceNo,
                                     CreatedBy = u.CreatedBy,
                                     CreatedDate = u.CreatedDate,
                                     ModifiedBy = u.ModifiedBy,
                                     ModifiedDate = u.ModifiedDate,
                                     PortCode = up.PortCode,
                                     PWD = u.PWD,
                                     IsFirstTimeLogin = u.IsFirstTimeLogin,
                                     PwdExpirtyDate = u.PwdExpirtyDate,
                                     IncorrectLogins = u.IncorrectLogins,
                                     LoginTime = u.LoginTime,
                                     DormantStatus = u.DormantStatus

                                 }).ToList();

                var TOlist = GetToList();

                var Emplist = GetEmpList();

                var ExternalUser = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                                    join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                                   on u.UserType equals sc.SubCatCode
                                    join up in _unitOfWork.Repository<UserPort>().Query().Tracking(true).Select()
                                    on u.UserID equals up.UserID
                                    where u.UserType == UserType.ExternalUser
                                    orderby u.ModifiedDate descending
                                    select new UserMasterVO
                                    {
                                        UserID = u.UserID,
                                        SubCatCode = u.UserType,
                                        SubCatName = sc.SubCatName,
                                        UserType = u.UserType,
                                        Name = u.FirstName + " " + u.LastName,
                                        UserName = u.UserName,
                                        RecordStatus = u.RecordStatus,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        EmailID = u.EmailID,
                                        UserTypeID = u.UserTypeID,
                                        ContactNo = u.ContactNo,
                                        ReferenceNo = "",
                                        CreatedBy = u.CreatedBy,
                                        CreatedDate = u.CreatedDate,
                                        ModifiedBy = u.ModifiedBy,
                                        ModifiedDate = u.ModifiedDate,
                                        PortCode = up.PortCode,
                                        PWD = string.Empty,
                                        IsFirstTimeLogin = u.IsFirstTimeLogin,
                                        PwdExpirtyDate = u.PwdExpirtyDate,
                                        IncorrectLogins = u.IncorrectLogins,
                                        LoginTime = u.LoginTime,
                                        DormantStatus = u.DormantStatus

                                    }).ToList();

                var Userlist = Agentlist.Union(Emplist).Union(TOlist).Union(ExternalUser).ToList();

                foreach (var user in Userlist)
                {
                    List<Role> rolelist = (from r in _unitOfWork.Repository<Role>().Query().Tracking(true).Select()
                                           join ur in _unitOfWork.Repository<UserRole>().Query().Select()
                                           on r.RoleID equals ur.RoleID
                                           where ur.UserID == user.UserID
                                           select r).ToList<Role>();
                    user.Roles = rolelist.MapToDto();

                }

                return Userlist;
            });
        }

        private List<UserMasterVO> GetToList()
        {
            var TOlist = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                          join a in _unitOfWork.Repository<TerminalOperator>().Query().Tracking(true).Select()
                          on u.UserTypeID equals a.TerminalOperatorID
                          join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                             on u.UserType equals sc.SubCatCode
                          join up in _unitOfWork.Repository<UserPort>().Query().Tracking(true).Select()
                           on u.UserID equals up.UserID
                          where u.UserType == UserType.TerminalOperator
                          orderby u.ModifiedDate descending
                          select new UserMasterVO
                          {
                              UserID = u.UserID,
                              SubCatCode = u.UserType,
                              SubCatName = sc.SubCatName,
                              UserType = u.UserType,
                              Name = u.FirstName + " " + u.LastName,
                              UserName = u.UserName,
                              RecordStatus = u.RecordStatus,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              EmailID = u.EmailID,
                              ContactNo = u.ContactNo,
                              UserTypeID = u.UserTypeID,
                              ReferenceNo = a.RegistrationNumber,
                              CreatedBy = u.CreatedBy,
                              CreatedDate = u.CreatedDate,
                              ModifiedBy = u.ModifiedBy,
                              ModifiedDate = u.ModifiedDate,
                              PortCode = up.PortCode,
                              PWD = u.PWD,
                              IsFirstTimeLogin = u.IsFirstTimeLogin,
                              PwdExpirtyDate = u.PwdExpirtyDate,
                              IncorrectLogins = u.IncorrectLogins,
                              LoginTime = u.LoginTime,
                              DormantStatus = u.DormantStatus

                          }).ToList();
            return TOlist;
        }

        private List<UserMasterVO> GetEmpList()
        {
            var Emplist = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                           join a in _unitOfWork.Repository<Employee>().Query().Tracking(true).Select()
                          on u.UserTypeID equals a.EmployeeID
                           join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                            on u.UserType equals sc.SubCatCode
                           where u.UserType == UserType.Employee
                           orderby u.ModifiedDate descending
                           select new UserMasterVO
                           {
                               UserID = u.UserID,
                               SubCatCode = u.UserType,
                               SubCatName = sc.SubCatName,
                               UserType = u.UserType,
                               Name = u.FirstName + " " + u.LastName,
                               UserName = u.UserName,
                               RecordStatus = u.RecordStatus,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               EmailID = u.EmailID,
                               UserTypeID = u.UserTypeID,
                               ContactNo = u.ContactNo,
                               Designation = a.SubCategory3.SubCatName,
                               ReferenceNo = a.SAPNumber,
                               CreatedBy = u.CreatedBy,
                               CreatedDate = u.CreatedDate,
                               ModifiedBy = u.ModifiedBy,
                               ModifiedDate = u.ModifiedDate,
                               PWD = string.Empty,
                               IsFirstTimeLogin = u.IsFirstTimeLogin,
                               PwdExpirtyDate = u.PwdExpirtyDate,
                               IncorrectLogins = u.IncorrectLogins,
                               LoginTime = u.LoginTime,
                               DormantStatus = u.DormantStatus

                           }).ToList();
            return Emplist;
        }

        public int AddUser(User userData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                List<UserRole> userRoles = userData.UserRoles.ToList();
                userData.UserRoles = null;
                List<UserPort> userPorts = userData.UserPorts.ToList();
                userData.UserPorts = null;
                userData.ModifiedDate = DateTime.Now;
                userData.ObjectState = ObjectState.Added;
                string _PWD = Password.Generate();

                userData.PWD = Password.Encrypt(_PWD, true);
                userData.PwdExpirtyDate = DateTime.Now.AddDays(1);
                userData.IncorrectLogins = 0;
                userData.IsFirstTimeLogin = UserLogin.FirstTimeLogin;
                if (userData.AnonymousUserYn == UserLogin.LoggedIn)
                {
                    userData.CreatedBy = _UserId;
                    userData.ModifiedBy = _UserId;
                }
                else
                {
                    userData.CreatedBy = anonymousUserId;
                    userData.ModifiedBy = anonymousUserId;
                }
                userData.CreatedDate = DateTime.Now;

                userData.ModifiedDate = DateTime.Now;
                _unitOfWork.Repository<User>().Insert(userData);
                _unitOfWork.SaveChanges();
                if (userData.UserType == UserType.ExternalUser && userData.UserTypeID == 0)
                {
                    _unitOfWork.ExecuteSqlCommand("update Users SET UserTypeID = @p0 where UserID = @p1", userData.UserID, userData.UserID);
                }
                foreach (var item in userRoles)
                {
                    item.UserID = userData.UserID;

                    item.CreatedBy = userData.CreatedBy;
                    item.CreatedDate = userData.CreatedDate;
                    item.ModifiedBy = userData.ModifiedBy;
                    item.ModifiedDate = userData.ModifiedDate;

                    _unitOfWork.Repository<UserRole>().Insert(item);
                    _unitOfWork.SaveChanges();
                }

                #region User Port(s)

                userData.UserPorts = null;
                userData.UserPorts1 = null;
                userData.UserPorts2 = null;
                userData.UserPorts3 = null;
                userData.UserPorts4 = null;

                userPorts.ToList().ForEach(item => 
                {
                    item.UserID = userData.UserID;
                    item.VerifiedBy = userData.CreatedBy;
                    item.VerifiedDate = userData.CreatedDate;
                    item.ApprovedBy = userData.CreatedBy;
                    item.ApprovedDate = userData.CreatedDate;
                    item.CreatedBy = userData.CreatedBy;
                    item.CreatedDate = userData.CreatedDate;
                    item.ModifiedBy = userData.ModifiedBy;
                    item.ModifiedDate = userData.ModifiedDate;
                    item.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<UserPort>().Insert(item);
                    _unitOfWork.SaveChanges() ; 
                });
                

                #endregion

                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = userData.UserType;
                nextStepCompany.UserTypeId = (userData.UserTypeID == 0 ? userData.UserID : userData.UserTypeID);

                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.User).EntityID, userData.UserID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, "WFSA");

                #region Notification using RabbitMQ Implementation

                userLog.Debug("AddUser :: Notification Pushed to RabbitMQ for User : " + userData.UserName + ", Email : " + userData.EmailID + " On " + DateTime.Now.ToString(CultureInfo.InvariantCulture));

                #endregion
                return userData.UserID;
            });
        }

        /// <summary>
        /// This method is used for fetches the employee details
        /// </summary>
        /// <returns></returns>
        public List<UserMasterVO> GetEmployeesDetails(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<UserMasterVO> employeeslist = new List<UserMasterVO>();
                if (!string.IsNullOrWhiteSpace(_PortCode))
                {
                    var portCode = new SqlParameter("@PortCode", _PortCode);
                    var searchval = new SqlParameter("@SearchValue", searchValue);

                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        employeeslist = _unitOfWork.SqlQuery<UserMasterVO>("select  emp.EmployeeID UserTypeID,emp.FirstName, emp.LastName ,emp.FirstName +' '+ emp.LastName Name, SAPNumber ReferenceNo,PortCode,SubCatName  Designation, OfficialMobileNo ContactNo , ISNULL(emp.EmailID,u.EmailID) EmailID, ISNULL(CONVERT(varchar(30), u.UserName),'') UserName, ISNULL(u.UserID,0) UserID ,Convert(VARCHAR(12),CONVERT(DATE, u.ValidFromDate)) ValidFromDate,  Convert(VARCHAR(12), CONVERT(DATE, u.ValidToDate)) ValidToDate, u.ReasonForAccess from Employee emp inner join SubCategory sub on sub.SubCatCode = emp.Designation left join Users u on u.UserTypeID = emp.EmployeeID and  u.UserType='EMP'  where emp.RecordStatus='A'  and ((emp.SAPNumber like '%'+@SearchValue+'%'))  order by emp.FirstName ASC", searchval).ToList();
                    }
                    else
                    {
                        employeeslist = _unitOfWork.SqlQuery<UserMasterVO>("select  emp.EmployeeID UserTypeID,emp.FirstName, emp.LastName ,emp.FirstName +' '+ emp.LastName Name, SAPNumber ReferenceNo,PortCode,SubCatName  Designation, OfficialMobileNo ContactNo , ISNULL(emp.EmailID,u.EmailID) EmailID, ISNULL(CONVERT(varchar(30), u.UserName),'') UserName, ISNULL(u.UserID,0) UserID, Convert(VARCHAR(12),CONVERT(DATE, u.ValidFromDate)) ValidFromDate,  Convert(VARCHAR(12), CONVERT(DATE, u.ValidToDate)) ValidToDate , u.ReasonForAccess from Employee emp inner join SubCategory sub on sub.SubCatCode = emp.Designation left join Users u on u.UserTypeID = emp.EmployeeID and  u.UserType='EMP' where emp.RecordStatus='A'  and  emp.PortCode=@PortCode   order by emp.FirstName ASC", portCode).ToList();
                    }
                }
                else
                {
                    employeeslist = _unitOfWork.SqlQuery<UserMasterVO>("select  emp.EmployeeID UserTypeID,emp.FirstName, emp.LastName ,emp.FirstName +' '+ emp.LastName Name, SAPNumber ReferenceNo,PortCode,SubCatName  Designation, OfficialMobileNo ContactNo ,  ISNULL(emp.EmailID,u.EmailID) EmailID, ISNULL(CONVERT(varchar(30), u.UserName),'') UserName, ISNULL(u.UserID,0) UserID, Convert(VARCHAR(12),CONVERT(DATE, u.ValidFromDate)) ValidFromDate,  Convert(VARCHAR(12), CONVERT(DATE, u.ValidToDate)) ValidToDate, u.ReasonForAccess from Employee emp inner join SubCategory sub on sub.SubCatCode = emp.Designation left join Users u on u.UserTypeID = emp.EmployeeID and  u.UserType='EMP' where emp.RecordStatus='A'  order by emp.FirstName ASC").ToList();
                }
                return employeeslist;
            });
        }

        public List<UserMasterVO> GetAgentDetails(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var wfSatus = new SqlParameter("@WFStatus", SuperCategoryConstants.WRKFLOWAPPROVALS);
                var portCode = new SqlParameter("@PortCode", _PortCode);
                var searchval = new SqlParameter("@SearchValue", searchValue);

                List<UserMasterVO> Agentsist = _unitOfWork.SqlQuery<UserMasterVO>("select  a.AgentID UserTypeID  ,RegisteredName Name,ReferenceNo,PortCode,'' Designation  from Agent a inner join AgentPort ap on ap.AgentID = a.AgentID  where a.RecordStatus='A' and ap.WFStatus=@WFStatus and ap.PortCode=@PortCode and a.RegisteredName like '%'+@SearchValue+'%'", wfSatus, portCode, searchval).ToList();

                return Agentsist;
            });
        }

        /// <summary>
        /// This method is used for fetches the terminal operator details
        /// </summary>
        /// <returns></returns>
        public List<UserMasterVO> GetToDetails(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<UserMasterVO> TerminalOperatorlist = new List<UserMasterVO>();
                if (!string.IsNullOrWhiteSpace(_PortCode))
                {
                    var portCode = new SqlParameter("@PortCode", _PortCode);
                    var searchval = new SqlParameter("@SearchValue", searchValue);
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        TerminalOperatorlist = _unitOfWork.SqlQuery<UserMasterVO>("select  tope.TerminalOperatorID UserTypeID  ,RegisteredName Name,RegistrationNumber ReferenceNo, PortCode, '' Designation from TerminalOperator tope inner join TerminalOperatorPort topp on tope.TerminalOperatorID = topp.TerminalOperatorID   where tope.RecordStatus='A' and topp.PortCode=@PortCode  and tope.RegisteredName like '%'+@SearchValue+'%'", portCode, searchval).ToList();
                    }
                    else
                    {
                        TerminalOperatorlist = _unitOfWork.SqlQuery<UserMasterVO>("select  tope.TerminalOperatorID UserTypeID  ,RegisteredName Name,RegistrationNumber ReferenceNo, PortCode, '' Designation,ContactNo, EmailID,u.FirstName, u.LastName, ISNULL(CONVERT(varchar(30), u.UserName),'') UserName, ISNULL(u.UserID,0) UserID,  Convert(VARCHAR(12),CONVERT(DATE, u.ValidFromDate)) ValidFromDate,  Convert(VARCHAR(12), CONVERT(DATE, u.ValidToDate)) ValidToDate, u.ReasonForAccess   from TerminalOperator tope inner join TerminalOperatorPort topp on tope.TerminalOperatorID = topp.TerminalOperatorID  left join Users u on u.UserTypeID = topp.TerminalOperatorID and u.UserType='TO' where tope.RecordStatus='A'  and topp.PortCode=@PortCode", portCode).ToList();
                    }
                }
                else
                {
                    TerminalOperatorlist = _unitOfWork.SqlQuery<UserMasterVO>("select  tope.TerminalOperatorID UserTypeID  ,RegisteredName Name,RegistrationNumber ReferenceNo, PortCode, '' Designation,ContactNo, EmailID,u.FirstName, u.LastName, ISNULL(CONVERT(varchar(30), u.UserName),'') UserName, ISNULL(u.UserID,0) UserID ,  Convert(VARCHAR(12),CONVERT(DATE, u.ValidFromDate)) ValidFromDate,  Convert(VARCHAR(12), CONVERT(DATE, u.ValidToDate)) ValidToDate , u.ReasonForAccess  from TerminalOperator tope inner join TerminalOperatorPort topp on tope.TerminalOperatorID = topp.TerminalOperatorID  left join Users u on u.UserTypeID = topp.TerminalOperatorID and u.UserType='TO' where tope.RecordStatus='A'  ").ToList();
                }

                return TerminalOperatorlist;
            });
        }

        /// <summary>
        /// This method is used for update the data
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public int ModifyUser(User userData, string ipAddress, string machineName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                userData.ModifiedBy = _UserId;
                userData.ModifiedDate = DateTime.Now;

                List<UserRole> userRoles = userData.UserRoles.ToList();

                List<UserRole> lstuserRole = _unitOfWork.Repository<UserRole>().Queryable().Where(e => e.UserID == userData.UserID).ToList();

                List<UserPort> userPorts = userData.UserPorts.ToList();
                List<UserPort> lstuserPorts = _unitOfWork.Repository<UserPort>().Queryable().Where(e => e.UserID == userData.UserID).ToList();

                //TODO : User Port Extension Log to be maintained

                //Bug 13696 : By mahesh 11-26-2015
                var ResUser = from r in _unitOfWork.Repository<User>().Query().Select()
                              where r.UserID == userData.UserID
                              select r;
                userData.PWD = ResUser.FirstOrDefault().PWD;
                //
                #region Audit trail
                HashSet<int> addedRoleIDs = new HashSet<int>(lstuserRole.Select(s => s.RoleID));
                var addedRoles = userRoles.Where(m => !addedRoleIDs.Contains(m.RoleID)).ToList();

                var removedRoles = new List<UserRole>();
                if (addedRoles.Count == 0)
                {
                    HashSet<int> removesRoleIDs = new HashSet<int>(userRoles.Select(s => s.RoleID));
                    removedRoles = lstuserRole.Where(m => !removesRoleIDs.Contains(m.RoleID)).ToList();
                }

                var logInuserDet = (from r in _unitOfWork.Repository<User>().Query().Select()
                                    where r.UserID == _UserId
                                    select r).FirstOrDefault();

                var seleteduserDet = (from r in _unitOfWork.Repository<User>().Query().Select()
                                      where r.UserID == userData.UserID
                                      select r).FirstOrDefault();

                string newRoles = AddAddedRoles(userData, addedRoles, logInuserDet);

                string rmvRoles = AddRemovedRoles(userData, removedRoles, logInuserDet);

                if (!string.IsNullOrWhiteSpace(newRoles))
                {
                    AddAuditLogsForUserRoles(newRoles, "UserRoleAdded", logInuserDet.UserName, _UserId, ipAddress, machineName);
                }
                else if (!string.IsNullOrWhiteSpace(rmvRoles))
                {
                    AddAuditLogsForUserRoles(rmvRoles, "UserRoleRemoved", logInuserDet.UserName, _UserId, ipAddress, machineName);
                }

                var portconfiguration = (from pc in _unitOfWork.Repository<PortGeneralConfig>().Query().Select()
                                         where pc.PortCode == _PortCode && pc.GroupName == "General Configuration" && pc.ConfigName == "IncorrectPWDCount"
                                         select pc).FirstOrDefault<PortGeneralConfig>();

                if (seleteduserDet.IncorrectLogins > Convert.ToInt32(portconfiguration.ConfigValue, CultureInfo.InvariantCulture) && seleteduserDet.RecordStatus == "I")
                {
                    string modifiedBy = logInuserDet.FirstName + " " + logInuserDet.LastName != string.Empty || logInuserDet.LastName != null ? logInuserDet.LastName : string.Empty;
                    string unlockUser = string.Format(CultureInfo.InvariantCulture, "{0} {1}", userData.FirstName, userData.LastName != string.Empty || userData.LastName != null ? userData.LastName : string.Empty);
                    string parmaters = string.Empty;
                    parmaters = string.Format(CultureInfo.InvariantCulture, "{0}@{1}", unlockUser, modifiedBy);

                    AddAuditLogsForUserRoles(parmaters, "UserUnlock", logInuserDet.UserName, _UserId, ipAddress, machineName);
                }

                #endregion

                foreach (UserRole userRole in lstuserRole)
                {
                    _unitOfWork.Repository<UserRole>().Delete(userRole);
                    _unitOfWork.SaveChanges();
                }

                foreach (UserRole userRole in userRoles)
                {
                    userRole.UserID = userData.UserID;
                    userRole.CreatedBy = _UserId;
                    userRole.CreatedDate = userData.ModifiedDate;
                    userRole.ModifiedBy = _UserId;
                    userRole.ModifiedDate = userData.ModifiedDate;
                    userRole.RecordStatus = RecordStatus.Active;
                    userRole.ObjectState = ObjectState.Added;

                    _unitOfWork.Repository<UserRole>().Insert(userRole);
                    _unitOfWork.SaveChanges();
                }

                foreach (UserPort userPort in lstuserPorts)
                {
                    _unitOfWork.Repository<UserPort>().Delete(userPort);
                    _unitOfWork.SaveChanges();
                }
                userPorts.ToList().ForEach(userPort => 
                {
                    userPort.UserID = userData.UserID;
                    userPort.VerifiedBy = userData.CreatedBy;
                    userPort.VerifiedDate = userData.CreatedDate;
                    userPort.ApprovedBy = userData.CreatedBy;
                    userPort.ApprovedDate = userData.CreatedDate;
                    userPort.CreatedBy = userData.CreatedBy;
                    userPort.CreatedDate = userData.CreatedDate;
                    userPort.ModifiedBy = userData.ModifiedBy;
                    userPort.ModifiedDate = userData.ModifiedDate;
                    userPort.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<UserPort>().Insert(userPort);
                    _unitOfWork.SaveChanges();
                });
                userData.UserRoles = null;
                userData.UserPorts = null;
                userData.Roles1 = null;

                userData.IncorrectLogins = default(int);


                userData.ObjectState = ObjectState.Modified;

                if (userData.UserID == _UserId)
                {
                    _unitOfWork.ExecuteSqlCommand("update Users set RecordStatus = @p0, ContactNo = @p1, EmailID = @p2, ReasonForAccess = @p3, ValidFromDate = @p4, ValidToDate = @p5, ModifiedDate = @p6, IncorrectLogins = @p7, DormantStatus = @p8 where UserID = @p9", userData.RecordStatus, userData.ContactNo, userData.EmailID, userData.ReasonForAccess, userData.ValidFromDate, userData.ValidToDate, userData.ModifiedDate, userData.IncorrectLogins, userData.DormantStatus, _UserId);
                }
               else
                {
                    if (userData.DormantStatus == UserLogin.LoggedIn)
                    {
                        var dormantUserCount = _unitOfWork.SqlQuery<int>("select count(1) from dbo.Users where DormantStatus = 'Y' and UserID = @p0", userData.UserID).FirstOrDefault();
                        if (dormantUserCount == 1)
                        {
                            userData.LoginTime = DateTime.Now;
                        }
                    }

                    _unitOfWork.ExecuteSqlCommand("update Users set RecordStatus = @p0, ContactNo = @p1, EmailID = @p2, ReasonForAccess = @p3, ValidFromDate = @p4, ValidToDate = @p5, ModifiedDate = @p6, IncorrectLogins = @p7, DormantStatus = @p8 where UserID = @p9", userData.RecordStatus, userData.ContactNo, userData.EmailID, userData.ReasonForAccess, userData.ValidFromDate, userData.ValidToDate, userData.ModifiedDate, userData.IncorrectLogins, userData.DormantStatus, userData.UserID);
                
                }


                return userData.UserID;
            });
        }

        private string AddRemovedRoles(User userData, List<UserRole> removedRoles, User logInuserDet)
        {
            StringBuilder sb = new StringBuilder();

            if (removedRoles.Count > 0)
            {
                sb.Append(logInuserDet.UserName + "@");

                foreach (var role in removedRoles)
                {
                    string roleName = string.Empty;
                    roleName = (from r in _unitOfWork.Repository<Role>().Query().Select()
                                where r.RoleID == role.RoleID
                                select new { r.RoleName }).FirstOrDefault().RoleName + " , ";

                    sb.Append(roleName);
                }
                sb.Append("#" + userData.UserName);
            }
            return sb.ToString();
        }

        private string AddAddedRoles(User userData, List<UserRole> addedRoles, User logInuserDet)
        {
            StringBuilder sb = new StringBuilder();

            if (addedRoles.Count > 0)
            {
                sb.Append(logInuserDet.UserName + "@");

                foreach (var role in addedRoles)
                {
                    string roleName = string.Empty;
                    roleName = (from r in _unitOfWork.Repository<Role>().Query().Select()
                                where r.RoleID == role.RoleID
                                select new { r.RoleName }).FirstOrDefault().RoleName + " ,";

                    sb.Append(roleName);
                }
                sb.Append("#" + userData.UserName);
            }
            return sb.ToString();
        }

        private void AddAuditLogsForUserRoles(string parameters, string actionName, string userName, int userID, string ipaddress, string mechineName)
        {
            AuditTrailConfig auditTrailConfig = new AuditTrailConfig();

            auditTrailConfig.ObjectState = ObjectState.Added;
            auditTrailConfig.ControlerName = EntityCodes.User;
            auditTrailConfig.ActionName = actionName;
            auditTrailConfig.IsAuditTrailRequired = UserLogin.FirstTimeLogin;
            auditTrailConfig.RecordStatus = RecordStatus.Active;
            auditTrailConfig.CreatedBy = userID;
            auditTrailConfig.CreatedDate = DateTime.Now;
            auditTrailConfig.ModifiedBy = userID;
            auditTrailConfig.ModifiedDate = DateTime.Now;
            auditTrailConfig.IsSecurityAuditTrail = UserLogin.FirstTimeLogin;
            auditTrailConfig.UserFriendlyDescription = string.Empty;
            AuditTrail auditTrail = new AuditTrail();
            auditTrail.EntryORExit = EntityCodes.EntryORExit;
            auditTrail.AuditDateTime = DateTime.Now;
            auditTrail.Content = string.Empty;
            auditTrail.UserID = userID;
            auditTrail.UserName = userName;
            auditTrail.UserIPAddress = ipaddress;
            auditTrail.UserComputerName = "";
            auditTrail.Parameters = parameters;
            auditTrail.RecordStatus = RecordStatus.Active;
            auditTrail.CreatedBy = userID;
            auditTrail.CreatedDate = DateTime.Now;
            auditTrail.ModifiedBy = userID;
            auditTrail.ModifiedDate = DateTime.Now;


            _auditLogRepository.UserActivityLogging(auditTrailConfig, auditTrail);
        }

        public static string GetMachineNameFromIPAddress(string ipAdress)
        {
            string machineName = string.Empty;
            machineName = "";

            return machineName;
        }

        /// <summary>
        /// This method is used for delete the data
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public User DeleteUserById(User userData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userObj = _unitOfWork.Repository<User>().Find(userData.UserID);
                userObj.RecordStatus = RecordStatus.InActive;
                userObj.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<User>().Update(userObj);
                _unitOfWork.SaveChanges();
                return userObj;
            });
        }

        /// <summary>
        /// This method is used for fetch the usermasterreferencedata details
        /// </summary>
        /// <returns></returns>
        public UserReferenceDataVO UserMasterReferenceData()
        {
            UserReferenceDataVO VO = new UserReferenceDataVO();
            VO.ListofUserType = _subcategoryRepository.UserTypes();
            VO.ListofRole = _userRepository.RoleList();
            return VO;
        }

        /// <summary>
        /// This method is used for insert roles data
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public User AddRoles(User userData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<UserRole> userRoles = userData.UserRoles.ToList();

                foreach (var item in userRoles)
                {
                    item.UserID = userData.UserID;
                    item.CreatedBy = userData.CreatedBy;
                    item.CreatedDate = userData.CreatedDate;
                    item.ModifiedBy = userData.CreatedBy;
                    item.ModifiedDate = userData.CreatedDate;
                    item.RecordStatus = RecordStatus.Active;
                    item.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<UserRole>().Insert(item);
                    _unitOfWork.SaveChanges();
                }

                return userData;
            });
        }

        /// <summary>
        /// This method is used for insert ports data
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public User AddPorts(User userData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                User obj = new User();
                obj.UserName = userData.UserName;
                obj.FirstName = userData.FirstName;
                obj.LastName = userData.LastName;
                obj.UserType = userData.UserType;
                obj.UserTypeID = userData.UserTypeID;
                obj.ContactNo = userData.ContactNo;
                obj.EmailID = userData.EmailID;
                obj.RecordStatus = RecordStatus.Active;
                obj.AnonymousUserYn = userData.AnonymousUserYn;
                obj.PWD = Password.Generate();
                obj.PwdExpirtyDate = DateTime.Now.AddDays(1);
                obj.IncorrectLogins = 0;
                obj.IsFirstTimeLogin = UserLogin.FirstTimeLogin;
                obj.CreatedBy = anonymousUserId;
                obj.ModifiedBy = anonymousUserId; 
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedDate = DateTime.Now;
                obj.ObjectState = ObjectState.Added;

                _unitOfWork.Repository<User>().Insert(obj);
                _unitOfWork.SaveChanges();

                obj.UserPorts = userData.UserPorts;


                if (userData.AnonymousUserYn == UserLogin.FirstTimeLogin)
                {
                    #region Workflow Integration
                    string remarks = "New User Registration";

                    #region User Registration Workflow

                    UserRegistrationWorkFlow userRegistrationWorkFlow = new UserRegistrationWorkFlow(_unitOfWork, obj, remarks);
                    WorkFlowEngine<UserRegistrationWorkFlow> wf = new WorkFlowEngine<UserRegistrationWorkFlow>(_unitOfWork, "", obj.CreatedBy);

                    wf.Process(userRegistrationWorkFlow, _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.WorkflowInitialStatus));

                    var an = _unitOfWork.Repository<User>().Find(Convert.ToInt32(userRegistrationWorkFlow.ReferenceId, CultureInfo.InvariantCulture));
                    an.ObjectState = ObjectState.Modified;

                    an.WorkflowInstanceId = wf.GetWorkFlowInstance(userRegistrationWorkFlow).WorkflowInstanceId;
                    _unitOfWork.Repository<User>().Update(an);
                    _unitOfWork.SaveChanges();

                    #endregion
                    #endregion
                }

                return userData;
            });
        }

        public User AddUserRegistration(User userData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                User obj = new User();
                obj.UserName = userData.UserName;
                obj.FirstName = userData.FirstName;
                obj.LastName = userData.LastName;
                obj.UserType = userData.UserType;
                obj.UserTypeID = userData.UserTypeID;
                obj.ContactNo = userData.ContactNo;
                obj.EmailID = userData.EmailID;
                obj.RecordStatus = RecordStatus.Active;
                obj.DormantStatus = userData.DormantStatus;
                obj.AnonymousUserYn = userData.AnonymousUserYn;
                obj.PWD = " ";
                obj.IncorrectLogins = 0;
                obj.IsFirstTimeLogin = UserLogin.FirstTimeLogin;

                obj.CreatedBy = userData.CreatedBy;
                obj.ModifiedBy = userData.ModifiedBy;
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedDate = DateTime.Now;

                obj.ValidFromDate = userData.ValidFromDate;
                obj.ValidToDate = userData.ValidToDate;
                obj.ReasonForAccess = userData.ReasonForAccess;
                obj.PwdExpirtyDate = DateTime.Now.AddDays(1);

                obj.ObjectState = ObjectState.Added;

                _unitOfWork.Repository<User>().Insert(obj);
                _unitOfWork.SaveChanges();

                obj.UserPorts = userData.UserPorts;

                if (userData.AnonymousUserYn == UserLogin.FirstTimeLogin)
                {
                    #region Workflow Integration
                    string remarks = "New User Registration";

                    #region User Registration Workflow

                    UserRegistrationWorkFlow userRegistrationWorkFlow = new UserRegistrationWorkFlow(_unitOfWork, obj, remarks);
                    WorkFlowEngine<UserRegistrationWorkFlow> wf = new WorkFlowEngine<UserRegistrationWorkFlow>(_unitOfWork, "", obj.CreatedBy);

                    wf.Process(userRegistrationWorkFlow, _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.WorkflowInitialStatus));

                    var an = _unitOfWork.Repository<User>().Find(Convert.ToInt32(userRegistrationWorkFlow.ReferenceId, CultureInfo.InvariantCulture));
                    an.ObjectState = ObjectState.Modified;

                    an.WorkflowInstanceId = wf.GetWorkFlowInstance(userRegistrationWorkFlow).WorkflowInstanceId;
                    _unitOfWork.Repository<User>().Update(an);
                    _unitOfWork.SaveChanges();

                    #endregion
                    #endregion
                }

                return userData;
            });
        }

        /// <summary>
        /// This method is used for fetch the user details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(int userId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var user = _unitOfWork.Repository<User>().Queryable().Where(x => x.UserID == userId).FirstOrDefault<User>();
                return user;
            });
        }

        /// <summary>
        /// This method is used for fetch the user details
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public User GetUserByName(string userName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var user = _userRepository.GetUserByName(userName);

                return user;
            });
        }

        /// <summary>
        /// This method is used for verify user registration user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void VerifyUserRegistration(string userId, string remarks, string taskCode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var user = _userRepository.GetUserById(Convert.ToInt32(userId, CultureInfo.InvariantCulture));

                user.UserPorts = user.UserPorts.Where(c => c.UserID == Convert.ToInt32(userId, CultureInfo.InvariantCulture) && c.PortCode == _PortCode).ToList();
                UserRegistrationWorkFlow userRegistrationWorkFlow = new UserRegistrationWorkFlow(_unitOfWork, user, remarks);

                WorkFlowEngine<UserRegistrationWorkFlow> wf = new WorkFlowEngine<UserRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(userRegistrationWorkFlow, taskCode);

                _unitOfWork.ExecuteSqlCommand("update dbo.UserPort SET VerifiedBy = @p0, VerifiedDate = @p1 where UserID = @p2 and PortCode=@p3", _UserId, DateTime.Now, user.UserID, _PortCode);

            });
        }

        /// <summary>
        /// This method is used for approve user registration user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void ApproveUserRegistration(string userId, string remarks, string taskCode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var user = _userRepository.GetUserById(Convert.ToInt32(userId, CultureInfo.InvariantCulture));
                user.UserPorts = user.UserPorts.Where(c => c.UserID == Convert.ToInt32(userId, CultureInfo.InvariantCulture) && c.PortCode == _PortCode).ToList();

                UserRegistrationWorkFlow userRegistrationWorkFlow = new UserRegistrationWorkFlow(_unitOfWork, user, remarks);
                WorkFlowEngine<UserRegistrationWorkFlow> wf = new WorkFlowEngine<UserRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(userRegistrationWorkFlow, taskCode);

                #region Password Generation for Approved users
                string _PWD = Password.Generate();

                _unitOfWork.ExecuteSqlCommand("update dbo.Users SET PWD = @p0, PwdExpirtyDate = @p1, IncorrectLogins = 0,IsFirstTimeLogin='Y',DormantStatus='N',ModifiedBy=@p2,ModifiedDate=@p3  where UserID =@p4", Password.Encrypt(_PWD, true), DateTime.Now.AddDays(1), _UserId, DateTime.Now, user.UserID);
                _unitOfWork.ExecuteSqlCommand("update dbo.UserPort SET WFStatus = @p0, ApprovedBy = @p1, ApprovedDate = @p2 where UserID = @p3 and PortCode=@p4", WFStatus.Approved, _UserId, DateTime.Now, user.UserID, _PortCode);

                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = user.UserType;
                nextStepCompany.UserTypeId = user.UserTypeID;

                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.User).EntityID, userId, _UserId, nextStepCompany, _PortCode, taskCode);
                #endregion

                #region Notification using RabbitMQ Implementation


                userLog.Debug("ApproveUserRegistration :: Notification Pushed to RabbitMQ for User : " + user.UserName + ", Email : " + user.EmailID + " on " + DateTime.Now.ToString(CultureInfo.InvariantCulture));

                #endregion

            });
        }

        /// <summary>
        /// This method is used for reject user registration user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void RejectUserRegistration(string userId, string remarks, string taskCode)
        { 
            EncloseTransactionAndHandleException(() =>
            {
                var user = _userRepository.GetUserById(Convert.ToInt32(userId, CultureInfo.InvariantCulture));
                user.UserPorts = user.UserPorts.Where(c => c.UserID == Convert.ToInt32(userId, CultureInfo.InvariantCulture) && c.PortCode == _PortCode).ToList();
                UserRegistrationWorkFlow userRegistrationWorkFlow = new UserRegistrationWorkFlow(_unitOfWork, user, remarks);
                WorkFlowEngine<UserRegistrationWorkFlow> wf = new WorkFlowEngine<UserRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(userRegistrationWorkFlow, taskCode);

            });
        }

        public UserMasterVO GetUserDetailsById()
        {
            return ExecuteFaultHandledOperation(() =>
            {

                var userdetails = _userRepository.GetUserDetailsById(_UserId);
                return userdetails;
            });
        }

        public UserMasterVO GetUserDetailsByIDView(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                var userdetails = _userRepository.GetUserDetailsByIDView(id);
                return userdetails;
            });
        }

        /// <summary>
        /// Purpose : To get Workmen service type user details
        /// </summary>
        /// <returns></returns>
        public List<UserMasterVO> GetUsersByWorkmenServiceTypeUsers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _userRepository.GetUsersByWorkmenServiceTypeUsers();
            });
        }

        public UserMasterVO ResetUserPassword(int userid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string newpwd = Password.Encrypt(Password.Generate(), true);

                UserMasterVO _uservo = _userRepository.ResetUserPassword(userid, newpwd, _UserId);

                #region Sending Email for Reset Password to the user
                var _user = _userRepository.GetUserById(userid);
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = _user.UserType;
                nextStepCompany.UserTypeId = _user.UserTypeID;
                _uservo.PWD = Password.Decrypt(newpwd, true);
                var _ApprovedCode = _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.ApprovedCode);

                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.User).EntityID, _user.UserID.ToString(CultureInfo.InvariantCulture), userid, nextStepCompany, _PortCode, _ApprovedCode);

                #endregion

                #region Notification using RabbitMQ Implementation

                // paasowrdchange and resetpassword  Report purpose satrt Anusha 28-05-2024

                try
                {
                    ResetPasswordLogs resetlog = new ResetPasswordLogs();
                    //ResetpasswordlogVO logsVo = new ResetpasswordlogVO();
                    if (_UserId > 1)

                    {
                        resetlog.AuditFlag = AuditPWDFlag.ResetPassword;
                    }
                    else
                    {
                        resetlog.AuditFlag = AuditPWDFlag.ForgotPassword;
                    }
                    resetlog.UserID = _uservo.UserID;
                    resetlog.UserName = _uservo.UserName;
                    resetlog.CreatedBy = Convert.ToInt32(_uservo.CreatedBy).ToString();
                    resetlog.CreatedDate = DateTime.Now;
                    resetlog.ModifiedBy = Convert.ToInt32(_uservo.ModifiedBy).ToString();
                    resetlog.ModifiedDate = DateTime.Now;
                    resetlog.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<ResetPasswordLogs>().Insert(resetlog);
                    _unitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("An error occurred: " + ex.Message);
                }


                //paasowrdchange and resetpassword Report purpose end Anusha 28 - 05 - 2024












                #endregion
                return _uservo;
            });
        }

        public int CheckUserExists(string UserTypeID, string UserName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<User> result = new List<User>();


                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    result = _unitOfWork.Repository<User>().Queryable().AsEnumerable().Where(x => x.UserName.ToUpperInvariant().Trim(' ') == UserName.ToUpperInvariant()).ToList();
                }

                return result.Count;
            });
        }





        //Anusha

        public List<EmployeeMasterDetails> GetEmployeesListFetching(string PortCode, string ReferenceNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _userRepository.GetEmployeesListFetching(PortCode, ReferenceNo, _PortCode);
            });
        }

        //Anusha

        public List<AgentDetailsVO> GetAgentListDetailsFetch(string PortCode, string ReferenceNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _userRepository.GetAgentListDetailsFetch(PortCode, ReferenceNo, _PortCode);
            });
        }


        //Anusha
        public List<TerminalOperatorVO> GetTerminalOperatorListDetailsFetch(string PortCode, string ReferenceNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _userRepository.GetTerminalOperatorListDetailsFetch(PortCode, ReferenceNo, _PortCode);
            });
        }












        public IEnumerable<PortVO> GetAllPortsDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IEnumerable<PortVO> ports = null;
                ports = _userRepository.GetAllPortsInfo();
                return ports;
            });
        }
    }
}
