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
    public class FuelRequisitionService : ServiceBase, IFuelRequisitionService
    {
        //private IWorkFlowEngine<FuelRequisitionWorkFlow> wfEngine;    
        private IPortConfigurationRepository _portConfigurationRepository;
        private ISubCategoryRepository _subcategoryRepository;     
        private IFuelRequisitionRepository _fuelRequisitionRepository;

        public FuelRequisitionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _fuelRequisitionRepository = new FuelRequisitionRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

         public FuelRequisitionService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _fuelRequisitionRepository = new FuelRequisitionRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);    
            _UserId = GetUserIdByLoginname(_LoginName);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
         //  wfEngine = new WorkFlowEngine<FuelRequisitionWorkFlow>();
            // TODO: Complete member initialization
        }


         /// <summary>
         ///  To Get Fuel Requisition Reference data While initialization
         /// </summary>
         /// <returns></returns>
         public FuelRequisitionVO GetFuelRequisitionReferenceVO()
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 FuelRequisitionVO VO = new FuelRequisitionVO();               
                 VO.OilTypes = _subcategoryRepository.GetOilTypes().MapToDto();
                 VO.GradeTypes = _subcategoryRepository.GetGradeTypes().MapToDto();
                 VO.UOMTypes = _subcategoryRepository.GetFuelUOMTypes().MapToDto();
                 VO.OwnersName = _fuelRequisitionRepository.GetUserNameByUserId(_UserId);

                 return VO;
             });
         }

         /// <summary>
         /// To Get Fuel Requisition Details
         /// </summary>
         /// <returns></returns>
         public List<FuelRequisitionVO> FuelRequisitionDetails()
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 return _fuelRequisitionRepository.GetFuelRequisitionDetails(_PortCode);
             });
         }

         /// <summary>
         /// To Get Craft Details
         /// </summary>
         /// <returns></returns>
         public List<FuelRequisitionVO> GetCraftNames()
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 return _fuelRequisitionRepository.GetCraftNames(_PortCode);

             });
         }

        /// <summary>
        /// To Get Craft Details By CraftID
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
         public FuelRequisitionVO GetCraftsByID(int CraftID)
         {
             return _fuelRequisitionRepository.GetCraftsByID(CraftID);
         }

         /// <summary>
         /// To Add Fuel Requisition Data
         /// </summary>
         /// <param name="supcatdata"></param>
         /// <returns></returns>
         public FuelRequisitionVO AddFuelRequisition(FuelRequisitionVO data)
         {
             return EncloseTransactionAndHandleException(() =>
             {
                 data.CreatedBy = _UserId;
                 data.CreatedDate = DateTime.Now;
                 data.ModifiedBy = _UserId;
                 data.ModifiedDate = DateTime.Now;
                 data.PortCode = _PortCode;
                 CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                 data.FuelRequistionNo = codeGenerator.GenerateCode("FUEL",_PortCode);

                 FuelRequisition fuelrequisition = new FuelRequisition();
                 fuelrequisition = FuelRequisitionMapExtension.MapToEntity(data);
                 fuelrequisition.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<FuelRequisition>().Insert(fuelrequisition);

                 codeGenerator.UpdateCode("FUEL", fuelrequisition.PortCode);

                 #region Fuel Requisition Approval Workflow Integration
                 string remarks = "New Fuel Requisition";

                 FuelRequisitionWorkFlow fuelRequisitionWorkFlow = new FuelRequisitionWorkFlow(_unitOfWork, fuelrequisition, remarks);
                 WorkFlowEngine<FuelRequisitionWorkFlow> wf = new WorkFlowEngine<FuelRequisitionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                 wf.Process(fuelRequisitionWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                 #endregion

                 //_unitOfWork.SaveChanges();
                 return data;
             });
         }

         /// <summary>
         /// To Modify Fuel Requisition Data
         /// </summary>
         /// <param name="supcatdata"></param>
         /// <returns></returns>
         public FuelRequisitionVO ModifyFuelRequisition(FuelRequisitionVO data)
         {
             return EncloseTransactionAndHandleException(() =>
             {
                 data.CreatedBy = _UserId;
                 data.CreatedDate = DateTime.Now;
                 data.ModifiedBy = _UserId;
                 data.ModifiedDate = DateTime.Now;
                 data.PortCode = _PortCode;
                 FuelRequisition fuelrequisition = new FuelRequisition();
                 fuelrequisition = FuelRequisitionMapExtension.MapToEntity(data);
                 fuelrequisition.ObjectState = ObjectState.Added;                 
                 _unitOfWork.Repository<FuelRequisition>().Update(fuelrequisition);

                 #region Fuel Requisition Approval Workflow Integration
                 string remarks = "Fuel Requisition Updated";

                 FuelRequisitionWorkFlow fuelRequisitionWorkFlow = new FuelRequisitionWorkFlow(_unitOfWork, fuelrequisition, remarks);
                 WorkFlowEngine<FuelRequisitionWorkFlow> wf = new WorkFlowEngine<FuelRequisitionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                 wf.Process(fuelRequisitionWorkFlow, "UPDT");

                 #endregion

                 //_unitOfWork.SaveChanges();
                 return data;
             });
         }



         /// <summary>
         ///  To Approve Fuel Requisition Request
         /// </summary>
         /// <param name="FuelRequisitionID"></param>
         /// <param name="comments"></param>
         /// <param name="taskcode"></param>
         public void ApproveFuelRequisition(string fuelrequisitionid, string remarks, string taskcode)
         {
             EncloseTransactionAndHandleException(() =>
             {

                 var andata = _fuelRequisitionRepository.GetFuelRequisitionApproveid(fuelrequisitionid);

                 FuelRequisitionWorkFlow fuelRequisitionWorkFlow = new FuelRequisitionWorkFlow(_unitOfWork, andata, remarks);
                 WorkFlowEngine<FuelRequisitionWorkFlow> wf = new WorkFlowEngine<FuelRequisitionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                 wf.Process(fuelRequisitionWorkFlow, taskcode);

             });
         }


         /// <summary>
         /// To Reject Fuel Requisitione Request
         /// </summary>
         /// <param name="FuelRequisitionID"></param>
         /// <param name="comments"></param>
         /// <param name="taskcode"></param>
         public void RejectFuelRequisition(string fuelrequisitionid, string remarks, string taskcode)
         {
             EncloseTransactionAndHandleException(() =>
             {
                 var andata = _fuelRequisitionRepository.GetFuelRequisitionApproveid(fuelrequisitionid);

                 FuelRequisitionWorkFlow fuelRequisitionWorkFlow = new FuelRequisitionWorkFlow(_unitOfWork, andata, remarks);
                 WorkFlowEngine<FuelRequisitionWorkFlow> wf = new WorkFlowEngine<FuelRequisitionWorkFlow>(_unitOfWork, _PortCode, _UserId);
                 wf.Process(fuelRequisitionWorkFlow, taskcode);

             });
         }

         /// <summary>
         ///  To get Fuel Requisitione Details based on fuelrequisitionid
         /// </summary>
         /// <param name="berthmaintenanceid"></param>
         /// <returns></returns>
         public List<FuelRequisitionVO> GetFuelRequisition(int fuelrequisitionid)
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 return _fuelRequisitionRepository.GetFuelRequisition(fuelrequisitionid);

             });
         }


    }
}
