using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System;

namespace IPMS.Domain.DTOS
{
    public static class SuppDryDockDocumentMapExtension
    {

        public static List<SuppDryDockDocumentVO> MapToDto(this IEnumerable<SuppDryDockDocument> suppDryDockDocuments)
        {
            List<SuppDryDockDocumentVO> suppDryDockDocumentVOs = new List<SuppDryDockDocumentVO>();
            if (suppDryDockDocuments != null)
            {
                foreach (SuppDryDockDocument obj in suppDryDockDocuments)
                {
                    suppDryDockDocumentVOs.Add(obj.MapToDto());
                }
            }
            return suppDryDockDocumentVOs;
        }

        public static List<SuppDryDockDocument> MapToEntity(this IEnumerable<SuppDryDockDocumentVO> suppDryDockDocumentVOs)
        {
            List<SuppDryDockDocument> suppDryDockDocuments = new List<SuppDryDockDocument>();
            if (suppDryDockDocumentVOs != null)
            {
                foreach (SuppDryDockDocumentVO obj in suppDryDockDocumentVOs)
                {
                    suppDryDockDocuments.Add(obj.MapToEntity());
                }
            }
            return suppDryDockDocuments;
        }

        public static SuppDryDockDocumentVO MapToDto(this SuppDryDockDocument data)
        {
            return new SuppDryDockDocumentVO
            {
                SuppDryDockDocumentID = data.SuppDryDockDocumentID,
                SuppDryDockID = data.SuppDryDockID,
                DocumentID = data.DocumentID,
                RecordStatus = data.RecordStatus,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate,
                Document = data.Document.MapToDTO(),
                FileName = data.Document.FileName,
                DocumentTypeName = data.Document.SubCategory1!=null?data.Document.SubCategory1.SubCatName:""
            };
        }

        public static SuppDryDockDocument MapToEntity(this SuppDryDockDocumentVO vo)
        {
            return new SuppDryDockDocument
            {
                SuppDryDockDocumentID = vo.SuppDryDockDocumentID,
                SuppDryDockID = vo.SuppDryDockID,
                DocumentID = vo.DocumentID,
                RecordStatus = vo.RecordStatus,
                CreatedBy = vo.CreatedBy,
                CreatedDate = vo.CreatedDate,
                ModifiedBy = vo.ModifiedBy,
                ModifiedDate = vo.ModifiedDate
            };
        }
    }
}
