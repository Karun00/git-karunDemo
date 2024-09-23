using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System.Data.SqlClient;
using IPMS.Domain;
using System.Globalization;


namespace IPMS.Repository
{
    public class VesselRegistrationRepository : IVesselRegistrationRepository
    {

        private IUnitOfWork _unitOfWork;

        public VesselRegistrationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// To get  vessel registration details by IMO number
        /// </summary>
        /// <param name="IMONum"></param>
        /// <returns></returns>
        public Vessel GetVesselRegistrationByIMO(string IMONum)
        {

            //var vesselDetails = (from va in _unitOfWork.Repository<Vessel>().Query().Select()
            //                     join wi in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on va.WorkflowInstanceId equals wi.WorkflowInstanceId into workflw
            //                     from subset1 in workflw.DefaultIfEmpty()
            //                     join wp in _unitOfWork.Repository<WorkflowProcess>().Query().Select() on subset1.WorkflowProcessId equals wp.WorkflowProcessId into workflw1
            //                     from subset2 in workflw1.DefaultIfEmpty()
            //                     where va.IMONo == IMONum
            //                     select va).FirstOrDefault<Vessel>();

            var vesselDetails = (from va in _unitOfWork.Repository<Vessel>().Queryable().Where(va => va.IMONo == IMONum)
                                 join wi in _unitOfWork.Repository<WorkflowInstance>().Queryable() on va.WorkflowInstanceId equals wi.WorkflowInstanceId
                                 join wp in _unitOfWork.Repository<WorkflowProcess>().Queryable() on wi.WorkflowProcessId equals wp.WorkflowProcessId
                                 join sub2 in _unitOfWork.Repository<SubCategory>().Queryable() on va.VesselNationality equals sub2.SubCatCode
                                 join sub3 in _unitOfWork.Repository<SubCategory>().Queryable() on va.VesselType equals sub3.SubCatCode

                                //where va.IMONo == IMONum
                                 select new 
                                 {
                                     IMONo = va.IMONo,
                                     VesselName = va.VesselName,
                                     VesselType = sub3.SubCatCode != null ? sub3.SubCatName : string.Empty,
                                     CallSign = va.CallSign != null ? va.CallSign : string.Empty,
                                     ExCallSign = va.ExCallSign,
                                     VesselNationality = sub2.SubCatCode != null ? sub2.SubCatName : string.Empty,
                                     CreatedBy = va.CreatedBy,
                                     CreatedDate = va.CreatedDate,
                                     SubmissionDate = va.CreatedDate,
                                     Remarks = wp.Remarks == null ? string.Empty : wp.Remarks
                                  }).ToList().Select(v => new Vessel()
                                    {
                                        IMONo = v.IMONo,
                                     VesselName = v.VesselName,
                                     VesselType = v.VesselType,
                                     CallSign = v.CallSign,
                                     ExCallSign = v.ExCallSign,
                                     VesselNationality = v.VesselNationality,
                                     CreatedBy = v.CreatedBy,
                                     CreatedDate = v.CreatedDate,
                                     SubmissionDate = v.CreatedDate,
                                     Remarks = v.Remarks
                                 }).FirstOrDefault<Vessel>();

            //var imoNo = new SqlParameter("@IMONo", IMONum);

            //var vesselDetails = _unitOfWork.SqlQuery<Vessel>("usp_GetVesselRegistrationByIMO @IMONo", imoNo).FirstOrDefault<Vessel>();

            return vesselDetails;
        }


        public List<PortRegistryVO> GetPortRegistry()
        {
            var portRegistry = (from p in _unitOfWork.Repository<PortRegistry>().Queryable()
                                select new PortRegistryVO
                                {

                                    PortCode = p.PortCode,
                                    PortName = p.PortName,
                                    IsSA = p.IsSA,
                                    IsTNPA = p.IsTNPA

                                });
            return portRegistry.ToList();
        }

        /// <summary>
        /// To get vessel registration details
        /// </summary>
        /// <returns></returns>
        //public List<Vessel> GetVesselRegistrationData()
        //{
        //    //var vesdetails = (from objvessel in _unitOfWork.Repository<Vessel>().Query().Select() orderby objvessel.VesselID descending select objvessel.VesselID).Take(200);

