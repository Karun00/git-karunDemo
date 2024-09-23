using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class ModuleMapExtension
    {
        #region MapToDTO
        public static List<ModuleVO> MapToListDto(this IEnumerable<Module> module)
        {
            List<ModuleVO> moduleVoList = new List<ModuleVO>();
            if (module != null)
            {
                foreach (var moduledata in module)
                {
                    moduleVoList.Add(moduledata.MapToDto());
                }
            }
            return moduleVoList;
        }

        public static ModuleVO MapToDto(this Module data)
        {
            ModuleVO moduleVo = new ModuleVO();
            if (data != null)
            {
                moduleVo.ModuleID = data.ModuleID;
                moduleVo.ModuleName = data.ModuleName;
                moduleVo.ParentModuleID = data.ParentModuleID;
                moduleVo.OrderNo = data.OrderNo;
                moduleVo.RecordStatus = data.RecordStatus;
                moduleVo.CreatedBy = data.CreatedBy;
                moduleVo.CreatedDate = data.CreatedDate;
                moduleVo.ModifiedBy = data.ModifiedBy;
                moduleVo.ModifiedDate = data.ModifiedDate;
            }
            return moduleVo;
        }
        #endregion

        #region MapToEntity
        public static List<Module> MapToListEntity(this IEnumerable<ModuleVO> moduleVoList)
        {
            List<Module> modulelist = new List<Module>();
            if (moduleVoList != null)
            {
                foreach (var moduledatavo in moduleVoList)
                {
                    modulelist.Add(moduledatavo.MapToEntity());
                }
            }
            return modulelist;
        }

        public static Module MapToEntity(this ModuleVO vo)
        {
            Module module = new Module();
            if (vo != null)
            {
                module.ModuleID = vo.ModuleID;
                module.ModuleName = vo.ModuleName;
                module.ParentModuleID = vo.ParentModuleID;
                module.OrderNo = vo.OrderNo;
                module.RecordStatus = vo.RecordStatus;
                module.CreatedBy = vo.CreatedBy;
                module.CreatedDate = vo.CreatedDate;
                module.ModifiedBy = vo.ModifiedBy;
                module.ModifiedDate = vo.ModifiedDate;
            }
            return module;
        }
        #endregion
    }
}
