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
    public class SuppDryDockWorkFlow : IWorkFlowEntity     
    {
        private readonly IUnitOfWork _unitOfWork;
        private SuppDryDock _suppdryDockService;
        private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.Supp_DryDock;
        private string _remarks;
        private CompanyVO vo = null;

         public SuppDryDockWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }
         public SuppDryDockWorkFlow(IUnitOfWork unitOfWork, SuppDryDock suppdryDockService, string remarks)
        {
            _unitOfWork = unitOfWork;
            _suppdryDockService = suppdryDockService;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
            vo = new CompanyVO();
        }
         public int userid
         {
             get { return _suppdryDockService.CreatedBy; }
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
             get { return Convert.ToString(_suppdryDockService.SuppDryDockID, CultureInfo.InvariantCulture); }
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
                 return Common.GetTokensDictionaryForReferenceData(entity, _suppdryDockService);

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
                                        where sd.SuppDryDockID == _suppdryDockService.SuppDryDockID
                                        select sd).FirstOrDefault<SuppDryDock>();


                 portcodes.Add(suppdrydock.DockPortCode);

                 return portcodes;
             }
         }



         public void SetWorkFlowId(int workFlowInstanceId, string portCode)
         {

             _suppdryDockService.WorkflowInstanceID = workFlowInstanceId;
             _unitOfWork.Repository<SuppDryDock>().Update(_suppdryDockService);


             _unitOfWork.SaveChanges();
         }

         public void ExecuteTask(string workflowTaskCode)
         {
             switch (workflowTaskCode)
             {
                 case "NEW":
                     Create();
                     vo.UserType = UserType.Employee;
                     vo.UserTypeId = 0;
                     break;
                 case "UPDT":
                     UpdateStatus();
                     vo.UserType = UserType.Employee;
                     vo.UserTypeId = 0;
                     break;
                 case "WFSA":
                     UpdateAgentVO();
                     break;
                 case "WFCO":
                     //vo.UserType = UserType.Employee;
                     //vo.UserTypeId = 0;
                     UpdateAgentVO();
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
                 case "WFCA":
                     UpdateRecordStatus();
                     break;
                 case "WFCC":
                     vo.UserType = UserType.Employee;
                     vo.UserTypeId = 0;
                     break;
                 case "WSSA":
                     UpdateRecordStatus();
                     break;
                 case "WSRE":
                     UpdateAgentVO();
                     
                     break;
             }
         }

         public void Create()
         {
             if (_suppdryDockService.SuppDryDockID != null && _suppdryDockService.SuppDryDockID > 0)
             {
                 UpdateStatus();
             }
             else
             {
                 List<SuppDryDockDocument> suppDryDockDocumententList = _suppdryDockService.SuppDryDockDocuments.ToList();
                 _suppdryDockService.SuppDryDockDocuments = null;

                 _unitOfWork.Repository<SuppDryDock>().Insert(_suppdryDockService);
                 _suppdryDockService.ObjectState = ObjectState.Added;



                 if (suppDryDockDocumententList.Count > 0)
                 {
                     foreach (var document in suppDryDockDocumententList)
                     {
                         document.SuppDryDockID = _suppdryDockService.SuppDryDockID;                         
                         document.RecordStatus = "A";
                         document.CreatedBy = _suppdryDockService.CreatedBy;
                         document.CreatedDate = DateTime.Now;
                         document.ModifiedBy = _suppdryDockService.ModifiedBy;
                         document.ModifiedDate = DateTime.Now;                         
                     }
                     _unitOfWork.Repository<SuppDryDockDocument>().InsertRange(suppDryDockDocumententList);
                 }


                 _unitOfWork.SaveChanges();
             }

         }

         public void UpdateStatus()
         {
             //Do all db operations
             List<SuppDryDockDocument> suppDryDockDocumententList = _suppdryDockService.SuppDryDockDocuments.ToList();
             _suppdryDockService.SuppDryDockDocuments = null;

             var brt = _unitOfWork.ExecuteSqlCommand(" delete dbo.SuppDryDockDocument where SuppDryDockID = @p0", _suppdryDockService.SuppDryDockID);

             //if (_suppdryDockService.Vessel == null)
             //    _suppdryDockService.Vessel = _unitOfWork.Repository<Vessel>().Find(_dockingPlanService.VesselID);

             if (suppDryDockDocumententList.Count > 0)
             {
                 foreach (var document in suppDryDockDocumententList)
                 {
                     document.SuppDryDockID = _suppdryDockService.SuppDryDockID;
                     document.RecordStatus = "A";
                     document.CreatedBy = _suppdryDockService.CreatedBy;
                     document.CreatedDate = DateTime.Now;
                     document.ModifiedBy = _suppdryDockService.ModifiedBy;
                     document.ModifiedDate = DateTime.Now;
                 }

                 _unitOfWork.Repository<SuppDryDockDocument>().InsertRange(suppDryDockDocumententList);
             }


             _suppdryDockService.ObjectState = ObjectState.Modified;
             _unitOfWork.Repository<SuppDryDock>().Update(_suppdryDockService);
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
             return vo;
         }

         public void UpdateAgentVO()
         {
             var usertype = _unitOfWork.Repository<User>().Find(_suppdryDockService.CreatedBy);
             if (usertype.UserType == UserType.Agent)
             {
                 vo.UserType = UserType.Agent;
                 vo.UserTypeId = _suppdryDockService.UserTypeId;
             }             
             else
             {
                 vo.UserType = UserType.Employee;
                 vo.UserTypeId = _suppdryDockService.CreatedBy;
             }
         }

         public void UpdateRecordStatus()
         {
             //if (_suppdryDockService.Vessel == null)
             //  _suppdryDockService.Vessel = _unitOfWork.Repository<Vessel>().Find(_suppdryDockService.);

             _suppdryDockService.ObjectState = ObjectState.Modified;
             _suppdryDockService.RecordStatus = "I";
             _unitOfWork.Repository<SuppDryDock>().Update(_suppdryDockService);
             _unitOfWork.SaveChanges();


         }
    }
}
