using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using log4net;
using log4net.Config;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;

namespace IPMS.Repository
{
    public class WorkFlowTaskRepository : IWorkFlowTaskRepository
    {
        protected IUnitOfWork _unitOfWork;
        //  private readonly ILog log;

        public WorkFlowTaskRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            //  log = LogManager.GetLogger(typeof(WorkFlowTaskRepository));
        }

        #region GetWorkFlowTasks
        /// <summary>
        /// To get all workflow Tasks
        /// </summary>
        /// <returns></returns>
        public List<WorkflowTask> GetWorkFlowTasks()
        {
            var WorkFlowTaskDetails = new List<WorkflowTask>();
            //try
            //{
            WorkFlowTaskDetails = (from wft in _unitOfWork.Repository<WorkflowTask>().Query().Tracking(true).Select()
                                   select wft).ToList<WorkflowTask>();
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            //}
            return WorkFlowTaskDetails;
        }
        #endregion

        #region GeCurrentTaskByEntityandReferance
        /// <summary>
        /// To get Current workflowtask from WorkflowInstance based on Entity Code and ReferenceID (Like VCN.,etc)
        /// </summary>
        /// <param name="p_EntityCode"></param>
        /// <param name="p_ReferenceID"></param>
        /// <returns></returns>
        public WorkflowTask GeCurrentTaskByEntityandReferance(string p_EntityCode, string p_ReferenceID)
        {
            var _wftask = new WorkflowTask();
            //try
            //{
            _wftask = (from wftask in _unitOfWork.Repository<WorkflowTask>().Queryable()
                       join wfi in _unitOfWork.Repository<WorkflowInstance>().Queryable()
                           on wftask.WorkflowTaskCode equals wfi.WorkflowTaskCode
                       join e in _unitOfWork.Repository<Entity>().Query().Tracking(true).Select()
                       on wftask.EntityID equals e.EntityID
                       where e.EntityCode == p_EntityCode && wfi.ReferenceID == p_ReferenceID && e.EntityID == wfi.EntityID
                       select wftask).FirstOrDefault<WorkflowTask>();
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            //}

            return _wftask;
        }
        #endregion

        #region GetNextStepTaskByEntityandReferance
        /// <summary>
        /// To get NextStep workflowtask based on Entity Code and ReferenceID (Like VCN.,etc)
        /// </summary>
        /// <param name="p_EntityCode"></param>
        /// <param name="p_ReferenceID"></param>
        /// <returns></returns>
        public WorkflowTask GetNextStepTaskByEntityandReferance(string p_EntityCode, string p_ReferenceID)
        {
            var nextsteptask = new WorkflowTask();
            //try
            //{
            var currtask = GeCurrentTaskByEntityandReferance(p_EntityCode, p_ReferenceID);

            nextsteptask = (from wftask in _unitOfWork.Repository<WorkflowTask>().Queryable()
                            join e in _unitOfWork.Repository<Entity>().Queryable()
                             on wftask.EntityID equals e.EntityID
                            where e.EntityCode == p_EntityCode &&
                            wftask.Step == currtask.NextStep
                            select wftask).FirstOrDefault<WorkflowTask>();
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            //}

            return nextsteptask;
        }
        #endregion

        #region GetWorkflowTaskByEntity
        /// <summary>
        /// To Get Workflow Task Record based on Entity Code
        /// </summary>
        /// <param name="p_EntityCode"></param>
        /// <returns></returns>
        public WorkflowTask GetWorkflowTaskByEntity(string p_EntityCode)
        {
            var taskcode = new WorkflowTask();
            //try
            //{
            taskcode = (from wftask in _unitOfWork.Repository<WorkflowTask>().Queryable()
                        join e in _unitOfWork.Repository<Entity>().Queryable()
                             on wftask.EntityID equals e.EntityID
                        where e.EntityCode == p_EntityCode
                        select wftask).FirstOrDefault<WorkflowTask>();


            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            //}
            return taskcode;
        }
        #endregion

