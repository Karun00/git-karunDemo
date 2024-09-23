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
    public  class DivingRequestOccupationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private DivingRequest divingrequestoccupationdata;
   //     private DivingOccupationApproval _DivingOccupationApproval;
        private IAccountRepository _accountRepository;
       // private const string _entityCode = "AGENTREG";
        private const string _entityCode = EntityCodes.DivingRequestOccupation;       //  USERREG
      //  private IWorkFlowEngine<AgentRegistrationWorkFlow> wfEngine;
        private string _remarks;

        public DivingRequestOccupationWorkFlow(IUnitOfWork unitOfWork, DivingRequest DivingRequestOccupation, string remarks)
        {
            _unitOfWork = unitOfWork;
            divingrequestoccupationdata = DivingRequestOccupation;
            
           // _AgentPorts = AgentPorts;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
        }

        public int DivingRequestid
        {
            get { return divingrequestoccupationdata.CreatedBy; }
        }

        public Entity Entity
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public string ReferenceId
        {
            get { return divingrequestoccupationdata.DivingRequestID.ToString(CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public string ReferenceData
        {
            get {
                return Common.GetTokensDictionaryForReferenceData(Entity, divingrequestoccupationdata); 
            }

        }

        public int GetRequestStatus(string pEntityCode, string pReferenceNo)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == pEntityCode
                                 && w.ReferenceID == pReferenceNo

                               select w).Count();

            return _entitycode;
        }       

        // Ports from Diving Request
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();

                var divingrequestoccupation = (from an in _unitOfWork.Repository<DivingRequest>().Query().Select()
                                               where an.DivingRequestID == divingrequestoccupationdata.DivingRequestID
                                               select an).FirstOrDefault<DivingRequest>();
                if (divingrequestoccupation.FromPortCode == null)
                {
                    divingrequestoccupation.FromPortCode = divingrequestoccupationdata.FromPortCode;
                }
                else if (string.IsNullOrEmpty(divingrequestoccupation.FromPortCode))// if (divingrequestoccupation.FromPortCode == "")
                {
                    divingrequestoccupation.FromPortCode = divingrequestoccupationdata.FromPortCode;
                }
              

                portcodes.Add(divingrequestoccupation.FromPortCode);
                return portcodes;
            }
        }

        public DivingRequestOccupationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            List<string> portcodes = new List<string>();
            DivingOccupationApproval item = new DivingOccupationApproval();
               if (divingrequestoccupationdata.FromPortCode == portCode)
               {
                  
                   item.WFStatus = "WFSN";

                   item.DivingRequestID = divingrequestoccupationdata.DivingRequestID;
                   item.CreatedBy = divingrequestoccupationdata.CreatedBy;
                   item.CreatedDate = DateTime.Now;
                   item.ModifiedBy = divingrequestoccupationdata.CreatedBy;
                   item.ModifiedDate = DateTime.Now;
                   item.ApprovedDate = DateTime.Now;
                   item.ApprovedBy = divingrequestoccupationdata.CreatedBy;
                   item.WorkflowInstanceID = 10;
                  

                   item.RecordStatus = "A";
                   item.WorkflowInstanceID = workFlowInstanceId;
                  // item.ObjectState = ObjectState.Modified;
                // _unitOfWork.Repository<DivingOccupationApproval>().Update(item);
                   item.ObjectState = ObjectState.Added;
                   _unitOfWork.Repository<DivingOccupationApproval>().Insert(item);
                   //  _unitOfWork.Repository<DivingRequest>().Update(divingrequestoccupationdata);
               }
           
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

            //divingrequestoccupationdata.ObjectState = ObjectState.Modified;

            //_unitOfWork.Repository<DivingRequest>().Update(divingrequestoccupationdata);

            DivingOccupationApproval item = new DivingOccupationApproval();
                item.WFStatus = "WFSN";
               
                item.DivingRequestID = divingrequestoccupationdata.DivingRequestID;
                item.CreatedBy = divingrequestoccupationdata.CreatedBy;
                item.CreatedDate = divingrequestoccupationdata.CreatedDate;
                item.ModifiedBy = divingrequestoccupationdata.CreatedBy;
                item.ModifiedDate = divingrequestoccupationdata.CreatedDate;
                item.ApprovedDate = divingrequestoccupationdata.CreatedDate;
            item.ApprovedBy = divingrequestoccupationdata.CreatedBy;
            item.WorkflowInstanceID = 10;

                item.RecordStatus = "A";
             //   item.ObjectState = ObjectState.Added;
             //   _unitOfWork.Repository<DivingOccupationApproval>().Insert(item);
             //_unitOfWork.SaveChanges();


            //List<DivingRequestPort> DivingRequestPorts = divingrequestoccupationdata.DivingRequestPorts.ToList();

            //foreach (var item in DivingRequestPorts)
            //{
            //    item.WFStatus = "WFSN";
            //    //item.VerifiedBy = divingrequestoccupationdata.CreatedBy;
            //    //item.VerifiedDate = Convert.ToDateTime(divingrequestoccupationdata.CreatedDate);
            //    //item.ApprovedBy = divingrequestoccupationdata.CreatedBy;
            //    //item.ApprovedDate = Convert.ToDateTime(divingrequestoccupationdata.CreatedDate);

            //    item.DivingRequestID = divingrequestoccupationdata.DivingRequestID;
            //    item.CreatedBy = divingrequestoccupationdata.CreatedBy;
            //    item.CreatedDate = divingrequestoccupationdata.CreatedDate;
            //    item.ModifiedBy = divingrequestoccupationdata.CreatedBy;
            //    item.ModifiedDate = divingrequestoccupationdata.CreatedDate;
            //    item.RecordStatus = "A";
            //    item.ObjectState = ObjectState.Added;
            //    _unitOfWork.Repository<DivingRequestPort>().Insert(item);

            //    // _unitOfWork.SaveChanges();
            //}

            //UserRole userRole = new UserRole();
            //if (divingrequestoccupationdata.UserType == "EMP")
            //{
            //    userRole.RoleID = 1;
            //}
            //else if (divingrequestoccupationdata.UserType == "AGNT")
            //{
            //    userRole.RoleID = 1;
            //}
            //else if (divingrequestoccupationdata.UserType == "TO")
            //{
            //    userRole.RoleID = 1;
            //}
            //userRole.UserID = divingrequestoccupationdata.UserID;
            //userRole.CreatedBy = divingrequestoccupationdata.CreatedBy;
            //userRole.CreatedDate = divingrequestoccupationdata.CreatedDate;
            //userRole.ModifiedBy = divingrequestoccupationdata.CreatedBy;
            //userRole.ModifiedDate = divingrequestoccupationdata.CreatedDate;
            //userRole.RecordStatus = "A";
            //userRole.ObjectState = ObjectState.Added;
            //_unitOfWork.Repository<UserRole>().Insert(userRole);
            //_unitOfWork.SaveChanges();
         
              
        }

       

        public void UpdateStatus()
        {
            //Do all db operations 
            //ArrivalNotificationWorkFlow returnObject = new ArrivalNotificationWorkFlow();
            //returnObject._notification = arrivalNotificationObject
        }




        /*public void SetWorkFlowData(ref WorkflowInstance instance)
        {
            instance.ReferenceID = _notification.VCN;
            instance.WorkflowCode = _workFlowCode;
        }*/


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

        //public int EntityID
        //{
        //    get { throw new NotImplementedException(); }
        //}
    }


}