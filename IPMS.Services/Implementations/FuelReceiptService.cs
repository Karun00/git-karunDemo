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
    public class FuelReceiptService : ServiceBase, IFuelReceiptService
    {
      //  private IWorkFlowEngine<FuelReceiptWorkFlow> wfEngine;    
        private IPortConfigurationRepository _portConfigurationRepository;
        private ISubCategoryRepository _subcategoryRepository;     
        private IFuelReceiptRepository _fuelReceiptRepository;
        private IBerthRepository _berthRepository;

        public FuelReceiptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _fuelReceiptRepository = new FuelReceiptRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);   
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public FuelReceiptService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _fuelReceiptRepository = new FuelReceiptRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
           // wfEngine = new WorkFlowEngine<FuelReceiptWorkFlow>();
            // TODO: Complete member initialization
        }

        /// <summary>
        /// To Get Fuel Receipt Details
        /// </summary>
        /// <returns></returns>
        public List<FuelRequisitionVO> FuelReceiptDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _fuelReceiptRepository.GetFuelReceiptDetails(_PortCode);
            });
        }

        /// <summary>
        ///  To Get Fuel Receipt Reference data While initialization
        /// </summary>
        /// <returns></returns>
        public FuelReceiptVO GetFuelReceiptReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                FuelReceiptVO VO = new FuelReceiptVO();
                VO.SupplyingModes = _subcategoryRepository.GetSupplyingModes().MapToDto();
                VO.GradeTypes = _subcategoryRepository.GetGradeTypes().MapToDto();                
                VO.Berths = _berthRepository.GetBerths(_PortCode); 

                return VO;
            });
        }



        /// <summary>
        /// To Add Fuel Receipt Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public FuelReceiptVO AddFuelReceipt(FuelReceiptVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                data.CreatedBy = _UserId;
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                data.PortCode = _PortCode;
                var fuelreceiptid = data.FuelReceiptID;
              
                if (fuelreceiptid == 0)
                {
                    CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                    data.FuelReceiptNo = codeGenerator.GenerateCode("RECEPT", _PortCode);
                    FuelReceipt fuelreceipt = new FuelReceipt();
                    fuelreceipt = FuelReceiptMapExtension.MapToEntity(data);
                    fuelreceipt.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<FuelReceipt>().Insert(fuelreceipt);

                    codeGenerator.UpdateCode("RECEPT", fuelreceipt.PortCode);

                    #region Fuel Receipt Workflow Integration
                    string remarks = "New Fuel Receipt";

                    FuelReceiptWorkFlow fuelReceiptWorkFlow = new FuelReceiptWorkFlow(_unitOfWork, fuelreceipt, remarks);
                    WorkFlowEngine<FuelReceiptWorkFlow> wf = new WorkFlowEngine<FuelReceiptWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(fuelReceiptWorkFlow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);

                    #endregion

                   // _unitOfWork.SaveChanges();
                }
                else
                {
                    FuelReceipt fuelreceipt = new FuelReceipt();
                    fuelreceipt = FuelReceiptMapExtension.MapToEntity(data);
                  //  fuelreceipt.FuelReceiptID = fuelreceiptid.FuelReceiptID;
                    fuelreceipt.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<FuelReceipt>().Update(fuelreceipt);
                    //_unitOfWork.SaveChanges();

                    #region Fuel Receipt Workflow Integration
                    string remarks = "Fuel Receipt Updated";

                    FuelReceiptWorkFlow fuelReceiptWorkFlow = new FuelReceiptWorkFlow(_unitOfWork, fuelreceipt, remarks);
                    WorkFlowEngine<FuelReceiptWorkFlow> wf = new WorkFlowEngine<FuelReceiptWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(fuelReceiptWorkFlow, "UPDT");

                    #endregion
                }           

               

                return data;
            });
        }


        /// <summary>
        ///  To Approve Fuel Receipt Request
        /// </summary>
        /// <param name="FuelReceiptID"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApproveFuelReceipt(string fuelreceiptid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {

                var andata = _fuelReceiptRepository.GetFuelReceiptApproveId(fuelreceiptid);

                FuelReceiptWorkFlow fuelReceiptWorkFlow = new FuelReceiptWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<FuelReceiptWorkFlow> wf = new WorkFlowEngine<FuelReceiptWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(fuelReceiptWorkFlow, taskcode);

            });
        }


        /// <summary>
        ///  To get Fuel Receipt Details based on fuelrequestionid
        /// </summary>
        /// <param name="fuelRequestionId"></param>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetFuelReceipt(int fuelRequestionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var fuelreceipt = _fuelReceiptRepository.GetFuelReceipt(fuelRequestionId);

                return fuelreceipt;

            });
        }

        /// <summary>
        ///  To get Fuel Receipt Details based on fuelreceiptid
        /// </summary>
        /// <param name="fuelReceiptId"></param>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetFuelReceiptFuelId(int fuelReceiptId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var fuelreceipt = _fuelReceiptRepository.GetFuelReceiptFuelId(fuelReceiptId);

                return fuelreceipt;

            });
        }
    }
}
