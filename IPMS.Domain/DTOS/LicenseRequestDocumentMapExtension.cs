using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class LicenseRequestDocumentMapExtension
    {


        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="licenseRequestDocument"></param>
        /// <returns></returns>
        public static List<LicenseRequestDocumentVO> MapToDto(this IEnumerable<LicenseRequestDocument> licenseRequestDocument)
        {
            var licenseRequestDocumentVOList = new List<LicenseRequestDocumentVO>();
            if (licenseRequestDocument != null)
            {
                foreach (var item in licenseRequestDocument)
                {
                    licenseRequestDocumentVOList.Add(item.MapToDto());
                }
            }
            return licenseRequestDocumentVOList;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="licenseRequestDocumentVOList"></param>
        /// <returns></returns>
        public static List<LicenseRequestDocument> MapToEntity(this IEnumerable<LicenseRequestDocumentVO> licenseRequestDocumentVOList)
        {
            var licenseRequestDocument = new List<LicenseRequestDocument>();
            if (licenseRequestDocumentVOList != null)
            {
                foreach (var item in licenseRequestDocumentVOList)
                {
                    licenseRequestDocument.Add(item.MapToEntity());
                }
            }
            return licenseRequestDocument;
        }


        /// <summary>
        /// Data Transfer from Entity to  DTO 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static LicenseRequestDocumentVO MapToDto(this LicenseRequestDocument data)
        {
            if (data == null)
                return new LicenseRequestDocumentVO();

            return new LicenseRequestDocumentVO
            {
                LicenseRequestID = data.LicenseRequestID,
                DocumentID = data.DocumentID,
                DocumentName = data.DocumentName,
                RecordStatus = data.RecordStatus,
                FileName = data.FileName,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }

        /// <summary>
        /// Data Transfer from DTO to Entity 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static LicenseRequestDocument MapToEntity(this LicenseRequestDocumentVO data)
        {
            if (data == null)
                return new LicenseRequestDocument();

            return new LicenseRequestDocument
            {
                LicenseRequestID = data.LicenseRequestID,
                DocumentID = data.DocumentID,
                DocumentName = data.DocumentName,
                RecordStatus = data.RecordStatus,
                FileName = data.FileName,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }

    }
}
