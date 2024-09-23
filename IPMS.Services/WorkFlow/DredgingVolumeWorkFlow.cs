using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace IPMS.Services.WorkFlow
{
    public class DredgingVolumeWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private DredgingOperation _dredgingOperationService;
        private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.Dredging_Volume;
        private string _remarks;

        public DredgingVolumeWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }
        public DredgingVolumeWorkFlow(IUnitOfWork unitOfWork, DredgingOperation dredgingOperationService, string remarks)
        {
            _unitOfWork = unitOfWork;
            _dredgingOperationService = dredgingOperationService;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
        }
        public int Userid
        {
            get { return _dredgingOperationService.CreatedBy; }
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
            get { return Convert.ToString(_dredgingOperationService.DredgingOperationID, CultureInfo.InvariantCulture); }
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
                return Common.GetTokensDictionaryForReferenceData(entity, _dredgingOperationService);

            }

        }
        public int GetRequestStatus(string EntityCode, string Referenceno)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == EntityCode
                                 && w.ReferenceID == Referenceno

                               select w).Count();

            return _entitycode;
        }

        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();

                var dredgingvolume = (from bm in _unitOfWork.Repository<DredgingOperation>().Query().Select()
                                      where bm.DredgingOperationID == _dredgingOperationService.DredgingOperationID
                                      select bm).FirstOrDefault();


                portcodes.Add(dredgingvolume.PortCode);

                return portcodes;
            }
        }
        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {

            _dredgingOperationService.DVWorkflowInstanceID = workFlowInstanceId;
            _dredgingOperationService.IsDVFinal = "N";
            _unitOfWork.Repository<DredgingOperation>().Update(_dredgingOperationService);


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
        }
        public int EntityId
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
