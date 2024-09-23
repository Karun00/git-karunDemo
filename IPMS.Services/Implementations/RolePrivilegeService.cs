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
using System.Data.SqlClient;
using log4net;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RolePrivilegeService : ServiceBase, IRolePrivilegeService
    {
        private IRolePrivilegeRepository _roleprivilegeRepository;
      //  private IAccountRepository _accountRepository;
      //  private readonly ILog log;

        public RolePrivilegeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roleprivilegeRepository = new RolePrivilegeRepository(_unitOfWork);

           // log = LogManager.GetLogger(typeof(RolePrivilegeService));

         //   _accountRepository = new AccountRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public RolePrivilegeService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
           // log = LogManager.GetLogger(typeof(RolePrivilegeService));
            _UserId = GetUserIdByLoginname(_LoginName);
            _roleprivilegeRepository = new RolePrivilegeRepository(_unitOfWork);
         //   _accountRepository = new AccountRepository(_unitOfWork);
            // TODO: Complete member initialization
        }

        /// <summary>
        /// To Add Role Privilege Data
        /// </summary>
        /// <param name="rolePrivData"></param>
        /// <returns></returns>
        public RoleVO AddRolePrivilege(RoleVO rolePrivData)
        {
            return EncloseTransactionAndHandleException(() =>
            {

                rolePrivData.CreatedBy = 1;  // TO DO      ....Have to send anonymous userid
                rolePrivData.CreatedDate = DateTime.Now;
                rolePrivData.ModifiedBy = 1;  // To DO     ....Have to send anonymous userid
                rolePrivData.ModifiedDate = DateTime.Now;
                Role Roles = new Role();
                Roles = RolePrivilegeMapExtension.MaoToEntity(rolePrivData);

                List<RolePrivilegeVO> _RolePrivileges = new List<RolePrivilegeVO>();
                _RolePrivileges = rolePrivData.RolePrivileges;

                Roles.RolePrivileges = null;
                Roles.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<Role>().Insert(Roles);
                _unitOfWork.SaveChanges();

                List<RolePrivilege> _RolePrivlegeList = new List<RolePrivilege>();
                foreach (var data in _RolePrivileges)
                {
                    RolePrivilege _roleprivelge = new RolePrivilege();
                    _roleprivelge.RoleID = Roles.RoleID;
                    _roleprivelge.EntityID = data.EntityID;
                    _roleprivelge.SubCatCode = data.SubCatCode;
                    _roleprivelge.RecordStatus = "A";
                    _roleprivelge.CreatedBy = 1;  // TO Do    ....Have to send anonymous userid
                    _roleprivelge.CreatedDate = rolePrivData.CreatedDate;
                    _roleprivelge.ModifiedBy = 1;  // TO DO     ....Have to send anonymous userid
                    _roleprivelge.ModifiedDate = rolePrivData.ModifiedDate;
                    _RolePrivlegeList.Add(_roleprivelge);
                }
                _unitOfWork.Repository<RolePrivilege>().InsertRange(_RolePrivlegeList);
                _unitOfWork.SaveChanges();
                return rolePrivData;
            });
        }


        /// <summary>
        /// To Modify Role Privilege Data
        /// </summary>
        /// <param name="rolePrivilegeData"></param>
        /// <returns></returns>
        public RoleVO ModifyRolePrivilege(RoleVO rolePrivilegeData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                
                rolePrivilegeData.ModifiedDate = DateTime.Now;
                Role RolePriv = new Role();
                RolePriv = RolePrivilegeMapExtension.MaoToEntity(rolePrivilegeData);

                List<RolePrivilegeVO> _RolePrivileges = new List<RolePrivilegeVO>();
                _RolePrivileges = rolePrivilegeData.RolePrivileges;

                var roleObj = _unitOfWork.Repository<Role>().Find(RolePriv.RoleID);

                if (roleObj != null)
                {
                    roleObj.RoleCode = RolePriv.RoleCode;
                    roleObj.RoleName = RolePriv.RoleName;
                    roleObj.RoleDescription = RolePriv.RoleDescription;
                    roleObj.RecordStatus = RolePriv.RecordStatus;
                    roleObj.CreatedBy = 1;   // TO DO   ....Have to send anonymous userid
                    roleObj.CreatedDate = RolePriv.CreatedDate;
                    roleObj.ModifiedBy = 1;    // TO DO   ....Have to send anonymous userid
                    roleObj.ModifiedDate = RolePriv.ModifiedDate;

                    roleObj.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<Role>().Update(roleObj);
                    _unitOfWork.SaveChanges();
                }


                if (rolePrivilegeData.RoleID > 0 && rolePrivilegeData.ModuleID > 0 && rolePrivilegeData.SubModuleID > 0)
                {
                    var recordst = "I";
                    _unitOfWork.ExecuteSqlCommand("Update RolePrivilege set RecordStatus=@p0,ModifiedDate=getdate(), ModifiedBy=@p1 Where EntityID in (select EntityID from Entity where Moduleid =@p2) and RoleID = @p3", recordst,1, rolePrivilegeData.SubModuleID, RolePriv.RoleID);

                    List<RolePrivilege> _RolePrivlegeList = new List<RolePrivilege>();
                    foreach (var data in _RolePrivileges)
                    {
                        var dbInactrolepriviliges = (from bc in _unitOfWork.Repository<RolePrivilege>().Query().Select().AsEnumerable()
                                                     where (bc.RoleID == RolePriv.RoleID) && (bc.EntityID == data.EntityID) && (bc.SubCatCode == data.SubCatCode)
                                                     select bc).ToList();

                        if (dbInactrolepriviliges.Count != 0)
                        {
                            recordst = "A";
                            _unitOfWork.ExecuteSqlCommand("update RolePrivilege set RecordStatus = @p0, ModifiedDate=getdate(), ModifiedBy=@p1 where EntityID = @p2 and RoleID = @p3 and SubCatCode = @p4", recordst,1, data.EntityID, data.RoleID, data.SubCatCode);
                            _unitOfWork.SaveChanges();
                        }
                        else
                        {
                            RolePrivilege _roleprivelge2 = new RolePrivilege();
                            _roleprivelge2.RoleID = RolePriv.RoleID;
                            _roleprivelge2.EntityID = data.EntityID;
                            _roleprivelge2.SubCatCode = data.SubCatCode;
                            _roleprivelge2.RecordStatus = "A";
                            _roleprivelge2.CreatedBy = rolePrivilegeData.CreatedBy;
                            _roleprivelge2.CreatedDate = rolePrivilegeData.CreatedDate;
                            _roleprivelge2.ModifiedBy = rolePrivilegeData.CreatedBy;
                            _roleprivelge2.ModifiedDate = rolePrivilegeData.ModifiedDate;
                            _unitOfWork.Repository<RolePrivilege>().Insert(_roleprivelge2);
                            _unitOfWork.SaveChanges();
                        }
                    }
                }

                return rolePrivilegeData;
            });
        }


        /// <summary>
        ///  To Get Role Privilege Details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleVO> RolePrivilegeDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _roleprivilegeRepository.RolePrivilegeDetails();

            });
        }


        /// <summary>
        /// To Get Module Details
        /// </summary>
        /// <returns></returns>
        public ReferenceDataVO GetRoleReferenceData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _roleprivilegeRepository.GetRoleReferenceData();
            });
        }



        public List<RolePrivlegeListVO> GetRolesPrivilegeEdit(int moduleId, int subModuleId, int roleId)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                return _roleprivilegeRepository.GetRolesPrivilegeEdit(moduleId, subModuleId, roleId);
            });
        }

        /// <summary>
        ///  To Get Entities with Privileges
        /// </summary>
        /// <param name="Moduleid"></param>
        /// <param name="SubModuleid"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public List<EntityListPrivlegesVO> GetEntitiesWithPrivileges(int moduleId, int subModuleId, int roleId)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                return _roleprivilegeRepository.GetEntitiesWithPrivileges(moduleId, subModuleId, roleId);
            });
        }

        /// <summary>
        ///  To Get SubModules based on Moduleid
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public IEnumerable<ModuleVO> GetSubModulesWithModuleId(int moduleId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _roleprivilegeRepository.GetSubModulesWithModuleId(moduleId);
            });
        }
    }
}
