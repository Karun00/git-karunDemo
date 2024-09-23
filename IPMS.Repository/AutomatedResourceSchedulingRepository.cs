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
using System.Text.RegularExpressions;

namespace IPMS.Repository
{
    public class AutomatedResourceSchedulingRepository : IAutomatedResourceSchedulingRepository
    {
        private IUnitOfWork _unitOfWork;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository = null;
        List<ResourceSlotVO> lstSlotConfig = null;

        public AutomatedResourceSchedulingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
        }

        public List<VesselCallMovementVO> GetPendingMovementsForAllocation(string portCode)
        {
            var code = new SqlParameter("@PortCode", portCode);

            var srq = _unitOfWork.SqlQuery<VesselCallMovementVO>("usp_GetPendingMovementsForAllocation @PortCode", code).ToList();

            return srq.ToList();
        }

        public List<MovementResourceAllocationVO> GetResourceAllocations(string portCode, DateTime slotDate)
        {
            var code = new SqlParameter("@PortCode", portCode);
            var date = new SqlParameter("@SlotDate", slotDate);
            var rscall = _unitOfWork.SqlQuery<ResourceAllocationVO>("usp_AutomatedResourceAllocationList @PortCode,@SlotDate", code, date).ToList();

            lstSlotConfig = _suppServiceResourceAllocRepository.GetSlotConfiguration(slotDate, portCode);

            foreach (var item in rscall)
            {
                Match pattern = Regex.Match(item.AllocSlot, @"(\d+)(\d+):(\d+)(\d+)-(\d+)(\d+):(\d+)(\d+)", RegexOptions.IgnorePatternWhitespace);

                if (pattern.Success == false)
                {
                    var period = item.AllocSlot.Split('-');

                    if (period != null)
                    {
                        var stratTime = Convert.ToInt32(period[0]) * 60;

                        var endTime = Convert.ToInt32(period[1]) * 60;

                        TimeSpan startslot = TimeSpan.FromMinutes(stratTime);

                        TimeSpan endsolt = TimeSpan.FromMinutes(endTime);

                        item.AllocSlot = startslot.ToString(@"hh\:mm") + "-" + endsolt.ToString(@"hh\:mm");
                    }
                }

            }

            List<MovementResourceAllocationVO> movres = ConstructSlotDetails(rscall.ToList(), slotDate, portCode);

            return movres;
        }

        public List<MovementResourceAllocationVO> ConstructSlotDetails(List<ResourceAllocationVO> rsCall, DateTime slotDate, string portCode)
        {
            List<MovementResourceAllocationVO> lstResourceAllocationSlotVOs = new List<MovementResourceAllocationVO>();

            int tempServiceRequestId = 0;
            if (rsCall != null)
            {
                foreach (var rsc in rsCall)
                {
                    MovementResourceAllocationVO obj = null;
                    bool isNewRecord = false;

                    var slotconfig = lstSlotConfig.Find(sl => sl.SlotPeriod == rsc.AllocSlot && sl.AllocationDate.Date == rsc.AllocationDate.Date);
                    if (slotconfig != null)
                    {
                        if (rsc.ServiceReferenceID != tempServiceRequestId)
                        {
                            obj = new MovementResourceAllocationVO();
                            obj.ResourceAllocationSlots = new List<ResourceAllocationSlotVO>();
                            obj.VCN = rsc.VCN;
                            obj.VesselName = rsc.VesselName;
                            obj.ServiceRequestId = rsc.ServiceReferenceID;
                            tempServiceRequestId = rsc.ServiceReferenceID;
                            obj.MovementType = rsc.MovementType;
                            obj.AnyDangerousGoodsonBoard = rsc.AnyDangerousGoodsonBoard;
                            isNewRecord = true;
                            obj.VesselType = rsc.VesselType;
                            obj.ETA = rsc.ETA;
                            obj.ReasonForVisit = rsc.ReasonForVisit;
                            obj.CargoType = rsc.CargoType;
                            obj.LOA = rsc.LOA;
                            obj.GRT = rsc.GRT;
                            obj.DWT = rsc.DWT;
                            obj.CurrentBerth = rsc.CurrentBerth;
                            obj.ToBerth = rsc.ToBerth;
                            obj.MovementDateTime = rsc.MovementDateTime;
                            obj.Beam = rsc.Beam;
                            obj.ArrivalDraft = rsc.ArrivalDraft;
                            obj.TidalCondition = rsc.TidalCondition;
                            obj.DayLightCondition = rsc.DayLightCondition;
                            obj.FromBollard = rsc.FromBollard;
                            obj.ToBollard = rsc.ToBollard;
                            obj.MovementTypeCode = rsc.MovementTypeCode;
                            obj.MovementStatus = rsc.MovementStatus;
                            obj.SideAlongSide = rsc.SideAlongSide;
                        }
                        else
                        {
                            obj = lstResourceAllocationSlotVOs.Last();
                        }

                        ResourceAllocationSlotVO slotVO = new ResourceAllocationSlotVO();
                        slotVO.ServiceTypeCode = rsc.ServiceTypeCode;
                        slotVO.ServiceTypeName = rsc.ServiceTypeName;
                        slotVO.IsCraft = rsc.IsCraft;
                        ResourceSlotVO slot = new ResourceSlotVO();

                        slot.SlotNumber = slotconfig.SlotNumber;
                        slot.ResourceID = rsc.ResourceID ?? 0;

                        string strResourceName = rsc.ResourceID > 0 ? rsc.Name : "Unscheduled";
                        
                        if (strResourceName.Length > 17)
                        {
                            slot.ResourceName = strResourceName.Substring(0, 15) + "...";
                        }
                        else
                        {
                            slot.ResourceName = strResourceName;
                        }
                        
                        slot.Status = rsc.TaskStatus;
                        slot.IsCraft = rsc.IsCraft;
                        slot.ServiceReferenceID = rsc.ServiceReferenceID;
                        slot.ResourceAllocationID = rsc.ResourceAllocationID;
                        slot.AllocationDate = rsc.AllocationDate;

                        string strTugResourceName = (rsc.ResourceID > 0 && rsc.CraftId > 0) ? rsc.CraftName + "/" + rsc.TugResourceName : "Unscheduled";
                        
                        if (strTugResourceName.Length > 17)
                        {
                            slot.TugResourceName = strTugResourceName.Substring(0, 15) + "...";
                        }
                        else
                        {
                            slot.TugResourceName = strTugResourceName;
                        }

                        slot.ServiceTypeCode = rsc.ServiceTypeCode;
                        slot.ServiceTypeName = rsc.ServiceTypeName;
                        slot.CraftID = rsc.CraftId ?? 0;
                        slot.CraftName = rsc.CraftId > 0 ? rsc.CraftName : null;
                        slot.TaskStatus = rsc.ResourceID > 0 ? rsc.TaskStatus : "PNDG";

                        int slotno = slot.SlotNumber - 1;
                        slotVO.ResourceAllocationID = rsc.ResourceAllocationID;
                        slotVO.RecordStatus = rsc.RecordStatus;
                        slotVO.ResourceSlots = _suppServiceResourceAllocRepository.GetSlotConfiguration(slotDate, portCode);
                        var blockedSlots = GetAutomatedBlockedSlots(slotVO.ResourceSlots, portCode);
                        slotVO.ResourceSlots[slotno] = slot;
                        slotVO.CreatedBy = rsc.CreatedBy;
                        slotVO.CreatedDate = rsc.CreatedDate.ToShortDateString();
                        slotVO.ModifiedBy = rsc.ModifiedBy;
                        slotVO.ModifiedDate = rsc.ModifiedDate.ToShortDateString();
                        slotVO.AllocSlot = rsc.AllocSlot;
                        slotVO.StartTime = Convert.ToString(rsc.StartTime, CultureInfo.InvariantCulture);
                        slotVO.EndTime = Convert.ToString(rsc.EndTime, CultureInfo.InvariantCulture);
                        slotVO.ServiceReferenceID = rsc.ServiceReferenceID;

                        obj.ResourceAllocationSlots.Add(slotVO);

                        if (isNewRecord == true)
                        {
                            lstResourceAllocationSlotVOs.Add(obj);
                        }
                    }
                }
            }

            return lstResourceAllocationSlotVOs;
        }

