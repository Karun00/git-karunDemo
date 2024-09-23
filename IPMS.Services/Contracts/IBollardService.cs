using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IBollardService
    {   
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
        /// To Add Bollard Data
        /// </summary>
        /// <param name="bollardData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BollardVO AddBollard(BollardVO bollardData);

        /// <summary>
        /// To Modify Bollard Data
        /// </summary>
        /// <param name="bollardData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BollardVO ModifyBollard(BollardVO bollardData);

        /// <summary>
        /// To get Bollards by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        BollardVO GetBollardById(string id);

        /// <summary>
        /// To Delete Bollards by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        BollardVO DelBollardById(long id);

        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        List<QuayVO> GetPortQuays(string id);

        /// <summary>
        /// To Get Berths based on Port and Quay
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<BerthVO> GetQuayBerths(string portCode,string quayCode);
    }
}
