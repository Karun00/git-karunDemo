using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class SuppHotColdWorkPermitDocumentMapExtension
    {
        public static List<SuppHotColdWorkPermitDocumentVO> MapToDTO(this List<SuppHotColdWorkPermitDocument> suppHotColdWorkPermitDocuments)
        {
            List<SuppHotColdWorkPermitDocumentVO> lstSuppHotColdWorkPermitDocumentVO = new List<SuppHotColdWorkPermitDocumentVO>();

            foreach (SuppHotColdWorkPermitDocument suppHotColdWorkPermitDocument in suppHotColdWorkPermitDocuments)
            {
                lstSuppHotColdWorkPermitDocumentVO.Add(suppHotColdWorkPermitDocument.MapToDTO());
            }

            return lstSuppHotColdWorkPermitDocumentVO;
        }

        public static List<SuppHotColdWorkPermitDocument> MapToEntity(this List<SuppHotColdWorkPermitDocumentVO> suppHotColdWorkPermitDocumentsvo)
        {
            List<SuppHotColdWorkPermitDocument> lstSuppHotColdWorkPermitDocument = new List<SuppHotColdWorkPermitDocument>();

            foreach (SuppHotColdWorkPermitDocumentVO suppHotColdWorkPermitDocumentVO in suppHotColdWorkPermitDocumentsvo)
            {
                lstSuppHotColdWorkPermitDocument.Add(suppHotColdWorkPermitDocumentVO.MapToEntity());
            }

            return lstSuppHotColdWorkPermitDocument;
        }

        public static SuppHotColdWorkPermitDocumentVO MapToDTO(this SuppHotColdWorkPermitDocument data)
        {
            return new SuppHotColdWorkPermitDocumentVO
            {
                //suppHotColdWorkPermitDocumentVO = new SuppHotColdWorkPermitDocumentVO();

                //var lstArrivalReasons = (data.ArrivalNotification.ArrivalReasons.Select(k => k.SubCategory.SubCatName)).ToList<String>();
                //        suppServiceRequestVO.ArrivalNotification.ReasonForVisit = lstArrivalReasons.Count > 0 ? String.Join(", ", lstArrivalReasons) : "NA";

                SuppHotColdWorkPermitDocumentID = data.SuppHotColdWorkPermitDocumentID,
                SuppHotColdWorkPermitID = data.SuppHotColdWorkPermitID,
                DocumentID = data.DocumentID,
                DocumentTypeName = (data.Document.SubCategory1 != null ? data.Document.SubCategory1.SubCatName : ""),
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate,
                FileName = data.Document.FileName

                //return suppHotColdWorkPermitDocumentVO;
            };
        }

        public static SuppHotColdWorkPermitDocument MapToEntity(this SuppHotColdWorkPermitDocumentVO vo)
        {
            return new SuppHotColdWorkPermitDocument
            {
                //suppHotColdWorkPermitDocument = new SuppHotColdWorkPermitDocument();

                SuppHotColdWorkPermitDocumentID = vo.SuppHotColdWorkPermitDocumentID,
                SuppHotColdWorkPermitID = vo.SuppHotColdWorkPermitID,
                DocumentID = vo.DocumentID,
                CreatedBy = vo.CreatedBy,
                CreatedDate = vo.CreatedDate,
                ModifiedBy = vo.ModifiedBy,
                ModifiedDate = vo.ModifiedDate

                //return suppHotColdWorkPermitDocument;
            };
        }
    }
}

