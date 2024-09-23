using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class PilotExemptionRequestDocumentMapExtension
    {
        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<PilotExemptionRequestDocument> MapToEntity(this List<PilotExemptionRequestDocumentVO> data)
        {
            List<PilotExemptionRequestDocument> vos = new List<PilotExemptionRequestDocument>();
            foreach (var pilotrequest in data)
            {
                vos.Add(pilotrequest.MapToEntity());
            }
            return vos;
        }

        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<PilotExemptionRequestDocumentVO> MapToDTO(this List<PilotExemptionRequestDocument> data)
        {
            List<PilotExemptionRequestDocumentVO> vos = new List<PilotExemptionRequestDocumentVO>();
            foreach (var pilotrequest in data)
            {
                vos.Add(pilotrequest.MapToDTO());
            }
            return vos;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static PilotExemptionRequestDocumentVO MapToDTO(this PilotExemptionRequestDocument data)
        {
            PilotExemptionRequestDocumentVO vo = new PilotExemptionRequestDocumentVO();
            vo.PilotExemptionRequestDocumentID = data.PilotExemptionRequestDocumentID;
            vo.PilotID = data.PilotID;
            vo.DocumentID = data.DocumentID;
            vo.FileName = data.FileName;
            vo.DocumentName = data.DocumentName;
            vo.RecordStatus = data.RecordStatus;
            return vo;
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static PilotExemptionRequestDocument MapToEntity(this PilotExemptionRequestDocumentVO vo)
        {
            PilotExemptionRequestDocument data = new PilotExemptionRequestDocument();
            data.PilotExemptionRequestDocumentID = vo.PilotExemptionRequestDocumentID;
            data.PilotID = vo.PilotID;
            data.DocumentID = vo.DocumentID;
            data.FileName = vo.FileName;
            data.DocumentName = vo.DocumentName;
            data.RecordStatus = vo.RecordStatus;
            return data;
        }



    }
}
