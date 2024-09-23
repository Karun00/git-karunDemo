using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.Core.Repository;
using Core.Repository;
using IPMS.Domain.Models;
using System.Linq;
using IPMS.Domain;
using System;
using System.Data.SqlClient;
using System.Globalization;

namespace IPMS.Repository
{
    public class SuppServiceResourceAllocRepository : ISuppServiceResourceAllocRepository
    {
        private IUnitOfWork _unitOfWork;
        private IShiftRepository _shiftRepository;
        private IAccountRepository _accountRepository;
        List<ResourceSlotVO> lstSlotConfig = null;

        public SuppServiceResourceAllocRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _shiftRepository = new ShiftRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get user details based on ServiceType
        /// </summary>
        /// <param name="ResourceSlot"></param>
        /// <param name="portcode"></param>
        /// <returns></returns>
        public List<IdNameVO> GetSearchResource(ResourceSlotVO ResourceSlot, string portcode)
        {
            var portcode2 = new SqlParameter("@PortCode", portcode);
            var movementType = new SqlParameter("@MovementType", ResourceSlot.ServiceTypeCode);
            var shiftID = new SqlParameter("@ShiftID", ResourceSlot.ShiftID);
            var allocationDate = new SqlParameter("@AllocationDate", ResourceSlot.AllocationDate);
            //getting list of users besed on port ,current date , designation and employee attendence
            List<IdNameVO> users = _unitOfWork.SqlQuery<IdNameVO>("usp_GetUsersForResourceAllocation @PortCode, @MovementType, @ShiftID, @AllocationDate", portcode2, movementType, shiftID, allocationDate).ToList();

            if (ResourceSlot.ServiceTypeCode == MovementTypes.Pilotage)
            {
                var vessel = (from r in _unitOfWork.Repository<ServiceRequest>().Queryable()
                              where r.ServiceRequestID == ResourceSlot.ServiceReferenceID
                              select new
                              {
                                  grossWeight = r.ArrivalNotification.Vessel.GrossRegisteredTonnageInMT,
                                  deadWeight = r.ArrivalNotification.Vessel.DeadWeightTonnageInMT
                              }).First();

                var slotconfig = (from rac in _unitOfWork.Repository<ResourceAllocationConfigRule>().Queryable()
                                  where rac.EffectedFrom <= DateTime.Now && rac.PortCode == portcode
                                  orderby rac.EffectedFrom descending
                                  select rac).FirstOrDefault();
                if (slotconfig != null)
                {
                    if (slotconfig.PilotCapacity == PilotCapacity.DeadweightTonnage)
                    {
                        users = users.FindAll(u => u.DeadWeightTonnage >= vessel.deadWeight).ToList();
                    }
                    else if (slotconfig.PilotCapacity == PilotCapacity.GrossTonnage)
                    {
                        users = users.FindAll(u => u.DeadWeightTonnage >= vessel.grossWeight).ToList();
                    }
                }
                else
                {
                    users = new List<IdNameVO>();
                }
            }

            return users;
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get list of resources based on date and portcode
        /// </summary>
        /// <param name="date"></param>
        /// <param name="portcode"></param>
        /// <returns></returns>
        public List<ResourceAllocationSlotVO> GetSupplementaryResourceAllocationByDate(DateTime date, string portcode, string vcn, string servicetypecode)
        {
            var _portcode = new SqlParameter("@PortCode", portcode);
            var _resourcedate = new SqlParameter("@ResourceDate", date);
            var _vcn = new SqlParameter("@VCN", vcn);
            var _servicetypecode = new SqlParameter("@ServiceType", servicetypecode);

            if (vcn == null) _vcn.Value = DBNull.Value;

            List<ResourceAllocationVO> suppResourceAllocation = _unitOfWork.SqlQuery<ResourceAllocationVO>("usp_GetSupplementaryResourceAllocationByDate @PortCode,@ResourceDate,@ServiceType,@VCN", _portcode, _resourcedate, _servicetypecode, _vcn).ToList();
            lstSlotConfig = GetSlotConfiguration(date, portcode);
            List<ResourceAllocationSlotVO> lstResourceAllocationSlotVO = ConstructSlotList(suppResourceAllocation, date, portcode);

            return lstResourceAllocationSlotVO;
        }     


        public List<ResourceSlotVO> GetSlotConfiguration(DateTime date, string portcode)
        {
            var slot = (from c in _unitOfWork.Repository<AutomatedSlotConfiguration>().Queryable()
                        where c.EffectiveFrm <= date && c.PortCode == portcode
                        orderby c.EffectiveFrm descending

                        select new
                        {
                            Duration = c.Duration,
                            ConfigPeriod = c.OperationalPeriod 
                        }).FirstOrDefault();

            var lstShifts = _shiftRepository.GetShiftsByPortCode(portcode).FindAll(s => s.IsShiftOff == "N" && s.IsContinuousShift == "N" && s.RecordStatus == RecordStatus.Active);

            int duration = slot != null ? slot.Duration : 2;
            int numberOfSlots = 1440;
            int slotDuration = duration;
            int slots = numberOfSlots / slotDuration;
            List<ResourceSlotVO> lstResourceSlotVOs = null;
            lstResourceSlotVOs = new List<ResourceSlotVO>();

            ResourceSlotVO obj = null;
            int slotperiod = default(int);

            int SlotStartDuration = Convert.ToInt32(slot.ConfigPeriod);

            DateTime dt = date;

            TimeSpan spStartTime = TimeSpan.FromMinutes(SlotStartDuration);
            int StartSlot = spStartTime.Hours; 
            string s1 = spStartTime.ToString(@"hh\:mm");

            slotperiod = SlotStartDuration;

            bool flag = false;
            for (int i = 0; i < slots; i++)
            {
                flag = false;
                obj = new ResourceSlotVO();
                obj.SlotNumber = i + 1;
                int startPeriod = slotperiod;
                int endPeriod = default(int);


                startPeriod = slotperiod;
                if (slotperiod + duration >= 1440)
                {
                    slotperiod = (slotperiod + duration) - 1440;
                    endPeriod = slotperiod;
                    flag = true;
                }
                else
                {
                    endPeriod = slotperiod + duration;
                    slotperiod += duration;
                }

                if (startPeriod == 1440)
                {
                    startPeriod = 0;
                    flag = true;
                }

                TimeSpan startslot = TimeSpan.FromMinutes(startPeriod);

                TimeSpan endsolt = TimeSpan.FromMinutes(endPeriod);

                obj.SlotPeriod = startslot.ToString(@"hh\:mm") + "-" + endsolt.ToString(@"hh\:mm");
                obj.ShiftID = GetShiftsBySlot(lstShifts, startslot.Hours);
                obj.AllocationDate = dt;
                obj.SlotDate = dt.Date.ToString("dd", CultureInfo.InvariantCulture) + "-" + dt.Date.ToString("MMM", CultureInfo.InvariantCulture);
                obj.SlotHeader = obj.SlotDate + "  /  " + startslot.ToString(@"hh\:mm") + " - " + endsolt.ToString(@"hh\:mm");
                obj.Duration = slot.Duration;
                obj.StartDate = dt.Date.AddHours(0).AddMinutes(startPeriod).AddSeconds(0);
                obj.EndDate = dt.Date.AddHours(0).AddMinutes(endPeriod).AddSeconds(0);
                obj.StartMinutes = startPeriod;
                obj.EndMinutes = endPeriod;


                
                if (flag)
                {
                    dt = date.AddDays(1);
                    obj.EndDate = dt.Date.AddHours(0).AddMinutes(endPeriod).AddSeconds(0);
                }
                lstResourceSlotVOs.Add(obj);
            }
            return lstResourceSlotVOs;
        }

        private int GetShiftsBySlot(List<ShiftVO> lstShifts, int startPeriod)
        {
            int shiftid = 0;
            foreach (ShiftVO shiftvo in lstShifts)
            {                
                
                DateTime stime = Convert.ToDateTime(shiftvo.StartTime, CultureInfo.InvariantCulture);

                DateTime etime = Convert.ToDateTime(shiftvo.EndTime, CultureInfo.InvariantCulture);

                double starttime = stime.TimeOfDay.TotalMinutes;

                double endtime = etime.TimeOfDay.TotalMinutes;

                double startminutes = startPeriod * 60;

                if (startminutes >= starttime && startminutes < endtime)
                {
                    shiftid = shiftvo.ShiftID;
                    break;
                }
            }

            return shiftid;
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get list of resources based on VCN and date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>

        private List<ResourceAllocationSlotVO> ConstructSlotList(List<ResourceAllocationVO> ResourceAllocationVO, DateTime date, string portcode)
        {
            List<ResourceAllocationSlotVO> lstResourceAllocationSlotVOs = new List<ResourceAllocationSlotVO>();

            ResourceAllocationSlotVO slotvo = null;
            ResourceSlotVO _resSlot = new ResourceSlotVO();


            foreach (ResourceAllocationVO obj in ResourceAllocationVO)
            {
                _resSlot.ServiceTypeCode = obj.ServiceTypeCode;

                ResourceSlotVO resSlot = lstSlotConfig.Find(r => r.SlotPeriod == obj.AllocSlot && r.AllocationDate.Date == obj.AllocationDate.Date);
                if (resSlot != null)
                {
                    int? userid = null;
                    string userName = string.Empty;

                        if (obj.ResourceID == 0)
                        {

                            int _shiftid = lstSlotConfig.Find(s => s.SlotPeriod == resSlot.SlotPeriod).ShiftID;
                            DateTime _allocationDate = lstSlotConfig.Find(s => s.SlotPeriod == resSlot.SlotPeriod).AllocationDate;

                            _resSlot.ShiftID = _shiftid;
                            _resSlot.AllocationDate = _allocationDate;
                            //getting list of users besed on port ,current date , designation and employee attendence

                            List<IdNameVO> lstusers = GetSearchResource(_resSlot, portcode);

                            //getting list of users in resource allocation users based on current date (ie.. allocated user )
                            List<ResourceAllocation> lstresc = _unitOfWork.SqlQuery<ResourceAllocation>("select * from ResourceAllocation where ServiceReferenceType = @p0 and CONVERT(DATE,AllocationDate) = @p1", "SUPP", obj.AllocationDate.Date).ToList();

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
                                            userName = user.Name;
                                            checkuser = false;
                                            break;
                                        }
                                    }

                                    if (checkuser)
                                    {
                                        lstresc = _unitOfWork.Repository<ResourceAllocation>().Query().Select()
                                            .Where(r => r.ServiceReferenceType == ServiceReferenceType.SupplementoryService && Convert.ToDateTime(r.AllocationDate, CultureInfo.InvariantCulture).ToShortDateString() == Convert.ToDateTime(obj.AllocationDate).ToShortDateString()).ToList();
                                        lstusers = GetSearchResource(_resSlot, portcode);

                                        var lst = lstresc.FindAll(c => c.TaskStatus == ResourceAllcationWorkFlowStatus.Completed || c.TaskStatus == ResourceAllcationWorkFlowStatus.Verified);
                                        lstresc = lstresc.FindAll(c => c.TaskStatus != ResourceAllcationWorkFlowStatus.Completed || c.TaskStatus != ResourceAllcationWorkFlowStatus.Verified);

                                        lstusers = lstusers.Where(a => !lstresc.Any(a1 => a1.ResourceID == a.ID)).ToList();

                                        if (lstusers.Count > 0)
                                        {
                                            if (lstusers.Count == 1)
                                            {
                                                userid = lstusers[0].ID;
                                                userName = lstusers[0].Name;
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
                                                        userName = user.Name;
                                                    }
                                                    else if (temp < _count)
                                                    {
                                                        temp = _count;
                                                        userid = user.ID;
                                                        userName = user.Name;
                                                    }
                                                    else
                                                    {
                                                        temp = _count;
                                                        userid = user.ID;
                                                        userName = user.Name;
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
                                } while (checkuser);
                            }
                        }
                        else
                        {
                            userid = obj.ResourceID;
                            userName = obj.Name;
                        }

                    if (resSlot != null)
                    {
                        int slotno = resSlot.SlotNumber - 1;

                        ResourceSlotVO slot = new ResourceSlotVO();
                        slot.SlotNumber = resSlot.SlotNumber;
                        if (obj.TaskStatus == ResourceAllcationWorkFlowStatus.Scheduled)
                        {
                            if (userid > 0)
                            {
                                slot.ResourceID = userid;
                                slot.ResourceName = userName;
                                slot.TaskStatus = obj.TaskStatus;
                                _unitOfWork.ExecuteSqlCommand("update dbo.ResourceAllocation SET ResourceID = @p0 where ResourceAllocationID = @p1", userid, obj.ResourceAllocationID);
                            }
                            else
                            {
                                slot.ResourceID = obj.ResourceID;
                                slot.ResourceName = obj.Name ?? "Unscheduled";
                                slot.TaskStatus = obj.ResourceID > 0 ? obj.TaskStatus : "PNDG";
                            }
                        }
                        else
                        {
                            slot.ResourceID = obj.ResourceID;
                            slot.ResourceName = obj.Name ?? "Unscheduled";
                            slot.TaskStatus = obj.ResourceID > 0 ? obj.TaskStatus : "PNDG";
                        }

                        slot.ServiceTypeCode = obj.ServiceTypeCode;
                        slot.IsCraft = false;
                        slot.ServiceReferenceID = obj.ServiceReferenceID;
                        slot.AllocationDate = obj.AllocationDate;

                        string strResourceName = slot.ResourceName;


                        if (strResourceName.Length > 18)
                        {
                            slot.ResourceName = strResourceName.Substring(0, 16) + "...";
                        }
                        else
                        {
                            slot.ResourceName = strResourceName;
                        }


                        slotvo = new ResourceAllocationSlotVO();
                        slotvo.ResourceAllocationID = obj.ResourceAllocationID;
                        slotvo.RecordStatus = obj.RecordStatus;
                        slotvo.ResourceSlots = GetSlotConfiguration(date, portcode);
                        slotvo.ResourceSlots[slotno] = slot;
                        slotvo.ServiceReferenceType = obj.ServiceReferenceType;
                        slotvo.ServiceReferenceID = obj.ServiceReferenceID;
                        slotvo.ServiceTypeCode = obj.ServiceTypeCode;
                        slotvo.CreatedBy = obj.CreatedBy;
                        slotvo.CreatedDate = obj.CreatedDate.ToShortDateString();
                        slotvo.AllocSlot = obj.AllocSlot;
                        slotvo.StartTime = Convert.ToString(obj.StartTime, CultureInfo.InvariantCulture);
                        slotvo.EndTime = Convert.ToString(obj.EndTime, CultureInfo.InvariantCulture);
                        slotvo.VCN = obj.VCN;
                        slotvo.VesselName = obj.VesselName;
                        slotvo.AllocationDate = Convert.ToString(obj.AllocationDate, CultureInfo.InvariantCulture);
                        slotvo.Quantity = obj.Quantity;
                        slotvo.BerthCode = obj.BerthCode;
                        slotvo.BerthName = obj.BerthName;
                        slotvo.ServiceTypeName = obj.ServiceTypeName;
                        slotvo.AcknowledgeDate = obj.AcknowledgeDate;
                        slotvo.TaskStatus = obj.TaskStatus;
                        slotvo.ResourceID = obj.ResourceID ?? 0;

                        slotvo.AnyDangerousGoodsonBoard = obj.AnyDangerousGoodsonBoard;

                        lstResourceAllocationSlotVOs.Add(slotvo);

                    }
                }
            }

            return lstResourceAllocationSlotVOs;
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 29th Dec 2014
        /// Purpose : To get PortName by PortCode
        /// </summary>
        /// <param name="portcode"></param>
        /// <returns></returns>
        public string GetPortNameByPortCode(string portcode)
        {
            var port = (from p in _unitOfWork.Repository<Port>().Queryable()
                        where p.PortCode == portcode
                        select p).SingleOrDefault();

            return port.PortName;
        }

        public ResourceAllocationVO GetResourceAllocationByID(int resourceAllocationId)
        {
            var resourceAllocation = (from ra in _unitOfWork.Repository<ResourceAllocation>().Query().Select()
                                      join ssr in _unitOfWork.Repository<SuppServiceRequest>().Query().Select() on ra.ServiceReferenceID equals ssr.SuppServiceRequestID
                                      join b in _unitOfWork.Repository<Berth>().Query().Select() on ssr.BerthCode equals b.BerthCode
                                      join an in _unitOfWork.Repository<ArrivalNotification>().Query().Select() on ssr.VCN equals an.VCN
                                      join v in _unitOfWork.Repository<Vessel>().Query().Select() on an.VesselID equals v.VesselID
                                      join st in _unitOfWork.Repository<ServiceType>().Query().Select() on ra.OperationType equals st.ServiceTypeCode
                                      join s in _unitOfWork.Repository<SubCategory>().Query().Select() on ra.ServiceReferenceType equals s.SubCatCode
                                      where ra.ResourceAllocationID == resourceAllocationId && ra.ServiceReferenceType == ServiceReferenceType.SupplementoryService
                                      select new ResourceAllocationVO
                                      {
                                          ResourceAllocationID = ra.ResourceAllocationID,
                                          OperationType = ra.OperationType,
                                          AllocationDate = Convert.ToDateTime(ra.AllocationDate, CultureInfo.InvariantCulture),
                                          ResourceID = ra.ResourceID,
                                          TaskStatus = ra.TaskStatus,
                                          AllocSlot = ra.AllocSlot,
                                          ServiceReferenceID = ra.ServiceReferenceID,
                                          ServiceReferenceType = ra.ServiceReferenceType,
                                          ServiceReferenceTypeName = s.SubCatName,
                                          VCN = an.VCN,
                                          VesselName = v.VesselName,
                                          BerthName = b.BerthName,
                                          Quantity = Convert.ToInt64(ssr.Quantity, CultureInfo.InvariantCulture),
                                          OperationTypeName = st.ServiceTypeName

                                      }).SingleOrDefault();

            return resourceAllocation;
        }

        private List<IdNameVO> GetSearchCraft(ResourceSlotVO resourceSlot, string portcode)
        {
            var crafts = (from c in _unitOfWork.Repository<Craft>().Query().Select()
                          join std in _unitOfWork.Repository<ServiceTypeDesignation>().Query().Select() on c.CraftType equals std.CraftType
                          join st in _unitOfWork.Repository<ServiceType>().Query().Select() on std.ServiceTypeID equals st.ServiceTypeID
                          where std.PortCode == portcode && st.ServiceTypeCode == resourceSlot.ServiceTypeCode && c.CraftCommissionStatus == CraftStatus.InCommission && std.CraftType == c.CraftType && c.RecordStatus == RecordStatus.Active && c.PortCode == portcode

                          select new IdNameVO
                          {
                              ID = c.CraftID,
                              Name = c.CraftName

                          }).ToList();

            return crafts;
        }

        public string GetRoleByUser(int userid, int FloatingCraneRoleID, int WaterManRoleID)
        {
            var roles = _accountRepository.GetUserRole(userid);

            var FloatingCrane = roles.Find(f => f.RoleID == FloatingCraneRoleID);
            var WaterMan = roles.Find(w => w.RoleID == WaterManRoleID);

            if (WaterMan != null)
            {
                return ServiceTypeCode.WaterService;
            }
            else if (FloatingCrane != null)
            {
                return ServiceTypeCode.FloatingCrane;
            }
            else
            {
                return null;
            }
        }

        public List<String> GetVCNDetails(string portcode, string servicetype, DateTime date)
        {
            var lstVCN = _unitOfWork.SqlQuery<string>("SELECT Distinct a.VCN FROM SuppServiceRequest s JOIN ArrivalNotification a on s.VCN = a.VCN JOIN ResourceAllocation r on s.SuppServiceRequestID = r.ServiceReferenceID JOIN ServiceType st on r.OperationType = st.ServiceTypeCode WHERE  r.OperationType = @p0 AND r.ServiceReferenceType = 'SUPP' AND s.PortCode = @p1 AND r.RecordStatus='A' AND CONVERT (DATE, r.AllocationDate) BETWEEN CONVERT(DATE, @p2) AND DATEADD(day,1,CONVERT (DATE, @p2))", servicetype, portcode, date).ToList();

            return lstVCN;
        }

        public List<ResourceSlotVO> GetActiveResourceSlots(string portcode)
        {
            var activeSlots = new List<ResourceSlotVO>();

            DateTime systemDate = DateTime.Now;
            int hour = systemDate.Hour;
            bool status = false;

            var resourceAllocationSlotVOs = GetSlotConfiguration(systemDate, portcode);

            var pendingSlots = new List<ResourceSlotVO>();

            foreach (var slot in resourceAllocationSlotVOs)
            {
                string[] period = slot.SlotPeriod.Split('-');

                if (hour >= int.Parse(period[0], CultureInfo.InvariantCulture) && hour < int.Parse(period[1], CultureInfo.InvariantCulture))
                {
                    status = true;
                }
                else
                {
                    if (!status)
                    {
                        pendingSlots.Add(slot);
                    }
                }
            }

            activeSlots = resourceAllocationSlotVOs.Where(a => !pendingSlots.Any(a1 => a1.SlotPeriod == a.SlotPeriod)).ToList();

            return activeSlots;
        }

    }
}
