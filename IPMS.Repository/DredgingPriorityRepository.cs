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
using System.Data;
using System.Globalization;

namespace IPMS.Repository
{
    public class DredgingPriorityRepository : IDredgingPriorityRepository
    {
        private IUnitOfWork _unitOfWork;
        //private readonly ILog log;


        public DredgingPriorityRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
           // log = 
           LogManager.GetLogger(typeof(ArrivalNotificationRepository));
        }
        /// <summary>
        ///  To get Dredging Priority Details
        /// </summary>
        /// <returns></returns>
        public List<DredgingPriorityVO> GetDredgingPriorityDetails(int financialYearId, string portCode)
        {
            var dredgingprioritys = new List<DredgingPriority>();


            dredgingprioritys = (from p in _unitOfWork.Repository<DredgingPriority>().Query()
                             .Include(p => p.DredgingOperations)
                            .Include(p => p.DredgingPriorityDocuments)
                            .Include(p => p.FinancialYear)
                            .Include(p => p.DredgingOperations.Select(s => s.SubCategory2))
                            .Include(p => p.DredgingOperations.Select(b => b.Berth))
                            .Include(p => p.DredgingOperations.Select(l => l.Location))
                            .Include(p => p.DredgingOperations.Select(f => f.FinancialYear))
                            .Include(p => p.DredgingPriorityDocuments.Select(d => d.Document))

                            .Select()
                                 where p.FinancialYearID == financialYearId
                                 where p.FinancialYearID == financialYearId && p.DredgingOperations.Count > 0 && p.DredgingOperations.FirstOrDefault().PortCode == portCode

                                 // where p.DredgingPriorityID == dpd.DredgingPriorityID && p.DredgingPriorityID == dpa.DredgingPriorityID
                                 orderby p.DredgingPriorityID descending
                                 select p).ToList<DredgingPriority>();



            // return dockingplans.MapToDTOForDocking();

            return dredgingprioritys.MapToDto();


        }
        /// <summary>
        /// gets Months
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<FinancialYearVO> GetMonths(int financialYearId)
        {
            var months = (from a in _unitOfWork.Repository<FinancialYear>().Query().Select()
                          where a.FinancialYearID == financialYearId
                          select new FinancialYearVO
                          {
                              StartDate = a.StartDate,
                              EndDate = a.EndDate,


                              // FinancialYear = (a.StartDate, a.EndDate) 
                          }).ToList();

            return months;
        }

        /// <summary>
        /// To Get dredging Priority Volume
        /// </summary>
        /// <returns></returns>
        public List<DredgingPriorityVolumeVO> GetDredgingPriorityVolumes(int financialYearId, string portCode)
        {

            var Volumes = GetVolumes(financialYearId, portCode);
            foreach (DredgingPriorityVolumeVO dvo in Volumes)
            {
                var DID = (from m in _unitOfWork.Repository<DredgingPriority>().Query().Select()
                           where dvo.DeploymentPlanID == m.DeploymentPlanID
                           select new DredgingPriorityVO
                           {
                               DredgingPriorityID = m.DredgingPriorityID
                           }).ToList();
                // var DredgingPriorityID = _unitOfWork.Repository<DredgingPriority>().Query().Select().Where(s => s.DeploymentPlanID == dvo.DeploymentPlanID).Select(g => g.DredgingPriorityID).FirstOrDefault();
                //dvo.DredgingPriorityID = DredgingPriorityID;
                //  List<int> DredgingPriorityIDList = dvo.DredgingPriorityID;
                foreach (DredgingPriorityVO dvo1 in DID)
                {
                    var DopID = (from m in _unitOfWork.Repository<DredgingOperation>().Query().Select()
                                 where dvo1.DredgingPriorityID == m.DredgingPriorityID && dvo.TypeCode == m.TypeCode && m.IsDVFinal=="Y "
                                 select new DredgingOperationVO
                                 {
                                     // Volume= m.Volume,
                                     Volume = m.Volume// ?? (Decimal)0
                                 }).ToList();
                    //var Volume = _unitOfWork.Repository<DredgingOperation>().Query().Select().Where(s => s.DredgingPriorityID == dvo1 && s.TypeCode == dvo.TypeCode).Select(g => g.Volume).FirstOrDefault();
                    //dvo.Volume = Volume;
                    // dvo1.Volume=vo

                    //  dvo1.Volume++;
                    //List<decimal> DredVolumeList = dvo.compVolume;

                    foreach (var dvo2 in DopID)
                    {
                        if (dvo2.Volume == null)
                        {

                            dvo2.Volume = 0;
                        }
                        dvo.Volume += Convert.ToInt32(dvo2.Volume, CultureInfo.InvariantCulture);
                    }
                    dvo.Volume = dvo.Volume;
                }
            }
            return Volumes;
        }

