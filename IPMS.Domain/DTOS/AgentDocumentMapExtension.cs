using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class AgentDocumentMapExtension
    {
        public static List<AgentDocumentsVO> MapToListDTO(this IEnumerable<AgentDocument> agentDocumentsList)
        {
            List<AgentDocumentsVO> agentDocumentsVoList = new List<AgentDocumentsVO>();
            if (agentDocumentsList != null)
            {
                foreach (var agentDocument in agentDocumentsList)
                {
                    AgentDocumentsVO agentDocumentVo = new AgentDocumentsVO();
                    agentDocumentVo.AgentID = agentDocument.AgentID;
                    agentDocumentVo.DocumentID = agentDocument.DocumentID;
                    agentDocumentVo.RecordStatus = agentDocument.RecordStatus;
                    agentDocumentVo.CreatedBy = agentDocument.CreatedBy;
                    agentDocumentVo.CreatedDate = agentDocument.CreatedDate;
                    agentDocumentVo.ModifiedBy = agentDocument.ModifiedBy;
                    agentDocumentVo.ModifiedDate = agentDocument.ModifiedDate;
                    agentDocumentVo.Document = agentDocument.Document;
                    agentDocumentsVoList.Add(agentDocumentVo);
                }
            }
            return agentDocumentsVoList;
        }
        public static List<AgentDocument> MapToListEntity(this IEnumerable<AgentDocumentsVO> agentDocumentsVoList)
        {
            List<AgentDocument> agentDocumentsList = new List<AgentDocument>();
            if (agentDocumentsVoList != null)
            {
                foreach (var agentDocumentVo in agentDocumentsVoList)
                {
                    AgentDocument agentDocument = new AgentDocument();
                    agentDocument.AgentID = agentDocumentVo.AgentID;
                    agentDocument.DocumentID = agentDocumentVo.DocumentID;
                    agentDocument.RecordStatus = agentDocumentVo.RecordStatus;
                    agentDocument.CreatedBy = agentDocumentVo.CreatedBy;
                    agentDocument.CreatedDate = agentDocumentVo.CreatedDate;
                    agentDocument.ModifiedBy = agentDocumentVo.ModifiedBy;
                    agentDocument.ModifiedDate = agentDocumentVo.ModifiedDate;
                    agentDocument.Document = agentDocumentVo.Document;
                    agentDocumentsList.Add(agentDocument);
                }
            }
            return agentDocumentsList;
        }
        public static AgentDocumentsVO MapToDTO(this AgentDocument data)
        {
            AgentDocumentsVO agentDocumentVo = new AgentDocumentsVO();
            if (data != null)
            {
                agentDocumentVo.AgentID = data.AgentID;
                agentDocumentVo.DocumentID = data.DocumentID;
                agentDocumentVo.RecordStatus = data.RecordStatus;
                agentDocumentVo.CreatedBy = data.CreatedBy;
                agentDocumentVo.CreatedDate = data.CreatedDate;
                agentDocumentVo.ModifiedBy = data.ModifiedBy;
                agentDocumentVo.ModifiedDate = data.ModifiedDate;
                agentDocumentVo.Document = data.Document;
            }
            return agentDocumentVo;
        }
        public static AgentDocument MapToEntity(this AgentDocumentsVO vo)
        {
            AgentDocument agentDocument = new AgentDocument();
            if (vo != null)
            {
                agentDocument.AgentID = vo.AgentID;
                agentDocument.DocumentID = vo.DocumentID;
                agentDocument.RecordStatus = vo.RecordStatus;
                agentDocument.CreatedBy = vo.CreatedBy;
                agentDocument.CreatedDate = vo.CreatedDate;
                agentDocument.ModifiedBy = vo.ModifiedBy;
                agentDocument.ModifiedDate = vo.ModifiedDate;
                agentDocument.Document = vo.Document;
            }
            return agentDocument;
        }
    }
}
