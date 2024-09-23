using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IBerthPlanningConfigurationsService
    {
        /// <summary>
        /// To Get Berth Planning Configurations Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<BerthPlanningConfigurationsVO> BerthPlanningConfigurationsDetails();


        /// <summary>
        ///  To Add Berth Planning Configurations Data
        /// </summary>
        /// <param name="berthplanconfigdata"></param>
        /// <returns></returns>
        [OperationContract]
        BerthPlanningConfigurationsVO AddBerthPlanConfig(BerthPlanningConfigurationsVO berthplanconfigdata);

        /// <summary>
        /// To Modify Berth Planning Configurations Data
        /// 
        /// </summary>
        /// <param name="berthplanconfigdata"></param>
        /// <returns></returns>
        [OperationContract]
        BerthPlanningConfigurationsVO ModifyBerthPlanConfig(BerthPlanningConfigurationsVO berthplanconfigdata);


    }
}