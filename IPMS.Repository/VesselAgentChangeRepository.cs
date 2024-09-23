using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using System.Globalization;
using System.Web.UI;

namespace IPMS.Repository
{
    public class VesselAgentChangeRepository : IVesselAgentChangeRepository
    {
        private IUnitOfWork _unitOfWork;
        //private string etafrom;

        public VesselAgentChangeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// To get agent id
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAgentId(string portcode, int userID)
        {
            var user = (from a in _unitOfWork.Repository<Agent>().Queryable()
                        join u in _unitOfWork.Repository<User>().Queryable().Where(u => u.UserType == GlobalConstants.AGENT && u.UserID == userID) on a.AgentID equals u.UserTypeID
                        join ap in _unitOfWork.Repository<AgentPort>().Queryable().Where(ap => ap.PortCode == portcode) on a.AgentID equals ap.AgentID
                        //where ap.PortCode == portcode && u.UserType == GlobalConstants.AGENT && u.UserID == userID
                        select a).FirstOrDefault<Agent>();
            if (user != null)
                return user.AgentID;
            else
                return 0;
        }

        /// <summary>
        /// To get user name
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserName(int userID)
        {
            var user = (from u in _unitOfWork.Repository<Agent>().Queryable()//.Select()
                        where u.AgentID == userID
                        select u).FirstOrDefault<Agent>();
            return user.RegisteredName;
        }

        /// <summary>
        /// To get Proposed agents
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="portcode"></param>
        /// <returns></returns>
        public List<AgentVO> GetProposedAgents(int agentId, string portcode, string mode)
        {
            var proagents = (from agentvo in _unitOfWork.Repository<Agent>().Queryable()
                             join agentportvo in _unitOfWork.Repository<AgentPort>().Queryable() on agentvo.AgentID equals agentportvo.AgentID
                             where agentvo.AgentID != 0 ? (mode != "VIEW" ? agentvo.AgentID != agentId && agentportvo.PortCode == portcode : agentportvo.PortCode == portcode) : agentportvo.PortCode == portcode
                             orderby agentvo.RegisteredName ascending

                             select new AgentVO
                             {
                                 AgentID = agentvo.AgentID,
                                 RegisteredName = agentvo.RegisteredName
                             }
                               ).ToList<AgentVO>();

            //var proagents = (from agentvo in _unitOfWork.Repository<Agent>().Query().Select()
            //                 join agentportvo in _unitOfWork.Repository<AgentPort>().Query().Select() on agentvo.AgentID equals agentportvo.AgentID
            //                 where agentvo.AgentID != 0 ? (mode != "VIEW" ? agentvo.AgentID != agentId && agentportvo.PortCode == portcode : agentportvo.PortCode == portcode) : agentportvo.PortCode == portcode
            //                 //where agentvo.AgentID != agentID && agentportvo.PortCode == portcode
            //                 orderby agentvo.RegisteredName ascending

            //                 select new AgentVO
            //                 {
            //                     AgentID = agentvo.AgentID,
            //                     RegisteredName = agentvo.RegisteredName
            //                 }
            //                   ).ToList<AgentVO>();
            return proagents;

        }

