using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Globalization;
using System.Data.Entity;
using log4net;

namespace IPMS.Repository
{
    public class AutomatedSlottingRepository : IAutomatedSlottingRepository
    {
        private IUnitOfWork _unitOfWork;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository = null;
        private IAccountRepository _accountRepository = null;
        private readonly ILog log;

        public AutomatedSlottingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            log = LogManager.GetLogger(typeof (AutomatedSlottingRepository));
        }

        public List<VesselCallMovementVO> GetUnPlannedVesselDetails(DateTime slotDate, string portCode, int userId, string userType)
        {
            var tasktype = new SqlParameter("@TaskType", "UNPLND");
            var slotDateTime = new SqlParameter("@SlotDate", slotDate);
            var port = new SqlParameter("@PortCode", portCode);
            List<VesselCallMovementVO> vessel = _unitOfWork.SqlQuery<VesselCallMovementVO>("usp_GetAutomatedSlotAllocationDet @TaskType,@SlotDate,@PortCode", tasktype, slotDateTime, port).ToList();

            foreach (VesselCallMovementVO s in vessel)
            {
                var IsPresent = (from slotOver in _unitOfWork.Repository<SlotOverRidingReasons>().Query().Select()
                                 where slotOver.VesselCallMovementID == s.VesselCallMovementID
                                 select slotOver).Count();
                if (IsPresent>=1)
                {
                    var OverrideDetails = (from a in _unitOfWork.Repository<SlotOverRidingReasons>().Query().Select()
                                           join p in _unitOfWork.Repository<SubCategory>().Query().Select()
                                             on a.ReasonCode equals p.SubCatCode
                                           where a.VesselCallMovementID == s.VesselCallMovementID
                                           select new VesselCallMovementVO
                                           {
                                               ReasonName = p.SubCatName,
                                               EnteredDateTime = a.EnteredDateAndTime,
                                           }).OrderByDescending(a => a.EnteredDateTime).ToList();
                   
                    if (OverrideDetails.Count() > 1)
                    {                      
                        foreach (var item in OverrideDetails)
                        {
                           // data.ETA.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                            var value = item.ReasonName + " - " + item.EnteredDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture); 
                            if (s.ReasonForDisplay == null)
                            {
                                s.ReasonForDisplay = value;
                            }
                            else
                            {
                                s.ReasonForDisplay = value + " , " + s.ReasonForDisplay;
                            }
                            value = string.Empty;
                        }
                    }
                    else
                    {
                        var vesselCallMovementVo = OverrideDetails.FirstOrDefault();
                        if (vesselCallMovementVo != null)
                            s.ReasonForDisplay = vesselCallMovementVo.ReasonName + " - " + vesselCallMovementVo.EnteredDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                    }
                }
                else
                s.ReasonForDisplay = "Nil";
            }
            return vessel;
        }

        public AutomatedSlotConfigurationVO GetAutomatedConfigurationDetails(DateTime slotDate, string portCode)
        {
            var configVO = _unitOfWork.Repository<AutomatedSlotConfiguration>().Queryable().Include(x => x.SlotPriorityConfigurations)
               .Include(x => x.SlotPriorityConfigurations.Select(s => s.SubCategory)).Where(x => x.PortCode == portCode && DbFunctions.TruncateTime(x.EffectiveFrm) <= slotDate)
               .OrderByDescending(x => x.EffectiveFrm).FirstOrDefault();

            return configVO.MapToDTO();
        }

        public AutomatedSlotConfigurationVO GetExtendableYesNo(string portCode, DateTime slotDate)
        {

            var paramPortCode = new SqlParameter("@portcode", portCode);
            var paramSlotDate = new SqlParameter("@slotdate", slotDate);
            var ExtendYn = _unitOfWork.SqlQuery<AutomatedSlotConfigurationVO>("dbo.usp_GetAutomatedSlotExtendYn @portcode, @slotdate", paramPortCode, paramSlotDate).FirstOrDefault();
            return ExtendYn;
        }

