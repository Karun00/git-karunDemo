using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public class EntityRepository : IEntityRepository
    {
        private IUnitOfWork _unitOfWork;
       // private readonly ILog log;

        public EntityRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
           // log =
            LogManager.GetLogger(typeof(EntityRepository));
        }

        #region GetEntities
        public List<EntityVO> GetEntities()
        {
            var entity = (from entityvo in _unitOfWork.Repository<Entity>().Query().Select()
                          where entityvo.RecordStatus == "A"
                          select new EntityVO
                          {
                              EntityCode = entityvo.EntityCode,
                              EntityID = entityvo.EntityID,
                              EntityName = entityvo.EntityName,
                              HasWorkflow = entityvo.HasWorkFlow,
                              HasMenuItem = entityvo.HasMenuItem
                          });
            return entity.ToList();
        }
        #endregion

        #region GetFeaturesEntity
        public List<EntityVO> GetFeaturesEntity()
        {
            var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                          where e.ModuleID == 21
                          select new EntityVO
                          {
                              EntityID = e.EntityID,
                              EntityCode = e.EntityCode,
                              EntityName = e.EntityName,
                              ModuleID = e.ModuleID,
                              OrderNo = e.OrderNo,
                              PageUrl = e.PageUrl,
                              RecordStatus = e.RecordStatus
                          });
            return entity.ToList();
        }
        #endregion

        #region GetEntityByCode
        /// <summary>
        /// To Get Entity Details based on EntityCode
        /// </summary>
        /// <param name="pEntityCode"></param>
        /// <returns></returns>
        public Entity GetEntityByCode(string pEntityCode)
        {
            var entity = new Entity();
            //try
            //{
                entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                          where e.EntityCode == pEntityCode
                          select e).FirstOrDefault<Entity>();
                return entity;
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception = ", ex);
            //    return null;
            //}
        }
        #endregion

        #region EntityDetails
        /// <summary>
        ///  To Get Entity Details
        /// </summary>
        /// <returns></returns>
        public List<EntityModulesVO> EntityDetails()
        {
            var entitites = (from p in _unitOfWork.Repository<Entity>().Queryable()
                             join m in _unitOfWork.Repository<Module>().Queryable() on p.ModuleID equals m.ModuleID
                             where m.RecordStatus == "A"
                             select new EntityModulesVO
                             {
                                 EntityID = p.EntityID,
                                 EntityCode = p.EntityCode,
                                 EntityName = p.EntityName,
                                 PageUrl = p.PageUrl,
                                 OrderNo = p.OrderNo,
                                 ModuleID = p.ModuleID,
                                 HasWorkflow = p.HasWorkFlow,
                                 HasMenuItem = p.HasMenuItem,
                                 ModuleNameList = m.ModuleName,
                                 HasWorkflowStatus = p.HasWorkFlow == "Y" ? true : false,
                                 HasMenuItemStatus = p.HasMenuItem == "Y" ? true : false,
                                 RecordStatus = p.RecordStatus,
                                 CreatedBy = p.CreatedBy,
                                 CreatedDate = p.CreatedDate
                             }).OrderByDescending(x=>x.CreatedDate).ToList<EntityModulesVO>();


            GetValueEntity(entitites);

            return entitites;
        }

        private void GetValueEntity(List<EntityModulesVO> entitites)
        {
            foreach (var entity in entitites)
            {
                var result = (from bc in _unitOfWork.Repository<EntityPrivilege>().Queryable()
                              join s in _unitOfWork.Repository<SubCategory>().Queryable() on bc.SubCatCode equals s.SubCatCode
                    where bc.EntityID == entity.EntityID && bc.RecordStatus == "A"
                    select new {bc.SubCatCode}).Distinct().ToList();
                List<string> bentitytype = new List<string>();

                foreach (var x in result)
                {
                    bentitytype.Add(x.SubCatCode.ToString());
                }

                entity.EntityPrivileges = bentitytype;
            }
        }

        #endregion

        #region GetAllSubModules
        /// <summary>
        /// To Get Sub Modules
        /// </summary>
        /// <returns></returns>
        public List<EntityModulesVO> GetAllSubModules()
        {
            var SubMod = from module in _unitOfWork.Repository<Module>().Queryable()
                         join pmod in _unitOfWork.Repository<Module>().Queryable() on module.ParentModuleID equals pmod.ModuleID
                         select new EntityModulesVO
                         {
                             ModuleID = module.ModuleID,
                             ModuleName = module.ModuleName,
                             ParentModuleID = module.ParentModuleID,
                             ParentModuleName = pmod.ModuleName
                         };
            return SubMod.OrderBy(x=>x.ModuleName).ToList();
        }
        #endregion

        #region GetEntitiesNotification
        /// <summary>
        /// To Get Entity Details Based on EntitiyCode For Notifications
        /// </summary>
        /// <param name="entityCode"></param>
        /// <returns></returns>
        public Entity GetEntitiesNotification(string entityCode)
        {
            var entity = (from e in _unitOfWork.Repository<Entity>().Queryable().Where(e => e.EntityCode == entityCode)
                          //where e.EntityCode == entityCode
                          select e).FirstOrDefault<Entity>();
            return entity;
        }
        #endregion
    }
}

