using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class ResourceAllocationService : ServiceBase, IResourceAllocationService
    {
        private ISubCategoryRepository _subcategoryRepository;
        private IResourceAllocationRepository _resourceallocationRepository;
        private IBerthRepository _berthRepository;
      //  private IWorkFlowTaskRepository _workFlowTaskRepository;
       // private IAccountRepository _accountRepository;
        private IBollardRepository _bollardRepository;
        
        private INotificationPublisher notificationpublisher;
        private IEntityRepository entityRepository;
        private IUserRepository userRepository;
        private IPortConfigurationRepository portConfigurationRepository;

        private const string _entityCode = EntityCodes.ServiceRecording;
        
       // private readonly ILog log;

        public ResourceAllocationService(IUnitOfWork unitOfWork)
        {
            //Get logger
          //  log = LogManager.GetLogger(typeof(ArrivalNotificationService));
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
           _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _resourceallocationRepository = new ResourceAllocationRepository(_unitOfWork);
           // _workFlowTaskRepository = new WorkFlowTaskRepository(_unitOfWork);
         //   _accountRepository = new AccountRepository(_unitOfWork);
            _bollardRepository = new BollardRepository(_unitOfWork);

            notificationpublisher = new NotificationPublisher(_unitOfWork);
            entityRepository = new EntityRepository(_unitOfWork);
            userRepository = new UserRepository(_unitOfWork);
            portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
        }

        public ResourceAllocationService()
        {
            //Get logger
     //       log = LogManager.GetLogger(typeof(ArrivalNotificationService));
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
           _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _resourceallocationRepository = new ResourceAllocationRepository(_unitOfWork);
           // _workFlowTaskRepository = new WorkFlowTaskRepository(_unitOfWork);
           // _accountRepository = new AccountRepository(_unitOfWork);
            _bollardRepository = new BollardRepository(_unitOfWork);
       
            notificationpublisher = new NotificationPublisher(_unitOfWork);
            entityRepository = new EntityRepository(_unitOfWork);
            userRepository = new UserRepository(_unitOfWork);
            portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
        }

        /// <summary>
        /// Method to Get ResourceAllocations
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="vesselName"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public List<ResourceAllocationVO> GetResourceAllocations(string vcn, string vesselName, string resourceName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var arrivalNotifications = _resourceallocationRepository.GetResourceAllocationDetails(_PortCode, vcn, vesselName, resourceName);
                return arrivalNotifications;
            });
        }


        /// <summary>
        /// Method to Get ResourceAllocationdetails_VCN
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<ResourceAllocationVO> GetresourceAllocationdetailsByVCN(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var arrivalNotifications = _resourceallocationRepository.GetResourceAllocationDetailsByVCN(_PortCode, vcn);
                return arrivalNotifications;
            });
        }

        /// <summary>
        /// Method to Get ResourceAllocations
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="vesselName"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public List<OtherServiceRecordingVO> GetWaterDetailsList(string resourceAllocationID, string action)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var arrivalNotifications = _resourceallocationRepository.GetWaterDetailsList(resourceAllocationID,action);
                return arrivalNotifications;
            });
        }

     
       


        /// <summary>
        /// Method to Get ResourceAllocationformDetails
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ResourceAllocationVO GetResourceAllocationformDetails(ResourceAllocationVO resource)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _resourceallocationRepository.GetResourceAllocationFormDetails(resource);
            });
        }

        /// <summary>
        /// Method to Get ResourceAllocationReferenceDataVO
        /// </summary>
        /// <returns></returns>
        public ResourceAllocationReferenceDataVO GetResourceAllocationReferenceDataVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ResourceAllocationReferenceDataVO VO = new ResourceAllocationReferenceDataVO();
                VO.Berths = _berthRepository.GetBerths(_PortCode);
                VO.MopsDelays = _subcategoryRepository.GetMopsDelays();
                // delay reasons code
                VO.DelayReasons = _subcategoryRepository.GetDelayReasons();
                VO.WaterResources = _resourceallocationRepository.GetReourceNamesByType(_PortCode, ResourceType.Water);
                VO.FloatingResources = _resourceallocationRepository.GetReourceNamesByType(_PortCode, ResourceType.Floating);
                return VO;
            });
        }

        /// <summary>
        /// Method to Get BollardsInBerths
        /// </summary>
        /// <param name="berthkey"></param>
        /// <returns></returns>
        public List<BollardVO> GetBollardsInBerths(string berthkey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string[] fields = berthkey.Split('.');
                string portCode = fields[0];
                string quayCode = fields[1];
                string berthCode = fields[2];

                var boolards = _bollardRepository.GetBollardsInBerths(portCode, quayCode, berthCode);
                return boolards;
            });
        }

        /// <summary>
        /// Method to Update ResourceAllocationformDetails
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ResourceAllocationVO UpdateResourceAllocationformDetails(ResourceAllocationVO resource)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var resourcedetails = _resourceallocationRepository.UpdateResourceAllocationFormDetails(resource, _UserId, _PortCode);

                var entityid = entityRepository.GetEntitiesNotification(EntityCodes.ServiceRecording).EntityID;
                var nextStepCompany = userRepository.GetUserDetails(_UserId);

                if (resourcedetails != null)
                {
                    if (resourcedetails.OperationType == ServiceTypeCode.Pilotage)
                    {
                        if (resourcedetails.MovementType == MovementTypes.ARRIVAL)
                        {
                            if (resourcedetails.PilotageServiceRecording != null)
                            {
                                if (resourcedetails.PilotageServiceRecording.PilotOff != null && resourcedetails.PilotageServiceRecording.PilotOnBoard != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                                }

                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SAILING)
                        {
                            if (resourcedetails.PilotageServiceRecording != null)
                            {
                                if (resourcedetails.PilotageServiceRecording.PilotOff != null && resourcedetails.PilotageServiceRecording.PilotOnBoard != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Resubmit);
                                }
                            }
                        }
                    }
                    else if (resourcedetails.OperationType == ServiceTypeCode.BerthMaster)
                    {
                        if (resourcedetails.MovementType == MovementTypes.ARRIVAL)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Approved);
                                }
                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SHIFTING)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Verified);
                                }
                            }
                        }
                         else if (resourcedetails.MovementType == MovementTypes.WARPING)
                         {
                             if (resourcedetails.ShiftingBerthingTaskExecution != null)
                             {
                                 if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                 {
                                     notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Confirmed);
                                 }
                             }
                         }
                         else if (resourcedetails.MovementType == MovementTypes.SAILING)
                         {
                             if (resourcedetails.ShiftingBerthingTaskExecution != null)
                             {
                                 if (resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                 {
                                     notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.ResubmitUpdate);
                                 }
                             }
                         }
                    }
                }

                return resourcedetails;
            });
        }
        public int CheckMeterNoExists(string meterno, int resourceAllocationID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _resourceallocationRepository.CheckMeterNoExists(meterno, resourceAllocationID);
            });
        }
       

        /// <summary>
        /// Method to Update ResourceAllocationformDetails
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ResourceAllocationVO SaveWaterAllocationDetails(ResourceAllocationVO resource)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var resourcedetails = _resourceallocationRepository.SaveWaterAllocationDetails(resource, _UserId, _PortCode);

                var entityid = entityRepository.GetEntitiesNotification(EntityCodes.ServiceRecording).EntityID;
                var nextStepCompany = userRepository.GetUserDetails(_UserId);

                if (resourcedetails != null)
                {
                    if (resourcedetails.OperationType == ServiceTypeCode.Pilotage)
                    {
                        if (resourcedetails.MovementType == MovementTypes.ARRIVAL)
                        {
                            if (resourcedetails.PilotageServiceRecording != null)
                            {
                                if (resourcedetails.PilotageServiceRecording.PilotOff != null && resourcedetails.PilotageServiceRecording.PilotOnBoard != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                                }

                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SAILING)
                        {
                            if (resourcedetails.PilotageServiceRecording != null)
                            {
                                if (resourcedetails.PilotageServiceRecording.PilotOff != null && resourcedetails.PilotageServiceRecording.PilotOnBoard != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Resubmit);
                                }
                            }
                        }
                    }
                    else if (resourcedetails.OperationType == ServiceTypeCode.BerthMaster)
                    {
                        if (resourcedetails.MovementType == MovementTypes.ARRIVAL)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Approved);
                                }
                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SHIFTING)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Verified);
                                }
                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.WARPING)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Confirmed);
                                }
                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SAILING)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.ResubmitUpdate);
                                }
                            }
                        }
                    }
                }


                return resourcedetails;
            });
        }



        /// <summary>
        /// Method to Modify ResourceAllocationformDetails
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public ResourceAllocationVO ModifyResourceAllocationformDetails(ResourceAllocationVO resource)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var resourcedetails = _resourceallocationRepository.ModifyResourceAllocationFormDetails(resource, _UserId, _PortCode);

                var entityid = entityRepository.GetEntitiesNotification(EntityCodes.ServiceRecording).EntityID;
                var nextStepCompany = userRepository.GetUserDetails(_UserId);

                if (resourcedetails != null)
                {
                    if (resourcedetails.OperationType == ServiceTypeCode.Pilotage)
                    {
                        if (resourcedetails.MovementType == MovementTypes.ARRIVAL)
                        {
                            if (resourcedetails.PilotageServiceRecording != null)
                            {
                                if (resourcedetails.PilotageServiceRecording.PilotOff != null && resourcedetails.PilotageServiceRecording.PilotOnBoard != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                                }

                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SAILING)
                        {
                            if (resourcedetails.PilotageServiceRecording != null)
                            {
                                if (resourcedetails.PilotageServiceRecording.PilotOff != null && resourcedetails.PilotageServiceRecording.PilotOnBoard != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Resubmit);
                                }
                            }
                        }
                    }
                    else if (resourcedetails.OperationType == ServiceTypeCode.BerthMaster)
                    {
                        if (resourcedetails.MovementType == MovementTypes.ARRIVAL)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Approved);
                                }
                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SHIFTING)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Verified);
                                }
                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.WARPING)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.LastLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineIn != null && resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.Confirmed);
                                }
                            }
                        }
                        else if (resourcedetails.MovementType == MovementTypes.SAILING)
                        {
                            if (resourcedetails.ShiftingBerthingTaskExecution != null)
                            {
                                if (resourcedetails.ShiftingBerthingTaskExecution.FirstLineOut != null && resourcedetails.ShiftingBerthingTaskExecution.LastLineOut != null)
                                {
                                    notificationpublisher.Publish(entityid, resourcedetails.ResourceAllocationID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, WFStatus.ResubmitUpdate);
                                }
                            }
                        }
                    }
                }

                return resourcedetails;


            });
        }

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 22nd Sep 2014
        /// Purpose : To get list of resources based on VCN, date and portcode
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<ResourceAllocationVO> GetResourceAllocationByDate(string date, string portcode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _resourceallocationRepository.GetResourceAllocationByDate(date, _PortCode);
            });
        }


        /// <summary>
        /// Method to Get ResourceAllocation_ResourceAllocID
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public ResourceAllocationVO GetresourceAllocationByResourceAllocId(string strResourceAllocationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var arrivalNotifications = _resourceallocationRepository.GetResourceAllocationByResourceAllocationId(_PortCode, strResourceAllocationId);
                return arrivalNotifications;
            });
        }
       
      

       
        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 28th April 2014
        /// Purpose: To verify and update resource allocation record
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public string VerifyResourceAllocationDetails(string operationType, string movementType, string resourceAllocationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _resourceallocationRepository.VerifyResourceAllocationDetails(operationType, movementType, resourceAllocationId);
            });
        }
        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<RevenuePostingVO> ServiceRecordingVcnDetailsforAutocomplete(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _resourceallocationRepository.ServiceRecordingVcnDetailsforAutocomplete(searchvalue, _PortCode);
            });
        }
        /// <summary>
        ///  Vessel / IMO No. Autocomplete for Search
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<VesselVO> ServiceRecordingVesselDetailsforAutocomplete(string searchvalue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _resourceallocationRepository.ServiceRecordingVesselDetailsforAutocomplete(_PortCode, searchvalue);
            });
        }
        //ServiceRecordingResourceDetailsforAutocomplete

        /// <summary>
        ///  Vessel / IMO No. Autocomplete for Search
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<UserMasterVO> ServiceRecordingResourceDetailsforAutocomplete(string searchvalue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _resourceallocationRepository.ServiceRecordingResourceDetailsforAutocomplete(_PortCode, searchvalue);
            });
        }
    }
}
