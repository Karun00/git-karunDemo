using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ISubCategoryService
    {       
        /// <summary>
        /// To Add Sub Category Data
        /// </summary>
        /// <param name="subCategoryData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SubCategoryVO AddSubCategory(SubCategoryVO subCategoryData);

        /// <summary>
        ///  To Modify Sub Category Data
        /// </summary>
        /// <param name="subCategoryData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SubCategoryVO ModifySubCategory(SubCategoryVO subCategoryData);

        /// <summary>
        ///  To Get Sub Category id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SubCategoryVO GetSubCategoryId(string id);

        /// <summary>
        /// To Delete Sub Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SubCategoryVO DeleteSubCategory(long id);

        /// <summary>
        /// To Get Sub Category Details by id
        /// </summary>
        /// <param name="supcatId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> SubCategoryDetails(string supcatId);

        /// <summary>
        /// To Get All Sub Category Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> AllSubCategoryDetails();

      
        /// <summary>
        /// To Get Sub Category based on Super Category 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetSubCategoryWithSuperCatogory();

        /// <summary>
        /// To Get Super Category Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuperCategoryVO> SuperCategoryDetails();

        /// <summary>
        /// To Get Countries List
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetCountriesList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetSubCatName(string code);
    }
}
