using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Services;
using IPMS.Domain.DTOS;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ModuleService : ServiceBase, IModuleService
    {
        private IModuleRepository _ModuleRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ModuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ModuleRepository = new ModuleRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ModuleService()
        {
            // TODO: Complete member initialization
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _ModuleRepository = new ModuleRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        /// <summary>
        /// Gets Modules list
        /// </summary>
        /// <returns></returns>
        public List<ModuleVO> GetModules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ModuleRepository.GetModules();
            });
        }

        /// <summary>
        /// Gets Parent Module List
        /// </summary>
        /// <returns></returns>
        public List<ModuleVO> GetParentModules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ModuleRepository.GetParentModules();
            });
        }

        /// <summary>
        /// Add / Inserts new Module
        /// </summary>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        public ModuleVO PostModuleData(ModuleVO moduleData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ModuleRepository.PostModuleData(moduleData, _UserId);
            });
        }

        /// <summary>
        /// Modifies the Module data
        /// </summary>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        public ModuleVO ModifyModule(ModuleVO moduleData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ModuleRepository.ModifyModule(moduleData, _UserId);
            });
        }
        // ships in ports only for admin
        public IEnumerable<UserRole> GetUserRoles(string username)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ModuleRepository.GetUserRoles(username);
            });
        }
    }
}
