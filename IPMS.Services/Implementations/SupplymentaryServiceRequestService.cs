using Core.Repository;
using IPMS.Data.Context;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using IPMS.Domain.DTOS;
using System;
using System.Linq;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using System.Data.SqlClient;
using Core.Repository.Providers.EntityFramework;
using System.Globalization;
namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SupplymentaryServiceRequestService : ServiceBase, ISupplymentaryServiceRequestService
    {
        ISupplymentaryServiceRepository _supplymentaryServiceRepository = null;
        // private IWorkFlowEngine<SupplymentaryServiceRequestWorkFlow> wfEngine;
        private IPortConfigurationRepository _portConfigurationRepository;
        //private IUserRepository _userRepository = null;
        // private IResourceAllocationRepository _resourceAllocationRepository = null;
        //private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository = null;
        private IAutomatedSlottingRepository _automatedSlottingRepository = null;
        private IServiceRequestRepository _serviceRequestRepository;

        private IAccountRepository _accountRepository;

        public SupplymentaryServiceRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _UserId = GetUserIdByLoginname(_LoginName);
            _supplymentaryServiceRepository = new SupplymentaryServiceRepository(_unitOfWork);


            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            // _userRepository = new UserRepository(_unitOfWork);
            // _resourceAllocationRepository = new ResourceAllocationRepository(_unitOfWork);
            // _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _automatedSlottingRepository = new AutomatedSlottingRepository(_unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);

        }

        public SupplymentaryServiceRequestService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());

            _UserId = GetUserIdByLoginname(_LoginName);
            _supplymentaryServiceRepository = new SupplymentaryServiceRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            //  _userRepository = new UserRepository(_unitOfWork);
            // _resourceAllocationRepository = new ResourceAllocationRepository(_unitOfWork);
            // _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _automatedSlottingRepository = new AutomatedSlottingRepository(_unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
        }

        /// <summary>
        /// Author  : Sandeep Appana 
        /// Date    : 21st August 2014
        /// Purpose : To Get ServiceType details
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetServiceType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _supplymentaryServiceRepository.GetServiceType();
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 22nd August 2014
        /// Purpose : To Get Supplymentary Service Request details
        /// </summary>
        /// <returns></returns>
        public List<SuppServiceRequestVO> GetSupplymentaryServiceRequestList(string frmdate, string todate, string vcnSearch, string vesselName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var data = _supplymentaryServiceRepository.GetUserTypesForUser(userid, _PortCode);
                int AgentUserID = 0;
                int ToUserID = 0;
                int EmpID = 0;

                if (data.UserType == "AGNT")
                {
                    //AgentUserID = data.UserID;
                    AgentUserID = data.UserTypeID;

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
                return _supplymentaryServiceRepository.GetSupplymentaryServiceRequestList(_PortCode, AgentUserID, ToUserID, EmpID, frmdate, todate, vcnSearch, vesselName);
            });
        }
        //SuppServiceRequestID

        /// <summary>
        /// Author  : Srini 
        /// Date    : 15th sep 2014
        /// Purpose : To Get Supplymentary Service Request details by SuppServiceRequestID 
        /// </summary>
        /// <param name="SuppServiceRequestID"></param>
        /// <returns></returns>
        public SuppServiceRequestVO GetSupplymentaryServiceRequest(string SuppServiceRequestId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _supplymentaryServiceRepository.GetSupplymentaryServiceRequest(_PortCode, SuppServiceRequestId);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>

        public List<SuppServiceRequestVO> GetSupplymentaryServiceRequestListVcn(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _supplymentaryServiceRepository.GetSupplymentaryServiceRequestListVcn(_PortCode, VCN);
            });
        }
        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 23rd August 2014
        /// Purpose : To Add Supplmentary Service Request Data
        /// </summary>
        /// <param name="suppServiceRequestVO"></param>
        /// <returns></returns>

        public SuppServiceRequestVO PostSupplymentaryServiceRequest(SuppServiceRequestVO suppServiceRequestVO)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                int loginuserid = _accountRepository.GetUserId(_LoginName);
                var data = _serviceRequestRepository.GetTerminalOperatorForUser(loginuserid, _PortCode);
                int AgentUserID = 0;
                // Modified by Srini - on 25th Jan 2016 - Employee all can able to do if he has Privilegies
                if (data.UserType == "AGNT")
                {
                    AgentUserID = data.UserTypeID;
                    suppServiceRequestVO.AgentId = AgentUserID;
                }
               


                suppServiceRequestVO.RecordStatus = "A";
                suppServiceRequestVO.CreatedDate = DateTime.Now;
                suppServiceRequestVO.CreatedBy = userid;
                suppServiceRequestVO.ModifiedDate = DateTime.Now;
                suppServiceRequestVO.ModifiedBy = userid;

                SuppServiceRequest suppServiceRequest = suppServiceRequestVO.MapToEntity();
                suppServiceRequest.ServiceTypeName = suppServiceRequestVO.ServiceTypeName;

                suppServiceRequest.PortCode = _PortCode;
                suppServiceRequest.ObjectState = ObjectState.Added;

                if (suppServiceRequest.ServiceType != "FCST" && suppServiceRequest.ServiceType != "WTST")
                {
                    suppServiceRequest.IsStartTime = "Y";
                }
                else
                {
                    suppServiceRequest.IsStartTime = "N";
                }

                _unitOfWork.Repository<SuppServiceRequest>().Insert(suppServiceRequest);

                if (suppServiceRequest.ServiceType == "FCST")
                {
                    List<SuppFloatingCrane> lstSuppFloatingCrane = suppServiceRequestVO.SuppFloatingCranesVO.MapToEntity();

                    if (lstSuppFloatingCrane.Count > 0)
                    {
                        foreach (SuppFloatingCrane suppFloatingCrane in lstSuppFloatingCrane)
                        {
                            suppFloatingCrane.SuppServiceRequestID = suppServiceRequest.SuppServiceRequestID;
                            suppFloatingCrane.CreatedBy = suppServiceRequest.CreatedBy;
                            suppFloatingCrane.CreatedDate = suppServiceRequest.CreatedDate;
                            suppFloatingCrane.ModifiedBy = suppServiceRequest.CreatedBy;
                            suppFloatingCrane.ModifiedDate = suppServiceRequest.CreatedDate;
                            suppFloatingCrane.RecordStatus = "A";
                            suppFloatingCrane.ObjectState = ObjectState.Added;

                            _unitOfWork.Repository<SuppFloatingCrane>().Insert(suppFloatingCrane);
                        }
                    }
                }
                else if (suppServiceRequest.ServiceType == "HCST" || suppServiceRequest.ServiceType == "HWST" || suppServiceRequest.ServiceType == "CWST")
                {
                    SuppHotColdWorkPermit suppHotColdWorkPermit = suppServiceRequestVO.SuppHotColdWorkPermitsVO.MapToEntity();

                    if (suppHotColdWorkPermit != null)
                    {
                        suppHotColdWorkPermit.SuppServiceRequestID = suppServiceRequest.SuppServiceRequestID;
                        suppHotColdWorkPermit.CreatedBy = suppServiceRequest.CreatedBy;
                        suppHotColdWorkPermit.CreatedDate = suppServiceRequest.CreatedDate;
                        suppHotColdWorkPermit.ModifiedBy = suppServiceRequest.CreatedBy;
                        suppHotColdWorkPermit.ModifiedDate = suppServiceRequest.CreatedDate;
                        suppHotColdWorkPermit.ObjectState = ObjectState.Added;

                        _unitOfWork.Repository<SuppHotColdWorkPermit>().Insert(suppHotColdWorkPermit);

                        if (suppServiceRequestVO.SuppHotColdWorkPermitsVO.SuppHotColdWorkPermitDocumentsVO != null)
                        {
                            List<SuppHotColdWorkPermitDocument> lstSuppHotColdWorkPermitDocument = suppServiceRequestVO.SuppHotColdWorkPermitsVO.SuppHotColdWorkPermitDocumentsVO.MapToEntity();

                            //if (lstSuppHotColdWorkPermitDocument.Count > 0)
                            //{
                            foreach (SuppHotColdWorkPermitDocument suppHotColdWorkPermitDocument in lstSuppHotColdWorkPermitDocument)
                            {
                                suppHotColdWorkPermitDocument.SuppHotColdWorkPermitID = suppHotColdWorkPermit.SuppHotColdWorkPermitID;
                                suppHotColdWorkPermitDocument.CreatedBy = suppServiceRequest.CreatedBy;
                                suppHotColdWorkPermitDocument.CreatedDate = suppServiceRequest.CreatedDate;
                                suppHotColdWorkPermitDocument.ModifiedBy = suppServiceRequest.CreatedBy;
                                suppHotColdWorkPermitDocument.ModifiedDate = suppServiceRequest.CreatedDate;
                                suppHotColdWorkPermitDocument.ObjectState = ObjectState.Added;

                                _unitOfWork.Repository<SuppHotColdWorkPermitDocument>().Insert(suppHotColdWorkPermitDocument);
                            }
                            //}
                        }
                    }
                }


                _unitOfWork.SaveChanges();

                // int res = _unitOfWork.ExecuteSqlCommand("update dbo.SuppServiceRequest set OcupationFromDate = @p1 ,OcupationToDate = @p2,HoursOfOccupation1 = @p3, ModifiedBy= @p4 ,   ModifiedDate= @p5 ,RecordStatus= 'A' where SuppServiceRequestID = @p0", divingrequest.SuppServiceRequestID, divingrequest.OcupationFromDate, divingrequest.OcupationToDate, divingrequest.HoursOfOccupation1, divingrequest.ModifiedBy, divingrequest.ModifiedDate);


                #region Workflow Integration
                string remarks = "Supplementary Service Request";

                #region User Registration Workflow

                SupplymentaryServiceRequestWorkFlow suppServiceRequestWorkFlow = new SupplymentaryServiceRequestWorkFlow(_unitOfWork, suppServiceRequest, remarks);
                WorkFlowEngine<SupplymentaryServiceRequestWorkFlow> wf = new WorkFlowEngine<SupplymentaryServiceRequestWorkFlow>(_unitOfWork, _PortCode, suppServiceRequest.CreatedBy);

                wf.Process(suppServiceRequestWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                var an = _unitOfWork.Repository<SuppServiceRequest>().Find(Convert.ToInt32(suppServiceRequestWorkFlow.ReferenceId, CultureInfo.InvariantCulture));

                an.ObjectState = ObjectState.Modified;

                an.WorkflowInstanceID = wf.GetWorkFlowInstance(suppServiceRequestWorkFlow).WorkflowInstanceId;
                // int res = _unitOfWork.ExecuteSqlCommand("update dbo.SuppServiceRequest set WorkflowInstanceID = @p1 where SuppServiceRequestID = @p0", suppServiceRequestWorkFlow.ReferenceId, an.WorkflowInstanceID );

                _unitOfWork.Repository<SuppServiceRequest>().Update(an);

                #endregion
                #endregion

                _unitOfWork.SaveChanges();

                return suppServiceRequestVO;
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 25st August 2014
        /// Purpose : To Add Supplmentary Service Request Data
        /// </summary>
        /// <param name="suppServiceRequestVO"></param>
        /// <returns></returns>
        public SuppServiceRequestVO ModifySupplymentaryServiceRequest(SuppServiceRequestVO suppServiceRequestVO)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                int loginuserid = _accountRepository.GetUserId(_LoginName);
                var data = _serviceRequestRepository.GetTerminalOperatorForUser(loginuserid, _PortCode);
                int AgentUserID = 0;
                // Modified by Srini - on 25th Jan 2016 - Employee all can able to do if he has Privilegies
                if (data.UserType == "AGNT")
                {
                    AgentUserID = data.UserTypeID;
                    suppServiceRequestVO.AgentId = AgentUserID;
                }
               
                suppServiceRequestVO.RecordStatus = "A";
                suppServiceRequestVO.ModifiedDate = DateTime.Now;
                suppServiceRequestVO.ModifiedBy = userid;
                SuppServiceRequest suppServiceRequest = suppServiceRequestVO.MapToEntity();
                suppServiceRequest.ServiceTypeName = suppServiceRequestVO.ServiceTypeName;
                suppServiceRequest.RecordStatus = "A";
                suppServiceRequest.ObjectState = ObjectState.Modified;

                if (suppServiceRequest.ServiceType != "FCST" && suppServiceRequest.ServiceType != "WTST")
                {
                    suppServiceRequest.IsStartTime = "Y";
                }
                else
                {
                    suppServiceRequest.IsStartTime = "N";
                }

                _unitOfWork.Repository<SuppServiceRequest>().Update(suppServiceRequest);
                //Floating Crane
                if (suppServiceRequest.ServiceType == "FCST")
                {

                    List<SuppFloatingCrane> lstSuppFloatingCranes = _unitOfWork.Repository<SuppFloatingCrane>().Queryable().Where(e => e.SuppServiceRequestID == suppServiceRequest.SuppServiceRequestID).ToList();

                    if (lstSuppFloatingCranes.Count > 0)
                    {
                        foreach (SuppFloatingCrane suppFloatingCrane in lstSuppFloatingCranes)
                        {
                            _unitOfWork.Repository<SuppFloatingCrane>().Delete(suppFloatingCrane);
                        }
                    }

                    _unitOfWork.SaveChanges();

                    List<SuppFloatingCrane> lstSuppFloatingCrane = suppServiceRequestVO.SuppFloatingCranesVO.MapToEntity();

                    if (lstSuppFloatingCrane.Count > 0)
                    {
                        foreach (SuppFloatingCrane suppFloatingCrane in lstSuppFloatingCrane)
                        {
                            suppFloatingCrane.SuppServiceRequestID = suppServiceRequest.SuppServiceRequestID;
                            suppFloatingCrane.CreatedBy = suppServiceRequest.ModifiedBy;
                            suppFloatingCrane.CreatedDate = suppServiceRequest.ModifiedDate;
                            suppFloatingCrane.ModifiedBy = suppServiceRequest.ModifiedBy;
                            suppFloatingCrane.ModifiedDate = suppServiceRequest.ModifiedDate;
                            suppFloatingCrane.RecordStatus = "A";
                            suppFloatingCrane.ObjectState = ObjectState.Added;

                            _unitOfWork.Repository<SuppFloatingCrane>().Insert(suppFloatingCrane);
                        }
                    }
                }
                //Hot Water / Cold Water / HOt & Cold Water 
                else if (suppServiceRequest.ServiceType == "HCST" || suppServiceRequest.ServiceType == "HWST" || suppServiceRequest.ServiceType == "CWST")
                {
                    SuppHotColdWorkPermit suppHotColdWorkPermit = suppServiceRequestVO.SuppHotColdWorkPermitsVO.MapToEntity();

                    if (suppHotColdWorkPermit != null)
                    {
                        suppHotColdWorkPermit.ModifiedBy = suppServiceRequest.ModifiedBy;
                        suppHotColdWorkPermit.ModifiedDate = suppServiceRequest.ModifiedDate;
                        suppHotColdWorkPermit.ObjectState = ObjectState.Modified;

                        _unitOfWork.Repository<SuppHotColdWorkPermit>().Update(suppHotColdWorkPermit);

                        if (suppServiceRequestVO.SuppHotColdWorkPermitsVO.SuppHotColdWorkPermitDocumentsVO != null)
                        {
                            List<SuppHotColdWorkPermitDocument> lstHotColdWorkPermitDocuments = _unitOfWork.Repository<SuppHotColdWorkPermitDocument>().Queryable().Where(d => d.SuppHotColdWorkPermitID == suppHotColdWorkPermit.SuppHotColdWorkPermitID).ToList();

                            if (lstHotColdWorkPermitDocuments.Count > 0)
                            {
                                foreach (SuppHotColdWorkPermitDocument document in lstHotColdWorkPermitDocuments)
                                {
                                    _unitOfWork.Repository<SuppHotColdWorkPermitDocument>().Delete(document);
                                }

                                _unitOfWork.SaveChanges();
                            }

                            List<SuppHotColdWorkPermitDocument> lstSuppHotColdWorkPermitDocument = suppServiceRequestVO.SuppHotColdWorkPermitsVO.SuppHotColdWorkPermitDocumentsVO.MapToEntity();

                            //if (lstSuppHotColdWorkPermitDocument.Count > 0)
                            //{
                            foreach (SuppHotColdWorkPermitDocument suppHotColdWorkPermitDocument in lstSuppHotColdWorkPermitDocument)
                            {
                                suppHotColdWorkPermitDocument.SuppHotColdWorkPermitID = suppHotColdWorkPermit.SuppHotColdWorkPermitID;
                                suppHotColdWorkPermitDocument.CreatedBy = suppServiceRequest.ModifiedBy;
                                suppHotColdWorkPermitDocument.CreatedDate = suppServiceRequest.ModifiedDate;
                                suppHotColdWorkPermitDocument.ModifiedBy = suppServiceRequest.ModifiedBy;
                                suppHotColdWorkPermitDocument.ModifiedDate = suppServiceRequest.ModifiedDate;
                                suppHotColdWorkPermitDocument.ObjectState = ObjectState.Added;

                                _unitOfWork.Repository<SuppHotColdWorkPermitDocument>().Insert(suppHotColdWorkPermitDocument);
                            }
                            //}
                        }
                    }
                }

                _unitOfWork.SaveChanges();

                #region Workflow Integration
                string remarks = "Supplementary Service Request Updated";

                #region User Registration Workflow

                SupplymentaryServiceRequestWorkFlow suppServiceRequestWorkFlow = new SupplymentaryServiceRequestWorkFlow(_unitOfWork, suppServiceRequest, remarks);
                WorkFlowEngine<SupplymentaryServiceRequestWorkFlow> wf = new WorkFlowEngine<SupplymentaryServiceRequestWorkFlow>(_unitOfWork, _PortCode, suppServiceRequest.CreatedBy);

                wf.Process(suppServiceRequestWorkFlow, "UPDT");
                var an = _unitOfWork.Repository<SuppServiceRequest>().Find(Convert.ToInt32(suppServiceRequestWorkFlow.ReferenceId, CultureInfo.InvariantCulture));

                an.ObjectState = ObjectState.Modified;

                an.WorkflowInstanceID = wf.GetWorkFlowInstance(suppServiceRequestWorkFlow).WorkflowInstanceId;
                // int res = _unitOfWork.ExecuteSqlCommand("update dbo.SuppServiceRequest set WorkflowInstanceID = @p1 where SuppServiceRequestID = @p0", suppServiceRequestWorkFlow.ReferenceId, an.WorkflowInstanceID );

                _unitOfWork.Repository<SuppServiceRequest>().Update(an);

                #endregion
                #endregion

                _unitOfWork.SaveChanges();

                return suppServiceRequestVO;
            });
        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <returns></returns>
        public List<SuppServiceRequestVO> AllSuppHotWorkInspectionDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _supplymentaryServiceRepository.AllSuppHotWorkInspectionDetails(_PortCode);
            });

        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <returns></returns>
        public List<SuppServiceRequestVO> AllSuppDockUnDockTimeDetails()
        {

            return ExecuteFaultHandledOperation(() =>
            {
                return _supplymentaryServiceRepository.AllSuppDockUnDockTimeDetails(_PortCode);
            });


        }

        /// <summary>
        ///  Verifies the Diving Request Occupation
        /// </summary>
        /// <param name="SuppServiceRequestID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void VerifySupplymentaryServiceRequest(string SuppServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var suppServiceRequest = _unitOfWork.SqlQuery<SuppServiceRequest>("select * from SuppServiceRequest where SuppServiceRequestID = @p0", SuppServiceRequestID).FirstOrDefault<SuppServiceRequest>();

                SupplymentaryServiceRequestWorkFlow SuppServiceRequestWorkFlow = new SupplymentaryServiceRequestWorkFlow(_unitOfWork, suppServiceRequest, comments);
                WorkFlowEngine<SupplymentaryServiceRequestWorkFlow> wf = new WorkFlowEngine<SupplymentaryServiceRequestWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(SuppServiceRequestWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// Approves the Diving Request Occupation
        /// </summary>
        /// <param name="SuppServiceRequestID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveSupplymentaryServiceRequest(string SuppServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var suppServiceRequest = _unitOfWork.SqlQuery<SuppServiceRequest>("select * from SuppServiceRequest where SuppServiceRequestID = @p0", SuppServiceRequestID).FirstOrDefault<SuppServiceRequest>();

                SupplymentaryServiceRequestWorkFlow SuppServiceRequestWorkFlow = new SupplymentaryServiceRequestWorkFlow(_unitOfWork, suppServiceRequest, comments);
                WorkFlowEngine<SupplymentaryServiceRequestWorkFlow> wf = new WorkFlowEngine<SupplymentaryServiceRequestWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(SuppServiceRequestWorkFlow, taskcode);

                if (suppServiceRequest.ServiceType == ServiceTypeCode.WaterService)
                {
                    ResourceAllocation resource = new ResourceAllocation
                    {
                        ServiceReferenceType = ServiceReferenceType.SupplementoryService,
                        ServiceReferenceID = suppServiceRequest.SuppServiceRequestID,
                        OperationType = ServiceTypeCode.WaterService,
                        //ResourceID = null,
                        //ResourceType = ServiceTypeCode.WaterService,
                        TaskStatus = ResourceAllcationWorkFlowStatus.Confirmed,
                        AllocationDate = suppServiceRequest.FromDate,
                        //AllocSlot = _automatedSlottingRepository.GetSlotPeriodBySlotDate(suppServiceRequest.FromDate, _PortCode),
                        AllocSlot = null,
                        IsConfirm = false,
                        RecordStatus = "A",
                        CreatedBy = _UserId,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = _UserId,
                        ModifiedDate = DateTime.Now,
                        ObjectState = ObjectState.Added
                    };

                    _unitOfWork.Repository<ResourceAllocation>().Insert(resource);
                    _unitOfWork.SaveChanges();
                }

                if (suppServiceRequest.ServiceType == ServiceTypeCode.FloatingCrane)
                {
                    ResourceAllocation resource = new ResourceAllocation
                    {
                        ServiceReferenceType = ServiceReferenceType.SupplementoryService,
                        ServiceReferenceID = suppServiceRequest.SuppServiceRequestID,
                        OperationType = ServiceTypeCode.FloatingCrane,
                        //ResourceID = null,
                        //ResourceType = ServiceTypeCode.WaterService,
                        TaskStatus = ResourceAllcationWorkFlowStatus.Confirmed,
                        AllocationDate = suppServiceRequest.FromDate,
                        //AllocSlot = _automatedSlottingRepository.GetSlotPeriodBySlotDate(suppServiceRequest.FromDate, _PortCode),
                        AllocSlot = null,
                        IsConfirm = false,
                        RecordStatus = "A",
                        CreatedBy = _UserId,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = _UserId,
                        ModifiedDate = DateTime.Now,
                        ObjectState = ObjectState.Added
                    };

                    _unitOfWork.Repository<ResourceAllocation>().Insert(resource);
                    _unitOfWork.SaveChanges();
                }
            });
        }

        /// <summary>
        ///  Rejects the Diving Request Occupation
        /// </summary>
        /// <param name="SuppServiceRequestID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectSupplymentaryServiceRequest(string SuppServiceRequestID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var suppServiceRequest = _unitOfWork.SqlQuery<SuppServiceRequest>("select * from SuppServiceRequest where SuppServiceRequestID = @p0", SuppServiceRequestID).FirstOrDefault<SuppServiceRequest>();

                SupplymentaryServiceRequestWorkFlow SuppServiceRequestWorkFlow = new SupplymentaryServiceRequestWorkFlow(_unitOfWork, suppServiceRequest, comments);
                WorkFlowEngine<SupplymentaryServiceRequestWorkFlow> wf = new WorkFlowEngine<SupplymentaryServiceRequestWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(SuppServiceRequestWorkFlow, taskcode);

            });
        }

        public List<IMDGInformationVO> GetIMDGForSupplymentaryServiceRequest(string vcn)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 return _supplymentaryServiceRepository.GetIMDGForSupplymentaryServiceRequest(vcn);

             });
        }

        public VesselCallMovementVO GetEtbEtubFromVcn(string vcn)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _supplymentaryServiceRepository.GetEtbEtubFromVcn(vcn);

            });
        }

        public SuppServiceRequestVO Cancel(SuppServiceRequestVO servicedata)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                SuppServiceRequest suppServiceRequest = servicedata.MapToEntity();
                suppServiceRequest.CreatedDate = suppServiceRequest.CreatedDate;
                suppServiceRequest.CreatedBy = suppServiceRequest.CreatedBy;
                suppServiceRequest.ModifiedDate = suppServiceRequest.ModifiedDate;
                suppServiceRequest.ModifiedBy = _UserId;
                suppServiceRequest.RecordStatus = "I";
                suppServiceRequest.Remarks = servicedata.WorkflowRemarks;
                #region Workflow Integration
                string remarks = servicedata.WorkflowRemarks;

                SupplymentaryServiceRequestWorkFlow suppServiceRequestWorkFlow = new SupplymentaryServiceRequestWorkFlow(_unitOfWork, suppServiceRequest, remarks);
                WorkFlowEngine<SupplymentaryServiceRequestWorkFlow> wf = new WorkFlowEngine<SupplymentaryServiceRequestWorkFlow>(_unitOfWork, _PortCode, servicedata.CreatedBy);
                wf.Process(suppServiceRequestWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).CancelCode);
               
                #endregion

                //VesselETAChangeVO _objvesselETAChange = new VesselETAChangeVO();
                //_objvesselETAChange.VCN = "";
                //_objvesselETAChange.SuppServiceRequestID = servicedata.SuppServiceRequestID;
                //_objvesselETAChange.CancelRemarks = servicedata.WorkflowRemarks;
                //_objvesselETAChange.CreatedBy = _UserId;

                //var suppproc = new VesselETAChangeVO.SuppServRequestCancel_proc(_objvesselETAChange);
                //_unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(suppproc);

                return servicedata;
            });
        }

        public List<ServiceRequestVCNDetails> GetVCNDetailsForSuppServiceRequest(string searchValue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int loginuserid = _accountRepository.GetUserId(_LoginName);
                var data = _serviceRequestRepository.GetTerminalOperatorForUser(loginuserid, _PortCode);
                int AgentUserID = 0;

                if (data.UserType == "AGNT")
                {
                    AgentUserID = data.UserTypeID;
                }

                return _supplymentaryServiceRepository.GetVCNDetailsForSuppServiceRequest(_PortCode, AgentUserID, searchValue);
            });
        }
        public List<SuppServiceRequestVO> GetSupplementaryGridDetails(string frmdate, string todate, string vcnSearch, string vesselName)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                //_LoginName = "agent001";
               // _PortCode = "DB";
                int userid = _accountRepository.GetUserId(_LoginName);
                var data = _supplymentaryServiceRepository.GetUserTypesForUser(userid, _PortCode);
                int AgentUserID = 0;
                int ToUserID = 0;
                int EmpID = 0;

                if (data.UserType == "AGNT")
                {
                    //AgentUserID = data.UserID;
                    AgentUserID = data.UserTypeID;

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
                return _supplymentaryServiceRepository.GetSupplementaryGridDetails(_PortCode, AgentUserID, ToUserID, EmpID, frmdate, todate, vcnSearch, vesselName);
            });
        }
   
    }
}
