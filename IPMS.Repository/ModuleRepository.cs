using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using IPMS.Core.Repository.Exceptions;
using IPMS.Domain;

namespace IPMS.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private IUnitOfWork _unitOfWork;
       // private readonly ILog log;

        public ModuleRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
           // log = 
            LogManager.GetLogger(typeof(ModuleRepository));
        }

        #region GetMobileModules
        /// <summary>
        /// Get Mobile Modules
        /// </summary>
        /// <returns></returns>
        public List<Module> GetMobileModules()
        {
            var modules = _unitOfWork.Repository<Module>().Queryable().Where(x => x.IsMobile == "Y" && x.ParentModuleID == null).ToList();
            return modules;
        }
        #endregion

        #region GetModules
        /// <summary>
        /// Gets Modules list
        /// </summary>
        /// <returns></returns>
        public List<ModuleVO> GetModules()
        {
            var ModuleList = _unitOfWork.Repository<Module>().Queryable();
            return ModuleList.MapToListDto();
        }
        #endregion

        #region GetParentModules
        /// <summary>
        /// Gets Parent Module List
        /// </summary>
        /// <returns></returns>
        public List<ModuleVO> GetParentModules()
        {
            var ModuleList = _unitOfWork.Repository<Module>().Queryable().Where(x => x.ParentModuleID == null).OrderBy(x => x.ModuleName);
            return ModuleList.MapToListDto();
        }
        #endregion

        #region CheckModuleNameExists
        public Boolean CheckModuleNameExists(string moduleName)
        {
            var Count = 0;
            Count = _unitOfWork.Repository<Module>().Queryable().Where(x => x.ModuleName == moduleName && (x.ParentModuleID == null)).Count();
            if (Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region CheckModuleNameExists
        public Boolean CheckModuleNameExistsByParentModuleId(string moduleName, int? parentModuleId)
        {
            var Count = _unitOfWork.Repository<Module>().Queryable().Where(x => x.ModuleName == moduleName && x.ParentModuleID == parentModuleId).Count();
            if (Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region PostModuleData
        /// <summary>
        /// Add / Inserts new Module
        /// </summary>
        /// <param name="entityVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ModuleVO PostModuleData(ModuleVO entityVo, int userId)
        {
            if (entityVo != null)
            {
                if (entityVo.ParentModuleID > 0 &&
                    CheckModuleNameExistsByParentModuleId(entityVo.ModuleName, entityVo.ParentModuleID))
                {
                    throw new BusinessExceptions("This sub module name already exists.");
                }
                else if (CheckModuleNameExists(entityVo.ModuleName))
                {
                    throw new BusinessExceptions("This module name already exists.");
                }
                else
                {
                    entityVo.CreatedBy = userId;
                    entityVo.CreatedDate = DateTime.Now;
                    entityVo.ModifiedBy = userId;
                    entityVo.ModifiedDate = DateTime.Now;

                    Module entity = new Module();
                    entity = ModuleMapExtension.MapToEntity(entityVo);
                    entity.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<Module>().Insert(entity);
                    _unitOfWork.SaveChanges();
                }
            }
            return entityVo;
        }
        #endregion

        #region ModifyModule
        /// <summary>
        /// Modifies the Module data
        /// </summary>
        /// <param name="entityVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ModuleVO ModifyModule(ModuleVO entityVo, int userId)
        {
            bool isForSave = true;

            var moduleDetails = (from m in _unitOfWork.Repository<Module>().Query().Select()
                                 where m.ModuleID == entityVo.ModuleID
                                 select m).FirstOrDefault();

            if (entityVo.ParentModuleID > 0)
            {
                if (moduleDetails.ParentModuleID == entityVo.ParentModuleID && moduleDetails.ModuleID == entityVo.ModuleID && moduleDetails.ModuleName == entityVo.ModuleName)
                {
                    isForSave = true;
                }
                else if (CheckModuleNameExistsByParentModuleId(entityVo.ModuleName, entityVo.ParentModuleID))
                {
                    isForSave = false;
                    throw new BusinessExceptions("This sub module name already exists.");
                }
            }
            else
            {
                if (moduleDetails.ModuleID == entityVo.ModuleID && moduleDetails.ModuleName == entityVo.ModuleName)
                {
                    isForSave = true;
                }

                else if (CheckModuleNameExists(entityVo.ModuleName))
                {
                    isForSave = false;
                    throw new BusinessExceptions("This module name already exists.");
                }
            }

            if (isForSave)
            {
                entityVo.CreatedBy = moduleDetails.CreatedBy;
                entityVo.CreatedDate = moduleDetails.CreatedDate;
                entityVo.ModifiedBy = userId;
                entityVo.ModifiedDate = DateTime.Now;

                Module entity = new Module();
                entity = ModuleMapExtension.MapToEntity(entityVo);
                entity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Module>().Update(entity);
                _unitOfWork.SaveChanges();
            }
            return entityVo;
        }
        #endregion


        //ships in ports only for admin
        public IEnumerable<UserRole> GetUserRoles(string username)
        {
            var userid = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserName == username
                         select u.UserID) ;
            List<UserRole> roles = new List<UserRole>();
            foreach(int uid in userid)
            {
                
                roles.AddRange(from ur in _unitOfWork.Repository<UserRole>().Query().Select()
                         where ur.UserID == uid
                         select ur);

            }
            return roles;
        }
    }
}
