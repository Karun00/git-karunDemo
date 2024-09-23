using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class DocumentMapExtension
    {
        public static DocumentVO MapToDTO(this Document data)
        {
            DocumentVO documentVo = new DocumentVO();
            if (data != null)
            {
                documentVo.DocumentID = data.DocumentID;
                documentVo.DocumentType = data.DocumentType;
                documentVo.DocumentName = data.DocumentName;
                documentVo.DocumentPath = data.DocumentPath;
                documentVo.FileType = data.FileType;
                documentVo.FileName = data.FileName;
                documentVo.RecordStatus = data.RecordStatus;
                documentVo.CreatedBy = data.CreatedBy;
                documentVo.CreatedDate = data.CreatedDate;
                documentVo.ModifiedBy = data.ModifiedBy;
                documentVo.ModifiedDate = data.ModifiedDate;

                documentVo.Data = data.Data;
            }
            return documentVo;
        }
        public static Document MapToEntity(this DocumentVO vo)
        {
            Document document = new Document();
            if (vo != null)
            {
                document.DocumentID = vo.DocumentID;
                document.DocumentType = vo.DocumentType;
                document.DocumentName = vo.DocumentName;
                document.DocumentPath = vo.DocumentPath;
                document.FileType = vo.FileType;
                document.FileName = vo.FileName;
                document.RecordStatus = vo.RecordStatus;
                document.CreatedBy = vo.CreatedBy;
                document.CreatedDate = vo.CreatedDate;
                document.ModifiedBy = vo.ModifiedBy;
                document.ModifiedDate = vo.ModifiedDate;

                document.Data = vo.Data;
            }
            return document;
        }
    }
}
