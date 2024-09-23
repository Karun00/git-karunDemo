using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class IncidentNatureMapExtension
    {

        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="incidentNature"></param>
        /// <returns></returns>
        public static List<IncidentNatureVO> MapToDto(this ICollection<IncidentNature> incidentNature)
        {
            List<IncidentNatureVO> incidentNatureVos = new List<IncidentNatureVO>();
            if (incidentNature != null)
            {
                foreach (var item in incidentNature)
                {
                    incidentNatureVos.Add(item.MapToDto());
                }
            }
            return incidentNatureVos;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="incidentNatureVos"></param>
        /// <returns></returns>
        public static List<IncidentNature> MapToEntity(this List<IncidentNatureVO> incidentNatureVos)
        {
            List<IncidentNature> incidentNature = new List<IncidentNature>();
            if (incidentNatureVos != null)
            {
                foreach (var item in incidentNatureVos)
                {
                    incidentNature.Add(item.MapToEntity());
                }
            }
            return incidentNature;
        }

        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IncidentNatureVO MapToDto(this IncidentNature data)
        {
            IncidentNatureVO incidentNatureVO = new IncidentNatureVO();
            if (data != null)
            {
                incidentNatureVO.CreatedBy = data.CreatedBy;
                incidentNatureVO.CreatedDate = data.CreatedDate;
                incidentNatureVO.ModifiedBy = data.ModifiedBy;
                incidentNatureVO.ModifiedDate = data.ModifiedDate;
                incidentNatureVO.RecordStatus = data.RecordStatus;
                incidentNatureVO.IncidentID = data.IncidentID;
                incidentNatureVO.IncidentNatureID = data.IncidentNatureID;
                incidentNatureVO.IncidentNature1 = data.IncidentNature1;
            }
            return incidentNatureVO;

        }

        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static IncidentNature MapToEntity(this IncidentNatureVO vo)
        {
            IncidentNature incidentNature = new IncidentNature();
            if (vo != null)
            {
                incidentNature.CreatedBy = vo.CreatedBy;
                incidentNature.CreatedDate = vo.CreatedDate;
                incidentNature.ModifiedBy = vo.ModifiedBy;
                incidentNature.ModifiedDate = vo.ModifiedDate;
                incidentNature.RecordStatus = vo.RecordStatus;
                incidentNature.IncidentID = vo.IncidentID;
                incidentNature.IncidentNatureID = vo.IncidentNatureID;
                incidentNature.IncidentNature1 = vo.IncidentNature1;
                incidentNature.SubCategory = vo.SubCategory.MapToEntity();
            }
            return incidentNature;
        }

        /// <summary>
        ///  Data List Transfer from entity to List<string>
        /// </summary>
        /// <param name="incidentNature"></param>
        /// <returns></returns>
        public static List<string> MapToIncedentTypeArray(this ICollection<IncidentNature> incidentNature)
        {
            List<string> IncedentTypeArray = new List<string>();
            if (incidentNature != null)
            {
                foreach (var item in incidentNature)
                {
                    IncedentTypeArray.Add(item.IncidentNature1);
                }
            }
            return IncedentTypeArray;
        }

        /// <summary>
        ///  Data List Transfer from List<string> to entity
        /// </summary>
        /// <param name="IncedentTypeArray"></param>
        /// <param name="incidentId"></param>
        /// <returns></returns>
        public static List<IncidentNature> MapToEntityIncidentNature(this List<string> IncedentTypeArray, int incidentId)
        {
            List<IncidentNature> incidentNature = new List<IncidentNature>();
            if (IncedentTypeArray != null)
            {
                foreach (var incidentType in IncedentTypeArray)
                {
                    IncidentNature incident = new IncidentNature();
                    incident.IncidentID = incidentId;
                    incident.IncidentNature1 = incidentType;

                    incidentNature.Add(incident);
                }
            }
            return incidentNature;
        }
    }
}