        #region GetRequestStatus
        /// <summary>
        /// To Get the Request Status
        /// </summary>
        /// <param name="p_entitycode"></param>
        /// <param name="p_referenceno"></param>
        /// <returns></returns>
        public int GetRequestStatus(string p_entitycode, string p_referenceno)
        {
            var _entitycode = 0;
            //try
            //{
            _entitycode = (from e in _unitOfWork.Repository<Entity>().Queryable()
                           join w in _unitOfWork.Repository<WorkflowInstance>().Queryable() on e.EntityID equals w.EntityID
                           join sc in _unitOfWork.Repository<SubCategory>().Queryable() on w.WorkflowTaskCode equals sc.SubCatCode
                           join pc in _unitOfWork.Repository<PortConfiguration>().Queryable() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }
                           where e.EntityCode == p_entitycode
                             && w.ReferenceID == p_referenceno
                           select w).Count();
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            //}

            return _entitycode;
        }
        #endregion

        #region GetWorkFlowTaskAction
        /// <summary>
        /// Get Work Flow Task Action
        /// </summary>
        /// <param name="ReferenceID"></param>
        /// <param name="WorkflowInstanceID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public IEnumerable<PendingTaskVO> GetWorkFlowTaskAction(string ReferenceID, int WorkflowInstanceID, int UserID)
        {
            var referenceid = new SqlParameter("@ReferenceID", ReferenceID);
            var workflowInstanceId = new SqlParameter("@WorkflowInstanceId", WorkflowInstanceID);
            var userid = new SqlParameter("@UserID", UserID);

            List<PendingTaskVO> pendingtask = _unitOfWork.SqlQuery<PendingTaskVO>("dbo.usp_WorkFlowTaskAction @ReferenceID, @WorkflowInstanceId,@UserID", referenceid, workflowInstanceId, userid).ToList();

            return pendingtask;
        }
        #endregion

        #region InsertOrUpdateWorkFlowTask
        /// <summary>
        /// Insert Or Update WorkFlow Task Configuration
        /// </summary>
        /// <param name="entityvalue"></param>
        /// <param name="userid"></param>
        /// <param name="portcode"></param>
        /// <param name="isUPdate"></param>
        /// <returns></returns>
        public EntityVO InsertOrUpdateWorkFlowTask(EntityVO entityvalue, int userid, string portcode, bool isUPdate)
        {
            if (entityvalue != null)
            {
                List<WorkFlowTaskUpdateVO> objUpdateWorkflowTask = new List<WorkFlowTaskUpdateVO>();

                objUpdateWorkflowTask = entityvalue.WorkFlowTaskVO.MapToEntityForSetWFTaskRoles();

                int _EntityID = entityvalue.EntityID;

                if (isUPdate)
                {
                    var wftaskdelete =
                        _unitOfWork.ExecuteSqlCommand(
                            "delete dbo.WorkflowTask where EntityID = @p0 and PortCode = @p1", _EntityID, portcode);
                    var wftaskroledelete =
                        _unitOfWork.ExecuteSqlCommand(
                            "delete dbo.WorkflowTaskRole where EntityID = @p0 and PortCode = @p1", _EntityID, portcode);
                }

                foreach (var obj in objUpdateWorkflowTask)
                {
                    obj.EntityID = obj.EntityID;

                    if (obj.HasNotification == "True")
                    {
                        obj.HasNotification = "Y";
                    }
                    else
                    {
                        obj.HasNotification = "N";
                    }

                    if (obj.HasRemarks == "True")
                    {
                        obj.HasRemarks = "Y";
                    }
                    else
                    {
                        obj.HasRemarks = "N";
                    }

                    obj.WorkflowTaskCode = obj.WorkflowTaskCode;
                    obj.ValidityPeriod = obj.ValidityPeriod;
                    obj.Step = obj.Step;
                    obj.APIUrl = obj.APIUrl;
                    obj.NextStep = obj.NextStep;
                    obj.PortCode = portcode;
                    obj.CommaSeperatedRoleIDs = obj.CommaSeperatedRoleIDs;
                    obj.RecordStatus = "A";

                    var Request =
                        _unitOfWork.SqlQuery<WorkFlowTaskUpdateVO>(
                            "usp_SetWorkFlowTasksAndRoles @EntityID=@p0,@WorkflowTaskCode=@p1,@HasNotification=@p2,@ValidityPeriod=@p3,@CommaSeperatedRoleIDs=@p4,@Step=@p5,@NextStep=@p6,@APIUrl=@p7,@PortCode=@p8,@CreatedBy=@p9,@ModifiedBy=@p10,@RecordStatus=@p11,@HasRemarks=@p12",
                            obj.EntityID, obj.WorkflowTaskCode, obj.HasNotification, obj.ValidityPeriod,
                            obj.CommaSeperatedRoleIDs, obj.Step, obj.NextStep, obj.APIUrl, portcode, userid, userid,
                            obj.RecordStatus, obj.HasRemarks).ToList();
                }
            }
            return entityvalue;
        }
        #endregion

        #region GetWorkFlowTask
        /// <summary>
        /// Get WorkFlowTasks by Portcode
        /// </summary>
        /// <param name="portcode"></param>
        /// <returns></returns>
        public List<EntityVO> GetWorkFlowTask(string portcode)
        {
            var _portcode = new SqlParameter("@portcode", portcode);
            List<usp_GetWorkFlowTaskVO> wftask = _unitOfWork.SqlQuery<usp_GetWorkFlowTaskVO>("dbo.usp_GetWorkFlowTask @portcode", _portcode).ToList();

            foreach (var wftasklist in wftask)
            {
                if (wftasklist.CommaSeperatedRoleIDs != null)
                {
                    wftasklist.arrayRoles = wftasklist.CommaSeperatedRoleIDs.Split(',').ToList();
                }
            }

            var WorkFlowTaskRoleVOdata = (from re in _unitOfWork.Repository<WorkflowTaskRole>().Queryable()
                                          where re.PortCode == portcode
                                          select re).MapToDTO().ToList();

            var entitylist = (from wft in wftask
                              group wft by new { wft.EntityID, wft.EntityName } into g
                              select new EntityVO
                              {
                                  EntityID = g.Key.EntityID,
                                  EntityName = g.Key.EntityName,
                                  WorkFlowTaskVO = g.Select(i =>
                                      new WorkFlowTaskVO
                                      {
                                          EntityID = i.EntityID,
                                          WorkflowTaskCode = i.WorkflowTaskCode,
                                          HasNotification = i.HasNotification,
                                          HasRemarks = i.HasRemarks,
                                          ValidityPeriod = i.ValidityPeriod,
                                          arrayRoles = i.arrayRoles,
                                          Step = i.Step ?? 0,
                                          NextStep = i.NextStep,
                                          APIUrl = i.APIUrl,
                                          PortCode = i.PortCode
                                      }).OrderBy(t => t.Step).ToList(),
                                  WorkFlowTaskRoleVO = WorkFlowTaskRoleVOdata.Where(t => t.EntityID == g.Key.EntityID).ToList()
                              }).ToList();

            return entitylist;
        }
        #endregion

        #region GetWorkFlowTaskStatus
        /// <summary>
        /// Get Work Flow Task Status
        /// </summary>
        /// <param name="ReferenceID"></param>
        /// <param name="WorkflowInstanceID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public PendingTaskVO GetWorkFlowTaskStatus(string ReferenceID, int WorkflowInstanceId, string TaskCode)
        {
            var referenceid = new SqlParameter("@ReferenceID", ReferenceID);
            var workflowInstanceId = new SqlParameter("@WorkflowInstanceId", WorkflowInstanceId);

            PendingTaskVO pend = new PendingTaskVO();

            List<PendingTaskVO> wfstatus = _unitOfWork.SqlQuery<PendingTaskVO>("dbo.usp_GetWorkflowTaskStatus @ReferenceID, @WorkflowInstanceId", referenceid, workflowInstanceId).ToList();

            foreach (var task in wfstatus)
            {
                if (task.WorkflowTaskCode == TaskCode) {
                    task.WFTaskStatus = true;
                    pend = task;
                }
            }

            return pend;

        }
        #endregion


    }
}
