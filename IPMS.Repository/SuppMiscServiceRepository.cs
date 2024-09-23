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
using System.Globalization;

namespace IPMS.Repository
{
    public class SuppMiscServiceRepository : ISuppMiscServiceRepository
    {
        private IUnitOfWork _unitOfWork;
       // private readonly ILog log;

        public SuppMiscServiceRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();         
            LogManager.GetLogger(typeof(SuppMiscServiceRepository));
        }


        /// <summary>
        ///  To get Miscellaneous Details
        /// </summary>
        /// <returns></returns>
        public List<SuppDryDockVO> GetSuppMiscServiceDetails(string portCode)
        {
            var suppMiscList = (from sm in _unitOfWork.Repository<SuppDryDock>().Queryable()
                              .Include(sm => sm.SuppMiscServices)
                                .Include(sm => sm.ArrivalNotification)
                                .Include(sm => sm.ArrivalNotification.Berth1)
                                .Include(sm => sm.ArrivalNotification.Vessel)
                                .Include(sm => sm.ArrivalNotification.Agent)
                                where sm.DockPortCode == portCode 
                                && sm.EnteredDockDateTime != null                               
                                select sm).OrderByDescending(x=>x.ModifiedDate).ToList<SuppDryDock>();               
            

            return suppMiscList.MapToDtoSuppMisc();
           


        }


        /// <summary>
        ///  To get Service Types
        /// </summary>
        /// <returns></returns>
        public List<SuppMiscServiceVO> GetServiceTypes()
        {
            var servicetypes = (from sr in _unitOfWork.Repository<ServiceType>().Queryable()
                                        where sr.IsServiceType == "Y"
                                select new SuppMiscServiceVO
                                {
                                  
                                    ServiceTypeID = sr.ServiceTypeID,
                                    ServiceTypeName = sr.ServiceTypeName,
                                    ServiceTypeCode = sr.ServiceTypeCode
                                  
                                });

            return servicetypes.ToList();


        }

        /// <summary>
        ///  To get Miscellaneous Recording Details
        /// </summary>
        /// <returns></returns>
        public List<SuppMiscServiceVO> GetSuppMiscServiceRecordDetails(int suppdrydockid)
        {
            var suppMiscList = (from sm in _unitOfWork.Repository<SuppMiscService>().Query().Select()
                                join st in _unitOfWork.Repository<ServiceType>().Query().Select() on sm.ServiceTypeID equals st.ServiceTypeID
                                join sb in _unitOfWork.Repository<SubCategory>().Query().Select() on st.ServiceUOM equals sb.SubCatCode                                
                                orderby sm.CreatedDate descending//order by added by divya on 31Oct2017 
                                where sm.SuppDryDockID == suppdrydockid
                                select new SuppMiscServiceVO
                                {
                                    SuppMiscServiceID = sm.SuppMiscServiceID,
                                    SuppDryDockID = sm.SuppDryDockID,
                                    ServiceTypeID = sm.ServiceTypeID,
                                    ServiceTypeName = st.ServiceTypeName,
                                    ServiceTypeCode = sm.ServiceTypeCode,
                                    Phase = sm.Phase,
                                    FromDateTime = sm.FromDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                                    ToDateTime = sm.ToDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                                    StartMeterReading = sm.StartMeterReading,//added by divya on 31stNov2017
                                    EndMeterReading = sm.EndMeterReading,//added by divya on 31stNov2017
                                    Quantity = sm.Quantity,
                                    UOMCode = sb.SubCatName,
                                    Remarks = sm.Remarks,
                                    RecordStatus = sm.RecordStatus,
                                    CreatedBy = sm.CreatedBy,
                                    CreatedDate = sm.CreatedDate,
                                    ModifiedBy = sm.ModifiedBy,
                                    ModifiedDate = sm.ModifiedDate
                                });

            return suppMiscList.ToList();


        }
    }
}