        private List<DredgingPriorityVolumeVO> GetVolumes(int financialYearId, string portCode)
        {
            var Volumes = (from dp in _unitOfWork.Repository<DeploymentPlan>().Query().Select()
                           join cf in _unitOfWork.Repository<DeploymentBudget>().Query().Select() on dp.DeploymentPlanID equals cf.DeploymentPlanID
                           join ac in _unitOfWork.Repository<SubCategory>().Query().Select() on cf.DredgingType equals ac.SubCatCode
                           where dp.FinancialYearID == financialYearId && dp.PortCode == portCode
                           select new DredgingPriorityVolumeVO
                           {
                               DredgingType = ac.SubCatName,
                               DredgPlan = cf.DredgPlan,
                               TypeCode = cf.DredgingType,
                               DeploymentPlanID = dp.DeploymentPlanID,
                               Volume = 0
                           }).ToList();
            return Volumes;
        }


        /// <summary>
        /// To Get Financial Year Data
        /// </summary>
        /// <returns></returns>
        public List<FinancialYearVO> GetFinancialYear()
        {

            var FinancialYears = (from fy in _unitOfWork.Repository<FinancialYear>().Query().Select()
                                  //  join ac in _unitOfWork.Repository<DeploymentPlan>().Query().Select() on fy.FinancialYearID equals ac.FinancialYearID
                                  //  where fy.FinancialYearID == ac.FinancialYearID
                                  select new FinancialYearVO
                                  {
                                      FinancialYearID = fy.FinancialYearID,
                                      StartDate = fy.StartDate,
                                      EndDate = fy.EndDate,
                                      FinancialYear = fy.StartDate.ToString("MMMM", CultureInfo.InvariantCulture) + " " + fy.StartDate.ToString("yyyy", CultureInfo.InvariantCulture) + " to " + fy.EndDate.ToString("MMMM", CultureInfo.InvariantCulture) + " " + fy.EndDate.ToString("yyyy", CultureInfo.InvariantCulture)
                                  }).ToList();

            return FinancialYears;
        }
        /// <summary>
        ///  To Get Get GetBerthTypes
        /// </summary>
        /// <returns></returns>
        public List<DredgingPriorityVO> GetBerthTypes(string portCode)
        {
            //   List<DredgingPriorityVO> _BearthTypeList = new List<DredgingPriorityVO>();
            var _BearthTypeList = (from c in _unitOfWork.Repository<Berth>().Query()
                                   .Select().AsEnumerable<Berth>()
                                   where c.PortCode == portCode
                                   select new DredgingPriorityVO
                                   {
                                       BerthCode = c.BerthCode,
                                       BerthName = c.BerthName,
                                       QuayCode = c.QuayCode,
                                       PortCode=c.PortCode,
                                       BerthKey = c.PortCode + "." + c.QuayCode + "." + c.BerthCode,
                                   }).ToList();
            return _BearthTypeList;
        }
        /// <summary>
        ///  To Get GetLocationTypes 
        /// </summary>
        /// <returns></returns>
        public List<DredgingPriorityVO> GetLocationTypes(string portCode)
        {
            // List<DredgingPriorityVO> _LocationTypesList = new List<DredgingPriorityVO>();
            var _LocationTypesList = (from c in _unitOfWork.Repository<Location>().Query()
                                   .Select().AsEnumerable<Location>()
                                      where c.PortCode == portCode
                                      select new DredgingPriorityVO
                                      {
                                          LocationID = c.LocationID,
                                          LocationName = c.LocationName,
                                      }).ToList();
            return _LocationTypesList;
        }

        /// <summary>
        /// Get Dredging Priority Details By DredgingPriorityID 
        /// </summary>
        /// <param name="DredgingPriorityID"></param>
        /// <returns></returns>