        public List<IdNameVO> GetSearchCraft(ResourceSlotVO resourceSlot, string portCode)
        {
            var ResSlots = _suppServiceResourceAllocRepository.GetSlotConfiguration(resourceSlot.AllocationDate, portCode);
            var crafts = GetSearchCraft_Crafts(resourceSlot, portCode);

            var ocCrafts = GetSearchCraft_OCCrafts(resourceSlot, portCode);

            var craftorder = ocCrafts.GroupBy(i => i.ID).Select(l => l.Last()).ToList();

            foreach (IdNameVO craft in craftorder)
            {
                if (craft.OCBackTime != null)
                {
                    var ocBackslotPeriod = GetSlotPeriodBySlotdate(Convert.ToDateTime(craft.OCBackTime, CultureInfo.InvariantCulture), portCode);
                    var ocBackslotnumber = ResSlots.Find(s => s.SlotPeriod == ocBackslotPeriod).SlotNumber;

                    if (Convert.ToDateTime(craft.OCBackTime, CultureInfo.InvariantCulture).Date == resourceSlot.AllocationDate.Date)
                    {
                        if (resourceSlot != null)
                        {
                            if (ocBackslotnumber >= resourceSlot.SlotNumber)
                            {
                                var _craft = crafts.Find(c => c.ID == craft.ID);
                                crafts.Remove(_craft);
                            }
                        }
                    }
                }
                else
                {
                    var _craft = crafts.Find(c => c.ID == craft.ID);
                    crafts.Remove(_craft);
                }
            }

            crafts = crafts.GroupBy(i => i.ID).Select(f => f.First()).ToList();

            return crafts;
        }

        private List<IdNameVO> GetSearchCraft_OCCrafts(ResourceSlotVO resourceSlot, string portCode)
        {
            var ocCrafts = (from oc in _unitOfWork.Repository<CraftOutOfCommission>().Queryable()
                            join c in _unitOfWork.Repository<Craft>().Queryable() on oc.CraftID equals c.CraftID
                            join std in _unitOfWork.Repository<ServiceTypeDesignation>().Queryable() on c.CraftType equals std.CraftType
                            join st in _unitOfWork.Repository<ServiceType>().Queryable() on std.ServiceTypeID equals st.ServiceTypeID
                            where std.PortCode == portCode && st.ServiceTypeCode == resourceSlot.ServiceTypeCode
                            && std.CraftType == c.CraftType
                            && c.PortCode == portCode

                            select new IdNameVO
                            {
                                ID = c.CraftID,
                                Name = c.CraftName,
                                OCTime = oc.OutOfCommissionDate,
                                OCBackTime = oc.BackToCommissionDate

                            }).ToList();
            return ocCrafts;
        }

        private List<IdNameVO> GetSearchCraft_Crafts(ResourceSlotVO resourceSlot, string portCode)
        {
            var crafts = (from c in _unitOfWork.Repository<Craft>().Queryable()
                          join std in _unitOfWork.Repository<ServiceTypeDesignation>().Queryable() on c.CraftType equals std.CraftType
                          join st in _unitOfWork.Repository<ServiceType>().Queryable() on std.ServiceTypeID equals st.ServiceTypeID
                          where std.PortCode == portCode && st.ServiceTypeCode == resourceSlot.ServiceTypeCode
                          && std.CraftType == c.CraftType
                          && c.PortCode == portCode

                          select new IdNameVO
                          {
                              ID = c.CraftID,
                              Name = c.CraftName

                          }).ToList();
            return crafts;
        }

        public List<ResourceCalendarAttendanceVO> GetCraftAvail(string portCode, string commissionStatus, string serviceType)
        {
            var crafts = (from c in _unitOfWork.Repository<Craft>().Query().Select()
                          join std in _unitOfWork.Repository<ServiceTypeDesignation>().Query().Select() on c.CraftType equals std.CraftType
                          join st in _unitOfWork.Repository<ServiceType>().Query().Select() on std.ServiceTypeID equals st.ServiceTypeID
                          where std.PortCode == portCode && st.ServiceTypeCode == serviceType
                          && c.RecordStatus == RecordStatus.Active
                          && c.CraftCommissionStatus == commissionStatus
                          && c.PortCode == portCode

                          select new ResourceCalendarAttendanceVO
                          {
                              CraftID = c.CraftID,
                              CraftName = c.CraftName,
                              CraftCommissionStatus = c.CraftCommissionStatus

                          }).ToList();

            return crafts;
        }

