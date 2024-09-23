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
        public static List<BerthOccupationDocumentVO> MapToDTO(this IEnumerable<BerthOccupationDocument> berthoccupationDocument)
        {
            var berthoccupationDocumentVOList = new List<BerthOccupationDocumentVO>();
            foreach (var item in berthoccupationDocument)
            {
                berthoccupationDocumentVOList.Add(item.MapToDTO());
            }
            return berthoccupationDocumentVOList;
        }

        public static List<BerthOccupationDocument> MapToEntity(this List<BerthOccupationDocumentVO> berthoccupationDocumentsvo)
        {
            List<BerthOccupationDocument> lstberthoccupationDocumentDocument = new List<BerthOccupationDocument>();

            foreach (BerthOccupationDocumentVO berthoccupationDocumentvo in berthoccupationDocumentsvo)
            {
                lstberthoccupationDocumentDocument.Add(berthoccupationDocumentvo.MapToEntity());
            }

            return lstberthoccupationDocumentDocument;
        }

        public static BerthOccupationDocumentVO MapToDTO(this BerthOccupationDocument data)
        {
            BerthOccupationDocumentVO VO = new BerthOccupationDocumentVO();
            VO.BerthOccupationDocumentID = data.BerthOccupationDocumentID;
            VO.DredgingOperationID = data.DredgingOperationID;
            VO.DocumentID = data.DocumentID;
            VO.RecordStatus = data.RecordStatus;
            VO.FileName = data.Document.FileName;
            return VO;

        }

        public static BerthOccupationDocument MapToEntity(this BerthOccupationDocumentVO VO)
        {
            BerthOccupationDocument data = new BerthOccupationDocument();
            data.BerthOccupationDocumentID = VO.BerthOccupationDocumentID;
            data.DredgingOperationID = VO.DredgingOperationID;
            data.DocumentID = VO.DocumentID;
            data.RecordStatus = "A";
            return data;
        }
    }
}
