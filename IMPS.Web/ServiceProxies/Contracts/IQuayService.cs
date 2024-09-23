using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IQuayService : IDisposable
    {
        /// <summary>
        /// Post Quay Data
        /// </summary>
        /// <param name="quayData"></param>
        /// <returns></returns>
        [OperationContract]
        QuayVO AddQuay(QuayVO quayData);

        /// <summary>
        ///  Modify Quay Data
        /// </summary>
        /// <param name="quayData"></param>
        /// <returns></returns>
        [OperationContract]
        QuayVO ModifyQuay(QuayVO quayData);

        /// <summary>
        ///  To Get Quay Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        QuayVO GetQuayId(long id);

        /// <summary>
        /// To Delete Quay by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        QuayVO DeleteQuay(long id);

        /// <summary>
        /// To Get Quay Details    
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<QuayVO> QuayDetails();

        /// <summary>
        /// To Get Berths based on Quay
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<QuayVO> GetQuaysWithBerths(string portCode);

        /// <summary>
        /// To Get Quays with Berths       
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<QuayVO> GetQuaysWithBerthsMobile();

        /// <summary>
        ///  Post Quay Data Asynchronously
        /// </summary>
        /// <param name="quayData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<QuayVO> AddQuayAsync(QuayVO quaydata);

        /// <summary>
        ///  Modify Quay Data Asynchronously
        /// </summary>
        /// <param name="quayData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<QuayVO> ModifyQuayAsync(QuayVO quaydata);

        /// <summary>
        /// To Get Quay Id Asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<QuayVO> GetQuayIDAsync(long id);

        /// <summary>
        /// To Get Quays with Berths Asynchronously
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<QuayVO>> GetQuaysWithBerthsAsync(string portCode);

        /// <summary>
        ///  To Delete Quay by Id Asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<QuayVO> DeletequayAsync(long id);

        /// <summary>
        /// To Get Quay Details Asynchronously
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<QuayVO>> QuayDetailsAsync();

    }
}
