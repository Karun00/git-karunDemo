using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class PilotExemptionRequestMapExtension
    {

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ICollection<PilotExemptionRequest> MapToEntity(this List<PilotExemptionRequestVO> data)
        {
            ICollection<PilotExemptionRequest> vos = new List<PilotExemptionRequest>();
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
        public static List<PilotExemptionRequestVO> MapToDTO(this List<PilotExemptionRequest> data)
        {
            List<PilotExemptionRequestVO> vos = new List<PilotExemptionRequestVO>();
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
        public static PilotExemptionRequestVO MapToDTO(this PilotExemptionRequest data)
        {
            PilotExemptionRequestVO vo = new PilotExemptionRequestVO();
            vo.PilotID = data.PilotID;
            vo.PilotExemptionRequestID = data.PilotExemptionRequestID;
            vo.MovementTypeCode = data.MovementTypeCode;
            vo.MovementDate = data.MovementDate.ToString();
            vo.PilotRoleCode = data.PilotRoleCode;
            vo.VesselID = data.VesselID;
            if (data.Vessel != null)
            {
                vo.VesselName = data.Vessel.VesselName;
            }
            vo.Remarks = data.Remarks;
            vo.RecordStatus = data.RecordStatus;
            return vo;
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static PilotExemptionRequest MapToEntity(this PilotExemptionRequestVO vo)
        {
            PilotExemptionRequest data = new PilotExemptionRequest();
            data.PilotID = vo.PilotID;
            data.PilotExemptionRequestID = vo.PilotExemptionRequestID;
            data.MovementTypeCode = vo.MovementTypeCode;
            data.MovementDate = Convert.ToDateTime(vo.MovementDate, CultureInfo.InvariantCulture);
            data.PilotRoleCode = vo.PilotRoleCode;
            data.VesselID = vo.VesselID;
            data.Remarks = vo.Remarks;
            data.RecordStatus = vo.RecordStatus;
            return data;
        }

    }
}
