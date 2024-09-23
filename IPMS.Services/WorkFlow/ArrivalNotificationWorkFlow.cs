using System;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using System.Collections.Generic;
using System.Linq;


namespace IPMS.Services.WorkFlow
{
    public class ArrivalNotificationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ArrivalNotification _arrivalnotification;
        private const string _entityCode = EntityCodes.Arrival_Notification;
        private IEntityRepository _entityRepository;
        private IWorkFlowTaskRepository _workFlowTaskRepository;
        private CompanyVO vo;

        private string _remarks;


        public ArrivalNotificationWorkFlow(IUnitOfWork unitOfWork, ArrivalNotification arrivalnotification, string remarks)
        {
            _unitOfWork = unitOfWork;
            _arrivalnotification = arrivalnotification;
            _remarks = remarks;
            _entityRepository = new EntityRepository(_unitOfWork);
            _workFlowTaskRepository = new WorkFlowTaskRepository(_unitOfWork);
            vo = new CompanyVO();
        }

        /// <summary>
        /// To get entity Details
        /// </summary>
        public Entity Entity
        {
            get
            {
                var entity = _entityRepository.GetEntityByCode(_entityCode);
                return entity;
            }
        }

        /// <summary>
        /// To get Request reference id
        /// </summary>
        public string ReferenceId
        {
            get { return _arrivalnotification.VCN; }
        }

        /// <summary>
        ///  to get Remarks
        /// </summary>
        public string Remarks
        {
            get { return _remarks; }
        }

        /// <summary>
        /// to get ReferenceData
        /// </summary>
        public string ReferenceData
        {
            get { return Common.GetTokensDictionaryForReferenceData(Entity, _arrivalnotification); }

        }

        /// <summary>
        /// to get GetRequestStatus
        /// </summary>
        /// <param name="entitycode"></param>
        /// <param name="referenceno"></param>
        /// <returns></returns>
        public int GetRequestStatus(string entitycode, string referenceno)
        {
            var _entitycode = _workFlowTaskRepository.GetRequestStatus(entitycode, referenceno);

            return _entitycode;
        }

