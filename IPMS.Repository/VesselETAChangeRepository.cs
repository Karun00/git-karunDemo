using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;
using IPMS.Domain;

namespace IPMS.Repository
{
    public class VesselETAChangeRepository : IVesselETAChangeRepository
    {
        private IUnitOfWork _unitOfWork;

        public VesselETAChangeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetArrivalVCNS
        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
        public List<VesselETAChangeVO> GetArrivalVcns(string portCode)
        {
            var port = new SqlParameter("@PortCode", portCode);
            var vcns = _unitOfWork.SqlQuery<VesselETAChangeVO>("dbo.[usp_GetArrivalVCNs] @PortCode", port).ToList();
            return vcns;
        }
        #endregion

        #region GetArrivalVCNSOnAgentBased
        public List<VesselETAChangeVO> GetArrivalVcnsOnAgentBased(string portcode, int agentId)
        {
            var port = new SqlParameter("@PortCode", portcode);
            var agent = new SqlParameter("@AgentID", agentId);
            var vcns = _unitOfWork.SqlQuery<VesselETAChangeVO>("dbo.[usp_GetArrivalVCNs] @PortCode,@AgentID", port, agent).ToList();
            return vcns;
        }
        #endregion

        #region GetVesselInfoByVCN
        /// <summary>
        /// To Get Vessel Information By VCN
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public VesselETAChangeVO GetVesselInfoByVcn(string vcn)
        {
            var vesselinfo = (from vc in _unitOfWork.Repository<VesselCall>().Queryable().Where(vc => vc.VCN == vcn)
                              select new VesselETAChangeVO
                              {
                                  VCN = vc.ArrivalNotification.VCN,
                                  VesselName = vc.ArrivalNotification.Vessel.VesselName,
                                  VesselAgent = vc.ArrivalNotification.Agent.RegisteredName,
                                  AgentName = vc.ArrivalNotification.Agent.RegisteredName,
                                  GRT = vc.ArrivalNotification.Vessel.GrossRegisteredTonnageInMT,
                                  LOA = vc.ArrivalNotification.Vessel.LengthOverallInM,
                                  Draft = vc.ArrivalNotification.ArrDraft,
                                  ATADateTime = vc.ATA,
                                  ATDDateTime = vc.ATD,
                                  ETADateTime = vc.ETA,
                                  ETDDateTime = vc.ETD,
                                  VoyageIn = vc.ArrivalNotification.VoyageIn,
                                  VoyageOut = vc.ArrivalNotification.VoyageOut,
                                  NoofTimesETAChanged = vc.NoofTimesETAChanged,
                                  PortOfRegistry = vc.ArrivalNotification.Vessel.PortRegistry.PortName,
                                  TelephoneNo1 = vc.ArrivalNotification.Agent.TelephoneNo1,
                                  VesselCallMovementID = vc.ArrivalNotification.VesselCallMovements.FirstOrDefault().VesselCallMovementID,
                                  ATBDateTime = vc.ATB,
                                  ATUBDateTime = vc.ATUB,
                                  PlanDateTimeOfBerthDT = vc.ArrivalNotification.PlanDateTimeOfBerth,
                                  PlanDateTimeToStartCargoDT = vc.ArrivalNotification.PlanDateTimeToStartCargo,
                                  PlanDateTimeToCompleteCargoDT = vc.ArrivalNotification.PlanDateTimeToCompleteCargo,
                                  PlanDateTimeToVacateBerthDT = vc.ArrivalNotification.PlanDateTimeToVacateBerth,
                                  ArrivalReasons = vc.ArrivalNotification.ArrivalReasons.ToList()
                              }).FirstOrDefault();

            vesselinfo.ATA = vesselinfo.ATADateTime != null ? Convert.ToString(vesselinfo.ATADateTime, CultureInfo.InvariantCulture) : string.Empty;
            vesselinfo.ATD = vesselinfo.ATDDateTime != null ? Convert.ToString(vesselinfo.ATDDateTime, CultureInfo.InvariantCulture) : string.Empty;
            vesselinfo.ETA = vesselinfo.ETADateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            vesselinfo.ETD = vesselinfo.ETDDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            vesselinfo.ATB = Convert.ToString(vesselinfo.ATBDateTime, CultureInfo.InvariantCulture);
            vesselinfo.ATUB = Convert.ToString(vesselinfo.ATUBDateTime, CultureInfo.InvariantCulture);

            vesselinfo.PlanDateTimeOfBerth = vesselinfo.PlanDateTimeOfBerthDT != null ?  Convert.ToString(vesselinfo.PlanDateTimeOfBerthDT, CultureInfo.InvariantCulture) : string.Empty;
            vesselinfo.PlanDateTimeToStartCargo = vesselinfo.PlanDateTimeToStartCargoDT != null ? Convert.ToString(vesselinfo.PlanDateTimeToStartCargoDT, CultureInfo.InvariantCulture) : string.Empty;
            vesselinfo.PlanDateTimeToCompleteCargo = vesselinfo.PlanDateTimeToCompleteCargoDT != null ? Convert.ToString(vesselinfo.PlanDateTimeToCompleteCargoDT, CultureInfo.InvariantCulture) : string.Empty;
            vesselinfo.PlanDateTimeToVacateBerth = vesselinfo.PlanDateTimeToVacateBerthDT != null ? Convert.ToString(vesselinfo.PlanDateTimeToVacateBerthDT, CultureInfo.InvariantCulture) : string.Empty;
            foreach (var item in vesselinfo.ArrivalReasons)
            {
                if (vesselinfo.ArrivalReasonforVisit == null)
                {
                    vesselinfo.ArrivalReasonforVisit = item.Reason;
                }
                else
                {
                    vesselinfo.ArrivalReasonforVisit = vesselinfo.ArrivalReasonforVisit + ',' + item.Reason;    
                }
            }
            

            return vesselinfo;
        }
        #endregion

