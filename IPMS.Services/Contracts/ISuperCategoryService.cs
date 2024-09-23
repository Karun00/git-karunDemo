using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ISuperCategoryService
    {
        /// <summary>
        /// To Add Super Category Data
        /// </summary>
        /// <param name="supCatData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuperCategoryVO AddSuperCategory(SuperCategoryVO supCatData);

        /// <summary>
        /// To Modify Super Category Data
        /// </summary>
        /// <param name="supCatData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuperCategoryVO ModifySuperCategory(SuperCategoryVO supCatData);

        /// <summary>
        /// To Get Super Category Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuperCategoryVO> SuperCategoryDetails();
    }
}