        //    //var vesregdata = (from vessel in _unitOfWork.Repository<Vessel>().Query().Include(t => t.WorkflowInstances).Include(t => t.ArrivalNotifications).Include(t => t.VesselApprovals).Include(t => t.VesselEngines).Include(t => t.VesselCertificateDetails).Include(t => t.VesselGears).Include(t => t.VesselGrabs).
        //    //                      Include(t => t.VesselHatchHolds).Include(t => t.SubCategory).Include(t => t.PortRegistry).Include(t => t.SubCategory2).Include(t => t.SubCategory3).Select().Where(g => vesdetails.Contains(g.VesselID))
        //    //                  orderby vessel.CreatedDate descending
        //    //                  select vessel).ToList<Vessel>();

        //  var vesregdata = (from vessel in _unitOfWork.Repository<Vessel>().Query().Include(t => t.WorkflowInstances).Include(t => t.ArrivalNotifications).Include(t => t.VesselApprovals).Include(t => t.VesselEngines).Include(t => t.VesselCertificateDetails).Include(t => t.VesselGears).Include(t => t.VesselGrabs).
        //                          Include(t => t.VesselHatchHolds).Include(t => t.SubCategory).Include(t => t.PortRegistry).Include(t => t.SubCategory2).Include(t => t.SubCategory3).Select()
        //                      orderby vessel.VesselID descending
        //                               select vessel).ToList<Vessel>().Take(200);



        //  List<Vessel> vessels = vesregdata.ToList<Vessel>();

        //  return vessels;

        //}

        public List<VesselVO> GetVesselRegistrationData(string portCode, int userID)
        {
            PortGeneralConfig validityperiodDet = _unitOfWork.Repository<PortGeneralConfig>().Queryable().Where(e => e.GroupName == "VesselRegistration" && e.PortCode == portCode).FirstOrDefault();

            int topRecords = default(int);
            if (validityperiodDet != null)
                topRecords = Convert.ToInt32(validityperiodDet.ConfigValue, CultureInfo.InvariantCulture);
            else
                topRecords = 200;

            int agentid = GetAgentID(portCode, userID);

            var _agentid = new SqlParameter("@agentID", agentid);

            List<VesselVO> vessels = _unitOfWork.SqlQuery<VesselVO>("select top " + topRecords + " vm.*, sub2.subcatname as VesselTypeName,CASE when vm.RecordStatus = 'I' Then 'Rejected' ELSE CASE when IsFinal = 'Y' Then 'Approved' ELSE 'New' END END as WfStatus,  pr.PortName PortOfRegistryName, vm.IsFinal,a.AgentID , CASE WHEN  @agentID = 0 THEN 'T' ELSE CASE WHEN a.AgentID = @agentID	THEN 'T' ELSE 'F' END END IsVisible  from Vessel vm inner join SubCategory sub2 on sub2.subcatcode = vm.VesselType inner join PortRegistry pr on pr.PortCode = vm.PortOfRegistry inner join Users u on u.UserID = vm.CreatedBy left join Agent a on a.AgentID = u.UserTypeID and u.UserType='AGNT'   order by vesselID desc ", _agentid).ToList();

            //List<VesselVO> vessels = _unitOfWork.SqlQuery<VesselVO>("select top " + topRecords + " vm.*, sub2.subcatname as VesselTypeName,CASE when vm.RecordStatus = 'I' Then 'Rejected' ELSE CASE when IsFinal = 'Y' Then 'Approved' ELSE 'New' END END as WfStatus,  pr.PortName PortOfRegistryName, vm.IsFinal  from Vessel vm inner join SubCategory sub2 on sub2.subcatcode = vm.VesselType inner join PortRegistry pr on pr.PortCode = vm.PortOfRegistry order by vesselID desc ").ToList();

            //List<VesselVO> vessels = _unitOfWork.SqlQuery<VesselVO>("select top " + topRecords + " vm.*, sub2.subcatname as VesselTypeName,CASE when vm.RecordStatus = 'I' Then 'Rejected' ELSE CASE when IsFinal = 'Y' Then 'Approved' ELSE 'New' END END as WfStatus,  pr.PortName PortOfRegistryName, vm.IsFinal,a.AgentID  from Vessel vm inner join SubCategory sub2 on sub2.subcatcode = vm.VesselType inner join PortRegistry pr on pr.PortCode = vm.PortOfRegistry inner join Users u on u.UserID = vm.CreatedBy left join Agent a on a.AgentID = u.UserTypeID and u.UserType='AGNT' where   (a.AgentID="+ @agentID + " Or @agentID is null)   order by vesselID desc ").ToList();


            string[] vesselIds = (from ves in vessels select ves.VesselID.ToString(CultureInfo.InvariantCulture)).ToArray();


            string vesselIdStr = String.Join(",", vesselIds);


            List<VesselEngine> vesselEngines = _unitOfWork.SqlQuery<VesselEngine>("select * from VesselEngine where VesselId in (" + vesselIdStr + ")").ToList();

            List<VesselCertificateDetail> vesselCertificateDetails = _unitOfWork.SqlQuery<VesselCertificateDetail>("select * from VesselCertificateDetails where VesselId in (" + vesselIdStr + ")").ToList();

            List<VesselGear> vesselGears = _unitOfWork.SqlQuery<VesselGear>("select * from VesselGear where VesselId in (" + vesselIdStr + ")").ToList();

            List<VesselGrab> vesselGrabs = _unitOfWork.SqlQuery<VesselGrab>("select * from VesselGrab where VesselId in (" + vesselIdStr + ")").ToList();

            List<VesselHatchHold> vesselHatchHolds = _unitOfWork.SqlQuery<VesselHatchHold>("select * from VesselHatchHold where VesselId in (" + vesselIdStr + ")").ToList();
            foreach (VesselVO ves in vessels)
            {
                ves.VesselEngines = vesselEngines.MapToDto().FindAll(t => t.VesselID == ves.VesselID);
                ves.VesselGears = vesselGears.MapToDto().FindAll(t => t.VesselID == ves.VesselID);
                ves.VesselGrabs = vesselGrabs.MapToDto().FindAll(t => t.VesselID == ves.VesselID);
                ves.VesselHatchHolds = vesselHatchHolds.MapToDto().FindAll(t => t.VesselID == ves.VesselID);
                ves.VesselCertificateDetails = vesselCertificateDetails.MapToDTO().FindAll(t => t.VesselID == ves.VesselID);
            }



            return vessels;
        }

