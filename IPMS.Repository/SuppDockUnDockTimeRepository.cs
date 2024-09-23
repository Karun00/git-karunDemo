using Core.Repository;
using IPMS.Domain.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Globalization;

namespace IPMS.Repository
{
    public class SuppDockUnDockTimeRepository : ISuppDockUnDockTimeRepository
    {
        private IUnitOfWork _unitOfWork;

        public SuppDockUnDockTimeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Author  : Srini
        /// Date    : 22st Sep 2014
        /// Purpose : To Get approved Dry dock Supplementary Service Request details
        /// </summary>
        /// <returns></returns>
        public List<SuppDryDockVO> AllSuppDockUnDockTimeDetails(string portCode)
        {
            var servicerequestlist = (from sd in _unitOfWork.Repository<SuppDryDock>().Queryable()
                                          // .Include(sd => sd.SuppDockUnDockTimes)
                                      .Include(sd => sd.Berth)
                                      .Include(sd => sd.SuppDryDockDocuments)
                                      .Include(p => p.SuppDryDockDocuments.Select(d => d.Document))
                                      .Include(sd => sd.WorkflowInstance)
                                      .Include(sd => sd.WorkflowInstance.SubCategory)
                                      .Include(sd => sd.ArrivalNotification.Vessel)
                                       .Include(sd => sd.ArrivalNotification.Berth1)
                                      //.Include(sd => sd.ArrivalNotification.VesselCalls)     
                                      // where sd.WorkflowInstance.SubCategory.SubCatCode == WFStatus.Confirmed
                                      where sd.DockPortCode == portCode && (sd.ScheduleStatus == DryDockStatus.Confirmed || sd.ScheduleStatus == DryDockStatus.Docking || sd.ScheduleStatus == DryDockStatus.UnDocking)
                                      select sd).OrderByDescending(x=>x.ModifiedDate).ToList();

            return servicerequestlist.MapToDto();
        }

        public SuppDryDock GetSuppDockUndockDetailsByID(string suppdrydockid)
        {

            var suppdrydockundocks = (from sd in _unitOfWork.Repository<SuppDryDock>().Query().Select()
                                      where sd.SuppDryDockID == Convert.ToInt32(suppdrydockid, CultureInfo.InvariantCulture)
                                      select new SuppDryDock
                                         {
                                             SuppDryDockID = sd.SuppDryDockID

                                         }).FirstOrDefault<SuppDryDock>();

            return suppdrydockundocks;

        }
    }
}
