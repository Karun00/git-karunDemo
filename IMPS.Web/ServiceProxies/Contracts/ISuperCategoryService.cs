using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISuperCategoryService : IDisposable
    {
        /// <summary>
        /// To Add Super Category Data
        /// </summary>
        /// <param name="supCatData"></param>
        /// <returns></returns>
        [OperationContract]
        SuperCategoryVO AddSuperCategory(SuperCategoryVO supCatData);

        /// <summary>
        /// To Modify Super Category Data
        /// </summary>
        /// <param name="supCatData"></param>
        /// <returns></returns>
        [OperationContract]
        SuperCategoryVO ModifySuperCategory(SuperCategoryVO supCatData);

        /// <summary>
        /// To Add Super Category Data Asynchronously
        /// </summary>
        /// <param name="supCatData"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<SuperCategoryVO> AddSuperCategoryAsync(SuperCategoryVO supcatdata);

        ///// <summary>
        ///// To Modify Super Category Data Asynchronously
        ///// </summary>
        ///// <param name="supcatdata"></param>
        ///// <returns></returns>
        //[OperationContract]
        //Task<SuperCategoryVO> ModifySuperCategoryAsync(SuperCategoryVO supcatdata);

        /// <summary>
        /// To Get Super Category Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SuperCategoryVO> SuperCategoryDetails();

        /// <summary>
        /// To Get Super Category Details Asynchronously
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<SuperCategoryVO>> SuperCategoryDetailsAsync();


    }
}
