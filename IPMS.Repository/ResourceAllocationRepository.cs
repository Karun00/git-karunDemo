using System.Collections.Generic;
using IPMS.Domain.Models;
using Core.Repository;
using System.Linq;
using IPMS.Domain.DTOS;
using IPMS.Domain.ValueObjects;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using IPMS.Domain;
using System.Globalization;
using IPMS.Core.Repository.Exceptions;

namespace IPMS.Repository
{
    public class ResourceAllocationRepository : IResourceAllocationRepository
    {
        protected IUnitOfWork _unitOfWork;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository;
        private IBerthPlanningRepository _berthPlanningRepository;
        private IUserRepository _userRepository;
        private IEntityRepository _entityRepository;

        public ResourceAllocationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _berthPlanningRepository = new BerthPlanningRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _entityRepository = new EntityRepository(_unitOfWork);
        }

        #region GetresourceAloocationdetails
         /// <summary>
        /// Method to Get ResourceAllocationdetails
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="vcn"></param>
        /// <param name="vesselName"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public List<ResourceAllocationVO> GetResourceAllocationDetails(string portCode, string vcn, string vesselName, string resourceName)
        {
            string[] vesselNames = vesselName.Split('-');
            string[] resourceNames = resourceName.Split('-');
            var portcode = new SqlParameter("@portcode", portCode);
            var _vcn = new SqlParameter("@VCN", vcn);
            var _vesselName = new SqlParameter("@VesselName", vesselNames[0].ToString().Trim());
            var _resourceName = new SqlParameter("@ResourceName", resourceNames[0].ToString().Trim());
            var sn = _unitOfWork.SqlQuery<ResourceAllocationVO>("dbo.usp_GetServiceRecordinDetails_New @portcode, @VCN, @VesselName, @ResourceName", portcode, _vcn, _vesselName, _resourceName).ToList();
            return sn;
        }

        #endregion

        #region GetWaterdetails
        /// <summary>
        /// Method to Get Waterdetails
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="vcn"></param>
        /// <param name="vesselName"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        /// 

#endregion

        public List<OtherServiceRecordingVO> GetWaterDetailsList(string resourceAllocationID, string action)
        {
            int ID = Convert.ToInt32(resourceAllocationID);
            string RecCount = Convert.ToString((from p in _unitOfWork.Repository<OtherServiceRecording>().Queryable().Where(p => p.ResourceAllocationID == ID && p.RecordStatus == "A")
                         select p).Count());
            var resources = (from p in _unitOfWork.Repository<OtherServiceRecording>().Queryable().Where(p => p.ResourceAllocationID == ID && p.RecordStatus == "A")
                                 .OrderByDescending(p => p.CreatedDate)
                             select new OtherServiceRecordingVO
                             {
                                 StartTime = p.StartTime,
                                 EndTime = p.EndTime,
                                 WaitingEndTime = p.WaitingEndTime,
                                 WaitingStartTime = p.WaitingStartTime,
                                 OpeningMeterReading = p.OpeningMeterReading,
                                 ClosingMeterReading = p.ClosingMeterReading,
                                 TotalDispensed = p.TotalDispensed,
                                 OtherServiceRecordingID = p.OtherServiceRecordingID,
                                 ResourceAllocationID = p.ResourceAllocationID,
                                 DelayReason=p.DelayReason,
                                 BerthCode=p.BerthCode,
                                 Remarks=p.Remarks,
                                 Deficiencies=p.Deficiencies,                                 
                                 IsCompleted=p.IsCompleted=="Y"?true:false,
                                 MeterNo=p.MeterNo
                             }).ToList<OtherServiceRecordingVO>();

            var Status = (from p in _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(p => p.ResourceAllocationID == ID && p.RecordStatus == "A")
                              select p.TaskStatus).FirstOrDefault();
            
                int count = 0;
                foreach (var item in resources)
                {
                    
                    if (Status == "VERF" || action=="View" || action=="Verify")
                    {
                        item.IsTop = false;
                    }
                    else
                    {
                    if (count == 0)
                    {
                        item.IsTop = true;
                        count++;
                    }
                    else
                    {
                        item.IsTop = false;
                    }
                }
            }

            return resources;
        }  

     



        #region GetresourceAllocationdetails_VCN
        /// <summary>
        /// Method to Get ResourceAllocationdetails_VCN
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>

        public List<ResourceAllocationVO> GetResourceAllocationDetailsByVCN(string portCode, string vcn)
        {
            var portcode = new SqlParameter("@portcode", portCode);
            var _vcn = new SqlParameter("@VCN", vcn);
            var sn = _unitOfWork.SqlQuery<ResourceAllocationVO>("dbo.usp_GetServiceRecordinDetails_VCN  @portcode,@VCN", portcode, _vcn).ToList();
            sn.ForEach(x => { if (x.TaskStatusName == "Verify") { x.TaskStatusName = "Verified"; } });
            return sn;
        }

        #endregion
        #region GetresourceAllocation_ResourceAllocID
        /// <summary>
        /// Method to Get ResourceAllocation_ResourceAllocID
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>

        public ResourceAllocationVO GetResourceAllocationByResourceAllocationId(string portCode, string strResourceAllocationId)
        {
            ResourceAllocationVO objResourceAllocationVO = new ResourceAllocationVO();
            int k = Convert.ToInt32(strResourceAllocationId, CultureInfo.InvariantCulture);
            var portcode = new SqlParameter("@portcode", portCode);
            var aloid = new SqlParameter("@Allid", k);
            var sn = _unitOfWork.SqlQuery<ResourceAllocationVO>("dbo.usp_GetServiceRecordinDetails_Allid  @portcode, @Allid", portcode, aloid).ToList();
            objResourceAllocationVO = sn.FirstOrDefault();
            return objResourceAllocationVO;
        }

        #endregion

        #region GetresourceAloocationformdetails
        /// <summary>
        /// Method to Get ResourceAllocationformdetails
        /// </summary>
        /// <param name="resourcedtls"></param>
        /// <returns></returns>
        public ResourceAllocation GetResourceAloocationFormDetails(ResourceAllocation resourcedtls)
        {
            var resources = (from t in _unitOfWork.Repository<ResourceAllocation>().Query().Include(t => t.SubCategory).Select()
                             select t).FirstOrDefault();
            return resources;
        }
        #endregion