        /// <summary>
        /// To get approved VCN's
        /// </summary>
        /// <param name="_portcode"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public List<VesselCallVO> GetVcnDetails(string portcode, int agentId)
        {
            var port = new SqlParameter("@PortCode", portcode);
            var userid = new SqlParameter("@CurrentAgentID", agentId);
            var pt = _unitOfWork.SqlQuery<VesselCallVO>("dbo.usp_VesselAgentChange_GetVCNDetails @PortCode, @CurrentAgentID", port, userid).ToList();

            return pt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Vessel> GetVesselDetails()
        {
            var vesselDetails = (from v in _unitOfWork.Repository<Vessel>().Queryable()
                                 join ar in _unitOfWork.Repository<ArrivalNotification>().Queryable() on v.VesselID equals ar.VesselID
                                 select v);
            return vesselDetails.ToList();
        }
        //To get Active VCN's
        public List<VesselCallVO> GetVCNActiveList(string portcode, int agentId)
        {
            var port = new SqlParameter("@PortCode", portcode);
            var userid = new SqlParameter("@CurrentAgentID", agentId);
            var pt = _unitOfWork.SqlQuery<VesselCallVO>("dbo.usp_VesselAgentChange_GetVCNActiveDetails @PortCode, @CurrentAgentID", port, userid).ToList();
            return pt;
        }
        /// <summary>
        /// To get cahnge of agent request details
        /// </summary>
        /// <returns></returns>
        public List<VesselAgentChange> GetVesselAgentChangeRequestDetails(string portCode, int agentId, int userId, string etaFrom, string etaTo)
        {
            List<VesselAgentChange> query = new List<VesselAgentChange>();
            if (agentId > 0)
            {
                query = (from t in _unitOfWork.Repository<VesselAgentChange>().Queryable().Where(p => p.ArrivalNotification.PortCode == portCode)
                                .Include(t => t.ArrivalNotification.SubCategory3)
                                .Include(t => t.ArrivalNotification.Vessel.SubCategory3)
                                .Include(t => t.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                                .Include(t => t.Agent)
                                .Include(t => t.SubCategory)
                                .Include(t => t.VesselAgentChangeDocuments)
                                .Include(t => t.WorkflowInstatnce.SubCategory)
                         //  .Select()
                         where // t.ArrivalNotification.PortCode == portCode &&
                         t.CreatedBy == userId ? t.CreatedBy == userId
                         && (t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.NewRequest
                         || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Verified || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Confirmed || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.RequestReject
                         || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Approved || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Reject) :
                         t.ProposedAgent == agentId && (t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Approved || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Reject)

                         orderby t.VesselAgentChangeID descending
                         select t
                         ).ToList<VesselAgentChange>();
            }
            else
            {
                query = (from t in _unitOfWork.Repository<VesselAgentChange>().Queryable().Where(p => p.ArrivalNotification.PortCode == portCode)
                    .Include(t => t.ArrivalNotification.SubCategory3)
                    .Include(t => t.ArrivalNotification.Vessel.SubCategory3)
                    .Include(t => t.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                    .Include(t => t.Agent)
                    .Include(t => t.SubCategory)
                    .Include(t => t.VesselAgentChangeDocuments)
                    .Include(t => t.WorkflowInstatnce.SubCategory)
                         //.Select()
                         // where t.ArrivalNotification.PortCode == portCode
                         orderby t.VesselAgentChangeID descending
                         select t
             ).ToList<VesselAgentChange>();
            }

            if (!string.IsNullOrWhiteSpace(etaFrom) && !string.IsNullOrWhiteSpace(etaTo))
                query = query.FindAll(t => (Convert.ToDateTime(t.ArrivalNotification.ETA, CultureInfo.InvariantCulture).Date >= Convert.ToDateTime(etaFrom, CultureInfo.InvariantCulture).Date && Convert.ToDateTime(t.ArrivalNotification.ETA, CultureInfo.InvariantCulture).Date < Convert.ToDateTime(etaTo, CultureInfo.InvariantCulture).AddDays(1).Date));


            return query;
        }

        /// <summary>
        ///  To view change of agent request details data from pending tasks 
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VesselAgentChange> GetzVesselAgentChangeRequestDetails(string vcn)
        {
            //var query = (from t in _unitOfWork.Repository<VesselAgentChange>().Query().Include(t => t.ArrivalNotification.SubCategory3).Include(t => t.ArrivalNotification.Vessel).Include(t => t.Agent).Include(t => t.SubCategory).Include(t => t.VesselAgentChangeDocuments).Include(t => t.WorkflowInstatnce.SubCategory).Select()
            //             orderby t.VesselAgentChangeID descending
            //             where t.VesselAgentChangeID == Convert.ToInt32(vcn)
            //             select t
            //             ).ToList<VesselAgentChange>();//.Where(t => t.VesselAgentChangeID == Convert.ToInt32(vcn)).ToList<VesselAgentChange>();
            //var query = (from t in _unitOfWork.Repository<VesselAgentChange>().Query().Include(t => t.ArrivalNotification.SubCategory3).Include(t => t.ArrivalNotification.Vessel.SubCategory3).Include(t => t.Agent).Include(t => t.SubCategory).Include(t => t.VesselAgentChangeDocuments).Include(t => t.WorkflowInstatnce.SubCategory).Include(t => t.WorkflowInstatnce.SubCategory).Select()
            //             where t.VesselAgentChangeID == Convert.ToInt32(vcn)
            //             orderby t.VesselAgentChangeID descending
            //             select t
            //             ).ToList<VesselAgentChange>();

            var query = (from t in _unitOfWork.Repository<VesselAgentChange>().Query()
                           .Include(t => t.ArrivalNotification.SubCategory3)
                           .Include(t => t.ArrivalNotification.Vessel.SubCategory3)
                           .Include(t => t.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                           .Include(t => t.Agent)
                           .Include(t => t.SubCategory)
                           .Include(t => t.VesselAgentChangeDocuments)
                           .Include(t => t.WorkflowInstatnce.SubCategory)
                           .Select()
                         where t.VesselAgentChangeID == Convert.ToInt32(vcn, CultureInfo.InvariantCulture)

                         orderby t.VesselAgentChangeID descending
                         select t
                    ).ToList<VesselAgentChange>();

            return query;
        }

        /// <summary>
        /// To get notification details
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VesselAgentChange GetVesselAgentcahngeNotificationById(string value)
        {

            var vcn = (from va in _unitOfWork.Repository<VesselAgentChange>().Query().Select()
                       join an in _unitOfWork.Repository<ArrivalNotification>().Query().Select() on va.VCN equals an.VCN
                       join v in _unitOfWork.Repository<Vessel>().Query().Select() on an.VesselID equals v.VesselID
                       join vsb in _unitOfWork.Repository<SubCategory>().Query().Select() on v.VesselType equals vsb.SubCatCode
                       //.Include(v => v.SubCategory3).Select() on an.VesselID equals v.VesselID
                       join ag1 in _unitOfWork.Repository<Agent>().Query().Select() on va.ProposedAgent equals ag1.AgentID
                       join ag2 in _unitOfWork.Repository<Agent>().Query().Select() on va.CurrentAgentID equals ag2.AgentID
                       join sub in _unitOfWork.Repository<SubCategory>().Query().Select() on va.ReasonForTransferCode equals sub.SubCatCode

                       where va.VesselAgentChangeID == Convert.ToInt32(value, CultureInfo.InvariantCulture)
                       select new VesselAgentChange
                       {
                           VCN = va.VCN,
                           VesselName = v.VesselName,
                           ReasonForTransferCode = sub.SubCatName,
                           ProposedAgentName = ag1.RegisteredName,
                           RequestedAgentName = ag2.RegisteredName,
                           EffectiveDateTime = va.EffectiveDateTime,
                           CurrentAgentID = va.CurrentAgentID,
                           ProposedAgent = va.ProposedAgent,
                           VesselType = vsb.SubCatName
                       }).FirstOrDefault<VesselAgentChange>();
            return vcn;
        }

        public int ValidateVcn(string vcn)
        {
            var result = (from a in _unitOfWork.Repository<VesselAgentChange>().Queryable()//.Select()
                          join b in _unitOfWork.Repository<WorkflowInstance>().Queryable()//.Select() 
                          on a.WorkflowInstanceId equals b.WorkflowInstanceId
                          where (b.WorkflowTaskCode == WFStatus.New || b.WorkflowTaskCode == WFStatus.Confirmed) && a.VCN == vcn
                          select a);
            return result.Count();
        }

        public List<VesselAgentChange> GetVesselAgentChangeRequestsSearchDetail(string vcn, string vesselName, string etaFrom, string etaTo, int agentId, int userId, string portCode)
        {

            List<VesselAgentChange> query = new List<VesselAgentChange>();
            if (agentId > 0)
            {

                query = (from t in _unitOfWork.Repository<VesselAgentChange>().Queryable().Where(p => p.ArrivalNotification.PortCode == portCode)
                               .Include(t => t.ArrivalNotification.SubCategory3)
                               .Include(t => t.ArrivalNotification.Vessel.SubCategory3)
                               .Include(t => t.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                               .Include(t => t.Agent)
                               .Include(t => t.SubCategory)
                               .Include(t => t.VesselAgentChangeDocuments)
                               .Include(t => t.WorkflowInstatnce.SubCategory)
                         //  .Select()
                         where // t.ArrivalNotification.PortCode == portCode &&
                         t.CreatedBy == userId ? t.CreatedBy == userId
                         && (t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.NewRequest
                         || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Verified || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Confirmed || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.RequestReject
                         || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Approved || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Reject) :
                         t.ProposedAgent == agentId && (t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Approved || t.WorkflowInstatnce.WorkflowTaskCode == WFStatus.Reject)
                         orderby t.VesselAgentChangeID descending
                         select t
                        ).ToList<VesselAgentChange>();
            }
            else
            {
                query = (from t in _unitOfWork.Repository<VesselAgentChange>().Queryable().Where(p => p.ArrivalNotification.PortCode == portCode)
                     .Include(t => t.ArrivalNotification.SubCategory3)
                    .Include(t => t.ArrivalNotification.Vessel.SubCategory3)
                    .Include(t => t.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                    .Include(t => t.Agent)
                    .Include(t => t.SubCategory)
                    .Include(t => t.VesselAgentChangeDocuments)
                    .Include(t => t.WorkflowInstatnce.SubCategory)
                         //.Select()
                         //where t.ArrivalNotification.PortCode == portCode
                         orderby t.VesselAgentChangeID descending
                         select t
             ).ToList<VesselAgentChange>();
            }


            if (vcn != "ALL")
                query = query.FindAll(t => t.VCN.ToUpperInvariant().Contains(vcn.ToUpperInvariant()));

            if (vesselName != "ALL")
                query = query.FindAll(t => t.ArrivalNotification.Vessel.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));

            if (!string.IsNullOrWhiteSpace(etaFrom) && !string.IsNullOrWhiteSpace(etaTo))
                query = query.FindAll(t => (Convert.ToDateTime(t.ArrivalNotification.ETA, CultureInfo.InvariantCulture).Date >= Convert.ToDateTime(etaFrom, CultureInfo.InvariantCulture).Date && Convert.ToDateTime(t.ArrivalNotification.ETA, CultureInfo.InvariantCulture).Date < Convert.ToDateTime(etaTo, CultureInfo.InvariantCulture).AddDays(1).Date));
            //query = query.FindAll(t => (Convert.ToDateTime(t.ArrivalNotification.ETA).Date >= Convert.ToDateTime(etafrom).Date && Convert.ToDateTime(t.ArrivalNotification.ETA).Date < Convert.ToDateTime(etato).AddDays(1).Date));

            return query;
        }

    }
}

