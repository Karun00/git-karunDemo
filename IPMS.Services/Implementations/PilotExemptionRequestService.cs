using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PilotExemptionRequestService : ServiceBase, IPilotExemptionRequestService
    {
        private IPortConfigurationRepository _portConfigurationRepository;
        private ISubCategoryRepository _subcategoryRepository;
        private IPilotRepository _pilotRepository;
        private IVesselRepository _vesselRepository;
        private IPortRepository _portRepository;
        private IPilotExemptionRequestRepository _pilotExemptionRequestRepository;
        private IAccountRepository _accountRepository;

        public PilotExemptionRequestService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _pilotRepository = new PilotRepository(_unitOfWork);
            _vesselRepository = new VesselRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _pilotExemptionRequestRepository = new PilotExemptionRequestRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        public PilotExemptionRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _pilotRepository = new PilotRepository(_unitOfWork);
            _vesselRepository = new VesselRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _pilotExemptionRequestRepository = new PilotExemptionRequestRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        #region AddPilotExemptionRequest
        /// <summary>
        /// To raise Request for Pilot Exemption Service
        /// </summary>
        /// <param name="PilotExemptionRequestdata"></param>
        /// <returns></returns>
        public PioltVO AddPilotExemptionRequest(PioltVO pilotexemptionrequestdata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                pilotexemptionrequestdata.CellNo = pilotexemptionrequestdata.CellNo.Replace("(", "").Replace(")", "").Replace("-", "");
                pilotexemptionrequestdata.ContactNo = pilotexemptionrequestdata.ContactNo.Replace("(", "").Replace(")", "").Replace("-", "");
                var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                Pilot pilot = null;
                pilotexemptionrequestdata.ModifiedBy = anonymousUserId; // 1; 
                pilotexemptionrequestdata.CreatedBy = anonymousUserId; // 1;
                pilotexemptionrequestdata.CreatedDate = DateTime.Now;
                pilotexemptionrequestdata.ModifiedDate = DateTime.Now;
                pilot = pilotexemptionrequestdata.MapToEntity();

                pilot.ObjectState = ObjectState.Added;
                string remarks = "New Pilot Exemption Request";

                #region License Request Workflow

                PilotExemptionRequestWorkflow pilotExemptionRequestWorkflow = new PilotExemptionRequestWorkflow(_unitOfWork, pilot, remarks);
                WorkFlowEngine<PilotExemptionRequestWorkflow> wf = new WorkFlowEngine<PilotExemptionRequestWorkflow>(_unitOfWork, pilot.PortCode, pilot.CreatedBy);
                wf.Process(pilotExemptionRequestWorkflow, _portConfigurationRepository.GetPortConfiguration(pilot.PortCode).WorkFlowInitialStatus);


                #endregion


                return pilotexemptionrequestdata;
            });
        }
        #endregion

        #region ModifyPilotExemptionRequest
        /// <summary>
        /// To Update raise Request for Pilot Exemption Service
        /// </summary>
        /// <param name="PilotExemptionRequestdata"></param>
        /// <returns></returns>
        public PioltVO ModifyPilotExemptionRequest(PioltVO pilotexemptionrequestdata)
        {

            return EncloseTransactionAndHandleException(() =>
        {
            int userid = _accountRepository.GetUserId(_LoginName);

            pilotexemptionrequestdata.ModifiedBy = userid;
            pilotexemptionrequestdata.CreatedBy = userid;
            pilotexemptionrequestdata.ModifiedDate = DateTime.Now;
            pilotexemptionrequestdata.CreatedDate = DateTime.Now;

            pilotexemptionrequestdata.CellNo = pilotexemptionrequestdata.CellNo.Replace("(", "").Replace(")", "").Replace("-", "");
            pilotexemptionrequestdata.ContactNo = pilotexemptionrequestdata.ContactNo.Replace("(", "").Replace(")", "").Replace("-", "");

            Pilot pilotexemptionrequest = null;
            pilotexemptionrequest = pilotexemptionrequestdata.MapToEntity();
            pilotexemptionrequest.ObjectState = ObjectState.Modified;
            pilotexemptionrequest.PostalAddress.CreatedBy = pilotexemptionrequestdata.CreatedBy;
            pilotexemptionrequest.PostalAddress.CreatedDate = pilotexemptionrequestdata.CreatedDate;
            pilotexemptionrequest.PostalAddress.ModifiedBy = pilotexemptionrequestdata.ModifiedBy;
            pilotexemptionrequest.PostalAddress.ModifiedDate = pilotexemptionrequestdata.ModifiedDate;
            pilotexemptionrequest.PostalAddress.RecordStatus = pilotexemptionrequestdata.RecordStatus;
            pilotexemptionrequest.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<Address>().Update(pilotexemptionrequest.PostalAddress);
            _unitOfWork.SaveChanges();
            pilotexemptionrequest.PostalAddressID = pilotexemptionrequest.PostalAddress.AddressID;
            pilotexemptionrequest.ResidentialAddress.CreatedBy = pilotexemptionrequest.CreatedBy;
            pilotexemptionrequest.ResidentialAddress.CreatedDate = pilotexemptionrequest.CreatedDate;
            pilotexemptionrequest.ResidentialAddress.ModifiedBy = pilotexemptionrequest.ModifiedBy;
            pilotexemptionrequest.ResidentialAddress.ModifiedDate = pilotexemptionrequest.ModifiedDate;
            pilotexemptionrequest.ResidentialAddress.RecordStatus = pilotexemptionrequest.RecordStatus;
            pilotexemptionrequest.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<Address>().Update(pilotexemptionrequest.ResidentialAddress);
            _unitOfWork.SaveChanges();
            pilotexemptionrequest.ResidentialAddressID = pilotexemptionrequest.ResidentialAddress.AddressID;  //PilotExemptionRequestID
            List<PilotExemptionRequest> pilotExemptionrequestList = pilotexemptionrequest.PilotExemptionRequests.ToList();


            if (pilotExemptionrequestList.Count > 0)
            {
                //foreach (var pilotExemptionrequest in pilotExemptionrequestList)
                //{
                //    var DC = _unitOfWork.ExecuteSqlCommand(" delete dbo.PilotExemptionRequest where PilotExemptionRequestID = @p0", pilotExemptionrequest.PilotExemptionRequestID);
                //}

                foreach (var pilotExemptionrequest in pilotExemptionrequestList)
                {
                    pilotExemptionrequest.PilotID = pilotexemptionrequestdata.PilotID;
                    pilotExemptionrequest.PilotExemptionRequestID = pilotExemptionrequest.PilotExemptionRequestID;
                    pilotExemptionrequest.CreatedBy = pilotexemptionrequestdata.CreatedBy;
                    pilotExemptionrequest.CreatedDate = pilotexemptionrequestdata.CreatedDate;
                    pilotExemptionrequest.ModifiedBy = pilotexemptionrequestdata.ModifiedBy;
                    pilotExemptionrequest.ModifiedDate = pilotexemptionrequestdata.ModifiedDate;
                    pilotExemptionrequest.RecordStatus = pilotexemptionrequestdata.RecordStatus;

                    if (pilotExemptionrequest.PilotExemptionRequestID > 0)
                    {
                        pilotexemptionrequest.ObjectState = ObjectState.Modified;
                        _unitOfWork.Repository<PilotExemptionRequest>().Update(pilotExemptionrequest);
                    }
                    else
                    {
                        pilotexemptionrequest.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<PilotExemptionRequest>().Insert(pilotExemptionrequest);
                    }

                }
            }
            pilotexemptionrequest.ObjectState = ObjectState.Modified;
            List<PilotExemptionRequestDocument> PilotExemptionRequestDocumentlist = pilotexemptionrequest.PilotExemptionRequestDocuments.ToList();
            //var DC1 =
                _unitOfWork.ExecuteSqlCommand(" delete dbo.PilotExemptionRequestDocument where PilotID = @p0", pilotexemptionrequest.PilotID);

            if (PilotExemptionRequestDocumentlist.Count > 0)
            {

                foreach (var PilotExemptionRequestDocument in PilotExemptionRequestDocumentlist)
                {

                    PilotExemptionRequestDocument.PilotID = pilotexemptionrequest.PilotID;
                    PilotExemptionRequestDocument.CreatedBy = pilotexemptionrequestdata.CreatedBy;
                    PilotExemptionRequestDocument.CreatedDate = pilotexemptionrequestdata.CreatedDate;
                    PilotExemptionRequestDocument.ModifiedBy = pilotexemptionrequestdata.CreatedBy;
                    PilotExemptionRequestDocument.ModifiedDate = pilotexemptionrequestdata.ModifiedDate;
                    PilotExemptionRequestDocument.RecordStatus = pilotexemptionrequestdata.RecordStatus;
                }
                pilotexemptionrequest.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<PilotExemptionRequestDocument>().InsertRange(PilotExemptionRequestDocumentlist);
                _unitOfWork.SaveChanges();
            }
            _unitOfWork.Repository<Pilot>().Update(pilotexemptionrequest);
            _unitOfWork.SaveChanges();

            pilotexemptionrequestdata = pilotexemptionrequest.MapToDTO();
            return pilotexemptionrequestdata;

        });

        }
        #endregion

        #region GetPilotExemptionRequestReferencesVO
        /// <summary>
        /// To get Pilot Exemption Request Reference data
        /// </summary>
        /// <returns></returns>
        public PilotexemptionRequestReferenceVO GetPilotExemptionRequestReferencesVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                PilotexemptionRequestReferenceVO VO = new PilotexemptionRequestReferenceVO();
                string portCode = _PortCode;

                VO.Ports = _portRepository.GetPortzs().OrderBy(t => t.PortName).ToList();
                VO.Pilots = _pilotRepository.GetListofPilots(portCode);
                VO.PilotRoleCode = _subcategoryRepository.PilotRoleCode().MapToDto();
                VO.Doctypes = _subcategoryRepository.PilotExemptionDocumentsTypes().MapToDto();
                VO.MomentTypes = _subcategoryRepository.MomentTypes().MapToDto();
                VO.Pilot_Nationality = _subcategoryRepository.Pilot_Nationality().MapToDto();
           //     VO.VesselDetails = _vesselRepository.GetVesselDetails().MapToDTOForCombo();

                return VO;
            });
        }
        #endregion

        #region GetPilotExemptionRequestlist
        /// <summary>
        /// To Get Pilot Exemption request Data List
        /// </summary>
        /// <returns></returns>
        /// 
        public List<PioltVO> GetPilotExemptionRequestlist()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portcode = _PortCode;
                return _pilotExemptionRequestRepository.GetPilotExemptionRequestlist(portcode).MapToListDTO();
            });
        }
        #endregion

        #region GetPilotExemptionRequest
        /// <summary>
        /// To Get Pilot Exemption request Data List
        /// </summary>
        /// <returns></returns>
        /// 
        public PioltVO GetPilotExemptionRequest(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _pilotExemptionRequestRepository.GetPilotRequestDetailsforFormByid(id).MapToDTO();
            });
        }
        #endregion

        #region GetVesselNamesautoComplete
        /// <summary>
        /// //////Mahesh : autocomplete:
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<VesselVO> GetVesselNamesautoComplete(string searchValue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
             //   return _vesselRepository.VesselDeetailsAutoCompleteforpilot(searchValue).MapToDTO();
                return _vesselRepository.VesselDeetailsAutoCompleteforpilot(searchValue);
            });
        }
        #endregion

        #region Approve Pilot Exemption Request
        /// <summary>
        /// To Approve Pilot Exemption Request
        /// </summary>
        /// <param name="pilotid"></param>
        /// <param name="comments"></param>
        /// <param name="taskcode"></param>
        public void ApprovePilotExemptionRegistration(string pilotid, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = (from t in _unitOfWork.Repository<Pilot>().Query().Select()
                              where t.PilotID == Convert.ToInt16(pilotid, System.Globalization.CultureInfo.InvariantCulture)
                              select t).FirstOrDefault<Pilot>();

                //PioltVO VO = new PioltVO();
                PilotExemptionRequestWorkflow licenseReqWorkFlow = new PilotExemptionRequestWorkflow(_unitOfWork, andata, comments);
                WorkFlowEngine<PilotExemptionRequestWorkflow> wf = new WorkFlowEngine<PilotExemptionRequestWorkflow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(licenseReqWorkFlow, taskcode);
            });
        }

        #endregion

        #region Reject Pilot Exemption Request
        /// <summary>
        /// To Reject Pilot Exemption Request
        /// </summary>
        /// <param name="pilotid"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        public void RejectPilotExemptionRegistration(string pilotid, string remarks, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = (from t in _unitOfWork.Repository<Pilot>().Query().Select()
                              where t.PilotID == Convert.ToInt16(pilotid, CultureInfo.InvariantCulture)
                              select t).FirstOrDefault<Pilot>();
               // PioltVO VO = new PioltVO();
                PilotExemptionRequestWorkflow agentRegistrationWorkFlow = new PilotExemptionRequestWorkflow(_unitOfWork, andata, remarks);
                WorkFlowEngine<PilotExemptionRequestWorkflow> wf = new WorkFlowEngine<PilotExemptionRequestWorkflow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(agentRegistrationWorkFlow, taskcode);
            });
        }

        #endregion
    }
}
