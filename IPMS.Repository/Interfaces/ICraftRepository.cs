using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ICraftRepository
    {
        List<CraftVO> GetCraftList(string portCode);
        CraftVO AddCraft(CraftVO craftData, string portCode, int userId);
        CraftVO ModifyCraft(CraftVO craftData, string portCode, int userId);
        CraftReferenceVO GetCraftReferences();
    }
}
