using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;


namespace IPMS.Repository
{
    public class BerthMaintenanceRepository : IBerthMaintenanceRepository
    {
        private IUnitOfWork _unitOfWork;

        public BerthMaintenanceRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  To get Berth Maintenance Details
        /// </summary>
        /// <returns></returns>
        public List<BerthMaintenanceVO> GetBerthMaintenanceDetails(string portCode)
        {
            var berths = (from p in _unitOfWork.Repository<BerthMaintenance>().Query().Include(p => p.Berth).Include(p => p.Bollard).Include(p => p.Bollard1).Include(p => p.SubCategory).Include(p => p.SubCategory1).Include(p => p.WorkflowInstance.SubCategory).Select()
                          where p.PortCode == portCode
                          orderby p.BerthMaintenanceID descending
                          select p).ToList<BerthMaintenance>();

            return berths.MapToDto();
      
        }

        /// <summary>
        /// To Get Bollards based on Berth
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <param name="berthCode"></param>
        /// <returns></returns>
        public List<BollardVO> GetBerthBollards(string portCode, string quayCode, string berthCode)
        {
            var berthbollard = _unitOfWork.Repository<Bollard>().Queryable().OrderBy(x => x.BollardName).Where(x => x.QuayCode == quayCode && x.PortCode == portCode && x.BerthCode == berthCode && x.RecordStatus == RecordStatus.Active).ToList();
            List<BollardVO> BollardVOList = new List<BollardVO>();
            foreach (var bollard in berthbollard)
            {
                BollardVO objBollardVO = new BollardVO();
                objBollardVO = BollardMapExtension.MapToDTO(bollard);
                BollardVOList.Add(objBollardVO);
            }

            return BollardVOList;

        }

        /// <summary>
        ///  To get Berth Maintenance Details based on berthmaintenanceid
        /// </summary>
        /// <param name="berthMaintenanceId"></param>
        /// <returns></returns>
        public List<BerthMaintenanceVO> GetBerthMaintenance(int berthMaintenanceId)
        {
            var berthmaintenances = (from p in _unitOfWork.Repository<BerthMaintenance>().Query().Include(p => p.Berth).Include(p => p.Bollard).Include(p => p.Bollard1).Include(p => p.SubCategory).Include(p => p.SubCategory1).Select()
                                     where p.BerthMaintenanceID == berthMaintenanceId
                                     select p).ToList<BerthMaintenance>();

            return berthmaintenances.MapToDto();	
        }


        public BerthMaintenance GetBerthMaintenanceApproveId(string berthMaintenanceId)
        {
          var andata = (from t in _unitOfWork.Repository<BerthMaintenance>().Query().Select()                               
                               where t.BerthMaintenanceID == Convert.ToInt32(berthMaintenanceId, CultureInfo.InvariantCulture)
                               select t).FirstOrDefault<BerthMaintenance>();
            return andata;
        }

        /// <summary>
        /// Get BerthMaintenance Details By BerthMaintenance Id 
        /// </summary>
        /// <param name="BerthMaintenanceID"></param>
        /// <returns></returns>
        public BerthMaintenanceVO GetBerthMaintenanceRequestDetailsByID(string berthmaintenanceid)
        {


            var berths = (from p in _unitOfWork.Repository<BerthMaintenance>().Query().Include(p => p.Berth).Include(p => p.Bollard).Include(p => p.Bollard1).Include(p => p.SubCategory).Include(p => p.SubCategory1).Include(p => p.WorkflowInstance.SubCategory).Select()
                          where p.BerthMaintenanceID == Convert.ToInt32(berthmaintenanceid, CultureInfo.InvariantCulture)
                          orderby p.BerthMaintenanceID descending
                          select p).FirstOrDefault<BerthMaintenance>();

            return berths.MapToDto();

            //var berthmaintenanceRequest = (from p in _unitOfWork.Repository<BerthMaintenance>().Query().Include(p => p.Berth).Include(p => p.Bollard).Include(p => p.Bollard1).Include(p => p.SubCategory).Include(p => p.SubCategory1).Tracking(true).Select()
            //                               where p.BerthMaintenanceID == Convert.ToInt32(BerthMaintenanceID)
            //                               select new BerthMaintenance
            //                             {
            //                                 BerthMaintenanceID = p.BerthMaintenanceID,
            //                                 PortCode = p.PortCode,
            //                                 ReferenceNo = p.BerthMaintenanceNo,
            //                                 BerthName = p.Berth.BerthName,
            //                                 BollardsFrom = p.Bollard.BollardName,
            //                                 BollardsTo = p.Bollard.BollardName,
            //                                 MaintenanceType = p.SubCategory1.SubCatName,
            //                                 PeriodFrom = p.PeriodFrom,
            //                                 PeriodTo = p.PeriodTo

            //                             }).FirstOrDefault<BerthMaintenance>();

            //return berthmaintenanceRequest;
        }

        public string GetWorkFlowRemarks(int workFlowInstanceId)
        {
            var workflowinstanceID = new SqlParameter("@p_@WorkflowInstanceId", workFlowInstanceId);

            var workFlowRemarks = _unitOfWork.SqlQuery<string>("Select dbo.udf_GetWorkflowPreviousRemarks(@p_@WorkflowInstanceId)", workflowinstanceID).Single();
            return workFlowRemarks;
        }

    }
}

