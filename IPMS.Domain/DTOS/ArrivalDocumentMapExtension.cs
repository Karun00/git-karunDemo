using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ArrivalDocumentMapExtension
    {

        /// <summary>
        /// Data List Transfer from Entity to DTO  
        /// </summary>
        /// <param name="arrivalDocuments"></param>
        /// <returns></returns>
        public static List<ArrivalDocumentVo> MapToDto(this IEnumerable<ArrivalDocument> arrivalDocuments)
        {
            var arrivalDocumentVoList = new List<ArrivalDocumentVo>();
            if (arrivalDocuments != null)
            {
                foreach (var item in arrivalDocuments)
                {
                    arrivalDocumentVoList.Add(item.MapToDto());
                }
            }
            return arrivalDocumentVoList;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="arrivalDocumentVoList"></param>
        /// <returns></returns>
        public static List<ArrivalDocument> MapToEntity(this IEnumerable<ArrivalDocumentVo> arrivalDocumentVoList)
        {

            var arrivalDocuments = new List<ArrivalDocument>();
            if (arrivalDocumentVoList != null)
            {
                foreach (var item in arrivalDocumentVoList)
                {
                    arrivalDocuments.Add(item.MapToEntity());
                }
            }
            return arrivalDocuments;
        }

        /// <summary>
        ///  Data Transfer from Entity to DTO        
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ArrivalDocumentVo MapToDto(this ArrivalDocument data)
        {
            if (data == null)
                return new ArrivalDocumentVo();

            return new ArrivalDocumentVo
            {
                VCN = data.VCN,
                DocumentID = data.DocumentID,
                DocumentCode = data.DocumentCode,
                DocumentName = (data.SubCategory == null ? "" : data.SubCategory.SubCatName),
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
        public static ArrivalDocument MapToEntity(this ArrivalDocumentVo data)
        {

            if (data == null)
                return new ArrivalDocument();

            return new ArrivalDocument
            {
                VCN = data.VCN,
                DocumentID = data.DocumentID,
              
                DocumentCode = data.DocumentCode,
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
