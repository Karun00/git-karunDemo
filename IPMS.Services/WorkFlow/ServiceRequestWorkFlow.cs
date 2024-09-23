using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPMS.Repository;
using System.Web.Script.Serialization;
using System.ComponentModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain;
using System.Web.UI;
using IPMS.Core.Repository.Exceptions;
using System.Globalization;

namespace IPMS.Services.WorkFlow
{
    public class ServiceRequestWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ServiceRequest servicerequest;
        private const string entityCode = EntityCodes.Service_Request;
        private IPortGeneralConfigsRepository portGeneralConfigurationRepository;
        private string _remarks;
        private IServiceRequestRepository serviceRequestRepository;
        private IDepartureNoticeRepository departureNoticeRepository;

        private IVesselCallRepository vesselCallRepository;
        private ISuppServiceResourceAllocRepository suppServiceResourceAllocRepository = null;
        private CompanyVO vo = null;

        public ServiceRequestWorkFlow(IUnitOfWork unitOfWork, ServiceRequest request, string remarks)
        {
            _unitOfWork = unitOfWork;
            servicerequest = request;
            _remarks = remarks;
            serviceRequestRepository = new ServiceRequestRepository(unitOfWork);
            vesselCallRepository = new VesselCallRepository(unitOfWork);
            portGeneralConfigurationRepository = new PortGeneralConfigsRepository(unitOfWork);
            suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(unitOfWork);
            departureNoticeRepository = new DepartureNoticeRepository(unitOfWork);
            vo = new CompanyVO();
        }

