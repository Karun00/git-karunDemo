using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class VesselAgentChangeDocumentMapExtension
    {

        public static VesselAgentChangeDocumentVO MapToDto(this VesselAgentChangeDocument data)
        {
            return new VesselAgentChangeDocumentVO
            {
                VesselAgentChangeID = data.VesselAgentChangeID,
                DocumentID = data.DocumentID,
                DocumentName = data.DocumentName,
                RecordStatus = data.RecordStatus,
                FileName = data.FileName,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }
        public static VesselAgentChangeDocument MapToEntity(this VesselAgentChangeDocumentVO data)
        {
            return new VesselAgentChangeDocument
            {
                VesselAgentChangeID = data.VesselAgentChangeID,
                DocumentID = data.DocumentID,
                DocumentName = data.DocumentName,
                RecordStatus = data.RecordStatus,
                FileName = data.FileName,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
        }

        public static List<VesselAgentChangeDocumentVO> MapToDto(this IEnumerable<VesselAgentChangeDocument> documents)
        {
            var documentVoList = new List<VesselAgentChangeDocumentVO>();
            foreach (var item in documents)
            {
                documentVoList.Add(item.MapToDto());
            }
            return documentVoList;
        }

        public static List<VesselAgentChangeDocument> MapToEntity(this IEnumerable<VesselAgentChangeDocumentVO> documents)
        {

            var documentList= new List<VesselAgentChangeDocument>();
            foreach (var item in documents)
            {
                documentList.Add(item.MapToEntity());
            }
            return documentList;
        }
    }
}
