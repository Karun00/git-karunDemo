using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using log4net;
using log4net.Config;
using IPMS.Domain;
using IPMS.Core.Repository.Exceptions;
using System.Data.SqlClient;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Data.Entity;

namespace IPMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILog log;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(NotificationRepository));
        }

        #region GetUsersForRoleAndPortCode
        public List<User> GetUsersForRoleAndPortCode(string portCode, List<NotificationRole> role)
        {
            List<int> _roleids = new List<int>();
            _roleids = role.Select(c => c.RoleID).ToList();


            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         join ur in _unitOfWork.Repository<UserRole>().Query().Select() on u.UserID equals ur.UserID
                         join up in _unitOfWork.Repository<UserPort>().Query().Select() on u.UserID equals up.UserID
                         where _roleids.Contains(ur.RoleID) && up.PortCode == portCode
                         group u by new { u.UserID, u.UserName, u.EmailID } into gu
                         select new User
                         {
                             UserID = gu.Key.UserID,
                             UserName = gu.Key.UserName,
                             EmailID = gu.Key.EmailID
                         }).ToList();
            return users;


        }
        #endregion

        #region GetUsersForRoleAndPortCodeByUserType
        public List<User> GetUsersForRoleAndPortCodeByUserType(string portCode, List<NotificationRole> role, string userType, int userTypeId)
        {
            List<int> _roleids = new List<int>();
            _roleids = role.Select(c => c.RoleID).ToList();


            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         join ur in _unitOfWork.Repository<UserRole>().Query().Select() on u.UserID equals ur.UserID
                         join up in _unitOfWork.Repository<UserPort>().Query().Select() on u.UserID equals up.UserID
                         where _roleids.Contains(ur.RoleID)
                         && up.PortCode == portCode
                         && u.RecordStatus == "A" && ur.RecordStatus == "A" && up.RecordStatus == "A"
                         select u).ToList();
            return users;

        }
        #endregion

        #region GetUserDetailsForRoleAndPortCodeByUserType
        public List<User> GetUserDetailsForRoleAndPortCodeByUserType(string portCode, string userType, string roleCode, int userTypeId)
        {
            var result = new List<User>();
            var _portcode = new SqlParameter("@p_PortCode", portCode);
            var _userType = new SqlParameter("@p_userType", userType);
            var _rolecode = new SqlParameter("@p_RoleCode", roleCode);
            var _userTypeID = new SqlParameter("@p_UserTypeID", userTypeId);
            log.Info(string.Format("dbo.usp_GetUsersForRoleAndPortCodeByUserType  PortCode={0}, UserType={1}, RoleCode={2}, UserTypeID={3}", portCode, userType, roleCode, userTypeId));

            result = _unitOfWork.SqlQuery<User>("dbo.usp_GetUsersForRoleAndPortCodeByUserType  @p_PortCode, @p_userType, @p_RoleCode, @p_UserTypeID", _portcode, _userType, _rolecode, _userTypeID).ToList();
            return result;

        }
        #endregion

        #region GetEmployee
        public Employee GetEmployee(int id)
        {
            var employee = (from e in _unitOfWork.Repository<Employee>().Query().Select()
                            where e.EmployeeID == id
                            select e).FirstOrDefault<Employee>();
            return employee;

        }
        #endregion

        #region RoleList
        public List<Role> RoleList()
        {
            var roleList = (from u in _unitOfWork.Repository<Role>().Query().Select()

                            select u).ToList();
            return roleList;


        }
        #endregion

        #region GetNames
        public Employee GetNames(string name, string userType)
        {

            var Names = (from u in _unitOfWork.Repository<Employee>().Query().Select().Where(e => e.FirstName.StartsWith(name))
                         select u).SingleOrDefault<Employee>();

            return Names;
        }
        #endregion

        #region GetUserIdByLoginname
        public int GetUserIdByLoginname(string loginname)
        {
            int _UserId = 0;
            if (_unitOfWork != null && !string.IsNullOrEmpty(loginname))
            {
                var user = (from u in _unitOfWork.Repository<User>().Queryable().AsEnumerable()
                            where u.UserName.ToUpperInvariant() == loginname.ToUpperInvariant()
                            select u).FirstOrDefault<User>();
                if (user != null)
                    _UserId = user.UserID;
            }

            return _UserId;
        }
        #endregion

        #region GetUserType
        public string GetUserType(string loginname)
        {
            string _UserType = "";
            if (_unitOfWork != null && !string.IsNullOrEmpty(loginname))
            {
                var user = (from u in _unitOfWork.Repository<User>().Queryable().AsEnumerable()
                            where u.UserName.ToUpperInvariant() == loginname.ToUpperInvariant()
                            select u).FirstOrDefault<User>();
                _UserType = user.UserType;
            }

            return _UserType;
        }
        #endregion

        #region GetUserByName
        /// <summary>
        /// This method is used for fetch the user details
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public User GetUserByName(string userName)
        {

            var user = (from u in _unitOfWork.Repository<User>().Queryable().AsEnumerable()
                        where u.UserName.ToUpperInvariant() == userName.ToUpperInvariant()
                        select u).FirstOrDefault<User>();


            if (user != null)
            {
                return user;
            }
            else
            {
                log.Info("Forgot Password : User Name is not exists");
                throw new BusinessExceptions(BusinessExceptions.InvalidUserName);

            }

        }
        #endregion

        #region GetUserByID
        public UserMasterVO GetUserByID(int userId)
        {

            if (userId == 0)
                throw new BusinessExceptions(BusinessExceptions.NotAuthorizedUser);

            var user = (from u in _unitOfWork.Repository<User>().Queryable()
                        where u.UserID == userId
                        select new UserMasterVO
                        {
                            UserID = u.UserID,
                            UserType = u.UserType,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            UserTypeID = u.UserTypeID,
                            UserName = u.UserName,
                            ContactNo = u.ContactNo,
                            EmailID = u.EmailID,
                            PWD = u.PWD,
                            IsFirstTimeLogin = u.IsFirstTimeLogin,
                            IncorrectLogins = u.IncorrectLogins,
                            LoginTime = u.LoginTime,
                            PwdExpirtyDate = u.PwdExpirtyDate
                        }).FirstOrDefault<UserMasterVO>();

            return user;

        }

        public User GetUserById(int userid)
        {
            var user = (from u in _unitOfWork.Repository<User>().Queryable().Where(u => u.UserID == userid)
                        join up in _unitOfWork.Repository<UserPort>().Queryable() on u.UserID equals up.UserID
                        select u).FirstOrDefault<User>();
            return user;

        }
        #endregion

        public List<UserMasterVO> GetUserDetailByID(int userId)
        {

            if (userId == 0)
                throw new BusinessExceptions(BusinessExceptions.NotAuthorizedUser);

            var __userType = (from u in _unitOfWork.Repository<User>().Queryable()
                              where u.UserID == userId
                              select new UserMasterVO
                              {
                                  UserType = u.UserType
                              });

            if (__userType.First().UserType.Trim().ToUpper(CultureInfo.InvariantCulture) == "EMP")
            {
                var user = (from u in _unitOfWork.Repository<User>().Queryable()
                            join a in _unitOfWork.Repository<Employee>().Queryable()
                            on u.UserTypeID equals a.EmployeeID
                            where u.UserType == "EMP" && u.UserID == userId
                            select new UserMasterVO
                            {
                                UserID = u.UserID,
                                UserType = u.UserType,
                                Name = u.FirstName + " " + u.LastName,
                                UserName = u.FirstName + " " + u.LastName,
                                RecordStatus = u.RecordStatus,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                EmailID = u.EmailID,
                                UserTypeID = u.UserTypeID,
                                ContactNo = u.ContactNo,
                                CreatedBy = u.CreatedBy,
                                CreatedDate = u.CreatedDate,
                                ModifiedBy = u.ModifiedBy,
                                ModifiedDate = u.ModifiedDate,
                                ArrivalCreatedAgent = "",
                            }).ToList<UserMasterVO>();

                return user;

            }

            else if (__userType.First().UserType.Trim().ToUpper(CultureInfo.InvariantCulture) == "AGNT")
            {
                var Agentlist = (from u in _unitOfWork.Repository<User>().Queryable()
                                 join a in _unitOfWork.Repository<Agent>().Queryable()
                                     on u.UserTypeID equals a.AgentID
                                 where u.UserType == "AGNT" && u.UserID == userId
                                 select new UserMasterVO
                                 {
                                     UserID = u.UserID,
                                     UserType = u.UserType,
                                     Name = u.FirstName + " " + u.LastName,
                                     UserName = u.FirstName + " " + u.LastName,
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
                                     ArrivalCreatedAgent = a.RegisteredName + " - " + a.RegistrationNumber,
                                 }).OrderByDescending(x=>x.ModifiedDate).ToList<UserMasterVO>();

                return Agentlist;
            }
            else
            {
                var TOlist = (from u in _unitOfWork.Repository<User>().Queryable()
                              join a in _unitOfWork.Repository<TerminalOperator>().Queryable()
                                  on u.UserTypeID equals a.TerminalOperatorID
                              where u.UserType == "TO" && u.UserID == userId
                              select new UserMasterVO
                              {
                                  UserID = u.UserID,
                                  UserType = u.UserType,
                                  Name = u.FirstName + " " + u.LastName,
                                  UserName = u.FirstName + " " + u.LastName,
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
                                  WorkflowInstanceId = u.WorkflowInstanceId,
                                  ReasonForAccess = u.ReasonForAccess,
                                  ArrivalCreatedAgent = "",
                              }).OrderByDescending(x=>x.ModifiedDate).ToList<UserMasterVO>();

                return TOlist;



            }
        }

        #region GetAgent
        public AgentDetailsVO GetAgent(int AgentId)
        {
            var agent = (from a in _unitOfWork.Repository<Agent>().Query().Select()
                         join ac in _unitOfWork.Repository<AuthorizedContactPerson>().Query().Select()
                         on a.AuthorizedContactPersonID equals ac.AuthorizedContactPersonID
                         where a.AgentID == AgentId
                         select new AgentDetailsVO
                         {
                             AgentID = a.AgentID,
                             RegisteredName = a.RegisteredName,
                             TelephoneNo1 = a.TelephoneNo1,
                             FaxNo = a.FaxNo,
                             FirstName = ac.FirstName,
                             SurName = ac.SurName,
                             EmailID = ac.EmailID,
                             CellularNo = ac.CellularNo
                         }).FirstOrDefault<AgentDetailsVO>();
            return agent;
        }
        #endregion

        #region GetUserDetailsByID
        /// <summary>
        /// Purpose     : Getting Employee details like Name and designation Only USERType Of "EMP"
        /// Created For : This method is required in Hot Work Permit Service Under Supplementary Service Request
        /// Created By  : Omprakash K On 11th Sep 2014
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserMasterVO GetUserDetailsById(int userId)
        {
            var __userType = (from u in _unitOfWork.Repository<User>().Query().Select()
                              where u.UserID == userId
                              select new UserMasterVO
                              {
                                  UserType = u.UserType
                              });
            if (__userType.First().UserType.Trim().ToUpper(CultureInfo.InvariantCulture) == "EMP")
            {

                var user = (from u in _unitOfWork.Repository<User>().Query().Select()
                            join emp in _unitOfWork.Repository<Employee>().Query().Select() on u.UserTypeID equals emp.EmployeeID
                            join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on emp.Designation equals sc.SubCatCode

                            where u.UserID == userId
                            select new UserMasterVO
                            {
                                UserID = u.UserID,
                                UserType = u.UserType,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                UserTypeID = u.UserTypeID,
                                UserName = u.UserName,
                                ContactNo = u.ContactNo,
                                EmailID = u.EmailID,
                                Designation = sc.SubCatName
                            }).FirstOrDefault<UserMasterVO>();
                return user;
            }
            else
            {
                UserMasterVO __UserMasterVO = new UserMasterVO();
                return __UserMasterVO;
            }
        }
        #endregion

        #region GetUserDetailsByIDView
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserMasterVO GetUserDetailsByIDView(int id)
        {

            var __userType = (from u in _unitOfWork.Repository<User>().Query().Select()
                              where u.UserID == id
                              select new UserMasterVO
                              {
                                  UserType = u.UserType
                              });
            List<Role> rolelist = (from r in _unitOfWork.Repository<Role>().Query().Select()
                                   join ur in _unitOfWork.Repository<UserRole>().Query().Select()
                                   on r.RoleID equals ur.RoleID
                                   where ur.UserID == id && ur.RecordStatus == "A"
                                   select r).ToList<Role>();

            if (__userType.First().UserType.Trim().ToUpper(CultureInfo.InvariantCulture) == "EMP")
            {
                var user = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                            join a in _unitOfWork.Repository<Employee>().Query().Tracking(true).Select()
                           on u.UserTypeID equals a.EmployeeID
                            join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                             on u.UserType equals sc.SubCatCode
                            join up in _unitOfWork.Repository<UserPort>().Query().Tracking(true).Select()
                            on u.UserID equals up.UserID
                            join p in _unitOfWork.Repository<Port>().Query().Tracking(true).Select()
                            on up.PortCode equals p.PortCode
                            join ws in _unitOfWork.Repository<WorkflowInstance>().Query().Tracking(true).Select()
                            on u.WorkflowInstanceId equals ws.WorkflowInstanceId
                            where u.UserType == "EMP" && u.RecordStatus == "A" && u.UserID == id
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
                                WorkflowInstanceId = u.WorkflowInstanceId,
                                ReasonForAccess = u.ReasonForAccess,
                                ValidFromDate = u.ValidFromDate.HasValue ? u.ValidFromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty,// Convert.ToString(u.ValidFromDate),
                                ValidToDate = u.ValidToDate.HasValue ? u.ValidToDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty//Convert.ToString(u.ValidToDate)
                                 ,  PortNames = u.UserPorts != null ? u.UserPorts.Select(g => g.Port.PortName).ToList() : null

                            }).FirstOrDefault<UserMasterVO>();
                user.Roles = rolelist.MapToDto();
                return user;

            }

            else if (__userType.First().UserType.Trim().ToUpper(CultureInfo.InvariantCulture) == "AGNT")
            {
                var Agentlist = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                                 join a in _unitOfWork.Repository<Agent>().Query().Tracking(true).Select()
                                on u.UserTypeID equals a.AgentID
                                 join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                                on u.UserType equals sc.SubCatCode
                                 join up in _unitOfWork.Repository<UserPort>().Query().Tracking(true).Select()
                                 on u.UserID equals up.UserID
                                 join p in _unitOfWork.Repository<Port>().Query().Tracking(true).Select()
                                 on up.PortCode equals p.PortCode
                                 join ws in _unitOfWork.Repository<WorkflowInstance>().Query().Tracking(true).Select()
                                 on u.WorkflowInstanceId equals ws.WorkflowInstanceId
                                 where u.UserType == "AGNT" && u.RecordStatus == "A" && u.UserID == id
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
                                     WorkflowInstanceId = u.WorkflowInstanceId,
                                     ReasonForAccess = u.ReasonForAccess,
                                     ValidFromDate = u.ValidFromDate.HasValue ? u.ValidFromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty,// Convert.ToString(u.ValidFromDate),
                                     ValidToDate = u.ValidToDate.HasValue ? u.ValidToDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty//Convert.ToString(u.ValidToDate)
                                      , PortNames = u.UserPorts != null ? u.UserPorts.Select(g => g.Port.PortName).ToList() : null
                                 }).FirstOrDefault<UserMasterVO>();
                Agentlist.Roles = rolelist.MapToDto();
                return Agentlist;
            }
            else
            {
                var TOlist = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                              join a in _unitOfWork.Repository<TerminalOperator>().Query().Tracking(true).Select()
                              on u.UserTypeID equals a.TerminalOperatorID
                              join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                               on u.UserType equals sc.SubCatCode
                              join up in _unitOfWork.Repository<UserPort>().Query().Tracking(true).Select()
                              on u.UserID equals up.UserID
                              join p in _unitOfWork.Repository<Port>().Query().Tracking(true).Select()
                              on up.PortCode equals p.PortCode
                              join ws in _unitOfWork.Repository<WorkflowInstance>().Query().Tracking(true).Select()
                              on u.WorkflowInstanceId equals ws.WorkflowInstanceId
                              where u.UserType == "TO" && u.RecordStatus == "A" && u.UserID == id
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
                                  WorkflowInstanceId = u.WorkflowInstanceId,
                                  ReasonForAccess = u.ReasonForAccess,
                                  ValidFromDate = u.ValidFromDate.HasValue ? u.ValidFromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty,// Convert.ToString(u.ValidFromDate),
                                  ValidToDate = u.ValidToDate.HasValue ? u.ValidToDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty//Convert.ToString(u.ValidToDate)
                                  , PortNames = u.UserPorts != null ? u.UserPorts.Select(g => g.Port.PortName).ToList() : null
                              }).FirstOrDefault<UserMasterVO>();
                TOlist.Roles = rolelist.MapToDto();
                return TOlist;
            }




        }
        #endregion

        #region GetUsersByWorkmenServiceTypeUsers
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get Workmen service type user details
        /// </summary>
        /// <returns></returns>
        public List<UserMasterVO> GetUsersByWorkmenServiceTypeUsers()
        {
            var workmenServiceTypeUsers = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                                           join e in _unitOfWork.Repository<Employee>().Query().Tracking(true).Select()
                                           on u.UserTypeID equals e.EmployeeID
                                           where u.UserType == "EMP" && e.Department == "DEP2"
                                           select new UserMasterVO
                                           {
                                               UserID = u.UserID,
                                               UserTypeID = u.UserTypeID,
                                               UserType = u.UserType,
                                               FirstName = u.FirstName,
                                               LastName = u.LastName,
                                               UserName = u.UserName,
                                               Name = u.FirstName + " " + u.LastName
                                           }).ToList();

            return workmenServiceTypeUsers;

        }
        #endregion

        #region GetRoles
        public List<RoleVO> GetRoles()
        {
            var Roles = _unitOfWork.Repository<Role>().Queryable().OrderBy(x => x.RoleName).ToList();
            return Roles.MapToDto();
        }
        #endregion

        #region ResetUserPassword
        public UserMasterVO ResetUserPassword(int userid, string newpwd, int _userid)
        {
            var finduser = _unitOfWork.Repository<User>().Queryable().Where(x => x.UserID == userid).FirstOrDefault();
            finduser.ObjectState = ObjectState.Modified;
            finduser.PWD = newpwd;
            finduser.PwdExpirtyDate = DateTime.Now.AddDays(1);
            finduser.IncorrectLogins = 0;
            finduser.IsFirstTimeLogin = "Y";
            int intuserid;
            if (_userid == 0)
            {
                intuserid = 1;
            }
            else
            {
                intuserid = _userid;
            }
            //finduser.ModifiedBy = intuserid;
            finduser.ModifiedDate = DateTime.Now;
            _unitOfWork.Repository<User>().Update(finduser);
            _unitOfWork.SaveChanges();

            return finduser.MapToDTO();

        }
        #endregion

        #region GetUserByUserID
        public UserMasterVO GetUserByUserID(int userid)
        {


            var user = (from u in _unitOfWork.Repository<User>().Queryable().Where(u=>u.UserID.Equals(userid))
                        select new UserMasterVO
                        {
                            UserID = u.UserID,
                            UserType = u.UserType,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            UserTypeID = u.UserTypeID,
                            UserName = u.UserName,
                            ContactNo = u.ContactNo,
                            EmailID = u.EmailID,
                            PWD = u.PWD,
                            PwdExpirtyDate = u.PwdExpirtyDate

                        }).FirstOrDefault<UserMasterVO>();

            return user;

        }
        #endregion

        #region GetUserDetails for CompanyVO
        /// <summary>
        /// To Get User Details by UserId
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CompanyVO GetUserDetails(int UserID)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Tracking(true).Select()
                         where u.UserID == UserID
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }
        #endregion






        //Anushasapnumber


        public List<EmployeeMasterDetails> GetEmployeesListFetching(string PortCode, string ReferenceNo, string portCode)
        {
            var employeeslist = (from e in _unitOfWork.Repository<Employee>().Queryable()
                                 join de in _unitOfWork.Repository<SubCategory>().Queryable()
                                 on e.Department equals de.SubCatCode
                                 join des in _unitOfWork.Repository<SubCategory>().Queryable()
                                  on e.Designation equals des.SubCatCode
                                 join bu in _unitOfWork.Repository<Port>().Queryable()
                                on e.PortCode equals bu.PortCode
                                 join cc in _unitOfWork.Repository<SubCategory>().Queryable()
                                  on e.CostCenter equals cc.SubCatCode
                                 join pa in _unitOfWork.Repository<SubCategory>().Queryable()
                                on e.PayrollArea equals pa.SubCatCode
                                 join ps in _unitOfWork.Repository<SubCategory>().Queryable()
                                on e.PSGroup equals ps.SubCatCode
                                 join psa in _unitOfWork.Repository<SubCategory>().Queryable()
                                 on e.PersonalSubArea equals psa.SubCatCode
                                 join ou in _unitOfWork.Repository<SubCategory>().Queryable()
                                 on e.OrganizationalUnit equals ou.SubCatCode
                                 join gen in _unitOfWork.Repository<SubCategory>().Queryable()
                                on e.Gender equals gen.SubCatCode
                                 where e.SAPNumber == ReferenceNo
                                 //-------------------------Anusha----------------------------------


                                 //-------------------------Anusha----------------------------------
                                 //where e.PortCode == portCode Commented by sandeep on 14-09-2015
                                 select new EmployeeMasterDetails
                                 {
                                     EmployeeID = e.EmployeeID,
                                     SAPNumber = e.SAPNumber,
                                     FirstName = e.FirstName,
                                     LastName = e.LastName,
                                     Initials = e.Initials,
                                     Name = e.FirstName + " " + e.LastName + " " + e.Initials,
                                     BirthDate = e.BirthDate,
                                     Age = e.Age,
                                     JoiningDate = e.JoiningDate,
                                     YearsofService = e.YearsofService,
                                     OfficialMobileNo = e.OfficialMobileNo,
                                     PersonalMobileNo = e.PersonalMobileNo,
                                     EmailID = e.EmailID,
                                     IDNo = e.IDNo,
                                     GenderCode = gen.SubCatCode,
                                     Gender = gen.SubCatName,
                                     DepartmentCode = e.Department,
                                     DepartmentName = de.SubCatName,
                                     DesignationCode = e.Designation,
                                     DesignationName = des.SubCatName,
                                     BusinessUnitCode = e.PortCode, // e.BusinessUnit,
                                     BusinessUnitName = bu.PortName, //bu.SubCatName,
                                     CostCenterCode = e.CostCenter,
                                     CostCenterName = cc.SubCatName,
                                     PayrollAreaCode = e.PayrollArea,
                                     PayrollAreaName = pa.SubCatName,
                                     PSGroupCode = e.PSGroup,
                                     PSGroupName = ps.SubCatName,
                                     PersonalSubAreaCode = e.PersonalSubArea,
                                     PersonalSubAreaName = psa.SubCatName,
                                     OrganizationalUnitCode = e.OrganizationalUnit,
                                     OrganizationalUnitName = ou.SubCatName,
                                     RecordStatus = e.RecordStatus,
                                     PortCode = e.PortCode,
                                     DeadWeightTonnage = e.DeadWeightTonnage
                                 }).OrderByDescending(x => x.EmployeeID).ToList();

            //var Result = GetEmployeeMasterDetailses(designation, searchText, employeeslist);

            return employeeslist;
        }



        public List<AgentDetailsVO> GetAgentListDetailsFetch(string PortCode, string ReferenceNo, string portCode)
        {
            var agent = (from a in _unitOfWork.Repository<Agent>().Query().Select()
                         join ac in _unitOfWork.Repository<AuthorizedContactPerson>().Query().Select()
                         on a.AuthorizedContactPersonID equals ac.AuthorizedContactPersonID
                         where a.ReferenceNo == ReferenceNo
                         select new AgentDetailsVO
                         {
                             AgentID = a.AgentID,
                             RegisteredName = a.RegisteredName,
                             TelephoneNo1 = a.TelephoneNo1,
                             FaxNo = a.FaxNo,
                             FirstName = ac.FirstName,
                             SurName = ac.SurName,
                             EmailID = ac.EmailID,
                             CellularNo = ac.CellularNo,
                             TradingName = a.TradingName,
                             ReferenceNo = a.ReferenceNo,
                             RegistrationNumber = a.RegistrationNumber,
                             VATNumber = a.VATNumber,
                             IncomeTaxNumber = a.IncomeTaxNumber,
                             // Uncomment if needed
                             // FromDate = a.FromDate,
                             // ToDate = a.ToDate
                         }).OrderByDescending(x => x.AgentID);

            return agent.ToList();
        }






        public List<TerminalOperatorVO> GetTerminalOperatorListDetailsFetch(string PortCode, string ReferenceNo, string portCode)
        {



            // Start with the query including the necessary navigation properties
            var query = (from t in _unitOfWork.Repository<TerminalOperator>().Queryable()
                         where t.RegistrationNumber == ReferenceNo
                         select new TerminalOperatorVO
                         {
                             RegisteredName = t.RegisteredName,
                             TradingName = t.TradingName,
                         });

            // Map to DTOs and return
            return query.ToList();
        }









        #region Get All Ports
        /// <summary>
        /// Get All Ports details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PortVO> GetAllPortsInfo()
        {
            var port = _unitOfWork.Repository<Port>().Queryable().Where(x => x.RecordStatus == RecordStatus.Active).OrderBy(x => x.PortName).ToList();
            return port.MapToDTO();
        }
        #endregion

        //karun Code

        public object WithoutUpdatedJsonData1(object notUpdatedObj, string modelName, object updatedObj)
        {
            if (notUpdatedObj == null || updatedObj == null)
            {
                throw new ArgumentNullException("Objects cannot be null");
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };


             object Previousdata = notUpdatedObjMethod(notUpdatedObj); 
            var previousJson = JsonConvert.SerializeObject(Previousdata, jsonSerializerSettings);




            var updatedJson = JsonConvert.SerializeObject(updatedObj, jsonSerializerSettings); 
            
            SaveWithoutUpdatedJsonData(updatedJson, modelName);

            return null;  
        }

        public void SaveWithoutUpdatedJsonData(string jsonobjet, string modelname)
        {
            CommonAllData model = new CommonAllData
            {
                Status = "A",
                ModelName = modelname,
                CreatedDate = DateTime.Now,
                ModelData = jsonobjet,
                CreatedBy = 32,
                ModifiedBy = 32,
                ModifiedDate = DateTime.Now,
                ObjectState = ObjectState.Added
            };

            try
            {
                _unitOfWork.Repository<CommonAllData>().Insert(model);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }


        //public object notUpdatedObjMethod(object notUpdatedObj)
        //{
        //    if (notUpdatedObj == null)
        //    {
        //        throw new ArgumentNullException(nameof(notUpdatedObj));
        //    }

        //    Type notUpdatedObType = notUpdatedObj.GetType();

        //    // Check if the object is an IQueryable
        //    var queryableType = typeof(IQueryable<>).MakeGenericType(notUpdatedObType.GetGenericArguments());
        //    if (queryableType.IsAssignableFrom(notUpdatedObType))
        //    {
        //        var queryable = (IQueryable<object>)notUpdatedObj;
        //        var resultList = queryable.ToList();

        //        if (resultList.Count > 0)
        //        {
        //            var firstItem = resultList[0];
        //            var properties = firstItem.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //            var innerData = new Dictionary<string, object>();

        //            foreach (var property in properties)
        //            {
        //                var value = property.GetValue(firstItem);

        //                // Check if the property's type is in the namespace to ignore
        //                if (value != null && !IsTypeInNamespace(value.GetType(), "IPMS.Domain.Models"))
        //                {
        //                    innerData[property.Name] = value;
        //                }
        //            }

        //            return innerData;
        //        }

        //        return null;
        //    }
        //    else
        //    {
        //        // Handle cases where the object might have a 'Result' property
        //        var resultProperty = notUpdatedObType.GetProperty("Result");
        //        if (resultProperty == null)
        //        {
        //            var propertyNames = notUpdatedObType.GetProperties()
        //                .Select(p => p.Name)
        //                .ToArray();

        //            throw new InvalidOperationException($"The object of type '{notUpdatedObType.FullName}' does not contain a property named 'Result'. Available properties are: {string.Join(", ", propertyNames)}");
        //        }

        //        return resultProperty.GetValue(notUpdatedObj);
        //    }
        //}

        //private bool IsTypeInNamespace(Type type, string namespaceToCheck)
        //{
        //    return type.Namespace != null && type.Namespace.StartsWith(namespaceToCheck);
        //}



        //karun codeEnd




































        public object notUpdatedObjMethod(object notUpdatedObj)
        {
            if (notUpdatedObj == null)
            {
                throw new ArgumentNullException(nameof(notUpdatedObj));
            }

            Type notUpdatedObType = notUpdatedObj.GetType();

            // Check if the object is an IQueryable
            var queryableType = typeof(IQueryable<>).MakeGenericType(notUpdatedObType.GetGenericArguments());
            if (queryableType.IsAssignableFrom(notUpdatedObType))
            {
                var queryable = (IQueryable<object>)notUpdatedObj;
                var resultList = queryable.AsNoTracking().ToList(); // Use AsNoTracking to prevent tracking

                if (resultList.Count > 0)
                {
                    var firstItem = resultList[0];
                    var properties = firstItem.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    var innerData = new Dictionary<string, object>();

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(firstItem);

                        // Check if the property's type is in the namespace to ignore
                        if (value != null && !IsTypeInNamespace(value.GetType(), "IPMS.Domain.Models"))
                        {
                            innerData[property.Name] = value;
                        }
                    }

                    return innerData;
                }

                return null;
            }
            else
            {
                // Handle cases where the object might have a 'Result' property
                var resultProperty = notUpdatedObType.GetProperty("Result");
                if (resultProperty == null)
                {
                    var propertyNames = notUpdatedObType.GetProperties()
                        .Select(p => p.Name)
                        .ToArray();

                    throw new InvalidOperationException($"The object of type '{notUpdatedObType.FullName}' does not contain a property named 'Result'. Available properties are: {string.Join(", ", propertyNames)}");
                }

                // Get the result value and ensure it's not causing tracking issues
                var resultValue = resultProperty.GetValue(notUpdatedObj);

              //  If resultValue is IQueryable, use AsNoTracking to prevent tracking issues
                //if (resultValue is IQueryable<object> resultQueryable)
                //{
                //    var resultList = resultQueryable.AsNoTracking().ToList(); // Use AsNoTracking to prevent tracking
                //    return resultList;
                //}

                return resultValue;
            }
        }

        private bool IsTypeInNamespace(Type type, string namespaceToCheck)
        {
            return type.Namespace != null && type.Namespace.StartsWith(namespaceToCheck);
        }






    }
}
