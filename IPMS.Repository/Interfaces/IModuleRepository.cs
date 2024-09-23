using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IModuleRepository
    {
        List<Module> GetMobileModules();
        List<ModuleVO> GetModules();
        List<ModuleVO> GetParentModules();
        ModuleVO PostModuleData(ModuleVO entityVo, int userId);
        ModuleVO ModifyModule(ModuleVO entityVo, int userId);
        IEnumerable<UserRole> GetUserRoles(string username);
    }
}
