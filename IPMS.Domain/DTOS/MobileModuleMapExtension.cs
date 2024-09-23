using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class MobileModuleMapExtension
    {
        public static List<MobileModuleVO> MapToListDTOMobile(this IEnumerable<Module> moduleList)
        {
            List<MobileModuleVO> modulevoList = new List<MobileModuleVO>();
            if (moduleList != null)
                foreach (var data in moduleList)
                {
                    modulevoList.Add(data.MapToDTOMobile());
                }
            return modulevoList;
        }

        public static MobileModuleVO MapToDTOMobile(this Module data)
        {
            MobileModuleVO moduleVo = new MobileModuleVO();
            if (data != null)
            {
                moduleVo.ModuleID = data.ModuleID;
                moduleVo.ModuleName = data.ModuleName;
                moduleVo.PageUrl = data.PageUrl;
                moduleVo.ParentModuleID = data.ParentModuleID;
                moduleVo.IsMobile = data.IsMobile;
                moduleVo.MobileImage = data.MobileImage;
                moduleVo.OrderNo = data.OrderNo;
                moduleVo.RecordStatus = data.RecordStatus;
                moduleVo.CreatedBy = data.CreatedBy;
                moduleVo.CreatedDate = data.CreatedDate;
                moduleVo.ModifiedBy = data.ModifiedBy;
                moduleVo.ModifiedDate = data.ModifiedDate;

                moduleVo.MobileReference = data.MobileReference;



                if (data.OrderNo < 3)
                {
                    moduleVo.DisplayCount = true;
                    if (data.OrderNo == 1)
                    {
                        moduleVo.Count = data.Count;

                    }
                }
                else
                {
                    moduleVo.DisplayCount = false;
                }
            }
            return moduleVo;
        }      
    }
}
