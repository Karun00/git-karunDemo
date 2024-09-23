using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IPMS.Services.WorkFlow
{
    public class PilotExemptionRequestWorkflow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private Pilot _pilotExemptionService;
       // private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.PilotExemption;
        private string _remarks;

        public PilotExemptionRequestWorkflow(IUnitOfWork unitOfWork, Pilot pilotrequest, string remarks)
        {
            _unitOfWork = unitOfWork;
            _pilotExemptionService = pilotrequest;
            // _AgentPorts = AgentPorts;
            _remarks = remarks;
            //_accountRepository = new AccountRepository(unitOfWork);
        }

        #region userid
        /// <summary>
        /// To get Created User Id
        /// </summary>
        public int userid
        {
            get { return _pilotExemptionService.CreatedBy; }
        }
        #endregion

        #region Entity
        /// <summary>
        /// To get entity Details
        /// </summary>
        public Entity Entity
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }
        #endregion

        #region GetCompanyDetails
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
        #endregion

        #region ReferenceId
        /// <summary>
        /// To get Request reference id
        /// </summary>
        public string ReferenceId
        {
            get { return _pilotExemptionService.PilotID.ToString(CultureInfo.InvariantCulture); }
        }
        #endregion

        #region Remarks
        /// <summary>
        ///  to get Remarks
        /// </summary>
        public string Remarks
        {
            get { return _remarks; }
        }
        #endregion

        #region ReferenceData
        /// <summary>
        /// to get ReferenceData
        /// </summary>
        public string ReferenceData
        {
            get
            {
                return Common.GetTokensDictionaryForReferenceData(Entity, _pilotExemptionService);
            }
        }
        #endregion

        #region GetRequestStatus
        /// <summary>
        /// to get GetRequestStatus
        /// </summary>
        /// <param name="p_entitycode"></param>
        /// <param name="p_referenceno"></param>
        /// <returns></returns>
        public int GetRequestStatus(string entitycode, string referenceno)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode }
                               equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }
                               where e.EntityCode == entitycode && w.ReferenceID == referenceno
                               select w).Count();
            return _entitycode;
        }
        #endregion

        #region PortCodes
        /// <summary>
        /// To get Port code List
        /// </summary>
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(_pilotExemptionService.PortCode);
                return portcodes;
            }
        }
        #endregion

        #region PilotExemptionRequestWorkflow
        /// <summary>
        /// PilotExemptionRequestWorkflow
        /// </summary>
        public PilotExemptionRequestWorkflow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }
        #endregion

        #region SetWorkFlowId
        /// <summary>
        /// SetWorkFlow for each Port
        /// </summary>
        /// <param name="workFlowInstanceId"></param>
        /// <param name="portCode"></param>
        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            _pilotExemptionService.WorkflowInstanceId = workFlowInstanceId;
            _pilotExemptionService.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<Pilot>().Update(_pilotExemptionService);
            _unitOfWork.SaveChanges();
        }
        #endregion

        #region ExecuteTask
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
                case "WFSA":
                    UpdateExpiryDate();
                    break;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create for Request
        /// </summary>
        public void Create()
        {
            if (_pilotExemptionService.PilotID > 0)
            {
                UpdateStatus();
            }
            else
            {
                _pilotExemptionService.ObjectState = ObjectState.Added;
                _pilotExemptionService.PostalAddress.CreatedBy = _pilotExemptionService.CreatedBy;
                _pilotExemptionService.PostalAddress.CreatedDate = _pilotExemptionService.CreatedDate;
                _pilotExemptionService.PostalAddress.ModifiedBy = _pilotExemptionService.CreatedBy;
                _pilotExemptionService.PostalAddress.ModifiedDate = _pilotExemptionService.CreatedDate;
                _pilotExemptionService.PostalAddress.RecordStatus = "A";
                _unitOfWork.Repository<Address>().Insert(_pilotExemptionService.PostalAddress);
                _unitOfWork.SaveChanges();

                _pilotExemptionService.ResidentialAddressID = _pilotExemptionService.PostalAddress.AddressID;

                if (_pilotExemptionService.ResidentialAddress != null)
                {
                    _pilotExemptionService.ResidentialAddress.CreatedBy = _pilotExemptionService.CreatedBy;
                    _pilotExemptionService.ResidentialAddress.CreatedDate = _pilotExemptionService.CreatedDate;
                    _pilotExemptionService.ResidentialAddress.ModifiedBy = _pilotExemptionService.CreatedBy;
                    _pilotExemptionService.ResidentialAddress.ModifiedDate = _pilotExemptionService.CreatedDate;
                    _pilotExemptionService.ResidentialAddress.RecordStatus = "A";
                    _unitOfWork.Repository<Address>().Insert(_pilotExemptionService.ResidentialAddress);
                    _unitOfWork.SaveChanges();
                    _pilotExemptionService.PostalAddressID = _pilotExemptionService.ResidentialAddress.AddressID;
                }

                //List<PilotCertificate> pilotcertificates = _pilotExemptionService.PilotCertificates.ToList();
                List<PilotExemptionRequest> Requests = _pilotExemptionService.PilotExemptionRequests.ToList();
                List<PilotExemptionRequestDocument> PilotExemptionRequestDocuments = _pilotExemptionService.PilotExemptionRequestDocuments.ToList();
                _pilotExemptionService.PilotCertificates = null;
                _pilotExemptionService.PilotExemptionRequests = null;
                _pilotExemptionService.PostalAddress = null;
                _pilotExemptionService.ResidentialAddress = null;
                _pilotExemptionService.PortCode = _pilotExemptionService.PortCode;
                _pilotExemptionService.PilotExemptionRequestDocuments = null;
                _unitOfWork.Repository<Pilot>().Insert(_pilotExemptionService);
                _unitOfWork.SaveChanges();

                foreach (PilotExemptionRequest request in Requests)
                {
                    request.PilotID = _pilotExemptionService.PilotID;
                    request.CreatedBy = _pilotExemptionService.CreatedBy;
                    request.CreatedDate = DateTime.Now;
                    request.ModifiedBy = _pilotExemptionService.ModifiedBy;
                    request.ModifiedDate = DateTime.Now;
                    request.RecordStatus = "A";
                }
                _unitOfWork.Repository<PilotExemptionRequest>().InsertRange(Requests);

                foreach (PilotExemptionRequestDocument RequestDocument in PilotExemptionRequestDocuments)
                {
                    RequestDocument.PilotID = _pilotExemptionService.PilotID;
                    RequestDocument.CreatedBy = _pilotExemptionService.CreatedBy;
                    RequestDocument.CreatedDate = DateTime.Now;
                    RequestDocument.ModifiedBy = _pilotExemptionService.ModifiedBy;
                    RequestDocument.ModifiedDate = DateTime.Now;
                    RequestDocument.RecordStatus = "A";
                }
                _unitOfWork.Repository<PilotExemptionRequestDocument>().InsertRange(PilotExemptionRequestDocuments);
                _unitOfWork.SaveChanges();
            }

        }
        #endregion

        #region UpdateStatus
        /// <summary>
        /// Update
        /// </summary>
        public void UpdateStatus()
        {
        }
        #endregion

        #region UpdateExpiryDate
        /// <summary>
        /// Update ExpiryDate in Pilot Table
        /// </summary>
        public void UpdateExpiryDate()
        {
            var ExpiryDate = (from e in _unitOfWork.Repository<FinancialYear>().Query().Select()
                              where e.IsCurrentFinancialYear == "Y"
                              select e.EndDate).FirstOrDefault();

            _pilotExemptionService.ObjectState = ObjectState.Modified;
            _pilotExemptionService.ExpiryDate = Convert.ToDateTime(ExpiryDate);
            _pilotExemptionService.IssuedApprovedDate = DateTime.Now;
            _unitOfWork.Repository<Pilot>().Update(_pilotExemptionService);
            _unitOfWork.SaveChanges();
        }
        #endregion
    }
}
