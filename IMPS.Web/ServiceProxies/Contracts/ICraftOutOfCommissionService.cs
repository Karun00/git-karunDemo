using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ICraftOutOfCommissionService
    {
        /// <summary>
        /// To Get Craft Out of Commissions Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CraftOutOfCommissionVO> CraftOutOfCommissionDetails();

        /// <summary>
        /// To Get Craft In Commissions Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CraftOutOfCommissionVO> CraftInCommissionDetails();
        
        /// <summary>
        ///  To Add Craft Out of Commissions Data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        CraftOutOfCommissionVO AddCraftOutOfCommission(CraftOutOfCommissionVO entity);

        /// <summary>
        /// To Modify Craft Out of Commissions Data
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        CraftOutOfCommissionVO ModifyCraftOutOfCommission(CraftOutOfCommissionVO entity);

        /// <summary>
        /// To Modify Craft Back to Commissions Data
        /// 
        /// </summary>
        /// <param name="craftInCommissionData"></param>
        /// <returns></returns>
        [OperationContract]
        CraftOutOfCommissionVO ModifyCraftInCommission(CraftOutOfCommissionVO craftInCommissionData);

        /// <summary>
        /// To Get Craft Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CraftVO> CraftsDetails();

        /// <summary>
        /// To Get Craft Details based on CraftID
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CraftVO> CraftsDetailsWithCraftId(int craftId);

        /// <summary>
        /// To Get Reason for Out of Commissions Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> ReasonForOutOfCommissionDetails(string reasonCode);

        /// <summary>
        /// To Get Craft Commission Status Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> CommissionStatusDetails(string status);

    }
}