        /// <summary>
        /// To get Port code List
        /// </summary>
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(_arrivalnotification.PortCode);
                return portcodes;
            }
        }

        /// <summary>
        /// ArrivalNotificationWorkFlow
        /// </summary>
        public ArrivalNotificationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

        /// <summary>
        /// SetWorkFlow for each Port
        /// </summary>
        /// <param name="workFlowInstanceId"></param>
        /// <param name="portCode"></param>
        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {


            _arrivalnotification.WorkflowInstanceId = workFlowInstanceId;
            _unitOfWork.Repository<ArrivalNotification>().Update(_arrivalnotification);



            _unitOfWork.SaveChanges();
        }

        public void InsertPreberthSchedule()
        {
            #region Adding Record in VesselCall, VesselCallMovement table, if the request is Approved
            VesselCall _objVesselCall = new VesselCall();
            _objVesselCall.VCN = _arrivalnotification.VCN;
            _objVesselCall.RecentAgentID = _arrivalnotification.AgentID;
            _objVesselCall.ETA = _arrivalnotification.ETA;
            _objVesselCall.ETD = _arrivalnotification.ETD;
            _objVesselCall.ETB = _arrivalnotification.ETA;
            _objVesselCall.ETUB = _arrivalnotification.ETD;
            _objVesselCall.FromPositionPortCode = _arrivalnotification.PortCode;
            _objVesselCall.RecordStatus = _arrivalnotification.RecordStatus;
            _objVesselCall.CreatedBy = _arrivalnotification.CreatedBy;
            _objVesselCall.CreatedDate = _arrivalnotification.CreatedDate;
            _objVesselCall.ModifiedBy = _arrivalnotification.ModifiedBy;
            _objVesselCall.ModifiedDate = _arrivalnotification.ModifiedDate;
            _unitOfWork.Repository<VesselCall>().Insert(_objVesselCall);


            VesselCallMovement vcmovement = new VesselCallMovement();
            vcmovement.VCN = _arrivalnotification.VCN;

            vcmovement.MovementType = "ARMV";
            vcmovement.ETB = _arrivalnotification.ETA;
            vcmovement.ETUB = _arrivalnotification.ETD;
            vcmovement.MovementStatus = "MPEN";
            vcmovement.SlotStatus = "PEND";
            vcmovement.SlotDate = _arrivalnotification.ETA;
            vcmovement.FromPositionPortCode = _arrivalnotification.PortCode;

            vcmovement.RecordStatus = _arrivalnotification.RecordStatus;
            vcmovement.CreatedBy = _arrivalnotification.CreatedBy;
            vcmovement.CreatedDate = _arrivalnotification.CreatedDate;
            vcmovement.ModifiedBy = _arrivalnotification.ModifiedBy;
            vcmovement.ModifiedDate = _arrivalnotification.ModifiedDate;
            _unitOfWork.Repository<VesselCallMovement>().Insert(vcmovement);

            _unitOfWork.SaveChanges();
            #endregion
        }

        /// <summary>
        /// Create for Request
        /// </summary>
        public void Create()
        {
            //if (_arrivalnotification.VCN != null && _arrivalnotification.VCN != string.Empty &&)
                if (!string.IsNullOrEmpty(_arrivalnotification.VCN))
            {
                UpdateStatus();
            }
            else
            {

                CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                _arrivalnotification.VCN = codeGenerator.GenerateVCN(_arrivalnotification.PortCode);

                List<ArrivalCommodity> commodityList = _arrivalnotification.ArrivalCommodities.ToList();
                List<ArrivalIMDGTanker> IMDGTankerList = _arrivalnotification.ArrivalIMDGTankers.ToList();
                List<ArrivalDocument> arrivalDocumentList = _arrivalnotification.ArrivalDocuments.ToList();
                List<IMDGInformation> IMDGInformationList = _arrivalnotification.IMDGInformations.ToList();
                List<ArrivalReason> ArrivalReasonList = _arrivalnotification.ArrivalReasons.ToList();
                List<ArrivalAgent> ArrivalAgentList = _arrivalnotification.ArrivalAgents.ToList();
                List<WasteDeclaration> WasteDeclarationList = _arrivalnotification.WasteDeclarations.ToList();


                if (_arrivalnotification.Vessel == null)
                    _arrivalnotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_arrivalnotification.VesselID);

                if (ArrivalAgentList.Count > 0)
                {
                    _unitOfWork.Repository<ArrivalAgent>().InsertRange(ArrivalAgentList);
                }


                if (ArrivalReasonList.Count > 0)
                {
                    foreach (var reasons in ArrivalReasonList)
                    {
                        reasons.VCN = _arrivalnotification.VCN;
                        reasons.CreatedBy = _arrivalnotification.CreatedBy;
                        reasons.CreatedDate = _arrivalnotification.CreatedDate;
                        reasons.ModifiedBy = _arrivalnotification.ModifiedBy;
                        reasons.ModifiedDate = _arrivalnotification.ModifiedDate;
                        reasons.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

                }


                if (commodityList.Count > 0)
                {
                    foreach (var commodity in commodityList)
                    {
                        commodity.VCN = _arrivalnotification.VCN;
                        commodity.CreatedBy = _arrivalnotification.CreatedBy;
                        commodity.CreatedDate = _arrivalnotification.CreatedDate;
                        commodity.ModifiedBy = _arrivalnotification.ModifiedBy;
                        commodity.ModifiedDate = _arrivalnotification.ModifiedDate;
                        commodity.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<ArrivalCommodity>().InsertRange(commodityList);

                }

                if (IMDGTankerList.Count > 0)
                {
                    foreach (var IMDGTanker in IMDGTankerList)
                    {
                        IMDGTanker.VCN = _arrivalnotification.VCN;
                        IMDGTanker.CreatedBy = _arrivalnotification.CreatedBy;
                        IMDGTanker.CreatedDate = _arrivalnotification.CreatedDate;
                        IMDGTanker.ModifiedBy = _arrivalnotification.ModifiedBy;
                        IMDGTanker.ModifiedDate = _arrivalnotification.ModifiedDate;
                        IMDGTanker.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<ArrivalIMDGTanker>().InsertRange(IMDGTankerList);

                }

                if (arrivalDocumentList.Count > 0)
                {
                    foreach (var document in arrivalDocumentList)
                    {
                        document.VCN = _arrivalnotification.VCN;
                        document.CreatedBy = _arrivalnotification.CreatedBy;
                        document.CreatedDate = _arrivalnotification.CreatedDate;
                        document.ModifiedBy = _arrivalnotification.ModifiedBy;
                        document.ModifiedDate = _arrivalnotification.ModifiedDate;
                        document.RecordStatus = "A";
                        _unitOfWork.Repository<ArrivalDocument>().Insert(document);
                    }


                }
                if (IMDGInformationList.Count > 0)
                {
                    foreach (var IMDGInformation in IMDGInformationList)
                    {
                        IMDGInformation.VCN = _arrivalnotification.VCN;
                        IMDGInformation.CreatedBy = _arrivalnotification.CreatedBy;
                        IMDGInformation.CreatedDate = _arrivalnotification.CreatedDate;
                        IMDGInformation.ModifiedBy = _arrivalnotification.ModifiedBy;
                        IMDGInformation.ModifiedDate = _arrivalnotification.ModifiedDate;
                        IMDGInformation.RecordStatus = "A";
                        _unitOfWork.Repository<IMDGInformation>().Insert(IMDGInformation);
                    }


                }

                if (WasteDeclarationList.Count > 0)
                {
                    foreach (var WasteDeclaration in WasteDeclarationList)
                    {
                        WasteDeclaration.VCN = _arrivalnotification.VCN;
                        WasteDeclaration.CreatedBy = _arrivalnotification.CreatedBy;
                        WasteDeclaration.CreatedDate = _arrivalnotification.CreatedDate;
                        WasteDeclaration.ModifiedBy = _arrivalnotification.ModifiedBy;
                        WasteDeclaration.ModifiedDate = _arrivalnotification.ModifiedDate;
                        WasteDeclaration.RecordStatus = "A";
                        _unitOfWork.Repository<WasteDeclaration>().Insert(WasteDeclaration);
                    }


                }


                _unitOfWork.Repository<ArrivalNotification>().Insert(_arrivalnotification);

                if (!string.IsNullOrEmpty(_arrivalnotification.GeneratedVCN))
                {
                    
                        _unitOfWork.ExecuteSqlCommand(" update dbo.ArrivalNotification SET RecordStatus = 'I', GeneratedVCN = @p0 WHERE VCN = @p1 ", _arrivalnotification.VCN, _arrivalnotification.GeneratedVCN);
                }

                codeGenerator.UpdateCode("VCN", _arrivalnotification.PortCode);

                _unitOfWork.SaveChanges();

            }
        }
        /// <summary>
        /// Execute DML
        /// </summary>
        /// <param name="workflowTaskCode"></param>
        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    Create();
                    break;
                case "UPDT":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    UpdateStatus();
                    break;
                case "VAP":
                    break;
                case "WFSA": //Once Berthplanner Approve the AN Insert record(s) in VesselCall & VesselCallMovement tables
                    InsertPreberthSchedule();
                    break;
                case "VRES":
                    vo.UserType = UserType.Agent;
                    vo.UserTypeId = _arrivalnotification.AgentID;
                    UpdateStatus();
                    break;
                case "VUPD":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    UpdateStatus();
                    break;
                case "EXPI":
                    break;
                case "CLOS":
                    break;
                case "WFCA":
                    Cancel();
                    break;
                case "WFRE":
                    Reject();
                    break;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// 

        public void UpdateStatus()
        {


            List<ArrivalCommodity> commodityList = _arrivalnotification.ArrivalCommodities.ToList();
            List<ArrivalIMDGTanker> IMDGTankerList = _arrivalnotification.ArrivalIMDGTankers.ToList();
            List<ArrivalDocument> arrivalDocumentList = _arrivalnotification.ArrivalDocuments.ToList();
            List<IMDGInformation> IMDGInformationList = _arrivalnotification.IMDGInformations.ToList();
            List<ArrivalReason> ArrivalReasonList = _arrivalnotification.ArrivalReasons.ToList();
            List<ArrivalAgent> ArrivalAgentList = _arrivalnotification.ArrivalAgents.ToList();
            List<WasteDeclaration> WasteDeclarationList = _arrivalnotification.WasteDeclarations.ToList();

            if (ArrivalAgentList.Count > 0)
            {              
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalAgent where VCN = @p0", _arrivalnotification.VCN);
                _unitOfWork.Repository<ArrivalAgent>().InsertRange(ArrivalAgentList);
            }           

            //TODO: Inline statements to be removed here : Bhoji
          
                _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalDocument where VCN = @p0", _arrivalnotification.VCN);
       
                _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalReason where VCN = @p0", _arrivalnotification.VCN);

                _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalCommodity where VCN = @p0", _arrivalnotification.VCN);

                _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalIMDGTanker where VCN = @p0", _arrivalnotification.VCN);

                _unitOfWork.ExecuteSqlCommand("delete dbo.IMDGInformation where VCN = @p0", _arrivalnotification.VCN);

                _unitOfWork.ExecuteSqlCommand("delete dbo.WasteDeclaration where VCN = @p0", _arrivalnotification.VCN);
                

            if (_arrivalnotification.Vessel == null)
                _arrivalnotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_arrivalnotification.VesselID);

            if (ArrivalReasonList.Count > 0)
            {
                foreach (var reasons in ArrivalReasonList)
                {
                    reasons.VCN = _arrivalnotification.VCN;
                    reasons.CreatedBy = _arrivalnotification.CreatedBy;
                    reasons.CreatedDate = _arrivalnotification.CreatedDate;
                    reasons.ModifiedBy = _arrivalnotification.ModifiedBy;
                    reasons.ModifiedDate = _arrivalnotification.ModifiedDate;
                    reasons.RecordStatus = "A";

                }
                _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

            }

            if (arrivalDocumentList.Count > 0)
            {
                foreach (var document in arrivalDocumentList)
                {
                    document.VCN = _arrivalnotification.VCN;
                    document.CreatedBy = _arrivalnotification.CreatedBy;
                    document.CreatedDate = _arrivalnotification.CreatedDate;
                    document.ModifiedBy = _arrivalnotification.ModifiedBy;
                    document.ModifiedDate = _arrivalnotification.ModifiedDate;
                    document.RecordStatus = "A";
                }

                _unitOfWork.Repository<ArrivalDocument>().InsertRange(arrivalDocumentList);
            }

            if (commodityList.Count > 0)
            {
                foreach (var commodity in commodityList)
                {
                    commodity.VCN = _arrivalnotification.VCN;
                    commodity.CreatedBy = _arrivalnotification.CreatedBy;
                    commodity.CreatedDate = _arrivalnotification.CreatedDate;
                    commodity.ModifiedBy = _arrivalnotification.ModifiedBy;
                    commodity.ModifiedDate = _arrivalnotification.ModifiedDate;
                    commodity.RecordStatus = "A";
                    
                }
                _unitOfWork.Repository<ArrivalCommodity>().InsertRange(commodityList);
             
            }

            if (IMDGTankerList.Count > 0)
            {
                foreach (var IMDGTanker in IMDGTankerList)
                {
                    IMDGTanker.VCN = _arrivalnotification.VCN;
                    IMDGTanker.CreatedBy = _arrivalnotification.CreatedBy;
                    IMDGTanker.CreatedDate = _arrivalnotification.CreatedDate;
                    IMDGTanker.ModifiedBy = _arrivalnotification.ModifiedBy;
                    IMDGTanker.ModifiedDate = _arrivalnotification.ModifiedDate;
                    IMDGTanker.RecordStatus = "A";

                   

                }
                _unitOfWork.Repository<ArrivalIMDGTanker>().InsertRange(IMDGTankerList);
             
            }

            if (IMDGInformationList.Count > 0)
            {
                foreach (var IMDGInformation in IMDGInformationList)
                {
                    IMDGInformation.VCN = _arrivalnotification.VCN;
                    IMDGInformation.CreatedBy = _arrivalnotification.CreatedBy;
                    IMDGInformation.CreatedDate = _arrivalnotification.CreatedDate;
                    IMDGInformation.ModifiedBy = _arrivalnotification.ModifiedBy;
                    IMDGInformation.ModifiedDate = _arrivalnotification.ModifiedDate;
                    IMDGInformation.RecordStatus = "A";
                  

                }
                _unitOfWork.Repository<IMDGInformation>().InsertRange(IMDGInformationList);            
            }
            

            if (WasteDeclarationList.Count > 0)
            {
                foreach (var WasteDeclaration in WasteDeclarationList)
                {                    
                    WasteDeclaration.VCN = _arrivalnotification.VCN;
                    WasteDeclaration.CreatedBy = _arrivalnotification.CreatedBy;
                    WasteDeclaration.CreatedDate = _arrivalnotification.CreatedDate;
                    WasteDeclaration.ModifiedBy = _arrivalnotification.ModifiedBy;
                    WasteDeclaration.ModifiedDate = _arrivalnotification.ModifiedDate;
                    WasteDeclaration.RecordStatus = "A";                   

                }
                _unitOfWork.Repository<WasteDeclaration>().InsertRange(WasteDeclarationList);
            }
            _unitOfWork.SaveChanges();


            _arrivalnotification.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<ArrivalNotification>().Update(_arrivalnotification);
            _unitOfWork.SaveChanges();
        }

        public void Reject()
        {
            _arrivalnotification.ObjectState = ObjectState.Modified;
            _arrivalnotification.RecordStatus = "I";
            _arrivalnotification.CancelRemarks = _remarks;
            _arrivalnotification.ModifiedDate = DateTime.Now;
            _unitOfWork.Repository<ArrivalNotification>().Update(_arrivalnotification);
            _unitOfWork.SaveChanges();
        }    


        /// <summary>
        /// ////Cancel : By Mahesh
        /// </summary>
        /// 
        public void Cancel()
        {         
                _unitOfWork.ExecuteSqlCommand(" update dbo.ArrivalDocument set RecordStatus = 'I',ModifiedBy='" + _arrivalnotification.ModifiedBy + "',ModifiedDate='" + _arrivalnotification.ModifiedDate + "' where VCN = @p0", _arrivalnotification.VCN);
        
                _unitOfWork.ExecuteSqlCommand(" update dbo.ArrivalCommodity set RecordStatus = 'I',ModifiedBy='" + _arrivalnotification.ModifiedBy + "',ModifiedDate='" + _arrivalnotification.ModifiedDate + "'  where VCN = @p0", _arrivalnotification.VCN);
         
                _unitOfWork.ExecuteSqlCommand(" update dbo.ArrivalIMDGTanker set RecordStatus = 'I',ModifiedBy='" + _arrivalnotification.ModifiedBy + "',ModifiedDate='" + _arrivalnotification.ModifiedDate + "'  where VCN = @p0", _arrivalnotification.VCN);
          
                _unitOfWork.ExecuteSqlCommand(" update dbo.IMDGInformation set RecordStatus = 'I',ModifiedBy='" + _arrivalnotification.ModifiedBy + "',ModifiedDate='" + _arrivalnotification.ModifiedDate + "'  where VCN = @p0", _arrivalnotification.VCN);
         
                _unitOfWork.ExecuteSqlCommand(" update dbo.ArrivalNotification set RecordStatus = 'I', CancelRemarks = '" + _arrivalnotification.CancelRemarks + "' ,ModifiedBy='" + _arrivalnotification.ModifiedBy + "',ModifiedDate='" + _arrivalnotification.ModifiedDate + "'  where VCN = @p0", _arrivalnotification.VCN);
         
                _unitOfWork.ExecuteSqlCommand(" update dbo.ArrivalReason set RecordStatus = 'I',ModifiedBy='" + _arrivalnotification.ModifiedBy + "',ModifiedDate='" + _arrivalnotification.ModifiedDate + "'  where VCN = @p0", _arrivalnotification.VCN);

                _unitOfWork.ExecuteSqlCommand(" update dbo.WasteDeclaration set RecordStatus = 'I',ModifiedBy='" + _arrivalnotification.ModifiedBy + "',ModifiedDate='" + _arrivalnotification.ModifiedDate + "'  where VCN = @p0", _arrivalnotification.VCN);

        }


        /// <summary>
        /// to Add Company details
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public CompanyVO GetCompanyDetails(int step)
        {
            return vo;
        }


    }

}