        #region GetResourceAllocationByDate
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get list of resources based on VCN and date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<ResourceAllocationVO> GetResourceAllocationByDate(string date, string portCode)
        {
            var suppResourceAllocation = (from s in _unitOfWork.Repository<SuppServiceRequest>().Query().Select()
                                          join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on s.WorkflowInstanceID equals w.WorkflowInstanceId
                                          join a in _unitOfWork.Repository<ArrivalNotification>().Query().Select() on s.VCN equals a.VCN
                                          join v in _unitOfWork.Repository<Vessel>().Query().Select() on a.VesselID equals v.VesselID
                                          join r in _unitOfWork.Repository<ResourceAllocation>().Query().Select() on s.SuppServiceRequestID equals r.ServiceReferenceID
                                          join su in _unitOfWork.Repository<SubCategory>().Query().Select() on r.TaskStatus equals su.SubCatCode
                                          join u in _unitOfWork.Repository<User>().Query().Select() on r.ResourceID equals u.UserID
                                          join e in _unitOfWork.Repository<Employee>().Query().Select() on u.UserTypeID equals e.EmployeeID
                                          where w.WorkflowTaskCode == WFStatus.Approved && s.ServiceType == ServiceTypeCode.WaterService
                                          && s.FromDate.ToShortDateString() == date && r.ServiceReferenceType == ServiceReferenceType.SupplementoryService && a.PortCode == portCode

                                          select new ResourceAllocationVO
                                          {
                                              VCN = s.VCN,
                                              ServiceReferenceID = r.ServiceReferenceID,
                                              ResourceID = r.ResourceID,
                                              ResourceAllocationID = r.ResourceAllocationID,
                                              ServiceReferenceType = r.ServiceReferenceType,
                                              ServiceTypeCode = r.OperationType,
                                              ResourceType = r.ResourceType,
                                              StartTime = r.StartTime != null ? r.StartTime : null,
                                              EndTime = r.EndTime != null ? r.EndTime : null,
                                              TaskStatus = r.TaskStatus,
                                              RecordStatus = r.RecordStatus,
                                              CreatedBy = r.CreatedBy,
                                              CreatedDate = r.CreatedDate,
                                              ModifiedBy = r.ModifiedBy,
                                              ModifiedDate = r.ModifiedDate,
                                              AllocSlot = r.AllocSlot,
                                              VesselName = v.VesselName,
                                              FirstName = e.FirstName,
                                              LastName = e.LastName,
                                              Name = e.FirstName + " " + e.LastName
                                          }).ToList();

            return suppResourceAllocation;
        }
        #endregion

        #region GetResourcesByDateAndServiceReferenceType
        /// <summary>
        /// Method to Get ResourcesByDateAndServiceReferenceType
        /// </summary>
        /// <param name="date"></param>
        /// <param name="serviceReferenceType"></param>
        /// <returns></returns>
        public List<ResourceAllocationVO> GetResourcesByDateAndServiceReferenceType(DateTime date, string serviceReferenceType)
        {
            var resourceallocationusers = (from ra in _unitOfWork.Repository<ResourceAllocation>().Query().Select()
                                           .Where(r => r.ServiceReferenceType == serviceReferenceType && Convert.ToDateTime(r.AllocationDate, CultureInfo.InvariantCulture).Date == date.Date)
                                           select new ResourceAllocationVO
                                           {
                                               ResourceID = ra.ResourceID,
                                               ServiceReferenceType = ra.ServiceReferenceType,
                                               AllocSlot = ra.AllocSlot,
                                               AllocationDate = Convert.ToDateTime(ra.AllocationDate, CultureInfo.InvariantCulture)
                                           }).ToList();

            return resourceallocationusers;
        }
        #endregion

