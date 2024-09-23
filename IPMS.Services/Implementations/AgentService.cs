using Core.Repository;
using IPMS.Core.Repository.Exceptions;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AgentService : ServiceBase, IPMS.Services.IAgentService
    {
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;
        private IAgentRepository _agentRepository;

        /// <summary>
        /// 
        /// </summary>
        public AgentService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _agentRepository = new AgentRepository(_unitOfWork);
        }

        /// <summary>
        /// private ServiceBase login
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AgentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _agentRepository = new AgentRepository(_unitOfWork);
        }

        #region GenerateRefNum
        /// <summary>
        /// function to auto generate VCN Number
        /// </summary>
        /// <returns></returns>
        public string Generaterefnum()
        {
            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int Day = DateTime.Now.Day;

            StringBuilder RefNO = new StringBuilder();

            int count = (from a in _unitOfWork.Repository<Agent>().Query().Select()

                         select a).Count();

            count = count + 1;

            string stMonth = Month.ToString(CultureInfo.InvariantCulture);
            string stDay = Day.ToString(CultureInfo.InvariantCulture);

            if (Month <= 9)
                stMonth = "0" + Month.ToString(CultureInfo.InvariantCulture);
            if (Day <= 9)
                stDay = "0" + Day.ToString(CultureInfo.InvariantCulture);

            RefNO.Append(Year.ToString(CultureInfo.InvariantCulture) + stMonth + stDay + count.ToString(CultureInfo.InvariantCulture));

            return RefNO.ToString();
        }
        #endregion

        #region RegisterAgent
        /// <summary>
        /// for insert and updation agent registration
        /// </summary>
        /// <param name="agentApplicant"></param>
        /// <returns></returns>
        public decimal RegisterAgent(Agent agentApplicant)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                agentApplicant.TelephoneNo1 = agentApplicant.TelephoneNo1.Replace("(", "").Replace(")", "").Replace("-", "");
                agentApplicant.FaxNo = agentApplicant.FaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                agentApplicant.AuthorizedContactPerson.CellularNo = agentApplicant.AuthorizedContactPerson.CellularNo.Replace("(", "").Replace(")", "").Replace("-", "");
                agentApplicant.SubmissionDate = DateTime.Now;

                if (agentApplicant.AgentID == 0)
                {
                    if (agentApplicant.AnonymousUserYn == "N")
                    {
                        agentApplicant.CreatedBy = _UserId;
                        agentApplicant.RecordStatus = "A";
                        agentApplicant.CreatedDate = DateTime.Now;
                        agentApplicant.ModifiedBy = _UserId;
                        agentApplicant.ModifiedDate = DateTime.Now;


                        agentApplicant.ObjectState = ObjectState.Added;
                        agentApplicant.Address.CreatedBy = agentApplicant.CreatedBy;

                        agentApplicant.Address.ModifiedBy = agentApplicant.ModifiedBy;
                        agentApplicant.ReferenceNo = Generaterefnum();
                        agentApplicant.Address.ModifiedDate = DateTime.Now;
                        agentApplicant.Address.CreatedDate = DateTime.Now;

                        _unitOfWork.Repository<Address>().Insert(agentApplicant.Address);
                        _unitOfWork.SaveChanges();

                        agentApplicant.BusinessAddressID = agentApplicant.Address.AddressID;
                        if (agentApplicant.Address1 != null)
                        {
                            agentApplicant.Address1.CreatedBy = agentApplicant.CreatedBy;
                            agentApplicant.Address1.ModifiedBy = agentApplicant.ModifiedBy;
                            agentApplicant.Address1.CreatedDate = DateTime.Now;
                            agentApplicant.Address1.ModifiedDate = DateTime.Now;
                            _unitOfWork.Repository<Address>().Insert(agentApplicant.Address1);
                            _unitOfWork.SaveChanges();
                            agentApplicant.PostalAddressID = agentApplicant.Address1.AddressID;
                        }


                        agentApplicant.AuthorizedContactPerson.CreatedBy = agentApplicant.CreatedBy;
                        agentApplicant.AuthorizedContactPerson.CreatedDate = DateTime.Now;
                        agentApplicant.AuthorizedContactPerson.ModifiedBy = agentApplicant.ModifiedBy;
                        agentApplicant.AuthorizedContactPerson.ModifiedDate = DateTime.Now;

                        _unitOfWork.Repository<AuthorizedContactPerson>().Insert(agentApplicant.AuthorizedContactPerson);
                        _unitOfWork.SaveChanges();
                        agentApplicant.AuthorizedContactPersonID = agentApplicant.AuthorizedContactPerson.AuthorizedContactPersonID;

                        List<AgentPort> applWorkFlow = agentApplicant.AgentPorts.ToList();
                        List<AgentDocument> documents = agentApplicant.AgentDocuments.ToList();
                        agentApplicant.AgentPorts = null;
                        agentApplicant.AuthorizedContactPerson = null;
                        agentApplicant.Address = null;
                        agentApplicant.Address1 = null;
                        agentApplicant.AgentDocuments = null;
                        _unitOfWork.Repository<Agent>().Insert(agentApplicant);
                        _unitOfWork.SaveChanges();

                        PortGeneralConfig validityperiodDet = _unitOfWork.Repository<PortGeneralConfig>().Query().Select().Where(e => e.GroupName == "Reports" && e.PortCode == _PortCode).FirstOrDefault();

                        foreach (var workFlow in applWorkFlow)
                        {
                            workFlow.AgentID = agentApplicant.AgentID;
                            workFlow.CreatedBy = agentApplicant.CreatedBy;
                            workFlow.CreatedDate = DateTime.Now;
                            workFlow.VerifiedDate = DateTime.Now;
                            workFlow.ApprovedDate = DateTime.Now;
                            workFlow.ModifiedBy = agentApplicant.ModifiedBy;
                            workFlow.ModifiedDate = DateTime.Now;
                            workFlow.WFStatus = WFStatus.Approved;

                            if (validityperiodDet != null)
                            {
                                workFlow.FromDate = DateTime.Now;
                                workFlow.ToDate = DateTime.Now.AddYears(Convert.ToInt32(validityperiodDet.ConfigValue, CultureInfo.InvariantCulture));
                            }
                        }
                        _unitOfWork.Repository<AgentPort>().InsertRange(applWorkFlow);
                        _unitOfWork.SaveChanges();
                        foreach (AgentDocument document in documents)
                        {
                            document.CreatedBy = agentApplicant.CreatedBy;
                            document.CreatedDate = DateTime.Now;
                            document.ModifiedBy = agentApplicant.ModifiedBy;
                            document.ModifiedDate = DateTime.Now;

                            document.Document.CreatedBy = agentApplicant.CreatedBy;
                            document.Document.CreatedDate = DateTime.Now;
                            document.Document.ModifiedBy = agentApplicant.ModifiedBy;

                            document.Document.ModifiedDate = DateTime.Now;
                            document.Document.RecordStatus = agentApplicant.RecordStatus;


                            document.AgentID = agentApplicant.AgentID;
                            document.DocumentID = document.Document.DocumentID;
                            document.RecordStatus = document.Document.RecordStatus;
                            document.CreatedBy = agentApplicant.CreatedBy;
                            document.CreatedDate = DateTime.Now;
                            document.ModifiedBy = agentApplicant.ModifiedBy;
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

                    if (agentApplicant.AnonymousUserYn == "Y")
                    {
                        #region Agent Registration Workflow
                        string remarks = "New Agent Port";

                        AgentRegistrationWorkFlow agentRegistrationWorkFlow = new AgentRegistrationWorkFlow(_unitOfWork, agentApplicant, remarks);
                        WorkFlowEngine<AgentRegistrationWorkFlow> wf = new WorkFlowEngine<AgentRegistrationWorkFlow>(_unitOfWork, "", agentApplicant.CreatedBy);
                        wf.Process(agentRegistrationWorkFlow, _portGeneralConfigurationRepository.GetPortConfiguration(agentRegistrationWorkFlow.PortCodes[0].ToString(), ConfigName.WorkflowInitialStatus));

                        #endregion


                    }

                }
                else
                {
                    agentApplicant.ModifiedBy = _UserId;
                    agentApplicant.ModifiedDate = DateTime.Now;

                    if (agentApplicant.AnonymousUserYn != null)
                    { }
                    else
                    { agentApplicant.AnonymousUserYn = "N"; }

                    agentApplicant.ObjectState = ObjectState.Modified;
                    agentApplicant.Address.ModifiedBy = _UserId;
                    agentApplicant.Address.ModifiedDate = DateTime.Now;
                    agentApplicant.Address.CreatedDate = DateTime.Now;

                    _unitOfWork.Repository<Address>().Update(agentApplicant.Address);
                    _unitOfWork.SaveChanges();

                    agentApplicant.Address1.ModifiedBy = _UserId;
                    agentApplicant.Address1.ModifiedDate = DateTime.Now;
                    agentApplicant.Address1.CreatedDate = DateTime.Now;
                    _unitOfWork.Repository<Address>().Update(agentApplicant.Address1);
                    _unitOfWork.SaveChanges();

                    agentApplicant.AuthorizedContactPerson.ModifiedBy = _UserId;
                    agentApplicant.AuthorizedContactPerson.ModifiedDate = DateTime.Now;

                    _unitOfWork.Repository<AuthorizedContactPerson>().Update(agentApplicant.AuthorizedContactPerson);
                    _unitOfWork.SaveChanges();
                    List<AgentDocument> documents = agentApplicant.AgentDocuments.ToList();
                    foreach (AgentDocument document in documents)
                    {
                        document.CreatedBy = agentApplicant.CreatedBy;
                        document.CreatedDate = DateTime.Now;
                        document.ModifiedBy = _UserId;
                        document.ModifiedDate = DateTime.Now;

                        document.Document.CreatedBy = agentApplicant.CreatedBy;
                        document.Document.CreatedDate = DateTime.Now;
                        document.Document.ModifiedBy = _UserId;
                        document.Document.ModifiedDate = DateTime.Now;
                        document.Document.RecordStatus = agentApplicant.RecordStatus;

                        document.AgentID = agentApplicant.AgentID;
                        document.DocumentID = document.Document.DocumentID;

                        document.ModifiedBy = _UserId;
                        document.ModifiedDate = DateTime.Now;
                        document.RecordStatus = agentApplicant.RecordStatus;

                        document.Document.AgentDocuments = null;
                        document.Document.ArrivalDocuments = null;
                        document.Document.SubCategory = null;
                        document.Document.SubCategory1 = null;
                        document.Document = null;
                        document.CreatedBy = agentApplicant.CreatedBy;
                        document.CreatedDate = DateTime.Now;

                        document.ModifiedBy = _UserId;
                        document.CreatedDate = DateTime.Now;


                    }

                    _unitOfWork.ExecuteSqlCommand(" Delete dbo.AgentDocument where AgentID = @p0", agentApplicant.AgentID);

                    _unitOfWork.Repository<AgentDocument>().InsertRange(documents);
                    _unitOfWork.SaveChanges();

                    agentApplicant.Address.ModifiedBy = _UserId;
                    agentApplicant.Address.ModifiedDate = DateTime.Now;

                    agentApplicant.AgentDocuments = null;
                    agentApplicant.Address = null;
                    agentApplicant.Address1 = null;
                    agentApplicant.AuthorizedContactPerson = null;

                    if (!string.IsNullOrEmpty(agentApplicant.ReferenceNo) || agentApplicant.ReferenceNo.Length > 6)
                        agentApplicant.ReferenceNo = agentApplicant.ReferenceNo;
                    else
                        agentApplicant.ReferenceNo = Generaterefnum();

                    var listOfAlreadySelectedPorts = (from a in _unitOfWork.Repository<AgentPort>().Query().Select()
                                                      where a.AgentID == agentApplicant.AgentID
                                                      select a).ToList();
                    if (listOfAlreadySelectedPorts != null)
                    {
                        foreach (var agentPort in listOfAlreadySelectedPorts)
                        {
                            if (!agentApplicant.AgentPorts.Any(cus => cus.PortCode == agentPort.PortCode))
                            {
                                agentPort.RecordStatus = "I";
                                _unitOfWork.Repository<AgentPort>().Update(agentPort);


                                _unitOfWork.SaveChanges();
                            }
                        }
                    }
                    _unitOfWork.Repository<UserRole>().Query().Include(t => t.Role).Select().Where(t => t.UserID == _UserId && t.Role.RoleCode == UserType.ADMIN);

                    var role = from a in _unitOfWork.Repository<UserRole>().Query().Select()
                               join b in _unitOfWork.Repository<Role>().Query().Select() on a.RoleID equals b.RoleID
                               where a.UserID == _UserId && b.RoleCode == UserType.ADMIN
                               select a;

                    if (agentApplicant.AnonymousUserYn != "Y" || role.Count() > 0)
                    {
                        foreach (var agentPort in agentApplicant.AgentPorts)
                        {
                            if ((from a in _unitOfWork.Repository<AgentPort>().Query().Select()
                                 where a.AgentID == agentApplicant.AgentID && a.PortCode == agentPort.PortCode
                                 select a).ToList().Count == 0)
                            {
                                agentPort.VerifiedDate = DateTime.Now;
                                agentPort.CreatedBy = _UserId;
                                agentPort.VerifiedDate = DateTime.Now;
                                agentPort.ApprovedDate = DateTime.Now;
                                agentPort.ModifiedBy = _UserId;
                                agentPort.ModifiedDate = DateTime.Now;
                                agentPort.WFStatus = WFStatus.Approved;

                                _unitOfWork.Repository<AgentPort>().Insert(agentPort);
                                _unitOfWork.SaveChanges();
                            }
                        }
                    }

                    if (agentApplicant.AnonymousUserYn == "Y" && role.Count() == 0)
                    {
                        #region Agent Registration Workflow
                        string remarks = "Add Agent Port";

                        List<AgentPort> applWorkFlow = agentApplicant.AgentPorts.ToList();
                        List<AgentPort> existPorts = _unitOfWork.Repository<AgentPort>().Query().Select().Where(e => e.AgentID == agentApplicant.AgentID).ToList<AgentPort>();
                        List<AgentPort> curentPorts = new List<AgentPort>();

                        foreach (AgentPort a in applWorkFlow)
                        {
                            var b = existPorts.Find(t => t.PortCode == a.PortCode);
                            if (b == null)
                                curentPorts.Add(a);
                        }
                        agentApplicant.AgentPorts = curentPorts;
                        if (curentPorts.Count > 0)
                        {
                            AgentRegistrationWorkFlow agentRegistrationWorkFlow = new AgentRegistrationWorkFlow(_unitOfWork, agentApplicant, remarks);
                            WorkFlowEngine<AgentRegistrationWorkFlow> wf = new WorkFlowEngine<AgentRegistrationWorkFlow>(_unitOfWork, "", agentApplicant.CreatedBy);
                            wf.Process(agentRegistrationWorkFlow, _portGeneralConfigurationRepository.GetPortConfiguration(agentRegistrationWorkFlow.PortCodes[0].ToString(), ConfigName.WorkflowInitialStatus));

                        }

                        #endregion
                    }
                    agentApplicant.AgentPorts = null;
                    _unitOfWork.Repository<Agent>().Update(agentApplicant);
                    _unitOfWork.SaveChanges();

                }


                return agentApplicant.AgentID;



            });
        }
        #endregion

        #region ConvertToDateTime
        /// <summary>
        /// for Converting Date time in the grid
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        private DateTime ConvertToDateTime(string strDateTime)
        {
            DateTime dtFinaldate;
            string sDateTime;
            if (!string.IsNullOrEmpty(strDateTime))
            {
                try
                {
                    dtFinaldate = Convert.ToDateTime(strDateTime, CultureInfo.InvariantCulture);

                }
                catch (Exception)
                {
                    string[] sDate = strDateTime.Split('/');
                    sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                    dtFinaldate = Convert.ToDateTime(sDateTime, CultureInfo.InvariantCulture);
                }
                string dd;
                dd = dtFinaldate.ToString(CultureInfo.InvariantCulture);
                dd = string.Format(CultureInfo.InvariantCulture, "{0:hh:mm:ss tt}", dd);
                return Convert.ToDateTime(dd, CultureInfo.InvariantCulture);
            }
            else
                return DateTime.Now;
        }
        #endregion

        #region GetAgents
        /// <summary>
        /// To get Grid details of the agent registration
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<AgentVO> GetAgents(string status)
        {

            var portCode = new SqlParameter("@PortCode", _PortCode);
            var agentID = new SqlParameter("@AgentID", GetAgentId(_PortCode, _UserId));
            var agentsList = _unitOfWork.SqlQuery<AgentVO>("dbo.[usp_AgentRegistrationGrid] @AgentID,@PortCode", agentID, portCode).ToList();

            return agentsList;
        }
        #endregion

        #region GetAgentID
        /// <summary>
        /// To get agent id
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAgentId(string portcode, int userid)
        {
            var user = (from a in _unitOfWork.Repository<Agent>().Queryable()
                        join u in _unitOfWork.Repository<User>().Queryable() on a.AgentID equals u.UserTypeID
                        join ap in _unitOfWork.Repository<AgentPort>().Queryable() on a.AgentID equals ap.AgentID
                        where ap.PortCode == portcode && u.UserType == GlobalConstants.AGENT && u.UserID == userid
                        select a).FirstOrDefault<Agent>();
            if (user != null)
                return user.AgentID;
            else
                return 0;
        }
        #endregion

        #region GetAgent
        /// <summary>
        /// To Get Agent Registration details based on the Agent Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Agent GetAgent(int agentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var agentList = (from agent in _unitOfWork.Repository<Agent>().Query().Tracking(true).Select()
                                 join agentPort in _unitOfWork.Repository<AgentPort>().Query().Tracking(true).Select() on agent.AgentID equals agentPort.AgentID
                                 join businessAddress in _unitOfWork.Repository<Address>().Query().Tracking(true).Select() on agent.BusinessAddressID equals businessAddress.AddressID
                                 join postalAddress in _unitOfWork.Repository<Address>().Query().Tracking(true).Select() on agent.PostalAddressID equals postalAddress.AddressID
                                 join authorizedConPersonDtl in _unitOfWork.Repository<AuthorizedContactPerson>().Query().Tracking(true).Select() on agent.AuthorizedContactPersonID equals authorizedConPersonDtl.AuthorizedContactPersonID
                                 join agentDocs in _unitOfWork.Repository<AgentDocument>().Query().Select() on agent.AgentID equals agentDocs.AgentID
                                 where agent.RecordStatus == "A" && agent.AgentID == agentId
                                 select agent).FirstOrDefault<Agent>();
                var applattachments = (from a in _unitOfWork.Repository<AgentDocument>().Query().Select()
                                       where a.AgentID == agentId
                                       select a).ToList<AgentDocument>();

                foreach (var attachment in applattachments)
                {
                    DocumentVO documentVo = null;
                    var document = _unitOfWork.SqlQuery<Document>("select * from Document where DocumentID = @p0", attachment.DocumentID).FirstOrDefault<Document>();
                    documentVo = document.MapToDTO();
                    document = documentVo.MapToEntity();
                    attachment.Document = document;
                }

                agentList.AgentDocuments = applattachments;
                agentList.Address1.User1 = null;
                agentList.Address1.User = null;
                agentList.Address.User1 = null;
                agentList.Address.User = null;
                
                return agentList;
            });
        }
        #endregion

        #region GetzAgent
        /// <summary>
        /// To Get Agent Registration details based on the Agent Id for Pending Tasks view
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public Agent GetzAgent(string vcn)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(vcn))
            {
                id = Convert.ToInt32(vcn, CultureInfo.InvariantCulture);
            }
            return ExecuteFaultHandledOperation(() =>
            {
                AgentVO agentVo = null;
                var applicant = (from a in _unitOfWork.Repository<Agent>().Query().Select()
                                 where a.AgentID == id
                                 select a).FirstOrDefault<Agent>();
                agentVo = applicant.MapToDTO();
                applicant = agentVo.MapToEntity();
                AddressVO address1Vo = null;
                var address = (from a in _unitOfWork.Repository<Address>().Query().Select()
                               where a.AddressID == applicant.BusinessAddressID
                               select a).FirstOrDefault<Address>();
                address1Vo = address.MapToDTO();
                address = address1Vo.MapToEntity();
                var address1 = new Address();
                AddressVO address2Vo = null;
                if (applicant.PostalAddressID != null)
                {
                    address1 = (from a in _unitOfWork.Repository<Address>().Query().Select()
                                where a.AddressID == applicant.PostalAddressID
                                select a).FirstOrDefault<Address>();
                    address2Vo = address1.MapToDTO();
                    address1 = address2Vo.MapToEntity();
                }
                AuthorizedContactPersonVO authContactDetailsVo = null;
                var contactDetails = (from a in _unitOfWork.Repository<AuthorizedContactPerson>().Query().Select()
                                      where a.AuthorizedContactPersonID == applicant.AuthorizedContactPersonID
                                      select a).FirstOrDefault<AuthorizedContactPerson>();


                authContactDetailsVo = contactDetails.MapToDTO();
                contactDetails = authContactDetailsVo.MapToEntity();
                List<AgentPortsVO> agentPortsVo = null;
                var agentPorts = (from a in _unitOfWork.Repository<AgentPort>().Query().Select()
                                  where a.AgentID == applicant.AgentID && a.RecordStatus == "A"
                                  select a).ToList<AgentPort>();
                agentPortsVo = agentPorts.MapToListDTO();
                agentPorts = agentPortsVo.MapToListEntity();

                var workflowinstanceID = agentPorts.Where(e => e.PortCode == _PortCode);

                agentVo.WorkflowInstanceId = workflowinstanceID != null ? workflowinstanceID.FirstOrDefault<AgentPort>().WorkflowInstanceId : default(int);

                agentVo.FromDate = workflowinstanceID != null ? workflowinstanceID.FirstOrDefault<AgentPort>().FromDate : null;
                agentVo.ToDate = workflowinstanceID != null ? workflowinstanceID.FirstOrDefault<AgentPort>().ToDate : null;

                List<AgentDocumentsVO> documentsVo = null;
                var applattachments = (from a in _unitOfWork.Repository<AgentDocument>().Query().Select()
                                       where a.AgentID == applicant.AgentID
                                       select a).ToList<AgentDocument>();
                foreach (var attachment in applattachments)
                {
                    DocumentVO documentVo = null;

                    var document = _unitOfWork.SqlQuery<Document>("select * from Document where DocumentID = @p0", attachment.DocumentID).FirstOrDefault<Document>();

                    documentVo = document.MapToDTO();
                    document = documentVo.MapToEntity();
                    attachment.Document = document;
                }
                documentsVo = applattachments.MapToListDTO();
                applattachments = documentsVo.MapToListEntity();
                applicant.Address = address;
                applicant.Address1 = address1;
                applicant.AgentPorts = agentPorts;
                applicant.AuthorizedContactPerson = contactDetails;
                applicant.AgentDocuments = applattachments;
                applicant.WorkflowInstanceId = agentVo.WorkflowInstanceId;
                applicant.ReferenceNo = applicant.ReferenceNo;
                return applicant;
            });

        }
        #endregion

        #region VerifyAgent
        /// <summary>
        /// To Verify Agent Registration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Agent VerifyAgent(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Agent applicantDetails = _unitOfWork.Repository<Agent>().Find(id);

                applicantDetails.ObjectState = ObjectState.Modified;
                if (applicantDetails.RecordStatus == "N")
                {
                    applicantDetails.RecordStatus = "V";
                    _unitOfWork.Repository<Agent>().Update(applicantDetails);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    applicantDetails.RecordStatus = "A";
                    _unitOfWork.Repository<Agent>().Update(applicantDetails);
                }

                return applicantDetails;
            });
        }
        #endregion

        #region PutRejectAgent
        /// <summary>
        /// To Reject Agent Registration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool PutRejectAgent(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                bool result = false;

                Agent applicantDetails = _unitOfWork.Repository<Agent>().Find(id);
                applicantDetails.RecordStatus = "R";
                _unitOfWork.Repository<Agent>().Update(applicantDetails);
                _unitOfWork.SaveChanges();
                result = true;

                return result;
            });
        }
        #endregion

        #region InactiveAgent
        /// <summary>
        /// To Inactive the Agent Registration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool InactiveAgent(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                bool result = false;

                Agent applicantDetails = _unitOfWork.Repository<Agent>().Find(id);
                applicantDetails.RecordStatus = "I";
                _unitOfWork.Repository<Agent>().Update(applicantDetails);
                _unitOfWork.SaveChanges();
                result = true;

                return result;
            });
        }
        #endregion

        #region GetDocumentTypes
        /// <summary>
        /// To Get Document types of the Agent Registration
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDocumentTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var docTypes = from s in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable()
                               where s.SupCatCode == "DCTY"
                               select s;
                return docTypes.ToList();
            });
        }
        #endregion

        #region CheckForRegCombinationExistance
        /// <summary>
        /// To check for Reg. No for Uniqueness
        /// </summary>
        /// <param name="regNo"></param>
        /// <returns></returns>
        public int CheckForRegCombinationExistance(string regNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var agentsList = from a in _unitOfWork.Repository<Agent>().Query().Select().AsEnumerable()
                                 where a.RegistrationNumber.ToUpperInvariant() == regNo.ToUpperInvariant()
                                 select a;
                return agentsList.ToList().Count;
            });
        }
        #endregion

        #region CheckForVatCombinationExistance
        /// <summary>
        /// To check for Vat No for Uniqueness
        /// </summary>
        /// <param name="vatNo"></param>
        /// <returns></returns>
        public int CheckForVatCombinationExistance(string vatNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var agentsList = from a in _unitOfWork.Repository<Agent>().Query().Select().AsEnumerable()
                                 where a.VATNumber.ToUpperInvariant() == vatNo.ToUpperInvariant()
                                 select a;
                return agentsList.ToList().Count;
            });
        }
        #endregion

        #region CheckForTaxCombinationExistance
        /// <summary>
        /// To check for Tax No for Uniqueness
        /// </summary>
        /// <param name="incTaxNo"></param>
        /// <returns></returns>
        public int CheckForTaxCombinationExistance(string incTaxNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var agentsList = from a in _unitOfWork.Repository<Agent>().Query().Select().AsEnumerable()
                                 where a.IncomeTaxNumber.ToUpperInvariant() == incTaxNo.ToUpperInvariant()
                                 select a;
                return agentsList.ToList().Count;
            });
        }
        #endregion

        #region Workflow Integration Methods
        /// <summary>
        /// Verification of the anonymous agent registration in work flow
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void VerifyAgentRegistration(string agentId, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var agent = _unitOfWork.SqlQuery<Agent>("select * from agent where agentid = @p0", agentId).FirstOrDefault<Agent>();
                var agentports = _unitOfWork.SqlQuery<AgentPort>("select * from AgentPort where agentid = @p0 and PortCode=@p1", agentId, _PortCode).ToList<AgentPort>();
                agent.AgentPorts = agentports;
                agent.SubmissionDate = agent.CreatedDate;
                AgentRegistrationWorkFlow agentRegistrationWorkFlow = new AgentRegistrationWorkFlow(_unitOfWork, agent, remarks);
                WorkFlowEngine<AgentRegistrationWorkFlow> wf = new WorkFlowEngine<AgentRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(agentRegistrationWorkFlow, taskcode);

                PortGeneralConfig validityperiodDet = _unitOfWork.Repository<PortGeneralConfig>().Query().Select().Where(e => e.GroupName == "Reports" && e.PortCode == _PortCode).FirstOrDefault();

                if (validityperiodDet != null)
                    _unitOfWork.ExecuteSqlCommand(" update dbo.AgentPort SET FromDate =  @p0, ToDate=@p1 , ModifiedBy = @p2,ModifiedDate = @p3, WFStatus=@p6  where agentID = @p4 and Portcode=@p5", DateTime.Now, DateTime.Now.AddYears(Convert.ToInt32(validityperiodDet.ConfigValue, CultureInfo.InvariantCulture)), _UserId, DateTime.Now, agentId, _PortCode, taskcode);

            });
        }
        /// <summary>
        /// Approval of the anonymous agent registration in work flow
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void ApproveAgentRegistration(string agentId, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var agent = _unitOfWork.SqlQuery<Agent>("select * from agent where agentid = @p0", agentId).FirstOrDefault<Agent>();
                var agentports = _unitOfWork.SqlQuery<AgentPort>("select * from AgentPort where agentid = @p0 and PortCode=@p1", agentId, _PortCode).ToList<AgentPort>();
                agent.AgentPorts = agentports;
                agent.SubmissionDate = agent.CreatedDate;
                AgentRegistrationWorkFlow agentRegistrationWorkFlow = new AgentRegistrationWorkFlow(_unitOfWork, agent, remarks);
                WorkFlowEngine<AgentRegistrationWorkFlow> wf = new WorkFlowEngine<AgentRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(agentRegistrationWorkFlow, taskcode);


                PortGeneralConfig validityperiodDet = _unitOfWork.Repository<PortGeneralConfig>().Query().Select().Where(e => e.GroupName == "Reports" && e.PortCode == _PortCode).FirstOrDefault();

                if (validityperiodDet != null)
                    _unitOfWork.ExecuteSqlCommand(" update dbo.AgentPort SET FromDate =  @p0, ToDate=@p1 , ModifiedBy = @p2,ModifiedDate = @p3, WFStatus=@p6  where agentID = @p4 and Portcode=@p5", DateTime.Now, DateTime.Now.AddYears(Convert.ToInt32(validityperiodDet.ConfigValue, CultureInfo.InvariantCulture)), _UserId, DateTime.Now, agentId, _PortCode, taskcode);


            });
        }
        /// <summary>
        /// Reject of the anonymous agent registration in work flow
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="Remarks"></param>
        /// <param name="taskcode"></param>
        public void RejectAgentRegistration(string agentId, string Remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var agent = _unitOfWork.SqlQuery<Agent>("select * from agent where agentid = @p0", agentId).FirstOrDefault<Agent>();
                var agentports = _unitOfWork.SqlQuery<AgentPort>("select * from AgentPort where agentid = @p0 and PortCode=@p1", agentId, _PortCode).ToList<AgentPort>();
                agent.AgentPorts = agentports;
                agent.SubmissionDate = agent.CreatedDate;

                AgentRegistrationWorkFlow agentRegistrationWorkFlow = new AgentRegistrationWorkFlow(_unitOfWork, agent, Remarks);
                WorkFlowEngine<AgentRegistrationWorkFlow> wf = new WorkFlowEngine<AgentRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(agentRegistrationWorkFlow, taskcode);

            });
        }
        #endregion

        #region GetAllAgents
        /// <summary>
        /// for Json Result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public List<UserMasterVO> GetAllAgents()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _agentRepository.GetAllAgents(_PortCode);
            });
        }
        #endregion

        #region GetAgentDetailsInVesselCallByVCN
        public AgentVO GetAgentDetailsInVesselCallByVcn(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _agentRepository.GetAgentDetailsInVesselCallByVcn(vcn);
            });
        }
        #endregion

        #region GetAgentportbasedAccount
        public List<PortCodeNameVO> GetAgentportbasedAccount(int agentid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _agentRepository.GetAgentportbasedAccount(agentid, _PortCode);
            });
        }
        #endregion

        #region AddAgentAccountDetails
        public int AddAgentAccountDetails(AgentVO agentAccountDetails)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<AgentAccount> agentAccountList = agentAccountDetails.AgentAccountVO.MapToEntity();

                var usedAgentAccounts = (from a in _unitOfWork.Repository<AgentAccount>().Queryable().Where(a => a.AgentID == agentAccountDetails.AgentID && a.PortCode != _PortCode) select a).ToList().GroupBy(x => x.AccountNo).Select(y=> y.First().AccountNo.ToUpperInvariant());
                
                foreach (var agentAccount in agentAccountList)
                {
                    var account = usedAgentAccounts.Contains(agentAccount.AccountNo.ToUpperInvariant());
                    if (!account)
                    {
                        agentAccount.AgentID = agentAccountDetails.AgentID;
                        agentAccount.CreatedBy = _UserId;
                        agentAccount.ModifiedBy = _UserId;
                        agentAccount.CreatedDate = DateTime.Now;
                        agentAccount.ModifiedDate = DateTime.Now;
                        if (agentAccount.AgentAccountID > 0)
                        {
                            agentAccount.ObjectState = ObjectState.Modified;
                            _unitOfWork.Repository<AgentAccount>().Update(agentAccount);
                        }
                        else
                        {
                            agentAccount.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<AgentAccount>().Insert(agentAccount);
                        }
                    }
                    else
                    {
                        throw new BusinessExceptions("Account Number: " + agentAccount.AccountNo + " is already exist in another port");
                    }
                }
                _unitOfWork.SaveChanges();
                return 1;
            });

        }
        #endregion

        #region GetAgentAccountDetails
        public List<AgentAccountVO> GetAgentAccountDetails(int agentid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _agentRepository.GetAgentAccountDetailsbyAgentId(agentid, _PortCode).MapToDTO();
            });
        }
        #endregion


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }

        }
        /// <summary>
        /// For Disposal of the all used services
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
