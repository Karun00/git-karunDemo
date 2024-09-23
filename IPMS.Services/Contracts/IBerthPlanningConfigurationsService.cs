using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IBerthPlanningConfigurationsService
    {
        /// <summary>
        /// To Add Berth Planning Configurations Data
        /// </summary>
        /// <param name="berthplanconfigdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BerthPlanningConfigurationsVO AddBerthPlanConfig(BerthPlanningConfigurationsVO berthplanconfigdata);

        /// <summary>
        /// To Modify Berth Planning Configurations Data
        /// </summary>
        /// <param name="berthplanconfigdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BerthPlanningConfigurationsVO ModifyBerthPlanConfig(BerthPlanningConfigurationsVO berthplanconfigdata);

        /// <summary>
        ///  To Get Berth Planning Configurations Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthPlanningConfigurationsVO> BerthPlanningConfigurationsDetails();
    }
}
