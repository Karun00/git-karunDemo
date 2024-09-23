using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PermitRequestVerifyedDocumentMapExtension
    {
        public static List<PermitRequestVerifyedDocument> MapToEntity(this IEnumerable<PermitRequestVerifyedDocumentVO> vos)
        {
            List<PermitRequestVerifyedDocument> entities = new List<PermitRequestVerifyedDocument>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<PermitRequestVerifyedDocumentVO> MapToDTO(this IEnumerable<PermitRequestVerifyedDocument> entities)
        {
            List<PermitRequestVerifyedDocumentVO> vos = new List<PermitRequestVerifyedDocumentVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static PermitRequestVerifyedDocumentVO MapToDTO(this PermitRequestVerifyedDocument data)
        {
            PermitRequestVerifyedDocumentVO Vo = new PermitRequestVerifyedDocumentVO();
            if (data != null)
            {

                Vo.PermitRequestverifyedID = data.PermitRequestverifyedID;
                Vo.PermitRequestverifyedDocumentID = data.PermitRequestverifyedDocumentID;
                Vo.DocumentID = data.DocumentID;    
                Vo.FileName = data.FileName;               
                Vo.DocumentName = data.DocumentName;
                Vo.RecordStatus = data.RecordStatus;
                Vo.CreatedBy = data.CreatedBy;
                Vo.CreatedDate = data.CreatedDate;
                Vo.ModifiedBy = data.ModifiedBy;    
                Vo.ModifiedDate = data.ModifiedDate;
            }
            return Vo;
        }
        public static PermitRequestVerifyedDocument MapToEntity(this PermitRequestVerifyedDocumentVO VO)
        {
            PermitRequestVerifyedDocument data = new PermitRequestVerifyedDocument();
            if (VO != null)
            {
                data.PermitRequestverifyedID = VO.PermitRequestverifyedID;
                data.PermitRequestverifyedDocumentID = VO.PermitRequestverifyedDocumentID;
                data.DocumentID = VO.DocumentID;
                data.FileName = VO.FileName;
                data.DocumentName = VO.DocumentName;
                data.RecordStatus = VO.RecordStatus;
                data.CreatedBy = VO.CreatedBy;
                data.CreatedDate = VO.CreatedDate;
                data.ModifiedBy = VO.ModifiedBy;
                data.ModifiedDate = VO.ModifiedDate;
            }
            return data;
        }
    }
}
