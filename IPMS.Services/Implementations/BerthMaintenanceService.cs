using Core.Repository;
using IPMS.Data.Context;
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


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
      ConcurrencyMode = ConcurrencyMode.Multiple)]
      public class BerthMaintenanceService : ServiceBase, IBerthMaintenanceService
      {
       
        //private IWorkFlowEngine<BerthMaintenanceApprovalWorkFlow> wfEngine;     
        private IPortConfigurationRepository _portConfigurationRepository;
        private ISubCategoryRepository _subcategoryRepository;
      //  private IDepartmentRepository _departmentRepository;    
        private IBerthRepository _berthRepository;
        private IBerthMaintenanceRepository _berthMaintenanceRepository;

          public BerthMaintenanceService(IUnitOfWork unitOfWork)
          {
               _unitOfWork = unitOfWork;
               _UserId = GetUserIdByLoginname(_LoginName);
               _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
               _berthRepository = new BerthRepository(_unitOfWork);              
               _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
             //  _departmentRepository = new DepartmentRepository(_unitOfWork);
               _berthMaintenanceRepository = new BerthMaintenanceRepository(_unitOfWork);
          }
          public BerthMaintenanceService()
          {
              _unitOfWork = new UnitOfWork(new TnpaContext());
              _UserId = GetUserIdByLoginname(_LoginName);
           //    wfEngine = new WorkFlowEngine<BerthMaintenanceApprovalWorkFlow>();
              _berthRepository = new BerthRepository(_unitOfWork);
              _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
          //    _departmentRepository = new DepartmentRepository(_unitOfWork);
              _berthMaintenanceRepository = new BerthMaintenanceRepository(_unitOfWork);
              _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
          }    
        

        /// <summary>
        ///  To get Berth Maintenance Details
        /// </summary>
        /// <returns></returns>
        public List<BerthMaintenanceVO> GetBerthMaintenanceDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _berthMaintenanceRepository.GetBerthMaintenanceDetails(_PortCode);              
            });
        }

        /// <summary>
        ///  To Get Berth Maintenance Reference data While initialization
        /// </summary>
        /// <returns></returns>
        public BerthMaintenanceVO GetBerthMaintenanceReferenceVO()
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 BerthMaintenanceVO VO = new BerthMaintenanceVO();                
                 VO.Berths = _berthRepository.GetBerths(_PortCode);
                 VO.DepartmentTypes = _subcategoryRepository.GetDepartments().MapToDto();
                 VO.MaintenanceTypes = _subcategoryRepository.MaintenanceType().MapToDto();               
                 return VO;
             });
         }

        /// <summary>
        ///  To Add Berth Maintenance Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         public BerthMaintenanceVO AddBerthMaintenance(BerthMaintenanceVO data)
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 //string name = _LoginName;  
                 data.PortCode = _PortCode;
                 data.CreatedBy = _UserId;
                 data.CreatedDate = DateTime.Now;
                 data.ModifiedBy = _UserId;
                 data.ModifiedDate = DateTime.Now;
                 CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                 data.BerthMaintenanceNo = codeGenerator.GenerateCode("BERT", _PortCode);

                 BerthMaintenance berthmaintenance = new BerthMaintenance();
                 berthmaintenance = BerthMaintenanceMapExtension.MapToEntity(data);               
                 berthmaintenance.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<BerthMaintenance>().Insert(berthmaintenance);

                 codeGenerator.UpdateCode("BERT", berthmaintenance.PortCode);

                 #region Berth Maintenance Approval Workflow Integration
                 string remarks = "New Berth Maintenance";

                 BerthMaintenanceApprovalWorkFlow berthMaintenanceApprovalWorkFlow = new BerthMaintenanceApprovalWorkFlow(_unitOfWork, berthmaintenance, remarks);
                 WorkFlowEngine<BerthMaintenanceApprovalWorkFlow> wf = new WorkFlowEngine<BerthMaintenanceApprovalWorkFlow>(_unitOfWork, _PortCode, _UserId);
                 wf.Process(berthMaintenanceApprovalWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                 #endregion
                 return data;
             });
         }

        /// <summary>
         /// To Modify Berth Maintenance Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         public BerthMaintenanceVO ModifyBerthMaintenance(BerthMaintenanceVO data)
         {
             return ExecuteFaultHandledOperation(() =>
             {
                // string name = _LoginName;  
                 data.PortCode = _PortCode;
                 BerthMaintenance berthmaintenance = new BerthMaintenance();
                 berthmaintenance = BerthMaintenanceMapExtension.MapToEntity(data);
                 berthmaintenance.ModifiedBy = _UserId;
                 berthmaintenance.ModifiedDate = DateTime.Now;
                 berthmaintenance.ObjectState = ObjectState.Modified;
                 _unitOfWork.Repository<BerthMaintenance>().Update(berthmaintenance);               

                 #region Berth Maintenance Approval Workflow Integration
                 string remarks = "Berth Maintenance Updated";

                 BerthMaintenanceApprovalWorkFlow berthMaintenanceApprovalWorkFlow = new BerthMaintenanceApprovalWorkFlow(_unitOfWork, berthmaintenance, remarks);
                 WorkFlowEngine<BerthMaintenanceApprovalWorkFlow> wf = new WorkFlowEngine<BerthMaintenanceApprovalWorkFlow>(_unitOfWork, _PortCode, _UserId);
                // wf.Process(berthMaintenanceApprovalWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                 wf.Process(berthMaintenanceApprovalWorkFlow, "UPDT");

                 #endregion


                 return data;
             });
         }

        /// <summary>
         /// To Get Bollards based on Berth
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <param name="berthCode"></param>
        /// <returns></returns>
         public List<BollardVO> GetBerthBollards(string portCode, string quayCode, string berthCode)
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 return _berthMaintenanceRepository.GetBerthBollards(portCode, quayCode, berthCode);
             });
        
         }

         /// <summary>
         ///  To Approve Berth Maintenance Request
         /// </summary>
         /// <param name="BerthMaintenanceID"></param>
         /// <param name="comments"></param>
         /// <param name="taskcode"></param>
         public void ApproveBerthMaintenance(string berthmaintenanceid, string remarks, string taskcode)
         {            
             EncloseTransactionAndHandleException(() =>
             {
                 //var andata = (from t in _unitOfWork.Repository<BerthMaintenance>().Query().Select()                               
                 //              where t.BerthMaintenanceID == Convert.ToInt32(BerthMaintenanceID)
                 //              select t).FirstOrDefault<BerthMaintenance>();
                 var andata = _berthMaintenanceRepository.GetBerthMaintenanceApproveId(berthmaintenanceid);

                 BerthMaintenanceApprovalWorkFlow berthMaintenanceApprovalWorkFlow = new BerthMaintenanceApprovalWorkFlow(_unitOfWork, andata, remarks);
                 WorkFlowEngine<BerthMaintenanceApprovalWorkFlow> wf = new WorkFlowEngine<BerthMaintenanceApprovalWorkFlow>(_unitOfWork, _PortCode, _UserId);
                 wf.Process(berthMaintenanceApprovalWorkFlow, taskcode);

             });
         }


        /// <summary>
         /// To Reject Berth Maintenance Request
        /// </summary>
        /// <param name="BerthMaintenanceID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
         public void RejectBerthMaintenance(string berthmaintenanceid, string remarks, string taskcode)
         { 
             EncloseTransactionAndHandleException(() =>
             {
                 //var andata = (from t in _unitOfWork.Repository<BerthMaintenance>().Query().Select()
                 //              where t.BerthMaintenanceID == Convert.ToInt32(BerthMaintenanceID)
                 //              select t).FirstOrDefault<BerthMaintenance>();
                 var andata = _berthMaintenanceRepository.GetBerthMaintenanceApproveId(berthmaintenanceid);

                 BerthMaintenanceApprovalWorkFlow berthMaintenanceApprovalWorkFlow = new BerthMaintenanceApprovalWorkFlow(_unitOfWork, andata, remarks);
                 WorkFlowEngine<BerthMaintenanceApprovalWorkFlow> wf = new WorkFlowEngine<BerthMaintenanceApprovalWorkFlow>(_unitOfWork, _PortCode, _UserId);
                 wf.Process(berthMaintenanceApprovalWorkFlow, taskcode);

             });
         }

        /// <summary>
         ///  To get Berth Maintenance Details based on berthmaintenanceid
        /// </summary>
        /// <param name="berthMaintenanceId"></param>
        /// <returns></returns>
         public List<BerthMaintenanceVO> GetBerthMaintenance(int berthMaintenanceId)
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 return _berthMaintenanceRepository.GetBerthMaintenance(berthMaintenanceId);           
             
             });
         }


         /// <summary>
         ///  To get Workflow Remarks
         /// </summary>
         /// <returns></returns>
         public string GetWorkFlowRemarks(int workFlowInstanceId)
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 return _berthMaintenanceRepository.GetWorkFlowRemarks(workFlowInstanceId);
             });
         }
    }
}



