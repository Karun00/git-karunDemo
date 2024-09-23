using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using Core.Repository;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using IPMS.Core.Repository.Exceptions;
using System.Web;
using System.Globalization;
using System.Configuration;
namespace IPMS.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILog log;
        private IAuditLogRepository _auditLogRepository;

        public AccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(AccountRepository));
            _auditLogRepository = new AuditLogRepository(_unitOfWork);
        }


        public AccountLoginModel UserLogin(string username, string password, string ipAddress)
        {
            User userexits = _unitOfWork.Repository<User>().Queryable().AsEnumerable().Where(x => x.UserName.ToUpperInvariant() == username.ToUpperInvariant()).FirstOrDefault();
            if (userexits != null)
            {
                User finduser = _unitOfWork.Repository<User>().Queryable().Where(x => x.UserID == userexits.UserID && x.RecordStatus == RecordStatus.Active && x.DormantStatus == "N").FirstOrDefault();
                if (finduser != null)
                {
                    DateTime currentDate = DateTime.Now;
                    User checkUserValidateFromDate = _unitOfWork.Repository<User>().Queryable().Where(x => x.UserID == userexits.UserID && x.RecordStatus == RecordStatus.Active && x.DormantStatus == "N" && currentDate >= x.ValidFromDate).FirstOrDefault();
                    if (checkUserValidateFromDate == null)
                    {
                        DateTime? dt2 = finduser.ValidFromDate;
                        string validDate = dt2.HasValue ? dt2.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty;
                        log.Info("Your account is valid From " + validDate);
                        AddAuditLogsForSecurity("Your account is valid From " + validDate, "Security", userexits.UserName, userexits.UserID, ipAddress);
                        throw new BusinessExceptions("Your account is valid From </br>" + validDate);
                    }
                    else
                    {

                        User checkUserValidateToDate = (from u in _unitOfWork.Repository<User>().Queryable().AsEnumerable()
                                                        where u.UserID == userexits.UserID && u.RecordStatus == RecordStatus.Active && u.DormantStatus == "N"
                                                        && Convert.ToDateTime(currentDate, CultureInfo.InvariantCulture) >= Convert.ToDateTime(u.ValidFromDate, CultureInfo.InvariantCulture) && Convert.ToDateTime(currentDate, CultureInfo.InvariantCulture) < Convert.ToDateTime(u.ValidToDate, CultureInfo.InvariantCulture).AddDays(1)
                                                        select u).FirstOrDefault();
                        if (checkUserValidateToDate == null)
                        {
                            log.Info("Your account has expired");
                            AddAuditLogsForSecurity("Your account has expired, Please Contact Administrator", "Security", userexits.UserName, userexits.UserID, ipAddress);
                            throw new BusinessExceptions("Your account has expired, Please Contact Administrator");
                        }

                    }


                    var consecutiveincorrectpwd = _unitOfWork.Repository<PortGeneralConfig>().Queryable().Where(x => x.ConfigName == ConfigName.IncorrectPWDCount).FirstOrDefault();
                    if (userexits.IncorrectLogins >= Convert.ToInt32(consecutiveincorrectpwd.ConfigValue))
                    {
                        if (userexits.LoginTime > DateTime.Now)
                        {
                            TimeSpan diff = Convert.ToDateTime(userexits.LoginTime) - DateTime.Now;
                            string msg = BusinessExceptions.InvalidCredentials + ". Please try after ";
                            msg = msg + (diff.Days > 0 ? diff.Days.ToString() + " day(s) " : "");
                            msg = msg + (diff.Hours > 0 ? diff.Hours.ToString() + " hour(s) " : "");
                            msg = msg + (diff.Minutes > 0 ? diff.Minutes.ToString() + " minute(s) " : "");
                            msg = msg + (diff.Seconds > 0 ? diff.Seconds.ToString() + " second(s) " : "");

                            log.Info(msg);
                            throw new BusinessExceptions(msg);
                        }
                        else
                        {
                            throw new BusinessExceptions(BusinessExceptions.InvalidCredentials);
                        }
                    }
                    else
                    {

                        var user = (from u in _unitOfWork.Repository<User>().Queryable().AsEnumerable()
                                where u.UserName.ToUpperInvariant() == username.ToUpperInvariant() && u.PWD == password && u.RecordStatus == "A"
                                select new AccountLoginModel
                                {
                                    UserName = u.UserName,
                                    Password = u.PWD,
                                    IsFirstTimeLogin = u.IsFirstTimeLogin,
                                    PwdExpirtyDate = u.PwdExpirtyDate,
                                    LoginTime = u.LoginTime

                                }).FirstOrDefault();
                    if (user != null)
                    {

                        finduser.ObjectState = ObjectState.Modified;
                        finduser.IncorrectLogins = 0;
                        finduser.LoginTime = DateTime.Now;
                        _unitOfWork.Repository<User>().Update(finduser);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        finduser.ObjectState = ObjectState.Modified;
                        finduser.IncorrectLogins = userexits.IncorrectLogins + 1;
                        finduser.LoginTime = DateTime.Now;
                        if (userexits.IncorrectLogins > Convert.ToInt32(consecutiveincorrectpwd.ConfigValue, CultureInfo.InvariantCulture) - 1)
                        {
                            finduser.LoginTime = DateTime.Now.AddMinutes(15);
                        }
                        _unitOfWork.Repository<User>().Update(finduser);
                        _unitOfWork.SaveChanges();

                        AddAuditLogsForSecurity(BusinessExceptions.InvalidCredentials, "Security", userexits.UserName, 1, ipAddress);

                        throw new BusinessExceptions(BusinessExceptions.InvalidCredentials);
                        //}
                    }
                    return user;
                    }

                }
                else
                {
                    log.Info("Your account is inactive");

                    AddAuditLogsForSecurity("Your account is inactive", "Userlock", userexits.UserName, 1, ipAddress);

                    throw new BusinessExceptions("Your account is inactive");
                }
            }
            else
            {
                log.Info("User Name is not exists");

                AddAuditLogsForSecurity("User Name is not exists", "Security", string.Empty, 1, ipAddress);

                throw new BusinessExceptions(BusinessExceptions.InvalidCredentials);
            }
        }

        public void AddAuditLogsForSecurity(string userFriendlyDescription,string actionName, string userName, int userId, string ipAddress)
        {
            AuditTrailConfig auditTrailConfig = new AuditTrailConfig();

            auditTrailConfig.ObjectState = ObjectState.Added;
            auditTrailConfig.ControlerName = "Account";
            auditTrailConfig.ActionName = actionName;
            auditTrailConfig.IsAuditTrailRequired = "Y";
            auditTrailConfig.UserFriendlyDescription = userFriendlyDescription;
            auditTrailConfig.RecordStatus = "A";
            auditTrailConfig.CreatedBy = userId;
            auditTrailConfig.CreatedDate = DateTime.Now;
            auditTrailConfig.ModifiedBy = userId;
            auditTrailConfig.ModifiedDate = DateTime.Now;
            auditTrailConfig.IsSecurityAuditTrail = "Y";
            AuditTrail auditTrail = new AuditTrail();
            auditTrail.EntryORExit = "ENTRY";
            auditTrail.AuditDateTime = DateTime.Now;
            auditTrail.Content = string.Empty;
            auditTrail.UserID = userId;
            auditTrail.UserName = userName;
            auditTrail.UserIPAddress = ipAddress;
            auditTrail.UserComputerName = "";
            auditTrail.RecordStatus = "A";
            auditTrail.CreatedBy = userId;
            auditTrail.CreatedDate = DateTime.Now;
            auditTrail.ModifiedBy = userId;
            auditTrail.ModifiedDate = DateTime.Now;

            _auditLogRepository.UserActivityLogging(auditTrailConfig, auditTrail);
        }


        public List<UserRole> GetUserRole(int userId)
        {
            var userrole = (from ur in _unitOfWork.Repository<UserRole>().Queryable()
                            where ur.UserID == userId && ur.RecordStatus == "A"
                            select ur).ToList<UserRole>();


            return userrole;
        }
        public Entity GetEntity(string entityCode)
        {
            var entity = _unitOfWork.Repository<Entity>().Queryable().Where(x => x.EntityCode == entityCode).FirstOrDefault<Entity>();
            return entity;
        }
        public int GetUserId(string loginName)
        {

            int cnt = 0;

            cnt = (from a in _unitOfWork.Repository<User>().Queryable().AsEnumerable()
                   where a.UserName.ToUpperInvariant() == loginName.ToUpperInvariant()
                   select a).Count();


            if (cnt > 0)
            {
                var user = (from u in _unitOfWork.Repository<User>().Queryable().AsEnumerable()
                            where u.UserName.ToUpperInvariant() == loginName.ToUpperInvariant()
                            select u).FirstOrDefault<User>();
                return user.UserID;
            }

            else
                return -1;



        }
        public bool ValidatePassword(string newPassword)
        {
            Match password = Regex.Match(newPassword, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$", RegexOptions.IgnorePatternWhitespace);
            if (password.Success)
                return true;
            else
            {
                password = Regex.Match(newPassword, @"(?=.*[A-Z])", RegexOptions.IgnorePatternWhitespace);
                if (!password.Success)
                    throw new Exception("At least one upper letter");

                password = Regex.Match(newPassword, @"(?=.*[a-z])", RegexOptions.IgnorePatternWhitespace);
                if (!password.Success)
                    throw new Exception("At least one lower letter");

                password = Regex.Match(newPassword, @"(?!.*\W)", RegexOptions.IgnorePatternWhitespace);
                if (!password.Success)
                    throw new Exception("At least one special character");

                password = Regex.Match(newPassword, @"(?=.*\d)", RegexOptions.IgnorePatternWhitespace);
                if (!password.Success)
                    throw new Exception("At least one numeric digit");
            }
            return true;
        }

        public bool ChangePassword(string password, string newPassword, string userName, int userId, string portCode, string previousPasswordsCount)
        {

            var oldpassword = (from u in _unitOfWork.Repository<User>().Query().Select()
                               where u.PWD == password && u.UserName.ToUpperInvariant() == userName.ToUpperInvariant()
                               select u).FirstOrDefault<User>();
            bool result = false;
            if (oldpassword != null)
            {
                PortGeneralConfig portGeneralConfigDtl = _unitOfWork.Repository<PortGeneralConfig>().Query().Select().Where(e => e.ConfigName == ConfigName.PasswordExpiryDays && e.GroupName == "General Configuration" && e.PortCode == portCode).FirstOrDefault();

                DateTime pwdexp = DateTime.Now;
                DateTime expiry = pwdexp.AddDays(portGeneralConfigDtl != null ? Convert.ToInt32(portGeneralConfigDtl.ConfigValue, CultureInfo.InvariantCulture) : Convert.ToInt32(default(int), CultureInfo.InvariantCulture));

                var previousPSWList = _unitOfWork.Repository<ChangePasswordLog>().Query().Select().OrderByDescending(t => t.LogTransId).Take(Convert.ToInt32(previousPasswordsCount, CultureInfo.InvariantCulture)).Where(t => t.UserID == userId && t.OldPwd.ToUpperInvariant().Trim() == newPassword.ToUpperInvariant().Trim()).ToList();
                var CurrentPSWList = _unitOfWork.Repository<User>().Query().Select().Where(t => t.UserID == userId && t.PWD.ToUpperInvariant().Trim() == newPassword.ToUpperInvariant().Trim()).ToList();


                if (previousPSWList.Count() == 0 && CurrentPSWList.Count() == 0)
                {
                    _unitOfWork.ExecuteSqlCommand("update dbo.Users SET PWD = @p0,IsFirstTimeLogin = @p1,PwdExpirtyDate = @p2, ModifiedBy = @p3,ModifiedDate = @p4 where UserID = @p5", newPassword, "N", expiry, userId, DateTime.Now, userId);

                    ChangePasswordLog changepwd = new ChangePasswordLog();
                    changepwd.ObjectState = ObjectState.Added;
                    changepwd.UserID = userId;
                    changepwd.ChangeDateTime = DateTime.Now;
                    changepwd.OldPwd = password;
                    changepwd.NewPwd = newPassword;
                    changepwd.RecordStatus = "A";
                    changepwd.CreatedBy = oldpassword.UserID;
                    changepwd.CreatedDate = DateTime.Now;
                    changepwd.ModifiedBy = userId;
                    changepwd.ModifiedDate = DateTime.Now;

                    _unitOfWork.Repository<ChangePasswordLog>().Insert(changepwd);
                    _unitOfWork.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;

                }
            }
            return result;
        }
        public IEnumerable<PortVO> GetPortsByUser(int userId)
        {
            var port = (from p in _unitOfWork.Repository<Port>().Queryable()
                        join up in _unitOfWork.Repository<UserPort>().Queryable()
                        on p.PortCode equals up.PortCode
                        where up.UserID == userId && p.RecordStatus == RecordStatus.Active
                        select p).OrderBy(x=>x.PortName).ToList();
            return port.MapToDTO();
        }
        public IEnumerable<Module> GetModulesByUser(int userId)
        {

            List<ModuleVO> moduleVo = null;
            List<Module> modules = new List<Module>();

            var submodules = (from m in _unitOfWork.Repository<Module>().Queryable()
                              join e in _unitOfWork.Repository<Entity>().Queryable()
                              on m.ModuleID equals e.ModuleID
                              join rp in _unitOfWork.Repository<RolePrivilege>().Queryable()
                              on e.EntityID equals rp.EntityID
                              join ur in _unitOfWork.Repository<UserRole>().Queryable()
                              on rp.RoleID equals ur.RoleID
                              where ur.UserID == userId && e.HasMenuItem == "Y" && ur.RecordStatus == "A" && rp.RecordStatus == "A"
                              select m).Distinct().OrderBy(x=>x.OrderNo).ToList();

            moduleVo = submodules.MapToListDto();
            submodules = moduleVo.MapToListEntity();

            List<int> list = new List<int>();
            foreach (var sm in submodules)
            {
                list.Add(Convert.ToInt32(sm.ParentModuleID, CultureInfo.InvariantCulture));
            }

            // You can convert it back to an array if you would like to
            int[] moduleid = list.ToArray();

            var module = (from m in _unitOfWork.Repository<Module>().Queryable()
                          where moduleid.Contains(m.ModuleID) && m.RecordStatus == "A"
                          select m).OrderBy(x => x.OrderNo).ToList();

            moduleVo = module.MapToListDto();
            module = moduleVo.MapToListEntity();


            List<Module> submodules1 = new List<Module>();


            foreach (var item in module)
            {
                submodules1 = (from m in _unitOfWork.Repository<Module>().Queryable()
                               join e in _unitOfWork.Repository<Entity>().Queryable()
                               on m.ModuleID equals e.ModuleID
                               join rp in _unitOfWork.Repository<RolePrivilege>().Queryable()
                               on e.EntityID equals rp.EntityID
                               join ur in _unitOfWork.Repository<UserRole>().Queryable()
                               on rp.RoleID equals ur.RoleID
                               where ur.UserID == userId && m.ParentModuleID == item.ModuleID && ur.RecordStatus == "A" && rp.RecordStatus == "A" && m.RecordStatus == "A"
                               select m).Distinct().OrderBy(x => x.OrderNo).ToList();

                moduleVo = submodules1.MapToListDto();
                submodules1 = moduleVo.MapToListEntity();


                List<EntityVO> entityVo = null;
                List<Module> submodulesm = new List<Module>();
                foreach (var sm in submodules1)
                {

                    // to get the entities of the module
                    var entity = (from e in _unitOfWork.Repository<Entity>().Queryable()
                                  join rp in _unitOfWork.Repository<RolePrivilege>().Queryable()
                                  on e.EntityID equals rp.EntityID
                                  join ur in _unitOfWork.Repository<UserRole>().Queryable()
                                  on rp.RoleID equals ur.RoleID
                                  where ur.UserID == userId && e.ModuleID == sm.ModuleID && e.HasMenuItem == "Y" && ur.RecordStatus == "A" && rp.RecordStatus == "A"
                                  select e).Distinct().OrderBy(x => x.OrderNo).ToList();

                    entityVo = entity.MapToListDto();
                    entity = entityVo.MapToListEntity();


                    submodulesm.Add(new Module()
                    {
                        ModuleID = sm.ModuleID,
                        ParentModuleID = sm.ParentModuleID,
                        ModuleName = sm.ModuleName,
                        Entities = entity

                    });

                }

                modules.Add(new Module()
                {
                    ModuleID = item.ModuleID,
                    ModuleName = item.ModuleName,
                    ParentModuleID = item.ParentModuleID,
                    Module1 = submodulesm

                });

            }

            return modules;

        }
        public IEnumerable<Role> GetRoles()
        {
            var roles = (from ur in _unitOfWork.Repository<Role>().Queryable()
                         select ur).ToList();

            return roles;
        }

        public int GetPendingTaskCount(int userId, string portCode)
        {
            var _portcode = new SqlParameter("@portcode", portCode);
            var _userid = new SqlParameter("@userid", userId);



            var pendingtaskcount = _unitOfWork.SqlQuery<int>("select count(1) [pendingtaskcount] from dbo.udf_pendingtask(@portcode, @userid)", _portcode, _userid).ToList(); ;

            return pendingtaskcount[0];

        }

        public IEnumerable<GroupedPendingTaskVO> GetPendingTask(int userId, string portCode)
        {
            var _portcode = new SqlParameter("@portcode", portCode);
            var _userid = new SqlParameter("@userid", userId);

            var dateformat = (from u in _unitOfWork.Repository<PortGeneralConfig>().Queryable()
                              where u.PortCode == portCode && u.ConfigName == ConfigName.DateFormat
                              select u.ConfigValue).FirstOrDefault();

            List<PendingTaskVO> pendingtask = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", _portcode, _userid).ToList();

            var groupedptList = (from p in pendingtask
                                 group p by new { p.EntityName, p.EntityCode, p.PageUrl, p.SubCatName, p.EntityColumns, p.RequestName } into g
                                 select new GroupedPendingTaskVO
                                 {
                                     EntityName = g.Key.EntityName,
                                     EntityCode = g.Key.EntityCode,
                                     PageUrl = g.Key.PageUrl,
                                     SubCatName = g.Key.SubCatName,
                                     EntityColumns = g.Key.EntityColumns,
                                     RequestName = g.Key.RequestName,
                                     pendingTasks = g.Select(i => new PendingTaskReferenceVO
                                     {
                                         ReferenceID = i.ReferenceID,
                                         ReferenceData = i.ReferenceData,
                                         Remarks = i.Remarks,
                                         WorkflowInstanceId = i.WorkflowInstanceId,
                                         WorkflowTaskCode = i.WorkflowTaskCode,
                                         TaskName = i.TaskName,
                                         TaskDescription = i.TaskDescription,
                                         PreviousRemarks = i.PreviousRemarks,
                                         TaskCode = i.TaskCode,
                                         APIUrl = i.APIUrl,
                                         HasRemarks = i.HasRemarks,
                                         DateTimeConfigFormat = dateformat
                                     }).ToList()
                                 }).ToList();

            return groupedptList;
        }
        public IEnumerable<Module> GetModulesTreeView(int userId)
        {

            List<ModuleVO> moduleVo = null;
            List<Module> modules = new List<Module>();
            var submodules = _unitOfWork.Repository<Module>().Queryable().OrderBy(x => x.ModuleName).Distinct().ToList();

            moduleVo = submodules.MapToListDto();
            submodules = moduleVo.MapToListEntity();

            List<int> list = new List<int>();
            foreach (var sm in submodules)
            {
                list.Add(Convert.ToInt32(sm.ParentModuleID, CultureInfo.InvariantCulture));
            }

            // You can convert it back to an array if you would like to
            int[] moduleid = list.ToArray();

            var module = (from m in _unitOfWork.Repository<Module>().Queryable()
                          where moduleid.Contains(m.ModuleID)
                          select m).OrderBy(x=>x.ModuleName).ToList();

            moduleVo = module.MapToListDto();
            module = moduleVo.MapToListEntity();


            List<Module> submodules1 = new List<Module>();


            foreach (var item in module)
            {
                submodules1 = _unitOfWork.Repository<Module>().Queryable().Where(x => x.ParentModuleID == item.ModuleID).OrderBy(x => x.ModuleName).Distinct().ToList();
                moduleVo = submodules1.MapToListDto();
                submodules1 = moduleVo.MapToListEntity();


                //   List<EntityVO> entityVo = null;
                List<Module> submodulesm = new List<Module>();
                foreach (var sm in submodules1)
                {

                    submodulesm.Add(new Module()
                    {
                        ModuleID = sm.ModuleID,
                        ParentModuleID = sm.ParentModuleID,
                        ModuleName = sm.ModuleName,
                        //  Entities = entity
                        OrderNo = sm.OrderNo,
                        RecordStatus = sm.RecordStatus



                    });

                }

                modules.Add(new Module()
                {
                    ModuleID = item.ModuleID,
                    ModuleName = item.ModuleName,
                    ParentModuleID = item.ParentModuleID,
                    Module1 = submodulesm,
                    OrderNo = item.OrderNo,
                    RecordStatus = item.RecordStatus

                });

            }

            return modules;


        }
        public IEnumerable<SystemNotificationVO> GetSystemNotifications(int userId, string portCode)
        {


            var _portcode = new SqlParameter("@portcode", portCode);
            var _userid = new SqlParameter("@userid", userId);

            var sn = _unitOfWork.SqlQuery<SystemNotificationVO>("dbo.usp_GetSystemNotifications  @userid, @portcode", _userid, _portcode).ToList();

            return sn;

        }
        public class RoleVO
        {
        }
        public string GetUserPrivilegesWithControllerName(string controllerName, string username)
        {
            

            var Controllername = new SqlParameter("@controllername", controllerName);
            var Username = new SqlParameter("@username", username.ToUpperInvariant());

                List<string> Roles = new List<string>();
                Roles = _unitOfWork.SqlQuery<string>("dbo.usp_GetRoleprivileges @controllername,@username", Controllername,Username).ToList();
            
            string RolePrivileges = "";
            string newRolePrivileges = "";
            foreach (string RP in Roles)
            {
                RolePrivileges = RP.ToString();
                if (string.IsNullOrEmpty(newRolePrivileges))
                {
                    newRolePrivileges = RolePrivileges;
                }
                else
                {
                    newRolePrivileges += "," + RolePrivileges;
                }
            }
            if (ConfigurationManager.AppSettings["VcnClosurePermission"] != null && ConfigurationManager.AppSettings["VcnClosurePermission"] != "")
            {
                string VcnClosurePermission = ConfigurationManager.AppSettings["VcnClosurePermission"] != null ? ConfigurationManager.AppSettings["VcnClosurePermission"].ToString() : null;
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(controllerName) && controllerName == "VesselCallAnchorage" && !string.IsNullOrEmpty(VcnClosurePermission))
                {
                    if(VcnClosurePermission.Contains(username))
                        newRolePrivileges += "," + "VcnClose";
                }
            }
            
            return newRolePrivileges;
        }

        private static string GetMachineNameFromIPAddress(string ipAdress)
        {
            string machineName = string.Empty;
            machineName = "";
            return machineName;
        }

    }
}
