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
    public static class VesselETAChangeMapExtension
    {
        public static VesselETAChangeVO MapToDTO(this VesselETAChange data)
        {
            VesselETAChangeVO obj = new VesselETAChangeVO();
            return obj;
        }

        public static VesselETAChange MapToEntity(this VesselETAChangeVO vodata)
        {
            VesselETAChange entity = new VesselETAChange();
            if (vodata != null)
            {
                entity.VCN = vodata.VCN;
                entity.VoyageIn = vodata.VoyageIn;
                entity.VoyageOut = vodata.VoyageOut;
                entity.ETA = Convert.ToDateTime(vodata.NewETA, CultureInfo.InvariantCulture);
                entity.ETD = Convert.ToDateTime(vodata.NewETD, CultureInfo.InvariantCulture);
                entity.Remarks = vodata.Remarks;
                entity.RecordStatus = vodata.RecordStatus;
                entity.CreatedBy = vodata.CreatedBy;
                entity.CreatedDate = vodata.CreatedDate;
                entity.ModifiedBy = vodata.ModifiedBy;
                entity.ModifiedDate = vodata.ModifiedDate;
                entity.OldETA = Convert.ToDateTime(vodata.OldETA, CultureInfo.InvariantCulture);
                entity.OldETD = Convert.ToDateTime(vodata.OldETD, CultureInfo.InvariantCulture);
                if (vodata.PlanDateTimeOfBerth != null)
                entity.PlanDateTimeOfBerth = Convert.ToDateTime(vodata.PlanDateTimeOfBerth, CultureInfo.InvariantCulture);
                if (vodata.PlanDateTimeToStartCargo != null)
                entity.PlanDateTimeToStartCargo = Convert.ToDateTime(vodata.PlanDateTimeToStartCargo, CultureInfo.InvariantCulture);
                if (vodata.PlanDateTimeToCompleteCargo != null)
                entity.PlanDateTimeToCompleteCargo = Convert.ToDateTime(vodata.PlanDateTimeToCompleteCargo, CultureInfo.InvariantCulture);
                if (vodata.PlanDateTimeToVacateBerth != null)
                entity.PlanDateTimeToVacateBerth = Convert.ToDateTime(vodata.PlanDateTimeToVacateBerth, CultureInfo.InvariantCulture);
                if (vodata.OldPlanDateTimeOfBerth != null)
                entity.OldPlanDateTimeOfBerth = Convert.ToDateTime(vodata.OldPlanDateTimeOfBerth, CultureInfo.InvariantCulture);
                if (vodata.OldPlanDateTimeToStartCargo != null)
                entity.OldPlanDateTimeToStartCargo = Convert.ToDateTime(vodata.OldPlanDateTimeToStartCargo, CultureInfo.InvariantCulture);
                if (vodata.OldPlanDateTimeToCompleteCargo != null)
                entity.OldPlanDateTimeToCompleteCargo = Convert.ToDateTime(vodata.OldPlanDateTimeToCompleteCargo, CultureInfo.InvariantCulture);
                if (vodata.OldPlanDateTimeToVacateBerth != null)
                entity.OldPlanDateTimeToVacateBerth = Convert.ToDateTime(vodata.OldPlanDateTimeToVacateBerth, CultureInfo.InvariantCulture);
            }
            return entity;
        }
    }
}