        #region ChangeETADetails
        /// <summary>
        /// To Get Change ETA Details
        /// </summary>
        /// <returns></returns>
        /// 
        public List<VesselETAChangeVO> ChangeEtaDetails(string portCode, string vcn, string vesselName, string etaFrom, string etaTo, string agentNameSearch)
        {
            List<VesselETAChangeVO> changeEtaDetails = new List<VesselETAChangeVO>();          

            changeEtaDetails = (from v in _unitOfWork.Repository<VesselETAChange>().Queryable().Where(v => v.ArrivalNotification.PortCode == portCode).OrderByDescending(v => v.CreatedDate)
                                select new VesselETAChangeVO
                                {
                                    VCN = v.VCN,
                                    VesselName = v.ArrivalNotification.Vessel.VesselName,
                                    VesselAgent = v.ArrivalNotification.Agent.RegisteredName,
                                    AgentName = v.ArrivalNotification.Agent.RegisteredName,
                                    GRT = v.ArrivalNotification.Vessel.GrossRegisteredTonnageInMT,
                                    LOA = v.ArrivalNotification.Vessel.LengthOverallInM,
                                    Draft = v.ArrivalNotification.ArrDraft,
                                    ETADateTime = v.ArrivalNotification.VesselCalls.FirstOrDefault().ETA,
                                    ETDDateTime = v.ArrivalNotification.VesselCalls.FirstOrDefault().ETD,
                                    NewETADateTime = v.ETA,
                                    NewETDDateTime = v.ETD,
                                    OldETADateTime = v.OldETA,
                                    OldETDDateTime = v.OldETD,
                                    VoyageIn = v.VoyageIn,
                                    VoyageOut = v.VoyageOut,
                                    Remarks = v.Remarks,
                                    NoofTimesETAChanged = v.ArrivalNotification.VesselCalls.FirstOrDefault().NoofTimesETAChanged,
                                    RecordStatus = v.RecordStatus,
                                    CreatedBy = v.CreatedBy,
                                    CreatedDate = v.CreatedDate,
                                    AnyDangerousGoodsonBoard = v.ArrivalNotification.AnyDangerousGoodsonBoard,
                                    ATBDateTime = v.ArrivalNotification.VesselCalls.FirstOrDefault().ATB,
                                    ATUBDateTime = v.ArrivalNotification.VesselCalls.FirstOrDefault().ATUB,
                                    PlanDateTimeOfBerthDT = v.OldPlanDateTimeOfBerth,
                                    PlanDateTimeToStartCargoDT = v.OldPlanDateTimeToStartCargo,
                                    PlanDateTimeToCompleteCargoDT = v.OldPlanDateTimeToCompleteCargo,
                                    PlanDateTimeToVacateBerthDT = v.OldPlanDateTimeToVacateBerth,
                                    ArrivalReasons = v.ArrivalNotification.ArrivalReasons.ToList()
                                }).ToList();

            foreach (var item in changeEtaDetails)
            {
                foreach (var item1 in item.ArrivalReasons)
                {
                    if (item.ArrivalReasonforVisit == null)
                    {
                        item.ArrivalReasonforVisit = item1.Reason;
                    }
                    else
                    {
                        item.ArrivalReasonforVisit = item.ArrivalReasonforVisit + ',' + item1.Reason;
                    }
                }
            }

            //Convert date-string for the solution of datetime error(5 hours late)
            (from coe in changeEtaDetails select coe).ToList().ForEach((coe) =>           
            {
                coe.ETA = Convert.ToString(coe.ETADateTime, CultureInfo.InvariantCulture);
                coe.ETD = Convert.ToString(coe.ETDDateTime, CultureInfo.InvariantCulture);
                coe.NewETA = Convert.ToString(coe.NewETADateTime, CultureInfo.InvariantCulture);
                coe.NewETD = Convert.ToString(coe.NewETDDateTime, CultureInfo.InvariantCulture);
                coe.OldETA = Convert.ToString(coe.OldETADateTime, CultureInfo.InvariantCulture);
                coe.OldETD = Convert.ToString(coe.OldETDDateTime, CultureInfo.InvariantCulture);
                coe.ATB = Convert.ToString(coe.ATBDateTime, CultureInfo.InvariantCulture);
                coe.ATUB = Convert.ToString(coe.ATUBDateTime, CultureInfo.InvariantCulture);

                coe.PlanDateTimeOfBerth = Convert.ToString(coe.PlanDateTimeOfBerthDT, CultureInfo.InvariantCulture);
                coe.PlanDateTimeToStartCargo = Convert.ToString(coe.PlanDateTimeToStartCargoDT, CultureInfo.InvariantCulture);
                coe.PlanDateTimeToCompleteCargo = Convert.ToString(coe.PlanDateTimeToCompleteCargoDT, CultureInfo.InvariantCulture);
                coe.PlanDateTimeToVacateBerth = Convert.ToString(coe.PlanDateTimeToVacateBerthDT, CultureInfo.InvariantCulture);
                
                if (coe.CreatedDate != null)
                {
                    coe.CreatedDateAndTime = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", coe.CreatedDate);
                }
            });

         
            if (!string.IsNullOrWhiteSpace(etaFrom) && !string.IsNullOrWhiteSpace(etaTo))
                changeEtaDetails = changeEtaDetails.FindAll(t => (Convert.ToDateTime(t.NewETA, CultureInfo.InvariantCulture).Date >= Convert.ToDateTime(etaFrom, CultureInfo.InvariantCulture).Date && Convert.ToDateTime(t.NewETA, CultureInfo.InvariantCulture).Date < Convert.ToDateTime(etaTo, CultureInfo.InvariantCulture).AddDays(1).Date));

            if (vcn != "ALL")
                changeEtaDetails = changeEtaDetails.FindAll(t => t.VCN.ToUpperInvariant().Contains(vcn.ToUpperInvariant()));

            if (vesselName != "ALL")
                changeEtaDetails = changeEtaDetails.FindAll(t => t.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));

