using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IServiceTypeService : IDisposable
    {
        /// <summary>
        /// To Get ServiceType Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ServiceTypeVO> ServiceTypeDetails();

        /// <summary>
        /// To Add ServiceType Data
        /// </summary>
        /// <param name="serviceTypeData"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceTypeVO AddServiceType(ServiceTypeVO serviceTypeData);

        /// <summary>
        /// To Modify ServiceType Data
        /// </summary>
        /// <param name="serviceTypeData"></param>
        /// <returns></returns>
        [OperationContract]
        ServiceTypeVO ModifyServiceType(ServiceTypeVO serviceTypeData);
    }
}
