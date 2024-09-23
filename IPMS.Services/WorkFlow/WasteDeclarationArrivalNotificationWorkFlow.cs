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
    class WasteDeclarationArrivalNotificationWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ArrivalNotification _WasteDeclarationArrivalNotification;
        private const string _entityCode = EntityCodes.WasteDeclarationAN;
        private IAccountRepository _accountRepository;
        private CompanyVO vo;

        private string _remarks;

          public WasteDeclarationArrivalNotificationWorkFlow(IUnitOfWork unitOfWork, ArrivalNotification notification, string remarks)
        {
            _unitOfWork = unitOfWork;
            _WasteDeclarationArrivalNotification = notification;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
            vo = new CompanyVO();
        }

          public WasteDeclarationArrivalNotificationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

          public int userid
          {
              get { return _WasteDeclarationArrivalNotification.CreatedBy; }
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
              get { return _WasteDeclarationArrivalNotification.VCN; }
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
                  return Common.GetTokensDictionaryForReferenceData(entity, _WasteDeclarationArrivalNotification);
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
                  portcodes.Add(_WasteDeclarationArrivalNotification.PortCode);
                  return portcodes;
              }
          }

          public void Create()
          {

          }

          public void UpdateStatus()
          {


              List<ArrivalCommodity> commodityList = _WasteDeclarationArrivalNotification.ArrivalCommodities.ToList();
              List<ArrivalIMDGTanker> IMDGTankerList = _WasteDeclarationArrivalNotification.ArrivalIMDGTankers.ToList();
              List<ArrivalDocument> arrivalDocumentList = _WasteDeclarationArrivalNotification.ArrivalDocuments.ToList();
              List<IMDGInformation> IMDGInformationList = _WasteDeclarationArrivalNotification.IMDGInformations.ToList();
              List<ArrivalReason> ArrivalReasonList = _WasteDeclarationArrivalNotification.ArrivalReasons.ToList();
              List<WasteDeclaration> WasteDeclarationList = _WasteDeclarationArrivalNotification.WasteDeclarations.ToList();

              //TODO: Inline statements to be removed here : Bhoji
              _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalDocument where VCN = @p0", _WasteDeclarationArrivalNotification.VCN);
              _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalReason where VCN = @p0", _WasteDeclarationArrivalNotification.VCN);

              _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalCommodity where VCN = @p0", _WasteDeclarationArrivalNotification.VCN);

              _unitOfWork.ExecuteSqlCommand("delete dbo.ArrivalIMDGTanker where VCN = @p0", _WasteDeclarationArrivalNotification.VCN);

              _unitOfWork.ExecuteSqlCommand("delete dbo.IMDGInformation where VCN = @p0", _WasteDeclarationArrivalNotification.VCN);

              _unitOfWork.ExecuteSqlCommand("delete dbo.WasteDeclaration where VCN = @p0", _WasteDeclarationArrivalNotification.VCN);

              if (_WasteDeclarationArrivalNotification.Vessel == null)
                  _WasteDeclarationArrivalNotification.Vessel = _unitOfWork.Repository<Vessel>().Find(_WasteDeclarationArrivalNotification.VesselID);




              if (ArrivalReasonList.Count > 0)
              {
                  foreach (var reasons in ArrivalReasonList)
                  {
                      reasons.VCN = _WasteDeclarationArrivalNotification.VCN;
                      reasons.CreatedBy = _WasteDeclarationArrivalNotification.CreatedBy;
                      reasons.CreatedDate = _WasteDeclarationArrivalNotification.CreatedDate;
                      reasons.ModifiedBy = _WasteDeclarationArrivalNotification.ModifiedBy;
                      reasons.ModifiedDate = _WasteDeclarationArrivalNotification.ModifiedDate;
                      reasons.RecordStatus = "A";

                  }
                  _unitOfWork.Repository<ArrivalReason>().InsertRange(ArrivalReasonList);

              }



              if (arrivalDocumentList.Count > 0)
              {
                  foreach (var document in arrivalDocumentList)
                  {
                      document.VCN = _WasteDeclarationArrivalNotification.VCN;
                      document.CreatedBy = _WasteDeclarationArrivalNotification.CreatedBy;
                      document.CreatedDate = _WasteDeclarationArrivalNotification.CreatedDate;
                      document.ModifiedBy = _WasteDeclarationArrivalNotification.ModifiedBy;
                      document.ModifiedDate = _WasteDeclarationArrivalNotification.ModifiedDate;
                      document.RecordStatus = "A";
                  }

                  _unitOfWork.Repository<ArrivalDocument>().InsertRange(arrivalDocumentList);
              }

              if (commodityList.Count > 0)
              {
                  foreach (var commodity in commodityList)
                  {
                      commodity.VCN = _WasteDeclarationArrivalNotification.VCN;
                      commodity.CreatedBy = _WasteDeclarationArrivalNotification.CreatedBy;
                      commodity.CreatedDate = _WasteDeclarationArrivalNotification.CreatedDate;
                      commodity.ModifiedBy = _WasteDeclarationArrivalNotification.ModifiedBy;
                      commodity.ModifiedDate = _WasteDeclarationArrivalNotification.ModifiedDate;
                      commodity.RecordStatus = "A";
                  }

                  _unitOfWork.Repository<ArrivalCommodity>().InsertRange(commodityList);
              }

              if (IMDGTankerList.Count > 0)
              {
                  foreach (var IMDGTanker in IMDGTankerList)
                  {
                      IMDGTanker.VCN = _WasteDeclarationArrivalNotification.VCN;
                      IMDGTanker.CreatedBy = _WasteDeclarationArrivalNotification.CreatedBy;
                      IMDGTanker.CreatedDate = _WasteDeclarationArrivalNotification.CreatedDate;
                      IMDGTanker.ModifiedBy = _WasteDeclarationArrivalNotification.ModifiedBy;
                      IMDGTanker.ModifiedDate = _WasteDeclarationArrivalNotification.ModifiedDate;
                      IMDGTanker.RecordStatus = "A";

                  }
                  _unitOfWork.Repository<ArrivalIMDGTanker>().InsertRange(IMDGTankerList);

              }

              if (IMDGInformationList.Count > 0)
              {
                  foreach (var IMDGInformation in IMDGInformationList)
                  {
                      IMDGInformation.VCN = _WasteDeclarationArrivalNotification.VCN;
                      IMDGInformation.CreatedBy = _WasteDeclarationArrivalNotification.CreatedBy;
                      IMDGInformation.CreatedDate = _WasteDeclarationArrivalNotification.CreatedDate;
                      IMDGInformation.ModifiedBy = _WasteDeclarationArrivalNotification.ModifiedBy;
                      IMDGInformation.ModifiedDate = _WasteDeclarationArrivalNotification.ModifiedDate;
                      IMDGInformation.RecordStatus = "A";


                  }
                  _unitOfWork.Repository<IMDGInformation>().InsertRange(IMDGInformationList);

              }


              if (WasteDeclarationList.Count > 0)
              {
                  foreach (var WasteDeclaration in WasteDeclarationList)
                  {
                      WasteDeclaration.VCN = _WasteDeclarationArrivalNotification.VCN;
                      WasteDeclaration.CreatedBy = _WasteDeclarationArrivalNotification.CreatedBy;
                      WasteDeclaration.CreatedDate = _WasteDeclarationArrivalNotification.CreatedDate;
                      WasteDeclaration.ModifiedBy = _WasteDeclarationArrivalNotification.ModifiedBy;
                      WasteDeclaration.ModifiedDate = _WasteDeclarationArrivalNotification.ModifiedDate;
                      WasteDeclaration.RecordStatus = "A";

                  }
                  _unitOfWork.Repository<WasteDeclaration>().InsertRange(WasteDeclarationList);

              }

              _unitOfWork.SaveChanges();

              _WasteDeclarationArrivalNotification.ObjectState = ObjectState.Modified;
              _unitOfWork.Repository<ArrivalNotification>().Update(_WasteDeclarationArrivalNotification);
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
                      vo.UserTypeId = _WasteDeclarationArrivalNotification.AgentID;
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
