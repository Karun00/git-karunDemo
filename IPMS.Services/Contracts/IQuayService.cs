using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IQuayService
    {       
        /// <summary>
        /// Post Quay Data
        /// </summary>
        /// <param name="quayData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        QuayVO AddQuay(QuayVO quayData);

        /// <summary>
        ///  Modify Quay Data
        /// </summary>
        /// <param name="quayData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        QuayVO ModifyQuay(QuayVO quayData);

        /// <summary>
        ///  To Get Quay Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        QuayVO GetQuayId(string id);

        /// <summary>
        ///  To Delete Quay by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        QuayVO DeleteQuay(long id);

        /// <summary>
        /// To Get Quay Details    
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<QuayVO> QuayDetails();

        /// <summary>
        /// To Get Berths based on Quay
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<QuayVO> GetQuaysWithBerths(string portCode);

        /// <summary>
        /// To Get Quays with Berths
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<QuayVO> GetQuaysWithBerthsMobile();
    }
}
