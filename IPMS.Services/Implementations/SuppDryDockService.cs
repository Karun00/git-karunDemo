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

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SuppDryDockService : ServiceBase, ISuppDryDockService
    {
        #region Declaration
        private ISuppDryDockRepository _suppDryDockRepository = null;
        private IPortConfigurationRepository _portConfigurationRepository;
        #endregion

        #region Constructor
        public SuppDryDockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _suppDryDockRepository = new SuppDryDockRepository(_unitOfWork);
        }

        public SuppDryDockService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());

            _UserId = GetUserIdByLoginname(_LoginName);
            _suppDryDockRepository = new SuppDryDockRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
        }
        #endregion

        #region Get Methods
        public List<SuppDryDockVO> GetSuppDryDockApplicationList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                CompanyVO nextStepCompany = _suppDryDockRepository.GetUserDetails(_UserId);
                var usertypeid = nextStepCompany.UserTypeId;
                return _suppDryDockRepository.GetSuppDryDockApplicationList(_PortCode, _UserId);
            });
        }

        public List<ServiceRequestVCNDetails> GetSuppVCNDetails(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                CompanyVO nextStepCompany = _suppDryDockRepository.GetUserDetails(_UserId);
                var usertypeid = nextStepCompany.UserTypeId;
                return _suppDryDockRepository.GetSuppVCNDetails(searchvalue, _UserId, _PortCode);
            });
        }

        public SuppDryDockVO GetSuppDryDockVCN(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppDryDockRepository.GetSuppDryDockVCN(vcn);
            });
        }

        #endregion

        #region CRUD Methods
        public SuppDryDockVO PostSuppDryDockApplication(SuppDryDockVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                SuppDryDock obj = data.MapToEntity();
                obj.ScheduleStatus = WFStatus.NewRequest;
                obj.DockPortCode = _PortCode;
                obj.CreatedBy = _UserId;
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedBy = _UserId;
                obj.ModifiedDate = DateTime.Now;
                obj.RecordStatus = "A";

                // -- changed by sandeep on 23-01-2015
                //obj.ApplicationDateTime = DateTime.Now;
                obj.ApplicationDateTime = obj.ModifiedDate;
                // -- end

                obj.ScheduleStatus = "NEW";
                //obj.ObjectState = ObjectState.Added;

                // _unitOfWork.Repository<SuppDryDock>().Insert(obj);

                //_unitOfWork.SaveChanges();

                //if (data.SuppDryDockDocuments.Count > 0)
                //{
                //    UpdateDocuments(data.SuppDryDockDocuments, obj.SuppDryDockID);
                //}
                //_unitOfWork.SaveChanges();

                #region Supplementary Dry Dock Approval Workflow Integration
                string remarks = "Supplementary Dry Dock";

                SuppDryDockWorkFlow suppDryDockWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, obj, remarks);
                WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                #endregion

                return data;
            });
        }

        public SuppDryDockVO PutSuppDryDockApplication(SuppDryDockVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SuppDryDock obj = data.MapToEntity();

                obj.ModifiedBy = _UserId;
                obj.ModifiedDate = DateTime.Now;

                //-- Added by sandeep on 29-01-2015
                obj.ApplicationDateTime = obj.ModifiedDate;
                // -- end

                //obj.ObjectState = ObjectState.Modified;

                //_unitOfWork.Repository<SuppDryDock>().Update(obj);

                //var drydockdocuments = _unitOfWork.Repository<SuppDryDockDocument>().Query().Select().Where(d => d.SuppDryDockID == obj.SuppDryDockID).ToList();

                //if (drydockdocuments.Count > 0)
                //{
                //    foreach (SuppDryDockDocument document in drydockdocuments)
                //    {
                //        _unitOfWork.Repository<SuppDryDockDocument>().Delete(document);
                //        _unitOfWork.SaveChanges();
                //    }
                //}

                //if (data.SuppDryDockDocuments.Count > 0)
                //{
                //    UpdateDocuments(data.SuppDryDockDocuments, obj.SuppDryDockID);
                //}

                //_unitOfWork.SaveChanges();


                #region Supplementary Plan Approval Workflow Integration
                string remarks = "Supplementary Dry Dock Updated";

                SuppDryDockWorkFlow suppDryDockWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, obj, remarks);
                WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, "UPDT");

                #endregion

                return data;
            });
        }
        #endregion

        #region Miscellaneous Methods
        private void UpdateDocuments(List<SuppDryDockDocumentVO> drydockdocument, int drydockid)
        {
            var documents = drydockdocument.MapToEntity();
            var lstDocuments = new List<SuppDryDockDocument>();

            foreach (SuppDryDockDocument document in documents)
            {
                document.SuppDryDockID = drydockid;
                document.CreatedBy = _UserId;
                document.CreatedDate = DateTime.Now;
                document.ModifiedBy = _UserId;
                document.ModifiedDate = DateTime.Now;
                document.ObjectState = ObjectState.Added;

                lstDocuments.Add(document);
            }

            _unitOfWork.Repository<SuppDryDockDocument>().InsertRange(lstDocuments);
        }
        #endregion



        /// <summary>
        /// To Approve Supplementary Dry Dock
        /// </summary>
        /// <param name="SuppDryDockID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                // changed by sandeep on 29-01-2015
                //var andata = _suppDryDockRepository.GetSuppDryDockApproveid(SuppDryDockID);
                CompanyVO nextStepCompany = _suppDryDockRepository.GetUserDetails(_UserId);
                var UserTypeID = nextStepCompany.UserTypeId;
                var andata = _suppDryDockRepository.GetSuppDryDockRequestDetailsByID(SuppDryDockID);
                // -- end
                SuppDryDockWorkFlow suppDryDockWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, taskcode);

            });
        }


        /// <summary>
        /// To Reject Supplementary Dry Dock
        /// </summary>
        /// <param name="SuppDryDockID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _suppDryDockRepository.GetSuppDryDockApproveid(SuppDryDockID);

                SuppDryDockWorkFlow suppDryDockWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To Confirm Supplementary Dry Dock
        /// </summary>
        /// <param name="SuppDryDockID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                // int userid = _accountRepository.GetUserID(_LoginName);
                //var andata = _suppDryDockRepository.GetSuppDryDockApproveid(SuppDryDockID);

                var andata = _suppDryDockRepository.GetSuppDryDockRequestDetailsByID(SuppDryDockID);

                // ServiceRequest_VO VO = new ServiceRequest_VO();
                SuppDryDockWorkFlow suppDryDockWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To Cancel Supplementary Dry Dock
        /// </summary>
        /// <param name="SuppDryDockID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void CancelSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _suppDryDockRepository.GetSuppDryDockApproveid(SuppDryDockID);

                SuppDryDockWorkFlow suppDryDockWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, taskcode);

            });
        }

        /// <summary>
        ///  To Get Supplementary Dry Dock Details based on Supplementary ID
        /// </summary>
        /// <param name="suppdrydockid"></param>
        /// <returns></returns>
        public List<SuppDryDockVO> GetSuppDryDock(int SuppDryDockID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppDryDockRepository.GetSuppDryDock(SuppDryDockID);

            });
        }

        #region GetDocumentTypes
        /// <summary>
        /// To Get Document types 
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDocumentTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var docTypes = (from s in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable()
                               where s.SupCatCode == "DADC"
                               select s).OrderByDescending(p=>p.SubCatName);
                return docTypes.ToList();
            });
        }
        #endregion

        //Add by Srinivas
        public SuppDryDockVO Cancel(SuppDryDockVO data)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                if (data.IsConfirmCancel == "Y")
                {
                    data.CreatedBy = _UserId;
                    data.CreatedDate = DateTime.Now;
                    data.ModifiedBy = _UserId;
                    data.ModifiedDate = DateTime.Now;
                    data.DockPortCode = _PortCode;
                    data.RecordStatus = "A";
                   // data.ApplicationDateTime=
                    SuppDryDock dockingplan = new SuppDryDock();
                    dockingplan = SuppDryDockMapExtension.MapToEntity(data);
                    dockingplan.ApplicationDateTime = Convert.ToDateTime(data.ApplicationDateTime);


                    #region Docking Plan Approval Workflow Integration
                    string remarks = "Cancelled";

                    SuppDryDockWorkFlow dockingPlanWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, dockingplan, remarks);
                    WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(dockingPlanWorkFlow, "WFCC");

                    #endregion
                }
                else
                {
                    data.CreatedBy = _UserId;
                    data.CreatedDate = DateTime.Now;
                    data.ModifiedBy = _UserId;
                    data.ModifiedDate = DateTime.Now;
                    data.DockPortCode = _PortCode;
                    data.RecordStatus = "A";
                    SuppDryDock dockingplan = new SuppDryDock();
                    dockingplan = SuppDryDockMapExtension.MapToEntity(data);
                    dockingplan.ApplicationDateTime = Convert.ToDateTime(data.ApplicationDateTime);


                    #region Docking Plan Approval Workflow Integration
                    string remarks = "Cancelled";

                    SuppDryDockWorkFlow dockingPlanWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, dockingplan, remarks);
                    WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(dockingPlanWorkFlow, "WFCA");

                    #endregion
                }
                return data;
            });
        }

        /// <summary>
        /// To Approve Dry Dock Confirm cancel in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void ApproveCancelConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _suppDryDockRepository.GetSuppDryDockApproveid(SuppDryDockID);
                SuppDryDockWorkFlow suppDryDockWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, taskcode);
            });
        }


        /// <summary>
        /// To Reject Dry Dock Confirm cancel in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void RejectCancelConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                CompanyVO nextStepCompany = _suppDryDockRepository.GetUserDetails(_UserId);
                var UserTypeID = nextStepCompany.UserTypeId;
                var andata = _suppDryDockRepository.GetSuppDryDockRequestDetailsByID(SuppDryDockID);                

                SuppDryDockWorkFlow suppDryDockWorkFlow = new SuppDryDockWorkFlow(_unitOfWork, andata, comments);
                WorkFlowEngine<SuppDryDockWorkFlow> wf = new WorkFlowEngine<SuppDryDockWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(suppDryDockWorkFlow, taskcode);

            });
        }


    }
}
