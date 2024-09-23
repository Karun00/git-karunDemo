using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using System.Globalization;
using System.Security.Policy;
using IPMS.Core.Repository.Exceptions;
namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MobileScheduledTasksService : ServiceBase, IMobileScheduledTasksService
    {
        private IEntityRepository _entity;
        private ISubCategoryRepository _subcategoryRepository;
        private INotificationPublisher _notificationpublisher;
        // private IPortConfigurationRepository _portConfigurationRepository;
        private IMobileScheduledTasksRepository _mobileScheduledRepository;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository;
        private IResourceAllocationRepository _resourceallocationRepository;
        private INotificationPublisher notificationpublisher;
        private IUserRepository userRepository;


        private const string _entityCode = EntityCodes.MobileTask_Code;

        public MobileScheduledTasksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            //  _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _mobileScheduledRepository = new MobileScheduledTasksRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _resourceallocationRepository = new ResourceAllocationRepository(_unitOfWork);
            notificationpublisher = new NotificationPublisher(_unitOfWork);
            userRepository = new UserRepository(_unitOfWork);
        }

        public MobileScheduledTasksService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            //  _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _mobileScheduledRepository = new MobileScheduledTasksRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _resourceallocationRepository = new ResourceAllocationRepository(_unitOfWork);
            notificationpublisher = new NotificationPublisher(_unitOfWork);
            userRepository = new UserRepository(_unitOfWork);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ScheduledTasksVO> GetScheduledTasks()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var userid = new SqlParameter("@puserid", _UserId);
                var portcode = new SqlParameter("@portcode", _PortCode);

                var scheduledTasks = _unitOfWork.SqlQuery<ScheduledTasksVO>("dbo.usp_GetScheduledTasksDetails @portcode, @puserid", portcode, userid).ToList();

                return scheduledTasks;
            });
        }

        public int ModifyScheduledTasks(ResourceAllocationVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Entity entity = GetEntities(_entityCode);
                // var portcode = _PortCode;
                CompanyVO nextStepCompany = GetUserDetails(_UserId);


                string taskremarks = "";

                if (data.Remarks != null)
                {
                    taskremarks = data.Remarks;
                }

                if (data.TaskStatus == "REJT")
                {
                    _unitOfWork.ExecuteSqlCommand("update dbo.ResourceAllocation SET AcknowledgeDate = @p0,Remarks= @p1, TaskStatus = @p2 where ResourceAllocationID = @p3", DateTime.Now, taskremarks, data.TaskStatus, data.ResourceAllocationID);

                    _notificationpublisher.Publish(entity.EntityID, data.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Reject);
                }
                else
                {
                    var resourceAllocationID = new SqlParameter("@ResourseAloID", data.ResourceAllocationID);
                    var remarks = new SqlParameter("@Remarks", taskremarks);
                    var taskstatus = new SqlParameter("@TaskStatus", data.TaskStatus);
                    var operationType = new SqlParameter("@OperationType", data.OperationType);
                    var userId = new SqlParameter("@Usrid", _UserId);

                    _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>("dbo.usp_MobileTaskAcknowledgement @ResourseAloID, @Remarks, @TaskStatus, @OperationType, @Usrid", resourceAllocationID, remarks, taskstatus, operationType, userId).ToList();
                }
                return data.ResourceAllocationID;
            });
        }

        public List<String> GetMobileScheduledTaskViewDetails(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var resourceAllocationID = new SqlParameter("@ResourceAllocationID", id);

                var scheduledTasksView = _unitOfWork.SqlQuery<ScheduledTasksViewVO>("dbo.usp_GetMobileScheduledTaskView @ResourceAllocationID", resourceAllocationID).ToList();

                return scheduledTasksView.MapToDTO();
            });
        }

        public List<ScheduledTaskExecutionVO> GetMobileResourceAllowTaskExecution(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var resourceAllocationID = new SqlParameter("@ResourceAllocationID", id);

                var scheduledTaskexecutionView = _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>("dbo.usp_MobileResourceAlloTaskExecution @ResourceAllocationID", resourceAllocationID).ToList();

                return scheduledTaskexecutionView;
            });
        }

        public int PostMobileScheduledTaskExecution(ScheduledTaskExecutionVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                if (_UserId != 0 && _UserId != null)
                {
                    var resourceAllocationID = new SqlParameter("@ResourseAloID", data.ResourceAllocationID);
                    var ValPKID = new SqlParameter("@Valpkid", data.ValPKID);
                    var fieldName = new SqlParameter("@FieldName ", data.FieldName);
                    var fieldValue = new SqlParameter("@FieldValue", data.FieldValue);
                    var operationType = new SqlParameter("@OperationType", data.OperationType);
                    var userId = new SqlParameter("@Usrid ", _UserId);

                    _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>(" dbo.usp_MobileTaskExecution @ResourseAloID, @Valpkid, @FieldName, @FieldValue, @OperationType , @Usrid ", resourceAllocationID, ValPKID, fieldName, fieldValue, operationType, userId).ToList();

                    if (data.OperationType == ServiceTypeCode.Pilotage)
                    {
                        var resourceid = Convert.ToString(data.ResourceAllocationID, CultureInfo.InvariantCulture);
                        var resourcedetails = _resourceallocationRepository.GetServiceRecordingDetailsForMobile(data.ResourceAllocationID);
                        var entityid = _entity.GetEntitiesNotification(EntityCodes.ServiceRecording).EntityID;
                        var nextStepCompany = userRepository.GetUserDetails(_UserId);
                        if (resourcedetails.MovementType == MovementTypes.ARRIVAL)
                        {
                            if (resourcedetails.PilotOff != null && resourcedetails.PilotOnBoard != null && resourcedetails.EndTime == null)
                                {
                                    notificationpublisher.Publish(entityid, resourceid, _UserId, nextStepCompany, _PortCode, WFStatus.New);
                                }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SAILING)
                        {
                            if (resourcedetails.PilotOff != null && resourcedetails.PilotOnBoard != null && resourcedetails.EndTime == null)
                                {
                                    notificationpublisher.Publish(entityid, resourceid, _UserId, nextStepCompany, _PortCode, WFStatus.Resubmit);
                                }
                         }
                    }
                    else if (data.OperationType == ServiceTypeCode.BerthMaster)
                    {
                        var resourceid = Convert.ToString(data.ResourceAllocationID);
                        var resourcedetails = _resourceallocationRepository.GetServiceRecordingDetailsForMobile(data.ResourceAllocationID);
                        var entityid = _entity.GetEntitiesNotification(EntityCodes.ServiceRecording).EntityID;
                        var nextStepCompany = userRepository.GetUserDetails(_UserId);
                        if (resourcedetails.MovementType == MovementTypes.ARRIVAL)
                        {
                            if (resourcedetails.LastLineIn != null && resourcedetails.FirstLineIn != null)
                                {
                                    if (resourcedetails.EndTime1 == null)
                                    {
                                        notificationpublisher.Publish(entityid, resourceid, _UserId, nextStepCompany,_PortCode, WFStatus.Approved);
                                    }
                                }
                            }
                        else if (resourcedetails.MovementType == MovementTypes.SHIFTING)
                        {
                            if (resourcedetails.LastLineIn != null && resourcedetails.FirstLineIn != null && resourcedetails.FirstLineOut != null && resourcedetails.LastLineOut != null)
                            {
                                if (resourcedetails.EndTime1 == null)
                                {
                                    notificationpublisher.Publish(entityid, resourceid, _UserId, nextStepCompany, _PortCode, WFStatus.Verified);
                                }
                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.WARPING)
                        {
                            if (resourcedetails.LastLineIn != null && resourcedetails.FirstLineIn != null && resourcedetails.FirstLineOut != null && resourcedetails.LastLineOut != null)
                            {
                                if(resourcedetails.EndTime1 == null)
                            {
                                notificationpublisher.Publish(entityid, resourceid, _UserId, nextStepCompany, _PortCode,WFStatus.Confirmed);
                            }
                        }
                    }
                        else if (resourcedetails.MovementType == MovementTypes.SAILING)
                        {
                            if (resourcedetails.FirstLineOut != null && resourcedetails.LastLineOut != null && resourcedetails.EndTime1 == null)
                                {
                                    notificationpublisher.Publish(entityid, resourceid, _UserId, nextStepCompany, _PortCode, WFStatus.ResubmitUpdate);
                                }
                        }
                    }

                    return data.ResourceAllocationID;
                }
                else
                {
                    throw new BusinessExceptions(BusinessExceptions.SessionTimeOut);
                }
            });

        }
        // for saving pilotage saving
        public int PostPilotageTaskExecution(PilotageServiceRecordingVO data)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                if (_UserId != 0 && _UserId != null)
                {
                    PilotageServiceRecording pilotageServiceRecordingobj;
                    pilotageServiceRecordingobj = data.MapToEntity();
                    pilotageServiceRecordingobj.PilotageServiceRecordingID = data.ValPKID;
                    pilotageServiceRecordingobj.ResourceAllocation = null;
                    pilotageServiceRecordingobj.User = null;
                    pilotageServiceRecordingobj.User1 = null;
                    pilotageServiceRecordingobj.RecordStatus = "A";
                    pilotageServiceRecordingobj.CreatedBy = _UserId;
                    pilotageServiceRecordingobj.CreatedDate = DateTime.Now;
                    pilotageServiceRecordingobj.ModifiedBy = _UserId;
                    pilotageServiceRecordingobj.ModifiedDate = DateTime.Now;
                    pilotageServiceRecordingobj.ObjectState = ObjectState.Modified;

                    _unitOfWork.Repository<PilotageServiceRecording>().Update(pilotageServiceRecordingobj);

                    _unitOfWork.SaveChanges();


                    var resourceAllocationID = new SqlParameter("@ResourseAloID", data.ResourceAllocationID);
                    var ValPKID = new SqlParameter("@Valpkid", data.ValPKID);
                    var operationType = new SqlParameter("@OperationType", data.OperationType);
                    var userId = new SqlParameter("@Usrid ", _UserId);

                    _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>("dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ", resourceAllocationID, ValPKID, operationType, userId).ToList();

                    return data.ResourceAllocationID;
                }
                else
                {
                    throw new BusinessExceptions(BusinessExceptions.SessionTimeOut);
                }
            });
        }

        public List<SubCategoryCodeNameVO> GetBerthingSide()
        {

            return ExecuteFaultHandledOperation(() =>
            {
                var berthingSide = _subcategoryRepository.DockTypes();
                return berthingSide.MapToDtoCodeName();
            });
        }



        //for berth saving
        public int PostBerthingDetails(ShiftingBerthingTaskExecutionVO data)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                if (_UserId != 0 && _UserId != null)
                {
                    ShiftingBerthingTaskExecution shiftingBerthingTaskExecution;
                    shiftingBerthingTaskExecution = data.MapToEntity();
                    shiftingBerthingTaskExecution.BerthingTaskExecutionID = data.ValPKID;
                    shiftingBerthingTaskExecution.RecordStatus = "A";
                    shiftingBerthingTaskExecution.CreatedBy = _UserId;
                    shiftingBerthingTaskExecution.CreatedDate = DateTime.Now;
                    shiftingBerthingTaskExecution.ModifiedBy = _UserId;
                    shiftingBerthingTaskExecution.ModifiedDate = DateTime.Now;
                    shiftingBerthingTaskExecution.ObjectState = ObjectState.Modified;
                    CompanyVO nextStepCompany = GetUserDetails(_UserId);
                    _unitOfWork.Repository<ShiftingBerthingTaskExecution>().Update(shiftingBerthingTaskExecution);

                    _unitOfWork.SaveChanges();

                    var resourceAllocationID = new SqlParameter("@ResourseAloID", data.ResourceAllocationID);
                    var ValPKID = new SqlParameter("@Valpkid", data.ValPKID);
                    var operationType = new SqlParameter("@OperationType", data.OperationType);
                    var userId = new SqlParameter("@Usrid ", _UserId);

                    _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>("dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ", resourceAllocationID, ValPKID, operationType, userId).ToList();

                    if (data.MomentType == MovementTypes.SAILING)
                    {
                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.MobileScheduleTaskExecution).EntityID, shiftingBerthingTaskExecution.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.ScheduleTaskSailing);
                    }
                    if (data.MomentType == MovementTypes.SHIFTING)
                    {
                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.MobileScheduleTaskExecution).EntityID, shiftingBerthingTaskExecution.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.ScheduleTaskShifting);
                    }
                    return data.ResourceAllocationID;
                }
                else
                {
                    throw new BusinessExceptions(BusinessExceptions.SessionTimeOut);
                }
            });
        }
        //for tugorworkboat saving
        public int PostTugOrWorkBoatTaskExecution(OtherServiceRecordingVO data)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                if (_UserId != 0 && _UserId != null)
                {
                    OtherServiceRecording otherServiceRecordingObj;
                    otherServiceRecordingObj = data.MapToEntity();
                    otherServiceRecordingObj.OtherServiceRecordingID = data.ValPKID;
                    otherServiceRecordingObj.ResourceAllocation = null;
                    otherServiceRecordingObj.User = null;
                    otherServiceRecordingObj.User1 = null;
                    otherServiceRecordingObj.RecordStatus = "A";
                    otherServiceRecordingObj.CreatedBy = _UserId;
                    otherServiceRecordingObj.CreatedDate = DateTime.Now;
                    otherServiceRecordingObj.ModifiedBy = _UserId;
                    otherServiceRecordingObj.ModifiedDate = DateTime.Now;
                    otherServiceRecordingObj.ObjectState = ObjectState.Modified;

                    _unitOfWork.Repository<OtherServiceRecording>().Update(otherServiceRecordingObj);

                    _unitOfWork.SaveChanges();


                    var resourceAllocationID = new SqlParameter("@ResourseAloID", data.ResourceAllocationID);
                    var ValPKID = new SqlParameter("@Valpkid", data.ValPKID);
                    var operationType = new SqlParameter("@OperationType", data.OperationType);
                    var userId = new SqlParameter("@Usrid ", _UserId);

                    _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>("dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ", resourceAllocationID, ValPKID, operationType, userId).ToList();

                    if (data.Extend)
                    {
                        //int servicereferenceid = _unitOfWork.SqlQuery<int>("select ServiceReferenceID from ResourceAllocation where ResourceAllocationID = {0}", data.ResourceAllocationID).FirstOrDefault();

                        var resourceallocation = _unitOfWork.SqlQuery<ResourceAllocation>("select * from ResourceAllocation where ResourceAllocationID = {0}", data.ResourceAllocationID).FirstOrDefault();

                        var slotconfig = _suppServiceResourceAllocRepository.GetSlotConfiguration(Convert.ToDateTime(resourceallocation.AllocationDate, CultureInfo.InvariantCulture), _PortCode);

                        int shiftid = slotconfig.Find(s => s.SlotPeriod == resourceallocation.AllocSlot).ShiftID;
                        int newshiftid;

                        List<int> shiftids = slotconfig.Select(ss => ss.ShiftID).Distinct().ToList();
                        int shiftindex = shiftids.FindIndex(s => s == shiftid);
                        int shiftidscount = shiftids.Count - 1;
                        DateTime date;
                        var slot = new ResourceSlotVO();

                        if (shiftindex < shiftidscount)
                        {
                            newshiftid = shiftids.ElementAt(shiftindex + 1);
                            slot = slotconfig.Find(s => s.ShiftID == newshiftid);
                            date = slot.AllocationDate;
                        }
                        else
                        {
                            newshiftid = shiftids.ElementAt(0);
                            slot = slotconfig.Find(s => s.ShiftID == newshiftid);
                            date = slot.AllocationDate.AddDays(1);
                        }

                        ResourceAllocation resrcAlcObj = new ResourceAllocation();
                        resrcAlcObj.OperationType = ServiceTypeCode.FloatingCrane;
                        resrcAlcObj.TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled;
                        resrcAlcObj.ServiceReferenceType = ServiceReferenceType.SupplementoryService;
                        resrcAlcObj.ServiceReferenceID = resourceallocation.ServiceReferenceID;
                        resrcAlcObj.RecordStatus = RecordStatus.Active;
                        resrcAlcObj.CreatedBy = _UserId;
                        resrcAlcObj.CreatedDate = DateTime.Now;
                        resrcAlcObj.ModifiedBy = _UserId;
                        resrcAlcObj.ModifiedDate = DateTime.Now;
                        resrcAlcObj.ObjectState = ObjectState.Added;
                        resrcAlcObj.AllocSlot = slot.SlotPeriod;
                        resrcAlcObj.AllocationDate = date;

                        _unitOfWork.Repository<ResourceAllocation>().Insert(resrcAlcObj);

                        _unitOfWork.SaveChanges();
                    }

                    return data.ResourceAllocationID;
                }
                else
                {
                    throw new BusinessExceptions(BusinessExceptions.SessionTimeOut);
                }
            });
        }


        public Entity GetEntities(string entityCode)
        {
            return _mobileScheduledRepository.GetEntities(entityCode);
        }


        public CompanyVO GetUserDetails(int UserId)
        {
            return _mobileScheduledRepository.GetUserDetails(_UserId);
        }
    }
}
