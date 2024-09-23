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
    public static class AutomatedSlotConfigurationMapToExtension
    {
        public static AutomatedSlotConfigurationVO MapToDTO(this AutomatedSlotConfiguration data)
        {
            AutomatedSlotConfigurationVO automatedslotconfigvo = new AutomatedSlotConfigurationVO();
            if (data != null)
            {
                automatedslotconfigvo.SlotCofiguratinid = data.SlotCofiguratinid;
                automatedslotconfigvo.EffectiveFrm = Convert.ToDateTime(data.EffectiveFrm).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                automatedslotconfigvo.Duration = data.Duration;
                double period = data.OperationalPeriod != null ? Convert.ToDouble(data.OperationalPeriod) : 0;
                DateTime? operationalTime = Convert.ToDateTime(data.EffectiveFrm, CultureInfo.InvariantCulture) == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(data.EffectiveFrm, CultureInfo.InvariantCulture).AddMinutes(period);
                automatedslotconfigvo.OperationalPeriod1 = operationalTime;
                automatedslotconfigvo.OperationalPeriod = data.OperationalPeriod;
                automatedslotconfigvo.NoofSlots = data.NoofSlots;
                automatedslotconfigvo.ExtendableSlots = data.ExtendableSlots;
                automatedslotconfigvo.PortCode = data.PortCode;
                automatedslotconfigvo.RecordStatus = data.RecordStatus;
                automatedslotconfigvo.SlotPriorityConfigurations = data.SlotPriorityConfigurations != null ? data.SlotPriorityConfigurations.MapToDTO() : null;
            }
            return automatedslotconfigvo;
        }
        public static AutomatedSlotConfiguration MapToEntity(this AutomatedSlotConfigurationVO data)
        {
            AutomatedSlotConfiguration automatedslotconfig = new AutomatedSlotConfiguration();
            if (data != null)
            {
                automatedslotconfig.SlotCofiguratinid = data.SlotCofiguratinid;
                automatedslotconfig.EffectiveFrm = DateTime.Parse(Convert.ToDateTime(data.EffectiveFrm, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                automatedslotconfig.Duration = data.Duration;
                automatedslotconfig.OperationalPeriod = Convert.ToString(data.OperationalPeriod1, CultureInfo.InvariantCulture);
                automatedslotconfig.NoofSlots = data.NoofSlots;
                automatedslotconfig.ExtendableSlots = data.ExtendableSlots;
                automatedslotconfig.PortCode = data.PortCode;
                automatedslotconfig.RecordStatus = data.RecordStatus;
                automatedslotconfig.SlotPriorityConfigurations = data.SlotPriorityConfigurations.MapToEntity();
            }
            return automatedslotconfig;
        }
        public static List<AutomatedSlotConfigurationVO> MapToDTO(this IEnumerable<AutomatedSlotConfiguration> AutomatedSlotConfigurations)
        {
            var automatedslotconfigurationvolist = new List<AutomatedSlotConfigurationVO>();
            if (AutomatedSlotConfigurations != null)
            {
                foreach (var item in AutomatedSlotConfigurations)
                {
                    automatedslotconfigurationvolist.Add(item.MapToDTO());
                }
            }
            return automatedslotconfigurationvolist;
        }
        public static List<AutomatedSlotConfiguration> MapToEntity(this IEnumerable<AutomatedSlotConfigurationVO> AutomatedSlotConfigurationvo)
        {
            var automatedslotconfigurationlist = new List<AutomatedSlotConfiguration>();
            if (AutomatedSlotConfigurationvo != null)
            {
                foreach (var item in AutomatedSlotConfigurationvo)
                {
                    automatedslotconfigurationlist.Add(item.MapToEntity());
                }
            }
            return automatedslotconfigurationlist;
        }


      
    }
}
    