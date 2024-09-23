using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Config;
using IPMS.Domain;
using System.Globalization;
using System.Data.SqlClient;
namespace IPMS.Repository
{
    public class SuppDryDockRepository : ISuppDryDockRepository
    {
        private IUnitOfWork _unitOfWork;
        //  private readonly ILog log;
        private IVesselAgentChangeRepository _vesselAgentChangeRepository;
        // private IUserRepository _userRepository;

        public SuppDryDockRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            //  log = LogManager.GetLogger(typeof(SuppDryDockRepository));
            _vesselAgentChangeRepository = new VesselAgentChangeRepository(_unitOfWork);
            //  _userRepository = new UserRepository(_unitOfWork);
        }

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 22st August 2014
        /// Purpose : To Get Supplymentary Service Request details
        /// </summary>
        /// <returns></returns>
        public List<SuppDryDockVO> GetSuppDryDockApplicationList(string portcode, int userid)
        {
            var suppDryDockAppList = new List<SuppDryDock>();
            int agentId = _vesselAgentChangeRepository.GetAgentId(portcode, userid);
           
            // List<int> userslist = new List<int>();
            // userslist = _userRepository.GetUserIdsByAgentID(agentId);

            //int[] userids = userslist.ToArray();

           if (agentId != 0)
           {
               suppDryDockAppList = (from sr in _unitOfWork.Repository<SuppDryDock>().Queryable()
                                         .Include(sr => sr.Berth)
                                         .Include(sr => sr.WorkflowInstance)
                                         .Include(sr => sr.WorkflowInstance.SubCategory)
                                         .Include(sr => sr.ArrivalNotification)
                                         .Include(sr => sr.ArrivalNotification.Berth1)
                                         .Include(sr => sr.ArrivalNotification.Vessel)
                                         .Include(sr => sr.ArrivalNotification.VesselCalls)
                                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(a => a.Agent))
                                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(a => a.Agent.AuthorizedContactPerson))
                                         .Include(sr => sr.SuppDryDockDocuments)
                                         .Include(sr => sr.SuppDryDockDocuments.Select(d => d.Document))
                                         .Include(p => p.SuppDryDockDocuments.Select(a => a.Document.SubCategory1))
                                         where sr.DockPortCode == portcode && sr.ArrivalNotification.VesselCalls.FirstOrDefault<VesselCall>().RecentAgentID == agentId
                                       // && sr.RecordStatus == "PLND" || sr.RecordStatus == "NEW"
                                        // && sr.RecordStatus == RecordStatus.Active 
                                         //&& sr.RecordStatus==RecordStatus.InActive
                                         select sr).OrderByDescending(x=>x.ModifiedDate).ToList();
           }
           else
           {
               suppDryDockAppList = (from sr in _unitOfWork.Repository<SuppDryDock>().Queryable()
                                      .Include(sr => sr.Berth)
                                      .Include(sr => sr.WorkflowInstance)
                                      .Include(sr => sr.WorkflowInstance.SubCategory)
                                      .Include(sr => sr.ArrivalNotification)
                                      .Include(sr => sr.ArrivalNotification.Berth1)
                                      .Include(sr => sr.ArrivalNotification.Vessel)
                                      .Include(sr => sr.ArrivalNotification.VesselCalls)
                                      .Include(sr => sr.ArrivalNotification.VesselCalls.Select(a => a.Agent))
                                      .Include(sr => sr.ArrivalNotification.VesselCalls.Select(a => a.Agent.AuthorizedContactPerson))
                                      .Include(sr => sr.SuppDryDockDocuments)
                                      .Include(sr => sr.SuppDryDockDocuments.Select(d => d.Document))
                                      .Include(p => p.SuppDryDockDocuments.Select(a => a.Document.SubCategory1))
                                         where sr.DockPortCode == portcode
                                         //&& sr.RecordStatus=="PLND" || sr.RecordStatus=="NEW"
                                         //&& sr.RecordStatus == RecordStatus.Active 
                                         //&& sr.RecordStatus==RecordStatus.InActive
                                     select sr).OrderByDescending(x => x.ModifiedDate).ToList();
           }

            return suppDryDockAppList.MapToDto();
        }

        /// <summary>
        /// Get Supplementary Dry Dock Details By SuppDryDockID 
        /// </summary>
        /// <param name="DockingPlanID"></param>
        /// <returns></returns>
        public SuppDryDock GetSuppDryDockRequestDetailsByID(string suppdrydockid)
        {

            var suppDryDockRequest = (from p in _unitOfWork.Repository<SuppDryDock>().Query().Tracking(true)
                                      .Include(p=> p.ArrivalNotification)
                                      .Include(p => p.ArrivalNotification.Vessel)
                                      .Include(p => p.ArrivalNotification.VesselCalls)
                                      .Include(p => p.ArrivalNotification.Agent)
                                      .Include(p => p.WorkflowInstance)
                                      .Include(p => p.WorkflowInstance.WorkflowProcess)
                                      .Select()

                                      where p.SuppDryDockID == Convert.ToInt32(suppdrydockid, CultureInfo.InvariantCulture)
                                      select new SuppDryDock
                                      {
                                          SuppDryDockID = p.SuppDryDockID,
                                          VesselName = p.ArrivalNotification.Vessel.VesselName,
                                          VesselAgent = p.ArrivalNotification.Agent.RegisteredName,

                                          // -- changed by sandeep on 23-01-2015
                                          //ApplicationDateTime = p.CreatedDate
                                          ApplicationDateTime = p.ModifiedDate,
                                          VCN = p.VCN,
                                          CreatedBy = p.CreatedBy,
                                          ModifiedBy = p.ModifiedBy,
                                          PortCode = p.DockPortCode,
                                          Comments = p.WorkflowInstance.WorkflowProcess.LastOrDefault().Remarks,
                                          UserTypeId = p.ArrivalNotification.VesselCalls.FirstOrDefault<VesselCall>().RecentAgentID

                                          // -- end

                                      }).FirstOrDefault<SuppDryDock>();

            return suppDryDockRequest;
        }

        public SuppDryDock GetSuppDryDockApproveid(string suppdrydockid)
        {
            var andata = (from t in _unitOfWork.Repository<SuppDryDock>().Query().Tracking(true).Select()
                          where t.SuppDryDockID == Convert.ToInt32(suppdrydockid, CultureInfo.InvariantCulture)
                          select t).FirstOrDefault<SuppDryDock>();
            return andata;
        }



        public List<SuppDryDockVO> GetSuppDryDock(int SuppDryDockID)
        {
            var suppdrydock = new List<SuppDryDock>();
                suppdrydock = (from sr in _unitOfWork.Repository<SuppDryDock>().Query().Tracking(true)
                         .Include(sr => sr.Berth)
                         .Include(sr => sr.WorkflowInstance)
                         .Include(sr => sr.WorkflowInstance.SubCategory)
                         .Include(sr => sr.ArrivalNotification)
                         .Include(sr => sr.ArrivalNotification.Berth1)
                         .Include(sr => sr.ArrivalNotification.Vessel)
                         .Include(sr => sr.ArrivalNotification.VesselCalls)
                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(a => a.Agent))
                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(a => a.Agent.AuthorizedContactPerson))
                         .Include(sr => sr.SuppDryDockDocuments)
                         .Include(sr => sr.SuppDryDockDocuments.Select(d => d.Document))
                         .Include(p => p.SuppDryDockDocuments.Select(a => a.Document.SubCategory1))

                         .Select()

                               where sr.SuppDryDockID == Convert.ToInt32(SuppDryDockID)
                               orderby sr.SuppDryDockID descending

                               select sr).ToList<SuppDryDock>();
            return suppdrydock.MapToDto();

        }

        public List<ServiceRequestVCNDetails> GetSuppVCNDetails(string searchvalue, int userid, string portcode)
        {
            return GetSupplementaryVCNs(searchvalue, userid, portcode);
        }

        private List<ServiceRequestVCNDetails> GetSupplementaryVCNs(string searchvalue, int userid, string portcode)
        {
            int agentId = _vesselAgentChangeRepository.GetAgentId(portcode, userid);

            var portCode = new SqlParameter("@p_PortCode", portcode);
            var agentid = new SqlParameter("@p_AgentID", agentId);
            var searchValue = new SqlParameter("@p_searchValue", searchvalue);

            var vcndtls = _unitOfWork.SqlQuery<ServiceRequestVCNDetails>("dbo.usp_GetVCNDetailsForDryDock  @p_PortCode,@p_AgentID,@p_searchValue", portCode, agentid, searchValue).ToList();

            return vcndtls;


            //var vcndtls = (from an in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true).Select()
            //               join ar in _unitOfWork.Repository<ArrivalReason>().Query().Tracking(true).Select()
            //                        on an.VCN equals ar.VCN
            //               join vs in _unitOfWork.Repository<Vessel>().Query().Tracking(true).Select()                           
            //               on an.VesselID equals vs.VesselID
            //               join vc in _unitOfWork.Repository<VesselCall>().Query().Tracking(true).Select()
            //               on an.VCN equals vc.VCN


            //               join wf in _unitOfWork.Repository<WorkflowInstance>().Query().Tracking(true).Select()
            //               on an.WorkflowInstanceId equals wf.WorkflowInstanceId

            //               //join sup in _unitOfWork.Repository<SuppDryDock>().Query().Tracking(true).Select()
            //               //on an.VCN equals sup.VCN
            //               //join supw in _unitOfWork.Repository<WorkflowInstance>().Query().Tracking(true).Select()
            //               //on an.WorkflowInstanceId equals supw.WorkflowInstanceId        

            //               //join sup in _unitOfWork.Repository<SuppDryDock>().Query().Tracking(true).Select() on an.VCN equals sup.VCN
            //               //into t3
            //               //from rt3 in t3.DefaultIfEmpty()

            //               where an.PortCode == portcode && an.WorkflowInstance.WorkflowTaskCode == WFStatus.Approved && ar.Reason == SuperCategoryConstants.Reason_DryDock && an.AgentID == usertypeid
            //               && an.VCN.ToUpperInvariant().Contains(searchvalue.ToUpperInvariant())
            //               orderby an.VCN
            //               select new ServiceRequestVCNDetails
            //               {
            //                   VCN = an.VCN,
            //                   VesselID = vs.VesselID,
            //                   VesselName = vs.VesselName,
            //                   VoyageIn = an.VoyageIn,
            //                   VoyageOut = an.VoyageOut,
            //                   ReasonForVisit = an.ReasonForVisit,
            //                   VesselType = vs.VesselType,
            //                   CallSign = vs.CallSign,
            //                   ETA = an.ETA,
            //                   ETD = an.ETD,
            //                   IMONo = vs.IMONo,
            //                   LengthOverallInM = vs.LengthOverallInM,
            //                   BeamInM = vs.BeamInM,
            //                   ArrDraft = an.ArrDraft,
            //                   VesselNationality = vs.VesselNationality,
            //                   GrossRegisteredTonnageInMT = vs.GrossRegisteredTonnageInMT,
            //                   DeadWeightTonnageInMT = vs.DeadWeightTonnageInMT,
            //                   LastPortOfCall = an.LastPortOfCall,
            //                   NextPortOfCall = an.NextPortOfCall,
            //                   Tidal = an.Tidal,
            //                   DaylightRestriction = an.DaylightRestriction,
            //                   // CurrentBerth = vc.Bollard.Berth.BerthName + '-' + vc.Bollard1.Berth.BerthName,
            //                   //  CurrentBerth = rt.BerthName + "-" + rt1.BerthName,
            //                   AnyDangerousGoodsonBoard = an.AnyDangerousGoodsonBoard == "I" ? "Not Binded" : "Yes",
            //                   DangerousGoodsClass = an.DangerousGoodsClass != null ? an.DangerousGoodsClass : "Not Binded",
            //                   UNNo = an.UNNo != null ? an.UNNo : "Not Binded",
            //                   CurrentBerth = vc.ToPositionBerthCode != null ? vc.ToPositionBerthCode : "NA"

            //               }).ToList();


            //  return vcndtls;

        }

        public CompanyVO GetUserDetails(int userid)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == userid
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }


        public SuppDryDockVO GetSuppDryDockVCN(string vcn)
        {

            var vessel = _unitOfWork.SqlQuery<SuppDryDockVO>("SELECT MAX(SUP.SuppDryDockID) as SuppDryDockID, SUP.VCN,  WF.WorkflowTaskCode as SuppWFStatus , SUP.LeftDockDateTime as LeftDockDateTime1 FROM SuppDryDock SUP join WorkflowInstance WF on SUP.WorkflowInstanceID = WF.WorkflowInstanceID WHERE SUP.VCN = @p0 GROUP BY SUP.VCN, WF.WorkflowTaskCode, SUP.LeftDockDateTime", vcn);

            return vessel.FirstOrDefault<SuppDryDockVO>();



            //var suppdry = (from sup in _unitOfWork.Repository<SuppDryDock>().Query()
            //             .Include(sup => sup.WorkflowInstance)
            //                   // .Include(vc => vc.Agent.AuthorizedContactPerson)
            //             .Select()
            //               //  where sup.VCN == vcn &&  sup.SuppDryDockID == (from s in _unitOfWork.Repository<SuppDryDock>().Query().Select().Where(a=>a.SuppDryDockID == sup.SuppDryDockID).Max(b=>b.SuppDryDockID)  select s.SuppDryDockID)


            //               select new SuppDryDockVO
            //               {
            //                   SuppDryDockID = sup.SuppDryDockID,
            //                   SuppWFStatus = sup.WorkflowInstance.WorkflowTaskCode,
            //                   LeftDockDateTime = sup.LeftDockDateTime != null ? Convert.ToString(sup.LeftDockDateTime) : null


            //               }).FirstOrDefault();




            //return suppdry;



            //   var supp1 = 
        }


    }
}
