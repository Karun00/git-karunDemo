using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PermitRequestDocumentMapExtension
    {
        public static List<PermitRequestDocument> MapToEntity(this IEnumerable<PermitRequestDocumentVO> vos)
        {
            List<PermitRequestDocument> entities = new List<PermitRequestDocument>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<PermitRequestDocumentVO> MapToDTO(this IEnumerable<PermitRequestDocument> entities)
        {
            List<PermitRequestDocumentVO> vos = new List<PermitRequestDocumentVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static PermitRequestDocumentVO MapToDTO(this PermitRequestDocument data)
        {
            PermitRequestDocumentVO Vo = new PermitRequestDocumentVO();
            if (data != null)
            {
           
                Vo.PermitRequestDocumentID = data.PermitRequestDocumentID;
                Vo.PermitRequestID = data.PermitRequestID;
                Vo.DocumentID = data.DocumentID;
                if (data.Document != null)
                {
                    Vo.FileName = data.Document.FileName;           
                    Vo.DocumentName = data.Document.SubCategory.SubCatName;
                }
                Vo.RecordStatus = data.RecordStatus;
                Vo.CreatedBy = data.CreatedBy;
                Vo.CreatedDate = data.CreatedDate;
                Vo.ModifiedBy = data.ModifiedBy;
                Vo.CreatedBy = data.CreatedBy;
                Vo.ModifiedDate = data.ModifiedDate;
                // Vo.Document = data.Document.MapToDTO();
               
            }
            return Vo;
        }
        public static PermitRequestDocument MapToEntity(this PermitRequestDocumentVO VO)
        {
            PermitRequestDocument data = new PermitRequestDocument();
            if (VO != null)
            {
                data.PermitRequestDocumentID = VO.PermitRequestDocumentID;
                data.PermitRequestID = VO.PermitRequestID;
                data.DocumentID = VO.DocumentID;
                data.RecordStatus = VO.RecordStatus;
                data.CreatedBy = VO.CreatedBy;
                data.CreatedDate = VO.CreatedDate;
                data.ModifiedBy = VO.ModifiedBy;
                data.CreatedBy = VO.CreatedBy;
                data.ModifiedDate = VO.ModifiedDate;
                //  data.Document = VO.Document.MapToEntity();
            }
            return data;
        }
    }
}
