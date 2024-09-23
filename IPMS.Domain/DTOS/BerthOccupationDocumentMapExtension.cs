using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class BerthOccupationDocumentMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<BerthOccupationDocumentVO> MapToDto(this IEnumerable<BerthOccupationDocument> berthOccupationDocument)
        {
            var berthoccupationDocumentVOList = new List<BerthOccupationDocumentVO>();
            if (berthOccupationDocument != null)
            {
                foreach (var item in berthOccupationDocument)
                {
                    berthoccupationDocumentVOList.Add(item.MapToDto());
                }
            }
            return berthoccupationDocumentVOList;
        }

        public static List<BerthOccupationDocument> MapToEntity(this List<BerthOccupationDocumentVO> berthOccupationDocumentsVo)
        {
            List<BerthOccupationDocument> lstberthoccupationDocumentDocument = new List<BerthOccupationDocument>();
            if (berthOccupationDocumentsVo != null)
            {
                foreach (BerthOccupationDocumentVO berthoccupationDocumentvo in berthOccupationDocumentsVo)
                {
                    lstberthoccupationDocumentDocument.Add(berthoccupationDocumentvo.MapToEntity());
                }
            }
            return lstberthoccupationDocumentDocument;
        }

        public static BerthOccupationDocumentVO MapToDto(this BerthOccupationDocument data)
        {
            if (data != null)
            {
            }
            return new BerthOccupationDocumentVO

                {
                    BerthOccupationDocumentID = data.BerthOccupationDocumentID,
                    DredgingOperationID = data.DredgingOperationID,
                    DocumentID = data.DocumentID,
                    RecordStatus = data.RecordStatus,
                    FileName = data.Document.FileName
                };
            
        }

        public static BerthOccupationDocument MapToEntity(this BerthOccupationDocumentVO Vo)
        {
            if (Vo != null)
            {
            }
            return new BerthOccupationDocument
            {
                BerthOccupationDocumentID = Vo.BerthOccupationDocumentID,
                DredgingOperationID = Vo.DredgingOperationID,
                DocumentID = Vo.DocumentID,
                RecordStatus = "A"
            };
        }
    }
}
