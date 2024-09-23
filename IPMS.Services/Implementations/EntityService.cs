using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{ 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, 
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class EntityService : ServiceBase, IEntityService
    {
        private IEntityRepository _entityRepository;
  
        public EntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _entityRepository = new EntityRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }
        public EntityService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _entityRepository = new EntityRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);

        }       

        /// <summary>
        ///  To Get Entity Details
        /// </summary>
        /// <returns></returns>
        public List<EntityModulesVO> EntityDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _entityRepository.EntityDetails();
            });
        }

        /// <summary>
        /// To Get Sub Modules
        /// </summary>
        /// <returns></returns>
        public List<EntityModulesVO> GetAllSubModules()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _entityRepository.GetAllSubModules();
            });
        }
            
        /// <summary>
        ///  To Get Privilege Types
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetPrivilegeTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SubCategoryRepository repository = new SubCategoryRepository(_unitOfWork);
                List<SubCategory> subcategories = repository.PrivilegeTypes();
                return subcategories.MapToDto();
            });
        }

        /// <summary>
        ///  To Add Entity Data
        /// </summary>
        /// <param name="entityData"></param>
        /// <returns></returns>
        public EntityModulesVO AddEntity(EntityModulesVO entityData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
              //  string name = _LoginName;

                entityData.CreatedBy = _UserId;
                entityData.CreatedDate = DateTime.Now;
                entityData.ModifiedBy = _UserId;
                entityData.ModifiedDate = DateTime.Now;
                Entity entity = new Entity();
                entity = EntityMapExtension.MapToEntityModule(entityData);
                List<EntityPrivilege> entityPrivileges = entity.EntityPrivileges.ToList();
                entity.EntityPrivileges = null;
                entity.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<Entity>().Insert(entity);
                _unitOfWork.SaveChanges();

                var entityid = (from p in _unitOfWork.Repository<Entity>().Query().Select()
                                where p.EntityCode == entityData.EntityCode
                                select p).FirstOrDefault<Entity>();     
                                        
                foreach (var entityprivilege in entityPrivileges)
                {
                    entityprivilege.EntityID = entityid.EntityID;                    
                    _unitOfWork.Repository<EntityPrivilege>().Insert(entityprivilege);
                    _unitOfWork.SaveChanges();
                }

                return entityData;
            });
        }

        /// <summary>
        ///  To Modify Entity Data
        /// </summary>
        /// <param name="entityData"></param>
        /// <returns></returns>
        public EntityModulesVO ModifyEntity(EntityModulesVO entityData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                entityData.ModifiedBy = _UserId;
                entityData.ModifiedDate = DateTime.Now;
                Entity entity = new Entity();
                entity = EntityMapExtension.MapToEntityModule(entityData);
                List<EntityPrivilege> entityPrivileges = entity.EntityPrivileges.ToList();
                entity.EntityPrivileges = null;              
                var entityObj = _unitOfWork.Repository<Entity>().Find(entity.EntityID);

                if (entityObj != null)
                {
                    entityObj.EntityCode = entity.EntityCode;
                    entityObj.ModuleID = entity.ModuleID;
                    entityObj.EntityName = entity.EntityName;
                    entityObj.PageUrl = entity.PageUrl;
                    entityObj.OrderNo = entity.OrderNo;
                    entityObj.HasWorkFlow = entity.HasWorkFlow;
                    entityObj.HasMenuItem = entity.HasMenuItem;
                    entityObj.RecordStatus = entity.RecordStatus;
                    entityObj.CreatedBy = entity.CreatedBy;
                    entityObj.CreatedDate = entity.CreatedDate;
                    entityObj.ModifiedBy = entity.ModifiedBy;
                    entityObj.ModifiedDate = entity.ModifiedDate;
                    entityObj.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<Entity>().Update(entityObj);
                    _unitOfWork.SaveChanges();

                }

                var dbentitypriviliges = (from bc in _unitOfWork.Repository<EntityPrivilege>().Query().Select().AsEnumerable()
                                     where bc.EntityID == entityData.EntityID
                                     select bc).ToList();



                if (dbentitypriviliges != null)
                {
                    foreach (var bc in entityPrivileges)
                    {
                        var newentity = true;
                        foreach (var dbbc in dbentitypriviliges)
                        {
                            if (bc.EntityID == dbbc.EntityID && bc.SubCatCode == dbbc.SubCatCode)
                            {
                                newentity = false;
                            }
                            if (bc.EntityID == dbbc.EntityID && bc.SubCatCode == dbbc.SubCatCode && dbbc.RecordStatus == "I")
                            {
                                dbbc.RecordStatus = "A";
                                dbbc.ModifiedBy = dbbc.ModifiedBy;
                                dbbc.ModifiedDate = dbbc.ModifiedDate;
                                _unitOfWork.Repository<EntityPrivilege>().Update(dbbc);
                                _unitOfWork.SaveChanges();
                            }
                        }

                        if (newentity)
                        {
                            bc.RecordStatus = "A";
                            bc.ObjectState = ObjectState.Added;
                            _unitOfWork.Repository<EntityPrivilege>().Insert(bc);
                            _unitOfWork.SaveChanges();
                        }
                    }

                    foreach (var dbbc in dbentitypriviliges)
                    {
                        var inactive = true;
                        foreach (var bc in entityPrivileges)
                        {
                            if (dbbc.EntityID == bc.EntityID && dbbc.SubCatCode == bc.SubCatCode)
                            {
                                inactive = false;
                            }
                        }
                        if (inactive)
                        {
                            dbbc.RecordStatus = "I";
                            dbbc.ModifiedBy = dbbc.ModifiedBy;
                            dbbc.ModifiedDate = dbbc.ModifiedDate;
                            _unitOfWork.Repository<EntityPrivilege>().Update(dbbc);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }

                return entityData;
            });
        }
    }
}
