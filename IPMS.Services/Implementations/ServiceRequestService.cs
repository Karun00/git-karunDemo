using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using IPMS.Core.Repository.Exceptions;
using System.Globalization;
using IPMS.Services;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                  ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServiceRequestService : ServiceBase, IServiceRequestService
    {
        private ISubCategoryRepository _subcategoryRepository;
        private IAccountRepository _accountRepository;
        private IPortConfigurationRepository _portConfigurationRepository;
        private IServiceRequestRepository _serviceRequestRepository;
        private IUserRepository _userRepository;
        private IAutomatedSlotConfigurationService _Automatedslotconfigrepository;
        //mahesh
        IServiceRequestService _servicerequestService;
        public ServiceRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _accountRepository = new AccountRepository(_unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _Automatedslotconfigrepository = new AutomatedSlotConfigurationService(_unitOfWork);
        }
        public ServiceRequestService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _accountRepository = new AccountRepository(_unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);            
            _userRepository = new UserRepository(_unitOfWork);            
        }

        public List<ServiceRequestVCNDetails> GetVCNDetailsForServiceRequest(string searchValue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int loginuserid = _accountRepository.GetUserId(_LoginName);
                var data = _serviceRequestRepository.GetTerminalOperatorForUser(loginuserid, _PortCode);
                int AgentUserID = 0;
                int ToUserID = 0;

                if (data.UserType == "AGNT")
                {
                    AgentUserID = data.UserTypeID;
                }
                else
                    if (data.UserType == "TO")
                    {
                        ToUserID = data.UserTypeID;
                    }


                return _serviceRequestRepository.GetVCNDetailsForServiceRequest(_PortCode, AgentUserID, ToUserID, searchValue);
            });
        }

        /// <summary>
        /// To Get VCN's and their Details   
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>      
        public List<ServiceRequestVCNDetails> GetVCNDetails()
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _serviceRequestRepository.GetVCNDetails(_PortCode);
            });
        }

        /// <summary>
        /// To Get Service request details For grid binding
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<ServiceRequest_VO> GetServiceRequestDetails(string frmdate, string todate, string vcnSearch, string vesselName, string MovementType)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int loginuserid = _accountRepository.GetUserId(_LoginName);
                var data = _serviceRequestRepository.GetTerminalOperatorForUser(loginuserid, _PortCode);
                int AgentUserID = 0;
                int ToUserID = 0;
                int EmpID = 0;

                if (data.UserType == "AGNT")
                {
                    AgentUserID = data.UserID;

                }
                else
                    if (data.UserType == "TO")
                    {
                        ToUserID = data.UserTypeID;
                    }
                    else
                        if (data.UserType == "EMP")
                        {
                            EmpID = data.UserTypeID;
                        }
                return _serviceRequestRepository.GetServiceRequestDetails(_PortCode, AgentUserID, ToUserID, EmpID, frmdate, todate, vcnSearch, vesselName, MovementType);
            });
        }

        /// <summary>
        /// To Get CurrentBerth and Bollard details of VCN
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<ServiceRequestCureentBerthnBollards> GetCurrentBerthAndBollards(string vcn, string PortCode)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _serviceRequestRepository.GetCurrentBerthAndBollards(vcn, _PortCode);
            });
        }

        /// <summary>
        /// To Get Bollards at respected Berth for Raised service request
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<BollardVO> GetBollardAtBerth(string BerthCode)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _serviceRequestRepository.GetBollardAtBerth(BerthCode, _PortCode).MapToDTO();
            });
        }

        /// <summary>
        /// To Get Slots 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SlotVO GetPreferredSlot(string PreferredDate)
        {
            return EncloseTransactionAndHandleException(() =>
            {              
                return _serviceRequestRepository.GetSlotPeriodBySlotDate(Convert.ToDateTime(PreferredDate, CultureInfo.InvariantCulture), _PortCode); 
            });
        }

        /// <summary>
        /// To Approve Service request in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void ApproveServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;
                ServiceRequest_VO VO = new ServiceRequest_VO();
                ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(servicerequestWorkFlow, taskcode);
            });
        }

        /// <summary>
        /// To Confirm Service request in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void ConfirmServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;
                ServiceRequest_VO VO = new ServiceRequest_VO();
                ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(servicerequestWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To Reject Service request in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void RejectServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;
                ServiceRequest_VO VO = new ServiceRequest_VO();

                if (servicedtls.MovementType == "SHMV")
                {
                    ServiceRequestShiftingWorkFlow servicerequestshiftingworkflow = new ServiceRequestShiftingWorkFlow(_unitOfWork, servicedtls, comments);
                    WorkFlowEngine<ServiceRequestShiftingWorkFlow> wf = new WorkFlowEngine<ServiceRequestShiftingWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(servicerequestshiftingworkflow, taskcode);
                }

                ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestWorkFlow> wf1 = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                wf1.Process(servicerequestWorkFlow, taskcode);

            });
        }


        /// <summary>
        /// To Cancel Service request in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void CancelServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;                       

                if (servicedtls.MovementType == "SHMV")
                {
                    ServiceRequestShiftingWorkFlow servicerequestshiftingworkflow = new ServiceRequestShiftingWorkFlow(_unitOfWork, servicedtls, comments);
                    WorkFlowEngine<ServiceRequestShiftingWorkFlow> wf = new WorkFlowEngine<ServiceRequestShiftingWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(servicerequestshiftingworkflow, _portConfigurationRepository.GetPortConfiguration(_PortCode).CancelCode);
                }

                ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestWorkFlow> wf1 = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                wf1.Process(servicerequestWorkFlow, taskcode);


            });
        }

        /// <summary>
        /// To Cancel the Confirmed Service request in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void ConfirmCancelServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;                

                ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestWorkFlow> wf1 = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                wf1.Process(servicerequestWorkFlow, taskcode);


            });
        }


        /// <summary>
        /// To Approve Service request in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void CancelApproveServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;
                ServiceRequest_VO VO = new ServiceRequest_VO();
                ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(servicerequestWorkFlow, taskcode);
            });
        }


        /// <summary>
        /// To Reject Service request in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void CancelRejectServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;
                ServiceRequest_VO VO = new ServiceRequest_VO();
                                
                ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestWorkFlow> wf1 = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                wf1.Process(servicerequestWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To Save service Request 
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="BusinessExceptions"></exception>
        /// <returns></returns>
        public ServiceRequest_VO AddServiceRequest(ServiceRequest_VO servicedata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                ServiceRequest serviceObj = null;
                serviceObj = servicedata.MapToEntity();

                if (serviceObj.ArrivalNotification == null)
                    serviceObj.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(serviceObj.VCN);

                int userid = _accountRepository.GetUserId(_LoginName);
                try
                {
                    serviceObj.CreatedBy = userid;
                    serviceObj.CreatedDate = DateTime.Now;
                    serviceObj.ModifiedBy = userid;
                    serviceObj.ModifiedDate = DateTime.Now;
                    serviceObj.RecordStatus = "A";
                }
                catch (Exception ex)
                {
                    throw new FaultException("_LoginName should not be empty, Exception = " + ex);
                }

                #region Validation - Ensuring no pending movements when raising Sailing Request

                if (serviceObj.MovementType == MovementTypes.SAILING && _serviceRequestRepository.GetIncompleteMovementDetailsById(servicedata.VCN).Any())
                {
                    throw new BusinessExceptions(string.Format("There is pending movements against {0}, Please Complete / Cancel those movements.", servicedata.VCN));
                }

                #endregion

                #region Workflow Integration
                string remarks = "New Service Request";

                #region Service Request Workflow

                //mahesh K

                //Begin Booking of Slots – All ports has reported that on a few occasions, the agent could book a slot which was already “full”

                //DateTime datetime = DateTime.ParseExact(servicedata.MovementDateTime, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
                // string wfapprovedcode = "";
                //string portcode = "";
                //List<AutomatedSlotConfigurationVO> AutomatedSlotConfigurationlist = _Automatedslotconfigrepository.GetAutomatedSlotConfigList();
                //if (AutomatedSlotConfigurationlist.Count > 0)
                //{
                //    portcode = AutomatedSlotConfigurationlist[0].PortCode;
                //}
                //string portcode = _portConfigurationRepository.GetPortConfiguration(_PortCode).PortCode;
            //    string msgInfo = _serviceRequestRepository.GetMomentTypeCount(servicedata.MovementSlot, datetime, servicedata.MovementType, portcode);         
             //   if(msgInfo!="")
            //    {
             //       throw new BusinessExceptions(string.Format(msgInfo));
             //   }               


                // End Booking of Slots – All ports has reported that on a few occasions, the agent could book a slot which was already “full”

                if (serviceObj.MovementType == MovementTypes.SHIFTING)
                {
                    ServiceRequestShiftingWorkFlow servicerequestshiftingworkflow = new ServiceRequestShiftingWorkFlow(_unitOfWork, serviceObj, remarks);
                    WorkFlowEngine<ServiceRequestShiftingWorkFlow> wf = new WorkFlowEngine<ServiceRequestShiftingWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(servicerequestshiftingworkflow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                }
                else
                {
                    ServiceRequestWorkFlow servicerequestworkflow = new ServiceRequestWorkFlow(_unitOfWork, serviceObj, remarks);
                    WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                    wf.Process(servicerequestworkflow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                }

                #endregion

                #endregion
                return servicedata;
            });

        }
       
      

        /// <summary>
        /// To Cancel service Request work flow
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ServiceRequest_VO Cancel(ServiceRequest_VO servicedata)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                string remarks = servicedata.workflowRemarks;
                if (servicedata.IsConfirmCancel == "Y")
                {                   
                    string ServiceRequestID = Convert.ToString(servicedata.ServiceRequestID);
                    int userid = _accountRepository.GetUserId(_LoginName);
                    var serviceObj = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                    serviceObj.MovementName = serviceObj.SubCategory.SubCatName;
                    serviceObj.VesselName = serviceObj.ArrivalNotification.Vessel.VesselName;
                    serviceObj.SubmittedDateTime = serviceObj.CreatedDate;

                    try
                    {

                        serviceObj.CreatedBy = userid;
                        serviceObj.CreatedDate = DateTime.Now;
                        serviceObj.ModifiedBy = userid;
                        serviceObj.ModifiedDate = DateTime.Now;
                        serviceObj.RecordStatus = "A";
                    }
                    catch (Exception ex)
                    {
                        throw new FaultException("_LoginName should not be empty, Exception = " + ex);
                    }

                                      
                        ServiceRequestWorkFlow servicerequestworkflow = new ServiceRequestWorkFlow(_unitOfWork, serviceObj, remarks);
                        WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(servicerequestworkflow, "WFCC");
                  
                    
                }
                else
                {                   
                    string ServiceRequestID = Convert.ToString(servicedata.ServiceRequestID);
                    int userid = _accountRepository.GetUserId(_LoginName);
                    var serviceObj = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                    serviceObj.MovementName = serviceObj.SubCategory.SubCatName;
                    serviceObj.VesselName = serviceObj.ArrivalNotification.Vessel.VesselName;
                    serviceObj.SubmittedDateTime = serviceObj.CreatedDate;
                    try
                    {

                        serviceObj.CreatedBy = userid;
                        serviceObj.CreatedDate = DateTime.Now;
                        serviceObj.ModifiedBy = userid;
                        serviceObj.ModifiedDate = DateTime.Now;
                        serviceObj.RecordStatus = "A";
                    }
                    catch (Exception ex)
                    {
                        throw new FaultException("_LoginName should not be empty, Exception = " + ex);
                    }

                    #region Workflow Integration

                    #region Service Request Workflow

                    if (serviceObj.MovementType == "SHMV" && serviceObj.WorkflowInstanceId == null)
                    {
                        ServiceRequestShiftingWorkFlow servicerequestshiftingworkflow = new ServiceRequestShiftingWorkFlow(_unitOfWork, serviceObj, remarks);
                        WorkFlowEngine<ServiceRequestShiftingWorkFlow> wf = new WorkFlowEngine<ServiceRequestShiftingWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(servicerequestshiftingworkflow, _portConfigurationRepository.GetPortConfiguration(_PortCode).CancelCode);
                    }
                    else
                    {
                        ServiceRequestWorkFlow servicerequestworkflow = new ServiceRequestWorkFlow(_unitOfWork, serviceObj, remarks);
                        WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, userid);
                        wf.Process(servicerequestworkflow, _portConfigurationRepository.GetPortConfiguration(_PortCode).CancelCode);
                    }

                    #endregion
                    #endregion
                }
                return servicedata;
            });
        }

        /// <summary>
        /// To Modify service Request
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ServiceRequest_VO ModifyServiceRequest(ServiceRequest_VO servicedata)
        {

            return EncloseTransactionAndHandleException(() =>
            {

                ServiceRequest serviceObj;
                serviceObj = servicedata.MapToEntity();

                int userid = _accountRepository.GetUserId(_LoginName);
                try
                {

                    serviceObj.CreatedBy = userid;
                    serviceObj.ModifiedBy = userid;
                    serviceObj.ModifiedDate = DateTime.Now;

                }
                catch (Exception ex)
                {
                    throw new FaultException("_LoginName should not be empty, Exception = " + ex);
                }


                #region Workflow Integration
                string remarks = "Update Service Request";

                #region Service Request Workflow

                if (serviceObj.MovementType == "SHMV")
                {
                    ServiceRequestShiftingWorkFlow servicerequestshiftingworkflow = new ServiceRequestShiftingWorkFlow(_unitOfWork, serviceObj, remarks);
                    WorkFlowEngine<ServiceRequestShiftingWorkFlow> wf = new WorkFlowEngine<ServiceRequestShiftingWorkFlow>(_unitOfWork, _PortCode, servicedata.ModifiedBy);
                    wf.Process(servicerequestshiftingworkflow, "UPDT");
                }
                else
                {
                    ServiceRequestWorkFlow servicerequestworkflow = new ServiceRequestWorkFlow(_unitOfWork, serviceObj, remarks);
                    WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, _PortCode, servicedata.ModifiedBy);
                    wf.Process(servicerequestworkflow, "UPDT");
                }

                #endregion
                #endregion

                servicedata = serviceObj.MapToDto();
                return servicedata;

            });
        }

        /// <summary>
        /// To get reference data for Movement types and Side along side
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ServiceRequestVO GetServiceRequestReferenceData(string PortCode)
        {
            ServiceRequestVO VO = new ServiceRequestVO();
            VO.getMomentTypes = _subcategoryRepository.MomentTypes();
            VO.getSideAlongSides = _subcategoryRepository.DockTypes();
            VO.getWarpSides = _subcategoryRepository.WarpSides();
            VO.getDocumenttypes = _subcategoryRepository.GetServiceDocumentTypes();
            VO.UserDetails = _userRepository.GetUserDetailByID(_UserId);
            VO.Slots = _serviceRequestRepository.GetSlotDetails(_PortCode);

            return VO;
        }

        /// <summary> 
        /// To Get Service request details based on Service requestid for Pending tasks view
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<ServiceRequest_VO> GetServiceRequest(string serviceid)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _serviceRequestRepository.GetServiceRequest(serviceid);
            });
        }


        # region Service Request Shifting

        /// <summary>
        /// To Approve Service request in pending tasks list (WorkFlow) for Shifting
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void ApproveServiceRequestShifting(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;
                ServiceRequest_VO VO = new ServiceRequest_VO();
                ServiceRequestShiftingWorkFlow servicerequestshiftingWorkFlow = new ServiceRequestShiftingWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestShiftingWorkFlow> wf = new WorkFlowEngine<ServiceRequestShiftingWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(servicerequestshiftingWorkFlow, taskcode);
            });
        }


        /// <summary>
        /// To Reject Service request in pending tasks list (WorkFlow) for Shifting
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void RejectServiceRequestShiting(string ServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _serviceRequestRepository.GetServiceRequestDetailsForWorkFlow(ServiceRequestID);
                servicedtls.MovementName = servicedtls.SubCategory.SubCatName;
                servicedtls.VesselName = servicedtls.ArrivalNotification.Vessel.VesselName;
                servicedtls.SubmittedDateTime = servicedtls.CreatedDate;
                ServiceRequest_VO VO = new ServiceRequest_VO();
                ServiceRequestShiftingWorkFlow servicerequestshiftingWorkFlow = new ServiceRequestShiftingWorkFlow(_unitOfWork, servicedtls, comments);
                WorkFlowEngine<ServiceRequestShiftingWorkFlow> wf = new WorkFlowEngine<ServiceRequestShiftingWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(servicerequestshiftingWorkFlow, taskcode);

            });
        }

        #endregion


    }
}
