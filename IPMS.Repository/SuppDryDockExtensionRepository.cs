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
using System.Data.Entity.Core.Objects.DataClasses;
using System.Globalization;


namespace IPMS.Repository
{
    public class SuppDryDockExtensionRepository : ISuppDryDockExtensionRepository
    {
        private IUnitOfWork _unitOfWork;
       // private readonly ILog log;
        private IVesselAgentChangeRepository _vesselAgentChangeRepository;
      //  private IUserRepository _userRepository;

        public SuppDryDockExtensionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
         //   log = 
           LogManager.GetLogger(typeof(SuppDryDockExtensionRepository));
           _vesselAgentChangeRepository = new VesselAgentChangeRepository(_unitOfWork);
       //    _userRepository = new UserRepository(_unitOfWork);
        }
        public List<ServiceRequestVCNsForDryDockExts> GetSuppVCNDetailsForExtension(string portcode, int userid)
        {
            var suppDryDockAppList = new List<ServiceRequestVCNsForDryDockExts>();
            int agentId = _vesselAgentChangeRepository.GetAgentId(portcode, userid);

            //List<int> userslist = new List<int>();
            //userslist = _userRepository.GetUserIdsByAgentID(agentId);

            //int[] userids = userslist.ToArray();

            if (agentId != 0)
            {
                suppDryDockAppList = _unitOfWork.SqlQuery<ServiceRequestVCNsForDryDockExts>("select distinct dd.VCN as VCN,an.VesselID,vsl.VesselName as VesselName,dd.SuppDryDockID as SuppDryDockID, dd.ModifiedDate as  ModifiedDate,vsl.LengthOverallInM as LengthOverallInM,vsl.GrossRegisteredTonnageInMT as GrossRegisteredTonnageInMT,vsl.IMONo as IMONo,vsl.BeamInM As BeamInM, an.ArrDraft As ArrDraft from ArrivalNotification an JOIN WorkflowInstance wf1 on an.WorkflowInstanceId = wf1.WorkflowInstanceId JOIN Vessel vsl on vsl.VesselID = an.VesselID JOIN VesselCall vc on vc.VCN = an.VCN JOIN SuppDryDock dd on dd.VCN = an.VCN Left JOIN SuppDryDockExtension  ddex on ddex.SuppDryDockID = dd.SuppDryDockID Where an.PortCode = '" + portcode + "' and dd.ScheduleStatus = '" + SuperCategoryConstants.DOCK_TYPE + "' AND vc.RecentAgentID = '" + agentId +"' AND dd.SuppDryDockID not in (select SuppDryDockID From SuppDryDockExtension Where ScheduleStatus in( '" + WFStatus.NewRequest + "','" + WFStatus.Update + "')) AND dd.DockBerthCode is not null AND dd.EnteredDockDateTime is not null AND dd.OnBlocksDateTime is not null AND dd.DryDockDateTime is not  null AND dd.FinishedDockDateTime is null AND dd.OffBlocksDateTime is null AND dd.LeftDockDateTime is null AND dd.ScheduleFromDate is not null AND dd.ScheduleToDate is not null order by dd.ModifiedDate desc").ToList();
            }
            else
            {
                suppDryDockAppList = _unitOfWork.SqlQuery<ServiceRequestVCNsForDryDockExts>("select distinct dd.VCN as VCN,an.VesselID,vsl.VesselName as VesselName,dd.SuppDryDockID as SuppDryDockID, dd.ModifiedDate as  ModifiedDate,vsl.LengthOverallInM as LengthOverallInM,vsl.GrossRegisteredTonnageInMT as GrossRegisteredTonnageInMT,vsl.IMONo as IMONo,vsl.BeamInM As BeamInM, an.ArrDraft As ArrDraft from ArrivalNotification an JOIN WorkflowInstance wf1 on an.WorkflowInstanceId = wf1.WorkflowInstanceId JOIN Vessel vsl on vsl.VesselID = an.VesselID JOIN SuppDryDock dd on dd.VCN = an.VCN Left JOIN SuppDryDockExtension  ddex on ddex.SuppDryDockID = dd.SuppDryDockID Where an.PortCode = '" + portcode + "' and dd.ScheduleStatus = '" + SuperCategoryConstants.DOCK_TYPE + "' AND dd.SuppDryDockID not in (select SuppDryDockID From SuppDryDockExtension Where ScheduleStatus in( '" + WFStatus.NewRequest + "','" + WFStatus.Update + "')) AND dd.DockBerthCode is not null AND dd.EnteredDockDateTime is not null AND dd.OnBlocksDateTime is not null AND dd.DryDockDateTime is not  null AND dd.FinishedDockDateTime is null AND dd.OffBlocksDateTime is null AND dd.LeftDockDateTime is null AND dd.ScheduleFromDate is not null AND dd.ScheduleToDate is not null order by dd.ModifiedDate desc").ToList();
            }
            
            return suppDryDockAppList;

        }

