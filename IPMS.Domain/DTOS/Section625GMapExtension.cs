using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625GMapExtension
    {
        public static List<Section625GVO> MapToDTO(this IEnumerable<Section625G> entities)
        {
            List<Section625GVO> Vos = new List<Section625GVO>();
            foreach (var item in entities)
            {
                Vos.Add(item.MapToDTO());
            }
            return Vos;
        }

        public static List<Section625G> MapToEntity(this IEnumerable<Section625GVO> Vos)
        {
            List<Section625G> entities = new List<Section625G>();
            foreach (var item in Vos)
            {
                entities.Add(item.MapToEntity());
            }

            return entities;
        }
        public static Section625GVO MapToDTO(this Section625G data)
        {
            Section625GVO VO = new Section625GVO();
            VO.Section625GID = data.Section625GID;
            VO.Section625ABCDID = data.Section625ABCDID;
            VO.Hour24Report625ID = data.Hour24Report625ID;
            VO.IncidentDateTime = data.IncidentDateTime;
            if (data.TimeReported != null)
            {
                VO.TimeReported = Convert.ToDateTime(data.TimeReported, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture);
            }
            VO.WIWitnessName1 = data.WIWitnessName1;
            VO.WIWitnessName2 = data.WIWitnessName2;
            VO.WIContactNo1 = data.WIContactNo1;
            VO.WIContactNo2 = data.WIContactNo2;
            VO.IncidentDescription = data.IncidentDescription;
            VO.IncidentExtent = data.IncidentExtent;
            VO.QuantityVolumeMaterial = data.QuantityVolumeMaterial;
            VO.EstimateDistanceNearestWaterway = data.EstimateDistanceNearestWaterway;
            VO.ActivityTypeIncident = data.ActivityTypeIncident;
            VO.IncidentIdentified = data.IncidentIdentified;
            VO.NameOfComplainant = data.NameOfComplainant;
            VO.ContactNoOfComplainant = data.ContactNoOfComplainant;
            if (data.LIMinorEnvironmentalIncident != null)
            {
                if (data.LIMinorEnvironmentalIncident == "Y")
                {
                    VO.LIMinorEnvironmentalIncident = "True";
                }
            }
            if (data.LISignificantEnvironmentalIncident != null)
            {
                if (data.LISignificantEnvironmentalIncident == "Y")
                {
                    VO.LISignificantEnvironmentalIncident = "True";
                }
            }
            if (data.LIMajorEnvironmentalIncident != null)
            {
                if (data.LIMajorEnvironmentalIncident == "Y")
                {
                    VO.LIMajorEnvironmentalIncident = "True";
                }
            }    
            VO.ImmediateReleventActionsTaken = data.ImmediateReleventActionsTaken;
            VO.EnvironmentalImpactDescription = data.EnvironmentalImpactDescription;
            VO.LikelyUnderlyingCauses = data.LikelyUnderlyingCauses;
            VO.ContributingFactorsCourses = data.ContributingFactorsCourses;
            VO.RecordStatus = data.RecordStatus;
            VO.CreatedBy = data.CreatedBy;
            VO.CreatedDate = data.CreatedDate;
            VO.ModifiedBy = data.ModifiedBy;
            VO.ModifiedDate = data.ModifiedDate;
            return VO;
        }

        public static Section625G MapToEntity(this Section625GVO VO)
        {
            Section625G data = new Section625G();
            data.Section625GID = VO.Section625GID;
            data.Section625ABCDID = VO.Section625ABCDID;
            data.Hour24Report625ID = VO.Hour24Report625ID;
            data.IncidentDateTime = VO.IncidentDateTime;
            if (VO.TimeReported != "")
            {
                data.TimeReported = DateTime.Parse(Convert.ToDateTime(VO.TimeReported, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            }
            data.WIWitnessName1 = VO.WIWitnessName1;
            data.WIContactNo1 = VO.WIContactNo1;
            data.WIWitnessName2 = VO.WIWitnessName2;
            data.WIContactNo2 = VO.WIContactNo2;
            data.IncidentDescription = VO.IncidentDescription;
            data.IncidentExtent = VO.IncidentExtent;
            data.QuantityVolumeMaterial = VO.QuantityVolumeMaterial;
            data.EstimateDistanceNearestWaterway = VO.EstimateDistanceNearestWaterway;
            data.ActivityTypeIncident = VO.ActivityTypeIncident;
            data.IncidentIdentified = VO.IncidentIdentified;
            data.NameOfComplainant = VO.NameOfComplainant;
            data.ContactNoOfComplainant = VO.ContactNoOfComplainant;
            if (VO.LIMinorEnvironmentalIncident != null)
            {
                if (VO.LIMinorEnvironmentalIncident=="True")
                {
                    data.LIMinorEnvironmentalIncident = "Y";
                }
            }
            if (VO.LISignificantEnvironmentalIncident != null)
            {
                if (VO.LISignificantEnvironmentalIncident == "True")
                {
                    data.LISignificantEnvironmentalIncident = "Y";
                }
            }
            if (VO.LIMajorEnvironmentalIncident != null)
            {
                if (VO.LIMajorEnvironmentalIncident == "True")
                {
                    data.LIMajorEnvironmentalIncident = "Y";
                }
            }
            //data.LIMinorEnvironmentalIncident = VO.LIMinorEnvironmentalIncident;
            //data.LISignificantEnvironmentalIncident = VO.LISignificantEnvironmentalIncident;
            //data.LIMajorEnvironmentalIncident = VO.LIMajorEnvironmentalIncident;
            data.ImmediateReleventActionsTaken = VO.ImmediateReleventActionsTaken;
            data.EnvironmentalImpactDescription = VO.EnvironmentalImpactDescription;
            data.LikelyUnderlyingCauses = VO.LikelyUnderlyingCauses;
            data.ContributingFactorsCourses = VO.ContributingFactorsCourses;
            data.RecordStatus = VO.RecordStatus;
            data.CreatedBy = VO.CreatedBy;
            data.CreatedDate = VO.CreatedDate;
            data.ModifiedBy = VO.ModifiedBy;
            data.ModifiedDate = VO.ModifiedDate;
            return data;
        }

    }
}
