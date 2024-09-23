using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class DredgingPriorityDocumentMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<DredgingPriorityDocumentVO> MapToDto(this IEnumerable<DredgingPriorityDocument> dredgingPriorityDocument)
        {
            var dredgingpriorityDocumentVOList = new List<DredgingPriorityDocumentVO>();
            if (dredgingPriorityDocument != null)
            {
                foreach (var item in dredgingPriorityDocument)
                {
                    dredgingpriorityDocumentVOList.Add(item.MapToDto());
                }
            }
            return dredgingpriorityDocumentVOList;
        }

        public static List<DredgingPriorityDocument> MapToEntity(this List<DredgingPriorityDocumentVO> dredgingPriorityDocumentsVO)
        {
            List<DredgingPriorityDocument> lstdredgingpriorityDocument = new List<DredgingPriorityDocument>();
            if (dredgingPriorityDocumentsVO != null)
            {
                foreach (DredgingPriorityDocumentVO dredgingpriorityDocumentvo in dredgingPriorityDocumentsVO)
                {
                    lstdredgingpriorityDocument.Add(dredgingpriorityDocumentvo.MapToEntity());
                }
            }

            return lstdredgingpriorityDocument;
        }
        public static DredgingPriorityDocumentVO MapToDto(this DredgingPriorityDocument data)
        {
            DredgingPriorityDocumentVO VO = new DredgingPriorityDocumentVO();
            if (data != null)
            {
                VO.DredgingPriorityID = data.DredgingPriorityID;
                VO.DocumentID = data.DocumentID;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
                VO.FileName = data.Document.FileName;
            }
            return VO;

        }
        public static DredgingPriorityDocument MapToEntity(this DredgingPriorityDocumentVO vo)
        {
            DredgingPriorityDocument data = new DredgingPriorityDocument();
            if (vo != null)
            {
                data.DredgingPriorityID = vo.DredgingPriorityID;
                data.DocumentID = vo.DocumentID;
                data.RecordStatus = "A";
                data.CreatedBy = vo.CreatedBy;
                data.CreatedDate = vo.CreatedDate;
                data.ModifiedBy = vo.ModifiedBy;
                data.ModifiedDate = vo.ModifiedDate;
            }
            return data;
        }
    }
}