        public AgentVO GetSuppVCNDetailsForExtensionByVCN(string vcn)
        {
            return GetSuppExtensionArrival(vcn);
        }

        private AgentVO GetSuppExtensionArrival(string vcn)
        {
            var agent = (from vc in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true)
                         .Include(vc => vc.Vessel)
                         .Include(vc => vc.VesselCalls)
                         .Include(vc => vc.Agent)
                         .Include(vc => vc.Agent.AuthorizedContactPerson)
                         .Select()
                         join dd in _unitOfWork.Repository<SuppDryDock>().Query().Tracking(true)
                         .Include(dd => dd.SuppDryDockDocuments.Select(d => d.Document))
                         .Select()
                          on vc.VCN equals dd.VCN
                         join bh in _unitOfWork.Repository<Berth>().Query().Tracking(true).Select()
                         on dd.DockBerthCode equals bh.BerthCode
                         where vc.VCN == vcn && dd.ScheduleFromDate != null && dd.ScheduleToDate != null
                                      && dd.EnteredDockDateTime != null && dd.OnBlocksDateTime != null && dd.DryDockDateTime != null
                                      && dd.FinishedDockDateTime == null && dd.OffBlocksDateTime == null && dd.LeftDockDateTime == null
                                      && dd.ScheduleStatus == SuperCategoryConstants.DOCK_TYPE
                                      && dd.DockBerthCode != null
                         select new AgentVO
                         {
                             RegisteredName = vc.Agent.RegisteredName,
                             TradingName = vc.Agent.TradingName,
                             TelephoneNo1 = vc.Agent.TelephoneNo1,
                             FaxNo = vc.Agent.FaxNo,
                             TelephoneNo2 = vc.Agent.AuthorizedContactPerson.CellularNo,
                             FromDate = dd.FromDate,
                             ToDate = dd.ToDate,
                             CargoTons = dd.CargoTons ?? 0,
                             Ballast = dd.Ballast ?? 0,
                             Bunkers = dd.Bunkers ?? 0,
                             ScheduleFromDate = dd.ScheduleFromDate,
                             ScheduleToDate = dd.ScheduleToDate,
                             SuppDryDockID = dd.SuppDryDockID,
                             SuppDryDockDocument = dd.SuppDryDockDocuments.MapToDto(),
                             CurrentBerth = bh.BerthName,
                             LengthOverallInM = vc.Vessel.LengthOverallInM ?? 0
                         }).FirstOrDefault();

