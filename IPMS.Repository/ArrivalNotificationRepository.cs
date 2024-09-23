using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace IPMS.Repository
{
    public class ArrivalNotificationRepository : IArrivalNotificationRepository
    {
        private IUnitOfWork _unitOfWork;        

        public ArrivalNotificationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            
        }

        /// <summary>
        /// To Get Arrivalnotification Details by VCN
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ArrivalNotificationDetails GetArrivalNotificationById(string value)
        {           
            var arrivalDetails = _unitOfWork.SqlQuery<ArrivalNotificationDetails>("select VCN,ETA,ETD,IMONo, PortCode,VesselName,PreferredBerthName,AlternateBerthName from dbo.udf_GetArrivalNotification() where VCN = @p0", value).FirstOrDefault<ArrivalNotificationDetails>();


            return arrivalDetails;
        }
        /// <summary>
        /// To Get the Arrival Notification record based on VCN
        /// </summary>
        /// <param name="VCN"></param>
        public ArrivalNotification GetArrivalNotificationByVcn(string vcn)
        {
            var andata = new ArrivalNotification();
            
            andata = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.VCN == vcn).Tracking(true).Include(t => t.SubCategory3)
                         .Include(t => t.Vessel)
                           .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                         .Include(t => t.TerminalOperator)
                         .Include(s => s.Vessel.SubCategory2)
                         .Include(w => w.Vessel.SubCategory3)
                         .Include(t => t.ArrivalCommodities)
                         .Include(t => t.ArrivalIMDGTankers)
                         .Include(t => t.IMDGInformations)
                         .Include(t => t.WasteDeclarations)
                         .Include(t => t.ArrivalDocuments)
                         .Include(t => t.ArrivalReasons)
                         .Include(p => p.ArrivalDocuments.Select(d => d.SubCategory))
                         .Include(t => t.VesselArrestImmobilizationSAMSAs)
                         .Include(t => t.WorkflowInstance.SubCategory).Select()
                      orderby t.CreatedDate descending
                      select t).FirstOrDefault<ArrivalNotification>();         
            return andata;

        }

        /// <summary>
        /// To Get the Arrival Notification record(s) based on PortCode
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<ArrivalNotification> GetArrivalNotificationSearch(string portCode, string userType, int userTypeId, int userId, string etaFrom, string etaTo)
        {
            var andataAll = new List<ArrivalNotification>();
            var andata = new List<ArrivalNotification>();           

            if (userType == GlobalConstants.AGENT)
            {
                andataAll = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.PortCode == portCode && t.Isdraft != "Y" && t.CreatedBy == userId).Include(t => t.SubCategory3)
                             .Include(t => t.Vessel)
                               .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                             .Include(t => t.TerminalOperator)
                             .Include(s => s.Vessel.SubCategory2)
                             .Include(w => w.Vessel.SubCategory3)
                             .Include(t => t.ArrivalCommodities)
                             .Include(t => t.ArrivalIMDGTankers)
                             .Include(t => t.IMDGInformations)
                             .Include(t => t.WasteDeclarations)
                             .Include(t => t.ArrivalDocuments)
                             .Include(t => t.ArrivalReasons)
                             .Include(p => p.ArrivalDocuments.Select(d => d.SubCategory))
                             .Include(t => t.VesselArrestImmobilizationSAMSAs)
                             .Include(t => t.WorkflowInstance.SubCategory).Select()
                             orderby t.CreatedDate descending
                             select t).ToList<ArrivalNotification>();
            }
            else
            {
                andataAll = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.PortCode == portCode && t.Isdraft != "Y").Include(t => t.SubCategory3)
                              .Include(t => t.Vessel)
                                .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                              .Include(t => t.TerminalOperator)
                              .Include(s => s.Vessel.SubCategory2)
                              .Include(w => w.Vessel.SubCategory3)
                              .Include(t => t.ArrivalCommodities)
                              .Include(t => t.ArrivalIMDGTankers)
                              .Include(t => t.IMDGInformations)
                              .Include(t => t.WasteDeclarations)
                              .Include(t => t.ArrivalDocuments)
                              .Include(t => t.ArrivalReasons)
                              .Include(p => p.ArrivalDocuments.Select(d => d.SubCategory))
                              .Include(t => t.VesselArrestImmobilizationSAMSAs)
                              .Include(t => t.WorkflowInstance.SubCategory).Select()
                             orderby t.CreatedDate descending
                             select t).ToList<ArrivalNotification>();
            }

            andata = andataAll.Where(vcm => (DateTime.Parse(vcm.ETA.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) >= DateTime.Parse(etaFrom, CultureInfo.InvariantCulture)) && (DateTime.Parse(vcm.ETA.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) <= DateTime.Parse(etaTo, CultureInfo.InvariantCulture))).ToList();
                       
            return andata;
        }

        public List<ArrivalNotificationGridVO> GetArrivalNotificationByPortCodeGrid(string fromDate, string toDate, string vcn, string vesselId, string imdg, string isps, string portCode, string suserType, int userTypeId, int userId, string imdgClear, string ispsClear, string phoClear)
        {
            var portcode = new SqlParameter("@A_PortCode", portCode);
            var usertype = new SqlParameter("@A_UserType", suserType);
            var usertypeid = new SqlParameter("@A_UserTypeid", userTypeId);
            var userid = new SqlParameter("@A_Userid", userId);

            var Sfrmdt = new SqlParameter("@A_frmdt", fromDate);
            var Stodt = new SqlParameter("@A_todt", toDate);
            var Svcn = new SqlParameter("@A_vcn", vcn);
            var Sveselid = new SqlParameter("@A_veselid", vesselId);
            var Simdg = new SqlParameter("@A_imdg", imdg);
            var Sisps = new SqlParameter("@A_isps", isps);

            var SimdgCleae = new SqlParameter("@A_imdgClear", imdgClear);
            var SispsClear = new SqlParameter("@A_ispsClear", ispsClear);
            var SphoClear = new SqlParameter("@A_phoClear", phoClear);


            var arrvNotification = _unitOfWork.SqlQuery<ArrivalNotificationGridVO>("dbo.usp_GetArrivalGridDetails  @A_PortCode, @A_UserType, @A_UserTypeid, @A_Userid ,@A_frmdt, @A_todt, @A_vcn, @A_veselid, @A_imdg, @A_isps, @A_imdgClear, @A_ispsClear, @A_phoClear",
                portcode, usertype, usertypeid, userid, Sfrmdt, Stodt, Svcn, Sveselid, Simdg, Sisps, SimdgCleae, SispsClear, SphoClear).ToList();
            return arrvNotification;
        }



        public List<ArrvWorkflowStatusVo> GetNotificationStatus(string vcn)
        {
            var VCN = new SqlParameter("@vcn", vcn);
            var arrvNotification = _unitOfWork.SqlQuery<ArrvWorkflowStatusVo>("dbo.usp_GetArrivalworkflowStatus @vcn", VCN).ToList();
            return arrvNotification;
        }


        /// <summary>
        /// To Get the Arrival Notification record(s) based on PortCode
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<ArrivalNotification> GetArrivalNotificationByPortCode(string portCode, string userType, int userTypeId, int userId)
        {
            var andata = new List<ArrivalNotification>();           

            if (userType == GlobalConstants.AGENT)
            {
                andata = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.PortCode == portCode && t.Isdraft != "Y" && t.CreatedBy == userId).Include(t => t.SubCategory3)
                             .Include(t => t.Vessel)
                               .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                              .Include(t => t.Vessel.DockingPlans)
                             .Include(t => t.VesselCalls)
                             .Include(t => t.TerminalOperator)
                             .Include(s => s.Vessel.SubCategory2)
                             .Include(w => w.Vessel.SubCategory3)
                             .Include(t => t.ArrivalCommodities)
                             .Include(t => t.ArrivalIMDGTankers)
                             .Include(t => t.IMDGInformations)
                             .Include(t => t.WasteDeclarations)
                             .Include(t => t.ArrivalDocuments)
                             .Include(t => t.ArrivalReasons)
                             .Include(p => p.ArrivalDocuments.Select(d => d.SubCategory))
                             .Include(t => t.VesselArrestImmobilizationSAMSAs)
                             .Include(t => t.WorkflowInstance.SubCategory).Tracking(true).Select()
                          orderby t.CreatedDate descending
                          select t).ToList<ArrivalNotification>();
            }
            else
            {
                andata = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.PortCode == portCode && t.Isdraft != "Y").Include(t => t.SubCategory3)
                              .Include(t => t.Vessel)
                                .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                              .Include(t => t.Vessel.DockingPlans)
                              .Include(t => t.VesselCalls)
                              .Include(t => t.TerminalOperator)
                              .Include(s => s.Vessel.SubCategory2)
                              .Include(w => w.Vessel.SubCategory3)
                              .Include(t => t.ArrivalCommodities)
                              .Include(t => t.ArrivalIMDGTankers)
                              .Include(t => t.IMDGInformations)
                              .Include(t => t.WasteDeclarations)
                              .Include(t => t.ArrivalDocuments)
                              .Include(t => t.ArrivalReasons)
                              .Include(p => p.ArrivalDocuments.Select(d => d.SubCategory))
                              .Include(t => t.VesselArrestImmobilizationSAMSAs)
                              .Include(t => t.WorkflowInstance.SubCategory).Tracking(true).Select()                          
                          orderby t.CreatedDate descending
                          select t).ToList<ArrivalNotification>();
            }
          
            return andata;
        }
        /// <summary>
        /// To Get the Arrival Notification record(s) based on AgentId
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public List<ArrivalNotification> GetArrivalNotificationsByAgentId(int agentId)
        {
            var andata = new List<ArrivalNotification>();
           
            andata = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.AgentID == agentId && t.Isdraft != "Y")
                          .Include(t => t.Vessel)
                            .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                          .Include(t => t.TerminalOperator)
                          .Include(t => t.ArrivalCommodities)
                          .Include(t => t.ArrivalIMDGTankers)
                          .Include(t => t.IMDGInformations)
                          .Include(t => t.WasteDeclarations)
                          .Include(t => t.ArrivalDocuments)
                          .Include(t => t.ArrivalReasons)
                          .Include(p => p.ArrivalDocuments.Select(d => d.SubCategory))
                          .Include(t => t.VesselArrestImmobilizationSAMSAs)
                          .Include(t => t.WorkflowInstance).Select()
                      orderby t.CreatedDate descending
                      select t).ToList<ArrivalNotification>();
           
            return andata;
        }

        public List<ArrivalNotificationDraftVO> GetArrivalNotificationsDrafts(string portCode, int agentId)
        {
            var _arrivalnotificationbyportcodeagentidDraft = new List<ArrivalNotification>();
          
            _arrivalnotificationbyportcodeagentidDraft = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.AgentID == agentId && t.PortCode == portCode && t.Isdraft == "Y" && (String.IsNullOrEmpty(t.GeneratedVCN))).Include(t => t.Vessel).Select()                                                          
                                                          orderby t.CreatedDate descending
                                                          select t
                                                          ).ToList<ArrivalNotification>();
            return _arrivalnotificationbyportcodeagentidDraft.DraftMapToDto();
           

        }

        public List<TerminalOperatorVO> GetBirthingTo(string portCode)
        {
            var berths = (from p in _unitOfWork.Repository<TerminalOperatorPort>().Queryable()
                          join t in _unitOfWork.Repository<TerminalOperator>().Queryable()
                          on p.TerminalOperatorID equals t.TerminalOperatorID
                          where p.PortCode == portCode && t.LicensedFor != "TOCH" && t.RecordStatus == "A" && p.RecordStatus == "A"
                          select new TerminalOperatorVO
                          {
                              TerminalOperatorID = t.TerminalOperatorID,
                              RegisteredName = t.RegisteredName,
                          }).OrderBy(x=>x.RegisteredName).ToList<TerminalOperatorVO>();
            return berths;
        }

        ///
        /// <summary>
        /// To get the Arrival Notification details based on Portcode and Agent Id
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public List<ArrivalNotificationVO> GetArrivalNotificationsByPortCodeAgentId(string portCode, int agentId)
        {
            var _arrivalnotificationbyportcodeagentid = new List<ArrivalNotification>();
            //try
            //{
            _arrivalnotificationbyportcodeagentid = (from t in _unitOfWork.Repository<ArrivalNotification>().Query(t => t.AgentID == agentId && t.PortCode == portCode && t.Isdraft != "Y")
                                                         .Include(t => t.Vessel)
                                                           .Include(t => t.ArrivalAgents.Select(n => n.Agent))
                                                          .Include(t => t.TerminalOperator)
                                                         .Include(t => t.ArrivalCommodities)
                                                         .Include(t => t.ArrivalIMDGTankers)
                                                         .Include(t => t.IMDGInformations)
                                                         .Include(t => t.ArrivalDocuments)
                                                         .Include(t => t.ArrivalReasons)
                                                         .Include(p => p.ArrivalDocuments.Select(d => d.SubCategory))
                                                         .Include(t => t.VesselArrestImmobilizationSAMSAs)
                                                         .Include(t => t.WorkflowInstance).Select()
                                                     orderby t.CreatedDate descending
                                                     select t).ToList<ArrivalNotification>();
            return _arrivalnotificationbyportcodeagentid.MapToDto();         

        }

        /// <summary>
        /// Author   : Sandeep Appana
        /// Date     : 27-8-2014
        /// Purpose  : To Get Arrival Commodity record(s) based on vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<ArrivalCommodityVo> GetArrivalCommoditiesByVcn(string vcn)
        {
            var ArrivalCommodities = new List<ArrivalCommodityVo>();

            //try
            //{
            ArrivalCommodities = (from ac in _unitOfWork.Repository<ArrivalCommodity>().Queryable().Where(t => t.VCN == vcn)
                                  .Include(ac => ac.Berth)
                                   .Include(ac => ac.SubCategory)                                  
                                  select new ArrivalCommodityVo
                                  {
                                      VCN = ac.VCN,
                                      BerthCode = ac.BerthCode,
                                      BerthName = ac.Berth != null ? ac.Berth.BerthName : null,
                                      CargoType = ac.CargoType,
                                      CargoName = ac.SubCategory != null ? ac.SubCategory.SubCatName : null,
                                      Package = ac.Package,
                                      PackageName = ac.SubCategory1 != null ? ac.SubCategory1.SubCatName : null,
                                      UOM = ac.UOM,
                                      UOMName = ac.SubCategory2 != null ? ac.SubCategory2.SubCatName : null,
                                      Quantity = ac.Quantity

                                  }).ToList();         

            return ArrivalCommodities;

        }
     

        public bool IsIspsClearanceRole(int userId)
        {
            bool Isisps = false;
          
            var arrivalDetails = (from u in _unitOfWork.Repository<Role>().Queryable().AsEnumerable<Role>()
                                  join r in _unitOfWork.Repository<UserRole>().Queryable().AsEnumerable<UserRole>()
                               on u.RoleID equals r.RoleID
                                  where r.UserID == userId
                                  select u).ToList<Role>();


            foreach (var item in arrivalDetails)
            {

                if (item.RoleCode == "ISPS")
                {
                    Isisps = true;
                }
           
            }
            return Isisps;
        }

        [EdmFunction("dbo", "udf_GetArrivalReasonForVisit")]
        public string GetArrivalReasonForVisit(string pvcn)
        {
            {
                var vcn = new SqlParameter("@p_VCN", pvcn);
                var areason = _unitOfWork.SqlQuery<string>("Select dbo.udf_GetArrivalReasonForVisit(@p_VCN)", vcn).Single();
                return areason;
            }
        }

        public List<RevenuePostingVO> GetArrivalVcnDetailsForAutocomplete(string searchValue, string portCode, string userType, int userId)
        {
            if (userType == GlobalConstants.AGENT)
            {
                var vcndtls = (from an in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true).Select()
                               where an.PortCode == portCode && an.RecordStatus == "A" && an.Isdraft == "N" && an.CreatedBy == userId
                               orderby an.VCN
                               select an).Where(x => x.VCN.ToUpperInvariant().Contains(searchValue.ToUpperInvariant())).ToList<ArrivalNotification>();
                List<RevenuePostingVO> vcnlistVO = new List<RevenuePostingVO>();
                foreach (var an in vcndtls)
                {
                    RevenuePostingVO vcnlist = new RevenuePostingVO();
                    vcnlist.vcn = an.VCN;
                    vcnlistVO.Add(vcnlist);

                }
                return vcnlistVO;
            }
            else
            {
                var vcndtls = (from an in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true).Select()
                               where an.PortCode == portCode && an.RecordStatus == "A" && an.Isdraft == "N"
                               orderby an.VCN
                               select an).Where(x => x.VCN.ToUpperInvariant().Contains(searchValue.ToUpperInvariant())).ToList<ArrivalNotification>();
                List<RevenuePostingVO> vcnlistVO = new List<RevenuePostingVO>();
                foreach (var an in vcndtls)
                {
                    RevenuePostingVO vcnlist = new RevenuePostingVO();
                    vcnlist.vcn = an.VCN;
                    vcnlistVO.Add(vcnlist);
                }
                return vcnlistVO;
            }
        }

        public WorkflowInstance GetTidalWorkflowStatusByVcn(string entityCode, string referenceId)
        {
            var tidalworkflow = new WorkflowInstance();         

            tidalworkflow = (from wfinstance in _unitOfWork.Repository<WorkflowInstance>().Query().Tracking(true).Select()
                             join e in _unitOfWork.Repository<Entity>().Query().Tracking(true).Select()
                              on wfinstance.EntityID equals e.EntityID
                             where e.EntityCode == entityCode &&
                            wfinstance.ReferenceID == referenceId
                             select wfinstance).FirstOrDefault<WorkflowInstance>();        

            return tidalworkflow;
        }

        public List<VesselVO> GetAgentArrivalVesselSearch(string PortCode, string searchValue, int userid)
        {

            var portcode = new SqlParameter("@p_PortCode", PortCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var userId = new SqlParameter("@p_userid", userid);

            var _VesselInfo = _unitOfWork.SqlQuery<VesselVO>("dbo.usp_GetAgentArrivalVesselSearch @p_SearchText, @p_PortCode, @p_userid", Searchvalue, portcode, userId).ToList();


            return _VesselInfo;
        }

        public string ArrivalNotificationVoyageValidation(int vesselid, string voyagein, string voyageout,string portcode)
        {
            string anVoyage = string.Empty;
            var anVCN = _unitOfWork.Repository<ArrivalNotification>().Queryable()
                .Join(_unitOfWork.Repository<WorkflowInstance>().Queryable(), an => an.WorkflowInstanceId, wf => wf.WorkflowInstanceId, (an, wf) => new { an, wf })
                .Where(x => x.an.PortCode == portcode)
                .Where(x => x.an.VesselID == vesselid)
                .Where(x => x.an.Isdraft == "N")
                .Where(x => x.an.IsANFinal == "Y")
                .Where(x => x.an.RecordStatus == RecordStatus.InActive)
                .Where(x => x.wf.WorkflowTaskCode == WFStatus.Reject)
                .Where(x => (x.an.VoyageIn == voyagein || x.an.VoyageOut == voyageout)).OrderByDescending(x => x.an.VCN).Take(1).Select(x => x.an.VCN).FirstOrDefault();
            
            if (anVCN != null && anVCN.Count() > 0)
            {
                anVoyage = string.Concat("1@", anVCN);
            }
            else
            {
                anVoyage = "0@0";
            }
            return anVoyage;
        }        

        public List<MarpolGroupVO> Marpol()
        {

            List<MarpolGroupVO> marpolGroupList = new List<MarpolGroupVO>();

            List<Marpol> marpolDetails = (from cg in _unitOfWork.Repository<Marpol>().Queryable()
                                          join sc in _unitOfWork.Repository<SubCategory>().Queryable()
                                          on cg.MarpolCode equals sc.SubCatCode
                                          orderby sc.SubCatName.ToLower() ascending
                                          where cg.RecordStatus == RecordStatus.Active
                                          select new 
                                          {
                                              MarpolCode = cg.MarpolCode,
                                              MarpolName = sc.SubCatName,
                                              ClassCode = cg.ClassCode,
                                              ClassName = cg.ClassName,
                                          }).ToList().Select(v => new Marpol()
                                          {
                                              MarpolCode = v.MarpolCode,
                                              MarpolName = v.MarpolName,
                                              ClassCode = v.ClassCode,
                                              ClassName = v.ClassName                                             
                                          }).ToList<Marpol>();

            var distinctItems = marpolDetails.GroupBy(x => x.MarpolCode).Select(y => y.First());


            foreach (var mrpGrp in distinctItems)
            {
                MarpolGroupVO marpolGroup = new MarpolGroupVO
                {
                    MarpolDetails = new List<MarpolVO>(),
                    MarpolCode = mrpGrp.MarpolCode,
                    MarpolName = mrpGrp.MarpolName
                };

                foreach (var item in marpolDetails)
                {
                    if (item.MarpolCode == mrpGrp.MarpolCode)
                    {
                        MarpolVO cg = new MarpolVO
                        {
                            ClassName = item.ClassName,
                            ClassCode = item.ClassCode,
                            MarpolCode = item.MarpolCode,
                            MarpolName = item.MarpolName
                        };
                        marpolGroup.MarpolDetails.Add(cg);
                    }
                }

                marpolGroupList.Add(marpolGroup);
            }

            return marpolGroupList;

        }

    }
}




