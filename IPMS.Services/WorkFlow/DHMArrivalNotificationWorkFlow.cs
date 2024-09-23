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
    public class DHMArrivalNotificationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ArrivalNotification _DHMArrivalNotification;
        private const string _entityCode = EntityCodes.DHMAN;
        private IAccountRepository _accountRepository;
        private CompanyVO vo;   

        private string _remarks;

          public DHMArrivalNotificationWorkFlow(IUnitOfWork unitOfWork, ArrivalNotification notification, string remarks)
        {
            _unitOfWork = unitOfWork;
            _DHMArrivalNotification = notification;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
            vo = new CompanyVO();
        }

          public DHMArrivalNotificationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

        public int userid
        {
            get { return _DHMArrivalNotification.CreatedBy; }
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
            get { return _DHMArrivalNotification.VCN; }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

         public string ReferenceData
        {
            get {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return Common.GetTokensDictionaryForReferenceData(entity, _DHMArrivalNotification);
            }
        }

        public int GetRequestStatus(string p_entitycode, string p_referenceno)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == p_entitycode
                                 && w.ReferenceID == p_referenceno

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
                portcodes.Add(_DHMArrivalNotification.PortCode);
                return portcodes;
            }
        }

        public void Create()
        {

        }

        public void UpdateStatus()
        {


            List<ArrivalCommodity> commodityList = _DHMArrivalNotification.ArrivalCommodities.ToList();
            List<ArrivalIMDGTanker> IMDGTankerList = _DHMArrivalNotification.ArrivalIMDGTankers.ToList();
            List<ArrivalDocument> arrivalDocumentList = _DHMArrivalNotification.ArrivalDocuments.ToList();
            List<IMDGInformation> IMDGInformationList = _DHMArrivalNotification.IMDGInformations.ToList();
            List<ArrivalReason> ArrivalReasonList = _DHMArrivalNotification.ArrivalReasons.ToList();
            List<WasteDeclaration> WasteDeclarationList = _DHMArrivalNotification.WasteDeclarations.ToList();

            //TODO: Inline statements to be removed here : Bhoji
            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalDocument where VCN = @p0", _DHMArrivalNotification.VCN);
            _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalReason where VCN = @p0", _DHMArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalCommodity where VCN = @p0", _DHMArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalIMDGTanker where VCN = @p0", _DHMArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.IMDGInformation where VCN = @p0", _DHMArrivalNotification.VCN);

            _unitOfWork.ExecuteSqlCommand("delete dbo.WasteDeclaration where VCN = @p0", _DHMArrivalNotification.VCN);

            if (_DHMArrivalNotification.Vessel == null)
                _DHMArrivalNotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_DHMArrivalNotification.VesselID);

            
            

            if (ArrivalReasonList.Count > 0)
            {
                foreach (var reasons in ArrivalReasonList)
                {
                    reasons.VCN = _DHMArrivalNotification.VCN;
                    reasons.CreatedBy = _DHMArrivalNotification.CreatedBy;
                    reasons.CreatedDate = _DHMArrivalNotification.CreatedDate;
                    reasons.ModifiedBy = _DHMArrivalNotification.ModifiedBy;
                    reasons.ModifiedDate = _DHMArrivalNotification.ModifiedDate;
                    reasons.RecordStatus = "A";

                }
                _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

            }



            if (arrivalDocumentList.Count > 0)
            {
                foreach (var document in arrivalDocumentList)
                {
                    document.VCN = _DHMArrivalNotification.VCN;
                    document.CreatedBy = _DHMArrivalNotification.CreatedBy;
                    document.CreatedDate = _DHMArrivalNotification.CreatedDate;
                    document.ModifiedBy = _DHMArrivalNotification.ModifiedBy;
                    document.ModifiedDate = _DHMArrivalNotification.ModifiedDate;
                    document.RecordStatus = "A";
                }

                _unitOfWork.Repository<ArrivalDocument>().InsertRange(arrivalDocumentList);
            }

            if (commodityList.Count > 0)
            {
                foreach (var commodity in commodityList)
                {
                    commodity.VCN = _DHMArrivalNotification.VCN;
                    commodity.CreatedBy = _DHMArrivalNotification.CreatedBy;
                    commodity.CreatedDate = _DHMArrivalNotification.CreatedDate;
                    commodity.ModifiedBy = _DHMArrivalNotification.ModifiedBy;
                    commodity.ModifiedDate = _DHMArrivalNotification.ModifiedDate;
                    commodity.RecordStatus = "A";                   
                }
               
                _unitOfWork.Repository<ArrivalCommodity>().InsertRange(commodityList);
            }

            if (IMDGTankerList.Count > 0)
            {
                foreach (var IMDGTanker in IMDGTankerList)
                {
                    IMDGTanker.VCN = _DHMArrivalNotification.VCN;
                    IMDGTanker.CreatedBy = _DHMArrivalNotification.CreatedBy;
                    IMDGTanker.CreatedDate = _DHMArrivalNotification.CreatedDate;
                    IMDGTanker.ModifiedBy = _DHMArrivalNotification.ModifiedBy;
                    IMDGTanker.ModifiedDate = _DHMArrivalNotification.ModifiedDate;
                    IMDGTanker.RecordStatus = "A";               

                }
                _unitOfWork.Repository<ArrivalIMDGTanker>().InsertRange(IMDGTankerList);
              
            }

            if (IMDGInformationList.Count > 0)
            {
                foreach (var IMDGInformation in IMDGInformationList)
                {
                    IMDGInformation.VCN = _DHMArrivalNotification.VCN;
                    IMDGInformation.CreatedBy = _DHMArrivalNotification.CreatedBy;
                    IMDGInformation.CreatedDate = _DHMArrivalNotification.CreatedDate;
                    IMDGInformation.ModifiedBy = _DHMArrivalNotification.ModifiedBy;
                    IMDGInformation.ModifiedDate = _DHMArrivalNotification.ModifiedDate;
                    IMDGInformation.RecordStatus = "A";


                }
                _unitOfWork.Repository<IMDGInformation>().InsertRange(IMDGInformationList);
                
            }


            if (WasteDeclarationList.Count > 0)
            {
                foreach (var WasteDeclaration in WasteDeclarationList)
                {
                    WasteDeclaration.VCN = _DHMArrivalNotification.VCN;
                    WasteDeclaration.CreatedBy = _DHMArrivalNotification.CreatedBy;
                    WasteDeclaration.CreatedDate = _DHMArrivalNotification.CreatedDate;
                    WasteDeclaration.ModifiedBy = _DHMArrivalNotification.ModifiedBy;
                    WasteDeclaration.ModifiedDate = _DHMArrivalNotification.ModifiedDate;
                    WasteDeclaration.RecordStatus = "A";
                   
                }
                _unitOfWork.Repository<WasteDeclaration>().InsertRange(WasteDeclarationList);
                
            }

            _unitOfWork.SaveChanges();

            _DHMArrivalNotification.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<ArrivalNotification>().Update(_DHMArrivalNotification);
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
                    vo.UserTypeId = _DHMArrivalNotification.AgentID;
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
