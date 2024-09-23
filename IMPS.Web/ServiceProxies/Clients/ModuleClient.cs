using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Web.ServiceProxies.Clients
{
    public class ModuleClient : UserClientBase<IModuleService>, IModuleService
    {
        /// <summary>
        /// Get All Modules
        /// </summary>
        /// <returns></returns>
        public List<ModuleVO> GetModules()
        {
            return WrapOperationWithException(() => Channel.GetModules());
        }
        /// <summary>
        /// Gets the parent modules
        /// </summary>
        /// <returns></returns>

        public List<ModuleVO> GetParentModules()
        {
            return WrapOperationWithException(() => Channel.GetParentModules());
        }
        /// <summary>
        /// gets post module data
        /// </summary>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        public ModuleVO PostModuleData(ModuleVO moduleData)
        {
            return WrapOperationWithException(() => Channel.PostModuleData(moduleData));
        }

        /// <summary>
        /// Modifies the module data
        /// </summary>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        public ModuleVO ModifyModule(ModuleVO moduleData)
        {
            return WrapOperationWithException(() => Channel.ModifyModule(moduleData));
        }

        //ships in ports only for admin
        public IEnumerable<UserRole> GetUserRoles(string username)
        {
            return WrapOperationWithException(() => Channel.GetUserRoles(username));
        }
      
        /// <summary>
        /// gets modules
        /// </summary>
        /// <returns></returns>
        //public Task<List<ModuleVO>> GetModulesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetModulesAsync());
        //}

        /// <summary>
        /// gets parent modules
        /// </summary>
        /// <returns></returns>
        //public Task<List<ModuleVO>> GetParentModulesAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetParentModulesAsync());
        //}
        /// <summary>
        /// adds modules data
        /// </summary>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        //public Task<ModuleVO> PostModuleDataAsync(ModuleVO moduledata)
        //{
        //    return WrapOperationWithException(() => Channel.PostModuleDataAsync(moduledata));
        //}

        /// <summary>
        /// modifies the module data
        /// </summary>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        //public Task<ModuleVO> ModifyModuleAsync(ModuleVO moduledata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyModuleAsync(moduledata));
        //}
    }
}