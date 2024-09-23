using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ICraftOutOfCommissionService
    {
        /// <summary>
        /// To Add Craft Out of Commissions Data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        CraftOutOfCommissionVO AddCraftOutOfCommission(CraftOutOfCommissionVO entity);

        /// <summary>
        /// To Modify Craft Out of Commissions Data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
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
        ///  To Get Craft Out of Commissions Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<CraftOutOfCommissionVO> CraftOutOfCommissionDetails();

        /// <summary>
        ///  To Get Craft Back to Commissions Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<CraftOutOfCommissionVO> CraftInCommissionDetails();

        /// <summary>
        ///  To Get Craft Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<CraftVO> CraftsDetails();

        /// <summary>
        ///  To Get Craft Details based on CraftID
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<CraftVO> CraftsDetailsWithCraftId(int craftId);

        /// <summary>
        ///  To Get Reason for Out of Commission Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> ReasonForOutOfCommissionDetails(string reasonCode);

        /// <summary>
        ///  To Get Craft Commission Status Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> CommissionStatusDetails(string status);
    }
}
