using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IPMS.Repository
{
    public class BerthMaintenanceCompletionRepository : IBerthMaintenanceCompletionRepository
    {
        private IUnitOfWork _unitOfWork;

        public BerthMaintenanceCompletionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

         /// <summary>
        ///  To get Berth Maintenance Details
        /// </summary>
        /// <returns></returns>
        public List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletionList(string portCode)
        {
            var BerthMaintCompDetails = from p in _unitOfWork.Repository<BerthMaintenanceCompletion>().Query()
                                            .Include(p => p.BerthMaintenance)
                                            .Include(p => p.BerthMaintenance.Berth)
                                            .Include(p => p.BerthMaintenance.Bollard)
                                            .Include(p => p.BerthMaintenance.Bollard1)
                                            .Include(p => p.BerthMaintenance.SubCategory)
                                            .Include(p => p.BerthMaintenance.SubCategory1)
                                            .Include(p => p.WorkflowInstance)
                                            .Include(p => p.WorkflowInstance.SubCategory).Select()
                                        orderby p.CreatedDate descending
                                        where p.BerthMaintenance.PortCode == portCode && p.RecordStatus == "A"
                                        select new BerthMaintenanceDataVO
                                        {
                                            BerthMaintenanceCompletionID = p.BerthMaintenanceCompletionID,
                                            BerthMaintenanceID = p.BerthMaintenanceID,
                                            CompletionDateTime = Convert.ToString(p.CompletionDateTime, CultureInfo.InvariantCulture),
                                            RecordStatus = p.RecordStatus,
                                            PortCode = p.BerthMaintenance.PortCode,
                                            ProjectNo = p.BerthMaintenance.ProjectNo,
                                            MaintenanceTypeCode = p.BerthMaintenance.SubCategory1.SubCatName,
                                            MaintPortCode = p.BerthMaintenance.MaintPortCode,
                                            MaintQuayCode = p.BerthMaintenance.MaintQuayCode,
                                            MaintBerthCode = p.BerthMaintenance.Berth.BerthName,
                                            FromPortCode = p.BerthMaintenance.FromPortCode,
                                            FromQuayCode = p.BerthMaintenance.FromQuayCode,
                                            FromBerthCode = p.BerthMaintenance.FromBerthCode,
                                            FromBollard = p.BerthMaintenance.Bollard.BollardName,
                                            ToPortCode = p.BerthMaintenance.ToPortCode,
                                            ToQuayCode = p.BerthMaintenance.ToQuayCode,
                                            ToBerthCode = p.BerthMaintenance.ToBerthCode,
                                            ToBollard = p.BerthMaintenance.Bollard1.BollardName,
                                            PeriodFrom = p.BerthMaintenance.PeriodFrom,
                                            PeriodTo = p.BerthMaintenance.PeriodTo,
                                            OccupationTypeCode = p.BerthMaintenance.OccupationTypeCode,
                                            Precinct = p.BerthMaintenance.Precinct,
                                            DisciplineCode = p.BerthMaintenance.SubCategory.SubCatName,
                                            SpecialConditions = p.BerthMaintenance.SpecialConditions,
                                            Description = p.BerthMaintenance.Description,
                                            observation = p.observation,
                                            CreatedBy = p.CreatedBy,
                                            CreatedDate = p.CreatedDate,  
                                            BerthMaintenanceNo = p.BerthMaintenance.BerthMaintenanceNo,
                                            WorkflowInstanceId = p.WorkflowInstance != null ? p.WorkflowInstance.WorkflowInstanceId : 0,
                                            WorkFlowStatus = p.WorkflowInstance != null ? (p.WorkflowInstance.SubCategory != null ? p.WorkflowInstance.SubCategory.SubCatName : string.Empty) : string.Empty

                                        };

            return BerthMaintCompDetails.ToList();
        }


        /// <summary>
        /// To Get Berth Maintenance Ids
        /// </summary>
        /// <returns></returns>
        public List<DataVO> GetBethMaintenanceIDs(string portCode)
        {
            var BethMaintenanceIDs = from bm in _unitOfWork.Repository<BerthMaintenance>().Query()
                                     .Include(bm => bm.BerthMaintenanceCompletions)
                                     .Include(bm => bm.WorkflowInstance)
                                     .Include(bm => bm.Berth)
                                     .Include(bm => bm.Bollard)
                                     .Include(bm => bm.Bollard1)
                                     .Include(bm => bm.SubCategory)
                                     .Include(bm => bm.SubCategory1)
                                     .Include(bm => bm.WorkflowInstance.SubCategory)
                                     .Include(bm => bm.WorkflowInstance.SubCategory.PortConfigurations).Select(bm => bm)
                                     .Where(bm => bm.BerthMaintenanceCompletions.Count == 0 && bm.WorkflowInstance.WorkflowTaskCode == bm.WorkflowInstance.SubCategory.PortConfigurations.Where(p => p.PortCode == bm.PortCode).Select(p => p.ApproveCode).FirstOrDefault())
                                     where bm.PortCode == portCode
                                     orderby bm.BerthMaintenanceNo
                                     select new DataVO
                                     {
                                         BerthMaintenanceID = bm.BerthMaintenanceID,
                                         BerthMaintenanceNo = bm.BerthMaintenanceNo,
                                         ProjectNo = bm.ProjectNo,
                                         MaintenanceTypeCode = bm.SubCategory1.SubCatName,
                                         MaintBerthCode = bm.Berth.BerthName,
                                         FromBollard = bm.Bollard.BollardName,
                                         ToBollard = bm.Bollard1.BollardName,
                                         PeriodFrom = bm.PeriodFrom,
                                         PeriodTo = bm.PeriodTo,
                                         OccupationTypeCode = bm.OccupationTypeCode,
                                         Precinct = bm.Precinct,
                                         DisciplineCode = bm.SubCategory.SubCatName,
                                         SpecialConditions = bm.SpecialConditions,
                                         Description = bm.Description
                                     };

            return BethMaintenanceIDs.ToList();

        }

        /// <summary>
        /// To Get Berth Maintenance Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DataVO> BethMaintenanceDetails(int id)
        {           
                var BerthMaintCompDetails = from q in _unitOfWork.Repository<BerthMaintenanceCompletion>().Query().Select().AsEnumerable<BerthMaintenanceCompletion>()
                                            join p in _unitOfWork.Repository<BerthMaintenance>().Query().Select().AsEnumerable<BerthMaintenance>() on
                                            q.BerthMaintenanceID equals p.BerthMaintenanceID
                                            orderby q.CreatedDate descending
                                            where q.BerthMaintenanceID == id & p.RecordStatus == "A"
                                            select new DataVO
                                            {
                                                BerthMaintenanceID = q.BerthMaintenanceID,
                                                BerthMaintenanceNo = p.BerthMaintenanceNo,
                                                ProjectNo = p.ProjectNo,
                                                MaintenanceTypeCode = p.MaintenanceTypeCode,
                                                MaintBerthCode = p.MaintBerthCode,
                                                FromBollard = p.FromBollard,
                                                ToBollard = p.ToBollard,
                                                PeriodFrom = p.PeriodFrom,
                                                PeriodTo = p.PeriodTo,
                                                OccupationTypeCode = p.OccupationTypeCode,
                                                Precinct = p.Precinct,
                                                DisciplineCode = p.DisciplineCode,
                                                SpecialConditions = p.SpecialConditions,
                                                Description = p.Description
                                            };

                return BerthMaintCompDetails.ToList();
           
        }

        /// <summary>
        /// To Get Berth Maintenance Completion Details By ID
        /// </summary>
        /// <param name="berthMaintenanceCompletionId"></param>
        /// <returns></returns>
        public List<BerthMaintenanceDataVO> GetBerthMaintenanceCompletion(int berthMaintenanceCompletionId)
        {
            var BerthMaintCompDetails = from p in _unitOfWork.Repository<BerthMaintenanceCompletion>().Query()
                                           .Include(p => p.BerthMaintenance)
                                           .Include(p => p.BerthMaintenance.Berth)
                                           .Include(p => p.BerthMaintenance.Bollard)
                                           .Include(p => p.BerthMaintenance.Bollard1)
                                           .Include(p => p.BerthMaintenance.SubCategory)
                                           .Include(p => p.BerthMaintenance.SubCategory1)
                                           .Include(p => p.WorkflowInstance).Select()
                                        //.Include(p => p.WorkflowInstance.SubCategory).Select()
                                        orderby p.CreatedDate descending
                                        where p.BerthMaintenanceCompletionID == berthMaintenanceCompletionId
                                        select new BerthMaintenanceDataVO
                                        {
                                            BerthMaintenanceCompletionID = p.BerthMaintenanceCompletionID,
                                            BerthMaintenanceID = p.BerthMaintenanceID,
                                            CompletionDateTime = Convert.ToString(p.CompletionDateTime, CultureInfo.InvariantCulture),
                                            RecordStatus = p.RecordStatus,
                                            PortCode = p.BerthMaintenance.PortCode,
                                            ProjectNo = p.BerthMaintenance.ProjectNo,
                                            MaintenanceTypeCode = p.BerthMaintenance.SubCategory1.SubCatName,
                                            MaintPortCode = p.BerthMaintenance.MaintPortCode,
                                            MaintQuayCode = p.BerthMaintenance.MaintQuayCode,
                                            MaintBerthCode = p.BerthMaintenance.Berth.BerthName,
                                            FromPortCode = p.BerthMaintenance.FromPortCode,
                                            FromQuayCode = p.BerthMaintenance.FromQuayCode,
                                            FromBerthCode = p.BerthMaintenance.FromBerthCode,
                                            FromBollard = p.BerthMaintenance.Bollard.BollardName,
                                            ToPortCode = p.BerthMaintenance.ToPortCode,
                                            ToQuayCode = p.BerthMaintenance.ToQuayCode,
                                            ToBerthCode = p.BerthMaintenance.ToBerthCode,
                                            ToBollard = p.BerthMaintenance.Bollard1.BollardName,
                                            PeriodFrom = p.BerthMaintenance.PeriodFrom,
                                            PeriodTo = p.BerthMaintenance.PeriodTo,
                                            OccupationTypeCode = p.BerthMaintenance.OccupationTypeCode,
                                            Precinct = p.BerthMaintenance.Precinct,
                                            DisciplineCode = p.BerthMaintenance.SubCategory.SubCatName,
                                            SpecialConditions = p.BerthMaintenance.SpecialConditions,
                                            Description = p.BerthMaintenance.Description,
                                            observation = p.observation,
                                            CreatedBy = p.CreatedBy,
                                            CreatedDate = p.CreatedDate,
                                            BerthMaintenanceNo = p.BerthMaintenance.BerthMaintenanceNo,
                                            WorkflowInstanceId = p.WorkflowInstance != null ? p.WorkflowInstance.WorkflowInstanceId : 0
                                        };
             

                return BerthMaintCompDetails.ToList();           
        }


        public BerthMaintenanceCompletion GetApproveid(string berthMaintenanceCompletionId)
        {
            var andata = (from t in _unitOfWork.Repository<BerthMaintenanceCompletion>().Query().Select()
                          where t.BerthMaintenanceCompletionID == Convert.ToInt32(berthMaintenanceCompletionId, CultureInfo.InvariantCulture)
                          select t).FirstOrDefault<BerthMaintenanceCompletion>();
            return andata;
        }
          
        
        /// <summary>
        /// To Get BerthMaintenanceCompletion Details By BerthMaintenanceCompletion Id 
        /// </summary>
        /// <param name="BerthMaintenanceCompletionID"></param>
        /// <returns></returns>
        public BerthMaintenanceDataVO GetBerthMaintenanceCompletionDetailsByID(string BerthMaintenanceCompletionID)
        {
            var berthmaintenanceRequest = from p in _unitOfWork.Repository<BerthMaintenanceCompletion>().Query()
                                        .Include(p => p.BerthMaintenance)
                                        .Include(p => p.BerthMaintenance.Berth)
                                        .Include(p => p.BerthMaintenance.Bollard)
                                        .Include(p => p.BerthMaintenance.Bollard1)
                                        .Include(p => p.BerthMaintenance.SubCategory)
                                        .Include(p => p.BerthMaintenance.SubCategory1)
                                        .Include(p => p.WorkflowInstance)
                                        .Include(p => p.WorkflowInstance.SubCategory).Select()
                                        orderby p.CreatedDate descending
                                          where p.BerthMaintenanceCompletionID == Convert.ToInt32(BerthMaintenanceCompletionID, CultureInfo.InvariantCulture)
                                        select new BerthMaintenanceDataVO
                                        {
                                            BerthMaintenanceCompletionID = p.BerthMaintenanceCompletionID,
                                            BerthMaintenanceID = p.BerthMaintenanceID,
                                            ReferenceNo = p.BerthMaintenance.BerthMaintenanceNo,
                                            CompletionDateTime = Convert.ToString(p.CompletionDateTime, CultureInfo.InvariantCulture),
                                            RecordStatus = p.RecordStatus,
                                            PortCode = p.BerthMaintenance.PortCode,
                                            ProjectNo = p.BerthMaintenance.ProjectNo,
                                            MaintenanceType = p.BerthMaintenance.SubCategory1.SubCatName,
                                            MaintPortCode = p.BerthMaintenance.MaintPortCode,
                                            MaintQuayCode = p.BerthMaintenance.MaintQuayCode,
                                            BerthName = p.BerthMaintenance.Berth.BerthName,
                                            FromPortCode = p.BerthMaintenance.FromPortCode,
                                            FromQuayCode = p.BerthMaintenance.FromQuayCode,
                                            FromBerthCode = p.BerthMaintenance.FromBerthCode,
                                            BollardsFrom = p.BerthMaintenance.Bollard.BollardName,
                                            ToPortCode = p.BerthMaintenance.ToPortCode,
                                            ToQuayCode = p.BerthMaintenance.ToQuayCode,
                                            ToBerthCode = p.BerthMaintenance.ToBerthCode,
                                            BollardsTo = p.BerthMaintenance.Bollard1.BollardName,
                                            PeriodFrom = p.BerthMaintenance.PeriodFrom,
                                            PeriodTo = p.BerthMaintenance.PeriodTo,
                                            OccupationTypeCode = p.BerthMaintenance.OccupationTypeCode,
                                            Precinct = p.BerthMaintenance.Precinct,
                                            DisciplineCode = p.BerthMaintenance.SubCategory.SubCatName,
                                            SpecialConditions = p.BerthMaintenance.SpecialConditions,
                                            Description = p.BerthMaintenance.Description,
                                            observation = p.observation,
                                            CreatedBy = p.CreatedBy,
                                            CreatedDate = p.CreatedDate,
                                            BerthMaintenanceNo = p.BerthMaintenance.BerthMaintenanceNo,
                                            WorkflowInstanceId = p.WorkflowInstance != null ? p.WorkflowInstance.WorkflowInstanceId : 0,
                                            WorkFlowStatus = p.WorkflowInstance != null ? (p.WorkflowInstance.SubCategory != null ? p.WorkflowInstance.SubCategory.SubCatName : string.Empty) : string.Empty

                                        };

            return berthmaintenanceRequest.FirstOrDefault();

            //var berthmaintenanceRequest = (from p in _unitOfWork.Repository<BerthMaintenanceCompletion>().Query().Include(p => p.BerthMaintenance).Select()
            //                               where p.BerthMaintenanceCompletionID == Convert.ToInt32(BerthMaintenanceCompletionID)
            //                               select p).FirstOrDefault<BerthMaintenanceCompletion>();

            //return berthmaintenanceRequest;
        }

    }
}

