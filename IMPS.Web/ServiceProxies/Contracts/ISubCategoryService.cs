using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISubCategoryService : IDisposable
    {
        /// <summary>
        /// To Add Sub Category Data
        /// </summary>
        /// <param name="subCategoryData"></param>
        /// <returns></returns>
        [OperationContract]
        SubCategoryVO AddSubCategory(SubCategoryVO subCategoryData);

        /// <summary>
        /// To Modify Sub Category Data
        /// </summary>
        /// <param name="subCategoryData"></param>
        /// <returns></returns>
        [OperationContract]
        SubCategoryVO ModifySubCategory(SubCategoryVO subCategoryData);

        /// <summary>
        /// To Get Sub Category id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        SubCategoryVO GetSubCategoryId(long id);

        /// <summary>
        /// To Delete Sub Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        SubCategoryVO DeleteSubCategory(long id);

        /// <summary>
        /// To Get Sub Category Details by id
        /// </summary>
        /// <param name="supcatId"></param>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> SubCategoryDetails(string supcatId);

        /// <summary>
        ///  To Get All Sub Category Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> AllSubCategoryDetails();

        /// <summary>
        ///  To Get Super Category Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SuperCategoryVO> SuperCategoryDetails();

        /// <summary>
        ///  To Get Countries List
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategoryVO> GetCountriesList();

        /// <summary>
        ///  To Get sun Cat Name
        ///  /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetSubCatName(string code);
    }
}
