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
    public class SuppDryDockExtensionWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private SuppDryDockExtension _suppDryDockExtService;
        private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.Supp_DryDockExtension;
        private SuppDryDockExtensionVO _suppDryDockExtVOService;

        private string _remarks;
        public SuppDryDockExtensionWorkFlow() {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }
        public SuppDryDockExtensionWorkFlow(IUnitOfWork unitOfWork, SuppDryDockExtension suppdryDockExtService, string remarks, SuppDryDockExtensionVO suppdryDockExtVOService)
        {
            _unitOfWork = unitOfWork;
            _suppDryDockExtService = suppdryDockExtService;
            _suppDryDockExtVOService = suppdryDockExtVOService;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
           
        }
        public int userid
        {
            get { return _suppDryDockExtService.CreatedBy; }
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
            get { return Convert.ToString(_suppDryDockExtService.SuppDryDockExtensionID, CultureInfo.InvariantCulture); }
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
                return Common.GetTokensDictionaryForReferenceData(entity, _suppDryDockExtVOService);

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

                var suppdrydock = (from sd in _unitOfWork.Repository<SuppDryDock>().Query().Select()
                                   where sd.SuppDryDockID == _suppDryDockExtService.SuppDryDockID
                                   select sd).FirstOrDefault<SuppDryDock>();


                portcodes.Add(suppdrydock.DockPortCode);

                return portcodes;
            }
        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {

            _suppDryDockExtService.WorkflowInstanceID = workFlowInstanceId;
            _unitOfWork.Repository<SuppDryDockExtension>().Update(_suppDryDockExtService);


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
                case "WFSA":
                    NotificationStatus();
                    break;
                case "WFRE":
                    NotificationStatus();
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
            if (_suppDryDockExtService.SuppDryDockExtensionID != null && _suppDryDockExtService.SuppDryDockExtensionID > 0)
            {
                UpdateStatus();
            }
            else
            {
                //List<SuppDryDockDocument> suppDryDockDocumententList = _suppDryDockExtService.SuppDryDockDocuments.ToList();
               // _suppDryDockExtService.SuppDryDockDocuments = null;
                _suppDryDockExtService.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<SuppDryDockExtension>().Insert(_suppDryDockExtService);
               



                //if (suppDryDockDocumententList.Count > 0)
                //{
                //    foreach (var document in suppDryDockDocumententList)
                //    {
                //        document.SuppDryDockID = _suppdryDockService.SuppDryDockID;
                //        document.RecordStatus = "A";
                //        document.CreatedBy = _suppdryDockService.CreatedBy;
                //        document.CreatedDate = DateTime.Now;
                //        document.ModifiedBy = _suppdryDockService.ModifiedBy;
                //        document.ModifiedDate = DateTime.Now;
                //    }
                //    _unitOfWork.Repository<SuppDryDockDocument>().InsertRange(suppDryDockDocumententList);
                //}


                _unitOfWork.SaveChanges();
            }

        }

        public void UpdateStatus()
        {
            //Do all db operations
            //List<SuppDryDockDocument> suppDryDockDocumententList = _suppdryDockService.SuppDryDockDocuments.ToList();
            //_suppdryDockService.SuppDryDockDocuments = null;
            //var brt = _unitOfWork.ExecuteSqlCommand(" delete dbo.SuppDryDockDocument where SuppDryDockID = @p0", _suppdryDockService.SuppDryDockID);

            //if (_suppdryDockService.Vessel == null)
            //    _suppdryDockService.Vessel = _unitOfWork.Repository<Vessel>().Find(_dockingPlanService.VesselID);

            //if (suppDryDockDocumententList.Count > 0)
            //{
            //    foreach (var document in suppDryDockDocumententList)
            //    {
            //        document.SuppDryDockID = _suppdryDockService.SuppDryDockID;
            //        document.RecordStatus = "A";
            //        document.CreatedBy = _suppdryDockService.CreatedBy;
            //        document.CreatedDate = DateTime.Now;
            //        document.ModifiedBy = _suppdryDockService.ModifiedBy;
            //        document.ModifiedDate = DateTime.Now;
            //    }

            //    _unitOfWork.Repository<SuppDryDockDocument>().InsertRange(suppDryDockDocumententList);
            //}


            _suppDryDockExtService.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<SuppDryDockExtension>().Update(_suppDryDockExtService);
            _unitOfWork.SaveChanges();


        }
        public void NotificationStatus()
        {
            _suppDryDockExtService.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<SuppDryDockExtension>().Update(_suppDryDockExtService);
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