        public Entity Entity
        {
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                              where e.EntityCode == entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public int userid
        {
            get { return servicerequest.CreatedBy; }
        }
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(servicerequest.ArrivalNotification.PortCode);
                return portcodes;
            }
        }

        public string ReferenceId
        {
            get { return Convert.ToString(servicerequest.ServiceRequestID, CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public int GetRequestStatus(string pentitycode, string preferenceno)
        {
            var wfportcode = servicerequest.ArrivalNotification.PortCode;

            var entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                              join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                              join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                              join pc in _unitOfWork.Repository<PortGeneralConfig>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode, _approvecode = ConfigName.ApprovedCode } equals new { taskcode = portGeneralConfigurationRepository.GetWFApprovedCode(wfportcode), portcode = pc.PortCode, _approvecode = pc.ConfigName }
                              where e.EntityCode == pentitycode
                                && w.ReferenceID == preferenceno

                              select w).Count();

            return entitycode;
        }

        public ServiceRequestWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

        public void UpdateStatus()
        {
        }

        public string ReferenceData
        {
            get { return Common.GetTokensDictionaryForReferenceData(Entity, servicerequest); }
        }



        private bool ValidateServiceRequest()
        {

            DateTime dt1 = servicerequest.MovementDateTime;
            bool bunkering = false;
            if (servicerequest.ServiceRequestID == null || servicerequest.ServiceRequestID == 0)
            {
                if (servicerequest.ArrivalNotification == null)
                {
                    throw new BusinessExceptions("Your VCN Is Not yet Approved by BerthPlanner");
                }
                if (servicerequest.ArrivalNotification != null)
                {
                    List<string> ArrivalReasons = _unitOfWork.Repository<ArrivalReason>().Queryable().Where(s => s.RecordStatus == RecordStatus.Active && s.VCN == servicerequest.VCN).Select(s => s.Reason).ToList<String>();

                    bunkering = false;
                    int count = 0;
                    foreach (var item in ArrivalReasons)
                    {
                        if (item == SuperCategoryConstants.Reason_Bunkering)
                        { bunkering = true; }
                        else
                        {
                            if (bunkering == false) { bunkering = false; }
                            count++;
                        }
                    }
                    if (count > 0)
                    { bunkering = false; }
                }

                var vcm = serviceRequestRepository.GetVCallMovtAtVCN(servicerequest.VCN, servicerequest.ArrivalNotification.PortCode);

                var vcmarrival = serviceRequestRepository.GetVCallMovtAtVCNArrival(servicerequest.VCN, servicerequest.ArrivalNotification.PortCode);
                if (vcm == null)
                {
                    throw new BusinessExceptions("Your VCN Is Not yet Approved by BerthPlanner");
                }

                var objVesselCall = vesselCallRepository.VesselCallDetails(servicerequest.VCN);
                var currenttime = DateTime.Now;
                var servdata = serviceRequestRepository.GetServRequestDeatailsForValidation(servicerequest.VCN);

                var servdataarrival = serviceRequestRepository.GetServRequestDeatailsForValidationForArrival(servicerequest.VCN);

                var autoSloBlocking = serviceRequestRepository.GetBlockedSlots(servicerequest.ArrivalNotification.PortCode);

                var AutoConfiguredSlots = serviceRequestRepository.GetAutoConfiguredSlots(servicerequest.MovementDateTime, servicerequest.ArrivalNotification.PortCode);
                var totalSlotsAvailable = serviceRequestRepository.GetTotalSlotsAvailable(servicerequest.MovementDateTime, servicerequest.MovementSlot, servicerequest.ArrivalNotification.PortCode);
                var ETASlot = serviceRequestRepository.GetSlotPeriodBySlotDate(objVesselCall.ETA, servicerequest.ArrivalNotification.PortCode);

                var portconfigurationTime = portGeneralConfigurationRepository.GetPortConfiguration(servicerequest.ArrivalNotification.PortCode, ConfigName.ServiceRequestCondition1);
                var portconfigurationTime1 = portGeneralConfigurationRepository.GetPortConfiguration(servicerequest.ArrivalNotification.PortCode, ConfigName.ServiceRequestBunkersCondition);
                var departurenotice = departureNoticeRepository.GetDepartureNoticeServiceRequest(servicerequest.VCN);

                if (servdataarrival.Count == 0)
                {

                    if (servicerequest.MovementType != MovementTypes.ARRIVAL)
                    {
                        throw new BusinessExceptions("Arrival Request should be initiated first");

                    }
                    if (servicerequest.MovementType == MovementTypes.ARRIVAL)
                    {
                        var servdataarrival1 = serviceRequestRepository.GetAllServRequestDeatailsForValidationForArrival(servicerequest.VCN);

                        if (vcmarrival.ServiceRequestID == null && (vcmarrival.MovementStatus == MovementStatus.CONFIRMED || vcmarrival.MovementStatus == MovementStatus.SCHEDULED))
                        {

                        }
                        else if (servdataarrival1 != null && servdataarrival1.RecordStatus == "I" && (vcmarrival.MovementStatus == MovementStatus.CONFIRMED || vcmarrival.MovementStatus == MovementStatus.SCHEDULED))
                        {

                        }
                        else
                        {
                            throw new BusinessExceptions("Berth is not yet scheduled for VCN : '" + servicerequest.VCN + "'");
                        }


                        DateTime dt2 = Convert.ToDateTime(objVesselCall.ETA);
                        DateTime dt3 = Convert.ToDateTime(objVesselCall.ETD);
                        if (dt3 > dt2)
                        {
                            if (!(dt2 <= servicerequest.MovementDateTime && servicerequest.MovementDateTime <= dt3))
                            {
                                if (bunkering == true)
                                {
                                    if (servicerequest.MovementDateTime <= ETASlot.StartDate && servicerequest.MovementDateTime <= ETASlot.EndDate)
                                    {
                                        throw new BusinessExceptions("Movement Date & Slot for Raised Request Should be between ETA and ETD");
                                    }
                                }
                                else
                                {
                                    throw new BusinessExceptions("Movement Date & Slot for Raised Request Should be between ETA and ETD");
                                }
                            }
                            if (servicerequest.MovementDateTime != null)
                            {
                                if (servicerequest.IsUpdateMovement == true)
                                {
                                    if (bunkering == true)
                                    {
                                        TimeSpan timespan = servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
                                        double timeDifference = timespan.TotalMinutes;
                                        double timelimit1 = Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture) / 60;
                                        if (Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture) > 0)
                                        {
                                            if (timeDifference < Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture))
                                            {
                                                throw new BusinessExceptions("The Movement Date & Slot should be atleast " + timelimit1 + " hours in future from the current Date & Time");
                                            }
                                        }
                                        else
                                        {
                                            var CurrentSlot = serviceRequestRepository.GetSlotPeriodBySlotDate(currenttime, servicerequest.ArrivalNotification.PortCode);
                                            if (servicerequest.MovementDateTime <= CurrentSlot.StartDate && servicerequest.MovementDateTime <= CurrentSlot.EndDate)
                                            {
                                                throw new BusinessExceptions("Service Request Movement Date & Slot should be greater than current Date & Time");
                                            }

                                        }
                                    }
                                    else
                                    {
                                        TimeSpan timespan = servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
                                        double timeDifference = timespan.TotalMinutes;
                                        double timelimit1 = Convert.ToDouble(portconfigurationTime) / 60;
                                        if (timeDifference < Convert.ToDouble(portconfigurationTime))
                                        {
                                            throw new BusinessExceptions("The Movement Date & Slot should be atleast " + timelimit1 + " hours in future from the current Date & Time");
                                        }
                                    }
                                }
                            }
                        }

                        if (servicerequest.MovementDateTime != null)
                        {
                            foreach (var slot in autoSloBlocking)
                            {
                                DateTime ab1 = Convert.ToDateTime(slot.FromDate);
                                DateTime ab2 = Convert.ToDateTime(slot.ToDate);

                                string fromDate = ab1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                                string toDate = ab2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                                ab1 = ab1.Date.AddHours(0).AddMinutes(slot.StartTime).AddSeconds(0);

                                if (slot.StartTime > slot.EndTime)
                                {
                                    ab2 = ab2.AddDays(1);
                                }

                                ab2 = ab2.Date.AddHours(0).AddMinutes(slot.EndTime).AddSeconds(0);
                                //mahesh k (21/09/2023)NIT_IPMS03 Blocked can still be booked
                                var servicemomentDatetime =servicerequest .MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                                //end mahesh K

                                //if (ab1 < servicerequest.MovementDateTime && servicerequest.MovementDateTime < ab2)
                                if (ab1 < servicemomentDatetime && servicemomentDatetime < ab2)
                                {
                                    throw new BusinessExceptions("Slots from " + fromDate + ' ' + slot.SlotFrom + " to " + toDate + ' ' + slot.SlotTo + " are blocked due to " + slot.ReasonName);
                                }
                            }
                        }

                        if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                        {
                            if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                            {
                                throw new BusinessExceptions("Cannot assign the slot " + servicerequest.MovementSlot + " as it reached the maximum limit. Please select another Slot");
                            }
                        }

                    }

                    return false;
                }


                if (servdata.Count > 0)
                {
                    if (vcm != null)
                    {

                        if (servicerequest.MovementType == MovementTypes.SHIFTING || servicerequest.MovementType == MovementTypes.WARPING || servicerequest.MovementType == MovementTypes.SAILING && vcm.MovementType == MovementTypes.ARRIVAL)
                        {

                            var serviceallmoments = serviceRequestRepository.GetAllServRequestDeatailsAllMoments(servicerequest.VCN, servicerequest.MovementType);

                            if (vcm.ATB == null && serviceallmoments != null && serviceallmoments.RecordStatus == "A")
                            {
                                throw new BusinessExceptions("Another Service Request already in Process for this VCN");
                            }
                        }

                        if (servicerequest.MovementType == MovementTypes.SHIFTING || servicerequest.MovementType == MovementTypes.WARPING || servicerequest.MovementType == MovementTypes.SAILING)
                        {
                            DateTime dt4 = Convert.ToDateTime(objVesselCall.ETA, CultureInfo.InvariantCulture);
                            DateTime dt5 = Convert.ToDateTime(objVesselCall.ETD, CultureInfo.InvariantCulture);

                            if (dt5 > dt4)
                            {
                                if (!(dt4 <= servicerequest.MovementDateTime && servicerequest.MovementDateTime <= dt5))
                                    throw new BusinessExceptions("Movement Date & Slot for Raised Request Should be between ETA and ETD");
                            }
                        }

                        if (servicerequest.MovementDateTime != null)
                        {
                            if (servicerequest.IsUpdateMovement == true)
                            {
                                if (bunkering == true)
                                {
                                    TimeSpan timespan = servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
                                    double timeDifference = timespan.TotalMinutes;
                                    double timelimit = Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture) / 60;
                                    if (Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture) > 0)
                                    {
                                        if (timeDifference < Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture))
                                        {
                                            throw new BusinessExceptions("Service Request should be raised atleast " + timelimit + " hrs Prior to Movement's Date & Time");
                                        }
                                    }
                                    else
                                    {
                                        var CurrentSlot = serviceRequestRepository.GetSlotPeriodBySlotDate(currenttime, servicerequest.ArrivalNotification.PortCode);
                                        if (servicerequest.MovementDateTime <= CurrentSlot.StartDate && servicerequest.MovementDateTime <= CurrentSlot.EndDate)
                                        {
                                            throw new BusinessExceptions("Service Request Movement Date & Slot should be greater than current Date & Time");
                                        }

                                    }
                                }
                                else
                                {
                                    TimeSpan timespan = servicerequest.MovementDateTime - Convert.ToDateTime(currenttime, CultureInfo.InvariantCulture);
                                    double timeDifference = timespan.TotalMinutes;

                                    double timelimit = Convert.ToDouble(portconfigurationTime, CultureInfo.InvariantCulture) / 60;

                                    if (timeDifference < Convert.ToDouble(portconfigurationTime, CultureInfo.InvariantCulture))
                                    {
                                        throw new BusinessExceptions("Service Request should be raised atleast " + timelimit + " hrs Prior to Movement's Date & Time");
                                    }
                                }
                            }

                            foreach (var slot in autoSloBlocking)
                            {
                                DateTime ab1 = Convert.ToDateTime(slot.FromDate);
                                DateTime ab2 = Convert.ToDateTime(slot.ToDate);

                                string fromDate = ab1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                                string toDate = ab2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                                ab1 = ab1.Date.AddHours(0).AddMinutes(slot.StartTime).AddSeconds(0);

                                if (slot.StartTime > slot.EndTime)
                                {
                                    ab2 = ab2.AddDays(1);
                                }

                                ab2 = ab2.Date.AddHours(0).AddMinutes(slot.EndTime).AddSeconds(0);


                                if (ab1 < servicerequest.MovementDateTime && servicerequest.MovementDateTime < ab2)
                                {
                                    throw new BusinessExceptions("Slots from " + fromDate + ' ' + slot.SlotFrom + " to " + toDate + ' ' + slot.SlotTo + " are blocked due to " + slot.ReasonName);
                                }
                            }

                            if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                            {
                                if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                                {
                                    throw new BusinessExceptions("Cannot assign the slot " + servicerequest.MovementSlot + " as it reached the maximum limit. Please select another Slot");
                                }
                            }


                        }

                        if (servicerequest.MovementType == MovementTypes.SHIFTING || servicerequest.MovementType == MovementTypes.WARPING || servicerequest.MovementType == MovementTypes.SAILING)
                        {
                            DateTime dt2 = Convert.ToDateTime(vcm.ATB, CultureInfo.InvariantCulture);
                            if (dt2 > dt1)
                            {
                                throw new BusinessExceptions("Movement Date & Slot for Raised Request Should be greater than" + vcm.ATB);
                            }
                        }




                    }

                    if (departurenotice == null && servicerequest.MovementType == MovementTypes.SAILING)
                    {
                        throw new BusinessExceptions("Departure Notice has not been Acknowledged");
                    }
                    foreach (var ser in servdata)
                    {
                        if (ser.VCN == servicerequest.VCN)
                        {

                            if (servicerequest.MovementType == MovementTypes.ARRIVAL)
                            {
                                foreach (var item in servdata)
                                {
                                    if (item.MovementType == MovementTypes.ARRIVAL)
                                    {
                                        throw new BusinessExceptions("An Arrival Request is already raised against this VCN");
                                    }

                                }
                                return false;
                            }
                            if (servicerequest.MovementType == MovementTypes.SAILING)
                            {
                                foreach (var item in servdata)
                                {
                                    if (item.MovementType == MovementTypes.SAILING)
                                    {
                                        throw new BusinessExceptions("A Sailing Request is already raised against this VCN");
                                    }
                                }
                                return false;
                            }


                            if (servicerequest.MovementType == MovementTypes.SHIFTING || servicerequest.MovementType == MovementTypes.WARPING)
                            {
                                foreach (var item in servdata)
                                {
                                    if (item.MovementType == MovementTypes.SAILING)
                                    {

                                        throw new BusinessExceptions("Sailing Request is already raised for this VCN, Hence Cannot Process this request");
                                    }
                                }
                                return false;
                            }



                        }
                        return false;
                    }

                }
                else
                {
                    if (servicerequest.MovementType != MovementTypes.ARRIVAL)
                    {
                        throw new BusinessExceptions("Arrival Request should be initiated first");

                    }
                    if (servicerequest.MovementType == MovementTypes.ARRIVAL)
                    {
                        if (vcm.ServiceRequestID == null && (vcm.MovementStatus == MovementStatus.CONFIRMED || vcm.MovementStatus == MovementStatus.SCHEDULED))
                        {

                        }
                        else
                        {
                            throw new BusinessExceptions("Berth is not yet scheduled for VCN : '" + servicerequest.VCN + "'");
                        }


                        DateTime dt2 = Convert.ToDateTime(objVesselCall.ETA, CultureInfo.InvariantCulture);
                        DateTime dt3 = Convert.ToDateTime(objVesselCall.ETD, CultureInfo.InvariantCulture);
                        if (dt3 > dt2)
                        {
                            if (!(dt2 <= servicerequest.MovementDateTime && servicerequest.MovementDateTime <= dt3))
                            {
                                if (bunkering == true)
                                {
                                    if (servicerequest.MovementDateTime <= ETASlot.StartDate && servicerequest.MovementDateTime <= ETASlot.EndDate)
                                    {
                                        throw new BusinessExceptions("Movement Date & Slot for Raised Request Should be between ETA and ETD");
                                    }
                                }
                                else
                                {
                                    throw new BusinessExceptions("Movement Date & Slot for Raised Request Should be between ETA and ETD");
                                }
                            }
                            if (servicerequest.MovementDateTime != null)
                            {
                                if (servicerequest.IsUpdateMovement == true)
                                {
                                    if (bunkering == true)
                                    {
                                        TimeSpan timespan = servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
                                        double timeDifference = timespan.TotalMinutes;
                                        double timelimit1 = Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture) / 60;
                                        if (Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture) > 0)
                                        {
                                            if (timeDifference < Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture))
                                            {
                                                throw new BusinessExceptions("The Movement Date & Slot should be atleast " + timelimit1 + " hours in future from the current Date & Time");
                                            }
                                        }
                                        else
                                        {
                                            var CurrentSlot = serviceRequestRepository.GetSlotPeriodBySlotDate(currenttime, servicerequest.ArrivalNotification.PortCode);
                                            if (servicerequest.MovementDateTime <= CurrentSlot.StartDate && servicerequest.MovementDateTime <= CurrentSlot.EndDate)
                                            {
                                                throw new BusinessExceptions("Service Request Movement Date & Slot should be greater than current Date & Time");
                                            }

                                        }
                                    }
                                    else
                                    {
                                        TimeSpan timespan = servicerequest.MovementDateTime - Convert.ToDateTime(currenttime, CultureInfo.InvariantCulture);
                                        double timeDifference = timespan.TotalMinutes;
                                        double timelimit1 = Convert.ToDouble(portconfigurationTime, CultureInfo.InvariantCulture) / 60;
                                        if (timeDifference < Convert.ToDouble(portconfigurationTime, CultureInfo.InvariantCulture))
                                        {
                                            throw new BusinessExceptions("The Movement Date & Slot should be atleast " + timelimit1 + " hours in future from the current Date & Time");
                                        }
                                    }

                                }

                                foreach (var slot in autoSloBlocking)
                                {
                                    DateTime ab1 = Convert.ToDateTime(slot.FromDate);
                                    DateTime ab2 = Convert.ToDateTime(slot.ToDate);

                                    string fromDate = ab1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                                    string toDate = ab2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                                    ab1 = ab1.Date.AddHours(0).AddMinutes(slot.StartTime).AddSeconds(0);

                                    if (slot.StartTime > slot.EndTime)
                                    {
                                        ab2 = ab2.AddDays(1);
                                    }

                                    ab2 = ab2.Date.AddHours(0).AddMinutes(slot.EndTime).AddSeconds(0);

                                    if (ab1 < servicerequest.MovementDateTime && servicerequest.MovementDateTime < ab2)
                                    {
                                        throw new BusinessExceptions("Slots from " + fromDate + ' ' + slot.SlotFrom + " to " + toDate + ' ' + slot.SlotTo + " are blocked due to " + slot.ReasonName);
                                    }
                                }

                                if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                                {
                                    if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                                    {
                                        throw new BusinessExceptions("Cannot assign the slot " + servicerequest.MovementSlot + " as it reached the maximum limit. Please select another Slot");
                                    }
                                }

                            }
                        }

                    }

                    return false;
                }
            }
            else
            {

                if (servicerequest != null)
                {
                    List<string> ArrivalReasons = _unitOfWork.Repository<ArrivalReason>().Queryable().Where(s => s.RecordStatus == RecordStatus.Active && s.VCN == servicerequest.VCN).Select(s => s.Reason).ToList<String>();

                    bunkering = false;
                    int count = 0;
                    foreach (var item in ArrivalReasons)
                    {
                        if (item == SuperCategoryConstants.Reason_Bunkering)
                        { bunkering = true; }
                        else
                        {
                            if (bunkering == false) { bunkering = false; }
                            count++;
                        }
                    }
                    if (count > 0)
                    { bunkering = false; }
                }

                var objVesselCalls = vesselCallRepository.VesselCallDetails(servicerequest.VCN);
                var portconfigurationTime = portGeneralConfigurationRepository.GetPortConfiguration(objVesselCalls.FromPositionPortCode, ConfigName.ServiceRequestCondition1);
                var portconfigurationTime1 = portGeneralConfigurationRepository.GetPortConfiguration(objVesselCalls.FromPositionPortCode, ConfigName.ServiceRequestBunkersCondition);
                var vcmObj = serviceRequestRepository.GetVCallMovtAtVCN(servicerequest.VCN, objVesselCalls.FromPositionPortCode);
                var autoSloBlocking = serviceRequestRepository.GetBlockedSlots(objVesselCalls.FromPositionPortCode);

                var AutoConfiguredSlots = serviceRequestRepository.GetAutoConfiguredSlots(servicerequest.MovementDateTime, objVesselCalls.FromPositionPortCode);
                var totalSlotsAvailable = serviceRequestRepository.GetTotalSlotsAvailable(servicerequest.MovementDateTime, servicerequest.MovementSlot, objVesselCalls.FromPositionPortCode);
                var ETASlot = serviceRequestRepository.GetSlotPeriodBySlotDate(objVesselCalls.ETA, objVesselCalls.FromPositionPortCode);

                var currenttime = DateTime.Now;
                DateTime dt2 = Convert.ToDateTime(objVesselCalls.ETA, CultureInfo.InvariantCulture);
                DateTime dt3 = Convert.ToDateTime(objVesselCalls.ETD, CultureInfo.InvariantCulture);
                DateTime dt4 = Convert.ToDateTime(vcmObj.ATB, CultureInfo.InvariantCulture);

                if (dt3 > dt2)
                {
                    if (!(dt2 <= servicerequest.MovementDateTime && servicerequest.MovementDateTime <= dt3))
                    {
                        if (bunkering == true)
                        {
                            if (servicerequest.MovementDateTime <= ETASlot.StartDate && servicerequest.MovementDateTime <= ETASlot.EndDate)
                            {
                                throw new BusinessExceptions("Movement Date & Slot for Updating Request Should be between ETA and ETD");
                            }
                        }
                        else
                        {
                            throw new BusinessExceptions("Movement Date & Slot for Updating Request Should be between ETA and ETD");
                        }
                    }
                    if (servicerequest.MovementDateTime != null)
                    {
                        if (servicerequest.IsUpdateMovement == true)
                        {
                            if (bunkering == true)
                            {
                                TimeSpan timespan = servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
                                double timeDifference = timespan.TotalMinutes;
                                double timelimit2 = Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture) / 60;
                                if (Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture) > 0)
                                {
                                    if (timeDifference < Convert.ToDouble(portconfigurationTime1, CultureInfo.InvariantCulture))
                                    {
                                        throw new BusinessExceptions("The Movement Date & Slot should be atleast " + timelimit2 + " hours in future from the current Date & Time");
                                    }
                                }
                                else
                                {
                                    if (servicerequest.MovementDateTime < Convert.ToDateTime(currenttime))
                                    {
                                        throw new BusinessExceptions("Service Request Movement Date & Slot should be greater than current Date & Time");
                                    }
                                }
                            }
                            else
                            {
                                TimeSpan timespan = servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
                                double timeDifference = timespan.TotalMinutes;
                                double timelimit2 = Convert.ToDouble(portconfigurationTime, CultureInfo.InvariantCulture) / 60;
                                if (timeDifference < Convert.ToDouble(portconfigurationTime, CultureInfo.InvariantCulture))
                                {
                                    throw new BusinessExceptions("The Movement Date & Slot should be atleast " + timelimit2 + " hours in future from the current Date & Time");
                                }
                            }
                        }

                        foreach (var slot in autoSloBlocking)
                        {
                            DateTime ab1 = Convert.ToDateTime(slot.FromDate);
                            DateTime ab2 = Convert.ToDateTime(slot.ToDate);

                            string fromDate = ab1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                            string toDate = ab2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                            ab1 = ab1.Date.AddHours(0).AddMinutes(slot.StartTime).AddSeconds(0);

                            if (slot.StartTime > slot.EndTime)
                            {
                                ab2 = ab2.AddDays(1);
                            }

                            ab2 = ab2.Date.AddHours(0).AddMinutes(slot.EndTime).AddSeconds(0);

                            if (ab1 < servicerequest.MovementDateTime && servicerequest.MovementDateTime < ab2)
                            {
                                throw new BusinessExceptions("Slots from " + fromDate + ' ' + slot.SlotFrom + " to " + toDate + ' ' + slot.SlotTo + " are blocked due to " + slot.ReasonName);
                            }
                        }

                        if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                        {
                            if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                            {
                                throw new BusinessExceptions("Cannot assign the slot " + servicerequest.MovementSlot + " as it reached the maximum limit. Please select another Slot");
                            }
                        }
                    }
                }
                return false;
            }
            return false;

        }

        public void Update()
        {
            ValidateServiceRequest();
            if (servicerequest.ArrivalNotification == null)
            {
                servicerequest.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(servicerequest.VCN);
            }
            if (servicerequest.OwnSteam == "True")
                servicerequest.OwnSteam = "Y";
            else
                servicerequest.OwnSteam = "N";
            if (servicerequest.NoMainEngine == "True")
                servicerequest.NoMainEngine = "Y";
            else
                servicerequest.NoMainEngine = "N";
            if (servicerequest.IsTidal == "True")
                servicerequest.IsTidal = "Y";
            else
                servicerequest.IsTidal = "N";

            List<ServiceRequestSailing> serviceRequestSailings = servicerequest.ServiceRequestSailings.ToList();
            Document doc = new Document();
            if (serviceRequestSailings.Count > 0)
            {
                serviceRequestSailings.ElementAt(0).Document = null;

            }

            List<ServiceRequestDocument> serviceRequestDocumentList = servicerequest.ServiceRequestDocuments.ToList();
            List<ServiceRequestShifting> serviceRequestShiftings = servicerequest.ServiceRequestShiftings.ToList();
            List<ServiceRequestWarping> serviceRequestWarpings = servicerequest.ServiceRequestWarpings.ToList();

            servicerequest.ServiceRequestShiftings = null;
            servicerequest.ServiceRequestSailings = null;
            servicerequest.ServiceRequestWarpings = null;
            servicerequest.ServiceRequestDocuments = null;

            servicerequest.ObjectState = ObjectState.Modified;
            //added mahesh K NIT_IPMS04            
            string[] slotstperiod = servicerequest.SlotPeriod.Split('-');
            var AutoConfiguredSlots = serviceRequestRepository.GetAutoConfiguredSlots(servicerequest.MovementDateTime, servicerequest.ArrivalNotification.PortCode);
            if (slotstperiod[1] == SlotPeriodTimeStatus.slotperiod1)
            {
                servicerequest.MovementDateTime = servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
            }
            //end
            _unitOfWork.Repository<ServiceRequest>().Update(servicerequest);

            var brt = _unitOfWork.ExecuteSqlCommand("delete dbo.ServiceRequestDocument where ServiceRequestID = @p0", servicerequest.ServiceRequestID);

            if (serviceRequestDocumentList.Count > 0)
            {
                foreach (var document in serviceRequestDocumentList)
                {
                    document.ServiceRequestID = servicerequest.ServiceRequestID;
                    document.CreatedBy = servicerequest.CreatedBy;
                    document.CreatedDate = servicerequest.CreatedDate;
                    document.ModifiedBy = servicerequest.ModifiedBy;
                    document.ModifiedDate = servicerequest.ModifiedDate;
                    document.RecordStatus = "A";
                }

                _unitOfWork.Repository<ServiceRequestDocument>().InsertRange(serviceRequestDocumentList);
            }


            foreach (var item1 in serviceRequestSailings)
            {
                item1.ServiceRequestID = servicerequest.ServiceRequestID;
                if (item1.MarineRevenueCleared == "True")
                    item1.MarineRevenueCleared = "Y";
                else
                    item1.MarineRevenueCleared = "N";
                item1.CreatedBy = servicerequest.CreatedBy;
                item1.CreatedDate = servicerequest.CreatedDate;
                item1.ModifiedBy = servicerequest.ModifiedBy;
                item1.ModifiedDate = servicerequest.ModifiedDate;
                item1.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestSailing>().Update(item1);

            }
            foreach (var item2 in serviceRequestShiftings)
            {
                item2.ServiceRequestID = servicerequest.ServiceRequestID;
                item2.CreatedBy = servicerequest.CreatedBy;
                item2.CreatedDate = servicerequest.CreatedDate;
                item2.ModifiedBy = servicerequest.ModifiedBy;
                item2.ModifiedDate = servicerequest.ModifiedDate;
                item2.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestShifting>().Update(item2);

            }
            foreach (var item3 in serviceRequestWarpings)
            {
                item3.ServiceRequestID = servicerequest.ServiceRequestID;
                item3.CreatedBy = servicerequest.CreatedBy;
                item3.CreatedDate = servicerequest.CreatedDate;
                item3.ModifiedBy = servicerequest.ModifiedBy;
                item3.ModifiedDate = servicerequest.ModifiedDate;
                item3.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestWarping>().Update(item3);

            }
            _unitOfWork.SaveChanges();
        }



        public void Create()
        {

            ValidateServiceRequest();
            if (servicerequest.ArrivalNotification == null)
            {
                servicerequest.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(servicerequest.VCN);
            }
            List<ServiceRequestSailing> serviceRequestSailings = servicerequest.ServiceRequestSailings.ToList();
            Document doc = new Document();
            if (serviceRequestSailings.Count > 0)
            {
                serviceRequestSailings.ElementAt(0).Document = null;
            }


            List<ServiceRequestDocument> serviceRequestDocumentList = servicerequest.ServiceRequestDocuments.ToList();
            List<ServiceRequestShifting> serviceRequestShiftings = servicerequest.ServiceRequestShiftings.ToList();
            List<ServiceRequestWarping> serviceRequestWarpings = servicerequest.ServiceRequestWarpings.ToList();

            servicerequest.ServiceRequestShiftings = null;
            servicerequest.ServiceRequestSailings = null;
            servicerequest.ServiceRequestWarpings = null;
            servicerequest.ServiceRequestDocuments = null;



            servicerequest.ObjectState = ObjectState.Added;
            if (servicerequest.OwnSteam == "True")
                servicerequest.OwnSteam = "Y";
            else
                servicerequest.OwnSteam = "N";
            if (servicerequest.NoMainEngine == "True")
                servicerequest.NoMainEngine = "Y";
            else
                servicerequest.NoMainEngine = "N";
            if (servicerequest.IsTidal == "True")
                servicerequest.IsTidal = "Y";
            else
                servicerequest.IsTidal = "N";

            //mahesh K NIT_IPMS04
            string[] slotstperiod = servicerequest.SlotPeriod.Split('-');
            var AutoConfiguredSlots =serviceRequestRepository.GetAutoConfiguredSlots(servicerequest.MovementDateTime,servicerequest.ArrivalNotification.PortCode);
            if (slotstperiod[1]== SlotPeriodTimeStatus.slotperiod1)
            {
                servicerequest.MovementDateTime=servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
            }
            //end mahesh k NIT_IPMS04

            _unitOfWork.Repository<ServiceRequest>().Insert(servicerequest);

            if (serviceRequestDocumentList.Count > 0)
            {
                foreach (var document in serviceRequestDocumentList)
                {
                    document.CreatedBy = servicerequest.CreatedBy;
                    document.CreatedDate = servicerequest.CreatedDate;
                    document.ModifiedBy = servicerequest.ModifiedBy;
                    document.ModifiedDate = servicerequest.ModifiedDate;
                    document.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestDocument>().Insert(document);
                }


            }

            foreach (var item1 in serviceRequestSailings)
            {
                item1.ServiceRequestID = servicerequest.ServiceRequestID;

                if (item1.MarineRevenueCleared == "True")
                    item1.MarineRevenueCleared = "Y";
                else
                    item1.MarineRevenueCleared = "N";
                item1.CreatedBy = servicerequest.CreatedBy;
                item1.CreatedDate = servicerequest.CreatedDate;
                item1.ModifiedBy = servicerequest.ModifiedBy;
                item1.ModifiedDate = servicerequest.ModifiedDate;
                item1.RecordStatus = "A";

                _unitOfWork.Repository<ServiceRequestSailing>().Insert(item1);

            }
            foreach (var item2 in serviceRequestShiftings)
            {
                item2.ServiceRequestID = servicerequest.ServiceRequestID;
                item2.CreatedBy = servicerequest.CreatedBy;
                item2.CreatedDate = servicerequest.CreatedDate;
                item2.ModifiedBy = servicerequest.ModifiedBy;
                item2.ModifiedDate = servicerequest.ModifiedDate;
                item2.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestShifting>().Insert(item2);

            }
            foreach (var item3 in serviceRequestWarpings)
            {
                item3.ServiceRequestID = servicerequest.ServiceRequestID;
                item3.CreatedBy = servicerequest.CreatedBy;
                item3.CreatedDate = servicerequest.CreatedDate;
                item3.ModifiedBy = servicerequest.ModifiedBy;
                item3.ModifiedDate = servicerequest.ModifiedDate;
                item3.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestWarping>().Insert(item3);

            }
            _unitOfWork.SaveChanges();
        }

        public void Cancel()
        {
            if (servicerequest.ArrivalNotification == null)
            {
                servicerequest.ArrivalNotification =
                    _unitOfWork.Repository<ArrivalNotification>().Find(servicerequest.VCN);
            }

            List<ServiceRequestSailing> serviceRequestSailings = servicerequest.ServiceRequestSailings.ToList();
            Document doc = new Document();
            if (serviceRequestSailings.Count > 0)
            {
                serviceRequestSailings.ElementAt(0).Document = null;
            }

            List<ServiceRequestShifting> serviceRequestShiftings = servicerequest.ServiceRequestShiftings.ToList();
            List<ServiceRequestWarping> serviceRequestWarpings = servicerequest.ServiceRequestWarpings.ToList();
            if (servicerequest.OwnSteam == "True")
                servicerequest.OwnSteam = "Y";
            else
                servicerequest.OwnSteam = "N";
            if (servicerequest.NoMainEngine == "True")
                servicerequest.NoMainEngine = "Y";
            else
                servicerequest.NoMainEngine = "N";
            if (servicerequest.IsTidal == "True")
                servicerequest.IsTidal = "Y";
            else
                servicerequest.IsTidal = "N";

            if (serviceRequestSailings.Count > 0)
            {
                foreach (var item1 in serviceRequestSailings)
                {
                    item1.ServiceRequestID = servicerequest.ServiceRequestID;
                    if (item1.MarineRevenueCleared == "True")
                        item1.MarineRevenueCleared = "Y";
                    else
                        item1.MarineRevenueCleared = "N";
                    item1.CreatedBy = servicerequest.CreatedBy;
                    item1.CreatedDate = servicerequest.CreatedDate;
                    item1.ModifiedBy = servicerequest.ModifiedBy;
                    item1.ModifiedDate = servicerequest.ModifiedDate;
                    item1.RecordStatus = "A";

                    _unitOfWork.Repository<ServiceRequestSailing>().Insert(item1);

                }
            }
            if (serviceRequestShiftings.Count > 0)
            {
                foreach (var item2 in serviceRequestShiftings)
                {
                    item2.ServiceRequestID = servicerequest.ServiceRequestID;
                    item2.CreatedBy = servicerequest.CreatedBy;
                    item2.CreatedDate = servicerequest.CreatedDate;
                    item2.ModifiedBy = servicerequest.ModifiedBy;
                    item2.ModifiedDate = servicerequest.ModifiedDate;
                    item2.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestShifting>().Insert(item2);

                }
            }
            if (serviceRequestWarpings.Count > 0)
            {
                foreach (var item3 in serviceRequestWarpings)
                {
                    item3.ServiceRequestID = servicerequest.ServiceRequestID;
                    item3.CreatedBy = servicerequest.CreatedBy;
                    item3.CreatedDate = servicerequest.CreatedDate;
                    item3.ModifiedBy = servicerequest.ModifiedBy;
                    item3.ModifiedDate = servicerequest.ModifiedDate;
                    item3.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestWarping>().Insert(item3);

                }
            }


            if (servicerequest.MovementType == "SHMV")
            {
                _unitOfWork.ExecuteSqlCommand("Update dbo.VesselCallMovement set FromPositionQuayCode = @p0, FromPositionBerthCode = @p0, FromPositionBollardCode = @p0, ToPositionPortCode = @p0, ToPositionQuayCode = @p0, ToPositionBerthCode = @p0, ToPositionBollardCode = @p0, MovementStatus = @p1, RecordStatus = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where ServiceRequestID = @p5", null, MovementStatus.PENDING, RecordStatus.InActive, servicerequest.ModifiedBy, servicerequest.ModifiedDate, servicerequest.ServiceRequestID);
            }

            servicerequest.ObjectState = ObjectState.Modified;
            servicerequest.RecordStatus = "I";
            _unitOfWork.Repository<ServiceRequest>().Update(servicerequest);
            _unitOfWork.SaveChanges();

        }

        public void Reject()
        {

            if (servicerequest.ArrivalNotification == null)
            {
                servicerequest.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(servicerequest.VCN);
            }
            List<ServiceRequestSailing> serviceRequestSailings = servicerequest.ServiceRequestSailings.ToList();
            Document doc = new Document();
            if (serviceRequestSailings.Count > 0)
            {
                serviceRequestSailings.ElementAt(0).Document = null;
            }

            List<ServiceRequestShifting> serviceRequestShiftings = servicerequest.ServiceRequestShiftings.ToList();
            List<ServiceRequestWarping> serviceRequestWarpings = servicerequest.ServiceRequestWarpings.ToList();
            if (servicerequest.OwnSteam == "True")
                servicerequest.OwnSteam = "Y";
            else
                servicerequest.OwnSteam = "N";
            if (servicerequest.NoMainEngine == "True")
                servicerequest.NoMainEngine = "Y";
            else
                servicerequest.NoMainEngine = "N";
            if (servicerequest.IsTidal == "True")
                servicerequest.IsTidal = "Y";
            else
                servicerequest.IsTidal = "N";

            if (serviceRequestSailings.Count > 0)
            {
                foreach (var item1 in serviceRequestSailings)
                {
                    item1.ServiceRequestID = servicerequest.ServiceRequestID;
                    if (item1.MarineRevenueCleared == "True")
                        item1.MarineRevenueCleared = "Y";
                    else
                        item1.MarineRevenueCleared = "N";
                    item1.CreatedBy = servicerequest.CreatedBy;
                    item1.CreatedDate = servicerequest.CreatedDate;
                    item1.ModifiedBy = servicerequest.ModifiedBy;
                    item1.ModifiedDate = servicerequest.ModifiedDate;
                    item1.RecordStatus = "A";

                    _unitOfWork.Repository<ServiceRequestSailing>().Insert(item1);

                }
            }
            if (serviceRequestShiftings.Count > 0)
            {
                foreach (var item2 in serviceRequestShiftings)
                {
                    item2.ServiceRequestID = servicerequest.ServiceRequestID;
                    item2.CreatedBy = servicerequest.CreatedBy;
                    item2.CreatedDate = servicerequest.CreatedDate;
                    item2.ModifiedBy = servicerequest.ModifiedBy;
                    item2.ModifiedDate = servicerequest.ModifiedDate;
                    item2.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestShifting>().Insert(item2);

                }
            }
            if (serviceRequestWarpings.Count > 0)
            {
                foreach (var item3 in serviceRequestWarpings)
                {
                    item3.ServiceRequestID = servicerequest.ServiceRequestID;
                    item3.CreatedBy = servicerequest.CreatedBy;
                    item3.CreatedDate = servicerequest.CreatedDate;
                    item3.ModifiedBy = servicerequest.ModifiedBy;
                    item3.ModifiedDate = servicerequest.ModifiedDate;
                    item3.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestWarping>().Insert(item3);

                }
            }

            _unitOfWork.ExecuteSqlCommand("Update dbo.VesselCallMovement set FromPositionQuayCode = @p0, FromPositionBerthCode = @p0, FromPositionBollardCode = @p0, ToPositionPortCode = @p0, ToPositionQuayCode = @p0, ToPositionBerthCode = @p0, ToPositionBollardCode = @p0, MovementStatus = @p1, RecordStatus = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where ServiceRequestID = @p5", null, MovementStatus.PENDING, RecordStatus.InActive, servicerequest.ModifiedBy, servicerequest.ModifiedDate, servicerequest.ServiceRequestID);

            servicerequest.ObjectState = ObjectState.Modified;
            servicerequest.RecordStatus = "I";
            _unitOfWork.Repository<ServiceRequest>().Update(servicerequest);
            _unitOfWork.SaveChanges();

        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            servicerequest.WorkflowInstanceId = workFlowInstanceId;

            servicerequest.IsFinal = null; //It is Computed column, should not get any value before saving
            _unitOfWork.Repository<ServiceRequest>().Update(servicerequest);
            _unitOfWork.SaveChanges();
        }

        public void InsertMovement()
        {

            if (servicerequest.RecordStatus == "A")
            {
                var AutoConfiguredSlots = serviceRequestRepository.GetAutoConfiguredSlots(servicerequest.MovementDateTime, servicerequest.ArrivalNotification.PortCode);
                //added mahesh K NIT_IPMS04 and NIT_IPMS03 combination
                string[] slotstperiod = servicerequest.SlotPeriod.Split('-');
                if (slotstperiod[1] == SlotPeriodTimeStatus.slotperiod1)
                {
                    servicerequest.MovementDateTime= servicerequest.MovementDateTime.AddMinutes(Convert.ToInt16(AutoConfiguredSlots.Duration));
                }
                //end
                var totalSlotsAvailable = serviceRequestRepository.GetTotalSlotsAvailable(servicerequest.MovementDateTime, servicerequest.MovementSlot, servicerequest.ArrivalNotification.PortCode);

                string servID = Convert.ToString(servicerequest.ServiceRequestID, CultureInfo.InvariantCulture);
                #region Adding Record in VesselCallmovement table, if the request is confirmed
                int _conformdcnt = -1;
                _conformdcnt = GetRequestStatus(entityCode, servID);

                if (_conformdcnt > 0)
                {

                    VesselCallMovement objVesselCallMovement = new VesselCallMovement();

                    if (servicerequest.MovementType == MovementTypes.ARRIVAL)
                    {

                        var vcnatvcm = serviceRequestRepository.GetVesselCallMovementAtVCN(servicerequest.VCN, servicerequest.ArrivalNotification.PortCode);


                        vcnatvcm.ServiceRequestID = servicerequest.ServiceRequestID;
                        vcnatvcm.MovementType = servicerequest.MovementType;
                        vcnatvcm.MovementDateTime = Convert.ToDateTime(servicerequest.MovementDateTime);
                        vcnatvcm.RecordStatus = servicerequest.RecordStatus;

                        //-- Added by sandeep on 24-02-2015
                        DateTime MovementStarttime = servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                        vcnatvcm.Slot = GetSlotPeriodBySlotdate(MovementStarttime, servicerequest.ArrivalNotification.PortCode);

                        vcnatvcm.SlotDate = MovementStarttime;
                        //-- end

                        if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                        {
                            if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                            {
                                vcnatvcm.SlotStatus = AutomatedSlotStatus.Pending;
                            }
                        }

                        _unitOfWork.Repository<VesselCallMovement>().Update(vcnatvcm);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        var vcallm = serviceRequestRepository.GetVesselCallMovement(servicerequest.ServiceRequestID);

                        if (vcallm.Count == 0)
                        {
                            objVesselCallMovement.VCN = servicerequest.VCN;
                            objVesselCallMovement.ServiceRequestID = servicerequest.ServiceRequestID;
                            objVesselCallMovement.MovementType = servicerequest.MovementType;
                            objVesselCallMovement.MovementDateTime = servicerequest.MovementDateTime;
                            objVesselCallMovement.SlotDate = servicerequest.MovementDateTime;
                            objVesselCallMovement.FromPositionPortCode = servicerequest.ArrivalNotification.PortCode;
                            var vcalldtls = vesselCallRepository.VesselCallDetails(servicerequest.VCN);


                            if (servicerequest.MovementType == MovementTypes.SHIFTING)
                            {
                                objVesselCallMovement.SlotStatus = MovementStatus.PEND;
                                objVesselCallMovement.MovementStatus = MovementStatus.PENDING;
                                objVesselCallMovement.ETB = servicerequest.MovementDateTime;
                                objVesselCallMovement.ETUB = vcalldtls.ETUB;


                            }
                            if (servicerequest.MovementType == MovementTypes.WARPING)
                            {
                                objVesselCallMovement.ETB = servicerequest.MovementDateTime;
                                objVesselCallMovement.ETUB = vcalldtls.ETUB;
                                objVesselCallMovement.MovementStatus = MovementStatus.CONFIRMED;
                                DateTime MovementStarttime = servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                                objVesselCallMovement.Slot = GetSlotPeriodBySlotdate(MovementStarttime, servicerequest.ArrivalNotification.PortCode);
                                //-- Added by sandeep on 24-02-2015                               
                                objVesselCallMovement.SlotDate = MovementStarttime;
                                //-- end
                                objVesselCallMovement.SlotStatus = AutomatedSlotStatus.Planned;
                            }
                            if (servicerequest.MovementType == MovementTypes.SAILING)
                            {
                                objVesselCallMovement.ETB = servicerequest.MovementDateTime;
                                objVesselCallMovement.ETUB = servicerequest.MovementDateTime;
                                objVesselCallMovement.MovementStatus = MovementStatus.CONFIRMED;
                                DateTime MovementStarttime = servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                                objVesselCallMovement.Slot = GetSlotPeriodBySlotdate(MovementStarttime, servicerequest.ArrivalNotification.PortCode);
                                //-- Added by sandeep on 24-02-2015                               
                                objVesselCallMovement.SlotDate = MovementStarttime;
                                //-- end
                                objVesselCallMovement.SlotStatus = AutomatedSlotStatus.Planned;
                            }

                            if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                            {
                                if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                                {
                                    objVesselCallMovement.SlotStatus = AutomatedSlotStatus.Pending;
                                }
                            }

                            objVesselCallMovement.RecordStatus = servicerequest.RecordStatus;
                            objVesselCallMovement.CreatedBy = servicerequest.CreatedBy;
                            objVesselCallMovement.CreatedDate = servicerequest.CreatedDate;
                            objVesselCallMovement.ModifiedBy = servicerequest.ModifiedBy;
                            objVesselCallMovement.ModifiedDate = servicerequest.ModifiedDate;
                            objVesselCallMovement.CreatedBy = servicerequest.CreatedBy;
                            _unitOfWork.Repository<VesselCallMovement>().Insert(objVesselCallMovement);
                            _unitOfWork.SaveChanges();
                        }
                        else
                        {
                            if (servicerequest.MovementType == MovementTypes.SHIFTING)
                            {
                                string slotStatus = AutomatedSlotStatus.Planned; // Added by sandeep on 09-07-2015                                
                                //DateTime? slotDate = servicerequest.MovementDateTime;
                                DateTime MovementStarttime = servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                                objVesselCallMovement.Slot = GetSlotPeriodBySlotdate(MovementStarttime, servicerequest.ArrivalNotification.PortCode);
                                objVesselCallMovement.SlotDate = MovementStarttime;
                                _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus =  @p0, SlotDate = @p1 where ServiceRequestID = @p2", slotStatus, MovementStarttime, servicerequest.ServiceRequestID);
                            }
                        }

                    }
                }
                #endregion
            }
        }

        public void UpdateVO()
        {
            var usertype = _unitOfWork.Repository<User>().Find(servicerequest.CreatedBy);
            if (usertype.UserType == UserType.Agent)
            {
                vo.UserType = UserType.Agent;
                vo.UserTypeId = servicerequest.ArrivalNotification.VesselCalls.FirstOrDefault<VesselCall>().RecentAgentID;
            }
            else if (usertype.UserType == UserType.TerminalOperator)
            {
                vo.UserType = UserType.TerminalOperator;
                vo.UserTypeId = Convert.ToInt32(servicerequest.ArrivalNotification.TerminalOperatorID, CultureInfo.InvariantCulture);
            }
            else
            {
                vo.UserType = UserType.Employee;
                vo.UserTypeId = servicerequest.CreatedBy;
            }
        }
        public string GetSlotPeriodBySlotdate(DateTime slotdatetime, string port)
        {
            int hours = slotdatetime.Hour;
            string slotPeriod = string.Empty;
            double totalminutes = slotdatetime.TimeOfDay.TotalMinutes;

            if (slotdatetime != DateTime.MinValue)
            {
                foreach (ResourceSlotVO slot in suppServiceResourceAllocRepository.GetSlotConfiguration(slotdatetime, port))
                {
                    string[] period = slot.SlotPeriod.Split('-');

                    DateTime sttime = Convert.ToDateTime(period[0], CultureInfo.InvariantCulture);

                    DateTime edtime = Convert.ToDateTime(period[1], CultureInfo.InvariantCulture);

                    double starttime = sttime.TimeOfDay.TotalMinutes;

                    double endtime = edtime.TimeOfDay.TotalMinutes;

                    if (starttime > endtime)
                    {
                        if (totalminutes <= endtime && starttime >= totalminutes)
                        {
                            slotPeriod = slot.SlotPeriod;
                            break;
                        }
                        if (totalminutes >= endtime && starttime <= totalminutes)
                        {
                            slotPeriod = slot.SlotPeriod;
                            break;
                        }
                    }

                    if (totalminutes >= starttime && totalminutes < endtime)
                    {
                        slotPeriod = slot.SlotPeriod;
                        break;
                    }

                }
            }
            return slotPeriod;
        }

        public void AutoSlotBlocking()
        {

            var autoSloBlocking = serviceRequestRepository.GetBlockedSlots(servicerequest.ArrivalNotification.PortCode);

            foreach (var slot in autoSloBlocking)
            {
                DateTime ab1 = Convert.ToDateTime(slot.FromDate);
                DateTime ab2 = Convert.ToDateTime(slot.ToDate);

                string fromDate = ab1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string toDate = ab2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                ab1 = ab1.Date.AddHours(0).AddMinutes(slot.StartTime).AddSeconds(0);

                if (slot.StartTime > slot.EndTime)
                {
                    ab2 = ab2.AddDays(1);
                }

                ab2 = ab2.Date.AddHours(0).AddMinutes(slot.EndTime).AddSeconds(0);

                if (ab1 < servicerequest.MovementDateTime && servicerequest.MovementDateTime < ab2)
                {
                    throw new BusinessExceptions("Slots from " + fromDate + ' ' + slot.SlotFrom + " to " + toDate + ' ' + slot.SlotTo + " are blocked due to " + slot.ReasonName);
                }
            }

        }


        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    Create();
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "UPDT":
                    Update();
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "WFSA":
                    AutoSlotBlocking();
                    UpdateVO();
                    break;
                case "WFCA":
                    Cancel();
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "WFRE":
                    Reject();
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "WFCO":
                    AutoSlotBlocking();
                    InsertMovement();
                    UpdateVO();
                    break;
                case "WFCC":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "WSSA":
                    Cancel();
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    break;
                case "WSRE":
                    AutoSlotBlocking();
                    InsertMovement();
                    UpdateVO();
                    break;
                case "EXPI":
                    break;
                case "CLOS":
                    break;
            }
        }

        public CompanyVO GetCompanyDetails(int step)
        {
            return vo;
        }

    }
}


