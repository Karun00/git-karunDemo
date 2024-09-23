using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;


namespace IPMS.ServiceProxies.Clients
{
    public class SubCategoryClient : UserClientBase<ISubCategoryService>, ISubCategoryService
    {
        public List<SubCategoryVO> SubCategoryDetails(string supcatId)
        {
            return WrapOperationWithException(() => Channel.SubCategoryDetails(supcatId));
        }

        public List<SuperCategoryVO> SuperCategoryDetails()
        {
            return WrapOperationWithException(() => Channel.SuperCategoryDetails());
        }

        public List<SubCategoryVO> AllSubCategoryDetails()
        {
            return WrapOperationWithException(() => Channel.AllSubCategoryDetails());
        }

        public SubCategoryVO AddSubCategory(SubCategoryVO subCategoryData)
        {
            return WrapOperationWithException(() => Channel.AddSubCategory(subCategoryData));
        }

        public SubCategoryVO ModifySubCategory(SubCategoryVO subCategoryData)
        {
            return WrapOperationWithException(() => Channel.ModifySubCategory(subCategoryData));
        }
     
        public SubCategoryVO GetSubCategoryId(long id)
        {
            return WrapOperationWithException(() => Channel.GetSubCategoryId(id));
        }

        public SubCategoryVO DeleteSubCategory(long id)
        {
            return WrapOperationWithException(() => Channel.DeleteSubCategory(id));
        }

        public List<SubCategoryVO> GetCountriesList()
        {
            return WrapOperationWithException(() => Channel.GetCountriesList());
        }

        public string GetSubCatName(string code)
        {
            return WrapOperationWithException(() => Channel.GetSubCatName(code));
        }    
    }
}