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
    public class ServiceRequestShiftingWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private ServiceRequest _servicerequest;
        private const string _entityCode = EntityCodes.ServiceRequest_Shifting;
        private IAccountRepository _accountRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;
        private string _remarks;
        private IWorkFlowEngine<ServiceRequestWorkFlow> wfEngine;
        private IServiceRequestRepository _serviceRequestRepository;
        private ServiceRequestWorkFlow _servicerequestworkflow;
        private IVesselCallRepository _vesselCallRepository;
        private ISuppServiceResourceAllocRepository _suppServiceResourceAllocRepository = null;
        private CompanyVO vo = null;

        public ServiceRequestShiftingWorkFlow(IUnitOfWork unitOfWork, ServiceRequest request, string remarks)
        {
           
            _unitOfWork = unitOfWork;
            _servicerequest = request;
            _remarks = remarks;
            _accountRepository = new AccountRepository(unitOfWork);
            _serviceRequestRepository = new ServiceRequestRepository(unitOfWork);
            _vesselCallRepository = new VesselCallRepository(unitOfWork);
            _servicerequestworkflow = new ServiceRequestWorkFlow(unitOfWork,null,null);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(unitOfWork);
            _suppServiceResourceAllocRepository = new SuppServiceResourceAllocRepository(_unitOfWork);         
            vo = new CompanyVO();
        }

        public Entity Entity
        {
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public int userid
        {
            get { return _servicerequest.CreatedBy; }
        }
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(_servicerequest.ArrivalNotification.PortCode);
                return portcodes;
            }
        }

        public string ReferenceId
        {
            get { return Convert.ToString(_servicerequest.ServiceRequestID, CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public int GetRequestStatus(string pEntityCode, string pReferenceNo)
        {
            var wfportcode = _servicerequest.ArrivalNotification.PortCode;

            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortGeneralConfig>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode, _approvecode = ConfigName.ApprovedCode } equals new { taskcode = _portGeneralConfigurationRepository.GetWFApprovedCode(wfportcode), portcode = pc.PortCode, _approvecode = pc.ConfigName }
                              
                               where e.EntityCode == pEntityCode
                                 && w.ReferenceID == pReferenceNo                              

                               select w).Count();

            return _entitycode;
        }

        public ServiceRequestShiftingWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }

        public void UpdateStatus()
        {
        }

        public string ReferenceData
        {
            get { return Common.GetTokensDictionaryForReferenceData(Entity, _servicerequest); }
        }



        private bool ValidateServiceRequest()
        {

            DateTime dt1 = _servicerequest.MovementDateTime;
            bool bunkering = false;   
            if (_servicerequest.ServiceRequestID == null || _servicerequest.ServiceRequestID == 0)
            {
                if (_servicerequest.ArrivalNotification == null)
                {
                    throw new BusinessExceptions("Your VCN Is Not yet Approved by BerthPlanner");
                }

                var vcm = _serviceRequestRepository.GetVCallMovtAtVCN(_servicerequest.VCN, _servicerequest.ArrivalNotification.PortCode);
                if (vcm == null)
                {
                    throw new BusinessExceptions("Your VCN Is Not yet Approved by BerthPlanner");
                }




                var vesselall = _serviceRequestRepository.GetAllVesselCallMovements(_servicerequest.VCN, _servicerequest.ArrivalNotification.PortCode);

                if (vesselall.Count > 0)
                {
                    foreach (var item in vesselall)
                    {
                        if (item.MovementType == "SHMV" && item.MovementStatus != "BERT")
                        {
                            throw new BusinessExceptions("Another Shifting Request is in Process for this VCN");
                        }
                    }
                }
               

                if (_servicerequest.ArrivalNotification != null)
                {
                    List<string> ArrivalReasons = _unitOfWork.Repository<ArrivalReason>().Queryable().Where(s => s.RecordStatus == RecordStatus.Active && s.VCN == _servicerequest.VCN).Select(s => s.Reason).ToList<String>();

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

                var objVesselCall = _vesselCallRepository.VesselCallDetails(_servicerequest.VCN);
                var currenttime = DateTime.Now;
                var servdata = _serviceRequestRepository.GetServRequestDeatailsForValidation(_servicerequest.VCN);
                var portconfigurationTime = _portGeneralConfigurationRepository.GetPortConfiguration(_servicerequest.ArrivalNotification.PortCode, ConfigName.ServiceRequestCondition1);
                var portconfigurationTime1 = _portGeneralConfigurationRepository.GetPortConfiguration(_servicerequest.ArrivalNotification.PortCode, ConfigName.ServiceRequestBunkersCondition);

                var autoSloBlocking = _serviceRequestRepository.GetBlockedSlots(_servicerequest.ArrivalNotification.PortCode);

                var AutoConfiguredSlots = _serviceRequestRepository.GetAutoConfiguredSlots(_servicerequest.MovementDateTime, _servicerequest.ArrivalNotification.PortCode);
                var totalSlotsAvailable = _serviceRequestRepository.GetTotalSlotsAvailable(_servicerequest.MovementDateTime, _servicerequest.MovementSlot, _servicerequest.ArrivalNotification.PortCode);
                var ETASlot = _serviceRequestRepository.GetSlotPeriodBySlotDate(objVesselCall.ETA, _servicerequest.ArrivalNotification.PortCode);
                //mahesh k 21/09/2023 NIT_IPMS01 shifting request can be raised without arraival request.
                var servdataarrival = _serviceRequestRepository.GetServRequestDeatailsForValidationForArrival(_servicerequest.VCN);
                if (servdataarrival.Count == 0)
                {
                    if (_servicerequest.MovementType != MovementTypes.ARRIVAL)
                    {
                        throw new BusinessExceptions("Arrival Request should be initiated first.....");
                    }
                }
                //mahesh k 
                if (_servicerequest.MovementType == MovementTypes.SHIFTING || _servicerequest.MovementType == MovementTypes.WARPING || _servicerequest.MovementType == MovementTypes.SAILING)
                {
                    DateTime dt4 = Convert.ToDateTime(objVesselCall.ETA);
                    DateTime dt5 = Convert.ToDateTime(objVesselCall.ETD);

                    if (dt5 > dt4)
                    {
                        if (!(dt4 <= _servicerequest.MovementDateTime && _servicerequest.MovementDateTime <= dt5))
                        {
                            if (bunkering == true)
                            {
                                if (_servicerequest.MovementDateTime <= ETASlot.StartDate && _servicerequest.MovementDateTime <= ETASlot.EndDate)
                                {
                                    throw new BusinessExceptions("Movement Date & Slot for Raised Request Should be between ETA and ETD");
                                }
                            }
                            else
                            {
                                throw new BusinessExceptions("Movement Date & Slot for Raised Request Should be between ETA and ETD");
                            }
                        }
                    }
                }

                if (_servicerequest.MovementDateTime != null)
                {
                    if (_servicerequest.IsUpdateMovement == true)
                    {
                        if (bunkering == true)
                        {
                            TimeSpan timespan = _servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
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
                                var CurrentSlot = _serviceRequestRepository.GetSlotPeriodBySlotDate(currenttime, _servicerequest.ArrivalNotification.PortCode);
                                if (_servicerequest.MovementDateTime <= CurrentSlot.StartDate && _servicerequest.MovementDateTime <= CurrentSlot.EndDate)
                                {
                                    throw new BusinessExceptions("Service Request Movement Date & Slot should be greater than current Date & Time");
                                }

                                //if (_servicerequest.MovementDateTime < Convert.ToDateTime(currenttime))
                                //{
                                //    throw new BusinessExceptions("Service Request Movement Date & Slot should be greater than current Date & Time");
                                //}

                            }
                        }
                        else
                        {
                            TimeSpan timespan = _servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
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
                        //mahesh k (21/09/2023)NIT_IPMS03 Blocked can still be booked
                        var servicemomentDatetime= _servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                        //end mahesh K

                        //if (ab1 < _servicerequest.MovementDateTime && _servicerequest.MovementDateTime < ab2)
                        if (ab1 < servicemomentDatetime && servicemomentDatetime < ab2)                            
                        {
                            throw new BusinessExceptions("Slots from " + fromDate + ' ' + slot.SlotFrom + " to " + toDate + ' ' + slot.SlotTo + " are blocked due. to " + slot.ReasonName);
                        }
                    }

                    if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                    {
                        if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                        {
                            throw new BusinessExceptions("Cannot assign the slot " + _servicerequest.MovementSlot + " as it reached the maximum limit. Please select another Slot");
                        }
                    }


                }
                
                foreach (var ser in servdata)
                {
                    if (ser.VCN == _servicerequest.VCN)
                    {                       


                        if (_servicerequest.MovementType == MovementTypes.SHIFTING || _servicerequest.MovementType == MovementTypes.WARPING)
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

                if (_servicerequest != null)
                {
                    List<string> ArrivalReasons = _unitOfWork.Repository<ArrivalReason>().Queryable().Where(s => s.RecordStatus == RecordStatus.Active && s.VCN == _servicerequest.VCN).Select(s => s.Reason).ToList<String>();

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

                var objVesselCalls = _vesselCallRepository.VesselCallDetails(_servicerequest.VCN);
                var portconfigurationTime = _portGeneralConfigurationRepository.GetPortConfiguration(objVesselCalls.FromPositionPortCode, ConfigName.ServiceRequestCondition1);
                var portconfigurationTime1 = _portGeneralConfigurationRepository.GetPortConfiguration(objVesselCalls.FromPositionPortCode, ConfigName.ServiceRequestBunkersCondition);
                var vcmObj = _serviceRequestRepository.GetVCallMovtAtVCN(_servicerequest.VCN, objVesselCalls.FromPositionPortCode);
                var autoSloBlocking = _serviceRequestRepository.GetBlockedSlots(objVesselCalls.FromPositionPortCode);

                var AutoConfiguredSlots = _serviceRequestRepository.GetAutoConfiguredSlots(_servicerequest.MovementDateTime, objVesselCalls.FromPositionPortCode);
                var totalSlotsAvailable = _serviceRequestRepository.GetTotalSlotsAvailable(_servicerequest.MovementDateTime, _servicerequest.MovementSlot, objVesselCalls.FromPositionPortCode);
                var ETASlot = _serviceRequestRepository.GetSlotPeriodBySlotDate(objVesselCalls.ETA, objVesselCalls.FromPositionPortCode);

                var currenttime = DateTime.Now;
                DateTime dt2 = Convert.ToDateTime(objVesselCalls.ETA, CultureInfo.InvariantCulture);
                DateTime dt3 = Convert.ToDateTime(objVesselCalls.ETD, CultureInfo.InvariantCulture);
                DateTime dt4 = Convert.ToDateTime(vcmObj.ATB, CultureInfo.InvariantCulture);

                if (dt3 > dt2)
                {
                    if (!(dt2 <= _servicerequest.MovementDateTime && _servicerequest.MovementDateTime <= dt3))
                    {
                        if (bunkering == true)
                        {
                            if (_servicerequest.MovementDateTime <= ETASlot.StartDate && _servicerequest.MovementDateTime <= ETASlot.EndDate)
                            {
                                throw new BusinessExceptions("Movement Date & Slot for Updating Request Should be between ETA and ETD");
                            }
                        }
                        else
                        {
                            throw new BusinessExceptions("Movement Date & Slot for Updating Request Should be between ETA and ETD");
                        }                        
                    }
                    if (_servicerequest.MovementDateTime != null)
                    {
                        if (_servicerequest.IsUpdateMovement == true)
                        {
                            if (bunkering == true)
                            {
                                TimeSpan timespan = _servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
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

                                    var CurrentSlot = _serviceRequestRepository.GetSlotPeriodBySlotDate(currenttime, objVesselCalls.FromPositionPortCode);
                                    if (_servicerequest.MovementDateTime <= CurrentSlot.StartDate && _servicerequest.MovementDateTime <= CurrentSlot.EndDate)
                                    {
                                        throw new BusinessExceptions("Service Request Movement Date & Slot should be greater than current Date & Time");
                                    }
                                    //if (_servicerequest.MovementDateTime < Convert.ToDateTime(currenttime))
                                    //{
                                    //    throw new BusinessExceptions("Service Request Movement Date & Slot should be greater than current Date & Time");
                                    //}

                                }
                            }
                            else
                            {
                                TimeSpan timespan = _servicerequest.MovementDateTime - Convert.ToDateTime(currenttime);
                                double timeDifference = timespan.TotalMinutes;
                                double timelimit2 = Convert.ToDouble(portconfigurationTime, CultureInfo.InvariantCulture) / 60;
                                if (timeDifference < Convert.ToDouble(portconfigurationTime, CultureInfo.InvariantCulture))
                                {
                                    throw new BusinessExceptions("The Movement Date & Slot should be atleast " + timelimit2 + " hours in future from the current Date & Time");
                                }
                            }
                        }
                        if (_servicerequest.MovementDateTime != null)
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
                            



                                if (ab1 < _servicerequest.MovementDateTime && _servicerequest.MovementDateTime < ab2)
                                {
                                    throw new BusinessExceptions("Slots from " + fromDate + ' ' + slot.SlotFrom + " to " + toDate + ' ' + slot.SlotTo + " are blocked due to " + slot.ReasonName);
                                }
                            }
                        }

                        if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                        {
                            if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                            {
                                throw new BusinessExceptions("Cannot assign the slot " + _servicerequest.MovementSlot + " as it reached the maximum limit. Please select another Slot");
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
            if (_servicerequest.ArrivalNotification == null)
            {
                _servicerequest.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(_servicerequest.VCN);
            }
            if (_servicerequest.OwnSteam == "True")
                _servicerequest.OwnSteam = "Y";
            else
                _servicerequest.OwnSteam = "N";
            if (_servicerequest.NoMainEngine == "True")
                _servicerequest.NoMainEngine = "Y";
            else
                _servicerequest.NoMainEngine = "N";
            if (_servicerequest.IsTidal == "True")
                _servicerequest.IsTidal = "Y";
            else
                _servicerequest.IsTidal = "N";
            List<ServiceRequestSailing> serviceRequestSailings = _servicerequest.ServiceRequestSailings.ToList();
            Document doc = new Document();
            if (serviceRequestSailings.Count > 0)
            {
                serviceRequestSailings.ElementAt(0).Document = null;

            }

            List<ServiceRequestDocument> serviceRequestDocumentList = _servicerequest.ServiceRequestDocuments.ToList();
            List<ServiceRequestShifting> serviceRequestShiftings = _servicerequest.ServiceRequestShiftings.ToList();
            List<ServiceRequestWarping> serviceRequestWarpings = _servicerequest.ServiceRequestWarpings.ToList();

            _servicerequest.ServiceRequestShiftings = null;
            _servicerequest.ServiceRequestSailings = null;
            _servicerequest.ServiceRequestWarpings = null;
            _servicerequest.ServiceRequestDocuments = null;

            _servicerequest.ObjectState = ObjectState.Modified;
            //added mahesh K NIT_IPMS04            
            string[] slotstperiod = _servicerequest.SlotPeriod.Split('-');
            var AutoConfiguredSlots = _serviceRequestRepository.GetAutoConfiguredSlots(_servicerequest.MovementDateTime, _servicerequest.ArrivalNotification.PortCode);
            if (slotstperiod[1] == SlotPeriodTimeStatus.slotperiod1)
            {
                _servicerequest.MovementDateTime = _servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
            }
            //end
            _unitOfWork.Repository<ServiceRequest>().Update(_servicerequest);

            var brt = _unitOfWork.ExecuteSqlCommand("delete dbo.ServiceRequestDocument where ServiceRequestID = @p0", _servicerequest.ServiceRequestID);

            if (serviceRequestDocumentList.Count > 0)
            {
                foreach (var document in serviceRequestDocumentList)
                {
                    document.ServiceRequestID = _servicerequest.ServiceRequestID;
                    document.CreatedBy = _servicerequest.CreatedBy;
                    document.CreatedDate = _servicerequest.CreatedDate;
                    document.ModifiedBy = _servicerequest.ModifiedBy;
                    document.ModifiedDate = _servicerequest.ModifiedDate;
                    document.RecordStatus = "A";
                }

                _unitOfWork.Repository<ServiceRequestDocument>().InsertRange(serviceRequestDocumentList);
            }


            foreach (var item1 in serviceRequestSailings)
            {
                item1.ServiceRequestID = _servicerequest.ServiceRequestID;
                if (item1.MarineRevenueCleared == "True")
                    item1.MarineRevenueCleared = "Y";
                else
                    item1.MarineRevenueCleared = "N";
                item1.CreatedBy = _servicerequest.CreatedBy;
                item1.CreatedDate = _servicerequest.CreatedDate;
                item1.ModifiedBy = _servicerequest.ModifiedBy;
                item1.ModifiedDate = _servicerequest.ModifiedDate;
                item1.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestSailing>().Update(item1);

            }
            foreach (var item2 in serviceRequestShiftings)
            {
                item2.ServiceRequestID = _servicerequest.ServiceRequestID;
                item2.CreatedBy = _servicerequest.CreatedBy;
                item2.CreatedDate = _servicerequest.CreatedDate;
                item2.ModifiedBy = _servicerequest.ModifiedBy;
                item2.ModifiedDate = _servicerequest.ModifiedDate;
                item2.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestShifting>().Update(item2);

            }
            foreach (var item3 in serviceRequestWarpings)
            {
                item3.ServiceRequestID = _servicerequest.ServiceRequestID;
                item3.CreatedBy = _servicerequest.CreatedBy;
                item3.CreatedDate = _servicerequest.CreatedDate;
                item3.ModifiedBy = _servicerequest.ModifiedBy;
                item3.ModifiedDate = _servicerequest.ModifiedDate;
                item3.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestWarping>().Update(item3);

            }
            _unitOfWork.SaveChanges();
        }



        public void Create()
        {

            ValidateServiceRequest();
            if (_servicerequest.ArrivalNotification == null)
            {
                _servicerequest.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(_servicerequest.VCN);
            }
            List<ServiceRequestSailing> serviceRequestSailings = _servicerequest.ServiceRequestSailings.ToList();
            Document doc = new Document();
            if (serviceRequestSailings.Count > 0)
            {
                serviceRequestSailings.ElementAt(0).Document = null;
            }


            List<ServiceRequestDocument> serviceRequestDocumentList = _servicerequest.ServiceRequestDocuments.ToList();
            List<ServiceRequestShifting> serviceRequestShiftings = _servicerequest.ServiceRequestShiftings.ToList();
            List<ServiceRequestWarping> serviceRequestWarpings = _servicerequest.ServiceRequestWarpings.ToList();

            _servicerequest.ServiceRequestShiftings = null;
            _servicerequest.ServiceRequestSailings = null;
            _servicerequest.ServiceRequestWarpings = null;
            _servicerequest.ServiceRequestDocuments = null;



            _servicerequest.ObjectState = ObjectState.Added;
            if (_servicerequest.OwnSteam == "True")
                _servicerequest.OwnSteam = "Y";
            else
                _servicerequest.OwnSteam = "N";
            if (_servicerequest.NoMainEngine == "True")
                _servicerequest.NoMainEngine = "Y";
            else
                _servicerequest.NoMainEngine = "N";
            if (_servicerequest.IsTidal == "True")
                _servicerequest.IsTidal = "Y";
            else
                _servicerequest.IsTidal = "N";
            //mahesh k NIT_IPMS04
            string[] slotstperiod = _servicerequest.SlotPeriod.Split('-');
            var AutoConfiguredSlots = _serviceRequestRepository.GetAutoConfiguredSlots(_servicerequest.MovementDateTime, _servicerequest.ArrivalNotification.PortCode);
            if (slotstperiod[1] == SlotPeriodTimeStatus.slotperiod1)
            {
                _servicerequest.MovementDateTime = _servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
            }
            //mahesh K NIT_IPMS04
            _unitOfWork.Repository<ServiceRequest>().Insert(_servicerequest);

            if (serviceRequestDocumentList.Count > 0)
            {
                foreach (var document in serviceRequestDocumentList)
                {                    
                    document.CreatedBy = _servicerequest.CreatedBy;
                    document.CreatedDate = _servicerequest.CreatedDate;
                    document.ModifiedBy = _servicerequest.ModifiedBy;
                    document.ModifiedDate = _servicerequest.ModifiedDate;
                    document.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestDocument>().Insert(document);
                }


            }

            foreach (var item1 in serviceRequestSailings)
            {
                item1.ServiceRequestID = _servicerequest.ServiceRequestID;

                if (item1.MarineRevenueCleared == "True")
                    item1.MarineRevenueCleared = "Y";
                else
                    item1.MarineRevenueCleared = "N";
                item1.CreatedBy = _servicerequest.CreatedBy;
                item1.CreatedDate = _servicerequest.CreatedDate;
                item1.ModifiedBy = _servicerequest.ModifiedBy;
                item1.ModifiedDate = _servicerequest.ModifiedDate;
                item1.RecordStatus = "A";

                _unitOfWork.Repository<ServiceRequestSailing>().Insert(item1);

            }
            foreach (var item2 in serviceRequestShiftings)
            {
                item2.ServiceRequestID = _servicerequest.ServiceRequestID;
                item2.CreatedBy = _servicerequest.CreatedBy;
                item2.CreatedDate = _servicerequest.CreatedDate;
                item2.ModifiedBy = _servicerequest.ModifiedBy;
                item2.ModifiedDate = _servicerequest.ModifiedDate;
                item2.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestShifting>().Insert(item2);

            }
            foreach (var item3 in serviceRequestWarpings)
            {
                item3.ServiceRequestID = _servicerequest.ServiceRequestID;
                item3.CreatedBy = _servicerequest.CreatedBy;
                item3.CreatedDate = _servicerequest.CreatedDate;
                item3.ModifiedBy = _servicerequest.ModifiedBy;
                item3.ModifiedDate = _servicerequest.ModifiedDate;
                item3.RecordStatus = "A";
                _unitOfWork.Repository<ServiceRequestWarping>().Insert(item3);

            }
            _unitOfWork.SaveChanges();
        }

        public void Cancel()
        {

            if (_servicerequest.ArrivalNotification == null)
            {
                _servicerequest.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(_servicerequest.VCN);
            }
            List<ServiceRequestSailing> serviceRequestSailings = _servicerequest.ServiceRequestSailings.ToList();
            Document doc = new Document();
            if (serviceRequestSailings.Count > 0)
            {
                serviceRequestSailings.ElementAt(0).Document = null;
            }

            List<ServiceRequestShifting> serviceRequestShiftings = _servicerequest.ServiceRequestShiftings.ToList();
            List<ServiceRequestWarping> serviceRequestWarpings = _servicerequest.ServiceRequestWarpings.ToList();
            if (_servicerequest.OwnSteam == "True")
                _servicerequest.OwnSteam = "Y";
            else
                _servicerequest.OwnSteam = "N";
            if (_servicerequest.NoMainEngine == "True")
                _servicerequest.NoMainEngine = "Y";
            else
                _servicerequest.NoMainEngine = "N";
            if (_servicerequest.IsTidal == "True")
                _servicerequest.IsTidal = "Y";
            else
                _servicerequest.IsTidal = "N";

            if (serviceRequestSailings.Count > 0)
            {
                foreach (var item1 in serviceRequestSailings)
                {
                    item1.ServiceRequestID = _servicerequest.ServiceRequestID;
                    if (item1.MarineRevenueCleared == "True")
                        item1.MarineRevenueCleared = "Y";
                    else
                        item1.MarineRevenueCleared = "N";
                    item1.CreatedBy = _servicerequest.CreatedBy;
                    item1.CreatedDate = _servicerequest.CreatedDate;
                    item1.ModifiedBy = _servicerequest.ModifiedBy;
                    item1.ModifiedDate = _servicerequest.ModifiedDate;
                    item1.RecordStatus = "A";

                    _unitOfWork.Repository<ServiceRequestSailing>().Insert(item1);

                }
            }
            if (serviceRequestShiftings.Count > 0)
            {
                foreach (var item2 in serviceRequestShiftings)
                {
                    item2.ServiceRequestID = _servicerequest.ServiceRequestID;
                    item2.CreatedBy = _servicerequest.CreatedBy;
                    item2.CreatedDate = _servicerequest.CreatedDate;
                    item2.ModifiedBy = _servicerequest.ModifiedBy;
                    item2.ModifiedDate = _servicerequest.ModifiedDate;
                    item2.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestShifting>().Insert(item2);

                }
            }
            if (serviceRequestWarpings.Count > 0)
            {
                foreach (var item3 in serviceRequestWarpings)
                {
                    item3.ServiceRequestID = _servicerequest.ServiceRequestID;
                    item3.CreatedBy = _servicerequest.CreatedBy;
                    item3.CreatedDate = _servicerequest.CreatedDate;
                    item3.ModifiedBy = _servicerequest.ModifiedBy;
                    item3.ModifiedDate = _servicerequest.ModifiedDate;
                    item3.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestWarping>().Insert(item3);

                }
            }
            if (_servicerequest.MovementType == "SHMV")
            {
                _unitOfWork.ExecuteSqlCommand("Update dbo.VesselCallMovement set FromPositionQuayCode = @p0, FromPositionBerthCode = @p0, FromPositionBollardCode = @p0, ToPositionPortCode = @p0, ToPositionQuayCode = @p0, ToPositionBerthCode = @p0, ToPositionBollardCode = @p0, MovementStatus = @p1, RecordStatus = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where ServiceRequestID = @p5", null, MovementStatus.PENDING, RecordStatus.InActive, _servicerequest.ModifiedBy, _servicerequest.ModifiedDate, _servicerequest.ServiceRequestID);                
            }
            
            
            _servicerequest.ObjectState = ObjectState.Modified;
            _servicerequest.RecordStatus = "I";
            _unitOfWork.Repository<ServiceRequest>().Update(_servicerequest);
            _unitOfWork.SaveChanges();

        }

        public void Reject()
        {

            if (_servicerequest.ArrivalNotification == null)
            {
                _servicerequest.ArrivalNotification = _unitOfWork.Repository<ArrivalNotification>().Find(_servicerequest.VCN);
            }
            List<ServiceRequestSailing> serviceRequestSailings = _servicerequest.ServiceRequestSailings.ToList();
            Document doc = new Document();
            if (serviceRequestSailings.Count > 0)
            {
                serviceRequestSailings.ElementAt(0).Document = null;
            }

            List<ServiceRequestShifting> serviceRequestShiftings = _servicerequest.ServiceRequestShiftings.ToList();
            List<ServiceRequestWarping> serviceRequestWarpings = _servicerequest.ServiceRequestWarpings.ToList();
            if (_servicerequest.OwnSteam == "True")
                _servicerequest.OwnSteam = "Y";
            else
                _servicerequest.OwnSteam = "N";
            if (_servicerequest.NoMainEngine == "True")
                _servicerequest.NoMainEngine = "Y";
            else
                _servicerequest.NoMainEngine = "N";
            if (_servicerequest.IsTidal == "True")
                _servicerequest.IsTidal = "Y";
            else
                _servicerequest.IsTidal = "N";

            if (serviceRequestSailings.Count > 0)
            {
                foreach (var item1 in serviceRequestSailings)
                {
                    item1.ServiceRequestID = _servicerequest.ServiceRequestID;
                    if (item1.MarineRevenueCleared == "True")
                        item1.MarineRevenueCleared = "Y";
                    else
                        item1.MarineRevenueCleared = "N";
                    item1.CreatedBy = _servicerequest.CreatedBy;
                    item1.CreatedDate = _servicerequest.CreatedDate;
                    item1.ModifiedBy = _servicerequest.ModifiedBy;
                    item1.ModifiedDate = _servicerequest.ModifiedDate;
                    item1.RecordStatus = "A";

                    _unitOfWork.Repository<ServiceRequestSailing>().Insert(item1);

                }
            }
            if (serviceRequestShiftings.Count > 0)
            {
                foreach (var item2 in serviceRequestShiftings)
                {
                    item2.ServiceRequestID = _servicerequest.ServiceRequestID;
                    item2.CreatedBy = _servicerequest.CreatedBy;
                    item2.CreatedDate = _servicerequest.CreatedDate;
                    item2.ModifiedBy = _servicerequest.ModifiedBy;
                    item2.ModifiedDate = _servicerequest.ModifiedDate;
                    item2.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestShifting>().Insert(item2);

                }
            }
            if (serviceRequestWarpings.Count > 0)
            {
                foreach (var item3 in serviceRequestWarpings)
                {
                    item3.ServiceRequestID = _servicerequest.ServiceRequestID;
                    item3.CreatedBy = _servicerequest.CreatedBy;
                    item3.CreatedDate = _servicerequest.CreatedDate;
                    item3.ModifiedBy = _servicerequest.ModifiedBy;
                    item3.ModifiedDate = _servicerequest.ModifiedDate;
                    item3.RecordStatus = "A";
                    _unitOfWork.Repository<ServiceRequestWarping>().Insert(item3);

                }
            }

            _unitOfWork.ExecuteSqlCommand("Update dbo.VesselCallMovement set FromPositionQuayCode = @p0, FromPositionBerthCode = @p0, FromPositionBollardCode = @p0, ToPositionPortCode = @p0, ToPositionQuayCode = @p0, ToPositionBerthCode = @p0, ToPositionBollardCode = @p0, MovementStatus = @p1, RecordStatus = @p2, ModifiedBy = @p3, ModifiedDate = @p4 where ServiceRequestID = @p5", null, MovementStatus.PENDING, RecordStatus.InActive, _servicerequest.ModifiedBy, _servicerequest.ModifiedDate, _servicerequest.ServiceRequestID);

            _servicerequest.ObjectState = ObjectState.Modified;
            _servicerequest.RecordStatus = "I";
            _unitOfWork.Repository<ServiceRequest>().Update(_servicerequest);
            _unitOfWork.SaveChanges();

        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            _servicerequest.BPWorkflowInstanceId = workFlowInstanceId;

            _servicerequest.IsFinal = null; //It is Computed column, should not get any value before saving
            _unitOfWork.Repository<ServiceRequest>().Update(_servicerequest);
            _unitOfWork.SaveChanges();
        }

        public void InsertMovement()
        {
            if (_servicerequest.RecordStatus == "A")
            {

                var AutoConfiguredSlots = _serviceRequestRepository.GetAutoConfiguredSlots(_servicerequest.MovementDateTime, _servicerequest.ArrivalNotification.PortCode);
                var totalSlotsAvailable = _serviceRequestRepository.GetTotalSlotsAvailable(_servicerequest.MovementDateTime, _servicerequest.MovementSlot, _servicerequest.ArrivalNotification.PortCode);

                string servID = Convert.ToString(_servicerequest.ServiceRequestID, CultureInfo.InvariantCulture);
                #region Adding Record in VesselCallmovement table, if the request is approved
          
                VesselCallMovement objVesselCallMovement = new VesselCallMovement();

                
                var vcallm = _serviceRequestRepository.GetVesselCallMovement(_servicerequest.ServiceRequestID);

                if (vcallm.Count == 0)
                {
                    //DateTime MovementStarttime = servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                   // vcnatvcm.Slot = _servicerequestworkflow.GetSlotPeriodBySlotdate(MovementStarttime, servicerequest.ArrivalNotification.PortCode);

                    //vcnatvcm.SlotDate = MovementStarttime;

                    objVesselCallMovement.VCN = _servicerequest.VCN;
                    objVesselCallMovement.ServiceRequestID = _servicerequest.ServiceRequestID;
                    objVesselCallMovement.MovementType = _servicerequest.MovementType;
                    objVesselCallMovement.MovementDateTime = _servicerequest.MovementDateTime;
                    DateTime MovementStarttime = _servicerequest.MovementDateTime.AddMinutes(-Convert.ToInt16(AutoConfiguredSlots.Duration));
                   // objVesselCallMovement.Slot = _servicerequest.ArrivalNotification.PortCode;
                    objVesselCallMovement.Slot = _servicerequestworkflow.GetSlotPeriodBySlotdate(MovementStarttime, _servicerequest.ArrivalNotification.PortCode);
                    objVesselCallMovement.SlotDate = MovementStarttime;
                    objVesselCallMovement.FromPositionPortCode = _servicerequest.ArrivalNotification.PortCode;
                    var vcalldtls = _vesselCallRepository.VesselCallDetails(_servicerequest.VCN);


                    if (_servicerequest.MovementType == MovementTypes.SHIFTING)
                    {
                        objVesselCallMovement.SlotStatus = MovementStatus.PEND;
                        objVesselCallMovement.MovementStatus = MovementStatus.PENDING;
                        objVesselCallMovement.ETB = _servicerequest.MovementDateTime;
                        objVesselCallMovement.ETUB = vcalldtls.ETUB;
                    }

                    if (AutoConfiguredSlots != null && totalSlotsAvailable.Count > 0)
                    {
                        if (totalSlotsAvailable.Count >= AutoConfiguredSlots.NoofSlots)
                        {
                            objVesselCallMovement.SlotStatus = AutomatedSlotStatus.Pending;
                        }
                    }


                    objVesselCallMovement.RecordStatus = _servicerequest.RecordStatus;
                    objVesselCallMovement.CreatedBy = _servicerequest.CreatedBy;
                    objVesselCallMovement.CreatedDate = _servicerequest.CreatedDate;
                    objVesselCallMovement.ModifiedBy = _servicerequest.ModifiedBy;
                    objVesselCallMovement.ModifiedDate = _servicerequest.ModifiedDate;
                    objVesselCallMovement.CreatedBy = _servicerequest.CreatedBy;
                    _unitOfWork.Repository<VesselCallMovement>().Insert(objVesselCallMovement);
                    _unitOfWork.SaveChanges();
                }                
                #endregion
            }
        }

        public void UpdateVO()
        {
            var usertype = _unitOfWork.Repository<User>().Find(_servicerequest.CreatedBy).UserType;
            if (usertype == UserType.Agent)
            {
                vo.UserType = UserType.Agent;
                vo.UserTypeId = _servicerequest.ArrivalNotification.AgentID;
            }
            else if (usertype == UserType.TerminalOperator)
            {
                vo.UserType = UserType.TerminalOperator;
                vo.UserTypeId = Convert.ToInt32(_servicerequest.ArrivalNotification.TerminalOperatorID, CultureInfo.InvariantCulture);
            }
            else
            {
                vo.UserType = UserType.Employee;
                vo.UserTypeId = _servicerequest.CreatedBy;
            }
        }        

        public void AutoSlotBlocking()
        {

            var autoSloBlocking = _serviceRequestRepository.GetBlockedSlots(_servicerequest.ArrivalNotification.PortCode);

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

                if (ab1 < _servicerequest.MovementDateTime && _servicerequest.MovementDateTime < ab2)
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
                    InsertMovement();
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