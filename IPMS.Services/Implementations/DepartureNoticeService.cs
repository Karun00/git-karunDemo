using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using IPMS.Core.Repository.Exceptions;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DepartureNoticeService : ServiceBase, IDepartureNoticeService
    {
        //private ISubCategoryRepository _subcategoryRepository;
        private IAccountRepository _accountRepository;
        //private IWorkFlowEngine<DepartureNoticeWorkFlow> wfEngine;
        private IPortConfigurationRepository _portConfigurationRepository;
        private IDepartureNoticeRepository _DepartureNoticeRepository;
        private int _userid;

        public DepartureNoticeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            //wfEngine = new WorkFlowEngine<DepartureNoticeWorkFlow>();
            // _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _DepartureNoticeRepository = new DepartureNoticeRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
        }

        public DepartureNoticeService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            // wfEngine = new WorkFlowEngine<DepartureNoticeWorkFlow>();
            //_subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _DepartureNoticeRepository = new DepartureNoticeRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
        }
        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<RevenuePostingVO> DepartureNoticeVcnDetailsforAutocomplete(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var data = _DepartureNoticeRepository.GetTerminalOperatorForUser(_UserId, _PortCode);
                int AgentUserID = 0;
                int UserID = 0;

                if (data.UserType == UserType.Agent)
                {
                    AgentUserID = data.UserTypeID;
                    UserID = _UserId;
                }
                else if (data.UserType == UserType.ADMIN)
                {
                    AgentUserID = 0;
                    UserID = 0;
                }
                return _DepartureNoticeRepository.DepartureNoticeVcnDetailsforAutocomplete(AgentUserID, _PortCode, UserID, searchvalue);
            });
        }
        /// <summary>
        ///  Vessel / IMO No. Autocomplete for Search
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<VesselVO> DepartureNoticeVesselDetailsforAutocomplete(string searchvalue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                var data = _DepartureNoticeRepository.GetTerminalOperatorForUser(_UserId, _PortCode);
                int AgentUserID = 0;
                int UserID = 0;

                if (data.UserType == UserType.Agent)
                {
                    AgentUserID = data.UserTypeID;
                    UserID = _UserId;
                }
                else if (data.UserType == UserType.ADMIN)
                {
                    AgentUserID = 0;
                    UserID = 0;
                }
                return _DepartureNoticeRepository.DepartureNoticeVesselDetailsforAutocomplete(AgentUserID, _PortCode,  UserID, searchvalue);
            });
        }
        #region AddDepartureNotice
        /// <summary>
        /// To Save Departure Notice 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DepartureNoticeVO AddDepartureNotice(DepartureNoticeVO servicedata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                DepartureNotice serviceObj = null;

                serviceObj = servicedata.MapToEntity();
                int userid = _accountRepository.GetUserId(_LoginName);

                serviceObj.CreatedBy = userid;
                serviceObj.CreatedDate = DateTime.Now;
                serviceObj.ModifiedBy = userid;
                serviceObj.ModifiedDate = DateTime.Now;
                serviceObj.RecordStatus = "A";
                serviceObj.ObjectState = ObjectState.Added;

                #region Workflow Integration -- Departure Notice
                string remarks = "New Departure Notice";
                DepartureNoticeWorkFlow DepartureNoticeworkflow = new DepartureNoticeWorkFlow(_unitOfWork, serviceObj, remarks);
                WorkFlowEngine<DepartureNoticeWorkFlow> wf = new WorkFlowEngine<DepartureNoticeWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(DepartureNoticeworkflow, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                #endregion

                return servicedata;
            });
        }
        #endregion

        #region ModifyDepartureNotice
        /// <summary>
        /// To Modify Departure Notice
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DepartureNoticeVO ModifyDepartureNotice(DepartureNoticeVO servicedata)
        {
            _userid = _accountRepository.GetUserId(_LoginName);
            return EncloseTransactionAndHandleException(() =>
            {
                DepartureNotice serviceObj;
                serviceObj = servicedata.MapToEntity();
                serviceObj.ObjectState = ObjectState.Modified;
                serviceObj.ModifiedBy = _userid;
                serviceObj.ModifiedDate = DateTime.Now;

                _unitOfWork.Repository<DepartureNotice>().Update(serviceObj);

                #region Workflow Integration -- Departure Notice
                string remarks = "Update Departure Notice";
                DepartureNoticeWorkFlow departureNoticeworkflow = new DepartureNoticeWorkFlow(_unitOfWork, serviceObj, remarks);
                WorkFlowEngine<DepartureNoticeWorkFlow> wf = new WorkFlowEngine<DepartureNoticeWorkFlow>(_unitOfWork, _PortCode, servicedata.ModifiedBy.GetValueOrDefault());
                wf.Process(departureNoticeworkflow, "UPDT");
                #endregion

                _unitOfWork.SaveChanges();

                servicedata = serviceObj.MapToDto();
                return servicedata;
            });
        }
        #endregion

        #region GetPendingArrivalNotifications
        /// <summary>
        /// Get Pending ArrivalNotifications to Departure Acknowledgement
        /// </summary>
        /// <param name="departureId"></param>
        /// <returns></returns>
        public List<DepartureNoticeVO> GetPendingArrivalNotifications(string DepartureID, string VCN, string VesselName, string SubmissionDateFrom, string SubmissionDateTO)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var data = _DepartureNoticeRepository.GetTerminalOperatorForUser(_UserId, _PortCode);
                int AgentUserID = 0;
                int UserID = 0;

                if (data.UserType == UserType.Agent)
                {
                    AgentUserID = data.UserTypeID;
                    UserID = _UserId;
                }
                else if (data.UserType == UserType.ADMIN)
                {
                    AgentUserID = 0;
                    UserID = 0;
                }

                return _DepartureNoticeRepository.GetPendingArrivalNotifications(AgentUserID, _PortCode, DepartureID, UserID, VCN, VesselName, SubmissionDateFrom, SubmissionDateTO);
            });
        }
        #endregion

        #region Workflow Integrated Methods
        /// <summary>
        /// To Confirm Departure Notice in pending tasks list (WorkFlow)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void AcknowledgeDepartureNotice(string DepartureNoticeID, string comments, string taskcode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                var servicedtls = _DepartureNoticeRepository.GetDepartureNoticeDetailsForWorkFlow(userid, _PortCode, DepartureNoticeID, _UserId);

                DepartureNotice serviceObj = null;
                serviceObj = servicedtls.MapToEntity();

                DepartureNoticeWorkFlow DepartureNoticeWorkFlow = new DepartureNoticeWorkFlow(_unitOfWork, serviceObj, comments);
                WorkFlowEngine<DepartureNoticeWorkFlow> wf = new WorkFlowEngine<DepartureNoticeWorkFlow>(_unitOfWork, _PortCode, userid);
                wf.Process(DepartureNoticeWorkFlow, taskcode);
            });
        }
        #endregion
    }
}
