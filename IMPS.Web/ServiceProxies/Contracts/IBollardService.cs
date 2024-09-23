using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IBollardService : IDisposable
    {
        /// <summary>
        ///  To Add Bollard Data
        /// </summary>
        /// <param name="bollardData"></param>
        /// <returns></returns>
        [OperationContract]
        BollardVO AddBollard(BollardVO bollardData);

        /// <summary>
        /// To Modify Bollard Data
        /// </summary>
        /// <param name="bollardData"></param>
        /// <returns></returns>
        [OperationContract]
        BollardVO ModifyBollard(BollardVO bollardData);
        
        /// <summary>
        /// To get Bollards by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        BollardVO GetBollardID(long id);

        /// <summary>
        /// To Delete Bollards by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        BollardVO DelBollard(long id);

        /// <summary>
        /// To Get Bollards
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<BollardVO> GetBollards();

        /// <summary>
        /// To Get Bollard Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<BollardVO> GetBollardDetails();

        /// <summary>
        /// To get Bollards based on port, quay and berth        
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <param name="berthCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<BollardVO> GetBollardsInBerths(string portCode, string quayCode, string berthCode);

        /// <summary>
        ///  To Add Bollard Data Asynchronously
        /// </summary>
        /// <param name="bollardData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<BollardVO> AddBollardAsync(BollardVO bollarddata);
        
        /// <summary>
        /// To Modify Bollard Data Asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<BollardVO> GetBollardIDAsync(long id);

        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        List<QuayVO> GetPortQuays(string id);

        /// <summary>
        ///  To Get Berths based on Port and Quay
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<BerthVO> GetQuayBerths(string portCode,string quayCode);

        /// <summary>
        ///  To Delete Bollards by id Asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<BollardVO> DelBollardAsync(long id);

        /// <summary>
        /// To Get Bollards Asynchronously
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<BollardVO>> GetBollardsAsync();


    }
}
