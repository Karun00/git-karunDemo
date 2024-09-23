using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ICargoManifestService
    {
        /// <summary>
        /// To Get Cargo Manifest Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VCNData> CargoManifestDetails();

        /// <summary>
        /// To Get Arrival Commodity Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ArrivalCommoditiesList> ArrivalCommodityDetails(string VCN);

        /// <summary>
        ///  To Add Cargo Manifest Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        CargoManifestVO AddCargoManifest(CargoManifestVO data);

        /// <summary>
        ///  To Add Cargo Manifest Data Asynchronously
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<CargoManifestVO> AddCargoManifAsync(CargoManifestVO data);

        /// <summary>
        /// To Modify Cargo Manifest Data
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        CargoManifestVO ModifyCargoManifest(CargoManifestVO data);

        /// <summary>        
        /// To Modify Cargo Manifest Data Asynchronously      
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<CargoManifestVO> ModifyCargoManifestAsync(CargoManifestVO data);
       
    }
}