using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ICraftReminderConfigRepository
    {
        List<CraftReminderConfig> GetCraftReminderConfigDetails(int craftId);
        CraftReminderConfig GetCraftReminderConfigByConfigId(int craftReminderConfigId);
        CraftReferenceVO GetCraftReminderReferences();
        CraftVO AddCraftReminderConfig(CraftReminderConfigVO data, string portCode, int userId);
        CraftReminderConfigVO ModifyCraftReminderConfig(CraftReminderConfigVO data, int userId);

        List<CraftVO> GetCraftReminderConfigById(int craftReminderConfigId, string portCode);
    }
}
