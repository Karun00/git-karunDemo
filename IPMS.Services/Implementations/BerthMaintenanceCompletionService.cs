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

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
       ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BerthMaintenanceCompletionService:ServiceBase,IBerthMaintenanceCompletionService
    {
      
       // private IWorkFlowEngine<BerthMaintenanceCompletionWorkFlow> wfEngine;    
        private IPortConfigurationRepository _portConfigurationRepository;
        private IBerthMaintenanceCompletionRepository _berthMaintenanceCompletionRepository;

        public BerthMaintenanceCompletionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _berthMaintenanceCompletionRepository = new BerthMaintenanceCompletionRepository(_unitOfWork);
        }

        public BerthMaintenanceCompletionService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
          //  wfEngine = new WorkFlowEngine<BerthMaintenanceCompletionWorkFlow>();          
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _berthMaintenanceCompletionRepository = new BerthMaintenanceCompletionRepository(_unitOfWork);
            // TODO: Complete member initialization
        }        
     
        /// <summary>
        ///  To Add Berth Maintenance Completion Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BerthMaintenanceCompletionVO AddBerthMaintenanceCompletion(BerthMaintenanceCompletionVO data) 
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string name = _LoginName;              
                data.CreatedBy = _UserId;
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                BerthMaintenanceCompletion BerthMaintComp = new BerthMaintenanceCompletion();
                BerthMaintComp = BerthMaintenanceCompletionMapExtension.MapToEntity(data);

                BerthMaintComp.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<BerthMaintenanceCompletion>().Insert(BerthMaintComp);
            
                #region Berth Maintenance Completion Approval Workflow Integration
                string remarks = "New Berth Maintenance Completion";

                BerthMaintenanceCompletionWorkFlow berthMaintenanceCompletionWorkFlow = new BerthMaintenanceCompletionWorkFlow(_unitOfWork, BerthMaintComp, remarks);
                WorkFlowEngine<BerthMaintenanceCompletionWorkFlow> wf = new WorkFlowEngine<BerthMaintenanceCompletionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(berthMaintenanceCompletionWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                #endregion

                return data;
            });            
        }

        /// <summary>
        /// To Modify Berth Maintenance Completion Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BerthMaintenanceCompletionVO ModifyBerthMaintenanceCompletion(BerthMaintenanceCompletionVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
               // string name = _LoginName;              
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                BerthMaintenanceCompletion BerthMaintComp = new BerthMaintenanceCompletion();
                BerthMaintComp = BerthMaintenanceCompletionMapExtension.MapToEntity(data);

                BerthMaintComp.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<BerthMaintenanceCompletion>().Update(BerthMaintComp);          

                #region Berth Maintenance Approval Workflow Integration
                string remarks = "Berth Maintenance Completion Updated";

                BerthMaintenanceCompletionWorkFlow berthMaintenanceCompletionWorkFlow = new BerthMaintenanceCompletionWorkFlow(_unitOfWork, BerthMaintComp, remarks);
                WorkFlowEngine<BerthMaintenanceCompletionWorkFlow> wf = new WorkFlowEngine<BerthMaintenanceCompletionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(berthMaintenanceCompletionWorkFlow, "UPDT");

                #endregion
                return data;
            });
        }

        /// <summary>
        ///  To get Berth Maintenance Completion Details
        /// </summary>
        /// <returns></returns>
        public List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletionList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthMaintenanceCompletionRepository.GetBerthMaintenanceCompletionList(_PortCode);         
             
            });
        }    
            
    
        /// <summary>
        /// To Get Berth Maintenance Ids
        /// </summary>
        /// <returns></returns>
        public List<DataVO> GetBethMaintenanceIDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthMaintenanceCompletionRepository.GetBethMaintenanceIDs(_PortCode);              
            });

        }

        /// <summary>
        /// To Get Berth Maintenance Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DataVO> BethMaintenanceDetails(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthMaintenanceCompletionRepository.BethMaintenanceDetails(id);          

            });
        }

        /// <summary>
        /// To Approve Berth Maintenance Completion
        /// </summary>
        /// <param name="BerthMaintenanceCompletionID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveBerthMaintenanceCompletion(string berthmaintenancecompletionid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _berthMaintenanceCompletionRepository.GetApproveid(berthmaintenancecompletionid);

                BerthMaintenanceCompletionWorkFlow berthMaintenanceCompletionWorkFlow = new BerthMaintenanceCompletionWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<BerthMaintenanceCompletionWorkFlow> wf = new WorkFlowEngine<BerthMaintenanceCompletionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(berthMaintenanceCompletionWorkFlow, taskcode);

            });
        }

        /// <summary>
        ///  To Reject Berth Maintenance Completion
        /// </summary>
        /// <param name="BerthMaintenanceCompletionID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectBerthMaintenanceCompletion(string berthmaintenancecompletionid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                //var andata = (from t in _unitOfWork.Repository<BerthMaintenanceCompletion>().Query().Select()
                //              where t.BerthMaintenanceCompletionID == Convert.ToInt32(BerthMaintenanceCompletionID)
                //              select t).FirstOrDefault<BerthMaintenanceCompletion>();
                var andata = _berthMaintenanceCompletionRepository.GetApproveid(berthmaintenancecompletionid);

                BerthMaintenanceCompletionWorkFlow berthMaintenanceCompletionWorkFlow = new BerthMaintenanceCompletionWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<BerthMaintenanceCompletionWorkFlow> wf = new WorkFlowEngine<BerthMaintenanceCompletionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(berthMaintenanceCompletionWorkFlow, taskcode);

            });
        }

        /// <summary>
        /// To Get Berth Maintenance Completion Details By ID
        /// </summary>
        /// <param name="berthMaintenanceCompletionId"></param>
        /// <returns></returns>
        public List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletion(int berthMaintenanceCompletionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthMaintenanceCompletionRepository.GetBerthMaintenanceCompletion(berthMaintenanceCompletionId);    

            });
        }
    }
}
