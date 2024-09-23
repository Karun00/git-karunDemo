using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Core.Repository.Exceptions;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using IPMS.Services.WorkFlow;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ArrivalNotificationService : ServiceBase, IArrivalNotificationService
    {

        private ISubCategoryRepository _subcategoryRepository;
        private IAgentRepository _agentRepository;
        private IPortRepository _portRepository;
        private IBerthRepository _berthRepository;
        private ILicensingRequestRepository _licensingRequestRepository;
        private IPilotRepository _pilotRepository;
        private IVesselRepository _vesselRepository;
        private IPortGeneralConfigsRepository _portConfigurationRepository;
        private IArrivalNotificationRepository _arrivalnotificationRepository;
        private IWorkFlowTaskRepository _workFlowTaskRepository;        
        private IArrivalNotificationRepository _arrivalNotificationRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigsRepository;
        private IUserRepository _userRepository;
        private INotificationRepository _notificationRepository;
        private IEntityRepository _entityRepository;
        private IDockingPlanRepository _dockinplanRepository;

        private const string _entityCode = EntityCodes.Arrival_Notification;
        private bool Arrival72 = false;        

        public ArrivalNotificationService(IUnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
            _agentRepository = new AgentRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);


            _berthRepository = new BerthRepository(_unitOfWork);
            _licensingRequestRepository = new LicensingRequestRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _pilotRepository = new PilotRepository(_unitOfWork);
            _vesselRepository = new VesselRepository(_unitOfWork);
            _portConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _arrivalnotificationRepository = new ArrivalNotificationRepository(_unitOfWork);
            _workFlowTaskRepository = new WorkFlowTaskRepository(_unitOfWork);
            _arrivalNotificationRepository = new ArrivalNotificationRepository(_unitOfWork);
            _portGeneralConfigsRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _notificationRepository = new NotificationRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
            _dockinplanRepository = new DockingPlanRepository(_unitOfWork);
        }

        public ArrivalNotificationService()
        {         
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
            _agentRepository = new AgentRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _licensingRequestRepository = new LicensingRequestRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _pilotRepository = new PilotRepository(_unitOfWork);
            _vesselRepository = new VesselRepository(_unitOfWork);
            _portConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _arrivalnotificationRepository = new ArrivalNotificationRepository(_unitOfWork);
            _workFlowTaskRepository = new WorkFlowTaskRepository(_unitOfWork);
            _arrivalNotificationRepository = new ArrivalNotificationRepository(_unitOfWork);
            _portGeneralConfigsRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _notificationRepository = new NotificationRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
            _dockinplanRepository = new DockingPlanRepository(_unitOfWork);
        }

        /// <summary>
        /// To get Agent Details
        /// </summary>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        public Agent GetAgentDetails(int agentid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var objAgentDetails = (from a in _unitOfWork.Repository<Agent>().Query().Select()
                                       where a.AgentID == agentid
                                       select a).FirstOrDefault<Agent>();


                return objAgentDetails;
            });

        }

        /// <summary>
        /// To Get vesel Details
        /// </summary>
        /// <param name="VesselID"></param>
        /// <returns></returns>
        public Vessel GetVesselDetails(int vesselid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var objVesselDetails = (from v in _unitOfWork.Repository<Vessel>().Query().Select()
                                        where v.VesselID == vesselid
                                        select v).FirstOrDefault<Vessel>();
                return objVesselDetails;

            });
        }

        public string AddTimeRuleConfig(ArrivalNotificationVO arrivalnotificationdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var PortCode = new SqlParameter("@PortCode", _PortCode);
                var Starttime = new SqlParameter("@StartDate", arrivalnotificationdata.ETA);
                string arrivaltimerule = _unitOfWork.SqlQuery<string>("dbo.usp_ArrivalNotificationTimeRule @StartDate, @PortCode", Starttime, PortCode).FirstOrDefault();

                return arrivaltimerule;
            });
        }

        private bool EdiTimeRule(ArrivalNotificationVO arrivalnotificationdata)
        {
            Arrival72 = false;
            var PortCode = new SqlParameter("@PortCode", _PortCode);
            var Starttime = new SqlParameter("@StartDate", arrivalnotificationdata.ETA);
            string arrivaltimerule = _unitOfWork.SqlQuery<string>("dbo.usp_ArrivalNotificationTimeRule @StartDate, @PortCode", Starttime, PortCode).FirstOrDefault();
            string[] fields = arrivaltimerule.Split('@');
            if (fields[0] == "2")
            {
                Arrival72 = true;
            }
            if (fields[0] == "1")
            {
                throw new BusinessExceptions("Arrival Notification Does Not Accept Below " + fields[1] + " Hours");
            }
            return true;
        }

        /// <summary>
        /// To Add ArrivalNotification Data
        /// </summary>
        /// <param name="arrivalnotificationdata"></param>
        /// <returns></returns>
        public ArrivalNotificationVO AddArrivalNotification(ArrivalNotificationVO arrivalnotificationdata)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                EdiTimeRule(arrivalnotificationdata);
                ArrivalNotification arrivalNotification = null;
                arrivalnotificationdata.NominationDate = DateTime.Now;
                arrivalnotificationdata.Isdraft = "N";
                arrivalnotificationdata.CellNo = arrivalnotificationdata.CellNo.Replace("(", "").Replace(")", "").Replace("-", "");
                arrivalNotification = arrivalnotificationdata.MapToEntity();
                arrivalNotification.Isdraft = "N";
                if (!string.IsNullOrEmpty(arrivalnotificationdata.DraftKey))
                {
                    arrivalNotification.GeneratedVCN = arrivalnotificationdata.DraftKey;
                    arrivalNotification.VCN = "";
                }

                if (arrivalnotificationdata.GRT > 500)
                {
                    if (arrivalNotification.AppliedDate == null)
                    {
                        throw new BusinessExceptions("Applied Date is Mandatory");
                    }
                }

                //TODO: Raise exception of _LoginName is null or empty.
                //DONE: Handled while fethcing _UserId in ServiceBase
                try
                {
                    var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);
                    if (Isagent != null)
                    {
                        arrivalNotification.AgentID = Isagent.AgentID;
                  
                    }
                    if (Isagent.AgentID == 1)
                    {
                        throw new BusinessExceptions("Internal Server error occured. Please contact to administrator.");
                    }
                    else if (Isagent.AgentID == 1 && Isagent != null)
                    {
                        arrivalNotification.AgentID = 0;
                    }
                    arrivalNotification.CreatedBy = _UserId;
                    arrivalNotification.CreatedDate = DateTime.Now;
                    arrivalNotification.ModifiedBy = _UserId;
                    arrivalNotification.ModifiedDate = DateTime.Now;
                    arrivalNotification.RecordStatus = "A";
                }
                catch (Exception ex)
                {
                    throw new FaultException("_LoginName should not be empty, Exception = " + ex);
                }

                //TODO: Raise exception of _PortCode is null or empty.
                arrivalNotification.PortCode = _PortCode;

                #region For Creating of Arrival Agent List Integration
                List<ArrivalAgent> arrivalaagentlist = new List<ArrivalAgent>();
                ArrivalAgent arrivalagent = new ArrivalAgent();
                arrivalagent.AgentID = arrivalNotification.AgentID;
                arrivalagent.VCN = arrivalnotificationdata.VCN;
                arrivalagent.IsPrimary = "Y";
                arrivalaagentlist.Add(arrivalagent);

                if (arrivalnotificationdata.SecondaryAgentID1 > 0)
                {
                    arrivalagent = new ArrivalAgent();
                    arrivalagent.AgentID = arrivalnotificationdata.SecondaryAgentID1;
                    arrivalagent.VCN = arrivalnotificationdata.VCN;
                    arrivalagent.IsPrimary = "F";
                    arrivalaagentlist.Add(arrivalagent);
                }
                if (arrivalnotificationdata.SecondaryAgentID2 > 0)
                {
                    arrivalagent = new ArrivalAgent();
                    arrivalagent.AgentID = arrivalnotificationdata.SecondaryAgentID2;
                    arrivalagent.VCN = arrivalnotificationdata.VCN;
                    arrivalagent.IsPrimary = "S";
                    arrivalaagentlist.Add(arrivalagent);
                }
                arrivalNotification.ArrivalAgents = arrivalaagentlist.ToList();
                #endregion

                

                #region Workflow Integration
                string remarks = "New Arrival Notification";

                #region Arrival Notification Workflow
                ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
               WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, arrivalNotification.CreatedBy);
                wf.Process(arrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(arrivalNotification.PortCode, ConfigName.WorkflowInitialStatus));

                #endregion

                #region IMDG Workflow

                if (arrivalnotificationdata.AnyDangerousGoodsonBoard == "A")
                {
                    remarks = "New IMDG Arrival Notification";
                    IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                    WorkFlowEngine<IMDGArrivalNotificationWorkFlow> imdgwf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, arrivalNotification.CreatedBy);
                    imdgwf.Process(imdgarrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(arrivalNotification.PortCode, ConfigName.WorkflowInitialStatus));
                }


                #endregion

                #region ISPS Workflow
                if (arrivalnotificationdata.AppliedForISPS == "A")
                {
                   remarks = "New ISPS Arrival Notification";
                    ISPSArrivalNotificationWorkFlow ispsarrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                    WorkFlowEngine<ISPSArrivalNotificationWorkFlow> ispswf = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, arrivalNotification.CreatedBy);
                    ispswf.Process(ispsarrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(arrivalNotification.PortCode, ConfigName.WorkflowInitialStatus));

                }
                #endregion

                #region PH Workflow
                remarks = "New PH Arrival Notification";
                PHArrivalNotificationWorkFlow pharrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                WorkFlowEngine<PHArrivalNotificationWorkFlow> phwf = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, arrivalNotification.CreatedBy);
               phwf.Process(pharrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(arrivalNotification.PortCode, ConfigName.WorkflowInitialStatus));


                #endregion


                #region DHM Workflow
                if (arrivalnotificationdata.Tidal == "A" || arrivalnotificationdata.IsSpecialNature == "A")
                {
                    remarks = "New DHM Arrival Notification";
                    DHMArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                    WorkFlowEngine<DHMArrivalNotificationWorkFlow> dhmwf = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, arrivalNotification.CreatedBy);
                    dhmwf.Process(dhmarrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(arrivalNotification.PortCode, ConfigName.WorkflowInitialStatus));

                }
                #endregion
                //Added By Srinivas
                #region WasteDeclaration Workflow
                if (arrivalnotificationdata.WasteDeclaration == "A")
                {
                    foreach (var i in arrivalnotificationdata.WasteDeclarations)
                    {
                        var marpolCode = i.MarpolCode;
                        if (marpolCode == "MRL5")
                        {
                            remarks = "Marpol V WasteDecalartion Arrival Notification";
                            var entityid = _entityRepository.GetEntitiesNotification(EntityCodes.WasteDeclarationAN).EntityID;
                            var nextStepCompany = _userRepository.GetUserDetails(_UserId);
                            _notificationRepository.PushMessageToQueue(entityid, arrivalnotificationdata.VCN, _UserId, nextStepCompany, _PortCode, WFStatus.An72, null);
                            WasteDeclarationArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new WasteDeclarationArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                            WorkFlowEngine<WasteDeclarationArrivalNotificationWorkFlow> dhmwf = new WorkFlowEngine<WasteDeclarationArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, arrivalNotification.CreatedBy);
                            dhmwf.Process(dhmarrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(arrivalNotification.PortCode, ConfigName.WorkflowInitialStatus));
                        }
                    }                 

                }
                #endregion
                #endregion
                arrivalnotificationdata.VCN = arrivalNotification.VCN;

                #region Arrival Notification below 72 Hours
                if (Arrival72)
                {
                    var entityid = _entityRepository.GetEntitiesNotification(EntityCodes.Arrival_Notification).EntityID;
                    var nextStepCompany = _userRepository.GetUserDetails(_UserId);
                   _notificationRepository.PushMessageToQueue(entityid, arrivalnotificationdata.VCN, _UserId, nextStepCompany, _PortCode, WFStatus.An72, null);
                }
                #endregion
                return arrivalnotificationdata;
            });
        }

        /// <summary>
        /// To Modify ArrivalNotification
        /// </summary>
        /// <param name="arrivalnotificationdata"></param>
        /// <returns></returns>
        public ArrivalNotificationVO ModifyArrivalNotification(ArrivalNotificationVO arrivalnotificationdata)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                int userid = _UserId;
                string _remarks = string.Empty;
                
                arrivalnotificationdata.CellNo = arrivalnotificationdata.CellNo.Replace("(", "").Replace(")", "").Replace("-", "");
                ArrivalNotification arrivalNotification = null;

                arrivalNotification = arrivalnotificationdata.MapToEntity();

                arrivalNotification.CreatedDate = arrivalNotification.CreatedDate;
                arrivalNotification.CreatedBy = arrivalNotification.CreatedBy;
                arrivalNotification.ModifiedDate = arrivalNotification.ModifiedDate;

                arrivalNotification.ModifiedBy = _UserId;
                arrivalNotification.RecordStatus = "A";
                arrivalNotification.PortCode = _PortCode;

                if (arrivalnotificationdata.GRT > 500)
                {
                    if (arrivalNotification.AppliedDate == null)
                    {
                        throw new BusinessExceptions("Applied Date is Mandatory");
                    }
                }

                #region For Creating of Arrival Agent List Integration
                List<ArrivalAgent> arrivalaagentlist = new List<ArrivalAgent>();
                ArrivalAgent arrivalagent = new ArrivalAgent();
                arrivalagent.AgentID = arrivalNotification.AgentID;
                arrivalagent.VCN = arrivalnotificationdata.VCN;
                arrivalagent.IsPrimary = "Y";
                arrivalaagentlist.Add(arrivalagent);

                if (arrivalnotificationdata.SecondaryAgentID1 > 0)
                {
                    arrivalagent = new ArrivalAgent();
                    arrivalagent.AgentID = arrivalnotificationdata.SecondaryAgentID1;
                    arrivalagent.VCN = arrivalnotificationdata.VCN;
                    arrivalagent.IsPrimary = "F";
                    arrivalaagentlist.Add(arrivalagent);
                }
                if (arrivalnotificationdata.SecondaryAgentID2 > 0)
                {
                    arrivalagent = new ArrivalAgent();
                    arrivalagent.AgentID = arrivalnotificationdata.SecondaryAgentID2;
                    arrivalagent.VCN = arrivalnotificationdata.VCN;
                    arrivalagent.IsPrimary = "S";
                    arrivalaagentlist.Add(arrivalagent);
                }
                arrivalNotification.ArrivalAgents = arrivalaagentlist.ToList();
                #endregion

                //IF block for workflow resubmission updates. 
               
                if (!string.IsNullOrEmpty(arrivalnotificationdata.WokflowFlag))
                {
                    if (arrivalnotificationdata.WokflowFlag == EntityCodes.Arrival_Notification)
                    {
                        var nextsteptaskcode = _workFlowTaskRepository.GetNextStepTaskByEntityandReferance(EntityCodes.Arrival_Notification, arrivalNotification.VCN);
                        ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, arrivalnotificationdata.workflowRemarks);
                        WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, nextsteptaskcode.WorkflowTaskCode);
                    }

                    if (arrivalnotificationdata.WokflowFlag == EntityCodes.IMDGAN)
                    {
                        var nextsteptaskcode = _workFlowTaskRepository.GetNextStepTaskByEntityandReferance(EntityCodes.IMDGAN, arrivalNotification.VCN);

                        IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, arrivalnotificationdata.workflowRemarks);
                        WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, nextsteptaskcode.WorkflowTaskCode);
                    }

                    if (arrivalnotificationdata.WokflowFlag == EntityCodes.ISPSAN)
                    {
                        var nextsteptaskcode = _workFlowTaskRepository.GetNextStepTaskByEntityandReferance(EntityCodes.ISPSAN, arrivalNotification.VCN);

                        ISPSArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, arrivalnotificationdata.workflowRemarks);
                        WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, nextsteptaskcode.WorkflowTaskCode);
                    }


                    if (arrivalnotificationdata.WokflowFlag == EntityCodes.PortHealthAN)
                    {
                        var nextsteptaskcode = _workFlowTaskRepository.GetNextStepTaskByEntityandReferance(EntityCodes.PortHealthAN, arrivalNotification.VCN);
                        var currentTask = _workFlowTaskRepository.GeCurrentTaskByEntityandReferance(EntityCodes.PortHealthAN, arrivalNotification.VCN);

                        PHArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, arrivalnotificationdata.workflowRemarks);
                        WorkFlowEngine<PHArrivalNotificationWorkFlow> wf = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, nextsteptaskcode.WorkflowTaskCode);
                    }

                    if (arrivalnotificationdata.WokflowFlag == EntityCodes.DHMAN)
                    {
                        var nextsteptaskcode = _workFlowTaskRepository.GetNextStepTaskByEntityandReferance(EntityCodes.DHMAN, arrivalNotification.VCN);

                        DHMArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, arrivalnotificationdata.workflowRemarks);
                        WorkFlowEngine<DHMArrivalNotificationWorkFlow> wf = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, nextsteptaskcode.WorkflowTaskCode);
                    }

                    if (arrivalnotificationdata.WasteDeclaration == "A")
                    {
                        foreach (var i in arrivalnotificationdata.WasteDeclarations)
                        {
                            var marpolCode = i.MarpolCode;
                            if (marpolCode == "MRL5")
                            {
                                var nextsteptaskcode = _workFlowTaskRepository.GetNextStepTaskByEntityandReferance(EntityCodes.WasteDeclarationAN, arrivalNotification.VCN);
                                WasteDeclarationArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new WasteDeclarationArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, arrivalnotificationdata.workflowRemarks);
                                WorkFlowEngine<WasteDeclarationArrivalNotificationWorkFlow> dhmwf = new WorkFlowEngine<WasteDeclarationArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                                dhmwf.Process(dhmarrivalNotificationWorkFlow, nextsteptaskcode.WorkflowTaskCode);
                            }
                        }

                    }


                }
                //ELSE block for Normal Arrival notification update.
                else
                {
                    if (arrivalNotification.WorkflowInstanceId != null)
                    {
                        var currentTask = _workFlowTaskRepository.GeCurrentTaskByEntityandReferance(EntityCodes.Arrival_Notification, arrivalNotification.VCN);


                        if (currentTask.WorkflowTaskCode != "WFSA")
                        {
                            ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, "Arrival Notification Updated");
                            WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                            wf.Process(arrivalNotificationWorkFlow, currentTask.WorkflowTaskCode);
                        }
                        else
                        {
                            arrivalnotificationdata.RecordStatus = "A";
                            arrivalNotification = arrivalnotificationdata.MapToEntity();
                            List<ArrivalCommodity> commodityList = arrivalNotification.ArrivalCommodities.ToList();
                            List<ArrivalIMDGTanker> IMDGTankerList = arrivalNotification.ArrivalIMDGTankers.ToList();
                            List<ArrivalDocument> arrivalDocumentList = arrivalNotification.ArrivalDocuments.ToList();
                            List<IMDGInformation> IMDGInformationList = arrivalNotification.IMDGInformations.ToList();
                            List<ArrivalReason> ArrivalReasonList = arrivalNotification.ArrivalReasons.ToList();
                            List<WasteDeclaration> WasteDeclarationList = arrivalNotification.WasteDeclarations.ToList();

                            //TODO: Inline statements to be removed here : Bhoji
                            
                            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalDocument where VCN = @p0", arrivalNotification.VCN);
                           
                            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalReason where VCN = @p0", arrivalNotification.VCN);

                            if (arrivalNotification.Vessel == null)
                                arrivalNotification.Vessel = _unitOfWork.Repository<Vessel>().Find(arrivalNotification.VesselID);

                            if (ArrivalReasonList.Count > 0)
                            {
                                foreach (var reasons in ArrivalReasonList)
                                {
                                    reasons.VCN = arrivalNotification.VCN;
                                    reasons.CreatedBy = arrivalNotification.CreatedBy;
                                    reasons.CreatedDate = arrivalNotification.CreatedDate;
                                    reasons.ModifiedBy = arrivalNotification.ModifiedBy;
                                    reasons.ModifiedDate = arrivalNotification.ModifiedDate;
                                    reasons.RecordStatus = "A";

                                }
                                _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

                            }

                            if (arrivalDocumentList.Count > 0)
                            {
                                foreach (var document in arrivalDocumentList)
                                {
                                    document.VCN = arrivalNotification.VCN;
                                    document.CreatedBy = arrivalNotification.CreatedBy;
                                    document.CreatedDate = arrivalNotification.CreatedDate;
                                    document.ModifiedBy = arrivalNotification.ModifiedBy;
                                    document.ModifiedDate = arrivalNotification.ModifiedDate;
                                    document.RecordStatus = "A";
                                }

                                _unitOfWork.Repository<ArrivalDocument>().InsertRange(arrivalDocumentList);
                            }

                            if (commodityList.Count > 0)
                            {
                                foreach (var commodity in commodityList)
                                {
                                    commodity.VCN = arrivalNotification.VCN;
                                    commodity.CreatedBy = arrivalNotification.CreatedBy;
                                    commodity.CreatedDate = arrivalNotification.CreatedDate;
                                    commodity.ModifiedBy = arrivalNotification.ModifiedBy;
                                    commodity.ModifiedDate = arrivalNotification.ModifiedDate;
                                    commodity.RecordStatus = "A";

                                    if (commodity.ArrivalCommodityID > 0)
                                    {
                                        arrivalNotification.ObjectState = ObjectState.Modified;
                                        _unitOfWork.Repository<ArrivalCommodity>().Update(commodity);
                                    }
                                    else
                                    {
                                        arrivalNotification.ObjectState = ObjectState.Added;
                                        _unitOfWork.Repository<ArrivalCommodity>().Insert(commodity);
                                    }
                                }
                                _unitOfWork.SaveChanges();
                            }

                            if (IMDGTankerList.Count > 0)
                            {
                                foreach (var IMDGTanker in IMDGTankerList)
                                {
                                    IMDGTanker.VCN = arrivalNotification.VCN;
                                    IMDGTanker.CreatedBy = arrivalNotification.CreatedBy;
                                    IMDGTanker.CreatedDate = arrivalNotification.CreatedDate;
                                    IMDGTanker.ModifiedBy = arrivalNotification.ModifiedBy;
                                    IMDGTanker.ModifiedDate = arrivalNotification.ModifiedDate;
                                    IMDGTanker.RecordStatus = "A";

                                    if (IMDGTanker.ArrivalIMDGTankerID > 0)
                                    {
                                        arrivalNotification.ObjectState = ObjectState.Modified;
                                        _unitOfWork.Repository<ArrivalIMDGTanker>().Update(IMDGTanker);
                                    }
                                    else
                                    {
                                        arrivalNotification.ObjectState = ObjectState.Added;
                                        _unitOfWork.Repository<ArrivalIMDGTanker>().Insert(IMDGTanker);
                                    }

                                }
                                _unitOfWork.SaveChanges();
                            }

                            if (IMDGInformationList.Count > 0)
                            {
                                foreach (var IMDGInformation in IMDGInformationList)
                                {
                                    IMDGInformation.VCN = arrivalNotification.VCN;
                                    IMDGInformation.CreatedBy = arrivalNotification.CreatedBy;
                                    IMDGInformation.CreatedDate = arrivalNotification.CreatedDate;
                                    IMDGInformation.ModifiedBy = arrivalNotification.ModifiedBy;
                                    IMDGInformation.ModifiedDate = arrivalNotification.ModifiedDate;
                                    IMDGInformation.RecordStatus = "A";

                                    if (IMDGInformation.IMDGInformationID > 0)
                                    {
                                        arrivalNotification.ObjectState = ObjectState.Modified;
                                        _unitOfWork.Repository<IMDGInformation>().Update(IMDGInformation);
                                    }
                                    else
                                    {
                                        arrivalNotification.ObjectState = ObjectState.Added;
                                        _unitOfWork.Repository<IMDGInformation>().Insert(IMDGInformation);
                                    }

                                }
                                _unitOfWork.SaveChanges();
                            }

                            if (WasteDeclarationList.Count > 0)
                            {
                                foreach (var WasteDeclaration in WasteDeclarationList)
                                {
                                    WasteDeclaration.VCN = arrivalNotification.VCN;
                                    WasteDeclaration.CreatedBy = arrivalNotification.CreatedBy;
                                    WasteDeclaration.CreatedDate = arrivalNotification.CreatedDate;
                                    WasteDeclaration.ModifiedBy = arrivalNotification.ModifiedBy;
                                    WasteDeclaration.ModifiedDate = arrivalNotification.ModifiedDate;
                                    WasteDeclaration.RecordStatus = "A";

                                    if (WasteDeclaration.WasteDeclarationID > 0)
                                    {
                                        arrivalNotification.ObjectState = ObjectState.Modified;
                                        _unitOfWork.Repository<WasteDeclaration>().Update(WasteDeclaration);
                                    }
                                    else
                                    {
                                        arrivalNotification.ObjectState = ObjectState.Added;
                                        _unitOfWork.Repository<WasteDeclaration>().Insert(WasteDeclaration);
                                    }

                                }
                                _unitOfWork.SaveChanges();
                            }



                            arrivalNotification.ObjectState = ObjectState.Modified;
                            _unitOfWork.Repository<ArrivalNotification>().Update(arrivalNotification);
                            _unitOfWork.SaveChanges();

                        }

                    }


                    if (arrivalNotification.WorkflowInstanceId != null && arrivalNotification.AnyDangerousGoodsonBoard == "A")
                    {
                        var prevarrivalnotificationIMDG = _arrivalnotificationRepository.GetArrivalNotificationByVcn(arrivalNotification.VCN);
                        var currentTask = _workFlowTaskRepository.GeCurrentTaskByEntityandReferance(EntityCodes.IMDGAN, arrivalNotification.VCN);
                        var TaskCode = "";

                        if (currentTask == null)
                        {
                            var ctask = _portGeneralConfigsRepository.GetPortConfiguration(_PortCode, ConfigName.WorkflowInitialStatus);
                            TaskCode = ctask;
                        }
                        else
                        {
                            TaskCode = currentTask.WorkflowTaskCode;
                        }

                        if (prevarrivalnotificationIMDG.AnyDangerousGoodsonBoard == "I")
                            _remarks = "New IMDG Arrival Notification";
                        else
                            _remarks = "IMDG Arrival Notification Updated";

                        IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, _remarks);
                        WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, TaskCode);

                    }

                    if (arrivalNotification.WorkflowInstanceId != null && arrivalNotification.AppliedForISPS == "A")
                    {
                        var prevarrivalnotificationISPS = _arrivalnotificationRepository.GetArrivalNotificationByVcn(arrivalNotification.VCN);
                        var currentTask = _workFlowTaskRepository.GeCurrentTaskByEntityandReferance(EntityCodes.ISPSAN, arrivalNotification.VCN);
                        var TaskCode = "";

                        if (currentTask == null)
                        {
                            var ctask = _portGeneralConfigsRepository.GetPortConfiguration(_PortCode, ConfigName.WorkflowInitialStatus);
                            TaskCode = ctask;
                        }
                        else
                        {
                            TaskCode = currentTask.WorkflowTaskCode;
                        }
                        if (prevarrivalnotificationISPS.AnyDangerousGoodsonBoard == "I")
                            _remarks = "New ISPS Arrival Notification";
                        else
                            _remarks = "ISPS Arrival Notification Updated";
                        ISPSArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, _remarks);
                        WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, TaskCode);
                    }

                    //Added by srinivas
                    if (arrivalNotification.WorkflowInstanceId != null && arrivalnotificationdata.WasteDeclaration == "A")
                    {
                        foreach (var i in arrivalnotificationdata.WasteDeclarations)
                        {
                            var marpolCode = i.MarpolCode;
                            if (marpolCode == "MRL5")
                            {
                                _remarks = "Marpol V WasteDecalartion Arrival Notification";
                                var entityid = _entityRepository.GetEntitiesNotification(EntityCodes.WasteDeclarationAN).EntityID;
                                var nextStepCompany = _userRepository.GetUserDetails(_UserId);
                                _notificationRepository.PushMessageToQueue(entityid, arrivalnotificationdata.VCN, _UserId, nextStepCompany, _PortCode, WFStatus.An72, null);
                                WasteDeclarationArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new WasteDeclarationArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, _remarks);
                                WorkFlowEngine<WasteDeclarationArrivalNotificationWorkFlow> dhmwf = new WorkFlowEngine<WasteDeclarationArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, arrivalNotification.CreatedBy);
                                dhmwf.Process(dhmarrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(arrivalNotification.PortCode, ConfigName.WorkflowInitialStatus));
                            }
                        }

                    }


                    if (arrivalNotification.WorkflowInstanceId != null && (arrivalNotification.Tidal == "A" || arrivalNotification.IsSpecialNature == "A"))
                    {
                        var prevarrivalnotificationISPS = _arrivalnotificationRepository.GetArrivalNotificationByVcn(arrivalNotification.VCN);
                        var currentTask = _workFlowTaskRepository.GeCurrentTaskByEntityandReferance(EntityCodes.DHMAN, arrivalNotification.VCN);
                        var TaskCode = "";

                        if (currentTask == null)
                        {
                            var ctask = _portGeneralConfigsRepository.GetPortConfiguration(_PortCode, ConfigName.WorkflowInitialStatus);
                            TaskCode = ctask;
                        }
                        else
                        {
                            TaskCode = currentTask.WorkflowTaskCode;
                        }
                        if (prevarrivalnotificationISPS.AnyDangerousGoodsonBoard == "I")
                            _remarks = "New DHM Arrival Notification";
                        else
                            _remarks = "DHM Arrival Notification Updated";
                        DHMArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, _remarks);
                        WorkFlowEngine<DHMArrivalNotificationWorkFlow> wf = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, TaskCode);
                    }

                    if (arrivalNotification.WorkflowInstanceId != null)
                    {

                        var currentTask = _workFlowTaskRepository.GeCurrentTaskByEntityandReferance(EntityCodes.PortHealthAN, arrivalNotification.VCN);

                        PHArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, "PH Arrival Notification Updated");
                        WorkFlowEngine<PHArrivalNotificationWorkFlow> wf = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(arrivalNotificationWorkFlow, currentTask.WorkflowTaskCode);

                    }


                }            

                return arrivalnotificationdata;

            });
        }


        /// <summary>
        /// To Add ArrivalNotification Draft Data
        /// </summary>
        /// <param name="arrivalnotificationdata"></param>
        /// <returns></returns>
        public ArrivalNotificationVO AddArrivalNotificationDraft(ArrivalNotificationVO arrivalnotificationdata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                ArrivalNotification _arrivalnotification = null;

                arrivalnotificationdata.NominationDate = DateTime.Now;
                arrivalnotificationdata.CellNo = arrivalnotificationdata.CellNo.Replace("(", "").Replace(")", "").Replace("-", "");
                _arrivalnotification = arrivalnotificationdata.MapToEntity();


                try
                {                    
                    var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);

                    if (Isagent != null)
                    {
                        _arrivalnotification.AgentID = Isagent.AgentID;
                    }
                    if (Isagent.AgentID == 1)
                    {
                        throw new BusinessExceptions("Your Session is Expired.Please contact to administrator.");
                    }
                    else
                    {
                        _arrivalnotification.AgentID = 0;
                    }
                    _arrivalnotification.CreatedBy = _UserId;
                    _arrivalnotification.CreatedDate = DateTime.Now;
                    _arrivalnotification.ModifiedBy = _UserId;
                    _arrivalnotification.ModifiedDate = DateTime.Now;
                    _arrivalnotification.RecordStatus = "A";
                }
                catch (Exception ex)
                {
                    throw new FaultException("_LoginName should not be empty, Exception = " + ex);
                }

                _arrivalnotification.PortCode = _PortCode;
                _arrivalnotification.Isdraft = "Y";
                if (!string.IsNullOrEmpty(arrivalnotificationdata.DraftKey))
                {
                    _arrivalnotification.VCN = arrivalnotificationdata.DraftKey;
                    List<ArrivalCommodity> commodityList = _arrivalnotification.ArrivalCommodities.ToList();
                    List<ArrivalIMDGTanker> IMDGTankerList = _arrivalnotification.ArrivalIMDGTankers.ToList();
                    List<ArrivalDocument> arrivalDocumentList = _arrivalnotification.ArrivalDocuments.ToList();
                    List<IMDGInformation> IMDGInformationList = _arrivalnotification.IMDGInformations.ToList();
                    List<ArrivalReason> ArrivalReasonList = _arrivalnotification.ArrivalReasons.ToList();
                    List<WasteDeclaration> WasteDeclarationList = _arrivalnotification.WasteDeclarations.ToList();

                    //TODO: Inline statements to be removed here : Bhoji
                    
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalDocument where VCN = @p0", _arrivalnotification.VCN);

                    if (_arrivalnotification.Vessel == null)
                        _arrivalnotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_arrivalnotification.VesselID);

                    
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalReason where VCN = @p0", _arrivalnotification.VCN);
                    
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalAgent where VCN = @p0", _arrivalnotification.VCN);

                    #region For Creating of Arrival Agent List Integration
                    List<ArrivalAgent> arrivalaagentlist = new List<ArrivalAgent>();
                    ArrivalAgent arrivalagent = new ArrivalAgent();
                    arrivalagent.AgentID = _arrivalnotification.AgentID;
                    arrivalagent.VCN = _arrivalnotification.VCN;
                    arrivalagent.IsPrimary = "Y";
                    arrivalaagentlist.Add(arrivalagent);

                    if (arrivalnotificationdata.SecondaryAgentID1 > 0)
                    {
                        arrivalagent = new ArrivalAgent();
                        arrivalagent.AgentID = arrivalnotificationdata.SecondaryAgentID1;
                        arrivalagent.VCN = arrivalnotificationdata.VCN;
                        arrivalagent.IsPrimary = "F";
                        arrivalaagentlist.Add(arrivalagent);
                    }
                    if (arrivalnotificationdata.SecondaryAgentID2 > 0)
                    {
                        arrivalagent = new ArrivalAgent();
                        arrivalagent.AgentID = arrivalnotificationdata.SecondaryAgentID2;
                        arrivalagent.VCN = arrivalnotificationdata.VCN;
                        arrivalagent.IsPrimary = "S";
                        arrivalaagentlist.Add(arrivalagent);
                    }
                    _unitOfWork.Repository<ArrivalAgent>().InsertRange(arrivalaagentlist);

                    #endregion



                    if (ArrivalReasonList.Count > 0)
                    {
                        foreach (var reasons in ArrivalReasonList)
                        {
                            reasons.VCN = _arrivalnotification.VCN;
                            reasons.CreatedBy = _arrivalnotification.CreatedBy;
                            reasons.CreatedDate = _arrivalnotification.CreatedDate;
                            reasons.ModifiedBy = _arrivalnotification.ModifiedBy;
                            reasons.ModifiedDate = _arrivalnotification.ModifiedDate;
                            reasons.RecordStatus = "A";

                        }
                        _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

                    }


                    if (arrivalDocumentList.Count > 0)
                    {
                        foreach (var document in arrivalDocumentList)
                        {
                            document.VCN = _arrivalnotification.VCN;
                            document.CreatedBy = _arrivalnotification.CreatedBy;
                            document.CreatedDate = _arrivalnotification.CreatedDate;
                            document.ModifiedBy = _arrivalnotification.ModifiedBy;
                            document.ModifiedDate = _arrivalnotification.ModifiedDate;
                            document.RecordStatus = "A";
                        }
                        _unitOfWork.Repository<ArrivalDocument>().InsertRange(arrivalDocumentList);
                    }

                    //TODO: Inline statements to be removed here : Bhoji
                   
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalCommodity where VCN = @p0", _arrivalnotification.VCN);
                    if (commodityList.Count > 0)
                    {
                        foreach (var commodity in commodityList)
                        {
                            commodity.VCN = _arrivalnotification.VCN;
                            commodity.CreatedBy = _arrivalnotification.CreatedBy;
                            commodity.CreatedDate = _arrivalnotification.CreatedDate;
                            commodity.ModifiedBy = _arrivalnotification.ModifiedBy;
                            commodity.ModifiedDate = _arrivalnotification.ModifiedDate;
                            commodity.RecordStatus = "A";
                            _arrivalnotification.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<ArrivalCommodity>().Insert(commodity);
                        }
                        _unitOfWork.SaveChanges();
                    }

                    //var brtcT = 
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalIMDGTanker where VCN = @p0", _arrivalnotification.VCN);
                    if (IMDGTankerList.Count > 0)
                    {
                        foreach (var IMDGTanker in IMDGTankerList)
                        {
                            IMDGTanker.VCN = _arrivalnotification.VCN;
                            IMDGTanker.CreatedBy = _arrivalnotification.CreatedBy;
                            IMDGTanker.CreatedDate = _arrivalnotification.CreatedDate;
                            IMDGTanker.ModifiedBy = _arrivalnotification.ModifiedBy;
                            IMDGTanker.ModifiedDate = _arrivalnotification.ModifiedDate;
                            IMDGTanker.RecordStatus = "A";
                            _arrivalnotification.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<ArrivalIMDGTanker>().Insert(IMDGTanker);
                        }
                        _unitOfWork.SaveChanges();
                    }

                    //var brtI = 
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.IMDGInformation where VCN = @p0", _arrivalnotification.VCN);
                    if (IMDGInformationList.Count > 0)
                    {
                        foreach (var IMDGInformation in IMDGInformationList)
                        {
                            IMDGInformation.VCN = _arrivalnotification.VCN;
                            IMDGInformation.CreatedBy = _arrivalnotification.CreatedBy;
                            IMDGInformation.CreatedDate = _arrivalnotification.CreatedDate;
                            IMDGInformation.ModifiedBy = _arrivalnotification.ModifiedBy;
                            IMDGInformation.ModifiedDate = _arrivalnotification.ModifiedDate;
                            IMDGInformation.RecordStatus = "A";
                            _arrivalnotification.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<IMDGInformation>().Insert(IMDGInformation);
                        }
                        _unitOfWork.SaveChanges();
                    }

                    _unitOfWork.ExecuteSqlCommand(" delete dbo.WasteDeclaration where VCN = @p0", _arrivalnotification.VCN);
                    if (WasteDeclarationList.Count > 0)
                    {
                        foreach (var WasteDeclaration in WasteDeclarationList)
                        {
                            WasteDeclaration.VCN = _arrivalnotification.VCN;
                            WasteDeclaration.CreatedBy = _arrivalnotification.CreatedBy;
                            WasteDeclaration.CreatedDate = _arrivalnotification.CreatedDate;
                            WasteDeclaration.ModifiedBy = _arrivalnotification.ModifiedBy;
                            WasteDeclaration.ModifiedDate = _arrivalnotification.ModifiedDate;
                            WasteDeclaration.RecordStatus = "A";
                            _arrivalnotification.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<WasteDeclaration>().Insert(WasteDeclaration);
                        }
                        _unitOfWork.SaveChanges();
                    }


                    _arrivalnotification.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<ArrivalNotification>().Update(_arrivalnotification);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    //TODO: Raise exception of _LoginName is null or empty.
                    //DONE: Handled while fethcing _UserId in ServiceBase
                    try
                    {                       
                        var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);

                        if (Isagent != null)
                        {
                            _arrivalnotification.AgentID = Isagent.AgentID;
                        }
                        else
                        {
                            _arrivalnotification.AgentID = 0;
                        }

                        _arrivalnotification.CreatedBy = _UserId;
                        _arrivalnotification.CreatedDate = DateTime.Now;
                        _arrivalnotification.ModifiedBy = _UserId;
                        _arrivalnotification.ModifiedDate = DateTime.Now;
                        _arrivalnotification.RecordStatus = "A";
                    }
                    catch (Exception ex)
                    {
                        throw new FaultException("_LoginName should not be empty, Exception = " + ex);
                    }

                    //TODO: Raise exception of _PortCode is null or empty.
                    _arrivalnotification.PortCode = _PortCode;                   

                    #region Arrival Notification Draft

                    CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                    _arrivalnotification.VCN = codeGenerator.GenerateDraft(_arrivalnotification.PortCode);
                    _arrivalnotification.Isdraft = "Y";
                    List<ArrivalCommodity> commodityList = _arrivalnotification.ArrivalCommodities.ToList();
                    List<ArrivalIMDGTanker> IMDGTankerList = _arrivalnotification.ArrivalIMDGTankers.ToList();
                    List<ArrivalDocument> arrivalDocumentList = _arrivalnotification.ArrivalDocuments.ToList();
                    List<IMDGInformation> IMDGInformationList = _arrivalnotification.IMDGInformations.ToList();
                    List<ArrivalReason> ArrivalReasonList = _arrivalnotification.ArrivalReasons.ToList();
                    List<WasteDeclaration> WasteDeclarationList = _arrivalnotification.WasteDeclarations.ToList();
                    //TODO: This is wrong.  Set the AgentID in the front end itself.  Don't set it in on the server side.    
                    //_arrivalnotification.AgentID =  new AgentRepository(_unitOfWork).GetAgentForUser().AgentID; //20;
                    //DONE: specified in ArrivalNotificationService layer based on Logged in user.

                    _arrivalnotification.ArrivalApprovals = null;
                    _arrivalnotification.ArrivalReasons = null;
                    _arrivalnotification.ArrivalCommodities = null;
                    _arrivalnotification.ArrivalDocuments = null;
                    _arrivalnotification.ArrivalIMDGTankers = null;
                    _arrivalnotification.CargoManifests = null;
                    _arrivalnotification.IMDGInformations = null;
                    _arrivalnotification.ServiceRequests = null;
                    _arrivalnotification.StatementFacts = null;
                    _arrivalnotification.SuppServiceRequests = null;
                    _arrivalnotification.VesselAgentChanges = null;
                    _arrivalnotification.VesselArrestImmobilizationSAMSAs = null;
                    _arrivalnotification.VesselCalls = null;
                    _arrivalnotification.VesselCallAnchorages = null;
                    _arrivalnotification.VesselCallMovements = null;
                    _arrivalnotification.VesselETAChanges = null;
                    _arrivalnotification.SuppDryDocks = null;
                    _arrivalnotification.WasteDeclarations = null;

                    if (_arrivalnotification.Vessel == null)
                        _arrivalnotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_arrivalnotification.VesselID);

                    _unitOfWork.Repository<ArrivalNotification>().Insert(_arrivalnotification);


                    #region For Creating of Arrival Agent List Integration
                    List<ArrivalAgent> arrivalaagentlist = new List<ArrivalAgent>();
                    ArrivalAgent arrivalagent = new ArrivalAgent();
                    arrivalagent.AgentID = _arrivalnotification.AgentID;
                    arrivalagent.VCN = _arrivalnotification.VCN;
                    arrivalagent.IsPrimary = "Y";
                    arrivalaagentlist.Add(arrivalagent);

                    if (arrivalnotificationdata.SecondaryAgentID1 > 0)
                    {
                        arrivalagent = new ArrivalAgent();
                        arrivalagent.AgentID = arrivalnotificationdata.SecondaryAgentID1;
                        arrivalagent.VCN = arrivalnotificationdata.VCN;
                        arrivalagent.IsPrimary = "F";
                        arrivalaagentlist.Add(arrivalagent);
                    }
                    if (arrivalnotificationdata.SecondaryAgentID2 > 0)
                    {
                        arrivalagent = new ArrivalAgent();
                        arrivalagent.AgentID = arrivalnotificationdata.SecondaryAgentID2;
                        arrivalagent.VCN = arrivalnotificationdata.VCN;
                        arrivalagent.IsPrimary = "S";
                        arrivalaagentlist.Add(arrivalagent);
                    }
                    _unitOfWork.Repository<ArrivalAgent>().InsertRange(arrivalaagentlist);

                    #endregion


                    if (ArrivalReasonList.Count > 0)
                    {
                        foreach (var reasons in ArrivalReasonList)
                        {
                            reasons.VCN = _arrivalnotification.VCN;
                            reasons.CreatedBy = _arrivalnotification.CreatedBy;
                            reasons.CreatedDate = _arrivalnotification.CreatedDate;
                            reasons.ModifiedBy = _arrivalnotification.ModifiedBy;
                            reasons.ModifiedDate = _arrivalnotification.ModifiedDate;
                            reasons.RecordStatus = "A";

                        }
                        _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

                    }


                    if (commodityList.Count > 0)
                    {
                        foreach (var commodity in commodityList)
                        {
                            commodity.VCN = _arrivalnotification.VCN;
                            commodity.CreatedBy = _arrivalnotification.CreatedBy;
                            commodity.CreatedDate = _arrivalnotification.CreatedDate;
                            commodity.ModifiedBy = _arrivalnotification.ModifiedBy;
                            commodity.ModifiedDate = _arrivalnotification.ModifiedDate;
                            commodity.RecordStatus = "A";
                        }
                        _unitOfWork.Repository<ArrivalCommodity>().InsertRange(commodityList);
                    }

                    if (IMDGTankerList.Count > 0)
                    {
                        foreach (var IMDGTanker in IMDGTankerList)
                        {
                            IMDGTanker.VCN = _arrivalnotification.VCN;
                            IMDGTanker.CreatedBy = _arrivalnotification.CreatedBy;
                            IMDGTanker.CreatedDate = _arrivalnotification.CreatedDate;
                            IMDGTanker.ModifiedBy = _arrivalnotification.ModifiedBy;
                            IMDGTanker.ModifiedDate = _arrivalnotification.ModifiedDate;
                            IMDGTanker.RecordStatus = "A";
                        }
                        _unitOfWork.Repository<ArrivalIMDGTanker>().InsertRange(IMDGTankerList);
                    }

                    if (arrivalDocumentList.Count > 0)
                    {
                        foreach (var document in arrivalDocumentList)
                        {
                            document.VCN = _arrivalnotification.VCN;
                            document.CreatedBy = _arrivalnotification.CreatedBy;
                            document.CreatedDate = _arrivalnotification.CreatedDate;
                            document.ModifiedBy = _arrivalnotification.ModifiedBy;
                            document.ModifiedDate = _arrivalnotification.ModifiedDate;
                            document.RecordStatus = "A";
                            _unitOfWork.Repository<ArrivalDocument>().Insert(document);
                        }
                    }

                    if (IMDGInformationList.Count > 0)
                    {
                        foreach (var IMDGInformation in IMDGInformationList)
                        {
                            IMDGInformation.VCN = _arrivalnotification.VCN;
                            IMDGInformation.CreatedBy = _arrivalnotification.CreatedBy;
                            IMDGInformation.CreatedDate = _arrivalnotification.CreatedDate;
                            IMDGInformation.ModifiedBy = _arrivalnotification.ModifiedBy;
                            IMDGInformation.ModifiedDate = _arrivalnotification.ModifiedDate;
                            IMDGInformation.RecordStatus = "A";
                            _unitOfWork.Repository<IMDGInformation>().Insert(IMDGInformation);
                        }
                    }

                    if (WasteDeclarationList.Count > 0)
                    {
                        foreach (var WasteDeclaration in WasteDeclarationList)
                        {
                            WasteDeclaration.VCN = _arrivalnotification.VCN;
                            WasteDeclaration.CreatedBy = _arrivalnotification.CreatedBy;
                            WasteDeclaration.CreatedDate = _arrivalnotification.CreatedDate;
                            WasteDeclaration.ModifiedBy = _arrivalnotification.ModifiedBy;
                            WasteDeclaration.ModifiedDate = _arrivalnotification.ModifiedDate;
                            WasteDeclaration.RecordStatus = "A";
                            _unitOfWork.Repository<WasteDeclaration>().Insert(WasteDeclaration);
                        }
                    }

                    codeGenerator.UpdateCode("DRFT", _arrivalnotification.PortCode);
                    _unitOfWork.SaveChanges();
                }
                    #endregion

                arrivalnotificationdata.VCN = _arrivalnotification.VCN;
                return arrivalnotificationdata;
            });

        }


        /// <summary>
        /// To Get Pilot Details
        /// </summary>
        /// <param name="PilotID"></param>
        /// <returns></returns>
        public Pilot GetPilotDetails(int PilotID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var objPilotDetails = (from p in _unitOfWork.Repository<Pilot>().Query().Select()
                                       where p.PilotID == PilotID
                                       select p).FirstOrDefault<Pilot>();

                return objPilotDetails;
            });

        }
     

        public IList<ArrivalNotificationGridVO> GetArrivalNotifications(string frmdt, string todt, string vcn, string veselid, string imdg, string isps, string imdgClear, string ispsClear, string phoClear)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes
               
                var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);
                int UserTypeId = 0;
                if (Isagent != null)
                {
                    UserTypeId = Isagent.AgentID;
                }

                var arrivalNotification = _arrivalnotificationRepository.GetArrivalNotificationByPortCodeGrid(frmdt, todt, vcn, veselid, imdg, isps, _PortCode, _UserType, UserTypeId, _UserId, imdgClear, ispsClear, phoClear);
                return arrivalNotification;
            });
        }

        /// <summary>
        /// To Get ArrivalNotification List Data
        /// </summary>
        /// <returns></returns>
        public IList<ArrivalNotificationVO> GetArrivalNotificationSearch(string etaFrom, string etaTo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                
                int UserTypeId = 0;
                var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);

                if (Isagent != null)
                {
                    UserTypeId = Isagent.AgentID;
                }

                var arrivalNotification = _arrivalnotificationRepository.GetArrivalNotificationSearch(_PortCode, _UserType, UserTypeId, _UserId, etaFrom, etaTo);
                return arrivalNotification.MapToDto(_UserType);
            });
        }
        /// <summary>
        /// To Get ArrivalNotification List Data Based on Port
        /// </summary>
        /// <returns></returns>
        public List<ArrivalNotificationVO> GetArrivalNotificationsByPortCode(string portcode)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                int UserTypeId = 0;

                var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);

                if (Isagent != null)
                {
                    UserTypeId = Isagent.AgentID;
                }




                var arrivalNotification = _arrivalnotificationRepository.GetArrivalNotificationByPortCode(_PortCode, _UserType, UserTypeId, _UserId);
                return arrivalNotification.MapToDto(_UserType);
            });
        }

        /// <summary>
        /// To Get ArrivalNotification List Data Based on Agent
        /// </summary>
        /// <returns></returns>
        public List<ArrivalNotificationVO> GetArrivalNotificationsByAgentId(int agentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var arrivalNotification = _arrivalnotificationRepository.GetArrivalNotificationsByAgentId(agentId);
                return arrivalNotification.MapToDto(_UserType);
            });
        }

        /// <summary>
        /// To Get ArrivalNotification List
        /// </summary>
        /// <returns></returns>
        public List<ArrivalNotificationVO> GetArrivalNotifications(string portcode, int agentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var arrivalnotification = _arrivalnotificationRepository.GetArrivalNotificationsByPortCodeAgentId(portcode, agentId);

                return arrivalnotification;
            });
        }
      

        /// <summary>
        /// To Get Arrivalnotification Details
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public ArrivalNotificationVO GetArrivalNotificationVO(string vcn)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                var arrivalNotifications = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
                return arrivalNotifications.MapToDto(_UserType);
            });

        }


        public ArrivalNotificationReferenceVO GetArrivalNotificationDraftReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {

                ArrivalNotificationReferenceVO VO = new ArrivalNotificationReferenceVO();


                var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);

                if (Isagent != null)
                {                    
                    VO.DraftDetails = _arrivalNotificationRepository.GetArrivalNotificationsDrafts(_PortCode, Isagent.AgentID);
                }
                else
                {
                    List<ArrivalNotificationDraftVO> Emptydtls = new List<ArrivalNotificationDraftVO>();
                    ArrivalNotificationDraftVO Emptydtl = new ArrivalNotificationDraftVO();
                    Emptydtl.VCN = "";
                    Emptydtl.VCNdraftDisplay = "";
                    Emptydtls.Add(Emptydtl);
                    VO.DraftDetails = Emptydtls;
                }
                
                return VO;
            });
        }


        /// <summary>
        /// To Get ArrivalNotification Reference data While initialization
        /// </summary>
        /// <returns></returns>
        /// 

        public ArrivalNotificationReferenceVO GetArrivalNotificationBirthReferenceVO(string toid)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                ArrivalNotificationReferenceVO VO = new ArrivalNotificationReferenceVO();
                if (!string.IsNullOrEmpty(toid) && toid != "0")
                {
                    int k = Convert.ToInt32(toid, CultureInfo.InvariantCulture);
                    VO.Berths = _berthRepository.GetBerthsOnTerminalOperator(k);
                }
                else
                {
                    VO.Berths = _berthRepository.GetBerthsForArrival(_PortCode).MapToDtoforArrivalBerths();
                }
                return VO;
            });
        }

        public ArrivalNotificationReferenceVO GetArrivalNotificationReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {

                ArrivalNotificationReferenceVO VO = new ArrivalNotificationReferenceVO();
                VO.agent = _agentRepository.GetAgentForUser(_UserId);




                VO.Berths = _berthRepository.GetBerthsForArrival(_PortCode).MapToDtoforArrivalBerths();


                var cargotypes = _subcategoryRepository.CargoTypes();
                VO.CargoTypes = cargotypes.MapToDtoCodeName();
                VO.Docks = _subcategoryRepository.DockTypes().MapToDtoCodeName();
                VO.Purpose = _subcategoryRepository.Purpose().MapToDtoCodeName();
                VO.ReasonForVisit = _subcategoryRepository.ReasonForVisit().MapToDtoCodeName();
                VO.Uoms = _subcategoryRepository.CargoUoms().MapToDtoCodeName();
                VO.DangerousGoods = _subcategoryRepository.DangerousGoods().MapToDtoCodeName();
                VO.Doctypes = _subcategoryRepository.DocumentsTypes().MapToDtoCodeName();
                VO.Tankers = _subcategoryRepository.TankerTypes().MapToDtoCodeName();
                VO.Commoditys = _subcategoryRepository.Commoditys().MapToDtoCodeName();
                VO.Pilots = _pilotRepository.GetApprovedPilotsList(_PortCode);
                VO.BunkerService = _subcategoryRepository.BunkerService().MapToDtoCodeName();                
                VO.UserDetails = _userRepository.GetUserDetailByID(_UserId);

                VO.Marpol = _arrivalNotificationRepository.Marpol();               
                VO.WasteDclServiceProvider = _licensingRequestRepository.GetWasteDeclarations(_PortCode, SuperCategoryConstants.WASTEDISPOSAL, SuperCategoryConstants.WRKFLOWAPPROVALS);


                VO.DryDocBerths = _berthRepository.GetDryDocBerths(_PortCode);

                var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);

                if (Isagent != null)
                {                    
                    VO.DraftDetails = _arrivalNotificationRepository.GetArrivalNotificationsDrafts(_PortCode, Isagent.AgentID);
                }
                else
                {
                    List<ArrivalNotificationDraftVO> Emptydtls = new List<ArrivalNotificationDraftVO>();
                    ArrivalNotificationDraftVO Emptydtl = new ArrivalNotificationDraftVO();
                    Emptydtl.VCN = "";
                    Emptydtl.VCNdraftDisplay = "";
                    Emptydtls.Add(Emptydtl);
                    VO.DraftDetails = Emptydtls;
                }

                VO.BirthTos = _arrivalNotificationRepository.GetBirthingTo(_PortCode);
                VO.BunkersDetails = _licensingRequestRepository.GetApprovedBunkers(_PortCode, SuperCategoryConstants.BUNKERINGLICENE, SuperCategoryConstants.WRKFLOWAPPROVALS);
                VO.PortDetails = _portRepository.GetAllExceptLoginPort(_PortCode);                
                VO.RefUserType = _arrivalNotificationRepository.IsIspsClearanceRole(_UserId);
                VO.BunkersRequiredDetails = _subcategoryRepository.BunkersRequiredType().MapToDtoCodeName();
                VO.BunkersMethodDetails = _subcategoryRepository.BunkersMethod().MapToDtoCodeName();
                return VO;
            });
        }

        /// <summary>
        /// To Get vesel Details on Vesel Name
        /// </summary>
        /// <param name="VesselID"></param>
        /// <returns></returns>
        public List<VesselVO> VesselDeetailsAutoComplete(string vslname)
        {
            List<VesselVO> vos = new List<VesselVO>();
            return vos;
        }

        /// <summary>
        /// To Get ArrivalNotification List Data
        /// </summary>
        /// <returns></returns>
        public IList<ArrivalNotificationVO> GetArrivalNotification(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var arrivalNotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.VCN == vcn)
                                               .Include(t => t.Vessel)
                                               .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                                               .Include(t => t.Vessel.DockingPlans)
                                               .Include(t => t.VesselCalls)
                                               .Include(t => t.TerminalOperator)
                                               .Include(s => s.Vessel.SubCategory2)
                                               .Include(w => w.Vessel.SubCategory3)
                                               .Include(t => t.ArrivalCommodities)
                                               .Include(t => t.ArrivalIMDGTankers)
                                               .Include(t => t.IMDGInformations)
                                               .Include(t => t.WasteDeclarations)
                                               .Include(t => t.ArrivalDocuments)
                                               .Include(t => t.ArrivalReasons.Select(k => k.SubCategory))
                                               .Include(t => t.ArrivalDocuments.Select(p => p.SubCategory))
                                               .Include(t => t.VesselArrestImmobilizationSAMSAs)
                                               .Include(t => t.User)
                                               .Include(x => x.WorkflowInstance.SubCategory)
                                               .Include(t => t.WorkflowInstance).Select()                                           
                                           select t
                                          ).ToList<ArrivalNotification>();


                int workflowinstanceid;

                workflowinstanceid = Convert.ToInt32(arrivalNotification[0].WorkflowInstanceId);
                WorkflowInstance wfdata = (from wf in _unitOfWork.Repository<WorkflowInstance>().Query(t => t.ReferenceID == vcn && t.WorkflowInstanceId == workflowinstanceid).Include(k => k.SubCategory).Select()
                                           select wf).FirstOrDefault<WorkflowInstance>();

                arrivalNotification.FirstOrDefault().WorkflowInstance = wfdata;
                arrivalNotification.FirstOrDefault().WorkflowInstance.SubCategory = wfdata.SubCategory;
                arrivalNotification.FirstOrDefault().WorkflowInstanceId = wfdata.WorkflowInstanceId;



                return arrivalNotification.MapToDto(_UserType);

            });

        }


        public IList<ArrivalNotificationVO> GetArrivalNotificationForWorkFlow(string vcn, int workflowinstanceid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                WorkflowInstance wfdata = (from wf in _unitOfWork.Repository<WorkflowInstance>().Query(t => t.ReferenceID == vcn && t.WorkflowInstanceId == workflowinstanceid).Include(k => k.SubCategory).Select()
                                           select wf).FirstOrDefault<WorkflowInstance>();

                var arrivalNotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.VCN == vcn)
                                               .Include(t => t.Vessel)
                                               .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                                               .Include(t => t.TerminalOperator)
                                               .Include(s => s.Vessel.SubCategory2)
                                               .Include(w => w.Vessel.SubCategory3)
                                               .Include(t => t.ArrivalCommodities)
                                               .Include(t => t.ArrivalIMDGTankers)
                                               .Include(t => t.IMDGInformations)
                                               .Include(t => t.WasteDeclarations)
                                               .Include(t => t.ArrivalDocuments)
                                               .Include(t => t.ArrivalReasons.Select(k => k.SubCategory))
                                               .Include(t => t.ArrivalDocuments.Select(p => p.SubCategory))
                                               .Include(t => t.VesselArrestImmobilizationSAMSAs)
                                               .Include(x => x.WorkflowInstance.SubCategory)
                                               .Include(t => t.User)
                                               .Include(t => t.WorkflowInstance).Select()                                          
                                           select t
                                          ).ToList<ArrivalNotification>();

                arrivalNotification.FirstOrDefault().WorkflowInstance = wfdata;
                arrivalNotification.FirstOrDefault().WorkflowInstance.SubCategory = wfdata.SubCategory;
                arrivalNotification.FirstOrDefault().WorkflowInstanceId = wfdata.WorkflowInstanceId;

                return arrivalNotification.MapToDto(_UserType);

            });

        }

        #region Arrivalnotification Integration with Notification and workflow related methods

        /// <summary>
        /// Get Requested Status
        /// </summary>
        /// <param name="p_entitycode"></param>
        /// <param name="p_referenceno"></param>
        /// <returns></returns>
        public int GetRequestStatus(string entitycode, string referenceno)
        {
            //12. Move all LINQ code, SQL code in Service classes to Repository classes.
            var _entitycode = _workFlowTaskRepository.GetRequestStatus(entitycode, referenceno);
            return _entitycode;
        }

        /// <summary>
        /// To Approve Arrival Notification Request
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ApproveArrivalNotification(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
             
                ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(arrivalNotificationWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RequestToResubmitArrivalNotification(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
                ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(arrivalNotificationWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// Arrival Notification Request ISPS
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ApproveArrivalNotificationIsps(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);                
                ISPSArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(arrivalNotificationWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition ISPS
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param> 
        public void RequestToResubmitArrivalNotificationIsps(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
              
                ISPSArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(arrivalNotificationWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// Arrival Notification Request IMDG
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ApproveArrivalNotificationImdg(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
                                
                IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(imdgarrivalNotificationWorkFlow, taskcode);
            });
        }

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition IMDG
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RequestToResubmitArrivalNotificationImdg(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
                                
                IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(imdgarrivalNotificationWorkFlow, taskcode);
            });
        }

        /// <summary>
        /// To Update Workflow for Arrival Notification PH
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ApproveArrivalNotificationPH(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
                
                PHArrivalNotificationWorkFlow PHarrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<PHArrivalNotificationWorkFlow> wf = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(PHarrivalNotificationWorkFlow, taskcode);
            });
        }

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for ResubmitionPH
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RequestToResubmitArrivalNotificationPH(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
                
                PHArrivalNotificationWorkFlow PHarrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<PHArrivalNotificationWorkFlow> wf = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(PHarrivalNotificationWorkFlow, taskcode);
            });
        }




        /// <summary>
        /// Arrival Notification Request DHM
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ApproveArrivalNotificationDhm(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
                
                DHMArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DHMArrivalNotificationWorkFlow> wf = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(arrivalNotificationWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition DHM
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param> 
        public void RequestToResubmitArrivalNotificationDhm(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);
                
                DHMArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DHMArrivalNotificationWorkFlow> wf = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(arrivalNotificationWorkFlow, taskcode);

            });
        }




        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ResubmitArrivalNotification(ArrivalNotificationVO arrivalNotificationVO)
        {
            EncloseTransactionAndHandleException(() =>
            {
                ArrivalNotification andata = arrivalNotificationVO.MapToEntity();
                ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, "Resubmission");
                WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>();
                wf.Process(arrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(andata.PortCode, ConfigName.WorkflowInitialStatus));
            });
        }

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition IMDG
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ResubmitArrivalNotificationImdg(ArrivalNotificationVO arrivalNotificationVO)
        {
            EncloseTransactionAndHandleException(() =>
            {
                ArrivalNotification andata = arrivalNotificationVO.MapToEntity();
                IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, andata, "Resubmission for IMDG");
                WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>();
                wf.Process(arrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(andata.PortCode, ConfigName.WorkflowInitialStatus));
            });
        }

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition ISPS
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param> 
        public void ResubmitArrivalNotificationIsps(ArrivalNotificationVO arrivalNotificationVO)
        {
            EncloseTransactionAndHandleException(() =>
            {
                ArrivalNotification andata = arrivalNotificationVO.MapToEntity();
                ISPSArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, andata, "Resubmission for ISPS");
                WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>();
                wf.Process(arrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(andata.PortCode, ConfigName.WorkflowInitialStatus));
            });

        }

        /// <summary>
        /// To Update Workflow for Arrival Notification Request for Resubmition PH
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ResubmitArrivalNotificationPH(ArrivalNotificationVO arrivalNotificationVO)
        {
            EncloseTransactionAndHandleException(() =>
            {
                ArrivalNotification andata = arrivalNotificationVO.MapToEntity();
                PHArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, andata, "Resubmission for PH");
                WorkFlowEngine<PHArrivalNotificationWorkFlow> wf = new WorkFlowEngine<PHArrivalNotificationWorkFlow>();
                wf.Process(arrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(andata.PortCode, ConfigName.WorkflowInitialStatus));
            });
        }



        //////Reject Arrival Notification by Berthplanner

        public void RejectArrivalNotification(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);                
                
                ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(arrivalNotificationWorkFlow, taskcode);

                if (andata.IsPHANFinal == "N")
                {
                    PHArrivalNotificationWorkFlow pharrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<PHArrivalNotificationWorkFlow> wfph = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfph.Process(pharrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsISPSANFinal == "N")
                {
                    ISPSArrivalNotificationWorkFlow ispsarrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wfisps = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfisps.Process(ispsarrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsIMDGANFinal == "N")
                {
                    IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wfimdg = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfimdg.Process(imdgarrivalNotificationWorkFlow, taskcode);
                }

                if (andata.Tidal == "A" || andata.IsSpecialNature == "A")
                {
                    var tidalstatus = _arrivalnotificationRepository.GetTidalWorkflowStatusByVcn(EntityCodes.DHMAN, vcn);
                    if (tidalstatus.WorkflowTaskCode != WFStatus.Approved)
                    {
                        DHMArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                        WorkFlowEngine<DHMArrivalNotificationWorkFlow> wfdhm = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                        wfdhm.Process(dhmarrivalNotificationWorkFlow, taskcode);
                    }
                }

            });
        }

        /// <summary>
        /// ////// Reject Arrival notification by phc
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RejectArrivalNotificationByPhc(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);

                PHArrivalNotificationWorkFlow pharrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<PHArrivalNotificationWorkFlow> wfph = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wfph.Process(pharrivalNotificationWorkFlow, taskcode);


                if (andata.IsANFinal == "N")
                {
                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(arrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsISPSANFinal == "N")
                {
                    ISPSArrivalNotificationWorkFlow ispsarrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wfisps = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfisps.Process(ispsarrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsIMDGANFinal == "N")
                {
                    IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wfimdg = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfimdg.Process(imdgarrivalNotificationWorkFlow, taskcode);
                }
                if (andata.Tidal == "A" || andata.IsSpecialNature == "A")
                {
                    var tidalstatus = _arrivalnotificationRepository.GetTidalWorkflowStatusByVcn(EntityCodes.DHMAN, vcn);
                    if (tidalstatus.WorkflowTaskCode != WFStatus.Approved)
                    {
                        DHMArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                        WorkFlowEngine<DHMArrivalNotificationWorkFlow> wfdhm = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                        wfdhm.Process(dhmarrivalNotificationWorkFlow, taskcode);
                    }
                }
            });
        }

        /// <summary>
        /// ///////////////Reject Arrival notification by ISPS
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RejectArrivalNotificationByIsps(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);

                ISPSArrivalNotificationWorkFlow ispsarrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wfisps = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wfisps.Process(ispsarrivalNotificationWorkFlow, taskcode);

                if (andata.IsANFinal == "N")
                {
                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(arrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsPHANFinal == "N")
                {
                    PHArrivalNotificationWorkFlow pharrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<PHArrivalNotificationWorkFlow> wfph = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfph.Process(pharrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsIMDGANFinal == "N")
                {
                    IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wfimdg = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfimdg.Process(imdgarrivalNotificationWorkFlow, taskcode);
                }
                if (andata.Tidal == "A" || andata.IsSpecialNature == "A")
                {
                    var tidalstatus = _arrivalnotificationRepository.GetTidalWorkflowStatusByVcn(EntityCodes.DHMAN, vcn);
                    if (tidalstatus.WorkflowTaskCode != WFStatus.Approved)
                    {
                        DHMArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                        WorkFlowEngine<DHMArrivalNotificationWorkFlow> wfdhm = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                        wfdhm.Process(dhmarrivalNotificationWorkFlow, taskcode);
                    }
                }
                #region DRYDOCKCancellation
                if (andata.ReasonForVisit == "DRYD")
                {

                    DockingPlan dockres = (from a in _unitOfWork.Repository<DockingPlan>().Query().Select()
                                           where a.VesselID == andata.VesselID && a.RecordStatus == "A"
                                           select a).FirstOrDefault<DockingPlan>();
                    dockres.RecordStatus = "I";
                    dockres.ModifiedBy = _UserId;
                    dockres.ModifiedDate = DateTime.Now;
                    dockres.PortCode = _PortCode;
                    string docremarks = "Cancelled";
                    DockingPlanWorkFlow dockingPlanWorkFlow = new DockingPlanWorkFlow(_unitOfWork, dockres, docremarks);
                    WorkFlowEngine<DockingPlanWorkFlow> wf1 = new WorkFlowEngine<DockingPlanWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf1.Process(dockingPlanWorkFlow, "WFCA");
                }
                #endregion

            });
        }

        /// <summary>
        /// /////////Reject Arrival Notification by IMDG
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RejectArrivalNotificationByImdg(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);

                IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wfimdg = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wfimdg.Process(imdgarrivalNotificationWorkFlow, taskcode);

                if (andata.IsANFinal == "N")
                {
                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(arrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsPHANFinal == "N")
                {
                    PHArrivalNotificationWorkFlow pharrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<PHArrivalNotificationWorkFlow> wfph = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfph.Process(pharrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsISPSANFinal == "N")
                {
                    ISPSArrivalNotificationWorkFlow ispsarrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wfisps = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfisps.Process(ispsarrivalNotificationWorkFlow, taskcode);
                }
                if (andata.Tidal == "A" || andata.IsSpecialNature == "A")
                {
                    var tidalstatus = _arrivalnotificationRepository.GetTidalWorkflowStatusByVcn(EntityCodes.DHMAN, vcn);
                    if (tidalstatus.WorkflowTaskCode != WFStatus.Approved)
                    {
                        DHMArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                        WorkFlowEngine<DHMArrivalNotificationWorkFlow> wfdhm = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                        wfdhm.Process(dhmarrivalNotificationWorkFlow, taskcode);
                    }
                }

            });
        }



        /// <summary>
        /// ///////////////Reject Arrival notification by DHM
        /// </summary>
        /// <param name="VCN"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RejectArrivalNotificationByDhm(string vcn, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);

                DHMArrivalNotificationWorkFlow dhmarrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DHMArrivalNotificationWorkFlow> wfdhm = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wfdhm.Process(dhmarrivalNotificationWorkFlow, taskcode);

                if (andata.IsANFinal == "N")
                {
                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(arrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsPHANFinal == "N")
                {
                    PHArrivalNotificationWorkFlow pharrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<PHArrivalNotificationWorkFlow> wfph = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfph.Process(pharrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsIMDGANFinal == "N")
                {
                    IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wfimdg = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfimdg.Process(imdgarrivalNotificationWorkFlow, taskcode);
                }
                if (andata.IsISPSANFinal == "N")
                {
                    ISPSArrivalNotificationWorkFlow ispsarrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, andata, remarks);
                    WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wfisps = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wfisps.Process(ispsarrivalNotificationWorkFlow, taskcode);
                }
            });
        }


        /// <summary>
        /// To Chnage Workflow Status
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="comments"></param>
        /// <param name="workFlowStatus"></param>
        /// <param name="entityCode"></param>
        public void ChangeWorkFlowStatus(string vcn, string comments, string workFlowStatus, string entityCode)
        {
            ExecuteFaultHandledOperation(() =>
            {
                if (entityCode == _entityCode)
                {
                    //12. Move all LINQ code, SQL code in Service classes to Repository classes.
                    var andata = _arrivalnotificationRepository.GetArrivalNotificationByVcn(vcn);

                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, andata, comments);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>();

                    wf.Process(arrivalNotificationWorkFlow, workFlowStatus);
                }
            });
        }
        #endregion

        /// <summary>
        /// Author   : Sandeep Appana
        /// Date     : 27-8-2014
        /// Purpose  : To Get Arrival Commodity record(s) based on vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<ArrivalCommodityVo> GetArrivalCommoditiesByVcn(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _arrivalnotificationRepository.GetArrivalCommoditiesByVcn(vcn);
            });

        }
       

        public string ArrivalDuplicateValidation(ArrivalNotificationVO arrivalnotificationdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var selectvcn = new SqlParameter("@ArVCN", arrivalnotificationdata.VCN);
                var Starttime = new SqlParameter("@StartDate", arrivalnotificationdata.ETA);
                var Endtime = new SqlParameter("@EndDate", arrivalnotificationdata.ETD);
                var VesselID = new SqlParameter("@VesselID", arrivalnotificationdata.VesselID);
                var PortCode = new SqlParameter("@PortCode", _PortCode);
                var VoyageIn = new SqlParameter("@VoyageIn", arrivalnotificationdata.VoyageIn);
                var VoyageOut = new SqlParameter("@VoyageOut", arrivalnotificationdata.VoyageOut);
                string arrivalDuplicate = _unitOfWork.SqlQuery<string>("dbo.usp_ArrivalNotification_DuplicateValidation @ArVCN, @StartDate, @EndDate,  @VesselID, @PortCode, @VoyageIn, @VoyageOut", selectvcn, Starttime, Endtime, VesselID, PortCode, VoyageIn, VoyageOut).FirstOrDefault();
                
                return arrivalDuplicate;
            });
        }

        public ArrivalNotificationVO CancelArrivalNotification(ArrivalNotificationVO arrivalnotificationdata)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                int userid = _UserId;
               
                string remarks = arrivalnotificationdata.workflowRemarks;
                

                ArrivalNotification arrivalNotification = null;
                arrivalNotification = arrivalnotificationdata.MapToEntity();

                arrivalNotification.CreatedDate = arrivalNotification.CreatedDate;
                arrivalNotification.CreatedBy = arrivalNotification.CreatedBy;
                arrivalNotification.ModifiedDate = arrivalNotification.ModifiedDate;
                arrivalNotification.ModifiedBy = _UserId;
                arrivalNotification.RecordStatus = "I";
                #region DRYDOCKCancellation
                if (arrivalnotificationdata.ReasonForVisit == "DRYD")
                {
                   
                    DockingPlan dockres = (from a in _unitOfWork.Repository<DockingPlan>().Query().Select()
                                           where a.VesselID == arrivalNotification.VesselID && a.RecordStatus=="A"
                                           select a).FirstOrDefault<DockingPlan>();
                    dockres.RecordStatus = "I";
                    dockres.CreatedBy = _UserId;
                    dockres.CreatedDate = DateTime.Now;
                    dockres.ModifiedBy = _UserId;
                    dockres.ModifiedDate = DateTime.Now;
                    dockres.PortCode = _PortCode;
                    string docremarks = "Cancelled";
                    DockingPlanWorkFlow dockingPlanWorkFlow = new DockingPlanWorkFlow(_unitOfWork, dockres, docremarks);
                    WorkFlowEngine<DockingPlanWorkFlow> wf1 = new WorkFlowEngine<DockingPlanWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf1.Process(dockingPlanWorkFlow, "WFCA");
                }
                #endregion
               arrivalNotification.PortCode = _PortCode;
                #region ArrivalNotification Workflow Integration
                ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(arrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.CancelCode));


                #endregion

                #region IMDGArrivalNotification Workflow Integration


                if (arrivalNotification.AnyDangerousGoodsonBoard == "A")
                {

                    var nextsteptaskcode = _workFlowTaskRepository.GetNextStepTaskByEntityandReferance(EntityCodes.IMDGAN, arrivalNotification.VCN);

                    IMDGArrivalNotificationWorkFlow IMDGArrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                    WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wfimdg = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                    wfimdg.Process(IMDGArrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.CancelCode));

                }
                #endregion

                #region ISPS Workflow Integration
                if (arrivalNotification.AppliedForISPS == "A")
                {

                    ISPSArrivalNotificationWorkFlow ISPSArrivalNotificationWorkFlow = new ISPSArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                    WorkFlowEngine<ISPSArrivalNotificationWorkFlow> wfisps = new WorkFlowEngine<ISPSArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                    wfisps.Process(ISPSArrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.CancelCode));

                }
                #endregion

                #region PH Workflow Integration


                PHArrivalNotificationWorkFlow PHArrivalNotificationWorkFlow = new PHArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                WorkFlowEngine<PHArrivalNotificationWorkFlow> wfphc = new WorkFlowEngine<PHArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                wfphc.Process(PHArrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.CancelCode));


                #endregion

                #region DHM Workflow Integration

                if (arrivalNotification.Tidal == "A" || arrivalNotification.IsSpecialNature == "A")
                {

                    DHMArrivalNotificationWorkFlow DHMArrivalNotificationWorkFlow = new DHMArrivalNotificationWorkFlow(_unitOfWork, arrivalNotification, remarks);
                    WorkFlowEngine<DHMArrivalNotificationWorkFlow> wfdhm = new WorkFlowEngine<DHMArrivalNotificationWorkFlow>(_unitOfWork, _PortCode, userid);
                    wfdhm.Process(DHMArrivalNotificationWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.CancelCode));
                }


                #endregion

                ArrivalNotificationVO _objArrivalNotification = new ArrivalNotificationVO();
                _objArrivalNotification.VCN = arrivalnotificationdata.VCN;
                _objArrivalNotification.workflowRemarks = arrivalnotificationdata.workflowRemarks;
                _objArrivalNotification.CreatedBy = _UserId;


                var arrproc = new ArrivalNotificationVO.ArrivalNotificationCancel_proc(_objArrivalNotification);
                
                _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(arrproc);                

                return arrivalnotificationdata;

            });
        }

        /// <summary>
        /// //////Mahesh : autocomplete:
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<VesselVO> GetVesselNamesAN(string searchvalue, string serchcolumn)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _vesselRepository.GetVesselDetailsWitDryDoc(_PortCode, searchvalue, serchcolumn);
            });
        }

        public string ArrivalBerthingRules1(string arrdraft, string preferedberthkey)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return "";
            });
        }

        public string ArrivalBerthingRules2(string arrdraft, string preferedberthkey, string cargotype)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return "";
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }

        }
        /// <summary>
        /// To DIspose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        public List<RevenuePostingVO> GetArrivalVcnDetailsForAutocomplete(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _arrivalnotificationRepository.GetArrivalVcnDetailsForAutocomplete(searchvalue, _PortCode, _UserType, _UserId);
            });
        }

        public List<ArrvWorkflowStatusVo> GetNotificationStatus(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _arrivalnotificationRepository.GetNotificationStatus(vcn);
            });
        }


        public List<AgentVO> GetAgents(string searchvalue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                var Isagent = new AgentRepository(_unitOfWork).GetAgentForUser(_UserId);
                int RefAgid = 0;
                if (Isagent != null)
                {
                    RefAgid = Isagent.AgentID;
                }


                return _agentRepository.GetAllAgentsExceptLogOnAgent(_PortCode, RefAgid, searchvalue);
            });
        }      

        public string ArrivalNotificationVoyageValidation(int vesselid, string voyagein, string voyageout)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _arrivalnotificationRepository.ArrivalNotificationVoyageValidation(vesselid, voyagein, voyageout, _PortCode);
            });

        }     

    }
}

