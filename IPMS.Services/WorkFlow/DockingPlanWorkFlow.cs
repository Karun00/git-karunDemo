using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IPMS.Services.WorkFlow
{
    public class DockingPlanWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private DockingPlan _dockingPlanService;
        private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.Docking_Plan;
        private string _remarks;

         public DockingPlanWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }
         public DockingPlanWorkFlow(IUnitOfWork unitOfWork, DockingPlan dockingPlanService, string remarks)
        {
            _unitOfWork = unitOfWork;
            _dockingPlanService = dockingPlanService;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
        }
         public int userid
         {
             get { return _dockingPlanService.CreatedBy; }
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
             get { return Convert.ToString(_dockingPlanService.DockingPlanID, CultureInfo.InvariantCulture); }
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
                 return Common.GetTokensDictionaryForReferenceData(entity, _dockingPlanService);

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

                 var fuelrequisition = (from dp in _unitOfWork.Repository<DockingPlan>().Query().Select()
                                        where dp.DockingPlanID == _dockingPlanService.DockingPlanID
                                        select dp).FirstOrDefault<DockingPlan>();


                 portcodes.Add(fuelrequisition.PortCode);

                 return portcodes;
             }
         }



         public void SetWorkFlowId(int workFlowInstanceId, string portCode)
         {
             _dockingPlanService.IsFinal = null;
             _dockingPlanService.WorkflowInstanceID = workFlowInstanceId;
             _unitOfWork.Repository<DockingPlan>().Update(_dockingPlanService);


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
                 case "WFRE":
                     Reject();
                     break;
                 case "WFCA":
                     UpdateRecordStatus();
                     break;

             }
         }

         public void Create()
         {
             if (_dockingPlanService.DockingPlanID != null && _dockingPlanService.DockingPlanID > 0)
             {
                 UpdateStatus();
             }
             else
             {
                 CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                 List<DockingPlanDocument> dockingplanDocumententList = _dockingPlanService.DockingPlanDocuments.ToList();
                 _dockingPlanService.DockingPlanDocuments = null;

                 _unitOfWork.Repository<DockingPlan>().Insert(_dockingPlanService);
                 _dockingPlanService.ObjectState = ObjectState.Added;


                 codeGenerator.UpdateCode("DOCKPL", _dockingPlanService.PortCode);



                 if (dockingplanDocumententList.Count > 0)
                 {
                     foreach (var document in dockingplanDocumententList)
                     {
                         document.DockingPlanID = _dockingPlanService.DockingPlanID;                         
                         document.RecordStatus = "A";
                         _unitOfWork.Repository<DockingPlanDocument>().InsertRange(dockingplanDocumententList);
                     }
                 }


                 _unitOfWork.SaveChanges();
             }

         }

         public void UpdateStatus()
         {
             //Do all db operations
             List<DockingPlanDocument> dockingplanDocumententList = _dockingPlanService.DockingPlanDocuments.ToList();
             _dockingPlanService.DockingPlanDocuments = null;

             _unitOfWork.ExecuteSqlCommand(" delete dbo.DockingPlanDocument where DockingPlanID = @p0", _dockingPlanService.DockingPlanID);

             if (_dockingPlanService.Vessel == null)
                 _dockingPlanService.Vessel = _unitOfWork.Repository<Vessel>().Find(_dockingPlanService.VesselID);

             if (dockingplanDocumententList.Count > 0)
             {
                 foreach (var document in dockingplanDocumententList)
                 {
                     document.DockingPlanID = _dockingPlanService.DockingPlanID;
                     document.RecordStatus = "A";
                 }

                 _unitOfWork.Repository<DockingPlanDocument>().InsertRange(dockingplanDocumententList);
             }

             //if (dockingplanDocumententList.Count > 0)
             //{
             //    foreach (var document in dockingplanDocumententList)
             //    {
             //        document.DockingPlanID = _dockingPlanService.DockingPlanID;                
             //        document.RecordStatus = "A";


             //        if (document.DockingPlanDocumentID > 0)
             //        {
             //            _dockingPlanService.ObjectState = ObjectState.Modified;
             //            _unitOfWork.Repository<DockingPlanDocument>().Update(document);
             //        }
             //        else
             //        {
             //            _dockingPlanService.ObjectState = ObjectState.Added;
             //            _unitOfWork.Repository<DockingPlanDocument>().Insert(document);
             //        }
             //    }
             //    _unitOfWork.SaveChanges();
             //}

             _dockingPlanService.ObjectState = ObjectState.Modified;
             _unitOfWork.Repository<DockingPlan>().Update(_dockingPlanService);
             _unitOfWork.SaveChanges();


         }

         public void Reject()
         {
             //Do all db operations
             List<DockingPlanDocument> dockingplanDocumententList = _dockingPlanService.DockingPlanDocuments.ToList();
             _dockingPlanService.DockingPlanDocuments = null;

            _unitOfWork.ExecuteSqlCommand(" delete dbo.DockingPlanDocument where DockingPlanID = @p0", _dockingPlanService.DockingPlanID);

             if (_dockingPlanService.Vessel == null)
                 _dockingPlanService.Vessel = _unitOfWork.Repository<Vessel>().Find(_dockingPlanService.VesselID);

             if (dockingplanDocumententList.Count > 0)
             {
                 foreach (var document in dockingplanDocumententList)
                 {
                     document.DockingPlanID = _dockingPlanService.DockingPlanID;
                     document.RecordStatus = "I";
                 }

                 _unitOfWork.Repository<DockingPlanDocument>().InsertRange(dockingplanDocumententList);
             }

             _dockingPlanService.ObjectState = ObjectState.Modified;
             _dockingPlanService.RecordStatus = "I";
             _unitOfWork.Repository<DockingPlan>().Update(_dockingPlanService);
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
         public void UpdateRecordStatus()
         {            
             if (_dockingPlanService.Vessel == null)
                 _dockingPlanService.Vessel = _unitOfWork.Repository<Vessel>().Find(_dockingPlanService.VesselID);            

             _dockingPlanService.ObjectState = ObjectState.Modified;
             _dockingPlanService.RecordStatus = "I";
             _unitOfWork.Repository<DockingPlan>().Update(_dockingPlanService);
             _unitOfWork.SaveChanges();


         }
    }
}

  