        #region GetResourceAllocationformDetails
        /// <summary>
        /// Method to Get ResourceAllocationformDetails
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ResourceAllocationVO GetResourceAllocationFormDetails(ResourceAllocationVO resource)
        {
            ResourceAllocation resourcedtls = new ResourceAllocation();
            resourcedtls = resource.MapToEntity();

            if (resourcedtls.OperationType == ServiceTypeCode.BerthMaster || resourcedtls.OperationType == ServiceTypeCode.Shifting)
            {
                var resources = (from t in _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ResourceAllocationID == resource.ResourceAllocationID)
                                     .Include(t => t.ShiftingBerthingTaskExecutions)
                                 select t).FirstOrDefault();
                resources.MovementDateTime = resource.MovementDateTime;
                return resources.MapToDTO(resource.MovementType);
            }
            else if (resourcedtls.OperationType == ServiceTypeCode.Pilotage || resourcedtls.OperationType == ServiceTypeCode.PilotBoatorHelicopterService)
            {
                var resources = (from t in _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ResourceAllocationID == resource.ResourceAllocationID)
                                     .Include(t => t.PilotageServiceRecordings)
                                 select t).FirstOrDefault();
                resources.MovementDateTime = resource.MovementDateTime;
                return resources.MapToDTO(resource.MovementType);
            }
            else
            {
                var resources = (from t in _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ResourceAllocationID == resource.ResourceAllocationID)
                                     .Include(t => t.OtherServiceRecordings)
                                 select t).FirstOrDefault();
                resources.MovementDateTime = resource.MovementDateTime;

                var Mappedresources = resources.MapToDTO(resource.MovementType);
               
                if (resourcedtls.OperationType == "WTST" && string.IsNullOrEmpty(Mappedresources.OtherServiceRecording.BerthKey))
                {
                    var SupRef = (from t in _unitOfWork.Repository<SuppServiceRequest>().Queryable().Where(r => r.SuppServiceRequestID == resource.ServiceReferenceID)
                                  select t).FirstOrDefault();
                    Mappedresources.OtherServiceRecording.BerthKey = SupRef.BerthCode != null ? SupRef.PortCode + "." + SupRef.QuayCode + "." + SupRef.BerthCode : null;                    

                }
                return Mappedresources;
            }
        }
        #endregion

     

        #region UpdateResourceAllocationformDetails
        /// <summary>
        /// Method to Update ResourceAllocationformDetails
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ResourceAllocationVO UpdateResourceAllocationFormDetails(ResourceAllocationVO resource, int userId, string portCode)
        {
            if (resource != null)
            {
                ResourceAllocation ResourceAllocation = null;
                resource.ModifiedBy = userId;
                resource.ModifiedDate = DateTime.Now;
                ResourceAllocation = resource.MapToEntity();
                ResourceAllocation.ObjectState = ObjectState.Modified;

                if (ResourceAllocation.OperationType == ServiceTypeCode.Shifting ||
                    ResourceAllocation.OperationType == ServiceTypeCode.BerthMaster)
                {
                    List<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutionsList =
                        ResourceAllocation.ShiftingBerthingTaskExecutions.ToList();

                    if (ShiftingBerthingTaskExecutionsList.Count > 0)
                    {
                        foreach (var shiftingBerthingTaskExecutions in ShiftingBerthingTaskExecutionsList)
                        {
                            ResourceAllocation.StartTime = shiftingBerthingTaskExecutions.StartTime;
                            ResourceAllocation.EndTime = shiftingBerthingTaskExecutions.EndTime;
                            ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Completed;

                            if (shiftingBerthingTaskExecutions.ResourceAllocationID > 0)
                            {
                                shiftingBerthingTaskExecutions.Remarks = resource.Remarks;
                                shiftingBerthingTaskExecutions.Deficiencies = resource.Deficiencies;
                                shiftingBerthingTaskExecutions.ObjectState = ObjectState.Modified;
                                shiftingBerthingTaskExecutions.ModifiedBy = resource.ModifiedBy;
                                shiftingBerthingTaskExecutions.ModifiedDate = DateTime.Now;

                                _unitOfWork.Repository<ShiftingBerthingTaskExecution>()
                                    .Update(shiftingBerthingTaskExecutions);
                            }

                            _unitOfWork.ExecuteSqlCommand(
                                " update dbo.ResourceAllocation SET EndTime =  @p0, StartTime =  @p1, ModifiedDate = @p2 WHERE ResourceAllocationID = @p3 ",
                               //Convert.ToDateTime(ResourceAllocation.EndTime.GetValueOrDefault()), Convert.ToDateTime(ResourceAllocation.StartTime.GetValueOrDefault()),
                              ResourceAllocation.EndTime, ResourceAllocation.StartTime,
                             
                              //(ResourceAllocation.EndTime,"yyyy-MM-dd",null),
                               //(DateTime)(ResourceAllocation.EndTime),(DateTime)(ResourceAllocation.StartTime),
                                shiftingBerthingTaskExecutions.ModifiedDate, resource.ResourceAllocationID);
                            //(DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                            //DateTime.ParseExact (txtPunchDate.Text, "yyyy-MM-dd" , null)
                            _unitOfWork.SaveChanges();
                             
                            var resourceAllocationID = new SqlParameter("@ResourseAloID",
                                ResourceAllocation.ResourceAllocationID);
                            var ValPKID = new SqlParameter("@Valpkid",
                                shiftingBerthingTaskExecutions.BerthingTaskExecutionID);
                            var operationType = new SqlParameter("@OperationType", ResourceAllocation.OperationType);
                            var _userId = new SqlParameter("@Usrid ", userId);

                            _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>(
                                "dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ",
                                resourceAllocationID, ValPKID, operationType, _userId).ToList();
                        }
                    }
                }
                else if (ResourceAllocation.OperationType == ServiceTypeCode.Pilotage ||
                         ResourceAllocation.OperationType == ServiceTypeCode.PilotBoatorHelicopterService)
                {
                    List<PilotageServiceRecording> PilotageServiceRecordingsList =
                        ResourceAllocation.PilotageServiceRecordings.ToList();

                    if (PilotageServiceRecordingsList.Count > 0)
                    {
                        foreach (var PilotageServiceRecordings in PilotageServiceRecordingsList)
                        {
                            ResourceAllocation.StartTime = PilotageServiceRecordings.StartTime;
                            ResourceAllocation.ActualScheduledTime = PilotageServiceRecordings.ActualScheduledTime;
                            ResourceAllocation.EndTime = PilotageServiceRecordings.EndTime;
                            ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Completed;

                            if (PilotageServiceRecordings.ResourceAllocationID > 0)
                            {
                                PilotageServiceRecordings.Remarks = resource.Remarks;
                                PilotageServiceRecordings.Deficiencies = resource.Deficiencies;
                                PilotageServiceRecordings.ObjectState = ObjectState.Modified;
                                PilotageServiceRecordings.ModifiedBy = resource.ModifiedBy;
                                PilotageServiceRecordings.ModifiedDate = DateTime.Now;
                                _unitOfWork.Repository<PilotageServiceRecording>().Update(PilotageServiceRecordings);
                            }

                            var result =
                                _unitOfWork.ExecuteSqlCommand(
                                    "update dbo.ResourceAllocation SET EndTime =  @p0, StartTime =  @p1,ModifiedDate = @p2 , ActualScheduledTime = @p4 WHERE ResourceAllocationID = @p3 ",
                                    ResourceAllocation.EndTime, ResourceAllocation.StartTime,
                                    PilotageServiceRecordings.ModifiedDate, resource.ResourceAllocationID, ResourceAllocation.ActualScheduledTime);

                            _unitOfWork.SaveChanges();

                            var resourceAllocationID = new SqlParameter("@ResourseAloID",
                                ResourceAllocation.ResourceAllocationID);
                            var ValPKID = new SqlParameter("@Valpkid",
                                PilotageServiceRecordings.PilotageServiceRecordingID);
                            var operationType = new SqlParameter("@OperationType", ResourceAllocation.OperationType);
                            var _userId = new SqlParameter("@Usrid ", userId);

                            var scheduledTaskexecutionView =
                                _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>(
                                    "dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ",
                                    resourceAllocationID, ValPKID, operationType, _userId).ToList();
                        }
                    }
                }
                else if (ResourceAllocation.OperationType == ServiceTypeCode.WaterService)
                {
                    List<OtherServiceRecording> OtherServiceRecordingssList =
                        ResourceAllocation.OtherServiceRecordings.ToList();
                    if (OtherServiceRecordingssList.Count > 0)
                    {
                        foreach (var OtherServiceRecording in OtherServiceRecordingssList)
                        {
                            ResourceAllocation.StartTime = OtherServiceRecording.StartTime;
                            ResourceAllocation.EndTime = OtherServiceRecording.EndTime;
                            if (OtherServiceRecording.IsCompleted == "N")
                                ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Accepted;
                            if (OtherServiceRecording.IsCompleted == "Y")
                                ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Completed;
                            if (OtherServiceRecording.ResourceAllocationID > 0)
                            {
                                OtherServiceRecording.RecordStatus = RecordStatus.Active;
                                OtherServiceRecording.DelayReason = resource.OtherServiceRecording.DelayReason;
                                OtherServiceRecording.Remarks = resource.Remarks;
                                OtherServiceRecording.Deficiencies = resource.Deficiencies;
                                OtherServiceRecording.ObjectState = ObjectState.Modified;
                                OtherServiceRecording.CreatedBy = userId;
                                OtherServiceRecording.CreatedDate = DateTime.Now;
                                OtherServiceRecording.ModifiedBy = userId;
                                OtherServiceRecording.ModifiedDate = DateTime.Now;
                                if (OtherServiceRecording.IsCompleted == "Y")
                                    OtherServiceRecording.IsCompleted = "Y";
                                else
                                    OtherServiceRecording.IsCompleted = "N";                              
                                _unitOfWork.Repository<OtherServiceRecording>().Update(OtherServiceRecording);
                            }
                            var count = (from t in _unitOfWork.Repository<OtherServiceRecording>().Query().Select().Where(t => t.ResourceAllocationID == resource.ResourceAllocationID)
                                         select t).Count();
                            if (count == 1)
                            {
                                var result =
                                    _unitOfWork.ExecuteSqlCommand(
                                        " update dbo.ResourceAllocation SET EndTime =  @p0,StartTime=@p1, ModifiedDate = @p2 WHERE ResourceAllocationID = @p3 ",
                                        ResourceAllocation.EndTime, ResourceAllocation.StartTime,
                                        OtherServiceRecording.ModifiedDate, resource.ResourceAllocationID);
                                _unitOfWork.SaveChanges();
                            }
                            else
                            {
                                var result =
                                    _unitOfWork.ExecuteSqlCommand(
                                        " update dbo.ResourceAllocation SET EndTime =  @p0, ModifiedDate = @p1 WHERE ResourceAllocationID = @p2 ",
                                        ResourceAllocation.EndTime,
                                        OtherServiceRecording.ModifiedDate, resource.ResourceAllocationID);
                                _unitOfWork.SaveChanges();
                            }
                            if (ResourceAllocation.TaskStatus == "COMP")
                            {
                                var resourceAllocationID = new SqlParameter("@ResourseAloID",
                                    resource.ResourceAllocationID);
                                var ValPKID = new SqlParameter("@Valpkid", OtherServiceRecording.OtherServiceRecordingID);
                                var operationType = new SqlParameter("@OperationType", ResourceAllocation.OperationType);
                                var _userId = new SqlParameter("@Usrid ", userId);

                                var scheduledTaskexecutionView =
                                    _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>(
                                        "dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ",
                                        resourceAllocationID, ValPKID, operationType, _userId).ToList();
                            }
                            else
                            {
                            }
                            if (resource.OperationType == ServiceTypeCode.WaterService )
                            {
                                var resourceAllocation =
                                    _unitOfWork.Repository<ResourceAllocation>()
                                        .Find(Convert.ToInt32(resource.ResourceAllocationID));

                                _unitOfWork.ExecuteSqlCommand(
                                    " update dbo.SuppServiceRequest SET IsStartTime = 'Y' WHERE SuppServiceRequestID = @p0 ",
                                    ResourceAllocation.ServiceReferenceID);

                                _unitOfWork.ExecuteSqlCommand(
                                  "update dbo.ResourceAllocation SET ResourceID = @p0 WHERE ResourceAllocationID = @p1 ",
                                 resource.ResourceID, resource.ResourceAllocationID);
                            }
                        }
                    }
                }
                else
                {

                    List<OtherServiceRecording> OtherServiceRecordingssList =
                        ResourceAllocation.OtherServiceRecordings.ToList();
                    if (OtherServiceRecordingssList.Count > 0)
                    {
                        foreach (var OtherServiceRecording in OtherServiceRecordingssList)
                        {
                            ResourceAllocation.StartTime = OtherServiceRecording.StartTime;
                            ResourceAllocation.EndTime = OtherServiceRecording.EndTime;                           
                                ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Completed;
                                OtherServiceRecording.ModifiedDate = DateTime.Now;
                            if (OtherServiceRecording.ResourceAllocationID > 0)
                            {
                                OtherServiceRecording.RecordStatus = RecordStatus.Active;
                                OtherServiceRecording.Remarks = resource.Remarks;
                                OtherServiceRecording.Deficiencies = resource.Deficiencies;
                                OtherServiceRecording.ObjectState = ObjectState.Modified;
                                OtherServiceRecording.CreatedBy = userId;
                                OtherServiceRecording.CreatedDate = DateTime.Now;
                                OtherServiceRecording.ModifiedBy = userId;
                                OtherServiceRecording.ModifiedDate = DateTime.Now;                                                            
                                _unitOfWork.Repository<OtherServiceRecording>().Update(OtherServiceRecording);
                            }

                            var result =
                                _unitOfWork.ExecuteSqlCommand(
                                    " update dbo.ResourceAllocation SET EndTime =  @p0,StartTime=@p1, ModifiedDate = @p2 WHERE ResourceAllocationID = @p3 ",
                                    ResourceAllocation.EndTime,ResourceAllocation.StartTime,
                                    OtherServiceRecording.ModifiedDate, resource.ResourceAllocationID);

                            _unitOfWork.SaveChanges();

                            
                                var resourceAllocationID = new SqlParameter("@ResourseAloID",
                                    resource.ResourceAllocationID);
                                var ValPKID = new SqlParameter("@Valpkid", OtherServiceRecording.OtherServiceRecordingID);
                                var operationType = new SqlParameter("@OperationType", ResourceAllocation.OperationType);
                                var _userId = new SqlParameter("@Usrid ", userId);

                                var scheduledTaskexecutionView =
                                    _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>(
                                        "dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ",
                                        resourceAllocationID, ValPKID, operationType, _userId).ToList();
                          
                            if (resource.OperationType == ServiceTypeCode.FloatingCrane)
                            {
                                var resourceAllocation =
                                    _unitOfWork.Repository<ResourceAllocation>()
                                        .Find(Convert.ToInt32(resource.ResourceAllocationID));

                                _unitOfWork.ExecuteSqlCommand(
                                    " update dbo.SuppServiceRequest SET IsStartTime = 'Y' WHERE SuppServiceRequestID = @p0 ",
                                    ResourceAllocation.ServiceReferenceID);

                                _unitOfWork.ExecuteSqlCommand(
                                  "update dbo.ResourceAllocation SET ResourceID = @p0 WHERE ResourceAllocationID = @p1 ",
                                 resource.ResourceID, resource.ResourceAllocationID);
                            }


                            if (OtherServiceRecording.Extend == "Y" && resource.TaskStatus != "COMP")
                            {  
                                ResourceAllocation resrcAlcObj = new ResourceAllocation();
                                resrcAlcObj.OperationType = ServiceTypeCode.FloatingCrane;
                                resrcAlcObj.TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled;
                                resrcAlcObj.ServiceReferenceType = ServiceReferenceType.SupplementoryService;
                                resrcAlcObj.ServiceReferenceID = resource.ServiceReferenceID;
                                resrcAlcObj.RecordStatus = RecordStatus.Active;
                                resrcAlcObj.CreatedBy = userId;
                                resrcAlcObj.CreatedDate = DateTime.Now;
                                resrcAlcObj.ModifiedBy = userId;
                                resrcAlcObj.ModifiedDate = DateTime.Now;
                                resrcAlcObj.ObjectState = ObjectState.Added;
                                resrcAlcObj.AllocSlot = "";
                                resrcAlcObj.AllocationDate = null;
                                _unitOfWork.Repository<ResourceAllocation>().Insert(resrcAlcObj);

                                _unitOfWork.SaveChanges();
                            }
                        }
                    }
                }
            }
            return resource;
        }
        #endregion

        public int CheckMeterNoExists(string meterno,int resourceAllocationID)
        {
            
                var andata = (from t in _unitOfWork.Repository<OtherServiceRecording>().Query().Select()
                              where (t.MeterNo == meterno && t.ResourceAllocationID==resourceAllocationID)
                              select t);
                return andata.Count();
            
        }


        #region SaveWaterAllocationDetails
        /// <summary>
        /// Method to Update ResourceAllocationformDetails
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ResourceAllocationVO SaveWaterAllocationDetails(ResourceAllocationVO resource, int userId, string portCode)
        {
            if (resource != null)
            {
                ResourceAllocation ResourceAllocation = null;
                resource.ModifiedBy = userId;
                resource.ModifiedDate = DateTime.Now;
                ResourceAllocation = resource.MapToEntity();
                ResourceAllocation.ObjectState = ObjectState.Modified;

                
                if (ResourceAllocation.OperationType == ServiceTypeCode.WaterService)
                {
                    List<OtherServiceRecording> OtherServiceRecordingssList =
                         ResourceAllocation.OtherServiceRecordings.ToList();
                    if (OtherServiceRecordingssList.Count > 0)
                    {
                        foreach (var OtherServiceRecording in OtherServiceRecordingssList)
                        {
                            ResourceAllocation.StartTime = OtherServiceRecording.StartTime;
                            ResourceAllocation.EndTime = OtherServiceRecording.EndTime;
                            if (OtherServiceRecording.IsCompleted == "N")
                                ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Accepted;
                            if (OtherServiceRecording.IsCompleted == "Y")
                                ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Completed;
                            if (OtherServiceRecording.ResourceAllocationID == 0)
                            {
                                OtherServiceRecording.ResourceAllocationID = resource.ResourceAllocationID;
                                OtherServiceRecording.MeterNo = OtherServiceRecording.MeterNo;
                                OtherServiceRecording.RecordStatus = RecordStatus.Active;
                                OtherServiceRecording.DelayReason = resource.OtherServiceRecording.DelayReason;
                                OtherServiceRecording.Remarks = resource.Remarks;
                                OtherServiceRecording.Deficiencies = resource.Deficiencies;
                                OtherServiceRecording.ObjectState = ObjectState.Modified;
                                OtherServiceRecording.CreatedBy = userId;
                                OtherServiceRecording.CreatedDate = DateTime.Now;
                                OtherServiceRecording.ModifiedBy = userId;
                                OtherServiceRecording.ModifiedDate = DateTime.Now;
                                if (OtherServiceRecording.IsCompleted == "Y")
                                    OtherServiceRecording.IsCompleted = "Y";
                                else
                                    OtherServiceRecording.IsCompleted = "N";
                                _unitOfWork.Repository<OtherServiceRecording>().Insert(OtherServiceRecording);

                            }
                            _unitOfWork.SaveChanges();
                            var count=(from t in _unitOfWork.Repository<OtherServiceRecording>().Query().Select().Where(t=>t.ResourceAllocationID==resource.ResourceAllocationID)
                             select t).Count();
                            if (count==1)
                            {
                                var result =
                                    _unitOfWork.ExecuteSqlCommand(
                                        " update dbo.ResourceAllocation SET EndTime =  @p0,StartTime=@p1, ModifiedDate = @p2 WHERE ResourceAllocationID = @p3 ",
                                        ResourceAllocation.EndTime, ResourceAllocation.StartTime,
                                        OtherServiceRecording.ModifiedDate, resource.ResourceAllocationID);
                            }
                            else
                            {
                                var result =
                                    _unitOfWork.ExecuteSqlCommand(
                                        " update dbo.ResourceAllocation SET EndTime =  @p0, ModifiedDate = @p1 WHERE ResourceAllocationID = @p2 ",
                                        ResourceAllocation.EndTime,
                                        OtherServiceRecording.ModifiedDate, resource.ResourceAllocationID);
                            }

                            _unitOfWork.SaveChanges();
                            if (ResourceAllocation.TaskStatus == "COMP")
                            {
                                var resourceAllocationID = new SqlParameter("@ResourseAloID",
                                    resource.ResourceAllocationID);
                                var ValPKID = new SqlParameter("@Valpkid", OtherServiceRecording.OtherServiceRecordingID);
                                var operationType = new SqlParameter("@OperationType", ResourceAllocation.OperationType);
                                var _userId = new SqlParameter("@Usrid ", userId);

                                var scheduledTaskexecutionView =
                                    _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>(
                                        "dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ",
                                        resourceAllocationID, ValPKID, operationType, _userId).ToList();
                            }
                            if (resource.OperationType == ServiceTypeCode.WaterService )
                            {
                                var resourceAllocation =
                                    _unitOfWork.Repository<ResourceAllocation>()
                                        .Find(Convert.ToInt32(resource.ResourceAllocationID));

                                _unitOfWork.ExecuteSqlCommand(
                                    " update dbo.SuppServiceRequest SET IsStartTime = 'Y' WHERE SuppServiceRequestID = @p0 ",
                                    ResourceAllocation.ServiceReferenceID);

                                _unitOfWork.ExecuteSqlCommand(
                                  "update dbo.ResourceAllocation SET ResourceID = @p0 WHERE ResourceAllocationID = @p1 ",
                                 resource.ResourceID, resource.ResourceAllocationID);
                            }
                        }
                    }
                }
              
            }
            return resource;
        }
        #endregion

        #region ModifyResourceAllocationformDetails
        /// <summary>
        /// Method to Modify ResourceAllocationformDetails
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ResourceAllocationVO ModifyResourceAllocationFormDetails(ResourceAllocationVO resource, int userId, string portCode)
        {
            if (resource != null)
            {
                ResourceAllocation ResourceAllocation = null;
                resource.ModifiedBy = userId;
                resource.ModifiedDate = DateTime.Now;
                ResourceAllocation = resource.MapToEntity();
                ResourceAllocation.ObjectState = ObjectState.Modified;



                if (ResourceAllocation.OperationType == ServiceTypeCode.BerthMaster)
                {
                    List<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutionsList = ResourceAllocation.ShiftingBerthingTaskExecutions.ToList();

                    if (ShiftingBerthingTaskExecutionsList.Count > 0)
                    {
                        foreach (var shiftingBerthingTaskExecutions in ShiftingBerthingTaskExecutionsList)
                        {
                            ResourceAllocation.StartTime = shiftingBerthingTaskExecutions.StartTime;
                            ResourceAllocation.EndTime = shiftingBerthingTaskExecutions.EndTime;
                            ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Completed;

                            if (shiftingBerthingTaskExecutions.ResourceAllocationID > 0)
                            {
                                shiftingBerthingTaskExecutions.Remarks = resource.Remarks;
                                shiftingBerthingTaskExecutions.Deficiencies = resource.Deficiencies;
                                shiftingBerthingTaskExecutions.ObjectState = ObjectState.Modified;
                                shiftingBerthingTaskExecutions.ModifiedBy = resource.ModifiedBy;
                                shiftingBerthingTaskExecutions.ModifiedDate = DateTime.Now;

                                _unitOfWork.Repository<ShiftingBerthingTaskExecution>().Update(shiftingBerthingTaskExecutions);
                            }

                            _unitOfWork.ExecuteSqlCommand(" update dbo.ResourceAllocation SET EndTime =  @p0, StartTime =  @p1, ModifiedDate = @p2 WHERE ResourceAllocationID = @p3 ", ResourceAllocation.EndTime, ResourceAllocation.StartTime, DateTime.Now, resource.ResourceAllocationID);

                            _unitOfWork.SaveChanges();


                            var resourceAllocationID = new SqlParameter("@ResourseAloID", ResourceAllocation.ResourceAllocationID);
                            var ValPKID = new SqlParameter("@Valpkid", shiftingBerthingTaskExecutions.BerthingTaskExecutionID);
                            var operationType = new SqlParameter("@OperationType", ResourceAllocation.OperationType);
                            var _userId = new SqlParameter("@Usrid ", userId);

                            _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>("dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ", resourceAllocationID, ValPKID, operationType, _userId).ToList();
                        }
                    }
                }
                else if (ResourceAllocation.OperationType == ServiceTypeCode.Pilotage || ResourceAllocation.OperationType == ServiceTypeCode.PilotBoatorHelicopterService)
                {
                    List<PilotageServiceRecording> PilotageServiceRecordingsList = ResourceAllocation.PilotageServiceRecordings.ToList();

                    if (PilotageServiceRecordingsList.Count > 0)
                    {
                        foreach (var PilotageServiceRecordings in PilotageServiceRecordingsList)
                        {
                            //if (ResourceAllocation.OperationType == ServiceTypeCode.Pilotage)
                            //{
                            //    if (PilotageServiceRecordings.PilotOnBoard != null &&
                            //        ResourceAllocation.MovementDateTime != null)
                            //    {
                            //        DateTime pilotDateTime = (DateTime) PilotageServiceRecordings.PilotOnBoard;
                            //        DateTime movementDateTime = (DateTime) ResourceAllocation.MovementDateTime;
                            //        TimeSpan span = pilotDateTime.Subtract(movementDateTime);
                            //        //if (span.TotalMinutes > 31)
                            //        //{
                            //        //    if (string.IsNullOrEmpty(PilotageServiceRecordings.DelayReason))
                            //        //    {
                            //        //        //throw new BusinessExceptions(ErrorMessages.DelayReason);
                            //        //    }
                            //        //    if (string.IsNullOrEmpty(PilotageServiceRecordings.MOPSDelay))
                            //        //    {
                            //        //        throw new BusinessExceptions(ErrorMessages.MOPSDelayReason);
                            //        //    }
                            //        //}
                            //    }
                            //}
                            ResourceAllocation.StartTime = PilotageServiceRecordings.StartTime;
                            ResourceAllocation.EndTime = PilotageServiceRecordings.EndTime;
                            ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Completed;

                            if (PilotageServiceRecordings.ResourceAllocationID > 0)
                            {
                                PilotageServiceRecordings.Remarks = resource.Remarks;
                                PilotageServiceRecordings.Deficiencies = resource.Deficiencies;
                                PilotageServiceRecordings.ObjectState = ObjectState.Modified;
                                PilotageServiceRecordings.ModifiedBy = resource.ModifiedBy;
                                PilotageServiceRecordings.ModifiedDate = DateTime.Now;
                                _unitOfWork.Repository<PilotageServiceRecording>().Update(PilotageServiceRecordings);
                            }

                            var result = _unitOfWork.ExecuteSqlCommand("update dbo.ResourceAllocation SET EndTime =  @p0, StartTime =  @p1, ModifiedDate = @p2 WHERE ResourceAllocationID = @p3 ", ResourceAllocation.EndTime, ResourceAllocation.StartTime, PilotageServiceRecordings.ModifiedDate, resource.ResourceAllocationID);

                            _unitOfWork.SaveChanges();

                            var resourceAllocationID = new SqlParameter("@ResourseAloID", ResourceAllocation.ResourceAllocationID);
                            var ValPKID = new SqlParameter("@Valpkid", PilotageServiceRecordings.PilotageServiceRecordingID);
                            var operationType = new SqlParameter("@OperationType", ResourceAllocation.OperationType);
                            var _userId = new SqlParameter("@Usrid ", userId);

                            var scheduledTaskexecutionView = _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>("dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ", resourceAllocationID, ValPKID, operationType, _userId).ToList();
                        }
                    }
                }
                else
                {
                    List<OtherServiceRecording> OtherServiceRecordingssList = ResourceAllocation.OtherServiceRecordings.ToList();
                    if (OtherServiceRecordingssList.Count > 0)
                    {
                        foreach (var OtherServiceRecording in OtherServiceRecordingssList)
                        {
                            ResourceAllocation.StartTime = OtherServiceRecording.StartTime;
                            ResourceAllocation.EndTime = OtherServiceRecording.EndTime;
                            if (OtherServiceRecording.IsCompleted == "N")
                                ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Accepted;
                                 if (OtherServiceRecording.IsCompleted=="Y")
                            ResourceAllocation.TaskStatus = ResourceAllcationWorkFlowStatus.Completed;

                            if (OtherServiceRecording.ResourceAllocationID > 0)
                            {
                                OtherServiceRecording.RecordStatus = RecordStatus.Active;
                                OtherServiceRecording.Remarks = resource.Remarks;
                                OtherServiceRecording.DelayReason = resource.DelayReason;
                                OtherServiceRecording.Deficiencies = resource.Deficiencies;
                                OtherServiceRecording.ObjectState = ObjectState.Modified;
                                OtherServiceRecording.CreatedBy = userId;
                                OtherServiceRecording.CreatedDate = DateTime.Now;
                                OtherServiceRecording.ModifiedBy = userId;
                                OtherServiceRecording.ModifiedDate = DateTime.Now;
                                if (OtherServiceRecording.IsCompleted == "Y")
                                    OtherServiceRecording.IsCompleted = "Y";
                                else
                                    OtherServiceRecording.IsCompleted = "N";

                                _unitOfWork.Repository<OtherServiceRecording>().Update(OtherServiceRecording);
                            }

                            var result = _unitOfWork.ExecuteSqlCommand(" update dbo.ResourceAllocation SET EndTime =  @p0, StartTime =  @p1, ModifiedDate = @p2 WHERE ResourceAllocationID = @p3 ", ResourceAllocation.EndTime, ResourceAllocation.StartTime, DateTime.Now, resource.ResourceAllocationID);

                            _unitOfWork.SaveChanges();

                            var resourceAllocationID = new SqlParameter("@ResourseAloID", ResourceAllocation.ResourceAllocationID);
                            var ValPKID = new SqlParameter("@Valpkid", OtherServiceRecording.OtherServiceRecordingID);
                            var operationType = new SqlParameter("@OperationType", ResourceAllocation.OperationType);
                            var _userId = new SqlParameter("@Usrid ", userId);

                            var scheduledTaskexecutionView = _unitOfWork.SqlQuery<ScheduledTaskExecutionVO>("dbo.usp_FinalTaskExecution @ResourseAloID, @Valpkid, @OperationType , @Usrid ", resourceAllocationID, ValPKID, operationType, _userId).ToList();


                            if (resource.OperationType == ServiceTypeCode.WaterService || resource.OperationType == ServiceTypeCode.FloatingCrane)
                            {
                                _unitOfWork.ExecuteSqlCommand(
                                  "update dbo.ResourceAllocation SET ResourceID = @p0 WHERE ResourceAllocationID = @p1 ",
                                 resource.ResourceID, resource.ResourceAllocationID);
                            }


                            if (OtherServiceRecording.Extend == "Y" && resource.TaskStatus != "COMP")
                            {
                               
                                ResourceAllocation resrcAlcObj = new ResourceAllocation();
                                resrcAlcObj.OperationType = ServiceTypeCode.FloatingCrane;
                                resrcAlcObj.TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled;
                                resrcAlcObj.ServiceReferenceType = ServiceReferenceType.SupplementoryService;
                                resrcAlcObj.ServiceReferenceID = resource.ServiceReferenceID;
                                resrcAlcObj.RecordStatus = RecordStatus.Active;
                                resrcAlcObj.CreatedBy = userId;
                                resrcAlcObj.CreatedDate = DateTime.Now;
                                resrcAlcObj.ModifiedBy = userId;
                                resrcAlcObj.ModifiedDate = DateTime.Now;
                                resrcAlcObj.ObjectState = ObjectState.Added;
                                resrcAlcObj.AllocSlot = "";
                                resrcAlcObj.AllocationDate = null;

                                _unitOfWork.Repository<ResourceAllocation>().Insert(resrcAlcObj);

                                _unitOfWork.SaveChanges();
                            }
                        }
                    }
                }

                _unitOfWork.ExecuteSqlCommand(" update dbo.ResourceAllocation SET TaskStatus =  @p0 WHERE ResourceAllocationID = @p1 ", "VERF", resource.ResourceAllocationID);
                if (ResourceAllocation.OperationType == ServiceTypeCode.BerthMaster)
                {
                    _berthPlanningRepository.ShiftingServiceRequest(resource.VCN);
                }

            }
            return resource;
        }
        #endregion

        #region VerifyResourceAllocationformDetails
        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 28th April 2014
        /// Purpose: To verify Resource Allocation Details
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public string VerifyResourceAllocationDetails(string operationType, string movementType, string resourceAllocationId)
        {
            ResourceAllocationVO obj = new ResourceAllocationVO();

            string status = "true";

            if (operationType == ServiceTypeCode.BerthMaster || operationType == ServiceTypeCode.Shifting)
            {
                var resources = (from t in _unitOfWork.Repository<ResourceAllocation>().Query()
                                     .Include(t => t.ShiftingBerthingTaskExecutions)
                                     .Select()
                                 where t.ResourceAllocationID == Convert.ToInt32(resourceAllocationId, CultureInfo.InvariantCulture)
                                 select t).FirstOrDefault();

                obj = resources.MapToDTO();
            }
            else if (operationType == ServiceTypeCode.Pilotage || operationType == ServiceTypeCode.PilotBoatorHelicopterService)
            {
                var resources = (from t in _unitOfWork.Repository<ResourceAllocation>().Query().Include(t => t.PilotageServiceRecordings).Select()
                                 where t.ResourceAllocationID == Convert.ToInt32(resourceAllocationId, CultureInfo.InvariantCulture)
                                 select t).FirstOrDefault();
                obj = resources.MapToDTO();
            }
            else
            {
                var resources = (from t in _unitOfWork.Repository<ResourceAllocation>().Query().Include(t => t.OtherServiceRecordings).Select()
                                 where t.ResourceAllocationID == Convert.ToInt32(resourceAllocationId, CultureInfo.InvariantCulture)
                                 select t).FirstOrDefault();
                obj = resources.MapToDTO();
            }


            if (movementType == "ARMV" && operationType == "BRTH")
            {

                if (obj.ShiftingBerthingTaskExecution.StartTime == null || obj.ShiftingBerthingTaskExecution.EndTime == null || obj.ShiftingBerthingTaskExecution.FromBerthCode == null || obj.ShiftingBerthingTaskExecution.ToBerthCode == null || obj.ShiftingBerthingTaskExecution.FromBollardCode == null || obj.ShiftingBerthingTaskExecution.ToBollardCode == null || obj.ShiftingBerthingTaskExecution.MooringBollardBowBollardCode == null || obj.ShiftingBerthingTaskExecution.MooringBollardStemBollardCode == null || obj.ShiftingBerthingTaskExecution.FirstLineIn == null || obj.ShiftingBerthingTaskExecution.LastLineIn == null || obj.ShiftingBerthingTaskExecution.AftDraft == null || obj.ShiftingBerthingTaskExecution.ForwardDraft == null)
                {
                    status = "false";
                }
            }
            else if ((movementType == "WRMV" || movementType == "SHMV") && operationType == "BRTH")
            {
                if (obj.ShiftingBerthingTaskExecution.StartTime == null || obj.ShiftingBerthingTaskExecution.EndTime == null || obj.ShiftingBerthingTaskExecution.FromBerthCode == null || obj.ShiftingBerthingTaskExecution.ToBerthCode == null || obj.ShiftingBerthingTaskExecution.FromBollardCode == null || obj.ShiftingBerthingTaskExecution.ToBollardCode == null || obj.ShiftingBerthingTaskExecution.MooringBollardBowBollardCode == null || obj.ShiftingBerthingTaskExecution.MooringBollardStemBollardCode == null || obj.ShiftingBerthingTaskExecution.FirstLineIn == null || obj.ShiftingBerthingTaskExecution.FirstLineOut == null || obj.ShiftingBerthingTaskExecution.LastLineIn == null || obj.ShiftingBerthingTaskExecution.LastLineOut == null || obj.ShiftingBerthingTaskExecution.AftDraft == null || obj.ShiftingBerthingTaskExecution.ForwardDraft == null)
                {
                    status = "false";
                }
            }
            else if ((movementType == "SGMV") && operationType == "BRTH")
            {
                if (obj.ShiftingBerthingTaskExecution.StartTime == null || obj.ShiftingBerthingTaskExecution.EndTime == null || obj.ShiftingBerthingTaskExecution.FirstLineOut == null || obj.ShiftingBerthingTaskExecution.LastLineOut == null || obj.ShiftingBerthingTaskExecution.AftDraft == null || obj.ShiftingBerthingTaskExecution.ForwardDraft == null)
                {
                    status = "false";
                }
            }
            else if (operationType == "PILT")
            {
                if (obj.PilotageServiceRecording.StartTime == null || obj.PilotageServiceRecording.EndTime == null || obj.PilotageServiceRecording.PilotOnBoard == null || obj.PilotageServiceRecording.PilotOff == null || obj.PilotageServiceRecording.WaitingStartTime == null || obj.PilotageServiceRecording.WaitingEndTime == null || obj.PilotageServiceRecording.AdditionalTugs == null)
                {
                    status = "false";
                }
            }
            else if (operationType == "FCST")
            {
                if (obj.OtherServiceRecording.StartTime == null || obj.OtherServiceRecording.EndTime == null || obj.OtherServiceRecording.FirstSwing == null || obj.OtherServiceRecording.LastSwing == null || obj.OtherServiceRecording.TimeAlongSide == null)
                {
                    status = "false";
                }
            }
            else if (operationType == "PIHE")
            {
                if (obj.PilotageServiceRecording.StartTime == null || obj.PilotageServiceRecording.EndTime == null || obj.PilotageServiceRecording.PilotOnBoard == null || obj.PilotageServiceRecording.PilotOff == null)
                {
                    status = "false";
                }
            }
            else if (operationType == "TGWR")
            {
                if (obj.OtherServiceRecording.StartTime == null || obj.OtherServiceRecording.EndTime == null || obj.OtherServiceRecording.LineUp == null || obj.OtherServiceRecording.LineDown == null)
                {
                    status = "false";
                }
            }
            else if (operationType == "WTST")
            {
                if (obj.OtherServiceRecording.StartTime == null || obj.OtherServiceRecording.EndTime == null || obj.OtherServiceRecording.OpeningMeterReading == null || obj.OtherServiceRecording.ClosingMeterReading == null || obj.OtherServiceRecording.TotalDispensed == null ||obj.MeterNo==null)
                {
                    status = "false";
                }
            }

            return status;
        }
        #endregion

        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<RevenuePostingVO> ServiceRecordingVcnDetailsforAutocomplete(string searchValue, string portCode)
        {

            var portcode = new SqlParameter("@p_PortCode", portCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var vcndtls = _unitOfWork.SqlQuery<RevenuePostingVO>("dbo.usp_GetServiceRecordingVCNSearch @p_SearchText, @p_PortCode", Searchvalue, portcode).OrderBy(a => a.vcn).ToList();
            return vcndtls;
            
        }
        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<VesselVO> ServiceRecordingVesselDetailsforAutocomplete(string PortCode, string searchValue)
        {

            var portcode = new SqlParameter("@p_PortCode", PortCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var _VesselInfo = _unitOfWork.SqlQuery<VesselVO>("dbo.usp_GetServiceRecordingVesselSearch @p_SearchText, @p_PortCode", Searchvalue, portcode).ToList();


            return _VesselInfo;
        }

        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<UserMasterVO> ServiceRecordingResourceDetailsforAutocomplete(string PortCode, string searchValue)
        {

            var portcode = new SqlParameter("@p_PortCode", PortCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);

            var _userInfo = _unitOfWork.SqlQuery<UserMasterVO>("dbo.usp_GetServiceRecordingResourceSearch @p_SearchText, @p_PortCode", Searchvalue, portcode).ToList();


            return _userInfo.OrderBy(a=>a.FirstName).ToList();
        }


        public ServiceRecordingVO GetServiceRecordingDetails(string resourceallocationid)
        {
            var resourceId = Convert.ToInt32(resourceallocationid, CultureInfo.InvariantCulture);

            var recordingdetails = (from ra in _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(ra => ra.ResourceAllocationID == resourceId && ra.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && ra.RecordStatus == RecordStatus.Active)
                           join srv in _unitOfWork.Repository<ServiceRequest>().Queryable() on ra.ServiceReferenceID equals srv.ServiceRequestID
                           join ar in _unitOfWork.Repository<ArrivalNotification>().Queryable() on srv.VCN equals ar.VCN
                           join vsl in _unitOfWork.Repository<Vessel>().Queryable() on ar.VesselID equals vsl.VesselID
                           join vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable() on srv.ServiceRequestID equals vcm.ServiceRequestID
                           join pr in _unitOfWork.Repository<PilotageServiceRecording>().Queryable() on ra.ResourceAllocationID equals pr.ResourceAllocationID
                           into prInfo
                           from pilotservice in prInfo.DefaultIfEmpty()
                           join sb in _unitOfWork.Repository<ShiftingBerthingTaskExecution>().Queryable() on ra.ResourceAllocationID equals sb.ResourceAllocationID
                           into sbInfo
                           from shiftberth in sbInfo.DefaultIfEmpty()
                           select new ServiceRecordingVO
                           {
                               VCN = ar.VCN,
                               VesselName = vsl.VesselName,
                               PilotOnBoard = pilotservice.PilotOnBoard,
                               PilotOff = pilotservice.PilotOff,
                               FirstLineIn = shiftberth.FirstLineIn,
                               LastLineIn = shiftberth.LastLineIn,
                               FirstLineOut = shiftberth.FirstLineOut,
                               LastLineOut = shiftberth.LastLineOut,
                               CreatedBy = ra.CreatedBy,
                               AgentID = ar.AgentID,
                               MovementType = srv.MovementType
                           }).FirstOrDefault();

            return recordingdetails;
        }


        public ServiceRecordingVO GetServiceRecordingDetailsForMobile(int resourceallocationid)
        {


            var recordingdetails = (from ra in _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(ra => ra.ResourceAllocationID == resourceallocationid && ra.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && ra.RecordStatus == RecordStatus.Active)
                                    join srv in _unitOfWork.Repository<ServiceRequest>().Queryable() on ra.ServiceReferenceID equals srv.ServiceRequestID
                                    join ar in _unitOfWork.Repository<ArrivalNotification>().Queryable() on srv.VCN equals ar.VCN
                                    join pr in _unitOfWork.Repository<PilotageServiceRecording>().Queryable() on ra.ResourceAllocationID equals pr.ResourceAllocationID
                                    into prInfo
                                    from pilotservice in prInfo.DefaultIfEmpty()
                                    join sb in _unitOfWork.Repository<ShiftingBerthingTaskExecution>().Queryable() on ra.ResourceAllocationID equals sb.ResourceAllocationID
                                    into sbInfo
                                    from shiftberth in sbInfo.DefaultIfEmpty()
                                    select new ServiceRecordingVO
                                    {
                                        VCN = ar.VCN,
                                        PilotOnBoard = pilotservice.PilotOnBoard,
                                        PilotOff = pilotservice.PilotOff,
                                        FirstLineIn = shiftberth.FirstLineIn,
                                        LastLineIn = shiftberth.LastLineIn,
                                        FirstLineOut = shiftberth.FirstLineOut,
                                        LastLineOut = shiftberth.LastLineOut,
                                        CreatedBy = ra.CreatedBy,
                                        AgentID = ar.AgentID,
                                        MovementType = srv.MovementType,
                                        EndTime = pilotservice.EndTime,
                                        WaitingStartTime = pilotservice.WaitingStartTime,
                                        WaitingEndTime = pilotservice.WaitingEndTime,
                                        EndTime1 = shiftberth.EndTime,
                                        WaitingStartTime1 = shiftberth.WaitingStartTime,
                                        WaitingEndTime1 = shiftberth.WaitingEndTime
                                    }).FirstOrDefault();

           
            return recordingdetails;
        }

        public List<UserMasterVO> GetReourceNamesByType(string portCode,string designation)
        {
            var Emplist = (from u in _unitOfWork.Repository<User>().Queryable()
                           join a in _unitOfWork.Repository<Employee>().Queryable()
                          on u.UserTypeID equals a.EmployeeID                     
                           join up in _unitOfWork.Repository<UserPort>().Queryable()
                          on u.UserID equals up.UserID 
                          where u.UserType == UserType.Employee && u.RecordStatus == RecordStatus.Active
                          && a.Designation == designation && up.PortCode == portCode
                           select new UserMasterVO
                            {
                                UserID = u.UserID,
                                UserName = u.FirstName + " " + u.LastName,
                            }).ToList();
            return Emplist;

        }


    }
}