        public DredgingPriority GetDredgingPriorityDetailsById(string dredging)
        {

            var DredgingPriorityDetails = (from va in _unitOfWork.Repository<DredgingPriority>().Query().Select()
                                           where va.DredgingPriorityID == Convert.ToInt32(dredging, CultureInfo.InvariantCulture)
                                           select va).FirstOrDefault<DredgingPriority>();
            return DredgingPriorityDetails;
        }

        /// <summary>
        ///  To get  Dredging Priority based on DredgingVolumeID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingpriorityid"></param>
        /// <returns></returns>
        public List<DredgingOperation> DredgingPriority(int dredgingOperationId)
        {
            var dredgingprioritys = new List<DredgingOperation>();

            dredgingprioritys = (from t in _unitOfWork.Repository<DredgingOperation>().Query().Select()
                                 where t.DredgingOperationID == Convert.ToInt32(dredgingOperationId)
                                 select t).ToList();

            //dredgingprioritys = (from p in _unitOfWork.Repository<DredgingPriority>().Query()
            //            .Include(p => p.DredgingPriorityAreas)
            //            .Include(p => p.DredgingPriorityDocuments)
            //            .Include(p => p.DeploymentPlan)
            //            .Include(p => p.DredgingPriorityDocuments.Select(d => d.Document))

            //            .Select()
            //                     where p.DredgingPriorityID == dredgingpriorityid
            //               // where p.DredgingPriorityID == dpd.DredgingPriorityID && p.DredgingPriorityID == dpa.DredgingPriorityID
            //                orderby p.DredgingPriorityID descending

            //                select p).ToList<DredgingPriority>();



            // return dockingplans.MapToDTOForDocking();

            return dredgingprioritys;



        }

        /// <summary>
        ///  To get Dredging Priority Details based on Dredging Priority ID
        /// </summary>
        /// <returns></returns>

