using Core.Repository;
using IPMS.Core.Repository.Exceptions;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AutomatedSlotBlockingService : ServiceBase, IAutomatedSlotBlockingService
    {       
        private ISubCategoryRepository _subcategoryRepository;
        private IServiceRequestRepository _serviceRequestRepository;
        private IAutomatedSlotBlockingRepository _automatedSlotBlockingRepository;

        public AutomatedSlotBlockingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
            _automatedSlotBlockingRepository = new AutomatedSlotBlockingRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public AutomatedSlotBlockingService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
           _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
           _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
            _automatedSlotBlockingRepository = new AutomatedSlotBlockingRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
        }      
        public List<AutomatedSlotBlockingVO> GetAutomatedSlotBlockings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedSlotBlockingRepository.GetAutomatedSlotBlockings(_PortCode);
            });
        }

        /// <summary>
        /// To get reference data for Reasons
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AutomatedSlotBlockingVO GetAutomatedReferenceData()
        {
            AutomatedSlotBlockingVO VO = new AutomatedSlotBlockingVO();
            VO.Reasons = _subcategoryRepository.ReasonTypes();
            VO.Slots = _serviceRequestRepository.GetSlotDetails(_PortCode);

            return VO;
        }

        /// <summary>
        /// To Add  Automated Slot Blocking 
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        public AutomatedSlotBlockingVO SaveAutomatedSlotBlocking(AutomatedSlotBlockingVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                data.CreatedBy = _UserId;
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                data.PortCode = _PortCode;

                if (data.RecordStatus == RecordStatus.Active)
                {
                    var dtFromDate = Convert.ToDateTime(data.FromDate, CultureInfo.InvariantCulture);
                    var dtFromTo = Convert.ToDateTime(data.ToDate, CultureInfo.InvariantCulture);

                    string[] slotstartperiod = data.SlotFrom.Split('-');

                    DateTime slotSttime = Convert.ToDateTime(slotstartperiod[0], CultureInfo.InvariantCulture);

                    double slotStartMinutes = slotSttime.TimeOfDay.TotalMinutes;

                    DateTime slotStartDate = dtFromDate.Date.AddHours(0).AddMinutes(slotStartMinutes).AddSeconds(0);



                    string[] slottoperiod = data.SlotTo.Split('-');

                    DateTime slotEdtime = Convert.ToDateTime(slottoperiod[1], CultureInfo.InvariantCulture);

                    double slotEndMinutes = slotEdtime.TimeOfDay.TotalMinutes;

                    DateTime slotEndtDate = dtFromTo;


                    if (slotStartMinutes > slotEndMinutes)
                    {
                        slotEndtDate = slotEndtDate.AddDays(1);
                    }

                    slotEndtDate = slotEndtDate.Date.AddHours(0).AddMinutes(slotEndMinutes).AddSeconds(0);

                    var blockedSlots = _serviceRequestRepository.GetSlotPeriodFromDateToDate(data, _PortCode);


                    var vesselcallMoments = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(a => a.FromPositionPortCode == _PortCode)
                                             join sr in _unitOfWork.Repository<ServiceRequest>().Queryable() on vcm.ServiceRequestID equals sr.ServiceRequestID
                                             where vcm.RecordStatus == RecordStatus.Active && sr.RecordStatus == RecordStatus.Active && vcm.Slot != null
                                               && (slotStartDate != null ? (vcm.SlotDate) >= slotStartDate : true) &&
                                                  (slotEndtDate != null ? (vcm.SlotDate) <= slotEndtDate : true)
                                             select vcm).ToList();

                    foreach (var blockSlot in blockedSlots)
                    {
                        foreach (var item in vesselcallMoments)
                        {

                            if (Convert.ToDateTime(item.SlotDate, CultureInfo.InvariantCulture) > blockSlot.StartDate && Convert.ToDateTime(item.SlotDate, CultureInfo.InvariantCulture) < blockSlot.EndDate)
                            {
                                throw new BusinessExceptions("You cannot block this Slot as there is a request already raised against this Slot");
                            }                            

                        }
                    }
                }


                AutomatedSlotBlocking autoblock = new AutomatedSlotBlocking();
                autoblock = AutomatedSlotBlockingMapExtension.MapToEntity(data);

                autoblock.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<AutomatedSlotBlocking>().Insert(autoblock);        

                _unitOfWork.SaveChanges();

                return data;
            });
        }

        /// <summary>
        /// To Modify Automated Slot Blocking 
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        public AutomatedSlotBlockingVO ModifyAutomatedSlotBlocking(AutomatedSlotBlockingVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                data.CreatedBy = _UserId;
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                data.PortCode = _PortCode;


                if (data.RecordStatus == RecordStatus.Active)
                {
                    var dtFromDate = Convert.ToDateTime(data.FromDate, CultureInfo.InvariantCulture);
                    var dtFromTo = Convert.ToDateTime(data.ToDate, CultureInfo.InvariantCulture);

                    string[] slotstartperiod = data.SlotFrom.Split('-');

                    DateTime slotSttime = Convert.ToDateTime(slotstartperiod[0], CultureInfo.InvariantCulture);

                    double slotStartMinutes = slotSttime.TimeOfDay.TotalMinutes;

                    DateTime slotStartDate = dtFromDate.Date.AddHours(0).AddMinutes(slotStartMinutes).AddSeconds(0);



                    string[] slottoperiod = data.SlotTo.Split('-');

                    DateTime slotEdtime = Convert.ToDateTime(slottoperiod[1], CultureInfo.InvariantCulture);

                    double slotEndMinutes = slotEdtime.TimeOfDay.TotalMinutes;

                    DateTime slotEndtDate = dtFromTo;

                    if (slotStartMinutes > slotEndMinutes)
                    {
                        slotEndtDate = slotEndtDate.AddDays(1);
                    }

                    slotEndtDate = slotEndtDate.Date.AddHours(0).AddMinutes(slotEndMinutes).AddSeconds(0);


                    var blockedSlots = _serviceRequestRepository.GetSlotPeriodFromDateToDate(data, _PortCode);


                    var vesselcallMoments = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Queryable().Where(a => a.FromPositionPortCode == _PortCode)
                                             join sr in _unitOfWork.Repository<ServiceRequest>().Queryable() on vcm.ServiceRequestID equals sr.ServiceRequestID
                                             where vcm.RecordStatus == RecordStatus.Active && sr.RecordStatus == RecordStatus.Active && vcm.Slot != null
                                               && (slotStartDate != null ? (vcm.SlotDate) >= slotStartDate : true) &&
                                                  (slotEndtDate != null ? (vcm.SlotDate) <= slotEndtDate : true)
                                             select vcm).ToList();

                    foreach (var blockSlot in blockedSlots)
                    {
                        foreach (var item in vesselcallMoments)
                        {
                            if (Convert.ToDateTime(item.SlotDate, CultureInfo.InvariantCulture) > blockSlot.StartDate && Convert.ToDateTime(item.SlotDate, CultureInfo.InvariantCulture) < blockSlot.EndDate)
                            {
                                throw new BusinessExceptions("You cannot block this Slot as there is a request already raised against this Slot");
                            }                            

                        }
                    }
                }

                AutomatedSlotBlocking autoblock = new AutomatedSlotBlocking();
                autoblock = AutomatedSlotBlockingMapExtension.MapToEntity(data);
                autoblock.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<AutomatedSlotBlocking>().Update(autoblock);

                _unitOfWork.SaveChanges();

                return data;
            });
        }

        
    }
}