using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IBerthService
    {
        /// <summary>
        /// To Get Berth Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthVO> GetBerthsDetails();

        /// <summary>
        /// To Get Berths In Quay
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthVO> GetBerthsInQuay(string portCode, string quayCode);

        /// <summary>
        /// To Add Berth Data
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BerthVO AddBerth(BerthVO berthData);

        /// <summary>
        /// To Modify Berth Data
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BerthVO ModifyBerth(BerthVO berthData);

        /// <summary>
        /// To Get Berth Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetBerthType();

        /// <summary>
        /// To Delete Berth By Id
        /// </summary>
        /// <param name="berthdata"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        BerthVO DelBerthById(BerthVO berthData);

        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="Portid"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<QuayVO> GetPortQuayDetails();

        /// <summary>
        /// To Get Cargo Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetCargoType();

        /// <summary>
        /// To Get Vessel Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryCodeNameVO> GetVesselType();

        /// <summary>
        /// To Get Reason For Visit Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryCodeNameVO> GetReasonType();


        /////////////mahesh/////////
        /// <summary>
        /// To Get Berthlist with correspond Bollards.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthVO> GetBerthsWithBollards();

        /// <summary>
        /// Author   :  Sandeep Appana
        /// Date     :  28-8-2014
        /// Purpose  :  To Get Berthlist based on Ports.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BerthVO> GetBerthsWithPortCode();
    }
}
