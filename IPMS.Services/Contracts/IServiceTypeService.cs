using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IServiceTypeService
    {
        /// <summary>
        /// To Get ServiceType Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceTypeVO> ServiceTypeDetails();

        /// <summary>
        /// To Add ServiceType Data
        /// </summary>
        /// <param name="serviceTypeData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ServiceTypeVO AddServiceType(ServiceTypeVO serviceTypeData);

        /// <summary>
        /// To Modify ServiceType Data
        /// </summary>
        /// <param name="serviceTypeData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ServiceTypeVO ModifyServiceType(ServiceTypeVO serviceTypeData);
    }
}
