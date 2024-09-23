using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ICraftOutOfCommissionRepository
    {
        //CraftOutOfCommissionVO AddCraftOutOfComm(CraftOutOfCommissionVO entity, int _UserId);

        //CraftOutOfCommissionVO ModifyCraftOutOfComm(CraftOutOfCommissionVO entity, int _UserId);

        //CraftOutOfCommissionVO ModifyCraftInComm(CraftOutOfCommissionVO craftincommdata, int _UserId);

        List<CraftOutOfCommissionVO> CraftOutOfCommissionDetails(string portCode);

        List<CraftOutOfCommissionVO> CraftInCommissionDetails(string portCode);

        List<CraftVO> CraftsDetailsWithCraftId(int craftId, string portCode);

        List<CraftVO> CraftsDetails(string portCode);

        List<SubCategoryVO> ReasonForOutOfCommissionDetails(string reasonCode);

        List<SubCategoryVO> CommissionStatusDetails(string status);

        Entity GetEntities(string entityCode);

        CompanyVO GetUserDetails(int userId);

        CraftOutOfCommissionVO GetCraftOutCommissionDetailsById(string craftOutOfCommissionId);
    }
}
