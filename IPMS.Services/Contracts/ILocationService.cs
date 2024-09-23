using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
   public interface ILocationService
    {
        /// <summary>
        /// To Get Location Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<LocationVO> LocationDetails();

        /// <summary>
        /// To Add Location Data
        /// </summary>
        /// <param name="locationData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        LocationVO AddLocation(LocationVO locationData);
        /// <summary>
        /// To Modify Location Data
        /// </summary>
        /// <param name="locationData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        LocationVO ModifyLocation(LocationVO locationData);
    }
      
}
