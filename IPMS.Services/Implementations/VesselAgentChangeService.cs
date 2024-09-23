using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.ServiceModel;



namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VesselAgentChangeService : ServiceBase, IVesselAgentChangeService
    {
        private ISubCategoryRepository subcategoryRepository;
        private IPortConfigurationRepository portConfigurationRepository;
        private IAgentRepository agentRepository;
        private IVesselAgentChangeRepository vesselagentRepository;
        private IAccountRepository accountRepository;

        public VesselAgentChangeService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            agentRepository = new AgentRepository(_unitOfWork);
            vesselagentRepository = new VesselAgentChangeRepository(_unitOfWork);
            subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            accountRepository = new AccountRepository(_unitOfWork);
        }

        public VesselAgentChangeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            accountRepository = new AccountRepository(_unitOfWork);
        }

        /// <summary>
        /// To get approved VCN 
        /// </summary>
        /// <returns></returns>
        public List<VesselCallVO> GetVCNDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return vesselagentRepository.GetVcnDetails(_PortCode, GetAgentID(_PortCode, _UserId));
            });
        }

        /// <summary>
        /// To get cahnge of agent request details
        /// </summary>
        /// <returns></returns>
        public List<VesselAgentChangeVO> GetVesselAgentChangeRequestDetails(string etafrom, string etato)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int existagentID = default(int);
                existagentID = GetAgentID(_PortCode, _UserId);

                string portcode = _PortCode;
                return vesselagentRepository.GetVesselAgentChangeRequestDetails(portcode, existagentID, _UserId, etafrom, etato).MapToDTO();
            });
        }

        /// <summary>
        /// To view change of agent request details data from pending tasks 
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VesselAgentChangeVO> GetzVesselAgentChangeRequestDetails(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return vesselagentRepository.GetzVesselAgentChangeRequestDetails(vcn).MapToDTO();
            });
        }

        /// <summary>
        /// To get agent id
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAgentID(string portcode, int userID)
        {
            return vesselagentRepository.GetAgentId(portcode, userID);
        }

        /// <summary>
        /// To get Active VCNs for Autocomplete
        /// </summary>
        /// <returns></returns>
        public List<VesselCallVO> GetVCNActiveList()
        {
            //TODO: Need to validate parameters
            return ExecuteFaultHandledOperation(() => vesselagentRepository.GetVCNActiveList(_PortCode, GetAgentID(_PortCode, _UserId)));

        }
        /// <summary>
        /// To Get Vessel Agent reffernce data
        /// </summary>
        /// <returns></returns>
        public VesselAgentReferenceVO GetVesselAgentChangeReferncesVo(string mode)
        {
            VesselAgentReferenceVO vo = new VesselAgentReferenceVO();

            string name = _LoginName;
            int userId = _UserId;
            string portCode = _PortCode;
            int existagentID = default(int);

            existagentID = GetAgentID(_PortCode, _UserId);

            if (existagentID > 0)
                vo.getPraposedAgents = vesselagentRepository.GetProposedAgents(existagentID, portCode, mode);
            else
                vo.getPraposedAgents = vesselagentRepository.GetProposedAgents(default(int), portCode, mode);

            vo.getResonForTransfer = subcategoryRepository.ReasonForTransfer().MapToDto();
            vo.getDocumentTypes = subcategoryRepository.GetVesselagentChangeDOCTypes().MapToDto();

            return vo;
        }

        /// <summary>
        /// To add change of agent request details 
        /// </summary>
        /// <param name="vesselagentchangedata"></param>
        /// <returns></returns>
        public VesselAgentChangeVO AddVesselAgentChange(VesselAgentChangeVO vesselagentchangedata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int UserId = accountRepository.GetUserId(_LoginName);


                vesselagentchangedata.CreatedDate = DateTime.Now;
                vesselagentchangedata.CreatedBy = UserId;
                vesselagentchangedata.ModifiedBy = UserId;
                vesselagentchangedata.RecordStatus = "A";
                vesselagentchangedata.ModifiedDate = DateTime.Now;

                VesselAgentChange vesselagentchange = new VesselAgentChange();

                vesselagentchange = vesselagentchangedata.MapToEntity();

                vesselagentchange.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(vesselagentchange.VCN);

                vesselagentchange.ProposedAgentName = vesselagentRepository.GetUserName(vesselagentchange.ProposedAgent);
                vesselagentchange.RequestedAgentName = vesselagentRepository.GetUserName(vesselagentchange.ArrivalNotification.AgentID);
                vesselagentchange.ProposedAgent = vesselagentchangedata.ProposedAgent;
                vesselagentchange.CurrentAgentID = GetAgentID(_PortCode, UserId);
                var vesselid = Convert.ToInt32(vesselagentchange.ArrivalNotification.VesselID);
                var vesselDetails = (from v in _unitOfWork.Repository<Vessel>().Queryable().Where(v => v.VesselID == vesselid)
                                   .Include(t => t.SubCategory3)
                                     select v
                               ).FirstOrDefault<Vessel>();

                vesselagentchange.VesselName = vesselDetails != null ? vesselDetails.VesselName : "";
                vesselagentchange.VesselType = vesselDetails.SubCategory3 != null ? vesselDetails.SubCategory3.SubCatName : "";
                vesselagentchange.ArrivalNotification.Vessel = vesselDetails;

                var vessel = _unitOfWork.Repository<VesselCall>().Queryable().Where(v => v.VCN == vesselagentchangedata.VCN);
                var vesselcall = _unitOfWork.Repository<VesselCall>().Find(vessel.SingleOrDefault<VesselCall>().VesselCallID);


                #region Workflow Integration
                string remarks = "New Vessel Agent Change Request";
                #region VesselAgent Change Workflow
                VesselAgentChangeReqWorkFlow vesselagentchangeReqWorkFlow = new VesselAgentChangeReqWorkFlow(_unitOfWork, vesselagentchange, remarks);
                WorkFlowEngine<VesselAgentChangeReqWorkFlow> wf = new WorkFlowEngine<VesselAgentChangeReqWorkFlow>(_unitOfWork, _PortCode, vesselagentchangeReqWorkFlow.userid);
                wf.Process(vesselagentchangeReqWorkFlow, portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                var an = _unitOfWork.Repository<VesselAgentChange>().Find(Convert.ToInt32(vesselagentchangeReqWorkFlow.ReferenceId, CultureInfo.InvariantCulture));
                an.ObjectState = ObjectState.Modified;
                //TODO 2 :Add WorkflowInstanceId field in VesselAgentChange Object and to be Update return id into this field
                an.WorkflowInstanceId = wf.GetWorkFlowInstance(vesselagentchangeReqWorkFlow).WorkflowInstanceId;
                _unitOfWork.Repository<VesselAgentChange>().Update(an);

                #endregion
                #endregion

                return vesselagentchangedata;
            });
        }

        /// <summary>
        /// To modify change of request details
        /// </summary>
        /// <param name="vesselagentchangedata"></param>
        /// <returns></returns>
        public VesselAgentChangeVO ModifyVesselAgentChanges(VesselAgentChangeVO vesselagentchangedata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int UserId = accountRepository.GetUserId(_LoginName);


                vesselagentchangedata.ModifiedBy = UserId;
                vesselagentchangedata.RecordStatus = "A";
                vesselagentchangedata.ModifiedDate = DateTime.Now;

                VesselAgentChange vesselagentchange = new VesselAgentChange();
                vesselagentchange = vesselagentchangedata.MapToEntity();
                vesselagentchange.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<VesselAgentChange>().Update(vesselagentchange);
                _unitOfWork.SaveChanges();
                return vesselagentchangedata;
            });
        }

        /// <summary>
        /// To verify change of agent request 
        /// </summary>
        /// <param name="ReferenceId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void VerifyVesselAgentChangeOfRequest(string ReferenceId, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = (from t in _unitOfWork.Repository<VesselAgentChange>().Query().Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel.SubCategory3).Include(t => t.Agent).Include(t => t.ArrivalNotification.Vessel).Include(t => t.ArrivalNotification.Vessel.SubCategory3).Select()
                              where t.VesselAgentChangeID == Convert.ToInt16(ReferenceId, CultureInfo.InvariantCulture)
                              select t).FirstOrDefault<VesselAgentChange>();

                andata.VesselType = andata.ArrivalNotification.Vessel.SubCategory3.SubCatName;
                andata.ProposedAgentName = andata.Agent.RegisteredName;
                andata.VesselName = andata.ArrivalNotification.Vessel.VesselName;
                VesselAgentChangeVO VO = new VesselAgentChangeVO();
                VesselAgentChangeReqWorkFlow vesselAgentChangeWorkFlow = new VesselAgentChangeReqWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<VesselAgentChangeReqWorkFlow> wf = new WorkFlowEngine<VesselAgentChangeReqWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(vesselAgentChangeWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To approve change of agent request
        /// </summary>
        /// <param name="ReferenceId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveVesselAgentChangeOfRequest(string ReferenceId, string comments, string taskcode)
        {

            EncloseTransactionAndHandleException(() =>
            {
                var andata = (from t in _unitOfWork.Repository<VesselAgentChange>().Query().Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel.SubCategory3).Include(t => t.Agent).Include(t => t.ArrivalNotification.Vessel).Include(t => t.ArrivalNotification.Vessel.SubCategory3).Select()
                              where t.VesselAgentChangeID == Convert.ToInt16(ReferenceId, CultureInfo.InvariantCulture)
                              select t).FirstOrDefault<VesselAgentChange>();

                andata.VesselType = andata.ArrivalNotification.Vessel.SubCategory3.SubCatName;
                andata.ProposedAgentName = andata.Agent.RegisteredName;

                VesselAgentChangeVO VO = new VesselAgentChangeVO();
                VesselAgentChangeReqWorkFlow agentRegistrationWorkFlow = new VesselAgentChangeReqWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<VesselAgentChangeReqWorkFlow> wf = new WorkFlowEngine<VesselAgentChangeReqWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(agentRegistrationWorkFlow, taskcode);



            });
        }

        /// <summary>
        /// To reject change of agent request 
        /// </summary>
        /// <param name="ReferenceId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectVesselAgentChangeOfRequest(string ReferenceId, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = (from t in _unitOfWork.Repository<VesselAgentChange>().Query().Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel.SubCategory3).Include(t => t.Agent).Include(t => t.ArrivalNotification.Vessel).Include(t => t.ArrivalNotification.Vessel.SubCategory3).Select()
                              where t.VesselAgentChangeID == Convert.ToInt16(ReferenceId, CultureInfo.InvariantCulture)
                              select t).FirstOrDefault<VesselAgentChange>();

                andata.VesselType = andata.ArrivalNotification.Vessel.SubCategory3.SubCatName;
                andata.ProposedAgentName = andata.Agent.RegisteredName;

                VesselAgentChangeVO VO = new VesselAgentChangeVO();
                VesselAgentChangeReqWorkFlow agentRegistrationWorkFlow = new VesselAgentChangeReqWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<VesselAgentChangeReqWorkFlow> wf = new WorkFlowEngine<VesselAgentChangeReqWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(agentRegistrationWorkFlow, taskcode);

            });
        }

        public int ValidateVCN(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return vesselagentRepository.ValidateVcn(VCN);
            });
        }

        public List<VesselAgentChangeVO> GetVesselAgentChangeRequestsSearchDetail(string vcn, string vesselName, string etafrom, string etato)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int agentID = default(int);

                agentID = GetAgentID(_PortCode, _UserId);

                return vesselagentRepository.GetVesselAgentChangeRequestsSearchDetail(vcn, vesselName, etafrom, etato, agentID, _UserId, _PortCode).MapToDTO();
            });
        }


    }
}
