using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Security.Cryptography;
using System.IO;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System.Data.Entity.SqlServer;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;
using IPMS.Core.Repository.Exceptions;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
            ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class AutomatedSlottingService : ServiceBase, IAutomatedSlottingService
    {
        private INotificationPublisher _notificationpublisher;
        private IEntityRepository _entity;
        private VesselCallMovementVO _NotificationVesselCallMovementVO;
        private IUserRepository _userRepository;

        private ISubCategoryRepository _subCategoryRepository;
        private IAutomatedSlottingRepository _automatedSlottingRespository;
        private IServiceRequestRepository _serviceRequestRepository;

        public AutomatedSlottingService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);
            _automatedSlottingRespository = new AutomatedSlottingRepository(_unitOfWork);

            _userRepository = new UserRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
        }

        public AutomatedSlottingService(IUnitOfWork unitofWork)
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);
            _automatedSlottingRespository = new AutomatedSlottingRepository(_unitOfWork);

            _userRepository = new UserRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(_unitOfWork);
        }

        /// <summary>
        /// Get  movement types 
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetMovementTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _subCategoryRepository.VesselMovementTypes().MapToDto();
            });
        }

        /// <summary>
        /// get unaplanned vesseles details
        /// </summary>
        /// <returns></returns>
        public List<VesselCallMovementVO> GetUnPlannedVesselDet(DateTime slotDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _user = _userRepository.GetUserByID(_UserId);
                return _automatedSlottingRespository.GetUnPlannedVesselDetails(slotDate, _PortCode, _user.UserID, _user.UserType);
            });
        }

        /// <summary>
        /// updateing planned slot details
        /// </summary>
        /// <param name="slotDetails"></param>
        /// <returns></returns>
        public int UpdateVesselSlotDetails(List<VesselCallMovementVO> slotDetails)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                var status = 0;
                string slotStatus = string.Empty;
                string slot = string.Empty;
                int vesselCallmovementID = default(int);                

                string temp = slotDetails[0].Slot;
                int count = 0;
                bool updateextendedslots = true;
                //int countV = 0;

                //var automatedSlotConfiguration = _automatedSlottingRespository.GetAutomatedConfigurationDetails(DateTime.Now, _PortCode);
                //int totalslots = automatedSlotConfiguration.NoofSlots;               
                //List<int> vesselCallIDs = new List<int>();
                foreach (var slotdtl in slotDetails)
                {
                    if (!String.IsNullOrEmpty(temp) && temp == slotdtl.Slot)
                    {
                        count = count + 1;
                    }
                    else
                    {
                        temp = slotdtl.Slot;
                        count = 1;
                    }

                    if (slotdtl.IsChanged)
                    {
                        slot = slotdtl.Slot;
                        double period = default(double);

                        if (!string.IsNullOrEmpty(slot))
                        {
                            string[] slotperiod = slotdtl.Slot.Split('-');      
                            
                            DateTime sttime = Convert.ToDateTime(slotperiod[0], CultureInfo.InvariantCulture);

                            period = sttime.TimeOfDay.TotalMinutes + 1;                            
                                               
                        }
                        else
                        {
                            period = 0;
                        }

                        if (!string.IsNullOrEmpty(slotdtl.ReasonCode))
                        {
                            var _user = _userRepository.GetUserByID(_UserId);
                            try
                            {
                                if (_user.UserID == 1)
                                {
                                    throw new BusinessExceptions("Internal Server error occured. Please contact to administrator.");
                                }

                                else
                                {

                                    SlotOverRidingReasons slotReason = new SlotOverRidingReasons();
                                    //var _user = _userRepository.GetUserByID(_UserId);
                                    slotReason.ObjectState = ObjectState.Added;
                                    slotReason.CreatedDate = DateTime.Now;
                                    slotReason.CreatedBy = _user.UserID;
                                    slotReason.ModifiedBy = _user.UserID;
                                    slotReason.ModifiedDate = DateTime.Now;
                                    slotReason.EnteredDateAndTime = DateTime.Now;
                                    slotReason.ReasonCode = slotdtl.ReasonCode;
                                    slotReason.VesselCallMovementID = slotdtl.VesselCallMovementID;
                                    // string PreviousOverriddenSlot = _automatedSlottingRespository.GetPreviousOverriddenSlot(slotdtl.VesselCallMovementID);
                                    SlotOverRidingReasonsVO details = _automatedSlottingRespository.GetPreviousOverriddenSlot(slotdtl.VesselCallMovementID);
                                    VesselCallMovementVO details1 = _automatedSlottingRespository.GetPreviousPlannedSlot(slotdtl.VesselCallMovementID);
                                    if (details == null)
                                    {
                                        slotReason.OverriddenSlot = slotdtl.OverriddenSlot;
                                        //slotReason.OverriddenSlotDate = slotReason.OverriddenSlot == "Awaiting Slot" ? (DateTime?)null : slotReason.EnteredDateAndTime;
                                        slotReason.PreviousSlot = details1.PreviousSlotStatus == "PEND" ? "Awaiting Slot" : slotdtl.PreviousSlot;//details1.PreviousSlotDis;
                                        slotReason.PreviousSlotDate = slotReason.PreviousSlot == "Awaiting Slot" ? (DateTime?)null : details1.SlotDate;
                                    }
                                    else if (details.PreviousSlotDis == slotdtl.PreviousSlot)
                                    {
                                        slotReason.OverriddenSlot = slotdtl.OverriddenSlot;
                                        //slotReason.OverriddenSlotDate = slotReason.OverriddenSlot == "Awaiting Slot" ? (DateTime?)null : slotReason.EnteredDateAndTime;
                                        slotReason.PreviousSlot = slotdtl.PreviousSlot;
                                        slotReason.PreviousSlotDate = details.PreviousSlotDate;
                                    }
                                    else
                                    {
                                        slotReason.OverriddenSlot = slotdtl.OverriddenSlot;
                                        //slotReason.OverriddenSlotDate = slotReason.OverriddenSlot == "Awaiting Slot" ? (DateTime?)null : slotReason.EnteredDateAndTime;
                                        slotReason.PreviousSlot = details.PreviousSlotDis;
                                        slotReason.PreviousSlotDate = details.PreviousSlotDate;


                                    }
                                    //if (PreviousOverriddenSlot == string.Empty)
                                    //{
                                    //    slotReason.OverriddenSlot = slotdtl.OverriddenSlot;
                                    //    slotReason.PreviousSlot = _automatedSlottingRespository.GetPreviousPlannedSlot(slotdtl.VesselCallMovementID);
                                    //}
                                    //else if (PreviousOverriddenSlot == slotdtl.PreviousSlot)
                                    //{
                                    //    slotReason.OverriddenSlot = slotdtl.OverriddenSlot;
                                    //    slotReason.PreviousSlot = slotdtl.PreviousSlot;
                                    //}
                                    //else
                                    //{
                                    //    slotReason.OverriddenSlot = slotdtl.OverriddenSlot;
                                    //    slotReason.PreviousSlot = PreviousOverriddenSlot;
                                    //}
                                    _unitOfWork.Repository<SlotOverRidingReasons>().Insert(slotReason);
                                    _unitOfWork.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new BusinessExceptions("Internal Server error occured. Please contact to administrator.");
                            }
                            
                        }
                        
                        slotdtl.SlotDate = Convert.ToDateTime(slotdtl.SlotDate, CultureInfo.InvariantCulture) == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(slotdtl.SlotDate, CultureInfo.InvariantCulture).Date.AddHours(0).AddMinutes(0).AddSeconds(0);
                        DateTime? slotDate = Convert.ToDateTime(slotdtl.SlotDate, CultureInfo.InvariantCulture) == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(slotdtl.SlotDate, CultureInfo.InvariantCulture).AddHours(0).AddMinutes(period).AddSeconds(0);
                        
                        slotStatus = slotdtl.SlotStatus;
                        vesselCallmovementID = slotdtl.VesselCallMovementID;

                        DateTime slotDate1 = Convert.ToDateTime(slotDate);

                        var automatedSlotConfiguration = _serviceRequestRepository.GetAutoConfiguredSlots(slotDate1, _PortCode);
                        int totalslots = automatedSlotConfiguration != null ? automatedSlotConfiguration.NoofSlots : 0;
                        int totalslots1 = automatedSlotConfiguration != null ? automatedSlotConfiguration.NoofSlots : 0;                        
                        //if (!string.IsNullOrEmpty(slotdtl.ExtendYn) && automatedSlotConfiguration != null)
                        // {

                        //    totalslots = slotdtl.ExtendYn == "Y" ? automatedSlotConfiguration.NoofSlots + automatedSlotConfiguration.ExtendableSlots : automatedSlotConfiguration.NoofSlots;
                        //}

                        //List<VesselCallMovementVO> totalSlotsAvailable = new List<VesselCallMovementVO>();                      
                        //if (!string.IsNullOrEmpty(slot))
                        //{
                        //    totalSlotsAvailable = _serviceRequestRepository.GetTotalSlotsAvailable(slotDate1, slotdtl.Slot, _PortCode);                                                  
                        //}

                        if (!string.IsNullOrEmpty(slotStatus))
                        {                            

                            if (slotStatus != AutomatedSlotStatus.Pending)
                            {
                                _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus =  @p0, SlotDate= @p1, Slot = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where VesselCallMovementID = @p5", slotStatus, slotDate, slot, _UserId, DateTime.Now, vesselCallmovementID);
                                //if (totalSlotsAvailable.Count > 0 && totalSlotsAvailable.Count >= totalslots)
                                //{
                                //    var vesselupdate = false;                                    
                                //    foreach (var vessel in totalSlotsAvailable)
                                //    {
                                //        if (vesselupdate == false)
                                //        {
                                //            if (slotdtl.VesselCallMovementID == vessel.VesselCallMovementID)
                                //            {
                                //                _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus =  @p0, SlotDate= @p1, Slot = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where VesselCallMovementID = @p5", slotStatus, slotDate, slot, _UserId, DateTime.Now, vesselCallmovementID);
                                //                vesselCallIDs.Add(slotdtl.VesselCallMovementID);
                                //                vesselupdate = true;
                                //            }
                                //            else
                                //            {
                                //                throw new BusinessExceptions("Cannot allot this slot as another resource has already utilized. Please refresh to view the updated Slot Plan.");
                                //            }
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                //    _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus =  @p0, SlotDate= @p1, Slot = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where VesselCallMovementID = @p5", slotStatus, slotDate, slot, _UserId, DateTime.Now, vesselCallmovementID);
                                //}                                

                                if (slotdtl.SlotStatus == AutomatedSlotStatus.Confirmed)
                                {
                                    if (count <= totalslots1)
                                    {
                                        _NotificationVesselCallMovementVO = slotdtl;
                                        var _user = _userRepository.GetUserById(_UserId);
                                        CompanyVO nextStepCompany = new CompanyVO();
                                        nextStepCompany.UserType = _user.UserType;
                                        nextStepCompany.UserTypeId = _user.UserTypeID;                                     
                                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.AutomatedSlotting).EntityID, _NotificationVesselCallMovementVO.VesselCallMovementID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Confirmed);
                                    }
                                    else
                                    {
                                        _NotificationVesselCallMovementVO = slotdtl;
                                        var _user = _userRepository.GetUserById(_UserId);
                                        CompanyVO nextStepCompany = new CompanyVO();
                                        nextStepCompany.UserType = _user.UserType;
                                        nextStepCompany.UserTypeId = _user.UserTypeID;                                      
                                       _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.AutomatedSlotting).EntityID, _NotificationVesselCallMovementVO.VesselCallMovementID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.ExtendSlot);
                                    }
                                }
                            }
                            else
                            {
                                _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus =  @p0, SlotDate = @p2, ModifiedBy = @p1, ModifiedDate = @p2 where VesselCallMovementID = @p3", slotStatus, _UserId, DateTime.Now, vesselCallmovementID); // Added by sandeep on 26-10-2015
                            }
                        }
                        if (updateextendedslots)
                        {
                            if (!string.IsNullOrEmpty(slotdtl.ExtendYn) && (!string.IsNullOrEmpty(slotdtl.FromPositionPortCode)))
                            {
                                if (slotdtl.ExtendedSlotDate != null)
                                {
                                    DateTime? extendedSlotDate = Convert.ToDateTime(slotdtl.ExtendedSlotDate, CultureInfo.InvariantCulture);

                                    _unitOfWork.ExecuteSqlCommand("delete from AutomatedSlotExtend where portcode='" + slotdtl.FromPositionPortCode + "' and slotdate='" + extendedSlotDate + "' ");
                                    _unitOfWork.ExecuteSqlCommand("insert into AutomatedSlotExtend values('" + slotdtl.FromPositionPortCode + "','" + extendedSlotDate + "', '" + slotdtl.ExtendYn + "') ");
                                    updateextendedslots = false;
                                }
                            }
                        }
                        
                        status = vesselCallmovementID;
                    }
                }
                return status;
            });
        }

        public bool UpdateSingleVesselSlotDetails(VesselCallMovementVO slotDetails)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                var status = false;
                string slotStatus = string.Empty;
                string slot = string.Empty;

                slot = slotDetails.Slot;
                double period = default(double);

                if (!string.IsNullOrEmpty(slot))
                {
                    string[] slotperiod = slotDetails.Slot.Split('-');
                    period = Convert.ToDouble(int.Parse(slotperiod[1], CultureInfo.InvariantCulture) - 1);
                }
                else
                {
                    period = 0;
                }

                DateTime? slotDate = Convert.ToDateTime(slotDetails.SlotDate, CultureInfo.InvariantCulture) == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(slotDetails.SlotDate, CultureInfo.InvariantCulture).AddHours(period).AddMinutes(59).AddSeconds(59);

                slotStatus = (slotDetails.SlotStatus == AutomatedSlotStatus.Overridden || slotDetails.SlotStatus == AutomatedSlotStatus.Planned) ? AutomatedSlotStatus.Confirmed : slotDetails.SlotStatus;

                if (!string.IsNullOrEmpty(slotStatus))
                {
                    if (slotStatus != AutomatedSlotStatus.Pending)
                    {
                        _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus =  @p0, SlotDate= @p1, Slot = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where VesselCallMovementID = @p5", slotStatus, slotDate, slot, _UserId, DateTime.Now, slotDetails.VesselCallMovementID);
                    }
                    else
                    {
                        _unitOfWork.ExecuteSqlCommand("update dbo.VesselCallMovement SET SlotStatus =  @p0, Slot = @p1 ,ModifiedBy = @p2, ModifiedDate = @p3 where VesselCallMovementID = @p4", slotStatus, slot, _UserId, DateTime.Now, slotDetails.VesselCallMovementID);
                    }
                    status = true;
                }

                return status;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slotDate"></param>
        /// <returns></returns>
        public List<VesselCallMovementVO> GetPlannedVesselDetails(DateTime slotDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var _user = _userRepository.GetUserByID(_UserId);
                return _automatedSlottingRespository.GetPlannedVesselDetails(slotDate, _PortCode, _user.UserID, _user.UserType);
            });
        }

        public AutomatedSlotConfigurationVO GetAutomatedConfigurationDetails(DateTime slotDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedSlottingRespository.GetAutomatedConfigurationDetails(slotDate, _PortCode);
            });
        }

        public AutomatedSlotConfigurationVO GetExtendableYesNo(DateTime slotDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedSlottingRespository.GetExtendableYesNo(_PortCode, slotDate);
            });
        }

        public VesselCallMovementVO GetVesselCallMovementVO(string strVesselCallMovementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedSlottingRespository.GetVesselCallMovementVORep(strVesselCallMovementId);
            });
        }        

        public bool GetPrivilegesByUserIdAndEntityCode(string entityCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var user = _userRepository.GetUserByID(_UserId);

                if (user.UserType == UserType.Agent)
                {
                    return false;
                }
                else
                {
                    return _automatedSlottingRespository.GetPrivilegesByUserIdAndEntityCode(_UserId, entityCode);
                }
            });
        }

        public List<string> GetActiveSlots()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedSlottingRespository.GetActiveSlots(_PortCode);
            });
        }

        public List<AutomatedSlotBlockingVO> GetBlockedSlots(DateTime slotDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _automatedSlottingRespository.GetBlockedSlots(slotDate, _PortCode);
            });
        }

        #region GetReasonTypes
        /// <summary>
        /// To Get Document types 
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetReasonTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var docTypes = from s in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable()
                               where s.SupCatCode == "ORSR"
                               select s;
                return docTypes.ToList();
            });
        }
        #endregion

     
    }
}
