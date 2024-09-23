using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class ServiceRequestDocumentMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO  
        /// </summary>
        /// <param name="arrivalDocuments"></param>
        /// <returns></returns>
        public static List<ServiceRequestDocumentVO> MapToDto(this IEnumerable<ServiceRequestDocument> serviceDocuments)
        {
            var serviceDocumentVoList = new List<ServiceRequestDocumentVO>();
            if (serviceDocuments != null)
            {
                foreach (var item in serviceDocuments)
                {
                    serviceDocumentVoList.Add(item.MapToDto());
                }
            }
            return serviceDocumentVoList;
        }


        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="arrivalDocumentVoList"></param>
        /// <returns></returns>
        public static List<ServiceRequestDocument> MapToEntity(this IEnumerable<ServiceRequestDocumentVO> serviceDocumentVoList)
        {

            var serviceDocuments = new List<ServiceRequestDocument>();
            if (serviceDocumentVoList != null)
            {
                foreach (var item in serviceDocumentVoList)
                {
                    serviceDocuments.Add(item.MapToEntity());
                }
            }
            return serviceDocuments;
        }

        public static ServiceRequestDocumentVO MapToDto(this ServiceRequestDocument data)
        {
            if (data == null)
                return new ServiceRequestDocumentVO();

            return new ServiceRequestDocumentVO
            {
                ServiceRequestDocumentID = data.ServiceRequestDocumentID,
                ServiceRequestID = data.ServiceRequestID,
                DocumentID = data.DocumentID,
                DocumentCode = data.DocumentCode,
                DocumentName = (data.SubCategory == null ? "" : data.SubCategory.SubCatName),
                FileName = (data.Document != null ? data.Document.FileName : null),
                RecordStatus = data.RecordStatus,                
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }

        public static ServiceRequestDocument MapToEntity(this ServiceRequestDocumentVO vo)
        {
            ServiceRequestDocument servicerequest = new ServiceRequestDocument();

            if (vo != null)
            {

                servicerequest.ServiceRequestDocumentID = vo.ServiceRequestDocumentID;
                servicerequest.ServiceRequestID = vo.ServiceRequestID;
                servicerequest.DocumentID = vo.DocumentID;
                servicerequest.DocumentCode = vo.DocumentCode;
                servicerequest.RecordStatus = vo.RecordStatus;
                servicerequest.CreatedBy = vo.CreatedBy;
                servicerequest.CreatedDate = vo.CreatedDate;
                servicerequest.ModifiedBy = vo.ModifiedBy;
                servicerequest.ModifiedDate = vo.ModifiedDate;
                if (vo.ServiceRequestDocument != null)
                {
                    servicerequest.Document = vo.ServiceRequestDocument.MapToEntity();
                }
            }
            return servicerequest;
        }
    }
}
