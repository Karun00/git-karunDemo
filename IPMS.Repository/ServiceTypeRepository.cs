using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private IUnitOfWork _unitOfWork;
        //  private readonly ILog log;

        public ServiceTypeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            // log = LogManager.GetLogger(typeof(ServiceTypeRepository));
        }

        IEnumerable<char> CharsToTitleCase(string s)
        {
            bool newWord = true;
            foreach (char c in s)
            {
                if (newWord) { yield return Char.ToUpper(c); newWord = false; }
                else yield return Char.ToLower(c);
                if (c == ' ') newWord = true;
            }
        }

        /// <summary>
        /// To Get ServiceType Details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> ServiceTypeDetails()
        {
            var ServiceTypeDetails = _unitOfWork.Repository<ServiceType>().Queryable().AsEnumerable<ServiceType>().Where(x => x.IsServiceType == "N").OrderBy(x => x.ServiceTypeName).ToList().MapToDto();
            return ServiceTypeDetails.ToList();
        }

        /// <summary>
        /// To Add ServiceType Data
        /// </summary>
        /// <param name="serviceTypeData"></param>
        /// <returns></returns>
        public ServiceTypeVO AddServiceType(ServiceTypeVO serviceTypeData, int userId)
        {
            if (serviceTypeData != null)
            {
                if (!string.IsNullOrWhiteSpace(serviceTypeData.ServiceTypeName))
                {
                    serviceTypeData.ServiceTypeName =
                        new string(CharsToTitleCase(serviceTypeData.ServiceTypeName).ToArray());
                }

                serviceTypeData.CreatedBy = userId;
                serviceTypeData.CreatedDate = DateTime.Now;
                serviceTypeData.ModifiedBy = userId;
                serviceTypeData.ModifiedDate = DateTime.Now;
                ServiceType ServiceType = new ServiceType();
                ServiceType = ServiceTypeMapExtension.MapToEntity(serviceTypeData);
                ServiceType.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<ServiceType>().Insert(ServiceType);
                _unitOfWork.SaveChanges();
            }
            return serviceTypeData;
        }

        /// <summary>
        /// To Modify ServiceType Data
        /// </summary>
        /// <param name="serviceTypeData"></param>
        /// <returns></returns>
        public ServiceTypeVO ModifyServiceType(ServiceTypeVO serviceTypeData, int userId)
        {
            if (serviceTypeData != null)
            {
                if (!string.IsNullOrWhiteSpace(serviceTypeData.ServiceTypeName))
                {
                    serviceTypeData.ServiceTypeName =
                        new string(CharsToTitleCase(serviceTypeData.ServiceTypeName).ToArray());
                }

                serviceTypeData.ModifiedBy = userId;
                serviceTypeData.ModifiedDate = DateTime.Now;
                ServiceType ServiceType = new ServiceType();
                ServiceType = ServiceTypeMapExtension.MapToEntity(serviceTypeData);
                ServiceType.CreatedDate = serviceTypeData.CreatedDate;
                ServiceType.ModifiedDate = DateTime.Now;
                ServiceType.ModifiedBy = userId;
                ServiceType.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<ServiceType>().Update(ServiceType);
                _unitOfWork.SaveChanges();
            }
            return serviceTypeData;
        }
    }
}

