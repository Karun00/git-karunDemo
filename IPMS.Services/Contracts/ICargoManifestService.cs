using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ICargoManifestService
    {
        /// <summary>
        /// To Add Cargo Manifest Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        CargoManifestVO AddCargoManifest(CargoManifestVO data);

        /// <summary>
        /// To Modify Cargo Manifest Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        CargoManifestVO ModifyCargoManifest(CargoManifestVO data);

        /// <summary>
        ///  To Get Cargo Manifest Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VCNData> CargoManifestDetails();

        /// <summary>
        ///  To Get Arrival Commodity Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ArrivalCommoditiesList> ArrivalCommodityDetails(string VCN);

    }
}
