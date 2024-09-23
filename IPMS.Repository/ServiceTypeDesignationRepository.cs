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
using System.Linq;

namespace IPMS.Repository
{
    public class ServiceTypeDesignationRepository : IServiceTypeDesignationRepository
    {
        private IUnitOfWork _unitOfWork;
        //private readonly ILog log;

        public ServiceTypeDesignationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
          //  log =
 LogManager.GetLogger(typeof(ServiceTypeDesignationRepository));
        }

        /// <summary>
        /// To Get ServiceTypeDesignation Details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> ServiceTypeDesignationDetails(string portCode)
        {
            var data = (from re in _unitOfWork.Repository<ServiceType>().Query()
                            .Include(s => s.ServiceTypeDesignations)
                             .Select()
                        where re.IsServiceType == "N"
                        orderby re.ServiceTypeName
                        select new ServiceTypeVO
                        {
                            ServiceTypeID = re.ServiceTypeID,
                            IsCraft = re.IsCraft,
                            ServiceTypeName = re.ServiceTypeName,
                            ServiceTypeCode = re.ServiceTypeCode,
                            ServiceTypeDesignations = re.ServiceTypeDesignations.Where(t => t.PortCode == portCode).MapToDto(),
                            ModifiedDate = re.ModifiedDate,
                            ModifiedBy = re.ModifiedBy,
                            RecordStatus = re.RecordStatus,
                            CreatedBy = re.CreatedBy,
                            CreatedDate = re.CreatedDate
                        });

            return data.ToList();
        }

        /// <summary>
        /// To Add ServiceTypeDesignation Data
        /// </summary>
        /// <param name="stdData"></param>
        /// <param name="userId"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public ServiceTypeVO AddServiceTypeDesignation(ServiceTypeVO stdData, int userId, string portCode)
        {
            if (stdData != null)
            {
            List<ServiceTypeDesignation> designations = stdData.ServiceTypeDesignations.MapToEntity();
            
                if (designations.Count > 0)
                {
                    foreach (var item in designations)
                    {
                        item.ServiceTypeID = stdData.ServiceTypeID;
                        item.RecordStatus = "A";
                        item.PortCode = portCode;
                        item.CreatedBy = userId;
                        item.CreatedDate = DateTime.Now;
                        _unitOfWork.Repository<ServiceTypeDesignation>().Insert(item);
                        _unitOfWork.SaveChanges();
                    }
                }
            }

            return stdData;
        }

        /// <summary>
        /// To Modify ServiceTypeDesignation Data
        /// </summary>
        /// <param name="stdData"></param>
        /// <returns></returns>
        public ServiceTypeVO ModifyServiceTypeDesignation(ServiceTypeVO stdData, int userId, string portCode)
        {
            if (stdData != null)
            {
                List<ServiceTypeDesignation> designations = stdData.ServiceTypeDesignations.MapToEntity();

                List<ServiceTypeDesignation> list =
                    _unitOfWork.Repository<ServiceTypeDesignation>()
                        .Queryable()
                        .Where(e => e.ServiceTypeID == stdData.ServiceTypeID && e.PortCode == portCode)
                        .ToList();

                if (list.Count > 0)
                {
                    foreach (ServiceTypeDesignation item in list)
                    {
                        _unitOfWork.Repository<ServiceTypeDesignation>().Delete(item);
                    }
                    _unitOfWork.SaveChanges();
                }

                if (designations.Count > 0)
                {
                    foreach (var item in designations)
                    {
                        item.ServiceTypeID = stdData.ServiceTypeID;
                        item.PortCode = portCode;
                        item.RecordStatus = "A";
                        item.CreatedBy = userId;
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedBy = userId;
                        item.ModifiedDate = DateTime.Now;

                        if (!string.IsNullOrWhiteSpace(item.DesignationCode))
                        {
                            int? count =
                                _unitOfWork.Repository<ServiceTypeDesignation>()
                                    .Queryable()
                                    .Where(e => e.ServiceTypeID == item.ServiceTypeID
                                                && e.PortCode == item.PortCode &&
                                                e.DesignationCode == item.DesignationCode).Count();

                            if (count > 0)
                            {
                            }
                            else
                            {
                                _unitOfWork.Repository<ServiceTypeDesignation>().Insert(item);
                                _unitOfWork.SaveChanges();
                            }
                        }
                    }
                }
            }
            return stdData;
        }

        /// <summary>
        /// To Get Service Types Details
        /// </summary>
        /// <returns></returns>
        public List<ServiceType> GetServiceTypes()
        {
            var serviceTypes =_unitOfWork.Repository<ServiceType>().Queryable().Where(x=>x.RecordStatus== RecordStatus.Active && x.IsServiceType=="N").OrderBy(x=>x.ServiceTypeName).ToList();
            return serviceTypes;
        }
       
        /// <summary>
        /// This method is used for fetches the designation details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            var DesignationType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.RecordStatus == RecordStatus.Active && x.SupCatCode == "DESG").OrderBy(x => x.SubCatName);
            return DesignationType.ToList();
        }

        /// <summary>
        /// To Get Craft Types Details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetCraftTypes(string portCode)
        {
            var crafts = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.RecordStatus == RecordStatus.Active && x.SupCatCode == "CRFY").OrderBy(x=>x.SubCatName).ToList();
            return crafts;
        }
    }
}