        /// <summary>
        /// To get agent id
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAgentID(string portcode, int userID)
        {
            var user = (from a in _unitOfWork.Repository<Agent>().Queryable()
                        join u in _unitOfWork.Repository<User>().Queryable() on a.AgentID equals u.UserTypeID
                        join ap in _unitOfWork.Repository<AgentPort>().Queryable() on a.AgentID equals ap.AgentID
                        where ap.PortCode == portcode && u.UserType == GlobalConstants.AGENT && u.UserID == userID
                        select a).FirstOrDefault<Agent>();
            if (user != null)
                return user.AgentID;
            else
                return 0;
        }



        //public List<Vessel> GetSearchVesselData(string IMONO, string VesselName, string PortofRegistry, string VesselNationality, string VesselType, string clallsign)
        //{
        //    // var vesdetails = (from objvessel in _unitOfWork.Repository<Vessel>().Query().Select().Take(50) select objvessel.VesselID);

        //    //List<int> ids = new List<int>() { 1,3,4} .Where
        //    //g => vesdetails.Contains(g.VesselID));

        //    var vesregdata = (from vessel in _unitOfWork.Repository<Vessel>().Query().Include(t => t.ArrivalNotifications).Include(t => t.VesselApprovals).Include(t => t.VesselEngines).Include(t => t.VesselCertificateDetails).Include(t => t.VesselGears).Include(t => t.VesselGrabs).
        //                         Include(t => t.VesselHatchHolds).Include(t => t.SubCategory).Include(t => t.PortRegistry).Include(t => t.SubCategory2).Include(t => t.SubCategory3).Select()
        //                      orderby vessel.CreatedDate descending
        //                      select vessel).ToList<Vessel>();



        //    if (IMONO != "ALL")
        //        vesregdata = vesregdata.Where(objdata => objdata.IMONo.Trim().ToLower() == IMONO.Trim().ToLower()).ToList();
        //    if (VesselName != "ALL")
        //        vesregdata = vesregdata.Where(objdata => objdata.VesselName.Trim().ToLower().Contains(VesselName.Trim().ToLower())).ToList();
        //    if (PortofRegistry != "ALL")
        //        vesregdata = vesregdata.Where(objdata => objdata.PortOfRegistry == PortofRegistry).ToList();
        //    if (VesselNationality != "ALL")
        //        vesregdata = vesregdata.Where(objdata => objdata.VesselNationality == VesselNationality).ToList();
        //    if (VesselType != "ALL")
        //        vesregdata = vesregdata.Where(objdata => objdata.VesselType == VesselType).ToList();
        //    if (clallsign != "ALL")
        //        vesregdata = vesregdata.Where(objdata => objdata.CallSign.Trim().ToLower().Contains(clallsign.Trim().ToLower())).ToList();
        //    return vesregdata;
        //}