            if (agentNameSearch != "ALL")
                changeEtaDetails = changeEtaDetails.FindAll(t => t.AgentName.ToUpperInvariant().Contains(agentNameSearch.ToUpperInvariant()));

            return changeEtaDetails;
        }
        #endregion

        #region ChangeETADetailsOnAgentBased
        /// <summary>
        /// Change ETA Details On Agent Based
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public List<VesselETAChangeVO> ChangeEtaDetailsOnAgentBased(string portCode, int agentId, string vcn, string vesselName, string etaFrom, string etaTo, string agentNameSearch)
        {
            List<VesselETAChangeVO> changeEtaDetails = new List<VesselETAChangeVO>();

            changeEtaDetails = (from v in _unitOfWork.Repository<VesselETAChange>().Queryable().Where(v => v.ArrivalNotification.PortCode == portCode && v.ArrivalNotification.VesselCalls.FirstOrDefault().RecentAgentID == agentId).OrderByDescending(v => v.CreatedDate)
                                select new VesselETAChangeVO
                                {
                                    VCN = v.VCN,
                                    VesselName = v.ArrivalNotification.Vessel.VesselName,
                                    VesselAgent = v.ArrivalNotification.Agent.RegisteredName,
                                    AgentName = v.ArrivalNotification.Agent.RegisteredName,
                                    GRT = v.ArrivalNotification.Vessel.GrossRegisteredTonnageInMT,
                                    LOA = v.ArrivalNotification.Vessel.LengthOverallInM,
                                    Draft = v.ArrivalNotification.ArrDraft,
                                    ETADateTime = v.ArrivalNotification.VesselCalls.FirstOrDefault().ETA,
                                    ETDDateTime = v.ArrivalNotification.VesselCalls.FirstOrDefault().ETD,
                                    NewETADateTime = v.ETA,
                                    NewETDDateTime = v.ETD,
                                    OldETADateTime = v.OldETA,
                                    OldETDDateTime = v.OldETD,
                                    VoyageIn = v.VoyageIn,
                                    VoyageOut = v.VoyageOut,
                                    Remarks = v.Remarks,
                                    NoofTimesETAChanged = v.ArrivalNotification.VesselCalls.FirstOrDefault().NoofTimesETAChanged,
                                    RecordStatus = v.RecordStatus,
                                    CreatedBy = v.CreatedBy,
                                    CreatedDate = v.CreatedDate,
                                    AnyDangerousGoodsonBoard = v.ArrivalNotification.AnyDangerousGoodsonBoard,
                                    ATBDateTime = v.ArrivalNotification.VesselCalls.FirstOrDefault().ATB,
                                    ATUBDateTime = v.ArrivalNotification.VesselCalls.FirstOrDefault().ATUB,
                                    PlanDateTimeOfBerthDT = v.OldPlanDateTimeOfBerth,
                                    PlanDateTimeToStartCargoDT = v.OldPlanDateTimeToStartCargo,
                                    PlanDateTimeToCompleteCargoDT = v.OldPlanDateTimeToCompleteCargo,
                                    PlanDateTimeToVacateBerthDT = v.OldPlanDateTimeToVacateBerth,
                                    ArrivalReasons = v.ArrivalNotification.ArrivalReasons.ToList()
                                }).ToList();

            foreach (var item in changeEtaDetails)
            {
                foreach (var item1 in item.ArrivalReasons)
                {
                    if (item.ArrivalReasonforVisit == null)
                    {
                        item.ArrivalReasonforVisit = item1.Reason;
                    }
                    else
                    {
                        item.ArrivalReasonforVisit = item.ArrivalReasonforVisit + ',' + item1.Reason;
                    }
                }
            }

            //Convert date-string for the solution of datetime error(5 hours late)
            (from coe in changeEtaDetails select coe).ToList().ForEach((coe) =>
            {
                coe.ETA = Convert.ToString(coe.ETADateTime, CultureInfo.InvariantCulture);
                coe.ETD = Convert.ToString(coe.ETDDateTime, CultureInfo.InvariantCulture);
                coe.NewETA = Convert.ToString(coe.NewETADateTime, CultureInfo.InvariantCulture);
                coe.NewETD = Convert.ToString(coe.NewETDDateTime, CultureInfo.InvariantCulture);
                coe.OldETA = Convert.ToString(coe.OldETADateTime, CultureInfo.InvariantCulture);
                coe.OldETD = Convert.ToString(coe.OldETDDateTime, CultureInfo.InvariantCulture);
                coe.ATB = Convert.ToString(coe.ATBDateTime, CultureInfo.InvariantCulture);
                coe.ATUB = Convert.ToString(coe.ATUBDateTime, CultureInfo.InvariantCulture);

                coe.PlanDateTimeOfBerth = Convert.ToString(coe.PlanDateTimeOfBerthDT, CultureInfo.InvariantCulture);
                coe.PlanDateTimeToStartCargo = Convert.ToString(coe.PlanDateTimeToStartCargoDT, CultureInfo.InvariantCulture);
                coe.PlanDateTimeToCompleteCargo = Convert.ToString(coe.PlanDateTimeToCompleteCargoDT, CultureInfo.InvariantCulture);
                coe.PlanDateTimeToVacateBerth = Convert.ToString(coe.PlanDateTimeToVacateBerthDT, CultureInfo.InvariantCulture);

                if (coe.CreatedDate != null)
                {
                    coe.CreatedDateAndTime = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", coe.CreatedDate);
                }
            });

            if (!string.IsNullOrWhiteSpace(etaFrom) && !string.IsNullOrWhiteSpace(etaTo))                
                changeEtaDetails = changeEtaDetails.FindAll(t => (Convert.ToDateTime(t.NewETA, CultureInfo.InvariantCulture).Date >= Convert.ToDateTime(etaFrom, CultureInfo.InvariantCulture).Date && Convert.ToDateTime(t.NewETA, CultureInfo.InvariantCulture).Date < Convert.ToDateTime(etaTo, CultureInfo.InvariantCulture).AddDays(1).Date));

            if (vcn != "ALL")
                changeEtaDetails = changeEtaDetails.FindAll(t => t.VCN.ToUpperInvariant().Contains(vcn.ToUpperInvariant()));

            if (vesselName != "ALL")
                changeEtaDetails = changeEtaDetails.FindAll(t => t.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));

