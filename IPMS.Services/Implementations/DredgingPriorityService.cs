using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Globalization;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                  ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DredgingPriorityService : ServiceBase, IDredgingPriorityService
    {
        private ISubCategoryRepository _subcategoryRepository;
        private IDredgingPriorityRepository _dredgingpriorityRepository;
       // private IWorkFlowEngine<BerthOccupationWorkFlow> wfEngine;
       // private IAccountRepository _accountRepository;
        private IPortConfigurationRepository _portConfigurationRepository;
       // private IPortRepository _PortRepository;

        public DredgingPriorityService(IUnitOfWork unitOfWork)
        {
           // wfEngine = 
                new WorkFlowEngine<BerthOccupationWorkFlow>();
            //_accountRepository = 
                new AccountRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
           // _PortRepository = 
                new PortRepository(_unitOfWork);
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _dredgingpriorityRepository = new DredgingPriorityRepository(_unitOfWork);

        }

        public DredgingPriorityService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _dredgingpriorityRepository = new DredgingPriorityRepository(_unitOfWork);

        }

        /// <summary>
        ///  To Get Dredging Priority Reference data While initialization
        /// </summary>
        /// <returns></returns>
        public DredgingPriorityVO GetDredgingPriorityReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                DredgingPriorityVO VO = new DredgingPriorityVO();
                VO.FinancialYears = _subcategoryRepository.GetFinancialYears().MapToDto();
                VO.DredgingTypes = _subcategoryRepository.GetDeploymentTypes().MapToDto();
                VO.BerthTypes = _dredgingpriorityRepository.GetBerthTypes(_PortCode);
                VO.LocationTypes = _dredgingpriorityRepository.GetLocationTypes(_PortCode);

                return VO;
            });
        }
        /// <summary>
        /// Gets Months
        /// </summary>
        /// <returns></returns>
        public List<FinancialYearVO> GetMonths(int financialYearId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetMonths(financialYearId);
            });
        }
        /// <summary>
        /// To Get Financial Year Data
        /// </summary>
        /// <returns></returns>
        public List<FinancialYearVO> GetFinancialYear()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetFinancialYear();

            });
        }
        /// <summary>
        ///  To Get Dredging Priority Details
        /// </summary>
        /// <returns></returns>
        public List<DredgingPriorityVO> DredgingPriorityDetails(int financialYearId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetDredgingPriorityDetails(financialYearId, _PortCode);
            });
        }
        /// <summary>
        /// To Get Dredging Priority Volumes
        /// </summary>
        /// <returns></returns>
        public List<DredgingPriorityVolumeVO> GetDredgingPriorityVolumes(int financialYearId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetDredgingPriorityVolumes(financialYearId, _PortCode);
            });
        }

        /// <summary>
        /// This method is used for fetch the Berth data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<DredgingPriorityVO> GetBerthTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetBerthTypes(_PortCode);

            });
        }
        /// <summary>
        /// This method is used for fetch the Location data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<DredgingPriorityVO> GetLocationTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetLocationTypes(_PortCode);
            });
        }

        public DeploymentPlanVO GetDeploymentId(int financialYearId, string portCode)
        {

            var deploymentplan = (from dp in _unitOfWork.Repository<DeploymentPlan>().Query().Select()
                                  where dp.FinancialYearID == financialYearId && portCode == _PortCode
                                  select new DeploymentPlanVO
                         {
                             DeploymentPlanID = dp.DeploymentPlanID
                         }).FirstOrDefault();
            return deploymentplan;
           
        }
        
        public DredgingPriorityVO AddDredgingPriorityDetails(DredgingPriorityVO dredgingPriorityData)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                dredgingPriorityData.CreatedBy = _UserId;
                dredgingPriorityData.CreatedDate = DateTime.Now;
                dredgingPriorityData.ModifiedBy = _UserId;
                dredgingPriorityData.ModifiedDate = DateTime.Now;
                dredgingPriorityData.RecordStatus = "A";
                int financialid = Convert.ToInt32(dredgingPriorityData.FinancialYearID, CultureInfo.InvariantCulture);
                //var deploymentplan = GetDeploymentId(financialid, _PortCode);
               // DredgingPrioritydata.DeploymentPlanID = deploymentplan.DeploymentPlanID;
                dredgingPriorityData.DeploymentPlanID = _unitOfWork.Repository<DeploymentPlan>().Query().Select().Where(s => s.FinancialYearID == financialid && s.PortCode ==_PortCode).Select(g => g.DeploymentPlanID).FirstOrDefault();
                dredgingPriorityData.PortCode = _PortCode;
                
                DredgingPriority dredgingPriority = new DredgingPriority();
                
                dredgingPriority = DredgingPriorityMapExtension.MapToEntity(dredgingPriorityData);

                dredgingPriority.DredgingPriorityDocuments = null;
                dredgingPriority.DredgingOperations = null;
                if (dredgingPriority.DredgingPriorityID == 0)
                {
                    _unitOfWork.Repository<DredgingPriority>().Insert(dredgingPriority);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    dredgingPriority.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<DredgingPriority>().Update(dredgingPriority);
                    _unitOfWork.SaveChanges();
                }
                dredgingPriorityData.DredgingPriorityID = dredgingPriority.DredgingPriorityID;
                dredgingPriority = dredgingPriorityData.MapToEntity();

                List<DredgingPriorityDocument> dredgingpriorityDocumentList = dredgingPriority.DredgingPriorityDocuments.ToList();
               // var brt =
                _unitOfWork.ExecuteSqlCommand(" delete dbo.DredgingPriorityDocument where DredgingPriorityID = @p0", dredgingPriority.DredgingPriorityID);

                if (dredgingpriorityDocumentList.Count() > 0)
                {
                    foreach (var dredgingdocument in dredgingpriorityDocumentList)
                    {
                        dredgingdocument.DredgingPriorityID = dredgingPriority.DredgingPriorityID;
                        dredgingdocument.RecordStatus = "A";
                        dredgingdocument.CreatedBy = _UserId;
                        dredgingdocument.CreatedDate = dredgingPriority.CreatedDate;
                        dredgingdocument.ModifiedBy = _UserId;
                        dredgingdocument.ModifiedDate = DateTime.Now;

                    }
                    _unitOfWork.Repository<DredgingPriorityDocument>().InsertRange(dredgingpriorityDocumentList);
                    _unitOfWork.SaveChanges();
                }

                List<DredgingOperation> dredgingpriorityArealist = dredgingPriority.DredgingOperations.ToList();

                if (dredgingpriorityArealist.Count() > 0)
                {
                    foreach (var dredgingArea in dredgingpriorityArealist)
                    {
                        if (dredgingArea.DredgingOperationID == 0)
                        {
                            dredgingArea.DredgingPriorityID = dredgingPriority.DredgingPriorityID;
                            dredgingArea.RecordStatus = "A";
                            dredgingArea.PortCode = _PortCode;
                            dredgingArea.DredgingStatus = "DPAL";
                            dredgingArea.IsDPAFinal = "N";
                            dredgingArea.OccupationFrom = null;
                            dredgingArea.OccupationTo = null;
                            dredgingArea.VolumeOccupationFrom = null;
                            dredgingArea.VolumeOccupationTo = null;
                            dredgingArea.CreatedBy = dredgingPriority.CreatedBy;
                            dredgingArea.CreatedDate = dredgingPriority.CreatedDate;
                            dredgingArea.ModifiedBy = dredgingPriority.ModifiedBy;
                            dredgingArea.ModifiedDate = dredgingPriority.ModifiedDate;
                            dredgingArea.Month = dredgingPriority.Month;
                            dredgingArea.PortName = _unitOfWork.Repository<Port>().Query().Select().Where(s =>s.PortCode == _PortCode).Select(g => g.PortName).FirstOrDefault();
                            #region Dredging Priority Workflow Integration
                string remarks = "Dredging Priority";
                            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
                            DredgingPriorityWorkFlow dredgingpriorityWorkFlow = new DredgingPriorityWorkFlow(_unitOfWork, dredgingArea, remarks);
                            WorkFlowEngine<DredgingPriorityWorkFlow> wf = new WorkFlowEngine<DredgingPriorityWorkFlow>(_unitOfWork, _PortCode, _UserId);
                            wf.Process(dredgingpriorityWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                            #endregion
                        }
                        else
                        {
                            dredgingArea.DredgingPriorityID = dredgingPriority.DredgingPriorityID;
                            dredgingArea.RecordStatus = "A";
                            dredgingArea.PortCode = _PortCode;
                            dredgingArea.DredgingStatus = "DPAL";
                            dredgingArea.IsDPAFinal = "N";
                            dredgingArea.OccupationFrom = null;
                            dredgingArea.OccupationTo = null;
                            dredgingArea.VolumeOccupationFrom = null;
                            dredgingArea.VolumeOccupationTo = null;
                            dredgingArea.CreatedBy = dredgingPriority.CreatedBy;
                            dredgingArea.CreatedDate = dredgingPriority.CreatedDate;
                            dredgingArea.ModifiedBy = dredgingPriority.ModifiedBy;
                            dredgingArea.ModifiedDate = dredgingPriority.ModifiedDate;
                            dredgingArea.Month = dredgingPriority.Month;
                            dredgingArea.PortName = _unitOfWork.Repository<Port>().Query().Select().Where(s => s.PortCode == _PortCode).Select(g => g.PortName).FirstOrDefault();

                            #region Dredging Priority Workflow Integration
                            string remarks = "Update Dredging Priority";
                _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
                            DredgingPriorityWorkFlow dredgingpriorityWorkFlow = new DredgingPriorityWorkFlow(_unitOfWork, dredgingArea, remarks);
                WorkFlowEngine<DredgingPriorityWorkFlow> wf = new WorkFlowEngine<DredgingPriorityWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dredgingpriorityWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                            #endregion

                        }

             

                    }
                }


                return dredgingPriorityData;
            });
                
        }
                

        /// <summary>
        /// To Update Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="DredgingPrioritydata"></param>
        /// <returns></returns>
        public DredgingPriorityVO ModifyDredgingPriorityDetails(DredgingPriorityVO dredgingPriorityData)
        {
            return EncloseTransactionAndHandleException(() =>
            {

                DredgingPriority dr = null;
                dr = dredgingPriorityData.MapToEntity();
                dr.ObjectState = ObjectState.Modified;

                //int userid = _accountRepository.GetUserID(_LoginName);
                dr.CreatedBy = _UserId;
                dr.CreatedDate = DateTime.Now;
                dr.ModifiedBy = _UserId;
                dr.ModifiedDate = DateTime.Now;
                dr.RecordStatus = "A";

                //-------------------------------
                dr.DredgingPriorityDocuments = null;
                //dr.DredgingPriorityAreas = null;

                _unitOfWork.Repository<DredgingPriority>().Update(dr);
                _unitOfWork.SaveChanges();
                dredgingPriorityData.DredgingPriorityID = dr.DredgingPriorityID;
                dredgingPriorityData.CreatedDate = dr.CreatedDate;
                dr = dredgingPriorityData.MapToEntity();

        


                List<DredgingPriorityDocument> dredgingpriorityDocumentList = dr.DredgingPriorityDocuments.ToList();
               // var delDredgingDocument = 
                _unitOfWork.ExecuteSqlCommand(" delete dbo.DredgingPriorityDocument where DredgingPriorityID = @p0", dredgingPriorityData.DredgingPriorityID);
                if (dredgingpriorityDocumentList.Count() > 0)
                {
                    foreach (var dredgingdocument in dredgingpriorityDocumentList)
                    {
                        dredgingdocument.DredgingPriorityID = dr.DredgingPriorityID;
                        dredgingdocument.CreatedBy = _UserId;
                        dredgingdocument.CreatedDate = dr.CreatedDate;
                        dredgingdocument.ModifiedBy = _UserId;
                        dredgingdocument.ModifiedDate = DateTime.Now;

                    }
                    _unitOfWork.Repository<DredgingPriorityDocument>().InsertRange(dredgingpriorityDocumentList);
                    _unitOfWork.SaveChanges();
                }
          

                return dredgingPriorityData;

            });
        }     


        /// <summary>
        ///  To Approve Dredging Priority Request
        /// </summary>
        /// <param name="DredgingPriorityID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveDredgingPriority(string dredgingPriorityId, string remarks, string taskCode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _dredgingpriorityRepository.GetDredgingPriorityApproveId(dredgingPriorityId);
               

                DredgingPriorityWorkFlow dredgingpriorityWorkFlow = new DredgingPriorityWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DredgingPriorityWorkFlow> wf = new WorkFlowEngine<DredgingPriorityWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dredgingpriorityWorkFlow, taskCode);
               // var delDredgingDocument =
                _unitOfWork.ExecuteSqlCommand(" update dbo.DredgingOperation set DredgingStatus = 'DPAP', IsDPAFinal = 'Y' where DredgingOperationID = @p0", dredgingPriorityId);
           
            });
        }

        /// <summary>
        /// To Reject Dredging Priority Request
        /// </summary>
        /// <param name="DredgingPriorityID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectDredgingPriority(string dredgingPriorityId, string remarks, string taskCode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _dredgingpriorityRepository.GetDredgingPriorityApproveId(dredgingPriorityId);

                DredgingPriorityWorkFlow dredgingpriorityWorkFlow = new DredgingPriorityWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DredgingPriorityWorkFlow> wf = new WorkFlowEngine<DredgingPriorityWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dredgingpriorityWorkFlow, taskCode);
               // var delDredgingDocument =
                _unitOfWork.ExecuteSqlCommand(" update dbo.DredgingOperation set DredgingStatus = 'DPRE', IsDPAFinal = 'R' where DredgingOperationID = @p0", dredgingPriorityId);
           
            });
        }

        /// <summary>
        ///  To get  Dredging Priority based on DredgingPriorityID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingpriorityid"></param>
        /// <returns></returns>
        public List<DredgingPriorityVO> GetDredgingPriorityPendingView(int dredgingPriorityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetDredgingPriorityPendingView(dredgingPriorityId);
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Dec 2014
        /// Purpose : To get List of Berth Occupation details
        /// </summary>
        /// <returns></returns>
        public List<DredgingOperationVO> GetBerthOccupationList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetBerthOccupationList();
            });
        }
        public List<DredgingOperationVO> GetBerthOccupationById(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetBerthOccupationById(id);
            });
        }
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Dec 2014
        /// Purpose : To get List of Berth Occupation details
        /// </summary>
        /// <returns></returns>
        public List<DredgingOperationVO> GetDredgingVolumeList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetDredgingVolumeList();
            });
        }
        public List<DredgingOperationVO> GetDredgingVolumeById(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetDredgingVolumeById(id);
            });
        }

        public DredgingOperationVO UpdateDredgingVolume(DredgingOperationVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                DredgingOperation dredgingVolume = data.MapToEntity();

                dredgingVolume.RecordStatus = "A";
                dredgingVolume.ObjectState = ObjectState.Modified;
                dredgingVolume.ModifiedBy = _UserId;
                dredgingVolume.ModifiedDate = DateTime.Now;
                dredgingVolume.DredgingStatus = "DVPO";
                if (string.IsNullOrEmpty(data.OccupationFrom))
                {
                    dredgingVolume.OccupationFrom = null;
                }
                if(string.IsNullOrEmpty(data.OccupationTo ))
                {
                    dredgingVolume.OccupationTo = null;
                }
                dredgingVolume.PortName = _unitOfWork.Repository<Port>().Query().Select().Where(s => s.PortCode == _PortCode).Select(g => g.PortName).FirstOrDefault();

                _unitOfWork.Repository<DredgingOperation>().Update(dredgingVolume);

                #region Dredging Volume Approval Workflow Integration
                string remarks = "Dredging Volume";

                _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
                DredgingVolumeWorkFlow dredgingVolumeWorkFlow = new DredgingVolumeWorkFlow(_unitOfWork, dredgingVolume, remarks);
                WorkFlowEngine<DredgingVolumeWorkFlow> wf = new WorkFlowEngine<DredgingVolumeWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dredgingVolumeWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);


                #endregion

                _unitOfWork.SaveChanges();

                return data;
            });
        }

        public DredgingOperationVO UpdateBerthOccupation(DredgingOperationVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                DredgingOperation dredgingOperation = data.MapToEntity();

                dredgingOperation.VolumeOccupationFrom = null;
                dredgingOperation.VolumeOccupationTo = null;
                if(string.IsNullOrEmpty(data.OccupationFrom ))
                {
                    dredgingOperation.OccupationFrom = null;
                }
                if (string.IsNullOrEmpty(data.OccupationTo))
                {
                    dredgingOperation.OccupationTo = null;
                }
                dredgingOperation.RecordStatus = "A";
                dredgingOperation.ObjectState = ObjectState.Modified;
                dredgingOperation.ModifiedBy = _UserId;
                dredgingOperation.ModifiedDate = DateTime.Now;
                dredgingOperation.DredgingStatus = "DORA";
                dredgingOperation.PortName = _unitOfWork.Repository<Port>().Query().Select().Where(s => s.PortCode == _PortCode).Select(g => g.PortName).FirstOrDefault();
                _unitOfWork.Repository<DredgingOperation>().Update(dredgingOperation);
                _unitOfWork.SaveChanges();

                if (data.BerthOccupationDocumentVO.Count > 0)
                {
                    List<BerthOccupationDocumentVO> berthOccupationDocuments = data.BerthOccupationDocumentVO;
                    var _berthOccupationDocuments = new List<BerthOccupationDocument>();
                    var lstBerthOccupationDocuments = _unitOfWork.Repository<BerthOccupationDocument>().Queryable().Where(bd => bd.DredgingOperationID == dredgingOperation.DredgingOperationID).ToList();

                    foreach (BerthOccupationDocument document in lstBerthOccupationDocuments)
                    {
                        _unitOfWork.Repository<BerthOccupationDocument>().Delete(document);
                        _unitOfWork.SaveChanges();
                    }

                    BerthOccupationDocument doc = null;

                    doc = UpdBerthOccupation(data, berthOccupationDocuments, _berthOccupationDocuments, doc);

                    _unitOfWork.Repository<BerthOccupationDocument>().InsertRange(_berthOccupationDocuments);

                   
                    
                    _unitOfWork.SaveChanges();
                }
                #region Docking Plan Approval Workflow Integration
                string remarks = "Dredging Berth Occupation";

                _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
                BerthOccupationWorkFlow berthOcuupationWorkFlow = new BerthOccupationWorkFlow(_unitOfWork, dredgingOperation, remarks);
                WorkFlowEngine<BerthOccupationWorkFlow> wf = new WorkFlowEngine<BerthOccupationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(berthOcuupationWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);


                #endregion

                return data;
            });
        }

        private BerthOccupationDocument UpdBerthOccupation(DredgingOperationVO data, List<BerthOccupationDocumentVO> berthOccupationDocuments, List<BerthOccupationDocument> _berthOccupationDocuments, BerthOccupationDocument doc)
        {
            foreach (BerthOccupationDocumentVO document in berthOccupationDocuments)
            {
                doc = new BerthOccupationDocument();

                doc.DredgingOperationID = data.DredgingOperationID;
                doc.DocumentID = document.DocumentID;
                doc.RecordStatus = RecordStatus.Active;
                doc.CreatedBy = _UserId;
                doc.CreatedDate = DateTime.Now;
                doc.ModifiedBy = _UserId;
                doc.ModifiedDate = DateTime.Now;
                doc.ObjectState = ObjectState.Added;

                _berthOccupationDocuments.Add(doc);
                doc = null;
            }
            return doc;
        }




        /// <summary>
        /// This method is used for approve 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveBerthOccupation(string dredgingPriorityAreaId, string remarks, string taskCode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _dredgingpriorityRepository.GetDredgingPriorityApproveId(dredgingPriorityAreaId);


                BerthOccupationWorkFlow dredgingpriorityWorkFlow = new BerthOccupationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<BerthOccupationWorkFlow> wf = new WorkFlowEngine<BerthOccupationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dredgingpriorityWorkFlow, taskCode);

               // var delDredgingDocument =
                _unitOfWork.ExecuteSqlCommand(" update dbo.DredgingOperation set DredgingStatus = 'DOAP', IsDOFinal = 'Y' where DredgingOperationID = @p0", dredgingPriorityAreaId);
               



            });
        }

        /// <summary>
        /// This method is used for reject 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectBerthOccupation(string dredgingPriorityAreaId, string remarks, string taskCode)
        { //ExecuteFaultHandledOperation
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _dredgingpriorityRepository.GetDredgingPriorityApproveId(dredgingPriorityAreaId);

                BerthOccupationWorkFlow dredgingpriorityWorkFlow = new BerthOccupationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<BerthOccupationWorkFlow> wf = new WorkFlowEngine<BerthOccupationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dredgingpriorityWorkFlow, taskCode);
               // var delDredgingDocument = 
                _unitOfWork.ExecuteSqlCommand(" update dbo.DredgingOperation set DredgingStatus = 'DORE', IsDOFinal = 'R' where DredgingOperationID = @p0", dredgingPriorityAreaId);
           

            });
        }

        /// <summary>
        /// This method is used for approve 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveDredgingVolume(string dredgingPriorityAreaId, string remarks, string taskCode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _dredgingpriorityRepository.GetDredgingPriorityApproveId(dredgingPriorityAreaId);


                DredgingVolumeWorkFlow dredgingpriorityWorkFlow = new DredgingVolumeWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DredgingVolumeWorkFlow> wf = new WorkFlowEngine<DredgingVolumeWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dredgingpriorityWorkFlow, taskCode);

                //var delDredgingDocument = 
                    _unitOfWork.ExecuteSqlCommand(" update dbo.DredgingOperation set DredgingStatus = 'DVAP', IsDVFinal = 'Y' where DredgingOperationID = @p0", dredgingPriorityAreaId);





            });
        }

        /// <summary>
        /// This method is used for reject 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectDredgingVolume(string dredgingPriorityAreaId, string remarks, string taskCode)
        { //ExecuteFaultHandledOperation
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _dredgingpriorityRepository.GetDredgingPriorityApproveId(dredgingPriorityAreaId);

                DredgingVolumeWorkFlow dredgingpriorityWorkFlow = new DredgingVolumeWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DredgingVolumeWorkFlow> wf = new WorkFlowEngine<DredgingVolumeWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dredgingpriorityWorkFlow, taskCode);
              //  var delDredgingDocument =
                _unitOfWork.ExecuteSqlCommand(" update dbo.DredgingOperation set DredgingStatus = 'DVRE', IsDVFinal = 'R' where DredgingOperationID = @p0", dredgingPriorityAreaId);
           

            });
        }


        /// <summary>
        /// To Get Dredging Area Details
        /// </summary>
        /// <returns></returns>
        public List<DredgingOperationVO> DredgingPriorityAreaDetails(int dredgingPriorityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetDredgingPriorityAreaDetails(dredgingPriorityId);
            });
        }
        /// <summary>
        /// To Get Dredging Area Details
        /// </summary>
        /// <returns></returns>
        public List<DredgingOperationVO> DredgingPriorityAreaDetailsPending(int dredgingPriorityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetDredgingPriorityAreaDetailsPending(dredgingPriorityId);
            });
        }

        /// <summary>
        /// To Get Document Details
        /// </summary>
        /// <returns></returns>
        public List<DredgingPriorityDocumentVO> GetDocument(int dredgingPriorityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dredgingpriorityRepository.GetDocument(dredgingPriorityId);
            });
        }

        /// <summary>
        /// To Cancel Berth occupation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DredgingOperationVO Cancel(DredgingOperationVO data)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                string remarks = data.workflowRemarks;
                DredgingOperation DreObj = null;
                DreObj = data.MapToEntity();
              //  int userid = _accountRepository.GetUserID(_LoginName);
                try
                {

                    DreObj.CreatedBy = _UserId;
                    DreObj.CreatedDate = DateTime.Now;
                    DreObj.ModifiedBy = _UserId;
                    DreObj.ModifiedDate = DateTime.Now;
                    DreObj.RecordStatus = "A";
                    DreObj.IsDOFinal = "c";
                    DreObj.DredgingStatus = "DOCA";
                }
                catch (Exception ex)
                {
                    throw new FaultException("_LoginName should not be empty, Exception = " + ex);
                }
                #region Workflow Integration

                #region Berth Occupation Workflow Integration
                _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
                BerthOccupationWorkFlow berthOcuupationWorkFlow = new BerthOccupationWorkFlow(_unitOfWork, DreObj, remarks);
                WorkFlowEngine<BerthOccupationWorkFlow> wf = new WorkFlowEngine<BerthOccupationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(berthOcuupationWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).CancelCode);
              //  var delDredgingDocument =
                _unitOfWork.ExecuteSqlCommand(" update dbo.DredgingOperation set  IsDOFinal = 'C' where DredgingOperationID = @p0", data.DredgingOperationID);

                #endregion
                #endregion
                return data;
               
               
            });
        }
    }
}