        public List<VesselCallMovementVO> GetPlannedVesselDetails(DateTime slotDate, string portCode, int userId, string userType)
        {
            List<ResourceSlotVO> lstslots = _suppServiceResourceAllocRepository.GetSlotConfiguration(slotDate, portCode);

            var tasktype = new SqlParameter("@TaskType", "PLND");
            var slotDateTime = new SqlParameter("@SlotDate", slotDate);
            var port = new SqlParameter("@PortCode", portCode);

            List<VesselCallMovementVO> vessel = _unitOfWork.SqlQuery<VesselCallMovementVO>("usp_GetAutomatedSlotAllocationDet @TaskType,@SlotDate,@PortCode", tasktype, slotDateTime, port).ToList();
            List<VesselCallMovementVO> dataList = new  List<VesselCallMovementVO>();
            dataList.AddRange(vessel);
           var count2=  dataList.Count != dataList.Distinct().Count();
            var totalvessels = new List<VesselCallMovementVO>();

            AutomatedSlotConfigurationVO slotConfigDet = GetAutomatedConfigurationDetails(slotDate, portCode);
            List<SlotPriorityConfigurationVO> lstSlotpriorities = slotConfigDet.SlotPriorityConfigurations;

            var distinctItems = lstSlotpriorities.GroupBy(x => x.Priority).Select(y => y.First());
            var prioritylist = distinctItems.OrderBy(x => x.Priority);
            int totalslotcount = default(int);
            var extendableslot = GetExtendableYesNo(portCode, slotDate);
            if (extendableslot != null)
            {
                totalslotcount = extendableslot.ExtendYn == "Y" ? slotConfigDet.NoofSlots + slotConfigDet.ExtendableSlots : slotConfigDet.NoofSlots;
            }
            else
            {
                totalslotcount = slotConfigDet.NoofSlots;
            }

            var groupbyslot = vessel.GroupBy(item => item.Slot);
            var slotgrp = groupbyslot.Select(grp => grp.OrderBy(item => item.Slot).First());

            foreach (VesselCallMovementVO s in slotgrp)
            {
                var resslotvo = lstslots.Find(a => Convert.ToDateTime(s.SlotDate, CultureInfo.InvariantCulture) >= a.StartDate && Convert.ToDateTime(s.SlotDate, CultureInfo.InvariantCulture) <= a.EndDate);


                if (resslotvo != null)
                {
                    //var slotcount = vessel.FindAll(a => Convert.ToDateTime(a.SlotDate, CultureInfo.InvariantCulture) >= resslotvo.StartDate && Convert.ToDateTime(a.SlotDate, CultureInfo.InvariantCulture) <= resslotvo.EndDate);

                    var slotcount = vessel.FindAll(a => Convert.ToDateTime(a.SlotDate, CultureInfo.InvariantCulture) == Convert.ToDateTime(s.SlotDate, CultureInfo.InvariantCulture));

                    var usedvessels = slotcount.FindAll(c => c.SlotStatus != "PLND");
                    var plannedvessels = slotcount.FindAll(c => c.SlotStatus != "PLND");
                    var unplannedvessels = slotcount.FindAll(d => d.SlotStatus == "PLND");

                    foreach (SlotPriorityConfigurationVO config in prioritylist)
                    {
                        var lst = unplannedvessels.FindAll(h => h.VesselType == config.VesselTypeName);
                        if (lst.Count > 0)
                        {
                            var vesselcount = usedvessels.FindAll(i => i.VesselType == config.VesselTypeName).Count;

                            int count = config.NoofVessels - vesselcount;

                            if (count > 0)
                            {
                                lst = lst.Take(count).ToList();
                                plannedvessels.AddRange(lst);
                                unplannedvessels.RemoveAll(items => lst.Contains(items));
                            }
                            else
                            {
                                plannedvessels.AddRange(lst);
                                unplannedvessels.RemoveAll(items => lst.Contains(items));
                            }
                        }
                    }

                    if (unplannedvessels.Count > 0)
                    {
                        plannedvessels.AddRange(unplannedvessels);
                    }

                    //if (slotDate.Date >= DateTime.Now.Date)
                    //{
                    //    if (plannedvessels.Count > totalslotcount)
                    //    {
                    //        foreach (var itm in plannedvessels)
                    //        {
                    //            if (itm.SlotStatus == AutomatedSlotStatus.Planned)
                    //            {
                    //                _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus = @p0 where VesselCallMovementID = @p1", "PEND", itm.VesselCallMovementID);
                    //                plannedvessels.Remove(itm);
                    //            }
                    //        }
                    //    }

                    //    //while (plannedvessels.Count > totalslotcount)
                    //    //{
                    //    //    if (plannedvessels[totalslotcount].SlotStatus == AutomatedSlotStatus.Planned)
                    //    //    {
                    //    //        _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus = @p0 where VesselCallMovementID = @p1", "PEND", plannedvessels[totalslotcount].VesselCallMovementID);
                    //    //        plannedvessels.Remove(plannedvessels[totalslotcount]);
                    //    //    }
                    //    //}
                    //}

                    foreach (SlotPriorityConfigurationVO config in prioritylist)
                    {
                        var lst = plannedvessels.FindAll(h => h.VesselType == config.VesselTypeName);
                        if (lst.Count > 0)
                        {
                            var vesselcount = plannedvessels.FindAll(i => i.VesselType == config.VesselTypeName).Count;

                            if (vesselcount > config.NoofVessels)
                            {
                                lst = lst.Take(config.NoofVessels).ToList();
                                totalvessels.AddRange(lst);
                                plannedvessels.RemoveAll(items => lst.Contains(items));
                            }
                            else
                            {
                                totalvessels.AddRange(lst);
                                plannedvessels.RemoveAll(items => lst.Contains(items));
                            }
                        }
                    }

                    if (plannedvessels.Count > 0)
                    {
                        totalvessels.AddRange(plannedvessels);
                    }
                }
    
            }
            foreach (VesselCallMovementVO s in totalvessels)
            {
                var IsPresent = (from slotOver in _unitOfWork.Repository<SlotOverRidingReasons>().Query().Select()
                                 where slotOver.VesselCallMovementID == s.VesselCallMovementID
                                 select slotOver).Count();
                if (IsPresent>=1)
                {
                    var OverrideDetails = (from a in _unitOfWork.Repository<SlotOverRidingReasons>().Query().Select()
                                           join p in _unitOfWork.Repository<SubCategory>().Query().Select()
                                             on a.ReasonCode equals p.SubCatCode
                                           where a.VesselCallMovementID == s.VesselCallMovementID
                                           select new VesselCallMovementVO
                                           {
                                               ReasonName = p.SubCatName,
                                               EnteredDateTime = a.EnteredDateAndTime,
                                           }).OrderByDescending(a => a.EnteredDateTime).ToList();
                    if (OverrideDetails.Count() > 1)
                    {
                        foreach (var item in OverrideDetails)
                        {
                            //Convert.ToString(item.EnteredDateTime, CultureInfo.InvariantCulture) 
                            var value = item.ReasonName + " - " + item.EnteredDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                            log.Info("Entered time for VCN " + item.VCN + "Date Time" + value);
                            if (s.ReasonForDisplay == null)
                            {
                                s.ReasonForDisplay = value;
                            }
                            else
                            {
                                s.ReasonForDisplay = value + " , " + s.ReasonForDisplay;
                            }
                            value = string.Empty;
                        }
                    }
                    else
                    {
                        var vesselCallMovementVo = OverrideDetails.FirstOrDefault();
                        if (vesselCallMovementVo != null)
                            s.ReasonForDisplay = vesselCallMovementVo.ReasonName + " - " + vesselCallMovementVo.EnteredDateTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        log.Info("Entered time for VCN " + s.VCN + "Date Time" + s.ReasonForDisplay);
                    }
                }
                else
                    s.ReasonForDisplay = "Nil";
            }
            
            
            return totalvessels;
        }

