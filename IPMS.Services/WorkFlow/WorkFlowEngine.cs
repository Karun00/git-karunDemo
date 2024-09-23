using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using System.Data.Entity.SqlServer;
using Core.Repository.Providers.EntityFramework;
using IPMS.Domain;
using IPMS.Core.Repository.Exceptions;
using System.Globalization;

namespace IPMS.Services.WorkFlow
{
    public class WorkFlowEngine<T>
        : IWorkFlowEngine<T> where T : IWorkFlowEntity
    {

        private readonly IUnitOfWork _unitOfWork;
        private string _contextPortCode;
        private int _contextUserId;
        private WorkflowTask _task;
        private INotificationPublisher _notificationpublisher;
        private IAccountRepository _accountRepository;
        private PortGeneralConfigsRepository _portconfiguration;

        private int _entityid;

        public WorkFlowEngine(IUnitOfWork unitOfWork, string portCode, int userId)
        {
            _unitOfWork = unitOfWork;
            _contextPortCode = portCode;
            _contextUserId = userId;
            _accountRepository = new AccountRepository(unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portconfiguration = new PortGeneralConfigsRepository(_unitOfWork);

        }

        public WorkFlowEngine()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portconfiguration = new PortGeneralConfigsRepository(_unitOfWork);

        }

        public void Process(T workflowEntity, string workFlowTaskCode)
        {

            string workflowinitialtstatus = string.Empty;

            _entityid = workflowEntity.Entity.EntityID;
            string hasworkflow = workflowEntity.Entity.HasWorkFlow;

            workflowEntity.ExecuteTask(workFlowTaskCode);


            if (hasworkflow == "Y")
            {
                List<string> _portCodes = new List<string>();
                _portCodes = workflowEntity.PortCodes.ToList();

                foreach (var portcode in _portCodes)
                {
                    _contextPortCode = portcode;
                    WorkflowInstance wfInstance = GetWorkFlowInstance(workflowEntity);

                    WorkflowProcess wfInstancedata = new WorkflowProcess();
                    _task = GetWorkflowTask(workFlowTaskCode, workflowEntity);
                    List<WorkflowTaskRole> roles = GetWorkFlowStepRoles(workFlowTaskCode, workflowEntity, _task);



                    if (wfInstance != null)
                    {
                        wfInstance.RecordStatus = RecordStatus.Active;
                        wfInstance.ReferenceID = workflowEntity.ReferenceId;

                        wfInstancedata.ToTaskCode = workFlowTaskCode;
                        // Changes done for Service Request Cancellation Reject
                        if (workFlowTaskCode == WFStatus.CancelReject && workflowEntity.Entity.EntityCode == EntityCodes.ServiceRequest)
                        {
                            wfInstancedata.ToTaskCode = WFStatus.Confirmed;
                        }

                        wfInstancedata.ReferenceData = workflowEntity.ReferenceData;
                        wfInstancedata.CreatedBy = _contextUserId;
                        wfInstancedata.CreatedDate = DateTime.Now;
                        wfInstancedata.RecordStatus = RecordStatus.Active;
                    }
                    else
                    {
                        wfInstance = new WorkflowInstance();
                        wfInstance.CreatedBy = _contextUserId;
                        wfInstance.CreatedDate = DateTime.Now;
                        wfInstance.RecordStatus = RecordStatus.Active;

                        wfInstancedata.FromTaskCode = workFlowTaskCode;
                        wfInstancedata.ToTaskCode = workFlowTaskCode;
                        wfInstance.ReferenceID = workflowEntity.ReferenceId;
                        wfInstancedata.ReferenceData = workflowEntity.ReferenceData;
                        wfInstancedata.CreatedBy = _contextUserId;
                        wfInstancedata.CreatedDate = DateTime.Now;
                        wfInstancedata.RecordStatus = RecordStatus.Active;

                    }

                    wfInstance.WorkflowTaskCode = workFlowTaskCode;
                    // Changes done for Service Request Cancellation Reject
                    if (workFlowTaskCode == WFStatus.CancelReject && workflowEntity.Entity.EntityCode == EntityCodes.ServiceRequest)
                    {
                        wfInstance.WorkflowTaskCode = WFStatus.Confirmed;
                    }

                    wfInstance.ModifiedBy = _contextUserId;
                    wfInstance.ModifiedDate = DateTime.Now;
                    wfInstance.EntityID = _entityid;
                    CompanyVO nextStepCompany = workflowEntity.GetCompanyDetails(_task.NextStep.Value);
                    wfInstance.UserType = nextStepCompany.UserType;
                    wfInstance.UserTypeId = nextStepCompany.UserTypeId;

                    wfInstancedata.Remarks = workflowEntity.Remarks;
                    wfInstancedata.ModifiedBy = _contextUserId;
                    wfInstancedata.ModifiedDate = DateTime.Now;


                    wfInstance.PortCode = portcode;

                    SaveWorkFlowInstance(wfInstance, wfInstancedata, roles);
                    if (_task.HasNotification == "Y")
                    {
                        _notificationpublisher.Publish(_entityid, wfInstance.ReferenceID, wfInstance.CreatedBy, nextStepCompany, portcode, workFlowTaskCode);
                    }

                    if (workFlowTaskCode == _portconfiguration.GetPortConfiguration(_contextPortCode, ConfigName.WorkflowInitialStatus).ToString())
                    {
                        workflowEntity.SetWorkFlowId(wfInstance.WorkflowInstanceId, portcode);
                    }
                }
            }

        }

        public string GetPrevTask(int WorkflowInstanceId)
        {
            var wfprocess = (from wfp in _unitOfWork.Repository<WorkflowProcess>().Queryable()
                             where wfp.WorkflowInstanceId == WorkflowInstanceId
                             orderby wfp.WorkflowProcessId descending
                             select wfp).FirstOrDefault<WorkflowProcess>();

            return wfprocess.ToTaskCode;
        }

        public WorkflowInstance GetWorkFlowInstance(T workflowEntity)
        {
            List<string> _portCodes = new List<string>();
            _portCodes = workflowEntity.PortCodes.ToList();
            string reference = Convert.ToString(workflowEntity.ReferenceId, CultureInfo.InvariantCulture);
            var wfinstance = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Queryable()
                              where wfi.EntityID == _entityid && wfi.ReferenceID == reference
                              && wfi.PortCode == _contextPortCode
                              select wfi).FirstOrDefault<WorkflowInstance>();
            return wfinstance;
        }

