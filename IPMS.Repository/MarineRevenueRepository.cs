using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Config;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Globalization;
namespace IPMS.Repository
{
    public class MarineRevenueRepository : IMarineRevenueRepository
    {
        private IUnitOfWork _unitOfWork;
        // private readonly ILog log;


        public MarineRevenueRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            //log = 
            LogManager.GetLogger(typeof(MarineRevenueRepository));
        }

        public List<RevenuePostingVO> GetMarineRevenueList(string portCode)
        {

            var revenueDetails = (from r in _unitOfWork.Repository<RevenuePosting>().Query()
                                       .Include(r => r.Agent)
                                       .Include(r => r.Agent.AgentAccounts)
                                       .Include(r => r.ArrivalNotification)
                                       .Include(r => r.ArrivalNotification.ArrivalReasons)
                                       .Include(r => r.ArrivalNotification.ArrivalReasons.Select(d => d.SubCategory))
                                       .Include(r => r.ArrivalNotification.SubCategory3)
                                       .Include(r => r.ArrivalNotification.Vessel)
                                       .Include(r => r.ArrivalNotification.VesselCalls)
                                       .Include(r => r.ArrivalNotification.Vessel.SubCategory3)
                                       .Select()
                                  where r.PortCode == portCode
                                  select r).OrderByDescending(x=>x.PostedDate).ToList();

            return revenueDetails.MapToDTO();
        }


