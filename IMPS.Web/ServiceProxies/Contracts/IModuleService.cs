using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;


namespace IPMS.Web.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IModuleService : IDisposable
    {
        [OperationContract]
        List<ModuleVO> GetModules();

        [OperationContract]
        List<ModuleVO> GetParentModules();

        [OperationContract]
        ModuleVO PostModuleData(ModuleVO moduleData);

        [OperationContract]
        ModuleVO ModifyModule(ModuleVO moduleData);
        //ships in ports only for  admin
        [OperationContract]
        IEnumerable<UserRole> GetUserRoles(string username);

        //[OperationContract]
        //Task<List<ModuleVO>> GetModulesAsync();

        //[OperationContract]
        //Task<ModuleVO> PostModuleDataAsync(ModuleVO moduledata);

        //[OperationContract]
        //Task<List<ModuleVO>> GetParentModulesAsync();

        //[OperationContract]
        //Task<ModuleVO> ModifyModuleAsync(ModuleVO moduledata);
    }
}