        private WorkflowTask GetWorkflowTask(string taskCode, T workflowEntity)
        {
            var wftask = (from wft in _unitOfWork.Repository<WorkflowTask>().Queryable()
                          where wft.WorkflowTaskCode == taskCode && wft.EntityID == _entityid && wft.PortCode == _contextPortCode

                          select wft).FirstOrDefault<WorkflowTask>();

            if (wftask == null)
            {
                throw new BusinessExceptions("Workflow Tasks are not configured for " + workflowEntity.Entity.EntityName + " request");
            }
            return wftask;

        }

        private List<WorkflowTaskRole> GetWorkFlowStepRoles(string workFlowTaskCode, T workflowEntity, WorkflowTask wftask)
        {

            var roles = (from wfsr in _unitOfWork.Repository<WorkflowTaskRole>().Queryable()
                         join wfi in _unitOfWork.Repository<WorkflowTask>().Queryable()
                         on wfsr.Step equals wfi.Step
                         where wfi.EntityID == _entityid && wfi.Step == wftask.Step && wfi.PortCode == _contextPortCode
                         where wfi.WorkflowTaskCode == workFlowTaskCode && wfsr.EntityID == _entityid && wfsr.PortCode == _contextPortCode
                         select wfsr).ToList();
            if (roles == null || roles.Count == 0)
            {
                throw new BusinessExceptions("Workflow task roles are not configured for " + workflowEntity.Entity.EntityName + " request");
            }
            return roles;
        }

        private string GetToWFtaskCode(WorkflowTask task)
        {
            var wftotask = (from wft in _unitOfWork.Repository<WorkflowTask>().Queryable()
                            join wftr in _unitOfWork.Repository<WorkflowTaskRole>().Queryable()
                            on wft.EntityID equals wftr.EntityID
                            where wft.Step == task.NextStep && wft.PortCode == _contextPortCode && wftr.PortCode == _contextPortCode
                            select wft).FirstOrDefault<WorkflowTask>();
            return wftotask.WorkflowTaskCode;

        }

        private bool SaveWorkFlowInstance(WorkflowInstance instance, WorkflowProcess wfInstancedata, List<WorkflowTaskRole> roles)
        {

            if (instance == null)
                return false;  //TODO: raise Argument null exception here.

            List<UserRole> userRoles = _accountRepository.GetUserRole(_contextUserId);

            int role = 0;
            foreach (var userRole in userRoles)
            {
                if (roles.Exists(r => r.RoleID == userRole.RoleID))
                {
                    role = userRole.RoleID;
                }
            }

            if (role == 0)
            {
                //TODO: Remove following statement. and throw authentication error once anonymous user is setup.
                role = 1;
            }

            if (instance.WorkflowInstanceId == null)
            {
                instance.WorkflowInstanceId = 0;
            }
            if (instance.WorkflowInstanceId > 0)
            {
                wfInstancedata.FromTaskCode = GetPrevTask(instance.WorkflowInstanceId);
            }
            if (instance.WorkflowInstanceId == 0)
            {
                var instanceproc = new WorkflowInstance.WorkflowInstance_Dml_Proc(instance);
                var instanceproc_result = _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(instanceproc);
                wfInstancedata.WorkflowInstanceId = instanceproc.WorkflowInstanceId;
            }
            else
            {
                _unitOfWork.ExecuteSqlCommand("update WorkFlowInstance set WorkflowTaskCode=@p0, ModifiedBy=@p1, ModifiedDate=@p2, UserTypeId = @p3, UserType=@p4 where WorkFlowInstanceId = @p5", instance.WorkflowTaskCode, instance.ModifiedBy, instance.ModifiedDate, instance.UserTypeId, instance.UserType, instance.WorkflowInstanceId);
                wfInstancedata.WorkflowInstanceId = instance.WorkflowInstanceId;
            }

            wfInstancedata.RoleId = role;

            var intanceprocessproc = new WorkflowProcess.WorkflowProcess_Dml_Proc(wfInstancedata);
            var intanceprocess_result = _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(intanceprocessproc);

            return true;
        }

        private WorkflowInstance CheckAndGetThisInstance(WorkflowInstance freshWFInstance)
        {
            var wfinstance = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Queryable()
                              join wfir in _unitOfWork.Repository<WorkflowProcess>().Queryable()
                              on wfi.WorkflowInstanceId equals wfir.WorkflowInstanceId
                              where wfi.EntityID == freshWFInstance.EntityID && wfi.ReferenceID == freshWFInstance.ReferenceID
                              && wfi.PortCode == freshWFInstance.PortCode
                              select wfi).FirstOrDefault<WorkflowInstance>();

            return wfinstance;
        }


    }

}