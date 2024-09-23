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
    public class DockingPlanService : ServiceBase, IDockingPlanService
    {
        private IPortConfigurationRepository _portConfigurationRepository;
        //private ISubCategoryRepository _subcategoryRepository;
        private IAccountRepository _accountRepository;

        private IDockingPlanRepository _dockingplanRepository;

        public DockingPlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _dockingplanRepository = new DockingPlanRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
          //  _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public DockingPlanService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _dockingplanRepository = new DockingPlanRepository(_unitOfWork);
            //_subcategoryRepository = new SubCategoryRepository(_unitOfWork);    
            _UserId = GetUserIdByLoginname(_LoginName);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
           // wfEngine = new WorkFlowEngine<FuelRequisitionWorkFlow>();
            // TODO: Complete member initialization
        }

        /// <summary>
        /// To Get Docking Plan Details
        /// </summary>
        /// <returns></returns>
        public List<DockingPlanVO> DockingPlanDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //CompanyVO nextStepCompany = _dockingplanRepository.GetUserDetails(_UserId);
                //var UserTypeID = nextStepCompany.UserTypeId;
                return _dockingplanRepository.GetDockingPlanDetails(_PortCode, _UserId);
            });
        }


        /// <summary>
        /// To Get Vessel Details
        /// </summary>
        /// <returns></returns>
        public List<DockingPlanVO> GetVesselNames(string searchValue, string searchColumn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //CompanyVO nextStepCompany = _dockingplanRepository.GetUserDetails(_UserId);
                //var UserTypeID = nextStepCompany.UserTypeId;
                return _dockingplanRepository.GetVesselNames(searchValue, _PortCode, searchColumn);

            });
        }

        /// <summary>
        /// To Get Vessel Details By VesselID
        /// </summary>
        /// <param name="vesselId"></param>
        /// <returns></returns>
        public DockingPlanVO GetVesselsById(int vesselId)
        {
            return _dockingplanRepository.GetVesselsById(vesselId);
        }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="userid"></param>
         /// <returns></returns>
        public DockingPlanUserVO GetUserName(int userid)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == userid
                         select new DockingPlanUserVO
                         {
                            //serType = u.UserType,
                           //  UserTypeId = u.UserTypeID,
                             AgentName = u.FirstName
                             
                         }).FirstOrDefault();
            return users;
        }

        /// <summary>
        /// To Add Docking Plan Data
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        public DockingPlanVO AddDockingPlan(DockingPlanVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                data.RecordStatus = "A";
                data.CreatedBy = _UserId;
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                data.PortCode = _PortCode;

                DockingPlanUserVO VO = GetUserName(_UserId);
                data.AgentName = VO.AgentName;
              //  dockingplandata.ApplicationDateTime = DateTime.Now;


                CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                data.DockingPlanNo = codeGenerator.GenerateCode("DOCKPL", _PortCode);
                DockingPlan dockingplan = new DockingPlan();
                dockingplan = DockingPlanMapExtension.MapToEntity(data);             

                //dockingplan.ObjectState = ObjectState.Added;
                // _unitOfWork.Repository<DockingPlan>().Insert(dockingplan);  
  
             //   codeGenerator.UpdateCode("DOCKPL", dockingplan.PortCode);

                #region Docking Plan Approval Workflow Integration
                string remarks = "Docking Plan";

                DockingPlanWorkFlow dockingPlanWorkFlow = new DockingPlanWorkFlow(_unitOfWork, dockingplan, remarks);
                WorkFlowEngine<DockingPlanWorkFlow> wf = new WorkFlowEngine<DockingPlanWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dockingPlanWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                #endregion

                //_unitOfWork.SaveChanges();
                return data;
            });
        }

        //Add by Srinivas
        public DockingPlanVO Cancel(DockingPlanVO data)
        {

            return EncloseTransactionAndHandleException(() =>
            {
                data.CreatedBy = _UserId;
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                data.PortCode = _PortCode;
                data.RecordStatus = "I";
                DockingPlan dockingplan = new DockingPlan();
                dockingplan = DockingPlanMapExtension.MapToEntity(data);
              

                #region Docking Plan Approval Workflow Integration
                string remarks = "Cancelled";

                DockingPlanWorkFlow dockingPlanWorkFlow = new DockingPlanWorkFlow(_unitOfWork, dockingplan, remarks);
                WorkFlowEngine<DockingPlanWorkFlow> wf = new WorkFlowEngine<DockingPlanWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dockingPlanWorkFlow, "WFCA");

                #endregion
                return data;
            });
        }
        /// <summary>
        /// To Modify Docking Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DockingPlanVO ModifyDockingPlan(DockingPlanVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                data.CreatedBy = _UserId;
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                data.PortCode = _PortCode;
                DockingPlan dockingplan = new DockingPlan();
                dockingplan = DockingPlanMapExtension.MapToEntity(data);
                //dockingplan.ObjectState = ObjectState.Added;
                //_unitOfWork.Repository<DockingPlan>().Update(dockingplan);

                #region Docking Plan Approval Workflow Integration
                string remarks = "Docking Plan Update";

                DockingPlanWorkFlow dockingPlanWorkFlow = new DockingPlanWorkFlow(_unitOfWork, dockingplan, remarks);
                WorkFlowEngine<DockingPlanWorkFlow> wf = new WorkFlowEngine<DockingPlanWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dockingPlanWorkFlow, "UPDT");

                #endregion

                //_unitOfWork.SaveChanges();
                return data;
            });
        }

        /// <summary>
        ///  To Approve DockingPlan Request
        /// </summary>
        /// <param name="DockingPlanID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveDockingPlan(string dockingplanid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _dockingplanRepository.GetDockingPlanApproveId(dockingplanid);

                DockingPlanWorkFlow dockingPlanWorkFlow = new DockingPlanWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DockingPlanWorkFlow> wf = new WorkFlowEngine<DockingPlanWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dockingPlanWorkFlow, taskcode);

            });
        }


        /// <summary>
        /// To Reject Docking Plan Request
        /// </summary>
        /// <param name="DockingPlanID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void RejectDockingPlan(string dockingplanid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = _dockingplanRepository.GetDockingPlanApproveId(dockingplanid);

                DockingPlanWorkFlow dockingPlanWorkFlow = new DockingPlanWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<DockingPlanWorkFlow> wf = new WorkFlowEngine<DockingPlanWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(dockingPlanWorkFlow, taskcode);

            });
        }


        /// <summary>
        ///  To get Docking Plan Details based on dockingplanid
        /// </summary>
        /// <param name="dockingPlanId"></param>
        /// <returns></returns>
        public List<DockingPlanVO> GetDockingPlan(int dockingPlanId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dockingplanRepository.GetDockingPlan(dockingPlanId);

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
                var docTypes = from s in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable()
                               where s.SupCatCode == "DPDC"
                               select s;
                return docTypes.ToList();
            });
        }
        #endregion



    }
}
