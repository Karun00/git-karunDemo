using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using IPMS.Domain;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class DivingRequestService : ServiceBase, IDivingRequestService
    {
        private IPortConfigurationRepository _portConfigurationRepository;
        private IUserRepository _userRepository;
        private INotificationPublisher _notificationpublisher;
        private IEntityRepository _entity;
        IDivingRequestRepository _DivingRequestRepository;
        private ISubCategoryRepository _subCategoryRepository;
        private IAccountRepository _accountRepository;

        public DivingRequestService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _DivingRequestRepository = new DivingRequestRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        public DivingRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _DivingRequestRepository = new DivingRequestRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);
        }

        /// <summary>
        /// Gets other locations list
        /// </summary>
        /// <returns></returns>
        public List<LocationVO> GetOtherLocations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetOtherLocations(_PortCode);
            });
        }

        /// <summary>
        /// Gets Quay list
        /// </summary>
        /// <returns></returns>
        public List<QuayVO> GetPortQuays()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetPortQuays(_PortCode);
            });
        }

        /// <summary>
        /// GEts Berth list by Quay
        /// </summary>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        public List<BerthVO> GetQuayBerths(string quayCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetQuayBerths(_PortCode, quayCode);
            });
        }

        /// <summary>
        /// Gets Bollards List
        /// </summary>
        /// <param name="quayCode"></param>
        /// <param name="berthCode"></param>
        /// <returns></returns>
        public List<BollardVO> GetBerthBollards(string quayCode, string berthCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetBerthBollards(_PortCode, quayCode, berthCode);
            });
        }

        /// <summary>
        /// Gets all diving request list
        /// </summary>
        /// <returns></returns>
        public List<DivingRequestVO> GetAllDivingRequests()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetAllDivingRequests(_PortCode);
            });
        }

        /// <summary>
        /// To Get diving requests for scrolling
        /// </summary>
        /// <returns></returns>
        public List<DivingRequestVO> GetDivingrequestsForScroll()
        {
            DateTime currentDatetime = DateTime.Now;
            string PortCodeByLogin = string.Empty;


            return ExecuteFaultHandledOperation(() =>
            {
                if (_PortCode != "")
                {
                    PortCodeByLogin = _PortCode;
                }
                else
                {
                    int userid = _accountRepository.GetUserId(_LoginName);
                    PortCodeByLogin = (from e in _unitOfWork.Repository<UserPort>().Queryable()
                                       where userid == e.UserID && e.RecordStatus == RecordStatus.Active
                                       select e.PortCode).FirstOrDefault();
                }
                return _DivingRequestRepository.GetDivingRequestsForScroll(PortCodeByLogin);
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To Get Diving Tas kExecution details
        /// </summary>
        /// <returns></returns>
        public List<DivingRequestVO> GetAllDivingTaskExecutions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetAllDivingTaskExecutions(_PortCode);
            });
        }

        /// <summary>
        ///  To Get all the Locations
        /// </summary>
        /// <returns></returns>
        public List<LocationVO> GetAllLocations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetAllLocations(_PortCode);
            });
        }

        /// <summary>
        /// GetDivingRequestOccupationById
        /// </summary>
        /// <param name="divingRequestId"></param>
        /// <returns></returns>
        public DivingRequestVO GetDivingRequestOccupationById(int divingRequestId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetDivingRequestOccupationById(_PortCode, divingRequestId);
            });
        }

        /// <summary>
        ///  Gets All Diviving Request Ocupation LIST / Details
        /// </summary>
        /// <returns></returns>
        public List<DivingRequestVO> GetAllDivingRequestOccupation()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetAllDivingRequestOccupation(_PortCode);
            });
        }

        /// <summary>
        /// Gets Diviving Request Ocupation Info by DivingRequestID
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public List<DivingRequestVO> GetDivingRequestByIdView(int requestId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.GetDivingRequestByIdView(requestId);
            });
        }

        /// <summary>
        /// GetLoggedInUserName
        /// </summary>
        /// <returns></returns>
        public string GetLoggedInUserName()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var username = _userRepository.GetUserByUserID(_UserId);
                return username.FirstName + " " + username.LastName;
            });
        }

        public List<SubCategoryCodeNameVO> GetDivingRequestReasons()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _subCategoryRepository.GetSubCategoryDetailsBySupCatCode(SuperCategoryConstants.Diving_Request_Reason);
            });
        }

        /// <summary>
        /// Add / Inserts the new Diving Request
        /// </summary>
        /// <param name="divingRequest"></param>
        /// <returns></returns>
        public DivingRequestVO AddDivingRequest(DivingRequestVO divingRequest)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _DivingRequestRepository.AddDivingRequest(divingRequest, _UserId, _PortCode);
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To Modify/Update Diving Checklist Data 
        /// </summary>
        /// <param name="divingRequestvo"></param>
        /// <returns></returns>
        public DivingRequestVO ModifyDivingCheckList(DivingRequestVO divingRequestvo)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _DivingRequestRepository.ModifyDivingChecklist(divingRequestvo, _UserId);
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To  Modify/Update Diving Task Execution Data
        /// </summary>
        /// <param name="divingRequestvo"></param>
        /// <returns></returns>
        public DivingRequestVO ModifyDivingTaskExecution(DivingRequestVO divingRequestvo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                if (divingRequestvo.StopTime != null)
                {
                    var _user = _userRepository.GetUserById(_UserId);
                    CompanyVO nextStepCompany = new CompanyVO();
                    nextStepCompany.UserType = _user.UserType;
                    nextStepCompany.UserTypeId = _user.UserTypeID;
                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.DivingRequestOnCompletion).EntityID, divingRequestvo.DivingRequestID.ToString(System.Globalization.CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Confirmed);
                }
                return _DivingRequestRepository.ModifyDivingTaskExecution(divingRequestvo, _UserId);
            });
        }

        #region ModifyDivingRequestOccupation
        /// <summary>
        /// Modifies / Updates the Diving Request Ocupation Details
        /// </summary>
        /// <param name="divingRequestvo"></param>
        /// <returns></returns>
        public DivingRequestVO ModifyDivingRequestOccupation(DivingRequestVO divingRequestvo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                DivingRequest divingrequest = new DivingRequest();
                divingrequest = DivingRequestMapExtension.MapToEntity(divingRequestvo);

                divingrequest.RecordStatus = "A";

                if (divingrequest.LocationType == "Other Location")
                {
                    divingrequest.LocationType = "O";
                }
                else
                {
                    divingrequest.LocationType = "Q";
                }

                // For other location, Port Code will not be available
                if (divingrequest.FromPortCode == null)
                {
                    divingrequest.FromPortCode = _PortCode;
                }
                else if (string.IsNullOrEmpty(divingrequest.FromPortCode)) //if (divingrequest.FromPortCode == "")
                {
                    divingrequest.FromPortCode = _PortCode;
                }

                divingrequest.ModifiedDate = DateTime.Now;
                divingrequest.ModifiedBy = _UserId;
                divingrequest.ObjectState = ObjectState.Modified;

                int res = _unitOfWork.ExecuteSqlCommand("update dbo.DivingRequest set OcupationFromDate = @p1 ,OcupationToDate = @p2,HoursOfOccupation1 = @p3, ModifiedBy= @p4 ,   ModifiedDate= @p5 ,RecordStatus= 'A' where DivingRequestID = @p0", divingrequest.DivingRequestID, divingrequest.OcupationFromDate, divingrequest.OcupationToDate, divingrequest.HoursOfOccupation1, divingrequest.ModifiedBy, divingrequest.ModifiedDate);

                #region Workflow Integration

                string remarks = "Diving Request Occupation";
                #region User Registration Workflow

                DivingRequestOccupationWorkFlow divingRequestWorkFlow = new DivingRequestOccupationWorkFlow(_unitOfWork, divingrequest, remarks);
                WorkFlowEngine<DivingRequestOccupationWorkFlow> wf = new WorkFlowEngine<DivingRequestOccupationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(divingRequestWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                var an = _unitOfWork.Repository<DivingOccupationApproval>().Find(Convert.ToInt32(divingRequestWorkFlow.ReferenceId, System.Globalization.CultureInfo.InvariantCulture));
                #endregion

                #endregion

                return divingRequestvo;
            });
        }
        #endregion

        #region AddDivingRequestOccupation
        /// <summary>
        /// Adds Diving Request  Occupation
        /// </summary>
        /// <param name="divingRequestData"></param>
        /// <returns></returns>
        public DivingRequest AddDivingRequestOccupation(DivingRequest divingRequestData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                #region Workflow Integration
                string remarks = "Diving Request Occupation";

                #region Diving Request Occupation

                DivingRequestOccupationWorkFlow divingRequestOccupationWorkFlow = new DivingRequestOccupationWorkFlow(_unitOfWork, divingRequestData, remarks);
                WorkFlowEngine<DivingRequestOccupationWorkFlow> wf = new WorkFlowEngine<DivingRequestOccupationWorkFlow>(_unitOfWork, "", divingRequestData.CreatedBy);
                wf.Process(divingRequestOccupationWorkFlow, _portConfigurationRepository.GetPortConfiguration(divingRequestOccupationWorkFlow.PortCodes[0].ToString()).WorkFlowInitialStatus);
                var an = _unitOfWork.Repository<DivingOccupationApproval>().Find(Convert.ToInt32(divingRequestOccupationWorkFlow.ReferenceId, CultureInfo.InvariantCulture));
                an.ObjectState = ObjectState.Modified;

                an.WorkflowInstanceID = wf.GetWorkFlowInstance(divingRequestOccupationWorkFlow).WorkflowInstanceId;
                _unitOfWork.Repository<DivingOccupationApproval>().Update(an);

                #endregion

                #endregion
                return divingRequestData;
            });
        }
        #endregion

        #region Workflow Tasks
        /// <summary>
        ///  Verifies the Diving Request Occupation
        /// </summary>
        /// <param name="divingRequestId"></param>
        /// <param name="comments"></param>
        /// <param name="taskCode"></param>
        public void VerifyDivingRequestOccupation(string divingRequestId, string comments, string taskCode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var divingrequestOccupation = _unitOfWork.SqlQuery<DivingRequest>("select * from DivingRequest where divingRequestId = @p0", divingRequestId).FirstOrDefault<DivingRequest>();
                var divingOccupationApprovals = _unitOfWork.SqlQuery<DivingOccupationApproval>("select * from DivingOccupationApproval where divingRequestId = @p0 ", divingRequestId, _PortCode).ToList<DivingOccupationApproval>();
                divingrequestOccupation.DivingOccupationApprovals = divingOccupationApprovals;

                DivingRequestOccupationWorkFlow DivingRequestOccupationWorkFlow = new DivingRequestOccupationWorkFlow(_unitOfWork, divingrequestOccupation, comments);
                WorkFlowEngine<DivingRequestOccupationWorkFlow> wf = new WorkFlowEngine<DivingRequestOccupationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(DivingRequestOccupationWorkFlow, taskCode);
            });
        }

        /// <summary>
        /// Approves the Diving Request Occupation
        /// </summary>
        /// <param name="divingRequestId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveDivingRequestOccupation(string divingRequestId, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var divingrequestOccupation = _unitOfWork.SqlQuery<DivingRequest>("select * from DivingRequest where divingRequestId = @p0", divingRequestId).FirstOrDefault<DivingRequest>();
                var divingOccupationApprovals = _unitOfWork.SqlQuery<DivingOccupationApproval>("select * from DivingOccupationApproval where divingRequestId = @p0 ", divingRequestId, _PortCode).ToList<DivingOccupationApproval>();
                divingrequestOccupation.DivingOccupationApprovals = divingOccupationApprovals;

                DivingRequestOccupationWorkFlow DivingRequestOccupationWorkFlow = new DivingRequestOccupationWorkFlow(_unitOfWork, divingrequestOccupation, comments);
                WorkFlowEngine<DivingRequestOccupationWorkFlow> wf = new WorkFlowEngine<DivingRequestOccupationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(DivingRequestOccupationWorkFlow, taskcode);

                _unitOfWork.ExecuteSqlCommand("update DivingRequest set ModifiedBy = @p0, ModifiedDate = @p1 where DivingRequestID = @p2", _UserId, DateTime.Now, divingrequestOccupation.DivingRequestID);
            });
        }

        /// <summary>
        ///  Rejects the Diving Request Occupation
        /// </summary>
        /// <param name="divingRequestId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectDivingRequestOccupation(string divingRequestId, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var divingrequestOccupation = _unitOfWork.SqlQuery<DivingRequest>("select * from DivingRequest where divingRequestId = @p0", divingRequestId).FirstOrDefault<DivingRequest>();
                var divingOccupationApprovals = _unitOfWork.SqlQuery<DivingOccupationApproval>("select * from DivingOccupationApproval where divingRequestId = @p0 ", divingRequestId, _PortCode).ToList<DivingOccupationApproval>();
                divingrequestOccupation.DivingOccupationApprovals = divingOccupationApprovals;

                DivingRequestOccupationWorkFlow DivingRequestOccupationWorkFlow = new DivingRequestOccupationWorkFlow(_unitOfWork, divingrequestOccupation, comments);
                WorkFlowEngine<DivingRequestOccupationWorkFlow> wf = new WorkFlowEngine<DivingRequestOccupationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(DivingRequestOccupationWorkFlow, taskcode);

                _unitOfWork.ExecuteSqlCommand("update DivingRequest set ModifiedBy = @p0, ModifiedDate = @p1 where DivingRequestID = @p2", _UserId, DateTime.Now, divingrequestOccupation.DivingRequestID);
            });
        }
        #endregion
    }
}
