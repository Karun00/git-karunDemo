using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public class RolePrivilegeRepository : IRolePrivilegeRepository
    {
        private IUnitOfWork _unitOfWork;

        public RolePrivilegeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  To Get Role Privilege Details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleVO> RolePrivilegeDetails()
        {
            var RolePrivilegeDetails = from q in _unitOfWork.Repository<Role>().Queryable().AsEnumerable<Role>()
                                       orderby q.CreatedDate descending
                                       select new RoleVO
                                       {
                                           RoleID = q.RoleID,
                                           RoleCode = q.RoleCode,
                                           RoleDescription = q.RoleDescription,
                                           RoleName = q.RoleName,
                                           RecordStatus = q.RecordStatus,
                                           CreatedBy = q.CreatedBy,
                                           CreatedDate = q.CreatedDate

                                       };

            return RolePrivilegeDetails.ToList();

        }


        /// <summary>
        /// To Get Module Details
        /// </summary>
        /// <returns></returns>
        public ReferenceDataVO GetRoleReferenceData()
        {
            ReferenceDataVO RoleReferenceDataVO = new ReferenceDataVO();
            var ModuleList = from p in _unitOfWork.Repository<Module>().Queryable().AsEnumerable<Module>()
                             where p.ParentModuleID.Equals(null) & p.RecordStatus == "A"
                             select new ModuleVO
                             {
                                 ModuleID = p.ModuleID,
                                 ModuleName = p.ModuleName
                             };
            RoleReferenceDataVO.getModules = ModuleList.OrderBy(x=>x.ModuleName).ToList();

            return RoleReferenceDataVO;
        }

        /// <summary>
        ///  To Get Entities with Privileges
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="subModuleId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<EntityListPrivlegesVO> GetEntitiesWithPrivileges(int moduleId, int subModuleId, int roleId)
        {
            if (roleId == 0)
            {

                return GetEntityListPrivlegesVos(subModuleId);
            }
            else
            {
                List<EntityListPrivlegesVO> entityprivlegeslist = new List<EntityListPrivlegesVO>();

                var Entitylist = (from t in _unitOfWork.Repository<Entity>().Query().Include(t => t.EntityPrivileges).Select()
                                  where t.ModuleID == subModuleId
                                  select t).ToList<Entity>();

                foreach (Entity _entity in Entitylist)
                {
                    EntityListPrivlegesVO entityprivlege = new EntityListPrivlegesVO();
                    entityprivlege.EntityID = _entity.EntityID;
                    entityprivlege.EntityName = _entity.EntityName;

                    var EntityprivList = (from q in _unitOfWork.Repository<EntityPrivilege>().Query().Select()
                                          join sub in _unitOfWork.Repository<SubCategory>().Query().Select() on q.SubCatCode equals sub.SubCatCode
                                          where q.EntityID == _entity.EntityID & q.RecordStatus == "A"
                                          select new PrivelgeVO
                                          {
                                              SubCatCode = q.SubCatCode,
                                              SubCatName = sub.SubCatName,
                                              IsRole = false
                                          }).ToList();

                    entityprivlege.EntityPrivileges = EntityprivList;
                    entityprivlegeslist.Add(entityprivlege);
                }

                foreach (EntityListPrivlegesVO _entity1 in entityprivlegeslist)
                {

                    List<RolePrivlegeListVO> Roleprivss = new List<RolePrivlegeListVO>();
                    var Roleslist = (from q in _unitOfWork.Repository<RolePrivilege>().Query().Select()
                                     where ((q.RoleID == roleId) & (q.RecordStatus == "A")) & (q.EntityID == _entity1.EntityID)
                                     select new RolePrivlegeListVO
                                     {
                                         RoleID = q.RoleID,
                                         EntityID = q.EntityID
                                     }).Distinct().ToList();

                    foreach (RolePrivlegeListVO _entity in Roleslist)
                    {

                        RolePrivlegeListVO roleprivlege = new RolePrivlegeListVO();
                        roleprivlege.EntityID = _entity.EntityID;
                        roleprivlege.RoleID = _entity.RoleID;


                        var RoleprivLists = (from q in _unitOfWork.Repository<RolePrivilege>().Query().Select()
                                             join sub in _unitOfWork.Repository<SubCategory>().Query().Select() on q.SubCatCode equals sub.SubCatCode
                                             where ((q.RoleID == roleId) & (q.RecordStatus == "A") & (q.EntityID == roleprivlege.EntityID))
                                             select new SubCategoryVO
                                             {
                                                 SubCatCode = q.SubCatCode,
                                                 SubCatName = sub.SubCatName
                                             }).Distinct().ToList();

                        roleprivlege.RolePrivEditList = RoleprivLists;
                        // Roleprivss.Add(roleprivlege);
                        GetValueIsRole(_entity1, roleprivlege);
                    }

                    //foreach (RolePrivlegeListVO rolpriv in Roleprivss)
                    //    {
                    //        foreach (PrivelgeVO privle in _entity1.EntityPrivileges)
                    //        {
                    //            foreach (SubCategoryVO rol in rolpriv.RolePrivEditList)
                    //            {
                    //                if (rol.SubCatCode == privle.SubCatCode)
                    //                {
                    //                    privle.IsRole = true;
                    //                }
                    //            }
                    //        }
                    //    }   
                }
                return entityprivlegeslist;
            }
        }

        private static void GetValueIsRole(EntityListPrivlegesVO _entity1, RolePrivlegeListVO roleprivlege)
        {
            foreach (PrivelgeVO privle in _entity1.EntityPrivileges)
            {
                foreach (SubCategoryVO rol in roleprivlege.RolePrivEditList)
                {
                    if (rol.SubCatCode == privle.SubCatCode)
                    {
                        privle.IsRole = true;
                    }
                }
            }
        }

        private List<EntityListPrivlegesVO> GetEntityListPrivlegesVos(int subModuleId)
        {
            List<EntityListPrivlegesVO> entityprivlegeslist = new List<EntityListPrivlegesVO>();


            var Entitylist = (from t in _unitOfWork.Repository<Entity>().Query().Include(t => t.EntityPrivileges).Select()
                where t.ModuleID == subModuleId & t.RecordStatus == "A"
                select t).ToList<Entity>();

            foreach (Entity _entity in Entitylist)
            {
                EntityListPrivlegesVO entityprivlege = new EntityListPrivlegesVO();
                entityprivlege.EntityID = _entity.EntityID;
                entityprivlege.EntityName = _entity.EntityName;


                var EntityprivList = (from q in _unitOfWork.Repository<EntityPrivilege>().Query().Select()
                    join sub in _unitOfWork.Repository<SubCategory>().Query().Select() on q.SubCatCode equals sub.SubCatCode
                    where q.EntityID == _entity.EntityID
                    select new PrivelgeVO
                    {
                        SubCatCode = q.SubCatCode,
                        SubCatName = sub.SubCatName,
                        IsRole = false
                    }).ToList();

                entityprivlege.EntityPrivileges = EntityprivList;
                entityprivlegeslist.Add(entityprivlege);
            }

            return entityprivlegeslist;
        }


        /// <summary>
        ///  To Get SubModules based on Moduleid
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public IEnumerable<ModuleVO> GetSubModulesWithModuleId(int moduleId)
        {
            var SubModuleDetails = from q in _unitOfWork.Repository<Module>().Query().Select().AsEnumerable<Module>()
                                   where (q.RecordStatus == "A" & q.ParentModuleID == moduleId)
                                   orderby q.ModuleName                                   
                                   select new ModuleVO
                                   {
                                       ModuleID = q.ModuleID,
                                       ModuleName = q.ModuleName,
                                       ParentModuleID = q.ParentModuleID
                                   };

            return SubModuleDetails.ToList();
        }


        public List<RolePrivlegeListVO> GetRolesPrivilegeEdit(int moduleId, int subModuleId, int roleId)
        {
            List<RolePrivlegeListVO> Roleprivss = new List<RolePrivlegeListVO>();

            var Entitylist = (from q in _unitOfWork.Repository<RolePrivilege>().Query().Select()
                              where ((q.RoleID == roleId) & (q.RecordStatus == "A"))
                              select new RolePrivlegeListVO
                              {
                                  RoleID = q.RoleID,
                                  EntityID = q.EntityID
                              }).Distinct().ToList();

            foreach (RolePrivlegeListVO _entity in Entitylist)
            {

                RolePrivlegeListVO roleprivlege = new RolePrivlegeListVO();
                roleprivlege.EntityID = _entity.EntityID;
                roleprivlege.RoleID = _entity.RoleID;


                var RoleprivLists = (from q in _unitOfWork.Repository<RolePrivilege>().Query().Select()
                                     join sub in _unitOfWork.Repository<SubCategory>().Query().Select() on q.SubCatCode equals sub.SubCatCode
                                     where ((q.RoleID == roleId) & (q.RecordStatus == "A") & (q.EntityID == roleprivlege.EntityID))
                                     select new SubCategoryVO
                                     {
                                         SubCatCode = q.SubCatCode,
                                         SubCatName = sub.SubCatName
                                     }).Distinct().ToList();

                roleprivlege.RolePrivEditList = RoleprivLists;
                Roleprivss.Add(roleprivlege);
            }
            return Roleprivss;
        }
    }
}





