using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
   public static class SubCategoryMapExtenstion
    {
       public static SubCategoryVO MapToDto(this SubCategory data)
       {
           SubCategoryVO subcategoryvo = new SubCategoryVO();
           if (data != null)
           {
               subcategoryvo.SubCatCode = data.SubCatCode;
               subcategoryvo.SupCatCode = data.SupCatCode;
               subcategoryvo.SubCatName = data.SubCatName;
               subcategoryvo.CreatedBy = data.CreatedBy;
               subcategoryvo.CreatedDate = data.CreatedDate;
               subcategoryvo.ModifiedBy = data.ModifiedBy;
               subcategoryvo.ModifiedDate = data.ModifiedDate;
               subcategoryvo.RecordStatus = data.RecordStatus;
           }
           return subcategoryvo;
       }
       public static SubCategory MapToEntity(this SubCategoryVO subCategoryVo)
       {
           
           SubCategory subcategory = new SubCategory();
           if (subCategoryVo != null)
           {
               subcategory.SubCatCode = subCategoryVo.SubCatCode;
               subcategory.SupCatCode = subCategoryVo.SupCatCode;
               subcategory.SubCatName = subCategoryVo.SubCatName;
               subcategory.CreatedBy = subCategoryVo.CreatedBy;
               subcategory.CreatedDate = subCategoryVo.CreatedDate;
               subcategory.ModifiedBy = subCategoryVo.ModifiedBy;
               subcategory.ModifiedDate = subCategoryVo.ModifiedDate;
               subcategory.RecordStatus = subCategoryVo.RecordStatus;
           }
           return subcategory;
       }
       public static List<SubCategory> MapToEntity(this List<SubCategoryVO> subCategoryVoList)
       {
           List<SubCategory> subcategoryEntities = new List<SubCategory>();
           if (subCategoryVoList != null)
           {
               foreach (var subcategoryvo in subCategoryVoList)
               {
                   subcategoryEntities.Add(subcategoryvo.MapToEntity());
               }
           }
           return subcategoryEntities;
       }
       public static List<SubCategoryVO> MapToDto(this List<SubCategory> subCategoryList)
       {
           List<SubCategoryVO> subcategoryvolist = new List<SubCategoryVO>();
           if (subCategoryList != null)
           {
               foreach (var subcategory in subCategoryList)
               {
                   subcategoryvolist.Add(subcategory.MapToDto());
               }
           }
           return subcategoryvolist;
       }

       public static List<SubCategoryCodeNameVO> MapToDtoCodeName(this List<SubCategory> subCategoryList)
       {
           List<SubCategoryCodeNameVO> subcategoryvolist = new List<SubCategoryCodeNameVO>();
           if (subCategoryList != null)
           {
               foreach (var subcategory in subCategoryList)
               {
                   subcategoryvolist.Add(subcategory.MapToDtoCodeName());
               }
           }
           return subcategoryvolist;
       }

       public static SubCategoryCodeNameVO MapToDtoCodeName(this SubCategory data)
       {
           SubCategoryCodeNameVO subcategoryvo = new SubCategoryCodeNameVO();
           if (data != null)
           {
               subcategoryvo.SubCatCode = data.SubCatCode;
               subcategoryvo.SubCatName = data.SubCatName;
           }
           return subcategoryvo;
       }
    }
}
