using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;

 
namespace IPMS.Services.WorkFlow
{
    public class ISPSArrivalNotificationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ArrivalNotification _ISPSArrivalNotification;
        private const string _entityCode = EntityCodes.ISPSAN;
        private IAccountRepository _accountRepository;
        private CompanyVO vo;       

        private string _remarks;

        public ISPSArrivalNotificationWorkFlow(IUnitOfWork unitOfWork, ArrivalNotification notification, string remarks)
        {
            _unitOfWork = unitOfWork;
            _ISPSArrivalNotification = notification;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
            vo = new CompanyVO();
        }

        public ISPSArrivalNotificationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

        public int userid
        {
            get { return _ISPSArrivalNotification.CreatedBy; }
        }


        public Entity Entity
        {
            get
            {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return entity;
            }
        }

        public string ReferenceId
        {
            get { return _ISPSArrivalNotification.VCN; }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

         public string ReferenceData
        {
            get {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return Common.GetTokensDictionaryForReferenceData(entity, _ISPSArrivalNotification);
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

        private string GetTokensDictionaryForArrivalNotification(Entity entity, ArrivalNotification arrivalNotification)
        {


            Dictionary<string, string> tokensDict = new Dictionary<string, string>();

            var js = new JavaScriptSerializer();
            string[] arrayOfTokensForEntity = entity.Tokens.Split(',');




            foreach (string _attribute in arrayOfTokensForEntity)
            {

                object _objval = GetProperty(arrivalNotification, _attribute);

                tokensDict.Add(_attribute, Convert.ToString(_objval, CultureInfo.InvariantCulture));

            }

            return js.Serialize(tokensDict);
        }

        private object GetProperty(object t, string PropertName)
        {
            object value = "";
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(t);

            foreach (PropertyDescriptor p in props)
            {

                string name = ((System.ComponentModel.MemberDescriptor)(p)).DisplayName;
                value = p.GetValue(t);
                if (PropertName == name)
                {
                    return Convert.ToString(value, CultureInfo.InvariantCulture);

                }
                else
                {
                    if (value != null)
                    {

                        if (value.GetType().Name == ((System.ComponentModel.MemberDescriptor)(p)).DisplayName || value.GetType().Name == "IList`1" || value.GetType().Name == "ICollection`1")
                        {
                            return GetProperty(p.GetValue(t), PropertName);
                        }
                    }

                }
            }
            return value;

        }

        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(_ISPSArrivalNotification.PortCode);
                return portcodes;
            }
        }

        public void Create()
        {

        }

        public void UpdateStatus()
        {


            List<ArrivalCommodity> commodityList = _ISPSArrivalNotification.ArrivalCommodities.ToList();
            List<ArrivalIMDGTanker> IMDGTankerList = _ISPSArrivalNotification.ArrivalIMDGTankers.ToList();
            List<ArrivalDocument> arrivalDocumentList = _ISPSArrivalNotification.ArrivalDocuments.ToList();
            List<IMDGInformation> IMDGInformationList = _ISPSArrivalNotification.IMDGInformations.ToList();
            List<ArrivalReason> ArrivalReasonList = _ISPSArrivalNotification.ArrivalReasons.ToList();
            List<WasteDeclaration> WasteDeclarationList = _ISPSArrivalNotification.WasteDeclarations.ToList();
            //TODO: Inline statements to be removed here : Bhoji
            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalDocument where VCN = @p0", _ISPSArrivalNotification.VCN);
            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalReason where VCN = @p0", _ISPSArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalCommodity where VCN = @p0", _ISPSArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalIMDGTanker where VCN = @p0", _ISPSArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.IMDGInformation where VCN = @p0", _ISPSArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.WasteDeclaration where VCN = @p0", _ISPSArrivalNotification.VCN);


            if (_ISPSArrivalNotification.Vessel == null)
                _ISPSArrivalNotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_ISPSArrivalNotification.VesselID);

            
            

            if (ArrivalReasonList.Count > 0)
            {
                foreach (var reasons in ArrivalReasonList)
                {
                    reasons.VCN = _ISPSArrivalNotification.VCN;
                    reasons.CreatedBy = _ISPSArrivalNotification.CreatedBy;
                    reasons.CreatedDate = _ISPSArrivalNotification.CreatedDate;
                    reasons.ModifiedBy = _ISPSArrivalNotification.ModifiedBy;
                    reasons.ModifiedDate = _ISPSArrivalNotification.ModifiedDate;
                    reasons.RecordStatus = "A";

                }
                _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

            }



            if (arrivalDocumentList.Count > 0)
            {
                foreach (var document in arrivalDocumentList)
                {
                    document.VCN = _ISPSArrivalNotification.VCN;
                    document.CreatedBy = _ISPSArrivalNotification.CreatedBy;
                    document.CreatedDate = _ISPSArrivalNotification.CreatedDate;
                    document.ModifiedBy = _ISPSArrivalNotification.ModifiedBy;
                    document.ModifiedDate = _ISPSArrivalNotification.ModifiedDate;
                    document.RecordStatus = "A";
                }

                _unitOfWork.Repository<ArrivalDocument>().InsertRange(arrivalDocumentList);
            }

            if (commodityList.Count > 0)
            {
                foreach (var commodity in commodityList)
                {
                    commodity.VCN = _ISPSArrivalNotification.VCN;
                    commodity.CreatedBy = _ISPSArrivalNotification.CreatedBy;
                    commodity.CreatedDate = _ISPSArrivalNotification.CreatedDate;
                    commodity.ModifiedBy = _ISPSArrivalNotification.ModifiedBy;
                    commodity.ModifiedDate = _ISPSArrivalNotification.ModifiedDate;
                    commodity.RecordStatus = "A";
                 
                }
                _unitOfWork.Repository<ArrivalCommodity>().InsertRange(commodityList);
             
            }

            if (IMDGTankerList.Count > 0)
            {
                foreach (var IMDGTanker in IMDGTankerList)
                {
                    IMDGTanker.VCN = _ISPSArrivalNotification.VCN;
                    IMDGTanker.CreatedBy = _ISPSArrivalNotification.CreatedBy;
                    IMDGTanker.CreatedDate = _ISPSArrivalNotification.CreatedDate;
                    IMDGTanker.ModifiedBy = _ISPSArrivalNotification.ModifiedBy;
                    IMDGTanker.ModifiedDate = _ISPSArrivalNotification.ModifiedDate;
                    IMDGTanker.RecordStatus = "A";


                }
                _unitOfWork.Repository<ArrivalIMDGTanker>().InsertRange(IMDGTankerList);
               
            }

            if (IMDGInformationList.Count > 0)
            {
                foreach (var IMDGInformation in IMDGInformationList)
                {
                    IMDGInformation.VCN = _ISPSArrivalNotification.VCN;
                    IMDGInformation.CreatedBy = _ISPSArrivalNotification.CreatedBy;
                    IMDGInformation.CreatedDate = _ISPSArrivalNotification.CreatedDate;
                    IMDGInformation.ModifiedBy = _ISPSArrivalNotification.ModifiedBy;
                    IMDGInformation.ModifiedDate = _ISPSArrivalNotification.ModifiedDate;
                    IMDGInformation.RecordStatus = "A";

                   

                }
                _unitOfWork.Repository<IMDGInformation>().InsertRange(IMDGInformationList);
               
            }


            if (WasteDeclarationList.Count > 0)
            {
                foreach (var WasteDeclaration in WasteDeclarationList)
                {
                    WasteDeclaration.VCN = _ISPSArrivalNotification.VCN;
                    WasteDeclaration.CreatedBy = _ISPSArrivalNotification.CreatedBy;
                    WasteDeclaration.CreatedDate = _ISPSArrivalNotification.CreatedDate;
                    WasteDeclaration.ModifiedBy = _ISPSArrivalNotification.ModifiedBy;
                    WasteDeclaration.ModifiedDate = _ISPSArrivalNotification.ModifiedDate;
                    WasteDeclaration.RecordStatus = "A";

                   

                }
                _unitOfWork.Repository<WasteDeclaration>().InsertRange(WasteDeclarationList);
                
            }
            _unitOfWork.SaveChanges();

            _ISPSArrivalNotification.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<ArrivalNotification>().Update(_ISPSArrivalNotification);
            _unitOfWork.SaveChanges();
        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
          
        }

        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "VAP":
                    break;
                case "VRES":
                    vo.UserType = UserType.Agent;
                    vo.UserTypeId = _ISPSArrivalNotification.AgentID;
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
                case "WFSA":
                    _unitOfWork.ExecuteSqlCommand("update dbo.ArrivalNotification SET Clearance = 'A'  WHERE VCN = @p0", _ISPSArrivalNotification.VCN);
                    break;
                case "WFRE":
                    break;
             
            }
        }

        public CompanyVO GetCompanyDetails(int step)
        {           
            return vo;
        }


    }
}