        /// <summary>
        ///  To get  GetDredgingPriorityApproveid based on BerthOccupationID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingpriorityid"></param>
        /// <returns></returns>
        public DredgingOperation GetDredgingPriorityApproveId(string dredgingPriorityAreaId)
        {
            var andata = (from t in _unitOfWork.Repository<DredgingOperation>().Query().Select()
                          where t.DredgingOperationID == Convert.ToInt32(dredgingPriorityAreaId, CultureInfo.InvariantCulture)
                          select t).FirstOrDefault();



            //var andata = (from p in _unitOfWork.Repository<DredgingPriority>().Query()
            //                .Include(p => p.DredgingPriorityAreas)
            //                .Include(p => p.DredgingPriorityDocuments)
            //                .Include(p => p.DeploymentPlan)
            //                .Include(p => p.DredgingPriorityDocuments.Select(d => d.Document))
            //                .Select()
            //                     //  where p.DredgingPriorityID == DredgingPriorityID
            //                     // where p.DredgingPriorityID == dpd.DredgingPriorityID && p.DredgingPriorityID == dpa.DredgingPriorityID
            //                     orderby p.DredgingPriorityID descending
            //              select p).FirstOrDefault<DredgingPriority>();

            return andata;
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Dec 2014
        /// Purpose : To get List of Berth Occupation details
        /// </summary>
        /// <returns></returns>
        public List<DredgingOperationVO> GetBerthOccupationList()
        {
            var berthOccupations = (from dp in _unitOfWork.Repository<DredgingOperation>().Query()
                                  .Include(dp => dp.FinancialYear)
                                  .Include(dp => dp.DredgingPriority)
                                  .Include(dp => dp.Berth)
                                  .Include(dp => dp.Location)
                                  .Include(dp => dp.SubCategory2)
                                  .Include(dp => dp.BerthOccupationDocuments)
                                  .Include(dp => dp.BerthOccupationDocuments.Select(d => d.Document))
                                        //.Include(dp=>dp.WorkflowInstance1)
                                  .Select()

                                    where dp.IsDPAFinal == "Y "// && dp.PortCode==portCode
                                    orderby dp.ModifiedDate descending
                                    select dp).ToList();

            return berthOccupations.MapToDto();
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Dec 2014
        /// Purpose : To get List of Berth Occupation details
        /// </summary>
        /// <returns></returns>
        public List<DredgingOperationVO> GetBerthOccupationById(int id)
        {
            var berthOccupations = (from dp in _unitOfWork.Repository<DredgingOperation>().Query()
                                  .Include(dp => dp.FinancialYear)
                                  .Include(dp => dp.DredgingPriority)
                                  .Include(dp => dp.Berth)
                                  .Include(dp => dp.Location)
                                  .Include(dp => dp.SubCategory2)
                                  .Include(dp => dp.BerthOccupationDocuments)
                                  .Include(dp => dp.BerthOccupationDocuments.Select(d => d.Document))
                                        //.Include(dp=>dp.WorkflowInstance1)
                                  .Select()
                                    where dp.DredgingOperationID == id && dp.IsDPAFinal == "Y "
                                    //&& dp.WorkflowInstance1.WorkflowTaskCode == WFStatus.Approved
                                    orderby dp.ModifiedDate descending
                                    select dp).ToList();


            return berthOccupations.MapToDto();
        }
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 1st Jan 2015
        /// Purpose : To get List of Dredging Volume details
        /// </summary>
        /// <returns></returns>
        public List<DredgingOperationVO> GetDredgingVolumeList()
        {
            var dredgingVolumes = (from dp in _unitOfWork.Repository<DredgingOperation>().Query()
                                  .Include(dp => dp.FinancialYear)
                                  .Include(dp => dp.DredgingPriority)
                                  .Include(dp => dp.Berth)
                                  .Include(dp => dp.Location)
                                  .Include(dp => dp.SubCategory2)
                                  .Include(dp => dp.BerthOccupationDocuments)
                                  .Include(dp => dp.BerthOccupationDocuments.Select(d => d.Document))
                                       //.Include(dp=>dp.WorkflowInstance)
                                  .Select()

                                   where dp.IsDOFinal == "Y "
                                   orderby dp.ModifiedDate descending
                                   select dp).ToList();

            return dredgingVolumes.MapToDto();
        }

        public List<DredgingPriorityVO> GetDredgingPriorityPendingView(int dredgingPriorityId)
        {

            //var dredgingprioritys = new List<DredgingPriority>();


            //dredgingprioritys = (from p in _unitOfWork.Repository<DredgingPriority>().Query()
            //                 .Include(p => p.DredgingOperations)
            //                .Include(p => p.DredgingPriorityDocuments)
            //                .Include(p => p.FinancialYear)
            //                .Include(p => p.DredgingOperations.Select(s => s.SubCategory2))
            //                .Include(p => p.DredgingOperations.Select(b => b.Berth))
            //                .Include(p => p.DredgingOperations.Select(l => l.Location))
            //                .Include(p => p.DredgingOperations.Select(f => f.FinancialYear))
            //                .Include(p => p.DredgingPriorityDocuments.Select(d => d.Document))

            //                .Select()
            //                    // where p.DredgingOperations.Count > 0 && p.DredgingOperations.FirstOrDefault().DredgingOperationID == dredgingpriorityid
            //                     where  p.DredgingOperations.FirstOrDefault().DredgingOperationID == dredgingpriorityid
            //                     orderby p.DredgingPriorityID descending
            //                     select p).ToList<DredgingPriority>();



            //// return dockingplans.MapToDTOForDocking();

            //return dredgingprioritys.MapToDTO();


            var dredgingprioritys = (from p in _unitOfWork.Repository<DredgingPriority>().Query().Select()

                                     join p1 in _unitOfWork.Repository<DredgingOperation>().Query().Select() on p.DredgingPriorityID equals p1.DredgingPriorityID
                                     // join p2 in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>() on p1.TypeCode equals p2.SubCatCode
                                     // join p3 in _unitOfWork.Repository<Berth>().Query().Select().AsEnumerable<Berth>() on p1.BerthCode equals p3.BerthCode
                                     // join p4 in _unitOfWork.Repository<Location>().Query().Select().AsEnumerable<Location>() on p1.AreaLocationID equals p4.LocationID
                                     // join p5 in _unitOfWork.Repository<FinancialYear>().Query().Select().AsEnumerable<FinancialYear>() on p.FinancialYearID equals p5.FinancialYearID
                                     // join p6 in _unitOfWork.Repository<DredgingPriorityDocument>().Query().Select().AsEnumerable<DredgingPriorityDocument>() on p.DredgingPriorityID equals p6.DredgingPriorityID
                                     // join p7 in _unitOfWork.Repository<Document>().Query().Select().AsEnumerable<Document>() on p6.DocumentID equals p7.DocumentID
                                     where p1.DredgingOperationID == dredgingPriorityId
                                     // where p.DredgingPriorityID == dpd.DredgingPriorityID && p.DredgingPriorityID == dpa.DredgingPriorityID
                                     // orderby p.DredgingPriorityID descending
                                     select new DredgingPriorityVO
                                     {

                                         DredgingPriorityID = p.DredgingPriorityID,
                                         DredgingOperationID = p1.DredgingOperationID,
                                         FinancialYearID = p.FinancialYearID,
                                         FromDate = p.FromDate.ToString(),
                                         MonthValue = Convert.ToString(p.FromDate.ToString("yyyy-MM", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                                         DPAWorkflowInstanceID = p1.DPAWorkflowInstanceID
                                         // DocumentID = p6.DocumentID,
                                         // DredgingPriorityDocumentID=p6.DredgingPriorityDocumentID,
                                         //  FileName = p7.FileName,
                                         // DocumentID = p6.DocumentID == null ? "" : _unitOfWork.Repository<DredgingPriorityDocument>().Query().Select().Where(p.DredgingPriorityID == p6.DredgingPriorityID).Select(DredgingPriorityDocument.DocumentID).FirstOrDefault(),
                                     });


            // return dockingplans.MapToDTOForDocking();
            return dredgingprioritys.ToList();
            //return dredgingprioritys.MapToDTO();


        }
        public List<DredgingOperationVO> GetDredgingPriorityAreaDetails(int dredgingPriorityId)
        {
            var dredgingoperation = (from dp in _unitOfWork.Repository<DredgingOperation>().Query().Select()
                                     //  join bt in _unitOfWork.Repository<Berth>().Query().Select() on dp.BerthCode equals bt.BerthCode
                                     join sb in _unitOfWork.Repository<SubCategory>().Query().Select() on dp.TypeCode equals sb.SubCatCode
                                     //join lc in  _unitOfWork.Repository<Location>().Query().Select() on dp.AreaLocationID equals lc.LocationID                            
                                     where dp.DredgingPriorityID == dredgingPriorityId
                                     orderby dp.CreatedDate descending
                                     select new DredgingOperationVO
                                     {
                                         DredgingOperationID = dp.DredgingOperationID,
                                         DredgingPriorityID = dp.DredgingPriorityID,
                                         Priority = dp.Priority,
                                         AreaType = dp.AreaType,
                                         BerthCode = _unitOfWork.Repository<Berth>().Query().Select().Where(s => s.BerthCode == dp.BerthCode && s.QuayCode == dp.QuayCode && s.PortCode == dp.PortCode).Select(g => g.BerthCode).FirstOrDefault(),
                                         PortCode = dp.PortCode,
                                         BerthName = _unitOfWork.Repository<Berth>().Query().Select().Where(s => s.BerthCode == dp.BerthCode && s.QuayCode == dp.QuayCode).Select(g => g.BerthName).FirstOrDefault(),
                                         QuayCode = dp.QuayCode,
                                         TypeCode = dp.TypeCode,
                                         DredgingMaterial = sb.SubCatName,
                                         AreaLocationID = dp.AreaLocationID,
                                         LocationName = _unitOfWork.Repository<Location>().Query().Select().Where(s => s.LocationID == dp.AreaLocationID).Select(g => g.LocationName).FirstOrDefault(),
                                         RequiredDate = dp.RequiredDate.ToString(),
                                         DesignDepth = dp.DesignDepth,
                                         PromulgateDepth = dp.PromulgateDepth,
                                         Requirement = dp.Requirement,
                                         DPARemarks = dp.DPARemarks,
                                         RecordStatus = dp.RecordStatus,
                                         CreatedBy = dp.CreatedBy,
                                         CreatedDate = dp.CreatedDate,
                                         ModifiedBy = dp.ModifiedBy,
                                         ModifiedDate = dp.ModifiedDate,
                                         IsDPAFinal = dp.IsDPAFinal,
                                         AreaName = dp.AreaName,
                                         DPAWorkflowInstanceID = dp.DPAWorkflowInstanceID,
                                         BerthKey = dp.PortCode + "." + dp.QuayCode + "." + dp.BerthCode,

                                     });

            return dredgingoperation.ToList();


        }

        public List<DredgingOperationVO> GetDredgingPriorityAreaDetailsPending(int dredgingPriorityId)
        {
            var dredgingoperation = (from dp in _unitOfWork.Repository<DredgingOperation>().Query().Select()
                                     //  join bt in _unitOfWork.Repository<Berth>().Query().Select() on dp.BerthCode equals bt.BerthCode
                                     join sb in _unitOfWork.Repository<SubCategory>().Query().Select() on dp.TypeCode equals sb.SubCatCode
                                     //join lc in  _unitOfWork.Repository<Location>().Query().Select() on dp.AreaLocationID equals lc.LocationID                            
                                     where dp.DredgingOperationID == dredgingPriorityId
                                     select new DredgingOperationVO
                                     {
                                         DredgingOperationID = dp.DredgingOperationID,
                                         DredgingPriorityID = dp.DredgingPriorityID,
                                         Priority = dp.Priority,
                                         AreaType = dp.AreaType,
                                         BerthCode = _unitOfWork.Repository<Berth>().Query().Select().Where(s => s.BerthCode == dp.BerthCode && s.QuayCode == dp.QuayCode && s.PortCode == dp.PortCode).Select(g => g.BerthCode).FirstOrDefault(),
                                         PortCode = dp.PortCode,
                                         BerthName = _unitOfWork.Repository<Berth>().Query().Select().Where(s => s.BerthCode == dp.BerthCode && s.QuayCode == dp.QuayCode).Select(g => g.BerthName).FirstOrDefault(),
                                         QuayCode = dp.QuayCode,
                                         TypeCode = dp.TypeCode,
                                         DredgingMaterial = sb.SubCatName,
                                         AreaLocationID = dp.AreaLocationID,
                                         LocationName = _unitOfWork.Repository<Location>().Query().Select().Where(s => s.LocationID == dp.AreaLocationID).Select(g => g.LocationName).FirstOrDefault(),
                                         RequiredDate = dp.RequiredDate.ToString(),
                                         DesignDepth = dp.DesignDepth,
                                         PromulgateDepth = dp.PromulgateDepth,
                                         Requirement = dp.Requirement,
                                         DPARemarks = dp.DPARemarks,
                                         RecordStatus = dp.RecordStatus,
                                         CreatedBy = dp.CreatedBy,
                                         CreatedDate = dp.CreatedDate,
                                         ModifiedBy = dp.ModifiedBy,
                                         ModifiedDate = dp.ModifiedDate,
                                         IsDPAFinal = dp.IsDPAFinal,
                                         AreaName = dp.AreaName,
                                         DPAWorkflowInstanceID = dp.DPAWorkflowInstanceID,
                                         BerthKey = dp.PortCode + "." + dp.QuayCode + "." + dp.BerthCode,
                                     });

            return dredgingoperation.ToList();


        }

        public List<DredgingOperationVO> GetDredgingVolumeById(int id)
        {
            var dredgingVolumes = (from dp in _unitOfWork.Repository<DredgingOperation>().Query()
                                  .Include(dp => dp.FinancialYear)
                                  .Include(dp => dp.DredgingPriority)
                                  .Include(dp => dp.Berth)
                                  .Include(dp => dp.Location)
                                  .Include(dp => dp.SubCategory2)
                                  .Include(dp => dp.BerthOccupationDocuments)
                                  .Include(dp => dp.BerthOccupationDocuments.Select(d => d.Document))
                                       //.Include(dp=>dp.WorkflowInstance)
                                  .Select()
                                   where dp.DredgingOperationID == id
                                   //&& dp.WorkflowInstance.WorkflowTaskCode == WFStatus.Approved
                                   orderby dp.ModifiedDate descending
                                   select dp).ToList();

            return dredgingVolumes.MapToDto();
        }


        /// <summary>
        ///  To get Dredging Priority Details
        /// </summary>
        /// <returns></returns>
        public List<DredgingPriorityDocumentVO> GetDocument(int dredgingPriorityId)
        {
            var dredgingprioritys = new List<DredgingPriorityDocument>();


            dredgingprioritys = (from p in _unitOfWork.Repository<DredgingPriorityDocument>().Query()
                             .Include(p => p.Document)


                            .Select()

                                 where p.DredgingPriorityID == dredgingPriorityId

                                 // where p.DredgingPriorityID == dpd.DredgingPriorityID && p.DredgingPriorityID == dpa.DredgingPriorityID
                                 orderby p.DredgingPriorityID descending
                                 select p).ToList<DredgingPriorityDocument>();



            // return dockingplans.MapToDTOForDocking();

            return dredgingprioritys.MapToDto();


        }
    }
}