        public int UpdateVesselSlotDetails(List<VesselCallMovementVO> slotDetails)
        {
            int vesselCallmovementID = default(int);

            return vesselCallmovementID;
        }

        public string GetSlotPeriodBySlotDate(DateTime slotDateTime, string port)
        {
            int hours = slotDateTime.Hour;
            string slotPeriod = string.Empty;
            double totalminutes = slotDateTime.TimeOfDay.TotalMinutes;

            if (slotDateTime != DateTime.MinValue)
            {
                foreach (ResourceSlotVO slot in _suppServiceResourceAllocRepository.GetSlotConfiguration(slotDateTime, port))
                {
                    string[] period = slot.SlotPeriod.Split('-');

                    DateTime sttime = Convert.ToDateTime(period[0], CultureInfo.InvariantCulture);

                    DateTime edtime = Convert.ToDateTime(period[1], CultureInfo.InvariantCulture);

                    double startTime = sttime.TimeOfDay.TotalMinutes;

                    double endTime = edtime.TimeOfDay.TotalMinutes;

                    if (startTime > endTime)
                    {
                        if (totalminutes <= endTime && startTime >= totalminutes)
                        {
                            slotPeriod = slot.SlotPeriod;
                            break;
                        }
                        if (totalminutes >= endTime && startTime <= totalminutes)
                        {
                            slotPeriod = slot.SlotPeriod;
                            break;
                        }
                    }

                    if (totalminutes >= startTime && totalminutes < endTime)
                    {
                        slotPeriod = Convert.ToString(startTime, CultureInfo.InvariantCulture) + '-' + Convert.ToString(endTime, CultureInfo.InvariantCulture);
                        break;
                    }
                }
            }
            return slotPeriod;
        }

