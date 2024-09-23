using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;


namespace IPMS.Domain.DTOS
{
    public static class BerthPlanningConfigurationsMapExtension
    {
        public static BerthPlanningConfigurationsVO MapToDTO(this BerthPlanningConfigurations data)
        {
            BerthPlanningConfigurationsVO BPCVO = new BerthPlanningConfigurationsVO();
            if (data != null)
            {

                BPCVO.BerthPlanConfigid = data.BerthPlanConfigid;
                BPCVO.Days = data.Days;
                BPCVO.Slot = data.Slot;
                BPCVO.PortCode = data.PortCode;
                BPCVO.RecordStatus = data.RecordStatus;
                BPCVO.CreatedBy = data.CreatedBy;
                BPCVO.CreatedDate = data.CreatedDate;
                BPCVO.ModifiedBy = data.ModifiedBy;
                BPCVO.ModifiedDate = data.ModifiedDate;
            }
            return BPCVO;
        }
        public static BerthPlanningConfigurations MapToEntity(this BerthPlanningConfigurationsVO BPCVO)
        {
            BerthPlanningConfigurations data = new BerthPlanningConfigurations();
            if (BPCVO != null)
            {

                data.BerthPlanConfigid = BPCVO.BerthPlanConfigid;
                data.Days = BPCVO.Days;
                data.Slot = BPCVO.Slot;
                data.PortCode = BPCVO.PortCode;
                data.RecordStatus = BPCVO.RecordStatus;
                data.CreatedBy = BPCVO.CreatedBy;
                data.CreatedDate = BPCVO.CreatedDate;
                data.ModifiedBy = BPCVO.ModifiedBy;
                data.ModifiedDate = BPCVO.ModifiedDate;
            }
            return data;
        }
    }
}