        public List<ResourceCalendarAttendanceVO> GetAvailableCrafts(string portCode, string serviceType, DateTime startTime, DateTime endTime)
        {
            var crafts = GetAvailableCrafts_Crafts(portCode, serviceType);

            var ocCrafts = GetAvailableCrafts_OCCrafts(portCode, serviceType);

            var craftorder = ocCrafts.GroupBy(i => i.CraftID).Select(l => l.Last()).ToList();

            foreach (ResourceCalendarAttendanceVO craft in craftorder)
            {
                if (craft.BackToCommisionDate != null)
                {
                    if (craft.OutOfCommissionDate <= startTime && craft.BackToCommisionDate >= endTime)
                    {

                    }
                    else
                    {
                        var _craft = crafts.Find(c => c.CraftID == craft.CraftID);
                        crafts.Remove(_craft);
                    }
                }
                else
                {
                    if (craft.OutOfCommissionDate <= startTime || craft.OutOfCommissionDate >= endTime)
                    {
                    }
                    else
                    {
                        var _craft = crafts.Find(c => c.CraftID == craft.CraftID);
                        crafts.Remove(_craft);
                    }
                }
            }

            crafts = crafts.Where(a => !ocCrafts.Any(a1 => a1.CraftID == a.CraftID)).ToList();
            return crafts;
        }

        private List<ResourceCalendarAttendanceVO> GetAvailableCrafts_OCCrafts(string portCode, string serviceType)
        {
            var ocCrafts = (from oc in _unitOfWork.Repository<CraftOutOfCommission>().Query().Select()
                            join c in _unitOfWork.Repository<Craft>().Query().Select() on oc.CraftID equals c.CraftID
                            join std in _unitOfWork.Repository<ServiceTypeDesignation>().Query().Select() on c.CraftType equals std.CraftType
                            join st in _unitOfWork.Repository<ServiceType>().Query().Select() on std.ServiceTypeID equals st.ServiceTypeID
                            where std.PortCode == portCode && st.ServiceTypeCode == serviceType
                            && std.CraftType == c.CraftType
                            && c.PortCode == portCode
                            select new ResourceCalendarAttendanceVO
                            {
                                CraftID = c.CraftID,
                                CraftName = c.CraftName,
                                CraftCommissionStatus = "IC",
                                OutOfCommissionDate = oc.OutOfCommissionDate,
                                BackToCommisionDate = oc.BackToCommissionDate

                            }).ToList();
            return ocCrafts;
        }

        private List<ResourceCalendarAttendanceVO> GetAvailableCrafts_Crafts(string portCode, string serviceType)
        {
            var crafts = (from c in _unitOfWork.Repository<Craft>().Query().Select()
                          join std in _unitOfWork.Repository<ServiceTypeDesignation>().Query().Select() on c.CraftType equals std.CraftType
                          join st in _unitOfWork.Repository<ServiceType>().Query().Select() on std.ServiceTypeID equals st.ServiceTypeID
                          where std.PortCode == portCode && st.ServiceTypeCode == serviceType
                          && std.CraftType == c.CraftType
                          && c.PortCode == portCode

                          select new ResourceCalendarAttendanceVO
                          {
                              CraftID = c.CraftID,
                              CraftName = c.CraftName,
                              CraftCommissionStatus = "IC"

                          }).ToList();
            return crafts;
        }

        public List<ResourceCalendarAttendanceVO> GetCommisionCrafts(string portCode, string serviceType, DateTime startTime, DateTime endTime, List<ResourceSlotVO> resourceSlot)
        {
            List<ResourceCalendarAttendanceVO> lstoutOfCommisionCrafts = new List<ResourceCalendarAttendanceVO>();

            var craftorder = GetCommisionCrafts_CraftOrder(portCode, serviceType);

            foreach (ResourceCalendarAttendanceVO craft in craftorder)
            {
                ResourceCalendarAttendanceVO _resourceCalendarAttendanceVO = new ResourceCalendarAttendanceVO();
                List<ResourceSlotVO> _resourceSlotVo = new List<ResourceSlotVO>();
                _resourceCalendarAttendanceVO = craft;

                ResourceSlotVO _slot = null;
                if (resourceSlot != null)
                {
                    foreach (var slot in resourceSlot)
                    {
                        _slot = new ResourceSlotVO();
                        _slot.SlotNumber = slot.SlotNumber;
                        _slot.SlotPeriod = slot.SlotPeriod;

                        string[] slotperiod = slot.SlotPeriod.Split('-');
                       startTime = startTime.Date.AddHours(Convert.ToDateTime(slotperiod[0], CultureInfo.InvariantCulture).Hour);
                       endTime = endTime.Date.AddHours(Convert.ToDateTime(slotperiod[1], CultureInfo.InvariantCulture).Hour);
                    
                        if (craft.BackToCommisionDate != null)
                        {
                            if (Convert.ToDateTime(craft.BackToCommisionDate, CultureInfo.InvariantCulture).Date < startTime.Date)
                            {
                                _slot.CraftStatus = "IC";
                            }

                            else if (Convert.ToDateTime(craft.OutOfCommissionDate, CultureInfo.InvariantCulture).Date <= startTime.Date)// && endtime <= Convert.ToDateTime(craft.BackToCommisionDate))
                            {
                                if (((startTime >= Convert.ToDateTime(craft.OutOfCommissionDate, CultureInfo.InvariantCulture) || endTime >= Convert.ToDateTime(craft.OutOfCommissionDate, CultureInfo.InvariantCulture)) && (startTime <= Convert.ToDateTime(craft.BackToCommisionDate, CultureInfo.InvariantCulture) || endTime <= Convert.ToDateTime(craft.BackToCommisionDate, CultureInfo.InvariantCulture))))
                                {
                                    _slot.CraftStatus = "OC";
                                }
                                else
                                {
                                    _slot.CraftStatus = "IC";
                                }
                            }
                            else
                            {
                                _slot.CraftStatus = "IC";
                            }
                        }
                        else
                        {
                            if (Convert.ToDateTime(craft.OutOfCommissionDate, CultureInfo.InvariantCulture).Date < startTime.Date)
                            {
                                _slot.CraftStatus = "OC";
                            }
                            else if (Convert.ToDateTime(craft.OutOfCommissionDate, CultureInfo.InvariantCulture) <= startTime || Convert.ToDateTime(craft.OutOfCommissionDate, CultureInfo.InvariantCulture) <= endTime)
                            {
                                _slot.CraftStatus = "OC";
                            }
                            else
                            {
                                _slot.CraftStatus = "IC";
                            }
                        }
                        _resourceSlotVo.Add(_slot);
                        _slot = null;
                    }
                }

                _resourceCalendarAttendanceVO.ResourceSlotVO = _resourceSlotVo;
                lstoutOfCommisionCrafts.Add(_resourceCalendarAttendanceVO);
                _resourceSlotVo = null;
                _resourceCalendarAttendanceVO = null;
            }

            return lstoutOfCommisionCrafts;
        }

