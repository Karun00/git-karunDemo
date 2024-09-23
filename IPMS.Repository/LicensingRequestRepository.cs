using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain;

namespace IPMS.Repository
{
    public class LicensingRequestRepository : ILicensingRequestRepository
    {
        private IUnitOfWork _unitOfWork;

        public LicensingRequestRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetLicensingRequestDetailsByID
        /// <summary>
        /// To Get LicensingRequest Details based on LicenseRequestID
        /// </summary>
        /// <param name="LiCReqId"></param>
        /// <returns></returns>
        public LicenseRequest GetLicensingRequestDetailsByid(int licreqid)
        {
            var licensingRequest = (from t in _unitOfWork.Repository<LicenseRequest>().Query()
                                        .Include(t => t.Bunkerings)
                                        .Include(t => t.Divings)
                                        .Include(t => t.FireEquipments)
                                        .Include(t => t.FireProtections)
                                        .Include(t => t.FloatingCranes)
                                        .Include(t => t.LicenseRequestPorts)
                                        .Include(t => t.LicenseRequestPorts.Select(k => k.WorkflowInstance))
                                        .Include(t => t.PestControls)
                                        .Include(t => t.PollutionControls)
                                        .Include(t => t.Stevedores)
                                        .Include(t => t.BusinessAddress)
                                        .Include(t => t.PostalAddress)
                                        .Include(t => t.AuthorizedContactPerson)
                                        .Include(t => t.LicenseRequestDocuments)
                                        .Include(t => t.SubCategory).Select()
                                    where t.LicenseRequestID == licreqid
                                    orderby t.LicenseRequestID descending
                                    select t).FirstOrDefault<LicenseRequest>();
            return licensingRequest;
        }
        #endregion

        #region GetLicensingRequestDetailsByRefID
        /// <summary>
        /// Get Licensing Request Details By RefID
        /// </summary>
        /// <param name="LiCRefId"></param>
        /// <returns></returns>
        public LicenseRequest GetLicensingRequestDetailsByrefid(string licrefid)
        {
            var licensingRequest = (from t in _unitOfWork.Repository<LicenseRequest>().Query()
                                        .Include(t => t.Bunkerings)
                                        .Include(t => t.Divings)
                                        .Include(t => t.FireEquipments)
                                        .Include(t => t.FireProtections)
                                        .Include(t => t.FloatingCranes)
                                        .Include(t => t.LicenseRequestPorts)
                                        .Include(t => t.LicenseRequestPorts.Select(k => k.WorkflowInstance))
                                        .Include(t => t.PestControls)
                                        .Include(t => t.PollutionControls)
                                        .Include(t => t.Stevedores)
                                        .Include(t => t.BusinessAddress)
                                        .Include(t => t.PostalAddress)
                                        .Include(t => t.AuthorizedContactPerson)
                                        .Include(t => t.LicenseRequestDocuments).Select()
                                    where t.ReferenceNo == licrefid
                                    orderby t.LicenseRequestID descending
                                    select t).FirstOrDefault<LicenseRequest>();
            return licensingRequest;
        }
        #endregion

        #region GetLicensingRequestlist
        /// <summary>
        /// Get Licensing Request list
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public List<LicenseRequestVO> GetLicensingRequestlist(string port)
        {
            var licensingRequestlist = (from to in _unitOfWork.Repository<LicenseRequest>().Queryable()
                                        join b in _unitOfWork.Repository<LicenseRequestPort>().Queryable()
                                        on to.LicenseRequestID equals b.LicenseRequestID
                                        join c in _unitOfWork.Repository<WorkflowInstance>().Queryable()
                                        on b.WorkflowInstanceID equals c.WorkflowInstanceId
                                        join d in _unitOfWork.Repository<SubCategory>().Queryable()
                                        on to.LicenseRequestType equals d.SubCatCode
                                        where to.RecordStatus == "A" && b.PortCode == port && b.WFStatus == "WFSA"
                                        select new LicenseRequestVO
                                        {
                                            LicenseRequestID = to.LicenseRequestID,
                                            RegisteredName = to.RegisteredName,
                                            LicenseRequestType = to.LicenseRequestType,
                                            TradingName = to.TradingName,
                                            RegistrationNumber = to.RegistrationNumber,
                                            VATNumber = to.VATNumber,
                                            IncomeTaxNumber = to.IncomeTaxNumber,
                                            Workflowstatus = c.WorkflowTaskCode,
                                            LicenseRequestTypeName = d.SubCatName,
                                            SkillsDevLevyNumber = to.SkillsDevLevyNumber,
                                            BBBEEStatus = to.VerifiedBBBEEStatus,
                                            WorkflowInstanceID = b.WorkflowInstanceID
                                        }).Distinct().ToList<LicenseRequestVO>();

            return licensingRequestlist.ToList();
        }
        #endregion

        #region GetApprovedBunkers
        /// <summary>
        /// Get ApprovedBunkers
        /// </summary>
        /// <param name="port"></param>
        /// <param name="lictype"></param>
        /// <param name="wrkflowcode"></param>
        /// <returns></returns>
        public List<LicenseRequestVO> GetApprovedBunkers(string port, string lictype, string wrkflowcode)
        {
            var berths = (from to in _unitOfWork.Repository<LicenseRequest>().Queryable()
                          join b in _unitOfWork.Repository<LicenseRequestPort>().Queryable()
                          on to.LicenseRequestID equals b.LicenseRequestID
                          join c in _unitOfWork.Repository<WorkflowInstance>().Queryable()
                          on b.WorkflowInstanceID equals c.WorkflowInstanceId
                          where to.LicenseRequestType == lictype && to.RecordStatus == "A"
                          && b.PortCode == port && c.WorkflowTaskCode == wrkflowcode
                          select new LicenseRequestVO
                          {
                              LicenseRequestID = to.LicenseRequestID,
                              RegisteredName = to.RegisteredName
                          }).ToList<LicenseRequestVO>();
            return berths.ToList();
        }
        #endregion

        #region GetWasteDeclarations
        /// <summary>
        /// Get WasteDeclarations
        /// </summary>
        /// <param name="port"></param>
        /// <param name="lictype"></param>
        /// <param name="wrkflowcode"></param>
        /// <returns></returns>
        public List<LicenseRequestVO> GetWasteDeclarations(string port, string lictype, string wrkflowcode)
        {
            var licenses = (from to in _unitOfWork.Repository<LicenseRequest>().Queryable()
                          join b in _unitOfWork.Repository<LicenseRequestPort>().Queryable()
                          on to.LicenseRequestID equals b.LicenseRequestID                          
                          where to.LicenseRequestType == lictype && to.RecordStatus == "A"
                          && b.PortCode == port && b.WFStatus == wrkflowcode
                          select new LicenseRequestVO
                          {
                              LicenseRequestID = to.LicenseRequestID,
                              RegisteredName = to.RegisteredName
                          }).ToList<LicenseRequestVO>();
            return licenses.ToList();
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
            bool exists = false;
            var licensingRequest = _unitOfWork.Repository<LicenseRequest>().Queryable().Where(x => x.RecordStatus == RecordStatus.Active && x.ReferenceNo == referenceno).OrderByDescending(x => x.LicenseRequestID).Count();
            if (licensingRequest > 0)
                exists = true;
            return exists;
        }
        #endregion
    }
}


