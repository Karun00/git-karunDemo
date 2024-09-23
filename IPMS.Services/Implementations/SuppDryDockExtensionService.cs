using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.DTOS;
using System;
using System.Linq;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using System.Globalization;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SuppDryDockExtensionService : ServiceBase, ISuppDryDockExtensionService
    {
         #region Declaration
        //private ISuppDryDockRepository _suppDryDockRepository = null;
        private IPortConfigurationRepository _portConfigurationRepository;
      //  private SuppDryDockExtension _suppDryDockExtService;

        private ISuppDryDockExtensionRepository _suppDryDockExtRepository = null;

        
        #endregion

        #region Constructor
        public SuppDryDockExtensionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            //_suppDryDockRepository = new SuppDryDockRepository(_unitOfWork);
            _suppDryDockExtRepository = new SuppDryDockExtensionRepository(_unitOfWork);

            
        }

        public SuppDryDockExtensionService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());

            _UserId = GetUserIdByLoginname(_LoginName);
           // _suppDryDockRepository = new SuppDryDockRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _suppDryDockExtRepository = new SuppDryDockExtensionRepository(_unitOfWork);
        }
        #endregion
        public SuppDryDockExtensionVO PostSuppDryDockExtension(SuppDryDockExtensionVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SuppDryDockExtension objExt = data.MapToEntity();

                objExt.ModifiedBy = _UserId;
                objExt.ModifiedDate = DateTime.Now;
                objExt.ScheduleStatus = WFStatus.NewRequest;
                //objExt.DockPortCode = _PortCode;
                objExt.CreatedBy = _UserId;
                objExt.CreatedDate = DateTime.Now;
                objExt.ModifiedBy = _UserId;
                objExt.ModifiedDate = DateTime.Now;
                objExt.RecordStatus = "A";
               
                #region Supplementary Dry Dock Approval Workflow Integration
                string remarks = "New Supplementary Dry Dock Extension";


                
                    if (data.SuppDryDockDocuments != null)
                    {
                        List<SuppDryDockDocument> lstSuppFloatingCranes = _unitOfWork.Repository<SuppDryDockDocument>().Queryable().Where(e => e.SuppDryDockID == data.SuppDryDockID).ToList();

                        if (lstSuppFloatingCranes.Count > 0)
                        {
                            foreach (SuppDryDockDocument suppFloatingCrane in lstSuppFloatingCranes)
                            {
                                _unitOfWork.Repository<SuppDryDockDocument>().Delete(suppFloatingCrane);
                            }
                        }
                        _unitOfWork.SaveChanges();

                        //SuppHotColdWorkPermit suppHotColdWorkPermit = suppServiceRequestVO.SuppHotColdWorkPermitsVO.MapToEntity();
                        List<SuppDryDockDocument> lstdryDockDoc = data.SuppDryDockDocuments.MapToEntity();

                        foreach (SuppDryDockDocument suppHotColdWorkPermitDocument in lstdryDockDoc)
                        {

                            suppHotColdWorkPermitDocument.SuppDryDockID = data.SuppDryDockID;
                            suppHotColdWorkPermitDocument.DocumentID = suppHotColdWorkPermitDocument.DocumentID;
                            suppHotColdWorkPermitDocument.RecordStatus = "A";
                            suppHotColdWorkPermitDocument.CreatedBy = _UserId;
                            suppHotColdWorkPermitDocument.CreatedDate = DateTime.Now;
                            suppHotColdWorkPermitDocument.ModifiedBy = _UserId;
                            suppHotColdWorkPermitDocument.ModifiedDate = DateTime.Now;
                            suppHotColdWorkPermitDocument.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<SuppDryDockDocument>().Insert(suppHotColdWorkPermitDocument);
                            _unitOfWork.SaveChanges();
                        }

                    }
                
                SuppDryDockExtensionWorkFlow suppDryDockExtWorkFlow = new SuppDryDockExtensionWorkFlow(_unitOfWork, objExt, remarks, data);
               
                WorkFlowEngine<SuppDryDockExtensionWorkFlow> wf = new WorkFlowEngine<SuppDryDockExtensionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockExtWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
              
                #endregion

                return data;
            });
        }

        public SuppDryDockExtensionVO PutSuppDryDockExtension(SuppDryDockExtensionVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SuppDryDockExtension objExt = data.MapToEntity();
                objExt.ScheduleStatus = WFStatus.Update;
                objExt.ModifiedBy = _UserId;
                objExt.ModifiedDate = DateTime.Now;
                if (data.SuppDryDockDocuments != null)
                {
                    List<SuppDryDockDocument> lstSuppFloatingCranes = _unitOfWork.Repository<SuppDryDockDocument>().Queryable().Where(e => e.SuppDryDockID == data.SuppDryDockID).ToList();

                    if (lstSuppFloatingCranes.Count > 0)
                    {
                        foreach (SuppDryDockDocument suppFloatingCrane in lstSuppFloatingCranes)
                        {
                            _unitOfWork.Repository<SuppDryDockDocument>().Delete(suppFloatingCrane);
                        }
                    }
                    _unitOfWork.SaveChanges();

                    //SuppHotColdWorkPermit suppHotColdWorkPermit = suppServiceRequestVO.SuppHotColdWorkPermitsVO.MapToEntity();
                    List<SuppDryDockDocument> lstdryDockDoc = data.SuppDryDockDocuments.MapToEntity();

                    foreach (SuppDryDockDocument suppHotColdWorkPermitDocument in lstdryDockDoc)
                    {

                        suppHotColdWorkPermitDocument.SuppDryDockID = data.SuppDryDockID;
                        suppHotColdWorkPermitDocument.DocumentID = suppHotColdWorkPermitDocument.DocumentID;
                        suppHotColdWorkPermitDocument.RecordStatus = "A";
                        suppHotColdWorkPermitDocument.CreatedBy = _UserId;
                        suppHotColdWorkPermitDocument.CreatedDate = DateTime.Now;
                        suppHotColdWorkPermitDocument.ModifiedBy = _UserId;
                        suppHotColdWorkPermitDocument.ModifiedDate = DateTime.Now;
                        suppHotColdWorkPermitDocument.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<SuppDryDockDocument>().Insert(suppHotColdWorkPermitDocument);
                        _unitOfWork.SaveChanges();
                    }

                }

                objExt.WorkflowInstance = (from wf1 in _unitOfWork.Repository<WorkflowInstance>().Query().Select() where wf1.WorkflowInstanceId == data.WorkflowInstanceID select wf1).SingleOrDefault();
                objExt.WorkflowInstance.WorkflowTaskCode = WFStatus.Update;
               

                #region Supplementary Dry Dock Approval Workflow Integration
                string remarks = "Update Supplementary Dry Dock Extension";

                SuppDryDockExtensionWorkFlow suppDryDockExtWorkFlow = new SuppDryDockExtensionWorkFlow(_unitOfWork, objExt, remarks,data);
                WorkFlowEngine<SuppDryDockExtensionWorkFlow> wf = new WorkFlowEngine<SuppDryDockExtensionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockExtWorkFlow, WFStatus.Update);
                #endregion

                return data;
            });
        }

        public List<ServiceRequestVCNsForDryDockExts> GetSuppVCNDetailsForExtension()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppDryDockExtRepository.GetSuppVCNDetailsForExtension(_PortCode, _UserId);
            });
        }

        public AgentVO GetSuppVCNDetailsForExtensionByVCN(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppDryDockExtRepository.GetSuppVCNDetailsForExtensionByVCN(vcn);
            });
        }
        public List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppDryDockExtRepository.GetSuppDryDockExtensionList(_PortCode);
            });
        }

        public List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionByID(string SuppDryDockExtensionID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppDryDockExtRepository.GetSuppDryDockExtensionByID(_PortCode, SuppDryDockExtensionID);
            });
        }
       


        
        /// <summary>
        /// To Approve Supplementary Dry Dock Extension
        /// </summary>
        /// <param name="SuppDryDockID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveSuppDryDockExtension(string suppdrydockextensionid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                SuppDryDockExtensionVO data = new SuppDryDockExtensionVO();
                var andata = _suppDryDockExtRepository.GetSuppDryDockExtensionApproveid(suppdrydockextensionid);
                andata.ScheduleStatus = WFStatus.Approved;
                SuppDryDockExtensionWorkFlow suppDryDockWorkFlow = new SuppDryDockExtensionWorkFlow(_unitOfWork, andata, remarks, data);
                WorkFlowEngine<SuppDryDockExtensionWorkFlow> wf = new WorkFlowEngine<SuppDryDockExtensionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, taskcode);
                int iSuppDryDockExtensionID = Convert.ToInt32(suppdrydockextensionid, CultureInfo.InvariantCulture);
                SuppDryDockExtension lstSuppDryDockExtension = _unitOfWork.Repository<SuppDryDockExtension>().Queryable().Where(ddex => ddex.SuppDryDockExtensionID == iSuppDryDockExtensionID).FirstOrDefault();
                SuppDryDock lstSuppDryDock = _unitOfWork.Repository<SuppDryDock>().Queryable().Where(e => e.SuppDryDockID == lstSuppDryDockExtension.SuppDryDockID).Where(e => e.ScheduleStatus == SuperCategoryConstants.DOCK_TYPE).FirstOrDefault();
              //  lstSuppDryDock.ScheduleToDate = lstSuppDryDockExtension.ExtensionDateTime;
                lstSuppDryDock.ExtensionDateTime = lstSuppDryDockExtension.ExtensionDateTime;
                lstSuppDryDock.ModifiedBy = _UserId;
                lstSuppDryDock.ModifiedDate = DateTime.Now;
                _unitOfWork.Repository<SuppDryDock>().Update(lstSuppDryDock);
                _unitOfWork.SaveChanges();


            });
        }

        /// <summary>
        /// To Approve Supplementary Dry Dock Extension
        /// </summary>
        /// <param name="SuppDryDockID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectSuppDryDockExtension(string suppdrydockextensionid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                SuppDryDockExtensionVO data = new SuppDryDockExtensionVO();
                var andata = _suppDryDockExtRepository.GetSuppDryDockExtensionApproveid(suppdrydockextensionid);
                andata.ScheduleStatus = WFStatus.Reject;
                SuppDryDockExtensionWorkFlow suppDryDockWorkFlow = new SuppDryDockExtensionWorkFlow(_unitOfWork, andata, remarks, data);
                WorkFlowEngine<SuppDryDockExtensionWorkFlow> wf = new WorkFlowEngine<SuppDryDockExtensionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, taskcode);

               

            });
        }
    }
}