            if (agentNameSearch != "ALL")
                changeEtaDetails = changeEtaDetails.FindAll(t => t.AgentName.ToUpperInvariant().Contains(agentNameSearch.ToUpperInvariant()));


            return changeEtaDetails;
        }
        #endregion

        #region ChangezETADetails
        /// <summary>
        /// To Get Change ETA Details by vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VesselETAChangeVO> ChangezEtaDetails(string vcn, int? VesselEtaChangeId)
        {
            List<VesselETAChangeVO> VesselEtaChangeVoData = new List<VesselETAChangeVO>();
            if (VesselEtaChangeId > 0)
            {
                VesselEtaChangeVoData = (from v in _unitOfWork.Repository<VesselETAChange>().Query().Select()
                                         join an in _unitOfWork.Repository<ArrivalNotification>().Query().Select() on new { v.VCN } equals new { an.VCN }
                                         join vs in _unitOfWork.Repository<Vessel>().Query().Select() on new { an.VesselID } equals new { vs.VesselID }
                                         join ag in _unitOfWork.Repository<Agent>().Query().Select() on an.AgentID equals ag.AgentID
                                         where v.VesselETAChangeID == VesselEtaChangeId
                                         orderby v.VesselETAChangeID descending
                                         select new VesselETAChangeVO
                                         {
                                             VCN = v.VCN,
                                             VesselName = vs.VesselName,
                                             VesselAgent = ag.RegisteredName,
                                             AgentName = ag.RegisteredName,
                                             GRT = vs.GrossRegisteredTonnageInMT,
                                             LOA = vs.LengthOverallInM,
                                             Draft = an.ArrDraft,
                                             ETA = Convert.ToString(an.ETA, CultureInfo.InvariantCulture),
                                             ETD = Convert.ToString(an.ETD, CultureInfo.InvariantCulture),
                                             NewETA = Convert.ToString(v.ETA, CultureInfo.InvariantCulture),
                                             NewETD = Convert.ToString(v.ETD, CultureInfo.InvariantCulture),
                                             OldETA = v.OldETA != null || v.OldETA != DateTime.MinValue ? Convert.ToString(v.OldETA, CultureInfo.InvariantCulture) : string.Empty,
                                             OldETD = v.OldETD != null || v.OldETD != DateTime.MinValue ? Convert.ToString(v.OldETD, CultureInfo.InvariantCulture) : string.Empty,
                                             VoyageIn = v.VoyageIn,
                                             VoyageOut = v.VoyageOut,
                                             Remarks = v.Remarks,
                                             RecordStatus = v.RecordStatus,
                                             CreatedBy = v.CreatedBy,
                                             CreatedDate = v.CreatedDate,
                                             PlanDateTimeOfBerthDT = v.OldPlanDateTimeOfBerth,
                                             PlanDateTimeToStartCargoDT = v.OldPlanDateTimeToStartCargo,
                                             PlanDateTimeToCompleteCargoDT = v.OldPlanDateTimeToCompleteCargo,
                                             PlanDateTimeToVacateBerthDT = v.OldPlanDateTimeToVacateBerth
                                         }).ToList();
            }
            else
            {
                VesselEtaChangeVoData = (from v in _unitOfWork.Repository<VesselETAChange>().Query().Select()
                                         join an in _unitOfWork.Repository<ArrivalNotification>().Query().Select() on new { v.VCN } equals new { an.VCN }
                                         join vs in _unitOfWork.Repository<Vessel>().Query().Select() on new { an.VesselID } equals new { vs.VesselID }
                                         join ag in _unitOfWork.Repository<Agent>().Query().Select() on an.AgentID equals ag.AgentID
                                         where v.VCN == vcn
                                         orderby v.VesselETAChangeID descending
                                         select new VesselETAChangeVO
                                         {
                                             VCN = v.VCN,
                                             VesselName = vs.VesselName,
                                             VesselAgent = ag.RegisteredName,
                                             AgentName = ag.RegisteredName,
                                             GRT = vs.GrossRegisteredTonnageInMT,
                                             LOA = vs.LengthOverallInM,
                                             Draft = an.ArrDraft,
                                             ETA = Convert.ToString(an.ETA, CultureInfo.InvariantCulture),
                                             ETD = Convert.ToString(an.ETD, CultureInfo.InvariantCulture),
                                             NewETA = Convert.ToString(v.ETA, CultureInfo.InvariantCulture),
                                             NewETD = Convert.ToString(v.ETD, CultureInfo.InvariantCulture),
                                             OldETA = v.OldETA != null || v.OldETA != DateTime.MinValue ? Convert.ToString(v.OldETA, CultureInfo.InvariantCulture) : string.Empty,
                                             OldETD = v.OldETD != null || v.OldETD != DateTime.MinValue ? Convert.ToString(v.OldETD, CultureInfo.InvariantCulture) : string.Empty,
                                             VoyageIn = v.VoyageIn,
                                             VoyageOut = v.VoyageOut,
                                             Remarks = v.Remarks,
                                             RecordStatus = v.RecordStatus,
                                             CreatedBy = v.CreatedBy,
                                             CreatedDate = v.CreatedDate,
                                             PlanDateTimeOfBerthDT = v.OldPlanDateTimeOfBerth,
                                             PlanDateTimeToStartCargoDT = v.OldPlanDateTimeToStartCargo,
                                             PlanDateTimeToCompleteCargoDT = v.OldPlanDateTimeToCompleteCargo,
                                             PlanDateTimeToVacateBerthDT = v.OldPlanDateTimeToVacateBerth
                                         }).ToList();
            }
            //Convert date-string for the solution of datetime error(5 hours late)
            foreach (var item in VesselEtaChangeVoData)
            {
                if (item.CreatedDate != null)
                {
                    item.CreatedDateAndTime = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", item.CreatedDate);
                }
            }
            return VesselEtaChangeVoData;
        }
        #endregion

        #region GetVesselETAChangeDetailsByID
        /// <summary>
        /// To Get Change ETA Details By VesselETAChangeID Id
        /// </summary>
        /// <param name="VesselETAChangeID"></param>
        /// <returns></returns>
        public VesselETAChangeVO GetVesselEtaChangeDetailsById(string VesselETAChangeID)
        {
            var data = (from v in _unitOfWork.Repository<VesselETAChange>().Query().Select()
                        join an in _unitOfWork.Repository<ArrivalNotification>().Query().Select() on v.VCN equals an.VCN
                        join vs in _unitOfWork.Repository<Vessel>().Query().Select() on an.VesselID equals vs.VesselID
                        where v.VesselETAChangeID == Convert.ToInt32(VesselETAChangeID, CultureInfo.InvariantCulture)
                        select new VesselETAChangeVO
                        {
                            VesselETAChangeID = v.VesselETAChangeID,
                            ETA = v.ETA.ToString(CultureInfo.InvariantCulture),
                            ETD = v.ETD.ToString(CultureInfo.InvariantCulture),
                            OldETA = v.OldETA.ToString(CultureInfo.InvariantCulture),
                            OldETD = v.OldETD.ToString(CultureInfo.InvariantCulture),
                            VCN = v.VCN,
                            PortCode = an.PortCode,
                            CreatedBy = v.CreatedBy,
                            VesselName = vs.VesselName
                        }).FirstOrDefault<VesselETAChangeVO>();

            data.ETA = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", data.ETA);
            data.ETD = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", data.ETD);
            data.OldETA = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", data.OldETA);
            data.OldETD = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", data.OldETD);

            return data;
        }
        #endregion
    }
}
