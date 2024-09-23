using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class LicensingRequestService : ServiceBase, ILicensingRequestService
    {
        private ISubCategoryRepository _subcategoryRepository;
       // private IWorkFlowEngine<LicensingRequestServiceWorkFlow> wfEngine;
       // private IPortConfigurationRepository _portConfigurationRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;
        private ILicensingRequestRepository _licensingRequestRepository;
        //private IAccountRepository _accountRepository;

        public LicensingRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            //_portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _licensingRequestRepository = new LicensingRequestRepository(_unitOfWork);
          //  _accountRepository = new AccountRepository(_unitOfWork);
        }

        public LicensingRequestService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
          //  _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _licensingRequestRepository = new LicensingRequestRepository(_unitOfWork);
           // _accountRepository = new AccountRepository(_unitOfWork);
        }

        #region To Fetch List Data
        /// <summary>
        /// to Get LicensingRequest list data
        /// </summary>
        /// <returns></returns>
        public List<LicenseRequestVO> GetLicensingRequestlist()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string Port = _PortCode;
                return _licensingRequestRepository.GetLicensingRequestlist(Port);
            });
        }

        /// <summary>
        /// to Get LicensingRequest list data
        /// </summary>
        /// <returns></returns>
        public LicenseRequestVO GetLicensingRequest(int licenserequestid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _licensingRequestRepository.GetLicensingRequestDetailsByid(licenserequestid).MapToDtoWithPortcode(_PortCode);
            });
        }

        /// <summary>
        /// GetLicensingRequestbyreference
        /// </summary>
        /// <param name="LicenseRequestRefID"></param>
        /// <returns></returns>
        public LicenseRequestVO GetLicensingRequestbyreference(string licenserequestrefid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _licensingRequestRepository.GetLicensingRequestDetailsByrefid(licenserequestrefid).MapToDto();
            });
        }
        #endregion

        #region Add New LicensingRequest
        /// <summary>
        /// Add New LicensingRequest
        /// </summary>
        /// <param name="licensingrequestdata"></param>
        /// <returns></returns>
        public LicenseRequestVO AddLicensingRequest(LicenseRequestVO licensingrequestdata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int userid = 0;

                if (!string.IsNullOrEmpty(_LoginName))
                {
                    userid = _UserId;
                }
                else
                {
                    var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                    userid = anonymousUserId; // 1; 
                }

                LicenseRequest licenseRequest = null;
                licensingrequestdata.CreatedDate = DateTime.Now;
                licensingrequestdata.ModifiedDate = DateTime.Now;
                licensingrequestdata.ModifiedBy = userid;
                licensingrequestdata.CreatedBy = userid;
                licenseRequest = licensingrequestdata.MapToEntity();

                licenseRequest.ObjectState = ObjectState.Added;
                string remarks = "New Licensing Request";

                #region License Request Workflow
                List<LicenseRequestPort> applWorkFlow = licenseRequest.LicenseRequestPorts.ToList();
                List<LicenseRequestPort> existPorts = _unitOfWork.Repository<LicenseRequestPort>().Query().Select().Where(e => e.LicenseRequestID == licenseRequest.LicenseRequestID).ToList<LicenseRequestPort>();
                List<LicenseRequestPort> curentPorts = new List<LicenseRequestPort>();

                foreach (LicenseRequestPort a in applWorkFlow)
                {
                    var b = existPorts.Find(t => t.PortCode == a.PortCode);
                    if (b == null)
                        curentPorts.Add(a);
                }
                licenseRequest.LicenseRequestPorts = curentPorts;
                if (curentPorts.Count > 0)
                {
                    LicensingRequestServiceWorkFlow licensingRequestServiceWorkFlow = new LicensingRequestServiceWorkFlow(_unitOfWork, licenseRequest, remarks);
                    WorkFlowEngine<LicensingRequestServiceWorkFlow> wf = new WorkFlowEngine<LicensingRequestServiceWorkFlow>(_unitOfWork, _PortCode, licenseRequest.CreatedBy);
                    //  wf.Process(licensingRequestServiceWorkFlow, _portConfigurationRepository.GetPortConfiguration().WorkFlowInitialStatus);
                    wf.Process(licensingRequestServiceWorkFlow, _portGeneralConfigurationRepository.GetPortConfiguration(licensingRequestServiceWorkFlow.PortCodes[0].ToString(), ConfigName.WorkflowInitialStatus));
                }

                #endregion

                return licensingrequestdata;
            });
        }
        #endregion

        #region Update/Modify LicensingRequest
        /// <summary>
        /// Update/Modify LicensingRequest
        /// </summary>
        /// <param name="licensingrequestdata"></param>
        /// <returns></returns>
        public LicenseRequestVO ModifyLicensingRequest(LicenseRequestVO licensingrequestdata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int userid = _UserId;
                LicenseRequest licenseRequest = null;
                licensingrequestdata.ModifiedBy = userid;
                licensingrequestdata.ModifiedDate = DateTime.Now;
                licenseRequest = licensingrequestdata.MapToEntity();
                licenseRequest.ObjectState = ObjectState.Modified;

                licenseRequest.PostalAddress.CreatedBy = licenseRequest.CreatedBy;
                licenseRequest.PostalAddress.CreatedDate = licenseRequest.CreatedDate;
                licenseRequest.PostalAddress.ModifiedBy = licenseRequest.ModifiedBy;
                licenseRequest.PostalAddress.ModifiedDate = licenseRequest.ModifiedDate;
                licenseRequest.PostalAddress.RecordStatus = licenseRequest.RecordStatus;
                licenseRequest.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Address>().Update(licenseRequest.PostalAddress);
                licenseRequest.PostalAddressID = licenseRequest.PostalAddress.AddressID;

                licenseRequest.BusinessAddress.CreatedBy = licenseRequest.CreatedBy;
                licenseRequest.BusinessAddress.CreatedDate = licenseRequest.CreatedDate;
                licenseRequest.BusinessAddress.ModifiedBy = licenseRequest.ModifiedBy;
                licenseRequest.BusinessAddress.ModifiedDate = licenseRequest.ModifiedDate;
                licenseRequest.BusinessAddress.RecordStatus = licenseRequest.RecordStatus;
                licenseRequest.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Address>().Update(licenseRequest.BusinessAddress);
                licenseRequest.BusinessAddressID = licenseRequest.BusinessAddress.AddressID;

                licenseRequest.AuthorizedContactPerson.CreatedBy = licenseRequest.CreatedBy;
                licenseRequest.AuthorizedContactPerson.CreatedDate = licenseRequest.CreatedDate;
                licenseRequest.AuthorizedContactPerson.ModifiedBy = licenseRequest.ModifiedBy;
                licenseRequest.AuthorizedContactPerson.ModifiedDate = licenseRequest.ModifiedDate;
                licenseRequest.AuthorizedContactPerson.RecordStatus = licenseRequest.RecordStatus;
                licenseRequest.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<AuthorizedContactPerson>().Update(licenseRequest.AuthorizedContactPerson);
                licenseRequest.AuthorizedContactPersonID = licenseRequest.AuthorizedContactPerson.AuthorizedContactPersonID;

                licenseRequest.CreatedDate = licenseRequest.CreatedDate;
                licenseRequest.CreatedBy = licenseRequest.CreatedBy;
                licenseRequest.ModifiedDate = licenseRequest.ModifiedDate;
                licenseRequest.ModifiedBy = licenseRequest.ModifiedBy;
                licenseRequest.ObjectState = ObjectState.Modified;

                List<LicenseRequestPort> applWorkFlow = licenseRequest.LicenseRequestPorts.ToList();
                List<LicenseRequestPort> existPorts = _unitOfWork.Repository<LicenseRequestPort>().Query().Select().Where(e => e.LicenseRequestID == licenseRequest.LicenseRequestID).ToList<LicenseRequestPort>();
                List<LicenseRequestPort> curentPorts = new List<LicenseRequestPort>();

                foreach (LicenseRequestPort a in applWorkFlow)
                {
                    var b = existPorts.Find(t => t.PortCode == a.PortCode);
                    if (b == null)
                        curentPorts.Add(a);
                }
                licenseRequest.LicenseRequestPorts = curentPorts;

                if (curentPorts.Count > 0)
                {
                    //var brt =
                        _unitOfWork.ExecuteSqlCommand(" delete dbo.LicenseRequestPort where LicenseRequestID = @p0", licenseRequest.LicenseRequestID);

                    if (licenseRequest.LicenseRequestPorts.Count > 0)
                    {

                        foreach (var requestforport in licenseRequest.LicenseRequestPorts)
                        {
                            requestforport.LicenseRequestID = licenseRequest.LicenseRequestID;
                            requestforport.WFStatus = "WFSA";
                            requestforport.ApprovedBy = licenseRequest.CreatedBy;
                            requestforport.ApprovedDate = licenseRequest.CreatedDate;
                            requestforport.CreatedBy = licenseRequest.CreatedBy;
                            requestforport.CreatedDate = licenseRequest.CreatedDate;
                            requestforport.VerifiedBy = licenseRequest.CreatedBy;
                            requestforport.VerifiedDate = licenseRequest.CreatedDate;
                            requestforport.ModifiedBy = licenseRequest.ModifiedBy;
                            requestforport.ModifiedDate = licenseRequest.ModifiedDate;
                            requestforport.RecordStatus = licenseRequest.RecordStatus;
                        }
                        licenseRequest.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<LicenseRequestPort>().InsertRange(licenseRequest.LicenseRequestPorts);

                    }
                }
                licenseRequest.ObjectState = ObjectState.Modified;

                List<LicenseRequestDocument> LicenseRequestDocumentList = licenseRequest.LicenseRequestDocuments.ToList();

                //var DC = 
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.LicenseRequestDocument where LicenseRequestID = @p0", licenseRequest.LicenseRequestID);

                if (LicenseRequestDocumentList.Count > 0)
                {

                    foreach (var licenseRequestDocument in LicenseRequestDocumentList)
                    {
                        licenseRequestDocument.LicenseRequestID = licenseRequest.LicenseRequestID;
                        licenseRequestDocument.CreatedBy = licenseRequest.CreatedBy;
                        licenseRequestDocument.CreatedDate = licenseRequest.CreatedDate;
                        licenseRequestDocument.ModifiedBy = licenseRequest.CreatedBy;
                        licenseRequestDocument.ModifiedDate = licenseRequest.ModifiedDate;
                        licenseRequestDocument.RecordStatus = licenseRequest.RecordStatus;
                    }
                    licenseRequest.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<LicenseRequestDocument>().InsertRange(LicenseRequestDocumentList);
                }

                List<Bunkering> BunkeringList = licenseRequest.Bunkerings.ToList();
                List<FireEquipment> FireEquipmentList = licenseRequest.FireEquipments.ToList();
                List<FireProtection> FireProtectionList = licenseRequest.FireProtections.ToList();
                List<PestControl> PestControlList = licenseRequest.PestControls.ToList();
                List<FloatingCrane> FloatingCraneList = licenseRequest.FloatingCranes.ToList();
                List<Stevedore> StevedoreList = licenseRequest.Stevedores.ToList();
                List<PollutionControl> PollutionControlList = licenseRequest.PollutionControls.ToList();
                List<Diving> DivingList = licenseRequest.Divings.ToList();

                switch (licenseRequest.LicenseRequestType)
                {
                    case "BNK":
                        licenseRequest.FireEquipments = null;
                        licenseRequest.FireProtections = null;
                        licenseRequest.PestControls = null;
                        licenseRequest.FloatingCranes = null;
                        licenseRequest.Stevedores = null;
                        licenseRequest.PollutionControls = null;
                        licenseRequest.Divings = null;

                        foreach (Bunkering bunkering in BunkeringList)
                        {
                            bunkering.LicenseRequestID = licenseRequest.LicenseRequestID;
                            bunkering.RecordStatus = licenseRequest.RecordStatus;
                            bunkering.CreatedBy = licenseRequest.CreatedBy;
                            bunkering.CreatedDate = licenseRequest.CreatedDate;
                            bunkering.ModifiedBy = licenseRequest.ModifiedBy;
                            bunkering.ModifiedDate = licenseRequest.ModifiedDate;
                            _unitOfWork.Repository<Bunkering>().Update(bunkering);
                        }

                        _unitOfWork.SaveChanges();
                        break;

                    case "FIE":
                        licenseRequest.Bunkerings = null;
                        licenseRequest.FireProtections = null;
                        licenseRequest.PestControls = null;
                        licenseRequest.FloatingCranes = null;
                        licenseRequest.Stevedores = null;
                        licenseRequest.PollutionControls = null;
                        licenseRequest.Divings = null;
                        foreach (FireEquipment fireEquipment in FireEquipmentList)
                        {
                            fireEquipment.LicenseRequestID = licenseRequest.LicenseRequestID;
                            fireEquipment.RecordStatus = licenseRequest.RecordStatus;
                            fireEquipment.CreatedBy = licenseRequest.CreatedBy;
                            fireEquipment.CreatedDate = licenseRequest.CreatedDate;
                            fireEquipment.ModifiedBy = licenseRequest.ModifiedBy;
                            fireEquipment.ModifiedDate = licenseRequest.ModifiedDate;
                            _unitOfWork.Repository<FireEquipment>().Update(fireEquipment);
                        }
                        _unitOfWork.SaveChanges();
                        break;

                    case "FIP":
                        licenseRequest.Bunkerings = null;
                        licenseRequest.FireEquipments = null;
                        licenseRequest.PestControls = null;
                        licenseRequest.FloatingCranes = null;
                        licenseRequest.Stevedores = null;
                        licenseRequest.PollutionControls = null;
                        licenseRequest.Divings = null;

                        foreach (FireProtection fireProtection in FireProtectionList)
                        {
                            fireProtection.LicenseRequestID = licenseRequest.LicenseRequestID;
                            fireProtection.RecordStatus = licenseRequest.RecordStatus;
                            fireProtection.CreatedBy = licenseRequest.CreatedBy;
                            fireProtection.CreatedDate = licenseRequest.CreatedDate;
                            fireProtection.ModifiedBy = licenseRequest.ModifiedBy;
                            fireProtection.ModifiedDate = licenseRequest.ModifiedDate;
                            _unitOfWork.Repository<FireProtection>().Update(fireProtection);

                        }
                        _unitOfWork.SaveChanges();
                        break;

                    case "PST":
                        licenseRequest.Bunkerings = null;
                        licenseRequest.FireEquipments = null;
                        licenseRequest.FireProtections = null;
                        licenseRequest.FloatingCranes = null;
                        licenseRequest.Stevedores = null;
                        licenseRequest.PollutionControls = null;
                        licenseRequest.Divings = null;
                        foreach (PestControl pestControl in PestControlList)
                        {
                            pestControl.LicenseRequestID = licenseRequest.LicenseRequestID;
                            pestControl.RecordStatus = licenseRequest.RecordStatus;
                            pestControl.CreatedBy = licenseRequest.CreatedBy;
                            pestControl.CreatedDate = licenseRequest.CreatedDate;
                            pestControl.ModifiedBy = licenseRequest.ModifiedBy;
                            pestControl.ModifiedDate = licenseRequest.ModifiedDate;
                            _unitOfWork.Repository<PestControl>().Update(pestControl);

                        }
                        _unitOfWork.SaveChanges();
                        break;

                    case "FLC":
                        licenseRequest.Bunkerings = null;
                        licenseRequest.FireEquipments = null;
                        licenseRequest.FireProtections = null;
                        licenseRequest.PestControls = null;
                        licenseRequest.Stevedores = null;
                        licenseRequest.PollutionControls = null;
                        licenseRequest.Divings = null;
                        foreach (FloatingCrane floatingCrane in FloatingCraneList)
                        {
                            floatingCrane.LicenseRequestID = licenseRequest.LicenseRequestID;
                            floatingCrane.RecordStatus = licenseRequest.RecordStatus;
                            floatingCrane.CreatedBy = licenseRequest.CreatedBy;
                            floatingCrane.CreatedDate = licenseRequest.CreatedDate;
                            floatingCrane.ModifiedBy = licenseRequest.ModifiedBy;
                            floatingCrane.ModifiedDate = licenseRequest.ModifiedDate;
                            _unitOfWork.Repository<FloatingCrane>().Update(floatingCrane);

                        }
                        _unitOfWork.SaveChanges();
                        break;

                    case "STD":
                        licenseRequest.Bunkerings = null;
                        licenseRequest.FireEquipments = null;
                        licenseRequest.FireProtections = null;
                        licenseRequest.PestControls = null;
                        licenseRequest.FloatingCranes = null;
                        licenseRequest.PollutionControls = null;
                        licenseRequest.Divings = null;

                        foreach (Stevedore stevedore in StevedoreList)
                        {
                            stevedore.LicenseRequestID = licenseRequest.LicenseRequestID;
                            stevedore.RecordStatus = licenseRequest.RecordStatus;
                            stevedore.CreatedBy = licenseRequest.CreatedBy;
                            stevedore.CreatedDate = licenseRequest.CreatedDate;
                            stevedore.ModifiedBy = licenseRequest.ModifiedBy;
                            stevedore.ModifiedDate = licenseRequest.ModifiedDate;
                            _unitOfWork.Repository<Stevedore>().Update(stevedore);

                        }
                        _unitOfWork.SaveChanges();
                        break;

                    case "POC":

                        licenseRequest.Bunkerings = null;
                        licenseRequest.FireEquipments = null;
                        licenseRequest.FireProtections = null;
                        licenseRequest.PestControls = null;
                        licenseRequest.FloatingCranes = null;
                        licenseRequest.Stevedores = null;
                        licenseRequest.Divings = null;

                        foreach (PollutionControl pollutionControl in PollutionControlList)
                        {
                            pollutionControl.LicenseRequestID = licenseRequest.LicenseRequestID;
                            pollutionControl.RecordStatus = licenseRequest.RecordStatus;
                            pollutionControl.CreatedBy = licenseRequest.CreatedBy;
                            pollutionControl.CreatedDate = licenseRequest.CreatedDate;
                            pollutionControl.ModifiedBy = licenseRequest.ModifiedBy;
                            pollutionControl.ModifiedDate = licenseRequest.ModifiedDate;
                            _unitOfWork.Repository<PollutionControl>().Update(pollutionControl);

                        }
                        _unitOfWork.SaveChanges();
                        break;

                    case "DIV":

                        licenseRequest.Bunkerings = null;
                        licenseRequest.FireEquipments = null;
                        licenseRequest.FireProtections = null;
                        licenseRequest.PestControls = null;
                        licenseRequest.FloatingCranes = null;
                        licenseRequest.Stevedores = null;
                        licenseRequest.PollutionControls = null;

                        foreach (Diving diving in DivingList)
                        {
                            diving.DivingID = DivingList.FirstOrDefault().DivingID;
                            diving.LicenseRequestID = licenseRequest.LicenseRequestID;
                            diving.RecordStatus = licenseRequest.RecordStatus;
                            diving.CreatedBy = licenseRequest.CreatedBy;
                            diving.CreatedDate = licenseRequest.CreatedDate;
                            diving.ModifiedBy = licenseRequest.ModifiedBy;
                            diving.ModifiedDate = licenseRequest.ModifiedDate;
                            _unitOfWork.Repository<Diving>().Update(diving);
                        }

                        break;
                }

                _unitOfWork.Repository<LicenseRequest>().Update(licenseRequest);
                _unitOfWork.SaveChanges();

                return licensingrequestdata;
            });
        }

        #endregion

        #region Verify LicensingRequest
        /// <summary>
        /// Verify LicensingRequest
        /// </summary>
        /// <param name="licensereqid"></param>
        /// <param name="rematks"></param>
        /// <param name="taskcode"></param>
        public void VerifyLicenseRegistration(string licensereqid, string rematks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var lrdata = _unitOfWork.SqlQuery<LicenseRequest>("select * from LicenseRequest where LicenseRequestID = @p0", licensereqid).FirstOrDefault<LicenseRequest>();
                var lrports = _unitOfWork.SqlQuery<LicenseRequestPort>("select * from LicenseRequestPort where LicenseRequestID = @p0 and PortCode=@p1", licensereqid, _PortCode).ToList<LicenseRequestPort>();
                lrdata.LicenseRequestPorts = lrports;
                LicensingRequestServiceWorkFlow licenseReqWorkFlow = new LicensingRequestServiceWorkFlow(_unitOfWork, lrdata, rematks);
                WorkFlowEngine<LicensingRequestServiceWorkFlow> wf = new WorkFlowEngine<LicensingRequestServiceWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(licenseReqWorkFlow, taskcode);

            });
        }

        #endregion

        #region Approve LicensingRequest
        /// <summary>
        /// Approve LicensingRequest
        /// </summary>
        /// <param name="licensereqid"></param>
        /// <param name="rematks"></param>
        /// <param name="taskcode"></param>
        public void ApproveLicenseRegistration(string licensereqid, string rematks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var lrdata = _unitOfWork.SqlQuery<LicenseRequest>("select * from LicenseRequest where LicenseRequestID = @p0", licensereqid).FirstOrDefault<LicenseRequest>();
                var lrports = _unitOfWork.SqlQuery<LicenseRequestPort>("select * from LicenseRequestPort where LicenseRequestID = @p0 and PortCode=@p1", licensereqid, _PortCode).ToList<LicenseRequestPort>();
                lrdata.LicenseRequestPorts = lrports;

                LicensingRequestServiceWorkFlow licenseReqWorkFlow = new LicensingRequestServiceWorkFlow(_unitOfWork, lrdata, rematks);
                WorkFlowEngine<LicensingRequestServiceWorkFlow> wf = new WorkFlowEngine<LicensingRequestServiceWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(licenseReqWorkFlow, taskcode);
                string updateQuery = "update dbo.LicenseRequestPort set WFStatus='WFSA' where LicenseRequestID = " +
           licensereqid + " and PortCode = '" + _PortCode + "'";
                _unitOfWork.ExecuteSqlCommand(updateQuery);
            });
        }

        #endregion

        #region Reject LicensingRequest
        /// <summary>
        /// Approve LicensingRequest
        /// </summary>
        /// <param name="licensereqid"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RejectLicenseRegistration(string licensereqid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var lrdata = _unitOfWork.SqlQuery<LicenseRequest>("select * from LicenseRequest where LicenseRequestID = @p0", licensereqid).FirstOrDefault<LicenseRequest>();
                var lrports = _unitOfWork.SqlQuery<LicenseRequestPort>("select * from LicenseRequestPort where LicenseRequestID = @p0 and PortCode=@p1", licensereqid, _PortCode).ToList<LicenseRequestPort>();
                lrdata.LicenseRequestPorts = lrports;
                LicensingRequestServiceWorkFlow licenseRequestWorkFlow = new LicensingRequestServiceWorkFlow(_unitOfWork, lrdata, remarks);
                WorkFlowEngine<LicensingRequestServiceWorkFlow> wf = new WorkFlowEngine<LicensingRequestServiceWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(licenseRequestWorkFlow, taskcode);
            });
        }
        #endregion

        #region LicenseRequest Initialize Reference data
        /// <summary>
        /// LicenseRequest Initialize Reference data
        /// </summary>
        /// <returns></returns>
        public LicenseRequestReferenceVO GetLicenseRequestReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                LicenseRequestReferenceVO VO = new LicenseRequestReferenceVO();
                VO.DocumentsTypes = _subcategoryRepository.LicenseRequestDocumentsTypes().MapToDto();
                VO.LicensingRequestTypes = _subcategoryRepository.LicensingRequestTypes().MapToDto();
                return VO;
            });
        }
        #endregion

        #region CheckReferenceNoExists
        /// <summary>
        /// Check ReferenceNo Exists
        /// </summary>
        /// <param name="ReferenceNo"></param>
        /// <returns></returns>
        public bool CheckReferenceNoExists(string referenceno)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _licensingRequestRepository.CheckReferenceNoExists(referenceno);
            });
        }
        #endregion
    }
}
