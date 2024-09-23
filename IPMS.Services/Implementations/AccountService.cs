using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Security.Cryptography;
using System.IO;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System.Data.Entity.SqlServer;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AccountService : ServiceBase, IAccountService
    {

        private IAccountRepository _accountRepository;
        private IUserRepository _userRepository;

        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;
        private INotificationPublisher _notificationpublisher;
        private IEntityRepository _entity;



        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _accountRepository = new AccountRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);

        }

        public AccountService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _accountRepository = new AccountRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);

        }

        public AccountLoginModel UserLogin(string username, string password, string ipAddress)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string pwd = Password.Encrypt(password, true);
                return _accountRepository.UserLogin(username, pwd, ipAddress);

            });
        }


        public IEnumerable<PortVO> GetPortsByUser()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _accountRepository.GetPortsByUser(_UserId);

            });

        }


        public IEnumerable<Module> GetModulesByUser()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _accountRepository.GetModulesByUser(_UserId);

            });
        }

        public IEnumerable<Role> GetRoles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _accountRepository.GetRoles();
            });
        }

        public IEnumerable<GroupedPendingTaskVO> GetPendingTask()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _accountRepository.GetPendingTask(_UserId, _PortCode);
            });
        }

        public static T DeserializeJSon<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(stream);
            return obj;
        }

        public PendingTaskVO ApprovedPendingTask(PendingTaskVO pendingtask)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                int userid = _accountRepository.GetUserId(_LoginName);
                string wfapprovedcode = _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.ApprovedCode);
                if (pendingtask.EntityCode == EntityCodes.Arrival_Notification)
                {
                    var andata = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                  where t.VCN == pendingtask.ReferenceID
                                  select t).FirstOrDefault<ArrivalNotification>();



                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, pendingtask.Remarks);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(arrivalNotificationWorkFlow, wfapprovedcode);


                }

                if (pendingtask.EntityCode == EntityCodes.Service_Request)
                {

                    var srdata = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Include(t => t.ServiceRequestSailings).Include(t => t.ServiceRequestWarpings).Include(t => t.ServiceRequestShiftings).Include(t => t.ServiceRequestShiftings.Select(p => p.Berth)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard1)).Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.VesselCallMovements).Include(t => t.SubCategory).Include(t => t.SubCategory1).Select()
                                  where t.ServiceRequestID == Convert.ToInt32(pendingtask.ReferenceID, CultureInfo.InvariantCulture)
                                  select t).FirstOrDefault<ServiceRequest>();

                    ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, srdata, pendingtask.Remarks);
                    WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(servicerequestWorkFlow, wfapprovedcode);


                }

                if (pendingtask.EntityCode == "VACHREQ")
                {

                    var query = (from t in _unitOfWork.Repository<VesselAgentChange>().Query().Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.Agent).Include(t => t.SubCategory).Select()
                                 orderby t.VesselAgentChangeID descending
                                 where t.VesselAgentChangeID == Convert.ToInt32(pendingtask.ReferenceID, CultureInfo.InvariantCulture)
                                 select t
                             ).FirstOrDefault<VesselAgentChange>();

                    VesselAgentChangeReqWorkFlow vesselagentchangerWorkFlow = new VesselAgentChangeReqWorkFlow(_unitOfWork, query, pendingtask.Remarks);
                    WorkFlowEngine<VesselAgentChangeReqWorkFlow> wf = new WorkFlowEngine<VesselAgentChangeReqWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(vesselagentchangerWorkFlow, wfapprovedcode);


                }
                return pendingtask;
            });
        }

        public PendingTaskVO RejectedPendingTask(PendingTaskVO pendingtask)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                if (pendingtask.EntityCode == EntityCodes.Arrival_Notification)
                {
                    var andata = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                  where t.VCN == pendingtask.ReferenceID
                                  select t).FirstOrDefault<ArrivalNotification>();
                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, pendingtask.Remarks);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(arrivalNotificationWorkFlow, pendingtask.TaskCode);

                }

                if (pendingtask.EntityCode == EntityCodes.Service_Request)
                {

                    var srdata = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Include(t => t.ServiceRequestSailings).Include(t => t.ServiceRequestWarpings).Include(t => t.ServiceRequestShiftings).Include(t => t.ServiceRequestShiftings.Select(p => p.Berth)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard1)).Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.VesselCallMovements).Include(t => t.SubCategory).Include(t => t.SubCategory1).Select()
                                  where t.ServiceRequestID == Convert.ToInt32(pendingtask.ReferenceID, CultureInfo.InvariantCulture)
                                  select t).FirstOrDefault<ServiceRequest>();

                    ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, srdata, pendingtask.Remarks);
                    WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(servicerequestWorkFlow, pendingtask.TaskCode);


                }

                if (pendingtask.EntityCode == "VACHREQ")
                {

                    var query = (from t in _unitOfWork.Repository<VesselAgentChange>().Query().Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.Agent).Include(t => t.SubCategory).Select()
                                 orderby t.VesselAgentChangeID descending
                                 where t.VesselAgentChangeID == Convert.ToInt32(pendingtask.ReferenceID, CultureInfo.InvariantCulture)
                                 select t
                             ).FirstOrDefault<VesselAgentChange>();

                    VesselAgentChangeReqWorkFlow vesselagentchangerWorkFlow = new VesselAgentChangeReqWorkFlow(_unitOfWork, query, pendingtask.Remarks);
                    WorkFlowEngine<VesselAgentChangeReqWorkFlow> wf = new WorkFlowEngine<VesselAgentChangeReqWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(vesselagentchangerWorkFlow, pendingtask.TaskCode);


                }
                return pendingtask;
            });
        }

        public IEnumerable<PendingTaskVO> GetWorkflowTask(PendingTaskVO pendingtask)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                string wfapprovedcode = _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.WorkflowInitialStatus);

                var wftotask = (from wft in _unitOfWork.Repository<WorkflowTask>().Query().Select()
                                join wftr in _unitOfWork.Repository<WorkflowTaskRole>().Query().Select()
                                on wft.EntityID equals wftr.EntityID
                                join e in _unitOfWork.Repository<Entity>().Query().Select()
                                on wft.EntityID equals e.EntityID
                                join sc in _unitOfWork.Repository<SubCategory>().Query().Select()
                                on wft.WorkflowTaskCode equals sc.SubCatCode
                                join ur in _unitOfWork.Repository<UserRole>().Query().Select()
                                on wftr.RoleID equals ur.RoleID
                                where ur.UserID == userid && wft.WorkflowTaskCode != wfapprovedcode
                                && e.EntityCode == pendingtask.EntityCode &&
                                wft.Step == pendingtask.NextStep
                                group wft by new { wft.WorkflowTaskCode, sc.SubCatName } into g
                                select new PendingTaskVO
                                {
                                    TaskCode = g.Key.WorkflowTaskCode,
                                    SubCatName = g.Key.SubCatName
                                });
                return wftotask.ToList();
            });
        }

        //--
        public IEnumerable<Module> GetModulesTreeView()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _accountRepository.GetModulesTreeView(_UserId);

            });
        }

        public IEnumerable<SystemNotificationVO> GetSystemNotifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _accountRepository.GetSystemNotifications(_UserId, _PortCode);
            });
        }

        public string ChangePassword(AccountLoginModel passwordModel)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                string password = Password.Encrypt(passwordModel.Password, true);
                string newpassword = Password.Encrypt(passwordModel.NewPassword, true);

                var _user = _userRepository.GetUserByID(_UserId);
                int _userid = _user.UserID;

                string msg = string.Empty;

                if (string.IsNullOrEmpty(_PortCode))
                {
                    var Userdata = (from t in _unitOfWork.Repository<User>().Query().Include(t => t.UserPorts).Select()
                                    where t.UserID == _UserId && t.IsFirstTimeLogin == "Y"
                                    select t
                            ).FirstOrDefault<User>();

                    foreach (var item in Userdata.UserPorts)
                    {
                        if (item.WFStatus == "WFSA")
                            _PortCode = item.PortCode;
                    }
                }

                PortGeneralConfig previousPasswordsCount = _unitOfWork.Repository<PortGeneralConfig>().Query().Select().Where(e => e.ConfigName == ConfigName.PreviousPasswordsCount && e.GroupName == "General Configuration" && e.PortCode == _PortCode).FirstOrDefault();

                passwordModel.UserID = (passwordModel.UserID == 0 ? _userid : passwordModel.UserID);
                var oldpassword = (from u in _unitOfWork.Repository<User>().Query().Select()
                                   where u.PWD == password && u.UserName.ToUpperInvariant() == passwordModel.UserName.ToUpperInvariant()
                                   select u).FirstOrDefault<User>();

                if (oldpassword != null)
                {
                    bool result = _accountRepository.ChangePassword(password, newpassword, passwordModel.UserName, passwordModel.UserID, _PortCode, previousPasswordsCount.ConfigValue);

                    if (result == true)
                    {

                        #region Sending Email for Change Password to the user
                        CompanyVO nextStepCompany = new CompanyVO();
                        nextStepCompany.UserType = _user.UserType;
                        nextStepCompany.UserTypeId = _user.UserTypeID;
                        var _ApprovedCode = _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.ApprovedCode);

                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.User).EntityID, _user.UserID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _ApprovedCode);
                        #endregion
                    }
                    else
                    {
                        msg = "Previous " + (previousPasswordsCount != null ? previousPasswordsCount.ConfigValue : string.Empty) + " passwords can't be repeated";
                    }
                }
                else
                {
                    msg = "The Current access password entered is not valid";
                }
                return msg;
            });
        }

        public string GetUserPrivilegesWithControllerName(string controllerName, string username)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string p = _accountRepository.GetUserPrivilegesWithControllerName(controllerName, username);
                return p;
            });


        }

        public int GetPendingTaskCount()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _accountRepository.GetPendingTaskCount(_UserId, _PortCode);

            });
        }

        public int GetPortSessiontimeOut()
        {

            PortGeneralConfig sesstionTimeout = _unitOfWork.Repository<PortGeneralConfig>().Queryable().Where(e => e.ConfigName == "SESSIONTIMEOUT" && e.PortCode == "DB").FirstOrDefault();
            int sesstionTimeoutPeriod = Convert.ToInt32(sesstionTimeout.ConfigValue, CultureInfo.InvariantCulture);

            return sesstionTimeoutPeriod;
        }

        public int GetMobilePortSessiontimeOut()
        {

            PortGeneralConfig sesstionTimeout = _unitOfWork.Repository<PortGeneralConfig>().Queryable().Where(e => e.ConfigName == "MOBILESESSIONTIMEOUT" && e.PortCode == "DB").FirstOrDefault();
            int sesstionTimeoutPeriod = Convert.ToInt32(sesstionTimeout.ConfigValue, CultureInfo.InvariantCulture);

            return sesstionTimeoutPeriod;
        }

        //By Mahesh : For mobile App
        public IEnumerable<PortVO> GetPortsByUserForMobile(string uname)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                _UserId = _userRepository.GetUserIdByLoginname(uname);
                return _accountRepository.GetPortsByUser(_UserId);

            });

        }



    }
}
