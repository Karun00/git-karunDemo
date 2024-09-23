using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class CraftOutOfCommissionClient : UserClientBase<ICraftOutOfCommissionService>, ICraftOutOfCommissionService
    {
        public List<CraftOutOfCommissionVO> CraftOutOfCommissionDetails()
        {
            return WrapOperationWithException(() => Channel.CraftOutOfCommissionDetails());
        }

        public List<CraftOutOfCommissionVO> CraftInCommissionDetails()
        {
            return WrapOperationWithException(() => Channel.CraftInCommissionDetails());
        }

        public CraftOutOfCommissionVO ModifyCraftOutOfCommission(CraftOutOfCommissionVO entity)
        {
            return WrapOperationWithException(() => Channel.ModifyCraftOutOfCommission(entity));
        }

        public CraftOutOfCommissionVO ModifyCraftInCommission(CraftOutOfCommissionVO craftInCommissionData)
        {
            return WrapOperationWithException(() => Channel.ModifyCraftInCommission(craftInCommissionData));
        }

        public CraftOutOfCommissionVO AddCraftOutOfCommission(CraftOutOfCommissionVO entity)
        {
            return WrapOperationWithException(() => Channel.AddCraftOutOfCommission(entity));
        }

        public List<CraftVO> CraftsDetails()
        {
            return WrapOperationWithException(() => Channel.CraftsDetails());
        }

        public List<CraftVO> CraftsDetailsWithCraftId(int craftId)
        {
            return WrapOperationWithException(() => Channel.CraftsDetailsWithCraftId(craftId));
        }

        public List<SubCategoryVO> ReasonForOutOfCommissionDetails(string reasonCode)
        {
            return WrapOperationWithException(() => Channel.ReasonForOutOfCommissionDetails(reasonCode));
        }

        public List<SubCategoryVO> CommissionStatusDetails(string status)
        {
            return WrapOperationWithException(() => Channel.CommissionStatusDetails(status));
        }
    }
}