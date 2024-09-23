using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using IPMS.Services.WorkFlow;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using IPMS.Core.Repository.Exceptions;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WorkFlowTaskService : ServiceBase, IWorkFlowTaskService
    {
        private ISubCategoryRepository _subcategoryRepository;
        private IEntityRepository _entityRepository;
        private IWorkFlowTaskRepository _workflowtaskRepository;
        private IUserRepository _userRepository;

        public WorkFlowTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _entityRepository = new EntityRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _workflowtaskRepository = new WorkFlowTaskRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public WorkFlowTaskService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _entityRepository = new EntityRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _workflowtaskRepository = new WorkFlowTaskRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        #region GetWorkFlowTaskReferenceVO
        public WorkFlowTaskReferenceVO GetWorkFlowTaskReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                WorkFlowTaskReferenceVO VO = new WorkFlowTaskReferenceVO();

                VO.Entities = _entityRepository.GetEntities().Where(t => t.HasWorkflow == "Y").OrderBy(t => t.EntityName).ToList();
                VO.WorkFlowEvents = _subcategoryRepository.WorkFlowEvents().MapToDto();
                VO.Roles = _userRepository.GetRoles();
                return VO;
            });
        }
        #endregion

        #region GetWorkFlowTasks
        public List<WorkFlowTaskVO> GetWorkFlowTasks()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _workflowtaskRepository.GetWorkFlowTasks().MapToDTO();
            });
        }
        #endregion

        #region AddWorkFlowTask
        public EntityVO AddWorkFlowTask(EntityVO value)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                bool isUPdate = false;
                return _workflowtaskRepository.InsertOrUpdateWorkFlowTask(value, _UserId, _PortCode, isUPdate);
            });
        }
        #endregion

        #region ModifyWorkFlowTask
        public EntityVO ModifyWorkFlowTask(EntityVO value)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                bool isUPdate = true;
                return _workflowtaskRepository.InsertOrUpdateWorkFlowTask(value, _UserId, _PortCode, isUPdate);
            });
        }
        #endregion

        #region GetWorkFlowTaskAction
        public IEnumerable<PendingTaskVO> GetWorkFlowTaskAction(string ReferenceID, int WorkflowInstanceID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _workflowtaskRepository.GetWorkFlowTaskAction(ReferenceID, WorkflowInstanceID, _UserId);
            });
        }
        #endregion

        #region GetWorkFlowTask
        public List<EntityVO> GetWorkFlowTask()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _workflowtaskRepository.GetWorkFlowTask(_PortCode).ToList();
            });
        }
        #endregion


        #region GetWorkFlowTaskStatus

        public PendingTaskVO GetWorkFlowTaskStatus(string ReferenceID, int WorkflowInstanceId, string TaskCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
              return _workflowtaskRepository.GetWorkFlowTaskStatus(ReferenceID, WorkflowInstanceId, TaskCode);
            });
        }

        #endregion
       

    }
}