        public List<RevenuePostingVO> GetMarineRevenueDetails(string portCode, string vcnSearch, string vesselName, string frmdate, string todate)
        {
            var revenueDetails = new List<RevenuePosting>();

            // DateTime fromDate = Convert.ToDateTime(frmdate.ToString());
            DateTime fromDate = Convert.ToDateTime(frmdate.ToString(), CultureInfo.InvariantCulture).Date;
            // DateTime toDate = Convert.ToDateTime(todate.ToString());
            DateTime toDate = Convert.ToDateTime(todate.ToString(), CultureInfo.InvariantCulture).AddDays(1).Date;
            string[] vesselNames = vesselName.Split('-');
            vesselName = vesselNames[0].ToString().Trim();
            if (vcnSearch == "All" && vesselName == "All")
            {
                revenueDetails = (from r in _unitOfWork.Repository<RevenuePosting>().Query(r => r.CreatedDate >= fromDate && r.CreatedDate <= toDate && r.PortCode == portCode)
                                           .Include(r => r.Agent)
                                           .Include(r => r.Agent.AgentAccounts)
                                           .Include(r => r.ArrivalNotification)
                                           .Include(r => r.ArrivalNotification.ArrivalReasons)
                                           .Include(r => r.ArrivalNotification.ArrivalReasons.Select(d => d.SubCategory))
                                           .Include(r => r.ArrivalNotification.SubCategory3)
                                           .Include(r => r.ArrivalNotification.Vessel)
                                           .Include(r => r.ArrivalNotification.VesselCalls)
                                           .Include(r => r.ArrivalNotification.Vessel.SubCategory3)                                           
                                           .Select()
                                  select r).OrderByDescending(x => x.PostedDate).ToList();
            }
            else if (vcnSearch != "All" && vesselName == "All")
            {

                revenueDetails = (from r in _unitOfWork.Repository<RevenuePosting>().Query(r => r.PortCode == portCode && r.vcn.Contains(vcnSearch.ToUpper()))
                                           .Include(r => r.Agent)
                                           .Include(r => r.Agent.AgentAccounts)
                                           .Include(r => r.ArrivalNotification)
                                           .Include(r => r.ArrivalNotification.ArrivalReasons)
                                           .Include(r => r.ArrivalNotification.ArrivalReasons.Select(d => d.SubCategory))
                                           .Include(r => r.ArrivalNotification.SubCategory3)
                                           .Include(r => r.ArrivalNotification.Vessel)
                                           .Include(r => r.ArrivalNotification.VesselCalls)
                                           .Include(r => r.ArrivalNotification.Vessel.SubCategory3)
                                           .Select()
                                  select r).OrderByDescending(x => x.PostedDate).ToList();

            }

            else if (vcnSearch == "All" && vesselName != "All")
            {

                //revenueDetails = (from r in _unitOfWork.Repository<RevenuePosting>().Query(r => r.PortCode == portCode && (r.vcn.Contains(vcnSearch.ToUpper()) || r.ArrivalNotification.Vessel.VesselName.Contains(vesselName.ToUpper())))
                //                           .Include(r => r.Agent)
                //                           .Include(r => r.Agent.AgentAccounts)
                //                           .Include(r => r.ArrivalNotification)
                //                           .Include(r => r.ArrivalNotification.ArrivalReasons)
                //                           .Include(r => r.ArrivalNotification.ArrivalReasons.Select(d => d.SubCategory))
                //                           .Include(r => r.ArrivalNotification.SubCategory3)
                //                           .Include(r => r.ArrivalNotification.Vessel)
                //                           .Include(r => r.ArrivalNotification.VesselCalls)
                //                           .Include(r => r.ArrivalNotification.Vessel.SubCategory3)
                //                           .Select()
                //                  select r).ToList();

                revenueDetails = (from r in _unitOfWork.Repository<RevenuePosting>().Query(r => r.PortCode == portCode && r.ArrivalNotification.Vessel.VesselName.Contains(vesselName.ToUpper()))
                                        .Include(r => r.Agent)
                                        .Include(r => r.Agent.AgentAccounts)
                                        .Include(r => r.ArrivalNotification)
                                        .Include(r => r.ArrivalNotification.ArrivalReasons)
                                        .Include(r => r.ArrivalNotification.ArrivalReasons.Select(d => d.SubCategory))
                                        .Include(r => r.ArrivalNotification.SubCategory3)
                                        .Include(r => r.ArrivalNotification.Vessel)
                                        .Include(r => r.ArrivalNotification.VesselCalls)
                                        .Include(r => r.ArrivalNotification.Vessel.SubCategory3)
                                        .Select()
                                  select r).OrderByDescending(x => x.PostedDate).ToList();


            }
            else if (vcnSearch != "All" && vesselName != "All")
            {
                revenueDetails = (from r in _unitOfWork.Repository<RevenuePosting>().Query(r => r.PortCode == portCode && r.vcn.Contains(vcnSearch.ToUpper()) && r.ArrivalNotification.Vessel.VesselName.Contains(vesselName.ToUpper()))
                                        .Include(r => r.Agent)
                                        .Include(r => r.Agent.AgentAccounts)
                                        .Include(r => r.ArrivalNotification)
                                        .Include(r => r.ArrivalNotification.ArrivalReasons)
                                        .Include(r => r.ArrivalNotification.ArrivalReasons.Select(d => d.SubCategory))
                                        .Include(r => r.ArrivalNotification.SubCategory3)
                                        .Include(r => r.ArrivalNotification.Vessel)
                                        .Include(r => r.ArrivalNotification.VesselCalls)
                                        .Include(r => r.ArrivalNotification.Vessel.SubCategory3)
                                        .Select()
                                  select r).OrderByDescending(x => x.PostedDate).ToList();

            }
            else
            {
                revenueDetails = (from r in _unitOfWork.Repository<RevenuePosting>().Query(r => r.CreatedDate >= fromDate && r.CreatedDate <= toDate && r.PortCode == portCode)
                           .Include(r => r.Agent)
                           .Include(r => r.Agent.AgentAccounts)
                           .Include(r => r.ArrivalNotification)
                           .Include(r => r.ArrivalNotification.ArrivalReasons)
                           .Include(r => r.ArrivalNotification.ArrivalReasons.Select(d => d.SubCategory))
                           .Include(r => r.ArrivalNotification.SubCategory3)
                           .Include(r => r.ArrivalNotification.Vessel)
                           .Include(r => r.ArrivalNotification.VesselCalls)
                           .Include(r => r.ArrivalNotification.Vessel.SubCategory3)
                           .Select()
                                  select r).OrderByDescending(x => x.PostedDate).ToList();

                //revenueDetails = (from r in _unitOfWork.Repository<RevenuePosting>().Query(r => r.PortCode == portCode && r.ArrivalNotification.Vessel.VesselName.Contains(vesselName.ToUpper()))
                //                           .Include(r => r.Agent)
                //                           .Include(r => r.Agent.AgentAccounts)
                //                           .Include(r => r.ArrivalNotification)
                //                           .Include(r => r.ArrivalNotification.ArrivalReasons)
                //                           .Include(r => r.ArrivalNotification.ArrivalReasons.Select(d => d.SubCategory))
                //                           .Include(r => r.ArrivalNotification.SubCategory3)
                //                           .Include(r => r.ArrivalNotification.Vessel)
                //                           .Include(r => r.ArrivalNotification.VesselCalls)
                //                           .Include(r => r.ArrivalNotification.Vessel.SubCategory3)
                //                           .Select()
                //                  select r).ToList();

            }


            if (vcnSearch != "All")
                revenueDetails = revenueDetails.FindAll(t => t.vcn.ToUpperInvariant().Contains(vcnSearch.ToUpperInvariant()));

            if (vesselName != "All")
                revenueDetails = revenueDetails.FindAll(t => t.ArrivalNotification.Vessel.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));
            //if (frmdate != "All")
            //    //  servicedtls = servicedtls.Where(sr => (DateTime.Parse(sr.MovementDateTime.ToString("yyyy-MM-dd")) >= DateTime.Parse(frmdate)) && (DateTime.Parse(sr.MovementDateTime.ToString("yyyy-MM-dd")) <= DateTime.Parse(todate))).ToList();
            //    revenueDetails = revenueDetails.FindAll(t => (Convert.ToDateTime(t.CreatedDate, CultureInfo.InvariantCulture).Date >= Convert.ToDateTime(frmdate, CultureInfo.InvariantCulture).Date && Convert.ToDateTime(t.CreatedDate, CultureInfo.InvariantCulture).Date < Convert.ToDateTime(todate, CultureInfo.InvariantCulture).AddDays(1).Date));