        private List<ResourceCalendarAttendanceVO> GetCommisionCrafts_CraftOrder(string portCode, string serviceType)
        {
            var ocCrafts = (from oc in _unitOfWork.Repository<CraftOutOfCommission>().Query().Select()
                            join c in _unitOfWork.Repository<Craft>().Query().Select() on oc.CraftID equals c.CraftID
                            join std in _unitOfWork.Repository<ServiceTypeDesignation>().Query().Select() on c.CraftType equals std.CraftType
                            join st in _unitOfWork.Repository<ServiceType>().Query().Select() on std.ServiceTypeID equals st.ServiceTypeID
                            where std.PortCode == portCode && st.ServiceTypeCode == serviceType
                            && std.CraftType == c.CraftType
                            && c.PortCode == portCode
                            select new ResourceCalendarAttendanceVO
                            {
                                CraftID = c.CraftID,
                                CraftName = c.CraftName,
                                CraftCommissionStatus = c.CraftCommissionStatus,
                                OutOfCommissionDate = oc.OutOfCommissionDate,
                                BackToCommisionDate = oc.BackToCommissionDate

                            }).ToList();

            var craftorder = ocCrafts.GroupBy(i => i.CraftID).Select(l => l.Last()).ToList();
            return craftorder;
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   :1st Oct  2014
        /// Purpose: To get all servicetype details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> GetServiceTypes()
        {
            var servicetypes = (from s in _unitOfWork.Repository<ServiceType>().Queryable()
                                where s.IsServiceType == "N" && s.RecordStatus == "A"
                                select s).ToList();

            return servicetypes.MapToDto();
        }

        public VesselVO GetServiceRequestDetailsById(int serviceRequestId)
        {
            var vessel = (from r in _unitOfWork.Repository<ServiceRequest>().Queryable()
                          join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on r.VCN equals an.VCN
                          join v in _unitOfWork.Repository<Vessel>().Queryable() on an.VesselID equals v.VesselID

                          where r.ServiceRequestID == serviceRequestId
                          select new VesselVO
                          {
                              LengthOverallInM = v.LengthOverallInM,
                              DeadWeightTonnageInMT = v.DeadWeightTonnageInMT,
                              GrossRegisteredTonnageInMT = v.GrossRegisteredTonnageInMT,
                              PilotExemptionID = an.ExemptionPilotID ?? 0,
                              PilotExemption = an.PilotExemption
                          }).First();

            return vessel;
        }

        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   :28th Nov 2014
        /// Purpose: To get all Resource Calendar details
        /// </summary>
        /// <returns></returns>
        public List<ResourceCalendarVO> GetResourceCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearchVO,
            string strPortCode)
        {
            string strServiceReferenceType = string.Empty;
            if (objResourceCalendarSearchVO != null)
            {
                if (objResourceCalendarSearchVO.ServiceReferenceType == "SUPP")
                {
                    strServiceReferenceType = "SUPP";
                }
                else
                {
                    strServiceReferenceType = "VTSR";
                }
            }
            var parPortCode = new SqlParameter("@PortCode", strPortCode);
            var parServiceReferenceType = new SqlParameter("@ServiceReferenceType", strServiceReferenceType);
            var allocationDate = DateTime.MinValue;

            if (objResourceCalendarSearchVO != null)
            {
                allocationDate = Convert.ToDateTime(objResourceCalendarSearchVO.AllocationDate);
            }

            var shiftId = 0;
            var operationtype = string.Empty;

            if (objResourceCalendarSearchVO != null)
            {
                shiftId = objResourceCalendarSearchVO.ShiftID;
                operationtype = objResourceCalendarSearchVO.OperationType;
            }

            var parAllocationDate = new SqlParameter("@AttendanceDate", allocationDate);
            var parShiftID = new SqlParameter("@ShiftID", shiftId);
            var parServiceTypeCode = new SqlParameter("@ServiceTypeCode", operationtype);

            var rsAttendance =
                _unitOfWork.SqlQuery<ResourceCalendarAttendanceVO>(
                    "usp_GetUsersAttendanceForResouceCalendar @PortCode,@ServiceReferenceType,@AttendanceDate,@ShiftID,@ServiceTypeCode",
                    parPortCode, parServiceReferenceType, parAllocationDate, parShiftID, parServiceTypeCode).ToList();

            var parAllocationDateSlot = new SqlParameter("@AllocationDate",
                Convert.ToDateTime(objResourceCalendarSearchVO.AllocationDate));
            parPortCode = new SqlParameter("@PortCode", strPortCode);
            parServiceReferenceType = new SqlParameter("@ServiceReferenceType", strServiceReferenceType);
            parServiceTypeCode = new SqlParameter("@ServiceTypeCode", objResourceCalendarSearchVO.OperationType);

            var rsResourceCalendarSlot =
                _unitOfWork.SqlQuery<ResourceCalendarSlotVO>(
                    "usp_GetSlotDetailsForResourceCalendar @ServiceReferenceType,@AllocationDate,@ServiceTypeCode,@PortCode",
                    parServiceReferenceType, parAllocationDateSlot, parServiceTypeCode, parPortCode).ToList();

            List<ResourceCalendarVO> lstResourceCalendarVO = ConstructResourceCalendarSlotDetails(rsAttendance, rsResourceCalendarSlot, objResourceCalendarSearchVO.AllocationDate, strPortCode, objResourceCalendarSearchVO.ShiftID);

            return lstResourceCalendarVO;
        }

