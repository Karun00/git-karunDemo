using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IBerthService : IDisposable
    {
        /// <summary>
        /// To Get Berth Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<BerthVO> GetBerthsDetails();

        /// <summary>
        /// To Get Berths In Quay
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<BerthVO> GetBerthsInQuay(string portCode,string quayCode);

        /// <summary>
        /// To Add Berth Data
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        [OperationContract]
        BerthVO AddBerth(BerthVO berthData);

        /// <summary>
        /// To Modify Berth Data
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        [OperationContract]
        BerthVO ModifyBerth(BerthVO berthData);

        /// <summary>
        /// To Get Berth Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategory> GetBerthType();

        /// <summary>
        ///  To Delete Berth By Id
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        [OperationContract]
        BerthVO DelBerthById(BerthVO berthData);

        /// <summary>
        /// To Get Quays based on Port
        /// </summary>
        /// <param name="Portid"></param>
        /// <returns></returns>
        [OperationContract]
        List<QuayVO> GetPortQuayDetails();

        /// <summary>
        /// To Get Berth Details Asynchronously
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<BerthVO>> GetBerthsDetailsAsync();

        /// <summary>
        /// To Add Berth Data Asynchronously
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<BerthVO> AddBerthAsync(BerthVO berthdata);

        /// <summary>
        /// To Modify Berth Data Asynchronously
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<BerthVO> ModifyBerthAsync(BerthVO berthdata);

        /// <summary>
        /// To Get Berth Type Asynchronously
        /// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //Task<List<SubCategory>> GetBerthTypeAsync();

        /// <summary>
        /// To Delete Berth By Id Asynchronously
        /// </summary>
        /// <param name="berthData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<BerthVO> DelBerthByIDAsync(BerthVO berthdata);      

        /// <summary>
        /// To Get Cargo Type
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategory> GetCargoType();

        /// <summary>
        /// To Get Vessel Type
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryCodeNameVO> GetVesselType();

        /// <summary>
        /// To Get Reason Type
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryCodeNameVO> GetReasonType();

        ////////mahesh/////////////
        /// <summary>
        /// To Get Berthlist with correspond Bollards
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<BerthVO> GetBerthsWithBollards();

        /// <summary>
        /// To Get Berthlist with correspond Bollards Asynchronously
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<BerthVO>> GetBerthsWithBollardsAsync();

        /// <summary>
        /// Author   :  Sandeep Appana
        /// Date     :  28-8-2014
        /// Purpose  :  To Get Berthlist based on Ports.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<BerthVO> GetBerthsWithPortCode();
       
    }
}