        public List<VesselVO> GetSearchVesselData(string imoNo, string vesselName, string portOfRegistry, string vesselNationality, string vesselType, string callSign, string portCode,int userId)
        {

            PortGeneralConfig validityperiodDet = _unitOfWork.Repository<PortGeneralConfig>().Query().Select().Where(e => e.GroupName == "VesselRegistration" && e.PortCode == portCode).FirstOrDefault();

            int topRecords = default(int);
            if (validityperiodDet != null)
                topRecords = Convert.ToInt32(validityperiodDet.ConfigValue, CultureInfo.InvariantCulture);
            else
                topRecords = 200;

            string searchstr = " 1=1  ";

            if (imoNo != "ALL")
                searchstr += "and (IMONO is Null OR IMONo Like '%" + imoNo + "%')  ";

            if (vesselName != "ALL")
                searchstr += " and (VesselName is Null OR VesselName Like '%" + vesselName + "%' )  ";

            if (portOfRegistry != "ALL")
                searchstr += "and (pr.PortName is Null OR pr.PortName ='" + portOfRegistry + "')  ";

            if (vesselNationality != "ALL")
                searchstr += "and (VesselNationality  is Null OR VesselNationality  ='" + vesselNationality + "')  ";

            if (vesselType != "ALL")
                searchstr += "and (VesselType  is Null OR VesselType  ='" + vesselType + "')  ";

            if (callSign != "ALL")
                searchstr += "and (CallSign  is Null OR CallSign Like '%" + callSign + "%')";

            int agentid = GetAgentID(portCode, userId);

            var _agentid = new SqlParameter("@agentID", agentid );
            //var _searchstr = new SqlParameter("@searchstr", searchstr);

            List<VesselVO> vesregdata = _unitOfWork.SqlQuery<VesselVO>("select top " + topRecords + " vm.*, sub2.subcatname as VesselTypeName,CASE when vm.RecordStatus = 'I' Then 'Rejected' ELSE CASE when IsFinal = 'Y' Then 'Approved' ELSE 'New' END END as WfStatus,  pr.PortName PortOfRegistryName, vm.IsFinal,a.AgentID , CASE WHEN  @agentID=0 THEN 'T' ELSE CASE WHEN a.AgentID = @agentID	THEN 'T' ELSE 'F' END END IsVisible  from Vessel vm inner join SubCategory sub2 on sub2.subcatcode = vm.VesselType inner join PortRegistry pr on pr.PortCode = vm.PortOfRegistry inner join Users u on u.UserID = vm.CreatedBy left join Agent a on a.AgentID = u.UserTypeID and u.UserType='AGNT'  Where  " + searchstr + " order by vesselID desc ", _agentid).ToList();


            //List<VesselVO> vesregdata = _unitOfWork.SqlQuery<VesselVO>("select top " + topRecords + " vm.*, sub2.subcatname as VesselTypeName,CASE when vm.RecordStatus = 'I' Then 'Rejected' ELSE CASE when IsFinal = 'Y' Then 'Approved' ELSE 'New' END END as WfStatus,  pr.PortName PortOfRegistryName, vm.IsFinal  from Vessel vm inner join SubCategory sub2 on sub2.subcatcode = vm.VesselType inner join PortRegistry pr on pr.PortCode = vm.PortOfRegistry Where " + searchstr + " order by vesselID desc ").ToList();

            //   List<VesselVO> vesregdata = _unitOfWork.SqlQuery<VesselVO>("select top " + topRecords + " vm.*, sub2.subcatname as VesselTypeName,dbo.udf_GetWorkflowStatusforGrid (wi.WorkflowTaskCode) as WFStatus,  pr.PortName PortOfRegistryName, vm.IsFinal  from Vessel vm inner join workflowinstance wi on wi.WorkflowInstanceID = vm.WorkflowInstanceID inner join SubCategory sub2 on sub2.subcatcode = vm.VesselType inner join PortRegistry pr on pr.PortCode = vm.PortOfRegistry Where " + searchstr + " order by vesselID desc ").ToList();


            //if (IMONO != "ALL")
            //    vesregdata = vesregdata.Where(objdata => objdata.IMONo.Trim().ToLower() == IMONO.Trim().ToLower()).ToList();
            //if (VesselName != "ALL")
            //    vesregdata = vesregdata.Where(objdata => objdata.VesselName.Trim().ToLower().Contains(VesselName.Trim().ToLower())).ToList();
            //if (PortofRegistry != "ALL")
            //    vesregdata = vesregdata.Where(objdata => objdata.PortOfRegistry == PortofRegistry).ToList();
            //if (VesselNationality != "ALL")
            //    vesregdata = vesregdata.Where(objdata => objdata.VesselNationality == VesselNationality).ToList();
            //if (VesselType != "ALL")
            //    vesregdata = vesregdata.Where(objdata => objdata.VesselType == VesselType).ToList();
            //if (clallsign != "ALL")
            //    vesregdata = vesregdata.Where(objdata => objdata.CallSign.Trim().ToLower().Contains(clallsign.Trim().ToLower())).ToList();

            string[] vesselIds = (from ves in vesregdata select ves.VesselID.ToString(CultureInfo.InvariantCulture)).ToArray();

            if (vesselIds.Length > 0)
            {

                string vesselIdStr = String.Join(",", vesselIds);


                List<VesselEngine> vesselEngines = _unitOfWork.SqlQuery<VesselEngine>("select * from VesselEngine where VesselId in (" + vesselIdStr + ")").ToList();

                List<VesselCertificateDetail> vesselCertificateDetails = _unitOfWork.SqlQuery<VesselCertificateDetail>("select * from VesselCertificateDetails where VesselId in (" + vesselIdStr + ")").ToList();

                List<VesselGear> vesselGears = _unitOfWork.SqlQuery<VesselGear>("select * from VesselGear where VesselId in (" + vesselIdStr + ")").ToList();

                List<VesselGrab> vesselGrabs = _unitOfWork.SqlQuery<VesselGrab>("select * from VesselGrab where VesselId in (" + vesselIdStr + ")").ToList();

                List<VesselHatchHold> vesselHatchHolds = _unitOfWork.SqlQuery<VesselHatchHold>("select * from VesselHatchHold where VesselId in (" + vesselIdStr + ")").ToList();
                foreach (VesselVO ves in vesregdata)
                {
                    ves.VesselEngines = vesselEngines.MapToDto().FindAll(t => t.VesselID == ves.VesselID);
                    ves.VesselGears = vesselGears.MapToDto().FindAll(t => t.VesselID == ves.VesselID);
                    ves.VesselGrabs = vesselGrabs.MapToDto().FindAll(t => t.VesselID == ves.VesselID);
                    ves.VesselHatchHolds = vesselHatchHolds.MapToDto().FindAll(t => t.VesselID == ves.VesselID);
                    ves.VesselCertificateDetails = vesselCertificateDetails.MapToDTO().FindAll(t => t.VesselID == ves.VesselID);
                }

            }
            return vesregdata;
        }

