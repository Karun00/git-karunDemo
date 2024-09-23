using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class EntityMapExtension
    {
        public static List<EntityVO> MapToListDto(this IEnumerable<Entity> entity)
        {
            List<EntityVO> entityVoList = new List<EntityVO>();
            if (entity != null)
            {
                foreach (var entitydata in entity)
                {
                    EntityVO entityVo = new EntityVO();
                    entityVo.EntityID = entitydata.EntityID;
                    entityVo.EntityCode = entitydata.EntityCode;
                    entityVo.ModuleID = entitydata.ModuleID;
                    entityVo.EntityName = entitydata.EntityName;
                    entityVo.PageUrl = entitydata.PageUrl;
                    entityVo.OrderNo = entitydata.OrderNo;
                    entityVo.HasWorkflow = entitydata.HasWorkFlow;
                    entityVo.RecordStatus = entitydata.RecordStatus;
                    entityVo.CreatedBy = entitydata.CreatedBy;
                    entityVo.CreatedDate = entitydata.CreatedDate;
                    entityVo.ModifiedBy = entitydata.ModifiedBy;
                    entityVo.ModifiedDate = entitydata.ModifiedDate;
                    entityVoList.Add(entityVo);
                }
            }
            return entityVoList;
        }

        public static List<Entity> MapToListEntity(this IEnumerable<EntityVO> entityVoList)
        {
            List<Entity> entitylist = new List<Entity>();
            if (entityVoList != null)
            {
                foreach (var entitydatavo in entityVoList)
                {
                    Entity entity = new Entity();
                    entity.EntityID = entitydatavo.EntityID;
                    entity.EntityCode = entitydatavo.EntityCode;
                    entity.ModuleID = entitydatavo.ModuleID;
                    entity.EntityName = entitydatavo.EntityName;
                    entity.PageUrl = entitydatavo.PageUrl;
                    entity.OrderNo = entitydatavo.OrderNo;
                    entity.HasWorkFlow = entitydatavo.HasWorkflow;
                    entity.RecordStatus = entitydatavo.RecordStatus;
                    entity.CreatedBy = entitydatavo.CreatedBy;
                    entity.CreatedDate = entitydatavo.CreatedDate;
                    entity.ModifiedBy = entitydatavo.ModifiedBy;
                    entity.ModifiedDate = entitydatavo.ModifiedDate;

                    entitylist.Add(entity);
                }
            }
            return entitylist;
        }

        public static EntityVO MapToDto(this Entity entitydata)
        {
            EntityVO entityVo = new EntityVO();
            if (entitydata != null)
            {
                entityVo.EntityID = entitydata.EntityID;
                entityVo.EntityCode = entitydata.EntityCode;
                entityVo.ModuleID = entitydata.ModuleID;
                entityVo.EntityName = entitydata.EntityName;
                entityVo.PageUrl = entitydata.PageUrl;
                entityVo.OrderNo = entitydata.OrderNo;
                entityVo.HasWorkflow = entitydata.HasWorkFlow;
                entityVo.RecordStatus = entitydata.RecordStatus;
                entityVo.CreatedBy = entitydata.CreatedBy;
                entityVo.CreatedDate = entitydata.CreatedDate;
                entityVo.ModifiedBy = entitydata.ModifiedBy;
                entityVo.ModifiedDate = entitydata.ModifiedDate;

                if (entitydata.WorkflowTasks != null)
                {
                    entityVo.WorkFlowTaskVO = entitydata.WorkflowTasks.ToList().MapToDTO();
                }
            }
            return entityVo;
        }

        public static Entity MapToEntity(this EntityVO entityDataVo)
        {
            Entity entity = new Entity();
            if (entityDataVo != null)
            {
            
            entity.EntityID = entityDataVo.EntityID;
            entity.EntityCode = entityDataVo.EntityCode;
            entity.ModuleID = entityDataVo.ModuleID;
            entity.EntityName = entityDataVo.EntityName;
            entity.PageUrl = entityDataVo.PageUrl;
            entity.OrderNo = entityDataVo.OrderNo;
            entity.HasWorkFlow = entityDataVo.HasWorkflow;
            entity.RecordStatus = entityDataVo.RecordStatus;
            entity.CreatedBy = entityDataVo.CreatedBy;
            entity.CreatedDate = entityDataVo.CreatedDate;
            entity.ModifiedBy = entityDataVo.ModifiedBy;
            entity.ModifiedDate = entityDataVo.ModifiedDate;
            entity.WorkflowTasks = entityDataVo.WorkFlowTaskVO.MapToEntity();
            entity.WorkflowTaskRole = entityDataVo.WorkFlowTaskRoleVO.MapToEntity();
        }

    return entity;
        }

        public static Entity MapToEntityModule(this EntityModulesVO entityDataVo)
        {
            Entity entity = new Entity();
            if (entityDataVo != null)
            {
                entity.EntityID = entityDataVo.EntityID;
                entity.EntityCode = entityDataVo.EntityCode;
                entity.ModuleID = entityDataVo.ModuleID;
                entity.EntityName = entityDataVo.EntityName;
                entity.PageUrl = entityDataVo.PageUrl;
                entity.OrderNo = entityDataVo.OrderNo;
                entity.HasWorkFlow = entityDataVo.HasWorkflow;
                entity.HasMenuItem = entityDataVo.HasMenuItem;
                entity.RecordStatus = entityDataVo.RecordStatus;
                List<EntityPrivilege> entityprivileges = new List<EntityPrivilege>();
                foreach (var entitytype in entityDataVo.EntityPrivileges)
                {
                    EntityPrivilege obj = new EntityPrivilege();
                    obj.EntityID = entityDataVo.EntityID;
                    obj.SubCatCode = entitytype;
                    obj.RecordStatus = entityDataVo.RecordStatus;
                    obj.CreatedBy = entityDataVo.CreatedBy;
                    obj.CreatedDate = entityDataVo.CreatedDate;
                    obj.ModifiedBy = entityDataVo.ModifiedBy;
                    obj.ModifiedDate = entityDataVo.ModifiedDate;

                    entityprivileges.Add(obj);
                }
                entity.EntityPrivileges = entityprivileges;
                entity.CreatedBy = entityDataVo.CreatedBy;
                entity.CreatedDate = entityDataVo.CreatedDate;
                entity.ModifiedBy = entityDataVo.ModifiedBy;
                entity.ModifiedDate = entityDataVo.ModifiedDate;
            }
            return entity;
        }

        public static List<EntityModulesVO> MapToEntityModulesListDto(this IEnumerable<Entity> entity)
        {
            List<EntityModulesVO> entityVoList = new List<EntityModulesVO>();
            if (entity != null)
            {
                foreach (var entitydata in entity)
                {
                    EntityModulesVO entityVo = new EntityModulesVO();
                    entityVo.EntityID = entitydata.EntityID;
                    entityVo.EntityCode = entitydata.EntityCode;
                    entityVo.ModuleID = entitydata.ModuleID;
                    entityVo.EntityName = entitydata.EntityName;
                    entityVo.PageUrl = entitydata.PageUrl;
                    entityVo.OrderNo = entitydata.OrderNo;
                    entityVo.HasWorkflow = entitydata.HasWorkFlow;
                    entityVo.HasMenuItem = entitydata.HasMenuItem;
                    entityVo.RecordStatus = entitydata.RecordStatus;
                    entityVo.CreatedBy = entitydata.CreatedBy;
                    entityVo.CreatedDate = entitydata.CreatedDate;
                    entityVo.ModifiedBy = entitydata.ModifiedBy;
                    entityVo.ModifiedDate = entitydata.ModifiedDate;
                    entityVo.Module = entitydata.Module.MapToDto();
                    entityVo.EntityPrivileges = entitydata.EntityPrivileges.MapToEntitykeysArray();

                    entityVoList.Add(entityVo);
                }
            }
            return entityVoList;
        }

        public static List<string> MapToEntitykeysArray(this ICollection<EntityPrivilege> entityPrivileges)
        {
            List<string> EntityArray = new List<string>();
            if (entityPrivileges != null)
            {
            
            foreach (var entityPrivilege in entityPrivileges)
            {
                EntityArray.Add(entityPrivilege.SubCatCode);
            }
        }

    return EntityArray;
        }
    }
}


