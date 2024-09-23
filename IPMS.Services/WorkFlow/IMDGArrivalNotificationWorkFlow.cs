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
    public class IMDGArrivalNotificationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ArrivalNotification _IMDGArrivalNotification;
        private const string _entityCode = EntityCodes.IMDGAN;
        private IAccountRepository _accountRepository;
        private CompanyVO vo;       

        private string _remarks;

        public IMDGArrivalNotificationWorkFlow(IUnitOfWork unitOfWork, ArrivalNotification notification, string remarks)
        {
            _unitOfWork = unitOfWork;
            _IMDGArrivalNotification = notification;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
            vo = new CompanyVO();
        }

        public IMDGArrivalNotificationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

        public int userid
        {
            get { return _IMDGArrivalNotification.CreatedBy; }
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
            get { return _IMDGArrivalNotification.VCN; }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

         public string ReferenceData
        {
            get {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return Common.GetTokensDictionaryForReferenceData(entity, _IMDGArrivalNotification);
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
                portcodes.Add(_IMDGArrivalNotification.PortCode);
                return portcodes;
            }
        }

        public void Create()
        {

        }

        public void UpdateStatus()
        {


            List<ArrivalCommodity> commodityList = _IMDGArrivalNotification.ArrivalCommodities.ToList();
            List<ArrivalIMDGTanker> IMDGTankerList = _IMDGArrivalNotification.ArrivalIMDGTankers.ToList();
            List<ArrivalDocument> arrivalDocumentList = _IMDGArrivalNotification.ArrivalDocuments.ToList();
            List<IMDGInformation> IMDGInformationList = _IMDGArrivalNotification.IMDGInformations.ToList();
            List<ArrivalReason> ArrivalReasonList = _IMDGArrivalNotification.ArrivalReasons.ToList();
            List<WasteDeclaration> WasteDeclarationList = _IMDGArrivalNotification.WasteDeclarations.ToList();
            //TODO: Inline statements to be removed here : Bhoji
            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalDocument where VCN = @p0", _IMDGArrivalNotification.VCN);
            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalReason where VCN = @p0", _IMDGArrivalNotification.VCN);


            _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalCommodity where VCN = @p0", _IMDGArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalIMDGTanker where VCN = @p0", _IMDGArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.IMDGInformation where VCN = @p0", _IMDGArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.WasteDeclaration where VCN = @p0", _IMDGArrivalNotification.VCN);


            if (_IMDGArrivalNotification.Vessel == null)
                _IMDGArrivalNotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_IMDGArrivalNotification.VesselID);

            if (ArrivalReasonList.Count > 0)
            {
                foreach (var reasons in ArrivalReasonList)
                {
                    reasons.VCN = _IMDGArrivalNotification.VCN;
                    reasons.CreatedBy = _IMDGArrivalNotification.CreatedBy;
                    reasons.CreatedDate = _IMDGArrivalNotification.CreatedDate;
                    reasons.ModifiedBy = _IMDGArrivalNotification.ModifiedBy;
                    reasons.ModifiedDate = _IMDGArrivalNotification.ModifiedDate;
                    reasons.RecordStatus = "A";

                }
                _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

            }

            if (_IMDGArrivalNotification.Vessel == null)
                _IMDGArrivalNotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_IMDGArrivalNotification.VesselID);

            if (arrivalDocumentList.Count > 0)
            {
                foreach (var document in arrivalDocumentList)
                {
                    document.VCN = _IMDGArrivalNotification.VCN;
                    document.CreatedBy = _IMDGArrivalNotification.CreatedBy;
                    document.CreatedDate = _IMDGArrivalNotification.CreatedDate;
                    document.ModifiedBy = _IMDGArrivalNotification.ModifiedBy;
                    document.ModifiedDate = _IMDGArrivalNotification.ModifiedDate;
                    document.RecordStatus = "A";
                }

                _unitOfWork.Repository<ArrivalDocument>().InsertRange(arrivalDocumentList);
            }

            if (commodityList.Count > 0)
            {
                foreach (var commodity in commodityList)
                {
                    commodity.VCN = _IMDGArrivalNotification.VCN;
                    commodity.CreatedBy = _IMDGArrivalNotification.CreatedBy;
                    commodity.CreatedDate = _IMDGArrivalNotification.CreatedDate;
                    commodity.ModifiedBy = _IMDGArrivalNotification.ModifiedBy;
                    commodity.ModifiedDate = _IMDGArrivalNotification.ModifiedDate;
                    commodity.RecordStatus = "A";                  
                }
                _unitOfWork.Repository<ArrivalCommodity>().InsertRange(commodityList);
            
            }

            if (IMDGTankerList.Count > 0)
            {
                foreach (var IMDGTanker in IMDGTankerList)
                {
                    IMDGTanker.VCN = _IMDGArrivalNotification.VCN;
                    IMDGTanker.CreatedBy = _IMDGArrivalNotification.CreatedBy;
                    IMDGTanker.CreatedDate = _IMDGArrivalNotification.CreatedDate;
                    IMDGTanker.ModifiedBy = _IMDGArrivalNotification.ModifiedBy;
                    IMDGTanker.ModifiedDate = _IMDGArrivalNotification.ModifiedDate;
                    IMDGTanker.RecordStatus = "A";
                  

                }
                _unitOfWork.Repository<ArrivalIMDGTanker>().InsertRange(IMDGTankerList);
              
            }

            if (IMDGInformationList.Count > 0)
            {
                foreach (var IMDGInformation in IMDGInformationList)
                {
                    IMDGInformation.VCN = _IMDGArrivalNotification.VCN;
                    IMDGInformation.CreatedBy = _IMDGArrivalNotification.CreatedBy;
                    IMDGInformation.CreatedDate = _IMDGArrivalNotification.CreatedDate;
                    IMDGInformation.ModifiedBy = _IMDGArrivalNotification.ModifiedBy;
                    IMDGInformation.ModifiedDate = _IMDGArrivalNotification.ModifiedDate;
                    IMDGInformation.RecordStatus = "A";                  

                }
                _unitOfWork.Repository<IMDGInformation>().InsertRange(IMDGInformationList);
              
            }

            if (WasteDeclarationList.Count > 0)
            {
                foreach (var WasteDeclaration in WasteDeclarationList)
                {
                    WasteDeclaration.VCN = _IMDGArrivalNotification.VCN;
                    WasteDeclaration.CreatedBy = _IMDGArrivalNotification.CreatedBy;
                    WasteDeclaration.CreatedDate = _IMDGArrivalNotification.CreatedDate;
                    WasteDeclaration.ModifiedBy = _IMDGArrivalNotification.ModifiedBy;
                    WasteDeclaration.ModifiedDate = _IMDGArrivalNotification.ModifiedDate;
                    WasteDeclaration.RecordStatus = "A";
                  

                }
                _unitOfWork.Repository<WasteDeclaration>().InsertRange(WasteDeclarationList);                
            }

            _unitOfWork.SaveChanges();

            _IMDGArrivalNotification.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<ArrivalNotification>().Update(_IMDGArrivalNotification);
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
                    vo.UserTypeId = _IMDGArrivalNotification.AgentID;
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