        /// <summary>
        /// To get vessel registration data from pending tasks
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<Vessel> GetzVesselRegistrationData(string vcn)
        {
            //var vesregdata = (from vessel in _unitOfWork.Repository<Vessel>().Query().Include(t => t.ArrivalNotifications).Include(t => t.VesselApprovals).Include(t => t.VesselEngines).Include(t => t.VesselCertificateDetails).Include(t => t.VesselGears).Include(t => t.VesselGrabs).
            //                      Include(t => t.VesselHatchHolds).Select().Where(t => t.ClassificationSociety != null || t.ClassificationSociety == null)
            //                  orderby vessel.CreatedDate descending
            //                  select vessel).Where(vessel => vessel.IMONo == vcn).ToList<Vessel>();


            var vesregdata = (from vessel in _unitOfWork.Repository<Vessel>().Query().Include(t => t.ArrivalNotifications).Include(t => t.VesselApprovals).Include(t => t.VesselEngines).Include(t => t.VesselCertificateDetails).Include(t => t.VesselGears).Include(t => t.VesselGrabs).
                                Include(t => t.VesselHatchHolds).Include(t => t.SubCategory).Include(t => t.PortRegistry).Include(t => t.SubCategory2).Include(t => t.SubCategory3).Select().Where(t => t.ClassificationSociety != null || t.ClassificationSociety == null)
                              orderby vessel.CreatedDate descending
                              select vessel).Where(vessel => vessel.IMONo == vcn).ToList<Vessel>(); ;

            return vesregdata;

        }



    }
}
