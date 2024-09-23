using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IPMS.Services.WorkFlow
{
    public class FuelRequisitionWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private FuelRequisition _fuelRequisitionService;
        private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.Fuel_Requisition;
        private string _remarks;

        public FuelRequisitionWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }
        public FuelRequisitionWorkFlow(IUnitOfWork unitOfWork, FuelRequisition fuelRequisitionService, string remarks)
        {
            _unitOfWork = unitOfWork;
            _fuelRequisitionService = fuelRequisitionService;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
        }

        public int userid
        {
            get { return _fuelRequisitionService.CreatedBy; }
        }

        public Entity Entity
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()//.Where(e => e.EntityCode.Equals(_entityCode))
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public string ReferenceId
        {
            get { return Convert.ToString(_fuelRequisitionService.FuelRequisitionID, CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public string ReferenceData
        {

            get
            {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return Common.GetTokensDictionaryForReferenceData(entity, _fuelRequisitionService);

            }

        }

        public int GetRequestStatus(string entitycode, string referenceno)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == entitycode
                                 && w.ReferenceID == referenceno

                               select w).Count();

            return _entitycode;
        }


        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();

                var fuelrequisition = (from bm in _unitOfWork.Repository<FuelRequisition>().Query().Select()
                                        where bm.FuelRequisitionID == _fuelRequisitionService.FuelRequisitionID
                                        select bm).FirstOrDefault<FuelRequisition>();


                portcodes.Add(fuelrequisition.PortCode);              

                return portcodes;
            }
        }



        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {

            _fuelRequisitionService.WorkflowInstanceId = workFlowInstanceId;
            _unitOfWork.Repository<FuelRequisition>().Update(_fuelRequisitionService);


            _unitOfWork.SaveChanges();
        }

        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    Create();
                    break;
                case "UPDT":
                    UpdateStatus();
                    break;
                case "VAP":
                    break;
                case "VRES":
                    break;
                case "VUPD":
                    break;
                case "EXPI":
                    break;
                case "CLOS":
                    break;
            }
        }

        public void Create()
        {

            _unitOfWork.SaveChanges();

        }

        public void UpdateStatus()
        {
            //Do all db operations 
            _unitOfWork.SaveChanges();
        }

     
        public int EntityID
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return entity.EntityID;
            }
        }

        public CompanyVO GetCompanyDetails(int step)
        {
            CompanyVO vo = new CompanyVO();
            vo.UserType = "EMP";
            vo.UserTypeId = 1;
            return vo;
        }      
    }
}