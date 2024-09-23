using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services;
using IPMS.Services.Business;
using IPMS.Services.WorkFlow;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                 ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PortEntryPassApplicationService : ServiceBase, IPortEntryPassApplicationService
    {
        private INotificationPublisher _notificationpublisher;
        //private INotificationPublishers _notificationpublishers;
        private IEntityRepository _entity;
        // private IPortConfigurationRepository _portConfigurationRepository;
        private ISubCategoryRepository _subcategoryRepository;
        private IPortGeneralConfigsRepository _portConfigurationRepository;
        private IPortRepository _portRepository;
        private IPortEntryPassApplicationRepository _portEntryPassApplicationRepository;
        //  private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.PortEntryPassApplication;
        ILog log = log4net.LogManager.GetLogger(typeof(PortEntryPassApplicationService));


        public PortEntryPassApplicationService()
        {

            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            //_notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _portConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            //  _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _portEntryPassApplicationRepository = new PortEntryPassApplicationRepository(_unitOfWork);
            //  _accountRepository = new AccountRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);

        }
        public PortEntryPassApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            //_portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _portEntryPassApplicationRepository = new PortEntryPassApplicationRepository(_unitOfWork);
            //_accountRepository = new AccountRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);



        }
        public PortEntryPassApplicationReferenceVO GetPortEntryPassReferenceData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                PortEntryPassApplicationReferenceVO VO = new PortEntryPassApplicationReferenceVO();
                VO.Ports = _portRepository.GetPortzs();
                VO.ApplicationCategory = _subcategoryRepository.Getapplicationcategory();
                VO.PermitTypes = _subcategoryRepository.GetPermittypes();
                VO.AreaOfConstruction = _subcategoryRepository.GetAreaOfConstruction();
                VO.TemporaryPermits = _subcategoryRepository.GetTemporaryPermits();
                VO.AdhocPermits = _subcategoryRepository.GetAdhocPermits();
                VO.OperatedTerminals = _subcategoryRepository.GetOperatedTerminals();
                VO.PermitDocumentTypes = _subcategoryRepository.GetPermitDocumentTypes();
                VO.SecurityDocumentTypes = _subcategoryRepository.GetSecurityDocumentTypes();
                VO.PermitRequeirements = _subcategoryRepository.GetPermitRequeirements();
                VO.AccessGates = _subcategoryRepository.GetAccessGates();
                VO.PermitRequeirementstypes = _subcategoryRepository.GetPermitRequeirementstypes();
                VO.PermitRequeirementsDuration = _subcategoryRepository.GetPermitRequeirementsDuration();
                VO.PermitCodes = _subcategoryRepository.GetPermitCodes();
                VO.PermitStatus = _subcategoryRepository.GetPermitStatus();
                VO.IndividualTemporaryPermits = _subcategoryRepository.GetTemporaryPermitsForIndividual();
                VO.AccessAreasForCTs = _subcategoryRepository.GetAccessAreasForCT();
                VO.AccessAreasForDBs = _subcategoryRepository.GetAccessAreasForDB();
                VO.AccessAreasForELs = _subcategoryRepository.GetAccessAreasForEL();
                VO.AccessAreasForNGs = _subcategoryRepository.GetAccessAreasForNG();
                VO.AccessAreasForMBs = _subcategoryRepository.GetAccessAreasForMB();
                VO.AccessAreasForSBs = _subcategoryRepository.GetAccessAreasForSB();
                VO.AccessAreasForPEs = _subcategoryRepository.GetAccessAreasForPE();
                VO.AccessAreasForRBs = _subcategoryRepository.GetAccessAreasForRB();
                VO.ContractorTemporaryPermits = _subcategoryRepository.GetContractorTemporaryPermits();
                VO.ContractorPermanentPermits = _subcategoryRepository.GetContractorPermanentPermits();
                VO.IndividualPermanentPermits = _subcategoryRepository.GetPermanentPermitsForIndividual();
                VO.ReasonForPermits = _subcategoryRepository.GetReasonsForPermit();
                VO.ContractorReasonForPermits = _subcategoryRepository.GetContractorReasonForPermit();
                VO.Country = _subcategoryRepository.VesselNationality().MapToDto();
                return VO;
            });
        }
        public List<PermitRequestVO> GetPortEntryPassRequestlist()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetPortEntryPassRequestlist(portcode).MapToDTO();
            });
        }
        public List<PermitRequestVO> GetPortEntryPassRequestlistForSsa()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetPortEntryPassRequestlistForSsa(portcode).MapToDTO();
            });
        }
        public List<PermitRequestVO> GetPortEntryPassRequestlistForSaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetPortEntryPassRequestlistForSaps(portcode).MapToDTO();
            });
        }
        public PermitRequestVO AddPortEntryPass(PermitRequestVO permitrequest)
        {
            log.Debug("Add Sevice Start RAMANA");
            return EncloseTransactionAndHandleException(() =>
            {
                log.Debug("Add Sevice Start RAMANA 1.1");
                var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);
                log.Debug("Add Sevice Start RAMANA 1.2");
                PermitRequest permitrequestentity = new PermitRequest();
                NotificationTemplate notification = new NotificationTemplate();
                permitrequest.ModifiedBy = (anonymousUserId == 0) ? 1 : anonymousUserId; // 1; 
                permitrequest.CreatedBy = (anonymousUserId == 0) ? 1 : anonymousUserId; // 1;
                //Entity entity = GetEntities(_entityCode);
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserTypeId = 0;
                nextStepCompany.UserType = "EMP";
                permitrequestentity.RecordStatus = "A";

                log.Debug("Add Sevice Start RAMANA 1.3");
                PermitRequestContractor PermitRequestContractors = null;
                List<PermitRequestDocument> PermitRequestDocuments = null;
                VehiclePermit VehiclePermits = null;
                VisitorPermit VisitorPermits = null;
                WharfVehiclePermit WharfVehiclePermits = null;
                PersonalPermit PersonalPermits = null;
                IndividualPermitApplicationDetails individualPermitApplicationDetails = null;
                List<string> selectedPermitRequirementCodes = null;
                List<string> selectedAccessGates = null;

                VehiclePermitRequirementCode VehiclePermitRequirementCode = new VehiclePermitRequirementCode();
                PermitRequestAccessGates PermitRequestAccessGates = new PermitRequestAccessGates();
                //IndividualVehiclePermit IndividualVehiclePermits = new IndividualVehiclePermit();
                IndividualPersonalPermit IndividualPersonalPermits = new IndividualPersonalPermit();
                ContractorPermitApplicationDetails ContractorPermitApplicationDetails = new ContractorPermitApplicationDetails();
                List<ContractorPermitEmployeeDetails> ContractorPermitEmployeeDetails = null;
                List<IndividualVehiclePermit> IndividualVehiclePermits = null;
                permitrequest.CreatedDate = DateTime.Now;
                permitrequest.ModifiedDate = DateTime.Now;
                permitrequestentity = permitrequest.MapToEntity();

                PermitRequestContractors = permitrequest.PermitRequestContractors.MapToEntity();
                VehiclePermits = permitrequest.VehiclePermits.MapToEntity();
                PersonalPermits = permitrequest.PersonalPermits.MapToEntity();
                VisitorPermits = permitrequest.VisitorPermits.MapToEntity();
                PermitRequestDocuments = permitrequest.PermitRequestDocuments.MapToEntity();
                WharfVehiclePermits = permitrequest.WharfVehiclePermits.MapToEntity();
                individualPermitApplicationDetails = permitrequest.IndividualPermitApplicationDetails.MapToEntity();
                // List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
                //List<PermitRequestSubArea> applPermitRequestSubAreas = permitrequestentity.PermitRequestSubAreas.ToList();
                selectedPermitRequirementCodes = permitrequest.selectedPermitRequirementCode;
                selectedAccessGates = permitrequest.selectedAccessGates;
                //  IndividualVehiclePermits = permitrequest.IndividualVehiclePermits.MapToEntity();
                IndividualPersonalPermits = permitrequest.IndividualPersonalPermits.MapToEntity();
                List<PermitReason> applPermitReasons = permitrequestentity.PermitReasons.ToList();
                List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
                List<PermitRequestSubArea> applPermitRequestSubAreas = permitrequestentity.PermitRequestSubAreas.ToList();
                ContractorPermitEmployeeDetails = permitrequest.ContractorPermitEmployeeDetails.MapToEntity();
                ContractorPermitApplicationDetails = permitrequest.ContractorPermitApplicationDetails.MapToEntity();
                IndividualVehiclePermits = permitrequest.IndividualVehiclePermits.MapToEntity();

                #region Port Entry Pass Saving

                permitrequestentity.RecordStatus = "A";
                log.Debug("Add Sevice Start RAMANA 1.4");
                permitrequestentity.PersonalPermits = null;
                permitrequestentity.PermitRequestDocuments = null;
                permitrequestentity.PermitRequestContractors = null;
                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.VisitorPermits = null;
                permitrequestentity.VehiclePermits = null;
                permitrequestentity.WharfVehiclePermits = null;
                permitrequestentity.IndividualPermitApplicationDetails = null;
                permitrequestentity.PermitReasons = null;

                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.PermitRequestSubAreas = null;

                CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                string port = permitrequestentity.PortCode;
                permitrequestentity.ReferenceNo = codeGenerator.GenerateLicenseefno("PEP", port);
                permitrequestentity.permitStatus = "PERN";
                //permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                log.Debug("Saving Prt Entry Req start");
                permitrequestentity.ObjectState = ObjectState.Added;
                log.Debug("Saving Prt Entry Req insert start");
                log.Debug("Add Sevice Start RAMANA 1.5");
                
                _unitOfWork.Repository<PermitRequest>().Insert(permitrequestentity);
                log.Debug("Saving Prt Entry Req insert end");
                log.Debug("anonymousUserId" + anonymousUserId);
                log.Debug("createdby" + permitrequestentity.CreatedBy);
                try
                {
                    _unitOfWork.SaveChanges();
                }
                catch (Exception  e)
                {

                    log.Debug("RAMANA EXCEPTION " + e.InnerException );
                    log.Debug("RAMANA EXCEPTION 1 " + e.Message);
                    log.Debug("RAMANA EXCEPTION 2" + e.StackTrace);
                    throw;
                }
                
                
                log.Debug("Saving Prt Entry Req End");
                
                log.Debug("RAMANA 2 ");
                //------------------------------------A
                if (permitrequestentity.PermitRequestTypeCode == "APCA")
                {
                    if (string.IsNullOrEmpty(PersonalPermits.PermitCategoryCode))
                    {
                        PersonalPermits.PermitCategoryCode = null;
                    }
                    if (string.IsNullOrEmpty(PersonalPermits.AdhocPermits))
                    {
                        PersonalPermits.AdhocPermits = null;
                    }
                    if (string.IsNullOrEmpty(PersonalPermits.TemporaryPermits))
                    {
                        PersonalPermits.TemporaryPermits = null;
                    }
                    if (string.IsNullOrEmpty(PersonalPermits.ConstructionArea))
                    {
                        PersonalPermits.ConstructionArea = null;
                    }
                    PersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                    PersonalPermits.CreatedBy = permitrequestentity.CreatedBy;
                    PersonalPermits.CreatedDate = permitrequestentity.CreatedDate;
                    PersonalPermits.ModifiedBy = permitrequestentity.CreatedBy;
                    PersonalPermits.ModifiedDate = permitrequestentity.ModifiedDate;
                    PersonalPermits.RecordStatus = permitrequestentity.RecordStatus;
                    PersonalPermits.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<PersonalPermit>().Insert(PersonalPermits);
                    _unitOfWork.SaveChanges();

                    if (applPermitRequestAreas.Count > 0)
                    {
                        foreach (var applPermitRequestArea in applPermitRequestAreas)
                        {
                            applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                        }
                        _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                        _unitOfWork.SaveChanges();
                    }

                    if (PermitRequestDocuments.Count > 0)
                    {
                        foreach (var PermitRequestDocument in PermitRequestDocuments)
                        {
                            PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                            PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                            PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                            PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                            PermitRequestDocument.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                        _unitOfWork.SaveChanges();
                    }
                }
                log.Debug("RAMANA 3 ");
                //PermitRequestContractors Saving------------------------------------------------------------------------------------
                if (permitrequestentity.PermitRequestTypeCode == "APCB")
                {
                    //permitrequestentity.RecordStatus = "A";
                    //permitrequestentity.PersonalPermits = null;
                    //permitrequestentity.PermitRequestDocuments = null;
                    //permitrequestentity.PermitRequestContractors = null;
                    //permitrequestentity.PermitRequestAreas = null;
                    //permitrequestentity.VisitorPermits = null;
                    //permitrequestentity.VehiclePermits = null;
                    //permitrequestentity.WharfVehiclePermits = null;
                    ////permitrequestentity.PortCode = "CT";
                    //permitrequestentity.ObjectState = ObjectState.Added;
                    //_unitOfWork.Repository<PermitRequest>().Insert(permitrequestentity);
                    //_unitOfWork.SaveChanges();

                    //PermitRequestContractors.Section625ABCDID = section625abcd.Section625ABCDID;
                    PermitRequestContractors.PermitRequestID = permitrequestentity.PermitRequestID;
                    PermitRequestContractors.CreatedDate = DateTime.Now;
                    PermitRequestContractors.ModifiedDate = DateTime.Now;
                    PermitRequestContractors.ModifiedBy = anonymousUserId; // 1;
                    PermitRequestContractors.CreatedBy = anonymousUserId; // 1;
                    PermitRequestContractors.RecordStatus = "A";
                    PermitRequestContractors.ContactNo = PermitRequestContractors.ContactNo.Replace("(", "").Replace(")", "").Replace("-", "");
                    PermitRequestContractors.MobileNo = PermitRequestContractors.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                    PermitRequestContractors.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<PermitRequestContractor>().Insert(PermitRequestContractors);
                    _unitOfWork.SaveChanges();

                    if (string.IsNullOrEmpty(PersonalPermits.PermitCategoryCode))
                    {
                        PersonalPermits.PermitCategoryCode = null;
                    }
                    if (string.IsNullOrEmpty(PersonalPermits.AdhocPermits))
                    {
                        PersonalPermits.AdhocPermits = null;
                    }
                    if (string.IsNullOrEmpty(PersonalPermits.TemporaryPermits))
                    {
                        PersonalPermits.TemporaryPermits = null;
                    }
                    if (string.IsNullOrEmpty(PersonalPermits.ConstructionArea))
                    {
                        PersonalPermits.ConstructionArea = null;
                    }

                    PersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                    PersonalPermits.CreatedBy = permitrequestentity.CreatedBy;
                    PersonalPermits.CreatedDate = permitrequestentity.CreatedDate;
                    PersonalPermits.ModifiedBy = permitrequestentity.CreatedBy;
                    PersonalPermits.ModifiedDate = permitrequestentity.ModifiedDate;
                    PersonalPermits.RecordStatus = "A";
                    PersonalPermits.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<PersonalPermit>().Insert(PersonalPermits);
                    _unitOfWork.SaveChanges();

                    if (PermitRequestDocuments.Count > 0)
                    {
                        foreach (var PermitRequestDocument in PermitRequestDocuments)
                        {

                            PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                            PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                            PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                            PermitRequestDocument.RecordStatus = "A";
                            PermitRequestDocument.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                        _unitOfWork.SaveChanges();
                    }
                    if (applPermitRequestAreas.Count > 0)
                    {
                        foreach (var applPermitRequestArea in applPermitRequestAreas)
                        {
                            applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                        }
                        _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                        _unitOfWork.SaveChanges();
                    }
                }
                log.Debug("RAMANA 4 ");
                //VehiclePermits Saving------------------------------------------------------------------------------------
                if (permitrequestentity.PermitRequestTypeCode == "APCC")
                {
                    VehiclePermits.PermitRequestID = permitrequestentity.PermitRequestID;
                    VehiclePermits.CreatedDate = DateTime.Now;
                    VehiclePermits.ModifiedDate = DateTime.Now;
                    VehiclePermits.ModifiedBy = anonymousUserId; // 1;
                    VehiclePermits.CreatedBy = anonymousUserId; // 1;
                    VehiclePermits.RecordStatus = "A";
                    VehiclePermits.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<VehiclePermit>().Insert(VehiclePermits);
                    _unitOfWork.SaveChanges();
                    if (selectedPermitRequirementCodes.Count > 0)
                    {
                        foreach (var selectedPermitRequirementCode in selectedPermitRequirementCodes)
                        {
                            VehiclePermitRequirementCode.VehiclePermitID = VehiclePermits.VehiclePermitID;
                            VehiclePermitRequirementCode.PermitRequirementCode = selectedPermitRequirementCode;
                            VehiclePermitRequirementCode.PermitRequestID = permitrequestentity.PermitRequestID;
                            VehiclePermitRequirementCode.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<VehiclePermitRequirementCode>().Insert(VehiclePermitRequirementCode);
                            _unitOfWork.SaveChanges();

                        }
                    }
                    log.Debug("RAMANA 5 ");
                    if (PermitRequestDocuments.Count > 0)
                    {
                        foreach (var PermitRequestDocument in PermitRequestDocuments)
                        {

                            PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                            PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                            PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                            PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                            PermitRequestDocument.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                        _unitOfWork.SaveChanges();
                    }
                }
                log.Debug("RAMANA 6 ");
                //WharfVehiclePermits Saving------------------------------------------------------------------------------------
                if (permitrequestentity.PermitRequestTypeCode == "APCD")
                {

                    WharfVehiclePermits.PermitRequestID = permitrequestentity.PermitRequestID;
                    WharfVehiclePermits.CreatedDate = DateTime.Now;
                    WharfVehiclePermits.ModifiedDate = DateTime.Now;
                    if (string.IsNullOrEmpty(WharfVehiclePermits.TemporaryPermits))
                    {
                        WharfVehiclePermits.TemporaryPermits = null;
                    }
                    WharfVehiclePermits.TelephoneNo = WharfVehiclePermits.TelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                    WharfVehiclePermits.Hometelephone = WharfVehiclePermits.Hometelephone.Replace("(", "").Replace(")", "").Replace("-", "");
                    WharfVehiclePermits.ModifiedBy = anonymousUserId; // 1;
                    WharfVehiclePermits.CreatedBy = anonymousUserId; // 1;
                    WharfVehiclePermits.RecordStatus = "A";
                    WharfVehiclePermits.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<WharfVehiclePermit>().Insert(WharfVehiclePermits);
                    _unitOfWork.SaveChanges();



                    if (selectedAccessGates.Count > 0)
                    {
                        foreach (var selectedAccessGate in selectedAccessGates)
                        {
                            PermitRequestAccessGates.WharfVehiclePermitID = WharfVehiclePermits.WharfVehiclePermitID;
                            PermitRequestAccessGates.PermitRequestID = permitrequestentity.PermitRequestID;
                            PermitRequestAccessGates.AccessGates = selectedAccessGate;
                            PermitRequestAccessGates.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<PermitRequestAccessGates>().Insert(PermitRequestAccessGates);
                            _unitOfWork.SaveChanges();

                        }
                    }
                    if (PermitRequestDocuments.Count > 0)
                    {
                        foreach (var PermitRequestDocument in PermitRequestDocuments)
                        {
                            PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                            PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                            PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                            PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                            PermitRequestDocument.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                        _unitOfWork.SaveChanges();
                    }
                }
                //Individual Application 
                log.Debug("RAMANA 7 ");
                if (permitrequestentity.PermitRequestTypeCode == "APCF")
                {
                    log.Debug("Entered Individual App");
                    individualPermitApplicationDetails.PermitRequestID = permitrequestentity.PermitRequestID;
                    log.Debug("IndividualPermitApplicationDetails entry start");
                    _unitOfWork.Repository<IndividualPermitApplicationDetails>().Insert(individualPermitApplicationDetails);
                    _unitOfWork.SaveChanges();
                    log.Debug("Save to indi permit appli details success");
                    //IndividualVehiclePermits.PermitRequestID = permitrequestentity.PermitRequestID;

                    //_unitOfWork.Repository<IndividualVehiclePermit>().Insert(IndividualVehiclePermits);
                    //_unitOfWork.SaveChanges();
                    if (IndividualVehiclePermits.Count > 0)
                    {
                        foreach (var IndividualVehiclePermit in IndividualVehiclePermits)
                        {
                            IndividualVehiclePermit.PermitRequestID = permitrequestentity.PermitRequestID;
                            // IndividualVehiclePermit.RecordStatus = "A";
                            IndividualVehiclePermit.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<IndividualVehiclePermit>().InsertRange(IndividualVehiclePermits);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to indi vehicle details success");




                    if (IndividualPersonalPermits.permittype == IndividualApplication.IndividualTemporary || IndividualPersonalPermits.permittype == IndividualApplication.IndividualPermanent)
                    {
                        IndividualPersonalPermits.ContractorPermanentPermits = null;
                        IndividualPersonalPermits.ContractorTemporaryPermits = null;
                        if (string.IsNullOrEmpty(IndividualPersonalPermits.IndividualPermanentPermits))
                        {
                            IndividualPersonalPermits.IndividualPermanentPermits = null;
                        }
                        if (string.IsNullOrEmpty(IndividualPersonalPermits.IndividualTemporaryPermits))
                        {
                            IndividualPersonalPermits.IndividualTemporaryPermits = null;
                        }
                        if (IndividualPersonalPermits.permittype == IndividualApplication.IndividualTemporary)
                        {
                            IndividualPersonalPermits.PerFromDate = null;
                            IndividualPersonalPermits.PerToDate = null;
                        }
                        else
                        {
                            IndividualPersonalPermits.TempFromDate = null;
                            IndividualPersonalPermits.TempToDate = null;
                        }
                    }

                    IndividualPersonalPermits.AuthorisedMobile = IndividualPersonalPermits.AuthorisedMobile.Replace("(", "").Replace(")", "").Replace("-", "");
                    IndividualPersonalPermits.TelephoneWork = IndividualPersonalPermits.TelephoneWork.Replace("(", "").Replace(")", "").Replace("-", "");
                    IndividualPersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                    IndividualPersonalPermits.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<IndividualPersonalPermit>().Insert(IndividualPersonalPermits);
                    _unitOfWork.SaveChanges();
                    log.Debug("Save to indi permit  details success");

                    if (applPermitReasons.Count > 0)
                    {
                        foreach (var applPermitReason in applPermitReasons)
                        {
                            applPermitReason.PermitRequestID = permitrequestentity.PermitRequestID;
                        }
                        _unitOfWork.Repository<PermitReason>().InsertRange(applPermitReasons);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  permit reason success");
                    if (applPermitRequestAreas.Count > 0)
                    {
                        foreach (var applPermitRequestArea in applPermitRequestAreas)
                        {
                            applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                        }
                        _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  permit areas success");

                    if (applPermitRequestSubAreas.Count > 0)
                    {
                        foreach (var applPermitRequestSubArea in applPermitRequestSubAreas)
                        {
                            applPermitRequestSubArea.PermitRequestID = permitrequestentity.PermitRequestID;
                            //applPermitRequestSubArea.PermitRequestAreaCode=applPermitRequestSubAreas.PermitRequestSubAreaCode.
                            // applPermitRequestSubArea.PermitRequestSubAreaCode=applPermitRequestSubAreas.PermitRequestSubAreaCode.Split('-')[0];
                            //  applPermitRequestSubArea.PermitRequestAreaCode=applPermitRequestSubAreas.PermitRequestAreaCode.Split('-')[1];


                        }
                        _unitOfWork.Repository<PermitRequestSubArea>().InsertRange(applPermitRequestSubAreas);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  permit sub areas success");

                    if (PermitRequestDocuments.Count > 0)
                    {
                        foreach (var PermitRequestDocument in PermitRequestDocuments)
                        {
                            PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                            PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                            PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                            PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                            PermitRequestDocument.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  permit doc success");
                }
                log.Debug("RAMANA 8 ");
                //contractor Application
                if (permitrequestentity.PermitRequestTypeCode == "APCH")
                {
                    log.Debug("enter to contract application");
                    ContractorPermitApplicationDetails.PermitRequestID = permitrequestentity.PermitRequestID;
                    ContractorPermitApplicationDetails.TelephoneNumber = ContractorPermitApplicationDetails.TelephoneNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                    ContractorPermitApplicationDetails.SubContractorTelephoneNumber = ContractorPermitApplicationDetails.SubContractorTelephoneNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                    ContractorPermitApplicationDetails.RecordStatus = "A";
                    ContractorPermitApplicationDetails.CreatedBy = permitrequestentity.CreatedBy;
                    ContractorPermitApplicationDetails.CreatedDate = DateTime.Now;
                    ContractorPermitApplicationDetails.ModifiedBy = permitrequestentity.ModifiedBy;
                    ContractorPermitApplicationDetails.ModifiedDate = DateTime.Now;
                    ContractorPermitApplicationDetails.ObjectState = ObjectState.Added;
                    log.Debug("ContractorPermitApplicationDetails Save to  started");
                    _unitOfWork.Repository<ContractorPermitApplicationDetails>().Insert(ContractorPermitApplicationDetails);
                    _unitOfWork.SaveChanges();
                    log.Debug("Save to  ContractorPermitApplicationDetails success");
                    if (ContractorPermitEmployeeDetails.Count > 0)
                    {
                        foreach (var ContractorPermitEmployeeDetail in ContractorPermitEmployeeDetails)
                        {
                            ContractorPermitEmployeeDetail.PermitRequestID = permitrequestentity.PermitRequestID;
                            ContractorPermitEmployeeDetail.RecordStatus = "A";
                            ContractorPermitEmployeeDetail.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<ContractorPermitEmployeeDetails>().InsertRange(ContractorPermitEmployeeDetails);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  ContractorPermitEmployeeDetails success");

                    if (IndividualPersonalPermits.permittype == ContractorApplication.ContractorTemporary || IndividualPersonalPermits.permittype == ContractorApplication.ContractorPermanent)
                    {
                        IndividualPersonalPermits.IndividualPermanentPermits = null;
                        IndividualPersonalPermits.IndividualTemporaryPermits = null;
                        if (string.IsNullOrEmpty(IndividualPersonalPermits.ContractorPermanentPermits))
                        {
                            IndividualPersonalPermits.ContractorPermanentPermits = null;
                        }
                        if (string.IsNullOrEmpty(IndividualPersonalPermits.ContractorTemporaryPermits))
                        {
                            IndividualPersonalPermits.ContractorTemporaryPermits = null;
                        }

                        if (IndividualPersonalPermits.permittype == ContractorApplication.ContractorTemporary)
                        {
                            IndividualPersonalPermits.ContractorPerFromDate = null;
                            IndividualPersonalPermits.ContractorPerToDate = null;
                        }
                        else
                        {
                            IndividualPersonalPermits.ContractorTempFromDate = null;
                            IndividualPersonalPermits.ContractorTempFromDate = null;
                        }
                    }


                    IndividualPersonalPermits.AuthorisedMobile = IndividualPersonalPermits.AuthorisedMobile.Replace("(", "").Replace(")", "").Replace("-", "");
                    IndividualPersonalPermits.TelephoneWork = IndividualPersonalPermits.TelephoneWork.Replace("(", "").Replace(")", "").Replace("-", "");
                    IndividualPersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                    IndividualPersonalPermits.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<IndividualPersonalPermit>().Insert(IndividualPersonalPermits);
                    _unitOfWork.SaveChanges();
                    log.Debug("Save to  IndividualPersonalPermit success");
                    if (applPermitReasons.Count > 0)
                    {
                        foreach (var applPermitReason in applPermitReasons)
                        {
                            applPermitReason.PermitRequestID = permitrequestentity.PermitRequestID;
                        }
                        _unitOfWork.Repository<PermitReason>().InsertRange(applPermitReasons);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  Permit Reason success");
                    if (applPermitRequestAreas.Count > 0)
                    {
                        foreach (var applPermitRequestArea in applPermitRequestAreas)
                        {
                            applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                        }
                        _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  Permit Areas success");
                    if (applPermitRequestSubAreas.Count > 0)
                    {
                        foreach (var applPermitRequestSubArea in applPermitRequestSubAreas)
                        {
                            applPermitRequestSubArea.PermitRequestID = permitrequestentity.PermitRequestID;

                        }
                        _unitOfWork.Repository<PermitRequestSubArea>().InsertRange(applPermitRequestSubAreas);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  Permit sub areas success");

                    if (PermitRequestDocuments.Count > 0)
                    {
                        foreach (var PermitRequestDocument in PermitRequestDocuments)
                        {
                            PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                            PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                            PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                            PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                            PermitRequestDocument.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                        _unitOfWork.SaveChanges();
                    }
                    log.Debug("Save to  Permit doc success");
                }
                log.Debug("RAMANA 9 ");
                //VisitorPermits Saving------------------------------------------------------------------------------------
                if (permitrequestentity.PermitRequestTypeCode == "APCE")
                {
                    VisitorPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                    VisitorPermits.CreatedDate = DateTime.Now;
                    VisitorPermits.ModifiedDate = DateTime.Now;
                    VisitorPermits.ModifiedBy = anonymousUserId; // 1;
                    VisitorPermits.CreatedBy = anonymousUserId; // 1;
                    VisitorPermits.RecordStatus = "A";
                    VisitorPermits.TelephoneNo = VisitorPermits.TelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                    VisitorPermits.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<VisitorPermit>().Insert(VisitorPermits);
                    _unitOfWork.SaveChanges();
                    if (PermitRequestDocuments.Count > 0)
                    {
                        foreach (var PermitRequestDocument in PermitRequestDocuments)
                        {

                            PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                            PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                            PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                            PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                            PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                            PermitRequestDocument.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                        _unitOfWork.SaveChanges();
                    }

                }


                //#region Workflow Integration
                string remarks = "New PortEntryPass";
                log.Debug("RAMANA 10 ");
                #region PortEntryPass Notification Workflow
                PortEntryPassWorkFlow portentrypassworkflow = new PortEntryPassWorkFlow(_unitOfWork, permitrequestentity, remarks);
                WorkFlowEngine<PortEntryPassWorkFlow> wf = new WorkFlowEngine<PortEntryPassWorkFlow>(_unitOfWork, _PortCode, permitrequestentity.CreatedBy);
                wf.Process(portentrypassworkflow, _portConfigurationRepository.GetPortConfiguration(permitrequestentity.PortCode, ConfigName.WorkflowInitialStatus));

                #endregion
                #endregion
                // string remarks = "New Port Entry Pass Request";
                log.Debug("RAMANA 11 ");
                #region Port Entry Pass Notifications


                //_notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), 1, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassNew);
                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), anonymousUserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassNew);
                // _notificationpublisher.ManualPublish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), anonymousUserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassManual, WFStatus.NotificationTemplateBase, permitrequest.PermitRequestID);


                #endregion

                log.Debug("RAMANA 12 ");
                permitrequest = permitrequestentity.MapToDTO();
                _notificationpublisher.ManualPublish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), anonymousUserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassManual, WFStatus.NotificationTemplateBase, permitrequest.PermitRequestID,individualPermitApplicationDetails.EmailAddress);
                log.Debug("RAMANA 13 ");
                return permitrequest;
            });
        }
        public PermitRequestVO EditPortEntryPass(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
           {
               var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

               PermitRequest permitrequestentity = new PermitRequest();
               permitrequest.ModifiedBy = (anonymousUserId == 0) ? 1 : anonymousUserId;
               permitrequest.CreatedBy = (anonymousUserId == 0) ? 1 : anonymousUserId;
               //permitrequestentity.RecordStatus = "A";
               CompanyVO nextStepCompany = new CompanyVO();
               nextStepCompany.UserTypeId = 0;
               nextStepCompany.UserType = "EMP";

               PermitRequestContractor PermitRequestContractors = null;
               List<PermitRequestDocument> PermitRequestDocuments = null;
               VehiclePermit VehiclePermits = null;
               VisitorPermit VisitorPermits = null;
               WharfVehiclePermit WharfVehiclePermits = null;
               PersonalPermit PersonalPermits = null;

               IndividualPermitApplicationDetails IndividualPermitApplicationDetails = null;
               List<string> selectedPermitRequirementCodes = null;
               List<string> selectedAccessGates = null;

               VehiclePermitRequirementCode VehiclePermitRequirementCode = new VehiclePermitRequirementCode();
               PermitRequestAccessGates PermitRequestAccessGates = new PermitRequestAccessGates();
               //IndividualVehiclePermit IndividualVehiclePermits = new IndividualVehiclePermit();
               IndividualPersonalPermit IndividualPersonalPermits = new IndividualPersonalPermit();
               ContractorPermitApplicationDetails ContractorPermitApplicationDetails = new ContractorPermitApplicationDetails();
               List<ContractorPermitEmployeeDetails> ContractorPermitEmployeeDetails = null;
               List<IndividualVehiclePermit> IndividualVehiclePermits = null;
               permitrequest.CreatedDate = DateTime.Now;
               permitrequest.ModifiedDate = DateTime.Now;


               permitrequest.CreatedDate = DateTime.Now;
               permitrequest.ModifiedDate = DateTime.Now;
               permitrequestentity = permitrequest.MapToEntity();

               PermitRequestContractors = permitrequest.PermitRequestContractors.MapToEntity();
               VehiclePermits = permitrequest.VehiclePermits.MapToEntity();
               PersonalPermits = permitrequest.PersonalPermits.MapToEntity();
               VisitorPermits = permitrequest.VisitorPermits.MapToEntity();
               PermitRequestDocuments = permitrequest.PermitRequestDocuments.MapToEntity();
               WharfVehiclePermits = permitrequest.WharfVehiclePermits.MapToEntity();
               List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
               PermitRequestContractors = permitrequest.PermitRequestContractors.MapToEntity();
               VehiclePermits = permitrequest.VehiclePermits.MapToEntity();
               PersonalPermits = permitrequest.PersonalPermits.MapToEntity();
               VisitorPermits = permitrequest.VisitorPermits.MapToEntity();
               PermitRequestDocuments = permitrequest.PermitRequestDocuments.MapToEntity();
               WharfVehiclePermits = permitrequest.WharfVehiclePermits.MapToEntity();
               IndividualPermitApplicationDetails = permitrequest.IndividualPermitApplicationDetails.MapToEntity();
               // List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
               //List<PermitRequestSubArea> applPermitRequestSubAreas = permitrequestentity.PermitRequestSubAreas.ToList();
               selectedPermitRequirementCodes = permitrequest.selectedPermitRequirementCode;
               selectedAccessGates = permitrequest.selectedAccessGates;
               //  IndividualVehiclePermits = permitrequest.IndividualVehiclePermits.MapToEntity();
               IndividualPersonalPermits = permitrequest.IndividualPersonalPermits.MapToEntity();
               List<PermitReason> applPermitReasons = permitrequestentity.PermitReasons.ToList();
               //List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
               List<PermitRequestSubArea> applPermitRequestSubAreas = permitrequestentity.PermitRequestSubAreas.ToList();
               ContractorPermitEmployeeDetails = permitrequest.ContractorPermitEmployeeDetails.MapToEntity();
               ContractorPermitApplicationDetails = permitrequest.ContractorPermitApplicationDetails.MapToEntity();
               IndividualVehiclePermits = permitrequest.IndividualVehiclePermits.MapToEntity();



               #region Port Entry Pass Editing

               permitrequestentity.RecordStatus = permitrequestentity.RecordStatus;

               permitrequestentity.PersonalPermits = null;
               permitrequestentity.PermitRequestDocuments = null;
               permitrequestentity.PermitRequestContractors = null;
               permitrequestentity.PermitRequestAreas = null;
               permitrequestentity.VisitorPermits = null;
               permitrequestentity.VehiclePermits = null;
               permitrequestentity.WharfVehiclePermits = null;

               permitrequestentity.IndividualPermitApplicationDetails = null;
               permitrequestentity.IndividualPersonalPermits = null;
               permitrequestentity.IndividualVehiclePermits = null;
               permitrequestentity.ContractorPermitApplicationDetails = null;
               permitrequestentity.ContractorPermitEmployeeDetails = null;
               permitrequestentity.PermitReasons = null;
               permitrequestentity.PermitRequestAreas = null;
               permitrequestentity.PermitRequestSubAreas = null;



               if (permitrequest.Flag == 1)
               { permitrequestentity.permitStatus = "PRSN"; }
               if (permitrequest.Flag == 2)
               { permitrequestentity.permitStatus = "PRRN"; }
               //permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
               permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
               permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
               permitrequestentity.ObjectState = ObjectState.Modified;
               _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
               _unitOfWork.SaveChanges();
               if (permitrequestentity.PermitRequestTypeCode == "APCA")
               {
                   if (string.IsNullOrEmpty(PersonalPermits.PermitCategoryCode))
                   {
                       PersonalPermits.PermitCategoryCode = null;
                   }
                   if (string.IsNullOrEmpty(PersonalPermits.AdhocPermits))
                   {
                       PersonalPermits.AdhocPermits = null;
                   }
                   if (string.IsNullOrEmpty(PersonalPermits.TemporaryPermits))
                   {
                       PersonalPermits.TemporaryPermits = null;
                   }
                   if (string.IsNullOrEmpty(PersonalPermits.ConstructionArea))
                   {
                       PersonalPermits.ConstructionArea = null;
                   }
                   PersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                   PersonalPermits.CreatedBy = permitrequestentity.CreatedBy;
                   PersonalPermits.CreatedDate = permitrequestentity.CreatedDate;
                   PersonalPermits.ModifiedBy = permitrequestentity.CreatedBy;
                   PersonalPermits.ModifiedDate = permitrequestentity.ModifiedDate;
                   PersonalPermits.RecordStatus = permitrequestentity.RecordStatus;
                   PersonalPermits.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<PersonalPermit>().Update(PersonalPermits);
                   _unitOfWork.SaveChanges();
                   //var DC2 = 
                   _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestArea where PermitRequestID = @p0", permitrequestentity.PermitRequestID);
                   if (applPermitRequestAreas.Count > 0)
                   {
                       foreach (var applPermitRequestArea in applPermitRequestAreas)
                       {
                           applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                       }
                       _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                       _unitOfWork.SaveChanges();
                   }
                   // List<PermitRequestDocument> PermitRequestDocumentslist = permitrequestentity.PermitRequestDocuments.ToList();
                   //var DC1 = 
                   _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestDocument where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (PermitRequestDocuments.Count > 0)
                   {
                       foreach (var PermitRequestDocument in PermitRequestDocuments)
                       {

                           PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                           PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                           PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                           PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                           PermitRequestDocument.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                       _unitOfWork.SaveChanges();
                   }
               }
               //PermitRequestContractors Saving------------------------------------------------------------------------------------
               if (permitrequestentity.PermitRequestTypeCode == "APCB")
               {
                   //permitrequestentity.RecordStatus = "A";
                   //permitrequestentity.PersonalPermits = null;
                   //permitrequestentity.PermitRequestDocuments = null;
                   //permitrequestentity.PermitRequestContractors = null;
                   //permitrequestentity.PermitRequestAreas = null;
                   //permitrequestentity.VisitorPermits = null;
                   //permitrequestentity.VehiclePermits = null;
                   //permitrequestentity.WharfVehiclePermits = null;
                   ////permitrequestentity.PortCode = "CT";
                   //permitrequestentity.ObjectState = ObjectState.Modified;
                   //_unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                   //_unitOfWork.SaveChanges();

                   //PermitRequestContractors.Section625ABCDID = section625abcd.Section625ABCDID;
                   PermitRequestContractors.PermitRequestID = permitrequestentity.PermitRequestID;
                   PermitRequestContractors.CreatedDate = DateTime.Now;
                   PermitRequestContractors.ModifiedDate = DateTime.Now;
                   PermitRequestContractors.ModifiedBy = anonymousUserId; // 1;
                   PermitRequestContractors.CreatedBy = anonymousUserId; // 1;
                   PermitRequestContractors.RecordStatus = "A";
                   PermitRequestContractors.ContactNo = PermitRequestContractors.ContactNo.Replace("(", "").Replace(")", "").Replace("-", "");
                   PermitRequestContractors.MobileNo = PermitRequestContractors.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                   PermitRequestContractors.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<PermitRequestContractor>().Update(PermitRequestContractors);
                   _unitOfWork.SaveChanges();
                   if (string.IsNullOrEmpty(PersonalPermits.PermitCategoryCode))
                   {
                       PersonalPermits.PermitCategoryCode = null;
                   }
                   if (string.IsNullOrEmpty(PersonalPermits.AdhocPermits))
                   {
                       PersonalPermits.AdhocPermits = null;
                   }
                   if (string.IsNullOrEmpty(PersonalPermits.TemporaryPermits))
                   {
                       PersonalPermits.TemporaryPermits = null;
                   }
                   if (string.IsNullOrEmpty(PersonalPermits.ConstructionArea))
                   {
                       PersonalPermits.ConstructionArea = null;
                   }
                   PersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                   PersonalPermits.CreatedBy = permitrequestentity.CreatedBy;
                   PersonalPermits.CreatedDate = permitrequestentity.CreatedDate;
                   PersonalPermits.ModifiedBy = permitrequestentity.CreatedBy;
                   PersonalPermits.ModifiedDate = permitrequestentity.ModifiedDate;
                   PersonalPermits.RecordStatus = "A";
                   PersonalPermits.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<PersonalPermit>().Update(PersonalPermits);
                   _unitOfWork.SaveChanges();

                   // List<PermitRequestDocument> PermitRequestDocumentslist = permitrequestentity.PermitRequestDocuments.ToList();
                   //var DC1 = 
                   _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestDocument where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (PermitRequestDocuments.Count > 0)
                   {
                       foreach (var PermitRequestDocument in PermitRequestDocuments)
                       {

                           PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                           PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                           PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                           PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                           PermitRequestDocument.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                       _unitOfWork.SaveChanges();
                   }
                   //var DC2 = 
                   _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestArea where PermitRequestID = @p0", permitrequestentity.PermitRequestID);
                   if (applPermitRequestAreas.Count > 0)
                   {
                       foreach (var applPermitRequestArea in applPermitRequestAreas)
                       {
                           applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                       }
                       _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                       _unitOfWork.SaveChanges();
                   }
               }

               //VehiclePermits Saving------------------------------------------------------------------------------------
               if (permitrequestentity.PermitRequestTypeCode == "APCC")
               {
                   //PermitRequestContractors.Section625ABCDID = section625abcd.Section625ABCDID;
                   //PermitRequestContractors.Hour24Report625ID = Hour24Report625.Hour24Report625ID;
                   VehiclePermits.PermitRequestID = permitrequestentity.PermitRequestID;
                   VehiclePermits.CreatedDate = DateTime.Now;
                   VehiclePermits.ModifiedDate = DateTime.Now;
                   VehiclePermits.ModifiedBy = anonymousUserId; // 1;
                   VehiclePermits.CreatedBy = anonymousUserId; // 1;
                   VehiclePermits.RecordStatus = "A";
                   VehiclePermits.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<VehiclePermit>().Update(VehiclePermits);
                   _unitOfWork.SaveChanges();
                   // List<PermitRequestDocument> PermitRequestDocumentslist = permitrequestentity.PermitRequestDocuments.ToList();
                   //var DC0 =
                   _unitOfWork.ExecuteSqlCommand(" delete dbo.VehiclePermitRequirementCodes where PermitRequestID = @p0", permitrequestentity.PermitRequestID);
                   if (selectedPermitRequirementCodes.Count > 0)
                   {
                       foreach (var selectedPermitRequirementCode in selectedPermitRequirementCodes)
                       {
                           VehiclePermitRequirementCode.VehiclePermitID = VehiclePermits.VehiclePermitID;
                           VehiclePermitRequirementCode.PermitRequirementCode = selectedPermitRequirementCode;
                           VehiclePermitRequirementCode.PermitRequestID = permitrequestentity.PermitRequestID;
                           VehiclePermitRequirementCode.ObjectState = ObjectState.Added;
                           _unitOfWork.Repository<VehiclePermitRequirementCode>().Insert(VehiclePermitRequirementCode);
                           _unitOfWork.SaveChanges();

                       }
                   }
                   var DC1 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestDocument where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (PermitRequestDocuments.Count > 0)
                   {
                       foreach (var PermitRequestDocument in PermitRequestDocuments)
                       {

                           PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                           PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                           PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                           PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                           PermitRequestDocument.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                       _unitOfWork.SaveChanges();
                   }
               }
               //WharfVehiclePermits Saving------------------------------------------------------------------------------------
               if (permitrequestentity.PermitRequestTypeCode == "APCD")
               {
                   WharfVehiclePermits.PermitRequestID = permitrequestentity.PermitRequestID;
                   WharfVehiclePermits.CreatedDate = DateTime.Now;
                   WharfVehiclePermits.ModifiedDate = DateTime.Now;
                   WharfVehiclePermits.ModifiedBy = anonymousUserId; // 1;
                   WharfVehiclePermits.CreatedBy = anonymousUserId; // 1;
                   WharfVehiclePermits.RecordStatus = "A";
                   WharfVehiclePermits.TelephoneNo = WharfVehiclePermits.TelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                   WharfVehiclePermits.Hometelephone = WharfVehiclePermits.Hometelephone.Replace("(", "").Replace(")", "").Replace("-", "");
                   WharfVehiclePermits.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<WharfVehiclePermit>().Update(WharfVehiclePermits);
                   _unitOfWork.SaveChanges();
                   // List<PermitRequestDocument> PermitRequestDocumentslist = permitrequestentity.PermitRequestDocuments.ToList();

                   //var DC0 = 
                   _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestAccessGates where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (selectedAccessGates.Count > 0)
                   {
                       foreach (var selectedAccessGate in selectedAccessGates)
                       {
                           PermitRequestAccessGates.WharfVehiclePermitID = WharfVehiclePermits.WharfVehiclePermitID;
                           PermitRequestAccessGates.PermitRequestID = permitrequestentity.PermitRequestID;
                           PermitRequestAccessGates.AccessGates = selectedAccessGate;
                           PermitRequestAccessGates.ObjectState = ObjectState.Added;
                           _unitOfWork.Repository<PermitRequestAccessGates>().Insert(PermitRequestAccessGates);
                           _unitOfWork.SaveChanges();

                       }
                   }


                   var DC1 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestDocument where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (PermitRequestDocuments.Count > 0)
                   {
                       foreach (var PermitRequestDocument in PermitRequestDocuments)
                       {

                           PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                           PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                           PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                           PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                           PermitRequestDocument.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                       _unitOfWork.SaveChanges();
                   }
               }
               //Individual Application 

               if (permitrequestentity.PermitRequestTypeCode == "APCF")
               {
                   IndividualPermitApplicationDetails.PermitRequestID = permitrequestentity.PermitRequestID;
                   IndividualPermitApplicationDetails.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<IndividualPermitApplicationDetails>().Update(IndividualPermitApplicationDetails);
                   _unitOfWork.SaveChanges();


                   var DC1 = _unitOfWork.ExecuteSqlCommand(" delete dbo.IndividualVehiclePermit where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (IndividualVehiclePermits.Count > 0)
                   {
                       foreach (var IndividualVehiclePermit in IndividualVehiclePermits)
                       {
                           IndividualVehiclePermit.PermitRequestID = permitrequestentity.PermitRequestID;
                           IndividualVehiclePermit.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<IndividualVehiclePermit>().InsertRange(IndividualVehiclePermits);
                       _unitOfWork.SaveChanges();
                   }




                   if (IndividualPersonalPermits.permittype == IndividualApplication.IndividualTemporary || IndividualPersonalPermits.permittype == IndividualApplication.IndividualPermanent)
                   {
                       IndividualPersonalPermits.ContractorPermanentPermits = null;
                       IndividualPersonalPermits.ContractorTemporaryPermits = null;
                       if (string.IsNullOrEmpty(IndividualPersonalPermits.IndividualPermanentPermits))
                       {
                           IndividualPersonalPermits.IndividualPermanentPermits = null;
                       }
                       if (string.IsNullOrEmpty(IndividualPersonalPermits.IndividualTemporaryPermits))
                       {
                           IndividualPersonalPermits.IndividualTemporaryPermits = null;
                       }
                       if (IndividualPersonalPermits.permittype == IndividualApplication.IndividualTemporary)
                       {
                           IndividualPersonalPermits.PerFromDate = null;
                           IndividualPersonalPermits.PerToDate = null;
                       }
                       else
                       {
                           IndividualPersonalPermits.TempFromDate = null;
                           IndividualPersonalPermits.TempToDate = null;
                       }
                   }

                   IndividualPersonalPermits.AuthorisedMobile = IndividualPersonalPermits.AuthorisedMobile.Replace("(", "").Replace(")", "").Replace("-", "");
                   IndividualPersonalPermits.TelephoneWork = IndividualPersonalPermits.TelephoneWork.Replace("(", "").Replace(")", "").Replace("-", "");
                   IndividualPersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                   IndividualPersonalPermits.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<IndividualPersonalPermit>().Update(IndividualPersonalPermits);
                   _unitOfWork.SaveChanges();
                   var DC2 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitReason where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (applPermitReasons.Count > 0)
                   {
                       foreach (var applPermitReason in applPermitReasons)
                       {
                           applPermitReason.PermitRequestID = permitrequestentity.PermitRequestID;
                       }
                       _unitOfWork.Repository<PermitReason>().InsertRange(applPermitReasons);
                       _unitOfWork.SaveChanges();
                   }
                   var DC3 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestArea where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (applPermitRequestAreas.Count > 0)
                   {
                       foreach (var applPermitRequestArea in applPermitRequestAreas)
                       {
                           applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                       }
                       _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                       _unitOfWork.SaveChanges();
                   }
                   var DC4 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestSubArea where PermitRequestID = @p0", permitrequestentity.PermitRequestID);
                   if (applPermitRequestSubAreas.Count > 0)
                   {
                       foreach (var applPermitRequestSubArea in applPermitRequestSubAreas)
                       {
                           applPermitRequestSubArea.PermitRequestID = permitrequestentity.PermitRequestID;
                       }
                       _unitOfWork.Repository<PermitRequestSubArea>().InsertRange(applPermitRequestSubAreas);
                       _unitOfWork.SaveChanges();
                   }

                   var DC5 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestDocument where PermitRequestID = @p0", permitrequestentity.PermitRequestID);
                   if (PermitRequestDocuments.Count > 0)
                   {
                       foreach (var PermitRequestDocument in PermitRequestDocuments)
                       {
                           PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                           PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                           PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                           PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                           PermitRequestDocument.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                       _unitOfWork.SaveChanges();
                   }
               }
               //contractor Application
               if (permitrequestentity.PermitRequestTypeCode == "APCH")
               {
                   ContractorPermitApplicationDetails.PermitRequestID = permitrequestentity.PermitRequestID;
                   ContractorPermitApplicationDetails.TelephoneNumber = ContractorPermitApplicationDetails.TelephoneNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                   ContractorPermitApplicationDetails.SubContractorTelephoneNumber = ContractorPermitApplicationDetails.SubContractorTelephoneNumber.Replace("(", "").Replace(")", "").Replace("-", "");
                   ContractorPermitApplicationDetails.RecordStatus = "A";
                   ContractorPermitApplicationDetails.CreatedBy = permitrequestentity.CreatedBy;
                   ContractorPermitApplicationDetails.CreatedDate = DateTime.Now;
                   ContractorPermitApplicationDetails.ModifiedBy = permitrequestentity.CreatedBy;
                   ContractorPermitApplicationDetails.ModifiedDate = DateTime.Now;
                   ContractorPermitApplicationDetails.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<ContractorPermitApplicationDetails>().Update(ContractorPermitApplicationDetails);
                   _unitOfWork.SaveChanges();
                   var DC5 = _unitOfWork.ExecuteSqlCommand(" delete dbo.ContractorPermitEmployeeDetails where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (ContractorPermitEmployeeDetails.Count > 0)
                   {
                       foreach (var ContractorPermitEmployeeDetail in ContractorPermitEmployeeDetails)
                       {
                           ContractorPermitEmployeeDetail.PermitRequestID = permitrequestentity.PermitRequestID;
                           ContractorPermitEmployeeDetail.RecordStatus = "A";
                           ContractorPermitEmployeeDetail.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<ContractorPermitEmployeeDetails>().InsertRange(ContractorPermitEmployeeDetails);
                       _unitOfWork.SaveChanges();
                   }

                   if (IndividualPersonalPermits.permittype == ContractorApplication.ContractorTemporary || IndividualPersonalPermits.permittype == ContractorApplication.ContractorPermanent)
                   {
                       IndividualPersonalPermits.IndividualPermanentPermits = null;
                       IndividualPersonalPermits.IndividualTemporaryPermits = null;
                       if (string.IsNullOrEmpty(IndividualPersonalPermits.ContractorPermanentPermits))
                       {
                           IndividualPersonalPermits.ContractorPermanentPermits = null;
                       }
                       if (string.IsNullOrEmpty(IndividualPersonalPermits.ContractorTemporaryPermits))
                       {
                           IndividualPersonalPermits.ContractorTemporaryPermits = null;
                       }

                       if (IndividualPersonalPermits.permittype == ContractorApplication.ContractorTemporary)
                       {
                           IndividualPersonalPermits.ContractorPerFromDate = null;
                           IndividualPersonalPermits.ContractorPerToDate = null;
                       }
                       else
                       {
                           IndividualPersonalPermits.ContractorTempFromDate = null;
                           IndividualPersonalPermits.ContractorTempFromDate = null;
                       }
                   }


                   IndividualPersonalPermits.AuthorisedMobile = IndividualPersonalPermits.AuthorisedMobile.Replace("(", "").Replace(")", "").Replace("-", "");
                   IndividualPersonalPermits.TelephoneWork = IndividualPersonalPermits.TelephoneWork.Replace("(", "").Replace(")", "").Replace("-", "");
                   IndividualPersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                   IndividualPersonalPermits.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<IndividualPersonalPermit>().Update(IndividualPersonalPermits);
                   _unitOfWork.SaveChanges();
                   var DC6 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitReason where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (applPermitReasons.Count > 0)
                   {
                       foreach (var applPermitReason in applPermitReasons)
                       {
                           applPermitReason.PermitRequestID = permitrequestentity.PermitRequestID;
                       }
                       _unitOfWork.Repository<PermitReason>().InsertRange(applPermitReasons);
                       _unitOfWork.SaveChanges();
                   }
                   var DC3 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestArea where PermitRequestID = @p0", permitrequestentity.PermitRequestID);
                   if (applPermitRequestAreas.Count > 0)
                   {
                       foreach (var applPermitRequestArea in applPermitRequestAreas)
                       {
                           applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                       }
                       _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                       _unitOfWork.SaveChanges();
                   }
                   var DC4 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestSubArea where PermitRequestID = @p0", permitrequestentity.PermitRequestID);
                   if (applPermitRequestSubAreas.Count > 0)
                   {
                       foreach (var applPermitRequestSubArea in applPermitRequestSubAreas)
                       {
                           applPermitRequestSubArea.PermitRequestID = permitrequestentity.PermitRequestID;

                       }
                       _unitOfWork.Repository<PermitRequestSubArea>().InsertRange(applPermitRequestSubAreas);
                       _unitOfWork.SaveChanges();
                   }

                   var DC9 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestDocument where PermitRequestID = @p0", permitrequestentity.PermitRequestID);
                   if (PermitRequestDocuments.Count > 0)
                   {
                       foreach (var PermitRequestDocument in PermitRequestDocuments)
                       {
                           PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                           PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                           PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                           PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                           PermitRequestDocument.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                       _unitOfWork.SaveChanges();
                   }
               }

               //VisitorPermits Saving------------------------------------------------------------------------------------
               if (permitrequestentity.PermitRequestTypeCode == "APCE")
               {
                   VisitorPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                   VisitorPermits.CreatedDate = DateTime.Now;
                   VisitorPermits.ModifiedDate = DateTime.Now;
                   VisitorPermits.ModifiedBy = permitrequestentity.ModifiedBy;
                   VisitorPermits.CreatedBy = permitrequestentity.CreatedBy;
                   VisitorPermits.RecordStatus = "A";
                   VisitorPermits.TelephoneNo = VisitorPermits.TelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                   VisitorPermits.ObjectState = ObjectState.Modified;
                   _unitOfWork.Repository<VisitorPermit>().Update(VisitorPermits);
                   _unitOfWork.SaveChanges();

                   // List<PermitRequestDocument> PermitRequestDocumentslist = permitrequestentity.PermitRequestDocuments.ToList();
                   var DC1 = _unitOfWork.ExecuteSqlCommand(" delete dbo.PermitRequestDocument where PermitRequestID = @p0", permitrequestentity.PermitRequestID);

                   if (PermitRequestDocuments.Count > 0)
                   {
                       foreach (var PermitRequestDocument in PermitRequestDocuments)
                       {

                           PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                           PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                           PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                           PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                           PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                           PermitRequestDocument.ObjectState = ObjectState.Added;
                       }
                       _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                       _unitOfWork.SaveChanges();
                   }

               }
               #endregion

               #region Port Entry Pass Notifications

               //_notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), 1, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.Reverted_Resubmission);
               _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), anonymousUserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.Reverted_Resubmission);


               #endregion

               permitrequest = permitrequestentity.MapToDTO();
               return permitrequest;
           });
        }
        public PermitRequestVO GetPortEntryPassRequest(string refrenceNumber, int flag, string portcode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                // string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetPortEntryPassRequest(refrenceNumber, flag, portcode).MapToDTO();
            });
        }

        public PermitRequestVO UpdateRecodeWithComments(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                PermitRequest permitrequestentity = new PermitRequest();
                PermitRequestVO PermitRequestvo = new PermitRequestVO();
                permitrequestentity = permitrequest.MapToEntity();
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserTypeId = 0;
                nextStepCompany.UserType = "EMP";
                permitrequestentity.permitStatus = "PRRS";
                permitrequestentity.CreatedBy = _UserId;
                permitrequestentity.ModifiedBy = _UserId;
                permitrequestentity.CreatedDate = DateTime.Now;
                permitrequestentity.ModifiedDate = DateTime.Now;
                permitrequestentity.PersonalPermits = null;
                permitrequestentity.PermitRequestDocuments = null;
                permitrequestentity.PermitRequestContractors = null;
                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.VisitorPermits = null;
                permitrequestentity.VehiclePermits = null;
                permitrequestentity.WharfVehiclePermits = null;
                //////
                permitrequestentity.IndividualPermitApplicationDetails = null;
                permitrequestentity.IndividualPersonalPermits = null;
                permitrequestentity.IndividualVehiclePermits = null;
                permitrequestentity.PermitRequestSubAreas = null;
                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.PermitReasons = null;
                permitrequestentity.ContractorPermitApplicationDetails = null;
                permitrequestentity.ContractorPermitEmployeeDetails = null;
                ///////////
                // permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                if (permitrequestentity.PermitRequestTypeCode == "APCF")
                {
                    permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                    permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                }
                permitrequestentity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                _unitOfWork.SaveChanges();
                #region Port Entry Pass Notifications
                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassResubmission);
                #endregion
                PermitRequestvo = permitrequestentity.MapToDTO();
                return PermitRequestvo;

            });
        }

        public PermitRequestVO ForwordRecodeWithComments(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                PermitRequest permitrequestentity = new PermitRequest();
                PermitRequestVO PermitRequestvo = new PermitRequestVO();
                permitrequestentity = permitrequest.MapToEntity();
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserTypeId = 0;
                nextStepCompany.UserType = "EMP";
                permitrequestentity.permitStatus = "PSCC";
                permitrequestentity.CreatedBy = _UserId;
                permitrequestentity.ModifiedBy = _UserId;
                permitrequestentity.CreatedDate = DateTime.Now;
                permitrequestentity.ModifiedDate = DateTime.Now;
                permitrequestentity.PersonalPermits = null;
                permitrequestentity.PermitRequestDocuments = null;
                permitrequestentity.PermitRequestContractors = null;
                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.VisitorPermits = null;
                permitrequestentity.VehiclePermits = null;
                permitrequestentity.WharfVehiclePermits = null;
                //////
                permitrequestentity.IndividualPermitApplicationDetails = null;
                permitrequestentity.IndividualPersonalPermits = null;
                permitrequestentity.IndividualVehiclePermits = null;
                permitrequestentity.PermitRequestSubAreas = null;
                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.PermitReasons = null;
                permitrequestentity.ContractorPermitApplicationDetails = null;
                permitrequestentity.ContractorPermitEmployeeDetails = null;
                ///////////
                // permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                _unitOfWork.SaveChanges();
                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.SAPSSecuriryCheck_Verification);
                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.SSASecuriryCheck_Verification);


                PermitRequestvo = permitrequestentity.MapToDTO();
                return PermitRequestvo;

            });
        }

        public PermitRequestVO AddverificationDetails(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 PermitRequest permitrequestentity = new PermitRequest();
                 PermitRequestVerifyedDetail PermitRequestVerifyedDetailsverifyedbySSA = null;
                 PermitRequestVerifyedDetail PermitRequestVerifyedDetailsverifyedbySAPS = null;
                 List<PermitRequestVerifyedDocument> PermitRequestverifyedbySSADocuments = null;
                 List<PermitRequestVerifyedDocument> PermitRequestverifyedbySAPSDocuments = null;
                 CompanyVO nextStepCompany = new CompanyVO();
                 nextStepCompany.UserTypeId = 0;
                 nextStepCompany.UserType = "EMP";
                 permitrequestentity = permitrequest.MapToEntity();
                 PermitRequestVerifyedDetailsverifyedbySSA = permitrequest.PermitRequestVerifyedDetailsverifyedbySSA.MapToEntity();
                 PermitRequestVerifyedDetailsverifyedbySAPS = permitrequest.PermitRequestVerifyedDetailsverifyedbySAPS.MapToEntity();
                 PermitRequestverifyedbySSADocuments = permitrequest.PermitRequestverifyedbySSADocuments.MapToEntity();
                 PermitRequestverifyedbySAPSDocuments = permitrequest.PermitRequestverifyedbySAPSDocuments.MapToEntity();
                 // PermitRequestVerifyedDetails.verifyedDate = DateTime.Now;   
                 if (permitrequest.Flag == 1)
                 {
                     PermitRequestVerifyedDetailsverifyedbySSA.permitrRequestID = permitrequestentity.PermitRequestID;
                     PermitRequestVerifyedDetailsverifyedbySSA.RecordStatus = "A";
                     PermitRequestVerifyedDetailsverifyedbySSA.Flag = "PSSA";
                     PermitRequestVerifyedDetailsverifyedbySSA.verifyedUserID = _UserId;
                     PermitRequestVerifyedDetailsverifyedbySSA.verifyedDate = DateTime.Now;
                     PermitRequestVerifyedDetailsverifyedbySSA.ObjectState = ObjectState.Added;
                     _unitOfWork.Repository<PermitRequestVerifyedDetail>().Insert(PermitRequestVerifyedDetailsverifyedbySSA);
                     _unitOfWork.SaveChanges();

                     if (PermitRequestverifyedbySSADocuments.Count > 0)
                     {
                         foreach (var PermitRequestVerifyedDocument in PermitRequestverifyedbySSADocuments)
                         {
                             PermitRequestVerifyedDocument.PermitRequestverifyedID = PermitRequestVerifyedDetailsverifyedbySSA.PermitRequestverifyedID;
                             PermitRequestVerifyedDocument.CreatedBy = _UserId;
                             PermitRequestVerifyedDocument.CreatedDate = DateTime.Now;
                             PermitRequestVerifyedDocument.ModifiedBy = _UserId;
                             PermitRequestVerifyedDocument.ModifiedDate = DateTime.Now;
                             PermitRequestVerifyedDocument.RecordStatus = permitrequestentity.RecordStatus;
                             PermitRequestVerifyedDocument.ObjectState = ObjectState.Added;
                         }
                         _unitOfWork.Repository<PermitRequestVerifyedDocument>().InsertRange(PermitRequestverifyedbySSADocuments);
                         _unitOfWork.SaveChanges();
                     }
                     _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassSSA_Verified);

                 }
                 if (permitrequest.Flag == 2)
                 {
                     PermitRequestVerifyedDetailsverifyedbySAPS.permitrRequestID = permitrequestentity.PermitRequestID;
                     PermitRequestVerifyedDetailsverifyedbySAPS.RecordStatus = "A";
                     PermitRequestVerifyedDetailsverifyedbySAPS.Flag = "SAPS";
                     PermitRequestVerifyedDetailsverifyedbySAPS.verifyedUserID = _UserId;
                     PermitRequestVerifyedDetailsverifyedbySAPS.verifyedDate = DateTime.Now;
                     PermitRequestVerifyedDetailsverifyedbySAPS.ObjectState = ObjectState.Added;
                     _unitOfWork.Repository<PermitRequestVerifyedDetail>().Insert(PermitRequestVerifyedDetailsverifyedbySAPS);
                     _unitOfWork.SaveChanges();
                     if (PermitRequestverifyedbySAPSDocuments.Count >= 0)
                     {
                         foreach (var PermitRequestVerifyedDocument in PermitRequestverifyedbySAPSDocuments)
                         {
                             PermitRequestVerifyedDocument.PermitRequestverifyedID = PermitRequestVerifyedDetailsverifyedbySAPS.PermitRequestverifyedID;
                             PermitRequestVerifyedDocument.CreatedBy = _UserId;
                             PermitRequestVerifyedDocument.CreatedDate = DateTime.Now;
                             PermitRequestVerifyedDocument.ModifiedBy = _UserId;
                             PermitRequestVerifyedDocument.ModifiedDate = DateTime.Now;
                             PermitRequestVerifyedDocument.RecordStatus = "A";
                             PermitRequestVerifyedDocument.ObjectState = ObjectState.Added;
                         }
                         _unitOfWork.Repository<PermitRequestVerifyedDocument>().InsertRange(PermitRequestverifyedbySAPSDocuments);
                         _unitOfWork.SaveChanges();
                     }
                     _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassSAPS_Verified);

                 }
                 List<PermitRequestVerifyedDetail> existrecords = _unitOfWork.Repository<PermitRequestVerifyedDetail>().Query().Select().Where(e => e.permitrRequestID == permitrequestentity.PermitRequestID).ToList<PermitRequestVerifyedDetail>();
                 if (existrecords.Count >= 2)
                 {
                     //foreach (var iteam in existrecords)
                     //{ iteam.Flag = "PSCV"; }
                     permitrequestentity.permitStatus = "PSCV";
                     permitrequestentity.PermitRequestVerifyedDetails = null;
                     permitrequestentity.PersonalPermits = null;
                     permitrequestentity.PermitRequestDocuments = null;
                     permitrequestentity.PermitRequestContractors = null;
                     permitrequestentity.PermitRequestAreas = null;
                     permitrequestentity.VisitorPermits = null;
                     permitrequestentity.VehiclePermits = null;
                     permitrequestentity.WharfVehiclePermits = null;
                     //////
                     permitrequestentity.IndividualPermitApplicationDetails = null;
                     permitrequestentity.IndividualPersonalPermits = null;
                     permitrequestentity.IndividualVehiclePermits = null;
                     permitrequestentity.PermitRequestSubAreas = null;
                     permitrequestentity.PermitRequestAreas = null;
                     permitrequestentity.PermitReasons = null;
                     permitrequestentity.ContractorPermitApplicationDetails = null;
                     permitrequestentity.ContractorPermitEmployeeDetails = null;
                     ///////////
                     permitrequestentity.CreatedBy = _UserId;
                     permitrequestentity.CreatedDate = DateTime.Now;
                     permitrequestentity.ModifiedBy = _UserId;
                     permitrequestentity.ModifiedDate = DateTime.Now;
                     permitrequestentity.RecordStatus = "A";
                     //permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                     permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                     permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                     permitrequestentity.ObjectState = ObjectState.Modified;
                     _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                     _unitOfWork.SaveChanges();
                 }
                 else if (existrecords.Count < 2)
                 {
                     if (existrecords.FirstOrDefault().Flag == "PSSA")
                     {
                         permitrequestentity.permitStatus = "PSAA";
                         permitrequestentity.PermitRequestVerifyedDetails = null;
                         permitrequestentity.PersonalPermits = null;
                         permitrequestentity.PermitRequestDocuments = null;
                         permitrequestentity.PermitRequestContractors = null;
                         permitrequestentity.PermitRequestAreas = null;
                         permitrequestentity.VisitorPermits = null;
                         permitrequestentity.VehiclePermits = null;
                         permitrequestentity.WharfVehiclePermits = null;
                         //////
                         permitrequestentity.IndividualPermitApplicationDetails = null;
                         permitrequestentity.IndividualPersonalPermits = null;
                         permitrequestentity.IndividualVehiclePermits = null;
                         permitrequestentity.PermitRequestSubAreas = null;
                         permitrequestentity.PermitRequestAreas = null;
                         permitrequestentity.PermitReasons = null;
                         permitrequestentity.ContractorPermitApplicationDetails = null;
                         permitrequestentity.ContractorPermitEmployeeDetails = null;
                         ///////////
                         permitrequestentity.CreatedBy = _UserId;
                         permitrequestentity.CreatedDate = DateTime.Now;
                         permitrequestentity.ModifiedBy = _UserId;
                         permitrequestentity.ModifiedDate = DateTime.Now;
                         permitrequestentity.RecordStatus = "A";
                         // permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                         permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                         permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                         permitrequestentity.ObjectState = ObjectState.Modified;
                         _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                         _unitOfWork.SaveChanges();

                     }
                     else if (existrecords.FirstOrDefault().Flag == "SAPS")
                     {
                         permitrequestentity.permitStatus = "PSCA";
                         permitrequestentity.PermitRequestVerifyedDetails = null;
                         permitrequestentity.PersonalPermits = null;
                         permitrequestentity.PermitRequestDocuments = null;
                         permitrequestentity.PermitRequestContractors = null;
                         permitrequestentity.PermitRequestAreas = null;
                         permitrequestentity.VisitorPermits = null;
                         permitrequestentity.VehiclePermits = null;
                         permitrequestentity.WharfVehiclePermits = null;
                         //////
                         permitrequestentity.IndividualPermitApplicationDetails = null;
                         permitrequestentity.IndividualPersonalPermits = null;
                         permitrequestentity.IndividualVehiclePermits = null;
                         permitrequestentity.PermitRequestSubAreas = null;
                         permitrequestentity.PermitRequestAreas = null;
                         permitrequestentity.PermitReasons = null;
                         permitrequestentity.ContractorPermitApplicationDetails = null;
                         permitrequestentity.ContractorPermitEmployeeDetails = null;
                         ///////////
                         permitrequestentity.CreatedBy = _UserId;
                         permitrequestentity.CreatedDate = DateTime.Now;
                         permitrequestentity.ModifiedBy = _UserId;
                         permitrequestentity.ModifiedDate = DateTime.Now;
                         permitrequestentity.RecordStatus = "A";
                         //permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                         permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                         permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                         permitrequestentity.ObjectState = ObjectState.Modified;
                         _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                         _unitOfWork.SaveChanges();

                     }
                 }
                 permitrequest = permitrequestentity.MapToDTO();
                 return permitrequest;
             });
        }

        public PermitRequestVO ApprovalDenyPortEntryPass(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
        {
            PermitRequest permitrequestentity = new PermitRequest();
            permitrequest.ModifiedBy = _UserId;
            permitrequest.CreatedBy = _UserId;
            CompanyVO nextStepCompany = new CompanyVO();
            nextStepCompany.UserTypeId = 0;
            nextStepCompany.UserType = "EMP";
            permitrequestentity.RecordStatus = "A";

            PermitRequestContractor PermitRequestContractors = null;
            List<PermitRequestDocument> PermitRequestDocuments = null;
            VehiclePermit VehiclePermits = null;
            VisitorPermit VisitorPermits = null;
            WharfVehiclePermit WharfVehiclePermits = null;
            PersonalPermit PersonalPermits = null;
            //added by divya on 18Sep19           
            IndividualPermitApplicationDetails individualPermitApplicationDetails = null;
            IndividualPersonalPermit IndividualPersonalPermits = null;
            ContractorPermitApplicationDetails ContractorPermitApplicationDetails = null;
            List<ContractorPermitEmployeeDetails> ContractorPermitEmployeeDetails = null;
            List<IndividualVehiclePermit> IndividualVehiclePermits = null;
            //added end




            permitrequest.CreatedDate = DateTime.Now;
            permitrequest.ModifiedDate = DateTime.Now;
            permitrequestentity = permitrequest.MapToEntity();

            //added by divya on 18Sep19     
            PermitRequestContractors = permitrequest.PermitRequestContractors.MapToEntity();
            VehiclePermits = permitrequest.VehiclePermits.MapToEntity();
            PersonalPermits = permitrequest.PersonalPermits.MapToEntity();
            VisitorPermits = permitrequest.VisitorPermits.MapToEntity();
            PermitRequestDocuments = permitrequest.PermitRequestDocuments.MapToEntity();
            WharfVehiclePermits = permitrequest.WharfVehiclePermits.MapToEntity();
            individualPermitApplicationDetails = permitrequest.IndividualPermitApplicationDetails.MapToEntity();
            // List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
            ////List<PermitRequestSubArea> applPermitRequestSubAreas = permitrequestentity.PermitRequestSubAreas.ToList();
            //selectedPermitRequirementCodes = permitrequest.selectedPermitRequirementCode;
            // selectedAccessGates = permitrequest.selectedAccessGates;
            //  IndividualVehiclePermits = permitrequest.IndividualVehiclePermits.MapToEntity();
            IndividualPersonalPermits = permitrequest.IndividualPersonalPermits.MapToEntity();
            List<PermitReason> applPermitReasons = permitrequestentity.PermitReasons.ToList();
            List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
            List<PermitRequestSubArea> applPermitRequestSubAreas = permitrequestentity.PermitRequestSubAreas.ToList();
            ContractorPermitEmployeeDetails = permitrequest.ContractorPermitEmployeeDetails.MapToEntity();
            ContractorPermitApplicationDetails = permitrequest.ContractorPermitApplicationDetails.MapToEntity();
            IndividualVehiclePermits = permitrequest.IndividualVehiclePermits.MapToEntity();

            //added end

            PermitRequestContractors = permitrequest.PermitRequestContractors.MapToEntity();
            VehiclePermits = permitrequest.VehiclePermits.MapToEntity();
            PersonalPermits = permitrequest.PersonalPermits.MapToEntity();
            VisitorPermits = permitrequest.VisitorPermits.MapToEntity();
            PermitRequestDocuments = permitrequest.PermitRequestDocuments.MapToEntity();
            WharfVehiclePermits = permitrequest.WharfVehiclePermits.MapToEntity();
            //List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();          
            permitrequestentity.PersonalPermits = null;
            permitrequestentity.PermitRequestDocuments = null;
            permitrequestentity.PermitRequestContractors = null;
            permitrequestentity.PermitRequestAreas = null;
            permitrequestentity.VisitorPermits = null;
            permitrequestentity.VehiclePermits = null;
            permitrequestentity.WharfVehiclePermits = null;
            ////added by divya on 18Sep19     
            permitrequestentity.IndividualPermitApplicationDetails = null;
            //IndividualPersonalPermits.TempFromDate = permitrequest.IndividualPersonalPermits.TempFromDate;
            // IndividualPersonalPermits.TempToDate = permitrequest.IndividualPersonalPermits.TempToDate;
            permitrequestentity.IndividualPersonalPermits = null;
            permitrequestentity.IndividualVehiclePermits = null;
            permitrequestentity.PermitReasons = null;
            permitrequestentity.PermitRequestAreas = null;
            permitrequestentity.PermitRequestSubAreas = null;
            permitrequestentity.ContractorPermitApplicationDetails = null;
            permitrequestentity.ContractorPermitEmployeeDetails = null;
            //added end

            permitrequestentity.RecordStatus = "A";
            //IndividualPersonalPermits = permitrequest.IndividualPersonalPermits.MapToEntity();
            // List<PermitRequestArea> permitrequestarea = permitrequestentity.PermitRequestAreas.ToList();
            // permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
            permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
            permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
            if (permitrequest.Flag == 1)
            {

                //permitrequestentity = permitrequest.MapToEntity();
                //IndividualPersonalPermits = permitrequest.IndividualPersonalPermits.MapToEntity();
                //IndividualPersonalPermits.IndividualPersonalPermitID = permitrequestentity.PermitRequestID;
                permitrequestentity.permitStatus = "PSOA";
                permitrequestentity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                _unitOfWork.SaveChanges();
                #region Port Entry Pass Notifications

                List<IndividualPersonalPermit> individualpersonallist = new List<IndividualPersonalPermit>();
                IndividualPersonalPermit list = new IndividualPersonalPermit()
                {
                    IndividualPersonalPermitID = permitrequest.IndividualPersonalPermits.PermitRequestID,
                    TempFromDate = permitrequest.IndividualPersonalPermits.TempFromDate,
                    TempToDate = permitrequest.IndividualPersonalPermits.TempToDate,
                    PerFromDate = permitrequest.IndividualPersonalPermits.PerFromDate,
                    PerToDate = permitrequest.IndividualPersonalPermits.PerToDate,
                    ContractorTempFromDate = permitrequest.IndividualPersonalPermits.ContractorTempFromDate,
                    ContractorTempToDate = permitrequest.IndividualPersonalPermits.ContractorTempToDate,
                    ContractorPerFromDate = permitrequest.IndividualPersonalPermits.ContractorPerFromDate,
                    ContractorPerToDate = permitrequest.IndividualPersonalPermits.ContractorPerToDate
                };
                individualpersonallist.Add(list);

                permitrequestentity.IndividualPersonalPermits = (ICollection<IndividualPersonalPermit>)individualpersonallist;

                //List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
                ICollection<PermitRequestArea> fsda = new List<PermitRequestArea>();


                //List<PermitRequestArea> l1ist = permitrequestentity.PermitRequestAreas.ToList();
               // foreach (var item in list2)
                //{
                   // fsda.Add(item);
               // }
                //PermitRequestArea llist = new PermitRequestArea()
                //{
                //    PermitRequestAreaCode =permitrequest.PermitRequestAreas[0],
                //    PermitRequestID=permitrequest.PermitRequestID,
                    
                    
                //};
                //fsda.Add(llist);
                //permitrequestentity.PermitRequestAreas = (List<PermitRequestArea>)fsda;
               

                //fsda.Add
              
                //PermitRequestArea nda = new PermitRequestArea()
                //{
                //    PermitRequestAreaCode =permitrequest.PermitRequestAreas
                //}.ToString();
                //fsda.Add(nda);
                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPass_Approve);
                #endregion
            }
            else if (permitrequest.Flag == 2)
            {
                permitrequestentity.permitStatus = "PERR";
                permitrequestentity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                _unitOfWork.SaveChanges();
                #region Port Entry Pass Notifications
                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPass_Rejected);
                #endregion
            }


            permitrequest = permitrequestentity.MapToDTO();
            return permitrequest;
        });
        }

        public PermitRequestVO IssuePortEntryPass(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                PermitRequest permitrequestentity = new PermitRequest();
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserTypeId = 0;
                nextStepCompany.UserType = "EMP";
                permitrequest.ModifiedBy = _UserId;
                permitrequest.CreatedBy = _UserId;
                permitrequest.CreatedDate = DateTime.Now;
                permitrequest.ModifiedDate = DateTime.Now;
                permitrequestentity = permitrequest.MapToEntity();
                permitrequestentity.PersonalPermits = null;
                permitrequestentity.PermitRequestDocuments = null;
                permitrequestentity.PermitRequestContractors = null;
                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.VisitorPermits = null;
                permitrequestentity.VehiclePermits = null;
                permitrequestentity.WharfVehiclePermits = null;

                permitrequestentity.IndividualPermitApplicationDetails = null;
                permitrequestentity.IndividualPersonalPermits = null;
                permitrequestentity.IndividualVehiclePermits = null;
                permitrequestentity.ContractorPermitApplicationDetails = null;
                permitrequestentity.ContractorPermitEmployeeDetails = null;
                permitrequestentity.PermitReasons = null;
                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.PermitRequestSubAreas = null;


                permitrequestentity.RecordStatus = "A";
                permitrequestentity.permitStatus = "PRIC";
                //permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                _unitOfWork.SaveChanges();
                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPass_Issue);

                permitrequest = permitrequestentity.MapToDTO();
                return permitrequest;
            });
        }

        public PermitRequestVO AppealApproveDenyPortEntryPass(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
      {
          PermitRequest permitrequestentity = new PermitRequest();
          permitrequest.ModifiedBy = _UserId;
          permitrequest.CreatedBy = _UserId;
          CompanyVO nextStepCompany = new CompanyVO();
          nextStepCompany.UserTypeId = 0;
          nextStepCompany.UserType = "EMP";
          permitrequestentity.RecordStatus = "A";

          PermitRequestContractor PermitRequestContractors = null;
          List<PermitRequestDocument> PermitRequestDocuments = null;
          VehiclePermit VehiclePermits = null;
          VisitorPermit VisitorPermits = null;
          WharfVehiclePermit WharfVehiclePermits = null;
          PersonalPermit PersonalPermits = null;
          permitrequest.CreatedDate = DateTime.Now;
          permitrequest.ModifiedDate = DateTime.Now;
          permitrequestentity = permitrequest.MapToEntity();

          PermitRequestContractors = permitrequest.PermitRequestContractors.MapToEntity();
          VehiclePermits = permitrequest.VehiclePermits.MapToEntity();
          PersonalPermits = permitrequest.PersonalPermits.MapToEntity();
          VisitorPermits = permitrequest.VisitorPermits.MapToEntity();
          PermitRequestDocuments = permitrequest.PermitRequestDocuments.MapToEntity();
          WharfVehiclePermits = permitrequest.WharfVehiclePermits.MapToEntity();
          List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
          permitrequestentity.PersonalPermits = null;
          permitrequestentity.PermitRequestDocuments = null;
          permitrequestentity.PermitRequestContractors = null;
          permitrequestentity.PermitRequestAreas = null;
          permitrequestentity.VisitorPermits = null;
          permitrequestentity.VehiclePermits = null;
          permitrequestentity.WharfVehiclePermits = null;

          permitrequestentity.IndividualPermitApplicationDetails = null;
          permitrequestentity.IndividualPersonalPermits = null;
          permitrequestentity.IndividualVehiclePermits = null;
          permitrequestentity.ContractorPermitApplicationDetails = null;
          permitrequestentity.ContractorPermitEmployeeDetails = null;
          permitrequestentity.PermitReasons = null;
          permitrequestentity.PermitRequestAreas = null;
          permitrequestentity.PermitRequestSubAreas = null;


          permitrequestentity.RecordStatus = "A";
          // permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
          permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
          permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
          if (permitrequest.Status == "Y")
          {
              permitrequestentity.permitStatus = "PAAD";
              permitrequestentity.ObjectState = ObjectState.Modified;
              _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
              _unitOfWork.SaveChanges();
              _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassAppeal_Approve);

          }
          else if (permitrequest.Status == "N")
          {
              permitrequestentity.permitStatus = "PAUP";
              permitrequestentity.ObjectState = ObjectState.Modified;
              _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
              _unitOfWork.SaveChanges();
              _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassAppeal_Upheld);

          }

          permitrequest = permitrequestentity.MapToDTO();
          return permitrequest;
      });
        }

        public PermitRequestVO AddInternalEmployeePermitDetails(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
              {
                  var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                  PermitRequest permitrequestentity = new PermitRequest();
                  permitrequest.ModifiedBy = anonymousUserId; // 1;
                  permitrequest.CreatedBy = anonymousUserId; // 1;
                  CompanyVO nextStepCompany = new CompanyVO();
                  nextStepCompany.UserTypeId = 0;
                  nextStepCompany.UserType = "EMP";
                  permitrequestentity.RecordStatus = "A";

                  PermitRequestContractor PermitRequestContractors = null;
                  List<PermitRequestDocument> PermitRequestDocuments = null;
                  VehiclePermit VehiclePermits = null;
                  VisitorPermit VisitorPermits = null;
                  WharfVehiclePermit WharfVehiclePermits = null;
                  PersonalPermit PersonalPermits = null;

                  permitrequest.CreatedDate = DateTime.Now;
                  permitrequest.ModifiedDate = DateTime.Now;
                  permitrequestentity = permitrequest.MapToEntity();

                  PermitRequestContractors = permitrequest.PermitRequestContractors.MapToEntity();
                  VehiclePermits = permitrequest.VehiclePermits.MapToEntity();
                  PersonalPermits = permitrequest.PersonalPermits.MapToEntity();
                  VisitorPermits = permitrequest.VisitorPermits.MapToEntity();
                  PermitRequestDocuments = permitrequest.PermitRequestDocuments.MapToEntity();
                  WharfVehiclePermits = permitrequest.WharfVehiclePermits.MapToEntity();
                  List<PermitRequestArea> applPermitRequestAreas = permitrequestentity.PermitRequestAreas.ToList();
                  #region Port Entry Pass Saving

                  permitrequestentity.RecordStatus = "A";
                  permitrequestentity.PersonalPermits = null;
                  permitrequestentity.PermitRequestDocuments = null;
                  permitrequestentity.PermitRequestContractors = null;
                  permitrequestentity.PermitRequestAreas = null;
                  permitrequestentity.VisitorPermits = null;
                  permitrequestentity.VehiclePermits = null;
                  permitrequestentity.WharfVehiclePermits = null;
                  CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                  //string port = permitrequestentity.PortCode;
                  permitrequestentity.ReferenceNo = codeGenerator.GenerateLicenseefno("IEP", _PortCode);
                  permitrequestentity.permitStatus = "IEMN";
                  permitrequestentity.PortCode = _PortCode;
                  permitrequestentity.PermitRequestTypeCode = "APCG";
                  permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                  permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                  permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                  permitrequestentity.ObjectState = ObjectState.Added;
                  _unitOfWork.Repository<PermitRequest>().Insert(permitrequestentity);
                  _unitOfWork.SaveChanges();
                  //if (PersonalPermits!=null)
                  //{
                  if (string.IsNullOrEmpty(PersonalPermits.PermitCategoryCode))
                  {
                      PersonalPermits.PermitCategoryCode = null;
                  }
                  if (string.IsNullOrEmpty(PersonalPermits.AdhocPermits))
                  {
                      PersonalPermits.AdhocPermits = null;
                  }
                  if (string.IsNullOrEmpty(PersonalPermits.TemporaryPermits))
                  {
                      PersonalPermits.TemporaryPermits = null;
                  }
                  if (string.IsNullOrEmpty(PersonalPermits.ConstructionArea))
                  {
                      PersonalPermits.ConstructionArea = null;
                  }
                  PersonalPermits.PermitRequestID = permitrequestentity.PermitRequestID;
                  PersonalPermits.CreatedBy = permitrequestentity.CreatedBy;
                  PersonalPermits.CreatedDate = permitrequestentity.CreatedDate;
                  PersonalPermits.ModifiedBy = permitrequestentity.CreatedBy;
                  PersonalPermits.ModifiedDate = permitrequestentity.ModifiedDate;
                  PersonalPermits.RecordStatus = permitrequestentity.RecordStatus;
                  PersonalPermits.ObjectState = ObjectState.Added;
                  _unitOfWork.Repository<PersonalPermit>().Insert(PersonalPermits);
                  _unitOfWork.SaveChanges();
                  //}
                  if (applPermitRequestAreas.Count > 0)
                  {
                      foreach (var applPermitRequestArea in applPermitRequestAreas)
                      {
                          applPermitRequestArea.PermitRequestID = permitrequestentity.PermitRequestID;
                      }
                      _unitOfWork.Repository<PermitRequestArea>().InsertRange(applPermitRequestAreas);
                      _unitOfWork.SaveChanges();
                  }
                  if (PermitRequestDocuments.Count > 0)
                  {
                      foreach (var PermitRequestDocument in PermitRequestDocuments)
                      {
                          PermitRequestDocument.PermitRequestID = permitrequestentity.PermitRequestID;
                          PermitRequestDocument.CreatedBy = permitrequestentity.CreatedBy;
                          PermitRequestDocument.CreatedDate = permitrequestentity.CreatedDate;
                          PermitRequestDocument.ModifiedBy = permitrequestentity.CreatedBy;
                          PermitRequestDocument.ModifiedDate = permitrequestentity.ModifiedDate;
                          PermitRequestDocument.RecordStatus = permitrequestentity.RecordStatus;
                          PermitRequestDocument.ObjectState = ObjectState.Added;
                      }
                      _unitOfWork.Repository<PermitRequestDocument>().InsertRange(PermitRequestDocuments);
                      _unitOfWork.SaveChanges();
                  }
                  //_notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), 1, nextStepCompany, _PortCode, WFStatus.PortEnteyPassNew);
                  _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), anonymousUserId, nextStepCompany, _PortCode, WFStatus.PortEnteyPassNew);

                  #endregion
                  permitrequest = permitrequestentity.MapToDTO();
                  return permitrequest;
              });
        }



        public List<PermitRequestVO> GetApprovedPermitrequestlist()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetApprovedPortEntryPassRequestlist(portcode).MapToDTO();
            });
        }

       

        public List<PermitRequestVO> GetApprovedPermitrequestlistSearch(PermitRequestSearchVO Searchmdl)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Searchmdl.PortCode = _PortCode;
                string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetApprovedPortEntryPassRequestlistSearch(Searchmdl).MapToDTO();
                //return _portEntryPassApplicationRepository.GetApprovedPortEntryPassRequestlist(portcode).MapToDTO();
            });
        }


        public List<PermitRequestVO> GetInternalEmployeePermitlist()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetInternalEmployeePermitlist(portcode).MapToDTO();
            });
        }




        public PermitRequestVO ApproveInternalEmployeePermitDetails(PermitRequestVO permitrequest)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                PermitRequest permitrequestentity = new PermitRequest();
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserTypeId = 0;
                nextStepCompany.UserType = "EMP";
                permitrequest.ModifiedBy = _UserId;
                permitrequest.CreatedBy = _UserId;
                permitrequest.CreatedDate = DateTime.Now;
                permitrequest.ModifiedDate = DateTime.Now;
                permitrequestentity = permitrequest.MapToEntity();
                #region Port Entry Pass Editing

                permitrequestentity.RecordStatus = "A";

                permitrequestentity.PersonalPermits = null;
                permitrequestentity.PermitRequestDocuments = null;
                permitrequestentity.PermitRequestContractors = null;
                permitrequestentity.PermitRequestAreas = null;
                permitrequestentity.VisitorPermits = null;
                permitrequestentity.VehiclePermits = null;
                permitrequestentity.WharfVehiclePermits = null;
                //if (permitrequest.Flag == 1)
                //{ permitrequestentity.permitStatus = "PRSN"; }
                //if (permitrequest.Flag == 2)
                //{ permitrequestentity.permitStatus = "PRRN"; }
                permitrequestentity.permitStatus = "IEPA";
                permitrequestentity.CompanyFaxNo = permitrequestentity.CompanyFaxNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.CompanyTelephoneNo = permitrequestentity.CompanyTelephoneNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.MobileNo = permitrequestentity.MobileNo.Replace("(", "").Replace(")", "").Replace("-", "");
                permitrequestentity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<PermitRequest>().Update(permitrequestentity);
                _unitOfWork.SaveChanges();
                _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.PortEntryPassApplication).EntityID, permitrequestentity.ReferenceNo.ToString(), _UserId, nextStepCompany, permitrequestentity.PortCode.ToString(), WFStatus.PortEnteyPassAppeal_Approve);
                #endregion
                permitrequest = permitrequestentity.MapToDTO();
                return permitrequest;
            });
        }

        public int GetvalidatePortEntryPassRequestforSsasaps(int id, string flag)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetvalidatePortEntryPassRequestforSsasaps(id, flag, portcode);
            });
        }

        public List<PermitRequestVO> GetInternalEmployeetobeapprovedPermitlist()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                return _portEntryPassApplicationRepository.GetInternalEmployeePermittobeapprovedlist(portcode).MapToDTO();
            });
        }

        /// <summary>
        /// To Get Entity Details Based on EntitiyCode
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        public Entity GetEntities(string entityCode)
        {
            return _portEntryPassApplicationRepository.GetEnties(entityCode);
        }
        /// <summary>
        /// To Get User Details by UserId
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CompanyVO GetUserDetails(int userid)
        {
            return _portEntryPassApplicationRepository.GetUserDetails(_UserId);
        }

        public List<SubCategoryCodeNameWithSupCatCodeVO> GetSubAccessAreasForRB(string supCatCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _subcategoryRepository.GetSubAccessAreasForRB(supCatCode);
            });
        }

        public List<SubCategoryCodeNameWithSupCatCodeVO> GetAreas(string supCatCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _subcategoryRepository.GetAreas(supCatCode);
            });
        }

        public List<PermitRequestVO> GetInternalEmployeetobeapprovedPermitlistSearch(PermitRequestSearchVO Searchmdl)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Searchmdl.PortCode = _PortCode;
                return _portEntryPassApplicationRepository.GetInternalEmployeePermittobeapprovedlistSearch(Searchmdl).MapToDTO();
            });
        }

        public List<PermitRequestVO> GetInternalEmployeePermitlistSearch(PermitRequestSearchVO Searchmdl)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Searchmdl.PortCode = _PortCode;
                return _portEntryPassApplicationRepository.GetInternalEmployeePermitlistSearch(Searchmdl).MapToDTO();
            });
        }

        public List<PermitRequestVO> GetPortEntryPassRequestlistSearch(PermitRequestSearchVO Searchmdl)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Searchmdl.PortCode = _PortCode;
                return _portEntryPassApplicationRepository.GetPortEntryPassRequestlistSearch(Searchmdl).MapToDTO();
            });
            

        }

        public List<PermitRequestVO> GetPortEntryPassRequestlistForSsaSearch(PermitRequestSearchVO Searchmdl)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                Searchmdl.PortCode = _PortCode;
                return _portEntryPassApplicationRepository.GetPortEntryPassRequestlistForSsaSearch(Searchmdl).MapToDTO();
            });
        }



        public List<PermitRequestVO> GetPortEntryPassRequestlistForSapsSearch(PermitRequestSearchVO Searchmdl)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                Searchmdl.PortCode = _PortCode;
                return _portEntryPassApplicationRepository.GetPortEntryPassRequestlistForSapsSearch(Searchmdl).MapToDTO();
            });
        }

    }

}