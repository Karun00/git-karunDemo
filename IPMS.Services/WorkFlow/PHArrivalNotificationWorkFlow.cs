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
    public class PHArrivalNotificationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ArrivalNotification _PHArrivalNotification;
        private const string _entityCode = EntityCodes.PortHealthAN;
        private IAccountRepository _accountRepository;
        private CompanyVO vo;    

        private string _remarks;

        public PHArrivalNotificationWorkFlow(IUnitOfWork unitOfWork, ArrivalNotification notification, string remarks)
        {
            _unitOfWork = unitOfWork;
            _PHArrivalNotification = notification;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
            vo = new CompanyVO();
        }

        public PHArrivalNotificationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

        public int userid
        {
            get { return _PHArrivalNotification.CreatedBy; }
        }


        public Entity Entity
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return entity;
            }
        }

        public string ReferenceId
        {
            get { return _PHArrivalNotification.VCN; }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

         public string ReferenceData
        {
            get {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return Common.GetTokensDictionaryForReferenceData(entity, _PHArrivalNotification);
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
                portcodes.Add(_PHArrivalNotification.PortCode);
                return portcodes;
            }
        }

        public void UpdateStatus()
        {


            List<ArrivalCommodity> commodityList = _PHArrivalNotification.ArrivalCommodities.ToList();
            List<ArrivalIMDGTanker> IMDGTankerList = _PHArrivalNotification.ArrivalIMDGTankers.ToList();
            List<ArrivalDocument> arrivalDocumentList = _PHArrivalNotification.ArrivalDocuments.ToList();
            List<IMDGInformation> IMDGInformationList = _PHArrivalNotification.IMDGInformations.ToList();
            List<ArrivalReason> ArrivalReasonList = _PHArrivalNotification.ArrivalReasons.ToList();
            List<WasteDeclaration> WasteDeclarationList = _PHArrivalNotification.WasteDeclarations.ToList();

            //TODO: Inline statements to be removed here : Bhoji
            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalDocument where VCN = @p0", _PHArrivalNotification.VCN);
            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalReason where VCN = @p0", _PHArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalCommodity where VCN = @p0", _PHArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalIMDGTanker where VCN = @p0", _PHArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.IMDGInformation where VCN = @p0", _PHArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.WasteDeclaration where VCN = @p0", _PHArrivalNotification.VCN);

            if (_PHArrivalNotification.Vessel == null)
                _PHArrivalNotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_PHArrivalNotification.VesselID);


            if (ArrivalReasonList.Count > 0)
            {
                foreach (var reasons in ArrivalReasonList)
                {
                    reasons.VCN = _PHArrivalNotification.VCN;
                    reasons.CreatedBy = _PHArrivalNotification.CreatedBy;
                    reasons.CreatedDate = _PHArrivalNotification.CreatedDate;
                    reasons.ModifiedBy = _PHArrivalNotification.ModifiedBy;
                    reasons.ModifiedDate = _PHArrivalNotification.ModifiedDate;
                    reasons.RecordStatus = "A";

                }
                _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

            }

            if (arrivalDocumentList.Count > 0)
            {
                foreach (var document in arrivalDocumentList)
                {
                    document.VCN = _PHArrivalNotification.VCN;
                    document.CreatedBy = _PHArrivalNotification.CreatedBy;
                    document.CreatedDate = _PHArrivalNotification.CreatedDate;
                    document.ModifiedBy = _PHArrivalNotification.ModifiedBy;
                    document.ModifiedDate = _PHArrivalNotification.ModifiedDate;
                    document.RecordStatus = "A";
                }

                _unitOfWork.Repository<ArrivalDocument>().InsertRange(arrivalDocumentList);
            }

            if (commodityList.Count > 0)
            {
                foreach (var commodity in commodityList)
                {
                    commodity.VCN = _PHArrivalNotification.VCN;
                    commodity.CreatedBy = _PHArrivalNotification.CreatedBy;
                    commodity.CreatedDate = _PHArrivalNotification.CreatedDate;
                    commodity.ModifiedBy = _PHArrivalNotification.ModifiedBy;
                    commodity.ModifiedDate = _PHArrivalNotification.ModifiedDate;
                    commodity.RecordStatus = "A";
                   
                }
                _unitOfWork.Repository<ArrivalCommodity>().InsertRange(commodityList);
             
            }

            if (IMDGTankerList.Count > 0)
            {
                foreach (var IMDGTanker in IMDGTankerList)
                {
                    IMDGTanker.VCN = _PHArrivalNotification.VCN;
                    IMDGTanker.CreatedBy = _PHArrivalNotification.CreatedBy;
                    IMDGTanker.CreatedDate = _PHArrivalNotification.CreatedDate;
                    IMDGTanker.ModifiedBy = _PHArrivalNotification.ModifiedBy;
                    IMDGTanker.ModifiedDate = _PHArrivalNotification.ModifiedDate;
                    IMDGTanker.RecordStatus = "A";

                    

                }
                _unitOfWork.Repository<ArrivalIMDGTanker>().InsertRange(IMDGTankerList);
              
            }

            if (IMDGInformationList.Count > 0)
            {
                foreach (var IMDGInformation in IMDGInformationList)
                {
                    IMDGInformation.VCN = _PHArrivalNotification.VCN;
                    IMDGInformation.CreatedBy = _PHArrivalNotification.CreatedBy;
                    IMDGInformation.CreatedDate = _PHArrivalNotification.CreatedDate;
                    IMDGInformation.ModifiedBy = _PHArrivalNotification.ModifiedBy;
                    IMDGInformation.ModifiedDate = _PHArrivalNotification.ModifiedDate;
                    IMDGInformation.RecordStatus = "A";
                   

                }
                _unitOfWork.Repository<IMDGInformation>().InsertRange(IMDGInformationList);
              
            }

            if (WasteDeclarationList.Count > 0)
            {
                foreach (var WasteDeclaration in WasteDeclarationList)
                {
                    WasteDeclaration.VCN = _PHArrivalNotification.VCN;
                    WasteDeclaration.CreatedBy = _PHArrivalNotification.CreatedBy;
                    WasteDeclaration.CreatedDate = _PHArrivalNotification.CreatedDate;
                    WasteDeclaration.ModifiedBy = _PHArrivalNotification.ModifiedBy;
                    WasteDeclaration.ModifiedDate = _PHArrivalNotification.ModifiedDate;
                    WasteDeclaration.RecordStatus = "A";
                }
                _unitOfWork.Repository<WasteDeclaration>().InsertRange(WasteDeclarationList);
                
            }
            _unitOfWork.SaveChanges();


            _PHArrivalNotification.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<ArrivalNotification>().Update(_PHArrivalNotification);
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
                    vo.UserTypeId = _PHArrivalNotification.AgentID;
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