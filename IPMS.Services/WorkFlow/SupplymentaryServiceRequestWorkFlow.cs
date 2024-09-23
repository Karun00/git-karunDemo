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
    public class SupplymentaryServiceRequestWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private SuppServiceRequest supplymentaryServiceRequestdata;
        //private DivingOccupationApproval _SuppServiceRequestApproval;
        private IAccountRepository _accountRepository;
        // private const string _entityCode = "AGENTREG";
        private const string _entityCode = EntityCodes.SupplymentaryServiceRequest;       //  USERREG
        private const string _entityColdCode = EntityCodes.ColdWorkSupplServiceRequest;
        private const string _entityfloatingCode = EntityCodes.FloatingSuppServiceRequest;
        private const string _entityHotWorkCode = EntityCodes.HotWorkSuppServiceRequest;
        private const string _entityHotColdCode = EntityCodes.HotColdSuppServiceRequest;
        private const string _entityWaterCode = EntityCodes.WaterSuppServiceRequest;
        //  private IWorkFlowEngine<AgentRegistrationWorkFlow> wfEngine;
        private string _remarks;

        public SupplymentaryServiceRequestWorkFlow(IUnitOfWork unitOfWork, SuppServiceRequest SupplymentaryServiceRequest, string remarks)
        {
            _unitOfWork = unitOfWork;
            supplymentaryServiceRequestdata = SupplymentaryServiceRequest;

            // _AgentPorts = AgentPorts;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
        }

        public int SuppServiceRequestid
        {
            get { return supplymentaryServiceRequestdata.CreatedBy; }
        }

        public Entity Entity
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                string entityCode = _entityColdCode;
                if (supplymentaryServiceRequestdata.ServiceType == ServiceTypeCode.FloatingCrane)
                {
                    entityCode = _entityfloatingCode;
                }
                else if (supplymentaryServiceRequestdata.ServiceType == ServiceTypeCode.HotWorkPermitService)
                {
                    entityCode = _entityHotWorkCode;
                }
                else if (supplymentaryServiceRequestdata.ServiceType == ServiceTypeCode.HotandColdWorkPermitService)
                {
                    entityCode = _entityHotColdCode;
                }
                else if (supplymentaryServiceRequestdata.ServiceType == ServiceTypeCode.WaterService)
                {
                    entityCode = _entityWaterCode;
                }
                else if (supplymentaryServiceRequestdata.ServiceType == ServiceTypeCode.ColdWorkPermitService)
                {
                    entityCode = _entityColdCode;
                }


                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()//.Where(e => e.EntityCode.Equals(_entityCode))
                              where e.EntityCode == entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public string ReferenceId
        {
            get { return supplymentaryServiceRequestdata.SuppServiceRequestID.ToString(CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public string ReferenceData
        {
            get
            {
                return Common.GetTokensDictionaryForReferenceData(Entity, supplymentaryServiceRequestdata);
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

        // Ports 
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();

                var suppservicerequest = (from an in _unitOfWork.Repository<SuppServiceRequest>().Query().Select()
                                          where an.SuppServiceRequestID == supplymentaryServiceRequestdata.SuppServiceRequestID
                                          select an).FirstOrDefault<SuppServiceRequest>();
                if (suppservicerequest.PortCode == null)
                {
                    suppservicerequest.PortCode = supplymentaryServiceRequestdata.PortCode;
                }
                else if (string.IsNullOrEmpty(suppservicerequest.PortCode))// if (suppservicerequest.PortCode == "")
                {
                    suppservicerequest.PortCode = supplymentaryServiceRequestdata.PortCode;
                }


                portcodes.Add(suppservicerequest.PortCode);
                return portcodes;
            }
        }

        public SupplymentaryServiceRequestWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            //List<string> portcodes = new List<string>();
            // SuppServiceRequest item = new SuppServiceRequest();
            if (supplymentaryServiceRequestdata.PortCode == portCode)
            {


            }


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
                case "WFCA":
                    Cancel();
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

        public void Cancel()
        {
           // _unitOfWork.ExecuteSqlCommand("Update dbo.VesselCallMovement set FromPositionQuayCode = @p0, FromPositionBerthCode = @p0, FromPositionBollardCode = @p0, ToPositionPortCode = @p0, ToPositionQuayCode = @p0, ToPositionBerthCode = @p0, ToPositionBollardCode = @p0, MovementStatus = @p1, RecordStatus = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where ServiceRequestID = @p5", null, MovementStatus.PENDING, RecordStatus.InActive, servicerequest.ModifiedBy, servicerequest.ModifiedDate, servicerequest.ServiceRequestID);
           _unitOfWork.ExecuteSqlCommand("update ResourceAllocation set ResourceID = @p0, RecordStatus = @p1 where ServiceReferenceID = @p2 and ServiceReferenceType = @p3", null, RecordStatus.InActive, supplymentaryServiceRequestdata.SuppServiceRequestID, ServiceReferenceType.SupplementoryService);
           _unitOfWork.ExecuteSqlCommand("update SuppServiceRequest set RecordStatus = @p0, Remarks = @p1 where SuppServiceRequestID = @p2", RecordStatus.InActive, supplymentaryServiceRequestdata.Remarks, supplymentaryServiceRequestdata.SuppServiceRequestID);
            
        }
        public void Create()
        {




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