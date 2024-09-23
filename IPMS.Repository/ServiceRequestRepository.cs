using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using System.Data.Entity;

namespace IPMS.Repository
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private IUnitOfWork _unitOfWork;      
        private IArrivalNotificationRepository _arrivalNotificationRepository;
        private IVesselAgentChangeRepository _vesselAgentChangeRepository;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository;

        public ServiceRequestRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;           
            _arrivalNotificationRepository = new ArrivalNotificationRepository(_unitOfWork);
            _vesselAgentChangeRepository = new VesselAgentChangeRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
        }

        public UserMasterVO GetTerminalOperatorForUser(int UserID, string PortCode)
        {
            var userdetails = (from user in _unitOfWork.Repository<User>().Queryable()
                               join userpt in _unitOfWork.Repository<UserPort>().Queryable()
                               on user.UserID equals userpt.UserID
                               where userpt.UserID == UserID && userpt.PortCode == PortCode
                               select new UserMasterVO
                               {
                                   UserID = user.UserID,
                                   UserType = user.UserType,
                                   UserTypeID = user.UserTypeID
                               }).FirstOrDefault<UserMasterVO>();

            return userdetails;

        }

        public List<ServiceRequestVCNDetails> GetVCNDetailsForServiceRequest(string PortCode, int AgentUserID, int ToUserID, string searchValue)
        {
            var _portcode = new SqlParameter("@p_PortCode", PortCode);
            var _agentuserid = new SqlParameter("@p_AgentUserID", AgentUserID);
            var _touserid = new SqlParameter("@p_ToUserID", ToUserID);
            var _searchvalue = new SqlParameter("@p_searchValue", searchValue);

            var vcndtls = _unitOfWork.SqlQuery<ServiceRequestVCNDetails>("dbo.usp_GetVCNDetailsForServiceRequest  @p_PortCode,@p_AgentUserID,@p_ToUserID,@p_searchValue", _portcode, _agentuserid, _touserid, _searchvalue).ToList();          
            return vcndtls;
        }


        ///////Get VCN's for Other screens

        public List<ServiceRequestVCNDetails> GetVCNDetails(string PortCode)
        {

            var vcndtls = (from vs in _unitOfWork.Repository<Vessel>().Query().Tracking(true).Select()
                           join an in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true).Select()
                           on vs.VesselID equals an.VesselID
                           join vc in _unitOfWork.Repository<VesselCall>().Query().Tracking(true).Select()
                           on an.VCN equals vc.VCN
                           where an.PortCode == PortCode && an.IsANFinal == "Y" && an.IsPHANFinal == "Y" && (an.IsIMDGANFinal == "Y" || an.IsIMDGANFinal == "NA") && (an.IsISPSANFinal == "Y" || an.IsISPSANFinal == "NA") && vc.ATUB == null
                           select new ServiceRequestVCNDetails
                           {
                               VCN = an.VCN,
                               VesselID = vs.VesselID,
                               VesselName = vs.VesselName,
                               VoyageIn = an.VoyageIn,
                               VoyageOut = an.VoyageOut,
                               ReasonForVisit = _arrivalNotificationRepository.GetArrivalReasonForVisit(an.VCN),
                               VesselType = vs.VesselType,
                               CallSign = vs.CallSign,
                               ETA = an.ETA,
                               ETD = an.ETD,
                               IMONo = vs.IMONo,
                               LengthOverallInM = vs.LengthOverallInM,
                               BeamInM = vs.BeamInM,
                               ArrDraft = an.ArrDraft,
                               VesselNationality = vs.VesselNationality,
                               GrossRegisteredTonnageInMT = vs.GrossRegisteredTonnageInMT,
                               DeadWeightTonnageInMT = vs.DeadWeightTonnageInMT,
                               LastPortOfCall = an.LastPortOfCall,
                               NextPortOfCall = an.NextPortOfCall,
                               Tidal = an.Tidal,
                               DaylightRestriction = an.DaylightRestriction,
                               AnyDangerousGoodsonBoard = an.AnyDangerousGoodsonBoard == "I" ? "Not Binded" : "Yes",
                               DangerousGoodsClass = an.DangerousGoodsClass != null ? an.DangerousGoodsClass : "Not Binded",
                               UNNo = an.UNNo != null ? an.UNNo : "Not Binded",
                               CurrentBerth = vc.ToPositionBerthCode != null ? vc.ToPositionBerthCode : "NA",
                               ETUB = vc.ETUB,
                               ETB = vc.ETB
                           }).ToList();
            return vcndtls;
        }

        /// <summary>
        /// To Get Service request details For grid binding
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<ServiceRequest_VO> GetServiceRequestDetails(string portCode, int AgentUserID, int ToUserID, int EmpID, string frmdate, string todate, string vcnSearch, string vesselName, string MovementType)
        {
            List<ServiceRequest_VO> objServiceRequest_VO = new List<ServiceRequest_VO>();
            var servicedtls = new List<ServiceRequest>();

            int agentId = _vesselAgentChangeRepository.GetAgentId(portCode, AgentUserID);

            DateTime? dtFromDate = null;
            if (frmdate != null && frmdate.Trim() != "null" && frmdate != "")
            {
                dtFromDate = Convert.ToDateTime(frmdate, CultureInfo.InvariantCulture).Date;
            }
            DateTime? dtFromTo = null;
            if (todate != null && todate.Trim() != "null" && todate != "")
            {
                dtFromTo = Convert.ToDateTime(todate, CultureInfo.InvariantCulture).AddDays(1).Date;
            }

            if (agentId != 0 || ToUserID != 0)
            {
                //DateTime MovementStarttime;
                objServiceRequest_VO = (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable()
                                        join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on sr.VCN equals an.VCN
                                        where (dtFromDate != null ? (sr.MovementDateTime) >= dtFromDate : true) &&
                                       (dtFromTo != null ? (sr.MovementDateTime) <= dtFromTo : true)
                                       && an.PortCode == portCode && sr.ArrivalNotification.VesselCalls.FirstOrDefault().RecentAgentID == agentId || sr.ArrivalNotification.TerminalOperatorID == ToUserID
                                        orderby sr.ServiceRequestID descending
                                        select new ServiceRequest_VO
                                        {
                                            WorkflowTaskCode = sr.WorkflowInstanceId != null ? sr.WorkflowInstance.WorkflowTaskCode : sr.WorkflowInstance1.WorkflowTaskCode,
                                            WorkflowInstanceTaskName = (sr.VesselCallMovements.FirstOrDefault().MovementStatus == "BERT" || sr.VesselCallMovements.FirstOrDefault().MovementStatus == "SALD") ? "Completed" : (sr.WorkflowInstanceId != null ? sr.WorkflowInstance.SubCategory.SubCatName : sr.WorkflowInstance1.SubCategory.SubCatName),
                                            SlotStatus = sr.VesselCallMovements.FirstOrDefault().SlotStatus,
                                            ServiceRequestID = sr.ServiceRequestID,
                                            VCN = sr.VCN,
                                            MovementType = sr.MovementType,
                                            MovementDate = sr.MovementDateTime,
                                            SubMovementDate = sr.MovementDateTime,                                   
                                            PreferredDate = sr.PreferredDateTime,
                                            SlotPeriod = sr.SlotPeriod,
                                            MovementSlot = sr.MovementSlot,
                                            SideAlongSideCode = sr.SideAlongSideCode,
                                            OwnSteam = sr.OwnSteam == "Y" ? true : false,
                                            NoMainEngine = sr.NoMainEngine == "Y" ? true : false,
                                            Comments = sr.Comments,
                                            WorkflowInstanceId = sr.WorkflowInstanceId,
                                            RecordStatus = sr.RecordStatus,
                                            CreatedBy = sr.CreatedBy,
                                            CreatedDate = sr.CreatedDate,
                                            ModifiedBy = sr.ModifiedBy,
                                            ModifiedDate = sr.ModifiedDate,
                                            IsFinal = sr.IsFinal,
                                            DraftFWD = sr.DraftFWD,
                                            DraftAFT = sr.DraftAFT,
                                            PassengersEmbarking = sr.PassengersEmbarking,
                                            PassengersDisembarking = sr.PassengersDisembarking,
                                            BPWorkflowInstanceId = sr.BPWorkflowInstanceId,
                                            IsTidal = sr.IsTidal == "Y" ? true : false,
                                            NextPortOfCall = an.NextPort != null ? an.NextPort.PortName : "",
                                            LastPortOfCall = an.LastPort != null ? an.LastPort.PortName : "",
                                            VesselName = an.Vessel.VesselName,
                                            VesselType = an.Vessel.SubCategory3.SubCatName,
                                            VesselNationality = an.Vessel.SubCategory2.SubCatName,
                                            MovementName = sr.SubCategory.SubCatName,
                                            ETA = an.VesselCalls.FirstOrDefault().ETA,
                                            ETD = an.VesselCalls.FirstOrDefault().ETD,
                                            SubmittedDateTime = sr.CreatedDate,
                                            UserName = sr.User != null ? sr.User.FirstName + " " + sr.User.LastName : "",
                                            ContactNo = sr.User != null ? sr.User.ContactNo : "",
                                            EmailID = sr.User != null ? sr.User.EmailID : "",
                                            ServiceRequestDocuments = (from srDoc in sr.ServiceRequestDocuments
                                                                       select new ServiceRequestDocumentVO
                                                                       {
                                                                           DocumentID = srDoc.DocumentID,
                                                                           CreatedBy = srDoc.CreatedBy,
                                                                           CreatedDate = srDoc.CreatedDate,
                                                                           DocumentCode = srDoc.DocumentCode,
                                                                           DocumentName = srDoc.Document != null ? srDoc.Document.SubCategory.SubCatName : null,
                                                                           FileName = srDoc.Document.FileName,
                                                                           ModifiedBy = srDoc.ModifiedBy,
                                                                           ModifiedDate = srDoc.ModifiedDate,
                                                                           RecordStatus = srDoc.RecordStatus,
                                                                           ServiceRequestDocumentID = srDoc.ServiceRequestDocumentID,
                                                                           ServiceRequestID = srDoc.ServiceRequestID
                                                                       }).ToList(),

                                            Agent = new AgentVO
                                            {
                                                AgentID = an.Agent.AgentID,
                                                RegisteredName = an.Agent.RegisteredName,
                                                TelephoneNo1 = an.Agent.TelephoneNo1,
                                                FaxNo = an.Agent.FaxNo
                                            },

                                            AuthorizedContactPerson = new AuthorizedContactPersonVO
                                            {
                                                AuthorizedContactPersonID = an.Agent.AuthorizedContactPerson.AuthorizedContactPersonID,
                                                FirstName = an.Agent.AuthorizedContactPerson.FirstName,
                                                SurName = an.Agent.AuthorizedContactPerson.SurName,
                                                CellularNo = an.Agent.AuthorizedContactPerson.CellularNo,
                                                EmailID = an.Agent.AuthorizedContactPerson.EmailID
                                            },

                                            ServiceRequestShifting = (from srShifting in sr.ServiceRequestShiftings
                                                                      select new ServiceRequestShiftingVO
                                                                      {
                                                                          ServiceRequestShiftingID = srShifting.ServiceRequestShiftingID,
                                                                          ServiceRequestID = srShifting.ServiceRequestID,
                                                                          ToPortCode = srShifting.ToPortCode,
                                                                          ToQuayCode = srShifting.ToQuayCode,
                                                                          ToBerthCode = srShifting.ToBerthCode,
                                                                          FromPositionPortCode = srShifting.FromPositionPortCode,
                                                                          FromPositionQuayCode = srShifting.FromPositionQuayCode,
                                                                          FromPositionBerthCode = srShifting.FromPositionBerthCode,
                                                                          FromPositionBollardCode = srShifting.FromPositionBollardCode,
                                                                          ToPositionPortCode = srShifting.ToPositionPortCode,
                                                                          ToPositionQuayCode = srShifting.ToPositionQuayCode,
                                                                          ToPositionBerthCode = srShifting.ToPositionBerthCode,
                                                                          ToPositionBollardCode = srShifting.ToPositionBollardCode,
                                                                          DraftFWD = srShifting.DraftFWD,
                                                                          DraftAFT = srShifting.DraftAFT,
                                                                          RecordStatus = srShifting.RecordStatus,
                                                                          CreatedBy = srShifting.CreatedBy,
                                                                          CreatedDate = srShifting.CreatedDate,
                                                                          ModifiedBy = srShifting.ModifiedBy,
                                                                          ModifiedDate = srShifting.ModifiedDate,
                                                                          BerthKey = srShifting.ToPortCode + "." + srShifting.ToQuayCode + "." + srShifting.ToBerthCode
                                                                      }).FirstOrDefault(),

                                            ServiceRequestWarping = (from srWarping in sr.ServiceRequestWarpings
                                                                     select new ServiceRequestWarpingVO
                                                                     {
                                                                         ServiceRequestWarpingID = srWarping.ServiceRequestWarpingID,
                                                                         ServiceRequestID = srWarping.ServiceRequestID,
                                                                         FromPositionPortCode = srWarping.FromPositionPortCode,
                                                                         FromPositionQuayCode = srWarping.FromPositionQuayCode,
                                                                         FromPositionBerthCode = srWarping.FromPositionBerthCode,
                                                                         FromPositionBollardCode = srWarping.FromPositionBollardCode,
                                                                         ToPositionPortCode = srWarping.ToPositionPortCode,
                                                                         ToPositionQuayCode = srWarping.ToPositionQuayCode,
                                                                         ToPositionBerthCode = srWarping.ToPositionBerthCode,
                                                                         ToPositionBollardCode = srWarping.ToPositionBollardCode,
                                                                         RecordStatus = srWarping.RecordStatus,
                                                                         CreatedBy = srWarping.CreatedBy,
                                                                         CreatedDate = srWarping.CreatedDate,
                                                                         ModifiedBy = srWarping.ModifiedBy,
                                                                         ModifiedDate = srWarping.ModifiedDate,
                                                                         Warp = srWarping.Warp,
                                                                         WarpDistance = srWarping.WarpDistance
                                                                     }).FirstOrDefault(),

                                            ServiceRequestSailing = (from srSailing in sr.ServiceRequestSailings
                                                                     select new ServiceRequestSailingVO
                                                                     {
                                                                         CreatedBy = srSailing.CreatedBy,
                                                                         CreatedDate = srSailing.CreatedDate,
                                                                         ModifiedBy = srSailing.ModifiedBy,
                                                                         ModifiedDate = srSailing.ModifiedDate,
                                                                         RecordStatus = srSailing.RecordStatus,
                                                                         ServiceRequestID = srSailing.ServiceRequestID,
                                                                         ServiceRequestSailingID = srSailing.ServiceRequestSailingID,
                                                                         DocumentID = srSailing.DocumentID,
                                                                         MarineRevenueCleared = srSailing.MarineRevenueCleared == "Y" ? true : false,                                                                        
                                                                     }).FirstOrDefault(),

                                            ArrivalNotification = new ArrivalNotificationVO
                                            {
                                                VCN = an.VCN,
                                                ArrDraft = an.ArrDraft,
                                                DepDraft = an.DepDraft,
                                                Tidal = an.Tidal,
                                                DaylightRestriction = an.DaylightRestriction,
                                                VoyageIn = an.VoyageIn,
                                                PilotExemption = an.PilotExemption,
                                                IsSpecialNature = an.IsSpecialNature,
                                                AnyDangerousGoodsonBoard = an.AnyDangerousGoodsonBoard,

                                                Vessel = new VesselVO
                                                {
                                                    VesselID = an.Vessel.VesselID,
                                                    VesselName = an.Vessel.VesselName,
                                                    IMONo = an.Vessel.IMONo,
                                                    LengthOverallInM = an.Vessel.LengthOverallInM,
                                                    BeamInM = an.Vessel.BeamInM,
                                                    GrossRegisteredTonnageInMT = an.Vessel.GrossRegisteredTonnageInMT,
                                                    DeadWeightTonnageInMT = an.Vessel.DeadWeightTonnageInMT,
                                                    VesselType = an.Vessel.VesselType,
                                                    CallSign = an.Vessel.CallSign
                                                },

                                                VesselCalls = (from vslCall in an.VesselCalls
                                                               select new VesselCallVO
                                                               {
                                                                   VesselCallID = vslCall.VesselCallID,
                                                                   ATA = vslCall.ATA != null ? vslCall.ATA.ToString() : null,
                                                                   ATB = vslCall.ATB
                                                               }).ToList(),

                                                ArrivalReasons = (from rv in an.ArrivalReasons
                                                                  select new ArrivalReasonVO
                                                                  {
                                                                      Reason = rv.SubCategory.SubCatName,
                                                                      ReasonCode = rv.Reason
                                                                  }).ToList()
                                            }
                                        }).ToList();
            }
            else
            {
                objServiceRequest_VO = (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable()
                                        join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on sr.VCN equals an.VCN
                                        where (dtFromDate != null ? (sr.MovementDateTime) >= dtFromDate : true) &&
                                       (dtFromTo != null ? (sr.MovementDateTime) <= dtFromTo : true)
                                       && an.PortCode == portCode
                                        orderby sr.ServiceRequestID descending
                                        select new ServiceRequest_VO
                                        {
                                            WorkflowTaskCode = sr.WorkflowInstanceId != null ? sr.WorkflowInstance.WorkflowTaskCode : sr.WorkflowInstance1.WorkflowTaskCode,
                                            WorkflowInstanceTaskName = (sr.VesselCallMovements.FirstOrDefault().MovementStatus == "BERT" || sr.VesselCallMovements.FirstOrDefault().MovementStatus == "SALD") ? "Completed" : (sr.WorkflowInstanceId != null ? sr.WorkflowInstance.SubCategory.SubCatName : sr.WorkflowInstance1.SubCategory.SubCatName),
                                            SlotStatus = sr.VesselCallMovements.FirstOrDefault().SlotStatus,
                                            ServiceRequestID = sr.ServiceRequestID,
                                            VCN = sr.VCN,
                                            MovementType = sr.MovementType,
                                            MovementDate = sr.MovementDateTime,
                                            SubMovementDate = sr.MovementDateTime,     
                                            PreferredDate = sr.PreferredDateTime,
                                            SlotPeriod = sr.SlotPeriod,
                                            MovementSlot = sr.MovementSlot,
                                            SideAlongSideCode = sr.SideAlongSideCode,
                                            OwnSteam = sr.OwnSteam == "Y" ? true : false,
                                            NoMainEngine = sr.NoMainEngine == "Y" ? true : false,
                                            Comments = sr.Comments,
                                            WorkflowInstanceId = sr.WorkflowInstanceId,
                                            RecordStatus = sr.RecordStatus,
                                            CreatedBy = sr.CreatedBy,
                                            CreatedDate = sr.CreatedDate,
                                            ModifiedBy = sr.ModifiedBy,
                                            ModifiedDate = sr.ModifiedDate,
                                            IsFinal = sr.IsFinal,
                                            DraftFWD = sr.DraftFWD,
                                            DraftAFT = sr.DraftAFT,
                                            PassengersEmbarking = sr.PassengersEmbarking,
                                            PassengersDisembarking = sr.PassengersDisembarking,
                                            BPWorkflowInstanceId = sr.BPWorkflowInstanceId,
                                            IsTidal = sr.IsTidal == "Y" ? true : false,
                                            NextPortOfCall = an.NextPort != null ? an.NextPort.PortName : "",
                                            LastPortOfCall = an.LastPort != null ? an.LastPort.PortName : "",
                                            VesselName = an.Vessel.VesselName,
                                            VesselType = an.Vessel.SubCategory3.SubCatName,
                                            VesselNationality = an.Vessel.SubCategory2.SubCatName,
                                            MovementName = sr.SubCategory.SubCatName,
                                            ETA = an.VesselCalls.FirstOrDefault().ETA,
                                            ETD = an.VesselCalls.FirstOrDefault().ETD,
                                            SubmittedDateTime = sr.CreatedDate,
                                            UserName = sr.User != null ? sr.User.FirstName + " " + sr.User.LastName : "",
                                            ContactNo = sr.User != null ? sr.User.ContactNo : "",
                                            EmailID = sr.User != null ? sr.User.EmailID : "",
                                            ServiceRequestDocuments = (from srDoc in sr.ServiceRequestDocuments
                                                                       select new ServiceRequestDocumentVO
                                                                       {
                                                                           DocumentID = srDoc.DocumentID,
                                                                           CreatedBy = srDoc.CreatedBy,
                                                                           CreatedDate = srDoc.CreatedDate,
                                                                           DocumentCode = srDoc.DocumentCode,
                                                                           DocumentName = srDoc.Document != null ? srDoc.Document.SubCategory.SubCatName : null,
                                                                           FileName = srDoc.Document.FileName,
                                                                           ModifiedBy = srDoc.ModifiedBy,
                                                                           ModifiedDate = srDoc.ModifiedDate,
                                                                           RecordStatus = srDoc.RecordStatus,
                                                                           ServiceRequestDocumentID = srDoc.ServiceRequestDocumentID,
                                                                           ServiceRequestID = srDoc.ServiceRequestID
                                                                       }).ToList(),

                                            Agent = new AgentVO
                                            {
                                                AgentID = an.Agent.AgentID,
                                                RegisteredName = an.Agent.RegisteredName,
                                                TelephoneNo1 = an.Agent.TelephoneNo1,
                                                FaxNo = an.Agent.FaxNo
                                            },

                                            AuthorizedContactPerson = new AuthorizedContactPersonVO
                                            {
                                                AuthorizedContactPersonID = an.Agent.AuthorizedContactPerson.AuthorizedContactPersonID,
                                                FirstName = an.Agent.AuthorizedContactPerson.FirstName,
                                                SurName = an.Agent.AuthorizedContactPerson.SurName,
                                                CellularNo = an.Agent.AuthorizedContactPerson.CellularNo,
                                                EmailID = an.Agent.AuthorizedContactPerson.EmailID
                                            },

                                            ServiceRequestShifting = (from srShifting in sr.ServiceRequestShiftings
                                                                      select new ServiceRequestShiftingVO
                                                                      {
                                                                          ServiceRequestShiftingID = srShifting.ServiceRequestShiftingID,
                                                                          ServiceRequestID = srShifting.ServiceRequestID,
                                                                          ToPortCode = srShifting.ToPortCode,
                                                                          ToQuayCode = srShifting.ToQuayCode,
                                                                          ToBerthCode = srShifting.ToBerthCode,
                                                                          FromPositionPortCode = srShifting.FromPositionPortCode,
                                                                          FromPositionQuayCode = srShifting.FromPositionQuayCode,
                                                                          FromPositionBerthCode = srShifting.FromPositionBerthCode,
                                                                          FromPositionBollardCode = srShifting.FromPositionBollardCode,
                                                                          ToPositionPortCode = srShifting.ToPositionPortCode,
                                                                          ToPositionQuayCode = srShifting.ToPositionQuayCode,
                                                                          ToPositionBerthCode = srShifting.ToPositionBerthCode,
                                                                          ToPositionBollardCode = srShifting.ToPositionBollardCode,
                                                                          DraftFWD = srShifting.DraftFWD,
                                                                          DraftAFT = srShifting.DraftAFT,
                                                                          RecordStatus = srShifting.RecordStatus,
                                                                          CreatedBy = srShifting.CreatedBy,
                                                                          CreatedDate = srShifting.CreatedDate,
                                                                          ModifiedBy = srShifting.ModifiedBy,
                                                                          ModifiedDate = srShifting.ModifiedDate,
                                                                          BerthKey = srShifting.ToPortCode + "." + srShifting.ToQuayCode + "." + srShifting.ToBerthCode
                                                                      }).FirstOrDefault(),

                                            ServiceRequestWarping = (from srWarping in sr.ServiceRequestWarpings
                                                                     select new ServiceRequestWarpingVO
                                                                     {
                                                                         ServiceRequestWarpingID = srWarping.ServiceRequestWarpingID,
                                                                         ServiceRequestID = srWarping.ServiceRequestID,
                                                                         FromPositionPortCode = srWarping.FromPositionPortCode,
                                                                         FromPositionQuayCode = srWarping.FromPositionQuayCode,
                                                                         FromPositionBerthCode = srWarping.FromPositionBerthCode,
                                                                         FromPositionBollardCode = srWarping.FromPositionBollardCode,
                                                                         ToPositionPortCode = srWarping.ToPositionPortCode,
                                                                         ToPositionQuayCode = srWarping.ToPositionQuayCode,
                                                                         ToPositionBerthCode = srWarping.ToPositionBerthCode,
                                                                         ToPositionBollardCode = srWarping.ToPositionBollardCode,
                                                                         RecordStatus = srWarping.RecordStatus,
                                                                         CreatedBy = srWarping.CreatedBy,
                                                                         CreatedDate = srWarping.CreatedDate,
                                                                         ModifiedBy = srWarping.ModifiedBy,
                                                                         ModifiedDate = srWarping.ModifiedDate,
                                                                         Warp = srWarping.Warp,
                                                                         WarpDistance = srWarping.WarpDistance
                                                                     }).FirstOrDefault(),

                                            ServiceRequestSailing = (from srSailing in sr.ServiceRequestSailings
                                                                     select new ServiceRequestSailingVO
                                                                     {
                                                                         CreatedBy = srSailing.CreatedBy,
                                                                         CreatedDate = srSailing.CreatedDate,
                                                                         ModifiedBy = srSailing.ModifiedBy,
                                                                         ModifiedDate = srSailing.ModifiedDate,
                                                                         RecordStatus = srSailing.RecordStatus,
                                                                         ServiceRequestID = srSailing.ServiceRequestID,
                                                                         ServiceRequestSailingID = srSailing.ServiceRequestSailingID,
                                                                         DocumentID = srSailing.DocumentID,
                                                                         MarineRevenueCleared = srSailing.MarineRevenueCleared == "Y" ? true : false,                                                                        
                                                                     }).FirstOrDefault(),

                                            ArrivalNotification = new ArrivalNotificationVO
                                            {
                                                VCN = an.VCN,
                                                ArrDraft = an.ArrDraft,
                                                DepDraft = an.DepDraft,
                                                Tidal = an.Tidal,
                                                DaylightRestriction = an.DaylightRestriction,
                                                VoyageIn = an.VoyageIn,
                                                PilotExemption = an.PilotExemption,
                                                IsSpecialNature = an.IsSpecialNature,
                                                AnyDangerousGoodsonBoard = an.AnyDangerousGoodsonBoard,

                                                Vessel = new VesselVO
                                                {
                                                    VesselID = an.Vessel.VesselID,
                                                    VesselName = an.Vessel.VesselName,
                                                    IMONo = an.Vessel.IMONo,
                                                    LengthOverallInM = an.Vessel.LengthOverallInM,
                                                    BeamInM = an.Vessel.BeamInM,
                                                    GrossRegisteredTonnageInMT = an.Vessel.GrossRegisteredTonnageInMT,
                                                    DeadWeightTonnageInMT = an.Vessel.DeadWeightTonnageInMT,
                                                    VesselType = an.Vessel.VesselType,
                                                    CallSign = an.Vessel.CallSign
                                                },

                                                VesselCalls = (from vslCall in an.VesselCalls
                                                               select new VesselCallVO
                                                               {
                                                                   VesselCallID = vslCall.VesselCallID,
                                                                   ATA = vslCall.ATA != null ? vslCall.ATA.ToString() : null,
                                                                   ATB = vslCall.ATB
                                                               }).ToList(),

                                                ArrivalReasons = (from rv in an.ArrivalReasons
                                                                  select new ArrivalReasonVO
                                                                  {
                                                                      Reason = rv.SubCategory.SubCatName,
                                                                      ReasonCode = rv.Reason
                                                                  }).ToList()
                                            }
                                        }).ToList();
            }

            foreach (ServiceRequest_VO s in objServiceRequest_VO)
            {
               // s.SubMovementDate = s.MovementDate.AddMinutes(-10);
                s.MovementDate = s.MovementDate.AddMinutes(-10);
                
                //s.MovementDateTime = Convert.ToString(s.MovementDate.AddMinutes(-10));
                s.MovementDateTime = s.MovementDate.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
                s.ReasonForVisit = s.ArrivalNotification.ArrivalReasons.Select(k => k.Reason).ToList();                
                s.PreferredDateTime = Convert.ToString(s.PreferredDate, CultureInfo.InvariantCulture);
                
                var reasons = s.ArrivalNotification.ArrivalReasons.Select(k => k.ReasonCode).ToList();
                               
                foreach (var item in reasons)
                {
                    if (s.Reasons == null)
                        s.Reasons = item;
                    else
                        s.Reasons = s.Reasons + ',' + item;
                }

                if (s.ServiceRequestSailing != null)
                {
                    var serviceRequestID = s.ServiceRequestID;
                    DocumentVO serviceObj = new DocumentVO();
                    serviceObj = (from sa in _unitOfWork.Repository<ServiceRequestSailing>().Queryable().Where(t => t.ServiceRequestID == serviceRequestID)
                                                                       join doc in _unitOfWork.Repository<Document>().Queryable() on 
                                                                       sa.DocumentID equals doc.DocumentID
                                                                      select new DocumentVO
                                                                      {
                                                                          DocumentID = doc.DocumentID,
                                                                          DocumentName = doc.DocumentName,
                                                                           FileName = doc.FileName
                                                                      }).FirstOrDefault();

                        s.ServiceRequestSailing.ServiceRequestDocument = serviceObj;
                }
            }


            if (vcnSearch != "All")
                objServiceRequest_VO = objServiceRequest_VO.FindAll(t => t.ArrivalNotification.VCN.ToUpperInvariant().Contains(vcnSearch.ToUpperInvariant()));

            if (vesselName != "All")
                objServiceRequest_VO = objServiceRequest_VO.FindAll(t => t.ArrivalNotification.Vessel.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));

            if (MovementType != "All")
                objServiceRequest_VO = objServiceRequest_VO.FindAll(t => t.MovementType.ToUpperInvariant().Contains(MovementType.ToUpperInvariant()));

            return objServiceRequest_VO;
        }

        /// <summary>
        /// To Get CurrentBerth and Bollard details of VCN
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<ServiceRequestCureentBerthnBollards> GetCurrentBerthAndBollards(string vcn, string PortCode)
        {

            var portcode = new SqlParameter("@p_PortCode", PortCode);
            var VCN = new SqlParameter("@p_vcn", vcn);

            var currentberthsnbollards = _unitOfWork.SqlQuery<ServiceRequestCureentBerthnBollards>("dbo.usp_GetCurrentBerthAndBollards @p_vcn, @p_PortCode", VCN, portcode).ToList();

            return currentberthsnbollards;          
  
        }

        /// <summary>
        /// To Get Bollards at respected Berth for Raised service request
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<Bollard> GetBollardAtBerth(string BerthCode, string PortCode)
        {
            var berthbollards = (from t in _unitOfWork.Repository<Bollard>().Queryable()
                                 where t.BerthCode == BerthCode && t.PortCode == PortCode
                                 select t).OrderBy(x=>x.BollardName).ToList<Bollard>();
            return berthbollards;
        }

        /// <summary>
        /// To Get Service request details based on Service requestid for Pending tasks view
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<ServiceRequest_VO> GetServiceRequest(string serviceid)
        {
            if (serviceid != null)
            {
                string[] srid = serviceid.Split('x');
                serviceid = srid[0];
            }
            int intserviceid = int.Parse(serviceid);
            List<ServiceRequest_VO> servicedtls = new List<ServiceRequest_VO>();

            servicedtls = (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable()
                           join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on sr.VCN equals an.VCN
                           where sr.ServiceRequestID == intserviceid
                           select new ServiceRequest_VO
                           {
                               WorkflowTaskCode = sr.WorkflowInstanceId != null ? sr.WorkflowInstance.WorkflowTaskCode : sr.WorkflowInstance1.WorkflowTaskCode,
                               WorkflowInstanceTaskName = (sr.VesselCallMovements.FirstOrDefault().MovementStatus == "BERT" || sr.VesselCallMovements.FirstOrDefault().MovementStatus == "SALD") ? "Completed" : (sr.WorkflowInstanceId != null ? sr.WorkflowInstance.SubCategory.SubCatName : sr.WorkflowInstance1.SubCategory.SubCatName),
                               ServiceRequestID = sr.ServiceRequestID,
                               VCN = sr.VCN,
                               MovementType = sr.MovementType,
                               MovementDate = sr.MovementDateTime,
                               PreferredDate = sr.PreferredDateTime,
                               SlotPeriod = sr.SlotPeriod,
                               MovementSlot = sr.MovementSlot,
                               SideAlongSideCode = sr.SideAlongSideCode,
                               OwnSteam = sr.OwnSteam == "Y" ? true : false,
                               NoMainEngine = sr.NoMainEngine == "Y" ? true : false,
                               Comments = sr.Comments,
                               WorkflowInstanceId = sr.WorkflowInstanceId,
                               RecordStatus = sr.RecordStatus,
                               CreatedBy = sr.CreatedBy,
                               CreatedDate = sr.CreatedDate,
                               ModifiedBy = sr.ModifiedBy,
                               ModifiedDate = sr.ModifiedDate,
                               IsFinal = sr.IsFinal,
                               DraftFWD = sr.DraftFWD,
                               DraftAFT = sr.DraftAFT,
                               PassengersEmbarking = sr.PassengersEmbarking,
                               PassengersDisembarking = sr.PassengersDisembarking,
                               BPWorkflowInstanceId = sr.BPWorkflowInstanceId,
                               IsTidal = sr.IsTidal == "Y" ? true : false,
                               NextPortOfCall = an.NextPort != null ? an.NextPort.PortName : "",
                               LastPortOfCall = an.LastPort != null ? an.LastPort.PortName : "",
                               VesselName = an.Vessel.VesselName,
                               VesselType = an.Vessel.SubCategory3.SubCatName,
                               VesselNationality = an.Vessel.SubCategory2.SubCatName,
                               MovementName = sr.SubCategory.SubCatName,
                               ETA = an.VesselCalls.FirstOrDefault().ETA,
                               ETD = an.VesselCalls.FirstOrDefault().ETD,
                               SubmittedDateTime = sr.CreatedDate,
                               UserName = sr.User != null ? sr.User.FirstName + " " + sr.User.LastName : "",
                               ContactNo = sr.User != null ? sr.User.ContactNo : "",
                               EmailID = sr.User != null ? sr.User.EmailID : "",
                               ServiceRequestDocuments = (from srDoc in sr.ServiceRequestDocuments
                                                          select new ServiceRequestDocumentVO
                                                          {
                                                              DocumentID = srDoc.DocumentID,
                                                              CreatedBy = srDoc.CreatedBy,
                                                              CreatedDate = srDoc.CreatedDate,
                                                              DocumentCode = srDoc.DocumentCode,
                                                              DocumentName = srDoc.Document != null ? srDoc.Document.SubCategory.SubCatName : null,
                                                              FileName = srDoc.Document.FileName,
                                                              ModifiedBy = srDoc.ModifiedBy,
                                                              ModifiedDate = srDoc.ModifiedDate,
                                                              RecordStatus = srDoc.RecordStatus,
                                                              ServiceRequestDocumentID = srDoc.ServiceRequestDocumentID,
                                                              ServiceRequestID = srDoc.ServiceRequestID
                                                          }).ToList(),

                               Agent = new AgentVO
                               {
                                   AgentID = an.Agent.AgentID,
                                   RegisteredName = an.Agent.RegisteredName,
                                   TelephoneNo1 = an.Agent.TelephoneNo1,
                                   FaxNo = an.Agent.FaxNo
                               },

                               AuthorizedContactPerson = new AuthorizedContactPersonVO
                               {
                                   AuthorizedContactPersonID = an.Agent.AuthorizedContactPerson.AuthorizedContactPersonID,
                                   FirstName = an.Agent.AuthorizedContactPerson.FirstName,
                                   SurName = an.Agent.AuthorizedContactPerson.SurName,
                                   CellularNo = an.Agent.AuthorizedContactPerson.CellularNo,
                                   EmailID = an.Agent.AuthorizedContactPerson.EmailID
                               },

                               ServiceRequestShifting = (from srShifting in sr.ServiceRequestShiftings
                                                         select new ServiceRequestShiftingVO
                                                         {
                                                             ServiceRequestShiftingID = srShifting.ServiceRequestShiftingID,
                                                             ServiceRequestID = srShifting.ServiceRequestID,
                                                             ToPortCode = srShifting.ToPortCode,
                                                             ToQuayCode = srShifting.ToQuayCode,
                                                             ToBerthCode = srShifting.ToBerthCode,
                                                             FromPositionPortCode = srShifting.FromPositionPortCode,
                                                             FromPositionQuayCode = srShifting.FromPositionQuayCode,
                                                             FromPositionBerthCode = srShifting.FromPositionBerthCode,
                                                             FromPositionBollardCode = srShifting.FromPositionBollardCode,
                                                             ToPositionPortCode = srShifting.ToPositionPortCode,
                                                             ToPositionQuayCode = srShifting.ToPositionQuayCode,
                                                             ToPositionBerthCode = srShifting.ToPositionBerthCode,
                                                             ToPositionBollardCode = srShifting.ToPositionBollardCode,
                                                             DraftFWD = srShifting.DraftFWD,
                                                             DraftAFT = srShifting.DraftAFT,
                                                             RecordStatus = srShifting.RecordStatus,
                                                             CreatedBy = srShifting.CreatedBy,
                                                             CreatedDate = srShifting.CreatedDate,
                                                             ModifiedBy = srShifting.ModifiedBy,
                                                             ModifiedDate = srShifting.ModifiedDate,
                                                             BerthKey = srShifting.ToPortCode + "." + srShifting.ToQuayCode + "." + srShifting.ToBerthCode
                                                         }).FirstOrDefault(),

                               ServiceRequestWarping = (from srWarping in sr.ServiceRequestWarpings
                                                        select new ServiceRequestWarpingVO
                                                        {
                                                            ServiceRequestWarpingID = srWarping.ServiceRequestWarpingID,
                                                            ServiceRequestID = srWarping.ServiceRequestID,
                                                            FromPositionPortCode = srWarping.FromPositionPortCode,
                                                            FromPositionQuayCode = srWarping.FromPositionQuayCode,
                                                            FromPositionBerthCode = srWarping.FromPositionBerthCode,
                                                            FromPositionBollardCode = srWarping.FromPositionBollardCode,
                                                            ToPositionPortCode = srWarping.ToPositionPortCode,
                                                            ToPositionQuayCode = srWarping.ToPositionQuayCode,
                                                            ToPositionBerthCode = srWarping.ToPositionBerthCode,
                                                            ToPositionBollardCode = srWarping.ToPositionBollardCode,
                                                            RecordStatus = srWarping.RecordStatus,
                                                            CreatedBy = srWarping.CreatedBy,
                                                            CreatedDate = srWarping.CreatedDate,
                                                            ModifiedBy = srWarping.ModifiedBy,
                                                            ModifiedDate = srWarping.ModifiedDate,
                                                            Warp = srWarping.Warp,
                                                            WarpDistance = srWarping.WarpDistance
                                                        }).FirstOrDefault(),

                               ServiceRequestSailing = (from srSailing in sr.ServiceRequestSailings
                                                        select new ServiceRequestSailingVO
                                                        {
                                                            CreatedBy = srSailing.CreatedBy,
                                                            CreatedDate = srSailing.CreatedDate,
                                                            ModifiedBy = srSailing.ModifiedBy,
                                                            ModifiedDate = srSailing.ModifiedDate,
                                                            RecordStatus = srSailing.RecordStatus,
                                                            ServiceRequestID = srSailing.ServiceRequestID,
                                                            ServiceRequestSailingID = srSailing.ServiceRequestSailingID,
                                                            DocumentID = srSailing.DocumentID,
                                                            MarineRevenueCleared = srSailing.MarineRevenueCleared == "Y" ? true : false,                                                            
                                                        }).FirstOrDefault(),

                               ArrivalNotification = new ArrivalNotificationVO
                               {
                                   VCN = an.VCN,
                                   ArrDraft = an.ArrDraft,
                                   DepDraft = an.DepDraft,
                                   Tidal = an.Tidal,
                                   DaylightRestriction = an.DaylightRestriction,
                                   VoyageIn = an.VoyageIn,
                                   PilotExemption = an.PilotExemption,
                                   IsSpecialNature = an.IsSpecialNature,
                                   AnyDangerousGoodsonBoard = an.AnyDangerousGoodsonBoard,

                                   Vessel = new VesselVO
                                   {
                                       VesselID = an.Vessel.VesselID,
                                       VesselName = an.Vessel.VesselName,
                                       IMONo = an.Vessel.IMONo,
                                       LengthOverallInM = an.Vessel.LengthOverallInM,
                                       BeamInM = an.Vessel.BeamInM,
                                       GrossRegisteredTonnageInMT = an.Vessel.GrossRegisteredTonnageInMT,
                                       DeadWeightTonnageInMT = an.Vessel.DeadWeightTonnageInMT,
                                       VesselType = an.Vessel.VesselType,
                                       CallSign = an.Vessel.CallSign
                                   },

                                   VesselCalls = (from vslCall in an.VesselCalls
                                                  select new VesselCallVO
                                                  {
                                                      VesselCallID = vslCall.VesselCallID,
                                                      ATA = vslCall.ATA != null ? vslCall.ATA.ToString() : null,
                                                      ATB = vslCall.ATB
                                                  }).ToList(),

                                   ArrivalReasons = (from rv in an.ArrivalReasons
                                                     select new ArrivalReasonVO
                                                     {
                                                         Reason = rv.SubCategory.SubCatName,
                                                           ReasonCode = rv.Reason
                                                     }).ToList()
                               }
                           }).ToList();

            servicedtls.FirstOrDefault().MovementDateTime = servicedtls.FirstOrDefault().MovementDate.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
            servicedtls.FirstOrDefault().ReasonForVisit = servicedtls.FirstOrDefault().ArrivalNotification.ArrivalReasons.Select(k => k.Reason).ToList();                  
            servicedtls.FirstOrDefault().PreferredDateTime = Convert.ToString(servicedtls.FirstOrDefault().PreferredDate, CultureInfo.InvariantCulture);

            var reasons = servicedtls.FirstOrDefault().ArrivalNotification.ArrivalReasons.Select(k => k.ReasonCode).ToList();

            foreach (var item in reasons)
            {
                if (servicedtls.FirstOrDefault().Reasons == null)
                    servicedtls.FirstOrDefault().Reasons = item;
                else
                    servicedtls.FirstOrDefault().Reasons = servicedtls.FirstOrDefault().Reasons + ',' + item;
            }


            if (servicedtls != null)
            {
                foreach (ServiceRequest_VO s in servicedtls)
                {
                    if (s.ServiceRequestSailing != null)
                    {
                        var serviceRequestID = s.ServiceRequestID;
                        DocumentVO serviceObj = new DocumentVO();
                        serviceObj =
                            (from sa in
                                _unitOfWork.Repository<ServiceRequestSailing>()
                                    .Queryable()
                                    .Where(t => t.ServiceRequestID == serviceRequestID)
                                join doc in _unitOfWork.Repository<Document>().Queryable() on
                                    sa.DocumentID equals doc.DocumentID
                                select new DocumentVO
                                {
                                    DocumentID = doc.DocumentID,
                                    DocumentName = doc.DocumentName,
                                    FileName = doc.FileName
                                }).FirstOrDefault();

                        s.ServiceRequestSailing.ServiceRequestDocument = serviceObj;
                    }
                }
            }

            return servicedtls;
        }

        /// <summary>
        /// To Get Service request details based on Service requestid for Electronic Notifications
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ServiceRequest_VO GetServiceRequestByID(string value)
        {
            int serviceRequestId = Int32.Parse(value, CultureInfo.InvariantCulture);
            var pServiceRequestId = new SqlParameter("@ServiceRequestID", serviceRequestId);

            //TODO : Due to performance issue LINQ query converted to Stored Procedure
            //Redmine #50141
            var serviceRequestDetails =
                _unitOfWork.SqlQuery<ServiceRequest_VO>("dbo.usp_GetServiceRequestByID @ServiceRequestID", pServiceRequestId)
                    .FirstOrDefault<ServiceRequest_VO>();
            return serviceRequestDetails;
        }


        /// <summary>
        /// To Get Arrival Notification details based on VCN for Electronic Notifications
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ArrivalNotificationDetails GetArrivalNotificationByID(string value)
        {
            var arrivalDetails = (from a in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true).Select().AsEnumerable<ArrivalNotification>()
                                  join v in _unitOfWork.Repository<Vessel>().Query().Tracking(true).Select().AsEnumerable<Vessel>()
                                  on a.VesselID equals v.VesselID
                                  join pb in _unitOfWork.Repository<Berth>().Query().Tracking(true).Select().AsEnumerable<Berth>()
                                  on new { pa = a.PreferredPortCode, pb = a.PreferredQuayCode, pc = a.PreferredBerthCode }
                                  equals new { pa = pb.PortCode, pb = pb.QuayCode, pc = pb.BerthCode }
                                  join ab in _unitOfWork.Repository<Berth>().Query().Tracking(true).Select().AsEnumerable<Berth>()
                                  on new { aa = a.AlternatePortCode, bb = a.AlternateQuayCode, cc = a.AlternateBerthCode }
                                  equals new { aa = ab.PortCode, bb = ab.QuayCode, cc = ab.BerthCode }
                                  where a.VCN == value
                                  select new ArrivalNotificationDetails
                                  {
                                      PortCode = a.PortCode,
                                      VesselName = v.VesselName,
                                      PreferredBerthName = pb.BerthName,
                                      AlternateBerthName = ab.BerthName,
                                      VCN = value,
                                      ETA = a.ETA,
                                      ETD = a.ETD,
                                      IMONo = v.IMONo
                                  }
            ).FirstOrDefault<ArrivalNotificationDetails>();

            return arrivalDetails;
        }

        /// <summary>
        /// To Get Service request details For workflow approve/confirm/cancel
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ServiceRequest GetServiceRequestDetailsForWorkFlow(string ServiceRequestID)
        {
            int serviceRequestIdConverted = Convert.ToInt32(ServiceRequestID, CultureInfo.InvariantCulture);
            var servicedtls = (from t in _unitOfWork.Repository<ServiceRequest>().Query(t => t.ServiceRequestID == serviceRequestIdConverted)
                                   .Include(t => t.SubCategory)
                                   .Include(t => t.ServiceRequestSailings)
                                   .Include(t => t.ServiceRequestSailings.Select(p => p.Document))
                                   .Include(t => t.ServiceRequestWarpings)
                                   .Include(t => t.ServiceRequestShiftings)
                                   .Include(t => t.ArrivalNotification)
                                   .Include(t => t.ArrivalNotification.Vessel)
                                   .Include(t => t.ArrivalNotification.VesselCalls)
                                   .Include(t => t.VesselCallMovements).Select()
                               select t).FirstOrDefault<ServiceRequest>();
            return servicedtls;
        }

        /// <summary>
        /// To Get Service request details For workflow approve/confirm/cancel
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<VesselCallMovement> GetVesselCallMovement(int ServiceRequestID)
        {

            var vcallmovement = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Query().Select()
                                 where vcm.ServiceRequestID == ServiceRequestID
                                 select vcm).ToList<VesselCallMovement>();
            return vcallmovement;

        }

        public VesselCallMovement GetVesselCallMovementAtVCN(string VCN, string portcode)
        {

            var vcallmovement = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Query().Select()
                                 where vcm.VCN == VCN && vcm.FromPositionPortCode == portcode
                                 select vcm);
            return vcallmovement.FirstOrDefault();

        }

        public List<VesselCallMovementVO> GetAllVesselCallMovements(string VCN, string portcode)
        {
            var vcallmovement = _unitOfWork.SqlQuery<VesselCallMovementVO>("SELECT S.ServiceRequestID, S.VCN, S.MovementType, V.MovementStatus from ServiceRequest S INNER JOIN ServiceRequestShifting SH ON SH.ServiceRequestID = S.ServiceRequestID LEFT JOIN VesselCallMovement V ON V.ServiceRequestID = S.ServiceRequestID  and V.FromPositionPortCode = @p1 WHERE S.ServiceRequestID > (select ServiceRequestID from ServiceRequest where MovementType = 'ARMV' and  VCN = @p0 and RecordStatus = 'A') and S.VCN = @p0 and S.RecordStatus = 'A' and S.MovementType = 'SHMV' and SH.ToPortCode = @p1", VCN, portcode).ToList();

            return vcallmovement;

        }


        public List<ServiceRequest> GetServRequestDeatailsForValidation(string VCN)
        {

            var serval = (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable().Where(sr => sr.VCN == VCN && sr.RecordStatus == RecordStatus.Active)                       
                          select sr).ToList<ServiceRequest>();
            return serval;

        }


        public List<ServiceRequest> GetServRequestDeatailsForValidationForArrival(string VCN)
        {

            var serval = (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable().Where(sr => sr.VCN == VCN && sr.RecordStatus == RecordStatus.Active && sr.MovementType == MovementTypes.ARRIVAL)                         
                          select sr).ToList<ServiceRequest>();
            return serval;

        }

        public ServiceRequest GetAllServRequestDeatailsForValidationForArrival(string VCN)
        {

            var serval = (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable().Where(sr => sr.VCN == VCN  && sr.MovementType == MovementTypes.ARRIVAL)
                          orderby sr.ServiceRequestID descending
                          select sr).FirstOrDefault<ServiceRequest>();
            return serval;

        }

        /// <summary>
        /// ///////////BY Mahesh : To get vesselcalmovemnt details at particular VCN
        /// </summary>
        /// <param name="slotDate"></param>
        /// <param name="_PortCode"></param>
        /// <returns></returns>        
        public VesselCallMovementVO GetVCallMovtAtVCN(string VCN, string PortCode)
        {        


            var vessel = _unitOfWork.SqlQuery<VesselCallMovementVO>("SELECT VCM.ATB,VC.ETA,VCM.MovementType,VCM.MovementDateTime,VCM.MovementStatus,VCM.ServiceRequestID FROM VesselCallMovement VCM join VesselCall VC on VC.VCN = VCM.VCN and VC.FromPositionPortCode = VCM.FromPositionPortCode WHERE VesselCallMovementID IN (SELECT Max (VesselCallMovementID) FROM VesselCallMovement IVCM  WHERE IVCM.VCN = @p0 AND IVCM.FromPositionPortCode = @p1)", VCN, PortCode);

            return vessel.FirstOrDefault<VesselCallMovementVO>();
        }


        public VesselCallMovementVO GetVCallMovtAtVCNArrival(string VCN, string PortCode)
        {


            var vessel = _unitOfWork.SqlQuery<VesselCallMovementVO>("SELECT VCM.ATB,VC.ETA,VCM.MovementType,VCM.MovementDateTime,VCM.MovementStatus,VCM.ServiceRequestID FROM VesselCallMovement VCM join VesselCall VC on VC.VCN = VCM.VCN and VC.FromPositionPortCode = VCM.FromPositionPortCode " +
                                            " WHERE VCM.MovementType = 'ARMV' AND VCM.RECORDSTATUS = 'A' and VCM.VCN = @p0 AND VCM.FromPositionPortCode = @p1 ", VCN, PortCode);
            return vessel.FirstOrDefault<VesselCallMovementVO>();
        }

        public ServiceRequest GetAllServRequestDeatailsAllMoments(string VCN, string MomentType)
        {
            var serval = (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable().Where(sr => sr.VCN == VCN && sr.MovementType == MomentType)
                          orderby sr.ServiceRequestID descending
                          select sr).FirstOrDefault<ServiceRequest>();
            return serval;

        }

        public List<SlotVO> GetSlotDetails(string portcode)
        {            
            DateTime date = DateTime.Now;

            var slot = (from c in _unitOfWork.Repository<AutomatedSlotConfiguration>().Queryable()
                        where c.EffectiveFrm <= date && c.PortCode == portcode
                        orderby c.EffectiveFrm descending

                        select new
                        {
                            Duration = c.Duration,
                            ConfigPeriod = c.OperationalPeriod 
                        }).FirstOrDefault();           

            int duration = slot != null ? slot.Duration : 2;
            int numberOfSlots = 1440;
            int slotDuration = duration;
            int slots = numberOfSlots / slotDuration;
            List<SlotVO> lstResourceSlotVOs = null;
            lstResourceSlotVOs = new List<SlotVO>();

            SlotVO obj = null;
            int slotperiod = default(int);

            int SlotStartDuration = Convert.ToInt32(slot.ConfigPeriod);

            DateTime dt = date;

            TimeSpan spStartTime = TimeSpan.FromMinutes(SlotStartDuration);
            int StartSlot = spStartTime.Hours;
            string s1 = spStartTime.ToString(@"hh\:mm");

            slotperiod = SlotStartDuration;

            bool flag = false;
            for (int i = 0; i < slots; i++)
            {
                flag = false;
                obj = new SlotVO();
                obj.SlotNumber = i + 1;
                int startPeriod = slotperiod;
                int endPeriod = default(int);


                startPeriod = slotperiod;
                if (slotperiod + duration >= 1440)
                {
                    slotperiod = (slotperiod + duration) - 1440;
                    endPeriod = slotperiod;
                    flag = true;
                }
                else
                {
                    endPeriod = slotperiod + duration;
                    slotperiod += duration;
                }

                if (startPeriod == 1440)
                {
                    startPeriod = 0;
                    flag = true;
                }

                TimeSpan startslot = TimeSpan.FromMinutes(startPeriod);

                TimeSpan endsolt = TimeSpan.FromMinutes(endPeriod);

                obj.SlotPeriod = startslot.ToString(@"hh\:mm") + "-" + endsolt.ToString(@"hh\:mm");
                obj.StartTime = startPeriod;
                obj.EndTime = endPeriod;
                lstResourceSlotVOs.Add(obj);

                //if (endPeriod == 1440)
                //{
                //    dt = date.AddDays(1);
                //}
                if (flag)
                {
                    dt = date.AddDays(1);
                }

            }
           return lstResourceSlotVOs;          
        }

        public SlotVO GetSlotPeriodBySlotDate(DateTime PreferredDate, string portCode)
        {
            int hours = PreferredDate.Hour;
            string slotPeriod = string.Empty;
            double totalminutes = PreferredDate.TimeOfDay.TotalMinutes;
            SlotVO Slots = new SlotVO();

            if (PreferredDate != DateTime.MinValue)
            {
                foreach (ResourceSlotVO slot in _suppServiceResourceAllocRepository.GetSlotConfiguration(PreferredDate, portCode))
                {
                    string[] period = slot.SlotPeriod.Split('-');                  

                    DateTime sttime = Convert.ToDateTime(period[0], CultureInfo.InvariantCulture);

                    DateTime edtime = Convert.ToDateTime(period[1], CultureInfo.InvariantCulture);

                    double startTime = sttime.TimeOfDay.TotalMinutes;

                    double endTime = edtime.TimeOfDay.TotalMinutes;


                    if(startTime > endTime)
                    {
                        if (totalminutes <= endTime && startTime >= totalminutes)
                        {
                            Slots.SlotPeriod = slot.SlotPeriod;
                            Slots.StartTime = startTime;
                            Slots.EndTime = endTime;
                            Slots.Duration = slot.Duration;
                            Slots.StartDate = sttime;
                            Slots.EndDate = edtime;
                            break;
                        }
                        if (totalminutes >= endTime && startTime <= totalminutes)
                        {
                            Slots.SlotPeriod = slot.SlotPeriod;
                            Slots.StartTime = startTime;
                            Slots.EndTime = endTime;
                            Slots.Duration = slot.Duration;
                            Slots.StartDate = sttime;
                            Slots.EndDate = edtime;
                            break;
                        }
                    }              

                    if (totalminutes >= startTime && totalminutes < endTime)
                    {                       
                        Slots.SlotPeriod = slot.SlotPeriod;
                        Slots.StartTime = startTime;
                        Slots.EndTime = endTime;
                        Slots.Duration = slot.Duration;
                        Slots.StartDate = sttime;
                        Slots.EndDate = edtime;
                        break;
                    }
                }
            }
            return Slots;
        }

        public List<AutomatedSlotBlockingVO> GetBlockedSlots(string portCode)
        {
            var Slots = (from sb in _unitOfWork.Repository<AutomatedSlotBlocking>().Queryable().Include(sb => sb.SubCategory).Where(sb => sb.RecordStatus == RecordStatus.Active && sb.PortCode == portCode)                         
                         select sb).ToList<AutomatedSlotBlocking>();

            List<AutomatedSlotBlockingVO> Slots1 = Slots != null ? Slots.MapToDto() : null;

            return Slots1;

        }

        public AutomatedSlotConfiguration GetAutoConfiguredSlots(DateTime movementDate, string portCode)
        {
            var configuredSlots = _unitOfWork.Repository<AutomatedSlotConfiguration>().Queryable().Where(x => x.PortCode == portCode && DbFunctions.TruncateTime(x.EffectiveFrm) <= movementDate && x.RecordStatus == RecordStatus.Active)
                      .OrderByDescending(x => x.EffectiveFrm).FirstOrDefault();
                      

            return configuredSlots;
        }


        public List<VesselCallMovementVO> GetTotalSlotsAvailable(DateTime movementDate, string slotPeriod, string portCode)
        {
            List<VesselCallMovementVO> vesselMovements = new List<VesselCallMovementVO>();
            var AutoConfiguredSlots = GetAutoConfiguredSlots(movementDate, portCode);
            string[] slotstperiod = slotPeriod.Split('-');
            //mahesh K(21-09-2023)NIT_IPMS02 overbooking of slots
            if (slotstperiod[1] == SlotPeriodTimeStatus.slotperiod0 || slotstperiod[1] == SlotPeriodTimeStatus.slotperiod1)
            {
                movementDate = movementDate.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
            }
            //End NIT_IPMS02
            DateTime slotSttime = Convert.ToDateTime(slotstperiod[0], CultureInfo.InvariantCulture);

            double slotStartMinutes = slotSttime.TimeOfDay.TotalMinutes;

            DateTime slotStartDate = movementDate.Date.AddHours(0).AddMinutes(slotStartMinutes).AddSeconds(0);


            DateTime slotEdtime = Convert.ToDateTime(slotstperiod[1], CultureInfo.InvariantCulture);

            double slotEndMinutes = slotEdtime.TimeOfDay.TotalMinutes;

            DateTime slotEndtDate = movementDate;  

            if (slotStartMinutes > slotEndMinutes)
            {
                slotEndtDate = slotEndtDate.AddDays(1);
            }

            slotEndtDate = slotEndtDate.Date.AddHours(0).AddMinutes(slotEndMinutes).AddSeconds(0);

          
            var moments = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(a => a.SlotStatus.Contains(AutomatedSlotStatus.Scheduled) || a.SlotStatus.Contains(AutomatedSlotStatus.Overridden) || a.SlotStatus.Contains(AutomatedSlotStatus.Confirmed) || a.SlotStatus.Contains(AutomatedSlotStatus.Planned))
                           join sr in _unitOfWork.Repository<ServiceRequest>().Queryable() on vcm.ServiceRequestID equals sr.ServiceRequestID
                       where vcm.RecordStatus == RecordStatus.Active && sr.RecordStatus == RecordStatus.Active && vcm.FromPositionPortCode == portCode
                        && (slotStartDate != null ? (vcm.SlotDate) >= slotStartDate : true) &&
                          (slotEndtDate != null ? (vcm.SlotDate) <= slotEndtDate : true)

                       select new VesselCallMovementVO
                       {
                           VesselCallMovementID = vcm.VesselCallMovementID,
                           VCN = vcm.VCN,
                           Slot = vcm.Slot,
                           SlotDate = vcm.SlotDate
                       }).ToList<VesselCallMovementVO>();
           

            return moments;

        }


        public List<SlotVO> GetSlotPeriodFromDateToDate(AutomatedSlotBlockingVO data, string portCode)
        {
        
        var dtFromDate = Convert.ToDateTime(data.FromDate, CultureInfo.InvariantCulture);
        var dtFromTo = Convert.ToDateTime(data.ToDate, CultureInfo.InvariantCulture);

        var slot = (from c in _unitOfWork.Repository<AutomatedSlotConfiguration>().Queryable()
                    where c.EffectiveFrm <= dtFromDate && c.PortCode == portCode
                    orderby c.EffectiveFrm descending

                    select new
                    {
                        Duration = c.Duration,
                        ConfigPeriod = c.OperationalPeriod 
                    }).FirstOrDefault();

        

        string[] slotstartperiod = data.SlotFrom.Split('-');

        DateTime slotSttime = Convert.ToDateTime(slotstartperiod[0], CultureInfo.InvariantCulture);

        double slotStartMinutes = slotSttime.TimeOfDay.TotalMinutes;

        DateTime slotStartDate = dtFromDate.Date.AddHours(0).AddMinutes(slotStartMinutes).AddSeconds(0);



        string[] slottoperiod = data.SlotTo.Split('-');

        DateTime slotEdtime = Convert.ToDateTime(slottoperiod[1], CultureInfo.InvariantCulture);

        double slotEndMinutes = slotEdtime.TimeOfDay.TotalMinutes;

        DateTime slotEndtDate = dtFromTo;

        if (slotStartMinutes > slotEndMinutes)
        {
            slotEndtDate = slotEndtDate.AddDays(1);
        }

        slotEndtDate = slotEndtDate.Date.AddHours(0).AddMinutes(slotEndMinutes).AddSeconds(0);

        int slots = Convert.ToInt32((slotEndtDate - slotStartDate).TotalMinutes / slot.Duration);

        int duration = slot != null ? slot.Duration : 2;

        List<SlotVO> lstSlotVOs = null;
        lstSlotVOs = new List<SlotVO>();

        SlotVO obj = null;
        int slotperiod = default(int);

        DateTime dt = dtFromDate;

        slotperiod = Convert.ToInt32(slotStartMinutes);

        bool flag = false;
        for (int i = 0; i < slots; i++)
        {
            flag = false;
            obj = new SlotVO();
            obj.SlotNumber = i + 1;
            int startPeriod = slotperiod;
            int endPeriod = default(int);


            startPeriod = slotperiod;
            if (slotperiod + duration >= 1440)
            {
                slotperiod = (slotperiod + duration) - 1440;
                endPeriod = slotperiod;
                flag = true;
            }
            else
            {
                endPeriod = slotperiod + duration;
                slotperiod += duration;
            }

            if (startPeriod == 1440)
            {
                startPeriod = 0;
                flag = true;
            }

            TimeSpan startslot = TimeSpan.FromMinutes(startPeriod);

            TimeSpan endsolt = TimeSpan.FromMinutes(endPeriod);

            obj.SlotPeriod = startslot.ToString(@"hh\:mm") + "-" + endsolt.ToString(@"hh\:mm");
            obj.SlotDate = dt;            
            obj.Duration = slot.Duration;
            obj.StartDate = dt.Date.AddHours(0).AddMinutes(startPeriod).AddSeconds(0);
            obj.EndDate = dt.Date.AddHours(0).AddMinutes(endPeriod).AddSeconds(0);
            lstSlotVOs.Add(obj);
            
            if (flag)
            {
                dt = dtFromDate.AddDays(1);
                obj.EndDate = dt.Date.AddHours(0).AddMinutes(endPeriod).AddSeconds(0);
            }

        }
        return lstSlotVOs;
        }

        public List<ServiceRequest_VO> GetIncompleteMovementDetailsById(string vcn)
        {
            List<ServiceRequest_VO> pendingServiceRequests =
                (from sr in _unitOfWork.Repository<ServiceRequest>().Queryable().Where(sr =>
                        sr.VCN == vcn && sr.RecordStatus == RecordStatus.Active && sr.IsRecordingCompleted == "N")
                    orderby sr.MovementType, sr.ServiceRequestID
                    select sr).ToList().MapToDto();

            return pendingServiceRequests;

        }
    }
}
