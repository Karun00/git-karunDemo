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
    public class DockingPlanRepository : IDockingPlanRepository
    {
        private IUnitOfWork _unitOfWork;
        private IServiceRequestRepository _serviceRequestRepository;

        public DockingPlanRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            // log = 
            LogManager.GetLogger(typeof(ArrivalNotificationRepository));
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
        }

        /// <summary>
        ///  To get Docking Plan Details
        /// </summary>
        /// <returns></returns>
        public List<DockingPlanVO> GetDockingPlanDetails(string portCode, int userId)
        {
            var dockingplans = new List<DockingPlan>();

            var data = _serviceRequestRepository.GetTerminalOperatorForUser(userId, portCode);
            int AgentUserID = 0;
            
            int EmpID = 0;

            if (data.UserType == "AGNT")
            {
                AgentUserID = data.UserID;

            }
            else if (data.UserType == "EMP")
            {
                EmpID = data.UserTypeID;
            }

            if (AgentUserID != 0)
            {
                dockingplans = (from p in _unitOfWork.Repository<DockingPlan>().Queryable()
                                .Include(p => p.Vessel)
                                .Include(p => p.Vessel.PortRegistry)
                                .Include(p => p.Vessel.SubCategory3)
                                .Include(p => p.DockingPlanDocuments)
                                .Include(p => p.DockingPlanDocuments.Select(d => d.Document))
                                .Include(p => p.DockingPlanDocuments.Select(a => a.Document.SubCategory1))
                                .Include(p => p.WorkflowInstance.SubCategory)
                                where p.PortCode == portCode && p.CreatedBy == AgentUserID
                                orderby p.DockingPlanID descending
                                select p).ToList<DockingPlan>();

            }
            else
            {

                dockingplans = (from p in _unitOfWork.Repository<DockingPlan>().Queryable()
                               .Include(p => p.Vessel)
                                .Include(p => p.Vessel.PortRegistry)
                               .Include(p => p.Vessel.SubCategory3)
                               .Include(p => p.DockingPlanDocuments)
                               .Include(p => p.DockingPlanDocuments.Select(d => d.Document))
                               .Include(p => p.DockingPlanDocuments.Select(a => a.Document.SubCategory1))
                               .Include(p => p.WorkflowInstance.SubCategory)
                                where p.PortCode == portCode // && p.CreatedBy == agentId
                                orderby p.DockingPlanID descending
                                select p).ToList<DockingPlan>();

            }
            
            var dockingPlansVO =  dockingplans.MapToDtoForDocking();

            if (AgentUserID != 0)
            {
                foreach (var item in dockingPlansVO)
                {
                    item.EditVisible = true;
                }
            }

            return dockingPlansVO;


        }

        /// <summary>
        /// To Get Vessel Details
        /// </summary>
        /// <returns></returns>
        public List<DockingPlanVO> GetVesselNames(string searchValue, string portCode, string searchColumn)
        {

            var portcode = new SqlParameter("@p_PortCode", portCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var serchColumn = new SqlParameter("@p_SrchOn", searchColumn);


            var vesselnames = _unitOfWork.SqlQuery<DockingPlanVO>("dbo.usp_GetDockingVessel @p_SearchText, @p_PortCode,@p_SrchOn", Searchvalue, portcode, serchColumn).ToList();


            return vesselnames;
        }

        /// <summary>
        /// To Get Vessel Details By VesselID
        /// </summary>
        /// <param name="vesselId"></param>
        /// <returns></returns>
        public DockingPlanVO GetVesselsById(int vesselId)
        {

            var vesselID = new SqlParameter("@p_vesselId", vesselId);

            var vesselnames = _unitOfWork.SqlQuery<DockingPlanVO>("dbo.usp_GetDockingVesselByID @p_vesselId", vesselID).FirstOrDefault();

            return vesselnames;            
        }

        /// <summary>
        /// Get Docking Plan Details By Docking Plan ID 
        /// </summary>
        /// <param name="dockingPlanId"></param>
        /// <returns></returns>
        public DockingPlan GetDockingPlanRequestDetailsById(string dockingPlanId)
        {
            var dockingPlanRequest = (from p in _unitOfWork.Repository<DockingPlan>().Query()
                                      .Include(p => p.Vessel)
                                      .Include(p => p.Vessel.PortRegistry)
                                      .Include(p => p.Vessel.SubCategory3)
                                      .Include(p => p.User)                                         
                                      .Select()
                                      where p.DockingPlanID == Convert.ToInt32(dockingPlanId, CultureInfo.InvariantCulture)
                                      select new DockingPlan
                                      {
                                          ReferenceNo = p.DockingPlanNo,
                                          DockingPlanID = p.DockingPlanID,
                                          VesselID = p.VesselID,
                                          VesselName = p.Vessel.VesselName,
                                          VesselAgent = p.User.FirstName,
                                          ApplicationDateTime = p.CreatedDate,
                                          PortCode = p.PortCode

                                      }).FirstOrDefault<DockingPlan>();

            return dockingPlanRequest;
        }

        public DockingPlan GetDockingPlanApproveId(string dockingplanid)
        {
            var andata = (from t in _unitOfWork.Repository<DockingPlan>().Query().Select()
                          where t.DockingPlanID == Convert.ToInt32(dockingplanid, CultureInfo.InvariantCulture)
                          select t).FirstOrDefault<DockingPlan>();
            return andata;
        }

        /// <summary>
        ///  To get Docking Plan based on dockingplanid
        /// </summary>
        /// <param name="dockingPlanId"></param>
        /// <returns></returns>
        public List<DockingPlanVO> GetDockingPlan(int dockingPlanId)
        {
            var dockingplans = new List<DockingPlan>();
            
            dockingplans = (from p in _unitOfWork.Repository<DockingPlan>().Query()
                            .Include(p => p.Vessel)                                
                            .Include(p => p.Vessel.PortRegistry)
                            .Include(p => p.Vessel.SubCategory3)
                            .Include(p => p.DockingPlanDocuments)
                            .Include(p => p.DockingPlanDocuments.Select(d => d.Document))
                             .Include(p => p.DockingPlanDocuments.Select(a => a.Document.SubCategory1))
                            .Include(p => p.WorkflowInstance.SubCategory)
                            .Select()
                            where p.DockingPlanID == Convert.ToInt32(dockingPlanId)
                            orderby p.DockingPlanID descending
                            select p).ToList<DockingPlan>();
                        
            return dockingplans.MapToDtoForDocking();

        }

        public CompanyVO GetUserDetails(int userId)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == userId
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }
    }
}
