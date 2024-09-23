using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class IncidentDocumentMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="incidentDocuments"></param>
        /// <returns></returns>
        public static List<IncidentDocumentVO> MapToDto(this ICollection<IncidentDocument> incidentDocuments)
        {
            List<IncidentDocumentVO> incidentDocumentVos = new List<IncidentDocumentVO>();
            if (incidentDocuments != null)
            {
                foreach (var item in incidentDocuments)
                {
                    incidentDocumentVos.Add(item.MapToDto());
                }
            }
            return incidentDocumentVos;
        }

        /// <summary>
        ///  Data List Transfer from DTO to Entity 
        /// </summary>
        /// <param name="incidentDocumentVos"></param>
        /// <returns></returns>
        public static List<IncidentDocument> MapToEntity(this List<IncidentDocumentVO> incidentDocumentVos)
        {
            List<IncidentDocument> incidentDocuments = new List<IncidentDocument>();
            if (incidentDocumentVos != null)
            {
                foreach (var item in incidentDocumentVos)
                {
                    incidentDocuments.Add(item.MapToEntity());
                }
            }
            return incidentDocuments;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IncidentDocumentVO MapToDto(this IncidentDocument data)
        {
            IncidentDocumentVO incidentDocumentVO = new IncidentDocumentVO();
            if (data != null)
            {
                incidentDocumentVO.CreatedBy = data.CreatedBy;
                incidentDocumentVO.CreatedDate = data.CreatedDate;
                incidentDocumentVO.DocumentID = data.DocumentID;
                incidentDocumentVO.DocumentName = data.DocumentName;
                incidentDocumentVO.DocumentType = data.DocumentType;
                incidentDocumentVO.FileName = data.FileName;
                incidentDocumentVO.IncidentDocument1 = data.IncidentDocument1;
                incidentDocumentVO.IncidentID = data.IncidentID;
                incidentDocumentVO.ModifiedBy = data.ModifiedBy;
                incidentDocumentVO.ModifiedDate = data.ModifiedDate;
                incidentDocumentVO.RecordStatus = data.RecordStatus;
            }
            return incidentDocumentVO;

        }

        /// <summary>
        ///  Data Transfer from DTO to Entity 
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static IncidentDocument MapToEntity(this IncidentDocumentVO vo)
        {
            IncidentDocument incidentDocument = new IncidentDocument();
            if (vo != null)
            {
                incidentDocument.CreatedBy = vo.CreatedBy;
                incidentDocument.CreatedDate = vo.CreatedDate;
                incidentDocument.DocumentID = vo.DocumentID;
                incidentDocument.DocumentName = vo.DocumentName;
                incidentDocument.DocumentType = vo.DocumentType;
                incidentDocument.FileName = vo.FileName;
                incidentDocument.IncidentDocument1 = vo.IncidentDocument1;
                incidentDocument.IncidentID = vo.IncidentID;
                incidentDocument.ModifiedBy = vo.ModifiedBy;
                incidentDocument.ModifiedDate = vo.ModifiedDate;
                incidentDocument.RecordStatus = vo.RecordStatus;
            }
            return incidentDocument;
        }


    }
}
