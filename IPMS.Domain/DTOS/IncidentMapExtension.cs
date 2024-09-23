using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class IncidentMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="incidents"></param>
        /// <returns></returns>
        public static List<IncidentVO> MapToDto(this List<Incident> incidents)
        {
            List<IncidentVO> incidentVos = new List<IncidentVO>();
            if (incidents != null)
            {
                foreach (var item in incidents)
                {
                    incidentVos.Add(item.MapToDto());
                }
            }
            return incidentVos;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="incidentVos"></param>
        /// <returns></returns>
        public static List<Incident> MapToEntity(this List<IncidentVO> incidentVos)
        {
            List<Incident> incidents = new List<Incident>();
            if (incidentVos != null)
            {
                foreach (var item in incidentVos)
                {
                    incidents.Add(item.MapToEntity());
                }
            }
            return incidents;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IncidentVO MapToDto(this Incident data)
        {
            IncidentVO incidentVO = new IncidentVO();
            if (data != null)
            {
                incidentVO.IncidentID = data.IncidentID;
                incidentVO.IncidentLocation = data.IncidentLocation;
                incidentVO.IncidentDescription = data.IncidentDescription;
                incidentVO.PortCode = data.PortCode;
                incidentVO.RecordStatus = data.RecordStatus;
                incidentVO.CreatedBy = data.CreatedBy;
                incidentVO.CreatedDate = data.CreatedDate;
                incidentVO.ModifiedBy = data.ModifiedBy;
                incidentVO.ModifiedDate = data.ModifiedDate;
                incidentVO.IncidentDocuments = data.IncidentDocuments != null ? data.IncidentDocuments.MapToDto() : null;
                incidentVO.IncidentNatures = data.IncidentNatures.MapToDto();
                incidentVO.IncedentTypeArray = data.IncidentNatures.MapToIncedentTypeArray();
                if (data.IncidentNatures != null)
                {
                    foreach (var incidentNature in data.IncidentNatures)
                    {
                        if (incidentNature.SubCategory != null)
                        {
                            if (!string.IsNullOrEmpty(incidentNature.SubCategory.SubCatName))
                            {
                                if (string.IsNullOrWhiteSpace(incidentVO.IncidentNatureDetails))
                                {
                                    incidentVO.IncidentNatureDetails = incidentNature.SubCategory.SubCatName;
                                }
                                else
                                {
                                    incidentVO.IncidentNatureDetails = incidentVO.IncidentNatureDetails + ", " + incidentNature.SubCategory.SubCatName;
                                }
                            }
                        }
                    }
                }
            }

            return incidentVO;

        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Incident MapToEntity(this IncidentVO vo)
        {
            Incident incident = new Incident();
            if (vo != null)
            {
                incident.IncidentID = vo.IncidentID;
                incident.IncidentLocation = vo.IncidentLocation;
                incident.IncidentDescription = vo.IncidentDescription;
                incident.ModifiedBy = vo.ModifiedBy;
                incident.ModifiedDate = vo.ModifiedDate;
                incident.CreatedDate = vo.CreatedDate;
                incident.CreatedBy = vo.CreatedBy;
                incident.IncidentDocuments = vo.IncidentDocuments.MapToEntity();
                incident.IncidentNatures = vo.IncedentTypeArray.MapToEntityIncidentNature(vo.IncidentID);
                incident.PortCode = vo.PortCode;
                incident.RecordStatus = vo.RecordStatus;
            }
            return incident;
        }


    }
}
