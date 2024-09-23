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
    public class AgentRegistrationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private Agent _AgentRegistration;
        private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.AGENTREG;
        private string _remarks;
        private CompanyVO vo;

        public AgentRegistrationWorkFlow(IUnitOfWork unitOfWork, Agent agentregistration, string remarks)
        {
            _unitOfWork = unitOfWork;
            _AgentRegistration = agentregistration;
            vo = new CompanyVO();
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
        }

        public int userid
        {
            get { return _AgentRegistration.CreatedBy; }
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
            get { return _AgentRegistration.AgentID.ToString(CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public string ReferenceData
        {
            get
            {
                return Common.GetTokensDictionaryForReferenceData(Entity, _AgentRegistration);
            }

        }


        public int GetRequestAgentRegStatus(string entitycode, int referenceno)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == entitycode
                                 && w.WorkflowInstanceId == referenceno

                               select w).Count();

            return _entitycode;
        }

        public int GetRequestStatus(string entitycode, string referenceno)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == entitycode
                                 && w.ReferenceID == referenceno

                               select w).Count();

            return _entitycode;
        }


        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                foreach (var agentPPort in _AgentRegistration.AgentPorts)
                {
                    portcodes.Add(agentPPort.PortCode);
                }
                return portcodes;
            }
        }

        public AgentRegistrationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();

        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
           // List<string> portcodes = new List<string>();
            foreach (var agentPPort in _AgentRegistration.AgentPorts.Where(e => e.PortCode.StartsWith(portCode,StringComparison.CurrentCultureIgnoreCase)).ToList())
            {
                if (agentPPort.PortCode == portCode)
                {
                    agentPPort.WorkflowInstanceId = workFlowInstanceId;
                    agentPPort.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<AgentPort>().Update(agentPPort);
                }
            }
            _unitOfWork.SaveChanges();

            int count = GetRequestAgentRegStatus(_entityCode, workFlowInstanceId);
            if (count > 0)
            {
                //var brt = 
                    _unitOfWork.ExecuteSqlCommand(" update dbo.AgentPort SET WFStatus =  (select ApproveCode from PortConfiguration where PortCode= (select PortCode from dbo.AgentPort where workFlowInstanceId=" + workFlowInstanceId + ")) where  workFlowInstanceId = " + workFlowInstanceId + " ");
            }

            string updateQuery = "update dbo.AgentPort set WorkflowInstanceId = " + workFlowInstanceId.ToString(CultureInfo.InvariantCulture) + " where AgentId = " +
                 _AgentRegistration.AgentID.ToString(CultureInfo.InvariantCulture) + " and PortCode = '" + portCode + "'";
            _unitOfWork.ExecuteSqlCommand(updateQuery);
        }


        public void UpdateVO()
        {
            var usertype = _unitOfWork.Repository<User>().Find(_AgentRegistration.CreatedBy).UserType;
            if (usertype == UserType.Agent)
            {
                vo.UserType = UserType.Agent;
                vo.UserTypeId = _AgentRegistration.AgentID;
            }
            else
            {
                vo.UserType = UserType.Employee;
                vo.UserTypeId = _AgentRegistration.CreatedBy;
            }
        }
        public void ExecuteTask(string workflowTaskCode)
        {
            var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]); 

            switch (workflowTaskCode)
            {
                case "NEW":
                    vo.UserType = UserType.Employee;
                    //vo.UserTypeId = GlobalConstants.AnonymousUserId;
                    vo.UserTypeId = anonymousUserId;
                    Create();
                    break;
                case "UPDT":
                    vo.UserType = UserType.Employee;
                    //vo.UserTypeId = GlobalConstants.AnonymousUserId;
                    vo.UserTypeId = anonymousUserId;
                    UpdateStatus();
                    break;
                case "WFSA":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    UpdateVO();
                    break;
                case "REJ":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "WFRE":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "WFVE":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "EXPI":
                    break;
                case "CLOS":
                    break;
                case "VAP":
                    break;


            }
        }

        public void Create()
        {
            CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);

            if (_AgentRegistration.AgentID == 0)
            {
                _AgentRegistration.ObjectState = ObjectState.Added;
                _AgentRegistration.Address.CreatedBy = _AgentRegistration.CreatedBy;

                _AgentRegistration.Address.ModifiedBy = _AgentRegistration.ModifiedBy;
                _AgentRegistration.ReferenceNo = codeGenerator.GenerateRefNum();
                _AgentRegistration.Address.ModifiedDate = DateTime.Now;
                _AgentRegistration.Address.CreatedDate = DateTime.Now;
                _AgentRegistration.Address.RecordStatus = "A";
                _unitOfWork.Repository<Address>().Insert(_AgentRegistration.Address);
                _unitOfWork.SaveChanges();

                _AgentRegistration.BusinessAddressID = _AgentRegistration.Address.AddressID;
                if (_AgentRegistration.Address1 != null)
                {
                    _AgentRegistration.Address1.CreatedBy = _AgentRegistration.CreatedBy;
                    _AgentRegistration.Address1.ModifiedBy = _AgentRegistration.ModifiedBy;
                    _AgentRegistration.Address1.ModifiedDate = DateTime.Now;
                    _AgentRegistration.Address1.ModifiedDate = DateTime.Now;
                    _unitOfWork.Repository<Address>().Insert(_AgentRegistration.Address1);
                    _unitOfWork.SaveChanges();
                    _AgentRegistration.PostalAddressID = _AgentRegistration.Address1.AddressID;
                }


                _AgentRegistration.AuthorizedContactPerson.CreatedBy = _AgentRegistration.CreatedBy;
                _AgentRegistration.AuthorizedContactPerson.ModifiedBy = _AgentRegistration.ModifiedBy;
                _AgentRegistration.AuthorizedContactPerson.ModifiedDate = DateTime.Now;

                _unitOfWork.Repository<AuthorizedContactPerson>().Insert(_AgentRegistration.AuthorizedContactPerson);
                _unitOfWork.SaveChanges();
                _AgentRegistration.AuthorizedContactPersonID = _AgentRegistration.AuthorizedContactPerson.AuthorizedContactPersonID;

                List<AgentPort> applWorkFlow = _AgentRegistration.AgentPorts.ToList();
                List<AgentDocument> documents = _AgentRegistration.AgentDocuments.ToList();
                _AgentRegistration.AgentPorts = null;
                _AgentRegistration.AuthorizedContactPerson = null;
                _AgentRegistration.Address = null;
                _AgentRegistration.Address1 = null;
                _AgentRegistration.AgentDocuments = null;
                _AgentRegistration.CreatedDate = DateTime.Now;
                _AgentRegistration.CreatedBy = _AgentRegistration.CreatedBy;
                _AgentRegistration.ModifiedDate = DateTime.Now;
                _AgentRegistration.ModifiedBy = _AgentRegistration.ModifiedBy;
                _unitOfWork.Repository<Agent>().Insert(_AgentRegistration);
                _unitOfWork.SaveChanges();

                foreach (var workFlow in applWorkFlow)
                {
                    workFlow.AgentID = _AgentRegistration.AgentID;
                    workFlow.CreatedBy = _AgentRegistration.CreatedBy;
                    workFlow.VerifiedDate = DateTime.Now;
                    workFlow.ApprovedDate = DateTime.Now;
                    workFlow.ModifiedBy = _AgentRegistration.ModifiedBy;
                    workFlow.ModifiedDate = DateTime.Now;
                    workFlow.WFStatus = "NEW";
                }
                _unitOfWork.Repository<AgentPort>().InsertRange(applWorkFlow);
                _unitOfWork.SaveChanges();
                foreach (AgentDocument document in documents)
                {
                    document.CreatedBy = _AgentRegistration.CreatedBy;
                    document.CreatedDate = DateTime.Now;
                    document.ModifiedBy = _AgentRegistration.ModifiedBy;
                    document.ModifiedDate = DateTime.Now;

                    document.Document.CreatedBy = _AgentRegistration.CreatedBy;
                    document.Document.CreatedDate = DateTime.Now;
                    document.Document.ModifiedBy = _AgentRegistration.ModifiedBy;

                    document.Document.ModifiedDate = DateTime.Now;
                    document.Document.RecordStatus = _AgentRegistration.RecordStatus;
                    //_unitOfWork.Repository<Document>().Insert(document.Document);
                    //_unitOfWork.SaveChanges();

                    document.AgentID = _AgentRegistration.AgentID;
                    document.DocumentID = document.Document.DocumentID;
                    document.RecordStatus = document.Document.RecordStatus;
                    document.CreatedBy = _AgentRegistration.CreatedBy;
                    document.CreatedDate = DateTime.Now;
                    document.ModifiedBy = _AgentRegistration.ModifiedBy;
                    document.ModifiedDate = DateTime.Now;

                    document.Document.AgentDocuments = null;
                    document.Document.ArrivalDocuments = null;
                    document.Document.SubCategory = null;
                    document.Document.SubCategory1 = null;
                    document.Document = null;
                }
                _unitOfWork.Repository<AgentDocument>().InsertRange(documents);
                _unitOfWork.SaveChanges();
            }
            else
            {

                List<AgentPort> applWorkFlow = _AgentRegistration.AgentPorts.ToList();
                foreach (var workFlow in applWorkFlow)
                {
                    workFlow.AgentID = _AgentRegistration.AgentID;
                    workFlow.CreatedBy = _AgentRegistration.CreatedBy;
                    workFlow.VerifiedDate = DateTime.Now;
                    workFlow.ApprovedDate = DateTime.Now;
                    workFlow.ModifiedBy = _AgentRegistration.ModifiedBy;
                    workFlow.ModifiedDate = DateTime.Now;
                    workFlow.WFStatus = "NEW";
                }
                _unitOfWork.Repository<AgentPort>().InsertRange(applWorkFlow);
                _unitOfWork.SaveChanges();
            }


        }



        public void UpdateStatus()
        {
            //Do all db operations 
            //ArrivalNotificationWorkFlow returnObject = new ArrivalNotificationWorkFlow();
            //returnObject._notification = arrivalNotificationObject
        }




        /*public void SetWorkFlowData(ref WorkflowInstance instance)
        {
            instance.ReferenceID = _notification.VCN;
            instance.WorkflowCode = _workFlowCode;
        }*/


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
            return vo;
        }

        public void Approve()
        {
            int validityperiod = 4;
            //var brt = _unitOfWork.ExecuteSqlCommand(" update dbo.AgentPort SET FromDate =  @p0, ToDate=@p1 where agentID = @p2 and Portcode=@p3", DateTime.Now, DateTime.Now.AddDays(validityperiod), _AgentRegistration.agentId, _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().PortCode); ;

            _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().AgentID = _AgentRegistration.AgentID;
            _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().PortCode = _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().PortCode;
            _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().FromDate = DateTime.Now;
            _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().ToDate = DateTime.Now.AddDays(validityperiod);
            _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().ModifiedBy = _AgentRegistration.ModifiedBy;
            _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().ModifiedDate = DateTime.Now;

            _AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>().ObjectState = ObjectState.Modified;

            _unitOfWork.Repository<AgentPort>().Update(_AgentRegistration.AgentPorts.FirstOrDefault<AgentPort>());

            _unitOfWork.SaveChanges();

        }
        //public int EntityID
        //{
        //    get { throw new NotImplementedException(); }
        //}
    }


}