            return agent;
        }

        /// <summary>
        /// Author  : Omprakash K
        /// Date    : 24th Dec 2014
        /// Purpose : To Get Dry Dock Extension details
        /// </summary>
        /// <returns></returns>
        public List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionList(string portcode)
        {
            return GetSuppDryDockList(portcode);


        }

        private List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockList(string portcode)
        {
            var suppDryDockAppList = (from sr in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true)
                                      .Include(sr => sr.Vessel)
                                      .Include(sr => sr.VesselCalls)
                                      .Include(sr => sr.WorkflowInstance)
                                      .Include(sr => sr.VesselCalls.Select(a => a.Agent))
                                      .Include(sr => sr.VesselCalls.Select(a => a.Agent.AuthorizedContactPerson))
                                      .Select()
                                      join dd in _unitOfWork.Repository<SuppDryDock>().Query().Tracking(true)
                                      .Include(dd => dd.SuppDryDockDocuments.Select(d => d.Document))
                                      .Select()
                                      on sr.VCN equals dd.VCN
                                      join ddex in _unitOfWork.Repository<SuppDryDockExtension>().Query().Tracking(true).Select()
                                      on dd.SuppDryDockID equals ddex.SuppDryDockID
                                      join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                                      on ddex.ScheduleStatus equals sc.SubCatCode
                                      join bh in _unitOfWork.Repository<Berth>().Query().Tracking(true).Select()
                                      on dd.DockBerthCode equals bh.BerthCode
                                      where sr.PortCode == portcode && dd.ScheduleStatus != WFStatus.Reject //&& ddex.ScheduleStatus != WFStatus.Reject
                                      && dd.DockBerthCode != null
                                      orderby ddex.ModifiedDate descending

                                      select new ServiceRequestVCNsForDryDockExts
                                      {
                                          VCN = sr.VCN,
                                          VesselID = sr.VesselID,
                                          VesselName = sr.Vessel.VesselName,
                                          VoyageIn = sr.VoyageIn,
                                          VoyageOut = sr.VoyageOut,
                                          //ReasonForVisit = sr.ReasonForVisit,
                                          ReasonForVisit = null,
                                          VesselType = sr.Vessel.VesselType,
                                          CallSign = sr.Vessel.CallSign,
                                          ETA = sr.ETA,
                                          ETD = sr.ETD,
                                          IMONo = sr.Vessel.IMONo,
                                          LengthOverallInM = sr.Vessel.LengthOverallInM ?? 0,
                                          BeamInM = sr.Vessel.BeamInM,
                                          ArrDraft = sr.ArrDraft,
                                          VesselNationality = sr.Vessel.VesselNationality,
                                          GrossRegisteredTonnageInMT = sr.Vessel.GrossRegisteredTonnageInMT,
                                          DeadWeightTonnageInMT = sr.Vessel.DeadWeightTonnageInMT,
                                          LastPortOfCall = sr.LastPortOfCall,
                                          NextPortOfCall = sr.NextPortOfCall,
                                          Tidal = sr.Tidal,
                                          DaylightRestriction = sr.DaylightRestriction,

                                          AnyDangerousGoodsonBoard = sr.AnyDangerousGoodsonBoard == "I" ? "Not Binded" : "Yes",
                                          DangerousGoodsClass = sr.DangerousGoodsClass != null ? sr.DangerousGoodsClass : "Not Binded",
                                          UNNo = sr.UNNo != null ? sr.UNNo : "Not Binded",
                                          SuppDryDockID = dd.SuppDryDockID,
                                          SuppDryDockDocuments = dd.SuppDryDockDocuments.MapToDto(),
                                          SuppDryDockExtensionID = ddex.SuppDryDockExtensionID,
                                          AgentID = sr.AgentID,
                                          Ballast = dd.Ballast,
                                          BarkeelCode = dd.BarkeelCode,
                                          Bunkers = dd.Bunkers,
                                          CargoTons = dd.CargoTons,
                                          Chamber = dd.Chamber,
                                          CreatedBy = ddex.CreatedBy,
                                          CreatedDate = ddex.CreatedDate,
                                          DockBerthCode = dd.DockBerthCode,
                                          DockPortCode = dd.DockPortCode,
                                          DockQuayCode = dd.DockQuayCode,
                                          ExtensionDateTime = ddex.ExtensionDateTime,
                                          FaxNo = sr.Agent.FaxNo,
                                          RecordStatus = ddex.RecordStatus,
                                          FromDate = dd.FromDate,
                                          ToDate = dd.ToDate,
                                          ModifiedBy = ddex.ModifiedBy,
                                          ModifiedDate = ddex.ModifiedDate,
                                          RegisteredName = sr.Agent.RegisteredName,
                                          Remarks = ddex.Remarks,
                                          ScheduleFromDate = dd.ScheduleFromDate,
                                          ScheduleToDate = dd.ScheduleToDate,
                                          ScheduleStatus = ddex.ScheduleStatus,
                                          ScheduleStatusText = GetGetWorkflowStatusforGrid(sc.SubCatCode),
                                          TelephoneNo1 = sr.Agent.TelephoneNo1,
                                          TelephoneNo2 = sr.Agent.AuthorizedContactPerson.CellularNo,
                                          TradingName = sr.Agent.TradingName,
                                          WorkflowInstanceID = ddex.WorkflowInstanceID ?? 0,
                                          CurrentBerth = bh.BerthName



                                      }).ToList();

            return suppDryDockAppList;
        }


        public List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionByID(string portcode, string SuppDryDockExtensionID)
        {
            return GetSuppDryDockExtByID(portcode, SuppDryDockExtensionID);

        }

        private List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtByID(string portcode, string SuppDryDockExtensionID)
        {
            var suppDryDockAppList = (from sr in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true)
                                      .Include(sr => sr.Vessel)
                                      .Include(sr => sr.VesselCalls)
                                      .Include(sr => sr.WorkflowInstance)
                                      .Include(sr => sr.VesselCalls.Select(a => a.Agent))
                                      .Include(sr => sr.VesselCalls.Select(a => a.Agent.AuthorizedContactPerson))
                                      .Select()
                                      join dd in _unitOfWork.Repository<SuppDryDock>().Query().Tracking(true)
                                      .Include(dd => dd.SuppDryDockDocuments.Select(d => d.Document))
                                      .Select()
                                      on sr.VCN equals dd.VCN
                                      join ddex in _unitOfWork.Repository<SuppDryDockExtension>().Query().Tracking(true).Select()
                                      on dd.SuppDryDockID equals ddex.SuppDryDockID
                                      join sc in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
                                      on ddex.ScheduleStatus equals sc.SubCatCode
                                      join bh in _unitOfWork.Repository<Berth>().Query().Tracking(true).Select()
                                      on dd.DockBerthCode equals bh.BerthCode
                                      where sr.PortCode == portcode && ddex.SuppDryDockExtensionID == Convert.ToInt32(SuppDryDockExtensionID, CultureInfo.InvariantCulture)
                                      orderby sr.ModifiedDate descending

                                      select new ServiceRequestVCNsForDryDockExts
                                      {
                                          VCN = sr.VCN,
                                          VesselID = sr.VesselID,
                                          VesselName = sr.Vessel.VesselName,
                                          VoyageIn = sr.VoyageIn,
                                          VoyageOut = sr.VoyageOut,
                                          //ReasonForVisit = sr.ReasonForVisit,
                                          ReasonForVisit = null,
                                          VesselType = sr.Vessel.VesselType,
                                          CallSign = sr.Vessel.CallSign,
                                          ETA = sr.ETA,
                                          ETD = sr.ETD,
                                          IMONo = sr.Vessel.IMONo,
                                          LengthOverallInM = sr.Vessel.LengthOverallInM ?? 0,
                                          BeamInM = sr.Vessel.BeamInM,
                                          ArrDraft = sr.ArrDraft,
                                          VesselNationality = sr.Vessel.VesselNationality,
                                          GrossRegisteredTonnageInMT = sr.Vessel.GrossRegisteredTonnageInMT,
                                          DeadWeightTonnageInMT = sr.Vessel.DeadWeightTonnageInMT,
                                          LastPortOfCall = sr.LastPortOfCall,
                                          NextPortOfCall = sr.NextPortOfCall,
                                          Tidal = sr.Tidal,
                                          DaylightRestriction = sr.DaylightRestriction,

                                          AnyDangerousGoodsonBoard = sr.AnyDangerousGoodsonBoard == "I" ? "Not Binded" : "Yes",
                                          DangerousGoodsClass = sr.DangerousGoodsClass != null ? sr.DangerousGoodsClass : "Not Binded",
                                          UNNo = sr.UNNo != null ? sr.UNNo : "Not Binded",
                                          SuppDryDockID = dd.SuppDryDockID,
                                          SuppDryDockDocuments = dd.SuppDryDockDocuments.MapToDto(),
                                          SuppDryDockExtensionID = ddex.SuppDryDockExtensionID,
                                          AgentID = sr.AgentID,
                                          Ballast = dd.Ballast,
                                          BarkeelCode = dd.BarkeelCode,
                                          Bunkers = dd.Bunkers,
                                          CargoTons = dd.CargoTons,
                                          Chamber = dd.Chamber,
                                          CreatedBy = ddex.CreatedBy,
                                          CreatedDate = ddex.CreatedDate,
                                          DockBerthCode = dd.DockBerthCode,
                                          DockPortCode = dd.DockPortCode,
                                          DockQuayCode = dd.DockQuayCode,
                                          ExtensionDateTime = ddex.ExtensionDateTime,
                                          FaxNo = sr.Agent.FaxNo,
                                          RecordStatus = ddex.RecordStatus,
                                          FromDate = dd.FromDate,
                                          ToDate = dd.ToDate,
                                          ModifiedBy = ddex.ModifiedBy,
                                          ModifiedDate = ddex.ModifiedDate,
                                          RegisteredName = sr.Agent.RegisteredName,
                                          Remarks = ddex.Remarks,
                                          ScheduleFromDate = ddex.ScheduleFromDate,
                                          ScheduleToDate = ddex.ScheduleToDate,
                                          ScheduleStatus = ddex.ScheduleStatus,
                                          ScheduleStatusText = sc.SubCatName,
                                          TelephoneNo1 = sr.Agent.TelephoneNo1,
                                          TelephoneNo2 = sr.Agent.TelephoneNo2,
                                          TradingName = sr.Agent.TradingName,
                                          WorkflowInstanceID = ddex.WorkflowInstanceID ?? 0,
                                          CurrentBerth = bh.BerthName



                                      }).ToList();

            return suppDryDockAppList;
        }

        public SuppDryDockExtension GetSuppDryDockExtensionApproveid(string suppdrydockextensionid)
        {
            var ddexdata = (from t in _unitOfWork.Repository<SuppDryDockExtension>().Query().Select()
                            where t.SuppDryDockExtensionID == Convert.ToInt32(suppdrydockextensionid, CultureInfo.InvariantCulture)
                            select t).FirstOrDefault<SuppDryDockExtension>();
            return ddexdata;
        }

        /// <summary>
        /// Get Supplementary Dry Dock Extension Details By SuppDryDockExtensionID 
        /// </summary>
        /// <param name="DockingPlanID"></param>
        /// <returns></returns>
        public SuppDryDockExtensionVO GetSuppDryDockExtensionRequestDetailsByID(string suppdrydockextensionid)
        {



            var servicedtls = (from t in _unitOfWork.Repository<SuppDryDockExtension>().Query().Tracking(true)
                              .Include(t => t.SuppDryDock)
                              .Select()
                               join ar in _unitOfWork.Repository<ArrivalNotification>().Query().Tracking(true).Select()
                               on t.SuppDryDock.VCN equals ar.VCN
                               join pt in _unitOfWork.Repository<Port>().Query().Tracking(true).Select()
                               on ar.PortCode equals pt.PortCode
                               join vn in _unitOfWork.Repository<Vessel>().Query().Tracking(true).Select()
                                on ar.VesselID equals vn.VesselID

                               where t.SuppDryDockExtensionID == int.Parse(suppdrydockextensionid, CultureInfo.InvariantCulture)
                               select new SuppDryDockExtensionVO
                               {
                                   VCN = t.SuppDryDock.VCN,
                                   VesselName = vn.VesselName,
                                   FromDate = t.SuppDryDock.FromDate.ToString(),
                                   ToDate = t.SuppDryDock.ToDate.ToString(),
                                   ScheduleFromDate = t.SuppDryDock.ScheduleFromDate.ToString(),
                                   ScheduleToDate = t.SuppDryDock.ScheduleToDate.ToString(),
                                   ExtensionDateTime = t.ExtensionDateTime.ToString(),
                                   PortCode = ar.PortCode,
                                   PortName = pt.PortName,
                                   ModifiedBy = t.ModifiedBy,
                                   CreatedBy = t.CreatedBy
                               }).First();


            return servicedtls;
        }

        [EdmFunction("dbo", "udf_GetWorkflowStatusforGrid")]
        public string GetGetWorkflowStatusforGrid(string p_SubCatCode)
        {
            {
                var subCatCode = new SqlParameter("@p_SubCatCode", p_SubCatCode);
                var areason = _unitOfWork.SqlQuery<string>("Select dbo.udf_GetWorkflowStatusforGrid(@p_SubCatCode)", subCatCode).Single();
                return areason;
            }
        }
    }
}
