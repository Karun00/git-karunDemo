using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ILocationService : IDisposable
    {
        /// <summary>
        /// To Get Location Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<LocationVO> LocationDetails();

        /// <summary>
        /// To Add Location Data
        /// </summary>
        /// <param name="locationData"></param>
        /// <returns></returns>
        [OperationContract]
        LocationVO AddLocation(LocationVO locationData);
        /// <summary>
        /// To Modify Location Data
        /// </summary>
        /// <param name="locationData"></param>
        /// <returns></returns>
        [OperationContract]
        LocationVO ModifyLocation(LocationVO locationData);
    }
}