        /// <summary>
        /// To Get Vessel Call Movement details based on VesselCallMovementID for Electronic Notifications
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VesselCallMovementVO GetVesselCallMovementId(string valVesselCallMovementId)
        {
            int VesselCallMovementID = Int32.Parse(valVesselCallMovementId, CultureInfo.InvariantCulture);

            var serviceRequestDetails = (from a in _unitOfWork.Repository<VesselCallMovement>().Query().Select()
                                         join v in _unitOfWork.Repository<SubCategory>().Query().Select()
                                         on a.MovementType equals v.SubCatCode
                                         join p in _unitOfWork.Repository<Port>().Query().Select()
                                         on a.FromPositionPortCode equals p.PortCode
                                         where a.VesselCallMovementID == VesselCallMovementID
                                         select new VesselCallMovementVO
                                         {
                                             VCN = a.VCN
                                             ,
                                             ATB = a.ATB
                                             ,
                                             ATUB = a.ATUB
                                             ,
                                             SlotStatus = a.SlotStatus
                                             ,
                                             SlotDate = a.SlotDate
                                             ,
                                             Slot = a.Slot
                                             ,
                                             MovementDateTime = a.MovementDateTime
                                             ,
                                             MovementType = a.MovementType
                                             ,
                                             PortCode = a.FromPositionPortCode
                                             ,
                                             PortName = p.PortName

                                         }).FirstOrDefault<VesselCallMovementVO>();

            return serviceRequestDetails;

        }

        public VesselCallMovementVO GetVesselCallMovementVORep(string strVesselCallMovementId)
        {
            int iVesselCallMovementID = int.Parse(strVesselCallMovementId, CultureInfo.InvariantCulture);


            VesselCallMovement VesselCallMovementVOdetails = _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(vcm => vcm.VesselCallMovementID == iVesselCallMovementID).FirstOrDefault();
            VesselCallMovementVO returnVesselCallMovementVO = VesselCallMovementVOdetails.MapToDto();
            User Userdetails = _unitOfWork.Repository<User>().Queryable().Where(vcm => vcm.UserID == VesselCallMovementVOdetails.ModifiedBy).FirstOrDefault();


            ArrivalNotification ArrivalNotificationdetails = _unitOfWork.Repository<ArrivalNotification>().Queryable().Where(vcm => vcm.VCN == VesselCallMovementVOdetails.VCN).FirstOrDefault();


            SubCategory SubCategorydetails = _unitOfWork.Repository<SubCategory>().Queryable().Where(vcm => vcm.SubCatCode == VesselCallMovementVOdetails.MovementType).FirstOrDefault();


            Vessel Vesseldetails = _unitOfWork.Repository<Vessel>().Queryable().Where(vcm => vcm.VesselID == ArrivalNotificationdetails.VesselID).FirstOrDefault();

            returnVesselCallMovementVO.UserName = Userdetails.UserName;


            Port Portdetails = _unitOfWork.Repository<Port>().Queryable().Where(vcm => vcm.PortCode == ArrivalNotificationdetails.PortCode).FirstOrDefault();
            returnVesselCallMovementVO.PortCode = ArrivalNotificationdetails.PortCode;
            returnVesselCallMovementVO.PortName = Portdetails.PortName;
            returnVesselCallMovementVO.VesselName = Vesseldetails.VesselName;
            returnVesselCallMovementVO.MovementType = SubCategorydetails.SubCatName;
            returnVesselCallMovementVO.MovementDateTime = VesselCallMovementVOdetails.MovementDateTime;


            return returnVesselCallMovementVO;

        }