        public List<ResourceCalendarVO> ConstructResourceCalendarSlotDetails(List<ResourceCalendarAttendanceVO> objResourceCalendarAttendanceVO,
            List<ResourceCalendarSlotVO> objResourceCalendarSlotVO, DateTime slotDate, string portCode, int shiftId)
        {
            List<ResourceCalendarVO> BindinglstResourceCalendarVO = new List<ResourceCalendarVO>();

            List<ResourceSlotVO> lstSlotConfigForResourceCalendar = new List<ResourceSlotVO>();

            List<ResourceCalendarAttendanceVO> lstResourceAvailable = new List<ResourceCalendarAttendanceVO>();

            if (objResourceCalendarAttendanceVO != null)
            {
                lstResourceAvailable = objResourceCalendarAttendanceVO.FindAll(r => r.AttendanceStatus == "Y");
            }

            var GetResourceAvailableWithSlots = (from UserIDs in lstResourceAvailable
                                                 where objResourceCalendarSlotVO.Exists(s => s.ResourceID == UserIDs.UserID)
                                                 select new { UserIDs });

            var GetResourceAvailableWithOutSlots = (from UserIDs in lstResourceAvailable
                                                    where !(objResourceCalendarSlotVO.Exists(s => s.ResourceID == UserIDs.UserID))
                                                    select new { UserIDs });


            lstSlotConfigForResourceCalendar = _suppServiceResourceAllocRepository.GetSlotConfiguration(slotDate, portCode);


            var varResourceAvailableWithSlots =
                            from ra in GetResourceAvailableWithSlots
                            join slt in objResourceCalendarSlotVO on ra.UserIDs.UserID equals slt.ResourceID
                            select new { vVCN = slt.VCN, vResourceID = slt.ResourceID, vUserFullName = ra.UserIDs.UserFullName, vSlotPeriod = slt.AllocSlot, vShiftID = ra.UserIDs.ShiftID, vMovementType = slt.MovementType };


            List<ResourceCalendarAttendanceVO> lstResourceNotAvailable = new List<ResourceCalendarAttendanceVO>();
            if (objResourceCalendarAttendanceVO != null)
            {
                lstResourceNotAvailable = objResourceCalendarAttendanceVO.FindAll(r => r.AttendanceStatus != "Y");
            }

            var slotconfig = lstSlotConfigForResourceCalendar.FindAll(sl => sl.ShiftID == shiftId);

            foreach (var rsl in varResourceAvailableWithSlots)
            {
                int irousr = rsl.vResourceID;
                var valuesResourceCalendarVOget = BindinglstResourceCalendarVO.Find(s => s.UserID == irousr);
                if (valuesResourceCalendarVOget == null)
                {
                    ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                    valuesResourceCalendarVO.UserFullName = rsl.vUserFullName;
                    valuesResourceCalendarVO.UserID = rsl.vResourceID;
                    for (int i = 0; i < slotconfig.Count; i++)
                    {
                        ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();
                        if (rsl.vSlotPeriod == slotconfig[i].SlotPeriod)
                        {
                            valResourceCalendarSlotDetailsVO.VCN = rsl.vVCN;
                            valResourceCalendarSlotDetailsVO.SlotText = rsl.vVCN + "-" + rsl.vMovementType;
                            valResourceCalendarSlotDetailsVO.MovementType = rsl.vMovementType;
                            valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                        }
                        else
                        {
                            valResourceCalendarSlotDetailsVO.VCN = null;
                            valResourceCalendarSlotDetailsVO.SlotText = null;
                            valResourceCalendarSlotDetailsVO.SlotPeriod = "";
                        }

                        valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                        valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                    }
                    BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
                }
                else
                {
                    BindinglstResourceCalendarVO.Remove(valuesResourceCalendarVOget);

                    ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                    valuesResourceCalendarVO.UserFullName = rsl.vUserFullName;
                    valuesResourceCalendarVO.UserID = rsl.vResourceID;

                    for (int i = 0; i < slotconfig.Count; i++)
                    {
                        ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();
                        if (valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].SlotPeriod == slotconfig[i].SlotPeriod)
                        {
                            valResourceCalendarSlotDetailsVO.VCN = valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].VCN;// rsl.vVCN;
                            valResourceCalendarSlotDetailsVO.SlotText = valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].VCN + "-" + valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].MovementType; //rsl.vVCN + "-" + rsl.vMovementType;
                            var getVCN = (from vcndata in varResourceAvailableWithSlots where vcndata.vSlotPeriod == slotconfig[i].SlotPeriod && vcndata.vResourceID == valuesResourceCalendarVOget.UserID select new { Slotdatails = vcndata.vVCN + "-" + vcndata.vMovementType });
                            if (getVCN != null)
                            {
                                var combined = string.Join(", ", getVCN).Replace("{ Slotdatails =", "").Replace("}", "");
                                valResourceCalendarSlotDetailsVO.SlotText = combined;
                            }
                            else
                            {
                                valResourceCalendarSlotDetailsVO.SlotText = valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].MovementType;
                            }
                            valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                            valResourceCalendarSlotDetailsVO.MovementType = valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].MovementType;
                        }
                        else if (rsl.vSlotPeriod == slotconfig[i].SlotPeriod)
                        {
                            valResourceCalendarSlotDetailsVO.VCN = rsl.vVCN;
                            valResourceCalendarSlotDetailsVO.SlotText = rsl.vVCN + "-" + rsl.vMovementType;
                            valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                            valResourceCalendarSlotDetailsVO.MovementType = rsl.vMovementType;
                        }
                        else
                        {
                            valResourceCalendarSlotDetailsVO.VCN = "";
                            valResourceCalendarSlotDetailsVO.SlotText = null;
                            valResourceCalendarSlotDetailsVO.SlotPeriod = "";
                        }

                        valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                        valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                    }

                    BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
                }

            }

            foreach (var rsl in GetResourceAvailableWithOutSlots)
            {
                ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                valuesResourceCalendarVO.UserFullName = rsl.UserIDs.UserFullName;
                valuesResourceCalendarVO.UserID = rsl.UserIDs.UserID;
                for (int i = 0; i < slotconfig.Count; i++)
                {
                    ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();
                    valResourceCalendarSlotDetailsVO.VCN = "UnPlanned";
                    valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                    valResourceCalendarSlotDetailsVO.SlotText = "UnPlanned";
                    valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                }
                BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
            }

            foreach (var rsl in lstResourceNotAvailable)
            {
                ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                valuesResourceCalendarVO.UserFullName = rsl.UserFullName;
                valuesResourceCalendarVO.UserID = rsl.UserID;
                for (int i = 0; i < slotconfig.Count; i++)
                {
                    ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();
                    valResourceCalendarSlotDetailsVO.VCN = "Absent";
                    valResourceCalendarSlotDetailsVO.SlotText = "Absent";
                    valResourceCalendarSlotDetailsVO.AttendanceStatus = "N";
                    valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                }
                BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
            }

            return BindinglstResourceCalendarVO;
        }


        /// <summary>
        /// Author : Omprakash Kotha
        /// Date   :28th Nov 2014
        /// Purpose: To get all Resource Calendar details
        /// </summary>
        /// <returns></returns>
        public List<ResourceCalendarVO> GetCraftCalendarDetails(ResourceCalendarSearchVO objResourceCalendarSearchVO, string strPortCode)
        {
            string strServiceReferenceType = string.Empty;
            if (objResourceCalendarSearchVO != null)
            {
                if (objResourceCalendarSearchVO.ServiceReferenceType == "SUPP")
                {
                    strServiceReferenceType = "SUPP";
                }
                else
                {
                    strServiceReferenceType = "VTSR";
                }
            }

            var parPortCode = new SqlParameter("@PortCode", strPortCode);
            var parServiceReferenceType = new SqlParameter("@ServiceReferenceType", strServiceReferenceType);

            var allocationDate = DateTime.MinValue;

            if (objResourceCalendarSearchVO != null)
            {
                allocationDate = Convert.ToDateTime(objResourceCalendarSearchVO.AllocationDate);
            }

            var shiftId = 0;
            var operationtype = string.Empty;

            if (objResourceCalendarSearchVO != null)
            {
                shiftId = objResourceCalendarSearchVO.ShiftID;
                operationtype = objResourceCalendarSearchVO.OperationType;
            }

            var parAllocationDate = new SqlParameter("@AttendanceDate", allocationDate);
            var parShiftID = new SqlParameter("@ShiftID", shiftId);
            var parServiceTypeCode = new SqlParameter("@ServiceTypeCode", operationtype);

            var rsAttendance =
                _unitOfWork.SqlQuery<ResourceCalendarAttendanceVO>(
                    "usp_GetUsersAttendanceForResouceCalendar @PortCode,@ServiceReferenceType,@AttendanceDate,@ShiftID,@ServiceTypeCode",
                    parPortCode, parServiceReferenceType, parAllocationDate, parShiftID, parServiceTypeCode)
                    .ToList();

            var parAllocationDateSlot = new SqlParameter("@AllocationDate",
                Convert.ToDateTime(objResourceCalendarSearchVO.AllocationDate));
            parPortCode = new SqlParameter("@PortCode", strPortCode);
            parServiceReferenceType = new SqlParameter("@ServiceReferenceType", strServiceReferenceType);
            parServiceTypeCode = new SqlParameter("@ServiceTypeCode", objResourceCalendarSearchVO.OperationType);

            var rsCraftCalendarSlot =
                _unitOfWork.SqlQuery<ResourceCalendarSlotVO>(
                    "[usp_GetSlotDetailsForCraftCalendar] @ServiceReferenceType,@AllocationDate,@ServiceTypeCode,@PortCode",
                    parServiceReferenceType, parAllocationDateSlot, parServiceTypeCode, parPortCode).ToList();
            List<ResourceCalendarVO> lstCraftCalendarVO = ConstructCraftCalendarSlotDetails(rsAttendance, rsCraftCalendarSlot, objResourceCalendarSearchVO.AllocationDate, strPortCode, objResourceCalendarSearchVO.ShiftID, objResourceCalendarSearchVO.OperationType);

            return lstCraftCalendarVO;
        }

        public List<ResourceCalendarVO> ConstructCraftCalendarSlotDetails(List<ResourceCalendarAttendanceVO> objResourceCalendarAttendanceVO,
            List<ResourceCalendarSlotVO> objCraftCalendarSlotVO, DateTime slotDate, string portCode, int shiftId, string serviceType)
        {
            var shift = (from s in _unitOfWork.Repository<Shift>().Query().Tracking(true).Select()
                         where s.ShiftID == shiftId
                         select s).First();

            DateTime startTime = slotDate.Date.AddHours(Convert.ToDateTime(shift.StartTime, CultureInfo.InvariantCulture).Hour);
            DateTime endTime = slotDate.Date.AddHours(Convert.ToDateTime(shift.EndTime, CultureInfo.InvariantCulture).Hour);

            List<ResourceCalendarVO> BindinglstResourceCalendarVO = new List<ResourceCalendarVO>();

            List<ResourceSlotVO> lstSlotConfigForResourceCalendar = new List<ResourceSlotVO>();

            List<ResourceCalendarAttendanceVO> lstResourceAvailable = new List<ResourceCalendarAttendanceVO>();

            List<ResourceCalendarAttendanceVO> lstCraftAvailable = new List<ResourceCalendarAttendanceVO>();
            List<ResourceCalendarAttendanceVO> lstCraftNotAvailable = new List<ResourceCalendarAttendanceVO>();
            lstSlotConfigForResourceCalendar = _suppServiceResourceAllocRepository.GetSlotConfiguration(slotDate, portCode);
            var slotconfig = lstSlotConfigForResourceCalendar.FindAll(sl => sl.ShiftID == shiftId);

            lstCraftAvailable = GetAvailableCrafts(portCode, serviceType, startTime.Date, endTime);

            if (objResourceCalendarAttendanceVO != null)
            {
                lstResourceAvailable = objResourceCalendarAttendanceVO.FindAll(r => r.AttendanceStatus == "Y");
            }

            lstCraftNotAvailable = GetCommisionCrafts(portCode, serviceType, startTime, endTime, slotconfig);


            var GetResourceAvailableWithSlots = (from UserIDs in lstResourceAvailable
                                                 where objCraftCalendarSlotVO.Exists(s => s.ResourceID == UserIDs.UserID)
                                                 select new { UserIDs });

            var varResourceAvailableWithSlots =
                           from ra in GetResourceAvailableWithSlots
                           join slt in objCraftCalendarSlotVO on ra.UserIDs.UserID equals slt.ResourceID
                           select new { vVCN = slt.VCN, vResourceID = slt.CraftID, vUserFullName = slt.CraftName, vSlotPeriod = slt.AllocSlot, vShiftID = ra.UserIDs.ShiftID, vMovementType = slt.MovementType };

            varResourceAvailableWithSlots = varResourceAvailableWithSlots.Distinct().ToList();
            var GetResourceAvailableWithCraftSlots = varResourceAvailableWithSlots.Where(a => lstCraftNotAvailable.Any(s => s.CraftID == a.vResourceID)).ToList();
            varResourceAvailableWithSlots = varResourceAvailableWithSlots.Where(a => !lstCraftNotAvailable.Any(s => s.CraftID == a.vResourceID)).ToList();

            var GetResourceAvailableWithOutSlots = (from UserIDs in lstCraftAvailable
                                                    where !(objCraftCalendarSlotVO.Exists(s => s.CraftID == UserIDs.CraftID))
                                                    select new { UserIDs });

            var GetOutOfCommisionCraftsWithOutSlots = lstCraftNotAvailable;
            var varGetResourceAvailableWithCraftSlots = lstCraftNotAvailable.Where(a => GetResourceAvailableWithCraftSlots.Any(b => b.vResourceID == a.CraftID));
            var varGetResourceAvailableWithOutCraftSlots = GetOutOfCommisionCraftsWithOutSlots.Except(varGetResourceAvailableWithCraftSlots);

            foreach (var rsl in varResourceAvailableWithSlots)
            {
                int irousr = rsl.vResourceID;
                var valuesResourceCalendarVOget = BindinglstResourceCalendarVO.Find(s => s.UserID == irousr);
                if (valuesResourceCalendarVOget == null)
                {
                    ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                    valuesResourceCalendarVO.UserFullName = rsl.vUserFullName;
                    valuesResourceCalendarVO.UserID = rsl.vResourceID;
                    for (int i = 0; i < slotconfig.Count; i++)
                    {
                        ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();
                        if (rsl.vSlotPeriod == slotconfig[i].SlotPeriod)
                        {
                            valResourceCalendarSlotDetailsVO.VCN = rsl.vVCN;
                            valResourceCalendarSlotDetailsVO.SlotText = rsl.vVCN + "-" + rsl.vMovementType;
                            valResourceCalendarSlotDetailsVO.MovementType = rsl.vMovementType;
                            valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                        }
                        else
                        {
                            valResourceCalendarSlotDetailsVO.VCN = null;
                            valResourceCalendarSlotDetailsVO.SlotText = null;
                            valResourceCalendarSlotDetailsVO.SlotPeriod = "";
                        }

                        valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                        valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                    }
                    BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
                }
                else
                {
                    BindinglstResourceCalendarVO.Remove(valuesResourceCalendarVOget);

                    ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                    valuesResourceCalendarVO.UserFullName = rsl.vUserFullName;
                    valuesResourceCalendarVO.UserID = rsl.vResourceID;

                    for (int i = 0; i < slotconfig.Count; i++)
                    {
                        ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();
                        if (valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].SlotPeriod == slotconfig[i].SlotPeriod)
                        {
                            valResourceCalendarSlotDetailsVO.VCN = valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].VCN;// rsl.vVCN;
                            valResourceCalendarSlotDetailsVO.SlotText = valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].VCN + "-" + valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].MovementType; //rsl.vVCN + "-" + rsl.vMovementType;
                            var getVCN = (from vcndata in varResourceAvailableWithSlots where vcndata.vSlotPeriod == slotconfig[i].SlotPeriod && vcndata.vResourceID == valuesResourceCalendarVOget.UserID select new { Slotdatails = vcndata.vVCN + "-" + vcndata.vMovementType });
                            if (getVCN != null)
                            {
                                var combined = string.Join(", ", getVCN).Replace("{ Slotdatails =", "").Replace("}", "");
                                valResourceCalendarSlotDetailsVO.SlotText = combined;
                            }
                            else
                            {
                                valResourceCalendarSlotDetailsVO.SlotText = valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].MovementType;
                            }
                            valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                            valResourceCalendarSlotDetailsVO.MovementType = valuesResourceCalendarVOget.ResourceCalendarSlotDetails[i].MovementType;
                        }
                        else if (rsl.vSlotPeriod == slotconfig[i].SlotPeriod)
                        {
                            valResourceCalendarSlotDetailsVO.VCN = rsl.vVCN;
                            valResourceCalendarSlotDetailsVO.SlotText = rsl.vVCN + "-" + rsl.vMovementType;
                            valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                            valResourceCalendarSlotDetailsVO.MovementType = rsl.vMovementType;
                        }
                        else
                        {
                            valResourceCalendarSlotDetailsVO.VCN = "";
                            valResourceCalendarSlotDetailsVO.SlotText = null;
                            valResourceCalendarSlotDetailsVO.SlotPeriod = "";
                        }

                        valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                        valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                    }

                    BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
                }
            }

            foreach (var rsl in GetResourceAvailableWithOutSlots)
            {
                ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                valuesResourceCalendarVO.UserFullName = rsl.UserIDs.CraftName;
                valuesResourceCalendarVO.UserID = rsl.UserIDs.CraftID;
                for (int i = 0; i < slotconfig.Count; i++)
                {
                    ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();
                    valResourceCalendarSlotDetailsVO.VCN = "UnPlanned";
                    valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                    valResourceCalendarSlotDetailsVO.SlotText = "UnPlanned";
                    valResourceCalendarSlotDetailsVO.SlotPeriod = slotconfig[i].SlotPeriod;
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                }
                BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
            }

            foreach (var rsl in varGetResourceAvailableWithCraftSlots)
            {
                ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                var obj = GetResourceAvailableWithCraftSlots.FindAll(c => c.vResourceID == rsl.CraftID);
                valuesResourceCalendarVO.UserFullName = rsl.CraftName;
                valuesResourceCalendarVO.UserID = rsl.UserID;
                for (int i = 0; i < rsl.ResourceSlotVO.Count; i++)
                {
                    ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();

                    var slot = obj.Find(s => s.vSlotPeriod == rsl.ResourceSlotVO[i].SlotPeriod);
                    if (slot != null)
                    {
                        valResourceCalendarSlotDetailsVO.VCN = slot.vVCN;
                        valResourceCalendarSlotDetailsVO.SlotText = slot.vVCN + "-" + slot.vMovementType;
                        valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                    }
                    else
                    {
                        if (rsl.ResourceSlotVO[i].CraftStatus == "IC")
                        {
                            valResourceCalendarSlotDetailsVO.VCN = "";
                            valResourceCalendarSlotDetailsVO.SlotText = "";
                            valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                        }
                        else
                        {
                            valResourceCalendarSlotDetailsVO.VCN = "Out of Commission";
                            valResourceCalendarSlotDetailsVO.SlotText = "Out of Commission";
                            valResourceCalendarSlotDetailsVO.AttendanceStatus = "N";
                        }
                    }
                    valResourceCalendarSlotDetailsVO.SlotPeriod = rsl.ResourceSlotVO[i].SlotPeriod;
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                }
                BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
            }

            foreach (var rsl in varGetResourceAvailableWithOutCraftSlots)
            {
                ResourceCalendarVO valuesResourceCalendarVO = new ResourceCalendarVO();
                valuesResourceCalendarVO.ResourceCalendarSlotDetails = new List<ResourceCalendarSlotDetailsVO>();
                valuesResourceCalendarVO.UserFullName = rsl.CraftName;
                valuesResourceCalendarVO.UserID = rsl.UserID;
                for (int i = 0; i < rsl.ResourceSlotVO.Count; i++)
                {
                    ResourceCalendarSlotDetailsVO valResourceCalendarSlotDetailsVO = new ResourceCalendarSlotDetailsVO();
                    if (rsl.ResourceSlotVO[i].CraftStatus == "IC")
                    {
                        valResourceCalendarSlotDetailsVO.VCN = "";
                        valResourceCalendarSlotDetailsVO.SlotText = "";
                        valResourceCalendarSlotDetailsVO.AttendanceStatus = "Y";
                    }
                    else
                    {
                        valResourceCalendarSlotDetailsVO.VCN = "Out of Commission";
                        valResourceCalendarSlotDetailsVO.SlotText = "Out of Commission";
                        valResourceCalendarSlotDetailsVO.AttendanceStatus = "N";
                    }
                    valResourceCalendarSlotDetailsVO.SlotPeriod = rsl.ResourceSlotVO[i].SlotPeriod;
                    valuesResourceCalendarVO.ResourceCalendarSlotDetails.Add(valResourceCalendarSlotDetailsVO);
                }
                BindinglstResourceCalendarVO.Add(valuesResourceCalendarVO);
            }

            return BindinglstResourceCalendarVO;
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   :13th Jan  2015
        /// Purpose: To get all Crafts Availability ServiceTypes details
        /// </summary>
        /// <returns></returns>
        public List<ServiceTypeVO> GetCraftAvailabilityServiceTypes()
        {
            var servicetypes = (from s in _unitOfWork.Repository<ServiceType>().Queryable()
                                where s.IsServiceType == "N" && s.IsCraft == true
                                select s).ToList();

            return servicetypes.MapToDto();
        }

        public ResourceAllocationVO GetResourceAllocationById(int resourceAllocationId)
        {
            var parresourceAllocationId = new SqlParameter("@resourceAllocationId", resourceAllocationId);
            var resourceAllocation = _unitOfWork.SqlQuery<ResourceAllocationVO>("usp_GetResourceAllocationByID @resourceAllocationId", parresourceAllocationId).FirstOrDefault();

            return resourceAllocation;
        }

        private string GetSlotPeriodBySlotdate(DateTime slotdatetime, string port)
        {
            int hours = slotdatetime.Hour;
            string slotPeriod = string.Empty;
            double totalminutes = slotdatetime.TimeOfDay.TotalMinutes;

            if (slotdatetime != DateTime.MinValue)
            {
                foreach (ResourceSlotVO slot in _suppServiceResourceAllocRepository.GetSlotConfiguration(slotdatetime, port))
                {
                    string[] period = slot.SlotPeriod.Split('-');
                    //int startTime = Convert.ToInt32(period[0], CultureInfo.InvariantCulture);
                    //int endTime = Convert.ToInt32(period[1], CultureInfo.InvariantCulture);

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
                        slotPeriod = slot.SlotPeriod;
                        break;
                    }
                }
            }
            return slotPeriod;
        }

        public bool VerifyMovementIsActiveByVCNAndServiceRequestId(string vcn, int serviceRequestId)
        {
            var vessel = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(t => t.VCN == vcn).Include(vcm=>vcm.ServiceRequest)
                .OrderByDescending(s => s.ServiceRequest.WorkflowInstanceId) select vcm).FirstOrDefault();

            if (vessel != null)
            {
                return vessel.ServiceRequestID == serviceRequestId;
            }
            else
            {
                return false;
            }
        }

        public List<AutomatedSlotBlockingVO> GetAutomatedBlockedSlots(List<ResourceSlotVO> resourceSlots, string portCode)
        {          

            var allblockedSlots = new List<AutomatedSlotBlockingVO>();            

            var pendingSlots = new List<ResourceSlotVO>();

            var blockSlots = (from sb in _unitOfWork.Repository<AutomatedSlotBlocking>().Queryable()
                               .Include(sb => sb.SubCategory).Where(sb => sb.RecordStatus == RecordStatus.Active && sb.PortCode == portCode)
                              select sb).ToList<AutomatedSlotBlocking>();

            var blockedSlotsList = blockSlots != null ? blockSlots.MapToDto() : null;

            foreach (var slot in resourceSlots)
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
                    // block.SlotToDate = block.SlotToDate.Date.AddHours(0).AddMinutes(blockEndTime).AddSeconds(0);

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
                        //item.SlotNumber = slot.SlotNumber;
                        slot.TaskStatus = "BLCK";
                        allblockedSlots.Add(item);
                    }
                }
            }

            return allblockedSlots;
        }

    }
}
