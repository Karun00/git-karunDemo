using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace IPMS.Services.WorkFlow
{
    public class VesselAgentChangeReqWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private VesselAgentChange agentChangeRequest;
        private IAccountRepository accountRepository;
        private const string _entityCode = EntityCodes.VACHREQ;
        //   private IWorkFlowEngine<VesselAgentChangeReqWorkFlow> wfEngine;
        private string _remarks;
        CompanyVO vo;

        public VesselAgentChangeReqWorkFlow(IUnitOfWork unitOfWork, VesselAgentChange notification, string remarks)
        {
            _unitOfWork = unitOfWork;
            agentChangeRequest = notification;
            _remarks = remarks;
            accountRepository = new AccountRepository(unitOfWork);
            vo = new CompanyVO();
        }

        public int userid
        {
            get { return agentChangeRequest.CreatedBy; }
        }


        public Entity Entity
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                //Entity entity = _accountRepository.GetEntity(_entityCode);
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()//.Where(e => e.EntityCode.Equals(_entityCode))
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public string ReferenceId
        {
            get { return Convert.ToString(agentChangeRequest.VesselAgentChangeID, CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public string ReferenceData
        {
            get
            {
                Entity entity = accountRepository.GetEntity(_entityCode);
                //TODO: Suresh has to pass VesselType Subcategory name instead of code
                agentChangeRequest.RequestedAgentName = _unitOfWork.SqlQuery<Agent>("select * from Agent where AgentID = @p0", agentChangeRequest.CurrentAgentID.ToString()).FirstOrDefault<Agent>().RegisteredName;
                //_agentChangeRequest.ProposedAgentName = _unitOfWork.SqlQuery<Agent>("select * from Agent where AgentID = @p0", _agentChangeRequest.ProposedAgent.ToString()).FirstOrDefault<Agent>().RegisteredName;

                return Common.GetTokensDictionaryForReferenceData(entity, agentChangeRequest);
            }

        }

        public int GetRequestStatus(string pentitycode, string preferenceno)
        {
            var entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == pentitycode
                                 && w.ReferenceID == preferenceno

                               select w).Count();

            return entitycode;
        }

        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(agentChangeRequest.ArrivalNotification.PortCode);
                return portcodes;
            }
        }


        public VesselAgentChangeReqWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            string updateQuery = "update dbo.VesselAgentChange set WorkflowInstanceId = " + workFlowInstanceId.ToString(CultureInfo.InvariantCulture) + " where VesselAgentChangeID = " + agentChangeRequest.VesselAgentChangeID.ToString(CultureInfo.InvariantCulture);
            _unitOfWork.ExecuteSqlCommand(updateQuery);
         
        }

        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    vo.UserType = UserType.Agent;
                    vo.UserTypeId = agentChangeRequest.ProposedAgent;
                    Create();
                    break;
                case "WFVE":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "WFCO":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "REJ":
                    agentChangeRequest.RecordStatus = "I";
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    UpdateStatus();
                    break;
                case "WFSA":                  
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                   _unitOfWork.ExecuteSqlCommand("Update VesselCall set RecentAgentID = "+ agentChangeRequest.ProposedAgent +"  where VCN= '"+ agentChangeRequest.VCN +"'");
                    break;
                case "WFRE":
                    agentChangeRequest.RecordStatus = "I";
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    UpdateStatus();
                    break;
                case "EXPI":
                    break;
                case "CLOS":
                    break;
            }
        }


        public void Create()
        {
            _unitOfWork.Repository<VesselAgentChange>().Insert(agentChangeRequest);
            List<VesselAgentChangeDocument> documentList = agentChangeRequest.VesselAgentChangeDocuments.ToList();
            foreach (var document in documentList)
            {
                document.VesselAgentChangeID = agentChangeRequest.VesselAgentChangeID;
                document.CreatedBy = agentChangeRequest.CreatedBy;
                document.CreatedDate = agentChangeRequest.CreatedDate;
                document.ModifiedBy = agentChangeRequest.ModifiedBy;
                document.ModifiedDate = agentChangeRequest.ModifiedDate;
                document.RecordStatus = "A";
                _unitOfWork.Repository<VesselAgentChangeDocument>().Insert(document);
            }
            _unitOfWork.SaveChanges();
        }

        public void UpdateStatus()
        {
            if (agentChangeRequest.RecordStatus == "I")
            {
                _unitOfWork.ExecuteSqlCommand("update dbo.VesselAgentChange set RecordStatus=@p0, ModifiedBy=@p1,ModifiedDate=@p2 where VesselAgentChangeID = @p3", agentChangeRequest.RecordStatus, agentChangeRequest.ModifiedBy, DateTime.Now.ToString(CultureInfo.InvariantCulture), agentChangeRequest.VesselAgentChangeID);
            }
        }



        public CompanyVO GetCompanyDetails(int step)
        {
            return vo;
        }

    }
}