            return revenueDetails.MapToDTO();
        }


        public List<AgentVO> GetVcnAgents(string searchValue, string portCode)
        {
            var Arvagnt = (from an in _unitOfWork.Repository<ArrivalAgent>().Query().Tracking(true).Select()
                           join ap in _unitOfWork.Repository<Agent>().Query().Tracking(true).Select()
                           on new { a = an.AgentID }
                           equals new { a = ap.AgentID }
                           where an.VCN == searchValue
                           select ap).Union(from ag in _unitOfWork.Repository<Agent>().Query().Tracking(true).Select()
                                            join vac in _unitOfWork.Repository<VesselAgentChange>().Query().Tracking(true).Select()
                                               on ag.AgentID equals vac.ProposedAgent
                                            //join wf in _unitOfWork.Repository<WorkflowInstance>().Query().Tracking(true).Select()
                                            //  on vac.WorkflowInstanceId equals wf.WorkflowInstanceId
                                            where
                                           vac.VCN == searchValue && vac.IsFinal == "Y"
                                            select ag).ToList();
            return Arvagnt.MapToDToIDNAME();
        }

        public List<RevenuePostingVO> GetVcnDetails(string searchValue, string searchColumn, string param, string portCode)
        {

            var Portcode = new SqlParameter("@p_PortCode", portCode);
            var searchtxt = new SqlParameter("@p_SearchText", searchValue);
            var searchon = new SqlParameter("@p_Searchon", searchColumn);
            var paramSearch = new SqlParameter("@p_Param", param);

            var vcndtls = _unitOfWork.SqlQuery<RevenuePostingVO>("dbo.usp_GetRevenueVCNSearch  @p_SearchText ,@p_PortCode, @p_Searchon, @p_Param", searchtxt, Portcode, searchon, paramSearch).ToList();

            return vcndtls;


            //var vcndtls = (from an in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true).Select()
            //               join re in _unitOfWork.Repository<ArrivalReason>().Query().Tracking(true).Select()
            //               on an.VCN equals re.VCN
            //               join vc in _unitOfWork.Repository<VesselCall>().Query().Tracking(true).Select()
            //               on an.VCN equals vc.VCN
            //               join ve in _unitOfWork.Repository<Vessel>().Query().Tracking(true).Select()
            //               on an.VesselID equals ve.VesselID
            //               join por1 in _unitOfWork.Repository<PortRegistry>().Query().Tracking(true).Select()
            //               on an.LastPortOfCall equals por1.PortCode
            //               join por2 in _unitOfWork.Repository<PortRegistry>().Query().Tracking(true).Select()
            //               on an.NextPortOfCall equals por2.PortCode
            //               join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
            //                on ve.VesselNationality equals sc.SubCatCode
            //               join subc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
            //                on an.ReasonForVisit equals subc.SubCatCode
            //               where an.PortCode == portcode && vc.ATA != null
            //               orderby an.VCN
            //               select an).Where(x => x.VCN.ToLower().Contains(searchValue.ToLower())).ToList<ArrivalNotification>();

            //List<RevenuePostingVO> berthvoList = new List<RevenuePostingVO>();
            //foreach (var an in vcndtls)
            //{
            //    RevenuePostingVO berthvo = new RevenuePostingVO();
            //    berthvo.vcn = an.VCN;
            //    berthvo.VesselName = an.Vessel.VesselName;  //  a .VesselName;
            //    berthvo.GRT = an.Vessel.GrossRegisteredTonnageInMT;
            //    berthvo.CallSign = an.Vessel.CallSign;
            //    berthvo.VoyageIn = an.VoyageIn;
            //    berthvo.VoyageOut = an.VoyageOut;

            //    foreach (var ar in an.ArrivalReasons)
            //    {
            //        if (berthvo.ReasonForVisit == null)
            //        {
            //            if (ar.SubCategory.SubCatName != null)
            //            {
            //                berthvo.ReasonForVisit = ar.SubCategory.SubCatName;
            //            }
            //        }
            //        else
            //        {
            //            if (ar.SubCategory.SubCatName != null)
            //            {
            //                berthvo.ReasonForVisit = berthvo.ReasonForVisit + ',' + ar.SubCategory.SubCatName;
            //            }
            //        }
            //    }
            //    berthvo.VesselType = an.Vessel.SubCategory3 != null ? an.Vessel.SubCategory3.SubCatName : "";
            //    berthvo.IMONo = an.Vessel.IMONo;

            //    berthvo.LastPortOfCall = an.LastPort.PortName;
            //    berthvo.NextPortOfCall = an.NextPort.PortName;
            //    berthvo.ATA = an.VesselCalls.OrderBy(T => T.VesselCallID).FirstOrDefault().ATA;
            //    berthvo.ATD = an.VesselCalls.OrderBy(T => T.VesselCallID).FirstOrDefault().ATD;
            //    berthvoList.Add(berthvo);
            //}


            //return berthvoList;
        }

        public RevenuePostingSectionsVO GetRevenueSectionDetails(string vcn, string portCode)
        {

            var portcode = new SqlParameter("@portcode", portCode);
            var VCN = new SqlParameter("@VCN", vcn);

            var revenueDetails = _unitOfWork.SqlQuery<MarineRevenuePostingVO>("dbo.usp_RevenueVTSRDues  @portcode,@VCN ", portcode, VCN).ToList();

            return revenueDetails.MapToDto();
        }



        public RevenuePostingSectionsVO GetRevenueSectionDetailsView(int revenuePostingId, int agentId, int accountId, string portCode)
        {

            //var AgntName = (from u in _unitOfWork.Repository<Agent>().Query().Select()
            //            where u.AgentID == Agentid
            //            select u).FirstOrDefault<Agent>();

            var agentAccountDetails = (from a in _unitOfWork.Repository<Agent>().Query().Tracking(true).Select()
                                       join ac in _unitOfWork.Repository<AgentAccount>().Query().Tracking(true).Select()
                                           on a.AgentID equals ac.AgentID
                                       where a.AgentID == agentId && ac.AgentAccountID == accountId
                                       select new RevenuePostingSectionsVO
                                       {
                                           RegisteredName = a.RegisteredName,
                                           AccountNo = ac.AccountNo
                                       }).FirstOrDefault();
            string AgntRegName = agentAccountDetails.RegisteredName;
            string AgntAccountNo = agentAccountDetails.AccountNo;


            var portcode = new SqlParameter("@portcode", portCode);
            var RevenuePostingId = new SqlParameter("@revenuePostingid", revenuePostingId);
            var revenueDetails = _unitOfWork.SqlQuery<MarineRevenuePostingVO>("dbo.usp_RevenueDtls_View  @portcode, @revenuePostingid ", portcode, RevenuePostingId).ToList();



            return revenueDetails.MapToDtoView(AgntRegName, AgntAccountNo);
        }

        public List<AgentAccountVO> GetAgentAccountDetails(int agentId, string portCode)
        {

            var agentid = new SqlParameter("@Agentid", agentId);
            var portcode = new SqlParameter("@Portcode", portCode);
            var agentAccountDetails = _unitOfWork.SqlQuery<AgentAccountVO>("dbo.usp_GetMarinePostingAccountList  @Agentid, @Portcode ", agentid, portcode).ToList();
            return agentAccountDetails;

            //var agentAccountDetails = (from a in _unitOfWork.Repository<Agent>().Query().Tracking(true).Select()
            //                           join ac in _unitOfWork.Repository<AgentAccount>().Query().Tracking(true).Select()
            //                               on a.AgentID equals ac.AgentID
            //                           where a.AgentID == agentID && ac.PortCode == portCode
            //                           select new AgentAccountVO
            //                           {
            //                               AgentAccountID = ac.AgentAccountID,
            //                               AccountNo = ac.AccountNo
            //                           }).ToList();

            //return agentAccountDetails;

        }



        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<RevenuePostingVO> RevenuePostingVcnDetailsforAutocomplete(string searchValue, string portCode)
        {

            var portcode = new SqlParameter("@p_PortCode", portCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var vcndtls = _unitOfWork.SqlQuery<RevenuePostingVO>("dbo.usp_GetRevenuePostingVCNSearch @p_SearchText, @p_PortCode", Searchvalue, portcode).ToList();

            //List<RevenuePostingVO> vcnlistVO = new List<RevenuePostingVO>();
            //foreach (var an in vcndtls)
            //{
            //    RevenuePostingVO vcnlist = new RevenuePostingVO();
            //    vcnlist.vcn = an.VCN;
            //    vcnlistVO.Add(vcnlist);
            //}
            return vcndtls;

        }
        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<VesselVO> RevenuePostingVesselDetailsforAutocomplete(string PortCode, string searchValue)
        {

            var portcode = new SqlParameter("@p_PortCode", PortCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            // var userId = new SqlParameter("@p_userid", userid);

            var _VesselInfo = _unitOfWork.SqlQuery<VesselVO>("dbo.usp_GetRevenuePostingVesselSearch @p_SearchText, @p_PortCode", Searchvalue, portcode).ToList();


            return _VesselInfo;
        }
    }
}