        public bool GetPrivilegesByUserIdAndEntityCode(int userId, string entityCode)
        {
            bool result = false;


            var entity = (from e in _unitOfWork.Repository<Entity>().Queryable()
                          where e.EntityCode == entityCode
                          select e).SingleOrDefault();

            var user = _unitOfWork.Repository<User>().Find(userId);

            PrivilegeVO privilege = new PrivilegeVO();

            privilege.Privileges = _accountRepository.GetUserPrivilegesWithControllerName(entity.ControllerName, user.UserName);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                if (privilege.HasAddPrivilege || privilege.HasEditPrivilege)
                {
                    result = true;
                }
            }

            return result;
        }

        public List<string> GetActiveSlots(string portCode)
        {

            var activeSlots = new List<string>();

            DateTime systemDate = DateTime.Now;
            int hour = systemDate.Hour;
            bool status = false;
            double totalminutes = systemDate.TimeOfDay.TotalMinutes;

            var resourceAllocationSlotVOs = _suppServiceResourceAllocRepository.GetSlotConfiguration(systemDate, portCode);

            var pendingSlots = new List<ResourceSlotVO>();

            foreach (var slot in resourceAllocationSlotVOs)
            {
                string[] period = slot.SlotPeriod.Split('-');

                DateTime sttime = Convert.ToDateTime(period[0], CultureInfo.InvariantCulture);

                DateTime edtime = Convert.ToDateTime(period[1], CultureInfo.InvariantCulture);

                double starttime = sttime.TimeOfDay.TotalMinutes;

                double endtime = edtime.TimeOfDay.TotalMinutes;

                if (starttime > endtime)
                {
                    endtime = starttime + (slot.Duration - 1);
                }

                if (totalminutes >= starttime && totalminutes < endtime)
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

            activeSlots = resourceAllocationSlotVOs.Where(a => !pendingSlots.Any(a1 => a1.SlotPeriod == a.SlotPeriod)).Select(s => s.SlotPeriod).ToList();

            return activeSlots;
        }

        public List<AutomatedSlotBlockingVO> GetBlockedSlots(DateTime slotDate, string portCode)
        {

            var allblockedSlots = new List<AutomatedSlotBlockingVO>();

            DateTime systemDate = DateTime.Now;
            int hour = systemDate.Hour;
            bool status = false;
            double totalminutes = systemDate.TimeOfDay.TotalMinutes;

            var resourceAllocationSlotVOs = _suppServiceResourceAllocRepository.GetSlotConfiguration(slotDate, portCode);

            var pendingSlots = new List<ResourceSlotVO>();

            var blockSlots = (from sb in _unitOfWork.Repository<AutomatedSlotBlocking>().Queryable()
                               .Include(sb => sb.SubCategory).Where(sb => sb.RecordStatus == RecordStatus.Active && sb.PortCode == portCode)
                              select sb).ToList<AutomatedSlotBlocking>();

            var blockedSlotsList = blockSlots != null ? blockSlots.MapToDto() : null;

            foreach (var slot in resourceAllocationSlotVOs)
            {
                string[] slotperiod = slot.SlotPeriod.Split('-');

                DateTime slotSttime = Convert.ToDateTime(slotperiod[0], CultureInfo.InvariantCulture);

                double slotStartMinutes = slotSttime.TimeOfDay.TotalMinutes;

                DateTime slotStartDate = slot.AllocationDate.Date.AddHours(0).AddMinutes(slotStartMinutes).AddSeconds(0);

                foreach (var block in blockedSlotsList)
                {
                    string[] blockstartSlot = block.SlotFrom.Split('-');

                    string[] blockendSlot = block.SlotTo.Split('-');

                    DateTime blcksttime = Convert.ToDateTime(blockstartSlot[0], CultureInfo.InvariantCulture);

                    DateTime blckedtime = Convert.ToDateTime(blockendSlot[1], CultureInfo.InvariantCulture);

                    double blockStartMinutes = blcksttime.TimeOfDay.TotalMinutes;
                    double blockEndTime = blckedtime.TimeOfDay.TotalMinutes;


                    block.SlotFromDate = block.SlotFromDate.Date.AddHours(0).AddMinutes(blockStartMinutes).AddSeconds(0);

                    DateTime BlockToDate = new DateTime();

                    BlockToDate = block.SlotToDate;

                    if (blockStartMinutes > blockEndTime)
                    {
                        BlockToDate = BlockToDate.AddDays(1);
                    }

                    BlockToDate = BlockToDate.Date.AddHours(0).AddMinutes(blockEndTime).AddSeconds(0);



                    if (block.SlotFromDate <= slotStartDate && BlockToDate > slotStartDate)
                    {
                        AutomatedSlotBlockingVO item = new AutomatedSlotBlockingVO();
                        item.SlotPeriod = slot.SlotPeriod;
                        item.FromDate = block.FromDate;
                        item.ToDate = block.ToDate;
                        item.ReasonName = block.ReasonName;
                        item.SlotNumber = slot.SlotNumber;
                        allblockedSlots.Add(item);
                    }
                }
            }

            return allblockedSlots;
        }

        //public string GetPreviousOverriddenSlot(int vesselCallMovementID)
        //{
        //    var list = (from a in _unitOfWork.Repository<SlotOverRidingReasons>().Queryable().Where(a => a.VesselCallMovementID == vesselCallMovementID).OrderByDescending(a => a.OverRideSlotID)                                           
        //                                    select a).FirstOrDefault();
        //    string PreviousOverridenSlot = list != null ? (list.OverriddenSlot != null ? list.OverriddenSlot : string.Empty) : string.Empty;
        //    return PreviousOverridenSlot;
        //}

        public SlotOverRidingReasonsVO GetPreviousOverriddenSlot(int vesselCallMovementID)
        {
            var list = (from a in _unitOfWork.Repository<SlotOverRidingReasons>().Queryable().Where(a => a.VesselCallMovementID == vesselCallMovementID).OrderByDescending(a => a.OverRideSlotID)
                        select new SlotOverRidingReasonsVO
                        {
                            PreviousSlotDate=a.EnteredDateAndTime,
                            PreviousSlotDis=a.OverriddenSlot
                        }).FirstOrDefault();
            return list != null ?list:null;
        }
        //public string GetPreviousPlannedSlot(int vesselCallMovementID)
        //{
        //    var slot = (from a in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(a => a.VesselCallMovementID == vesselCallMovementID).OrderByDescending(a => a.CreatedDate)
        //                where a.VesselCallMovementID == vesselCallMovementID
        //                select a.Slot).FirstOrDefault().ToString();
        //    return slot;
        //}

        public VesselCallMovementVO GetPreviousPlannedSlot(int vesselCallMovementID)
        {
            var list = (from a in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(a => a.VesselCallMovementID == vesselCallMovementID).OrderByDescending(a => a.CreatedDate)
                        select new VesselCallMovementVO
                        {
                            PreviousSlotDate = a.SlotDate,
                            PreviousSlotDis = a.Slot,
                            PreviousSlotStatus=a.SlotStatus
                        }).FirstOrDefault();
            return list;
        }
    }
}
