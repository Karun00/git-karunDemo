using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IPMS.Services.WorkFlow
{
    public class LicensingRequestServiceWorkFlow : IWorkFlowEntity
    {

        private readonly IUnitOfWork _unitOfWork;
        private LicenseRequest _licenseRequest;
      //  private LicenseRequestPort _licenseRequestPort;
       // private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.LICENSEREQ;
        private string _remarks;

        public LicensingRequestServiceWorkFlow(IUnitOfWork unitOfWork, LicenseRequest licenserequest, string remarks)
        {
            _unitOfWork = unitOfWork;
            _licenseRequest = licenserequest;
            _remarks = remarks;
            //_accountRepository = new AccountRepository(unitOfWork);
        }

        /// <summary>
        /// To get Created User Id
        /// </summary>
        public int Userid
        {
            get { return _licenseRequest.CreatedBy; }
        }

        /// <summary>
        /// To get entity Details
        /// </summary>
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

        /// <summary>
        /// to Add Company details
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public CompanyVO GetCompanyDetails(int step)
        {
            CompanyVO vo = new CompanyVO();
            vo.UserType = "EMP";
            vo.UserTypeId = 1;
            return vo;
        }

        /// <summary>
        /// To get Request reference id
        /// </summary>
        public string ReferenceId
        {
            get { return _licenseRequest.LicenseRequestID.ToString(CultureInfo.InvariantCulture); }
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
            get
            {
                return Common.GetTokensDictionaryForReferenceData(Entity, _licenseRequest);
            }

        }
        public int GetRequestLicensingRegStatus(string entitycode, int referenceno)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == entitycode
                                 && w.WorkflowInstanceId == referenceno

                               select w).Count();

            return _entitycode;
        }

        /// <summary>
        /// to get GetRequestStatus
        /// </summary>
        /// <param name="entitycode"></param>
        /// <param name="referenceno"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To get Port code List
        /// </summary>
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                foreach (var licreqPort in _licenseRequest.LicenseRequestPorts)
                {
                    portcodes.Add(licreqPort.PortCode);
                }
                return portcodes;
            }
        }

        /// <summary>
        /// LicensingRequestServiceWorkFlow
        /// </summary>
        public LicensingRequestServiceWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }

        /// <summary>
        /// SetWorkFlow for each Port
        /// </summary>
        /// <param name="workFlowInstanceId"></param>
        /// <param name="portCode"></param>
        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            //List<string> portcodes = new List<string>();
            foreach (var LicReqPort in _licenseRequest.LicenseRequestPorts.Where(e => e.PortCode.StartsWith(portCode)).ToList())
            {
                if (LicReqPort.PortCode == portCode)
                {
                    LicReqPort.WorkflowInstanceID = workFlowInstanceId;
                    LicReqPort.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<LicenseRequestPort>().Update(LicReqPort);
                  // _unitOfWork.Repository<LicenseRequest>().Update(_licenseRequest);
                }
            }
            _unitOfWork.SaveChanges();
            int count = GetRequestLicensingRegStatus(_entityCode, workFlowInstanceId);
            if (count > 0)
            {
                //var brt = 
                    _unitOfWork.ExecuteSqlCommand(" update dbo.LicenseRequestPort SET WFStatus =  (select ApproveCode from PortConfiguration where PortCode= (select PortCode from dbo.LicenseRequestPort where workFlowInstanceId=" + workFlowInstanceId + ")) where  workFlowInstanceId = " + workFlowInstanceId + " ");
            }

            string updateQuery = "update dbo.LicenseRequestPort set WorkflowInstanceId = " + workFlowInstanceId.ToString(CultureInfo.InvariantCulture) + " where LicenseRequestID = " +
                 _licenseRequest.LicenseRequestID.ToString( CultureInfo.InvariantCulture) + " and PortCode = '" + portCode + "'";
            _unitOfWork.ExecuteSqlCommand(updateQuery);  
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
                    Create();
                    break;
                case "UPDT":
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

        /// <summary>
        /// Create for Request
        /// </summary>
        public void Create()
        {

            if (_licenseRequest.LicenseRequestID != null && _licenseRequest.LicenseRequestID > 0)
            {
                //UpdateStatus();
                List<LicenseRequestPort> licenseRequestPortList = _licenseRequest.LicenseRequestPorts.ToList();
                _licenseRequest.ObjectState = ObjectState.Modified;
                if (licenseRequestPortList.Count > 0)
                {
                    foreach (var requestforport in licenseRequestPortList)
                    {
                        requestforport.LicenseRequestID = _licenseRequest.LicenseRequestID;
                        requestforport.WFStatus = "WFSN";
                        requestforport.ApprovedBy = _licenseRequest.CreatedBy;
                        requestforport.ApprovedDate = _licenseRequest.CreatedDate;
                        requestforport.CreatedBy = _licenseRequest.CreatedBy;
                        requestforport.CreatedDate = _licenseRequest.CreatedDate;
                        requestforport.VerifiedBy = _licenseRequest.CreatedBy;
                        requestforport.VerifiedDate = _licenseRequest.CreatedDate;
                        requestforport.ModifiedBy = _licenseRequest.ModifiedBy;
                        requestforport.ModifiedDate = _licenseRequest.ModifiedDate;
                        requestforport.RecordStatus = _licenseRequest.RecordStatus;
                    }
                    _unitOfWork.Repository<LicenseRequestPort>().InsertRange(licenseRequestPortList);
                    _unitOfWork.SaveChanges();
                   
                }
            }
            else
            {

                _licenseRequest.BusinessAddress.CreatedBy = _licenseRequest.CreatedBy;
                _licenseRequest.BusinessAddress.CreatedDate = _licenseRequest.CreatedDate;
                _licenseRequest.BusinessAddress.ModifiedBy = _licenseRequest.CreatedBy;
                _licenseRequest.BusinessAddress.ModifiedDate = _licenseRequest.CreatedDate;
                _licenseRequest.BusinessAddress.RecordStatus = "A";
                _unitOfWork.Repository<Address>().Insert(_licenseRequest.BusinessAddress);
                _licenseRequest.BusinessAddressID = _licenseRequest.BusinessAddress.AddressID;

                if (_licenseRequest.SubCategory == null)
                    _licenseRequest.SubCategory = _unitOfWork.Repository<SubCategory>().Find(_licenseRequest.LicenseRequestType);

                if (_licenseRequest.PostalAddress != null)
                {
                    _licenseRequest.PostalAddress.CreatedBy = _licenseRequest.CreatedBy;
                    _licenseRequest.PostalAddress.CreatedDate = _licenseRequest.CreatedDate;
                    _licenseRequest.PostalAddress.ModifiedBy = _licenseRequest.CreatedBy;
                    _licenseRequest.PostalAddress.ModifiedDate = _licenseRequest.CreatedDate;
                    _licenseRequest.PostalAddress.RecordStatus = "A";
                    _unitOfWork.Repository<Address>().Insert(_licenseRequest.PostalAddress);
                    _licenseRequest.PostalAddressID = _licenseRequest.PostalAddress.AddressID;
                }

                if (_licenseRequest.AuthorizedContactPerson != null)
                {
                    _licenseRequest.AuthorizedContactPerson.CreatedBy = _licenseRequest.CreatedBy;
                    _licenseRequest.AuthorizedContactPerson.CreatedDate = _licenseRequest.CreatedDate;
                    _licenseRequest.AuthorizedContactPerson.ModifiedBy = _licenseRequest.CreatedBy;
                    _licenseRequest.AuthorizedContactPerson.ModifiedDate = _licenseRequest.CreatedDate;
                    _licenseRequest.AuthorizedContactPerson.RecordStatus = "A";
                    _licenseRequest.AuthorizedContactPerson.AuthorizedContactPersonType = "EMP";
                    _unitOfWork.Repository<AuthorizedContactPerson>().Insert(_licenseRequest.AuthorizedContactPerson);
                    _licenseRequest.AuthorizedContactPersonID = _licenseRequest.AuthorizedContactPerson.AuthorizedContactPersonID;
                }

                List<LicenseRequestPort> licenseRequestPortList = _licenseRequest.LicenseRequestPorts.ToList();
                List<LicenseRequestDocument> licenseRequestDocumentList = _licenseRequest.LicenseRequestDocuments.ToList();
                List<Bunkering> BunkeringList = _licenseRequest.Bunkerings.ToList();
                List<FireEquipment> FireEquipmentList = _licenseRequest.FireEquipments.ToList();
                List<FireProtection> FireProtectionList = _licenseRequest.FireProtections.ToList();
                List<PestControl> PestControlList = _licenseRequest.PestControls.ToList();
                List<FloatingCrane> FloatingCraneList = _licenseRequest.FloatingCranes.ToList();
                List<Stevedore> StevedoreList = _licenseRequest.Stevedores.ToList();
                List<PollutionControl> PollutionControlList = _licenseRequest.PollutionControls.ToList();
                List<Diving> DivingList = _licenseRequest.Divings.ToList();

                _licenseRequest.LicenseRequestDocuments = null;
                _licenseRequest.LicenseRequestPorts = null;
                _licenseRequest.Bunkerings = null;
                _licenseRequest.FireEquipments = null;
                _licenseRequest.FireProtections = null;
                _licenseRequest.PestControls = null;
                _licenseRequest.FloatingCranes = null;
                _licenseRequest.Stevedores = null;
                _licenseRequest.PollutionControls = null;
                _licenseRequest.Divings = null;

                CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                //_licenseRequest.ReferenceNo = codeGenerator.GenerateLicenseefno("LIR", licenseRequestPortList[0].PortCode);
                string Reno = "LIR";
                _licenseRequest.ReferenceNo = Reno + codeGenerator.GenerateReferenceNumber();
                _unitOfWork.Repository<LicenseRequest>().Insert(_licenseRequest);


                _licenseRequest.ObjectState = ObjectState.Added;
                if (licenseRequestPortList.Count > 0)
                {
                    foreach (var requestforport in licenseRequestPortList)
                    {
                        requestforport.LicenseRequestID = _licenseRequest.LicenseRequestID;
                        requestforport.WFStatus = "WFSN";
                        requestforport.ApprovedBy = _licenseRequest.CreatedBy;
                        requestforport.ApprovedDate = _licenseRequest.CreatedDate;
                        requestforport.CreatedBy = _licenseRequest.CreatedBy;
                        requestforport.CreatedDate = _licenseRequest.CreatedDate;
                        requestforport.VerifiedBy = _licenseRequest.CreatedBy;
                        requestforport.VerifiedDate = _licenseRequest.CreatedDate;
                        requestforport.ModifiedBy = _licenseRequest.ModifiedBy;
                        requestforport.ModifiedDate = _licenseRequest.ModifiedDate;
                        requestforport.RecordStatus = _licenseRequest.RecordStatus;
                    }
                    _unitOfWork.Repository<LicenseRequestPort>().InsertRange(licenseRequestPortList);
                }


                if (licenseRequestDocumentList.Count > 0)
                {
                    foreach (var licenseRequestDocument in licenseRequestDocumentList)
                    {
                        licenseRequestDocument.LicenseRequestID = _licenseRequest.LicenseRequestID;
                        licenseRequestDocument.CreatedBy = _licenseRequest.CreatedBy;
                        licenseRequestDocument.CreatedDate = _licenseRequest.CreatedDate;
                        licenseRequestDocument.ModifiedBy = _licenseRequest.CreatedBy;
                        licenseRequestDocument.ModifiedDate = _licenseRequest.ModifiedDate;
                        licenseRequestDocument.RecordStatus = _licenseRequest.RecordStatus;
                    }
                    _unitOfWork.Repository<LicenseRequestDocument>().InsertRange(licenseRequestDocumentList);
                }

                switch (_licenseRequest.LicenseRequestType)
                {
                    case "BNK":

                        foreach (Bunkering bunkering in BunkeringList)
                        {
                            bunkering.LicenseRequestID = _licenseRequest.LicenseRequestID;
                            bunkering.RecordStatus = _licenseRequest.RecordStatus;
                            bunkering.CreatedBy = _licenseRequest.CreatedBy;
                            bunkering.CreatedDate = _licenseRequest.CreatedDate;
                            bunkering.ModifiedBy = _licenseRequest.CreatedBy;
                            bunkering.ModifiedDate = _licenseRequest.CreatedDate;
                        }
                        _unitOfWork.Repository<Bunkering>().InsertRange(BunkeringList);
                        _unitOfWork.SaveChanges();
                        break;

                    case "FIE":

                        foreach (FireEquipment fireEquipment in FireEquipmentList)
                        {
                            fireEquipment.LicenseRequestID = _licenseRequest.LicenseRequestID;
                            fireEquipment.RecordStatus = _licenseRequest.RecordStatus;
                            fireEquipment.CreatedBy = _licenseRequest.CreatedBy;
                            fireEquipment.CreatedDate = _licenseRequest.CreatedDate;
                            fireEquipment.ModifiedBy = _licenseRequest.CreatedBy;
                            fireEquipment.ModifiedDate = _licenseRequest.CreatedDate;
                        }
                        _unitOfWork.Repository<FireEquipment>().InsertRange(FireEquipmentList);
                        _unitOfWork.SaveChanges();
                        break;

                    case "FIP":

                        foreach (FireProtection fireProtection in FireProtectionList)
                        {
                            fireProtection.LicenseRequestID = _licenseRequest.LicenseRequestID;
                            fireProtection.RecordStatus = _licenseRequest.RecordStatus;
                            fireProtection.CreatedBy = _licenseRequest.CreatedBy;
                            fireProtection.CreatedDate = _licenseRequest.CreatedDate;
                            fireProtection.ModifiedBy = _licenseRequest.CreatedBy;
                            fireProtection.ModifiedDate = _licenseRequest.CreatedDate;
                        }
                        _unitOfWork.Repository<FireProtection>().InsertRange(FireProtectionList);
                        _unitOfWork.SaveChanges();
                        break;

                    case "PST":

                        foreach (PestControl pestControl in PestControlList)
                        {
                            pestControl.LicenseRequestID = _licenseRequest.LicenseRequestID;
                            pestControl.RecordStatus = _licenseRequest.RecordStatus;
                            pestControl.CreatedBy = _licenseRequest.CreatedBy;
                            pestControl.CreatedDate = _licenseRequest.CreatedDate;
                            pestControl.ModifiedBy = _licenseRequest.CreatedBy;
                            pestControl.ModifiedDate = _licenseRequest.CreatedDate;
                        }
                        _unitOfWork.Repository<PestControl>().InsertRange(PestControlList);
                        _unitOfWork.SaveChanges();
                        break;

                    case "FLC":

                        foreach (FloatingCrane floatingCrane in FloatingCraneList)
                        {
                            floatingCrane.LicenseRequestID = _licenseRequest.LicenseRequestID;
                            floatingCrane.RecordStatus = _licenseRequest.RecordStatus;
                            floatingCrane.CreatedBy = _licenseRequest.CreatedBy;
                            floatingCrane.CreatedDate = _licenseRequest.CreatedDate;
                            floatingCrane.ModifiedBy = _licenseRequest.CreatedBy;
                            floatingCrane.ModifiedDate = _licenseRequest.CreatedDate;
                        }
                        _unitOfWork.Repository<FloatingCrane>().InsertRange(FloatingCraneList);
                        _unitOfWork.SaveChanges();
                        break;

                    case "STD":

                        foreach (Stevedore stevedore in StevedoreList)
                        {
                            stevedore.LicenseRequestID = _licenseRequest.LicenseRequestID;
                            stevedore.RecordStatus = _licenseRequest.RecordStatus;
                            stevedore.CreatedBy = _licenseRequest.CreatedBy;
                            stevedore.CreatedDate = _licenseRequest.CreatedDate;
                            stevedore.ModifiedBy = _licenseRequest.CreatedBy;
                            stevedore.ModifiedDate = _licenseRequest.CreatedDate;
                        }
                        _unitOfWork.Repository<Stevedore>().InsertRange(StevedoreList);
                        _unitOfWork.SaveChanges();
                        break;

                    case "POC":

                        foreach (PollutionControl pollutionControl in PollutionControlList)
                        {
                            pollutionControl.LicenseRequestID = _licenseRequest.LicenseRequestID;
                            pollutionControl.RecordStatus = _licenseRequest.RecordStatus;
                            pollutionControl.CreatedBy = _licenseRequest.CreatedBy;
                            pollutionControl.CreatedDate = _licenseRequest.CreatedDate;
                            pollutionControl.ModifiedBy = _licenseRequest.CreatedBy;
                            pollutionControl.ModifiedDate = _licenseRequest.CreatedDate;
                        }
                        _unitOfWork.Repository<PollutionControl>().InsertRange(PollutionControlList);
                        _unitOfWork.SaveChanges();
                        break;

                    case "DIV":

                        foreach (Diving diving in DivingList)
                        {
                            diving.LicenseRequestID = _licenseRequest.LicenseRequestID;
                            diving.RecordStatus = _licenseRequest.RecordStatus;
                            diving.CreatedBy = _licenseRequest.CreatedBy;
                            diving.CreatedDate = _licenseRequest.CreatedDate;
                            diving.ModifiedBy = _licenseRequest.CreatedBy;
                            diving.ModifiedDate = _licenseRequest.CreatedDate;
                        }
                        _unitOfWork.Repository<Diving>().InsertRange(DivingList);
                        _unitOfWork.SaveChanges();
                        break;

                }
                _licenseRequest.LicenseRequestType = _licenseRequest.SubCategory.SubCatName;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        public void UpdateStatus()
        {
            
            } 



        }



    }

