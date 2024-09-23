using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using IPMS.Domain;
using IPMS.Services.WorkFlow;
using System.Configuration;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SuppServiceResourceAllocService : ServiceBase, ISuppServiceResourceAllocService
    {
        ISupplymentaryServiceRepository _suppServiceRequestRepository = null;
        ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository = null;
        private IUserRepository _userRepository;
        // private IAccountRepository _accountRepository;
        private INotificationPublisher _notificationpublisher;
        private IEntityRepository _entity;

        public SuppServiceResourceAllocService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _suppServiceRequestRepository = new SupplymentaryServiceRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            // _accountRepository = new AccountRepository(_unitOfWork);
        }

        public SuppServiceResourceAllocService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _suppServiceRequestRepository = new SupplymentaryServiceRepository(_unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _entity = new EntityRepository(_unitOfWork);
            // _accountRepository = new AccountRepository(_unitOfWork);
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get all approved water service details
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<SuppServiceRequestVO> GetApprovedWaterService(string vcn, string date)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppServiceRequestRepository.GetApprovedWaterService(vcn, date);
            });
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get user details based on ServiceType
        /// </summary>
        /// <param name="ResourceSlot"></param>
        /// <returns></returns>
        public List<IdNameVO> GetSearchResource(ResourceSlotVO ResourceSlot)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<ResourceSlotVO> lstslot = _suppServiceResourceAllocRepository.GetSlotConfiguration(DateTime.Now, _PortCode);

                int _shiftID = lstslot.Find(s => s.SlotNumber == ResourceSlot.SlotNumber).ShiftID;
                ResourceSlot.ShiftID = _shiftID;

                return _suppServiceResourceAllocRepository.GetSearchResource(ResourceSlot, _PortCode);
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get list of water resources based on date adn port code
        /// </summary>
        /// <param name="date"></param>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<ResourceAllocationSlotVO> GetSupplementaryResourceAllocationByDate(DateTime date, string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                //var roles = _accountRepository.GetUserRole(_UserId);

                //var FloatingCraneRoleID = roles.Find(f => f.RoleID == Convert.ToInt32(ConfigurationManager.AppSettings["FloatingCraneRoleID"]));
                //var WaterManRoleID = roles.Find(w => w.RoleID == Convert.ToInt32(ConfigurationManager.AppSettings["WaterManRoleID"]));

                //if (WaterManRoleID != null)
                //{
                //    return _suppServiceResourceAllocRepository.GetSupplementaryResourceAllocationByDate(date, _PortCode, vcn, ServiceTypeCode.WaterService);
                //}
                //else if (FloatingCraneRoleID != null)
                //{
                //    return _suppServiceResourceAllocRepository.GetSupplementaryResourceAllocationByDate(date, _PortCode, vcn, ServiceTypeCode.FloatingCrane);
                //}
                //else
                //{
                //    return null;
                //}
                var role = _unitOfWork.Repository<Role>().Queryable().ToList();
                int FloatingCraneRoleID = role.Find(r => r.RoleCode == Roles.FloatingCraneMaster).RoleID; //Convert.ToInt32(ConfigurationManager.AppSettings["FloatingCraneRoleID"]);
                int WaterManRoleID = role.Find(r => r.RoleCode == Roles.WaterMan).RoleID; // Convert.ToInt32(ConfigurationManager.AppSettings["WaterManRoleID"]);

                var servicetype = _suppServiceResourceAllocRepository.GetRoleByUser(_UserId, FloatingCraneRoleID, WaterManRoleID);

                if (servicetype != null)
                {
                    return _suppServiceResourceAllocRepository.GetSupplementaryResourceAllocationByDate(date, _PortCode, vcn, servicetype);
                }
                else
                {
                    return new List<ResourceAllocationSlotVO>();
                }

            });
        }

        ///// <summary>
        ///// Author  : Sandeep Appana
        ///// Date    : 22nd Sep 2014
        ///// Purpose : To get list of resources based on VCN, date and portcode
        ///// </summary>
        ///// <param name="vcn"></param>
        ///// <param name="date"></param>
        ///// <returns></returns>
        //public List<ResourceAllocationVO> GetSupplementaryResourceAllocationByVCNAndDate(string vcn, string date)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        return _suppServiceResourceAllocRepository.GetSupplementaryResourceAllocationByVCNAndDate(vcn, date, _PortCode);
        //    });
        //}

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 22nd Sep 2014
        /// Purpose : To Post data into the service
        /// </summary>
        /// <param name="ResourceAllocationSlotVOs"></param>
        /// <returns></returns>
        public List<ResourceAllocationSlotVO> PostSupplementaryResourceAllocation(List<ResourceAllocationSlotVO> ResourceAllocationSlotVOs)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                List<ResourceSlotVO> lstResourceSlot = _suppServiceResourceAllocRepository.GetSlotConfiguration(DateTime.Now, _PortCode);
                foreach (var Slot in ResourceAllocationSlotVOs)
                {
                    if (Slot.IsConfirm == "true")
                    {
                        bool status = false;
                        ResourceSlotVO slotVO = Slot.ResourceSlots.Find(s => s.ResourceName != null);

                        //if (Convert.ToInt32(Slot.AllocSlot) != slotVO.SlotNumber)

                        //if (slotVO.TaskStatus == ResourceAllcationWorkFlowStatus.Scheduled)
                        //{

                        ResourceSlotVO slotvo = lstResourceSlot.Find(a => a.SlotNumber == slotVO.SlotNumber);
                        //if (slotVO.IsChanged)
                        //{
                        //ResourceAllocation res = (from ra in _unitOfWork.Repository<ResourceAllocation>().Query().Select().Where(ra => ra.ServiceReferenceID == Slot.ServiceReferenceID) select ra).First();
                        ResourceAllocation res = new ResourceAllocation();

                        res.ResourceAllocationID = Slot.ResourceAllocationID;
                        res.ServiceReferenceType = ServiceReferenceType.SupplementoryService;
                        res.ServiceReferenceID = Slot.ServiceReferenceID;
                        res.OperationType = slotVO.ServiceTypeCode;
                        res.ResourceID = slotVO.ResourceID != 0 ? slotVO.ResourceID : null;
                        res.CreatedBy = Slot.CreatedBy;
                        res.CreatedDate = DateTime.Parse(Slot.CreatedDate, CultureInfo.InvariantCulture);
                        res.ModifiedBy = _UserId;
                        res.ModifiedDate = DateTime.Now;
                        //res.AllocSlot = slotVO.SlotNumber.ToString();
                        //res.AllocSlot = slotVO.SlotPeriod;
                        res.RecordStatus = "A";
                        //res.AllocationDate = Slot.AllocationDate != "" ? DateTime.Parse(Slot.AllocationDate) : default(DateTime?);
                        if (slotVO.TaskStatus == ResourceAllcationWorkFlowStatus.Scheduled || slotVO.TaskStatus == ResourceAllcationWorkFlowStatus.Overridden)
                        {
                            if (slotVO.ResourceID != 0)
                            {
                                res.TaskStatus = ResourceAllcationWorkFlowStatus.Confirmed;
                            }
                            else
                            {
                                res.TaskStatus = ResourceAllcationWorkFlowStatus.Pending;
                            }
                            res.StartTime = default(DateTime?);
                            res.EndTime = default(DateTime?);
                            res.AcknowledgeDate = default(DateTime?);
                            status = true;
                        }
                        else if (slotVO.TaskStatus == ResourceAllcationWorkFlowStatus.Pending)
                        {
                            res.TaskStatus = ResourceAllcationWorkFlowStatus.Pending;
                            res.StartTime = default(DateTime?);
                            res.EndTime = default(DateTime?);
                            res.AcknowledgeDate = default(DateTime?);
                            status = true;
                        }
                        else
                        {
                            res.TaskStatus = slotVO.TaskStatus;
                            res.StartTime = Slot.StartTime != "" ? DateTime.Parse(Slot.StartTime, CultureInfo.InvariantCulture) : default(DateTime?);
                            res.EndTime = Slot.EndTime != "" ? DateTime.Parse(Slot.EndTime, CultureInfo.InvariantCulture) : default(DateTime?);
                            res.AcknowledgeDate = Slot.AcknowledgeDate != null ? Convert.ToDateTime(Slot.AcknowledgeDate, CultureInfo.InvariantCulture) : default(DateTime?);
                        }

                        if (status)
                        {
                            res.AllocationDate = slotvo.AllocationDate;
                            res.AllocSlot = slotvo.SlotPeriod;
                            res.ObjectState = ObjectState.Modified;

                            _unitOfWork.Repository<ResourceAllocation>().Update(res);


                            #region Sending Email for SAMSA Stop / Arrest
                            var _user = _userRepository.GetUserById(_UserId);
                            CompanyVO nextStepCompany = new CompanyVO();
                            nextStepCompany.UserType = _user.UserType;
                            nextStepCompany.UserTypeId = _user.UserTypeID;
                            int userID = Convert.ToInt32(res.ResourceID, CultureInfo.InvariantCulture);

                            // Send Notifications when Vessel Arrested
                            if (Slot.TaskStatus == "SCHD")
                            {
                                if (res.ResourceID > 0)
                                {
                                    var _resourceAssignedCode = WFStatus.ResourceAssigned;
                                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                                }
                            }
                            else if (Slot.TaskStatus == "PNDG")
                            {
                                if (Slot.ResourceID > 0)
                                {
                                    var _resourceAssignedCode = WFStatus.ResourceRemoved;
                                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                                }
                            }
                            else if (slotVO.TaskStatus == "OVRD")
                            {
                                if (Slot.ResourceID == 0)
                                {
                                    var _resourceAssignedCode = WFStatus.ResourceAssigned;
                                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                                }
                                else if (Slot.ResourceID > 0 && Slot.ResourceID == res.ResourceID)
                                {
                                    var _resourceAssignedCode = WFStatus.ResourceUpdated;
                                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                                }
                                else if (Slot.ResourceID > 0 && Slot.ResourceID != res.ResourceID)
                                {
                                    var _resourceAssignedCode = WFStatus.ResourceAssigned;
                                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);

                                    _resourceAssignedCode = WFStatus.ResourceRemoved;
                                    _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                                }
                            }

                            #endregion
                        }

                        //}
                        //}
                    }
                }

                _unitOfWork.SaveChanges();

                return ResourceAllocationSlotVOs;
            });
        }

        public List<ResourceSlotVO> GetSlotConfiguration(DateTime date)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppServiceResourceAllocRepository.GetSlotConfiguration(date, _PortCode);
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 29th Dec 2014
        /// Purpose : To get PortName by PortCode
        /// </summary>
        /// <param name="portcode"></param>
        /// <returns></returns>
        public string GetPortNameByPortCode()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppServiceResourceAllocRepository.GetPortNameByPortCode(_PortCode);
            });
        }

        public ResourceAllocationVO GetResourceAllocationByID(int resourceAllocationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppServiceResourceAllocRepository.GetResourceAllocationByID(resourceAllocationId);
            });
        }

        public List<String> GetVCNDetails(DateTime date)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var role = _unitOfWork.Repository<Role>().Queryable().ToList();
                int FloatingCraneRoleID = role.Find(r => r.RoleCode == Roles.FloatingCraneMaster).RoleID; //Convert.ToInt32(ConfigurationManager.AppSettings["FloatingCraneRoleID"]);
                int WaterManRoleID = role.Find(r => r.RoleCode == Roles.WaterMan).RoleID; // Convert.ToInt32(ConfigurationManager.AppSettings["WaterManRoleID"]);

                var servicetype = _suppServiceResourceAllocRepository.GetRoleByUser(_UserId, FloatingCraneRoleID, WaterManRoleID);

                if (servicetype != null)
                {
                    return _suppServiceResourceAllocRepository.GetVCNDetails(_PortCode, servicetype, date);
                }
                else
                {
                    return null;
                }
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Mar 2015
        /// Purpose : To Save data into the database
        /// </summary>
        /// <param name="ResourceAllocationSlotVO"></param>
        /// <returns></returns>
        public ResourceAllocationSlotVO UpdateResourceAllocation(ResourceAllocationSlotVO resourceAllocationSlotData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                List<ResourceSlotVO> lstResourceSlot = _suppServiceResourceAllocRepository.GetSlotConfiguration(DateTime.Now, _PortCode);
                //bool status = false;
                ResourceSlotVO slotVO = resourceAllocationSlotData.ResourceSlots.Find(s => s.ResourceName != null);
                ResourceSlotVO slotvo = lstResourceSlot.Find(a => a.SlotNumber == slotVO.SlotNumber);
                ResourceAllocation res = new ResourceAllocation();

                res.ResourceAllocationID = resourceAllocationSlotData.ResourceAllocationID;
                res.ServiceReferenceType = ServiceReferenceType.SupplementoryService;
                res.ServiceReferenceID = resourceAllocationSlotData.ServiceReferenceID;
                res.OperationType = slotVO.ServiceTypeCode;
                res.ResourceID = slotVO.ResourceID != 0 ? slotVO.ResourceID : null;
                res.CreatedBy = resourceAllocationSlotData.CreatedBy;
                res.CreatedDate = DateTime.Parse(resourceAllocationSlotData.CreatedDate, CultureInfo.InvariantCulture);
                res.ModifiedBy = _UserId;
                res.ModifiedDate = DateTime.Now;
                res.RecordStatus = "A";

                if (resourceAllocationSlotData.IsConfirm == "true")
                {
                    res.TaskStatus = ResourceAllcationWorkFlowStatus.Confirmed;
                }
                else
                {
                    res.TaskStatus = slotVO.TaskStatus;
                }

                //if (slotVO.TaskStatus == ResourceAllcationWorkFlowStatus.Scheduled || slotVO.TaskStatus == ResourceAllcationWorkFlowStatus.Overridden)
                //{
                //    if (slotVO.ResourceID != 0)
                //    {
                //        res.TaskStatus = ResourceAllcationWorkFlowStatus.Confirmed;
                //    }
                //    else
                //    {
                //        res.TaskStatus = ResourceAllcationWorkFlowStatus.Pending;
                //    }
                //    res.StartTime = default(DateTime?);
                //    res.EndTime = default(DateTime?);
                //    res.AcknowledgeDate = default(DateTime?);
                //    status = true;
                //}
                //else if (slotVO.TaskStatus == ResourceAllcationWorkFlowStatus.Pending)
                //{
                //    res.TaskStatus = ResourceAllcationWorkFlowStatus.Pending;
                //    res.StartTime = default(DateTime?);
                //    res.EndTime = default(DateTime?);
                //    res.AcknowledgeDate = default(DateTime?);
                //    status = true;
                //}
                //else
                //{
                //res.TaskStatus = slotVO.TaskStatus;
                res.StartTime = resourceAllocationSlotData.StartTime != "" ? DateTime.Parse(resourceAllocationSlotData.StartTime, CultureInfo.InvariantCulture) : default(DateTime?);
                res.EndTime = resourceAllocationSlotData.EndTime != "" ? DateTime.Parse(resourceAllocationSlotData.EndTime, CultureInfo.InvariantCulture) : default(DateTime?);
                res.AcknowledgeDate = resourceAllocationSlotData.AcknowledgeDate != null ? Convert.ToDateTime(resourceAllocationSlotData.AcknowledgeDate, CultureInfo.InvariantCulture) : default(DateTime?);
                //}

                //if (status)
                //{
                res.AllocationDate = slotvo.AllocationDate;
                res.AllocSlot = slotvo.SlotPeriod;
                res.ObjectState = ObjectState.Modified;

                _unitOfWork.Repository<ResourceAllocation>().Update(res);


                #region Sending Email for SAMSA Stop / Arrest
                var _user = _userRepository.GetUserById(_UserId);
                CompanyVO nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = _user.UserType;
                nextStepCompany.UserTypeId = _user.UserTypeID;
                int userID = Convert.ToInt32(res.ResourceID, CultureInfo.InvariantCulture);

                // Send Notifications when Vessel Arrested
                if (resourceAllocationSlotData.TaskStatus == "SCHD")
                {
                    if (res.ResourceID > 0)
                    {
                        var _resourceAssignedCode = WFStatus.ResourceAssigned;
                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                    }
                }
                else if (resourceAllocationSlotData.TaskStatus == "PNDG")
                {
                    if (resourceAllocationSlotData.ResourceID > 0)
                    {
                        var _resourceAssignedCode = WFStatus.ResourceRemoved;
                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                    }
                }
                else if (slotVO.TaskStatus == "OVRD")
                {
                    if (resourceAllocationSlotData.ResourceID == 0)
                    {
                        var _resourceAssignedCode = WFStatus.ResourceAssigned;
                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                    }
                    else if (resourceAllocationSlotData.ResourceID > 0 && resourceAllocationSlotData.ResourceID == res.ResourceID)
                    {
                        var _resourceAssignedCode = WFStatus.ResourceUpdated;
                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                    }
                    else if (resourceAllocationSlotData.ResourceID > 0 && resourceAllocationSlotData.ResourceID != res.ResourceID)
                    {
                        var _resourceAssignedCode = WFStatus.ResourceAssigned;
                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);

                        _resourceAssignedCode = WFStatus.ResourceRemoved;
                        _notificationpublisher.Publish(_entity.GetEntitiesNotification(EntityCodes.SupplemetaryResourceAllocation).EntityID, res.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _resourceAssignedCode);
                    }
                }

                #endregion
                //}
                _unitOfWork.SaveChanges();

                return resourceAllocationSlotData;
            });
        }

        public List<ResourceSlotVO> GetActiveResourceSlots()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppServiceResourceAllocRepository.GetActiveResourceSlots(_PortCode);
            });
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 30th Mar 2015
        /// Purpose : To Save data into the database
        /// </summary>
        /// <param name="ResourceAllocationSlotVO"></param>
        /// <returns></returns>
        public string UpdateResourceAllocationSlotById(string resourceAllocationId)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                var resourceallocation = _unitOfWork.Repository<ResourceAllocation>().Find(Convert.ToInt32(resourceAllocationId));
                var allocSlot = String.Empty;
                var hour = DateTime.Now.Hour;
                DateTime date = DateTime.Now;
                var slot = new ResourceSlotVO();

                var slotconfig = _suppServiceResourceAllocRepository.GetSlotConfiguration(Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture), _PortCode);

                if (Convert.ToDateTime(resourceallocation.AllocationDate).Date < DateTime.Now.Date)
                {
                    foreach (var s in from s in slotconfig
                                      let slotPeriod = s.SlotPeriod.Split('-')
                                      where int.Parse(slotPeriod[0]) >= hour && hour < int.Parse(slotPeriod[1])
                                      select s)
                    {
                        allocSlot = s.SlotPeriod;
                        break;
                    }

                    int slotNumber = slotconfig.Find(s => s.SlotPeriod == allocSlot).SlotNumber + 1;

                    var count = slotconfig.Find(s => s.SlotNumber == slotNumber);

                    if (count == null)
                    {
                        List<int> shiftids = slotconfig.Select(ss => ss.ShiftID).Distinct().ToList();
                        int newshiftid = shiftids.ElementAt(0);
                        slot = slotconfig.Find(s => s.ShiftID == newshiftid);
                        date = slot.AllocationDate.AddDays(1);
                    }
                    else
                    {
                        slot.SlotPeriod = count.SlotPeriod;
                    }
                }
                else
                {
                    allocSlot = resourceallocation.AllocSlot;

                    int shiftid = slotconfig.Find(s => s.SlotPeriod == allocSlot).ShiftID;
                    int newshiftid;

                    List<int> shiftids = slotconfig.Select(ss => ss.ShiftID).Distinct().ToList();
                    var shiftindex = shiftids.FindIndex(s => s == shiftid);
                    var shiftidscount = shiftids.Count - 1;

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
                }

                resourceallocation.TaskStatus = ResourceAllcationWorkFlowStatus.Scheduled;
                resourceallocation.ResourceID = null;
                resourceallocation.ModifiedBy = _UserId;
                resourceallocation.ModifiedDate = DateTime.Now;
                resourceallocation.ObjectState = ObjectState.Modified;
                resourceallocation.AllocSlot = slot.SlotPeriod;
                resourceallocation.AllocationDate = date;

                _unitOfWork.Repository<ResourceAllocation>().Update(resourceallocation);
                _unitOfWork.SaveChanges();

                return "true";
            });
        }
    }
}
