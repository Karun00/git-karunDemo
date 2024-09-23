using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Security.Cryptography;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System.Data.Entity.SqlServer;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Linq;
using System;
using System.Configuration;
using System.Globalization;
using IPMS.Core.Repository.Exceptions;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AutomatedResourceSchedulingService : ServiceBase, IAutomatedResourceSchedulingService
    {
        private IAutomatedResourceSchedulingRepository _automatedResourceSchedulingRepository;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository;
        private List<ResourceSlotVO> lstSlotConfig = null;

        public AutomatedResourceSchedulingService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _automatedResourceSchedulingRepository = new AutomatedResourceSchedulingRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
        }

        public AutomatedResourceSchedulingService(IUnitOfWork unitofWork)
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _automatedResourceSchedulingRepository = new AutomatedResourceSchedulingRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
        }

        /// <summary>
        /// Get Confirmed Service Requests
        /// </summary>
        /// <returns></returns>
        public List<VesselCallMovementVO> GetPendingMovementsForAllocation()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedResourceSchedulingRepository.GetPendingMovementsForAllocation(_PortCode);
            });
        }

        public List<MovementResourceAllocationVO> GetResourceAllocations(DateTime slotDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedResourceSchedulingRepository.GetResourceAllocations(_PortCode, slotDate);
            });
        }

        public List<MovementResourceAllocationVO> SaveResourceAllocations(MovementResourceAllocationVO vesselCallMovements)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 if (vesselCallMovements.OperationType == "DEAL")
                 {
                     List<MovementResourceAllocationVO> lstMovementResourceAllocationVO = _unitOfWork.SqlQuery<MovementResourceAllocationVO>("Delete from ResourceAllocation WHERE ServiceReferenceID = " + vesselCallMovements.ServiceRequestId + "").ToList();

                     var vesselcallmovemnt = _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus = @p0 where ServiceRequestID = @p1", AutomatedSlotStatus.Confirmed, vesselCallMovements.ServiceRequestId);
                 }
                 else
                 {
                    List<ResourceAllocationSlotVO> resourceslotvo = vesselCallMovements.ResourceAllocationSlots;
                  
                     ResourceSlotVO resourceSlots = new ResourceSlotVO();

                     int total = resourceslotvo.Count;
                     
                     int noresourceCount = 0;

                     foreach (var item in resourceslotvo)
                     {
                         if (item.ResourceSlots.Find(r => r.ResourceID != null && r.ResourceID != 0) != null)
                         {
                             resourceSlots = item.ResourceSlots.Find(r => r.ResourceID != null && r.ResourceID != 0);
                         }
                         else
                         {
                             noresourceCount++;
                         }
                     }

                     if (total == noresourceCount)
                     {
                         throw new BusinessExceptions(ErrorMessages.UnscheduleAllResources);
                     }



                     List<ResourceSlotVO> lstSlotConfig = _suppServiceResourceAllocRepository.GetSlotConfiguration(DateTime.Now, _PortCode);

                     var status = lstSlotConfig.Find(s => s.SlotNumber == resourceSlots.SlotNumber && Convert.ToDateTime(s.AllocationDate).Date == Convert.ToDateTime(resourceSlots.AllocationDate).Date);

                     if (status == null)
                     {                         
                         lstSlotConfig = _suppServiceResourceAllocRepository.GetSlotConfiguration(DateTime.Now.AddDays(1), _PortCode);
                     }

                     foreach (ResourceAllocationSlotVO rsrcall in resourceslotvo)
                     {
                         ResourceAllocation obj = new ResourceAllocation();

                         List<ResourceSlotVO> lst = rsrcall.ResourceSlots;
                         ResourceSlotVO slotVO = lst.Find(a => a.ResourceID != null);

                         obj.ServiceReferenceType = ServiceReferenceType.VeselTraficServices;
                         obj.ServiceReferenceID = rsrcall.ServiceReferenceID;
                         obj.ResourceID = slotVO.ResourceID > 0 ? slotVO.ResourceID : null;

                         var config = lstSlotConfig.Find(sl => sl.SlotNumber == slotVO.SlotNumber);
                         if (config != null)
                         {
                             obj.AllocSlot = config.SlotPeriod;
                             obj.AllocationDate = config.AllocationDate;
                         }

                         obj.TaskStatus = slotVO.TaskStatus;
                         obj.CraftID = Convert.ToInt32(slotVO.CraftID, CultureInfo.InvariantCulture) > 0 ? slotVO.CraftID : null;
                         obj.StartTime = rsrcall.StartTime != "" ? DateTime.Parse(rsrcall.StartTime, CultureInfo.InvariantCulture) : default(DateTime?);
                         obj.EndTime = rsrcall.EndTime != "" ? DateTime.Parse(rsrcall.EndTime, CultureInfo.InvariantCulture) : default(DateTime?);
                         obj.AcknowledgeDate = rsrcall.AcknowledgeDate != null ? Convert.ToDateTime(rsrcall.AcknowledgeDate, CultureInfo.InvariantCulture) : default(DateTime?);
                         obj.IsConfirm = vesselCallMovements.IsConfirm;

                         if (!rsrcall.IsServiceTypeDeleted)
                         {
                             if (rsrcall.ResourceAllocationID > 0 && rsrcall.ResourceAllocationID != null)
                             {
                                 // obj.CreatedDate = Convert.ToDateTime(rsrcall.CreatedDate, CultureInfo.InvariantCulture);
                                 obj.CreatedDate = DateTime.ParseExact(rsrcall.CreatedDate.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                 obj.CreatedBy = rsrcall.CreatedBy;
                                 obj.ModifiedDate = DateTime.Now;
                                 obj.ModifiedBy = _UserId;
                                 obj.RecordStatus = "A";

                                 obj.OperationType = rsrcall.ServiceTypeCode;
                                 obj.ResourceAllocationID = rsrcall.ResourceAllocationID;
                                 obj.ObjectState = ObjectState.Modified;
                                 if (obj.IsConfirm)
                                 {
                                     if (obj.TaskStatus == ResourceAllcationWorkFlowStatus.Pending || obj.TaskStatus == ResourceAllcationWorkFlowStatus.Scheduled || obj.TaskStatus == AutomatedSlotStatus.Overridden)
                                     {
                                         obj.TaskStatus = ResourceAllcationWorkFlowStatus.Confirmed;
                                     }
                                 }
                                 else if (obj.TaskStatus == AutomatedSlotStatus.Overridden || obj.TaskStatus == ResourceAllcationWorkFlowStatus.Scheduled)
                                 {
                                     obj.TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled;
                                 }

                                 _unitOfWork.Repository<ResourceAllocation>().Update(obj);
                             }
                             else
                             {
                                 obj.CreatedDate = DateTime.Now;
                                 obj.CreatedBy = _UserId;
                                 obj.ModifiedDate = DateTime.Now;
                                 obj.ModifiedBy = _UserId;
                                 if (obj.IsConfirm)
                                 {
                                     obj.TaskStatus = ResourceAllcationWorkFlowStatus.Confirmed;
                                 }
                                 else
                                 {
                                     obj.TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled;
                                 }
                                 obj.OperationType = rsrcall.ServiceTypeCode;
                                 obj.ObjectState = ObjectState.Added;
                                 obj.RecordStatus = "A";

                                 _unitOfWork.Repository<ResourceAllocation>().Insert(obj);
                             }
                         }
                         else
                         {
                             if (rsrcall.ResourceAllocationID > 0 && rsrcall.ResourceAllocationID != null)
                             {
                                 // obj.CreatedDate = Convert.ToDateTime(rsrcall.CreatedDate.Replace("-", "/"), CultureInfo.InvariantCulture);
                                 obj.CreatedDate = DateTime.ParseExact(rsrcall.CreatedDate.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                 obj.CreatedBy = rsrcall.CreatedBy;
                                 obj.ModifiedDate = DateTime.Now;
                                 obj.ModifiedBy = _UserId;
                                 obj.ResourceAllocationID = rsrcall.ResourceAllocationID;
                                 obj.RecordStatus = "I";
                                 obj.ObjectState = ObjectState.Modified;

                                 _unitOfWork.Repository<ResourceAllocation>().Update(obj);
                             }
                         }
                         _unitOfWork.SaveChanges();
                     }
                 }
                 return new List<MovementResourceAllocationVO>();
             });
        }

        public string ScheduleMovements(VesselCallMovementVO vesselMovements)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                var serviceTypes = new List<ServiceTypeVO>();

                string result = string.Empty;
                int count = 0;

                //Get Service Types for movment
                var portcode = new SqlParameter("@PortCode", _PortCode);
                var movemntType = new SqlParameter("@MovemntType", vesselMovements.ServiceRequest);
                serviceTypes = _unitOfWork.SqlQuery<ServiceTypeVO>("Select * from  dbo.udf_tbl_GetServiceReqTypeByMoventType(@PortCode,@MovemntType)", portcode, movemntType).ToList();

                lstSlotConfig = _suppServiceResourceAllocRepository.GetSlotConfiguration(DateTime.Now, _PortCode);                

                var status = lstSlotConfig.Find(a => Convert.ToDateTime(vesselMovements.SlotDate, CultureInfo.InvariantCulture) >= a.StartDate && Convert.ToDateTime(vesselMovements.SlotDate, CultureInfo.InvariantCulture) <= a.EndDate);

                if (status != null)
                {
                    vesselMovements.Slot = status.SlotPeriod;
                }
                if (status == null)
                {
                    lstSlotConfig = _suppServiceResourceAllocRepository.GetSlotConfiguration(Convert.ToDateTime(vesselMovements.SlotDate), _PortCode);
                }

                ResourceSlotVO _serviceType = new ResourceSlotVO();
                int _shiftid = lstSlotConfig.Find(a => Convert.ToDateTime(vesselMovements.SlotDate, CultureInfo.InvariantCulture) >= a.StartDate && Convert.ToDateTime(vesselMovements.SlotDate, CultureInfo.InvariantCulture) <= a.EndDate).ShiftID;
                int _slotNumber = lstSlotConfig.Find(a => Convert.ToDateTime(vesselMovements.SlotDate, CultureInfo.InvariantCulture) >= a.StartDate && Convert.ToDateTime(vesselMovements.SlotDate, CultureInfo.InvariantCulture) <= a.EndDate).SlotNumber;
                DateTime _allocationDate = lstSlotConfig.Find(a => Convert.ToDateTime(vesselMovements.SlotDate, CultureInfo.InvariantCulture) >= a.StartDate && Convert.ToDateTime(vesselMovements.SlotDate, CultureInfo.InvariantCulture) <= a.EndDate).AllocationDate;               

                DateTime prevdt = _allocationDate.AddDays(-1);
                DateTime nxtdt = _allocationDate.AddDays(1);

                _serviceType.ShiftID = _shiftid;
                _serviceType.SlotNumber = _slotNumber;
                _serviceType.AllocationDate = _allocationDate;

                if (serviceTypes.Count > 0)
                {
                    foreach (ServiceTypeVO service in serviceTypes)
                    {
                        var portcode1 = new SqlParameter("@PortCode", _PortCode);
                        var serviceTypecode = new SqlParameter("@ServiceTypeCode", service.ServiceTypeCode);

                        List<ResourceAllocationConfigVO> slotconfig = _unitOfWork.SqlQuery<ResourceAllocationConfigVO>("usp_GetResourceAllocationConfigDet @PortCode,@ServiceTypeCode", portcode1, serviceTypecode).ToList();
                                              
                        _serviceType.ServiceTypeCode = service.ServiceTypeCode;

                        if (service.ServiceTypeCode == ServiceTypeCode.TugorworkBoatService)
                        {
                            for (int i = 0; i < slotconfig[0].TotalTugs; i++)
                            {
                                List<IdNameVO> lstusers = _suppServiceResourceAllocRepository.GetSearchResource(_serviceType, _PortCode);

                                //getting list of users in resource allocation users based on current date (ie.. allocated user )                               

                                List<ResourceAllocation> lstresc = _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && r.AllocationDate > prevdt && r.AllocationDate < nxtdt).ToList();

                                int? userid = null;
                                bool checkuser = true;

                                if (lstusers.Count > 0)
                                {
                                    do
                                    {
                                        foreach (IdNameVO user in lstusers)
                                        {
                                            //comparing with allocated users 
                                            ResourceAllocation rsuser = lstresc.Find(u => u.ResourceID == user.ID);

                                            // if any user is not allocated
                                            if (rsuser == null)
                                            {
                                                userid = user.ID;
                                                checkuser = false;
                                                break;
                                            }
                                        }

                                        if (checkuser)
                                        {
                                            lstresc = lstresc.FindAll(s => s.AllocSlot == vesselMovements.Slot);
                                            lstusers = lstusers.Where(a => !lstresc.Any(a1 => a1.ResourceID == a.ID)).ToList();

                                            foreach (IdNameVO user in lstusers)
                                            {
                                                //comparing with allocated users 
                                                ResourceAllocation rsuser = lstresc.Find(u => u.ResourceID == user.ID);

                                                // if any user is not allocated
                                                if (rsuser == null)
                                                {
                                                    userid = user.ID;
                                                    checkuser = false;

                                                    break;
                                                }
                                            }

                                            if (checkuser)
                                            {                                              

                                                lstresc = _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && r.AllocationDate > prevdt && r.AllocationDate < nxtdt).ToList();

                                                lstusers = _suppServiceResourceAllocRepository.GetSearchResource(_serviceType, _PortCode);

                                                var lst = lstresc.FindAll(c => c.TaskStatus == ResourceAllcationWorkFlowStatus.Completed || c.TaskStatus == ResourceAllcationWorkFlowStatus.Verified);
                                                lstresc = lstresc.FindAll(c => c.TaskStatus != ResourceAllcationWorkFlowStatus.Completed && c.TaskStatus != ResourceAllcationWorkFlowStatus.Verified);

                                                lstusers = lstusers.Where(a => !lstresc.Any(a1 => a1.ResourceID == a.ID)).ToList();

                                                if (lstusers.Count > 0)
                                                {
                                                    if (lstusers.Count == 1)
                                                    {
                                                        userid = lstusers[0].ID;
                                                        checkuser = false;
                                                    }
                                                    else
                                                    {
                                                        int? _count = null;
                                                        int? temp = null;

                                                        foreach (IdNameVO user in lstusers)
                                                        {
                                                            _count = lst.FindAll(d => d.ResourceID == user.ID).Count();

                                                            if (temp == null)
                                                            {
                                                                temp = _count;
                                                                userid = user.ID;
                                                            }
                                                            else if (temp < _count)
                                                            {
                                                                temp = _count;
                                                                userid = user.ID;
                                                            }
                                                            else
                                                            {
                                                                temp = _count;
                                                                userid = user.ID;
                                                            }
                                                        }
                                                        checkuser = false;
                                                    }
                                                }
                                                else
                                                {
                                                    checkuser = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            checkuser = false;
                                        }
                                    } while (checkuser);
                                }                              

                                lstresc = _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && r.AllocationDate > prevdt && r.AllocationDate < nxtdt).ToList();

                                List<IdNameVO> lstCrafts = _automatedResourceSchedulingRepository.GetSearchCraft(_serviceType, _PortCode);

                                int? craftid = null;
                                bool checkcraft = true;

                                if (lstCrafts.Count > 0)
                                {
                                    do
                                    {
                                        foreach (IdNameVO craft in lstCrafts)
                                        {
                                            //comparing with allocated users 
                                            ResourceAllocation rsuser = lstresc.Find(u => u.CraftID == craft.ID);

                                            // if any user is not allocated
                                            if (rsuser == null)
                                            {
                                                craftid = craft.ID;
                                                checkcraft = false;
                                                break;
                                            }
                                        }

                                        if (checkcraft)
                                        {
                                            lstresc = lstresc.FindAll(s => s.AllocSlot == vesselMovements.Slot);
                                            lstCrafts = lstCrafts.Where(a => !lstresc.Any(a1 => a1.CraftID == a.ID)).ToList();

                                            foreach (IdNameVO craft in lstCrafts)
                                            {
                                                //comparing with allocated users 
                                                ResourceAllocation rsuser = lstresc.Find(u => u.CraftID == craft.ID);

                                                // if any user is not allocated
                                                if (rsuser == null)
                                                {
                                                    craftid = craft.ID;
                                                    checkcraft = false;
                                                    break;
                                                }
                                            }

                                            if (checkcraft)
                                            {                                                

                                                lstresc = _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && r.AllocationDate > prevdt && r.AllocationDate < nxtdt).ToList();

                                                lstCrafts = _automatedResourceSchedulingRepository.GetSearchCraft(_serviceType, _PortCode);

                                                var lst = lstresc.FindAll(c => c.TaskStatus == ResourceAllcationWorkFlowStatus.Completed || c.TaskStatus == ResourceAllcationWorkFlowStatus.Verified);
                                                lstresc = lstresc.FindAll(c => c.TaskStatus != ResourceAllcationWorkFlowStatus.Completed && c.TaskStatus != ResourceAllcationWorkFlowStatus.Verified);

                                                lstCrafts = lstCrafts.Where(a => !lstresc.Any(a1 => a1.CraftID == a.ID)).ToList();

                                                if (lstCrafts.Count > 0)
                                                {
                                                    if (lstCrafts.Count == 1)
                                                    {
                                                        craftid = lstCrafts[0].ID;
                                                        checkcraft = false;
                                                    }
                                                    else
                                                    {
                                                        int? _count = null;
                                                        int? temp = null;

                                                        foreach (IdNameVO craft in lstCrafts)
                                                        {
                                                            _count = lst.FindAll(d => d.CraftID == craft.ID).Count();

                                                            if (temp == null)
                                                            {
                                                                temp = _count;
                                                                craftid = craft.ID;
                                                            }
                                                            else if (temp < _count)
                                                            {
                                                                temp = _count;
                                                                craftid = craft.ID;
                                                            }
                                                            else
                                                            {
                                                                temp = _count;
                                                                craftid = craft.ID;
                                                            }
                                                        }
                                                        checkcraft = false;
                                                    }
                                                }
                                                else
                                                {
                                                    checkcraft = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            checkcraft = false;
                                        }
                                    } while (checkcraft);
                                }

                                int? _userid = (userid == null || craftid == null) ? null : userid;
                                int? _craftid = (userid == null || craftid == null) ? null : craftid;

                                ResourceAllocation resourceAllocation = new ResourceAllocation
                                {
                                    ServiceReferenceType = ServiceReferenceType.VeselTraficServices,
                                    ServiceReferenceID = vesselMovements.ServiceRequestID ?? default(int),
                                    OperationType = service.ServiceTypeCode,
                                    TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled,
                                    RecordStatus = "A",
                                    CreatedBy = _UserId,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = _UserId,
                                    ModifiedDate = DateTime.Now,
                                    AllocSlot = vesselMovements.Slot,
                                    ResourceID = _userid,
                                    AllocationDate = vesselMovements.SlotDate,
                                    ObjectState = ObjectState.Added,
                                    CraftID = _craftid
                                };

                                _unitOfWork.Repository<ResourceAllocation>().Insert(resourceAllocation);
                                _unitOfWork.SaveChanges();

                                count = count + 1;
                            }
                        }
                        else if (service.ServiceTypeCode == ServiceTypeCode.Shifting || service.ServiceTypeCode == ServiceTypeCode.BerthMaster)
                        {
                            // Get Servicerequest details by servicerequest id
                            var servicereq = _automatedResourceSchedulingRepository.GetServiceRequestDetailsById(vesselMovements.ServiceRequestID ?? default(int));

                            int noofGangs = default(int);
                            foreach (ResourceAllocationConfigVO config in slotconfig)
                            {
                                //Here getting noofgangs besed on vessel length
                                if (servicereq.LengthOverallInM >= config.FromMeter && servicereq.LengthOverallInM <= config.ToMeter)
                                {
                                    noofGangs = config.NoOfGangs;
                                    break;
                                }
                            }

                            if (noofGangs > 0)
                            {                                
                                for (int k = 0; k < noofGangs; k++)
                                {
                                    //getting list of users besed on port ,current date , designation and employee attendence
                                    List<IdNameVO> lstusers = _suppServiceResourceAllocRepository.GetSearchResource(_serviceType, _PortCode);
                                    
                                    //getting list of users in resource allocation users based on current date (ie.. allocated user )                                 

                                    List<ResourceAllocation> lstresc = _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && r.AllocationDate > prevdt && r.AllocationDate < nxtdt).ToList();

                                    int? userid = null;
                                    bool checkuser = true;

                                    if (lstusers.Count > 0)
                                    {
                                        do
                                        {
                                            foreach (IdNameVO user in lstusers)
                                            {
                                                //comparing with allocated users 
                                                ResourceAllocation rsuser = lstresc.Find(u => u.ResourceID == user.ID);

                                                // if any user is not allocated
                                                if (rsuser == null)
                                                {
                                                    userid = user.ID;
                                                    checkuser = false;
                                                    break;
                                                }
                                            }

                                            if (checkuser)
                                            {
                                                lstresc = lstresc.FindAll(s => s.AllocSlot == vesselMovements.Slot);
                                                lstusers = lstusers.Where(a => !lstresc.Any(a1 => a1.ResourceID == a.ID)).ToList();

                                                foreach (IdNameVO user in lstusers)
                                                {
                                                    //comparing with allocated users 
                                                    ResourceAllocation rsuser = lstresc.Find(u => u.ResourceID == user.ID);

                                                    // if any user is not allocated
                                                    if (rsuser == null)
                                                    {
                                                        userid = user.ID;
                                                        checkuser = false;
                                                        break;
                                                    }
                                                }

                                                if (checkuser)
                                                {                                                   

                                                    lstresc = _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && r.AllocationDate > prevdt && r.AllocationDate < nxtdt).ToList();

                                                    lstusers = _suppServiceResourceAllocRepository.GetSearchResource(_serviceType, _PortCode);

                                                    var lst = lstresc.FindAll(c => c.TaskStatus == ResourceAllcationWorkFlowStatus.Completed || c.TaskStatus == ResourceAllcationWorkFlowStatus.Verified);
                                                    lstresc = lstresc.FindAll(c => c.TaskStatus != ResourceAllcationWorkFlowStatus.Completed && c.TaskStatus != ResourceAllcationWorkFlowStatus.Verified);

                                                    lstusers = lstusers.Where(a => !lstresc.Any(a1 => a1.ResourceID == a.ID)).ToList();

                                                    if (lstusers.Count > 0)
                                                    {
                                                        if (lstusers.Count == 1)
                                                        {
                                                            userid = lstusers[0].ID;
                                                            checkuser = false;
                                                        }
                                                        else
                                                        {
                                                            int? _count = null;
                                                            int? temp = null;

                                                            foreach (IdNameVO user in lstusers)
                                                            {
                                                                _count = lst.FindAll(d => d.ResourceID == user.ID).Count();

                                                                if (temp == null)
                                                                {
                                                                    temp = _count;
                                                                    userid = user.ID;
                                                                }
                                                                else if (temp < _count)
                                                                {
                                                                    temp = _count;
                                                                    userid = user.ID;
                                                                }
                                                                else
                                                                {
                                                                    temp = _count;
                                                                    userid = user.ID;
                                                                }
                                                            }
                                                            checkuser = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        checkuser = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                checkuser = false;
                                            }
                                        } while (checkuser);
                                    }

                                    ResourceAllocation resourceAllocation = new ResourceAllocation
                                    {
                                        ServiceReferenceType = ServiceReferenceType.VeselTraficServices,
                                        ServiceReferenceID = vesselMovements.ServiceRequestID ?? default(int),
                                        OperationType = service.ServiceTypeCode,
                                        TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled,
                                        RecordStatus = "A",
                                        CreatedBy = _UserId,
                                        CreatedDate = DateTime.Now,
                                        ModifiedBy = _UserId,
                                        ModifiedDate = DateTime.Now,
                                        AllocSlot = vesselMovements.Slot,
                                        ResourceID = userid,
                                        AllocationDate = vesselMovements.SlotDate,
                                        ObjectState = ObjectState.Added,
                                    };

                                    _unitOfWork.Repository<ResourceAllocation>().Insert(resourceAllocation);
                                    _unitOfWork.SaveChanges();
                                    count = count + 1;

                                }
                            }
                        }
                        else if (service.ServiceTypeCode == ServiceTypeCode.Pilotage)
                        {
                            var servicereq = _automatedResourceSchedulingRepository.GetServiceRequestDetailsById(vesselMovements.ServiceRequestID ?? default(int));

                            if (servicereq.PilotExemption == "I")
                            {                               
                                _serviceType.ServiceReferenceID = vesselMovements.ServiceRequestID ?? default(int);
                                                                
                                //getting list of users besed on port ,current date , designation and employee attendence
                                List<IdNameVO> lstusers = _suppServiceResourceAllocRepository.GetSearchResource(_serviceType, _PortCode);
                                
                                List<ResourceAllocation> lstresc = _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && r.AllocationDate > prevdt && r.AllocationDate < nxtdt).ToList();

                                int? userid = null;
                                bool checkuser = true;

                                if (lstusers.Count > 0)
                                {
                                    do
                                    {
                                        foreach (IdNameVO user in lstusers)
                                        {
                                            //comparing with allocated users 
                                            ResourceAllocation rsuser = lstresc.Find(u => u.ResourceID == user.ID);

                                            // if any user is not allocated
                                            if (rsuser == null)
                                            {
                                                userid = user.ID;
                                                checkuser = false;
                                                break;
                                            }
                                        }

                                        if (checkuser)
                                        {
                                            lstresc = lstresc.FindAll(s => s.AllocSlot == vesselMovements.Slot);
                                            lstusers = lstusers.Where(a => !lstresc.Any(a1 => a1.ResourceID == a.ID)).ToList();

                                            foreach (IdNameVO user in lstusers)
                                            {
                                                //comparing with allocated users 
                                                ResourceAllocation rsuser = lstresc.Find(u => u.ResourceID == user.ID);

                                                // if any user is not allocated
                                                if (rsuser == null)
                                                {
                                                    userid = user.ID;
                                                    checkuser = false;
                                                    break;
                                                }
                                            }

                                            if (checkuser)
                                            {                                                

                                                lstresc = _unitOfWork.Repository<ResourceAllocation>().Queryable().Where(r => r.ServiceReferenceType == ServiceReferenceType.VeselTraficServices && r.AllocationDate > prevdt && r.AllocationDate < nxtdt).ToList();

                                                lstusers = _suppServiceResourceAllocRepository.GetSearchResource(_serviceType, _PortCode);

                                                var lst = lstresc.FindAll(c => c.TaskStatus == ResourceAllcationWorkFlowStatus.Completed || c.TaskStatus == ResourceAllcationWorkFlowStatus.Verified);
                                                lstresc = lstresc.FindAll(c => c.TaskStatus != ResourceAllcationWorkFlowStatus.Completed && c.TaskStatus != ResourceAllcationWorkFlowStatus.Verified);

                                                lstusers = lstusers.Where(a => !lstresc.Any(a1 => a1.ResourceID == a.ID)).ToList();

                                                if (lstusers.Count > 0)
                                                {
                                                    if (lstusers.Count == 1)
                                                    {
                                                        userid = lstusers[0].ID;
                                                        checkuser = false;
                                                    }
                                                    else
                                                    {
                                                        int? _count = null;
                                                        int? temp = null;

                                                        foreach (IdNameVO user in lstusers)
                                                        {
                                                            _count = lst.FindAll(d => d.ResourceID == user.ID).Count();

                                                            if (temp == null)
                                                            {
                                                                temp = _count;
                                                                userid = user.ID;
                                                            }
                                                            else if (temp < _count)
                                                            {
                                                                temp = _count;
                                                                userid = user.ID;
                                                            }
                                                            else
                                                            {
                                                                temp = _count;
                                                                userid = user.ID;
                                                            }
                                                        }
                                                        checkuser = false;
                                                    }
                                                }
                                                else
                                                {
                                                    checkuser = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            checkuser = false;
                                        }
                                    } while (checkuser);
                                }

                                ResourceAllocation resource = new ResourceAllocation
                                {
                                    ServiceReferenceID = vesselMovements.ServiceRequestID ?? default(int),
                                    ResourceID = userid,
                                    CreatedBy = _UserId,
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = _UserId,
                                    ModifiedDate = DateTime.Now,
                                    RecordStatus = "A",
                                    ServiceReferenceType = ServiceReferenceType.VeselTraficServices,
                                    OperationType = service.ServiceTypeCode,
                                    AllocSlot = vesselMovements.Slot,
                                    TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled,
                                    ObjectState = ObjectState.Added,
                                    AllocationDate = vesselMovements.SlotDate,
                                };
                                _unitOfWork.Repository<ResourceAllocation>().Insert(resource);
                                _unitOfWork.SaveChanges();
                                count = count + 1;
                            }
                        }
                    }

                    if (count > 0)
                    {
                        result = count + ",true";
                        var vesselcallmovemnt = _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus = @p0 where ServiceRequestID = @p1", "SCHD", vesselMovements.ServiceRequestID);
                    }
                    else
                    {
                        result = count + ",true";
                    }
                }
                else
                {
                    result = "0,false";
                }
                return result;
            });
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get craft details based on ServiceType
        /// </summary>
        /// <param name="resourceSlot"></param>
        /// <returns></returns>
        public List<IdNameVO> GetSearchCraft(ResourceSlotVO resourceSlot)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedResourceSchedulingRepository.GetSearchCraft(resourceSlot, _PortCode);
            });
        }


        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 1st Oct  2014
        /// Purpose: To get all ServiceType details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> GetServiceTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedResourceSchedulingRepository.GetServiceTypes();
            });
        }

        /// <summary>
        /// To get the shift details Based on Ports
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ShiftVO> GetShiftDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var PortCode = new SqlParameter("@portcode", _PortCode);
                var date = new SqlParameter("@datename", DateTime.Now);
                var position = new SqlParameter("@designation", "NotRequired");
                var shifts = _unitOfWork.SqlQuery<ShiftVO>("dbo.usp_GetDateWiseShifts @datename, @designation, @portcode", date, position, PortCode).ToList();

                return shifts;
            });
        }


        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   : 28th Nov  2014
        /// Purpose: To get all Resource Calendar details
        /// </summary>
        /// <returns></returns>
        public List<ResourceCalendarVO> GetResourceCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearch)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                bool status = true;
                if (objResourceCalendarSearch.ServiceReferenceType == "SUPP")
                {
                    var role = _unitOfWork.Repository<Role>().Query().Select().ToList();
                    int FloatingCraneRoleID = role.Find(r => r.RoleCode == Roles.FloatingCraneMaster).RoleID;
                    int WaterManRoleID = role.Find(r => r.RoleCode == Roles.WaterMan).RoleID;

                    var servicetype = _suppServiceResourceAllocRepository.GetRoleByUser(_UserId, FloatingCraneRoleID, WaterManRoleID);

                    if (servicetype != null)
                    {
                        objResourceCalendarSearch.OperationType = servicetype;
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
                else
                {
                    status = true;
                }

                if (status)
                {
                    return _automatedResourceSchedulingRepository.GetResourceCalendarDetails(objResourceCalendarSearch, _PortCode);
                }
                else
                {
                    return new List<ResourceCalendarVO>();
                }

            });
        }

        public List<ResourceCalendarVO> GetCraftCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearchVO)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedResourceSchedulingRepository.GetCraftCalendarDetails(objResourceCalendarSearchVO, _PortCode);
            });
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   :13th Jan  2015
        /// Purpose: To get all Crafts Availability ServiceTypes details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> GetCraftAvailabilityServiceTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedResourceSchedulingRepository.GetCraftAvailabilityServiceTypes();
            });
        }

        /// <summary>
        /// To Get Resource Allocation record based on ResourceAllocationID  
        /// </summary>
        /// <param name="resourceAllocationId"></param>
        /// <returns></returns>
        public ResourceAllocationVO GetResourceAllocationById(int resourceAllocationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedResourceSchedulingRepository.GetResourceAllocationById(resourceAllocationId);
            });
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 19th May 2015
        /// Purpose: To verify ServiceRequestID in Vesselcallmovement 
        /// </summary>
        /// <param name="vesselCallMovement"></param>
        /// <returns></returns>
        public bool VerifyMovementIsActiveByVCNAndServiceRequestId(string vcn, int serviceRequestId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedResourceSchedulingRepository.VerifyMovementIsActiveByVCNAndServiceRequestId(vcn, serviceRequestId);
            });
        }

    }
}
