using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace IPMS.Services.WorkFlow
{
    public class UserRegistrationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private User userdata;
       // private UserPort _UserPorts;
        private IAccountRepository _accountRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;

        private const string _entityCode = EntityCodes.User_Registration;
        private string _remarks;

        public UserRegistrationWorkFlow(IUnitOfWork unitOfWork, User userRegistration, string remarks)
        {
            _unitOfWork = unitOfWork;
            userdata = userRegistration;

            _remarks = remarks;
            _accountRepository = new AccountRepository(_unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);

        }

        public int UserId
        {
            get { return userdata.CreatedBy; }
        }

        public Entity Entity
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()//.Where(e => e.EntityCode.Equals(_entityCode))
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public string ReferenceId
        {
            get { return userdata.UserID.ToString(CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public string ReferenceData
        {
            get
            {
                return Common.GetTokensDictionaryForReferenceData(Entity, userdata);
            }
        }

        public int GetRequestStatus(string pEntitycode, string pReferenceNo)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == pEntitycode
                                 && w.ReferenceID == pReferenceNo

                               select w).Count();

            return _entitycode;
        }


        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                foreach (var userPPort in userdata.UserPorts)
                {
                    portcodes.Add(userPPort.PortCode);
                }
                return portcodes;
            }
        }

        public UserRegistrationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);


        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            List<string> portcodes = new List<string>();
            foreach (var userPort in userdata.UserPorts.Where(e => e.PortCode.StartsWith(portCode)).ToList())
            {
                if (userPort.PortCode == portCode)
                {
                    userPort.WorkflowInstanceId = workFlowInstanceId;
                    userPort.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<UserPort>().Update(userPort);
                }
            }
            _unitOfWork.SaveChanges();
        }

        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    Create();
                    break;
                case "UPDT":
                    UpdateStatus();
                    break;
                case "VAP":
                    break;
                case "VRES":
                    break;
                case "VUPD":
                    break;
                case "EXPI":
                    break;
                case "CLOS":
                    break;
            }
        }

        public void Create()
        {
            List<UserPort> userPorts = userdata.UserPorts.ToList();

            foreach (var item in userPorts)
            {
                item.WFStatus = _portGeneralConfigurationRepository.GetPortConfiguration(item.PortCode, ConfigName.WorkflowInitialStatus);

                item.UserID = userdata.UserID;
                item.CreatedBy = userdata.CreatedBy;
                item.CreatedDate = userdata.CreatedDate;
                item.ModifiedBy = userdata.CreatedBy;
                item.ModifiedDate = userdata.CreatedDate;
                item.RecordStatus = "A";
                item.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<UserPort>().Insert(item);
            }

            UserRole userRole = new UserRole();
            int _roleid = 0;

            if (userdata.UserType == "EMP")
            {
                try
                {
                    _roleid = _unitOfWork.Repository<Role>().Queryable().Where(r => r.RoleCode == "GEN").SingleOrDefault<Role>().RoleID;
                }
                catch (Exception)
                {
                    //Specified Role not found then Default ADMN role is assigning
                    _roleid = 1;
                }
            }
            else
            {
                try
                {
                    _roleid = _unitOfWork.Repository<Role>().Queryable().Where(r => r.RoleCode == userdata.UserType).SingleOrDefault<Role>().RoleID;

                }
                catch (Exception)
                {
                    //Specified Role not found then Default ADMN role is assigning
                    _roleid = 1;
                }
            }
            userRole.RoleID = _roleid;
            userRole.UserID = userdata.UserID;
            userRole.CreatedBy = userdata.CreatedBy;
            userRole.CreatedDate = userdata.CreatedDate;
            userRole.ModifiedBy = userdata.CreatedBy;
            userRole.ModifiedDate = userdata.CreatedDate;
            userRole.RecordStatus = "A";
            userRole.ObjectState = ObjectState.Added;
            _unitOfWork.Repository<UserRole>().Insert(userRole);
            _unitOfWork.SaveChanges();
        }

        public void UpdateStatus()
        {
            //Do all db operations             
        }

        public int EntityId
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return entity.EntityID;
            }
        }

        public CompanyVO GetCompanyDetails(int step)
        {
            var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

            CompanyVO vo = new CompanyVO();
            vo.UserType = "EMP";
            //vo.UserTypeId = GlobalConstants.AnonymousUserId; //Always Anonymous User (1) should not change 
            vo.UserTypeId = anonymousUserId; //Always Anonymous User (1) should not change
            return vo;
        }
    }
}